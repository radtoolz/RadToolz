Imports ExcelDna.Integration
Imports Microsoft.Office.Interop.Excel

' First IExcelAddIn implementation in this codebase (see DDR-0015). Hooks
' Application.WorkbookOpen so a workbook containing =RTZUpdate() checks for
' updates as soon as it's opened, instead of waiting for an unrelated
' recalculation to happen to trigger it. Every hooked COM event here is
' unhooked in AutoClose, per section_16_com_and_memory_lifetime_rules.
Friend NotInheritable Class RadToolzAddIn
    Implements IExcelAddIn

    ' Coordination flag with RTZUpdate/ComputeRtzUpdateStatus (RadToolzFunctions.vb):
    ' set immediately before the on-open formula-reassignment refresh below, so that
    ' one (and only one) resulting RTZUpdate() re-evaluation skips its own dialog -
    ' this dialog (with the close-workbook/quit-Excel behavior) already covers the
    ' same update. ComputeRtzUpdateStatus reads and clears it itself, under a lock,
    ' so at most one invocation is ever suppressed. A small, accepted race window
    ' exists if the user manually re-triggers RTZUpdate() in the same narrow window
    ' as the on-open check - worst case is a dialog shown or suppressed once when it
    ' shouldn't be, not anything destructive.
    Private Shared ReadOnly SuppressionLock As New Object()
    Private Shared _suppressNextRtzUpdateDialog As Boolean

    Private _app As Application
    Private _updateCheckedThisSession As Boolean

    Friend Shared Sub ArmDialogSuppression()
        SyncLock SuppressionLock
            _suppressNextRtzUpdateDialog = True
        End SyncLock
    End Sub

    ' Called by ComputeRtzUpdateStatus (RadToolzFunctions.vb) right before it would
    ' queue its own dialog. Returns True (and clears the flag) at most once per arm.
    Friend Shared Function ConsumeDialogSuppression() As Boolean
        SyncLock SuppressionLock
            Dim wasSet As Boolean = _suppressNextRtzUpdateDialog
            _suppressNextRtzUpdateDialog = False
            Return wasSet
        End SyncLock
    End Function

    Public Sub AutoOpen() Implements IExcelAddIn.AutoOpen
        _app = DirectCast(iExcel, Application)
        AddHandler _app.WorkbookOpen, AddressOf OnWorkbookOpen
    End Sub

    Public Sub AutoClose() Implements IExcelAddIn.AutoClose
        If _app IsNot Nothing Then
            RemoveHandler _app.WorkbookOpen, AddressOf OnWorkbookOpen
            _app = Nothing
        End If
    End Sub

    Private Sub OnWorkbookOpen(wb As Workbook)
        ' WorkbookOpen only ever fires on the main STA thread, so checking and
        ' setting this flag here (before any background work starts) needs no
        ' locking - there is no concurrent caller to race against.
        If _updateCheckedThisSession Then Return
        Dim rtzUpdateCell As Range = FindRtzUpdateFormula(wb)
        If rtzUpdateCell Is Nothing Then Return
        _updateCheckedThisSession = True

        ' DEBT-0007/threading_and_asynchrony: the DNS check must not run on the
        ' main thread here - this now fires unconditionally on every qualifying
        ' workbook open, so a slow/offline network would otherwise hang Excel on
        ' every such open, not just occasionally. CheckForUpdate touches no
        ' Excel COM object; only the QueueAsMacro callbacks below do.
        Task.Run(
            Sub()
                Dim promptMessage As String = Nothing
                Try
                    promptMessage = CheckForUpdate()
                Catch
                    ' Best-effort background check on workbook open - not worth
                    ' interrupting the user with an error dialog for.
                End Try

                If promptMessage IsNot Nothing Then
                    ' Update available: refresh the cell first, then show our own
                    ' dialog with the close-workbook/quit-Excel behavior DDR-0015
                    ' built - so RTZUpdate()'s displayed status is already
                    ' updating in the sheet as/before the browser prompt appears,
                    ' not only after the user declines it. This re-runs
                    ' RTZUpdate()'s own async check (DDR-0016) in the background;
                    ' arm the suppression flag first so that one re-evaluation
                    ' skips its own (redundant) dialog for the same update.
                    ' Reassigning Formula to itself (rather than Calculate()) forces
                    ' a genuine fresh evaluation, matching F2+Enter - RTZUpdate() is
                    ' deliberately not volatile (see its own notes: Volatile +
                    ' ExcelAsyncUtil.Run is a documented recalculation-loop risk),
                    ' so plain Calculate() is a no-op when its declared input hasn't
                    ' changed, which it never does here.
                    ArmDialogSuppression()
                    ExcelAsyncUtil.QueueAsMacro(Sub() rtzUpdateCell.Formula = rtzUpdateCell.Formula)

                    ExcelAsyncUtil.QueueAsMacro(
                        Sub()
                            ' Checked here, right before showing the dialog, rather than earlier -
                            ' the async DNS lookup above can take a few seconds, during which the
                            ' user could have opened or closed other workbooks.
                            Dim isOnlyWorkbookOpen As Boolean = (_app.Workbooks.Count = 1)
                            Dim closingNote As String = If(isOnlyWorkbookOpen,
                                "This workbook will close, and Excel will close since no other workbooks are open.",
                                "This workbook will close.")

                            Dim dialogResult As MsgBoxResult = MsgBox(
                                promptMessage & " " & closingNote, MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, "Update RadToolz")
                            If dialogResult = MsgBoxResult.Yes Then
                                Process.Start("https://github.com/radtoolz/RadToolz/releases/latest")
                                ' Nothing has been edited yet - this fires right on open - so
                                ' discarding (rather than prompting to save) is correct here.
                                wb.Close(SaveChanges:=False)
                                If _app.Workbooks.Count = 0 Then _app.Quit()
                            End If
                        End Sub)
                Else
                    ' No update (or the DNS lookup was inconclusive): no dialog,
                    ' just refresh the cell's own displayed status. RTZUpdate()
                    ' is itself an Excel-DNA async function (DDR-0016) and is not
                    ' volatile, so reassigning Formula to itself (rather than
                    ' Calculate(), which would be a no-op here) forces a genuine
                    ' fresh evaluation - kicks off its background work and returns
                    ' immediately, it does not block on the DNS lookup.
                    ExcelAsyncUtil.QueueAsMacro(Sub() rtzUpdateCell.Formula = rtzUpdateCell.Formula)
                End If
            End Sub)
    End Sub

    ' Single Cells.Find call per sheet (not a cell-by-cell loop) to detect whether
    ' this workbook has any =RTZUpdate() formula worth checking for.
    Private Function FindRtzUpdateFormula(wb As Workbook) As Range
        For Each ws As Worksheet In wb.Worksheets
            Dim foundCell As Range = Nothing
            Try
                foundCell = ws.Cells.Find(What:="RTZUpdate(", LookIn:=XlFindLookIn.xlFormulas, LookAt:=XlLookAt.xlPart)
            Catch
                ' Cells.Find can throw on unusual sheet states (e.g. a fully empty
                ' sheet on some Excel versions) - treat as not found rather than
                ' fail workbook open over a best-effort scan.
            End Try
            If foundCell IsNot Nothing Then Return foundCell
        Next
        Return Nothing
    End Function

    ' DEBT-0015b: duplicates RTZUpdate's DNS-fetch-and-compare logic
    ' (RadToolzFunctions.vb, ComputeRtzUpdateStatus) rather than extracting a
    ' shared helper - see DDR-0015/DDR-0016's Decision sections for why (this
    ' path needs its own close-workbook/quit-Excel dialog behavior that
    ' RTZUpdate()'s own dialog does not have). The redundant-second-dialog risk
    ' this created (both this check and RTZUpdate()'s own re-evaluation
    ' detecting the same update) is resolved via ArmDialogSuppression/
    ' ConsumeDialogSuppression above, not by removing the duplication itself.
    ' Returns the base MsgBox prompt text when an update should be offered (the caller
    ' appends a closing-behavior note, since whether Excel itself will also
    ' close depends on live workbook count at dialog-show time, not here),
    ' or Nothing when up to date / the version couldn't be determined.
    Private Function CheckForUpdate() As String
        Dim vers As String = GetTxtRecord("radtoolz.com", "version=")
        Dim versNum As Double

        If Not Double.TryParse(vers, Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, versNum) Then
            Return Nothing
        End If

        If versNum > RadToolzVersion Then
            Return ("RadToolz Is now at version " & vers & ".  You should update.") & vbCrLf &
                "Open browser to the latest RadToolz release on GitHub?"
        ElseIf versNum = RadToolzVersion AndAlso RadToolzPreRelease <> "" Then
            Return ("RadToolz " & vers & " has been released.  You have a pre-release version And should update.") & vbCrLf &
                "Open browser to the latest RadToolz release on GitHub?"
        End If

        Return Nothing
    End Function

End Class
