Public Class FrmUser
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mCode As String = ""
    Dim mStrModuleList As String = ""

    Private Const StrModuleSql As String = "SELECT MnuModule AS Module FROM User_Permission WHERE UserName = 'SA' GROUP BY MnuModule"


    Private Const Col_SNo As Byte = 0
    'Dgl1 Column Index Constants
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Private Const Col1CompCode As Byte = 1
    Private Const Col1SelectSite As Byte = 2
    Private Const Col1SiteList As Byte = 3

    'Dgl2 Column Index Constants
    Public WithEvents DGL2 As New AgControls.AgDataGrid
    Private Const Col2Select As Byte = 1
    Private Const Col2Module As Byte = 2

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal AglibVar As AgLibrary.ClsMain)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
        AgL = AglibVar
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
        With AgCL
            .AddAgTextColumn(DGL1, "DGL1SNo", 40, 5, "S.No.", True, True, False)
            .AddAgTextColumn(DGL1, "DGL1CompCode", 300, 6, "Company Name", True, False, False, True)
            .AddAgButtonColumn(DGL1, "DGL1SelectSite", 30, " ", True, False, , , , , 10, "1")
            .AddAgTextColumn(DGL1, "DGL1SiteList", 100, 6, "Site List", True, True, False, True)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.ColumnHeadersHeight = 40

        With AgCL
            .AddAgTextColumn(Dgl2, "Dgl2SNo", 40, 5, "S.No.", True, True, False)
            AgL.AddCheckColumn(DGL2, "Dgl2Select", 60, 50, "Select", True, True)
            .AddAgTextColumn(DGL2, "Dgl2Module", 200, 6, "Module Name", True, True, False, False)
        End With
        AgL.AddAgDataGrid(DGL2, Pnl2)
        DGL2.ColumnHeadersHeight = 40
        DGL2.AllowUserToAddRows = False
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
            AgL.WinSetting(Me, 500, 880, 0, 0)
            AgL.GridDesign(DGL1)
            AgL.GridDesign(DGL2)
            IniGrid()
            Topctrl1.ChangeAgGridState(DGL1, False)
            Topctrl1.ChangeAgGridState(DGL2, False)
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Dim CondStr As String = " Where 1 = 1 "

        If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
            AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

            CondStr += " And User_Name = '" & AgL.PubUserName & "' "
        End If

        mQry = "Select User_Name As SearchCode " & _
        " From UserMast " & CondStr

        Topctrl1.FIniForm(DTMaster, AgL.GcnMain, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Sub Ini_List()
        mQry = "Select User_Name As Code, User_Name As Name From UserMast " & _
            "  Order By User_Name"
        TxtUser_Name.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnMain)

        mQry = "Select Distinct Description As Code, Description As Name From UserMast " & _
            "  Where IsNull(Description,'')<>'' Order By Description"
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnMain)

        mQry = "SELECT C.Comp_Code AS Code, C.Comp_Name + ' (' + C.cyear + ')' AS Name " & _
                " FROM Company C " & _
                " WHERE C.Div_Code = '" & AgL.PubDivCode & "' AND IsNULL(C.DeletedYN,'N')<>'Y' " & _
                " ORDER BY C.Comp_Name, C.Start_Dt Desc "
        DGL1.AgHelpDataSet(Col1CompCode) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        Call ProcFillModule()
        TxtUser_Name.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False


        Try
            MastPos = BMBMaster.Position


            If DTMaster.Rows.Count > 0 Then

                If TxtUser_Name.Text.ToUpper.Trim = "SA" Or AgL.StrCmp(TxtUser_Name.Text, AgL.PubUserName) Then
                    MsgBox(AgL.PubUserName & " User can not be deleted!!!" & vbCrLf & "Either User is SA (System Adminstrator) Or User Is Currently Busy...", vbInformation, "Validation Check")
                    TxtUser_Name.Focus()
                    Exit Sub
                End If

                If AgL.Check_Entry("Login_Log", "User_Name", mSearchCode, AgLibrary.ClsMain.FieldType.StringType, "Login Log", AgL.GcnMain) = False Then Exit Sub
                If AgL.Check_Entry("User_Permission", "UserName", mSearchCode, AgLibrary.ClsMain.FieldType.StringType, "User Permission", AgL.GcnMain) = False Then Exit Sub
                If AgL.Check_Entry("User_Control_Permission", "UserName", mSearchCode, AgLibrary.ClsMain.FieldType.StringType, "User Control Permission", AgL.GcnMain) = False Then Exit Sub
                If AgL.Check_Entry("User_Target", "UserName", mSearchCode, AgLibrary.ClsMain.FieldType.StringType, "User Target", AgL.GCn) = False Then Exit Sub
                If AgL.Check_Entry("UserSite", "User_Name", mSearchCode, AgLibrary.ClsMain.FieldType.StringType, "User Site", AgL.GCn) = False Then Exit Sub


                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AgL.GcnMain.CreateCommand
                    AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.Dman_ExecuteNonQry("Delete From UserMast Where User_Name='" & mSearchCode & "'", AgL.GcnMain, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = False

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn)

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
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            Dim CondStr As String = " Where 1 = 1 "

            If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
                AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                CondStr += " And User_Name = '" & AgL.PubUserName & "' "
            End If


            AgL.PubFindQry = "Select  User_Name As SearchCode, User_Name As [User Name], Description," & _
                                " Case IsNull(Admin,'N') When 'Y' Then 'Yes' Else 'No' End As Administrator " & _
                                " From  UserMast " & CondStr

            AgL.PubFindQryOrdBy = "[User Name]"

            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
            If AgL.PubSearchRow <> "" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
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
        Dim I As Integer
        Dim mUploadDate As String


        Try
            MastPos = BMBMaster.Position

            If Not Data_Validation() Then Exit Sub

            If AgL.PubManageOfflineData Then
                mUploadDate = AgL.PubLoginDate
            Else
                mUploadDate = ""
            End If

            AgL.ECmd = AgL.GcnMain.CreateCommand
            AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans

            If AgL.PubManageOfflineData Then
                AgL.ECmdSite = AgL.GcnSite.CreateCommand
                AgL.ETransSite = AgL.GcnSite.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmdSite.Transaction = AgL.ETransSite
            End If

            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into UserMast (Code, USER_NAME, PASSWD, Description, Admin, ModuleList, UpLoadDate) Values(" & _
                        " '" & mCode & "', '" & mSearchCode & "', " & AgL.Chk_Text(AgL.CODIFY(TxtPassword.Text)) & "," & _
                        " " & AgL.Chk_Text(TxtDescription.Text) & ",'" & AgL.MidStr(TxtAdminYn.Text, 0, 1) & "', " & _
                        " " & AgL.Chk_Text(mStrModuleList) & ", " & AgL.ConvertDate(mUploadDate) & ") "

                If AgL.PubManageOfflineData Then AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite, AgL.ECmdSite)
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
            Else
                mQry = "Update UserMast Set " & _
                        " Description=" & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                        " PASSWD = " & AgL.Chk_Text(AgL.CODIFY(TxtPassword.Text)) & "," & _
                        " Admin='" & AgL.MidStr(TxtAdminYn.Text, 0, 1) & "', " & _
                        " ModuleList = " & AgL.Chk_Text(mStrModuleList) & ", " & _
                        " UpLoadDate = " & AgL.ConvertDate(mUploadDate) & " " & _
                        " Where USER_NAME='" & mSearchCode & "' "

                If AgL.PubManageOfflineData Then AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite, AgL.ECmdSite)
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
            End If


            If Topctrl1.Mode = "Edit" Then
                mQry = "Delete From UserSite Where User_Name = '" & mSearchCode & "'"

                If AgL.PubManageOfflineData Then AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite, AgL.ECmdSite)
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
            End If

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1CompCode, I).Value <> "" Then
                        mQry = "INSERT INTO UserSite ( User_Name, CompCode, Sitelist, UpLoadDate ) VALUES  (" & _
                                " '" & mSearchCode & "', " & AgL.Chk_Text(.AgSelectedValue(Col1CompCode, I)) & "," & _
                                " " & AgL.Chk_Text(.Item(Col1SiteList, I).Value) & ", " & _
                                " " & AgL.ConvertDate(mUploadDate) & ")"

                        If AgL.PubManageOfflineData Then AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite, AgL.ECmdSite)
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
                    End If
                Next I
            End With

            If AgL.PubManageOfflineData Then AgL.ETransSite.Commit()

            AgL.ETrans.Commit()
            mTrans = False

            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn)

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
            If mTrans = True Then
                AgL.ETrans.Rollback()
                If AgL.PubManageOfflineData Then AgL.ETransSite.Rollback()
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Dim I As Integer
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                mQry = "Select Um.* " & _
                    " From UserMast Um Where Um.User_Name='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GcnMain)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        mCode = AgL.XNull(.Rows(0)("Code"))
                        TxtUser_Name.AgSelectedValue = AgL.XNull(.Rows(0)("User_Name"))
                        TxtDescription.AgSelectedValue = AgL.XNull(.Rows(0)("Description"))
                        TxtOldPassword.Tag = AgL.DCODIFY(AgL.XNull(.Rows(0)("PassWd")))
                        TxtPassword.Text = AgL.DCODIFY(AgL.XNull(.Rows(0)("PassWd")))
                        TxtVarifyPassword.Text = AgL.DCODIFY(AgL.XNull(.Rows(0)("PassWd")))
                        TxtAdminYn.Text = IIf(AgL.XNull(.Rows(0)("Admin")).ToString.ToUpper = "Y", "Yes", "No")
                        mStrModuleList = AgL.XNull(.Rows(I)("ModuleList"))
                        Call ProcFillModule()
                    End If
                End With

                DsTemp = Nothing

                mQry = "Select Us.* From UserSite Us Where Us.User_Name = '" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GcnMain)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            DGL1.Rows.Add()
                            DGL1.Item(Col_SNo, I).Value = DGL1.Rows.Count - 1
                            DGL1.AgSelectedValue(Col1CompCode, I) = AgL.XNull(.Rows(I)("CompCode"))
                            DGL1.Item(Col1SiteList, I).Value = AgL.XNull(.Rows(I)("SiteList"))                            
                        Next
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
        mSearchCode = "" : mCode = "" : mStrModuleList = ""
        TxtChangePassword.Text = "No"
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
        DGL2.RowCount = 1 : DGL2.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If Topctrl1.Mode = "Edit" Then
            TxtUser_Name.Enabled = False

            If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
                    AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                TxtPassword.ReadOnly = True
                TxtVarifyPassword.ReadOnly = True
            End If

            TxtChangePassword.ReadOnly = False
            TxtOldPassword.ReadOnly = False
        Else
            TxtChangePassword.ReadOnly = True
            TxtOldPassword.ReadOnly = True
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

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
           TxtUser_Name.Enter, TxtAdminYn.Enter, TxtChangePassword.Enter, TxtOldPassword.Enter, TxtPassword.Enter, TxtVarifyPassword.Enter
        Try
            Select Case sender.name
                Case TxtPassword.Name, TxtVarifyPassword.Name
                    If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
                        If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
                                AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then
                            TxtPassword.ReadOnly = True
                            TxtVarifyPassword.ReadOnly = True
                        End If

                        If AgL.StrCmp(TxtChangePassword.Text, "Yes") Then
                            If TxtOldPassword.Text.Trim = TxtOldPassword.Tag.Trim Then
                                TxtPassword.ReadOnly = False
                                TxtVarifyPassword.ReadOnly = False
                            End If
                        End If
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
           TxtUser_Name.Validating, TxtAdminYn.Validating, TxtChangePassword.Validating, TxtOldPassword.Validating, _
           TxtPassword.Validating, TxtVarifyPassword.Validating
        Try
            Select Case sender.NAME
                Case TxtOldPassword.Name
                    If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
                        If AgL.StrCmp(TxtChangePassword.Text, "Yes") Then
                            If TxtOldPassword.Text.Trim <> TxtOldPassword.Tag.Trim Then
                                MsgBox("Please Reenter Old Password!...") : e.Cancel = True
                            End If
                        End If
                    End If

                Case TxtChangePassword.Name
                    '<Executable code>
            End Select
            Call Calculation()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        If Topctrl1.Mode = "Browse" Then Exit Sub
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub

        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1CompCode
                    If Me.Controls("HelpDg") IsNot Nothing And CType(Me.Controls("HelpDg"), AgControls.AgDataGrid).CurrentCell IsNot Nothing Then
                        With CType(Me.Controls("HelpDg"), AgControls.AgDataGrid)
                            If .CurrentCell Is Nothing Or DGL1.AgSelectedValue(mColumnIndex, mRowIndex).Trim = "" Then
                                'DGL1.AgSelectedValue(<ColumnIndex>, mRowIndex) = ""
                            Else
                                'DGL1.AgSelectedValue(<ColumnIndex>, mRowIndex) = AgL.XNull(.Item("Item", .CurrentCell.RowIndex).Value)
                            End If
                        End With
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGL1.EditingControlShowing
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If TypeOf e.Control Is ComboBox Then
            e.Control.Text = ""
        End If
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                sender.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        Try
            Select Case sender.CurrentCell.ColumnIndex
                'Case <Dgl_Column>
                '    <Executable Code>
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGL1.RowsAdded
        Try
            sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        Catch ex As Exception

        End Try
        Call Calculation()
    End Sub

    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DGL1.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try

        AgL.FSetSNo(sender, Col_SNo)
        Call Calculation()
    End Sub

    Private Sub DGL1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellContentClick
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim FrmObjSiteList As FrmSiteList
        Dim mSiteList$ = ""
        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1SelectSite
                    If DGL1.Item(Col1SiteList, mRowIndex).Value Is Nothing Then DGL1.Item(Col1SiteList, mRowIndex).Value = ""
                    mSiteList = DGL1.Item(Col1SiteList, mRowIndex).Value
                    FrmObjSiteList = New FrmSiteList : FrmObjSiteList.SiteListStr = mSiteList : FrmObjSiteList.ShowDialog()
                    mSiteList = FrmObjSiteList.SiteListStr : FrmObjSiteList = Nothing
                    DGL1.Item(Col1SiteList, mRowIndex).Value = mSiteList
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl2.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                'sender.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Try
            Select Case Dgl2.CurrentCell.ColumnIndex
                Case Col2Select
                    If e.KeyCode = Keys.Space Then
                        AgL.ProcSetCheckColumnCellValue(DGL2, Col2Select)
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgl2_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl2.CellMouseUp
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex

            If Dgl2.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl2.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case DGL2.CurrentCell.ColumnIndex
                Case Col2Select
                    Call AgL.ProcSetCheckColumnCellValue(DGL2, Col2Select)
            End Select
            Calculation()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Calculation()
        'Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub
        'With DGL2
        '    For I = 0 To .Rows.Count - 1
        '        If .Item(Col2Vehicle, I).Value Is Nothing Then .Item(Col2Vehicle, I).Value = ""
        '        If .Item(Col2TollTax, I).Value Is Nothing Then .Item(Col2TollTax, I).Value = ""
        '        If .Item(Col2BorderExpenses, I).Value Is Nothing Then .Item(Col2BorderExpenses, I).Value = ""
        '        If .Item(Col2DriverFooding, I).Value Is Nothing Then .Item(Col2DriverFooding, I).Value = ""
        '        If .Item(Col2HandlingExpenses, I).Value Is Nothing Then .Item(Col2HandlingExpenses, I).Value = ""
        '        If .Item(Col2FixTimeHrs, I).Value Is Nothing Then .Item(Col2FixTimeHrs, I).Value = ""
        '        If .Item(Col2FixTimeHrs_Down, I).Value Is Nothing Then .Item(Col2FixTimeHrs_Down, I).Value = ""

        '        If .Item(Col2Vehicle, I).Value <> "" Then
        '            .Item(Col2RouteExpenses, I).Value = Format(Val(.Item(Col2TollTax, I).Value) + Val(.Item(Col2BorderExpenses, I).Value) + Val(.Item(Col2DriverFooding, I).Value), "0.00")
        '        End If
        '    Next
        'End With
    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0, J As Integer = 0
        Try
            Call Calculation()

            If AgL.RequiredField(TxtUser_Name) Then Exit Function
            If AgL.RequiredField(TxtDescription) Then Exit Function
            If AgL.RequiredField(TxtAdminYn) Then Exit Function

            If AgL.StrCmp(TxtUser_Name.Text, AgLibrary.ClsConstant.PubSuperUserName) Then
                MsgBox("User Name Is Not Valid!...")
                TxtUser_Name.Focus()
                Exit Function
            End If

            If Not AgCL.AgCheckMandatory(Me) Then Exit Function
            If AgCL.AgIsBlankGrid(DGL1, Col1CompCode) Then Exit Function
            If AgCL.AgIsDuplicate(DGL1, "" & Col1CompCode & "") Then Exit Function


            If Topctrl1.Mode = "Edit" Then
                If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
                    AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                    If AgL.StrCmp(TxtChangePassword.Text, "Yes") Then
                        If TxtOldPassword.Text.Trim <> TxtOldPassword.Tag.Trim Then MsgBox("Please Reenter Old Password!...") : TxtOldPassword.Focus() : Exit Function
                    Else
                        If TxtPassword.Text.Trim <> TxtOldPassword.Tag.Trim Then MsgBox("Password is mismatching!...") : TxtPassword.Focus() : Exit Function
                    End If
                End If
            End If
            If TxtPassword.Text.Trim <> TxtVarifyPassword.Text.Trim Then MsgBox("Please Reenter Password!...") : TxtPassword.Focus() : Exit Function

            mStrModuleList = GetModuleList()

            If Topctrl1.Mode = "Add" Then
                mCode = AgL.GetMaxId("UserMast", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 13, True, True, , AgL.Gcn_ConnectionString)

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From UserMast Where User_Name='" & TxtUser_Name.Text & "' ", AgL.GcnMain)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("User Name Already Exist!") : TxtUser_Name.Focus() : Exit Function

                mSearchCode = TxtUser_Name.Text
            End If


            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Public Sub ProcFillModule()
        Dim DsTemp As DataSet = Nothing
        Dim mCondStr$ = "", mCondStr1$ = ""
        Dim bIntI As Integer = 0

        Try
            DGL2.RowCount = 1 : DGL2.Rows.Clear()

            If mStrModuleList.Trim <> "" Then
                mCondStr = " Where vUp.Module In (" & Replace(mStrModuleList, "|", "'") & ") "
                mCondStr1 = " Where vUp.Module Not In (" & Replace(mStrModuleList, "|", "'") & ") "
            Else
                mCondStr = " Where 1 = 2 "
            End If

            mQry = "Select Convert(BIT,1) AS [Select], vUp.Module As [Module] " & _
                    " From (" & StrModuleSql & ") As vUp " & mCondStr
            mQry = mQry & " UNION ALL " & _
                    " Select Convert(BIT,0) AS [Select], vUp.Module As [Module] " & _
                    " From (" & StrModuleSql & ") As vUp " & mCondStr1

            DsTemp = AgL.FillData(mQry, AgL.GCn)

            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    For bIntI = 0 To .Rows.Count - 1
                        DGL2.Rows.Add()
                        DGL2.Item(Col_SNo, bIntI).Value = DGL2.Rows.Count
                        DGL2.Item(Col2Module, bIntI).Value = AgL.XNull(.Rows(bIntI)("Module"))
                        DGL2.Item(Col2Select, bIntI).Value = IIf(AgL.VNull(.Rows(bIntI)("Select")), AgLibrary.ClsConstant.StrCheckedValue, AgLibrary.ClsConstant.StrUnCheckedValue)
                    Next
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

    Private Function GetModuleList() As String
        Dim I As Integer = 0
        Dim bStrModuleList$ = ""
        Try
            With DGL2
                For I = 0 To .Rows.Count - 1
                    If .Item(Col2Select, I).Value Is Nothing Then .Item(Col2Select, I).Value = AgLibrary.ClsConstant.StrUnCheckedValue
                    If .Item(Col2Module, I).Value Is Nothing Then .Item(Col2Module, I).Value = ""

                    If .Item(Col2Select, I).Value.ToString.Trim = AgLibrary.ClsConstant.StrCheckedValue _
                        And .Item(Col2Module, I).Value.ToString.Trim <> "" Then

                        If bStrModuleList.Trim = "" Then
                            bStrModuleList = "|" + .Item(Col2Module, I).Value.ToString + "|"
                        Else
                            bStrModuleList = bStrModuleList + ", |" + .Item(Col2Module, I).Value.ToString + "|"
                        End If

                    End If
                Next
            End With
        Catch ex As Exception
            bStrModuleList = ""
        Finally
            GetModuleList = bStrModuleList
        End Try
    End Function
End Class
