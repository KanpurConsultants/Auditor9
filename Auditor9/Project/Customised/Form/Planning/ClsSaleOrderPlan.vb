Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Microsoft.Reporting.WinForms
Public Class ClsSaleOrderPlan

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""

    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4
    Dim StrSQLQuery As String = ""
    Private Const CnsProfitAndLoss As String = "PRLS"

    Dim mShowReportType As String = ""

    Public Const Col1Select As String = "Tick"
    Public Const Col1SaleOrder As String = "Sale Order"
    Public Const Col1SaleOrderSr As String = "Sale Order Sr"

    Public Const Col1ItemCategoryCode As String = "Item Category Code"
    Public Const Col1ItemGroupCode As String = "Item Group Code"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Dimension1Code As String = "Dimension1Code"
    Public Const Col1Dimension2Code As String = "Dimension2Code"
    Public Const Col1Dimension3Code As String = "Dimension3Code"
    Public Const Col1Dimension4Code As String = "Dimension4Code"
    Public Const Col1SizeCode As String = "Size Code"
    Public Const Col1SKUCode As String = "Sku Code"


    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1SKU As String = "Sku"

    Public Const Col1MItemCategory As String = "Main Item Category"
    Public Const Col1MItemGroup As String = "Main Item Group"
    Public Const Col1MItemSpecification As String = "Main Item Specification"
    Public Const Col1MDimension1 As String = "MDimension1"
    Public Const Col1MDimension2 As String = "MDimension2"
    Public Const Col1MDimension3 As String = "MDimension3"
    Public Const Col1MDimension4 As String = "MDimension4"
    Public Const Col1MSize As String = "Main Size"

    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1UnitMultiplier As String = "Unit Multiplier"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1DealUnit As String = "Deal Unit"
    Public Const Col1ProdPlanQty As String = "Prod Plan Qty"
    Public Const Col1PurchPlanQty As String = "Purch Plan Qty"
    Public Const Col1StockPlanQty As String = "Stock Plan Qty"
    Public Const Col1Remark As String = "Remark"

    Private Const mFormat_SaleOrderPendingForPlan As String = "SaleOrderPendingForPlan"
    Private Const mFormat_SummaryToPlan As String = "SummaryToPlan"

    Private Const mPlanType_Production As String = "Production"
    Private Const mPlanType_Purchase As String = "Purchase"
    Private Const mPlanType_Stock As String = "Stock"



    Dim mFormat As String = ""

    Dim bTempTable As String = Guid.NewGuid.ToString

    Dim mItemDataSet As DataSet
    Dim mDimension1DataSet As DataSet
    Dim mDimension2DataSet As DataSet
    Dim mDimension3DataSet As DataSet
    Dim mDimension4DataSet As DataSet
    Dim mSizeDataSet As DataSet

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
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg Where Sg.SubGroupType = '" & SubgroupType.Customer & "' "
    Dim mHelpSaleOrderQry$ = "SELECT 'o' As Tick, H.DocID, H.ManualRefNo AS SaleOrderNo FROM SaleOrder H  "
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupQry$, "All", 500, 500, 360)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("SaleOrder", "Sale Order", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSaleOrderQry)
            ReportFrm.CreateHelpGrid("Remarks", "Remarks", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")
            ReportFrm.BtnProceed.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcSaleOrderPlan()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcSaleOrderPlan(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Sale Order Plan"

            mCondStr = mCondStr & " AND H.V_Date Between '" & CDate(ReportFrm.FGetText(0)).ToString("s") & "' And '" & CDate(ReportFrm.FGetText(1)).ToString("s") & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 2)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 3), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 4), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.DocId", 5), "''", "'")

            mQry = " SELECT 'o' As Tick, VSaleOrder.SaleOrder, VSaleOrder.SaleOrderSr, VSaleOrder.Party, 
                    VSaleOrder.SaleOrderNo, VSaleOrder.SaleOrderDate, 
                    Sku.Code As SkuCode, Sku.Description As Sku, Sku.ItemCategory As ItemCategoryCode, 
                    Sku.ItemGroup As ItemGroupCode, Sku.BaseItem As ItemCode, 
                    SKU.Dimension1 As Dimension1Code, SKU.Dimension2 As Dimension2Code, 
                    Sku.Dimension3 As Dimension3Code, Sku.Dimension4 As Dimension4Code, Sku.Size As SizeCode, 
                    IC.Description as ItemCategory, IG.Description as ItemGroup,
                    I.Description as Item, D1.Description as Dimension1,D2.Description as Dimension2,
                    D3.Description as Dimension3, D4.Description as Dimension4, Size.Description as Size, 
                    IsNull(VSaleOrder.SaleOrderQty,0) - IsNull(VPurchPlanDetailBase.PlanQty,0) AS Qty,
                    VSaleOrder.Unit As Unit, VSaleOrder.UnitMultiplier As UnitMultiplier, 
                    VSaleOrder.DealQty As DealQty, VSaleOrder.DealUnit As DealUnit
                    FROM (
	                    SELECT L.SaleOrder, L.SaleOrderSr, Max(IsNull(H.SaleToPartyDocNo,H.ManualRefNo)) AS SaleOrderNo, Max(H.V_Date) AS SaleOrderDate, 
	                    Max(H.SaleToPartyName) AS Party, Max(L.Item) As Item,
	                    Sum(L.Qty) AS SaleOrderQty, Max(L.Unit) As Unit, Max(L.UnitMultiplier) As UnitMultiplier, 
                        Sum(L.DealQty) As DealQty, Max(L.DealUnit) As DealUnit
	                    FROM SaleOrder H 
                        LEFT JOIN SaleOrderDetail L On H.DocID = L.DocID
	                    WHERE L.SaleOrder Is Not NULL And L.SaleOrderSr Is Not NULL " & mCondStr &
                        " GROUP BY L.SaleOrder, L.SaleOrderSr
                    ) AS VSaleOrder
                    LEFT JOIN (
                        Select Ppdb.SaleInvoice AS SaleOrder, Ppdb.SaleInvoiceSr AS SaleOrderSr, Sum(Ppdb.Qty) As PlanQty
                        FROM PurchPlanDetailBaseSaleOrder Ppdb
                        GROUP BY Ppdb.SaleInvoice, Ppdb.SaleInvoiceSr
                    ) As VPurchPlanDetailBase ON VSaleOrder.SaleOrder = VPurchPlanDetailBase.SaleOrder 
			                    AND VSaleOrder.SaleOrderSr = VPurchPlanDetailBase.SaleOrderSr
                    LEFT JOIN Item Sku ON Sku.Code = VSaleOrder.Item
                    LEFT JOIN Item I ON I.Code = IfNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & agConstants.ItemV_Type.SKU & "'
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                    LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                    LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                    LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                    LEFT JOIN Item Size ON Size.Code = Sku.Size
                    WHERE IsNull(VSaleOrder.SaleOrderQty,0) - IsNull(VPurchPlanDetailBase.PlanQty,0) > 0
                    ORDER BY VSaleOrder.Party, VSaleOrder.SaleOrderDate, VSaleOrder.SaleOrderNo, 
                    I.Description, D1.Description, D2.Description, D3.Description, D4.Description "
            DsReport = AgL.FillData(mQry, AgL.GCn)

            If DsReport.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sale Order Plan"
            ReportFrm.ClsRep = Me
            ReportFrm.InputColumnsStr = Col1ItemCode + Col1Dimension1Code + Col1Dimension2Code + Col1Dimension3Code + Col1Dimension4Code + Col1SizeCode

            mFormat = mFormat_SaleOrderPendingForPlan

            ReportFrm.Text = "Sale Order Plan - " + mFormat

            ReportFrm.ProcFillGrid(DsReport)

            ReportFrm.DGL1.Columns(Col1SaleOrder).Visible = False
            ReportFrm.DGL1.Columns(Col1SaleOrderSr).Visible = False
            ReportFrm.DGL1.Columns(Col1SKUCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SKU).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCategoryCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemGroupCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCode).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension1Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4Code).Visible = False
            ReportFrm.DGL1.Columns(Col1SizeCode).Visible = False

            FAdjustFootedGrid()

            FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsReport = Nothing
        End Try
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        If mFormat = mFormat_SaleOrderPendingForPlan Then
            mQry = "CREATE TEMPORARY TABLE [#" & bTempTable & "] " &
                        " (SaleOrder nVarChar(21), SaleOrderSr Int, 
                            SkuCode nVarChar(10), Qty Decimal(18,4), Unit NVARCHAR (10), UnitMultiplier Decimal(18,4), 
                            DealQty Decimal(18,4), DealUnit NVARCHAR (10))"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnRead)

            For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                If ReportFrm.DGL1.Item(Col1Select, I).Value = "þ" Then
                    mQry = " INSERT INTO [#" & bTempTable & "](SaleOrder, SaleOrderSr, SkuCode,
                        Qty, Unit, UnitMultiplier, DealQty, DealUnit)
                        Select " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SaleOrder, I).Value) & " As SaleOrder, 
                        " & Val(ReportFrm.DGL1.Item(Col1SaleOrderSr, I).Value) & " As SaleOrderSr,
                        " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SKUCode, I).Value) & " As SKUCode,
                        " & Val(ReportFrm.DGL1.Item(Col1Qty, I).Value) & " As Qty, 
                        " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Unit, I).Value) & " As Unit,
                        " & Val(ReportFrm.DGL1.Item(Col1UnitMultiplier, I).Value) & " As UnitMultiplier,
                        " & Val(ReportFrm.DGL1.Item(Col1DealQty, I).Value) & " As DealQty,
                        " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DealUnit, I).Value) & " As DealUnit
                        "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnRead)
                End If
            Next

            mQry = "Select Max(Sku.Code) As SkuCode, Max(Sku.BaseItem) As ItemCode, Max(Sku.ItemCategory) As ItemCategoryCode, Max(Sku.ItemGroup) As ItemGroupCode, 
                    Max(SKU.Dimension1) As Dimension1Code, Max(SKU.Dimension2) As Dimension2Code, Max(Sku.Dimension3) As Dimension3Code, Max(Sku.Dimension4) As Dimension4Code, Max(Sku.Size) As SizeCode, 
                    Max(Sku.Description) As Sku, Max(IC.Description) as ItemCategory, 
                    Max(IG.Description) as ItemGroup, Max(I.Description) as Item, 
                    Max(D1.Description) as Dimension1, Max(D2.Description) as Dimension2,
                    Max(D3.Description) as Dimension3, Max(D4.Description) as Dimension4,
                    Max(Size.Description) as Size,                
                    Max(I.ItemCategory) as MainItemCategory, Max(I.ItemGroup) as MainItemGroup, Max(I.Specification) as MainItemSpecification, 
                    Max(I.Dimension1) as MDimension1,  Max(I.Dimension2) as MDimension2,  
                    Max(I.Dimension3) as MDimension3,  Max(I.Dimension4) as MDimension4,  Max(I.Size) as MainSize,
                    Sum(T.Qty) As Qty, Max(T.Unit) As Unit, 
                    Max(T.UnitMultiplier) As UnitMultiplier, Sum(T.DealQty) As DealQty, Max(T.DealUnit) As DealUnit, 
                    Sum(T.Qty) As ProdPlanQty, 0 As PurchPlanQty, 0 As StockPlanQty, '' As Remark
                    From [#" & bTempTable & "] T
                    LEFT JOIN SaleOrderDetail L On T.SaleOrder = L.DocId And T.SaleOrderSr = L.Sr 
                    LEFT JOIN Item Sku ON Sku.Code = L.Item 
                    LEFT JOIN Item I ON I.Code = IfNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & agConstants.ItemV_Type.SKU & "'
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                    LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                    LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                    LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                    LEFT JOIN Item Size ON Size.Code = Sku.Size
                    Group By L.Item "
            DsReport = AgL.FillData(mQry, AgL.GcnRead)

            If DsReport.Tables(0).Rows.Count = 0 Then MsgBox("No Records Selected...!", MsgBoxStyle.Information) : Exit Sub

            ReportFrm.Text = "Sale Order Plan"
            ReportFrm.ClsRep = Me
            ReportFrm.InputColumnsStr = Col1ItemCode + Col1Dimension1Code + Col1Dimension2Code + Col1Dimension3Code + Col1Dimension4Code

            mFormat = mFormat_SummaryToPlan

            ReportFrm.Text = "Sale Order Plan - " + mFormat

            ReportFrm.ProcFillGrid(DsReport)


            For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategoryCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag = ReportFrm.DGL1.Item(Col1ItemCategoryCode, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroupCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag = ReportFrm.DGL1.Item(Col1ItemGroupCode, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Item, I).Tag = ReportFrm.DGL1.Item(Col1ItemCode, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1Code, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension1, I).Tag = ReportFrm.DGL1.Item(Col1Dimension1Code, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2Code, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension2, I).Tag = ReportFrm.DGL1.Item(Col1Dimension2Code, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3Code, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension3, I).Tag = ReportFrm.DGL1.Item(Col1Dimension3Code, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4Code, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension4, I).Tag = ReportFrm.DGL1.Item(Col1Dimension4Code, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1SizeCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Size, I).Tag = ReportFrm.DGL1.Item(Col1SizeCode, I).Value
                End If
            Next


            ReportFrm.DGL1.Columns(Col1SKUCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SKU).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCategoryCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemGroupCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCode).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension1Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4Code).Visible = False
            ReportFrm.DGL1.Columns(Col1SizeCode).Visible = False

            ReportFrm.InputColumnsStr = Col1Remark

            ReportFrm.DGL1.Columns(Col1ProdPlanQty).Visible = True
            ReportFrm.DGL1.Columns(Col1PurchPlanQty).Visible = True
            ReportFrm.DGL1.Columns(Col1StockPlanQty).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark).Visible = True
            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next
            ReportFrm.DGL1.Columns(Col1Remark).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1ProdPlanQty).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1PurchPlanQty).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1StockPlanQty).ReadOnly = False

            FAdjustFootedGrid()

            FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1)
        ElseIf mFormat = mFormat_SummaryToPlan Then

            FDataValidation()

            Try
                Dim mTrans As String = ""
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                Dim bProdPlanQty As Double = 0
                Dim bPurchPlanQty As Double = 0
                Dim bStockPlanQty As Double = 0
                For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                    bProdPlanQty += Val(ReportFrm.DGL1.Item(Col1ProdPlanQty, I).Value)
                    bPurchPlanQty += Val(ReportFrm.DGL1.Item(Col1PurchPlanQty, I).Value)
                    bStockPlanQty += Val(ReportFrm.DGL1.Item(Col1StockPlanQty, I).Value)
                Next

                If bProdPlanQty > 0 Then FSave(AgL.GCn, AgL.ECmd, mPlanType_Production)
                If bPurchPlanQty > 0 Then FSave(AgL.GCn, AgL.ECmd, mPlanType_Purchase)
                If bStockPlanQty > 0 Then FSave(AgL.GCn, AgL.ECmd, mPlanType_Stock)

                AgL.ETrans.Commit()
                mTrans = "Commit"
                MsgBox("Process Completed...!", MsgBoxStyle.Information)
                ReportFrm.DGL1.DataSource = Nothing
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Public Sub FSave(Conn As Object, Cmd As Object, PlanType As String)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0

        Dim I As Integer = 0, J As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim mDescription As String = ""
        Dim mPurchPlanDocId As String = ""
        Dim mV_Type As String = Ncat.ProcessPlan
        Dim mV_No As String
        Dim mV_Prefix As String
        Dim mV_Date As String
        Dim mSr As Integer = 0
        Dim mManualRefNo As String = ""
        Dim mRemarks As String = ""
        Dim bQtyColumnName As String = ""
        Dim bProcess As String = ""

        mV_Date = AgL.PubLoginDate.ToString
        mPurchPlanDocId = AgL.GetDocId(mV_Type, CStr(0), CDate(mV_Date), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
        mV_No = Val(AgL.DeCodeDocID(mPurchPlanDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
        mV_Prefix = AgL.DeCodeDocID(mPurchPlanDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

        If PlanType = mPlanType_Production Then
            bQtyColumnName = Col1ProdPlanQty
            bProcess = "PProduct"
            mManualRefNo = ReportFrm.FGetText(5).ToString() + "-M"
        ElseIf PlanType = mPlanType_Purchase Then
            bQtyColumnName = Col1PurchPlanQty
            bProcess = "PPurchase"
            mManualRefNo = ReportFrm.FGetText(5).ToString() + "-P"
        ElseIf PlanType = mPlanType_Stock Then
            bQtyColumnName = Col1StockPlanQty
            bProcess = "PStock"
            mManualRefNo = ReportFrm.FGetText(5).ToString() + "-S"
        End If

        mRemarks = AgL.XNull(ReportFrm.FGetText(6)).ToString()


        mQry = "INSERT INTO PurchPlan (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, 
                    ManualRefNo, Remarks, EntryBy, EntryDate)
                    Select " & AgL.Chk_Text(mPurchPlanDocId) & " As Docid, " & AgL.Chk_Text(mV_Type) & " As V_Type, 
                    " & AgL.Chk_Text(mV_Prefix) & " As v_prefix, " & AgL.Chk_Text(mV_Date) & " As v_date, 
                    " & Val(mV_No) & " As V_No, " & AgL.Chk_Text(AgL.PubDivCode) & " As div_code, 
                    " & AgL.Chk_Text(AgL.PubSiteCode) & " As Site_Code, " & AgL.Chk_Text(mManualRefNo) & "  As ManualRefNo, 
                    " & AgL.Chk_Text(mRemarks) & "  As Remarks,  " & AgL.Chk_Text(AgL.PubUserName) & "  As entryby, 
                    " & AgL.Chk_Text(AgL.PubLoginDate) & " As EntryDate "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To ReportFrm.DGL1.RowCount - 1
            If AgL.XNull(ReportFrm.DGL1.Item(Col1SKU, I).Value) <> "" Then
                If Val(ReportFrm.DGL1.Item(bQtyColumnName, I).Value) > 0 Then
                    mSr += 1
                    mQry = "Insert Into PurchPlanDetail(DocId, Sr, Process, Item, 
                           Qty, Unit, UnitMultiplier, DealUnit, DealQty, Remark) "
                    mQry += " Select " & AgL.Chk_Text(mPurchPlanDocId) & ", " & mSr & ", " &
                        " " & AgL.Chk_Text(bProcess) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SKU, I).Tag) & ", " &
                        " " & Val(ReportFrm.DGL1.Item(bQtyColumnName, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Unit, I).Value) & ", " &
                        " " & Val(ReportFrm.DGL1.Item(Col1UnitMultiplier, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DealUnit, I).Value) & ", " &
                        " " & Val(ReportFrm.DGL1.Item(Col1DealQty, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Remark, I).Value) & " "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = " Select L.Docid, L.Sr As TSr, T.SaleOrder, T.SaleOrderSr, T.Qty As SaleOrderQty,
                            L.Qty * T.Qty / (
                                Select Sum(T.Qty) As Qty
                                From [#" & bTempTable & "] T
                                Where IsNull(T.SkuCode,'') = IsNull(L.Item,'') 
                            ) As Qty
                            From PurchPlanDetail L With (NoLock)
                            LEFT JOIN [#" & bTempTable & "] T On IsNull(L.Item,'') = IsNull(T.SkuCode,'') 
                            Where L.DocId = " & AgL.Chk_Text(mPurchPlanDocId) & " And Sr = " & mSr & ""
                    Dim bPurchPlanDetailBase As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

                    Dim bPurchPlanDetailBaseSr As Integer = 0
                    For J = 0 To bPurchPlanDetailBase.Rows.Count - 1
                        bPurchPlanDetailBaseSr += 1
                        mQry = " INSERT INTO PurchPlanDetailBase (DocID, TSr, Sr, Qty)
                            Select " & AgL.Chk_Text(bPurchPlanDetailBase.Rows(J)("DocId")) & " As DocID, 
                            " & Val(bPurchPlanDetailBase.Rows(J)("TSr")) & " As TSr, 
                            " & Val(bPurchPlanDetailBaseSr) & " As Sr, 
                            " & Val(bPurchPlanDetailBase.Rows(J)("Qty")) & " As Qty "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "INSERT INTO PurchPlanDetailBaseSaleOrder (GenDocID, GenSr, 
                                PurchPlanDetailBase, PurchPlanDetailBaseTSr, PurchPlanDetailBaseSr, 
                                SaleInvoice, SaleInvoiceSr, Qty)
                                Select " & AgL.Chk_Text(mPurchPlanDocId) & " As GenDocID, 
                                " & Val(mSr) & " As GenSr, 
                                " & AgL.Chk_Text(bPurchPlanDetailBase.Rows(J)("DocId")) & " As PurchPlanDetailBase, 
                                " & Val(bPurchPlanDetailBase.Rows(J)("TSr")) & " As PurchPlanDetailBaseTSr, 
                                " & Val(bPurchPlanDetailBaseSr) & " As PurchPlanDetailBaseSr, 
                                " & AgL.Chk_Text(bPurchPlanDetailBase.Rows(J)("SaleOrder")) & " As SaleInvoice, 
                                " & Val(bPurchPlanDetailBase.Rows(J)("SaleOrderSr")) & " As SaleInvoiceSr, 
                                " & Val(bPurchPlanDetailBase.Rows(J)("SaleOrderQty")) & " As Qty "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Next
                End If
            End If
        Next
        AgL.UpdateVoucherCounter(mPurchPlanDocId, CDate(mV_Date), Conn, Cmd, AgL.PubDivCode, AgL.PubSiteCode)
    End Sub
    Private Sub FAdjustFootedGrid()
        For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
            ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
        Next
    End Sub
    Private Function FDataValidation() As Boolean
        FDataValidation = False

        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategory, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroup, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Size, I).Value) <> "" _
                   Then
                ReportFrm.DGL1.Item(Col1SKU, I).Tag = ClsMain.FGetSKUCode(I + 1, ItemTypeCode.InternalProduct _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategoryCode, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroup, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Size, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Size, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MItemCategory, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MItemGroup, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MItemSpecification, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MDimension1, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MDimension2, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MDimension3, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MDimension4, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MSize, I).Value)
                               )
                If ReportFrm.DGL1.Item(Col1SKU, I).Tag = "" Then
                    MsgBox("Item Combination is not allowed...!", MsgBoxStyle.Information)
                    FDataValidation = False
                    Exit Function
                End If
            End If
        Next

        FDataValidation = True
    End Function
End Class
