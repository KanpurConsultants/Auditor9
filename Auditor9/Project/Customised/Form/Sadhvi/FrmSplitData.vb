Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.FrmSaleInvoiceDirect_WithDimension

Public Class FrmSplitData
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_ExternalDatabase As New SQLite.SQLiteConnection
    Public mDbPath As String = ""
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker


    Public Const Col1Value As String = "Value"

    Public Const Col1Head As String = "Head"
    Public Const Col1Status As String = "Status"
    Public Const Col1Message As String = "Message"

    Dim rowV_Date As Integer = 0

    Public Const hcV_Date As String = "Date"

    Dim DtSiteMast As DataTable

    Private Delegate Sub UpdateChildProgressBarInvoker(ByVal Value As String, ChildPrMaxVal As Integer, ChildPrgValue As Integer)
    Private Delegate Sub UpdateParentProgressBarInvoker(ByVal Value As String, ParentPrMaxVal As Integer)
    Private Delegate Sub FRecordMessageInvoker(Head As String, Status As String, Message As String)
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        BtnOK.Enabled = False
        _backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        _backgroundWorker1.WorkerSupportsCancellation = False
        _backgroundWorker1.WorkerReportsProgress = False
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FProcSave)
        _backgroundWorker1.RunWorkerAsync()
    End Sub
    Private Sub Ini_Grid()
        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, Col1Head, 250, 0, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 400, 0, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(DglMain, PnlMain)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.ColumnHeadersVisible = False
        DglMain.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(DglMain)
        DglMain.AgAllowFind = False
        DglMain.AllowUserToAddRows = False
        DglMain.AgSkipReadOnlyColumns = True

        DglMain.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        DglMain.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        DglMain.BackgroundColor = Me.BackColor
        AgCL.GridSetiingShowXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain, False)


        DglMain.Rows.Add(1)

        DglMain.Item(Col1Head, rowV_Date).Value = hcV_Date

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
        For I As Integer = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).DefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8)
        Next
    End Sub
    Private Sub FrmImportFromExcel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        DglMain.Item(Col1Value, rowV_Date).Value = ClsMain.FormatDate((DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))))
    End Sub
    Public Sub FProcSave()
        Dim mTrans As String = ""

        If AgL.XNull(DglMain.Item(Col1Value, rowV_Date).Value) = "" Then
            MsgBox("Date is required.", MsgBoxStyle.Information)
            Exit Sub
        End If

        UpdateChildProgressBar("Initializing...", 1, 0)

        FGetOpening()

        UpdateChildProgressBar(" ", 1, 0)
        UpdateParentProgressBar(" ", 1)
        MsgBox("Process Completed ...", MsgBoxStyle.Information)
    End Sub
    Private Sub BtnSelectExcelFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile.Click
        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName

        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection_ExternalDatabase.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
        Else
            Connection_ExternalDatabase.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        TxtExcelPath.Text = mDbPath
    End Sub
    Public Sub UpdateChildProgressBar(ByVal Value As String, ChildPrMaxVal As Integer, ChildPrgValue As Integer)
        If Me.LblChildProgress.InvokeRequired Then
            Me.LblChildProgress.Invoke(New UpdateChildProgressBarInvoker(AddressOf Me.UpdateChildProgressBar), New Object() {Value, ChildPrMaxVal, ChildPrgValue})
        Else
            Me.LblChildProgress.Text = Value
            PrgBarChild.Maximum = ChildPrMaxVal
            PrgBarChild.Value = ChildPrgValue
            LblChildProgress.Refresh()
        End If
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
    Private Sub FRecordMessage(Head As String, Status As String, Message As String)
        If Me.Dgl1.InvokeRequired Then
            Me.Dgl1.Invoke(New FRecordMessageInvoker(AddressOf Me.FRecordMessage), New Object() {Head, Status, Message})
        Else
            Dgl1.Rows.Add()
            Dgl1.Item(Col1Head, Dgl1.Rows.Count - 1).Value = Head
            Dgl1.Item(Col1Status, Dgl1.Rows.Count - 1).Value = Status
            Dgl1.Item(Col1Message, Dgl1.Rows.Count - 1).Value = Message
            If Status = "Error" Then
                Dgl1.Rows(Dgl1.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Red
            End If
            Dgl1.FirstDisplayedScrollingRowIndex = Dgl1.RowCount - 1
        End If
    End Sub
    Private Sub FGetOpening()
        Dim mChildPrgCnt As Integer = 0

        Dim DtLedgerOpening As DataTable

        mQry = " SELECT SubCode, Name FROM Subgroup WHERE IfNull(Nature,'') Not In ('Expense','Income','Customer','Supplier') "
        Dim DtLedgerAccount As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT SubCode, Name  FROM Subgroup WHERE IfNull(Nature,'') In ('Customer') "
        Dim DtCustomer As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.DocID, H.V_Type || '-' || H.ManualRefNo As DocNo
                FROM SaleInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                WHERE Vt.NCat = '" & Ncat.SaleReturn & "' 
                And H.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " "
        Dim DtSaleReturnToDelete As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " SELECT H.DocID, H.V_Type || '-' || H.ManualRefNo As DocNo
                FROM SaleInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                WHERE Vt.NCat = '" & Ncat.SaleInvoice & "' 
                And H.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " "
        Dim DtSaleInvoiceToDelete As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.DocID, H.V_Type || '-' || H.ManualRefNo As DocNo
                FROM PurchInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN Cloth_SupplierSettlementPayments S ON H.DocID = S.PaymentDocId
                LEFT JOIN LedgerHead Lh ON S.DocID = Lh.DocID
                WHERE Vt.NCat = '" & Ncat.PurchaseReturn & "' 
                And H.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " 
                AND Lh.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & ""
        Dim DtPurchReturnToDelete As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.DocID, H.V_Type || '-' || H.ManualRefNo As DocNo
                FROM PurchInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN Cloth_SupplierSettlementInvoices S ON H.DocID = S.PurchaseInvoiceDocId
                LEFT JOIN LedgerHead Lh ON S.DocID = Lh.DocID
                WHERE Vt.NCat = '" & Ncat.PurchaseInvoice & "' 
                And H.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " 
                AND Lh.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & ""
        Dim DtPurchInvoiceToDelete As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " SELECT H.DocID, H.V_Sno, H.V_Type || '-' || H.RecId As DocNo
                FROM Ledger H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN Cloth_SupplierSettlementPayments S ON H.DocID = S.PaymentDocId And H.V_Sno = S.PaymentDocIdSr
                LEFT JOIN LedgerHead Lh ON S.DocID = Lh.DocID
                WHERE Vt.NCat Not In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') 
                And H.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " 
                AND Lh.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & ""
        Dim DtLedgerToDelete As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.DocID, H.V_Type || '-' || H.ManualRefNo As DocNo
                FROM LedgerHead H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                WHERE Vt.NCat In ('" & Ncat.PaymentSettlement & "') 
                And H.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " "
        Dim DtPaymentSettlementToDelete As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim mParentPrgBarMaxVal As Integer = DtLedgerAccount.Rows.Count +
                            DtCustomer.Rows.Count +
                            DtSaleReturnToDelete.Rows.Count +
                            DtSaleInvoiceToDelete.Rows.Count +
                            DtPurchReturnToDelete.Rows.Count +
                            DtPurchInvoiceToDelete.Rows.Count +
                            DtLedgerToDelete.Rows.Count +
                            DtPaymentSettlementToDelete.Rows.Count

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mChildPrgCnt = 0
            'For I As Integer = 0 To DtLedgerAccount.Rows.Count - 1
            '    UpdateParentProgressBar("Inserting Opening For Other Accounts", mParentPrgBarMaxVal)
            '    UpdateChildProgressBar("Retrieving Opening for " + AgL.XNull(DtLedgerAccount.Rows(I)("Name")), DtLedgerAccount.Rows.Count * 2, mChildPrgCnt)
            '    mChildPrgCnt += 1

            '    FDeleteOpening(AgL.XNull(DtLedgerAccount.Rows(I)("SubCode")))

            '    mQry = " Select L.SubCode, Max(Sg.Name) As SubCodeName, 
            '            Case When IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) > 0 Then IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) Else 0 End As AmtDr,
            '            Case When IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) < 0 Then Abs(IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0)) Else 0 End As AmtCr,
            '            Null As ReferenceNo, Null As ReferenceDate
            '            From Ledger L
            '            LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
            '            WHERE IfNull(Nature,'') Not In ('Expense','Income','Customer','Supplier') 
            '            And L.SubCode = '" & AgL.XNull(DtLedgerAccount.Rows(I)("SubCode")) & "'
            '            And L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & "
            '            Group By L.SubCode 
            '            Having IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) <> 0 "
            '    DtLedgerOpening = AgL.FillData(mQry, AgL.GCn).Tables(0)

            '    If DtLedgerOpening.Rows.Count > 0 Then
            '        UpdateChildProgressBar("Inserting Opening for " + DtLedgerAccount.Rows(I)("Name"), DtLedgerAccount.Rows.Count * 2, mChildPrgCnt)
            '        FTransferOpening(DtLedgerOpening)
            '        FRecordMessage(LblChildProgress.Text, "Success.", "")
            '    Else
            '        UpdateChildProgressBar("Opening not found for " + DtLedgerAccount.Rows(I)("Name"), DtLedgerAccount.Rows.Count * 2, mChildPrgCnt)
            '    End If
            '    mChildPrgCnt += 1
            'Next
            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mChildPrgCnt = 0
            'For I As Integer = 0 To DtCustomer.Rows.Count - 1
            '    UpdateParentProgressBar("Inserting Opening For Customers", mParentPrgBarMaxVal)
            '    UpdateChildProgressBar("Retrieving Opening for " + AgL.XNull(DtCustomer.Rows(I)("Name")), DtCustomer.Rows.Count * 2, mChildPrgCnt)
            '    mChildPrgCnt += 1

            '    FDeleteOpening(AgL.XNull(DtLedgerAccount.Rows(I)("SubCode")))

            '    Dim DsChukti As DataTable
            '    DsChukti = FGetChukti(AgL.XNull(DtCustomer.Rows(I)("SubCode")))

            '    Dim strSql As String = ""
            '    For J As Integer = 0 To DsChukti.Rows.Count - 1
            '        If AgL.VNull(DsChukti.Rows(J)("DrAmount")) <> 0 Then
            '            If strSql <> "" Then strSql += " UNION ALL "
            '            strSql += " Select '" & AgL.XNull(DsChukti.Rows(J)("DrSubCode")) & "' As SubCode, 
            '                '" & AgL.XNull(DsChukti.Rows(J)("PartyName")) & "' As SubCodeName, 
            '                " & AgL.VNull(DsChukti.Rows(J)("DrAmount")) & " As AmtDr, 
            '                0 As AmtCr,
            '                '" & AgL.XNull(DsChukti.Rows(J)("DrDocNo")) & "' As ReferenceNo, 
            '                '" & AgL.XNull(DsChukti.Rows(J)("DrDate")) & "' As ReferenceDate "
            '        End If
            '    Next

            '    For J As Integer = 0 To DsChukti.Rows.Count - 1
            '        If AgL.VNull(DsChukti.Rows(J)("CrAmount")) <> 0 Then
            '            If strSql <> "" Then strSql += " UNION ALL "
            '            strSql += " Select '" & AgL.XNull(DsChukti.Rows(J)("CrSubCode")) & "' As SubCode, 
            '                '" & AgL.XNull(DsChukti.Rows(J)("PartyName")) & "' As SubCodeName, 
            '                0 As AmtDr, 
            '                " & AgL.VNull(DsChukti.Rows(J)("CrAmount")) & " As AmtCr,
            '                '" & AgL.XNull(DsChukti.Rows(J)("CrDocNo")) & "' As ReferenceNo, 
            '                '" & AgL.XNull(DsChukti.Rows(J)("CrDate")) & "' As ReferenceDate "
            '        End If
            '    Next
            '    If strSql <> "" Then
            '        DtLedgerOpening = AgL.FillData(strSql, AgL.GCn).Tables(0)
            '        UpdateChildProgressBar("Inserting Opening for " + DtCustomer.Rows(I)("Name"), DtCustomer.Rows.Count * 2, mChildPrgCnt)
            '        FTransferOpening(DtLedgerOpening)
            '        FRecordMessage(LblChildProgress.Text, "Success.", "")
            '    Else
            '        UpdateChildProgressBar("Opening not found for " + DtCustomer.Rows(I)("Name"), DtCustomer.Rows.Count * 2, mChildPrgCnt)
            '    End If
            '    mChildPrgCnt += 1
            'Next

            mChildPrgCnt = 0
            For I As Integer = 0 To DtSaleReturnToDelete.Rows.Count - 1
                UpdateParentProgressBar("Deleting Sale Returns", mParentPrgBarMaxVal)
                UpdateChildProgressBar("Deleting Sale Return " + DtSaleReturnToDelete.Rows(I)("DocNo"), DtSaleReturnToDelete.Rows.Count, mChildPrgCnt)
                FDeleteSale(AgL.XNull(DtSaleReturnToDelete.Rows(I)("DocId")))
                FRecordMessage(LblChildProgress.Text, "Success.", "")
                mChildPrgCnt += 1
            Next

            mChildPrgCnt = 0
            For I As Integer = 0 To DtSaleInvoiceToDelete.Rows.Count - 1
                UpdateParentProgressBar("Deleting Sale Invoices", mParentPrgBarMaxVal)
                UpdateChildProgressBar("Deleting Sale Invoice " + DtSaleInvoiceToDelete.Rows(I)("DocNo"), DtSaleInvoiceToDelete.Rows.Count, mChildPrgCnt)
                FDeleteSale(AgL.XNull(DtSaleInvoiceToDelete.Rows(I)("DocId")))
                FRecordMessage(LblChildProgress.Text, "Success.", "")
                mChildPrgCnt += 1
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mChildPrgCnt = 0
            For I As Integer = 0 To DtPurchReturnToDelete.Rows.Count - 1
                UpdateParentProgressBar("Deleting Purchase Returns", mParentPrgBarMaxVal)
                UpdateChildProgressBar("Deleting Purchase Return " + DtPurchReturnToDelete.Rows(I)("DocNo"), DtPurchReturnToDelete.Rows.Count, mChildPrgCnt)
                FDeletePurchase(AgL.XNull(DtPurchReturnToDelete.Rows(I)("DocId")))
                FRecordMessage(LblChildProgress.Text, "Success.", "")
                mChildPrgCnt += 1
            Next

            mChildPrgCnt = 0
            For I As Integer = 0 To DtPurchInvoiceToDelete.Rows.Count - 1
                UpdateParentProgressBar("Deleting Purchase Invoices", mParentPrgBarMaxVal)
                UpdateChildProgressBar("Deleting Purchase Invoice " + DtPurchInvoiceToDelete.Rows(I)("DocNo"), DtPurchInvoiceToDelete.Rows.Count, mChildPrgCnt)
                FDeletePurchase(AgL.XNull(DtPurchInvoiceToDelete.Rows(I)("DocId")))
                FRecordMessage(LblChildProgress.Text, "Success.", "")
                mChildPrgCnt += 1
            Next

            mChildPrgCnt = 0
            For I As Integer = 0 To DtLedgerToDelete.Rows.Count - 1
                If AgL.VNull(AgL.Dman_Execute(" Select Count(*) 
                            From LedgerHeadDetail L 
                            Where L.DocId = '" & AgL.XNull(DtLedgerToDelete.Rows(I)("DocId")) & "'", AgL.GCn).ExecuteScalar()) = 1 Then
                    UpdateParentProgressBar("Deleting Ledger Record ", mParentPrgBarMaxVal)
                    UpdateChildProgressBar("Deleting Ledger Record " + DtLedgerToDelete.Rows(I)("DocNo"), DtLedgerToDelete.Rows.Count, mChildPrgCnt)
                    FDeleteLedgerHead(AgL.XNull(DtLedgerToDelete.Rows(I)("DocId")))
                    FRecordMessage(LblChildProgress.Text, "Success.", "")
                    mChildPrgCnt += 1
                End If
            Next

            mChildPrgCnt = 0
            For I As Integer = 0 To DtPaymentSettlementToDelete.Rows.Count - 1
                UpdateParentProgressBar("Deleting Payment Settlements", mParentPrgBarMaxVal)
                UpdateChildProgressBar("Deleting Payment Settlement " + DtPaymentSettlementToDelete.Rows(I)("DocNo"), DtPaymentSettlementToDelete.Rows.Count, mChildPrgCnt)
                FDeleteLedgerHead(AgL.XNull(DtPaymentSettlementToDelete.Rows(I)("DocId")))
                FRecordMessage(LblChildProgress.Text, "Success.", "")
                mChildPrgCnt += 1
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FTransferOpening(DtLedgerOpening As DataTable)
        Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
        Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead
        VoucherEntryTable.DocID = ""
        VoucherEntryTable.V_Type = Ncat.OpeningBalance
        VoucherEntryTable.V_Prefix = ""
        VoucherEntryTable.V_Date = DglMain.Item(Col1Value, rowV_Date).Value
        VoucherEntryTable.V_No = 1
        VoucherEntryTable.Div_Code = AgL.PubDivCode
        VoucherEntryTable.Site_Code = AgL.PubSiteCode
        VoucherEntryTable.ManualRefNo = ""
        VoucherEntryTable.Subcode = ""
        VoucherEntryTable.SubcodeName = ""
        VoucherEntryTable.UptoDate = ""
        VoucherEntryTable.Remarks = ""
        VoucherEntryTable.Status = "Active"
        VoucherEntryTable.SalesTaxGroupParty = ""
        VoucherEntryTable.PlaceOfSupply = ""
        VoucherEntryTable.PartySalesTaxNo = ""
        VoucherEntryTable.StructureCode = ""
        VoucherEntryTable.CustomFields = ""
        VoucherEntryTable.PartyDocNo = ""
        VoucherEntryTable.PartyDocDate = ""
        VoucherEntryTable.EntryBy = AgL.PubUserName
        VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
        VoucherEntryTable.ApproveBy = ""
        VoucherEntryTable.ApproveDate = ""
        VoucherEntryTable.MoveToLog = ""
        VoucherEntryTable.MoveToLogDate = ""
        VoucherEntryTable.UploadDate = ""

        VoucherEntryTable.Gross_Amount = 0
        VoucherEntryTable.Taxable_Amount = 0
        VoucherEntryTable.Tax1_Per = 0
        VoucherEntryTable.Tax1 = 0
        VoucherEntryTable.Tax2_Per = 0
        VoucherEntryTable.Tax2 = 0
        VoucherEntryTable.Tax3_Per = 0
        VoucherEntryTable.Tax3 = 0
        VoucherEntryTable.Tax4_Per = 0
        VoucherEntryTable.Tax4 = 0
        VoucherEntryTable.Tax5_Per = 0
        VoucherEntryTable.Tax5 = 0
        VoucherEntryTable.SubTotal1 = 0
        VoucherEntryTable.Deduction_Per = 0
        VoucherEntryTable.Deduction = 0
        VoucherEntryTable.Other_Charge_Per = 0
        VoucherEntryTable.Other_Charge = 0
        VoucherEntryTable.Round_Off = 0
        VoucherEntryTable.Net_Amount = 0

        For J As Integer = 0 To DtLedgerOpening.Rows.Count - 1
            VoucherEntryTable.Line_Sr = J + 1
            VoucherEntryTable.Line_SubCode = AgL.XNull(DtLedgerOpening.Rows(J)("SubCode"))
            VoucherEntryTable.Line_SubCodeName = ""
            VoucherEntryTable.Line_SpecificationDocID = ""
            VoucherEntryTable.Line_SpecificationDocIDSr = ""
            VoucherEntryTable.Line_Specification = ""
            VoucherEntryTable.Line_SalesTaxGroupItem = ""
            VoucherEntryTable.Line_Qty = 0
            VoucherEntryTable.Line_Unit = ""
            VoucherEntryTable.Line_Rate = 0
            VoucherEntryTable.Line_Amount = AgL.VNull(DtLedgerOpening.Rows(J)("AmtDr"))
            VoucherEntryTable.Line_Amount_Cr = AgL.VNull(DtLedgerOpening.Rows(J)("AmtCr"))
            VoucherEntryTable.Line_ChqRefNo = ""
            VoucherEntryTable.Line_ChqRefDate = ""
            VoucherEntryTable.Line_ReferenceNo = AgL.XNull(DtLedgerOpening.Rows(J)("ReferenceNo"))
            VoucherEntryTable.Line_ReferenceDate = AgL.XNull(DtLedgerOpening.Rows(J)("ReferenceDate"))
            VoucherEntryTable.Line_Remarks = ""
            VoucherEntryTable.Line_Gross_Amount = 0
            VoucherEntryTable.Line_Taxable_Amount = 0
            VoucherEntryTable.Line_Tax1_Per = 0
            VoucherEntryTable.Line_Tax1 = 0
            VoucherEntryTable.Line_Tax2_Per = 0
            VoucherEntryTable.Line_Tax2 = 0
            VoucherEntryTable.Line_Tax3_Per = 0
            VoucherEntryTable.Line_Tax3 = 0
            VoucherEntryTable.Line_Tax4_Per = 0
            VoucherEntryTable.Line_Tax4 = 0
            VoucherEntryTable.Line_Tax5_Per = 0
            VoucherEntryTable.Line_Tax5 = 0
            VoucherEntryTable.Line_SubTotal1 = 0
            VoucherEntryTable.Line_Deduction_Per = 0
            VoucherEntryTable.Line_Deduction = 0
            VoucherEntryTable.Line_Other_Charge_Per = 0
            VoucherEntryTable.Line_Other_Charge = 0
            VoucherEntryTable.Line_Round_Off = 0
            VoucherEntryTable.Line_Net_Amount = 0

            VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
            ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
        Next


        FrmVoucherEntry.InsertLedgerHead(VoucherEntryTableList)
    End Sub
    Private Sub DglMain_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub

            If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
                CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

                Select Case DglMain.CurrentCell.RowIndex
                    Case rowV_Date
                        CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetChukti(SubCode) As DataTable
        Dim ReportFrm As New ReportLayout.FrmReportLayout("", "", "", "")
        ReportFrm.CreateHelpGrid("Report Format", "Report Format", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Format 1")
        ReportFrm.CreateHelpGrid("As On Date", "As On Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
        ReportFrm.CreateHelpGrid("Grace Days", "Grace Days", ReportLayout.FrmReportLayout.FieldFilterDataType.NumericType, ReportLayout.FrmReportLayout.FieldDataType.NumericType, "", 60)
        ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, "", "", 450, 825, 300)
        ReportFrm.FGMain.Item(4, 3).Value = AgL.Chk_Text(SubCode)
        ReportFrm.CreateHelpGrid("Records Type", "Records Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "After Chukti")
        ReportFrm.CreateHelpGrid("Agent", "Agent", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, "")
        ReportFrm.CreateHelpGrid("City", "City", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, "")
        ReportFrm.CreateHelpGrid("Area", "Area", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, "")
        ReportFrm.CreateHelpGrid("Division", "Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, "", "[DIVISIONCODE]")
        ReportFrm.CreateHelpGrid("Site", "Site", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, "", "[SITECODE]")
        ReportFrm.CreateHelpGrid("Interest Rate", "Interest Rate", ReportLayout.FrmReportLayout.FieldFilterDataType.NumericType, ReportLayout.FrmReportLayout.FieldDataType.NumericType, "", AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")))


        Dim CRepProc As New ClsReportProcedures(ReportFrm)
        Dim DsRep As DataSet = CRepProc.FunConcurLedger()
        If DsRep IsNot Nothing Then
            FGetChukti = DsRep.Tables(0)
        Else
            FGetChukti = Nothing
        End If
    End Function
    Private Sub FDeleteOpening(SubCode As String)
        mQry = " Select L.DocId, L.Sr
                        From LedgerHead H 
                        LEFT JOIN LedgerHeadDetail L On H.DocId = L.DocId 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                        Where Vt.NCat = '" & Ncat.OpeningBalance & "'
                        And L.SubCode = '" & SubCode & "'"
        Dim DtLedgerHeadDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For J As Integer = 0 To DtLedgerHeadDetail.Rows.Count - 1
            mQry = " Delete From LedgerHeadDetail Where DocId = '" & AgL.XNull(DtLedgerHeadDetail.Rows(J)("DocId")) & "'
                    And Sr = " & AgL.VNull(DtLedgerHeadDetail.Rows(J)("Sr")) & " "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next

        mQry = " Select L.DocId, L.V_Sno 
                From Ledger L 
                LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type 
                Where Vt.NCat = '" & Ncat.OpeningBalance & "'
                And L.SubCode = '" & SubCode & "'"
        Dim DtLedger As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For J As Integer = 0 To DtLedger.Rows.Count - 1
            mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLedger.Rows(J)("DocId")) & "'
                    And V_Sno = " & AgL.VNull(DtLedger.Rows(J)("V_Sno")) & " "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next
    End Sub
    Private Sub FDeletePurchase(DocId As String)
        mQry = "DELETE FROM PurchInvoiceBarcodeLastTransactionValues Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceDetail Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceDetailBarCodeValues Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceDetailBom Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceDetailBomSku Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceDetailHelpValues Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceDetailSku Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceDimensionDetail Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceDimensionDetailSku Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoiceTransport Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerHead Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM PurchInvoice Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FDeleteSale(DocId As String)
        mQry = "DELETE FROM SaleInvoiceBarcodeLastTransactionValues Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoiceDetail Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoiceDetailBarCodeValues Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoiceDetailHelpValues Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoiceDetailSku Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoiceDimensionDetail Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoiceDimensionDetailSku Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoicePayment Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoiceReferences Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM SaleInvoice Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FDeleteLedgerHead(DocId As String)
        mQry = "DELETE From Cloth_SupplierSettlementInvoices Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE From Cloth_SupplierSettlementInvoicesAdjustment Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE From Cloth_SupplierSettlementInvoicesLine Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE From Cloth_SupplierSettlementPayments Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM Ledger Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerAdj Where Vr_DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerHeadCharges Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerHeadDetail Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerHeadDetailCharges Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerHeadDetailChequePrinting Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerItemAdj Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerM Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM LedgerHead Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FDeleteStockHead(DocId As String)
        mQry = "DELETE FROM Stock Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM StockProcess Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM StockHeadDetail Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = "DELETE FROM StockHead Where DocId = '" & DocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
End Class