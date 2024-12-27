﻿Imports System
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Excel.Constants
Imports ExcelDna.Integration
Imports ExcelDna.Integration.XlCall
Imports ExcelDna.Integration.XlCallException
Imports ExcelDna.Integration.ExcelIntegration
Imports System.Text.RegularExpressions

<Assembly: CLSCompliant(True)> 

Public Module MyFunctions
    <ExcelFunction(Description:="Return dose conversion factor (rem/uCi) for inhalation or ingestion", Category:="RadToolz")> _
    Public Function DCF( _
        <ExcelArgument(Name:="Radionuclide", Description:="Radionuclide of interest (e.g. Cs-137)")> _
        Isotope As String, _
        <ExcelArgument(Name:="DCF Standard", Description:="ICRP-68 values (i.e., 68), or ICRP-72 (i.e., 72)")> _
        DCFStd As String, _
        <ExcelArgument(Name:="Pathway", Description:="[INH]alation or [ING]estion")> _
        DCFPath As String, _
        <ExcelArgument(Name:="(optional) Lung Absorption Type", Description:="[S]low, [M]oderate, or [F]ast absorption, default is maximum")> _
        Optional DCFType As String = "X", _
        <ExcelArgument(Name:="(optional) INH AMAD for ICRP 68", Description:="1 or 5 micron, default is maximum.  For ICRP-72, reverts to 1 micron in all cases.")> _
        Optional DCFAMAD As String = "9") _
        As Object
        '* Usage:       Lookup dose conversion factor for Isotope
        '* Input:       Isotope (e.g., Cs-137)
        '*              DCF Standard (e.g. 68 or 72)
        '*              Pathway (e.g., INH or ING)
        '*              optional Absorption Type (e.g. S, M, F), defaults to maximum value
        '*              AMAD (e.g. 1 or 5), defaults to maximum value
        '* Returns:     either inhalation or ingestion dose conversion factor (rem/uCi)
        '* Author:      J. J. Prowse
        '* Date:        4/15/2016

        'Variables
        Dim pds As New ProcessDecaySeries
        Dim bRsp As Boolean
        Dim Msg As String
        Dim DCFTemp As String
        Dim cDC(0 To maxBranches) As Collection
        Dim S1 As Double, S5 As Double, M1 As Double, M5 As Double, F1 As Double, F5 As Double

        'Assume it goes bad
        DCF = "#N/A"

        'fix for default values are not being stored in the function variable
        If Trim(DCFType) = "" Or IsNothing(DCFType) Then DCFType = "X"
        If Trim(DCFAMAD) = "" Or IsNothing(DCFType) Then DCFAMAD = "9"

        'Sanitize Input
        Isotope = (UCase(Isotope))
        DCFStd = Left(UCase(DirectCast(DCFStd, String)), 2)
        DCFPath = Left(UCase(DCFPath), 3)
        DCFType = Left(UCase(DCFType), 1)
        DCFAMAD = Left(UCase(DirectCast(DCFAMAD, String)), 1)

        Select Case DCFStd
            Case "68"
            Case "72"
                DCFAMAD = "1" 'ICRP-72 only has AMAD 1 micron
            Case Else
                DCF = "Invalid DCF Standard (68|72)"
                GoTo ExitHere
        End Select

        Select Case DCFPath
            Case "INH"
            Case "ING"
            Case Else
                DCF = "Invalid pathway (INH|ING)"
                GoTo ExitHere
        End Select

        Select Case DCFType
            Case "F"
            Case "M"
            Case "S"
            Case "X"
            Case Else
                DCF = "Invalid Absorption Type (S|M|F)"
                GoTo ExitHere
        End Select

        Select Case DCFAMAD
            Case "1"
            Case "5"
            Case "9"
            Case Else
                DCF = "Invalid AMAD (1|5)"
                GoTo ExitHere
        End Select

        If Not pds.VerifyIsotope(Isotope) Then
            DCF = "Invalid Radionuclide"
            GoTo ExitHere
        End If

        '*Assert:  Input parameters are all valid

        'Load decay chain
        bRsp = pds.InitBranches(cDC)
        If Not bRsp Then GoTo HandleErrors

        'Get Info for Isotope of Interest
        bRsp = pds.GetDecayChain(Isotope, Isotope, cDC)
        If Not bRsp Then '
            DCF = "#N/A"
            GoTo ExitHere
        End If

        bRsp = pds.VerifyDecayChain(Isotope, Isotope, cDC(1))
        If Not bRsp Then
            DCF = "#N/A"
            GoTo ExitHere
        End If

        'Do Ingestion Cases
        If DCFPath = "ING" Then
            If DCFStd = "68" Then
                DCF = DirectCast(cDC(1).Item(1).DCF68ing, Double)
                GoTo ExitHere
            Else 'DCFStd = 72
                DCF = DirectCast(cDC(1).Item(1).DCF72ing, Double)
                GoTo ExitHere
            End If
        End If

        '*Assert:  lookging for INH
        'Load the DCF's by ICRP
        Select Case DCFStd
            Case "68"
                S1 = DirectCast(cDC(1).Item(1).DCF68inhS1, Double)
                S5 = DirectCast(cDC(1).Item(1).DCF68inhS5, Double)
                M1 = DirectCast(cDC(1).Item(1).DCF68inhM1, Double)
                M5 = DirectCast(cDC(1).Item(1).DCF68inhM5, Double)
                F1 = DirectCast(cDC(1).Item(1).DCF68inhF1, Double)
                F5 = DirectCast(cDC(1).Item(1).DCF68inhF5, Double)
            Case "72"
                S1 = DirectCast(cDC(1).Item(1).DCF72inhS1, Double)
                M1 = DirectCast(cDC(1).Item(1).DCF72inhM1, Double)
                F1 = DirectCast(cDC(1).Item(1).DCF72inhF1, Double)
                S5 = 0.0#
                M5 = 0.0#
                F5 = 0.0#
            Case Else 'should NEVER get to this
                DCF = "#N/A"
                GoTo ExitHere
        End Select

        'Is this a max case? 
        If DCFType = "X" And DCFAMAD = "9" Then 'this will return the highest DCF, already know whether 68 or 72
            Dim doubles As New List(Of Double)(New Double() {S1, S5, M1, M5, F1, F5})
            DCF = doubles.Max()
            GoTo ExitHere
        End If

        If DCFType = "X" Then
            'find max of Type for given AMAD
            Select Case DCFAMAD
                Case "1"
                    Dim doubles As New List(Of Double)(New Double() {S1, M1, F1})
                    DCF = doubles.Max
                    GoTo ExitHere
                Case "5"
                    Dim doubles As New List(Of Double)(New Double() {S5, M5, F5})
                    DCF = doubles.Max
                    GoTo ExitHere
                Case Else 'should NEVER get to this
                    DCF = "#N/A"
                    GoTo ExitHere
            End Select

        End If

        If DCFAMAD = "9" Then
            Select Case DCFType
                Case "S"
                    Dim doubles As New List(Of Double)(New Double() {S1, S5})
                    DCF = doubles.Max
                    GoTo ExitHere
                Case "M"
                    Dim doubles As New List(Of Double)(New Double() {M1, M5})
                    DCF = doubles.Max
                    GoTo ExitHere
                Case "F"
                    Dim doubles As New List(Of Double)(New Double() {F1, F5})
                    DCF = doubles.Max
                    GoTo ExitHere
                Case Else 'should NEVER get to this
                    DCF = "#N/A"
                    GoTo ExitHere
            End Select
        End If

        'Regular Case
        DCFTemp = DCFType & DCFAMAD

        Select Case DCFTemp
            Case "S1"
                DCF = S1
            Case "S5"
                DCF = S5
            Case "M1"
                DCF = M1
            Case "M5"
                DCF = M5
            Case "F1"
                DCF = F1
            Case "F5"
                DCF = F5
            Case Else 'should NEVER get to this

        End Select

