DDR-0002: Prune unreachable branches before copy in GetDecayChain
Status: Accepted
Date: 2026-07-07
Task: GetDecayChain fork-copy pruning (Large) — DEBT-0001

Context
  GetDecayChain builds every branch of the decay tree in full — copying
  the entire accumulated prefix chain item-by-item into a new Collection
  at every fork point, then recursing to the branch's natural end — before
  the post-hoc VerifyDecayChain step removes any branch whose last isotope
  isn't sTerminal. For a caller that wants one specific terminal isotope
  partway down a heavily-forked chain (RadDecay's sTerminal parameter;
  U-238-class chains fork ~70 times), most of that built work is thrown
  away. Measured (DEBT-0001): ~31.8 ms/call for U-238 -> Po-210 versus
  ~90 us/call for a single-branch isotope, with zero exceptions involved
  on either side (DDR-0001 already removed the exception-driven dedup
  cost). Discovery for this DDR narrowed the affected surface: of the 8
  UDFs routing through GetDecayChain, only EnumDecayChain (sTerminal=
  "END") and RadDecay (sTerminal=a specific isotope) ever call it with
  sParent <> sTerminal at the top level, so only these two exercise the
  fork/recursion path at all. AValue, DCF, HalfLife, SpA, PECi, and FGE
  all call it with sParent = sTerminal and never enter the fork block or
  recurse past the first item.

Decision
  Before committing to a fork's prefix copy (and before recursing down the
  primary occurrence's daughter), check whether that specific occurrence's
  subtree can ever reach sTerminal, using a new memoized, isotope-name-only
  reachability helper (no DecaySeriesItem copies, no Collection use). If it
  cannot, skip the copy and the recursive call entirely — the branch is
  never built instead of being built and then discarded. Output is
  unchanged: the same branches that survive VerifyDecayChain today are the
  only ones ever built under this change.

  Reachability is defined as: sTerminal = "END" is always reachable
  (matches VerifyDecayChain's existing "sTerminal = END always passes"
  rule, so EnumDecayChain's behavior and performance are both untouched —
  it already wants every branch built). Otherwise isotope = sTerminal is
  reachable; otherwise isotope is reachable iff any of its own occurrences'
  daughters are reachable (recursive OR, memoized per isotope name for the
  lifetime of one top-level GetDecayChain call, since sTerminal is fixed
  for that whole call tree). A per-call "currently visiting" guard prevents
  runaway recursion if the table ever contained a cycle — decay chains are
  acyclic today, but nothing upstream currently enforces that as an
  invariant, so this is a defensive addition rather than a behavior change.

Alternatives Considered
  1. Leave as-is — rejected: this is the dominant remaining cost per
     DEBT-0001, and RadDecay/EnumDecayChain are the two UDFs that pay it.
  2. Replace Microsoft.VisualBasic.Collection with List(Of
     DecaySeriesItem) throughout GetDecayChain (removes late-bound
     Item/Add overhead in addition to pruning) — deferred: larger blast
     radius, touches BubbleSortCollection's key-based insert logic and
     every caller's Item(x) access pattern. User elected prune-as-you-go
     only for this task; List(Of) conversion remains open as separate
     future work (docs/debt.md, DEBT-0001).
  3. Precompute a full isotope reachability graph once per DecaySeriesRepository
     load (cached alongside _index) instead of per-call memoization —
     rejected: reachability depends on sTerminal, which varies per call
     (and per-cell recalculation, potentially for a different isotope each
     time), so a single precomputed graph keyed only by isotope pairs
     would need to cover the full isotope x isotope matrix (~5,500^2) to
     be reusable across calls, which is unnecessary work when the vast
     majority of calls need reachability for only a handful of isotopes.

Consequences
  Positive: eliminates the copy-then-discard cost for every branch that
    cannot reach sTerminal. Measured standalone (ProcessDecaySeries.
    GetDecayChain called directly, same methodology as DDR-0001):
    - U-238 -> an unreachable terminal: ~31ms -> ~0.08ms (~370x) - the
      entire fork tree is skipped since nothing in it reaches the target.
    - U-238 -> Po-210 (RadDecay's realistic usage - a genuine mid-chain
      terminal): ~41ms -> ~36ms (modest). Most forks in this particular
      series reconverge back onto the Po-210 path through their own
      daughter chain, so CanReachTerminal correctly reports them
      reachable and they still get built - pruning only removes
      provably-wasted work, it does not change how much of the tree is
      legitimately needed for a terminal most branches converge on.
    - U-238 -> "END" (EnumDecayChain's usage): unchanged (~31ms both
      before and after), as designed - every branch is wanted, so nothing
      is pruned.
    Also reduces peak branch-slot usage against maxBranches (Constants.vb,
    = 150) for cases that do prune, since discarded branches never
    consume a slot in the first place.
  Negative / accepted trade-offs: adds a new recursive helper and a
    threaded-through Optional ByRef cache parameter to GetDecayChain's
    signature (mirrors the existing pds threading pattern) — slightly more
    surface in an already-recursive method. The List(Of)/Collection
    late-binding cost (DEBT-0001's alternative 2) remains untouched and is
    now the dominant cost for reconverging chains like the Po-210 case
    above — re-recorded as the narrowed remaining scope of DEBT-0001.
  Regression implications: High per section_21_regression_risk_matrix
    (shared engine, 8 call sites across the public UDF surface) —
    mitigated by characterization tests: 5 representative cases
    (single-branch isotope Cs-137; 3-way-fork isotope Bi-214; U-238 ->
    Po-210, a mid-chain terminal; U-238 -> "END", full enumerate; U-238 ->
    an unreachable terminal), full output diffed field-by-field
    before/after (isotope, daughter, lambda, branching ratio, order,
    count). 0 diffs across all 5.
  Compatibility implications: none — GetDecayChain is an internal helper,
    not an exported UDF; no .dna change; the new parameter is Optional
    with a default that preserves every existing call site unchanged.
