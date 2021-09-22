'https://www.teachoo.com/6645/1948/Download-GSTR-3B-Return-in-Excel-Format/category/GST-Return-Format-/
Public Class FrmGSTReport
    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""
    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""


    Public Const PlaceOfSupplay_WithinState = "Within State"
    Public Const PlaceOfSupplay_OutsideState = "Outside State"

    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""
    Dim DtTable As DataTable = Nothing
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnGST3B.Click
        ProcGSTR3BReports()
    End Sub
    Private Sub BtnGSTR1_Click(sender As Object, e As EventArgs) Handles BtnGSTR1.Click
        ProcGSTR1Reports()
    End Sub

#Region "GSTR-3B Reports"
    Private Sub ProcGSTR3BReports()
        Dim SubTitle$ = ""
        Dim GroupHeaderTitle1$ = "", GroupHeaderTitle2$ = ""
        Dim IsReturn As Integer = 0
        Dim AssessmentYear$ = ""
        Dim OutputFile As String = My.Application.Info.DirectoryPath + "\TaxReturns\GSTR3B.xlsm"
        Dim mCondStr$ = ""

        Dim ToDate As DateTime = "25/Mar/2018"
        Dim xlApp As Excel.Application
        Dim TemplateWorkBook As Excel.Workbook
        Dim OutputWorkBook As Excel.Workbook

        xlApp = New Excel.Application
        xlApp.AlertBeforeOverwriting = False
        xlApp.DisplayAlerts = False

        TemplateWorkBook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\Templates\" + "GSTR3B_Excel_Utility_V3.0.xlsm")
        TemplateWorkBook.SaveAs(OutputFile)
        xlApp.Workbooks.Close()
        OutputWorkBook = xlApp.Workbooks.Open(OutputFile)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = OutputWorkBook.Worksheets("GSTR-3B")

            'For GSTIN, Legal Name of the registered person
            mQry = " SELECT Sg.Name, Sgr.RegistrationNo
                                from Division D
                                left join SubGroup Sg On D.SubCode = Sg.SubCode
                                LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode
                                where D.Div_Code = '" & AgL.PubDivCode & "' AND  Sgr.RegistrationType = 'GSTIN' "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(5, 3).Value = DtTable.Rows(0)("RegistrationNo")
                xlWorkSheet.Cells.Item(6, 3).Value = DtTable.Rows(0)("Name")
            End If

            'For Year
            xlWorkSheet.Cells.Item(5, 7).Value = AgL.XNull(AgL.Dman_Execute(" Select cyear From Company Where Comp_Code = '" & AgL.PubCompCode & "' ", AgL.GCn).ExecuteScalar)

            'Month	
            Dim newdate = String.Format("{0:yyyy-MM-dd}", ToDate)
            xlWorkSheet.Cells.Item(6, 7).Value = AgL.XNull(AgL.Dman_Execute(" select case strftime('%m', '" & newdate & "') when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' 
                    when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' 
                    when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' 
                    when '12' then 'December' else '' end as month ", AgL.GCn).ExecuteScalar)


            '3.1 (a) Outward Taxable  supplies  (other than zero rated, nil rated and exempted)
            'Sales Amount And Tax On It (Both Local And Central Combined)
            mQry = " SELECT Sum(L.Taxable_Amount) as TotalTaxablevalue, Sum(L.Tax1) As IntegratedTax, Sum(L.Tax2) as CentralTax, Sum(L.Tax3) as StateTax, 0 As Cess
                    from SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(11, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(11, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(11, 5).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(11, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (b) Outward Taxable  supplies  (zero rated )
            'Export Sales (Both on Bond Without Bond)
            mQry = " SELECT 0 as TotalTaxablevalue, 0 As IntegratedTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(12, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(12, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(12, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (c) Other Outward Taxable  supplies (Nil rated, exempted)
            'Goods Covered in Excemtion Notification & Goods Having rate 0%
            mQry = " SELECT ifnull(Sum(L.Taxable_Amount),0) as TotalTaxablevalue
                    from SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID 
                    Where ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) = 0 "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(13, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
            End If

            '3.1 (d) Inward supplies (liable to reverse charge) 
            'Tax to be Paid on reverse charge.
            mQry = " SELECT Sum(L.Taxable_Amount) as TotalTaxablevalue, Sum(L.Tax1) As IntegratedTax, Sum(L.Tax2) as CentralTax, Sum(L.Tax3) as StateTax, 0 As Cess
                    from PurchInvoice H 
                    left join PurchInvoiceDetail L On H.DocID = L.DocID 
                    Where H.SalesTaxGroupParty = 'Unregistered'"
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(14, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(14, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(14, 5).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(14, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (e) Non-GST Outward supplies
            'Goods not covered in GST, Like Diesel
            mQry = " SELECT 0 as TotalTaxablevalue "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(15, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
            End If


            '3.2  Of the supplies shown in 3.1 (a), details of inter-state supplies made to unregistered persons, composition taxable person and UIN holders						
            'Suppliers Made to UnRegistered Person : Only InterState Sales to Unregistered
            'Suppliers Made to Composition Taxable Person : Only InterState Sales to Composition Dealer
            'Suppliers Made to UiN Holders : Only InterState Sales to UIN Holders like Embassy
            mQry = " SELECT S.Description As PlaceOfSupply,
                    Sum(CASE when H.SalesTaxGroupParty =  'Unregistered' THEN L.Taxable_Amount Else 0 END) As TotalTaxablevalue_Unregistered,
                    0 As AmountOfIntegratedTax_Unregistered,
                    Sum(CASE when H.SalesTaxGroupParty = 'Composition' THEN L.Taxable_Amount Else 0 END) As TotalTaxablevalue_Composition,
                    0 As AmountOfIntegratedTax_Composition,
                    0 As TotalTaxablevalue_UINholders,
                    0 As AmountOfIntegratedTax_UINholders
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L on H.DocID = L.DocID
                    Left join City C On H.SaleToPartyCity = C.CityCode
                    left join State S on C.State = S.Code
                    Where H.PlaceOfSupply = '" & PlaceOfSupplay_OutsideState & "' 
                    Group By S.Description "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(79, 2).Value = DtTable.Rows(0)("PlaceOfSupply")

                xlWorkSheet.Cells.Item(79, 3).Value = DtTable.Rows(0)("TotalTaxablevalue_Unregistered")
                xlWorkSheet.Cells.Item(79, 4).Value = DtTable.Rows(0)("AmountOfIntegratedTax_Unregistered")

                xlWorkSheet.Cells.Item(79, 5).Value = DtTable.Rows(0)("TotalTaxablevalue_Composition")
                xlWorkSheet.Cells.Item(79, 6).Value = DtTable.Rows(0)("AmountOfIntegratedTax_Composition")

                xlWorkSheet.Cells.Item(79, 7).Value = DtTable.Rows(0)("TotalTaxablevalue_UINholders")
                xlWorkSheet.Cells.Item(79, 8).Value = DtTable.Rows(0)("AmountOfIntegratedTax_UINholders")
            End If


            '4. Eligible ITC	(1)   Import of goods 
            'Tax Charged on Import of Goods liKe IGST
            mQry = " SELECT 0 as IntegratedTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(22, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(22, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(2)   Import of services
            'Tax paid on Import of service (Covered under Reverse Charge) 
            mQry = " SELECT 0 as IntegratedTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(23, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(23, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(3)   Inward supplies liable to reverse charge        (other than 1 &2 above)
            'All Other purchase from unregistered Dealer (Local Purchase)
            mQry = " SELECT 0 as IntegratedTax, 0 As CentralTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(24, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(24, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(24, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(4)   Inward supplies from ISD
            'Input from other Branches (Input Service Distributors)
            mQry = " SELECT 0 as IntegratedTax, 0 As CentralTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(25, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(25, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(25, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(5)   All other ITC
            'Normal Purchase from Registered Dealer
            mQry = " SELECT Sum(L.Tax1) As IntegratedTax, Sum(L.Tax2) as CentralTax, Sum(L.Tax3) as StateTax, 0 As Cess
                    from PurchInvoice H 
                    left join PurchInvoiceDetail L on H.DocID = L.DocID
                    Where H.SalesTaxGroupParty = 'Registered' 
                    And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) <> 0 "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(26, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(26, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(26, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '5. Values of exempt, From a supplier under composition scheme, Exempt  and Nil rated supply	
            'Purchase of Goods 0%, Exempted etc
            mQry = " SELECT Case When H.PlaceOfSupply = '" & PlaceOfSupplay_OutsideState & "' Then Sum(L.Taxable_Amount) Else 0 End As InterStatesupplies,
                    Case When H.PlaceOfSupply <> '" & PlaceOfSupplay_OutsideState & "' Then   Sum(L.Taxable_Amount) Else 0 End As Intrastatesupplies
                    from PurchInvoice H 
                    left join PurchInvoiceDetail L on H.DocID = L.DocID
                    Where ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) = 0 "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(39, 4).Value = DtTable.Rows(0)("InterStatesupplies")
                xlWorkSheet.Cells.Item(39, 5).Value = DtTable.Rows(0)("Intrastatesupplies")
            End If

            '5. Values of exempt, Non GST supply	
            'Purchase of Goods not Covered on GST
            mQry = " SELECT 0 as InterStatesupplies, 0 As Intrastatesupplies "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(40, 4).Value = DtTable.Rows(0)("InterStatesupplies")
                xlWorkSheet.Cells.Item(40, 5).Value = DtTable.Rows(0)("Intrastatesupplies")
            End If


            '5.1 Interest & late fee payable	
            'Intrest @18% on late payment of tax
            mQry = " SELECT 0 as IntegratedTax, 0 As CentralTax, 0 As StateTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(56, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(56, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(56, 5).Value = DtTable.Rows(0)("StateTax")
                xlWorkSheet.Cells.Item(56, 6).Value = DtTable.Rows(0)("Cess")
            End If


            ClsMain.FReleaseObjects(xlWorkSheet)

            OutputWorkBook.Save()
            OutputWorkBook.Close()
            xlApp.Quit()

            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)

            System.Diagnostics.Process.Start(OutputFile)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
            OutputWorkBook.Close()
            xlApp.Quit()
            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
        End Try
    End Sub
    Private Sub ProcGSTR1Reports()
        Dim SubTitle$ = ""
        Dim GroupHeaderTitle1$ = "", GroupHeaderTitle2$ = ""
        Dim IsReturn As Integer = 0
        Dim AssessmentYear$ = ""
        Dim OutputFile As String = My.Application.Info.DirectoryPath + "\TaxReturns\GSTR1.xlsx"
        Dim I As Integer
        Dim mCondStr$ = ""

        Dim ToDate As DateTime = "25/Mar/2018"
        Dim xlApp As Excel.Application
        Dim TemplateWorkBook As Excel.Workbook
        Dim OutputWorkBook As Excel.Workbook

        xlApp = New Excel.Application
        xlApp.AlertBeforeOverwriting = False
        xlApp.DisplayAlerts = False

        TemplateWorkBook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\Templates\" + "GSTR1_Excel_Workbook_Template_V1.5.xlsx")
        TemplateWorkBook.SaveAs(OutputFile)
        xlApp.Workbooks.Close()
        OutputWorkBook = xlApp.Workbooks.Open(OutputFile)

        Try
            FWriteGSTR1B2B(OutputWorkBook)
            FWriteGSTR1B2CL(OutputWorkBook)
            FWriteGSTR1B2CS(OutputWorkBook)
            FWriteGSTR1CDNR(OutputWorkBook)
            FWriteGSTR1CDNUR(OutputWorkBook)
            FWriteGSTR1EXEMP(OutputWorkBook)
            FWriteGSTR1HSN(OutputWorkBook)
            FWriteGSTR1DOC(OutputWorkBook)

            OutputWorkBook.Save()
            OutputWorkBook.Close()
            xlApp.Quit()

            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
            ClsMain.FReleaseObjects(OutputWorkBook)

            System.Diagnostics.Process.Start(OutputFile)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
            OutputWorkBook.Close()
            xlApp.Quit()
            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
        End Try
    End Sub

    Private Sub FWriteGSTR1B2B(ByVal xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("b2b")

        mQry = " SELECT Max(Sgr.RegistrationNo) As GSTINofRecipient, Max(Sg.Name) As ReceiverName, Max(H.ReferenceNo) As InvoiceNumber,
                    Max(H.V_Date) As InvoiceDate, Max(H.Net_Amount) As InvoiceValue, Max(S.ManualCode || '-' || S.Description) As PlaceOfSupply, 'N' As ReverseCharge,
                    0 As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    Max(IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0)) As Rate,	
                    Sum(L.Taxable_Amount) As TaxableValue, 0 As CessAmount
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'GSTIN'
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code
                    WHERE H.SalesTaxGroupParty = 'Registered'
                    Group BY L.DocID, SalesTaxGroupItem "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTable.Rows.Count > 0 Then
            For I = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells(I + 5, 1) = DtTable.Rows(I)("GSTINofRecipient")
                xlWorkSheet.Cells(I + 5, 2) = DtTable.Rows(I)("ReceiverName")
                xlWorkSheet.Cells(I + 5, 3) = DtTable.Rows(I)("InvoiceNumber")
                xlWorkSheet.Cells(I + 5, 4) = CDate(DtTable.Rows(I)("InvoiceDate")).ToString("dd-MMM-yyyy")
                xlWorkSheet.Cells(I + 5, 5) = DtTable.Rows(I)("InvoiceValue")
                xlWorkSheet.Cells(I + 5, 6) = DtTable.Rows(I)("PlaceOfSupply")
                xlWorkSheet.Cells(I + 5, 7) = DtTable.Rows(I)("ReverseCharge")
                xlWorkSheet.Cells(I + 5, 8) = DtTable.Rows(I)("ApplicableTaxRate")
                xlWorkSheet.Cells(I + 5, 9) = DtTable.Rows(I)("InvoiceType")
                xlWorkSheet.Cells(I + 5, 10) = DtTable.Rows(I)("ECommerceGSTIN")
                xlWorkSheet.Cells(I + 5, 11) = DtTable.Rows(I)("Rate")
                xlWorkSheet.Cells(I + 5, 12) = DtTable.Rows(I)("TaxableValue")
                xlWorkSheet.Cells(I + 5, 13) = DtTable.Rows(I)("CessAmount")
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub

    Private Sub FWriteGSTR1B2CL(ByVal xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("b2cl")

        mQry = " SELECT Max(Sgr.RegistrationNo) As GSTINofRecipient, Max(Sg.Name) As ReceiverName, Max(H.ReferenceNo) As InvoiceNumber,
                    Max(H.V_Date) As InvoiceDate, Max(H.Net_Amount) As InvoiceValue, Max(S.Code + '-' + S.Description) As PlaceOfSupply, 'N' As ReverseCharge,
                    0 As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    Max(IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0)) As Rate,	
                    Sum(L.Taxable_Amount) As TaxableValue, 0 As CessAmount
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'GSTIN'
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code
                    WHERE H.SalesTaxGroupParty = 'Registered'
                    And H.PlaceOfSupply = '" & PlaceOfSupplay_OutsideState & "'
                    And H.Net_Amount > 250000
                    Group BY L.DocID, SalesTaxGroupItem "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTable.Rows.Count > 0 Then
            For I = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells(I + 5, 1) = DtTable.Rows(I)("InvoiceNumber")
                xlWorkSheet.Cells(I + 5, 2) = CDate(DtTable.Rows(I)("InvoiceDate")).ToString("dd-MMM-yyyy")
                xlWorkSheet.Cells(I + 5, 3) = DtTable.Rows(I)("InvoiceValue")
                xlWorkSheet.Cells(I + 5, 4) = DtTable.Rows(I)("PlaceOfSupply")
                xlWorkSheet.Cells(I + 5, 5) = DtTable.Rows(I)("ReverseCharge")
                xlWorkSheet.Cells(I + 5, 6) = DtTable.Rows(I)("ApplicableTaxRate")
                xlWorkSheet.Cells(I + 5, 7) = DtTable.Rows(I)("Rate")
                xlWorkSheet.Cells(I + 5, 8) = DtTable.Rows(I)("TaxableValue")
                xlWorkSheet.Cells(I + 5, 9) = DtTable.Rows(I)("CessAmount")
                xlWorkSheet.Cells(I + 5, 10) = DtTable.Rows(I)("ECommerceGSTIN")
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub

    Private Sub FWriteGSTR1B2CS(ByVal xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("b2cs")

        mQry = " SELECT Max(Sgr.RegistrationNo) As GSTINofRecipient, Max(Sg.Name) As ReceiverName, Max(H.ReferenceNo) As InvoiceNumber,
                    Max(H.V_Date) As InvoiceDate, Max(H.Net_Amount) As InvoiceValue, Max(S.Code + '-' + S.Description) As PlaceOfSupply, 'N' As ReverseCharge,
                    0 As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    Max(IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0)) As Rate,	
                    Sum(L.Taxable_Amount) As TaxableValue, 0 As CessAmount
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'GSTIN'
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code
                    WHERE H.SalesTaxGroupParty = 'UnRegistered'
                    And H.PlaceOfSupply = '" & PlaceOfSupplay_WithinState & "'
                    Group BY L.DocID, SalesTaxGroupItem "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTable.Rows.Count > 0 Then
            For I = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells(I + 5, 1) = DtTable.Rows(I)("Type")
                xlWorkSheet.Cells(I + 5, 2) = DtTable.Rows(I)("PlaceOfSupply")
                xlWorkSheet.Cells(I + 5, 3) = DtTable.Rows(I)("ApplicableTaxRate")
                xlWorkSheet.Cells(I + 5, 4) = DtTable.Rows(I)("Rate")
                xlWorkSheet.Cells(I + 5, 5) = DtTable.Rows(I)("TaxableValue")
                xlWorkSheet.Cells(I + 5, 6) = DtTable.Rows(I)("CessAmount")
                xlWorkSheet.Cells(I + 5, 7) = DtTable.Rows(I)("ECommerceGSTIN")
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1CDNR(ByVal xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("cdnr")

        mQry = " SELECT Sgr.RegistrationNo As GSTINofRecipient, Sg.Name As ReceiverName, Si.ReferenceNo As InvoiceNumber, Si.V_Date As InvoiceDate,
                H.ReferenceNo As LedgerHeadNo, H.V_Date As LedgerHeadDate, substr(Vt.Description,1,1) As DocumentType,
                S.ManualCode || '-' || S.Description As PlaceOfSupply, Lc.Net_Amount As LedgerHeadValue, 
                Null As ApplicableTaxRate, L.SalesTaxGroupItem As Rate,
                0 As TaxableValue, 0 As CessAmount, NUll As PreGST
                From LedgerHead H 
                Left join LedgerHeadDetail L on H.DocID = L.DocID
                LEft join LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID and L.Sr = Lc.Sr
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                left join SubGroup Sg On H.Subcode = Sg.SubCode
                LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'GSTIN'
                Left join SaleInvoiceDetail Sid On L.SpecificationDocID = Sid.DocID And L.SpecificationDocIDSr = Sid.Sr
                Left join SaleInvoice Si On Sid.DocID = Si.DocID
                LEFT JOIN City C On Si.SaleToPartyCity = C.CityCode
                LEFT JOIN State S on C.State = S.Code "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTable.Rows.Count > 0 Then
            For I = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells(I + 5, 1) = DtTable.Rows(I)("GSTINofRecipient")
                xlWorkSheet.Cells(I + 5, 2) = DtTable.Rows(I)("ReceiverName")
                xlWorkSheet.Cells(I + 5, 3) = DtTable.Rows(I)("InvoiceNumber")
                xlWorkSheet.Cells(I + 5, 4) = CDate(DtTable.Rows(I)("InvoiceDate")).ToString("dd-MMM-yyyy")
                xlWorkSheet.Cells(I + 5, 5) = DtTable.Rows(I)("LedgerHeadNo")
                xlWorkSheet.Cells(I + 5, 6) = CDate(DtTable.Rows(I)("LedgerHeadDate")).ToString("dd-MMM-yyyy")
                xlWorkSheet.Cells(I + 5, 7) = DtTable.Rows(I)("DocumentType")
                xlWorkSheet.Cells(I + 5, 8) = DtTable.Rows(I)("PlaceOfSupply")
                xlWorkSheet.Cells(I + 5, 9) = DtTable.Rows(I)("LedgerHeadValue")
                xlWorkSheet.Cells(I + 5, 10) = DtTable.Rows(I)("ApplicableTaxRate")
                xlWorkSheet.Cells(I + 5, 11) = DtTable.Rows(I)("Rate")
                xlWorkSheet.Cells(I + 5, 12) = DtTable.Rows(I)("TaxableValue")
                xlWorkSheet.Cells(I + 5, 13) = DtTable.Rows(I)("CessAmount")
                xlWorkSheet.Cells(I + 5, 14) = DtTable.Rows(I)("PreGST")
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1CDNUR(ByVal xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("cdnur")

        mQry = " SELECT Si.V_Date As InvoiceDate, S.ManualCode || '-' || S.Description As PlaceOfSupply, 
                Lc.Net_Amount As LedgerLineValue, Null As ApplicableTaxRate, L.SalesTaxGroupItem As Rate,
                0 As TaxableValue, 0 As CessAmount, NUll As PreGST
                From LedgerHead H 
                Left join LedgerHeadDetail L on H.DocID = L.DocID
                LEft join LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID and L.Sr = Lc.Sr
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                left join SubGroup Sg On H.Subcode = Sg.SubCode
                LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'GSTIN'
                Left join SaleInvoiceDetail Sid On L.SpecificationDocID = Sid.DocID And L.SpecificationDocIDSr = Sid.Sr
                Left join SaleInvoice Si On Sid.DocID = Si.DocID
                LEFT JOIN City C On Si.SaleToPartyCity = C.CityCode
                LEFT JOIN State S on C.State = S.Code
                Where Si.SalesTaxGroupParty = 'UnRegistered'
                And Si.PlaceOfSupply = 'Outstate'
                And Si.Net_Amount > 250000 "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTable.Rows.Count > 0 Then
            For I = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells(I + 5, 1) = CDate(DtTable.Rows(I)("InvoiceDate")).ToString("dd-MMM-yyyy")
                xlWorkSheet.Cells(I + 5, 2) = DtTable.Rows(I)("PlaceOfSupply")
                xlWorkSheet.Cells(I + 5, 3) = DtTable.Rows(I)("LedgerLineValue")
                xlWorkSheet.Cells(I + 5, 4) = DtTable.Rows(I)("ApplicableTaxRate")
                xlWorkSheet.Cells(I + 5, 5) = DtTable.Rows(I)("Rate")
                xlWorkSheet.Cells(I + 5, 6) = DtTable.Rows(I)("TaxableValue")
                xlWorkSheet.Cells(I + 5, 7) = DtTable.Rows(I)("CessAmount")
                xlWorkSheet.Cells(I + 5, 8) = DtTable.Rows(I)("PreGST")
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1EXEMP(ByVal xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("exemp")

        mQry = " SELECT Ic.Description As Description, 
                Sum(Case When L.SalesTaxGroupItem = 'GST 0%' Then L.Amount Else 0 End) As NilRatedSupplies,
                Sum(Case When L.SalesTaxGroupItem = 'GST Excempt' Then L.Amount Else 0 End) As ExemptedSupplies,
                0 As NonGSTSupplies
                From SaleInvoice H 
                Left join SaleInvoiceDetail L on H.DocId = L.DocID 
                Left join Item I on L.Item = I.Code
                Left Join ItemCategory Ic On I.ItemCategory = Ic.Code
                Where L.SalesTaxGroupItem In ('GST 0%','GST Excempt')
                Group By Ic.Description "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTable.Rows.Count > 0 Then
            For I = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells(I + 5, 1) = DtTable.Rows(I)("Description")
                xlWorkSheet.Cells(I + 5, 2) = DtTable.Rows(I)("NilRatedSupplies")
                xlWorkSheet.Cells(I + 5, 3) = DtTable.Rows(I)("ExemptedSupplies")
                xlWorkSheet.Cells(I + 5, 4) = DtTable.Rows(I)("NonGSTSupplies")
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub

    Private Sub FWriteGSTR1HSN(ByVal xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("hsn")

        mQry = " SELECT I.HSN, Max(Ic.Description) As Description, Max(U.UQC) As UQC,
                Sum(L.Qty) As TotalQuantity, Sum(L.Amount) As TotalValue, Sum(L.Taxable_Amount) As TaxableValue,
                Sum(L.Tax1) As IntegratedTaxAmount,
                Sum(L.Tax2) As CentralTaxAmount,
                Sum(L.Tax3) As StateTaxAmount,
                0 As CessAmount
                From SaleInvoice H 
                Left join SaleInvoiceDetail L on H.DocId = L.DocID 
                Left join Item I on L.Item = I.Code
                Left Join Unit U on I.Unit = U.Code
                Left Join ItemCategory Ic On I.ItemCategory = Ic.Code
                Group By I.HSN "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTable.Rows.Count > 0 Then
            For I = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells(I + 5, 1) = DtTable.Rows(I)("HSN")
                xlWorkSheet.Cells(I + 5, 2) = DtTable.Rows(I)("Description")
                xlWorkSheet.Cells(I + 5, 3) = DtTable.Rows(I)("UQC")
                xlWorkSheet.Cells(I + 5, 4) = DtTable.Rows(I)("TotalQuantity")
                xlWorkSheet.Cells(I + 5, 5) = DtTable.Rows(I)("TotalValue")
                xlWorkSheet.Cells(I + 5, 6) = DtTable.Rows(I)("TaxableValue")
                xlWorkSheet.Cells(I + 5, 7) = DtTable.Rows(I)("IntegratedTaxAmount")
                xlWorkSheet.Cells(I + 5, 8) = DtTable.Rows(I)("CentralTaxAmount")
                xlWorkSheet.Cells(I + 5, 9) = DtTable.Rows(I)("StateTaxAmount")
                xlWorkSheet.Cells(I + 5, 10) = DtTable.Rows(I)("CessAmount")
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1DOC(ByVal xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("docs")

        mQry = " SELECT 'Invoices for outward supply' As NatureOfDocument, Min(H.ReferenceNo) As SrNoFrom, Max(H.ReferenceNo) As SrNoTo, Count(*) As TotalNumber, Null As Cancelled
                    From SaleInvoice H 
                    Left JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTable.Rows.Count > 0 Then
            For I = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells(I + 5, 1) = DtTable.Rows(I)("NatureOfDocument")
                xlWorkSheet.Cells(I + 5, 2) = DtTable.Rows(I)("SrNoFrom")
                xlWorkSheet.Cells(I + 5, 3) = DtTable.Rows(I)("SrNoTo")
                xlWorkSheet.Cells(I + 5, 4) = DtTable.Rows(I)("TotalNumber")
                xlWorkSheet.Cells(I + 5, 5) = DtTable.Rows(I)("Cancelled")
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub
#End Region
End Class