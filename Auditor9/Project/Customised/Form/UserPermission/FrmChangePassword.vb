Public Class FrmChangePassword

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        If TxtOldPassword.Text.ToUpper = AgL.PubUserPassword.ToUpper Then
            If TxtNewPassword.Text.ToUpper = TxtConfirmPassword.Text.ToUpper Then
                AgL.Dman_ExecuteNonQry("Update UserMast set PassWd =" & AgL.Chk_Text(AgL.CODIFY(TxtNewPassword.Text)) & " Where User_Name = '" & LblTitle.Text & "' ", AgL.GCn)
                Call AgL.LogTableEntry(LblTitle.Text, Me.Text, "E", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd, "Password change Old password : " & AgL.CODIFY(TxtOldPassword.Text) & "  New Password : " & AgL.CODIFY(TxtNewPassword.Text))
                MsgBox("Password changed successfully. Please login again.")
                End
            Else
                MsgBox("Confirm password is incorrect")
                TxtConfirmPassword.Focus()
            End If
        Else
            MsgBox("Incorrect password")
            TxtOldPassword.Focus()
        End If
    End Sub

    Private Sub FrmChangePassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If  (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Private Sub FrmChangePassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LblTitle.Text = AgL.PubUserName.ToUpper
        TxtOldPassword.Focus()

    End Sub

    Private Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Dispose()
    End Sub
End Class