Imports AgLibrary.ClsMain.agConstants
Imports AgTemplate.ClsMain
Imports System.Threading
Imports System.ComponentModel

Public Class FrmSplitData
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_Pakka As New SQLite.SQLiteConnection
    Public mDbPath As String = ""
    Dim Connection_ExternalDatabase As New SQLite.SQLiteConnection

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker

    Dim DtItem As DataTable
    Dim DtSubGroup As DataTable
    Dim DtSaleInvoice As DataTable
    Dim DtSaleInvoiceDetail As DataTable
    Dim DtPurchInvoice As DataTable
    Dim DtPurchInvoiceDetail As DataTable
    Dim DtLedgerHead As DataTable
    Dim DtLedgerHeadDetail As DataTable

    Dim bIsMastersImportedSuccessfully As Boolean = True
    Dim bIsSaleOrdersImportedSuccessfully As Boolean = True
    Dim bIsSaleInvoicesImportedSuccessfully As Boolean = True
    Dim bIsPurchaseInvoicesImportedSuccessfully As Boolean = True

    Private Delegate Sub UpdateLabelInvoker(ByVal text As String)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnSync.Click
        BtnSync.Enabled = False
        _backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        _backgroundWorker1.WorkerSupportsCancellation = False
        _backgroundWorker1.WorkerReportsProgress = False
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FDeleteNewDataFromOldDatabase)
        _backgroundWorker1.RunWorkerAsync()
    End Sub
    Public Sub FDeleteNewDataFromOldDatabase()
        Dim mTrans As String = ""
        Dim bConStr$ = ""

        UpdateLabel("Initializing...")

        If MsgBox("Are you sure you want to proceed delete data ?" & vbNewLine & "This will wash selected data.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                bConStr = " Where DocId In (SELECT H.DocID
                FROM SaleInvoice H 
                WHERE Date(H.V_Date) >= " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"

                mQry = "DELETE FROM SaleInvoiceBarcodeLastTransactionValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailBarCodeValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailHelpValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDimensionDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDimensionDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoicePayment " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceReferences " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoice " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                bConStr = " Where DocId In (SELECT H.DocID
                    FROM PurchInvoice H 
                    WHERE Date(H.V_Date) >= " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"

                mQry = "DELETE FROM PurchInvoiceBarcodeLastTransactionValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBarCodeValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBom " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBomSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailHelpValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDimensionDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDimensionDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceTransport " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoice " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                bConStr = " Where DocId In (SELECT H.DocID
                    FROM LedgerHead H 
                    WHERE Date(H.V_Date) >= " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"

                mQry = "DELETE FROM Ledger " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerAdj " & " Where Vr_DocId In (SELECT H.DocID
                FROM LedgerHead H 
                WHERE Date(H.V_Date) >= " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadCharges " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetailCharges " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetailChequePrinting " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerItemAdj " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerM " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHead " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                bConStr = " Where DocId In (SELECT H.DocID
                    FROM StockHead H 
                    WHERE Date(H.V_Date) >= " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"

                mQry = "DELETE FROM Stock " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM StockHeadDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM StockHead " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                AgL.ETrans.Commit()
                mTrans = "Commit"
                MsgBox("Process Complete.", MsgBoxStyle.Information)
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
        UpdateLabel(" ")
        MsgBox("Process Completed Successfully...", MsgBoxStyle.Information)
    End Sub
    Public Sub FDeleteOldDataFromNewDatabase()
        Dim mTrans As String = ""
        Dim bConStr$ = ""

        UpdateLabel("Initializing...")

        If MsgBox("Are you sure you want to proceed delete data ?" & vbNewLine & "This will wash selected data.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                bConStr = " Where DocId In (SELECT H.DocID
                FROM SaleInvoice H 
                WHERE Date(H.V_Date) < " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"

                mQry = "DELETE FROM SaleInvoiceBarcodeLastTransactionValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailBarCodeValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailHelpValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDimensionDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDimensionDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoicePayment " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceReferences " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoice " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                bConStr = " Where DocId In (SELECT H.DocID
                    FROM PurchInvoice H 
                    WHERE Date(H.V_Date) < " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"


                mQry = "DELETE FROM PurchInvoiceBarcodeLastTransactionValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBarCodeValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBom " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBomSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailHelpValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDimensionDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDimensionDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceTransport " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoice " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                bConStr = " Where DocId In (SELECT H.DocID
                    FROM LedgerHead H 
                    WHERE Date(H.V_Date) < " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"

                mQry = "DELETE FROM Ledger " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerAdj " & " Where Vr_DocId In (SELECT H.DocID
                FROM LedgerHead H 
                WHERE Date(H.V_Date) < " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadCharges " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetailCharges " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetailChequePrinting " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerItemAdj " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerM " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHead " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                bConStr = " Where DocId In (SELECT H.DocID
                    FROM StockHead H 
                    WHERE Date(H.V_Date) < " & AgL.Chk_Date(CDate("01/Apr/2019").ToString("s")) & ")"

                mQry = "DELETE FROM Stock " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM StockHeadDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM StockHead " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                AgL.ETrans.Commit()
                mTrans = "Commit"
                MsgBox("Process Complete.", MsgBoxStyle.Information)
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
        UpdateLabel(" ")
        MsgBox("Process Completed Successfully...", MsgBoxStyle.Information)
    End Sub

    Public Sub UpdateLabel(ByVal Value As String)
        If Me.LblProgress.InvokeRequired Then
            Me.LblProgress.Invoke(New UpdateLabelInvoker(AddressOf Me.UpdateLabel), New Object() {Value})
            'Me.lblStatus.Invoke(New MethodInvoker(Me, DirectCast(Me.SaveCompleted, IntPtr)))
        Else
            Me.LblProgress.Text = Value
            LblProgress.Refresh()
        End If
    End Sub
    Private Sub FTransferOpening()
        mQry = " Select L.SubCode, IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) As Balance
                From Ledger L
                Group By L.SubCode "
        Dim DtHeader As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I As Integer = 0 To DtHeader.Rows.Count - 1
                Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead

                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = "OB"
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.V_Date = "31/Mar/2019"
                VoucherEntryTable.V_No = 1
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.ManualRefNo = 1
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


                mQry = " Select L.SubCode, 
                        Case When IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) > 0 Then IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) Else 0 End As AmtDr,
                        Case When IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) < 0 Then Abs(IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0)) Else 0 End As AmtCr
                        From Ledger L
                        Where L.SubCode = '" & DtHeader.Rows(I)("SubCode") & "' "
                Dim DtLine As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For J As Integer = 0 To DtLine.Rows.Count - 1
                    VoucherEntryTable.Line_Sr = J + 1
                    VoucherEntryTable.Line_SubCode = AgL.XNull(DtLine.Rows(J)("SubCode"))
                    VoucherEntryTable.Line_SubCodeName = ""
                    VoucherEntryTable.Line_SpecificationDocID = ""
                    VoucherEntryTable.Line_SpecificationDocIDSr = ""
                    VoucherEntryTable.Line_Specification = ""
                    VoucherEntryTable.Line_SalesTaxGroupItem = ""
                    VoucherEntryTable.Line_Qty = 0
                    VoucherEntryTable.Line_Unit = ""
                    VoucherEntryTable.Line_Rate = 0
                    VoucherEntryTable.Line_Amount = AgL.VNull(DtLine.Rows(J)("AmtDr"))
                    VoucherEntryTable.Line_Amount_Cr = AgL.VNull(DtLine.Rows(J)("AmtCr"))
                    VoucherEntryTable.Line_ChqRefNo = ""
                    VoucherEntryTable.Line_ChqRefDate = ""
                    VoucherEntryTable.Line_ReferenceNo = AgL.XNull(DtLine.Rows(J)("v_no"))
                    VoucherEntryTable.Line_ReferenceDate = AgL.XNull(DtLine.Rows(J)("date"))
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

                AgL.ETrans.Commit()
                mTrans = "Commit"
            Next
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
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
End Class