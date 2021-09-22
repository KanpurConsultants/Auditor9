Imports System.IO
Imports DenariuChequeWriterSoft.My.Resources
Imports System.Data.SqlClient
Public Class frmPrintEnvelopeNew
    Dim tb As JISTable = db.EnvelopeSize

    Function FormValidate() As Boolean
        Dim RValue As Boolean = True
        If txtFromLeft.Text.Trim() = "" Then
            ErrorProvider1.SetError(txtFromLeft, "Please Enter the Left Envelope size")
            txtFromLeft.Focus()
            RValue = False
        Else
            ErrorProvider1.SetError(txtFromLeft, "")
        End If

        If txtFromTop.Text.Trim() = "" Then
            ErrorProvider1.SetError(txtFromTop, "Please Enter the Top Envelope size")
            txtFromTop.Focus()
            RValue = False
        Else
            ErrorProvider1.SetError(txtFromTop, "")
        End If


        If (Val(txtFromLeft.Text) > 0 And Val(txtFromLeft.Text) <= 200) Then
            RValue = True
        Else
            MsgBox("Size Must From 0.1 mm to 150 mm for LEFT")
            RValue = False
        End If

        If (Val(txtFromTop.Text) > 0 And Val(txtFromTop.Text) <= 150) Then
            RValue = True
        Else
            MsgBox("Size Must From 0.1 mm to 150 mm for TOP")
            RValue = False
        End If

        If txtEnvName.Text.Trim() = "" Then
            ErrorProvider1.SetError(txtEnvName, "Please Enter the Envelope Name")
            txtEnvName.Focus()
            RValue = False
        ElseIf tb.State = "New" And tb.Data("EnvelopeSizeId", String.Format("EnvelopeName='{0}' and CompanyId='{1}'", txtEnvName.Text, db.CompanyDetails.IdByCode(CompName))) <> "" Then
            ErrorProvider1.SetError(txtEnvName, txtEnvName.Text + " is Already Exist. Please Enter the New Envelope Name")
            txtEnvName.Focus()
            RValue = False
        Else
            ErrorProvider1.SetError(txtEnvName, "")
        End If

        Return RValue
    End Function

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.Enter, txtEnvName.Enter, txtEnvelopeHeight.Enter, txtFromLeft.Enter, txtEnvelopeWidth.Enter, txtFromTop.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.name = "txtSearchBox" Then List_Datas(txtSearchbox, tb.UserCodes())
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.Leave, txtEnvName.Leave, txtEnvelopeHeight.Leave, txtFromLeft.Leave, txtEnvelopeWidth.Leave, txtFromTop.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub

    Private Sub frmPrintEnvelopeNew_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Icon = My.Resources.Plain
    End Sub

    Private Sub frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetFormStyle(Me)
        tb.SetControls(txtId, txtEnvName, txtFromLeft, txtFromTop, txtEnvelopeWidth, txtEnvelopeHeight, txtCompanyId)
        NewForm()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        FormShowView(frmEnvelopeReport)
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

    Private Sub frm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlContent.Resize
        Dim l, t As Integer
        l = (pnlContent.Width - pnlForm.Width) / 2
        If l < 0 Then l = 0
        t = (pnlContent.Height - pnlForm.Height) / 2
        If t < 0 Then t = 0
        pnlForm.Left = l
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
        ErrorProvider1.Clear()
        txtEnvelopeHeight.Text = "0"
        txtEnvelopeWidth.Text = "0"
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
            NewForm()
            If str.ToLower.Trim() = "Save Sucessfully" Or str.ToLower.Trim() = "updated" Then NewForm()
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
        FormShowView(frmEnvelopeSizeSearch)
    End Sub

    Private Sub txtFromLeft_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFromTop.KeyPress, txtFromLeft.KeyPress
        If (Asc(e.KeyChar) = 124 Or Asc(e.KeyChar) = 36 Or Asc(e.KeyChar) = 94 Or Char.IsPunctuation(e.KeyChar) = True Or Char.IsLetter(e.KeyChar) = True) And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub
End Class