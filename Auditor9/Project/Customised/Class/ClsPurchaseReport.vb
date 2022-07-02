Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsPurchaseReport

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""
    Dim SubRecordType As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4

    Public Const Col1Rate As String = "Rate"
    Public Const Col1AmountExDiscount As String = "Amount Ex Discount"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1Process As String = "Process"
    Public Const Col1SearchCode As String = "Search Code"
    Public Const Col1Tags As String = "Tags"

    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Dim rowReportType As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowProcess As Integer = 3
    Dim rowParty As Integer = 4
    Dim rowSite As Integer = 5
    Dim rowVoucherType As Integer = 6
    Dim rowCashCredit As Integer = 7
    Dim rowAgent As Integer = 8
    Dim rowItemType As Integer = 9
    Dim rowItemCategory As Integer = 10
    Dim rowItemGroup As Integer = 11
    Dim rowItem As Integer = 12
    Dim rowDimension1 As Integer = 13
    Dim rowDimension2 As Integer = 14
    Dim rowDimension3 As Integer = 15
    Dim rowDimension4 As Integer = 16
    Dim rowSize As Integer = 17
    Dim rowCity As Integer = 18
    Dim rowState As Integer = 19
    Dim rowHSN As Integer = 20
    Dim rowTags As Integer = 21
    Dim rowSettlementStatus As Integer = 22
    Dim rowCustomer As Integer = 23
    Dim rowOrderBy As Integer = 24
    Dim rowPartyTaxGroup As Integer = 25
    Dim rowItemTaxGroup As Integer = 26
    Dim rowPartyTags As Integer = 27

    'Dim rowTransporter As Integer = 22
    'Dim rowLRNo As Integer = 23
    'Dim rowLRDate As Integer = 24
    'Dim rowNoOfBales As Integer = 25
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

    Public Shared mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Public Shared mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Public Shared mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Public Shared mHelpProcessQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.Name AS Process FROM SubGroup Sg Where Sg.SubGroupType = '" & SubgroupType.Process & "' And IfNull(Sg.Status,'Active') = 'Active' "
    Public Shared mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Public Shared mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Public Shared mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Public Shared mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Public Shared mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Public Shared mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Public Shared mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Public Shared mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Public Shared mHelpTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM PurchInvoice H "
    Public Shared mHelpDimension1Qry$ = "Select 'o' As Tick, Code, Description From Dimension1 "
    Public Shared mHelpDimension2Qry$ = "Select 'o' As Tick, Code, Description From Dimension2 "
    Public Shared mHelpDimension3Qry$ = "Select 'o' As Tick, Code, Description From Dimension3 "
    Public Shared mHelpDimension4Qry$ = "Select 'o' As Tick, Code, Description From Dimension4 "
    Public Shared mHelpSizeQry$ = "Select 'o' As Tick, Code, Description From Size "
    Public Shared mHelpCustomerQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Cash') "
    Public Shared mHelpPartyTaxGroup$ = "SELECT 'o' As Tick, H.Description AS Code, H.Description FROM PostingGroupSalesTaxParty H  "
    Public Shared mHelpItemTaxGroup$ = "SELECT 'o' As Tick, H.Description AS Code, H.Description FROM PostingGroupSalesTaxItem H  "
    Public Shared mHelpPartyTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM SubGroup H "

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
                            Union All Select 'Party Tax Group Wise Summary' as Code, 'Party Tax Group Wise Summary' as Name
                            Union All Select 'Item Tax Group Wise Summary' as Code, 'Item Tax Group Wise Summary' as Name
                            Union All Select 'City Wise Summary' as Code, 'City Wise Summary' as Name 
                            Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name                             
                            "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Month Wise Summary")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Process", "Process", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpProcessQry)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry,, 600, 650, 300)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
            ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice", EntryNCat))
            ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")

            If EntryNCat = Ncat.StockIssue Or EntryNCat = Ncat.StockReceive Then
                ReportFrm.FilterGrid.Rows(rowCashCredit).Visible = False
            End If

            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPurchaseAgentQry)
            ReportFrm.CreateHelpGrid("Item Type", "Item Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTypeQry)
            ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
            ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
            ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
            ReportFrm.CreateHelpGrid("Dimension1", "Dimension1", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension1Qry)
            ReportFrm.CreateHelpGrid("Dimension2", "Dimension2", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension2Qry)
            ReportFrm.CreateHelpGrid("Dimension3", "Dimension3", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension3Qry)
            ReportFrm.CreateHelpGrid("Dimension4", "Dimension4", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension4Qry)
            ReportFrm.CreateHelpGrid("Size", "Size", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSizeQry)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
            ReportFrm.CreateHelpGrid("HSN", "HSN", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
            ReportFrm.FilterGrid.Rows(rowHSN).Visible = False 'Hide HSN Row
            ReportFrm.CreateHelpGrid("Tags", "Tags", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTagQry)
            mQry = "Select 'Settled' as Code, 'Settled' as Name 
                            Union All Select 'Pending' as Code, 'Pending' as Name 
                            Union All Select 'All' as Code, 'All' as Name 
                            "
            ReportFrm.CreateHelpGrid("Settlement Status", "Settlement Status", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "All")
            ReportFrm.CreateHelpGrid("Customer", "Customer", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCustomerQry,, 600, 650, 300)

            mQry = "Select 'Doc Date' as Code, 'Doc Date' as Name 
                    Union All 
                    Select 'Party Doc Date' as Code, 'Party Doc Date' as Name "
            ReportFrm.CreateHelpGrid("Order By", "Order By", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Doc Date")

            ReportFrm.CreateHelpGrid("Party Tax Group", "Party Tax Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyTaxGroup)
            ReportFrm.CreateHelpGrid("Item Tax Group", "Item Tax Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTaxGroup)
            ReportFrm.CreateHelpGrid("PartyTags", "Party Tags", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyTagQry)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcPurchaseReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, ByVal strNCat As String, ByVal strSubRecordType As String)
        ReportFrm = mReportFrm
        EntryNCat = strNCat
        SubRecordType = strSubRecordType
    End Sub
    Public Sub ProcPurchaseReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim mPartyTags As String() = Nothing
            Dim J As Integer



            If mReportDefaultText = "" Then
                mReportDefaultText = ReportFrm.Text
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Month Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, rowFromDate).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, rowToDate).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Item Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, rowItem).Value = mGridRow.Cells("Item").Value
                        mFilterGrid.Item(GFilterCode, rowItem).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, rowParty).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, rowParty).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, rowAgent).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, rowAgent).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Item Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, rowItemGroup).Value = mGridRow.Cells("Item Group").Value
                        mFilterGrid.Item(GFilterCode, rowItemGroup).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Item Category Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, rowItemCategory).Value = mGridRow.Cells("Item Category").Value
                        mFilterGrid.Item(GFilterCode, rowItemCategory).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "City Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, rowCity).Value = mGridRow.Cells("City").Value
                        mFilterGrid.Item(GFilterCode, rowCity).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "State Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, rowState).Value = mGridRow.Cells("State").Value
                        mFilterGrid.Item(GFilterCode, rowState).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "HSN Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, rowHSN).Value = mGridRow.Cells("HSN").Value
                        mFilterGrid.Item(GFilterCode, rowHSN).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Party Tax Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, rowPartyTaxGroup).Value = mGridRow.Cells("Party Tax Group").Value
                        mFilterGrid.Item(GFilterCode, rowPartyTaxGroup).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Tax Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, rowItemTaxGroup).Value = mGridRow.Cells("Item Tax Group").Value
                        mFilterGrid.Item(GFilterCode, rowItemTaxGroup).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail" Or
                                mFilterGrid.Item(GFilter, rowReportType).Value = "Item Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If



            If SubRecordType <> "" Then
                mCondStr = " Where (VT.NCat In ('" & Replace(EntryNCat, ",", "','") & "') "
                mCondStr = mCondStr & " Or L.SubRecordType In ('" & Replace(SubRecordType, ",", "','") & "')) "
            Else
                mCondStr = " Where VT.NCat In ('" & Replace(EntryNCat, ",", "','") & "') "
                mCondStr = mCondStr & " AND L.SubRecordType Is Null "
            End If
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", rowProcess)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.BillToParty", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", rowSite)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", rowVoucherType)
            If ReportFrm.FGetText(rowCashCredit) = "Cash" Then
                mCondStr = mCondStr & " AND Sg.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(rowCashCredit) = "Credit" Then
                mCondStr = mCondStr & " AND Sg.Nature <> 'Cash'"
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemType", rowItemType)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemCategory", rowItemCategory)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemGroup", rowItemGroup)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", rowItem)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension1", rowDimension1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension2", rowDimension2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension3", rowDimension3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension4", rowDimension4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Size", rowSize)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", rowCity)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", rowState)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SalesTaxGroupParty", rowPartyTaxGroup)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesTaxGroupItem", rowItemTaxGroup)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SI.SaleToParty", rowCustomer)
            If ReportFrm.FGetText(rowHSN) <> "All" Then
                mCondStr = mCondStr & " And I.HSN = " & AgL.Chk_Text(ReportFrm.FGetText(rowHSN)) & " "
            End If

            If ReportFrm.FGetText(rowTags) <> "All" Then
                mTags = ReportFrm.FGetText(rowTags).ToString.Split(",")
                For J = 0 To mTags.Length - 1
                    mCondStr += " And CharIndex('+' || '" & mTags(J) & "',H.Tags) > 0 "
                Next
            End If

            If ReportFrm.FGetText(rowPartyTags) <> "All" Then
                mPartyTags = ReportFrm.FGetText(rowPartyTags).ToString.Split(",")
                For J = 0 To mPartyTags.Length - 1
                    mCondStr += " And CharIndex('" & mPartyTags(J) & "',Party.Tags) > 0 "
                Next
            End If

            If ReportFrm.FGetText(rowSettlementStatus) = "Settled" Then
                mCondStr = mCondStr & " And H.DocID In (SELECT H.PurchaseInvoiceDocId FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
            ElseIf ReportFrm.FGetText(rowSettlementStatus) = "Pending" Then
                mCondStr = mCondStr & " And H.DocID Not In (SELECT H.PurchaseInvoiceDocId FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
            End If


            mQry = " SELECT H.DocID, L.Sr, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.ManualRefNo As InvoiceNo,
                    Prs.Name As Process, H.Vendor, Sku.ItemGroup, Sku.ItemCategory,
                    Sku.BaseItem, Sku.Dimension1, Sku.Dimension2, Sku.Dimension3, Sku.Dimension4, Sku.Size,
                    Party.Name As VendorName, H.VendorSalesTaxNo as PartyGstNo,
                    Agent.Code As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.VendorDocNo as PartyInvoiceNo, 
                    strftime('%d/%m/%Y', H.VendorDocDate) As PartyInvoiceDate, 
                    H.VendorDocDate As PartyInvoiceDate_ActualFormat,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as ManualRefNo, 
                    H.SalesTaxGroupParty, L.SalesTaxGroupItem,
                    L.Item, I.Specification As ItemSpecification, IfNull(I.HSN,IC.HSN) As HSN, 
                    IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, B.Description AS BarcodeDesc,
                    Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else Sku.Specification End as ItemDesc,
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer  as nVarchar) End) as DiscountPer, 
                    L.DiscountAmount + L.AdditionalDiscountAmount as Discount, 
                    L.Taxable_Amount, L.Net_Amount, L.Commission, L.AdditionalCommission, L.Commission + L.AdditionalCommission as TotalCommission, L.Qty, L.Unit, 
                    L.DealQty, L.DealUnit,
                    L.Rate, L.Amount +(L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax, H.Tags,
                    (select Max(Tags) From SaleInvoice Where DocId In (Select SaleInvoice From SaleInvoiceDetail Where DocId=SI.DocID)) as OrderTags,
                    Transporter.Name as TransporterName, HT.LRNo, strftime('%d/%m/%Y', HT.LRDate) as LRDate, HT.NoOfBales, Cust.Name as CustomerName
                    FROM PurchInvoice H 
                    Left Join PurchInvoiceDetail L On H.DocID = L.DocID 
                    Left Join Barcode B On B.GenDocID = L.DocID AND B.GenSr = L.Sr
                    Left Join PurchInvoiceTransport HT On H.DocId = HT.DocID
                    Left Join viewHelpSubgroup Transporter On HT.Transporter = Transporter.Code
                    LEFT JOIN SubGroup Prs On H.Process = Prs.SubCode
                    LEFT JOIN Item Sku ON Sku.Code = L.Item
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    Left Join Item IC On IfNull(Sku.ItemCategory, Sku.Code) = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item I ON IfNull(Sku.BaseItem,Sku.Code) = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    Left Join viewHelpSubgroup Party On H.Vendor = Party.Code                     
                    Left Join viewHelpSubgroup Sg On H.BillToParty = Sg.Code                     
                    Left Join (Select SILTV.Subcode,SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code ) as LTV On Party.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On IfNull(H.VendorCity,Party.CityCode) = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                    Left Join SaleInvoice SI On H.GenDocID = SI.DocId 
                    Left Join viewHelpSubgroup Cust On SI.SaleToParty = Cust.Code " & mCondStr

            Dim bOrderByDate As String = ""
            If ReportFrm.FGetText(rowOrderBy) = "Party Doc Date" Then
                bOrderByDate = "Order By Max(VMain.PartyInvoiceDate_ActualFormat)"
            Else
                bOrderByDate = "Order By Max(VMain.V_Date_ActualFormat)"
            End If


            If ReportFrm.FGetText(rowReportType) = "Doc.Header Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As DocDate, Max(VMain.InvoiceNo) As DocNo, 
                    Max(VMain.PartyInvoiceNo) As PartyDocNo, Max(Vmain.PartyInvoiceDate) as PartyDocDate,
                    Max(VMain.Process) As Process, 
                    Max(VMain.VendorName) As Party, Max(Vmain.PartyGstNo) as PartyGstNo, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount,
                    IfNull(Sum(VMain.Net_Amount),0) As NetAmount,IfNull(Sum(VMain.TotalCommission),0) As TotalCommission, 
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt, 
                    Max(VMain.Tags) as Tags, Max(VMain.OrderTags) as OrderTags,
                    Max(VMain.TransporterName) as TransporterName, Max(VMain.LRNo) as LrNo, Max(VMain.LRDate) as LrDate,
                    Max(VMain.NoOfBales) as NoOfBales, Max(VMain.CustomerName) as CustomerName
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId " & bOrderByDate
            ElseIf ReportFrm.FGetText(rowReportType) = "Item Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Doc Date], Max(VMain.InvoiceNo) As [Doc No], 
                    Max(Vmain.PartyInvoiceNo) as PartyDocNo,Max(Vmain.PartyInvoiceDate) as PartyDocDate,
                    Max(VMain.Process) As Process, 
                    Max(VMain.VendorName) As Party, Max(Vmain.PartyGstNo) as PartyGstNo, 
                    Max(VMain.ItemCategoryDesc) As ItemCategory, 
                    Max(VMain.ItemGroupDesc) As ItemGroup, 
                    Max(VMain.ItemDesc) As Item, 
                    Max(VMain.Dimension1Desc) As Dimension1, 
                    Max(VMain.Dimension2Desc) As Dimension2, 
                    Max(VMain.Dimension3Desc) As Dimension3, 
                    Max(VMain.Dimension4Desc) As Dimension4, 
                    Max(VMain.BarcodeDesc) As Barcode, 
                    Max(VMain.SizeDesc) As Size, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As AmountExDiscount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.Amount) as Amount,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount],
                    Sum(VMain.TotalCommission) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) > 0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    , Max(VMain.CustomerName) as CustomerName
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr " &
                    bOrderByDate & ", Max(VMain.PartyInvoiceNo), Vmain.Sr "
            ElseIf ReportFrm.FGetText(rowReportType) = "Party Wise Summary" Then
                mQry = " Select VMain.Vendor as SearchCode, Max(VMain.VendorName) As Party, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Vendor 
                    Order By Max(VMain.VendorName)"
            ElseIf ReportFrm.FGetText(rowReportType) = "HSN Wise Summary" Then
                mQry = " Select VMain.HSN As SearchCode, VMain.HSN, Max(VMain.ItemCategoryDesc) As [Description], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.Tax1),0) As IGST, IfNull(Sum(VMain.Tax2),0) As CGST, IfNull(Sum(VMain.Tax3),0) As SGST, IfNull(Sum(VMain.Tax4),0) As Cess, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.HSN 
                    Order By VMain.HSN, Max(VMain.ItemCategoryDesc)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Item Wise Summary" Then
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
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory, VMain.ItemGroup, VMain.Item, 
                    VMain.Dimension1, VMain.Dimension2, VMain.Dimension3, VMain.Dimension4, VMain.Size
                    Order By Max(VMain.ItemDesc)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Item Group Wise Summary" Then
                mQry = " Select VMain.ItemGroup as SearchCode, Max(VMain.ItemGroupDesc) As [Item Group], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemGroup 
                    Order By Max(VMain.ItemGroupDesc)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Item Category Wise Summary" Then
                mQry = " Select VMain.ItemCategory as SearchCode, Max(VMain.ItemCategoryDesc) As [Item Category], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory 
                    Order By Max(VMain.ItemCategoryDesc)"
            ElseIf ReportFrm.FGetText(0) = "Party Tax Group Wise Summary" Then
                mQry = " Select VMain.SalesTaxGroupParty as SearchCode, Max(VMain.SalesTaxGroupParty) As [Party Tax Group], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesTaxGroupParty 
                    Order By Max(VMain.SalesTaxGroupParty)"
            ElseIf ReportFrm.FGetText(0) = "Item Tax Group Wise Summary" Then
                mQry = " Select VMain.SalesTaxGroupItem as SearchCode, Max(VMain.SalesTaxGroupItem) As [Item Tax Group], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesTaxGroupItem
                    Order By Max(VMain.SalesTaxGroupItem)"
            ElseIf ReportFrm.FGetText(rowReportType) = "City Wise Summary" Then
                mQry = " Select VMain.CityCode as SearchCode, Max(VMain.CityName) As [City], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CityCode 
                    Order By Max(VMain.CityName)"
            ElseIf ReportFrm.FGetText(rowReportType) = "State Wise Summary" Then
                mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.StateCode 
                    Order By Max(VMain.StateName)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode as SearchCode, Max(VMain.AgentName) As [Agent], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By Max(VMain.AgentName)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Month Wise Summary" Then
                If AgL.PubServerName = "" Then
                    mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], 
                    Round(Sum(VMain.Qty),3) As Qty, Max(VMain.Unit) As Unit, 
                    Round(Sum(VMain.DealQty),3) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,Sum(VMain.Net_Amount) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat)  
                    Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat)"
                Else
                    mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
                    Round(Sum(VMain.Qty),3) As Qty, Max(VMain.Unit) As Unit, 
                    Round(Sum(VMain.DealQty),3) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount], IfNull(Sum(VMain.TotalCommission),0) As [Total Commission],
                    (Case When IfNull(Sum(VMain.TotalCommission),0) >0 Then IfNull(Sum(VMain.Net_Amount),0)-IfNull(Sum(VMain.TotalCommission),0) Else 0 End) as NetPurchaseAmt
                    From (" & mQry & ") As VMain
                    GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7)
                    Order By Max(Year(VMain.V_Date_ActualFormat)), Max(Month(VMain.V_Date_ActualFormat)) "
                End If
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")



            ReportFrm.Text = mReportDefaultText + "-" + ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPurchaseReport"
            ReportFrm.InputColumnsStr = Col1Tags


            ReportFrm.ProcFillGrid(DsHeader)
            AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)

            If EntryNCat = Ncat.PurchaseInvoice Then
                If ReportFrm.DGL1.Columns.Contains(Col1Process) Then ReportFrm.DGL1.Columns(Col1Process).Visible = False
            End If

            If ReportFrm.DGL1.Columns.Contains(Col1Tags) Then ReportFrm.DGL1.Columns(Col1Tags).Visible = True
            'If ReportFrm.DGL1.Columns.Contains(Col1Rate) Then ReportFrm.DGL1.Columns(Col1Rate).Visible = False
            'If ReportFrm.DGL1.Columns.Contains(Col1Amount) Then ReportFrm.DGL1.Columns(Col1Amount).Visible = False
            If ReportFrm.DGL1.Columns.Contains(Col1AmountExDiscount) Then ReportFrm.DGL1.Columns(Col1AmountExDiscount).Visible = False
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


    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Dim dsTemp As DataSet
        Try

            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1Tags
                    'mQry = " 
                    '        Select 'GSTR2' as Code, '+GSTR2' as Description 
                    '        Union All
                    '        Select '' as Code, '' as Description 
                    '       "
                    mQry = " Select Code, '+' || Description As Description From Tag
                                Union All
                                Select '' as Code, '' as Description "
                    dsTemp = AgL.FillData(mQry, AgL.GCn)
                    FSingleSelectForm(Col1Tags, bRowIndex, dsTemp)

                    mQry = "Update PurchInvoice Set Tags = " & AgL.Chk_Text(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value) & " Where DocID = '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FSingleSelectForm(bColumnName As String, bRowIndex As Integer, bDataSet As DataSet)
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(CType(bDataSet, DataSet).Tables(0)), "", 500, 500, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Description", 400, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Tag = FRH_Single.DRReturn("Code")
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Value = FRH_Single.DRReturn("Description")
        End If
    End Sub

End Class
