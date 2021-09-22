Imports CustomControls.IconComboBox

Public Class frmNewAddress

    Private Sub btnImBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImBrowse.Click
        Dim NewAdd As New IconComboItem
        Dim ico As New Icon(My.Application.Info.DirectoryPath & "\Users.ico")
        NewAdd.DisplayText = txtAddress.Text
        NewAdd.ItemImage = ico
        Form1.cmbAddress.Items.Add(NewAdd)
        Me.Close()
    End Sub

    Private Sub GlassButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GlassButton1.Click
        Me.Close()
    End Sub
End Class