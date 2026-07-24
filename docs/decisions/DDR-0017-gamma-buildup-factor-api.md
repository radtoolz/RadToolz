DDR-0017: Add BuildupFactor UDF using JAERI-M 90-110 G-P coefficient data
Status: Proposed
Date: 2026-07-24
Task: Gamma buildup factor API (Large - new exported UDF surface)

Context
  RadToolz has no gamma-ray buildup factor function. The approved design
  adds one new exported UDF, BuildupFactor(Energy, Material, MeanFreePaths,
  Optional BuildupType), computing the point-isotropic, infinite-medium
  buildup factor via the ANSI/ANS-6.4.3-1991 Geometric-Progression (G-P)
  method (Harima), for Air, Water, Iron, Lead, and Concrete, both Exposure
  (EBF) and Energy-Absorption (EABF) buildup, over the standard 0.015-15
  MeV / 25-point energy grid.

  The design named NUREG/CR-5740 (Trubey, 1991; NRC ADAMS ML19059A414) as
  the data source, on the basis that it is a US-government report and
  therefore public domain. The PDF actually on disk under that filename
  (docs/references/ML19059A414.pdf, 101 scanned pages, no text layer) is a
  different document: JAERI-M 90-110, "QAD-CGGP2 and G33-GP2: Revised
  Versions of QAD-CGGP and G33-GP Codes with the Conversion Factors from
  Exposure to Ambient and Maximum Dose Equivalents" (Sakamoto, Y. and
  Tanaka, S., Japan Atomic Energy Research Institute, July 1990).

  This was confirmed by rendering the PDF's pages to images (pdftoppm/
  pytesseract were unavailable; pymupdf was installed via pip for this
  purpose) and reading its title page and Appendix D directly, since no
  text-extraction tool could read the scanned pages.

