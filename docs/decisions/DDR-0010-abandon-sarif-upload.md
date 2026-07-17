DDR-0010: Abandon SARIF upload for SecurityCodeScan; fail the job on findings instead
Status: Proposed
Date: 2026-07-12
Task: CI static analysis gap (DDR-0009) - SARIF integration follow-up

Context
  Following DDR-0009's restoration of the SecurityCodeScan (SCS) CI
  workflow, an attempt was made to also surface findings in GitHub's
  Security > Code scanning alerts tab (via SARIF upload) rather than only
  as inline build warnings. Three independent attempts failed, each for a
  different surface reason traced back to the same root cause:
    1. The original security-code-scan-results-action (pinned since before
       its restoration) installs Sarif.Multitool 2.3.10, which requires the
       .NET Core 3.1 runtime - removed from GitHub-hosted runner images -
       so the step failed outright.
    2. Bypassing that action and emitting SARIF natively via MSBuild's
       /p:ErrorLog=...,version=2.1 produced a file upload-sarif's validator
       rejected: flat tool fields instead of a nested tool.driver, run-level
       rules instead of tool.driver.rules, string message fields instead of
       message objects, and legacy fields (suppressionStates, a
       resultFile-keyed location) with no equivalent in the SARIF 2.1.0
       schema.
    3. Upgrading that output with a current, actively maintained
       Sarif.Multitool (5.5.0, targets net8.0, no EOL runtime dependency)
       via `sarif rewrite --sarif-output-version Current` produced a
       schema-valid file that uploaded successfully, but GitHub's Code
       Scanning backend then rejected every result during processing
       ("expected at least one location") - the generic rewrite/upgrade
       command silently dropped the resultFile-keyed location data instead
       of translating it, since resultFile is not a field from any
       standard SARIF version the tool understands.
  This project's VB compiler toolset (invoked via classic msbuild for this
  non-SDK-style project) emits a non-standard, apparently
  pre-standardization SARIF dialect that neither the original bespoke tool
  chain nor current, actively maintained generic SARIF tooling translates
  correctly. Each of the three attempts failed in a different place rather
  than converging - a signal to stop chasing this path rather than attempt
  a fourth fix.

Decision
  Abandon SARIF upload for SCS. Findings remain visible only as ordinary
  "warning SCS####" lines in the Build step's log, unchanged from how they
  have worked since DDR-0009's restoration. Added a follow-up step that
  greps the build log for that pattern and fails the job (non-zero exit,
  with the specific finding lines echoed as an annotated error) if any are
  present - turning a run with findings into a visible failing check rather
  than a silently green one, since a scan whose output requires manually
  opening every log to check is not effectively monitored.

Alternatives Considered
  1. Keep chasing SARIF translation with a hand-written field-mapping
     script for the resultFile-style locations - rejected: would require
     reverse-engineering an undocumented, Roslyn-internal SARIF dialect
     with no confirmed specification, disproportionate effort for a
     supplementary CI feature once a simpler mechanism (fail-on-finding)
     already gives real, actionable notification.
  2. Reintroduce the original action's exact Sarif.Multitool 2.3.10 by also
     installing the EOL .NET Core 3.1 runtime alongside it - rejected:
     keeps an abandoned, unmaintained action alive by depending on an
     unsupported runtime indefinitely, with no confirmation it would even
     correctly translate the resultFile-style locations either (never
     tested, since Option A was chosen instead of pursuing this).
  3. Leave the job always green regardless of findings (the state
     immediately after DDR-0009, before this DDR) - rejected: findings
     that only exist as text in a build log nobody is prompted to open
     provide no real assurance.

Consequences
  Positive: SCS findings now produce an actionable failing check using
    GitHub's native CI failure-notification path, with no dependency on
    any SARIF translation; removes the now-unused security-events: write
    permission and the SARIF-specific steps, leaving the workflow simpler
    and closer to DDR-0009's original restoration.
  Negative / accepted trade-offs: no centralized Security > Code scanning
    alerts view, and no per-finding tracking, dismissal history, or
    cross-run de-duplication that view would provide; findings are visible
    per-run in the build log only, and a failing job blocks nothing by
    itself unless branch protection is separately configured to require
    this check.
  Regression implications: none - CI-only change; does not affect the
    shipped build output or the build's own pass/fail determination beyond
    the new, intentional fail condition tied specifically to SCS findings.
  Compatibility implications (UDF surface, persisted formats, .dna): none.

Related: DDR-0009 (original SCS restoration decision, still governs why
  the workflow runs at all; this DDR only concerns findings visibility).
