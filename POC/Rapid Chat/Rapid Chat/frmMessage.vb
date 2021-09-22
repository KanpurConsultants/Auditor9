Public Class frmMessage

    Dim max As Integer = Screen.GetWorkingArea(Me).Height - Me.Height
    Dim x As New Integer
    Dim y As Integer = Screen.GetWorkingArea(Me).Height
    Dim i As Integer = 100

    Private Sub frmMessage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        PicMe.Image = Form1.PicClient.Image


        x = Screen.GetWorkingArea(Me).Width
        y = Screen.GetWorkingArea(Me).Height
        Me.Location = New Point(x, y)
        Timer1.Enabled = True


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        x = Screen.GetWorkingArea(Me).Width - Me.Width
        If y = max Then
            Timer1.Enabled = False
            Timer2.Enabled = True
        Else
            Me.Location = New Point(x, y)
            y = y - 1
        End If
    End Sub

    Private Sub GlassButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GlassButton1.Click
        Me.Close()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If i = 30 Then
            Me.Close()
        Else
            Me.Opacity = i
            i = i - 1
        End If
    End Sub
End Class