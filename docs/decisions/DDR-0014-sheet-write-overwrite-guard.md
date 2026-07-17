DDR-0014: Guard RTZFunctions/RTZParams/ListAll against overwriting populated ranges
Status: Accepted
Date: 2026-07-17
Task: DEBT-0004 (Large - changes exported UDF semantics; irreversible-write risk)

Context
  RTZFunctions (~18 rows x 2 cols) and RTZParams/ListAll (1 + ~5,477 rows x
  17 cols, ~93,000 cells) write directly to worksheet cells via the COM
  object model, starting at a user-supplied anchor cell. These are
  IsMacroType:=True functions - a documented, deliberate exception to the
  normal "UDFs must be side-effect free" rule, required because Excel only
  permits direct Range writes from macro-context calls. The writes are not
  undoable with Ctrl+Z (COM writes bypass Excel's undo stack).

  The only existing guard (StrComp(cCellAddr, uCellAddr) = 0 in both
  RTZFunctions and RTZParams) rejects a call only when the anchor is
  exactly the calling cell. If the calling cell lies anywhere else inside
  the computed output rectangle, the guard does not catch it: the write
  proceeds and can overwrite the caller's own formula mid-loop, plus
  silently overwrite any other pre-existing data anywhere in the
  rectangle, populated or not.

  Per user direction: the fix should not just close the narrow
  self-overwrite case, but guard against overwriting any non-empty target
  range, with a confirmation dialog (not silent refusal) when the range
  already has content - since re-running these functions at the same
  anchor to refresh their own prior output is a plausible, legitimate use
  case and should not be permanently blocked.

Decision
  Add a single check per write site: compute the output rectangle's exact
  extent (already-known row/column counts), read whether any cell in that
  rectangle is non-empty via a single Application.WorksheetFunction.CountA
  call (one COM round-trip, not a per-cell loop, per this codebase's
  bulk-access convention), and if so, show a MsgBox (Yes/No) asking the
  user to confirm the overwrite before proceeding - matching the existing
  confirm-before-action pattern already used by RTZLicense. Declining
  returns the same failure convention each function already uses
  (ExcelError.ExcelErrorValue for RTZFunctions/RTZParams's existing
  self-overwrite path; False from ListAll, which RTZParams already
  surfaces as "Failed").

  This single check subsumes the original narrow self-overwrite bug as a
  special case: the calling cell, if inside the rectangle, always
  contains the formula that produced the call and is therefore never
  "empty" - CountA catches it without a separate intersection test. The
  existing narrow guards are left in place unchanged (redundant now, but
  harmless, and removing working code isn't necessary to fix this).

  ListAll is the sole caller-facing site for the isotope-table write (its
  only caller is RTZParams); the new check is added there, where the
  actual row count (1 header + DecaySeriesRepository.GetAllList().Count)
  is already available. RTZFunctions' row count (18) is a small, static,
  hand-maintained list with a comment noting it must stay in sync with
  the entries written below it - not restructured into a data-driven loop,
  since that goes beyond this fix's stated scope.

Alternatives Considered
  1. Silent refusal instead of a confirmation dialog - rejected per user
     direction: would permanently block the legitimate refresh-in-place
     use case with no way to proceed short of manually clearing the range
     first.
  2. Guard only the original self-overwrite case (minimal fix) - rejected
     per user direction: leaves pre-existing unrelated data in the rest of
     the rectangle unprotected, which is the larger data-loss risk the
     debt entry actually flagged.
  3. Restructure RTZFunctions' write into a data-driven array + loop to
     eliminate the hand-maintained row-count constant entirely - deferred:
     correct and removes a small drift risk, but is a larger diff than
     this fix requires and changes code shape beyond the guard itself;
     left as a note rather than bundled in.

Consequences
  Positive: closes the actual data-loss risk DEBT-0004 flagged, not just
    the narrow self-overwrite case; consistent behavior (confirm-before-
    overwrite) across all three write sites; single bulk CountA check per
    site, no per-cell COM loop added.
  Negative / accepted trade-offs: once a target range has been written to
    once, every subsequent recalculation of that same formula (F9,
    Ctrl+Alt+F9, workbook reopen, or the input cell's reference changing)
    will pop the confirmation dialog again, since the range now always
    contains the prior output. This is the direct, accepted consequence of
    choosing a confirmation dialog over one-time/silent handling - a
    workbook that recalculates one of these formulas automatically and
    frequently will see the dialog often. Also: RTZFunctions' row count
    (18) stays a hand-maintained constant; if a function entry is ever
    added or removed from the hardcoded list without updating it, the
    guard would check the wrong extent (under- or over-checking one row).
    Recorded as debt if the drift risk becomes real (see debt.md).
  Regression implications: Medium (per regression_risk_matrix - public UDF
    surface, though semantics-only, no signature change). Existing
    behavior for already-empty target ranges is unchanged (no dialog
    fires). Existing narrow self-overwrite guards are untouched. Every
    previously-successful call where the target happened to already
    contain data will now prompt instead of silently overwriting -
    intentional per this DDR, but is a visible behavior change worth
    flagging to users of existing workbooks that rely on silent refresh.
  Compatibility implications (UDF surface, persisted formats, .dna): No
    argument list or name changes. Semantics change (a new confirmation
    prompt can appear where none did before) - additive/safety-only, not
    breaking per api_and_backward_compatibility (no workbook that worked
    before stops working; some now get an extra prompt).
