''' <summary>
''' One row of the gamma-ray buildup factor coefficient database: a
''' material, buildup type (Exposure or Absorption), the photon energy this
''' row applies to, and the five Geometric-Progression (G-P) fitting
''' parameters (B, C, A, Xk, D) for that (material, type, energy) point.
''' Deserialized in bulk from the embedded BuildupFactorData.json resource
''' by BuildupFactorRepository.LoadFromEmbeddedResource (Newtonsoft.Json
''' matches JSON property names to these Property names).
''' </summary>
Public Class BuildupFactorItem

    ''' <summary>Shield material this row's coefficients apply to (e.g. "Iron").</summary>
    Public Property Material As String

    ''' <summary>Buildup type this row's coefficients apply to: "Exposure" or "Absorption".</summary>
    Public Property BuildupType As String

    ''' <summary>Photon energy (MeV) this row's coefficients were fitted at.</summary>
    Public Property Energy As Double

    ''' <summary>G-P coefficient: buildup factor value at 1 mean free path.</summary>
    Public Property B As Double

    ''' <summary>G-P coefficient: coefficient c in the K(x) power-law term.</summary>
    Public Property C As Double

    ''' <summary>G-P coefficient: exponent a in the K(x) power-law term.</summary>
    Public Property A As Double

    ''' <summary>G-P coefficient: Xk, the tanh transition scale in K(x).</summary>
    Public Property Xk As Double

    ''' <summary>G-P coefficient: d, the tanh term amplitude in K(x).</summary>
    Public Property D As Double

End Class
