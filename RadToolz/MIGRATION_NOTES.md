# Decay-series data migration: hardcoded Collection → cached JSON

## What changed

1. **`DecaySeriesData.json`** (new) — the 182 isotope records that used to
   be 182 hardcoded `New DecaySeriesItem With {...}` blocks inside
   `ProcessDecaySeries.GetDecaySeries()`. Field names match
   `DecaySeriesItem`'s public properties exactly (`Isotope`, `Daughter`,
   `BranchingRatio`, `Lambda`, `A1`, `A2`, and the eleven `DCF*` fields), so
   Newtonsoft.Json deserializes straight into `DecaySeriesItem` with no
   custom converter needed. Record order is preserved exactly - this
   matters because `GetDecayChain`'s branch-point logic depends on
   sequential position, and the same isotope symbol legitimately appears
   more than once (e.g. `PA-234M` appears twice, once per decay branch).

2. **`DecaySeriesRepository.vb`** (new) — loads and caches the data.
   - Reads `DecaySeriesData.json` from this assembly's embedded
     resources (`Assembly.GetManifestResourceStream`), so it travels
     inside `RadToolz.dll` and therefore inside the packed `.xll` — no
     external file to deploy or go missing.
   - Parses it exactly once via `Lazy(Of List(Of DecaySeriesItem))`
     (`ExecutionAndPublication` mode), which is thread-safe even if Excel
     calls into multiple RadToolz UDFs on different calculation threads
     at once.
   - `GetAll()` returns a **new `Collection`** on every call (so nothing
     about the existing calling convention changes), but the
     `DecaySeriesItem` objects inside it are the **same cached instances**
     every time - building the Collection is just 182 `.Add()` calls, not
     182 allocations or a JSON re-parse.

3. **`ProcessDecaySeries.vb`** (edited) — `GetDecaySeries()`'s ~3,840-line
   body was replaced with a single line: `Return
   DecaySeriesRepository.GetAll()`. Nothing before or after that function
   changed - verified byte-for-byte identical against the original file
   outside the replaced range. Because the return type (`Collection`) and
   contents are unchanged, `GetDecayChain`, `ListAll`, `VerifyIsotope`,
   and every UDF in `RadToolzFunctions.vb` need **no changes at all**.

4. **`RadToolz.vbproj`** (edited) - added:
   - A `Reference` to `Newtonsoft.Json` (v13.0.4, the current stable
     release - everything below 13.0.0 is flagged vulnerable on
     NuGet.org).
   - `<Compile Include="DecaySeriesRepository.vb" />`
   - `<EmbeddedResource Include="DecaySeriesData.json" />`

5. **`packages.config`** (edited) - added the `Newtonsoft.Json` 13.0.4
   entry, in the same style as your existing package entries.

## Why this is safe to cache (not just faster)

I traced every place that reads from the master collection
(`AddDecayChainItem`, `BubbleSortCollection`, `ListAll`, `VerifyIsotope`).
All of them copy each item's scalar fields into a **brand-new**
`DecaySeriesItem` via `LoadDecaySeriesItem()` before anything downstream
ever sets a value - nothing in the codebase mutates a `DecaySeriesItem`
obtained from `GetDecaySeries()` in place. That convention is what makes
sharing the same cached objects across every call safe. `DecaySeriesRepository.vb`
documents this contract explicitly so it stays true going forward.

## Data-integrity verification performed

Before touching any code, I extracted the JSON with a script and validated it
three independent ways:
- **Count**: 182 blocks found via regex parsing = 182 `dci.Add(dsi)` calls
  in the original source = 182 records in the JSON.
- **Order & identity**: an independent `grep`-based extraction of every
  `.Isotope = "..."` line was compared against the parsed JSON's isotope
  order - exact match, in sequence.
- **Numeric fidelity**: every field was round-tripped through `Decimal`
  before conversion to JSON's float64, and checked for NaN/Inf (none
  found). Spot-checked the first record (CM-246), last record (F-18), and
  the two-branch `PA-234M` duplicate against the original source text -
  all exact matches, including the `4.6173...E-12`-scale `Lambda` value.

## What you need to do in Visual Studio 2022

1. Copy the five files below into your project folder (overwriting the
   existing `ProcessDecaySeries.vb`, `RadToolz.vbproj`, `packages.config`).
2. Right-click the solution → **Restore NuGet Packages** (or just build -
   packages.config-style restore is automatic) to pull down
   Newtonsoft.Json 13.0.4.
3. Build. `DecaySeriesData.json` will show up in Solution Explorer with
   Build Action **Embedded Resource** automatically (it's declared that
   way in the `.vbproj`).
4. Sanity check: call `RTZParams` (or any UDF, e.g. `=DCF("CS-137","ING")`)
   and confirm results match what you'd get from the current build.
   `DecaySeriesRepository.Count` will be `182` once loaded, if you want a
   quick diagnostic check.

I wasn't able to compile this in my own sandbox - it's an old-style
.NET Framework 4.8.1 project with Office/Excel COM interop, which needs
Windows + full MSBuild. Everything here was hand-verified for syntax and
cross-checked against the data, but please do a build + a few spot-check
formulas before relying on it.
