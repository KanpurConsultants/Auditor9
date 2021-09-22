Public Class ClsFunction
    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, ByVal StrModule As String, ByVal StrReportPath As String)
        Dim FrmObj As Form

        Agl.PubReportTitle = ""
        FrmObj = New FrmReportLayout(StrModule, StrSender, StrSenderText, StrReportPath)
        'FrmObj = New FrmReportLayout("Report", "MnuTestLedger", "Test Ledger", "")

        Return FrmObj
    End Function
End Class
