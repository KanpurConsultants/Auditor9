Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPurchInvoiceMaterialIssue
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1EntryDate As String = "Entry Date"
    Public Const Col1EntryNo As String = "Entry No"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1DocQty As String = "Doc Qty"
    Public Const Col1Unit As String = "Unit"


    Dim mSearchCode As String
    Dim mSearchCodeSr As Integer

    Dim DtItemRelation As DataTable

    Dim mMaterialIssueDocId$ = ""
    Dim mVendorCode$ = ""
    Dim mUnitDecimalPlace As Integer
    Dim mDglRow As DataGridViewRow
    Dim mDtV_TypeSettings As DataTable
    Dim mObjFrm As Object
    Public Property ObjFrm() As Object
        Get
            ObjFrm = mObjFrm
        End Get
        Set(ByVal value As Object)
            mObjFrm = value
        End Set
    End Property
    Public Property MaterialIssueDocId() As String
        Get
            MaterialIssueDocId = mMaterialIssueDocId
        End Get
        Set(ByVal value As String)
            mMaterialIssueDocId = value
        End Set
    End Property
    Public Property VendorCode() As String
        Get
            VendorCode = mVendorCode
        End Get
        Set(ByVal value As String)
            mVendorCode = value
        End Set
    End Property
    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub IniGrid(DocID As String)
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1EntryDate, 100, 0, Col1EntryDate, True, False)
            .AddAgTextColumn(Dgl1, Col1EntryNo, 100, 0, Col1EntryNo, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 230, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 100, 8, 2, False, Col1DocQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.Name = "Dgl1"
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)
        Dgl1.ReadOnly = True
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        FMoverec()

        ApplyUISetting()
    End Sub
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
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.Click, BtnAdd.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnClose.Name
                mOkButtonPressed = True
                Me.Close()

            Case BtnAdd.Name
                Me.Close()
                Dim StrUserPermission As String
                Dim DTUP As DataTable
                Dim objMdi As New MDIMain
                StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, objMdi.MnuItemMaster.Name, objMdi.MnuItemMaster.Text, DTUP)
                Dim FrmObj As New FrmStockEntry(StrUserPermission, DTUP, Ncat.StockIssue)
                FrmObj.MdiParent = ObjFrm.MdiParent
                FrmObj.Show()
                FrmObj.Topctrl1.FButtonClick(0)
                FrmObj.DglMain.Item(FrmStockEntry.Col1Value, FrmObj.rowParty).Tag = VendorCode
                FrmObj.DglMain.Item(FrmStockEntry.Col1Value, FrmObj.rowParty).Value = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup Where SubCode = '" & VendorCode & "'", AgL.GCn).ExecuteScalar())
                FrmObj.DglMain.Item(FrmStockEntry.Col1Value, FrmObj.rowV_Date).Value = ObjFrm.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, ObjFrm.rowV_Date).Value
                FrmObj.Validating_SaleToParty(FrmObj.DglMain.Item(FrmStockEntry.Col1Value, FrmObj.rowParty).Tag)
                FrmObj.Dgl2.Item(FrmStockEntry.Col1Value, FrmObj.rowReferenceDocId).Tag = mSearchCode
                FrmObj.Dgl2.Item(FrmStockEntry.Col1Value, FrmObj.rowReferenceDocId).Value = ObjFrm.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, ObjFrm.rowV_Type).Tag + "-" + ObjFrm.DglMain.Item(FrmStockEntry.Col1Value, ObjFrm.rowReferenceNo).Value

                mQry = "Select SubCode As Code, Name 
                        From SubGroup 
                        Where SubGroupType = '" & SubgroupType.Godown & "'"
                Dim DtGodown As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtGodown.Rows.Count = 1 Then
                    FrmObj.Dgl2.Item(FrmStockEntry.Col1Value, FrmObj.rowGodown).Tag = AgL.XNull(DtGodown.Rows(0)("Code"))
                    FrmObj.Dgl2.Item(FrmStockEntry.Col1Value, FrmObj.rowGodown).Value = AgL.XNull(DtGodown.Rows(0)("Name"))
                End If

                If ClsMain.FDivisionNameForCustomization(14) = "PRATHAM APPARE" Then
                    FrmObj.DglMain.Item(FrmStockEntry.Col1Value, FrmObj.rowSettingGroup).Tag = ClsGarmentProduction.SettingGroup_RawAndOtherMaterial
                    FrmObj.DglMain.Item(FrmStockEntry.Col1Value, FrmObj.rowSettingGroup).Value = AgL.XNull(AgL.Dman_Execute("Select Name From SettingGroup Where Code = '" & FrmObj.DglMain.Item(FrmStockEntry.Col1Value, FrmObj.rowSettingGroup).Tag & "'", AgL.GCn).ExecuteScalar())
                    FrmObj.IniGrid()
                End If
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Select Case e.KeyCode
            Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
            Case Else
                e.Handled = True
        End Select
        Exit Sub

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                sender.Rows(sender.currentcell.rowindex).Visible = False
                e.Handled = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub
    Public Sub FMoverec()
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer

        mQry = "Select H.V_Type || '-' || H.ManualRefNo As ManualRefNo, H.V_Date, L.*, 
                Pi.V_Type || '-' || Pi.ManualRefNo As PurchInvoiceNo, Barcode.Description as BarcodeName, 
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
                D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc
                From (Select * From StockHeadDetail  With (NoLock)  Where DocId In ('" & Replace(mMaterialIssueDocId, ",", "','") & "')) As L 
                LEFT JOIN StockHeadDetailSku Sids With (NoLock) On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                LEFT JOIN StockHead H ON L.DocId = H.DocID
                LEFT JOIN PurchInvoice Pi  With (NoLock) On L.ReferenceDocId = Pi.DocId 
                LEFT JOIN Voucher_Type Vt With (NoLock) On Pi.V_Type = Vt.V_Type
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
                Order By H.V_Date, H.V_No, L.Sr "


        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))

                    Dgl1.Item(Col1EntryNo, I).Tag = AgL.XNull(.Rows(I)("DocId"))
                    Dgl1.Item(Col1EntryNo, I).Value = AgL.XNull(.Rows(I)("ManualRefNo"))
                    Dgl1.Item(Col1EntryDate, I).Value = AgL.XNull(.Rows(I)("V_Date"))

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

                    Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))

                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Unit, I).Tag = AgL.VNull(.Rows(I)("ShowDimensionDetailInSales"))
                    If AgL.VNull(Dgl1.Item(Col1Unit, I).Tag) Then
                        Dgl1.Item(Col1DocQty, I).Style.ForeColor = Color.Blue
                        ShowStockEntryDimensionDetail(I, False)
                    End If
                Next I
            End If
        End With
    End Sub
    Private Sub ApplyUISetting()
        Dim bNCat As String = ""
        bNCat = Ncat.StockIssue
        ClsMain.GetUISetting(Dgl1, "FrmStockEntry", AgL.PubDivCode, AgL.PubSiteCode,
                             bNCat, "", "", ClsGarmentProduction.SettingGroup_RawAndOtherMaterial, ClsMain.GridTypeConstants.HorizontalGrid)
        Dgl1.Columns(Col1EntryNo).Visible = True
        Dgl1.Columns(Col1EntryDate).Visible = True

    End Sub
    Private Sub Dgl1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Dgl1.Columns(e.ColumnIndex).Name = Col1DocQty Then
                Dim mRow As Integer = e.RowIndex
                ShowStockEntryDimensionDetail(mRow)
            Else
                Me.Close()
                ClsMain.FOpenForm(Dgl1.Item(Col1EntryNo, Dgl1.CurrentCell.RowIndex).Tag, ObjFrm)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ShowStockEntryDimensionDetail(mRow As Integer, Optional IsShowFrm As Boolean = True)
        If mRow < 0 Then Exit Sub
        If Dgl1.Item(Col1DocQty, mRow).Tag IsNot Nothing Then
            If IsShowFrm = True Then
                Dgl1.Item(Col1DocQty, mRow).Tag.ShowDialog()
            End If
        Else
            If Dgl1.Item(Col1Unit, mRow).Tag Then
                Dim FrmObj As FrmPurchInvoiceMaterialssueDimension
                FrmObj = New FrmPurchInvoiceMaterialssueDimension
                FrmObj.Unit = Dgl1.Item(Col1Unit, mRow).Value
                FrmObj.UnitDecimalPlace = 3
                FrmObj.IniGrid(Dgl1.Item(Col1EntryNo, mRow).Tag, Val(Dgl1.Item(ColSNo, mRow).Tag))
                Dgl1.Item(Col1DocQty, mRow).Tag = FrmObj
                If IsShowFrm = True Then
                    Dgl1.Item(Col1DocQty, mRow).Tag.ShowDialog()
                End If
            End If
        End If
    End Sub
End Class