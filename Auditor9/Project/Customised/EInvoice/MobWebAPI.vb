Module MobWebAPI
    Public Function WebRequest(url As String) As String
        Dim http
        http = CreateObject("WinHttp.WinHttpRequest.5.1")

        http.Open("GET", url, False)
        http.Send
        http.WaitForResponse(10000)

        WebRequest = http.ResponseText

    End Function

    Public Function WebRequestbody(url As String, body As String) As String
        Dim http
        http = CreateObject("WinHttp.WinHttpRequest.5.1")

        http.Open("POST", url, False)
        http.SetRequestHeader("Content-Type", "application/json; charset=utf-8")
        http.Send(body)
        http.WaitForResponse(10000)
        WebRequestbody = http.ResponseText
    End Function
End Module
