Imports ExcelDna.Integration

' Solution-wide constants shared by every module in the add-in (ProcessDecaySeries,
' RadToolzFunctions, DecaySeriesRepository). Friend scope: internal to RadToolz.dll,
' not part of the exported UDF surface.
Friend Module Constants

    '* Author:      Backscatter enterprises
    '* Date:        7/8/2026

    ' RTZVers()/RTZUpdate() (RadToolzFunctions.vb) combine these two into the
    ' displayed version string and compare RadToolzVersion against the DNS TXT
    ' record published at radtoolz.com to detect available updates.
    Public Const RadToolzVersion As Double = 5.0

    Public Const RadToolzPreRelease As String = "RC3" 'α ß Γ π Σ σ µ

    ' Must exceed the largest number of distinct decay-chain branches
    ' any single starting isotope's full tree can fork into. The full
    ' ENSDF-sourced table (unlike the old 182-isotope table) records
    ' isotopes with more than two decay modes - e.g. Bi-214 has three -
    ' so a chain like U-238's now forks into ~70 branches before
    ' GetDecayChain prunes back to the ones ending at the requested
    ' terminal member. ProcessDecaySeries.GetDecayChain sizes its gdcdci()
    ' branch array to this value; raise it if a future table addition ever
    ' produces a wider fork than U-238's ~70 branches.
    Public Const maxBranches As Integer = 150

    ' Cached handle to the host Excel.Application, resolved once at module load via
    ' ExcelDnaUtil.Application. Used (as Object, i.e. late-bound) wherever code needs
    ' the COM object model directly - e.g. ProcessDecaySeries.ListAll
    ' and RadToolzFunctions.RTZFunctions/RTZParams resolving a user-supplied range
    ' reference. This is the add-in's own in-process Application RCW: per
    ' section_16_com_and_memory_lifetime_rules, it must never be passed to
    ' Marshal.ReleaseComObject.
    Public iExcel As Object = ExcelDnaUtil.Application

    'Conversion factors
    ' rem -> Sievert (SI dose unit). Used by DCF()/PECi() when their SI argument
    ' requests SI output instead of the default rem/uCi convention.
    Public Const rem2Sv As Double = 0.01

    ' Curie -> Becquerel (SI activity unit). Used alongside rem2Sv for SI unit
    ' conversions in DCF()/AValue()/SpA().
    Public Const Ci2Bq As Double = 37000000000.0

End Module