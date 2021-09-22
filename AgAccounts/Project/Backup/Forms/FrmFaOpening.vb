Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmFaOpening

    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable

    Dim mQry As String = "", mSearchCode As String = ""

    Private Const Col_SNo As Byte = 0
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Private Const Col1_Date As Byte = 1
    Private Const Col1_SubCode As Byte = 2
    Private Const Col1_AmtDr As Byte = 3
    Private Const Col1_AmtCr As Byte = 4
    Private Const Col1_GroupCode As Byte = 5
    Private Const Col1_GroupNature As Byte = 6

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
        DGL1.ColumnHeadersHeight = 25
        DGL1.RowHeadersVisible = False
        Pnl1.Visible = False
        Controls.Add(DGL1)
        DGL1.Visible = True
        DGL1.BringToFront()
        With AgCL
            .AddAgTextColumn(DGL1, "DGL1SNo", 40, 5, "S.No.", True, True, False)
            .AddAgDateColumn(DGL1, "Dgl1Date", 100, "Date", False, True, False)
            .AddAgTextColumn(DGL1, "Dgl1SubCode", 420, 0, "A/C Name", True, False, False)
            .AddAgNumberColumn(DGL1, "Dgl1AmtDr", 100, 10, 2, False, "Amount (Dr)", True, False, True)
            .AddAgNumberColumn(DGL1, "Dgl1AmtCr", 100, 10, 2, False, "Amount (Cr)", True, False, True)
            .AddAgTextColumn(DGL1, "Dgl1GroupCode", 100, 0, "GroupCode", False, False, False)
            .AddAgTextColumn(DGL1, "Dgl1GroupNature", 100, 0, "Group Nature", False, False, False)
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
            AgL.WinSetting(Me, 650, 880, 0, 0)
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

        mQry = "Select Lm.Docid As SearchCode " & _
                " From LedgerM Lm " & _
                " Left Join Voucher_Type Vt On Lm.V_Type = Vt.V_Type " & _
                " Where Vt.NCat = " & AgL.Chk_Text(AgLibrary.ClsConstant.NCat_FaOpening) & " And " & AgL.PubSiteCondition("Lm.Site_Code", AgL.PubSiteCode) & " "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        '' Initialization of Help Grid
        mQry = "Select Code As Code, Name As Name From SiteMast " & _
              " Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & "  Order By Name"
        TxtSite_Code.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select V_Type As Code, Description As Name, NCat From Voucher_Type " & _
              " Where NCat = " & AgL.Chk_Text(AgLibrary.ClsConstant.NCat_FaOpening) & "" & _
              " Order By Description"
        TxtV_Type.AgHelpDataSet(1) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Sg.SubCode, Sg.Name, Sg.GroupCode, Sg.GroupNature " & _
                " FROM SubGroup Sg " & _
                " Where 1=1 And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Sg.Site_Code", AgL.PubSiteCode, "Sg.CommonAc") & ""

        DGL1.AgHelpDataSet(Col1_SubCode, 2) = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText(True)

        TxtSite_Code.AgSelectedValue = AgL.PubSiteCode
        TxtV_Date.Text = Format(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate)), AgLibrary.ClsConstant.DateFormat_ShortDate)

        If TxtV_Type.AgHelpDataSet.Tables(0).Rows.Count = 1 Then
            TxtV_Type.AgSelectedValue = TxtV_Type.AgHelpDataSet.Tables(0).Rows(0)("Code")
            LblV_Type.Tag = AgL.XNull(TxtV_Type.AgHelpDataSet.Tables(0).Rows(0)("NCat"))
            TxtV_Type.Enabled = False
        Else
            TxtV_Type.Enabled = True
        End If

        If TxtV_Type.Enabled Then TxtV_Type.Focus() Else TxtV_Date.Focus()
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

                    AgL.LedgerUnPost(AgL.GCn, AgL.ECmd, mSearchCode)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

                    AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
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
        DispText(True)
        TxtV_Date.Focus()
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = "Select Lm.DocId As SearchCode, Convert(varchar,Lm.V_Date,3) As [Voucher Date], " & AgL.V_No_Field("Lm.DocId") & " As [Voucher No.], S.Name As [Site Name] " & _
                                    " From LedgerM Lm " & _
                                    " Left Join Voucher_Type Vt On Lm.V_Type = Vt.V_Type " & _
                                    " Left Join SiteMast S On Lm.Site_Code = S.Code " & _
                                    " Where Vt.NCat = " & AgL.Chk_Text(AgLibrary.ClsConstant.NCat_FaOpening) & " And " & AgL.PubSiteCondition("Lm.Site_Code", AgL.PubSiteCode) & " "

            AgL.PubFindQryOrdBy = "SearchCode"



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
        Dim I As Integer, mSr As Integer = 0
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If Not Data_Validation() Then Exit Sub

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO dbo.LedgerM	( " & _
                     " DocId, V_No, V_Type, V_Prefix, V_Date, Narration, " & _
                     " PreparedBY, PostedBY, Site_Code, U_Name, U_EntDt, U_AE) " & _
                     " VALUES (" & _
                     " '" & mSearchCode & "', " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & _
                     " " & AgL.Chk_Text(LblPrefix.Text.ToString) & ", " & AgL.ConvertDate(TxtV_Date.Text) & ", " & AgL.Chk_Text(TxtNarration.Text) & ", " & _
                     " '" & AgL.PubUserName & "', '" & AgL.PubUserName & "', " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ", " & _
                     " '" & AgL.PubUserName & "','" & AgL.PubLoginDate & "','A')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Update LedgerM " & _
                        " SET " & _
                        " V_Date = " & AgL.ConvertDate(TxtV_Date.Text) & ", " & _
                        " Narration = " & AgL.Chk_Text(TxtNarration.Text) & ", " & _
                        " PreparedBY = '" & AgL.PubUserName & "', " & _
                        " PostedBY = '" & AgL.PubUserName & "', " & _
                        " U_Name = '" & AgL.PubUserName & "', " & _
                        " U_EntDt = '" & AgL.PubLoginDate & "', " & _
                        " U_AE = 'E' " & _
                        " WHERE DocId = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If Topctrl1.Mode = "Edit" Then
                mQry = "Delete From Ledger Where DocId = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mSr = 0
            With DGL1
                For I = 0 To .RowCount - 1
                    If .Item(Col1_SubCode, I).Value.ToString.Trim <> "" And _
                        AgL.XNull(.Item(Col1_Date, I).Value) <> "" And _
                        (Val(.Item(Col1_AmtDr, I).Value) > 0 Or Val(.Item(Col1_AmtCr, I).Value) > 0) Then

                        mSr = mSr + 1
                        mQry = "INSERT INTO dbo.Ledger(DocId, V_SNO, V_Type, V_No, v_Prefix, DivCode, Site_Code, V_Date, " & _
                                " SubCode, AmtDr, AmtCr, GroupCode, GroupNature, U_Name, U_EntDt, U_AE) " & _
                               " VALUES(" & _
                               " '" & mSearchCode & "'," & mSr & "," & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & Val(TxtV_No.Text) & ", " & _
                               " " & AgL.Chk_Text(LblPrefix.Text.ToString) & ", '" & AgL.PubDivCode & "', " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ", " & _
                               " " & AgL.ConvertDate(.Item(Col1_Date, I).Value.ToString) & "," & AgL.Chk_Text(.AgSelectedValue(Col1_SubCode, I)) & ", " & _
                               " " & Val(.Item(Col1_AmtDr, I).Value) & "," & Val(.Item(Col1_AmtCr, I).Value) & ", " & AgL.Chk_Text(.Item(Col1_GroupCode, I).Value) & ", " & _
                               " " & AgL.Chk_Text(.Item(Col1_GroupNature, I).Value) & ",'" & AgL.PubUserName & "'," & AgL.ConvertDate(AgL.PubLoginDate) & ", " & _
                               " '" & Topctrl1.Mode.Substring(0, 1) & "')"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next
            End With

            AgL.UpdateVoucherCounter(mSearchCode, CDate(TxtV_Date.Text), AgL.GCn, AgL.ECmd, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd, , TxtV_Date.Text, , , , TxtSite_Code.AgSelectedValue, AgL.PubDivCode)

            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
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

    Private Sub DGL1_EditingControl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DGL1.EditingControl_KeyPress
        Select Case DGL1.CurrentCell.ColumnIndex
            Case Col1_AmtDr
                If Val(DGL1.Item(Col1_AmtCr, DGL1.CurrentCell.RowIndex).Value) <> 0 Then e.Handled = True
            Case Col1_AmtCr
                If Val(DGL1.Item(Col1_AmtDr, DGL1.CurrentCell.RowIndex).Value) <> 0 Then e.Handled = True
        End Select

    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing, TblTemp As DataTable = Nothing
        Dim MastPos As Long
        Dim I As Integer
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")

                mQry = "Select Lm.*, Vt.NCat " & _
                        " From LedgerM Lm " & _
                        " Left Join Voucher_Type Vt On Lm.V_Type = Vt.V_Type " & _
                        " Where DocId = '" & mSearchCode & "' "
                DsTemp = AgL.FillData(mQry, AgL.GCn)

                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtDocId.Text = mSearchCode
                        TxtSite_Code.AgSelectedValue = AgL.XNull(.Rows(0)("Site_Code"))
                        TxtV_Type.AgSelectedValue = AgL.XNull(.Rows(0)("V_Type"))
                        TxtV_Date.Text = Format(AgL.XNull(.Rows(0)("V_Date")), AgLibrary.ClsConstant.DateFormat_ShortDate)
                        LblPrefix.Text = AgL.XNull(.Rows(0)("V_Prefix"))
                        TxtV_No.Text = Format(AgL.VNull(.Rows(0)("V_No")), "0.".PadRight(+2, "0"))
                        LblV_Type.Tag = AgL.XNull(.Rows(0)("NCat"))
                        TxtNarration.Text = AgL.XNull(.Rows(0)("Narration"))

                    End If
                End With

                mQry = "Select L.*, S.Name, S.GroupCode, S.GroupNature " & _
                        " From Ledger L " & _
                        " Left Join SubGroup S On L.SubCode = S.SubCode " & _
                        " Where DocId = '" & mSearchCode & "' " & _
                        " Order By S.Name "
                TblTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                With TblTemp
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            DGL1.Rows.Add()
                            DGL1.Item(Col_SNo, I).Value = DGL1.Rows.Count - 1
                            DGL1.Item(Col1_Date, I).Value = AgL.XNull(.Rows(I)("V_Date"))
                            DGL1.AgSelectedValue(Col1_SubCode, I) = AgL.XNull(.Rows(I)("SubCode"))
                            DGL1.Item(Col1_AmtDr, I).Value = Format(AgL.VNull(.Rows(I)("AmtDr")), "0.00")
                            DGL1.Item(Col1_AmtCr, I).Value = Format(AgL.VNull(.Rows(I)("AmtCr")), "0.00")
                            DGL1.Item(Col1_GroupCode, I).Value = AgL.XNull(.Rows(I)("GroupCode"))
                            DGL1.Item(Col1_GroupNature, I).Value = AgL.XNull(.Rows(I)("Groupnature"))
                        Next I
                    End If
                End With


            Else
                BlankText()
            End If

            Topctrl1.FSetDispRec(BMBMaster)

            If mSearchCode.Trim <> "" Then
                Topctrl1.tAdd = False
            Else
                If InStr(Topctrl1.Tag, "A") > 0 Then Topctrl1.tAdd = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
            TblTemp = Nothing
            Topctrl1.tPrn = False
        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = "" : LblPrefix.Text = ""
        TxtV_Date.Text = ""
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        '<Executable Code>
        TxtSite_Code.Enabled = False : TxtV_No.Enabled = False

        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
            TxtV_Type.Enabled = False
        End If
    End Sub

    Private Sub DGL1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellValidated
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Try
            Select Case sender.CurrentCell.ColumnIndex
                'Case Col1Rank_Code
                '    DGL1.Item(Col1Rank_Code, DGL1.CurrentCell.RowIndex).Value = DGL1.Item(Col1Rank_Code, DGL1.CurrentCell.RowIndex).Value
            End Select


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1_SubCode
                    If DGL1.Item(mColumnIndex, mRowIndex).Value.ToString.Trim = "" Or DGL1.AgSelectedValue(mColumnIndex, mRowIndex).Trim = "" Then
                        DGL1.Item(Col1_GroupCode, mRowIndex).Value = ""
                        DGL1.Item(Col1_GroupNature, mRowIndex).Value = ""
                    Else
                        If DGL1.AgHelpDataSet(mColumnIndex) IsNot Nothing Then
                            DrTemp = DGL1.AgHelpDataSet(mColumnIndex).Tables(0).Select("SubCode = " & AgL.Chk_Text(DGL1.AgSelectedValue(mColumnIndex, mRowIndex)) & "")
                            DGL1.Item(Col1_GroupCode, mRowIndex).Value = AgL.XNull(DrTemp(0)("GroupCode"))
                            DGL1.Item(Col1_GroupNature, mRowIndex).Value = AgL.XNull(DrTemp(0)("GroupNature"))
                        End If
                    End If

            End Select

            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DrTemp = Nothing
        End Try
    End Sub


    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGL1.EditingControlShowing
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If TypeOf e.Control Is ComboBox Then
            e.Control.Text = "" : CType(e.Control, ComboBox).SelectedIndex = -1
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
        sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DGL1.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try

        Call Calculation()

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

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
        TxtSite_Code.Validating, TxtV_Type.Validating, TxtV_No.Validating, TxtDocId.Validating, TxtV_Date.Validating

        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing

        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        LblV_Type.Tag = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = TxtV_Type.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & "")
                            LblV_Type.Tag = AgL.XNull(DrTemp(0)("NCat"))
                        End If
                    End If

                Case TxtV_Date.Name
                    If TxtV_Date.Text.Trim = "" Then TxtV_Date.Text = Format(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate)), AgLibrary.ClsConstant.DateFormat_ShortDate)
            End Select

            If Topctrl1.Mode = "Add" And TxtV_Type.AgSelectedValue.Trim <> "" And TxtV_Date.Text.Trim <> "" And TxtSite_Code.Text.Trim <> "" Then
                mSearchCode = AgL.GetDocId(TxtV_Type.AgSelectedValue, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
                TxtDocId.Text = mSearchCode
                TxtV_No.Text = Val(AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                LblPrefix.Text = AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            End If

            Call Calculation()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If DtTemp IsNot Nothing Then DtTemp.Dispose()
            DrTemp = Nothing
        End Try
    End Sub

    Private Sub Calculation()
        Dim I As Integer = 0
        If Topctrl1.Mode = "Browse" Then Exit Sub

        With DGL1
            For I = 0 To DGL1.Rows.Count - 1
                If .Item(Col1_SubCode, I).Value Is Nothing Then .Item(Col1_SubCode, I).Value = ""
                If .Item(Col1_Date, I).Value Is Nothing Then .Item(Col1_Date, I).Value = ""
                If .Item(Col1_AmtDr, I).Value Is Nothing Then .Item(Col1_AmtDr, I).Value = ""
                If .Item(Col1_AmtCr, I).Value Is Nothing Then .Item(Col1_AmtCr, I).Value = ""

                If .Item(Col1_SubCode, I).Value.ToString.Trim <> "" And _
                    (Val(.Item(Col1_AmtDr, I).Value) > 0 Or Val(.Item(Col1_AmtCr, I).Value) > 0) Then

                    .Item(Col1_Date, I).Value = TxtV_Date.Text
                End If
            Next
        End With
    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0, J As Integer = 0
        Dim bFlag As Boolean = False

        Try
            Call Calculation()

            If AgL.RequiredField(TxtSite_Code) Then Exit Function
            If AgL.RequiredField(TxtV_Type) Then Exit Function
            If AgL.RequiredField(TxtV_Date, "Voucher Date") Then Exit Function


            If CDate(TxtV_Date.Text.ToString) > CDate(AgL.PubEndDate) Then
                MsgBox("Opening Date Can't Be Greater Than From " & AgL.PubEndDate & "!...") : TxtV_Date.Focus() : Exit Function
            ElseIf CDate(TxtV_Date.Text.ToString) < DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate)) Then
                MsgBox("Opening Date Can't Be Less Than From " & DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate)) & "!...") : TxtV_Date.Focus() : Exit Function
            End If

            If AgCL.AgIsDuplicate(DGL1, "" & Col1_SubCode & "") Then Exit Function

            With DGL1
                For I = 0 To DGL1.Rows.Count - 1
                    If .Item(Col1_SubCode, I).Value.ToString.Trim <> "" And _
                        (Val(.Item(Col1_AmtDr, I).Value) > 0 Or Val(.Item(Col1_AmtCr, I).Value) > 0) Then

                        If bFlag = False Then bFlag = True : Exit For
                    End If
                Next

                If Not bFlag Then MsgBox("Fill A/c Opening Balance In The Grid!...") : DGL1.CurrentCell = DGL1(Col1_SubCode, 0) : DGL1.Focus() : Exit Function
            End With


            If Topctrl1.Mode = "Add" Then
                mSearchCode = AgL.GetDocId(TxtV_Type.AgSelectedValue, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
                TxtV_No.Text = Val(AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                LblPrefix.Text = AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

                If mSearchCode <> TxtDocId.Text Then
                    MsgBox("DocId : " & TxtDocId.Text & " Already Exist New DocId Alloted : " & mSearchCode & "")
                    TxtDocId.Text = mSearchCode
                End If
            End If

            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Public Sub FindMove(ByVal mDocId As String)
        Try
            If mDocId <> "" Then
                AgL.PubSearchRow = mDocId
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                Call MoveRec()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
