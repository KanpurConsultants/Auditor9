Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient

Public Class FrmPartyMaster
    Dim AgL As AgLibrary.ClsMain
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""

    Dim mParty_Type As Integer = 0
    Dim mGroupCode = "", mGroupNature$ = "", mNature$ = "", mEntryMode$ = "Add"
    Dim mConn As SqlClient.SqlConnection, mConnectionString As String = ""

#Region "SubGroup Properties"
    Public Property AgLibObj() As AgLibrary.ClsMain
        Get
            AgLibObj = AgL
        End Get
        Set(ByVal value As AgLibrary.ClsMain)
            AgL = value
        End Set
    End Property

    Public Property GroupCode() As String
        Get
            GroupCode = mGroupCode
        End Get
        Set(ByVal value As String)
            mGroupCode = value
        End Set
    End Property

    Public Property GroupNature() As String
        Get
            GroupNature = mGroupNature
        End Get
        Set(ByVal value As String)
            mGroupNature = value
        End Set
    End Property

    Public Property Nature() As String
        Get
            Nature = mNature
        End Get
        Set(ByVal value As String)
            mNature = value
        End Set
    End Property

    Public Property Party_Type() As Integer
        Get
            Party_Type = mParty_Type
        End Get
        Set(ByVal value As Integer)
            mParty_Type = value
        End Set
    End Property

    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property

    Public Property Conn(Optional ByVal ConnectionString As String = "") As SqlClient.SqlConnection
        Get
            Conn = mConn
            ConnectionString = mConnectionString
        End Get
        Set(ByVal value As SqlClient.SqlConnection)
            mConn = value
            If ConnectionString Is Nothing Then ConnectionString = ""
            If ConnectionString.Trim = "" Then
                mConnectionString = value.ConnectionString
            Else
                mConnectionString = ConnectionString
            End If
        End Set
    End Property
