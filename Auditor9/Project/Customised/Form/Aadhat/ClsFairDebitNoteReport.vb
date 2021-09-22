Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsFairDebitNoteReport

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Public Const Col1SearchCode As String = "Search Code"

    Dim rowReportFor As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowParty As Integer = 4
    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property
    Public Property ShowReportType() As String
        Get
            ShowReportType = mShowReportType
        End Get
        Set(ByVal value As String)
            mShowReportType = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.Code As Code, Sg.Name AS Party, Sg.Address FROM ViewHelpSubgroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash')  "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Dim mHelpTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM PurchInvoiceDetail H "
    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Customer' as Code, 'Customer' as Name 
                            Union All Select 'Supplier' as Code, 'Supplier' as Name 
                            "
            ReportFrm.CreateHelpGrid("Report For", "Report For", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Supplier")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry,, 600, 650, 300)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcFairDebitNoteReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcFairDebitNoteReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Customer Discount Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            If ReportFrm.FGetText(rowReportFor) = "Supplier" Then
                mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') "
            Else
                mCondStr = " Where VT.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "') "
            End If
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", rowParty)
            'mCondStr = mCondStr & " And (CharIndex('+' || 'FAIR',H.Tags) > 0 Or CharIndex('+' || 'FAIR',AgH.Tags) > 0 ) "
            mCondStr = mCondStr & " And (CharIndex('+' || 'FAIR',H.Tags) > 0 ) "

            If ReportFrm.FGetText(rowReportFor) = "Supplier" Then
                mQry = " SELECT H.DocID, L.Sr, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.ManualRefNo As InvoiceNo,
                    Prs.Name As Process, H.Vendor, Sku.ItemGroup, Sku.ItemCategory,
                    Sku.BaseItem, Sku.Dimension1, Sku.Dimension2, Sku.Dimension3, Sku.Dimension4, Sku.Size,
                    Party.Name As VendorName, H.VendorSalesTaxNo as PartyGstNo,
                    Agent.Code As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.VendorDocNo as PartyInvoiceNo, strftime('%d/%m/%Y', H.VendorDocDate) As PartyInvoiceDate, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as ManualRefNo, 
                    L.Item, I.Specification As ItemSpecification, I.HSN, 
                    IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, I.Description As ItemDesc, 
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                    Sg.FairDiscountPer,
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer  as nVarchar) End) as DiscountPer, 
                    L.DiscountAmount + L.AdditionalDiscountAmount as Discount, 
                    L.Taxable_Amount, L.Net_Amount, L.Qty, L.Unit, 
                    L.DealQty, L.DealUnit,
                    L.Rate, L.Amount + ((L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) * (Case When L.Amount < 0 Then -1 Else 1 End)) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax, H.Tags
                    FROM PurchInvoice H 
                    Left Join PurchInvoiceDetail L On H.DocID = L.DocID                     
                    Left Join PurchInvoice AgH On L.ReferenceDocID = Agh.DocID
                    LEFT JOIN SubGroup Prs On H.Process = Prs.SubCode
                    LEFT JOIN Item Sku ON Sku.Code = L.Item
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item I ON Sku.BaseItem = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    Left Join viewHelpSubGroup Party On H.Vendor = Party.Code                     
                    Left Join SubGroup Sg On H.BillToParty = Sg.SubCode                     
                    Left Join (Select SILTV.Subcode,SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code ) as LTV On Party.Code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.VendorCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    Left Join (select PI.DocID, Max(SO.Tags) as Tags from purchInvoice PI
                            Left Join SaleInvoice SI On PI.GenDociD = SI.DocID
                            Left Join SaleInvoiceDetail SIL On SIL.DocID = SI.DocID
                            Left Join SaleInvoice SO On SIL.SaleInvoice = SO.DocID
                            Where SO.Tags Is Not Null 
                            group By PI.DocID) as SOTag On H.DocID = SOTag.DocID
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As DocDate, Max(VMain.InvoiceNo) As DocNo, 
                    Max(VMain.PartyInvoiceNo) As PartyDocNo, Max(Vmain.PartyInvoiceDate) as PartyDocDate,
                    Max(VMain.VendorName) As Party, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, 
                    IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As NetAmount,
                    IfNull(Max(VMain.FairDiscountPer),0) As FairDiscountPer,
                    IfNull(Sum(VMain.AmountExDiscount),0) * IfNull(Max(VMain.FairDiscountPer),0) / 100 As FairDiscount       
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
                DsHeader = AgL.FillData(mQry, AgL.GCn)
            Else
                mQry = " SELECT H.DocID, L.Sr, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.ManualRefNo As InvoiceNo,
                    Prs.Name As Process, H.SaleToParty, Sku.ItemGroup, Sku.ItemCategory,
                    Sku.BaseItem, Sku.Dimension1, Sku.Dimension2, Sku.Dimension3, Sku.Dimension4, Sku.Size,
                    Party.Name As CustomerName, H.SaleToPartySalesTaxNo as PartyGstNo,
                    Agent.Code As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as ManualRefNo, 
                    L.Item, I.Specification As ItemSpecification, I.HSN, 
                    IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, I.Description As ItemDesc, 
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                    Sg.FairDiscountPer,
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer  as nVarchar) End) as DiscountPer, 
                    L.DiscountAmount + L.AdditionalDiscountAmount as Discount, 
                    L.Taxable_Amount, L.Net_Amount, L.Qty, L.Unit, 
                    L.DealQty, L.DealUnit,
                    L.Rate, L.Amount + ((L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) * (Case When L.Amount < 0 Then -1 Else 1 End)) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax, H.Tags
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID                     
                    Left Join SaleInvoice AgH On L.ReferenceDocID = Agh.DocID
                    LEFT JOIN SubGroup Prs On H.Process = Prs.SubCode
                    LEFT JOIN Item Sku ON Sku.Code = L.Item
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item I ON Sku.BaseItem = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    Left Join viewHelpSubGroup Party On H.SaleToParty = Party.Code                     
                    Left Join SubGroup Sg On H.BillToParty = Sg.SubCode                     
                    Left Join (Select SILTV.Subcode,SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code ) as LTV On Party.Code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    Left Join (select SI.DocID, Max(SO.Tags) as Tags from SaleInvoice SI
                            Left Join SaleInvoiceDetail SIL On SIL.DocID = SI.DocID
                            Left Join SaleInvoice SO On SIL.SaleInvoice = SO.DocID
                            Where SO.Tags Is Not Null 
                            group By SI.DocID) as SOTag On H.DocID = SOTag.DocID
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As DocDate, Max(VMain.InvoiceNo) As DocNo,                     
                    Max(VMain.CustomerName) As Party, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, 
                    IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As NetAmount,
                    IfNull(Max(VMain.FairDiscountPer),0) As FairDiscountPer,
                    IfNull(Sum(VMain.AmountExDiscount),0) * IfNull(Max(VMain.FairDiscountPer),0) / 100 As FairDiscount       
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
                DsHeader = AgL.FillData(mQry, AgL.GCn)

            End If

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Fair Debit Note Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFairDebitNoteReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
End Class
