Imports System.IO
Imports System.Reflection
Imports Newtonsoft.Json

Public Module BuildupFactorRepository
    '* Usage:       Single, cached source of the gamma-ray buildup factor
    '*              G-P coefficient database. The data lives in the
    '*              embedded resource BuildupFactorData.json (compiled
    '*              into RadToolz.dll, which is packed into the .xll),
    '*              transcribed from JAERI-M 90-110 (Sakamoto and Tanaka,
    '*              1990), Appendix D - see DDR-0017 for exact table/page
    '*              citations. It is parsed exactly once per Excel
    '*              session, the first time GetRows() is called from any
    '*              thread; every call after that reuses the cached data
    '*              instead of re-parsing. Mirrors DecaySeriesRepository's
    '*              Lazy-cache pattern.
    '*
    '*              Lead has no rows at 0.015/0.020 MeV: the source data's
    '*              G-P fit is not defined there (see DDR-0017). Callers
    '*              must not assume every material covers the full
    '*              0.015-15 MeV grid.

    Private Const ResourceSuffix As String = "BuildupFactorData.json"

    ' LazyThreadSafetyMode.ExecutionAndPublication (the default for this
    ' constructor) guarantees the factory runs at most once even if two
    ' Excel calculation threads call GetRows() at the same time - one
    ' thread runs LoadFromEmbeddedResource, any others block and then
    ' share its result.
    Private ReadOnly _cache As New Lazy(Of List(Of BuildupFactorItem))(AddressOf LoadFromEmbeddedResource)

    ' (Material|BuildupType), both upper-cased -> that group's rows, sorted
    ' ascending by Energy. Built once from the already-cached flat list.
    Private ReadOnly _index As New Lazy(Of Dictionary(Of String, List(Of BuildupFactorItem)))(AddressOf BuildIndex)

    Private ReadOnly _emptyRows As New List(Of BuildupFactorItem)

    ''' <summary>
    ''' Returns the G-P coefficient rows for the given material and buildup
    ''' type, sorted ascending by Energy, or an empty list if that
    ''' (material, buildupType) combination is not in the database.
    ''' </summary>
    ''' <param name="material">Canonical material name (e.g. "Iron"), case-insensitive.</param>
    ''' <param name="buildupType">Canonical buildup type ("Exposure" or "Absorption"), case-insensitive.</param>
    Public Function GetRows(material As String, buildupType As String) As IReadOnlyList(Of BuildupFactorItem)
        Dim rows As List(Of BuildupFactorItem) = Nothing
        Dim key As String = IndexKey(material, buildupType)

        Return If(_index.Value.TryGetValue(key, rows), rows, _emptyRows)
    End Function

    Private Function IndexKey(material As String, buildupType As String) As String
        Return material.ToUpperInvariant() & "|" & buildupType.ToUpperInvariant()
    End Function

    Private Function BuildIndex() As Dictionary(Of String, List(Of BuildupFactorItem))
        '* Usage:       Groups the already-cached flat row list by
        '*              (Material, BuildupType) and sorts each group
        '*              ascending by Energy, once.

        Dim map As New Dictionary(Of String, List(Of BuildupFactorItem))
        Dim items As List(Of BuildupFactorItem) = _cache.Value

        For Each item As BuildupFactorItem In items
            Dim key As String = IndexKey(item.Material, item.BuildupType)
            Dim group As List(Of BuildupFactorItem) = Nothing

            If Not map.TryGetValue(key, group) Then
                group = New List(Of BuildupFactorItem)
                map(key) = group
            End If

            group.Add(item)
        Next

        For Each group As List(Of BuildupFactorItem) In map.Values
            group.Sort(Function(x, y) x.Energy.CompareTo(y.Energy))
        Next

        Return map
    End Function

    Private Function LoadFromEmbeddedResource() As List(Of BuildupFactorItem)
        '* Usage:       Reads and deserializes BuildupFactorData.json from
        '*              this assembly's embedded resources.
        '* Returns:     A populated List(Of BuildupFactorItem). Throws if
        '*              the resource is missing or fails to parse - a
        '*              silently empty database is worse than a loud
        '*              failure here.

        Dim Msg As String

        Try
            Dim asm As Assembly = Assembly.GetExecutingAssembly()

            Dim resourceName As String = asm.GetManifestResourceNames().
                FirstOrDefault(Function(n) n.EndsWith(ResourceSuffix, StringComparison.OrdinalIgnoreCase))

            If resourceName Is Nothing Then
                Throw New InvalidOperationException(
                    "Embedded resource ending in '" & ResourceSuffix & "' was not found in " & asm.FullName &
                    ". Check that BuildupFactorData.json's Build Action is set to 'Embedded Resource'.")
            End If

            Using stream As Stream = asm.GetManifestResourceStream(resourceName)
                Using reader As New StreamReader(stream)
                    Dim json As String = reader.ReadToEnd()
                    Dim items As List(Of BuildupFactorItem) = JsonConvert.DeserializeObject(Of List(Of BuildupFactorItem))(json)

                    If items Is Nothing OrElse items.Count = 0 Then
                        Throw New InvalidOperationException("BuildupFactorData.json parsed to an empty list.")
                    End If

                    Return items
                End Using
            End Using
        Catch ex As Exception
            Msg = "RadToolz could not load its buildup factor database (BuildupFactorData.json)." & Chr(13) &
                  "Error: " & ex.Message
            Dim msgBoxResult As Object = MsgBox(Msg, vbCritical, "Error")
            Throw

        End Try

    End Function

End Module
