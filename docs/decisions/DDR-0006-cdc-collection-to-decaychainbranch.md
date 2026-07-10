DDR-0006: Replace Collection branch representation with a DecayChainBranch (List(Of DecaySeriesItem)) wrapper
Status: Accepted
Date: 2026-07-10
Task: Option Strict On epic, Milestone 1 (Large) - closes DEBT-0001's
      remaining scope; see DDR-0005

Context
  gdcdci()/cDC() throughout ProcessDecaySeries.vb and RadToolzFunctions.vb
  is a fixed-size array of Microsoft.VisualBasic.Collection, one slot per
  decay-chain branch. Collection.Item(i) returns Object, so every
  .Item(i).PropertyName access downstream (55 of the 73 Option-Strict-On
  errors found in DDR-0005's scoping build) is late binding. DEBT-0001
  already proposed replacing this with List(Of DecaySeriesItem); DDR-0002
  deferred it as out of scope for the prune-as-you-go fix, noting it
  "touches BubbleSortCollection's key-based insert logic and every
  caller's Item(x) access pattern."

  Discovery for this DDR found the caller-facing blast radius is smaller
  than that note implied: every .Item(i)/.Count/.Remove(i) call site
  outside BubbleSortCollection uses plain integer indexing (never a
  string-keyed lookup), so a 1-based wrapper preserving Collection's
  existing Item/Count/Remove call syntax needs zero changes to
  RadToolzFunctions.vb's 8 UDFs beyond 6 "Dim cDC(...) As Collection"
  declarations - every existing DirectCast(cDC(x).Item(y).Prop, T)
  expression keeps compiling (the DirectCast becomes redundant-but-legal
  once .Item(y) returns DecaySeriesItem instead of Object; VB does not
  error or warn on an identity DirectCast).

Decision
  Introduce RadToolz/DecayChainBranch.vb: a small class wrapping
  List(Of DecaySeriesItem), with a 1-based Default Property Item(index)
  As DecaySeriesItem (get/set), ReadOnly Property Count, Sub
  Add(item As DecaySeriesItem), and Sub Remove(index) (1-based) -
  matching Collection's existing call-site surface exactly except that
  Item now returns DecaySeriesItem instead of Object. Replace every
  gdcdci()/cDC() As Collection declaration (ProcessDecaySeries.vb:
  ClearBranches, GetDecayChain, InitBranches, VerifyDecayChain,
  AddDecayChainItem, BubbleSortCollection; RadToolzFunctions.vb: 6
  UDF-local Dim cDC(...) declarations) with As DecayChainBranch.

  Three call sites need real logic changes beyond the type swap:
  1. GetDecayChain's sParent = "ALL" special case (gdcdci(0) =
     DecaySeriesRepository.GetAll()) - GetAll() returns Collection;
     rewritten to build a DecayChainBranch from
     DecaySeriesRepository.GetAllList() (the existing, already-preferred,
     non-late-bound accessor) instead, so DecaySeriesRepository.vb itself
     needs no change.
  2. AddDecayChainItem's fromDCI As Object and LoadDecaySeriesItem's
     fromDSI As Object both tighten to DecaySeriesItem - every actual
     argument at both call sites is already statically a DecaySeriesItem
     once (1) lands, so LoadDecaySeriesItem's 16
     DirectCast(fromDSI.X, T) lines become plain toDSI.X = fromDSI.X
     assignments. The On Error GoTo HandleError wrapper and its MsgBox
     recovery block are untouched - only the protected assignment
     statements change.
  3. BubbleSortCollection's swap mechanic: Collection has no Item(i) =
     value setter, so the existing code works around that by removing the
     out-of-order item and re-Adding it positioned via a string-keyed
     before:= lookup (copied through an Object-typed intermediate via
     LoadDecaySeriesItem). DecayChainBranch's Item(index) supports a
     direct setter, so the swap becomes a plain 3-line temp-swap (Item(i),
     Item(i+1) exchanged in place) - O(1) instead of the current O(n)
     remove-and-reinsert, and removes the LoadDecaySeriesItem call and its
     now-dead HandleErrors: path from this method entirely
     (BubbleSortCollection has no On Error GoTo at its top - HandleErrors:
     here is a manual GoTo target reached only from the code path this
     change removes, so removing it deletes genuinely unreachable code
     introduced by this same change, not preserved legacy code).

