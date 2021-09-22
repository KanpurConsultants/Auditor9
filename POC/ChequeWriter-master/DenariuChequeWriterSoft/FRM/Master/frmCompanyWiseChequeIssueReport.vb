Public Class frmCompanyWiseChequeIssueReport
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
        'SetFormStyle(Me)
        Dim dv As New DataView
        ViewSearch()
        Me.Icon = DenariuChequeWriterSoft.My.Resources._256
    End Sub

    Sub ViewSearch()
        Dim dtFrom As Date = dtpChequeDateFrom.Value
        Dim dtTo As Date = DateAdd(DateInterval.Day, 1, dtpChequeDateTo.Value)

        dtFrom = New Date(dtFrom.Year, dtFrom.Month, dtFrom.Day, 0, 0, 0)
        dtTo = New Date(dtTo.Year, dtTo.Month, dtTo.Day, 0, 0, 0)

        Dim PType As String = ""
        Dim SDate As String = dtFrom.ToString()
        Dim EDate As String = dtTo.ToString()

        Dim WQry As String

        Dim BillAmtFrom, BillAmtTo As Long
        BillAmtFrom = 0
        BillAmtTo = 99999999

        If txtBillAmountFrom.Text.Trim <> "" Then BillAmtFrom = Val(txtBillAmountFrom.Text)
        If txtBillAmountTo.Text.Trim <> "" Then BillAmtTo = Val(txtBillAmountTo.Text)

        WQry = String.Format("{0} between  '{1}' And '{2}' and Amount>='{3}' and Amount<='{4}'", If(rbnIssueDate.Checked, "IssueDate", "ChequeDate"), SDate, EDate, BillAmtFrom, BillAmtTo)

        

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

        If txtCompanyName.Text.Trim <> "" Then
            If WQry <> "" Then
                WQry += " and "
                WQry += "CompanyName Like '"
                If rbnSuffixCompany.Checked = True Or rbnInfixCompany.Checked = True Then WQry += "%"
                WQry += txtCompanyName.Text
                If rbnPrefixCompany.Checked = True Or rbnInfixCompany.Checked = True Then WQry += "%"
                WQry += "'"
            End If
        End If

        Dim ds As New DataSet
        Dim DTPrintData As New DataTable()

        ' DTPrint = db.ExecuteQuery(String.Format("Select {0} as PayeeName, {1} as PayDate, {2} as Amount", frmMaster.cmbPayee.Text, frmMaster.dtpDate.Value, frmMaster.txtAmount.Text)).ToTable("PrintData")
        DTPrintData = db.PrintData.Grid("*", WQry).ToTable("PrintData")

        ds.Clear()
        ds.Tables.Add(DTPrintData)
        ReportViewMain(CrystalReportViewer1, "cryChqIssue.rpt", ds, Nothing)

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewSearch()
    End Sub

    Private Sub cmbCustomerName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompanyName.Enter, txtBankName.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompanyName.Leave, txtBankName.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
    End Sub

    Private Sub frmUnitsOfMeasurementSearch_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim l As Integer
        l = (pnlHeader.Width - pnlToolBar.Width) / 2
        If l < 0 Then l = 0
        pnlToolBar.Left = l

    End Sub

    Private Sub pboxMinimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub pboxRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxRestore.Click
        If (Me.WindowState = FormWindowState.Maximized) Then
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.WindowState = FormWindowState.Normal
            pboxMaximize.Show()
            pboxRestore.Hide()
        End If
    End Sub

    Private Sub pboxMaximize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxMaximize.Click
        If (Me.WindowState = FormWindowState.Normal) Then
            Me.WindowState = FormWindowState.Maximized
            pboxMaximize.Hide()
            pboxRestore.Show()
        End If
    End Sub

    Private Sub pboxClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pboxClose.Click
        Me.Close()
    End Sub
End Class