Imports Customised.ClsMain

Public Class FrmLRTransfer
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public DtV_TypeSettings As DataTable
    Protected Const Col1Select As String = "Tick"
    Public Const ColSNo As String = "S.No."
    Public Const Col1LRNo As String = "LR No"
    Public Const Col1LRDate As String = "LR Date"
    Public Const Col1Party As String = "Party"
    Public Const Col1TotalBales As String = "Total Bales"
    Public Const Col1TotalInvoiceAmount As String = "Total Invoice Amount"
    Public Const Col1TotalWeight As String = "Total Weight"

    Dim mQry As String = ""

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
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Select, 35, 0, Col1Select, True, True, False)
            .AddAgTextColumn(Dgl1, Col1LRNo, 100, 0, Col1LRNo, True, True)
            .AddAgTextColumn(Dgl1, Col1LRDate, 100, 0, Col1LRDate, True, True)
            .AddAgTextColumn(Dgl1, Col1Party, 100, 0, Col1Party, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalBales, 70, 8, 4, False, Col1TotalBales, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalInvoiceAmount, 70, 8, 4, False, Col1TotalInvoiceAmount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalWeight, 70, 8, 4, False, Col1TotalWeight, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        AgL.GridDesign(Dgl1)

        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Columns(Col1Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        MovRec()
    End Sub
    Private Sub MovRec()
        Dim mQry As String = ""

        LblTotalPcs.Text = 0
        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0

        mQry = " Select Bc.Code As LRNoCode, Bc.Specification1 As LRNo
                From BarCode Bc 
                LEFT JOIN BarCodeSiteDetail Bcsd On Bc.Code = Bcsd.Code
                Where Bcsd.CurrentGodown = 'Transport' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I As Integer = 0 To .Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1Select, I).Value = "o"
                    Dgl1.Item(Col1LRNo, I).Tag = AgL.XNull(.Rows(I)("LRNoCode"))
                    Dgl1.Item(Col1LRNo, I).Value = AgL.XNull(.Rows(I)("LRNo"))

                    'If Dgl1.Item(Col1Select, I).Value = "þ" Then
                    '    LblTotalPcs.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                    '    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                    '    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                    'End If
                Next I
            End If
        End With
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
    Private Sub Dgl1_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgl1.MouseUp
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Select).Index Then
                            ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1Item).Index)
                            FSave(mSearchCode, Dgl1.Item(ColSNo, mRowIndex).Tag, Dgl1.Item(Col1Select, mRowIndex).Value, mRowIndex)
                        End If
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1Item).Index)
                        FSave(mSearchCode, Dgl1.Item(ColSNo, mRowIndex).Tag, Dgl1.Item(Col1Select, mRowIndex).Value, mRowIndex)
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub FSave(DocId As String, Sr As Integer, SelectValue As String, mRowIndex As Integer)
        'If SelectValue = "þ" Then
        '    If AgL.PubServerName = "" Then
        '        mQry = "UPDATE SaleInvoiceDetail Set ReconcileDateTime = strftime('%Y-%m-%d %H:%M:%S','now'), ReconcileBy = '" & AgL.PubUserName & "'
        '            Where DocId = '" & DocId & "' And Sr = " & Sr & ""
        '    Else
        '        mQry = "UPDATE SaleInvoiceDetail Set ReconcileDateTime = getdate(), ReconcileBy = '" & AgL.PubUserName & "'
        '            Where DocId = '" & DocId & "' And Sr = " & Sr & ""
        '    End If
        '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        '    Dgl1.Rows(mRowIndex).DefaultCellStyle.BackColor = ColorConstants.Verified
        'ElseIf SelectValue = "o" Then
        '    mQry = "UPDATE SaleInvoiceDetail Set ReconcileDateTime = Null, ReconcileBy = Null
        '            Where DocId = '" & DocId & "' And Sr = " & Sr & ""
        '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        '    Dgl1.Rows(mRowIndex).DefaultCellStyle.BackColor = Color.White
        'End If
    End Sub
    Private Sub Calculation()
        LblTotalPcs.Text = 0
        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0
        For I As Integer = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Select, I).Value = "þ" Then
                LblTotalPcs.Text = Val(LblTotalPcs.Text) + Val(Dgl1.Item(Col1Pcs, I).Value)
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
End Class