Imports DnsClient

' NOTE: as of this pass, nothing in the RadToolz solution constructs or calls
' this class (verified by searching all .vb files for "DNSLookup" / "New
' DNSLookup"). RTZUpdate (RadToolzFunctions.vb) performs its actual TXT-record
' check through the separate DnsFunctions.GetTxtRecord (WindowsDNSFunctions.vb),
' which P/Invokes dnsapi.dll directly rather than using this class's DnsClient
' (NuGet) dependency. This class appears to be an earlier or alternate
' implementation left in place. Preserved as-is per the preservation policy -
' reported here rather than removed, since deleting a public type is outside
' a comment-only pass and its removal has not been requested/approved.
Public Class DNSLookup
    ''' <summary>
    ''' Queries the given domain's TXT records via the DnsClient NuGet library
    ''' and returns every TXT string found, across all TXT records. Unlike
    ''' WindowsDNSFunctions.GetTxtRecord, this does not filter by a prefix -
    ''' callers get the raw, unfiltered TXT record contents.
    ''' </summary>
    ''' <param name="domain">Fully-qualified domain name to query.</param>
    ''' <returns>Every TXT record string for the domain, or an empty list on any lookup failure (network error, invalid domain, etc. - see Catch below).</returns>
    Public Function GetTxtRecord(domain As String) As List(Of String)
        Try
            ' Create a new LookupClient instance
            Dim lookup = New LookupClient()

            ' Query for TXT records
            Dim result = lookup.Query(domain, QueryType.TXT)

            ' Extract TXT records from the result
            Dim txtRecords As New List(Of String)()

            For Each txtRecord In result.Answers.TxtRecords()
                txtRecords.AddRange(txtRecord.Text)
            Next

            Return txtRecords
        Catch ex As Exception
            ' Broad catch is intentional here: any DNS/network failure (unreachable
            ' resolver, NXDOMAIN, timeout, etc.) should degrade to an empty result
            ' rather than throw, since this is not on a UDF calculation boundary but
            ' could still be called from one indirectly. Logged to Console rather than
            ' the add-in's own logging path, unlike WindowsDNSFunctions.GetTxtRecord.
            Console.WriteLine($"Error fetching TXT record: {ex.Message}")
            Return New List(Of String)()
        End Try
    End Function
End Class
