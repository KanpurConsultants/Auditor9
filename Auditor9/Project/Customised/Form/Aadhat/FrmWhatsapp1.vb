Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Text
Imports Customised.ClsMain

Public Class FrmWhatsapp1
    'Private Const RequestUrl As String = "http://app.laksmartindia.com/api/v1/message/create"
    'Private Const Username As String = "Satyam%20Tripathi"
    'Private Const Password As String = "KC@12345"
    'Private Const receiverMobileNo As String = "8299399688"
    Private RequestUrl As String = FGetSettings(SettingFields.WhatsappRequestUrl, "E Invoice", "", "", "", "", "", "", "")
    Private Username As String = FGetSettings(SettingFields.WhatsappUsername, "E Invoice", "", "", "", "", "", "", "")
    Private Password As String = FGetSettings(SettingFields.WhatsappPassword, "E Invoice", "", "", "", "", "", "", "")

    Public Function SendPDFByWhatsapp(receiverMobileNo As String, message As String, FilePath As String) As String
        'Dim url As String = "http://app.laksmartindia.com/api/v1/message/create"
        'Dim username As String = "Satyam Tripathi"
        'Dim password As String = "KC@12345"


        ' 1. Combine username and password
        Dim authString As String = Username & ":" & Password

        ' 2. Convert to base64
        Dim authBytes As Byte() = Encoding.UTF8.GetBytes(authString)
        Dim authBase64 As String = Convert.ToBase64String(authBytes)

        Dim fileBytes As Byte() = File.ReadAllBytes(FilePath)
        Dim base64Body As String = Convert.ToBase64String(fileBytes)
        Dim fileName As String = System.IO.Path.GetFileName(FilePath)

        Dim json As String = "{
  ""receiverMobileNo"": ""+91" & receiverMobileNo & """,
  ""message"": [
    """ & message & """
  ],
  ""base64File"": [
    {
      ""name"": """ & FileName & """,
      ""body"": """ & base64Body & """
    }
  ]
}"

        Try
            Dim request As HttpWebRequest = CType(System.Net.WebRequest.Create(RequestUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", "Basic " & authBase64)
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
            Return ("Whatsapp Send Sucessfully !")
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

    Public Function SendMessageByWhatsapp(receiverMobileNo As String, message As String) As String
        'Dim url As String = "http://app.laksmartindia.com/api/v1/message/create"
        'Dim username As String = "Satyam Tripathi"
        'Dim password As String = "KC@12345"


        ' 1. Combine username and password
        Dim authString As String = Username & ":" & Password

        ' 2. Convert to base64
        Dim authBytes As Byte() = Encoding.UTF8.GetBytes(authString)
        Dim authBase64 As String = Convert.ToBase64String(authBytes)


        Dim json As String = "{
  ""receiverMobileNo"": ""+91" & receiverMobileNo & """,
  ""message"": [
    """ & message & """
  ]
}"

        Try
            Dim request As HttpWebRequest = CType(System.Net.WebRequest.Create(RequestUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", "Basic " & authBase64)
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
            Return ("Whatsapp Send Sucessfully !")
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

    Private Sub BtnSendDocument_Click(sender As Object, e As EventArgs) Handles BtnSendWhatsapp.Click
        MsgBox(FSendWhatsapp(), MsgBoxStyle.Information)
    End Sub

    Public Function FSendWhatsapp()
        Dim mQry As String = ""
        Dim DtTemp As DataTable = Nothing
        If TxtToMobile.Text.ToString.Replace(",", "") = "" Then
            FSendWhatsapp = "Invalid Mobile No"
            Exit Function
        End If
        If TxtMessage.Text.ToString.Replace(",", "") = "" Then
            FSendWhatsapp = "Invalid Message"
            Exit Function
        End If

        Try
            Dim MobileNoList As String = TxtToMobile.Text
            Dim Message As String = TxtMessage.Text.Replace(vbCrLf, "\n").Replace(vbLf, "\n")
            If TxtFilePath.Text <> "" Then
                FSendWhatsapp = SendPDFByWhatsapp(MobileNoList, Message, TxtFilePath.Text)
            Else
                FSendWhatsapp = SendMessageByWhatsapp(MobileNoList, Message)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub BtnTo_Click(sender As Object, e As EventArgs) Handles BtnTo.Click
        Select Case sender.Name
            Case BtnTo.Name
                If TxtToMobile.Text <> "" Then
                    TxtToMobile.Text = TxtToMobile.Text + "," + FHPGD_PhoneContacts()
                Else
                    TxtToMobile.Text = FHPGD_PhoneContacts()
                End If
        End Select
    End Sub
    Private Function FHPGD_PhoneContacts() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable
        Dim mQry As String = ""

        mQry = " Select 'o' As Tick, Sg.SubCode As SearchKey, Sg.Name, C.CityName, Ag.GroupName, IfNull(Sg.Mobile,Sg.Phone) 
                From SubGroup Sg
                Left Join City C On Sg.CityCode = C.CityCode
                Left Join AcGroup AG On Sg.GroupCode = Ag.GroupCode
                Where IfNull(Sg.Mobile,Sg.Phone) Is Not Null And IfNull(Sg.Mobile,Sg.Phone) <> '' 
                Order By Sg.Name, C.CityName"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            Exit Function
        End If

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 800, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 280, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "City", 130, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Ac Group", 130, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, "Mobile", 130, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(5, "", "", ",", True)
        End If
        FHPGD_PhoneContacts = StrRtn

        FRH_Multiple = Nothing
    End Function
    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName
        TxtFilePath.Text = mDbPath
    End Sub

End Class