Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization

Public Class WhatsAppSenderdb
    'Public Async Function UploadPdfToDropbox(filePath As String) As Task(Of String)
    '    Dim dropboxToken As String = "YOUR_DROPBOX_ACCESS_TOKEN"
    '    Dim dropboxPath As String = "/WhatsAppFiles/" & Path.GetFileName(filePath)

    '    Using client As New HttpClient()
    '        client.DefaultRequestHeaders.Authorization = New Net.Http.Headers.AuthenticationHeaderValue("Bearer", dropboxToken)
    '        Dim fileBytes As Byte() = File.ReadAllBytes(filePath)
    '        Dim content As New ByteArrayContent(fileBytes)

    '        Dim response = Await client.PostAsync($"https://content.dropboxapi.com/2/files/upload?path={Uri.EscapeDataString(dropboxPath)}", content)
    '        Dim result = Await response.Content.ReadAsStringAsync()

    '        ' Get the shared link
    '        Dim shareResponse = Await client.PostAsync("https://api.dropboxapi.com/2/sharing/create_shared_link_with_settings", New StringContent($"{""path"":""{dropboxPath}""}", Encoding.UTF8, "application/json"))
    '        Dim shareResult = Await shareResponse.Content.ReadAsStringAsync()

    '        ' Extract URL from JSON
    '        Dim json = Newtonsoft.Json.Linq.JObject.Parse(shareResult)
    '        Return json("url").ToString().Replace("dl=0", "raw=1") ' Force download
    '    End Using
    'End Function
End Class
