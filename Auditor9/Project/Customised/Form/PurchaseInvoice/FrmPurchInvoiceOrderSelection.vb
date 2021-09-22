Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPurchInvoiceOrderSelection
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Public Const ColSNo As String = "S.No."
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"

    Public Const rowOrderNo As Integer = 0
    Public Const rowItemCategory As Integer = 1
    Public Const rowItemGroup As Integer = 2
    Public Const rowItem As Integer = 3
    Public Const rowDimension1 As Integer = 4
    Public Const rowDimension2 As Integer = 5
    Public Const rowDimension3 As Integer = 6
    Public Const rowDimension4 As Integer = 7
    Public Const rowSize As Integer = 8
    Public Const rowBtnOrderBalance As Integer = 9

    Public Const HcOrderNo As String = "Order No"
    Public Const HcItemCategory As String = "Item Category"
    Public Const HcItemGroup As String = "Item Group"
    Public Const HcItem As String = "Item"
    Public Const HcDimension1 As String = "Dimension1"
    Public Const HcDimension2 As String = "Dimension2"
    Public Const HcDimension3 As String = "Dimension3"
    Public Const HcDimension4 As String = "Dimension4"
    Public Const HcSize As String = "Size"
    Public Const HcBtnOrderBalance As String = "Order Balance"

    Public Const Col1ReferenceDocId As String = "Order No"
    Public Const Col1ReferenceDocIdTSr As String = "Reference TSr"
    Public Const Col1ReferenceDocIdSr As String = "Reference Sr"
    Public Const Col1ReferenceDocIdDate As String = "Order Date"
    Public Col1ItemType As String = AgL.PubCaptionItemType
    Public Const Col1Sku As String = "Sku"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1BalanceQty As String = "Balance Qty"
    Public Const Col1ReceiveQty As String = "Receive Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1RawMaterial As String = "Raw Material"



    Dim mSearchcode As String
    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mPartyCode As String
    Dim mProcessCode As String
    Dim mV_Type As String = ""
    Dim mV_Date As String = ""
    Dim mDglMainLastRowIndex As Integer
    Dim mCopyToSearchCodesArr As String()
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property
    Public Property PartyCode() As String
        Get
            PartyCode = mPartyCode
        End Get
        Set(ByVal value As String)
            mPartyCode = value
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
    Public Property V_Date() As String
        Get
            V_Date = mV_Date
        End Get
        Set(ByVal value As String)
            mV_Date = value
        End Set
    End Property
    Public Property ProcessCode() As String
        Get
            ProcessCode = mProcessCode
        End Get
        Set(ByVal value As String)
            mProcessCode = value
        End Set
    End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub IniGrid(SearchCode As String)
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocId, 70, 0, Col1ReferenceDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocIdTSr, 40, 5, Col1ReferenceDocIdTSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocIdSr, 40, 5, Col1ReferenceDocIdSr, False, True, False)
            .AddAgDateColumn(Dgl1, Col1ReferenceDocIdDate, 110, Col1ReferenceDocIdDate, True, True)
            .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, Col1ItemType, False, True)
            .AddAgTextColumn(Dgl1, Col1Sku, 100, 0, Col1Sku, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, False, True)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, False, True)
            .AddAgTextColumn(Dgl1, Col1RawMaterial, 100, 0, Col1RawMaterial, False, True)
            .AddAgNumberColumn(Dgl1, Col1BalanceQty, 70, 8, 4, False, Col1BalanceQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReceiveQty, 70, 8, 4, False, Col1ReceiveQty, False, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, False, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl2)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)




        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 360, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 260, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 480, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(DglMain, Pnl1)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.Name = "DglMain"
        DglMain.Tag = "VerticalGrid"

        DglMain.Rows.Add(10)
        DglMain.Item(Col1Head, rowOrderNo).Value = HcOrderNo
        DglMain.Item(Col1Head, rowItemCategory).Value = HcItemCategory
        DglMain.Item(Col1Head, rowItemGroup).Value = HcItemGroup
        DglMain.Item(Col1Head, rowItem).Value = HcItem
        DglMain.Item(Col1Head, rowItemCategory).Value = HcItemCategory
        DglMain.Item(Col1Head, rowDimension1).Value = HcDimension1
        DglMain.Item(Col1Head, rowDimension2).Value = HcDimension2
        DglMain.Item(Col1Head, rowDimension3).Value = HcDimension3
        DglMain.Item(Col1Head, rowDimension4).Value = HcDimension4
        DglMain.Item(Col1Head, rowSize).Value = HcSize
        DglMain.Item(Col1Head, rowBtnOrderBalance).Value = HcBtnOrderBalance
        DglMain.Item(Col1Value, rowBtnOrderBalance) = New DataGridViewButtonCell
        For I As Integer = 0 To DglMain.Rows.Count - 1
            If AgL.XNull(DglMain(Col1HeadOriginal, I).Value) = "" Then
                DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
            End If
        Next

        AgL.FSetDimensionCaptionForVerticalGrid(DglMain, AgL)

        'ApplyUISetting()

        FMoveRec(SearchCode)
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            mOkButtonPressed = False
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(DglMain)
            AgL.GridDesign(Dgl1)
            'Me.Top = 300
            'Me.Left = 300
            If DglMain.Rows(rowOrderNo).Visible = True Then
                DglMain.CurrentCell = DglMain.Item(Col1Value, rowOrderNo)
                DglMain.Focus()
            End If
            FFillOrderBalance()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DglMain_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            'If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub
            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Select Case DglMain.CurrentCell.RowIndex
                Case rowItemCategory, rowDimension1, rowDimension2, rowDimension3
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 50
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            'If e.KeyCode = Keys.Enter Then Exit Sub
            'If mEntryMode = "Browse" Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowOrderNo
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select DocId As Code, ManualRefNo As OrderNo 
                                From PurchOrder H 
                                Where H.Vendor = '" & mPartyCode & "' 
                                And H.Process = '" & mProcessCode & "' Order By H.V_Date "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowItemCategory
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description From ItemCategory H  With (NoLock) Order By H.Description  "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowDimension1
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description From Dimension1 H  With (NoLock) Order By H.Description  "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowDimension2
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description From Dimension2 H  With (NoLock) Order By H.Description  "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowDimension3
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description From Dimension3 H  With (NoLock) Order By H.Description  "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowDimension4
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description From Dimension4 H  With (NoLock) Order By H.Description  "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowSize
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description From Size H  With (NoLock) Order By H.Description  "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        FOkButtonClick()
    End Sub
    Public Function DataValidation() As Boolean
        DataValidation = False

        For I As Integer = 0 To DglMain.Rows.Count - 1
            If DglMain.Item(Col1Mandatory, I).Value <> "" Then
                If DglMain(Col1Value, I).Value = "" Then
                    MsgBox(DglMain.Item(Col1Head, I).Value & " can not be blank...!", MsgBoxStyle.Information)
                    Exit Function
                End If
            End If
        Next

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1ReceiveQty, I).Value) > Val(Dgl1.Item(Col1BalanceQty, I).Value) Then
                MsgBox("Receive Qty is greater then Balance Qty at row no." & Dgl1.Item(ColSNo, I).Value, MsgBoxStyle.Information)
                Dgl1.CurrentCell = Dgl1.Item(Col1ReceiveQty, I)
                Dgl1.Focus()
                Exit Function
            End If
        Next

        DataValidation = True
    End Function
    Public Sub FMoveRec(ByVal SearchCode As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0


        mSearchcode = SearchCode

        Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DglMain_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If DglMain.CurrentCell.RowIndex = mDglMainLastRowIndex Then
            '    BtnOk.Focus()
            'End If
        End If
    End Sub
    Private Sub DglMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellContentClick
        Select Case e.RowIndex
            Case rowBtnOrderBalance
                FFillOrderBalance()
        End Select
    End Sub
    Public Sub FFillOrderBalance()
        Dim DtTemp As DataTable
        Dim StrRtn As String = ""
        Dim bPendingOrderQry As String = ""

        bPendingOrderQry = " SELECT VOrder.PurchOrder, VOrder.PurchOrderSr, IsNull(VOrder.OrderQty,0) - IsNull(VReceive.ReceiveQty,0) AS BalanceQty
                FROM (
                    SELECT L.PurchOrder, L.PurchOrderSr, Sum(L.Qty) AS OrderQty
                    FROM PurchOrder H 
                    LEFT JOIN PurchOrderDetail L ON H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    Where H.Vendor = '" & mPartyCode & "'
                    And H.Process = '" & mProcessCode & "' " &
                    " And H.V_Date <= " & AgL.Chk_Date(CDate(mV_Date)) & "" &
                    " And L.SubRecordType Is Null
	                GROUP BY L.PurchOrder, L.PurchOrderSr
                ) AS VOrder
                LEFT JOIN (
                    SELECT L.ReferenceDocId As PurchOrder, L.ReferenceTSr As PurchOrderSr, Sum(L.Qty_Rec) AS ReceiveQty
                    FROM (Select ReferenceDocId, ReferenceTSr, Qty_Rec From Stock
                          Union ALL
                          Select ReferenceDocId, ReferenceTSr, Qty_Rec From StockVirtual) L 
                    GROUP BY L.ReferenceDocId, L.ReferenceTSr	
                ) AS VReceive ON VOrder.PurchOrder = VReceive.PurchOrder AND VOrder.PurchOrderSr = VReceive.PurchOrderSr 
                WHERE 1=1 
                And IsNull(VOrder.OrderQty,0) - IsNull(VReceive.ReceiveQty,0) > 0 "

        mQry = " Select 'o' As Tick, L.DocID || '#' || Cast(L.Sr as Varchar) As SearchKey, 
                H.V_Type || '-' || H.ManualRefNo As PurchOrderNo, H.V_Date As PurchOrderDate, 
                Ic.Description As ItemCategory, Ig.Description As ItemGroup, I.Description As Item,
                D1.Description As Dimension1, D2.Description As Dimension2, 
                D3.Description As Dimension3, D4.Description As Dimension4,
                Size.Description As Size, VPendingOrder.BalanceQty, L.Unit,
                Sku.Code As SkuCode, Ic.Code As ItemCategoryCode, Ig.Code As ItemGroupCode, I.Code As ItemCode,
                D1.Code As Dimension1Code, D2.Code As Dimension2Code, 
                D3.Code As Dimension3Code, D4.Code As Dimension4Code,
                Size.Code As SizeCode, It.Code As ItemTypeCode, It.Name As ItemType, 
                Rm.Code As RawMaterial, Rm.Description As RawMaterialDesc,
                VPendingOrder.PurchOrder, VPendingOrder.PurchOrderSr,
                L.DealUnit, L.UnitMultiplier, L.UnitMultiplier * VPendingOrder.BalanceQty As DealQty,
                L.Barcode, Bc.Description As BarcodeDesc, Sku.Code As SkuCode, Sku.Description As Sku, U.DecimalPlaces As QtyDecimalPlaces
                FROM (" & bPendingOrderQry & ") As VPendingOrder
                LEFT JOIN PurchOrderDetail L On VPendingOrder.PurchOrder = L.DocId And VPendingOrder.PurchOrderSr = L.Sr 
                LEFT JOIN PurchOrder H On L.DocId = H.DocId 
                LEFT JOIN Item Sku ON Sku.Code = L.Item
                LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                LEFT JOIN Item IC On Sku.ItemCategory = IC.Code
                LEFT JOIN Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size 
                LEFT JOIN Item Rm ON Rm.Code = L.RawMaterial
                LEFT JOIN Barcode Bc On L.Barcode = Bc.Code
                Left Join Unit U With (NoLock) On L.Unit = U.Code 
                Where 1=1 "

        If AgL.XNull(DglMain.Item(Col1Value, rowOrderNo).Tag) <> "" Then
            mQry += " And H.DocId = '" & DglMain.Item(Col1Value, rowOrderNo).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag) <> "" Then
            mQry += " And Ic.Code = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Tag) <> "" Then
            mQry += " And Ig.Code = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowItem).Tag) <> "" Then
            mQry += " And I.Code = '" & DglMain.Item(Col1Value, rowItem).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowDimension1).Tag) <> "" Then
            mQry += " And D1.Code = '" & DglMain.Item(Col1Value, rowDimension1).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowDimension2).Tag) <> "" Then
            mQry += " And D2.Code = '" & DglMain.Item(Col1Value, rowDimension2).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowDimension3).Tag) <> "" Then
            mQry += " And D3.Code = '" & DglMain.Item(Col1Value, rowDimension3).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowDimension4).Tag) <> "" Then
            mQry += " And D4.Code = '" & DglMain.Item(Col1Value, rowDimension4).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowSize).Tag) <> "" Then
            mQry += " And Size.Code = '" & DglMain.Item(Col1Value, rowSize).Tag & "'"
        End If

        mQry += " Order By PurchOrderDate, Ic.Description, Ig.Description, I.Description,
                D1.Description, D2.Description, D3.Description, D4.Description, Size.Description "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(I).Name <> Col1ItemType And Dgl1.Columns(I).Name <> Col1Sku And
                Dgl1.Columns(I).Name <> Col1ReferenceDocIdTSr And Dgl1.Columns(I).Name <> Col1ReferenceDocIdSr And
                Dgl1.Columns(I).Name <> Col1QtyDecimalPlaces Then
                Dgl1.Columns(I).Visible = True
            End If
        Next


        If DtTemp.Select("[ItemCategory] <> '' ").Length = 0 Then Dgl1.Columns(Col1ItemCategory).Visible = False
        If DtTemp.Select("[ItemGroup] <> '' ").Length = 0 Then Dgl1.Columns(Col1ItemGroup).Visible = False
        If DtTemp.Select("[Item] <> '' ").Length = 0 Then Dgl1.Columns(Col1Item).Visible = False
        If DtTemp.Select("[Dimension1] <> '' ").Length = 0 Then Dgl1.Columns(Col1Dimension1).Visible = False
        If DtTemp.Select("[Dimension2] <> '' ").Length = 0 Then Dgl1.Columns(Col1Dimension2).Visible = False
        If DtTemp.Select("[Dimension3] <> '' ").Length = 0 Then Dgl1.Columns(Col1Dimension3).Visible = False
        If DtTemp.Select("[Dimension4] <> '' ").Length = 0 Then Dgl1.Columns(Col1Dimension4).Visible = False
        If DtTemp.Select("[Size] <> '' ").Length = 0 Then Dgl1.Columns(Col1Size).Visible = False
        If DtTemp.Select("[RawMaterial] <> '' ").Length = 0 Then Dgl1.Columns(Col1RawMaterial).Visible = False


        Dgl1.RowCount = 1
        Dgl1.Rows.Clear()
        For I As Integer = 0 To DtTemp.Rows.Count - 1
            Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count

            Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(DtTemp.Rows(I)("PurchOrder"))
            Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchOrderNo"))
            Dgl1.Item(Col1ReferenceDocIdTSr, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchOrderSr"))

            Dgl1.Item(Col1ReferenceDocIdDate, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchOrderDate"))

            Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(DtTemp.Rows(I)("ItemTypeCode"))
            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemType"))

            Dgl1.Item(Col1Sku, I).Tag = AgL.XNull(DtTemp.Rows(I)("SkuCode"))
            Dgl1.Item(Col1Sku, I).Value = AgL.XNull(DtTemp.Rows(I)("Sku"))

            Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(DtTemp.Rows(I)("ItemCategoryCode"))
            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemCategory"))

            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(DtTemp.Rows(I)("ItemGroupCode"))
            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroup"))

            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(DtTemp.Rows(I)("ItemCode"))
            Dgl1.Item(Col1Item, I).Value = AgL.XNull(DtTemp.Rows(I)("Item"))

            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension1Code"))
            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(DtTemp.Rows(I)("Dimension1"))

            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension2Code"))
            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(DtTemp.Rows(I)("Dimension2"))

            Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension3Code"))
            Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(DtTemp.Rows(I)("Dimension3"))

            Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension4Code"))
            Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(DtTemp.Rows(I)("Dimension4"))

            Dgl1.Item(Col1Size, I).Tag = AgL.XNull(DtTemp.Rows(I)("SizeCode"))
            Dgl1.Item(Col1Size, I).Value = AgL.XNull(DtTemp.Rows(I)("Size"))

            Dgl1.Item(Col1RawMaterial, I).Tag = AgL.XNull(DtTemp.Rows(I)("RawMaterial"))
            Dgl1.Item(Col1RawMaterial, I).Value = AgL.XNull(DtTemp.Rows(I)("RawMaterialDesc"))

            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(DtTemp.Rows(I)("QtyDecimalPlaces"))

            Dgl1.Item(Col1BalanceQty, I).Value = Format(AgL.VNull(DtTemp.Rows(I)("BalanceQty")), "0.".PadRight(AgL.VNull(DtTemp.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(DtTemp.Rows(I)("Unit"))
            'Dgl1.Item(Col1Unit, I).Tag = AgL.VNull(DtTemp.Rows(I)("ShowDimensionDetailInSales"))
        Next
    End Sub
    Private Sub FrmPurchInvoiceOrderSelection_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = (Keys.F And e.Alt) Then
            FFillOrderBalance()
        ElseIf e.KeyCode = (Keys.O And e.Alt) Then
            FOkButtonClick()
        End If
    End Sub
    Private Sub FOkButtonClick()
        Dim I As Integer = 0
        If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
        mOkButtonPressed = True
        If DataValidation() = False Then Exit Sub
        Me.Close()
    End Sub
    Private Sub ApplyUISetting()
        Dim bEntryNCat As String = AgL.Dman_Execute("Select NCat From Voucher_Type Where V_Type = '" & mV_Type & "'", AgL.GCn).ExecuteScalar()

        ClsMain.GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode,
                bEntryNCat, mV_Type, "", "", ClsMain.GridTypeConstants.VerticalGrid)
    End Sub
    Private Sub DglMain_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DglMain.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = DglMain.CurrentCell.RowIndex
            mColumnIndex = DglMain.CurrentCell.ColumnIndex
            If DglMain.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DglMain.Item(mColumnIndex, mRowIndex).Value = ""

            If AgL.XNull(DglMain.Item(Col1Value, rowOrderNo).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowItem).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowDimension1).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowDimension2).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowDimension3).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowDimension4).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowSize).Tag) <> "" Then
                FFillOrderBalance()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ReceiveQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ReceiveQty
                    If Val(Dgl1.Item(Col1ReceiveQty, mRowIndex).Value) > Val(Dgl1.Item(Col1BalanceQty, mRowIndex).Value) Then
                        MsgBox("Receive Qty is greater then Balance Qty at row no." & Dgl1.Item(ColSNo, mRowIndex).Value, MsgBoxStyle.Information)
                        Dgl1.CurrentCell = Dgl1.Item(Col1ReceiveQty, mRowIndex)
                        Dgl1.Focus()
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class