Imports System.Runtime.InteropServices

Public Module DnsFunctions

    ' DNS constants
    Private Const DNS_TYPE_TEXT As UShort = 16

    Private Const DNS_QUERY_STANDARD As UInteger = 0

    ' Import DnsQuery_W from dnsapi.dll
    <DllImport("dnsapi.dll", EntryPoint:="DnsQuery_W", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Function DnsQuery(
        ByVal pszName As String,
        ByVal wType As UShort,
        ByVal options As UInteger,
        ByVal pExtra As IntPtr,
        ByRef ppQueryResults As IntPtr,
        ByVal pReserved As IntPtr) As Integer
    End Function

    ' Import DnsRecordListFree from dnsapi.dll
    <DllImport("dnsapi.dll", SetLastError:=True)>
    Private Sub DnsRecordListFree(
        ByVal pRecordList As IntPtr,
        ByVal freeType As Integer)
    End Sub

    ' Structure for DNS TXT Record
    <StructLayout(LayoutKind.Sequential)>
    Private Structure DnsRecordTxt
        Public pNext As IntPtr
        Public pName As IntPtr
        Public wType As UShort
        Public wDataLength As UShort
        Public flags As UInteger
        Public dwTtl As UInteger
        Public dwReserved As UInteger
        Public stringCount As UShort
        Public pStringArray As IntPtr
    End Structure

    Public Function GetTxtRecord(
         domain As String,
         prefix As String
    ) As String
        Dim pQueryResults As IntPtr = IntPtr.Zero
        Try
            ' Perform DNS query
            Dim queryResult As Object = DnsQuery(domain, DNS_TYPE_TEXT, DNS_QUERY_STANDARD, IntPtr.Zero, pQueryResults, IntPtr.Zero)
            If queryResult <> 0 Then
                Return $"DNS query failed with error code {queryResult}."
            End If

            ' Process the linked list of TXT records
            Dim currentRecord As IntPtr = pQueryResults
            While currentRecord <> IntPtr.Zero
                Dim txtRecord As Object = Marshal.PtrToStructure(Of DnsRecordTxt)(currentRecord)

                ' Extract TXT strings from the record
                Dim i As Integer
                Dim txtPointer As IntPtr
                Dim txt As Object
                For i = 0 To CInt(txtRecord.stringCount) - 1
                    txtPointer = Marshal.ReadIntPtr(txtRecord.pStringArray, i * IntPtr.Size)
                    txt = Marshal.PtrToStringUni(txtPointer)

                    ' Match the prefix
                    If CType(txt.StartsWith(prefix), Boolean) Then
                        Return txt.Substring("version=".Length).Trim().ToString
                    End If
                Next

                ' Move to the next record
                currentRecord = CType(txtRecord.pNext, IntPtr)
            End While

            ' No matching record found
            Return "No matching TXT record found."
        Catch ex As Exception
            Return $"Error: {ex.Message}"
        Finally
            ' Free the DNS record list memory
            If pQueryResults <> IntPtr.Zero Then
                DnsRecordListFree(pQueryResults, 0)
            End If
        End Try
    End Function

End Module