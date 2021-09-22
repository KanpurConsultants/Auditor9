Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmUpdateStatus
    Dim mQry As String = ""
    Dim mDocId As String = ""
    Dim mUId As String = ""
    Dim mTableName As String = ""

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        'Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        'Topctrl1.SetDisp(True)
    End Sub

    Public Property DocId() As String
        Get
            DocId = mDocId
        End Get
        Set(ByVal value As String)
            mDocId = value
        End Set
    End Property

    Public Property UID() As String
        Get
            UID = mUId
        End Get
        Set(ByVal value As String)
            mUId = value
        End Set
    End Property

    Public Property TableName() As String
        Get
            TableName = mTableName
        End Get
        Set(ByVal value As String)
            mTableName = value
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
        mQry = " SELECT '" & AgTemplate.ClsMain.EntryStatus.Active & "' AS Code, '" & AgTemplate.ClsMain.EntryStatus.Active & "' AS Status " & _
                " UNION ALL " & _
                " SELECT '" & AgTemplate.ClsMain.EntryStatus.Inactive & "' AS Code, '" & AgTemplate.ClsMain.EntryStatus.Inactive & "' AS Status " & _
                " UNION ALL " & _
                " SELECT '" & AgTemplate.ClsMain.EntryStatus.Cancelled & "' AS Code, '" & AgTemplate.ClsMain.EntryStatus.Cancelled & "' AS Status " & _
                " UNION ALL " & _
                " SELECT '" & AgTemplate.ClsMain.EntryStatus.Closed & "' AS Code, '" & AgTemplate.ClsMain.EntryStatus.Closed & "' AS Status "
        TxtStatus.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT USER_NAME AS Code, USER_NAME AS Name FROM UserMast "
        TxtEntryBy.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

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
        'TxtNarration.Focus()
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


    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Try
            mQry = " SELECT TOP 1 * FROM Status_Update " & _
                    " WHERE TableName = '" & mTableName & "' AND DocId ='" & mDocId & "' " & _
                    " ORDER BY EntryDate "
            DsTemp = AgL.FillData(mQry, AgL.GCn)

            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    TxtStatus.Text = AgL.XNull(.Rows(0)("Status"))
                    TxtRemark.Text = AgL.XNull(.Rows(0)("StatusRemark"))
                    TxtEntryBy.Text = AgL.XNull(.Rows(0)("EntryBy"))
                    TxtEntryDate.Text = AgL.XNull(.Rows(0)("EntryDate"))
                End If
            End With

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
        'TxtStatus.Visible = False
        'LblID.Visible = False
        Topctrl1.Enabled = False

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

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Try
            If TxtEntryDate.Text = "" Then
                TxtEntryDate.Text = AgL.PubLoginDate
            End If

            mQry = " INSERT INTO Status_Update (TableName, DocId, Status, StatusRemark, EntryBy, EntryDate ) " & _
                    " VALUES ( " & AgL.Chk_Text(mTableName) & " , " & AgL.Chk_Text(mDocId) & " ," & AgL.Chk_Text(TxtStatus.Text) & "," & AgL.Chk_Text(TxtRemark.Text) & ", " & AgL.Chk_Text(AgL.PubUserName) & ",  " & AgL.Chk_Text(TxtEntryDate.Text) & " ) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = " Update " & mTableName & " SET " & _
                    " Status = '" & TxtStatus.Text & "', " & _
                    " StatusRemark = '" & TxtRemark.Text & "', " & _
                    " StatusChangeBy = " & AgL.Chk_Text(AgL.PubUserName) & ", " & _
                    " StatusChangeDate = " & AgL.Chk_Text(TxtEntryDate.Text) & " " & _
                    " WHERE DocID = '" & mDocId & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub
End Class
