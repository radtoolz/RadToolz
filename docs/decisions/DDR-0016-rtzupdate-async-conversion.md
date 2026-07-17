DDR-0016: Convert RTZUpdate to an Excel-DNA async function
Status: Accepted
Date: 2026-07-17
Task: RTZUpdate on-open cell refresh (Large - exported UDF runtime
behavior change, threading model change)

Context
  DDR-0015 added a workbook-open update check with its own dialog, deciding
  independently of RTZUpdate() whether to prompt. Testing surfaced a gap:
  the request was for RTZUpdate() to "recalculate" on open, not just for a
  separate notification to appear - the =RTZUpdate() cell's own displayed
  text was expected to refresh too, for every state, not just the two
  update-available ones.

  The only legitimate way to refresh a formula cell's cached displayed
  value without destroying the formula is to actually recalculate it
  (Range.Calculate()). RTZUpdate() today is a synchronous UDF - its own
  DNS lookup (GetTxtRecord) runs inline. Calling Calculate() on it from
  the main thread (as the on-open handler must, since Excel COM only
  works from the main thread) would therefore block Excel for the
  duration of that DNS call on every qualifying workbook open - exactly
  the risk DEBT-0007 flags and the reason the on-open check was made
  async in the first place. There is no lighter-weight way around this:
  Excel does not expose a way to set a formula's cached value without
  either recalculating it for real or overwriting it with a static value
  (which would destroy the formula).

  The only way to make Calculate() itself non-blocking is Excel-DNA's
  native async UDF pattern (ExcelAsyncUtil.Run) - the cell shows its
  previous value (not #N/A, since a value already exists) until the
  background work completes, then updates automatically. This requires
  converting RTZUpdate() itself, not just the on-open path.

  A further fork surfaced during design: if RTZUpdate()'s own async
  worker keeps its existing inline MsgBox logic (as it must, to leave
  manual-invocation behavior unchanged), a Calculate() triggered from the
  on-open handler goes through that same dialog logic - there is no way
  for a plain Calculate() call to signal "this one came from workbook-open,
  add the close-the-workbook behavior" that DDR-0015 built. Per explicit
  user direction, the on-open path's dialog (with its close-workbook/
  quit-Excel-if-last behavior) stays separate from RTZUpdate()'s own
  dialog, at the cost of keeping the version-check logic duplicated
  between the two (DEBT-0015b, already recorded, still applies).

Decision
  Convert RTZUpdate() to ExcelAsyncUtil.Run. The function body becomes a
  thin wrapper; the DNS-fetch-and-compare logic and the existing inline
  MsgBox/Process.Start behavior move into a new private helper
  (ComputeRtzUpdateStatus) that runs on a background thread managed by
  ExcelAsyncUtil - GetTxtRecord's own I/O is safe to call from there (it
  touches no Excel COM), and the MsgBox/Process.Start calls are marshaled
  back to the main thread via ExcelAsyncUtil.QueueAsMacro, exactly like
  the on-open path already does. RTZUpdate's legacy On Error GoTo control
  flow is replaced with structured Try/Catch in the new helper - VB.NET
  does not support On Error GoTo inside a lambda, so this was not
  optional once the function moves into ExcelAsyncUtil.Run's delegate
  form. Manual invocation behavior (message text, MsgBox, browser launch)
  is preserved exactly; only the mechanism (async vs. synchronous) and
  the error-handling style change.

  On the on-open side (RadToolzAddIn.vb), the handler now does two
  different things depending on what its own (still-separate, still
  duplicated) check found:
  - Update available: queues rtzUpdateCell.Calculate() first (so the cell
    starts refreshing as part of the same flow, not only after the user
    declines), then shows its own dialog with the close-workbook/quit-
    Excel behavior, exactly as DDR-0015 built. RTZUpdate()'s own async
    check may show its own (generic, no close-workbook note) dialog too,
    moments later, if it still detects the update when it completes - a
    minor redundancy, accepted rather than building fragile cross-file
    coordination to suppress it.
  - No update (or the DNS lookup was inconclusive): no dialog, just calls
    Formula reassignment (cell.Formula = cell.Formula) on the found
    RTZUpdate() cell so its displayed text refreshes to the current
    status.
  FindRtzUpdateFormula (renamed from WorkbookHasRtzUpdateFormula) now
  returns the found Range itself rather than a Boolean, since the on-open
  handler needs the actual cell to act on.

  Post-implementation findings, confirmed empirically with the user in
  real Excel, in two rounds:

  Round 1: Range.Calculate() alone was not sufficient. RTZUpdate() is not
  volatile, so Excel's calculation engine considered the cell "not dirty"
  (its declared input, no_input, hadn't changed) and skipped
  re-evaluating it even via Calculate() or F9/CalculateFull - only
  re-entering the formula (F2, Enter), which bypasses Excel's
  dirty-tracking entirely, forced a fresh result. RTZUpdate() was
  therefore marked IsVolatile:=True as the apparent fix.

  Round 2: IsVolatile:=True did not actually fix F9, and further research
  found why it should never have been tried for this function in the
  first place: Volatile + ExcelAsyncUtil.Run is a documented,
  known-broken combination per Excel-DNA's own project discussion group -
  completing the async call updates the cell, which (because the
  function is volatile) triggers another recalculation, which calls the
  function again, indefinitely. IsVolatile:=True was reverted. In its
  place, the on-open handler now forces a genuine fresh evaluation via
  formula reassignment (cell.Formula = cell.Formula) - the same trick
  F2+Enter performs internally, without needing volatility. This fixes
  the on-open refresh (the original ask) without the recalculation-loop
  risk. Per explicit user direction, F9 is accepted as NOT refreshing
  RTZUpdate() going forward - consistent with any non-volatile UDF whose
  declared input hasn't changed, and the only alternative (reverting the
  async conversion entirely) was rejected as reintroducing the
  blocking-on-slow-network risk this whole conversion exists to avoid.

