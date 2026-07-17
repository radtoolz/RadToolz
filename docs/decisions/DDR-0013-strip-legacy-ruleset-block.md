DDR-0013: Remove JJPAnalysisRules.ruleset's legacy general-quality rule block
Status: Accepted
Date: 2026-07-17
Task: DEBT-0011 (Large - touches the same ruleset file backing the CI security gate)

Context
  DEBT-0011 recorded that CA1502 (cyclomatic complexity) could not be
  suppressed via RadToolz/.editorconfig while JJPAnalysisRules.ruleset stayed
  wired: the ruleset's Microsoft.Analyzers.ManagedCodeAnalysis block
  explicitly sets CA1502 to Action="Warning", and that ruleset-level
  directive (passed to the compiler via /ruleset:, independent of
  RunCodeAnalysis) won out over .editorconfig for that one rule. DEBT-0011's
  suggested remedy - strip the block, leave the SecurityCodeScan.VS2019
  block untouched - required first verifying the SCS block was genuinely
  load-bearing before touching a file that also backs the CI security gate.
  That verification happened as DDR-0012: real CI logs confirmed
  SecurityCodeScan was silently non-functional (a missing YamlDotNet
  dependency), and after fixing it, a deliberately-planted weak-hashing
  violation correctly surfaced as error SCS0006 - proof the ruleset's
  SCS0xxx -> Error entries are genuinely active and must be preserved.

  Separately, CA1502 turned out not to be a one-off: Microsoft's own docs
  list CA1502 as "Enabled by default in .NET 10: No" (as is CA1707,
  spot-checked) - the Microsoft.Analyzers.ManagedCodeAnalysis block is
  force-enabling a number of legacy CA1xxx/CA2xxx rules that ship disabled
  by default, via the same ruleset mechanism .editorconfig can't override
  per-rule. Isolated testing (removing the block, then separately removing
  DEBT-0010's .editorconfig suppression list) showed this only explains
  5 of the 9 rules DEBT-0010 was suppressing: CA1502, CA1024, CA1031,
  CA1045, and CA1060 stop firing once the ruleset block is gone, with no
  .editorconfig entry needed. CA1305, CA1707, CA1822, and CA2263 keep
  firing regardless - despite CA1707 individually showing "disabled by
  default" in the same Microsoft docs, contradicting a first attempt to
  drop the whole suppression list as redundant. Likely explanation: the
  "enabled by default" table describes the .NET SDK's curated AnalysisMode
  defaults, which don't apply here at all - this is an old-style project
  with no <AnalysisLevel>/<EnableNETAnalyzers>, wiring NetAnalyzers as a
  raw <Analyzer> reference, so actual behavior is each rule's raw baked-in
  DiagnosticDescriptor default, not the SDK's curated table. Empirical
  build output is the only reliable signal in this project's setup, not
  the docs table - confirmed by testing, not assumed.

Decision
  Remove the Microsoft.Analyzers.ManagedCodeAnalysis rule block from
  JJPAnalysisRules.ruleset in full. Leave IncludeAll, the
  Microsoft.CodeAnalysis.Analyzers (RS10xx) block, and the
  SecurityCodeScan.VS2019 block untouched. Keep RadToolz/.editorconfig's
  full 9-rule suppression list from DEBT-0010 as-is (CA1502, CA1024,
  CA1031, CA1045, CA1060, CA1305, CA1707, CA1822, CA2263) - 5 entries are
  now technically redundant given the ruleset fix, but verified behavior
  showed the other 4 are independently required, and there's no reliable
  way to tell which is which from documentation alone on this toolchain.
  Trimming the list would require re-verifying it stays correct forever;
  keeping it intact is one fewer thing that can silently regress.

Alternatives Considered
  1. Leave the block in place, keep working around it rule-by-rule in
     .editorconfig as new non-security rules surface - rejected: this is
     what DEBT-0010 already had to do for 8 rules and still couldn't fully
     work around for CA1502; it's treating the symptom repeatedly instead
     of the cause once, and leaves a stale, unmaintained ~180-rule legacy
     list in place for no purpose anyone could identify (RunCodeAnalysis is
     already false; nothing else in the repo references this block).
  2. Delete JJPAnalysisRules.ruleset entirely - rejected (as already
     decided when DEBT-0011 was first recorded): the SecurityCodeScan.VS2019
     block is load-bearing for the CI gate per DDR-0012's verification;
     deleting the whole file would remove that too.

Consequences
  Positive: CA1502 stops firing - closes DEBT-0011 for real rather than
    documenting it as an accepted gap. Root cause (a legacy, unmaintained
    ~180-rule block with no other purpose - RunCodeAnalysis is already
    false) is removed rather than worked around per-rule.
  Negative / accepted trade-offs: DEBT-0010's .editorconfig suppression
    list stays exactly as large as before (5 of its 9 entries are now
    technically redundant, but kept rather than risk trimming to a set
    this session already got wrong once).
  Regression implications: none to product behavior - ruleset/build-config
    only. Verified locally: clean Release rebuild (full solution, both
    bitness targets), 0 warnings, with the ruleset block removed and the
    full .editorconfig suppression list restored together.
  Compatibility implications (UDF surface, persisted formats, .dna): none.
  Security gate implications: SecurityCodeScan.VS2019 block untouched and
    verified still present in the file; not part of this change.
