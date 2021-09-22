Public Class ClsErrHandler
    Dim StrTemp As String


    Public Shared Sub HandleException(ByVal Ex As Exception, Optional ByVal Msg As String = "")
        Dim StrTemp As String
        StrTemp = Ex.Message
        If Msg.Trim <> "" Then
            StrTemp = StrTemp & vbCrLf & " " & Msg
        End If
        AgMsgBox(StrTemp)
    End Sub


    Public Shared Function AgMsgBox(ByVal Message As Object, Optional ByVal ButtonType As MsgBoxStyle = MsgBoxStyle.OkOnly) As MsgBoxResult
        MsgBox(Message, MsgBoxStyle.OkOnly, "Saran")
    End Function
End Class
