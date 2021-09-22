Imports System.Data.SQLite
Public Class FrmStructureAcPosting
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""

    Private Const Col_SNo As Byte = 0
    Public WithEvents DGL1 As New AgControls.AgDataGrid

    Private Const Col1_Charges As Byte = 1
    Private Const Col1_ChargesManualCode As Byte = 2
    Private Const Col1_PostAc As Byte = 3
    Private Const Col1_DrCr As Byte = 4
    Private Const Col1_Structure As Byte = 5



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
        AgL.AddAgDataGrid(DGL1, Pnl1)
        With AgCL
            .AddAgNumberColumn(DGL1, "DGL1SNo", 40, 5, 0, False, "S.No.", True, False, True)
            .AddAgTextColumn(DGL1, "DGL1Charges", 170, 0, "Charges", True, False, False)
            .AddAgTextColumn(DGL1, "DGL1ChargesManualCode", 100, 0, "Charges Code", True, True, False)
            .AddAgTextColumn(DGL1, "DGL1PostAc", 150, 0, "Post A/c", True, False, False)
            .AddAgListColumn(DGL1, "Dr,Cr", "DGL1DrCr", 80, "Dr,Cr", "Dr/Cr", True, False, False)
            .AddAgTextColumn(DGL1, "Structure", 150, 0, "Structure", False, False, False)
        End With
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
            AgL.WinSetting(Me, 650, 990, 0, 0)
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
        mQry = "Select distinct NCat As SearchCode " & _
                " From Structure_AcPosting  "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        mQry = "SELECT C.NCat AS Code, Max(C.NCatDescription) AS EntryType FROM VoucherCat C Group by C.NCat Order By Max(C.NCatDescription)"

        TxtCode.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        'TxtCopyFrom.AgHelpDataSet = TxtDescription.AgHelpDataSet.Copy

        mQry = "Select Code, Description, ManualCode From Charges Order By Description"
        DGL1.AgHelpDataSet(DGL1.Columns(Col1_Charges).Index) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select SubCode, Name From Subgroup Where " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Site_Code", AgL.PubSiteCode, "CommonAc") & " "
        mQry = mQry + "Union All Select '|PARTY|' AS SubCode, '|PARTY|' AS Name  Order By Name "

        DGL1.AgHelpDataSet(DGL1.Columns(Col1_PostAc).Index) = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub Topctrl1_StyleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Topctrl1.StyleChanged

    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtCode.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
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

                    mQry = "Delete From Structure_AcPosting Where ncat = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


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

    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = "Select  NCAT As SearchCode,  NCATDescription As [ENTRY TYPE]  " & _
                                " From  Structure_AcPosting   "

            AgL.PubFindQryOrdBy = "[SearchCode]"


            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, "Find", AgL)
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

    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn

    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim I As Integer, mSr As Integer = 0
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If Not Data_Validation() Then Exit Sub


            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Structure_AcPosting Where NCat='" & TxtCode.AgSelectedValue & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Entry Type Already Exist!") : TxtCode.Focus() : Exit Sub

                mSearchCode = TxtCode.AgSelectedValue  'AgL.GetMaxId("Charges", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 6, True, True, , AgL.Gcn_ConnectionString)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Structure_AcPosting Where NCat='" & TxtCode.AgSelectedValue & "' And NCAT<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Code Already Exist!") : TxtCode.Focus() : Exit Sub
            End If




            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            'If Topctrl1.Mode = "Add" Then


            '    mQry = "INSERT INTO Structure_AcPosting " & _
            '        "(Code,Div_Code, Site_Code," & _
            '        "Description, HeaderTable, LineTable,PreparedBy,U_EntDt,U_AE) " & _
            '        "VALUES " & _
            '        "(" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(AgL.PubSiteCode) & ",   " & _
            '        "" & AgL.Chk_Text(TxtDescription.Text) & "," & AgL.Chk_Text(TxtHeaderTable.Text) & ", " & AgL.Chk_Text(TxtLineTable.Text) & "," & _
            '        " " & AgL.Chk_Text(AgL.PubUserName) & "," & AgL.ConvertDate(AgL.PubLoginDate) & ",'" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "')"

            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            'Else
            '    mQry = "UPDATE Structure " & _
            '           "SET Description =" & AgL.Chk_Text(TxtDescription.Text) & ", HeaderTable =" & AgL.Chk_Text(TxtHeaderTable.Text) & ",LineTable =" & AgL.Chk_Text(TxtLineTable.Text) & ", " & _
            '           " U_AE = 'E',	Edit_Date = " & AgL.ConvertDate(AgL.PubLoginDate) & ",	ModifiedBy = " & AgL.Chk_Text(AgL.PubUserName) & "  " & _
            '          "Where Code = '" & mSearchCode & "' "

            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            'End If

            mQry = "Delete From Structure_AcPosting Where NCat = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            With DGL1
                mSr = 0
                For I = 0 To .Rows.Count - 1
                    If .Item("Dgl1Charges", I).Value <> "" Then
                        mSr = mSr + 1

                        mQry = "INSERT INTO  dbo.Structure_AcPosting " & _
                        "( " & _
                        "NCat, " & _
                        "Sr, " & _
                        "Structure, " & _
                        "Charges, " & _
                        "PostAc, " & _
                        "DrCr " & _
                        ") " & _
                        "VALUES" & _
                        "(" & _
                        "" & AgL.Chk_Text(mSearchCode) & ", " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col_SNo, I).Value) & ", " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_Structure, I).Value) & ", " & _
                        "" & AgL.Chk_Text(DGL1.AgSelectedValue(Col1_Charges, I)) & " , " & _
                        "" & AgL.Chk_Text(DGL1.AgSelectedValue(Col1_PostAc, I)) & " , " & _
                        "" & AgL.Chk_Text(DGL1.AgSelectedValue(Col1_DrCr, I)) & "  " & _
                        ") "

                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
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

        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then



                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                'mQry = "Select SI.* " & _
                '        " From Structure Si " & _
                '        " Where SI.Code='" & mSearchCode & "'"
                'DsTemp = AgL.FillData(mQry, AgL.GCn)
                'With DsTemp.Tables(0)
                '    If .Rows.Count > 0 Then
                '        TxtCode.AgSelectedValue = AgL.XNull(.Rows(0)("Code"))

                '    End If
                'End With

                TxtCode.AgSelectedValue = mSearchCode
                MoveRecLine(mSearchCode)

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

    Private Sub MoveRecLine(ByVal mCode As String)
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = "Select Sd.Sr, Sd.Code as Structure, sd.Charges, SN.PostAc, SN.DrCr, C.ManualCode " & _
            " From VoucherCat SC " & _
            " Left Join StructureDetail SD On SC.Structure = Sd.Code " & _
            " Left Join Structure_AcPosting SN On SD.Charges + '" & mCode & "' = SN.Charges + SN.NCat  " & _
            " Left Join Charges C On Sd.Charges = C.Code " & _
            " Where SC.NCat='" & mCode & "' Order By SD.Sr"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGL1.RowCount = 1
            DGL1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGL1.Rows.Add()
                    DGL1.Item("Dgl1Sno", I).Value = AgL.VNull(.Rows(I)("Sr"))
                    DGL1.Item(Col1_Structure, I).Value = AgL.XNull(.Rows(I)("Structure"))
                    DGL1.AgSelectedValue(Col1_Charges, I) = AgL.XNull(.Rows(I)("Charges"))
                    DGL1.Item(Col1_ChargesManualCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                    DGL1.AgSelectedValue(Col1_PostAc, I) = AgL.XNull(.Rows(I)("PostAc"))
                    DGL1.AgSelectedValue(Col1_DrCr, I) = AgL.XNull(.Rows(I)("DrCr"))
                Next I
            End If
        End With

    End Sub


    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes()
        mSearchCode = ""

        DGL1.RowCount = 1 : DGL1.Rows.Clear()

    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If Topctrl1.Mode <> "Add" Then
            TxtCode.Enabled = False
            BtnFill.Enabled = False
        Else
            TxtCode.Enabled = True
            BtnFill.Enabled = True
        End If
    End Sub

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case DGL1.CurrentCell.ColumnIndex
                Case "Dgl1Charges"

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow()

        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1_Charges
                    DrTemp = DGL1.AgHelpDataSet(mColumnIndex).Tables(0).Select("Code = " & AgL.Chk_Text(DGL1.AgSelectedValue(mColumnIndex, mRowIndex)) & "")
                    If DrTemp.Length > 0 Then
                        DGL1.Item(Col1_ChargesManualCode, mRowIndex).Value = AgL.XNull(DrTemp(0)("ManualCode"))
                    End If

            End Select


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
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try
        AgL.FSetSNo(sender, Col_SNo)

        If DGL1.Rows.Count = 1 And Topctrl1.Mode = "Add" Then
            If DGL1.Item("Dgl1Charges", 0).Value Is Nothing Then DGL1.Item("Dgl1Charges", 0).Value = ""

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

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            Select Case sender.NAME

            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0
        Try



            If AgCL.AgCheckMandatory(Me) = False Then Exit Function

            If AgCL.AgIsDuplicate(DGL1, "" & "Dgl1Charges" & "") Then Exit Function

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If .Item("Dgl1Charges", I).Value Is Nothing Then .Item("Dgl1Charges", I).Value = ""

                Next
            End With


            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function



    Private Sub BtnCopyFrom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyFrom.Click
        If TxtCopyFrom.Text <> "" Then
            If MsgBox("Sure to Copy From Selected Value?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                MoveRecLine(TxtCopyFrom.AgSelectedValue)
            End If
        End If
    End Sub

    Private Sub BtnFill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        MoveRecLine(TxtCode.AgSelectedValue)
    End Sub
End Class
