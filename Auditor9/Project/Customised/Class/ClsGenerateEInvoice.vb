Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Imports Newtonsoft.Json
Imports RestSharp
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Net
Imports System.Windows.Forms
Imports TaxProEInvoice.API
Imports Customised.ClsMain

Public Class ClsGenerateEInvoice

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1


    Dim mDocId As String = ""

    Private WorkingFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
    Private eInvSession As eInvoiceSession = New eInvoiceSession(True, True)

    Dim DTDivisionSiteDetail As DataTable = Nothing

    Dim mClientId As String = ""
    Dim mClientSecret As String = ""
    Dim mGspName As String = ""
    Dim mAspUserId As String = ""
    Dim mBaseUrl As String = ""
    Dim mAuthUrl As String = ""
    Dim mAspPassword As String = ""
    Dim mTokenExp As String = ""
    Dim mSek As String = ""
    Dim mAuthToken As String = ""
    Dim mAppKey As String = ""
    Dim mGstin As String = ""
    Dim mPassword As String = ""
    Dim mUserName As String = ""
    Dim mResponceHdr As String = ""
    Dim mEwbBaseUrl As String = ""
    Dim mCancelEWB As String = ""
    Dim mResponse As String = ""


    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property
    Public Property ShowReportType() As String
        Get
            ShowReportType = mShowReportType
        End Get
        Set(ByVal value As String)
            mShowReportType = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Dim mHelpTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM PurchInvoiceDetail H "
    'Public Sub New()
    '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    '    loadApiSetting()
    '    loadApiLoginDetail()
    '    'eInvSession.RefreshAuthTokenCompleted += AddressOf RefreshLoginDetailsDisplay
    '    AddHandler eInvSession.RefreshAuthTokenCompleted, AddressOf RefreshLoginDetailsDisplay
    'End Sub
    Private Sub RefreshLoginDetailsDisplay(ByVal sender As Object, ByVal e As EventArgs)
        DisplayLoginDetail()
    End Sub
    Private Sub DisplayLoginDetail()
        mUserName = eInvSession.eInvApiLoginDetails.UserName
        mPassword = eInvSession.eInvApiLoginDetails.Password
        mGstin = eInvSession.eInvApiLoginDetails.GSTIN
        mAppKey = eInvSession.eInvApiLoginDetails.AppKey
        mAuthToken = eInvSession.eInvApiLoginDetails.AuthToken
        mSek = eInvSession.eInvApiLoginDetails.Sek
        mTokenExp = eInvSession.eInvApiLoginDetails.E_InvoiceTokenExp.ToString()
    End Sub

    Private Sub ApiSetting()
        Try
            eInvSession.eInvApiSetting = New eInvoiceAPISetting()
            eInvSession.eInvApiSetting.GSPName = mGspName
            eInvSession.eInvApiSetting.AspUserId = mAspUserId
            eInvSession.eInvApiSetting.AspPassword = mAspPassword
            eInvSession.eInvApiSetting.client_id = mClientId
            eInvSession.eInvApiSetting.client_secret = mClientSecret
            eInvSession.eInvApiSetting.AuthUrl = mAuthUrl
            eInvSession.eInvApiSetting.BaseUrl = mBaseUrl
            eInvSession.eInvApiSetting.EwbByIRN = mEwbBaseUrl
            eInvSession.eInvApiSetting.CancelEwbUrl = mCancelEWB
            [Shared].SaveAPISetting(eInvSession.eInvApiSetting)
            MessageBox.Show("API Setting saved successfully...")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSaveLoginDetails_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            eInvSession.eInvApiLoginDetails = New eInvoiceAPILoginDetails()
            eInvSession.eInvApiLoginDetails.UserName = mUserName
            eInvSession.eInvApiLoginDetails.Password = mPassword
            eInvSession.eInvApiLoginDetails.GSTIN = mGstin
            eInvSession.eInvApiLoginDetails.AppKey = mAppKey
            eInvSession.eInvApiLoginDetails.AuthToken = mAuthToken
            eInvSession.eInvApiLoginDetails.Sek = mSek
            eInvSession.eInvApiLoginDetails.E_InvoiceTokenExp = Convert.ToDateTime(mTokenExp)
            [Shared].SaveAPILoginDetails(eInvSession.eInvApiLoginDetails)
            MessageBox.Show("API Login Details saved successfully...")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub loadApiSetting()
        mGspName = eInvSession.eInvApiSetting.GSPName
        mAspUserId = eInvSession.eInvApiSetting.AspUserId
        mAspPassword = eInvSession.eInvApiSetting.AspPassword
        mClientId = eInvSession.eInvApiSetting.client_id
        mClientSecret = eInvSession.eInvApiSetting.client_secret
        mAuthUrl = eInvSession.eInvApiSetting.AuthUrl
        mBaseUrl = eInvSession.eInvApiSetting.BaseUrl
        mEwbBaseUrl = eInvSession.eInvApiSetting.EwbByIRN
        mCancelEWB = eInvSession.eInvApiSetting.CancelEwbUrl
    End Sub

    Public Sub loadApiLoginDetail()
        mUserName = eInvSession.eInvApiLoginDetails.UserName
        mPassword = eInvSession.eInvApiLoginDetails.Password
        mGstin = eInvSession.eInvApiLoginDetails.GSTIN
        mAppKey = eInvSession.eInvApiLoginDetails.AppKey
        mAuthToken = eInvSession.eInvApiLoginDetails.AuthToken
        mSek = eInvSession.eInvApiLoginDetails.Sek
        mTokenExp = eInvSession.eInvApiLoginDetails.E_InvoiceTokenExp?.ToString("yyyy-MM-dd HH:mm:ss")
    End Sub

    Private Async Sub AuthToken()
        Dim txnRespWithObj As TxnRespWithObj(Of eInvoiceSession) = Await eInvoiceAPI.GetAuthTokenAsync(eInvSession)

        If txnRespWithObj.IsSuccess Then
            DisplayLoginDetail()
        End If

        mResponceHdr = "Auth Api Responce"
        mResponse = txnRespWithObj.TxnOutcome
    End Sub



    Private Async Sub VerifySignedInv()
        Dim RespJson As String = File.ReadAllText(WorkingFilesPath & "\RespPlGenIRN.m")
        Dim verifyRespPl As VerifyRespPl = New VerifyRespPl()
        Dim respPlGenIRN As RespPlGenIRN = New RespPlGenIRN()
        respPlGenIRN = JsonConvert.DeserializeObject(Of RespPlGenIRN)(RespJson)
        Dim txnRespWithObj As TxnRespWithObj(Of VerifyRespPl) = Await eInvoiceAPI.VerifySignedInvoice(eInvSession, respPlGenIRN)

        If txnRespWithObj.IsSuccess Then
            verifyRespPl.IsVerified = txnRespWithObj.RespObj.IsVerified
            verifyRespPl.JwtIssuerIRP = txnRespWithObj.RespObj.JwtIssuerIRP
            verifyRespPl.VerifiedWithCertificateEffectiveFrom = txnRespWithObj.RespObj.VerifiedWithCertificateEffectiveFrom
            verifyRespPl.CertificateName = txnRespWithObj.RespObj.CertificateName
            verifyRespPl.CertStartDate = txnRespWithObj.RespObj.CertStartDate
            verifyRespPl.CertExpiryDate = txnRespWithObj.RespObj.CertExpiryDate
        Else
            verifyRespPl = txnRespWithObj.RespObj
        End If
    End Sub

    Private Async Sub GetEDetails()
        Dim IrnNo As String = "745df0b4855ee4afb167152bb9d4c4879c496988892027f51fc5bf14be6fc02e"
        Dim txnRespWithObj As TxnRespWithObj(Of RespPlGenIRN) = Await eInvoiceAPI.GetEInvDetailsAsync(eInvSession, IrnNo)

        If txnRespWithObj.IsSuccess Then
            mResponse = JsonConvert.SerializeObject(txnRespWithObj.RespObj)
        Else
            mResponse = txnRespWithObj.TxnOutcome
        End If

        mResponceHdr = "Get IRN Detail Responce..."
    End Sub

    Private Async Sub GSTINDet()
        Dim GSTIN As String = "************"
        Dim txnRespWithObj As TxnRespWithObj(Of RespPlGetGSTIN) = Await eInvoiceAPI.GetGSTINDetailsAsync(eInvSession, GSTIN)

        If txnRespWithObj.IsSuccess Then
            mResponse = JsonConvert.SerializeObject(txnRespWithObj.RespObj)
        Else
            mResponse = txnRespWithObj.TxnOutcome
        End If

        mResponceHdr = "Get GSTIN Detail Responce..."
    End Sub



    Private Async Sub btnGetEDetailbyAck_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Async Sub DecryptedAuthToken()
        Dim client As RestClient = New RestClient("https://api.taxprogsp.co.in/eivital/dec/v1.03/auth?aspid=1234568817&password=$RadhaMadhav1&Gstin=27AACCC1596Q1Z2&Username=CHARTEREDINFO_7&eInvPwd=*88Taxpro")
        Dim request As RestRequest = New RestRequest(Method.[GET])
        request.AddHeader("Gstin", "27AACCC1596Q1Z2")
        request.AddHeader("user_name", "CHARTEREDINFO_7")
        request.AddHeader("aspid", "1234568817")
        request.AddHeader("password", "$RadhaMadhav1")
        request.AddHeader("Content-Type", "application/json; charset=utf-8")
        request.RequestFormat = DataFormat.Json
        Dim response As IRestResponse = Await client.ExecuteTaskAsync(request)
        mResponse = response.Content
    End Sub

    Private Async Sub DecGenIRN(ByVal sender As Object, ByVal e As EventArgs)
        Dim strJson As String = File.ReadAllText(WorkingFilesPath & "\ProductionSampleJsonGenIRN.m")
        Dim client As RestClient = New RestClient("https://api.taxprogsp.co.in/eicore/dec/v1.03/Invoice?aspid=************&password=************&Gstin=************&AuthToken=jSNGkXqh8RshEAf91CAFMMdcp&user_name=************&QrCodeSize=250")
        Dim request As RestRequest = New RestRequest(Method.POST)
        request.AddHeader("Gstin", "************")
        request.AddHeader("user_name", "************")
        request.AddHeader("AuthToken", "jSNGkXqh8RshEAf91CAFMMdcp")
        request.AddHeader("aspid", "************")
        request.AddHeader("password", "************")
        request.AddHeader("Content-Type", "application/json; charset=utf-8")
        request.RequestFormat = DataFormat.Json
        request.AddParameter("application/json", strJson, ParameterType.RequestBody)
        Dim response As IRestResponse = Await client.ExecuteTaskAsync(request)
        Dim respPl As RespPl = New RespPl()
        respPl = JsonConvert.DeserializeObject(Of RespPl)(response.Content)
        Dim respPlGenIRNDec As RespPlGenIRNDec = New RespPlGenIRNDec()
        respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
        mResponse = respPlGenIRNDec.Irn
        Dim qrImg As Byte() = Convert.FromBase64String(respPlGenIRNDec.QrCodeImage)
        Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(Bitmap))
        Dim bitmap1 As Bitmap = CType(tc.ConvertFrom(qrImg), Bitmap)
        bitmap1.Save(WorkingFilesPath & "\qr.png")
    End Sub

    Private Sub btnDecCancelIRN_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Async Sub GetEinvDetailByDec(ByVal sender As Object, ByVal e As EventArgs)
        Dim client As RestClient = New RestClient("http://testapi.taxprogsp.co.in/gstcore/dec/v1.01/Invoice/irn/b27676323d37f357c02495580677d434be958a77870b7b5636d6d551c479818b?aspid=******&password=******&Gstin=************&AuthToken=fBM4dCRuLSrMI8SwyR1kYm0Mc&user_name=************")
        Dim request As RestRequest = New RestRequest(Method.[GET])
        request.AddHeader("Gstin", "************")
        request.AddHeader("user_name", "************")
        request.AddHeader("AuthToken", "fBM4dCRuLSrMI8SwyR1kYm0Mc")
        request.AddHeader("aspid", "******")
        request.AddHeader("password", "*******")
        request.AddHeader("Content-Type", "application/json; charset=utf-8")
        request.RequestFormat = DataFormat.Json
        Dim response As IRestResponse = Await client.ExecuteTaskAsync(request)
        Dim respPl As RespPl = New RespPl()
        respPl = JsonConvert.DeserializeObject(Of RespPl)(response.Content)
        Dim respPlGenIRNDec As RespPlGenIRNDec = New RespPlGenIRNDec()
        respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
        mResponse = JsonConvert.SerializeObject(respPlGenIRNDec)
    End Sub

    Private Async Sub GenEWB(ByVal sender As Object, ByVal e As EventArgs)
        Dim reqPlGenEwbByIRN As ReqPlGenEwbByIRN = New ReqPlGenEwbByIRN()
        reqPlGenEwbByIRN.Irn = "48004d31b8f2afd2bb660911fd58602753c069cfec3bca58eda300822ce0b540"
        reqPlGenEwbByIRN.TransId = "27AACFM5833D1ZH"
        reqPlGenEwbByIRN.TransMode = "3"
        reqPlGenEwbByIRN.TransDocNo = "DOC7"
        reqPlGenEwbByIRN.TransDocDt = "18/11/2020"
        reqPlGenEwbByIRN.VehNo = "ka123458"
        reqPlGenEwbByIRN.Distance = 100
        reqPlGenEwbByIRN.VehType = "R"
        reqPlGenEwbByIRN.TransName = "DFHGF"
        reqPlGenEwbByIRN.ExpShipDtls = New ExportShipDetails()
        reqPlGenEwbByIRN.ExpShipDtls.Addr1 = "7th block, kuvempu layout"
        reqPlGenEwbByIRN.ExpShipDtls.Addr2 = "kuvempu layout"
        reqPlGenEwbByIRN.ExpShipDtls.Loc = "Banagalore"
        reqPlGenEwbByIRN.ExpShipDtls.Pin = 562160
        reqPlGenEwbByIRN.ExpShipDtls.Stcd = "29"
        reqPlGenEwbByIRN.DispDtls = New DispatchedDetails()
        reqPlGenEwbByIRN.DispDtls.Nm = "ABC company pvt ltd"
        reqPlGenEwbByIRN.DispDtls.Addr1 = "7th block, kuvempu layout"
        reqPlGenEwbByIRN.DispDtls.Addr2 = "kuvempu layout"
        reqPlGenEwbByIRN.DispDtls.Loc = "Banagalore"
        reqPlGenEwbByIRN.DispDtls.Pin = 562160
        reqPlGenEwbByIRN.DispDtls.Stcd = "29"
        Dim txnRespWithObj As TxnRespWithObj(Of RespPlGenEwbByIRN) = Await eInvoiceAPI.GenEwbByIRNAsync(eInvSession, reqPlGenEwbByIRN)
        Dim ErrorCodes As String = ""
        Dim ErrorDesc As String = ""
        mResponse = ""

        If txnRespWithObj.IsSuccess Then
            mResponse = JsonConvert.SerializeObject(txnRespWithObj.RespObj)
        Else

            If txnRespWithObj.ErrorDetails IsNot Nothing Then

                For Each errPl As RespErrDetailsPl In txnRespWithObj.ErrorDetails
                    ErrorCodes += errPl.ErrorCode & ","
                    ErrorDesc += errPl.ErrorCode & ": " + errPl.ErrorMessage & Environment.NewLine
                    mResponse = ErrorDesc
                Next
            End If
        End If

        mResponceHdr = "Generate IRN Responce..."
    End Sub

    Private Async Sub DecGenEwbByIRN(ByVal sender As Object, ByVal e As EventArgs)
        Dim reqPlGenEwbByIRN As ReqPlGenEwbByIRN = New ReqPlGenEwbByIRN()
        reqPlGenEwbByIRN.Irn = "cc47964d094e5efa9813ca1f5d1822a67e55995c5800f979598dac8669ab5d51"
        reqPlGenEwbByIRN.TransId = "27AACFM5833D1ZH"
        reqPlGenEwbByIRN.TransMode = "1"
        reqPlGenEwbByIRN.TransDocNo = "DOC113"
        reqPlGenEwbByIRN.TransDocDt = "21/09/2020"
        reqPlGenEwbByIRN.VehNo = "ka123458"
        reqPlGenEwbByIRN.Distance = 100
        reqPlGenEwbByIRN.VehType = "R"
        reqPlGenEwbByIRN.TransName = "DFHGF"
        Dim client As RestClient = New RestClient("https://api.taxprogsp.co.in/eiewb/dec/v1.03/ewaybill?aspid=************&password=************&Gstin=************&AuthToken=jSNGkXqh8RshEAf91CAFMMdcp&user_name=************")
        Dim request As RestRequest = New RestRequest(Method.POST)
        request.AddHeader("Gstin", "************")
        request.AddHeader("user_name", "************")
        request.AddHeader("AuthToken", "jSNGkXqh8RshEAf91CAFMMdcp")
        request.AddHeader("aspid", "************")
        request.AddHeader("password", "************")
        request.AddHeader("Content-Type", "application/json; charset=utf-8")
        request.RequestFormat = DataFormat.Json
        Dim jsonStr As String = JsonConvert.SerializeObject(reqPlGenEwbByIRN)
        request.AddBody(reqPlGenEwbByIRN)
        Dim response As IRestResponse = Await client.ExecuteTaskAsync(request)
        Dim respPl As RespPl = New RespPl()
        respPl = JsonConvert.DeserializeObject(Of RespPl)(response.Content)
        Dim resp As RespPlGenEwbByIRN = New RespPlGenEwbByIRN()
        resp = JsonConvert.DeserializeObject(Of RespPlGenEwbByIRN)(respPl.Data.Replace("""{", "{").Replace("}""", "}").Replace("\""", """"))
        mResponse = JsonConvert.SerializeObject(resp)
    End Sub

    Private Async Sub CancelEwbByDec(ByVal sender As Object, ByVal e As EventArgs)
        Dim action As String = "CANEWB"
        Dim reqPlCancelEWB As ReqPlCancelEWB = New ReqPlCancelEWB()
        reqPlCancelEWB.ewbNo = 211223256570
        reqPlCancelEWB.cancelRsnCode = 2
        reqPlCancelEWB.cancelRmrk = "Cancelled the order"
        Dim client As RestClient = New RestClient("https://api.taxprogsp.co.in/v1.03/dec/ewayapi?action=CANEWB&aspid=************&password=************&gstin=************&authtoken=jSNGkXqh8RshEAf91CAFMMdcp&username=************")
        Dim request As RestRequest = New RestRequest(Method.POST)
        request.AddHeader("Gstin", "************")
        request.AddHeader("username", "************")
        request.AddHeader("AuthToken", "jSNGkXqh8RshEAf91CAFMMdcp")
        request.AddHeader("aspid", "************")
        request.AddHeader("password", "************")
        request.AddHeader("Content-Type", "application/json; charset=utf-8")
        request.RequestFormat = DataFormat.Json
        Dim strJson As String = JsonConvert.SerializeObject(reqPlCancelEWB)
        request.AddParameter("application/json", strJson, ParameterType.RequestBody)
        Dim response As IRestResponse = Await client.ExecuteTaskAsync(request)
        Dim resp As RespPlCancelEWB = New RespPlCancelEWB()
        resp = JsonConvert.DeserializeObject(Of RespPlCancelEWB)(response.Content)
        mResponse = JsonConvert.SerializeObject(resp)
    End Sub

    Private Async Sub CancelEwayBill(ByVal sender As Object, ByVal e As EventArgs)
        Dim action As String = "CANEWB"
        Dim reqPlCancelEWB As ReqPlCancelEWB = New ReqPlCancelEWB()
        reqPlCancelEWB.ewbNo = 251221608254
        reqPlCancelEWB.cancelRsnCode = 2
        reqPlCancelEWB.cancelRmrk = "Cancelled the order"
        Dim txnRespWithObj As TxnRespWithObj(Of RespPlCancelEWB) = Await eInvoiceAPI.CancelEWBAsync(eInvSession, reqPlCancelEWB, action)

        If txnRespWithObj.IsSuccess Then
            mResponse = JsonConvert.SerializeObject(txnRespWithObj.RespObj)
        Else
            mResponse = txnRespWithObj.TxnOutcome
        End If

        mResponceHdr = "Generate IRN Responce..."
    End Sub

    Private Sub Browse(ByVal sender As Object, ByVal e As EventArgs)
        Dim size As Integer = -1
        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog()
        Dim result As DialogResult = openFileDialog1.ShowDialog()

        If result = DialogResult.OK Then
            Dim filename As String = openFileDialog1.FileName
            mResponceHdr = filename

            Try
                mResponse = File.ReadAllText(filename)
            Catch __unusedIOException1__ As IOException
            End Try
        End If
    End Sub

    Private Sub LoadProduction(ByVal sender As Object, ByVal e As EventArgs)
        mGspName = "TaxPro_Production"
        mAuthUrl = "https://api.taxprogsp.co.in/eivital/v1.03"
        mBaseUrl = "https://api.taxprogsp.co.in/eicore/v1.03"
        mEwbBaseUrl = "https://api.taxprogsp.co.in/eiewb/v1.03"
        mCancelEWB = "https://api.taxprogsp.co.in/v1.03"
    End Sub

    Private Sub LoadSandBoxSetting(ByVal sender As Object, ByVal e As EventArgs)
        mGspName = "TaxPro_Sandbox"
        mAuthUrl = "http://testapi.taxprogsp.co.in/eivital/v1.03"
        mBaseUrl = "http://testapi.taxprogsp.co.in/eicore/v1.03"
        mEwbBaseUrl = "http://testapi.taxprogsp.co.in/eiewb/v1.03"
        mCancelEWB = "http://testapi.taxprogsp.co.in/v1.03"
        mUserName = "************"
        mPassword = "abcd1234*"
        mGstin = "************"
    End Sub


    Private Async Sub GetSyncGSTINDetails(ByVal sender As Object, ByVal e As EventArgs)
        If True Then
            Dim GSTIN As String = "33GSPTN1882G1Z3"
            Dim txnRespWithObj As TxnRespWithObj(Of RespPlGetGSTIN) = Await eInvoiceAPI.SyncGSTINAsync(eInvSession, GSTIN)

            If txnRespWithObj.IsSuccess Then
                mResponse = JsonConvert.SerializeObject(txnRespWithObj.RespObj)
            Else
                mResponse = txnRespWithObj.TxnOutcome
            End If

            mResponceHdr = "Get Sync GSTIN Detail Responce..."
        End If
    End Sub
    Private Async Sub GetIRNDetailsByDocDetails(ByVal sender As Object, ByVal e As EventArgs)
        If True Then
            Dim DocType As String = "INV"
            Dim DocNum As String = "DOC/189661"
            Dim DocDate As String = "01/02/2021"
            Dim txnRespWithObj As TxnRespWithObj(Of RespPlGenIRN) = Await eInvoiceAPI.GetIRNDetailsByDocDetailsAsync(eInvSession, DocType, DocNum, DocDate, 250)

            If txnRespWithObj.IsSuccess Then
                mResponse = JsonConvert.SerializeObject(txnRespWithObj.RespObj)
            Else
                mResponse = txnRespWithObj.TxnOutcome
            End If

            mResponceHdr = "Get IRN Details By Doc Detail Responce..."
        End If
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function
    Public Sub Ini_Grid()
        Try
            'mQry = "Select 'Summary' as Code, 'Summary' as Name 
            '        Union All Select 'Detail' as Code, 'Detail' as Name "
            'ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Summary",,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthStartDate(AgL.PubLoginDate))
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthEndDate(AgL.PubLoginDate))
            'ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            'ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            'ReportFrm.CreateHelpGrid("Mobile", "Mobile", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.StringType, "", "")
            'ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
            'ReportFrm.FilterGrid.Rows(rowMobile).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcGenerateEInvoice()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, Optional bDocId As String = "")
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        LoadEInvoiceSessionValues()
        loadApiSetting()
        loadApiLoginDetail()
        'eInvSession.RefreshAuthTokenCompleted += AddressOf RefreshLoginDetailsDisplay
        AddHandler eInvSession.RefreshAuthTokenCompleted, AddressOf RefreshLoginDetailsDisplay
        AuthToken()
        mDocId = bDocId
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcGenerateEInvoice(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mSaleCondStr$ = ""
            Dim mPurchaseReturnCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Generate E-Invoice"


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If


            If mDocId <> "" Then
                mQry = " Select H.V_Date, H.V_Type || '-' || H.ManualRefNo As InvoiceNo From SaleInvoice H Where H.DocId = '" & mDocId & "'"
                Dim DtInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtInvoiceDetail.Rows.Count > 0 Then
                    ReportFrm.FilterGrid.Item(GFilter, rowFromDate).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                    ReportFrm.FilterGrid.Item(GFilter, rowToDate).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                End If
                mSaleCondStr = " Where H.DocId = '" & mDocId & "' "
            Else
                mSaleCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            End If
            mSaleCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mSaleCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mSaleCondStr += " And Vt.NCat = 'SI' "

            mQry = "SELECT H.V_Type AS InvoiceType, H.V_Type + '-' + H.ManualRefNo AS InvoiceNo, H.V_Date AS InvoiceDate, 
                H.SaleToPartySalesTaxNo, H.SaleToPartyName, H.SaleToPartyAddress,
                C.CityName AS SaleToPartyCityName, H.SaleToPartyPinCode, S.ManualCode AS SaleToPartyStateCode,
                ShipParty.Name AS ShipToPartyName, ShipParty.Address AS ShipToPartyAddress, ShipCity.CityName AS ShipToPartyCity, 
                ShipParty.Pin AS ShipToPartyPinCode, ShipState.ManualCode AS ShipToPartyStateCode,
                CASE WHEN I.ItemType = 'SP' THEN 'Y' ELSE 'N' END AS IsService, I.Description AS ItemDesc,
                IsNull(I.HSN, Ic.HSN) AS HSN, 
                L.Qty, L.Unit, L.Rate, L.Qty * L.Rate AS Amount, L.DiscountAmount, L.Taxable_Amount, Sti.GrossTaxRate,
                L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5,
                H.Taxable_Amount AS Header_Taxable_Amount, H.Tax1 AS Header_Tax1, H.Tax2 AS Header_Tax2, 
                H.Tax3 AS Header_Tax3, H.Tax4 AS Header_Tax4, H.Tax5 AS Header_Tax5
                FROM SaleInvoice H 
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.SaleToParty = VReg.SubCode
                LEFT JOIN City C ON H.SaleToPartyCity = C.CityCode
                LEFT JOIN State S ON C.State = S.Code
                LEFT JOIN Subgroup ShipParty ON H.ShipToParty = ShipParty.SubCode
                LEFT JOIN City ShipCity ON ShipParty.CityCode = ShipCity.CityCode
                LEFT JOIN State ShipState ON ShipCity.State = ShipState.Code
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId
                LEFT JOIN Item I ON L.Item = I.Code
                LEFT JOIN Item Ic ON I.ItemCategory = Ic.Code
                LEFT JOIN PostingGroupSalesTaxItem Sti ON L.SalesTaxGroupItem = Sti.Description
                WHERE H.DocID = 'E2    SI 2020   11904' "



            'Sale Invoice Qry
            mQry = "Select " & IIf(mDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocId  As SearchCode, Vt.Description As VoucherType, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNo, 
                strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, IfNull(H.Net_Amount,0) As InvoiceValue,
                Sg.DispName As Party, 
                Case When H.ShipToParty Is Not Null Then ShipTo.Pin Else H.SaleToPartyPinCode End As PinCode, 
                Case When H.ShipToParty Is Not Null Then ShipToState.Description Else S.Description End As State, 
                TSg.DispName As Transporter,
                VDist.Distance As Distance, H.EInvoiceIRN As Irn
                From SaleInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                LEFT JOIN SubGroup ShipTo On H.ShipToParty = ShipTo.SubCode
                LEFT JOIN City ShipToCity On ShipTo.CityCode = ShipToCity.CityCode
                LEFT JOIN State ShipToState On ShipToCity.State = ShipToState.Code
                LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail
                            Group By SubCode) As Hlt On H.SaleToParty = Hlt.SubCode
                LEFT JOIN SubGroup TSg ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.SaleToParty = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail 
                            Where Site_Code = '" & AgL.PubSiteCode & "' 
                            And Div_Code = '" & AgL.PubDivCode & "') As VDist On IfNull(H.ShipToParty,H.SaleToParty) = VDist.SubCode " & mSaleCondStr

            DsHeader = AgL.FillData(mQry, AgL.GCn)


            mQry = " SELECT H.DocID, IfNull(I.HSN,Ic.HSN) As HSN
                    FROM SaleInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Item I ON L.Item = I.Code 
                    LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                    " & mSaleCondStr &
                    " And IfNull(I.HSN,Ic.HSN) Is Null "

            mQry = mQry + " UNION ALL "

            mQry = mQry + " SELECT H.DocID, IfNull(I.HSN,Ic.HSN) As HSN
                    FROM PurchInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Item I ON L.Item = I.Code 
                    LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                    " & mSaleCondStr &
                    " And IfNull(I.HSN,Ic.HSN) Is Null "
            Dim DtLine As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I As Integer = 0 To DsHeader.Tables(0).Rows.Count - 1
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")) = "" Then
                    If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                    DsHeader.Tables(0).Rows(I)("Exception") += "Party Pin Code is blank."
                End If
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Distance")) = "" Or AgL.VNull(DsHeader.Tables(0).Rows(I)("Distance")) = 0 Then
                    If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                    DsHeader.Tables(0).Rows(I)("Exception") += "Party Distance is blank."
                End If
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")) <> "" Then
                    If Not System.Text.RegularExpressions.Regex.IsMatch(AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")), "^[0-9 ]+$") Then
                        If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                        DsHeader.Tables(0).Rows(I)("Exception") += "Party Pin Code is not valid."
                    End If
                End If

                Dim DtRowLineDetail_ForHeader As DataRow() = DtLine.Select(" DocId = " + AgL.Chk_Text(DsHeader.Tables(0).Rows(I)("SearchCode")))
                If DtRowLineDetail_ForHeader.Length > 0 Then
                    For M As Integer = 0 To DtRowLineDetail_ForHeader.Length - 1
                        If AgL.XNull(DtRowLineDetail_ForHeader(M)("HSN")) = "" Then
                            If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                            DsHeader.Tables(0).Rows(I)("Exception") += "Some items have blank HSN Codes."
                        End If
                    Next
                End If


                'If AgL.XNull(DsRep.Tables(0).Rows(I)("Exception")) <> "" Then
                '    DsRep.Tables(0).Rows(I)("Tick") = "o"
                'End If
            Next

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            mQry = "Select 'Generate E-Invoice' As MenuText, 'GenIRN' As FunctionName "
            mQry += "UNION ALL "
            mQry += "Select 'Cancel E-Invoice' As MenuText, 'CancelIRN' As FunctionName "
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            ReportFrm.Text = "E Invoice Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcGenerateEInvoice"
            ReportFrm.DTCustomMenus = DtMenuList

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Public Async Sub GenIRN(DGL As AgControls.AgDataGrid)
        Dim I As Integer = 0
        Dim mSearchCode As String = ""

        For I = 0 To DGL.Rows.Count - 1
            If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
                mSearchCode = DGL.Item("Search Code", I).Value

                mQry = "SELECT H.Div_Code, H.Site_Code, H.V_Type AS InvoiceType, H.V_Type + '-' + H.ManualRefNo AS InvoiceNo, H.V_Date AS InvoiceDate, 
                        H.SaleToPartySalesTaxNo, H.SaleToPartyName, H.SaleToPartyAddress,
                        C.CityName AS SaleToPartyCityName, H.SaleToPartyPinCode, S.ManualCode AS SaleToPartyStateCode,
                        VShipToPartyReg.SalesTaxNo As ShipToPartySalesTaxNo, ShipParty.Name AS ShipToPartyName, ShipParty.Address AS ShipToPartyAddress, ShipCity.CityName AS ShipToPartyCity, 
                        ShipParty.Pin AS ShipToPartyPinCode, ShipState.ManualCode AS ShipToPartyStateCode,
                        CASE WHEN I.ItemType = 'SP' THEN 'Y' ELSE 'N' END AS IsService, I.Description AS ItemDesc,
                        IsNull(I.HSN, Ic.HSN) AS HSN, 
                        L.Qty, L.Unit, L.Rate, L.Qty * L.Rate AS Amount, L.DiscountAmount, L.Taxable_Amount, Sti.GrossTaxRate,
                        L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Net_Amount,
                        H.Taxable_Amount AS Header_Taxable_Amount, H.Tax1 AS Header_Tax1, H.Tax2 AS Header_Tax2, 
                        H.Tax3 AS Header_Tax3, H.Tax4 AS Header_Tax4, H.Tax5 AS Header_Tax5,
                        H.Round_Off AS Header_Round_Off, H.Net_Amount AS Header_Net_Amount
                        FROM SaleInvoice H 
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                                    From SubgroupRegistration 
                                    Where RegistrationType = 'Sales Tax No') As VReg On H.SaleToParty = VReg.SubCode
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                                    From SubgroupRegistration 
                                    Where RegistrationType = 'Sales Tax No') As VShipToPartyReg On H.ShipToParty = VShipToPartyReg.SubCode
                        LEFT JOIN City C ON H.SaleToPartyCity = C.CityCode
                        LEFT JOIN State S ON C.State = S.Code
                        LEFT JOIN Subgroup ShipParty ON H.ShipToParty = ShipParty.SubCode
                        LEFT JOIN City ShipCity ON ShipParty.CityCode = ShipCity.CityCode
                        LEFT JOIN State ShipState ON ShipCity.State = ShipState.Code
                        LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId
                        LEFT JOIN Item I ON L.Item = I.Code
                        LEFT JOIN Item Ic ON I.ItemCategory = Ic.Code
                        LEFT JOIN PostingGroupSalesTaxItem Sti ON L.SalesTaxGroupItem = Sti.Description
                        WHERE H.DocID = '" & mSearchCode & "'"
                Dim DtSaleInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                'mQry = " Select VReg.SalesTaxNo As DivisionSalesTaxNo, Sg.DispName As DivisionName, 
                '        Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                '             Then IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'')
                '             Else Sg.Address END As DivisionAddress,
                '        Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                '             Then Sm.PinNo Else Sg.PIN END As DivisionPinCode, 
                '        Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                '             Then Sc.CityName Else C.CityName END As DivisionCityName,
                '        Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                '             Then SS.ManualCode Else S.ManualCode END As DivisionStateCode
                '        From Division D
                '        LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                '        LEFT JOIN City C On Sg.CityCode = C.CityCode
                '        LEFT JOIN State S On C.State = S.Code
                '        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                '                    From SubgroupRegistration 
                '                    Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
                '        LEFT JOIN SiteMast Sm ON 1=1
                '        LEFT JOIN City SC On Sm.City_Code = SC.CityCode
                '        LEFT JOIN State SS On SC.State = SS.Code
                '        Where D.Div_Code = '" & AgL.XNull(DtSaleInvoice.Rows(0)("Div_Code")) & "'
                '        And Sm.Code = '" & AgL.XNull(DtSaleInvoice.Rows(0)("Site_Code")) & "'"
                'Dim DTDivisionDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                'If AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo")) = "" Then
                '    Dim mDivisionSiteSalesTaxNo As String = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteSalesTaxNo,
                '                                        SettingType.General, AgL.XNull(DtSaleInvoice.Rows(0)("Div_Code")),
                '                                        DtSaleInvoice.Rows(0)("Site_Code"), "", "", "", "", "")
                '    If mDivisionSiteSalesTaxNo = "" Then
                '        MsgBox("Company GST No. is blank.", MsgBoxStyle.Information)
                '        Exit Sub
                '    Else
                '        DTDivisionSiteDetail.Rows(0)("DivisionSalesTaxNo") = mDivisionSiteSalesTaxNo
                '    End If
                'End If


                Dim reqPlGenIRN As ReqPlGenIRN = New ReqPlGenIRN()
                reqPlGenIRN.Version = "1.1"

                reqPlGenIRN.TranDtls = New ReqPlGenIRN.TranDetails()
                reqPlGenIRN.TranDtls.TaxSch = "GST"
                reqPlGenIRN.TranDtls.SupTyp = "B2B"

                reqPlGenIRN.DocDtls = New ReqPlGenIRN.DocSetails()
                reqPlGenIRN.DocDtls.Typ = "INV"
                reqPlGenIRN.DocDtls.No = AgL.XNull(DtSaleInvoice.Rows(0)("InvoiceNo"))
                reqPlGenIRN.DocDtls.Dt = CDate(AgL.XNull(DtSaleInvoice.Rows(0)("InvoiceDate"))).ToString("dd/MM/yyyy")

                reqPlGenIRN.SellerDtls = New ReqPlGenIRN.SellerDetails()
                reqPlGenIRN.SellerDtls.Gstin = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo"))
                reqPlGenIRN.SellerDtls.LglNm = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionName"))
                reqPlGenIRN.SellerDtls.TrdNm = Nothing
                reqPlGenIRN.SellerDtls.Addr1 = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionAddress"))
                reqPlGenIRN.SellerDtls.Addr2 = Nothing
                reqPlGenIRN.SellerDtls.Loc = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionCityName"))
                reqPlGenIRN.SellerDtls.Pin = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionPinCode"))
                reqPlGenIRN.SellerDtls.Stcd = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionStateCode"))
                reqPlGenIRN.SellerDtls.Ph = Nothing
                reqPlGenIRN.SellerDtls.Em = Nothing

                'reqPlGenIRN.SellerDtls.Gstin = "34AACCC1596Q002"
                'reqPlGenIRN.SellerDtls.LglNm = "ABC company pvt ltd"
                'reqPlGenIRN.SellerDtls.TrdNm = Nothing
                'reqPlGenIRN.SellerDtls.Addr1 = "5th block, kuvempu layout"
                'reqPlGenIRN.SellerDtls.Addr2 = Nothing
                'reqPlGenIRN.SellerDtls.Loc = "GANDHINAGAR"
                'reqPlGenIRN.SellerDtls.Pin = 605005
                'reqPlGenIRN.SellerDtls.Stcd = "34"
                'reqPlGenIRN.SellerDtls.Ph = Nothing
                'reqPlGenIRN.SellerDtls.Em = Nothing

                reqPlGenIRN.BuyerDtls = New ReqPlGenIRN.BuyerDetails()
                reqPlGenIRN.BuyerDtls.Gstin = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartySalesTaxNo"))
                reqPlGenIRN.BuyerDtls.LglNm = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyName"))
                reqPlGenIRN.BuyerDtls.TrdNm = Nothing
                reqPlGenIRN.BuyerDtls.Pos = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyStateCode"))
                reqPlGenIRN.BuyerDtls.Addr1 = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyAddress"))
                reqPlGenIRN.BuyerDtls.Addr2 = Nothing
                reqPlGenIRN.BuyerDtls.Loc = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyCityName"))
                reqPlGenIRN.BuyerDtls.Pin = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyPinCode"))
                reqPlGenIRN.BuyerDtls.Stcd = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyStateCode"))
                reqPlGenIRN.BuyerDtls.Ph = Nothing
                reqPlGenIRN.BuyerDtls.Em = Nothing

                'reqPlGenIRN.DispDtls = New ReqPlGenIRN.DispatchedDetails()
                'reqPlGenIRN.DispDtls.Nm = "ABC company pvt ltd"
                'reqPlGenIRN.DispDtls.Addr1 = "7th block, kuvempu layout"
                'reqPlGenIRN.DispDtls.Addr2 = Nothing
                'reqPlGenIRN.DispDtls.Loc = "Banagalore"
                'reqPlGenIRN.DispDtls.Pin = 560043
                'reqPlGenIRN.DispDtls.Stcd = "29"

                reqPlGenIRN.DispDtls = Nothing

                reqPlGenIRN.ShipDtls = New ReqPlGenIRN.ShippedDetails()
                If AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyName")) <> "" Then
                    reqPlGenIRN.ShipDtls.Gstin = AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartySalesTaxNo"))
                    reqPlGenIRN.ShipDtls.LglNm = AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyName"))
                    reqPlGenIRN.ShipDtls.TrdNm = Nothing
                    reqPlGenIRN.ShipDtls.Addr1 = AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyAddress"))
                    reqPlGenIRN.ShipDtls.Addr2 = Nothing
                    reqPlGenIRN.ShipDtls.Loc = AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyCity"))
                    reqPlGenIRN.ShipDtls.Pin = AgL.VNull(DtSaleInvoice.Rows(0)("ShipToPartyPinCode"))
                    reqPlGenIRN.ShipDtls.Stcd = AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyStateCode"))
                Else
                    reqPlGenIRN.ShipDtls = Nothing
                End If


                'reqPlGenIRN.ShipDtls.Gstin = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartySalesTaxNo"))
                'reqPlGenIRN.ShipDtls.LglNm = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyName"))
                'reqPlGenIRN.ShipDtls.TrdNm = Nothing
                'reqPlGenIRN.ShipDtls.Addr1 = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyAddress"))
                'reqPlGenIRN.ShipDtls.Addr2 = Nothing
                'reqPlGenIRN.ShipDtls.Loc = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyCityName"))
                'reqPlGenIRN.ShipDtls.Pin = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyPinCode"))
                'reqPlGenIRN.ShipDtls.Stcd = AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyStateCode"))



                reqPlGenIRN.ItemList = New List(Of ReqPlGenIRN.ItmList)()
                For J As Integer = 0 To DtSaleInvoice.Rows.Count - 1
                    Dim itm As ReqPlGenIRN.ItmList = New ReqPlGenIRN.ItmList()
                    itm.SlNo = J + 1
                    itm.IsServc = AgL.XNull(DtSaleInvoice.Rows(J)("IsService"))
                    itm.PrdDesc = AgL.XNull(DtSaleInvoice.Rows(J)("ItemDesc"))
                    itm.HsnCd = DtSaleInvoice.Rows(J)("HSN")
                    itm.BchDtls = Nothing
                    itm.Qty = AgL.VNull(DtSaleInvoice.Rows(J)("Qty"))
                    itm.Unit = AgL.XNull(DtSaleInvoice.Rows(J)("Unit"))
                    itm.UnitPrice = AgL.VNull(DtSaleInvoice.Rows(J)("Rate"))
                    itm.TotAmt = AgL.VNull(DtSaleInvoice.Rows(J)("Amount"))
                    itm.Discount = AgL.VNull(DtSaleInvoice.Rows(J)("DiscountAmount"))
                    itm.AssAmt = AgL.VNull(DtSaleInvoice.Rows(J)("Taxable_Amount"))
                    itm.GstRt = AgL.VNull(DtSaleInvoice.Rows(J)("GrossTaxRate"))
                    itm.IgstAmt = AgL.VNull(DtSaleInvoice.Rows(J)("Tax1"))
                    itm.CgstAmt = AgL.VNull(DtSaleInvoice.Rows(J)("Tax2"))
                    itm.SgstAmt = AgL.VNull(DtSaleInvoice.Rows(J)("Tax3"))
                    itm.CesRt = 0.0
                    itm.CesAmt = 0.0
                    itm.CesNonAdvlAmt = 0.0
                    itm.StateCesRt = 0.0
                    itm.StateCesAmt = 0.0
                    itm.StateCesNonAdvlAmt = 0.0
                    itm.OthChrg = 0.0
                    itm.TotItemVal = AgL.VNull(DtSaleInvoice.Rows(J)("Net_Amount"))
                    itm.AttribDtls = Nothing
                    reqPlGenIRN.ItemList.Add(itm)
                Next

                reqPlGenIRN.PayDtls = Nothing
                reqPlGenIRN.RefDtls = Nothing
                reqPlGenIRN.AddlDocDtls = Nothing
                reqPlGenIRN.ExpDtls = Nothing
                reqPlGenIRN.EwbDtls = Nothing

                reqPlGenIRN.ValDtls = New ReqPlGenIRN.ValDetails()
                reqPlGenIRN.ValDtls.AssVal = AgL.VNull(DtSaleInvoice.Rows(0)("Header_Taxable_Amount"))
                reqPlGenIRN.ValDtls.IgstVal = AgL.VNull(DtSaleInvoice.Rows(0)("Header_Tax1"))
                reqPlGenIRN.ValDtls.CgstVal = AgL.VNull(DtSaleInvoice.Rows(0)("Header_Tax2"))
                reqPlGenIRN.ValDtls.SgstVal = AgL.VNull(DtSaleInvoice.Rows(0)("Header_Tax3"))
                reqPlGenIRN.ValDtls.CesVal = 0.0
                reqPlGenIRN.ValDtls.StCesVal = 0.0
                reqPlGenIRN.ValDtls.RndOffAmt = AgL.VNull(DtSaleInvoice.Rows(0)("Header_Round_Off"))
                reqPlGenIRN.ValDtls.TotInvVal = AgL.VNull(DtSaleInvoice.Rows(0)("Header_Net_Amount"))
                Dim txnRespWithObj As TxnRespWithObj(Of RespPlGenIRN) = Await eInvoiceAPI.GenIRNAsync(eInvSession, reqPlGenIRN)

                'Dim reqPlGenIRN As ReqPlGenIRN = New ReqPlGenIRN()
                'Dim ReqJson As String = File.ReadAllText(mResponceHdr)
                'Dim txnRespWithObj As TxnRespWithObj(Of RespPlGenIRN) = Await eInvoiceAPI.GenIRNAsync(eInvSession, mResponse, 250)





                Dim respPlGenIRN As RespPlGenIRN = txnRespWithObj.RespObj
                Dim ErrorCodes As String = ""
                Dim ErrorDesc As String = ""
                mResponse = ""

                If txnRespWithObj.IsSuccess Then
                    mResponse = JsonConvert.SerializeObject(respPlGenIRN)
                    Dim DestinationPath As String = PubAttachmentPath + mSearchCode + "\"
                    If Not Directory.Exists(DestinationPath) Then
                        Directory.CreateDirectory(DestinationPath)
                    End If
                    respPlGenIRN.QrCodeImage.Save(DestinationPath + "\" + mSearchCode.Replace(" ", "") + "_EInvoiceQR.png")

                    mQry = " UPDATE SaleInvoice Set EInvoiceIRN = " & AgL.Chk_Text(respPlGenIRN.Irn) & ",
                                EInvoiceACKNo = " & AgL.Chk_Text(respPlGenIRN.AckNo) & "
                                EInvoiceACKDate = " & AgL.Chk_Date(respPlGenIRN.AckDt) & "
                                Where DocId = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    Dim txnRespWithObj1 As TxnRespWithObj(Of VerifyRespPl) = Await eInvoiceAPI.VerifySignedInvoice(eInvSession, respPlGenIRN)
                    Dim verifyRespPl As VerifyRespPl = New VerifyRespPl()

                    If txnRespWithObj.IsSuccess Then
                        verifyRespPl.IsVerified = txnRespWithObj1.RespObj.IsVerified
                        verifyRespPl.JwtIssuerIRP = txnRespWithObj1.RespObj.JwtIssuerIRP
                        verifyRespPl.VerifiedWithCertificateEffectiveFrom = txnRespWithObj1.RespObj.VerifiedWithCertificateEffectiveFrom
                        verifyRespPl.CertificateName = txnRespWithObj1.RespObj.CertificateName
                        verifyRespPl.CertStartDate = txnRespWithObj1.RespObj.CertStartDate
                        verifyRespPl.CertExpiryDate = txnRespWithObj1.RespObj.CertExpiryDate
                    End If

                    MsgBox("E-Invoice Generated Successfully...!", MsgBoxStyle.Information)
                    ReportFrm.DGL1.DataSource = Nothing
                Else

                    If txnRespWithObj.ErrorDetails IsNot Nothing Then

                        For Each errPl As RespErrDetailsPl In txnRespWithObj.ErrorDetails
                            ErrorCodes += errPl.ErrorCode & ","
                            ErrorDesc += errPl.ErrorCode & ": " + errPl.ErrorMessage & Environment.NewLine
                            mResponse = ErrorDesc
                            MsgBox(mResponse)
                        Next
                    End If

                    Dim respInfoDtlsPl As RespInfoDtlsPl = New RespInfoDtlsPl()

                    If txnRespWithObj.InfoDetails IsNot Nothing Then

                        For Each infoPl As RespInfoDtlsPl In txnRespWithObj.InfoDetails
                            Dim strDupIrnPl = JsonConvert.SerializeObject(infoPl.Desc)

                            Select Case infoPl.InfCd
                                Case "DUPIRN"
                                    Dim dupIrnPl As DupIrnPl = JsonConvert.DeserializeObject(Of DupIrnPl)(strDupIrnPl)
                                Case "EWBERR"
                                    Dim ewbErrPl As List(Of EwbErrPl) = JsonConvert.DeserializeObject(Of List(Of EwbErrPl))(strDupIrnPl)
                                Case "ADDNLNFO"
                                    Dim strDesc As String = CStr(infoPl.Desc)
                            End Select
                        Next
                    End If
                End If
            End If
        Next
    End Sub
    Public Async Sub CancelIRN(DGL As AgControls.AgDataGrid)
        Dim I As Integer = 0
        Dim mEInvoiceIRN As String = ""
        Dim mSearchCode As String = ""

        For I = 0 To DGL.Rows.Count - 1
            If AgL.XNull(DGL.Item("Search Code", I).Value) IsNot Nothing And AgL.XNull(DGL.Item("Search Code", I).Value) <> "" Then
                If AgL.XNull(DGL.Item("Irn", I).Value) IsNot Nothing And AgL.XNull(DGL.Item("Irn", I).Value) <> "" Then
                    mEInvoiceIRN = DGL.Item("Irn", I).Value
                    mSearchCode = DGL.Item("Search Code", I).Value

                    Dim reqPlCancelIRN As ReqPlCancelIRN = New ReqPlCancelIRN()
                    reqPlCancelIRN.CnlRem = "Data Entry Mystake"
                    reqPlCancelIRN.CnlRsn = "2"
                    reqPlCancelIRN.Irn = mEInvoiceIRN
                    Dim txnRespWithObj As TxnRespWithObj(Of RespPlCancelIRN) = Await eInvoiceAPI.CancelIRNIRNAsync(eInvSession, reqPlCancelIRN)

                    If txnRespWithObj.IsSuccess Then
                        mResponse = JsonConvert.SerializeObject(txnRespWithObj.RespObj)

                        mQry = " UPDATE SaleInvoice Set EInvoiceIRN = Null Where DocId = '" & mSearchCode & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        Call AgL.LogTableEntry(mSearchCode, "SaleInvoice", "E", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd, mResponse)

                        MsgBox("E-Invoice Cancelled...!", MsgBoxStyle.Information)
                        ReportFrm.DGL1.DataSource = Nothing
                    Else
                        mResponse = txnRespWithObj.TxnOutcome
                        MsgBox(mResponse, MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox("E-Invoice is not generated for Invoice no. " & DGL.Item("Invoice No", I).Value & ". Can't cancel it...!", MsgBoxStyle.Information)
                End If
            End If
        Next
    End Sub
    Public Async Sub GetEWBByIRN(DGL As AgControls.AgDataGrid)
        If True Then
            Dim IRN_No As String = "37d86afc1379fc963c1488a1bcf3c781f0011a920fc6ee7e1cd5ffc177bc7e18"
            Dim txnRespWithObj As TxnRespWithObj(Of RespGetEWBByIRN) = Await eInvoiceAPI.GetEWBByIRNAsync(eInvSession, IRN_No)

            If txnRespWithObj.IsSuccess Then
                mResponse = JsonConvert.SerializeObject(txnRespWithObj.RespObj)
            Else
                mResponse = txnRespWithObj.TxnOutcome
            End If
            MsgBox(mResponse)
        End If
    End Sub
    Private Sub LoadEInvoiceSessionValues()
        FGetSellerDivisionSiteData(AgL.PubDivCode, AgL.PubSiteCode)

        eInvSession.eInvApiSetting.client_id = FGetSettings(SettingFields.EInvoiceClientId, SettingType.EInvoice)
        eInvSession.eInvApiSetting.client_secret = FGetSettings(SettingFields.EInvoiceClientSecret, SettingType.EInvoice)
        eInvSession.eInvApiSetting.GSPName = FGetSettings(SettingFields.EInvoiceGSPName, SettingType.EInvoice)
        eInvSession.eInvApiSetting.AspUserId = FGetSettings(SettingFields.EInvoiceAspUserId, SettingType.EInvoice)
        eInvSession.eInvApiSetting.AspPassword = FGetSettings(SettingFields.EInvoiceAspPassword, SettingType.EInvoice)
        eInvSession.eInvApiSetting.AuthUrl = FGetSettings(SettingFields.EInvoiceAuthURL, SettingType.EInvoice)
        eInvSession.eInvApiSetting.BaseUrl = FGetSettings(SettingFields.EInvoiceBaseURL, SettingType.EInvoice)
        eInvSession.eInvApiSetting.EwbByIRN = FGetSettings(SettingFields.EInvoiceEWBURL, SettingType.EInvoice)
        eInvSession.eInvApiSetting.CancelEwbUrl = FGetSettings(SettingFields.EInvoiceCancelEWBURL, SettingType.EInvoice)

        eInvSession.eInvApiLoginDetails.UserName = DTDivisionSiteDetail.Rows(0)("DivisionSiteEInvoiceUserName")
        eInvSession.eInvApiLoginDetails.Password = DTDivisionSiteDetail.Rows(0)("DivisionSiteEInvoicePassword")
        eInvSession.eInvApiLoginDetails.GSTIN = DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo")


        'eInvSession.eInvApiSetting.GSPName = "TaxPro_Sandbox"
        'eInvSession.eInvApiSetting.AspUserId = "1655233121"
        'eInvSession.eInvApiSetting.AspPassword = "P@ssw0rd!"
        'eInvSession.eInvApiSetting.AuthUrl = "http://testapi.taxprogsp.co.in/eivital/v1.03"
        'eInvSession.eInvApiSetting.BaseUrl = "http://testapi.taxprogsp.co.in/eicore/v1.03"
        'eInvSession.eInvApiSetting.EwbByIRN = "http://testapi.taxprogsp.co.in/eiewb/v1.03"
        'eInvSession.eInvApiSetting.CancelEwbUrl = "http://testapi.taxprogsp.co.in/v1.03"

        'eInvSession.eInvApiLoginDetails.UserName = "TaxProEnvPON"
        'eInvSession.eInvApiLoginDetails.Password = "abc34*"
        'eInvSession.eInvApiLoginDetails.GSTIN = "34AACCC1596Q002"
    End Sub
    Private Sub FGetSellerDivisionSiteData(bDiv_Code As String, bSite_Code As String)
        Dim mDivisionSiteSalesTaxNo As String = ""
        Dim DivisionSiteEInvoiceUserName As String = ""
        Dim DivisionSiteEInvoicePassword As String = ""

        mQry = " Select VReg.SalesTaxNo As DivisionSiteSalesTaxNo, '' As DivisionSiteEInvoiceUserName, 
                '' As DivisionSiteEInvoicePassword, Sg.DispName As DivisionName, 
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                        Then IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'')
                        Else Sg.Address END As DivisionAddress,
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                        Then Sm.PinNo Else Sg.PIN END As DivisionPinCode, 
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                        Then Sc.CityName Else C.CityName END As DivisionCityName,
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                        Then SS.ManualCode Else S.ManualCode END As DivisionStateCode
                From Division D
                LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                LEFT JOIN City C On Sg.CityCode = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
                LEFT JOIN SiteMast Sm ON 1=1
                LEFT JOIN City SC On Sm.City_Code = SC.CityCode
                LEFT JOIN State SS On SC.State = SS.Code
                Where D.Div_Code = '" & bDiv_Code & "'
                And Sm.Code = '" & bSite_Code & "'"
        DTDivisionSiteDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo")) = "" Then
            mDivisionSiteSalesTaxNo = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteSalesTaxNo, SettingType.General, bDiv_Code, bSite_Code, "", "", "", "", "")
            If mDivisionSiteSalesTaxNo = "" Then
                MsgBox("Company GST No. is blank.", MsgBoxStyle.Information)
                Exit Sub
            Else
                DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo") = mDivisionSiteSalesTaxNo
            End If
        End If

        DivisionSiteEInvoiceUserName = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteEInvoiceUserName, SettingType.EInvoice, bDiv_Code, bSite_Code, "", "", "", "", "")
        DivisionSiteEInvoicePassword = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteEInvoicePassword, SettingType.EInvoice, bDiv_Code, bSite_Code, "", "", "", "", "")

        If DivisionSiteEInvoiceUserName = "" Then
            MsgBox("Company E-Invoice User Name Is Blank.", MsgBoxStyle.Information)
            Exit Sub
        Else
            DTDivisionSiteDetail.Rows(0)("DivisionSiteEInvoiceUserName") = DivisionSiteEInvoiceUserName
        End If

        If DivisionSiteEInvoicePassword = "" Then
            MsgBox("Company E-Invoice Password Is Blank.", MsgBoxStyle.Information)
            Exit Sub
        Else
            DTDivisionSiteDetail.Rows(0)("DivisionSiteEInvoicePassword") = DivisionSiteEInvoicePassword
        End If
    End Sub

End Class
