'Imports ExcelDna.Integration
'Imports System

Public Class DecaySeriesItem
    '* Author:      J. J. Prowse
    '* Date:        7/19/2015

    Private cIsotope As String
    Private cDaughter As String
    Private cLambda As Double
    Private cBranchingRatio As Double
    Private cDCF68inhF1 As Double
    Private cDCF68inhF5 As Double
    Private cDCF68inhM1 As Double
    Private cDCF68inhM5 As Double
    Private cDCF68inhS1 As Double
    Private cDCF68inhS5 As Double
    Private cDCF68ing As Double
    Private cDCF72inhF1 As Double
    Private cDCF72inhM1 As Double
    Private cDCF72inhS1 As Double
    Private cDCF72ing As Double

    Public Property Isotope As String = cIsotope
    Public Property Daughter As String = cDaughter
    Public Property Lambda As Double = cLambda
    Public Property BranchingRatio As Double = cBranchingRatio
    Public Property DCF68inhF1 As Double = cDCF68inhF1
    Public Property DCF68inhF5 As Double = cDCF68inhF5
    Public Property DCF68inhM1 As Double = cDCF68inhM1
    Public Property DCF68inhM5 As Double = cDCF68inhM5
    Public Property DCF68inhS1 As Double = cDCF68inhS1
    Public Property DCF68inhS5 As Double = cDCF68inhS5
    Public Property DCF68ing As Double = cDCF68ing
    Public Property DCF72inhF1 As Double = cDCF72inhF1
    Public Property DCF72inhM1 As Double = cDCF72inhM1
    Public Property DCF72inhS1 As Double = cDCF72inhS1
    Public Property DCF72ing As Double = cDCF72ing

End Class