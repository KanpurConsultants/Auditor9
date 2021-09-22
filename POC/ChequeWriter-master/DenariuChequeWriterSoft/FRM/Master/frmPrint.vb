Public Class frmPrint

    Private Sub frmPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    
    Private Sub frm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyValue = Keys.Return Then
            Dim ctl As Control = Me.ActiveControl
            If ctl.Text = "+" Then

            ElseIf ctl.Text = "@" Then

            Else
                If TypeOf ctl Is TextBox Then
                    Dim txt As TextBox = DirectCast(ctl, TextBox)
                    If txt.Multiline = True Then
                        If txt.Text.EndsWith(vbCrLf) Then
                            Me.SelectNextControl(ctl, True, True, True, True)
                        End If
                    Else
                        Me.SelectNextControl(ctl, True, True, True, True)
                    End If
                Else
                    Me.SelectNextControl(ctl, True, True, True, True)
                End If


            End If

        ElseIf e.KeyValue = Keys.Escape Then
            If MsgDialog.ShowMsgDlg("Do you want to Exit?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
        End If

    End Sub

    Private Sub lblMinimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub lblClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SendKeys.Send("{ESCAPSE}")
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub pboxMinimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxMinimize.Click

    End Sub

    Private Sub pboxRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxRestore.Click
        If (Me.WindowState = FormWindowState.Maximized) Then
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.WindowState = FormWindowState.Normal
            pboxMaximize.Show()
            pboxRestore.Hide()
        End If
    End Sub

    Private Sub pboxClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxClose.Click
        Me.Close()
    End Sub

    Private Sub pboxMaximize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxMaximize.Click
        If (Me.WindowState = FormWindowState.Normal) Then
            Me.WindowState = FormWindowState.Maximized
            pboxMaximize.Hide()
            pboxRestore.Show()
        End If
    End Sub
End Class