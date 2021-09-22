Public Class FrmCopySettings
    Public mOkClicked As Boolean = False

    Private Sub TxtSiteName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtSiteName.KeyDown
        Dim mQry$ = ""
        If e.KeyCode <> Keys.Enter Then
            If TxtSiteName.AgHelpDataSet Is Nothing Then
                mQry = " Select Code, Name From SiteMast "
                TxtSiteName.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
            End If
        End If
    End Sub

    Private Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Try
            Select Case sender.Name
                Case BtnOk.Name
                    mOkClicked = True

                Case BtnCancel.Name
                    mOkClicked = False
            End Select
            Me.Close()
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
        End If
    End Sub
End Class