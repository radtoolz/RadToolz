Imports System.Text.RegularExpressions
Imports ExcelDna.Integration
Imports Microsoft.Office.Interop.Excel

Public Class ProcessDecaySeries

    Public Function GetDecaySeries() As Collection
        '* Usage:       Contains the database for isotopes
        '* Input:       Nothing
        '* Returns:     Nothing
        '* Author:      Backscatter enterprises
        '* Date:        12/24/224

        'Static Dim dci As Collection 'static added to clear rule violation
        Dim dci As Collection
        Dim dsi As DecaySeriesItem

        dci = New Collection

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        dsi = New DecaySeriesItem With {
  .Isotope = "CM-246",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 148,
  .DCF68inhM5 = 99.9,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.777,
  .DCF72inhF1 = 362.6,
  .DCF72inhM1 = 155.4,
  .DCF72inhS1 = 59.2,
  .DCF72ing = 0.777,
  .A1 = 9,
  .A2 = 0.0009,
  .Daughter = "PU-242",
  .Lambda = 0.00000000000461730265411607,
  .BranchingRatio = 0.9997385
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PU-242",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 162.8,
  .DCF68inhM5 = 114.7,
  .DCF68inhS1 = 51.8,
  .DCF68inhS5 = 28.49,
  .DCF68ing = 0.888,
  .DCF72inhF1 = 407,
  .DCF72inhM1 = 177.6,
  .DCF72inhS1 = 55.5,
  .DCF72ing = 0.888,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "U-238",
  .Lambda = 0.000000000000058572023268347,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "U-238",
  .DCF68inhF1 = 1.813,
  .DCF68inhF5 = 2.146,
  .DCF68inhM1 = 9.62,
  .DCF68inhM5 = 5.92,
  .DCF68inhS1 = 27.01,
  .DCF68inhS5 = 21.09,
  .DCF68ing = 0.1628,
  .DCF72inhF1 = 1.85,
  .DCF72inhM1 = 10.73,
  .DCF72inhS1 = 29.6,
  .DCF72ing = 0.1665,
  .A1 = -1,
  .A2 = -1,
  .Daughter = "TH-234",
  .Lambda = 4.92477774117267E-18,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TH-234",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.02331,
  .DCF68inhM5 = 0.01961,
  .DCF68inhS1 = 0.02701,
  .DCF68inhS5 = 0.02146,
  .DCF68ing = 0.01258,
  .DCF72inhF1 = 0.00925,
  .DCF72inhM1 = 0.02442,
  .DCF72inhS1 = 0.02849,
  .DCF72ing = 0.01258,
  .A1 = 0.3,
  .A2 = 0.3,
  .Daughter = "PA-234M",
  .Lambda = 0.000000332788684284084,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PA-234M",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "U-234",
  .Lambda = 0.00996760397699087,
  .BranchingRatio = 0.9984
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PA-234M",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PA-234",
  .Lambda = 0.00996760397699087,
  .BranchingRatio = 0.0016
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "U-234",
  .DCF68inhF1 = 2.035,
  .DCF68inhF5 = 2.368,
  .DCF68inhM1 = 11.47,
  .DCF68inhM5 = 7.77,
  .DCF68inhS1 = 31.45,
  .DCF68inhS5 = 25.16,
  .DCF68ing = 0.1813,
  .DCF72inhF1 = 2.072,
  .DCF72inhM1 = 12.95,
  .DCF72inhS1 = 34.78,
  .DCF72ing = 0.1813,
  .A1 = 40,
  .A2 = 0.006,
  .Daughter = "TH-230",
  .Lambda = 0.0000000000000892866208358948,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TH-230",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 148,
  .DCF68inhM5 = 103.6,
  .DCF68inhS1 = 48.1,
  .DCF68inhS5 = 26.64,
  .DCF68ing = 0.777,
  .DCF72inhF1 = 370,
  .DCF72inhM1 = 159.1,
  .DCF72inhS1 = 51.8,
  .DCF72ing = 0.777,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "RA-226",
  .Lambda = 0.000000000000291306481772283,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RA-226",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 11.84,
  .DCF68inhM5 = 8.14,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 1.036,
  .DCF72inhF1 = 1.332,
  .DCF72inhM1 = 12.95,
  .DCF72inhS1 = 35.15,
  .DCF72ing = 1.036,
  .A1 = 0.2,
  .A2 = 0.003,
  .Daughter = "RN-222",
  .Lambda = 0.0000000000137278179535188,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RN-222",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0.3,
  .A2 = 0.004,
  .Daughter = "PO-218",
  .Lambda = 0.0000020993381618639,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-218",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PB-214",
  .Lambda = 0.00373020762329106,
  .BranchingRatio = 0.9998
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-218",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "AT-218",
  .Lambda = 0.00373020762329106,
  .BranchingRatio = 0.0002
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PB-214",
  .DCF68inhF1 = 0.01073,
  .DCF68inhF5 = 0.01776,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000518,
  .DCF72inhF1 = 0.01036,
  .DCF72inhM1 = 0.0518,
  .DCF72inhS1 = 0.0555,
  .DCF72ing = 0.000518,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "BI-214",
  .Lambda = 0.000426919919044066,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-214",
  .DCF68inhF1 = 0.02664,
  .DCF68inhF5 = 0.0444,
  .DCF68inhM1 = 0.0518,
  .DCF68inhM5 = 0.0777,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000407,
  .DCF72inhF1 = 0.02627,
  .DCF72inhM1 = 0.0518,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.000407,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PO-214",
  .Lambda = 0.000586121410925034,
  .BranchingRatio = 0.9979
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-214",
  .DCF68inhF1 = 0.02664,
  .DCF68inhF5 = 0.0444,
  .DCF68inhM1 = 0.0518,
  .DCF68inhM5 = 0.0777,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000407,
  .DCF72inhF1 = 0.02627,
  .DCF72inhM1 = 0.0518,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.000407,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "TL-210",
  .Lambda = 0.000586121410925034,
  .BranchingRatio = 0.00209999999999999
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-214",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PB-210",
  .Lambda = 4239.95094543642,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PB-210",
  .DCF68inhF1 = 3.293,
  .DCF68inhF5 = 4.07,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 2.516,
  .DCF72inhF1 = 3.33,
  .DCF72inhM1 = 4.07,
  .DCF72inhS1 = 20.72,
  .DCF72ing = 2.553,
  .A1 = 1,
  .A2 = 0.05,
  .Daughter = "BI-210",
  .Lambda = 0.000000000989392284938294,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-210",
  .DCF68inhF1 = 0.00407,
  .DCF68inhF5 = 0.00518,
  .DCF68inhM1 = 0.3108,
  .DCF68inhM5 = 0.222,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00481,
  .DCF72inhF1 = 0.00407,
  .DCF72inhM1 = 0.3441,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00481,
  .A1 = 1,
  .A2 = 0.6,
  .Daughter = "PO-210",
  .Lambda = 0.00000160066576457231,
  .BranchingRatio = 0.999868
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-210",
  .DCF68inhF1 = 0.00407,
  .DCF68inhF5 = 0.00518,
  .DCF68inhM1 = 0.3108,
  .DCF68inhM5 = 0.222,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00481,
  .DCF72inhF1 = 0.00407,
  .DCF72inhM1 = 0.3441,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00481,
  .A1 = 1,
  .A2 = 0.6,
  .Daughter = "TL-206",
  .Lambda = 0.00000160066576457231,
  .BranchingRatio = 0.000132
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-210",
  .DCF68inhF1 = 2.22,
  .DCF68inhF5 = 2.627,
  .DCF68inhM1 = 11.1,
  .DCF68inhM5 = 8.14,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.888,
  .DCF72inhF1 = 2.257,
  .DCF72inhM1 = 12.21,
  .DCF72inhS1 = 15.91,
  .DCF72ing = 4.44,
  .A1 = 40,
  .A2 = 0.02,
  .Daughter = "END",
  .Lambda = 0.0000000579755222075504,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PA-234",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.001406,
  .DCF68inhM5 = 0.002035,
  .DCF68inhS1 = 0.00148,
  .DCF68inhS5 = 0.002146,
  .DCF68ing = 0.001887,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.001406,
  .DCF72inhS1 = 0.00148,
  .DCF72ing = 0.001887,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "U-234",
  .Lambda = 0.000028862371981543,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AT-218",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "BI-214",
  .Lambda = 0.541521234812457,
  .BranchingRatio = 0.9995
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AT-218",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "RN-218",
  .Lambda = 0.541521234812457,
  .BranchingRatio = 0.0005
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RN-218",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PO-214",
  .Lambda = 20.5376942388132,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TL-210",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PB-210",
  .Lambda = 0.00888650231487109,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TL-206",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.00274927487133089,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CM-246",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 148,
  .DCF68inhM5 = 99.9,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.777,
  .DCF72inhF1 = 362.6,
  .DCF72inhM1 = 155.4,
  .DCF72inhS1 = 59.2,
  .DCF72ing = 0.777,
  .A1 = 9,
  .A2 = 0.0009,
  .Daughter = "END",
  .Lambda = 0.00000000000461730265411607,
  .BranchingRatio = 0.0263
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CF-249",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 244.2,
  .DCF68inhM5 = 166.5,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 1.295,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 259,
  .DCF72inhS1 = 0,
  .DCF72ing = 1.295,
  .A1 = 3,
  .A2 = 0.0008,
  .Daughter = "CM-245",
  .Lambda = 0.0000000000626304782595669,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CM-245",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 148,
  .DCF68inhM5 = 99.9,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.777,
  .DCF72inhF1 = 366.3,
  .DCF72inhM1 = 155.4,
  .DCF72inhS1 = 59.2,
  .DCF72ing = 0.777,
  .A1 = 9,
  .A2 = 0.0009,
  .Daughter = "PU-241",
  .Lambda = 0.00000000000266397922688055,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PU-241",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 3.145,
  .DCF68inhM5 = 2.146,
  .DCF68inhS1 = 0.592,
  .DCF68inhS5 = 0.3108,
  .DCF68ing = 0.01739,
  .DCF72inhF1 = 8.51,
  .DCF72inhM1 = 3.33,
  .DCF72inhS1 = 0.629,
  .DCF72ing = 0.01776,
  .A1 = 40,
  .A2 = 0.06,
  .Daughter = "AM-241",
  .Lambda = 0.00000000153287101162887,
  .BranchingRatio = 0.9999753
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PU-241",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 3.145,
  .DCF68inhM5 = 2.146,
  .DCF68inhS1 = 0.592,
  .DCF68inhS5 = 0.3108,
  .DCF68ing = 0.01739,
  .DCF72inhF1 = 8.51,
  .DCF72inhM1 = 3.33,
  .DCF72inhS1 = 0.629,
  .DCF72ing = 0.01776,
  .A1 = 40,
  .A2 = 0.06,
  .Daughter = "U-237",
  .Lambda = 0.00000000153287101162887,
  .BranchingRatio = 0.0000247
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AM-241",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 144.3,
  .DCF68inhM5 = 99.9,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.74,
  .DCF72inhF1 = 355.2,
  .DCF72inhM1 = 155.4,
  .DCF72inhS1 = 59.2,
  .DCF72ing = 0.74,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "NP-237",
  .Lambda = 0.0000000000507732517929499,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NP-237",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 77.7,
  .DCF68inhM5 = 55.5,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.407,
  .DCF72inhF1 = 185,
  .DCF72inhM1 = 85.1,
  .DCF72inhS1 = 44.4,
  .DCF72ing = 0.407,
  .A1 = 20,
  .A2 = 0.002,
  .Daughter = "PA-233",
  .Lambda = 0.0000000000000102637891241262,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PA-233",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.01147,
  .DCF68inhM5 = 0.01036,
  .DCF68inhS1 = 0.01369,
  .DCF68inhS5 = 0.01184,
  .DCF68ing = 0.003219,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.01221,
  .DCF72inhS1 = 0.01443,
  .DCF72ing = 0.003219,
  .A1 = 5,
  .A2 = 0.7,
  .Daughter = "U-233",
  .Lambda = 0.000000297406369306262,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "U-233",
  .DCF68inhF1 = 2.109,
  .DCF68inhF5 = 2.442,
  .DCF68inhM1 = 11.84,
  .DCF68inhM5 = 8.14,
  .DCF68inhS1 = 32.19,
  .DCF68inhS5 = 25.53,
  .DCF68ing = 0.185,
  .DCF72inhF1 = 2.146,
  .DCF72inhM1 = 13.32,
  .DCF72inhS1 = 35.52,
  .DCF72ing = 0.1887,
  .A1 = 40,
  .A2 = 0.006,
  .Daughter = "TH-229",
  .Lambda = 0.000000000000138141564312139,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TH-229",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 366.3,
  .DCF68inhM5 = 255.3,
  .DCF68inhS1 = 240.5,
  .DCF68inhS5 = 177.6,
  .DCF68ing = 1.776,
  .DCF72inhF1 = 888,
  .DCF72inhM1 = 407,
  .DCF72inhS1 = 262.7,
  .DCF72ing = 1.813,
  .A1 = 5,
  .A2 = 0.0005,
  .Daughter = "RA-225",
  .Lambda = 0.00000000000277768052173634,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RA-225",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 21.46,
  .DCF68inhM5 = 17.76,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.3515,
  .DCF72inhF1 = 0.481,
  .DCF72inhM1 = 23.31,
  .DCF72inhS1 = 28.49,
  .DCF72ing = 0.3663,
  .A1 = 0.2,
  .A2 = 0.004,
  .Daughter = "AC-225",
  .Lambda = 0.000000542063298110568,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AC-225",
  .DCF68inhF1 = 3.219,
  .DCF68inhF5 = 3.7,
  .DCF68inhM1 = 25.53,
  .DCF68inhM5 = 21.09,
  .DCF68inhS1 = 29.23,
  .DCF68inhS5 = 24.05,
  .DCF68ing = 0.0888,
  .DCF72inhF1 = 3.256,
  .DCF72inhM1 = 27.38,
  .DCF72inhS1 = 31.45,
  .DCF72ing = 0.0888,
  .A1 = 0.8,
  .A2 = 0.006,
  .Daughter = "FR-221",
  .Lambda = 0.000000808805001717552,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "FR-221",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "AT-217",
  .Lambda = 0.00240625973949853,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AT-217",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "BI-213",
  .Lambda = 21.2621834527591,
  .BranchingRatio = 0.99993
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AT-217",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "RN-217",
  .Lambda = 21.2621834527591,
  .BranchingRatio = 0.00007
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-213",
  .DCF68inhF1 = 0.0407,
  .DCF68inhF5 = 0.0666,
  .DCF68inhM1 = 0.1073,
  .DCF68inhM5 = 0.1517,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00074,
  .DCF72inhF1 = 0.037,
  .DCF72inhM1 = 0.111,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00074,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PO-213",
  .Lambda = 0.000253304383303713,
  .BranchingRatio = 0.9786
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TL-209",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PB-209",
  .Lambda = 0.00534341027258669,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PB-209",
  .DCF68inhF1 = 0.0000666,
  .DCF68inhF5 = 0.0001184,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0002109,
  .DCF72inhF1 = 0.0000629,
  .DCF72inhM1 = 0.0002072,
  .DCF72inhS1 = 0.0002257,
  .DCF72ing = 0.0002109,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.0000595180474463288,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "U-237",
  .DCF68inhF1 = 0.000703,
  .DCF68inhF5 = 0.001221,
  .DCF68inhM1 = 0.00592,
  .DCF68inhM5 = 0.00555,
  .DCF68inhS1 = 0.00666,
  .DCF68inhS5 = 0.00629,
  .DCF68ing = 0.002849,
  .DCF72inhF1 = 0.000666,
  .DCF72inhM1 = 0.00629,
  .DCF72inhS1 = 0.00703,
  .DCF72ing = 0.002812,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "NP-237",
  .Lambda = 0.00000118817192121392,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RN-217",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PO-213",
  .Lambda = 1174.82572976262,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-213",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PB-209",
  .Lambda = 187033.777808944,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CM-244",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 92.5,
  .DCF68inhM5 = 62.9,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.444,
  .DCF72inhF1 = 210.9,
  .DCF72inhM1 = 99.9,
  .DCF72inhS1 = 48.1,
  .DCF72ing = 0.444,
  .A1 = 20,
  .A2 = 0.002,
  .Daughter = "PU-240",
  .Lambda = 0.00000000121270476621191,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PU-240",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 173.9,
  .DCF68inhM5 = 118.4,
  .DCF68inhS1 = 55.5,
  .DCF68inhS5 = 30.71,
  .DCF68ing = 0.925,
  .DCF72inhF1 = 444,
  .DCF72inhM1 = 185,
  .DCF72inhS1 = 59.2,
  .DCF72ing = 0.925,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "U-236",
  .Lambda = 0.00000000000334712576965501,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "U-236",
  .DCF68inhF1 = 1.924,
  .DCF68inhF5 = 2.257,
  .DCF68inhM1 = 10.73,
  .DCF68inhM5 = 7.03,
  .DCF68inhS1 = 29.23,
  .DCF68inhS5 = 23.31,
  .DCF68ing = 0.1702,
  .DCF72inhF1 = 1.961,
  .DCF72inhM1 = 11.84,
  .DCF72inhS1 = 32.19,
  .DCF72ing = 0.1739,
  .A1 = 40,
  .A2 = 0.006,
  .Daughter = "TH-232",
  .Lambda = 0.000000000000000938654219044022,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TH-232",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 155.4,
  .DCF68inhM5 = 107.3,
  .DCF68inhS1 = 85.1,
  .DCF68inhS5 = 44.4,
  .DCF68ing = 0.814,
  .DCF72inhF1 = 407,
  .DCF72inhM1 = 166.5,
  .DCF72inhS1 = 92.5,
  .DCF72ing = 0.851,
  .A1 = -1,
  .A2 = -1,
  .Daughter = "RA-228",
  .Lambda = 1.55776657628582E-18,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RA-228",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 9.62,
  .DCF68inhM5 = 6.29,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 2.479,
  .DCF72inhF1 = 3.33,
  .DCF72inhM1 = 9.62,
  .DCF72inhS1 = 59.2,
  .DCF72ing = 2.553,
  .A1 = 0.6,
  .A2 = 0.02,
  .Daughter = "AC-228",
  .Lambda = 0.00000000381991456097915,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AC-228",
  .DCF68inhF1 = 0.0925,
  .DCF68inhF5 = 0.1073,
  .DCF68inhM1 = 0.0592,
  .DCF68inhM5 = 0.0444,
  .DCF68inhS1 = 0.0518,
  .DCF68inhS5 = 0.0444,
  .DCF68ing = 0.001591,
  .DCF72inhF1 = 0.0925,
  .DCF72inhM1 = 0.0629,
  .DCF72inhS1 = 0.0592,
  .DCF72ing = 0.001591,
  .A1 = 0.6,
  .A2 = 0.5,
  .Daughter = "TH-228",
  .Lambda = 0.0000313074607298982,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TH-228",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 111,
  .DCF68inhM5 = 81.4,
  .DCF68inhS1 = 136.9,
  .DCF68inhS5 = 92.5,
  .DCF68ing = 0.2664,
  .DCF72inhF1 = 111,
  .DCF72inhM1 = 118.4,
  .DCF72inhS1 = 148,
  .DCF72ing = 0.2664,
  .A1 = 0.5,
  .A2 = 0.001,
  .Daughter = "RA-224",
  .Lambda = 0.0000000114901175589193,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RA-224",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 10.73,
  .DCF68inhM5 = 8.88,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.2405,
  .DCF72inhF1 = 0.2775,
  .DCF72inhM1 = 11.1,
  .DCF72inhS1 = 12.58,
  .DCF72ing = 0.2405,
  .A1 = 0.4,
  .A2 = 0.02,
  .Daughter = "RN-220",
  .Lambda = 0.0000022092740374071,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RN-220",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PO-216",
  .Lambda = 0.0124666759093515,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-216",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PB-212",
  .Lambda = 4.81352208722184,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PB-212",
  .DCF68inhF1 = 0.0703,
  .DCF68inhF5 = 0.1221,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.02183,
  .DCF72inhF1 = 0.0666,
  .DCF72inhM1 = 0.629,
  .DCF72inhS1 = 0.703,
  .DCF72ing = 0.0222,
  .A1 = 0.7,
  .A2 = 0.2,
  .Daughter = "BI-212",
  .Lambda = 0.0000181163797035071,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-212",
  .DCF68inhF1 = 0.03441,
  .DCF68inhF5 = 0.0555,
  .DCF68inhM1 = 0.111,
  .DCF68inhM5 = 0.1443,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000962,
  .DCF72inhF1 = 0.03367,
  .DCF72inhM1 = 0.1147,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.000962,
  .A1 = 0.7,
  .A2 = 0.6,
  .Daughter = "PO-212",
  .Lambda = 0.000190788806284494,
  .BranchingRatio = 0.6406
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-212",
  .DCF68inhF1 = 0.03441,
  .DCF68inhF5 = 0.0555,
  .DCF68inhM1 = 0.111,
  .DCF68inhM5 = 0.1443,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000962,
  .DCF72inhF1 = 0.03367,
  .DCF72inhM1 = 0.1147,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.000962,
  .A1 = 0.7,
  .A2 = 0.6,
  .Daughter = "TL-208",
  .Lambda = 0.000190788806284494,
  .BranchingRatio = 0.3594
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-212",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 2349651.45952524,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TL-208",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.00378396757593594,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CF-251",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 247.9,
  .DCF68inhM5 = 170.2,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 1.332,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 262.7,
  .DCF72inhS1 = 0,
  .DCF72ing = 1.332,
  .A1 = 7,
  .A2 = 0.0007,
  .Daughter = "CM-247",
  .Lambda = 0.0000000000244593638370046,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CM-247",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 133.2,
  .DCF68inhM5 = 92.5,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.703,
  .DCF72inhF1 = 333,
  .DCF72inhM1 = 144.3,
  .DCF72inhS1 = 51.8,
  .DCF72ing = 0.703,
  .A1 = 3,
  .A2 = 0.001,
  .Daughter = "PU-243",
  .Lambda = 0.00000000000000140798132856603,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PU-243",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.0003034,
  .DCF68inhM5 = 0.000407,
  .DCF68inhS1 = 0.0003145,
  .DCF68inhS5 = 0.000407,
  .DCF68ing = 0.0003145,
  .DCF72inhF1 = 0.0001184,
  .DCF72inhM1 = 0.0003071,
  .DCF72inhS1 = 0.0003182,
  .DCF72ing = 0.0003145,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "AM-243",
  .Lambda = 0.0000388578977777747,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AM-243",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 144.3,
  .DCF68inhM5 = 99.9,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.74,
  .DCF72inhF1 = 355.2,
  .DCF72inhM1 = 151.7,
  .DCF72inhS1 = 55.5,
  .DCF72ing = 0.74,
  .A1 = 5,
  .A2 = 0.001,
  .Daughter = "NP-239",
  .Lambda = 0.00000000000299040282173317,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NP-239",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.00333,
  .DCF68inhM5 = 0.00407,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00296,
  .DCF72inhF1 = 0.000629,
  .DCF72inhM1 = 0.003441,
  .DCF72inhS1 = 0.0037,
  .DCF72ing = 0.00296,
  .A1 = 7,
  .A2 = 0.4,
  .Daughter = "PU-239",
  .Lambda = 0.00000340544053486561,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PU-239",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 173.9,
  .DCF68inhM5 = 118.4,
  .DCF68inhS1 = 55.5,
  .DCF68inhS5 = 30.71,
  .DCF68ing = 0.925,
  .DCF72inhF1 = 444,
  .DCF72inhM1 = 185,
  .DCF72inhS1 = 59.2,
  .DCF72ing = 0.925,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "U-235",
  .Lambda = 0.000000000000911390403553117,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "U-235",
  .DCF68inhF1 = 1.887,
  .DCF68inhF5 = 2.22,
  .DCF68inhM1 = 10.36,
  .DCF68inhM5 = 6.66,
  .DCF68inhS1 = 28.49,
  .DCF68inhS5 = 22.57,
  .DCF68ing = 0.1702,
  .DCF72inhF1 = 1.924,
  .DCF72inhM1 = 11.47,
  .DCF72inhS1 = 31.45,
  .DCF72ing = 0.1739,
  .A1 = -1,
  .A2 = -1,
  .Daughter = "TH-231",
  .Lambda = 3.11995862579973E-17,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TH-231",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.001073,
  .DCF68inhM5 = 0.001369,
  .DCF68inhS1 = 0.001184,
  .DCF68inhS5 = 0.00148,
  .DCF68ing = 0.001258,
  .DCF72inhF1 = 0.0002886,
  .DCF72inhM1 = 0.001147,
  .DCF72inhS1 = 0.001221,
  .DCF72ing = 0.001258,
  .A1 = 40,
  .A2 = 0.02,
  .Daughter = "PA-231",
  .Lambda = 0.00000754470546586496,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PA-231",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 481,
  .DCF68inhM5 = 329.3,
  .DCF68inhS1 = 118.4,
  .DCF68inhS5 = 62.9,
  .DCF68ing = 2.627,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 518,
  .DCF72inhS1 = 125.8,
  .DCF72ing = 2.627,
  .A1 = 4,
  .A2 = 0.0004,
  .Daughter = "AC-227",
  .Lambda = 0.000000000000671697514545264,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AC-227",
  .DCF68inhF1 = 1998,
  .DCF68inhF5 = 2331,
  .DCF68inhM1 = 777,
  .DCF68inhM5 = 555,
  .DCF68inhS1 = 244.2,
  .DCF68inhS5 = 173.9,
  .DCF68ing = 4.07,
  .DCF72inhF1 = 2035,
  .DCF72inhM1 = 814,
  .DCF72inhS1 = 266.4,
  .DCF72ing = 4.07,
  .A1 = 0.9,
  .A2 = 0.00009,
  .Daughter = "TH-227",
  .Lambda = 0.0000000010088188644221,
  .BranchingRatio = 0.9862
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AC-227",
  .DCF68inhF1 = 1998,
  .DCF68inhF5 = 2331,
  .DCF68inhM1 = 777,
  .DCF68inhM5 = 555,
  .DCF68inhS1 = 244.2,
  .DCF68inhS5 = 173.9,
  .DCF68ing = 4.07,
  .DCF72inhF1 = 2035,
  .DCF72inhM1 = 814,
  .DCF72inhS1 = 266.4,
  .DCF72ing = 4.07,
  .A1 = 0.9,
  .A2 = 0.00009,
  .Daughter = "FR-223",
  .Lambda = 0.0000000010088188644221,
  .BranchingRatio = 0.0138
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TH-227",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 28.86,
  .DCF68inhM5 = 22.94,
  .DCF68inhS1 = 35.52,
  .DCF68inhS5 = 28.12,
  .DCF68ing = 0.03293,
  .DCF72inhF1 = 2.479,
  .DCF72inhM1 = 31.45,
  .DCF72inhS1 = 37,
  .DCF72ing = 0.03256,
  .A1 = 10,
  .A2 = 0.005,
  .Daughter = "RA-223",
  .Lambda = 0.000000429159541877242,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RA-223",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 25.53,
  .DCF68inhM5 = 21.09,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.37,
  .DCF72inhF1 = 0.444,
  .DCF72inhM1 = 27.38,
  .DCF72inhS1 = 32.19,
  .DCF72ing = 0.37,
  .A1 = 0.4,
  .A2 = 0.007,
  .Daughter = "RN-219",
  .Lambda = 0.000000701565063316462,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RN-219",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PO-215",
  .Lambda = 0.175037166808067,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-215",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PB-211",
  .Lambda = 389.189882403114,
  .BranchingRatio = 0.9999977
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-215",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "AT-215",
  .Lambda = 389.189882403114,
  .BranchingRatio = 0.00000230000000001063
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PB-211",
  .DCF68inhF1 = 0.01443,
  .DCF68inhF5 = 0.02072,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000666,
  .DCF72inhF1 = 0.01443,
  .DCF72inhM1 = 0.0407,
  .DCF72inhS1 = 0.0444,
  .DCF72ing = 0.000666,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "BI-211",
  .Lambda = 0.000319437384469305,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-211",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "TL-207",
  .Lambda = 0.00539834252772543,
  .BranchingRatio = 0.99724
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-211",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PO-211",
  .Lambda = 0.00539834252772543,
  .BranchingRatio = 0.00275999999999998
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TL-207",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.00242189790552042,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "FR-223",
  .DCF68inhF1 = 0.003367,
  .DCF68inhF5 = 0.00481,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00851,
  .DCF72inhF1 = 0.003293,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00888,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "AT-219",
  .Lambda = 0.000525350296013298,
  .BranchingRatio = 0.000059999999999949
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AT-215",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "BI-211",
  .Lambda = 18733.7075827012,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PO-211",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 1.34330848945726,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CM-243",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 107.3,
  .DCF68inhM5 = 74,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.555,
  .DCF72inhF1 = 255.3,
  .DCF72inhM1 = 114.7,
  .DCF72inhS1 = 55.5,
  .DCF72ing = 0.555,
  .A1 = 9,
  .A2 = 0.001,
  .Daughter = "PU-239",
  .Lambda = 0.000000000752724767842019,
  .BranchingRatio = 0.9971
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CM-243",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 107.3,
  .DCF68inhM5 = 74,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.555,
  .DCF72inhF1 = 255.3,
  .DCF72inhM1 = 114.7,
  .DCF72inhS1 = 55.5,
  .DCF72ing = 0.555,
  .A1 = 9,
  .A2 = 0.001,
  .Daughter = "AM-243",
  .Lambda = 0.000000000752724767842019,
  .BranchingRatio = 0.00290000000000001
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AM-242",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.0592,
  .DCF68inhM5 = 0.0444,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00111,
  .DCF72inhF1 = 0.0407,
  .DCF72inhM1 = 0.0629,
  .DCF72inhS1 = 0.074,
  .DCF72ing = 0.00111,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "CM-242",
  .Lambda = 0.0000120187817408785,
  .BranchingRatio = 0.83
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AM-242",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.0592,
  .DCF68inhM5 = 0.0444,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00111,
  .DCF72inhF1 = 0.0407,
  .DCF72inhM1 = 0.0629,
  .DCF72inhS1 = 0.074,
  .DCF72ing = 0.00111,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PU-242",
  .Lambda = 0.0000120187817408785,
  .BranchingRatio = 0.17
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CM-242",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 17.76,
  .DCF68inhM5 = 13.69,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0444,
  .DCF72inhF1 = 12.21,
  .DCF72inhM1 = 19.24,
  .DCF72inhS1 = 21.83,
  .DCF72ing = 0.0444,
  .A1 = 40,
  .A2 = 0.01,
  .Daughter = "PU-238",
  .Lambda = 0.0000000492542780699681,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PU-238",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 159.1,
  .DCF68inhM5 = 111,
  .DCF68inhS1 = 55.5,
  .DCF68inhS5 = 40.7,
  .DCF68ing = 0.851,
  .DCF72inhF1 = 407,
  .DCF72inhM1 = 170.2,
  .DCF72inhS1 = 59.2,
  .DCF72ing = 0.851,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "U-234",
  .Lambda = 0.000000000250450498581871,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AM-242M",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 129.5,
  .DCF68inhM5 = 88.8,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.703,
  .DCF72inhF1 = 340.4,
  .DCF72inhM1 = 136.9,
  .DCF72inhS1 = 40.7,
  .DCF72ing = 0.703,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "AM-242",
  .Lambda = 0.000000000154788645000917,
  .BranchingRatio = 0.9955
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AM-242M",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 129.5,
  .DCF68inhM5 = 88.8,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.703,
  .DCF72inhF1 = 340.4,
  .DCF72inhM1 = 136.9,
  .DCF72inhS1 = 40.7,
  .DCF72ing = 0.703,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "NP-238",
  .Lambda = 0.000000000154788645000917,
  .BranchingRatio = 0.0045
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NP-238",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.0074,
  .DCF68inhM5 = 0.00629,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.003367,
  .DCF72inhF1 = 0.01295,
  .DCF72inhM1 = 0.00777,
  .DCF72inhS1 = 0.00555,
  .DCF72ing = 0.003367,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PU-238",
  .Lambda = 0.00000382152946793522,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NP-236",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 11.1,
  .DCF68inhM5 = 7.4,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0629,
  .DCF72inhF1 = 29.6,
  .DCF72inhM1 = 11.84,
  .DCF72inhS1 = 3.7,
  .DCF72ing = 0.0629,
  .A1 = 9,
  .A2 = 0.02,
  .Daughter = "U-236",
  .Lambda = 0.000000000000141706507907291,
  .BranchingRatio = 0.88
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NP-236",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 11.1,
  .DCF68inhM5 = 7.4,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0629,
  .DCF72inhF1 = 29.6,
  .DCF72inhM1 = 11.84,
  .DCF72inhS1 = 3.7,
  .DCF72ing = 0.0629,
  .A1 = 9,
  .A2 = 0.02,
  .Daughter = "PU-236",
  .Lambda = 0.000000000000141706507907291,
  .BranchingRatio = 0.12
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PU-236",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 66.6,
  .DCF68inhM5 = 48.1,
  .DCF68inhS1 = 35.52,
  .DCF68inhS5 = 27.38,
  .DCF68ing = 0.3182,
  .DCF72inhF1 = 148,
  .DCF72inhM1 = 74,
  .DCF72inhS1 = 37,
  .DCF72ing = 0.3219,
  .A1 = 30,
  .A2 = 0.003,
  .Daughter = "U-232",
  .Lambda = 0.00000000768527247222888,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "U-232",
  .DCF68inhF1 = 14.8,
  .DCF68inhF5 = 17.39,
  .DCF68inhM1 = 26.64,
  .DCF68inhM5 = 17.76,
  .DCF68inhS1 = 129.5,
  .DCF68inhS5 = 96.2,
  .DCF68ing = 0.1369,
  .DCF72inhF1 = 14.8,
  .DCF72inhM1 = 28.86,
  .DCF72inhS1 = 136.9,
  .DCF72ing = 1.221,
  .A1 = 10,
  .A2 = 0.001,
  .Daughter = "TH-228",
  .Lambda = 0.000000000318788225335706,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "FR-223",
  .DCF68inhF1 = 0.003367,
  .DCF68inhF5 = 0.00481,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00851,
  .DCF72inhF1 = 0.003293,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00888,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "RA-223",
  .Lambda = 0.000525350296013298,
  .BranchingRatio = 0.99994
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AT-219",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "BI-215",
  .Lambda = 0.0123776282242847,
  .BranchingRatio = 0.936
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "AT-219",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "RN-219",
  .Lambda = 0.0123776282242847,
  .BranchingRatio = 0.064
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-215",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "PO-215",
  .Lambda = 0.00152005960649111,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BI-213",
  .DCF68inhF1 = 0.0407,
  .DCF68inhF5 = 0.0666,
  .DCF68inhM1 = 0.1073,
  .DCF68inhM5 = 0.1517,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00074,
  .DCF72inhF1 = 0.037,
  .DCF72inhM1 = 0.111,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00074,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "TL-209",
  .Lambda = 0.000253304383303713,
  .BranchingRatio = 0.0214
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "EU-155",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.02405,
  .DCF68inhM5 = 0.01739,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.001184,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.02553,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.001184,
  .A1 = 20,
  .A2 = 3,
  .Daughter = "END",
  .Lambda = 0.00000000463190820869467,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "EU-154",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.185,
  .DCF68inhM5 = 0.1295,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0074,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.1961,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.0074,
  .A1 = 0.9,
  .A2 = 0.6,
  .Daughter = "END",
  .Lambda = 0.00000000255668824649402,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "EU-152",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.1443,
  .DCF68inhM5 = 0.0999,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00518,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.1554,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00518,
  .A1 = 1,
  .A2 = 1,
  .Daughter = "END",
  .Lambda = 0.0000000016249544074595,
  .BranchingRatio = 0.7208
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "EU-152",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.1443,
  .DCF68inhM5 = 0.0999,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00518,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.1554,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00518,
  .A1 = 1,
  .A2 = 1,
  .Daughter = "GD-152",
  .Lambda = 0.0000000016249544074595,
  .BranchingRatio = 0.2792
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "GD-152",
  .DCF68inhF1 = 70.3,
  .DCF68inhF5 = 81.4,
  .DCF68inhM1 = 27.38,
  .DCF68inhM5 = 18.5,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.1517,
  .DCF72inhF1 = 70.3,
  .DCF72inhM1 = 29.6,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.1517,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "SM-148",
  .Lambda = 2.03375080792872E-22,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SM-148",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "ND-144",
  .Lambda = 3.21118548620323E-24,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "ND-144",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 9.59148852647604E-24,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SM-151",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.01369,
  .DCF68inhM5 = 0.00962,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0003626,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.0148,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.0003626,
  .A1 = 40,
  .A2 = 10,
  .Daughter = "END",
  .Lambda = 0.000000000232182967501376,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PM-147",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.01739,
  .DCF68inhM5 = 0.01295,
  .DCF68inhS1 = 0.01702,
  .DCF68inhS5 = 0.01184,
  .DCF68ing = 0.000962,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.0185,
  .DCF72inhS1 = 0.01813,
  .DCF72ing = 0.000962,
  .A1 = 40,
  .A2 = 2,
  .Daughter = "SM-147",
  .Lambda = 0.00000000837240749764817,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SM-147",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 32.93,
  .DCF68inhM5 = 22.57,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.1813,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 35.52,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.1813,
  .A1 = -1,
  .A2 = -1,
  .Daughter = "END",
  .Lambda = 2.05275782482525E-19,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PM-146",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.0703,
  .DCF68inhM5 = 0.0481,
  .DCF68inhS1 = 0.0592,
  .DCF68inhS5 = 0.0333,
  .DCF68ing = 0.00333,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.0777,
  .DCF72inhS1 = 0.0629,
  .DCF72ing = 0.00333,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.00000000397188222886621,
  .BranchingRatio = 0.657
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PM-146",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.0703,
  .DCF68inhM5 = 0.0481,
  .DCF68inhS1 = 0.0592,
  .DCF68inhS5 = 0.0333,
  .DCF68ing = 0.00333,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.0777,
  .DCF72inhS1 = 0.0629,
  .DCF72ing = 0.00333,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "SM-146",
  .Lambda = 0.00000000397188222886621,
  .BranchingRatio = 0.343
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SM-146",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 36.63,
  .DCF68inhM5 = 24.79,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.1998,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 40.7,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.1998,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.000000000000000323007481259267,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CE-144",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.1258,
  .DCF68inhM5 = 0.0851,
  .DCF68inhS1 = 0.1813,
  .DCF68inhS5 = 0.1073,
  .DCF68ing = 0.01924,
  .DCF72inhF1 = 0.148,
  .DCF72inhM1 = 0.1332,
  .DCF72inhS1 = 0.1961,
  .DCF72ing = 0.01924,
  .A1 = 0.2,
  .A2 = 0.2,
  .Daughter = "PR-144",
  .Lambda = 0.000000028160516178529,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PR-144",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.0000666,
  .DCF68inhM5 = 0.0001073,
  .DCF68inhS1 = 0.0000703,
  .DCF68inhS5 = 0.000111,
  .DCF68ing = 0.000185,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.0000666,
  .DCF72inhS1 = 0.0000666,
  .DCF72ing = 0.000185,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "ND-144",
  .Lambda = 0.000668544734336367,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CS-137",
  .DCF68inhF1 = 0.01776,
  .DCF68inhF5 = 0.02479,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0481,
  .DCF72inhF1 = 0.01702,
  .DCF72inhM1 = 0.03589,
  .DCF72inhS1 = 0.1443,
  .DCF72ing = 0.0481,
  .A1 = 2,
  .A2 = 0.6,
  .Daughter = "BA-137M",
  .Lambda = 0.000000000731979495638688,
  .BranchingRatio = 0.947
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CS-137",
  .DCF68inhF1 = 0.01776,
  .DCF68inhF5 = 0.02479,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0481,
  .DCF72inhF1 = 0.01702,
  .DCF72inhM1 = 0.03589,
  .DCF72inhS1 = 0.1443,
  .DCF72ing = 0.0481,
  .A1 = 2,
  .A2 = 0.6,
  .Daughter = "END",
  .Lambda = 0.000000000731979495638688,
  .BranchingRatio = 0.053
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "BA-137M",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.00452717807403888,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CS-135",
  .DCF68inhF1 = 0.002627,
  .DCF68inhF5 = 0.003663,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0074,
  .DCF72inhF1 = 0.002553,
  .DCF72inhM1 = 0.01147,
  .DCF72inhS1 = 0.03182,
  .DCF72ing = 0.0074,
  .A1 = 40,
  .A2 = 1,
  .Daughter = "END",
  .Lambda = 0.0000000000000165146682147595,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CS-134",
  .DCF68inhF1 = 0.02516,
  .DCF68inhF5 = 0.03552,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0703,
  .DCF72inhF1 = 0.02442,
  .DCF72inhM1 = 0.03367,
  .DCF72inhS1 = 0.074,
  .DCF72ing = 0.0703,
  .A1 = 0.7,
  .A2 = 0.7,
  .Daughter = "END",
  .Lambda = 0.0000000106334763388992,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "I-131",
  .DCF68inhF1 = 0.02812,
  .DCF68inhF5 = 0.0407,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0814,
  .DCF72inhF1 = 0.02738,
  .DCF72inhM1 = 0.00888,
  .DCF72inhS1 = 0.00592,
  .DCF72ing = 0.0814,
  .A1 = 3,
  .A2 = 0.7,
  .Daughter = "XE-131M",
  .Lambda = 0.000000999730433790223,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "XE-131M",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 40,
  .A2 = 40,
  .Daughter = "END",
  .Lambda = 0.000000672411098150734,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "I-129",
  .DCF68inhF1 = 0.1369,
  .DCF68inhF5 = 0.1887,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.407,
  .DCF72inhF1 = 0.1332,
  .DCF72inhM1 = 0.0555,
  .DCF72inhS1 = 0.03626,
  .DCF72ing = 0.407,
  .A1 = -1,
  .A2 = -1,
  .Daughter = "END",
  .Lambda = 0.0000000000000013642552003497,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SB-126",
  .DCF68inhF1 = 0.00407,
  .DCF68inhF5 = 0.00629,
  .DCF68inhM1 = 0.00999,
  .DCF68inhM5 = 0.01184,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00888,
  .DCF72inhF1 = 0.0037,
  .DCF72inhM1 = 0.01036,
  .DCF72inhS1 = 0.01184,
  .DCF72ing = 0.00888,
  .A1 = 0.4,
  .A2 = 0.4,
  .Daughter = "END",
  .Lambda = 0.000000652238765206212,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SB-126M",
  .DCF68inhF1 = 0.0000481,
  .DCF68inhF5 = 0.0000851,
  .DCF68inhM1 = 0.000074,
  .DCF68inhM5 = 0.0001221,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0001332,
  .DCF72inhF1 = 0.0000444,
  .DCF72inhM1 = 0.0000703,
  .DCF72inhS1 = 0.000074,
  .DCF72ing = 0.0001332,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.000604840471692797,
  .BranchingRatio = 0.814
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SB-126M",
  .DCF68inhF1 = 0.0000481,
  .DCF68inhF5 = 0.0000851,
  .DCF68inhM1 = 0.000074,
  .DCF68inhM5 = 0.0001221,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0001332,
  .DCF72inhF1 = 0.0000444,
  .DCF72inhM1 = 0.0000703,
  .DCF72inhS1 = 0.000074,
  .DCF72ing = 0.0001332,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "SB-126",
  .Lambda = 0.000604840471692797,
  .BranchingRatio = 0.186
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SN-126",
  .DCF68inhF1 = 0.0407,
  .DCF68inhF5 = 0.0518,
  .DCF68inhM1 = 0.0999,
  .DCF68inhM5 = 0.0666,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.01739,
  .DCF72inhF1 = 0.0407,
  .DCF72inhM1 = 0.1036,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.01739,
  .A1 = 0.6,
  .A2 = 0.4,
  .Daughter = "SB-126M",
  .Lambda = 0.000000000000110931862250657,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "I-125",
  .DCF68inhF1 = 0.01961,
  .DCF68inhF5 = 0.02701,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0555,
  .DCF72inhF1 = 0.01887,
  .DCF72inhM1 = 0.00518,
  .DCF72inhS1 = 0.001406,
  .DCF72ing = 0.0555,
  .A1 = 20,
  .A2 = 3,
  .Daughter = "END",
  .Lambda = 0.00000013506408990263,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SB-125",
  .DCF68inhF1 = 0.00518,
  .DCF68inhF5 = 0.00629,
  .DCF68inhM1 = 0.01665,
  .DCF68inhM5 = 0.01221,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00407,
  .DCF72inhF1 = 0.00518,
  .DCF72inhM1 = 0.01776,
  .DCF72inhS1 = 0.0444,
  .DCF72ing = 0.00407,
  .A1 = 2,
  .A2 = 1,
  .Daughter = "TE-125M",
  .Lambda = 0.00000000796479266259206,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TE-125M",
  .DCF68inhF1 = 0.001887,
  .DCF68inhF5 = 0.002479,
  .DCF68inhM1 = 0.01221,
  .DCF68inhM5 = 0.01073,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.003219,
  .DCF72inhF1 = 0.001887,
  .DCF72inhM1 = 0.01258,
  .DCF72inhS1 = 0.01554,
  .DCF72ing = 0.003219,
  .A1 = 20,
  .A2 = 0.9,
  .Daughter = "END",
  .Lambda = 0.000000139765449687045,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "I-124",
  .DCF68inhF1 = 0.01665,
  .DCF68inhF5 = 0.02331,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0481,
  .DCF72inhF1 = 0.01628,
  .DCF72inhM1 = 0.00444,
  .DCF72inhS1 = 0.002849,
  .DCF72ing = 0.0481,
  .A1 = 1,
  .A2 = 1,
  .Daughter = "END",
  .Lambda = 0.00000192110555843784,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "I-123",
  .DCF68inhF1 = 0.0002812,
  .DCF68inhF5 = 0.000407,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000777,
  .DCF72inhF1 = 0.0002738,
  .DCF72inhM1 = 0.0002368,
  .DCF72inhS1 = 0.000222,
  .DCF72ing = 0.000777,
  .A1 = 6,
  .A2 = 3,
  .Daughter = "END",
  .Lambda = 0.0000145611691451099,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SN-121M",
  .DCF68inhF1 = 0.00296,
  .DCF68inhF5 = 0.003589,
  .DCF68inhM1 = 0.01554,
  .DCF68inhM5 = 0.01221,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.001406,
  .DCF72inhF1 = 0.00296,
  .DCF72inhM1 = 0.01665,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.001406,
  .A1 = 40,
  .A2 = 0.9,
  .Daughter = "SN-121",
  .Lambda = 0.000000000500330494889069,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SN-121",
  .DCF68inhF1 = 0.0002368,
  .DCF68inhF5 = 0.00037,
  .DCF68inhM1 = 0.000814,
  .DCF68inhM5 = 0.001036,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000851,
  .DCF72inhF1 = 0.000222,
  .DCF72inhM1 = 0.000851,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.000851,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.00000711796242103045,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SN-121M",
  .DCF68inhF1 = 0.00296,
  .DCF68inhF5 = 0.003589,
  .DCF68inhM1 = 0.01554,
  .DCF68inhM5 = 0.01221,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.001406,
  .DCF72inhF1 = 0.00296,
  .DCF72inhM1 = 0.01665,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.001406,
  .A1 = 40,
  .A2 = 0.9,
  .Daughter = "END",
  .Lambda = 0.000000000500330494889069,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CD-113",
  .DCF68inhF1 = 0.444,
  .DCF68inhF5 = 0.518,
  .DCF68inhM1 = 0.1961,
  .DCF68inhM5 = 0.1591,
  .DCF68inhS1 = 0.0925,
  .DCF68inhS5 = 0.0777,
  .DCF68ing = 0.0925,
  .DCF72inhF1 = 0.444,
  .DCF72inhM1 = 0.2035,
  .DCF72inhS1 = 0.0962,
  .DCF72ing = 0.0925,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 2.73190407035201E-24,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CD-113M",
  .DCF68inhF1 = 0.407,
  .DCF68inhF5 = 0.481,
  .DCF68inhM1 = 0.185,
  .DCF68inhM5 = 0.148,
  .DCF68inhS1 = 0.111,
  .DCF68inhS5 = 0.0888,
  .DCF68ing = 0.0851,
  .DCF72inhF1 = 0.407,
  .DCF72inhM1 = 0.1924,
  .DCF72inhS1 = 0.1147,
  .DCF72ing = 0.0851,
  .A1 = 40,
  .A2 = 0.5,
  .Daughter = "END",
  .Lambda = 0.00000000158131812279555,
  .BranchingRatio = 0.99036
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CD-113M",
  .DCF68inhF1 = 0.407,
  .DCF68inhF5 = 0.481,
  .DCF68inhM1 = 0.185,
  .DCF68inhM5 = 0.148,
  .DCF68inhS1 = 0.111,
  .DCF68inhS5 = 0.0888,
  .DCF68ing = 0.0851,
  .DCF72inhF1 = 0.407,
  .DCF72inhM1 = 0.1924,
  .DCF72inhS1 = 0.1147,
  .DCF72ing = 0.0851,
  .A1 = 40,
  .A2 = 0.5,
  .Daughter = "CD-113",
  .Lambda = 0.00000000158131812279555,
  .BranchingRatio = 0.0964
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "PD-107",
  .DCF68inhF1 = 0.0000962,
  .DCF68inhF5 = 0.0001221,
  .DCF68inhM1 = 0.000296,
  .DCF68inhM5 = 0.0001924,
  .DCF68inhS1 = 0.002035,
  .DCF68inhS5 = 0.001073,
  .DCF68ing = 0.0001369,
  .DCF72inhF1 = 0.0000925,
  .DCF72inhM1 = 0.0003145,
  .DCF72inhS1 = 0.002183,
  .DCF72ing = 0.0001369,
  .A1 = -1,
  .A2 = -1,
  .Daughter = "END",
  .Lambda = 0.00000000000000337915518855848,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RU-106",
  .DCF68inhF1 = 0.0296,
  .DCF68inhF5 = 0.03626,
  .DCF68inhM1 = 0.0962,
  .DCF68inhM5 = 0.0629,
  .DCF68inhS1 = 0.2294,
  .DCF68inhS5 = 0.1295,
  .DCF68ing = 0.0259,
  .DCF72inhF1 = 0.02923,
  .DCF72inhM1 = 0.1036,
  .DCF72inhS1 = 0.2442,
  .DCF72ing = 0.0259,
  .A1 = 0.2,
  .A2 = 0.2,
  .Daughter = "RH-106",
  .Lambda = 0.0000000215775600108564,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RH-106",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.0230511200718306,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RH-99",
  .DCF68inhF1 = 0.001221,
  .DCF68inhF5 = 0.001813,
  .DCF68inhM1 = 0.002701,
  .DCF68inhM5 = 0.003034,
  .DCF68inhS1 = 0.003071,
  .DCF68inhS5 = 0.003293,
  .DCF68ing = 0.001887,
  .DCF72inhF1 = 0.001184,
  .DCF72inhM1 = 0.002849,
  .DCF72inhS1 = 0.003219,
  .DCF72ing = 0.001887,
  .A1 = 2,
  .A2 = 2,
  .Daughter = "END",
  .Lambda = 0.000000498294211927727,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TC-99",
  .DCF68inhF1 = 0.001073,
  .DCF68inhF5 = 0.00148,
  .DCF68inhM1 = 0.01443,
  .DCF68inhM5 = 0.01184,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.002886,
  .DCF72inhF1 = 0.001073,
  .DCF72inhM1 = 0.0148,
  .DCF72inhS1 = 0.0481,
  .DCF72ing = 0.002368,
  .A1 = 40,
  .A2 = 0.9,
  .Daughter = "END",
  .Lambda = 0.000000000000104097197751802,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TC-99M",
  .DCF68inhF1 = 0.0000444,
  .DCF68inhF5 = 0.000074,
  .DCF68inhM1 = 0.0000703,
  .DCF68inhM5 = 0.0001073,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0000814,
  .DCF72inhF1 = 0.0000444,
  .DCF72inhM1 = 0.0000703,
  .DCF72inhS1 = 0.000074,
  .DCF72ing = 0.0000814,
  .A1 = 10,
  .A2 = 4,
  .Daughter = "TC-99",
  .Lambda = 0.000032051151680268,
  .BranchingRatio = 0.999963
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "TC-99M",
  .DCF68inhF1 = 0.0000444,
  .DCF68inhF5 = 0.000074,
  .DCF68inhM1 = 0.0000703,
  .DCF68inhM5 = 0.0001073,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0000814,
  .DCF72inhF1 = 0.0000444,
  .DCF72inhM1 = 0.0000703,
  .DCF72inhS1 = 0.000074,
  .DCF72ing = 0.0000814,
  .A1 = 10,
  .A2 = 4,
  .Daughter = "END",
  .Lambda = 0.000032051151680268,
  .BranchingRatio = 0.0000369999999999537
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "ZR-95",
  .DCF68inhF1 = 0.00925,
  .DCF68inhF5 = 0.0111,
  .DCF68inhM1 = 0.01665,
  .DCF68inhM5 = 0.01332,
  .DCF68inhS1 = 0.02035,
  .DCF68inhS5 = 0.01554,
  .DCF68ing = 0.003256,
  .DCF72inhF1 = 0.00925,
  .DCF72inhM1 = 0.01776,
  .DCF72inhS1 = 0.02183,
  .DCF72ing = 0.003515,
  .A1 = 2,
  .A2 = 0.8,
  .Daughter = "NB-95",
  .Lambda = 0.000000125289492941598,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "ZR-93",
  .DCF68inhF1 = 0.0925,
  .DCF68inhF5 = 0.1073,
  .DCF68inhM1 = 0.03552,
  .DCF68inhM5 = 0.02442,
  .DCF68inhS1 = 0.01147,
  .DCF68inhS5 = 0.00629,
  .DCF68ing = 0.001036,
  .DCF72inhF1 = 0.0925,
  .DCF72inhM1 = 0.037,
  .DCF72inhS1 = 0.01221,
  .DCF72ing = 0.00407,
  .A1 = -1,
  .A2 = -1,
  .Daughter = "NB-93M",
  .Lambda = 0.000000000000013642552003497,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NB-95",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.00518,
  .DCF68inhM5 = 0.00481,
  .DCF68inhS1 = 0.00592,
  .DCF68inhS5 = 0.00481,
  .DCF68ing = 0.002146,
  .DCF72inhF1 = 0.002109,
  .DCF72inhM1 = 0.00555,
  .DCF72inhS1 = 0.00666,
  .DCF72ing = 0.002146,
  .A1 = 10,
  .A2 = 1000000,
  .Daughter = "END",
  .Lambda = 0.000000229274293733714,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NB-93M",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.001702,
  .DCF68inhM5 = 0.001073,
  .DCF68inhS1 = 0.00592,
  .DCF68inhS5 = 0.003182,
  .DCF68ing = 0.000444,
  .DCF72inhF1 = 0.000814,
  .DCF72inhM1 = 0.001887,
  .DCF72inhS1 = 0.00666,
  .DCF72ing = 0.000444,
  .A1 = 40,
  .A2 = 30,
  .Daughter = "END",
  .Lambda = 0.00000000136256257603165,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NB-94",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.037,
  .DCF68inhM5 = 0.02664,
  .DCF68inhS1 = 0.1665,
  .DCF68inhS5 = 0.0925,
  .DCF68ing = 0.00629,
  .DCF72inhF1 = 0.02146,
  .DCF72inhM1 = 0.0407,
  .DCF72inhS1 = 0.1813,
  .DCF72ing = 0.00629,
  .A1 = 10,
  .A2 = 1000000,
  .Daughter = "END",
  .Lambda = 0.00000000000107669160419756,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SR-90",
  .DCF68inhF1 = 0.0888,
  .DCF68inhF5 = 0.111,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0.555,
  .DCF68inhS5 = 0.2849,
  .DCF68ing = 0.1036,
  .DCF72inhF1 = 0.0888,
  .DCF72inhM1 = 0.1332,
  .DCF72inhS1 = 0.592,
  .DCF72ing = 0.1036,
  .A1 = 0.3,
  .A2 = 0.3,
  .Daughter = "Y-90",
  .Lambda = 0.000000000759754712059154,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "Y-90",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.00518,
  .DCF68inhM5 = 0.00592,
  .DCF68inhS1 = 0.00555,
  .DCF68inhS5 = 0.00629,
  .DCF68ing = 0.00999,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0.00518,
  .DCF72inhS1 = 0.00555,
  .DCF72ing = 0.00999,
  .A1 = 0.3,
  .A2 = 0.3,
  .Daughter = "END",
  .Lambda = 0.00000300629053319292,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "RB-86",
  .DCF68inhF1 = 0.003552,
  .DCF68inhF5 = 0.00481,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.01036,
  .DCF72inhF1 = 0.003441,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.01036,
  .A1 = 0.5,
  .A2 = 0.5,
  .Daughter = "END",
  .Lambda = 0.000000429679010874426,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "KR-85",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 10,
  .A2 = 10,
  .Daughter = "END",
  .Lambda = 0.00000000204701852056199,
  .BranchingRatio = 0.788
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "KR-85",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 10,
  .A2 = 10,
  .Daughter = "KR-85M",
  .Lambda = 0.00000000204701852056199,
  .BranchingRatio = 0.212
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "KR-85M",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 8,
  .A2 = 3,
  .Daughter = "END",
  .Lambda = 0.0000429778757787664,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "SE-79",
  .DCF68inhF1 = 0.00444,
  .DCF68inhF5 = 0.00592,
  .DCF68inhM1 = 0.01073,
  .DCF68inhM5 = 0.01147,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.01073,
  .DCF72inhF1 = 0.00407,
  .DCF72inhM1 = 0.00962,
  .DCF72inhS1 = 0.02516,
  .DCF72ing = 0.01073,
  .A1 = 40,
  .A2 = 2,
  .Daughter = "END",
  .Lambda = 0.0000000000000671697514545264,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "ZN-65",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0.01073,
  .DCF68inhS5 = 0.01036,
  .DCF68ing = 0.01443,
  .DCF72inhF1 = 0.00814,
  .DCF72inhM1 = 0.00592,
  .DCF72inhS1 = 0.0074,
  .DCF72ing = 0.01443,
  .A1 = 2,
  .A2 = 2,
  .Daughter = "END",
  .Lambda = 0.0000000328886845079999,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CU-64",
  .DCF68inhF1 = 0.0001406,
  .DCF68inhF5 = 0.0002516,
  .DCF68inhM1 = 0.000407,
  .DCF68inhM5 = 0.000555,
  .DCF68inhS1 = 0.000444,
  .DCF68inhS5 = 0.000555,
  .DCF68ing = 0.000444,
  .DCF72inhF1 = 0.0001295,
  .DCF72inhM1 = 0.000407,
  .DCF72inhS1 = 0.000444,
  .DCF72ing = 0.000444,
  .A1 = 6,
  .A2 = 1,
  .Daughter = "END",
  .Lambda = 0.000015159863904263,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NI-63",
  .DCF68inhF1 = 0.001628,
  .DCF68inhF5 = 0.001924,
  .DCF68inhM1 = 0.001628,
  .DCF68inhM5 = 0.001147,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000555,
  .DCF72inhF1 = 0.001628,
  .DCF72inhM1 = 0.001776,
  .DCF72inhS1 = 0.00481,
  .DCF72ing = 0.000555,
  .A1 = 40,
  .A2 = 30,
  .Daughter = "END",
  .Lambda = 0.000000000217901872278077,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CO-60",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.03552,
  .DCF68inhM5 = 0.02627,
  .DCF68inhS1 = 0.1073,
  .DCF68inhS5 = 0.0629,
  .DCF68ing = 0.01258,
  .DCF72inhF1 = 0.01924,
  .DCF72inhM1 = 0.037,
  .DCF72inhS1 = 0.1147,
  .DCF72ing = 0.01258,
  .A1 = 0.4,
  .A2 = 0.4,
  .Daughter = "END",
  .Lambda = 0.00000000416704775671222,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CU-59",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "NI-59",
  .Lambda = 0.0084530143970725,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NI-59",
  .DCF68inhF1 = 0.000666,
  .DCF68inhF5 = 0.000814,
  .DCF68inhM1 = 0.000481,
  .DCF68inhM5 = 0.0003478,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0002331,
  .DCF72inhF1 = 0.000666,
  .DCF72inhM1 = 0.000481,
  .DCF72inhS1 = 0.001628,
  .DCF72ing = 0.0002331,
  .A1 = -1,
  .A2 = -1,
  .Daughter = "END",
  .Lambda = 0.000000000000271166774390495,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "FE-59",
  .DCF68inhF1 = 0.00814,
  .DCF68inhF5 = 0.0111,
  .DCF68inhM1 = 0.01295,
  .DCF68inhM5 = 0.01184,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00666,
  .DCF72inhF1 = 0.00814,
  .DCF72inhM1 = 0.01369,
  .DCF72inhS1 = 0.0148,
  .DCF72ing = 0.00666,
  .A1 = 0.9,
  .A2 = 0.9,
  .Daughter = "END",
  .Lambda = 0.000000180301984763151,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "ZN-59",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0,
  .DCF72inhF1 = 0,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "CU-59",
  .Lambda = 3.80640955826439,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CO-57",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.001924,
  .DCF68inhM5 = 0.001443,
  .DCF68inhS1 = 0.003478,
  .DCF68inhS5 = 0.00222,
  .DCF68ing = 0.000777,
  .DCF72inhF1 = 0.000703,
  .DCF72inhM1 = 0.002035,
  .DCF72inhS1 = 0.0037,
  .DCF72ing = 0.000777,
  .A1 = 10,
  .A2 = 10,
  .Daughter = "END",
  .Lambda = 0.0000000295163238117601,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "FE-55",
  .DCF68inhF1 = 0.002849,
  .DCF68inhF5 = 0.003404,
  .DCF68inhM1 = 0.001369,
  .DCF68inhM5 = 0.001221,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.001221,
  .DCF72inhF1 = 0.002849,
  .DCF72inhM1 = 0.001406,
  .DCF72inhS1 = 0.000666,
  .DCF72ing = 0.001221,
  .A1 = 40,
  .A2 = 40,
  .Daughter = "END",
  .Lambda = 0.00000000796970563339264,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "MN-54",
  .DCF68inhF1 = 0.003219,
  .DCF68inhF5 = 0.00407,
  .DCF68inhM1 = 0.00555,
  .DCF68inhM5 = 0.00444,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.002627,
  .DCF72inhF1 = 0.003145,
  .DCF72inhM1 = 0.00555,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.002627,
  .A1 = 1,
  .A2 = 1,
  .Daughter = "END",
  .Lambda = 0.0000000257050202244037,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CR-51",
  .DCF68inhF1 = 0.0000777,
  .DCF68inhF5 = 0.000111,
  .DCF68inhM1 = 0.0001147,
  .DCF68inhM5 = 0.0001258,
  .DCF68inhS1 = 0.0001332,
  .DCF68inhS5 = 0.0001332,
  .DCF68ing = 0.0001406,
  .DCF72inhF1 = 0.000074,
  .DCF72inhM1 = 0.0001184,
  .DCF72inhS1 = 0.0001369,
  .DCF72ing = 0.0001406,
  .A1 = 30,
  .A2 = 30,
  .Daughter = "END",
  .Lambda = 0.000000289618085371923,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CA-45",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0.00999,
  .DCF68inhM5 = 0.00851,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.002812,
  .DCF72inhF1 = 0.001702,
  .DCF72inhM1 = 0.00999,
  .DCF72inhS1 = 0.01369,
  .DCF72ing = 0.002627,
  .A1 = 40,
  .A2 = 1,
  .Daughter = "END",
  .Lambda = 0.0000000493360605869036,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CL-36",
  .DCF68inhF1 = 0.001258,
  .DCF68inhF5 = 0.001813,
  .DCF68inhM1 = 0.02553,
  .DCF68inhM5 = 0.01887,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.003441,
  .DCF72inhF1 = 0.001221,
  .DCF72inhM1 = 0.02701,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.003441,
  .A1 = 10,
  .A2 = 0.6,
  .Daughter = "END",
  .Lambda = 0.0000000000000729717897861466,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "S-35",
  .DCF68inhF1 = 0.0001961,
  .DCF68inhF5 = 0.000296,
  .DCF68inhM1 = 0.00481,
  .DCF68inhM5 = 0.00407,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.002849,
  .DCF72inhF1 = 0.0001887,
  .DCF72inhM1 = 0.00518,
  .DCF72inhS1 = 0.00703,
  .DCF72ing = 0.002849,
  .A1 = 40,
  .A2 = 3,
  .Daughter = "END",
  .Lambda = 0.0000000918435811337883,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "P-33",
  .DCF68inhF1 = 0.0003552,
  .DCF68inhF5 = 0.000518,
  .DCF68inhM1 = 0.00518,
  .DCF68inhM5 = 0.00481,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.000888,
  .DCF72inhF1 = 0.0003404,
  .DCF72inhM1 = 0.00555,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.000888,
  .A1 = 40,
  .A2 = 1,
  .Daughter = "END",
  .Lambda = 0.000000316470880159227,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "P-32",
  .DCF68inhF1 = 0.00296,
  .DCF68inhF5 = 0.00407,
  .DCF68inhM1 = 0.01184,
  .DCF68inhM5 = 0.01073,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.00888,
  .DCF72inhF1 = 0.002849,
  .DCF72inhM1 = 0.01258,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.00888,
  .A1 = 0.5,
  .A2 = 0.5,
  .Daughter = "END",
  .Lambda = 0.000000562314208455625,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "NA-22",
  .DCF68inhF1 = 0.00481,
  .DCF68inhF5 = 0.0074,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.01184,
  .DCF72inhF1 = 0.00481,
  .DCF72inhM1 = 0,
  .DCF72inhS1 = 0,
  .DCF72ing = 0.01184,
  .A1 = 0.5,
  .A2 = 0.5,
  .Daughter = "END",
  .Lambda = 0.00000000844171902287948,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "C-14",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.002146,
  .DCF72inhF1 = 0.00074,
  .DCF72inhM1 = 0.0074,
  .DCF72inhS1 = 0.02146,
  .DCF72ing = 0.002146,
  .A1 = 40,
  .A2 = 3,
  .Daughter = "END",
  .Lambda = 0.00000000000386291043363175,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "H-3",
  .DCF68inhF1 = 0,
  .DCF68inhF5 = 0,
  .DCF68inhM1 = 0,
  .DCF68inhM5 = 0,
  .DCF68inhS1 = 0,
  .DCF68inhS5 = 0,
  .DCF68ing = 0.0001554,
  .DCF72inhF1 = 0.00002294,
  .DCF72inhM1 = 0.0001665,
  .DCF72inhS1 = 0.000962,
  .DCF72ing = 0.0001554,
  .A1 = 0,
  .A2 = 0,
  .Daughter = "END",
  .Lambda = 0.00000000178252966017401,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "CD-109",
  .DCF68inhF1 = 0.02997,
  .DCF68inhF5 = 0.03552,
  .DCF68inhM1 = 0.02294,
  .DCF68inhM5 = 0.01887,
  .DCF68inhS1 = 0.02146,
  .DCF68inhS5 = 0.01628,
  .DCF68ing = 0.0074,
  .DCF72inhF1 = 0.02997,
  .DCF72inhM1 = 0.02442,
  .DCF72inhS1 = 0.02294,
  .DCF72ing = 0.0074,
  .A1 = 30,
  .A2 = 2,
  .Daughter = "END",
  .Lambda = 0.0000000173655500498645,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        dsi = New DecaySeriesItem With {
  .Isotope = "F-18",
  .DCF68inhF1 = 0.000111,
  .DCF68inhF5 = 0.0001998,
  .DCF68inhM1 = 0.0002109,
  .DCF68inhM5 = 0.0003293,
  .DCF68inhS1 = 0.000222,
  .DCF68inhS5 = 0.0003441,
  .DCF68ing = 0.0001813,
  .DCF72inhF1 = 0.0001036,
  .DCF72inhM1 = 0.0002072,
  .DCF72inhS1 = 0.0002183,
  .DCF72ing = 0.0001813,
  .A1 = 1,
  .A2 = 0.6,
  .Daughter = "END",
  .Lambda = 0.000105276878718833,
  .BranchingRatio = 1
}
        dci.Add(dsi)

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        GetDecaySeries = dci

    End Function 'GetDecaySeries

    Public Function GetDecayChain(
    sParent As String, ByVal sTerminal As String,
    ByRef gdcdci() As Collection,
    Optional Instance As Integer = 1,
    Optional currBranch As Integer = 1,
    Optional nextBranch As Integer = 1,
    Optional ByRef pds As Collection = Nothing,
    Optional OptionalSortOrder As Integer = 1) As Boolean
        '* Usage:       Gets decay chain from sParent to sTerminal including all branches
        '* Input:       sParent = starting member isotope
        '*              sTerminal = last member isotope
        '*              gdcdci() = an empty array of Collections of DecaySeriesItems
        '*              Instance = which recursion instance is this
        '*              currBranch = which member of the gdcdci() is being loaded
        '*              nextBranch = which member is next to be loaded
        '* Returns:     Nothing, but gdcdci() is fully loaded and ready for use
        '* Author:      Backscatter enterprises
        '* Date:        8/25/2023

        Dim Msg As String
        Dim x As Integer
        Dim y As Integer
        Dim z As Integer
        Dim bRsp As Boolean

        On Error GoTo 0

        sParent = UCase(sParent)
        sTerminal = UCase(sTerminal)

        If pds Is Nothing Then 'only load pds once per parent, then refer for each daughter in each branch
            pds = New Collection
            pds = GetDecaySeries()
        End If

        'All Isotopes
        If sParent = "ALL" Then
            gdcdci(0) = pds
            GoTo ExitHere
        End If

        'MsgBox("GDC: pds count =" & pds.Count, vbOKOnly) '<<debug>>

        For x = 1 To pds.Count
            If sParent = DirectCast(pds.Item(x).Isotope, String) Then 'sParent found
                If (DirectCast(pds.Item(x).BranchingRatio, Double) <> 1.0#) And (sParent <> sTerminal) Then 'hit a branch point
                    'MsgBox("GDC:  branch point", vbOKOnly) '<<debug>>
                    'find empty branch = nextBranch
                    For y = (currBranch + 1) To maxBranches
                        If (gdcdci(y).Count = 0) And (y <> currBranch) Then
                            nextBranch = y
                            y = maxBranches 'abort loop
                        End If
                    Next y
                    'Copy everything from currBranch to nextBranch
                    For y = 1 To gdcdci(currBranch).Count
                        bRsp = AddDecayChainItem(gdcdci(currBranch).Item(y), gdcdci(nextBranch))
                        If Not bRsp Then
                            'MsgBox("GDC:  Error add decay chain") '<<debug>>
                            GoTo HandleErrors
                        End If

                    Next y

                    'Find next instance of sParent
                    'MsgBox("SParent = " & sParent & ", nextBranch = " & nextBranch & ", Start y = " & x + 1, vbOKOnly) ' <<debug>>
                    For y = x + 1 To pds.Count
                        If sParent = DirectCast(pds.Item(y).Isotope, String) Then 'found next instance
                            'Copy next instance of sParent to nextBranch
                            'MsgBox("Found next instance of " & sParent & " NB = " & nextBranch) '<<debug>>
                            bRsp = AddDecayChainItem(pds.Item(y), gdcdci(nextBranch))
                            If Not bRsp Then
                                GoTo HandleErrors
                            End If
                            y = pds.Count 'abort loop
                        End If
                    Next y
                    'Follow daughter down nextBranch
                    bRsp = GetDecayChain(DirectCast(gdcdci(nextBranch).Item(gdcdci(nextBranch).Count).Daughter, String), sTerminal, gdcdci, Instance + 1, nextBranch)
                    If Not bRsp Then GoTo HandleErrors
                End If

                'Load database info into collection for first parent
                bRsp = AddDecayChainItem(pds.Item(x), gdcdci(currBranch))
                If Not bRsp Then GoTo HandleErrors

                'Continue with first daughter
                If sParent <> sTerminal Then
                    bRsp = GetDecayChain(DirectCast(pds.Item(x).Daughter, String), sTerminal, gdcdci, Instance + 1, currBranch)
                    If Not bRsp Then GoTo HandleErrors
                End If
                x = pds.Count 'abort loop
            End If
        Next x

        'AbortBranchSearch:

        If Instance > 1 Then GoTo ExitHere 'still working branches

        'Kill branches that do not terminate with sTerminal
        For x = 1 To maxBranches
            If gdcdci(x).Count > 0 Then 'trap empty gdcdci(x)
                bRsp = VerifyDecayChain(sParent, sTerminal, gdcdci(x))
                If Not bRsp Then 'did not verify - remove branch
                    For y = 1 To gdcdci(x).Count
                        gdcdci(x).Remove(1)
                    Next y
                End If
            End If
        Next x

        'Make branches contiguous
        bRsp = False 'flag for found an array to pull back
        For x = 1 To maxBranches - 1
            If gdcdci(x).Count = 0 Then 'it's empty
                For y = x To (maxBranches - 1) 'pull the next one back
                    For z = 1 To gdcdci(y + 1).Count
                        gdcdci(y).Add(gdcdci(y + 1).Item(z))
                        bRsp = True 'found one to pull back
                    Next z
                    For z = 1 To gdcdci(y + 1).Count
                        gdcdci(y + 1).Remove(1)
                    Next z
                Next y
            End If
            If bRsp Then
                x = 0
                bRsp = False 'reset to look for double empty array
            End If
        Next x

        'Populate array element zero with key to create unique isotope enumeration
        For x = 1 To maxBranches
            'If Not gdcdci(x) Is Nothing Then
            If gdcdci(x) IsNot Nothing Then
                For y = 1 To gdcdci(x).Count
                    On Error Resume Next
                    gdcdci(0).Add(gdcdci(x).Item(y), DirectCast(gdcdci(x).Item(y).Isotope, String))
                    On Error GoTo 0
                Next y
            End If
        Next x

        'Bubble sort gdcdci(0) on Isotope mass then name
        bRsp = BubbleSortCollection(gdcdci(0), OptionalSortOrder)
        If Not bRsp Then GoTo HandleErrors

ExitHere:
        GetDecayChain = True
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, , "Error")
        End If

        GetDecayChain = False

    End Function 'GetDecayChain

    Public Function VerifyDecayChain(ByVal sParent As String, ByVal sTerminal As String, ByVal vdcdci As Collection) As Boolean
        '* Usage:       Verifies that sParent is the first item and sTerminal is the last item
        '* Input:       sParent = starting member isotope
        '*              sTerminal = last member isotope
        '*              vdcdci = Collection of DecaySeriesItems
        '* Returns:     True if sParent is first and sTerminal is last OR sTerminal is END
        '* Author:      Backscatter enterprises
        '* Date:        12/25/2014

        Dim Msg As String

        If sParent = sTerminal Then
            VerifyDecayChain = True
            GoTo ExitHere
        Else
            VerifyDecayChain = False
        End If

        On Error GoTo HandleErrors

        sParent = UCase(sParent)
        sTerminal = UCase(sTerminal)

        If DirectCast(vdcdci.Item(1).Isotope, String) = sParent And
        (DirectCast(vdcdci.Item(vdcdci.Count).Isotope, String) = sTerminal Or sTerminal = "END") Then
            VerifyDecayChain = True
        End If

ExitHere:
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, , "Error")
        End If

        VerifyDecayChain = False

    End Function 'VerifyDecayChain

    Public Function InitBranches(ByRef gdcdci() As Collection, Optional Branches As Integer = maxBranches) As Boolean
        '* Usage:       Initializes the collection data structure
        '* Input:       gdcdci() - array of Collection object
        '*              Branches - number of collections to initialize in the array of
        '* Returns:     True if successful, otherwise False
        '* Author:      Backscatter enterprises
        '* Date:        12/25/2014

        Dim x As Integer
        Dim Msg As String

        On Error GoTo HandleErrors
        InitBranches = False

        For x = 0 To Branches
            gdcdci(x) = New Collection
        Next x

        InitBranches = True
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, vbCritical, "Error")
        End If

        InitBranches = False

    End Function 'InitBranches

    Public Function ClearBranches(ByRef gdcdci() As Collection, Optional Branches As Integer = maxBranches) As Boolean
        '* Usage:       Clears the collection data structures
        '* Input:       gdcdci() - array of Collection object
        '*              Branches - number of collections to clear in the array of
        '* Returns:     True if successful, otherwise False
        '* Author:      Backscatter enterprises
        '* Date:        12/25/2014

        Dim x As Integer
        Dim Msg As String

        On Error GoTo HandleErrors
        ClearBranches = False

        For x = 0 To Branches
            gdcdci(x) = Nothing
        Next x

        ClearBranches = True
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, vbCritical, "Error")
        End If

        ClearBranches = False

    End Function 'ClearBranches

    Private Shared Function BubbleSortCollection(ByRef gdcdci As Collection, OptionalSortOrder As Integer) As Boolean
        '* Usage:       Sorts gdcdci by .Isotopoe descending mass, then alphabetic symbol
        '* Input:       gdcdci = collection of DecaySeriesItems
        '* Returns:     True sort is successful
        '* Author:      Backscatter enterprises
        '* Date:        12/25/2014

        Dim dsi As DecaySeriesItem
        Dim i As Integer
        Dim j As Integer
        Dim NoExchanges As Boolean
        Dim Msg As String
        Dim bRsp As Boolean

        ' Loop until no more "exchanges" are made.

        If OptionalSortOrder < 2 Or OptionalSortOrder > 3 Then GoTo ExitHere 'No Sort

        j = 2 '***Assert element 1 is primary parent and is in the correct order, alphabetical within decreasing mass
        If OptionalSortOrder = 2 Then j = 1 '***Assert element 1 must be sorted, alphabetical within increasing mass

        Do
            NoExchanges = True
            ' Loop through each element in the array.
            For i = j To gdcdci.Count - 1

                'Substitution when isotope1 is less than the isotope2 following
                '***Assert OptionalSortOrder = {2,3}
                If DSIKeyCompare(DirectCast(gdcdci.Item(i).Isotope, String), DirectCast(gdcdci.Item(i + 1).Isotope, String), OptionalSortOrder) Then
                    NoExchanges = False
                    dsi = New DecaySeriesItem
                    bRsp = LoadDecaySeriesItem(gdcdci.Item(i), dsi)
                    If Not bRsp Then GoTo HandleErrors
                    gdcdci.Remove(i)
                    If i < gdcdci.Count Then
                        gdcdci.Add(dsi, dsi.Isotope, gdcdci.Item(i + 1).Isotope)
                    Else
                        gdcdci.Add(dsi, dsi.Isotope)
                    End If
                End If

            Next i

        Loop While Not (NoExchanges)

