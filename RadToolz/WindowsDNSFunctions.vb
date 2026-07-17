Imports System.Runtime.InteropServices

' Windows DNS TXT-record lookup via a direct P/Invoke of dnsapi.dll, used by
' RTZUpdate (RadToolzFunctions.vb) to read the version-check TXT record
' published at radtoolz.com. Not an Excel visible module: nothing here is
' decorated with <ExcelFunction>, so none of it appears in the Function
' Wizard or is directly callable from a worksheet cell - GetTxtRecord below
' is an internal helper called from RTZUpdate's Public Function body.
Public Module DnsFunctions

    ' DNS constants
    ' DNS resource record type TXT, per RFC 1035 / the Windows DNS API's
    ' DNS_TYPE_TEXT constant (windns.h).
    Private Const DNS_TYPE_TEXT As UShort = 16

    ' Standard recursive query, no special DnsQuery options (DNS_QUERY_STANDARD).
    Private Const DNS_QUERY_STANDARD As UInteger = 0

    ' Import DnsQuery_W from dnsapi.dll
#Disable Warning IDE0081 ' 'ByVal' keyword is unnecessary and can be removed.

    ' P/Invoke signature for the Windows DnsQuery_W API (Unicode entry point).
    ' ppQueryResults receives a pointer to the head of a caller-owned, singly
    ' linked list of DNS_RECORD structures (here typed as DnsRecordTxt, since
    ' only TXT records are requested) that MUST be released via
    ' DnsRecordListFree once processed - GetTxtRecord's Try/Finally below is
    ' that release, not a routine .NET cleanup.
    <DllImport("dnsapi.dll", EntryPoint:="DnsQuery_W", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Function DnsQuery(
        ByVal pszName As String,
        ByVal wType As UShort,
        ByVal options As UInteger,
        ByVal pExtra As IntPtr,
        ByRef ppQueryResults As IntPtr,
        ByVal pReserved As IntPtr) As Integer
#Enable Warning IDE0081 ' 'ByVal' keyword is unnecessary and can be removed.
    End Function

    ' Import DnsRecordListFree from dnsapi.dll
#Disable Warning IDE0081 ' 'ByVal' keyword is unnecessary and can be removed.

    ' Releases the unmanaged DNS_RECORD list returned by DnsQuery. freeType:=0
    ' below is DnsFreeRecordListDeep, freeing the whole linked list rather than
    ' just the head node.
    <DllImport("dnsapi.dll", SetLastError:=True)>
    Private Sub DnsRecordListFree(
        ByVal pRecordList As IntPtr,
        ByVal freeType As Integer)
#Enable Warning IDE0081 ' 'ByVal' keyword is unnecessary and can be removed.
    End Sub

    ' Structure for DNS TXT Record
    ' Managed layout mirroring the native DNS_TXT_DATA record shape returned by
    ' DnsQuery for a TXT-type result. LayoutKind.Sequential with IntPtr/UShort/
    ' UInteger fields matches the native struct's field order and size exactly -
    ' do not reorder or resize these fields without also verifying against the
    ' current windns.h layout, since Marshal.PtrToStructure trusts this shape
    ' completely and a mismatch corrupts every field read after the first
    ' misaligned one.
    <StructLayout(LayoutKind.Sequential)>
    Private Structure DnsRecordTxt
        Public pNext As IntPtr           ' -> next DnsRecordTxt in the list, or IntPtr.Zero at the tail
        Public pName As IntPtr           ' -> queried record name (unused here)
        Public wType As UShort           ' record type; DNS_TYPE_TEXT for every node reachable from a TXT query
        Public wDataLength As UShort
        Public flags As UInteger
        Public dwTtl As UInteger
        Public dwReserved As UInteger
        Public stringCount As UInteger   ' number of strings in pStringArray (native DWORD dwStringCount)
        Public pStringArray As IntPtr    ' -> array of stringCount native Unicode string pointers
    End Structure

    ''' <summary>
    ''' Queries <paramref name="domain"/> for TXT records and returns the first
    ''' string (trimmed) found in a record whose text starts with
    ''' <paramref name="prefix"/>, with the prefix itself stripped off.
    ''' </summary>
    ''' <param name="domain">Fully-qualified domain name to query (e.g. "radtoolz.com").</param>
    ''' <param name="prefix">Prefix used only to select the matching TXT string.</param>
    ''' <returns>
    ''' The matched TXT text with its prefix removed, or a human-readable
    ''' failure string ("Invalid input parameters.", "DNS query failed with
    ''' error code N.", "No matching TXT record found.", or "Error: ...") -
    ''' never throws across this boundary. Today's only caller, RTZUpdate,
    ''' calls this as GetTxtRecord("radtoolz.com", "version=") and expects a
    ''' bare version number string back.
    ''' </returns>
    Public Function GetTxtRecord(
         domain As String,
         prefix As String
    ) As String
        ' Validate inputs
        If String.IsNullOrWhiteSpace(domain) OrElse String.IsNullOrWhiteSpace(prefix) Then
            Return "Invalid input parameters."
        End If

        Dim pQueryResults As IntPtr = IntPtr.Zero
        Try
            ' Perform DNS query
            Dim queryResult As Integer = DnsQuery(domain, DNS_TYPE_TEXT, DNS_QUERY_STANDARD, IntPtr.Zero, pQueryResults, IntPtr.Zero)
            If queryResult <> 0 Then
                Return $"DNS query failed with error code {queryResult}."
            End If

            ' Process the linked list of TXT records
            Dim currentRecord As IntPtr = pQueryResults
            While currentRecord <> IntPtr.Zero
                Dim txtRecord As DnsRecordTxt = Marshal.PtrToStructure(Of DnsRecordTxt)(currentRecord)

                ' Extract TXT strings from the record
                Dim i As Integer
                Dim txtPointer As IntPtr
                Dim txt As String
                ' pStringArray is a flexible array member: its storage starts at
                ' this field's own offset in the native record, so the marshaled
                ' txtRecord.pStringArray value is already pStringArray(0) itself,
                ' not the array's base address. Recompute the real base address
                ' from the original native record pointer so each i-th pointer is
                ' read from the array, not from inside the first string's text.
                Dim stringArrayAddr As IntPtr = IntPtr.Add(currentRecord, Marshal.OffsetOf(GetType(DnsRecordTxt), "pStringArray").ToInt32())
                For i = 0 To CInt(txtRecord.stringCount) - 1
                    ' Each native string pointer is IntPtr.Size bytes apart in
                    ' pStringArray; read the i-th pointer, then marshal the
                    ' Unicode C string it points to into a managed String.
                    txtPointer = Marshal.ReadIntPtr(stringArrayAddr, i * IntPtr.Size)
                    txt = Marshal.PtrToStringUni(txtPointer)

                    ' Match the prefix
                    If txt IsNot Nothing AndAlso txt.StartsWith(prefix, StringComparison.Ordinal) Then
                        Return txt.Substring(prefix.Length).Trim().ToString
                    End If
                Next

                ' Move to the next record
                currentRecord = txtRecord.pNext
            End While

            ' No matching record found
            Return "No matching TXT record found."
        Catch ex As Exception
            ' Broad catch is intentional: this is effectively a boundary for
            ' RTZUpdate (an <ExcelFunction>), and any failure - network down,
            ' marshaling error, etc. - must degrade to a descriptive string
            ' rather than propagate and destabilize the calculation chain.
            Return $"Error: {ex.Message}"
        Finally
            ' Free the DNS record list memory
            ' Always runs, even on the exception path above, so a successful
            ' DnsQuery is never leaked regardless of how the Try block exits.
            If pQueryResults <> IntPtr.Zero Then
                DnsRecordListFree(pQueryResults, 0)
            End If
        End Try
    End Function

End Module