ExitHere:
        If IsNothing(pds) Then Exit Function 'aborted due to bad user input

        'clean-up
        bRsp = pds.ClearBranches(cDC)

        If Not bRsp Then GoTo HandleErrors
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, vbCritical, "Error")
        End If

        DCF = False

    End Function 'DCF

    <ExcelFunction(Description:="Enumerate a decay chain", Category:="RadToolz")> _
    Public Function EnumDecayChain( _
        <ExcelArgument(Name:="Starting Member", Description:="First radionuclide of the decay chain (e.g., U-238)")> _
        StartingMember As String, _
        <ExcelArgument(Name:="Member Number", Description:="Number of the radionuclide in the decay chain based on the sort order (e.g., 1 = U-238)")> _
        Member As Double, _
        <ExcelArgument(Name:="(Optional) Sort Order", Description:="Order to list members; 1 = no sort (default), 2 = increasing mass, or 3 = decreasing mass")> _
        Optional OptionalSortOrder As Integer = 1) _
        As Object
        '* Usage:       Populates cell with decay chain member text
        '* Input:       StartingMember - first member of serial decay chain (e.g., U-238)
        '*              Member - number of member in chain
        '* Returns:     Decay chain member value
        '* Author:      J. J. Prowse
        '* Date:        12/25/2014

        'Variables
        Dim n As Double
        Dim pds As New ProcessDecaySeries
        Dim bRsp As Boolean
        Dim Msg As String
        Dim cDC(0 To maxBranches) As Collection

        If OptionalSortOrder = 0 Then OptionalSortOrder = 1

        If Not pds.VerifyIsotope(UCase(StartingMember)) Then
            EnumDecayChain = "StartingMember is not available"
            GoTo ExitHere
        End If

        If Member <= 0 Then
            EnumDecayChain = "Member number not valid"
            GoTo ExitHere
        End If

        'Load decay chain
        pds = New ProcessDecaySeries
        bRsp = pds.InitBranches(cDC)
        If Not bRsp Then GoTo HandleErrors

        bRsp = pds.GetDecayChain(StartingMember, "END", cDC, , , , , OptionalSortOrder)
        If Not bRsp Then '
            EnumDecayChain = "Radioisotope not found"
            GoTo ExitHere
        End If

        n = cDC(0).Count

        If n >= Member Then
            EnumDecayChain = DirectCast(cDC(0).Item(Member).Isotope, String)
        Else
            EnumDecayChain = "Member number exceeds members in decay chain"
        End If

