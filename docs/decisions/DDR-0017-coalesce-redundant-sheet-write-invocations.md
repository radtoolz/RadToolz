DDR-0017: Coalesce redundant same-range re-invocations of RTZParams/RTZFunctions
Status: Accepted
Date: 2026-07-20
Task: Fix duplicate overwrite-confirmation popups on workbook open (Large)

Context
  RTZParams (via ListAll) and RTZFunctions each write their full output
  table to a worksheet range and, per DDR-0014, prompt Yes/No before
  overwriting a non-empty target range. Reported behavior: opening a
  workbook whose RTZParams output range already holds data from a prior
  run popped the confirmation dialog 2-3 times in a row, not once.

  Root cause is not the guard itself (it correctly re-prompts whenever
  the target range is non-empty) but that Excel invokes RTZParams more
  than once for the identical target range within one open event.
  Confirmed via Excel-DNA's own issue tracker
  (github.com/Excel-DNA/ExcelDna/issues/86): AllowReference:=True on the
  uRng parameter - required so the function receives a genuine cell
  reference rather than its resolved value - makes Excel treat the whole
  function as effectively volatile, reevaluating it on virtually any
  workbook/worksheet change, independent of anything the function itself
  does. RTZFunctions shares the identical guard-then-write structure and
  the same AllowReference parameter, so it is exposed to the same
  behavior even though it was not the function originally reported; user
  direction was to fix both in this same change.

  Diagnostic instrumentation (temporary LogDisplay logging, removed
  before this DDR's final state) confirmed the target workbook has only
  one genuine RTZParams formula cell - the duplicate invocations
  originate from Excel's own dispatch, not a workbook authoring issue.
  A first mitigation attempt (an in-progress marker checked after
  resolving the target Range/Worksheet objects) still lost the race: the
  COM calls needed to resolve those objects gave a second
  near-simultaneous Excel-dispatched call enough of a window to also
  pass the check before the first call reached its own marker-set.
  Moving the check to the earliest point each function can identify its
  own target - the raw uRngVal string in ListAll (no COM calls needed to
  read it); the resolved uCellAddr in RTZFunctions (the earliest value it
  has after the C API calls it must make to identify its target) -
  closed the user-visible symptom: confirmed by the user in real Excel
  that only one dialog now appears, answered with a single click, on the
  scenario that previously produced 2-3. A residual oddity - two
  "invoked" log entries still appeared for what visibly resolved as one
  dialog - was not fully root-caused (plausibly Excel still dispatches
  twice but the second dispatch does not reach a second visible MsgBox
  for reasons not confirmed), but produces no observable problem, so
  further investigation was not pursued past this point per explicit
  user direction favoring the resolved symptom over exhaustive
  mechanism certainty.

Decision
  Two layered mechanisms, in both ListAll (RTZParams) and RTZFunctions:

  1. An in-progress marker (Shared/module-level HashSet(Of String)),
     checked and set as the very first thing each function does with its
     target-identifying string (uRngVal for ListAll; uCellAddr for
     RTZFunctions, right after it is resolved) - before any Range/
     Worksheet COM resolution, before the CountA guard, before the
     write. A call finding its own key already present treats the range
     as already being handled and returns the normal success value
     without re-prompting or re-writing. Cleared on every exit path
     (decline, success, and the shared HandleErrors label - safe as a
     no-op if the error occurred before the key was ever added, since
     HashSet(Of String).Remove on a reference-type key that was never
     added, or is Nothing, is a safe no-op) without altering the
     existing legacy On Error control flow itself (section_29_stop_and_ask
     on modifying it).

  2. The originally-designed completed-write debounce (rangeKey + UTC
     timestamp): if invoked again for the exact same resolved target
     range within 2 seconds of a completed write, skip the CountA check,
     the prompt, and the write, and return the same success value the
     normal write path would. This catches a genuinely-later re-trigger
     (e.g. the write itself counting as one of the "any change" triggers
     noted above) that completes after the in-progress marker for that
     call has already been cleared - a case the marker alone does not
     cover.

  In both cases, the tracking state is only updated on a genuinely
  successful write (guard-passed-or-empty), never after a decline, so a
  real decline is never masked on a subsequent real retry.

  Per section_09 (second occurrence -> duplicate with a debt entry, not
  extract), both mechanisms are duplicated between RadToolzFunctions.vb
  (RTZFunctions) and ProcessDecaySeries.vb (ListAll) rather than factored
  into a shared helper across the two classes - recorded as new debt,
  matching the DEBT-0015b precedent.

Alternatives Considered
  1. Remove AllowReference:=True - rejected: the function's design
     requires receiving uRng as an unresolved ExcelReference (to compute
     its address via xlfReftext), which AllowReference exists to enable;
     removing it would break the function's actual purpose, not just its
     recalculation behavior.
  2. Remove the `uRng = uRng` self-registration trick noted in the
     existing code comment - considered and dropped once AllowReference
     itself was confirmed as the actual volatility source; that trick's
     real effect remains unverified and unrelated to this fix.
  3. Session-long "already prompted" suppression flag - rejected: would
     also suppress genuine subsequent recalcs (F9, workbook reopen,
     upstream change) that DDR-0014 explicitly wants to keep prompting
     on.
  4. Hook workbook-open (mirroring RadToolzAddIn's arm/consume
     suppression flag from DDR-0016) - rejected as unnecessarily wider
     blast radius: touches AutoOpen/AutoClose (an additional hard
     trigger) for a fix achievable entirely within the two write sites.
  5. An in-progress marker keyed on the fully-resolved target range
     (attempted first, before the uRngVal/uCellAddr-keyed version above)
     - reverted: lost the race against a second near-simultaneous
     Excel-dispatched call because too many COM calls sat between
     function entry and the marker being set.

Consequences
  Positive: closes the user-visible duplicate-popup bug (confirmed in
    real Excel: one dialog, one click, where 2-3 appeared before)
    without touching AutoOpen, threading, or the write logic itself.
    Covers both known-affected write sites (RTZParams and RTZFunctions).
  Negative / accepted trade-offs: a genuine, intentional re-run of
    RTZParams or RTZFunctions at the same anchor within 2 seconds of the
    prior run would be silently treated as already-done rather than
    re-prompted/rewritten - judged extremely unlikely for macro-type
    writes of this size that a user runs deliberately. Both mechanisms'
    state are static/in-process (not persisted), so neither survives
    add-in reload, which is correct. The exact mechanism behind why
    Excel's second dispatch (still observed in diagnostic logging) no
    longer produces a second visible dialog was not fully isolated -
    accepted per explicit user direction once the observable symptom was
    confirmed resolved. Both mechanisms are duplicated across two files
    (recorded as debt, not extracted, per section_09).
  Regression implications: Medium (per regression_risk_matrix - UDF
    semantics, no signature change; behavior only changes for the
    rapid-repeat-invocation case this bug describes).
  Compatibility implications (UDF surface, persisted formats, .dna): No
    argument list or name changes. Additive/safety-only semantics change.

Verification
  Confirmed by the user in real Excel, using the actual regression
  workbook that originally reproduced the bug: reopening a workbook with
  a pre-populated RTZParams output range now shows exactly one
  confirmation dialog, resolved with a single click, where the
  unpatched code showed 2-3. RTZFunctions received the identical fix by
  code inspection and symmetry with ListAll's confirmed behavior, but
  was not independently re-verified live per explicit user direction to
  finalize rather than run a further confirmation round.
