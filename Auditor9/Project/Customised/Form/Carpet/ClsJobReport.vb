Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsJobReport

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

    Public Const Col1Rate As String = "Rate"
    Public Const Col1AmountExDiscount As String = "Amount Ex Discount"
    Public Const Col1Amount As String = "Amount"

    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

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
    Public Shared mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Public Shared mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Public Shared mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Public Shared mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Public Shared mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Public Shared mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Public Shared mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "

    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Doc.Header Wise Detail' as Code, 'Doc.Header Wise Detail' as Name 
                            Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name 
                            Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'HSN Wise Summary' as Code, 'HSN Wise Summary' as Name 
                            Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name 
                            Union All Select 'Item Group Wise Summary' as Code, 'Item Group Wise Summary' as Name 
                            Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name 
                            Union All Select 'City Wise Summary' as Code, 'City Wise Summary' as Name 
                            Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name                             
                            "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Month Wise Summary")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
            ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
            ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice", EntryNCat))
            ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPurchaseAgentQry)
            ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
            ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
            ReportFrm.CreateHelpGrid("HSN", "HSN", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
            ReportFrm.FilterGrid.Rows(13).Visible = False 'Hide HSN Row
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcJobReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, ByVal strNCat As String)
        ReportFrm = mReportFrm
        EntryNCat = strNCat
    End Sub
    Public Sub ProcJobReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            If mReportDefaultText = "" Then
                mReportDefaultText = ReportFrm.Text
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Month Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 1).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, 2).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Item").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 8).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 8).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Item Group").Value
                        mFilterGrid.Item(GFilterCode, 9).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Category Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 10).Value = mGridRow.Cells("Item Category").Value
                        mFilterGrid.Item(GFilterCode, 10).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "City Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 11).Value = mGridRow.Cells("City").Value
                        mFilterGrid.Item(GFilterCode, 11).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "State Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 12).Value = mGridRow.Cells("State").Value
                        mFilterGrid.Item(GFilterCode, 12).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "HSN Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 13).Value = mGridRow.Cells("HSN").Value
                        mFilterGrid.Item(GFilterCode, 13).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail" Or
                                mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If


            'If GRepFormName = PurchaseOrderReport Then
            '    mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseOrder & "', '" & Ncat.PurchaseOrderCancel & "') "
            'Else
            '    mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseInvoice & "', '" & Ncat.PurchaseReturn & "') "
            'End If
            mCondStr = " Where VT.NCat In ('" & Replace(EntryNCat, ",", "','") & "') "
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.BillToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            If ReportFrm.FGetText(7) = "Cash" Then
                mCondStr = mCondStr & " AND Sg.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(7) = "Credit" Then
                mCondStr = mCondStr & " AND Sg.Nature <> 'Cash'"
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 12)
            If ReportFrm.FGetText(13) <> "All" Then
                mCondStr = mCondStr & " And I.HSN = " & AgL.Chk_Text(ReportFrm.FGetText(13)) & " "
            End If

            mQry = " SELECT H.DocID, L.Sr, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.Vendor, Sku.ItemGroup, Sku.ItemCategory,
                    Sku.BaseItem, Sku.Dimension1, Sku.Dimension2, Sku.Dimension3, Sku.Dimension4, Sku.Size,
                    Party.Name As VendorName, H.VendorSalesTaxNo as PartyGstNo,
                    Agent.Code As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.VendorDocNo as InvoiceNo, H.VendorDocDate As PartyInvoiceDate, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as ManualRefNo, 
                    L.Item, I.Specification As ItemSpecification, I.HSN, 
                    IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, I.Description As ItemDesc, 
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer  as nVarchar) End) as DiscountPer, 
                    L.DiscountAmount + L.AdditionalDiscountAmount as Discount, 
                    L.Taxable_Amount, L.Net_Amount, L.Qty, L.Unit, 
                    L.DealQty, L.DealUnit,
                    L.Rate, L.Amount -(L.DiscountAmount + L.AdditionalDiscountAmount) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax
                    FROM PurchInvoice H 
                    Left Join PurchInvoiceDetail L On H.DocID = L.DocID 
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
                    Left Join viewHelpSubgroup Party On H.Vendor = Party.Code                     
                    Left Join viewHelpSubgroup Sg On H.BillToParty = Sg.Code                     
                    Left Join (Select SILTV.Subcode,SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code ) as LTV On Party.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.VendorCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


            If ReportFrm.FGetText(0) = "Doc.Header Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As DocDate, Max(VMain.InvoiceNo) As DocNo, Max(Vmain.PartyInvoiceDate) as PartyDocDate,
                    Max(VMain.VendorName) As Party, Max(Vmain.PartyGstNo) as PartyGstNo, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
            ElseIf ReportFrm.FGetText(0) = "Item Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Doc Date], Max(VMain.InvoiceNo) As [Doc No], Max(Vmain.PartyInvoiceDate) as PartyDocDate,
                    Max(VMain.VendorName) As Party, Max(Vmain.PartyGstNo) as PartyGstNo, 
                    Max(VMain.ItemCategoryDesc) As ItemCategory, 
                    Max(VMain.ItemGroupDesc) As ItemGroup, 
                    Max(VMain.ItemDesc) As Item, 
                    Max(VMain.Dimension1Desc) As Dimension1, 
                    Max(VMain.Dimension2Desc) As Dimension2, 
                    Max(VMain.Dimension3Desc) As Dimension3, 
                    Max(VMain.Dimension4Desc) As Dimension4, 
                    Max(VMain.SizeDesc) As Size, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As AmountExDiscount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.Amount) as Amount,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr 
                    Order By Max(VMain.V_Date_ActualFormat), Max(VMain.InvoiceNo), Vmain.Sr "
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Vendor as SearchCode, Max(VMain.VendorName) As Party, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Vendor 
                    Order By Max(VMain.VendorName)"
            ElseIf ReportFrm.FGetText(0) = "HSN Wise Summary" Then
                mQry = " Select VMain.HSN As SearchCode, VMain.HSN, Max(VMain.ItemCategoryDesc) As [Description], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.Tax1),0) As IGST, IfNull(Sum(VMain.Tax2),0) As CGST, IfNull(Sum(VMain.Tax3),0) As SGST, IfNull(Sum(VMain.Tax4),0) As Cess, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.HSN 
                    Order By VMain.HSN, Max(VMain.ItemCategoryDesc)"
            ElseIf ReportFrm.FGetText(0) = "Item Wise Summary" Then
                mQry = " Select VMain.Item As SearchCode, 
                    Max(VMain.ItemCategoryDesc) As ItemCategory, 
                    Max(VMain.ItemGroupDesc) As ItemGroup, 
                    Max(VMain.ItemDesc) As Item, 
                    Max(VMain.Dimension1Desc) As Dimension1, 
                    Max(VMain.Dimension2Desc) As Dimension2, 
                    Max(VMain.Dimension3Desc) As Dimension3, 
                    Max(VMain.Dimension4Desc) As Dimension4, 
                    Max(VMain.SizeDesc) As Size, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory, VMain.ItemGroup, VMain.Item, 
                    VMain.Dimension1, VMain.Dimension2, VMain.Dimension3, VMain.Dimension4, VMain.Size
                    Order By Max(VMain.ItemDesc)"
            ElseIf ReportFrm.FGetText(0) = "Item Group Wise Summary" Then
                mQry = " Select VMain.ItemGroup as SearchCode, Max(VMain.ItemGroupDesc) As [Item Group], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemGroup 
                    Order By Max(VMain.ItemGroupDesc)"
            ElseIf ReportFrm.FGetText(0) = "Item Category Wise Summary" Then
                mQry = " Select VMain.ItemCategory as SearchCode, Max(VMain.ItemCategoryDesc) As [Item Category], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory 
                    Order By Max(VMain.ItemCategoryDesc)"
            ElseIf ReportFrm.FGetText(0) = "City Wise Summary" Then
                mQry = " Select VMain.CityCode as SearchCode, Max(VMain.CityName) As [City], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CityCode 
                    Order By Max(VMain.CityName)"
            ElseIf ReportFrm.FGetText(0) = "State Wise Summary" Then
                mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.StateCode 
                    Order By Max(VMain.StateName)"
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode as SearchCode, Max(VMain.AgentName) As [Agent], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By Max(VMain.AgentName)"
            ElseIf ReportFrm.FGetText(0) = "Month Wise Summary" Then
                If AgL.PubServerName = "" Then
                    mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat)  
                    Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat)"
                Else
                    mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7)
                    Order By Max(Year(VMain.V_Date_ActualFormat)), Max(Month(VMain.V_Date_ActualFormat)) "
                End If
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")



            ReportFrm.Text = mReportDefaultText + "-" + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcJobReport"

            ReportFrm.ProcFillGrid(DsHeader)
            AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)

            If ReportFrm.DGL1.Columns.Contains(Col1Rate) Then ReportFrm.DGL1.Columns(Col1Rate).Visible = False
            If ReportFrm.DGL1.Columns.Contains(Col1AmountExDiscount) Then ReportFrm.DGL1.Columns(Col1AmountExDiscount).Visible = False
            If ReportFrm.DGL1.Columns.Contains(Col1Amount) Then ReportFrm.DGL1.Columns(Col1Amount).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        Finally
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next
        End Try
    End Sub
    Private Function FGetVoucher_TypeQry(ByVal TableName As String, Optional NCat As String = "") As String
        Dim mQry As String
        mQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
        If NCat <> "" Then
            NCat = Replace(NCat, ",", "','")
            mQry = mQry & " Where Vt.NCat In ('" & NCat & "') "
        End If
        FGetVoucher_TypeQry = mQry
    End Function
End Class
