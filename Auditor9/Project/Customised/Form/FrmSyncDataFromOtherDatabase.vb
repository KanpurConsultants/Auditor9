Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.FrmSaleInvoiceDirect_WithDimension
Public Class FrmSyncDataFromOtherDatabase
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_ExternalDatabase As New SQLite.SQLiteConnection
    Public mDbPath As String = ""
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker

    Dim DtCity As DataTable
    Dim DtArea As DataTable
    Dim DtCatalog As DataTable
    Dim DtItem As DataTable
    Dim DtAcGroup As DataTable
    Dim DtSubGroup As DataTable
    Dim DtSaleInvoice As DataTable
    Dim DtSaleInvoiceDetail As DataTable
    Dim DtPurchInvoice As DataTable
    Dim DtPurchInvoiceDetail As DataTable
    Dim DtLedgerHead As DataTable
    Dim DtLedgerHeadDetail As DataTable

    Dim bIsMastersImportedSuccessfully As Boolean = True
    Dim bIsSaleInvoicesImportedSuccessfully As Boolean = True
    Dim bIsPurchInvoicesImportedSuccessfully As Boolean = True

    Public Const Col1Head As String = "Head"
    Public Const Col1Status As String = "Status"
    Public Const Col1Message As String = "Message"

    Dim rowDataSyncFromDate As Integer = 0
    Public Const hcDataSyncFromDate As String = "Data Sync From Date"


    Dim DtSiteMast As DataTable
    Dim DtDivMast As DataTable
    Dim DtExternalData_SaleInvoice As New DataTable
    Dim DtExternalData_SaleReturn As New DataTable
    Dim DtExternalData_PurchInvoice As New DataTable
    Dim DtExternalData_PurchReturn As New DataTable
    Dim DtExternalData_LedgerHead As New DataTable
    Dim DtExternalData_Item As New DataTable
    Dim DtExternalData_Catalog As New DataTable
    Dim DtExternalData_SubGroup As New DataTable

    Dim mParentPrgBarMaxVal As Integer = 0

    Private IsApplicableImport_Item As Boolean = True
    Private IsApplicableImport_SubGroup As Boolean = True
    Private IsApplicableImport_Catalog As Boolean = True
    Private IsApplicableImport_SaleInvoice As Boolean = True
    Private IsApplicableImport_SaleReturn As Boolean = True
    Private IsApplicableImport_PurchInvoice As Boolean = True
    Private IsApplicableImport_PurchReturn As Boolean = True
    Private IsApplicableImport_LedgerHead As Boolean = True


    Private Delegate Sub UpdateChildProgressBarInvoker(ByVal Value As String, ChildPrMaxVal As Integer, ChildPrgValue As Integer)
    Private Delegate Sub UpdateParentProgressBarInvoker(ByVal Value As String, ParentPrMaxVal As Integer)
    Private Delegate Sub FRecordMessageInvoker(Head As String, Status As String, Message As String, Conn As Object, Cmd As Object)

    Private SadhviEnterprises_KanpurBranch As String = "SADHVI ENTERPRISES BRANCH"
    Private SadhviEmbroidery_KanpurBranch As String = "SADHVI EMBROIDERY BRANCH"

    Private SadhviEnterprises_KanpurBranch2 As String = "SADHVI ENTERPRISES BRANCH 2"
    Private SadhviEmbroidery_KanpurBranch2 As String = "SADHVI EMBROIDERY BRANCH 2"

    Private SadhviEnterprises_BhopalBranch As String = "SADHVI ENTERPRISES BHOPAL BRANCH"
    Private SadhviEmbroidery_BhopalBranch As String = "SADHVI EMBROIDERY BHOPAL BRANCH"

    Private SadhviEnterprises_JaunpurBranch As String = "SADHVI ENTERPRISES JAUNPUR BRANCH"
    Private SadhviEmbroidery_JaunpurBranch As String = "SADHVI EMBROIDERY JAUNPUR BRANCH"
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

        UpdateChildProgressBar("Initializing...", 1, 0)

        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
            If IsValidDatabase = "Yes" Then
                IsApplicableImport_Item = False
                IsApplicableImport_Catalog = False
                IsApplicableImport_SubGroup = True
                IsApplicableImport_SaleInvoice = True
                IsApplicableImport_SaleReturn = True
                IsApplicableImport_PurchInvoice = True
                IsApplicableImport_PurchReturn = True
                IsApplicableImport_LedgerHead = True
            Else
                MsgBox("Wrong File.", MsgBoxStyle.Information)
                Exit Sub
            End If

        End If

        If ClsMain.FDivisionNameForCustomization(13) = "JAIN BROTHERS" Or
                ClsMain.FDivisionNameForCustomization(11) = "BOOK SHOPEE" Then
            IsApplicableImport_Item = True
            IsApplicableImport_Catalog = True
            IsApplicableImport_SubGroup = True
            IsApplicableImport_SaleInvoice = True
            IsApplicableImport_SaleReturn = True
            IsApplicableImport_PurchInvoice = False
            IsApplicableImport_PurchReturn = False
            IsApplicableImport_LedgerHead = False
        End If


        FGetDataExternal()

        mQry = "SELECT Distinct L.Site_Code FROM Ledger L "
        Dim DtSiteList As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        For I As Integer = 0 To DtSiteList.Rows.Count - 1
            Dim mBranchLastLedgerDate As String = ""
            mQry = "SELECT Max(L.V_Date) AS MaxDate FROM Ledger L Where L.Site_Code = '" & AgL.XNull(DtSiteList.Rows(I)("Site_Code")) & "' "
            mBranchLastLedgerDate = AgL.XNull(AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0).Rows(0)("MaxDate"))

            Dim mHoLastLedgerDate As String = ""
            mQry = "SELECT Max(L.V_Date) AS MaxDate
                    FROM Ledger L
                    LEFT JOIN LedgerHead H ON H.DocID = L.DocId
                    LEFT JOIN PurchInvoice Pi ON L.DocId = Pi.DocID
                    LEFT JOIN SaleInvoice Si ON L.DocId = Si.DocID
                    WHERE L.Site_Code = '" & FGetExportSiteCodeFromSiteCode(AgL.XNull(DtSiteList.Rows(I)("Site_Code"))) & "'
                    AND IsNull(IsNULL(H.DocID,Pi.DocID),Si.DocID) IS NOT NULL "
            mHoLastLedgerDate = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())

            If mBranchLastLedgerDate <> "" And mHoLastLedgerDate <> "" Then
                If CDate(mHoLastLedgerDate) > CDate(mBranchLastLedgerDate) Then
                    MsgBox("Please check data you are importing. May be it is back dated.", MsgBoxStyle.Information)
                    Exit Sub
                End If
            End If
        Next

        FLoadCity()
        FLoadArea()
        FLoadAcGroup()

        FLoadSubGroup()
        If IsApplicableImport_SubGroup = True Then
            FUpdateSubGroup(DtExternalData_SubGroup)
            FAddSubGroup(DtExternalData_SubGroup)
            FLoadSubGroup()
        End If

        FLoadItem()
        If IsApplicableImport_Item = True Then
            FUpdateItem(DtExternalData_Item)
            FAddItem(DtExternalData_Item)
            FLoadItem()
        End If

        FLoadCatalog()
        If IsApplicableImport_Catalog = True Then
            FAddCatalog(DtExternalData_Catalog)
            FLoadCatalog()
        End If

        If bIsMastersImportedSuccessfully = False Then
            FRecordMessage("Completed", "Error", "Some masters are not synced successfully, that's why can't process transactions.", AgL.GCn, AgL.ECmd)
            UpdateChildProgressBar(" ", 1, 0)
            UpdateParentProgressBar(" ", 1)
            MsgBox("Process Completed With Error....", MsgBoxStyle.Information)
        End If

        If IsApplicableImport_SaleInvoice = True Then
            FLoadSale()
            FUpdateSale(DtExternalData_SaleInvoice)
            FAddSale(DtExternalData_SaleInvoice)
        End If

        If bIsSaleInvoicesImportedSuccessfully = False Then
            FRecordMessage("Completed", "Error", "Some Sale Invoice are not synced successfully, that's why can't process Sale Returns.", AgL.GCn, AgL.ECmd)
            UpdateChildProgressBar(" ", 1, 0)
            UpdateParentProgressBar(" ", 1)
            MsgBox("Process Completed With Error....", MsgBoxStyle.Information)
            Exit Sub
        End If

        If IsApplicableImport_SaleReturn = True Then
            FLoadSale()
            FUpdateSale(DtExternalData_SaleReturn)
            FAddSale(DtExternalData_SaleReturn)
            FDeleteSale(DtExternalData_SaleReturn, Ncat.SaleReturn)
        End If

        If IsApplicableImport_SaleInvoice = True Then
            FLoadSale()
            FDeleteSale(DtExternalData_SaleInvoice, Ncat.SaleInvoice)
        End If

        If IsApplicableImport_PurchInvoice = True Then
            FLoadPurch()
            FUpdatePurch(DtExternalData_PurchInvoice)
            FAddPurch(DtExternalData_PurchInvoice)
        End If

        If IsApplicableImport_PurchReturn = True Then
            FLoadPurch()
            FUpdatePurch(DtExternalData_PurchReturn)
            FAddPurch(DtExternalData_PurchReturn)
            FDeletePurch(DtExternalData_PurchReturn, Ncat.PurchaseReturn)
        End If

        If IsApplicableImport_PurchInvoice = True Then
            FLoadPurch()
            FDeletePurch(DtExternalData_PurchInvoice, Ncat.PurchaseInvoice)
        End If

        If IsApplicableImport_LedgerHead = True Then
            FLoadLedgerHead()
            FUpdateLedgerHead(DtExternalData_LedgerHead)
            FAddLedgerHead(DtExternalData_LedgerHead)
            FDeleteLedgerHead(DtExternalData_LedgerHead)
        End If

        Dim mCode As String = AgL.GetMaxId("Status", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        If AgL.VNull(AgL.Dman_Execute("Select count(*) From Status Where FieldName = '" & ClsMain.StatusFields.DataSyncedTillDate & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = " Insert Into Status(Code, FieldName, Value)
                Select '" & mCode & "', '" & ClsMain.StatusFields.DataSyncedTillDate & "', 
                " & AgL.Chk_Date(AgL.PubLoginDate) & " "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE Status Set Value = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                    Where FieldName = '" & ClsMain.StatusFields.DataSyncedTillDate & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
            FGetBranchItemRateForSadhvi()
        End If

        UpdateChildProgressBar(" ", 1, 0)
        UpdateParentProgressBar(" ", 1)
        MsgBox("Process Completed Successfully...", MsgBoxStyle.Information)
    End Sub
    Private Sub FLoadAcGroup()
        mQry = " Select * From AcGroup "
        DtAcGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadArea()
        mQry = " Select * From Area "
        DtArea = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadCatalog()
        mQry = " Select * From Catalog "
        DtCatalog = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadCity()
        mQry = " Select * From City "
        DtCity = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadSubGroup()
        mQry = " Select * From SubGroup "
        DtSubGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadItem()
        mQry = " Select * From Item "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadSale()
        Try
            If DtSaleInvoice IsNot Nothing Then DtSaleInvoice.Dispose()
            mQry = " Select H.* 
                From SaleInvoice H With (NoLock)
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type "
            DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtSaleInvoiceDetail IsNot Nothing Then DtSaleInvoiceDetail.Dispose()
            mQry = " Select L.* 
                From SaleInvoice H With (NoLock)
                LEFT JOIN SaleInvoiceDetail L With (NoLock) On H.DocId = L.DocId
                LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type 
                Where H.V_Date>='01-Apr-2019' and H.Site_Code='2' "
            DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FLoadPurch()
        mQry = " Select H.* 
                From PurchInvoice H With (NoLock)
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type "
        DtPurchInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select L.* 
                From PurchInvoice H With (NoLock)
                LEFT JOIN PurchInvoiceDetail L With (NoLock) On H.DocId = L.DocId
                LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type "
        DtPurchInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadLedgerHead()
        mQry = " Select * From LedgerHead With (NoLock) "
        DtLedgerHead = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select * From LedgerHeadDetail With (NoLock) "
        DtLedgerHeadDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Function FGetUpdateClause(DtExternalData As DataTable, RowIndexExternalData As Integer,
                                      DtLocalData As DataTable, RowIndexLocalData As Integer,
                                      FieldName As String, Optional DataType As String = "")
        If AgL.XNull(DtExternalData.Rows(RowIndexExternalData)(FieldName)) <> AgL.XNull(DtLocalData.Rows(RowIndexLocalData)(FieldName)) Then
            If DataType = "Date" Then
                FGetUpdateClause = FieldName + " = " & AgL.Chk_Date(AgL.XNull(DtExternalData.Rows(RowIndexExternalData)(FieldName))) & "" + ","
            ElseIf DataType = "Number" Then
                FGetUpdateClause = FieldName + " = " & AgL.VNull(DtExternalData.Rows(RowIndexExternalData)(FieldName)) & "" + ","
            Else
                FGetUpdateClause = FieldName + " = " & AgL.Chk_Text(AgL.XNull(DtExternalData.Rows(RowIndexExternalData)(FieldName))) & "" + ","
            End If
        Else
            FGetUpdateClause = ""
        End If
    End Function
    Private Function FGetUpdateClauseForCodes(DtExternalData As DataTable, RowIndexExternalData As Integer,
                                      DtLocalData As DataTable, RowIndexLocalData As Integer,
                                      FieldName As String, PrimaryKeyFieldName As String, DtTable As DataTable, TableName As String)

        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") And AgL.StrCmp(TableName, "Item") Then
            If AgL.XNull(DtExternalData.Rows(RowIndexExternalData)(FieldName)) <> AgL.XNull(DtLocalData.Rows(RowIndexLocalData)(FieldName)) Then
                FGetUpdateClauseForCodes = FieldName + " = " + AgL.Chk_Text(AgL.XNull(DtExternalData.Rows(RowIndexExternalData)(FieldName))) + ","
                Exit Function
            End If
        End If

        If AgL.XNull(DtExternalData.Rows(RowIndexExternalData)(FieldName)) <> AgL.XNull(DtLocalData.Rows(RowIndexLocalData)(FieldName + "OMSId")) Then
            Dim DtRow As DataRow() = DtTable.Select("OMSId = '" & AgL.XNull(DtExternalData.Rows(RowIndexExternalData)(FieldName)) & "'")
            If DtRow.Length > 0 Then
                FGetUpdateClauseForCodes = FieldName + " = " + AgL.Chk_Text(AgL.XNull(DtRow(0)(PrimaryKeyFieldName))) + ","
            Else
                FGetUpdateClauseForCodes = ""
            End If
        Else
            FGetUpdateClauseForCodes = ""
        End If
    End Function
    Public Sub FAddSubGroup(DtExternalData_Header As DataTable)
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0

        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        Dim bLastAcGroupCode As Integer = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(GroupCode AS INTEGER)),0) FROM AcGroup WHERE ABS(GroupCode)>0", AgL.GcnRead).ExecuteScalar)
        Dim DtAccountGroup = DtExternalData_Header.DefaultView.ToTable(True, "GroupCode", "GroupName")

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtAccountGroup.Rows.Count

        For I = 0 To DtAccountGroup.Rows.Count - 1
            If DtAcGroup.Select("OMSId = '" & AgL.XNull(DtAccountGroup.Rows(I)("GroupCode")) & "'").Length = 0 Then
                If AgL.XNull(DtAccountGroup.Rows(I)("GroupName")) <> "" Then
                    Dim AcGroupTable As New FrmPerson.StructAcGroup
                    Dim bAcGroupCode As String = (bLastAcGroupCode + (I + 1)).ToString.PadLeft(4).Replace(" ", "0")

                    AcGroupTable.GroupCode = bAcGroupCode
                    AcGroupTable.SNo = ""
                    AcGroupTable.GroupName = AgL.XNull(DtAccountGroup.Rows(I)("GroupName"))
                    AcGroupTable.ContraGroupName = AgL.XNull(DtAccountGroup.Rows(I)("GroupName"))
                    AcGroupTable.GroupUnder = ""
                    AcGroupTable.GroupNature = ""
                    AcGroupTable.Nature = ""
                    AcGroupTable.SysGroup = ""
                    AcGroupTable.LockText = "Synced From Other Database."
                    AcGroupTable.U_Name = AgL.PubUserName
                    AcGroupTable.U_EntDt = AgL.GetDateTime(AgL.GcnRead)
                    AcGroupTable.U_AE = "A"
                    AcGroupTable.OMSId = AgL.XNull(DtAccountGroup.Rows(I)("GroupCode"))

                    UpdateChildProgressBar("Inserting Account Group " + AcGroupTable.GroupName, mChildPrgMaxVal, mChildPrgCnt)

                    FrmPerson.ImportAcGroupTable(AcGroupTable)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                    mChildPrgCnt += 1
                End If
            End If
        Next



        Dim bLastCityCode As String = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        Dim DtCityToImport As DataTable = DtExternalData_Header.DefaultView.ToTable(True, "CityCode", "CityName", "State")

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtCityToImport.Rows.Count

        For I = 0 To DtCityToImport.Rows.Count - 1
            If DtCity.Select("OMSId = '" & AgL.XNull(DtCityToImport.Rows(I)("CityCode")) & "'").Length = 0 Then
                If AgL.XNull(DtCityToImport.Rows(I)("CityName")) <> "" Then
                    Dim CityTable As New FrmCity.StructCity
                    Dim bCityCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastCityCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    CityTable.CityCode = bCityCode
                    CityTable.CityName = AgL.XNull(DtCityToImport.Rows(I)("CityName"))
                    CityTable.State = AgL.XNull(DtCityToImport.Rows(I)("State"))
                    CityTable.EntryBy = AgL.PubUserName
                    CityTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    CityTable.EntryType = "A"
                    CityTable.EntryStatus = ""
                    CityTable.OMSId = AgL.XNull(DtCityToImport.Rows(I)("CityCode"))

                    UpdateChildProgressBar("Inserting Cities " + CityTable.CityName, mChildPrgMaxVal, mChildPrgCnt)

                    FrmCity.ImportCityTable(CityTable)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                    mChildPrgCnt += 1
                End If
            End If
        Next
        FLoadCity()


        Dim bLastAreaCode As String = AgL.GetMaxId("Area", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        Dim DtAreaToImport As DataTable = DtExternalData_Header.DefaultView.ToTable(True, "Area", "AreaName")

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtAreaToImport.Rows.Count

        For I = 0 To DtAreaToImport.Rows.Count - 1
            If DtArea.Select("OMSId = '" & AgL.XNull(DtAreaToImport.Rows(I)("Area")) & "'").Length = 0 Then
                If AgL.XNull(DtAreaToImport.Rows(I)("AreaName")) <> "" Then
                    Dim AreaTable As New FrmArea.StructArea
                    Dim bAreaCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastAreaCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    AreaTable.Code = bAreaCode
                    AreaTable.Description = AgL.XNull(DtAreaToImport.Rows(I)("AreaName"))
                    AreaTable.EntryBy = AgL.PubUserName
                    AreaTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    AreaTable.EntryType = "A"
                    AreaTable.EntryStatus = ""
                    AreaTable.OMSId = AgL.XNull(DtAreaToImport.Rows(I)("Area"))

                    UpdateChildProgressBar("Inserting Areas " + AreaTable.Description, mChildPrgMaxVal, mChildPrgCnt)

                    FrmArea.ImportAreaTable(AreaTable)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                    mChildPrgCnt += 1
                End If
            End If
        Next
        FLoadArea()


        Dim bLastSubCode As String = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        Dim ExportSiteCode As String = FGetExportSiteCodeFromSiteCode(AgL.XNull(DtExternalData_Header.Rows(0)("Site_Code")))

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count
        For I = 0 To DtExternalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Inserting Parties", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking " + AgL.XNull(DtExternalData_Header.Rows(I)("Name")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtSubGroup.Select("OMSId = '" & DtExternalData_Header.Rows(I)("SubCode") & "' AND Site_Code = '" & ExportSiteCode & "' ").Length = 0 Then
                Dim SubGroupTable As New FrmPerson.StructSubGroupTable
                Dim bSubCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastSubCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(8, "0")

                SubGroupTable.SubCode = bSubCode
                SubGroupTable.SubgroupType = AgL.XNull(DtExternalData_Header.Rows(I)("SubgroupType"))
                SubGroupTable.Site_Code = ExportSiteCode
                SubGroupTable.Name = AgL.XNull(DtExternalData_Header.Rows(I)("Name"))
                SubGroupTable.DispName = AgL.XNull(DtExternalData_Header.Rows(I)("DispName"))

                'If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
                '    If AgL.StrCmp(SubGroupTable.SubgroupType, SubgroupType.Customer) Or AgL.StrCmp(SubGroupTable.SubgroupType, SubgroupType.Supplier) Then
                '        SubGroupTable.Name = AgL.XNull(DtExternalData_Header.Rows(I)("Name")) + " (Branch)"
                '        If AgL.XNull(DtExternalData_Header.Rows(I)("DispName")) <> "" Then
                '            SubGroupTable.DispName = AgL.XNull(DtExternalData_Header.Rows(I)("DispName"))
                '        Else
                '            SubGroupTable.DispName = AgL.XNull(DtExternalData_Header.Rows(I)("Name"))
                '        End If
                '    End If
                'End If

                SubGroupTable.ManualCode = AgL.XNull(DtExternalData_Header.Rows(I)("ManualCode"))
                SubGroupTable.AccountGroup = AgL.XNull(DtExternalData_Header.Rows(I)("GroupName"))
                SubGroupTable.StateName = AgL.XNull(DtExternalData_Header.Rows(I)("StateName"))
                SubGroupTable.AgentName = ""
                SubGroupTable.TransporterName = ""
                SubGroupTable.AreaCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("Area")), "", DtArea, "Code")
                SubGroupTable.AreaName = ""
                SubGroupTable.CityCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("CityCode")), "", DtCity, "CityCode")
                SubGroupTable.CityName = AgL.XNull(DtExternalData_Header.Rows(I)("CityName"))
                SubGroupTable.GroupCode = AgL.XNull(DtExternalData_Header.Rows(I)("GroupCode"))
                SubGroupTable.GroupNature = AgL.XNull(DtExternalData_Header.Rows(I)("GroupNature"))
                SubGroupTable.Nature = AgL.XNull(DtExternalData_Header.Rows(I)("Nature"))
                SubGroupTable.Address = AgL.XNull(DtExternalData_Header.Rows(I)("Address"))
                SubGroupTable.PIN = AgL.XNull(DtExternalData_Header.Rows(I)("PIN"))
                SubGroupTable.Phone = AgL.XNull(DtExternalData_Header.Rows(I)("Phone"))
                SubGroupTable.ContactPerson = AgL.XNull(DtExternalData_Header.Rows(I)("ContactPerson"))
                SubGroupTable.Mobile = AgL.XNull(DtExternalData_Header.Rows(I)("Mobile"))
                SubGroupTable.CreditDays = AgL.XNull(DtExternalData_Header.Rows(I)("CreditDays"))
                SubGroupTable.CreditLimit = AgL.XNull(DtExternalData_Header.Rows(I)("CreditLimit"))
                SubGroupTable.EMail = AgL.XNull(DtExternalData_Header.Rows(I)("EMail"))
                SubGroupTable.ParentCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("Parent")), ExportSiteCode, DtSubGroup, "SubCode")
                SubGroupTable.SalesTaxPostingGroup = AgL.XNull(DtExternalData_Header.Rows(I)("SalesTaxPostingGroup"))
                SubGroupTable.EntryBy = AgL.XNull(DtExternalData_Header.Rows(I)("EntryBy"))
                SubGroupTable.EntryDate = AgL.XNull(DtExternalData_Header.Rows(I)("EntryDate"))
                SubGroupTable.EntryType = "Add"
                SubGroupTable.EntryStatus = LogStatus.LogOpen
                SubGroupTable.Div_Code = AgL.PubDivCode
                SubGroupTable.Status = "Active"
                SubGroupTable.SalesTaxNo = AgL.XNull(DtExternalData_Header.Rows(I)("SalesTaxNo"))
                SubGroupTable.PANNo = AgL.XNull(DtExternalData_Header.Rows(I)("PANNo"))
                SubGroupTable.AadharNo = AgL.XNull(DtExternalData_Header.Rows(I)("AadharNo"))
                SubGroupTable.OMSId = AgL.XNull(DtExternalData_Header.Rows(I)("SubCode"))
                SubGroupTable.LockText = "Synced From Other Database."
                SubGroupTable.Cnt = I

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    UpdateChildProgressBar("Inserting Party " + SubGroupTable.Name, mChildPrgMaxVal, mChildPrgCnt)
                    FrmPerson.ImportSubgroupTable(SubGroupTable)
                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
            mChildPrgCnt += 1
        Next
    End Sub
    Private Sub FUpdateSubGroup(DtExternalData_Header As DataTable)
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0


        Dim DtFieldList_Header As DataTable

        Dim bExternalDocIdStr As String = ""
        Dim ExportSiteCode As String = FGetExportSiteCodeFromSiteCode(AgL.XNull(DtExternalData_Header.Rows(0)("Site_Code")))
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            If bExternalDocIdStr <> "" Then bExternalDocIdStr += ","
            bExternalDocIdStr += AgL.Chk_Text(AgL.XNull(DtExternalData_Header.Rows(I)("SubCode")))
        Next

        mQry = " Select Psg.OMSId As ParentOMSId, C.OMSId As CityCodeOMSId, A.OMSId As AreaOMSId, Sg.* 
                From SubGroup Sg
                LEFT JOIN SubGroup PSg On Sg.Parent = PSg.SubCode
                LEFT JOIN City C ON Sg.CityCode = C.CityCode
                LEFT JOIN Area A On Sg.Area = A.Code
                Where Sg.OMSId In (" & bExternalDocIdStr & ") AND Sg.Site_Code =  '" & ExportSiteCode & "'"
        Dim DtHeaderLocal As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
        mQry = "PRAGMA table_info(SubGroup);"
        DtFieldList_Header = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Updating Parties", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking " + AgL.XNull(DtExternalData_Header.Rows(I)("Name")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            For J As Integer = 0 To DtHeaderLocal.Rows.Count - 1
                If AgL.XNull(DtExternalData_Header.Rows(I)("SubCode")) = AgL.XNull(DtHeaderLocal.Rows(J)("OMSId")) Then
                    bUpdateClauseQry = ""
                    For F As Integer = 0 To DtFieldList_Header.Rows.Count - 1
                        If DtFieldList_Header.Rows(F)("Name") = "SubCode" Or
                            DtFieldList_Header.Rows(F)("Name") = "GroupCode" Or
                            DtFieldList_Header.Rows(F)("Name") = "GroupNature" Or
                            DtFieldList_Header.Rows(F)("Name") = "Nature" Or
                            DtFieldList_Header.Rows(F)("Name") = "Site_Code" Or
                            DtFieldList_Header.Rows(F)("Name") = "LockText" Or
                            DtFieldList_Header.Rows(F)("Name") = "OMSId" Then
                            'Do Nothing
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "CityCode" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtHeaderLocal, J, DtFieldList_Header.Rows(F)("Name"), "CityCode", DtCity, "City")
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "Area" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtHeaderLocal, J, DtFieldList_Header.Rows(F)("Name"), "Code", DtArea, "Area")
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "Parent" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtHeaderLocal, J, DtFieldList_Header.Rows(F)("Name"), "SubCode", DtSubGroup, "SubGroup")
                        Else
                            bUpdateClauseQry += FGetUpdateClause(DtExternalData_Header, I, DtHeaderLocal, J, DtFieldList_Header.Rows(F)("Name"), DtFieldList_Header.Rows(F)("Type"))
                        End If
                    Next

                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"


                        If bUpdateClauseQry <> "" Then
                            UpdateChildProgressBar("Updating Party " + AgL.XNull(DtExternalData_Header.Rows(I)("Name")), mChildPrgMaxVal, mChildPrgCnt)
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE SubGroup  Set " + bUpdateClauseQry + " Where SubCode = '" & AgL.XNull(DtHeaderLocal.Rows(J)("SubCode")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                        End If


                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                        AgL.ETrans.Rollback()
                    End Try
                End If
            Next
            mChildPrgCnt += 1
        Next
    End Sub
    Public Sub FAddItem(DtExternalData_Header As DataTable)
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0


        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer


        Dim bLastItemCategoryCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        Dim DtItemCategory = DtExternalData_Header.DefaultView.ToTable(True, "ItemCategoryDesc")

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtItemCategory.Rows.Count

        For I = 0 To DtItemCategory.Rows.Count - 1
            If AgL.XNull(DtItemCategory.Rows(I)("ItemCategoryDesc")) <> "" Then
                Dim ItemCategoryTable As New FrmItemMaster.StructItemCategory
                Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemCategoryTable.Code = bItemCategoryCode
                ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)("ItemCategoryDesc"))
                ItemCategoryTable.ItemType = ItemTypeCode.TradingProduct
                ItemCategoryTable.SalesTaxPostingGroup = "GST 0%"
                ItemCategoryTable.Unit = "Nos"
                ItemCategoryTable.EntryBy = AgL.PubUserName
                ItemCategoryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemCategoryTable.EntryType = "Add"
                ItemCategoryTable.LockText = "Synced From Other Database."
                ItemCategoryTable.EntryStatus = LogStatus.LogOpen
                ItemCategoryTable.Div_Code = AgL.PubDivCode
                ItemCategoryTable.Status = "Active"

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmItemMaster.ImportItemCategoryTable(ItemCategoryTable)

                    UpdateChildProgressBar("Inserting Item Category " + ItemCategoryTable.Description, mChildPrgMaxVal, mChildPrgCnt)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                    mChildPrgCnt += 1

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next



        Dim bLastItemGroupCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        Dim DtItemGroup = DtExternalData_Header.DefaultView.ToTable(True, "ItemGroupDesc", "ItemCategoryDesc")

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtItemGroup.Rows.Count

        For I = 0 To DtItemGroup.Rows.Count - 1
            If AgL.XNull(DtItemGroup.Rows(I)("ItemGroupDesc")) <> "" Then
                Dim ItemGroupTable As New FrmItemMaster.StructItemGroup
                Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemGroupTable.Code = bItemGroupCode
                ItemGroupTable.Description = AgL.XNull(DtItemGroup.Rows(I)("ItemGroupDesc"))
                ItemGroupTable.ItemCategory = AgL.XNull(DtItemGroup.Rows(I)("ItemCategoryDesc"))
                ItemGroupTable.ItemType = ItemTypeCode.TradingProduct
                ItemGroupTable.SalesTaxPostingGroup = "GST 0%"
                ItemGroupTable.Unit = "Nos"
                ItemGroupTable.EntryBy = AgL.PubUserName
                ItemGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemGroupTable.EntryType = "Add"
                ItemGroupTable.LockText = "Synced From Other Database."
                ItemGroupTable.EntryStatus = LogStatus.LogOpen
                ItemGroupTable.Div_Code = AgL.PubDivCode
                ItemGroupTable.Status = "Active"

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmItemMaster.ImportItemGroupTable(ItemGroupTable)

                    UpdateChildProgressBar("Inserting Item Group " + ItemGroupTable.Description, mChildPrgMaxVal, mChildPrgCnt)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                    mChildPrgCnt += 1

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count

        Dim bLastItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        For I = 0 To DtExternalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Inserting Items", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking " + AgL.XNull(DtExternalData_Header.Rows(I)("Description")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtItem.Select("OMSId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("Code")) & "'").Length = 0 Then
                Dim ItemTable As New FrmItemMaster.StructItem
                Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemTable.Code = bItemCode
                ItemTable.ManualCode = AgL.XNull(DtExternalData_Header.Rows(I)("ManualCode"))
                ItemTable.Description = AgL.XNull(DtExternalData_Header.Rows(I)("Description"))
                ItemTable.DisplayName = AgL.XNull(DtExternalData_Header.Rows(I)("DisplayName"))
                ItemTable.Specification = AgL.XNull(DtExternalData_Header.Rows(I)("Specification"))
                ItemTable.ItemGroupDesc = AgL.XNull(DtExternalData_Header.Rows(I)("ItemGroupDesc"))
                ItemTable.ItemCategoryDesc = AgL.XNull(DtExternalData_Header.Rows(I)("ItemCategoryDesc"))
                ItemTable.ItemType = AgL.XNull(DtExternalData_Header.Rows(I)("ItemType"))
                ItemTable.V_Type = AgL.XNull(DtExternalData_Header.Rows(I)("V_Type"))
                ItemTable.Unit = AgL.XNull(DtExternalData_Header.Rows(I)("Unit"))
                ItemTable.PurchaseRate = AgL.XNull(DtExternalData_Header.Rows(I)("PurchaseRate"))
                ItemTable.Rate = AgL.XNull(DtExternalData_Header.Rows(I)("Rate"))
                ItemTable.SalesTaxPostingGroup = AgL.XNull(DtExternalData_Header.Rows(I)("SalesTaxPostingGroup"))
                ItemTable.HSN = AgL.XNull(DtExternalData_Header.Rows(I)("HSN"))
                ItemTable.EntryBy = AgL.PubUserName
                ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemTable.EntryType = "Add"
                ItemTable.EntryStatus = LogStatus.LogOpen
                ItemTable.Div_Code = AgL.PubDivCode
                ItemTable.Status = "Active"
                ItemTable.LockText = "Synced From Other Database."
                ItemTable.OMSId = AgL.XNull(DtExternalData_Header.Rows(I)("Code"))
                ItemTable.StockYN = 1
                ItemTable.IsSystemDefine = 0


                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmItemMaster.ImportItemTable(ItemTable)

                    UpdateChildProgressBar("Inserting Item " + ItemTable.Description, mChildPrgMaxVal, mChildPrgCnt)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
            mChildPrgCnt += 1
        Next
    End Sub
    Private Sub FUpdateItem(DtExternalData_Header As DataTable)
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0


        Dim DtFieldList_Header As DataTable


        Dim bExternalDocIdStr As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            If bExternalDocIdStr <> "" Then bExternalDocIdStr += ","
            bExternalDocIdStr += AgL.Chk_Text(AgL.XNull(DtExternalData_Header.Rows(I)("Code")))
        Next

        mQry = " Select Ig.OMSId As ItemGroupOMSId, Ic.OMSId As ItemCategoryOMSId, I.* 
                From Item I
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where I.OMSId In (" & bExternalDocIdStr & ") "
        Dim DtHeaderLocal As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
        mQry = "PRAGMA table_info(Item);"
        DtFieldList_Header = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)


        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Updating Items", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking " + AgL.XNull(DtExternalData_Header.Rows(I)("Description")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            For J As Integer = 0 To DtHeaderLocal.Rows.Count - 1
                If AgL.XNull(DtExternalData_Header.Rows(I)("Code")) = AgL.XNull(DtHeaderLocal.Rows(J)("OMSId")) Then
                    bUpdateClauseQry = ""
                    For F As Integer = 0 To DtFieldList_Header.Rows.Count - 1
                        If DtFieldList_Header.Rows(F)("Name") = "SubCode" Or DtFieldList_Header.Rows(F)("Name") = "OMSId" Then
                            'Do Nothing
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "ItemCategory" Or DtFieldList_Header.Rows(F)("Name") = "ItemGroup" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtHeaderLocal, J, DtFieldList_Header.Rows(F)("Name"), "Code", DtItem, "Item")
                        Else
                            bUpdateClauseQry += FGetUpdateClause(DtExternalData_Header, I, DtHeaderLocal, J, DtFieldList_Header.Rows(F)("Name"), DtFieldList_Header.Rows(F)("Type"))
                        End If
                    Next


                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        If bUpdateClauseQry <> "" Then
                            UpdateChildProgressBar("Updating Item " & AgL.XNull(DtExternalData_Header.Rows(I)("Description")), mChildPrgMaxVal, mChildPrgCnt)
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE Item Set " + bUpdateClauseQry + " Where Code = '" & AgL.XNull(DtHeaderLocal.Rows(J)("Code")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                        End If


                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                        AgL.ETrans.Rollback()
                    End Try
                End If
            Next
            mChildPrgCnt += 1
        Next
    End Sub
    Private Sub FUpdateSale(DtExternalData_Header As DataTable)
        Dim mEntryChanged As Boolean = False

        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0


        Dim DtFieldList_Header As DataTable
        Dim DtFieldList_Line As DataTable

        Dim DtExternalData_Line As DataTable

        Dim DtLocalData_Header As DataTable
        Dim DtLocalData_Line As DataTable
        Dim ExportSiteCode As String
        If DtExternalData_Header.Rows.Count > 0 Then
            ExportSiteCode = FGetExportSiteCodeFromSiteCode(AgL.XNull(DtExternalData_Header.Rows(0)("Site_Code")))
        End If
        Dim bEntryType As String = ""
        If DtExternalData_Header.Rows.Count > 0 Then
            mQry = " Select NCat From Voucher_Type Where V_Type = '" & AgL.XNull(DtExternalData_Header.Rows(0)("V_Type")) & "'"
            Dim bNCat As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
            If bNCat = Ncat.SaleInvoice Then
                bEntryType = " Invoice "
            ElseIf bNCat = Ncat.SaleReturn Then
                bEntryType = " Return "
            ElseIf bNCat = Ncat.SaleOrder Then
                bEntryType = " Order "
            End If
        End If


        Dim bExternalDocIdStr As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            If bExternalDocIdStr <> "" Then bExternalDocIdStr += ","
            bExternalDocIdStr += AgL.Chk_Text(AgL.XNull(DtExternalData_Header.Rows(I)("DocId")))
        Next

        If bExternalDocIdStr = "" Then Exit Sub

        mQry = " Select Sg.OMSId As SaleToPartyOMSId, BSg.OMSId As BillToPartyOMSId, 
                C.OMSID As SaleToPartyCityOMSId, H.* 
                From SaleInvoice H 
                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                LEFT JOIN SubGroup BSg On H.BillToParty = BSg.SubCode
                LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                Where H.OMSId In (" & bExternalDocIdStr & ")  AND H.Site_Code =  '" & ExportSiteCode & "'"
        DtLocalData_Header = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
        mQry = "PRAGMA table_info(SaleInvoice);"
        DtFieldList_Header = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            mEntryChanged = False
            UpdateParentProgressBar("Updating Sale" & bEntryType, mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking Sale" & bEntryType + AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) + "-" + AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            For J As Integer = 0 To DtLocalData_Header.Rows.Count - 1
                If AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) = AgL.XNull(DtLocalData_Header.Rows(J)("OMSId")) And ExportSiteCode = AgL.XNull(DtLocalData_Header.Rows(J)("Site_Code")) Then
                    bUpdateClauseQry = ""
                    For F As Integer = 0 To DtFieldList_Header.Rows.Count - 1
                        If DtFieldList_Header.Rows(F)("Name") = "DocId" Or
                            DtFieldList_Header.Rows(F)("Name") = "V_No" Or
                            DtFieldList_Header.Rows(F)("Name") = "Site_Code" Or
                            DtFieldList_Header.Rows(F)("Name") = "Div_Code" Or
                            DtFieldList_Header.Rows(F)("Name") = "LockText" Or
                            DtFieldList_Header.Rows(F)("Name") = "ApproveBy" Or
                            DtFieldList_Header.Rows(F)("Name") = "ApproveDate" Or
                            DtFieldList_Header.Rows(F)("Name") = "MoveToLog" Or
                            DtFieldList_Header.Rows(F)("Name") = "MoveToLogDate" Or
                            DtFieldList_Header.Rows(F)("Name") = "OMSId" Then
                            'Do Nothing
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "SaleToParty" Or DtFieldList_Header.Rows(F)("Name") = "BillToParty" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), "SubCode", DtSubGroup, "SubGroup")
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "SaleToPartyCity" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), "CityCode", DtCity, "City")
                        Else
                            bUpdateClauseQry += FGetUpdateClause(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), DtFieldList_Header.Rows(F)("Type"))
                        End If
                    Next

                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE SaleInvoice Set " + bUpdateClauseQry + " Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            mEntryChanged = True
                        End If




                        'For Line Logic
                        mQry = "Select * From SaleInvoiceDetail Where DocId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                        DtExternalData_Line = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
                        mQry = "Select * From SaleInvoiceDetail Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                        DtLocalData_Line = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

                        For K As Integer = 0 To DtLocalData_Line.Rows.Count - 1
                            If DtExternalData_Line.Select(" DocId + Sr = '" + AgL.XNull(DtLocalData_Line.Rows(K)("OMSId")) + "'").Length = 0 Then
                                mQry = " Delete From SaleInvoiceDetail Where OMSId = '" & AgL.XNull(DtLocalData_Line.Rows(K)("OMSId")) & "' AND DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "' "
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next

                        For K As Integer = 0 To DtExternalData_Line.Rows.Count - 1
                            If DtLocalData_Line.Select(" OMSId = '" + AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) + "'").Length = 0 Then
                                mQry = " Insert Into SaleInvoiceDetail(DocId, Sr, SaleInvoice, SaleInvoiceSr, OMSId)
                                        Select '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "', 
                                        " & AgL.VNull(DtExternalData_Line.Rows(K)("Sr")) & ", 
                                        '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "', 
                                        " & AgL.VNull(DtExternalData_Line.Rows(K)("Sr")) & ", 
                                        '" & AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) & "'"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next

                        mQry = "Select I.OMSId As ItemOMSId, Ist.OMSId As ItemStateOMSId, C.OMSId As CatalogOMSId, 
                                G.OMSId As GodownOMSId, SRep.OMSId As SalesRepresentativeOMSId,  L.* 
                                From SaleInvoiceDetail L With (NoLock) 
                                LEFT JOIN Item I ON L.Item = I.Code
                                LEFT JOIN Item Ist ON L.ItemState = Ist.Code
                                LEFT JOIN Catalog C On L.Catalog = C.Code
                                LEFT JOIN SubGroup G On L.Godown = G.SubCode
                                LEFT JOIN SubGroup SRep On L.SalesRepresentative = SRep.SubCode
                                Where L.DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                        DtLocalData_Line = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
                        mQry = "PRAGMA table_info(SaleInvoiceDetail);"
                        DtFieldList_Line = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)


                        For K As Integer = 0 To DtExternalData_Line.Rows.Count - 1
                            For L As Integer = 0 To DtLocalData_Line.Rows.Count - 1
                                If AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) = AgL.XNull(DtLocalData_Line.Rows(L)("OMSId")) Then
                                    bUpdateClauseQry = ""
                                    For F As Integer = 0 To DtFieldList_Line.Rows.Count - 1
                                        If DtFieldList_Line.Rows(F)("Name") = "DocId" Or DtFieldList_Line.Rows(F)("Name") = "Sr" Or
                                            DtFieldList_Line.Rows(F)("Name") = "SaleInvoice" Or DtFieldList_Line.Rows(F)("Name") = "SaleInvoiceSr" Or DtFieldList_Line.Rows(F)("Name") = "OMSId" Then
                                            'Do Nothing
                                        ElseIf DtFieldList_Line.Rows(F)("Name") = "Item" Or DtFieldList_Line.Rows(F)("Name") = "ItemState" Then
                                            'bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), "Code", DtItem)
                                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), "Code", DtItem, "Item")
                                        ElseIf DtFieldList_Line.Rows(F)("Name") = "Godown" Or DtFieldList_Line.Rows(F)("Name") = "SalesRepresentative" Then
                                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), "SubCode", DtSubGroup, "SubGroup")
                                        ElseIf DtFieldList_Line.Rows(F)("Name") = "Catalog" Then
                                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), "Code", DtCatalog, "Catalog")
                                        Else
                                            bUpdateClauseQry += FGetUpdateClause(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), DtFieldList_Line.Rows(F)("Type"))
                                        End If
                                    Next

                                    If bUpdateClauseQry <> "" Then
                                        bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                                        mQry = " UPDATE SaleInvoiceDetail Set " + bUpdateClauseQry +
                                                    " Where DocId = '" & AgL.XNull(DtLocalData_Line.Rows(L)("DocId")) & "'
                                                    And Sr = " & AgL.XNull(DtLocalData_Line.Rows(L)("Sr")) & ""
                                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                                        mEntryChanged = True
                                    End If
                                End If
                            Next
                        Next

                        If mEntryChanged = True Then
                            mQry = " Delete From StockAdj Where StockOutDocID = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Delete From StockAdj Where StockInDocID = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                SubCode, SalesTaxGroupParty,  Item,  LotNo, 
                                EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                ReferenceDocID, ReferenceDocIDSr, Rate, Amount, Landed_Value) 
                                Select L.DocId, L.Sr, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, 
                                H.Div_Code, H.Site_Code, H.SaleToParty,  H.SalesTaxGroupParty,  L.Item,
                                L.LotNo, 'I', 
                                Case When  IfNull(L.Qty,0) >= 0 Then L.Qty Else 0 End As Qty_Iss, 
                                Case When  IfNull(L.Qty,0) < 0 Then IfNull(Abs(L.Qty),0) Else 0 End As Qty_Rec, 
                                L.Unit, L.UnitMultiplier, 
                                Case When  IfNull(L.DealQty,0) >= 0 Then L.DealQty Else 0 End As DealQty_Iss, 
                                Case When  IfNull(L.DealQty,0) < 0 Then IfNull(Abs(L.DealQty),0) Else 0 End As DealQty_Rec, 
                                L.DealUnit,  
                                L.ReferenceDocId, L.ReferenceDocIdSr, 
                                L.Amount/(Case When IfNull(L.Qty,0) = 0 Then 1 Else L.Qty End), L.Amount, L.Amount
                                FROM SaleInvoiceDetail L    
                                LEFT JOIN SaleInvoice H On L.DocId = H.DocId 
                                WHERE L.DocId =  '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "' "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            FrmSaleInvoiceDirect_WithDimension.FGetCalculationData(AgL.XNull(DtLocalData_Header.Rows(J)("DocId")), AgL.GCn, AgL.ECmd)

                            UpdateChildProgressBar("Updating Sale" & bEntryType & AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) & "-" & AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")), mChildPrgMaxVal, mChildPrgCnt)
                            FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                        End If

                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                        AgL.ETrans.Rollback()
                    End Try
                End If
            Next
            mChildPrgCnt += 1
        Next
    End Sub
    Public Sub FAddSale(DtExternalData_Header As DataTable)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim DtExternalData_Line As DataTable
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0

        Dim ExportSiteCode As String = FGetExportSiteCodeFromSiteCode(AgL.XNull(DtExternalData_Header.Rows(0)("Site_Code")))

        Dim bEntryType As String = ""
        If DtExternalData_Header.Rows.Count > 0 Then
            mQry = " Select NCat From Voucher_Type Where V_Type = '" & AgL.XNull(DtExternalData_Header.Rows(0)("V_Type")) & "'"
            Dim bNCat As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
            If bNCat = Ncat.SaleInvoice Then
                bEntryType = " Invoice "
            ElseIf bNCat = Ncat.SaleReturn Then
                bEntryType = " Return "
            ElseIf bNCat = Ncat.SaleOrder Then
                bEntryType = " Order "
            End If
        End If



        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count
        For I = 0 To DtExternalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Inserting Sale" & bEntryType, mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking Sale" & bEntryType & AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) & "-" & AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")) & " exist or not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtSaleInvoice.Select("OMSId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "' AND Site_Code = '" & ExportSiteCode & "' ").Length = 0 Then
                Dim SaleInvoiceTableList(0) As FrmSaleInvoiceDirect_WithDimension.StructSaleInvoice
                Dim SaleInvoiceTable As New FrmSaleInvoiceDirect_WithDimension.StructSaleInvoice

                SaleInvoiceTable.DocID = ""
                SaleInvoiceTable.V_Type = AgL.XNull(DtExternalData_Header.Rows(I)("V_Type"))
                SaleInvoiceTable.V_Prefix = AgL.XNull(DtExternalData_Header.Rows(I)("V_Prefix"))
                SaleInvoiceTable.Site_Code = ExportSiteCode
                SaleInvoiceTable.Div_Code = AgL.XNull(DtExternalData_Header.Rows(I)("Div_Code"))
                SaleInvoiceTable.V_No = 0
                SaleInvoiceTable.V_Date = AgL.XNull(DtExternalData_Header.Rows(I)("V_Date"))
                SaleInvoiceTable.ManualRefNo = AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo"))
                SaleInvoiceTable.SaleToParty = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("SaleToParty")), ExportSiteCode, DtSubGroup, "SubCode")
                SaleInvoiceTable.SaleToPartyName = AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartyName"))
                SaleInvoiceTable.AgentCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("Agent")), ExportSiteCode, DtSubGroup, "SubCode")
                SaleInvoiceTable.AgentName = ""
                SaleInvoiceTable.BillToPartyCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("BillToParty")), ExportSiteCode, DtSubGroup, "SubCode")
                SaleInvoiceTable.BillToPartyName = AgL.XNull(DtExternalData_Header.Rows(I)("BillToPartyName"))
                SaleInvoiceTable.SaleToPartyAddress = AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartyAddress"))
                SaleInvoiceTable.SaleToPartyPinCode = AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartyPinCode"))
                SaleInvoiceTable.SaleToPartyCityCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartyCity")), "", DtCity, "CityCode")
                SaleInvoiceTable.SaleToPartyState = AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartyState"))
                SaleInvoiceTable.SaleToPartyMobile = AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartyMobile"))
                SaleInvoiceTable.SaleToPartySalesTaxNo = AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartySalesTaxNo"))
                SaleInvoiceTable.ShipToAddress = AgL.XNull(DtExternalData_Header.Rows(I)("ShipToAddress"))
                SaleInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtExternalData_Header.Rows(I)("SalesTaxGroupParty"))
                SaleInvoiceTable.PlaceOfSupply = AgL.XNull(DtExternalData_Header.Rows(I)("PlaceOfSupply"))
                SaleInvoiceTable.StructureCode = AgL.XNull(DtExternalData_Header.Rows(I)("Structure"))
                SaleInvoiceTable.CustomFields = AgL.XNull(DtExternalData_Header.Rows(I)("CustomFields"))
                SaleInvoiceTable.SaleToPartyDocNo = AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartyDocNo"))
                SaleInvoiceTable.SaleToPartyDocDate = AgL.XNull(DtExternalData_Header.Rows(I)("SaleToPartyDocDate"))
                SaleInvoiceTable.ReferenceDocId = ""
                SaleInvoiceTable.Tags = AgL.XNull(DtExternalData_Header.Rows(I)("Tags"))
                SaleInvoiceTable.Remarks = AgL.XNull(DtExternalData_Header.Rows(I)("Remarks"))
                SaleInvoiceTable.TermsAndConditions = AgL.XNull(DtExternalData_Header.Rows(I)("TermsAndConditions"))
                SaleInvoiceTable.Status = "Active"
                SaleInvoiceTable.EntryBy = AgL.XNull(DtExternalData_Header.Rows(I)("EntryBy"))
                SaleInvoiceTable.EntryDate = AgL.XNull(DtExternalData_Header.Rows(I)("EntryDate"))
                SaleInvoiceTable.ApproveBy = AgL.PubUserName
                SaleInvoiceTable.ApproveDate = AgL.GetDateTime(AgL.GcnRead)
                SaleInvoiceTable.MoveToLog = ""
                SaleInvoiceTable.MoveToLogDate = ""
                SaleInvoiceTable.UploadDate = ""
                SaleInvoiceTable.OmsId = AgL.XNull(DtExternalData_Header.Rows(I)("DocId"))
                SaleInvoiceTable.LockText = "Synced From Other Database."

                SaleInvoiceTable.Gross_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Gross_Amount"))
                SaleInvoiceTable.SpecialDiscount_Per = AgL.VNull(DtExternalData_Header.Rows(I)("SpecialDiscount_Per"))
                SaleInvoiceTable.SpecialDiscount = AgL.VNull(DtExternalData_Header.Rows(I)("SpecialDiscount"))
                SaleInvoiceTable.SpecialAddition_Per = AgL.VNull(DtExternalData_Header.Rows(I)("SpecialAddition_Per"))
                SaleInvoiceTable.SpecialAddition = AgL.VNull(DtExternalData_Header.Rows(I)("SpecialAddition"))
                SaleInvoiceTable.Taxable_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Taxable_Amount"))
                SaleInvoiceTable.Tax1 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax1"))
                SaleInvoiceTable.Tax2 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax2"))
                SaleInvoiceTable.Tax3 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax3"))
                SaleInvoiceTable.Tax4 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax4"))
                SaleInvoiceTable.Tax5 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax5"))
                SaleInvoiceTable.SubTotal1 = AgL.VNull(DtExternalData_Header.Rows(I)("SubTotal1"))
                SaleInvoiceTable.Other_Charge = AgL.VNull(DtExternalData_Header.Rows(I)("Other_Charge"))
                SaleInvoiceTable.Deduction = AgL.VNull(DtExternalData_Header.Rows(I)("Deduction"))
                SaleInvoiceTable.Round_Off = AgL.VNull(DtExternalData_Header.Rows(I)("Round_Off"))
                SaleInvoiceTable.Net_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Net_Amount"))

                mQry = " Select I.Description As ItemDesc, Ls.ItemCategory, Ls.ItemGroup,
                        OrderH.ManualRefNo As OrderManualRefNo, L.*
                        From SaleInvoiceDetail L 
                        LEFT JOIN SaleInvoiceDetailSku Ls On L.DocId = Ls.DocId And L.Sr = Ls.Sr
                        LEFT JOIN SaleInvoice OrderH On L.SaleInvoice = OrderH.DocId
                        LEFT JOIN Item I ON L.Item = I.Code
                        Where L.DocId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                DtExternalData_Line = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                For J = 0 To DtExternalData_Line.Rows.Count - 1
                    SaleInvoiceTable.Line_Sr = AgL.XNull(DtExternalData_Line.Rows(J)("Sr"))

                    If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
                        SaleInvoiceTable.Line_ItemCode = AgL.XNull(DtExternalData_Line.Rows(J)("Item"))
                    Else
                        SaleInvoiceTable.Line_ItemCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Line.Rows(J)("Item")), ExportSiteCode, DtItem, "Code")
                    End If

                    SaleInvoiceTable.Line_ItemName = AgL.XNull(DtExternalData_Line.Rows(J)("ItemDesc"))
                    SaleInvoiceTable.Line_ItemCategoryCode = AgL.XNull(DtExternalData_Line.Rows(J)("ItemCategory"))
                    SaleInvoiceTable.Line_ItemGroupCode = AgL.XNull(DtExternalData_Line.Rows(J)("ItemGroup"))
                    SaleInvoiceTable.Line_Specification = AgL.XNull(DtExternalData_Line.Rows(J)("Specification"))
                    SaleInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtExternalData_Line.Rows(J)("SalesTaxGroupItem"))
                    SaleInvoiceTable.Line_ReferenceNo = AgL.XNull(DtExternalData_Line.Rows(J)("ReferenceNo"))
                    SaleInvoiceTable.Line_DocQty = AgL.VNull(DtExternalData_Line.Rows(J)("DocQty"))
                    SaleInvoiceTable.Line_FreeQty = AgL.VNull(DtExternalData_Line.Rows(J)("FreeQty"))
                    SaleInvoiceTable.Line_Qty = AgL.VNull(DtExternalData_Line.Rows(J)("Qty"))
                    SaleInvoiceTable.Line_Unit = AgL.XNull(DtExternalData_Line.Rows(J)("Unit"))
                    SaleInvoiceTable.Line_Pcs = AgL.VNull(DtExternalData_Line.Rows(J)("Pcs"))
                    SaleInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtExternalData_Line.Rows(J)("UnitMultiplier"))
                    SaleInvoiceTable.Line_DealUnit = AgL.XNull(DtExternalData_Line.Rows(J)("DealUnit"))
                    SaleInvoiceTable.Line_DocDealQty = AgL.XNull(DtExternalData_Line.Rows(J)("DocDealQty"))

                    If AgL.XNull(DtExternalData_Line.Rows(J)("OrderManualRefNo")) <> "" Then
                        Dim DtRowSaleOrderDetail As DataRow() = DtSaleInvoiceDetail.Select("OMSId = " + AgL.Chk_Text(AgL.XNull(DtExternalData_Line.Rows(J)("SaleInvoice")) +
                                                                AgL.XNull(DtExternalData_Line.Rows(J)("SaleInvoiceSr"))))
                        If DtRowSaleOrderDetail.Length > 0 Then
                            SaleInvoiceTable.Line_SaleInvoice = AgL.XNull(DtRowSaleOrderDetail(0)("DocId"))
                            SaleInvoiceTable.Line_SaleInvoiceSr = AgL.XNull(DtRowSaleOrderDetail(0)("Sr"))
                        End If
                    End If

                    SaleInvoiceTable.Line_OmsId = AgL.XNull(DtExternalData_Line.Rows(J)("DocId")) + AgL.XNull(DtExternalData_Line.Rows(J)("Sr"))
                    SaleInvoiceTable.Line_Rate = AgL.XNull(DtExternalData_Line.Rows(J)("Rate"))
                    SaleInvoiceTable.Line_DiscountPer = AgL.VNull(DtExternalData_Line.Rows(J)("DiscountPer"))
                    SaleInvoiceTable.Line_DiscountAmount = AgL.VNull(DtExternalData_Line.Rows(J)("DiscountAmount"))
                    SaleInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtExternalData_Line.Rows(J)("AdditionalDiscountPer"))
                    SaleInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtExternalData_Line.Rows(J)("AdditionalDiscountAmount"))
                    SaleInvoiceTable.Line_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Amount"))
                    SaleInvoiceTable.Line_Remark = AgL.XNull(DtExternalData_Line.Rows(J)("Remark"))
                    SaleInvoiceTable.Line_CatalogCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Line.Rows(J)("Catalog")), "", DtCatalog, "Code")
                    SaleInvoiceTable.Line_CatalogName = ""
                    SaleInvoiceTable.Line_BaleNo = AgL.XNull(DtExternalData_Line.Rows(J)("BaleNo"))
                    SaleInvoiceTable.Line_LotNo = AgL.XNull(DtExternalData_Line.Rows(J)("LotNo"))
                    SaleInvoiceTable.Line_ReferenceDocId = ""
                    SaleInvoiceTable.Line_ReconcileDateTime = AgL.XNull(DtExternalData_Line.Rows(J)("ReconcileDateTime"))
                    SaleInvoiceTable.Line_GrossWeight = AgL.VNull(DtExternalData_Line.Rows(J)("GrossWeight"))
                    SaleInvoiceTable.Line_NetWeight = AgL.VNull(DtExternalData_Line.Rows(J)("NetWeight"))
                    SaleInvoiceTable.Line_Gross_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Gross_Amount"))
                    SaleInvoiceTable.Line_SpecialDiscount_Per = AgL.VNull(DtExternalData_Line.Rows(J)("SpecialDiscount_Per"))
                    SaleInvoiceTable.Line_SpecialDiscount = AgL.VNull(DtExternalData_Line.Rows(J)("SpecialDiscount"))
                    SaleInvoiceTable.Line_SpecialAddition_Per = AgL.VNull(DtExternalData_Line.Rows(J)("SpecialAddition_Per"))
                    SaleInvoiceTable.Line_SpecialAddition = AgL.VNull(DtExternalData_Line.Rows(J)("SpecialAddition"))
                    SaleInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Taxable_Amount"))
                    SaleInvoiceTable.Line_Tax1_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax1_Per"))
                    SaleInvoiceTable.Line_Tax1 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax1"))
                    SaleInvoiceTable.Line_Tax2_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax2_Per"))
                    SaleInvoiceTable.Line_Tax2 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax2"))
                    SaleInvoiceTable.Line_Tax3_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax3_Per"))
                    SaleInvoiceTable.Line_Tax3 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax3"))
                    SaleInvoiceTable.Line_Tax4_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax4_Per"))
                    SaleInvoiceTable.Line_Tax4 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax4"))
                    SaleInvoiceTable.Line_Tax5_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax5_Per"))
                    SaleInvoiceTable.Line_Tax5 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax5"))
                    SaleInvoiceTable.Line_SubTotal1 = AgL.VNull(DtExternalData_Line.Rows(J)("SubTotal1"))
                    SaleInvoiceTable.Line_Other_Charge = AgL.VNull(DtExternalData_Line.Rows(J)("Other_Charge"))
                    SaleInvoiceTable.Line_Deduction = AgL.VNull(DtExternalData_Line.Rows(J)("Deduction"))
                    SaleInvoiceTable.Line_Round_Off = AgL.VNull(DtExternalData_Line.Rows(J)("Round_Off"))
                    SaleInvoiceTable.Line_Net_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Net_Amount"))

                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                    ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)
                Next

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    UpdateChildProgressBar("Inserting Sale" & bEntryType & SaleInvoiceTable.V_Type & "-" & SaleInvoiceTable.ManualRefNo, mChildPrgMaxVal, mChildPrgCnt)
                    Dim bDocId As String = FrmSaleInvoiceDirect_WithDimension.InsertSaleInvoice(SaleInvoiceTableList)
                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                    If AgL.XNull(DtExternalData_Header.Rows(0)("V_Type")) = "SI" Then
                        bIsSaleInvoicesImportedSuccessfully = False
                    End If
                End Try
            End If
            mChildPrgCnt += 1
        Next
    End Sub
    Public Sub FAddPurch(DtExternalData_Header As DataTable)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim DtExternalData_Line As DataTable
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0


        Dim bEntryType As String = ""
        If DtExternalData_Header.Rows.Count > 0 Then
            mQry = " Select NCat From Voucher_Type Where V_Type = '" & AgL.XNull(DtExternalData_Header.Rows(0)("V_Type")) & "'"
            Dim bNCat As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
            If bNCat = Ncat.PurchaseInvoice Then
                bEntryType = " Invoice "
            ElseIf bNCat = Ncat.PurchaseReturn Then
                bEntryType = " Return "
            ElseIf bNCat = Ncat.PurchaseOrder Then
                bEntryType = " Order "
            End If
        End If


        Dim ExportSiteCode As String = FGetExportSiteCodeFromSiteCode(AgL.XNull(DtExternalData_Header.Rows(0)("Site_Code")))

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count
        For I = 0 To DtExternalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Inserting Purch" & bEntryType, mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking Purch" & bEntryType & AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) & "-" & AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")) & " exist or not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtPurchInvoice.Select("OMSId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "' AND Site_Code = '" & ExportSiteCode & "' ").Length = 0 Then
                Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice
                Dim PurchInvoiceTable As New FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice

                PurchInvoiceTable.DocID = ""
                PurchInvoiceTable.V_Type = AgL.XNull(DtExternalData_Header.Rows(I)("V_Type"))
                PurchInvoiceTable.V_Prefix = AgL.XNull(DtExternalData_Header.Rows(I)("V_Prefix"))
                PurchInvoiceTable.Site_Code = ExportSiteCode
                PurchInvoiceTable.Div_Code = AgL.XNull(DtExternalData_Header.Rows(I)("Div_Code"))
                PurchInvoiceTable.V_No = 0
                PurchInvoiceTable.V_Date = AgL.XNull(DtExternalData_Header.Rows(I)("V_Date"))
                PurchInvoiceTable.ManualRefNo = AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo"))
                PurchInvoiceTable.Vendor = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("Vendor")), ExportSiteCode, DtSubGroup, "SubCode")
                PurchInvoiceTable.VendorName = AgL.XNull(DtExternalData_Header.Rows(I)("VendorName"))
                PurchInvoiceTable.AgentCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("Agent")), ExportSiteCode, DtSubGroup, "SubCode")
                PurchInvoiceTable.AgentName = ""
                PurchInvoiceTable.BillToPartyCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("BillToParty")), ExportSiteCode, DtSubGroup, "SubCode")
                PurchInvoiceTable.BillToPartyName = AgL.XNull(DtExternalData_Header.Rows(I)("BillToPartyName"))
                PurchInvoiceTable.VendorAddress = AgL.XNull(DtExternalData_Header.Rows(I)("VendorAddress"))
                PurchInvoiceTable.VendorMobile = AgL.XNull(DtExternalData_Header.Rows(I)("VendorMobile"))
                PurchInvoiceTable.VendorSalesTaxNo = AgL.XNull(DtExternalData_Header.Rows(I)("VendorSalesTaxNo"))
                PurchInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtExternalData_Header.Rows(I)("SalesTaxGroupParty"))
                PurchInvoiceTable.PlaceOfSupply = AgL.XNull(DtExternalData_Header.Rows(I)("PlaceOfSupply"))
                PurchInvoiceTable.StructureCode = AgL.XNull(DtExternalData_Header.Rows(I)("Structure"))
                PurchInvoiceTable.CustomFields = AgL.XNull(DtExternalData_Header.Rows(I)("CustomFields"))
                PurchInvoiceTable.VendorDocNo = AgL.XNull(DtExternalData_Header.Rows(I)("VendorDocNo"))
                PurchInvoiceTable.VendorDocDate = AgL.XNull(DtExternalData_Header.Rows(I)("VendorDocDate"))
                PurchInvoiceTable.ReferenceDocId = ""
                PurchInvoiceTable.Tags = AgL.XNull(DtExternalData_Header.Rows(I)("Tags"))
                PurchInvoiceTable.Remarks = AgL.XNull(DtExternalData_Header.Rows(I)("Remarks"))
                PurchInvoiceTable.Status = "Active"
                PurchInvoiceTable.EntryBy = AgL.XNull(DtExternalData_Header.Rows(I)("EntryBy"))
                PurchInvoiceTable.EntryDate = AgL.XNull(DtExternalData_Header.Rows(I)("EntryDate"))
                PurchInvoiceTable.ApproveBy = AgL.PubUserName
                PurchInvoiceTable.ApproveDate = AgL.GetDateTime(AgL.GcnRead)
                PurchInvoiceTable.MoveToLog = ""
                PurchInvoiceTable.MoveToLogDate = ""
                PurchInvoiceTable.UploadDate = ""
                PurchInvoiceTable.OmsId = AgL.XNull(DtExternalData_Header.Rows(I)("DocId"))
                PurchInvoiceTable.LockText = "Synced From Other Database."

                PurchInvoiceTable.Gross_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Gross_Amount"))
                PurchInvoiceTable.SpecialDiscount_Per = AgL.VNull(DtExternalData_Header.Rows(I)("SpecialDiscount_Per"))
                PurchInvoiceTable.SpecialDiscount = AgL.VNull(DtExternalData_Header.Rows(I)("SpecialDiscount"))
                PurchInvoiceTable.SpecialAddition_Per = AgL.VNull(DtExternalData_Header.Rows(I)("SpecialAddition_Per"))
                PurchInvoiceTable.SpecialAddition = AgL.VNull(DtExternalData_Header.Rows(I)("SpecialAddition"))
                PurchInvoiceTable.Taxable_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Taxable_Amount"))
                PurchInvoiceTable.Tax1 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax1"))
                PurchInvoiceTable.Tax2 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax2"))
                PurchInvoiceTable.Tax3 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax3"))
                PurchInvoiceTable.Tax4 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax4"))
                PurchInvoiceTable.Tax5 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax5"))
                PurchInvoiceTable.SubTotal1 = AgL.VNull(DtExternalData_Header.Rows(I)("SubTotal1"))
                PurchInvoiceTable.Other_Charge = AgL.VNull(DtExternalData_Header.Rows(I)("Other_Charge"))
                PurchInvoiceTable.Deduction = AgL.VNull(DtExternalData_Header.Rows(I)("Deduction"))
                PurchInvoiceTable.Round_Off = AgL.VNull(DtExternalData_Header.Rows(I)("Round_Off"))
                PurchInvoiceTable.Net_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Net_Amount"))

                mQry = " SELECT I.Description As ItemDesc, OrderH.ManualRefNo As OrderManualRefNo, L.*
                        FROM PurchInvoiceDetail L 
                        LEFT JOIN PurchOrder OrderH On L.PurchInvoice = OrderH.DocId
                        LEFT JOIN Item I ON L.Item = I.Code
                        Where L.DocId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                DtExternalData_Line = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                For J = 0 To DtExternalData_Line.Rows.Count - 1
                    PurchInvoiceTable.Line_Sr = AgL.XNull(DtExternalData_Line.Rows(J)("Sr"))
                    If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
                        PurchInvoiceTable.Line_ItemCode = AgL.XNull(DtExternalData_Line.Rows(J)("Item"))
                    Else
                        PurchInvoiceTable.Line_ItemCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Line.Rows(J)("Item")), ExportSiteCode, DtItem, "Code")
                    End If
                    PurchInvoiceTable.Line_ItemName = AgL.XNull(DtExternalData_Line.Rows(J)("ItemDesc"))
                    PurchInvoiceTable.Line_Specification = AgL.XNull(DtExternalData_Line.Rows(J)("Specification"))
                    PurchInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtExternalData_Line.Rows(J)("SalesTaxGroupItem"))
                    PurchInvoiceTable.Line_ReferenceNo = AgL.XNull(DtExternalData_Line.Rows(J)("ReferenceNo"))
                    PurchInvoiceTable.Line_DocQty = AgL.VNull(DtExternalData_Line.Rows(J)("DocQty"))
                    PurchInvoiceTable.Line_FreeQty = AgL.VNull(DtExternalData_Line.Rows(J)("FreeQty"))
                    PurchInvoiceTable.Line_Qty = AgL.VNull(DtExternalData_Line.Rows(J)("Qty"))
                    PurchInvoiceTable.Line_Unit = AgL.XNull(DtExternalData_Line.Rows(J)("Unit"))
                    PurchInvoiceTable.Line_Pcs = AgL.VNull(DtExternalData_Line.Rows(J)("Pcs"))
                    PurchInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtExternalData_Line.Rows(J)("UnitMultiplier"))
                    PurchInvoiceTable.Line_DealUnit = AgL.XNull(DtExternalData_Line.Rows(J)("DealUnit"))
                    PurchInvoiceTable.Line_DocDealQty = AgL.XNull(DtExternalData_Line.Rows(J)("DocDealQty"))

                    If AgL.XNull(DtExternalData_Line.Rows(J)("OrderManualRefNo")) <> "" Then
                        Dim DtRowPurchOrderDetail As DataRow() = DtPurchInvoiceDetail.Select("OMSId = " + AgL.Chk_Text(AgL.XNull(DtExternalData_Line.Rows(J)("PurchInvoice")) +
                                                                AgL.XNull(DtExternalData_Line.Rows(J)("PurchInvoiceSr"))))
                        If DtRowPurchOrderDetail.Length > 0 Then
                            PurchInvoiceTable.Line_PurchInvoice = AgL.XNull(DtRowPurchOrderDetail(0)("DocId"))
                            PurchInvoiceTable.Line_PurchInvoiceSr = AgL.XNull(DtRowPurchOrderDetail(0)("Sr"))
                        End If
                    End If

                    PurchInvoiceTable.Line_OmsId = AgL.XNull(DtExternalData_Line.Rows(J)("DocId")) + AgL.XNull(DtExternalData_Line.Rows(J)("Sr"))
                    PurchInvoiceTable.Line_Rate = AgL.XNull(DtExternalData_Line.Rows(J)("Rate"))
                    PurchInvoiceTable.Line_DiscountPer = AgL.VNull(DtExternalData_Line.Rows(J)("DiscountPer"))
                    PurchInvoiceTable.Line_DiscountAmount = AgL.VNull(DtExternalData_Line.Rows(J)("DiscountAmount"))
                    PurchInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtExternalData_Line.Rows(J)("AdditionalDiscountPer"))
                    PurchInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtExternalData_Line.Rows(J)("AdditionalDiscountAmount"))
                    PurchInvoiceTable.Line_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Amount"))
                    PurchInvoiceTable.Line_Remark = AgL.XNull(DtExternalData_Line.Rows(J)("Remark"))
                    PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtExternalData_Line.Rows(J)("BaleNo"))
                    PurchInvoiceTable.Line_LotNo = AgL.XNull(DtExternalData_Line.Rows(J)("LotNo"))
                    PurchInvoiceTable.Line_ReferenceDocId = ""
                    PurchInvoiceTable.Line_GrossWeight = AgL.VNull(DtExternalData_Line.Rows(J)("GrossWeight"))
                    PurchInvoiceTable.Line_NetWeight = AgL.VNull(DtExternalData_Line.Rows(J)("NetWeight"))
                    PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Gross_Amount"))
                    PurchInvoiceTable.Line_SpecialDiscount_Per = AgL.VNull(DtExternalData_Line.Rows(J)("SpecialDiscount_Per"))
                    PurchInvoiceTable.Line_SpecialDiscount = AgL.VNull(DtExternalData_Line.Rows(J)("SpecialDiscount"))
                    PurchInvoiceTable.Line_SpecialAddition_Per = AgL.VNull(DtExternalData_Line.Rows(J)("SpecialAddition_Per"))
                    PurchInvoiceTable.Line_SpecialAddition = AgL.VNull(DtExternalData_Line.Rows(J)("SpecialAddition"))
                    PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Taxable_Amount"))
                    PurchInvoiceTable.Line_Tax1_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax1_Per"))
                    PurchInvoiceTable.Line_Tax1 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax1"))
                    PurchInvoiceTable.Line_Tax2_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax2_Per"))
                    PurchInvoiceTable.Line_Tax2 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax2"))
                    PurchInvoiceTable.Line_Tax3_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax3_Per"))
                    PurchInvoiceTable.Line_Tax3 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax3"))
                    PurchInvoiceTable.Line_Tax4_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax4_Per"))
                    PurchInvoiceTable.Line_Tax4 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax4"))
                    PurchInvoiceTable.Line_Tax5_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax5_Per"))
                    PurchInvoiceTable.Line_Tax5 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax5"))
                    PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtExternalData_Line.Rows(J)("SubTotal1"))
                    PurchInvoiceTable.Line_Other_Charge = AgL.VNull(DtExternalData_Line.Rows(J)("Other_Charge"))
                    PurchInvoiceTable.Line_Deduction = AgL.VNull(DtExternalData_Line.Rows(J)("Deduction"))
                    PurchInvoiceTable.Line_Round_Off = AgL.VNull(DtExternalData_Line.Rows(J)("Round_Off"))
                    PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Net_Amount"))

                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                    ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                Next

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    UpdateChildProgressBar("Inserting Purch" & bEntryType & PurchInvoiceTable.V_Type & "-" & PurchInvoiceTable.ManualRefNo, mChildPrgMaxVal, mChildPrgCnt)
                    Dim bDocId As String = FrmPurchInvoiceDirect_WithDimension.InsertPurchInvoice(PurchInvoiceTableList)

                    If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
                        mQry = "UPDATE Ledger Set SubCode = 'GOODS' Where DocId = '" & bDocId & "' And SubCode = 'PURCH'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                    If AgL.XNull(DtExternalData_Header.Rows(0)("V_Type")) = "PI" Then
                        bIsPurchInvoicesImportedSuccessfully = False
                    End If
                End Try
            End If
            mChildPrgCnt += 1
        Next
    End Sub

    Public Sub FAddLedgerHead(DtExternalData_Header As DataTable)
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0


        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count
        Dim ExportSiteCode As String = FGetExportSiteCodeFromSiteCode(AgL.XNull(DtExternalData_Header.Rows(0)("Site_Code")))

        For I = 0 To DtExternalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Inserting Ledger Heads", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking " + AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) + "-" + AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtLedgerHead.Select("OMSId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "' AND Site_Code = '" & ExportSiteCode & "' ").Length = 0 Then
                Dim LedgerHeadTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim LedgerHeadTable As New FrmVoucherEntry.StructLedgerHead

                LedgerHeadTable.DocID = ""
                LedgerHeadTable.V_Type = AgL.XNull(DtExternalData_Header.Rows(I)("V_Type"))
                LedgerHeadTable.V_Prefix = AgL.XNull(DtExternalData_Header.Rows(I)("V_Prefix"))
                LedgerHeadTable.Site_Code = ExportSiteCode
                LedgerHeadTable.Div_Code = AgL.XNull(DtExternalData_Header.Rows(I)("Div_Code"))
                LedgerHeadTable.V_No = 0
                LedgerHeadTable.V_Date = AgL.XNull(DtExternalData_Header.Rows(I)("V_Date"))
                LedgerHeadTable.ManualRefNo = AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo"))
                LedgerHeadTable.Subcode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Header.Rows(I)("SubCode")), ExportSiteCode, DtSubGroup, "SubCode")
                LedgerHeadTable.SubcodeName = AgL.XNull(DtExternalData_Header.Rows(I)("PartyName_Master"))
                LedgerHeadTable.SalesTaxGroupParty = AgL.XNull(DtExternalData_Header.Rows(I)("SalesTaxGroupParty"))
                LedgerHeadTable.PlaceOfSupply = AgL.XNull(DtExternalData_Header.Rows(I)("PlaceOfSupply"))
                If LedgerHeadTable.V_Type = "VR" Then
                    LedgerHeadTable.StructureCode = ""
                Else
                    LedgerHeadTable.StructureCode = AgL.XNull(DtExternalData_Header.Rows(I)("Structure"))
                End If
                LedgerHeadTable.CustomFields = AgL.XNull(DtExternalData_Header.Rows(I)("CustomFields"))
                LedgerHeadTable.PartyDocNo = AgL.XNull(DtExternalData_Header.Rows(I)("PartyDocNo"))
                LedgerHeadTable.PartyDocDate = AgL.XNull(DtExternalData_Header.Rows(I)("PartyDocDate"))
                LedgerHeadTable.Remarks = AgL.XNull(DtExternalData_Header.Rows(I)("Remarks"))
                LedgerHeadTable.Status = "Active"
                LedgerHeadTable.EntryBy = AgL.PubUserName
                LedgerHeadTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                LedgerHeadTable.ApproveBy = ""
                LedgerHeadTable.ApproveDate = ""
                LedgerHeadTable.MoveToLog = ""
                LedgerHeadTable.MoveToLogDate = ""
                LedgerHeadTable.UploadDate = ""
                LedgerHeadTable.OMSId = AgL.XNull(DtExternalData_Header.Rows(I)("DocId"))
                LedgerHeadTable.LockText = "Synced From Other Database."

                LedgerHeadTable.Gross_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Gross_Amount"))
                LedgerHeadTable.Taxable_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Taxable_Amount"))
                LedgerHeadTable.Tax1 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax1"))
                LedgerHeadTable.Tax2 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax2"))
                LedgerHeadTable.Tax3 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax3"))
                LedgerHeadTable.Tax4 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax4"))
                LedgerHeadTable.Tax5 = AgL.VNull(DtExternalData_Header.Rows(I)("Tax5"))
                LedgerHeadTable.SubTotal1 = AgL.VNull(DtExternalData_Header.Rows(I)("SubTotal1"))
                LedgerHeadTable.Other_Charge = AgL.VNull(DtExternalData_Header.Rows(I)("Other_Charge"))
                LedgerHeadTable.Deduction = AgL.VNull(DtExternalData_Header.Rows(I)("Deduction"))
                LedgerHeadTable.Round_Off = AgL.VNull(DtExternalData_Header.Rows(I)("Round_Off"))
                LedgerHeadTable.Net_Amount = AgL.VNull(DtExternalData_Header.Rows(I)("Net_Amount"))

                mQry = " Select Sg1.Name As SubCodeName, Sg2.Name As LinkedSubCodeName, L.*, Lc.*
                        From LedgerHeadDetail L
                        Left Join LedgerHeadDetailCharges Lc On L.DocId = Lc.DocId And L.Sr = Lc.Sr
                        Left Join SubGroup Sg1 On L.SubCode = Sg1.SubCode
                        Left Join SubGroup Sg2 On L.LinkedSubCode = Sg2.SubCode
                        Where L.DocId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                Dim DtExternalData_Line As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                For J = 0 To DtExternalData_Line.Rows.Count - 1
                    LedgerHeadTable.Line_Sr = AgL.XNull(DtExternalData_Line.Rows(J)("Sr"))
                    LedgerHeadTable.Line_SubCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Line.Rows(J)("SubCode")), ExportSiteCode, DtSubGroup, "SubCode")
                    LedgerHeadTable.Line_SubCodeName = AgL.XNull(DtExternalData_Line.Rows(J)("SubCodeName"))
                    LedgerHeadTable.Line_LinkedSubCode = FGetCodeFromOMSId(AgL.XNull(DtExternalData_Line.Rows(J)("LinkedSubCode")), ExportSiteCode, DtSubGroup, "SubCode")
                    LedgerHeadTable.Line_LinkedSubCodeName = AgL.XNull(DtExternalData_Line.Rows(J)("LinkedSubCodeName"))
                    LedgerHeadTable.Line_Specification = AgL.XNull(DtExternalData_Line.Rows(J)("Specification"))
                    LedgerHeadTable.Line_SalesTaxGroupItem = AgL.XNull(DtExternalData_Line.Rows(J)("SalesTaxGroupItem"))
                    LedgerHeadTable.Line_Qty = AgL.VNull(DtExternalData_Line.Rows(J)("Qty"))
                    LedgerHeadTable.Line_Unit = AgL.XNull(DtExternalData_Line.Rows(J)("Unit"))
                    LedgerHeadTable.Line_Rate = AgL.VNull(DtExternalData_Line.Rows(J)("Rate"))
                    LedgerHeadTable.Line_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Amount"))
                    LedgerHeadTable.Line_Amount_Cr = AgL.VNull(DtExternalData_Line.Rows(J)("AmountCr"))
                    LedgerHeadTable.Line_ChqRefNo = AgL.XNull(DtExternalData_Line.Rows(J)("ChqRefNo"))
                    LedgerHeadTable.Line_ChqRefDate = AgL.XNull(DtExternalData_Line.Rows(J)("ChqRefDate"))
                    LedgerHeadTable.Line_Remarks = AgL.XNull(DtExternalData_Line.Rows(J)("Remarks"))
                    LedgerHeadTable.Line_OMSId = AgL.XNull(DtExternalData_Line.Rows(J)("DocId")) + AgL.XNull(DtExternalData_Line.Rows(J)("Sr"))

                    LedgerHeadTable.Line_Gross_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Gross_Amount"))
                    LedgerHeadTable.Line_Taxable_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Taxable_Amount"))
                    LedgerHeadTable.Line_Tax1_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax1_Per"))
                    LedgerHeadTable.Line_Tax1 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax1"))
                    LedgerHeadTable.Line_Tax2_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax2_Per"))
                    LedgerHeadTable.Line_Tax2 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax2"))
                    LedgerHeadTable.Line_Tax3_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax3_Per"))
                    LedgerHeadTable.Line_Tax3 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax3"))
                    LedgerHeadTable.Line_Tax4_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax4_Per"))
                    LedgerHeadTable.Line_Tax4 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax4"))
                    LedgerHeadTable.Line_Tax5_Per = AgL.VNull(DtExternalData_Line.Rows(J)("Tax5_Per"))
                    LedgerHeadTable.Line_Tax5 = AgL.VNull(DtExternalData_Line.Rows(J)("Tax5"))
                    LedgerHeadTable.Line_SubTotal1 = AgL.VNull(DtExternalData_Line.Rows(J)("SubTotal1"))
                    LedgerHeadTable.Line_Other_Charge = AgL.VNull(DtExternalData_Line.Rows(J)("Other_Charge"))
                    LedgerHeadTable.Line_Deduction = AgL.VNull(DtExternalData_Line.Rows(J)("Deduction"))
                    LedgerHeadTable.Line_Round_Off = AgL.VNull(DtExternalData_Line.Rows(J)("Round_Off"))
                    LedgerHeadTable.Line_Net_Amount = AgL.VNull(DtExternalData_Line.Rows(J)("Net_Amount"))

                    LedgerHeadTableList(UBound(LedgerHeadTableList)) = LedgerHeadTable
                    ReDim Preserve LedgerHeadTableList(UBound(LedgerHeadTableList) + 1)
                Next

                mQry = " Select Count(*) As Cnt From Ledger L
                        Where L.DocId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                Dim DtExternalData_Ledger As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    UpdateChildProgressBar("Inserting Entry " & LedgerHeadTable.V_Type & "-" & LedgerHeadTable.ManualRefNo, mChildPrgMaxVal, mChildPrgCnt)
                    Dim bDocId As String = FrmVoucherEntry.InsertLedgerHead(LedgerHeadTableList)
                    FPostReverEffectInBranchSite(bDocId, AgL.GCn, AgL.ECmd)
                    FReverseEffectOnHOSite(bDocId, AgL.GCn, AgL.ECmd)
                    FLinkVisitReceiptAndCashReceiptAccordingToBranch(bDocId, AgL.XNull(DtExternalData_Header.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)

                    'It Means Source Database is not posting this DocId in Ledger that's why it will not post in ledger table.
                    If AgL.VNull(DtExternalData_Ledger.Rows(0)("Cnt")) = 0 Then
                        mQry = " Delete From Ledger Where DocId = '" & bDocId & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                End Try
            End If
            mChildPrgCnt += 1
        Next
    End Sub
    Private Sub FUpdateLedgerHead(DtExternalData_Header As DataTable)
        Dim mEntryChanged As Boolean = False

        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0


        Dim DtFieldList_Header As DataTable
        Dim DtFieldList_Line As DataTable

        Dim DtExternalData_Line As DataTable

        Dim DtLocalData_Header As DataTable
        Dim DtLocalData_Line As DataTable

        Dim DtExternalData_Ledger As DataTable
        Dim DtLocalData_Ledger As DataTable


        Dim bExternalDocIdStr As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            If bExternalDocIdStr <> "" Then bExternalDocIdStr += ","
            bExternalDocIdStr += AgL.Chk_Text(AgL.XNull(DtExternalData_Header.Rows(I)("DocId")))
        Next

        If bExternalDocIdStr = "" Then Exit Sub
        Dim ExportSiteCode As String = FGetExportSiteCodeFromSiteCode(AgL.XNull(DtExternalData_Header.Rows(0)("Site_Code")))

        mQry = " Select H.*, Sg.OMSId As SubCodeOMSId, C.OMSID As PartyCityOMSId 
                From LedgerHead H
                LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                LEFT JOIN City C On H.PartyCity = C.CityCode
                Where H.OMSId In (" & bExternalDocIdStr & ") AND H.Site_Code =  '" & ExportSiteCode & "' "
        DtLocalData_Header = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
        mQry = "PRAGMA table_info(LedgerHead);"
        DtFieldList_Header = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            mEntryChanged = False
            UpdateParentProgressBar("Updating Ledger Heads", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking " + AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) + "-" + AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            For J As Integer = 0 To DtLocalData_Header.Rows.Count - 1
                If AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) = AgL.XNull(DtLocalData_Header.Rows(J)("OMSId")) And ExportSiteCode = AgL.XNull(DtLocalData_Header.Rows(J)("Site_Code")) Then
                    bUpdateClauseQry = ""
                    For F As Integer = 0 To DtFieldList_Header.Rows.Count - 1
                        If DtFieldList_Header.Rows(F)("Name") = "DocId" Or
                            DtFieldList_Header.Rows(F)("Name") = "V_No" Or
                            DtFieldList_Header.Rows(F)("Name") = "Site_Code" Or
                            DtFieldList_Header.Rows(F)("Name") = "Div_Code" Or
                            DtFieldList_Header.Rows(F)("Name") = "LockText" Or
                            DtFieldList_Header.Rows(F)("Name") = "Structure" Or
                            DtFieldList_Header.Rows(F)("Name") = "ApproveBy" Or
                            DtFieldList_Header.Rows(F)("Name") = "ApproveDate" Or
                            DtFieldList_Header.Rows(F)("Name") = "MoveToLog" Or
                            DtFieldList_Header.Rows(F)("Name") = "MoveToLogDate" Or
                            DtFieldList_Header.Rows(F)("Name") = "OMSId" Then
                            'Do Nothing
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "SubCode" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), "SubCode", DtSubGroup, "SubGroup")
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "PartyCity" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), "CityCode", DtCity, "City")
                        Else
                            bUpdateClauseQry += FGetUpdateClause(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), DtFieldList_Header.Rows(F)("Type"))
                        End If
                    Next

                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"



                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE LedgerHead Set " + bUpdateClauseQry + " Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            mEntryChanged = True
                        End If

                        'For Line Logic
                        mQry = "Select * From LedgerHeadDetail Where DocId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                        DtExternalData_Line = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
                        mQry = "Select * From LedgerHeadDetail Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                        DtLocalData_Line = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

                        If AgL.XNull(DtLocalData_Line.Rows(0)("DocId")) = "D2    JV 2019    3349" Then MsgBox("In")

                        For K As Integer = 0 To DtLocalData_Line.Rows.Count - 1
                            If DtExternalData_Line.Select(" DocId + Sr = '" + AgL.XNull(DtLocalData_Line.Rows(K)("OMSId")) + "'").Length = 0 Then
                                mQry = " Delete From LedgerHeadDetail Where OMSId = '" & AgL.XNull(DtLocalData_Line.Rows(K)("OMSId")) & "' AND DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "' "
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next

                        For K As Integer = 0 To DtExternalData_Line.Rows.Count - 1
                            If DtLocalData_Line.Select(" OMSId = '" + AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) + "'").Length = 0 Then
                                mQry = " Insert Into LedgerHeadDetail(DocId, Sr, OMSId)
                                        Select '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "', 
                                        " & AgL.VNull(DtExternalData_Line.Rows(K)("Sr")) & ", 
                                        '" & AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) & "'"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next

                        mQry = "Select Sg.OMSId As SubCodeOMSId, L.* 
                                From LedgerHeadDetail L With (NoLock) 
                                LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
                                Where L.DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                        DtLocalData_Line = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
                        mQry = "PRAGMA table_info(LedgerHeadDetail);"
                        DtFieldList_Line = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                        For K As Integer = 0 To DtExternalData_Line.Rows.Count - 1
                            For L As Integer = 0 To DtLocalData_Line.Rows.Count - 1
                                If AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) = AgL.XNull(DtLocalData_Line.Rows(L)("OMSId")) Then
                                    bUpdateClauseQry = ""
                                    For F As Integer = 0 To DtFieldList_Line.Rows.Count - 1
                                        If DtFieldList_Line.Rows(F)("Name") = "DocId" Or
                                            DtFieldList_Line.Rows(F)("Name") = "Sr" Or
                                            DtFieldList_Line.Rows(F)("Name") = "OMSId" Or
                                            DtFieldList_Line.Rows(F)("Name") = "ReferenceDocId" Or
                                            DtFieldList_Line.Rows(F)("Name") = "ReferenceDocIdSr" Then
                                            'Do Nothing
                                        ElseIf DtFieldList_Line.Rows(F)("Name") = "SubCode" Then
                                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), "SubCode", DtSubGroup, "SubGroup")
                                        Else
                                            bUpdateClauseQry += FGetUpdateClause(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), DtFieldList_Line.Rows(F)("Type"))
                                        End If
                                    Next

                                    If bUpdateClauseQry <> "" Then
                                        bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                                        mQry = " UPDATE LedgerHeadDetail Set " + bUpdateClauseQry +
                                                    " Where DocId = '" & AgL.XNull(DtLocalData_Line.Rows(L)("DocId")) & "'
                                                    And Sr = " & AgL.XNull(DtLocalData_Line.Rows(L)("Sr")) & ""
                                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                                        mEntryChanged = True
                                    End If
                                End If
                            Next
                        Next

                        mQry = "Select IfNull(Sum(AmtDr),0) As AmtDr_Total, IfNull(Sum(AmtCr),0) As AmtCr_Total From Ledger Where DocId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                        DtExternalData_Ledger = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                        mQry = "Select IfNull(Sum(AmtDr),0) As AmtDr_Total, IfNull(Sum(AmtCr),0) As AmtCr_Total From Ledger Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                        DtLocalData_Ledger = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

                        If AgL.VNull(DtExternalData_Ledger.Rows(0)("AmtDr_Total")) <> AgL.VNull(DtLocalData_Ledger.Rows(0)("AmtDr_Total")) Or
                            AgL.VNull(DtExternalData_Ledger.Rows(0)("AmtCr_Total")) <> AgL.VNull(DtLocalData_Ledger.Rows(0)("AmtCr_Total")) Then
                            mEntryChanged = True
                        End If

                        FLinkVisitReceiptAndCashReceiptAccordingToBranch(AgL.XNull(DtLocalData_Header.Rows(J)("DocId")), AgL.XNull(DtExternalData_Header.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)

                        If mEntryChanged = True Then
                            mQry = " Delete From LedgerHeadCharges Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Delete From LedgerHeadDetailCharges Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = "SELECT Hc.*
                                    FROM LedgerHeadCharges Hc
                                    WHERE Hc.DocID = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                            Dim DtExternalData_HeaderCharges As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                            If DtExternalData_HeaderCharges.Rows.Count > 0 Then
                                mQry = " INSERT INTO LedgerHeadCharges (DocID, Gross_Amount, SpecialDiscount_Per, SpecialDiscount, SpecialAddition_Per, SpecialAddition, Taxable_Amount, Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount)
                                        SELECT '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "' As DocId, 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Gross_Amount")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("SpecialDiscount_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("SpecialDiscount")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("SpecialAddition_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("SpecialAddition")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Taxable_Amount")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax1_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax1")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax2_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax2")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax3_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax3")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax4_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax4")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax5_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Tax5")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("SubTotal1")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Deduction_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Deduction")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Other_Charge_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Other_Charge")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Round_Off")) & ", 
                                        " & AgL.VNull(DtExternalData_HeaderCharges.Rows(0)("Net_Amount")) & " "
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If

                            mQry = "SELECT Lc.*
                                    FROM LedgerHeadDetailCharges Lc
                                    WHERE Lc.DocID = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                            Dim DtExternalData_LineCharges As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                            For X As Integer = 0 To DtExternalData_LineCharges.Rows.Count - 1
                                mQry = " INSERT INTO LedgerHeadDetailCharges (DocID, Sr, Gross_Amount, SpecialDiscount_Per, SpecialDiscount, SpecialAddition_Per, SpecialAddition, Taxable_Amount, Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount)
                                        SELECT '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "' As DocId, 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Sr")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Gross_Amount")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("SpecialDiscount_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("SpecialDiscount")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("SpecialAddition_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("SpecialAddition")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Taxable_Amount")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax1_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax1")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax2_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax2")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax3_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax3")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax4_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax4")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax5_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Tax5")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("SubTotal1")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Deduction_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Deduction")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Other_Charge_Per")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Other_Charge")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Round_Off")) & ", 
                                        " & AgL.VNull(DtExternalData_LineCharges.Rows(X)("Net_Amount")) & " "
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            Next

                            'It Means Source Database is not posting this DocId in Ledger that's why it will not post in ledger table.
                            If AgL.VNull(DtExternalData_Ledger.Rows(0)("AmtDr_Total")) <> 0 Or AgL.VNull(DtExternalData_Ledger.Rows(0)("AmtCr_Total")) <> 0 Then
                                FrmVoucherEntry.FGetCalculationData(AgL.XNull(DtLocalData_Header.Rows(J)("DocId")), AgL.GCn, AgL.ECmd)
                            End If
                            FPostReverEffectInBranchSite(AgL.XNull(DtLocalData_Header.Rows(J)("DocId")), AgL.GCn, AgL.ECmd)
                            FReverseEffectOnHOSite(AgL.XNull(DtLocalData_Header.Rows(J)("DocId")), AgL.GCn, AgL.ECmd)
                            FLinkVisitReceiptAndCashReceiptAccordingToBranch(AgL.XNull(DtLocalData_Header.Rows(J)("DocId")), AgL.XNull(DtExternalData_Header.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)

                            UpdateChildProgressBar("Updating Entry " & AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) & "-" & AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")), mChildPrgMaxVal, mChildPrgCnt)
                            FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                        End If

                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                        AgL.ETrans.Rollback()
                    End Try
                End If
            Next
            mChildPrgCnt += 1
        Next
    End Sub
    Private Sub FUpdatePurch(DtExternalData_Header As DataTable)
        Dim mEntryChanged As Boolean = False

        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0


        Dim DtFieldList_Header As DataTable
        Dim DtFieldList_Line As DataTable

        Dim DtExternalData_Line As DataTable

        Dim DtLocalData_Header As DataTable
        Dim DtLocalData_Line As DataTable


        Dim bEntryType As String = ""
        If DtExternalData_Header.Rows.Count > 0 Then
            mQry = " Select NCat From Voucher_Type Where V_Type = '" & AgL.XNull(DtExternalData_Header.Rows(0)("V_Type")) & "'"
            Dim bNCat As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
            If bNCat = Ncat.PurchaseInvoice Then
                bEntryType = " Invoice "
            ElseIf bNCat = Ncat.PurchaseReturn Then
                bEntryType = " Return "
            ElseIf bNCat = Ncat.PurchaseOrder Then
                bEntryType = " Order "
            End If
        End If

        Dim ExportSiteCode As String = FGetExportSiteCodeFromSiteCode(AgL.XNull(DtExternalData_Header.Rows(0)("Site_Code")))
        Dim bExternalDocIdStr As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            If bExternalDocIdStr <> "" Then bExternalDocIdStr += ","
            bExternalDocIdStr += AgL.Chk_Text(AgL.XNull(DtExternalData_Header.Rows(I)("DocId")))
        Next

        If bExternalDocIdStr = "" Then Exit Sub

        mQry = " Select Sg.OMSId As VendorOMSId, BSg.OMSId As BillToPartyOMSId, 
                C.OMSID As VendorCityOMSId, H.* 
                From PurchInvoice H 
                LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode
                LEFT JOIN SubGroup BSg On H.BillToParty = BSg.SubCode
                LEFT JOIN City C On H.VendorCity = C.CityCode
                Where H.OMSId In (" & bExternalDocIdStr & ") AND H.Site_Code =  '" & ExportSiteCode & "'"
        DtLocalData_Header = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
        mQry = "PRAGMA table_info(PurchInvoice);"
        DtFieldList_Header = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtExternalData_Header.Rows.Count - 1
            mEntryChanged = False
            UpdateParentProgressBar("Updating Purch" & bEntryType, mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking Purch" & bEntryType + AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) + "-" + AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            For J As Integer = 0 To DtLocalData_Header.Rows.Count - 1
                If AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) = AgL.XNull(DtLocalData_Header.Rows(J)("OMSId")) And ExportSiteCode = AgL.XNull(DtLocalData_Header.Rows(J)("Site_Code")) Then
                    bUpdateClauseQry = ""
                    For F As Integer = 0 To DtFieldList_Header.Rows.Count - 1
                        If DtFieldList_Header.Rows(F)("Name") = "DocId" Or
                            DtFieldList_Header.Rows(F)("Name") = "V_No" Or
                            DtFieldList_Header.Rows(F)("Name") = "Site_Code" Or
                            DtFieldList_Header.Rows(F)("Name") = "Div_Code" Or
                            DtFieldList_Header.Rows(F)("Name") = "LockText" Or
                            DtFieldList_Header.Rows(F)("Name") = "ApproveBy" Or
                            DtFieldList_Header.Rows(F)("Name") = "ApproveDate" Or
                            DtFieldList_Header.Rows(F)("Name") = "MoveToLog" Or
                            DtFieldList_Header.Rows(F)("Name") = "MoveToLogDate" Or
                            DtFieldList_Header.Rows(F)("Name") = "OMSId" Then
                            'Do Nothing
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "Vendor" Or DtFieldList_Header.Rows(F)("Name") = "BillToParty" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), "SubCode", DtSubGroup, "SubGroup")
                        ElseIf DtFieldList_Header.Rows(F)("Name") = "VendorCity" Then
                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), "CityCode", DtCity, "City")
                        Else
                            bUpdateClauseQry += FGetUpdateClause(DtExternalData_Header, I, DtLocalData_Header, J, DtFieldList_Header.Rows(F)("Name"), DtFieldList_Header.Rows(F)("Type"))
                        End If
                    Next

                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE PurchInvoice Set " + bUpdateClauseQry + " Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            mEntryChanged = True
                        End If




                        'For Line Logic
                        mQry = "Select * From PurchInvoiceDetail Where DocId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("DocId")) & "'"
                        DtExternalData_Line = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
                        mQry = "Select * From PurchInvoiceDetail Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                        DtLocalData_Line = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

                        For K As Integer = 0 To DtLocalData_Line.Rows.Count - 1
                            If DtExternalData_Line.Select(" DocId + Sr = '" + AgL.XNull(DtLocalData_Line.Rows(K)("OMSId")) + "'").Length = 0 Then
                                mQry = " Delete From PurchInvoiceDetail Where OMSId = '" & AgL.XNull(DtLocalData_Line.Rows(K)("OMSId")) & "' AND DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "' "
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next

                        For K As Integer = 0 To DtExternalData_Line.Rows.Count - 1
                            If DtLocalData_Line.Select(" OMSId = '" + AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) + "'").Length = 0 Then
                                mQry = " Insert Into PurchInvoiceDetail(DocId, Sr, PurchInvoice, PurchInvoiceSr, OMSId)
                                        Select '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "', 
                                        " & AgL.VNull(DtExternalData_Line.Rows(K)("Sr")) & ", 
                                        '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "', 
                                        " & AgL.VNull(DtExternalData_Line.Rows(K)("Sr")) & ", 
                                        '" & AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) & "'"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next

                        mQry = "Select I.OMSId As ItemOMSId, Ist.OMSId As ItemStateOMSId,  
                                G.OMSId As GodownOMSId, L.* 
                                From PurchInvoiceDetail L With (NoLock) 
                                LEFT JOIN Item I ON L.Item = I.Code
                                LEFT JOIN Item Ist ON L.ItemState = Ist.Code
                                LEFT JOIN SubGroup G On L.Godown = G.SubCode
                                Where L.DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                        DtLocalData_Line = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
                        mQry = "PRAGMA table_info(PurchInvoiceDetail);"
                        DtFieldList_Line = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)


                        For K As Integer = 0 To DtExternalData_Line.Rows.Count - 1
                            For L As Integer = 0 To DtLocalData_Line.Rows.Count - 1
                                If AgL.XNull(DtExternalData_Line.Rows(K)("DocId")) +
                                        AgL.XNull(DtExternalData_Line.Rows(K)("Sr")) = AgL.XNull(DtLocalData_Line.Rows(L)("OMSId")) Then
                                    bUpdateClauseQry = ""
                                    For F As Integer = 0 To DtFieldList_Line.Rows.Count - 1
                                        If DtFieldList_Line.Rows(F)("Name") = "DocId" Or DtFieldList_Line.Rows(F)("Name") = "Sr" Or
                                            DtFieldList_Line.Rows(F)("Name") = "PurchInvoice" Or DtFieldList_Line.Rows(F)("Name") = "PurchInvoiceSr" Or DtFieldList_Line.Rows(F)("Name") = "OMSId" Then
                                            'Do Nothing
                                        ElseIf DtFieldList_Line.Rows(F)("Name") = "Item" Or DtFieldList_Line.Rows(F)("Name") = "ItemState" Then
                                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), "Code", DtItem, "Item")
                                        ElseIf DtFieldList_Line.Rows(F)("Name") = "Godown" Then
                                            bUpdateClauseQry += FGetUpdateClauseForCodes(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), "SubCode", DtSubGroup, "SubGroup")
                                        Else
                                            bUpdateClauseQry += FGetUpdateClause(DtExternalData_Line, K, DtLocalData_Line, L, DtFieldList_Line.Rows(F)("Name"), DtFieldList_Line.Rows(F)("Type"))
                                        End If
                                    Next

                                    If bUpdateClauseQry <> "" Then
                                        bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                                        mQry = " UPDATE PurchInvoiceDetail Set " + bUpdateClauseQry +
                                                    " Where DocId = '" & AgL.XNull(DtLocalData_Line.Rows(L)("DocId")) & "'
                                                    And Sr = " & AgL.XNull(DtLocalData_Line.Rows(L)("Sr")) & ""
                                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                                        mEntryChanged = True
                                    End If
                                End If
                            Next
                        Next

                        If mEntryChanged = True Then
                            mQry = " Delete From StockAdj Where StockInDocID = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Delete From StockAdj Where StockOutDocID = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                    SubCode, SalesTaxGroupParty,  Item,  LotNo, 
                                    EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                    ReferenceDocID, ReferenceDocIDSr, Rate, Amount, Landed_Value) 
                                    Select L.DocId, L.Sr, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, 
                                    H.Div_Code, H.Site_Code, H.Vendor,  H.SalesTaxGroupParty,  L.Item,
                                    L.LotNo, 'I', 
                                    Case When IfNull(Vt.Nature,'') In ('" & NCatNature.Receive & "', '" & NCatNature.Invoice & "') Then 0 Else IfNull(Abs(L.Qty),0) End As Qty_Iss,
                                    Case When IfNull(Vt.Nature,'') In ('" & NCatNature.Receive & "', '" & NCatNature.Invoice & "') Then IfNull(Abs(L.Qty),0) Else 0 End As Qty_Rec,
                                    L.Unit, L.UnitMultiplier, 
                                    0 As DealQty_Iss, 
                                    0 As DealQty_Rec, 
                                    L.DealUnit,  
                                    L.ReferenceDocId, L.ReferenceSr, 
                                    L.Amount/(Case When IfNull(L.Qty,0) = 0 Then 1 Else L.Qty End), L.Amount, L.Amount
                                    FROM PurchInvoiceDetail L    
                                    LEFT JOIN PurchInvoice H On L.DocId = H.DocId 
                                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                                    WHERE L.DocId =  '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "' "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            FrmPurchInvoiceDirect_WithDimension.FGetCalculationData(AgL.XNull(DtLocalData_Header.Rows(J)("DocId")), AgL.GCn, AgL.ECmd)

                            If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
                                mQry = "UPDATE Ledger Set SubCode = 'GOODS' Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(J)("DocId")) & "' And SubCode = 'PURCH'"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If


                            UpdateChildProgressBar("Updating Purch" & bEntryType & AgL.XNull(DtExternalData_Header.Rows(I)("V_Type")) & "-" & AgL.XNull(DtExternalData_Header.Rows(I)("ManualRefNo")), mChildPrgMaxVal, mChildPrgCnt)
                            FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                        End If

                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                        AgL.ETrans.Rollback()
                    End Try
                End If
            Next
            mChildPrgCnt += 1
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

            Call AgL.LogTableEntry("Data Syncing", Me.Text, "A", AgL.PubMachineName,
                AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), Conn, Cmd,
                mMessage, DglMain(Col1Value, rowDataSyncFromDate).Value,,,,
                AgL.PubSiteCode, AgL.PubDivCode, "", "", "")
        End If
    End Sub
    Private Function FGetCodeFromOMSId(Code As String, Site_Code As String, DtTable As DataTable, PrimaryKeyField As String) As String
        Dim DtRow As DataRow()
        If Site_Code <> "" Then
            If Code = "CASH" Then
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
    Private Function FGetExportSiteCodeFromSiteCode(Site_COde As String) As String
        Dim DtSiteRow As DataRow() = DtSiteMast.Select("Site_Code = '" & Site_COde & "'")
        If DtSiteRow.Length > 0 Then
            FGetExportSiteCodeFromSiteCode = DtSiteRow(0)("Export_Site_Code")
        Else
            FGetExportSiteCodeFromSiteCode = ""
        End If
    End Function
    Private Function FGetExportDivCodeFromDivCode(Div_Code As String) As String
        Dim DtDivRow As DataRow() = DtDivMast.Select("Div_Code = '" & Div_Code & "'")
        If DtDivRow.Length > 0 Then
            FGetExportDivCodeFromDivCode = DtDivRow(0)("Export_Div_Code")
        Else
            FGetExportDivCodeFromDivCode = ""
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

        If IsApplicableImport_SaleInvoice = True Then
            mQry = " Select Sg.Name As BillToPartyName, Sg1.Name As SaleToPartyName_Master,  H.*
                    From SaleInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
                    LEFT JOIN SubGroup Sg1 ON H.SaleToParty = Sg1.SubCode 
                    Where Vt.NCat = '" & Ncat.SaleInvoice & "'"
            mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
            DtExternalData_SaleInvoice = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
        End If

        If IsApplicableImport_SaleReturn = True Then
            mQry = " Select Sg.Name As BillToPartyName, Sg1.Name As SaleToPartyName_Master,  H.*
                    From SaleInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
                    LEFT JOIN SubGroup Sg1 ON H.SaleToParty = Sg1.SubCode 
                    Where Vt.NCat = '" & Ncat.SaleReturn & "'"
            mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
            DtExternalData_SaleReturn = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
        End If

        If IsApplicableImport_PurchInvoice = True Then
            mQry = " Select Sg.Name As BillToPartyName, Sg1.Name As VendorName_Master,  H.*
                    From PurchInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
                    LEFT JOIN SubGroup Sg1 ON H.Vendor = Sg1.SubCode 
                    Where Vt.NCat = '" & Ncat.PurchaseInvoice & "'"
            mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
            DtExternalData_PurchInvoice = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
        End If

        If IsApplicableImport_PurchReturn = True Then
            mQry = " Select Sg.Name As BillToPartyName, Sg1.Name As VendorName_Master,  H.*
                    From PurchInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
                    LEFT JOIN SubGroup Sg1 ON H.Vendor = Sg1.SubCode 
                    Where Vt.NCat = '" & Ncat.PurchaseReturn & "'"
            mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
            DtExternalData_PurchReturn = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
        End If

        If IsApplicableImport_Item = True Then
            mQry = "Select Ic.Description As ItemCategoryDesc, Ig.Description As ItemGroupDesc, I.*
                From Item I
                LEFT JOIN (Select * From Item Where V_Type = 'IC') As Ic On I.ItemCategory = Ic.Code
                LEFT JOIN (Select * From Item Where V_Type = 'IG') As Ig On I.ItemGroup = Ig.Code "
            DtExternalData_Item = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
        End If

        If IsApplicableImport_Catalog = True Then
            mQry = "Select C.* From Catalog C "
            DtExternalData_Catalog = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
        End If


        If IsApplicableImport_SubGroup = True Then
            Dim mPartyQry As String = ""

            mPartyQry = " Select  "
            If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
                mPartyQry += " Case When Sg.SubGroupType In ('Customer','Supplier') Then Sg.Name || ' (' || IfNULL(SM.ShortName,'Branch') || ')' Else Sg.Name End As Name, "
            End If
            mPartyQry += " VReg.SalesTaxNo, VReg.PanNo, VReg.AadharNo, 
                C.CityName, S.Code As State, S.Description As StateName, A.Description As AreaName, Ag.GroupName, Sg.*
                From SubGroup Sg
                LEFT JOIN SiteMast SM On 1 = 1
                LEFT JOIN AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                LEFT JOIN City C ON Sg.CityCode = C.CityCode 
                LEFT JOIN State S ON C.State = S.Code
                LEFT JOIN Area A On Sg.Area = A.Code
                LEFT JOIN (
	                SELECT Sgr.Subcode, 
	                Max(CASE WHEN Sgr.RegistrationType =  'Sales Tax No' THEN Sgr.RegistrationNo ELSE NULL END) AS SalesTaxNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'PAN No' THEN Sgr.RegistrationNo ELSE NULL END) AS PanNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'AADHAR NO' THEN Sgr.RegistrationNo ELSE NULL END) AS AadharNo
	                FROM SubgroupRegistration Sgr 
	                GROUP BY Sgr.Subcode         
                ) AS VReg ON Sg.SubCode = VReg.SubCode "
            DtExternalData_SubGroup = AgL.FillData(mPartyQry, Connection_ExternalDatabase).Tables(0)
        End If

        If IsApplicableImport_LedgerHead = True Then
            mQry = " Select Sg.Name As PartyName_Master,  H.*, Hc.*
                    From LedgerHead H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode
                    LEFT JOIN LedgerHeadCharges Hc On H.DocId = Hc.DocId
                    Where Vt.NCat Not In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') "
            mQry = mQry & " AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
            mQry = mQry & " Order By H.V_Type Desc "
            DtExternalData_LedgerHead = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
        End If

        mParentPrgBarMaxVal = (DtExternalData_Item.Rows.Count +
                            DtExternalData_Catalog.Rows.Count +
                            DtExternalData_SubGroup.Rows.Count +
                            DtExternalData_SaleInvoice.Rows.Count +
                            DtExternalData_SaleReturn.Rows.Count +
                            DtExternalData_PurchInvoice.Rows.Count +
                            DtExternalData_PurchReturn.Rows.Count +
                            DtExternalData_LedgerHead.Rows.Count) * 3

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
    Private Sub FReverseEffectOnHOSite(SearchCode As String, Conn As Object, Cmd As Object)
        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") And AgL.StrCmp(AgL.PubDBName, "Sadhvi") Then
            mQry = " Select Count(*) From LedgerHead With (NoLock) Where GenDocId = '" & SearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) > 0 Then
                mQry = " Delete From Ledger Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & SearchCode & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = " Delete From LedgerHeadDetailCharges Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & SearchCode & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = " Delete From LedgerHeadDetail Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & SearchCode & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = " Delete From LedgerHeadCharges Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & SearchCode & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = " Delete From LedgerHead Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & SearchCode & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = " Select Sg.Nature As SubGroupNature, Vt.NCat, H.*, Hc.* 
                        From LedgerHead H With (NoLock)
                        LEFT JOIN LedgerHeadCharges Hc With (NoLock) On H.DocId = Hc.DocId
                        LEFT JOIN SubGroup Sg With (NoLock) On H.SubCode = Sg.SubCode
                        LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type
                        Where H.DocId = '" & SearchCode & "'"
            Dim DtHeader As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

            If (AgL.StrCmp(AgL.XNull(DtHeader.Rows(0)("NCat")), Ncat.Receipt) Or
                AgL.StrCmp(AgL.XNull(DtHeader.Rows(0)("NCat")), Ncat.Payment)) And
                AgL.StrCmp(AgL.XNull(DtHeader.Rows(0)("SubGroupNature")), "BANK") Then
                Dim bSadhviBranch As String = ""

                If AgL.XNull(DtHeader.Rows(0)("Site_Code")) = "2" Then
                    If AgL.XNull(DtHeader.Rows(0)("Div_Code")) = "E" Then
                        bSadhviBranch = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                        Where Name = '" & SadhviEmbroidery_KanpurBranch & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                    Else
                        bSadhviBranch = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                        Where Name = '" & SadhviEnterprises_KanpurBranch & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                    End If
                ElseIf AgL.XNull(DtHeader.Rows(0)("Site_Code")) = "3" Then
                    If AgL.XNull(DtHeader.Rows(0)("Div_Code")) = "E" Then
                        bSadhviBranch = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                        Where Name = '" & SadhviEmbroidery_BhopalBranch & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                    Else
                        bSadhviBranch = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                        Where Name = '" & SadhviEnterprises_BhopalBranch & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                    End If
                ElseIf AgL.XNull(DtHeader.Rows(0)("Site_Code")) = "4" Then
                    If AgL.XNull(DtHeader.Rows(0)("Div_Code")) = "E" Then
                        bSadhviBranch = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                        Where Name = '" & SadhviEmbroidery_JaunpurBranch & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                    Else
                        bSadhviBranch = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                        Where Name = '" & SadhviEnterprises_JaunpurBranch & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                    End If
                ElseIf AgL.XNull(DtHeader.Rows(0)("Site_Code")) = "5" Then
                    If AgL.XNull(DtHeader.Rows(0)("Div_Code")) = "E" Then
                        bSadhviBranch = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                        Where Name = '" & SadhviEmbroidery_KanpurBranch2 & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                    Else
                        bSadhviBranch = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                        Where Name = '" & SadhviEnterprises_KanpurBranch2 & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                    End If
                End If



                Dim LedgerHeadTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim LedgerHeadTable As New FrmVoucherEntry.StructLedgerHead

                LedgerHeadTable.DocID = ""
                LedgerHeadTable.V_Type = AgL.XNull(DtHeader.Rows(0)("V_Type"))
                LedgerHeadTable.V_Prefix = AgL.XNull(DtHeader.Rows(0)("V_Prefix"))
                LedgerHeadTable.Site_Code = "1"
                LedgerHeadTable.Div_Code = AgL.XNull(DtHeader.Rows(0)("Div_Code"))
                LedgerHeadTable.V_No = 0
                LedgerHeadTable.V_Date = AgL.XNull(DtHeader.Rows(0)("V_Date"))
                LedgerHeadTable.ManualRefNo = ""
                LedgerHeadTable.Subcode = AgL.XNull(DtHeader.Rows(0)("SubCode"))
                LedgerHeadTable.SubcodeName = AgL.XNull(DtHeader.Rows(0)("PartyName"))
                LedgerHeadTable.SalesTaxGroupParty = AgL.XNull(DtHeader.Rows(0)("SalesTaxGroupParty"))
                LedgerHeadTable.PlaceOfSupply = AgL.XNull(DtHeader.Rows(0)("PlaceOfSupply"))
                LedgerHeadTable.StructureCode = AgL.XNull(DtHeader.Rows(0)("Structure"))
                LedgerHeadTable.CustomFields = AgL.XNull(DtHeader.Rows(0)("CustomFields"))
                LedgerHeadTable.PartyDocNo = AgL.XNull(DtHeader.Rows(0)("PartyDocNo"))
                LedgerHeadTable.PartyDocDate = AgL.XNull(DtHeader.Rows(0)("PartyDocDate"))
                LedgerHeadTable.Remarks = AgL.XNull(DtHeader.Rows(0)("Remarks"))
                LedgerHeadTable.Status = "Active"
                LedgerHeadTable.EntryBy = AgL.PubUserName
                LedgerHeadTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                LedgerHeadTable.ApproveBy = ""
                LedgerHeadTable.ApproveDate = ""
                LedgerHeadTable.MoveToLog = ""
                LedgerHeadTable.MoveToLogDate = ""
                LedgerHeadTable.UploadDate = ""
                LedgerHeadTable.OMSId = ""
                LedgerHeadTable.GenDocId = SearchCode
                LedgerHeadTable.LockText = "Synced As Reverse Effect From Other Database."

                LedgerHeadTable.Gross_Amount = AgL.VNull(DtHeader.Rows(0)("Gross_Amount"))
                LedgerHeadTable.Taxable_Amount = AgL.VNull(DtHeader.Rows(0)("Taxable_Amount"))
                LedgerHeadTable.Tax1 = AgL.VNull(DtHeader.Rows(0)("Tax1"))
                LedgerHeadTable.Tax2 = AgL.VNull(DtHeader.Rows(0)("Tax2"))
                LedgerHeadTable.Tax3 = AgL.VNull(DtHeader.Rows(0)("Tax3"))
                LedgerHeadTable.Tax4 = AgL.VNull(DtHeader.Rows(0)("Tax4"))
                LedgerHeadTable.Tax5 = AgL.VNull(DtHeader.Rows(0)("Tax5"))
                LedgerHeadTable.SubTotal1 = AgL.VNull(DtHeader.Rows(0)("SubTotal1"))
                LedgerHeadTable.Other_Charge = AgL.VNull(DtHeader.Rows(0)("Other_Charge"))
                LedgerHeadTable.Deduction = AgL.VNull(DtHeader.Rows(0)("Deduction"))
                LedgerHeadTable.Round_Off = AgL.VNull(DtHeader.Rows(0)("Round_Off"))
                LedgerHeadTable.Net_Amount = AgL.VNull(DtHeader.Rows(0)("Net_Amount"))

                mQry = " SELECT Sg1.Name As SubCodeName, Sg2.Name As LinkedSubCodeName, L.*, Lc.*
                            FROM LedgerHeadDetail L With (NoLock)
                            LEFT JOIN LedgerHeadDetailCharges Lc With (NoLock) On L.DocId = Lc.DocId And L.Sr = Lc.Sr
                            LEFT JOIN SubGroup Sg1 With (NoLock) On L.SubCode = Sg1.SubCode
                            LEFT JOIN SubGroup Sg2 With (NoLock) On L.LinkedSubCode = Sg2.SubCode
                            Where L.DocId = '" & AgL.XNull(DtHeader.Rows(0)("DocId")) & "'"
                Dim DtLine As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

                For J As Integer = 0 To DtLine.Rows.Count - 1
                    LedgerHeadTable.Line_Sr = AgL.XNull(DtLine.Rows(J)("Sr"))
                    LedgerHeadTable.Line_SubCode = bSadhviBranch
                    LedgerHeadTable.Line_SubCodeName = ""
                    LedgerHeadTable.Line_LinkedSubCode = ""
                    LedgerHeadTable.Line_LinkedSubCodeName = ""
                    LedgerHeadTable.Line_Specification = AgL.XNull(DtLine.Rows(J)("Specification"))
                    LedgerHeadTable.Line_SalesTaxGroupItem = AgL.XNull(DtLine.Rows(J)("SalesTaxGroupItem"))
                    LedgerHeadTable.Line_Qty = AgL.VNull(DtLine.Rows(J)("Qty"))
                    LedgerHeadTable.Line_Unit = AgL.XNull(DtLine.Rows(J)("Unit"))
                    LedgerHeadTable.Line_Rate = AgL.VNull(DtLine.Rows(J)("Rate"))
                    LedgerHeadTable.Line_Amount = AgL.VNull(DtLine.Rows(J)("Amount"))
                    LedgerHeadTable.Line_ChqRefNo = AgL.XNull(DtLine.Rows(J)("ChqRefNo"))
                    LedgerHeadTable.Line_ChqRefDate = AgL.XNull(DtLine.Rows(J)("ChqRefDate"))
                    LedgerHeadTable.Line_Remarks = AgL.XNull(DtLine.Rows(J)("Remarks"))
                    LedgerHeadTable.Line_OMSId = ""

                    LedgerHeadTable.Line_Gross_Amount = AgL.VNull(DtLine.Rows(J)("Gross_Amount"))
                    LedgerHeadTable.Line_Taxable_Amount = AgL.VNull(DtLine.Rows(J)("Taxable_Amount"))
                    LedgerHeadTable.Line_Tax1_Per = AgL.VNull(DtLine.Rows(J)("Tax1_Per"))
                    LedgerHeadTable.Line_Tax1 = AgL.VNull(DtLine.Rows(J)("Tax1"))
                    LedgerHeadTable.Line_Tax2_Per = AgL.VNull(DtLine.Rows(J)("Tax2_Per"))
                    LedgerHeadTable.Line_Tax2 = AgL.VNull(DtLine.Rows(J)("Tax2"))
                    LedgerHeadTable.Line_Tax3_Per = AgL.VNull(DtLine.Rows(J)("Tax3_Per"))
                    LedgerHeadTable.Line_Tax3 = AgL.VNull(DtLine.Rows(J)("Tax3"))
                    LedgerHeadTable.Line_Tax4_Per = AgL.VNull(DtLine.Rows(J)("Tax4_Per"))
                    LedgerHeadTable.Line_Tax4 = AgL.VNull(DtLine.Rows(J)("Tax4"))
                    LedgerHeadTable.Line_Tax5_Per = AgL.VNull(DtLine.Rows(J)("Tax5_Per"))
                    LedgerHeadTable.Line_Tax5 = AgL.VNull(DtLine.Rows(J)("Tax5"))
                    LedgerHeadTable.Line_SubTotal1 = AgL.VNull(DtLine.Rows(J)("SubTotal1"))
                    LedgerHeadTable.Line_Other_Charge = AgL.VNull(DtLine.Rows(J)("Other_Charge"))
                    LedgerHeadTable.Line_Deduction = AgL.VNull(DtLine.Rows(J)("Deduction"))
                    LedgerHeadTable.Line_Round_Off = AgL.VNull(DtLine.Rows(J)("Round_Off"))
                    LedgerHeadTable.Line_Net_Amount = AgL.VNull(DtLine.Rows(J)("Net_Amount"))

                    LedgerHeadTableList(UBound(LedgerHeadTableList)) = LedgerHeadTable
                    ReDim Preserve LedgerHeadTableList(UBound(LedgerHeadTableList) + 1)
                Next
                FrmVoucherEntry.InsertLedgerHead(LedgerHeadTableList)
            End If
        End If
    End Sub
    Private Sub FPostReverEffectInBranchSite(SearchCode As String, Conn As Object, Cmd As Object)
        Dim dtLine As DataTable
        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") And AgL.StrCmp(AgL.PubDBName, "SADHVI") Then
            mQry = " Select Sg.Nature As SubGroupNature, Vt.NCat, H.*, Hc.* 
                    From LedgerHead H With (NoLock)
                    LEFT JOIN LedgerHeadCharges Hc With (NoLock) On H.DocId = Hc.DocId
                    LEFT JOIN SubGroup Sg With (NoLock) On H.SubCode = Sg.SubCode
                    LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type
                    Where H.DocId = '" & SearchCode & "'"
            Dim DtHeader As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

            If (AgL.XNull(DtHeader.Rows(0)("NCat")) = Ncat.Receipt Or AgL.XNull(DtHeader.Rows(0)("NCat")) = Ncat.VisitReceipt Or
                AgL.XNull(DtHeader.Rows(0)("NCat")) = Ncat.Payment) And AgL.XNull(DtHeader.Rows(0)("SubGroupNature")).ToString.ToUpper = "BANK" Then
                Dim bSadhviHO As String = ""

                If AgL.XNull(DtHeader.Rows(0)("Div_Code")) = "E" Then
                    bSadhviHO = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                            Where Name = 'SADHVI EMBROIDERY (Branch)'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                Else
                    bSadhviHO = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                            Where Name = 'SADHVI ENTERPRISES (Branch)'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                End If

                mQry = "Select Sr From LedgerHeadDetail with (NoLock) Where DocId = '" & SearchCode & "'"
                dtLine = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
                If dtLine.Rows.Count > 0 Then
                    For I As Integer = 0 To dtLine.Rows.Count - 1
                        Dim mMaxSr As Integer = AgL.VNull(AgL.Dman_Execute("Select Max(V_SNo) As V_SNo From Ledger With (NoLock)
                            Where DocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())

                        Dim mDebitAmount As String = ""
                        Dim mCreditAmount As String = ""

                        If AgL.XNull(DtHeader.Rows(0)("NCat")) = Ncat.Payment Then
                            mDebitAmount = " 0 "
                            mCreditAmount = " Sum(L.Amount) "
                        Else
                            mDebitAmount = " Sum(L.Amount) "
                            mCreditAmount = " 0 "
                        End If

                        mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                        AmtDr, AmtCr, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                        SELECT H.DocId, " & mMaxSr + 1 & " AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                        Max(H.V_Date) AS V_Date, '" & bSadhviHO & "' AS SubCode, Max(H.SubCode) AS ContraSub, 
                        " & mDebitAmount & " AS AmtDr, " & mCreditAmount & " AS AmtCr, 'Being Payment Transfered To HO' AS Narration, 
                        Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                        Max(H.ManualRefNo) AS RecId
                        FROM LedgerHead H With (NoLock)
                        LEFT JOIN LedgerHeadDetail L With (NoLock) ON H.DocID = L.DocID
                        WHERE H.DocId = '" & SearchCode & "' And L.Sr = " & AgL.VNull(dtLine.Rows(I)("Sr")) & "
                        GROUP BY H.DocID	
                        UNION ALL
                        SELECT H.DocId, " & mMaxSr + 2 & " AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                        Max(H.V_Date) AS V_Date, Max(H.SubCode) AS SubCode, '" & bSadhviHO & "' AS ContraSub, 
                        " & mCreditAmount & " AS AmtDr, " & mDebitAmount & " AS AmtCr, 'Being Payment Transfered To HO' AS Narration, 
                        Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                        Max(H.ManualRefNo) AS RecId
                        FROM LedgerHead H With (NoLock)
                        LEFT JOIN LedgerHeadDetail L With (NoLock) ON H.DocID = L.DocID
                        WHERE H.DocId = '" & SearchCode & "' And L.Sr = " & AgL.VNull(dtLine.Rows(I)("Sr")) & "
                        GROUP BY H.DocID "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Next I
                End If
            End If
        End If
    End Sub
    Private Sub FLinkVisitReceiptAndCashReceiptAccordingToBranch(SearchCode, ExternalDocId, Conn, Cmd)
        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") And AgL.StrCmp(AgL.PubDBName, "SADHVI") Then
            mQry = " Select Sg.Nature As SubGroupNature, Vt.NCat, H.*, Hc.* 
                    From LedgerHead H With (NoLock)
                    LEFT JOIN LedgerHeadCharges Hc With (NoLock) On H.DocId = Hc.DocId
                    LEFT JOIN SubGroup Sg With (NoLock) On H.SubCode = Sg.SubCode
                    LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type
                    Where H.DocId = '" & SearchCode & "'"
            Dim DtHeader As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

            If AgL.StrCmp(AgL.XNull(DtHeader.Rows(0)("V_Type")), "CR") And AgL.XNull(DtHeader.Rows(0)("SubGroupNature")).ToString.ToUpper = "CASH" Then
                mQry = " Select L.* From Ledger L Where L.DocId = '" & ExternalDocId & "'"
                Dim DtExternalData_Ledger As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                Dim bReferenceDocId As String = ""
                bReferenceDocId = AgL.XNull(AgL.Dman_Execute(" Select DocId 
                    From LedgerHead With (NoLock)
                    Where OMSId = '" & DtExternalData_Ledger.Rows(0)("ReferenceDocId") & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                If bReferenceDocId <> "" Then
                    mQry = " UPDATE Ledger Set ReferenceDocId = '" & bReferenceDocId & "', ReferenceDocIDSr = 1 Where DocId = '" & SearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = " UPDATE LedgerHeadDetail Set ReferenceDocId = '" & bReferenceDocId & "', ReferenceDocIDSr = 1 Where DocId = '" & SearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        End If
    End Sub
    Private Sub FDeleteSale(DtExternalData_Header As DataTable, mNCat As String)
        Dim mEntryChanged As Boolean = False

        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0

        Dim DtLocalData_Header As DataTable

        Dim bEntryType As String = ""
        If DtExternalData_Header.Rows.Count > 0 Then
            mQry = " Select NCat From Voucher_Type With (NoLock) Where V_Type = '" & AgL.XNull(DtExternalData_Header.Rows(0)("V_Type")) & "'"
            Dim bNCat As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
            If bNCat = Ncat.SaleInvoice Then
                bEntryType = " Invoice "
            ElseIf bNCat = Ncat.SaleReturn Then
                bEntryType = " Return "
            ElseIf bNCat = Ncat.SaleOrder Then
                bEntryType = " Order "
            End If
        End If


        Dim DtSiteAndDivisionsAndV_Type As DataTable = DtExternalData_Header.DefaultView.ToTable(True, "Site_Code", "Div_Code", "V_Type")

        Dim mSite_CodeStr As String = ""
        Dim mDiv_CodeStr As String = ""
        Dim mV_TypeStr As String = ""

        For I As Integer = 0 To DtSiteAndDivisionsAndV_Type.Rows.Count - 1
            If mSite_CodeStr <> "" Then mSite_CodeStr += ","
            mSite_CodeStr += AgL.Chk_Text(FGetExportSiteCodeFromSiteCode(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Site_Code"))))
            If mDiv_CodeStr <> "" Then mDiv_CodeStr += ","
            'mDiv_CodeStr += AgL.Chk_Text(FGetExportDivCodeFromDivCode(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Div_Code"))))
            mDiv_CodeStr += AgL.Chk_Text(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Div_Code")))
            If mV_TypeStr <> "" Then mV_TypeStr += ","
            mV_TypeStr += AgL.Chk_Text(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("V_Type")))
        Next


        mQry = " Select H.* From SaleInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where H.OMSId Is Not Null And Vt.NCat = '" & mNCat & "' "
        If mSite_CodeStr <> "" Then mQry += " And H.Site_Code In (" & mSite_CodeStr & ")"
        If mDiv_CodeStr <> "" Then mQry += " And H.Div_Code In (" & mDiv_CodeStr & ")"
        If mV_TypeStr <> "" Then mQry += " And H.V_Type In (" & mV_TypeStr & ") "
        mQry = mQry & " And Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
        DtLocalData_Header = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)


        mChildPrgCnt = 0
        mChildPrgMaxVal = DtLocalData_Header.Rows.Count
        For I As Integer = 0 To DtLocalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Deleting Sale" & bEntryType & " which are deleted from source database.", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking Sale" & bEntryType & AgL.XNull(DtLocalData_Header.Rows(I)("V_Type")) + "-" + AgL.XNull(DtLocalData_Header.Rows(I)("ManualRefNo")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtExternalData_Header.Select(" DocId = '" + AgL.XNull(DtLocalData_Header.Rows(I)("OMSId")) + "'").Length = 0 Then
                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    UpdateChildProgressBar("Deleting Sale" & bEntryType & AgL.XNull(DtLocalData_Header.Rows(I)("V_Type")) & "-" & AgL.XNull(DtLocalData_Header.Rows(I)("ManualRefNo")), mChildPrgMaxVal, mChildPrgCnt)

                    Dim mErrorMsg As String = FDataValidationForDeletion(AgL.XNull(DtLocalData_Header.Rows(I)("DocId")), "SaleInvoice", AgL.GCn, AgL.ECmd)
                    If mErrorMsg <> "" Then
                        Err.Raise(1, "", mErrorMsg)
                    End If

                    mQry = " Delete From SaleInvoiceTrnSetting Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoicePayment Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoiceDimensionDetailSku Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoiceDimensionDetail Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoiceBarcodeLastTransactionValues Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoiceDetailSku Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoiceTransport Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoiceDetailHelpValues Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoiceDetail Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From SaleInvoice Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                End Try
            End If
            mChildPrgCnt += 1
        Next
    End Sub
    Private Sub FDeletePurch(DtExternalData_Header As DataTable, mNCat As String)
        Dim mEntryChanged As Boolean = False

        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0

        Dim DtLocalData_Header As DataTable

        Dim bEntryType As String = ""
        If DtExternalData_Header.Rows.Count > 0 Then
            mQry = " Select NCat From Voucher_Type Where V_Type = '" & AgL.XNull(DtExternalData_Header.Rows(0)("V_Type")) & "'"
            Dim bNCat As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
            If bNCat = Ncat.PurchaseInvoice Then
                bEntryType = " Invoice "
            ElseIf bNCat = Ncat.PurchaseReturn Then
                bEntryType = " Return "
            ElseIf bNCat = Ncat.PurchaseOrder Then
                bEntryType = " Order "
            End If
        End If


        Dim DtSiteAndDivisionsAndV_Type As DataTable = DtExternalData_Header.DefaultView.ToTable(True, "Site_Code", "Div_Code", "V_Type")

        Dim mSite_CodeStr As String = ""
        Dim mDiv_CodeStr As String = ""
        Dim mV_TypeStr As String = ""

        For I As Integer = 0 To DtSiteAndDivisionsAndV_Type.Rows.Count - 1
            If mSite_CodeStr <> "" Then mSite_CodeStr += ","
            mSite_CodeStr += AgL.Chk_Text(FGetExportSiteCodeFromSiteCode(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Site_Code"))))
            If mDiv_CodeStr <> "" Then mDiv_CodeStr += ","
            'mDiv_CodeStr += AgL.Chk_Text(FGetExportDivCodeFromDivCode(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Div_Code"))))
            mDiv_CodeStr += AgL.Chk_Text(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Div_Code")))
            If mV_TypeStr <> "" Then mV_TypeStr += ","
            mV_TypeStr += AgL.Chk_Text(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("V_Type")))
        Next


        mQry = " Select H.* From PurchInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where H.OMSId Is Not Null And Vt.NCat = '" & mNCat & "' "
        If mSite_CodeStr <> "" Then mQry += " And H.Site_Code In (" & mSite_CodeStr & ")"
        If mDiv_CodeStr <> "" Then mQry += " And H.Div_Code In (" & mDiv_CodeStr & ")"
        If mV_TypeStr <> "" Then mQry += " And H.V_Type In (" & mV_TypeStr & ") "
        mQry = mQry & " And Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
        DtLocalData_Header = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)


        mChildPrgCnt = 0
        mChildPrgMaxVal = DtLocalData_Header.Rows.Count
        For I As Integer = 0 To DtLocalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Deleting Purch" & bEntryType & " which are deleted from source database.", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking Purch" & bEntryType & AgL.XNull(DtLocalData_Header.Rows(I)("V_Type")) + "-" + AgL.XNull(DtLocalData_Header.Rows(I)("ManualRefNo")) + " exists Or Not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtExternalData_Header.Select(" DocId = '" + AgL.XNull(DtLocalData_Header.Rows(I)("OMSId")) + "'").Length = 0 Then
                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    UpdateChildProgressBar("Deleting Purch" & bEntryType & AgL.XNull(DtLocalData_Header.Rows(I)("V_Type")) & "-" & AgL.XNull(DtLocalData_Header.Rows(I)("ManualRefNo")), mChildPrgMaxVal, mChildPrgCnt)

                    Dim mErrorMsg As String = FDataValidationForDeletion(AgL.XNull(DtLocalData_Header.Rows(I)("DocId")), "PurchInvoice", AgL.GCn, AgL.ECmd)
                    If mErrorMsg <> "" Then
                        Err.Raise(1, "", mErrorMsg)
                    End If

                    mQry = " Delete From StockProcess Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From PurchInvoicePayment Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From PurchInvoiceDimensionDetailSku Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From PurchInvoiceDimensionDetail Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From PurchInvoiceDetailSku Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From PurchInvoiceTransport Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From PurchInvoiceDetail Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From PurchInvoice Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                End Try
            End If
            mChildPrgCnt += 1
        Next
    End Sub
    Private Sub FDeleteLedgerHead(DtExternalData_Header As DataTable)
        Dim mEntryChanged As Boolean = False

        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0

        Dim DtLocalData_Header As DataTable

        Dim DtSiteAndDivisionsAndV_Type As DataTable = DtExternalData_Header.DefaultView.ToTable(True, "Site_Code", "Div_Code", "V_Type")

        Dim mSite_CodeStr As String = ""
        Dim mDiv_CodeStr As String = ""
        Dim mV_TypeStr As String = ""

        For I As Integer = 0 To DtSiteAndDivisionsAndV_Type.Rows.Count - 1
            If mSite_CodeStr <> "" Then mSite_CodeStr += ","
            mSite_CodeStr += AgL.Chk_Text(FGetExportSiteCodeFromSiteCode(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Site_Code"))))
            If mDiv_CodeStr <> "" Then mDiv_CodeStr += ","
            'mDiv_CodeStr += AgL.Chk_Text(FGetExportDivCodeFromDivCode(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Div_Code"))))
            mDiv_CodeStr += AgL.Chk_Text(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("Div_Code")))
            If mV_TypeStr <> "" Then mV_TypeStr += ","
            mV_TypeStr += AgL.Chk_Text(AgL.XNull(DtSiteAndDivisionsAndV_Type.Rows(I)("V_Type")))
        Next

        mQry = " Select H.* From LedgerHead H 
                Where OMSId Is Not Null "
        If mSite_CodeStr <> "" Then mQry += " And Site_Code In (" & mSite_CodeStr & ")"
        If mDiv_CodeStr <> "" Then mQry += " And Div_Code In (" & mDiv_CodeStr & ")"
        If mV_TypeStr <> "" Then mQry += " And V_Type In (" & mV_TypeStr & ") "
        mQry = mQry & " And Date(H.V_Date) >= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowDataSyncFromDate).Value).ToString("s")) & ""
        DtLocalData_Header = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)




        mChildPrgCnt = 0
        mChildPrgMaxVal = DtLocalData_Header.Rows.Count
        For I As Integer = 0 To DtLocalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Deleting Ledger Heads" & " which are deleted from source database.", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking " & AgL.XNull(DtLocalData_Header.Rows(I)("V_Type")) + "-" + AgL.XNull(DtLocalData_Header.Rows(I)("ManualRefNo")) + " exists Or Not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtExternalData_Header.Select(" DocId = '" + AgL.XNull(DtLocalData_Header.Rows(I)("OMSId")) + "'").Length = 0 Then
                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    UpdateChildProgressBar("Deleting " & AgL.XNull(DtLocalData_Header.Rows(I)("V_Type")) & "-" & AgL.XNull(DtLocalData_Header.Rows(I)("ManualRefNo")), mChildPrgMaxVal, mChildPrgCnt)

                    Dim mErrorMsg As String = FDataValidationForDeletion(AgL.XNull(DtLocalData_Header.Rows(I)("DocId")), "LedgerHead", AgL.GCn, AgL.ECmd)
                    If mErrorMsg <> "" Then
                        Err.Raise(1, "", mErrorMsg)
                    End If

                    mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From LedgerHeadDetailCharges Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From LedgerHeadDetail Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From LedgerHeadCharges Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = " Delete From LedgerHead Where DocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
                        mQry = " Select Count(*) From LedgerHead With (NoLock) Where GenDocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "'"
                        If AgL.VNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) > 0 Then
                            mQry = " Delete From Ledger Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "')"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            mQry = " Delete From LedgerHeadDetailCharges Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "')"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            mQry = " Delete From LedgerHeadDetail Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "')"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            mQry = " Delete From LedgerHeadCharges Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "')"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            mQry = " Delete From LedgerHead Where DocId In (Select DocId From LedgerHead H Where GenDocId = '" & AgL.XNull(DtLocalData_Header.Rows(I)("DocId")) & "')"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If
                    End If

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
                    AgL.ETrans.Rollback()
                End Try
            End If
            mChildPrgCnt += 1
        Next
    End Sub
    Private Function FDataValidationForDeletion(SearchCode As String, TableName As String, Conn As Object, Cmd As Object) As String
        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
            If AgL.XNull(AgL.Dman_Execute(" Select Site_Code From " & TableName & " With (NoLock)
                    Where DocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()) = "1" Then
                FDataValidationForDeletion = "It is trying to delete main branch record. Task is aborted by system."
            End If
        End If
    End Function
    Private Sub FGetBranchItemRateForSadhvi()
        Connection_ExternalDatabase.Open()

        mQry = " Select Code, Description, PurchaseRate From Item "
        Dim DtExternalData_ItemRate As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

        mQry = " Delete From ItemBranchRate "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I As Integer = 0 To DtExternalData_ItemRate.Rows.Count - 1
            mQry = " INSERT INTO ItemBranchRate(Code, Description, PurchaseRate)
                Values(" & AgL.Chk_Text(AgL.XNull(DtExternalData_ItemRate.Rows(I)("Code"))) & ",
                " & AgL.Chk_Text(AgL.XNull(DtExternalData_ItemRate.Rows(I)("Description"))) & ",
                " & Val(AgL.VNull(DtExternalData_ItemRate.Rows(I)("PurchaseRate"))) & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next
    End Sub













    Public Sub FAddCatalog(DtExternalData_Header As DataTable)
        Dim mChildPrgCnt As Integer = 0
        Dim mChildPrgMaxVal As Integer = 0

        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        Dim bLastCatalogCode As String = AgL.GetMaxId("Catalog", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mChildPrgCnt = 0
        mChildPrgMaxVal = DtExternalData_Header.Rows.Count

        For I = 0 To DtExternalData_Header.Rows.Count - 1
            UpdateParentProgressBar("Inserting Catalogs", mParentPrgBarMaxVal)
            UpdateChildProgressBar("Checking " + AgL.XNull(DtExternalData_Header.Rows(I)("Description")) + " exists or not.", mChildPrgMaxVal, mChildPrgCnt)
            If DtCatalog.Select("OMSId = '" & AgL.XNull(DtExternalData_Header.Rows(I)("Code")) & "'").Length = 0 Then
                If AgL.XNull(DtExternalData_Header.Rows(I)("Description")) <> "" Then
                    Dim CatalogTableList(0) As FrmCatalog.StructCatalog
                    Dim CatalogTable As New FrmCatalog.StructCatalog

                    CatalogTable.Code = AgL.GetMaxId("Catalog", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                    CatalogTable.Specification = AgL.XNull(DtExternalData_Header.Rows(I)("Specification"))
                    CatalogTable.Description = AgL.XNull(DtExternalData_Header.Rows(I)("Description"))
                    CatalogTable.Site_Code = AgL.XNull(DtExternalData_Header.Rows(I)("Site_Code"))
                    CatalogTable.EntryBy = AgL.XNull(DtExternalData_Header.Rows(I)("EntryBy"))
                    CatalogTable.EntryDate = AgL.XNull(DtExternalData_Header.Rows(I)("EntryDate"))
                    CatalogTable.EntryType = AgL.XNull(DtExternalData_Header.Rows(I)("EntryType"))
                    CatalogTable.EntryStatus = AgL.XNull(DtExternalData_Header.Rows(I)("EntryStatus"))
                    CatalogTable.Status = AgL.XNull(DtExternalData_Header.Rows(I)("Status"))
                    CatalogTable.Div_Code = AgL.XNull(DtExternalData_Header.Rows(I)("Div_Code"))
                    CatalogTable.UID = AgL.XNull(DtExternalData_Header.Rows(I)("UID"))
                    CatalogTable.OmsId = AgL.XNull(DtExternalData_Header.Rows(I)("Code"))
                    CatalogTable.UploadDate = AgL.XNull(DtExternalData_Header.Rows(I)("UploadDate"))

                    mQry = " Select * From CatalogDetail Where Code = '" & DtExternalData_Header.Rows(I)("Code") & "'"
                    Dim DtCatalogDetailSource_ForHeader As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)

                    For J As Integer = 0 To DtCatalogDetailSource_ForHeader.Rows.Count - 1
                        CatalogTable.Line_Sr = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Sr"))
                        CatalogTable.Line_ItemCode = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Item"))
                        CatalogTable.Line_ItemName = ""
                        CatalogTable.Line_Qty = AgL.VNull(DtCatalogDetailSource_ForHeader.Rows(J)("Qty"))
                        CatalogTable.Line_Unit = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Unit"))
                        CatalogTable.Line_Rate = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Rate"))
                        CatalogTable.Line_DiscountPer = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("DiscountPer"))
                        CatalogTable.Line_AdditionalDiscountPer = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("AdditionalDiscountPer"))
                        CatalogTable.Line_AdditionPer = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("AdditionPer"))
                        CatalogTable.Line_ItemStateCode = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("ItemState"))
                        CatalogTable.Line_ItemStateName = ""
                        CatalogTable.Line_OMSId = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Code")) + AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Sr"))

                        CatalogTableList(UBound(CatalogTableList)) = CatalogTable
                        ReDim Preserve CatalogTableList(UBound(CatalogTableList) + 1)
                    Next
                    UpdateChildProgressBar("Inserting Catalogs " + CatalogTable.Description, mChildPrgMaxVal, mChildPrgCnt)

                    FrmCatalog.InsertCatalog(CatalogTableList)

                    FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
                    mChildPrgCnt += 1
                End If
            End If
        Next
        FLoadCatalog()
    End Sub
End Class

