Public Class frmPrintEnvelopePrint

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEnvelopeName.Enter, cmbEnvelopeSize.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.name = cmbEnvelopeSize.Name Then List_Datas(cmbEnvelopeSize, db.EnvelopeSize.Column("EnvelopeName", String.Format("companyid='{0}'", db.CompanyDetails.IdByCode(CompName))))
        If sender.name = cmbEnvelopeName.Name Then List_Datas(cmbEnvelopeName, db.EnvelopeName.Column("EnvelopeName", String.Format("companyid='{0}'", db.CompanyDetails.IdByCode(CompName))))

        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub
    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEnvelopeName.Leave, cmbEnvelopeSize.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub

    Private Sub frmPrintEnvelopePrint_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Icon = My.Resources.Plain
    End Sub

    Private Sub frmPrintEnvelopePrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFormStyle(Me)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        SendKeys.Send("{ESCAPE}")
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If cmbEnvelopeSize.Text.Trim() = "" Then
            MsgBox("Select Envelope Name")
        ElseIf Not db.EnvelopeSize.ExistCode(cmbEnvelopeSize.Text.Trim()) Then
            MsgBox("Select Valid Envelope Name")
        ElseIf cmbEnvelopeName.Text.Trim() = "" Then
            MsgBox("Select Envelope Address")
        ElseIf Not db.EnvelopeName.ExistCode(cmbEnvelopeName.Text.Trim()) Then
            MsgBox("Select valid Envelope Address")
        Else
            Dim ESId As String = db.EnvelopeSize.IdByCode(cmbEnvelopeSize.Text)
            Dim ENId As String = db.EnvelopeName.IdByCode(cmbEnvelopeName.Text)
            Dim frm As New frmEnvelopePrinting
            frm.txtEnvelopeId.Text = ENId
            frm.txtESId.Text = ESId
            FormShow(frm)
        End If
    End Sub

    Private Sub btnEnvelopeSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnvelopeSelect.Click
        ' If frmPrintEnvelopeNew.WindowState = FormWindowState.Minimized Or frmPrintEnvelopeNew.WindowState = FormWindowState.Maximized Then frmPrintEnvelopeNew.WindowState = FormWindowState.Normal
        FormShow(frmPrintEnvelopeNew)
    End Sub

    Private Sub btnAddressSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddressSelect.Click
        'If frmEnvelopeSizeNew.WindowState = FormWindowState.Minimized Or frmEnvelopeSizeNew.WindowState = FormWindowState.Maximized Then frmEnvelopeSizeNew.WindowState = FormWindowState.Normal
        FormShow(frmEnvelopeSizeNew)
    End Sub

    Private Sub frmPrintEnvelopePrint_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlContent.Resize
        Dim l, t As Integer
        l = (pnlContent.Width - pnlForm.Width) / 2
        If l < 0 Then l = 0
        t = (pnlContent.Height - pnlForm.Height) / 2
        If t < 0 Then t = 0
        pnlForm.Left = l
    End Sub
End Class