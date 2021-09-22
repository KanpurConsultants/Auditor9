Public Class FrmErrorBox
    Dim mQry As String = ""
    Public mQuitButtonPressed As Boolean

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mErrorQry$ = ""

    Public Sub New(ByVal ErrorQry As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        mErrorQry = ErrorQry
        If mErrorQry <> "" Then Dgl1.DataSource = AgL.FillData(mErrorQry, AgL.GCn).Tables(0)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            mQuitButtonPressed = True
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Dgl1.ReadOnly = True
            Dgl1.EnableHeadersVisualStyles = False
            FManageGrid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FManageGrid()
        Dgl1.Columns("Item").Width = 200
        Dgl1.Columns("Message").Width = 365

        Dim I As Integer = 0
        For I = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Item(I, 0).Value.GetType.ToString.ToUpper = "SYSTEM.INT32" Or Dgl1.Item(I, 0).Value.GetType.ToString.ToUpper = "SYSTEM.DECIMAL" Then
                Dgl1.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Dgl1.Columns(I).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            Dgl1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item("Message", I).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Underline)
            Dgl1.Item("Message", I).Style.ForeColor = Color.Blue
        Next

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows()
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnQuit.Click, BtnContinue.Click
        Select Case sender.Name
            Case BtnQuit.Name
                mQuitButtonPressed = True
                Me.Close()

            Case BtnContinue.Name
                mQuitButtonPressed = False
                Me.Close()
        End Select
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case "Message"
                    MsgBox(Dgl1.Item("Message", Dgl1.CurrentCell.RowIndex).Value, MsgBoxStyle.Information)
            End Select
        Catch ex As Exception
        End Try
    End Sub
End Class