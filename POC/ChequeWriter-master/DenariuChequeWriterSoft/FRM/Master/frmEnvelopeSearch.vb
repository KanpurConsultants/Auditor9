Public Class frmEnvelopeSearch
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
        'SetFormStyle(Me)
        Dim dv As New DataView
        ViewSearch()
    End Sub

    Sub ViewSearch()
        Dim WQry As String = ""

        If txtEnvelopeName.Text.Trim <> "" Then
            WQry += "EnvelopeName Like '"
            If rbnSuffixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += txtEnvelopeName.Text
            If rbnPrefixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += "'"
        End If

        dgvEnvelope.DataSource = db.EnvelopeName.Grid("EnvelopeName,RecipientName,Address1,Address2,Zip,City,Country", WQry)
        dgvEnvelope.Columns("EnvelopeName").Width = 200
        dgvEnvelope.Columns("RecipientName").Width = 200
        dgvEnvelope.Columns("Address1").Width = 200
        dgvEnvelope.Columns("Address2").Width = 200
        dgvEnvelope.Columns("Zip").Width = 100
        dgvEnvelope.Columns("City").Width = 150
        dgvEnvelope.Columns("Country").Width = 150

        dgvEnvelope.Columns("EnvelopeName").HeaderText = "ENVELOPE NAME"
        dgvEnvelope.Columns("RecipientName").HeaderText = "RECIPIENT"
        dgvEnvelope.Columns("Address1").HeaderText = "ADDRESS1"
        dgvEnvelope.Columns("Address2").HeaderText = "ADDRESS2"
        dgvEnvelope.Columns("Zip").HeaderText = "ZIP"
        dgvEnvelope.Columns("City").HeaderText = "CITY"
        dgvEnvelope.Columns("Country").HeaderText = "COUNTRY"
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
            frmEnvelopeSizeNew.txtSearchbox.Text = dgvEnvelope.SelectedRows(0).Cells(0).Value()
            frmEnvelopeSizeNew.ViewForm()
            frmEnvelopeSizeNew.Show()
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmUnitsOfMeasurementSearch_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim l As Integer
        l = (pnlHeader.Width - pnlToolBar.Width) / 2
        If l < 0 Then l = 0
        pnlToolBar.Left = l
    End Sub

End Class