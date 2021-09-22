Imports System.Data.SQLite
Public Class FrmUser
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mCode As String = ""

    Private Const Col_SNo As Byte = 0
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Private Const Col1CompCode As Byte = 1
    Private Const Col1SelectSite As Byte = 2
    Private Const Col1SiteList As Byte = 3
    Private Const Col1SelectDivision As Byte = 4
    Private Const Col1DivisionList As Byte = 5

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
        DGL1.Height = Pnl1.Height
        DGL1.Width = Pnl1.Width
        DGL1.Top = Pnl1.Top
        DGL1.Left = Pnl1.Left
        Pnl1.Visible = False
        Controls.Add(DGL1)
        DGL1.Visible = True
        DGL1.BringToFront()
        With AgCL
            .AddAgTextColumn(DGL1, "DGL1SNo", 40, 5, "S.No.", True, True, False)
            .AddAgTextColumn(DGL1, "DGL1CompCode", 300, 6, "Company Name", True, False, False, True)
            .AddAgButtonColumn(DGL1, "DGL1SelectSite", 30, " ", True, False, , , , , 10, "1")
            .AddAgTextColumn(DGL1, "DGL1SiteList", 100, 6, "Site List", True, True, False, True)
            .AddAgButtonColumn(DGL1, "DGL1SelectDivision", 30, " ", True, False, , , , , 10, "1")
            .AddAgTextColumn(DGL1, "DGL1DivisionList", 100, 6, "Division List", True, True, False, True)
        End With
        DGL1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(DGL1, Col_SNo)
        DGL1.TabIndex = Pnl1.TabIndex
        DGL1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        DGL1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)

    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If


        If Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And
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
        Dim CondStr As String = " Where 1 = 1 "

        If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
            AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

            CondStr += " And User_Name = '" & AgL.PubUserName & "' "
        End If

        mQry = "Select User_Name As SearchCode " &
        " From UserMast " & CondStr

        Topctrl1.FIniForm(DTMaster, AgL.GcnMain, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Sub Ini_List()
        mQry = "Select User_Name As Code, User_Name As Name From UserMast " &
            "  Order By User_Name"
        TxtUser_Name.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnMain)

        mQry = "Select Distinct Description As Code, Description As Name From UserMast " &
            "  Where IfNull(Description,'')<>'' Order By Description"
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnMain)

        mQry = "SELECT C.Comp_Code AS Code, C.Comp_Name + ' (' + C.cyear + ')' AS Name " &
                " FROM Company C " &
                " WHERE IfNull(C.DeletedYN,'N')<>'Y' " &
                " ORDER BY C.Comp_Name, C.Start_Dt Desc "
        DGL1.AgHelpDataSet(Col1CompCode) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtUser_Name.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SQLiteCommand
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

            If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
                AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                CondStr += " And User_Name = '" & AgL.PubUserName & "' "
            End If


            AgL.PubFindQry = "Select  User_Name As SearchCode, User_Name As [User Name], Description," &
                                " Case IfNull(Admin,'N') When 'Y' Then 'Yes' Else 'No' End As Administrator " &
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
                mQry = "Insert Into UserMast (Code, USER_NAME, PASSWD, Description, Admin, UpLoadDate) Values(" &
                        " '" & mCode & "', '" & mSearchCode & "', " & AgL.Chk_Text(AgL.CODIFY(TxtPassword.Text)) & "," &
                        " " & AgL.Chk_Text(TxtDescription.Text) & ",'" & AgL.MidStr(TxtAdminYn.Text, 0, 1) & "', " & AgL.ConvertDate(mUploadDate) & ") "

                If AgL.PubManageOfflineData Then AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite, AgL.ECmdSite)
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
            Else
                mQry = "Update UserMast Set Description=" & AgL.Chk_Text(TxtDescription.Text) & ", PASSWD = " & AgL.Chk_Text(AgL.CODIFY(TxtPassword.Text)) & "," &
                        " Admin='" & AgL.MidStr(TxtAdminYn.Text, 0, 1) & "', UpLoadDate = " & AgL.ConvertDate(mUploadDate) & " " &
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
                        mQry = "INSERT INTO UserSite ( User_Name, CompCode, Sitelist, UpLoadDate , DivisionList) VALUES  (" &
                                " '" & mSearchCode & "', " & AgL.Chk_Text(.AgSelectedValue(Col1CompCode, I)) & "," &
                                " " & AgL.Chk_Text(.Item(Col1SiteList, I).Value) & ", " &
                                " " & AgL.ConvertDate(mUploadDate) & ",  " &
                                " " & AgL.Chk_Text(.Item(Col1DivisionList, I).Value) & ")"

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
                mQry = "Select Um.* " &
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
                            DGL1.Item(Col1DivisionList, I).Value = AgL.XNull(.Rows(I)("DivisionList"))
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
        mSearchCode = "" : mCode = ""
        TxtChangePassword.Text = "No"
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If Topctrl1.Mode = "Edit" Then
            TxtUser_Name.Enabled = False

            If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
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
                        If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
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
           TxtUser_Name.Validating, TxtAdminYn.Validating, TxtChangePassword.Validating, TxtOldPassword.Validating,
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
        Dim FrmObj As Utility.FrmSiteList
        Dim FrmObjDivision As FrmDivisionList
        Dim mSiteList$ = "", mDivisionList$ = ""
        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1SelectSite
                    If DGL1.Item(Col1SiteList, mRowIndex).Value Is Nothing Then DGL1.Item(Col1SiteList, mRowIndex).Value = ""
                    mSiteList = DGL1.Item(Col1SiteList, mRowIndex).Value
                    FrmObj = New Utility.FrmSiteList : FrmObj.SiteListStr = mSiteList : FrmObj.ShowDialog()
                    mSiteList = FrmObj.SiteListStr : FrmObj = Nothing
                    DGL1.Item(Col1SiteList, mRowIndex).Value = mSiteList

                Case Col1SelectDivision
                    If DGL1.Item(Col1DivisionList, mRowIndex).Value Is Nothing Then DGL1.Item(Col1DivisionList, mRowIndex).Value = ""
                    mDivisionList = DGL1.Item(Col1DivisionList, mRowIndex).Value
                    FrmObjDivision = New FrmDivisionList : FrmObjDivision.DivisionListStr = mDivisionList : FrmObjDivision.ShowDialog()
                    mDivisionList = FrmObjDivision.DivisionListStr : FrmObjDivision = Nothing
                    DGL1.Item(Col1DivisionList, mRowIndex).Value = mDivisionList

            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
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
                If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
                    AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                    If AgL.StrCmp(TxtChangePassword.Text, "Yes") Then
                        If TxtOldPassword.Text.Trim <> TxtOldPassword.Tag.Trim Then MsgBox("Please Reenter Old Password!...") : TxtOldPassword.Focus() : Exit Function
                    Else
                        If TxtPassword.Text.Trim <> TxtOldPassword.Tag.Trim Then MsgBox("Password is mismatching!...") : TxtPassword.Focus() : Exit Function
                    End If
                End If
            End If
            If TxtPassword.Text.Trim <> TxtVarifyPassword.Text.Trim Then MsgBox("Please Reenter Password!...") : TxtPassword.Focus() : Exit Function

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


End Class