ExitHere:
        BubbleSortCollection = True
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, , "Error")
        End If

        BubbleSortCollection = False

    End Function 'BubbleSortCollection

    Private Shared Function DSIKeyCompare(a As String, b As String, Optional OptionalSortOrder As Double = 1) As Boolean
        '* Usage:       Compares the value of a to b for sorting
        '* Input:       a and b are gdcdci.Isotope strings
        '* Returns:     True if mass A < mass B
        '*              False if mass A = mass B AND sym A > sym B
        '* Author:      Backscatter enterprises
        '* Date:        12/25/2014

        '***Assert a<>b

        Dim massA As String
        Dim massB As String
        Dim symA As String
        Dim symB As String
        Dim x As Integer
        Dim Msg As String

        On Error GoTo HandleErrors

        'get mass
        x = InStr(1, a, "-")
        massA = Right(a, Len(a) - x)
        If Right(a, 1) = "M" Then
            massA = Left(massA, Len(massA) - 1) 'clip it
        End If
        x = InStr(1, b, "-")
        massB = Right(b, Len(b) - x)
        If Right(b, 1) = "M" Then
            massB = Left(massB, Len(massB) - 1) 'clip it
        End If

        If Val(massA) <> Val(massB) Then
            Select Case OptionalSortOrder
                Case 2
                    DSIKeyCompare = Val(massA) > Val(massB)
                Case 3
                    DSIKeyCompare = Val(massA) < Val(massB)
                Case Else
                    DSIKeyCompare = True
            End Select
            Exit Function
        End If

        '***Assert masses are equal
        'get symbols
        x = InStr(1, a, "-")
        symA = Left(a, x)
        If Right(a, 1) = "M" Then
            symA += "M"
        End If
        x = InStr(1, b, "-")
        symB = Left(b, x)
        If Right(b, 1) = "M" Then
            symB += "M"
        End If

        DSIKeyCompare = symA > symB
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, , "Error")
        End If

        DSIKeyCompare = False

    End Function 'DSIKeyCompare

    Private Shared Function AddDecayChainItem(fromDCI As Object, ByRef toDCI As Collection) As Boolean
        '* Usage:       Adds a decay chain item to specified collection
        '* Input:       dci is a collection of decay series items
        '* Returns:     True if successful, else False
        '* Author:      Backscatter enterprises
        '* Date:        12/25/2014

        Dim dsi As DecaySeriesItem
        Dim bRsp As Boolean
        Dim Msg As String

        On Error GoTo HandleErrors

        dsi = New DecaySeriesItem
        bRsp = LoadDecaySeriesItem(fromDCI, dsi)
        If Not bRsp Then GoTo HandleErrors
        toDCI.Add(dsi)

        dsi = Nothing

        AddDecayChainItem = True
        Exit Function

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, , "Error")
        End If

        AddDecayChainItem = False

    End Function 'AddDecayChainItem

    Private Shared Function LoadDecaySeriesItem(fromDSI As Object, ByRef toDSI As DecaySeriesItem) As Boolean
        '* Usage:       Loads decay series item to specified collection
        '* Input:       dci is a collection of decay series items
        '* Returns:     True if successful, else False
        '* Author:      Backscatter enterprises
        '* Date:        7/19/2015

        Dim Msg As String

        On Error GoTo HandleError

        toDSI.Isotope = DirectCast(fromDSI.Isotope, String)
        toDSI.DCF68inhF1 = DirectCast(fromDSI.DCF68inhF1, Double)
        toDSI.DCF68inhF5 = DirectCast(fromDSI.DCF68inhF5, Double)
        toDSI.DCF68inhM1 = DirectCast(fromDSI.DCF68inhM1, Double)
        toDSI.DCF68inhM5 = DirectCast(fromDSI.DCF68inhM5, Double)
        toDSI.DCF68inhS1 = DirectCast(fromDSI.DCF68inhS1, Double)
        toDSI.DCF68inhS5 = DirectCast(fromDSI.DCF68inhS5, Double)
        toDSI.DCF68ing = DirectCast(fromDSI.DCF68ing, Double)
        toDSI.DCF72inhF1 = DirectCast(fromDSI.DCF72inhF1, Double)
        toDSI.DCF72inhM1 = DirectCast(fromDSI.DCF72inhM1, Double)
        toDSI.DCF72inhS1 = DirectCast(fromDSI.DCF72inhS1, Double)
        toDSI.DCF72ing = DirectCast(fromDSI.DCF72ing, Double)
        toDSI.A1 = DirectCast(fromDSI.A1, Double)
        toDSI.A2 = DirectCast(fromDSI.A2, Double)
        toDSI.Daughter = DirectCast(fromDSI.Daughter, String)
        toDSI.Lambda = DirectCast(fromDSI.Lambda, Double)
        toDSI.BranchingRatio = DirectCast(fromDSI.BranchingRatio, Double)

        LoadDecaySeriesItem = True
        Exit Function

