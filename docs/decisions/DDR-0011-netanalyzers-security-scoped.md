DDR-0011: Add NetAnalyzers as a second, security-scoped static analysis engine
Status: Accepted
Date: 2026-07-17
Task: DEBT-0010 - second static-analysis engine (Medium)

Context
  SecurityCodeScan's rule set hasn't grown since its last 2022 release; DEBT-0010
  asks for a second, actively-maintained engine. The project already has two
  adjacent, pre-existing analysis mechanisms that had to be accounted for so the
  new one doesn't duplicate or get silently overridden by them: a
  CodeAnalysisRuleSet (JJPAnalysisRules.ruleset) gated behind RunCodeAnalysis=false
  in RadToolz.vbproj, and a blanket dotnet_analyzer_diagnostic.severity = warning
  default in RadToolz/.editorconfig that would otherwise promote every rule a new
  analyzer package ships, not just security ones. The codebase has never been
  scanned with NetAnalyzers, so gating the build on it immediately risked failing
  CI for the whole team on pre-existing findings unrelated to whatever anyone
  happens to be changing.

Decision
  Add Microsoft.CodeAnalysis.NetAnalyzers 10.0.302 as a NuGet analyzer package
  referenced directly in RadToolz.vbproj, scoped to suppress the non-security
  rules it currently enables by default on this codebase. Landed first as
  report-only - warnings visible in the build log, not failing the job - with a
  hard fail-the-build gate to follow as a separate, later task once the codebase
  is characterized clean against it.

  Implementation note: category-level bulk severity
  (dotnet_analyzer_diagnostic.category-<Name>.severity) does not take effect on
  this project's compiler toolchain - verified empirically: individual
  dotnet_diagnostic.<ID>.severity overrides work, category-level ones silently
  no-op. Scoping is therefore done by suppressing each non-security,
  enabled-by-default rule ID individually (CA1024, CA1031, CA1045, CA1060,
  CA1305, CA1502, CA1707, CA1822, CA2263 as of this writing) rather than by
  category. This list reflects what the codebase currently triggers, not
  every rule NetAnalyzers could ever enable by default; a newly-triggered
  non-security rule surfaces as a build warning (report-only, not gating) until
  added to RadToolz/.editorconfig.

  A second wrinkle: RadToolz.vbproj's pre-existing CodeAnalysisRuleSet
  (JJPAnalysisRules.ruleset) is passed to the compiler via /ruleset: regardless
  of RunCodeAnalysis=false, and its explicit severities win over
  .editorconfig for any overlapping rule ID. CA1502 (cyclomatic complexity) is
  in both files, so it cannot be suppressed via .editorconfig while the old
  ruleset stays wired - it remains a build warning (3 occurrences, all
  pre-existing legacy code, unrelated to this task). Per user decision, this
  gap is accepted and documented rather than editing the legacy ruleset file,
  which is out of DEBT-0010's scope. Recorded as DEBT-0011.

  No Security-category findings were present anywhere in the codebase at
  implementation time.

Alternatives Considered
  1. CodeQL - rejected for this pass: broader coverage, but needs its own
     job/workflow and adds CI time; NetAnalyzers chosen as the lower-lift first
     step.
  2. Revive the dormant JJPAnalysisRules.ruleset (flip RunCodeAnalysis=true)
     instead of adding a new package - rejected: unclear why it was disabled,
     it's the legacy FxCop-style engine rather than modern Roslyn/NetAnalyzers,
     and it's a ~180-rule general-quality ruleset, not the security-only scope
     DEBT-0010 asks for. Left untouched, out of scope.
  3. Hard-gate immediately (fail build on first finding, matching
     SecurityCodeScan's current behavior) - rejected: never run before on this
     codebase; an immediate gate risked breaking CI on unrelated pre-existing
     findings with no single bisectable change to point to.

Consequences
  Positive:
    Second, actively-maintained security engine; no added CI time in this phase
    (runs in-process during the existing msbuild step, not a new job); local dev
    builds surface the same findings CI does, since the package is referenced at
    the project level rather than CI-only.
  Negative / accepted trade-offs:
    Two-task rollout means DEBT-0010 isn't fully closed until the follow-up
    gating task lands; .editorconfig gains RadToolz-specific security-category
    overrides alongside the pre-existing blanket JJP severity default.
  Regression implications:
    None to product behavior - build-time/CI-only change. Verified Debug and
    Release builds still succeed and both XLL bitness targets still pack.
  Compatibility implications (UDF surface, persisted formats, .dna):
    None.
