Public Class frmEnvelopeSizeSearch
    Private Sub frmUnitsOfMeasurementSearch_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Icon = My.Resources.Plain
        ViewSearch()
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

    Private Sub frm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

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
        SetFormStyle(Me)
        Dim dv As New DataView
        ViewSearch()
    End Sub

    Sub ViewSearch()
        Dim WQry As String = String.Format("CompanyID='{0}'", db.CompanyDetails.IdByCode(CompName))

        If txtEnvelopeName.Text.Trim <> "" Then
            WQry += "EnvelopeName Like '"
            If rbnSuffixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += txtEnvelopeName.Text
            If rbnPrefixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += "'"
        End If

        dgvEnvelope.DataSource = db.EnvelopeSize.Grid("EnvelopeName,FromLeft,FromTop", WQry)

        dgvEnvelope.Columns("EnvelopeName").Width = 250
        dgvEnvelope.Columns("FromLeft").Width = 250
        dgvEnvelope.Columns("FromTop").Width = 250
       

        dgvEnvelope.Columns("EnvelopeName").HeaderText = "ENVELOPE NAME"
        dgvEnvelope.Columns("FromLeft").HeaderText = "FROM LEFT(IN mm)"
        dgvEnvelope.Columns("FromTop").HeaderText = "FROM TOP (IN mm)"
        
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewSearch()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SendKeys.Send("{ESCAPE}")
    End Sub

    Private Sub cmbCustomerName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnvelopeName.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEnvelopeName.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub

    Private Sub dgvCustomerSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvEnvelope.DoubleClick
        Try
            frmPrintEnvelopeNew.txtSearchbox.Text = dgvEnvelope.SelectedRows(0).Cells(0).Value()
            frmPrintEnvelopeNew.ViewForm()
            frmPrintEnvelopeNew.Show()
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmEnvelopeSize_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlHeader.Resize
        Dim l, t As Integer
        l = (pnlHeader.Width - pnlToolBar.Width) / 2
        If l < 0 Then l = 0
        t = (pnlHeader.Height - pnlToolBar.Height) / 2
        If t < 0 Then t = 0
        pnlToolBar.Left = l

    End Sub

    Private Sub frmEnvelopeSizeSearch_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlContent.Resize
        Dim l1, t1 As Integer
        l1 = (pnlContent.Width - dgvEnvelope.Width) / 2
        If l1 < 0 Then l1 = 0
        t1 = (pnlContent.Height - dgvEnvelope.Height) / 2
        If t1 < 0 Then t1 = 0
        dgvEnvelope.Left = l1

    End Sub

End Class