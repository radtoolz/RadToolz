Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Excel.Constants
Imports ExcelDna.Integration
Imports ExcelDna.Integration.XlCall
Imports ExcelDna.Integration.XlCallException
Imports ExcelDna.Integration.ExcelIntegration
Imports ExcelDna.Integration.ExcelReference
Imports ExcelDna.Integration.ExcelLimits
Imports System.Text.RegularExpressions

Public Class ProcessDecaySeries

    Public Function GetDecaySeries() As Collection
        '* Usage:       Contains the database for isotopes
        '* Input:       Nothing
        '* Returns:     Nothing
        '* Author:      J. J. Prowse
        '* Date:        7/19/2015

        'Static Dim dci As Collection 'static added to clear rule violation
        Dim dci As Collection
        Dim dsi As DecaySeriesItem

        dci = New Collection

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        dsi = New DecaySeriesItem
        dsi.Isotope = "CM-246"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 148
        dsi.DCF68inhM5 = 99.9
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.777
        dsi.DCF72inhF1 = 362.6
        dsi.DCF72inhM1 = 155.4
        dsi.DCF72inhS1 = 59.2
        dsi.DCF72ing = 0.777
        dsi.Daughter = "PU-242"
        dsi.Lambda = 0.00000000000466765778154845
        dsi.BranchingRatio = 0.999737
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PU-242"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 162.8
        dsi.DCF68inhM5 = 114.7
        dsi.DCF68inhS1 = 51.8
        dsi.DCF68inhS5 = 28.49
        dsi.DCF68ing = 0.888
        dsi.DCF72inhF1 = 407
        dsi.DCF72inhM1 = 177.6
        dsi.DCF72inhS1 = 55.5
        dsi.DCF72ing = 0.888
        dsi.Daughter = "U-238"
        dsi.Lambda = 0.000000000000058592322955194
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "U-238"
        dsi.DCF68inhF1 = 1.813
        dsi.DCF68inhF5 = 2.146
        dsi.DCF68inhM1 = 9.62
        dsi.DCF68inhM5 = 5.92
        dsi.DCF68inhS1 = 27.01
        dsi.DCF68inhS5 = 21.09
        dsi.DCF68ing = 0.1628
        dsi.DCF72inhF1 = 1.85
        dsi.DCF72inhM1 = 10.73
        dsi.DCF72inhS1 = 29.6
        dsi.DCF72ing = 0.1665
        dsi.Daughter = "TH-234"
        dsi.Lambda = 4.91595987592438E-18
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TH-234"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.02331
        dsi.DCF68inhM5 = 0.01961
        dsi.DCF68inhS1 = 0.02701
        dsi.DCF68inhS5 = 0.02146
        dsi.DCF68ing = 0.01258
        dsi.DCF72inhF1 = 0.00925
        dsi.DCF72inhM1 = 0.02442
        dsi.DCF72inhS1 = 0.02849
        dsi.DCF72ing = 0.01258
        dsi.Daughter = "PA-234M"
        dsi.Lambda = 0.000000332885344897776
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PA-234M"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "U-234"
        dsi.Lambda = 0.00996760397699087
        dsi.BranchingRatio = 0.9984
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PA-234M"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PA-234"
        dsi.Lambda = 0.00996760397699087
        dsi.BranchingRatio = 0.0016
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "U-234"
        dsi.DCF68inhF1 = 2.035
        dsi.DCF68inhF5 = 2.368
        dsi.DCF68inhM1 = 11.47
        dsi.DCF68inhM5 = 7.77
        dsi.DCF68inhS1 = 31.45
        dsi.DCF68inhS5 = 25.16
        dsi.DCF68ing = 0.1813
        dsi.DCF72inhF1 = 2.072
        dsi.DCF72inhM1 = 12.95
        dsi.DCF72inhS1 = 34.78
        dsi.DCF72ing = 0.1813
        dsi.Daughter = "TH-230"
        dsi.Lambda = 0.0000000000000894684673141757
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TH-230"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 148
        dsi.DCF68inhM5 = 103.6
        dsi.DCF68inhS1 = 48.1
        dsi.DCF68inhS5 = 26.64
        dsi.DCF68ing = 0.777
        dsi.DCF72inhF1 = 370
        dsi.DCF72inhM1 = 159.1
        dsi.DCF72inhS1 = 51.8
        dsi.DCF72ing = 0.777
        dsi.Daughter = "RA-226"
        dsi.Lambda = 0.000000000000291306481772283
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RA-226"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 11.84
        dsi.DCF68inhM5 = 8.14
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 1.036
        dsi.DCF72inhF1 = 1.332
        dsi.DCF72inhM1 = 12.95
        dsi.DCF72inhS1 = 35.15
        dsi.DCF72ing = 1.036
        dsi.Daughter = "RN-222"
        dsi.Lambda = 0.0000000000137278179535188
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RN-222"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PO-218"
        dsi.Lambda = 0.00000209821807559472
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-218"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PB-214"
        dsi.Lambda = 0.00372900355369026
        dsi.BranchingRatio = 0.9998
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-218"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "AT-218"
        dsi.Lambda = 0.00372900355369026
        dsi.BranchingRatio = 0.0002
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PB-214"
        dsi.DCF68inhF1 = 0.01073
        dsi.DCF68inhF5 = 0.01776
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000518
        dsi.DCF72inhF1 = 0.01036
        dsi.DCF72inhM1 = 0.0518
        dsi.DCF72inhS1 = 0.0555
        dsi.DCF72ing = 0.000518
        dsi.Daughter = "BI-214"
        dsi.Lambda = 0.000431061679452702
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-214"
        dsi.DCF68inhF1 = 0.02664
        dsi.DCF68inhF5 = 0.0444
        dsi.DCF68inhM1 = 0.0518
        dsi.DCF68inhM5 = 0.0777
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000407
        dsi.DCF72inhF1 = 0.02627
        dsi.DCF72inhM1 = 0.0518
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.000407
        dsi.Daughter = "PO-214"
        dsi.Lambda = 0.000580525276850875
        dsi.BranchingRatio = 0.9979
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-214"
        dsi.DCF68inhF1 = 0.02664
        dsi.DCF68inhF5 = 0.0444
        dsi.DCF68inhM1 = 0.0518
        dsi.DCF68inhM5 = 0.0777
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000407
        dsi.DCF72inhF1 = 0.02627
        dsi.DCF72inhM1 = 0.0518
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.000407
        dsi.Daughter = "TL-210"
        dsi.Lambda = 0.000580525276850875
        dsi.BranchingRatio = 0.00209999999999999
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-214"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PB-210"
        dsi.Lambda = 4218.7898999388
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PB-210"
        dsi.DCF68inhF1 = 3.293
        dsi.DCF68inhF5 = 4.07
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 2.516
        dsi.DCF72inhF1 = 3.33
        dsi.DCF72inhM1 = 4.07
        dsi.DCF72inhS1 = 20.72
        dsi.DCF72ing = 2.553
        dsi.Daughter = "BI-210"
        dsi.Lambda = 0.000000000989392284938294
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-210"
        dsi.DCF68inhF1 = 0.00407
        dsi.DCF68inhF5 = 0.00518
        dsi.DCF68inhM1 = 0.3108
        dsi.DCF68inhM5 = 0.222
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00481
        dsi.DCF72inhF1 = 0.00407
        dsi.DCF72inhM1 = 0.3441
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00481
        dsi.Daughter = "PO-210"
        dsi.Lambda = 0.00000160066576457231
        dsi.BranchingRatio = 0.99999868
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-210"
        dsi.DCF68inhF1 = 0.00407
        dsi.DCF68inhF5 = 0.00518
        dsi.DCF68inhM1 = 0.3108
        dsi.DCF68inhM5 = 0.222
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00481
        dsi.DCF72inhF1 = 0.00407
        dsi.DCF72inhM1 = 0.3441
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00481
        dsi.Daughter = "TL-206"
        dsi.Lambda = 0.00000160066576457231
        dsi.BranchingRatio = 0.0000013
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-210"
        dsi.DCF68inhF1 = 2.22
        dsi.DCF68inhF5 = 2.627
        dsi.DCF68inhM1 = 11.1
        dsi.DCF68inhM5 = 8.14
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.888
        dsi.DCF72inhF1 = 2.257
        dsi.DCF72inhM1 = 12.21
        dsi.DCF72inhS1 = 15.91
        dsi.DCF72ing = 4.44
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000579763601494219
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PA-234"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.001406
        dsi.DCF68inhM5 = 0.002035
        dsi.DCF68inhS1 = 0.00148
        dsi.DCF68inhS5 = 0.002146
        dsi.DCF68ing = 0.001887
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.001406
        dsi.DCF72inhS1 = 0.00148
        dsi.DCF72ing = 0.001887
        dsi.Daughter = "U-234"
        dsi.Lambda = 0.0000287374452968468
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AT-218"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "BI-214"
        dsi.Lambda = 0.462098120373297
        dsi.BranchingRatio = 0.999
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AT-218"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "RN-218"
        dsi.Lambda = 0.462098120373297
        dsi.BranchingRatio = 0.001
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RN-218"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PO-214"
        dsi.Lambda = 19.8042051588556
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TL-210"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PB-210"
        dsi.Lambda = 0.00888650231487109
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TL-206"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 0.00274927487133089
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CM-246"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 148
        dsi.DCF68inhM5 = 99.9
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.777
        dsi.DCF72inhF1 = 362.6
        dsi.DCF72inhM1 = 155.4
        dsi.DCF72inhS1 = 59.2
        dsi.DCF72ing = 0.777
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000000466765778154845
        dsi.BranchingRatio = 0.0263
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CF-249"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 244.2
        dsi.DCF68inhM5 = 166.5
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 1.295
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 259
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 1.295
        dsi.Daughter = "CM-245"
        dsi.Lambda = 0.0000000000625769479362682
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CM-245"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 148
        dsi.DCF68inhM5 = 99.9
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.777
        dsi.DCF72inhF1 = 366.3
        dsi.DCF72inhM1 = 155.4
        dsi.DCF72inhS1 = 59.2
        dsi.DCF72ing = 0.777
        dsi.Daughter = "PU-241"
        dsi.Lambda = 0.00000000000260777720300958
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PU-241"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 3.145
        dsi.DCF68inhM5 = 2.146
        dsi.DCF68inhS1 = 0.592
        dsi.DCF68inhS5 = 0.3108
        dsi.DCF68ing = 0.01739
        dsi.DCF72inhF1 = 8.51
        dsi.DCF72inhM1 = 3.33
        dsi.DCF72inhS1 = 0.629
        dsi.DCF72ing = 0.01776
        dsi.Daughter = "AM-241"
        dsi.Lambda = 0.0000000015332990384384
        dsi.BranchingRatio = 0.999975
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PU-241"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 3.145
        dsi.DCF68inhM5 = 2.146
        dsi.DCF68inhS1 = 0.592
        dsi.DCF68inhS5 = 0.3108
        dsi.DCF68ing = 0.01739
        dsi.DCF72inhF1 = 8.51
        dsi.DCF72inhM1 = 3.33
        dsi.DCF72inhS1 = 0.629
        dsi.DCF72ing = 0.01776
        dsi.Daughter = "U-237"
        dsi.Lambda = 0.0000000015332990384384
        dsi.BranchingRatio = 0.0000245
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AM-241"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 144.3
        dsi.DCF68inhM5 = 99.9
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.74
        dsi.DCF72inhF1 = 355.2
        dsi.DCF72inhM1 = 155.4
        dsi.DCF72inhS1 = 59.2
        dsi.DCF72ing = 0.74
        dsi.Daughter = "NP-237"
        dsi.Lambda = 0.0000000000507732517929499
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NP-237"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 77.7
        dsi.DCF68inhM5 = 55.5
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.407
        dsi.DCF72inhF1 = 185
        dsi.DCF72inhM1 = 85.1
        dsi.DCF72inhS1 = 44.4
        dsi.DCF72ing = 0.407
        dsi.Daughter = "PA-233"
        dsi.Lambda = 0.00000000000001024464026382
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PA-233"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.01147
        dsi.DCF68inhM5 = 0.01036
        dsi.DCF68inhS1 = 0.01369
        dsi.DCF68inhS5 = 0.01184
        dsi.DCF68ing = 0.003219
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.01221
        dsi.DCF72inhS1 = 0.01443
        dsi.DCF72ing = 0.003219
        dsi.Daughter = "U-233"
        dsi.Lambda = 0.000000297406369306262
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "U-233"
        dsi.DCF68inhF1 = 2.109
        dsi.DCF68inhF5 = 2.442
        dsi.DCF68inhM1 = 11.84
        dsi.DCF68inhM5 = 8.14
        dsi.DCF68inhS1 = 32.19
        dsi.DCF68inhS5 = 25.53
        dsi.DCF68ing = 0.185
        dsi.DCF72inhF1 = 2.146
        dsi.DCF72inhM1 = 13.32
        dsi.DCF72inhS1 = 35.52
        dsi.DCF72ing = 0.1887
        dsi.Daughter = "TH-229"
        dsi.Lambda = 0.000000000000137968019633355
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TH-229"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 366.3
        dsi.DCF68inhM5 = 255.3
        dsi.DCF68inhS1 = 240.5
        dsi.DCF68inhS5 = 177.6
        dsi.DCF68ing = 1.776
        dsi.DCF72inhF1 = 888
        dsi.DCF72inhM1 = 407
        dsi.DCF72inhS1 = 262.7
        dsi.DCF72ing = 1.813
        dsi.Daughter = "RA-225"
        dsi.Lambda = 0.0000000000027691009487683
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RA-225"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 21.46
        dsi.DCF68inhM5 = 17.76
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.3515
        dsi.DCF72inhF1 = 0.481
        dsi.DCF72inhM1 = 23.31
        dsi.DCF72inhS1 = 28.49
        dsi.DCF72ing = 0.3663
        dsi.Daughter = "AC-225"
        dsi.Lambda = 0.000000538425289398416
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AC-225"
        dsi.DCF68inhF1 = 3.219
        dsi.DCF68inhF5 = 3.7
        dsi.DCF68inhM1 = 25.53
        dsi.DCF68inhM5 = 21.09
        dsi.DCF68inhS1 = 29.23
        dsi.DCF68inhS5 = 24.05
        dsi.DCF68ing = 0.0888
        dsi.DCF72inhF1 = 3.256
        dsi.DCF72inhM1 = 27.38
        dsi.DCF72inhS1 = 31.45
        dsi.DCF72ing = 0.0888
        dsi.Daughter = "FR-221"
        dsi.Lambda = 0.00000080225368120364
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "FR-221"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "AT-217"
        dsi.Lambda = 0.00242274442698338
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AT-217"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "BI-213"
        dsi.Lambda = 21.4596650328156
        dsi.BranchingRatio = 0.99993
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AT-217"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "RN-217"
        dsi.Lambda = 21.4596650328156
        dsi.BranchingRatio = 0.00007
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-213"
        dsi.DCF68inhF1 = 0.0407
        dsi.DCF68inhF5 = 0.0666
        dsi.DCF68inhM1 = 0.1073
        dsi.DCF68inhM5 = 0.1517
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00074
        dsi.DCF72inhF1 = 0.037
        dsi.DCF72inhM1 = 0.111
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00074
        dsi.Daughter = "PO-213"
        dsi.Lambda = 0.000253398837669059
        dsi.BranchingRatio = 0.978
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TL-209"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PB-209"
        dsi.Lambda = 0.00534588292889052
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PB-209"
        dsi.DCF68inhF1 = 0.0000666
        dsi.DCF68inhF5 = 0.0001184
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0002109
        dsi.DCF72inhF1 = 0.0000629
        dsi.DCF72inhM1 = 0.0002072
        dsi.DCF72inhS1 = 0.0002257
        dsi.DCF72ing = 0.0002109
        dsi.Daughter = "END"
        dsi.Lambda = 0.000059188713030702
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "U-237"
        dsi.DCF68inhF1 = 0.000703
        dsi.DCF68inhF5 = 0.001221
        dsi.DCF68inhM1 = 0.00592
        dsi.DCF68inhM5 = 0.00555
        dsi.DCF68inhS1 = 0.00666
        dsi.DCF68inhS5 = 0.00629
        dsi.DCF68ing = 0.002849
        dsi.DCF72inhF1 = 0.000666
        dsi.DCF72inhM1 = 0.00629
        dsi.DCF72inhS1 = 0.00703
        dsi.DCF72ing = 0.002812
        dsi.Daughter = "NP-237"
        dsi.Lambda = 0.00000118852397215354
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RN-217"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PO-213"
        dsi.Lambda = 1283.60588992582
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-213"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PB-209"
        dsi.Lambda = 186329.887247297
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CM-244"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 92.5
        dsi.DCF68inhM5 = 62.9
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.444
        dsi.DCF72inhF1 = 210.9
        dsi.DCF72inhM1 = 99.9
        dsi.DCF72inhS1 = 48.1
        dsi.DCF72ing = 0.444
        dsi.Daughter = "PU-240"
        dsi.Lambda = 0.00000000121350876937183
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PU-240"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 173.9
        dsi.DCF68inhM5 = 118.4
        dsi.DCF68inhS1 = 55.5
        dsi.DCF68inhS5 = 30.71
        dsi.DCF68ing = 0.925
        dsi.DCF72inhF1 = 444
        dsi.DCF72inhM1 = 185
        dsi.DCF72inhS1 = 59.2
        dsi.DCF72ing = 0.925
        dsi.Daughter = "U-236"
        dsi.Lambda = 0.00000000000334773795543821
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "U-236"
        dsi.DCF68inhF1 = 1.924
        dsi.DCF68inhF5 = 2.257
        dsi.DCF68inhM1 = 10.73
        dsi.DCF68inhM5 = 7.03
        dsi.DCF68inhS1 = 29.23
        dsi.DCF68inhS5 = 23.31
        dsi.DCF68ing = 0.1702
        dsi.DCF72inhF1 = 1.961
        dsi.DCF72inhM1 = 11.84
        dsi.DCF72inhS1 = 32.19
        dsi.DCF72ing = 0.1739
        dsi.Daughter = "TH-232"
        dsi.Lambda = 0.000000000000000937852635594796
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TH-232"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 155.4
        dsi.DCF68inhM5 = 107.3
        dsi.DCF68inhS1 = 85.1
        dsi.DCF68inhS5 = 44.4
        dsi.DCF68ing = 0.814
        dsi.DCF72inhF1 = 407
        dsi.DCF72inhM1 = 166.5
        dsi.DCF72inhS1 = 92.5
        dsi.DCF72ing = 0.851
        dsi.Daughter = "RA-228"
        dsi.Lambda = 1.56889348040215E-18
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RA-228"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 9.62
        dsi.DCF68inhM5 = 6.29
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 2.479
        dsi.DCF72inhF1 = 3.33
        dsi.DCF72inhM1 = 9.62
        dsi.DCF72inhS1 = 59.2
        dsi.DCF72ing = 2.553
        dsi.Daughter = "AC-228"
        dsi.Lambda = 0.00000000381991456097915
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AC-228"
        dsi.DCF68inhF1 = 0.0925
        dsi.DCF68inhF5 = 0.1073
        dsi.DCF68inhM1 = 0.0592
        dsi.DCF68inhM5 = 0.0444
        dsi.DCF68inhS1 = 0.0518
        dsi.DCF68inhS5 = 0.0444
        dsi.DCF68ing = 0.001591
        dsi.DCF72inhF1 = 0.0925
        dsi.DCF72inhM1 = 0.0629
        dsi.DCF72inhS1 = 0.0592
        dsi.DCF72ing = 0.001591
        dsi.Daughter = "TH-228"
        dsi.Lambda = 0.0000313074607298982
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TH-228"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 111
        dsi.DCF68inhM5 = 81.4
        dsi.DCF68inhS1 = 136.9
        dsi.DCF68inhS5 = 92.5
        dsi.DCF68ing = 0.2664
        dsi.DCF72inhF1 = 111
        dsi.DCF72inhM1 = 118.4
        dsi.DCF72inhS1 = 148
        dsi.DCF72ing = 0.2664
        dsi.Daughter = "RA-224"
        dsi.Lambda = 0.0000000114901175589193
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RA-224"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 10.73
        dsi.DCF68inhM5 = 8.88
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.2405
        dsi.DCF72inhF1 = 0.2775
        dsi.DCF72inhM1 = 11.1
        dsi.DCF72inhS1 = 12.58
        dsi.DCF72ing = 0.2405
        dsi.Daughter = "RN-220"
        dsi.Lambda = 0.00000220890905918015
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RN-220"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PO-216"
        dsi.Lambda = 0.0124666759093515
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-216"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PB-212"
        dsi.Lambda = 4.78032538317204
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PB-212"
        dsi.DCF68inhF1 = 0.0703
        dsi.DCF68inhF5 = 0.1221
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.02183
        dsi.DCF72inhF1 = 0.0666
        dsi.DCF72inhM1 = 0.629
        dsi.DCF72inhS1 = 0.703
        dsi.DCF72ing = 0.0222
        dsi.Daughter = "BI-212"
        dsi.Lambda = 0.0000180959476963227
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-212"
        dsi.DCF68inhF1 = 0.03441
        dsi.DCF68inhF5 = 0.0555
        dsi.DCF68inhM1 = 0.111
        dsi.DCF68inhM5 = 0.1443
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000962
        dsi.DCF72inhF1 = 0.03367
        dsi.DCF72inhM1 = 0.1147
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.000962
        dsi.Daughter = "PO-212"
        dsi.Lambda = 0.000190791957214408
        dsi.BranchingRatio = 0.6406
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-212"
        dsi.DCF68inhF1 = 0.03441
        dsi.DCF68inhF5 = 0.0555
        dsi.DCF68inhM1 = 0.111
        dsi.DCF68inhM5 = 0.1443
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000962
        dsi.DCF72inhF1 = 0.03367
        dsi.DCF72inhM1 = 0.1147
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.000962
        dsi.Daughter = "TL-208"
        dsi.Lambda = 0.000190791957214408
        dsi.BranchingRatio = 0.3594
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-212"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 2318217.99518376
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TL-208"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 0.00378396757593594
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CF-251"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 247.9
        dsi.DCF68inhM5 = 170.2
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 1.332
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 262.7
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 1.332
        dsi.Daughter = "CM-247"
        dsi.Lambda = 0.0000000000244593638370046
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CM-247"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 133.2
        dsi.DCF68inhM5 = 92.5
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.703
        dsi.DCF72inhF1 = 333
        dsi.DCF72inhM1 = 144.3
        dsi.DCF72inhS1 = 51.8
        dsi.DCF72ing = 0.703
        dsi.Daughter = "PU-243"
        dsi.Lambda = 0.00000000000000140798132856603
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PU-243"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.0003034
        dsi.DCF68inhM5 = 0.000407
        dsi.DCF68inhS1 = 0.0003145
        dsi.DCF68inhS5 = 0.000407
        dsi.DCF68ing = 0.0003145
        dsi.DCF72inhF1 = 0.0001184
        dsi.DCF72inhM1 = 0.0003071
        dsi.DCF72inhS1 = 0.0003182
        dsi.DCF72ing = 0.0003145
        dsi.Daughter = "AM-243"
        dsi.Lambda = 0.0000388500572011448
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AM-243"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 144.3
        dsi.DCF68inhM5 = 99.9
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.74
        dsi.DCF72inhF1 = 355.2
        dsi.DCF72inhM1 = 151.7
        dsi.DCF72inhS1 = 55.5
        dsi.DCF72ing = 0.74
        dsi.Daughter = "NP-239"
        dsi.Lambda = 0.00000000000298025898583855
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NP-239"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.00333
        dsi.DCF68inhM5 = 0.00407
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00296
        dsi.DCF72inhF1 = 0.000629
        dsi.DCF72inhM1 = 0.003441
        dsi.DCF72inhS1 = 0.0037
        dsi.DCF72ing = 0.00296
        dsi.Daughter = "PU-239"
        dsi.Lambda = 0.00000340515144823277
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PU-239"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 173.9
        dsi.DCF68inhM5 = 118.4
        dsi.DCF68inhS1 = 55.5
        dsi.DCF68inhS5 = 30.71
        dsi.DCF68ing = 0.925
        dsi.DCF72inhF1 = 444
        dsi.DCF72inhM1 = 185
        dsi.DCF72inhS1 = 59.2
        dsi.DCF72ing = 0.925
        dsi.Daughter = "U-235"
        dsi.Lambda = 0.000000000000911012390113236
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "U-235"
        dsi.DCF68inhF1 = 1.887
        dsi.DCF68inhF5 = 2.22
        dsi.DCF68inhM1 = 10.36
        dsi.DCF68inhM5 = 6.66
        dsi.DCF68inhS1 = 28.49
        dsi.DCF68inhS5 = 22.57
        dsi.DCF68ing = 0.1702
        dsi.DCF72inhF1 = 1.924
        dsi.DCF72inhM1 = 11.47
        dsi.DCF72inhS1 = 31.45
        dsi.DCF72ing = 0.1739
        dsi.Daughter = "TH-231"
        dsi.Lambda = 3.11995862579973E-17
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TH-231"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.001073
        dsi.DCF68inhM5 = 0.001369
        dsi.DCF68inhS1 = 0.001184
        dsi.DCF68inhS5 = 0.00148
        dsi.DCF68ing = 0.001258
        dsi.DCF72inhF1 = 0.0002886
        dsi.DCF72inhM1 = 0.001147
        dsi.DCF72inhS1 = 0.001221
        dsi.DCF72ing = 0.001258
        dsi.Daughter = "PA-231"
        dsi.Lambda = 0.00000754470546586496
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PA-231"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 481
        dsi.DCF68inhM5 = 329.3
        dsi.DCF68inhS1 = 118.4
        dsi.DCF68inhS5 = 62.9
        dsi.DCF68ing = 2.627
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 518
        dsi.DCF72inhS1 = 125.8
        dsi.DCF72ing = 2.627
        dsi.Daughter = "AC-227"
        dsi.Lambda = 0.000000000000670467299317159
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AC-227"
        dsi.DCF68inhF1 = 1998
        dsi.DCF68inhF5 = 2331
        dsi.DCF68inhM1 = 777
        dsi.DCF68inhM5 = 555
        dsi.DCF68inhS1 = 244.2
        dsi.DCF68inhS5 = 173.9
        dsi.DCF68ing = 4.07
        dsi.DCF72inhF1 = 2035
        dsi.DCF72inhM1 = 814
        dsi.DCF72inhS1 = 266.4
        dsi.DCF72ing = 4.07
        dsi.Daughter = "TH-227"
        dsi.Lambda = 0.00000000100884203222626
        dsi.BranchingRatio = 0.9862
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AC-227"
        dsi.DCF68inhF1 = 1998
        dsi.DCF68inhF5 = 2331
        dsi.DCF68inhM1 = 777
        dsi.DCF68inhM5 = 555
        dsi.DCF68inhS1 = 244.2
        dsi.DCF68inhS5 = 173.9
        dsi.DCF68ing = 4.07
        dsi.DCF72inhF1 = 2035
        dsi.DCF72inhM1 = 814
        dsi.DCF72inhS1 = 266.4
        dsi.DCF72ing = 4.07
        dsi.Daughter = "FR-223"
        dsi.Lambda = 0.00000000100884203222626
        dsi.BranchingRatio = 0.0138
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TH-227"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 28.86
        dsi.DCF68inhM5 = 22.94
        dsi.DCF68inhS1 = 35.52
        dsi.DCF68inhS5 = 28.12
        dsi.DCF68ing = 0.03293
        dsi.DCF72inhF1 = 2.479
        dsi.DCF72inhM1 = 31.45
        dsi.DCF72inhS1 = 37
        dsi.DCF72ing = 0.03256
        dsi.Daughter = "RA-223"
        dsi.Lambda = 0.000000429471992079037
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RA-223"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 25.53
        dsi.DCF68inhM5 = 21.09
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.37
        dsi.DCF72inhF1 = 0.444
        dsi.DCF72inhM1 = 27.38
        dsi.DCF72inhS1 = 32.19
        dsi.DCF72ing = 0.37
        dsi.Daughter = "RN-219"
        dsi.Lambda = 0.000000701884235523745
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RN-219"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PO-215"
        dsi.Lambda = 0.175037166808067
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-215"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PB-211"
        dsi.Lambda = 389.189882403114
        dsi.BranchingRatio = 0.9999977
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-215"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "AT-215"
        dsi.Lambda = 389.189882403114
        dsi.BranchingRatio = 0.00000230000000001063
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PB-211"
        dsi.DCF68inhF1 = 0.01443
        dsi.DCF68inhF5 = 0.02072
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000666
        dsi.DCF72inhF1 = 0.01443
        dsi.DCF72inhM1 = 0.0407
        dsi.DCF72inhS1 = 0.0444
        dsi.DCF72ing = 0.000666
        dsi.Daughter = "BI-211"
        dsi.Lambda = 0.00032001254873497
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-211"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "TL-207"
        dsi.Lambda = 0.00539834252772543
        dsi.BranchingRatio = 0.99724
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-211"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PO-211"
        dsi.Lambda = 0.00539834252772543
        dsi.BranchingRatio = 0.00275999999999998
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TL-207"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 0.00242189790552042
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "FR-223"
        dsi.DCF68inhF1 = 0.003367
        dsi.DCF68inhF5 = 0.00481
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00851
        dsi.DCF72inhF1 = 0.003293
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00888
        dsi.Daughter = "AT-219"
        dsi.Lambda = 0.000525111500424201
        dsi.BranchingRatio = 0.000059999999999949
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AT-215"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "BI-211"
        dsi.Lambda = 6931.47180559945
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PO-211"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 1.34330848945726
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CM-243"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 107.3
        dsi.DCF68inhM5 = 74
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.555
        dsi.DCF72inhF1 = 255.3
        dsi.DCF72inhM1 = 114.7
        dsi.DCF72inhS1 = 55.5
        dsi.DCF72ing = 0.555
        dsi.Daughter = "PU-239"
        dsi.Lambda = 0.000000000754794114282822
        dsi.BranchingRatio = 0.9971
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CM-243"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 107.3
        dsi.DCF68inhM5 = 74
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.555
        dsi.DCF72inhF1 = 255.3
        dsi.DCF72inhM1 = 114.7
        dsi.DCF72inhS1 = 55.5
        dsi.DCF72ing = 0.555
        dsi.Daughter = "AM-243"
        dsi.Lambda = 0.000000000754794114282822
        dsi.BranchingRatio = 0.00290000000000001
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AM-242"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.0592
        dsi.DCF68inhM5 = 0.0444
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00111
        dsi.DCF72inhF1 = 0.0407
        dsi.DCF72inhM1 = 0.0629
        dsi.DCF72inhS1 = 0.074
        dsi.DCF72ing = 0.00111
        dsi.Daughter = "CM-242"
        dsi.Lambda = 0.0000120187817408785
        dsi.BranchingRatio = 0.827
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AM-242"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.0592
        dsi.DCF68inhM5 = 0.0444
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00111
        dsi.DCF72inhF1 = 0.0407
        dsi.DCF72inhM1 = 0.0629
        dsi.DCF72inhS1 = 0.074
        dsi.DCF72ing = 0.00111
        dsi.Daughter = "PU-242"
        dsi.Lambda = 0.0000120187817408785
        dsi.BranchingRatio = 0.173
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CM-242"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 17.76
        dsi.DCF68inhM5 = 13.69
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0444
        dsi.DCF72inhF1 = 12.21
        dsi.DCF72inhM1 = 19.24
        dsi.DCF72inhS1 = 21.83
        dsi.DCF72ing = 0.0444
        dsi.Daughter = "PU-238"
        dsi.Lambda = 0.0000000492641919374517
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PU-238"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 159.1
        dsi.DCF68inhM5 = 111
        dsi.DCF68inhS1 = 55.5
        dsi.DCF68inhS5 = 40.7
        dsi.DCF68ing = 0.851
        dsi.DCF72inhF1 = 407
        dsi.DCF72inhM1 = 170.2
        dsi.DCF72inhS1 = 59.2
        dsi.DCF72ing = 0.851
        dsi.Daughter = "U-234"
        dsi.Lambda = 0.000000000250450498581871
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AM-242M"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 129.5
        dsi.DCF68inhM5 = 88.8
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.703
        dsi.DCF72inhF1 = 340.4
        dsi.DCF72inhM1 = 136.9
        dsi.DCF72inhS1 = 40.7
        dsi.DCF72ing = 0.703
        dsi.Daughter = "AM-242"
        dsi.Lambda = 0.000000000155763411361785
        dsi.BranchingRatio = 0.99541
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AM-242M"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 129.5
        dsi.DCF68inhM5 = 88.8
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.703
        dsi.DCF72inhF1 = 340.4
        dsi.DCF72inhM1 = 136.9
        dsi.DCF72inhS1 = 40.7
        dsi.DCF72ing = 0.703
        dsi.Daughter = "NP-238"
        dsi.Lambda = 0.000000000155763411361785
        dsi.BranchingRatio = 0.459
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NP-238"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.0074
        dsi.DCF68inhM5 = 0.00629
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.003367
        dsi.DCF72inhF1 = 0.01295
        dsi.DCF72inhM1 = 0.00777
        dsi.DCF72inhS1 = 0.00555
        dsi.DCF72ing = 0.003367
        dsi.Daughter = "PU-238"
        dsi.Lambda = 0.00000378957808787738
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NP-236"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 11.1
        dsi.DCF68inhM5 = 7.4
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0629
        dsi.DCF72inhF1 = 29.6
        dsi.DCF72inhM1 = 11.84
        dsi.DCF72inhS1 = 3.7
        dsi.DCF72ing = 0.0629
        dsi.Daughter = "U-236"
        dsi.Lambda = 0.000000000000143568181557569
        dsi.BranchingRatio = 0.873
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NP-236"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 11.1
        dsi.DCF68inhM5 = 7.4
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0629
        dsi.DCF72inhF1 = 29.6
        dsi.DCF72inhM1 = 11.84
        dsi.DCF72inhS1 = 3.7
        dsi.DCF72ing = 0.0629
        dsi.Daughter = "PU-236"
        dsi.Lambda = 0.000000000000143568181557569
        dsi.BranchingRatio = 0.125
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PU-236"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 66.6
        dsi.DCF68inhM5 = 48.1
        dsi.DCF68inhS1 = 35.52
        dsi.DCF68inhS5 = 27.38
        dsi.DCF68ing = 0.3182
        dsi.DCF72inhF1 = 148
        dsi.DCF72inhM1 = 74
        dsi.DCF72inhS1 = 37
        dsi.DCF72ing = 0.3219
        dsi.Daughter = "U-232"
        dsi.Lambda = 0.00000000768524016054578
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "U-232"
        dsi.DCF68inhF1 = 14.8
        dsi.DCF68inhF5 = 17.39
        dsi.DCF68inhM1 = 26.64
        dsi.DCF68inhM5 = 17.76
        dsi.DCF68inhS1 = 129.5
        dsi.DCF68inhS5 = 96.2
        dsi.DCF68ing = 0.1369
        dsi.DCF72inhF1 = 14.8
        dsi.DCF72inhM1 = 28.86
        dsi.DCF72inhS1 = 136.9
        dsi.DCF72ing = 1.221
        dsi.Daughter = "TH-228"
        dsi.Lambda = 0.000000000318834949659588
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "FR-223"
        dsi.DCF68inhF1 = 0.003367
        dsi.DCF68inhF5 = 0.00481
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00851
        dsi.DCF72inhF1 = 0.003293
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00888
        dsi.Daughter = "RA-223"
        dsi.Lambda = 0.000525111500424201
        dsi.BranchingRatio = 0.99994
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AT-219"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "BI-215"
        dsi.Lambda = 0.0123776282242847
        dsi.BranchingRatio = 0.97
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "AT-219"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "RN-219"
        dsi.Lambda = 0.0123776282242847
        dsi.BranchingRatio = 0.03
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-215"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "PO-215"
        dsi.Lambda = 0.00152005960649111
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BI-213"
        dsi.DCF68inhF1 = 0.0407
        dsi.DCF68inhF5 = 0.0666
        dsi.DCF68inhM1 = 0.1073
        dsi.DCF68inhM5 = 0.1517
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00074
        dsi.DCF72inhF1 = 0.037
        dsi.DCF72inhM1 = 0.111
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00074
        dsi.Daughter = "TL-209"
        dsi.Lambda = 0.000253398837669059
        dsi.BranchingRatio = 0.022
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "EU-155"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.02405
        dsi.DCF68inhM5 = 0.01739
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.001184
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.02553
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.001184
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000462098120373297
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "EU-154"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.185
        dsi.DCF68inhM5 = 0.1295
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0074
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.1961
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.0074
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000255396897774482
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "EU-152"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.1443
        dsi.DCF68inhM5 = 0.0999
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00518
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.1554
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00518
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000162363311100164
        dsi.BranchingRatio = 0.7208
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "EU-152"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.1443
        dsi.DCF68inhM5 = 0.0999
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00518
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.1554
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00518
        dsi.Daughter = "GD-152"
        dsi.Lambda = 0.00000000162363311100164
        dsi.BranchingRatio = 0.2792
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "GD-152"
        dsi.DCF68inhF1 = 70.3
        dsi.DCF68inhF5 = 81.4
        dsi.DCF68inhM1 = 27.38
        dsi.DCF68inhM5 = 18.5
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.1517
        dsi.DCF72inhF1 = 70.3
        dsi.DCF72inhM1 = 29.6
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.1517
        dsi.Daughter = "SM-148"
        dsi.Lambda = 2.03375080792872E-22
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SM-148"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "ND-144"
        dsi.Lambda = 3.1377869608043E-24
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "ND-144"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 9.59148852647604E-24
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SM-151"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.01369
        dsi.DCF68inhM5 = 0.00962
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0003626
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.0148
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.0003626
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000000244065908647868
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PM-147"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.01739
        dsi.DCF68inhM5 = 0.01295
        dsi.DCF68inhS1 = 0.01702
        dsi.DCF68inhS5 = 0.01184
        dsi.DCF68ing = 0.000962
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.0185
        dsi.DCF72inhS1 = 0.01813
        dsi.DCF72ing = 0.000962
        dsi.Daughter = "SM-147"
        dsi.Lambda = 0.00000000837235391424019
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SM-147"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 32.93
        dsi.DCF68inhM5 = 22.57
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.1813
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 35.52
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.1813
        dsi.Daughter = "END"
        dsi.Lambda = 2.07218888059774E-19
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PM-146"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.0703
        dsi.DCF68inhM5 = 0.0481
        dsi.DCF68inhS1 = 0.0592
        dsi.DCF68inhS5 = 0.0333
        dsi.DCF68ing = 0.00333
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.0777
        dsi.DCF72inhS1 = 0.0629
        dsi.DCF72ing = 0.00333
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000397219014647533
        dsi.BranchingRatio = 0.66
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PM-146"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.0703
        dsi.DCF68inhM5 = 0.0481
        dsi.DCF68inhS1 = 0.0592
        dsi.DCF68inhS5 = 0.0333
        dsi.DCF68ing = 0.00333
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.0777
        dsi.DCF72inhS1 = 0.0629
        dsi.DCF72ing = 0.00333
        dsi.Daughter = "SM-146"
        dsi.Lambda = 0.00000000397219014647533
        dsi.BranchingRatio = 0.34
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SM-146"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 36.63
        dsi.DCF68inhM5 = 24.79
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.1998
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 40.7
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.1998
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000000000000213276055556906
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CE-144"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.1258
        dsi.DCF68inhM5 = 0.0851
        dsi.DCF68inhS1 = 0.1813
        dsi.DCF68inhS5 = 0.1073
        dsi.DCF68ing = 0.01924
        dsi.DCF72inhF1 = 0.148
        dsi.DCF72inhM1 = 0.1332
        dsi.DCF72inhS1 = 0.1961
        dsi.DCF72ing = 0.01924
        dsi.Daughter = "PR-144"
        dsi.Lambda = 0.0000000281538253679913
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PR-144"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.0000666
        dsi.DCF68inhM5 = 0.0001073
        dsi.DCF68inhS1 = 0.0000703
        dsi.DCF68inhS5 = 0.000111
        dsi.DCF68ing = 0.000185
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.0000666
        dsi.DCF72inhS1 = 0.0000666
        dsi.DCF72ing = 0.000185
        dsi.Daughter = "ND-144"
        dsi.Lambda = 0.000668544734336367
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CS-137"
        dsi.DCF68inhF1 = 0.01776
        dsi.DCF68inhF5 = 0.02479
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0481
        dsi.DCF72inhF1 = 0.01702
        dsi.DCF72inhM1 = 0.03589
        dsi.DCF72inhS1 = 0.1443
        dsi.DCF72ing = 0.0481
        dsi.Daughter = "BA-137M"
        dsi.Lambda = 0.00000000073021928011085
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "BA-137M"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 0.00452682327951897
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CS-135"
        dsi.DCF68inhF1 = 0.002627
        dsi.DCF68inhF5 = 0.003663
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0074
        dsi.DCF72inhF1 = 0.002553
        dsi.DCF72inhM1 = 0.01147
        dsi.DCF72inhS1 = 0.03182
        dsi.DCF72ing = 0.0074
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000000000095501127109389
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CS-134"
        dsi.DCF68inhF1 = 0.02516
        dsi.DCF68inhF5 = 0.03552
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0703
        dsi.DCF72inhF1 = 0.02442
        dsi.DCF72inhM1 = 0.03367
        dsi.DCF72inhS1 = 0.074
        dsi.DCF72ing = 0.0703
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000106359855847774
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "I-131"
        dsi.DCF68inhF1 = 0.02812
        dsi.DCF68inhF5 = 0.0407
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0814
        dsi.DCF72inhF1 = 0.02738
        dsi.DCF72inhM1 = 0.00888
        dsi.DCF72inhS1 = 0.00592
        dsi.DCF72ing = 0.0814
        dsi.Daughter = "XE-131M"
        dsi.Lambda = 0.000000999668146841998
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "XE-131M"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000067757912263821
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "I-129"
        dsi.DCF68inhF1 = 0.1369
        dsi.DCF68inhF5 = 0.1887
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.407
        dsi.DCF72inhF1 = 0.1332
        dsi.DCF72inhM1 = 0.0555
        dsi.DCF72inhS1 = 0.03626
        dsi.DCF72ing = 0.407
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000000000139901329462612
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SB-126"
        dsi.DCF68inhF1 = 0.00407
        dsi.DCF68inhF5 = 0.00629
        dsi.DCF68inhM1 = 0.00999
        dsi.DCF68inhM5 = 0.01184
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00888
        dsi.DCF72inhF1 = 0.0037
        dsi.DCF72inhM1 = 0.01036
        dsi.DCF72inhS1 = 0.01184
        dsi.DCF72ing = 0.00888
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000649598122432098
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SB-126M"
        dsi.DCF68inhF1 = 0.0000481
        dsi.DCF68inhF5 = 0.0000851
        dsi.DCF68inhM1 = 0.000074
        dsi.DCF68inhM5 = 0.0001221
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0001332
        dsi.DCF72inhF1 = 0.0000444
        dsi.DCF72inhM1 = 0.0000703
        dsi.DCF72inhS1 = 0.000074
        dsi.DCF72ing = 0.0001332
        dsi.Daughter = "END"
        dsi.Lambda = 0.00060326125375104
        dsi.BranchingRatio = 0.86
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SB-126M"
        dsi.DCF68inhF1 = 0.0000481
        dsi.DCF68inhF5 = 0.0000851
        dsi.DCF68inhM1 = 0.000074
        dsi.DCF68inhM5 = 0.0001221
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0001332
        dsi.DCF72inhF1 = 0.0000444
        dsi.DCF72inhM1 = 0.0000703
        dsi.DCF72inhS1 = 0.000074
        dsi.DCF72ing = 0.0001332
        dsi.Daughter = "SB-126"
        dsi.Lambda = 0.00060326125375104
        dsi.BranchingRatio = 0.14
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SN-126"
        dsi.DCF68inhF1 = 0.0407
        dsi.DCF68inhF5 = 0.0518
        dsi.DCF68inhM1 = 0.0999
        dsi.DCF68inhM5 = 0.0666
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.01739
        dsi.DCF72inhF1 = 0.0407
        dsi.DCF72inhM1 = 0.1036
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.01739
        dsi.Daughter = "SB-126M"
        dsi.Lambda = 0.000000000000095501127109389
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "I-125"
        dsi.DCF68inhF1 = 0.01961
        dsi.DCF68inhF5 = 0.02701
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0555
        dsi.DCF72inhF1 = 0.01887
        dsi.DCF72inhM1 = 0.00518
        dsi.DCF72inhS1 = 0.001406
        dsi.DCF72ing = 0.0555
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000135043628057912
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SB-125"
        dsi.DCF68inhF1 = 0.00518
        dsi.DCF68inhF5 = 0.00629
        dsi.DCF68inhM1 = 0.01665
        dsi.DCF68inhM5 = 0.01221
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00407
        dsi.DCF72inhF1 = 0.00518
        dsi.DCF72inhM1 = 0.01776
        dsi.DCF72inhS1 = 0.0444
        dsi.DCF72ing = 0.00407
        dsi.Daughter = "TE-125M"
        dsi.Lambda = 0.00000000796263274623717
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TE-125M"
        dsi.DCF68inhF1 = 0.001887
        dsi.DCF68inhF5 = 0.002479
        dsi.DCF68inhM1 = 0.01221
        dsi.DCF68inhM5 = 0.01073
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.003219
        dsi.DCF72inhF1 = 0.001887
        dsi.DCF72inhM1 = 0.01258
        dsi.DCF72inhS1 = 0.01554
        dsi.DCF72ing = 0.003219
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000139765449687045
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "I-124"
        dsi.DCF68inhF1 = 0.01665
        dsi.DCF68inhF5 = 0.02331
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0481
        dsi.DCF72inhF1 = 0.01628
        dsi.DCF72inhM1 = 0.00444
        dsi.DCF72inhS1 = 0.002849
        dsi.DCF72ing = 0.0481
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000192110555843784
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "I-123"
        dsi.DCF68inhF1 = 0.0002812
        dsi.DCF68inhF5 = 0.000407
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000777
        dsi.DCF72inhF1 = 0.0002738
        dsi.DCF72inhM1 = 0.0002368
        dsi.DCF72inhS1 = 0.000222
        dsi.DCF72ing = 0.000777
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000145605084500226
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SN-121"
        dsi.DCF68inhF1 = 0.0002368
        dsi.DCF68inhF5 = 0.00037
        dsi.DCF68inhM1 = 0.000814
        dsi.DCF68inhM5 = 0.001036
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000851
        dsi.DCF72inhF1 = 0.000222
        dsi.DCF72inhM1 = 0.000851
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.000851
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000071232291338836
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SN-121M"
        dsi.DCF68inhF1 = 0.00296
        dsi.DCF68inhF5 = 0.003589
        dsi.DCF68inhM1 = 0.01554
        dsi.DCF68inhM5 = 0.01221
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.001406
        dsi.DCF72inhF1 = 0.00296
        dsi.DCF72inhM1 = 0.01665
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.001406
        dsi.Daughter = "SN-121"
        dsi.Lambda = 0.000000000500467278382632
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SN-121M"
        dsi.DCF68inhF1 = 0.00296
        dsi.DCF68inhF5 = 0.003589
        dsi.DCF68inhM1 = 0.01554
        dsi.DCF68inhM5 = 0.01221
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.001406
        dsi.DCF72inhF1 = 0.00296
        dsi.DCF72inhM1 = 0.01665
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.001406
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000000500467278382632
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CD-113"
        dsi.DCF68inhF1 = 0.444
        dsi.DCF68inhF5 = 0.518
        dsi.DCF68inhM1 = 0.1961
        dsi.DCF68inhM5 = 0.1591
        dsi.DCF68inhS1 = 0.0925
        dsi.DCF68inhS5 = 0.0777
        dsi.DCF68ing = 0.0925
        dsi.DCF72inhF1 = 0.444
        dsi.DCF72inhM1 = 0.2035
        dsi.DCF72inhS1 = 0.0962
        dsi.DCF72ing = 0.0925
        dsi.Daughter = "END"
        dsi.Lambda = 2.74513734875226E-24
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CD-113M"
        dsi.DCF68inhF1 = 0.407
        dsi.DCF68inhF5 = 0.481
        dsi.DCF68inhM1 = 0.185
        dsi.DCF68inhM5 = 0.148
        dsi.DCF68inhS1 = 0.111
        dsi.DCF68inhS5 = 0.0888
        dsi.DCF68ing = 0.0851
        dsi.DCF72inhF1 = 0.407
        dsi.DCF72inhM1 = 0.1924
        dsi.DCF72inhS1 = 0.1147
        dsi.DCF72ing = 0.0851
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000155763411361785
        dsi.BranchingRatio = 0.9986
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CD-113M"
        dsi.DCF68inhF1 = 0.407
        dsi.DCF68inhF5 = 0.481
        dsi.DCF68inhM1 = 0.185
        dsi.DCF68inhM5 = 0.148
        dsi.DCF68inhS1 = 0.111
        dsi.DCF68inhS5 = 0.0888
        dsi.DCF68ing = 0.0851
        dsi.DCF72inhF1 = 0.407
        dsi.DCF72inhM1 = 0.1924
        dsi.DCF72inhS1 = 0.1147
        dsi.DCF72ing = 0.0851
        dsi.Daughter = "CD-113"
        dsi.Lambda = 0.00000000155763411361785
        dsi.BranchingRatio = 0.00139999999999996
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "PD-107"
        dsi.DCF68inhF1 = 0.0000962
        dsi.DCF68inhF5 = 0.0001221
        dsi.DCF68inhM1 = 0.000296
        dsi.DCF68inhM5 = 0.0001924
        dsi.DCF68inhS1 = 0.002035
        dsi.DCF68inhS5 = 0.001073
        dsi.DCF68ing = 0.0001369
        dsi.DCF72inhF1 = 0.0000925
        dsi.DCF72inhM1 = 0.0003145
        dsi.DCF72inhS1 = 0.002183
        dsi.DCF72ing = 0.0001369
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000000000337922767433671
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RU-106"
        dsi.DCF68inhF1 = 0.0296
        dsi.DCF68inhF5 = 0.03626
        dsi.DCF68inhM1 = 0.0962
        dsi.DCF68inhM5 = 0.0629
        dsi.DCF68inhS1 = 0.2294
        dsi.DCF68inhS5 = 0.1295
        dsi.DCF68ing = 0.0259
        dsi.DCF72inhF1 = 0.02923
        dsi.DCF72inhM1 = 0.1036
        dsi.DCF72inhS1 = 0.2442
        dsi.DCF72ing = 0.0259
        dsi.Daughter = "RH-106"
        dsi.Lambda = 0.0000000215799246749672
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RH-106"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 0.0230511200718306
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RH-99"
        dsi.DCF68inhF1 = 0.001221
        dsi.DCF68inhF5 = 0.001813
        dsi.DCF68inhM1 = 0.002701
        dsi.DCF68inhM5 = 0.003034
        dsi.DCF68inhS1 = 0.003071
        dsi.DCF68inhS5 = 0.003293
        dsi.DCF68ing = 0.001887
        dsi.DCF72inhF1 = 0.001184
        dsi.DCF72inhM1 = 0.002849
        dsi.DCF72inhS1 = 0.003219
        dsi.DCF72ing = 0.001887
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000498294211927727
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TC-99"
        dsi.DCF68inhF1 = 0.001073
        dsi.DCF68inhF5 = 0.00148
        dsi.DCF68inhM1 = 0.01443
        dsi.DCF68inhM5 = 0.01184
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.002886
        dsi.DCF72inhF1 = 0.001073
        dsi.DCF72inhM1 = 0.0148
        dsi.DCF72inhS1 = 0.0481
        dsi.DCF72ing = 0.002368
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000000000104047885957509
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "TC-99M"
        dsi.DCF68inhF1 = 0.0000444
        dsi.DCF68inhF5 = 0.000074
        dsi.DCF68inhM1 = 0.0000703
        dsi.DCF68inhM5 = 0.0001073
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0000814
        dsi.DCF72inhF1 = 0.0000444
        dsi.DCF72inhM1 = 0.0000703
        dsi.DCF72inhS1 = 0.000074
        dsi.DCF72ing = 0.0000814
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000320543532203828
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "ZR-93"
        dsi.DCF68inhF1 = 0.0925
        dsi.DCF68inhF5 = 0.1073
        dsi.DCF68inhM1 = 0.03552
        dsi.DCF68inhM5 = 0.02442
        dsi.DCF68inhS1 = 0.01147
        dsi.DCF68inhS5 = 0.00629
        dsi.DCF68ing = 0.001036
        dsi.DCF72inhF1 = 0.0925
        dsi.DCF72inhM1 = 0.037
        dsi.DCF72inhS1 = 0.01221
        dsi.DCF72ing = 0.00407
        dsi.Daughter = "NB-93M"
        dsi.Lambda = 0.0000000000000136424811163586
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NB-93M"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.001702
        dsi.DCF68inhM5 = 0.001073
        dsi.DCF68inhS1 = 0.00592
        dsi.DCF68inhS5 = 0.003182
        dsi.DCF68ing = 0.000444
        dsi.DCF72inhF1 = 0.000814
        dsi.DCF72inhM1 = 0.001887
        dsi.DCF72inhS1 = 0.00666
        dsi.DCF72ing = 0.000444
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000136171783791879
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SR-90"
        dsi.DCF68inhF1 = 0.0888
        dsi.DCF68inhF5 = 0.111
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0.555
        dsi.DCF68inhS5 = 0.2849
        dsi.DCF68ing = 0.1036
        dsi.DCF72inhF1 = 0.0888
        dsi.DCF72inhM1 = 0.1332
        dsi.DCF72inhS1 = 0.592
        dsi.DCF72ing = 0.1036
        dsi.Daughter = "Y-90"
        dsi.Lambda = 0.00000000076001760296298
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "Y-90"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.00518
        dsi.DCF68inhM5 = 0.00592
        dsi.DCF68inhS1 = 0.00555
        dsi.DCF68inhS5 = 0.00629
        dsi.DCF68ing = 0.00999
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0.00518
        dsi.DCF72inhS1 = 0.00555
        dsi.DCF72ing = 0.00999
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000300596199223883
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "RB-86"
        dsi.DCF68inhF1 = 0.003552
        dsi.DCF68inhF5 = 0.00481
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.01036
        dsi.DCF72inhF1 = 0.003441
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.01036
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000430347431178865
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "KR-85"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000204283005260697
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "SE-79"
        dsi.DCF68inhF1 = 0.00444
        dsi.DCF68inhF5 = 0.00592
        dsi.DCF68inhM1 = 0.01073
        dsi.DCF68inhM5 = 0.01147
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.01073
        dsi.DCF72inhF1 = 0.00407
        dsi.DCF72inhM1 = 0.00962
        dsi.DCF72inhS1 = 0.02516
        dsi.DCF72ing = 0.01073
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000000000744598969341439
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "ZN-65"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0.01073
        dsi.DCF68inhS5 = 0.01036
        dsi.DCF68ing = 0.01443
        dsi.DCF72inhF1 = 0.00814
        dsi.DCF72inhM1 = 0.00592
        dsi.DCF72inhS1 = 0.0074
        dsi.DCF72ing = 0.01443
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000328886845079999
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CU-64"
        dsi.DCF68inhF1 = 0.0001406
        dsi.DCF68inhF5 = 0.0002516
        dsi.DCF68inhM1 = 0.000407
        dsi.DCF68inhM5 = 0.000555
        dsi.DCF68inhS1 = 0.000444
        dsi.DCF68inhS5 = 0.000555
        dsi.DCF68ing = 0.000444
        dsi.DCF72inhF1 = 0.0001295
        dsi.DCF72inhM1 = 0.000407
        dsi.DCF72inhS1 = 0.000444
        dsi.DCF72ing = 0.000444
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000151595058254369
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NI-63"
        dsi.DCF68inhF1 = 0.001628
        dsi.DCF68inhF5 = 0.001924
        dsi.DCF68inhM1 = 0.001628
        dsi.DCF68inhM5 = 0.001147
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000555
        dsi.DCF72inhF1 = 0.001628
        dsi.DCF72inhM1 = 0.001776
        dsi.DCF72inhS1 = 0.00481
        dsi.DCF72ing = 0.000555
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000000217040600055634
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CO-60"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.03552
        dsi.DCF68inhM5 = 0.02627
        dsi.DCF68inhS1 = 0.1073
        dsi.DCF68inhS5 = 0.0629
        dsi.DCF68ing = 0.01258
        dsi.DCF72inhF1 = 0.01924
        dsi.DCF72inhM1 = 0.037
        dsi.DCF72inhS1 = 0.1147
        dsi.DCF72ing = 0.01258
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000416694548950615
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CU-59"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "NI-59"
        dsi.Lambda = 0.00850487338110362
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NI-59"
        dsi.DCF68inhF1 = 0.000666
        dsi.DCF68inhF5 = 0.000814
        dsi.DCF68inhM1 = 0.000481
        dsi.DCF68inhM5 = 0.0003478
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0002331
        dsi.DCF72inhF1 = 0.000666
        dsi.DCF72inhM1 = 0.000481
        dsi.DCF72inhS1 = 0.001628
        dsi.DCF72ing = 0.0002331
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000000000289052202068368
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "FE-59"
        dsi.DCF68inhF1 = 0.00814
        dsi.DCF68inhF5 = 0.0111
        dsi.DCF68inhM1 = 0.01295
        dsi.DCF68inhM5 = 0.01184
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00666
        dsi.DCF72inhF1 = 0.00814
        dsi.DCF72inhM1 = 0.01369
        dsi.DCF72inhS1 = 0.0148
        dsi.DCF72ing = 0.00666
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000180301984763151
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "ZN-59"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0
        dsi.DCF72inhF1 = 0
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0
        dsi.Daughter = "CU-59"
        dsi.Lambda = 3.80850099208761
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CO-57"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.001924
        dsi.DCF68inhM5 = 0.001443
        dsi.DCF68inhS1 = 0.003478
        dsi.DCF68inhS5 = 0.00222
        dsi.DCF68ing = 0.000777
        dsi.DCF72inhF1 = 0.000703
        dsi.DCF72inhM1 = 0.002035
        dsi.DCF72inhS1 = 0.0037
        dsi.DCF72ing = 0.000777
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000295228409952028
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "FE-55"
        dsi.DCF68inhF1 = 0.002849
        dsi.DCF68inhF5 = 0.003404
        dsi.DCF68inhM1 = 0.001369
        dsi.DCF68inhM5 = 0.001221
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.001221
        dsi.DCF72inhF1 = 0.002849
        dsi.DCF72inhM1 = 0.001406
        dsi.DCF72inhS1 = 0.000666
        dsi.DCF72ing = 0.001221
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000800455857348037
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "MN-54"
        dsi.DCF68inhF1 = 0.003219
        dsi.DCF68inhF5 = 0.00407
        dsi.DCF68inhM1 = 0.00555
        dsi.DCF68inhM5 = 0.00444
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.002627
        dsi.DCF72inhF1 = 0.003145
        dsi.DCF72inhM1 = 0.00555
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.002627
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000257033731002063
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CR-51"
        dsi.DCF68inhF1 = 0.0000777
        dsi.DCF68inhF5 = 0.000111
        dsi.DCF68inhM1 = 0.0001147
        dsi.DCF68inhM5 = 0.0001258
        dsi.DCF68inhS1 = 0.0001332
        dsi.DCF68inhS5 = 0.0001332
        dsi.DCF68ing = 0.0001406
        dsi.DCF72inhF1 = 0.000074
        dsi.DCF72inhM1 = 0.0001184
        dsi.DCF72inhS1 = 0.0001369
        dsi.DCF72ing = 0.0001406
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000289596130747637
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CA-45"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0.00999
        dsi.DCF68inhM5 = 0.00851
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.002812
        dsi.DCF72inhF1 = 0.001702
        dsi.DCF72inhM1 = 0.00999
        dsi.DCF72inhS1 = 0.01369
        dsi.DCF72ing = 0.002627
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000493360605869036
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CL-36"
        dsi.DCF68inhF1 = 0.001258
        dsi.DCF68inhF5 = 0.001813
        dsi.DCF68inhM1 = 0.02553
        dsi.DCF68inhM5 = 0.01887
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.003441
        dsi.DCF72inhF1 = 0.001221
        dsi.DCF72inhM1 = 0.02701
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.003441
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000000000729717897861466
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "S-35"
        dsi.DCF68inhF1 = 0.0001961
        dsi.DCF68inhF5 = 0.000296
        dsi.DCF68inhM1 = 0.00481
        dsi.DCF68inhM5 = 0.00407
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.002849
        dsi.DCF72inhF1 = 0.0001887
        dsi.DCF72inhM1 = 0.00518
        dsi.DCF72inhS1 = 0.00703
        dsi.DCF72ing = 0.002849
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000918225570795056
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "P-33"
        dsi.DCF68inhF1 = 0.0003552
        dsi.DCF68inhF5 = 0.000518
        dsi.DCF68inhM1 = 0.00518
        dsi.DCF68inhM5 = 0.00481
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.000888
        dsi.DCF72inhF1 = 0.0003404
        dsi.DCF72inhM1 = 0.00555
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.000888
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000316470880159227
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "P-32"
        dsi.DCF68inhF1 = 0.00296
        dsi.DCF68inhF5 = 0.00407
        dsi.DCF68inhM1 = 0.01184
        dsi.DCF68inhM5 = 0.01073
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.00888
        dsi.DCF72inhF1 = 0.002849
        dsi.DCF72inhM1 = 0.01258
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.00888
        dsi.Daughter = "END"
        dsi.Lambda = 0.000000562511345676371
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "NA-22"
        dsi.DCF68inhF1 = 0.00481
        dsi.DCF68inhF5 = 0.0074
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.01184
        dsi.DCF72inhF1 = 0.00481
        dsi.DCF72inhM1 = 0
        dsi.DCF72inhS1 = 0
        dsi.DCF72ing = 0.01184
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000843912426542826
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "C-14"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.002146
        dsi.DCF72inhF1 = 0.00074
        dsi.DCF72inhM1 = 0.0074
        dsi.DCF72inhS1 = 0.02146
        dsi.DCF72ing = 0.002146
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000000385342258344388
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "H-3"
        dsi.DCF68inhF1 = 0
        dsi.DCF68inhF5 = 0
        dsi.DCF68inhM1 = 0
        dsi.DCF68inhM5 = 0
        dsi.DCF68inhS1 = 0
        dsi.DCF68inhS5 = 0
        dsi.DCF68ing = 0.0001554
        dsi.DCF72inhF1 = 0.00002294
        dsi.DCF72inhM1 = 0.0001665
        dsi.DCF72inhS1 = 0.000962
        dsi.DCF72ing = 0.0001554
        dsi.Daughter = "END"
        dsi.Lambda = 0.00000000178283350045699
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "CD-109"
        dsi.DCF68inhF1 = 0.02997
        dsi.DCF68inhF5 = 0.03552
        dsi.DCF68inhM1 = 0.02294
        dsi.DCF68inhM5 = 0.01887
        dsi.DCF68inhS1 = 0.02146
        dsi.DCF68inhS5 = 0.01628
        dsi.DCF68ing = 0.0074
        dsi.DCF72inhF1 = 0.02997
        dsi.DCF72inhM1 = 0.02442
        dsi.DCF72inhS1 = 0.02294
        dsi.DCF72ing = 0.0074
        dsi.Daughter = "END"
        dsi.Lambda = 0.0000000173873793065375
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        dsi = New DecaySeriesItem
        dsi.Isotope = "F-18"
        dsi.DCF68inhF1 = 0.000111
        dsi.DCF68inhF5 = 0.0001998
        dsi.DCF68inhM1 = 0.0002109
        dsi.DCF68inhM5 = 0.0003293
        dsi.DCF68inhS1 = 0.000222
        dsi.DCF68inhS5 = 0.0003441
        dsi.DCF68ing = 0.0001813
        dsi.DCF72inhF1 = 0.0001036
        dsi.DCF72inhM1 = 0.0002072
        dsi.DCF72inhS1 = 0.0002183
        dsi.DCF72ing = 0.0001813
        dsi.Daughter = "END"
        dsi.Lambda = 0.000105242352275963
        dsi.BranchingRatio = 1
        dci.Add(dsi)

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        GetDecaySeries = dci

    End Function 'GetDecaySeries

    Public Function GetDecayChain( _
    sParent As String, ByVal sTerminal As String, _
    ByRef gdcdci() As Collection, _
    Optional Instance As Integer = 1, _
    Optional currBranch As Integer = 1, _
    Optional nextBranch As Integer = 1, _
    Optional ByRef pds As Collection = Nothing, _
    Optional OptionalSortOrder As Integer = 1) As Boolean
        '* Usage:       Gets decay chain from sParent to sTerminal including all branches
        '* Input:       sParent = starting member isotope
        '*              sTerminal = last member isotope
        '*              gdcdci() = an empty array of Collections of DecaySeriesItems
        '*              Instance = which recursion instance is this
        '*              currBranch = which member of the gdcdci() is being loaded
        '*              nextBranch = which member is next to be loaded
        '* Returns:     Nothing, but gdcdci() is fully loaded and ready for use
        '* Author:      J. J. Prowse
        '* Date:        7/31/2015

        Dim Msg As String
        Dim x As Integer
        Dim y As Integer
        Dim z As Integer
        Dim bRsp As Boolean

        On Error GoTo 0

        'Debug.Print "CB=" & currBranch & " NB=" & nextBranch & " N=" & gdcdci(currBranch).Count

        'If nextBranch > 1 Then
        ' MsgBox("CB=" & currBranch & " NB=" & nextBranch & " N=" & gdcdci(currBranch).Count) '<<debug>>
        ' End If


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
            If Not gdcdci(x) Is Nothing Then
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
        '* Author:      J. J. Prowse
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

        If DirectCast(vdcdci.Item(1).Isotope, String) = sParent And _
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
        '* Author:      J. J. Prowse
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
        '* Author:      J. J. Prowse
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
        '* Author:      J. J. Prowse
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
        '* Author:      J. J. Prowse
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
            symA = symA & "M"
        End If
        x = InStr(1, b, "-")
        symB = Left(b, x)
        If Right(b, 1) = "M" Then
            symB = symB & "M"
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
        '* Author:      J. J. Prowse
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
        '* Author:      J. J. Prowse
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
        '* Author:      J. J. Prowse
        '* Date:        8/16/2015

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
        Dim rRegex As Regex = New Regex(pattern)
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
        iSheet.Cells(r, c + 13) = "Daughter"
        iSheet.Cells(r, c + 14) = "BR"
        r = r + 1 'increment to the next row

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
            iSheet.Cells(r + x - 1, c + 13) = pds.Item(x).Daughter
            iSheet.Cells(r + x - 1, c + 14) = pds.Item(x).BranchingRatio
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
        ReferenceToRange = ExcelDnaUtil.Application.range(strAddress)
    End Function

    Public Function VerifyIsotope(uIsotope As String) As Boolean
        '* Usage:       Verifies isotope is in Class
        '* Input:       uIsotope (e.g., CS-137)
        '* Returns:     True if successful, else False
        '* Author:      J. J. Prowse
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