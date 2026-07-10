# Technical Debt Register

Per CLAUDE.md (`section_26_technical_debt_handling`): debt is recorded here,
never fixed opportunistically inside an unrelated diff. Each entry becomes
its own classified task with its own approval before being addressed.

---

## DEBT-0001: `GetDecayChain`'s `Collection`/late-binding branch representation is the remaining cost for branching decay chains

- **Location:** `RadToolz/ProcessDecaySeries.vb`, `GetDecayChain` and `BubbleSortCollection` — the `Microsoft.VisualBasic.Collection`-based branch representation (`gdcdci()`) and its late-bound `Item`/`Add` access.
- **Status:** CLOSED 2026-07-10 by DDR-0006 (Option Strict On epic, Milestone 1) — `Collection` replaced throughout with `DecayChainBranch` (`List(Of DecaySeriesItem)`-backed, 1-based `Item`/`Add`/`Remove`/`Count`, matching the existing call-site convention). Verified via a 7-run characterization pass (0 diffs) and a clean Release build (0 errors, 0 warnings, both bitness targets). Also incidentally made `BubbleSortCollection`'s swap O(1) instead of O(n) (a direct `Item(i)` setter exchange, replacing the old remove-and-reinsert-by-key dance) — not the goal of DDR-0006, but a side effect of the same change.
- Prior partial progress (retained for history): DDR-0002 (2026-07-07) added reachability pruning (`CanReachTerminal`) before DDR-0006 addressed the `Collection` representation itself — see that DDR for the standalone pruning measurements.
- **Risk:** N/A — closed.
- **Date recorded:** 2026-07-06
- **Date closed:** 2026-07-10
- **Related:** DDR-0001 (exception-dedup fix), DDR-0002 (prune-as-you-go), DDR-0005 (epic), DDR-0006 (this closure).
