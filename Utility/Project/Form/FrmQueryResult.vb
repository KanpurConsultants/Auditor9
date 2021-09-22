Imports System.Data.SqlClient
Public Class FrmQueryResult
    Dim mQry As String = ""
    Dim DtTemp As DataTable = Nothing
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Sub New(ByVal DataTableValue As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        DtTemp = DataTableValue
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Public Sub IniGrid()
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AllowUserToAddRows = False
        Dgl1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        Dgl1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        Dgl1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.CellBorderStyle = DataGridViewCellBorderStyle.None
        Dgl1.ReadOnly = True
        Dgl1.AllowUserToDeleteRows = False
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
            If e.KeyCode = Keys.Escape Then Me.Close()
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Me.Close()
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            IniGrid()
            BtnCancel.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
            Dgl1.DataSource = DtTemp
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub
End Class