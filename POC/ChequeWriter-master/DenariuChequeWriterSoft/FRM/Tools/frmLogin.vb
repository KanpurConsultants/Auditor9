Public Class frmLogin
    Dim IsLogined As Boolean = False

    Private Sub frmLogin_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        cbCompanyName.Text = CompName
    End Sub

    Private Sub frmLogin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If IsLogined = False Then
        '    e.Cancel = True
        '    If MsgDialog.ShowMsgDlg("Do you want to Exit?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then End
        'End If        
    End Sub
    Private Sub frmLogin_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If MsgDialog.ShowMsgDlg("Do you want to Exit?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then End
        End If
    End Sub

    Sub Login()
        lblInvalidUserName.Text = ""
        If cbCompanyName.Text = "" Then
            lblInvalidUserName.Text = "Select Company Name"
            cbCompanyName.Focus()
        ElseIf txtUserName.Text = "" Then
            lblInvalidUserName.Text = "User Name Missing"
            txtUserName.Focus()
        ElseIf txtPassword.Text = "" Then
            lblInvalidUserName.Text = "Password Missing"
            txtPassword.Focus()
        Else
            Dim CompanyId As String = db.CompanyDetails.IdByCode(cbCompanyName.Text)
            Dim Arr As New ArrayList
            Arr = db.UserAccount.Row("*", String.Format("UserName='{0}' and Password='{1}' and CompanyName='{2}'", txtUserName.Text, txtPassword.Text, CompanyId))

            If Arr.Count > 0 Then
                If Arr(2) = txtUserName.Text And Arr(3) = txtPassword.Text And Arr(5) = CompanyId Then
                    UserId = Arr(0)
                    CompName = cbCompanyName.Text
                    frmMaster.Show()
                    frmMaster.BringToFront()
                    IsLogined = True
                    Me.Close()

                Else
                    lblInvalidUserName.Text = "Invalid UserName or Password"
                End If
            Else
                'lblInvalid.Text = "Invalid User"
                lblInvalidUserName.Text = "Invalid UserName or Password"
            End If

        End If

        If lblInvalidUserName.Text = "" Then
            lblInvalidUserName.Visible = False
        Else
            lblInvalidUserName.Visible = True
        End If
    End Sub

    Private Sub txtUserName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserName.KeyPress
        If e.KeyChar = vbLf Or e.KeyChar = vbCr Or e.KeyChar = vbCrLf Then
            txtPassword.Focus()
        End If
    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim CName As String = db.CompanyDetails.CodeById(1)


        While Not CheckLicense()
            frmKeyRegistration.StartPosition = FormStartPosition.CenterScreen
            If frmKeyRegistration.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Me.Close()
                Exit While
            End If
            MsgBox("You registerd successfully")
        End While

        'If CName = "" Then
        '    frmCompanyDetails.StartPosition = FormStartPosition.CenterScreen
        '    frmCompanyDetails.ShowDialog()
        '    MsgBox("You Register your Company Succesfully. Please restart your Application")
        '    End
        'Else



        'End If

        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)

        'Me.Icon = DenariuChequeWriterSoft.My.Resources.
        'List_Datas(cmbCompanyName, db.CompanyDetails.Codes())

        cbCompanyName.ForeColor = Color.Linen
    End Sub

    Private Sub frmLogin_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        pnlMain.Left = (PnlMaster.Width - pnlMain.Width) / 2
        pnlMain.Top = (PnlMaster.Height - pnlMain.Height) / 2
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Login()
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
        End If

    End Sub

     

    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If (e.Control And e.KeyValue = 86) Then
            Clipboard.Clear()
            MsgBox("Can't Paste Here", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub txtPassword_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtPassword.MouseDown
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            Clipboard.Clear()
            MsgBox("Can't Paste Here", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub btnCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompany.Click
        CompName = ""
        Dim frm As New frmCompanyDetails()
        Me.Hide()
        frm.ShowDialog()
    End Sub

    Private Sub cbCompanyName_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCompanyName.Enter
        List_Datas(cbCompanyName, db.CompanyDetails.Codes())
    End Sub

    Private Sub txtUserName_TextChanged(sender As Object, e As EventArgs) Handles txtUserName.TextChanged

    End Sub
End Class