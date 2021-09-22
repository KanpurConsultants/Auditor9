Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Microsoft.Reporting.WinForms
Public Class ClsStockIssue

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

    Dim mShowReportType As String = ""
    Dim mObjFrm As Object

    Public Const Col1Select As String = "Tick"
    Public Const Col1PurchOrder As String = "Purch Order"
    Public Const Col1PurchOrderSr As String = "Purch Order Sr"

    Public Const Col1OrderNo As String = "Order No"
    Public Const Col1PartyCode As String = "Party Code"
    Public Const Col1ItemTypeCode As String = "Item Type Code"
    Public Const Col1ItemCategoryCode As String = "Item Category Code"
    Public Const Col1ItemGroupCode As String = "Item Group Code"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Dimension1Code As String = "Dimension1Code"
    Public Const Col1Dimension2Code As String = "Dimension2Code"
    Public Const Col1Dimension3Code As String = "Dimension3Code"
    Public Const Col1Dimension4Code As String = "Dimension4Code"
    Public Const Col1SizeCode As String = "Size Code"
    Public Const Col1SKUCode As String = "Sku Code"

    Public Const Col1Party As String = "Party"
    Public Col1ItemType As String = AgL.PubCaptionItemType
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
    Public Const Col1Remark As String = "Remark"

    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1
    Dim rowParty As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4
    Dim rowOrderNo As Integer = 5
    Dim rowEntryNo As Integer = 6
    Dim rowEntryDate As Integer = 7
    Dim rowGodown As Integer = 8
    Dim rowRemarks As Integer = 9


    Private Const mFormat_ProcessOrderPendingForStockIssue As String = "ProcessOrderPendingForStockIssue"
    Private Const mFormat_SummaryToStockIssue As String = "SummaryToStockIssue"

    Dim mFormat As String = ""
    Dim mV_Type As String = ""

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
    Public Property V_Type() As String
        Get
            V_Type = mV_Type
        End Get
        Set(ByVal value As String)
            mV_Type = value
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
    Public Property ObjFrm() As Object
        Get
            ObjFrm = mObjFrm
        End Get
        Set(ByVal value As Object)
            mObjFrm = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg Where Sg.SubGroupType = '" & SubgroupType.Customer & "' "
    Dim mHelpGodwnQry$ = "Select Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg Where Sg.SubGroupType = '" & SubgroupType.Godown & "' "
    Dim mHelpPurchOrderQry$ = "SELECT 'o' As Tick, H.DocID, H.ManualRefNo AS OrderNo 
                            FROM PurchOrder H  
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                            Where Vt.NCat = '" & ClsCarpet.NCat_WeavingOrder & "'"
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupQry$, "All", 500, 500, 360)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("OrderNo", "Order No", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPurchOrderQry)
            Dim bManualRefNo As String = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", mV_Type, AgL.PubLoginDate, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
            ReportFrm.CreateHelpGrid("EntryNo", "Entry No", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", bManualRefNo)
            ReportFrm.CreateHelpGrid("EntryDate", "Entry Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Godown", "Godown", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpGodwnQry, "")
            ReportFrm.CreateHelpGrid("Remarks", "Remarks", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")
            ReportFrm.BtnProceed.Visible = True

            Dim DtGodown As DataTable = AgL.FillData(mHelpGodwnQry, AgL.GCn).Tables(0)

            If DtGodown.Rows.Count = 1 Then
                ReportFrm.FilterGrid.Item(GFilterCode, rowGodown).Value = AgL.XNull(DtGodown.Rows(0)("Code"))
                ReportFrm.FilterGrid.Item(GFilter, rowGodown).Value = AgL.XNull(DtGodown.Rows(0)("Name"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcStockIssue()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcStockIssue(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Stock Issue"

            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", rowParty)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.DocId", rowOrderNo), "''", "'")

            mQry = "SELECT 'o' As Tick, VPurchOrder.PurchOrder, VPurchOrder.PurchOrderSr, 
                    VPurchOrder.PurchOrderNo As OrderNo, VPurchOrder.PurchOrderDate As OrderDate, 
                    H.Vendor As PartyCode, Sg.Name As Party,
                    Sku.Code As SkuCode, Sku.Description As Sku, It.Code As ItemTypeCode, Sku.ItemCategory As ItemCategoryCode, 
                    Sku.ItemGroup As ItemGroupCode, Sku.BaseItem As ItemCode, 
                    SKU.Dimension1 As Dimension1Code, SKU.Dimension2 As Dimension2Code, 
                    Sku.Dimension3 As Dimension3Code, Sku.Dimension4 As Dimension4Code, Sku.Size As SizeCode, 
                    It.Name As ItemType, IC.Description as ItemCategory, IG.Description as ItemGroup,
                    I.Description as Item, D1.Description as Dimension1,D2.Description as Dimension2,
                    D3.Description as Dimension3, D4.Description as Dimension4, Size.Description as Size, 
                    IsNull(VPurchOrder.PurchOrderQty,0) - IsNull(VPurchOrderDetailBase.OrderQty,0) AS Qty,
                    VPurchOrder.Unit As Unit, VPurchOrder.UnitMultiplier As UnitMultiplier, 
                    VPurchOrder.DealQty As DealQty, VPurchOrder.DealUnit As DealUnit
                    FROM (
                        SELECT L.PurchOrder, L.PurchOrderSr, Max(H.ManualRefNo) AS PurchOrderNo, Max(H.V_Date) AS PurchOrderDate, 
                        Max(L.Item) As Item,
                        Sum(L.Qty) AS PurchOrderQty, Max(L.Unit) As Unit, Max(L.UnitMultiplier) As UnitMultiplier, 
                        Round(Sum(L.Qty * S.Area),3) As DealQty, Max(S.Unit) As DealUnit
                        FROM PurchOrder H 
                        LEFT JOIN PurchOrderDetail L On H.DocID = L.DocID
                        LEFT JOIN Item I ON L.Item = I.Code
                        LEFT JOIN Size S ON I.Size = S.Code
                        WHERE L.PurchOrder Is Not NULL And L.PurchOrderSr Is Not NULL " & mCondStr &
                        " GROUP BY L.PurchOrder, L.PurchOrderSr
                    ) AS VPurchOrder
                    LEFT JOIN (
                        Select IsNull(Ppdb.ReferenceDocId ,'') AS ReferenceDocId, 
                        IsNull(Ppdb.ReferenceDocIdTSr,0) AS ReferenceDocIdTSr, Sum(Ppdb.BaseQty) As OrderQty
                        FROM StockHeadDetailBase Ppdb
                        WHERE Ppdb.ReferenceDocId IS NOT NULL AND Ppdb.ReferenceDocIdTSr IS NOT NULL
                        GROUP BY Ppdb.ReferenceDocId, Ppdb.ReferenceDocIdTSr
                    ) As VPurchOrderDetailBase ON VPurchOrder.PurchOrder = VPurchOrderDetailBase.ReferenceDocId 
                                AND VPurchOrder.PurchOrderSr = VPurchOrderDetailBase.ReferenceDocIdTSr
                    LEFT JOIN Item Sku ON Sku.Code = VPurchOrder.Item
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                    LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                    LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                    LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                    LEFT JOIN Item Size ON Size.Code = Sku.Size
                    LEFT JOIN PurchOrder H On VPurchOrder.PurchOrder = H.DocId
                    LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    WHERE Vt.NCat = '" & ClsCarpet.NCat_WeavingOrder & "'
                    And IsNull(VPurchOrder.PurchOrderQty,0) - IsNull(VPurchOrderDetailBase.OrderQty,0) > 0
                    ORDER BY VPurchOrder.PurchOrderDate, VPurchOrder.PurchOrderNo, 
                    I.Description, D1.Description, D2.Description, D3.Description, D4.Description "
            DsReport = AgL.FillData(mQry, AgL.GCn)

            If DsReport.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Stock Issue"
            ReportFrm.ClsRep = Me
            ReportFrm.InputColumnsStr = "|" + Col1ItemCode + "|" + "|" + Col1Dimension1Code + "|" + "|" + Col1Dimension2Code + "|" + "|" + Col1Dimension3Code + "|" + "|" + Col1Dimension4Code + "|" + "|" + Col1SizeCode + "|"

            mFormat = mFormat_ProcessOrderPendingForStockIssue

            ReportFrm.Text = "Stock Issue - " + mFormat

            ReportFrm.ProcFillGrid(DsReport)

            ReportFrm.DGL1.ReadOnly = True
            ReportFrm.DGL1.Columns(Col1PurchOrder).Visible = False
            ReportFrm.DGL1.Columns(Col1PurchOrderSr).Visible = False
            ReportFrm.DGL1.Columns(Col1PartyCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SKUCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SKU).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemTypeCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemType).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCategoryCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemGroupCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCode).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension1Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4Code).Visible = False
            ReportFrm.DGL1.Columns(Col1SizeCode).Visible = False

            ReportFrm.DGL1.Columns(Col1UnitMultiplier).Visible = False
            'ReportFrm.DGL1.Columns(Col1DealQty).Visible = False
            'ReportFrm.DGL1.Columns(Col1DealUnit).Visible = False

            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next

            ReportFrm.BtnProceed.Text = "Proceed"
            AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsReport = Nothing
        End Try
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        If mFormat = mFormat_ProcessOrderPendingForStockIssue Then
            If AgL.IsTableExist(bTempTable.Replace("[", "").Replace("]", ""), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)) Then
                mQry = "Drop Table " & "[" & bTempTable & "]"
                AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead))
            End If

            mQry = "CREATE TABLE [" & bTempTable & "] " &
                        " (PurchOrder nVarChar(21), PurchOrderSr Int, BaseQty Decimal(18,4),
                            PartyCode nVarChar(10), SkuCode nVarChar(10), Qty Decimal(18,4), Unit NVARCHAR (10), UnitMultiplier Decimal(18,4), 
                            DealQty Decimal(18,4), DealUnit NVARCHAR (10))"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnRead)

            For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                If ReportFrm.DGL1.Item(Col1Select, I).Value = "þ" Then
                    'Dim bBomItem As String = ClsMain.GetConsumption(ReportFrm.DGL1.Item(Col1SKUCode, I).Value)
                    Dim bBomItem As String = ClsMain.FGetBomWithBomPattern("", "",
                            "", "", "", "", "", "", "", "", ReportFrm.DGL1.Item(Col1SKUCode, I).Value, "")

                    If bBomItem = "" Then
                        MsgBox("Consumption not found for item at row number " + (I + 1).ToString, MsgBoxStyle.Information)
                        Exit Sub
                    End If

                    mQry = " INSERT INTO [" & bTempTable & "](PurchOrder, PurchOrderSr, BaseQty, PartyCode, SkuCode,
                            Qty, Unit, UnitMultiplier, DealQty, DealUnit)
                            Select " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1PurchOrder, I).Value) & " As PurchOrder, 
                            " & Val(ReportFrm.DGL1.Item(Col1PurchOrderSr, I).Value) & " As PurchOrderSr,
                            " & Val(ReportFrm.DGL1.Item(Col1Qty, I).Value) & " As BaseQty,
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1PartyCode, I).Value) & " As PartyCode, 
                            L.Item, 
                            Case When H.DealUnit = '" & ReportFrm.DGL1.Item(Col1DealUnit, I).Value & "' Then 
                                " & Val(ReportFrm.DGL1.Item(Col1DealQty, I).Value) & " * L.Qty 
                                Else " & Val(ReportFrm.DGL1.Item(Col1DealQty, I).Value) & " * Su.Multiplier * L.Qty End As Qty, 
                            I.Unit, 1 As UnitMultiplier, 
                            Case When H.DealUnit = '" & ReportFrm.DGL1.Item(Col1DealUnit, I).Value & "' Then 
                                " & Val(ReportFrm.DGL1.Item(Col1DealQty, I).Value) & " * L.Qty 
                                Else " & Val(ReportFrm.DGL1.Item(Col1DealQty, I).Value) & " * Su.Multiplier * L.Qty End As DealQty, 
                            I.Unit As DealUnit
                            From (Select * From BomDetail Where Code = '" & bBomItem & "') As L 
                            LEFT JOIN Item H On L.Code = H.Code
                            LEFT JOIN Item I On L.Item = I.Code 
                            LEFT JOIN StandardUnitConversion Su On Su.FromUnit = '" & ReportFrm.DGL1.Item(Col1DealUnit, I).Value & "' 
                                        And Su.ToUnit = H.DealUnit "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnRead)
                End If
            Next



            mQry = "Select Max(It.Code) As ItemTypeCode, Max(Sg.SubCode) As PartyCode, Max(Sku.Code) As SkuCode, Max(Sku.BaseItem) As ItemCode, Max(Sku.ItemCategory) As ItemCategoryCode, Max(Sku.ItemGroup) As ItemGroupCode, 
                    Max(SKU.Dimension1) As Dimension1Code, Max(SKU.Dimension2) As Dimension2Code, Max(Sku.Dimension3) As Dimension3Code, Max(Sku.Dimension4) As Dimension4Code, Max(Sku.Size) As SizeCode, 
                    Max(Sg.Name) As Party, Max(It.Name) As ItemType, Max(Sku.Description) As Sku, Max(IC.Description) as ItemCategory, 
                    Max(IG.Description) as ItemGroup, Max(I.Description) as Item, 
                    Max(D1.Description) as Dimension1, Max(D2.Description) as Dimension2,
                    Max(D3.Description) as Dimension3, Max(D4.Description) as Dimension4,
                    Max(Size.Description) as Size,                
                    Max(I.ItemCategory) as MainItemCategory, Max(I.ItemGroup) as MainItemGroup, Max(I.Specification) as MainItemSpecification, 
                    Max(I.Dimension1) as MDimension1,  Max(I.Dimension2) as MDimension2,  
                    Max(I.Dimension3) as MDimension3,  Max(I.Dimension4) as MDimension4,  Max(I.Size) as MainSize,
                    Sum(T.Qty) As Qty, Max(T.Unit) As Unit, 
                    Max(T.UnitMultiplier) As UnitMultiplier, Sum(T.DealQty) As DealQty, Max(T.DealUnit) As DealUnit, 
                    '' As Remark
                    From [" & bTempTable & "] T
                    LEFT JOIN SubGroup Sg ON Sg.SubCode = T.PartyCode
                    LEFT JOIN Item Sku ON Sku.Code = T.SkuCode 
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    LEFT JOIN Item I ON I.Code = IfNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & agConstants.ItemV_Type.SKU & "'
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                    LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                    LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                    LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                    LEFT JOIN Item Size ON Size.Code = Sku.Size
                    Group By T.SkuCode "
            DsReport = AgL.FillData(mQry, AgL.GcnRead)

            'If DsReport.Tables(0).Rows.Count = 0 Then MsgBox("No Records Selected...!", MsgBoxStyle.Information) : Exit Sub

            ReportFrm.Text = "Stock Issue"
            ReportFrm.ClsRep = Me
            'ReportFrm.InputColumnsStr = "|" + Col1ItemCode + "|" + "|" + Col1Dimension1Code + "|" + "|" + Col1Dimension2Code + "|" + "|" + Col1Dimension3Code + "|" + "|" + Col1Dimension4Code + "|"
            ReportFrm.InputColumnsStr = "|" + Col1Remark + "|" + "|" + Col1Qty + "|"

            mFormat = mFormat_SummaryToStockIssue

            ReportFrm.Text = "Stock Issue - " + mFormat

            ReportFrm.ProcFillGrid(DsReport)


            For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                If AgL.XNull(ReportFrm.DGL1.Item(Col1PartyCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Party, I).Tag = ReportFrm.DGL1.Item(Col1PartyCode, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemTypeCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1ItemType, I).Tag = ReportFrm.DGL1.Item(Col1ItemTypeCode, I).Value
                End If
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


            ReportFrm.DGL1.Columns(Col1PartyCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SKUCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SKU).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemTypeCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemType).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCategoryCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemGroupCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCode).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension1Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4Code).Visible = False
            ReportFrm.DGL1.Columns(Col1SizeCode).Visible = False

            ReportFrm.DGL1.Columns(Col1MItemCategory).Visible = False
            ReportFrm.DGL1.Columns(Col1MItemGroup).Visible = False
            ReportFrm.DGL1.Columns(Col1MItemSpecification).Visible = False
            ReportFrm.DGL1.Columns(Col1MDimension1).Visible = False
            ReportFrm.DGL1.Columns(Col1MDimension2).Visible = False
            ReportFrm.DGL1.Columns(Col1MDimension3).Visible = False
            ReportFrm.DGL1.Columns(Col1MDimension4).Visible = False
            ReportFrm.DGL1.Columns(Col1MSize).Visible = False

            ReportFrm.DGL1.Columns(Col1UnitMultiplier).Visible = False
            ReportFrm.DGL1.Columns(Col1DealQty).Visible = False
            ReportFrm.DGL1.Columns(Col1DealUnit).Visible = False

            ReportFrm.InputColumnsStr = "|" + Col1Remark + "|"

            ReportFrm.DGL1.Columns(Col1Remark).Visible = True
            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next
            ReportFrm.DGL1.Columns(Col1Qty).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark).ReadOnly = False

            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next

            ReportFrm.BtnProceed.Text = "Save"
            AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)
        ElseIf mFormat = mFormat_SummaryToStockIssue Then
            If FDataValidation() = False Then Exit Sub

            Try
                Dim mTrans As String = ""
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"


                FSave(AgL.GCn, AgL.ECmd)

                If AgL.IsTableExist(bTempTable.Replace("[", "").Replace("]", ""), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)) Then
                    mQry = "Drop Table " & "[" & bTempTable & "]"
                    AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead))
                End If


                AgL.ETrans.Commit()
                mTrans = "Commit"
                MsgBox("Process Completed...!", MsgBoxStyle.Information)
                ReportFrm.DGL1.DataSource = Nothing

                Try
                    ObjFrm.FRefreshMovRec()
                Catch ex As Exception
                End Try
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Public Sub FSave(Conn As Object, Cmd As Object)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0

        Dim I As Integer = 0, J As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim mDescription As String = ""
        Dim mStockHeadDocId As String = ""
        Dim mV_No As String
        Dim mV_Prefix As String
        Dim mV_Date As String
        Dim mSr As Integer = 0
        Dim mManualRefNo As String = ""
        Dim mRemarks As String = ""
        Dim bProcess As String = ""

        If AgL.XNull(ReportFrm.FGetText(rowEntryNo)).ToString() = "" Then
            MsgBox("Entry No is required...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If AgL.XNull(ReportFrm.FGetText(rowEntryDate)).ToString() = "" Then
            MsgBox("Entry date is required...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If AgL.XNull(ReportFrm.FGetText(rowGodown)).ToString() = "" Then
            MsgBox("Godown is required...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        mV_Date = AgL.XNull(ReportFrm.FGetText(rowEntryDate)).ToString()
        'mStockHeadDocId = AgL.GetDocId(mV_Type, CStr(0), CDate(mV_Date), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
        mStockHeadDocId = AgL.CreateDocId(AgL, "StockHead", mV_Type, CStr(0), CDate(mV_Date), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
        mV_No = Val(AgL.DeCodeDocID(mStockHeadDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
        mV_Prefix = AgL.DeCodeDocID(mStockHeadDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

        bProcess = "PWeaving"
        mManualRefNo = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", mV_Type, AgL.PubLoginDate, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
        mRemarks = AgL.XNull(ReportFrm.FGetText(rowRemarks)).ToString()

        If AgL.VNull(AgL.Dman_Execute(" SELECT Count(*) AS Cnt
                FROM StockHead H With (NoLock)
                WHERE H.Div_Code = '" & AgL.PubDivCode & "' 
                AND H.Site_Code = '" & AgL.PubSiteCode & "' 
                AND H.ManualRefNo = '" & mManualRefNo & "' 
                AND H.V_Type = '" & mV_Type & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) > 0 Then
            Err.Raise(1,, "Entry no already exist...!")
        End If

        mQry = "INSERT INTO StockHead (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, 
                    ManualRefNo, Process, SettingGroup, SubCode, Remarks, EntryBy, EntryDate)
                    Select " & AgL.Chk_Text(mStockHeadDocId) & " As Docid, " & AgL.Chk_Text(mV_Type) & " As V_Type, 
                    " & AgL.Chk_Text(mV_Prefix) & " As v_prefix, " & AgL.Chk_Date(mV_Date) & " As v_date, 
                    " & Val(mV_No) & " As V_No, " & AgL.Chk_Text(AgL.PubDivCode) & " As div_code, 
                    " & AgL.Chk_Text(AgL.PubSiteCode) & " As Site_Code, 
                    " & AgL.Chk_Text(mManualRefNo) & "  As ManualRefNo, 
                    " & AgL.Chk_Text(bProcess) & "  As Process, 
                    " & AgL.Chk_Text("RM") & "  As SettingGroup, 
                    " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1PartyCode, 0).Value)) & "  As SubCode, 
                    " & AgL.Chk_Text(mRemarks) & " As Remarks,  " & AgL.Chk_Text(AgL.PubUserName) & "  As entryby, 
                    " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To ReportFrm.DGL1.RowCount - 1
            If AgL.XNull(ReportFrm.DGL1.Item(Col1SKU, I).Value) <> "" Then
                If Val(ReportFrm.DGL1.Item(Col1Qty, I).Value) > 0 Then
                    mSr += 1
                    mQry = "Insert Into StockHeadDetail(DocId, Sr, Godown, Item, 
                           Qty, Unit, UnitMultiplier, DealUnit, DealQty, Remark) "
                    mQry += " Select " & AgL.Chk_Text(mStockHeadDocId) & ", " & mSr & ", " &
                        " " & AgL.Chk_Text(AgL.XNull(ReportFrm.FGetCode(rowGodown))) & "  As Godown,  " &
                        " " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1SKU, I).Tag)) & ", " &
                        " " & Val(AgL.VNull(ReportFrm.DGL1.Item(Col1Qty, I).Value)) & ", " &
                        " " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Unit, I).Value)) & ", " &
                        " " & Val(AgL.VNull(ReportFrm.DGL1.Item(Col1UnitMultiplier, I).Value)) & ", " &
                        " " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1DealUnit, I).Value)) & ", " &
                        " " & Val(AgL.VNull(ReportFrm.DGL1.Item(Col1DealQty, I).Value)) & ", " &
                        " " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Remark, I).Value)) & " "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = "INSERT INTO StockHeadDetailSku (DocID, Sr, ItemCategory, ItemGroup, Item, 
                            Dimension1, Dimension2, Dimension3, Dimension4, Size)
                            Select " & AgL.Chk_Text(mStockHeadDocId) & ", " & mSr & ", 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag) & " ItemCategory, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag) & " ItemGroup, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Tag) & " Item, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag) & " Dimension1, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag) & " Dimension2, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag) & " Dimension3, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag) & " Dimension4, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Size, I).Tag) & " Size "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = " Select * From [" & bTempTable & "]"
                    Dim Dt1 As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

                    mQry = " Select L.Docid, L.Sr As TSr, T.PurchOrder, T.PurchOrderSr, T.BaseQty As BaseQty,
                            L.Qty * T.BaseQty / (
                                Select Sum(T.BaseQty) As Qty
                                From [" & bTempTable & "] T
                                Where IsNull(T.SkuCode,'') = IsNull(L.Item,'') 
                            ) As Qty
                            From StockHeadDetail L With (NoLock)
                            LEFT JOIN [" & bTempTable & "] T On IsNull(L.Item,'') = IsNull(T.SkuCode,'') 
                            Where L.DocId = " & AgL.Chk_Text(mStockHeadDocId) & " And Sr = " & mSr & ""
                    Dim bStockHeadDetailBase As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

                    Dim bStockHeadDetailBaseSr As Integer = 0
                    For J = 0 To bStockHeadDetailBase.Rows.Count - 1
                        bStockHeadDetailBaseSr += 1
                        mQry = " INSERT INTO StockHeadDetailBase (DocID, TSr, Sr, ReferenceDocId, ReferenceDocIdTSr, BaseQty, Qty)
                            Select " & AgL.Chk_Text(AgL.XNull(bStockHeadDetailBase.Rows(J)("DocId"))) & " As DocID, 
                            " & Val(AgL.VNull(bStockHeadDetailBase.Rows(J)("TSr"))) & " As TSr, 
                            " & Val(bStockHeadDetailBaseSr) & " As Sr, 
                            " & AgL.Chk_Text(AgL.XNull(bStockHeadDetailBase.Rows(J)("PurchOrder"))) & " As PurchOrder,
                            " & Val(AgL.XNull(bStockHeadDetailBase.Rows(J)("PurchOrderSr"))) & " As PurchOrderSr,
                            " & Val(AgL.XNull(bStockHeadDetailBase.Rows(J)("BaseQty"))) & " As BaseQty,
                            " & Val(AgL.VNull(bStockHeadDetailBase.Rows(J)("Qty"))) & " As Qty "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Next
                End If
            End If
        Next
        AgL.UpdateVoucherCounter(mStockHeadDocId, CDate(mV_Date), Conn, Cmd, AgL.PubDivCode, AgL.PubSiteCode)
    End Sub
    Private Function FDataValidation() As Boolean
        FDataValidation = False

        If CDate(AgL.XNull(ReportFrm.FGetText(rowEntryDate))) > CDate(AgL.PubLoginDate) Then
            MsgBox("Future date transaction is not allowed.", MsgBoxStyle.Information)
            ReportFrm.FilterGrid.Focus()
            FDataValidation = False
            Exit Function
        End If



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
                ReportFrm.DGL1.Item(Col1SKU, I).Tag = ClsMain.FGetSKUCode(I + 1, AgL.XNull(ReportFrm.DGL1.Item(Col1ItemType, I).Tag) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategory, I).Value) _
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
