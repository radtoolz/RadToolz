# Technical Debt Register

Per CLAUDE.md (`section_26_technical_debt_handling`): debt is recorded here,
never fixed opportunistically inside an unrelated diff. Each entry becomes
its own classified task with its own approval before being addressed.

---

## DEBT-0001: `GetDecayChain`'s `Collection`/late-binding branch representation is the remaining cost for branching decay chains

- **Location:** `RadToolz/ProcessDecaySeries.vb`, `GetDecayChain` and `BubbleSortCollection` — the `Microsoft.VisualBasic.Collection`-based branch representation (`gdcdci()`) and its late-bound `Item`/`Add` access.
- **Status:** Partially resolved by DDR-0002 (2026-07-07) — `GetDecayChain` now checks reachability (`CanReachTerminal`, memoized, isotope-name-only) before copying a branch's prefix or recursing into it, instead of building every branch and pruning afterward. Measured standalone (same U-238-class case as below): the previously-worst case, a terminal unreachable from the starting isotope, dropped from ~31ms to ~0.08ms (~370x) since no subtree ever gets built. The RadDecay-realistic case (U-238 → Po-210, a mid-chain terminal most branches of this particular series happen to reconverge back into) only improved modestly, ~41ms → ~36ms, because most forks in that specific chain *do* eventually reach Po-210 through their own daughter path and still get built — pruning only skips work that's provably wasted, it doesn't change how much of the tree legitimately needs building for a terminal most branches converge on. 32 characterization checks (5 representative cases × output diff) — 0 diffs.
- **Remaining scope:** Replace the `Collection`-based branch representation with `List(Of DecaySeriesItem)` to remove late-bound `Item`/`Add` overhead — this is the cost that's now dominant for chains where reconvergence limits how much pruning can skip (e.g. the Po-210 case above). Larger blast radius than DDR-0002's fix — touches `BubbleSortCollection`'s key-based insert logic and every caller's `Item(x)` access pattern — needs its own DDR, its own plan, and its own characterization-test pass per `section_21_regression_risk_matrix` (High: shared engine, ~8 downstream call sites, only 2 of which — `EnumDecayChain`, `RadDecay` — actually exercise the branching path).
- **Risk:** Performance only, not correctness — no incorrect output observed or suspected. Risk is user-facing recalculation lag on `RadDecay`/`EnumDecayChain` against heavily-forked parents. No data-corruption or crash potential.
- **Date recorded:** 2026-07-06
- **Related:** DDR-0001 (exception-dedup fix), DDR-0002 (prune-as-you-go) both explicitly deferred the `List(Of)` conversion rather than bundling it in, per the smallest-safe-change rule.
