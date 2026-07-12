DDR-0009: Restore SecurityCodeScan CI workflow
Status: Accepted
Date: 2026-07-12
Task: Supply-chain hygiene review follow-up (I2) - CI static analysis gap

Context
  SecurityCodeScan (SCS), the project's only static analyzer capable of
  scanning the actual VB.NET application source for security-vulnerability
  patterns (SQL/command injection, path traversal, weak crypto, insecure
  deserialization, hardcoded secrets, etc.), was deleted from CI on
  2024-12-30 (5d7b271 "Delete SCS") and never replaced. CodeQL
  (codeql.yml, currently on master) does run, but its own header comment
  states it is scoped to the "actions" language only, since CodeQL does not
  support VB.NET - it scans the workflow YAML files themselves, not
  RadToolz's source. JJPAnalysisRules.ruleset remains in the project
  configuration with nothing in CI left to exercise it. This surfaced
  during a supply-chain hygiene review (I2), prompted in part by this
  session's own discovery of a real memory-safety bug in the DNS P/Invoke
  interop code (DDR-0007) - exactly the class of security-sensitive code
  this project's own handbook (security_standards) calls out, and exactly
  the class of finding a dedicated .NET security analyzer exists to catch
  early (though SCS's own pattern set would not itself have caught that
  particular bug, since P/Invoke marshaling correctness isn't one of its
  checks).

Decision
  Restore .github/workflows/securitycodescan.yml with the same job
  definition it had before deletion (checkout, restore, build under the SCS
  analyzer, on push/PR to master and a weekly schedule), updating only the
  two generic, tag-pinned actions (actions/checkout, microsoft/setup-
  msbuild) to the versions already in use by this repo's other active CI
  workflows, for consistency. The SARIF-upload steps remain commented out,
  matching their state at deletion - re-enabling SARIF upload to GitHub's
  Code Scanning tab is a separate, follow-on decision, not bundled here.

Alternatives Considered
  1. Adopt a different/newer .NET security analyzer instead of restoring
     SCS - rejected for now: SCS was already configured and proven for this
     project before its removal; reintroducing the known-working tool
     closes the gap with the smallest, most reviewable change. Evaluating
     alternatives can be a separate future decision if SCS proves
     insufficient.
  2. Leave the gap and record it as accepted risk - rejected: this is a
     downloadable add-in whose interop/security-sensitive code has already
     produced one real, credible defect this session (DDR-0007); leaving no
     automated security-pattern scanning in place is not an acceptable
     standing risk once a live example of why it matters exists.
  3. Broaden the workflow's branch triggers to include dev (this session's
     active branch), not just master - not adopted here: the original
     scope was master-only and every other CI workflow in this repo is
     likewise centralized on master; changing the trigger scope is a
     separate design decision from restoring what was deleted, and is left
     to a future DDR if wanted.

Consequences
  Positive: closes the CI static-analysis gap for the actual VB.NET
    application source; JJPAnalysisRules.ruleset is exercised again;
    restores parity with the tool's pre-deletion configuration rather than
    introducing new, unreviewed CI behavior.
  Negative / accepted trade-offs: SCS only runs against pushes/PRs to
    master and a weekly schedule - a change made and left on a feature/dev
    branch (as this session's own work currently is) is not scanned until
    merged; this matches the original design and the repo's existing CI
    convention, not a new gap introduced by this decision.
  Regression implications: none - adds a new CI workflow file; does not
    modify build output, packaging, or any shipped artifact.
  Compatibility implications (UDF surface, persisted formats, .dna): none.
