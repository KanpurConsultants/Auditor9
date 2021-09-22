Public Class MDIMain

    Private Sub MDIMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim mCount As Integer = 0
        If e.KeyCode = Keys.Escape Then
            For Each ChildForm As Form In Me.MdiChildren
                mCount = mCount + 1
            Next

            If mCount = 0 Then
                If MsgBox("Do You Want to Exit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'End
                End If
            End If
        End If
    End Sub

    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If AgL Is Nothing Then
        '    If FOpenIni(StrPath + IniName, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
        'FOpenConnection("1", "1")
        '    End If

        '    FOpenConnection("1", "1")

        'Dim ClsObj As New ClsMain(AgL)
        'ClsObj.UpdateTableStructure()
        'ClsObj = Nothing

        'End If

        If AgL Is Nothing Then
            If FOpenIni(StrPath + IniName, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
                AgIniVar.FOpenConnection("2", "1", False)
            End If

            AgL.PubDivCode = "D"
            AgL.PubDivName = AgL.Dman_Execute("Select D.Div_Name From Division D Where D.Div_Code = '" & AgL.PubDivCode & "' ", AgL.GCn).ExecuteScalar


            Dim ClsObj As New ClsMain(AgL)
            Dim ClsObjAgTemplate As New AgTemplate.ClsMain(AgL)
            'ClsObj.UpdateTableStructure()
            'ClsObj = Nothing
        End If
    End Sub



    Private Sub MnuReports_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        Dim FrmObj As AgLibrary.RepFormGlobal
        Dim CFOpen As New ClsFunction

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text, False)
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If
    End Sub


    Private Sub MnuMaster_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) _
                                                                    Handles MnuStructure.DropDownItemClicked, _
                                                                    MnuUser.DropDownItemClicked, _
                                                                    MnuDatabase.DropDownItemClicked, _
                                                                    MnuCustomFields.DropDownItemClicked, _
                                                                    MnuVoucherType.DropDownItemClicked, _
                                                                    MnuCompanyHierarchy.DropDownItemClicked
        Dim FrmObj As Form
        Dim CFOpen As New ClsFunction

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text)
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If
    End Sub
End Class
