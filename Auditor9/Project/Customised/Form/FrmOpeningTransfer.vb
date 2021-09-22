Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.FrmSaleInvoiceDirect_WithDimension

Public Class FrmOpeningTransfer
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_ExternalDatabase As New SQLite.SQLiteConnection
    Public mDbPath As String = ""
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker


    Public Const Col1Value As String = "Value"

    Public Const Col1Head As String = "Head"
    Public Const Col1Status As String = "Status"
    Public Const Col1Message As String = "Message"


    Public Const Col2LyCode As String = "LyCode"
    Public Const Col2LyName As String = "Last Year A/c Name"
    Public Const Col2LyAcGroupName As String = "Last Year A/c Group"
    Public Const Col2LyGroupNature As String = "Last Year Group Nature"
    Public Const Col2LyClosingBalance As String = "Last Year Closing Balance"
    Public Const Col2LyDrCr As String = "LyDrCr"
    Public Const Col2CyCode As String = "CyCode"
    Public Const Col2CyName As String = "Current Year A/c Name"
    Public Const Col2CyAcGroupName As String = "Current Year A/c Group"
    Public Const Col2CyGroupNature As String = "Current Year Group Nature"
    Public Const Col2CyOpeningBalance As String = "Current Year Opening Balance"
    Public Const Col2CyDrCr As String = "CyDrCr"
    Public Const Col2Remark As String = "Remark"

    Dim rowV_Date As Integer = 0
    Dim rowTransferOpeningForCustomers As Integer = 1
    Dim rowTransferOpeningForSuppliers As Integer = 2
    Dim rowTransferOpeningForOtherAccounts As Integer = 3

    Public Const hcV_Date As String = "Date"
    Public Const hcTransferOpeningForCustomers As String = "Transfer Opening For Customers"
    Public Const hcTransferOpeningForSuppliers As String = "Transfer Opening For Suppliers"
    Public Const hcTransferOpeningForOtherAccounts As String = "Transfer Opening For Other Accounts"

    Dim DtSiteMast As DataTable

    Private Delegate Sub UpdateChildProgressBarInvoker(ByVal Value As String, ChildPrMaxVal As Integer, ChildPrgValue As Integer)
    Private Delegate Sub UpdateParentProgressBarInvoker(ByVal Value As String, ParentPrMaxVal As Integer)
    Private Delegate Sub FRecordMessageInvoker(Head As String, Status As String, Message As String, Conn As Object, Cmd As Object)
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
            .AddAgTextColumn(DglMain, Col1Head, 350, 0, Col1Head, True, True)
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

        DglMain.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        DglMain.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        DglMain.BackgroundColor = Me.BackColor
        DglMain.CellBorderStyle = DataGridViewCellBorderStyle.None
        AgCL.GridSetiingShowXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain, False)


        DglMain.Rows.Add(4)

        DglMain.Item(Col1Head, rowV_Date).Value = hcV_Date
        DglMain.Item(Col1Head, rowTransferOpeningForCustomers).Value = hcTransferOpeningForCustomers
        DglMain.Item(Col1Head, rowTransferOpeningForSuppliers).Value = hcTransferOpeningForSuppliers
        DglMain.Item(Col1Head, rowTransferOpeningForOtherAccounts).Value = hcTransferOpeningForOtherAccounts

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


        With AgCL
            .AddAgTextColumn(Dgl2, Col2LyCode, 100, 0, Col2LyCode, False, True)
            .AddAgTextColumn(Dgl2, Col2LyName, 320, 0, Col2LyName, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl2, Col2LyAcGroupName, 140, 0, Col2LyAcGroupName, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl2, Col2LyGroupNature, 140, 0, Col2LyGroupNature, False, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl2, Col2LyClosingBalance, 140, 8, 4, False, Col2LyClosingBalance, True, True, True,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl2, Col2LyDrCr, 30, 0, " ", True, True,,, DataGridViewColumnSortMode.Automatic)

            .AddAgTextColumn(Dgl2, Col2CyCode, 100, 0, Col2CyCode, False, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl2, Col2CyName, 320, 0, Col2CyName, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl2, Col2CyAcGroupName, 140, 0, Col2CyAcGroupName, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl2, Col2CyGroupNature, 140, 0, Col2CyGroupNature, False, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl2, Col2CyOpeningBalance, 140, 8, 4, False, Col2CyOpeningBalance, True, True, True,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl2, Col2CyDrCr, 30, 0, " ", True, True,,, DataGridViewColumnSortMode.Automatic)

            .AddAgTextColumn(Dgl2, Col2Remark, 67, 0, Col2Remark, True, True,,, DataGridViewColumnSortMode.Automatic)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 40
        AgL.GridDesign(Dgl2)
        Dgl2.AllowUserToOrderColumns = True
        Dgl2.Name = "Dgl2"
        Dgl2.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl2.BackgroundColor = Me.BackColor
        Dgl2.AllowUserToAddRows = False
        Dgl2.CellBorderStyle = DataGridViewCellBorderStyle.None
        Dgl2.BorderStyle = BorderStyle.None
        Dgl2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        For I As Integer = 0 To Dgl2.Columns.Count - 1
            Dgl2.Columns(I).DefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8)
        Next
    End Sub
    Private Sub FrmImportFromExcel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        DglMain.Item(Col1Value, rowV_Date).Value = ClsMain.FormatDate((DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))))
        DglMain.Item(Col1Value, rowTransferOpeningForCustomers).Value = "No"
        DglMain.Item(Col1Value, rowTransferOpeningForSuppliers).Value = "No"
        DglMain.Item(Col1Value, rowTransferOpeningForOtherAccounts).Value = "No"
    End Sub
    Public Sub FProcSave()
        Dim mTrans As String = ""

        If AgL.XNull(DglMain.Item(Col1Value, rowV_Date).Value) = "" Then
            MsgBox("Date is required.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If AgL.XNull(DglMain.Item(Col1Value, rowTransferOpeningForCustomers).Value) = "No" And
            AgL.XNull(DglMain.Item(Col1Value, rowTransferOpeningForSuppliers).Value) = "No" And
            AgL.XNull(DglMain.Item(Col1Value, rowTransferOpeningForOtherAccounts).Value) = "No" Then
            MsgBox("Please select which opening you want to transfer.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If TxtFilePath.Text = "" Then
            MsgBox("Please select file.", MsgBoxStyle.Information)
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
        TxtFilePath.Text = mDbPath
        Connection_ExternalDatabase.Open()
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

            Call AgL.LogTableEntry("Opening Transfer", Me.Text, "A", AgL.PubMachineName,
                AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), Conn, Cmd,
                mMessage, DglMain(Col1Value, rowV_Date).Value,,,,
                AgL.PubSiteCode, AgL.PubDivCode, "", "", "")
        End If
    End Sub
    Private Sub FGetOpening()
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0

        Dim DtLedgerOpening As DataTable

        If DglMain.Item(Col1Value, rowTransferOpeningForOtherAccounts).Value = "Yes" Then
            mQry = " SELECT SubCode, Name, IfNull(GroupNature,'') As GroupNature FROM Subgroup WHERE IfNull(Nature,'') Not In ('Customer','Supplier') "
        Else
            mQry = " SELECT SubCode, Name FROM Subgroup WHERE IfNull(Nature,'') Not In ('Expense','Income','Customer','Supplier') And 1=2 "
        End If
        Dim DtLedgerAccount As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        If DglMain.Item(Col1Value, rowTransferOpeningForCustomers).Value = "Yes" Then
            mQry = " SELECT SubCode, Name  FROM Subgroup WHERE IfNull(Nature,'') In ('Customer') "
        Else
            mQry = " SELECT SubCode, Name  FROM Subgroup WHERE IfNull(Nature,'') In ('Customer') And 1=2 "
        End If
        Dim DtCustomer As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        If DglMain.Item(Col1Value, rowTransferOpeningForSuppliers).Value = "Yes" Then
            mQry = " SELECT SubCode, Name  FROM Subgroup WHERE IfNull(Nature,'') In ('Supplier') "
        Else
            mQry = " SELECT SubCode, Name  FROM Subgroup WHERE IfNull(Nature,'') In ('Supplier') And 1=2 "
        End If
        Dim DtSupplier As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        Dim mParentPrgBarMaxVal As Integer = DtLedgerAccount.Rows.Count +
                            DtCustomer.Rows.Count + DtSupplier.Rows.Count

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mChildPrgCnt = 0
            mChildPrgMaxVal = DtLedgerAccount.Rows.Count * 3
            For I As Integer = 0 To DtLedgerAccount.Rows.Count - 1
                UpdateParentProgressBar("Inserting Opening For Other Accounts", mParentPrgBarMaxVal)

                UpdateChildProgressBar("Deleting Old Ledger for " + AgL.XNull(DtLedgerAccount.Rows(I)("Name")), mChildPrgMaxVal, mChildPrgCnt)
                FDeleteLedger(AgL.XNull(DtLedgerAccount.Rows(I)("SubCode")))
                mChildPrgCnt += 1

                UpdateChildProgressBar("Retrieving Opening for " + AgL.XNull(DtLedgerAccount.Rows(I)("Name")), mChildPrgMaxVal, mChildPrgCnt)
                mChildPrgCnt += 1

                mQry = " Select L.SubCode, Max(Sg.Name) As SubCodeName, 
                        Case When IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) > 0 Then IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) Else 0 End As AmtDr,
                        Case When IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) < 0 Then Abs(IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0)) Else 0 End As AmtCr,
                        Null As ReferenceNo, Null As ReferenceDate
                        From Ledger L
                        LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
                        WHERE L.SubCode = '" & AgL.XNull(DtLedgerAccount.Rows(I)("SubCode")) & "'
                        And IfNull(Sg.GroupNature,'') Not In ('R','E')
                        And L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & "
                        Group By L.SubCode 
                        Having IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) <> 0 "
                DtLedgerOpening = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                If DtLedgerOpening.Rows.Count > 0 Then
                    UpdateChildProgressBar("Inserting Opening for " + DtLedgerAccount.Rows(I)("Name"), mChildPrgMaxVal, mChildPrgCnt)
                    FTransferOpening(DtLedgerOpening)
                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                Else
                    UpdateChildProgressBar("Opening not found for " + DtLedgerAccount.Rows(I)("Name"), mChildPrgMaxVal, mChildPrgCnt)
                End If
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
            mChildPrgMaxVal = DtCustomer.Rows.Count * 3
            For I As Integer = 0 To DtCustomer.Rows.Count - 1
                UpdateParentProgressBar("Inserting Opening For Customers", mParentPrgBarMaxVal)


                UpdateChildProgressBar("Deleting Old Ledger for " + DtCustomer.Rows(I)("Name"), mChildPrgMaxVal, mChildPrgCnt)
                FDeleteLedger(AgL.XNull(DtCustomer.Rows(I)("SubCode")))
                mChildPrgCnt += 1


                UpdateChildProgressBar("Retrieving Opening for " + AgL.XNull(DtCustomer.Rows(I)("Name")), mChildPrgMaxVal, mChildPrgCnt)
                mChildPrgCnt += 1

                Dim DsChukti As DataTable
                DsChukti = FGetChukti(AgL.XNull(DtCustomer.Rows(I)("SubCode")), Connection_ExternalDatabase)

                Dim strSql As String = ""
                For J As Integer = 0 To DsChukti.Rows.Count - 1
                    If AgL.VNull(DsChukti.Rows(J)("DrAmount")) <> 0 Then
                        If strSql <> "" Then strSql += " UNION ALL "
                        strSql += " Select '" & AgL.XNull(DsChukti.Rows(J)("DrSubCode")) & "' As SubCode, 
                            '" & AgL.XNull(DsChukti.Rows(J)("PartyName")) & "' As SubCodeName, 
                            " & AgL.VNull(DsChukti.Rows(J)("DrAmount")) & " As AmtDr, 
                            0 As AmtCr,
                            '" & AgL.XNull(DsChukti.Rows(J)("DrDocNo")) & "' As ReferenceNo, 
                            '" & AgL.XNull(DsChukti.Rows(J)("DrDate")) & "' As ReferenceDate "
                    End If
                Next

                For J As Integer = 0 To DsChukti.Rows.Count - 1
                    If AgL.VNull(DsChukti.Rows(J)("CrAmount")) <> 0 Then
                        If strSql <> "" Then strSql += " UNION ALL "
                        strSql += " Select '" & AgL.XNull(DsChukti.Rows(J)("CrSubCode")) & "' As SubCode, 
                            '" & AgL.XNull(DsChukti.Rows(J)("PartyName")) & "' As SubCodeName, 
                            0 As AmtDr, 
                            " & AgL.VNull(DsChukti.Rows(J)("CrAmount")) & " As AmtCr,
                            '" & AgL.XNull(DsChukti.Rows(J)("CrDocNo")) & "' As ReferenceNo, 
                            '" & AgL.XNull(DsChukti.Rows(J)("CrDate")) & "' As ReferenceDate "
                    End If
                Next
                If strSql <> "" Then
                    DtLedgerOpening = AgL.FillData(strSql, AgL.GCn).Tables(0)
                    UpdateChildProgressBar("Inserting Opening for " + DtCustomer.Rows(I)("Name"), mChildPrgMaxVal, mChildPrgCnt)
                    FTransferOpening(DtLedgerOpening)
                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                Else
                    UpdateChildProgressBar("Opening not found for " + DtCustomer.Rows(I)("Name"), mChildPrgMaxVal, mChildPrgCnt)
                End If
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
            mChildPrgMaxVal = DtSupplier.Rows.Count
            For I As Integer = 0 To DtSupplier.Rows.Count - 1
                UpdateParentProgressBar("Inserting Opening For Suppliers", mParentPrgBarMaxVal)

                UpdateChildProgressBar("Deleting Old Ledger for " + AgL.XNull(DtSupplier.Rows(I)("Name")), mChildPrgMaxVal, mChildPrgCnt)
                FDeleteLedgerForSupplier(AgL.XNull(DtSupplier.Rows(I)("SubCode")))
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

                    Case rowTransferOpeningForCustomers, rowTransferOpeningForSuppliers, rowTransferOpeningForOtherAccounts
                        DglMain.Item(Col1Value, DglMain.CurrentCell.RowIndex).ReadOnly = True
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetChukti(SubCode As String, Conn As Object) As DataTable
        'Dim ReportFrm As New Aglibrary.FrmReportLayout("", "", "", "")
        Dim ReportFrm As New AgLibrary.FrmReportLayout(AgL, "", "", "", "")
        ReportFrm.CreateHelpGrid("Report Format", "Report Format", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Format 1")
        ReportFrm.CreateHelpGrid("As On Date", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", ClsMain.FormatDate((DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate)))))
        ReportFrm.CreateHelpGrid("Grace Days", "Grace Days", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", 60)
        ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, "", "", 450, 825, 300)
        ReportFrm.FGMain.Item(4, 3).Value = AgL.Chk_Text(SubCode)
        ReportFrm.CreateHelpGrid("Records Type", "Records Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "After Chukti")
        ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, "")
        ReportFrm.CreateHelpGrid("City", "City", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, "")
        ReportFrm.CreateHelpGrid("Area", "Area", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, "")
        ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, "", "[DIVISIONCODE]")
        ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, "", "[SITECODE]")
        ReportFrm.CreateHelpGrid("Interest Rate", "Interest Rate", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")))
        ReportFrm.CreateHelpGrid("Account Group", "Account Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, "")


        Dim CRepProc As New ClsConcurLedger(ReportFrm)
        Dim DsRep As DataSet = CRepProc.FunConcurLedger(Conn)
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
    Private Sub FDeleteLedger(SubCode As String)
        mQry = " Select L.DocId, L.V_Sno From Ledger L 
                Where L.SubCode = '" & SubCode & "'
                And L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " "
        Dim DtLedger As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For J As Integer = 0 To DtLedger.Rows.Count - 1
            mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLedger.Rows(J)("DocId")) & "'
                    And V_Sno = " & AgL.VNull(DtLedger.Rows(J)("V_Sno")) & " "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next
    End Sub
    Private Sub FDeleteLedgerForSupplier(SubCode As String)
        If AgL.VNull(AgL.Dman_Execute(" Select Count(*) As Cnt
                From LedgerHead H
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where H.SubCode = '" & SubCode & "' And Vt.NCat = '" & Ncat.PaymentSettlement & "'", AgL.GCn).ExecuteScalar()) <> 0 Then
            mQry = " SELECT L.DocID, L.V_Sno, L.V_Type || '-' || L.RecId As DocNo
                FROM Ledger L 
                LEFT JOIN Cloth_SupplierSettlementPayments S ON L.DocID = S.PaymentDocId 
                LEFT JOIN LedgerHead Lh ON S.DocID = Lh.DocID
                WHERE L.SubCode = '" & SubCode & "' 
                And L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " 
                AND Lh.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & ""
            Dim DtLedgerPayment As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For J As Integer = 0 To DtLedgerPayment.Rows.Count - 1
                mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLedgerPayment.Rows(J)("DocId")) & "'
                    And V_Sno = " & AgL.VNull(DtLedgerPayment.Rows(J)("V_Sno")) & " "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            mQry = " SELECT L.DocID, L.V_Sno, L.V_Type || '-' || L.RecId As DocNo
                FROM Ledger L 
                LEFT JOIN Cloth_SupplierSettlementInvoices S ON L.DocID = S.PurchaseInvoiceDocId 
                LEFT JOIN LedgerHead Lh ON S.DocID = Lh.DocID
                WHERE L.SubCode = '" & SubCode & "' 
                And L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " 
                AND Lh.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & ""
            Dim DtLedgerPurchase As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For J As Integer = 0 To DtLedgerPurchase.Rows.Count - 1
                mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLedgerPurchase.Rows(J)("DocId")) & "'
                    And V_Sno = " & AgL.VNull(DtLedgerPurchase.Rows(J)("V_Sno")) & " "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            mQry = " SELECT L.DocID, L.V_Sno, L.V_Type || '-' || L.RecId As DocNo
                FROM Ledger L 
                LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                WHERE L.SubCode = '" & SubCode & "' 
                And Vt.NCat = '" & Ncat.PaymentSettlement & "'
                And L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " "
            Dim DtLedgerSettlement As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For J As Integer = 0 To DtLedgerSettlement.Rows.Count - 1
                mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLedgerSettlement.Rows(J)("DocId")) & "'
                    And V_Sno = " & AgL.VNull(DtLedgerSettlement.Rows(J)("V_Sno")) & " "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next
        Else
            FDeleteLedger(SubCode)
            mQry = " Select L.SubCode, Max(Sg.Name) As SubCodeName, 
                        Case When IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) > 0 Then IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) Else 0 End As AmtDr,
                        Case When IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) < 0 Then Abs(IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0)) Else 0 End As AmtCr,
                        Null As ReferenceNo, Null As ReferenceDate
                        From Ledger L
                        LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
                        WHERE L.SubCode = '" & SubCode & "'
                        And L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & "
                        Group By L.SubCode 
                        Having IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) <> 0 "
            Dim DtSupplierOpening As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

            If DtSupplierOpening.Rows.Count > 0 Then
                FTransferOpening(DtSupplierOpening)
            End If
        End If
    End Sub
    Private Sub DglMain_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.KeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex
            Select Case mRow
                Case rowTransferOpeningForCustomers, rowTransferOpeningForSuppliers, rowTransferOpeningForOtherAccounts
                    If e.KeyCode <> Keys.Enter Then
                        If AgL.StrCmp(ChrW(e.KeyCode), "Y") Then
                            DglMain.Item(Col1Value, mRow).Value = "Yes"
                        ElseIf AgL.StrCmp(ChrW(e.KeyCode), "N") Then
                            DglMain.Item(Col1Value, mRow).Value = "No"
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FCompareOpeningAndClosing()
        If TxtFilePath.Text = "" Then MsgBox("No File selected.", MsgBoxStyle.Information) : Exit Sub

        Try
            mQry = "Attach '" & TxtFilePath.Text & "' AS LastYearData;"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Catch ex As Exception
        End Try

        Dim mLastYearDataClosing As String = "SELECT Sg.Code As LyCode, Max(Sg.Name) AS LyName, Max(Ag.GroupName) AS LyAcGroupName, Max(Ag.GroupNature) As LyGroupNature, Abs(IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0)) AS LyClosingBalance,
                CASE WHEN IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) > 0 THEN 'Dr'
	                 WHEN IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) < 0 THEN 'Cr' ELSE NULL END AS LyDrCr
                FROM LastYearData.ViewHelpSubgroup Sg
                LEFT JOIN LastYearData.AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                LEFT JOIN (Select * From LastYearData.Ledger Where V_Date  <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " ) As L ON L.SubCode = Sg.Code
                Where (L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " Or L.V_Date Is Null)
                GROUP BY Sg.Code "

        Dim mCurrentYearDataOpening As String = "SELECT 
                Sg.Code As CyCode, Max(Sg.Name) AS CyName, Max(Ag.GroupName) AS CyAcGroupName, Max(Ag.GroupNature) As CyGroupNature, Abs(IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0)) AS CyOpeningBalance,
                CASE WHEN IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) > 0 THEN 'Dr'
	                 WHEN IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) < 0 THEN 'Cr' ELSE NULL END AS CyDrCr
                FROM ViewHelpSubgroup Sg
                LEFT JOIN AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                LEFT JOIN (Select * From Ledger Where V_Date  <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " ) As L ON L.SubCode = Sg.Code
                Where (L.V_Date <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value).ToString("s")) & " Or L.V_Date Is Null)
                GROUP BY Sg.Code "

        'mQry = " Select VMain.LyCode,  Max(VMain.LyName) As LyName, Sum(VMain.LyClosingBalance) As LyClosingBalance, Max(VMain.LyDrCr) As LyDrCr, 
        '        VMain.CyCode, Max(VMain.CyName) As CyName, Sum(VMain.CyOpeningBalance) As CyOpeningBalance,  Max(VMain.CyDrCr) As CyDrCr
        '        From (" & mLastYearDataClosing & " UNION ALL " & mCurrentYearDataOpening & ") As VMain 
        '        Group By VMain.LyCode, VMain.CyCode "

        mQry = " Select Ly.LyCode,  Max(Ly.LyName) As LyName, Max(Ly.LyAcGroupName) As LyAcGroupName, Max(Ly.LyGroupNature) As LyGroupNature, Sum(Ly.LyClosingBalance) As LyClosingBalance, Max(Ly.LyDrCr) As LyDrCr, 
                Cy.CyCode, Max(Cy.CyName) As CyName, Max(Cy.CyAcGroupName) As CyAcGroupName, Max(Cy.CyGroupNature) As CyGroupNature, Sum(Cy.CyOpeningBalance) As CyOpeningBalance,  Max(Cy.CyDrCr) As CyDrCr
                From (" & mLastYearDataClosing & ") As Ly
                LEFT JOIN (" & mCurrentYearDataOpening & ") As Cy On Ly.LyCode = Cy.CyCode
                    And (Ly.LyName = Cy.CyName Or Ly.LyAcGroupName = Cy.CyAcGroupName)
                Group By Ly.LyCode "

        mQry += " UNION ALL "

        mQry += " Select Ly.LyCode,  Max(Ly.LyName) As LyName, Max(Ly.LyAcGroupName) As LyAcGroupName, Max(Ly.LyGroupNature) As LyGroupNature, Sum(Ly.LyClosingBalance) As LyClosingBalance, Max(Ly.LyDrCr) As LyDrCr, 
                Cy.CyCode, Max(Cy.CyName) As CyName, Max(Cy.CyAcGroupName) As CyAcGroupName, Max(Cy.CyGroupNature) As CyGroupNature, Sum(Cy.CyOpeningBalance) As CyOpeningBalance,  Max(Cy.CyDrCr) As CyDrCr
                From (" & mCurrentYearDataOpening & ") As Cy 
                LEFT JOIN (" & mLastYearDataClosing & ") As Ly On Ly.LyCode = Cy.CyCode
                    And (Ly.LyName = Cy.CyName Or Ly.LyAcGroupName = Cy.CyAcGroupName)
                Where Ly.LyCode Is Null
                Group By Cy.CyCode 
                Having Sum(Cy.CyOpeningBalance) <> 0
                Order By LyName "

        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        If DtTemp.Rows.Count > 0 Then
            For I As Integer = 0 To DtTemp.Rows.Count - 1
                Dgl2.Rows.Add()
                Dgl2.Item(Col2LyCode, I).Value = AgL.XNull(DtTemp.Rows(I)("LyCode"))
                Dgl2.Item(Col2LyName, I).Value = AgL.XNull(DtTemp.Rows(I)("LyName"))
                Dgl2.Item(Col2LyAcGroupName, I).Value = AgL.XNull(DtTemp.Rows(I)("LyAcGroupName"))
                Dgl2.Item(Col2LyGroupNature, I).Value = AgL.XNull(DtTemp.Rows(I)("LyGroupNature"))
                Dgl2.Item(Col2LyClosingBalance, I).Value = Math.Round(AgL.VNull(DtTemp.Rows(I)("LyClosingBalance")), 2)
                Dgl2.Item(Col2LyDrCr, I).Value = AgL.XNull(DtTemp.Rows(I)("LyDrCr"))
                Dgl2.Item(Col2CyCode, I).Value = AgL.XNull(DtTemp.Rows(I)("CyCode"))
                Dgl2.Item(Col2CyName, I).Value = AgL.XNull(DtTemp.Rows(I)("CyName"))
                Dgl2.Item(Col2CyAcGroupName, I).Value = AgL.XNull(DtTemp.Rows(I)("CyAcGroupName"))
                Dgl2.Item(Col2CyGroupNature, I).Value = AgL.XNull(DtTemp.Rows(I)("CyGroupNature"))
                Dgl2.Item(Col2CyOpeningBalance, I).Value = Math.Round(AgL.VNull(DtTemp.Rows(I)("CyOpeningBalance")), 2)
                Dgl2.Item(Col2CyDrCr, I).Value = AgL.XNull(DtTemp.Rows(I)("CyDrCr"))


                If (Dgl2.Item(Col2LyGroupNature, I).Value = "R" Or Dgl2.Item(Col2LyGroupNature, I).Value = "E") Then
                    If Dgl2.Item(Col2CyName, I).Value = "" Then
                        Dgl2.Rows(I).DefaultCellStyle.BackColor = Color.CadetBlue
                        Dgl2.Rows(I).DefaultCellStyle.ForeColor = Color.White
                        Dgl2.Rows(I).DefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                        Dgl2.Item(Col2Remark, I).Value = "No Corresponding A/c Name found."
                    End If

                    If Val(Dgl2.Item(Col2CyOpeningBalance, I).Value) <> 0 Then
                        Dgl2.Rows(I).DefaultCellStyle.BackColor = Color.ForestGreen
                        Dgl2.Rows(I).DefaultCellStyle.ForeColor = Color.White
                        Dgl2.Rows(I).DefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                        Dgl2.Item(Col2Remark, I).Value = "Expense & Income Opening should be 0."
                    End If
                End If

                If Val(Dgl2.Item(Col2LyClosingBalance, I).Value) <> Val(Dgl2.Item(Col2CyOpeningBalance, I).Value) Or
                        Dgl2.Item(Col2LyDrCr, I).Value <> Dgl2.Item(Col2LyDrCr, I).Value Then
                    If Dgl2.Item(Col2LyGroupNature, I).Value <> "R" And Dgl2.Item(Col2LyGroupNature, I).Value <> "E" Then
                        Dgl2.Rows(I).DefaultCellStyle.BackColor = Color.Red
                        Dgl2.Rows(I).DefaultCellStyle.ForeColor = Color.White
                        Dgl2.Rows(I).DefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                        Dgl2.Item(Col2Remark, I).Value = "Balances Mismatched"
                    End If
                ElseIf Dgl2.Item(Col2LyAcGroupName, I).Value <> Dgl2.Item(Col2CyAcGroupName, I).Value Then
                    Dgl2.Rows(I).DefaultCellStyle.BackColor = Color.MediumVioletRed
                    Dgl2.Rows(I).DefaultCellStyle.ForeColor = Color.White
                    Dgl2.Rows(I).DefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                    Dgl2.Item(Col2Remark, I).Value = "A/c Groups Mismatched"
                ElseIf Dgl2.Item(Col2LyName, I).Value <> Dgl2.Item(Col2CyName, I).Value Then
                    Dgl2.Rows(I).DefaultCellStyle.BackColor = Color.PaleVioletRed
                    Dgl2.Rows(I).DefaultCellStyle.ForeColor = Color.White
                    Dgl2.Rows(I).DefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                    Dgl2.Item(Col2Remark, I).Value = "Names Mismatched"
                End If
            Next
        End If
    End Sub
    Private Sub Tc1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tc1.SelectedIndexChanged
        Select Case Tc1.TabPages(Tc1.SelectedIndex).Name
            Case Tp2.Name
                FCompareOpeningAndClosing()
        End Select
    End Sub
    Private Sub DGL1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If e.RowIndex > -1 Then Dgl1.Rows(e.RowIndex).Selected = True
        Dgl1.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray
    End Sub
    Private Sub Dgl2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.RowEnter
        If e.RowIndex > -1 Then Dgl2.Rows(e.RowIndex).Selected = True
        Dgl2.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray
    End Sub
End Class