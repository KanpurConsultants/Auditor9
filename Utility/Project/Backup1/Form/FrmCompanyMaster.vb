'Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmCompanyMaster
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mCompCode As String = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
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

    Private Sub FrmCompanyMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            AgL.WinSetting(Me, 550, 880, 0, 0)
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
            mQry = " SELECT Comp_Code As SearchCode " & _
                    " FROM Company C " & _
                    " Where IsNull(C.DeletedYN,'N') <> 'Y' " & _
                    " ORDER BY Comp_Name "

            Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
        End If
    End Sub


    Sub Ini_List()
        'mQry = " Select Code  As Code, Code As Name " & _
        '        " From SiteMast " & _
        '        " Order By Name"
        'TxtSite_Code.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Comp_Code AS Code,Comp_Name AS Name " & _
                " FROM Company " & _
                " ORDER BY Comp_Name "
        TxtCompanyName.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Comp_Code AS Code,CentralData_Path " & _
                " FROM Company "
        TxtPreviousDB.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub


    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long

        Dim GcnRead As New SqlClient.SqlConnection
        GcnRead.ConnectionString = AgL.Gcn_ConnectionString
        GcnRead.Open()

        Try
            FClear()
            BlankText()
            If AgL.PubMoveRecApplicable Then
                If BMBMaster.Position < 0 Then Exit Sub
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
            Else
                If AgL.PubSearchRow <> "" Then mSearchCode = AgL.PubSearchRow
            End If

            If mSearchCode <> "" Then
                mQry = "Select Company.* " & _
                    " From Company " & _
                    " Where Comp_Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)

                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtComp_Code.Text = AgL.XNull(.Rows(0)("Comp_Code"))
                        TxtSName.Text = AgL.XNull(.Rows(0)("SName"))
                        TxtDiv_Code.Text = AgL.XNull(.Rows(0)("Div_Code"))
                        TxtCompanyName.AgSelectedValue = AgL.XNull(.Rows(0)("Comp_Code"))
                        TxtSite_Code.Text = AgL.XNull(.Rows(0)("Site_Code"))
                        TxtDeletedYn.Text = IIf(AgL.XNull(.Rows(0)("DeletedYN")).ToString.Trim = "Y", "Yes", "No")
                        TxtStartDate.Text = Format(AgL.XNull(.Rows(0)("Start_Dt")), AgLibrary.ClsConstant.DateFormat_ShortDate)
                        TxtEndDate.Text = Format(AgL.XNull(.Rows(0)("End_Dt")), AgLibrary.ClsConstant.DateFormat_ShortDate)
                        TxtAdd1.Text = AgL.XNull(.Rows(0)("address1"))
                        TxtAdd2.Text = AgL.XNull(.Rows(0)("address2"))
                        TxtCity.Text = AgL.XNull(.Rows(0)("city"))
                        TxtPinNo.Text = AgL.XNull(.Rows(0)("pin"))
                        TxtState.Text = AgL.XNull(.Rows(0)("State"))
                        TxtCountry.Text = AgL.XNull(.Rows(0)("Country"))
                        TxtPhone.Text = AgL.XNull(.Rows(0)("phone"))
                        TxtFax.Text = AgL.XNull(.Rows(0)("fax"))
                        TxtCSTNo.Text = AgL.XNull(.Rows(0)("cstno"))
                        TxtCSTDate.Text = Format(AgL.XNull(.Rows(0)("cstdate")), AgLibrary.ClsConstant.DateFormat_ShortDate)
                        TxtSerialKeyNo.Text = AgL.XNull(.Rows(0)("SerialKeyNo"))
                        TxtVatDate.Text = Format(AgL.XNull(.Rows(0)("VatDate")), AgLibrary.ClsConstant.DateFormat_ShortDate)
                        TxtEMailId.Text = AgL.XNull(.Rows(0)("EMail"))
                        Txttinno.Text = AgL.XNull(.Rows(0)("TinNo"))
                        TxtPanNo.Text = AgL.XNull(.Rows(0)("PANNo"))
                        TxtCurrentYear.Text = AgL.XNull(.Rows(0)("cyear"))
                        TxtPreviousYear.Text = AgL.XNull(.Rows(0)("pyear"))
                        TxtPanNo.Text = AgL.XNull(.Rows(0)("PANNo"))
                        TxtUseSiteNameAsCompanyName.Text = IIf(AgL.VNull(.Rows(0)("UseSiteNameAsCompanyName")), "Yes", "No")
                        TxtPreviousDB.Text = AgL.XNull(.Rows(0)("PrevDBName"))
                        TxtCentralDataPath.Text = AgL.XNull(.Rows(0)("CentralData_Path"))
                        TxtReportPath.Text = AgL.XNull(.Rows(0)("Repo_Path"))
                        TxtPreviousDB.Text = AgL.XNull(.Rows(0)("PrevDBName"))
                        TxtDBPrefix.Text = AgL.XNull(.Rows(0)("DbPrefix"))
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
            If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then Topctrl1.tAdd = False
            If Not (AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.PubIsUserAdmin) Then Topctrl1.tEdit = False
        End Try
    End Sub
    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = ""
        TxtDeletedYn.Text = "No" : TxtUseSiteNameAsCompanyName.Text = "No"
    End Sub
    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        ''Coding To Enable/Disable Controls
        If Topctrl1.Mode = "Edit" Then
            TxtComp_Code.Enabled = False : TxtDiv_Code.Enabled = False : TxtCentralDataPath.Enabled = False : TxtDBPrefix.Enabled = False
        End If
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
            If AgL.RequiredField(TxtComp_Code, "Company Code") Then Exit Function
            If AgL.RequiredField(TxtSName, "Short Name") Then Exit Function
            If AgL.RequiredField(TxtDiv_Code, "Division Code") Then Exit Function
            If AgL.RequiredField(TxtCompanyName, "Company Name") Then Exit Function
            If AgL.RequiredField(TxtComp_Code, "Company Code") Then Exit Function
            If AgL.RequiredField(TxtCity, "City") Then Exit Function
            If AgL.RequiredField(TxtStartDate, "Start Date") Then Exit Function
            If AgL.RequiredField(TxtEndDate, "End Date") Then Exit Function
            If AgL.RequiredField(TxtCurrentYear, "Current Year") Then Exit Function
            If AgL.RequiredField(TxtPreviousYear, "Previous Year") Then Exit Function
            If AgL.RequiredField(TxtComp_Code, "Company Code") Then Exit Function
            If AgL.RequiredField(TxtDBPrefix, "Database Prefix") Then Exit Function
            If AgL.RequiredField(TxtCentralDataPath, "Current Database") Then Exit Function
            If AgL.RequiredField(TxtSerialKeyNo, "Serial Key No.") Then Exit Function
            If AgL.RequiredField(TxtUseSiteNameAsCompanyName, "Use Site Name as Company") Then Exit Function


            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("SELECT count(*) FROM Company WHERE Comp_Code='" & TxtComp_Code.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Company Code Already Exist!") : TxtComp_Code.Focus() : Exit Function

                mSearchCode = TxtComp_Code.Text
            Else
                AgL.ECmd = AgL.Dman_Execute("SELECT count(*) FROM Company WHERE Comp_Code='" & TxtComp_Code.Text & "' And Comp_Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Company Code Already Exist!") : TxtComp_Code.Focus() : Exit Function
            End If

            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
        TxtStartDate.Validating, TxtEndDate.Validating, TxtUseSiteNameAsCompanyName.Validating, TxtComp_Code.Validating, TxtCompanyName.Validating, TxtAdd1.Validating, _
        TxtAdd2.Validating, TxtCentralDataPath.Validating, TxtCity.Validating, TxtCountry.Validating, TxtCSTDate.Validating, TxtCSTNo.Validating, TxtCurrentYear.Validating, _
        TxtPreviousYear.Validating, TxtPreviousDB.Validating, TxtReportPath.Validating, TxtSerialKeyNo.Validating, TxtSite_Code.Validating, TxtSName.Validating, TxtState.Validating, _
        Txttinno.Validating, TxtVatDate.Validating

        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing

        Try
            Select Case sender.NAME
                Case TxtUseSiteNameAsCompanyName.Name
                    If TxtUseSiteNameAsCompanyName.Text.ToString.Trim <> "" Then
                        LblUseSiteNameAsCompanyName.Tag = "1"
                    End If
            End Select

            If TxtStartDate.Text.ToString.Trim <> "" And TxtEndDate.Text.ToString.Trim <> "" Then
                TxtCurrentYear.Text = CDate(TxtStartDate.Text).Year.ToString + "-" + CDate(TxtEndDate.Text).Year.ToString
                TxtPreviousYear.Text = (CDate(TxtStartDate.Text).Year - 1).ToString + "-" + (CDate(TxtEndDate.Text).Year - 1).ToString
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If DtTemp IsNot Nothing Then DtTemp.Dispose()
        End Try
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtComp_Code.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False


        Try
            MastPos = BMBMaster.Position


            If DTMaster.Rows.Count > 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True
                    AgL.Dman_ExecuteNonQry("Update Company set DeletedYN = 'Y' Where Comp_Code='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = False


                    FIniMaster(1)
                    Topctrl1_tbRef()
                    MoveRec()
                End If
            End If
        Catch Ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        Topctrl1.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DispText()
        TxtCompanyName.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        'If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = " SELECT C.Comp_Code As SearchCode, C.Comp_Name as [Company Name], C.address1 AS Address1, C.address2 AS Address2, C.city AS City, C.pin AS PIN, C.State, C.phone AS Phone, C.Country " & _
                                " FROM Company C " & _
                                " Where IsNull(C.DeletedYN,'N') <> 'Y' "



            AgL.PubFindQryOrdBy = "[Company Name]"



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
                mQry = " INSERT INTO Company ( Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName, DbPrefix, Repo_Path, Start_Dt, " & _
                        " End_Dt, address1,	address2, city,	pin, phone, fax, cstno,	cstdate, cyear, pyear, SerialKeyNo,	SName, " & _
                        " EMail,	VatNo,	VatDate,	TinNo,	Site_Code,	PANNo,	State,	U_Name,	U_EntDt,	U_AE,	DeletedYN,	Country, " & _
                        " UseSiteNameAsCompanyName )" & _
                        " VALUES( " & _
                        " '" & mSearchCode & "', " & AgL.Chk_Text(TxtDiv_Code.Text) & ", " & AgL.Chk_Text(TxtCompanyName.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtCentralDataPath.Text) & ", " & AgL.Chk_Text(TxtPreviousDB.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtDBPrefix.Text) & ", " & AgL.Chk_Text(TxtReportPath.Text) & "," & AgL.ConvertDate(TxtStartDate.Text) & "," & _
                        " " & AgL.ConvertDate(TxtEndDate.Text) & ", " & AgL.Chk_Text(TxtAdd1.Text) & ", " & AgL.Chk_Text(TxtAdd2.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtCity.Text) & ", " & AgL.Chk_Text(TxtPinNo.Text) & ", " & AgL.Chk_Text(TxtPhone.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtFax.Text) & ", " & AgL.Chk_Text(TxtCSTNo.Text) & "," & AgL.ConvertDate(TxtCSTDate.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtCurrentYear.Text) & "," & AgL.Chk_Text(TxtPreviousYear.Text) & ", " & AgL.Chk_Text(TxtSerialKeyNo.Text) & "," & _
                        " " & AgL.Chk_Text(TxtSName.Text) & ", " & AgL.Chk_Text(TxtEMailId.Text) & ", " & AgL.Chk_Text(Txttinno.Text) & "," & AgL.ConvertDate(TxtVatDate.Text) & "," & _
                        " " & AgL.Chk_Text(Txttinno.Text) & ", " & AgL.Chk_Text(TxtSite_Code.Text) & "," & AgL.Chk_Text(TxtPanNo.Text) & ", " & AgL.Chk_Text(TxtState.Text) & "," & _
                        " '" & AgL.PubUserName & "'," & AgL.ConvertDate(AgL.PubLoginDate) & ",'A',LEFT('" & TxtDeletedYn.Text & "',1), " & _
                        " " & AgL.Chk_Text(TxtCountry.Text) & ", " & IIf(AgL.StrCmp(TxtUseSiteNameAsCompanyName.Text, "Yes"), 1, 0) & " ) "

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = " UPDATE dbo.Company Set " & _
                         " Div_Code = " & AgL.Chk_Text(TxtDiv_Code.Text) & ", " & _
                         " Comp_Name = " & AgL.Chk_Text(TxtCompanyName.Text) & ", " & _
                         " CentralData_Path = " & AgL.Chk_Text(TxtCentralDataPath.Text) & ", " & _
                         " PrevDBName = " & AgL.Chk_Text(TxtPreviousDB.Text) & ", " & _
                         " Repo_Path = " & AgL.Chk_Text(TxtReportPath.Text) & ", " & _
                         " Start_Dt = " & AgL.ConvertDate(TxtStartDate.Text) & ", " & _
                         " End_Dt = " & AgL.ConvertDate(TxtEndDate.Text) & ", " & _
                         " address1 = " & AgL.Chk_Text(TxtAdd1.Text) & ", " & _
                         " address2 = " & AgL.Chk_Text(TxtAdd2.Text) & ", " & _
                         " city = " & AgL.Chk_Text(TxtCity.Text) & ", " & _
                         " pin = " & AgL.Chk_Text(TxtPinNo.Text) & ", " & _
                         " phone = " & AgL.Chk_Text(TxtPhone.Text) & ", " & _
                         " fax = " & AgL.Chk_Text(TxtFax.Text) & ", " & _
                         " cstno = " & AgL.Chk_Text(TxtCSTNo.Text) & ", " & _
                         " cstdate = " & AgL.ConvertDate(TxtCSTDate.Text) & ", " & _
                         " cyear = " & AgL.Chk_Text(TxtCurrentYear.Text) & ", " & _
                         " pyear = " & AgL.Chk_Text(TxtPreviousYear.Text) & ", " & _
                         " SerialKeyNo = " & AgL.Chk_Text(TxtSerialKeyNo.Text) & ", " & _
                         " SName = " & AgL.Chk_Text(TxtSName.Text) & ", " & _
                         " EMail = " & AgL.Chk_Text(TxtEMailId.Text) & ", " & _
                         " VatNo = " & AgL.Chk_Text(Txttinno.Text) & ", " & _
                         " VatDate = " & AgL.ConvertDate(TxtVatDate.Text) & ", " & _
                         " TinNo = " & AgL.Chk_Text(Txttinno.Text) & ", " & _
                         " Site_Code = " & AgL.Chk_Text(TxtSite_Code.Text) & ", " & _
                         " PANNo = " & AgL.Chk_Text(TxtPanNo.Text) & ", " & _
                         " State = " & AgL.Chk_Text(TxtState.Text) & ", " & _
                         " U_AE = 'E', " & _
                         " Country = " & AgL.Chk_Text(TxtCountry.Text) & ", " & _
                         " UseSiteNameAsCompanyName = " & IIf(AgL.StrCmp(TxtUseSiteNameAsCompanyName.Text, "Yes"), 1, 0) & " " & _
                         " Where Comp_Code='" & mSearchCode & "' "


                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            End If


            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)


            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
            AgL.ETrans.Commit()
            mTrans = False
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
End Class
