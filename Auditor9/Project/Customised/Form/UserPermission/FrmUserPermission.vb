Imports System.Data.SQLite

Public Class FrmUserPermission

    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mModuleName As String = ""
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Dim DtModules As DataTable



    Private Const Col1_ParentText As String = "Parent Name"
    Private Const Col1_MnuName As String = "Menu Name"
    Private Const Col1_MnuText As String = "Menu Description"
    Private Const Col1_Add As String = "Add"
    Private Const Col1_Edit As String = "Edit"
    Private Const Col1_Delete As String = "Delete"
    Private Const Col1_Print As String = "Print"
    Private Const Col1_SNo As String = "S.No."
    Private Const Col1_MnuLevel As String = "Menu Level"
    Private Const Col1_MnuModule As String = "Module"
    Private Const Col1_Parent As String = "Parent"
    Private Const Col1_Permission As String = "Permission"
    Private Const Col1_ReportFor As String = "Report For"
    Private Const Col1_Active As String = "Active"
    Private Const Col1_MainStreamCode As String = "Main Stream Code"

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
        'DGL1.Height = Pnl1.Height
        'DGL1.Width = Pnl1.Width
        'DGL1.Top = Pnl1.Top
        'DGL1.Left = Pnl1.Left
        'Pnl1.Visible = False
        'Me.Controls.Add(DGL1)
        'DGL1.Visible = True
        'DGL1.ColumnHeadersHeight = 50
        'DGL1.BringToFront()
        'DGL1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        'DGL1.TabIndex = Pnl1.TabIndex
        'DGL1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        'DGL1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)


        DGL1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGL1, Col1_MnuName, 40, 0, Col1_MnuName, False, False, False)
            .AddAgTextColumn(DGL1, Col1_MnuModule, 120, 0, Col1_MnuModule, True, True, False)
            .AddAgTextColumn(DGL1, Col1_ParentText, 120, 0, Col1_ParentText, True, True, False)
            .AddAgTextColumn(DGL1, Col1_MnuText, 250, 0, Col1_MnuText, True, True, False)
            .AddAgTextColumn(DGL1, Col1_Add, 50, 0, Col1_Add, True, True, False)
            .AddAgTextColumn(DGL1, Col1_Edit, 50, 0, Col1_Edit, True, True, False)
            .AddAgTextColumn(DGL1, Col1_Delete, 50, 0, Col1_Delete, True, True, False)
            .AddAgTextColumn(DGL1, Col1_Print, 50, 0, Col1_Print, True, True, False)
            .AddAgTextColumn(DGL1, Col1_MnuLevel, 200, 0, Col1_MnuLevel, False, True, False)
            .AddAgTextColumn(DGL1, Col1_SNo, 200, 0, Col1_SNo, False, True, False)
            .AddAgTextColumn(DGL1, Col1_Parent, 200, 0, Col1_Parent, False, True, False)
            .AddAgTextColumn(DGL1, Col1_Permission, 200, 0, Col1_Permission, False, True, False)
            .AddAgTextColumn(DGL1, Col1_ReportFor, 200, 0, Col1_ReportFor, False, True, False)
            .AddAgTextColumn(DGL1, Col1_Active, 200, 0, Col1_Active, False, True, False)
            .AddAgTextColumn(DGL1, Col1_MainStreamCode, 200, 0, Col1_MainStreamCode, False, True, False)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.EnableHeadersVisualStyles = False
        DGL1.AgSkipReadOnlyColumns = True
        DGL1.RowHeadersVisible = False
        AgL.GridDesign(DGL1)
        DGL1.MultiSelect = True

        DGL1.Columns(Col1_Add).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)
        DGL1.Columns(Col1_Edit).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)
        DGL1.Columns(Col1_Delete).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)
        DGL1.Columns(Col1_Print).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)


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
            ''AgL.WinSetting(Me, 600, 880, 0, 0)
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

        If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
            AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

            CondStr += " And User_Name = '" & AgL.PubUserName & "' "
            CondStr += " And User_Name <> 'Super' "
        End If
        mQry = "Select User_Name As SearchCode " & _
            " From UserMast " & CondStr

        Topctrl1.FIniForm(DTMaster, AgL.GcnMain, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        Dim CondStr As String = ""

        mQry = "Select U.User_Name As Code, U.User_Name as Name, U.Description, " &
                " Case IfNull(U.Admin,'N') When 'Y' Then 'Yes' Else 'No' End As Administrator " &
                " From UserMast U " &
                " Where 1=1 " &
                " Order By U.User_Name "
        TxtUserName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GcnMain)




        If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
            AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

            CondStr = " And User_Name = '" & AgL.PubUserName & "' "
        Else
            CondStr = ""
        End If
        mQry = "Select User_Name As Code, User_Name As Name From UserMast " &
                "  Where 1 = 1 " & CondStr & " Order By User_Name "
        AgCL.IniAgHelpList(AgL.GcnMain, CboUserName, mQry, "Name", "Code")
    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtUserName.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim MastPos As Long
        Dim mTrans As Boolean = False


        Try
            MastPos = BMBMaster.Position


            If DTMaster.Rows.Count > 0 Then
                If Not AgL.StrCmp(AgL.PubUserName, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "Login User Is Not System Administrator!")
                If AgL.StrCmp(mSearchCode, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "User Is System Administrator!")

                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
                    AgL.ECmd = AgL.GcnMain.CreateCommand
                    AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    mQry = "Delete From User_Permission Where UserName='" & mSearchCode & "'  "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)

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
        DGL1.Focus()
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            Dim CondStr As String = " Where 1 = 1 "

            If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
                AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                CondStr += " And U.User_Name = '" & AgL.PubUserName & "' "
            End If

            AgL.PubFindQry = "Select U.User_Name As SearchCode, U.User_Name as [User Name], U.Description, " &
                                " Case IfNull(U.Admin,'N') When 'Y' Then 'Yes' Else 'No' End As Administrator " &
                                " From UserMast U " & CondStr


            AgL.PubFindQryOrdBy = "SearchCode"



            '*************** common code start *****************
            Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, Me.Text & " Find", AgL)
            Frmbj.ShowDialog()
            AgL.PubSearchRow = Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value.ToString
            If AgL.PubSearchRow <> "" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                MoveRec()
            End If


            'AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            'AgL.PubObjFrmFind.ShowDialog()
            'AgL.PubObjFrmFind = Nothing
            'If AgL.PubSearchRow <> "" Then
            '    AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
            '    BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
            '    MoveRec()
            'End If
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
        Dim I As Integer
        Dim mTrans As Boolean = False

        Try
            MastPos = BMBMaster.Position
            DtModules.DefaultView.RowFilter = Nothing

            If Not Data_Validation() Then Exit Sub



            AgL.ECmd = AgL.GcnMain.CreateCommand
            AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            mQry = "Delete From User_Permission " &
                    " Where UserName='" & mSearchCode & "'  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If AgL.XNull(.Item(Col1_MnuName, I).Value).ToString.Trim <> "" Then


                        mQry = "Insert Into User_Permission (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent," &
                                "  Permission, ReportFor, Active, MainStreamCode, RowID) Values(" &
                                " '" & mSearchCode & "'," & AgL.Chk_Text(.Item(Col1_MnuModule, I).Value) & "," & AgL.Chk_Text(.Item(Col1_MnuName, I).Value) & "," &
                                " " & AgL.Chk_Text(.Item(Col1_MnuText, I).Value) & "," & Val(.Item(Col1_SNo, I).Value) & "," &
                                " " & AgL.Chk_Text(.Item(Col1_MnuLevel, I).Value) & "," & AgL.Chk_Text(.Item(Col1_Parent, I).Value) & "," &
                                " " & AgL.Chk_Text(.Item(Col1_Permission, I).Value) & "," & AgL.Chk_Text(.Item(Col1_ReportFor, I).Value) & "," &
                                " " & AgL.Chk_Text(.Item(Col1_Active, I).Value) & ", " & AgL.Chk_Text(.Item(Col1_MainStreamCode, I).Value) & ",1)"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
                    End If
                Next I
            End With


            mQry = "Insert Into User_Permission (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, MainStreamCode,GroupLevel,RowID) "
            mQry = mQry & "SELECT Distinct '" & mSearchCode & "',MnuModule,MnuName,MnuText, " &
                "SNo,MnuLevel,Parent,Permission,ReportFor, " &
                "Active, Up.MainStreamCode, GroupLevel, RowID " &
                "FROM User_Permission UP , " &
                "(Select MainStreamCode From User_Permission  Where UserName = '" & mSearchCode & "' And Permission <>'****') As CUP " &
                "Where UserName='SA' And SUBSTR(CUP.MainStreamCode,1,Length(UP.MainStreamCode))= Up.MainStreamCode  And CUP.MainStreamCode <> UP.MainStreamCode"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)


            AgL.ETrans.Commit()
            mTrans = False

            Call AgL.LogTableEntry(mSearchCode, Me.Text, "E", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn)

            FIniMaster(0, 1)
            Topctrl1_tbRef()
            Topctrl1.SetDisp(True)
            MoveRec()
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        Finally
            'Finally Code
        End Try
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
                TxtUserName.AgSelectedValue = mSearchCode
                CboUserName.Enabled = IIf(mSearchCode.Trim.ToUpper = "SA", False, True)
                CboUserName.SelectedValue = mSearchCode
                User_Permission(mSearchCode)
                Fill_Grid(TxtUserName.Text)
            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
            Topctrl1.tAdd = False : Topctrl1.tEdit = False : Topctrl1.tDel = False : Topctrl1.tPrn = False
        End Try
    End Sub
    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = ""
        TreeView1.Nodes.Clear() : DGL1.DataSource = Nothing
    End Sub
    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
    End Sub

    Private Sub DGL1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEndEdit
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim bBlnPermission As Boolean = False
        Try

            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            Try
                If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""
            Catch ex As Exception

            End Try

            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1_Add, Col1_Edit, Col1_Delete
                    If AgL.XNull(DGL1.Item(Col1_MnuName, mRowIndex).Value).ToString.Trim <> "" Then
                        bBlnPermission = DGL1.Item(mColumnIndex, mRowIndex).Value
                        If mColumnIndex <> Col1_Print And AgL.XNull(DGL1.Item(Col1_ReportFor, mRowIndex).Value).ToString.Trim <> "" Then
                            bBlnPermission = False
                        End If
                        DGL1.Item(mColumnIndex, mRowIndex).Value = bBlnPermission
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        If Topctrl1.Mode = "Browse" Then Exit Sub
        '<Executable Code>
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        '<Executable Code>
    End Sub

    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGL1.EditingControlShowing
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If TypeOf e.Control Is ComboBox Then
            e.Control.Text = ""
        End If
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If e.Control And e.KeyCode = Keys.D Then sender.CurrentRow.Selected = True
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        If e.KeyCode = Keys.Delete Then DGL1.Item(sender.CurrentCell.ColumnIndex, sender.CurrentCell.rowindex).value = ""

        Try
            Select Case DGL1.CurrentCell.ColumnIndex
                'Case <Dgl_Column>
                '    <Executable Code>
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
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
        Handles TxtUserName.Validating

        Dim DsTemp As DataSet
        Try
            Select Case sender.NAME
                Case TxtUserName.Name
                    ''
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub



    Private Function Data_Validation() As Boolean
        Dim I As Integer, mCount As Integer
        Dim mPermission As String = ""

        Try
            If AgL.RequiredField(TxtUserName) Then Exit Function
            If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
                AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                Err.Raise(1, , "Permission Denied!..." & vbCrLf & "Login User Is Not System Administrator!")
            End If

            If AgL.StrCmp(mSearchCode, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "User Is System Administrator!")
            mCount = 0
            With DGL1
                For I = 0 To .Rows.Count - 1
                    mPermission = ""
                    If AgL.XNull(.Item(Col1_MnuName, I).Value).ToString.Trim <> "" Then
                        mPermission = mPermission & IIf(.Item(Col1_Add, I).Value = "þ", AgL.MidStr(.Columns(Col1_Add).Name, 0, 1), "*")
                        mPermission = mPermission & IIf(.Item(Col1_Edit, I).Value = "þ", AgL.MidStr(.Columns(Col1_Edit).Name, 0, 1), "*")
                        mPermission = mPermission & IIf(.Item(Col1_Delete, I).Value = "þ", AgL.MidStr(.Columns(Col1_Delete).Name, 0, 1), "*")
                        mPermission = mPermission & IIf(.Item(Col1_Print, I).Value = "þ", AgL.MidStr(.Columns(Col1_Print).Name, 0, 1), "*")

                    Else
                        mPermission = "****"
                    End If
                    .Item(Col1_Permission, I).Value = mPermission
                    mCount = mCount + 1
                Next
            End With
            If mCount = 0 Then Err.Raise(1, , "Grid Can't be Blank")
            Data_Validation = True
        Catch ex As Exception
            Data_Validation = False
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub User_Permission(ByVal mUser As String)
        Dim mQry As String
        Dim DTblModule As DataTable, DTbl As DataTable = Nothing
        Dim I As Integer = 0, J As Integer
        Dim mNode As New TreeNode, tNode As New TreeNode
        Dim bStrModuleList$ = ""
        Try
            'bStrModuleList = AgL.XNull(AgL.Dman_Execute("SELECT IfNull(U.ModuleList,'') As ModuleList FROM UserMast U WHERE U.USER_NAME = '" & mUser & "' And IfNull(U.ModuleList,'') <> '' ", AgL.GcnMain).ExecuteScalar)
            bStrModuleList = AgL.FunGetUserModuleList(mUser)

            mQry = "SELECT Distinct U.MnuModule, U.MainStreamCode " &
                    " FROM User_Permission U " &
                    " Where IfNull(U.MnuModule,'')<>'' And U.UserName='SA' And Parent='' And IfNull(U.Active,'Y') = 'Y'  " &
                    " " & IIf(bStrModuleList.Trim <> "", " AND U.MnuModule IN (" & bStrModuleList.Replace("|", "'") & ") ", "") & " " &
                    " ORDER BY U.MnuModule"

            DTblModule = AgL.FillData(mQry, AgL.GcnMain).Tables(0)



            TreeView1.Nodes.Clear()
            If DTblModule.Rows.Count > 0 Then
                For J = 0 To DTblModule.Rows.Count - 1

                    TreeView1.Nodes.Add(New TreeNode(DTblModule.Rows(J)("MnuModule")))
                    mModuleName = DTblModule.Rows(J)("MnuModule")

                    mNode = TreeView1.Nodes(J)
                    mNode.Tag = DTblModule.Rows(J)("MainStreamCode")
                Next
            End If
        Catch ex As Exception
            DGL1.DataSource = Nothing
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PopulateTreeView(ByVal inParentID As String, ByRef inTreeNode As TreeNode, ByVal mUser As String)
        Dim mQry As String = ""
        Dim DSNASPSearch As DataSet

        mQry = "SELECT U.MnuName,U.MnuText, " &
                " IfNull(U.Parent,'') AS Parent " &
                " FROM User_Permission U" &
                " Where IfNull(U.Parent,'')='" & inParentID & "' And U.UserName='SA' And " &
                " IfNull(U.MnuModule,'') = '" & inTreeNode.ToolTipText & "'  And IfNull(U.Active,'N') = 'Y' " &
                " ORDER BY U.SNo ,U.MnuLevel , U.Parent,U.MnuName"

        DSNASPSearch = AgL.FillData(mQry, AgL.GcnMain)
        Dim parentrow As DataRow
        Dim ParentTable As DataTable
        ParentTable = DSNASPSearch.Tables(0)
        If ParentTable.Rows.Count > 0 Then
            For Each parentrow In ParentTable.Rows
                Dim parentnode As TreeNode
                'we'll provide some text for the tree node.
                Dim strLabel As String = parentrow.Item("MnuText")
                parentnode = New TreeNode(strLabel)
                inTreeNode.Nodes.Add(parentnode)
                'set the tag property for the current node. This comes in useful if 
                'you want to pass the value of a specific record id.
                'since the tag value is not visible, in the TreeView1_AfterSelect event 
                'you could pass the value to another sub routine, for example:
                'FillDataGrid(TreeView1.SelectedNode.Tag)
                parentnode.ToolTipText = mModuleName
                parentnode.Tag = parentrow.Item("MnuName")
                'call the routine again to find childern of this record.
                PopulateTreeView(parentrow.Item("MnuName"), parentnode, mUser)
            Next parentrow
        End If
    End Sub



    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Try
            If sender.SelectedNode IsNot Nothing Then
                DtModules.DefaultView.RowFilter = Nothing
                DtModules.DefaultView.RowFilter = " SUBSTRING(MainStreamCode,1, " & Len(sender.SelectedNode.tag) & ")= '" & sender.SelectedNode.tag & "' "

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Fill_Grid(ByVal UserName As String)
        Dim mQry As String
        Dim mSubQry As String
        Dim mSubQryParents As String
        Dim bStrModuleList$ = ""
        Dim bIntI% = 0
        Dim I As Integer

        Try

            'bStrModuleList = AgL.XNull(AgL.Dman_Execute("SELECT IfNull(U.ModuleList,'') As ModuleList FROM UserMast U WHERE U.USER_NAME = '" & UserName & "' And IfNull(U.ModuleList,'') <> '' ", AgL.GcnMain).ExecuteScalar)
            bStrModuleList = AgL.FunGetUserModuleList(UserName)

            mSubQry = "SELECT MnuName, MnuText, MainStreamCode, MnuLevel " &
                      "FROM User_Permission  " &
                      "WHERE MnuName IN (SELECT DISTINCT Parent FROM User_Permission WHERE UserName  = 'sa' And IfNull(Active,'Y') = 'Y' ) " &
                      "AND UserName ='SA' AND Parent <>'' And IfNull(Active,'Y') = 'Y' "

            'mSubQryParents = "SELECT MnuName " & _
            '               " FROM User_Permission " & _
            '               " WHERE MnuName Not IN (SELECT DISTINCT IfNull(Parent,'') FROM User_Permission Where UserName = 'SA' And IfNull(Active,'Y') = 'Y' ) " & _
            '               " AND UserName ='SA' And IfNull(Active,'Y') = 'Y' "

            mSubQryParents = "SELECT MnuName " &
                           " FROM User_Permission " &
                           " WHERE IfNull(IsParent,0)=0 " &
                           " AND UserName ='SA' And IfNull(Active,'Y') = 'Y' "



            'mQry = "SELECT (Select Top 1 MnuText from User_Permission Where MnuName=V.Parent And UserName='SA') as [Parent Name],  V.MnuName As [Menu Name], V.MnuText As [Menu Description], " & _
            '                    " CONVERT(BIT,CASE SUBSTRING(IfNull(U.Permission,''),1,1) WHEN 'A' THEN 1 ELSE 0 END) AS [Add], " & _
            '                    " CONVERT(BIT,CASE SUBSTRING(IfNull(U.Permission,''),2,1) WHEN 'E' THEN 1 ELSE 0 END) AS [Edit], " & _
            '                    " CONVERT(BIT,CASE SUBSTRING(IfNull(U.Permission,''),3,1) WHEN 'D' THEN 1 ELSE 0 END) AS [Delete], " & _
            '                    " CONVERT(BIT,CASE SUBSTRING(IfNull(U.Permission,''),4,1) WHEN 'P' THEN 1 ELSE 0 END) AS [Print], " & _
            '                    " V.SNo ,V.MnuLevel, V.MnuModule,V.Parent, U.Permission, V.ReportFor, V.Active, V.MainStreamCode " & _
            '                    " FROM (SELECT MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, ReportFor, Active, MainStreamCode FROM User_Permission U " & _
            '                    "       WHERE U.UserName ='SA' And IfNull(U.Active,'Y') = 'Y' And MnuName In (" & mSubQryParents & ") " & _
            '                    "       " & IIf(bStrModuleList.Trim <> "", " AND U.MnuModule IN (" & bStrModuleList.Replace("|", "'") & ") ", "") & " " & _
            '                    " ) As V " & _
            '                    " Left Join User_Permission U On U.MnuModule = V.MnuModule And U.MnuName = V.MnuName And U.UserName = '" & UserName & "' " & _
            '                    " Where IfNull(U.Active,'Y') = 'Y' " & _
            '                    " ORDER BY V.MainStreamCode "


            mQry = "SELECT (Select  MnuText from User_Permission Where MnuName=V.Parent And UserName='SA') as [Parent Name],  V.MnuName As [Menu Name], V.MnuText As [Menu Description], " &
                                " CASE SUBSTR(IfNull(U.Permission,''),1,1) WHEN 'A' THEN '1' ELSE '' END AS [Add], " &
                                " CASE SUBSTR(IfNull(U.Permission,''),2,1) WHEN 'E' THEN '1' ELSE '' END AS [Edit], " &
                                " CASE SUBSTR(IfNull(U.Permission,''),3,1) WHEN 'D' THEN '1' ELSE '' END AS [Delete], " &
                                " CASE SUBSTR(IfNull(U.Permission,''),4,1) WHEN 'P' THEN '1' ELSE '' END AS [Print], " &
                                " V.SNo ,V.MnuLevel, V.MnuModule,V.Parent, U.Permission, V.ReportFor, V.Active, V.MainStreamCode " &
                                " FROM (SELECT MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, ReportFor, Active, MainStreamCode FROM User_Permission U " &
                                "       WHERE U.UserName ='SA' And IfNull(U.Active,'Y') = 'Y' And IfNull(IsParent,0)=0 " &
                                "       " & IIf(bStrModuleList.Trim <> "", " AND U.MnuModule IN (" & bStrModuleList.Replace("|", "'") & ") ", "") & " " &
                                " ) As V " &
                                " Left Join User_Permission U On U.MnuModule = V.MnuModule And U.MnuName = V.MnuName And U.UserName = '" & UserName & "' " &
                                " Where IfNull(U.Active,'Y') = 'Y' " &
                                " ORDER BY V.MainStreamCode "

            DtModules = AgL.FillData(mQry, AgL.GcnMain).tables(0)
            Dim mCol(1) As DataColumn

            mCol(0) = DtModules.Columns("MainStreamCode")
            'DtModules.PrimaryKey = mCol
            DGL1.RowCount = 1
            DGL1.Rows.Clear()
            For I = 0 To DtModules.Rows.Count - 1
                DGL1.Rows.Add("")
                DGL1.Item(Col1_MnuName, I).Value = AgL.XNull(DtModules.Rows(I)("Menu Name"))
                DGL1.Item(Col1_ParentText, I).Value = AgL.XNull(DtModules.Rows(I)("Parent Name"))
                DGL1.Item(Col1_MnuText, I).Value = AgL.XNull(DtModules.Rows(I)("Menu Description"))
                DGL1.Item(Col1_Add, I).Value = AgL.XNull(DtModules.Rows(I)("Add"))
                DGL1.Item(Col1_Edit, I).Value = AgL.XNull(DtModules.Rows(I)("Edit"))
                DGL1.Item(Col1_Delete, I).Value = AgL.XNull(DtModules.Rows(I)("Delete"))
                DGL1.Item(Col1_Print, I).Value = AgL.XNull(DtModules.Rows(I)("Print"))
                DGL1.Item(Col1_MnuLevel, I).Value = AgL.XNull(DtModules.Rows(I)("MnuLevel"))
                DGL1.Item(Col1_MnuModule, I).Value = AgL.XNull(DtModules.Rows(I)("MnuModule"))
                DGL1.Item(Col1_SNo, I).Value = AgL.XNull(DtModules.Rows(I)("SNo"))
                DGL1.Item(Col1_Parent, I).Value = AgL.XNull(DtModules.Rows(I)("Parent"))
                DGL1.Item(Col1_ReportFor, I).Value = AgL.XNull(DtModules.Rows(I)("ReportFor"))
                DGL1.Item(Col1_Active, I).Value = AgL.XNull(DtModules.Rows(I)("Active"))
                DGL1.Item(Col1_MainStreamCode, I).Value = AgL.XNull(DtModules.Rows(I)("MainStreamCode"))



                If UCase(Trim(DGL1.Item(Col1_Add, I).Value)) = "1" Then
                    DGL1.Item(Col1_Add, I).Value = "þ"
                Else
                    DGL1(Col1_Add, I).Value = "o"
                End If
                If UCase(Trim(DGL1.Item(Col1_Edit, I).Value)) = "1" Then
                    DGL1.Item(Col1_Edit, I).Value = "þ"
                Else
                    DGL1(Col1_Edit, I).Value = "o"
                End If
                If UCase(Trim(DGL1.Item(Col1_Delete, I).Value)) = "1" Then
                    DGL1.Item(Col1_Delete, I).Value = "þ"
                Else
                    DGL1(Col1_Delete, I).Value = "o"
                End If
                If UCase(Trim(DGL1.Item(Col1_Print, I).Value)) = "1" Then
                    DGL1.Item(Col1_Print, I).Value = "þ"
                Else
                    DGL1(Col1_Print, I).Value = "o"
                End If

            Next

            'With DGL1
            '    .DataSource = DtModules

            '    .Columns(Col1_MnuName).Visible = False
            '    .Columns(Col1_MnuText).Width = 200
            '    .Columns(Col1_Add).Width = 40
            '    .Columns(Col1_Edit).Width = 40
            '    .Columns(Col1_Delete).Width = 45
            '    .Columns(Col1_Print).Width = 40
            '    .Columns(Col1_ParentText).ReadOnly = True
            '    .Columns(Col1_MnuText).ReadOnly = True
            '    .Columns(Col1_MnuModule).Visible = False
            '    .Columns(Col1_SNo).Visible = False
            '    .Columns(Col1_MnuLevel).Visible = False
            '    .Columns(Col1_Parent).Visible = False
            '    .Columns(Col1_Permission).Visible = False
            '    .Columns(Col1_ReportFor).Visible = False
            '    .Columns(Col1_Active).Visible = False
            '    .Columns(Col1_MainStreamCode).Visible = False

            '    For bIntI = 0 To .Rows.Count - 1
            '        If AgL.XNull(.Item(Col1_MnuName, bIntI).Value).ToString.Trim <> "" Then
            '            If AgL.XNull(.Item(Col1_ReportFor, bIntI).Value).ToString.Trim <> "" Then
            '                DGL1.Item(Col1_Add, bIntI).ReadOnly = True
            '                DGL1.Item(Col1_Edit, bIntI).ReadOnly = True
            '                DGL1.Item(Col1_Delete, bIntI).ReadOnly = True
            '            End If
            '        End If
            '    Next

            'End With


        Catch ex As Exception
            DGL1.DataSource = Nothing
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FManageTick(columnIndex As String)
        Dim I As Integer

        If DGL1.CurrentCell.RowIndex < 0 Then Exit Sub
        Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
            Case Col1_Add, Col1_Edit, Col1_Delete, Col1_Print
                For I = 0 To DGL1.SelectedCells.Count - 1
                    FTick(columnIndex, DGL1.SelectedCells.Item(I).RowIndex)
                Next
        End Select
    End Sub
    Private Sub FTick(ByVal IntColIndex As Integer, ByVal IntRowIndex As Integer)
        If IntRowIndex < 0 Then Exit Sub
        'If IntColIndex <> 0 Then Exit Sub
        Select Case DGL1.Columns(IntColIndex).Name
            Case Col1_Add, Col1_Edit, Col1_Delete, Col1_Print
                If DGL1(Col1_MnuText, IntRowIndex).Value <> "" Then
                    If DGL1(IntColIndex, IntRowIndex).Value = "þ" Then
                        DGL1(IntColIndex, IntRowIndex).Value = "o"
                    Else
                        DGL1(IntColIndex, IntRowIndex).Value = "þ"
                    End If
                Else
                    DGL1(IntColIndex, IntRowIndex).Value = ""
                End If
        End Select
    End Sub


    Private Sub CmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCopy.Click
        Dim mTrans As Boolean = False
        Try
            DGL1.DataSource = Nothing
            If DTMaster.Rows.Count > 0 Then
                If AgL.RequiredField(CboUserName) Then Exit Sub
                If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
                    AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                    Err.Raise(1, , "Permission Denied!..." & vbCrLf & "Login User Is Not System Administrator!")
                End If

                If AgL.StrCmp(mSearchCode, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "User Is System Administrator!")
                If AgL.StrCmp(mSearchCode, CboUserName.Text) Then Err.Raise(1, , "Copy From User is Same as Current User!...")

                If MsgBox("Are You Sure to copy User Permission From """ & CboUserName.Text & """?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                AgL.ECmd = AgL.GcnMain.CreateCommand
                AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = True

                mQry = "Delete From User_Permission Where UserName='" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)

                mQry = "Insert Into User_Permission (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active,RowID) " &
                        " Select '" & mSearchCode & "', MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active,RowID " &
                        " From User_Permission Where UserName='" & CboUserName.Text & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
                AgL.ETrans.Commit()

                mTrans = False

                Call AgL.LogTableEntry(mSearchCode, Me.Text, "A", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn)

                FIniMaster(0, 1)
                Topctrl1_tbRef()
                Topctrl1.SetDisp(True)
                MoveRec()
            End If
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CmdRevoke_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRevoke.Click, CmdAdd.Click, CmdEdit.Click, CmdDel.Click, CmdPrint.Click, CmdApprove.Click
        Try
            Select Case sender.name
                Case CmdRevoke.Name
                    Assign_Permission(False, Col1_Add)
                    Assign_Permission(False, Col1_Edit)
                    Assign_Permission(False, Col1_Delete)
                    Assign_Permission(False, Col1_Print)                    
                Case CmdAdd.Name
                    Assign_Permission(True, Col1_Add)
                Case CmdEdit.Name
                    Assign_Permission(True, Col1_Edit)
                Case CmdDel.Name
                    Assign_Permission(True, Col1_Delete)
                Case CmdPrint.Name
                    Assign_Permission(True, Col1_Print)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Assign_Permission(ByVal mPermission As Boolean, ByVal mColIndex As String)
        Dim I As Integer
        Dim bBlnPermission As Boolean = False
        Try
            If AgL.StrCmp(mSearchCode, "SA") Then Exit Sub

            Dim mCol_Index As Integer
            mCol_Index = DGL1.Columns(mColIndex).Index

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If AgL.XNull(.Item(Col1_MnuName, I).Value).ToString.Trim <> "" Then
                        bBlnPermission = mPermission
                        If mColIndex <> Col1_Print And AgL.XNull(.Item(Col1_ReportFor, I).Value).ToString.Trim <> "" Then
                            bBlnPermission = False
                        End If

                        If mPermission Then
                            DGL1(mCol_Index, I).Value = "þ"
                        Else
                            DGL1(mCol_Index, I).Value = "o"
                        End If

                        '.Item(mCol_Index, I).Value = bBlnPermission
                    End If
                Next
            End With
        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Topctrl1_tbSave()
    End Sub

    Private Sub DGL1_MouseUp(sender As Object, e As MouseEventArgs) Handles DGL1.MouseUp
        Try
            If e.Button = Windows.Forms.MouseButtons.Left Then FManageTick(DGL1.CurrentCell.ColumnIndex)

        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
End Class
