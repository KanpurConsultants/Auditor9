Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class MDIMain
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub MDIMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim mCount As Integer = 0
        If e.KeyCode = Keys.Escape Then
            For Each ChildForm As Form In Me.MdiChildren
                mCount = mCount + 1
            Next

            If mCount = 0 Then
                If MsgBox("Do You Want to Exit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'End
                End If
            End If
        End If

        'If e.KeyCode = (Keys.S And e.Alt) Then
        '    Dim eventargs As New System.Windows.Forms.ToolStripItemClickedEventArgs(MnuSalesEntry)
        '    Mnu_DropDownItemClicked(MnuSalesEntry.GetCurrentParent, eventargs)
        'End If
        'FOpenEntryFromShortCut(e)
    End Sub

    Public Function getx()
        Dim dpiX As Double
        Dim dpiPer As Double

        dpiX = Screen.PrimaryScreen.Bounds.Width
        dpiPer = Math.Round(dpiX / 1024, 0)
        MsgBox(dpiPer)
        Return dpiPer
    End Function
    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DtTemp As DataTable
        Dim mQry As String

        Try
            If AgL Is Nothing Then
                If FOpenIni(StrPath + IniName, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
                    'If FOpenIni(StrPath + IniName, "Sa", "") Then
                    AgL.PubSiteCode = "3"
                    AgL.PubDivCode = "D"
                    AgL.PubLoginDate = DateTime.Now()
                    AgL.PubLastTransactionDate = Now()
                    'Dim clsf As New ClsMain(AgL)
                    'clsf.UpdateTableStructure()
                    'End


                    AgIniVar.FOpenConnection("5", AgL.PubSiteCode, False)
                End If
                AgL.PubStopWatch.Start()

                AgL.PubDivCode = "D"

                Try
                    mCrd.Load(AgL.PubReportPath & "\" & "SaleInvoice_Print.rpt")
                Catch ex As Exception
                End Try

                'DtTemp = AgL.FillData("Select D.Div_Name, D.ShortName, Sg.DispName 
                '    From Division D 
                '    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                '    Where D.Div_Code = '" & AgL.PubDivCode & "'", AgL.GCn).Tables(0)
                'If DtTemp.Rows.Count > 0 Then
                '    AgL.PubDivName = AgL.XNull(DtTemp.Rows(0)("Div_Name"))
                '    AgL.PubDivShortName = AgL.XNull(DtTemp.Rows(0)("ShortName"))
                '    AgL.PubDivPrintName = AgL.XNull(DtTemp.Rows(0)("DispName"))
                'End If

                'DtTemp = AgL.FillData("Select Sm.Name, SM.ShortName From SiteMast Sm Where Sm.Code = '" & AgL.PubSiteCode & "' ", AgL.GCn).Tables(0)
                'If DtTemp.Rows.Count > 0 Then
                '    AgL.PubSiteName = AgL.XNull(DtTemp.Rows(0)("Name"))
                '    AgL.PubSiteShortName = AgL.XNull(DtTemp.Rows(0)("ShortName"))
                'End If

                'AgL.PubSiteStateCode = AgL.Dman_Execute("Select C.State From SiteMast S Left Join City C On S.City_Code = C.CityCode Where S.Code = '" & AgL.PubSiteCode & "' ", AgL.GCn).ExecuteScalar

                'AgL.PubDivisionCount = AgL.Dman_Execute("Select Count(*) From Division", AgL.GcnMain).ExecuteScalar()
                'AgL.PubSiteCount = AgL.Dman_Execute("Select Count(*) From SiteMast", AgL.GcnMain).ExecuteScalar()

                'If AgL.Dman_Execute("Select Count(*) from LedgerHead Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
                '    mQry = "Update LedgerHead Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
                '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                'End If
                'If AgL.Dman_Execute("Select Count(*) from StockHead Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
                '    mQry = "Update StockHead Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
                '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                'End If
                'If AgL.Dman_Execute("Select Count(*) from SaleInvoice Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
                '    mQry = "Update SaleInvoice Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
                '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                'End If
                'If AgL.Dman_Execute("Select Count(*) from PurchInvoice Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
                '    mQry = "Update PurchInvoice Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
                '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                'End If
                'If AgL.Dman_Execute("Select Count(*) from SaleEnquiry Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
                '    mQry = "Update SaleEnquiry Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
                '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                'End If
                'If AgL.Dman_Execute("Select Count(*) from PurchPlan Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar() > 0 Then
                '    mQry = "Update PurchPlan Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
                '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                'End If
                'mQry = " Delete From StockVirtual "
                'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                'Try
                '    AgL.PubCrystalDocument.Load(AgL.PubReportPath + "\SaleInvoice_Print.rpt")
                'Catch ex As Exception
                '    MsgBox(ex.Message & " While loading PubCrystalDocument ")
                'End Try

                'ClsMain.FCreateItemDataTable()
                'ClsMain.FCreateSettingDataTable()
                'ClsMain.FCreateEntrySettingDataTable()
                'ClsMain.LoadVoucherTypeDateLock()
                'ClsMain.LoadVoucherTypeTimePlan()


                'AgL.PubCaptionDimension1 = ClsMain.FGetSettings(SettingFields.Dimension1Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionDimension2 = ClsMain.FGetSettings(SettingFields.Dimension2Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionDimension3 = ClsMain.FGetSettings(SettingFields.Dimension3Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionDimension4 = ClsMain.FGetSettings(SettingFields.Dimension4Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubPrintDivisionShortNameOnDocumentsYn = ClsMain.FGetSettings(SettingFields.PrintDivisionShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubPrintSiteShortNameOnDocumentsYn = ClsMain.FGetSettings(SettingFields.PrintSiteShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionItemType = ClsMain.FGetSettings(SettingFields.ItemTypeCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'If AgL.PubCaptionItemType = "" Then AgL.PubCaptionItemType = "Item Type"
                'AgL.PubCaptionItemCategory = ClsMain.FGetSettings(SettingFields.ItemCategoryCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'If AgL.PubCaptionItemCategory = "" Then AgL.PubCaptionItemCategory = "Item Category"
                'AgL.PubCaptionItemGroup = ClsMain.FGetSettings(SettingFields.ItemGroupCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'If AgL.PubCaptionItemGroup = "" Then AgL.PubCaptionItemGroup = "Item Group"
                'AgL.PubCaptionItem = ClsMain.FGetSettings(SettingFields.ItemCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'If AgL.PubCaptionItem = "" Then AgL.PubCaptionItem = "Item"
                'AgL.PubCaptionBarcode = ClsMain.FGetSettings(SettingFields.BarcodeCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionDimension1 = ClsMain.FGetSettings(SettingFields.Dimension1Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionDimension2 = ClsMain.FGetSettings(SettingFields.Dimension2Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionDimension3 = ClsMain.FGetSettings(SettingFields.Dimension3Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionDimension4 = ClsMain.FGetSettings(SettingFields.Dimension4Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubCaptionLineDiscount = ClsMain.FGetSettings(SettingFields.LineDiscountCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'If AgL.PubCaptionLineDiscount = "" Then AgL.PubCaptionLineDiscount = "Disc."
                'AgL.PubCaptionLineAdditionalDiscount = ClsMain.FGetSettings(SettingFields.LineAdditionalDiscountCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'If AgL.PubCaptionLineAdditionalDiscount = "" Then AgL.PubCaptionLineAdditionalDiscount = "A.Disc."
                'AgL.PubCaptionLineAddition = ClsMain.FGetSettings(SettingFields.LineAdditionCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'If AgL.PubCaptionLineAddition = "" Then AgL.PubCaptionLineAddition = "Addition"




                'AgL.PubPrintDivisionShortNameOnDocumentsYn = ClsMain.FGetSettings(SettingFields.PrintDivisionShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                'AgL.PubPrintSiteShortNameOnDocumentsYn = ClsMain.FGetSettings(SettingFields.PrintSiteShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")


                'Dim ClsObj As New ClsMain(AgL)
                ''ClsObj.()
                'Dim ClsObjTemplateUpdateTableStructure As New AgTemplate.ClsMain(AgL)
                'Dim ClsObjStructure As New AgStructure.ClsMain(AgL)
                'Dim ClsObjCustomFields As New AgCustomFields.ClsMain(AgL)




                'FSetDimensionCaptionForMdi()
                'Dim iVar As New AgLibrary.ClsIniVariables(AgL)
                'iVar.IniEnviro()

                MDI_Load_Things(Me)

                'If Not ClsMain.IsScopeOfWorkContains(IndustryType.CarpetIndustry) Then
                '    MnuPlanning.Visible = False
                '    MnuWeaving.Visible = False
                '    MnuDyeing.Visible = False
                '    MnuFinishing.Visible = False
                'End If
            End If

            Dim attachmentPath As String = ""
            attachmentPath = AgL.INIRead(StrPath + IniName, "CompanyInfo", "AttachmentPath", "")
            If attachmentPath <> "" Then
                PubAttachmentPath = attachmentPath
            End If
            'MsgBox(PubAttachmentPath)

        Catch ex As Exception
            MsgBox(ex.Message & " at Mdi Load")
        End Try
    End Sub

    Private Sub Mnu_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles _
                MnuMaster.DropDownItemClicked, MnuUtility.DropDownItemClicked,
                MnuSale.DropDownItemClicked, MnuPurchase.DropDownItemClicked, MnuInventory.DropDownItemClicked,
                MnuAccountReports1.DropDownItemClicked, MnuAccountReports2.DropDownItemClicked,
                MnuSaleReports.DropDownItemClicked, MnuPurchaseReports.DropDownItemClicked, MnuInventoryReports.DropDownItemClicked,
                MnuStatutory.DropDownItemClicked, MnuAccounts.DropDownItemClicked, MnuTimeOffice.DropDownItemClicked, MnuUserSetup.DropDownItemClicked, MnuMasterReports.DropDownItemClicked, MnuAccountsReports.DropDownItemClicked,
                MnuSaleTools.DropDownItemClicked, MnuItem.DropDownItemClicked, MnuMasterSetup.DropDownItemClicked, MnuDeveloperTools.DropDownItemClicked, MnuProduction.DropDownItemClicked, MnuProductionReports.DropDownItemClicked, MnuChequeManagement.DropDownItemClicked,
                MnuReports.DropDownItemClicked, MnuLeadManagement.DropDownItemClicked, MnuFallPico.DropDownItemClicked


        'Dim Cls_Accounts As New AgAccounts.ClsMain(AgL)
        'If AgL.StrCmp(e.ClickedItem.ToolTipText, "Accounts") Then
        '    Dim FrmObj_FromReference As Form = Nothing
        '    Dim objAccountsClsFunction As New AgAccounts.ClsFunction
        '    FrmObj_FromReference = objAccountsClsFunction.FOpen(e.ClickedItem.Name, e.ClickedItem.ToString, TargetEntryType.EntryPoint)
        '    If IsNothing(FrmObj_FromReference) Then Exit Sub
        '    FrmObj_FromReference.MdiParent = Me
        '    AgL.PubSearchRow = ""
        '    FrmObj_FromReference.Show()
        '    FrmObj_FromReference = Nothing
        '    Exit Sub
        'End If


        Dim FrmObj As Form
        Dim CFOpen As New ClsFunction()
        Dim mTargetEntryType As TargetEntryType

        If e.ClickedItem.Tag Is Nothing Then e.ClickedItem.Tag = ""
        If e.ClickedItem.Tag.Trim = "" Then
            mTargetEntryType = TargetEntryType.EntryPoint
        ElseIf AgL.StrCmp(e.ClickedItem.Tag.Trim, "Grid Report") Then
            mTargetEntryType = TargetEntryType.GridReport
        Else
            mTargetEntryType = TargetEntryType.Report
        End If

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text, mTargetEntryType)
        If FrmObj IsNot Nothing Then
            For I As Integer = 0 To Me.MdiChildren.Length - 1
                If Me.MdiChildren(I).WindowState = FormWindowState.Maximized Then
                    Me.MdiChildren(I).WindowState = FormWindowState.Normal
                End If
            Next


            FrmObj.MdiParent = Me
            'Try
            '    FrmObj.Visible = True
            'Catch ex As Exception
            'End Try
            FrmObj.Show()
            If FrmObj.Name <> "FrmReportLayout" Then
                FrmObj.WindowState = FormWindowState.Maximized
            End If
            FrmObj = Nothing
        End If
    End Sub

    Public Function FOpenForm(ByVal StrModuleName, ByVal StrMnuName, ByVal StrMnuText) As Form
        Select Case UCase(StrModuleName)
            Case UCase(ClsMain.ModuleName)
                Dim CFOpen As New Customised.ClsFunction()
                FOpenForm = CFOpen.FOpen(StrMnuName, StrMnuText)
                CFOpen = Nothing

            Case Else
                FOpenForm = Nothing
        End Select
    End Function

    Private Sub MnuUpdateTableStructure_Click(sender As Object, e As EventArgs)
        Dim cf As New ClsMain(AgL)
        cf.UpdateTableStructure()
    End Sub

    Private Sub MnuUpdateDefaultSettings_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MnuCreditNote_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MnuUtility_Click(sender As Object, e As EventArgs) Handles MnuUtility.Click

    End Sub

    Private Sub MnuBankReconsilationEntry_Click(sender As Object, e As EventArgs) Handles MnuBankReconsilationEntry.Click

    End Sub

    Private Sub MnuVoucherEntry_Click(sender As Object, e As EventArgs) Handles MnuVoucherEntry.Click

    End Sub
    Public Sub FSetDimensionCaptionForMdi()
        Dim menues As New List(Of ToolStripItem)
        For Each t As ToolStripItem In Me.MnuMain.Items
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
    'Private Sub FOpenEntryFromShortCut(ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Dim mKeyPressed As String = ""
    '    Dim mQry As String = ""
    '    If e.KeyCode = 18 Then Exit Sub
    '    If e.Alt = True Then mKeyPressed = "+Alt"
    '    mKeyPressed = mKeyPressed + "+" & Chr(e.KeyCode)

    '    Dim DrMenu As DataRow() = Nothing
    '    DrMenu = AgL.PubDtMenus.Select("ShortCutKey = '" & mKeyPressed & "'")
    '    If DrMenu.Length > 0 Then
    '        Dim MnuName As String = AgL.XNull(DrMenu(0)("MnuName"))
    '        Dim MnuObject As ToolStripItem = MnuMain.Items.Find(MnuName, True)(0)
    '        Dim EventArgs As New System.Windows.Forms.ToolStripItemClickedEventArgs(MnuObject)
    '        Mnu_DropDownItemClicked(MnuObject.GetCurrentParent, EventArgs)
    '    End If
    'End Sub
End Class
