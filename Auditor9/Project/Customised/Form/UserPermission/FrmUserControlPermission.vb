Imports System.Data.SQLite

Public Class FrmUserControlPermission

    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mModuleName As String = ""
    Public WithEvents DGL1 As New AgControls.AgDataGrid


    Private Const Col1_MnuName As Byte = 0
    Private Const Col1_ControlName As Byte = 1
    Private Const Col1_Permission As Byte = 2
    Private Const Col1_MnuModule As Byte = 3
    Private Const Col1_MnuText As Byte = 4
    Private Const Col1_GroupName As Byte = 5
    Private Const Col1_GroupText As Byte = 6

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
        AgL = AgLibVar
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
        Me.Controls.Add(DGL1)
        DGL1.Visible = True
        DGL1.ColumnHeadersHeight = 50
        DGL1.BringToFront()
        DGL1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
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
            'AgL.WinSetting(Me, 600, 880, 0, 0)
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
        Dim CondStr As String = ""

        mQry = "Select User_Name As SearchCode " & _
            " From UserMast " & CondStr

        Topctrl1.FIniForm(DTMaster, AgL.GcnMain, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Sub Ini_List()
        mQry = "Select U.User_Name As Code, U.User_Name as Name, U.Description, " &
                " Case IfNull(U.Admin,'N') When 'Y' Then 'Yes' Else 'No' End As Administrator " &
                " From UserMast U " &
                " Where 1=1 " &
                " Order By U.User_Name "
        TxtUserName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GcnMain)

        'mQry = "Select User_Name As Code, User_Name As Name From UserMast " & _
        '    "  Order By User_Name"
        'AgCL.IniAgHelpList(AgL.GcnMain, CboUserName, mQry, "Name", "Code")
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

                    mQry = "Delete From User_Control_Permission Where UserName='" & mSearchCode & "'  "
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

            mQry = "Select User_Name As SearchCode " &
                    " From UserMast "

            AgL.PubFindQry = "Select U.User_Name As SearchCode, U.User_Name as [User Name], U.Description, " &
                                " Case IfNull(U.Admin,'N') When 'Y' Then 'Yes' Else 'No' End As Administrator " &
                                " From UserMast U "


            AgL.PubFindQryOrdBy = "SearchCode"



            '*************** Common Code Start *****************
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
            '*************** Common Code End  *****************
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
            If Not Data_Validation() Then Exit Sub

            AgL.ECmd = AgL.GcnMain.CreateCommand
            AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            With DGL1
                For I = 0 To .Rows.Count - 1
                    If AgL.XNull(.Item(Col1_MnuName, I).Value).ToString.Trim <> "" Then

                        mQry = "Delete From User_Control_Permission " &
                                " Where UserName='" & mSearchCode & "' And " &
                                "       MnuModule=" & AgL.Chk_Text(.Item(Col1_MnuModule, I).Value) & " And " &
                                "       MnuName=" & AgL.Chk_Text(.Item(Col1_MnuName, I).Value) & " And " &
                                "       GroupText=" & AgL.Chk_Text(.Item(Col1_ControlName, I).Value) & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)

                        If AgL.XNull(.Item(Col1_GroupText, I).Value).ToString.Trim <> "" Then
                            mQry = "Insert Into User_Control_Permission (UserName, MnuModule, MnuName, MnuText, GroupText, GroupName) Values(" &
                                    " '" & mSearchCode & "'," & AgL.Chk_Text(.Item(Col1_MnuModule, I).Value) & "," & AgL.Chk_Text(.Item(Col1_MnuName, I).Value) & "," &
                                    " " & AgL.Chk_Text(.Item(Col1_MnuText, I).Value) & ", " & AgL.Chk_Text(.Item(Col1_GroupText, I).Value) & ", " &
                                    " " & AgL.Chk_Text(.Item(Col1_GroupName, I).Value) & ")"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
                        End If
                    End If
                Next I
            End With
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
                User_Control_Permission(mSearchCode)

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

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Try
            Select Case sender.currentcell.columnindex
                'Case Col1Installment_No
                '    If DGL1.Item(Col1Policy_No, DGL1.CurrentCell.RowIndex).Value <> "" And DGL1.Item(Col1TransactionType, DGL1.CurrentCell.RowIndex).Value <> "" Then
                '        If AgL.StrCmp(DGL1.Item(Col1TransactionType, DGL1.CurrentCell.RowIndex).Value, "Post") Then
                '            mQry = "Select Convert(VarChar,Installment_No) As Code, Convert(nVarChar,Installment_Date,3) As [Installment Date], Convert(VarChar,Installment_No) As [Installment No] " & _
                '                   "From Case_Installment " & _
                '                   "Where CaseDocID  = " & AgL.Chk_Text(DGL1.Item(Col1Policy_No, DGL1.CurrentCell.RowIndex).Tag) & " " & _
                '                   "And (PremiumPostingDocId Is Null Or PremiumPostingDocID = '" & mSearchCode & "') Order By Installment_No"
                '            DGL1.AgHelpDataSet(Col1Installment_No, 0, TabControl1.Top, TabControl1.Left) = AgL.FillData(mQry, AgL.GcnMain)
                '        End If
                '    Else
                '        DGL1.AgHelpDataSet(sender.currentcell.columnindex) = Nothing
                '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Select Case DGL1.CurrentCell.ColumnIndex
            'Case Col1Installment_No
            '    If DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).Tag <> "" Then
            '        If AgL.StrCmp(DGL1.Item(Col1TransactionType, DGL1.CurrentCell.RowIndex).Value, "Post") Then
            '            DGL1.Item(Col1CommissionAmt_InsComp, DGL1.CurrentCell.RowIndex).Value = DGL1.Item(Col1CommissionAmt, DGL1.CurrentCell.RowIndex).Value
            '        Else
            '            If Me.Controls("HelpDg") IsNot Nothing Then
            '                With CType(Me.Controls("HelpDg"), AgControls.AgDataGrid)
            '                    DGL1.Item(Col1CommissionAmt, DGL1.CurrentCell.RowIndex).Value = .Item("CommissionAmt", .CurrentCell.RowIndex).Value
            '                    DGL1.Item(Col1CommissionAmt_InsComp, DGL1.CurrentCell.RowIndex).Value = .Item("CommissionAmt_InsComp", .CurrentCell.RowIndex).Value
            '                    DGL1.Item(Col1DocIdCancelled, DGL1.CurrentCell.RowIndex).Value = .Item("DocId", .CurrentCell.RowIndex).Value
            '                End With
            '            End If

            '        End If
            '        DGL1.Item(Col1PremiumDueDate_InsComp, DGL1.CurrentCell.RowIndex).Value = DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).Value
            '    End If
        End Select
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
        'sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub


    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DGL1.RowsRemoved
        'Try
        '    DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        'Catch ex As Exception
        'End Try

        'AgL.FSetSNo(sender, Col_SNo)
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
                        mPermission = IIf(.Item(Col1_Permission, I).Value = True, .Item(Col1_ControlName, I).Value, "")
                    Else
                        mPermission = ""
                    End If
                    .Item(Col1_GroupText, I).Value = mPermission
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

    Private Sub User_Control_Permission(ByVal mUser As String)
        Dim mQry As String
        Dim DTblModule As DataTable, DTbl As DataTable
        Dim I As Integer, J As Integer
        Dim mNode As New TreeNode, tNode As New TreeNode
        Dim bStrModuleList$ = ""

        Try
            bStrModuleList = AgL.FunGetUserModuleList(mUser)

            'mQry = "SELECT Distinct U.MnuModule " & _
            '        " FROM User_Control_Permission U" & _
            '        " Where IfNull(U.MnuModule,'')<>'' And U.UserName='" & mUser & "' " & _
            '        " " & IIf(bStrModuleList.Trim <> "", " AND U.MnuModule IN (" & bStrModuleList.Replace("|", "'") & ") ", "") & " " & _
            '        " ORDER BY U.MnuModule"

            mQry = "SELECT Distinct U.MnuModule " &
                    " FROM User_Control_Permission U" &
                    " Where IfNull(U.MnuModule,'')<>'' And U.UserName='SA' " &
                    " " & IIf(bStrModuleList.Trim <> "", " AND U.MnuModule IN (" & bStrModuleList.Replace("|", "'") & ") ", "") & " " &
                    " ORDER BY U.MnuModule"
            DTblModule = AgL.FillData(mQry, AgL.GcnMain).Tables(0)



            TreeView1.Nodes.Clear()
            If DTblModule.Rows.Count > 0 Then
                For J = 0 To DTblModule.Rows.Count - 1

                    TreeView1.Nodes.Add(New TreeNode(DTblModule.Rows(J)("MnuModule")))
                    mModuleName = DTblModule.Rows(J)("MnuModule")
                    mNode = TreeView1.Nodes(J)

                    'mQry = "SELECT Distinct U.MnuName,U.MnuText " & _
                    '        " FROM User_Control_Permission U" & _
                    '        " Where IfNull(U.MnuModule,'')='" & DTblModule.Rows(J)("MnuModule") & "' And U.UserName='" & mUser & "' " & _
                    '        " ORDER BY U.MnuName"
                    mQry = "SELECT Distinct U.MnuName,U.MnuText " &
                            " FROM User_Control_Permission U" &
                            " Where IfNull(U.MnuModule,'')='" & DTblModule.Rows(J)("MnuModule") & "' " &
                            " And U.UserName='SA' " &
                            " ORDER BY U.MnuText "
                    DTbl = AgL.FillData(mQry, AgL.GcnMain).Tables(0)

                    If DTbl.Rows.Count > 0 Then
                        With DTbl
                            For I = 0 To .Rows.Count - 1
                                mNode.Nodes.Add(New TreeNode(.Rows(I)("MnuText")))
                                tNode = mNode.Nodes(I)

                                tNode.ToolTipText = mModuleName
                                tNode.Tag = .Rows(I)("MnuName")
                            Next
                        End With
                    End If
                    mNode.Expand()
                Next
            End If
        Catch ex As Exception
            DGL1.DataSource = Nothing
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Try
            If sender.SelectedNode IsNot Nothing Then
                Fill_Grid(sender.SelectedNode.ToolTipText, sender.SelectedNode.tag)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Fill_Grid(ByVal mMnuModule As String, ByVal mParent As String)
        Dim mQry As String, bStrModuleList$ = "", mSubQry$ = ""
        Dim DTbl As DataTable
        Try
            bStrModuleList = AgL.FunGetUserModuleList(CboUserName.Text)

            'mQry = "SELECT U.MnuName As [Menu Name], U.GroupText As [Control Name], " & _
            '        " CONVERT(BIT,CASE WHEN Len(IfNull(U.GroupText,'')) > 0 THEN 1 ELSE 0 END) AS [Permission], " & _
            '        " U.MnuModule, U.MnuText, U.GroupName, U.GroupText " & _
            '        " FROM User_Control_Permission U " & _
            '        " WHERE U.UserName='" & CboUserName.Text & "' And U.MnuModule ='" & mMnuModule & "' And" & _
            '        " U.MnuName ='" & mParent & "' " & _
            '        " " & IIf(bStrModuleList.Trim <> "", " AND U.MnuModule IN (" & bStrModuleList.Replace("|", "'") & ") ", "") & " " & _
            '        " ORDER BY U.GroupText"

            mSubQry = "SELECT '" & CboUserName.Text & "' AS UserName, U1.MnuName, U1.MnuModule, U1.MnuText, U1.GroupName, U1.GroupText " &
                        " FROM User_Control_Permission U1 " &
                        " WHERE U1.UserName='SA' "

            mQry = "SELECT U.MnuName As [Menu Name], U.GroupText As [Control Name], " &
                    " CONVERT(BIT,CASE WHEN Len(IfNull(U2.GroupText,'')) > 0 THEN 1 ELSE 0 END) AS [Permission], " &
                    " U.MnuModule, U.MnuText, U.GroupName, U.GroupText " &
                    " FROM (" & mSubQry & ") As U " &
                    " Left Join User_Control_Permission U2 On U.UserName = U2.UserName And U.MnuName = U2.MnuName And U.MnuModule = U2.MnuModule And U.GroupName = U2.GroupName And U.GroupText = U2.GroupText " &
                    " WHERE U.UserName='" & CboUserName.Text & "' And U.MnuModule ='" & mMnuModule & "' And" &
                    " U.MnuName ='" & mParent & "' " &
                    " " & IIf(bStrModuleList.Trim <> "", " AND U.MnuModule IN (" & bStrModuleList.Replace("|", "'") & ") ", "") & " " &
                    " ORDER BY U.GroupText"
            DTbl = AgL.FillData(mQry, AgL.GcnMain).tables(0)


            With DGL1
                .DataSource = DTbl
                .Columns(Col1_MnuName).Visible = False
                .Columns(Col1_ControlName).Width = 200
                .Columns(Col1_Permission).Width = 50
                .Columns(Col1_ControlName).ReadOnly = False
                .Columns(Col1_MnuModule).Visible = False
                .Columns(Col1_MnuText).Visible = False
                .Columns(Col1_GroupName).Visible = False
                .Columns(Col1_GroupText).Visible = False
            End With


        Catch ex As Exception
            DGL1.DataSource = Nothing
        End Try
    End Sub


    Private Sub CmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCopy.Click
        Dim mTrans As Boolean = False
        Try
            DGL1.DataSource = Nothing
            If DTMaster.Rows.Count > 0 Then
                If AgL.RequiredField(CboUserName) Then Exit Sub
                If Not AgL.StrCmp(AgL.PubUserName, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "Login User Is Not System Administrator!")
                If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
                    AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                    Err.Raise(1, , "Permission Denied!..." & vbCrLf & "Login User Is Not System Administrator!")
                End If

                If AgL.StrCmp(mSearchCode, CboUserName.Text) Then Err.Raise(1, , "Copy From User is Same as Current User!...")

                If MsgBox("Are You Sure to copy User Control Permission From """ & CboUserName.Text & """?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                AgL.ECmd = AgL.GcnMain.CreateCommand
                AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = True

                mQry = "Delete From User_Control_Permission Where UserName='" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)

                mQry = "Insert Into User_Control_Permission (UserName, MnuModule, MnuName, MnuText, GroupText, GroupName) " & _
                        " (Select '" & mSearchCode & "', MnuModule, MnuName, MnuText, GroupText, GroupName " & _
                        " From User_Control_Permission Where UserName='" & CboUserName.Text & "')"
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

    Private Sub CmdRevoke_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRevoke.Click, CmdAdd.Click
        Try
            Select Case sender.name
                Case CmdRevoke.Name
                    Assign_Permission(False, Col1_Permission)
                Case CmdAdd.Name
                    Assign_Permission(True, sender.tag)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Assign_Permission(ByVal mPermission As Boolean, ByVal mCol_Index As Integer)
        Dim I As Integer
        Try
            If AgL.StrCmp(mSearchCode, "SA") Then Exit Sub
            With DGL1
                For I = 0 To .Rows.Count - 1
                    If AgL.XNull(.Item(Col1_MnuName, I).Value).ToString.Trim <> "" Then
                        .Item(mCol_Index, I).Value = mPermission
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
End Class
