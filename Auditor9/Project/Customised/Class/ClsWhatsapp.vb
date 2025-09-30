Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Text
Imports System.Threading.Tasks
Imports AgLibrary.ClsMain.agConstants
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Customised.ClsMain
Public Class WhatsAppSender
    'Dim RequestUrl As String = "http://app.laksmartindia.com/api/v1/message/create"
    'Dim Username As String = "Satyam Tripathi"
    'Dim Password As String = "KC@12345"

    Private RequestUrl As String = FGetSettings(SettingFields.WhatsappRequestUrl, "E Invoice", "", "", "", "", "", "", "")
    Private Username As String = FGetSettings(SettingFields.WhatsappUsername, "E Invoice", "", "", "", "", "", "", "")
    Private Password As String = FGetSettings(SettingFields.WhatsappPassword, "E Invoice", "", "", "", "", "", "", "")

    Public Function SendMessageByWhatsapp(receiverMobileNo As String, message As String) As String
        'receiverMobileNo = "8299399688"
        ' 1. Combine username and password
        Dim authString As String = Username & ":" & Password

        ' 2. Convert to base64
        Dim authBytes As Byte() = Encoding.UTF8.GetBytes(authString)
        Dim authBase64 As String = Convert.ToBase64String(authBytes)

        'Dim MS As MemoryStream = CType((CType(CrvReport.ReportSource, ReportDocument).ExportToStream(ExportFormatType.PortableDocFormat)), MemoryStream)
        Dim ms As MemoryStream = DirectCast(AgL.PubCrystalDocument.ExportToStream(ExportFormatType.PortableDocFormat), MemoryStream)
        'Dim ms As MemoryStream = DirectCast(AgL.PubCrystalDocument.ExportToStream(ExportFormatType.WordForWindows), MemoryStream)



        'AgL.PubCrystalDocument.ExportToDisk(ExportFormatType.)
        Dim base64Body As String = Convert.ToBase64String(ms.ToArray())

        Dim json As String = "{
          ""receiverMobileNo"": ""+91" & receiverMobileNo & """,
          ""message"": [
            """ & message & """
          ]
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

    Public Function SendPDFByWhatsapp(receiverMobileNo As String, FileName As String) As String
        'receiverMobileNo = "8299399688"
        ' 1. Combine username and password
        Dim authString As String = Username & ":" & Password

        ' 2. Convert to base64
        Dim authBytes As Byte() = Encoding.UTF8.GetBytes(authString)
        Dim authBase64 As String = Convert.ToBase64String(authBytes)

        'Dim MS As MemoryStream = CType((CType(CrvReport.ReportSource, ReportDocument).ExportToStream(ExportFormatType.PortableDocFormat)), MemoryStream)
        Dim ms As MemoryStream = DirectCast(AgL.PubCrystalDocument.ExportToStream(ExportFormatType.PortableDocFormat), MemoryStream)
        'Dim ms As MemoryStream = DirectCast(AgL.PubCrystalDocument.ExportToStream(ExportFormatType.WordForWindows), MemoryStream)



        'AgL.PubCrystalDocument.ExportToDisk(ExportFormatType.)
        Dim base64Body As String = Convert.ToBase64String(ms.ToArray())

        '        Dim json As String = "{
        '  ""receiverMobileNo"": ""+91" & receiverMobileNo & """,
        '  ""message"": [
        '    """ & message & """
        '  ],
        '  ""base64File"": [
        '    {
        '      ""name"": """ & FileName & """,
        '      ""body"": """ & base64Body & """
        '    }
        '  ]
        '}"

        Dim json As String = "{
  ""receiverMobileNo"": ""+91" & receiverMobileNo & """,
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

    Public Shared Sub FPrintThisDocument(ByVal objFrm As Object, ByVal objRepFrm As Object, ByVal V_Type As String,
     Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "",
     Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "",
     Optional ByVal SubReport_QueryList As String = "",
     Optional ByVal SubReport_NameList As String = "", Optional ByVal Division As String = "", Optional ByVal Site As String = "", Optional mSearchCode As String = "", Optional ByVal IsSendPDFWhatsapp As Boolean = False, Optional ByVal PartyMobileNo As String = "", Optional ByVal WhatsappMessage As String = "", Optional ByVal WhatsappFileName As String = ""
     )

        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String = ""
        Dim DsRep As New DataSet
        Dim strQry As String = ""

        Dim RepName As String = ""
        Dim RepTitle As String = ""
        Dim RepQry As String = ""

        Dim RetIndex As Integer = 0

        Dim Report_QryArr() As String = Nothing
        Dim Report_NameArr() As String = Nothing
        Dim Report_TitleArr() As String = Nothing
        Dim Report_FormatArr() As String = Nothing

        Dim SubReport_QryArr() As String = Nothing
        Dim SubReport_NameArr() As String = Nothing
        Dim SubReport_DataSetArr() As DataSet = Nothing

        Dim I As Integer = 0

        Try

            If TypeOf (objFrm) Is AgTemplate.TempTransaction Then
                If Division = "" Then
                    Division = objFrm.TxtDivision.Tag
                End If
            Else
                If Division = "" Then
                    Division = AgL.PubDivCode
                End If
            End If



            If Report_QueryList <> "" Then Report_QryArr = Split(Report_QueryList, "~")
            If Report_TitleList <> "" Then Report_TitleArr = Split(Report_TitleList, "|")
            If Report_NameList <> "" Then Report_NameArr = Split(Report_NameList, "|")

            If Report_FormatList <> "" Then
                Report_FormatArr = Split(Report_FormatList, "|")

                For I = 0 To Report_FormatArr.Length - 1
                    If strQry <> "" Then strQry += " UNION ALL "
                    strQry += " Select " & I & " As Code, '" & Report_FormatArr(I) & "' As Name "
                Next

                Dim FRH_Single As DMHelpGrid.FrmHelpGrid
                FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(strQry, AgL.GCn).TABLES(0)), "", 300, 350, , , False)
                FRH_Single.FFormatColumn(0, , 0, , False)
                FRH_Single.FFormatColumn(1, "Report Format", 250, DataGridViewContentAlignment.MiddleLeft)
                FRH_Single.StartPosition = FormStartPosition.CenterScreen
                FRH_Single.ShowDialog()

                If FRH_Single.BytBtnValue = 0 Then
                    RetIndex = FRH_Single.DRReturn("Code")
                End If

                If Report_NameArr.Length = Report_FormatArr.Length Then RepName = Report_NameArr(RetIndex) Else RepName = Report_NameArr(0)
                If Report_TitleArr.Length = Report_FormatArr.Length Then RepTitle = Report_TitleArr(RetIndex) Else RepTitle = Report_TitleArr(0)
                If Report_QryArr.Length = Report_FormatArr.Length Then RepQry = Report_QryArr(RetIndex) Else RepQry = Report_QryArr(0)
            Else
                RepName = Report_NameArr(0)
                RepTitle = Report_TitleArr(0)
                RepQry = Report_QryArr(0)
            End If



            AgL.PubTempStr = AgL.PubTempStr & "Start Execute Main Query to print : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            DsRep = AgL.FillData(RepQry, AgL.GCn)
            AgL.PubTempStr = AgL.PubTempStr & "End Execute Main Query to print : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf



            Dim mCompanyLogoFileName As String
            Dim mCompanyAuthorisedSignatoryFileName As String
            Dim mEInvoiceQrCodeFileName As String = ""
            Dim mPaymentQrCodeFileName As String

            AgL.PubTempStr = AgL.PubTempStr & "Start fetching logo & signature file name from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If TypeOf (objFrm) Is AgTemplate.TempTransaction Then
                mCompanyLogoFileName = ClsMain.FGetSettings(SettingFields.CompanyLogoFileName, SettingType.General, objFrm.TxtDivision.Tag, objFrm.TxtSite_Code.Tag, "", "", "", "", "")
                mCompanyAuthorisedSignatoryFileName = ClsMain.FGetSettings(SettingFields.CompanyAuthorisedSignatoryFileName, SettingType.General, objFrm.TxtDivision.Tag, objFrm.TxtSite_Code.Tag, "", "", "", "", "")
                mPaymentQrCodeFileName = ClsMain.FGetSettings(SettingFields.PaymentQrCodeFileName, SettingType.General, objFrm.TxtDivision.Tag, objFrm.TxtSite_Code.Tag, "", "", "", "", "")
            Else
                mCompanyLogoFileName = ClsMain.FGetSettings(SettingFields.CompanyLogoFileName, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                mCompanyAuthorisedSignatoryFileName = ClsMain.FGetSettings(SettingFields.CompanyAuthorisedSignatoryFileName, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                mPaymentQrCodeFileName = ClsMain.FGetSettings(SettingFields.PaymentQrCodeFileName, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            End If
            mEInvoiceQrCodeFileName = PubAttachmentPath + mSearchCode + "\" + "EInvoiceQrCode.PNG"

            AgL.PubTempStr = AgL.PubTempStr & "End fetching logo & signature file name from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            DsRep.Tables(0).Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"))
            DsRep.Tables(0).Columns.Add("CompanyAuthorisedSignature", System.Type.GetType("System.Byte[]"))
            DsRep.Tables(0).Columns.Add("EInvoiceQrCode", System.Type.GetType("System.Byte[]"))
            DsRep.Tables(0).Columns.Add("PaymentQrCode", System.Type.GetType("System.Byte[]"))

            AgL.PubTempStr = AgL.PubTempStr & "Start Reading Logo File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            Dim FileCompanyLogo() As Byte
            If File.Exists(mCompanyLogoFileName) Then
                FileCompanyLogo = ReadFile(mCompanyLogoFileName)
            Else
                FileCompanyLogo = ConvertToByteArray(My.Resources.BlankImage)
            End If


            For I = 0 To DsRep.Tables(0).Rows.Count - 1
                DsRep.Tables(0).Rows(I)("CompanyLogo") = FileCompanyLogo
            Next
            AgL.PubTempStr = AgL.PubTempStr & "End Reading Logo File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            AgL.PubTempStr = AgL.PubTempStr & "Start Reading Signature File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            Dim FileCompanySign() As Byte
            If File.Exists(mCompanyAuthorisedSignatoryFileName) Then
                FileCompanySign = ReadFile(mCompanyAuthorisedSignatoryFileName)
            Else
                FileCompanySign = ConvertToByteArray(My.Resources.BlankImage)
            End If


            For I = 0 To DsRep.Tables(0).Rows.Count - 1
                DsRep.Tables(0).Rows(I)("CompanyAuthorisedSignature") = FileCompanySign
            Next
            AgL.PubTempStr = AgL.PubTempStr & "End Reading Signature File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            AgL.PubTempStr = AgL.PubTempStr & "Start Reading Payment Qr Code File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            Dim FilePaymentQrCode() As Byte
            If File.Exists(mPaymentQrCodeFileName) Then
                FilePaymentQrCode = ReadFile(mPaymentQrCodeFileName)
            Else
                FilePaymentQrCode = ConvertToByteArray(My.Resources.BlankImage)
            End If


            For I = 0 To DsRep.Tables(0).Rows.Count - 1
                DsRep.Tables(0).Rows(I)("PaymentQrCode") = FilePaymentQrCode
            Next
            AgL.PubTempStr = AgL.PubTempStr & "End Reading Payment Qr Code File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            AgL.PubTempStr = AgL.PubTempStr & "Start Reading EInvoice QR File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            Dim FileEInvoiceQR() As Byte
            If File.Exists(mEInvoiceQrCodeFileName) Then
                FileEInvoiceQR = ReadFile(mEInvoiceQrCodeFileName)
            Else
                FileEInvoiceQR = ConvertToByteArray(My.Resources.BlankImage)
            End If


            For I = 0 To DsRep.Tables(0).Rows.Count - 1
                DsRep.Tables(0).Rows(I)("EInvoiceQrCode") = FileEInvoiceQR
            Next
            AgL.PubTempStr = AgL.PubTempStr & "End Reading EInvoice QR File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf




            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

            If SubReport_QueryList <> "" Then SubReport_QryArr = Split(SubReport_QueryList, "^")
            If SubReport_NameList <> "" Then SubReport_NameArr = Split(SubReport_NameList, "^")


            AgL.PubTempStr = AgL.PubTempStr & "Start Executing Subreport Queries : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                If SubReport_QryArr.Length <> SubReport_NameArr.Length Then
                    MsgBox("Number Of SubReport Qries And SubReport Names Are Not Equal.", MsgBoxStyle.Information)
                    Exit Sub
                End If

                For I = 0 To SubReport_QryArr.Length - 1
                    ReDim Preserve SubReport_DataSetArr(I)
                    SubReport_DataSetArr(I) = New DataSet
                    SubReport_DataSetArr(I) = AgL.FillData(SubReport_QryArr(I).ToString, AgL.GCn)

                    AgPL.CreateFieldDefFile1(SubReport_DataSetArr(I), AgL.PubReportPath & "\" & Report_NameList & SubReport_NameArr(I).ToString & ".ttx", True)
                Next
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End Executing Subreport Queries : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            If FDivisionNameForCustomization(12) = "MAA KI KRIPA" Then
            Else
                AgL.PubCrystalDocument = New ReportDocument
            End If

            AgL.PubTempStr = AgL.PubTempStr & "Start Loading Crystal Report Document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            AgL.PubCrystalDocument.Load(AgL.PubReportPath & "\" & RepName)
            AgL.PubTempStr = AgL.PubTempStr & "End Loading Crystal Report Document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            AgL.PubTempStr = AgL.PubTempStr & "Start Setting Datasource to report document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            AgL.PubCrystalDocument.SetDataSource(DsRep.Tables(0))
            AgL.PubTempStr = AgL.PubTempStr & "End Setting Datasource to report document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            AgL.PubTempStr = AgL.PubTempStr & "Start Setting Datasource to subreports : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                For I = 0 To SubReport_NameArr.Length - 1
                    Try
                        AgL.PubCrystalDocument.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
                    Catch ex As Exception
                    End Try
                Next
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End Setting Datasource to subreports : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            AgL.PubTempStr = AgL.PubTempStr & "Start Assigning PubCrystalDocument to Report Source : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            CType(objRepFrm.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = AgL.PubCrystalDocument
            AgL.PubTempStr = AgL.PubTempStr & "End Assigning PubCrystalDocument to Report Source : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            AgL.PubTempStr = AgL.PubTempStr & "Start setting Formulas : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If TypeOf (objFrm) Is AgTemplate.TempTransaction Then
                ClsMain.Formula_Set(AgL.PubCrystalDocument, Division, CType(objFrm, AgTemplate.TempTransaction).TxtSite_Code.Tag, V_Type, RepTitle)
            ElseIf TypeOf (objFrm) Is AgLibrary.FrmRepDisplay Then
                ClsMain.Formula_Set(AgL.PubCrystalDocument, AgL.PubDivCode, AgL.PubSiteCode, V_Type, RepTitle)
                ClsMain.SetFormulaFilters(AgL.PubCrystalDocument, objFrm)
            Else
                ClsMain.Formula_Set(AgL.PubCrystalDocument, Division, AgL.PubSiteCode, V_Type, RepTitle)
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End setting Formulas : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            'AgPL.Show_Report(objRepFrm, "* " & RepTitle & " *", objFrm.MdiParent)

            If IsSendPDFWhatsapp = True Then

                AgL.PubTempStr = AgL.PubTempStr & "Start To Send Whatsapp : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
                ''objRepFrm.MdiParent = objFrm.MdiParent
                Dim FSendWhatsapp As String = ""
                Dim sender As New WhatsAppSender()
                FSendWhatsapp = sender.SendPDFByWhatsapp(PartyMobileNo, WhatsappFileName)
                AgL.PubTempStr = AgL.PubTempStr & "End Send To Whatsapp : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            Else
                AgL.PubTempStr = AgL.PubTempStr & "Start Printing To Screen : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
                objRepFrm.MdiParent = objFrm.MdiParent
                objRepFrm.Show()
                AgL.PubTempStr = AgL.PubTempStr & "End Printing To Screen : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            End If


            AgL.PubTempStr = AgL.PubTempStr & "Start Insert to Log Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If TypeOf (objFrm) Is AgTemplate.TempTransaction Then
                Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
            Else
                Call AgL.LogTableEntry("", objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End Insert to Log Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub


End Class

