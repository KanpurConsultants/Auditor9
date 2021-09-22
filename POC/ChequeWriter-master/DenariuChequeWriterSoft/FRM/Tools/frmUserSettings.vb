Imports System.IO
Imports DenariuChequeWriterSoft.My.Resources
Imports System.Data.SqlClient
Public Class frmUserSettings
    Dim cnn As SqlConnection
    Dim connectionString As String

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SendKeys.Send("{ESCAPE}")
    End Sub
    Private Sub frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsername.Text = UserName
    End Sub

    Private Sub frmUserSettings_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyValue = Keys.Escape Then
            If MsgDialog.ShowMsgDlg(Message.Bank_Exit, Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
        End If
    End Sub

    Function Form_Validate()
        Dim RValue As Boolean = True
        Dim OldPassword As String = db.UserAccount.Data("Password", String.Format("UserID='{0}'", UserId))
        If txtOldPassword.Text.Trim() = "" Then
            MsgBox(Message.Company_Old_Password)
            txtOldPassword.Focus()
            RValue = False
        ElseIf txtOldPassword.Text.Trim() <> OldPassword Then
            MsgBox(Message.Company_NotMatched)
            txtOldPassword.Focus()
            RValue = False
        ElseIf txtNewPassword.Text.Trim() = "" Then
            MsgBox(Message.Company_New_Password)
            txtNewPassword.Focus()
            RValue = False
        ElseIf txtConfirmPassword.Text.Trim() = "" Then
            MsgBox(Message.Company_Pwd_Conformation)
            txtConfirmPassword.Focus()
            RValue = False
        ElseIf txtConfirmPassword.Text.Trim() <> txtNewPassword.Text.Trim() Then
            MsgBox(Message.Company_Pwd_Mismatch)
            txtConfirmPassword.Focus()
            RValue = False
        End If

        Return RValue

    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Form_Validate() Then
            Dim Qry, Str As String
            Qry = String.Format("Update UserAccount Set UserName='{1}',Password='{2}' where UserId='{0}'", UserId, txtUsername.Text, txtNewPassword.Text)
            Str = JISDb.ExecuteQuery(Qry)
            If Str <> "" Then
                MsgBox(Message.Bank_Save)
                txtUsername.Text = ""
                txtOldPassword.Text = ""
                txtNewPassword.Text = ""
                txtConfirmPassword.Text = ""
            End If
        End If

    End Sub

    Private Sub frmUserSettings_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        pnlMain.Left = (Me.Width - pnlMain.Width) / 2
        pnlMain.Top = (Me.Height - pnlMain.Height) / 2
    End Sub


    Private Sub pboxMinimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub pboxRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxRestore.Click
        If (Me.WindowState = FormWindowState.Maximized) Then
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.WindowState = FormWindowState.Normal
            pboxMaximize.Show()
            pboxRestore.Hide()
        End If
    End Sub

    Private Sub pboxMaximize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxMaximize.Click
        If (Me.WindowState = FormWindowState.Normal) Then
            Me.WindowState = FormWindowState.Maximized
            pboxMaximize.Hide()
            pboxRestore.Show()
        End If
    End Sub

    Private Sub pboxClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxClose.Click
        Me.Close()
    End Sub


   
End Class