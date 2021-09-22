'Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Imports System.IO

Public Class FrmSiteMaster
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mEnviroId As String = ""

    Dim Photo_Byte As Byte()
    Dim mCopySettingsFromSite$ = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then StrUPVar = StrUPVar.Replace("A", "*")
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub


    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub


    Private Sub IniGrid()

    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If


        If Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And _
                Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid)  Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub


    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 500, 880, 0, 0)
            IniGrid()
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        If AgL.PubMoveRecApplicable Then
            mQry = "Select SiteMast.Code As SearchCode " & _
                    " From SiteMast " & _
                    " Order By Name"

            Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
        End If
    End Sub


    Sub Ini_List()
        'mQry = "Select Code  As Code, Code As Name From SiteMast " & _
        '    "  Order By Name"
        'AgCL.IniAgHelpList(AgL.Gcn, CboSiteCode, mQry, "Name", "Code")

        'mQry = "Select Code  As Code, Name As Name From SiteMast " & _
        '    "  Order By Name"
        'AgCL.IniAgHelpList(AgL.Gcn, CboName, mQry, "Name", "Code")

        'mQry = "Select CityCode As Code, CityName As Name From City " & _
        '    "  Order By CityName"
        'AgCL.IniAgHelpList(AgL.Gcn, CboCity_Code, mQry, "Name", "Code")

        'mQry = "Select SubCode As Code, Name  From ViewSubGroup " & _
        '    "Where IfNull(CommonAc,0)<> 0 And Left(MainGrCodeS," & AgLibrary.ClsMain.PubBranchDivisionsMainGRLen & ")='" & AgLibrary.ClsMain.PubBranchDivisionsMainGRCode & "' " & _
        '    "Order By CityName "
        'TxtAcCode.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        'mQry = "Select Code  As Code, ManualCode As Name " & _
        '        " From SiteMast " & _
        '        " Where IfNull(ManualCode,'') <> '' " & _
        '        " Order By ManualCode"
        'TxtManualCode.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        CboSiteCode.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        'Dim BlnTrans As Boolean = False
        'Dim GCnCmd As New SqliteCommand
        'Dim MastPos As Long
        'Dim mTrans As Boolean = False


        'Try
        '    MastPos = BMBMaster.Position


        '    If DTMaster.Rows.Count > 0 Then
        '        If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


        '            AgL.ECmd = AgL.Gcn.CreateCommand
        '            AgL.ETrans = AgL.Gcn.BeginTransaction(IsolationLevel.ReadCommitted)
        '            AgL.ECmd.Transaction = AgL.ETrans
        '            mTrans = True
        '            AgL.Dman_ExecuteNonQry("Delete From SiteMast Where Code='" & mSearchCode & "'", AgL.Gcn, AgL.ECmd)


        '            Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.Gcn, AgL.ECmd)

        '            AgL.ETrans.Commit()
        '            mTrans = False


        '            FIniMaster(1)
        '            Topctrl1_tbRef()
        '            MoveRec()
        '        End If
        '    End If
        'Catch Ex As Exception
        '    If mTrans = True Then AgL.ETrans.Rollback()
        '    MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        'End Try
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        Topctrl1.Focus()
    End Sub


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DispText()
        TxtManualCode.Focus()
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        'If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = "Select  SiteMast.Code As SearchCode,  SiteMast.Name As [Site Name],  SiteMast.HO_YN As [Head Office [(Y)es/(N)o]],  SiteMast.Add1 As [Address],  SiteMast.Add2 As [Add2],  SiteMast.Add3 As [Add3],  City6.CityName As [City],  SiteMast.PinNo As [Pin],  SiteMast.Phone As [Phone No.],  SiteMast.Mobile As [Mobile No.]  From  SiteMast  Left Join  City City6 On City6.CityCode = SiteMast.City_Code "


            AgL.PubFindQryOrdBy = "[Site Name]"



            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
            If AgL.PubSearchRow <> "" Then
                If AgL.PubMoveRecApplicable Then
                    AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                    BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                End If
                MoveRec()
            End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub



    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Try
            If AgL.PubMoveRecApplicable Then MastPos = BMBMaster.Position


            If Not Data_Validation() Then Exit Sub

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into SiteMast (Code, ManualCode, Name, HO_YN, Add1, Add2, Add3, City_Code, PinNo, Phone, Mobile, AcCode, CreditLimit, Active, " &
                        " U_EntDt, U_Name, U_AE) Values(" &
                        " '" & mSearchCode & "', " & AgL.Chk_Text(TxtManualCode.Text) & ", " & AgL.Chk_Text(CboName.Text) & ", " &
                        " '" & AgL.MidStr(TxtHO_YN.Text, 0, 1) & "', " & AgL.Chk_Text(TxtAdd1.Text) & "," &
                        " " & AgL.Chk_Text(TxtAdd2.Text) & ", " & AgL.Chk_Text(TxtAdd3.Text) & "," &
                        " " & AgL.Chk_Text(CboCity_Code.SelectedValue) & ", " & AgL.Chk_Text(TxtPinNo.Text) & "," &
                        " " & AgL.Chk_Text(TxtPhone.Text) & ", " & AgL.Chk_Text(TxtMobile.Text) & ", " & AgL.Chk_Text(TxtAcCode.AgSelectedValue) & ", " & Val(TxtCreditLimit.Text) & ", " & IIf(AgL.StrCmp(TxtActive.Text, "Yes"), 1, 0) & "," &
                        " '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "', 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                If AgL.PubSitewiseV_No = True Then
                    'Inserting Record in Voucher Prefix Table----------------------------------
                    mQry = "Delete From Voucher_Prefix Where Site_Code='" & mSearchCode & "' And Div_Code='" & AgL.PubDivCode & "' And IfNull(Comp_Code,'" & AgL.PubCompCode & "') = '" & AgL.PubCompCode & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "Insert Into Voucher_Prefix(Site_Code,V_Type,Date_From,Date_To,Prefix,Start_Srl_No,Div_Code,Comp_Code)" &
                            " (Select '" & mSearchCode & "' As Site_Code,V_Type,Date_From,Date_To,Prefix,0 As Start_Srl_No,'" & AgL.PubDivCode & "' As Div_Code, '" & AgL.PubCompCode & "' As Comp_Code From Voucher_Prefix Where Site_Code='" & AgL.PubSiteCode & "' And Div_Code='" & AgL.PubDivCode & "' And IfNull(Comp_Code,'" & AgL.PubCompCode & "') = '" & AgL.PubCompCode & "' )"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    'Inserting Record in Enviro Table----------------------------------
                    mQry = "Delete From Enviro Where Site_Code='" & mSearchCode & "' And Div_Code='" & AgL.PubDivCode & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "Insert Into Enviro(Id,Site_Code,Div_Code, " &
                            " U_EntDt, PreparedBy, U_AE) Values (" &
                            " '" & mEnviroId & "', '" & mSearchCode & "', '" & AgL.PubDivCode & "', " &
                            " '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "', 'A') "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    'Inserting Record in FaEnviro Table----------------------------------
                    mQry = "Delete From FaEnviro Where Site_Code='" & mSearchCode & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "INSERT INTO FaEnviro ( " &
                            " Age1, Age2, Age3, Age4, Age5, Age6, Amt1, Amt2, Amt3, Amt4, Amt5, Amt6,  " &
                            " TagadaHeader1, TagadaHeader2, TagadaHeader3, TagadaHeader4, TagadaHeader5, " &
                            " TagadaFooter1, TagadaFooter2, TagadaFooter3, TagadaFooter4, TagadaFooter5, " &
                            " CreditLimit, DebitLimit, NegativeCashBalance, ShowGroup, DonotShowGroup, " &
                            " ShowCurrentBalance, VerticleBalanceSheet, ShowQty, ShowCityName, LedDivCode, " &
                            " LedSiteCode, LedPrefix, linefiller, daterfill, titlerfill, pagenofill, " &
                            " RunPIF, FilterAC, DateLock, AddressHelp, CashBookBalance, MonthTotal, OnLineAdjustment, " &
                            " OpStockQTY, OpStockValue, ClStockQTY, ClStockValue, CashBookPage, RepToBy, PreBal, " &
                            " PDCDt, ToBy, CityNameDisp, Site_Code) " &
                            " ( Select Age1, Age2, Age3, Age4, Age5, Age6, Amt1, Amt2, Amt3, Amt4, Amt5, Amt6,  " &
                            " TagadaHeader1, TagadaHeader2, TagadaHeader3, TagadaHeader4, TagadaHeader5, " &
                            " TagadaFooter1, TagadaFooter2, TagadaFooter3, TagadaFooter4, TagadaFooter5, " &
                            " CreditLimit, DebitLimit, NegativeCashBalance, ShowGroup, DonotShowGroup, " &
                            " ShowCurrentBalance, VerticleBalanceSheet, ShowQty, ShowCityName, LedDivCode, " &
                            " LedSiteCode, LedPrefix, linefiller, daterfill, titlerfill, pagenofill, " &
                            " RunPIF, FilterAC, DateLock, AddressHelp, CashBookBalance, MonthTotal, OnLineAdjustment, " &
                            " OpStockQTY, OpStockValue, ClStockQTY, ClStockValue, CashBookPage, RepToBy, PreBal, " &
                            " PDCDt, ToBy, CityNameDisp, '" & mSearchCode & "' As Site_Code " &
                            " From FaEnviro Where Site_Code='" & AgL.PubSiteCode & "') "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    'Inserting Record in Voucher_Type_Settings----------------------------------
                    ProcCopyToAllSite()
                End If
            Else
                mQry = "Update SiteMast Set ManualCode=" & AgL.Chk_Text(TxtManualCode.Text) & ", Name = " & AgL.Chk_Text(CboName.Text) & ", HO_YN = '" & AgL.MidStr(TxtHO_YN.Text, 0, 1) & "'," &
                        " Add1 = " & AgL.Chk_Text(TxtAdd1.Text) & ", Add2 = " & AgL.Chk_Text(TxtAdd2.Text) & ", Add3 = " & AgL.Chk_Text(TxtAdd3.Text) & "," &
                        " City_Code = " & AgL.Chk_Text(CboCity_Code.SelectedValue) & ", PinNo = " & AgL.Chk_Text(TxtPinNo.Text) & "," &
                        " Phone = " & AgL.Chk_Text(TxtPhone.Text) & ", Mobile = " & AgL.Chk_Text(TxtMobile.Text) & ", AcCode = " & AgL.Chk_Text(TxtAcCode.AgSelectedValue) & ", CreditLimit = " & Val(TxtCreditLimit.Text) & "," &
                        " Active = " & IIf(AgL.StrCmp(TxtActive.Text, "Yes"), 1, 0) & ", " &
                        " Edit_Date='" & Format(AgL.PubLoginDate, "Short Date") & "', ModifiedBy = '" & AgL.PubUserName & "', U_AE = 'E'" &
                        " Where Code='" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                If AgL.StrCmp(AgL.PubSiteCode, mSearchCode) Then
                    AgL.PubSiteName = CboName.Text

                    AgL.PubSiteManualCode = TxtManualCode.Text
                    AgL.PubSiteAdd1 = TxtAdd1.Text
                    AgL.PubSiteAdd2 = TxtAdd2.Text
                    AgL.PubSiteAdd3 = TxtAdd3.Text
                    AgL.PubSiteCity = CboCity_Code.Text
                    AgL.PubSitePinNo = TxtPinNo.Text
                    AgL.PubSitePhone = TxtPhone.Text
                    AgL.PubSiteMobile = TxtMobile.Text

                    AgL.PubLogSiteName = AgL.PubSiteName

                End If
            End If


            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)


            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
            AgL.ETrans.Commit()
            mTrans = False

            Update_Picture("SiteMast", "Photo", "Where Code = '" & mSearchCode & "'", Photo_Byte)

            If AgL.PubMoveRecApplicable Then
                FIniMaster(0, 1)
                Topctrl1_tbRef()
            End If
            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            FClear()
            BlankText()

            If AgL.PubMoveRecApplicable Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
            Else
                mSearchCode = AgL.PubSearchRow
            End If

            If mSearchCode <> "" Then
                CboSiteCode.SelectedValue = mSearchCode
                mQry = "Select SiteMast.* " &
                    " From SiteMast Where Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)

                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        CboName.SelectedValue = AgL.XNull(.Rows(0)("Code"))
                        TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                        TxtHO_YN.Text = IIf(AgL.XNull(.Rows(0)("HO_YN")).ToString.Trim = "Y", "Yes", "No")
                        TxtAdd1.Text = AgL.XNull(.Rows(0)("Add1"))
                        TxtAdd2.Text = AgL.XNull(.Rows(0)("Add2"))
                        TxtAdd3.Text = AgL.XNull(.Rows(0)("Add3"))
                        CboCity_Code.SelectedValue = AgL.XNull(.Rows(0)("City_Code"))
                        TxtPinNo.Text = AgL.XNull(.Rows(0)("PinNo"))
                        TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                        TxtMobile.Text = AgL.XNull(.Rows(0)("Mobile"))
                        TxtAcCode.AgSelectedValue = AgL.XNull(.Rows(0)("AcCode"))
                        TxtCreditLimit.Text = AgL.VNull(.Rows(0)("CreditLimit"))
                        TxtActive.Text = IIf(AgL.VNull(.Rows(0)("Active")), "Yes", "No")

                        If Not IsDBNull(.Rows(0)("Photo")) Then
                            Photo_Byte = DirectCast(.Rows(0)("Photo"), Byte())
                            Show_Picture(PicPhoto, Photo_Byte)
                        End If

                        TxtPrepared.Text = AgL.XNull(.Rows(0)("U_Name"))
                        TxtModified.Text = AgL.XNull(.Rows(0)("ModifiedBy"))
                        GroupBox4.Visible = IIf(TxtModified.Text.Trim <> "", True, False)
                    End If
                End With
            Else
                BlankText()
            End If
            If AgL.PubMoveRecApplicable Then Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
            Topctrl1.tDel = False : Topctrl1.tPrn = False
        End Try
    End Sub
    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = "" : mEnviroId = ""
        TxtActive.Text = "Yes"
        PicPhoto.Image = Nothing : Photo_Byte = Nothing
    End Sub
    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If Topctrl1.Mode = "Edit" Then CboSiteCode.Enabled = False
        TxtPrepared.Enabled = False : TxtModified.Enabled = False
    End Sub
    Private Sub FClear()
        DTStruct.Clear()
    End Sub
    Private Sub FAddRowStructure()
        Dim DRStruct As DataRow
        Try
            DRStruct = DTStruct.NewRow
            DTStruct.Rows.Add(DRStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Data_Validation() As Boolean
        Try
            If AgL.RequiredField(CboSiteCode) Then Exit Function
            If AgL.RequiredField(TxtManualCode) Then Exit Function
            If AgL.RequiredField(CboName) Then Exit Function
            If AgL.RequiredField(TxtHO_YN) Then Exit Function
            If AgL.RequiredField(TxtActive) Then Exit Function

            If AgL.StrCmp(CboSiteCode.Text, AgLibrary.ClsConstant.SiteCode_Reserve) Then
                MsgBox("Site Code Is Not Valid!...")
                CboSiteCode.Focus()
                Exit Function
            End If

            If TxtHO_YN.Text = "Yes" Then
                mQry = "Select IfNull(Count(*),0) As Cnt From SiteMast Sm Where IfNull(Sm.Ho_Yn,'')='Y' " & IIf(Topctrl1.Mode = "Add", "", " And Code<>'" & mSearchCode & "'") & " "
                AgL.ECmd = AgL.Dman_Execute(mQry, AgL.Gcn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Head Office Already Exist!") : TxtHO_YN.Focus() : Exit Function
            End If

            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SiteMast Where Code='" & CboSiteCode.Text & "' ", AgL.Gcn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Site Code Already Exist!") : CboSiteCode.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SiteMast Where Name='" & CboName.Text & "' ", AgL.Gcn)
                If AgL.ECmd.ExecuteScalar() > 0 Then
                    If MsgBox("Site Name Already Exist! Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        CboName.Focus() : Exit Function
                    End If

                End If

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SiteMast Where ManualCode='" & TxtManualCode.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Manual Code Already Exist!") : TxtManualCode.Focus() : Exit Function

                mSearchCode = CboSiteCode.Text

                mEnviroId = AgL.GetMaxId("Enviro", "ID", AgL.GCn, AgL.PubDivCode, CboSiteCode.Text, 2, True, True, , AgL.Gcn_ConnectionString)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SiteMast Where Name='" & CboName.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Site Name Already Exist!") : CboName.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SiteMast Where ManualCode='" & TxtManualCode.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Manual Code Already Exist!") : TxtManualCode.Focus() : Exit Function
            End If

            If AgL.StrCmp(Topctrl1.Mode, "Add") Then
                Dim FrmObj As New FrmCopySettings
                FrmObj.StartPosition = FormStartPosition.CenterScreen
                FrmObj.ShowDialog()
                If FrmObj.mOkClicked Then
                    mCopySettingsFromSite = FrmObj.TxtSiteName.Tag
                Else
                    mCopySettingsFromSite = ""
                End If

                If mCopySettingsFromSite = "" Then
                    MsgBox("You Have Not Selected Any Site", MsgBoxStyle.Information)
                    Exit Function
                End If
            End If

            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub PictureBox_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PicPhoto.DoubleClick
        If Topctrl1.Mode = "Browse" Then Exit Sub

        Select Case sender.Name
            Case PicPhoto.Name
                AgL.GetPicture(sender, Photo_Byte)
        End Select
    End Sub

    Sub Show_Picture(ByVal PicBox As PictureBox, ByVal B As Byte())
        Dim Mem As MemoryStream
        Dim Img As Image

        Mem = New MemoryStream(B)
        Img = Image.FromStream(Mem)
        PicBox.Image = Img
    End Sub

    Sub Update_Picture(ByVal mTable As String, ByVal mColumn As String, ByVal mCondition As String, ByVal ByteArr As Byte())
        If ByteArr Is Nothing Then Exit Sub
        Dim sSQL As String = "Update " & mTable & " Set " & mColumn & "=@pic " & mCondition

        Dim cmd As SqliteCommand = New SqliteCommand(sSQL, AgL.GCn)
        Dim Pic As SQLiteParameter = New SQLiteParameter("@pic", SqlDbType.Image)
        Pic.Value = ByteArr
        cmd.Parameters.Add(Pic)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub ProcCopyToAllSite()
        mQry = " INSERT INTO Voucher_Type_Settings (Code, V_Type , EntryBy , EntryDate, Site_Code, Div_Code ) " & _
                " Select Convert(VARCHAR, (SELECT Max(Convert(NUMERIC,Code)) FROM Voucher_Type_Settings WHERE IsNumeric(Code)>0)  + Row_Number() OVER (ORDER BY V_Type)) AS Code, " & _
                " V_Type , " & AgL.Chk_Text(AgL.PubUserName) & ", " & _
                " " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", '" & mSearchCode & "', Div_Code " & _
                " FROM Voucher_Type_Settings " & _
                " Where Site_Code = '" & mCopySettingsFromSite & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "  Update Voucher_Type_Settings  " & _
                " Set  " & RetTableColStr("Voucher_Type_Settings") & _
                " FROM " & _
                " ( " & _
                " SELECT *    " & _
                " From Voucher_Type_Settings   " & _
                " Where Site_Code = '" & mCopySettingsFromSite & "'  " & _
                " ) V1 WHERE V1.V_TYpe = Voucher_Type_Settings.V_Type  " & _
                " AND V1.Div_Code = Voucher_Type_Settings.Div_Code  " & _
                " AND Voucher_Type_Settings.Site_Code =  '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Function RetTableColStr(ByVal TableName As String) As String
        Dim mQry$
        mQry = "DECLARE @ColStr VARCHAR(Max) " & _
                "SET @ColStr='' " & _
                "SELECT @ColStr=@ColStr + ' " & TableName & ".' + C.COLUMN_NAME + ' = V1.' + C.COLUMN_NAME  + ',' " & _
                "FROM INFORMATION_SCHEMA.COLUMNS C  " & _
                "WHERE C.TABLE_NAME ='" & TableName & "' " & _
                "AND C.COLUMN_NAME NOT IN ('UID', 'RowID', 'Site_Code','Div_Code','V_Type','Code','EntryBy','EntryDate') " & _
                "IF LEN(@ColStr)>0 SET @ColStr=substring (@ColStr,1,len(@ColStr)-1) " & _
                " SELECT @ColStr "
        RetTableColStr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    End Function
End Class
