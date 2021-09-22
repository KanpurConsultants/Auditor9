Public Class FrmCompanyInput
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Dim mQry As String
        mQry = "Update Company Set Comp_Name='" & TxtDispName.Text & "', City = '" & TxtCity.Text & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub
End Class