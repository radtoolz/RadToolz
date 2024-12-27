'Imports ExcelDna.Integration
'Imports System

Public Class DecaySeriesItem
    '* Author:      Backscatter enterprises
    '* Date:        10/23/2018

    Private ReadOnly cIsotope As String
    Private ReadOnly cDaughter As String
    Private ReadOnly cLambda As Double
    Private ReadOnly cBranchingRatio As Double
    Private ReadOnly cDCF68inhF1 As Double
    Private ReadOnly cDCF68inhF5 As Double
    Private ReadOnly cDCF68inhM1 As Double
    Private ReadOnly cDCF68inhM5 As Double
    Private ReadOnly cDCF68inhS1 As Double
    Private ReadOnly cDCF68inhS5 As Double
    Private ReadOnly cDCF68ing As Double
    Private ReadOnly cDCF72inhF1 As Double
    Private ReadOnly cDCF72inhM1 As Double
    Private ReadOnly cDCF72inhS1 As Double
    Private ReadOnly cDCF72ing As Double
    Private ReadOnly cA1 As Double
    Private ReadOnly cA2 As Double

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
    Public Property A1 As Double = cA1
    Public Property A2 As Double = cA2

End Class