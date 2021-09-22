Public Class TempShipmentEntry
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1Document As String = "Document"
    Protected Const Col1DocumentNo As String = "Document No"

    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2BOE As String = "BOE"
    Protected Const Col2FCValue As String = "Value (FC)"
    Protected Const Col2INRValue As String = "Value (INR)"
    Protected Const Col2Term As String = "Term"
    Protected Const Col2DueDate As String = "Due Date"

    Public WithEvents Dgl3 As New AgControls.AgDataGrid
    Protected Const Col3Item As String = "Item"
    Protected Const Col3ItemDescription As String = "Item Description"
    Protected Const Col3Unit As String = "Unit"
    Protected Const Col3Qty As String = "Qty"
    Protected Const Col3Rate As String = "Rate"
    Protected Const Col3Amount As String = "Amount"
    Protected Const Col3ContainerNo As String = "Container No"
    Protected Const Col3KindsOfPackages As String = "Kinds Of Packages"

    Public Class HelpDataSet
        Public Shared PurchaseOrder As DataSet = Nothing
        Public Shared Port As DataSet = Nothing
        Public Shared CHA As DataSet = Nothing
        Public Shared Transporter As DataSet = Nothing
        Public Shared Document As DataSet = Nothing
        Public Shared DocumentNo As DataSet = Nothing
        Public Shared Item As DataSet = Nothing
    End Class
    

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtPurchaseOrder = New AgControls.AgTextBox
        Me.LblPurchaseOrder = New System.Windows.Forms.Label
        Me.LblEntryNoReq = New System.Windows.Forms.Label
        Me.TxtPurchaseOrderReferenceNo = New AgControls.AgTextBox
        Me.LblPoReferenceNo = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtDocRealisationDate = New AgControls.AgTextBox
        Me.LblDocRealisationDate = New System.Windows.Forms.Label
        Me.TxtDutyAmountPaidDate = New AgControls.AgTextBox
        Me.LblExportExpiryDate = New System.Windows.Forms.Label
        Me.TxtInvoiceNo = New AgControls.AgTextBox
        Me.LblInvoiceNo = New System.Windows.Forms.Label
        Me.TxtInvoiceDate = New AgControls.AgTextBox
        Me.LblInvoiceDate = New System.Windows.Forms.Label
        Me.TxtBillOfEntryDate = New AgControls.AgTextBox
        Me.LblBillOfEntrydate = New System.Windows.Forms.Label
        Me.TxtBillOfEntryNo = New AgControls.AgTextBox
        Me.LblBillOfEntryNo = New System.Windows.Forms.Label
        Me.TxtShippingLine = New AgControls.AgTextBox
        Me.LblShippingLine = New System.Windows.Forms.Label
        Me.TxtCountryOfOrigin = New AgControls.AgTextBox
        Me.LblCountryOfOrigin = New System.Windows.Forms.Label
        Me.TxtInsuranceDetail = New AgControls.AgTextBox
        Me.lblInsuranceDetail = New System.Windows.Forms.Label
        Me.TxtPortOfDispatch = New AgControls.AgTextBox
        Me.LblPortOfDispatch = New System.Windows.Forms.Label
        Me.TxtFinalPlaceOfDelivery = New AgControls.AgTextBox
        Me.LblFinalPlaceOfDelivery = New System.Windows.Forms.Label
        Me.TxtPortOfDicharge = New AgControls.AgTextBox
        Me.LblPortOfDicharge = New System.Windows.Forms.Label
        Me.TxtETAAtIndianSeaPort = New AgControls.AgTextBox
        Me.LblETAAtIndianSeaPort = New System.Windows.Forms.Label
        Me.TxtETAAtICD = New AgControls.AgTextBox
        Me.LblETAAtICD = New System.Windows.Forms.Label
        Me.TxtCHA = New AgControls.AgTextBox
        Me.LblCHA = New System.Windows.Forms.Label
        Me.TxtShipperInformationDate = New AgControls.AgTextBox
        Me.LblShipperInformationDate = New System.Windows.Forms.Label
        Me.TxtShipperInformationRemark = New AgControls.AgTextBox
        Me.LblShipperInformationRemark = New System.Windows.Forms.Label
        Me.TxtDocSubmissionDateToCHA = New AgControls.AgTextBox
        Me.LblDocSubmissionDateToCHA = New System.Windows.Forms.Label
        Me.AgTextBox13 = New AgControls.AgTextBox
        Me.LblClearingBillNo = New System.Windows.Forms.Label
        Me.TxtShipmentReleaseDate = New AgControls.AgTextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.TxtTransporter = New AgControls.AgTextBox
        Me.LblTrasnsporterName = New System.Windows.Forms.Label
        Me.TxtVehicleNo = New AgControls.AgTextBox
        Me.LblVehicleNo = New System.Windows.Forms.Label
        Me.TxtDriverName = New AgControls.AgTextBox
        Me.LblDriverName = New System.Windows.Forms.Label
        Me.TxtArrivalDateAtFactory = New AgControls.AgTextBox
        Me.LblArrivalDateAtFactory = New System.Windows.Forms.Label
        Me.TxtVehicleReturnDate = New AgControls.AgTextBox
        Me.LblVehicleReturnDate = New System.Windows.Forms.Label
        Me.TxtProofSubmissionDate = New AgControls.AgTextBox
        Me.LblProofSubmissionDate = New System.Windows.Forms.Label
        Me.TxtBankAdviceNo = New AgControls.AgTextBox
        Me.LblBankAdviceNo = New System.Windows.Forms.Label
        Me.TxtBankAdviceDate = New AgControls.AgTextBox
        Me.LblBankAdviceDate = New System.Windows.Forms.Label
        Me.TxtExchangeRate = New AgControls.AgTextBox
        Me.LblExchangeRate = New System.Windows.Forms.Label
        Me.TxtBillOfLadingNo = New AgControls.AgTextBox
        Me.LblBillOfLadingNo = New System.Windows.Forms.Label
        Me.TxtBillOfLadingDate = New AgControls.AgTextBox
        Me.LblBillOfLadingDate = New System.Windows.Forms.Label
        Me.TxtShipmentOnBoardDate = New AgControls.AgTextBox
        Me.LblShipmentOnBoardDate = New System.Windows.Forms.Label
        Me.TxtVesselName = New AgControls.AgTextBox
        Me.LblVesselName = New System.Windows.Forms.Label
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.TpShipmentDetail1 = New System.Windows.Forms.TabPage
        Me.TpShipmentItemDetail1 = New System.Windows.Forms.TabPage
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl2.SuspendLayout()
        Me.TpShipmentDetail1.SuspendLayout()
        Me.TpShipmentItemDetail1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(756, 585)
        Me.GroupBox2.Size = New System.Drawing.Size(148, 40)
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Location = New System.Drawing.Point(29, 19)
        Me.TxtStatus.Tag = ""
        '
        'CmdStatus
        '
        Me.CmdStatus.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(596, 585)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(142, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'CmdMoveToLog
        '
        Me.CmdMoveToLog.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(421, 585)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Size = New System.Drawing.Size(26, 19)
        '
        'CmdApprove
        '
        Me.CmdApprove.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(145, 585)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(11, 585)
        Me.GrpUP.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 581)
        Me.GroupBox1.Size = New System.Drawing.Size(1030, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(287, 585)
        Me.GBoxDivision.Size = New System.Drawing.Size(114, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Tag = ""
        '
        'TxtDocId
        '
        Me.TxtDocId.AgSelectedValue = ""
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Location = New System.Drawing.Point(849, 342)
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblV_No
        '
        Me.LblV_No.Location = New System.Drawing.Point(292, 14)
        Me.LblV_No.Tag = ""
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(402, 14)
        Me.TxtV_No.Size = New System.Drawing.Size(95, 18)
        Me.TxtV_No.TabIndex = 1
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(157, 20)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(11, 15)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(755, 348)
        Me.LblV_TypeReq.Tag = ""
        Me.LblV_TypeReq.Visible = False
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(173, 14)
        Me.TxtV_Date.Size = New System.Drawing.Size(111, 18)
        Me.TxtV_Date.TabIndex = 0
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(675, 344)
        Me.LblV_Type.Size = New System.Drawing.Size(88, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Voucher Type"
        Me.LblV_Type.Visible = False
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(341, 4)
        Me.TxtV_Type.Size = New System.Drawing.Size(25, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        Me.TxtV_Type.Visible = False
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(630, 342)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(605, 356)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        Me.LblSite_Code.Visible = False
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(646, 340)
        Me.TxtSite_Code.Size = New System.Drawing.Size(23, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        Me.TxtSite_Code.Visible = False
        '
        'LblDocId
        '
        Me.LblDocId.Location = New System.Drawing.Point(802, 344)
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(904, 343)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 19)
        Me.TabControl1.Size = New System.Drawing.Size(1018, 333)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtVesselName)
        Me.TP1.Controls.Add(Me.LblVesselName)
        Me.TP1.Controls.Add(Me.TxtShipmentOnBoardDate)
        Me.TP1.Controls.Add(Me.LblShipmentOnBoardDate)
        Me.TP1.Controls.Add(Me.TxtBillOfLadingDate)
        Me.TP1.Controls.Add(Me.LblBillOfLadingDate)
        Me.TP1.Controls.Add(Me.TxtBillOfLadingNo)
        Me.TP1.Controls.Add(Me.LblBillOfLadingNo)
        Me.TP1.Controls.Add(Me.TxtExchangeRate)
        Me.TP1.Controls.Add(Me.LblExchangeRate)
        Me.TP1.Controls.Add(Me.TxtBankAdviceDate)
        Me.TP1.Controls.Add(Me.TxtPortOfDicharge)
        Me.TP1.Controls.Add(Me.LblPortOfDicharge)
        Me.TP1.Controls.Add(Me.LblBankAdviceDate)
        Me.TP1.Controls.Add(Me.TxtBankAdviceNo)
        Me.TP1.Controls.Add(Me.LblBankAdviceNo)
        Me.TP1.Controls.Add(Me.TxtProofSubmissionDate)
        Me.TP1.Controls.Add(Me.LblProofSubmissionDate)
        Me.TP1.Controls.Add(Me.TxtVehicleReturnDate)
        Me.TP1.Controls.Add(Me.LblVehicleReturnDate)
        Me.TP1.Controls.Add(Me.TxtArrivalDateAtFactory)
        Me.TP1.Controls.Add(Me.LblArrivalDateAtFactory)
        Me.TP1.Controls.Add(Me.TxtDriverName)
        Me.TP1.Controls.Add(Me.LblDriverName)
        Me.TP1.Controls.Add(Me.TxtVehicleNo)
        Me.TP1.Controls.Add(Me.LblVehicleNo)
        Me.TP1.Controls.Add(Me.TxtTransporter)
        Me.TP1.Controls.Add(Me.LblTrasnsporterName)
        Me.TP1.Controls.Add(Me.TxtShipmentReleaseDate)
        Me.TP1.Controls.Add(Me.Label15)
        Me.TP1.Controls.Add(Me.AgTextBox13)
        Me.TP1.Controls.Add(Me.LblClearingBillNo)
        Me.TP1.Controls.Add(Me.TxtDocSubmissionDateToCHA)
        Me.TP1.Controls.Add(Me.LblDocSubmissionDateToCHA)
        Me.TP1.Controls.Add(Me.TxtShipperInformationRemark)
        Me.TP1.Controls.Add(Me.LblShipperInformationRemark)
        Me.TP1.Controls.Add(Me.TxtShipperInformationDate)
        Me.TP1.Controls.Add(Me.LblShipperInformationDate)
        Me.TP1.Controls.Add(Me.TxtCHA)
        Me.TP1.Controls.Add(Me.LblCHA)
        Me.TP1.Controls.Add(Me.TxtETAAtICD)
        Me.TP1.Controls.Add(Me.LblETAAtICD)
        Me.TP1.Controls.Add(Me.TxtETAAtIndianSeaPort)
        Me.TP1.Controls.Add(Me.LblETAAtIndianSeaPort)
        Me.TP1.Controls.Add(Me.TxtFinalPlaceOfDelivery)
        Me.TP1.Controls.Add(Me.LblFinalPlaceOfDelivery)
        Me.TP1.Controls.Add(Me.TxtPortOfDispatch)
        Me.TP1.Controls.Add(Me.LblPortOfDispatch)
        Me.TP1.Controls.Add(Me.TxtInsuranceDetail)
        Me.TP1.Controls.Add(Me.lblInsuranceDetail)
        Me.TP1.Controls.Add(Me.TxtCountryOfOrigin)
        Me.TP1.Controls.Add(Me.LblCountryOfOrigin)
        Me.TP1.Controls.Add(Me.TxtShippingLine)
        Me.TP1.Controls.Add(Me.LblShippingLine)
        Me.TP1.Controls.Add(Me.TxtBillOfEntryDate)
        Me.TP1.Controls.Add(Me.LblBillOfEntrydate)
        Me.TP1.Controls.Add(Me.TxtBillOfEntryNo)
        Me.TP1.Controls.Add(Me.LblBillOfEntryNo)
        Me.TP1.Controls.Add(Me.TxtInvoiceDate)
        Me.TP1.Controls.Add(Me.LblInvoiceDate)
        Me.TP1.Controls.Add(Me.TxtInvoiceNo)
        Me.TP1.Controls.Add(Me.LblInvoiceNo)
        Me.TP1.Controls.Add(Me.TxtDutyAmountPaidDate)
        Me.TP1.Controls.Add(Me.LblExportExpiryDate)
        Me.TP1.Controls.Add(Me.TxtDocRealisationDate)
        Me.TP1.Controls.Add(Me.LblDocRealisationDate)
        Me.TP1.Controls.Add(Me.LblEntryNoReq)
        Me.TP1.Controls.Add(Me.TxtPurchaseOrderReferenceNo)
        Me.TP1.Controls.Add(Me.LblPoReferenceNo)
        Me.TP1.Controls.Add(Me.TxtPurchaseOrder)
        Me.TP1.Controls.Add(Me.LblPurchaseOrder)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(1010, 307)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPurchaseOrder, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPurchaseOrder, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPoReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPurchaseOrderReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblEntryNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocRealisationDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocRealisationDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblExportExpiryDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDutyAmountPaidDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblInvoiceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtInvoiceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblInvoiceDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtInvoiceDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBillOfEntryNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillOfEntryNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBillOfEntrydate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillOfEntryDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblShippingLine, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtShippingLine, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCountryOfOrigin, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCountryOfOrigin, 0)
        Me.TP1.Controls.SetChildIndex(Me.lblInsuranceDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtInsuranceDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPortOfDispatch, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPortOfDispatch, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFinalPlaceOfDelivery, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFinalPlaceOfDelivery, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblETAAtIndianSeaPort, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtETAAtIndianSeaPort, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblETAAtICD, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtETAAtICD, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCHA, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCHA, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblShipperInformationDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtShipperInformationDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblShipperInformationRemark, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtShipperInformationRemark, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocSubmissionDateToCHA, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocSubmissionDateToCHA, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblClearingBillNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.AgTextBox13, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label15, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtShipmentReleaseDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblTrasnsporterName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtTransporter, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVehicleNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVehicleNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDriverName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDriverName, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblArrivalDateAtFactory, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtArrivalDateAtFactory, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVehicleReturnDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVehicleReturnDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblProofSubmissionDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProofSubmissionDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBankAdviceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBankAdviceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBankAdviceDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPortOfDicharge, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPortOfDicharge, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBankAdviceDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblExchangeRate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtExchangeRate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBillOfLadingNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillOfLadingNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBillOfLadingDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillOfLadingDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblShipmentOnBoardDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtShipmentOnBoardDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVesselName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVesselName, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(1012, 41)
        Me.Topctrl1.TabIndex = 2
        '
        'Dgl1
        '
        Me.Dgl1.AgMandatoryColumn = 0
        Me.Dgl1.AgReadOnlyColumnColor = System.Drawing.Color.Ivory
        Me.Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.CancelEditingControlValidating = False
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'TxtPurchaseOrder
        '
        Me.TxtPurchaseOrder.AgMandatory = False
        Me.TxtPurchaseOrder.AgMasterHelp = False
        Me.TxtPurchaseOrder.AgNumberLeftPlaces = 8
        Me.TxtPurchaseOrder.AgNumberNegetiveAllow = False
        Me.TxtPurchaseOrder.AgNumberRightPlaces = 2
        Me.TxtPurchaseOrder.AgPickFromLastValue = False
        Me.TxtPurchaseOrder.AgRowFilter = ""
        Me.TxtPurchaseOrder.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPurchaseOrder.AgSelectedValue = Nothing
        Me.TxtPurchaseOrder.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPurchaseOrder.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPurchaseOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPurchaseOrder.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPurchaseOrder.Location = New System.Drawing.Point(173, 34)
        Me.TxtPurchaseOrder.MaxLength = 50
        Me.TxtPurchaseOrder.Name = "TxtPurchaseOrder"
        Me.TxtPurchaseOrder.Size = New System.Drawing.Size(111, 18)
        Me.TxtPurchaseOrder.TabIndex = 2
        '
        'LblPurchaseOrder
        '
        Me.LblPurchaseOrder.AutoSize = True
        Me.LblPurchaseOrder.BackColor = System.Drawing.Color.Transparent
        Me.LblPurchaseOrder.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPurchaseOrder.Location = New System.Drawing.Point(11, 34)
        Me.LblPurchaseOrder.Name = "LblPurchaseOrder"
        Me.LblPurchaseOrder.Size = New System.Drawing.Size(99, 16)
        Me.LblPurchaseOrder.TabIndex = 706
        Me.LblPurchaseOrder.Text = "Against PO No."
        '
        'LblEntryNoReq
        '
        Me.LblEntryNoReq.AutoSize = True
        Me.LblEntryNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblEntryNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblEntryNoReq.Location = New System.Drawing.Point(385, 20)
        Me.LblEntryNoReq.Name = "LblEntryNoReq"
        Me.LblEntryNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblEntryNoReq.TabIndex = 738
        Me.LblEntryNoReq.Text = "Ä"
        '
        'TxtPurchaseOrderReferenceNo
        '
        Me.TxtPurchaseOrderReferenceNo.AgMandatory = False
        Me.TxtPurchaseOrderReferenceNo.AgMasterHelp = False
        Me.TxtPurchaseOrderReferenceNo.AgNumberLeftPlaces = 8
        Me.TxtPurchaseOrderReferenceNo.AgNumberNegetiveAllow = False
        Me.TxtPurchaseOrderReferenceNo.AgNumberRightPlaces = 2
        Me.TxtPurchaseOrderReferenceNo.AgPickFromLastValue = False
        Me.TxtPurchaseOrderReferenceNo.AgRowFilter = ""
        Me.TxtPurchaseOrderReferenceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPurchaseOrderReferenceNo.AgSelectedValue = Nothing
        Me.TxtPurchaseOrderReferenceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPurchaseOrderReferenceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPurchaseOrderReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPurchaseOrderReferenceNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPurchaseOrderReferenceNo.Location = New System.Drawing.Point(402, 34)
        Me.TxtPurchaseOrderReferenceNo.MaxLength = 50
        Me.TxtPurchaseOrderReferenceNo.Name = "TxtPurchaseOrderReferenceNo"
        Me.TxtPurchaseOrderReferenceNo.Size = New System.Drawing.Size(95, 18)
        Me.TxtPurchaseOrderReferenceNo.TabIndex = 3
        '
        'LblPoReferenceNo
        '
        Me.LblPoReferenceNo.AutoSize = True
        Me.LblPoReferenceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblPoReferenceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPoReferenceNo.Location = New System.Drawing.Point(292, 34)
        Me.LblPoReferenceNo.Name = "LblPoReferenceNo"
        Me.LblPoReferenceNo.Size = New System.Drawing.Size(70, 16)
        Me.LblPoReferenceNo.TabIndex = 737
        Me.LblPoReferenceNo.Text = "PO Ref No"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(4, 3)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(398, 200)
        Me.Pnl1.TabIndex = 0
        '
        'TxtDocRealisationDate
        '
        Me.TxtDocRealisationDate.AgMandatory = False
        Me.TxtDocRealisationDate.AgMasterHelp = False
        Me.TxtDocRealisationDate.AgNumberLeftPlaces = 8
        Me.TxtDocRealisationDate.AgNumberNegetiveAllow = False
        Me.TxtDocRealisationDate.AgNumberRightPlaces = 2
        Me.TxtDocRealisationDate.AgPickFromLastValue = False
        Me.TxtDocRealisationDate.AgRowFilter = ""
        Me.TxtDocRealisationDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDocRealisationDate.AgSelectedValue = Nothing
        Me.TxtDocRealisationDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDocRealisationDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDocRealisationDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDocRealisationDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDocRealisationDate.Location = New System.Drawing.Point(686, 53)
        Me.TxtDocRealisationDate.MaxLength = 20
        Me.TxtDocRealisationDate.Name = "TxtDocRealisationDate"
        Me.TxtDocRealisationDate.Size = New System.Drawing.Size(88, 18)
        Me.TxtDocRealisationDate.TabIndex = 22
        '
        'LblDocRealisationDate
        '
        Me.LblDocRealisationDate.AutoSize = True
        Me.LblDocRealisationDate.BackColor = System.Drawing.Color.Transparent
        Me.LblDocRealisationDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocRealisationDate.Location = New System.Drawing.Point(503, 53)
        Me.LblDocRealisationDate.Name = "LblDocRealisationDate"
        Me.LblDocRealisationDate.Size = New System.Drawing.Size(141, 16)
        Me.LblDocRealisationDate.TabIndex = 762
        Me.LblDocRealisationDate.Text = "Docs. Realisation Date"
        '
        'TxtDutyAmountPaidDate
        '
        Me.TxtDutyAmountPaidDate.AgMandatory = False
        Me.TxtDutyAmountPaidDate.AgMasterHelp = False
        Me.TxtDutyAmountPaidDate.AgNumberLeftPlaces = 8
        Me.TxtDutyAmountPaidDate.AgNumberNegetiveAllow = False
        Me.TxtDutyAmountPaidDate.AgNumberRightPlaces = 2
        Me.TxtDutyAmountPaidDate.AgPickFromLastValue = False
        Me.TxtDutyAmountPaidDate.AgRowFilter = ""
        Me.TxtDutyAmountPaidDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDutyAmountPaidDate.AgSelectedValue = Nothing
        Me.TxtDutyAmountPaidDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDutyAmountPaidDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDutyAmountPaidDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDutyAmountPaidDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDutyAmountPaidDate.Location = New System.Drawing.Point(904, 53)
        Me.TxtDutyAmountPaidDate.MaxLength = 20
        Me.TxtDutyAmountPaidDate.Name = "TxtDutyAmountPaidDate"
        Me.TxtDutyAmountPaidDate.Size = New System.Drawing.Size(88, 18)
        Me.TxtDutyAmountPaidDate.TabIndex = 23
        '
        'LblExportExpiryDate
        '
        Me.LblExportExpiryDate.AutoSize = True
        Me.LblExportExpiryDate.BackColor = System.Drawing.Color.Transparent
        Me.LblExportExpiryDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblExportExpiryDate.Location = New System.Drawing.Point(778, 53)
        Me.LblExportExpiryDate.Name = "LblExportExpiryDate"
        Me.LblExportExpiryDate.Size = New System.Drawing.Size(124, 16)
        Me.LblExportExpiryDate.TabIndex = 764
        Me.LblExportExpiryDate.Text = "Duty Amt Paid Date"
        '
        'TxtInvoiceNo
        '
        Me.TxtInvoiceNo.AgMandatory = False
        Me.TxtInvoiceNo.AgMasterHelp = False
        Me.TxtInvoiceNo.AgNumberLeftPlaces = 8
        Me.TxtInvoiceNo.AgNumberNegetiveAllow = False
        Me.TxtInvoiceNo.AgNumberRightPlaces = 2
        Me.TxtInvoiceNo.AgPickFromLastValue = False
        Me.TxtInvoiceNo.AgRowFilter = ""
        Me.TxtInvoiceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtInvoiceNo.AgSelectedValue = Nothing
        Me.TxtInvoiceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtInvoiceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtInvoiceNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInvoiceNo.Location = New System.Drawing.Point(173, 53)
        Me.TxtInvoiceNo.MaxLength = 50
        Me.TxtInvoiceNo.Name = "TxtInvoiceNo"
        Me.TxtInvoiceNo.Size = New System.Drawing.Size(111, 18)
        Me.TxtInvoiceNo.TabIndex = 4
        '
        'LblInvoiceNo
        '
        Me.LblInvoiceNo.AutoSize = True
        Me.LblInvoiceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblInvoiceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInvoiceNo.Location = New System.Drawing.Point(11, 53)
        Me.LblInvoiceNo.Name = "LblInvoiceNo"
        Me.LblInvoiceNo.Size = New System.Drawing.Size(71, 16)
        Me.LblInvoiceNo.TabIndex = 766
        Me.LblInvoiceNo.Text = "Invoice No."
        '
        'TxtInvoiceDate
        '
        Me.TxtInvoiceDate.AgMandatory = False
        Me.TxtInvoiceDate.AgMasterHelp = False
        Me.TxtInvoiceDate.AgNumberLeftPlaces = 8
        Me.TxtInvoiceDate.AgNumberNegetiveAllow = False
        Me.TxtInvoiceDate.AgNumberRightPlaces = 2
        Me.TxtInvoiceDate.AgPickFromLastValue = False
        Me.TxtInvoiceDate.AgRowFilter = ""
        Me.TxtInvoiceDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtInvoiceDate.AgSelectedValue = Nothing
        Me.TxtInvoiceDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtInvoiceDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtInvoiceDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInvoiceDate.Location = New System.Drawing.Point(402, 53)
        Me.TxtInvoiceDate.MaxLength = 50
        Me.TxtInvoiceDate.Name = "TxtInvoiceDate"
        Me.TxtInvoiceDate.Size = New System.Drawing.Size(95, 18)
        Me.TxtInvoiceDate.TabIndex = 5
        '
        'LblInvoiceDate
        '
        Me.LblInvoiceDate.AutoSize = True
        Me.LblInvoiceDate.BackColor = System.Drawing.Color.Transparent
        Me.LblInvoiceDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInvoiceDate.Location = New System.Drawing.Point(292, 53)
        Me.LblInvoiceDate.Name = "LblInvoiceDate"
        Me.LblInvoiceDate.Size = New System.Drawing.Size(78, 16)
        Me.LblInvoiceDate.TabIndex = 768
        Me.LblInvoiceDate.Text = "Invoice Date"
        '
        'TxtBillOfEntryDate
        '
        Me.TxtBillOfEntryDate.AgMandatory = False
        Me.TxtBillOfEntryDate.AgMasterHelp = False
        Me.TxtBillOfEntryDate.AgNumberLeftPlaces = 8
        Me.TxtBillOfEntryDate.AgNumberNegetiveAllow = False
        Me.TxtBillOfEntryDate.AgNumberRightPlaces = 2
        Me.TxtBillOfEntryDate.AgPickFromLastValue = False
        Me.TxtBillOfEntryDate.AgRowFilter = ""
        Me.TxtBillOfEntryDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillOfEntryDate.AgSelectedValue = Nothing
        Me.TxtBillOfEntryDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillOfEntryDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtBillOfEntryDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillOfEntryDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillOfEntryDate.Location = New System.Drawing.Point(402, 73)
        Me.TxtBillOfEntryDate.MaxLength = 50
        Me.TxtBillOfEntryDate.Name = "TxtBillOfEntryDate"
        Me.TxtBillOfEntryDate.Size = New System.Drawing.Size(95, 18)
        Me.TxtBillOfEntryDate.TabIndex = 7
        '
        'LblBillOfEntrydate
        '
        Me.LblBillOfEntrydate.AutoSize = True
        Me.LblBillOfEntrydate.BackColor = System.Drawing.Color.Transparent
        Me.LblBillOfEntrydate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBillOfEntrydate.Location = New System.Drawing.Point(292, 73)
        Me.LblBillOfEntrydate.Name = "LblBillOfEntrydate"
        Me.LblBillOfEntrydate.Size = New System.Drawing.Size(109, 16)
        Me.LblBillOfEntrydate.TabIndex = 772
        Me.LblBillOfEntrydate.Text = "Bill Of Entry Date"
        '
        'TxtBillOfEntryNo
        '
        Me.TxtBillOfEntryNo.AgMandatory = False
        Me.TxtBillOfEntryNo.AgMasterHelp = False
        Me.TxtBillOfEntryNo.AgNumberLeftPlaces = 8
        Me.TxtBillOfEntryNo.AgNumberNegetiveAllow = False
        Me.TxtBillOfEntryNo.AgNumberRightPlaces = 2
        Me.TxtBillOfEntryNo.AgPickFromLastValue = False
        Me.TxtBillOfEntryNo.AgRowFilter = ""
        Me.TxtBillOfEntryNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillOfEntryNo.AgSelectedValue = Nothing
        Me.TxtBillOfEntryNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillOfEntryNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillOfEntryNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillOfEntryNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillOfEntryNo.Location = New System.Drawing.Point(173, 73)
        Me.TxtBillOfEntryNo.MaxLength = 50
        Me.TxtBillOfEntryNo.Name = "TxtBillOfEntryNo"
        Me.TxtBillOfEntryNo.Size = New System.Drawing.Size(111, 18)
        Me.TxtBillOfEntryNo.TabIndex = 6
        '
        'LblBillOfEntryNo
        '
        Me.LblBillOfEntryNo.AutoSize = True
        Me.LblBillOfEntryNo.BackColor = System.Drawing.Color.Transparent
        Me.LblBillOfEntryNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBillOfEntryNo.Location = New System.Drawing.Point(11, 73)
        Me.LblBillOfEntryNo.Name = "LblBillOfEntryNo"
        Me.LblBillOfEntryNo.Size = New System.Drawing.Size(102, 16)
        Me.LblBillOfEntryNo.TabIndex = 770
        Me.LblBillOfEntryNo.Text = "Bill Of Entry No."
        '
        'TxtShippingLine
        '
        Me.TxtShippingLine.AgMandatory = False
        Me.TxtShippingLine.AgMasterHelp = False
        Me.TxtShippingLine.AgNumberLeftPlaces = 8
        Me.TxtShippingLine.AgNumberNegetiveAllow = False
        Me.TxtShippingLine.AgNumberRightPlaces = 2
        Me.TxtShippingLine.AgPickFromLastValue = False
        Me.TxtShippingLine.AgRowFilter = ""
        Me.TxtShippingLine.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShippingLine.AgSelectedValue = Nothing
        Me.TxtShippingLine.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShippingLine.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShippingLine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShippingLine.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShippingLine.Location = New System.Drawing.Point(173, 93)
        Me.TxtShippingLine.MaxLength = 50
        Me.TxtShippingLine.Name = "TxtShippingLine"
        Me.TxtShippingLine.Size = New System.Drawing.Size(324, 18)
        Me.TxtShippingLine.TabIndex = 8
        '
        'LblShippingLine
        '
        Me.LblShippingLine.AutoSize = True
        Me.LblShippingLine.BackColor = System.Drawing.Color.Transparent
        Me.LblShippingLine.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShippingLine.Location = New System.Drawing.Point(11, 93)
        Me.LblShippingLine.Name = "LblShippingLine"
        Me.LblShippingLine.Size = New System.Drawing.Size(86, 16)
        Me.LblShippingLine.TabIndex = 774
        Me.LblShippingLine.Text = "Shipping Line"
        '
        'TxtCountryOfOrigin
        '
        Me.TxtCountryOfOrigin.AgMandatory = False
        Me.TxtCountryOfOrigin.AgMasterHelp = False
        Me.TxtCountryOfOrigin.AgNumberLeftPlaces = 8
        Me.TxtCountryOfOrigin.AgNumberNegetiveAllow = False
        Me.TxtCountryOfOrigin.AgNumberRightPlaces = 2
        Me.TxtCountryOfOrigin.AgPickFromLastValue = False
        Me.TxtCountryOfOrigin.AgRowFilter = ""
        Me.TxtCountryOfOrigin.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCountryOfOrigin.AgSelectedValue = Nothing
        Me.TxtCountryOfOrigin.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCountryOfOrigin.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCountryOfOrigin.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCountryOfOrigin.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCountryOfOrigin.Location = New System.Drawing.Point(173, 113)
        Me.TxtCountryOfOrigin.MaxLength = 50
        Me.TxtCountryOfOrigin.Name = "TxtCountryOfOrigin"
        Me.TxtCountryOfOrigin.Size = New System.Drawing.Size(324, 18)
        Me.TxtCountryOfOrigin.TabIndex = 9
        '
        'LblCountryOfOrigin
        '
        Me.LblCountryOfOrigin.AutoSize = True
        Me.LblCountryOfOrigin.BackColor = System.Drawing.Color.Transparent
        Me.LblCountryOfOrigin.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCountryOfOrigin.Location = New System.Drawing.Point(11, 113)
        Me.LblCountryOfOrigin.Name = "LblCountryOfOrigin"
        Me.LblCountryOfOrigin.Size = New System.Drawing.Size(108, 16)
        Me.LblCountryOfOrigin.TabIndex = 776
        Me.LblCountryOfOrigin.Text = "Country Of Origin"
        '
        'TxtInsuranceDetail
        '
        Me.TxtInsuranceDetail.AgMandatory = False
        Me.TxtInsuranceDetail.AgMasterHelp = False
        Me.TxtInsuranceDetail.AgNumberLeftPlaces = 8
        Me.TxtInsuranceDetail.AgNumberNegetiveAllow = False
        Me.TxtInsuranceDetail.AgNumberRightPlaces = 2
        Me.TxtInsuranceDetail.AgPickFromLastValue = False
        Me.TxtInsuranceDetail.AgRowFilter = ""
        Me.TxtInsuranceDetail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtInsuranceDetail.AgSelectedValue = Nothing
        Me.TxtInsuranceDetail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtInsuranceDetail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtInsuranceDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtInsuranceDetail.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInsuranceDetail.Location = New System.Drawing.Point(173, 133)
        Me.TxtInsuranceDetail.MaxLength = 0
        Me.TxtInsuranceDetail.Multiline = True
        Me.TxtInsuranceDetail.Name = "TxtInsuranceDetail"
        Me.TxtInsuranceDetail.Size = New System.Drawing.Size(324, 38)
        Me.TxtInsuranceDetail.TabIndex = 10
        '
        'lblInsuranceDetail
        '
        Me.lblInsuranceDetail.AutoSize = True
        Me.lblInsuranceDetail.BackColor = System.Drawing.Color.Transparent
        Me.lblInsuranceDetail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInsuranceDetail.Location = New System.Drawing.Point(11, 133)
        Me.lblInsuranceDetail.Name = "lblInsuranceDetail"
        Me.lblInsuranceDetail.Size = New System.Drawing.Size(101, 16)
        Me.lblInsuranceDetail.TabIndex = 778
        Me.lblInsuranceDetail.Text = "Insurance Detail"
        '
        'TxtPortOfDispatch
        '
        Me.TxtPortOfDispatch.AgMandatory = False
        Me.TxtPortOfDispatch.AgMasterHelp = False
        Me.TxtPortOfDispatch.AgNumberLeftPlaces = 8
        Me.TxtPortOfDispatch.AgNumberNegetiveAllow = False
        Me.TxtPortOfDispatch.AgNumberRightPlaces = 2
        Me.TxtPortOfDispatch.AgPickFromLastValue = False
        Me.TxtPortOfDispatch.AgRowFilter = ""
        Me.TxtPortOfDispatch.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPortOfDispatch.AgSelectedValue = Nothing
        Me.TxtPortOfDispatch.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPortOfDispatch.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPortOfDispatch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPortOfDispatch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPortOfDispatch.Location = New System.Drawing.Point(173, 173)
        Me.TxtPortOfDispatch.MaxLength = 50
        Me.TxtPortOfDispatch.Name = "TxtPortOfDispatch"
        Me.TxtPortOfDispatch.Size = New System.Drawing.Size(324, 18)
        Me.TxtPortOfDispatch.TabIndex = 11
        '
        'LblPortOfDispatch
        '
        Me.LblPortOfDispatch.AutoSize = True
        Me.LblPortOfDispatch.BackColor = System.Drawing.Color.Transparent
        Me.LblPortOfDispatch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPortOfDispatch.Location = New System.Drawing.Point(11, 173)
        Me.LblPortOfDispatch.Name = "LblPortOfDispatch"
        Me.LblPortOfDispatch.Size = New System.Drawing.Size(104, 16)
        Me.LblPortOfDispatch.TabIndex = 780
        Me.LblPortOfDispatch.Text = "Port Of Dispatch"
        '
        'TxtFinalPlaceOfDelivery
        '
        Me.TxtFinalPlaceOfDelivery.AgMandatory = False
        Me.TxtFinalPlaceOfDelivery.AgMasterHelp = False
        Me.TxtFinalPlaceOfDelivery.AgNumberLeftPlaces = 8
        Me.TxtFinalPlaceOfDelivery.AgNumberNegetiveAllow = False
        Me.TxtFinalPlaceOfDelivery.AgNumberRightPlaces = 2
        Me.TxtFinalPlaceOfDelivery.AgPickFromLastValue = False
        Me.TxtFinalPlaceOfDelivery.AgRowFilter = ""
        Me.TxtFinalPlaceOfDelivery.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFinalPlaceOfDelivery.AgSelectedValue = Nothing
        Me.TxtFinalPlaceOfDelivery.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFinalPlaceOfDelivery.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFinalPlaceOfDelivery.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFinalPlaceOfDelivery.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinalPlaceOfDelivery.Location = New System.Drawing.Point(173, 213)
        Me.TxtFinalPlaceOfDelivery.MaxLength = 100
        Me.TxtFinalPlaceOfDelivery.Name = "TxtFinalPlaceOfDelivery"
        Me.TxtFinalPlaceOfDelivery.Size = New System.Drawing.Size(324, 18)
        Me.TxtFinalPlaceOfDelivery.TabIndex = 13
        '
        'LblFinalPlaceOfDelivery
        '
        Me.LblFinalPlaceOfDelivery.AutoSize = True
        Me.LblFinalPlaceOfDelivery.BackColor = System.Drawing.Color.Transparent
        Me.LblFinalPlaceOfDelivery.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFinalPlaceOfDelivery.Location = New System.Drawing.Point(11, 212)
        Me.LblFinalPlaceOfDelivery.Name = "LblFinalPlaceOfDelivery"
        Me.LblFinalPlaceOfDelivery.Size = New System.Drawing.Size(139, 16)
        Me.LblFinalPlaceOfDelivery.TabIndex = 782
        Me.LblFinalPlaceOfDelivery.Text = "Final Place Of Delivery"
        '
        'TxtPortOfDicharge
        '
        Me.TxtPortOfDicharge.AgMandatory = False
        Me.TxtPortOfDicharge.AgMasterHelp = False
        Me.TxtPortOfDicharge.AgNumberLeftPlaces = 8
        Me.TxtPortOfDicharge.AgNumberNegetiveAllow = False
        Me.TxtPortOfDicharge.AgNumberRightPlaces = 2
        Me.TxtPortOfDicharge.AgPickFromLastValue = False
        Me.TxtPortOfDicharge.AgRowFilter = ""
        Me.TxtPortOfDicharge.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPortOfDicharge.AgSelectedValue = Nothing
        Me.TxtPortOfDicharge.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPortOfDicharge.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPortOfDicharge.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPortOfDicharge.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPortOfDicharge.Location = New System.Drawing.Point(173, 193)
        Me.TxtPortOfDicharge.MaxLength = 50
        Me.TxtPortOfDicharge.Name = "TxtPortOfDicharge"
        Me.TxtPortOfDicharge.Size = New System.Drawing.Size(324, 18)
        Me.TxtPortOfDicharge.TabIndex = 12
        '
        'LblPortOfDicharge
        '
        Me.LblPortOfDicharge.AutoSize = True
        Me.LblPortOfDicharge.BackColor = System.Drawing.Color.Transparent
        Me.LblPortOfDicharge.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPortOfDicharge.Location = New System.Drawing.Point(11, 193)
        Me.LblPortOfDicharge.Name = "LblPortOfDicharge"
        Me.LblPortOfDicharge.Size = New System.Drawing.Size(104, 16)
        Me.LblPortOfDicharge.TabIndex = 784
        Me.LblPortOfDicharge.Text = "Port Of Dicharge"
        '
        'TxtETAAtIndianSeaPort
        '
        Me.TxtETAAtIndianSeaPort.AgMandatory = False
        Me.TxtETAAtIndianSeaPort.AgMasterHelp = False
        Me.TxtETAAtIndianSeaPort.AgNumberLeftPlaces = 8
        Me.TxtETAAtIndianSeaPort.AgNumberNegetiveAllow = False
        Me.TxtETAAtIndianSeaPort.AgNumberRightPlaces = 2
        Me.TxtETAAtIndianSeaPort.AgPickFromLastValue = False
        Me.TxtETAAtIndianSeaPort.AgRowFilter = ""
        Me.TxtETAAtIndianSeaPort.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtETAAtIndianSeaPort.AgSelectedValue = Nothing
        Me.TxtETAAtIndianSeaPort.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtETAAtIndianSeaPort.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtETAAtIndianSeaPort.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtETAAtIndianSeaPort.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtETAAtIndianSeaPort.Location = New System.Drawing.Point(173, 233)
        Me.TxtETAAtIndianSeaPort.MaxLength = 50
        Me.TxtETAAtIndianSeaPort.Name = "TxtETAAtIndianSeaPort"
        Me.TxtETAAtIndianSeaPort.Size = New System.Drawing.Size(111, 18)
        Me.TxtETAAtIndianSeaPort.TabIndex = 14
        '
        'LblETAAtIndianSeaPort
        '
        Me.LblETAAtIndianSeaPort.AutoSize = True
        Me.LblETAAtIndianSeaPort.BackColor = System.Drawing.Color.Transparent
        Me.LblETAAtIndianSeaPort.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblETAAtIndianSeaPort.Location = New System.Drawing.Point(11, 233)
        Me.LblETAAtIndianSeaPort.Name = "LblETAAtIndianSeaPort"
        Me.LblETAAtIndianSeaPort.Size = New System.Drawing.Size(139, 16)
        Me.LblETAAtIndianSeaPort.TabIndex = 786
        Me.LblETAAtIndianSeaPort.Text = "ETA At Indian SeaPort"
        '
        'TxtETAAtICD
        '
        Me.TxtETAAtICD.AgMandatory = False
        Me.TxtETAAtICD.AgMasterHelp = False
        Me.TxtETAAtICD.AgNumberLeftPlaces = 8
        Me.TxtETAAtICD.AgNumberNegetiveAllow = False
        Me.TxtETAAtICD.AgNumberRightPlaces = 2
        Me.TxtETAAtICD.AgPickFromLastValue = False
        Me.TxtETAAtICD.AgRowFilter = ""
        Me.TxtETAAtICD.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtETAAtICD.AgSelectedValue = Nothing
        Me.TxtETAAtICD.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtETAAtICD.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtETAAtICD.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtETAAtICD.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtETAAtICD.Location = New System.Drawing.Point(388, 233)
        Me.TxtETAAtICD.MaxLength = 50
        Me.TxtETAAtICD.Name = "TxtETAAtICD"
        Me.TxtETAAtICD.Size = New System.Drawing.Size(109, 18)
        Me.TxtETAAtICD.TabIndex = 15
        '
        'LblETAAtICD
        '
        Me.LblETAAtICD.AutoSize = True
        Me.LblETAAtICD.BackColor = System.Drawing.Color.Transparent
        Me.LblETAAtICD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblETAAtICD.Location = New System.Drawing.Point(296, 233)
        Me.LblETAAtICD.Name = "LblETAAtICD"
        Me.LblETAAtICD.Size = New System.Drawing.Size(67, 16)
        Me.LblETAAtICD.TabIndex = 788
        Me.LblETAAtICD.Text = "ETAAtICD"
        '
        'TxtCHA
        '
        Me.TxtCHA.AgMandatory = False
        Me.TxtCHA.AgMasterHelp = False
        Me.TxtCHA.AgNumberLeftPlaces = 8
        Me.TxtCHA.AgNumberNegetiveAllow = False
        Me.TxtCHA.AgNumberRightPlaces = 2
        Me.TxtCHA.AgPickFromLastValue = False
        Me.TxtCHA.AgRowFilter = ""
        Me.TxtCHA.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCHA.AgSelectedValue = Nothing
        Me.TxtCHA.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCHA.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCHA.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCHA.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCHA.Location = New System.Drawing.Point(173, 253)
        Me.TxtCHA.MaxLength = 50
        Me.TxtCHA.Name = "TxtCHA"
        Me.TxtCHA.Size = New System.Drawing.Size(324, 18)
        Me.TxtCHA.TabIndex = 16
        '
        'LblCHA
        '
        Me.LblCHA.AutoSize = True
        Me.LblCHA.BackColor = System.Drawing.Color.Transparent
        Me.LblCHA.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCHA.Location = New System.Drawing.Point(11, 253)
        Me.LblCHA.Name = "LblCHA"
        Me.LblCHA.Size = New System.Drawing.Size(35, 16)
        Me.LblCHA.TabIndex = 790
        Me.LblCHA.Text = "CHA"
        '
        'TxtShipperInformationDate
        '
        Me.TxtShipperInformationDate.AgMandatory = False
        Me.TxtShipperInformationDate.AgMasterHelp = False
        Me.TxtShipperInformationDate.AgNumberLeftPlaces = 8
        Me.TxtShipperInformationDate.AgNumberNegetiveAllow = False
        Me.TxtShipperInformationDate.AgNumberRightPlaces = 2
        Me.TxtShipperInformationDate.AgPickFromLastValue = False
        Me.TxtShipperInformationDate.AgRowFilter = ""
        Me.TxtShipperInformationDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipperInformationDate.AgSelectedValue = Nothing
        Me.TxtShipperInformationDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipperInformationDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtShipperInformationDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipperInformationDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipperInformationDate.Location = New System.Drawing.Point(173, 273)
        Me.TxtShipperInformationDate.MaxLength = 50
        Me.TxtShipperInformationDate.Name = "TxtShipperInformationDate"
        Me.TxtShipperInformationDate.Size = New System.Drawing.Size(111, 18)
        Me.TxtShipperInformationDate.TabIndex = 17
        '
        'LblShipperInformationDate
        '
        Me.LblShipperInformationDate.AutoSize = True
        Me.LblShipperInformationDate.BackColor = System.Drawing.Color.Transparent
        Me.LblShipperInformationDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShipperInformationDate.Location = New System.Drawing.Point(11, 273)
        Me.LblShipperInformationDate.Name = "LblShipperInformationDate"
        Me.LblShipperInformationDate.Size = New System.Drawing.Size(158, 16)
        Me.LblShipperInformationDate.TabIndex = 792
        Me.LblShipperInformationDate.Text = "Shipper Information (Date)"
        '
        'TxtShipperInformationRemark
        '
        Me.TxtShipperInformationRemark.AgMandatory = False
        Me.TxtShipperInformationRemark.AgMasterHelp = False
        Me.TxtShipperInformationRemark.AgNumberLeftPlaces = 8
        Me.TxtShipperInformationRemark.AgNumberNegetiveAllow = False
        Me.TxtShipperInformationRemark.AgNumberRightPlaces = 2
        Me.TxtShipperInformationRemark.AgPickFromLastValue = False
        Me.TxtShipperInformationRemark.AgRowFilter = ""
        Me.TxtShipperInformationRemark.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipperInformationRemark.AgSelectedValue = Nothing
        Me.TxtShipperInformationRemark.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipperInformationRemark.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShipperInformationRemark.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipperInformationRemark.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipperInformationRemark.Location = New System.Drawing.Point(685, 14)
        Me.TxtShipperInformationRemark.MaxLength = 255
        Me.TxtShipperInformationRemark.Name = "TxtShipperInformationRemark"
        Me.TxtShipperInformationRemark.Size = New System.Drawing.Size(309, 18)
        Me.TxtShipperInformationRemark.TabIndex = 19
        '
        'LblShipperInformationRemark
        '
        Me.LblShipperInformationRemark.AutoSize = True
        Me.LblShipperInformationRemark.BackColor = System.Drawing.Color.Transparent
        Me.LblShipperInformationRemark.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShipperInformationRemark.Location = New System.Drawing.Point(503, 14)
        Me.LblShipperInformationRemark.Name = "LblShipperInformationRemark"
        Me.LblShipperInformationRemark.Size = New System.Drawing.Size(176, 16)
        Me.LblShipperInformationRemark.TabIndex = 794
        Me.LblShipperInformationRemark.Text = "Shipper Information (Remark)"
        '
        'TxtDocSubmissionDateToCHA
        '
        Me.TxtDocSubmissionDateToCHA.AgMandatory = False
        Me.TxtDocSubmissionDateToCHA.AgMasterHelp = False
        Me.TxtDocSubmissionDateToCHA.AgNumberLeftPlaces = 8
        Me.TxtDocSubmissionDateToCHA.AgNumberNegetiveAllow = False
        Me.TxtDocSubmissionDateToCHA.AgNumberRightPlaces = 2
        Me.TxtDocSubmissionDateToCHA.AgPickFromLastValue = False
        Me.TxtDocSubmissionDateToCHA.AgRowFilter = ""
        Me.TxtDocSubmissionDateToCHA.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDocSubmissionDateToCHA.AgSelectedValue = Nothing
        Me.TxtDocSubmissionDateToCHA.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDocSubmissionDateToCHA.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDocSubmissionDateToCHA.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDocSubmissionDateToCHA.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDocSubmissionDateToCHA.Location = New System.Drawing.Point(686, 34)
        Me.TxtDocSubmissionDateToCHA.MaxLength = 50
        Me.TxtDocSubmissionDateToCHA.Name = "TxtDocSubmissionDateToCHA"
        Me.TxtDocSubmissionDateToCHA.Size = New System.Drawing.Size(90, 18)
        Me.TxtDocSubmissionDateToCHA.TabIndex = 20
        '
        'LblDocSubmissionDateToCHA
        '
        Me.LblDocSubmissionDateToCHA.AutoSize = True
        Me.LblDocSubmissionDateToCHA.BackColor = System.Drawing.Color.Transparent
        Me.LblDocSubmissionDateToCHA.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocSubmissionDateToCHA.Location = New System.Drawing.Point(503, 34)
        Me.LblDocSubmissionDateToCHA.Name = "LblDocSubmissionDateToCHA"
        Me.LblDocSubmissionDateToCHA.Size = New System.Drawing.Size(183, 16)
        Me.LblDocSubmissionDateToCHA.TabIndex = 796
        Me.LblDocSubmissionDateToCHA.Text = "Doc Submission Date To CHA"
        '
        'AgTextBox13
        '
        Me.AgTextBox13.AgMandatory = False
        Me.AgTextBox13.AgMasterHelp = False
        Me.AgTextBox13.AgNumberLeftPlaces = 8
        Me.AgTextBox13.AgNumberNegetiveAllow = False
        Me.AgTextBox13.AgNumberRightPlaces = 2
        Me.AgTextBox13.AgPickFromLastValue = False
        Me.AgTextBox13.AgRowFilter = ""
        Me.AgTextBox13.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.AgTextBox13.AgSelectedValue = Nothing
        Me.AgTextBox13.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.AgTextBox13.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.AgTextBox13.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AgTextBox13.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AgTextBox13.Location = New System.Drawing.Point(906, 34)
        Me.AgTextBox13.MaxLength = 50
        Me.AgTextBox13.Name = "AgTextBox13"
        Me.AgTextBox13.Size = New System.Drawing.Size(89, 18)
        Me.AgTextBox13.TabIndex = 21
        '
        'LblClearingBillNo
        '
        Me.LblClearingBillNo.AutoSize = True
        Me.LblClearingBillNo.BackColor = System.Drawing.Color.Transparent
        Me.LblClearingBillNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblClearingBillNo.Location = New System.Drawing.Point(778, 34)
        Me.LblClearingBillNo.Name = "LblClearingBillNo"
        Me.LblClearingBillNo.Size = New System.Drawing.Size(101, 16)
        Me.LblClearingBillNo.TabIndex = 798
        Me.LblClearingBillNo.Text = "Clearing Bill No."
        '
        'TxtShipmentReleaseDate
        '
        Me.TxtShipmentReleaseDate.AgMandatory = False
        Me.TxtShipmentReleaseDate.AgMasterHelp = False
        Me.TxtShipmentReleaseDate.AgNumberLeftPlaces = 8
        Me.TxtShipmentReleaseDate.AgNumberNegetiveAllow = False
        Me.TxtShipmentReleaseDate.AgNumberRightPlaces = 2
        Me.TxtShipmentReleaseDate.AgPickFromLastValue = False
        Me.TxtShipmentReleaseDate.AgRowFilter = ""
        Me.TxtShipmentReleaseDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipmentReleaseDate.AgSelectedValue = Nothing
        Me.TxtShipmentReleaseDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipmentReleaseDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtShipmentReleaseDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipmentReleaseDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipmentReleaseDate.Location = New System.Drawing.Point(686, 73)
        Me.TxtShipmentReleaseDate.MaxLength = 20
        Me.TxtShipmentReleaseDate.Name = "TxtShipmentReleaseDate"
        Me.TxtShipmentReleaseDate.Size = New System.Drawing.Size(88, 18)
        Me.TxtShipmentReleaseDate.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(503, 73)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(141, 16)
        Me.Label15.TabIndex = 800
        Me.Label15.Text = "Shipment Realise Date"
        '
        'TxtTransporter
        '
        Me.TxtTransporter.AgMandatory = False
        Me.TxtTransporter.AgMasterHelp = False
        Me.TxtTransporter.AgNumberLeftPlaces = 8
        Me.TxtTransporter.AgNumberNegetiveAllow = False
        Me.TxtTransporter.AgNumberRightPlaces = 2
        Me.TxtTransporter.AgPickFromLastValue = False
        Me.TxtTransporter.AgRowFilter = ""
        Me.TxtTransporter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransporter.AgSelectedValue = Nothing
        Me.TxtTransporter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransporter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransporter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporter.Location = New System.Drawing.Point(686, 93)
        Me.TxtTransporter.MaxLength = 20
        Me.TxtTransporter.Name = "TxtTransporter"
        Me.TxtTransporter.Size = New System.Drawing.Size(307, 18)
        Me.TxtTransporter.TabIndex = 25
        '
        'LblTrasnsporterName
        '
        Me.LblTrasnsporterName.AutoSize = True
        Me.LblTrasnsporterName.BackColor = System.Drawing.Color.Transparent
        Me.LblTrasnsporterName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTrasnsporterName.Location = New System.Drawing.Point(503, 93)
        Me.LblTrasnsporterName.Name = "LblTrasnsporterName"
        Me.LblTrasnsporterName.Size = New System.Drawing.Size(118, 16)
        Me.LblTrasnsporterName.TabIndex = 802
        Me.LblTrasnsporterName.Text = "Trasnsporter Name"
        '
        'TxtVehicleNo
        '
        Me.TxtVehicleNo.AgMandatory = False
        Me.TxtVehicleNo.AgMasterHelp = False
        Me.TxtVehicleNo.AgNumberLeftPlaces = 8
        Me.TxtVehicleNo.AgNumberNegetiveAllow = False
        Me.TxtVehicleNo.AgNumberRightPlaces = 2
        Me.TxtVehicleNo.AgPickFromLastValue = False
        Me.TxtVehicleNo.AgRowFilter = ""
        Me.TxtVehicleNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVehicleNo.AgSelectedValue = Nothing
        Me.TxtVehicleNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVehicleNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVehicleNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVehicleNo.Location = New System.Drawing.Point(685, 113)
        Me.TxtVehicleNo.MaxLength = 20
        Me.TxtVehicleNo.Name = "TxtVehicleNo"
        Me.TxtVehicleNo.Size = New System.Drawing.Size(307, 18)
        Me.TxtVehicleNo.TabIndex = 26
        '
        'LblVehicleNo
        '
        Me.LblVehicleNo.AutoSize = True
        Me.LblVehicleNo.BackColor = System.Drawing.Color.Transparent
        Me.LblVehicleNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVehicleNo.Location = New System.Drawing.Point(503, 113)
        Me.LblVehicleNo.Name = "LblVehicleNo"
        Me.LblVehicleNo.Size = New System.Drawing.Size(75, 16)
        Me.LblVehicleNo.TabIndex = 804
        Me.LblVehicleNo.Text = "Vehicle No."
        '
        'TxtDriverName
        '
        Me.TxtDriverName.AgMandatory = False
        Me.TxtDriverName.AgMasterHelp = False
        Me.TxtDriverName.AgNumberLeftPlaces = 8
        Me.TxtDriverName.AgNumberNegetiveAllow = False
        Me.TxtDriverName.AgNumberRightPlaces = 2
        Me.TxtDriverName.AgPickFromLastValue = False
        Me.TxtDriverName.AgRowFilter = ""
        Me.TxtDriverName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDriverName.AgSelectedValue = Nothing
        Me.TxtDriverName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDriverName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDriverName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDriverName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDriverName.Location = New System.Drawing.Point(685, 133)
        Me.TxtDriverName.MaxLength = 50
        Me.TxtDriverName.Name = "TxtDriverName"
        Me.TxtDriverName.Size = New System.Drawing.Size(307, 18)
        Me.TxtDriverName.TabIndex = 27
        '
        'LblDriverName
        '
        Me.LblDriverName.AutoSize = True
        Me.LblDriverName.BackColor = System.Drawing.Color.Transparent
        Me.LblDriverName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDriverName.Location = New System.Drawing.Point(503, 133)
        Me.LblDriverName.Name = "LblDriverName"
        Me.LblDriverName.Size = New System.Drawing.Size(78, 16)
        Me.LblDriverName.TabIndex = 806
        Me.LblDriverName.Text = "Driver Name"
        '
        'TxtArrivalDateAtFactory
        '
        Me.TxtArrivalDateAtFactory.AgMandatory = False
        Me.TxtArrivalDateAtFactory.AgMasterHelp = False
        Me.TxtArrivalDateAtFactory.AgNumberLeftPlaces = 8
        Me.TxtArrivalDateAtFactory.AgNumberNegetiveAllow = False
        Me.TxtArrivalDateAtFactory.AgNumberRightPlaces = 2
        Me.TxtArrivalDateAtFactory.AgPickFromLastValue = False
        Me.TxtArrivalDateAtFactory.AgRowFilter = ""
        Me.TxtArrivalDateAtFactory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtArrivalDateAtFactory.AgSelectedValue = Nothing
        Me.TxtArrivalDateAtFactory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtArrivalDateAtFactory.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtArrivalDateAtFactory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtArrivalDateAtFactory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtArrivalDateAtFactory.Location = New System.Drawing.Point(685, 153)
        Me.TxtArrivalDateAtFactory.MaxLength = 20
        Me.TxtArrivalDateAtFactory.Name = "TxtArrivalDateAtFactory"
        Me.TxtArrivalDateAtFactory.Size = New System.Drawing.Size(88, 18)
        Me.TxtArrivalDateAtFactory.TabIndex = 28
        '
        'LblArrivalDateAtFactory
        '
        Me.LblArrivalDateAtFactory.AutoSize = True
        Me.LblArrivalDateAtFactory.BackColor = System.Drawing.Color.Transparent
        Me.LblArrivalDateAtFactory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblArrivalDateAtFactory.Location = New System.Drawing.Point(503, 154)
        Me.LblArrivalDateAtFactory.Name = "LblArrivalDateAtFactory"
        Me.LblArrivalDateAtFactory.Size = New System.Drawing.Size(139, 16)
        Me.LblArrivalDateAtFactory.TabIndex = 808
        Me.LblArrivalDateAtFactory.Text = "Arrival Date At Factory"
        '
        'TxtVehicleReturnDate
        '
        Me.TxtVehicleReturnDate.AgMandatory = False
        Me.TxtVehicleReturnDate.AgMasterHelp = False
        Me.TxtVehicleReturnDate.AgNumberLeftPlaces = 8
        Me.TxtVehicleReturnDate.AgNumberNegetiveAllow = False
        Me.TxtVehicleReturnDate.AgNumberRightPlaces = 2
        Me.TxtVehicleReturnDate.AgPickFromLastValue = False
        Me.TxtVehicleReturnDate.AgRowFilter = ""
        Me.TxtVehicleReturnDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVehicleReturnDate.AgSelectedValue = Nothing
        Me.TxtVehicleReturnDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVehicleReturnDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtVehicleReturnDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVehicleReturnDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVehicleReturnDate.Location = New System.Drawing.Point(906, 153)
        Me.TxtVehicleReturnDate.MaxLength = 20
        Me.TxtVehicleReturnDate.Name = "TxtVehicleReturnDate"
        Me.TxtVehicleReturnDate.Size = New System.Drawing.Size(88, 18)
        Me.TxtVehicleReturnDate.TabIndex = 29
        '
        'LblVehicleReturnDate
        '
        Me.LblVehicleReturnDate.AutoSize = True
        Me.LblVehicleReturnDate.BackColor = System.Drawing.Color.Transparent
        Me.LblVehicleReturnDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVehicleReturnDate.Location = New System.Drawing.Point(778, 154)
        Me.LblVehicleReturnDate.Name = "LblVehicleReturnDate"
        Me.LblVehicleReturnDate.Size = New System.Drawing.Size(124, 16)
        Me.LblVehicleReturnDate.TabIndex = 810
        Me.LblVehicleReturnDate.Text = "Vehicle Return Date"
        '
        'TxtProofSubmissionDate
        '
        Me.TxtProofSubmissionDate.AgMandatory = False
        Me.TxtProofSubmissionDate.AgMasterHelp = False
        Me.TxtProofSubmissionDate.AgNumberLeftPlaces = 8
        Me.TxtProofSubmissionDate.AgNumberNegetiveAllow = False
        Me.TxtProofSubmissionDate.AgNumberRightPlaces = 2
        Me.TxtProofSubmissionDate.AgPickFromLastValue = False
        Me.TxtProofSubmissionDate.AgRowFilter = ""
        Me.TxtProofSubmissionDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProofSubmissionDate.AgSelectedValue = Nothing
        Me.TxtProofSubmissionDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProofSubmissionDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtProofSubmissionDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProofSubmissionDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProofSubmissionDate.Location = New System.Drawing.Point(685, 173)
        Me.TxtProofSubmissionDate.MaxLength = 20
        Me.TxtProofSubmissionDate.Name = "TxtProofSubmissionDate"
        Me.TxtProofSubmissionDate.Size = New System.Drawing.Size(88, 18)
        Me.TxtProofSubmissionDate.TabIndex = 30
        '
        'LblProofSubmissionDate
        '
        Me.LblProofSubmissionDate.AutoSize = True
        Me.LblProofSubmissionDate.BackColor = System.Drawing.Color.Transparent
        Me.LblProofSubmissionDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProofSubmissionDate.Location = New System.Drawing.Point(503, 173)
        Me.LblProofSubmissionDate.Name = "LblProofSubmissionDate"
        Me.LblProofSubmissionDate.Size = New System.Drawing.Size(141, 16)
        Me.LblProofSubmissionDate.TabIndex = 812
        Me.LblProofSubmissionDate.Text = "Proof Submission Date"
        '
        'TxtBankAdviceNo
        '
        Me.TxtBankAdviceNo.AgMandatory = False
        Me.TxtBankAdviceNo.AgMasterHelp = False
        Me.TxtBankAdviceNo.AgNumberLeftPlaces = 8
        Me.TxtBankAdviceNo.AgNumberNegetiveAllow = False
        Me.TxtBankAdviceNo.AgNumberRightPlaces = 2
        Me.TxtBankAdviceNo.AgPickFromLastValue = False
        Me.TxtBankAdviceNo.AgRowFilter = ""
        Me.TxtBankAdviceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBankAdviceNo.AgSelectedValue = Nothing
        Me.TxtBankAdviceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankAdviceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBankAdviceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBankAdviceNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAdviceNo.Location = New System.Drawing.Point(685, 193)
        Me.TxtBankAdviceNo.MaxLength = 20
        Me.TxtBankAdviceNo.Name = "TxtBankAdviceNo"
        Me.TxtBankAdviceNo.Size = New System.Drawing.Size(89, 18)
        Me.TxtBankAdviceNo.TabIndex = 31
        '
        'LblBankAdviceNo
        '
        Me.LblBankAdviceNo.AutoSize = True
        Me.LblBankAdviceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblBankAdviceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBankAdviceNo.Location = New System.Drawing.Point(503, 193)
        Me.LblBankAdviceNo.Name = "LblBankAdviceNo"
        Me.LblBankAdviceNo.Size = New System.Drawing.Size(104, 16)
        Me.LblBankAdviceNo.TabIndex = 814
        Me.LblBankAdviceNo.Text = "Bank Advice No."
        '
        'TxtBankAdviceDate
        '
        Me.TxtBankAdviceDate.AgMandatory = False
        Me.TxtBankAdviceDate.AgMasterHelp = False
        Me.TxtBankAdviceDate.AgNumberLeftPlaces = 8
        Me.TxtBankAdviceDate.AgNumberNegetiveAllow = False
        Me.TxtBankAdviceDate.AgNumberRightPlaces = 2
        Me.TxtBankAdviceDate.AgPickFromLastValue = False
        Me.TxtBankAdviceDate.AgRowFilter = ""
        Me.TxtBankAdviceDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBankAdviceDate.AgSelectedValue = Nothing
        Me.TxtBankAdviceDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankAdviceDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtBankAdviceDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBankAdviceDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAdviceDate.Location = New System.Drawing.Point(906, 193)
        Me.TxtBankAdviceDate.MaxLength = 20
        Me.TxtBankAdviceDate.Name = "TxtBankAdviceDate"
        Me.TxtBankAdviceDate.Size = New System.Drawing.Size(87, 18)
        Me.TxtBankAdviceDate.TabIndex = 32
        '
        'LblBankAdviceDate
        '
        Me.LblBankAdviceDate.AutoSize = True
        Me.LblBankAdviceDate.BackColor = System.Drawing.Color.Transparent
        Me.LblBankAdviceDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBankAdviceDate.Location = New System.Drawing.Point(778, 193)
        Me.LblBankAdviceDate.Name = "LblBankAdviceDate"
        Me.LblBankAdviceDate.Size = New System.Drawing.Size(111, 16)
        Me.LblBankAdviceDate.TabIndex = 816
        Me.LblBankAdviceDate.Text = "Bank Advice Date"
        '
        'TxtExchangeRate
        '
        Me.TxtExchangeRate.AgMandatory = False
        Me.TxtExchangeRate.AgMasterHelp = False
        Me.TxtExchangeRate.AgNumberLeftPlaces = 8
        Me.TxtExchangeRate.AgNumberNegetiveAllow = False
        Me.TxtExchangeRate.AgNumberRightPlaces = 2
        Me.TxtExchangeRate.AgPickFromLastValue = False
        Me.TxtExchangeRate.AgRowFilter = ""
        Me.TxtExchangeRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExchangeRate.AgSelectedValue = Nothing
        Me.TxtExchangeRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExchangeRate.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtExchangeRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExchangeRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExchangeRate.Location = New System.Drawing.Point(686, 213)
        Me.TxtExchangeRate.MaxLength = 20
        Me.TxtExchangeRate.Name = "TxtExchangeRate"
        Me.TxtExchangeRate.Size = New System.Drawing.Size(89, 18)
        Me.TxtExchangeRate.TabIndex = 33
        '
        'LblExchangeRate
        '
        Me.LblExchangeRate.AutoSize = True
        Me.LblExchangeRate.BackColor = System.Drawing.Color.Transparent
        Me.LblExchangeRate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblExchangeRate.Location = New System.Drawing.Point(503, 212)
        Me.LblExchangeRate.Name = "LblExchangeRate"
        Me.LblExchangeRate.Size = New System.Drawing.Size(97, 16)
        Me.LblExchangeRate.TabIndex = 818
        Me.LblExchangeRate.Text = "Exchange Rate"
        '
        'TxtBillOfLadingNo
        '
        Me.TxtBillOfLadingNo.AgMandatory = False
        Me.TxtBillOfLadingNo.AgMasterHelp = False
        Me.TxtBillOfLadingNo.AgNumberLeftPlaces = 8
        Me.TxtBillOfLadingNo.AgNumberNegetiveAllow = False
        Me.TxtBillOfLadingNo.AgNumberRightPlaces = 2
        Me.TxtBillOfLadingNo.AgPickFromLastValue = False
        Me.TxtBillOfLadingNo.AgRowFilter = ""
        Me.TxtBillOfLadingNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillOfLadingNo.AgSelectedValue = Nothing
        Me.TxtBillOfLadingNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillOfLadingNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillOfLadingNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillOfLadingNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillOfLadingNo.Location = New System.Drawing.Point(686, 233)
        Me.TxtBillOfLadingNo.MaxLength = 20
        Me.TxtBillOfLadingNo.Name = "TxtBillOfLadingNo"
        Me.TxtBillOfLadingNo.Size = New System.Drawing.Size(89, 18)
        Me.TxtBillOfLadingNo.TabIndex = 34
        '
        'LblBillOfLadingNo
        '
        Me.LblBillOfLadingNo.AutoSize = True
        Me.LblBillOfLadingNo.BackColor = System.Drawing.Color.Transparent
        Me.LblBillOfLadingNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBillOfLadingNo.Location = New System.Drawing.Point(503, 233)
        Me.LblBillOfLadingNo.Name = "LblBillOfLadingNo"
        Me.LblBillOfLadingNo.Size = New System.Drawing.Size(109, 16)
        Me.LblBillOfLadingNo.TabIndex = 820
        Me.LblBillOfLadingNo.Text = "Bill Of Lading No."
        '
        'TxtBillOfLadingDate
        '
        Me.TxtBillOfLadingDate.AgMandatory = False
        Me.TxtBillOfLadingDate.AgMasterHelp = False
        Me.TxtBillOfLadingDate.AgNumberLeftPlaces = 8
        Me.TxtBillOfLadingDate.AgNumberNegetiveAllow = False
        Me.TxtBillOfLadingDate.AgNumberRightPlaces = 2
        Me.TxtBillOfLadingDate.AgPickFromLastValue = False
        Me.TxtBillOfLadingDate.AgRowFilter = ""
        Me.TxtBillOfLadingDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillOfLadingDate.AgSelectedValue = Nothing
        Me.TxtBillOfLadingDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillOfLadingDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtBillOfLadingDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillOfLadingDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillOfLadingDate.Location = New System.Drawing.Point(906, 233)
        Me.TxtBillOfLadingDate.MaxLength = 20
        Me.TxtBillOfLadingDate.Name = "TxtBillOfLadingDate"
        Me.TxtBillOfLadingDate.Size = New System.Drawing.Size(89, 18)
        Me.TxtBillOfLadingDate.TabIndex = 35
        '
        'LblBillOfLadingDate
        '
        Me.LblBillOfLadingDate.AutoSize = True
        Me.LblBillOfLadingDate.BackColor = System.Drawing.Color.Transparent
        Me.LblBillOfLadingDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBillOfLadingDate.Location = New System.Drawing.Point(778, 233)
        Me.LblBillOfLadingDate.Name = "LblBillOfLadingDate"
        Me.LblBillOfLadingDate.Size = New System.Drawing.Size(116, 16)
        Me.LblBillOfLadingDate.TabIndex = 822
        Me.LblBillOfLadingDate.Text = "Bill Of Lading Date"
        '
        'TxtShipmentOnBoardDate
        '
        Me.TxtShipmentOnBoardDate.AgMandatory = False
        Me.TxtShipmentOnBoardDate.AgMasterHelp = False
        Me.TxtShipmentOnBoardDate.AgNumberLeftPlaces = 8
        Me.TxtShipmentOnBoardDate.AgNumberNegetiveAllow = False
        Me.TxtShipmentOnBoardDate.AgNumberRightPlaces = 2
        Me.TxtShipmentOnBoardDate.AgPickFromLastValue = False
        Me.TxtShipmentOnBoardDate.AgRowFilter = ""
        Me.TxtShipmentOnBoardDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipmentOnBoardDate.AgSelectedValue = Nothing
        Me.TxtShipmentOnBoardDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipmentOnBoardDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtShipmentOnBoardDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipmentOnBoardDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipmentOnBoardDate.Location = New System.Drawing.Point(685, 253)
        Me.TxtShipmentOnBoardDate.MaxLength = 20
        Me.TxtShipmentOnBoardDate.Name = "TxtShipmentOnBoardDate"
        Me.TxtShipmentOnBoardDate.Size = New System.Drawing.Size(90, 18)
        Me.TxtShipmentOnBoardDate.TabIndex = 36
        '
        'LblShipmentOnBoardDate
        '
        Me.LblShipmentOnBoardDate.AutoSize = True
        Me.LblShipmentOnBoardDate.BackColor = System.Drawing.Color.Transparent
        Me.LblShipmentOnBoardDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShipmentOnBoardDate.Location = New System.Drawing.Point(503, 253)
        Me.LblShipmentOnBoardDate.Name = "LblShipmentOnBoardDate"
        Me.LblShipmentOnBoardDate.Size = New System.Drawing.Size(153, 16)
        Me.LblShipmentOnBoardDate.TabIndex = 824
        Me.LblShipmentOnBoardDate.Text = "Shipment On Board Date"
        '
        'TxtVesselName
        '
        Me.TxtVesselName.AgMandatory = False
        Me.TxtVesselName.AgMasterHelp = False
        Me.TxtVesselName.AgNumberLeftPlaces = 8
        Me.TxtVesselName.AgNumberNegetiveAllow = False
        Me.TxtVesselName.AgNumberRightPlaces = 2
        Me.TxtVesselName.AgPickFromLastValue = False
        Me.TxtVesselName.AgRowFilter = ""
        Me.TxtVesselName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVesselName.AgSelectedValue = Nothing
        Me.TxtVesselName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVesselName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVesselName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVesselName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVesselName.Location = New System.Drawing.Point(686, 273)
        Me.TxtVesselName.MaxLength = 100
        Me.TxtVesselName.Name = "TxtVesselName"
        Me.TxtVesselName.Size = New System.Drawing.Size(306, 18)
        Me.TxtVesselName.TabIndex = 37
        '
        'LblVesselName
        '
        Me.LblVesselName.AutoSize = True
        Me.LblVesselName.BackColor = System.Drawing.Color.Transparent
        Me.LblVesselName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVesselName.Location = New System.Drawing.Point(503, 273)
        Me.LblVesselName.Name = "LblVesselName"
        Me.LblVesselName.Size = New System.Drawing.Size(86, 16)
        Me.LblVesselName.TabIndex = 826
        Me.LblVesselName.Text = "Vessel Name"
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl2.Location = New System.Drawing.Point(405, 3)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(594, 200)
        Me.Pnl2.TabIndex = 1
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TpShipmentDetail1)
        Me.TabControl2.Controls.Add(Me.TpShipmentItemDetail1)
        Me.TabControl2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl2.Location = New System.Drawing.Point(0, 352)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(1016, 233)
        Me.TabControl2.TabIndex = 1
        '
        'TpShipmentDetail1
        '
        Me.TpShipmentDetail1.Controls.Add(Me.Pnl2)
        Me.TpShipmentDetail1.Controls.Add(Me.Pnl1)
        Me.TpShipmentDetail1.Location = New System.Drawing.Point(4, 22)
        Me.TpShipmentDetail1.Name = "TpShipmentDetail1"
        Me.TpShipmentDetail1.Padding = New System.Windows.Forms.Padding(3)
        Me.TpShipmentDetail1.Size = New System.Drawing.Size(1008, 207)
        Me.TpShipmentDetail1.TabIndex = 0
        Me.TpShipmentDetail1.Text = "Shipment Detail"
        Me.TpShipmentDetail1.UseVisualStyleBackColor = True
        '
        'TpShipmentItemDetail1
        '
        Me.TpShipmentItemDetail1.Controls.Add(Me.PnlCalcGrid)
        Me.TpShipmentItemDetail1.Controls.Add(Me.Pnl3)
        Me.TpShipmentItemDetail1.Location = New System.Drawing.Point(4, 22)
        Me.TpShipmentItemDetail1.Name = "TpShipmentItemDetail1"
        Me.TpShipmentItemDetail1.Padding = New System.Windows.Forms.Padding(3)
        Me.TpShipmentItemDetail1.Size = New System.Drawing.Size(1008, 207)
        Me.TpShipmentItemDetail1.TabIndex = 1
        Me.TpShipmentItemDetail1.Text = "Shipment Item Detail"
        Me.TpShipmentItemDetail1.UseVisualStyleBackColor = True
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PnlCalcGrid.Location = New System.Drawing.Point(641, 3)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(363, 200)
        Me.PnlCalcGrid.TabIndex = 6
        '
        'Pnl3
        '
        Me.Pnl3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl3.Location = New System.Drawing.Point(2, 3)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(635, 200)
        Me.Pnl3.TabIndex = 5
        '
        'TxtStructure
        '
        Me.TxtStructure.AgMandatory = False
        Me.TxtStructure.AgMasterHelp = False
        Me.TxtStructure.AgNumberLeftPlaces = 8
        Me.TxtStructure.AgNumberNegetiveAllow = False
        Me.TxtStructure.AgNumberRightPlaces = 2
        Me.TxtStructure.AgPickFromLastValue = False
        Me.TxtStructure.AgRowFilter = ""
        Me.TxtStructure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtStructure.AgSelectedValue = Nothing
        Me.TxtStructure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStructure.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStructure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStructure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStructure.Location = New System.Drawing.Point(363, 273)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(134, 18)
        Me.TxtStructure.TabIndex = 18
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(296, 274)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 828
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'FrmShipmentEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(1012, 626)
        Me.Controls.Add(Me.TabControl2)
        Me.Name = "FrmShipmentEntry"
        Me.Text = "Template Purchase Indent"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.TabControl2, 0)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl2.ResumeLayout(False)
        Me.TpShipmentDetail1.ResumeLayout(False)
        Me.TpShipmentItemDetail1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtPurchaseOrder As AgControls.AgTextBox
    Protected WithEvents LblPurchaseOrder As System.Windows.Forms.Label
    Protected WithEvents LblEntryNoReq As System.Windows.Forms.Label
    Protected WithEvents TxtPurchaseOrderReferenceNo As AgControls.AgTextBox
    Protected WithEvents LblPoReferenceNo As System.Windows.Forms.Label
    Protected WithEvents TxtDutyAmountPaidDate As AgControls.AgTextBox
    Protected WithEvents LblExportExpiryDate As System.Windows.Forms.Label
    Protected WithEvents TxtDocRealisationDate As AgControls.AgTextBox
    Protected WithEvents LblDocRealisationDate As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtBillOfEntryDate As AgControls.AgTextBox
    Protected WithEvents LblBillOfEntrydate As System.Windows.Forms.Label
    Protected WithEvents TxtBillOfEntryNo As AgControls.AgTextBox
    Protected WithEvents LblBillOfEntryNo As System.Windows.Forms.Label
    Protected WithEvents TxtInvoiceDate As AgControls.AgTextBox
    Protected WithEvents LblInvoiceDate As System.Windows.Forms.Label
    Protected WithEvents TxtInvoiceNo As AgControls.AgTextBox
    Protected WithEvents LblInvoiceNo As System.Windows.Forms.Label
    Protected WithEvents TxtShipperInformationDate As AgControls.AgTextBox
    Protected WithEvents LblShipperInformationDate As System.Windows.Forms.Label
    Protected WithEvents TxtCHA As AgControls.AgTextBox
    Protected WithEvents LblCHA As System.Windows.Forms.Label
    Protected WithEvents TxtETAAtICD As AgControls.AgTextBox
    Protected WithEvents LblETAAtICD As System.Windows.Forms.Label
    Protected WithEvents TxtETAAtIndianSeaPort As AgControls.AgTextBox
    Protected WithEvents LblETAAtIndianSeaPort As System.Windows.Forms.Label
    Protected WithEvents TxtPortOfDicharge As AgControls.AgTextBox
    Protected WithEvents LblPortOfDicharge As System.Windows.Forms.Label
    Protected WithEvents TxtFinalPlaceOfDelivery As AgControls.AgTextBox
    Protected WithEvents LblFinalPlaceOfDelivery As System.Windows.Forms.Label
    Protected WithEvents TxtPortOfDispatch As AgControls.AgTextBox
    Protected WithEvents LblPortOfDispatch As System.Windows.Forms.Label
    Protected WithEvents TxtInsuranceDetail As AgControls.AgTextBox
    Protected WithEvents lblInsuranceDetail As System.Windows.Forms.Label
    Protected WithEvents TxtCountryOfOrigin As AgControls.AgTextBox
    Protected WithEvents LblCountryOfOrigin As System.Windows.Forms.Label
    Protected WithEvents TxtShippingLine As AgControls.AgTextBox
    Protected WithEvents LblShippingLine As System.Windows.Forms.Label
    Protected WithEvents AgTextBox13 As AgControls.AgTextBox
    Protected WithEvents LblClearingBillNo As System.Windows.Forms.Label
    Protected WithEvents TxtDocSubmissionDateToCHA As AgControls.AgTextBox
    Protected WithEvents LblDocSubmissionDateToCHA As System.Windows.Forms.Label
    Protected WithEvents TxtShipperInformationRemark As AgControls.AgTextBox
    Protected WithEvents LblShipperInformationRemark As System.Windows.Forms.Label
    Protected WithEvents TxtShipmentReleaseDate As AgControls.AgTextBox
    Protected WithEvents Label15 As System.Windows.Forms.Label
    Protected WithEvents TxtArrivalDateAtFactory As AgControls.AgTextBox
    Protected WithEvents LblArrivalDateAtFactory As System.Windows.Forms.Label
    Protected WithEvents TxtDriverName As AgControls.AgTextBox
    Protected WithEvents LblDriverName As System.Windows.Forms.Label
    Protected WithEvents TxtVehicleNo As AgControls.AgTextBox
    Protected WithEvents LblVehicleNo As System.Windows.Forms.Label
    Protected WithEvents TxtTransporter As AgControls.AgTextBox
    Protected WithEvents LblTrasnsporterName As System.Windows.Forms.Label
    Protected WithEvents TxtBankAdviceDate As AgControls.AgTextBox
    Protected WithEvents LblBankAdviceDate As System.Windows.Forms.Label
    Protected WithEvents TxtBankAdviceNo As AgControls.AgTextBox
    Protected WithEvents LblBankAdviceNo As System.Windows.Forms.Label
    Protected WithEvents TxtProofSubmissionDate As AgControls.AgTextBox
    Protected WithEvents LblProofSubmissionDate As System.Windows.Forms.Label
    Protected WithEvents TxtVehicleReturnDate As AgControls.AgTextBox
    Protected WithEvents LblVehicleReturnDate As System.Windows.Forms.Label
    Protected WithEvents TxtBillOfLadingDate As AgControls.AgTextBox
    Protected WithEvents LblBillOfLadingDate As System.Windows.Forms.Label
    Protected WithEvents TxtBillOfLadingNo As AgControls.AgTextBox
    Protected WithEvents LblBillOfLadingNo As System.Windows.Forms.Label
    Protected WithEvents TxtExchangeRate As AgControls.AgTextBox
    Protected WithEvents LblExchangeRate As System.Windows.Forms.Label
    Protected WithEvents TxtVesselName As AgControls.AgTextBox
    Protected WithEvents LblVesselName As System.Windows.Forms.Label
    Protected WithEvents TxtShipmentOnBoardDate As AgControls.AgTextBox
    Protected WithEvents LblShipmentOnBoardDate As System.Windows.Forms.Label
    Protected WithEvents Pnl2 As System.Windows.Forms.Panel
    Protected WithEvents TabControl2 As System.Windows.Forms.TabControl
    Protected WithEvents TpShipmentDetail1 As System.Windows.Forms.TabPage
    Protected WithEvents TpShipmentItemDetail1 As System.Windows.Forms.TabPage
    Protected WithEvents Pnl3 As System.Windows.Forms.Panel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "IE_Shipment"
        LogTableName = "IE_Shipment_Log"
        MainLineTableCsv = "IE_ShipmentDoc,IE_ShipmentBOE,IE_ShipmentItem,Structure_TransFooter,Structure_TransLine"
        LogLineTableCsv = "IE_ShipmentDoc_Log,IE_ShipmentBOE_Log,IE_ShipmentItem_Log,Structure_TransFooter_Log,Structure_TransLine_Log"
        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl2)
        AgL.GridDesign(Dgl3)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " & _
                " From IE_Shipment H " & _
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
                " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select H.UID As SearchCode " & _
               " From IE_Shipment_Log H " & _
               " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
               " Where H.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By H.EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = "SELECT H.DocID, Vt.Description AS [Entry Type], H.V_Date AS [Entry Date], " & _
                            " H.V_No AS [Entry No] " & _
                            " FROM IE_Shipment H " & _
                            " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " & _
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.UID as SearchCode, H.DocId, Vt.Description AS [Entry Type], " & _
                            " H.V_Date AS [Entry Date], H.V_No AS [Entry No] " & _
                            " FROM IE_Shipment_Log H " & _
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                            " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Document, 100, 5, Col1Document, True, False, False)
            .AddAgTextColumn(Dgl1, Col1DocumentNo, 220, 5, Col1DocumentNo, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 25

        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2BOE, 100, 5, Col2BOE, True, False, False)
            .AddAgTextColumn(Dgl2, Col2FCValue, 100, 5, Col2FCValue, True, False, False)
            .AddAgTextColumn(Dgl2, Col2INRValue, 100, 5, Col2INRValue, True, False, False)
            .AddAgTextColumn(Dgl2, Col2Term, 100, 5, Col2Term, True, False, False)
            .AddAgDateColumn(Dgl2, Col2DueDate, 100, Col2DueDate, True, False)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 25


        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl3, Col3Item, 100, 5, Col3Item, True, False, False)
            .AddAgTextColumn(Dgl3, Col3ItemDescription, 190, 100, Col3ItemDescription, True, False, False)
            .AddAgTextColumn(Dgl3, Col3Unit, 50, 5, Col3Unit, True, True, False)
            .AddAgNumberColumn(Dgl3, Col3Qty, 50, 8, 4, False, Col3Qty, True)
            .AddAgNumberColumn(Dgl3, Col3Rate, 50, 8, 2, False, Col3Rate, True)
            .AddAgNumberColumn(Dgl3, Col3Amount, 70, 8, 2, False, Col3Amount, True, False)
            .AddAgTextColumn(Dgl3, Col3ContainerNo, 130, 100, Col3ContainerNo, True, False, False)
            .AddAgTextColumn(Dgl3, Col3KindsOfPackages, 130, 100, Col3KindsOfPackages, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 35

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)
        AgCalcGrid1.AgLineGrid = Dgl3
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl3.Columns(Col3Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl3.Columns(Col3Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl3.Columns(Col3ContainerNo).Index

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl3.AgSkipReadOnlyColumns = True

        FrmProductionOrder_BaseFunction_FIniList()

        'Ini_List()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        mQry = "UPDATE IE_Shipment_Log " & _
                " SET PurchaseOrder = " & AgL.Chk_Text(TxtPurchaseOrder.AgSelectedValue) & ", " & _
                " PurchaseOrderReferenceNo = " & AgL.Chk_Text(TxtPurchaseOrderReferenceNo.Text) & ", " & _
                " InvoiceNo = " & AgL.Chk_Text(TxtInvoiceNo.Text) & ", " & _
                " InvoiceDate = " & AgL.Chk_Text(TxtInvoiceDate.Text) & ", " & _
                " BillOfEntryNo = " & AgL.Chk_Text(TxtBillOfEntryNo.Text) & ", " & _
                " BillOfEntryDate = " & AgL.Chk_Text(TxtBillOfEntryDate.Text) & ", " & _
                " ShippingLine = " & AgL.Chk_Text(TxtShippingLine.Text) & ", " & _
                " CountryOfOrigin = " & AgL.Chk_Text(TxtCountryOfOrigin.Text) & ", " & _
                " InsuranceDetail = " & AgL.Chk_Text(TxtInsuranceDetail.Text) & ", " & _
                " PortOfDispatch = " & AgL.Chk_Text(TxtPortOfDispatch.Text) & ", " & _
                " PortOfDicharge = " & AgL.Chk_Text(TxtPortOfDicharge.Text) & ", " & _
                " FinalPlaceOfDelivery = " & AgL.Chk_Text(TxtFinalPlaceOfDelivery.Text) & ", " & _
                " ETAAtIndianSeaPort = " & AgL.Chk_Text(TxtETAAtIndianSeaPort.Text) & ", " & _
                " ETAAtICD = " & AgL.Chk_Text(TxtETAAtICD.Text) & ", " & _
                " CHA = " & AgL.Chk_Text(TxtCHA.AgSelectedValue) & ", " & _
                " ShipperInformationDate = " & AgL.Chk_Text(TxtShipperInformationDate.Text) & ", " & _
                " ShipperInformationRemark = " & AgL.Chk_Text(TxtShipperInformationRemark.Text) & ", " & _
                " DocSubmissionDateToCHA = " & AgL.Chk_Text(TxtDocSubmissionDateToCHA.Text) & ", " & _
                " DocRealisationDate = " & AgL.Chk_Text(TxtDocRealisationDate.Text) & ", " & _
                " DutyAmountPaidDate = " & AgL.Chk_Text(TxtDutyAmountPaidDate.Text) & ", " & _
                " ShipmentReleaseDate = " & AgL.Chk_Text(TxtShipmentReleaseDate.Text) & ", " & _
                " Transporter = " & AgL.Chk_Text(TxtTransporter.AgSelectedValue) & ", " & _
                " VehicleNo = " & AgL.Chk_Text(TxtVehicleNo.Text) & ", " & _
                " DriverName = " & AgL.Chk_Text(TxtDriverName.Text) & ", " & _
                " ArrivalDateAtFactory = " & AgL.Chk_Text(TxtArrivalDateAtFactory.Text) & ", " & _
                " VehicleReturnDate = " & AgL.Chk_Text(TxtVehicleReturnDate.Text) & ", " & _
                " ProofSubmissionDate = " & AgL.Chk_Text(TxtProofSubmissionDate.Text) & ", " & _
                " BankAdviceNo = " & AgL.Chk_Text(TxtBankAdviceNo.Text) & ", " & _
                " BankAdviceDate = " & AgL.Chk_Text(TxtBankAdviceDate.Text) & ", " & _
                " ExchangeRate = " & Val(TxtExchangeRate.Text) & ", " & _
                " BillOfLadingNo = " & AgL.Chk_Text(TxtBillOfLadingNo.Text) & ", " & _
                " BillOfLadingDate = " & AgL.Chk_Text(TxtBillOfLadingDate.Text) & ", " & _
                " ShipmentOnBoardDate = " & AgL.Chk_Text(TxtShipmentOnBoardDate.Text) & ", " & _
                " VesselName = " & AgL.Chk_Text(TxtVesselName.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        AgCalcGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)

        mQry = "Delete From IE_ShipmentDoc_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From IE_ShipmentBOE_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From IE_ShipmentItem_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Document, I).Value <> "" Then
                    mSr += 1
                    mQry = "INSERT INTO dbo.IE_ShipmentDoc_Log(UID, DocId, Sr, Document, DocumentNo) " & _
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                            " " & AgL.Chk_Text(mInternalCode) & ",	" & mSr & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1Document, I)) & ",	" & _
                            " " & AgL.Chk_Text(.Item(Col1DocumentNo, I).Value) & " )"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

        With Dgl2
            For I = 0 To .RowCount - 1
                If .Item(Col2BOE, I).Value <> "" Then
                    mSr += 1
                    mQry = "INSERT INTO IE_ShipmentBOE_Log(UID, DocId, Sr, BOE, FCValue, INRValue, Term, DueDate) " & _
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & "," & _
                            " " & AgL.Chk_Text(mInternalCode) & ",	" & mSr & ", " & _
                            " " & AgL.Chk_Text(.Item(Col2BOE, I).Value) & ", " & _
                            " " & Val(.Item(Col2FCValue, I).Value) & ",	" & _
                            " " & Val(.Item(Col2INRValue, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col2Term, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col2DueDate, I).Value) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

        With Dgl3
            For I = 0 To .RowCount - 1
                If .Item(Col3Item, I).Value <> "" Then
                    mSr += 1
                    mQry = "INSERT INTO IE_ShipmentItem_Log(UID, DocId, Sr, Item, ItemDescription, Unit, Qty, " & _
                            " Rate, Amount, ContainerNo, KindsOfPackages ) " & _
                            " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " & _
                            " " & AgL.Chk_Text(mInternalCode) & ",	" & mSr & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col3Item, I)) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3ItemDescription, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3Unit, I).Value) & ", " & _
                            " " & Val(.Item(Col3Qty, I).Value) & ", " & Val(.Item(Col3Rate, I).Value) & ", " & _
                            " " & Val(.Item(Col3Amount, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3ContainerNo, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3KindsOfPackages, I).Value) & " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    AgCalcGrid1.Save_TransLine(mInternalCode, mSr, I, Conn, Cmd, SearchCode)
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select H.* " & _
                " From IE_Shipment H " & _
                " Where H.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select H.* " & _
                " From IE_Shipment_Log H " & _
                " Where H.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.AgSelectedValue = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue

                IniGrid()
                TxtPurchaseOrder.AgSelectedValue = AgL.XNull(.Rows(0)("PurchaseOrder"))
                TxtPurchaseOrderReferenceNo.Text = AgL.XNull(.Rows(0)("PurchaseOrderReferenceNo"))
                TxtInvoiceNo.Text = AgL.XNull(.Rows(0)("InvoiceNo"))
                TxtInvoiceDate.Text = AgL.XNull(.Rows(0)("InvoiceDate"))
                TxtBillOfEntryNo.Text = AgL.XNull(.Rows(0)("BillOfEntryNo"))
                TxtBillOfEntryDate.Text = AgL.XNull(.Rows(0)("BillOfEntryDate"))
                TxtShippingLine.Text = AgL.XNull(.Rows(0)("ShippingLine"))
                TxtCountryOfOrigin.Text = AgL.XNull(.Rows(0)("CountryOfOrigin"))
                TxtInsuranceDetail.Text = AgL.XNull(.Rows(0)("InsuranceDetail"))
                TxtPortOfDispatch.Text = AgL.XNull(.Rows(0)("PortOfDispatch"))
                TxtPortOfDicharge.Text = AgL.XNull(.Rows(0)("PortOfDicharge"))
                TxtFinalPlaceOfDelivery.Text = AgL.XNull(.Rows(0)("FinalPlaceOfDelivery"))
                TxtETAAtIndianSeaPort.Text = AgL.XNull(.Rows(0)("ETAAtIndianSeaPort"))
                TxtETAAtICD.Text = AgL.XNull(.Rows(0)("ETAAtICD"))
                TxtCHA.AgSelectedValue = AgL.XNull(.Rows(0)("CHA"))
                TxtShipperInformationDate.Text = AgL.XNull(.Rows(0)("ShipperInformationDate"))
                TxtShipperInformationRemark.Text = AgL.XNull(.Rows(0)("ShipperInformationRemark"))
                TxtDocSubmissionDateToCHA.Text = AgL.XNull(.Rows(0)("DocSubmissionDateToCHA"))
                TxtDocRealisationDate.Text = AgL.XNull(.Rows(0)("DocRealisationDate"))
                TxtDutyAmountPaidDate.Text = AgL.XNull(.Rows(0)("DutyAmountPaidDate"))
                TxtShipmentReleaseDate.Text = AgL.XNull(.Rows(0)("ShipmentReleaseDate"))
                TxtTransporter.AgSelectedValue = AgL.XNull(.Rows(0)("Transporter"))
                TxtVehicleNo.Text = AgL.XNull(.Rows(0)("VehicleNo"))
                TxtDriverName.Text = AgL.XNull(.Rows(0)("DriverName"))
                TxtArrivalDateAtFactory.Text = AgL.XNull(.Rows(0)("ArrivalDateAtFactory"))
                TxtVehicleReturnDate.Text = AgL.XNull(.Rows(0)("VehicleReturnDate"))
                TxtProofSubmissionDate.Text = AgL.XNull(.Rows(0)("ShipmentReleaseDate"))
                TxtBankAdviceNo.Text = AgL.XNull(.Rows(0)("BankAdviceNo"))
                TxtBankAdviceDate.Text = AgL.XNull(.Rows(0)("BankAdviceDate"))
                TxtExchangeRate.Text = AgL.XNull(.Rows(0)("ExchangeRate"))
                TxtBillOfLadingNo.Text = AgL.XNull(.Rows(0)("BillOfLadingNo"))
                TxtBillOfLadingDate.Text = AgL.XNull(.Rows(0)("BillOfLadingDate"))
                TxtShipmentOnBoardDate.Text = AgL.XNull(.Rows(0)("ShipmentOnBoardDate"))
                TxtVesselName.Text = AgL.XNull(.Rows(0)("VesselName"))

                AgCalcGrid1.MoveRec_TransFooter(SearchCode)

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from IE_ShipmentDoc where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from IE_ShipmentDoc_Log where UID = '" & SearchCode & "' Order By Sr"
                End If
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1Document, I) = AgL.XNull(.Rows(I)("Document"))
                            Dgl1.Item(Col1DocumentNo, I).Value = AgL.XNull(.Rows(I)("DocumentNo"))
                        Next I
                    End If
                End With


                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from IE_ShipmentBOE where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from IE_ShipmentBOE_Log where UID = '" & SearchCode & "' Order By Sr"
                End If
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl2.RowCount = 1
                    Dgl2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl2.Rows.Add()
                            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                            Dgl2.Item(Col2BOE, I).Value = AgL.XNull(.Rows(I)("BOE"))
                            Dgl2.Item(Col2FCValue, I).Value = AgL.XNull(.Rows(I)("FCValue"))
                            Dgl2.Item(Col2INRValue, I).Value = AgL.XNull(.Rows(I)("INRValue"))
                            Dgl2.Item(Col2Term, I).Value = AgL.XNull(.Rows(I)("Term"))
                            Dgl2.Item(Col2DueDate, I).Value = AgL.XNull(.Rows(I)("DueDate"))
                        Next I
                    End If
                End With

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from IE_ShipmentItem where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from IE_ShipmentItem_Log where UID = '" & SearchCode & "' Order By Sr"
                End If
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl3.RowCount = 1
                    Dgl3.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl3.Rows.Add()
                            Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count - 1
                            Dgl3.AgSelectedValue(Col3Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl3.Item(Col3ItemDescription, I).Value = AgL.XNull(.Rows(I)("ItemDescription"))
                            Dgl3.Item(Col3Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl3.Item(Col3Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl3.Item(Col3Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl3.Item(Col3Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl3.Item(Col3ContainerNo, I).Value = AgL.XNull(.Rows(I)("ContainerNo"))
                            Dgl3.Item(Col3KindsOfPackages, I).Value = AgL.XNull(.Rows(I)("KindsOfPackages"))
                            Call AgCalcGrid1.MoveRec_TransLine(mSearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        Topctrl1.ChangeAgGridState(Dgl2, False)
        Topctrl1.ChangeAgGridState(Dgl3, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtPurchaseOrder.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    IniGrid()

                Case TxtPurchaseOrder.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtPurchaseOrderReferenceNo.Text = AgL.XNull(DrTemp(0)("ReferenceNo"))
                        End If
                    Else
                        TxtPurchaseOrderReferenceNo.Text = ""
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        TxtPurchaseOrder.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.PurchaseOrder
        TxtPortOfDicharge.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Port
        TxtPortOfDispatch.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Port
        TxtCHA.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.CHA
        TxtTransporter.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Transporter
        Dgl1.AgHelpDataSet(Col1Document, , TabControl2.Top + TpShipmentDetail1.Top, TabControl2.Left + TpShipmentDetail1.Left) = HelpDataSet.Document
        Dgl1.AgHelpDataSet(Col1DocumentNo, 1, TabControl2.Top + TpShipmentDetail1.Top, TabControl2.Left + TpShipmentDetail1.Left) = HelpDataSet.DocumentNo
        Dgl3.AgHelpDataSet(Col3Item, 1, TabControl2.Top + TpShipmentDetail1.Top, TabControl2.Left + TpShipmentDetail1.Left) = HelpDataSet.Item
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl2.RowsAdded, Dgl3.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtV_Date, LblV_Date.Text) Then passed = False : Exit Sub
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()
    End Sub

    Private Sub FrmShipmentEntry_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
    End Sub

    Private Sub FrmShipmentEntry_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer = 0

        For I = 0 To Dgl3.RowCount - 1
            If Dgl3.Item(Col3Item, I).Value <> "" Then
                Dgl3.Item(Col3Amount, I).Value = Val(Dgl3.Item(Col3Qty, I).Value) * Val(Dgl3.Item(Col3Rate, I).Value)
            End If
        Next
        AgCalcGrid1.Calculation()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        mRowIndex = Dgl1.CurrentCell.RowIndex
        mColumnIndex = Dgl1.CurrentCell.ColumnIndex

        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1DocumentNo
                Dgl1.AgRowFilter(Dgl1.Columns(Col1DocumentNo).Index) = " Document = '" & Dgl1.AgSelectedValue(Col1Document, mRowIndex) & "'  "
        End Select
        Calculation()
    End Sub

    Protected Overridable Sub Dgl3_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl3.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl3.CurrentCell.RowIndex
            mColumnIndex = Dgl3.CurrentCell.ColumnIndex
            If Dgl3.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl3.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl3.Columns(Dgl3.CurrentCell.ColumnIndex).Name
                Case Col3Item
                    If Dgl3.Item(Col3Item, mRowIndex).Value.ToString.Trim = "" Or Dgl3.AgSelectedValue(Col3Item, mRowIndex).ToString.Trim = "" Then
                        Dgl3.Item(Col3Unit, mRowIndex).Value = ""
                    Else
                        If Dgl3.AgHelpDataSet(Col3Item) IsNot Nothing Then
                            DrTemp = Dgl3.AgHelpDataSet(Col3Item).Tables(0).Select("Code = '" & Dgl3.AgSelectedValue(Col3Item, mRowIndex) & "'")
                            Dgl3.Item(Col3Unit, mRowIndex).Value = AgL.XNull(DrTemp(0)("Unit"))
                        End If
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmShipmentEntry_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtPurchaseOrderReferenceNo.Enabled = False
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub TempShipmentEntry_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = " SELECT Po.DocID AS Code, Po.V_Type + '-' + Convert(NVARCHAR,Po.V_No), Po.ReferenceNo " & _
                " FROM PurchOrder Po "
        HelpDataSet.PurchaseOrder = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select S.Code As Code, S.Description As Port " & _
                " From SeaPort S "
        HelpDataSet.Port = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select Sg.SubCode As Code, Sg.DispName As Name " & _
                " From SubGroup Sg "
        HelpDataSet.CHA = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select Sg.SubCode As Code, Sg.DispName As Transporter " & _
                " From Transporter T " & _
                " LEFT JOIN SubGroup Sg On T.SubCode = Sg.SubCode "
        HelpDataSet.Transporter = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT D.Code AS Code, D.Description AS Document  " & _
                " FROM IE_Doc D "
        HelpDataSet.Document = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT D.DocumentNo AS Code, D.DocumentNo, D.Document  " & _
                " FROM IE_DocEntry  D " & _
                " WHERE D.DocumentNo IS NOT NULL  "
        HelpDataSet.DocumentNo = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select I.Code As Code, I.Description As Item, I.Unit " & _
                " From Item I "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)
    End Sub
End Class