HandleError:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, , "Error")
        End If

        LoadDecaySeriesItem = False

    End Function 'LoadDecaySeriesItem

    Public Function ListAll(<ExcelArgument(AllowReference:=True)> ByVal uRngVal As String) As Boolean
        '* Usage:       Lists entire database starting at uRng
        '* Input:       uRng = Cell address
        '* Returns:     True if successful, else False
        '* Author:      Backscatter enterprises
        '* Date:        10/29/2016

        On Error GoTo HandleErrors

        Dim pds As Collection
        Dim x As Double
        Dim r As Double
        Dim c As Double
        Dim Msg As String
        Dim iRng As Range
        Dim iSheet As Worksheet
        Dim uSheet As String
        Dim uRng As String = ""
        Dim pattern As String = "\](\w*)'?!(.*)"
        Dim rRegex As New Regex(pattern)
        Dim m As Match = rRegex.Match(uRngVal)

        'Get Regex groups from match
        uSheet = m.Groups(1).ToString
        uRng = m.Groups(2).ToString

        'Create the ranges and worksheet
        iRng = DirectCast(iExcel.Range(uRng), Range)
        iSheet = DirectCast(iExcel.Worksheets(uSheet), Worksheet)
        r = Convert.ToInt32(iRng.Row)
        c = Convert.ToInt32(iRng.Column)

        'Write Headers
        iSheet.Cells(r, c) = "Isotope"
        iSheet.Cells(r, c + 1) = "Lambda (/s)"
        iSheet.Cells(r, c + 2) = "DCF68inhF1 (rem/uCi)"
        iSheet.Cells(r, c + 3) = "DCF68inhF5 (rem/uCi)"
        iSheet.Cells(r, c + 4) = "DCF68inhM1 (rem/uCi)"
        iSheet.Cells(r, c + 5) = "DCF68inhM5 (rem/uCi)"
        iSheet.Cells(r, c + 6) = "DCF68inhS1 (rem/uCi)"
        iSheet.Cells(r, c + 7) = "DCF68inhS5 (rem/uCi)"
        iSheet.Cells(r, c + 8) = "DCF68ing (rem/uCi)"
        iSheet.Cells(r, c + 9) = "DCF72inhF1 (rem/uCi)"
        iSheet.Cells(r, c + 10) = "DCF72inhM1 (rem/uCi)"
        iSheet.Cells(r, c + 11) = "DCF72inhS1"
        iSheet.Cells(r, c + 12) = "DCF72ing (rem/uCi)"
        iSheet.Cells(r, c + 13) = "A1 (TBq)"
        iSheet.Cells(r, c + 14) = "A2 (TBq)"
        iSheet.Cells(r, c + 15) = "Daughter"
        iSheet.Cells(r, c + 16) = "BR"
        r += 1 'increment to the next row

        pds = New Collection
        pds = GetDecaySeries()

        For x = 1 To pds.Count
            iSheet.Cells(r + x - 1, c) = pds.Item(x).Isotope
            iSheet.Cells(r + x - 1, c + 1) = pds.Item(x).Lambda
            iSheet.Cells(r + x - 1, c + 2) = pds.Item(x).DCF68inhF1
            iSheet.Cells(r + x - 1, c + 3) = pds.Item(x).DCF68inhF5
            iSheet.Cells(r + x - 1, c + 4) = pds.Item(x).DCF68inhM1
            iSheet.Cells(r + x - 1, c + 5) = pds.Item(x).DCF68inhM5
            iSheet.Cells(r + x - 1, c + 6) = pds.Item(x).DCF68inhS1
            iSheet.Cells(r + x - 1, c + 7) = pds.Item(x).DCF68inhS5
            iSheet.Cells(r + x - 1, c + 8) = pds.Item(x).DCF68ing
            iSheet.Cells(r + x - 1, c + 9) = pds.Item(x).DCF72inhF1
            iSheet.Cells(r + x - 1, c + 10) = pds.Item(x).DCF72inhM1
            iSheet.Cells(r + x - 1, c + 11) = pds.Item(x).DCF72inhS1
            iSheet.Cells(r + x - 1, c + 12) = pds.Item(x).DCF72ing
            iSheet.Cells(r + x - 1, c + 13) = pds.Item(x).A1
            iSheet.Cells(r + x - 1, c + 14) = pds.Item(x).A2
            iSheet.Cells(r + x - 1, c + 15) = pds.Item(x).Daughter
            iSheet.Cells(r + x - 1, c + 16) = pds.Item(x).BranchingRatio
        Next x

        ListAll = True

        Exit Function

