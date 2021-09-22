Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class FrmPurchaseInvoiceStockIssRec
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1BtnDetail As String = "Detail"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"


    Public Const ColSNo As String = "S.No."
    Public Const ColFromProcess As String = "From Process"
    Public Const ColItemType As String = "Item Type"
    Public Const ColSku As String = "Sku"
    Public Const ColItemCategory As String = "Item Category"
    Public Const ColItemGroup As String = "Item Group"
    Public Const ColItem As String = "Item"
    Public Const ColDimension1 As String = "Dimension1"
    Public Const ColDimension2 As String = "Dimension2"
    Public Const ColDimension3 As String = "Dimension3"
    Public Const ColDimension4 As String = "Dimension4"
    Public Const ColSize As String = "Size"
    Public Const ColStock As String = "Stock"
    Public Const ColDocQty As String = "Doc Qty"
    Public Const ColLossQty As String = "Loss Qty"
    Public Const ColQty As String = "Qty"
    Public Const ColUnit As String = "Unit"
    Public Const ColQtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const ColPcs As String = "Pcs"
    Public Const ColUnitMultiplier As String = "Unit Multiplier"
    Public Const ColDealQty As String = "Deal Qty"
    Public Const ColDealUnit As String = "Deal Unit"
    Public Const ColDealUnitDecimalPlaces As String = "Deal Decimal Places"
    Public Const ColRate As String = "Rate"
    Public Const ColAmount As String = "Amount"
    Public Const ColStockSr As String = "Stock Sr"
    Public Const ColStockIssueNo As String = "Stock Issue No"
    Public Const ColStockIssueDate As String = "Stock Issue Date"
    Public Const ColRemark As String = "Remark"
    Public Const ColIsRecordLocked As String = "Is Record Locked"

    Public rowGodown As Integer = 0
    Public rowStockIssRecNos As Integer = 1
    Public rowBtnStandardConsumption As Integer = 2

    Public Const hcGodown As String = "Godown"
    Public Const hcStockIssRecNos As String = "Stock Iss Rec Nos"
    Public Const hcBtnStandardConsumption As String = "Fill Standard Consumption"


    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mSearchCode As String = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mPartyCode As String
    Dim mProcessCode As String = ""
    Dim mDglMainLastRowIndex As Integer
    Dim mCopyToSearchCodesArr As String()
    Dim DtItemRelation As DataTable
    Dim mTransNature As String = NCatNature.Issue
    Dim mObjFrmPurchInvoice As FrmPurchInvoiceDirect_WithDimension

    Public mDimensionSrl As Integer

    Public Property objFrmPurchInvoice() As FrmPurchInvoiceDirect_WithDimension
        Get
            objFrmPurchInvoice = mObjFrmPurchInvoice
        End Get
        Set(ByVal value As FrmPurchInvoiceDirect_WithDimension)
            mObjFrmPurchInvoice = value
        End Set
    End Property
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub IniGrid(SearchCode As String)
        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 250, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 500, 255, Col1Value, True, False)
            .AddAgTextColumn(DglMain, Col1LastValue, 170, 255, Col1LastValue, False, False)
        End With
        AgL.AddAgDataGrid(DglMain, PnlMain)
        AgL.GridDesign(DglMain)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.ColumnHeadersVisible = False
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None

        DglMain.Rows.Add(3)
        For I As Integer = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next
        DglMain.Item(Col1Head, rowGodown).Value = hcGodown
        DglMain.Item(Col1Head, rowStockIssRecNos).Value = hcStockIssRecNos
        DglMain.Item(Col1Head, rowBtnStandardConsumption).Value = hcBtnStandardConsumption
        DglMain.Item(Col1Value, rowBtnStandardConsumption) = New DataGridViewButtonCell
        DglMain.Name = "DglMain"
        DglMain.Tag = "VerticalGrid"

        For I As Integer = 0 To DglMain.Rows.Count - 1
            If AgL.XNull(DglMain(Col1HeadOriginal, I).Value) = "" Then
                DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
            End If
        Next


        Dgl1.Name = "Dgl1"
        FDesignColumns(Dgl1)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgLastColumn = Dgl1.Columns(ColRemark).Index
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)

        Dgl2.Name = "Dgl2"
        FDesignColumns(Dgl2)
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 40
        Dgl2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl2)
        Dgl2.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl2.BackgroundColor = Me.BackColor
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AgLastColumn = Dgl2.Columns(ColRemark).Index
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl2, AgL)

        ApplyUISetting()

        If AgL.StrCmp(EntryMode, "Browse") Then
            Dgl1.ReadOnly = True
            Dgl2.ReadOnly = True
        Else
            Dgl1.ReadOnly = False
            Dgl2.ReadOnly = False
        End If

        FMoveRec(SearchCode)

        mSearchCode = SearchCode

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2, False)
    End Sub
    Private Sub FDesignColumns(DglControl As AgControls.AgDataGrid)
        With AgCL
            .AddAgTextColumn(DglControl, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglControl, ColItemType, 100, 0, ColItemType, False, True)
            .AddAgTextColumn(DglControl, ColSku, 100, 0, ColSku, False, True)
            .AddAgTextColumn(DglControl, ColItemCategory, 120, 0, ColItemCategory, True, False)
            .AddAgTextColumn(DglControl, ColItemGroup, 120, 0, ColItemGroup, False, False)
            .AddAgTextColumn(DglControl, ColItem, 120, 0, ColItem, False, False)
            .AddAgTextColumn(DglControl, ColDimension1, 120, 0, ColDimension1, True, False)
            .AddAgTextColumn(DglControl, ColDimension2, 120, 0, ColDimension2, False, False)
            .AddAgTextColumn(DglControl, ColDimension3, 120, 0, ColDimension3, False, False)
            .AddAgTextColumn(DglControl, ColDimension4, 120, 0, ColDimension4, True, False)
            .AddAgTextColumn(DglControl, ColSize, 120, 0, ColSize, False, False)
            .AddAgTextColumn(DglControl, ColFromProcess, 100, 255, ColFromProcess, False, True)
            .AddAgNumberColumn(DglControl, ColStock, 70, 8, 0, False, ColStock, False, True, True)
            .AddAgNumberColumn(DglControl, ColDocQty, 70, 8, 4, False, ColDocQty, True, False, True)
            .AddAgNumberColumn(DglControl, ColLossQty, 70, 8, 4, False, ColLossQty, False, False, True)
            .AddAgNumberColumn(DglControl, ColQty, 70, 8, 4, False, ColQty, True, False, True)
            .AddAgTextColumn(DglControl, ColUnit, 50, 0, ColUnit, False, True)
            .AddAgTextColumn(DglControl, ColQtyDecimalPlaces, 50, 0, ColQtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(DglControl, ColPcs, 80, 8, 4, False, ColPcs, False, False, True)
            .AddAgNumberColumn(DglControl, ColUnitMultiplier, 70, 8, 4, False, ColUnitMultiplier, False, True, True)
            .AddAgNumberColumn(DglControl, ColDealQty, 70, 8, 3, False, ColDealQty, False, True, True)
            .AddAgTextColumn(DglControl, ColDealUnit, 60, 0, ColDealUnit, False, True)
            .AddAgTextColumn(DglControl, ColDealUnitDecimalPlaces, 50, 0, ColDealUnitDecimalPlaces, False, True, False)
            .AddAgNumberColumn(DglControl, ColRate, 80, 8, 2, False, ColRate, True, False, True)
            .AddAgNumberColumn(DglControl, ColAmount, 100, 8, 2, False, ColAmount, True, True, True)
            .AddAgTextColumn(DglControl, ColStockSr, 150, 255, ColStockSr, False, False)
            .AddAgTextColumn(DglControl, ColStockIssueNo, 60, 0, ColStockIssueNo, False, True)
            .AddAgDateColumn(DglControl, ColStockIssueDate, 120, ColStockIssueDate, False, True)
            .AddAgTextColumn(DglControl, ColRemark, 150, 255, ColRemark, True, False)
            .AddAgTextColumn(DglControl, ColIsRecordLocked, 150, 255, ColIsRecordLocked, False, True)
        End With
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            mOkButtonPressed = False
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            'Me.Top = 300
            'Me.Left = 300
            FIniList()

            If AgL.StrCmp(EntryMode, "Browse") Then
                DglMain.ReadOnly = True
                Dgl1.ReadOnly = True
                Dgl2.ReadOnly = True
            Else
                DglMain.ReadOnly = False
                Dgl1.ReadOnly = False
                Dgl2.ReadOnly = False
            End If

            If AgL.XNull(DglMain.Item(Col1Value, rowGodown).Tag) = "" And
                    AgL.XNull(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowGodown).Tag) <> "" Then
                DglMain.Item(Col1Value, rowGodown).Tag = mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowGodown).Tag
                DglMain.Item(Col1Value, rowGodown).Value = mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowGodown).Value
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        FOkButtonClick()
    End Sub
    Public Sub FMoveRec(ByVal SearchCode As String)
        mQry = "Select Distinct L.DocId As DocId, H.V_Type || '-' || H.ManualRefNo As PurchInvoiceNo 
                From PurchInvoice H 
                LEFT JOIN PurchInvoiceDetail L On H.DocId = L.DocId
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where Vt.NCat = '" & Ncat.StockIssue & "'
                And L.ReferenceDocId = '" & SearchCode & "'"
        Dim DtStockIssNo As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For K As Integer = 0 To DtStockIssNo.Rows.Count - 1
            If DglMain.Item(Col1Value, rowStockIssRecNos).Tag <> "" Then DglMain.Item(Col1Value, rowStockIssRecNos).Tag += ","
            DglMain.Item(Col1Value, rowStockIssRecNos).Tag += DtStockIssNo.Rows(K)("DocId")

            If DglMain.Item(Col1Value, rowStockIssRecNos).Value <> "" Then DglMain.Item(Col1Value, rowStockIssRecNos).Value += ","
            DglMain.Item(Col1Value, rowStockIssRecNos).Value += DtStockIssNo.Rows(K)("PurchInvoiceNo")
        Next

        mQry = " Select Distinct Godown, Sg.Name As GodownName 
                From PurchInvoiceDetail L 
                LEFT JOIN SubGroup Sg On L.Godown = Sg.SubCode
                Where L.DocId = '" & SearchCode & "'"
        Dim DtGodown As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtGodown.Rows.Count > 0 Then
            DglMain.Item(Col1Value, rowGodown).Tag = DtGodown.Rows(0)("Godown")
            DglMain.Item(Col1Value, rowGodown).Value = DtGodown.Rows(0)("GodownName")
        End If


        FMoveRecForGrid(SearchCode, Dgl1, ItemTypeCode.RawProduct)
        FMoveRecForGrid(SearchCode, Dgl2, ItemTypeCode.OtherRawProduct)
    End Sub
    Public Sub FMoveRecForGrid(ByVal DocId As String, DglControl As AgControls.AgDataGrid, ItemType As String)
        Dim DtTemp As DataTable = Nothing
        Dim DsMain As DataSet
        Dim I As Integer = 0
        Dim mQryStockSr As String = ""

        Try
            If AgL.PubServerName = "" Then
                mQryStockSr = "Select   (Sr ,',') from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr"
            Else
                mQryStockSr = "Select  Cast(Sr as Varchar) + ',' from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr for xml path('')"
            End If

            Dim bExternalDocIds As String = ""
            If AgL.XNull(DglMain.Item(Col1Value, rowStockIssRecNos).Tag) <> "" Then
                bExternalDocIds = DglMain.Item(Col1Value, rowStockIssRecNos).Tag.ToString.Replace(",", "','")
            End If

            mQry = "Select L.*, H.V_Type || '-' || H.ManualRefNo As StockIssueNo, H.V_Date As StockIssueDate, 
                    Barcode.Description as BarcodeName, 
                    I.Description As ItemDesc, I.ManualCode, 
                    U.ShowDimensionDetailInSales, U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, U.ShowDimensionDetailInPurchase,
                    MU.DecimalPlaces As DealUnitDecimalPlaces,
                    Sku.Code As SkuCode, Sku.Description As SkuDescription, 
                    It.Code As ItemType, It.Name As ItemTypeDesc,
                    IG.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, 
                    Sids.Item As ItemCode, Sids.ItemCategory, Sids.ItemGroup, 
                    Sids.Dimension1, Sids.Dimension2, 
                    Sids.Dimension3, Sids.Dimension4, Sids.Size, 
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                    I.ItemCategory as MItemCategory, I.ItemGroup as MItemGroup, I.Specification as MItemSpecification, 
                    I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MSize, 
                    Godown.Name as GodownName, ISt.Description as ItemStateName, RawMaterial.Description As RawMaterialDesc, 
                    (" & mQryStockSr & ") as StockSr 
                    From (Select * From PurchInvoiceDetail  With (NoLock)  
                                Where (DocId = '" & DocId & "' And SubRecordType = '" & mSubRecordType_StockIssue & "')
                                Or DocId In ('" & bExternalDocIds & "')) As L 
                    LEFT JOIN PurchInvoiceDetailSku Sids With (NoLock) On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                    LEFT JOIN PurchInvoice H On L.DocId = H.DocId
                    LEFT JOIN Item Sku ON Sku.Code = L.Item
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    Left Join Item IC On Sids.ItemCategory = IC.Code
                    Left Join Item IG On Sids.ItemGroup = IG.Code
                    LEFT JOIN Item I ON Sids.Item = I.Code
                    LEFT JOIN Item D1 ON Sids.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sids.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sids.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sids.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sids.Size = Size.Code
                    LEFT JOIN Item Ist On L.ItemState = Ist.Code
                    LEFT JOIN Barcode  With (NoLock) On L.Barcode = Barcode.Code
                    LEFT JOIN SubGroup G On L.Godown = G.SubCode
                    Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                    Left Join Unit MU  With (NoLock) On L.DealUnit = MU.Code 
                    Left Join Subgroup Godown On L.Godown = Godown.Subcode
                    LEFT JOIN Item RawMaterial ON L.RawMaterial = RawMaterial.Code
                    Where Sku.ItemType = '" & ItemType & "'
                    Order By L.Sr "
            DsMain = AgL.FillData(mQry, AgL.GCn)
            With DsMain.Tables(0)
                DglControl.RowCount = 1
                DglControl.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DsMain.Tables(0).Rows.Count - 1
                        DglControl.Rows.Add()
                        DglControl.Item(ColSNo, I).Value = DglControl.Rows.Count - 1
                        DglControl.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))

                        DglControl.Item(ColStockSr, I).Value = AgL.XNull(.Rows(I)("StockSr"))
                        If DglControl.Item(ColStockSr, I).Value <> "" Then
                            If DglControl.Item(ColStockSr, I).Value.ToString.Substring(DglControl.Item(ColStockSr, I).Value.ToString.Length - 1, 1) = "," Then
                                DglControl.Item(ColStockSr, I).Value = DglControl.Item(ColStockSr, I).Value.ToString.Substring(0, DglControl.Item(ColStockSr, I).Value.ToString.Length - 1)
                            End If
                        End If

                        DglControl.Item(ColSku, I).Tag = AgL.XNull(.Rows(I)("SkuCode"))
                        DglControl.Item(ColSku, I).Value = AgL.XNull(.Rows(I)("SkuDescription"))

                        DglControl.Item(ColItemType, I).Tag = AgL.XNull(.Rows(I)("ItemType"))
                        DglControl.Item(ColItemType, I).Value = AgL.XNull(.Rows(I)("ItemTypeDesc"))

                        DglControl.Item(ColItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                        DglControl.Item(ColItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))

                        DglControl.Item(ColItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                        DglControl.Item(ColItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))

                        DglControl.Item(ColItem, I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                        DglControl.Item(ColItem, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                        DglControl.Item(ColDimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        DglControl.Item(ColDimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                        DglControl.Item(ColDimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        DglControl.Item(ColDimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                        DglControl.Item(ColDimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                        DglControl.Item(ColDimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))

                        DglControl.Item(ColDimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                        DglControl.Item(ColDimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))

                        DglControl.Item(ColSize, I).Tag = AgL.XNull(.Rows(I)("Size"))
                        DglControl.Item(ColSize, I).Value = AgL.XNull(.Rows(I)("SizeDesc"))

                        DglControl.Item(ColDocQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("DocQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        DglControl.Item(ColQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        DglControl.Item(ColUnit, I).Value = AgL.XNull(.Rows(I)("Unit"))

                        DglControl.Item(ColUnit, I).Tag = AgL.VNull(.Rows(I)("ShowDimensionDetailInSales"))

                        DglControl.Item(ColQtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))


                        If AgL.VNull(DglControl.Item(ColUnit, I).Tag) Then
                            DglControl.Item(ColDocQty, I).Style.ForeColor = Color.Blue
                            DglControl.Item(ColDocQty, I).ReadOnly = True
                            ShowPurchInvoiceDimensionDetail(DocId, DglControl, I, False)
                        End If

                        DglControl.Item(ColDealUnitDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DealUnitDecimalPlaces"))
                        DglControl.Item(ColUnitMultiplier, I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                        DglControl.Item(ColDealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                        DglControl.Item(ColDealQty, I).Value = Format(AgL.VNull(.Rows(I)("DealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                        DglControl.Item(ColRate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                        DglControl.Item(ColAmount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                        DglControl.Item(ColRemark, I).Value = AgL.XNull(.Rows(I)("Remark"))

                        If DocId <> AgL.XNull(.Rows(I)("DocId")) Then
                            DglControl.Item(ColIsRecordLocked, I).Value = 1
                            If DglControl.Item(ColIsRecordLocked, I).Value <> 0 Then DglControl.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : DglControl.Rows(I).ReadOnly = True

                            DglControl.Item(ColStockIssueNo, I).Value = AgL.XNull(.Rows(I)("StockIssueNo"))
                            DglControl.Item(ColStockIssueDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("StockIssueDate")))
                        End If

                        If DglControl.Name = Dgl1.Name Then
                            LblTotalQtyForDgl1.Text = Val(LblTotalQtyForDgl1.Text) + Val(DglControl.Item(ColQty, I).Value)
                        ElseIf DglControl.Name = Dgl2.Name Then
                            LblTotalQtyForDgl2.Text = Val(LblTotalQtyForDgl2.Text) + Val(DglControl.Item(ColQty, I).Value)
                        End If
                    Next I
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FOkButtonClick()
        Dim I As Integer = 0
        If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
        mOkButtonPressed = True
        Me.Close()
    End Sub
    Private Sub ApplyUISetting()
        Dim bEntryNCat As String = AgL.Dman_Execute("Select NCat From Voucher_Type Where V_Type = '" & mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag & "'", AgL.GCn).ExecuteScalar()

        GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bEntryNCat, mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag, "", "", ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bEntryNCat, mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag, "", "", ClsMain.GridTypeConstants.HorizontalGrid)
        GetUISetting_WithDataTables(Dgl2, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bEntryNCat, mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag, "", "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Public Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim bEntryNCat As String = Ncat.StockIssue
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode,
                AgL.PubSiteCode, "", bEntryNCat, "", mProcessCode, "")
        FGetSettings = mValue
    End Function
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        DglControlEditingControlKeyDown(Dgl1, e)
    End Sub
    Private Sub Dgl2_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl2.EditingControl_KeyDown
        DglControlEditingControlKeyDown(Dgl2, e)
    End Sub
    Private Sub DglControlEditingControlKeyDown(DglControl As AgControls.AgDataGrid, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If DglControl.CurrentCell Is Nothing Then Exit Sub
            Dim mRowIndex As Integer = DglControl.CurrentCell.RowIndex

            Select Case DglControl.Columns(DglControl.CurrentCell.ColumnIndex).Name
                Case ColItem
                    If e.KeyCode <> Keys.Enter Then
                        If DglControl.AgHelpDataSet(ColItem) Is Nothing Then
                            DglControl.AgHelpDataSet(ColItem) = FCreateHelpItem(DglControl.CurrentCell.RowIndex, DglControl.Item(ColItemType, mRowIndex).Tag)
                        End If
                    End If
                Case ColItemCategory
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If DglControl.AgHelpDataSet(ColItemCategory) Is Nothing Then
                            DglControl.AgHelpDataSet(ColItemCategory) = FCreateHelpItemCategory(DglControl.CurrentCell.RowIndex, DglControl.Item(ColItemType, mRowIndex).Tag)
                        End If
                    End If
                Case ColItemGroup
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If DglControl.AgHelpDataSet(ColItemGroup) Is Nothing Then
                            DglControl.AgHelpDataSet(ColItemGroup) = FCreateHelpItemGroup(DglControl.CurrentCell.RowIndex)
                        End If
                    End If
                Case ColDimension1
                    If e.KeyCode <> Keys.Enter Then
                        If DglControl.AgHelpDataSet(ColDimension1) Is Nothing Then
                            DglControl.AgHelpDataSet(ColDimension1) = FCreateHelpDimension1(DglControl.CurrentCell.RowIndex, DglControl.Item(ColItemCategory, mRowIndex).Tag, DglControl.Item(ColItem, mRowIndex).Tag, DglControl.Item(ColDimension3, mRowIndex).Tag)
                        End If
                    End If
                Case ColDimension2
                    If e.KeyCode <> Keys.Enter Then
                        If DglControl.AgHelpDataSet(ColDimension2) Is Nothing Then
                            DglControl.AgHelpDataSet(ColDimension2) = FCreateHelpDimension2(DglControl.CurrentCell.RowIndex, DglControl.Item(ColItemCategory, mRowIndex).Tag, DglControl.Item(ColItem, mRowIndex).Tag, DglControl.Item(ColDimension3, mRowIndex).Tag)
                        End If
                    End If
                Case ColDimension3
                    If e.KeyCode <> Keys.Enter Then
                        If DglControl.AgHelpDataSet(ColDimension3) Is Nothing Then
                            DglControl.AgHelpDataSet(ColDimension3) = FCreateHelpDimension3(DglControl.CurrentCell.RowIndex, DglControl.Item(ColItemCategory, mRowIndex).Tag, DglControl.Item(ColItem, mRowIndex).Tag, DglControl.Item(ColDimension3, mRowIndex).Tag)
                        End If
                    End If
                Case ColDimension4
                    If e.KeyCode <> Keys.Enter Then
                        If DglControl.AgHelpDataSet(ColDimension4) Is Nothing Then
                            DglControl.AgHelpDataSet(ColDimension4) = FCreateHelpDimension4(DglControl.CurrentCell.RowIndex, DglControl.Item(ColItemCategory, mRowIndex).Tag, DglControl.Item(ColItem, mRowIndex).Tag, DglControl.Item(ColDimension3, mRowIndex).Tag)
                        End If
                    End If
                Case ColSize
                    If e.KeyCode <> Keys.Enter Then
                        If DglControl.AgHelpDataSet(ColSize) Is Nothing Then
                            DglControl.AgHelpDataSet(ColSize) = FCreateHelpSize(DglControl.CurrentCell.RowIndex, DglControl.Item(ColItemCategory, mRowIndex).Tag, DglControl.Item(ColItem, mRowIndex).Tag, DglControl.Item(ColDimension3, mRowIndex).Tag)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FCreateHelpItemCategory(RowIndex As Integer, ItemType As String) As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        'Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        'If bFilterInclude_ItemType <> "" Then
        '    If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
        '        strCond += " And CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 "
        '    ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
        '        strCond += " And CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 "
        '    End If
        'End If

        strCond += " And I.ItemType =  '" & ItemType & "'"

        mQry = "SELECT I.Code, I.Description
                FROM ItemCategory I  With (NoLock)
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpItemCategory = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpItemGroup(RowIndex As Integer) As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        'Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        'If bFilterInclude_ItemType <> "" Then
        '    If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
        '        strCond += " And CharIndex('+' || I.Code,'" & bFilterInclude_ItemType & "') > 0 "
        '    ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
        '        strCond += " And CharIndex('-' || I.Code,'" & bFilterInclude_ItemType & "') <= 0 "
        '    End If
        'End If

        If Dgl1.Item(ColItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(ColItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null ) "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
            strCond += " And (IG.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(IG.ShowItemGroupInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
            strCond += " And (IG.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(IG.ShowItemGroupInOtherSites,0) =1) "
        End If


        mQry = "Select IG.Code, IG.Description 
                From Item I  With (NoLock)
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & "
                Group By I.ItemGroup,IG.Code, IG.Description "
        FCreateHelpItemGroup = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension1(RowIndex As Integer, ItemCategory As String, Item As String, Dimension3 As String) As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        'Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        'If bFilterInclude_ItemType <> "" Then
        '    If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
        '        strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
        '    ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
        '        strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
        '    End If
        'End If

        If Dgl1.Item(ColItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & ItemCategory & "' Or I.ItemCategory Is Null Or I.BaseItem Is Not Null) "
        End If

        If Dgl1.Item(ColItem, RowIndex).Value <> "" Then
            strCond += " And (I.BaseItem = '" & Item & "' Or I.BaseItem Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension1 & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dimension3) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.Dimension1 & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dimension3 & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                FROM Item I  With (NoLock)
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpDimension1 = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension2(RowIndex As Integer, ItemCategory As String, Item As String, Dimension3 As String) As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        'Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        'If bFilterInclude_ItemType <> "" Then
        '    If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
        '        strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
        '    ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
        '        strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
        '    End If
        'End If

        If Dgl1.Item(ColItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(ColItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null Or I.BaseItem Is Not Null) "
        End If

        If Dgl1.Item(ColItem, RowIndex).Value <> "" Then
            strCond += " And (I.BaseItem = '" & Dgl1.Item(ColItem, RowIndex).Tag & "' Or I.BaseItem Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension2 & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dgl1.Item(ColDimension3, RowIndex).Tag) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.Dimension2 & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dgl1.Item(ColDimension3, RowIndex).Tag & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpDimension2 = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension3(RowIndex As Integer, ItemCategory As String, Item As String, Dimension3 As String) As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        'Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        'If bFilterInclude_ItemType <> "" Then
        '    If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
        '        strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
        '    ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
        '        strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
        '    End If
        'End If

        If ItemCategory <> "" Then
            strCond += " And (I.ItemCategory = '" & ItemCategory & "' Or I.ItemCategory Is Null Or I.BaseItem Is Not Null) "
        End If

        If Item <> "" Then
            strCond += " And (I.BaseItem = '" & Item & "' Or I.BaseItem Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension3 & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dimension3) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.Dimension3 & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dimension3 & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpDimension3 = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension4(RowIndex As Integer, ItemCategory As String, Item As String, Dimension3 As String) As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        'Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        'If bFilterInclude_ItemType <> "" Then
        '    If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
        '        strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
        '    ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
        '        strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
        '    End If
        'End If

        If ItemCategory <> "" Then
            strCond += " And (I.ItemCategory = '" & ItemCategory & "' Or I.ItemCategory Is Null Or I.BaseItem Is Not Null) "
        End If

        If Item <> "" Then
            strCond += " And (I.BaseItem = '" & Item & "' Or I.BaseItem Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension4 & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dimension3) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.Dimension4 & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dimension3 & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpDimension4 = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpSize(RowIndex As Integer, ItemCategory As String, Item As String, Dimension3 As String) As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        'Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        'If bFilterInclude_ItemType <> "" Then
        '    If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
        '        strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
        '    ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
        '        strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
        '    End If
        'End If


        If ItemCategory <> "" Then
            strCond += " And (I.ItemCategory = '" & ItemCategory & "' Or I.ItemCategory Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.SIZE & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dimension3) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.SIZE & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dimension3 & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                FROM Item I  With (NoLock)
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpSize = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpItem(RowIndex As Integer, ItemType As String) As DataSet
        Dim strCond As String = ""

        'Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        'If bFilterInclude_ItemType <> "" Then
        '    If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
        '        strCond += " And CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 "
        '    ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
        '        strCond += " And CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 "
        '    End If
        'End If

        'strCond += " And CharIndex('-' || I.ItemType,'" & ItemType & "') > 0 "
        strCond += " And I.ItemType = '" & ItemType & "' "

        Dim bFilterInclude_ItemV_Type As String = FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General)
        If bFilterInclude_ItemV_Type <> "" Then
            If bFilterInclude_ItemV_Type.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.V_Type,'" & bFilterInclude_ItemV_Type & "') > 0 "
            ElseIf bFilterInclude_ItemV_Type.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.V_Type,'" & bFilterInclude_ItemV_Type & "') <= 0 "
            End If
        Else
            strCond += " And I.V_Type = 'ITEM' "
        End If

        Dim bFilterInclude_ItemGroup As String = FGetSettings(SettingFields.FilterInclude_ItemGroup, SettingType.General)
        If bFilterInclude_ItemGroup <> "" Then
            If bFilterInclude_ItemGroup.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.ItemGroup,'" & bFilterInclude_ItemGroup & "') > 0 "
            ElseIf bFilterInclude_ItemGroup.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.ItemGroup,'" & bFilterInclude_ItemGroup & "') <= 0 "
            End If
        End If

        Dim bFilterInclude_Item As String = FGetSettings(SettingFields.FilterInclude_Item, SettingType.General)
        If bFilterInclude_Item <> "" Then
            If bFilterInclude_Item.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.Code,'" & bFilterInclude_Item & "') > 0 "
            ElseIf bFilterInclude_Item.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.Code,'" & bFilterInclude_Item & "') <= 0 "
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1) "
        End If

        mQry = "SELECT I.Code, I.Description, I.ManualCode as ItemCode, I.Rate " &
                  " FROM Item I  With (NoLock) " &
                  " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpItem = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Sub FIniList()
        mQry = "SELECT Ir.*, I.V_Type As ItemV_Type, RI.V_Type As RelatedItemV_Type 
                FROM ItemRelation Ir 
                LEFT JOIN Item I On Ir.Item = I.Code 
                LEFT JOIN Item RI On Ir.RelatedItem = Ri.Code "
        DtItemRelation = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl2.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DtItem As DataTable
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case ColItemCategory
                    mQry = "Select I.Unit, U.ShowDimensionDetailInPurchase, U.DecimalPlaces as QtyDecimalPlaces 
                            From Item I  With (NoLock)
                            Left Join Unit U  With (NoLock) On I.Unit = U.Code 
                            Where I.Code ='" & Dgl1.Item(ColItemCategory, mRowIndex).Tag & "'"
                    DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtItem.Rows.Count > 0 Then
                        Dgl1.Item(ColUnit, mRowIndex).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                        Dgl1.Item(ColUnit, mRowIndex).Tag = AgL.XNull(DtItem.Rows(0)("showdimensiondetailInPurchase"))
                        Dgl1.Item(ColQtyDecimalPlaces, mRowIndex).Value = AgL.VNull(DtItem.Rows(0)("QtyDecimalPlaces"))
                        If Dgl1.Item(ColUnit, mRowIndex).Tag <> "" Then
                            Dgl1.Item(ColDocQty, mRowIndex).Style.ForeColor = Color.Blue
                            Dgl1.Item(ColDocQty, mRowIndex).ReadOnly = True
                        End If
                    End If
                Case ColDimension4
                    If Dgl1.Item(ColUnit, mRowIndex).Tag Then ShowPurchInvoiceDimensionDetail(mSearchCode, Dgl1, Dgl1.CurrentCell.RowIndex, True)
            End Select
            FGeterateSkuName(Dgl1, mRowIndex)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl2_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DtItem As DataTable
        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex
            If Dgl2.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl2.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case ColItem
                    mQry = "Select I.Unit
                            From Item I  With (NoLock)
                            Where I.Code ='" & Dgl2.Item(ColItem, mRowIndex).Tag & "'"
                    DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtItem.Rows.Count > 0 Then
                        Dgl2.Item(ColUnit, mRowIndex).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                    End If
            End Select
            FGeterateSkuName(Dgl2, mRowIndex)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FSave(DocId As String, Conn As Object, Cmd As Object)
        Calculation()

        If FDataValidation(Dgl1) = False Then Exit Sub
        If FDataValidation(Dgl2) = False Then Exit Sub

        If AgL.XNull(DglMain.Item(Col1Value, rowStockIssRecNos).Value) <> "" Then
            mQry = " UPDATE PurchInvoiceDetail Set ReferenceDocId = '" & DocId & "'
                    Where DocId In ('" & DglMain.Item(Col1Value, rowStockIssRecNos).Tag.ToString.Replace(",", "','") & "')"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        ElseIf AgL.XNull(DglMain.Item(Col1Value, rowStockIssRecNos).Tag) <> "" Then
            mQry = " UPDATE PurchInvoiceDetail Set ReferenceDocId = Null
                    Where DocId In ('" & DglMain.Item(Col1Value, rowStockIssRecNos).Tag.ToString.Replace(",", "','") & "')
                    And ReferenceDocId = '" & DocId & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        Dim mMaxSr As Integer = 0

        mMaxSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchInvoiceDetail With (NoLock) 
                Where DocId = '" & DocId & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
        If mMaxSr < 1000 Then mMaxSr = 1000
        FSaveGridWiseData(Dgl1, DocId, mMaxSr, Conn, Cmd)

        mMaxSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchInvoiceDetail With (NoLock)
                Where DocId = '" & DocId & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
        If mMaxSr < 2000 Then mMaxSr = 2000
        FSaveGridWiseData(Dgl2, DocId, mMaxSr, Conn, Cmd)
    End Sub
    Private Sub Calculation()
        Dim I As Integer

        LblTotalQtyForDgl1.Text = 0
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(ColSku, I).Value <> "" And Dgl1.Rows(I).Visible Then
                Dgl1.Item(ColQty, I).Value = Val(Dgl1.Item(ColDocQty, I).Value) - Val(Dgl1.Item(ColLossQty, I).Value)
                LblTotalQtyForDgl1.Text = Val(LblTotalQtyForDgl1.Text) + Val(Dgl1.Item(ColQty, I).Value)
            End If
        Next
        LblTotalQtyForDgl1.Text = Val(LblTotalQtyForDgl1.Text)



        LblTotalQtyForDgl2.Text = 0
        For I = 0 To Dgl2.RowCount - 1
            If Dgl2.Item(ColSku, I).Value <> "" And Dgl2.Rows(I).Visible Then
                Dgl2.Item(ColQty, I).Value = Val(Dgl2.Item(ColDocQty, I).Value) - Val(Dgl2.Item(ColLossQty, I).Value)
                LblTotalQtyForDgl2.Text = Val(LblTotalQtyForDgl2.Text) + Val(Dgl2.Item(ColQty, I).Value)
            End If
        Next
        LblTotalQtyForDgl2.Text = Val(LblTotalQtyForDgl2.Text)
    End Sub
    Private Function FDataValidation(DglControl As AgControls.AgDataGrid) As Boolean
        FDataValidation = False

        Dim I As Integer = 0
        For I = 0 To DglControl.Rows.Count - 1
            If CType(AgL.VNull(ClsMain.FGetSettings(SettingFields.SkuManagementApplicableYN, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, DglControl.Item(ColItemType, I).Tag, "", ItemV_Type.SKU, "", "")), Boolean) = True Then
                If AgL.XNull(DglControl.Item(ColItemCategory, I).Value) <> "" _
                        Or AgL.XNull(DglControl.Item(ColItemGroup, I).Value) <> "" _
                        Or AgL.XNull(DglControl.Item(ColItem, I).Value) <> "" _
                        Or AgL.XNull(DglControl.Item(ColDimension1, I).Value) <> "" _
                        Or AgL.XNull(DglControl.Item(ColDimension2, I).Value) <> "" _
                        Or AgL.XNull(DglControl.Item(ColDimension3, I).Value) <> "" _
                        Or AgL.XNull(DglControl.Item(ColDimension4, I).Value) <> "" _
                        Or AgL.XNull(DglControl.Item(ColSize, I).Value) <> "" _
                        Then
                    DglControl.Item(ColSku, I).Tag = ClsMain.FGetSKUCode(DglControl.Item(ColSNo, I).Value, DglControl.Item(ColItemType, I).Tag, DglControl.Item(ColItemCategory, I).Tag, DglControl.Item(ColItemCategory, I).Value _
                                            , DglControl.Item(ColItemGroup, I).Tag, DglControl.Item(ColItemGroup, I).Value _
                                            , DglControl.Item(ColItem, I).Tag, DglControl.Item(ColItem, I).Value _
                                            , DglControl.Item(ColDimension1, I).Tag, DglControl.Item(ColDimension1, I).Value _
                                            , DglControl.Item(ColDimension2, I).Tag, DglControl.Item(ColDimension2, I).Value _
                                            , DglControl.Item(ColDimension3, I).Tag, DglControl.Item(ColDimension3, I).Value _
                                            , DglControl.Item(ColDimension4, I).Tag, DglControl.Item(ColDimension4, I).Value _
                                            , DglControl.Item(ColSize, I).Tag, DglControl.Item(ColSize, I).Value _
                                            , "", "", "", "", "", "", "", "")
                    If DglControl.Item(ColSku, I).Tag = "" Then
                        FDataValidation = False
                        Exit Function
                    End If

                    If DglControl.Item(ColDocQty, I).Tag IsNot Nothing Then
                        If CType(DglControl.Item(ColDocQty, I).Tag, FrmPurchaseInvoiceStockIssRecDimension).FData_Validation() = False Then
                            Exit Function
                        End If
                    End If
                End If
            Else
                DglControl.Item(ColSku, I).Tag = DglControl.Item(ColItem, I).Tag
            End If
        Next

        FDataValidation = True
    End Function
    Private Sub FGeterateSkuName(DglControl As AgControls.AgDataGrid, bRowIndex As Integer)
        If DglControl.Item(ColItemCategory, bRowIndex).Value <> "" Or
                DglControl.Item(ColItemGroup, bRowIndex).Value <> "" Or
                DglControl.Item(ColItem, bRowIndex).Value <> "" Or
                DglControl.Item(ColDimension1, bRowIndex).Value <> "" Or
                DglControl.Item(ColDimension2, bRowIndex).Value <> "" Or
                DglControl.Item(ColDimension3, bRowIndex).Value <> "" Or
                DglControl.Item(ColDimension4, bRowIndex).Value <> "" Or
                DglControl.Item(ColSize, bRowIndex).Value <> "" Then
            DglControl.Item(ColSku, bRowIndex).Value = DglControl.Item(ColItemCategory, bRowIndex).Value + " " +
                                    DglControl.Item(ColItemGroup, bRowIndex).Value + " " +
                                    DglControl.Item(ColItem, bRowIndex).Value + " " +
                                    DglControl.Item(ColDimension1, bRowIndex).Value + " " +
                                    DglControl.Item(ColDimension2, bRowIndex).Value + " " +
                                    DglControl.Item(ColDimension3, bRowIndex).Value + " " +
                                    DglControl.Item(ColDimension4, bRowIndex).Value + " " +
                                    DglControl.Item(ColSize, bRowIndex).Value

            If DglControl.Item(ColItem, bRowIndex).Tag <> "" And
                       DglControl.Item(ColDimension1, bRowIndex).Tag = "" And
                       DglControl.Item(ColDimension2, bRowIndex).Tag = "" And
                       DglControl.Item(ColDimension3, bRowIndex).Tag = "" And
                       DglControl.Item(ColDimension4, bRowIndex).Tag = "" And
                       DglControl.Item(ColSize, bRowIndex).Tag = "" Then
                DglControl.Item(ColSku, bRowIndex).Tag = DglControl.Item(ColItem, bRowIndex).Tag
            Else
                Dim DrSKU As DataRow() = AgL.PubDtItem.Select(" IsNull(ItemCategory,'') = '" & DglControl.Item(ColItemCategory, bRowIndex).Tag & "'
                                    And IsNull(ItemGroup,'') = '" & DglControl.Item(ColItemGroup, bRowIndex).Tag & "'
                                    And IsNull(BaseItem,'') = '" & DglControl.Item(ColItem, bRowIndex).Tag & "'
                                    And IsNull(Dimension1,'') = '" & DglControl.Item(ColDimension1, bRowIndex).Tag & "'
                                    And IsNull(Dimension2,'') = '" & DglControl.Item(ColDimension2, bRowIndex).Tag & "'
                                    And IsNull(Dimension3,'') = '" & DglControl.Item(ColDimension3, bRowIndex).Tag & "'
                                    And IsNull(Dimension4,'') = '" & DglControl.Item(ColDimension4, bRowIndex).Tag & "'
                                    And IsNull(Size,'') = '" & DglControl.Item(ColSize, bRowIndex).Tag & "'")
                If DrSKU.Length > 0 Then
                    DglControl.Item(ColSku, bRowIndex).Tag = AgL.XNull(DrSKU(0)("Code"))
                End If
            End If
        Else
            DglControl.Item(ColSku, bRowIndex).Value = ""
        End If
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case ColItemCategory, ColItem
                Dgl1.Item(ColItemType, Dgl1.CurrentCell.RowIndex).Tag = ItemTypeCode.RawProduct
        End Select

        If AgL.VNull(Dgl1.Item(ColIsRecordLocked, Dgl1.CurrentCell.RowIndex).Value) > 0 Then
            Dgl1.CurrentCell.ReadOnly = True
            Exit Sub
        End If
    End Sub
    Private Sub Dgl2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
            Case ColItemCategory, ColItem
                Dgl2.Item(ColItemType, Dgl2.CurrentCell.RowIndex).Tag = ItemTypeCode.OtherRawProduct
        End Select

        If AgL.VNull(Dgl2.Item(ColIsRecordLocked, Dgl2.CurrentCell.RowIndex).Value) > 0 Then
            Dgl2.CurrentCell.ReadOnly = True
            Exit Sub
        End If
    End Sub
    Private Sub FSaveGridWiseData(DglControl As AgControls.AgDataGrid, ByVal DocId As String, MaxSr As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bPurchInvoiceSelectionQry$ = "", bHelpValuesSelectionQry$ = ""

        mDimensionSrl = 0
        mSr = MaxSr
        For I = 0 To DglControl.RowCount - 1
            If DglControl.Item(ColSku, I).Value <> "" And DglControl.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                If DglControl.Item(ColSNo, I).Tag Is Nothing And DglControl.Rows(I).Visible = True Then
                    mSr += 1
                    InsertPurchInvoiceDetail(DglControl, DocId, mSr, I, Conn, Cmd)

                    If DglControl.Item(ColDocQty, I).Tag IsNot Nothing Then
                        CType(DglControl.Item(ColDocQty, I).Tag, FrmPurchaseInvoiceStockIssRecDimension).FSave(DocId, mSr, I, Conn, Cmd)
                    Else
                        mDimensionSrl += 1
                        InsertStock(DglControl, DocId, mSr, mDimensionSrl, I, Conn, Cmd)
                        InsertStockProcess(DglControl, DocId, mSr, mDimensionSrl, I, Conn, Cmd)
                    End If
                Else
                    If DglControl.Rows(I).Visible = True Then
                        UpdatePurchInvoiceDetail(DglControl, DocId, Val(DglControl.Item(ColSNo, I).Tag), I, Conn, Cmd)

                        If DglControl.Item(ColDocQty, I).Tag IsNot Nothing Then
                            CType(DglControl.Item(ColDocQty, I).Tag, FrmPurchaseInvoiceStockIssRecDimension).FSave(DocId, Val(DglControl.Item(ColSNo, I).Tag), I, Conn, Cmd)
                        Else
                            UpdateStock(DglControl, DocId, Val(DglControl.Item(ColSNo, I).Tag), Val(DglControl.Item(ColStockSr, I).Value), I, Conn, Cmd)
                            UpdateStockProcess(DglControl, DocId, Val(DglControl.Item(ColSNo, I).Tag), Val(DglControl.Item(ColStockSr, I).Value), I, Conn, Cmd)
                        End If
                    Else
                        DeleteLineData(DglControl, DocId, Val(DglControl.Item(ColSNo, I).Tag), I, Conn, Cmd)
                    End If
                End If
            End If
        Next

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & DglControl.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglControl)
        End If
    End Sub
    Private Sub InsertPurchInvoiceDetail(DglControl As AgControls.AgDataGrid, DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into PurchInvoiceDetail(DocId, Sr, Item, 
                           Godown,
                           DocQty, LossQty, Qty, Unit, Pcs, UnitMultiplier, DealUnit, DealQty,
                           Rate, Amount, Remark, SubRecordType) "
        mQry += " Values( " & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                        " " & AgL.Chk_Text(DglControl.Item(ColSku, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowGodown).Tag) & ", " &
                        " " & Val(DglControl.Item(ColDocQty, LineGridRowIndex).Value) & ", " &
                        " " & Val(DglControl.Item(ColLossQty, LineGridRowIndex).Value) & ", " &
                        " " & Val(DglControl.Item(ColQty, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(DglControl.Item(ColUnit, LineGridRowIndex).Value) & ", " &
                        " " & Val(DglControl.Item(ColPcs, LineGridRowIndex).Value) & ", " &
                        " " & Val(DglControl.Item(ColUnitMultiplier, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(DglControl.Item(ColDealUnit, LineGridRowIndex).Value) & ", " &
                        " " & Val(DglControl.Item(ColDealQty, LineGridRowIndex).Value) & ", " &
                        " " & Val(DglControl.Item(ColRate, LineGridRowIndex).Value) & ", " &
                        " " & Val(DglControl.Item(ColAmount, LineGridRowIndex).Value) & ",  " &
                        " " & AgL.Chk_Text(DglControl.Item(ColRemark, LineGridRowIndex).Value) & ", " &
                        " '" & mSubRecordType_StockIssue & "' " &
                        " ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into PurchInvoiceDetailSku
                (DocId, Sr, ItemCategory, ItemGroup, Item, Dimension1, 
                Dimension2, Dimension3, Dimension4, Size) "
        mQry += " Values(" & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                " " & AgL.Chk_Text(DglControl.Item(ColItemCategory, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(DglControl.Item(ColItemGroup, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(DglControl.Item(ColItem, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(DglControl.Item(ColDimension1, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(DglControl.Item(ColDimension2, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(DglControl.Item(ColDimension3, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(DglControl.Item(ColDimension4, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(DglControl.Item(ColSize, LineGridRowIndex).Tag) & ")"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub UpdatePurchInvoiceDetail(DglControl As AgControls.AgDataGrid, DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If DglControl.Rows(LineGridRowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
            mQry = " UPDATE PurchInvoiceDetail " &
                    " Set " &
                    " Item = " & AgL.Chk_Text(DglControl.Item(ColSku, LineGridRowIndex).Tag) & ", " &
                    " Godown = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowGodown).Tag) & " ," &
                    " DocQty = " & Val(DglControl.Item(ColDocQty, LineGridRowIndex).Value) & ", " &
                    " LossQty = " & Val(DglControl.Item(ColLossQty, LineGridRowIndex).Value) & ", " &
                    " Qty = " & Val(DglControl.Item(ColQty, LineGridRowIndex).Value) & ", " &
                    " Unit = " & AgL.Chk_Text(DglControl.Item(ColUnit, LineGridRowIndex).Value) & ", " &
                    " Pcs = " & Val(DglControl.Item(ColPcs, LineGridRowIndex).Value) & ", " &
                    " UnitMultiplier = " & Val(DglControl.Item(ColUnitMultiplier, LineGridRowIndex).Value) & ", " &
                    " DealUnit = " & AgL.Chk_Text(DglControl.Item(ColDealUnit, LineGridRowIndex).Value) & ", " &
                    " DealQty = " & Val(DglControl.Item(ColDealQty, LineGridRowIndex).Value) & ", " &
                    " Rate = " & Val(DglControl.Item(ColRate, LineGridRowIndex).Value) & ", " &
                    " Amount = " & Val(DglControl.Item(ColAmount, LineGridRowIndex).Value) & ", " &
                    " Remark = " & AgL.Chk_Text(DglControl.Item(ColRemark, LineGridRowIndex).Value) & " " &
                    " Where DocId = '" & DocID & "' " &
                    " And Sr = " & DglControl.Item(ColSNo, LineGridRowIndex).Tag & " "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Update PurchInvoiceDetailSku " &
                    " SET ItemCategory = " & AgL.Chk_Text(DglControl.Item(ColItemCategory, LineGridRowIndex).Tag) & ", " &
                    " ItemGroup = " & AgL.Chk_Text(DglControl.Item(ColItemGroup, LineGridRowIndex).Tag) & ", " &
                    " Item = " & AgL.Chk_Text(DglControl.Item(ColItem, LineGridRowIndex).Tag) & ", " &
                    " Dimension1 = " & AgL.Chk_Text(DglControl.Item(ColDimension1, LineGridRowIndex).Tag) & ", " &
                    " Dimension2 = " & AgL.Chk_Text(DglControl.Item(ColDimension2, LineGridRowIndex).Tag) & ", " &
                    " Dimension3 = " & AgL.Chk_Text(DglControl.Item(ColDimension3, LineGridRowIndex).Tag) & ", " &
                    " Dimension4 = " & AgL.Chk_Text(DglControl.Item(ColDimension4, LineGridRowIndex).Tag) & ", " &
                    " Size = " & AgL.Chk_Text(DglControl.Item(ColSize, LineGridRowIndex).Tag) & " " &
                    " Where DocId = '" & DocID & "' " &
                    " And Sr = " & DglControl.Item(ColSNo, LineGridRowIndex).Tag & " "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertStock(DglControl As AgControls.AgDataGrid, DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""

        If CType(AgL.VNull(FGetSettings(SettingFields.PostInStockYn, SettingType.General)), Boolean) = True Then
            Dim bQty_Issue As Double = 0
            Dim bQty_Receive As Double = 0

            If mTransNature = NCatNature.Receive Then
                bQty_Issue = 0
                bQty_Receive = Val(DglControl.Item(ColQty, LineGridRowIndex).Value)
            Else
                bQty_Issue = Val(DglControl.Item(ColQty, LineGridRowIndex).Value)
                bQty_Receive = 0
            End If



            mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                SubCode, Process, SalesTaxGroupParty, Item, 
                EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                Rate, Amount, Landed_Value) 
                Values
                (
                    '" & DocID & "', " & TSr & ", " & Sr & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag) & ", 
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.LblPrefix.Text) & ",
                    " & AgL.Chk_Date(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Date).Value) & ", 
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_No).Value) & ", 
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowReferenceNo).Value) & ",  
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtDivision.Tag) & ", 
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowSite_Code).Tag) & ",
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowVendor).Tag) & ", 
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowProcess).Tag) & ", 
                    " & AgL.Chk_Text(bSalesTaxGroupParty) & " , 
                    " & AgL.Chk_Text(DglControl.Item(ColSku, LineGridRowIndex).Tag) & ", 
                    'I', " & Val(bQty_Issue) & "," & Val(bQty_Receive) & ", " & AgL.Chk_Text(DglControl.Item(ColUnit, LineGridRowIndex).Value) & "," & Val(DglControl.Item(ColUnitMultiplier, LineGridRowIndex).Value) & ",
                    " & Val(DglControl.Item(ColDealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(DglControl.Item(ColDealUnit, LineGridRowIndex).Value) & ", " & Val(DglControl.Item(ColRate, LineGridRowIndex).Value) & ", " & Val(DglControl.Item(ColAmount, LineGridRowIndex).Value) & ",0
                ) "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub UpdateStock(DglControl As AgControls.AgDataGrid, DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""
        If CType(AgL.VNull(FGetSettings(SettingFields.PostInStockYn, SettingType.General)), Boolean) = True Then
            Dim bQty_Issue As Double = 0
            Dim bQty_Receive As Double = 0

            If mTransNature = NCatNature.Receive Then
                bQty_Issue = 0
                bQty_Receive = Val(DglControl.Item(ColQty, LineGridRowIndex).Value)
            Else
                bQty_Issue = Val(DglControl.Item(ColQty, LineGridRowIndex).Value)
                bQty_Receive = 0
            End If

            If DglControl.Item(ColStockSr, LineGridRowIndex).Value <> "" Then
                If DglControl.Item(ColStockSr, LineGridRowIndex).Value.ToString.Contains(",") = 0 Then
                    mQry = "Update Stock Set
                        V_Type = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag) & ", 
                        V_Prefix = " & AgL.Chk_Text(mObjFrmPurchInvoice.LblPrefix.Text) & ",
                        V_Date = " & AgL.Chk_Date(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Date).Value) & ", 
                        V_No = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_No).Value) & ", 
                        RecId = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowReferenceNo).Value) & ",  
                        Div_Code = " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtDivision.Tag) & ", 
                        Site_Code = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowSite_Code).Tag) & ",
                        Subcode = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowVendor).Tag) & ", 
                        Process = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowProcess).Tag) & ", 
                        SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ",
                        Item = " & AgL.Chk_Text(DglControl.Item(ColSku, LineGridRowIndex).Tag) & ", 
                        SalesTaxGroupItem = Null, 
                        EType_IR = 'I', 
                        Qty_Iss = " & Val(bQty_Issue) & ",
                        Qty_Rec = " & Val(bQty_Receive) & ",
                        Unit = " & AgL.Chk_Text(DglControl.Item(ColUnit, LineGridRowIndex).Value) & ",
                        UnitMultiplier = " & Val(DglControl.Item(ColUnitMultiplier, LineGridRowIndex).Value) & ",
                        DealQty_Iss = " & Val(DglControl.Item(ColDealQty, LineGridRowIndex).Value) & ", 
                        DealQty_Rec =0,  
                        DealUnit = " & AgL.Chk_Text(DglControl.Item(ColDealUnit, LineGridRowIndex).Value) & ", 
                        Rate = " & Val(DglControl.Item(ColRate, LineGridRowIndex).Value) & ", 
                        Amount = " & Val(DglControl.Item(ColAmount, LineGridRowIndex).Value) & ",
                        Landed_Value = 0
                        Where DocId = '" & DocID & "' and TSr =" & TSr & " And Sr =" & Sr & "
                    "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Else
                mDimensionSrl += 1
                mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                    SubCode, Process, SalesTaxGroupParty, Item, 
                    EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                    Rate, Amount, Landed_Value) 
                    Values
                    (
                        '" & DocID & "', " & TSr & ", " & mDimensionSrl & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.LblPrefix.Text) & ",
                        " & AgL.Chk_Date(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Date).Value) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_No).Value) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowReferenceNo).Value) & ",  
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtDivision.Tag) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowSite_Code).Tag) & ",
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowVendor).Tag) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowProcess).Tag) & ", 
                        " & AgL.Chk_Text(bSalesTaxGroupParty) & " , 
                        " & AgL.Chk_Text(DglControl.Item(ColSku, LineGridRowIndex).Tag) & ", 
                        'I', " & Val(bQty_Issue) & ", " & Val(bQty_Receive) & ", " & AgL.Chk_Text(DglControl.Item(ColUnit, LineGridRowIndex).Value) & "," & Val(DglControl.Item(ColUnitMultiplier, LineGridRowIndex).Value) & ",
                        " & Val(DglControl.Item(ColDealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(DglControl.Item(ColDealUnit, LineGridRowIndex).Value) & ", " & Val(DglControl.Item(ColRate, LineGridRowIndex).Value) & ", " & Val(DglControl.Item(ColAmount, LineGridRowIndex).Value) & ",0
                    )"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub
    Private Sub InsertStockProcess(DglControl As AgControls.AgDataGrid, DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""

        If CType(AgL.VNull(FGetSettings(SettingFields.PostInStockProcessYn, SettingType.General)), Boolean) = True Then
            Dim bQty_Issue As Double = 0
            Dim bQty_Receive As Double = 0

            If mTransNature = NCatNature.Receive Then
                bQty_Issue = Val(DglControl.Item(ColQty, LineGridRowIndex).Value)
                bQty_Receive = 0
            Else
                bQty_Issue = 0
                bQty_Receive = Val(DglControl.Item(ColQty, LineGridRowIndex).Value)
            End If

            mQry = "Insert Into StockProcess(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                SubCode, SalesTaxGroupParty, Item, 
                EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                Rate, Amount, Landed_Value, Process) 
                Values
                (
                    '" & DocID & "', " & TSr & ", " & Sr & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.LblPrefix.Text) & ",
                    " & AgL.Chk_Date(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Date).Value) & ", 
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_No).Value) & ", 
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowReferenceNo).Value) & ",  
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtDivision.Tag) & ", 
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowSite_Code).Tag) & ",
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowVendor).Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & " , 
                    " & AgL.Chk_Text(DglControl.Item(ColSku, LineGridRowIndex).Tag) & ", 
                    'I', " & Val(bQty_Issue) & "," & Val(bQty_Receive) & ", " & AgL.Chk_Text(DglControl.Item(ColUnit, LineGridRowIndex).Value) & "," & Val(DglControl.Item(ColUnitMultiplier, LineGridRowIndex).Value) & ",
                    " & Val(DglControl.Item(ColDealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(DglControl.Item(ColDealUnit, LineGridRowIndex).Value) & ", " & Val(DglControl.Item(ColRate, LineGridRowIndex).Value) & ", " & Val(DglControl.Item(ColAmount, LineGridRowIndex).Value) & ",0,
                    " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowProcess).Tag) & "
                ) "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub UpdateStockProcess(DglControl As AgControls.AgDataGrid, DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""
        If CType(AgL.VNull(FGetSettings(SettingFields.PostInStockProcessYn, SettingType.General)), Boolean) = True Then
            Dim bQty_Issue As Double = 0
            Dim bQty_Receive As Double = 0

            If mTransNature = NCatNature.Receive Then
                bQty_Issue = Val(DglControl.Item(ColQty, LineGridRowIndex).Value)
                bQty_Receive = 0
            Else
                bQty_Issue = 0
                bQty_Receive = Val(DglControl.Item(ColQty, LineGridRowIndex).Value)
            End If

            If DglControl.Item(ColStockSr, LineGridRowIndex).Value <> "" Then
                If DglControl.Item(ColStockSr, LineGridRowIndex).Value.ToString.Contains(",") = 0 Then
                    mQry = "Update StockProcess Set
                        V_Type = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag) & ", 
                        V_Prefix = " & AgL.Chk_Text(mObjFrmPurchInvoice.LblPrefix.Text) & ",
                        V_Date = " & AgL.Chk_Date(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Date).Value) & ", 
                        V_No = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_No).Value) & ", 
                        RecId = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowReferenceNo).Value) & ",  
                        Div_Code = " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtDivision.Tag) & ", 
                        Site_Code = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowSite_Code).Tag) & ",
                        Subcode = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowVendor).Tag) & ", 
                        Process = " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowProcess).Tag) & ", 
                        SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ",
                        Item = " & AgL.Chk_Text(DglControl.Item(ColSku, LineGridRowIndex).Tag) & ", 
                        SalesTaxGroupItem = Null, 
                        EType_IR = 'I', 
                        Qty_Iss = " & Val(bQty_Issue) & ",
                        Qty_Rec = " & Val(bQty_Receive) & ",
                        Unit = " & AgL.Chk_Text(DglControl.Item(ColUnit, LineGridRowIndex).Value) & ",
                        UnitMultiplier = " & Val(DglControl.Item(ColUnitMultiplier, LineGridRowIndex).Value) & ",
                        DealQty_Iss = " & Val(DglControl.Item(ColDealQty, LineGridRowIndex).Value) & ", 
                        DealQty_Rec =0,  
                        DealUnit = " & AgL.Chk_Text(DglControl.Item(ColDealUnit, LineGridRowIndex).Value) & ", 
                        Rate = " & Val(DglControl.Item(ColRate, LineGridRowIndex).Value) & ", 
                        Amount = " & Val(DglControl.Item(ColAmount, LineGridRowIndex).Value) & ",
                        Landed_Value = 0
                        Where DocId = '" & DocID & "' and TSr =" & TSr & " And Sr =" & Sr & " "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Else
                mDimensionSrl += 1
                mQry = "Insert Into StockProcess(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                    SubCode, SalesTaxGroupParty, Item, 
                    EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                    Rate, Amount, Landed_Value, Process) 
                    Values
                    (
                        '" & DocID & "', " & TSr & ", " & mDimensionSrl & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.LblPrefix.Text) & ",
                        " & AgL.Chk_Date(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Date).Value) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_No).Value) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowReferenceNo).Value) & ",  " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtDivision.Tag) & ", 
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowSite_Code).Tag) & ",
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowVendor).Tag) & ", 
                        " & AgL.Chk_Text(bSalesTaxGroupParty) & " , 
                        " & AgL.Chk_Text(DglControl.Item(ColSku, LineGridRowIndex).Tag) & ", 
                        'I', " & Val(bQty_Issue) & "," & Val(bQty_Receive) & ", " & AgL.Chk_Text(DglControl.Item(ColUnit, LineGridRowIndex).Value) & ",
                        " & Val(DglControl.Item(ColUnitMultiplier, LineGridRowIndex).Value) & ",
                        " & Val(DglControl.Item(ColDealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(DglControl.Item(ColDealUnit, LineGridRowIndex).Value) & ", 
                        " & Val(DglControl.Item(ColRate, LineGridRowIndex).Value) & ", " & Val(DglControl.Item(ColAmount, LineGridRowIndex).Value) & ",0,
                        " & AgL.Chk_Text(mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowProcess).Tag) & "
                    )"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub
    Private Sub DeleteLineData(DglControl As AgControls.AgDataGrid, DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Val(DglControl.Item(ColSNo, LineGridRowIndex).Tag) > 0 Then
            'mQry = " Delete From PurchInvoiceDetailBase Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From PurchInvoiceDetailSku Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From PurchInvoiceDetail Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From Stock Where DocId = '" & DocID & "' And TSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From StockProcess Where DocId = '" & DocID & "' And TSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown, Dgl2.KeyDown
        If Dgl1.CurrentCell IsNot Nothing Then
            If e.Control And e.KeyCode = Keys.D And Dgl1.Rows(Dgl1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                sender.CurrentRow.Visible = False
                Calculation()
            End If

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case ColDocQty
                    If e.KeyCode = Keys.Space Then ShowPurchInvoiceDimensionDetail(mSearchCode, Dgl1, Dgl1.CurrentCell.RowIndex)
            End Select
        End If
    End Sub
    Private Sub DglMain_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.KeyDown
        Dim mRow As Integer
        Dim mColumn As Integer

        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub

            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex

            Select Case mRow
                Case rowStockIssRecNos
                    If ClsMain.IsSpecialKeyPressed(e) = False Then
                        If e.KeyCode = Keys.Delete Then
                            DglMain.Item(Col1Value, rowStockIssRecNos).Value = ""
                        ElseIf e.KeyCode <> Keys.Enter Then
                            FHPGD_StockIssRec(DglMain.Item(Col1Value, rowStockIssRecNos).Tag, DglMain.Item(Col1Value, rowStockIssRecNos).Value)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FHPGD_StockIssRec(ByRef Code As String, ByRef Description As String) As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = "SELECT 'o' As Tick, H.DocID, H.V_Type + '-' +  H.ManualRefNo AS StockIssNo, H.V_Date As Date
                FROM PurchInvoice H 
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN (Select L.DocId, Count(*) As Cnt
                        From PurchInvoiceDetail L 
                        Where L.ReferenceDocId Is Not Null
                        Group By L.DocId
                ) As VLine On H.DocId = VLine.DocId
                WHERE H.Vendor = '" & mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowVendor).Tag & "'
                And Vt.NCat = '" & Ncat.StockIssue & "' 
                And IsNull(VLine.Cnt,0) = 0 "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 300, 330, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Stock Iss No", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Stock Iss Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            Code = FRH_Multiple.FFetchData(1, "", "", ",")
            Description = FRH_Multiple.FFetchData(2, "", "", ",")
        Else
            Code = ""
            Description = ""
        End If

        FFillRefenceStockIssueDocIdData(Code, Dgl1, ItemTypeCode.RawProduct)
        FFillRefenceStockIssueDocIdData(Code, Dgl2, ItemTypeCode.OtherRawProduct)

        FRH_Multiple = Nothing
    End Function
    Private Sub FFillRefenceStockIssueDocIdData(DocIdStr As String, DglControl As AgControls.AgDataGrid, ItemType As String)
        Dim DsMain As DataSet
        Dim mRow As Integer = 0
        Dim I As Integer = 0
        Dim mQryStockSr As String = ""

        If AgL.PubServerName = "" Then
            mQryStockSr = "Select   (Sr ,',') from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr"
        Else
            mQryStockSr = "Select  Cast(Sr as Varchar) + ',' from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr for xml path('')"
        End If

        mQry = "Select L.*, H.V_Type || '-' || H.ManualRefNo As StockIssueNo, H.V_Date As StockIssueDate, 
                    Barcode.Description as BarcodeName, 
                    I.Description As ItemDesc, I.ManualCode, 
                    U.ShowDimensionDetailInSales, U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, U.ShowDimensionDetailInPurchase,
                    MU.DecimalPlaces As DealUnitDecimalPlaces,
                    Sku.Code As SkuCode, Sku.Description As SkuDescription, 
                    It.Code As ItemType, It.Name As ItemTypeDesc,
                    IG.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, 
                    Sids.Item As ItemCode, Sids.ItemCategory, Sids.ItemGroup, 
                    Sids.Dimension1, Sids.Dimension2, 
                    Sids.Dimension3, Sids.Dimension4, Sids.Size, 
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                    I.ItemCategory as MItemCategory, I.ItemGroup as MItemGroup, I.Specification as MItemSpecification, 
                    I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MSize, 
                    Godown.Name as GodownName, ISt.Description as ItemStateName, RawMaterial.Description As RawMaterialDesc, 
                    (" & mQryStockSr & ") as StockSr 
                    From (Select * From PurchInvoiceDetail  With (NoLock)  
                                Where DocId In ('" & DocIdStr & "')) As L 
                    LEFT JOIN PurchInvoiceDetailSku Sids With (NoLock) On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                    LEFT JOIN PurchInvoice H On L.DocId = H.DocId
                    LEFT JOIN Item Sku ON Sku.Code = L.Item
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    Left Join Item IC On Sids.ItemCategory = IC.Code
                    Left Join Item IG On Sids.ItemGroup = IG.Code
                    LEFT JOIN Item I ON Sids.Item = I.Code
                    LEFT JOIN Item D1 ON Sids.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sids.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sids.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sids.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sids.Size = Size.Code
                    LEFT JOIN Item Ist On L.ItemState = Ist.Code
                    LEFT JOIN Barcode  With (NoLock) On L.Barcode = Barcode.Code
                    LEFT JOIN SubGroup G On L.Godown = G.SubCode
                    Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                    Left Join Unit MU  With (NoLock) On L.DealUnit = MU.Code 
                    Left Join Subgroup Godown On L.Godown = Godown.Subcode
                    LEFT JOIN Item RawMaterial ON L.RawMaterial = RawMaterial.Code
                    Where Sku.ItemType = '" & ItemType & "'
                    Order By L.Sr "
        DsMain = AgL.FillData(mQry, AgL.GCn)

        If DsMain.Tables(0).Rows.Count = 0 Then Exit Sub

        mRow = DglControl.Rows.Count - 1


        If DglControl.Rows(mRow).IsNewRow = False Then
            DglControl.Rows.Remove(DglControl.Rows(mRow))
        End If
        DglControl.Rows.Insert(mRow, DsMain.Tables(0).Rows.Count)

        With DsMain.Tables(0)
            For I = 0 To DsMain.Tables(0).Rows.Count - 1
                DglControl.Item(ColSNo, mRow + I).Value = DglControl.Rows.Count - 1
                DglControl.Item(ColSNo, mRow + I).Tag = AgL.XNull(.Rows(I)("Sr"))

                DglControl.Item(ColStockSr, mRow + I).Value = AgL.XNull(.Rows(I)("StockSr"))
                If DglControl.Item(ColStockSr, mRow + I).Value <> "" Then
                    If DglControl.Item(ColStockSr, mRow + I).Value.ToString.Substring(DglControl.Item(ColStockSr, mRow + I).Value.ToString.Length - 1, 1) = "," Then
                        DglControl.Item(ColStockSr, mRow + I).Value = DglControl.Item(ColStockSr, mRow + I).Value.ToString.Substring(0, DglControl.Item(ColStockSr, mRow + I).Value.ToString.Length - 1)
                    End If
                End If

                DglControl.Item(ColSku, mRow + I).Tag = AgL.XNull(.Rows(I)("SkuCode"))
                DglControl.Item(ColSku, mRow + I).Value = AgL.XNull(.Rows(I)("SkuDescription"))

                DglControl.Item(ColItemType, mRow + I).Tag = AgL.XNull(.Rows(I)("ItemType"))
                DglControl.Item(ColItemType, mRow + I).Value = AgL.XNull(.Rows(I)("ItemTypeDesc"))

                DglControl.Item(ColItemCategory, mRow + I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                DglControl.Item(ColItemCategory, mRow + I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))

                DglControl.Item(ColItemGroup, mRow + I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                DglControl.Item(ColItemGroup, mRow + I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))

                DglControl.Item(ColItem, mRow + I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                DglControl.Item(ColItem, mRow + I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                DglControl.Item(ColDimension1, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                DglControl.Item(ColDimension1, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                DglControl.Item(ColDimension2, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                DglControl.Item(ColDimension2, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                DglControl.Item(ColDimension3, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                DglControl.Item(ColDimension3, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))

                DglControl.Item(ColDimension4, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                DglControl.Item(ColDimension4, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))

                DglControl.Item(ColSize, mRow + I).Tag = AgL.XNull(.Rows(I)("Size"))
                DglControl.Item(ColSize, mRow + I).Value = AgL.XNull(.Rows(I)("SizeDesc"))

                DglControl.Item(ColDocQty, mRow + I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("DocQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                DglControl.Item(ColQty, mRow + I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                DglControl.Item(ColUnit, mRow + I).Value = AgL.XNull(.Rows(I)("Unit"))

                DglControl.Item(ColUnit, mRow + I).Tag = AgL.VNull(.Rows(I)("ShowDimensionDetailInSales"))

                DglControl.Item(ColQtyDecimalPlaces, mRow + I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))


                If AgL.VNull(DglControl.Item(ColUnit, mRow + I).Tag) Then
                    DglControl.Item(ColDocQty, mRow + I).Style.ForeColor = Color.Blue
                    DglControl.Item(ColDocQty, mRow + I).ReadOnly = True
                    ShowPurchInvoiceDimensionDetail(AgL.XNull(.Rows(I)("DocId")), DglControl, mRow + I, False)
                End If

                DglControl.Item(ColDealUnitDecimalPlaces, mRow + I).Value = AgL.VNull(.Rows(I)("DealUnitDecimalPlaces"))
                DglControl.Item(ColUnitMultiplier, mRow + I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                DglControl.Item(ColDealUnit, mRow + I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                DglControl.Item(ColDealQty, mRow + I).Value = Format(AgL.VNull(.Rows(I)("DealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                DglControl.Item(ColRate, mRow + I).Value = AgL.VNull(.Rows(I)("Rate"))
                DglControl.Item(ColAmount, mRow + I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")


                DglControl.Item(ColIsRecordLocked, mRow + I).Value = 1
                If DglControl.Item(ColIsRecordLocked, mRow + I).Value <> 0 Then DglControl.Rows(mRow + I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : DglControl.Rows(I).ReadOnly = True

                DglControl.Item(ColStockIssueNo, mRow + I).Value = AgL.XNull(.Rows(I)("StockIssueNo"))
                DglControl.Item(ColStockIssueDate, mRow + I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("StockIssueDate")))

                If DglControl.Name = Dgl1.Name Then
                    LblTotalQtyForDgl1.Text = Val(LblTotalQtyForDgl1.Text) + Val(DglControl.Item(ColQty, mRow + I).Value)
                ElseIf DglControl.Name = Dgl2.Name Then
                    LblTotalQtyForDgl2.Text = Val(LblTotalQtyForDgl2.Text) + Val(DglControl.Item(ColQty, mRow + I).Value)
                End If
            Next I
        End With
    End Sub
    Private Sub DglMain_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex

            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case mRow
                Case rowStockIssRecNos
                    DglMain.Item(Col1Value, rowStockIssRecNos).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex
            Select Case mRow
                Case rowGodown
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Godown & "' Order By Name"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select

            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                If DglMain.CurrentCell.RowIndex = LastCell.RowIndex And DglMain.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                    If Dgl1.Visible Then
                        Dgl1.CurrentCell = Dgl1.FirstDisplayedCell
                        Dgl1.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ShowPurchInvoiceDimensionDetail(ByVal SearchCode As String, DglControl As AgControls.AgDataGrid, mRow As Integer, Optional IsShowFrm As Boolean = True)
        If mRow < 0 Then Exit Sub
        If Dgl1.Item(ColDocQty, mRow).Tag IsNot Nothing Then
            CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).EntryMode = mEntryMode
            CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).objFrmPurchInvoice = objFrmPurchInvoice
            CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).objFrmPurchaseInvoiceStockIssRec = Me
            CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).objLineGrid = Dgl1
            CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).DglRow = Dgl1.Rows(mRow)
            CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).FReInitializeDimensionColumns()

            If IsShowFrm = True Then
                Dgl1.Item(ColDocQty, mRow).Tag.ShowDialog()
                Dgl1.Item(ColDocQty, mRow).Value = CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).GetTotalQty
                Dgl1.Item(ColQty, mRow).Value = CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).GetTotalQty
            End If
        Else
            If Dgl1.Item(ColUnit, mRow).Tag Then
                Dim FrmObj As FrmPurchaseInvoiceStockIssRecDimension
                FrmObj = New FrmPurchaseInvoiceStockIssRecDimension
                FrmObj.ItemName = Dgl1.Item(ColItem, mRow).Value
                FrmObj.Unit = Dgl1.Item(ColUnit, mRow).Value
                FrmObj.UnitDecimalPlace = Val(Dgl1.Item(ColQtyDecimalPlaces, mRow).Value)
                FrmObj.DglRow = Dgl1.Rows(mRow)
                FrmObj.EntryMode = mEntryMode
                FrmObj.objFrmPurchInvoice = objFrmPurchInvoice
                FrmObj.objFrmPurchaseInvoiceStockIssRec = Me
                FrmObj.objLineGrid = Dgl1
                FrmObj.IniGrid(SearchCode, Val(Dgl1.Item(ColSNo, mRow).Tag))
                FrmObj.FReInitializeDimensionColumns()
                Dgl1.Item(ColDocQty, mRow).Tag = FrmObj

                If IsShowFrm = True Then
                    Dgl1.Item(ColDocQty, mRow).Tag.ShowDialog()
                    Dgl1.Item(ColDocQty, mRow).Value = CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).GetTotalQty
                    Dgl1.Item(ColQty, mRow).Value = CType(Dgl1.Item(ColDocQty, mRow).Tag, FrmPurchaseInvoiceStockIssRecDimension).GetTotalQty
                End If
            End If
        End If
        Calculation()
    End Sub
    Private Sub Dgl1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        Dim mRow As Integer
        mRow = e.RowIndex
        If Dgl1.Columns(e.ColumnIndex).Name = ColDocQty Then ShowPurchInvoiceDimensionDetail(mSearchCode, Dgl1, mRow)
    End Sub
    Private Sub DglMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellContentClick
        If e.ColumnIndex = DglMain.Columns(Col1Value).Index And TypeOf (DglMain(Col1Value, e.RowIndex)) Is DataGridViewButtonCell Then
            Select Case e.RowIndex
                Case rowBtnStandardConsumption
                    If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                        FFillStandardConsumption_Garment()
                    End If
            End Select
        End If
    End Sub
    Private Sub FFillStandardConsumption_Garment()
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim DsTemp As DataSet
        Dim DtLine As DataTable

        mQry = "SELECT Max(I.Description) As ItemDesc, 
                Max(U.DecimalPlaces) AS DecimalPlaces, Max(U.DecimalPlaces) As QtyDecimalPlaces, 
                Max(Sku.Code) As SkuCode, Max(Sku.Description) As SkuDescription, 
                Max(It.Code) As ItemType, Max(It.Name) As ItemTypeDesc,
                Max(IG.Description) As ItemGroupDesc, Max(IC.Description) As ItemCategoryDesc, 
                Pids.Item As ItemCode, Pids.ItemCategory, Pids.ItemGroup, 
                Pids.Dimension1, Max(Pids.Dimension2) AS Dimension2, 
                Pids.Dimension3, Pids.Dimension4, Pids.Size, 
                Max(D1.Description) as Dimension1Desc, Max(D2.Description) as Dimension2Desc,
                Max(D3.Description) as Dimension3Desc, Max(D4.Description) as Dimension4Desc, Max(Size.Description) as SizeDesc,
                Sum(L.Qty) AS Qty, Max(L.Unit) As Unit, Max(Cast(U.ShowDimensionDetailInSales As BIGINT)) As ShowDimensionDetailInSales 
                FROM (Select * From PurchInvoiceDetailBom Where DocId = '" & mSearchCode & "' And IsNull(ConsiderInIssueYN,1) <> 0) As L 
                LEFT JOIN PurchInvoiceDetailBomSku Pids ON L.DocID = Pids.DocID AND L.TSr = Pids.TSr AND L.Sr = Pids.Sr
                LEFT JOIN Item Sku ON Sku.Code = L.Item
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                Left Join Item IC On Pids.ItemCategory = IC.Code
                Left Join Item IG On Pids.ItemGroup = IG.Code
                LEFT JOIN Item I ON Pids.Item = I.Code
                LEFT JOIN Item D1 ON Pids.Dimension1 = D1.Code
                LEFT JOIN Item D2 ON Pids.Dimension2 = D2.Code
                LEFT JOIN Item D3 ON Pids.Dimension3 = D3.Code
                LEFT JOIN Item D4 ON Pids.Dimension4 = D4.Code
                LEFT JOIN Item Size ON Pids.Size = Size.Code
                Left Join Unit U With (NoLock) On L.Unit = U.Code 
                GROUP BY Pids.ItemCategory, Pids.ItemGroup, Pids.Item, Pids.Dimension1, Pids.Dimension3, Pids.Dimension4, Pids.Size"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        Dim mRow As Integer = 0

        If Dgl1.Rows.Count > 1 Then
            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(ColIsRecordLocked, I).Value = 0 Then
                    If Not Dgl1.Rows(I).IsNewRow Then
                        Dgl1.Rows(I).Visible = False
                    End If
                End If
            Next
            mRow = Dgl1.Rows.Count - 1
        End If


        With DsTemp.Tables(0)

            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, mRow + I).Value = I + 1

                    Dgl1.Item(ColItemCategory, mRow + I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(ColItemCategory, mRow + I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))

                    Dgl1.Item(ColItem, mRow + I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                    Dgl1.Item(ColItem, mRow + I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                    Dgl1.Item(ColDimension1, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                    Dgl1.Item(ColDimension1, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                    Dgl1.Item(ColDimension3, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl1.Item(ColDimension3, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))

                    Dgl1.Item(ColDimension4, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                    Dgl1.Item(ColDimension4, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))

                    Dgl1.Item(ColDocQty, mRow + I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl1.Item(ColQty, mRow + I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl1.Item(ColUnit, mRow + I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(ColUnit, mRow + I).Tag = AgL.VNull(.Rows(I)("ShowDimensionDetailInSales"))

                    Dgl1.Item(ColQtyDecimalPlaces, mRow + I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                    If AgL.XNull(.Rows(I)("Dimension4")) <> "" Then
                        mQry = "SELECT Max(I.Description) As ItemDesc, 
                            Max(U.DecimalPlaces) AS DecimalPlaces, Max(U.DecimalPlaces) As QtyDecimalPlaces, 
                            Max(Sku.Code) As SkuCode, Max(Sku.Description) As SkuDescription, 
                            Max(It.Code) As ItemType, Max(It.Name) As ItemTypeDesc,
                            Max(IG.Description) As ItemGroupDesc, Max(IC.Description) As ItemCategoryDesc, 
                            Pids.Item As ItemCode, Pids.ItemCategory, Pids.ItemGroup, 
                            Pids.Dimension1, Pids.Dimension2 AS Dimension2, 
                            Pids.Dimension3, Pids.Dimension4, Pids.Size, 
                            Max(D1.Description) as Dimension1Desc, Max(D2.Description) as Dimension2Desc,
                            Max(D3.Description) as Dimension3Desc, Max(D4.Description) as Dimension4Desc, Max(Size.Description) as SizeDesc,
                            Sum(L.Qty) AS Qty, Max(L.Unit) As Unit
                            FROM (Select * From PurchInvoiceDetailBom Where DocId = '" & mSearchCode & "' And IsNull(ConsiderInIssueYN,1) <> 0) As L 
                            LEFT JOIN PurchInvoiceDetailBomSku Pids ON L.DocID = Pids.DocID AND L.TSr = Pids.TSr AND L.Sr = Pids.Sr
                            LEFT JOIN Item Sku ON Sku.Code = L.Item
                            LEFT JOIN ItemType It On Sku.ItemType = It.Code
                            Left Join Item IC On Pids.ItemCategory = IC.Code
                            Left Join Item IG On Pids.ItemGroup = IG.Code
                            LEFT JOIN Item I ON Pids.Item = I.Code
                            LEFT JOIN Item D1 ON Pids.Dimension1 = D1.Code
                            LEFT JOIN Item D2 ON Pids.Dimension2 = D2.Code
                            LEFT JOIN Item D3 ON Pids.Dimension3 = D3.Code
                            LEFT JOIN Item D4 ON Pids.Dimension4 = D4.Code
                            LEFT JOIN Item Size ON Pids.Size = Size.Code
                            Left Join Unit U With (NoLock) On L.Unit = U.Code 
                            Where IsNull(Pids.ItemCategory,'') = '" & Dgl1.Item(ColItemCategory, mRow + I).Tag & "'
                            And IsNull(Pids.ItemGroup,'') = '" & Dgl1.Item(ColItemGroup, mRow + I).Tag & "'
                            And IsNull(Pids.Dimension1,'') = '" & Dgl1.Item(ColDimension1, mRow + I).Tag & "'
                            And IsNull(Pids.Dimension3,'') = '" & Dgl1.Item(ColDimension3, mRow + I).Tag & "'
                            And IsNull(Pids.Dimension4,'') = '" & Dgl1.Item(ColDimension4, mRow + I).Tag & "'
                            GROUP BY Pids.ItemCategory, Pids.ItemGroup, Pids.Item, Pids.Dimension1, Pids.Dimension2, Pids.Dimension3, Pids.Dimension4, Pids.Size"
                        DtLine = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        If DtLine.Rows.Count > 0 Then
                            Dim FrmObj As FrmPurchaseInvoiceStockIssRecDimension
                            FrmObj = New FrmPurchaseInvoiceStockIssRecDimension
                            FrmObj.ItemName = Dgl1.Item(ColItem, mRow + I).Value
                            FrmObj.Unit = Dgl1.Item(ColUnit, mRow + I).Value
                            FrmObj.UnitDecimalPlace = Val(Dgl1.Item(ColQtyDecimalPlaces, mRow + I).Value)
                            FrmObj.DglRow = Dgl1.Rows(mRow + I)
                            FrmObj.EntryMode = mEntryMode
                            FrmObj.objFrmPurchaseInvoiceStockIssRec = Me
                            FrmObj.objFrmPurchInvoice = objFrmPurchInvoice
                            FrmObj.IniGrid(mSearchCode, Val(Dgl1.Item(ColSNo, mRow + I).Tag))
                            FrmObj.FReInitializeDimensionColumns()
                            Dgl1.Item(ColDocQty, mRow + I).Tag = FrmObj
                            Dgl1.Item(ColDocQty, mRow + I).ReadOnly = True
                            Dgl1.Item(ColDocQty, mRow + I).Style.ForeColor = Color.Blue

                            For J = 0 To DtLine.Rows.Count - 1
                                FrmObj.Dgl1.Rows.Add()
                                FrmObj.Dgl1.Item(FrmPurchaseInvoiceStockIssRecDimension.ColSNo, J).Value = FrmObj.Dgl1.Rows.Count - 1
                                FrmObj.Dgl1.Item(FrmPurchaseInvoiceStockIssRecDimension.Col1Dimension2, J).Tag = AgL.XNull(DtLine.Rows(J)("Dimension2"))
                                FrmObj.Dgl1.Item(FrmPurchaseInvoiceStockIssRecDimension.Col1Dimension2, J).Value = AgL.XNull(DtLine.Rows(J)("Dimension2Desc"))
                                FrmObj.Dgl1.Item(FrmPurchaseInvoiceStockIssRecDimension.Col1Pcs, J).Value = 1
                                FrmObj.Dgl1.Item(FrmPurchaseInvoiceStockIssRecDimension.Col1Qty, J).Value = AgL.VNull(DtLine.Rows(J)("Qty"))
                                FrmObj.Dgl1.Item(FrmPurchaseInvoiceStockIssRecDimension.Col1TotalQty, J).Value = AgL.VNull(DtLine.Rows(J)("Qty"))
                                FrmObj.Calculation()
                            Next
                        End If
                    End If


                    FGeterateSkuName(Dgl1, mRow + I)
                Next I
            End If
        End With
        Calculation()
    End Sub
End Class