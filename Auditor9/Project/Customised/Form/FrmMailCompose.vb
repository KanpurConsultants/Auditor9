Imports System.Drawing.Printing
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.Reporting.WinForms

Public Class FrmMailCompose

    Dim dsReport As New DataSet()
    Dim mReportStr As String = ""
    Dim I As Integer = 0
    Dim mReportFontSize As Integer = 8
    Dim mTotalColumnWidth As Double = 0
    Dim A4PortraitSizeWidth As Integer = 850
    Dim A4LandscapeSizeWidth As Integer = 1100
    Dim mReportTitle As String = "", mReportSubTitle As String = ""
    Dim mAttachmentName As String = ""
    Dim AgL As AgLibrary.ClsMain

    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar

    End Sub
    Public Property AttachmentName() As String
        Get
            AttachmentName = mAttachmentName
        End Get
        Set(ByVal value As String)
            mAttachmentName = value
        End Set
    End Property
    Public Property ReportTitle() As String
        Get
            ReportTitle = mReportTitle
        End Get
        Set(ByVal value As String)
            mReportTitle = value
        End Set
    End Property
    Public Property ReportSubTitle() As String
        Get
            ReportSubTitle = mReportSubTitle
        End Get
        Set(ByVal value As String)
            mReportSubTitle = value
        End Set
    End Property
    Private Sub FSetPageSetting()
        Me.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        reportViewer1.ZoomMode = ZoomMode.Percent
        reportViewer1.ZoomPercent = 50

        Dim ps As New PageSettings()
        ps.Margins = New Margins(40, 10, 20, 20)
        If mTotalColumnWidth <= A4PortraitSizeWidth Then
            ps.PaperSize = New PaperSize("A4", 850, 1100)
            ps.PaperSize.RawKind = PaperKind.A4
        Else
            ps.Landscape = True
            ps.PaperSize = New PaperSize("A4", 850, 1100)
            ps.PaperSize.RawKind = PaperKind.A4
        End If
        reportViewer1.SetPageSettings(ps)
    End Sub
    Private Sub FrmReportPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        Me.Location = New System.Drawing.Point(0, 0)
        reportViewer1.RefreshReport()
        FSetPageSetting()
    End Sub
    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Public Function FSendEMail() As Boolean
        Dim DtTemp As DataTable = Nothing
        Dim MLDFrom As System.Net.Mail.MailAddress
        Dim MLMMain As System.Net.Mail.MailMessage
        Dim SMTPMain As System.Net.Mail.SmtpClient
        Dim I As Integer
        Dim bBlnEnableSsl As Boolean = False
        Dim mQry$ = ""
        'Dim SmtpHost As String = "smtp.gmail.com"
        'Dim SmtpPort As String = "587"
        'Dim FromEmail As String = "equal2.noreply@gmail.com"
        'Dim FromEmailPassword As String = "P@ssw0rd!"
        Dim SmtpHost As String = ""
        Dim SmtpPort As String = ""
        Dim FromEmail As String = ""
        Dim FromEmailPassword As String = ""
        Dim FileName As String = ""
        Dim ToEMailArr As String() = Nothing
        Dim CcEMailArr As String() = Nothing


        mQry = "Select * From MailSender Where Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            mQry = "Select * From MailSender Where Div_Code = '" & AgL.PubDivCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count = 0 Then
                mQry = "Select * From MailSender Where Site_Code = '" & AgL.PubSiteCode & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count = 0 Then
                    mQry = "Select * From MailSender "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                End If
            End If
        End If

        If DtTemp.Rows.Count = 0 Then
            MsgBox("Please define mail settings...!", MsgBoxStyle.Information)
            Exit Function
        End If

        If DtTemp.Rows.Count > 0 Then
            SmtpHost = AgL.XNull(DtTemp.Rows(0)("SmtpHost"))
            SmtpPort = AgL.XNull(DtTemp.Rows(0)("SmtpPort"))
            FromEmail = AgL.XNull(DtTemp.Rows(0)("FromEmailAddress"))
            FromEmailPassword = AgL.XNull(DtTemp.Rows(0)("FromEmailPassword"))
        End If

        If SmtpHost = "" Then MsgBox("Smtp Host is not defined in settings.") : Exit Function
        If SmtpPort = "" Then MsgBox("Smtp Port is not defined in settings.") : Exit Function
        If FromEmail = "" Then MsgBox("From Email is not defined in settings.") : Exit Function
        If FromEmailPassword = "" Then MsgBox("From Email Password is not defined in settings.") : Exit Function

        FileName = mAttachmentName + ".pdf"

        ToEMailArr = TxtToEmail.Text.Split(",")
        CcEMailArr = TxtCcEMail.Text.Split(",")

        Try
            SmtpHost = AgL.XNull(SmtpHost)
            SmtpPort = AgL.XNull(SmtpPort)

            MLDFrom = New System.Net.Mail.MailAddress(FromEmail)
            MLMMain = New System.Net.Mail.MailMessage()
            MLMMain.From = MLDFrom
            SMTPMain = New System.Net.Mail.SmtpClient(SmtpHost, SmtpPort)
            MLMMain.Body = TxtMessage.Text
            MLMMain.Subject = TxtSubject.Text

            For I = 0 To ToEMailArr.Length - 1
                If ToEMailArr(I) <> "" Then
                    MLMMain.To.Add(ToEMailArr(I))
                End If
            Next

            For I = 0 To CcEMailArr.Length - 1
                If CcEMailArr(I) <> "" Then
                    MLMMain.CC.Add(CcEMailArr(I))
                End If
            Next

            Dim pdfContent As Byte() = reportViewer1.LocalReport.Render("PDF", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Dim warn() As Warning = Nothing
            Dim streamids() As String = Nothing
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = String.Empty
            Dim bytes() As Byte
            bytes = reportViewer1.LocalReport.Render("pdf", Nothing, mimeType, encoding, extension, streamids, warn)
            Dim MS As MemoryStream = New System.IO.MemoryStream(bytes)
            MLMMain.Attachments.Add(New System.Net.Mail.Attachment(MS, FileName))


            SMTPMain.Credentials = New Net.NetworkCredential(FromEmail, FromEmailPassword)
            SMTPMain.EnableSsl = True
            SMTPMain.Send(MLMMain)

            MLMMain.Dispose()
            FSendEMail = True


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Function FHPGD_EmailContacts() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable
        Dim mQry As String = ""

        mQry = " Select 'o' As Tick, SubCode As SearchKey, Name, Email From SubGroup Where EMail Is Not Null "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            Exit Function
        End If

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 720, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "EMail", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(3, "", "", ",", True)
        End If
        FHPGD_EmailContacts = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub BtnSend_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
        If FSendEMail() = True Then
            MsgBox("Mail Send Sucessfully...!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub BtnTo_Click(sender As Object, e As EventArgs) Handles BtnTo.Click, BtnCc.Click
        Select Case sender.Name
            Case BtnTo.Name
                TxtToEmail.Text = TxtToEmail.Text + "," + FHPGD_EmailContacts()
            Case BtnCc.Name
                TxtCcEMail.Text = TxtCcEMail.Text + "," + FHPGD_EmailContacts()
        End Select
    End Sub
End Class