Decision
  Use JAERI-M 90-110 as the data source, with the discrepancy and its
  implications documented here (user-approved). Appendix D ("GP Buildup
  Factor Coefficients Data", pp.69-96) contains the b, c, a, Xk, d G-P
  fitting parameters for 26 materials including all five needed here, at
  the same 25-point 0.015-15 MeV grid, using the identical G-P formula
  (the report's eq. 6-7 matches the locked design's formula verbatim) -
  this is the primary JAERI source the ANS-6.4.3-1991 standard's own G-P
  coefficients are built from, not a secondary reproduction.

  Each material has up to two Appendix D tables: "[Material] MEDIUM,
  [Material] RESPONSE" (self-response) and "[Material] MEDIUM, AIR
  RESPONSE". Appendix B of the same report explicitly defines "AIR
  RESPONSE" as a "flux-to-exposure conversion factor" - direct textual
  confirmation that AIR RESPONSE = Exposure Buildup Factor (EBF).
  Self-response is therefore Energy-Absorption Buildup Factor (EABF) by
  elimination and by standard shielding-physics convention (the medium's
  own energy-absorption response). For Material=Air, medium and response
  coincide by construction, so Air's Exposure and Absorption coefficients
  are identical - both are stored (duplicated) in the data file so the
  lookup path needs no material-specific special case.

  Lead has no valid G-P fit at 0.015 and 0.020 MeV: both tables list all
  five coefficients as exactly 0 at those two energies (not a small or
  rounded value - an absent fit). BuildupFactor therefore enforces a
  material-specific energy floor: 0.03 MeV for Lead, 0.015 MeV for every
  other material. The two invalid Lead rows are simply not present in
  BuildupFactorData.json, so an energy floor check (not a data lookup
  failure) is what produces the descriptive error for Lead below 0.03 MeV.

  Table transcription: every material's two tables were read twice
  independently (a 200 DPI pass, then a 400 DPI pass immediately before
  writing BuildupFactorData.json) and cross-checked row by row. One
  transcription error was caught and corrected this way: the first-pass
  reading of Lead's self-response (EABF) table misread its D column
  starting at 0.050 MeV (a low-resolution misread, not a genuine
  ambiguity in the source - the corrected 400 DPI reading is internally
  consistent, with no missing or duplicated rows). All nine other tables
  matched exactly between passes. The full transcribed table is presented
  separately for the user's own spot-check against their ANS-6.4.3 copy,
  per the approved plan - this DDR's two-pass check does not replace that.

  Engine implementation mirrors the DecaySeriesRepository/DecaySeriesItem
  pattern: BuildupFactorItem.vb (POCO), BuildupFactorRepository.vb
  (Lazy-cached loader over the embedded BuildupFactorData.json resource,
  indexed by Material|BuildupType, sorted ascending by Energy),
  BuildupFactor.vb (material/buildup-type alias resolution, range
  validation, the G-P formula, and log-log energy interpolation between
  bracketing grid points - exact grid hits bypass interpolation). mfp=0
  returns 1.0 exactly before any data lookup. A computed result that is
  NaN or infinite (possible in principle if K(x) evaluates negative for
  some coefficient/mfp combination, making K^x undefined for a
  non-integer x) returns a descriptive error string instead of leaking a
  NaN into the workbook.

Alternatives Considered
  1. Track down the actual NUREG/CR-5740 PDF before transcribing anything -
     rejected per user direction: JAERI-M 90-110 is the primary source for
     the same coefficients, already in hand and fully verified to contain
     everything the design needs; delaying to source a different document
     with identical numeric content was not worth the delay.
  2. Treat Lead's 0.015/0.020 MeV zero-coefficient rows as a data error and
     extrapolate or substitute a nearby fit - rejected: fabricating
     coefficients the source itself does not provide is a correctness risk
     for no real benefit; a clear material-specific range error is safer
     and more honest about the fit's actual domain.
  3. Single embedded table keyed only by Material (deriving EABF/EBF some
     other way, e.g. a fixed offset) - rejected: the source provides two
     genuinely independent fitted coefficient sets per material; deriving
     one from the other would be a fabricated approximation, not the
     published data.

Consequences
  Positive: BuildupFactor is fully data-driven from a verified primary
    source, covers both buildup types and all five required materials
    over the complete standard energy range (except Lead's documented
    floor), and follows the existing embedded-JSON-plus-Lazy-repository
    pattern exactly, so it needs no new infrastructure.
  Negative / accepted trade-offs: the data source citation differs from
    what was originally locked (JAERI-M 90-110, not NUREG/CR-5740) -
    accepted by the user in this session. IsThreadSafe:=False, matching
    every existing RadToolz UDF, even though BuildupFactor is pure
    computation with no shared state and could safely be
    IsThreadSafe:=True; recorded as debt (see debt.md) rather than taken
    now, to keep this change's registration flags consistent with the
    rest of the exported surface rather than introducing the first
    thread-safe UDF as a side effect of an unrelated feature.
  Regression implications: Low (per regression_risk_matrix) - this is a
    wholly new UDF; no existing exported function's name, arguments, or
    behavior changes. RTZFunctions' row count and RTZRefs' citation text
    both changed to include it (RTZFunctions.vb), which is additive to
    those functions' own output, not a behavior change to anything else.
  Compatibility implications (UDF surface, persisted formats, .dna): New
    additive UDF; no existing UDF renamed, removed, or reordered. No
    persisted-format change. .dna is unaffected (no new AssemblyLocation/
    reference entries - the new .vb files compile into the existing
    RadToolz.dll already referenced there).

Copyright note
  JAERI-M 90-110 carries a "(C) Japan Atomic Energy Research Institute,
  1990" notice, unlike a US-government NUREG report (public domain under
  17 U.S.C. Section 105). The numeric G-P coefficients themselves are
  measured/fitted physical data (facts), not independently copyrightable
  expression, but this is a different legal footing than the "public
  domain (US govt/ORNL report)" basis originally assumed - accepted by
  the user in this session; the full citation is carried in RTZRefs and
  this DDR rather than asserting public-domain status the source does not
  actually have.
