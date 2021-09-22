Imports DenariuChequeWriterSoft.My.Resources
Imports System.Text.RegularExpressions

Public Class frmPayeeInfo
    Dim tb As JISTable = db.Payee

    Function FormValidate() As Boolean
        Dim RValue As Boolean = True
        If txtPayeeName.Text.Trim() = "" Then
            'ErrorProvider1.SetError(txtPayeeName, "Please Enter the Payee Name")
            MsgBox(Message.Payee_Name_Required, MsgBoxStyle.Information)
            txtPayeeName.Focus()
            RValue = False
        ElseIf tb.State = "New" And tb.Data("PayeeId", String.Format("PayeeName='{0}' and CompanyId='{1}'", txtPayeeName.Text, db.CompanyDetails.IdByCode(CompName))) <> "" Then
            MsgBox("Payee Name Already Exist", MsgBoxStyle.Information)
            'ErrorProvider1.SetError(txtPayeeName, txtPayeeName.Text + " is Already Exist. Please Enter the New Payee Name")
            txtPayeeName.Focus()
            RValue = False
           
        Else
            ErrorProvider1.SetError(txtPayeeName, "")
        End If
        Return RValue
    End Function

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.Enter, txtPhoneNo.Enter, txtPayeeName.Enter, txtMobileNo.Enter, txtEMailId.Enter, txtAddressLine2.Enter, txtAddressLine1.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.name = "txtSearchBox" Then List_Datas(txtSearchbox, tb.UserCodes())
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.Leave, txtPhoneNo.Leave, txtPayeeName.Leave, txtMobileNo.Leave, txtEMailId.Leave, txtAddressLine2.Leave, txtAddressLine1.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
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

    Private Sub frmPayeeInfo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Icon = My.Resources.Plain
    End Sub

    Private Sub frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tb.SetControls(txtId, txtPayeeName, txtAddressLine1, txtAddressLine2, txtPhoneNo, txtMobileNo, txtEMailId, txtCompanyId)
        SetFormStyle(Me)
        NewForm()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        FormShowView(frmPayeeInfoReport)
        
         
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
            If MsgDialog.ShowMsgDlg(Message.Payee_Exit, Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
        End If

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        NewForm()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MsgDialog.ShowMsgDlg(Message.Payee_Exit, Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
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
        txtCompanyId.Text = db.CompanyDetails.IdByCode(CompName)
        ErrorProvider1.Clear()
    End Sub

    Sub ViewForm()
        If txtSearchbox.Text.Trim() = "" Then
            txtSearchbox.Focus()
        Else
            Dim str As String = ""
            str = tb.ViewRecordByCode(txtSearchbox.Text)
            If str <> "" Then MsgDialog.ShowMsgDlg(str, Me.Text, "I") Else txtSearchbox.Text = ""
            ErrorProvider1.Clear()
        End If
    End Sub

    Sub SaveForm()
        If FormValidate() Then
            Dim str As String = ""
            str = tb.SaveRecord()

            If str.ToLower.Trim() = "inserted" Or str.ToLower.Trim() = "updated" Then
                MsgDialog.ShowMsgDlg(Message.Payee_Saved, Me.Text, "I")
                NewForm()
            End If

        End If


    End Sub

    Sub DeleteForm()
        If tb.State = "New" Then Exit Sub
        If MsgDialog.ShowMsgDlg(Message.Payee_Delete_Record, Me.Text, "Q") <> Windows.Forms.DialogResult.Yes Then Exit Sub

        Dim str As String
        str = tb.DeleteRecord()
        MsgDialog.ShowMsgDlg(str, "DELETE", "I")
        NewForm()
    End Sub

    Private Sub btnSearch1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch1.Click
        FormShowView(frmPayeeInfoSearch)

    End Sub

    Private Sub txtPhoneNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhoneNo.KeyPress, txtMobileNo.KeyPress
        'If (Asc(e.KeyChar) = 124 Or Asc(e.KeyChar) = 36 Or Asc(e.KeyChar) = 94 Or Char.IsPunctuation(e.KeyChar) = True Or Char.IsLetter(e.KeyChar) = True) And Asc(e.KeyChar) <> 8 Then
        '    e.Handled = True
        'End If
        If Not e.KeyChar = "+" Then
            e.KeyChar = NumericCheckwithsingledot(sender, e.KeyChar, False)
        End If
    End Sub
    Private Sub frmPayeeInfo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
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

    Private Sub frmPayeeInfo_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlContent.Resize
        Dim l, t As Integer
        l = (pnlContent.Width - pnlForm.Width) / 2
        If l < 0 Then l = 0
        t = (pnlContent.Height - pnlForm.Height) / 2
        If t < 0 Then t = 0
        pnlForm.Left = l
    End Sub
   
    Private Sub txtPayeeName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPayeeName.KeyPress
        'If (Asc(e.KeyChar) = 124 Or Asc(e.KeyChar) = 36 Or Asc(e.KeyChar) = 94 Or Char.IsPunctuation(e.KeyChar) = True Or Char.IsLetter(e.KeyChar) = False) And Asc(e.KeyChar) <> 8 Then
        '    e.Handled = False
        'End If
        'If Char.IsLetter(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub
End Class