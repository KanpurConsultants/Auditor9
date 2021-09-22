Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Module MdlFunction
    Dim mQry As String = ""


    Public Function FOpenIni(ByVal StrIniPath As String, ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        Dim OLECmd As New OleDb.OleDbCommand
        Dim BlnRtn As Boolean = False
        Dim ECmd As Object

        Try
            AgL = New AgLibrary.ClsMain : AgL.AglObj = AgL
            ClsMain_Structure = New AgStructure.ClsMain(AgL)
            'ClsMain_Purchase = New Purchase.ClsMain(AgL)
            'ClsMain_Sales = New Sales.ClsMain(AgL)
            ClsMain_CustomFields = New AgCustomFields.ClsMain(AgL)
            'ClsMain_ReportLayout = New ReportLayout.ClsMain(AgL)
            ClsMain_EMail = New EMail.ClsMain(AgL)

            AgL.PubDBUserSQL = "sa"
            AgL.PubDBPasswordSQL = ""
            AgL.PubServerName = AgL.INIRead(StrIniPath, "CompanyInfo", "Server", "")
            AgL.PubReportPath = AgL.INIRead(StrIniPath, "Reports", "Path", "")
            AgL.PubCompanyDBPath = AgL.INIRead(StrIniPath, "CompanyInfo", "Path", "")
            AgL.PubCompanyDBName = AgL.INIRead(StrIniPath, "CompanyInfo", "DbName", "")
            AgL.PubChkPasswordSQL = AgL.INIRead(StrIniPath, "Security", "PasswordSQL", "")
            AgL.PubChkPasswordAccess = AgL.INIRead(StrIniPath, "Security", "PasswordAccess", "")
            AgL.PubDataBackUpPath = AgL.INIRead(StrIniPath, "CompanyInfo", "BackupPath", "")
            AgL.PubReportPath_CommonData = AgL.INIRead(StrIniPath, "Reports", "CommonData", AgL.PubReportPath)
            AgL.PubReportPath_Utility = AgL.INIRead(StrIniPath, "Reports", "Utility", AgL.PubReportPath)
            AgL.PubIsDatabaseEncrypted = AgL.INIRead(StrIniPath, "CompanyInfo", "Encryption", "")

            AgL.PubReportPath = My.Application.Info.DirectoryPath & "\Reports"
            AgIniVar = New AgLibrary.ClsIniVariables(AgL)

            BlnRtn = AgIniVar.FOpenIni(StrUserName, StrPassword)

            OLECmd = Nothing
        Catch Ex As Exception
            BlnRtn = False
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        Finally
            ECmd = Nothing
            AgPL = New AgLibrary.ClsPrinting(AgL)

            FOpenIni = BlnRtn
        End Try
    End Function

    Public Sub FOpenConnection(ByVal StrCompanyCode As String)
        Dim ADTemp As OleDb.OleDbDataAdapter = Nothing

        Dim DTTemp As New DataTable
        Dim mQry As String
        Try
            mQry = "Select * From AgReports_Enviro Where Comp_Code='" & StrCompanyCode & "'"

            DTTemp = AgL.FillData(mQry, AgL.ECompConn).tables(0)
            If DTTemp.Rows.Count > 0 Then
                AgL.PubCompAdd1 = AgL.XNull(DTTemp.Rows(0).Item("address1"))
                AgL.PubCompAdd2 = AgL.XNull(DTTemp.Rows(0).Item("address2"))
                AgL.PubCompCST = AgL.XNull(DTTemp.Rows(0).Item("cstno"))
                AgL.PubCompName = AgL.XNull(DTTemp.Rows(0).Item("Comp_Name"))
                AgL.PubCompPhone = AgL.XNull(DTTemp.Rows(0).Item("phone"))
                AgL.PubCompTIN = AgL.XNull(DTTemp.Rows(0).Item("TinNo"))
                AgL.PubDBName = AgL.XNull(DTTemp.Rows(0).Item("CentralData_Path"))
                PubReportDataPath = AgL.XNull(DTTemp.Rows(0).Item("ReportData_Path"))
                AgL.PubEndDate = AgL.XNull(DTTemp.Rows(0).Item("End_Dt"))
                AgL.PubStartDate = AgL.XNull(DTTemp.Rows(0).Item("Start_Dt"))
                AgL.PubCompCity = AgL.XNull(DTTemp.Rows(0).Item("City"))
                AgL.PubCompPinCode = AgL.XNull(DTTemp.Rows(0).Item("PIN"))
            Else
                AgL.PubCompAdd1 = ""
                AgL.PubCompAdd2 = ""
                AgL.PubCompCST = ""
                AgL.PubCompName = ""
                AgL.PubCompPhone = ""
                AgL.PubCompTIN = ""
                AgL.PubEndDate = ""
                AgL.PubStartDate = ""
                AgL.PubCompCity = ""
                AgL.PubCompPinCode = ""

            End If


            GcnTrans = New SQLite.SQLiteConnection()
            AgL.GCn = New SQLite.SQLiteConnection()
            If UCase(Trim(AgL.PubChkPasswordSQL)) = "Y" Then
                GcnTrans.ConnectionString = "Persist Security Info=False;User ID='sa';pwd=" & StrDBPasswordSQL & ";Initial Catalog=" & PubReportDataPath & ";Data Source=" & AgL.PubServerName & ";Connect TimeOut=1024"
                AgL.GCn.ConnectionString = "Persist Security Info=False;User ID='sa';pwd=" & StrDBPasswordSQL & ";Initial Catalog=" & AgL.PubDBName & ";Data Source=" & AgL.PubServerName & ";Connect TimeOut=1024"
            Else
                GcnTrans.ConnectionString = "Persist Security Info=False;User ID='sa';pwd=;Initial Catalog=" & PubReportDataPath & ";Data Source=" & AgL.PubServerName & ";Connection TimeOut=1024;"
                AgL.GCn.ConnectionString = "Persist Security Info=False;User ID='sa';pwd=;Initial Catalog=" & AgL.PubDBName & ";Data Source=" & AgL.PubServerName & ";Connection TimeOut=1024;"
            End If

            GcnTrans.Open()
            AgL.GCn.Open()

            DTTemp.Clear()
            DTTemp = AgL.FillData("Select GetDate() As SrvDate ", GcnTrans)
            If DTTemp.Rows.Count > 0 Then
                AgL.PubLoginDate = Now() 'Format(AgL.XNull(DTTemp.Rows(0).Item("SrvDate")), "Short Date")
                AgL.PubLastTransactionDate = Now()
            End If
            AgL.PubMachineName = Customised.My.Computer.Name

            'Call PLib.Ini_PubEnviroVariables(PLib)



        Catch Ex As Exception
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub


    'Public Sub IniDtEnviro()
    '    Call IniDtCommon_Enviro()


    'End Sub

    'Public Sub IniDtCommon_Enviro()

    '    AgL.PubDtEnviro = AgL.FillData("SELECT E.* FROM Enviro E  WHERE E.Site_Code ='" & AgL.PubSiteCode & "'", AgL.GcnMain).Tables(0)
    'End Sub
    Public Sub MDI_Load_Things(objMdi As Object)
        On Error Resume Next

        Dim dtTemp As DataTable


        dtTemp = AgL.FillData("Select D.Div_Name, D.ShortName, Sg.DispName 
                    From Division D 
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    Where D.Div_Code = '" & AgL.PubDivCode & "'", AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            AgL.PubDivName = AgL.XNull(dtTemp.Rows(0)("Div_Name"))
            AgL.PubDivShortName = AgL.XNull(dtTemp.Rows(0)("ShortName"))
            AgL.PubDivPrintName = AgL.XNull(dtTemp.Rows(0)("DispName"))
        End If

        dtTemp = AgL.FillData("Select Sm.Name, SM.ShortName, 
                        Case IfNull(Sm.Ho_Yn,'N') When 'N' Then 0 When '' Then 0 Else 1 End As IsHO 
                        From SiteMast Sm Where Sm.Code = '" & AgL.PubSiteCode & "' ", AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            AgL.PubSiteName = AgL.XNull(dtTemp.Rows(0)("Name"))
            AgL.PubSiteShortName = AgL.XNull(dtTemp.Rows(0)("ShortName"))
            AgL.PubIsHo = AgL.VNull(dtTemp.Rows(0).Item("IsHO"))
        End If

        AgL.PubSiteStateCode = AgL.Dman_Execute("Select C.State From SiteMast S Left Join City C On S.City_Code = C.CityCode Where S.Code = '" & AgL.PubSiteCode & "' ", AgL.GCn).ExecuteScalar

        AgL.PubDivisionCount = AgL.Dman_Execute("Select Count(*) From Division", AgL.GcnMain).ExecuteScalar()
        AgL.PubSiteCount = AgL.Dman_Execute("Select Count(*) From SiteMast", AgL.GcnMain).ExecuteScalar()

        If AgL.Dman_Execute("Select Count(*) from LedgerHead Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
            mQry = "Update LedgerHead Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) from StockHead Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
            mQry = "Update StockHead Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) from SaleInvoice Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
            mQry = "Update SaleInvoice Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) from PurchInvoice Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
            mQry = "Update PurchInvoice Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) from SaleEnquiry Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
            mQry = "Update SaleEnquiry Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) from PurchPlan Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
            mQry = "Update PurchPlan Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        mQry = " Delete From StockVirtual "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = "SELECT * FROM Menus "
        AgL.PubDtMenus = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "SELECT IfNull(D.ScopeOfWork,'') FROM Division D WHERE D.Div_Code = '" & AgL.PubDivCode & "' "
        AgL.PubScopeOfWork = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar


        AgL.PubCrystalDocument.Load(AgL.PubReportPath + "\SaleInvoice_Print.rpt")

        ClsMain.FCreateItemDataTable()
        ClsMain.FCreateSettingDataTable()
        ClsMain.FCreateEntrySettingDataTable()
        ClsMain.LoadVoucherTypeDateLock()
        ClsMain.LoadVoucherTypeTimePlan()
        ClsMain.LoadFinancialYearDateLock()


        AgL.PubCaptionDimension1 = ClsMain.FGetSettings(SettingFields.Dimension1Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionDimension2 = ClsMain.FGetSettings(SettingFields.Dimension2Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionDimension3 = ClsMain.FGetSettings(SettingFields.Dimension3Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionDimension4 = ClsMain.FGetSettings(SettingFields.Dimension4Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubPrintDivisionShortNameOnDocumentsYn = ClsMain.FGetSettings(SettingFields.PrintDivisionShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubPrintSiteShortNameOnDocumentsYn = ClsMain.FGetSettings(SettingFields.PrintSiteShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionItemType = ClsMain.FGetSettings(SettingFields.ItemTypeCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionItemType = "" Then AgL.PubCaptionItemType = "Item Type"
        AgL.PubCaptionItemCategory = ClsMain.FGetSettings(SettingFields.ItemCategoryCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionItemCategory = "" Then AgL.PubCaptionItemCategory = "Item Category"
        AgL.PubCaptionItemGroup = ClsMain.FGetSettings(SettingFields.ItemGroupCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionItemGroup = "" Then AgL.PubCaptionItemGroup = "Item Group"
        AgL.PubCaptionItem = ClsMain.FGetSettings(SettingFields.ItemCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionItem = "" Then AgL.PubCaptionItem = "Item"
        AgL.PubCaptionBarcode = ClsMain.FGetSettings(SettingFields.BarcodeCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionDimension1 = ClsMain.FGetSettings(SettingFields.Dimension1Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionDimension2 = ClsMain.FGetSettings(SettingFields.Dimension2Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionDimension3 = ClsMain.FGetSettings(SettingFields.Dimension3Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionDimension4 = ClsMain.FGetSettings(SettingFields.Dimension4Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubCaptionLineDiscount = ClsMain.FGetSettings(SettingFields.LineDiscountCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionLineDiscount = "" Then AgL.PubCaptionLineDiscount = "Disc."
        AgL.PubCaptionLineAdditionalDiscount = ClsMain.FGetSettings(SettingFields.LineAdditionalDiscountCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionLineAdditionalDiscount = "" Then AgL.PubCaptionLineAdditionalDiscount = "A.Disc."
        AgL.PubCaptionLineAddition = ClsMain.FGetSettings(SettingFields.LineAdditionCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionLineAddition = "" Then AgL.PubCaptionLineAddition = "Addition"
        AgL.PubCaptionLotNo = ClsMain.FGetSettings(SettingFields.LotNoCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionLotNo = "" Then AgL.PubCaptionLotNo = "Lot No"
        AgL.PubCaptionPcs = ClsMain.FGetSettings(SettingFields.PcsCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionPcs = "" Then AgL.PubCaptionPcs = "Pcs"


        AgL.PubCaptionDocQty = ClsMain.FGetSettings(SettingFields.DocQtyCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionDocQty = "" Then AgL.PubCaptionDocQty = "Doc Qty"
        AgL.PubCaptionLossQty = ClsMain.FGetSettings(SettingFields.LossQtyCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionLossQty = "" Then AgL.PubCaptionLossQty = "Loss Qty"
        AgL.PubCaptionQty = ClsMain.FGetSettings(SettingFields.QtyCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionQty = "" Then AgL.PubCaptionQty = "Qty"



        AgL.PubCaptionDocDealQty = ClsMain.FGetSettings(SettingFields.DocDealQtyCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionDocDealQty = "" Then AgL.PubCaptionDocDealQty = "Doc Deal Qty"
        AgL.PubCaptionLossDealQty = ClsMain.FGetSettings(SettingFields.LossDealQtyCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionLossDealQty = "" Then AgL.PubCaptionLossDealQty = "Loss Deal Qty"
        AgL.PubCaptionDealQty = ClsMain.FGetSettings(SettingFields.DealQtyCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionDealQty = "" Then AgL.PubCaptionDealQty = "Deal Qty"


        AgL.PubCaptionCustomer = ClsMain.FGetSettings(SettingFields.CustomerCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionCustomer = "" Then AgL.PubCaptionCustomer = "Sale To Party"
        AgL.PubCaptionSupplier = ClsMain.FGetSettings(SettingFields.SupplierCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionSupplier = "" Then AgL.PubCaptionSupplier = "Vendor"
        AgL.PubCaptionLinkedParty = ClsMain.FGetSettings(SettingFields.LinkedPartyCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If AgL.PubCaptionLinkedParty = "" Then AgL.PubCaptionSupplier = "Linked Party"



        AgL.PubPrintDivisionShortNameOnDocumentsYn = ClsMain.FGetSettings(SettingFields.PrintDivisionShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        AgL.PubPrintSiteShortNameOnDocumentsYn = ClsMain.FGetSettings(SettingFields.PrintSiteShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")


        Dim ClsObj As New ClsMain(AgL)
        'ClsObj.()
        Dim ClsObjTemplateUpdateTableStructure As New AgTemplate.ClsMain(AgL)
        Dim ClsObjStructure As New AgStructure.ClsMain(AgL)
        Dim ClsObjCustomFields As New AgCustomFields.ClsMain(AgL)




        FSetDimensionCaptionForMdi(objMdi)
        Dim iVar As New AgLibrary.ClsIniVariables(AgL)
        iVar.IniEnviro()
    End Sub

    Public Sub FSetDimensionCaptionForMdi(objMdi As Object)
        Dim menues As New List(Of ToolStripItem)
        For Each t As ToolStripItem In objMdi.MnuMain.Items
            GetMenues(t, menues)
        Next

        For Each Mnu As ToolStripItem In menues
            If Mnu.Text.Contains("Item Type") And AgL.PubCaptionItemType <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Item Type", AgL.PubCaptionItemType)
            ElseIf Mnu.Text.Contains("Item Category") And AgL.PubCaptionItemCategory <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Item Category", AgL.PubCaptionItemCategory)
            ElseIf Mnu.Text.Contains("Item Group") And AgL.PubCaptionItemGroup <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Item Group", AgL.PubCaptionItemGroup)
            ElseIf Mnu.Text.Contains("Item") And AgL.PubCaptionItem <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Item", AgL.PubCaptionItem)
            End If
            If Mnu.Text.Contains("Barcode") And AgL.PubCaptionBarcode <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Barcode", AgL.PubCaptionBarcode)
            End If


            If Mnu.Text.Contains("Dimension1") And AgL.PubCaptionDimension1 <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Dimension1", AgL.PubCaptionDimension1)
            End If
            If Mnu.Text.Contains("Dimension2") And AgL.PubCaptionDimension2 <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Dimension2", AgL.PubCaptionDimension2)
            End If
            If Mnu.Text.Contains("Dimension3") And AgL.PubCaptionDimension3 <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Dimension3", AgL.PubCaptionDimension3)
            End If
            If Mnu.Text.Contains("Dimension4") And AgL.PubCaptionDimension4 <> "" Then
                Mnu.Text = Mnu.ToString.Replace("Dimension4", AgL.PubCaptionDimension4)
            End If
        Next
    End Sub
    Public Sub GetMenues(ByVal Current As ToolStripItem, ByRef menues As List(Of ToolStripItem))
        menues.Add(Current)
        If TypeOf (Current) Is ToolStripMenuItem Then
            For Each menu As ToolStripItem In DirectCast(Current, ToolStripMenuItem).DropDownItems
                GetMenues(menu, menues)
            Next
        End If
    End Sub
End Module