ExitHere:
        bRsp = pds.ClearBranches(cDC)
        If Not bRsp Then GoTo HandleErrors
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, vbCritical, "Error")
        End If

        EnumDecayChain = False

    End Function 'EnumDecayChain
    <ExcelFunction(Description:="Calculates U-235 or Pu-239 fissile gram equivalent", Category:="RadToolz")> _
    Public Function FGE( _
        <ExcelArgument(Name:="Radionuclide", Description:="Fissile radionuclide of interest (e.g., Pu-241)")> _
        Radionuclide As String, _
        <ExcelArgument(Name:="Activity", Description:="Curies of radionuclide")> _
        Activity As Double, _
        <ExcelArgument(Name:="(Optional) Equivalence Basis", Description:="Either U-235 or Pu-239 equivalence (i.e., U-235 or Pu-239 (default))")> _
        Optional Basis As String = "P") _
        As Object
        '* Usage:       Calcualte FGE
        '* Input:       Radionuclide (e.g., Pu-241)
        '*              Activity, curies of radionuclide
        '*              optional Basis, basis of equivalence
        '* Returns:     FGE in grams
        '* Author:      J. J. Prowse
        '* Date:        4/15/2016

        On Error GoTo HandleErrors

        'Variables
        Dim N As Double
        Dim D As Double
        Dim Msg As String
        Dim uSpA As Object

        'Kludge because optional parameter is not being set for no entry
        If Basis = "" Or IsNothing(Basis) Then
            Basis = "P"
        End If

        'Sanitize Basis
        If IsNumeric(Basis) Then
            FGE = "Basis must be either U-235 or Pu-239"
            Exit Function
        End If

        Basis = Left(UCase(Basis), 1) ' make it P or U

        Select Case Basis
            Case "P"
                N = 450
            Case "U"
                N = 700
            Case Else
                FGE = "Basis must be either U-235 or Pu-239"
                Exit Function
        End Select

        'Get radionuclide minimum fissile mass based on ANSI 8.1 or 8.15
        Radionuclide = UCase(Radionuclide)
        Select Case Radionuclide
            Case "AM-242M"
                D = 13
            Case "CF-249"
                D = 10
            Case "CF-251"
                D = 5
            Case "CM-243"
                D = 90
            Case "CM-245"
                D = 30
            Case "CM-247"
                D = 900
            Case "PU-239"
                D = 450
            Case "PU-241"
                D = 200
            Case "U-233"
                D = 500
            Case "U-235"
                D = 700
            Case Else
                FGE = 0 'traps non-fissile input
                Exit Function
        End Select
        'Sanitize Radionuclide
        uSpA = SpA(Radionuclide)
        If IsNumeric(uSpA) Then
            uSpA = DirectCast(uSpA, Double)
        Else
            FGE = uSpA 'return SpA() error msg
            Exit Function
        End If

        'Santize Acitivty
        If Not IsNumeric(Activity) Then
            FGE = "Activity is not a number"
            Exit Function
        End If

        FGE = (N / D) * (Activity / uSpA)

        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, vbCritical, "Error")
        End If

        FGE = 0

    End Function


    <ExcelFunction(Description:="Half life in specified time for a given radionuclide", Category:="RadToolz")> _
    Public Function HalfLife( _
        <ExcelArgument(Name:="Radionuclide", Description:="Radionuclide of interest (e.g., U-238)")> _
        Isotope As String, _
        <ExcelArgument(Name:="(Optional) Time Unit", Description:="Units to report half life (i.e., [S]econds, [M]inutes, [H]ours, or [Y]ears (default)")> _
        Optional TimeUnit As String = "Y") _
        As Object
        '* Usage:       Lookup half-life for Isotope
        '* Input:       Isotope (e.g., Cs-137)
        '*              TimeUnit either S, M, D, Y
        '* Returns:     Half-life in time unit
        '* Author:      J. J. Prowse
        '* Date:        8/3/2015

        'Variables
        Dim k As Double
        Dim pds As New ProcessDecaySeries
        Dim bRsp As Boolean
        Dim Msg As String
        Dim cDC(0 To maxBranches) As Collection

        If TimeUnit = "" Then TimeUnit = "Y" 'Required for ExcelDNA

        TimeUnit = UCase(Left(TimeUnit, 1))
        Isotope = UCase(Isotope)

        If Not pds.VerifyIsotope(Isotope) Then
            HalfLife = "Invalid Radionuclide"
            GoTo ExitHere
        End If

        'Load decay chain
        bRsp = pds.InitBranches(cDC)
        If Not bRsp Then GoTo HandleErrors

        bRsp = pds.GetDecayChain(Isotope, Isotope, cDC)
        If Not bRsp Then
            HalfLife = "Radionuclide not found"
            GoTo ExitHere
        End If

        'Calculate time conversion factor
        k = 1
        Select Case TimeUnit
            Case "S"
                k = 1
            Case "M"
                k = k / 60
            Case "H"
                k = k / 60 / 60
            Case "D"
                k = k / 60 / 60 / 24
            Case "Y"
                k = k / 60 / 60 / 24 / 365.25
            Case Else
                HalfLife = "Invalid Time unit (S|M|H|D|Y)"
                GoTo ExitHere
        End Select

        HalfLife = k * Math.Log(2) / DirectCast(cDC(1).Item(1).Lambda, Double)

ExitHere:
        bRsp = pds.ClearBranches(cDC) 'clean-up memory
        If Not bRsp Then GoTo HandleErrors
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, vbCritical, "Error")
        End If

        HalfLife = False

    End Function 'HalfLife

    <ExcelFunction(Description:="Calculates the plutonium equivalent curies (PE-Ci) in the activity units for a radionuclide based on either ICRP-68 or -72", Category:="RadToolz")> _
    Public Function PECi( _
        <ExcelArgument(Name:="Radionuclide", Description:="Radionuclide of interest (e.g. Cs-137)")> _
        Isotope As String, _
        <ExcelArgument(Name:="Activity", Description:="Activity (e.g. Ci)")> _
        Activity As Double, _
        <ExcelArgument(Name:="DCF Standard", Description:="ICRP-68 values (i.e., 68), or ICRP-72 (i.e., 72)")> _
        DCFStd As String, _
        <ExcelArgument(Name:="(optional) Lung Absorption Type", Description:="[S]low, [M]oderate, or [F]ast absorption, default is maximum")> _
        Optional DCFType As String = "X", _
        <ExcelArgument(Name:="(optional) INH AMAD for ICRP 68", Description:="1 or 5 micron, default is maximum")> _
        Optional DCFAMAD As String = "9") _
        As Object
        '* Usage:       Calculates the PE-Ci
        '* Input:       Isotope (e.g., Cs-137)
        '*              Activity
        '*              DCF Standard (e.g. 68 or 72)
        '*              optional Absorption Type (e.g. S, M, F), defaults to maximum value
        '*              AMAD (e.g. 1 or 5), defaults to maximum value
        '* Returns:     PE-Ci of Isotope
        '* Author:      J. J. Prowse
        '* Date:        4/2/2016

        'Variables
        'Dim pds As New ProcessDecaySeries
        'Dim bRsp As Boolean
        Dim PuDCF As Object
        Dim uIsoDCF As Object
        'Dim DCFTemp As String
        Dim DCFPath As String
        'Dim DCFAct As Double
        'Dim cDC(0 To maxBranches) As Collection
        'Dim S1 As Double, S5 As Double, M1 As Double, M5 As Double, F1 As Double, F5 As Double

        'Assume it goes bad
        PECi = "#N/A"

        'fix for default values are not being stored in the function variable
        If Trim(DCFType) = "" Or IsNothing(DCFType) Then DCFType = "X"
        If Trim(DCFAMAD) = "" Or IsNothing(DCFType) Then DCFAMAD = "9"

        'Sanitize Input
        Isotope = (UCase(Isotope))
        DCFStd = Left(UCase(DirectCast(DCFStd, String)), 2)
        DCFPath = "INH"
        DCFType = Left(UCase(DCFType), 1)
        DCFAMAD = Left(UCase(DirectCast(DCFAMAD, String)), 1)

        'Use DCF to validate input parameters
        uIsoDCF = DCF(Isotope, DCFStd, DCFPath, DCFType, DCFAMAD)
        If Not IsNumeric(uIsoDCF) Then GoTo ExitHere 'trap for string response
        'bRsp = DirectCast(uIsoDCF, Boolean)
        'MsgBox(bRsp)
        'If Not bRsp Then 'bad info for DCF
        ' GoTo ExitHere
        'End If

        '*Assert:  Input parameters are all valid

        'Get Pu-239 DCF
        PuDCF = DCF("PU-239", DCFStd, DCFPath, DCFType, DCFAMAD)
        If Not IsNumeric(PuDCF) Then GoTo ExitHere 'trap for string response
        If DirectCast(PuDCF, Double) = 0 Then GoTo ExitHere 'trap for div 0


        'Calculate PE-Ci for Isotope
        PECi = Activity * DirectCast(uIsoDCF, Double) / DirectCast(PuDCF, Double)

