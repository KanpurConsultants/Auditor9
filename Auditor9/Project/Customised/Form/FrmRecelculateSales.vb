Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Customised.FrmSaleInvoiceDirect_WithDimension
Public Class FrmRecelculateSales
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_ExternalDatabase As New SQLite.SQLiteConnection
    Public mDbPath As String = ""
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker


    Public Const Col1Head As String = "Head"
    Public Const Col1Status As String = "Status"
    Public Const Col1Message As String = "Message"

    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1

    Public Const hcFromDate As String = "From Date"
    Public Const hcToDate As String = "To Date"


    Dim mParentPrgBarMaxVal As Integer = 0


    Private Delegate Sub UpdateParentProgressBarInvoker(ByVal Value As String, ParentPrMaxVal As Integer)
    Private Delegate Sub FRecordMessageInvoker(Head As String, Status As String, Message As String, Conn As Object, Cmd As Object)
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        'BtnOK.Enabled = False
        If Not TxtPassword.Text = "123456" Then
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
        DglMain.BorderStyle = BorderStyle.None

        DglMain.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        DglMain.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        DglMain.BackgroundColor = Me.BackColor
        DglMain.CellBorderStyle = DataGridViewCellBorderStyle.None
        AgCL.GridSetiingShowXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain, False)


        DglMain.Rows.Add(2)

        DglMain.Item(Col1Head, rowFromDate).Value = hcFromDate
        DglMain.Item(Col1Head, rowToDate).Value = hcToDate


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

        If AgL.XNull(DglMain.Item(Col1Value, rowFromDate).Value) = "" Or AgL.XNull(DglMain.Item(Col1Value, rowToDate).Value) = "" Then
            MsgBox("Date is required.", MsgBoxStyle.Information)
            Exit Sub
        End If


        mQry = " Select Distinct H.DocId, H.V_Type || '-' || H.ManualRefNo As InvoiceNo
                From SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN (Select DocId, Count(*) As CntPaymentModes From SaleInvoicePayment Where IfNull(PaymentMode,'') <> 'Cash' Group By DocId) As Sip On H.DocId = Sip.DocId
                Where Vt.NCat = '" & Ncat.SaleInvoice & "' 
                And IfNull(Ig.CalcCode,0) > 0 
                And IfNull(Sip.CntPaymentModes,0) = 0
                And H.ReCalculationBy Is Null "
        mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowFromDate).Value).ToString("s")) & ""
        mQry = mQry & " AND Date(H.V_Date) <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowToDate).Value).ToString("s")) & ""
        Dim DtTables As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mParentPrgBarMaxVal = DtTables.Rows.Count

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I As Integer = 0 To DtTables.Rows.Count - 1
                UpdateParentProgressBar("Re-Calculating Bill " & AgL.XNull(DtTables.Rows(I)("InvoiceNo")) & ".", mParentPrgBarMaxVal)
                FGenerateNewSaleEntry(AgL.XNull(DtTables.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
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
    Public Sub FGenerateNewSaleEntry(SearchCode As String, Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer



        mQry = " Select H.*
                From SaleInvoice H With (NoLock)
                Where H.DocId = '" & SearchCode & "'"
        Dim DtHeaderSource As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        mQry = " SELECT L.*, Lv.PurchaseRate, Lv.PurchaseDiscountPer, 
                Lv.PurchaseAdditionalDiscountPer, Lv.PurchaseDeal,
                Ls.ItemCategory, Ls.ItemGroup, Ig.CalcCode
                FROM SaleInvoiceDetail L With (NoLock)
                LEFT JOIN SaleInvoiceDetailSku Ls With (NoLock) On L.DocId = Ls.DocId And L.Sr = Ls.Sr
                LEFT JOIN SaleInvoiceDetailHelpValues Lv With (NoLock) On L.DocId = Lv.DocId 
                        And L.Sr = Lv.Sr
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                Where L.DocId = '" & SearchCode & "'"
        Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        mQry = " Select * From SaleInvoicePayment Where DocId = '" & SearchCode & "'"
        Dim DtPaymentDetailSource As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        For I = 0 To DtHeaderSource.Rows.Count - 1
            Dim Tot_Gross_Amount As Double = 0
            Dim Tot_Taxable_Amount As Double = 0
            Dim Tot_Tax1 As Double = 0
            Dim Tot_Tax2 As Double = 0
            Dim Tot_Tax3 As Double = 0
            Dim Tot_Tax4 As Double = 0
            Dim Tot_Tax5 As Double = 0
            Dim Tot_SubTotal1 As Double = 0
            Dim Tot_Other_Charge As Double = 0
            Dim Tot_Deduction As Double = 0

            Dim SaleInvoiceTableList(0) As FrmSaleInvoiceDirect_WithDimension.StructSaleInvoice
            Dim SaleInvoiceTable As New FrmSaleInvoiceDirect_WithDimension.StructSaleInvoice

            SaleInvoiceTable.DocID = ""
            SaleInvoiceTable.V_Type = AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))
            SaleInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
            SaleInvoiceTable.Site_Code = AgL.XNull(DtHeaderSource.Rows(I)("Site_Code"))
            SaleInvoiceTable.Div_Code = AgL.XNull(DtHeaderSource.Rows(I)("Div_Code"))
            SaleInvoiceTable.V_No = 0
            SaleInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
            SaleInvoiceTable.ManualRefNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))

            SaleInvoiceTable.SaleToParty = AgL.XNull(DtHeaderSource.Rows(I)("SaleToParty"))
            SaleInvoiceTable.SaleToPartyName = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyName"))
            SaleInvoiceTable.AgentCode = AgL.XNull(DtHeaderSource.Rows(I)("Agent"))
            SaleInvoiceTable.AgentName = ""
            SaleInvoiceTable.BillToPartyCode = AgL.XNull(DtHeaderSource.Rows(I)("BillToParty"))
            SaleInvoiceTable.BillToPartyName = ""
            SaleInvoiceTable.SaleToPartyAddress = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyAddress"))
            SaleInvoiceTable.SaleToPartyCityCode = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyCity"))
            SaleInvoiceTable.SaleToPartyMobile = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyMobile"))
            SaleInvoiceTable.SaleToPartySalesTaxNo = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartySalesTaxNo"))
            SaleInvoiceTable.ShipToAddress = AgL.XNull(DtHeaderSource.Rows(I)("ShipToAddress"))
            SaleInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtHeaderSource.Rows(I)("SalesTaxGroupParty"))
            SaleInvoiceTable.PlaceOfSupply = AgL.XNull(DtHeaderSource.Rows(I)("PlaceOfSupply"))
            SaleInvoiceTable.StructureCode = AgL.XNull(DtHeaderSource.Rows(I)("Structure"))
            SaleInvoiceTable.CustomFields = AgL.XNull(DtHeaderSource.Rows(I)("CustomFields"))
            SaleInvoiceTable.SaleToPartyDocNo = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyDocNo"))
            SaleInvoiceTable.SaleToPartyDocDate = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyDocDate"))
            SaleInvoiceTable.ReferenceDocId = ""
            SaleInvoiceTable.Tags = AgL.XNull(DtHeaderSource.Rows(I)("Tags"))
            SaleInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
            SaleInvoiceTable.Status = "Active"
            SaleInvoiceTable.EntryBy = AgL.PubUserName
            SaleInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            SaleInvoiceTable.ApproveBy = ""
            SaleInvoiceTable.ApproveDate = ""
            SaleInvoiceTable.MoveToLog = ""
            SaleInvoiceTable.MoveToLogDate = ""
            SaleInvoiceTable.UploadDate = ""
            SaleInvoiceTable.GenDocId = ""
            SaleInvoiceTable.OmsId = ""
            SaleInvoiceTable.LockText = ""

            Dim DtSaleInvoiceDetail_ForHeader As New DataTable
            For M As Integer = 0 To DtLineDetailSource.Columns.Count - 1
                Dim DColumn As New DataColumn
                DColumn.ColumnName = DtLineDetailSource.Columns(M).ColumnName
                DtSaleInvoiceDetail_ForHeader.Columns.Add(DColumn)
            Next

            Dim DtRowSaleInvoiceDetail_ForHeader As DataRow() = DtLineDetailSource.Select("DocId = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("DocId"))))
            If DtRowSaleInvoiceDetail_ForHeader.Length > 0 Then
                For M As Integer = 0 To DtRowSaleInvoiceDetail_ForHeader.Length - 1
                    DtSaleInvoiceDetail_ForHeader.Rows.Add()
                    For N As Integer = 0 To DtSaleInvoiceDetail_ForHeader.Columns.Count - 1
                        DtSaleInvoiceDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDetail_ForHeader(M)(N)
                    Next
                Next
            End If


            For J = 0 To DtSaleInvoiceDetail_ForHeader.Rows.Count - 1
                SaleInvoiceTable.Line_Sr = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Sr"))
                SaleInvoiceTable.Line_ItemCategoryCode = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ItemCategory"))
                SaleInvoiceTable.Line_ItemGroupCode = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ItemGroup"))
                SaleInvoiceTable.Line_ItemCode = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Item"))
                SaleInvoiceTable.Line_ItemName = ""
                SaleInvoiceTable.Line_Barcode = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Barcode"))
                SaleInvoiceTable.Line_Specification = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Specification"))
                SaleInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SalesTaxGroupItem"))
                SaleInvoiceTable.Line_ReferenceNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ReferenceNo"))
                SaleInvoiceTable.Line_DocQty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DocQty"))
                SaleInvoiceTable.Line_FreeQty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("FreeQty"))
                SaleInvoiceTable.Line_Qty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Qty"))
                SaleInvoiceTable.Line_Unit = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Unit"))
                SaleInvoiceTable.Line_Pcs = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Pcs"))
                SaleInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("UnitMultiplier"))
                SaleInvoiceTable.Line_DealUnit = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DealUnit"))
                SaleInvoiceTable.Line_DocDealQty = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DocDealQty"))
                SaleInvoiceTable.Line_OmsId = ""
                'SaleInvoiceTable.Line_Rate = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate"))

                If AgL.StrCmp(AgL.PubDBName, "SHADHVINANDI") Then
                    SaleInvoiceTable.Line_Rate = Math.Round(AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate")) - (AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate")) * AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("CalcCode")) / 100), 0)
                Else
                    SaleInvoiceTable.Line_Rate = Math.Round(AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate")) - (AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate")) * AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("CalcCode")) / 100), 2)
                End If


                SaleInvoiceTable.Line_DiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DiscountPer"))
                SaleInvoiceTable.Line_DiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DiscountAmount"))
                SaleInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountPer"))
                SaleInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountAmount"))
                'SaleInvoiceTable.Line_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Amount"))
                SaleInvoiceTable.Line_Amount = (SaleInvoiceTable.Line_Qty * SaleInvoiceTable.Line_Rate) - SaleInvoiceTable.Line_DiscountAmount - SaleInvoiceTable.Line_AdditionalDiscountAmount
                SaleInvoiceTable.Line_Remark = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Remark"))
                SaleInvoiceTable.Line_BaleNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                SaleInvoiceTable.Line_LotNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("LotNo"))

                SaleInvoiceTable.Line_Deal = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deal"))
                SaleInvoiceTable.Line_MRP = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("MRP"))
                SaleInvoiceTable.Line_Expiry = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ExpiryDate"))

                SaleInvoiceTable.Line_PurchaseRate = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("PurchaseRate"))
                SaleInvoiceTable.Line_PurchaseDiscountPer = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("PurchaseDiscountPer"))
                SaleInvoiceTable.Line_PurchaseAdditionalDiscountPer = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("PurchaseAdditionalDiscountPer"))
                SaleInvoiceTable.Line_PurchaseDeal = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("PurchaseDeal"))




                SaleInvoiceTable.Line_ReferenceDocId = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ReferenceDocId"))
                SaleInvoiceTable.Line_GrossWeight = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("GrossWeight"))
                SaleInvoiceTable.Line_NetWeight = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("NetWeight"))

                SaleInvoiceTable.Line_Gross_Amount = SaleInvoiceTable.Line_Amount

                SaleInvoiceTable.Line_Tax1_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                SaleInvoiceTable.Line_Tax2_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                SaleInvoiceTable.Line_Tax3_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                SaleInvoiceTable.Line_Tax4_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                SaleInvoiceTable.Line_Tax5_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))

                If AgL.StrCmp(AgL.PubDBName, "SHADHVINANDI") Then
                    Dim Tax As Double = 0
                    Tax = SaleInvoiceTable.Line_Tax1_Per + SaleInvoiceTable.Line_Tax2_Per + SaleInvoiceTable.Line_Tax3_Per + SaleInvoiceTable.Line_Tax4_Per + SaleInvoiceTable.Line_Tax5_Per
                    SaleInvoiceTable.Line_Taxable_Amount = SaleInvoiceTable.Line_Amount * 100 / (100 + Tax)
                Else
                    SaleInvoiceTable.Line_Taxable_Amount = SaleInvoiceTable.Line_Amount
                End If


                SaleInvoiceTable.Line_Tax1 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax1_Per / 100, 2)
                SaleInvoiceTable.Line_Tax2 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax2_Per / 100, 2)
                SaleInvoiceTable.Line_Tax3 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax3_Per / 100, 2)
                SaleInvoiceTable.Line_Tax4 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax4_Per / 100, 2)
                SaleInvoiceTable.Line_Tax5 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax5_Per / 100, 2)
                SaleInvoiceTable.Line_SubTotal1 = SaleInvoiceTable.Line_Taxable_Amount + SaleInvoiceTable.Line_Tax1 + SaleInvoiceTable.Line_Tax2 + SaleInvoiceTable.Line_Tax3 + SaleInvoiceTable.Line_Tax4 + SaleInvoiceTable.Line_Tax5
                SaleInvoiceTable.Line_Other_Charge = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                SaleInvoiceTable.Line_Deduction = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                SaleInvoiceTable.Line_Round_Off = 0
                SaleInvoiceTable.Line_Net_Amount = SaleInvoiceTable.Line_SubTotal1 + SaleInvoiceTable.Line_Other_Charge - SaleInvoiceTable.Line_Deduction

                'For Header Values
                Tot_Gross_Amount += SaleInvoiceTable.Line_Gross_Amount
                Tot_Taxable_Amount += SaleInvoiceTable.Line_Taxable_Amount
                Tot_Tax1 += SaleInvoiceTable.Line_Tax1
                Tot_Tax2 += SaleInvoiceTable.Line_Tax2
                Tot_Tax3 += SaleInvoiceTable.Line_Tax3
                Tot_Tax4 += SaleInvoiceTable.Line_Tax4
                Tot_Tax5 += SaleInvoiceTable.Line_Tax5
                Tot_SubTotal1 += SaleInvoiceTable.Line_SubTotal1
                Tot_Other_Charge += SaleInvoiceTable.Line_Other_Charge
                Tot_Deduction += SaleInvoiceTable.Line_Deduction

                SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)
            Next

            SaleInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
            SaleInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
            SaleInvoiceTableList(0).Tax1 = Tot_Tax1
            SaleInvoiceTableList(0).Tax2 = Tot_Tax2
            SaleInvoiceTableList(0).Tax3 = Tot_Tax3
            SaleInvoiceTableList(0).Tax4 = Tot_Tax4
            SaleInvoiceTableList(0).Tax5 = Tot_Tax5
            SaleInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
            SaleInvoiceTableList(0).Other_Charge = Tot_Other_Charge
            SaleInvoiceTableList(0).Deduction = Tot_Deduction
            SaleInvoiceTableList(0).Round_Off = Math.Round(Tot_SubTotal1 + Tot_Other_Charge - Tot_Deduction, 0) - (Tot_SubTotal1 + Tot_Other_Charge - Tot_Deduction)
            SaleInvoiceTableList(0).Net_Amount = Math.Round(Tot_SubTotal1 + Tot_Other_Charge - Tot_Deduction, 0)


            'Dim Tot_Other_Charge As Double = 0
            'Dim Tot_Deduction As Double = 0
            Dim Tot_RoundOff As Double = 0
            Dim Tot_NetAmount As Double = 0
            For J = 0 To SaleInvoiceTableList.Length - 1
                'SaleInvoiceTableList(J).Line_Other_Charge = Math.Round(SaleInvoiceTableList(0).Other_Charge * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                'SaleInvoiceTableList(J).Line_Deduction = Math.Round(SaleInvoiceTableList(0).Deduction * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)

                SaleInvoiceTableList(J).Line_Round_Off = Math.Round(SaleInvoiceTableList(0).Round_Off * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                'SaleInvoiceTableList(J).Line_Net_Amount = Math.Round(SaleInvoiceTableList(0).Net_Amount * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                SaleInvoiceTableList(J).Line_Net_Amount = Math.Round(SaleInvoiceTableList(J).Line_Net_Amount - SaleInvoiceTableList(J).Line_Round_Off, 2)

                'Tot_Other_Charge += SaleInvoiceTableList(J).Line_Other_Charge
                'Tot_Deduction += SaleInvoiceTableList(J).Line_Deduction
                Tot_RoundOff += SaleInvoiceTableList(J).Line_Round_Off
                Tot_NetAmount += SaleInvoiceTableList(J).Line_Net_Amount
            Next

            Tot_RoundOff = Math.Round(Tot_RoundOff, 2)

            'If Tot_Other_Charge <> SaleInvoiceTableList(0).Other_Charge Then
            '    SaleInvoiceTableList(0).Line_Other_Charge = SaleInvoiceTableList(0).Line_Other_Charge + (SaleInvoiceTableList(0).Other_Charge - Tot_Other_Charge)
            'End If
            'If Tot_Deduction <> SaleInvoiceTableList(0).Deduction Then
            '    SaleInvoiceTableList(0).Line_Deduction = SaleInvoiceTableList(0).Line_Deduction + (SaleInvoiceTableList(0).Deduction - Tot_Deduction)
            'End If
            If Tot_RoundOff <> SaleInvoiceTableList(0).Round_Off Then
                SaleInvoiceTableList(0).Line_Round_Off = SaleInvoiceTableList(0).Line_Round_Off + (SaleInvoiceTableList(0).Round_Off - Tot_RoundOff)
            End If
            If Tot_NetAmount <> SaleInvoiceTableList(0).Net_Amount Then
                SaleInvoiceTableList(0).Line_Net_Amount = SaleInvoiceTableList(0).Line_Net_Amount + (SaleInvoiceTableList(0).Net_Amount - Tot_NetAmount)
            End If

            If AgL.VNull(DtHeaderSource.Rows(I)("PaidAmt")) <> 0 Then
                SaleInvoiceTableList(0).PaidAmt = SaleInvoiceTableList(0).Net_Amount
            End If

            Dim SaleInvoicePaymentTableList(0) As FrmSaleInvoiceDirect_WithDimension.StructSaleInvoicePayment
            For K As Integer = 0 To DtPaymentDetailSource.Rows.Count - 1
                Dim SaleInvoicePaymentTable As New FrmSaleInvoiceDirect_WithDimension.StructSaleInvoicePayment

                SaleInvoicePaymentTable.DocId = ""
                SaleInvoicePaymentTable.Sr = AgL.VNull(DtPaymentDetailSource.Rows(K)("Sr"))
                SaleInvoicePaymentTable.PaymentMode = AgL.XNull(DtPaymentDetailSource.Rows(K)("PaymentMode"))
                SaleInvoicePaymentTable.PostToAc = AgL.XNull(DtPaymentDetailSource.Rows(K)("PostToAc"))
                SaleInvoicePaymentTable.Amount = AgL.VNull(SaleInvoiceTableList(0).Net_Amount)



                SaleInvoicePaymentTableList(UBound(SaleInvoicePaymentTableList)) = SaleInvoicePaymentTable
                ReDim Preserve SaleInvoicePaymentTableList(UBound(SaleInvoicePaymentTableList) + 1)
            Next



            Dim bDocId As String = InsertSaleInvoice(SaleInvoiceTableList,, SaleInvoicePaymentTableList)

            FSaveSalesTaxSummaryStr(bDocId, Conn, Cmd)

            mQry = " UPDATE SaleInvoice Set ReCalculationBy = '" & AgL.PubUserName & "', 
                    ReCalculationDate = " & AgL.Chk_Date(SaleInvoiceTableList(0).UploadDate) & " 
                    Where DocId = '" & bDocId & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            FDeleteOldSale(SearchCode, Conn, Cmd)
        Next
    End Sub
    Private Sub FDeleteOldSale(SearchCode As String, Conn As Object, Cmd As Object)
        mQry = "Select DocId From SaleInvoice With (NoLock) Where DocId = '" & SearchCode & "'"
        Dim DtGeneratedEntries As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        For I As Integer = 0 To DtGeneratedEntries.Rows.Count - 1
            mQry = " Delete From SaleInvoiceTrnSetting Where DocId = '" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = " Delete From StockAdj Where StockOutDocId = '" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = " Delete From SaleInvoicePayment Where DocId = '" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = " Delete From SaleInvoiceDimensionDetail Where DocId = '" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete from SaleInvoiceDimensionDetailSku where DocID='" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete from SaleInvoiceBarcodeLastTransactionValues where DocID='" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete from SaleInvoiceTransport where DocID='" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete from SaleInvoiceDetailHelpValues where DocID='" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete from SaleInvoiceDetailSku where DocID='" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete from SaleInvoiceDetail where DocID='" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete from SaleInvoice where DocID='" & AgL.XNull(DtGeneratedEntries.Rows(I)("DocId")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Next
    End Sub
    Private Sub FSaveSalesTaxSummaryStr(bDocId As String, Conn As Object, Cmd As Object)
        Dim SalesTaxSummaryStrColumns As String = ""
        'SalesTaxSummaryStrColumns = FGetSettings(SettingFields.SalesTaxSummaryStrColumns, SettingType.General)
        mQry = " Select H.Div_Code, H.Site_Code, H.V_Type, H.SettingGroup, Vt.NCat
                From SaleInvoice H With (NoLock)
                LEFT JOIN Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Where H.DocId = '" & bDocId & "'"
        Dim DtSaleInvoice As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        SalesTaxSummaryStrColumns = ClsMain.FGetSettings(SettingFields.SalesTaxSummaryStrColumns, SettingType.General,
                    AgL.XNull(DtSaleInvoice.Rows(0)("Div_Code")),
                    AgL.XNull(DtSaleInvoice.Rows(0)("Site_Code")),
                    VoucherCategory.Sales, AgL.XNull(DtSaleInvoice.Rows(0)("NCat")),
                    AgL.XNull(DtSaleInvoice.Rows(0)("V_Type")), "",
                    AgL.XNull(DtSaleInvoice.Rows(0)("SettingGroup")))
        If SalesTaxSummaryStrColumns <> "" Then
            mQry = " Select L.DocID "
            If SalesTaxSummaryStrColumns.Contains("HSN") Then
                mQry += " ,IfNull(I.HSN,Ic.HSN) As HSN "
            End If
            If SalesTaxSummaryStrColumns.Contains("SALES TAX GROUP") Then
                mQry += " ,L.SalesTaxGroupItem "
            End If
            mQry += " ,Sum(L.Taxable_Amount) As Taxable_Amount, 
                Sum(L.Tax1) As Tax1, Sum(L.Tax2) As Tax2, Sum(L.Tax3) As Tax3
                From SaleInvoiceDetail L  With (NoLock)
                LEFT JOIN Item I With (NoLock) ON L.Item = I.Code
                LEFT JOIN ItemCategory Ic  With (NoLock) On I.ItemCategory = Ic.Code
                Where L.DocId = '" & bDocId & "'
                Group By L.DocId "
            If SalesTaxSummaryStrColumns.Contains("HSN") Then
                mQry += " ,IfNull(I.HSN,Ic.HSN) "
            End If
            If SalesTaxSummaryStrColumns.Contains("SALES TAX GROUP") Then
                mQry += " ,L.SalesTaxGroupItem "
            End If
            Dim DtSalesTaxSummary As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)



            Dim SalesTaxSummaryStr As String = ""
            For I As Integer = 0 To DtSalesTaxSummary.Rows.Count - 1
                If SalesTaxSummaryStrColumns.Contains("HSN") Then SalesTaxSummaryStr += AgL.XNull(DtSalesTaxSummary.Rows(I)("HSN"))
                If SalesTaxSummaryStrColumns.Contains("SALES TAX GROUP") Then SalesTaxSummaryStr += " " & AgL.XNull(DtSalesTaxSummary.Rows(I)("SalesTaxGroupItem"))
                If SalesTaxSummaryStrColumns.Contains("TAXABLE AMOUNT") Then SalesTaxSummaryStr += " Taxable Amt : " & AgL.XNull(DtSalesTaxSummary.Rows(I)("Taxable_Amount"))
                If SalesTaxSummaryStrColumns.Contains("TAX AMOUNT") Then SalesTaxSummaryStr += " Tax Amt : " & (AgL.VNull(DtSalesTaxSummary.Rows(I)("Tax1")) + AgL.VNull(DtSalesTaxSummary.Rows(I)("Tax2")) + AgL.VNull(DtSalesTaxSummary.Rows(I)("Tax3"))).ToString()
                SalesTaxSummaryStr += ", "
            Next

            mQry = " UPDATE SaleInvoice Set SalesTaxSummaryStr = '" & SalesTaxSummaryStr & "'
                Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub DglMain_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex

            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case mRow
                Case rowFromDate, rowToDate
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

