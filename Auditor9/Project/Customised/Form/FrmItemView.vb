Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmItemView
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Dim rowItemName As Integer = 0
    Dim rowSaleRate As Integer = 1
    Dim rowPurchaseRate As Integer = 2
    Dim rowStock As Integer = 3

    Public Const hcItemName As String = "Item Name"
    Public Const hcSaleRate As String = "Sale Rate"
    Public Const hcPurchaseRate As String = "Purchase Rate"
    Public Const hcStock As String = "Stock"

    Dim mQry As String = ""
    Dim IsFrmLoaded As Boolean = False
    Dim mSearchCode$ = ""
    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, Col1Head, 250, 0, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1Value, 400, 0, Col1Value, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.ColumnHeadersVisible = False
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)


        Dgl1.Rows.Add(4)

        Dgl1.Item(Col1Head, rowItemName).Value = hcItemName
        Dgl1.Item(Col1Head, rowSaleRate).Value = hcSaleRate
        Dgl1.Item(Col1Head, rowPurchaseRate).Value = hcPurchaseRate
        Dgl1.Item(Col1Head, rowStock).Value = hcStock

        Dgl1.Rows(rowPurchaseRate).Visible = False

        Dgl1.Item(Col1Value, rowItemName).Style.BackColor = Color.White
    End Sub
    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        MoveRec()
        Me.StartPosition = FormStartPosition.CenterParent
        TxtItem.Focus()
        IsFrmLoaded = True
    End Sub
    Public Sub MoveRec()
        Dim mQry As String = ""

        If mSearchCode <> "" And AgL.XNull(TxtItem.Tag) = "" Then TxtItem.Tag = mSearchCode

        If AgL.XNull(TxtItem.Tag) <> "" Then
            mQry = " SELECT I.Description, I.Rate As SaleRate, I.PurchaseRate 
                    FROM Item I WHERE I.Code =  '" & TxtItem.Tag & "' "
            Dim DtItemDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtItemDetail.Rows.Count > 0 Then
                TxtItem.Text = AgL.XNull(DtItemDetail.Rows(0)("Description"))
                Dgl1.Item(Col1Value, rowItemName).Value = AgL.XNull(DtItemDetail.Rows(0)("Description"))
                Dgl1.Item(Col1Value, rowSaleRate).Value = AgL.XNull(DtItemDetail.Rows(0)("SaleRate"))
                Dgl1.Item(Col1Value, rowPurchaseRate).Value = AgL.XNull(DtItemDetail.Rows(0)("PurchaseRate"))
            End If

            mQry = " SELECT L.Item, IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) AS StockQty
                    FROM Stock L 
                    WHERE L.Item = '" & TxtItem.Tag & "'
                    GROUP BY L.Item "
            Dim DtStock As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtStock.Rows.Count > 0 Then
                Dgl1.Item(Col1Value, rowStock).Value = AgL.VNull(DtStock.Rows(0)("StockQty"))
            Else
                Dgl1.Item(Col1Value, rowStock).Value = 0
            End If

            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        End If
        TxtItem.Focus()
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Dgl1_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl1_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub


    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If IsFrmLoaded = True Then
                TxtItem.Focus()
                Dgl1.CurrentCell = Nothing
                IsFrmLoaded = False
            End If
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).ReadOnly = True

            'Select Case Dgl1.CurrentCell.RowIndex
            '    Case rowPartyName
            '        Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).ReadOnly = False
            '    Case Else
            '        Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).ReadOnly = True
            'End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtItem.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtItem.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = " Select I.Code As Code, I.Description 
                                    From Item I Where I.V_Type = '" & ItemV_Type.Item & "' "
                            TxtItem.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TxtItem_Validating(sender As Object, e As CancelEventArgs) Handles TxtItem.Validating
        If AgL.XNull(TxtItem.Tag) <> "" And AgL.XNull(TxtItem.Tag) <> AgL.XNull(TxtItem.AgLastValueTag) Then
            MoveRec()
            TxtItem.Focus()
            TxtItem.SelectionStart = TxtItem.Text.Length
            TxtItem.SelectionLength = 0
            Dgl1.CurrentCell = Nothing
        End If
    End Sub
    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            'Topctrl1.TopKey_Down(e)
        End If

        If Me.ActiveControl IsNot Nothing Then
            If TypeOf (Me.ActiveControl) Is TextBox Then
                If Not CType(Me.ActiveControl, TextBox).Multiline Then
                    If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
                End If
            End If

            'If e.KeyCode = Keys.Insert Then OpenLinkForm(Me.ActiveControl)
        End If
    End Sub
End Class