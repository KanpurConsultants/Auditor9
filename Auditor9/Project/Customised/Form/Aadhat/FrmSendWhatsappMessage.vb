Imports AgLibrary.ClsMain.agConstants
Imports System.Threading
Imports System.ComponentModel
Imports System.IO

Public Class FrmSendWhatsappMessage
    Dim mQry As String = ""
    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnSendMessageForTodaySaleInvoice.Click
        BtnSendMessageForTodaySaleInvoice.Enabled = False
        _backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        _backgroundWorker1.WorkerSupportsCancellation = False
        _backgroundWorker1.WorkerReportsProgress = False
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FProcMessageForTodaySaleInvoice)
        _backgroundWorker1.RunWorkerAsync()
    End Sub

    Public Sub FProcMessageForTodaySaleInvoice()
        Dim IsSuccess As Boolean
        Dim ToMobileNo As String
        Dim ToMessage As String
        Dim I As Integer
        Dim MessageFormat As String
        MessageFormat = "Subject: New Invoices Generated

Dear <PartyName>,

Your invoices have been successfully generated on <EntryDate>.

Invoice Details:

<InvoiceDetails>

Total Amount : ₹ <TotalAmount>

Thank you for your business.

Sincerely
<DivisionName>"

        mQry = "SELECT H.SaleToParty, Max(VP.Name) AS SaleToPartyName, Max(H.SaleToPartyMobile) AS  SaleToPartyMobile, count(H.DocId) NoBill,
                replace( convert(NVARCHAR, Max(H.V_Date),106),' ','/') AS V_Date, Sum(H.Net_Amount) AS TotalAmount, Max(Sg.DispName) As DivisionName,
                (
                SELECT  A.InvoiceDetail + ', ' + CHAR(10) 
                FROM 
                (
                SELECT  
                Max(H1.V_Type) + '-' + Max(H1.ManualRefNo) + ' for Rs. ' +  ltrim (Str(Max(H1.Net_Amount), 25, 2))  AS InvoiceDetail
                FROM SaleInvoice H1 WITH (Nolock)
                WHERE H1.V_Date = '" & AgL.PubLoginDate & "' AND H1.SaleToPartyMobile IS NOT NULL 
                AND H1.SaleToParty = H.SaleToParty
                GROUP BY H1.DocID  
                ) A
                FOR XML Path ('')
                ) AS InvoiceDetail 
                FROM SaleInvoice H WITH (Nolock)
                LEFT JOIN ViewHelpSubgroup VP WITH (Nolock) On VP.Code = H.SaleToParty
                LEFT JOIN Division D WITH (Nolock) On H.Div_Code = D.Div_Code
                LEFT JOIN SubGroup Sg WITH (Nolock) On D.SubCode = Sg.SubCode
                WHERE H.V_Date = '" & AgL.PubLoginDate & "' AND H.SaleToPartyMobile IS NOT NULL 
                GROUP BY H.SaleToParty "
        Dim DtDocData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtDocData.Rows.Count > 0 Then
            For I = 0 To DtDocData.Rows.Count - 1
                ToMobileNo = AgL.XNull(DtDocData.Rows(I)("SaleToPartyMobile"))
                ToMessage = MessageFormat.
                            Replace("<PartyName>", AgL.XNull(DtDocData.Rows(I)("SaleToPartyName"))).
                            Replace("<EntryDate>", AgL.XNull(DtDocData.Rows(I)("V_Date"))).
                            Replace("<InvoiceDetails>", AgL.XNull(DtDocData.Rows(I)("InvoiceDetail"))).
                            Replace("<TotalAmount>", AgL.XNull(DtDocData.Rows(I)("TotalAmount"))).
                            Replace("<DivisionName>", AgL.XNull(DtDocData.Rows(I)("DivisionName")))

                IsSuccess = ClsMain.FSendWhatsappMessage(ToMobileNo, ToMessage, "Message", "")
            Next
        End If

        MsgBox("Message Send For Today SaleInvoice Successfully...", MsgBoxStyle.Information)

    End Sub

    Private Sub BtnSyncImages_Click(sender As Object, e As EventArgs)
        'BtnSync.Enabled = False
        'BtnSyncImages.Enabled = False
        '_backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        '_backgroundWorker1.WorkerSupportsCancellation = False
        '_backgroundWorker1.WorkerReportsProgress = False
        'AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FSyncDocuments)
        '_backgroundWorker1.RunWorkerAsync()
    End Sub
End Class