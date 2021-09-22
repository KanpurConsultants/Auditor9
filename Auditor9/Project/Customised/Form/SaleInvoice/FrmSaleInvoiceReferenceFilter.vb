Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Public Class FrmSaleInvoiceReferenceFilter
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Public Const rowInvoiceNo As Integer = 0
    Public Const rowFromDate As Integer = 1
    Public Const rowToDate As Integer = 2

    Public Const HcInvoiceNo As String = "Invoice No"
    Public Const HcFromDate As String = "From Date"
    Public Const HcToDate As String = "To Date"

    Public DrSelected As DataRow()
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, False, True)
            .AddAgTextColumn(Dgl1, Col1Value, 350, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.TabIndex = Pnl1.TabIndex
        AgL.GridDesign(Dgl1)

        Dgl1.Rows.Add(3)
        Dgl1.Item(Col1Head, rowInvoiceNo).Value = HcInvoiceNo
        Dgl1.Item(Col1Head, rowFromDate).Value = HcFromDate
        Dgl1.Item(Col1Head, rowToDate).Value = HcToDate

        Dgl1.Item(Col1Value, rowFromDate).Value = AgL.PubLoginDate
        Dgl1.Item(Col1Value, rowToDate).Value = AgL.PubLoginDate
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Me.Top = 230
            Me.Left = 300
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

            'If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Select Case Dgl1.CurrentCell.RowIndex
                Case rowInvoiceNo
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT H.DocID AS Code, H.V_Type + '-' + H.ManualRefNo AS InvoiceNo, H.V_Date As InvoiceDate
                                FROM SaleInvoice H 
                                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                                WHERE Vt.NCat = '" & Ncat.SaleInvoice & "'
                                And H.Site_Code = '" & AgL.PubSiteCode & "'
                                And H.Div_Code = '" & AgL.PubDivCode & "'"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim dtTemp As DataTable
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowInvoiceNo
            End Select
            'Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0

        Select Case sender.Name
            Case BtnOk.Name
                DrSelected = FOpenSelectionWindow()
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub
    Private Sub FrmSaleInvoiceParty_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub
    Private Sub FrmSaleInvoiceParty_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Dgl1 IsNot Nothing Then
            If Dgl1.FirstDisplayedCell IsNot Nothing Then
                Dgl1.CurrentCell = Dgl1.FirstDisplayedCell 'Dgl1(Col1Value, rowMobile)
                Dgl1.Focus()
            End If
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Function FOpenSelectionWindow() As DataRow()
        Dim DtTemp As DataTable
        Dim StrRtn As String = ""
        Dim mRow As Integer = 0

        mQry = "SELECT 'o' As Tick, L.DocID + CAST(L.Sr AS NVARCHAR) AS SearchKey, H.V_Type + '-' + H.ManualRefNo AS SaleInvoiceNo,
                H.V_Date As InvoiceDate,
                Ic.Description As ItemCategory, Ig.Description As ItemGroup, I.Description As Item,
                D1.Description As Dimension1, D2.Description As Dimension2, 
                D3.Description As Dimension3, D4.Description As Dimension4,
                Size.Description As Size, L.Qty, L.Unit,
                Ic.Code As ItemCategoryCode, Ig.Code As ItemGroupCode, I.Code As ItemCode,
                D1.Code As Dimension1Code, D2.Code As Dimension2Code, 
                D3.Code As Dimension3Code, D4.Code As Dimension4Code, I.SalesTaxPostingGroup,
                Size.Code As SizeCode, It.Code As ItemTypeCode, It.Name As ItemType,
                L.SaleInvoice, L.SaleInvoiceSr 
                FROM SaleInvoice H
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN Item Sku ON Sku.Code = L.Item
                LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                LEFT JOIN Item IC On IfNull(Sku.ItemCategory,Sku.Code) = IC.Code
                LEFT JOIN Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size 
                WHERE Vt.NCat = '" & Ncat.SaleInvoice & "' 
                And H.Site_Code = '" & AgL.PubSiteCode & "'
                And H.Div_Code = '" & AgL.PubDivCode & "'"

        If AgL.XNull(Dgl1.Item(Col1Value, rowInvoiceNo).Tag) <> "" Then
            mQry += " And H.DocId = '" & AgL.XNull(Dgl1.Item(Col1Value, rowInvoiceNo).Tag) & "'"
        End If
        If AgL.XNull(Dgl1.Item(Col1Value, rowFromDate).Value) <> "" Then
            mQry += " And Date(H.V_Date) >= " & AgL.Chk_Date(CDate(Dgl1.Item(Col1Value, rowFromDate).Value)) & ""
        End If
        If AgL.XNull(Dgl1.Item(Col1Value, rowToDate).Value) <> "" Then
            mQry += " And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(Dgl1.Item(Col1Value, rowToDate).Value)) & ""
        End If
        mQry += " Order By H.V_Date "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 990, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Invoice No.", 90, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Invoice Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, AgL.PubCaptionItemCategory, 90, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[ItemCategory] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(5, AgL.PubCaptionItemGroup, 90, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[ItemGroup] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(6, AgL.PubCaptionItem, 180, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Item] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(7, AgL.PubCaptionDimension1, 90, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension1] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(8, AgL.PubCaptionDimension2, 90, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension2] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(9, AgL.PubCaptionDimension3, 90, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension3] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(10, AgL.PubCaptionDimension4, 90, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension4] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(11, "Size", 90, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Size] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(12, "Qty", 90, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(13, "Unit", 70, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(14, , 0, , False)
        FRH_Multiple.FFormatColumn(15, , 0, , False)
        FRH_Multiple.FFormatColumn(16, , 0, , False)
        FRH_Multiple.FFormatColumn(17, , 0, , False)
        FRH_Multiple.FFormatColumn(18, , 0, , False)
        FRH_Multiple.FFormatColumn(19, , 0, , False)
        FRH_Multiple.FFormatColumn(20, , 0, , False)
        FRH_Multiple.FFormatColumn(21, , 0, , False)
        FRH_Multiple.FFormatColumn(22, , 0, , False)
        FRH_Multiple.FFormatColumn(23, , 0, , False)
        FRH_Multiple.FFormatColumn(24, , 0, , False)
        FRH_Multiple.FFormatColumn(25, , 0, , False)
        FRH_Multiple.FFormatColumn(26, , 0, , False)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If

        Dim DrSelected As DataRow()
        If StrRtn <> "" Then
            DrSelected = DtTemp.Select("SearchKey In (" & StrRtn & ")")
        End If
        FOpenSelectionWindow = DrSelected
    End Function
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            'If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowInvoiceNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 50
                Case rowFromDate, rowToDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class