Public Class FrmMainDisplay
    Dim ClsMaster As Master.ClsMain
    Dim ClsSales As Sales.ClsMain
    Dim ClsAccounts As Accounts.ClsMain
    Dim ClsReportProduction As ReportProduction.ClsMain
    Dim ClsReportPurchase As ReportPurchase.ClsMain
    Dim ClsReportSales As ReportSales.ClsMain
    Dim ClsReportStock As ReportStock.ClsMain

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles BtnClose.Click, BtnUP.Click
        Dim GCnCmd As New OleDb.OleDbCommand
        Select Case sender.name
            Case BtnClose.Name
                End
            Case BtnUP.Name
                Dim MD As New MDIMain
                FAddMenu(MD, "Master")
                MD.StrCurrentModule = "Master"
                MD.Show()
        End Select
    End Sub
    Private Sub FrmMainDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LblCompName.Text = LIPublic.StrPubCompName
        LblUName.Text = LblUName.Text + LIPublic.StrPubUserName
        LblLoginDate.Text = LblLoginDate.Text + LIPublic.StrPubLoginDate
    End Sub
End Class