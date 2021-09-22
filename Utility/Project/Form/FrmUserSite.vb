Imports System.Data.SQLite
Public Class FrmUserSite
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mCode As String = ""

    Private Const StrModuleSql As String = "SELECT MnuModule AS Module FROM User_Permission WHERE UserName = 'SA' GROUP BY MnuModule"


    Private Const Col_SNo As Byte = 0
    'Dgl1 Column Index Constants
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Private Const Col1CompCode As Byte = 1
    Private Const Col1SelectSite As Byte = 2
    Private Const Col1SiteList As Byte = 3


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
        Try
            With AgCL
                .AddAgTextColumn(DGL1, "DGL1SNo", 40, 5, "S.No.", True, True, False)
                .AddAgTextColumn(DGL1, "DGL1CompCode", 310, 6, "Company Name", True, False, False, True)
                .AddAgButtonColumn(DGL1, "DGL1SelectSite", 30, " ", True, False, , , , , 10, "1")
                .AddAgTextColumn(DGL1, "DGL1SiteList", 110, 6, "Site List", True, True, False, True)
            End With
            AgL.AddAgDataGrid(DGL1, Pnl1)
            DGL1.ColumnHeadersHeight = 40
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            Topctrl1.ChangeAgGridState(DGL1, False)
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
                " WHERE C.Div_Code = '" & AgL.PubDivCode & "' AND IfNull(C.DeletedYN,'N')<>'Y' " &
                " ORDER BY C.Comp_Name, C.Start_Dt Desc "
        DGL1.AgHelpDataSet(Col1CompCode) = AgL.FillData(mQry, AgL.ECompConn)
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


                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AgL.ECompConn.CreateCommand
                    AgL.ETrans = AgL.ECompConn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.Dman_ExecuteNonQry("Delete From UserSite Where User_Name='" & mSearchCode & "'", AgL.ECompConn, AgL.ECmd)

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

        Try
            MastPos = BMBMaster.Position

            If Not Data_Validation() Then Exit Sub


            AgL.ECmd = AgL.ECompConn.CreateCommand
            AgL.ETrans = AgL.ECompConn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans

            mTrans = True

            If Topctrl1.Mode = "Edit" Then
                mQry = "Delete From UserSite Where User_Name = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.ECompConn, AgL.ECmd)
            End If

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1CompCode, I).Value <> "" Then
                        mQry = "INSERT INTO UserSite ( User_Name, CompCode, Sitelist) VALUES  (" &
                                " '" & mSearchCode & "', " & AgL.Chk_Text(.AgSelectedValue(Col1CompCode, I)) & "," &
                                " " & AgL.Chk_Text(.Item(Col1SiteList, I).Value) & " " &
                                " )"

                        If AgL.PubManageOfflineData Then AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite, AgL.ECmdSite)
                        AgL.Dman_ExecuteNonQry(mQry, AgL.ECompConn, AgL.ECmd)
                    End If
                Next I
            End With

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
                DsTemp = AgL.FillData(mQry, AgL.ECompConn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        mCode = AgL.XNull(.Rows(0)("Code"))
                        TxtUser_Name.AgSelectedValue = AgL.XNull(.Rows(0)("User_Name"))
                        TxtDescription.AgSelectedValue = AgL.XNull(.Rows(0)("Description"))
                        TxtAdminYn.Text = IIf(AgL.XNull(.Rows(0)("Admin")).ToString.ToUpper = "Y", "Yes", "No")
                    End If
                End With

                DsTemp = Nothing

                mQry = "Select Us.* From UserSite Us Where Us.User_Name = '" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.ECompConn)
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
            Topctrl1.tAdd = False : Topctrl1.tPrn = False
        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = "" : mCode = ""

        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        TxtUser_Name.Enabled = False : TxtAdminYn.Enabled = False : TxtDescription.Enabled = False
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
           TxtUser_Name.Enter, TxtAdminYn.Enter
        Try
            Select Case sender.name
                'Case <sender>.Name
                '<Executable Code>
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
           TxtUser_Name.Validating, TxtAdminYn.Validating
        Try
            Select Case sender.NAME
                'Case <sender>.Name
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


            If Topctrl1.Mode = "Add" Then
                mSearchCode = TxtUser_Name.Text
            End If


            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function
End Class
