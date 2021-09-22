Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmReminder
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""
    Dim mStrReminderTo As String = ""
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const ColSNo As String = "ColSNo"
    Protected Const Col1Select As String = "Select"
    Protected Const Col1UserName As String = "User Name"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AGL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgCheckColumn(Dgl1, Col1Select, 60, Col1Select, True)
            .AddAgTextColumn(Dgl1, Col1UserName, 250, 21, Col1UserName, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 30
        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = False

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
        AGL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 450, 900, 0, 0)
            AgL.GridDesign(Dgl1)
            IniGrid()
            Topctrl1.ChangeAgGridState(Dgl1, False)
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Dim mCondStr As String
        mCondStr = " WHERE EntryBy = " & AgL.Chk_Text(AgL.PubUserName) & " "
        mQry = " Select ID As SearchCode " & _
                " From Reminder " & mcondStr
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Sub Ini_List()
        mQry = "SELECT User_Name AS Code,User_Name  FROM UserMast ORDER BY USER_NAME "
        TxtEntryBy.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        Dgl1.AgHelpDataSet(Col1UserName) = TxtEntryBy.AgHelpDataSet.Copy
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText(True)
        ProcFillUser()
        TxtEntryBy.Text = AgL.PubUserName
        TxtDate.Text = AgL.GetDateTime(AgL.GCn, AgL.ECmd).Substring(0, 11)
        TxtTime.Text = CDate(AgL.GetDateTime(AgL.GCn, AgL.ECmd)).TimeOfDay.ToString().Substring(0, 5)
        TxtReminderDate.Focus()
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

                    AgL.Dman_ExecuteNonQry("Delete From ReminderDetail Where Id='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From Reminder Where Id='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

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
        TxtNarration.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        Dim mCondStr As String = ""
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try

            mCondStr = " WHERE EntryBy = " & AgL.Chk_Text(AgL.PubUserName) & " "

            AgL.PubFindQry = " SELECT ID AS SearchCode, V_Date + V_Time AS [Entry DATE], EntryBy AS [Remind By], Narration, " & _
                            " Reminder_Date + Reminder_Time AS [Reminder DATE],RemindTo AS [Remind To] " & _
                            " FROM Reminder " & mCondStr


            AgL.PubFindQryOrdBy = "[Entry DATE]"


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
    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn

    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim mStrTime() As String = Nothing
        Try
            ProcMakeReminderTo()


            If AgL.RequiredField(TxtReminderDate, LblReminderDate.Text) Then Exit Sub
            If AgL.RequiredField(TxtNarration, LblNarration.Text) Then Exit Sub
            If TxtReminderTime.Text = "" Then TxtReminderTime.Text = "12:00"

            If mStrReminderTo = "" Then
                MsgBox("Select Reminder To User") : Exit Sub
            End If
            mStrReminderTo = mStrReminderTo.Substring(0, mStrReminderTo.Length - 1)
            TxtReminderTime.Text = TxtReminderTime.Text.ToString().Replace(".", ":")
            TxtReminderTime.Text = TxtReminderTime.Text.ToString().Replace("/", ":")

            If TxtReminderTime.Text <> "" Then
                mStrTime = TxtReminderTime.Text.Split(":")
                If mStrTime.Length <> 2 Then
                    MsgBox("Invalid Format of Reminder Time") : TxtReminderTime.Focus() : Exit Sub
                Else
                    If Convert.ToInt16(mStrTime(0)) > 24 Or Convert.ToInt16(mStrTime(0)) < 0 Then
                        MsgBox("Invalid Format of Reminder Time") : TxtReminderTime.Focus() : Exit Sub
                    End If

                    If Convert.ToInt16(mStrTime(1)) > 60 Or Convert.ToInt16(mStrTime(0)) < 0 Then
                        MsgBox("Invalid Format of Reminder Time") : TxtReminderTime.Focus() : Exit Sub
                    End If

                End If
            End If

            If Topctrl1.Mode = "Add" Then
                Call ClsMain.ProcReminderSave(TxtDate.Text, TxtTime.Text, TxtReminderDate.Text, TxtReminderTime.Text, TxtNarration.Text, mStrReminderTo)
            Else
                Call ClsMain.ProcReminderSave(TxtDate.Text, TxtTime.Text, TxtReminderDate.Text, TxtReminderTime.Text, TxtNarration.Text, mStrReminderTo, mSearchCode)
            End If


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
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcMakeReminderTo()
        Dim I As Integer, bSr As Integer = 0
        mStrReminderTo = ""
        With Dgl1
            bSr = 0
            For I = 0 To .Rows.Count - 1
                If .Item(Col1UserName, I).Value <> "" And .Item(Col1Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue Then
                    bSr = bSr + 1
                    mStrReminderTo = mStrReminderTo + .Item(Col1UserName, I).Value + ","
                End If
            Next I
        End With
    End Sub

    Public Sub MoveRec()
        Dim I As Integer = 0, Cnt As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                mQry = " SELECT * FROM Reminder " & _
                     " WHERE ID = '" & mSearchCode & "' "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtDate.Text = AgL.XNull(.Rows(0)("V_Date"))
                        TxtTime.Text = AgL.XNull(.Rows(0)("V_Time"))
                        TxtEntryBy.Text = AgL.XNull(.Rows(0)("EntryBy"))
                        TxtNarration.Text = AgL.XNull(.Rows(0)("Narration"))
                        TxtReminderDate.Text = AgL.XNull(.Rows(0)("Reminder_Date"))
                        TxtReminderTime.Text = AgL.XNull(.Rows(0)("Reminder_Time"))
                    End If
                End With

                mQry = " SELECT * FROM ReminderDetail WHERE ID = '" & mSearchCode & "' Order By Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
                            Dgl1.Item(Col1UserName, I).Value = AgL.XNull(.Rows(I)("Reminder_To"))
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

    Private Sub ProcFillUser()
        Dim I As Integer
        Dim DsTemp As DataSet
        mQry = "SELECT H.USER_NAME  FROM UserMast H ORDER BY H.USER_NAME"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Select, I).Value = AgLibrary.ClsConstant.StrUnCheckedValue
                    Dgl1.Item(Col1UserName, I).Value = AgL.XNull(.Rows(I)("USER_NAME"))
                Next I
            End If
        End With
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = ""
        Dgl1.RowCount = 1
        Dgl1.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        TxtId.Visible = False
        LblID.Visible = False
        TxtEntryBy.Enabled = False
        TxtDate.Enabled = False
        TxtTime.Enabled = False
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
       TxtId.Validating, TxtDate.Validating
        Try
            Select Case sender.NAME
                Case TxtId.Name
                    If TxtId.Text.Trim <> "" Then TxtNarration.Text = TxtId.Text
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Dim mRowIndex As Integer = 0, mColumnIndex As Integer = 0
        Try
            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        AgL.ProcSetCheckColumnCellValue(sender, mColumnIndex)
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGL1_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl1.CellMouseUp
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex

            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    Call AgL.ProcSetCheckColumnCellValue(sender, Dgl1.CurrentCell.ColumnIndex)
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        Dim I As Integer = 0
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        Try
            With Dgl1
                For I = .Columns(Col1Select).Index To .Columns(Col1Select).Index
                    If .Columns(I).Name <> ColSNo Then
                        sender.Item(I, sender.Rows.Count - 1).Value = AgLibrary.ClsConstant.StrUnCheckedValue
                    End If
                Next
            End With
        Catch ex As Exception
        End Try
    End Sub

End Class
