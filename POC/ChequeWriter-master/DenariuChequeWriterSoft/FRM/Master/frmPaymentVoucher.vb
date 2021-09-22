Public Class frmPaymentVoucher

    Private Sub frm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.Return Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyValue = Keys.Escape Then
            If MsgDialog.ShowMsgDlg("Do you want to Exit?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
        End If
    End Sub

    Private Sub frm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(13) Then e.KeyChar = ChrW(0)
    End Sub

    Private Sub frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetFormStyle(Me)
        dgvChequeIssue.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvChequeIssue.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
    End Sub

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayName.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
    End Sub
    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayName.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewSearch()
    End Sub
    Sub ViewSearch()
        Dim dtFrom As Date = dtpChequeDateFrom.Value
        Dim dtTo As Date = dtpChequeDateTo.Value

        dtFrom = New Date(dtFrom.Year, dtFrom.Month, dtFrom.Day, 0, 0, 0)
        dtTo = New Date(dtTo.Year, dtTo.Month, dtTo.Day, 23, 59, 59)

        Dim PType As String = ""

        Dim WQry As String

        WQry = String.Format("{0} between  '{1:MM/dd/yyyy}' And '{2:MM/dd/yyyy}' and CompanyName='{3}'", If(rbnIssueDate.Checked, "IssueDate", "ChequeDate"), dtFrom, dtTo, db.CompanyDetails.IdByCode(CompName))

        If txtPayName.Text.Trim <> "" Then

            If WQry <> "" Then
                WQry += " and "
                WQry += "PayeeName Like '"
                If rbnSuffixPayee.Checked = True Or rbnInfixPayee.Checked = True Then WQry += "%"
                WQry += txtPayName.Text
                If rbnInfixPayee.Checked = True Or rbnInfixPayee.Checked = True Then WQry += "%"
                WQry += "'"
            End If

        End If

        Dim DV As New DataView
        DV = db.PrintData.Grid("VoucherNo,BankName,Particulars,PayeeName,ChequeDate,ChequeNo,IssueDate,ChequeStatus,Amount", WQry, "VoucherNo desc")
        dgvChequeIssue.Rows.Clear()
        For i As Integer = 0 To DV.Count - 1
            dgvChequeIssue.Rows.Add()
            dgvChequeIssue.Item("VoucherNo", i).Value = DV.Item(i)("VoucherNo").ToString()
            dgvChequeIssue.Item("Particulars", i).Value = DV.Item(i)("Particulars").ToString()
            dgvChequeIssue.Item("BankName", i).Value = DV.Item(i)("BankName").ToString()
            dgvChequeIssue.Item("PayeeName", i).Value = DV.Item(i)("PayeeName").ToString()
            dgvChequeIssue.Item("ChequeDate", i).Value = DV.Item(i)("ChequeDate")
            dgvChequeIssue.Item("ChequeDate", i).Value = DV.Item(i)("ChequeNo")
            dgvChequeIssue.Item("IssueDate", i).Value = DV.Item(i)("IssueDate")
            dgvChequeIssue.Item("ChequeStatus", i).Value = DV.Item(i)("ChequeStatus")
            dgvChequeIssue.Item("Amount", i).Value = DV.Item(i)("Amount")
        Next
        dgvChequeIssue.Columns("IssueDate").DefaultCellStyle.Format = "dd/MM/yyyy"
        dgvChequeIssue.Columns("ChequeDate").DefaultCellStyle.Format = "dd/MM/yyyy"
        dgvChequeIssue.Columns("Amount").DefaultCellStyle.Format = "0.00"
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SendKeys.Send("{ESCAPE}")
    End Sub
    Private Sub frmChequeIssueReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlHeader.Resize
        Dim l, t As Integer
        l = (pnlHeader.Width - pnlToolBar.Width) / 2
        If l < 0 Then l = 0
        t = (pnlHeader.Height - pnlToolBar.Height) / 2
        If t < 0 Then t = 0
        pnlToolBar.Left = l
    End Sub

    Private Sub frmPaymentVoucher_InputLanguageChanged(sender As Object, e As InputLanguageChangedEventArgs) Handles Me.InputLanguageChanged
        Me.Icon = My.Resources.Plain
    End Sub

    Private Sub dgvChequeIssue_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChequeIssue.CellValidated

    End Sub

    Private Sub dgvChequeIssue_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChequeIssue.CellValueChanged
        If dgvChequeIssue.Focused Then
            Dim tot As Double = 0
            For i As Integer = 0 To dgvChequeIssue.Rows.Count - 1
                If dgvChequeIssue.Item(0, i).Value = True Then
                    tot += dgvChequeIssue.Item("Amount", i).Value
                End If
            Next
            lblTotal.Text = String.Format("Total : {0:0.00}", tot)
        End If

    End Sub

    Private Sub dgvChequeIssue_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvChequeIssue.CurrentCellDirtyStateChanged
        If dgvChequeIssue.IsCurrentCellDirty Then
            dgvChequeIssue.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
End Class