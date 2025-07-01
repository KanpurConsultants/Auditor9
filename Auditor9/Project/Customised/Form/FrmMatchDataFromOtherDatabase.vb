Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.FrmSaleInvoiceDirect_WithDimension
Public Class FrmMatchDataFromOtherDatabase
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

    Dim rowDataSyncFromDate As Integer = 0
    Public Const hcDataSyncFromDate As String = "Data Match From Date"


    Dim DtSiteMast As DataTable
    Dim DtDivMast As DataTable
    Dim DtExternalData_Item As New DataTable
    Dim DtExternalData_Subgroup As New DataTable
    Dim DtExternalData_SaleInvoice As New DataTable
    Dim DtExternalData_PurchInvoice As New DataTable
    Dim DtExternalData_LedgerHead As New DataTable

    Dim mParentPrgBarMaxVal As Integer = 0


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
        DglMain.BorderStyle = BorderStyle.None

        DglMain.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        DglMain.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        DglMain.BackgroundColor = Me.BackColor
        DglMain.CellBorderStyle = DataGridViewCellBorderStyle.None
        AgCL.GridSetiingShowXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain, False)


        DglMain.Rows.Add(1)

        DglMain.Item(Col1Head, rowDataSyncFromDate).Value = hcDataSyncFromDate


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
        mQry = " Select Value From Status Where FieldName = '" & ClsMain.StatusFields.DataSyncedTillDate & "'"
        Dim bDataSyncedTillDate As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
        If bDataSyncedTillDate <> "" Then
            DglMain.Item(Col1Value, rowDataSyncFromDate).Value = ClsMain.FormatDate((DateAdd(DateInterval.Day, 1, CDate(bDataSyncedTillDate))))
        Else
            DglMain.Item(Col1Value, rowDataSyncFromDate).Value = ClsMain.FormatDate((CDate(AgL.PubStartDate)))
        End If
    End Sub
    Public Sub FProcSave()
        Dim mTrans As String = ""
        Dim DatabaseName As String = ""
        Dim IsValidDatabase As String = ""

        If AgL.XNull(DglMain.Item(Col1Value, rowDataSyncFromDate).Value) = "" Then
            MsgBox("Date is required.", MsgBoxStyle.Information)
            Exit Sub
        End If


        DatabaseName = Connection_ExternalDatabase.ConnectionString

        If DatabaseName.Contains("SHADHVINEW") Then
            IsValidDatabase = "Yes"
        End If

        If DatabaseName.Contains("SHADHVIJaunpur") Then
            IsValidDatabase = "Yes"
        End If

        If DatabaseName.Contains("SHADHVIKANPURB2") Then
            IsValidDatabase = "Yes"
        End If

        If DatabaseName.Contains("SHADHVINANDI") Then
            IsValidDatabase = "Yes"
        End If


        UpdateChildProgressBar("Initializing...", 1, 0)

        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
            If IsValidDatabase = "Yes" Then

            Else
                MsgBox("Wrong File.", MsgBoxStyle.Information)
                Exit Sub
            End If

        End If



        FGetDataExternal()
        FCheckSale()




        UpdateChildProgressBar(" ", 1, 0)
        UpdateParentProgressBar(" ", 1)
        MsgBox("Process Completed Successfully...", MsgBoxStyle.Information)
    End Sub
    Public Sub FCheckSale()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0

        mQry = "DELETE FROM TempToMatchItem"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM TempToMatchSubgroup"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM TempToMatchPurchInvoice"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM TempToMatchSaleInvoice"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM TempToMatchLedger"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



        For I = 0 To DtExternalData_Item.Rows.Count - 1
            mQry = "INSERT INTO TempToMatchItem (Code, ManualCode, Description, DisplayName, Specification, Unit, DealQty, DealUnit, ItemGroup, ItemCategory, ItemType, OmsId)
                    SELECT '" & AgL.XNull(DtExternalData_Item.Rows(I)("Code")) & "', '" & AgL.XNull(DtExternalData_Item.Rows(I)("ManualCode")) & "', '" & AgL.XNull(DtExternalData_Item.Rows(I)("Description")) & "', 
                    '" & AgL.XNull(DtExternalData_Item.Rows(I)("DisplayName")) & "','" & AgL.XNull(DtExternalData_Item.Rows(I)("Specification")) & "', '" & AgL.XNull(DtExternalData_Item.Rows(I)("Unit")) & "', '" & AgL.XNull(DtExternalData_Item.Rows(I)("DealQty")) & "', '" & AgL.XNull(DtExternalData_Item.Rows(I)("DealUnit")) & "', 
                    '" & AgL.XNull(DtExternalData_Item.Rows(I)("ItemGroup")) & "', '" & AgL.XNull(DtExternalData_Item.Rows(I)("ItemCategory")) & "', 
                    '" & AgL.XNull(DtExternalData_Item.Rows(I)("ItemType")) & "', '" & AgL.XNull(DtExternalData_Item.Rows(I)("OmsId")) & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next

        For I = 0 To DtExternalData_Subgroup.Rows.Count - 1
            mQry = "INSERT INTO TempToMatchSubgroup (GroupName, Subcode, ManualCode, NamePrefix, Name, DispName, OmsId)
                    SELECT '" & AgL.XNull(DtExternalData_Subgroup.Rows(I)("GroupName")) & "', '" & AgL.XNull(DtExternalData_Subgroup.Rows(I)("Subcode")) & "', '" & AgL.XNull(DtExternalData_Subgroup.Rows(I)("ManualCode")) & "', 
                    '" & AgL.XNull(DtExternalData_Subgroup.Rows(I)("NamePrefix")) & "','" & AgL.XNull(DtExternalData_Subgroup.Rows(I)("Name")) & "', '" & AgL.XNull(DtExternalData_Subgroup.Rows(I)("DispName")) & "', '" & AgL.XNull(DtExternalData_Subgroup.Rows(I)("OmsId")) & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next

        For I = 0 To DtExternalData_PurchInvoice.Rows.Count - 1
            mQry = "INSERT INTO TempToMatchPurchInvoice (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, VendorDocNo, VendorDocDate, OmsId, Sr, Item, Qty, Rate, MRP, Amount)
                    SELECT '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("DocID")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("V_Type")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("V_Prefix")) & "', 
                    '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("V_Date")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("V_No")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("Div_Code")) & "','" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("Site_Code")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("ManualRefNo")) & "', 
                    '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("VendorDocNo")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("VendorDocDate")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("OmsId")) & "', 
                    '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("Sr")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("Item")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("Qty")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("Rate")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("MRP")) & "', '" & AgL.XNull(DtExternalData_PurchInvoice.Rows(I)("Amount")) & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next

        For I = 0 To DtExternalData_SaleInvoice.Rows.Count - 1
            mQry = "INSERT INTO TempToMatchSaleInvoice (Export_Site_Code, DocID, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, BillToPartyName_Master, SaleToPartyName_Master, Qty, Amount, AmtDr, AmtCr)
                    SELECT '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("Export_Site_Code")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("DocID")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("V_Date")) & "', 
                    '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("V_No")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("Div_Code")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("Site_Code")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("ManualRefNo")) & "', 
                    '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("BillToPartyName_Master")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("SaleToPartyName_Master")) & "', 
                    '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("Qty")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("Amount")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("AmtDr")) & "', '" & AgL.XNull(DtExternalData_SaleInvoice.Rows(I)("AmtCr")) & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next

        For I = 0 To DtExternalData_LedgerHead.Rows.Count - 1
            mQry = "INSERT INTO TempToMatchLedger (Export_Site_Code, DivCode, DocID, V_Date, RecId, SubCode, AmtDr, AmtCr)
                    SELECT '" & AgL.XNull(DtExternalData_LedgerHead.Rows(I)("Export_Site_Code")) & "', '" & AgL.XNull(DtExternalData_LedgerHead.Rows(I)("DivCode")) & "', '" & AgL.XNull(DtExternalData_LedgerHead.Rows(I)("DocID")) & "', '" & AgL.XNull(DtExternalData_LedgerHead.Rows(I)("V_Date")) & "', 
                    '" & AgL.XNull(DtExternalData_LedgerHead.Rows(I)("RecId")) & "', '" & AgL.XNull(DtExternalData_LedgerHead.Rows(I)("SubCode")) & "', 
                    '" & AgL.XNull(DtExternalData_LedgerHead.Rows(I)("AmtDr")) & "', '" & AgL.XNull(DtExternalData_LedgerHead.Rows(I)("AmtCr")) & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next



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

            Call AgL.LogTableEntry("Data Match", Me.Text, "A", AgL.PubMachineName,
                AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), Conn, Cmd,
                mMessage, DglMain(Col1Value, rowDataSyncFromDate).Value,,,,
                AgL.PubSiteCode, AgL.PubDivCode, "", "", "")
        End If
    End Sub
    Private Function FGetCodeFromOMSId(Code As String, Site_Code As String, DtTable As DataTable, PrimaryKeyField As String) As String
        Dim DtRow As DataRow()
        If Site_Code <> "" Then
            If Code = "CASH" Or Code = "RevenuePnt" Then
                DtRow = DtTable.Select("OMSId = '" & Code & "' ")
            Else
                DtRow = DtTable.Select("OMSId = '" & Code & "' AND Site_Code = '" & Site_Code & "' ")
            End If
        Else
            DtRow = DtTable.Select("OMSId = '" & Code & "' ")
        End If

        If DtRow.Length > 0 Then
            FGetCodeFromOMSId = DtRow(0)(PrimaryKeyField)
        Else
            FGetCodeFromOMSId = ""
        End If
    End Function

    Private Sub DGL1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If e.RowIndex > -1 Then Dgl1.Rows(e.RowIndex).Selected = True
        Dgl1.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray
    End Sub
    Private Sub FGetDataExternal()
        Connection_ExternalDatabase.Open()

        mQry = " Select Code As Site_Code, Export_Site_Code From SiteMast "
        DtSiteMast = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mQry = " SELECT Div_Code, Export_Div_Code FROM Division "
        DtDivMast = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        For I As Integer = 0 To DtSiteMast.Rows.Count - 1
            If AgL.XNull(DtSiteMast.Rows(I)("Export_Site_Code")) = "" Then
                Err.Raise(1, "", "Export_Site_Code is blank in External Database.")
            End If
        Next

        For I As Integer = 0 To DtDivMast.Rows.Count - 1
            If AgL.XNull(DtDivMast.Rows(I)("Export_Div_Code")) = "" Then
                Err.Raise(1, "", "Export_Div_Code is blank in External Database.")
            End If
        Next

        mQry = "SELECT I.Code, I.ManualCode, I.Description, I.DisplayName, I.Specification, I.Unit, I.DealQty, I.DealUnit, I.ItemGroup, I.ItemCategory, I.ItemType, I.OmsId  
                FROM Item I "
        DtExternalData_Item = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mQry = "SELECT GroupName, SG.Subcode, SG.ManualCode, SG.NamePrefix, SG.Name, SG.DispName,SG.OmsId  
                FROM Subgroup SG
                LEFT JOIN AcGroup ON AcGroup.GroupCode = SG.GroupCode "
        DtExternalData_Subgroup = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ManualRefNo, H.VendorDocNo, H.VendorDocDate, H.OmsId,
                    L.Sr, L.ReferenceNo, L.Barcode, L.Item, L.Qty, L.Rate, L.MRP, L.Amount
                    FROM PurchInvoice H
                    LEFT JOIN PurchInvoiceDetail L ON L.DocID = H.DocID 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    Where Vt.NCat = Vt.NCat "
        mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
        DtExternalData_PurchInvoice = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mQry = " Select SM.Export_Site_Code, H.DocID, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ManualRefNo,  
                    Sg.Name As BillToPartyName_Master, Sg1.Name As SaleToPartyName_Master, SIL.Qty, SIL.Amount, L.AmtDr, L.AmtCr
                    From SaleInvoice H
                    LEFT Join SiteMast SM ON 1=1
                    LEFT JOIN 
                    (
                    SELECT SIL.DocID, Sum(SIL.Qty) Qty, Sum(SIL.Amount) Amount  FROM SaleInvoiceDetail SIL GROUP BY SIL.DocID 
                    ) SIL ON SIL.DocID = H.DocID   
                    LEFT JOIN 
                    (
                    SELECT L.DocId, Sum(L.AmtDr) AS AmtDr, Sum(L.AmtCr) AS AmtCr  FROM Ledger L GROUP BY L.DocId 
                    ) L On L.DocId = H.DocId
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
                    LEFT JOIN SubGroup Sg1 ON H.SaleToParty = Sg1.SubCode 
                    Where Vt.NCat = Vt.NCat "
        mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
        DtExternalData_SaleInvoice = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mQry = " SELECT Max(SM.Export_Site_Code) AS Export_Site_Code, Max(H.DivCode) as DivCode, H.DocId, H.SubCode, H.V_Date, Max(H.RecId) as RecId, Sum(H.AmtDr) AS AmtDr, Sum(H.AmtCr) AS AmtCr  
                 FROM Ledger H
                 LEFT Join SiteMast SM ON 1=1
                 Where 1=1 "
        mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & "
                GROUP BY H.DocId, H.V_Date, H.SubCode "
        DtExternalData_LedgerHead = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        Connection_ExternalDatabase.Close()
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
                Case rowDataSyncFromDate
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class

