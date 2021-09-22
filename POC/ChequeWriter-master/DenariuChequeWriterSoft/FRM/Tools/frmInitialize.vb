Public Class frmInitialize

    Private Sub frmInitialize_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            ShowLogin()
        End If
    End Sub

    Sub ShowLogin()
        Timer1.Enabled = False
        Me.Hide()
        frmLogin.Show()
        frmLogin.BringToFront()
        frmLogin.txtUserName.Focus()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShowLogin()
    End Sub

    Private Sub frmInitialize_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmInitialize_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        pnlMain.Left = (Me.Width - pnlMain.Width) / 2
        pnlMain.Top = (Me.Height - pnlMain.Height) / 2
    End Sub
End Class