Alternatives Considered
  1. Wrapper preserves Collection's full Add(item, key)/Add(item, key,
     beforeKey) API instead of simplifying BubbleSortCollection's swap -
     rejected: every remaining caller of the keyed Add overloads only used
     the key for Collection's built-in duplicate-key exception (already
     superseded by DDR-0001's HashSet dedup) or for the before:=
     positional insert BubbleSortCollection's own swap no longer needs
     once Item(index) supports a setter. Replicating key-based
     lookup-and-insert on top of a List would be more code and more
     surface for bugs than the direct index swap it would exist only to
     serve.
  2. Change DecaySeriesRepository.GetAll()'s return type from Collection
     to DecayChainBranch directly, instead of rebuilding from
     GetAllList() at the one call site - rejected: GetAll() is also called
     by ProcessDecaySeries.GetDecaySeries() (a Public method with no
     caller found anywhere in this solution during discovery - flagged as
     a possible separate dead-code item, not touched here as it is out of
     this task's scope), so changing its signature has a blast radius
     outside what this milestone needs. Both left untouched.
  3. Convert everything to 0-based indexing to match List(Of T)'s native
     convention - rejected: every one of the ~30 existing call sites uses
     1-based arithmetic throughout both files; forcing a 0-based rewrite
     at every call site is a much larger, more error-prone diff for zero
     behavioral benefit over a 1-based wrapper property.

Consequences
  Positive: resolves 53 of the 73 Option-Strict-On errors found during
    DDR-0005's scoping (29 in RadToolzFunctions.vb via zero body changes
    beyond 6 declarations, plus retyping RadDecay's i/j/n loop counters
    from Double to Integer - forced by the new typed Item(index As
    Integer) indexer, see Implementation Notes; 24 of 26 in
    ProcessDecaySeries.vb). The other 2 RadToolzFunctions.vb errors
    originally lumped into the pre-implementation estimate turned out to
    be RTZFunctions' iExcel.Range/.Worksheets calls - the same COM
    Application-access root cause as ProcessDecaySeries.ListAll's 2
    errors, not cDC-related; folded into Milestone 4's scope instead (now
    4 error sites across both files, not 2 - DDR-0005 updated). Closes
    DEBT-0001. BubbleSortCollection's swap becomes O(1) instead of O(n)
    per swap, an incidental improvement to the same cost driver DDR-0002
    already identified as dominant for reconverging chains - not the goal
    of this change, but worth measuring alongside the characterization
    pass.
  Negative / accepted trade-offs: touches BubbleSortCollection's internal
    swap mechanic more than a pure type-rename would - judged safer than
    preserving a key-based Remove/Add dance solely to minimize this diff
    (see Alternative 1).
  Regression implications: High per section_21_regression_risk_matrix
    (shared engine, 8 downstream UDFs: AValue, DCF, EnumDecayChain, FGE,
    HalfLife, PECi, RadDecay, SpA). Mitigated by: characterization tests
    before any change (reuse/extend DDR-0002's 5-case set - single-branch
    Cs-137, 3-way-fork Bi-214, U-238->Po-210, U-238->"END",
    U-238->unreachable terminal - diffed field-by-field, plus a new
    sort-order-sensitive case exercising BubbleSortCollection's
    OptionalSortOrder=2/3 paths specifically, since that method's
    internals change the most); full output diff before/after; build
    verification both bitness targets, zero new warnings.
  Compatibility implications: none - DecayChainBranch, ClearBranches,
    GetDecayChain, InitBranches, VerifyDecayChain, AddDecayChainItem,
    BubbleSortCollection, and LoadDecaySeriesItem are all internal
    (non-UDF, non-COM-visible); no .dna change; no exported UDF's name,
    argument list, or return semantics changes.

Implementation Notes (added post-implementation)
  Two consequences surfaced only once the compiler actually ran that the
  Decision section did not anticipate:
  1. VB.NET's DirectCast forbids a same-type floating-point identity cast
     (BC36760: "Using DirectCast operator to cast a floating-point value
     to the same type is not supported") - unlike a reference-type
     identity DirectCast (e.g. the String casts at
     RadToolzFunctions.vb:481 and :954, left untouched, which compile
     fine), this is a hard error, not a harmless redundancy. Every
     DirectCast(cDC(x).Item(y).SomeDoubleProperty, Double) in
     RadToolzFunctions.vb (27 sites) had the DirectCast(..., Double)
     wrapper removed, leaving the now-directly-typed property access bare.
     Same fix applied to LoadDecaySeriesItem's 16 DirectCast(fromDSI.X,
     Double) lines, as already anticipated in the Decision section.
  2. DecayChainBranch.Item's index parameter is Integer (a deliberate,
     correctly-typed index), but RadDecay's loop counters i/j/n were
     declared As Double (legacy loose typing) - fine against
     Collection.Item(Index As Object), which just boxes a Double with no
     narrowing, but DecayChainBranch.Item(index As Integer) narrows it,
     producing new BC42016 warnings (10 sites) that would have violated
     the zero-new-warnings build gate. i/j/n are internal locals used only
     as loop counters and Item indices (always whole-number values 1..n
     where n=Count), so retyping them Dim i/j/n As Integer is
     behavior-neutral and was applied. EnumDecayChain's Member (an
     exported UDF parameter, Member As Double - part of the public
     surface, out of scope to retype) needed an explicit CInt(Member) at
     its one Item(...) call site instead, preserving the parameter's
     existing type and marshaling.
  Verified results: build clean, 0 errors, 0 warnings, both bitness
  targets packed. Characterization harness (6 cases, 7 runs including the
  two sort-order variants) - 0 diffs, byte-identical output before/after
  across all fields for every branch slot, not just cDC(0). Re-running the
  /p:OptionStrict=On scoping build: 73 -> 20 errors (53 resolved, matching
  the corrected estimate above exactly once the iExcel miscount was
  isolated).
