Public Class frmPayeeInfoReport

    Private Sub frmPayeeInfoReport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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
        ViewSearch()
    End Sub

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayeeName.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
    End Sub
    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayeeName.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewSearch()
    End Sub
    Sub ViewSearch()

        Dim WQry As String = String.Format("CompanyID='{0}'", db.CompanyDetails.IdByCode(CompName))

        If txtPayeeName.Text.Trim <> "" Then
            'If WQry <> "" Then
            '    WQry += " and "
            '    WQry += "PayeeName Like '"
            '    If rbnSuffixCompany.Checked = True Or rbnInfixCompany.Checked = True Then WQry += "%"
            '    WQry += txtPayeeName.Text
            '    If rbnPrefixCompany.Checked = True Or rbnInfixCompany.Checked = True Then WQry += "%"
            '    WQry += "'"
            'End If
            WQry += "PayeeName Like '"
            If rbnSuffixCompany.Checked = True Or rbnInfixCompany.Checked = True Then WQry += "%"
            WQry += txtPayeeName.Text
            If rbnPrefixCompany.Checked = True Or rbnInfixCompany.Checked = True Then WQry += "%"
            WQry += "'"
        End If

        Dim ds As New DataSet
        Dim DTCompanyDetails As New DataTable()
        DTCompanyDetails = db.Payee.Grid("*", WQry).ToTable("Payee")

        ds.Clear()
        ds.Tables.Add(DTCompanyDetails)
        ReportViewMain(CrystalReportViewer1, "cryPayeeDetails.rpt", ds, Nothing)
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SendKeys.Send("{ESCAPE}")
    End Sub

    Private Sub frmPayeeInfoReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim l, t As Integer
        l = (pnlHeader.Width - pnlToolBar.Width) / 2
        If l < 0 Then l = 0
        t = (pnlHeader.Height - pnlToolBar.Height) / 2
        If t < 0 Then t = 0
        pnlToolBar.Left = l
    End Sub
End Class