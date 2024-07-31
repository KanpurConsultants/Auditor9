Imports System.Drawing.Printing
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Web
Imports Microsoft.Reporting.WinForms
Public Class FrmSendWhatsapp

    Dim dsReport As New DataSet()
    Dim mReportStr As String = ""
    Dim I As Integer = 0
    Dim mReportFontSize As Integer = 8
    Dim mTotalColumnWidth As Double = 0
    Dim A4PortraitSizeWidth As Integer = 850
    Dim A4LandscapeSizeWidth As Integer = 1100
    Dim mReportTitle As String = "", mReportSubTitle As String = ""
    Dim AgL As AgLibrary.ClsMain
    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub
    Private Sub FrmReportPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        'Me.Location = New System.Drawing.Point(0, 0)
    End Sub
    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
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
    Private Sub BtnSend_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
        If FSendWhatsappMessage() = True Then
            MsgBox("Message Send Sucessfully...!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub
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
    Private Sub BtnSelectExcelFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile.Click
        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName

        'If AgL.PubIsDatabaseEncrypted = "N" Then
        '    Connection_ExternalDatabase.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
        'Else
        '    Connection_ExternalDatabase.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        'End If
        TxtExcelPath.Text = mDbPath
    End Sub

    Public Function FSendWhatsappMessage()
        Dim mQry As String = ""
        Dim DtTemp As DataTable = Nothing
        If TxtToMobile.Text.ToString.Replace(",", "") = "" Then
            Exit Function
        End If
        Try

            Dim MobileNoList As String = TxtToMobile.Text
            Dim BaseAPI As String = ""
            If TxtExcelPath.Text = "" Then
                BaseAPI = "https://clicke.co.in/eapi/sendMessage?auth_key=TrBcnEuWHfS8r4CnObJE&message='<Message>'&mobileNumber=<MobileNo>&type=msg"
            ElseIf StrReverse(TxtExcelPath.Text.ToString()).Substring(0, 4) = ".pdf" Then
                BaseAPI = "https://clicke.co.in/eapi/sendMessage?auth_key=TrBcnEuWHfS8r4CnObJE&message='<Message>'&pdf='<PdfPath>'&mobileNumber=<MobileNo>&type=pdf"
            ElseIf TxtExcelPath.Text = "" Then
                BaseAPI = "https://clicke.co.in/eapi/sendMessage?auth_key=TrBcnEuWHfS8r4CnObJE&message=<Message>&img=<ImagePath>&mobileNumber=<MobileNo>&type=img"
            End If


            Dim SmsAPI = BaseAPI.Replace("<MobileNo>", MobileNoList).Replace("<Message>", TxtMessage.Text).Replace("<PdfPath>", TxtExcelPath.Text).Replace("<ImagePath>", TxtExcelPath.Text)
            'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim myReq As HttpWebRequest = HttpWebRequest.Create(SmsAPI)
            Dim myResp As HttpWebResponse = myReq.GetResponse
            Dim respStreamReader As System.IO.StreamReader = New System.IO.StreamReader(myResp.GetResponseStream())
            Dim responseString As String = respStreamReader.ReadToEnd()
            respStreamReader.Close()
            myResp.Close()

            FSendWhatsappMessage = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

End Class