#End Region

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
                Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
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
            AgL.WinSetting(Me, 450, 880, 0, 0)
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
        mQry = "Select SubGroup.SubCode As SearchCode " & _
        " From SubGroup Where IsNull(Party_Type,0) In (" & mParty_Type & ")"
        Topctrl1.FIniForm(DTMaster, mConn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Sub Ini_List()
        mQry = "Select Code As Code, Name As Name From SiteMast " & _
                " Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " Order By Name"
        TxtSite_Code.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select SubCode  As Code, Name As Name " & _
                " From SubGroup " & _
                " Where IsNull(Party_Type,0) In (" & mParty_Type & ") And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Site_Code", AgL.PubSiteCode, "CommonAc") & " " & _
                " Order By Name"
        TxtName.AgHelpDataSet = AgL.FillData(mQry, mConn)

        mQry = "Select SubCode  As Code, DispName As Name " & _
                " From SubGroup " & _
                " Where IsNull(Party_Type,0) In (" & mParty_Type & ") And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Site_Code", AgL.PubSiteCode, "CommonAc") & " " & _
                " Order By DispName"
        TxtDispName.AgHelpDataSet = AgL.FillData(mQry, mConn)

        mQry = "Select SubCode  As Code, ManualCode As Name From SubGroup " & _
            " Where IsNull(ManualCode,'')<>'' Order By ManualCode"
        TxtManualCode.AgHelpDataSet = AgL.FillData(mQry, mConn)

        mQry = "Select CityCode As Code, CityName As Name From City " & _
            " Order By CityName"
        TxtCityCode.AgHelpDataSet = AgL.FillData(mQry, mConn)

        mQry = "Select Distinct FatherNamePrefix  As Code, FatherNamePrefix As Name From SubGroup " & _
                " Where IsNull(FatherNamePrefix,'')<>'' " & _
                " Order By FatherNamePrefix"
        TxtFatherNamePrefix.AgHelpDataSet = AgL.FillData(mQry, mConn)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtSite_Code.AgSelectedValue = AgL.PubSiteCode
        TxtManualCode.Focus()
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
                    AgL.ECmd = mConn.CreateCommand
                    AgL.ETrans = mConn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.Dman_ExecuteNonQry("Delete From SubGroup Where SubCode='" & mSearchCode & "'", mConn, AgL.ECmd)
                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, mConn, AgL.ECmd, , , mSearchCode, mParty_Type, TxtSite_Code.AgSelectedValue, AgL.PubDivCode)

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
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            AgL.PubFindQry = "Select  Sg.SubCode As SearchCode, Sg.ManualCode As [Party ID], Sg.Name As [Name],  Sg.DispName As [Display Name], " & _
                                " Sg.Add1 As [Address1],  Sg.Add2 As [Address2],  Sg.Add3 As [Address3],  City7.CityName As [City Name], " & _
                                " Sg.Phone As [Phone No.],  Sg.Mobile As [Mobile No.] " & _
                                " From  SubGroup Sg " & _
                                " Left Join  City City7 On City7.CityCode = Sg.CityCode " & _
                                " Where IsNull(Sg.Party_Type,0) In (" & mParty_Type & ")"
            AgL.PubFindQryOrdBy = "[Name]"
            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
            If AgL.PubSearchRow <> " Then" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                MoveRec()
            End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn

    End Sub

    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            mEntryMode = Topctrl1.Mode
            If Not Data_Validation() Then Exit Sub

            AgL.ECmd = mConn.CreateCommand
            AgL.ETrans = mConn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            SaveSubGroupRecord(mConn, AgL.ECmd, mConnectionString)

            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, mConn, AgL.ECmd, , , mSearchCode, mParty_Type, TxtSite_Code.AgSelectedValue, AgL.PubDivCode)

            AgL.ETrans.Commit()
            mTrans = False
            FIniMaster(0, 1)
            Topctrl1_tbRef()
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

    Public Sub SaveSubGroupRecord(ByVal TmConn As SqlClient.SqlConnection, ByVal TmCmd As SqlClient.SqlCommand, ByVal TmConnectionString As String)
        If AgL.StrCmp(mEntryMode, "Add") Then
            mSearchCode = AgL.CreateSubGroup(AgL, TmConn, TmCmd, TmConnectionString, TxtDispName.Text, TxtManualCode.Text, mGroupCode, mGroupNature, mNature, mParty_Type, TxtSite_Code.AgSelectedValue)
        End If
        If mSearchCode.Trim = "" Then Err.Raise(1, , "Fatel Error!" & vbCrLf & "Error In SubCode Generation!")

        mQry = "Update SubGroup Set Name = " & AgL.Chk_Text(TxtName.Text) & ", DispName = " & AgL.Chk_Text(TxtDispName.Text) & ", " & _
                " GroupCode=" & AgL.Chk_Text(mGroupCode) & ",GroupNature=" & AgL.Chk_Text(mGroupNature) & ",Nature=" & AgL.Chk_Text(mNature) & ", " & _
                " ManualCode=" & AgL.Chk_Text(TxtManualCode.Text) & ", Add1 = " & AgL.Chk_Text(TxtAdd1.Text) & ", Add2 = " & AgL.Chk_Text(TxtAdd2.Text) & ", Add3 = " & AgL.Chk_Text(TxtAdd3.Text) & ", CityCode = " & AgL.Chk_Text(TxtCityCode.AgSelectedValue) & "," & _
                " Phone = " & AgL.Chk_Text(TxtPhone.Text) & ", Mobile = " & AgL.Chk_Text(TxtMobile.Text) & ", DOB=" & AgL.ConvertDate(TxtDob.Text) & ", FatherNamePrefix=" & AgL.Chk_Text(TxtFatherNamePrefix.Text) & ", FatherName=" & AgL.Chk_Text(TxtFatherName.Text) & ", " & _
                " EMail = " & AgL.Chk_Text(TxtEMail.Text) & ", CommonAc = " & IIf(AgL.StrCmp(TxtCommonAc.Text, "Yes"), 1, 0) & ", " & _
                " U_AE='" & AgL.MidStr(mEntryMode, 0, 1) & "', Edit_Date='" & Format(AgL.PubLoginDate, "Short Date") & "', ModifiedBy = '" & AgL.PubUserName & "' Where SubCode='" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, TmConn, TmCmd)

    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                mQry = "Select SubGroup.* " & _
                    " From SubGroup Where SubCode='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, mConn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtSite_Code.AgSelectedValue = AgL.XNull(.Rows(0)("Site_Code"))
                        TxtCommonAc.Text = IIf(AgL.VNull(.Rows(0)("CommonAc")), "Yes", "No")
                        TxtManualCode.AgSelectedValue = AgL.XNull(.Rows(0)("SubCode"))
                        TxtName.AgSelectedValue = AgL.XNull(.Rows(0)("SubCode"))
                        TxtDispName.AgSelectedValue = AgL.XNull(.Rows(0)("SubCode"))
                        TxtFatherNamePrefix.Text = AgL.XNull(.Rows(0)("FatherNamePrefix"))
                        TxtFatherName.Text = AgL.XNull(.Rows(0)("FatherName"))
                        TxtAdd1.Text = AgL.XNull(.Rows(0)("Add1"))
                        TxtAdd2.Text = AgL.XNull(.Rows(0)("Add2"))
                        TxtAdd3.Text = AgL.XNull(.Rows(0)("Add3"))
                        TxtCityCode.AgSelectedValue = AgL.XNull(.Rows(0)("CityCode"))
                        TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                        TxtMobile.Text = AgL.XNull(.Rows(0)("Mobile"))
                        TxtEMail.Text = AgL.XNull(.Rows(0)("EMail"))
                        TxtDob.Text = AgL.RetDate(AgL.XNull(.Rows(0)("DOB")))
                    End If
                End With
            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = ""
        TxtCommonAc.Text = "Yes"
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        TxtSite_Code.Enabled = False
        TxtName.Enabled = False
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

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles TxtSite_Code.Validating, TxtDispName.Validating, TxtName.Validating, TxtManualCode.Validating, _
                TxtCityCode.Validating, TxtAdd1.Validating, TxtAdd2.Validating, TxtAdd3.Validating, _
                TxtMobile.Validating, TxtPhone.Validating, TxtEMail.Validating, TxtFatherName.Validating, TxtFatherNamePrefix.Validating
        Try
            Select Case sender.NAME
                Case TxtDispName.Name, TxtManualCode.Name
                    TxtName.Text = TxtDispName.Text + Space(1) + "{" + TxtManualCode.Text + "}"
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Data_Validation() As Boolean
        Try
            If AgL.RequiredField(TxtSite_Code) Then Exit Function
            If AgL.RequiredField(TxtDispName) Then Exit Function
            If AgL.RequiredField(TxtManualCode) Then Exit Function
            If AgL.RequiredField(TxtName) Then Exit Function

            If TxtEMail.Text.Trim <> "" Then If Not AgL.IsValid_EMailId(TxtEMail, "EMail ID") Then Exit Function

            If AgL.StrCmp(mEntryMode, "Add") Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where ManualCode='" & TxtManualCode.Text & "' ", mConn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Party ID Already Exist!") : TxtManualCode.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where DispName='" & TxtDispName.Text & "' ", mConn)
                If AgL.ECmd.ExecuteScalar() > 0 Then If MsgBox("Display Name Already Exist!..." & vbCrLf & "Want to Continue!", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then TxtDispName.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where Name='" & TxtName.Text & "' ", mConn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Name Already Exist!") : TxtDispName.Focus() : Exit Function
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where ManualCode='" & TxtManualCode.Text & "' And SubCode<>'" & mSearchCode & "' ", mConn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Party ID Already Exist!") : TxtManualCode.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where DispName='" & TxtDispName.Text & "' And SubCode<>'" & mSearchCode & "' ", mConn)
                If AgL.ECmd.ExecuteScalar() > 0 Then If MsgBox("Display Name Already Exist!..." & vbCrLf & "Want to Continue!", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then TxtDispName.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where Name='" & TxtName.Text & "' And SubCode<>'" & mSearchCode & "' ", mConn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Name Already Exist!") : TxtDispName.Focus() : Exit Function
            End If

            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function
End Class
