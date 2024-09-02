Imports System.Net
Public Class FrmWhatsappAttachmentTest
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try


            Dim client As New WebClient()

            ' Upload the file
            Dim response As Byte() = client.UploadFile("https://file.io", "POST", "C:\Users\HP\Desktop\TestFile.txt")

            ' Convert the response to a string
            Dim responseString As String = System.Text.Encoding.UTF8.GetString(response)
            Console.WriteLine(responseString)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class