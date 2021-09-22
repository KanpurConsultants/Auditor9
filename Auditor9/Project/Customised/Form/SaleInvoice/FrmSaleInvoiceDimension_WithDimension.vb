Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmSaleInvoiceDimension_WithDimension
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Col1ItemType As String = AgL.PubCaptionItemType
    Public Const Col1SKU As String = "SKU"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1Pcs As String = "Pcs"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1TotalQty As String = "Total Qty"
    Public Const Col1UnitMultiplier As String = "Unit Multiplier"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1TotalDealQty As String = "Total Deal Qty"



    Public Const Col1MItemCategory As String = "M Item Category"
    Public Const Col1MItemGroup As String = "M Item Group"
    Public Const Col1MItemSpecification As String = "M Item Specification"
    Public Const Col1MDimension1 As String = "M Dimension 1"
    Public Const Col1MDimension2 As String = "M Dimension 2"
    Public Const Col1MDimension3 As String = "M Dimension 3"
    Public Const Col1MDimension4 As String = "M Dimension 4"
    Public Const Col1MSize As String = "M Size"



    Dim mSearchCode As String
    Dim mSearchCodeSr As Integer

    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mDealUnit$ = ""
    Dim mItemName As String
    Dim mUnitDecimalPlace As Integer
    Dim mDealUnitDecimalPlace As Integer
    Dim mObjFrmSaleInvoice As FrmSaleInvoiceDirect_WithDimension
    Dim mDglRow As DataGridViewRow
    Dim mDtV_TypeSettings As DataTable

    Public Property DglRow() As DataGridViewRow
        Get
            DglRow = mDglRow
        End Get
        Set(ByVal value As DataGridViewRow)
            mDglRow = value
        End Set
    End Property
    Public Property ItemName() As String
        Get
            ItemName = mItemName
        End Get
        Set(ByVal value As String)
            mItemName = value
        End Set
    End Property

    Public Property Unit() As String
        Get
            Unit = mUnit
        End Get
        Set(ByVal value As String)
            mUnit = value
        End Set
    End Property
    Public Property DealUnit() As String
        Get
            DealUnit = mDealUnit
        End Get
        Set(ByVal value As String)
            mDealUnit = value
        End Set
    End Property
    Public Property DtV_TypeSettings() As DataTable
        Get
            DtV_TypeSettings = mDtV_TypeSettings
        End Get
        Set(ByVal value As DataTable)
            mDtV_TypeSettings = value
        End Set
    End Property
    Public Property UnitDecimalPlace() As Integer
        Get
            UnitDecimalPlace = mUnitDecimalPlace
        End Get
        Set(ByVal value As Integer)
            mUnitDecimalPlace = value
        End Set
    End Property
    Public Property DealUnitDecimalPlace() As Integer
        Get
            DealUnitDecimalPlace = mDealUnitDecimalPlace
        End Get
        Set(ByVal value As Integer)
            mDealUnitDecimalPlace = value
        End Set
    End Property
    Public ReadOnly Property GetTotalPcs() As Double
        Get
            GetTotalPcs = Val(LblTotalPcs.Text)
        End Get
    End Property
    Public ReadOnly Property GetTotalQty() As Double
        Get
            GetTotalQty = Val(LblTotalQty.Text)
        End Get
    End Property
    Public ReadOnly Property GetTotalDealQty() As Double
        Get
            GetTotalDealQty = Val(LblTotalDealQty.Text)
        End Get
    End Property
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property

    Public Property objFrmSaleInvoice() As Object
        Get
            objFrmSaleInvoice = mObjFrmSaleInvoice
        End Get
        Set(ByVal value As Object)
            mObjFrmSaleInvoice = value
        End Set

    End Property


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    AgL.FPaintForm(Me, e, 0)
    'End Sub

    Public Sub IniGrid(DocID As String, Sr As Integer)
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, Col1ItemType, False, False)
            .AddAgTextColumn(Dgl1, Col1SKU, 300, 0, Col1SKU, False, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 120, 0, Col1Dimension1, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 120, 0, Col1Dimension2, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 120, 0, Col1Dimension3, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 120, 0, Col1Dimension4, False, False)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 160, 255, Col1Specification, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, mUnitDecimalPlace, False, mUnit, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Pcs, 100, 5, 0, False, Col1Pcs, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalQty, 100, 8, 4, False, "Total " & mUnit, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1UnitMultiplier, 70, 8, 4, False, Col1UnitMultiplier, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1DealQty, 100, 8, mDealUnitDecimalPlace, False, mDealUnit, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDealQty, 100, 8, 4, False, "Total " & mDealUnit, True, False, True)

            .AddAgTextColumn(Dgl1, Col1MItemCategory, 100, 0, Col1MItemCategory, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemGroup, 100, 0, Col1MItemGroup, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemSpecification, 100, 0, Col1MItemSpecification, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension1, 100, 0, "M " & AgL.PubCaptionDimension1, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension2, 100, 0, "M " & AgL.PubCaptionDimension2, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension3, 100, 0, "M " & AgL.PubCaptionDimension3, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension4, 100, 0, "M " & AgL.PubCaptionDimension4, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MSize, 100, 0, Col1MSize, False, False, False)

        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Name = "Dgl1"
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        ApplyUISetting()


        FMoverec(DocID, Sr)
    End Sub
    Public Sub FMoverec(DocID As String, Sr As Integer)
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer
        'mQry = "Select L.*, U.DecimalPlaces as QtyDecimalPlaces 
        '        From SaleInvoiceDimensionDetail L  With (NoLock)
        '        Left Join SaleInvoiceDetail IL  With (NoLock) on L.DocId = IL.DocId And L.Tsr = IL.Sr                
        '        Left Join Unit U  With (NoLock) On IL.Unit = U.Code
        '        Where L.DocId = '" & DocID & "' And L.TSr ='" & Sr & "'
        '        Order By L.Sr "

        mQry = "Select L.*, I.Description As ItemDesc, 
                        U.DecimalPlaces As QtyDecimalPlaces, 
                        DU.DecimalPlaces As DealQtyDecimalPlaces, 
                        Sku.Code As SkuCode, Sku.Description As SkuDescription, 
                        It.Code As ItemType, It.Name As ItemTypeDesc,
                        IG.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, 
                        Sids.ItemCategory, Sids.ItemGroup, 
                        Sids.Dimension1, Sids.Dimension2, 
                        Sids.Dimension3, Sids.Dimension4, Sids.Size, 
                        D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                        D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                        I.ItemCategory as MItemCategory, I.ItemGroup as MItemGroup, I.Specification as MItemSpecification, 
                        I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MSize 
                        From (Select * From SaleInvoiceDimensionDetail  With (NoLock)  Where DocId = '" & DocID & "' And TSr ='" & Sr & "') As L 
                        LEFT JOIN SaleInvoiceDimensionDetailSku Sids With (NoLock) On L.DocId = Sids.DocId And L.TSr = Sids.TSr And L.Sr = Sids.Sr
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
                        Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                        Left Join Unit DU  With (NoLock) On L.DealUnit = DU.Code 
                        Order By L.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))

                    Dgl1.Item(Col1SKU, I).Tag = AgL.XNull(.Rows(I)("SkuCode"))
                    Dgl1.Item(Col1SKU, I).Value = AgL.XNull(.Rows(I)("SkuDescription"))


                    Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(.Rows(I)("ItemType"))
                    Dgl1.Item(Col1ItemType, I).Value = AgL.XNull(.Rows(I)("ItemTypeDesc"))


                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))

                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))

                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                    Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                    Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                    Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                    Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                    Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))

                    Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))

                    Dgl1.Item(Col1Size, I).Tag = AgL.XNull(.Rows(I)("Size"))
                    Dgl1.Item(Col1Size, I).Value = AgL.XNull(.Rows(I)("SizeDesc"))



                    Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                    'Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1Qty, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Qty")))
                    Dgl1.Item(Col1Pcs, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Pcs")))
                    'Dgl1.Item(Col1TotalQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1TotalQty, I).Value = Math.Abs(AgL.VNull(.Rows(I)("TotalQty")))

                    Dgl1.Item(Col1UnitMultiplier, I).Value = AgL.VNull(.Rows(I)("UnitMultiplier"))
                    Dgl1.Item(Col1DealQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("DealQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("DealQtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1TotalDealQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalDealQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("DealQtyDecimalPlaces")) + 2, "0"))

                    Dgl1.Item(Col1MItemCategory, I).Tag = AgL.XNull(.Rows(I)("MItemCategory"))
                    Dgl1.Item(Col1MItemGroup, I).Tag = AgL.XNull(.Rows(I)("MItemGroup"))
                    Dgl1.Item(Col1MItemSpecification, I).Value = AgL.XNull(.Rows(I)("MItemSpecification"))
                    Dgl1.Item(Col1MDimension1, I).Tag = AgL.XNull(.Rows(I)("MDimension1"))
                    Dgl1.Item(Col1MDimension2, I).Tag = AgL.XNull(.Rows(I)("MDimension2"))
                    Dgl1.Item(Col1MDimension3, I).Tag = AgL.XNull(.Rows(I)("MDimension3"))
                    Dgl1.Item(Col1MDimension4, I).Tag = AgL.XNull(.Rows(I)("MDimension4"))
                    Dgl1.Item(Col1MSize, I).Tag = AgL.XNull(.Rows(I)("MSize"))

                Next I
            End If
        End With
        Calculation()
    End Sub
    Public Function FData_Validation() As Boolean
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) <> "" _
                                Or AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) <> "" _
                                Or AgL.XNull(Dgl1.Item(Col1Item, I).Value) <> "" _
                                Or AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) <> "" _
                                Or AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) <> "" _
                                Or AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) <> "" _
                                Or AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) <> "" _
                                Or AgL.XNull(Dgl1.Item(Col1Size, I).Value) <> "" _
                   Then
                Dgl1.Item(Col1SKU, I).Tag = ClsMain.FGetSKUCode(Dgl1.Item(ColSNo, I).Value, Dgl1.Item(Col1ItemType, I).Tag, Dgl1.Item(Col1ItemCategory, I).Tag, Dgl1.Item(Col1ItemCategory, I).Value _
                                                       , Dgl1.Item(Col1ItemGroup, I).Tag, Dgl1.Item(Col1ItemGroup, I).Value _
                                                       , Dgl1.Item(Col1Item, I).Tag, Dgl1.Item(Col1Item, I).Value _
                                                       , Dgl1.Item(Col1Dimension1, I).Tag, Dgl1.Item(Col1Dimension1, I).Value _
                                                       , Dgl1.Item(Col1Dimension2, I).Tag, Dgl1.Item(Col1Dimension2, I).Value _
                                                       , Dgl1.Item(Col1Dimension3, I).Tag, Dgl1.Item(Col1Dimension3, I).Value _
                                                       , Dgl1.Item(Col1Dimension4, I).Tag, Dgl1.Item(Col1Dimension4, I).Value _
                                                       , Dgl1.Item(Col1Size, I).Tag, Dgl1.Item(Col1Size, I).Value _
                                                       , Dgl1.Item(Col1MItemCategory, I).Tag _
                                                       , Dgl1.Item(Col1MItemGroup, I).Tag _
                                                       , Dgl1.Item(Col1MItemSpecification, I).Tag _
                                                       , Dgl1.Item(Col1MDimension1, I).Tag _
                                                       , Dgl1.Item(Col1MDimension2, I).Tag _
                                                       , Dgl1.Item(Col1MDimension3, I).Tag _
                                                       , Dgl1.Item(Col1MDimension4, I).Tag _
                                                       , Dgl1.Item(Col1MSize, I).Tag
                                                       )
                If Dgl1.Item(Col1SKU, I).Tag = "" Then
                    FData_Validation = False
                    Exit Function
                End If
            End If
        Next
        FData_Validation = True
    End Function

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)

            Me.Top = 400
            Me.Left = 400
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex




            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
    '    If e.Control And e.KeyCode = Keys.D Then
    '        sender.CurrentRow.Selected = True
    '    End If
    '    If e.Control Or e.Shift Or e.Alt Then Exit Sub
    'End Sub



    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If EntryMode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                'Case Col1FromUnit
                '    Dgl1.Item(Col1Equal, mRowIndex).Value = "="
                '    Dgl1.Item(Col1ToUnit, mRowIndex).Value = mUnit
                '    Dgl1.Item(Col1ToQtyDecimalPlaces, mRowIndex).Value = mToQtyDecimalPlace
                '    If Val(Dgl1.Item(Col1FromQty, mRowIndex).Value) = 0 Then
                '        Dgl1.Item(Col1FromQty, mRowIndex).Value = "1"
                '    End If

                '    If Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) Is Nothing Then Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) = ""

                '    If Dgl1.Item(Col1FromUnit, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex).ToString.Trim = "" Then
                '        Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = ""
                '    Else
                '        If Dgl1.AgDataRow IsNot Nothing Then
                '            Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
                '        End If
                '    End If

                Case Col1Qty
                    If AgL.XNull(Dgl1.Item(Col1Pcs, mRowIndex).Value) = "" Then
                        Dgl1.Item(Col1Pcs, mRowIndex).Value = "1"
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1

        Try
            Dgl1.Item(Col1ItemType, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemType).Tag)
            Dgl1.Item(Col1ItemCategory, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory).Tag)
            Dgl1.Item(Col1ItemGroup, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup).Tag)
            Dgl1.Item(Col1Item, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Item).Tag)
            Dgl1.Item(Col1Dimension1, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension1).Tag)
            Dgl1.Item(Col1Dimension2, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension2).Tag)
            Dgl1.Item(Col1Dimension3, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension3).Tag)
            Dgl1.Item(Col1Dimension4, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension4).Tag)
            Dgl1.Item(Col1Size, e.RowIndex).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Size).Tag)

            Dgl1.Item(Col1ItemCategory, e.RowIndex).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory).Value)
            Dgl1.Item(Col1ItemGroup, e.RowIndex).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup).Value)
            Dgl1.Item(Col1Item, e.RowIndex).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Item).Value)
            Dgl1.Item(Col1Dimension1, e.RowIndex).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension1).Value)
            Dgl1.Item(Col1Dimension2, e.RowIndex).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension2).Value)
            Dgl1.Item(Col1Dimension3, e.RowIndex).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension3).Value)
            Dgl1.Item(Col1Dimension4, e.RowIndex).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension4).Value)
            Dgl1.Item(Col1Size, e.RowIndex).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Size).Value)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub Calculation()
        Dim I As Integer
        Dim mTotalQty As Double
        Dim mTotalDealQty As Double
        Dim mTotalPcs As Double
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Rows(I).Visible Then
                If Val(Dgl1.Item(Col1Pcs, I).Value) > 0 And Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                    Dgl1.Item(Col1TotalQty, I).Value = Val(Dgl1.Item(Col1Pcs, I).Value) * Val(Dgl1.Item(Col1Qty, I).Value)
                End If

                If Val(Dgl1.Item(Col1UnitMultiplier, I).Value) > 0 And Val(Dgl1.Item(Col1TotalQty, I).Value) > 0 Then
                    Dgl1.Item(Col1TotalDealQty, I).Value = Val(Dgl1.Item(Col1UnitMultiplier, I).Value) * Val(Dgl1.Item(Col1TotalQty, I).Value)
                End If

                If Val(Dgl1.Item(Col1TotalQty, I).Value) > 0 Then
                    mTotalQty += Val(Dgl1.Item(Col1TotalQty, I).Value)
                    mTotalDealQty += Val(Dgl1.Item(Col1TotalDealQty, I).Value)
                    mTotalPcs += Val(Dgl1.Item(Col1Pcs, I).Value)
                End If
            End If
        Next
        LblTotalQty.Text = mTotalQty.ToString()
        LblTotalDealQty.Text = mTotalDealQty.ToString()
        LblTotalPcs.Text = mTotalPcs.ToString()
    End Sub
    Public Sub FSave(DocId As String, TSr As Integer, mGridRowIndex As Integer, ByVal Conn As Object, ByVal Cmd As Object, Optional MultiplyWithMinus As Boolean = False)
        Dim I As Integer
        Dim mDimensionDetail As String

        'If FData_Validation() = False Then Exit Sub

        'Dim mSr As Integer
        'mQry = "Delete From SaleInvoiceDimensionDetail Where DocId = '" & DocId & "' and TSr = " & TSr & " "
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Dim bSalesTaxGroupParty As String = ""
        If mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1BtnDetail, mObjFrmSaleInvoice.rowSaleToParty).Tag IsNot Nothing Then
            bSalesTaxGroupParty = mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1BtnDetail, mObjFrmSaleInvoice.rowSaleToParty).Tag.Dgl1.Item(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1BtnDetail, mObjFrmSaleInvoice.rowSaleToParty).Tag.Col1Value, mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1BtnDetail, mObjFrmSaleInvoice.rowSaleToParty).Tag.rowSalesTaxGroup).Value
        End If
        mDimensionDetail = ""
        For I = 0 To Dgl1.RowCount - 1
            If MultiplyWithMinus Then
                Dgl1.Item(Col1Pcs, I).Value = Val(Dgl1.Item(Col1Pcs, I).Value) * -1
                Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1Qty, I).Value) * -1
                Dgl1.Item(Col1TotalQty, I).Value = Val(Dgl1.Item(Col1TotalQty, I).Value) * -1
                Dgl1.Item(Col1DealQty, I).Value = Val(Dgl1.Item(Col1DealQty, I).Value) * -1
                Dgl1.Item(Col1TotalDealQty, I).Value = Val(Dgl1.Item(Col1TotalDealQty, I).Value) * -1
            End If

            If Dgl1.Rows(I).Visible Then
                If Val(Dgl1.Item(Col1TotalQty, I).Value) <> 0 Then
                    If Dgl1.Item(ColSNo, I).Tag Is Nothing Then
                        mObjFrmSaleInvoice.mDimensionSrl += 1
                        mQry = " INSERT INTO SaleInvoiceDimensionDetail (DocID, TSr, Sr, Item, Specification, Pcs, Qty, Unit, TotalQty, 
                            UnitMultiplier, DealQty, DealUnit, TotalDealQty) " &
                           " VALUES (" & AgL.Chk_Text(DocId) & ", " &
                           " " & TSr & ", " &
                            " " & mObjFrmSaleInvoice.mDimensionSrl & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1SKU, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1Pcs, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", 
                            " & AgL.Chk_Text(mUnit) & ",
                            " & Val(Dgl1.Item(Col1TotalQty, I).Value) & ",
                            " & Val(Dgl1.Item(Col1UnitMultiplier, I).Value) & ",
                            " & Val(Dgl1.Item(Col1DealQty, I).Value) & ", 
                            " & AgL.Chk_Text(mDealUnit) & ",
                            " & Val(Dgl1.Item(Col1TotalDealQty, I).Value) & "
                            ) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Insert Into SaleInvoiceDimensionDetailSku
                                (DocId, TSr, Sr, ItemCategory, ItemGroup, Item, Dimension1, 
                                Dimension2, Dimension3, Dimension4, Size) "
                        mQry += " Values(" & AgL.Chk_Text(DocId) & ", " & TSr & ", " &
                            " " & mObjFrmSaleInvoice.mDimensionSrl & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Size, I).Tag) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, 
                                V_Prefix, V_Date, V_No, RecID, 
                                Div_Code, Site_Code, SubCode,SalesTaxGroupParty, Item, SalesTaxGroupItem, 
                                LotNo, Godown, EType_IR, Qty_Iss, Qty_Rec, Unit, 
                                UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit,
                                Rate, Amount, Landed_Value) 
                                Values
                                (
                                    '" & DocId & "', " & TSr & ", " & mObjFrmSaleInvoice.mDimensionSrl & ", 
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowV_Type).Tag) & ",
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.LblPrefix.Text) & ", 
                                    " & AgL.Chk_Date(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowV_Date).Value) & ", 
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowV_No).Value) & ", 
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowReferenceNo).Value) & ",
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.TxtDivision.Tag) & ", 
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowSite_Code).Tag) & ", 
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowSaleToParty).Tag) & ", 
                                    " & AgL.Chk_Text(bSalesTaxGroupParty) & ", 
                                    " & AgL.Chk_Text(IIf(AgL.XNull(Dgl1(Col1SKU, I).Tag) = "", Dgl1.Item(Col1Item, I).Tag, AgL.XNull(Dgl1(Col1SKU, I).Tag))) & ", 
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1SalesTaxGroup, mGridRowIndex).Value) & ",
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1LotNo, mGridRowIndex).Value) & ",
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1Godown, mGridRowIndex).Value) & ", 
                                    'I', " & Val(Dgl1(Col1TotalQty, I).Value) & ", 0,
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1Unit, mGridRowIndex).Value) & ", 
                                    " & Val(Dgl1(Col1UnitMultiplier, I).Value) & ",
                                    " & Val(Dgl1(Col1TotalDealQty, I).Value) & ", 0,
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1DealUnit, mGridRowIndex).Value) & ", 
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1Rate, mGridRowIndex).Value) & ", 
                                    " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1Amount, mGridRowIndex).Value) & ",0
                                )   "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        If mDimensionDetail <> "" Then
                            If ClsMain.FDivisionNameForCustomization(13) = "ARORA TEXTILE" Then
                                mDimensionDetail += " + "
                            Else
                                mDimensionDetail += ", "
                            End If

                            If Dgl1.Item(Col1Dimension1, I).Value <> "" And Dgl1.Columns(Col1Dimension1).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension1, I).Value + "-"
                            If Dgl1.Item(Col1Dimension2, I).Value <> "" And Dgl1.Columns(Col1Dimension2).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension2, I).Value + "-"
                            If Dgl1.Item(Col1Dimension3, I).Value <> "" And Dgl1.Columns(Col1Dimension3).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension3, I).Value + "-"
                            If Dgl1.Item(Col1Dimension4, I).Value <> "" And Dgl1.Columns(Col1Dimension4).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension4, I).Value + "-"
                            If Dgl1.Columns(Col1Pcs).DisplayIndex < Dgl1.Columns(Col1Qty).DisplayIndex Then
                                mDimensionDetail += Math.Abs(AgL.VNull(Dgl1.Item(Col1Pcs, I).Value)).ToString + " X " + Math.Abs(AgL.VNull(Dgl1.Item(Col1Qty, I).Value)).ToString
                            Else
                                mDimensionDetail += Math.Abs(AgL.VNull(Dgl1.Item(Col1Qty, I).Value)).ToString + " X " + Math.Abs(AgL.VNull(Dgl1.Item(Col1Pcs, I).Value)).ToString
                            End If
                        Else
                            If Dgl1.Item(Col1Dimension1, I).Value <> "" And Dgl1.Columns(Col1Dimension1).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension1, I).Value + "-"
                            If Dgl1.Item(Col1Dimension2, I).Value <> "" And Dgl1.Columns(Col1Dimension2).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension2, I).Value + "-"
                            If Dgl1.Item(Col1Dimension3, I).Value <> "" And Dgl1.Columns(Col1Dimension3).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension3, I).Value + "-"
                            If Dgl1.Item(Col1Dimension4, I).Value <> "" And Dgl1.Columns(Col1Dimension4).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension4, I).Value + "-"
                            If Dgl1.Columns(Col1Pcs).DisplayIndex < Dgl1.Columns(Col1Qty).DisplayIndex Then
                                mDimensionDetail += Math.Abs(AgL.VNull(Dgl1.Item(Col1Pcs, I).Value)).ToString + " X " + Math.Abs(AgL.VNull(Dgl1.Item(Col1Qty, I).Value)).ToString
                            Else
                                mDimensionDetail += Math.Abs(AgL.VNull(Dgl1.Item(Col1Qty, I).Value)).ToString + " X " + Math.Abs(AgL.VNull(Dgl1.Item(Col1Pcs, I).Value)).ToString
                            End If
                        End If
                    Else
                        mQry = "Update SaleInvoiceDimensionDetail Set 
                            Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, I).Tag) & ",
                            Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Tag) & ",
                            Pcs = " & Val(Dgl1.Item(Col1Pcs, I).Value) & ",
                            Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ",
                            TotalQty = " & Val(Dgl1.Item(Col1TotalQty, I).Value) & ",
                            UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, I).Value) & ",
                            DealQty = " & Val(Dgl1.Item(Col1DealQty, I).Value) & ",
                            TotalDealQty = " & Val(Dgl1.Item(Col1TotalDealQty, I).Value) & "
                            Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & "
                            "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Update SaleInvoiceDimensionDetailSku " &
                                " SET ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ", " &
                                " ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ", " &
                                " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                                " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                                " Dimension3 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ", " &
                                " Dimension4 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & ", " &
                                " Size = " & AgL.Chk_Text(Dgl1.Item(Col1Size, I).Tag) & " " &
                                " Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)



                        mQry = "Update Stock Set V_Type = " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowV_Type).Tag) & ",
                                    V_Prefix = " & AgL.Chk_Text(mObjFrmSaleInvoice.LblPrefix.Text) & ", 
                                    V_Date = " & AgL.Chk_Date(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowV_Date).Value) & ", 
                                    V_No = " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowV_No).Value) & ", 
                                    RecId = " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowReferenceNo).Value) & ",
                                    Div_Code = " & AgL.Chk_Text(mObjFrmSaleInvoice.TxtDivision.Tag) & ", 
                                    Site_Code = " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowSite_Code).Tag) & ", 
                                    Subcode = " & AgL.Chk_Text(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowSaleToParty).Tag) & ", 
                                    SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ", 
                                    Item = " & AgL.Chk_Text(IIf(AgL.XNull(Dgl1(Col1SKU, I).Tag) = "", Dgl1.Item(Col1Item, I).Tag, AgL.XNull(Dgl1(Col1SKU, I).Tag))) & ",
                                    SalesTaxGroupItem = " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1SalesTaxGroup, mGridRowIndex).Tag) & ",
                                    LotNo = " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1LotNo, mGridRowIndex).Value) & ", 
                                    Godown = " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1Godown, mGridRowIndex).Value) & ", 
                                    EType_IR = 'I', 
                                    Qty_Iss = " & Val(Dgl1(Col1TotalQty, I).Value) & ", 
                                    Qty_Rec=0,
                                    Unit=" & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1Unit, mGridRowIndex).Value) & ", 
                                    UnitMultiplier = " & Val(Dgl1(Col1UnitMultiplier, I).Value) & ", 
                                    DealQty_Iss = " & Val(Dgl1(Col1TotalDealQty, I).Value) & ", 
                                    DealQty_Rec = 0, 
                                    DealUnit = " & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1DealUnit, mGridRowIndex).Value) & ", 
                                    Rate=" & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1Rate, mGridRowIndex).Value) & ", 
                                    Amount=" & AgL.Chk_Text(mObjFrmSaleInvoice.Dgl1(FrmSaleInvoiceDirect_WithDimension.Col1Amount, mGridRowIndex).Value) & ",
                                    Landed_Value=0                                
                                    Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & "
                                "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        If mDimensionDetail <> "" Then
                            If ClsMain.FDivisionNameForCustomization(13) = "ARORA TEXTILE" Then
                                mDimensionDetail += " + "
                            Else
                                mDimensionDetail += ", "
                            End If

                            If Dgl1.Item(Col1Dimension1, I).Value <> "" And Dgl1.Columns(Col1Dimension1).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension1, I).Value + "-"
                            If Dgl1.Item(Col1Dimension2, I).Value <> "" And Dgl1.Columns(Col1Dimension2).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension2, I).Value + "-"
                            If Dgl1.Item(Col1Dimension3, I).Value <> "" And Dgl1.Columns(Col1Dimension3).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension3, I).Value + "-"
                            If Dgl1.Item(Col1Dimension4, I).Value <> "" And Dgl1.Columns(Col1Dimension4).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension4, I).Value + "-"
                            If Dgl1.Columns(Col1Pcs).DisplayIndex < Dgl1.Columns(Col1Qty).DisplayIndex Then
                                mDimensionDetail += Math.Abs(AgL.VNull(Dgl1.Item(Col1Pcs, I).Value)).ToString + " X " + Math.Abs(AgL.VNull(Dgl1.Item(Col1Qty, I).Value)).ToString
                            Else
                                mDimensionDetail += Math.Abs(AgL.VNull(Dgl1.Item(Col1Qty, I).Value)).ToString + " X " + Math.Abs(AgL.VNull(Dgl1.Item(Col1Pcs, I).Value)).ToString
                            End If
                        Else
                            If Dgl1.Item(Col1Dimension1, I).Value <> "" And Dgl1.Columns(Col1Dimension1).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension1, I).Value + "-"
                            If Dgl1.Item(Col1Dimension2, I).Value <> "" And Dgl1.Columns(Col1Dimension2).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension2, I).Value + "-"
                            If Dgl1.Item(Col1Dimension3, I).Value <> "" And Dgl1.Columns(Col1Dimension3).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension3, I).Value + "-"
                            If Dgl1.Item(Col1Dimension4, I).Value <> "" And Dgl1.Columns(Col1Dimension4).Visible = True Then mDimensionDetail += Dgl1.Item(Col1Dimension4, I).Value + "-"
                            If Dgl1.Columns(Col1Pcs).DisplayIndex < Dgl1.Columns(Col1Qty).DisplayIndex Then
                                mDimensionDetail = Math.Abs(AgL.VNull(Dgl1.Item(Col1Pcs, I).Value)).ToString + " X " + Math.Abs(AgL.VNull(Dgl1.Item(Col1Qty, I).Value)).ToString
                            Else
                                mDimensionDetail = Math.Abs(AgL.VNull(Dgl1.Item(Col1Qty, I).Value)).ToString + " X " + Math.Abs(AgL.VNull(Dgl1.Item(Col1Pcs, I).Value)).ToString
                            End If
                        End If
                    End If
                End If
            Else
                If Dgl1.Item(ColSNo, I).Tag IsNot Nothing Then
                    mQry = "Delete from SaleInvoiceDimensionDetailSku  Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & ""
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = "Delete from SaleInvoiceDimensionDetail  Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & ""
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = "Delete from Stock Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & ""
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next

        If ClsMain.FDivisionNameForCustomization(13) = "ARORA TEXTILE" Then
            mDimensionDetail = mDimensionDetail & " "
            mDimensionDetail = mDimensionDetail.Replace(" X 1 ", " ")
        End If

        mQry = "Update SaleInvoiceDetail Set DimensionDetail = " & AgL.Chk_Text(mDimensionDetail) & " Where DocID = '" & DocId & "' and Sr = " & TSr & "  "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If EntryMode = "Browse" Then
            Select Case e.KeyCode
                Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
                Case Else
                    e.Handled = True
            End Select
            Exit Sub
        End If

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                sender.Rows(sender.currentcell.rowindex).Visible = False
                Calculation()
                e.Handled = True
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub



    End Sub
    Private Sub ApplyUISetting()
        Me.Name = "FrmSaleInvoiceDimension"
        ClsMain.GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode,
                objFrmSaleInvoice.LblV_Type.Tag, objFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, objFrmSaleInvoice.rowV_Type).Tag,
                "", objFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, objFrmSaleInvoice.rowSettingGroup).Tag, ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub

    Private Sub FCreateHelpDimension2(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
                End If
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension2 & "' "



        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            FCreateHelpDimension2(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FReInitializeDimensionColumns()
        Try
            For I As Integer = 0 To Dgl1.Rows.Count - 1
                If AgL.VNull(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                    If Dgl1.Columns(Col1ItemType).Visible = False Then
                        Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemType).Tag)
                        Dgl1.Item(Col1ItemType, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemType).Value)
                    End If
                    If Dgl1.Columns(Col1ItemCategory).Visible = False Then
                        Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory).Tag)
                        Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory).Value)
                    End If
                    If Dgl1.Columns(Col1ItemGroup).Visible = False Then
                        Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup).Tag)
                        Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup).Value)
                    End If
                    If Dgl1.Columns(Col1Item).Visible = False Then
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Item).Tag)
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Item).Value)
                    End If
                    If Dgl1.Columns(Col1Dimension1).Visible = False Then
                        Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension1).Tag)
                        Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension1).Value)
                    End If
                    If Dgl1.Columns(Col1Dimension2).Visible = False Then
                        Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension2).Tag)
                        Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension2).Value)
                    End If
                    If Dgl1.Columns(Col1Dimension3).Visible = False Then
                        Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension3).Tag)
                        Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension3).Value)
                    End If
                    If Dgl1.Columns(Col1Dimension4).Visible = False Then
                        Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension4).Tag)
                        Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Dimension4).Value)
                    End If
                    If Dgl1.Columns(Col1Size).Visible = False Then
                        Dgl1.Item(Col1Size, I).Tag = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Size).Tag)
                        Dgl1.Item(Col1Size, I).Value = AgL.XNull(DglRow.Cells(FrmSaleInvoiceDirect_WithDimension.Col1Size).Value)
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class