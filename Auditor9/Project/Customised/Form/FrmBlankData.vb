Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.FrmSaleInvoiceDirect_WithDimension
Public Class FrmBlankData
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_ExternalDatabase As New SQLite.SQLiteConnection
    Public mDbPath As String = ""
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker


    Public Const Col1Head As String = "Head"
    Public Const Col1Status As String = "Status"
    Public Const Col1Message As String = "Message"

    Dim rowDataSyncFromDate As Integer = 50
    Public Const hcDataSyncFromDate As String = "Data Sync From Date"


    Dim mParentPrgBarMaxVal As Integer = 0


    Private Delegate Sub UpdateParentProgressBarInvoker(ByVal Value As String, ParentPrMaxVal As Integer)
    Private Delegate Sub FRecordMessageInvoker(Head As String, Status As String, Message As String, Conn As Object, Cmd As Object)
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        'BtnOK.Enabled = False
        If Not TxtPassword.Text = "P@ssw0rd!" Then
            MsgBox("Incorrect Password", MsgBoxStyle.Information)
            Exit Sub
        End If
        _backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        _backgroundWorker1.WorkerSupportsCancellation = False
        _backgroundWorker1.WorkerReportsProgress = False
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FProcSave)
        _backgroundWorker1.RunWorkerAsync()
    End Sub
    Private Sub Ini_Grid()
        With AgCL
            .AddAgTextColumn(Dgl1, Col1Head, 400, 0, " ", True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Status, 200, 0, " ", True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Message, 700, 0, " ", True, True,,, DataGridViewColumnSortMode.Automatic)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 25
        AgL.GridDesign(Dgl1)
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.AllowUserToAddRows = False
        Dgl1.CellBorderStyle = DataGridViewCellBorderStyle.None
        Dgl1.BorderStyle = BorderStyle.None
        Dgl1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        For I As Integer = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).DefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8)
        Next
    End Sub
    Private Sub FrmImportFromExcel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
    End Sub
    Public Sub FProcSave()
        Dim mTrans As String = ""

        Dim mSr As Integer = 0
        mQry = "Select " & mSr & " As Sr, 'Cloth_SupplierSettlementInvoicesLine' As TableName Union All " : mSr = mSr + 1
            mQry += "Select " & mSr & " As Sr, 'Cloth_SupplierSettlementInvoices' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr, 'Cloth_SupplierSettlementInvoicesAdjustment' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr, 'Cloth_SupplierSettlementPayments' As TableName Union All " : mSr = mSr + 1

        mQry += "Select " & mSr & " As Sr,'Stock' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockProcess' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceBarcodeLastTransactionValues' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceDetailBarCodeValues' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceDimensionDetailSku' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceDimensionDetail' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceDetailSku' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceDetailHelpValues' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceDetail' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceTransport' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoicePayment' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoice' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'SaleInvoiceTrnSetting' As TableName Union All " : mSr = mSr + 1

        mQry += "Select " & mSr & " As Sr,'PurchInvoiceDimensionDetailSku' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'PurchInvoiceDimensionDetail' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'PurchInvoiceDetailSku' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'PurchInvoiceDetail' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'PurchInvoiceTransport' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'PurchInvoicePayment' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'PurchInvoice' As TableName Union All " : mSr = mSr + 1


        mQry += "Select " & mSr & " As Sr,'LedgerHeadDetailCharges' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'LedgerHeadDetail' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'LedgerHeadCharges' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'LedgerHead' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'LedgerM' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'Ledger' As TableName Union All " : mSr = mSr + 1

        mQry += "Select " & mSr & " As Sr,'StockHeadDetailBarCodeValues' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHeadDetailBase' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHeadDimensionDetailSku' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHeadDimensionDetail' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHeadDetailBomSku' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHeadDetailBom' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHeadDetailTransfer' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHeadDetailSku' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHeadDetail' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'StockHead' As TableName Union All " : mSr = mSr + 1


        mQry += "Select " & mSr & " As Sr,'TransactionReferences' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'ItemGroupPerson' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'LogTable' As TableName Union All " : mSr = mSr + 1

        mQry += "Select " & mSr & " As Sr,'Item' As TableName Union All " : mSr = mSr + 1
        mQry += "Select " & mSr & " As Sr,'Subgroup' As TableName  "


        Dim DtTables As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mParentPrgBarMaxVal = DtTables.Rows.Count

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I As Integer = 0 To DtTables.Rows.Count - 1
                UpdateParentProgressBar("Deleting Data From " & AgL.XNull(DtTables.Rows(I)("TableName")) & ".", mParentPrgBarMaxVal)
                If AgL.XNull(DtTables.Rows(I)("TableName")) = "Item" Then
                    mQry = " Delete From " & DtTables.Rows(I)("TableName") & ""
                    mQry += " Where Code Like '%D1%'"
                    mQry += " Or Code Like '%E1%'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                ElseIf AgL.XNull(DtTables.Rows(I)("TableName")) = "SubGroup" Then
                    mQry = " Delete From " & DtTables.Rows(I)("TableName") & ""
                    mQry += " Where SubCode Like '%D1%'"
                    mQry += " Or SubCode Like '%E1%'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    mQry = " Delete From " & DtTables.Rows(I)("TableName") & ""
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If

                FRecordMessage(LblParentProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

            MsgBox("Process Completed Successfully...", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            AgL.ETrans.Rollback()
        End Try


        UpdateParentProgressBar(" ", 1)
    End Sub
    Public Sub UpdateParentProgressBar(ByVal Value As String, ParentPrMaxVal As Integer)
        If Me.LblParentProgress.InvokeRequired Then
            Me.LblParentProgress.Invoke(New UpdateParentProgressBarInvoker(AddressOf Me.UpdateParentProgressBar), New Object() {Value, ParentPrMaxVal})
        Else
            Me.LblParentProgress.Text = Value
            PrgBarParent.Maximum = ParentPrMaxVal
            If Me.LblParentProgress.Text = " " Then
                PrgBarParent.Value = 0
            Else
                PrgBarParent.Increment(1)
            End If
            LblParentProgress.Refresh()
        End If
    End Sub

    Private Sub DGL1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If e.RowIndex > -1 Then Dgl1.Rows(e.RowIndex).Selected = True
        Dgl1.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray
    End Sub
    Private Sub FRecordMessage(Head As String, Status As String, Message As String, Conn As Object, Cmd As Object)
        If Me.Dgl1.InvokeRequired Then
            Me.Dgl1.Invoke(New FRecordMessageInvoker(AddressOf Me.FRecordMessage), New Object() {Head, Status, Message, Conn, Cmd})
        Else
            Dgl1.Rows.Add()
            Dgl1.Item(Col1Head, Dgl1.Rows.Count - 1).Value = Head
            Dgl1.Item(Col1Status, Dgl1.Rows.Count - 1).Value = Status
            Dgl1.Item(Col1Message, Dgl1.Rows.Count - 1).Value = Message
            If Status = "Error" Then
                Dgl1.Rows(Dgl1.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Red
            End If
            Dgl1.FirstDisplayedScrollingRowIndex = Dgl1.RowCount - 1

            Dim mMessage As String = Head + " " + Status + " " + Message
            If mMessage.Length > 255 Then
                mMessage = (Head + " " + Status + " " + Message).Substring(1, 255)
            End If
        End If
    End Sub
End Class

