Imports System.IO
Imports System.Net

Public Class FrmUploadFileOnline
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim request As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create("ftp://182.156.84.26/delivery_challan.pdf"), System.Net.WebRequest)
        request.Credentials = New System.Net.NetworkCredential("equal2", "P@ssw0rd!P@ssw0rd!")
        request.Method = System.Net.WebRequestMethods.Ftp.UploadFile
        Dim file() As Byte = System.IO.File.ReadAllBytes("d:\delivery_challan.pdf")
        Dim strz As System.IO.Stream = request.GetRequestStream()
        strz.Write(file, 0, file.Length)
        strz.Close()
        strz.Dispose()
    End Sub
End Class