Imports System.IO
Imports System.Reflection
Imports Newtonsoft.Json

Public Module DecaySeriesRepository
    '* Usage:       Single, cached source of the nuclear decay-series
    '*              database. The data lives in the embedded resource
    '*              DecaySeriesData.json (compiled into RadToolz.dll,
    '*              which is packed into the .xll). It is parsed exactly
    '*              once per Excel session, the first time GetAll() is
    '*              called from any thread; every call after that reuses
    '*              the cached data instead of re-parsing.
    '*
    '*              IMPORTANT: the DecaySeriesItem objects returned by
    '*              GetAll() are the SAME shared instances on every call,
    '*              for the life of the add-in. Treat them as read-only.
    '*              If code needs a modifiable copy, copy the properties
    '*              into a new DecaySeriesItem first, the same way
    '*              LoadDecaySeriesItem does in ProcessDecaySeries.vb.
    '*              Nothing in the existing codebase mutates a
    '*              DecaySeriesItem obtained from GetDecaySeries()/
    '*              GetAll() in place - every consumer copies fields out
    '*              before changing anything - so this is safe as long as
    '*              that convention holds going forward.
    '* Author:      Backscatter enterprises
    '* Date:        7/3/2026

    Private Const ResourceSuffix As String = "DecaySeriesData.json"

    ' LazyThreadSafetyMode.ExecutionAndPublication (the default for this
    ' constructor) guarantees the factory runs at most once even if two
    ' Excel calculation threads call GetAll() at the same time - one
    ' thread runs LoadFromEmbeddedResource, any others block and then
    ' share its result.
    Private ReadOnly _cache As New Lazy(Of List(Of DecaySeriesItem))(AddressOf LoadFromEmbeddedResource)

    ' Isotope symbol -> ascending list of positions in _cache.Value where that
    ' symbol occurs (more than one position = a decay-chain branch point).
    ' Built once, in the same forward-pass order as the embedded JSON, so the
    ' per-symbol lists are already in the sequential order GetDecayChain's
    ' branch logic depends on.
    Private ReadOnly _index As New Lazy(Of Dictionary(Of String, List(Of Integer)))(AddressOf BuildIndex)

    Private ReadOnly _emptyIndices As New List(Of Integer)

    ''' <summary>
    ''' Returns the full decay-series database as a Collection of
    ''' DecaySeriesItem, in the same order as the original hardcoded
    ''' table. Order matters: GetDecayChain's branch-point logic in
    ''' ProcessDecaySeries.vb relies on sequential position and on the
    ''' same isotope symbol legitimately appearing more than once (once
    ''' per branch).
    ''' </summary>
    ''' <returns>A new Collection each call. Building it is just 182
    ''' Collection.Add() calls against already-cached objects - no JSON
    ''' parsing and no new DecaySeriesItem allocations after the first
    ''' call.</returns>
    Public Function GetAll() As Collection
        Dim result As New Collection

        For Each item As DecaySeriesItem In _cache.Value
            result.Add(item)
        Next

        Return result
    End Function

    ''' <summary>
    ''' Returns the full decay-series database as a strongly-typed,
    ''' read-only list, in the same order as GetAll()/the embedded JSON -
    ''' with no per-call copy. Use this instead of GetAll() for internal
    ''' code that scans the whole table (e.g. GetDecayChain, ListAll),
    ''' since it avoids both the O(n) Collection.Add copy and the late
    ''' binding that comes from Collection.Item(x) being late-bound Object.
    ''' </summary>
    Public Function GetAllList() As IReadOnlyList(Of DecaySeriesItem)
        Return _cache.Value
    End Function

    ''' <summary>
    ''' Positions (0-based, into GetAllList()) where the given isotope
    ''' symbol occurs, in ascending/original-table order. Empty if the
    ''' isotope is not in the database. O(1) lookup instead of an O(n)
    ''' scan over the full table.
    ''' </summary>
    Public Function IndicesOf(isotope As String) As IReadOnlyList(Of Integer)
        Dim positions As List(Of Integer) = Nothing

        Return If(_index.Value.TryGetValue(isotope, positions), positions, _emptyIndices)
    End Function

    ''' <summary>
    ''' Number of isotope records currently loaded. Exposed mainly for
    ''' diagnostics/sanity checks (e.g. from RTZUpdate or a test).
    ''' </summary>
    Public ReadOnly Property Count As Integer
        Get
            Return _cache.Value.Count
        End Get
    End Property

    Private Function BuildIndex() As Dictionary(Of String, List(Of Integer))
        '* Usage:       Builds the isotope-symbol -> position(s) index used
        '*              by IndicesOf(), once, from the already-cached data.
        '* Author:      Backscatter enterprises
        '* Date:        7/5/2026

        Dim map As New Dictionary(Of String, List(Of Integer))(StringComparer.OrdinalIgnoreCase)
        Dim items As List(Of DecaySeriesItem) = _cache.Value

        For i As Integer = 0 To items.Count - 1
            Dim isotope As String = items(i).Isotope
            Dim positions As List(Of Integer) = Nothing

            If Not map.TryGetValue(isotope, positions) Then
                positions = New List(Of Integer)
                map(isotope) = positions
            End If

            positions.Add(i)
        Next

        Return map
    End Function

    Private Function LoadFromEmbeddedResource() As List(Of DecaySeriesItem)
        '* Usage:       Reads and deserializes DecaySeriesData.json from
        '*              this assembly's embedded resources.
        '* Returns:     A populated List(Of DecaySeriesItem). Throws if
        '*              the resource is missing or fails to parse - a
        '*              silently empty database is worse than a loud
        '*              failure here, since every RadToolz function
        '*              depends on this data.
        '* Author:      Backscatter enterprises
        '* Date:        7/3/2026

        Dim Msg As String

        Try
            Dim asm As Assembly = Assembly.GetExecutingAssembly()

            ' Resolved defensively (by suffix) rather than hardcoding the
            ' full manifest name (e.g. "RadToolz.DecaySeriesData.json"),
            ' so this keeps working if the file is later moved into a
            ' subfolder or the default namespace changes.
            Dim resourceName As String = asm.GetManifestResourceNames().
                FirstOrDefault(Function(n) n.EndsWith(ResourceSuffix, StringComparison.OrdinalIgnoreCase))

            If resourceName Is Nothing Then
                Throw New InvalidOperationException(
                    "Embedded resource ending in '" & ResourceSuffix & "' was not found in " & asm.FullName &
                    ". Check that DecaySeriesData.json's Build Action is set to 'Embedded Resource'.")
            End If

            Using stream As Stream = asm.GetManifestResourceStream(resourceName)
                Using reader As New StreamReader(stream)
                    Dim json As String = reader.ReadToEnd()
                    Dim items As List(Of DecaySeriesItem) = JsonConvert.DeserializeObject(Of List(Of DecaySeriesItem))(json)

                    If items Is Nothing OrElse items.Count = 0 Then
                        Throw New InvalidOperationException("DecaySeriesData.json parsed to an empty list.")
                    End If

                    Return items
                End Using
            End Using
        Catch ex As Exception
            Msg = "RadToolz could not load its isotope database (DecaySeriesData.json)." & Chr(13) &
                  "Error: " & ex.Message
            Dim msgBoxResult As Object = MsgBox(Msg, vbCritical, "Error")
            Throw

        End Try

    End Function

End Module