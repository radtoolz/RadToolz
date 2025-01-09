Imports DnsClient

Public Class DNSLookup
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
            ' Handle exceptions (e.g., network errors, invalid domain)
            Console.WriteLine($"Error fetching TXT record: {ex.Message}")
            Return New List(Of String)()
        End Try
    End Function
End Class