HandleErrors:

        If Err.Number = 1004 Or Err.Number = 9 Then
            Err.Clear()
            ListAll = True
            Exit Function
        End If

        If Err.Number <> 0 Then
            Msg = "List All Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, , "Error")
        End If

        ListAll = False

    End Function 'ListAll

    Private Shared Function ReferenceToRange(<ExcelArgument(AllowReference:=True)> xlref As Object) As Object
        Dim strAddress As String = DirectCast(XlCall.Excel(XlCall.xlfReftext, xlref, True), String)
        ReferenceToRange = ExcelDnaUtil.Application.Range(strAddress)
    End Function

    Public Function VerifyIsotope(uIsotope As String) As Boolean
        '* Usage:       Verifies isotope is in Class
        '* Input:       uIsotope (e.g., CS-137)
        '* Returns:     True if successful, else False
        '* Author:      Backscatter enterprises
        '* Date:        7/31/2015

        On Error GoTo HandleErrors

        Dim pds As Collection
        Dim x As Double
        Dim r As Double
        Dim c As Double
        Dim Msg As String
        Dim uRng As Object

        pds = New Collection
        pds = GetDecaySeries()

        VerifyIsotope = False

        For x = 1 To pds.Count
            If DirectCast(pds.Item(x).Isotope, String) = uIsotope Then
                VerifyIsotope = True
                x = pds.Count 'ends loop
            End If
        Next

HandleErrors:
        If Err.Number <> 0 Then
            Msg = "Error # " & Str(Err.Number) & " was generated by " _
             & Err.Source & Chr(13) & "Error Line: " & Erl() & Chr(13) & Err.Description
            MsgBox(Msg, , "Error")
        End If

    End Function

End Class