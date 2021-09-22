Imports System.Drawing.Printing.PrinterSettings
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports AgTemplate.ClsMain

Public Class FrmMailBulk
    Dim mAttachmentName As String = ""
    Dim mAttachmentSaveFolderName As String = "GalaPetPro"
    Dim mQry As String = ""

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker
    Private Delegate Sub UpdateLabelInvoker(ByVal text As String)
    Public Function FSendEMail(SMTPMain As System.Net.Mail.SmtpClient, MLMMain As System.Net.Mail.MailMessage) As Boolean
        Try
            SMTPMain.Send(MLMMain)
            MLMMain.Dispose()
            FSendEMail = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        FShowAttachments()
    End Sub
    Private Sub FShowAttachments()
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.SearchCode = mAttachmentSaveFolderName
        FrmObj.TableName = "SubGroupAttachments"
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()

        BtnAttachments.Tag = FrmObj

        Dim AttachmentPath As String = PubAttachmentPath + mAttachmentSaveFolderName + "\"
        If Directory.Exists(AttachmentPath) Then
            Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
            If FileCount > 0 Then BtnAttachments.Text = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else BtnAttachments.Text = "Attachments"
        Else
            BtnAttachments.Text = "Attachments"
        End If
    End Sub
    Private Sub SendBulkEMail()
        Dim DtTemp As DataTable = Nothing
        Dim MLDFrom As System.Net.Mail.MailAddress
        Dim MLMMain As System.Net.Mail.MailMessage
        Dim SMTPMain As System.Net.Mail.SmtpClient
        Dim I As Integer
        Dim bBlnEnableSsl As Boolean = False
        Dim mQry$ = ""
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
            Exit Sub
        End If

        If DtTemp.Rows.Count > 0 Then
            SmtpHost = AgL.XNull(DtTemp.Rows(0)("SmtpHost"))
            SmtpPort = AgL.XNull(DtTemp.Rows(0)("SmtpPort"))
            FromEmail = AgL.XNull(DtTemp.Rows(0)("FromEmailAddress"))
            FromEmailPassword = AgL.XNull(DtTemp.Rows(0)("FromEmailPassword"))
        End If

        If SmtpHost = "" Then MsgBox("Smtp Host is not defined in settings.") : Exit Sub
        If SmtpPort = "" Then MsgBox("Smtp Port is not defined in settings.") : Exit Sub
        If FromEmail = "" Then MsgBox("From Email is not defined in settings.") : Exit Sub
        If FromEmailPassword = "" Then MsgBox("From Email Password is not defined in settings.") : Exit Sub
        FileName = mAttachmentName + ".pdf"

        SmtpHost = AgL.XNull(SmtpHost)
        SmtpPort = AgL.XNull(SmtpPort)




        Dim bCondStr As String = ""
        If TxtAcGroup.Text <> "" Then
            bCondStr = " And Subgroup.GroupCode = '" & TxtAcGroup.Tag & "'"
        End If

        If TxtCity.Text <> "" Then
            bCondStr = " And Subgroup.CityCode = '" & TxtCity.Tag & "'"
        End If

        Dim mMaxId As String = ""

        Dim DtEmail As DataTable
        If TxtTestMail.Text <> "" Then
            If TxtTestMail.Text.Contains(",") Then
                mQry = ""
                Dim TxtTestMailArr As String() = TxtTestMail.Text.Split(",")
                For I = 0 To TxtTestMailArr.Length - 1
                    If TxtTestMailArr(I) <> "" Then
                        If mQry <> "" Then mQry += " UNION ALL "
                        mQry += "Select '" & TxtTestMailArr(I) & "' As Email "
                    End If
                Next
            Else
                mQry = "Select '" & TxtTestMail.Text & "' As Email "
            End If
            DtEmail = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Else
            mQry = "SELECT Sg.Email
                FROM (SELECT EMail FROM Subgroup Where 1=1 " & bCondStr &
                " GROUP BY EMail) AS Sg
                LEFT JOIN (SELECT Email, Max(U_EntDt) AS U_EntDt 
		                   FROM EMailLogTable
		                   GROUP BY Email
                ) AS  L ON Sg.Email = L.Email 
                WHERE Sg.Email IS NOT NULL
                AND (L.Email IS NULL OR julianday(" & AgL.Chk_Date(AgL.PubLoginDate) & ")  - julianday(U_EntDt)  > 7) "

            'IsNull(DateDiff(DAY,L.U_EntDt,'" & AgL.PubLoginDate & "'),0) 
            DtEmail = AgL.FillData(mQry, AgL.GCn).Tables(0)

            'mQry = "Select 'singh.akash409@gmail.com,meet2arpitg@gmail.com' As Email"
            'DtEmail = AgL.FillData(mQry, AgL.GCn).Tables(0)
        End If




        For I = 0 To DtEmail.Rows.Count - 1
            UpdateLabel((I + 1).ToString() + "/" + (DtEmail.Rows.Count).ToString)

            MLDFrom = New System.Net.Mail.MailAddress(FromEmail)
            MLMMain = New System.Net.Mail.MailMessage()
            MLMMain.From = MLDFrom
            SMTPMain = New System.Net.Mail.SmtpClient(SmtpHost, SmtpPort)


            MLMMain.IsBodyHtml = True
            'MLMMain.Body = "<div id=':2pt' class='a3s aXjCH '><u></u><div><div style='font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt'><span class='im'><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'>Dear Sir/Madam<br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'><br class='m_-8128810127888643946x_-1397340922m_2321117555251958158x_319038573Apple-interchange-newline'>We would like to take this opportunity to introduce us as Gala Enterprises, an exporter of leather pet toys of premium quality in competitive price. We take pride in informing that our company continually engages in development of new products and colours since our inception in 2013. Customer satisfaction is the prime motive.<span>&nbsp;</span></span></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'>We are located in Kanpur City in north of India which is a hub of Leather industry. This gives us advantage to give you our best competitive prices &amp; fast delivery.</span><br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'>We have gathered renowned image for the quality of the pet toys that we have been supplying to our clients all over the world, customer satisfaction &amp; on time delivery is our first priority.</span><br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'>We are attaching herewith the presentation of our range for your reference. All our toys can come in customise packaging according to your needs.<br><br>We hope to start a fruitful business relationship with your esteemed organisation and looking forward to receive your inquiries.</span></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div><div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'>Thanks &amp; With Regards,</span><br></div><div><br></div></span><div id='m_-8128810127888643946'><div><b><span class='m_-8128810127888643946size' style='font-size:13.3333px'><span class='m_-8128810127888643946font' style='font-family:verdana,sans-serif'>Arpit Gupta<br></span></span></b></div><div><b><span class='m_-8128810127888643946size' style='font-size:13.3333px'><span class='m_-8128810127888643946font' style='font-family:verdana,sans-serif'>Manager - Marketing</span></span></b><br></div><div><img src='https://scontent.fmaa2-1.fna.fbcdn.net/v/t1.0-9/66041613_432916460627360_6199236098602827776_n.png?_nc_cat=106&_nc_oc=AQlZyQNEXPtBMHVNJn51kFZJ_wJObQRb3ruktm-b2sF30yDd3E27FkdRRBxZlJqwBq9lzwalzclSXnvtwAOlFGme&_nc_ht=scontent.fmaa2-1.fna&oh=3b2f46c61a87ff5eb8959b01e6a7498e&oe=5D800B37' width='230' height='50' data-image-whitelisted='' class='CToWUd'><br></div><div><b><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>Gala Enterprises</span></span></b><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'><br></span></span></div><div><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>13/383 Parmat, Civil Lines<br></span></span></div><div><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>Kanpur - 208001 (India)<br>Mob : +91 9335 671 971<br></span></span></div><div><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>Web : </span></span><a href='http://www.galapetpro.com' target='_blank' data-saferedirecturl='https://www.google.com/url?q=http://www.galapetpro.com&amp;source=gmail&amp;ust=1562227721992000&amp;usg=AFQjCNERl6r-sz57kqBVmG4wj_eSqYokQQ'><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>www.GalaPetPro.com</span></span></a><div class='yj6qo ajU'><div id=':2hf' class='ajR' role='button' tabindex='0' aria-label='Hide expanded content' aria-expanded='true' data-tooltip='Hide expanded content'><img class='ajT' src='//ssl.gstatic.com/ui/v1/icons/mail/images/cleardot.gif'></div></div><div class='adL'><br></div></div><div class='adL'><div class='adm'><div id='q_662' class='ajR h4' data-tooltip='Hide expanded content' aria-label='Hide expanded content' aria-expanded='true'><div class='ajT'></div></div></div><div class='im'><div><br></div><div><br></div><div><span class='m_-8128810127888643946highlight' style='background-color:rgb(255,255,255)'><span class='m_-8128810127888643946colour' style='color:rgb(0,0,0)'><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:13.3333px'>Confidentiality :<span>&nbsp;</span><span class='m_-8128810127888643946colour' style='color:rgb(51,51,51)'><span class='m_-8128810127888643946m_2321117555251958158x_225865836font'><span class='m_-8128810127888643946size' style='font-size:12.8px'>This email and any files transmitted with it are confidential and intended solely for the use of the individual or entity to whom they are addressed. If you have received this email in error please notify the system manager. This message contains confidential information and is intended only for the individual named. If you are not the named addressee you should not disseminate, distribute or copy this e-mail. Please notify the sender immediately by e-mail if you have received this e-mail by mistake and delete this e-mail from your system. If you are not the intended recipient you are notified that disclosing, copying, distributing or taking any action in reliance on the contents of this information is strictly prohibited.</span></span></span></span></span></span></span><br><br></div></div></div></div><div class='adL'><br></div><div class='adL'><br></div></div><div class='adL'><br></div></div></div>"

            MLMMain.Body = "<div id=':2pt' class='a3s aXjCH '>
   <u></u>
   <div>
      <div style='font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt'>
         <span class='im'>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'>Dear Sir/Madam<br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'><br class='m_-8128810127888643946x_-1397340922m_2321117555251958158x_319038573Apple-interchange-newline'>We would like to take this opportunity to introduce us as Gala Enterprises, an exporter of leather pet toys of premium quality in competitive price. We take pride in informing that our company continually engages in development of new products and colours since our inception in 2013. Customer satisfaction is the prime motive.<span>&nbsp;</span></span></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'>We are located in Kanpur City in north of India which is a hub of Leather industry. This gives us advantage to give you our best competitive prices &amp; fast delivery.</span><br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'>We have gathered renowned image for the quality of the pet toys that we have been supplying to our clients all over the world, customer satisfaction &amp; on time delivery is our first priority.</span><br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'>We are attaching herewith the presentation of our range for your reference. All our toys can come in customise packaging according to your needs.<br><br>We hope to start a fruitful business relationship with your esteemed organisation and looking forward to receive your inquiries.</span></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><br></div>
            <div style='color:rgb(0,0,0);font-family:Verdana,Arial,Helvetica,sans-serif;font-size:10pt;font-style:normal;font-variant-ligatures:normal;font-variant-caps:normal;font-weight:400;letter-spacing:normal;text-align:start;text-indent:0px;text-transform:none;white-space:normal;word-spacing:0px;text-decoration-style:initial;text-decoration-color:initial;background-color:rgb(255,255,255)'><span style='font-family:verdana,sans-serif' class='m_-8128810127888643946x_-1397340922font'>Thanks &amp; With Regards,</span><br></div>
            <div><br></div>
         </span>
         <div id='m_-8128810127888643946'>
            <div><b><span class='m_-8128810127888643946size' style='font-size:13.3333px'><span class='m_-8128810127888643946font' style='font-family:verdana,sans-serif'>Arpit Gupta<br></span></span></b></div>
            <div><b><span class='m_-8128810127888643946size' style='font-size:13.3333px'><span class='m_-8128810127888643946font' style='font-family:verdana,sans-serif'>Manager - Marketing</span></span></b><br></div>
            <div><img src='https://scontent.fmaa2-1.fna.fbcdn.net/v/t1.0-9/66041613_432916460627360_6199236098602827776_n.png?_nc_cat=106&_nc_oc=AQlZyQNEXPtBMHVNJn51kFZJ_wJObQRb3ruktm-b2sF30yDd3E27FkdRRBxZlJqwBq9lzwalzclSXnvtwAOlFGme&_nc_ht=scontent.fmaa2-1.fna&oh=3b2f46c61a87ff5eb8959b01e6a7498e&oe=5D800B37' width='230' height='50' data-image-whitelisted='' class='CToWUd'><br></div>
            <div><b><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>Gala Enterprises</span></span></b><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'><br></span></span></div>
            <div><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>13/383 Parmat, Civil Lines<br></span></span></div>
            <div><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>Kanpur - 208001 (India)<br>Mob : +91 9335 671 971<br></span></span></div>
            <div>
               <span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>Web : </span></span><a href='http://www.galapetpro.com' target='_blank' data-saferedirecturl='https://www.google.com/url?q=http://www.galapetpro.com&amp;source=gmail&amp;ust=1562227721992000&amp;usg=AFQjCNERl6r-sz57kqBVmG4wj_eSqYokQQ'><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:16px'>www.GalaPetPro.com</span></span></a>
               <div class='yj6qo ajU'>
                  <div id=':2hf' class='ajR' role='button' tabindex='0' aria-label='Hide expanded content' aria-expanded='true' data-tooltip='Hide expanded content'><img class='ajT' src='//ssl.gstatic.com/ui/v1/icons/mail/images/cleardot.gif'></div>
               </div>
               <div class='adL'><br></div>
            </div>
            <div class='adL'>
               <div class='adm'>
                  <div id='q_662' class='ajR h4' data-tooltip='Hide expanded content' aria-label='Hide expanded content' aria-expanded='true'>
                     <div class='ajT'></div>
                  </div>
               </div>
               <div class='im'>
                  <div><br></div>
                  <div><span class='m_-8128810127888643946highlight' style='background-color:rgb(255,255,255)'><span class='m_-8128810127888643946colour' style='color:rgb(0,0,0)'><span class='m_-8128810127888643946font' style='font-family:Calibri,Verdana,Arial,sans-serif,sans-serif'><span class='m_-8128810127888643946size' style='font-size:13.3333px'>Confidentiality :<span>&nbsp;</span><span class='m_-8128810127888643946colour' style='color:rgb(51,51,51)'><span class='m_-8128810127888643946m_2321117555251958158x_225865836font'><span class='m_-8128810127888643946size' style='font-size:12.8px'>This email and any files transmitted with it are confidential and intended solely for the use of the individual or entity to whom they are addressed. If you have received this email in error please notify the system manager. This message contains confidential information and is intended only for the individual named. If you are not the named addressee you should not disseminate, distribute or copy this e-mail. Please notify the sender immediately by e-mail if you have received this e-mail by mistake and delete this e-mail from your system. If you are not the intended recipient you are notified that disclosing, copying, distributing or taking any action in reliance on the contents of this information is strictly prohibited.</span></span></span></span></span></span></span><br><br></div>
               </div>
            </div>
         </div>
         <div class='adL'><br></div>
         <div class='adL'><br></div>
      </div>
      <div class='adL'><br></div>
   </div>
</div>"

            'MLMMain.Body = TxtMessage.Text



            MLMMain.Subject = TxtSubject.Text

            If BtnAttachments.Tag IsNot Nothing Then
                Dim AttachmentPath As String = PubAttachmentPath + mAttachmentSaveFolderName + "\"
                If Directory.Exists(AttachmentPath) Then
                    Dim di As New IO.DirectoryInfo(AttachmentPath)
                    Dim diar1 As IO.FileInfo() = di.GetFiles().ToArray
                    Dim dra As IO.FileInfo
                    For Each dra In diar1
                        MLMMain.Attachments.Add(New System.Net.Mail.Attachment(dra.FullName))
                    Next
                End If
            End If

            If MLMMain.Attachments.Count = 0 Then MsgBox("No Attachments found...!", MsgBoxStyle.Information) : Exit Sub

            SMTPMain.Credentials = New Net.NetworkCredential(FromEmail, FromEmailPassword)
            SMTPMain.EnableSsl = True

            If AgL.XNull(DtEmail.Rows(I)("Email")).ToString.Contains(",") Then
                Dim ToMailArr As String() = AgL.XNull(DtEmail.Rows(I)("Email")).ToString.Split(",")
                For J As Integer = 0 To ToMailArr.Length - 1
                    If ToMailArr(J) <> "" Then
                        If AgL.IsValid_EMailId(AgL.XNull(ToMailArr(J)).ToString().Replace(vbLf, "").Replace(vbCrLf, "")) Then
                            MLMMain.To.Add(AgL.XNull(ToMailArr(J)).ToString().Replace(vbLf, "").Replace(vbCrLf, ""))
                        End If
                    End If
                Next
            Else
                MLMMain.To.Add(AgL.XNull(DtEmail.Rows(I)("Email")))
            End If

            If FSendEMail(SMTPMain, MLMMain) = True Then
                mMaxId = AgL.GetMaxId("EMailLogTable", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                mQry = " INSERT INTO EMailLogTable(Code, Email, U_EntDt)"
                mQry += " Select '" & mMaxId & "' As Code, 
                        '" & AgL.XNull(DtEmail.Rows(I)("Email")) & "' As Email, 
                        " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
            System.Threading.Thread.Sleep(2000)
        Next
        MsgBox("Process Complete.", MsgBoxStyle.Information)

    End Sub
    Public Sub UpdateLabel(ByVal Value As String)
        If Me.LblProgress.InvokeRequired Then
            Me.LblProgress.Invoke(New UpdateLabelInvoker(AddressOf Me.UpdateLabel), New Object() {Value})
            'Me.lblStatus.Invoke(New MethodInvoker(Me, DirectCast(Me.SaveCompleted, IntPtr)))
        Else
            Me.LblProgress.Text = Value
            LblProgress.Refresh()
        End If
    End Sub
    Private Sub BtnSend_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
        If BtnAttachments.Tag Is Nothing Then MsgBox("No Attachments found...!", MsgBoxStyle.Information) : Exit Sub
        If TxtSubject.Text = "" Then MsgBox("Subject is blank...!", MsgBoxStyle.Information) : Exit Sub
        BtnSend.Enabled = False
        BtnAttachments.Enabled = False
        _backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        _backgroundWorker1.WorkerSupportsCancellation = False
        _backgroundWorker1.WorkerReportsProgress = False
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.SendBulkEMail)
        _backgroundWorker1.RunWorkerAsync()
        LblProgress.Text = ""
    End Sub
    Public Sub FImportLeadsFromExcel()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As DataTable
        Dim DtDataFields As DataTable
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, 'Name' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Customer / Supplier / Transporter / Sales Agent / Purchase Agent. If Party is a simple ledger account like expenses then this field can be blank.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Account Group' as [Field Name], 'Text' as [Data Type], 100 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'City' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'State' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pin No' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Country' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Contact No' as [Field Name], 'Text' as [Data Type], 35 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'EMail' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        DtDataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Person Master Import"
        ObjFrmImport.Dgl1.DataSource = DtDataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)

        For I = 0 To DtDataFields.Rows.Count - 1
            If AgL.XNull(DtDataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtTemp.Columns.Contains(AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtDataFields.Rows(I)("Field Name").ToString()) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtDataFields.Rows(I)("Field Name").ToString()) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("State")) = "" Then
                DtTemp.Rows(I)("State") = AgL.XNull(DtTemp.Rows(I)("City"))
            End If

        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim bLastStateCode = AgL.GetMaxId("State", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            Dim DtState = DtTemp.DefaultView.ToTable(True, "State", "Country")
            For I = 0 To DtState.Rows.Count - 1
                Dim bStateCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastStateCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")



                mQry = "INSERT INTO State (Code, Description, EntryBy, EntryDate, EntryType, EntryStatus, Status, Div_Code, ManualCode, Country)
                        VALUES ('" & bStateCode & "', '" & AgL.XNull(DtState.Rows(I)("State")).ToString.Trim & "', '" & AgL.PubUserName & "', 
                        " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'Add', 'Open', 'Active', 
                        '" & AgL.PubDivCode & "', Null, 
                        '" & AgL.XNull(DtState.Rows(I)("Country")).ToString.Trim & "') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            Dim bLastAcGroupCode As Integer = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(GroupCode AS INTEGER)),0) FROM AcGroup WHERE ABS(GroupCode)>0", AgL.GcnRead).ExecuteScalar)
            Dim DtAccountGroup = DtTemp.DefaultView.ToTable(True, "Account Group")
            For I = 0 To DtAccountGroup.Rows.Count - 1
                Dim AcGroupTable As New FrmPerson.StructAcGroup
                Dim bAcGroupCode As String = (bLastAcGroupCode + (I + 1)).ToString.PadLeft(4).Replace(" ", "0")

                AcGroupTable.GroupCode = bAcGroupCode
                AcGroupTable.SNo = ""
                AcGroupTable.GroupName = AgL.XNull(DtAccountGroup.Rows(I)("Account Group")).ToString.Trim
                AcGroupTable.ContraGroupName = AgL.XNull(DtAccountGroup.Rows(I)("Account Group")).ToString.Trim
                AcGroupTable.GroupUnder = ""
                AcGroupTable.GroupNature = "A"
                AcGroupTable.Nature = "Others"
                AcGroupTable.SysGroup = "N"
                AcGroupTable.U_Name = AgL.PubUserName
                AcGroupTable.U_EntDt = AgL.GetDateTime(AgL.GcnRead)
                AcGroupTable.U_AE = "A"

                FrmPerson.ImportAcGroupTable(AcGroupTable)
            Next


            Dim bLastSubCode As String = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            For I = 0 To DtTemp.Rows.Count - 1
                Dim SubGroupTable As New FrmPerson.StructSubGroupTable
                Dim bSubCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastSubCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(8, "0")

                SubGroupTable.SubCode = bSubCode
                SubGroupTable.Site_Code = AgL.PubSiteCode
                SubGroupTable.Name = AgL.XNull(DtTemp.Rows(I)("Name")).ToString.Trim
                SubGroupTable.DispName = AgL.XNull(DtTemp.Rows(I)("Name")).ToString.Trim
                SubGroupTable.ManualCode = ""
                SubGroupTable.AccountGroup = AgL.XNull(DtTemp.Rows(I)("Account Group")).ToString.Trim
                SubGroupTable.StateName = AgL.XNull(DtTemp.Rows(I)("State")).ToString.Trim
                SubGroupTable.AgentName = ""
                SubGroupTable.TransporterName = ""
                SubGroupTable.AreaName = ""
                SubGroupTable.CityName = AgL.XNull(DtTemp.Rows(I)("City")).ToString.Trim
                SubGroupTable.GroupCode = ""
                SubGroupTable.GroupNature = ""
                SubGroupTable.Nature = ""
                SubGroupTable.Address = AgL.XNull(DtTemp.Rows(I)("Address")).ToString.Trim
                SubGroupTable.CityCode = ""
                SubGroupTable.PIN = AgL.XNull(DtTemp.Rows(I)("Pin No")).ToString.Trim
                SubGroupTable.Phone = AgL.XNull(DtTemp.Rows(I)("Contact No")).ToString.Trim
                SubGroupTable.ContactPerson = ""
                SubGroupTable.SubgroupType = SubgroupType.Customer
                SubGroupTable.Mobile = ""
                SubGroupTable.CreditDays = ""
                SubGroupTable.CreditLimit = ""
                SubGroupTable.EMail = AgL.XNull(DtTemp.Rows(I)("EMail")).ToString.Trim
                SubGroupTable.ParentCode = ""
                SubGroupTable.SalesTaxPostingGroup = ""
                SubGroupTable.EntryBy = AgL.PubUserName
                SubGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                SubGroupTable.EntryType = "Add"
                SubGroupTable.EntryStatus = LogStatus.LogOpen
                SubGroupTable.Div_Code = AgL.PubDivCode
                SubGroupTable.Status = "Active"
                SubGroupTable.SalesTaxNo = ""
                SubGroupTable.PANNo = ""
                SubGroupTable.AadharNo = ""
                SubGroupTable.Remark = AgL.XNull(DtTemp.Rows(I)("Remark")).ToString.Trim
                SubGroupTable.OMSId = ""
                SubGroupTable.Cnt = I
                ImportSubgroupTable(SubGroupTable)
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message + " at Record " + I.ToString)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub
    Private Sub MnuImportLeadsFromExcel_Click(sender As Object, e As EventArgs) Handles MnuImportLeadsFromExcel.Click
        FImportLeadsFromExcel()
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtAcGroup.KeyDown, TxtCity.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtAcGroup.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "SELECT GroupCode, GroupName  FROM AcGroup"
                            TxtAcGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtCity.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "SELECT CityCode, CityName  FROM City"
                            TxtCity.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmMailBulk_Load(sender As Object, e As EventArgs) Handles Me.Load
        TxtSubject.Text = "Gala Pet Pro-Leather Pet Toys-India"
        FShowAttachments()
    End Sub
    Public Shared Function ImportSubgroupTable(SubGroupTable As FrmPerson.StructSubGroupTable, Optional UpdateIfExists As Boolean = False) As String
        Dim mQry As String = ""
        Dim mRegSr As Integer = 0

        If AgL.Dman_Execute("SELECT Count(*) From Subgroup With (NoLock) where Phone = " & AgL.Chk_Text(SubGroupTable.Phone) & " ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then

            If AgL.Dman_Execute("SELECT Count(*) From City With (NoLock) where CityName = '" & SubGroupTable.CityName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
                Dim mLastCityCode = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.GCn.ConnectionString)

                Dim mCityCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mLastCityCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + 1).ToString().PadLeft(4, "0")


                mQry = "INSERT INTO City (CityCode, CityName, State, IsDeleted,
                             Country, EntryBy, EntryDate, EntryType,
                             EntryStatus, Status, Div_Code, U_Name, U_AE)
                             Select '" & mCityCode & "' As CityCode, '" & SubGroupTable.CityName & "' CityName, 
                             (Select Code From State Where Description = '" & SubGroupTable.StateName & "') State, 
                             0 As IsDeleted,
                             'India' As Country, '" & SubGroupTable.EntryBy & "' EntryBy, 
                             " & AgL.Chk_Date(SubGroupTable.EntryDate) & " As EntryDate, 
                             " & AgL.Chk_Text(SubGroupTable.EntryType) & " As EntryType,
                             " & AgL.Chk_Text(SubGroupTable.EntryStatus) & " As EntryStatus, 
                             " & AgL.Chk_Text(SubGroupTable.Status) & " As Status, 
                             " & AgL.Chk_Text(SubGroupTable.Div_Code) & " As Div_Code, 
                             '" & SubGroupTable.EntryBy & "'  As U_Name, 'A' As U_AE "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            SubGroupTable.CityCode = AgL.Dman_Execute("SELECT CityCode From City With (NoLock) where CityName = '" & SubGroupTable.CityName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar

            'If AgL.XNull(AgL.Dman_Execute("Select State From City With (NoLock) Where CityCode = '" & SubGroupTable.CityCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead))) = "" Then

            'End If

            If AgL.Dman_Execute("SELECT Count(*) From Area With (NoLock) where Description = '" & SubGroupTable.AreaName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
                Dim mLastAreaCode = AgL.GetMaxId("Area", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.GCn.ConnectionString)

                Dim mAreaCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mLastAreaCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + SubGroupTable.Cnt).ToString().PadLeft(4, "0")

                mQry = "INSERT INTO Area (Code, Description, IsDeleted,
                             EntryBy, EntryDate, EntryType,
                             EntryStatus, Status, Div_Code)
                             Select '" & mAreaCode & "' As AreaCode, '" & SubGroupTable.AreaName & "' Description, 
                             0 As IsDeleted,
                             '" & SubGroupTable.EntryBy & "' EntryBy, 
                             " & AgL.Chk_Date(SubGroupTable.EntryDate) & " As EntryDate, 
                             " & AgL.Chk_Text(SubGroupTable.EntryType) & " As EntryType,
                             " & AgL.Chk_Text(SubGroupTable.EntryStatus) & " As EntryStatus, 
                             " & AgL.Chk_Text(SubGroupTable.Status) & " As Status, 
                             " & AgL.Chk_Text(SubGroupTable.Div_Code) & " As Div_Code "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            End If

            SubGroupTable.AreaCode = AgL.Dman_Execute("SELECT Code From Area With (NoLock) where Description = '" & SubGroupTable.AreaName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar

            SubGroupTable.TransporterCode = AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock) Where Name = '" & SubGroupTable.TransporterName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            SubGroupTable.AgentCode = AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock) Where Name = '" & SubGroupTable.AgentName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            SubGroupTable.ParentCode = AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock) Where Name = '" & SubGroupTable.ParentCode & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar


            mQry = "SELECT GroupCode, GroupNature, Nature  From AcGroup With (NoLock) WHERE GroupName =  '" & SubGroupTable.AccountGroup & "'"
            Dim DtAcGroup As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
            If (DtAcGroup.Rows.Count > 0) Then
                If SubGroupTable.GroupCode = "" Then SubGroupTable.GroupCode = AgL.XNull(DtAcGroup.Rows(0)("GroupCode"))
                If SubGroupTable.GroupNature = "" Then SubGroupTable.GroupNature = AgL.XNull(DtAcGroup.Rows(0)("GroupNature"))
                If SubGroupTable.Nature = "" Then SubGroupTable.Nature = AgL.XNull(DtAcGroup.Rows(0)("Nature"))
            End If

            If SubGroupTable.SubgroupType = "" Then
                If SubGroupTable.Nature = "Customer" Or SubGroupTable.AccountGroup.Contains("Debtor") Then
                    SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.Customer
                ElseIf SubGroupTable.Nature = "Supplier" Or SubGroupTable.AccountGroup.Contains("Creditor") Then
                    SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.Supplier
                ElseIf SubGroupTable.Nature = "TRANSPORT" Or SubGroupTable.AccountGroup.Contains("TRANSPORT") Then
                    SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.Transporter
                ElseIf SubGroupTable.Nature = "Broker" Or SubGroupTable.AccountGroup.Contains("Broker") Then
                    SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.SalesAgent
                End If
            End If

            If SubGroupTable.SubgroupType = "" Then
                SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.LedgerAccount
            End If

            If SubGroupTable.SalesTaxPostingGroup = "Regular" Then
                SubGroupTable.SalesTaxPostingGroup = AgLibrary.ClsMain.agConstants.PostingGroupSalesTaxParty.Registered
            End If

            If SubGroupTable.PIN IsNot Nothing Then
                If SubGroupTable.PIN.Length > 6 Then
                    SubGroupTable.PIN = SubGroupTable.PIN.Substring(1, 6)
                End If
            End If


            'If SubGroupTable.Mobile.Length > 10 Then
            '    SubGroupTable.Mobile = SubGroupTable.Mobile.Substring(0, 9)
            'End If

            mQry = "INSERT INTO SubGroup(SubCode, Site_Code, Name, DispName, " &
                    " GroupCode, GroupNature, ManualCode,	Nature,	Address, CityCode,  " &
                    " PIN, Phone,  ContactPerson, SubgroupType, " &
                    " Mobile, CreditDays, CreditLimit, EMail, Parent, SalesTaxPostingGroup, " &
                    " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status, LockText, OMSId) " &
                    " Select " & AgL.Chk_Text(SubGroupTable.SubCode) & ", " &
                    " '" & SubGroupTable.Site_Code & "', " & AgL.Chk_Text(SubGroupTable.Name) & ",	" &
                    " " & AgL.Chk_Text(SubGroupTable.Name) & ", " & AgL.Chk_Text(SubGroupTable.GroupCode) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.GroupNature) & ", " & AgL.Chk_Text(SubGroupTable.ManualCode) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.Nature) & ", " & AgL.Chk_Text(SubGroupTable.Address) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.CityCode) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.PIN) & ", " & AgL.Chk_Text(SubGroupTable.Phone) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.ContactPerson) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.SubgroupType) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.Mobile) & ", " &
                    " " & Val(SubGroupTable.CreditDays) & ", " &
                    " " & Val(SubGroupTable.CreditLimit) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.EMail) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.ParentCode) & ", " & AgL.Chk_Text(SubGroupTable.SalesTaxPostingGroup) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.EntryBy) & ", " & AgL.Chk_Date(SubGroupTable.EntryDate) & ",   " &
                    " " & AgL.Chk_Text(SubGroupTable.EntryType) & ", " & AgL.Chk_Text(SubGroupTable.EntryStatus) & ",  " &
                    " " & AgL.Chk_Text(SubGroupTable.Div_Code) & ", " & AgL.Chk_Text(SubGroupTable.Status) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.LockText) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.OMSId) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            mQry = "INSERT INTO SubgroupSiteDivisionDetail (SubCode, V_Type, Div_Code, Site_Code,
                        V_Date, V_No, RateType, Transporter, TermsAndConditions, Agent)
                        Select '" & SubGroupTable.SubCode & "' As SubCode,  'SI' As V_Type, '" & SubGroupTable.Div_Code & "' As Div_Code, 
                        '" & SubGroupTable.Site_Code & "' As Site_Code,
                        Null As V_Date, Null As V_No, Null As RateType, " & AgL.Chk_Text(SubGroupTable.TransporterCode) & " As Transporter, 
                        Null As TermsAndConditions, " & AgL.Chk_Text(SubGroupTable.AgentCode) & " As Agent "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If SubGroupTable.SalesTaxNo <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, SR, RegistrationType, RegistrationNo)
                        Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.SalesTaxNo & "', " & AgL.Chk_Text(SubGroupTable.SalesTaxNo) & ") "
                Try
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If


            If SubGroupTable.PANNo <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.PanNo & "', " & AgL.Chk_Text(SubGroupTable.PANNo) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If SubGroupTable.AadharNo <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.AadharNo.ToUpper & "', " & AgL.Chk_Text(SubGroupTable.AadharNo) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Else
            SubGroupTable.SubCode = AgL.Dman_Execute("SELECT SubCode From Subgroup With (NoLock) where Name = " & AgL.Chk_Text(SubGroupTable.Name) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            SubGroupTable.AgentCode = AgL.Dman_Execute("SELECT SubCode From Subgroup With (NoLock) where Name = " & AgL.Chk_Text(SubGroupTable.AgentName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            SubGroupTable.TransporterCode = AgL.Dman_Execute("SELECT SubCode From Subgroup With (NoLock) where Name = " & AgL.Chk_Text(SubGroupTable.TransporterName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar

            mQry = " UPDATE Subgroup
                SET Address = " & AgL.Chk_Text(SubGroupTable.Address) & ",
	                PIN = " & AgL.Chk_Text(SubGroupTable.PIN) & ",
	                Phone = " & AgL.Chk_Text(SubGroupTable.Phone) & ",
	                Mobile = " & AgL.Chk_Text(SubGroupTable.Mobile) & ",
	                Email = " & AgL.Chk_Text(SubGroupTable.EMail) & ",
	                ContactPerson = " & AgL.Chk_Text(SubGroupTable.ContactPerson) & "
                WHERE Subcode = '" & SubGroupTable.SubCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "UPDATE SubgroupSiteDivisionDetail 
                    Set Transporter = " & AgL.Chk_Text(SubGroupTable.TransporterCode) & ", 
                    Agent = " & AgL.Chk_Text(SubGroupTable.AgentCode) & "
                    WHERE Subcode = '" & SubGroupTable.SubCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mRegSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) from SubgroupRegistration With (NoLock) Where SubCode = '" & SubGroupTable.SubCode & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            If SubGroupTable.SalesTaxNo <> "" Then
                If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration With (NoLock)
                            Where SubCode = '" & SubGroupTable.SubCode & "'
                            And Upper(RegistrationType) = '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.SalesTaxNo.ToString.ToUpper & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then
                    mRegSr += 1
                    mQry = "Insert Into SubgroupRegistration(Subcode, SR, RegistrationType, RegistrationNo)
                            Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.SalesTaxNo & "', " & AgL.Chk_Text(SubGroupTable.SalesTaxNo) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If


            If SubGroupTable.PANNo <> "" Then
                If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration With (NoLock)
                            Where SubCode = '" & SubGroupTable.SubCode & "'
                            And Upper(RegistrationType) = '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.PanNo.ToString.ToUpper & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then

                    mRegSr += 1
                    mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.PanNo & "', " & AgL.Chk_Text(SubGroupTable.PANNo) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If

            If SubGroupTable.AadharNo <> "" Then
                If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration With (NoLock)
                            Where SubCode = '" & SubGroupTable.SubCode & "'
                            And Upper(RegistrationType) = '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.AadharNo.ToString.ToUpper & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then

                    mRegSr += 1
                    mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.AadharNo.ToUpper & "', " & AgL.Chk_Text(SubGroupTable.AadharNo) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If
        End If
        Return SubGroupTable.SubCode
    End Function
End Class