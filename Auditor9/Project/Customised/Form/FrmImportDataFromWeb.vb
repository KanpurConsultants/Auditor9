Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.FrmSaleInvoiceDirect_WithDimension
Public Class FrmImportDataFromWeb
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

    Dim BranchSupplierNameENTERPRISES As String = ""
    Dim BranchSupplierNameEMBROIDERY As String = ""

    Public Const Col1Head As String = "Head"
    Public Const Col1Status As String = "Status"
    Public Const Col1Message As String = "Message"

    Dim rowDataSyncFromDate As Integer = 0
    Public Const hcDataSyncFromDate As String = "Data Sync From Date"


    Dim Export_Site_Code As String
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
        Export_Site_Code = AgL.PubSiteCode
        mQry = " Select Value From Status Where FieldName = '" & ClsMain.StatusFields.DataSyncedTillDate & "' AND Site_Code = '" & Export_Site_Code & "'"
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
        Dim SiteCodeToSync As String = ""
        Dim IsValidDatabase As String = ""


        If AgL.XNull(DglMain.Item(Col1Value, rowDataSyncFromDate).Value) = "" Then
            MsgBox("Date is required.", MsgBoxStyle.Information)
            Exit Sub
        End If


        DatabaseName = Connection_ExternalDatabase.ConnectionString

        If AgL.StrCmp(AgL.PubDBName, "Sadhvi2") Then
            IsValidDatabase = "Yes"
            'BranchSupplierNameENTERPRISES = "SADHVI ENTERPRISES (Branch)"
            'BranchSupplierNameEMBROIDERY = "SADHVI EMBROIDERY (Branch)"
        End If



        UpdateChildProgressBar("Initializing...", 1, 0)

        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then
            If IsValidDatabase = "Yes" Then
                IsApplicableImport_Item = True
                'IsApplicableImport_Catalog = False
                'IsApplicableImport_SubGroup = True
                'IsApplicableImport_SaleInvoice = True
                'IsApplicableImport_SaleReturn = True
                'IsApplicableImport_PurchInvoice = True
                'IsApplicableImport_PurchReturn = True
                'IsApplicableImport_LedgerHead = True
            Else
                MsgBox("Wrong File.", MsgBoxStyle.Information)
                Exit Sub
            End If

        End If




        'mQry = "SELECT Distinct L.Site_Code FROM Ledger L "
        'Dim DtSiteList As DataTable = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)






        If IsApplicableImport_Item = True Then
            'FUpdateItem(DtExternalData_Item)

            'mQry = "Select Ic.Description As ItemCategoryDesc, Ig.Description As ItemGroupDesc, I.*
            '    From Item I
            '    LEFT JOIN (Select * From Item Where V_Type = 'IC') As Ic On I.ItemCategory = Ic.Code
            '    LEFT JOIN (Select * From Item Where V_Type = 'IG') As Ig On I.ItemGroup = Ig.Code "
            ' DtExternalData_Item = AgL.FillData(mQry, Connection_ExternalDatabase).Tables(0)
            mQry = "SELECT 'SW_ProductGroup-' + Convert(NVARCHAR,P.ProductGroupID) AS ProductGroupCode,'SW_Product-' + Convert(NVARCHAR,P.ProductID) AS Code, P.ProductCode AS ManualCode,P.ProductDescription AS DisplayName, P.ProductDescription AS Description, 'Pcs' Unit, 0 DealQty, 'Pcs' DealUnit, PT.ProductTypeName As ItemCategoryDesc, PG.ProductGroupName As ItemGroupDesc, 'TP' AS ItemType, P.StandardCost AS PurchaseRate, 0 AS  Rate, 
                    EntryBy, EntryDate, MoveToLog, MoveToLogDate, 'Active' AS Status, Div_Code, 'GST 5%' SalesTaxPostingGroup, Specification, StockYN, Gross_Weight, 0 IsSystemDefine, IsRestricted_InTransaction, IsMandatory_UnitConversion, '540752' AS HSN, PU.ProductUidName AS BarcodeDesc, 'SW_ProductUID-' + Convert(NVARCHAR,PU.ProductUIDId)  AS BarcodeId, ShowItemInOtherDivisions, MRP, DiscountPerPurchase, DiscountPerSale, AdditionPerSale, MaintainStockYn, 'ITEM' AS V_Type, Default_DiscountPerSale, Default_AdditionalDiscountPerSale, Default_AdditionPerSale, Default_DiscountPerPurchase, Default_AdditionalDiscountPerPurchase, Default_MarginPer, 
                    'Fixed' AS BarcodeType, 'Auto' AS BarcodePattern, Default_AdditionPerPurchase, ShowItemInOtherSites, Site_Code, LockText, '' OmsId, IsNewItemAllowedPurch, IsNewDimension1AllowedPurch, IsNewDimension2AllowedPurch, IsNewDimension3AllowedPurch, IsNewDimension4AllowedPurch, Addition, Loss, ShowDimensionDetailInPurchase, ShowDimensionDetailInSales, IsLotApplicable, IsStockInPcsApplicable, LossQtyPer, LossQty, LossDealQtyPer, LossDealQty
                    FROM [SadhviW].Web.Products P
                    LEFT JOIN Item I ON I.OMSId = 'SW_Product-' + Convert(NVARCHAR,P.ProductID)
                    LEFT JOIN [SadhviW].Web.ProductGroups PG ON P.ProductGroupID = PG.ProductGroupID
                    LEFT JOIN [SadhviW].Web.ProductTypes PT ON PT.ProductTypeId = PG.ProductTypeId
                    LEFT JOIN [SadhviW].Web.ProductUids PU ON PU.ProductId = P.ProductID
                    WHERE I.Code IS NULL "
            DtExternalData_Item = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            FAddItem(DtExternalData_Item)
            'FLoadItem()
        End If



        If bIsMastersImportedSuccessfully = False Then
            FRecordMessage("Completed", "Error", "Some masters are not synced successfully, that's why can't process transactions.", AgL.GCn, AgL.ECmd)
            UpdateChildProgressBar(" ", 1, 0)
            UpdateParentProgressBar(" ", 1)
            MsgBox("Process Completed With Error....", MsgBoxStyle.Information)
        End If



        If bIsSaleInvoicesImportedSuccessfully = False Then
            FRecordMessage("Completed", "Error", "Some Sale Invoice are not synced successfully, that's why can't process Sale Returns.", AgL.GCn, AgL.ECmd)
            UpdateChildProgressBar(" ", 1, 0)
            UpdateParentProgressBar(" ", 1)
            MsgBox("Process Completed With Error....", MsgBoxStyle.Information)
            Exit Sub
        End If



        Dim mCode As String = AgL.GetMaxId("Status", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        If AgL.VNull(AgL.Dman_Execute("Select count(*) From Status Where FieldName = '" & ClsMain.StatusFields.DataSyncedTillDate & "'  AND Site_Code = '" & Export_Site_Code & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = " Insert Into Status(Code, FieldName, Site_Code, Value)
                Select '" & mCode & "', '" & ClsMain.StatusFields.DataSyncedTillDate & "', '" & Export_Site_Code & "',
                " & AgL.Chk_Date(AgL.PubLoginDate) & " "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE Status Set Value = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                    Where FieldName = '" & ClsMain.StatusFields.DataSyncedTillDate & "' AND Site_Code = '" & Export_Site_Code & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") Then

        End If

        UpdateChildProgressBar(" ", 1, 0)
        UpdateParentProgressBar(" ", 1)
        MsgBox("Process Completed Successfully...", MsgBoxStyle.Information)
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

        'For I = 0 To DtItemCategory.Rows.Count - 1
        '    If AgL.XNull(DtItemCategory.Rows(I)("ItemCategoryDesc")) <> "" Then
        '        Dim ItemCategoryTable As New FrmItemMaster.StructItemCategory
        '        Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

        '        ItemCategoryTable.Code = bItemCategoryCode
        '        ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)("ItemCategoryDesc"))
        '        ItemCategoryTable.ItemType = ItemTypeCode.TradingProduct
        '        ItemCategoryTable.SalesTaxPostingGroup = "GST 0%"
        '        ItemCategoryTable.Unit = "Nos"
        '        ItemCategoryTable.EntryBy = AgL.PubUserName
        '        ItemCategoryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
        '        ItemCategoryTable.EntryType = "Add"
        '        ItemCategoryTable.LockText = "Synced From Other Database."
        '        ItemCategoryTable.EntryStatus = LogStatus.LogOpen
        '        ItemCategoryTable.Div_Code = AgL.PubDivCode
        '        ItemCategoryTable.Status = "Active"

        '        Try
        '            AgL.ECmd = AgL.GCn.CreateCommand
        '            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        '            AgL.ECmd.Transaction = AgL.ETrans
        '            mTrans = "Begin"
        '            FrmItemMaster.ImportItemCategoryTable(ItemCategoryTable)

        '            UpdateChildProgressBar("Inserting Item Category " + ItemCategoryTable.Description, mChildPrgMaxVal, mChildPrgCnt)

        '            FRecordMessage(LblChildProgress.Text, "Success.", "", AgL.GCn, AgL.ECmd)
        '            mChildPrgCnt += 1

        '            AgL.ETrans.Commit()
        '            mTrans = "Commit"
        '        Catch ex As Exception
        '            FRecordMessage(LblChildProgress.Text, "Error", ex.Message, AgL.GCn, AgL.ECmd)
        '            AgL.ETrans.Rollback()
        '            bIsMastersImportedSuccessfully = False
        '        End Try
        '    End If
        'Next



        Dim bLastItemGroupCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        Dim DtItemGroup = DtExternalData_Header.DefaultView.ToTable(True, "ProductGroupCode", "ItemGroupDesc", "ItemCategoryDesc")

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
                ItemGroupTable.OMSId = AgL.XNull(DtItemGroup.Rows(I)("ProductGroupCode"))
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

        mQry = " Select * From Item "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)

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
                ItemTable.Specification = AgL.XNull(DtExternalData_Header.Rows(I)("ManualCode"))
                ItemTable.ItemGroupDesc = AgL.XNull(DtExternalData_Header.Rows(I)("ItemGroupDesc"))
                ItemTable.ItemCategoryDesc = AgL.XNull(DtExternalData_Header.Rows(I)("ItemCategoryDesc"))
                ItemTable.ItemType = AgL.XNull(DtExternalData_Header.Rows(I)("ItemType"))
                ItemTable.V_Type = AgL.XNull(DtExternalData_Header.Rows(I)("V_Type"))
                ItemTable.Unit = AgL.XNull(DtExternalData_Header.Rows(I)("Unit"))
                ItemTable.PurchaseRate = AgL.XNull(DtExternalData_Header.Rows(I)("PurchaseRate"))
                ItemTable.Rate = AgL.XNull(DtExternalData_Header.Rows(I)("Rate"))
                ItemTable.SalesTaxPostingGroup = AgL.XNull(DtExternalData_Header.Rows(I)("SalesTaxPostingGroup"))
                ItemTable.HSN = AgL.XNull(DtExternalData_Header.Rows(I)("HSN"))
                ItemTable.BarcodeType = AgL.XNull(DtExternalData_Header.Rows(I)("BarcodeType"))
                ItemTable.BarcodePattern = AgL.XNull(DtExternalData_Header.Rows(I)("BarcodePattern"))
                ItemTable.BarcodeDesc = AgL.XNull(DtExternalData_Header.Rows(I)("BarcodeDesc"))
                ItemTable.BarcodeOMSId = AgL.XNull(DtExternalData_Header.Rows(I)("BarcodeId"))
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

    Private Sub DGL1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If e.RowIndex > -1 Then Dgl1.Rows(e.RowIndex).Selected = True
        Dgl1.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray
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

