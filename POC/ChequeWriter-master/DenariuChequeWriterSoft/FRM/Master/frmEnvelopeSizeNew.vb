Imports System.IO
Imports DenariuChequeWriterSoft.My.Resources
Imports System.Data.SqlClient
Public Class frmEnvelopeSizeNew
    Dim tb As JISTable = db.EnvelopeName

    Private Sub frmEnvelopeSizeNew_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Icon = My.Resources.Plain
    End Sub
    Private Sub frmEnvelopeSizeNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFormStyle(Me)
       
    End Sub
    Function FormValidate() As Boolean
        Dim RValue As Boolean = True
        If txtEnvelopeName.Text.Trim() = "" Then
            ErrorProvider1.SetError(txtEnvelopeName, "Please Enter the Envelope Name")
            txtEnvelopeName.Focus()
            RValue = False
        ElseIf tb.State = "New" And tb.Data("EnvelopeId", String.Format("EnvelopeName='{0}' and CompanyId='{1}'", txtEnvelopeName.Text, db.CompanyDetails.IdByCode(CompName))) <> "" Then
            ErrorProvider1.SetError(txtEnvelopeName, txtEnvelopeName.Text + " is Already Exist. Please Enter the New Envelope Name")
            txtEnvelopeName.Focus()
            RValue = False

        ElseIf txtRecipientName.Text.Trim() = "" Then
            ErrorProvider1.SetError(txtRecipientName, "Please Enter the Recipient Name")
            txtRecipientName.Focus()
            RValue = False

        ElseIf txtAddressLine1.Text.Trim() = "" Then
            ErrorProvider1.SetError(txtAddressLine1, "Please Enter the Address")
            txtAddressLine1.Focus()
            RValue = False
        ElseIf txtPostalCode.Text.Trim() = "" Then
            ErrorProvider1.SetError(txtPostalCode, "Please Enter the PostalCode")
            txtPostalCode.Focus()
            RValue = False

        Else
            ErrorProvider1.SetError(txtEnvelopeName, "")

        End If

        Return RValue
    End Function

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEnvelopeName.Enter, txtAddressLin2.Enter, txtPostalCode.Enter, txtCity.Enter, txtAddressLine1.Enter, txtRecipientName.Enter, txtCountry.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEnvelopeName.Leave, txtAddressLin2.Leave, txtPostalCode.Leave, txtCity.Leave, txtAddressLine1.Leave, txtRecipientName.Leave, txtCountry.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
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
        tb.SetControls(txtId, txtEnvelopeName, txtRecipientName, txtAddressLine1, txtAddressLin2, txtPostalCode, txtCity, txtCountry, txtCompanyId)
        SetFormStyle(Me)
        NewForm()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        FormShowView(frmEnvelopeNewReport)
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
        SendKeys.Send("{ESCAPE}")
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
        txtEnvelopeName.Focus()
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
            If str.Trim() <> "" Then MsgBox(Message.Company_Updated)
            NewForm() 'MsgDialog.ShowMsgDlg(str, Me.Text, "I")

            If str.ToLower.Trim() = "Inserted" Or str.ToLower.Trim() = "updated" Then NewForm()
        End If
    End Sub

    Sub DeleteForm()
        If tb.State = "New" Then Exit Sub
        If MsgDialog.ShowMsgDlg("Do you Delete this Record?", Me.Text, "Q") <> Windows.Forms.DialogResult.Yes Then Exit Sub
        Dim str As String
        str = tb.DeleteRecord()
        MsgDialog.ShowMsgDlg(str, "DELETE", "I")
        NewForm()
    End Sub
    Private Sub btnSearch1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch1.Click
        FormShowView(frmEnvelopeNewSearch)
    End Sub

    Private Sub frmMain_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlContent.Resize
        Dim l, t As Integer
        l = (pnlContent.Width - pnlForm.Width) / 2
        If l < 0 Then l = 0
        t = (pnlContent.Height - pnlForm.Height) / 2
        If t < 0 Then t = 0
        pnlForm.Left = l
    End Sub
    Private Sub txtPostalCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPostalCode.KeyPress
        'If (Asc(e.KeyChar) = 124 Or Asc(e.KeyChar) = 36 Or Asc(e.KeyChar) = 94 Or Char.IsPunctuation(e.KeyChar) = True Or Char.IsLetter(e.KeyChar) = True) And Asc(e.KeyChar) <> 8 Then
        '    e.Handled = True
        'End If
        If Not e.KeyChar = "+" Then
            e.KeyChar = NumericCheckwithsingledot(sender, e.KeyChar, False)
        End If
    End Sub
End Class