Public Class frmChequeIssueReport

    Private Sub frmChequeIssueReport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Icon = My.Resources.Plain
    End Sub
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
    End Sub

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayName.Enter, txtBankName.Enter, txtCompanyName.Enter, txtBillAmountFrom.Enter, txtBillAmountTo.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
    End Sub
    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayName.Leave, txtBankName.Leave, txtCompanyName.Leave, txtBillAmountFrom.Leave, txtBillAmountTo.Leave
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

        Dim BillAmtFrom, BillAmtTo As Long
        BillAmtFrom = 0
        BillAmtTo = 999999999999

        If txtBillAmountFrom.Text.Trim <> "" Then BillAmtFrom = Val(txtBillAmountFrom.Text)
        If txtBillAmountTo.Text.Trim <> "" Then BillAmtTo = Val(txtBillAmountTo.Text)


        WQry = String.Format("{0} between  '{1:MM/dd/yyyy}' And '{2:MM/dd/yyyy}' and Amount>='{3}' and Amount<='{4}' and CompanyName='{5}'", If(rbnIssueDate.Checked, "IssueDate", "ChequeDate"), dtFrom, dtTo, BillAmtFrom, BillAmtTo, db.CompanyDetails.IdByCode(CompName))



        If txtPayName.Text.Trim <> "" Then
            If WQry <> "" Then
                WQry += " and "
                WQry += "PayeeName Like '"
                If rbnSuffixCompany.Checked = True Or rbnInfixCompany.Checked = True Then WQry += "%"
                WQry += txtPayName.Text
                If rbnPrefixCompany.Checked = True Or rbnInfixCompany.Checked = True Then WQry += "%"
                WQry += "'"
            End If

        End If

        If txtBankName.Text.Trim <> "" Then
            If WQry <> "" Then
                WQry += " and "
                WQry += "BankName Like '"
                If rbnSuffixBank.Checked = True Or rbnInfixBank.Checked = True Then WQry += "%"
                WQry += txtBankName.Text
                If rbnPrefixBank.Checked = True Or rbnInfixBank.Checked = True Then WQry += "%"
                WQry += "'"
            End If
        End If

        If cmbChequeStatus.Text.Trim <> "" Then          
            WQry += String.Format(" and ChequeStatus='{0}'", cmbChequeStatus.Text)
        End If
        If txtChequeNumber.Text.Trim <> "" Then
            WQry += String.Format(" and ChequeNo='{0}'", txtChequeNumber.Text)
        End If

        Dim ds As New DataSet
        Dim DTPrintData As New DataTable()

        ' DTPrint = db.ExecuteQuery(String.Format("Select {0} as PayeeName, {1} as PayDate, {2} as Amount", frmMaster.cmbPayee.Text, frmMaster.dtpDate.Value, frmMaster.txtAmount.Text)).ToTable("PrintData")
        DTPrintData = db.PrintData.Grid("*", WQry).ToTable("PrintData")

        ds.Clear()
        ds.Tables.Add(DTPrintData)
        ReportViewMain(CrystalReportViewer1, "cryChqIssue.rpt", ds, Nothing)
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

    Private Sub txtChequeNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChequeNumber.KeyPress
        e.KeyChar = NumericCheckwithsingledot(sender, e.KeyChar, False)
    End Sub

End Class