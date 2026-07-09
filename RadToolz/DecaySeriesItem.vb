''' <summary>
''' One row of the isotope decay-series database: an isotope, its decay
''' constant, its daughter, dose conversion factors, ANSI A1/A2 transport
''' values, and branching ratio to that daughter. Deserialized in bulk from
''' the embedded DecaySeriesData.json resource by
''' DecaySeriesRepository.LoadFromEmbeddedResource (Newtonsoft.Json matches
''' JSON property names to these Property names). Also constructed
''' one-off by ProcessDecaySeries.LoadDecaySeriesItem when a caller needs
''' its own private, mutable copy of a shared, cached instance - see the
''' "treat as read-only" contract documented on DecaySeriesRepository.GetAll.
''' </summary>
Public Class DecaySeriesItem
    '* Author:      Backscatter enterprises
    '* Date:        10/23/2018

    ' NOTE ON THE FIELDS BELOW: these private ReadOnly backing fields are never
    ' assigned anywhere (no constructor sets them), so each is permanently its
    ' type's default value (0.0 for Double, Nothing for String). Each is used
    ' only once, as the initializer expression for the public auto-property of
    ' the same name below (e.g. "Public Property A1 As Double = cA1"). VB.NET
    ' auto-properties generate their own hidden backing field distinct from
    ' cA1 et al., so this pattern:
    '   1. Sets every property's initial value to 0.0/Nothing (the same result
    '      as omitting the initializer entirely, since that is Double/String's
    '      default anyway).
    '   2. Is otherwise inert - Newtonsoft.Json and LoadDecaySeriesItem both
    '      set these properties directly through their public setters, never
    '      touching cA1 et al.
    ' In short: these fields do not do anything beyond documenting each
    ' property's default. Preserved as-is (existing code is presumed correct
    ' absent evidence it causes a problem, and it does not); flagged here so a
    ' future reader does not mistake them for active backing storage.
    Private ReadOnly cA1 As Double
    Private ReadOnly cA2 As Double
    Private ReadOnly cBranchingRatio As Double
    Private ReadOnly cDaughter As String
    Private ReadOnly cDCF68ing As Double
    Private ReadOnly cDCF68inhF1 As Double
    Private ReadOnly cDCF68inhF5 As Double
    Private ReadOnly cDCF68inhM1 As Double
    Private ReadOnly cDCF68inhM5 As Double
    Private ReadOnly cDCF68inhS1 As Double
    Private ReadOnly cDCF68inhS5 As Double
    Private ReadOnly cDCF72ing As Double
    Private ReadOnly cDCF72inhF1 As Double
    Private ReadOnly cDCF72inhM1 As Double
    Private ReadOnly cDCF72inhS1 As Double
    Private ReadOnly cIsotope As String
    Private ReadOnly cLambda As Double

    ''' <summary>ANSI/IEEE A1 transport activity limit (TBq) - see AValue().</summary>
    Public Property A1 As Double = cA1
    ''' <summary>ANSI/IEEE A2 transport activity limit (TBq) - see AValue().</summary>
    Public Property A2 As Double = cA2
    ''' <summary>Fraction of this isotope's decays that proceed to Daughter (1.0 if this is the only decay mode).</summary>
    Public Property BranchingRatio As Double = cBranchingRatio
    ''' <summary>Isotope symbol this isotope decays into along this branch, or "END" for a terminal member.</summary>
    Public Property Daughter As String = cDaughter
    ''' <summary>ICRP-68 ingestion dose conversion factor (rem/uCi).</summary>
    Public Property DCF68ing As Double = cDCF68ing
    ''' <summary>ICRP-68 inhalation DCF, Fast absorption, AMAD 1 micron (rem/uCi).</summary>
    Public Property DCF68inhF1 As Double = cDCF68inhF1
    ''' <summary>ICRP-68 inhalation DCF, Fast absorption, AMAD 5 micron (rem/uCi).</summary>
    Public Property DCF68inhF5 As Double = cDCF68inhF5
    ''' <summary>ICRP-68 inhalation DCF, Moderate absorption, AMAD 1 micron (rem/uCi).</summary>
    Public Property DCF68inhM1 As Double = cDCF68inhM1
    ''' <summary>ICRP-68 inhalation DCF, Moderate absorption, AMAD 5 micron (rem/uCi).</summary>
    Public Property DCF68inhM5 As Double = cDCF68inhM5
    ''' <summary>ICRP-68 inhalation DCF, Slow absorption, AMAD 1 micron (rem/uCi).</summary>
    Public Property DCF68inhS1 As Double = cDCF68inhS1
    ''' <summary>ICRP-68 inhalation DCF, Slow absorption, AMAD 5 micron (rem/uCi).</summary>
    Public Property DCF68inhS5 As Double = cDCF68inhS5
    ''' <summary>ICRP-72 ingestion dose conversion factor (rem/uCi).</summary>
    Public Property DCF72ing As Double = cDCF72ing
    ''' <summary>ICRP-72 inhalation DCF, Fast absorption, AMAD 1 micron only (rem/uCi) - ICRP-72 has no AMAD-5 values; see DCF()'s AMAD override for the "72" standard.</summary>
    Public Property DCF72inhF1 As Double = cDCF72inhF1
    ''' <summary>ICRP-72 inhalation DCF, Moderate absorption, AMAD 1 micron (rem/uCi).</summary>
    Public Property DCF72inhM1 As Double = cDCF72inhM1
    ''' <summary>ICRP-72 inhalation DCF, Slow absorption, AMAD 1 micron (rem/uCi).</summary>
    Public Property DCF72inhS1 As Double = cDCF72inhS1
    ''' <summary>Isotope symbol for this record (e.g. "CS-137"). Always upper-case in the loaded table.</summary>
    Public Property Isotope As String = cIsotope
    ''' <summary>Decay constant lambda, in inverse seconds (ln(2) / half-life-in-seconds).</summary>
    Public Property Lambda As Double = cLambda
End Class