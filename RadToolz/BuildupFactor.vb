Imports ExcelDna.Integration

Public Module BuildupFactorFunctions

    <ExcelFunction(Description:="Return the gamma-ray buildup factor (point-isotropic source, infinite medium) for a shield material, photon energy, and shield thickness.", Category:="RadToolz")>
    Public Function BuildupFactor(
        <ExcelArgument(Name:="Energy", Description:="Gamma energy in MeV. Valid range 0.015-15 MeV (Lead: 0.03-15 MeV).")>
        Energy As Double,
        <ExcelArgument(Name:="Material", Description:="Shield material: Air, Water, Iron (Fe), Lead (Pb), or Concrete")>
        Material As String,
        <ExcelArgument(Name:="Mean Free Paths", Description:="Shield thickness in mean free paths (mfp). Valid range 0-40; 0 returns 1 exactly.")>
        MeanFreePaths As Double,
        <ExcelArgument(Name:="(optional) Buildup Type", Description:="Exposure (EBF, default) or Absorption (EABF). Accepts Exposure/EBF/E or Absorption/EABF/A.")>
        Optional BuildupType As String = "Exposure") _
        As Object
        '* Usage:       Calculates the gamma-ray buildup factor for a
        '*              point-isotropic source in an infinite homogeneous
        '*              medium, using the ANSI/ANS-6.4.3-1991 Geometric
        '*              Progression (G-P) fitting method (Harima).
        '* Input:       Photon energy (MeV), shield material, shield
        '*              thickness (mean free paths), optional buildup type
        '* Returns:     Dimensionless buildup factor, or a descriptive
        '*              error string for invalid input (never throws
        '*              across the UDF boundary).
        '* Notes:       DDR-0017. G-P coefficients (B, C, A, Xk, D per
        '*              material/type/energy) are transcribed from
        '*              JAERI-M 90-110 (Sakamoto and Tanaka, 1990),
        '*              Appendix D - the coefficient database the
        '*              ANS-6.4.3-1991 standard itself is built from; see
        '*              RTZRefs and DDR-0017 for exact table/page
        '*              citations. Formula (source eq. 6-7):
        '*                B(x) = 1 + (b-1)*(K^x - 1)/(K-1)   for K != 1
        '*                B(x) = 1 + (b-1)*x                  for K = 1
        '*                K(x) = c*x^a + d*(tanh(x/Xk-2)-tanh(-2))/(1-tanh(-2))
        '*              where x is mfp and b,c,a,Xk,d are the coefficients
        '*              at a given (material, type, energy). Energies
        '*              between the standard 25-point grid are found by
        '*              evaluating B at the two bracketing grid energies
        '*              and log-log interpolating (ln(B) vs ln(E)); an
        '*              exact grid-energy hit bypasses interpolation
        '*              entirely. mfp between 0 and 0.5 is accepted but is
        '*              a mild extrapolation of the fit (the source data
        '*              is tabulated from 0.5 mfp).
        '*              Lead has no valid G-P fit at 0.015/0.020 MeV (the
        '*              source coefficients are all exactly zero there -
        '*              not a small/rounding value, an absent fit) so
        '*              Lead's accepted energy floor is 0.03 MeV, unlike
        '*              every other material's 0.015 MeV floor.
        '*              "Exposure"/"EBF"/"E" select the air-response G-P
        '*              coefficients; "Absorption"/"EABF"/"A" select the
        '*              medium's own (self-response) coefficients - see
        '*              DDR-0017 for why AIR RESPONSE = exposure buildup.
        '*              For Material="Air", both buildup types resolve to
        '*              the same coefficients (medium = response for air
        '*              by construction), so EBF and EABF are identical
        '*              for Air - a real property of the source data, not
        '*              a bug.
        '*              IsThreadSafe:=False, matching every other RadToolz
        '*              UDF. This function is pure computation with no
        '*              shared state, so it is a candidate for
        '*              IsThreadSafe:=True - recorded as debt rather than
        '*              taken now (DDR-0017), to keep this change
        '*              consistent with the rest of the exported surface.

        Try
            Dim resolvedMaterial As String = Nothing
            Dim resolvedType As String = Nothing

            If Not TryResolveMaterial(Material, resolvedMaterial) Then
                Return "Error: Invalid material. Must be Air, Water, Iron, Lead, or Concrete."
            End If

            If Not TryResolveBuildupType(BuildupType, resolvedType) Then
                Return "Error: Invalid buildup type. Must be Exposure (EBF) or Absorption (EABF)."
            End If

            If resolvedMaterial = "Lead" Then
                If Energy < 0.03 OrElse Energy > 15.0 Then
                    Return "Error: Energy out of range for Lead. Must be between 0.03 and 15 MeV."
                End If
            Else
                If Energy < 0.015 OrElse Energy > 15.0 Then
                    Return "Error: Energy out of range. Must be between 0.015 and 15 MeV."
                End If
            End If

            If MeanFreePaths = 0.0 Then
                Return 1.0
            End If

            If MeanFreePaths < 0.0 OrElse MeanFreePaths > 40.0 Then
                Return "Error: Mean free paths out of range. Must be between 0 and 40."
            End If

            Dim rows As IReadOnlyList(Of BuildupFactorItem) = BuildupFactorRepository.GetRows(resolvedMaterial, resolvedType)

            If rows Is Nothing OrElse rows.Count = 0 Then
                Return "Error: No buildup factor data available for " & resolvedMaterial & " (" & resolvedType & ")."
            End If

            Return EvaluateBuildupFactor(rows, Energy, MeanFreePaths)

        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try

    End Function

    Private Function TryResolveMaterial(material As String, ByRef resolved As String) As Boolean
        '* Usage:       Maps a Material argument (including the aliases
        '*              locked in the design: Fe, Pb, H2O) to the
        '*              canonical name used as the data lookup key.

        If material Is Nothing Then Return False

        Select Case material.Trim().ToUpperInvariant()
            Case "AIR"
                resolved = "Air"
            Case "WATER", "H2O"
                resolved = "Water"
            Case "IRON", "FE"
                resolved = "Iron"
            Case "LEAD", "PB"
                resolved = "Lead"
            Case "CONCRETE"
                resolved = "Concrete"
            Case Else
                Return False
        End Select

        Return True
    End Function

    Private Function TryResolveBuildupType(buildupType As String, ByRef resolved As String) As Boolean
        '* Usage:       Maps a BuildupType argument to the canonical
        '*              "Exposure"/"Absorption" data lookup key.

        If buildupType Is Nothing Then Return False

        Select Case buildupType.Trim().ToUpperInvariant()
            Case "EXPOSURE", "EBF", "E"
                resolved = "Exposure"
            Case "ABSORPTION", "EABF", "A"
                resolved = "Absorption"
            Case Else
                Return False
        End Select

        Return True
    End Function

    Private Function EvaluateBuildupFactor(rows As IReadOnlyList(Of BuildupFactorItem), energy As Double, mfp As Double) As Object
        '* Usage:       Locates the grid row (exact hit) or bracketing
        '*              pair (interpolated) for the given energy, then
        '*              returns the G-P buildup factor - or a descriptive
        '*              error string if the result is not a valid number.
        '* Notes:       Energy is validated by the caller to lie within
        '*              [rows(0).Energy, rows(rows.Count-1).Energy], so a
        '*              bracketing pair always exists once no exact grid
        '*              hit is found.

        Const EnergyTolerance As Double = 0.0000001

        Dim lowerIndex As Integer = -1

        For i As Integer = 0 To rows.Count - 1
            If Math.Abs(rows(i).Energy - energy) <= EnergyTolerance Then
                Return ToBuildupFactorResult(EvaluateGPFormula(rows(i), mfp))
            End If

            If rows(i).Energy < energy Then lowerIndex = i
        Next

        Dim lower As BuildupFactorItem = rows(lowerIndex)
        Dim upper As BuildupFactorItem = rows(lowerIndex + 1)

        Dim buildupLower As Double = EvaluateGPFormula(lower, mfp)
        Dim buildupUpper As Double = EvaluateGPFormula(upper, mfp)

        If buildupLower <= 0.0 OrElse buildupUpper <= 0.0 Then
            Return "Error: Buildup factor could not be computed for this input combination."
        End If

        Dim logEnergy As Double = Math.Log(energy)
        Dim logLower As Double = Math.Log(lower.Energy)
        Dim logUpper As Double = Math.Log(upper.Energy)
        Dim logBuildupLower As Double = Math.Log(buildupLower)
        Dim logBuildupUpper As Double = Math.Log(buildupUpper)

        Dim interpolatedLogBuildup As Double = logBuildupLower +
            (logBuildupUpper - logBuildupLower) * (logEnergy - logLower) / (logUpper - logLower)

        Return ToBuildupFactorResult(Math.Exp(interpolatedLogBuildup))
    End Function

    Private Function EvaluateGPFormula(row As BuildupFactorItem, x As Double) As Double
        '* Usage:       G-P formula (source eq. 6-7 - see BuildupFactor's
        '*              Notes for the full citation).

        Dim tanhNegativeTwo As Double = Math.Tanh(-2.0)

        Dim k As Double = row.C * (x ^ row.A) +
            row.D * (Math.Tanh(x / row.Xk - 2.0) - tanhNegativeTwo) / (1.0 - tanhNegativeTwo)

        If Math.Abs(k - 1.0) < 0.000000001 Then
            Return 1.0 + (row.B - 1.0) * x
        End If

        Return 1.0 + (row.B - 1.0) * (Math.Pow(k, x) - 1.0) / (k - 1.0)
    End Function

    Private Function ToBuildupFactorResult(value As Double) As Object
        If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then
            Return "Error: Buildup factor could not be computed for this input combination (outside the fit's valid domain)."
        End If

        Return value
    End Function

End Module
