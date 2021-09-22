Public Class frmBankInfoSearch
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
        Dim WQry As String = ""

        If txtBankName.Text.Trim <> "" Then
            WQry += "BankName Like '"
            If rbnSuffixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += txtBankName.Text
            If rbnPrefixCustomer.Checked = True Or rbnInfixCustomer.Checked = True Then WQry += "%"
            WQry += "'"
        End If

        dgvBank.DataSource = db.Bank.Grid("BankName,ChqWidth,ChqHeight,RPTNameLeft,RPTNameTop,RPTDateLeft,RPTDateTop,RPTAmountLeft,RPTAmountTop,RPTWordsLeft,RPTWordsTop", WQry)
        dgvBank.Columns("BankName").Width = 300
        dgvBank.Columns("ChqWidth").Width = 150
        dgvBank.Columns("ChqHeight").Width = 150
        dgvBank.Columns("RPTNameTop").Width = 150
        dgvBank.Columns("RPTDateLeft").Width = 150
        dgvBank.Columns("RPTDateTop").Width = 150
        dgvBank.Columns("RPTAmountLeft").Width = 150
        dgvBank.Columns("RPTAmountTop").Width = 150
        dgvBank.Columns("RPTWordsLeft").Width = 150
        dgvBank.Columns("RPTWordsLeft").Width = 150
        dgvBank.Columns("RPTWordsTop").Width = 150

        dgvBank.Columns("BankName").HeaderText = "BANK NAME"
        dgvBank.Columns("ChqWidth").HeaderText = "CHEQUE WIDTH"
        dgvBank.Columns("ChqHeight").HeaderText = "CHEQUE HEIGHT"
        dgvBank.Columns("RPTNameLeft").HeaderText = "NAME LEFT"
        dgvBank.Columns("RPTNameTop").HeaderText = "NAME TOP"
        dgvBank.Columns("RPTDateLeft").HeaderText = "DATE LEFT"
        dgvBank.Columns("RPTDateTop").HeaderText = "DATE TOP"
        dgvBank.Columns("RPTAmountLeft").HeaderText = "AMOUNT LEFT"
        dgvBank.Columns("RPTAmountTop").HeaderText = "AMOUNT TOP"
        dgvBank.Columns("RPTWordsLeft").HeaderText = "WORDS LEFT"
        dgvBank.Columns("RPTWordsTop").HeaderText = "WORDS TOP"
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewSearch()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SendKeys.Send("{ESCAPE}")
    End Sub

    Private Sub cmbCustomerName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBankName.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBankName.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub

    Private Sub dgvCustomerSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvBank.DoubleClick
        Try
            frmBankInfo.txtSearchbox.Text = dgvBank.SelectedRows(0).Cells(0).Value()
            frmBankInfo.ViewForm()
            frmBankInfo.Show()
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