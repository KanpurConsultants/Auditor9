Public Class FrmProgressBar
    Const IntMaxValue = 5000
    Dim FrmObj As Object = Nothing
    Public Sub FMoveBar()
        If PGBMain.Maximum <> IntMaxValue Then PGBMain.Maximum = IntMaxValue
        If PGBMain.Value >= IntMaxValue Then PGBMain.Value = 0

        If PGBMain1.Maximum <> IntMaxValue Then PGBMain1.Maximum = IntMaxValue
        If PGBMain1.Value >= IntMaxValue Then PGBMain1.Value = 0

        PGBMain.Value = PGBMain.Value + 50
        PGBMain1.Value = PGBMain1.Value + 50
    End Sub
    Private Sub FrmProgressBar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Width = 301
        Me.Height = 11
    End Sub
End Class