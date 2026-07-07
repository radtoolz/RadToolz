Imports ExcelDna.Integration

Friend Module Constants
    '* Author:      Backscatter enterprises
    '* Date:        1/25/2025

    Public Const RadToolzVersion As Double = 5.0
    Public Const RadToolzPreRelease As String = "ß" 'α ß Γ π Σ σ µ
    ' Must exceed the largest number of distinct decay-chain branches
    ' any single starting isotope's full tree can fork into. The full
    ' ENSDF-sourced table (unlike the old 182-isotope table) records
    ' isotopes with more than two decay modes - e.g. Bi-214 has three -
    ' so a chain like U-238's now forks into ~70 branches before
    ' GetDecayChain prunes back to the ones ending at the requested
    ' terminal member.
    Public Const maxBranches As Integer = 150
    Public iExcel As Object = ExcelDnaUtil.Application

    'Conversion factors
    Public Const rem2Sv As Double = 0.01

    Public Const Ci2Bq As Double = 37000000000.0

End Module