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
        '    If FOpenIni(StrPath + IniName, "SA", "") Then
        '        FOpenConnection("1", "1")
        '    End If
        'End If
    End Sub


    Private Sub MnuReports_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        MnuCityList.Click

        'Dim FrmObj As RepFormGlobal
        'Dim CFOpen As New ClsFunction

        'FrmObj = CFOpen.FOpen(sender.name, sender.Text, False)
        'If FrmObj IsNot Nothing Then
        '    FrmObj.MdiParent = Me
        '    FrmObj.Show()
        '    FrmObj = Nothing
        'End If
    End Sub


End Class
