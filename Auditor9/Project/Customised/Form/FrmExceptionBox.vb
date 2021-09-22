Imports Customised.ClsMain
Public Class FrmExceptionBox
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public DtV_TypeSettings As DataTable
    Public Const ColSNo As String = "S.No."
    Dim mQry As String = ""
    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Sub Ini_Grid()
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.ReadOnly = True
        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        Dgl1.BackgroundColor = Color.White
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
End Class