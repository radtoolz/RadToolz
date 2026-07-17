DDR-0015: Check for updates on workbook open (async, session-gated)
Status: Accepted
Date: 2026-07-17
Task: RTZUpdate on-open check (Large - new IExcelAddIn/event-hook
subsystem, threading, initialization order)

Context
  RTZUpdate() is a plain UDF (not IsMacroType, not IsVolatile). Per
  Excel-DNA's documented behavior, plain UDFs use the cell's last-saved
  cached value and do not automatically recalculate on workbook open -
  nothing in this codebase currently hooks into workbook-open behavior at
  all (confirmed: no IExcelAddIn implementation, no AutoOpen/AutoClose, no
  COM event wiring anywhere in the project today). The user wants a
  workbook containing =RTZUpdate() to reliably check for updates as soon
  as it opens, without waiting for an unrelated recalculation to happen to
  trigger it.

  Two existing debt items bear directly on how this must be built, not
  just recorded:
  - DEBT-0007 (synchronous DNS query + modal dialogs on the calc thread):
    forcing this check to run unconditionally on every qualifying
    workbook open turns an occasional cost into a guaranteed one - if left
    synchronous, every such open risks a multi-second hang on a slow or
    offline network. Per user direction, this must be async.
  - The DEBT-0004 work earlier in this session went to real effort to
    keep RTZParams/RTZFunctions from being disrupted by automatic
    workbook-open recalculation. A naive "recalculate everything on open"
    approach for this feature would silently reintroduce exactly that
    problem (sweeping RTZParams/RTZFunctions into every workbook open too,
    popping their overwrite-confirmation dialog unpredictably). The
    mechanism must specifically target RTZUpdate() formulas, not trigger a
    workbook-wide recalculation.

Decision
  Add a new IExcelAddIn implementation (RadToolzAddIn.vb - first of its
  kind in this codebase) that hooks Application.WorkbookOpen in AutoOpen()
  and unhooks it in AutoClose(), per this codebase's COM event lifecycle
  rules (every hooked event must be unhooked at teardown).

  On WorkbookOpen: scan the opened workbook's worksheets for a formula
  containing "RTZUpdate(" via Worksheet.Cells.Find(LookIn:=xlFormulas) -
  one bulk COM call per sheet, not a cell-by-cell loop. If found, and this
  is the first time this check has fired in the current Excel session
  (a simple module-level flag, checked and set synchronously on the main
  thread before any background work starts - WorkbookOpen only ever fires
  on the main STA thread, so there is no race to guard against), kick off
  the version check.

  RTZUpdate() itself is left completely untouched - not refactored, not
  called into. A new, independent, self-contained CheckForUpdate function
  (clean Try/Catch, since it is new code - not RTZUpdate's legacy On Error
  GoTo) duplicates the small fetch-and-four-way-compare logic for the
  on-open path. This is a deliberate correction from this DDR's first
  draft, which proposed extracting a shared helper - that was wrong: with
  only two call sites (RTZUpdate and the new on-open path), this
  codebase's own duplication guidance (section_09) calls for duplicating
  with a recorded debt entry, not extracting - extraction is for a third
  occurrence (Rule of Three), which this isn't. Duplicating also avoids
  surgically modifying RTZUpdate's fragile legacy On Error GoTo control
  flow, which the user has already hand-tested and confirmed works -
  touching it for this change would trade a real, unnecessary regression
  risk for a small amount of duplicated logic. Recorded as new debt
  (see debt.md) to revisit if a third use case ever needs the same check.

  The new CheckForUpdate function runs on a background Task (Task.Run),
  then marshals back to the main thread via ExcelAsyncUtil.QueueAsMacro to
  show the MsgBox/launch the browser only if an update is available -
  never touching Excel COM from the background thread itself, per this
  codebase's threading rules. Background-thread failures are caught and
  logged rather than surfaced as an error dialog during workbook open
  (GetTxtRecord already swallows its own errors and returns a descriptive
  string; anything else reaching this path is an unexpected failure not
  worth interrupting the user's workbook-open experience for).

Alternatives Considered
  1. Application.CalculateFull() / a workbook-wide recalculation on open -
     rejected: recalculates every formula in the workbook, including
     RTZParams/RTZFunctions, reintroducing the exact disruptive-on-open
     behavior this session's DEBT-0004 work specifically avoided.
  2. Check for updates on every qualifying workbook open, not gated to
     once per session - rejected per user direction: would mean repeated
     prompts if several RadToolz workbooks are opened in one session.
  3. Keep the check synchronous, accept the blocking risk - rejected per
     user direction, and per this codebase's own threading rules (UDFs/
     macro-context work must not block the main thread on I/O); forcing
     this to run on every open makes the existing DEBT-0007 concern a
     certainty rather than an occasional risk.
  4. Extract a shared helper instead of duplicating the version-check
     logic - this DDR's first draft chose this, citing "Rule of Three"
     with only two call sites, which was wrong per section_09's own
     decision table (duplicate-with-debt-entry is correct for a second
     occurrence; extraction is for a third). Corrected before
     implementation: duplicate instead, record the duplication as debt.

Consequences
  Positive: closes the actual UX gap (update checks now reliably surface
    on open, not just on incidental recalculation) without the async-vs-
    blocking or workbook-wide-recalc regressions the naive approaches
    would have caused. First reusable event-hook infrastructure in the
    codebase, in case future work needs it.
  Negative / accepted trade-offs: adds a new file/class and the codebase's
    first genuine multithreaded (Task-based) code path - more surface
    area than any single-function fix in this session so far. The
    once-per-session gate means a user who declines an update on the
    first qualifying workbook open won't be re-prompted again that
    session even in a different workbook - intentional, per user
    direction.
  Regression implications: Medium-High (per regression_risk_matrix -
    COM lifetime, threading, initialization order all touched at once).
    RTZUpdate()'s own behavior when called as a formula is unchanged
    (same synchronous call, same messages, only the destination URL
    already changed in the prior small commit). Requires characterization
    testing in real Excel before merging with confidence, not just a
    clean build.
  Compatibility implications (UDF surface, persisted formats, .dna): No
    change to RTZUpdate's signature or exported behavior. New file must
    be added to RadToolz.vbproj's Compile list and the .dna's export
    surface reviewed for whether IExcelAddIn needs explicit registration
    (Excel-DNA auto-discovers IExcelAddIn implementations in the same
    assembly - no .dna change expected, verified at implementation time).

Superseded in part by DDR-0016
  Testing this design surfaced that Calculate() does not force a
  non-volatile UDF to genuinely re-evaluate (Excel's own dirty-tracking
  skips it), which meant RTZUpdate()'s own cell display never refreshed
  on open the way this DDR assumed it would once wired up. DDR-0016
  covers the fix (RTZUpdate converted to an Excel-DNA async function;
  on-open refresh via formula reassignment instead of Calculate();
  RTZUpdate is NOT volatile - that combination is a documented
  recalculation-loop risk with async functions). This DDR's own
  "RTZUpdate()'s own behavior when called as a formula is unchanged"
  claim under Regression implications above no longer holds - see
  DDR-0016 for what actually changed and why.

Verification
  Confirmed working end-to-end in real Excel by the user, across several
  rounds of fixes covered in DDR-0016: on-open detection, the overwrite/
  close-workbook dialog, the cell-refresh-without-dialog path, and
  (final round) the redundant-second-dialog suppression. User's own
  words on the final state: "That worked EXACTLY as I wanted it to."
