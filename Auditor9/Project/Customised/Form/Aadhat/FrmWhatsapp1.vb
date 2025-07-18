Imports System.IO
Imports System.Net
Imports System.Text
Public Class FrmWhatsapp1
    Private Const RequestUrl As String = "http://app.laksmartindia.com/api/v1/message/create"
    Private Const Username As String = "Satyam%20Tripathi"
    Private Const Password As String = "KC@12345"
    Private Const receiverMobileNo As String = "8299399688"

    '────────── BUTTON HANDLERS ──────────
    Private Sub btnSendText_Click(sender As Object, e As EventArgs) Handles btnSendText.Click
        'MessageBox.Show(SendOnlyText())
        MessageBox.Show(SendMessageByWhatsapp("8299399688", "Hello Dost"))
    End Sub

    Private Sub btnSendTextFile_Click(sender As Object, e As EventArgs) Handles btnSendTextFile.Click
        'MessageBox.Show(SendTextPlusFile())
        MessageBox.Show(SendPDFByWhatsapp("8299399688", "C:\Users\Public\test.pdf"))
    End Sub

    Private Sub btnSendMulti_Click(sender As Object, e As EventArgs) Handles btnSendMulti.Click
        'MessageBox.Show(SendMultiTextMultiFile)
    End Sub

    Private Sub btnbtByURL_Click(sender As Object, e As EventArgs) Handles btByURL.Click
        'MessageBox.Show(PostByURL())
    End Sub

    Public Function SendMessageByWhatsapp(receiverMobileNo As String, message As String) As String
        Dim url As String = "http://app.laksmartindia.com/api/v1/message/create"

        Dim json As String = "{
                              ""receiverMobileNo"": ""+91" & receiverMobileNo & """,
                              ""message"": [""" & message & """]
                              }"

        Try
            Dim request As HttpWebRequest = CType(System.Net.WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", "Basic U2F0eWFtIFRyaXBhdGhpOktDQDEyMzQ1")
            request.Accept = "application/json"

            ' Convert JSON to byte array
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(json)
            request.ContentLength = bytes.Length

            ' Write request body
            Using stream As Stream = request.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            ' Get the response
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Dim responseText As String = reader.ReadToEnd()
                Console.WriteLine("Response: " & responseText)
            End Using
            Return ("Message Send Sussesfully !")
        Catch ex As WebException
            Console.WriteLine("Error: " & ex.Message)
            Return ("Server says: " & ex.Message)
            ' Optional: print server error response if any
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Dim errorText As String = reader.ReadToEnd()
                    Console.WriteLine("Server says: " & errorText)
                    Return ("Server says: " & errorText)
                End Using
            End If
        End Try
    End Function

    Public Function SendPDFByWhatsapp(receiverMobileNo As String, filePath As String) As String
        Dim url As String = "http://app.laksmartindia.com/api/v1/message/create"
        '        Dim json As String = "{
        '  ""receiverMobileNo"": ""+918299399688"",
        '  ""message"": ""Hello%20Satyam%20WelcomeinPostByJSN"",
        '  ""base64File"": [
        '    {
        '      ""name"": ""dummy.pdf"",
        '      ""body"": ""JVBERi0xLjIgCjkgMCBvYmoKPDwKPj4Kc3RyZWFtCkJULyA5IFRmKFRlc3QpJyBFVAplbmRzdHJlYW0KZW5kb2JqCjQgMCBvYmoKPDwKL1R5cGUgL1BhZ2UKL1BhcmVudCA1IDAgUgovQ29udGVudHMgOSAwIFIKPj4KZW5kb2JqCjUgMCBvYmoKPDwKL0tpZHMgWzQgMCBSIF0KL0NvdW50IDEKL1R5cGUgL1BhZ2VzCi9NZWRpYUJveCBbIDAgMCA5OSA5IF0KPj4KZW5kb2JqCjMgMCBvYmoKPDwKL1BhZ2VzIDUgMCBSCi9UeXBlIC9DYXRhbG9nCj4+CmVuZG9iagp0cmFpbGVyCjw8Ci9Sb290IDMgMCBSCj4+CiUlRU9G""
        '    }
        '  ]
        '}"

        'Dim fileBytes As Byte() = File.ReadAllBytes("C:\Users\Public\test.pdf")
        Dim fileBytes As Byte() = File.ReadAllBytes(filePath)
        Dim base64Body As String = Convert.ToBase64String(fileBytes)

        Dim json As String = "{
  ""receiverMobileNo"": ""+91" & receiverMobileNo & """,
  ""base64File"": [
    {
      ""name"": ""test.pdf"",
      ""body"": """ & base64Body & """
    }
  ]
}"

        Try
            Dim request As HttpWebRequest = CType(System.Net.WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", "Basic U2F0eWFtIFRyaXBhdGhpOktDQDEyMzQ1")
            request.Accept = "application/json"

            ' Convert JSON to byte array
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(json)
            request.ContentLength = bytes.Length

            ' Write request body
            Using stream As Stream = request.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            ' Get the response
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Dim responseText As String = reader.ReadToEnd()
                Console.WriteLine("Response: " & responseText)
            End Using
            Return ("Message Send Sussesfully !")
        Catch ex As WebException
            Console.WriteLine("Error: " & ex.Message)
            Return ("Server says: " & ex.Message)
            ' Optional: print server error response if any
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Dim errorText As String = reader.ReadToEnd()
                    Console.WriteLine("Server says: " & errorText)
                    Return ("Server says: " & errorText)
                End Using
            End If
        End Try
    End Function

End Class