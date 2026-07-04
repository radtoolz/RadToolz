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
    ''' Number of isotope records currently loaded. Exposed mainly for
    ''' diagnostics/sanity checks (e.g. from RTZUpdate or a test).
    ''' </summary>
    Public ReadOnly Property Count As Integer
        Get
            Return _cache.Value.Count
        End Get
    End Property

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