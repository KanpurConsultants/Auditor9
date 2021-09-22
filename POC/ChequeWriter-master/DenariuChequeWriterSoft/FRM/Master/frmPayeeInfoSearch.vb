Public Class frmPayeeInfoSearch
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

        If txtPayeeName.Text.Trim <> "" Then
            WQry += "PayeeName Like '"
            If rbnSuffixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += txtPayeeName.Text
            If rbnPrefixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += "'"
        End If

        dgvPayee.DataSource = db.Payee.Grid("PayeeName,AddressLine1,AddressLine2,MobileNo,EmailId", WQry)
        dgvPayee.Columns("PayeeName").Width = 215
        dgvPayee.Columns("AddressLine1").Width = 250
        dgvPayee.Columns("AddressLine2").Width = 250
        dgvPayee.Columns("MobileNo").Width = 150
        dgvPayee.Columns("EmailId").Width = 150

        dgvPayee.Columns("PayeeName").HeaderText = "PAYEE"
        dgvPayee.Columns("AddressLine1").HeaderText = "ADDRESS1"
        dgvPayee.Columns("AddressLine2").HeaderText = "ADDRESS2"
        dgvPayee.Columns("MobileNo").HeaderText = "MOBILE NO"
        dgvPayee.Columns("EmailId").HeaderText = "EMAIL ID"

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewSearch()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MsgDialog.ShowMsgDlg("Do you want to Exit?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
    End Sub

    Private Sub cmbCustomerName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPayeeName.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayeeName.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub

    Private Sub dgvCustomerSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPayee.DoubleClick
        Try
            frmPayeeInfo.txtSearchbox.Text = dgvPayee.SelectedRows(0).Cells(0).Value()
            frmPayeeInfo.ViewForm()
            frmPayeeInfo.Show()
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlContent.Resize
        Dim l, t As Integer
        l = (pnlContent.Width - Panel4.Width) / 2
        If l < 0 Then l = 0
        t = (pnlContent.Height - Panel4.Height) / 2
        If t < 0 Then t = 0
        Panel4.Left = l
    End Sub
End Class