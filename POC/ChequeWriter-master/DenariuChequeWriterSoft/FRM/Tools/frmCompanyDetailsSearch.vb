Public Class frmCompanyDetailsSearch
    Private Sub frmUnitsOfMeasurementSearch_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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
        Dim WQry As String = ""

        If txtCompanyName.Text.Trim <> "" Then
            WQry += "CompanyName Like '"
            If rbnSuffixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += txtCompanyName.Text
            If rbnPrefixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += "'"
        End If

        dgvCompany.DataSource = db.CompanyDetails.Grid("CompanyName,AddressLine1,AddressLine2,MobileNo,EmailId", WQry)
        dgvCompany.Columns("CompanyName").Width = 215
        dgvCompany.Columns("AddressLine1").Width = 250
        dgvCompany.Columns("AddressLine2").Width = 250
        dgvCompany.Columns("MobileNo").Width = 150
        dgvCompany.Columns("EmailId").Width = 150

        dgvCompany.Columns("CompanyName").HeaderText = "COMPANY"
        dgvCompany.Columns("AddressLine1").HeaderText = "ADDRESS1"
        dgvCompany.Columns("AddressLine2").HeaderText = "ADDRESS2"
        dgvCompany.Columns("MobileNo").HeaderText = "MOBILE NO"
        dgvCompany.Columns("EmailId").HeaderText = "EMAIL ID"

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewSearch()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MsgDialog.ShowMsgDlg("Do you want to Exit?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
    End Sub

    Private Sub cmbCustomerName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompanyName.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompanyName.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub

    Private Sub dgvCustomerSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCompany.DoubleClick
        Try
            frmCompanyDetails.txtSearchbox.Text = dgvCompany.SelectedRows(0).Cells(0).Value()
            frmCompanyDetails.ViewForm()
            frmCompanyDetails.Show()
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