DDR-0003: Pack Newtonsoft.Json into the add-in XLL
Status: Accepted
Date: 2026-07-08
Task: Fix dangling Newtonsoft.Json runtime dependency (Large - .dna config change)

Context
  RadToolz.dll depends on Newtonsoft.Json (used by DecaySeriesRepository.
  LoadFromEmbeddedResource to deserialize the isotope table). RadToolz-AddIn.dna
  only declared RadToolz.dll itself as Pack="true"; it had no <Reference> entry
  for Newtonsoft.Json. As a result, bin\Release\Newtonsoft.Json.dll sat loose
  next to the packed XLLs rather than being embedded - confirmed by inspecting
  the Release output directory. RadToolz-AddIn-packed.xll/
  RadToolz-AddIn64-packed.xll are meant to be single-file distributables; before
  this change they would only actually load if Newtonsoft.Json.dll happened to
  be copied alongside them.

Decision
  Add <Reference Path="Newtonsoft.Json.dll" Pack="true" /> to
  RadToolz-AddIn.dna, inside <DnaLibrary>. ExcelDnaPack embeds the assembly's
  bytes as a resource in both packed XLLs (32- and 64-bit), and Excel-DNA's own
  assembly-resolve hook loads it from there at runtime instead of probing the
  file system.

Alternatives Considered
  1. ILMerge/ILRepack Newtonsoft.Json into RadToolz.dll at build time -
     rejected: adds a new build tool dependency for a problem ExcelDnaPack
     already solves natively via .dna <Reference Pack="true">.
  2. Leave Newtonsoft.Json.dll as a loose file and document it as a required
     companion file - rejected: that was the status quo being fixed, and it
     defeats the point of a "packed" single-file XLL.

Consequences
  Positive: both packed XLLs are now genuinely self-contained; no loose
    Newtonsoft.Json.dll is required alongside them. Verified via the
    ExcelDnaPack build log (a new "Updating resource: Type: ASSEMBLY_LZMA,
    Name: NEWTONSOFT.JSON, Length: 211340" entry for both bitness targets,
    alongside the existing RADTOOLZ entry) and independently by scanning the
    raw bytes of both packed .xll files for the Newtonsoft.Json assembly name
    string.
  Negative / accepted trade-offs: packed XLL file size grows by Newtonsoft.
    Json's compressed assembly size (~206 KB per bitness target).
    Newtonsoft.Json.dll/.xml still get copied loose into bin\Release as
    normal MSBuild CopyLocal output for the compile-time reference - this is
    expected and harmless; only the two -packed.xll files are the actual
    shipped deliverable.
  Regression implications: Low - purely additive packaging config; RadToolz.
    dll's own code and behavior are unchanged. Clean Release rebuild: 0
    warnings, 0 errors, both bitness targets.
  Compatibility implications: None to the UDF surface. This changes the
    shipped XLL's file layout (self-contained vs. requiring a sibling DLL) -
    a compatibility improvement for anyone currently distributing just the
    -packed.xll.
