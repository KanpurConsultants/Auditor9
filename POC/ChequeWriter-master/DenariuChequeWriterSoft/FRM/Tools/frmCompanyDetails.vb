Imports System.IO
Imports DenariuChequeWriterSoft.My.Resources
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmCompanyDetails
    Dim tb As JISTable = db.CompanyDetails
    Dim fName As String
    Dim cnn As SqlConnection
    Dim connectionString As String

    Private Function ImageToStream(ByVal fileName As String) As Byte()
        Dim stream As New MemoryStream()
        Try
            Dim image As New Bitmap(fileName)
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Catch ex As Exception

        End Try
        Return stream.ToArray()
    End Function
    Function FormValidate() As Boolean
        Dim RValue As Boolean = True
        Dim VNO As String = db.CompanyDetails.Data("isNull(Max(VoucherNo),0)")

        If txtCompanyName.Text.Trim() = "" Then
            MsgBox(Message.Company_Name_Required)
            txtCompanyName.Focus()
            RValue = False
        ElseIf tb.State = "New" And tb.ExistCode(txtCompanyName.Text) Then
            MsgBox(txtCompanyName.Text & " Already Exist")
            txtCompanyName.Focus()
            RValue = False
        ElseIf tb.State = "New" And Val(db.CompanyDetails.Data("isnull(Count(*),0)")) >= 3 Then
            MsgBox("Could Not Create More Than Three Companies. Please Contact Denariusoft Admin", MsgBoxStyle.OkOnly, "ChequeWritePro v1.0")
            txtCompanyName.Focus()
            RValue = False
        ElseIf txtUserName.Text.Trim() = "" Then
            MsgBox("Please enter the User Name")
            txtUserName.Focus()
            RValue = False
        ElseIf txtPassword.Text.Trim() = "" Then
            MsgBox("Please enter the Password", MsgBoxStyle.Exclamation, "")
            txtPassword.Focus()
            RValue = False
        ElseIf txtConfirmPassword.Text.Trim() = "" Then
            MsgBox("Please enter the Confirm Password", MsgBoxStyle.Exclamation, "")
            txtConfirmPassword.Focus()
            RValue = False
        ElseIf validateEmail(txtEMailId.Text) = False Then
            MsgBox("Please enter Valid Maid Id")
            txtEMailId.Focus()
            RValue = False
        ElseIf txtVoucherNo.Text.Trim() = "" And VNO.Contains("") Then
            MsgBox("Please enter the Voucher No")
            txtVoucherNo.Focus()
            RValue = False
        Else
            RValue = True
        End If

        Return RValue

    End Function

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompanyName.Enter, txtPhoneNo.Enter, txtMobileNo.Enter, txtGSTNo.Enter, txtEMailId.Enter, txtAddressLine2.Enter, txtAddressLine1.Enter, txtUserName.Enter, txtPassword.Enter, txtVoucherNo.Enter, txtConfirmPassword.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub txtGSTNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTNo.KeyPress
        e.KeyChar = NumericCheckwithsingledot(sender, e.KeyChar, False)
    End Sub

    Private Sub txtCompanyName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCompanyName.KeyPress

        'If (Asc(e.KeyChar) = Keys.Space OrElse Asc(e.KeyChar) = Keys.Back) OrElse Char.IsLetter(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Function validateEmail(ByVal emailAddress) As Boolean
        ' Dim email As New Regex("^(?<user>[^@]+)@(?<host>.+)$")
        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")
        If email.IsMatch(emailAddress) Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhoneNo.Leave, txtMobileNo.Leave, txtGSTNo.Leave, txtEMailId.Leave, txtCompanyName.Leave, txtAddressLine2.Leave, txtAddressLine1.Leave, txtUserName.Leave, txtPassword.Leave, txtVoucherNo.Leave, txtConfirmPassword.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
        If chkShowPassword.Checked = True Then
            chkShowPassword.Checked = False

        End If
    End Sub

    Private Sub frmCompanyDetails_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Icon = My.Resources.Plain
    End Sub

    Private Sub frmBanks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim ctl As Control = Me.ActiveControl
            If TypeOf ctl Is TextBox Then
                Dim txt As TextBox = DirectCast(ctl, TextBox)
                If txt.Multiline = False Then e.KeyChar = ChrW(0)
            Else
                e.KeyChar = ChrW(0)
            End If
        End If

    End Sub

    Private Sub frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tb.SetControls(txtId, txtCompanyName, txtAddressLine1, txtAddressLine2, txtPhoneNo, txtMobileNo, txtEMailId, txtGSTNo, PictureBox1, CheckBox1, txtVoucherNo)
        SetFormStyle(Me)

        If CompName = "" Then
            Me.StartPosition = FormStartPosition.CenterParent
            NewForm()
        Else
            txtSearchbox.Text = CompName
            ViewForm()
        End If
        Dim tooltip As New ToolTip()
        tooltip.ShowAlways = True
        tooltip.SetToolTip(Me.chkShowPassword, "It's Show Your Typed Password.")
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        FormShow(frmCompanyDetailsReport)
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
            If MsgDialog.ShowMsgDlg(Message.Bank_Exit, Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
        End If

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        NewForm()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MsgDialog.ShowMsgDlg(Message.Bank_Exit, Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveForm()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteForm()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewForm()
    End Sub

    Sub NewForm()
        tb.NewRecord()
        txtCompanyName.Focus()
        ErrorProvider1.Clear()
        txtUserName.Text = ""
        txtPassword.Text = ""

        txtVoucherNo.Enabled = True
        txtFilePath.Text = Application.StartupPath & "\Default.png"
        txtFileName.Text = System.IO.Path.GetFileNameWithoutExtension("Default.png")
        Me.PictureBox1.ImageLocation = txtFilePath.Text
        PictureBox1.Tag = ImageToStream(txtFilePath.Text)
    End Sub

    Sub ViewForm()
        If txtSearchbox.Text.Trim() = "" Then
            txtSearchbox.Focus()
        Else
            Dim str As String = ""
            str = tb.ViewRecordByCode(txtSearchbox.Text)
            If str <> "" Then
                MsgDialog.ShowMsgDlg(str, Me.Text, "I")
            Else
                txtSearchbox.Text = ""
                ErrorProvider1.Clear()
                Dim arr As New ArrayList
                arr = db.UserAccount.Row("UserName,Password", String.Format("CompanyName='{0}'", txtId.Text))
                If arr.Count > 0 Then
                    txtUserName.Text = arr(0)
                    txtPassword.Text = arr(1)
                    txtConfirmPassword.Text = arr(1)
                End If
                
            End If
        End If
        txtVoucherNo.Enabled = False
        'txtCompanyName.Enabled = False
    End Sub

    Sub SaveForm()

        If FormValidate() Then
            Try
                connectionString = GetConStr()
                cnn = New SqlConnection(connectionString)

                cnn.Open()


                If txtPassword.Text.Trim() = txtConfirmPassword.Text.Trim() Then

                    If tb.State = "New" Then
                        Dim cmd As New SqlCommand("insert into CompanyDetails (CompanyId,CompanyName,AddressLine1,AddressLine2,PhoneNo,MobileNo,EmailId,GSTNO,CompanyImg,IsRotate90Degree,voucherno) values (@CompanyId,@CompanyName,@AddressLine1,@AddressLine2,@PhoneNo,@MobileNo,@EmailId,@GSTNO,@CompanyImg,@IsRotate90Degree,@VoucherNo)", cnn)
                        'cmd.Parameters.AddWithValue("@id", id)
                        cmd.Parameters.AddWithValue("@CompanyId", txtId.Text)
                        cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text)
                        cmd.Parameters.AddWithValue("@AddressLine1", txtAddressLine1.Text)
                        cmd.Parameters.AddWithValue("@AddressLine2", txtAddressLine2.Text)
                        cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text)
                        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text)
                        cmd.Parameters.AddWithValue("@EmailId", txtEMailId.Text)
                        cmd.Parameters.AddWithValue("@GSTNO", txtGSTNo.Text)
                        cmd.Parameters.AddWithValue("@CompanyImg", PictureBox1.Tag)
                        cmd.Parameters.AddWithValue("@IsRotate90Degree", False)
                        cmd.Parameters.AddWithValue("@VoucherNo", txtVoucherNo.Text)

                        cmd.ExecuteNonQuery()

                        db.UserAccount.SaveRecord(db.UserAccount.NewId(), "", txtUserName.Text, txtPassword.Text, "admin", txtId.Text)
                        MsgBox(String.Format("Saved Sucessfully"))
                        frmLogin.Show()
                        frmLogin.BringToFront()
                        Me.Close()

                    Else

                        Dim cmd As New SqlCommand("update CompanyDetails set CompanyName=@CompanyName,AddressLine1=@AddressLine1,AddressLine2=@AddressLine2,PhoneNo=@PhoneNo,MobileNo=@MobileNo,EmailId=@EmailId,GSTNO=@GSTNO,CompanyImg=@CompanyImg,IsRotate90Degree=@IsRotate90Degree,VoucherNo=@VoucherNo where CompanyId = @CompanyId", cnn)
                        'cmd.Parameters.AddWithValue("@id", id)
                        cmd.Parameters.AddWithValue("@CompanyId", txtId.Text)
                        cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text)
                        cmd.Parameters.AddWithValue("@AddressLine1", txtAddressLine1.Text)
                        cmd.Parameters.AddWithValue("@AddressLine2", txtAddressLine2.Text)
                        cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text)
                        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text)
                        cmd.Parameters.AddWithValue("@EmailId", txtEMailId.Text)
                        cmd.Parameters.AddWithValue("@GSTNO", txtGSTNo.Text)
                        cmd.Parameters.AddWithValue("@CompanyImg", PictureBox1.Tag)
                        cmd.Parameters.AddWithValue("@IsRotate90Degree", False)


                        cmd.Parameters.AddWithValue("@VoucherNo", txtVoucherNo.Text)

                        cmd.ExecuteNonQuery()
                        Dim cmd1 As New SqlCommand("update userAccount set username='" + txtUserName.Text + "',Password='" + txtPassword.Text + "' where userid=1", cnn)
                        cmd1.ExecuteNonQuery()

                        MsgBox(Message.Company_Updated)
                   
                    End If
                Else
                    MsgBox("Password Miss Match !!", MsgBoxStyle.Exclamation, "Password")
                    txtPassword.Focus()
                End If
        CompName = txtCompanyName.Text
        cnn.Close()
        'Me.Close()
            Catch ex As Exception

        End Try
        End If
        'txtUserName.Text = ""
        'txtPassword.Text = ""
        'NewForm()


    End Sub

    Sub DeleteForm()
        If tb.State = "New" Then Exit Sub
        If MsgDialog.ShowMsgDlg(Message.Company_Delete, Me.Text, "Q") <> Windows.Forms.DialogResult.Yes Then Exit Sub
        Dim str As String
        str = tb.DeleteRecord()
        db.UserAccount.Delete(String.Format("CompanyName='{0}'", txtCompanyName.Text))
        MsgDialog.ShowMsgDlg(str, "DELETE", "I")
        NewForm()
    End Sub

    Private Sub txtMobile1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobileNo.KeyPress, txtPhoneNo.KeyPress
        'If (Asc(e.KeyChar) = 124 Or Asc(e.KeyChar) = 36 Or Asc(e.KeyChar) = 94 Or Char.IsPunctuation(e.KeyChar) = True Or Char.IsLetter(e.KeyChar) = True) And Asc(e.KeyChar) <> 8 Then
        '    e.Handled = True
        'End If
        If Not e.KeyChar = "+" Then
            e.KeyChar = NumericCheckwithsingledot(sender, e.KeyChar, False)
        End If
    End Sub

    Private Sub btnSearch1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch1.Click
        FormShow(frmCompanyDetailsSearch)
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim od As New OpenFileDialog
        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFilePath.Text = od.FileName
            txtFileName.Text = System.IO.Path.GetFileNameWithoutExtension(od.FileName)
            'PictureBox1.Image = New Bitmap(od.FileName)
            Me.PictureBox1.ImageLocation = txtFilePath.Text
            PictureBox1.Tag = ImageToStream(txtFilePath.Text)
        End If
    End Sub

    'Private Sub frmMain_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
    '    pnlMain.Left = (Me.Width - pnlMain.Width) / 2
    '    pnlMain.Top = (Me.Height - pnlMain.Height) / 2
    'End Sub

    Private Sub lblHeading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHeading.Click

    End Sub

    Private Sub frmCompanyDetails_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlContent.Resize
        Dim l, t As Integer
        l = (pnlContent.Width - pnlForm.Width) / 2
        If l < 0 Then l = 0
        t = (pnlContent.Height - pnlForm.Height) / 2
        If t < 0 Then t = 0
        pnlForm.Left = l
    End Sub


    Private Sub txtVoucherNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVoucherNo.KeyPress
        e.KeyChar = NumericCheckwithsingledot(sender, e.KeyChar, False)
    End Sub

    Private Sub frmCompanyDetails_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout

    End Sub

    Private Sub chkShowPassword_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowPassword.CheckStateChanged
        If chkShowPassword.CheckState = CheckState.Checked Then
            txtPassword.PasswordChar = ""
            txtConfirmPassword.PasswordChar = ""
        ElseIf chkShowPassword.CheckState = CheckState.Unchecked Then
            txtPassword.PasswordChar = "*"
            txtConfirmPassword.PasswordChar = "*"
        End If
    End Sub

    
    
End Class