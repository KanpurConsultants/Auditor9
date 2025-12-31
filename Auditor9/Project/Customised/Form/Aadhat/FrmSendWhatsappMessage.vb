Imports System.Data.SqlClient
Imports System.Threading
Imports System.ComponentModel
Imports System.IO

Public Class FrmSendWhatsappMessage
    Dim mQry As String = ""
    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker
    Dim WhatsAppSender As New WhatsAppSender()
    Dim WithEvents checkThread As Thread
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Start background thread
        checkThread = New Thread(AddressOf CheckDataLoop)
        checkThread.IsBackground = True
        checkThread.Start()
        WhatsAppSender.StartChecking()
    End Sub

    Private Sub CheckDataLoop()
        While True
            Try
                Dim count As Integer = GetTableCount()
                ' Update button text safely from another thread
                Me.Invoke(New MethodInvoker(Sub()
                                                BtnStartToSendWhatsapp.Text = "Pending Whatsapp To Send : " & count.ToString()
                                            End Sub))
            Catch ex As Exception
                ' Optional: handle error
            End Try

            Thread.Sleep(120000) ' Wait 2 minutes (120000 milliseconds)
        End While
    End Sub

    Private Function GetTableCount() As Integer
        Dim count As Integer = 0
        mQry = "SELECT COUNT(*) FROM SendWhatsapp H WHERE H.WhatsappSendDate IS NULL "
        count = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
        Return count
    End Function

    Private Sub BtnSendMessageForTodaySaleInvoice_Click(sender As Object, e As EventArgs) Handles BtnSendMessageForTodaySaleInvoice.Click
        BtnSendMessageForTodaySaleInvoice.Enabled = False
        _backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        _backgroundWorker1.WorkerSupportsCancellation = False
        _backgroundWorker1.WorkerReportsProgress = False
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FProcMessageForTodaySaleInvoice)
        _backgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BtnSendMessageForTodayLRUpdate_Click(sender As Object, e As EventArgs) Handles BtnSendMessageForTodayLRUpdate.Click
        BtnSendMessageForTodayLRUpdate.Enabled = False
        _backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        _backgroundWorker1.WorkerSupportsCancellation = False
        _backgroundWorker1.WorkerReportsProgress = False
        AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FProcMessageForTodayLRUpdate)
        _backgroundWorker1.RunWorkerAsync()
    End Sub

    Public Sub FProcMessageForTodaySaleInvoice()
        Dim ToMobileNo As String
        Dim ToMessage As String
        Dim I As Integer
        Dim MessageFormat As String
        MessageFormat = "Subject: New Invoices Generated" & vbCrLf &
                    "Dear <PartyName>," & vbCrLf &
                    "Your invoices have been successfully generated on <EntryDate>." & vbCrLf &
                    "Invoice Details:" & vbCrLf &
                    "<InvoiceDetails>" & vbCrLf &
                    "Total Amount : ₹ <TotalAmount>" & vbCrLf &
                    "Thank you for your business." & vbCrLf &
                    "Sincerely" & vbCrLf &
                    "<DivisionName>"

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

                'IsSuccess = ClsMain.FSendWhatsappMessage(ToMobileNo, ToMessage, "Message", "")
                Dim FSendWhatsapp As String = ""
                Dim sender As New WhatsAppSender()
                'FSendWhatsapp = sender.SendMessageByWhatsapp(ToMobileNo, ToMessage)
                ToMessage = ToMessage.Replace(vbCrLf, "\n").Replace(vbCr, "\n").Replace(vbLf, "\n")
                sender.EntrySendWhatsapp(ToMobileNo, ToMessage, "Message For Today SaleInvoice", AgL.GCn)
            Next
        End If

        MsgBox("Message Send For Today SaleInvoice Successfully...", MsgBoxStyle.Information)

    End Sub

    Public Sub FProcMessageForTodayLRUpdate()
        Dim ToMobileNo As String
        Dim ToMessage As String
        Dim I As Integer
        Dim MessageFormat As String
        MessageFormat = "Dear <PartyName>," & vbCrLf &
                    "Your Inv.No. <EntryNo> Dated <EntryDate> of Rs.<NetAmount> has been dispatched By Transport <TransporterName> with LR No. <LRNo> on Date <LRDate> ." & vbCrLf &
                    "Sincerely" & vbCrLf &
                    "<DivisionName>"

        mQry = "Select 
                    Max(Sg.DispName) As DivisionName, 
                    replace(Convert(NVARCHAR,H.V_Date,106),' ','/') AS SaleDate,
                    Max(Party.DispName) As PartyName, Max(Party.Mobile) As PartyMobile,
                    Max(T.Name) As TransporterName, SIt.LrNo, replace(Convert(NVARCHAR,SIt.LrDate,106),' ','/') As LrDate, Sum(H.Net_Amount) Net_Amount,
                    (
                    SELECT H1.ManualRefNo + ', '
                    From SaleInvoice H1 
                                     LEFT JOIN SaleInvoiceTransport SIt1 ON H1.DocID = SIt1.DocID
                                     Where Sit1.LRUpdatedDate >'" & AgL.PubLoginDate & "' AND H1.V_Date = H.V_Date AND H1.SaleToParty = H.SaleToParty  AND SIT1.Transporter = SIT.Transporter AND SIT1.LrNo = SIT.LrNo
                    FOR XML Path ('')
                    ) AS SaleNo
                    From SaleInvoice H 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN Voucher_Type VT ON VT.V_Type = H.V_Type 
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SaleInvoiceTransport SIt ON H.DocID = SIt.DocID
                    LEFT JOIN SubGroup T On SIT.Transporter = T.SubCode
                    Where Sit.LRUpdatedDate >'" & AgL.PubLoginDate & "'
                    GROUP BY H.SaleToParty,H.V_Date,SIT.Transporter,SIt.LrDate,SIt.LrNo "
        Dim DtDocData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtDocData.Rows.Count > 0 Then
            For I = 0 To DtDocData.Rows.Count - 1
                ToMobileNo = AgL.XNull(DtDocData.Rows(I)("PartyMobile"))
                ToMessage = MessageFormat.
                            Replace("<PartyName>", AgL.XNull(DtDocData.Rows(I)("PartyName"))).
                            Replace("<EntryNo>", AgL.XNull(DtDocData.Rows(I)("SaleNo"))).
                            Replace("<EntryDate>", AgL.XNull(DtDocData.Rows(I)("SaleDate"))).
                            Replace("<LRNo>", AgL.XNull(DtDocData.Rows(I)("LRNo"))).
                            Replace("<LRDate>", AgL.XNull(DtDocData.Rows(I)("LRDate"))).
                            Replace("<DivisionName>", AgL.XNull(DtDocData.Rows(I)("DivisionName"))).
                            Replace("<TransporterName>", AgL.XNull(DtDocData.Rows(I)("TransporterName"))).
                            Replace("<NetAmount>", Format(AgL.VNull(DtDocData.Rows(I)("Net_Amount")), "0.00")).
                            Replace("&", "And")

                'IsSuccess = ClsMain.FSendWhatsappMessage(ToMobileNo, ToMessage, "Message", "")
                Dim FSendWhatsapp As String = ""
                Dim sender As New WhatsAppSender()
                'FSendWhatsapp = sender.SendMessageByWhatsapp(ToMobileNo, ToMessage)
                ToMessage = ToMessage.Replace(vbCrLf, "\n").Replace(vbCr, "\n").Replace(vbLf, "\n")
                sender.EntrySendWhatsapp(ToMobileNo, ToMessage, "Message For Today LR Update", AgL.GCn)
            Next
        End If

        MsgBox("Message Send For Today LR Update Successfully...", MsgBoxStyle.Information)

    End Sub

End Class