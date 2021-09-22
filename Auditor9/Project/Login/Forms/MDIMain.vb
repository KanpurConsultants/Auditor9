Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class MDIMain
    Public StrCurrentModule As String

    Dim MainMnuCounter As Integer
    Dim SubMnuCounter As Integer
    Dim LeafMnuCounter As Integer
    Dim MnuMainStreamCode As String
    Dim MnuGroupLevel As Integer
    'Dim Agl As AgLibrary.ClsMain


    Dim Cls_Customised As New Customised.ClsMain(AgL)
    Dim Cls_Accounts As New AgAccounts.ClsMain(AgL)
    'Dim Cls_Utility As New Utility.ClsMain(AgL)
    Dim Cls_AgTemplate As New AgTemplate.ClsMain(AgL)
    'Dim Cls_AgStructure As New AgStructure.ClsMain(AgL)
    'Dim Cls_AgCustomFields As New AgCustomFields.ClsMain(AgL)

    Dim ClsMF As New AgLibrary.ClsMDIFunctions(AgL)
    WithEvents TxtHelp As New ToolStripTextBox

    Dim PlaceHolder_Search$ = "Type Here To Search"


    Public Enum TargetEntryType
        EntryPoint = 0
        Report = 1
        GridReport = 2
    End Enum
    Public Function FOpenForm(ByVal StrModuleName, ByVal StrMnuName, ByVal StrMnuText) As Form
        Select Case UCase(StrModuleName)
            Case "ACCOUNTS"
                Dim CFOpen As New AgAccounts.ClsFunction
                FOpenForm = CFOpen.FOpen(StrMnuName, StrMnuText)
                CFOpen = Nothing
            Case Customised.ClsMain.ModuleName.ToUpper
                Dim CFOpen As New Customised.ClsFunction
                FOpenForm = CFOpen.FOpen(StrMnuName, StrMnuText)
                CFOpen = Nothing
            Case Else
                FOpenForm = Nothing
        End Select
    End Function

    Public Sub FMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FrmObj As Form = Nothing
        Dim StrType As String = ""

        If FMenuItem_Windows(sender) Then Exit Sub

        If sender.tag Is Nothing Then
            StrType = ""
        Else
            StrType = sender.tag
        End If

        If sender.ToolTipText IsNot Nothing Then
            StrCurrentModule = sender.ToolTipText
        End If

        'If Cls_Customised.CFOpen.FOpen(sender.NAME, sender.TEXT) IsNot Nothing Then
        '    If StrType.Trim = "" Then
        '        FrmObj = Cls_Customised.CFOpen.FOpen(sender.NAME, sender.TEXT, True, StrCurrentModule)
        '    ElseIf Not AgL.StrCmp(StrType.Trim, "CWDS") Then
        '        FrmObj = Cls_Customised.CFOpen.FOpen(sender.NAME, sender.TEXT, False, StrCurrentModule)
        '    End If
        'Else
        Select Case Trim(UCase(StrCurrentModule))
            Case "ACCOUNTS"
                Dim objAccountsClsFunction As New AgAccounts.ClsFunction
                If StrType.Trim = "" Then
                    FrmObj = objAccountsClsFunction.FOpen(sender.NAME, sender.ToString, TargetEntryType.EntryPoint)
                ElseIf AgL.StrCmp(StrType.Trim, "GRID REPORT") Then
                    FrmObj = objAccountsClsFunction.FOpen(sender.NAME, sender.ToString, TargetEntryType.GridReport)
                ElseIf AgL.StrCmp(StrType.Trim, "REPORT") Then
                    FrmObj = objAccountsClsFunction.FOpen(sender.NAME, sender.ToString, TargetEntryType.Report)
                End If

            Case Trim(UCase(Customised.ClsMain.ModuleName))
                If StrType.Trim = "" Then
                    FrmObj = Cls_Customised.CFOpen.FOpen(sender.NAME, sender.TEXT, TargetEntryType.EntryPoint)
                ElseIf AgL.StrCmp(StrType.Trim, "GRID REPORT") Then
                    FrmObj = Cls_Customised.CFOpen.FOpen(sender.NAME, sender.TEXT, TargetEntryType.GridReport)
                ElseIf AgL.StrCmp(StrType.Trim, "REPORT") Then
                    FrmObj = Cls_Customised.CFOpen.FOpen(sender.NAME, sender.TEXT, TargetEntryType.Report)
                End If



                'Case Trim(UCase("Utility"))
                '    If StrType.Trim = "" Then
                '        FrmObj = Cls_Utility.CFOpen.FOpen(sender.NAME, sender.TEXT, True)
                '    ElseIf Not AgL.StrCmp(StrType.Trim, "CWDS") Then
                '        FrmObj = Cls_Utility.CFOpen.FOpen(sender.NAME, sender.TEXT, False)
                '    End If




            Case Else
                FrmObj = Nothing
        End Select
        'End If
        If IsNothing(FrmObj) Then Exit Sub

        For I As Integer = 0 To Me.MdiChildren.Length - 1
            If Me.MdiChildren(I).WindowState = FormWindowState.Maximized Then
                Me.MdiChildren(I).WindowState = FormWindowState.Normal
            End If
        Next

        FrmObj.MdiParent = Me
        AgL.PubSearchRow = ""
        FrmObj.Show()
        FrmObj.WindowState = FormWindowState.Maximized
        FrmObj = Nothing
    End Sub
    Sub FOpenMenuClicked(ByVal ModuleName As String, ByVal MnuName As String, ByVal MnuText As String, ByVal MnuType As String)
        Dim FrmObj As Form = Nothing
        Select Case Trim(UCase(ModuleName))


            Case Trim(UCase(Customised.ClsMain.ModuleName))
                If MnuType.Trim = "" Then
                    FrmObj = Cls_Customised.CFOpen.FOpen(MnuName, MnuText, True)
                ElseIf Not AgL.StrCmp(MnuType.Trim, "CWDS") Then
                    FrmObj = Cls_Customised.CFOpen.FOpen(MnuName, MnuText, False)
                End If

                'Case Trim(UCase("Utility"))
                '    If MnuType.Trim = "" Then
                '        FrmObj = Cls_Utility.CFOpen.FOpen(MnuName, MnuText, True)
                '    ElseIf Not AgL.StrCmp(MnuType.Trim, "CWDS") Then
                '        FrmObj = Cls_Utility.CFOpen.FOpen(MnuName, MnuText, False)
                '    End If

            Case "ACCOUNTS"
                Dim objAccountsClsFunction As New AgAccounts.ClsFunction
                FrmObj = objAccountsClsFunction.FOpen(MnuName, MnuText)

            Case Else
                FrmObj = Nothing
        End Select
        If IsNothing(FrmObj) Then Exit Sub
        FrmObj.MdiParent = Me
        AgL.PubSearchRow = ""
        FrmObj.Show()
        FrmObj = Nothing

    End Sub


    Public Function FMenuItem_Windows(ByVal Sender) As Boolean
        Dim BlnFlagRtn As Boolean = False

        If UCase(Trim(Sender.Tag)) = "CWDS" Then
            Select Case UCase(Trim(Sender.Text))
                Case UCase(Trim("Cascade"))
                    Me.LayoutMdi(MdiLayout.Cascade)
                    BlnFlagRtn = True
                Case UCase(Trim("Tile Horizontal"))
                    Me.LayoutMdi(MdiLayout.TileHorizontal)
                    BlnFlagRtn = True
                Case UCase(Trim("Tile Vertical"))
                    Me.LayoutMdi(MdiLayout.TileVertical)
                    BlnFlagRtn = True
                Case UCase(Trim("Close All"))
                    For Each ChildForm As Form In Me.MdiChildren
                        ChildForm.Close()
                    Next
                    BlnFlagRtn = True
                Case UCase(Trim("Exit"))
                    Me.Dispose()
            End Select
        End If
        Return BlnFlagRtn
    End Function

    Private Sub FManageMDI()


        If Not (AgL.StrCmp("SA", AgL.PubUserName) Or AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)) Then MsgBox("Permission Denied!...") : Exit Sub

        If MsgBox("Are You To Run Manage MDI Tool?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = MsgBoxResult.No Then Exit Sub


        AgL.Dman_ExecuteNonQry("Delete From User_Permission Where UserName='SA'", AgL.GCn)
        AgL.Dman_ExecuteNonQry("Delete From User_Permission Where UserName='SUPER'", AgL.GCn)


        'Dim AccountsMdi As New AgAccounts.MDIMain1
        'AccountsMdi.Visible = True
        'FGenerate_UP(AccountsMdi, "Accounts", 0, "Accounts", GCnCmd)

        If AgL.StrCmp(AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Product", ""), "ChequePrinting") Then
            Dim CustomisedMdi As New Customised.MDICheque
            CustomisedMdi.Visible = True
            FGenerate_UP(CustomisedMdi, Customised.ClsMain.ModuleName, 1, Customised.ClsMain.ModuleName, AgL.ECmd)
        ElseIf AgL.StrCmp(AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Product", ""), "School") Then
            Dim CustomisedMdi As New Customised.MDISchool
            CustomisedMdi.Visible = True
            FGenerate_UP(CustomisedMdi, Customised.ClsMain.ModuleName, 1, Customised.ClsMain.ModuleName, AgL.ECmd)
        ElseIf AgL.StrCmp(AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Product", ""), "Spare") Then
            Dim CustomisedMdi As New Customised.MdiSpare
            CustomisedMdi.Visible = True
            FGenerate_UP(CustomisedMdi, Customised.ClsMain.ModuleName, 1, Customised.ClsMain.ModuleName, AgL.ECmd)
        Else
            Dim CustomisedMdi As New Customised.MDIMain
            CustomisedMdi.FSetDimensionCaptionForMdi()
            CustomisedMdi.Visible = True
            FGenerate_UP(CustomisedMdi, Customised.ClsMain.ModuleName, 1, Customised.ClsMain.ModuleName, AgL.ECmd)
            FRemoveParentWithoutChildMenus()
        End If


        'Dim RugUtilityMdi As New Utility.MDIMain
        'RugUtilityMdi.Visible = True
        'FGenerate_UP(RugUtilityMdi, "Utility", 5, "Utility", GCnCmd)

        ClsMF.FUpdateUserGroupLevels(AgL.GCn, AgL.ECmd)
        ClsMF.FManageEntryPointPermission(AgL.GCn, AgL.ECmd)

        MsgBox("Process Completed." & vbCrLf & "Please Reload the Software!...", MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo) : End
    End Sub

    Private Sub FManageUserControl()
        Dim GCnCmd As New Object


        If Not (AgL.StrCmp("SA", AgL.PubUserName) Or AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)) Then MsgBox("Permission Denied!...") : Exit Sub

        If MsgBox("Are You To Run Manage User Control Tool?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = MsgBoxResult.No Then Exit Sub

        GCnCmd = AgL.ECompConn.createcommand
        GCnCmd.CommandText = "Delete From User_Control_Permission Where UserName='SA'"
        GCnCmd.ExecuteNonQuery()


        ClsMF.FGenerate_UP_Control(Cls_Customised, Customised.ClsMain.ModuleName, GCnCmd)
        'ClsMF.FGenerate_UP_Control(Cls_Utility, "Utility", GCnCmd)
        ClsMF.FGenerate_UP_Control(Cls_Accounts, "ACCOUNTS", GCnCmd)
        MsgBox("Process Completed.", MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
    End Sub

    Private Sub MDIMain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
    End Sub

    Private Sub MDIMain_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        FrmDivisionSelection.Dispose()
        FrmLogin.Dispose()
        End
    End Sub

    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mQry$ = ""
        Dim DtTemp As DataTable
        Try

            AgL.PubStopWatch.Start()

            TSSL_User.Text = "User : " & AgL.PubUserName
            TSSL_Company.Text = AgL.PubCompName
            TSSL_Site.Text = "Site/Branch : " & AgL.PubSiteName
            'TSSL_OnlineOffLine.Text = IIf(AgL.PubOfflineApplicable, " [Online]", " [Offline]")

            mQry = "SELECT Name FROM Subgroup Sg WHERE Sg.Subcode = '" & AgL.PubDivCode & "' "
            AgL.PubCompName = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar


            DtTemp = AgL.FillData("Select ShortName From SiteMast Where Code = '" & AgL.PubSiteCode & "'", AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                AgL.PubSiteShortName = AgL.XNull(DtTemp.Rows(0)("ShortName"))
            End If


            DtTemp = AgL.FillData("Select D.Div_Name, D.ShortName, Sg.DispName 
                    From Division D 
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    Where D.Div_Code = '" & AgL.PubDivCode & "'", AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                AgL.PubDivName = AgL.XNull(DtTemp.Rows(0)("Div_Name"))
                AgL.PubDivShortName = AgL.XNull(DtTemp.Rows(0)("ShortName"))
                AgL.PubDivPrintName = AgL.XNull(DtTemp.Rows(0)("DispName"))
            End If



            TSSL_Division.Text = "Division : " & AgL.PubDivName


            If AgL.PubUserName.ToUpper = "SA" Or AgL.PubUserName.ToUpper = "SUPER" Then
                If AgL.PubServerName = "" Then
                    AgL.PubDivisionList = AgL.Dman_Execute("Select  group_concat('|' || div_code || '|' ,', ')   from division", AgL.GCn).ExecuteScalar
                Else
                    'AgL.PubDivisionList = AgL.Dman_Execute("Select  div_code + ','    from division for xml path('')", AgL.GCn).ExecuteScalar
                    AgL.PubDivisionList = AgL.Dman_Execute("Select  '|' + div_code + '|' + ','    from division for xml path('')", AgL.GCn).ExecuteScalar
                    AgL.PubDivisionList = AgL.PubDivisionList.Substring(0, AgL.PubDivisionList.Length - 1)
                End If
            Else
                AgL.PubDivisionList = AgL.Dman_Execute("Select IfNull(DivisionList,'') From UserSite Where User_Name = '" & AgL.PubUserName & "' And CompCode = '" & AgL.PubCompCode & "' ", AgL.GCn).ExecuteScalar
            End If



            If AgL.PubDivisionList = "" Then
                AgL.PubDivisionList = "''"
            Else
                AgL.PubDivisionList = "" & Replace(AgL.PubDivisionList, "|", "'") & ""
            End If

            If AgL.PubUserName.ToUpper = "SA" Or AgL.PubUserName.ToUpper = "SUPER" Then
                If AgL.PubServerName = "" Then
                    AgL.PubSiteList = AgL.Dman_Execute("Select  group_concat('|' || code || '|' ,', ')   from SITEMAST", AgL.GCn).ExecuteScalar
                Else
                    'AgL.PubSiteList = AgL.Dman_Execute("Select  code + ','   from SITEMAST for xml path('')", AgL.GCn).ExecuteScalar
                    AgL.PubSiteList = AgL.Dman_Execute("Select  '|' + code  + '|' + ','   from SITEMAST for xml path('')", AgL.GCn).ExecuteScalar
                    AgL.PubSiteList = AgL.PubSiteList.Substring(0, AgL.PubSiteList.Length - 1)
                End If
            Else
                AgL.PubSiteList = AgL.Dman_Execute("Select IfNull(SiteList,'') From UserSite Where User_Name = '" & AgL.PubUserName & "' And CompCode = '" & AgL.PubCompCode & "' ", AgL.GCn).ExecuteScalar
            End If

            If AgL.PubSiteList = "" Then
                AgL.PubSiteList = "''"
            Else
                AgL.PubSiteList = "" & Replace(AgL.PubSiteList, "|", "'") & ""
            End If


            mQry = "SELECT IfNull(D.ScopeOfWork,'') FROM Division D WHERE D.Div_Code = '" & AgL.PubDivCode & "' "
            AgL.PubScopeOfWork = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar


            mQry = "Select I.Code, I.Description, I.Specification, IfNull(I.ItemGroup,'') as ItemGroup
                , IfNull(I.ItemCategory,'') as ItemCategory, IfNull(I.ItemType,'') as ItemType, IfNull(I.BaseItem,'') as BaseItem
                , IfNull(I.Dimension1,'') as Dimension1, IfNull(I.Dimension2,'') as Dimension2
                , IfNull(I.Dimension3,'') as Dimension3, IfNull(I.Dimension4,'') as Dimension4
                , IfNull(I.Size,'') as Size, I.V_Type, I.Unit
                , IfNull(I.Div_Code,'') as Div_Code
                , IfNull(I.Site_Code,'') as Site_Code, I.MaintainStockYn, IfNull(I.HSN,'') as HSN 
                from Item I"
            AgL.PubDtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)

            mQry = "SELECT * FROM Menus "
            AgL.PubDtMenus = AgL.FillData(mQry, AgL.GCn).Tables(0)

            mQry = "Select H.SettingType, IfNull(H.Category,'') as Category, IfNull(H.NCat,'') as NCat, IfNull(H.VoucherType,'') as VoucherType, 
                    IfNull(H.Process,'') as Process, IfNull(H.SettingGroup,'') as SettingGroup, IfNull(H.Div_Code,'') as Div_Code, IfNull(H.Site_Code,'') as Site_Code, IfNull(H.FieldName,'') as FieldName, IfNull(H.Value,'') as Value 
                    from Setting H"
            AgL.PubDtSetting = AgL.FillData(mQry, AgL.GCn).Tables(0)

            mQry = "SELECT EntryName, GridName, FieldName, IsNull(Div_Code,'') AS Div_Code, IsNull(Site_Code,'') AS Site_Code, 
                    IsNull(NCat,'') AS NCat, IsNull(V_Type,'') AS V_Type, IsNull(Process,'') AS Process, IsNull(SettingGroup,'') AS SettingGroup, 
                    IsVisible, IsMandatory, IsEditable, IsSystemDefined, DisplayIndex, Caption, 
                    TextCase, BackColour, FontColour, FontSize, RowHeight, DataType, DataLength, DataMinLength
                    FROM EntryHeaderUISetting "
            AgL.PubEntryHeaderUISetting = AgL.FillData(mQry, AgL.GcnMain).Tables(0)

            mQry = "SELECT EntryName, GridName, FieldName, IsNull(Div_Code,'') AS Div_Code, IsNull(Site_Code,'') AS Site_Code, 
                    IsNull(NCat,'') AS NCat, IsNull(V_Type,'') AS V_Type, IsNull(Process,'') AS Process, IsNull(SettingGroup,'') AS SettingGroup, 
                    IsVisible, IsMandatory, IsEditable, IsSystemDefined, DisplayIndex, Caption, 
                    TextCase, BackColour, FontColour, FontSize, ColumnWidth, DataType, DataLength, DataMinLength
                    FROM EntryLineUISetting "
            AgL.PubEntryLineUISetting = AgL.FillData(mQry, AgL.GcnMain).Tables(0)

            AgL.PubDivisionCount = AgL.Dman_Execute("Select Count(*) From Division", AgL.GcnMain).ExecuteScalar()
            AgL.PubSiteCount = AgL.Dman_Execute("Select Count(*) From SiteMast", AgL.GcnMain).ExecuteScalar()

            AgL.PubCaptionItemType = FGetSettings(SettingFields.ItemTypeCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If AgL.PubCaptionItemType = "" Then AgL.PubCaptionItemType = "Item Type"
            AgL.PubCaptionItemCategory = FGetSettings(SettingFields.ItemCategoryCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If AgL.PubCaptionItemCategory = "" Then AgL.PubCaptionItemCategory = "Item Category"
            AgL.PubCaptionItemGroup = FGetSettings(SettingFields.ItemGroupCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If AgL.PubCaptionItemGroup = "" Then AgL.PubCaptionItemGroup = "Item Group"
            AgL.PubCaptionItem = FGetSettings(SettingFields.ItemCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If AgL.PubCaptionItem = "" Then AgL.PubCaptionItem = "Item"
            AgL.PubCaptionBarcode = FGetSettings(SettingFields.BarcodeCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If AgL.PubCaptionBarcode = "" Then AgL.PubCaptionBarcode = "Barcode"
            AgL.PubCaptionDimension1 = FGetSettings(SettingFields.Dimension1Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            AgL.PubCaptionDimension2 = FGetSettings(SettingFields.Dimension2Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            AgL.PubCaptionDimension3 = FGetSettings(SettingFields.Dimension3Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            AgL.PubCaptionDimension4 = FGetSettings(SettingFields.Dimension4Caption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            AgL.PubPrintDivisionShortNameOnDocumentsYn = FGetSettings(SettingFields.PrintDivisionShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            AgL.PubPrintSiteShortNameOnDocumentsYn = FGetSettings(SettingFields.PrintSiteShortNameOnDocumentsYn, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")

            AgL.PubCaptionLineDiscount = FGetSettings(SettingFields.LineDiscountCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If AgL.PubCaptionLineDiscount = "" Then AgL.PubCaptionLineDiscount = "Disc."
            AgL.PubCaptionLineAdditionalDiscount = FGetSettings(SettingFields.LineAdditionalDiscountCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If AgL.PubCaptionLineAdditionalDiscount = "" Then AgL.PubCaptionLineAdditionalDiscount = "A.Disc."
            AgL.PubCaptionLineAddition = FGetSettings(SettingFields.LineAdditionCaption, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If AgL.PubCaptionLineAddition = "" Then AgL.PubCaptionLineAddition = "Addition"


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



            'mQry = "Update LedgerHead Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            'mQry = "Update StockHead Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            'mQry = "Update SaleInvoice Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            'mQry = "Update PurchInvoice Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            'mQry = "Update SaleEnquiry Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            'mQry = "Update PurchPlan Set InUseBy=Null, InUseToken=Null Where InUseBy='" & AgL.PubUserName & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code= '" & AgL.PubDivCode & "' "
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            mQry = " Delete From StockVirtual "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)



            AgL.AllowTableLog(True, AgL.GCn)
            AgL.PubIsLogInProjectActive = False


            'Try
            '    mQry = "Select * from VoucherTypeSetting"
            '    AgL.PubDtVoucherTypeSetting = AgL.FillData(mQry, AgL.GCn).Tables(0)
            'Catch ex As Exception
            'End Try

            Try
                mQry = "Select * from VoucherTypeDateLock"
                AgL.PubDtVoucherTypeDateLock = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Catch ex As Exception
            End Try
            Try
                mQry = "Select * from VoucherTypeTimePlan"
                AgL.PubDtVoucherTypeTimePlan = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Catch ex As Exception
            End Try
            Try
                mQry = "SELECT C.End_Dt, C.cyear, F.* 
                    FROM FinancialYearLock F
                    LEFT JOIN Company C ON F.Comp_Code = C.Comp_Code"
                AgL.PubDtFinancialYearDateLock = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Catch ex As Exception
            End Try




            Dim C As Control

            For Each C In Me.Controls
                If TypeOf C Is MdiClient Then
                    C.BackColor = Color.White
                    Exit For
                End If
            Next
            C = Nothing

            If AgL.StrCmp(AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "MarketedBy", ""), "Equal2") Then
                Me.BackgroundImage = My.Resources.Equal2MDIBackgroud
                Me.BackgroundImageLayout = ImageLayout.Center
                Me.Text = "Equal2"
            ElseIf AgL.StrCmp(AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "MarketedBy", ""), "Auditor9") Then
                Me.BackgroundImage = My.Resources.Auditor9MDIBackgroud
                Me.BackgroundImageLayout = ImageLayout.Stretch
                Me.Text = "Auditor9"
            Else
                Me.BackgroundImage = Nothing
                Me.Text = "ERP"
            End If

            Me.Text = AgL.PubDivName & " \ " & AgL.PubSiteName & " \ " & AgL.PubCompYear

            FCreateHelpTextBox()


            If IO.File.Exists(My.Application.Info.DirectoryPath + "\" + "MdiImage.JPG") Then
                Me.BackgroundImage = Image.FromFile(My.Application.Info.DirectoryPath + "\" + "MdiImage.JPG")
                Me.BackgroundImageLayout = ImageLayout.Stretch
            End If


            Try
                AgL.PubCrystalDocument.Load(AgL.PubReportPath + "\SaleInvoice_Print.rpt")
            Catch ex As Exception
                MsgBox(ex.Message & " While loading PubCrystalDocument ")
            End Try



        Catch ex As Exception
            MsgBox(ex.Message & "   Can't Load Software")
            End
        End Try
    End Sub
    Private Sub FCreateHelpTextBox()
        SSrpMain.Items.Insert(0, TxtHelp)
        SSrpMain.ImageScalingSize = New Size(40, 40)
        TxtHelp.BorderStyle = BorderStyle.FixedSingle
        TxtHelp.AutoSize = False
        TxtHelp.Width = 400
        TxtHelp.Text = PlaceHolder_Search
        TxtHelp.ForeColor = Color.LightGray
        TxtHelp.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
    End Sub



    Private Sub MDIMain_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate
        'If IsNothing(ActiveMdiChild) Then Exit Sub
        'If UCase(ActiveMdiChild.Name) <> UCase("RepView") And UCase(ActiveMdiChild.Name) <> UCase("FrmRepDisplay") And
        '    UCase(ActiveMdiChild.Name) <> UCase("FrmReportPrint") Then
        '    Me.ActiveMdiChild.WindowState = FormWindowState.Normal
        'End If
    End Sub


    Private Sub TSSL_Btn_ManageMDI_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles TSSL_Btn_ManageMDI.Click, TSSL_Btn_ManageUserControl.Click, TSSL_Btn_UpdateTableStructure.Click, TSSL_UpdateTableStructureWebToolStripMenuItem.Click

        Select Case sender.Name
            Case TSSL_Btn_ManageMDI.Name
                FManageMDI()

            Case TSSL_Btn_ManageUserControl.Name
                FManageUserControl()

            Case TSSL_Btn_UpdateTableStructure.Name
                If Not (AgL.StrCmp("SA", AgL.PubUserName) Or AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)) Then MsgBox("Permission Denied!...") : Exit Sub

                If MsgBox("Are You Sure to Update Table Structure?...", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                'If MsgBox("Want To Take Database Backup", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                '    Dim FrmObj As Form
                '    FrmObj = New AgLibrary.FrmBackupDatase(AgL)
                '    FrmObj.ShowDialog()
                'End If



                Cls_Customised.UpdateTableStructure()

                MsgBox("Please Reload the Software!...") : End

            Case TSSL_UpdateTableStructureWebToolStripMenuItem.Name
                If Not (AgL.StrCmp("SA", AgL.PubUserName) Or AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)) Then MsgBox("Permission Denied!...") : Exit Sub
                If MsgBox("Is Machine : " & AgL.PubMachineName & " Connected to Internet?...", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub
                'Cls_SID.UpdateTableStructureWeb()

                MsgBox("Update Table Structure Web Completed!")
        End Select
    End Sub

    Private Sub ReconnectDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSSL_Btn_ReconnectDatabase.Click
        If Not FOpenIni(StrPath + "\" + IniName, AgL.PubUserName, AgL.PubUserPassword) Then
            MsgBox("Can't Connect to Database")
        Else
            AgIniVar.FOpenConnection(AgL.PubCompCode, AgL.PubSiteCode)
            AgIniVar.ProcSwapSiteCompanyDetail()
        End If
    End Sub

    Private Sub TspMenu_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)

    End Sub



    Public Sub Fill_PermissionTree(ByVal ModuleName As String, ByVal MSCode As String, Optional ByVal TNode As TreeNode = Nothing)
        Dim DtTemp As DataTable
        Dim I As Integer
        DtTemp = MdlFunction.DtMenu.Copy
        Dim mTNode As New TreeNode


        DtTemp.DefaultView.RowFilter = " mnuModule = '" & ModuleName & "' And substring(MainStreamCode,1," & Len(MSCode) & ")='" & MSCode & "' and Len(MainStreamCode)=" & Len(MSCode) + 3 & " "
        For I = 0 To DtTemp.DefaultView.Count - 1
            If TNode Is Nothing Then
                TreeView1.Nodes.Add(DtTemp.DefaultView.Item(I)("MnuText"))
                TreeView1.Nodes(TreeView1.Nodes.Count - 1).Name = DtTemp.DefaultView.Item(I)("MnuName")
                TreeView1.Nodes(TreeView1.Nodes.Count - 1).Tag = DtTemp.DefaultView.Item(I)("ReportFor")
                TreeView1.Nodes(TreeView1.Nodes.Count - 1).ImageIndex = 0

            Else
                TNode.Nodes.Add(DtTemp.DefaultView.Item(I)("MnuText"))
                TNode.Nodes(TNode.Nodes.Count - 1).Name = DtTemp.DefaultView.Item(I)("MnuName")
                TNode.Nodes(TNode.Nodes.Count - 1).Tag = DtTemp.DefaultView.Item(I)("ReportFor")

            End If
            mTNode = TreeView1.Nodes(TreeView1.Nodes.Count - 1)

            Fill_PermissionTree(ModuleName, DtTemp.DefaultView.Item(I)("MainStreamCode"), mTNode)
        Next
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Try
            If sender.SelectedNode IsNot Nothing Then
                FOpenMenuClicked(sender.tag, sender.SelectedNode.name, sender.SelectedNode.text, AgL.XNull(sender.SelectedNode.tag))

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub MDIMain_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'If e.Location.X <splitter1.Location.X Then
        '    Do Until TbcMain.Width >= 300
        '        TbcMain.Width = TbcMain.Width + 20
        '    Loop
        'Else
        '    Do Until TbcMain.Width <= 25
        '        TbcMain.Width = TbcMain.Width - 40
        '    Loop
        '    TbcMain.Width = 25
        'End If
    End Sub



    Public Sub FGenerate_UP(ByVal ObjFor As Object, ByVal StrParent As String,
                ByVal IntSno As Integer, ByVal StrMnuPath As String, ByVal GCnCmd As Object)
        Dim Mnu As Object
        For Each Mnu In ObjFor.Controls
            If Mnu.GetType.ToString = "System.Windows.Forms.MenuStrip" Then
                FRotateAllMenuItems(Mnu, Mnu.Name, StrParent, StrParent, IntSno, GCnCmd)
            End If
        Next
    End Sub

    Public Function FRotateAllMenuItems(ByRef MnuStrp As MenuStrip, ByVal StrMnuMain As String, ByVal StrModule As String, ByVal StrParent As String,
    ByVal IntSno As Integer, ByVal GCnCmd As Object) As Integer
        Dim TSI_Main As ToolStripItem
        Dim TSMI_Main As ToolStripMenuItem
        Dim IntRtn As Integer
        Dim ReportFor As String


        For Each TSI_Main In MnuStrp.Items
            If TSI_Main.Visible Then
                'If TSI_Main.AccessibleDescription Is Nothing Then TSI_Main.AccessibleDescription = ""
                'If TSI_Main.AccessibleDescription = "" Or AgL.PubScopeOfWork.ToUpper.Contains(TSI_Main.AccessibleDescription.ToUpper) Or AgL.PubMainCompName.ToUpper.Contains("AUDITOR9") Then
                If FExcludeMenus(TSI_Main.Name) = False Then
                        If TSI_Main.GetCurrentParent.Name = StrMnuMain Then
                            IntSno = 0
                            LeafMnuCounter = 0
                            SubMnuCounter = 0
                            MainMnuCounter += 1
                            MnuMainStreamCode = Format(MainMnuCounter, "000").ToString
                        End If

                        If TSI_Main.Tag Is Nothing Or IsDBNull(TSI_Main.Tag) Then
                            ReportFor = ""
                        Else
                            ReportFor = TSI_Main.Tag
                        End If


                        FInsertUP("", TSI_Main.Text, TSI_Main.Name, StrParent, IntSno, IntSno, ReportFor, TSI_Main.AccessibleDescription)



                        If TSI_Main.GetType.ToString = "System.Windows.Forms.ToolStripMenuItem" Then
                            TSI_Main.Visible = True
                            TSMI_Main = TSI_Main
                            IntRtn = FRotateAllMenuItems(TSMI_Main.DropDownItems, StrMnuMain, StrModule, TSMI_Main.Name, IntSno + 1, GCnCmd)
                            If IntRtn <> 0 Then
                                IntSno = IntRtn
                            End If
                        End If
                    End If
                'End If
            End If
        Next
        Return IntSno
    End Function

    Public Function FRotateAllMenuItems(ByRef Menus As ToolStripItemCollection, ByVal StrMnuMain As String,
                                        ByVal StrModule As String, ByVal StrParent As String,
                                        ByVal IntSno As Integer, ByVal GCnCmd As Object) As Integer
        Dim TSI_Main As ToolStripItem
        Dim TSMI_Main As ToolStripMenuItem
        Dim MenuScope() As String
        Dim ReportFor As String
        Dim I As Integer

        For Each TSI_Main In Menus
            Debug.Print(TSI_Main.Text)
            If Trim(TSI_Main.Text) <> "" And TSI_Main.AccessibleRole <> Windows.Forms.AccessibleRole.None Then
                'If TSI_Main.Visible = True Then
                'If Trim(TSI_Main.Text) <> "" Then
                'If TSI_Main.AccessibleDescription Is Nothing Then TSI_Main.AccessibleDescription = ""
                'MenuScope = TSI_Main.AccessibleDescription.Split("+")
                'If MenuScope.Length > 0 And TSI_Main.AccessibleDescription <> "" Then
                'For I = 0 To MenuScope.Length - 1
                '        If MenuScope(I) <> "" Then
                '            If AgL.PubScopeOfWork.ToUpper.Contains(MenuScope(I).ToUpper) Then
                '                If TSI_Main.Tag Is Nothing Or IsDBNull(TSI_Main.Tag) Then
                '                    ReportFor = ""
                '                Else
                '                    ReportFor = TSI_Main.Tag
                '                End If

                '                MnuMainStreamCode = AgL.FillData("Select MainStreamCode From User_Permission Where UserName = 'SA' And MnuModule = '" & StrModule & "' And MnuName='" & StrParent & "' ", AgL.GCn).Tables(0).Rows(0)(0)
                '                MnuMainStreamCode = MnuMainStreamCode + Format(IntSno, "000").ToString

                '                FInsertUP(StrParent, TSI_Main.Text, TSI_Main.Name, StrModule, IntSno, IntSno, ReportFor, TSI_Main.AccessibleDescription)
                '                If TSI_Main.GetType.ToString = "System.Windows.Forms.ToolStripMenuItem" Then
                '                    TSMI_Main = TSI_Main
                '                    IntSno = FRotateAllMenuItems(TSMI_Main.DropDownItems, StrMnuMain, StrModule, TSMI_Main.Name, IntSno + 1, GCnCmd)
                '                End If
                '                Exit For
                '            End If
                '        End If
                '    Next
                'Else
                If TSI_Main.Tag Is Nothing Or IsDBNull(TSI_Main.Tag) Then
                        ReportFor = ""
                    Else
                        ReportFor = TSI_Main.Tag
                    End If

                    MnuMainStreamCode = AgL.FillData("Select MainStreamCode From User_Permission Where UserName = 'SA' And MnuModule = '" & StrModule & "' And MnuName='" & StrParent & "' ", AgL.GCn).Tables(0).Rows(0)(0)
                    MnuMainStreamCode = MnuMainStreamCode + Format(IntSno, "000").ToString

                    FInsertUP(StrParent, TSI_Main.Text, TSI_Main.Name, StrModule, IntSno, IntSno, ReportFor, TSI_Main.AccessibleDescription)
                    If TSI_Main.GetType.ToString = "System.Windows.Forms.ToolStripMenuItem" Then
                        TSMI_Main = TSI_Main
                        IntSno = FRotateAllMenuItems(TSMI_Main.DropDownItems, StrMnuMain, StrModule, TSMI_Main.Name, IntSno + 1, GCnCmd)
                    End If
                End If
            'If TSI_Main.AccessibleDescription = "" Or AgL.PubScopeOfWork.ToUpper.Contains(TSI_Main.AccessibleDescription.ToUpper) Or AgL.PubMainCompName.ToUpper.Contains("AUDITOR9") Then
            '    If TSI_Main.Tag Is Nothing Or IsDBNull(TSI_Main.Tag) Then
            '        ReportFor = ""
            '    Else
            '        ReportFor = TSI_Main.Tag
            '    End If

            '    MnuMainStreamCode = AgL.FillData("Select MainStreamCode From User_Permission Where UserName = 'SA' And MnuModule = '" & StrModule & "' And MnuName='" & StrParent & "' ", AgL.GCn).Tables(0).Rows(0)(0)
            '    MnuMainStreamCode = MnuMainStreamCode + Format(IntSno, "000").ToString

            '    FInsertUP(StrParent, TSI_Main.Text, TSI_Main.Name, StrModule, IntSno, IntSno, ReportFor, TSI_Main.AccessibleDescription)
            '    If TSI_Main.GetType.ToString = "System.Windows.Forms.ToolStripMenuItem" Then
            '        TSMI_Main = TSI_Main
            '        IntSno = FRotateAllMenuItems(TSMI_Main.DropDownItems, StrMnuMain, StrModule, TSMI_Main.Name, IntSno + 1, GCnCmd)
            '    End If
            'End If
            'End If
            'End If
        Next
        Return IntSno
    End Function

    Public Sub FInsertUP(ByVal StrParent As String, ByVal StrMnuText As String, ByVal StrMnuName As String,
                         ByVal StrMnuModule As String, ByVal IntSNo As Integer, ByVal IntLevel As String,
                         ByVal ReportFor As String, ByVal ControlPermissionGroups As String)

        Static Dim I As Integer
        I = I + 1

        AgL.Dman_ExecuteNonQry("Insert Into User_Permission(UserName,Parent,MnuText,MnuName,Permission,SNo,MnuModule,MnuLevel,ReportFor, ControlPermissionGroups,MainStreamCode, GroupLevel, Active,RowId) Values " &
                                "('SUPER','" & StrParent & "','" & Replace(StrMnuText, "&", "") & "','" & StrMnuName & "','AEDP'," & I & ",'" & StrMnuModule & "'," & IntLevel & "," & AgL.Chk_Text(ReportFor) & ", " & AgL.Chk_Text(ControlPermissionGroups) & ", " & AgL.Chk_Text(MnuMainStreamCode) & ", " & MnuGroupLevel & ", 'Y'," & I & ")", AgL.GcnMain)

        Dim DrMenu As DataRow() = Nothing
        DrMenu = AgL.PubDtMenus.Select("MnuName = '" & StrMnuName & "' ")

        Dim IsInsertMenu As Boolean = False
        If DrMenu.Length = 0 Then
            IsInsertMenu = True
        Else
            If AgL.VNull(DrMenu(0)("IsVisible")) <> 0 Then IsInsertMenu = True
        End If

        If IsInsertMenu = True Then
            AgL.Dman_ExecuteNonQry("Insert Into User_Permission(UserName,Parent,MnuText,MnuName,Permission,SNo,MnuModule,MnuLevel,ReportFor, ControlPermissionGroups,MainStreamCode, GroupLevel, Active,RowId) Values " &
                                "('SA','" & StrParent & "','" & Replace(StrMnuText, "&", "") & "','" & StrMnuName & "','AEDP'," & I & ",'" & StrMnuModule & "'," & IntLevel & "," & AgL.Chk_Text(ReportFor) & ", " & AgL.Chk_Text(ControlPermissionGroups) & ", " & AgL.Chk_Text(MnuMainStreamCode) & ", " & MnuGroupLevel & ", 'Y'," & I & ")", AgL.GcnMain)
        End If



        If StrParent <> "" Then
            AgL.Dman_ExecuteNonQry("UPDATE User_Permission SET IsParent = 1 WHERE UserName = 'SA' AND MnuName = '" & StrParent & "' ", AgL.GcnMain)
        End If

    End Sub

    Private Sub TSSL_User_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TSSL_User.Click
        Dim FrmObj As Form

        FrmObj = New Customised.FrmChangePassword()
        If FrmObj IsNot Nothing Then
            FrmObj.Text = "Change Password"
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If
    End Sub

    Private Sub FShowPartyHelp()
        Dim mQry As String = "SELECT Sg.Code, Sg.Name, 'SubGroup' As MasterType FROM ViewHelpSubgroup Sg"
        mQry += " UNION ALL "
        mQry += " SELECT I.Code, I.Description As Name, 'Item' As MasterType FROM Item I Where I.V_Type = 'ITEM' "
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, TxtHelp.Width, SSrpMain.Top - 405, TxtHelp.Bounds.Left, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Name", 350, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(2, "MasterType", 350, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            bCode = FRH_Single.DRReturn("Code")
            If FRH_Single.DRReturn("MasterType") = "Item" Then
                Dim FrmObj As New Customised.FrmItemView
                FrmObj.StartPosition = FormStartPosition.CenterParent
                FrmObj.SearchCode = bCode
                FrmObj.ShowDialog()
            Else
                Dim FrmObj As New Customised.FrmQuickView
                FrmObj.StartPosition = FormStartPosition.CenterParent
                FrmObj.SearchCode = bCode
                FrmObj.ShowDialog()
            End If
        End If

        Me.Focus()
    End Sub
    Private Sub TxtHelp_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtHelp.KeyDown
        FShowPartyHelp()
    End Sub
    Private Sub TxtHelp_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtHelp.GotFocus
        Select Case sender.name
            Case TxtHelp.Name
                If TxtHelp.Text = PlaceHolder_Search Then
                    TxtHelp.Text = ""
                    TxtHelp.ForeColor = Nothing
                End If
        End Select
    End Sub
    Private Sub TxtHelp_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtHelp.LostFocus
        Select Case sender.name
            Case TxtHelp.Name
                If TxtHelp.Text = "" Then
                    TxtHelp.Text = PlaceHolder_Search
                    TxtHelp.ForeColor = Color.LightGray
                End If
        End Select
    End Sub
    Private Sub MDIMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If AgL.StrCmp(AgL.PubUserName, "Sa") Then
            If MsgBox("Do you want to take backup", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim FrmObj As Form = Nothing
                FrmObj = Cls_Customised.CFOpen.FOpen("MnuBackupDatabase", "Backup Database", TargetEntryType.EntryPoint)
                AgL.PubSearchRow = ""
                FrmObj.ShowDialog()
            End If
        End If
    End Sub
    Private Function FExcludeMenus(bMnuName As String) As Boolean
        FExcludeMenus = False
        If bMnuName = "MnuStockReport" Then
            If AgL.PubScopeOfWork.ToUpper.Contains(IndustryType.GarmentIndustry) Then
                FExcludeMenus = True
            End If
        End If
    End Function
    Private Sub FOpenEntryFromShortCut(ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim mKeyPressed As String = ""
        Dim mQry As String = ""

        Try
            If Me.ActiveMdiChild IsNot Nothing Then Exit Sub
            'If e.KeyCode = 18 Or e.KeyCode = 13 Then Exit Sub
            If e.KeyCode = 13 Then Exit Sub
            If e.Alt = True Then mKeyPressed = "+Alt"
            If e.Control = True Then mKeyPressed = "+Ctrl"
            Dim mSpecialKey As String = SpecialKey(e)
            If mSpecialKey <> "" Then
                mKeyPressed = mKeyPressed + "+" & mSpecialKey
            Else
                mKeyPressed = mKeyPressed + "+" & Chr(e.KeyCode)
            End If

            Dim DrMenu As DataRow() = Nothing
            DrMenu = AgL.PubDtMenus.Select("ShortCutKey = '" & mKeyPressed & "'")
            If DrMenu.Length > 0 Then
                Dim MnuName As String = AgL.XNull(DrMenu(0)("MnuName"))

                Dim ToolStripObj As MenuStrip
                For I As Integer = 0 To Me.Controls.Count - 1
                    If Me.Controls(I).GetType.ToString = GetType(MenuStrip).ToString Then
                        ToolStripObj = Me.Controls(I)
                    End If
                Next

                If ToolStripObj IsNot Nothing Then
                    Dim MnuObject As ToolStripItem = ToolStripObj.Items.Find(MnuName, True)(0)
                    Dim EventArgs As New System.Windows.Forms.ToolStripItemClickedEventArgs(MnuObject)
                    FMenuItem_Click(MnuObject, EventArgs)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function SpecialKey(ByVal e As System.Windows.Forms.KeyEventArgs) As String
        Select Case e.KeyCode
            Case Keys.F1
                SpecialKey = "F1"
            Case Keys.F2
                SpecialKey = "F2"
            Case Keys.F3
                SpecialKey = "F3"
            Case Keys.F4
                SpecialKey = "F4"
            Case Keys.F5
                SpecialKey = "F5"
            Case Keys.F6
                SpecialKey = "F6"
            Case Keys.F7
                SpecialKey = "F7"
            Case Keys.F8
                SpecialKey = "F8"
            Case Keys.F9
                SpecialKey = "F9"
            Case Keys.F10
                SpecialKey = "F10"
            Case Keys.F11
                SpecialKey = "F11"
            Case Keys.F12
                SpecialKey = "F12"
        End Select
    End Function

    Private Sub MDIMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        FOpenEntryFromShortCut(e)
    End Sub
    Private Sub FRemoveParentWithoutChildMenus()
        Dim mQry As String = ""
        Dim mParentWithoutChildQry As String = ""

        mParentWithoutChildQry = "SELECT U.MnuName 
                FROM User_Permission U
                LEFT JOIN (Select * From User_Permission Where UserName = 'Sa') As U1 On U.MnuName = U1.Parent
                WHERE U.UserName = 'Sa' And IsNull(U.IsParent,0) <> 0
                And U1.MnuName Is Null"

        mQry = "Delete From User_Permission Where MnuName In (" & mParentWithoutChildQry & ") And UserName = 'Sa' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        If AgL.FillData(mParentWithoutChildQry, AgL.GCn).Tables(0).Rows.Count() > 0 Then
            FRemoveParentWithoutChildMenus()
        End If
    End Sub
End Class
