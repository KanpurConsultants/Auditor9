Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class FrmPurchaseInvoiceStockSelection
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Public Const ColSNo As String = "S.No."
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"

    Public Const rowGodown As Integer = 0
    Public Const rowFromProcess As Integer = 1
    Public Const rowItemCategory As Integer = 2
    Public Const rowItemGroup As Integer = 3
    Public Const rowItem As Integer = 4
    Public Const rowDimension1 As Integer = 5
    Public Const rowDimension2 As Integer = 6
    Public Const rowDimension3 As Integer = 7
    Public Const rowDimension4 As Integer = 8
    Public Const rowSize As Integer = 9
    Public Const rowBtnStockBalance As Integer = 10

    Public Const HcGodown As String = "Godown"
    Public Const HcFromProcess As String = "From Process"
    Public Const HcItemCategory As String = "Item Category"
    Public Const HcItemGroup As String = "Item Group"
    Public Const HcItem As String = "Item"
    Public Const HcDimension1 As String = "Dimension1"
    Public Const HcDimension2 As String = "Dimension2"
    Public Const HcDimension3 As String = "Dimension3"
    Public Const HcDimension4 As String = "Dimension4"
    Public Const HcSize As String = "Size"
    Public Const HcBtnStockBalance As String = "Stock Balance"


    Public Const Col1FromProcess As String = "From Process"
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
    Public Const Col1Stock As String = "Stock"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1BarcodeType As String = "Barcode Type"
    Public Const Col1BarcodePattern As String = "Barcode Pattern"



    Dim mSearchcode As String
    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mPartyCode As String
    Dim mV_Type As String = ""
    Dim mV_Date As String = ""
    Dim mProcessCode As String = ""
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
            .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, Col1ItemType, False, True)
            .AddAgTextColumn(Dgl1, Col1Sku, 100, 0, Col1Sku, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 120, 0, Col1ItemCategory, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 120, 0, Col1ItemGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 120, 0, Col1Dimension1, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 120, 0, Col1Dimension2, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 120, 0, Col1Dimension3, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 120, 0, Col1Dimension4, False, True)
            .AddAgTextColumn(Dgl1, Col1Size, 120, 0, Col1Size, False, True)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 120, 255, Col1FromProcess, False, True)
            .AddAgNumberColumn(Dgl1, Col1Stock, 70, 8, 0, False, Col1Stock, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 0, False, Col1Qty, False, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, False, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, True, True, False)
            .AddAgTextColumn(Dgl1, Col1BarcodePattern, 100, 0, Col1BarcodePattern, False, False)
            .AddAgTextColumn(Dgl1, Col1BarcodeType, 100, 0, Col1BarcodeType, False, False)
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

        DglMain.Rows.Add(11)
        DglMain.Item(Col1Head, rowGodown).Value = HcGodown
        DglMain.Item(Col1Head, rowFromProcess).Value = HcFromProcess
        DglMain.Item(Col1Head, rowItemCategory).Value = HcItemCategory
        DglMain.Item(Col1Head, rowItemGroup).Value = HcItemGroup
        DglMain.Item(Col1Head, rowItem).Value = HcItem
        DglMain.Item(Col1Head, rowItemCategory).Value = HcItemCategory
        DglMain.Item(Col1Head, rowDimension1).Value = HcDimension1
        DglMain.Item(Col1Head, rowDimension2).Value = HcDimension2
        DglMain.Item(Col1Head, rowDimension3).Value = HcDimension3
        DglMain.Item(Col1Head, rowDimension4).Value = HcDimension4
        DglMain.Item(Col1Head, rowSize).Value = HcSize
        DglMain.Item(Col1Head, rowBtnStockBalance).Value = HcBtnStockBalance
        DglMain.Item(Col1Value, rowBtnStockBalance) = New DataGridViewButtonCell
        DglMain.Item(Col1Value, rowBtnStockBalance).Value = "Alt + F"
        DglMain.Item(Col1Value, rowBtnStockBalance).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        For I As Integer = 0 To DglMain.Rows.Count - 1
            If AgL.XNull(DglMain(Col1HeadOriginal, I).Value) = "" Then
                DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
            End If
        Next


        DglMain.Item(Col1Value, rowFromProcess).Tag = AgL.XNull(AgL.Dman_Execute("Select PrevProcess 
                From ProcessDetail Where SubCode = '" & mProcessCode & "'", AgL.GCn).ExecuteScalar())

        AgL.FSetDimensionCaptionForVerticalGrid(DglMain, AgL)

        ApplyUISetting()

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
                Case rowGodown
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Godown & "' Order By Name"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowFromProcess
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT SubCode As Code, Name FROM SubGroup With (NoLock) 
                                    Where SubGroupType = '" & SubgroupType.Process & "' 
                                    Order By Name "
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
                        mQry = "SELECT Code, Description From Size H  With (NoLock) 
                                Where H.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "' 
                                Order By H.Description  "
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

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.VNull(Dgl1.Item(Col1Qty, I).Value) <> 0 Then
                If AgL.VNull(Dgl1.Item(Col1Stock, I).Value) < AgL.VNull(Dgl1.Item(Col1Qty, I).Value) Then
                    MsgBox("Qty exceeding stock value at row no. " & I + 1.ToString & " . can not be continue !", MsgBoxStyle.Exclamation)
                    Dgl1.CurrentCell = Dgl1(Col1Qty, I)
                    Exit Function
                End If
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
    'Private Sub ApplyUISettings(NCAT As String)
    '    Dim mQry As String
    '    Dim DtTemp As DataTable
    '    Dim I As Integer, J As Integer
    '    Dim mDglMainRowCount As Integer
    '    Try
    '        For I = 0 To DglMain.Rows.Count - 1
    '            DglMain.Rows(I).Visible = False
    '        Next
    '        DglMain.Visible = False

    '        mQry = "Select H.*
    '                from EntryHeaderUISetting H                   
    '                Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & DglMain.Name & "' "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


    '        If DtTemp.Rows.Count > 0 Then
    '            For I = 0 To DtTemp.Rows.Count - 1
    '                For J = 0 To DglMain.Rows.Count - 1
    '                    If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DglMain.Item(Col1Head, J).Value Then
    '                        DglMain.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
    '                        If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglMainRowCount += 1
    '                        DglMain.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
    '                        If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
    '                            DglMain.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
    '                        End If
    '                    End If
    '                Next
    '            Next
    '        End If
    '        If mDglMainRowCount > 0 Then
    '            DglMain.Visible = True
    '        End If


    '        For I = 0 To DglMain.Rows.Count - 1
    '            If DglMain.Rows(I).Visible = True Then
    '                mDglMainLastRowIndex = I
    '            End If
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
    '    End Try
    'End Sub
    Private Sub DglMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellContentClick
        Select Case e.RowIndex
            Case rowBtnStockBalance
                FFillStockBalance()
        End Select
    End Sub
    Public Sub FFillStockBalance()
        Dim DtTemp As DataTable
        Dim StrRtn As String = ""
        Dim bPendingStockQry As String = ""

        'Dim mTableName As String = "Select V_Date, Process, Godown, Item, Qty_Iss, Qty_Rec From Stock
        '                            UNION ALL 
        '                            Select V_Date, Process, Godown, Item, Qty_Iss, Qty_Rec From StockVirtual "

        Dim mTableName As String = "Select V_Date, Process, Godown, Item, Qty_Iss, Qty_Rec From Stock"


        bPendingStockQry = " SELECT L.Process, L.Item, 
	                IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) AS BalanceQty
	                FROM (" & mTableName & ") L 
                    Where 1 = 1 " &
                    " And L.V_Date <= " & AgL.Chk_Date(CDate(mV_Date)) & "" &
                    IIf(AgL.XNull(DglMain.Item(Col1Value, rowGodown).Tag) = "", "", "And L.Godown = '" & DglMain.Item(Col1Value, rowGodown).Tag & "'") &
                    IIf(AgL.XNull(DglMain.Item(Col1Value, rowFromProcess).Tag) = "", "", "And L.Process = '" & DglMain.Item(Col1Value, rowFromProcess).Tag & "'") &
                    " GROUP BY L.Process, L.Item
                    HAVING IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) <> 0 "

        mQry = " SELECT 'o' As Tick, V.ItemCode As SearchKey,
                 Max(V.Process) Process, Max(V.ItemCategory) ItemCategory, Max(V.ItemType) ItemType, Max(V.ItemGroup) ItemGroup, Max(V.Item) Item, Max(V.Dimension1) Dimension1, Max(V.Dimension2) Dimension2,
                 Max(V.Dimension3) Dimension3, Max(V.Dimension4) Dimension4, Max(V.Size) Size, Sum(V.BalanceQty) as BalanceQty, Max(Unit) AS Unit, Max(Sku) Sku,
                 Max(V.QtyDecimalPlaces) as QtyDecimalPlaces,Max(V.BarcodePattern) as BarcodePattern, Max(V.BarcodeType) as BarcodeType,
                 V.ItemCategoryCode, V.ItemGroupCode, V.ItemCode, V.Dimension1Code,V.Dimension2Code,V.Dimension3Code,V.Dimension4Code,
                 V.SizeCode, V.ItemTypeCode, Max(V.SkuCode) AS SkuCode ,V.ProcessCode
                 FROM (SELECT Prs.Name As Process, Ic.Description As ItemCategory, Ig.Description As ItemGroup, I.Description As Item,
                D1.Description As Dimension1, D2.Description As Dimension2, 
                D3.Description As Dimension3, D4.Description As Dimension4,
                Size.Description As Size, VPendingStock.BalanceQty AS BalanceQty, Sku.Unit,
                Ic.Code As ItemCategoryCode, Ig.Code As ItemGroupCode, I.Code As ItemCode,
                D1.Code As Dimension1Code, D2.Code As Dimension2Code, 
                D3.Code As Dimension3Code, D4.Code As Dimension4Code,
                Size.Code As SizeCode, Prs.SubCode As ProcessCode, It.Code As ItemTypeCode, 
                It.Name As ItemType, Sku.Code As SkuCode, Sku.Description As Sku,
                U.DecimalPlaces As QtyDecimalPlaces, U.ShowDimensionDetailInSales,
                IsNull(Ig.BarcodeType,Ic.BarcodeType) As BarcodeType, 
                IsNull(Ig.BarcodePattern,Ic.BarcodePattern) As BarcodePattern
                FROM (" & bPendingStockQry & ") AS VPendingStock
                LEFT JOIN SubGroup Prs ON VPendingStock.Process = Prs.SubCode
                LEFT JOIN Item Sku ON Sku.Code = VPendingStock.Item
                LEFT JOIN Item I ON I.Code = Sku.BaseItem
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                LEFT JOIN Item IC On Sku.ItemCategory = IC.Code
                LEFT JOIN Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size 
                Left Join Unit U  With (NoLock) On Sku.Unit = U.Code 
                Where 1=1 "

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


        Dim FilterInclude_ContraProcess As String = FGetSettings(SettingFields.FilterInclude_ContraProcess, SettingType.General)
        If FilterInclude_ContraProcess <> "" Then
            mQry += " And (CharIndex('+' || Prs.SubCode,'" & FilterInclude_ContraProcess & "') > 0 Or
                                CharIndex('+' || Prs.Parent,'" & FilterInclude_ContraProcess & "') > 0) "
        End If



        mQry += " ) V               
                    GROUP BY  V.ItemCategoryCode, V.ItemGroupCode, V.ItemCode, V.Dimension1Code,V.Dimension2Code,V.Dimension3Code,V.Dimension4Code, V.SizeCode, V.ItemTypeCode,V.ProcessCode
                    HAVING Sum(V.BalanceQty) >0
                    Order By Max(V.ItemCategory), Max(V.ItemGroup), Max(V.Item), Max(V.Dimension1), Max(V.Dimension2), Max(V.Dimension3), Max(V.Dimension4), Max(V.Size) "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(I).Name <> Col1ItemType And Dgl1.Columns(I).Name <> Col1Sku Then
                Dgl1.Columns(I).Visible = True
            End If
        Next


        Dgl1.Columns(Col1BarcodePattern).Visible = False
        Dgl1.Columns(Col1BarcodeType).Visible = False

        If DtTemp.Select("[ItemCategory] <> '' ").Length = 0 Then Dgl1.Columns(Col1ItemCategory).Visible = False
        If DtTemp.Select("[ItemGroup] <> '' ").Length = 0 Then Dgl1.Columns(Col1ItemGroup).Visible = False
        If DtTemp.Select("[Item] <> '' ").Length = 0 Then Dgl1.Columns(Col1Item).Visible = False
        If DtTemp.Select("[Dimension1] <> '' ").Length = 0 Then Dgl1.Columns(Col1Dimension1).Visible = False
        If DtTemp.Select("[Dimension2] <> '' ").Length = 0 Then Dgl1.Columns(Col1Dimension2).Visible = False
        If DtTemp.Select("[Dimension3] <> '' ").Length = 0 Then Dgl1.Columns(Col1Dimension3).Visible = False
        If DtTemp.Select("[Dimension4] <> '' ").Length = 0 Then Dgl1.Columns(Col1Dimension4).Visible = False
        If DtTemp.Select("[Size] <> '' ").Length = 0 Then Dgl1.Columns(Col1Size).Visible = False


        Dgl1.RowCount = 1
        Dgl1.Rows.Clear()
        For I As Integer = 0 To DtTemp.Rows.Count - 1
            Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count

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

            Dgl1.Item(Col1FromProcess, I).Tag = AgL.XNull(DtTemp.Rows(I)("ProcessCode"))
            Dgl1.Item(Col1FromProcess, I).Value = AgL.XNull(DtTemp.Rows(I)("Process"))

            'Dgl1.Item(Col1Stock, I).Value = AgL.VNull(DtTemp.Rows(I)("BalanceQty"))
            Dgl1.Item(Col1Stock, I).Value = Format(Math.Abs(AgL.VNull(DtTemp.Rows(I)("BalanceQty"))), "0.".PadRight(AgL.VNull(DtTemp.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(DtTemp.Rows(I)("Unit"))
            'Dgl1.Item(Col1Unit, I).Tag = AgL.VNull(DtTemp.Rows(I)("ShowDimensionDetailInSales"))

            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(DtTemp.Rows(I)("QtyDecimalPlaces"))

            Dgl1.Item(Col1BarcodePattern, I).Value = AgL.XNull(DtTemp.Rows(I)("BarcodePattern"))
            Dgl1.Item(Col1BarcodeType, I).Value = AgL.XNull(DtTemp.Rows(I)("BarcodeType"))
        Next
    End Sub
    Private Sub FrmPurchaseInvoiceStockSelection_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = (Keys.F And e.Alt) Then
            FFillStockBalance()
        ElseIf e.KeyCode = (Keys.O And e.Alt) Then
            FOkButtonClick()
        End If
    End Sub
    Private Sub FOkButtonClick()
        Dim I As Integer = 0

        If DataValidation() = False Then Exit Sub
        If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
        mOkButtonPressed = True
        Me.Close()
    End Sub
    Private Sub ApplyUISetting()
        Dim bEntryNCat As String = AgL.Dman_Execute("Select NCat From Voucher_Type Where V_Type = '" & mV_Type & "'", AgL.GCn).ExecuteScalar()

        ClsMain.GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode,
                bEntryNCat, mV_Type, "", "", ClsMain.GridTypeConstants.VerticalGrid)

        Dgl1.Columns(Col1BarcodePattern).Visible = False
        Dgl1.Columns(Col1BarcodeType).Visible = False
    End Sub
    Private Sub FrmPurchaseInvoiceStockSelection_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If DglMain.FirstDisplayedCell IsNot Nothing Then
            DglMain.CurrentCell = DglMain.Item(Col1Value, DglMain.FirstDisplayedCell.RowIndex)
            DglMain.Focus()
        ElseIf DglMain.Rows(rowGodown).Visible = True Then
            DglMain.CurrentCell = DglMain.Item(Col1Value, rowGodown)
            DglMain.Focus()
        ElseIf DglMain.Rows(rowFromProcess).Visible = True Then
            DglMain.CurrentCell = DglMain.Item(Col1Value, rowFromProcess)
            DglMain.Focus()
        ElseIf DglMain.Rows(rowItemCategory).Visible = True Then
            DglMain.CurrentCell = DglMain.Item(Col1Value, rowItemCategory)
            DglMain.Focus()
        End If
    End Sub
    Private Sub DglMain_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DglMain.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = DglMain.CurrentCell.RowIndex
            mColumnIndex = DglMain.CurrentCell.ColumnIndex
            If DglMain.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DglMain.Item(mColumnIndex, mRowIndex).Value = ""

            If AgL.XNull(DglMain.Item(Col1Value, rowGodown).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowFromProcess).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowItem).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowDimension1).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowDimension2).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowDimension3).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowDimension4).Tag) <> "" Or
                AgL.XNull(DglMain.Item(Col1Value, rowSize).Tag) <> "" Then
                FFillStockBalance()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim bEntryNCat As String = AgL.Dman_Execute("Select NCat From Voucher_Type Where V_Type = '" & mV_Type & "'", AgL.GCn).ExecuteScalar()
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode,
                AgL.PubSiteCode, "", bEntryNCat, mV_Type, mProcessCode, "")
        FGetSettings = mValue
    End Function
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class