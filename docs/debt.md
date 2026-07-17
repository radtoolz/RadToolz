# Technical Debt Register

Per CLAUDE.md (`section_26_technical_debt_handling`): debt is recorded here,
never fixed opportunistically inside an unrelated diff. Each entry becomes
its own classified task with its own approval before being addressed.

---

## Source: Fable 5 security & vulnerability assessment (2026-07-14)

Assessed commit `0a0cabf` (dev). Overall verdict: Low risk, no Critical/High/Medium
findings. Suggested remediation order: DEBT-0003, DEBT-0006, DEBT-0002, then
DEBT-0004/DEBT-0005, then DEBT-0009, at leisure.

## DEBT-0002: Update check trusts an unauthenticated DNS TXT record

- **Location:** `WindowsDNSFunctions.vb` (whole module); `RadToolzFunctions.vb:1433` (`RTZUpdate`).
- **Description:** `RTZUpdate()` reads the "latest version" string from a plain DNS TXT query (`DnsQuery_W`, `DNS_QUERY_STANDARD`) against `radtoolz.com` — no DNSSEC, no TLS, no signature. Anyone able to spoof DNS for the victim (rogue Wi-Fi, cache poisoning, malicious resolver) controls the string. Impact is contained today: the value must pass `Double.TryParse` (line 1435), the browser target is the hardcoded literal `https://www.radtoolz.com/` (never derived from the record), and the browser only opens after an explicit Yes on a MsgBox. Realistic attacks are limited to update suppression (spoof the victim's current version so a real update never announces) and nagware (spoof a higher version to produce a false "you should update" dialog). Any workbook containing `=RTZUpdate()` can trigger the query/dialogs on recalculation, including third-party workbooks.
- **Risk:** Low.
- **Suggested remedy:** Treat DNS as advisory only (effectively already true) or move the version check to HTTPS (e.g., GitHub Releases API or a JSON endpoint on radtoolz.com) to authenticate the publisher via existing certificate infrastructure. If DNS stays, document the trust model in a DDR, as was done for DDR-0008.
- **Date recorded:** 2026-07-17

## DEBT-0003: Version string parsed with current culture — wrong results on comma-decimal locales

- **Location:** `RadToolzFunctions.vb:1435` and `:1441`.
- **Description:** `Double.TryParse(vers, versNum)` uses the machine's current culture. On comma-decimal locales (German, French, most of Europe and South America), `"5.1"` from the TXT record parses as `51` — the period reads as a thousands separator — producing a false "RadToolz is now at version 5.1. You should update." dialog whenever the published version has a decimal. The redundant `Convert.ToDouble(vers)` at line 1441 (TryParse already populated `versNum`) inherits the same culture behavior. The display side already handles this correctly — `RTZVers` formats with `CultureInfo.InvariantCulture` — so the parse side is the one gap.
- **Risk:** Low (real user-facing misbehavior today, not a security issue).
- **Suggested remedy:** `Double.TryParse(vers, NumberStyles.Float, CultureInfo.InvariantCulture, versNum)`; delete line 1441.
- **Status:** CLOSED 2026-07-17 — `Double.TryParse` now takes `Globalization.NumberStyles.Float`/`Globalization.CultureInfo.InvariantCulture` explicitly; the redundant `Convert.ToDouble` line removed. Commit `8341d5f`.
- **Date recorded:** 2026-07-17
- **Date closed:** 2026-07-17

## DEBT-0004: Sheet-writing functions can destroy data; self-overwrite guard covers only the anchor cell

- **Location:** `RadToolzFunctions.vb:1114` (`RTZFunctions`), `:1278` (`RTZParams`); `ProcessDecaySeries.vb:356` (`ListAll`).
- **Description:** `RTZFunctions` writes ~19 rows × 2 columns and `RTZParams`/`ListAll` writes the full isotope table (~5,479 rows × 17 columns, ~93,000 cells) starting at a user-supplied anchor, overwriting whatever is there; COM writes from macro-type functions are not undoable with Ctrl+Z. The guard at lines 1114/1278 (`StrComp(cCellAddr, uCellAddr) = 0`) only rejects the case where the target anchor *is* the calling cell — if the calling cell lies anywhere else inside the output rectangle (e.g., `=RTZParams(A1)` entered in C10), the function overwrites its own formula mid-run along with everything around it. Documented as a known limitation in the argument help ("function does not check for values in cells"), but a one-mistake, unrecoverable wipe of ~93k cells is worth more protection. **Note:** this touches the handbook's stop-and-ask criterion for irreversible/data-loss-capable changes (`section_29_stop_and_ask`) — flagging for prioritization rather than treating as routine low-priority debt.
- **Risk:** Low per the external review, but data-loss potential is unrecoverable when triggered.
- **Suggested remedy:** Extend the guard to test whether the caller's cell intersects the computed output extent (top-left anchor + known row/column counts); consider refusing to write over any non-empty range without a confirmation dialog.
- **Date recorded:** 2026-07-17

## DEBT-0005: Reference-text regex breaks on sheet names containing spaces or non-word characters

- **Location:** `RadToolzFunctions.vb:1109`; `ProcessDecaySeries.vb:388`.
- **Description:** Both sheet writers parse the `xlfReftext` string with `\](\w*)'?!(.*)`. Sheet names limited to `\w` characters work; anything else — spaces ("My Sheet"), hyphens, periods, non-ASCII — fails to match, yielding empty groups, a throwing `iExcel.Range("")`, and a `Failed`/#VALUE result. Backtracking against quoted reftext forms (`'[Book1.xlsx]My Sheet'!$A$1`) confirms a partial/wrong-sheet match is not reachable — impact is broken functionality on legitimately named sheets, not a misdirected bulk write.
- **Risk:** Low (functional bug, not a security or data-integrity issue).
- **Suggested remedy:** Resolve the sheet name from the `ExcelReference` via the C API (`xlSheetNm`) and the local address via `xlfReftext` with `fAbsNum:=False`, rather than parsing reftext strings; or split on the last `!` with quote-aware unescaping.
- **Date recorded:** 2026-07-17

## DEBT-0006: `GetDecayChain` silently corrupts results if branch slots are ever exhausted

- **Location:** `ProcessDecaySeries.vb:173-178`; `Constants.vb:27`.
- **Description:** The empty-slot scan (`For y = currBranch + 1 To maxBranches`) has no failure path. If a future dataset forks a chain into more than `maxBranches` (150) branches, the loop completes without finding a slot, `nextBranch` silently retains its previous value, and the fork is appended onto an already-occupied branch, producing wrong decay-chain output with no error. Not attacker-reachable today (the database is embedded and curated; the widest known fork is U-238's ~70 branches, per the constant's own comment). **Note:** "silently corrupts results" touches the handbook's stop-and-ask criterion for data-corruption potential (`section_29_stop_and_ask`) — flagging for prioritization; for a radiological calculation tool, a loud failure costs nothing here.
- **Risk:** Low today given the curated dataset, but the failure mode is silently-wrong output rather than a crash.
- **Suggested remedy:** After the scan, if no empty slot was found, return an error via the existing `HandleErrors` path instead of proceeding; optionally assert `gdcdci(nextBranch).Count = 0` before the copy.
- **Status:** CLOSED 2026-07-17 — added a `foundSlot` guard that jumps to the existing `HandleErrors` path (returns `False`) when the empty-slot scan comes up empty, instead of silently reusing a stale `nextBranch`. Build verified clean; Excel-side characterization check (U-238/Bi-214 output unchanged) still pending user confirmation. Commit `6b65245`.
- **Date recorded:** 2026-07-17
- **Date closed:** 2026-07-17

## DEBT-0007: Synchronous DNS query and modal dialogs run on the calculation thread

- **Location:** `WindowsDNSFunctions.vb` / `RadToolzFunctions.vb` (`RTZUpdate`, `RTZLicense`, error handlers).
- **Description:** `RTZUpdate`'s `DnsQuery` is synchronous on the calculation thread — offline or with a slow resolver, Excel blocks until the DNS timeout. Every workbook containing `=RTZUpdate()` also emits a `radtoolz.com` lookup on recalculation (a mild usage beacon). `RTZUpdate`, `RTZLicense`, and error handlers throughout raise modal MsgBoxes during calc.
- **Risk:** Informational.
- **Suggested remedy:** Consider async patterns (`ExcelAsyncUtil`) or restrict these calls to ribbon/menu invocation instead of worksheet calculation.
- **Date recorded:** 2026-07-17

## DEBT-0008: P/Invoke module hardening nits (dnsapi.dll)

- **Location:** `WindowsDNSFunctions.vb` (DNS TXT record parsing), around line 126/129.
- **Description:** Struct layout, flexible-array-member handling, and `DnsRecordListFree` cleanup were all verified correct (matches DDR-0007). Two nits remain: `Marshal.PtrToStringUni` can return `Nothing`, and the following line would throw a `NullReferenceException` (currently swallowed by a broad `Catch` — functional, but silent); and `txt.StartsWith(prefix)` is culture-sensitive.
- **Risk:** Informational.
- **Suggested remedy:** Add an explicit null check after `PtrToStringUni`; use `txt.StartsWith(prefix, StringComparison.Ordinal)`.
- **Status:** CLOSED 2026-07-17 — added an `IsNot Nothing` check before `StartsWith`, which now also passes `StringComparison.Ordinal`. Commit `c89241a`.
- **Date recorded:** 2026-07-17
- **Date closed:** 2026-07-17

## DEBT-0009: CI / supply-chain hardening refinements

- **Location:** `.github/workflows/securitycodescan.yml` (SecurityCodeScan gate).
- **Description:** The SecurityCodeScan gate is solid (fails build on any `SCS` warning; runs on push/PR to master+dev plus weekly cron), but three refinements were identified: (a) SecurityCodeScan's last upstream release was 2022 — still functional, but its rule set won't grow; consider adding `Microsoft.CodeAnalysis.NetAnalyzers` security rules (CA2xxx/CA3xxx/CA5xxx) or CodeQL as a second engine; (b) `setup-nuget` and `security-code-scan-add-action` are SHA-pinned but `actions/checkout@v7` and `microsoft/setup-msbuild@v3` are pinned by mutable tag — pin all four by SHA; (c) the workflow declares no `permissions:` block, so `GITHUB_TOKEN` gets the repository default — add `permissions: contents: read`.
- **Risk:** Informational.
- **Suggested remedy:** See description; three independent, low-effort workflow edits.
- **Status:** PARTIALLY CLOSED 2026-07-17 — (b) and (c) done: `actions/checkout` and `microsoft/setup-msbuild` are now SHA-pinned (all four actions in the job are SHA-pinned), and a workflow-level `permissions: contents: read` block was added. (a) — a second static-analysis engine — is split out as DEBT-0010 below, since it's a larger addition than a one-line edit.
- **Date recorded:** 2026-07-17
- **Date closed:** 2026-07-17 (partial — see DEBT-0010)

## DEBT-0010: Add a second static-analysis engine alongside SecurityCodeScan

- **Location:** `RadToolz/RadToolz.vbproj`, `RadToolz/packages.config`, `RadToolz/.editorconfig`, `.github/workflows/securitycodescan.yml`.
- **Description:** Split out from DEBT-0009(a). SecurityCodeScan's last upstream release was 2022 — it still works but its rule set won't grow. Consider adding `Microsoft.CodeAnalysis.NetAnalyzers` security rules (CA2xxx/CA3xxx/CA5xxx) or CodeQL as a second, actively-maintained engine.
- **Risk:** Informational.
- **Suggested remedy:** Evaluate NetAnalyzers (lower lift — an MSBuild analyzer package) vs. CodeQL (separate workflow/job, broader rule set) and add as a second gate, not a replacement for SecurityCodeScan.
- **Status:** CLOSED 2026-07-17 — Phase 1: `Microsoft.CodeAnalysis.NetAnalyzers` 10.0.302 added to `RadToolz.vbproj`/`packages.config`, scoped to suppress the codebase's currently-firing non-security rules (see DDR-0011); landed report-only first. Phase 2 (gating): `.github/workflows/securitycodescan.yml` now fails the `SCS` job's build on any of the 94 official NetAnalyzers Security-category rule IDs (explicit list, not a numeric range — the category's IDs aren't contiguous), reusing the existing `build.log` rather than a second build. Verified against a synthetic log (catches security findings, ignores non-security noise) and the real codebase (0 findings, gate lands green). Local dev builds remain report-only, matching how SecurityCodeScan itself is only gated in CI. A residual scoping gap (CA1502 vs. the legacy ruleset) was found during phase 1 and recorded separately as DEBT-0011 — not blocking, since CA1502 isn't a security rule.
- **Date recorded:** 2026-07-17
- **Date closed:** 2026-07-17

## DEBT-0011: CA1502 can't be suppressed via .editorconfig while JJPAnalysisRules.ruleset stays wired

- **Location:** `RadToolz/RadToolz.vbproj` (`CodeAnalysisRuleSet`), `JJPAnalysisRules.ruleset`, `RadToolz/.editorconfig`.
- **Description:** Discovered while scoping DEBT-0010's NetAnalyzers addition to security-only rules. `RadToolz.vbproj`'s `CodeAnalysisRuleSet` property is passed to the VB compiler via `/ruleset:` regardless of `RunCodeAnalysis=false` — a separate mechanism from the legacy FxCop-style analysis pass that flag actually controls. `JJPAnalysisRules.ruleset` explicitly sets `CA1502` (cyclomatic complexity) to `Action="Warning"` under its `Microsoft.Analyzers.ManagedCodeAnalysis` block, and that ruleset-level severity wins over any `.editorconfig` `dotnet_diagnostic.CA1502.severity` override. Also discovered: `dotnet_analyzer_diagnostic.category-<Name>.severity` bulk category overrides silently no-op on this toolchain (individual `dotnet_diagnostic.<ID>.severity` overrides work correctly), which is why DEBT-0010's scoping is done rule-by-rule rather than by category. Net effect: `CA1502` shows up as a build warning on 3 pre-existing, already-known-complex methods (`DCF`, `RadDecay`, `GetDecayChain` — the latter two already flagged in DEBT-0006's neighborhood) and cannot currently be silenced without editing the legacy ruleset file. The same ruleset file also contains an `SecurityCodeScan.VS2019` block (all `SCS0xxx` rules as `Action="Error"`) that DDR-0009 states is "exercised" by the CI SecurityCodeScan workflow — that block is very likely load-bearing for the CI security gate today, given the same `/ruleset:` passthrough behavior confirmed here, so any cleanup of this file must not touch it without separately verifying CI's actual behavior.
- **Risk:** Informational (build noise only; does not fail the build; not a security finding).
- **Suggested remedy:** Strip just the `Microsoft.Analyzers.ManagedCodeAnalysis` block from `JJPAnalysisRules.ruleset` (leaving the `SecurityCodeScan.VS2019` block untouched), after first verifying in CI (not just inferring) whether that SCS block is actually load-bearing there. Treat as its own task with its own DDR, since it touches CI-security-relevant config.
- **Status:** CLOSED 2026-07-17 (DDR-0013) — the SCS block's CI relevance was verified for real first (DDR-0012: it was found to be load-bearing, and non-functional until fixed). With that confirmed, the `Microsoft.Analyzers.ManagedCodeAnalysis` block was removed from `JJPAnalysisRules.ruleset` in full; `SecurityCodeScan.VS2019` and the `Microsoft.CodeAnalysis.Analyzers` (RS10xx) blocks are untouched. `CA1502` no longer fires. Isolated testing showed the ruleset block only fully explains 5 of DEBT-0010's 9 suppressed rule IDs (`CA1502`, `CA1024`, `CA1031`, `CA1045`, `CA1060`); `CA1305`/`CA1707`/`CA1822`/`CA2263` are independently required regardless of the ruleset, so `RadToolz/.editorconfig`'s full 9-rule suppression list was kept intact rather than trimmed to an uncertain subset. Verified via a clean full-solution rebuild (0 warnings, both XLL bitness targets pack).
- **Date recorded:** 2026-07-17
- **Date closed:** 2026-07-17
