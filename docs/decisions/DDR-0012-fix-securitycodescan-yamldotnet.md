DDR-0012: Fix SecurityCodeScan's non-functional analyzer wiring in CI
Status: Proposed
Date: 2026-07-17
Task: Discovered during DEBT-0011 investigation (Large - security-sensitive CI gate)

Context
  While investigating DEBT-0011 (a cosmetic CA1502/ruleset overlap), local
  reproduction of exactly what security-code-scan-add-action does in CI
  revealed that every single SecurityCodeScan analyzer rule throws
  TypeInitializationException / FileNotFoundException on 'YamlDotNet,
  Version=11.0.0.0' and never runs. This was confirmed against the real CI
  log (run 29610141719, job 87982631233, via gh CLI after the user
  authenticated it), not just the local repro - identical failure, every SCS
  analyzer type, every recent run.

  Root cause: security-code-scan-add-action's action.yml, for
  packages.config-style projects (RadToolz.vbproj is one), wires only
  <Analyzer Include="...SecurityCodeScan.VS2019.dll"> into the project at
  CI time. It does not also wire SecurityCodeScan.VS2019.dll's own
  YamlDotNet.dll dependency, which several of its analyzer types need to
  type-initialize. Roslyn's analyzer host does not probe an analyzer DLL's
  own directory for dependencies the way ordinary .NET assembly loading
  would - each <Analyzer Include> entry needs to be wired explicitly. Git
  history confirms this: before SecurityCodeScan was moved from a local
  packages.config reference to CI-only (commit 3e015d3, 2026-07-10), the
  project wired BOTH SecurityCodeScan.VS2019.dll and YamlDotNet.dll as
  separate <Analyzer> entries. The CI action was never doing that.

  Practical effect: the failure surfaces only as warning BC42376 /
  AD0001 (analyzer-load-failure), never as an SCS0xxx finding, so the
  existing "Fail if SecurityCodeScan found anything" grep step
  (which only matches 'warning SCS\d+') has nothing to catch. CI has
  reported success on every run since DDR-0009 restored this workflow,
  but SecurityCodeScan has not actually scanned any code in any of them.

Decision
  Add a step to .github/workflows/securitycodescan.yml, between "Restore
  dependencies" and "Build", that discovers whichever
  SecurityCodeScan.VS2019.<version> folder nuget restore actually pulled
  down (the add-action always installs "latest" - not pinned - so the
  fix must not hardcode a version) and appends the missing
  <Analyzer Include="...YamlDotNet.dll"> entry to the project before it
  builds. The step fails the job loudly (exit 1, ::error::) if the
  expected package layout isn't found, rather than silently doing
  nothing - matching this project's existing "fail loudly" convention
  (e.g. 6b65245).

Alternatives Considered
  1. Fork or vendor a patched copy of security-code-scan-add-action -
     rejected: heavier maintenance burden (a whole action to keep in
     sync) for a one-line gap; a small workaround step in the existing
     workflow is a much smaller, more reviewable surface.
  2. Go back to wiring SecurityCodeScan locally in RadToolz.vbproj/
     packages.config (as it was before 3e015d3) instead of patching the
     CI action's output - rejected: reintroduces SCS as a local dev-time
     dependency and diff, when the project's established pattern (per
     DDR-0009) is CI-only; the CI-side patch keeps that boundary intact
     and needs no committed vbproj/packages.config change at all.
  3. Report the bug upstream to security-code-scan-add-action and wait -
     rejected as the sole fix: worth doing separately, but leaves the
     gate non-functional in the meantime for an unknown period.

Consequences
  Positive: SecurityCodeScan will actually scan code for the first time
    since its restoration. CI-only change; no local dev build impact.
    Fails loudly instead of silently if the workaround's assumptions
    ever stop holding (e.g. the package restructures analyzers
    differently in a future version).
  Negative / accepted trade-offs: once SCS actually runs, it may surface
    real findings in the existing 10+ year old codebase that were never
    caught because the tool was never functioning - the next CI run
    after this lands could fail for the first time on genuine findings,
    not on this fix being wrong. That is the intended, correct outcome
    of fixing a broken gate, not a regression to worry about.
  Regression implications: none to product behavior - CI-only. Real risk
    is CI going red on a genuine, previously-hidden finding; if that
    happens it is signal, not noise, and gets triaged as its own task.
  Compatibility implications (UDF surface, persisted formats, .dna):
    none.
