Imports AgLibrary.ClsMain.agConstants
Imports System.Threading
Imports System.ComponentModel
Imports System.IO

Public Class FrmSyncData
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_Pakka As New SQLite.SQLiteConnection
    Public mDbPath As String = ""

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker

    Dim DtInterestSlab As DataTable
    Dim DtRateType As DataTable
    Dim DtCity As DataTable
    Dim DtArea As DataTable
    Dim DtZone As DataTable
    Dim DtItem As DataTable
    Dim DtSubGroup As DataTable
    Dim DtSaleInvoice As DataTable
    Dim DtSaleInvoiceDetail As DataTable
    Dim DtPurchInvoice As DataTable
    Dim DtPurchInvoiceDetail As DataTable
    Dim DtLedgerHead As DataTable
    Dim DtLedgerHeadDetail As DataTable

    Dim mFromDate As String = "01/Jan/2020"

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
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FProcSave)
        _backgroundWorker1.RunWorkerAsync()
    End Sub

    Public Sub FProcSave()

        Dim mTrans As String = ""


        UpdateLabel("Initializing...")

        FIniList()


        Try
            'mQry = "Attach 'D:\WorkingCopy\Client Data\ShyamaShyam' AS Pakka "
            mQry = "Attach '" & mDbPath & "' AS Pakka "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Catch ex As Exception
        End Try

        FLoadInterestSlab_W()
        FAddInterestSlab(AgL.GCn, AgL.ECmd)
        FUpdateInterestSlab(AgL.GCn, AgL.ECmd)

        FAddRateType(AgL.GCn, AgL.ECmd)

        FLoadCity_W()
        FAddCity(AgL.GCn, AgL.ECmd)

        FLoadArea_W()
        FAddArea(AgL.GCn, AgL.ECmd)

        FLoadZone_W()
        FAddZone(AgL.GCn, AgL.ECmd)

        FLoadCity_W()
        FLoadInterestSlab_W()
        FLoadRateType_W()

        FLoadArea_W()
        FLoadZone_W()

        FLoadSubGroup_W()
        FUpdateSubGroup(SubgroupType.SalesAgent, AgL.GCn, AgL.ECmd)
        FAddSubGroup(SubgroupType.SalesAgent, AgL.GCn, AgL.ECmd)
        FLoadSubGroup_W()
        FUpdateSubGroup(SubgroupType.PurchaseAgent, AgL.GCn, AgL.ECmd)
        FAddSubGroup(SubgroupType.PurchaseAgent, AgL.GCn, AgL.ECmd)
        FLoadSubGroup_W()
        FUpdateSubGroup(SubgroupType.Employee, AgL.GCn, AgL.ECmd)
        FAddSubGroup(SubgroupType.Employee, AgL.GCn, AgL.ECmd)
        FLoadSubGroup_W()
        FUpdateSubGroup("Master Customer", AgL.GCn, AgL.ECmd)
        FAddSubGroup("Master Customer", AgL.GCn, AgL.ECmd)
        FLoadSubGroup_W()
        FUpdateSubGroup("Master Supplier", AgL.GCn, AgL.ECmd)
        FAddSubGroup("Master Supplier", AgL.GCn, AgL.ECmd)
        FLoadSubGroup_W()
        FUpdateSubGroup("", AgL.GCn, AgL.ECmd)
        FAddSubGroup("", AgL.GCn, AgL.ECmd)
        FLoadSubGroup_W()

        FLoadItem_W()
        FUpdateItem(AgL.GCn, AgL.ECmd)
        FAddItem(AgL.GCn, AgL.ECmd)
        FLoadItem_W()



        FAddItemGroupPerson(AgL.GCn, AgL.ECmd)
        FAddItemGroupRateType(AgL.GCn, AgL.ECmd)
        FAddPersonExtraDiscount(AgL.GCn, AgL.ECmd)


        If bIsMastersImportedSuccessfully = True Then
            FLoadSaleInvoice_W()
            FUpdateSaleInvoice(AgL.GCn, AgL.ECmd)
            FAddSale(AgL.GCn, AgL.ECmd, "SO")
            FLoadSaleInvoice_W()

            FLoadLedgerHead_W()
            FUpdateLedgerHead(AgL.GCn, AgL.ECmd)
            FAddLedgerHead(AgL.GCn, AgL.ECmd)
            FLoadLedgerHead_W()

            FDeleteLedgerHead(AgL.GCn, AgL.ECmd)
            FDeleteSubGroup(AgL.GCn, AgL.ECmd)
            FDeleteItem(AgL.GCn, AgL.ECmd)
            FAddTransactionReferencesCancelled(AgL.GCn, AgL.ECmd)
            FAddTransactionReferences(AgL.GCn, AgL.ECmd)
        Else
            MsgBox("Some masters are not synced successfully, that's why can't process transactions.", MsgBoxStyle.Information)
        End If

        'AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FProcSave)
        'AddHandler LblProgress.TextChanged, New EventHandler(Of EventArgs)(AddressOf Me.UpdateLabel)
        UpdateLabel(" ")
        'LblProgress.Text = " "
        'LblProgress.Refresh()

        MsgBox("Process Completed Successfully...", MsgBoxStyle.Information)

    End Sub
    Private Sub FLoadSubGroup_W()
        mQry = " Select * From SubGroup "
        DtSubGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadItem_W()
        mQry = " Select * From Item "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadCity_W()
        mQry = " Select * From City "
        DtCity = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

    Private Sub FLoadInterestSlab_W()
        mQry = " Select * From InterestSlab "
        DtInterestSlab = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

    Private Sub FLoadArea_W()
        mQry = " Select * From Area "
        DtArea = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadZone_W()
        mQry = " Select * From Zone "
        DtZone = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

    Private Sub FLoadRateType_W()
        mQry = " Select * From RateType "
        DtRateType = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

    Private Sub FLoadSaleInvoice_W()
        mQry = " Select H.* 
                From SaleInvoice H
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                Where Vt.NCat = '" & Ncat.SaleOrder & "'"
        DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select L.* 
                From SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                Where Vt.NCat = '" & Ncat.SaleOrder & "'"
        DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadPurchInvoice_W()
        mQry = " Select * From PurchInvoice "
        DtPurchInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select * From PurchInvoiceDetail "
        DtPurchInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FLoadLedgerHead_W()
        mQry = " Select * From LedgerHead "
        DtLedgerHead = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select * From LedgerHeadDetail "
        DtLedgerHeadDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FIniList()
        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
        Else
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
    End Sub
    Private Function FGetUpdateClause(DtPakka As DataTable, RowIndexPakka As Integer,
                                      DtKachha As DataTable, RowIndexKaccha As Integer,
                                      FieldName As String, Optional DataType As String = "")
        If AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) <> AgL.XNull(DtKachha.Rows(RowIndexKaccha)(FieldName)) Then
            If DataType = "Date" Then
                FGetUpdateClause = FieldName + " = " & AgL.Chk_Date(AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName))) & "" + ","
            ElseIf DataType = "Number" Then
                FGetUpdateClause = FieldName + " = " & AgL.VNull(DtPakka.Rows(RowIndexPakka)(FieldName)) & "" + ","
            Else
                FGetUpdateClause = FieldName + " = " & AgL.Chk_Text(AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName))) & "" + ","
            End If
        Else
            FGetUpdateClause = ""
        End If
    End Function
    Private Function FGetUpdateClauseForSubGroup(DtPakka As DataTable, RowIndexPakka As Integer,
                                      DtKachha As DataTable, RowIndexKaccha As Integer,
                                      FieldName As String)
        If AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) <> AgL.XNull(DtKachha.Rows(RowIndexKaccha)(FieldName + "OMSId")) Then
            Dim DtSubGroupRow As DataRow() = DtSubGroup.Select("OMSId = '" & AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) & "'")
            If DtSubGroupRow.Length > 0 Then
                FGetUpdateClauseForSubGroup = FieldName + " = " + AgL.Chk_Text(AgL.XNull(DtSubGroupRow(0)("SubCode"))) + ","
            Else
                FGetUpdateClauseForSubGroup = ""
            End If
        Else
            FGetUpdateClauseForSubGroup = ""
        End If
    End Function
    Private Function FGetUpdateClauseForItem(DtPakka As DataTable, RowIndexPakka As Integer,
                                      DtKachha As DataTable, RowIndexKaccha As Integer,
                                      FieldName As String)
        If AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) <> AgL.XNull(DtKachha.Rows(RowIndexKaccha)(FieldName + "OMSId")) Then
            Dim DtItemRow As DataRow() = DtItem.Select("OMSId = '" & AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) & "'")
            If DtItemRow.Length > 0 Then
                FGetUpdateClauseForItem = FieldName + " = " + AgL.Chk_Text(AgL.XNull(DtItemRow(0)("Code"))) + ","
            Else
                FGetUpdateClauseForItem = ""
            End If
        Else
            FGetUpdateClauseForItem = ""
        End If
    End Function
    Private Function FGetUpdateClauseForCity(DtPakka As DataTable, RowIndexPakka As Integer,
                                      DtKachha As DataTable, RowIndexKaccha As Integer,
                                      FieldName As String)
        If AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) <> AgL.XNull(DtKachha.Rows(RowIndexKaccha)(FieldName + "OMSId")) Then
            Dim DtCityRow As DataRow() = DtCity.Select("OMSId = '" & AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) & "'")
            If DtCityRow.Length > 0 Then
                FGetUpdateClauseForCity = FieldName + " = " + AgL.Chk_Text(AgL.XNull(DtCityRow(0)("CityCode"))) + ","
            Else
                FGetUpdateClauseForCity = ""
            End If
        Else
            FGetUpdateClauseForCity = ""
        End If
    End Function

    Private Function FGetUpdateClauseForArea(DtPakka As DataTable, RowIndexPakka As Integer,
                                      DtKachha As DataTable, RowIndexKaccha As Integer,
                                      FieldName As String)
        If AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) <> AgL.XNull(DtKachha.Rows(RowIndexKaccha)(FieldName + "OMSId")) Then
            Dim DtAreaRow As DataRow() = DtArea.Select("OMSId = '" & AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) & "'")
            If DtAreaRow.Length > 0 Then
                FGetUpdateClauseForArea = FieldName + " = " + AgL.Chk_Text(AgL.XNull(DtAreaRow(0)("Code"))) + ","
            Else
                FGetUpdateClauseForArea = ""
            End If
        Else
            FGetUpdateClauseForArea = ""
        End If
    End Function


    Private Function FGetUpdateClauseForInterestSlab(DtPakka As DataTable, RowIndexPakka As Integer,
                                      DtKachha As DataTable, RowIndexKaccha As Integer,
                                      FieldName As String)
        'If AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) <> AgL.XNull(DtKachha.Rows(RowIndexKaccha)(FieldName + "OMSId")) Then
        Dim DtInterestSlabRow As DataRow() = DtInterestSlab.Select("OMSId = '" & AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) & "'")
        If DtInterestSlabRow.Length > 0 Then
            FGetUpdateClauseForInterestSlab = FieldName + " = " + AgL.Chk_Text(AgL.XNull(DtInterestSlabRow(0)("Code"))) + ","
        Else
            FGetUpdateClauseForInterestSlab = ""
        End If
        'Else
        'FGetUpdateClauseForInterestSlab = ""
        'End If
    End Function

    Private Function FGetUpdateClauseForRateType(DtPakka As DataTable, RowIndexPakka As Integer,
                                      DtKachha As DataTable, RowIndexKaccha As Integer,
                                      FieldName As String)
        'If AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) <> AgL.XNull(DtKachha.Rows(RowIndexKaccha)(FieldName + "OMSId")) Then
        Dim DtRateTypeRow As DataRow() = DtRateType.Select("OMSId = '" & AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) & "'")
        If DtRateTypeRow.Length > 0 Then
            FGetUpdateClauseForRateType = FieldName + " = " + AgL.Chk_Text(AgL.XNull(DtRateTypeRow(0)("Code"))) + ","
        Else
            FGetUpdateClauseForRateType = ""
        End If
        'Else
        'FGetUpdateClauseForRateType = ""
        'End If
    End Function

    Public Sub FAddSubGroup(SubgroupTypeStr As String, Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer


        Dim mPartyQry As String = " Select VReg.SalesTaxNo, VReg.PanNo, VReg.AadharNo,  
                C.CityName, C.State, S.Description As StateName, Ag.GroupName, 
                A.Description As AreaName, Sg.*
                From SubGroup Sg
                LEFT JOIN AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                LEFT JOIN City C ON Sg.CityCode = C.CityCode 
                LEFT JOIN State S ON C.State = S.Code
                LEFT JOIN Area A ON Sg.Area = A.Code
                LEFT JOIN (
	                SELECT Sgr.Subcode, 
	                Max(CASE WHEN Sgr.RegistrationType =  'Sales Tax No' THEN Sgr.RegistrationNo ELSE NULL END) AS SalesTaxNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'PAN No' THEN Sgr.RegistrationNo ELSE NULL END) AS PanNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'AADHAR NO' THEN Sgr.RegistrationNo ELSE NULL END) AS AadharNo
	                FROM SubgroupRegistration Sgr 
	                GROUP BY Sgr.Subcode         
                ) AS VReg ON Sg.SubCode = VReg.SubCode
                Where Sg.UploadDate Is Null "
        If SubgroupTypeStr <> "" Then
            mPartyQry = mPartyQry & " and SubgroupType In (" & AgL.Chk_Text(SubgroupTypeStr.Replace(",", "','")) & ")"
        End If
        mPartyQry = mPartyQry & " Order By Sg.Parent "
        Dim DtPartySource As DataTable = AgL.FillData(mPartyQry, Connection_Pakka).Tables(0)



        Dim bLastAcGroupCode As Integer = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(GroupCode AS INTEGER)),0) FROM AcGroup WHERE ABS(GroupCode)>0", AgL.GcnRead).ExecuteScalar)
        Dim DtAccountGroup = DtPartySource.DefaultView.ToTable(True, "GroupName")
        For I = 0 To DtAccountGroup.Rows.Count - 1
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

            UpdateLabel("Inserting Account Group " + AcGroupTable.GroupName)
            'LblProgress.Text = "Inserting Account Group " + AcGroupTable.GroupName
            'LblProgress.Refresh()


            FrmPerson.ImportAcGroupTable(AcGroupTable)
        Next




        Dim bLastCityCode As String = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        Dim DtCityToImport As DataTable = DtPartySource.DefaultView.ToTable(True, "CityCode", "CityName", "State")

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

                    FrmCity.ImportCityTable(CityTable)
                End If
            End If
        Next
        FLoadCity_W()



        Dim bLastSubCode As String = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtPartySource.Rows.Count - 1
            If DtSubGroup.Select("OMSId = '" & DtPartySource.Rows(I)("SubCode") & "'").Length = 0 Then
                Dim SubGroupTable As New FrmPerson.StructSubGroupTable
                Dim bSubCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastSubCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(8, "0")

                SubGroupTable.SubCode = bSubCode
                SubGroupTable.Site_Code = AgL.PubSiteCode
                SubGroupTable.Name = AgL.XNull(DtPartySource.Rows(I)("Name"))
                SubGroupTable.DispName = AgL.XNull(DtPartySource.Rows(I)("DispName"))
                SubGroupTable.ManualCode = AgL.XNull(DtPartySource.Rows(I)("ManualCode"))
                SubGroupTable.AccountGroup = AgL.XNull(DtPartySource.Rows(I)("GroupName"))
                SubGroupTable.StateName = AgL.XNull(DtPartySource.Rows(I)("StateName"))
                SubGroupTable.AgentName = ""
                SubGroupTable.TransporterName = ""
                SubGroupTable.AreaName = AgL.XNull(DtPartySource.Rows(I)("AreaName"))
                SubGroupTable.GroupCode = AgL.XNull(DtPartySource.Rows(I)("GroupCode"))
                SubGroupTable.GroupNature = AgL.XNull(DtPartySource.Rows(I)("GroupNature"))
                SubGroupTable.ParentCode = FGetSubCodeFromOMSId(AgL.XNull(DtPartySource.Rows(I)("Parent")))
                SubGroupTable.Nature = AgL.XNull(DtPartySource.Rows(I)("Nature"))
                SubGroupTable.Address = AgL.XNull(DtPartySource.Rows(I)("Address"))
                SubGroupTable.CityCode = FGetCityCodeFromOMSId(AgL.XNull(DtPartySource.Rows(I)("CityCode")))
                SubGroupTable.CityName = AgL.XNull(DtPartySource.Rows(I)("CityName"))
                SubGroupTable.PIN = AgL.XNull(DtPartySource.Rows(I)("PIN"))
                SubGroupTable.Phone = AgL.XNull(DtPartySource.Rows(I)("Phone"))
                SubGroupTable.ContactPerson = AgL.XNull(DtPartySource.Rows(I)("ContactPerson"))
                SubGroupTable.SubgroupType = AgL.XNull(DtPartySource.Rows(I)("SubgroupType"))
                SubGroupTable.Mobile = AgL.XNull(DtPartySource.Rows(I)("Mobile"))
                SubGroupTable.CreditDays = AgL.XNull(DtPartySource.Rows(I)("CreditDays"))
                SubGroupTable.CreditLimit = AgL.XNull(DtPartySource.Rows(I)("CreditLimit"))
                SubGroupTable.EMail = AgL.XNull(DtPartySource.Rows(I)("EMail"))
                SubGroupTable.SalesTaxPostingGroup = AgL.XNull(DtPartySource.Rows(I)("SalesTaxPostingGroup"))
                SubGroupTable.InterestSlab = FGetInterestSlabCodeFromOMSId(AgL.XNull(DtPartySource.Rows(I)("InterestSlab")))
                SubGroupTable.EntryBy = AgL.PubUserName
                SubGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                SubGroupTable.EntryType = "Add"
                SubGroupTable.EntryStatus = ClsMain.LogStatus.LogOpen
                SubGroupTable.Div_Code = AgL.PubDivCode
                SubGroupTable.Status = AgL.XNull(DtPartySource.Rows(I)("WStatus"))
                SubGroupTable.SalesTaxNo = AgL.XNull(DtPartySource.Rows(I)("SalesTaxNo"))
                SubGroupTable.PANNo = AgL.XNull(DtPartySource.Rows(I)("PANNo"))
                SubGroupTable.AadharNo = AgL.XNull(DtPartySource.Rows(I)("AadharNo"))
                SubGroupTable.OMSId = AgL.XNull(DtPartySource.Rows(I)("SubCode"))
                SubGroupTable.LockText = "Synced From Other Database."
                SubGroupTable.Cnt = I

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    UpdateLabel("Inserting Party " + SubGroupTable.Name)
                    'LblProgress.Text = "Inserting Party " + SubGroupTable.Name
                    'LblProgress.Refresh()

                    FrmPerson.ImportSubgroupTable(SubGroupTable)

                    mQry = " Delete From SubgroupSiteDivisionDetail Where SubCode = '" & SubGroupTable.SubCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "INSERT INTO SubgroupSiteDivisionDetail (SubCode, Div_Code, Site_Code, Agent, RateType, OMSId)
                            Select '" & SubGroupTable.SubCode & "', H.Div_Code, H.Site_Code, Ag.SubCode,Rt.Code,
                            H.SubCode || H.Div_Code || H.Site_Code 
                            FROM Pakka.SubgroupSiteDivisionDetail H 
                            LEFT JOIN Pakka.SubGroup PAg On H.Agent = PAg.SubCode
                            LEFT JOIN SubgroupSiteDivisionDetail Sgd On H.SubCode || H.Div_Code || H.Site_Code = Sgd.OMSId 
                            LEFT JOIN SubGroup Ag On PAg.SubCode = Ag.OMSId
                            LEFT JOIN RateType Rt On H.RateType = Rt.OMSId
                            Where H.SubCode = '" & AgL.XNull(DtPartySource.Rows(I)("SubCode")) & "'
                            And Sgd.SubCode Is Null "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    mQry = " UPDATE Pakka.SubGroup Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.SubGroup.SubCode = '" & AgL.XNull(DtPartySource.Rows(I)("SubCode")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    'Connection_Pakka.Open()
                    'mQry = " UPDATE SubGroup Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                    '        Where SubGroup.SubCode = '" & AgL.XNull(DtPartySource.Rows(I)("SubCode")) & "'"
                    'AgL.   (mQry, Connection_Pakka)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next
    End Sub
    Private Sub FUpdateSubGroup(SubgroupTypeStr As String, Conn As Object, Cmd As Object)
        Connection_Pakka.Open()

        mQry = " Select * From SubGroup Where UploadDate Is Null "
        If SubgroupTypeStr <> "" Then
            mQry = mQry & " and SubgroupType In (" & AgL.Chk_Text(SubgroupTypeStr.Replace(",", "','")) & ")"
        End If
        mQry = mQry & " Order by Parent "

        Dim DtPakka As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Connection_Pakka.Close()





        Dim bSourceDocIdStr As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            If bSourceDocIdStr <> "" Then bSourceDocIdStr += ","
            bSourceDocIdStr += AgL.Chk_Text(AgL.XNull(DtPakka.Rows(I)("SubCode")))
        Next

        mQry = " Select Psg.OMSId As ParentOMSId, C.OMSId As CityCodeOMSId, 
                A.OMSId As AreaOMSId, Sg.* 
                From SubGroup Sg
                LEFT JOIN SubGroup PSg On Sg.Parent = PSg.SubCode
                LEFT JOIN City C On Sg.CityCode = C.CityCode
                LEFT JOIN Area A On Sg.Area = A.Code
                Where Sg.OMSId In (" & bSourceDocIdStr & ") "
        Dim DtKachha As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            For J As Integer = 0 To DtKachha.Rows.Count - 1
                If AgL.XNull(DtPakka.Rows(I)("SubCode")) = AgL.XNull(DtKachha.Rows(J)("OMSId")) Then
                    bUpdateClauseQry = ""
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SubgroupType")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ManualCode")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "NamePrefix")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Name")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "DispName")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "GroupCode")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "GroupNature")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Nature")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Address")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PIN")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Phone")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Mobile")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Email")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ContactPerson")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "CreditLimit")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "CreditDays")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SalesTaxPostingGroup")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "Parent")
                    bUpdateClauseQry += FGetUpdateClauseForCity(DtPakka, I, DtKachha, J, "CityCode")
                    bUpdateClauseQry += FGetUpdateClauseForArea(DtPakka, I, DtKachha, J, "Area")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Div_Code")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Site_Code")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PostingGroupSalesTaxItem")
                    bUpdateClauseQry += FGetUpdateClauseForInterestSlab(DtPakka, I, DtKachha, J, "InterestSlab")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "HSN")

                    'bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Status")
                    If AgL.XNull(DtPakka.Rows(I)("WStatus")) <> AgL.XNull(DtKachha.Rows(J)("Status")) Then
                        bUpdateClauseQry += "Status" + " = " & AgL.Chk_Text(AgL.XNull(DtPakka.Rows(I)("WStatus"))) & "" + ","
                    End If

                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryBy")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryType")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Remarks")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShowAccountInOtherDivisions")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "WeekOffDays")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShowAccountInOtherSites")

                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        UpdateLabel("Updating Party " + AgL.XNull(DtPakka.Rows(I)("Name")))
                        'LblProgress.Text = "Updating Party " + AgL.XNull(DtPakka.Rows(I)("Name"))
                        'LblProgress.Refresh()

                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE SubGroup  Set " + bUpdateClauseQry + " Where SubCode = '" & AgL.XNull(DtKachha.Rows(J)("SubCode")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If

                        Dim bSubCode As String = FGetSubCodeFromOMSId(AgL.XNull(DtPakka.Rows(I)("SubCode")))

                        mQry = " Delete From SubgroupSiteDivisionDetail Where SubCode = '" & bSubCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "INSERT INTO SubgroupSiteDivisionDetail (SubCode, Div_Code, Site_Code, Agent, RateType, OMSId)
                                Select '" & bSubCode & "', H.Div_Code, H.Site_Code, Ag.SubCode, Rt.Code,
                                H.SubCode || H.Div_Code || H.Site_Code As OMSId
                                FROM Pakka.SubgroupSiteDivisionDetail H 
                                LEFT JOIN Pakka.SubGroup PAg On H.Agent = PAg.SubCode
                                LEFT JOIN SubgroupSiteDivisionDetail Sgd On H.SubCode || H.Div_Code || H.Site_Code = Sgd.OMSId 
                                LEFT JOIN SubGroup Ag On PAg.SubCode = Ag.OMSId
                                LEFT JOIN RateType Rt On H.RateType = Rt.OMSId
                                Where H.SubCode = '" & AgL.XNull(DtPakka.Rows(I)("SubCode")) & "'
                                And Sgd.SubCode Is Null "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                        mQry = " UPDATE Pakka.SubGroup Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                                Where Pakka.SubGroup.SubCode = '" & AgL.XNull(DtPakka.Rows(I)("SubCode")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        AgL.ETrans.Rollback()
                        MsgBox(ex.Message)
                    End Try
                End If
            Next
        Next
    End Sub
    Public Sub FAddItem(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting Items...")
        'LblProgress.Text = " Start Inserting Items..."
        'LblProgress.Refresh()


        mQry = "Select I.*
                From Item I
                Where IfNull(I.V_Type,'') = 'IC' And I.UploadDate Is Null "
        Dim DtItemCategory As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bLastItemCategoryCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtItemCategory.Rows.Count - 1
            If AgL.XNull(DtItemCategory.Rows(I)("Description")) <> "" Then
                Dim ItemCategoryTable As New FrmItemMaster.StructItemCategory
                Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemCategoryTable.Code = bItemCategoryCode
                ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)("Description"))
                ItemCategoryTable.ItemType = ItemTypeCode.TradingProduct
                ItemCategoryTable.SalesTaxPostingGroup = "GST 0%"
                ItemCategoryTable.Unit = "Nos"
                ItemCategoryTable.EntryBy = AgL.PubUserName
                ItemCategoryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemCategoryTable.EntryType = "Add"
                ItemCategoryTable.LockText = "Synced From Other Database."
                ItemCategoryTable.EntryStatus = ClsMain.LogStatus.LogOpen
                ItemCategoryTable.Div_Code = AgL.PubDivCode
                ItemCategoryTable.Status = "Active"
                ItemCategoryTable.OMSId = AgL.XNull(DtItemCategory.Rows(I)("Code"))

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmItemMaster.ImportItemCategoryTable(ItemCategoryTable)

                    UpdateLabel("Inserting Item Category " + ItemCategoryTable.Description)
                    'LblProgress.Text = "Inserting Item Category " + ItemCategoryTable.Description
                    'LblProgress.Refresh()

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next

        mQry = "Select Ic.Description As ItemCategoryDesc, I.*
                From Item I
                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                Where IfNull(I.V_Type,'') = 'IG' And I.UploadDate Is Null "
        Dim DtItemGroup As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bLastItemGroupCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtItemGroup.Rows.Count - 1
            If AgL.XNull(DtItemGroup.Rows(I)("Description")) <> "" Then
                Dim ItemGroupTable As New FrmItemMaster.StructItemGroup
                Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemGroupTable.Code = bItemGroupCode
                ItemGroupTable.Description = AgL.XNull(DtItemGroup.Rows(I)("Description"))
                ItemGroupTable.ItemCategory = AgL.XNull(DtItemGroup.Rows(I)("ItemCategoryDesc"))
                ItemGroupTable.ItemType = ItemTypeCode.TradingProduct
                ItemGroupTable.SalesTaxPostingGroup = "GST 0%"
                ItemGroupTable.DefaultSupplier = FGetSubCodeFromOMSId(AgL.XNull(DtItemGroup.Rows(I)("DefaultSupplier")))
                ItemGroupTable.Unit = "Nos"
                ItemGroupTable.EntryBy = AgL.PubUserName
                ItemGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemGroupTable.EntryType = "Add"
                ItemGroupTable.LockText = "Synced From Other Database."
                ItemGroupTable.EntryStatus = ClsMain.LogStatus.LogOpen
                ItemGroupTable.Div_Code = AgL.PubDivCode
                ItemGroupTable.Status = "Active"
                ItemGroupTable.OMSId = AgL.XNull(DtItemGroup.Rows(I)("Code"))

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmItemMaster.ImportItemGroupTable(ItemGroupTable)

                    UpdateLabel("Inserting Item Group " + ItemGroupTable.Description)
                    'LblProgress.Text = "Inserting Item Group " + ItemGroupTable.Description
                    'LblProgress.Refresh()


                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next


        mQry = "Select Ic.Description As ItemCategoryDesc, Ig.Description As ItemGroupDesc, I.*
                From Item I
                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                Where IfNull(I.V_Type,'') = 'ITEM' And I.UploadDate Is Null "
        Dim DtItemSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bLastItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtItemSource.Rows.Count - 1
            If AgL.XNull(DtItemSource.Rows(I)("Description")) <> "" Then
                If DtItem.Select("OMSId = '" & AgL.XNull(DtItemSource.Rows(I)("Code")) & "'").Length = 0 Then
                    Dim ItemTable As New FrmItemMaster.StructItem
                    Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemTable.Code = bItemCode
                    ItemTable.ManualCode = AgL.XNull(DtItemSource.Rows(I)("ManualCode"))
                    ItemTable.Description = AgL.XNull(DtItemSource.Rows(I)("Description"))
                    ItemTable.DisplayName = AgL.XNull(DtItemSource.Rows(I)("DisplayName"))
                    ItemTable.Specification = AgL.XNull(DtItemSource.Rows(I)("Specification"))
                    ItemTable.ItemGroupDesc = AgL.XNull(DtItemSource.Rows(I)("ItemGroupDesc"))
                    ItemTable.ItemCategoryDesc = AgL.XNull(DtItemSource.Rows(I)("ItemCategoryDesc"))
                    ItemTable.ItemType = AgL.XNull(DtItemSource.Rows(I)("ItemType"))
                    ItemTable.V_Type = AgL.XNull(DtItemSource.Rows(I)("V_Type"))
                    ItemTable.Unit = AgL.XNull(DtItemSource.Rows(I)("Unit"))
                    ItemTable.PurchaseRate = AgL.XNull(DtItemSource.Rows(I)("PurchaseRate"))
                    ItemTable.Rate = AgL.XNull(DtItemSource.Rows(I)("Rate"))
                    'ItemTable.SalesTaxPostingGroup = AgL.XNull(DtItemSource.Rows(I)("SalesTaxPostingGroup"))
                    ItemTable.SalesTaxPostingGroup = "GST 0%"
                    ItemTable.HSN = AgL.XNull(DtItemSource.Rows(I)("HSN"))
                    ItemTable.EntryBy = AgL.PubUserName
                    ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    ItemTable.EntryType = "Add"
                    ItemTable.EntryStatus = ClsMain.LogStatus.LogOpen
                    ItemTable.Div_Code = AgL.PubDivCode
                    ItemTable.Status = "Active"
                    ItemTable.LockText = "Synced From Other Database."
                    ItemTable.OMSId = AgL.XNull(DtItemSource.Rows(I)("Code"))
                    ItemTable.StockYN = 1
                    ItemTable.IsSystemDefine = 0


                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"
                        FrmItemMaster.ImportItemTable(ItemTable)

                        UpdateLabel("Inserting Item " + ItemTable.Description)
                        'LblProgress.Text = "Inserting Item " + ItemTable.Description
                        'LblProgress.Refresh()


                        mQry = " UPDATE Pakka.Item Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.Item.Code = '" & AgL.XNull(DtItemSource.Rows(I)("Code")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        AgL.ETrans.Rollback()
                        MsgBox(ex.Message)
                        bIsMastersImportedSuccessfully = False
                    End Try
                End If
            End If
        Next
    End Sub
    Private Sub FUpdateItem(Conn As Object, Cmd As Object)
        Connection_Pakka.Open()

        mQry = " Select * From Item Where UploadDate Is Null "
        Dim DtPakka As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Connection_Pakka.Close()


        Dim bSourceDocIdStr As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            If bSourceDocIdStr <> "" Then bSourceDocIdStr += ","
            bSourceDocIdStr += AgL.Chk_Text(AgL.XNull(DtPakka.Rows(I)("Code")))
        Next

        mQry = " Select Ig.OMSId As ItemGroupOMSId, Ic.OMSId As ItemCategoryOMSId, 
                Sg.OMSId As DefaultSupplierOMSId, I.* 
                From Item I
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                LEFT JOIN SubGroup Sg On I.DefaultSupplier = Sg.SubCode
                Where I.OMSId In (" & bSourceDocIdStr & ") "
        Dim DtKachha As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            For J As Integer = 0 To DtKachha.Rows.Count - 1
                If AgL.XNull(DtPakka.Rows(I)("Code")) = AgL.XNull(DtKachha.Rows(J)("OMSId")) Then
                    bUpdateClauseQry = ""
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ManualCode")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Description")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "DisplayName")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Unit")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "DealQty", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "DealUnit")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ItemType")

                    bUpdateClauseQry += FGetUpdateClauseForItem(DtPakka, I, DtKachha, J, "ItemCategory")
                    bUpdateClauseQry += FGetUpdateClauseForItem(DtPakka, I, DtKachha, J, "ItemGroup")

                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "DefaultSupplier")

                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PurchaseRate", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Rate", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryBy")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Div_Code")
                    'bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SalesTaxPostingGroup")

                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "LastPurchaseRate", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "LastPurchaseDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Specification")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "StockYN")

                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "HSN")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Site_Code")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "HSN")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShowItemInOtherDivisions")

                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "MRP", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Default_DiscountPerSale", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Default_AdditionalDiscountPerSale", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Default_AdditionPerSale", "Number")


                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Default_DiscountPerPurchase", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Default_AdditionalDiscountPerPurchase", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Default_AdditionPerPurchase", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Default_MarginPer", "Number")


                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE Item Set " + bUpdateClauseQry + " Where Code = '" & AgL.XNull(DtKachha.Rows(J)("Code")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        UpdateLabel("Updating Item " & AgL.XNull(DtPakka.Rows(I)("Description")))
                        'LblProgress.Text = "Updating Item " & AgL.XNull(DtPakka.Rows(I)("Description"))
                        'LblProgress.Refresh()


                        mQry = " UPDATE Pakka.Item Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.Item.Code = '" & AgL.XNull(DtPakka.Rows(I)("Code")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        AgL.ETrans.Rollback()
                        MsgBox(ex.Message)
                    End Try
                End If
            Next
        Next
    End Sub

    Private Sub FUpdateSaleInvoice(Conn As Object, Cmd As Object)
        Connection_Pakka.Open()

        mQry = " Select * From SaleInvoice H Where UploadDate Is Null "
        Dim DtPakka As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Connection_Pakka.Close()

        UpdateLabel("Start Updating Sale Invoices...")
        'LblProgress.Text = "Start Updating Sale Invoices..."
        'LblProgress.Refresh()

        Dim bSourceDocIdStr As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            If bSourceDocIdStr <> "" Then bSourceDocIdStr += ","
            bSourceDocIdStr += AgL.Chk_Text(AgL.XNull(DtPakka.Rows(I)("DocId")))
        Next

        mQry = " Select H.*, Sg1.OMSId As SaleToPartyOMSId, Sg2.OMSId As BillToPartyOMSId, Sg3.OmsId AS AgentOMSId 
                From SaleInvoice H
                LEFT JOIN SubGroup Sg1 On H.SaleToParty = Sg1.SubCode
                LEFT JOIN SubGroup Sg2 On H.BillToParty = Sg2.SubCode
                LEFT JOIN SubGroup Sg3 On H.Agent = Sg3.Subcode 
                Where H.OMSId In (" & bSourceDocIdStr & ") "
        Dim DtKachha As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            For J As Integer = 0 To DtKachha.Rows.Count - 1
                If AgL.XNull(DtPakka.Rows(I)("DocId")) = AgL.XNull(DtKachha.Rows(J)("OMSId")) Then
                    bUpdateClauseQry = ""
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "V_Date", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ManualRefNo")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "SaleToParty")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "BillToParty")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "Agent")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyName")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyAddress")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyPinCode")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyMobile")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartySalesTaxNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShipToAddress")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SalesTaxGroupParty")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PlaceOfSupply")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyDocNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyDocDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Remarks")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "TermsAndConditions")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Gross_Amount", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Taxable_Amount", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax1_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax1", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax2_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax2", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax3_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax3", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax4_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax4", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax5_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax5", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SubTotal1", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Deduction_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Deduction", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Other_Charge_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Other_Charge", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Round_Off", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Net_Amount", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PaidAmt", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "CreditLimit", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "CreditDays", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tags")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Status")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryBy")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyAadharNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyPanNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "DeliveryDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ReferenceNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SpecialDiscount_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SpecialDiscount", "Number")


                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE SaleInvoice Set " + bUpdateClauseQry + " Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        UpdateLabel("Updating Sale " & AgL.XNull(DtPakka.Rows(I)("V_Type")) & "-" & AgL.XNull(DtPakka.Rows(I)("ManualRefNo")))
                        'LblProgress.Text = "Updating Sale " & AgL.XNull(DtPakka.Rows(I)("V_Type")) & "-" & AgL.XNull(DtPakka.Rows(I)("ManualRefNo"))
                        'LblProgress.Refresh()


                        mQry = " UPDATE Pakka.SaleInvoice Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.SaleInvoice.DocId = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                        'For Line Logic

                        mQry = " Delete From SaleInvoiceDetail Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "INSERT INTO SaleInvoiceDetail (DocId, Sr, Item, Specification, SalesTaxGroupItem, LotNo, BaleNo, Pcs, Deal, ExpiryDate, LrNo, LrDate, DocQty, FreeQty, Qty, RejQty, Unit, UnitMultiplier, DocDealQty, FreeDealQty, DealQty, RejDealQty, DealUnit, Rate, MRP, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount, Amount, ProfitMarginPer, Sale_Rate, ReferenceDocId, ReferenceDocIdTSr, ReferenceDocIdSr, SaleInvoice, SaleInvoiceSr, Godown, SalesRepresentative, Remark, DimensionDetail, GrossWeight, NetWeight, ReconcileDateTime, ReconcileBy, Gross_Amount, SpecialDiscount_Per, SpecialDiscount, Taxable_Amount, Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount, 
                            AdditionPer, AdditionAmount, OmsId, ItemState, Remarks1, Remarks2)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.Sr, I.Code AS Item, L.Specification, L.SalesTaxGroupItem, L.LotNo, L.BaleNo, L.Pcs, L.Deal, L.ExpiryDate, L.LrNo, 
                            L.LrDate, L.DocQty, L.FreeQty, L.Qty, L.RejQty, L.Unit, L.UnitMultiplier, L.DocDealQty, L.FreeDealQty, L.DealQty, 
                            L.RejDealQty, L.DealUnit, L.Rate, L.MRP, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, 
                            L.AdditionalDiscountAmount, L.Amount, L.ProfitMarginPer, L.Sale_Rate, L.ReferenceDocId, L.ReferenceDocIdTSr, 
                            L.ReferenceDocIdSr, 
                            IfNull(Sid.DocID,'" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' ) AS SaleInvoice, 
                            IfNull(Sid.Sr,L.Sr) AS SaleInvoiceSr, L.Godown, L.SalesRepresentative, L.Remark, L.DimensionDetail, 
                            L.GrossWeight, L.NetWeight, L.ReconcileDateTime, L.ReconcileBy, L.Gross_Amount, L.SpecialDiscount_Per, 
                            L.SpecialDiscount, L.Taxable_Amount, L.Tax1_Per, L.Tax1, L.Tax2_Per, L.Tax2, L.Tax3_Per, L.Tax3, L.Tax4_Per, L.Tax4, 
                            L.Tax5_Per, L.Tax5, L.SubTotal1, L.Deduction_Per, L.Deduction, L.Other_Charge_Per, L.Other_Charge, L.Round_Off, 
                            L.Net_Amount, L.AdditionPer, L.AdditionAmount, 
                            L.DocId || Cast(L.Sr As nvarchar) As OmsId, 
                            L.ItemState, L.Remarks1, L.Remarks2 
                            FROM Pakka.SaleInvoice H 
                            LEFT JOIN Pakka.SaleInvoiceDetail L ON H.DocID = L.DocID
                            LEFT JOIN Item I ON L.Item = I.OmsId
                            LEFT JOIN SaleInvoiceDetail Sid ON L.SaleInvoice || CAST(L.SaleInvoiceSr AS INTEGER) = Sid.OmsId
                            WHERE H.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " INSERT INTO Stock (DocId, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, Subcode, Currency, 
                            SalesTaxGroupParty, Structure, BillingType, Item, Item_UID, LotNo, 
                            ProcessGroup, Godown, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss, DealQty_Rec, 
                            DealUnit, Rate, Amount, Addition, Deduction, NetAmount, Remarks, 
                            Process, Status, RecId, UID, FIFORate, FIFOAmt, AVGRate, AVGAmt, Cost, Doc_Qty, ReferenceDocID, FIFOValue, BaleNo, ProdOrder, ReferenceDocIDSr, ExpiryDate, MRP, NDP, CurrentStock, EType_IR, Landed_Value, OtherAdjustment, CostCenter, Sale_Rate, Specification, Manufacturer, SalesTaxGroupItem, ItemState)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.TSr, L.Sr, L.V_Type, L.V_Prefix, L.V_Date, 
                            " & AgL.XNull(DtKachha.Rows(J)("V_No")) & " As V_No, 
                            L.Div_Code, L.Site_Code, Sg.SubCode, L.Currency, 
                            L.SalesTaxGroupParty, L.Structure, L.BillingType, I.Code AS Item, L.Item_UID, L.LotNo, L.ProcessGroup, L.Godown, 
                            L.Qty_Iss, L.Qty_Rec, L.Unit, L.UnitMultiplier, L.DealQty_Iss, L.DealQty_Rec, L.DealUnit, L.Rate, L.Amount, 
                            L.Addition, L.Deduction, L.NetAmount, L.Remarks, L.Process, L.Status, L.RecId, L.UID, L.FIFORate, L.FIFOAmt, 
                            L.AVGRate, L.AVGAmt, L.Cost, L.Doc_Qty, L.ReferenceDocID, L.FIFOValue, L.BaleNo, L.ProdOrder, L.ReferenceDocIDSr, 
                            L.ExpiryDate, L.MRP, L.NDP, L.CurrentStock, L.EType_IR, L.Landed_Value, L.OtherAdjustment, L.CostCenter, 
                            L.Sale_Rate, L.Specification, L.Manufacturer, L.SalesTaxGroupItem, L.ItemState
                            FROM Pakka.Stock L 
                            LEFT JOIN Item I ON L.Item = I.OmsId
                            LEFT JOIN Subgroup Sg ON L.SubCode = Sg.OmsId
                            WHERE L.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, AmtDr, AmtCr, Chq_No, Chq_Date, Clg_Date, TDSCategory, TdsDesc, TdsOnAmt, TdsPer, Tds_Of_V_Sno, Narration, Site_Code, U_Name, U_EntDt, U_AE, DivCode, PQty, SQty, AgRefNo, GroupCode, GroupNature, RowId, UpLoadDate, AddBy, AddDate, ModifyBy, ModifyDate, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, OldDocid, CostCenter, System_Generated, FarmulaString, ContraText, RecId, FormulaString, OrignalAmt, TDSDeductFrom, ReferenceDocId, ReferenceDocIdSr, CreditDays, EffectiveDate, LinkedSubcode)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.V_SNo, 
                            " & AgL.XNull(DtKachha.Rows(J)("V_No")) & " As V_No, L.V_Type, L.V_Prefix, L.V_Date, Sg1.Subcode AS SubCode, Sg2.Subcode AS ContraSub, L.AmtDr, L.AmtCr, 
                            L.Chq_No, L.Chq_Date, L.Clg_Date, L.TDSCategory, L.TdsDesc, L.TdsOnAmt, L.TdsPer, L.Tds_Of_V_Sno, 
                            L.Narration, L.Site_Code, L.U_Name, L.U_EntDt, L.U_AE, L.DivCode, L.PQty, L.SQty, L.AgRefNo, L.GroupCode, L.GroupNature, 
                            L.RowId, L.UpLoadDate, L.AddBy, L.AddDate, L.ModifyBy, L.ModifyDate, L.ApprovedBy, L.ApprovedDate, L.GPX1, L.GPX2, 
                            L.GPN1, L.GPN2, L.OldDocid, L.CostCenter, L.System_Generated, L.FarmulaString, L.ContraText, L.RecId, L.FormulaString, 
                            L.OrignalAmt, L.TDSDeductFrom, L.ReferenceDocId, L.ReferenceDocIdSr, L.CreditDays, L.EffectiveDate, Sg3.Subcode AS LinkedSubcode
                            FROM Pakka.Ledger L 
                            LEFT JOIN Subgroup Sg1 ON L.SubCode = Sg1.OmsId
                            LEFT JOIN Subgroup Sg2 ON L.ContraSub = Sg2.OmsId
                            LEFT JOIN Subgroup Sg3 ON L.LinkedSubcode = Sg3.OmsId
                            WHERE L.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " UPDATE Pakka.SaleInvoiceDetail Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.SaleInvoiceDetail.DocId = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        AgL.ETrans.Rollback()
                        MsgBox(ex.Message)
                    End Try
                End If
            Next
        Next
    End Sub

    Public Sub FAddSale(Conn As Object, Cmd As Object, V_Type As String)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer

        UpdateLabel("Start Inserting Sale Invoices...")
        'LblProgress.Text = "Start Inserting Sale Invoices..."
        'LblProgress.Refresh()

        mQry = " Select Sg.Name As BillToPartyName, Sg1.Name As SaleToPartyName_Master,  H.*
            From SaleInvoice H 
            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
            LEFT JOIN SubGroup Sg1 ON H.SaleToParty = Sg1.SubCode
            Where H.UploadDate Is Null 
            And H.V_Type = '" & V_Type & "'"
        Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        mQry = " SELECT H.V_Type, H.ManualRefNo, I.Description As ItemDesc, 
                OrderH.ManualRefNo As OrderManualRefNo, L.*
                FROM SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN SaleOrder OrderH On L.SaleInvoice = OrderH.DocId
                LEFT JOIN Item I ON L.Item = I.Code
                Where H.UploadDate Is Null 
                And H.V_Type = '" & V_Type & "'"
        Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        For I = 0 To DtHeaderSource.Rows.Count - 1
            If DtSaleInvoice.Select("OMSId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'").Length = 0 Then
                Dim SaleInvoiceTableList(0) As FrmSaleInvoiceDirect.StructSaleInvoice
                Dim SaleInvoiceTable As New FrmSaleInvoiceDirect.StructSaleInvoice

                SaleInvoiceTable.DocID = ""
                SaleInvoiceTable.V_Type = AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))
                SaleInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                SaleInvoiceTable.Site_Code = AgL.XNull(DtHeaderSource.Rows(I)("Site_Code"))
                SaleInvoiceTable.Div_Code = AgL.XNull(DtHeaderSource.Rows(I)("Div_Code"))
                SaleInvoiceTable.V_No = 0
                SaleInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                SaleInvoiceTable.ManualRefNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                SaleInvoiceTable.SaleToParty = FGetSubCodeFromOMSId(AgL.XNull(DtHeaderSource.Rows(I)("SaleToParty")))
                SaleInvoiceTable.SaleToPartyName = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyName_Master"))
                SaleInvoiceTable.AgentCode = FGetSubCodeFromOMSId(AgL.XNull(DtHeaderSource.Rows(I)("Agent")))
                SaleInvoiceTable.AgentName = ""
                SaleInvoiceTable.BillToPartyCode = FGetSubCodeFromOMSId(AgL.XNull(DtHeaderSource.Rows(I)("BillToParty")))
                SaleInvoiceTable.BillToPartyName = AgL.XNull(DtHeaderSource.Rows(I)("BillToPartyName"))
                SaleInvoiceTable.SaleToPartyAddress = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyAddress"))
                SaleInvoiceTable.SaleToPartyCity = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyCity"))
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
                SaleInvoiceTable.OmsId = AgL.XNull(DtHeaderSource.Rows(I)("DocId"))
                SaleInvoiceTable.LockText = "Synced From Other Database."

                SaleInvoiceTable.Gross_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Gross_Amount"))
                SaleInvoiceTable.Taxable_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Taxable_Amount"))
                SaleInvoiceTable.Tax1 = AgL.VNull(DtHeaderSource.Rows(I)("Tax1"))
                SaleInvoiceTable.Tax2 = AgL.VNull(DtHeaderSource.Rows(I)("Tax2"))
                SaleInvoiceTable.Tax3 = AgL.VNull(DtHeaderSource.Rows(I)("Tax3"))
                SaleInvoiceTable.Tax4 = AgL.VNull(DtHeaderSource.Rows(I)("Tax4"))
                SaleInvoiceTable.Tax5 = AgL.VNull(DtHeaderSource.Rows(I)("Tax5"))
                SaleInvoiceTable.SubTotal1 = AgL.VNull(DtHeaderSource.Rows(I)("SubTotal1"))
                SaleInvoiceTable.Other_Charge = AgL.VNull(DtHeaderSource.Rows(I)("Other_Charge"))
                SaleInvoiceTable.Deduction = AgL.VNull(DtHeaderSource.Rows(I)("Deduction"))
                SaleInvoiceTable.Round_Off = AgL.VNull(DtHeaderSource.Rows(I)("Round_Off"))
                SaleInvoiceTable.Net_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Net_Amount"))

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
                    SaleInvoiceTable.Line_ItemCode = FGetItemCodeFromOMSId(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Item")))
                    SaleInvoiceTable.Line_ItemName = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ItemDesc"))
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


                    'If DtSaleInvoiceDetail_ForHeader.Columns.Contains("OrderManualRefNo") Then
                    '    mQry = " Select DocId
                    '            From SaleInvoice 
                    '            Where ManualRefNo = '" & AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("OrderManualRefNo")) & "'
                    '            And V_Type = '" & AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("OrderV_Type")) & "'"
                    '    Dim DtSaleOrder As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    '    If DtSaleOrder.Rows.Count > 0 Then
                    '        SaleInvoiceTable.Line_SaleInvoice = AgL.XNull(DtSaleOrder.Rows(0)("DocId"))
                    '        SaleInvoiceTable.Line_SaleInvoiceSr = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(0)("SaleInvoiceSr"))
                    '    End If
                    'End If

                    If AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("OrderManualRefNo")) <> "" Then
                        Dim DtRowSaleOrderDetail As DataRow() = DtSaleInvoiceDetail.Select("OMSId = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SaleInvoice")) +
                                                                AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SaleInvoiceSr"))))
                        If DtRowSaleOrderDetail.Length > 0 Then
                            SaleInvoiceTable.Line_SaleInvoice = AgL.XNull(DtRowSaleOrderDetail(0)("DocId"))
                            SaleInvoiceTable.Line_SaleInvoiceSr = AgL.XNull(DtRowSaleOrderDetail(0)("Sr"))
                        End If
                    End If



                    SaleInvoiceTable.Line_OmsId = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DocId")) + AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Sr"))
                    SaleInvoiceTable.Line_Rate = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate"))
                    SaleInvoiceTable.Line_DiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DiscountPer"))
                    SaleInvoiceTable.Line_DiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DiscountAmount"))
                    SaleInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountPer"))
                    SaleInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountAmount"))
                    SaleInvoiceTable.Line_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Amount"))
                    SaleInvoiceTable.Line_Remark = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Remark"))
                    SaleInvoiceTable.Line_BaleNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                    SaleInvoiceTable.Line_LotNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("LotNo"))
                    SaleInvoiceTable.Line_ReferenceDocId = ""
                    SaleInvoiceTable.Line_GrossWeight = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("GrossWeight"))
                    SaleInvoiceTable.Line_NetWeight = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("NetWeight"))
                    SaleInvoiceTable.Line_Gross_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Gross_Amount"))
                    SaleInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount"))
                    SaleInvoiceTable.Line_Tax1_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                    SaleInvoiceTable.Line_Tax1 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1"))
                    SaleInvoiceTable.Line_Tax2_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                    SaleInvoiceTable.Line_Tax2 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2"))
                    SaleInvoiceTable.Line_Tax3_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                    SaleInvoiceTable.Line_Tax3 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3"))
                    SaleInvoiceTable.Line_Tax4_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                    SaleInvoiceTable.Line_Tax4 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4"))
                    SaleInvoiceTable.Line_Tax5_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))
                    SaleInvoiceTable.Line_Tax5 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5"))
                    SaleInvoiceTable.Line_SubTotal1 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SubTotal1"))
                    SaleInvoiceTable.Line_Other_Charge = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                    SaleInvoiceTable.Line_Deduction = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                    SaleInvoiceTable.Line_Round_Off = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Round_Off"))
                    SaleInvoiceTable.Line_Net_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Net_Amount"))

                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                    ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)
                Next

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    Dim bDocId As String = FrmSaleInvoiceDirect.InsertSaleInvoice(SaleInvoiceTableList)
                    mQry = " UPDATE Pakka.SaleInvoice Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.SaleInvoice.DocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    UpdateLabel("Inserting Sale " & SaleInvoiceTable.V_Type & "-" & SaleInvoiceTable.ManualRefNo)
                    'LblProgress.Text = "Inserting Sale " & SaleInvoiceTable.V_Type & "-" & SaleInvoiceTable.ManualRefNo
                    'LblProgress.Refresh()


                    mQry = " UPDATE Pakka.SaleInvoiceDetail Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.SaleInvoiceDetail.DocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    If V_Type = "SO" Then
                        bIsSaleOrdersImportedSuccessfully = False
                    ElseIf V_Type = "SI" Then
                        bIsSaleInvoicesImportedSuccessfully = False
                    End If
                End Try
            End If
        Next
    End Sub
    Private Function FGetSubCodeFromOMSId(SubCode As String) As String
        Dim DtSubGroupRow As DataRow() = DtSubGroup.Select("OMSId = '" & SubCode & "'")
        If DtSubGroupRow.Length > 0 Then
            FGetSubCodeFromOMSId = DtSubGroupRow(0)("SubCode")
        Else
            FGetSubCodeFromOMSId = ""
        End If
    End Function
    Private Function FGetItemCodeFromOMSId(Code As String) As String
        Dim DtItemRow As DataRow() = DtItem.Select("OMSId = '" & Code & "'")
        If DtItemRow.Length > 0 Then
            FGetItemCodeFromOMSId = DtItemRow(0)("Code")
        Else
            FGetItemCodeFromOMSId = ""
        End If
    End Function
    Private Function FGetCityCodeFromOMSId(Code As String) As String
        Dim DtCityRow As DataRow() = DtCity.Select("OMSId = '" & Code & "'")
        If DtCityRow.Length > 0 Then
            FGetCityCodeFromOMSId = DtCityRow(0)("CityCode")
        Else
            FGetCityCodeFromOMSId = ""
        End If
    End Function
    Private Function FGetInterestSlabCodeFromOMSId(Code As String) As String
        Dim DtInterestSlabRow As DataRow() = DtInterestSlab.Select("OMSId = '" & Code & "'")
        If DtInterestSlabRow.Length > 0 Then
            FGetInterestSlabCodeFromOMSId = DtInterestSlabRow(0)("Code")
        Else
            FGetInterestSlabCodeFromOMSId = ""
        End If
    End Function

    Private Function FGetRateTypeCodeFromOMSId(Code As String) As String
        Dim DtRateTypeRow As DataRow() = DtRateType.Select("OMSId = '" & Code & "'")
        If DtRateTypeRow.Length > 0 Then
            FGetRateTypeCodeFromOMSId = DtRateTypeRow(0)("Code")
        Else
            FGetRateTypeCodeFromOMSId = ""
        End If
    End Function


    Public Sub FAddPurchase(Conn As Object, Cmd As Object, V_Type As String)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer

        UpdateLabel("Start Inserting Purchase Invoices...")
        'LblProgress.Text = "Start Inserting Purchase Invoices..."
        'LblProgress.Refresh()

        mQry = " Select Sg.Name As BillToPartyName, Sg1.Name As VendorName_Master,  H.*
            From PurchInvoice H 
            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
            LEFT JOIN SubGroup Sg1 ON H.Vendor = Sg1.SubCode
            Where H.UploadDate Is Null 
            And H.V_Type = '" & V_Type & "'
            AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(mFromDate).ToString("s")) & ""
        Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        mQry = " SELECT H.V_Type, H.ManualRefNo, I.Description As ItemDesc, 
                L.*
                FROM PurchInvoice H 
                LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN Item I ON L.Item = I.Code
                Where H.UploadDate Is Null 
                And H.V_Type = '" & V_Type & "'
                AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(mFromDate).ToString("s")) & ""
        Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        For I = 0 To DtHeaderSource.Rows.Count - 1
            If DtPurchInvoice.Select("OMSId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'").Length = 0 Then
                Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect.StructPurchInvoice
                Dim PurchInvoiceTable As New FrmPurchInvoiceDirect.StructPurchInvoice

                PurchInvoiceTable.DocID = ""
                PurchInvoiceTable.V_Type = AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))
                PurchInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                PurchInvoiceTable.Site_Code = AgL.XNull(DtHeaderSource.Rows(I)("Site_Code"))
                PurchInvoiceTable.Div_Code = AgL.XNull(DtHeaderSource.Rows(I)("Div_Code"))
                PurchInvoiceTable.V_No = 0
                PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                PurchInvoiceTable.ManualRefNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                PurchInvoiceTable.Vendor = FGetSubCodeFromOMSId(AgL.XNull(DtHeaderSource.Rows(I)("Vendor")))
                PurchInvoiceTable.VendorName = AgL.XNull(DtHeaderSource.Rows(I)("VendorName_Master"))
                PurchInvoiceTable.AgentCode = FGetSubCodeFromOMSId(AgL.XNull(DtHeaderSource.Rows(I)("Agent")))
                PurchInvoiceTable.AgentName = ""
                PurchInvoiceTable.BillToPartyCode = FGetSubCodeFromOMSId(AgL.XNull(DtHeaderSource.Rows(I)("BillToParty")))
                PurchInvoiceTable.BillToPartyName = AgL.XNull(DtHeaderSource.Rows(I)("BillToPartyName"))
                PurchInvoiceTable.VendorAddress = AgL.XNull(DtHeaderSource.Rows(I)("VendorAddress"))
                PurchInvoiceTable.VendorCity = AgL.XNull(DtHeaderSource.Rows(I)("VendorCity"))
                PurchInvoiceTable.VendorMobile = AgL.XNull(DtHeaderSource.Rows(I)("VendorMobile"))
                PurchInvoiceTable.VendorSalesTaxNo = AgL.XNull(DtHeaderSource.Rows(I)("VendorSalesTaxNo"))
                PurchInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtHeaderSource.Rows(I)("SalesTaxGroupParty"))
                PurchInvoiceTable.PlaceOfSupply = AgL.XNull(DtHeaderSource.Rows(I)("PlaceOfSupply"))
                PurchInvoiceTable.StructureCode = AgL.XNull(DtHeaderSource.Rows(I)("Structure"))
                PurchInvoiceTable.CustomFields = AgL.XNull(DtHeaderSource.Rows(I)("CustomFields"))
                PurchInvoiceTable.VendorDocNo = AgL.XNull(DtHeaderSource.Rows(I)("VendorDocNo"))
                PurchInvoiceTable.VendorDocDate = AgL.XNull(DtHeaderSource.Rows(I)("VendorDocDate"))
                PurchInvoiceTable.ReferenceDocId = ""
                PurchInvoiceTable.Tags = AgL.XNull(DtHeaderSource.Rows(I)("Tags"))
                PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
                PurchInvoiceTable.Status = "Active"
                PurchInvoiceTable.EntryBy = AgL.PubUserName
                PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                PurchInvoiceTable.ApproveBy = ""
                PurchInvoiceTable.ApproveDate = ""
                PurchInvoiceTable.MoveToLog = ""
                PurchInvoiceTable.MoveToLogDate = ""
                PurchInvoiceTable.UploadDate = ""

                Dim DtRowSaleInvoice As DataRow() = DtSaleInvoice.Select("OMSId = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("GenDocId"))))
                If DtRowSaleInvoice.Length > 0 Then
                    PurchInvoiceTable.GenDocId = AgL.XNull(DtRowSaleInvoice(0)("DocId"))
                End If

                PurchInvoiceTable.OmsId = AgL.XNull(DtHeaderSource.Rows(I)("DocId"))
                PurchInvoiceTable.LockText = "Synced From Other Database."

                PurchInvoiceTable.Gross_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Gross_Amount"))
                PurchInvoiceTable.Taxable_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Taxable_Amount"))
                PurchInvoiceTable.Tax1 = AgL.VNull(DtHeaderSource.Rows(I)("Tax1"))
                PurchInvoiceTable.Tax2 = AgL.VNull(DtHeaderSource.Rows(I)("Tax2"))
                PurchInvoiceTable.Tax3 = AgL.VNull(DtHeaderSource.Rows(I)("Tax3"))
                PurchInvoiceTable.Tax4 = AgL.VNull(DtHeaderSource.Rows(I)("Tax4"))
                PurchInvoiceTable.Tax5 = AgL.VNull(DtHeaderSource.Rows(I)("Tax5"))
                PurchInvoiceTable.SubTotal1 = AgL.VNull(DtHeaderSource.Rows(I)("SubTotal1"))
                PurchInvoiceTable.Other_Charge = AgL.VNull(DtHeaderSource.Rows(I)("Other_Charge"))
                PurchInvoiceTable.Deduction = AgL.VNull(DtHeaderSource.Rows(I)("Deduction"))
                PurchInvoiceTable.Round_Off = AgL.VNull(DtHeaderSource.Rows(I)("Round_Off"))
                PurchInvoiceTable.Net_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Net_Amount"))

                Dim DtPurchInvoiceDetail_ForHeader As New DataTable
                For M As Integer = 0 To DtLineDetailSource.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLineDetailSource.Columns(M).ColumnName
                    DtPurchInvoiceDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowPurchInvoiceDetail_ForHeader As DataRow() = DtLineDetailSource.Select("DocId = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("DocId"))))
                If DtRowPurchInvoiceDetail_ForHeader.Length > 0 Then
                    For M As Integer = 0 To DtRowPurchInvoiceDetail_ForHeader.Length - 1
                        DtPurchInvoiceDetail_ForHeader.Rows.Add()
                        For N As Integer = 0 To DtPurchInvoiceDetail_ForHeader.Columns.Count - 1
                            DtPurchInvoiceDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDetail_ForHeader(M)(N)
                        Next
                    Next
                End If


                For J = 0 To DtPurchInvoiceDetail_ForHeader.Rows.Count - 1
                    PurchInvoiceTable.Line_Sr = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr"))
                    PurchInvoiceTable.Line_ItemCode = FGetItemCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Item")))
                    PurchInvoiceTable.Line_ItemName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemDesc"))
                    PurchInvoiceTable.Line_Specification = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Specification"))
                    PurchInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SalesTaxGroupItem"))
                    PurchInvoiceTable.Line_ReferenceNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ReferenceNo"))
                    PurchInvoiceTable.Line_DocQty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocQty"))
                    PurchInvoiceTable.Line_FreeQty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("FreeQty"))
                    PurchInvoiceTable.Line_Qty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Qty"))
                    PurchInvoiceTable.Line_Unit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Unit"))
                    PurchInvoiceTable.Line_Pcs = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Pcs"))
                    PurchInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("UnitMultiplier"))
                    PurchInvoiceTable.Line_DealUnit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DealUnit"))
                    PurchInvoiceTable.Line_DocDealQty = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocDealQty"))

                    PurchInvoiceTable.Line_OmsId = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocId")) + AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr"))
                    PurchInvoiceTable.Line_Rate = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Rate"))
                    PurchInvoiceTable.Line_DiscountPer = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DiscountPer"))
                    PurchInvoiceTable.Line_DiscountAmount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DiscountAmount"))
                    PurchInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountPer"))
                    PurchInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountAmount"))
                    PurchInvoiceTable.Line_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                    PurchInvoiceTable.Line_Remark = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Remark"))
                    PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                    PurchInvoiceTable.Line_LotNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("LotNo"))
                    PurchInvoiceTable.Line_ReferenceDocId = ""
                    PurchInvoiceTable.Line_GrossWeight = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("GrossWeight"))
                    PurchInvoiceTable.Line_NetWeight = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("NetWeight"))
                    PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Gross_Amount"))
                    PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount"))
                    PurchInvoiceTable.Line_Tax1_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                    PurchInvoiceTable.Line_Tax1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1"))
                    PurchInvoiceTable.Line_Tax2_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                    PurchInvoiceTable.Line_Tax2 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2"))
                    PurchInvoiceTable.Line_Tax3_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                    PurchInvoiceTable.Line_Tax3 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3"))
                    PurchInvoiceTable.Line_Tax4_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                    PurchInvoiceTable.Line_Tax4 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4"))
                    PurchInvoiceTable.Line_Tax5_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))
                    PurchInvoiceTable.Line_Tax5 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5"))
                    PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SubTotal1"))
                    PurchInvoiceTable.Line_Other_Charge = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                    PurchInvoiceTable.Line_Deduction = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                    PurchInvoiceTable.Line_Round_Off = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Round_Off"))
                    PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Net_Amount"))

                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                    ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                Next

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    Dim bDocId As String = FrmPurchInvoiceDirect.InsertPurchInvoice(PurchInvoiceTableList)
                    mQry = " UPDATE Pakka.PurchInvoice Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.PurchInvoice.DocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    UpdateLabel("Inserting Purchase " & PurchInvoiceTable.V_Type & "-" & PurchInvoiceTable.ManualRefNo)
                    'LblProgress.Text = "Inserting Purchase " & PurchInvoiceTable.V_Type & "-" & PurchInvoiceTable.ManualRefNo
                    'LblProgress.Refresh()


                    mQry = " UPDATE Pakka.PurchInvoiceDetail Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.PurchInvoiceDetail.DocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    If V_Type = "PI" Then
                        bIsPurchaseInvoicesImportedSuccessfully = False
                    End If
                End Try
            End If
        Next
    End Sub
    Private Sub FUpdatePurchInvoice(Conn As Object, Cmd As Object)
        Connection_Pakka.Open()

        mQry = " Select * From PurchInvoice H Where UploadDate Is Null 
                AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(mFromDate).ToString("s")) & ""
        Dim DtPakka As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Connection_Pakka.Close()

        UpdateLabel("Start Updating Purchase Invoices...")
        'LblProgress.Text = "Start Updating Purchase Invoices..."
        'LblProgress.Refresh()

        Dim bSourceDocIdStr As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            If bSourceDocIdStr <> "" Then bSourceDocIdStr += ","
            bSourceDocIdStr += AgL.Chk_Text(AgL.XNull(DtPakka.Rows(I)("DocId")))
        Next

        mQry = " Select H.*, Sg1.OMSId As VendorOMSId, Sg2.OMSId As BillToPartyOMSId, Sg3.OmsId AS AgentOMSId 
                From PurchInvoice H
                LEFT JOIN SubGroup Sg1 On H.Vendor = Sg1.SubCode
                LEFT JOIN SubGroup Sg2 On H.BillToParty = Sg2.SubCode
                LEFT JOIN SubGroup Sg3 On H.Agent = Sg3.Subcode 
                Where H.OMSId In (" & bSourceDocIdStr & ") "
        Dim DtKachha As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            For J As Integer = 0 To DtKachha.Rows.Count - 1
                If AgL.XNull(DtPakka.Rows(I)("DocId")) = AgL.XNull(DtKachha.Rows(J)("OMSId")) Then
                    bUpdateClauseQry = ""
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "V_Date", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ManualRefNo")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "Vendor")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "BillToParty")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "Agent")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorName")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorAddress")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorPinCode")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorMobile")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorSalesTaxNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShipToAddress")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SalesTaxGroupParty")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PlaceOfSupply")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorDocNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorDocDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Remarks")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Gross_Amount", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Taxable_Amount", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax1_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax1", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax2_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax2", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax3_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax3", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax4_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax4", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax5_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax5", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SubTotal1", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Deduction_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Deduction", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Other_Charge_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Other_Charge", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Round_Off", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Net_Amount", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Status")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryBy")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorAadharNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "VendorPanNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SpecialDiscount_Per", "Number")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SpecialDiscount", "Number")


                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        UpdateLabel("Updating Purchase " & AgL.XNull(DtPakka.Rows(I)("V_Type")) & "-" & AgL.XNull(DtPakka.Rows(I)("ManualRefNo")))
                        'LblProgress.Text = "Updating Purchase " & AgL.XNull(DtPakka.Rows(I)("V_Type")) & "-" & AgL.XNull(DtPakka.Rows(I)("ManualRefNo"))
                        'LblProgress.Refresh()

                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE PurchInvoice Set " + bUpdateClauseQry + " Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        mQry = " UPDATE Pakka.PurchInvoice Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.PurchInvoice.DocId = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                        'For Line Logic

                        mQry = " Delete From PurchInvoiceDetail Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " INSERT INTO PurchInvoiceDetail (DocId, Sr, ReferenceNo, Barcode, Item, Specification, SalesTaxGroupItem, LotNo, BaleNo, Deal, ExpiryDate, LrNo, LrDate, Pcs, DocQty, FreeQty, Qty, RejQty, Unit, UnitMultiplier, DocDealQty, DealQty, RejDealQty, DealUnit, Rate, MRP, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount, Amount, ProfitMarginPer, Sale_Rate, ReferenceDocId, ReferenceTSr, ReferenceSr, 
                            PurchInvoice, PurchInvoiceSr, Godown, Remark, DimensionDetail, GrossWeight, NetWeight, Gross_Amount, Taxable_Amount, Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount, UploadDate, OmsId, BaleNoBarCode, AdditionPer, AdditionAmount, SpecialDiscount_Per, SpecialDiscount, LrBaleCode, LrCode)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.Sr, L.ReferenceNo, L.Barcode, I.Code AS Item, L.Specification, L.SalesTaxGroupItem, L.LotNo, L.BaleNo, L.Deal, L.ExpiryDate, L.LrNo, L.LrDate, L.Pcs, L.DocQty, L.FreeQty, L.Qty, L.RejQty, L.Unit, L.UnitMultiplier, L.DocDealQty, L.DealQty, L.RejDealQty, L.DealUnit, L.Rate, L.MRP, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, L.Amount, L.ProfitMarginPer, L.Sale_Rate, L.ReferenceDocId, L.ReferenceTSr, L.ReferenceSr, 
                            IfNull(Sid.DocID,'" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "') AS PurchInvoice, 
                            IfNull(Sid.Sr,L.Sr) AS PurchInvoiceSr,
                            L.Godown, L.Remark, L.DimensionDetail, L.GrossWeight, L.NetWeight, L.Gross_Amount, L.Taxable_Amount, L.Tax1_Per, L.Tax1, L.Tax2_Per, L.Tax2, L.Tax3_Per, L.Tax3, L.Tax4_Per, L.Tax4, L.Tax5_Per, L.Tax5, L.SubTotal1, L.Deduction_Per, L.Deduction, L.Other_Charge_Per, L.Other_Charge, L.Round_Off, L.Net_Amount, L.UploadDate, L.OmsId, L.BaleNoBarCode, L.AdditionPer, L.AdditionAmount, L.SpecialDiscount_Per, L.SpecialDiscount, L.LrBaleCode, L.LrCode
                            FROM Pakka.PurchInvoice H 
                            LEFT JOIN Pakka.PurchInvoiceDetail L ON H.DocID = L.DocID
                            LEFT JOIN Item I ON L.Item = I.OmsId
                            LEFT JOIN PurchInvoiceDetail Sid ON L.PurchInvoice || CAST(L.PurchInvoiceSr AS INTEGER) = Sid.OmsId 
                            WHERE H.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"

                        'mQry = "INSERT INTO PurchInvoiceDetail (DocId, Sr, Item, Specification, SalesTaxGroupItem, LotNo, BaleNo, Pcs, Deal, ExpiryDate, LrNo, LrDate, DocQty, FreeQty, Qty, RejQty, Unit, UnitMultiplier, DocDealQty, FreeDealQty, DealQty, RejDealQty, DealUnit, Rate, MRP, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount, Amount, ProfitMarginPer, Purch_Rate, ReferenceDocId, ReferenceDocIdTSr, ReferenceDocIdSr, PurchInvoice, PurchInvoiceSr, Godown, SalesRepresentative, Remark, DimensionDetail, GrossWeight, NetWeight, ReconcileDateTime, ReconcileBy, Gross_Amount, SpecialDiscount_Per, SpecialDiscount, Taxable_Amount, Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount, 
                        '        AdditionPer, AdditionAmount, OmsId, ItemState, Remarks1, Remarks2)
                        '        SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.Sr, I.Code AS Item, L.Specification, L.SalesTaxGroupItem, L.LotNo, L.BaleNo, L.Pcs, L.Deal, L.ExpiryDate, L.LrNo, 
                        '        L.LrDate, L.DocQty, L.FreeQty, L.Qty, L.RejQty, L.Unit, L.UnitMultiplier, L.DocDealQty, L.FreeDealQty, L.DealQty, 
                        '        L.RejDealQty, L.DealUnit, L.Rate, L.MRP, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, 
                        '        L.AdditionalDiscountAmount, L.Amount, L.ProfitMarginPer, L.Purch_Rate, L.ReferenceDocId, L.ReferenceDocIdTSr, 
                        '        L.ReferenceDocIdSr, 
                        '        IfNull(Sid.DocID,'" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' ) AS PurchInvoice, 
                        '        IfNull(Sid.Sr,L.Sr) AS PurchInvoiceSr, L.Godown, L.PurchsRepresentative, L.Remark, L.DimensionDetail, 
                        '        L.GrossWeight, L.NetWeight, L.ReconcileDateTime, L.ReconcileBy, L.Gross_Amount, L.SpecialDiscount_Per, 
                        '        L.SpecialDiscount, L.Taxable_Amount, L.Tax1_Per, L.Tax1, L.Tax2_Per, L.Tax2, L.Tax3_Per, L.Tax3, L.Tax4_Per, L.Tax4, 
                        '        L.Tax5_Per, L.Tax5, L.SubTotal1, L.Deduction_Per, L.Deduction, L.Other_Charge_Per, L.Other_Charge, L.Round_Off, 
                        '        L.Net_Amount, L.AdditionPer, L.AdditionAmount, 
                        '        L.DocId || Cast(L.Sr As nvarchar) As OmsId, 
                        '        L.ItemState, L.Remarks1, L.Remarks2 
                        '        FROM Pakka.PurchInvoice H 
                        '        LEFT JOIN Pakka.PurchInvoiceDetail L ON H.DocID = L.DocID
                        '        LEFT JOIN Item I ON L.Item = I.OmsId
                        '        LEFT JOIN PurchInvoiceDetail Sid ON L.PurchInvoice || CAST(L.PurchInvoiceSr AS INTEGER) = Sid.OmsId
                        '        WHERE H.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " INSERT INTO Stock (DocId, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, Subcode, Currency, 
                            SalesTaxGroupParty, Structure, BillingType, Item, Item_UID, LotNo, 
                            ProcessGroup, Godown, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss, DealQty_Rec, 
                            DealUnit, Rate, Amount, Addition, Deduction, NetAmount, Remarks, 
                            Process, Status, RecId, UID, FIFORate, FIFOAmt, AVGRate, AVGAmt, Cost, Doc_Qty, 
                            ReferenceDocID, FIFOValue, BaleNo, ProdOrder, ReferenceDocIDSr, ExpiryDate, 
                            MRP, NDP, CurrentStock, EType_IR, Landed_Value, OtherAdjustment, CostCenter, Specification, Manufacturer, SalesTaxGroupItem, ItemState)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.TSr, L.Sr, L.V_Type, L.V_Prefix, L.V_Date, 
                            " & AgL.XNull(DtKachha.Rows(J)("V_No")) & " As V_No, 
                            L.Div_Code, L.Site_Code, Sg.SubCode, L.Currency, 
                            L.SalesTaxGroupParty, L.Structure, L.BillingType, I.Code AS Item, L.Item_UID, L.LotNo, L.ProcessGroup, L.Godown, 
                            L.Qty_Iss, L.Qty_Rec, L.Unit, L.UnitMultiplier, L.DealQty_Iss, L.DealQty_Rec, L.DealUnit, L.Rate, L.Amount, 
                            L.Addition, L.Deduction, L.NetAmount, L.Remarks, L.Process, L.Status, L.RecId, L.UID, L.FIFORate, L.FIFOAmt, 
                            L.AVGRate, L.AVGAmt, L.Cost, L.Doc_Qty, L.ReferenceDocID, L.FIFOValue, L.BaleNo, L.ProdOrder, L.ReferenceDocIDSr, 
                            L.ExpiryDate, L.MRP, L.NDP, L.CurrentStock, L.EType_IR, L.Landed_Value, L.OtherAdjustment, L.CostCenter, 
                            L.Specification, L.Manufacturer, L.SalesTaxGroupItem, L.ItemState
                            FROM Pakka.Stock L 
                            LEFT JOIN Item I ON L.Item = I.OmsId
                            LEFT JOIN Subgroup Sg ON L.SubCode = Sg.OmsId
                            WHERE L.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, AmtDr, AmtCr, Chq_No, Chq_Date, Clg_Date, TDSCategory, TdsDesc, TdsOnAmt, TdsPer, Tds_Of_V_Sno, Narration, Site_Code, U_Name, U_EntDt, U_AE, DivCode, PQty, SQty, AgRefNo, GroupCode, GroupNature, RowId, UpLoadDate, AddBy, AddDate, ModifyBy, ModifyDate, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, OldDocid, CostCenter, System_Generated, FarmulaString, ContraText, RecId, FormulaString, OrignalAmt, TDSDeductFrom, ReferenceDocId, ReferenceDocIdSr, CreditDays, EffectiveDate, LinkedSubcode)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.V_SNo, 
                            " & AgL.XNull(DtKachha.Rows(J)("V_No")) & " As V_No, L.V_Type, L.V_Prefix, L.V_Date, IfNull(Sg3.Subcode,Sg1.Subcode) AS SubCode, Sg2.Subcode AS ContraSub, L.AmtDr, L.AmtCr, 
                            L.Chq_No, L.Chq_Date, L.Clg_Date, L.TDSCategory, L.TdsDesc, L.TdsOnAmt, L.TdsPer, L.Tds_Of_V_Sno, 
                            L.Narration, L.Site_Code, L.U_Name, L.U_EntDt, L.U_AE, L.DivCode, L.PQty, L.SQty, L.AgRefNo, L.GroupCode, L.GroupNature, 
                            L.RowId, L.UpLoadDate, L.AddBy, L.AddDate, L.ModifyBy, L.ModifyDate, L.ApprovedBy, L.ApprovedDate, L.GPX1, L.GPX2, 
                            L.GPN1, L.GPN2, L.OldDocid, L.CostCenter, L.System_Generated, L.FarmulaString, L.ContraText, L.RecId, L.FormulaString, 
                            L.OrignalAmt, L.TDSDeductFrom, L.ReferenceDocId, L.ReferenceDocIdSr, L.CreditDays, L.EffectiveDate, Sg3.Subcode AS LinkedSubcode
                            FROM Pakka.Ledger L 
                            LEFT JOIN Subgroup Sg1 ON L.SubCode = Sg1.OmsId
                            LEFT JOIN Subgroup Sg2 ON L.ContraSub = Sg2.OmsId
                            LEFT JOIN Subgroup Sg3 ON L.LinkedSubcode = Sg3.OmsId
                            WHERE L.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " UPDATE Pakka.PurchInvoiceDetail Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.PurchInvoiceDetail.DocId = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        AgL.ETrans.Rollback()
                        MsgBox(ex.Message)
                    End Try
                End If
            Next
        Next
    End Sub

    Public Sub FAddTransactionReferencesCancelled(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim J As Integer


        UpdateLabel("Start Inserting Transaction References Cancelled...")


        mQry = " Select *
            From TransactionReferences H 
            Where H.Type Is Not Null And H.UploadDate Is Null AND H.Type='Cancelled'"
        Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            For I = 0 To DtHeaderSource.Rows.Count - 1
                mQry = "Select H.* from TransactionReferences H 
                    Where H.DocId=(Select IfNull(Max(DocID),'') From LedgerHead Where OmsId = '" & DtHeaderSource.Rows(I)("DocID") & "')
                    And H.ReferenceDocId=(Select IfNull(Max(DocID),'') From LedgerHead Where OmsId = '" & DtHeaderSource.Rows(I)("ReferenceDocID") & "')"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count = 0 Then
                    Dim mDocID As String = AgL.Dman_Execute("Select IfNull(Max(DocID),'') From LedgerHead Where OmsId = '" & DtHeaderSource.Rows(I)("DocID") & "'", AgL.GCn).ExecuteScalar()
                    Dim mReferenceDocID As String = AgL.Dman_Execute("Select IfNull(Max(DocID),'') From LedgerHead Where OmsId = '" & DtHeaderSource.Rows(I)("ReferenceDocID") & "'", AgL.GCn).ExecuteScalar()
                    If mDocID <> "" And mReferenceDocID <> "" Then
                        mQry = "Insert Into TransactionReferences(DocID, DocIdSr, ReferenceDocID, Referencesr, Type, Remark)
                        Values (" & AgL.Chk_Text(mDocID) & ", " & AgL.Chk_Text(DtHeaderSource.Rows(I)("DocIDSr")) & ", " & AgL.Chk_Text(mReferenceDocID) & ", " & AgL.Chk_Text(DtHeaderSource.Rows(I)("ReferenceSr")) & ", " & AgL.Chk_Text(DtHeaderSource.Rows(I)("Type")) & ", " & AgL.Chk_Text(DtHeaderSource.Rows(I)("Remark")) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = " UPDATE Pakka.TransactionReferences Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.TransactionReferences.DocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'
                            And Pakka.TransactionReferences.ReferenceDocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("ReferenceDocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next
            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub FAddTransactionReferences(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim J As Integer


        UpdateLabel("Start Inserting Transaction References...")


        mQry = " Select *
            From TransactionReferences H 
            Where H.Type Is Not Null And H.UploadDate Is Null "
        Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            For I = 0 To DtHeaderSource.Rows.Count - 1
                mQry = "Select H.* from TransactionReferences H 
                    Where H.DocId=(Select IfNull(Max(DocID),'') From LedgerHead Where OmsId = '" & DtHeaderSource.Rows(I)("DocID") & "')
                    And H.ReferenceDocId=(Select IfNull(Max(DocID),'') From LedgerHead Where OmsId = '" & DtHeaderSource.Rows(I)("ReferenceDocID") & "')"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count = 0 Then
                    Dim mDocID As String = AgL.Dman_Execute("Select IfNull(Max(DocID),'') From LedgerHead Where OmsId = '" & DtHeaderSource.Rows(I)("DocID") & "'", AgL.GCn).ExecuteScalar()
                    Dim mReferenceDocID As String = AgL.Dman_Execute("Select IfNull(Max(DocID),'') From LedgerHead Where OmsId = '" & DtHeaderSource.Rows(I)("ReferenceDocID") & "'", AgL.GCn).ExecuteScalar()
                    If mDocID <> "" And mReferenceDocID <> "" Then
                        mQry = "Insert Into TransactionReferences(DocID, DocIdSr, ReferenceDocID, Referencesr, Type, Remark)
                        Values (" & AgL.Chk_Text(mDocID) & ", " & AgL.Chk_Text(DtHeaderSource.Rows(I)("DocIDSr")) & ", " & AgL.Chk_Text(mReferenceDocID) & ", " & AgL.Chk_Text(DtHeaderSource.Rows(I)("ReferenceSr")) & ", " & AgL.Chk_Text(DtHeaderSource.Rows(I)("Type")) & ", " & AgL.Chk_Text(DtHeaderSource.Rows(I)("Remark")) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = " UPDATE Pakka.TransactionReferences Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.TransactionReferences.DocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'
                            And Pakka.TransactionReferences.ReferenceDocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("ReferenceDocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next
            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub FAddLedgerHead(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer


        UpdateLabel("Start Inserting Financial Data...")
        'LblProgress.Text = "Start Inserting Financial Data..."
        'LblProgress.Refresh()

        mQry = " Select Sg.Name As PartyName_Master,  H.*, Hc.*
            From LedgerHead H 
            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode
            LEFT JOIN LedgerHeadCharges Hc On H.DocId = Hc.DocId
            Where H.UploadDate Is Null 
            And Vt.NCat Not In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "')
            And H.V_Type Not In ('JVA', 'VR','OPMT') 
            AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(mFromDate).ToString("s")) & ""
        Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        mQry = " SELECT H.V_Type, H.ManualRefNo, Sg1.Name As SubCodeName, 
                Sg2.Name As LinkedSubCodeName, L.*, Lc.*
                FROM LedgerHead H 
                LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID
                LEFT JOIN LedgerHeadDetailCharges Lc On L.DocId = Lc.DocId And L.Sr = Lc.Sr
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN SubGroup Sg1 On L.SubCode = Sg1.SubCode
                LEFT JOIN SubGroup Sg2 On L.LinkedSubCode = Sg2.SubCode
                Where H.UploadDate Is Null 
                And Vt.NCat Not In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "')
                And H.V_Type Not In ('JVA','VR','OPMT') 
                AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(mFromDate).ToString("s")) & ""
        Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        For I = 0 To DtHeaderSource.Rows.Count - 1
            If DtLedgerHead.Select("OMSId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'").Length = 0 Then
                Dim LedgerHeadTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim LedgerHeadTable As New FrmVoucherEntry.StructLedgerHead


                LedgerHeadTable.DocID = ""
                LedgerHeadTable.V_Type = AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))
                LedgerHeadTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                LedgerHeadTable.Site_Code = AgL.XNull(DtHeaderSource.Rows(I)("Site_Code"))
                LedgerHeadTable.Div_Code = AgL.XNull(DtHeaderSource.Rows(I)("Div_Code"))
                LedgerHeadTable.V_No = 0
                LedgerHeadTable.V_Date = ClsMain.FormatDate(AgL.XNull(DtHeaderSource.Rows(I)("V_Date")))
                LedgerHeadTable.ManualRefNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                LedgerHeadTable.Subcode = FGetSubCodeFromOMSId(AgL.XNull(DtHeaderSource.Rows(I)("SubCode")))
                LedgerHeadTable.LinkedSubcode = FGetSubCodeFromOMSId(AgL.XNull(DtHeaderSource.Rows(I)("LinkedSubCode")))
                LedgerHeadTable.SubcodeName = AgL.XNull(DtHeaderSource.Rows(I)("PartyName_Master"))
                LedgerHeadTable.SalesTaxGroupParty = AgL.XNull(DtHeaderSource.Rows(I)("SalesTaxGroupParty"))
                LedgerHeadTable.PlaceOfSupply = AgL.XNull(DtHeaderSource.Rows(I)("PlaceOfSupply"))
                LedgerHeadTable.StructureCode = AgL.XNull(DtHeaderSource.Rows(I)("Structure"))
                LedgerHeadTable.CustomFields = AgL.XNull(DtHeaderSource.Rows(I)("CustomFields"))
                LedgerHeadTable.PartyDocNo = AgL.XNull(DtHeaderSource.Rows(I)("PartyDocNo"))
                LedgerHeadTable.PartyDocDate = AgL.XNull(DtHeaderSource.Rows(I)("PartyDocDate"))
                LedgerHeadTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
                LedgerHeadTable.Status = "Active"
                LedgerHeadTable.EntryBy = AgL.PubUserName
                LedgerHeadTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                LedgerHeadTable.ApproveBy = ""
                LedgerHeadTable.ApproveDate = ""
                LedgerHeadTable.MoveToLog = ""
                LedgerHeadTable.MoveToLogDate = ""
                LedgerHeadTable.UploadDate = ""
                LedgerHeadTable.OMSId = AgL.XNull(DtHeaderSource.Rows(I)("DocId"))
                LedgerHeadTable.LockText = "Synced From Other Database."

                LedgerHeadTable.Gross_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Gross_Amount"))
                LedgerHeadTable.Taxable_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Taxable_Amount"))
                LedgerHeadTable.Tax1 = AgL.VNull(DtHeaderSource.Rows(I)("Tax1"))
                LedgerHeadTable.Tax2 = AgL.VNull(DtHeaderSource.Rows(I)("Tax2"))
                LedgerHeadTable.Tax3 = AgL.VNull(DtHeaderSource.Rows(I)("Tax3"))
                LedgerHeadTable.Tax4 = AgL.VNull(DtHeaderSource.Rows(I)("Tax4"))
                LedgerHeadTable.Tax5 = AgL.VNull(DtHeaderSource.Rows(I)("Tax5"))
                LedgerHeadTable.SubTotal1 = AgL.VNull(DtHeaderSource.Rows(I)("SubTotal1"))
                LedgerHeadTable.Other_Charge = AgL.VNull(DtHeaderSource.Rows(I)("Other_Charge"))
                LedgerHeadTable.Deduction = AgL.VNull(DtHeaderSource.Rows(I)("Deduction"))
                LedgerHeadTable.Round_Off = AgL.VNull(DtHeaderSource.Rows(I)("Round_Off"))
                LedgerHeadTable.Net_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Net_Amount"))

                Dim DtPurchInvoiceDetail_ForHeader As New DataTable
                For M As Integer = 0 To DtLineDetailSource.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLineDetailSource.Columns(M).ColumnName
                    DtPurchInvoiceDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowPurchInvoiceDetail_ForHeader As DataRow() = DtLineDetailSource.Select("DocId = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("DocId"))))
                If DtRowPurchInvoiceDetail_ForHeader.Length > 0 Then
                    For M As Integer = 0 To DtRowPurchInvoiceDetail_ForHeader.Length - 1
                        DtPurchInvoiceDetail_ForHeader.Rows.Add()
                        For N As Integer = 0 To DtPurchInvoiceDetail_ForHeader.Columns.Count - 1
                            DtPurchInvoiceDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDetail_ForHeader(M)(N)
                        Next
                    Next
                End If


                For J = 0 To DtPurchInvoiceDetail_ForHeader.Rows.Count - 1
                    LedgerHeadTable.Line_Sr = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr"))
                    LedgerHeadTable.Line_SubCode = FGetSubCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SubCode")))
                    LedgerHeadTable.Line_SubCodeName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SubCodeName"))
                    LedgerHeadTable.Line_LinkedSubCode = FGetSubCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("LinkedSubCode")))
                    LedgerHeadTable.Line_LinkedSubCodeName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("LinkedSubCodeName"))
                    LedgerHeadTable.Line_Specification = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Specification"))
                    LedgerHeadTable.Line_SalesTaxGroupItem = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SalesTaxGroupItem"))
                    LedgerHeadTable.Line_Qty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Qty"))
                    LedgerHeadTable.Line_Unit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Unit"))
                    LedgerHeadTable.Line_Rate = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Rate"))
                    LedgerHeadTable.Line_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                    LedgerHeadTable.Line_Amount_Cr = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("AmountCr"))
                    LedgerHeadTable.Line_ChqRefNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ChqRefNo"))
                    LedgerHeadTable.Line_ChqRefDate = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ChqRefDate"))
                    LedgerHeadTable.Line_Remarks = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Remarks"))

                    LedgerHeadTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Gross_Amount"))
                    LedgerHeadTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount"))
                    LedgerHeadTable.Line_Tax1_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                    LedgerHeadTable.Line_Tax1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1"))
                    LedgerHeadTable.Line_Tax2_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                    LedgerHeadTable.Line_Tax2 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2"))
                    LedgerHeadTable.Line_Tax3_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                    LedgerHeadTable.Line_Tax3 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3"))
                    LedgerHeadTable.Line_Tax4_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                    LedgerHeadTable.Line_Tax4 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4"))
                    LedgerHeadTable.Line_Tax5_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))
                    LedgerHeadTable.Line_Tax5 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5"))
                    LedgerHeadTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SubTotal1"))
                    LedgerHeadTable.Line_Other_Charge = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                    LedgerHeadTable.Line_Deduction = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                    LedgerHeadTable.Line_Round_Off = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Round_Off"))
                    LedgerHeadTable.Line_Net_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Net_Amount"))

                    LedgerHeadTableList(UBound(LedgerHeadTableList)) = LedgerHeadTable
                    ReDim Preserve LedgerHeadTableList(UBound(LedgerHeadTableList) + 1)
                Next

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"

                    Dim bDocId As String = FrmVoucherEntry.InsertLedgerHead(LedgerHeadTableList)
                    mQry = " UPDATE Pakka.LedgerHead Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.LedgerHead.DocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " UPDATE Pakka.LedgerHeadDetail Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.LedgerHeadDetail.DocId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    UpdateLabel("Inserting Ledger Head " & LedgerHeadTable.V_Type & "-" & LedgerHeadTable.ManualRefNo)
                    'LblProgress.Text = "Inserting Ledger Head " & LedgerHeadTable.V_Type & "-" & LedgerHeadTable.ManualRefNo
                    'LblProgress.Refresh()


                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                End Try
            End If
        Next
    End Sub
    Private Sub FUpdateLedgerHead(Conn As Object, Cmd As Object)
        Connection_Pakka.Open()

        mQry = " SELECT H.*
                FROM LedgerHead H 
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                Where H.UploadDate Is Null 
                And Vt.NCat Not In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "')
                AND Date(H.V_Date) >= " & AgL.Chk_Date(CDate(mFromDate).ToString("s")) & ""
        Dim DtPakka As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Connection_Pakka.Close()

        UpdateLabel("Start Updating Financial Data...")
        'LblProgress.Text = "Start Updating Financial Data..."
        'LblProgress.Refresh()

        Dim bSourceDocIdStr As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            If bSourceDocIdStr <> "" Then bSourceDocIdStr += ","
            bSourceDocIdStr += AgL.Chk_Text(AgL.XNull(DtPakka.Rows(I)("DocId")))
        Next


        mQry = " Select H.*, Sg.OMSId As SubCodeOMSId, LSg.OmsID as LinkedSubcodeOMSID 
                From LedgerHead H
                LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                LEFT JOIN SubGroup LSg On H.LinkedSubCode = LSg.SubCode
                Where H.OMSId In (" & bSourceDocIdStr & ") "
        Dim DtKachha As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            For J As Integer = 0 To DtKachha.Rows.Count - 1
                If AgL.XNull(DtPakka.Rows(I)("DocId")) = AgL.XNull(DtKachha.Rows(J)("OMSId")) Then
                    bUpdateClauseQry = ""
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "V_Date", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ManualRefNo")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "Subcode")
                    bUpdateClauseQry += FGetUpdateClauseForSubGroup(DtPakka, I, DtKachha, J, "LinkedSubcode")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartyName")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartyAddress")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartyPincode")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartyMobile")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartySalesTaxNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShipToAddress")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SalesTaxGroupParty")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PlaceOfSupply")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartyDocNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartyDocDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Remarks")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Status")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryBy")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryDate", "Date")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartyAadharNo")
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PartyPanNo")


                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        UpdateLabel("Updating Ledger Head " & AgL.XNull(DtPakka.Rows(I)("V_Type")) & "-" & AgL.XNull(DtPakka.Rows(I)("ManualRefNo")))
                        'LblProgress.Text = "Updating Ledger Head " & AgL.XNull(DtPakka.Rows(I)("V_Type")) & "-" & AgL.XNull(DtPakka.Rows(I)("ManualRefNo"))
                        'LblProgress.Refresh()


                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE LedgerHead Set " + bUpdateClauseQry + " Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        mQry = " UPDATE Pakka.LedgerHead Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.LedgerHead.DocId = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                        'For Line Logic

                        mQry = " Delete From LedgerHeadCharges Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Delete From LedgerHeadDetailCharges Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Delete From LedgerHeadDetail Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                        mQry = " INSERT INTO LedgerHeadCharges (DocID, Gross_Amount, Taxable_Amount, Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount, SpecialDiscount_Per, SpecialDiscount)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, Hc.Gross_Amount, Hc.Taxable_Amount, Hc.Tax1_Per, Hc.Tax1, Hc.Tax2_Per, Hc.Tax2, Hc.Tax3_Per, Hc.Tax3, Hc.Tax4_Per, Hc.Tax4, Hc.Tax5_Per, Hc.Tax5, Hc.SubTotal1, Hc.Deduction_Per, Hc.Deduction, Hc.Other_Charge_Per, Hc.Other_Charge, Hc.Round_Off, Hc.Net_Amount, Hc.SpecialDiscount_Per, Hc.SpecialDiscount
                            FROM Pakka.LedgerHead H 
                            LEFT JOIN Pakka.LedgerHeadCharges Hc ON H.DocID = Hc.DocID
                            WHERE H.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'
                            And Hc.DocId Is Not Null "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "INSERT INTO LedgerHeadDetail (DocId, Sr, Subcode, SpecificationDocID, SpecificationDocIDSr, Specification, SalesTaxGroupItem, Qty, Unit, Rate, Amount, ChqRefNo, ChqRefDate, EffectiveDate, Remarks, LinkedSubcode, HSN, AmountCr, AmountInWords, FormattedDate)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.Sr, Sg1.Subcode AS Subcode, 
                            IfNull(Sid.DocID,Pid.DocID) AS SpecificationDocID, IfNull(Sid.Sr,Pid.Sr) AS SpecificationDocIDSr, 
                            L.Specification, L.SalesTaxGroupItem, 
                            L.Qty, L.Unit, L.Rate, L.Amount, L.ChqRefNo, L.ChqRefDate, L.EffectiveDate, L.Remarks, 
                            Sg2.Subcode AS LinkedSubcode, L.HSN, L.AmountCr, L.AmountInWords, L.FormattedDate
                            FROM Pakka.LedgerHead H 
                            LEFT JOIN Pakka.LedgerHeadDetail L ON H.DocID = L.DocID
                            LEFT JOIN Subgroup Sg1 ON L.SubCode = Sg1.OmsId
                            LEFT JOIN Subgroup Sg2 ON L.LinkedSubcode = Sg2.OmsId
                            LEFT JOIN SaleInvoiceDetail Sid ON L.SpecificationDocID || CAST(L.SpecificationDocIDSr AS INTEGER) = Sid.OmsId
                            LEFT JOIN PurchInvoiceDetail Pid ON L.SpecificationDocID || CAST(L.SpecificationDocIDSr AS INTEGER) = Pid.OmsId
                            WHERE H.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " INSERT INTO LedgerHeadDetailCharges (DocID, Sr, Gross_Amount, Taxable_Amount, Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount, SpecialDiscount_Per, SpecialDiscount)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, Hdc.Sr,
                            Hdc.Gross_Amount, Hdc.Taxable_Amount, Hdc.Tax1_Per, Hdc.Tax1, Hdc.Tax2_Per, 
                            Hdc.Tax2, Hdc.Tax3_Per, Hdc.Tax3, Hdc.Tax4_Per, Hdc.Tax4, Hdc.Tax5_Per, 
                            Hdc.Tax5, Hdc.SubTotal1, Hdc.Deduction_Per, Hdc.Deduction, Hdc.Other_Charge_Per, 
                            Hdc.Other_Charge, Hdc.Round_Off, Hdc.Net_Amount, Hdc.SpecialDiscount_Per, Hdc.SpecialDiscount
                            FROM Pakka.LedgerHead H 
                            LEFT JOIN Pakka.LedgerHeadDetailCharges Hdc ON H.DocID = Hdc.DocID
                            WHERE H.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'
                            And Hdc.DocId Is Not Null "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, AmtDr, AmtCr, Chq_No, Chq_Date, Clg_Date, TDSCategory, TdsDesc, TdsOnAmt, TdsPer, Tds_Of_V_Sno, Narration, Site_Code, U_Name, U_EntDt, U_AE, DivCode, PQty, SQty, AgRefNo, GroupCode, GroupNature, RowId, UpLoadDate, AddBy, AddDate, ModifyBy, ModifyDate, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, OldDocid, CostCenter, System_Generated, FarmulaString, ContraText, RecId, FormulaString, OrignalAmt, TDSDeductFrom, ReferenceDocId, ReferenceDocIdSr, CreditDays, EffectiveDate, LinkedSubcode)
                            SELECT '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "' As DocId, L.V_SNo, 
                            " & AgL.XNull(DtKachha.Rows(J)("V_No")) & " As V_No, L.V_Type, L.V_Prefix, L.V_Date, Sg1.Subcode AS SubCode, Sg2.Subcode AS ContraSub, L.AmtDr, L.AmtCr, 
                            L.Chq_No, L.Chq_Date, L.Clg_Date, L.TDSCategory, L.TdsDesc, L.TdsOnAmt, L.TdsPer, L.Tds_Of_V_Sno, 
                            L.Narration, L.Site_Code, L.U_Name, L.U_EntDt, L.U_AE, L.DivCode, L.PQty, L.SQty, L.AgRefNo, L.GroupCode, L.GroupNature, 
                            L.RowId, L.UpLoadDate, L.AddBy, L.AddDate, L.ModifyBy, L.ModifyDate, L.ApprovedBy, L.ApprovedDate, L.GPX1, L.GPX2, 
                            L.GPN1, L.GPN2, L.OldDocid, L.CostCenter, L.System_Generated, L.FarmulaString, L.ContraText, L.RecId, L.FormulaString, 
                            L.OrignalAmt, L.TDSDeductFrom, L.ReferenceDocId, L.ReferenceDocIdSr, L.CreditDays, L.EffectiveDate, Sg3.Subcode AS LinkedSubcode
                            FROM Pakka.Ledger L 
                            LEFT JOIN Subgroup Sg1 ON L.SubCode = Sg1.OmsId
                            LEFT JOIN Subgroup Sg2 ON L.ContraSub = Sg2.OmsId
                            LEFT JOIN Subgroup Sg3 ON L.LinkedSubcode = Sg3.OmsId
                            WHERE L.DocID = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " UPDATE Pakka.LedgerHeadDetail Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.LedgerHeadDetail.DocId = '" & AgL.XNull(DtPakka.Rows(I)("DocId")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        AgL.ETrans.Rollback()
                        MsgBox(ex.Message)
                    End Try
                End If
            Next
        Next
    End Sub
    Private Sub FDeleteSubGroup(Conn As Object, Cmd As Object)

        UpdateLabel("Start Deleting Parties not found in Pakka...")
        'LblProgress.Text = "Start Deleting Parties not found in Pakka..."
        'LblProgress.Refresh()

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = " DELETE FROM Subgroup WHERE Subcode IN (
	            SELECT KSg.Subcode
	            FROM Subgroup KSg 
	            LEFT JOIN Pakka.Subgroup PSg ON Ksg.OmsId = Psg.Subcode
	            WHERE Ksg.OmsId IS NOT NULL AND PSg.Subcode IS NULL
            ) "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FDeleteItem(Conn As Object, Cmd As Object)
        UpdateLabel("Start Deleting Items not found in Pakka...")
        'LblProgress.Text = "Start Deleting Items not found in Pakka..."
        'LblProgress.Refresh()

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = " DELETE FROM Item WHERE Code IN (
	            SELECT KSg.Code
	            FROM Item KSg 
	            LEFT JOIN Pakka.Item PSg ON Ksg.OmsId = Psg.Code
	            WHERE Ksg.OmsId IS NOT NULL AND PSg.Code IS NULL
            ) "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FDeleteSale(Conn As Object, Cmd As Object)
        UpdateLabel("Start Deleting Sale Invoices not found in Pakka...")
        'LblProgress.Text = "Start Deleting Sale Invoices not found in Pakka..."
        'LblProgress.Refresh()

        mQry = " SELECT KSg.DocId, KSg.V_Type, KSg.ManualRefNo
                FROM SaleInvoice KSg 
                LEFT JOIN Pakka.SaleInvoice PSg ON Ksg.OmsId = Psg.DocId
                WHERE Ksg.OmsId IS NOT NULL AND PSg.DocId IS NULL "
        Dim DtSaleInvoiceForDeletion As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtSaleInvoiceForDeletion.Rows.Count - 1
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                UpdateLabel("Deleting Sale " & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("V_Type")) & "-" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("ManualRefNo")))
                'LblProgress.Text = "Deleting Sale " & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("V_Type")) & "-" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("ManualRefNo"))
                'LblProgress.Refresh()


                mQry = " Delete From SaleInvoiceTrnSetting Where DocId = '" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Delete From SaleInvoiceDimensionDetail Where DocId = '" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "Delete from SaleInvoiceBarcodeLastTransactionValues where DocID = '" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Delete From SaleInvoiceDetail Where DocId = '" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Delete From SaleInvoice Where DocId = '" & AgL.XNull(DtSaleInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                AgL.ETrans.Commit()
                mTrans = "Commit"
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        Next
    End Sub
    Private Sub FDeletePurchase(Conn As Object, Cmd As Object)
        UpdateLabel("Deleting Purchase Invoices not found in Pakka...")
        'LblProgress.Text = "Deleting Purchase Invoices not found in Pakka..."
        'LblProgress.Refresh()

        mQry = " SELECT KSg.DocId, KSg.V_Type, KSg.ManualRefNo
                FROM PurchInvoice KSg 
                LEFT JOIN Pakka.PurchInvoice PSg ON Ksg.OmsId = Psg.DocId
                WHERE Ksg.OmsId IS NOT NULL AND PSg.DocId IS NULL "
        Dim DtPurchInvoiceForDeletion As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtPurchInvoiceForDeletion.Rows.Count - 1
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                UpdateLabel("Deleting Purchase " & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("V_Type")) & "-" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("ManualRefNo")))
                'LblProgress.Text = "Deleting Purchase " & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("V_Type")) & "-" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("ManualRefNo"))
                'LblProgress.Refresh()


                mQry = " Delete From Stock Where DocId = '" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From PurchInvoiceTransport Where DocId = '" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From PurchInvoiceDimensionDetail Where DocId = '" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From BarcodeSiteDetail Where Code In (Select Code From Barcode Where  GenDocID ='" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "' ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From Barcode Where GenDocID ='" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "' "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From Stock Where DocID = (Select DocID From StockHead Where GenDocID ='" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "') "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From StockHeadDetail Where DocID = (Select DocID From StockHead Where GenDocID ='" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "') "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From StockHead Where GenDocID ='" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "' "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From PurchInvoiceDetail Where DocId ='" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "' "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From PurchInvoice Where DocId ='" & AgL.XNull(DtPurchInvoiceForDeletion.Rows(I)("DocId")) & "' "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                AgL.ETrans.Commit()
                mTrans = "Commit"
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        Next
    End Sub
    Private Sub FDeleteLedgerHead(Conn As Object, Cmd As Object)
        UpdateLabel("Deleting Financial Data not found in Pakka...")
        'LblProgress.Text = "Deleting Financial Data not found in Pakka..."
        'LblProgress.Refresh()

        mQry = " SELECT KSg.DocId, KSg.V_Type, KSg.ManualRefNo
                FROM LedgerHead KSg 
                LEFT JOIN Pakka.LedgerHead PSg ON Ksg.OmsId = Psg.DocId
                WHERE Ksg.OmsId IS NOT NULL AND PSg.DocId IS NULL "
        Dim DtLedgerHeadForDeletion As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtLedgerHeadForDeletion.Rows.Count - 1
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                UpdateLabel("Deleting LedgerHead " & AgL.XNull(DtLedgerHeadForDeletion.Rows(I)("V_Type")) & "-" & AgL.XNull(DtLedgerHeadForDeletion.Rows(I)("ManualRefNo")))
                'LblProgress.Text = "Deleting LedgerHead " & AgL.XNull(DtLedgerHeadForDeletion.Rows(I)("V_Type")) & "-" & AgL.XNull(DtLedgerHeadForDeletion.Rows(I)("ManualRefNo"))
                'LblProgress.Refresh()


                mQry = " Delete From Ledger Where DocId = '" & AgL.XNull(DtLedgerHeadForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Delete From LedgerHeadDetail Where DocId = '" & AgL.XNull(DtLedgerHeadForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Delete From LedgerHead Where DocId = '" & AgL.XNull(DtLedgerHeadForDeletion.Rows(I)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                AgL.ETrans.Commit()
                mTrans = "Commit"
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        Next
    End Sub

    Public Sub UpdateLabel(ByVal Value As String)
        'If LblProgress.InvokeRequired Then
        '    Dim dlg As New UpdateLabelInvoker(AddressOf UpdateLabel)
        '    'Me.LblProgress.Invoke(New UpdateLabelInvoker(AddressOf Me.UpdateLabel))
        '    LblProgress.Invoke(dlg, Value)
        '    LblProgress.Refresh()
        'Else
        '    LblProgress.Text = Value
        '    LblProgress.Refresh()
        'End If

        If Me.LblProgress.InvokeRequired Then
            Me.LblProgress.Invoke(New UpdateLabelInvoker(AddressOf Me.UpdateLabel), New Object() {Value})
            'Me.lblStatus.Invoke(New MethodInvoker(Me, DirectCast(Me.SaveCompleted, IntPtr)))
        Else
            Me.LblProgress.Text = Value
            LblProgress.Refresh()
        End If
    End Sub

    Private Sub LblProgress_TextChanged(sender As Object, e As EventArgs) Handles LblProgress.TextChanged

    End Sub



    'Private Delegate Sub SetControlTextDelegate(ByVal text As String)
    'Private Sub SetControlText(ByVal someText As String)
    '    If LblProgress.InvokeRequired Then
    '        Dim del As New SetControlTextDelegate(AddressOf SetControlText)
    '        Me.Invoke(del, New Object() {LblProgress, someText})
    '    Else
    '        LblProgress.Text = someText
    '    End If
    'End Sub
    ' Runs on a separate thread to the UI.
    'Private Sub WorkThread()
    '    Dim I As Integer
    '    ' We can't safely access any controls directly from this thread.
    '    ' So we must use our own Subs that will access the controls from
    '    ' the UI thread.
    '    SetButton1Enabled(False)
    '    SetControlText(LblProgress, "Processing...")
    '    Do Until i = 1000
    '        SetControlText(LblProgress, i.ToString)
    '        i += 1
    '    Loop
    '    SetButton1Enabled(True)
    'End Sub





    Public Sub FAddItemGroupPerson(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting Items Group Person...")


        mQry = "SELECT Ic.Description AS ItemCategoryDesc, Ig.Description AS ItemGroupDesc, Sg.Name AS PersonName, Igp.*
                FROM ItemGroupPerson Igp
                LEFT JOIN Item Ic ON Igp.ItemCategory = Ic.Code
                LEFT JOIN Item Ig ON Igp.ItemGroup = Ig.Code
                LEFT JOIN Subgroup Sg ON Igp.Person = Sg.Subcode "
        Dim DtItemGroupPersonSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        For I = 0 To DtItemGroupPersonSource.Rows.Count - 1
            Dim ItemGroupPersonTable As New FrmItemMaster.StructItemGroupPerson

            ItemGroupPersonTable.ItemCategory = FGetItemCodeFromOMSId(AgL.XNull(DtItemGroupPersonSource.Rows(I)("ItemCategory")))
            ItemGroupPersonTable.ItemGroup = FGetItemCodeFromOMSId(AgL.XNull(DtItemGroupPersonSource.Rows(I)("ItemGroup")))
            ItemGroupPersonTable.Person = FGetSubCodeFromOMSId(AgL.XNull(DtItemGroupPersonSource.Rows(I)("Person")))
            ItemGroupPersonTable.DiscountCalculationPattern = AgL.XNull(DtItemGroupPersonSource.Rows(I)("DiscountCalculationPattern"))
            ItemGroupPersonTable.DiscountPer = AgL.XNull(DtItemGroupPersonSource.Rows(I)("DiscountPer"))
            ItemGroupPersonTable.AdditionalDiscountPer = AgL.XNull(DtItemGroupPersonSource.Rows(I)("AdditionalDiscountPer"))
            ItemGroupPersonTable.AdditionalDiscountCalculationPattern = AgL.XNull(DtItemGroupPersonSource.Rows(I)("AdditionalDiscountCalculationPattern"))
            ItemGroupPersonTable.AdditionCalculationPattern = AgL.XNull(DtItemGroupPersonSource.Rows(I)("AdditionCalculationPattern"))
            ItemGroupPersonTable.AdditionPer = AgL.XNull(DtItemGroupPersonSource.Rows(I)("AdditionPer"))
            ItemGroupPersonTable.InterestSlab = AgL.XNull(DtItemGroupPersonSource.Rows(I)("InterestSlab"))

            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"
                FrmItemMaster.ImportItemGroupPersonTable(ItemGroupPersonTable)

                UpdateLabel("Inserting Item Group Person " + AgL.XNull(DtItemGroupPersonSource.Rows(I)("PersonName")))

                AgL.ETrans.Commit()
                mTrans = "Commit"
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
                bIsMastersImportedSuccessfully = False
            End Try
        Next
    End Sub

    Public Sub FAddItemGroupRateType(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting Items Group RateType...")


        mQry = "SELECT Ig.Description AS ItemGroupDesc, Rt.Description AS RateTypeName, Igp.*
                FROM ItemGroupRateType Igp                
                LEFT JOIN Item Ig ON Igp.Code = Ig.Code
                LEFT JOIN RateType Rt ON Igp.RateType = Rt.code "
        Dim DtItemGroupPersonSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        For I = 0 To DtItemGroupPersonSource.Rows.Count - 1
            Dim ItemGroupRateTypeTable As New FrmItemMaster.StructItemGroupRateType

            ItemGroupRateTypeTable.ItemGroup = FGetItemCodeFromOMSId(AgL.XNull(DtItemGroupPersonSource.Rows(I)("Code")))
            ItemGroupRateTypeTable.RateType = FGetRateTypeCodeFromOMSId(AgL.XNull(DtItemGroupPersonSource.Rows(I)("RateType")))
            ItemGroupRateTypeTable.Margin = AgL.XNull(DtItemGroupPersonSource.Rows(I)("Margin"))
            ItemGroupRateTypeTable.DiscountCalculationPattern = AgL.XNull(DtItemGroupPersonSource.Rows(I)("DiscountCalculationPattern"))
            ItemGroupRateTypeTable.DiscountPer = AgL.VNull(DtItemGroupPersonSource.Rows(I)("DiscountPer"))
            ItemGroupRateTypeTable.AdditionalDiscountPer = AgL.VNull(DtItemGroupPersonSource.Rows(I)("AdditionalDiscountPer"))
            ItemGroupRateTypeTable.AdditionalDiscountCalculationPattern = AgL.XNull(DtItemGroupPersonSource.Rows(I)("AdditionalDiscountCalculationPattern"))
            ItemGroupRateTypeTable.AdditionCalculationPattern = AgL.XNull(DtItemGroupPersonSource.Rows(I)("AdditionCalculationPattern"))
            ItemGroupRateTypeTable.AdditionPer = AgL.VNull(DtItemGroupPersonSource.Rows(I)("AdditionPer"))

            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"
                FrmItemMaster.ImportItemGroupRateTypeTable(ItemGroupRateTypeTable)

                UpdateLabel("Inserting Item Group RateType " + AgL.XNull(DtItemGroupPersonSource.Rows(I)("RateTypeName")))

                AgL.ETrans.Commit()
                mTrans = "Commit"
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
                bIsMastersImportedSuccessfully = False
            End Try
        Next
    End Sub


    Public Sub FAddPersonExtraDiscount(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting Person Extra Discount...")


        mQry = "SELECT Ic.Description AS ItemCategoryDesc, Ig.Description AS ItemGroupDesc, Sg.Name AS PersonName, Igp.*
                FROM PersonExtraDiscount Igp
                LEFT JOIN Item Ic ON Igp.ItemCategory = Ic.Code
                LEFT JOIN Item Ig ON Igp.ItemGroup = Ig.Code
                LEFT JOIN Subgroup Sg ON Igp.Person = Sg.Subcode "
        Dim DtPersonExtraDiscountSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        For I = 0 To DtPersonExtraDiscountSource.Rows.Count - 1
            Dim PersonExtraDiscountTable As New FrmPerson.StructPersonExtraDiscount

            PersonExtraDiscountTable.ItemCategory = FGetItemCodeFromOMSId(AgL.XNull(DtPersonExtraDiscountSource.Rows(I)("ItemCategory")))
            PersonExtraDiscountTable.ItemGroup = FGetItemCodeFromOMSId(AgL.XNull(DtPersonExtraDiscountSource.Rows(I)("ItemGroup")))
            PersonExtraDiscountTable.Person = FGetSubCodeFromOMSId(AgL.XNull(DtPersonExtraDiscountSource.Rows(I)("Person")))
            'PersonExtraDiscountTable.ExtraDiscountCalculationPattern = AgL.XNull(DtPersonExtraDiscountSource.Rows(I)("DiscountCalculationPattern"))
            PersonExtraDiscountTable.ExtraDiscountPer = AgL.XNull(DtPersonExtraDiscountSource.Rows(I)("ExtraDiscountPer"))

            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"
                FrmPerson.ImportPersonExtraDiscount(PersonExtraDiscountTable)

                UpdateLabel("Inserting Person Extra Discount " + AgL.XNull(DtPersonExtraDiscountSource.Rows(I)("PersonName")))

                AgL.ETrans.Commit()
                mTrans = "Commit"
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
                bIsMastersImportedSuccessfully = False
            End Try
        Next
    End Sub


    Private Sub CopyAttachments(SourceDocId As String, DestinationDocId As String)
        Try

            Dim SourceDatabasePath As String = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
            Dim SourcePath As String = System.IO.Path.GetDirectoryName(SourceDatabasePath) + "\Images\" + SourceDocId
            Dim DestinationPath As String = PubAttachmentPath + DestinationDocId

            If (Directory.Exists(SourcePath)) Then
                Dim bDirectoryInfo As New DirectoryInfo(SourcePath)
                Dim mFileArr As FileInfo() = bDirectoryInfo.GetFiles()

                Dim mFile As FileInfo
                For Each mFile In mFileArr
                    Dim destinationFileName As String = System.IO.Path.Combine(DestinationPath, mFile.Name)
                    My.Computer.FileSystem.CopyFile(SourcePath + "\" + mFile.Name, destinationFileName, True)
                    My.Computer.FileSystem.DeleteFile(SourcePath + "\" + mFile.Name)
                Next mFile
                My.Computer.FileSystem.DeleteDirectory(SourcePath, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in Copy attachment function")
        End Try
    End Sub
    Private Sub FSyncDocuments()
        Try

            mQry = "Select 'SubGroup-' || SubCode As KachhaSubCode, Name As KachhaName, 'SubGroup-' || OMSId As PakksaSubCode
                From SubGroup Where OMSId Is Not Null"
            Dim DtSubGroup As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For I As Integer = 0 To DtSubGroup.Rows.Count - 1
                UpdateLabel("Transfering Images For : " + AgL.XNull(DtSubGroup.Rows(I)("KachhaName")))
                CopyAttachments(AgL.XNull(DtSubGroup.Rows(I)("PakksaSubCode")), AgL.XNull(DtSubGroup.Rows(I)("KachhaSubCode")))
            Next
        Catch ex As Exception
            MsgBox(ex.Message & " in syncing images for party")
        End Try

        Try

            mQry = "Select DocId As KachhaSaleOrder, V_Type || '-' || ManualRefNo As KachhaSaleOrderNo, OMSId As PakksaSaleOrder
                From SaleOrder Where OMSId Is Not Null"
            Dim DtSaleOrder As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For I As Integer = 0 To DtSaleOrder.Rows.Count - 1
                UpdateLabel("Transfering Images For Pakka Sale Order : " + AgL.XNull(DtSaleOrder.Rows(I)("KachhaSaleOrderNo")))
                CopyAttachments(AgL.XNull(DtSaleOrder.Rows(I)("PakksaSaleOrder")), AgL.XNull(DtSaleOrder.Rows(I)("KachhaSaleOrder")))
            Next
        Catch ex As Exception
            MsgBox(ex.Message & " in syncing images for Sales Order")
        End Try


        'Try
        '    mQry = "Select H.DocId As KachhaSaleInvoice, H.V_Type || '-' || H.ManualRefNo As KachhaSaleInvoiceNo, 
        '        H.AMSDocId As PakkaSaleInvoice, H.AMSDocNo As PakkaSaleInvoiceNo
        '        From SaleInvoice H
        '        Where H.V_Type = 'WSI'
        '        And H.AMSDocId Is Not Null "
        '    Dim DtSaleInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '    For I As Integer = 0 To DtSaleInvoice.Rows.Count - 1
        '        UpdateLabel("Transfering Images For Pakka Sale Invoice : " + AgL.XNull(DtSaleInvoice.Rows(I)("PakkaSaleInvoiceNo")))
        '        CopyAttachments(AgL.XNull(DtSaleInvoice.Rows(I)("PakkaSaleInvoice")), AgL.XNull(DtSaleInvoice.Rows(I)("KachhaSaleInvoice")))
        '    Next
        'Catch ex As Exception
        '    MsgBox(ex.Message & " in syncing images for Sale Invoice")
        'End Try


        Try
            mQry = "Select H.DocId As KachhaPurchInvoice, H.V_Type || '-' || H.ManualRefNo As KachhaPurchInvoiceNo, 
                H.AMSDocId As PakkaPurchInvoice, H.AmsDocNo As PakkaPurchInvoiceNo
                From PurchInvoice H
                Where H.V_Type = 'WPI'
                And H.AMSDocId Is Not Null "
            Dim DtPurchInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For I As Integer = 0 To DtPurchInvoice.Rows.Count - 1
                UpdateLabel("Transfering Images For Pakka Purch Invoice : " + AgL.XNull(DtPurchInvoice.Rows(I)("PakkaPurchInvoiceNo")))
                CopyAttachments(AgL.XNull(DtPurchInvoice.Rows(I)("PakkaPurchInvoice")), AgL.XNull(DtPurchInvoice.Rows(I)("KachhaPurchInvoice")))
            Next
        Catch ex As Exception
            MsgBox(ex.Message & " in syncing images for purchase Invoice")
        End Try

        FRemoveAttachments()

        MsgBox("Process Complete.", MsgBoxStyle.Information)
        UpdateLabel(" ")
    End Sub
    Private Sub BtnSyncImages_Click(sender As Object, e As EventArgs) Handles BtnSyncImages.Click
        BtnSync.Enabled = False
        BtnSyncImages.Enabled = False
        _backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        _backgroundWorker1.WorkerSupportsCancellation = False
        _backgroundWorker1.WorkerReportsProgress = False
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FSyncDocuments)
        _backgroundWorker1.RunWorkerAsync()
    End Sub








    Public Sub FAddInterestSlab(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting Interest Slab...")

        mQry = "Select I.* From InterestSlab I Where I.UploadDate Is Null "
        Dim DtInterestSlabSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bLastInterestSlabCode As String = AgL.GetMaxId("InterestSlab", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtInterestSlabSource.Rows.Count - 1
            If DtInterestSlab.Select("OMSId = '" & DtInterestSlabSource.Rows(I)("Code") & "'").Length = 0 Then
                Dim InterestSlabTableList(0) As FrmInterestSlab.StructInterestSlab
                Dim InterestSlabTable As New FrmInterestSlab.StructInterestSlab


                Dim bInterestSlabCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastInterestSlabCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                InterestSlabTable.Code = bInterestSlabCode
                InterestSlabTable.Description = AgL.XNull(DtInterestSlabSource.Rows(I)("Description"))
                InterestSlabTable.LeaverageDays = AgL.XNull(DtInterestSlabSource.Rows(I)("LeaverageDays"))
                InterestSlabTable.EntryBy = AgL.PubUserName
                InterestSlabTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                InterestSlabTable.EntryType = "Add"
                InterestSlabTable.EntryStatus = ClsMain.LogStatus.LogOpen
                InterestSlabTable.Div_Code = AgL.PubDivCode
                InterestSlabTable.Status = "Active"
                InterestSlabTable.LockText = "Synced From Other Database."
                InterestSlabTable.OMSId = AgL.XNull(DtInterestSlabSource.Rows(I)("Code"))
                InterestSlabTable.IsSystemDefine = 0

                mQry = " Select * From InterestSlabDetail Where Code = '" & AgL.XNull(DtInterestSlabSource.Rows(I)("Code")) & "'"
                Dim DtInterestSlabDetailSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

                For J As Integer = 0 To DtInterestSlabDetailSource.Rows.Count - 1
                    InterestSlabTable.Line_DaysGreaterThan = AgL.XNull(DtInterestSlabDetailSource.Rows(J)("DaysGreaterThan"))
                    InterestSlabTable.Line_InterestRate = AgL.VNull(DtInterestSlabDetailSource.Rows(J)("InterestRate"))

                    InterestSlabTableList(UBound(InterestSlabTableList)) = InterestSlabTable
                    ReDim Preserve InterestSlabTableList(UBound(InterestSlabTableList) + 1)
                Next


                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmInterestSlab.ImportInterestSlabTable(InterestSlabTableList)

                    UpdateLabel("Inserting Interest Slab " + InterestSlabTable.Description)

                    mQry = " UPDATE Pakka.InterestSlab Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                        Where Pakka.InterestSlab.Code = '" & AgL.XNull(DtInterestSlabSource.Rows(I)("Code")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next
    End Sub

    Public Sub FAddRateType(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting Interest Slab...")

        mQry = "Select I.* From RateType I Where I.UploadDate Is Null "
        Dim DtRateTypeSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bLastRateTypeCode As String = AgL.GetMaxId("RateType", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtRateTypeSource.Rows.Count - 1
            Dim RateTypeTableList(0) As FrmRateType.StructRateType
            Dim RateTypeTable As New FrmRateType.StructRateType


            Dim bRateTypeCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastRateTypeCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

            RateTypeTable.Code = bRateTypeCode
            RateTypeTable.Description = AgL.XNull(DtRateTypeSource.Rows(I)("Description"))
            RateTypeTable.EntryBy = AgL.PubUserName
            RateTypeTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            RateTypeTable.EntryType = "Add"
            RateTypeTable.EntryStatus = ClsMain.LogStatus.LogOpen
            RateTypeTable.Div_Code = AgL.PubDivCode
            RateTypeTable.Status = "Active"
            RateTypeTable.LockText = "Synced From Other Database."
            RateTypeTable.OMSId = AgL.XNull(DtRateTypeSource.Rows(I)("Code"))

            RateTypeTableList(0) = RateTypeTable

            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"
                FrmRateType.ImportRateTypeTable(RateTypeTableList)

                UpdateLabel("Inserting Interest Slab " + RateTypeTable.Description)

                mQry = " UPDATE Pakka.RateType Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                        Where Pakka.RateType.Code = '" & AgL.XNull(DtRateTypeSource.Rows(I)("Code")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                AgL.ETrans.Commit()
                mTrans = "Commit"
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
                bIsMastersImportedSuccessfully = False
            End Try
        Next
    End Sub









    Private Sub FUpdateInterestSlab(Conn As Object, Cmd As Object)
        Connection_Pakka.Open()

        mQry = "Select I.* From InterestSlab I Where I.UploadDate Is Null "
        Dim DtPakka As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Connection_Pakka.Close()

        UpdateLabel("Start Updating Financial Data...")
        'LblProgress.Text = "Start Updating Financial Data..."
        'LblProgress.Refresh()

        Dim bSourceDocIdStr As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            If bSourceDocIdStr <> "" Then bSourceDocIdStr += ","
            bSourceDocIdStr += AgL.Chk_Text(AgL.XNull(DtPakka.Rows(I)("Code")))
        Next

        If bSourceDocIdStr = "" Then bSourceDocIdStr = "''"
        mQry = " Select H.* From InterestSlab H
                Where H.OMSId In (" & bSourceDocIdStr & ") "
        Dim DtKachha As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            For J As Integer = 0 To DtKachha.Rows.Count - 1
                If AgL.XNull(DtPakka.Rows(I)("Code")) = AgL.XNull(DtKachha.Rows(J)("OMSId")) Then
                    bUpdateClauseQry = ""
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Description")

                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"

                        UpdateLabel("Updating Interest Slab " & AgL.XNull(DtPakka.Rows(I)("Description")) & "-" & AgL.XNull(DtPakka.Rows(I)("Description")))

                        If bUpdateClauseQry <> "" Then
                            bUpdateClauseQry = bUpdateClauseQry.Substring(0, bUpdateClauseQry.Length - 1)
                            mQry = " UPDATE InterestSlab Set " + bUpdateClauseQry + " Where Code = '" & AgL.XNull(DtKachha.Rows(J)("Code")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If


                        'For Line Logic

                        mQry = " Delete From InterestSlabDetail Where Code = '" & AgL.XNull(DtKachha.Rows(J)("Code")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "INSERT INTO InterestSlabDetail (Code, DaysGreaterThan, InterestRate)
                                Select '" & AgL.XNull(DtKachha.Rows(J)("Code")) & "' As Code, 
                                DaysGreaterThan, InterestRate
                                From Pakka.InterestSlabDetail
                                Where Code = '" & AgL.XNull(DtPakka.Rows(I)("Code")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " UPDATE Pakka.InterestSlab Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & "
                        Where Pakka.InterestSlab.Code = '" & AgL.XNull(DtPakka.Rows(I)("Code")) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                    Catch ex As Exception
                        AgL.ETrans.Rollback()
                        MsgBox(ex.Message)
                    End Try
                End If
            Next
        Next
    End Sub









    Public Sub FAddArea(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting Area...")

        mQry = "Select I.* From Area I Where I.UploadDate Is Null "
        Dim DtAreaSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bLastAreaCode As String = AgL.GetMaxId("Area", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtAreaSource.Rows.Count - 1
            If DtArea.Select("OMSId = '" & DtAreaSource.Rows(I)("Code") & "'").Length = 0 Then
                Dim AreaTableList(0) As FrmArea.StructArea
                Dim AreaTable As New FrmArea.StructArea

                Dim bAreaCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastAreaCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                AreaTable.Code = bAreaCode
                AreaTable.Description = AgL.XNull(DtAreaSource.Rows(I)("Description"))
                AreaTable.EntryBy = AgL.PubUserName
                AreaTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                AreaTable.EntryType = "Add"
                AreaTable.EntryStatus = ClsMain.LogStatus.LogOpen
                AreaTable.OMSId = AgL.XNull(DtAreaSource.Rows(I)("Code"))

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmArea.ImportAreaTable(AreaTable)

                    UpdateLabel("Inserting Area " + AreaTable.Description)

                    mQry = " UPDATE Pakka.Area Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                        Where Pakka.Area.Code = '" & AgL.XNull(DtAreaSource.Rows(I)("Code")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next
    End Sub



    Public Sub FAddZone(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting Zone...")

        mQry = "Select I.* From Zone I Where I.UploadDate Is Null "
        Dim DtZoneSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bLastZoneCode As String = AgL.GetMaxId("Zone", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtZoneSource.Rows.Count - 1
            If DtZone.Select("OMSId = '" & DtZoneSource.Rows(I)("Code") & "'").Length = 0 Then
                Dim ZoneTableList(0) As FrmZone.StructZone
                Dim ZoneTable As New FrmZone.StructZone

                Dim bZoneCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastZoneCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ZoneTable.Code = bZoneCode
                ZoneTable.Description = AgL.XNull(DtZoneSource.Rows(I)("Description"))
                ZoneTable.EntryBy = AgL.PubUserName
                ZoneTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ZoneTable.EntryType = "Add"
                ZoneTable.EntryStatus = ClsMain.LogStatus.LogOpen
                ZoneTable.OMSId = AgL.XNull(DtZoneSource.Rows(I)("Code"))

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmZone.ImportZoneTable(ZoneTable)

                    UpdateLabel("Inserting Zone " + ZoneTable.Description)

                    mQry = " UPDATE Pakka.Zone Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                        Where Pakka.Zone.Code = '" & AgL.XNull(DtZoneSource.Rows(I)("Code")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next
    End Sub
    Public Sub FAddCity(Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        UpdateLabel(" Start Inserting City...")

        mQry = "Select I.* From City I Where I.UploadDate Is Null "
        Dim DtCitySource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bLastCityCode As String = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtCitySource.Rows.Count - 1
            If DtCity.Select("OMSId = '" & DtCitySource.Rows(I)("CityCode") & "'").Length = 0 Then
                Dim CityTableList(0) As FrmCity.StructCity
                Dim CityTable As New FrmCity.StructCity

                Dim bCityCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastCityCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                CityTable.CityCode = bCityCode
                CityTable.CityName = AgL.XNull(DtCitySource.Rows(I)("CityName"))
                CityTable.State = AgL.XNull(DtCitySource.Rows(I)("State"))
                CityTable.EntryBy = AgL.PubUserName
                CityTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                CityTable.EntryType = "Add"
                CityTable.EntryStatus = ClsMain.LogStatus.LogOpen
                CityTable.OMSId = AgL.XNull(DtCitySource.Rows(I)("CityCode"))

                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"
                    FrmCity.ImportCityTable(CityTable)

                    UpdateLabel("Inserting City " + CityTable.CityName)

                    mQry = " UPDATE Pakka.City Set UploadDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                            Where Pakka.City.CityCode = '" & AgL.XNull(DtCitySource.Rows(I)("CityCode")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = "Commit"
                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                    bIsMastersImportedSuccessfully = False
                End Try
            End If
        Next
    End Sub
    Private Sub FRemoveAttachments()
        Dim SourceDatabasePath As String = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        Dim SourcePath As String = System.IO.Path.GetDirectoryName(SourceDatabasePath) + "\Images\"
        Dim Directories() As String = IO.Directory.GetDirectories(SourcePath)

        Dim FolderList As String = ""
        Dim FolderName As String = ""
        For Each Directory As String In Directories
            FolderName = Path.GetFileName(Directory)
            If FolderName.Contains("PI") Or FolderName.Contains("PR") Then
                If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From PurchInvoice Where DocId = '" & FolderName & "'", AgL.GCn).ExecuteScalar()) = 0 Then
                    If FolderList <> "" Then FolderList += ","
                    FolderList += Path.GetFileName(Directory)
                End If
            ElseIf FolderName.Contains("SI") Or FolderName.Contains("SR") Or FolderName.Contains("SO") Then
                If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From SaleInvoice Where DocId = '" & FolderName & "'", AgL.GCn).ExecuteScalar()) = 0 Then
                    If FolderList <> "" Then FolderList += ","
                    FolderList += Path.GetFileName(Directory)
                End If
            End If
        Next

        If FolderList <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "FolderListToDelete.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "FolderListToDelete.txt", FolderList, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "FolderListToDelete.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "FolderListToDelete.txt", FolderList, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "FolderListToDelete.txt")

            If MsgBox("Do You Want To Delete These Extra Folders ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim FolderArr() As String = FolderList.Split(",")
                For I As Integer = 0 To FolderArr.Length - 1
                    My.Computer.FileSystem.DeleteDirectory(SourcePath + FolderArr(I), FileIO.DeleteDirectoryOption.DeleteAllContents)
                Next
            End If
        End If
    End Sub
End Class