Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmLeadFollowup


    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Action As String = "Action"
    Public Const Col1Status As String = "Status"
    Public Const Col1NextDate As String = "Next Date"
    Public Const Col1NextAction As String = "Next Action"
    Public Const Col1Remark As String = "Remark"

    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"


    Public Const rowCurrentAction As Integer = 0
    Public Const rowStatus As Integer = 1
    Public Const rowNextDate As Integer = 2
    Public Const rowNextAction As Integer = 3
    Public Const rowRemark As Integer = 4


    Public Const hcCurrentAction As String = "Current Action"
    Public Const hcStatus As String = "Status"
    Public Const hcNextDate As String = "Next Date"
    Public Const hcNextAction As String = "Next Action"
    Public Const hcRemark As String = "Remark"

    Dim mSearchCode As String

    Dim mEntryMode$ = ""
    Dim mLeadCode$ = ""
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property
    Public Property LeadCode() As String
        Get
            LeadCode = mLeadCode
        End Get
        Set(ByVal value As String)
            mLeadCode = value
        End Set
    End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    AgL.FPaintForm(Me, e, 0)
    'End Sub

    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Action, 120, 255, Col1Action, True, True)
            .AddAgTextColumn(Dgl1, Col1Status, 120, 255, Col1Status, True, True)
            .AddAgTextColumn(Dgl1, Col1NextDate, 120, 255, Col1NextDate, True, True)
            .AddAgTextColumn(Dgl1, Col1NextAction, 120, 255, Col1NextAction, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 120, 255, Col1Remark, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 200, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 350, 255, Col1Value, True, False)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 0, 255, Col1HeadOriginal, False, False)
        End With
        AgL.AddAgDataGrid(DglMain, PnlMain)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.Name = "DglMain"
        AgL.GridDesign(DglMain)


        DglMain.Rows.Add(5)
        DglMain.Item(Col1Head, rowCurrentAction).Value = hcCurrentAction
        DglMain.Item(Col1Head, rowStatus).Value = hcStatus
        DglMain.Item(Col1Head, rowNextDate).Value = hcNextDate
        DglMain.Item(Col1Head, rowNextAction).Value = hcNextAction
        DglMain.Item(Col1Head, rowRemark).Value = hcRemark
    End Sub
    Public Sub FMoverec(Code As String)
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mQry As String

        mQry = "SELECT * FROM LeadActivity Where LeadCode = '" & Code & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        For I = 0 To DtTemp.Rows.Count - 1
            Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
            Dgl1.Item(Col1Action, I).Value = AgL.XNull(DtTemp.Rows(I)("CurrentAction"))
            Dgl1.Item(Col1Status, I).Value = AgL.XNull(DtTemp.Rows(I)("Status"))
            Dgl1.Item(Col1NextDate, I).Value = AgL.XNull(DtTemp.Rows(I)("NextDate"))
            Dgl1.Item(Col1NextAction, I).Value = AgL.XNull(DtTemp.Rows(I)("NextAction"))
            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(DtTemp.Rows(I)("Remark"))
        Next I
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            DglMain.CurrentCell = DglMain.Item(Col1Value, rowCurrentAction)
            'Me.Top = 400
            'Me.Left = 400
            DglMain.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Dim mQry As String
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub
            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Select Case DglMain.CurrentCell.RowIndex
                Case rowNextDate
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowCurrentAction, rowNextAction
                    mQry = " Select 'Phone' as Code, 'Phone' as Description "
                    mQry += " Union All Select 'Visit' as Code, 'Visit' as Description "
                    DglMain.AgHelpDataSet(Col1Value) = AgL.FillData(mQry, AgL.GCn)
                Case rowStatus
                    mQry = " Select 'Cold' as Code, 'Cold' as Description "
                    mQry += " Union All Select 'Warm' as Code, 'Warm' as Description "
                    mQry += " Union All Select 'Hot' as Code, 'Hot' as Description "
                    mQry += " Union All Select 'Close' as Code, 'Close' as Description "
                    mQry += " Union All Select 'Lost' as Code, 'Lost' as Description "
                    DglMain.AgHelpDataSet(Col1Value) = AgL.FillData(mQry, AgL.GCn)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If mEntryMode = "Browse" Then Exit Sub


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If EntryMode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub
    Public Sub Calculation()
        Dim I As Integer
        Dim mTotalNewAmount
    End Sub
    Public Sub FSave()
        Dim I As Integer
        Dim mTrans As String

        Try
            If AgL.XNull(DglMain.Item(Col1Value, rowCurrentAction).Value).ToString.ToUpper = "N/A" Then
                If DglMain.Item(Col1Value, rowStatus).Value = "" Then
                    MsgBox("Remark can not be blank")
                    DglMain.CurrentCell = DglMain(Col1Value, rowStatus)
                    DglMain.Focus()
                    Exit Sub
                End If
            End If


            If DglMain.Item(Col1Value, rowStatus).Value <> "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowCurrentAction).Value).ToString.ToUpper = "" Then
                    MsgBox("Unable to connect can not be blank")
                    DglMain.CurrentCell = DglMain(Col1Value, rowCurrentAction)
                    DglMain.Focus()
                    Exit Sub
                End If


                If AgL.XNull(DglMain.Item(Col1Value, rowNextDate).Value).ToString.ToUpper = "" Then
                    MsgBox("Next date can not be blank")
                    DglMain.CurrentCell = DglMain(Col1Value, rowNextDate)
                    DglMain.Focus()
                    Exit Sub
                End If

            End If


            If DglMain.Item(Col1Value, rowStatus).Value = "" And DglMain.Item(Col1Value, rowNextDate).Value = "" And DglMain.Item(Col1Value, rowCurrentAction).Value = "" Then Exit Sub


            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "INSERT INTO LeadActivity (LeadCode, CurrentAction, Status, NextDate, NextAction, Remark, EntryBy, EntryDate)
                    VALUES (" & AgL.Chk_Text(mLeadCode) & ", 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowCurrentAction).Value) & ", 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStatus).Value) & ", 
                    " & AgL.Chk_Date(DglMain.Item(Col1Value, rowNextDate).Value) & ", 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowNextAction).Value) & ", 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowRemark).Value) & ", 
                    " & AgL.Chk_Text(AgL.PubUserName) & ", 
                    " & AgL.Chk_Date(AgL.PubLoginDate) & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"

            mOkButtonPressed = True
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try

        'If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
        '    AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        'End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If EntryMode = "Browse" Then
            Select Case e.KeyCode
                Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
                Case Else
                    e.Handled = True
            End Select
            Exit Sub
        End If

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                sender.Rows(sender.currentcell.rowindex).Visible = False
                Calculation()
                e.Handled = True
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub



    End Sub
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        FSave()
        If mOkButtonPressed = True Then
            Me.Close()
        End If
    End Sub
End Class