DDR-0005: Path to Option Strict On
Status: Accepted
Date: 2026-07-10
Task: Enable Option Strict On project-wide (Epic)

Context
  RadToolz.vbproj currently builds with OptionStrict Off / OptionInfer Off
  (OptionExplicit On, OptionCompare Text). Building with the compiler
  override /p:OptionStrict=On surfaces 73 real compile errors across
  exactly 3 files (RadToolzFunctions.vb: 41, ProcessDecaySeries.vb: 26,
  WindowsDNSFunctions.vb: 6) - not a codebase-wide problem. Late binding on
  Object-typed collection/COM references is exactly the class of defect
  Option Strict exists to catch at compile time instead of at runtime
  inside a live workbook recalculation.

  Correction (2026-07-10, during Milestone 2): this DDR originally flagged
  RadToolzFunctions.vb:1225-1227's Msg/vbYes comparison as "a plausible
  live bug masked today only by Option Strict Off," reasoning that
  MsgBoxResult & String would stringify to "Yes" and then fail comparing
  against the integer vbYes. Empirically tested before touching it
  (compiled and ran a standalone repro): MsgBoxResult.Yes & "" actually
  produces the STRING "6" (VB's runtime enum-to-string conversion uses the
  underlying numeric value, not the symbolic name), and late-bound Object
  "=" parses that numeric string and compares correctly. All three
  Msg/vbYes sites in the codebase (RTZLicense:1227, RTZUpdate:1440/1447)
  work correctly today and always have - retracting the "live bug" claim.
  Milestone 2 fixed all three for Option Strict compliance with zero
  behavior change, not as a bug fix.

Decision
  Reach Option Strict On through an ordered sequence of independently-
  shippable milestones, each fixing one coherent root cause of the current
  error count, rather than one large flip-the-switch change. The final
  milestone flips <OptionStrict>Off</OptionStrict> to On in RadToolz.vbproj
  once the preceding milestones have eliminated every error.

