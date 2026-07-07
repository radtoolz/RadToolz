DDR-0001: Replace exception-driven isotope dedup in GetDecayChain with HashSet check
Status: Accepted
Date: 2026-07-06
Task: GetDecayChain dedup performance fix (Large)

Context
  GetDecayChain populates its deduplicated isotope list (gdcdci(0)) by
  attempting a keyed Collection.Add for every item across every branch,
  relying on VB6 Collection's duplicate-key exception to silently drop
  repeats. Every isotope shared across branch points (common in any
  chain with a fork, e.g. Bi-214's 3-way split) throws. Measured: 895
  real collisions / 1790 first-chance exceptions in a single U-238 call,
  accounting for the large majority of that call's ~71ms cost versus a
  single-branch isotope's ~90us.

Decision
  Replace the throw-and-catch dedup with an explicit HashSet(Of String)
  membership check performed before each Add, preserving exact
  first-occurrence-wins output and the isotope-keyed Collection shape
  BubbleSortCollection depends on.

Alternatives Considered
  1. Leave as-is — rejected: measured cost is the dominant factor in
     every branching-chain call; only single-branch isotopes are
     unaffected.
  2. Replace gdcdci entirely with List(Of DecaySeriesItem) throughout
     GetDecayChain (removes late-bound Collection access too) —
     rejected for this task: much larger blast radius, touches
     BubbleSortCollection's key-based insert logic and every caller's
     Item(x) access pattern; proposed instead as separate future work
     (see docs/debt.md, DEBT-0001).

Consequences
  Positive: removed the single largest measured cost driver for any
    branching decay chain call, with no output change. Measured
    25-57% faster on U-238-class calls; exceptions eliminated
    (1790 -> 0, 554 -> 0 across the two heaviest cases tested).
  Negative / accepted trade-offs: none identified; the fork-and-copy
    recursion that builds each branch (the ~31-44ms floor even at zero
    collisions) is untouched and remains the next-largest cost driver -
    recorded as debt (docs/debt.md, DEBT-0001), not fixed here.
  Regression implications: High per section_21_regression_risk_matrix
    (shared engine, ~8 call sites across the public UDF surface) -
    mitigated by characterization tests over representative inputs
    (0-branch, 2-branch, ~70-branch) confirming byte-identical outputs
    before/after (32 checks, 0 diffs).
  Compatibility implications: none - GetDecayChain is not itself an
    exported UDF; no .dna change.
