Public Class frmMyData

    Private Sub frmMyData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblIP.Text = Form1.GetIPAddress
        lblConnection.Text = My.Computer.Network.IsAvailable
    End Sub
End Class