Roadmap
  Milestone 1 (DDR-0006, COMPLETE 2026-07-10): Microsoft.VisualBasic.
    Collection -> a small List(Of DecaySeriesItem)-backed DecayChainBranch
    wrapper, closing DEBT-0001's remaining scope. Resolved 53 of 73 errors
    (29 in RadToolzFunctions.vb, 24 of 26 in ProcessDecaySeries.vb) -
    verified by re-running the scoping build (73 -> 20) and a 7-run
    characterization pass (0 diffs). 2 errors originally estimated as part
    of this milestone turned out to belong to Milestone 4 instead (see
    below).
  Milestone 2 (COMPLETE 2026-07-10): Object-typed UDF return variables
    used in typed arithmetic/comparison (RadDecay, FGE), Object-returning
    internal calls to RTZVers() used in String concatenation
    (RTZAttribution, RTZFunctions, RTZParams, RTZRefs), and the three
    Msg/vbYes sites (RTZLicense, RTZUpdate x2 - see Context correction
    above: not a bug, zero behavior change). Resolved exactly the 10
    errors estimated. Verified: clean build (0 errors, 0 warnings, both
    bitness targets), scoping build 20 -> 10 errors. No characterization
    harness needed - none of these functions are in the shared
    decay-chain engine, and every changed comparison/arithmetic operand
    was traced back to an unchanged runtime value before the fix.
  Milestone 3 (COMPLETE 2026-07-10): WindowsDNSFunctions.vb - Object-typed
    locals capturing an already-strongly-typed expression (DnsQuery's
    Integer return, Marshal.PtrToStructure(Of DnsRecordTxt)'s
    DnsRecordTxt return, Marshal.PtrToStringUni's String return). No
    P/Invoke signature, struct layout, or marshaling changes - purely
    local variable static-type tightening to match what each call already
    returned at runtime. Also removed the #Disable/#Enable Warning
    BC42016 pragma around txt.StartsWith(prefix), now meaningless since
    txt is no longer late-bound. Resolved exactly the 6 errors estimated;
    verified clean build (0 errors, 0 warnings) and scoping build 10 -> 4
    errors (the 4 remaining are exactly Milestone 4's iExcel sites).
  Milestone 4 (found during Milestone-1 discovery; scope refined during
    Milestone-1 implementation): iExcel.Range(...) / iExcel.Worksheets(...)
    calls, in both ProcessDecaySeries.ListAll (2 errors) and
    RadToolzFunctions.RTZFunctions (2 errors, found only once Milestone 1's
    changes let the scoping build isolate them from the cDC-related count
    they were originally lumped into) - iExcel is a deliberately late-bound
    Object handle to the host Excel.Application (see Constants.vb's
    comment on the cached Application handle). 4 errors total. Distinct
    root cause from Milestones 1-3: COM Application/Range access, not a
    managed collection or a needlessly-Object local. Needs its own risk
    read before choosing early-bound typed access versus a narrower fix,
    since Constants.vb's existing comment specifically defends late
    binding here as an established pattern for COM access elsewhere in the
    codebase - reversing it without revisiting the pattern generally is a
    design decision worth its own DDR.
  Milestone 5: Flip <OptionStrict>Off</OptionStrict> to On in
    RadToolz.vbproj. Full rebuild, both bitness targets, zero errors
    expected by construction; zero new warnings required per
    testing_standards.build_verification.

Alternatives Considered
  1. One big-bang change across all 73 errors - rejected: violates
     diff_minimization_policy and regression_risk_matrix guidance for a
     High-risk shared engine; unreviewable as a single diff, unbisectable
     if something regresses.
  2. Suppress individual error categories instead of fixing them - not
     applicable; these are compile errors under Option Strict On, not
     suppressible warnings, so there is no such option once the switch is
     flipped. Raised and rejected only to record it was considered.

Consequences
  Positive: each milestone independently leaves the add-in shippable and
    separately reviewable; Milestone 1 also closes DEBT-0001, a two-DDR-
    old open item. Milestone 2's Msg/vbYes sites looked like a plausible
    bug hunt going in but turned out not to be one (see Context
    correction) - each milestone is still a genuine type-safety
    improvement even where it isn't also a behavior fix.
  Negative / accepted trade-offs: five milestones is more process overhead
    than a single change; accepted because the alternative fails
    regression_risk_matrix's High-risk-shared-engine bar for a one-shot
    diff.
  Regression implications: assessed per-milestone; Milestone 1 carries the
    highest blast radius (shared decay-chain engine, 8 downstream UDFs)
    and gets its own DDR-0006 with characterization tests before any code
    is written.
  Compatibility implications: none across the whole epic - Option Strict
    is a compiler switch, not part of the exported UDF surface, and no
    milestone changes a public UDF's name, argument list, or semantics.

Resume Point (as of 2026-07-10, session paused before Milestone 4)
  Working tree has uncommitted changes for Milestones 1-3 (not committed
  per standing instruction - user commits, not Claude). Release build is
  clean (0 errors, 0 warnings, both bitness targets) as of the last build
  run this session. /p:OptionStrict=On scoping build currently shows
  exactly 4 remaining errors, all Milestone 4's iExcel COM-access pattern:
    ProcessDecaySeries.vb(397,27) and (398,29) - ListAll:
      iRng = DirectCast(iExcel.Range(uRng), Range)
      iSheet = DirectCast(iExcel.Worksheets(uSheet), Worksheet)
    RadToolzFunctions.vb(1123,27) and (1124,29) - RTZFunctions:
      iRng = DirectCast(iExcel.Range(uRngVal), Range)
      iSheet = DirectCast(iExcel.Worksheets(uSheet), Worksheet)
  iExcel is declared once, module/class-level, As Object (grep "iExcel As"
  to find the declaration - not yet located/read this session). Milestone
  4 has not started design work yet. Open question to resolve first: does
  retyping iExcel (or just these 4 local DirectCast results) to the
  early-bound Microsoft.Office.Interop.Excel.Application/Range/Worksheet
  types conflict with Constants.vb's documented rationale for keeping the
  cached Application handle late-bound (section_16_com_and_memory_lifetime_rules
  context)? That comment needs re-reading before proposing a fix - it may
  defend late binding for a reason that does or doesn't apply to these 4
  specific call sites. This needs its own DDR per DDR-0005's Milestone 4
  entry above before any code changes.
  To resume: re-run the two verification builds (plain Release build, and
  the /p:OptionStrict=On override build per the commands used throughout
  this epic) to confirm nothing changed, then start Milestone 4 discovery
  from the iExcel declaration and the Constants.vb comment referenced
  above.
