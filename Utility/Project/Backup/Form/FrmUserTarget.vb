Public Class FrmUserTarget
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""
    Private Const Col_SNo As Byte = 0
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Private Const Col1EntryPoint As Byte = 1
    Private Const Col1Add_Target As Byte = 2
    Private Const Col1Edit_Target As Byte = 3
    Private Const Col1Print_Target As Byte = 4
    Private Const Col1Sms_Target As Byte = 5
    Private Const Col1Email_Target As Byte = 6
    Private Const Col1Fax_Target As Byte = 7

    Dim Col1EntryPoint_Qry As String = ""

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
            Col1EntryPoint_Qry = "Select Distinct MnuText As Code, MnuText As Name " & _
                                    " From User_Permission " & _
                                    " Where MnuLevel>0 " & _
                                    " Order By MnuText "

            .AddAgComboColumn(AgL.GCn, DGL1, Col1EntryPoint_Qry, "DGL1EntryPoint", 190, "EntryPoint")
            .AddAgNumberColumn(DGL1, "DGL1Add_Target", 80, 4, 0, False, "Add Target", True, False, True)
            .AddAgNumberColumn(DGL1, "DGL1Edit_Target", 80, 4, 0, False, "Edit Target", True, False, True)
            .AddAgNumberColumn(DGL1, "DGL1Print_Target", 80, 4, 0, False, "Print Target", True, False, True)
            .AddAgNumberColumn(DGL1, "DGL1Sms_Target", 80, 4, 0, False, "SMS Target", False, True, True)
            .AddAgNumberColumn(DGL1, "DGL1Email_Target", 90, 4, 0, False, "EMail Target", False, True, True)
            .AddAgNumberColumn(DGL1, "DGL1Fax_Target", 80, 4, 0, False, "Fax Target", False, True, True)
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
        mQry = "Select User_Target.Code As SearchCode " & _
        " From User_Target "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        mQry = "Select USER_NAME As Code, USER_NAME As Name From UserMast " & _
            "  Order By USER_NAME"
        AgCL.IniAgHelpList(AgL.GCn, CboUserName, mQry, "Name", "Code")

    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        CboUserName.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Dim mCondStr As String = ""

        Try
            MastPos = BMBMaster.Position


            If DTMaster.Rows.Count > 0 Then
                If TxtDate_To.Text.Trim <> "" And DTMaster.Rows.Count > 1 Then
                    mCondStr = " And UserName=" & AgL.Chk_Text(CboUserName.SelectedValue) & " And Code<>'" & mSearchCode & "'"

                    mQry = "Select IsNull(Count(*),0) Cnt From User_Target " & _
                            " Where Date_From > " & AgL.ConvertDate(TxtDate_From.Text) & " " & mCondStr
                    AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GCn)

                    If AgL.ECmd.ExecuteScalar > 0 Then
                        MsgBox("Permission Denied...") : TxtDate_To.Focus() : Exit Sub
                    End If
                End If


                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.Dman_ExecuteNonQry("Delete From User_Target_Detail Where Code='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From User_Target Where Code='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)


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
        CboUserName.Focus()
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = "Select  User_Target.Code As SearchCode,  UserMast1.USER_NAME As [User Name],  Convert(nVarChar,User_Target.Date_From,3) As [Date From],  Convert(nVarChar,User_Target.Date_To,3) As [Date To]  From  User_Target  Left Join  UserMast UserMast1 On UserMast1.USER_NAME = User_Target.UserName "


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

        AgCL.RefGridHelp(AgL.GCn, DGL1, Col1EntryPoint, Col1EntryPoint_Qry)
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim I As Integer, Sr As Integer
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If Not Data_Validation() Then Exit Sub

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into User_Target (Code, UserName, Date_From, Date_To, " & _
                        " Div_Code, Site_Code, U_EntDt, PreparedBy, U_AE) Values(" & _
                        " '" & mSearchCode & "', " & AgL.Chk_Text(CboUserName.SelectedValue) & "," & _
                        " " & AgL.ConvertDate(TxtDate_From.Text) & ", " & AgL.ConvertDate(TxtDate_To.Text) & "," & _
                        " '" & AgL.PubDivCode & "', '" & AgL.PubSiteCode & "', '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "', 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Update User_Target Set UserName = " & AgL.Chk_Text(CboUserName.SelectedValue) & "," & _
                        " Date_From = " & AgL.ConvertDate(TxtDate_From.Text) & ", Date_To = " & AgL.ConvertDate(TxtDate_To.Text) & ", " & _
                        " Edit_Date='" & Format(AgL.PubLoginDate, "Short Date") & "', ModifiedBy = '" & AgL.PubUserName & "', U_AE = 'E' " & _
                        " Where Code='" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            mQry = "Delete From User_Target_Detail Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Sr = 0
            With DGL1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1EntryPoint, I).Value IsNot Nothing Then
                        If .Item(Col1EntryPoint, I).Value <> "" And _
                            (Val(.Item(Col1Add_Target, I).Value) <> 0 Or Val(.Item(Col1Edit_Target, I).Value) <> 0 Or Val(.Item(Col1Print_Target, I).Value) <> 0 Or _
                            Val(.Item(Col1Sms_Target, I).Value) <> 0 Or Val(.Item(Col1Fax_Target, I).Value) <> 0 Or Val(.Item(Col1Email_Target, I).Value) <> 0) Then

                            Sr = Sr + 1
                            mQry = "Insert Into User_Target_Detail ( Code, Sr, EntryPoint, Add_Target, Edit_Target, Print_Target," & _
                                    " Sms_Target, Email_Target, Fax_Target) Values(" & _
                                    " '" & mSearchCode & "', " & Sr & "," & AgL.Chk_Text(.Item(Col1EntryPoint, I).Value) & ", " & _
                                    " " & Val(.Item(Col1Add_Target, I).Value) & ", " & Val(.Item(Col1Edit_Target, I).Value) & ", " & _
                                    " " & Val(.Item(Col1Print_Target, I).Value) & ", " & Val(.Item(Col1Sms_Target, I).Value) & ", " & _
                                    " " & Val(.Item(Col1Email_Target, I).Value) & ", " & Val(.Item(Col1Fax_Target, I).Value) & " " & _
                                    " ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If
                    End If
                Next I
            End With


            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)


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
                mQry = "Select User_Target.* " & _
                    " From User_Target Where Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        CboUserName.SelectedValue = AgL.XNull(.Rows(0)("UserName"))
                        TxtDate_From.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Date_From")))
                        TxtDate_To.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Date_To")))
                    End If
                End With
                mQry = "Select User_Target_Detail.* " & _
                    " From User_Target_Detail " & _
                    " Where Code='" & mSearchCode & "'" & _
                    " Order By Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL1.RowCount = 1
                    DGL1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL1.Rows.Add()
                            DGL1.Item(Col_SNo, I).Value = .Rows(I)("Sr")
                            DGL1.Item(Col1EntryPoint, I).Value = .Rows(I)("EntryPoint")
                            DGL1.Item(Col1Add_Target, I).Value = Format(AgL.VNull(.Rows(I)("Add_Target")), "0.".PadRight(0 + 2, "0"))
                            DGL1.Item(Col1Edit_Target, I).Value = Format(AgL.VNull(.Rows(I)("Edit_Target")), "0.".PadRight(0 + 2, "0"))
                            DGL1.Item(Col1Print_Target, I).Value = Format(AgL.VNull(.Rows(I)("Print_Target")), "0.".PadRight(0 + 2, "0"))
                            DGL1.Item(Col1Sms_Target, I).Value = Format(AgL.VNull(.Rows(I)("Sms_Target")), "0.".PadRight(0 + 2, "0"))
                            DGL1.Item(Col1Email_Target, I).Value = Format(AgL.VNull(.Rows(I)("Email_Target")), "0.".PadRight(0 + 2, "0"))
                            DGL1.Item(Col1Fax_Target, I).Value = Format(AgL.VNull(.Rows(I)("Fax_Target")), "0.".PadRight(0 + 2, "0"))
                        Next I
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
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub
    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
    End Sub

    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGL1.EditingControlShowing
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            If TypeOf e.Control Is ComboBox Then e.Control.Text = "" : CType(e.Control, ComboBox).SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
        sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub


    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DGL1.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try

        AgL.FSetSNo(sender, Col_SNo)
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
        Dim I As Integer, J As Integer, mPrevRowNo As Integer = 0
        Dim mCondStr As String = ""
        Dim mDataExists As Boolean = False

        If AgL.RequiredField(CboUserName) Then Exit Function
        If AgL.RequiredField(TxtDate_From) Then Exit Function
        If TxtDate_To.Text.Trim <> "" Then
            If CDate(TxtDate_From.Text) >= CDate(TxtDate_To.Text) Then
                MsgBox("Date To Can't be Less Than From Date From") : TxtDate_To.Focus() : Exit Function
            End If
        End If

        mCondStr = " And UserName=" & AgL.Chk_Text(CboUserName.SelectedValue) & ""
        mCondStr = mCondStr & IIf(Topctrl1.Mode = "Add", "", " And Code<>'" & mSearchCode & "'")


        mQry = "Select IsNull(Count(*),0) Cnt From User_Target " & _
                " Where Date_To Is Null " & mCondStr
        AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GCn)
        If AgL.ECmd.ExecuteScalar > 0 Then
            MsgBox("User Target is Already Active!..." & vbCrLf & "First Fill The Date To in That Plan.") : TxtDate_From.Focus() : Exit Function
        End If


        If TxtDate_To.Text.Trim <> "" Then
            mCondStr = mCondStr & " And (" & AgL.ConvertDate(TxtDate_From.Text) & " Between Date_From And Date_To Or " & AgL.ConvertDate(TxtDate_To.Text) & " Between Date_From And Date_To) "
        Else
            mCondStr = mCondStr & " And " & AgL.ConvertDate(TxtDate_From.Text) & " Between Date_From And Date_To "
        End If
        mQry = "Select IsNull(Count(*),0) Cnt From User_Target " & _
                " Where 1=1 " & mCondStr
        AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GCn)
        If AgL.ECmd.ExecuteScalar > 0 Then
            MsgBox("User Target Already Exists For Selected Period!...") : TxtDate_From.Focus() : Exit Function
        End If


        With DGL1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1EntryPoint, I).Value IsNot Nothing Then
                    If .Item(Col1EntryPoint, I).Value <> "" Then
                        If .Item(Col1Add_Target, I).Value Is Nothing Then .Item(Col1Add_Target, I).Value = ""
                        If .Item(Col1Edit_Target, I).Value Is Nothing Then .Item(Col1Edit_Target, I).Value = ""
                        If .Item(Col1Print_Target, I).Value Is Nothing Then .Item(Col1Print_Target, I).Value = ""
                        If .Item(Col1Sms_Target, I).Value Is Nothing Then .Item(Col1Sms_Target, I).Value = ""
                        If .Item(Col1Fax_Target, I).Value Is Nothing Then .Item(Col1Fax_Target, I).Value = ""
                        If .Item(Col1Email_Target, I).Value Is Nothing Then .Item(Col1Email_Target, I).Value = ""

                        For J = I + 1 To .Rows.Count - 1
                            If .Item(Col1EntryPoint, J).Value IsNot Nothing Then
                                If .Item(Col1EntryPoint, I).Value = .Item(Col1EntryPoint, J).Value Then
                                    MsgBox("Duplicate ""Entry Point"" Row No. " & Val(.Item(Col_SNo, J).Value) & "") : DGL1.Focus() : Exit Function
                                End If
                            End If
                        Next

                        If mDataExists = False And (Val(.Item(Col1Add_Target, I).Value) <> 0 Or Val(.Item(Col1Edit_Target, I).Value) <> 0 Or Val(.Item(Col1Print_Target, I).Value) <> 0 Or _
                            Val(.Item(Col1Sms_Target, I).Value) <> 0 Or Val(.Item(Col1Fax_Target, I).Value) <> 0 Or Val(.Item(Col1Email_Target, I).Value) <> 0) Then
                            mDataExists = True
                        End If
                    End If
                End If
            Next I
            If mDataExists = False Then MsgBox("Target Detail Grid Can't be Blank!...") : .CurrentCell = DGL1(Col1Add_Target, 0) : .Focus() : Exit Function Else mDataExists = False
        End With


        If Topctrl1.Mode = "Add" Then
            mSearchCode = AgL.GetMaxId("User_Target", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, , AgL.Gcn_ConnectionString)
        End If

        Data_Validation = True
    End Function
End Class
