Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmRemindMe
    Dim mQry As String = ""
    Dim mStrReminderId As String = ""
    Dim mStrRemindBy As String = ""
    Dim mStrRemindDateTime As String = ""
    Dim mStrRemindNarration As String = ""
    Const Period_Hour As String = "Hour"
    Const Period_Day As String = "Day"
    Const Period_Week As String = "Week"
    Const Period_Month As String = "Month"
    Const Period_Year As String = "Year"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Public Property ReminderId() As String
        Get
            ReminderId = mStrReminderId
        End Get
        Set(ByVal value As String)
            mStrReminderId = value
        End Set
    End Property

    Public Property RemindBy() As String
        Get
            RemindBy = mStrRemindBy
        End Get
        Set(ByVal value As String)
            mStrRemindBy = value
        End Set
    End Property

    Public Property RemindDateTime() As String
        Get
            RemindDateTime = mStrRemindDateTime
        End Get
        Set(ByVal value As String)
            mStrRemindDateTime = value
        End Set
    End Property

    Public Property RemindNarration() As String
        Get
            RemindNarration = mStrRemindNarration
        End Get
        Set(ByVal value As String)
            mStrRemindNarration = value
        End Set
    End Property

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AGL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        ' DTMaster = Nothing
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
        AGL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'AgL.WinSetting(Me, 333, 460, 0, 0)
            'Me.StartPosition = FormStartPosition.CenterParent
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

    End Sub

    Sub Ini_List()

    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText(True)
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        'Dim MastPos As Long
        Dim mTrans As Boolean = False

        Try

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
        ' If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


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


    End Sub

    Public Sub MoveRec()
        Try
            LblRemindeBy.Text = "Reminder By : " & mStrRemindBy & ""
            LblRemindOnDateTime.Text = "On : " & mStrRemindDateTime & ""
            TxtNarration.Text = mStrRemindNarration

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        ' mSearchCode = ""
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        TxtId.Visible = False
        LblID.Visible = False
        TxtNarration.ReadOnly = True
        Topctrl1.Enabled = False
        DODPeriodValue.SelectedItem = "0"
        DUDPeriodText.SelectedItem = Period_Day
        DODPeriodValue.ReadOnly = True
        DUDPeriodText.ReadOnly = True
    End Sub

    Private Sub FClear()
        'DTStruct.Clear()
    End Sub

    Private Sub FAddRowStructure()
        'Dim DRStruct As DataRow
        Try
            'DRStruct = DTStruct.NewRow
            'DTStruct.Rows.Add(DRStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
       TxtId.Validating, DODPeriodValue.Validating
        Try
            Select Case sender.NAME
                Case TxtId.Name
                    'If TxtId.Text.Trim <> "" Then TxtNarration.Text = TxtId.Text
            End Select
            Call Calculation()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Dim mStrCurrentdate As String
        Dim mStrCurrentTime As String

        mStrCurrentdate = AgL.GetDateTime(AgL.GCn, AgL.ECmd).Substring(0, 11)
        mStrCurrentTime = CDate(AgL.GetDateTime(AgL.GCn, AgL.ECmd)).TimeOfDay.ToString().Substring(0, 5)

        Try
            Call Calculation()

            mQry = " UPDATE ReminderDetail " & _
                    " SET ActReminder_Date = " & AgL.Chk_Text(mStrCurrentdate) & ", " & _
                    " ActReminder_Time = " & AgL.Chk_Text(mStrCurrentTime) & ", " & _
                    " Status = 'InActive' " & _
                    " WHERE ID = " & AgL.Chk_Text(mStrReminderId) & "  " & _
                    " AND Reminder_To = " & AgL.Chk_Text(AgL.PubUserName) & " "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If DODPeriodValue.SelectedItem <> "0" And DUDPeriodText.SelectedItem <> "" Then
                Call ClsMain.ProcReminderSave(mStrCurrentdate, mStrCurrentTime, TxtReminderDate.Text, TxtReminderTime.Text, TxtNarration.Text, AgL.PubUserName, "", mStrReminderId)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Me.Close()
    End Sub

    Private Sub Calculation()
        Dim mCurrentDateTime As String
        Dim mReminderDateTime As String = ""
        TxtReminderDate.Text = AgL.GetDateTime(AgL.GCn, AgL.ECmd).Substring(0, 11)
        TxtReminderTime.Text = CDate(AgL.GetDateTime(AgL.GCn, AgL.ECmd)).TimeOfDay.ToString().Substring(0, 5)

        mCurrentDateTime = AgL.GetDateTime(AgL.GCn, AgL.ECmd).ToString()

        If DODPeriodValue.SelectedItem <> "0" Then
            Select Case DUDPeriodText.SelectedItem
                Case Period_Hour
                    mReminderDateTime = CDate(mCurrentDateTime).AddHours(Val(DODPeriodValue.SelectedItem)).ToString()
                Case Period_Day
                    mReminderDateTime = CDate(mCurrentDateTime).AddDays(Val(DODPeriodValue.SelectedItem)).ToString()
                Case Period_Week
                    mReminderDateTime = CDate(mCurrentDateTime).AddDays(Val(DODPeriodValue.SelectedItem) * 7).ToString()
                Case Period_Month
                    mReminderDateTime = CDate(mCurrentDateTime).AddMonths(Val(DODPeriodValue.SelectedItem)).ToString()
                Case Period_Year
                    mReminderDateTime = CDate(mCurrentDateTime).AddYears(Val(DODPeriodValue.SelectedItem)).ToString()
            End Select

            TxtReminderDate.Text = mReminderDateTime.ToString.Substring(0, 11)
            TxtReminderTime.Text = CDate(mReminderDateTime).TimeOfDay.ToString().Substring(0, 5)

        End If

    End Sub
End Class