Alternatives Considered
  1. Unify the on-open path into RTZUpdate()'s own dialog logic (just call
     Calculate() always, let RTZUpdate() handle everything) - rejected
     per explicit user direction: loses the close-workbook/quit-Excel
     behavior already built and tested, since manual invocation and
     on-open triggering would become indistinguishable inside RTZUpdate().
  2. Keep RTZUpdate() synchronous, accept the blocking risk for the
     "refresh the cell" cases only (narrower than blocking on every open,
     since it would only fire for the non-update states) - rejected per
     explicit user direction: still a real hang risk on a slow/offline
     network, which the user was not willing to accept even in this
     narrower form.
  3. Try to find a way to pass on-open context into RTZUpdate() so it
     could conditionally add the close-workbook behavior itself (e.g. a
     shared module-level flag checked inside its async worker) - not
     pursued: inherently racy (the flag would need to survive from the
     main-thread Calculate() call through to the background worker's
     completion, seconds later, with no clean way to scope it to that
     one specific triggering call), and more fragile than accepting the
     already-recorded DEBT-0015b duplication.
  4. IsVolatile:=True - tried first, then reverted: Volatile +
     ExcelAsyncUtil.Run is a documented recalculation-loop risk per
     Excel-DNA's own project discussion group, and did not actually fix
     F9 either. Not a viable option for this function while it stays
     async.
  5. CalculateFullRebuild() on workbook open instead of targeting just the
     RTZUpdate() cell - rejected: recalculates the entire workbook,
     including RTZParams/RTZFunctions, reintroducing the disruptive-on-
     open problem DEBT-0004 specifically avoided.
  6. Formula reassignment (cell.Formula = cell.Formula) on the found cell
     - adopted: forces a genuine fresh evaluation (the same mechanism
     F2+Enter uses internally) without volatility, without touching any
     other cell in the workbook, and without the recalculation-loop risk.

Consequences
  Positive: the =RTZUpdate() cell now genuinely refreshes on workbook
    open for all states, not just the two that show a dialog, without
    reintroducing a main-thread blocking risk. RTZUpdate() becomes async
    for every caller, not just the on-open trigger - a user manually
    recalculating it also no longer blocks Excel while the DNS lookup
    runs.
  Negative / accepted trade-offs: RTZUpdate()'s exported behavior changes
    for every existing caller, not just the on-open path - a cell
    containing =RTZUpdate() will show its previous cached value (not
    immediately update) while the background check runs, rather than
    blocking until the new value is ready. This is generally an
    improvement (Excel stays responsive) but is a genuine behavior change
    to a shipped UDF, not purely internal. DEBT-0015b's duplication is
    unresolved by this change (still two independent version-check
    implementations) - accepted per explicit user direction favoring the
    close-workbook behavior over eliminating the duplication. F9/
    CalculateFull will not refresh an already-open =RTZUpdate() cell
    unless its declared input changes - by design, per explicit user
    direction, to avoid the Volatile+async recalculation-loop risk. Only
    the on-open path (via formula reassignment) and manual re-entry
    (F2+Enter) force a genuine fresh check.
  Regression implications: High (per regression_risk_matrix - public UDF
    surface, threading model, exception-handling style all change at
    once in a function that was already hand-tested working). Requires
    fresh characterization testing in real Excel, not just a clean build
    - the async conversion is a materially different runtime path than
    what was validated so far this session.
  Compatibility implications (UDF surface, persisted formats, .dna): No
    change to RTZUpdate's name, argument list, or ultimate return text
    for any given state - additive/behavioral only (async timing, not
    semantics). No .dna change expected (ExcelAsyncUtil.Run does not
    require special registration beyond the existing <ExcelFunction>
    attribute).

Verification
  Confirmed working end-to-end in real Excel by the user, across several
  rounds: (1) the async conversion itself and the initial on-open cell
  refresh attempt; (2) discovering Calculate() doesn't force a
  non-volatile UDF to re-evaluate, trying IsVolatile:=True as an apparent
  fix; (3) discovering via Excel-DNA's own project discussion group that
  Volatile+async is a documented recalculation-loop risk, reverting
  IsVolatile, and switching to formula reassignment (cell.Formula =
  cell.Formula) instead - confirmed this fixed the on-open refresh
  without needing volatility; (4) a redundant second dialog (RTZUpdate()'s
  own, triggered by the formula-reassignment refresh, on top of
  RadToolzAddIn's) reported as annoying and fixed via a locked
  coordination flag (ArmDialogSuppression/ConsumeDialogSuppression).
  Final user confirmation: "That worked EXACTLY as I wanted it to."
