DDR-0018: Explicitly clear the auto-applied volatile flag on RTZParams/RTZFunctions
Status: Accepted
Date: 2026-07-20
Task: Stop RTZParams/RTZFunctions recalculating automatically on open (Large)

Context
  RTZParams and RTZFunctions are IsMacroType:=True with AllowReference:=True
  on their uRng parameter - required so each function receives uRng as a
  genuine, unresolved cell reference (to compute its address via
  xlfReftext) rather than the referenced cell's resolved value. Per
  Excel-DNA's maintainer (govert), confirmed directly in
  github.com/Excel-DNA/ExcelDna/issues/84: "If a function is marked as
  IsMacroType=true and has an argument that is marked AllowReference=true
  then the function is automatically considered volatile by Excel."

  This explains behavior broader than DDR-0017's redundant-invocation fix
  addressed: these functions were recalculating not just multiple times
  per open event, but on every open, and (per the same volatility) on any
  other unrelated workbook change too. Per explicit user direction: this
  was never wanted - only a fresh formula entry or an explicit forced
  recalculation should cause these functions to run.

  Setting IsVolatile:=False on the ExcelFunction attribute has no effect
  for this specific combination (also confirmed by the maintainer in the
  same issue thread - a separate report, buybackoff, tried exactly this
  and it didn't work). The only mechanism that does work is explicitly
  clearing the flag from inside the function via the C API:
  XlCall.Excel(XlCall.xlfVolatile, False).

  The maintainer's own reproduction (same issue thread) demonstrates a
  direct consequence of turning this off: an IsMacroType:=true function
  reading its declared input via the C API (rather than Excel's normal
  dependency-tracked read) does not react even to that input actually
  changing under plain F9 - only Ctrl+Alt+F9 (Full Calculation, which
  reevaluates every cell regardless of dirty/volatile state) forces it.
  This was surfaced to the user directly and confirmed acceptable before
  implementation.

Decision
  Add Excel(xlfVolatile, False) as the first executable statement in both
  RTZParams and RTZFunctions (immediately after On Error GoTo
  HandleErrors), using the same unqualified Excel(...) call style already
  used elsewhere in both functions (e.g. Excel(xlfReftext, ...)), backed
  by the existing Imports ExcelDna.Integration.XlCall at the top of the
  file.

  The existing `uRng = uRng` "dummy operation to calm the Excel
  dependency tree down" line in both functions is left untouched. Its
  comment already flagged it as unverified against current Excel-DNA
  internals; this DDR's finding (AllowReference itself is the actual
  volatility source, not this self-assignment) makes it very likely
  inert, but removing dead-but-harmless code is a separate cleanup, not
  in scope for this fix, per the diff minimization policy.

Alternatives Considered
  1. Remove IsMacroType:=True - rejected: required for the direct COM
     sheet-write both functions perform (per DDR-0014's own context);
     Excel does not permit that kind of write from an ordinary UDF.
  2. Remove AllowReference:=True - rejected: required so uRng arrives as
     an unresolved reference; both functions need to compute its address
     via xlfReftext, not read its value.
  3. Application.Volatile(False) (COM) instead of the C API call -
     rejected: the maintainer's own testing found the C API call
     (XlCall.Excel(XlCall.xlfVolatile, False)) works reliably where the
     COM approach did not, for this exact IsMacroType+AllowReference
     combination.

Consequences
  Positive: RTZParams/RTZFunctions no longer recalculate automatically on
    workbook open or on unrelated workbook edits - matches the explicitly
    stated requirement. DDR-0017's redundant-invocation coalescing stays
    in place underneath as defense-in-depth for the remaining legitimate
    triggers (fresh entry, Ctrl+Alt+F9).
  Negative / accepted trade-offs: plain F9 (Calculate Now) no longer
    re-triggers either function, even if its declared input cell changes -
    only Ctrl+Alt+F9 (Full Calculation) or re-entering the formula does.
    Explicitly confirmed acceptable by the user before implementation.
  Regression implications: Medium-High (per regression_risk_matrix - UDF
    recalculation semantics change, no signature change). Any existing
    workbook or workflow relying on plain F9 refreshing these two
    functions' output will need Ctrl+Alt+F9 instead going forward - a
    real, deliberate behavior change, not a bug fix in the narrow sense.
  Compatibility implications (UDF surface, persisted formats, .dna): No
    argument list or name changes. Recalculation-timing behavior change
    only - additive/intentional per explicit user direction, not a
    regression against DDR-0014/DDR-0017's intent.

Verification
  Build verified clean (0 warnings, both bitness targets pack). Confirmed
  by the user in real Excel on the actual regression workbook. First open
  after the code change still triggered RTZParams once - the workbook's
  persisted formula carried OOXML's ca="1" ("calculate always") attribute
  from before this fix existed, set the last time the file was saved
  while the function was genuinely volatile; that persisted flag is
  independent of the add-in's current runtime behavior and is honored by
  Excel on open regardless of it. After answering that one prompt and
  re-saving the workbook (so Excel re-persists the formula's calculation
  metadata under the now-corrected, non-volatile behavior), a subsequent
  close/reopen confirmed RTZParams stays fully dormant on open, exactly
  as intended. Any other workbook containing a pre-existing RTZParams/
  RTZFunctions formula saved before this fix will need the same one-time
  "answer the prompt, then re-save" step before it stops recalculating on
  open.
