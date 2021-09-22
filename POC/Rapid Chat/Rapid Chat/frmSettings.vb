Public Class frmSettings

    Private Sub btnImBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImBrowse.Click
        Form1.txtName.Text = txtName.Text
        Form1.txtSound.Text = txtSound.Text
        My.Settings.ChkSound = chkSound.CheckState
        Form1.PicClient.ImageLocation = picDisplay.ImageLocation
        Form1.picMypic.ImageLocation = picDisplay.ImageLocation
        Form1.RichTextBox1.Font = New Font(cmbFont.Text, cmbSize.Text)
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub GlassButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GlassButton1.Click
        Me.Close()
    End Sub

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtName.Text = Form1.txtName.Text
        txtSound.Text = My.Application.Info.DirectoryPath & "\Sounds\Notify.wav"
        ofdSound.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        chkSound.CheckState = My.Settings.ChkSound

        For Each font As FontFamily In FontFamily.Families
            cmbFont.Items.Add(font.Name)
        Next font

        cmbFont.Text = Form1.RichTextBox1.Font.Name
        cmbSize.Text = Form1.RichTextBox1.Font.Size

        picDisplay.ImageLocation = Form1.PicClient.ImageLocation
        txtPic.Text = Form1.PicClient.ImageLocation
    End Sub
  
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        ofdSound.ShowDialog()
        If DialogResult.OK Then
            txtSound.Text = ofdSound.FileName
        Else
            txtSound.Text = My.Application.Info.DirectoryPath & "\Sounds\Notify.wav"
        End If
    End Sub

    Private Sub btnPicBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPicBrowse.Click
        Form1.ofdImage.ShowDialog()
        If DialogResult.OK Then
            txtPic.Text = Form1.ofdImage.FileName
            picDisplay.ImageLocation = Form1.ofdImage.FileName
        End If
    End Sub

    Private Sub GlassButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GlassButton2.Click
        colFont.ShowDialog()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        tvwBlockedUser.TopNode.Nodes.Add(txtblockUser.Text)
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        tvwBlockedUser.Nodes.RemoveAt(tvwBlockedUser.SelectedNode.Index)
    End Sub
End Class