ExitHere:

        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            MsgBox("Error # " & Str(Err.Number) & " was generated by " _
                & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description, vbCritical, "Error")
        End If

        PECi = "#N/A"

    End Function 'PECi

    <ExcelFunction(Description:="Decay and in-growth of an isotope from a decay chain using the Bateman equation", Category:="RadToolz", IsMacroType:=False)> _
    Public Function RadDecay( _
        <ExcelArgument(Name:="Starting Member", Description:="First isotope of the decay chain (e.g., U-238)")> _
        StartingMember As String, _
        <ExcelArgument(Name:="Terminal Member", Description:="Last isotope of the decay chain (e.g., Ra-226).  Reported value will be for this isotope")> _
        TerminalMember As String, _
        <ExcelArgument(Name:="(Optional) Starting Activity", Description:="Starting activity of first isotope in decay chain (default = 0)")> _
        Optional A0 As Double = 0, _
        <ExcelArgument(Name:="(Optional) Decay Time", Description:="Time period of decay (default = 1)")> _
        Optional DecayTime As Double = 0, _
        <ExcelArgument(Name:="(Optional) Time Unit", Description:="[S]econds (default), [M]inutes, [H]hours, or [Y]ears")> _
        Optional TimeUnit As String = "Sec") _
        As Object
        '* Usage:       Calculates the activity of the terminal member after decay
        '* Input:       StartingMember - first member of serial decay chain (e.g., U-238)
        '*              TerminalMember - member of serial decay chain to return activity (e.g., Th-230)
        '*              A0 - initial activity of StartingMember
        '*              DecayTime - time of decay in units of TimeUnit, default is seconds
        '*              TimeUnit - Seconds, Minutes, Hours, Days, or Years, default is seconds
        '* Returns:     Activity of TerminalMember after DecayTime, assuming 0 initial activity
        '* Author:      J. J. Prowse
        '* Date:        12/25/2014

        'Variables
        Dim i As Double
        Dim j As Double
        Dim n As Double
        Dim Branch As Integer
        Dim numBranches As Integer
        Dim x As Integer
        Dim k As Double
        Dim a As Double 'alpha
        Dim b As Double 'beta
        Dim BR As Double 'branching ratio
        Dim N0 As Double 'initial atoms of StartingMember
        Dim Nnt As Double
        Dim Lambda As Double
        Dim deltaLambda As Double
        Dim delta As Double
        Dim pds As New ProcessDecaySeries
        Dim bRsp As Boolean
        Dim Msg As String
        Dim cDC(0 To maxBranches) As Collection

        'Check for missing defaults (ExcelDNA fix)
        If TimeUnit = "" Then TimeUnit = "S"

        'Upper input strings
        StartingMember = UCase(StartingMember)
        TerminalMember = UCase(TerminalMember)

        If StartingMember = "" Or TerminalMember = "" Then
            Return ExcelError.ExcelErrorValue
        End If

        If Not pds.VerifyIsotope(StartingMember) Then
            RadDecay = "Invalid Starting Member"
            GoTo ExitHere
        End If

        If Not pds.VerifyIsotope(TerminalMember) Then
            RadDecay = "Invalid Terminal Member"
            GoTo ExitHere
        End If

        'Convert DecayTime to seconds
        TimeUnit = UCase(Left(TimeUnit, 1))

        k = 1.0#
        Select Case TimeUnit
            Case "S"
                k = 1.0#
            Case "M"
                k = 60.0#
            Case "H"
                k = (60.0# * 60.0#)
            Case "D"
                k = (60.0# * 60.0# * 24.0#)
            Case "Y"
                k = (60.0# * 60.0# * 24.0# * 365.25)
            Case Else
                RadDecay = "Invalid Time unit (S|M|H|D|Y)"
                GoTo ExitHere
        End Select
        DecayTime = DecayTime * k
        '*Assert: DecayTime is now in seconds

        'Load decay chain
        pds = New ProcessDecaySeries
        bRsp = pds.InitBranches(cDC)
        If Not bRsp Then GoTo HandleErrors

        bRsp = pds.GetDecayChain(StartingMember, TerminalMember, cDC)
        If Not bRsp Then '
            RadDecay = "Radionuclide not found"
            GoTo ExitHere
        End If

        'Calculate N0
        N0 = A0 / DirectCast(cDC(0).Item(1).Lambda, Double) 'all branches have the same parent
        Nnt = 0

        'Loop for each branch
        For x = 1 To maxBranches
            'Branches are contiguous
            If Not cDC(x) Is Nothing Then
                If cDC(x).Count <> 0 Then numBranches = x
            End If
        Next x

        For Branch = 1 To numBranches 'Branch(0) reserved for consolidated isotope list
            'Determine if branch ends at terminal
            If DirectCast(cDC(Branch).Item(cDC(Branch).Count).Isotope, String) <> TerminalMember Then
                cDC(Branch) = Nothing
                GoTo NextBranch 'skip empty branches
            Else
                Lambda = DirectCast(cDC(Branch).Item(cDC(Branch).Count).Lambda, Double) 'store lambda for TerminalMember
            End If

            'Determine number of elements in decay chain
            n = cDC(Branch).Count
            If n = 1 Then GoTo SimpleDecay

            'Calculate BR
            BR = 1
            For i = 1 To n - 1
                BR = BR * DirectCast(cDC(Branch).Item(i).BranchingRatio, Double)
            Next i

            'Calculate Nnt
            b = 0
            For i = 1 To n
                a = 1
                For j = 1 To n
                    If j <> i Then
                        deltaLambda = (DirectCast(cDC(Branch).Item(j).Lambda, Double) - DirectCast(cDC(Branch).Item(i).Lambda, Double))
                        If deltaLambda = 0 Then 'div by zero trap
                            delta = 0.000000000000001
                            deltaLambda = (DirectCast(cDC(Branch).Item(j).Lambda, Double) * (1 + delta)) - (DirectCast(cDC(Branch).Item(i).Lambda, Double) * (1 - delta))
                        End If
                        a = a * DirectCast(cDC(Branch).Item(j).Lambda, Double) / deltaLambda
                    End If
                Next j
                b = b + DirectCast(cDC(Branch).Item(i).Lambda, Double) * a * Math.E ^ (-DirectCast(cDC(Branch).Item(i).Lambda, Double) * DecayTime)
            Next i

            Nnt = ((N0 * BR / DirectCast(cDC(Branch).Item(n).Lambda, Double)) * b) + Nnt

NextBranch:  'used to skip empty branches

        Next Branch

        'Return Nnt
        RadDecay = Nnt * Lambda 'convert atoms to activity
        If RadDecay < 0.0# Then RadDecay = 0.0#
        GoTo ExitHere

SimpleDecay:
        RadDecay = A0 * Math.E ^ (-DirectCast(cDC(1).Item(1).Lambda, Double) * DecayTime)
        If RadDecay < 0.0# Then RadDecay = 0.0#

ExitHere:
        bRsp = pds.ClearBranches(cDC) 'clean-up memory
        If Not bRsp Then GoTo HandleErrors
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, vbCritical, "Error")
        End If

        RadDecay = False

    End Function 'RadDecay

    <ExcelFunction(Description:="Specific activity (Ci/g) for an isotope", Category:="RadToolz")> _
    Public Function SpA( _
        <ExcelArgument(Name:="Isotope", Description:="Isotope of interest (e.g., U-238)")> _
        Isotope As String) _
        As Object
        '* Usage:       Calculate Specific Activity for Isotope
        '* Input:       Isotope (e.g., Cs-137)
        '* Returns:     Specific Activity in Ci/g
        '* Author:      J. J. Prowse
        '* Date:        7/31/2015

        'Variables
        Dim m As Integer
        Dim pds As New ProcessDecaySeries
        Dim bRsp As Boolean
        Dim Msg As String
        Dim cDC(0 To maxBranches) As Collection

        Isotope = UCase(Isotope)

        If Not pds.VerifyIsotope(Isotope) Then
            SpA = "Invalid Radionuclide"
            GoTo ExitHere
        End If

        'Load decay chain
        bRsp = pds.InitBranches(cDC)
        If Not bRsp Then GoTo HandleErrors

        bRsp = pds.GetDecayChain(Isotope, Isotope, cDC)

        'Strip trailing m from Isotope
        If Right(Isotope, 1) = "M" Then Isotope = Left(Isotope, Len(Isotope) - 1)

        'Find mass
        m = InStr(1, Isotope, "-", vbTextCompare)
        Isotope = Right(Isotope, Len(Isotope) - m)
        m = Convert.ToInt32(Val(Isotope))

        SpA = DirectCast(cDC(1).Item(1).Lambda, Double) * 6.0221413E+23 / m / 37000000000.0#

ExitHere:
        bRsp = pds.ClearBranches(cDC)
        If Not bRsp Then GoTo HandleErrors
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, vbCritical, "Error")
        End If

        SpA = False

    End Function 'SpA

    <ExcelFunction(Description:="Calculate atmospheric dispersion value chi over q (s/m3)", Category:="RadToolz")> _
    Public Function XoQ( _
        <ExcelArgument(Name:="Downwind Distance", Description:="Downwind (x) distance of receptor in meters")> _
        x As Double, _
        <ExcelArgument(Name:="Crosswind Distance", Description:="Crosswind (y) distance of receptor in meters")> _
        y As Double, _
        <ExcelArgument(Name:="Height", Description:="Height (z) of receptor above the ground in meters")> _
        z As Double, _
        <ExcelArgument(Name:="Effective Stack Height", Description:="Effective height (h) of stack in meters")> _
        h As Double, _
        <ExcelArgument(Name:="Ceiling Height", Description:="Height of ceiling (L) in meters")> _
        L As Double, _
        <ExcelArgument(Name:="Stability Class", Description:="Pasquill-Gifford stability class (A-F)")> _
        SC As String, _
        <ExcelArgument(Name:="Wind Speed", Description:="Average wind speed (u) in meters/second")> _
        u As Double) _
        As Object

        '<ExcelArgument(Name:="Deposition Velocity", Description:="Dry deposition velocity (v) in centimeters/second")> _
        'v As Double) _
        '* Usage:       Calculate Chi over Q value
        '* Input:       x, y, z, h, L, SC, and u as specified above
        '* Returns:     Dilution factor s/m3
        '* Author:      J. J. Prowse
        '* Date:        12/30/2014

        'Variables

        Dim oy As Double
        Dim oz As Double
        Dim PG As Integer
        Dim n As Double
        Dim k(8) As Double
        Dim DF As Double 'depletion function

        'Convert stability class to integer
        SC = UCase(SC)
        Select Case SC
            Case "A"
                PG = 0
            Case "B"
                PG = 1
            Case "C"
                PG = 2
            Case "D"
                PG = 3
            Case "E"
                PG = 4
            Case "F"
                PG = 5
            Case Else
                GoTo HandleErrors
        End Select

        'Get sigma y
        oy = TGSigma("y", PG, x)

        'Get sigma x
        oz = TGSigma("z", PG, x)

        'Calculate k() values
        n = 1
        k(0) = 1 / (2 * Math.PI * u * oy * oz)
        k(1) = Math.E ^ (-(y ^ 2) / (2 * oy ^ 2))
        k(2) = Math.E ^ (-((z - h) ^ 2) / (2 * oz ^ 2))
        k(3) = Math.E ^ (-((z + h) ^ 2) / (2 * oz ^ 2))
        k(8) = 0
        For n = 1 To 5
            k(4) = Math.E ^ (-((z - h - (2 * n * L)) ^ 2) / (2 * oz ^ 2))
            k(5) = Math.E ^ (-((z + h - (2 * n * L)) ^ 2) / (2 * oz ^ 2))
            k(6) = Math.E ^ (-((z - h + (2 * n * L)) ^ 2) / (2 * oz ^ 2))
            k(7) = Math.E ^ (-((z + h + (2 * n * L)) ^ 2) / (2 * oz ^ 2))
            k(8) = k(8) + k(4) + k(5) + k(6) + k(7)
        Next

        'calculate dry deposition depletion function DF  <future feature>
        DF = 1

ExitHere:

        'XoQ = TGSigma("y", PG, x)
        XoQ = SigFig(DF * k(0) * k(1) * (k(2) + k(3) + k(8)), 3)
        Exit Function

HandleErrors:
        XoQ = ExcelError.ExcelErrorValue

    End Function 'XoQ

    <ExcelFunction(Description:="Return Number to desired accuracy", Category:="RadToolz")> _
    Public Function SigFig( _
        <ExcelArgument(Name:="Number", Description:="A number or expression of a number")> _
        num As Double, _
        <ExcelArgument(Name:="Significant Figures", Description:="Integer representing the desired number of significant figures")> _
        sf As Integer) _
        As Object
        '* Usage:       Return a number to the requested accuracy
        '* Input:       num - the number to return to the desired accuracy
        '*              sf - number of significant figures as integer
        '* Returns:     The number to the requested number of significant figures
        '* Author:      J. J. Prowse
        '* Date:        10/31/2015

        If (num = 0) Then Return 0

        Dim d As Double = Math.Ceiling(Math.Log10(If(num < 0, -num, num)))
        Dim power As Integer = sf - Convert.ToInt32(d)
        Dim magnitude As Double = Math.Pow(10, power)
        Dim shifted As Double = Math.Round(num * magnitude)

        Return shifted / magnitude

    End Function 'SigFig

    <ExcelFunction(Description:="Return Number to desired precision", Category:="RadToolz")> _
    Public Function ANSIRound( _
        <ExcelArgument(Name:="Number", Description:="is the number you want to round")> _
        num As Decimal, _
        <ExcelArgument(Name:="(Optional) Number of Digits", Description:="is the number of digits you want to round to.  Negative round to the left of the decimal point. Zero to the nearest integer (default)")> _
        Optional digits As Integer = 0) _
        As Object
        '* Usage:       Return a number to the desired precision
        '* Input:       num - the number to return to the desired precision
        '*              digits - the number of decimal places to represent
        '* Returns:     The number to the requested number of digits
        '* Author:      J. J. Prowse
        '* Date:        10/31/2015

        Return Math.Round(num, digits)

    End Function 'SigFig

    <ExcelFunction(Description:="List available RadToolz functions", Category:="RadToolz", IsMacroType:=True, IsVolatile:=False)> _
    Public Function RTZFunctions( _
        <ExcelArgument(Name:="Output Cell", Description:="Cell in which to start the output table.  Caution:  allow for ~20 empty rows; function does not check for values in cells", AllowReference:=True)> _
        uRng As Object) _
        As Object
        '* Usage:       Lists all functions from RadToolz
        '* Input:       uRngVal - cell address to begin data dump
        '* Returns:     Title including version
        '* Author:      J. J. Prowse
        '* Date:        4/2/2016

        On Error GoTo HandleErrors

        'Dim Rsp As Boolean
        Dim cCellAddr As String = DirectCast(Excel(xlfReftext, DirectCast(XlCall.Excel(XlCall.xlfCaller), ExcelReference), True), String)
        Dim uCellAddr As String = DirectCast(Excel(xlfReftext, uRng, True), String)
        Dim iRng As Range
        Dim iSheet As Worksheet
        Dim uSheet As String
        Dim r As Integer
        Dim c As Integer
        Dim uRngVal As String = ""
        Dim pattern As String = "\](\w*)'?!(.*)"
        Dim rRegex As Regex = New Regex(pattern)
        Dim m As Match

        'Ensure calling cell range and user defined range are not the same
        If StrComp(cCellAddr, uCellAddr) = 0 Then
            RTZFunctions = ExcelError.ExcelErrorValue
            Exit Function
        End If

        'Get Regex groups from match
        m = rRegex.Match(uCellAddr)
        uSheet = m.Groups(1).ToString
        uRngVal = m.Groups(2).ToString

        'Create the ranges and worksheet
        iRng = DirectCast(iExcel.Range(uRngVal), Range)
        iSheet = DirectCast(iExcel.Worksheets(uSheet), Worksheet)
        r = Convert.ToInt32(iRng.Row)
        c = Convert.ToInt32(iRng.Column)

        'Write Nuclear Functions 'r++ after each description 
        iSheet.Cells(r, c) = "ANSIRound"
        iSheet.Cells(r, c + 1) = "Round a value in accordance with ANSI standard"
        r = r + 1
        iSheet.Cells(r, c) = "DCF"
        iSheet.Cells(r, c + 1) = "Dose conversion factors (ICRP-68 or 72) for a radionuclide"
        r = r + 1
        iSheet.Cells(r, c) = "FGE"
        iSheet.Cells(r, c + 1) = "Calculates U-235 or Pu-239 fissile gram equivalents"
        r = r + 1
        iSheet.Cells(r, c) = "EnumDecayChain"
        iSheet.Cells(r, c + 1) = "List members of a decay chain (e.g., U-238 decay chain)"
        r = r + 1
        iSheet.Cells(r, c) = "HalfLife"
        iSheet.Cells(r, c + 1) = "Half life for a radionuclide"
        r = r + 1
        iSheet.Cells(r, c) = "PECi"
        iSheet.Cells(r, c + 1) = "Calculates the plutonium equivalent curies for a radionuclide"
        r = r + 1
        iSheet.Cells(r, c) = "RadDecay"
        iSheet.Cells(r, c + 1) = "Time decayed activity of a radionuclide or progeny"
        r = r + 1
        iSheet.Cells(r, c) = "SigFig"
        iSheet.Cells(r, c + 1) = "Convert a value to specified number of significant digits"
        r = r + 1
        iSheet.Cells(r, c) = "SpA"
        iSheet.Cells(r, c + 1) = "Specific activity for a radionuclide"
        r = r + 1
        iSheet.Cells(r, c) = "XoQ"
        iSheet.Cells(r, c + 1) = "Calculate chi over q (atmospheric dispersion value)"
        r = r + 1

        'Write RadToolz Information Functions
        iSheet.Cells(r, c) = "RTZAttribution"
        iSheet.Cells(r, c + 1) = "Returns the preferred attribution to RadToolz for derivative works"
        r = r + 1
        iSheet.Cells(r, c) = "RTZFunctions"
        iSheet.Cells(r, c + 1) = "List RadToolz functions"
        r = r + 1
        iSheet.Cells(r, c) = "RTZLicense"
        iSheet.Cells(r, c + 1) = "Presents the RadToolz license from the Internet"
        r = r + 1
        iSheet.Cells(r, c) = "RTZParams"
        iSheet.Cells(r, c + 1) = "Displays the RadToolz radionuclide parameters in table form for verification and validation"
        r = r + 1
        iSheet.Cells(r, c) = "RTZRefs"
        iSheet.Cells(r, c + 1) = "Returns message box with references used by RadToolz"
        r = r + 1
        iSheet.Cells(r, c) = "RTZUpdate"
        iSheet.Cells(r, c + 1) = "Checks for updates to Radtoolz from the Internet"
        r = r + 1
        iSheet.Cells(r, c) = "RTZVers"
        iSheet.Cells(r, c + 1) = "Returns version of RadToolz being used"

        RTZFunctions = "Functions for Radtoolz version " & RTZVers()

        uRng = uRng 'dummy operation to calm the Excel denpendency tree down!

        Exit Function

HandleErrors:

        If Err.Number = 1004 Then
            Err.Clear()
        Else
            RTZFunctions = "Failed"
        End If

    End Function

    <ExcelFunction(Description:="Display RadToolz version number", Category:="RadToolz")> _
    Public Function RTZVers( _
        <ExcelArgument(Name:="None", Description:="No input required")> _
        Optional no_input As Object = Nothing) _
        As Object
        '* Usage:       Reports back the version number of the RadToolz
        '* Input:       None
        '* Returns:     Version number, from public constant
        '* Author:      J. J. Prowse
        '* Date:        4/8/2016

        RTZVers = RadToolzVersion.ToString("0.00", Globalization.CultureInfo.InvariantCulture) & RadToolzPreRelease

        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            MsgBox("Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description, MsgBoxStyle.Critical, "Error")
        End If

        RTZVers = False

    End Function 'RTZVers

    <ExcelFunction(Description:="Display RadToolz references", Category:="RadToolz")> _
    Public Function RTZRefs( _
        <ExcelArgument(Name:="None", Description:="No input required")> _
        Optional no_input As Object = Nothing) _
        As Object
        '* Usage:       Reports back the version number of the RadToolz
        '* Input:       None
        '* Returns:     Version number, from public constant
        '* Author:      J. J. Prowse
        '* Date:        4/15/2016

        'Dim result As Boolean
        Dim Msg As String
        Dim ENSDF As String
        Dim ICRP119 As String
        Dim GENII As String
        Dim ANSI8_1 As String
        Dim ANSI8_15 As String

        ENSDF = "Nuclide data and adapted equations are from:" & vbCrLf & _
                vbCrLf & _
                "    Evaluated Nuclear Structure Data File (ENSDF). National Nuclear" & vbCrLf & _
                "    Data Center. Brookhaven National Laboratory. Upton, NY 11973-5000." & vbCrLf & _
                "    http://www.nndc.bnl.gov/" & vbCrLf & vbCrLf

        ICRP119 = "Dose conversion factors are from:" & vbCrLf & _
                vbCrLf & _
                "    ICRP, 2012. Compendium of Dose Coefficients based on ICRP " & vbCrLf & _
                  "    Publication 60.ICRP Publication 119.  Ann. ICRP 41 (Suppl.)." & vbCrLf & _
                  "    http://www.icrp.org/publication.asp?id=ICRP%20Publication%20119" & vbCrLf & vbCrLf

        GENII = "Atmospheric dispersion (X/Q) coefficients and adapted equations are from:" & vbCrLf & _
                vbCrLf & _
                "    DOE, 2004. GENII Computer Code, Application Guidance for" & vbCrLf & _
                "    Documented Safety Analysis,Final Report, U.S. Department of Energy." & vbCrLf & _
                "    http://energy.gov/ehss/downloads/" & vbCrLf & _
                "    guidance-genii-computer-code-july-6-2004" & vbCrLf & vbCrLf

        ANSI8_1 = "Fissile critical mass values are from:" & vbCrLf & _
                vbCrLf & _
                "    ANSI, 2014a.  Nuclear Criticality Safety in Operations" & vbCrLf & _
                  "    with Fissionable Material OUtside Reactors.  ANSI/ANS-8.1-2014." & vbCrLf & vbCrLf

        ANSI8_15 = "    ANSI, 2014b.  Nuclear Criticality Safety Control of " & vbCrLf & _
                   "    Selected Actinide Nuclides.  ANSI/ANS-8.15-2014." & vbCrLf & vbCrLf

        Msg = ENSDF & ICRP119 & GENII & ANSI8_1 & ANSI8_15
        MsgBox(Msg, vbOKOnly, "RadToolz vers. " & RTZVers())

        RTZRefs = ""

        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, MsgBoxStyle.Critical, "Error")
        End If

        RTZRefs = False

    End Function 'RTZRefs

    <ExcelFunction(Description:="Check www.RadToolz.com for update availability", Category:="RadToolz")> _
    Public Function RTZUpdate( _
        <ExcelArgument(Name:="None", Description:="No input required")> _
        Optional no_input As Object = Nothing) _
        As Object
        '* Usage:       Checks internet for RadToolz update
        '* Input:       None
        '* Returns:     Update status
        '* Author:      J. J. Prowse
        '* Date:        4/15/2016

        On Error GoTo HandleErrors

        Dim Msg As Object
        Dim src As String = New System.Net.WebClient().DownloadString("http://www.radtoolz.com/p/hiddenversion.html")
        Dim x As Integer
        Dim vers As String
        Dim versNum As Double

        If IsError(src) Then
            RTZUpdate = "Unable to connect to www.RadToolz.com"
            GoTo exithere
        End If

        x = src.IndexOf("articleBody")
        vers = Mid(src, x + Len("articleBody'> "), 20) ' got version
        x = vers.IndexOf("<")
        vers = Left(vers, x - 1) 'strips the < off
        vers = vers.Trim 'version is now a string
        versNum = Convert.ToDouble(vers)

        If versNum > RadToolzVersion Then 'Need an update
            Msg = "RadToolz is now at version " + vers + ".  You should update." & vbCrLf & "Open browser to www.RadToolz.com?"
            Msg = MsgBox(Msg, MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, "Update RadToolz")
            If Msg = vbYes Then Process.Start("http://www.radtoolz.com/")
            vers = "Current RadToolz version is " & vers & "."
        ElseIf versNum < RadToolzVersion Then 'Pre-release version
            vers = "RadToolz is now at version " + vers + ".  You have pre-release version " & RTZVers().ToString
        ElseIf versNum = RadToolzVersion And RadToolzPreRelease <> "" Then 'Pre-release of current version
            Msg = "RadToolz " + vers + " has been released.  You have a pre-release version and should update." & vbCrLf & "Open browser to www.RadToolz.com?"
            Msg = MsgBox(Msg, MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, "Update RadToolz")
            If Msg = vbYes Then Process.Start("http://www.radtoolz.com/")
            vers = "Current RadToolz version is " & vers & "."
        Else 'Current release version
            vers = "RadToolz is up to date."
        End If

        RTZUpdate = vers

exithere:
        Exit Function

HandleErrors:

        Select Case Err.Number
            Case 0
                RTZUpdate = vers
            Case 5
                RTZUpdate = "Unable to connect to www.RadToolz.com.  Try again later."
            Case Else
                Msg = "Error # " & Str(Err.Number) & " was generated by " _
                    & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
                MsgBox(Msg, MsgBoxStyle.Critical, "Error")
                RTZUpdate = ExcelError.ExcelErrorValue
        End Select

    End Function 'RTZUpdate

    <ExcelFunction(Description:="Display RadToolz license", Category:="RadToolz")> _
    Public Function RTZLicense( _
        <ExcelArgument(Name:="None", Description:="No input required")> _
        Optional no_input As Object = Nothing) _
        As Object
        '* Usage:       Opens Browser to RadToolzLicense
        '* Input:       None
        '* Returns:     Version number, from public constant
        '* Author:      J. J. Prowse
        '* Date:        8/14/2015

        'Dim result As Boolean
        Dim Msg As Object
        Dim license As String = "http://www.radtoolz.com/p/license.html"

        Msg = MsgBox("Open browser for RadToolz license?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "RadToolz License")
        If Msg = vbYes Then Process.Start(license)

        RTZLicense = "RadToolz license may be found at " & license & _
            ".  Excel-DNA license may be found at https://github.com/Excel-DNA/ExcelDna/blob/master/LICENSE.txt"

        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, MsgBoxStyle.Critical, "Error")
        End If

        RTZLicense = False

    End Function 'RTZLicense

    <ExcelFunction(Description:="List all isotopes and values for all isotopes", Category:="RadToolz", IsMacroType:=True, IsVolatile:=False)> _
    Public Function RTZParams( _
        <ExcelArgument(Name:="Output Cell", Description:="Cell in which to start the output table.  Caution:  allow for ~200 empty rows; function does not check for values in cells", AllowReference:=True)> _
        uRng As Object) _
        As Object
        '* Usage:       Lists all values from RadToolz
        '* Input:       uRng - cell address to begin data dump
        '* Returns:     Title including version
        '* Author:      J. J. Prowse
        '* Date:        8/14/2015

        On Error GoTo HandleErrors

        Dim pds As New ProcessDecaySeries
        Dim Rsp As Boolean
        Dim cCellAddr As String = DirectCast(Excel(xlfReftext, DirectCast(XlCall.Excel(XlCall.xlfCaller), ExcelReference), True), String)
        Dim uCellAddr As String = DirectCast(Excel(xlfReftext, uRng, True), String)

        If StrComp(cCellAddr, uCellAddr) = 0 Then
            RTZParams = ExcelError.ExcelErrorValue
            Exit Function
        End If

        Rsp = pds.ListAll(uCellAddr)

        If Rsp Then
            RTZParams = "Radtoolz version " & RTZVers()
        Else
            RTZParams = "Failed"
        End If

        uRng = uRng 'dummy operation to calm the Excel denpendency tree down!

        Exit Function

HandleErrors:

        If Err.Number = 1004 Then
            Err.Clear()
        End If

    End Function 'RTZListParams

    <ExcelFunction(Description:="Return RadToolz attribution", Category:="RadToolz")> _
    Public Function RTZAttribution( _
        <ExcelArgument(Name:="None", Description:="No input required")> _
        Optional no_input As Object = Nothing) _
        As Object
        '* Usage:       Displays RadToolz Attribution
        '* Input:       None
        '* Returns:     Attribution text
        '* Author:      J. J. Prowse
        '* Date:        8/9/2015

        Dim Msg As Object = ""

        RTZAttribution = _
            "RadToolz version " & RTZVers() & ".  Copyright (c) " & Year(Now) & " " & _
            "by Backscatter enterprises.  Licensed under a " & _
            "Creative Commons Attribution 4.0 International Public License at " & _
            "http://creativecommons.org/licenses/by/4.0/legalcode" & ".  " & _
            "RadToolz is provided as-is and as-available.  No warranties are given " & _
            "(see Disclaimer of Warranties and Limitation of Liability in the License). " & _
            "Based on a work at http://radtoolz.com"

        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, MsgBoxStyle.Critical, "Error")
        End If

        RTZAttribution = False

    End Function 'RTZAttribution

    Private Function TGSigma(Direction As String, PG As Integer, Distance As Double) As Double

        Dim ay(5) As Double
        Dim az(5) As Double
        Dim by(5) As Double
        Dim bz(5) As Double
        Dim cy(5) As Double
        Dim cz(5) As Double

        Direction = UCase(Direction)

        'Load Tadmor-Gur fitting constants
        ay(0) = 0.3658
        ay(1) = 0.2751
        ay(2) = 0.2089
        ay(3) = 0.1474
        ay(4) = 0.1046
        ay(5) = 0.0722
        by(0) = 0.9031
        by(1) = 0.9031
        by(2) = 0.9031
        by(3) = 0.9031
        by(4) = 0.9031
        by(5) = 0.9031
        cy(0) = 0
        cy(1) = 0
        cy(2) = 0
        cy(3) = 0
        cy(4) = 0
        cy(5) = 0

        If Distance >= 100 And Distance < 5000 Then
            az(0) = 0.00025
            az(1) = 0.0019
            az(2) = 0.2
            az(3) = 0.3
            az(4) = 0.4
            az(5) = 0.2

            bz(0) = 2.125
            bz(1) = 1.6021
            bz(2) = 0.8543
            bz(3) = 0.6532
            bz(4) = 0.6021
            bz(5) = 0.602

        ElseIf Distance >= 5000 And Distance <= 50000 Then
            az(0) = 0
            az(1) = 0
            az(2) = 0.5742
            az(3) = 0.9605
            az(4) = 2.125
            az(5) = 2.182

            bz(0) = 0
            bz(1) = 0
            bz(2) = 0.716
            bz(3) = 0.5409
            bz(4) = 0.3979
            bz(5) = 0.331

        Else
            az(0) = 0
            az(1) = 0
            az(2) = 0
            az(3) = 0
            az(4) = 0
            az(5) = 0

            bz(0) = 0
            bz(1) = 0
            bz(2) = 0
            bz(3) = 0
            bz(4) = 0
            bz(5) = 0

        End If

        Select Case Direction
            Case "Y"
                'Calculate sigma y
                TGSigma = ((ay(PG) * (Distance ^ by(PG))) + cy(PG))
            Case "Z"
                'Calculate sigma z
                TGSigma = ((az(PG) * (Distance ^ bz(PG))) + cz(PG))
            Case Else
                TGSigma = -1
        End Select

    End Function

End Module
