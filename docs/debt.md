# Technical Debt Register

Per CLAUDE.md (`section_26_technical_debt_handling`): debt is recorded here,
never fixed opportunistically inside an unrelated diff. Each entry becomes
its own classified task with its own approval before being addressed.

---

## DEBT-0001: `GetDecayChain` fork-copy-then-prune tree build is the dominant remaining cost for branching decay chains

- **Location:** `RadToolz/ProcessDecaySeries.vb`, `GetDecayChain` — the recursive branch-fork logic (the `For altIndex = 1 To parentIndices.Count - 1` block and its nested copy loop, roughly lines 96-125), which runs *before* the pruning/dedup step.
- **Description:** For every branch point (a parent isotope occurring more than once in the decay table, e.g. Bi-214's 3-way fork), `GetDecayChain` copies the *entire accumulated prefix chain* item-by-item into a new `Collection` before recursing down the alternate path. It always builds every branch of the full tree first and only prunes non-matching branches afterward — this build cost is paid even when the caller only wants a single terminal isotope. Measured directly (`ProcessDecaySeries.GetDecayChain` called standalone for U-238 → Po-210, a specific pruned terminal): ~31.8 ms/call after DDR-0001's exception-dedup fix, versus ~90 us/call for a single-branch isotope (Cs-137) — a ~350x gap with zero exceptions involved on either side. This is now the single largest cost in any call against a heavily-forked starting isotope (U-238, Th-232, etc.), for every UDF that routes through it: `AValue`, `DCF`, `EnumDecayChain`, `FGE`, `HalfLife`, `PECi`, `RadDecay`, `SpA`.
- **Risk:** Performance only, not correctness — no incorrect output observed or suspected. Risk is user-facing recalculation lag: a worksheet column dragging `EnumDecayChain`/`RadDecay` down many rows against a forked parent costs tens of milliseconds per cell, noticeable on F9 at moderate row counts. No data-corruption or crash potential, so this is a normal debt entry, not a Stop-and-Ask item.
- **Suggested remedy:** Prune-as-you-go instead of build-then-prune (stop recursing down a branch as soon as it's provably unable to reach `sTerminal`), and/or replace the `Microsoft.VisualBasic.Collection`-based branch representation with `List(Of DecaySeriesItem)` to remove late-bound `Item`/`Add` overhead. Larger blast radius than DDR-0001's fix — touches the shared tree-building mechanism and (for the `List` option) `BubbleSortCollection`'s key-based insert logic — needs its own DDR, its own plan, and its own characterization-test pass per `section_21_regression_risk_matrix` (High: shared engine, ~8 downstream call sites).
- **Date recorded:** 2026-07-06
- **Related:** DDR-0001 (`docs/decisions/DDR-0001-...` if filed) explicitly deferred this rather than bundling it into the exception-dedup fix, per the smallest-safe-change rule.
