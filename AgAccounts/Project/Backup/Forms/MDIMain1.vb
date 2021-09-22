Public Class MDIMain1
    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If AgL Is Nothing Then
            AgL = New AgLibrary.ClsMain()



            If FOpenIni(StrPath + IniName, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
                AgIniVar.FOpenConnection("2", "1")
            End If

            AgIniVar.FOpenConnection("2", "1")
            AgL.PubSiteList = "'1'"
            AgL.PubDivCode = "D"
            'CMain.UpdateTableStructure()
            'Dim x As AgLibrary.ClsMain.LITable()
            'CMain.FDatabase(x)
            'CMain.FExecuteDBScript(x)

            Dim ClsObj As New ClsMain(AgL)
            ClsObj.FDatabase(AgL.PubMdlTable)
            'AgL.FExecuteDBScript(AgL.PubMdlTable, AgL.GCn)
        End If
    End Sub

    Public Function FOpenForm(ByVal StrModuleName, ByVal StrMnuName, ByVal StrMnuText) As Form
        Select Case UCase(StrModuleName)
            Case "ACCOUNTS"
                Dim CFOpen As New ClsFunction
                FOpenForm = CFOpen.FOpen(StrMnuName, StrMnuText)
                CFOpen = Nothing

                'Case "CUSTOMISED"
                '    Dim CFOpen As New Customised.ClsFunction
                '    FOpenForm = CFOpen.FOpen(StrMnuName, StrMnuText)
                '    CFOpen = Nothing

            Case Else
                FOpenForm = Nothing
        End Select
    End Function


    Private Sub MnuProductionPlanningMaster_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) _
                    Handles MnuMaster.DropDownItemClicked, MnuTransactions.DropDownItemClicked, MnuDisplay.DropDownItemClicked, MnuReports.DropDownItemClicked, MnuReportsII.DropDownItemClicked



        Dim FrmObj As Form = Nothing
        Dim CFOpen As New ClsFunction

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text)
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If
    End Sub

End Class
