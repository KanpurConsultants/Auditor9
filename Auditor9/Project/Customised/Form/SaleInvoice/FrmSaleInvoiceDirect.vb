Imports CrystalDecisions.CrystalReports.Engine
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports System.Xml
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq
Imports System.Net

Public Class FrmSaleInvoiceDirect
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "Srl."
    Public Const Col1ImportStatus As String = "Import Status"
    Public Const Col1V_Nature As String = "V_Nature"
    Public Const Col1Barcode As String = "Barcode"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1ItemCode As String = "Item Code"
    Public Col1ItemType As String = AgL.PubCaptionItemType
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1ItemState As String = "Item State"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Public Const Col1BaleNo As String = "Bale No"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1DocQty As String = "Doc Qty"
    Public Const Col1FreeQty As String = "Free Qty"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1Pcs As String = "Pcs"
    Public Const Col1UnitMultiplier As String = "Unit Multiplier"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1DealUnit As String = "Deal Unit"
    Public Const Col1DealUnitDecimalPlaces As String = "Deal Decimal Places"
    Public Const Col1MasterSaleRate As String = "Master Rate"
    Public Const Col1RateDiscountPer As String = "Rate Disc. %"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1DiscountPer As String = "Disc. %"
    Public Const Col1DiscountAmount As String = "Disc. Amt"
    Public Const Col1AdditionalDiscountPer As String = "Add. Disc. %"
    Public Const Col1AdditionalDiscountAmount As String = "Add. Disc. Amt"
    Public Const Col1AdditionPer As String = "Addition %"
    Public Const Col1AdditionAmount As String = "Addition Amt"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1Godown As String = "Godown"
    Public Const Col1ReferenceNo As String = "Reference No"
    Public Const Col1ReferenceDate As String = "Reference Date"
    Public Const Col1ReferenceDocId As String = "Reference DocID"
    Public Const Col1ReferenceDocIdTSr As String = "Reference TSr"
    Public Const Col1ReferenceDocIdSr As String = "Reference Sr"
    Public Const Col1PurchaseRate As String = "Purchase Rate"
    Public Const Col1SaleInvoice As String = "Sale Invoice DocID"
    Public Const Col1SaleInvoiceSr As String = "Sale Invoice Sr"
    Public Const Col1DefaultDiscountPer As String = "Default Discount %"
    Public Const Col1DefaultAdditionalDiscountPer As String = "Default Additional Discount %"
    Public Const Col1DefaultAdditionPer As String = "Default Addition %"
    Public Const Col1DiscountCalculationPattern As String = "Discount Calculation Pattern"
    Public Const Col1AdditionalDiscountCalculationPattern As String = "Additional Discount Calculation Pattern"
    Public Const Col1AdditionCalculationPattern As String = "Additional Calculation Pattern"
    Public Const Col1PersonalDiscountPer As String = "Personal Discount %"
    Public Const Col1PersonalAdditionalDiscountPer As String = "Personal Additional Discount %"
    Public Const Col1PersonalAdditionPer As String = "Personal Addition %"
    Public Const Col1StockSr As String = "Stock Sr"
    Public Const Col1IsRecordLocked As String = "Is Record Locked"
    Public Const Col1BarcodeType As String = "Barcode Type"
    Public Const Col1MaintainStockYn As String = "Maintain Stock Yn"
    Public Const Col1Progress As String = "Progress"
    Public Const Col1CurrentStock As String = "Current Stock"
    Public Const Col1SalesAc As String = "SalesAc"

    '========================================================================


    Public Const Col1PurchaseDiscountPer As String = "Purch Disc. %"
    Public Const Col1PurchaseDiscountAmount As String = "Purch Disc. Amt"
    Public Const Col1PurchaseAdditionalDiscountPer As String = "Purch Add. Disc. %"
    Public Const Col1PurchaseAdditionalDiscountAmount As String = "Purch Add. Disc. Amt"
    Public Const Col1PurityPer As String = "Purity %"



    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public WithEvents Dgl3 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"


    Dim rowRateType As Integer = 0
    Dim rowPartyDocNo As Integer = 1
    Dim rowPartyDocDate As Integer = 2
    Dim rowDeliveryDate As Integer = 3
    Dim rowMinDeliveryDate As Integer = 4
    Dim rowGodown As Integer = 5
    Dim rowReferenceNo As Integer = 6
    Dim rowShipToParty As Integer = 7
    Dim rowSalesTaxNo As Integer = 8
    Dim rowAadharNo As Integer = 9
    Dim rowStructure As Integer = 10

    Public Const hcRateType As String = "Rate Type"
    Public Const hcPartyDocNo As String = "Party Doc.No."
    Public Const hcPartyDocDate As String = "Party Doc.Date"
    Public Const hcDeliveryDate As String = "Delivery Date"
    Public Const hcMinDeliveryDate As String = "Min.Delivery Date"
    Public Const hcReferenceNo As String = "Reference No."
    Public Const hcGodown As String = "Godown"
    Public Const hcShipToParty As String = "Ship To Party"
    Public Const HcSalesTaxNo As String = "GST No"
    Public Const HcAadharNo As String = "Aadhar No"
    Public Const hcStructure As String = "Structure"

    Dim rowCreditDays As Integer = 0
    Dim rowAgent As Integer = 1
    Dim rowTransporter As Integer = 2
    Dim rowResponsiblePerson As Integer = 3
    Dim rowSalesRepresentative As Integer = 4
    Dim rowRemarks1 As Integer = 5
    Dim rowRemarks2 As Integer = 6
    Dim rowTags As Integer = 7
    Dim rowRemarks As Integer = 8
    Dim rowTermsAndConditions As Integer = 9
    Dim rowCustomerInvoiceCount As Integer = 10
    Dim rowEInvoiceIRN As Integer = 11
    Dim rowEInvoiceACKNo As Integer = 12
    Dim rowEInvoiceACKDate As Integer = 13



    Dim mPersonalDiscountPer As Double
    Dim mPersonalAdditionalDiscountPer As Double
    Dim mPersonalAdditionalPer As Double


    Public Const hcCreditDays As String = "Credit Days"
    Public Const hcAgent As String = "Agent"
    Public Const hcTransporter As String = "Transporter"
    Public Const hcResponsiblePerson As String = "Responsible Person"
    Public Const hcSalesRepresentative As String = "Sales Rep."
    Public Const hcRemarks1 As String = "Remarks 1"
    Public Const hcRemarks2 As String = "Remarks 2"
    Public Const hcTags As String = "Tags"
    Public Const hcRemarks As String = "Remarks"
    Public Const hcTermsAndConditions As String = "Terms & Conditions"
    Public Const hcCustomerInvoiceCount As String = "Customer Invoice Count"
    Public Const hcEInvoiceIRN As String = "E-Invoice IRN"
    Public Const hcEInvoiceACKNo As String = "E-Invoice ACK No"
    Public Const hcEInvoiceACKDate As String = "E-Invoice ACK Date"



    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay

    Dim mPrevRowIndex As Integer = 0
    Protected WithEvents BtnHeaderDetail As Button
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Dim Dgl As New AgControls.AgDataGrid

    Dim DtV_TypeTrnSettings As DataTable
    Public WithEvents TxtBarcode As AgControls.AgTextBox
    Public WithEvents LblBarcode As Label
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuEditSave As ToolStripMenuItem
    Friend WithEvents MnuGenerateEWayBill As ToolStripMenuItem
    Friend WithEvents MnuReconcileBill As ToolStripMenuItem
    Friend WithEvents MnuEMail As ToolStripMenuItem
    Friend WithEvents MnuSendSms As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Public mDimensionSrl As Integer
    Friend WithEvents MnuPrintQACopy As ToolStripMenuItem
    Public WithEvents Pnl2 As Panel
    Protected WithEvents BtnAttachments As Button
    Public WithEvents Pnl3 As Panel
    Public Shared mFlag_Import As Boolean = False

    Dim DtItemTypeSettingsAll As DataTable

    Dim UserMovedOverItemGroup As Boolean
    Public WithEvents LblTotalPcs As Label
    Public WithEvents LblTotalPcsText As Label
    Friend WithEvents MnuReport As ToolStripMenuItem
    Friend WithEvents MnuReferenceEntries As ToolStripMenuItem
    Friend WithEvents MnuHistory As ToolStripMenuItem
    Friend WithEvents MnuPrintBulk As ToolStripMenuItem
    Public WithEvents LblCurrentStock As Label
    Public WithEvents Label7 As Label
    Public WithEvents AgTextBox1 As AgControls.AgTextBox
    Public WithEvents Label8 As Label
    Friend WithEvents MnuShowLedgerPosting As ToolStripMenuItem
    Friend WithEvents MnuWhatsappDocument As ToolStripMenuItem
    Friend WithEvents MnuGenerateEBill As ToolStripMenuItem
    Friend WithEvents MnuSendWhatsapp As ToolStripMenuItem
    Friend WithEvents MnuSendWhatsappPDF As ToolStripMenuItem
    Dim UserMovedOverItemCategory As Boolean


    Public Function FItemTypeSettings(ItemType As String) As DataRow
        Dim DrItemTypeSetting As DataRow()

        DrItemTypeSetting = DtItemTypeSettingsAll.Select("ItemType='" & ItemType & "' And Div_Code='" & TxtDivision.Tag & "'")
        If DrItemTypeSetting.Length <= 0 Then
            DrItemTypeSetting = DtItemTypeSettingsAll.Select("ItemType='" & ItemType & "'")
        End If

        FItemTypeSettings = DrItemTypeSetting(0)
    End Function

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from SaleInvoiceSetting H  With (NoLock) Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') Or H.V_Type Is Null "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found")
        End If
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSaleInvoiceDirect))
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtSaleToParty = New AgControls.AgTextBox()
        Me.LblBuyer = New System.Windows.Forms.Label()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LblCurrentStock = New System.Windows.Forms.Label()
        Me.LblTotalPcs = New System.Windows.Forms.Label()
        Me.LblTotalPcsText = New System.Windows.Forms.Label()
        Me.LblTotalBale = New System.Windows.Forms.Label()
        Me.LblTotalBaleText = New System.Windows.Forms.Label()
        Me.LblDealQty = New System.Windows.Forms.Label()
        Me.LblDealQtyText = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalAmount = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalAmountText = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.TxtStructure = New AgControls.AgTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.LblCurrency = New System.Windows.Forms.Label()
        Me.PnlCalcGrid = New System.Windows.Forms.Panel()
        Me.TxtCreditLimit = New AgControls.AgTextBox()
        Me.LblCreditLimit = New System.Windows.Forms.Label()
        Me.TxtCurrBal = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblNature = New System.Windows.Forms.Label()
        Me.TxtNature = New AgControls.AgTextBox()
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtBillToParty = New AgControls.AgTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox()
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button()
        Me.LblPurchaseRate = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LblHelp = New System.Windows.Forms.Label()
        Me.BtnHeaderDetail = New System.Windows.Forms.Button()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEditSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuGenerateEWayBill = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReconcileBill = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEMail = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSendSms = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSendWhatsapp = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuWhatsappDocument = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrintQACopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrintBulk = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReferenceEntries = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuShowLedgerPosting = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuGenerateEBill = New System.Windows.Forms.ToolStripMenuItem()
        Me.TxtBarcode = New AgControls.AgTextBox()
        Me.LblBarcode = New System.Windows.Forms.Label()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.Pnl3 = New System.Windows.Forms.Panel()
        Me.AgTextBox1 = New AgControls.AgTextBox()
        Me.MnuSendWhatsappPDF = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.PnlTotals.SuspendLayout()
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(787, 690)
        Me.GroupBox2.Size = New System.Drawing.Size(148, 40)
        Me.GroupBox2.Visible = False
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
        Me.GBoxMoveToLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(601, 578)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(118, 39)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 18)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(112, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxApprove.Location = New System.Drawing.Point(841, 577)
        Me.GBoxApprove.Size = New System.Drawing.Size(137, 40)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
        Me.TxtApproveBy.Size = New System.Drawing.Size(79, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(108, 18)
        Me.CmdDiscard.Size = New System.Drawing.Size(26, 19)
        '
        'CmdApprove
        '
        Me.CmdApprove.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(168, 635)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrpUP.Location = New System.Drawing.Point(481, 576)
        Me.GrpUP.Size = New System.Drawing.Size(117, 42)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Location = New System.Drawing.Point(3, 21)
        Me.TxtEntryBy.Size = New System.Drawing.Size(111, 18)
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 642)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxDivision.Location = New System.Drawing.Point(721, 578)
        Me.GBoxDivision.Size = New System.Drawing.Size(119, 39)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 18)
        Me.TxtDivision.Size = New System.Drawing.Size(113, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtDocId
        '
        Me.TxtDocId.AgSelectedValue = ""
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblV_No
        '
        Me.LblV_No.Location = New System.Drawing.Point(276, 267)
        Me.LblV_No.Size = New System.Drawing.Size(71, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Invoice No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(384, 266)
        Me.TxtV_No.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(115, 38)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Date.Location = New System.Drawing.Point(11, 35)
        Me.LblV_Date.Size = New System.Drawing.Size(100, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Invoice Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(391, 18)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(131, 34)
        Me.TxtV_Date.Size = New System.Drawing.Size(100, 17)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(237, 16)
        Me.LblV_Type.Size = New System.Drawing.Size(101, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Invoice Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Type.Location = New System.Drawing.Point(345, 14)
        Me.TxtV_Type.Size = New System.Drawing.Size(173, 17)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(179, 18)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(11, 15)
        Me.LblSite_Code.Size = New System.Drawing.Size(105, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(131, 14)
        Me.TxtSite_Code.Size = New System.Drawing.Size(100, 17)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(336, 267)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 17)
        Me.TabControl1.Size = New System.Drawing.Size(992, 131)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Pnl2)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtBillToParty)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.LblNature)
        Me.TP1.Controls.Add(Me.TxtNature)
        Me.TP1.Controls.Add(Me.Panel3)
        Me.TP1.Controls.Add(Me.Panel2)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtSaleToParty)
        Me.TP1.Controls.Add(Me.LblBuyer)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 105)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBuyer, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSaleToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel2, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.Pnl2, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(327, 38)
        Me.Label1.TabIndex = 737
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.Location = New System.Drawing.Point(345, 34)
        Me.TxtReferenceNo.Size = New System.Drawing.Size(173, 17)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.Location = New System.Drawing.Point(237, 34)
        Me.LblReferenceNo.Size = New System.Drawing.Size(90, 16)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Invoice No."
        '
        'Dgl1
        '
        Me.Dgl1.AgAllowFind = True
        Me.Dgl1.AgLastColumn = -1
        Me.Dgl1.AgMandatoryColumn = 0
        Me.Dgl1.AgReadOnlyColumnColor = System.Drawing.Color.Ivory
        Me.Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.AgSkipReadOnlyColumns = False
        Me.Dgl1.CancelEditingControlValidating = False
        Me.Dgl1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(115, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 694
        Me.Label4.Text = "Ä"
        '
        'TxtSaleToParty
        '
        Me.TxtSaleToParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleToParty.AgLastValueTag = Nothing
        Me.TxtSaleToParty.AgLastValueText = Nothing
        Me.TxtSaleToParty.AgMandatory = True
        Me.TxtSaleToParty.AgMasterHelp = False
        Me.TxtSaleToParty.AgNumberLeftPlaces = 8
        Me.TxtSaleToParty.AgNumberNegetiveAllow = False
        Me.TxtSaleToParty.AgNumberRightPlaces = 2
        Me.TxtSaleToParty.AgPickFromLastValue = False
        Me.TxtSaleToParty.AgRowFilter = ""
        Me.TxtSaleToParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleToParty.AgSelectedValue = Nothing
        Me.TxtSaleToParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleToParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleToParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleToParty.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleToParty.Location = New System.Drawing.Point(131, 54)
        Me.TxtSaleToParty.MaxLength = 0
        Me.TxtSaleToParty.Name = "TxtSaleToParty"
        Me.TxtSaleToParty.Size = New System.Drawing.Size(358, 17)
        Me.TxtSaleToParty.TabIndex = 4
        '
        'LblBuyer
        '
        Me.LblBuyer.AutoSize = True
        Me.LblBuyer.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyer.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyer.Location = New System.Drawing.Point(11, 56)
        Me.LblBuyer.Name = "LblBuyer"
        Me.LblBuyer.Size = New System.Drawing.Size(47, 16)
        Me.LblBuyer.TabIndex = 693
        Me.LblBuyer.Text = "Party"
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.Label8)
        Me.PnlTotals.Controls.Add(Me.LblCurrentStock)
        Me.PnlTotals.Controls.Add(Me.LblTotalPcs)
        Me.PnlTotals.Controls.Add(Me.LblTotalPcsText)
        Me.PnlTotals.Controls.Add(Me.LblTotalBale)
        Me.PnlTotals.Controls.Add(Me.LblTotalBaleText)
        Me.PnlTotals.Controls.Add(Me.LblDealQty)
        Me.PnlTotals.Controls.Add(Me.LblDealQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(4, 386)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 694
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Maroon
        Me.Label8.Location = New System.Drawing.Point(634, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 16)
        Me.Label8.TabIndex = 721
        Me.Label8.Text = "Curr.Stock"
        '
        'LblCurrentStock
        '
        Me.LblCurrentStock.AutoSize = True
        Me.LblCurrentStock.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStock.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblCurrentStock.Location = New System.Drawing.Point(715, 3)
        Me.LblCurrentStock.Name = "LblCurrentStock"
        Me.LblCurrentStock.Size = New System.Drawing.Size(12, 16)
        Me.LblCurrentStock.TabIndex = 720
        Me.LblCurrentStock.Text = "."
        Me.LblCurrentStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalPcs
        '
        Me.LblTotalPcs.AutoSize = True
        Me.LblTotalPcs.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPcs.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalPcs.Location = New System.Drawing.Point(423, 4)
        Me.LblTotalPcs.Name = "LblTotalPcs"
        Me.LblTotalPcs.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalPcs.TabIndex = 718
        Me.LblTotalPcs.Text = "."
        Me.LblTotalPcs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalPcsText
        '
        Me.LblTotalPcsText.AutoSize = True
        Me.LblTotalPcsText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPcsText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalPcsText.Location = New System.Drawing.Point(349, 3)
        Me.LblTotalPcsText.Name = "LblTotalPcsText"
        Me.LblTotalPcsText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalPcsText.TabIndex = 717
        Me.LblTotalPcsText.Text = "Total Pcs :"
        '
        'LblTotalBale
        '
        Me.LblTotalBale.AutoSize = True
        Me.LblTotalBale.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBale.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBale.Location = New System.Drawing.Point(585, 4)
        Me.LblTotalBale.Name = "LblTotalBale"
        Me.LblTotalBale.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBale.TabIndex = 716
        Me.LblTotalBale.Text = "."
        Me.LblTotalBale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalBaleText
        '
        Me.LblTotalBaleText.AutoSize = True
        Me.LblTotalBaleText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBaleText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalBaleText.Location = New System.Drawing.Point(499, 3)
        Me.LblTotalBaleText.Name = "LblTotalBaleText"
        Me.LblTotalBaleText.Size = New System.Drawing.Size(86, 16)
        Me.LblTotalBaleText.TabIndex = 715
        Me.LblTotalBaleText.Text = "Total Bales :"
        '
        'LblDealQty
        '
        Me.LblDealQty.AutoSize = True
        Me.LblDealQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblDealQty.Location = New System.Drawing.Point(281, 3)
        Me.LblDealQty.Name = "LblDealQty"
        Me.LblDealQty.Size = New System.Drawing.Size(12, 16)
        Me.LblDealQty.TabIndex = 666
        Me.LblDealQty.Text = "."
        Me.LblDealQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblDealQtyText
        '
        Me.LblDealQtyText.AutoSize = True
        Me.LblDealQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblDealQtyText.Location = New System.Drawing.Point(170, 3)
        Me.LblDealQtyText.Name = "LblDealQtyText"
        Me.LblDealQtyText.Size = New System.Drawing.Size(105, 16)
        Me.LblDealQtyText.TabIndex = 665
        Me.LblDealQtyText.Text = "Total Deal Qty :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(97, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(868, 4)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(12, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(771, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(762, 154)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 16)
        Me.Label7.TabIndex = 3020
        Me.Label7.Text = "Bal. Limit"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(4, 176)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 209)
        Me.Pnl1.TabIndex = 11
        '
        'TxtStructure
        '
        Me.TxtStructure.AgAllowUserToEnableMasterHelp = False
        Me.TxtStructure.AgLastValueTag = Nothing
        Me.TxtStructure.AgLastValueText = Nothing
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
        Me.TxtStructure.Location = New System.Drawing.Point(641, 221)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(60, 18)
        Me.TxtStructure.TabIndex = 15
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(569, 222)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 715
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(332, 219)
        Me.LblCurrency.Name = "LblCurrency"
        Me.LblCurrency.Size = New System.Drawing.Size(60, 16)
        Me.LblCurrency.TabIndex = 735
        Me.LblCurrency.Text = "Currency"
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCalcGrid.Location = New System.Drawing.Point(651, 413)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(327, 164)
        Me.PnlCalcGrid.TabIndex = 15
        '
        'TxtCreditLimit
        '
        Me.TxtCreditLimit.AgAllowUserToEnableMasterHelp = False
        Me.TxtCreditLimit.AgLastValueTag = Nothing
        Me.TxtCreditLimit.AgLastValueText = Nothing
        Me.TxtCreditLimit.AgMandatory = False
        Me.TxtCreditLimit.AgMasterHelp = False
        Me.TxtCreditLimit.AgNumberLeftPlaces = 8
        Me.TxtCreditLimit.AgNumberNegetiveAllow = False
        Me.TxtCreditLimit.AgNumberRightPlaces = 0
        Me.TxtCreditLimit.AgPickFromLastValue = False
        Me.TxtCreditLimit.AgRowFilter = ""
        Me.TxtCreditLimit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCreditLimit.AgSelectedValue = Nothing
        Me.TxtCreditLimit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCreditLimit.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCreditLimit.BackColor = System.Drawing.Color.White
        Me.TxtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCreditLimit.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.TxtCreditLimit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditLimit.Location = New System.Drawing.Point(620, 154)
        Me.TxtCreditLimit.MaxLength = 20
        Me.TxtCreditLimit.Name = "TxtCreditLimit"
        Me.TxtCreditLimit.ReadOnly = True
        Me.TxtCreditLimit.Size = New System.Drawing.Size(128, 16)
        Me.TxtCreditLimit.TabIndex = 8
        Me.TxtCreditLimit.TabStop = False
        Me.TxtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCreditLimit.UseWaitCursor = True
        '
        'LblCreditLimit
        '
        Me.LblCreditLimit.AutoSize = True
        Me.LblCreditLimit.BackColor = System.Drawing.Color.Transparent
        Me.LblCreditLimit.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditLimit.Location = New System.Drawing.Point(523, 154)
        Me.LblCreditLimit.Name = "LblCreditLimit"
        Me.LblCreditLimit.Size = New System.Drawing.Size(92, 16)
        Me.LblCreditLimit.TabIndex = 741
        Me.LblCreditLimit.Text = "Credit Limit"
        '
        'TxtCurrBal
        '
        Me.TxtCurrBal.AgAllowUserToEnableMasterHelp = False
        Me.TxtCurrBal.AgLastValueTag = Nothing
        Me.TxtCurrBal.AgLastValueText = Nothing
        Me.TxtCurrBal.AgMandatory = False
        Me.TxtCurrBal.AgMasterHelp = False
        Me.TxtCurrBal.AgNumberLeftPlaces = 8
        Me.TxtCurrBal.AgNumberNegetiveAllow = True
        Me.TxtCurrBal.AgNumberRightPlaces = 2
        Me.TxtCurrBal.AgPickFromLastValue = False
        Me.TxtCurrBal.AgRowFilter = ""
        Me.TxtCurrBal.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCurrBal.AgSelectedValue = Nothing
        Me.TxtCurrBal.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCurrBal.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCurrBal.BackColor = System.Drawing.Color.White
        Me.TxtCurrBal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCurrBal.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.TxtCurrBal.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCurrBal.Location = New System.Drawing.Point(400, 154)
        Me.TxtCurrBal.MaxLength = 20
        Me.TxtCurrBal.Name = "TxtCurrBal"
        Me.TxtCurrBal.ReadOnly = True
        Me.TxtCurrBal.Size = New System.Drawing.Size(120, 16)
        Me.TxtCurrBal.TabIndex = 7
        Me.TxtCurrBal.TabStop = False
        Me.TxtCurrBal.Text = "9999999999.99"
        Me.TxtCurrBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCurrBal.UseWaitCursor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(290, 154)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 16)
        Me.Label3.TabIndex = 743
        Me.Label3.Text = "Curr. Balance"
        '
        'LblNature
        '
        Me.LblNature.AutoSize = True
        Me.LblNature.BackColor = System.Drawing.Color.Transparent
        Me.LblNature.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNature.Location = New System.Drawing.Point(622, 163)
        Me.LblNature.Name = "LblNature"
        Me.LblNature.Size = New System.Drawing.Size(46, 16)
        Me.LblNature.TabIndex = 745
        Me.LblNature.Text = "Nature"
        Me.LblNature.Visible = False
        '
        'TxtNature
        '
        Me.TxtNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtNature.AgLastValueTag = Nothing
        Me.TxtNature.AgLastValueText = Nothing
        Me.TxtNature.AgMandatory = False
        Me.TxtNature.AgMasterHelp = False
        Me.TxtNature.AgNumberLeftPlaces = 8
        Me.TxtNature.AgNumberNegetiveAllow = False
        Me.TxtNature.AgNumberRightPlaces = 2
        Me.TxtNature.AgPickFromLastValue = False
        Me.TxtNature.AgRowFilter = ""
        Me.TxtNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNature.AgSelectedValue = Nothing
        Me.TxtNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNature.Location = New System.Drawing.Point(736, 162)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(95, 18)
        Me.TxtNature.TabIndex = 10
        Me.TxtNature.Visible = False
        '
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.BackColor = System.Drawing.Color.White
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.Black
        Me.BtnFillPartyDetail.Image = Global.Customised.My.Resources.Resources._41104_200
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(493, 54)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(25, 16)
        Me.BtnFillPartyDetail.TabIndex = 5
        Me.BtnFillPartyDetail.TabStop = False
        Me.BtnFillPartyDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPartyDetail.UseVisualStyleBackColor = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.PnlCustomGrid.Location = New System.Drawing.Point(474, 439)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(19, 22)
        Me.PnlCustomGrid.TabIndex = 3
        '
        'TxtCustomFields
        '
        Me.TxtCustomFields.AgAllowUserToEnableMasterHelp = False
        Me.TxtCustomFields.AgLastValueTag = Nothing
        Me.TxtCustomFields.AgLastValueText = Nothing
        Me.TxtCustomFields.AgMandatory = False
        Me.TxtCustomFields.AgMasterHelp = False
        Me.TxtCustomFields.AgNumberLeftPlaces = 8
        Me.TxtCustomFields.AgNumberNegetiveAllow = False
        Me.TxtCustomFields.AgNumberRightPlaces = 2
        Me.TxtCustomFields.AgPickFromLastValue = False
        Me.TxtCustomFields.AgRowFilter = ""
        Me.TxtCustomFields.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCustomFields.AgSelectedValue = Nothing
        Me.TxtCustomFields.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCustomFields.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCustomFields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCustomFields.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomFields.Location = New System.Drawing.Point(486, 636)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1011
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(115, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 3003
        Me.Label5.Text = "Ä"
        '
        'TxtBillToParty
        '
        Me.TxtBillToParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtBillToParty.AgLastValueTag = Nothing
        Me.TxtBillToParty.AgLastValueText = Nothing
        Me.TxtBillToParty.AgMandatory = True
        Me.TxtBillToParty.AgMasterHelp = False
        Me.TxtBillToParty.AgNumberLeftPlaces = 8
        Me.TxtBillToParty.AgNumberNegetiveAllow = False
        Me.TxtBillToParty.AgNumberRightPlaces = 2
        Me.TxtBillToParty.AgPickFromLastValue = False
        Me.TxtBillToParty.AgRowFilter = ""
        Me.TxtBillToParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillToParty.AgSelectedValue = Nothing
        Me.TxtBillToParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillToParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillToParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillToParty.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillToParty.Location = New System.Drawing.Point(131, 74)
        Me.TxtBillToParty.MaxLength = 0
        Me.TxtBillToParty.Name = "TxtBillToParty"
        Me.TxtBillToParty.Size = New System.Drawing.Size(387, 17)
        Me.TxtBillToParty.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 16)
        Me.Label6.TabIndex = 3002
        Me.Label6.Text = "Post to A/c"
        '
        'GBoxImportFromExcel
        '
        Me.GBoxImportFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxImportFromExcel.BackColor = System.Drawing.Color.Transparent
        Me.GBoxImportFromExcel.Controls.Add(Me.BtnImprtFromExcel)
        Me.GBoxImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxImportFromExcel.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxImportFromExcel.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(678, 662)
        Me.GBoxImportFromExcel.Name = "GBoxImportFromExcel"
        Me.GBoxImportFromExcel.Size = New System.Drawing.Size(99, 47)
        Me.GBoxImportFromExcel.TabIndex = 1013
        Me.GBoxImportFromExcel.TabStop = False
        Me.GBoxImportFromExcel.Tag = "UP"
        Me.GBoxImportFromExcel.Text = "Import From Excel"
        Me.GBoxImportFromExcel.Visible = False
        '
        'BtnImprtFromExcel
        '
        Me.BtnImprtFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromExcel.Image = CType(resources.GetObject("BtnImprtFromExcel.Image"), System.Drawing.Image)
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(58, 9)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnImprtFromExcel.TabIndex = 669
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'LblPurchaseRate
        '
        Me.LblPurchaseRate.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.LblPurchaseRate.AutoSize = True
        Me.LblPurchaseRate.Location = New System.Drawing.Point(468, 418)
        Me.LblPurchaseRate.Name = "LblPurchaseRate"
        Me.LblPurchaseRate.Size = New System.Drawing.Size(39, 13)
        Me.LblPurchaseRate.TabIndex = 1014
        Me.LblPurchaseRate.Text = "Label7"
        Me.LblPurchaseRate.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(4, 119)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(973, 227)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(4, 119)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 227)
        Me.Panel3.TabIndex = 11
        '
        'LblHelp
        '
        Me.LblHelp.AutoSize = True
        Me.LblHelp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHelp.Location = New System.Drawing.Point(480, 663)
        Me.LblHelp.Name = "LblHelp"
        Me.LblHelp.Size = New System.Drawing.Size(122, 39)
        Me.LblHelp.TabIndex = 3006
        Me.LblHelp.Text = "D - Direct Invoice" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "S - For Stock" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "R - Return" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.LblHelp.Visible = False
        '
        'BtnHeaderDetail
        '
        Me.BtnHeaderDetail.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnHeaderDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnHeaderDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHeaderDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnHeaderDetail.Location = New System.Drawing.Point(498, 413)
        Me.BtnHeaderDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnHeaderDetail.Name = "BtnHeaderDetail"
        Me.BtnHeaderDetail.Size = New System.Drawing.Size(134, 23)
        Me.BtnHeaderDetail.TabIndex = 14
        Me.BtnHeaderDetail.TabStop = False
        Me.BtnHeaderDetail.Text = "Transport Detail"
        Me.BtnHeaderDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnHeaderDetail.UseVisualStyleBackColor = True
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuEditSave, Me.MnuGenerateEWayBill, Me.MnuReconcileBill, Me.MnuEMail, Me.MnuSendSms, Me.MnuSendWhatsapp, Me.MnuSendWhatsappPDF, Me.MnuWhatsappDocument, Me.MnuPrintQACopy, Me.MnuPrintBulk, Me.MnuReferenceEntries, Me.MnuHistory, Me.MnuShowLedgerPosting, Me.MnuReport, Me.MnuGenerateEBill})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(187, 422)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(186, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(186, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(186, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuEditSave
        '
        Me.MnuEditSave.Name = "MnuEditSave"
        Me.MnuEditSave.Size = New System.Drawing.Size(186, 22)
        Me.MnuEditSave.Text = "Edit & Save"
        '
        'MnuGenerateEWayBill
        '
        Me.MnuGenerateEWayBill.Name = "MnuGenerateEWayBill"
        Me.MnuGenerateEWayBill.Size = New System.Drawing.Size(186, 22)
        Me.MnuGenerateEWayBill.Text = "Generate EWay Bill"
        '
        'MnuReconcileBill
        '
        Me.MnuReconcileBill.Name = "MnuReconcileBill"
        Me.MnuReconcileBill.Size = New System.Drawing.Size(186, 22)
        Me.MnuReconcileBill.Text = "Reconcile Document"
        '
        'MnuEMail
        '
        Me.MnuEMail.Name = "MnuEMail"
        Me.MnuEMail.Size = New System.Drawing.Size(186, 22)
        Me.MnuEMail.Text = "E-Mail Document"
        '
        'MnuSendSms
        '
        Me.MnuSendSms.Name = "MnuSendSms"
        Me.MnuSendSms.Size = New System.Drawing.Size(186, 22)
        Me.MnuSendSms.Text = "Send Sms"
        '
        'MnuSendWhatsapp
        '
        Me.MnuSendWhatsapp.Name = "MnuSendWhatsapp"
        Me.MnuSendWhatsapp.Size = New System.Drawing.Size(186, 22)
        Me.MnuSendWhatsapp.Text = "Send Whatsapp"
        '
        'MnuWhatsappDocument
        '
        Me.MnuWhatsappDocument.Name = "MnuWhatsappDocument"
        Me.MnuWhatsappDocument.Size = New System.Drawing.Size(186, 22)
        Me.MnuWhatsappDocument.Text = "Whatsapp Document"
        '
        'MnuPrintQACopy
        '
        Me.MnuPrintQACopy.Name = "MnuPrintQACopy"
        Me.MnuPrintQACopy.Size = New System.Drawing.Size(186, 22)
        Me.MnuPrintQACopy.Text = "Print QA Copy"
        '
        'MnuPrintBulk
        '
        Me.MnuPrintBulk.Name = "MnuPrintBulk"
        Me.MnuPrintBulk.Size = New System.Drawing.Size(186, 22)
        Me.MnuPrintBulk.Text = "Print Bulk"
        '
        'MnuReferenceEntries
        '
        Me.MnuReferenceEntries.Name = "MnuReferenceEntries"
        Me.MnuReferenceEntries.Size = New System.Drawing.Size(186, 22)
        Me.MnuReferenceEntries.Text = "Reference Entries"
        '
        'MnuHistory
        '
        Me.MnuHistory.Name = "MnuHistory"
        Me.MnuHistory.Size = New System.Drawing.Size(186, 22)
        Me.MnuHistory.Text = "History"
        '
        'MnuShowLedgerPosting
        '
        Me.MnuShowLedgerPosting.Name = "MnuShowLedgerPosting"
        Me.MnuShowLedgerPosting.Size = New System.Drawing.Size(186, 22)
        Me.MnuShowLedgerPosting.Text = "Show Ledger Posting"
        '
        'MnuReport
        '
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(186, 22)
        Me.MnuReport.Text = "Report"
        '
        'MnuGenerateEBill
        '
        Me.MnuGenerateEBill.Name = "MnuGenerateEBill"
        Me.MnuGenerateEBill.Size = New System.Drawing.Size(186, 22)
        Me.MnuGenerateEBill.Text = "Generate E Bill"
        '
        'TxtBarcode
        '
        Me.TxtBarcode.AgAllowUserToEnableMasterHelp = False
        Me.TxtBarcode.AgLastValueTag = Nothing
        Me.TxtBarcode.AgLastValueText = Nothing
        Me.TxtBarcode.AgMandatory = False
        Me.TxtBarcode.AgMasterHelp = False
        Me.TxtBarcode.AgNumberLeftPlaces = 8
        Me.TxtBarcode.AgNumberNegetiveAllow = False
        Me.TxtBarcode.AgNumberRightPlaces = 2
        Me.TxtBarcode.AgPickFromLastValue = False
        Me.TxtBarcode.AgRowFilter = ""
        Me.TxtBarcode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBarcode.AgSelectedValue = Nothing
        Me.TxtBarcode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBarcode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBarcode.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBarcode.Location = New System.Drawing.Point(144, 153)
        Me.TxtBarcode.MaxLength = 20
        Me.TxtBarcode.Name = "TxtBarcode"
        Me.TxtBarcode.Size = New System.Drawing.Size(141, 19)
        Me.TxtBarcode.TabIndex = 10
        '
        'LblBarcode
        '
        Me.LblBarcode.AutoSize = True
        Me.LblBarcode.BackColor = System.Drawing.Color.Transparent
        Me.LblBarcode.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBarcode.Location = New System.Drawing.Point(6, 155)
        Me.LblBarcode.Name = "LblBarcode"
        Me.LblBarcode.Size = New System.Drawing.Size(136, 14)
        Me.LblBarcode.TabIndex = 3004
        Me.LblBarcode.Text = "Enter Barcode Here"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(532, 5)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(447, 97)
        Me.Pnl2.TabIndex = 3004
        '
        'BtnAttachments
        '
        Me.BtnAttachments.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAttachments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAttachments.Location = New System.Drawing.Point(498, 438)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(134, 23)
        Me.BtnAttachments.TabIndex = 3017
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Add Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'Pnl3
        '
        Me.Pnl3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Pnl3.Location = New System.Drawing.Point(4, 413)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(475, 205)
        Me.Pnl3.TabIndex = 3018
        '
        'AgTextBox1
        '
        Me.AgTextBox1.AgAllowUserToEnableMasterHelp = False
        Me.AgTextBox1.AgLastValueTag = Nothing
        Me.AgTextBox1.AgLastValueText = Nothing
        Me.AgTextBox1.AgMandatory = False
        Me.AgTextBox1.AgMasterHelp = False
        Me.AgTextBox1.AgNumberLeftPlaces = 8
        Me.AgTextBox1.AgNumberNegetiveAllow = False
        Me.AgTextBox1.AgNumberRightPlaces = 0
        Me.AgTextBox1.AgPickFromLastValue = False
        Me.AgTextBox1.AgRowFilter = ""
        Me.AgTextBox1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.AgTextBox1.AgSelectedValue = Nothing
        Me.AgTextBox1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.AgTextBox1.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.AgTextBox1.BackColor = System.Drawing.Color.White
        Me.AgTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AgTextBox1.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.AgTextBox1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AgTextBox1.Location = New System.Drawing.Point(847, 154)
        Me.AgTextBox1.MaxLength = 20
        Me.AgTextBox1.Name = "AgTextBox1"
        Me.AgTextBox1.ReadOnly = True
        Me.AgTextBox1.Size = New System.Drawing.Size(128, 16)
        Me.AgTextBox1.TabIndex = 3019
        Me.AgTextBox1.TabStop = False
        Me.AgTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.AgTextBox1.UseWaitCursor = True
        '
        'MnuSendWhatsappPDF
        '
        Me.MnuSendWhatsappPDF.Name = "MnuSendWhatsappPDF"
        Me.MnuSendWhatsappPDF.Size = New System.Drawing.Size(186, 22)
        Me.MnuSendWhatsappPDF.Text = "Send Whatsapp PDF"
        '
        'FrmSaleInvoiceDirect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.AgTextBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.TxtCreditLimit)
        Me.Controls.Add(Me.LblCreditLimit)
        Me.Controls.Add(Me.TxtCurrBal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblBarcode)
        Me.Controls.Add(Me.TxtBarcode)
        Me.Controls.Add(Me.BtnHeaderDetail)
        Me.Controls.Add(Me.LblHelp)
        Me.Controls.Add(Me.LblPurchaseRate)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmSaleInvoiceDirect"
        Me.Text = "Sale Invoice"
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.LblPurchaseRate, 0)
        Me.Controls.SetChildIndex(Me.LblHelp, 0)
        Me.Controls.SetChildIndex(Me.BtnHeaderDetail, 0)
        Me.Controls.SetChildIndex(Me.TxtBarcode, 0)
        Me.Controls.SetChildIndex(Me.LblBarcode, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtCurrBal, 0)
        Me.Controls.SetChildIndex(Me.LblCreditLimit, 0)
        Me.Controls.SetChildIndex(Me.TxtCreditLimit, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
        Me.Controls.SetChildIndex(Me.Pnl3, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.AgTextBox1, 0)
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
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents LblBuyer As System.Windows.Forms.Label
    Public WithEvents TxtSaleToParty As AgControls.AgTextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents PnlTotals As System.Windows.Forms.Panel
    Public WithEvents LblTotalQty As System.Windows.Forms.Label
    Public WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents TxtStructure As AgControls.AgTextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents LblTotalAmount As System.Windows.Forms.Label
    Public WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Public WithEvents LblDealQty As System.Windows.Forms.Label
    Public WithEvents LblDealQtyText As System.Windows.Forms.Label
    Public WithEvents LblCurrency As System.Windows.Forms.Label
    Public WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Public WithEvents TxtCreditLimit As AgControls.AgTextBox
    Public WithEvents LblCreditLimit As System.Windows.Forms.Label
    Public WithEvents LblNature As System.Windows.Forms.Label
    Public WithEvents TxtNature As AgControls.AgTextBox
    Public WithEvents TxtCurrBal As AgControls.AgTextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Public WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents LblTotalBale As System.Windows.Forms.Label
    Public WithEvents LblTotalBaleText As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents TxtBillToParty As AgControls.AgTextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Public WithEvents LblPurchaseRate As System.Windows.Forms.Label
    Public WithEvents Panel3 As System.Windows.Forms.Panel
    Public WithEvents Panel2 As System.Windows.Forms.Panel
    Public WithEvents LblHelp As System.Windows.Forms.Label

#End Region


    Public Sub PostData()
        'Dim request As HttpWebRequest = CType(WebRequest.Create("https://example.com/api/endpoint"), HttpWebRequest)
        Dim request As HttpWebRequest = CType(System.Net.WebRequest.Create("http://app.laksmartindia.com/api/v1/message/create"), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"

        ' JSON body (change this to your actual payload)
        'Dim jsonData As String = "{""username"":""Satyam Tripathi"",""password"":""KC@12345""}"
        Dim jsonData As String = "{""username"":""Satyam Tripathi"",""password"":""KC@12345"",""receiverMobileNo"":""8299399688"",""message"":""Hello""}"

        ' Write the request body
        Using streamWriter As New StreamWriter(request.GetRequestStream())
            streamWriter.Write(jsonData)
        End Using

        ' Get the response
        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        Using streamReader As New StreamReader(response.GetResponseStream())
            Dim result As String = streamReader.ReadToEnd()
            MsgBox("Response: " & result)
        End Using
    End Sub

    Public Sub UploadFileViaFTP()
        Dim ftpServer As String = "ftp://164.52.202.56/~equal2464/public_html/Sadhvi/"
        Dim localFilePath As String = "D:\11.pdf"
        Dim fileName As String = Path.GetFileName(localFilePath)

        Try
            Dim ftpUsername As String = "equal2464"
            Dim ftpPassword As String = "tActL$*$P*67"


            Dim ftpFullPath As String = ftpServer & fileName

            Dim ftpRequest As FtpWebRequest = CType(System.Net.WebRequest.Create(ftpFullPath), FtpWebRequest)
            ftpRequest.Credentials = New NetworkCredential(ftpUsername, ftpPassword)
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile
            ftpRequest.UseBinary = True
            ftpRequest.KeepAlive = False
            ftpRequest.Proxy = Nothing ' disable proxy

            Dim fileContents As Byte() = File.ReadAllBytes(localFilePath)

            Using requestStream As Stream = ftpRequest.GetRequestStream()
                requestStream.Write(fileContents, 0, fileContents.Length)
            End Using

            Using response As FtpWebResponse = CType(ftpRequest.GetResponse(), FtpWebResponse)
                MessageBox.Show($"Upload complete. Status: {response.StatusDescription}")
            End Using

        Catch ex As Exception
            'MessageBox.Show($"Error uploading file: {ex.Message}")
            MessageBox.Show("Uploading to: " & ftpServer & fileName)
            MessageBox.Show($"Error uploading file: {ex.Message}{vbCrLf}{ex.StackTrace}")
        End Try
    End Sub


    'Public Sub UploadFileViaFTP()
    '    Try
    '        ' FTP Server details
    '        'Dim ftpServer As String = "ftp://yourserver.com/"
    '        'Dim ftpServer As String = "ftp://164.52.202.56/~equal2464/public_html/Sadhvi/"
    '        Dim ftpServer As String = "ftp://ftp.equal2.in/public_html/Sadhvi/"
    '        Dim ftpUsername As String = "equal2464"
    '        Dim ftpPassword As String = "tActL$*$P*67"

    '        ' File to upload
    '        Dim localFilePath As String = "D:\11.pdf"
    '        Dim fileName As String = Path.GetFileName(localFilePath)
    '        Dim ftpFullPath As String = ftpServer & fileName

    '        ' Create FTP request
    '        'Dim ftpRequest As FtpWebRequest = CType(WebRequest.Create(ftpFullPath), FtpWebRequest)
    '        Dim ftpRequest As System.Net.FtpWebRequest = CType(System.Net.WebRequest.Create(ftpServer & fileName), FtpWebRequest)
    '        ftpRequest.Proxy = Nothing
    '        ftpRequest.Credentials = New NetworkCredential(ftpUsername, ftpPassword)
    '        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile
    '        ftpRequest.UseBinary = True
    '        ftpRequest.KeepAlive = False

    '        ' Read file and upload
    '        Dim fileContents As Byte() = File.ReadAllBytes(localFilePath)

    '        Using requestStream As Stream = ftpRequest.GetRequestStream()
    '            requestStream.Write(fileContents, 0, fileContents.Length)
    '        End Using

    '        ' Get response (optional)
    '        Using response As FtpWebResponse = CType(ftpRequest.GetResponse(), FtpWebResponse)
    '            MessageBox.Show($"Upload complete. Status: {response.StatusDescription}")
    '        End Using

    '    Catch ex As Exception
    '        MessageBox.Show($"Error uploading file: {ex.Message}")
    '    End Try
    'End Sub

    Private Sub FrmSaleInvoice_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim DtSaleInvoice As DataTable = Nothing
        Dim I As Integer = 0

        mQry = " Delete From SaleInvoiceTrnSetting Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From SaleInvoiceDimensionDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete from SaleInvoiceBarcodeLastTransactionValues where DocID='" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SaleInvoice"
        LogTableName = "SaleInvoice_Log"
        MainLineTableCsv = "SaleInvoiceDetail,SaleInvoiceDetailHelpValues,SaleInvoiceTransport,SaleInvoiceDetailSku"
        LogLineTableCsv = "SaleInvoiceDetail_Log,SaleInvoiceDetailSku_Log"


        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        If mFlag_Import = True And DTMaster.Rows.Count > 0 Then Exit Sub

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        '        mCondStr = mCondStr & " AND H.DocID IN ('D2    SI 2018      20', 'D2    SI 2018      32', 'D1    SI 2018     228', 'D1    SI 2018     538',
        ''D5    SI 2018      42', 'D5    SI 2018      50', 'D5    SI 2018      52', 'D5    SI 2018      54', 'D5    SI 2018      68', 'D5    SI 2018      70', 'D5    SI 2018      91', 'D5    SI 2018     107', 'D5    SI 2018     124', 'D1    SI 2018     249', 'D1    SI 2018     302', 'D1    SI 2018     304', 'D2   WSI 2018     824', 'D2   WSI 2018     825', 'D2   WSI 2018     826',
        ''D5    SI 2018      44', 'D5    SI 2018      56', 'D2    SI 2018     204', 'D1    SI 2018      41', 'D1    SI 2018      86', 'D1    SI 2018      92', 'D1    SI 2018     115', 'D1    SI 2018     149')
        ' "

        mQry = "Select DocID As SearchCode 
                From SaleInvoice H  With (NoLock)
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  
                Where 1 = 1  " & mCondStr & "  Order By V_Date , V_No  "




        'mQry = "Select H.DocID As SearchCode 
        '        From SaleInvoice H  With (NoLock)
        '        Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  
        '        LEFT JOIN Ledger L With (NoLock) On H.DocId = L.DocId
        '        Where L.DocId Is Null  " & mCondStr & "  Order By H.V_Date , H.V_No  "
        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Invoice_Type], Cast(strftime('%d/%m/%Y', H.V_Date) As nvarchar) AS Date, SGV.Name AS [Party], " &
                            " H.ManualRefNo AS [Manual_No], H.SalesTaxGroupParty AS [Sales_Tax_Group_Party], " &
                            " H.Remarks,  " &
                            " H.EntryBy AS [Entry_By], Cast(strftime('%d/%m/%Y', H.EntryDate) As nvarchar) AS [Entry_Date] " &
                            " FROM SaleInvoice H  With (NoLock) " &
                            " LEFT JOIN Voucher_Type Vt  With (NoLock) ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup SGV  With (NoLock) ON SGV.SubCode  = H.SaleToParty " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            If DtV_TypeSettings Is Nothing Then Exit Sub
            If DtV_TypeSettings.Rows.Count = 0 Then Exit Sub


            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ImportStatus, 50, 0, Col1ImportStatus, False, True)
            .AddAgTextColumn(Dgl1, Col1V_Nature, 70, 0, Col1V_Nature, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Barcode, 100, 0, AgL.PubCaptionBarcode, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, AgL.PubCaptionItemType, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, AgL.PubCaptionItemCategory, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, AgL.PubCaptionItemGroup, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, AgL.PubCaptionItem & " Code", False, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, AgL.PubCaptionItem, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgL.PubCaptionDimension1, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgL.PubCaptionDimension2, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, AgL.PubCaptionDimension3, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, AgL.PubCaptionDimension4, False, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 130, 0, Col1Specification, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemState, 100, 0, Col1ItemState, False, False)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, False, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 60, 255, Col1BaleNo, False, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 60, 255, Col1LotNo, False, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 8, 4, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1FreeQty, 80, 8, 4, False, Col1FreeQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, False, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MasterSaleRate, 80, 8, 2, False, Col1MasterSaleRate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1RateDiscountPer, 80, 8, 2, False, Col1RateDiscountPer, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountPer, 50, 2, 2, False, Col1DiscountPer, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountAmount, 100, 8, 2, False, Col1DiscountAmount, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountPer, 50, 2, 2, False, Col1AdditionalDiscountPer, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountAmount, 100, 8, 2, False, Col1AdditionalDiscountAmount, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionPer, 50, 2, 3, False, Col1AdditionPer, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionAmount, 100, 8, 3, False, Col1AdditionAmount, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Pcs, 80, 8, 4, False, Col1Pcs, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1UnitMultiplier, 70, 8, 4, False, Col1UnitMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DealQty, 70, 8, 3, False, Col1DealQty, False, True, True)
            .AddAgTextColumn(Dgl1, Col1DealUnit, 60, 0, Col1DealUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1DealUnitDecimalPlaces, 50, 0, Col1DealUnitDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1Godown, 100, 0, Col1Godown, AgL.IsFeatureApplicable_Godown, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceNo, 100, 0, Col1ReferenceNo, LblV_Type.Tag = Ncat.SaleReturn, False)
            .AddAgDateColumn(Dgl1, Col1ReferenceDate, 100, Col1ReferenceDate, False, False, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocId, 100, 0, Col1ReferenceDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocIdTSr, 40, 5, Col1ReferenceDocIdTSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocIdSr, 40, 5, Col1ReferenceDocIdSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1SaleInvoice, 100, 255, Col1SaleInvoice, False, False)
            .AddAgTextColumn(Dgl1, Col1SaleInvoiceSr, 40, 5, Col1SaleInvoiceSr, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1PurchaseRate, 80, 2, 3, False, Col1PurchaseRate, True, True, True)
            .AddAgTextColumn(Dgl1, Col1DefaultDiscountPer, 150, 255, Col1DefaultDiscountPer, False, False)
            .AddAgTextColumn(Dgl1, Col1DiscountCalculationPattern, 150, 255, Col1DiscountCalculationPattern, False, False)
            .AddAgTextColumn(Dgl1, Col1AdditionalDiscountCalculationPattern, 150, 255, Col1AdditionalDiscountCalculationPattern, False, False)
            .AddAgTextColumn(Dgl1, Col1AdditionCalculationPattern, 150, 255, Col1AdditionCalculationPattern, False, False)
            .AddAgTextColumn(Dgl1, Col1DefaultAdditionalDiscountPer, 150, 255, Col1DefaultAdditionalDiscountPer, False, False)
            .AddAgTextColumn(Dgl1, Col1DefaultAdditionPer, 150, 255, Col1DefaultAdditionPer, False, False)
            .AddAgTextColumn(Dgl1, Col1PersonalDiscountPer, 150, 255, Col1PersonalDiscountPer, False, False)
            .AddAgTextColumn(Dgl1, Col1PersonalAdditionalDiscountPer, 150, 255, Col1PersonalAdditionalDiscountPer, False, False)
            .AddAgTextColumn(Dgl1, Col1PersonalAdditionPer, 150, 255, Col1PersonalAdditionPer, False, False)
            .AddAgTextColumn(Dgl1, Col1PurityPer, 150, 255, Col1PurityPer, False, False)
            .AddAgTextColumn(Dgl1, Col1BarcodeType, 100, 0, Col1BarcodeType, False, False)
            .AddAgTextColumn(Dgl1, Col1Progress, 100, 255, Col1Progress, False, True)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 100, 8, 2, False, Col1CurrentStock, True, True, True)
            .AddAgTextColumn(Dgl1, Col1StockSr, 150, 255, Col1StockSr, False, False)
            .AddAgTextColumn(Dgl1, Col1IsRecordLocked, 150, 255, Col1IsRecordLocked, False, False)


            .AddAgNumberColumn(Dgl1, Col1PurchaseDiscountPer, 80, 2, 3, False, Col1PurchaseDiscountPer, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1PurchaseDiscountAmount, 100, 8, 3, False, Col1PurchaseDiscountAmount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1PurchaseAdditionalDiscountPer, 80, 2, 3, False, Col1PurchaseAdditionalDiscountPer, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1PurchaseAdditionalDiscountAmount, 100, 8, 3, False, Col1PurchaseAdditionalDiscountAmount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1MaintainStockYn, 150, 255, Col1MaintainStockYn, False, False)
            .AddAgTextColumn(Dgl1, Col1SalesAc, 100, 0, Col1SalesAc, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl1)
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
        Dgl1.AgMandatoryColumn = Dgl1.Columns(Col1Item).Index
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top


        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If

        If LblV_Type.Tag = Ncat.SaleInvoice And
                CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsApplicable_SaleOrder")), Boolean) = True Then
            Dgl1.Columns(Col1SaleInvoice).Visible = True
            Dgl1.Columns(Col1SaleInvoice).ReadOnly = False
            Dgl1.Columns(Col1SaleInvoice).DefaultCellStyle.BackColor = Color.White
            Dgl1.Columns(Col1SaleInvoice).HeaderText = "Sale Order"
        End If



        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col1Head, 150, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl2, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl2, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl2, Col1Value, 270, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        AgL.GridDesign(Dgl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl2.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Dgl2.ColumnHeadersHeight = 35
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AllowUserToAddRows = False
        Dgl2.RowHeadersVisible = False
        Dgl2.ColumnHeadersVisible = False


        Dgl2.Rows.Add(11)

        Dgl2.Item(Col1Head, rowRateType).Value = hcRateType
        Dgl2.Item(Col1Head, rowPartyDocNo).Value = hcPartyDocNo
        Dgl2.Item(Col1Head, rowPartyDocDate).Value = hcPartyDocDate
        Dgl2.Item(Col1Head, rowDeliveryDate).Value = hcDeliveryDate
        Dgl2.Item(Col1Head, rowMinDeliveryDate).Value = hcMinDeliveryDate
        Dgl2.Item(Col1Head, rowReferenceNo).Value = hcReferenceNo
        Dgl2.Item(Col1Head, rowGodown).Value = hcGodown
        Dgl2.Item(Col1Head, rowShipToParty).Value = hcShipToParty
        Dgl2.Item(Col1Head, rowSalesTaxNo).Value = HcSalesTaxNo
        Dgl2.Item(Col1Head, rowAadharNo).Value = HcAadharNo
        Dgl2.Item(Col1Head, rowStructure).Value = hcStructure
        Dgl2.Name = "Dgl2"
        Dgl2.Tag = "VerticalGrid"





        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl3, Col1Head, 150, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl3, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl3, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl3, Col1Value, 290, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        AgL.GridDesign(Dgl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl3.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Dgl3.ColumnHeadersHeight = 35
        Dgl3.AgSkipReadOnlyColumns = True
        Dgl3.AllowUserToAddRows = False
        Dgl3.RowHeadersVisible = False
        Dgl3.ColumnHeadersVisible = False


        Dgl3.Rows.Add(14)

        Dgl3.Item(Col1Head, rowCreditDays).Value = hcCreditDays
        Dgl3.Item(Col1Head, rowAgent).Value = hcAgent
        Dgl3.Item(Col1Head, rowTransporter).Value = hcTransporter
        Dgl3.Item(Col1Head, rowResponsiblePerson).Value = hcResponsiblePerson
        Dgl3.Item(Col1Head, rowSalesRepresentative).Value = hcSalesRepresentative
        Dgl3.Item(Col1Head, rowRemarks1).Value = hcRemarks1
        Dgl3.Item(Col1Head, rowRemarks2).Value = hcRemarks2
        Dgl3.Item(Col1Head, rowTags).Value = hcTags
        Dgl3.Item(Col1Head, rowRemarks).Value = hcRemarks
        Dgl3.Item(Col1Head, rowTermsAndConditions).Value = hcTermsAndConditions
        Dgl3.Item(Col1Head, rowCustomerInvoiceCount).Value = hcCustomerInvoiceCount
        Dgl3.Item(Col1Head, rowEInvoiceIRN).Value = hcEInvoiceIRN
        Dgl3.Item(Col1Head, rowEInvoiceACKNo).Value = hcEInvoiceACKNo
        Dgl3.Item(Col1Head, rowEInvoiceACKDate).Value = hcEInvoiceACKDate

        Dgl3.Name = "Dgl3"
        Dgl3.Tag = "VerticalGrid"
        Dgl3.Anchor = AnchorStyles.Bottom + AnchorStyles.Left

        AgL.ProcCreateLink(Dgl1, Col1ReferenceDocId)
        AgL.ProcCreateLink(Dgl1, Col1SaleInvoice)
        AgL.ProcCreateLink(Dgl1, Col1ImportStatus)


        ApplyUISettings(EntryNCat)


        AgCalcGrid1.Ini_Grid(EntryNCat, TxtV_Date.Text)
        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        If AgL.VNull(AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable")) = True Then
            AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        Else
            AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = -1
        End If
        AgCalcGrid1.AgPostingPartyAc = TxtSaleToParty.AgSelectedValue
        AgCalcGrid1.Anchor = AnchorStyles.Bottom + AnchorStyles.Right


        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False




        AgCalcGrid1.Name = "AgCalcGrid1"
        AgCustomGrid1.Name = "AgCustomGrid1"


        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bStockSelectionQry$ = "", bHelpValuesSelectionQry$ = ""
        Dim mMultiplyWithMinus As Boolean = False


        If LblV_Type.Tag = Ncat.SaleReturn Then
            mMultiplyWithMinus = True
        End If

        mQry = " Update SaleInvoice " &
                " SET  " &
                " ManualRefNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                " SaleToPartyDocNo = " & AgL.Chk_Text(Dgl2(Col1Value, rowPartyDocNo).Value) & ", " &
                " SaleToPartyDocDate = " & AgL.Chk_Date(Dgl2(Col1Value, rowPartyDocDate).Value) & ", " &
                " MinDeliveryDate = " & AgL.Chk_Date(Dgl2(Col1Value, rowMinDeliveryDate).Value) & ", " &
                " DeliveryDate = " & AgL.Chk_Date(Dgl2(Col1Value, rowDeliveryDate).Value) & ", " &
                " SaleToParty = " & AgL.Chk_Text(TxtSaleToParty.Tag) & ", " &
                " BillToParty = " & AgL.Chk_Text(TxtBillToParty.Tag) & ", " &
                " ShipToParty = " & AgL.Chk_Text(Dgl2(Col1Value, rowShipToParty).Tag) & ", " &
                " Agent = " & AgL.Chk_Text(Dgl3(Col1Value, rowAgent).Tag) & ", " &
                " ResponsiblePerson = " & AgL.Chk_Text(Dgl3(Col1Value, rowResponsiblePerson).Tag) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                " RateType = " & AgL.Chk_Text(Dgl2(Col1Value, rowRateType).Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(Dgl3(Col1Value, rowRemarks).Value) & ", " &
                " ReferenceNo = " & AgL.Chk_Text(Dgl2(Col1Value, rowReferenceNo).Value) & ", " &
                " ReferenceDocID = " & AgL.Chk_Text(Dgl2(Col1Value, rowReferenceNo).Tag) & ", " &
                " Tags = " & AgL.Chk_Text(Dgl3(Col1Value, rowTags).Value) & ", " &
                " Remarks1 = " & AgL.Chk_Text(Dgl3(Col1Value, rowRemarks1).Value) & ", " &
                " Remarks2 = " & AgL.Chk_Text(Dgl3(Col1Value, rowRemarks2).Value) & ", " &
                " TermsAndConditions = " & AgL.Chk_Text(Dgl3(Col1Value, rowTermsAndConditions).Value) & ", " &
                " EInvoiceIRN = " & AgL.Chk_Text(Dgl3(Col1Value, rowEInvoiceIRN).Value) & ", " &
                " EInvoiceACKNo = " & AgL.Chk_Text(Dgl3(Col1Value, rowEInvoiceACKNo).Value) & ", " &
                " EInvoiceACKDate = " & AgL.Chk_Date(Dgl3(Col1Value, rowEInvoiceACKDate).Value) & ", " &
                " UploadDate = Null, " &
                " CreditDays = " & Val(Dgl3(Col1Value, rowCreditDays).Value) & ", " &
                " CreditLimit = " & Val(TxtCreditLimit.Text) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & " " &
                " " & IIf(TxtStructure.Tag = "", "", ", ") &
                " " & AgCalcGrid1.FFooterTableUpdateStr(mMultiplyWithMinus) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Topctrl1.Mode.ToUpper = "ADD" Then
            mQry = "
                    Insert Into SaleInvoiceTrnSetting
                    (DocID, IsPostedInStock, IsPostedInLedger)
                    Values
                    ('" & mSearchCode & "', " & AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStock")) & ", " & AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInLedger")) & ")
                   "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If Topctrl1.Mode.ToUpper = "EDIT" Then
            mQry = "Delete from Ledger where docId='" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete from SaleInvoiceDetailHelpValues Where DocID = '" & mSearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).FSave(mSearchCode, Conn, Cmd)

        If BtnHeaderDetail.Tag IsNot Nothing Then
            CType(BtnHeaderDetail.Tag, FrmSaleInvoiceTransport).FSave(mSearchCode, Conn, Cmd)
        Else
            If AgL.Dman_Execute("Select Count(*) From SaleInvoiceTransport  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar() > 0 Then
                mQry = "Update SaleInvoiceTransport  Set Transporter = " & AgL.Chk_Text(Dgl3(Col1Value, rowTransporter).Tag) & " 
                        Where DocID = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Else
                If Dgl3(Col1Value, rowTransporter).Tag <> "" Then
                    mQry = "Insert Into SaleInvoiceTransport(DocID, Transporter) Values ('" & mSearchCode & "', " & AgL.Chk_Text(Dgl3(Col1Value, rowTransporter).Tag) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        End If

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From SaleInvoiceDetail  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then

                If AgL.StrCmp(Dgl1.Item(Col1V_Nature, I).Value, "RETURN") Or LblV_Type.Tag = Ncat.SaleReturn Then
                    mMultiplyWithMinus = True
                Else
                    mMultiplyWithMinus = False
                End If

                If mMultiplyWithMinus Then
                    Dgl1.Item(Col1Qty, I).Value = -Math.Abs(Val(Dgl1.Item(Col1Qty, I).Value))
                    Dgl1.Item(Col1DocQty, I).Value = -Math.Abs(Val(Dgl1.Item(Col1DocQty, I).Value))
                    Dgl1.Item(Col1Amount, I).Value = -Math.Abs(Val(Dgl1.Item(Col1Amount, I).Value))
                End If

                If Dgl1.Item(Col1SaleInvoice, I).Tag = "" Then Dgl1.Item(Col1SaleInvoice, I).Tag = mSearchCode : Dgl1.Item(Col1SaleInvoiceSr, I).Value = mSr





                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1

                    InsertSaleInvoiceDetail(mSearchCode, mSr, I, mMultiplyWithMinus, Conn, Cmd)
                    InsertSaleInvoiceDetailHelpValues(mSearchCode, mSr, I, Conn, Cmd)
                    InsertSaleInvoiceBarcodeLastTransactionDetail(mSearchCode, mSr, I, Conn, Cmd)
                    UpdateBarcodeSiteDetail(mSearchCode, mSr, I, Conn, Cmd)


                    If Dgl1.Item(Col1DocQty, I).Tag IsNot Nothing Then
                        CType(Dgl1.Item(Col1DocQty, I).Tag, FrmSaleInvoiceDimension).FSave(mSearchCode, mSr, I, Conn, Cmd, mMultiplyWithMinus)
                    Else
                        mDimensionSrl += 1
                        InsertStock(mSearchCode, mSr, mDimensionSrl, I, mMultiplyWithMinus, Conn, Cmd)
                    End If
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        UpdateSaleInvoiceDetail(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, mMultiplyWithMinus, Conn, Cmd)
                        UpdateBarcodeSiteDetail(mSearchCode, mSr, I, Conn, Cmd)
                        InsertSaleInvoiceDetailHelpValues(mSearchCode, Val(Dgl1.Item(ColSNo, I).Value), I, Conn, Cmd)

                        If Dgl1.Item(Col1DocQty, I).Tag IsNot Nothing Then
                            CType(Dgl1.Item(Col1DocQty, I).Tag, FrmSaleInvoiceDimension).FSave(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd, mMultiplyWithMinus)
                        Else
                            UpdateStock(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), Val(Dgl1.Item(Col1StockSr, I).Value), I, mMultiplyWithMinus, Conn, Cmd)
                        End If
                    Else
                        DeleteLineData(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
                    End If
                End If

                UpdateItemGroupPerson(I, Conn, Cmd)
            End If
        Next


        Dim mNarrParty As String
        Dim mNarr As String

        mNarrParty = TxtV_Type.Text
        mNarr = TxtV_Type.Text & " : " & TxtSaleToParty.Text

        Dim bPartyLedgerPostingAc As String = ""
        Dim bLinkedPartyAc As String = ""
        If AgL.StrCmp(AgL.XNull(DtV_TypeSettings.Rows(0)("LedgerPostingPartyAcType")), SaleInvoiceLedgerPostingPartyAcType.SaleToParty) Then
            bPartyLedgerPostingAc = TxtSaleToParty.AgSelectedValue
            bLinkedPartyAc = TxtBillToParty.AgSelectedValue
        Else
            bPartyLedgerPostingAc = TxtBillToParty.AgSelectedValue
            bLinkedPartyAc = TxtSaleToParty.AgSelectedValue
        End If

        Call ClsFunction.PostStructureLineToAccounts(AgCalcGrid1, mNarrParty, mNarr, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue,
                           TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtReferenceNo.Text, bPartyLedgerPostingAc, TxtV_Date.Text, Conn, Cmd,, mMultiplyWithMinus, bLinkedPartyAc)

        mQry = " UPDATE Ledger Set CreditDays = " & Val(Dgl3(Col1Value, rowCreditDays).Value) & " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        If Dgl1.Columns(Col1Barcode).Visible Then
            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1Barcode, I).Value <> "" Then
                    ClsMain.UpdateCurrentStockAtBarcode(Dgl1.Item(Col1Barcode, I).Tag, TxtSite_Code.Tag, Conn, Cmd)
                End If
            Next
        End If

        UpdateLastTransactionData(Conn, Cmd)

        'If Val(TxtPaidAmt.Text) <> 0 And (Not AgL.StrCmp(TxtNature.Text, "Cash")) Then
        '    Call AccountPosting(Conn, Cmd)
        'End If

        If mFlag_Import = False Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
                AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
                AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
                AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
            End If
        End If
    End Sub

    Private Sub DeleteLineData(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim DtTemp As DataTable

        If Val(Dgl1.Item(ColSNo, LineGridRowIndex).Tag) > 0 Then
            mQry = "Delete From Stock Where DocId = '" & DocID & "' and TSr =" & Sr & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            If Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag <> "" Then
                mQry = "Select * from SaleInvoiceBarcodeLastTransactionValues With (NoLock) Where  DocId = '" & DocID & "' and Sr =" & Sr & ""
                DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mQry = "Update BarcodeSiteDetail set
                                            LastTrnDocID = " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(0)("LastTrnDocID"))) & ",
                                            LastTrnSr=" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(0)("LastTrnSr"))) & ",
                                            LastTrnV_Type=" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(0)("LastTrnV_Type"))) & ",
                                            LastTrnManualRefNo =" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(0)("LastTrnManualRefNo"))) & ",
                                            LastTrnSubcode=" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(0)("LastTrnSubcode"))) & ",
                                            LastTrnProcess=" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(0)("LastTrnProcess"))) & ",
                                            CurrentGodown=" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(0)("CurrentGodown"))) & ",
                                            Status =" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(0)("Status"))) & "
                                            WHERE CODE='" & Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag & "' AND Div_Code='" & TxtDivision.Tag & "' And Site_code='" & TxtSite_Code.Tag & "'                    
                                           "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If

                mQry = "Delete From SaleInvoiceBarcodeLastTransactionValues Where DocId = '" & DocID & "' and Sr =" & Sr & ""
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            mQry = "Delete From SaleInvoiceDimensionDetail Where DocId = '" & DocID & "' and TSr =" & Sr & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleInvoiceDetailSku Where DocId = '" & mSearchCode & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleInvoiceDetail Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub UpdateItemGroupPerson(LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        'We will  record personal discount in ItemGroupPerson table only if we are not providing default discount                
        If TxtNature.Text.ToUpper <> "CASH" Then
            If Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) > 0 And AgL.XNull(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Value) <> "" Then
                If Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) = 0 And Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) <> Val(Dgl1.Item(Col1DefaultDiscountPer, LineGridRowIndex).Value) Then

                    If AgL.Dman_Execute("Select Count(*) From ItemGroupPerson  With (NoLock) Where ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & "
                    And ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & "
                    And Person = " & AgL.Chk_Text(TxtSaleToParty.Tag) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then


                        mQry = " Insert Into ItemGroupPerson
                            (ItemCategory, ItemGroup, Person, DiscountCalculationPattern, DiscountPer, AdditionalDiscountCalculationPattern, AdditionalDiscountPer, AdditionCalculationPattern, AdditionPer)
                            Values
                            (" & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & ", " & AgL.Chk_Text(TxtSaleToParty.Tag) & ",
                             " & AgL.Chk_Text(Dgl1.Item(Col1DiscountCalculationPattern, LineGridRowIndex).Tag) & "," & Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1AdditionalDiscountCalculationPattern, LineGridRowIndex).Tag) & ", " & Val(Dgl1.Item(Col1AdditionalDiscountPer, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1AdditionCalculationPattern, LineGridRowIndex).Tag) & ", " & Val(Dgl1.Item(Col1AdditionPer, LineGridRowIndex).Value) & "
                            )
                           "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                ElseIf Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) > 0 And Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) <> Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) Then
                    mQry = "
                                Update ItemGroupPerson 
                                Set 
                                DiscountCalculationPattern = " & AgL.Chk_Text(Dgl1.Item(Col1DiscountCalculationPattern, LineGridRowIndex).Tag) & ",
                                DiscountPer=" & Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) & ",
                                AdditionalDiscountCalculationPattern = " & AgL.Chk_Text(Dgl1.Item(Col1AdditionalDiscountCalculationPattern, LineGridRowIndex).Tag) & ",
                                AdditionalDiscountPer=" & Val(Dgl1.Item(Col1AdditionalDiscountPer, LineGridRowIndex).Value) & ",
                                AdditionCalculationPattern = " & AgL.Chk_Text(Dgl1.Item(Col1AdditionCalculationPattern, LineGridRowIndex).Tag) & ",
                                AdditionPer=" & Val(Dgl1.Item(Col1AdditionPer, LineGridRowIndex).Value) & "
                                Where ItemCategory=" & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & "
                                And ItemGroup=" & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & "
                                And Person=" & AgL.Chk_Text(TxtSaleToParty.Tag) & "
                               "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        End If
    End Sub


    Private Sub InsertSaleInvoiceDetailHelpValues(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "
                Insert Into SaleInvoiceDetailHelpValues 
                (DocID, Sr, PurchaseRate, PurchaseDiscountPer, PurchaseAdditionalDiscountPer, 
                DefaultDiscountPer, DefaultAdditionalDiscountPer, DefaultAdditionPer, 
                PersonalDiscountPer, PersonalAdditionalDiscountPer, PersonalAdditionPer,
                DiscountCalculationPattern, AdditionalDiscountCalculationPattern, AdditionCalculationPattern) 
                Values('" & DocID & "', " & Sr & ", " & Val(Dgl1.Item(Col1PurchaseRate, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1PurchaseDiscountPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1PurchaseAdditionalDiscountPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1DefaultDiscountPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1DefaultAdditionalDiscountPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1DefaultAdditionPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1PersonalAdditionalDiscountPer, LineGridRowIndex).Value) & ",
                " & Val(Dgl1.Item(Col1PersonalAdditionPer, LineGridRowIndex).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1DiscountCalculationPattern, LineGridRowIndex).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1AdditionalDiscountCalculationPattern, LineGridRowIndex).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1AdditionCalculationPattern, LineGridRowIndex).Value) & "                
                ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub


    Private Sub InsertStock(DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""
        If BtnFillPartyDetail.Tag IsNot Nothing Then
            bSalesTaxGroupParty = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowSalesTaxGroup).Value
        End If

        mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                SubCode, SalesTaxGroupParty, Barcode, Item, ItemState, SalesTaxGroupItem,  LotNo, 
                                EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                                Values
                                (
                                    '" & DocID & "', " & TSr & ", " & Sr & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                                    " & AgL.Chk_Date(TxtV_Date.Text) & ", " & AgL.Chk_Text(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                                    " & AgL.Chk_Text(TxtSaleToParty.Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & " , " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                                    'I', " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ",0, " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                                    " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",0,
                                    " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & "
                                )"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    End Sub

    Private Sub UpdateStock(DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""
        If LblV_Type.Tag = Ncat.SaleOrder Then Exit Sub
        If Dgl1.Item(Col1StockSr, LineGridRowIndex).Value <> "" Then
            If Dgl1.Item(Col1StockSr, LineGridRowIndex).Value.ToString.Contains(",") = 0 Then

                If BtnFillPartyDetail.Tag IsNot Nothing Then
                    bSalesTaxGroupParty = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowSalesTaxGroup).Value
                End If

                mQry = "Update Stock Set
                        V_Type = " & AgL.Chk_Text(TxtV_Type.Tag) & ", 
                        V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                        V_Date = " & AgL.Chk_Date(TxtV_Date.Text) & ", 
                        V_No = " & AgL.Chk_Text(TxtV_No.Text) & ", 
                        RecId = " & AgL.Chk_Text(TxtReferenceNo.Text) & ",  
                        Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                        Site_Code = " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                        Subcode = " & AgL.Chk_Text(TxtSaleToParty.Tag) & ", 
                        SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ",
                        Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                        Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", 
                        ItemState = " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", 
                        SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, LineGridRowIndex).Value) & ", 
                        LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                        Godown = " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & ",
                        EType_IR = 'I', 
                        Qty_Iss = " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ",
                        Qty_Rec = 0, 
                        Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ",
                        UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                        DealQty_Iss = " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 
                        DealQty_Rec =0,  
                        DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", 
                        Rate = " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", 
                        Amount = " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",
                        Landed_Value = 0,
                        ReferenceDocId = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Value) & ", 
                        ReferenceTSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ", 
                        ReferenceDocIdSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & " 
                        Where DocId = '" & DocID & "' and TSr =" & TSr & " And Sr =" & Sr & "
                    "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Else
            If BtnFillPartyDetail.Tag IsNot Nothing Then
                bSalesTaxGroupParty = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowSalesTaxGroup).Value
            End If
            mDimensionSrl += 1
            mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                SubCode, SalesTaxGroupParty, Barcode, Item, ItemState, SalesTaxGroupItem,  LotNo, Godown, 
                                EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                                Values
                                (
                                    '" & DocID & "', " & TSr & ", " & mDimensionSrl & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                                    " & AgL.Chk_Date(TxtV_Date.Text) & ", " & AgL.Chk_Text(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                                    " & AgL.Chk_Text(TxtSaleToParty.Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & " , " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Value) & ",
                                    'I', " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ",0, " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                                    " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",0,
                                    " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & "
                                )"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub InsertSaleInvoiceDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into SaleInvoiceDetail(DocId, Sr, Barcode, Item, Specification, ItemState, SalesTaxGroupItem, 
                           DocQty, FreeQty, Qty, Unit, Pcs, UnitMultiplier, DealUnit, 
                           DocDealQty, MasterSaleRate, RateDiscountPer, Rate, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount,  
                           AdditionPer, AdditionAmount, PurityPer, 
                           Amount, Remark, SalesRepresentative, BaleNo, LotNo, Godown,  
                           ReferenceNo, ReferenceDate, ReferenceDocId, ReferenceDocIDTSr, ReferenceDocIdSr, SaleInvoice, SaleInvoiceSr,
                           V_Nature " & IIf(TxtStructure.Tag = "", "", ",") & AgCalcGrid1.FLineTableFieldNameStr() & ") "
        mQry += " Values( " & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, LineGridRowIndex).Tag) & ", " &
                                        " " & Val(Dgl1.Item(Col1DocQty, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1FreeQty, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Pcs, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1MasterSaleRate, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1RateDiscountPer, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DiscountAmount, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1AdditionalDiscountPer, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1AdditionalDiscountAmount, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1AdditionPer, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1AdditionAmount, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1PurityPer, LineGridRowIndex).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl3(Col1Value, rowSalesRepresentative).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & " , " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & " , " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & " , " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1SaleInvoice, LineGridRowIndex).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1SaleInvoiceSr, LineGridRowIndex).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1V_Nature, LineGridRowIndex).Value) & IIf(TxtStructure.Tag = "", "", ",") &
                                        " " & AgCalcGrid1.FLineTableFieldValuesStr(LineGridRowIndex, MultiplyWithMinus) & " )"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into SaleInvoiceDetailSku
                (DocId, Sr, ItemCategory, ItemGroup, Item, Dimension1, 
                Dimension2, Dimension3, Dimension4) "
        mQry += " Values(" & AgL.Chk_Text(mSearchCode) & ", " & Sr & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & "
                )"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub UpdateSaleInvoiceDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
        If Dgl1.Rows(LineGridRowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
            mQry = " UPDATE SaleInvoiceDetail " &
                                    " Set " &
                                    " Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", " &
                                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
                                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", " &
                                    " ItemState = " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", " &
                                    " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, LineGridRowIndex).Value) & ", " &
                                    " DocQty = " & Val(Dgl1.Item(Col1DocQty, LineGridRowIndex).Value) & ", " &
                                    " FreeQty = " & Val(Dgl1.Item(Col1FreeQty, LineGridRowIndex).Value) & ", " &
                                    " Qty = " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " &
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ", " &
                                    " Pcs = " & Val(Dgl1.Item(Col1Pcs, LineGridRowIndex).Value) & ", " &
                                    " UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ", " &
                                    " DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " &
                                    " DocDealQty = " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", " &
                                    " MasterSaleRate = " & Val(Dgl1.Item(Col1MasterSaleRate, LineGridRowIndex).Value) & ", " &
                                    " RateDiscountPer = " & Val(Dgl1.Item(Col1RateDiscountPer, LineGridRowIndex).Value) & ", " &
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " &
                                    " DiscountPer = " & Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) & ", " &
                                    " DiscountAmount = " & Val(Dgl1.Item(Col1DiscountAmount, LineGridRowIndex).Value) & ", " &
                                    " AdditionalDiscountPer = " & Val(Dgl1.Item(Col1AdditionalDiscountPer, LineGridRowIndex).Value) & ", " &
                                    " AdditionalDiscountAmount = " & Val(Dgl1.Item(Col1AdditionalDiscountAmount, LineGridRowIndex).Value) & ", " &
                                    " AdditionPer = " & Val(Dgl1.Item(Col1AdditionPer, LineGridRowIndex).Value) & ", " &
                                    " AdditionAmount = " & Val(Dgl1.Item(Col1AdditionAmount, LineGridRowIndex).Value) & ", " &
                                    " PurityPer = " & Val(Dgl1.Item(Col1PurityPer, LineGridRowIndex).Value) & ", " &
                                    " Amount = " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ", " &
                                    " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & ", " &
                                    " SalesRepresentative = " & AgL.Chk_Text(Dgl3(Col1Value, rowSalesRepresentative).Tag) & ", " &
                                    " BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ", " &
                                    " LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ", " &
                                    " Godown = " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Value) & ", " &
                                    " ReferenceNo = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, LineGridRowIndex).Value) & ", " &
                                    " ReferenceDate = " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, LineGridRowIndex).Value) & ", " &
                                    " ReferenceDocId = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Value) & ", " &
                                    " ReferenceDocIdTSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ", " &
                                    " ReferenceDocIdSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & ", " &
                                    " SaleInvoice = " & AgL.Chk_Text(Dgl1.Item(Col1SaleInvoice, LineGridRowIndex).Tag) & ", " &
                                    " SaleInvoiceSr = " & AgL.Chk_Text(Dgl1.Item(Col1SaleInvoiceSr, LineGridRowIndex).Value) & ", " &
                                    " UploadDate = Null, " &
                                    " V_Nature = " & AgL.Chk_Text(Dgl1.Item(Col1V_Nature, LineGridRowIndex).Value) & IIf(TxtStructure.Tag = "", "", ",") &
                                    " " & AgCalcGrid1.FLineTableUpdateStr(LineGridRowIndex, MultiplyWithMinus) & " " &
                                    " Where DocId = '" & mSearchCode & "' " &
                                    " And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag & " "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Update SaleInvoiceDetailSku " &
                    " SET ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & ", " &
                    " ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & ", " &
                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
                    " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & ", " &
                    " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & ", " &
                    " Dimension3 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & ", " &
                    " Dimension4 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & " " &
                    " Where DocId = '" & mSearchCode & "' " &
                    " And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag & " "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        End If
    End Sub

    Private Sub InsertSaleInvoiceBarcodeLastTransactionDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag <> "" Then
            mQry = "
                        INSERT INTO SaleInvoiceBarcodeLastTransactionValues 
                        (DocID, Sr, LastTrnDiv_Code, LastTrnSite_Code, LastTrnDocID, LastTrnSr, LastTrnV_Type, LastTrnManualRefNo, LastTrnSubcode, LastTrnProcess, CurrentGodown, Status)
                        select '" & DocID & "' DocID, " & Sr & " Sr, Div_Code, Site_Code, LastTrnDocID, LastTrnSr, LastTrnV_Type, LastTrnManualRefNo, LastTrnSubcode, LastTrnProcess, CurrentGodown, Status
                        From barcodesitedetail  With (NoLock)
                        WHERE CODE='" & Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag & "' AND Div_Code='" & TxtDivision.Tag & "' And Site_code='" & TxtSite_Code.Tag & "'                    
                    "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        End If

    End Sub

    Private Sub UpdateBarcodeSiteDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim mBarcodeStatus As String = ""
        If LblV_Type.Tag = Ncat.SaleInvoice Then
            mBarcodeStatus = BarcodeStatus.Issue
        ElseIf LblV_Type.Tag = Ncat.SaleInvoice Then
            mBarcodeStatus = BarcodeStatus.Receive
        End If

        If Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag <> "" And mBarcodeStatus <> "" Then
            mQry = "Update BarcodeSiteDetail Set
                                LastTrnDocID = " & AgL.Chk_Text(DocID) & ",
                                LastTrnSr=" & AgL.Chk_Text(Sr) & ",
                                LastTrnV_Type=" & AgL.Chk_Text(TxtV_Type.Tag) & ",
                                LastTrnManualRefNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ",
                                LastTrnSubcode=" & AgL.Chk_Text(TxtSaleToParty.Tag) & ",
                                LastTrnProcess=" & AgL.Chk_Text(Process.Sales) & ",
                                CurrentGodown=" & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & ",
                                Status = " & AgL.Chk_Text(mBarcodeStatus) & "
                                WHERE CODE='" & Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag & "' AND Div_Code='" & TxtDivision.Tag & "' And Site_code='" & TxtSite_Code.Tag & "'                    
                               "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

    End Sub
    Private Sub UpdateLastTransactionData(ByRef Conn As Object, ByRef Cmd As Object)

        Dim bTransporter As String = ""
        Dim bTermsAndConditions As String = ""
        Dim DtSubgroupTypeSetting As DataTable

        'If BtnHeaderDetail.Tag <> "" Then bTransporter = CType(BtnHeaderDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Head, FrmSaleInvoiceTransport.rowTransporter).Tag
        If BtnHeaderDetail.Tag IsNot Nothing Then bTransporter = CType(BtnHeaderDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowTransporter).Tag
        If bTransporter = "" Then bTransporter = Dgl3(Col1Value, rowTransporter).Tag
        If Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditions")) Then bTermsAndConditions = Dgl3(Col1Value, rowTermsAndConditions).Value

        mQry = "Select * From SubgroupTypeSetting Where SubgroupType = '" & SubgroupType.Customer & "'"
        DtSubgroupTypeSetting = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        If DtSubgroupTypeSetting.Rows.Count > 0 Then
            If AgL.VNull(DtSubgroupTypeSetting.Rows(0)("PersonCanHaveDivisionWiseRateTypeYn")) And AgL.VNull(DtSubgroupTypeSetting.Rows(0)("PersonCanHaveSiteWiseRateTypeYn")) Then
                mQry = "Update SubgroupSiteDivisionDetail Set RateType = " & AgL.Chk_Text(Dgl2(Col1Value, rowRateType).Tag) & "
                Where Subcode = " & AgL.Chk_Text(TxtSaleToParty.Tag) & " And Site_Code='" & TxtSite_Code.Tag & "' And Div_Code='" & TxtDivision.Tag & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            ElseIf AgL.VNull(DtSubgroupTypeSetting.Rows(0)("PersonCanHaveDivisionWiseRateTypeYn")) Then
                mQry = "Update SubgroupSiteDivisionDetail Set RateType = " & AgL.Chk_Text(Dgl2(Col1Value, rowRateType).Tag) & "
                Where Subcode = " & AgL.Chk_Text(TxtSaleToParty.Tag) & " And Div_Code='" & TxtDivision.Tag & "' And Site_Code Is Null "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            ElseIf AgL.VNull(DtSubgroupTypeSetting.Rows(0)("PersonCanHaveSiteWiseRateTypeYn")) Then
                mQry = "Update SubgroupSiteDivisionDetail Set RateType = " & AgL.Chk_Text(Dgl2(Col1Value, rowRateType).Tag) & "
                Where Subcode = " & AgL.Chk_Text(TxtSaleToParty.Tag) & " And Site_Code='" & TxtSite_Code.Tag & "' And Div_Code Is Null "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            If AgL.VNull(DtSubgroupTypeSetting.Rows(0)("PersonCanHaveDivisionWiseTransporterYn")) And AgL.VNull(DtSubgroupTypeSetting.Rows(0)("PersonCanHaveSiteWiseTransporterYn")) Then
                mQry = "Update SubgroupSiteDivisionDetail Set Transporter = " & AgL.Chk_Text(Dgl3(Col1Value, rowTransporter).Tag) & "
                Where Subcode = " & AgL.Chk_Text(TxtSaleToParty.Tag) & " And Site_Code='" & TxtSite_Code.Tag & "' And Div_Code='" & TxtDivision.Tag & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            ElseIf AgL.VNull(DtSubgroupTypeSetting.Rows(0)("PersonCanHaveDivisionWiseRateTypeYn")) Then
                mQry = "Update SubgroupSiteDivisionDetail Set Transporter = " & AgL.Chk_Text(Dgl3(Col1Value, rowTransporter).Tag) & "
                Where Subcode = " & AgL.Chk_Text(TxtSaleToParty.Tag) & " And Div_Code='" & TxtDivision.Tag & "' And Site_Code Is Null "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            ElseIf AgL.VNull(DtSubgroupTypeSetting.Rows(0)("PersonCanHaveSiteWiseRateTypeYn")) Then
                mQry = "Update SubgroupSiteDivisionDetail Set Transporter = " & AgL.Chk_Text(Dgl3(Col1Value, rowTransporter).Tag) & "
                Where Subcode = " & AgL.Chk_Text(TxtSaleToParty.Tag) & " And Site_Code='" & TxtSite_Code.Tag & "' And Div_Code Is Null "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            mQry = "Update SubgroupSiteDivisionDetail Set 
                TermsAndConditions = " & AgL.Chk_Text(bTermsAndConditions) & "
                Where Subcode = " & AgL.Chk_Text(TxtSaleToParty.Tag) & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Else
            MsgBox("Subgroup Type Settings not found for customer type")
        End If

    End Sub
    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl2RowCount As Integer
        Dim mDgl3RowCount As Integer
        Try


            For I = 0 To Dgl2.Rows.Count - 1
                Dgl2.Rows(I).Visible = False
            Next
            Dgl2.Visible = False

            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & Dgl2.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl2.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl2.Item(Col1Head, J).Value Then
                            Dgl2.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl2RowCount += 1
                            Dgl2.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl2.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            'MsgBox(NameOf(rowAdditionalDiscountPatternPurchase))
                        End If
                    Next
                Next
            End If
            If mDgl2RowCount > 0 Then
                Dgl2.Visible = True
            End If



            For I = 0 To Dgl3.Rows.Count - 1
                Dgl3.Rows(I).Visible = False
            Next
            Dgl3.Visible = False


            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & Dgl3.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl3.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl3.Item(Col1Head, J).Value Then
                            Dgl3.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl3RowCount += 1
                            Dgl3.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl3.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            'MsgBox(NameOf(rowAdditionalDiscountPatternPurchase))
                        End If
                    Next
                Next
            End If
            If mDgl3RowCount > 0 Then
                Dgl3.Visible = True
            End If




            For I = 1 To Dgl1.Columns.Count - 1
                Dgl1.Columns(I).Visible = False
            Next

            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='" & Me.Name & "' And NCat = '" & NCAT & "' 
                    And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
                            Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If


            If LblV_Type.Tag.ToString.ToUpper = AgLibrary.ClsMain.agConstants.Ncat.SaleReturn Then
                Dgl1.Columns(Col1ReferenceNo).Visible = True
                Dgl1.Columns(Col1ReferenceDate).Visible = True
            End If

            If AgL.StrCmp(AgL.PubDBName, "SHADHVINEW") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIKANPURB2") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIjaunpur") Or AgL.StrCmp(AgL.PubDBName, "SHADHVINANDI") Then
                Dgl1.Columns(Col1Rate).ReadOnly = True
            End If

            'mQry = "Select H.*
            '        from EntryLineUISetting H                    
            '        Where EntryName='" & Me.Name & "' And NCat = '" & NCAT & "' And GridName ='" & Dgl1.Name & "' "
            'DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            'If DtTemp.Rows.Count > 0 Then
            '    For I = 0 To DtTemp.Rows.Count - 1
            '        For J = 0 To Dgl1.Columns.Count - 1
            '            If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
            '                Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
            '                If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1ColumnCount += 1
            '                If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
            '                    Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
            '                End If
            '                'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
            '            End If
            '        Next
            '    Next
            'End If
            'If mDgl1ColumnCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False
        Dim mQryStockSr As String

        Dim DsMain As DataSet
        Dim DtTemp As DataTable

        If mFlag_Import = False And DtV_TypeSettings.Rows.Count > 0 Then
            mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtV_TypeSettings.Rows.Count = 0 Then
                            mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        End If
                    End If
                End If
            End If
            If DtV_TypeSettings.Rows.Count = 0 Then
                MsgBox("Voucher Type Settings Not Found.")
            End If
        End If


        LblTotalPcs.Text = 0
        LblTotalQty.Text = 0
        LblDealQty.Text = 0
        LblTotalBale.Text = 0
        LblTotalAmount.Text = 0

        If LblV_Type.Tag = Ncat.SaleReturn Then
            mMultiplyWithMinus = True
        End If


        mQry = " Select H.*, Sg.Name As SaleToPartyDesc, 
                BillToParty.Name As BillToPartyDesc, ShipToParty.Name as ShipToPartyDesc,
                C1.CityName As SaleToPartyCityName, Agent.Name As AgentName, ResponsiblePerson.Name as ResponsiblePersonName, Sg.Nature, RT.Description as RateTypeName                
                From (Select * From SaleInvoice With (NoLock) Where DocID='" & SearchCode & "') H 
                LEFT JOIN viewHelpSubGroup Sg With (NoLock) ON H.SaleToParty = Sg.Code 
                LEFT JOIN viewHelpSubGroup BillToParty With (NoLock) On H.BillToParty = BillToParty.Code 
                LEFT JOIN viewHelpSubGroup ShipToParty With (NoLock) On H.ShipToParty = ShipToParty.Code 
                LEFT JOIN City C1  With (NoLock) On H.SaleToPartyCity = C1.CityCode 
                LEFT JOIN City BillToPartyCity  With (NoLock) On BillToParty.CityCode = BillToPartyCity.CityCode 
                LEFT JOIN viewHelpSubGroup Agent  With (NoLock) On H.Agent = Agent.Code 
                LEFT JOIN viewHelpSubGroup ResponsiblePerson  With (NoLock) On H.ResponsiblePerson = ResponsiblePerson.Code 
                Left Join RateType RT  With (NoLock) On H.RateType = RT.Code
                "
        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                'TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                'TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)


                TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))

                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag

                TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))

                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()
                FGetStructureForInputField()

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                Dgl2(Col1Value, rowPartyDocNo).Value = AgL.XNull(.Rows(0)("SaleToPartyDocNo"))
                Dgl2(Col1Value, rowPartyDocDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("SaleToPartyDocDate")))
                Dgl2(Col1Value, rowDeliveryDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("DeliveryDate")))
                Dgl2(Col1Value, rowMinDeliveryDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("MinDeliveryDate")))


                Dgl2(Col1Value, rowReferenceNo).Tag = AgL.XNull(.Rows(0)("ReferenceDocId"))
                Dgl2(Col1Value, rowReferenceNo).Value = AgL.XNull(AgL.XNull(.Rows(0)("ReferenceNo")))

                TxtSaleToParty.Tag = AgL.XNull(.Rows(0)("SaleToParty"))
                TxtSaleToParty.Text = AgL.XNull(.Rows(0)("SaleToPartyDesc"))
                TxtBillToParty.Tag = AgL.XNull(.Rows(0)("BillToParty"))
                TxtBillToParty.Text = AgL.XNull(.Rows(0)("BillToPartyDesc"))
                Dgl2(Col1Value, rowShipToParty).Tag = AgL.XNull(.Rows(0)("ShipToParty"))
                Dgl2(Col1Value, rowShipToParty).Value = AgL.XNull(.Rows(0)("ShipToPartyDesc"))

                Dgl3(Col1Value, rowAgent).Tag = AgL.XNull(.Rows(0)("Agent"))
                Dgl3(Col1Value, rowAgent).Value = AgL.XNull(.Rows(0)("AgentName"))
                Dgl3(Col1Value, rowResponsiblePerson).Tag = AgL.XNull(.Rows(0)("ResponsiblePerson"))
                Dgl3(Col1Value, rowResponsiblePerson).Value = AgL.XNull(.Rows(0)("ResponsiblePersonName"))

                TxtNature.Text = AgL.XNull(.Rows(0)("Nature"))

                mQry = " SELECT Max(CASE WHEN Sgr.RegistrationType = 'Sales Tax No' THEN Sgr.RegistrationNo ELSE NULL END) AS SalesTaxNo,
                        Max(CASE WHEN Sgr.RegistrationType = 'AADHAR NO' THEN Sgr.RegistrationNo ELSE NULL END) AS AadharNo
                        FROM SubgroupRegistration Sgr
                        WHERE Sgr.Subcode = '" & TxtSaleToParty.Tag & "'
                        GROUP BY Sgr.Subcode"
                Dim DtRegistration As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtRegistration.Rows.Count > 0 Then
                    Dgl2.Item(Col1Value, rowSalesTaxNo).Value = DtRegistration.Rows(0)("SalesTaxNo")
                    Dgl2.Item(Col1Value, rowAadharNo).Value = DtRegistration.Rows(0)("AadharNo")
                End If


                SetPersonWiseDiscounts(TxtSaleToParty.Tag)




                If LblV_Type.Tag = Ncat.SaleReturn Then
                    mQry = "Select Count(*) 
                            From SaleInvoice H With (NoLock) 
                            Left Join Voucher_Type VT With (NoLock) On H.V_Type = VT.V_Type
                            Where VT.NCat = '" & Ncat.SaleInvoice & "' 
                            And H.SaleToParty = '" & TxtSaleToParty.Tag & "'
                            And Date(H.V_Date) <= " & AgL.Chk_Date(TxtV_Date.Text) & " 
                           "
                    Dgl3.Item(Col1Value, rowCustomerInvoiceCount).Value = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()
                End If




                mQry = "Select H.Transporter, T.Name as TransporterName 
                        From SaleInvoiceTransport H  With (NoLock)
                        Left Join viewHelpSubgroup T  With (NoLock) On H.Transporter = T.Code Where DocID ='" & SearchCode & "'"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Dgl3(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("Transporter"))
                    Dgl3(Col1Value, rowTransporter).Value = AgL.XNull(DtTemp.Rows(0)("TransporterName"))
                End If
                If Dgl3(Col1Value, rowTransporter).Tag = "" Then
                    mQry = "Select H.Transporter, T.Name as TransporterName 
                        From SubgroupSiteDivisionDetail H  With (NoLock)
                        Left Join viewHelpSubgroup T  With (NoLock) On H.Transporter = T.Code Where Subcode ='" & TxtSaleToParty.Tag & "'"
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl3(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("Transporter"))
                        Dgl3(Col1Value, rowTransporter).Value = AgL.XNull(DtTemp.Rows(0)("TransporterName"))
                    End If
                End If



                If mFlag_Import = False Then Call FGetCurrBal(TxtSaleToParty.AgSelectedValue)

                Dgl2(Col1Value, rowRateType).Tag = AgL.XNull(.Rows(0)("RateType"))
                Dgl2(Col1Value, rowRateType).Value = AgL.XNull(.Rows(0)("RateTypeName"))



                Dgl3(Col1Value, rowRemarks).Value = AgL.XNull(.Rows(0)("Remarks"))
                Dgl3(Col1Value, rowRemarks1).Value = AgL.XNull(.Rows(0)("Remarks1"))
                Dgl3(Col1Value, rowRemarks2).Value = AgL.XNull(.Rows(0)("Remarks2"))
                Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(.Rows(0)("TermsAndConditions"))
                Dgl3(Col1Value, rowCreditDays).Value = AgL.VNull(.Rows(0)("CreditDays"))
                TxtCreditLimit.Text = Format(AgL.VNull(.Rows(0)("CreditLimit")), "0.00")


                Dgl3(Col1Value, rowEInvoiceIRN).Value = AgL.XNull(.Rows(0)("EInvoiceIRN"))
                Dgl3(Col1Value, rowEInvoiceACKNo).Value = AgL.XNull(.Rows(0)("EInvoiceACKNo"))
                Dgl3(Col1Value, rowEInvoiceACKDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("EInvoiceACKDate")))


                Dgl3(Col1Value, rowTags).Value = AgL.XNull(.Rows(0)("Tags"))







                'Dim FrmObj As New FrmSaleInvoicePartyDetail
                'FrmObj.TxtSaleToPartyMobile.Text = AgL.XNull(.Rows(0)("SaleToPartyMobile"))
                'FrmObj.TxtSaleToPartyName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                'FrmObj.TxtSaleToPartyAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAddress"))
                'FrmObj.TxtSaleToPartyCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                'FrmObj.TxtSaleToPartyCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))

                'BtnFillPartyDetail.Tag = FrmObj

                'AgCustomGrid1.MoveRec_TransFooter(SearchCode)

                AgCalcGrid1.FMoveRecFooterTable(DsMain.Tables(0), EntryNCat, TxtV_Date.Text, mMultiplyWithMinus)

                AgCustomGrid1.FMoveRecFooterTable(DsMain.Tables(0))




                If AgL.PubServerName = "" Then
                    mQryStockSr = "Select  group_concat(Sr ,',') from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr"
                Else
                    mQryStockSr = "Select  Cast(Sr as Varchar) + ',' from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr for xml path('')"
                End If
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select L.*, SalesRep.Name as SalesRepresentativeName, Barcode.Description as BarcodeName, 
                        I.Description As ItemDesc, I.ManualCode, I.MaintainStockYn,
                        Case When Vt.NCat = '" & Ncat.SaleOrder & "' Then Si.V_Type || '-' || Si.ManualRefNo Else Null End As SaleInvoiceNo, 
                        Stock.V_Type || '-' || Stock.RecID As PurchaseNo, U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, 
                        U.ShowDimensionDetailInSales, MU.DecimalPlaces As DealUnitDecimalPlaces, 
                        (Stock.Landed_Value/(Stock.Qty_Rec+Stock.Qty_Iss)) + (Stock.Landed_Value/(Stock.Qty_Rec+Stock.Qty_Iss))*1/100 As PurchaseRate, 
                        IG.Description As ItemGroupName, I.ItemCategory, I.ItemGroup, IC.Description As ItemCategoryName,
                        I.ItemType, IT.Name as ItemTypeName, Godown.Name as GodownName, ISt.Description as ItemStateName, I.SalesAc, SAC.Name as SalesAcName ,   HV.*,
                        (" & mQryStockSr & ") as StockSr
                        From (Select * From SaleInvoiceDetail  With (NoLock)  Where DocId = '" & SearchCode & "') As L 
                        Left Join SaleInvoiceDetailHelpValues HV  With (NoLock) On L.DocID = HV.DocId And L.Sr = HV.Sr
                        LEFT JOIN Item I  With (NoLock) On L.Item = I.Code 
                        Left Join Item IC  With (NoLock) On I.ItemCategory = IC.Code 
                        Left Join Item IG  With (NoLock) On I.ItemGroup = IG.Code 
                        Left Join ItemType IT  With (NoLock) On I.ItemType = IT.Code 
                        Left Join Item ISt  With (NoLock) On L.ItemState = ISt.Code 
                        Left Join viewHelpSubgroup SalesRep On L.SalesRepresentative = SalesRep.Code
                        LEFT JOIN Stock  With (NoLock) On L.ReferenceDocId = Stock.docid And l.ReferenceDocIdSr = Stock.Sr  
                        LEFT JOIN SaleInvoice Si  With (NoLock) On L.SaleInvoice = Si.DocId 
                        LEFT JOIN Voucher_Type Vt With (NoLock) On Si.V_Type = Vt.V_Type
                        Left Join Barcode  With (NoLock) On L.Barcode = Barcode.Code
                        Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                        Left Join Unit MU  With (NoLock) On L.DealUnit = MU.Code 
                        Left Join Subgroup Godown On L.Godown = Godown.Subcode
                        Left Join viewHelpSubgroup SAC On I.SalesAc = SAC.Code
                        Order By L.Sr "

                DsMain = AgL.FillData(mQry, AgL.GCn)
                With DsMain.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsMain.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))

                            Dgl1.Item(Col1StockSr, I).Value = AgL.XNull(.Rows(I)("StockSr"))
                            If Dgl1.Item(Col1StockSr, I).Value <> "" Then
                                If Dgl1.Item(Col1StockSr, I).Value.ToString.Substring(Dgl1.Item(Col1StockSr, I).Value.ToString.Length - 1, 1) = "," Then
                                    Dgl1.Item(Col1StockSr, I).Value = Dgl1.Item(Col1StockSr, I).Value.ToString.Substring(0, Dgl1.Item(Col1StockSr, I).Value.ToString.Length - 1)
                                End If
                            End If


                            Dgl1.Item(Col1Barcode, I).Tag = AgL.XNull(.Rows(I)("Barcode"))
                            Dgl1.Item(Col1Barcode, I).Value = AgL.XNull(.Rows(I)("BarcodeName"))
                            Dgl2.Item(Col1Value, rowGodown).Tag = AgL.XNull(.Rows(I)("Godown"))
                            Dgl2.Item(Col1Value, rowGodown).Value = AgL.XNull(.Rows(I)("GodownName"))


                            If Dgl1.Item(Col1Barcode, I).Tag <> "" Then
                                Dim DtBarcodeSiteDetail As DataTable
                                mQry = "Select * From BarcodeSiteDetail  With (NoLock) Where Code='" & Dgl1.Item(Col1Barcode, I).Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' "
                                DtBarcodeSiteDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                If DtBarcodeSiteDetail.Rows.Count > 0 Then
                                    If AgL.XNull(DtBarcodeSiteDetail.Rows(0)("LastTrnDocId")) = SearchCode And
                                   AgL.VNull(DtBarcodeSiteDetail.Rows(0)("LastTrnSr")) = Val(Dgl1.Item(ColSNo, I).Tag) Then
                                    Else
                                        If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" And AgL.StrCmp(AgL.PubDBName, "Sadhvi") Then

                                        Else
                                            Dgl1.Item(Col1IsRecordLocked, I).Value = 1
                                        End If
                                    End If
                                End If
                            End If
                            Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(.Rows(I)("ItemType"))
                            Dgl1.Item(Col1ItemType, I).Value = AgL.XNull(.Rows(I)("ItemTypeName"))


                            Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryName"))

                            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))

                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl1.Item(Col1ItemState, I).Tag = AgL.XNull(.Rows(I)("ItemState"))
                            Dgl1.Item(Col1ItemState, I).Value = AgL.XNull(.Rows(I)("ItemStateName"))


                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1MaintainStockYn, I).Value = AgL.VNull(.Rows(I)("MaintainStockYn"))

                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))


                            Dgl1.Item(Col1SalesAc, I).Tag = AgL.XNull(.Rows(I)("SalesAc"))
                            Dgl1.Item(Col1SalesAc, I).Value = AgL.XNull(.Rows(I)("SalesAcName"))

                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1DocQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("DocQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1FreeQty, I).Value = Format(AgL.VNull(.Rows(I)("FreeQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Pcs, I).Value = AgL.VNull(.Rows(I)("Pcs"))

                            Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DealUnitDecimalPlaces"))
                            Dgl1.Item(Col1UnitMultiplier, I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                            Dgl1.Item(Col1DealQty, I).Value = Format(AgL.VNull(.Rows(I)("DocDealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MasterSaleRate, I).Value = AgL.VNull(.Rows(I)("MasterSaleRate"))
                            Dgl1.Item(Col1RateDiscountPer, I).Value = AgL.VNull(.Rows(I)("RateDiscountPer"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                            Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(.Rows(I)("DiscountPer"))
                            Dgl1.Item(Col1DiscountAmount, I).Value = AgL.VNull(.Rows(I)("DiscountAmount"))
                            Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(.Rows(I)("AdditionalDiscountPer"))
                            Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = AgL.VNull(.Rows(I)("AdditionalDiscountAmount"))
                            Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(.Rows(I)("AdditionPer"))
                            Dgl1.Item(Col1AdditionAmount, I).Value = AgL.VNull(.Rows(I)("AdditionAmount"))
                            Dgl1.Item(Col1PurchaseRate, I).Value = Format(AgL.VNull(.Rows(I)("PurchaseRate")), "0.00")
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1ReferenceNo, I).Value = AgL.XNull(.Rows(I)("ReferenceNo"))
                            Dgl1.Item(Col1ReferenceDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ReferenceDate")))
                            Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(.Rows(I)("ReferenceDocId"))
                            Dgl1.Item(Col1ReferenceDocIdTSr, I).Value = AgL.VNull(.Rows(I)("ReferenceDocIdTSr"))
                            Dgl1.Item(Col1ReferenceDocIdSr, I).Value = AgL.VNull(.Rows(I)("ReferenceDocIdSr"))
                            Dgl1.Item(Col1SaleInvoice, I).Tag = AgL.XNull(.Rows(I)("SaleInvoice"))
                            Dgl1.Item(Col1SaleInvoice, I).Value = AgL.XNull(.Rows(I)("SaleInvoiceNo"))
                            Dgl1.Item(Col1SaleInvoiceSr, I).Value = AgL.VNull(.Rows(I)("SaleInvoiceSr"))
                            Dgl1.Item(Col1DefaultDiscountPer, I).Value = AgL.VNull(.Rows(I)("DefaultDiscountPer"))
                            Dgl1.Item(Col1DefaultAdditionalDiscountPer, I).Value = AgL.VNull(.Rows(I)("DefaultAdditionalDiscountPer"))
                            Dgl1.Item(Col1DefaultAdditionPer, I).Value = AgL.VNull(.Rows(I)("DefaultAdditionPer"))
                            Dgl1.Item(Col1PersonalDiscountPer, I).Value = AgL.VNull(.Rows(I)("PersonalDiscountPer"))
                            Dgl1.Item(Col1PersonalAdditionalDiscountPer, I).Value = AgL.VNull(.Rows(I)("PersonalAdditionalDiscountPer"))
                            Dgl1.Item(Col1PersonalAdditionPer, I).Value = AgL.VNull(.Rows(I)("PersonalAdditionPer"))
                            Dgl1.Item(Col1PurityPer, I).Value = AgL.VNull(.Rows(I)("PurityPer"))
                            Dgl1.Item(Col1DiscountCalculationPattern, I).Value = AgL.XNull(.Rows(I)("DiscountCalculationPattern"))
                            Dgl1.Item(Col1AdditionalDiscountCalculationPattern, I).Value = AgL.XNull(.Rows(I)("AdditionalDiscountCalculationPattern"))
                            Dgl1.Item(Col1AdditionCalculationPattern, I).Value = AgL.XNull(.Rows(I)("AdditionCalculationPattern"))

                            Dgl1.Item(Col1V_Nature, I).Value = AgL.XNull(.Rows(I)("V_Nature"))
                            ''Dgl1.Item(Col1DiscountCalculationPattern, I).Value = ""
                            ''Dgl1.Item(Col1AdditionalDiscountCalculationPattern, I).Value = ""
                            ''Dgl1.Item(Col1AdditionCalculationPattern, I).Value = ""

                            'If Dgl3.Rows(rowSalesRepresentative).Visible = True Then
                            Dgl3(Col1Value, rowSalesRepresentative).Tag = AgL.XNull(.Rows(I)("SalesRepresentative"))
                            Dgl3(Col1Value, rowSalesRepresentative).Value = AgL.XNull(.Rows(I)("SalesRepresentativeName"))
                            'End If
                            '


                            If Dgl1.Item(Col1ReferenceDocId, I).Value = "" And Dgl1.Item(Col1ReferenceDocIdTSr, I).Value = 0 And Dgl1.Item(Col1ReferenceDocIdSr, I).Value = 0 Then
                                Dgl1.Item(Col1Unit, I).Tag = AgL.XNull(.Rows(I)("ShowDimensionDetailInSales"))
                                If AgL.VNull(Dgl1.Item(Col1Unit, I).Tag) Then
                                    Dgl1.Item(Col1DocQty, I).Style.ForeColor = Color.Blue
                                End If
                            End If

                            If LblV_Type.Tag = Ncat.SaleOrder Then
                                mQry = "Select IfNull(Sum(L.Amount),0) as ProgressAmount From SaleInvoice H Left Join SaleInvoiceDetail L On H.DocID = L.DocID Where L.SaleInvoice<>L.DocID And L.SaleInvoice || Cast(L.SaleInvoiceSr As NVARCHAR) = '" & SearchCode & "' || '" & Val(AgL.VNull(.Rows(I)("Sr"))) & "' Group By H.DocID,H.ManualRefNo "
                                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                If DtTemp.Rows.Count > 0 Then
                                    Dgl1.Item(Col1Progress, I).Value = Math.Round((AgL.VNull(DtTemp.Rows(0)("ProgressAmount")) / AgL.VNull(Dgl1.Item(Col1Amount, I).Value)) * 100, 2).ToString + "%"
                                End If
                            End If



                            FFormatRateCells(I)
                            If Val(Dgl1.Item(Col1IsRecordLocked, I).Value) > 0 Then
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True
                            End If

                            Call AgCalcGrid1.FMoveRecLineTable(DsMain.Tables(0), I, mMultiplyWithMinus)


                            LblTotalPcs.Text = Val(LblTotalPcs.Text) + Val(Dgl1.Item(Col1Pcs, I).Value)
                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                            LblTotalBale.Text += 1
                        Next I
                    End If
                End With
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

                '-------------------------------------------------------------

                Dgl1.Columns(Col1ImportStatus).Visible = False

            End If
        End With
        'ApplyUISettings(LblV_Type.Tag)
        SetAttachmentCaption()
        Dgl1.ReadOnly = True
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub


    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtSaleToParty.Validating, TxtReferenceNo.Validating, TxtBillToParty.Validating, TxtV_Date.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        'Dim FrmObj As New FrmSaleInvoicePartyDetail


        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    If TxtV_Type.Tag = "" Then Exit Sub

                    mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from SaleInvoiceSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtV_TypeSettings.Rows.Count = 0 Then
                            mQry = "Select * from SaleInvoiceSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtV_TypeSettings.Rows.Count = 0 Then
                                mQry = "Select * from SaleInvoiceSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                If DtV_TypeSettings.Rows.Count = 0 Then
                                    mQry = "Select * from SaleInvoiceSetting  With (NoLock) Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                End If
                            End If
                        End If
                    End If
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        MsgBox("Voucher Type Settings Not Found, Can not continue.")
                        Topctrl1.FButtonClick(14, True)
                        Exit Sub
                    End If

                    TxtSaleToParty.AgHelpDataSet = Nothing
                    TxtBillToParty.AgHelpDataSet = Nothing

                    Dgl2(Col1Value, rowRateType).Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RateType"))
                    If Dgl2(Col1Value, rowRateType).Tag <> "" Then
                        Dgl2(Col1Value, rowRateType).Value = AgL.Dman_Execute("Select Description from RateType  With (NoLock) Where Code ='" & Dgl2(Col1Value, rowRateType).Tag & "'", AgL.GCn).ExecuteScalar
                    End If


                    TxtStructure.AgSelectedValue = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GcnRead).ExecuteScalar
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue

                    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                    IniGrid()
                    'ApplyUISettings(LblV_Type.Tag)
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)


                    LblBarcode.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BarcodeGunTextbox")), Boolean)
                    TxtBarcode.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BarcodeGunTextbox")), Boolean)


                    If AgL.XNull(DtV_TypeSettings.Rows(0)("SaleInvoicePattern")) = SaleInvoicePattern.PointOfSale Then
                        If AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RevenuePoint")) <> "" Then
                            TxtSaleToParty.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RevenuePoint"))
                            TxtSaleToParty.Text = AgL.Dman_Execute("Select Name From viewHelpSubgroup  With (NoLock) Where Code = '" & TxtSaleToParty.Tag & "'", AgL.GCn).ExecuteScalar
                            Validating_SaleToParty(TxtSaleToParty.Tag, False)
                            If TxtBarcode.Visible = True And TxtBarcode.Enabled = True Then
                                TxtBarcode.Focus()
                            Else
                                Dgl1.Focus()
                            End If
                        End If
                    End If

                Case TxtSaleToParty.Name
                    'If TxtV_Date.Text <> "" And TxtSaleToParty.Text <> "" Then
                    '    If TxtSaleToParty.AgLastValueTag <> TxtSaleToParty.Tag Then
                    '        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")

                    '        TxtCreditDays.Text = AgL.VNull(DrTemp(0)("CreditDays"))
                    '        TxtCreditLimit.Text = AgL.VNull(DrTemp(0)("CreditLimit"))
                    '        TxtNature.Text = AgL.XNull(DrTemp(0)("Nature"))


                    '        mQry = "Select H.*, RT.Description as RateTypeName 
                    '                From SubgroupSiteDivisionDetail H
                    '                Left Join RateType RT on H.RateType = RT.Code
                    '                Where Subcode = '" & TxtSaleToParty.Tag & "'"
                    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    '        If DtTemp.Rows.Count > 0 Then
                    '            TxtRateType.Tag = AgL.XNull(DtTemp.Rows(0)("RateType"))
                    '            TxtRateType.Text = AgL.XNull(DtTemp.Rows(0)("RateTypeName"))
                    '            TxtTermsAndConditions.Text = AgL.XNull(DtTemp.Rows(0)("TermsAndConditions"))
                    '        Else
                    '            TxtRateType.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RateType"))
                    '            If TxtRateType.Tag <> "" Then
                    '                TxtRateType.Text = AgL.Dman_Execute("Select Description from RateType Where Code ='" & TxtRateType.Tag & "'", AgL.GCn).ExecuteScalar
                    '            End If
                    '            TxtTermsAndConditions.Text = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditions"))
                    '        End If



                    '        FGetCurrBal(TxtSaleToParty.AgSelectedValue)


                    '        BtnFillPartyDetail.Tag = Nothing
                    '        ShowSaleInvoiceParty("", TxtSaleToParty.Tag, TxtNature.Text, True)
                    '        TxtBillToParty.Tag = TxtSaleToParty.Tag
                    '        TxtBillToParty.Text = TxtSaleToParty.Text
                    '    End If
                    'End If

                    If LblV_Type.Tag = Ncat.SaleReturn Then
                        If Dgl1.Rows.Count >= 1 Then
                            If AgL.XNull(Dgl1.Item(Col1ItemCategory, 0).Value) <> "" Then
                                If TxtSaleToParty.AgLastValueText <> TxtSaleToParty.Text Then
                                    MsgBox("Change of party name is not allowed after feeding items")
                                    TxtSaleToParty.Tag = TxtSaleToParty.AgLastValueTag
                                    TxtSaleToParty.Text = TxtSaleToParty.AgLastValueText
                                    e.Cancel = True
                                    Exit Sub
                                End If
                            End If
                        End If
                    ElseIf LblV_Type.Tag = Ncat.SaleInvoice Then
                        If Dgl1.Rows.Count >= 1 Then
                            If AgL.XNull(Dgl1.Item(Col1SaleInvoice, 0).Tag) <> "" And AgL.XNull(Dgl1.Item(Col1SaleInvoice, 0).Tag) <> mSearchCode Then
                                If TxtSaleToParty.AgLastValueText <> TxtSaleToParty.Text Then
                                    MsgBox("Change of party name is not allowed after feeding items...!", MsgBoxStyle.Information)
                                    TxtSaleToParty.Tag = TxtSaleToParty.AgLastValueTag
                                    TxtSaleToParty.Text = TxtSaleToParty.AgLastValueText
                                    e.Cancel = True
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If


                    If AgL.XNull(DtV_TypeSettings.Rows(0)("SaleInvoicePattern")) = SaleInvoicePattern.PointOfSale Then
                        Validating_SaleToParty(TxtSaleToParty.Tag, False)
                    Else
                        Validating_SaleToParty(TxtSaleToParty.Tag)
                    End If



                Case TxtReferenceNo.Name
                    'e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "SaleInvoice",
                    '                TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                    '                TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                    '                TxtReferenceNo.Text, mSearchCode)
                    AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "SaleInvoice",
                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                    TxtReferenceNo.Text, mSearchCode)


                Case TxtBillToParty.Name
                    If TxtBillToParty.Text <> "" Then
                        If TxtBillToParty.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtNature.Text = AgL.XNull(DrTemp(0)("Nature"))
                        End If
                    End If


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub Validating_SaleToParty(Subcode As String, Optional ShowDialogForCashParty As Boolean = True)
        Dim DtTemp As DataTable



        If TxtV_Date.Text <> "" And TxtSaleToParty.Text <> "" Then
            If TxtSaleToParty.AgLastValueTag <> TxtSaleToParty.Tag Or Topctrl1.Mode = "Add" Then

                If ClsMain.IsPartyBlocked(Subcode, LblV_Type.Tag) Then
                    MsgBox("Party is blocked for " & TxtV_Type.Text & ". Record will not be saved")
                End If

                mQry = "Select * From Subgroup  With (NoLock) Where Subcode = '" & Subcode & "'"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Dgl3(Col1Value, rowCreditDays).Value = AgL.VNull(DtTemp.Rows(0)("CreditDays"))
                    TxtCreditLimit.Text = Format(AgL.VNull(DtTemp.Rows(0)("CreditLimit")), "0.00")
                    TxtNature.Text = AgL.XNull(DtTemp.Rows(0)("Nature"))
                End If


                Dim TemporaryLimit As Double = AgL.VNull(AgL.Dman_Execute("SELECT L.Amount As TemporaryCreditLimit  
                    FROM SubgroupTemporaryCreditLimit L With (NoLock)
                    WHERE L.Subcode = '" & Subcode & "' 
                    AND Date(L.FromDate) <= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & "
                    AND Date(L.ToDate) >= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & "
                    ", AgL.GCn).ExecuteScalar())
                If TemporaryLimit > 0 Then
                    TxtCreditLimit.Text = TemporaryLimit
                    LblCreditLimit.Tag = TemporaryLimit
                End If

                mQry = "Select H.*, RT.Description as RateTypeName, Agent.Name as AgentName, Transporter.Name as TransporterName 
                                    From SubgroupSiteDivisionDetail H  With (NoLock)
                                    Left Join RateType RT With (NoLock) on H.RateType = RT.Code
                                    Left Join viewHelpSubgroup agent With (NoLock) On H.Agent = Agent.Code
                                    Left Join viewHelpSubgroup Transporter With (NoLock) On H.Transporter = Transporter.Code
                                    Where H.Subcode = '" & Subcode & "' And H.Site_Code='" & TxtSite_Code.Tag & "' And H.Div_Code='" & TxtDivision.Tag & "'"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then

                    Dgl2(Col1Value, rowRateType).Tag = AgL.XNull(DtTemp.Rows(0)("RateType"))
                    Dgl2(Col1Value, rowRateType).Value = AgL.XNull(DtTemp.Rows(0)("RateTypeName"))
                    Dgl3(Col1Value, rowAgent).Tag = AgL.XNull(DtTemp.Rows(0)("Agent"))
                    Dgl3(Col1Value, rowAgent).Value = AgL.XNull(DtTemp.Rows(0)("AgentName"))
                    Dgl3(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("Transporter"))
                    Dgl3(Col1Value, rowTransporter).Value = AgL.XNull(DtTemp.Rows(0)("TransporterName"))







                    If AgL.XNull(DtTemp.Rows(0)("TermsAndConditions")) <> "" Then
                        If TxtNature.Text.ToUpper = "CASH" Then
                            Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditionsCash"))
                        Else
                            Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtTemp.Rows(0)("TermsAndConditions"))
                        End If
                    Else
                        If TxtNature.Text.ToUpper = "CASH" Then
                            Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditionsCash"))
                        Else
                            Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditions"))
                        End If
                    End If
                Else
                    'TxtRateType.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RateType"))
                    'If TxtRateType.Tag <> "" Then
                    '    TxtRateType.Text = AgL.Dman_Execute("Select Description from RateType Where Code ='" & TxtRateType.Tag & "'", AgL.GCn).ExecuteScalar
                    'End If
                    If TxtNature.Text.ToUpper = "CASH" Then
                        Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditionsCash"))
                    Else
                        Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditions"))
                    End If

                End If



                FGetCurrBal(Subcode)


                BtnFillPartyDetail.Tag = Nothing
                ShowSaleInvoiceParty("", Subcode, TxtNature.Text, ShowDialogForCashParty)
                TxtBillToParty.Tag = TxtSaleToParty.Tag
                TxtBillToParty.Text = TxtSaleToParty.Text
                TxtBillToParty.AgHelpDataSet = Nothing

                If BtnFillPartyDetail.Tag IsNot Nothing Then
                    Dgl2.Item(Col1Value, rowSalesTaxNo).Value = CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowSalesTaxNo).Value
                    Dgl2.Item(Col1Value, rowAadharNo).Value = CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowAadharNo).Value
                End If



                If TxtSaleToParty.Tag IsNot Nothing And TxtSaleToParty.Text <> "" Then
                    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeType")).ToString.Contains(TreeNodeType.Leaf) Then
                        mQry = "Select Par.Code, Par.Name
                            From SubGroup Sg
                            LEFT JOIN ViewHelpSubGroup Par On Sg.Parent = Par.Code
                            Where Sg.SubCode = '" & TxtSaleToParty.Tag & "'"
                        Dim DtBillToParty As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtBillToParty.Rows.Count > 0 Then
                            TxtBillToParty.Tag = AgL.XNull(DtBillToParty.Rows(0)("Code"))
                            TxtBillToParty.Text = AgL.XNull(DtBillToParty.Rows(0)("Name"))
                        End If
                    End If
                End If
                SetPersonWiseDiscounts(Subcode)

                If LblV_Type.Tag = Ncat.SaleReturn Then
                    mQry = "Select Count(*) 
                            From SaleInvoice H With (NoLock) 
                            Left Join Voucher_Type VT With (NoLock) On H.V_Type = VT.V_Type
                            Where VT.NCat = '" & Ncat.SaleInvoice & "' 
                            And H.SaleToParty = '" & TxtSaleToParty.Tag & "'
                            And Date(H.V_Date) <= " & AgL.Chk_Date(TxtV_Date.Text) & " 
                           "
                    Dgl3.Item(Col1Value, rowCustomerInvoiceCount).Value = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()
                End If
            End If
        End If
        Dgl1.AgHelpDataSet(Col1ReferenceNo) = Nothing
    End Sub

    Sub SetPersonWiseDiscounts(PartyCode As String)
        mQry = "Select IfNull(Max(DiscountPer),0) From PersonDiscount 
                        Where Person = '" & PartyCode & "' 
                        And ItemGroup Is Null 
                        And ItemCategory Is Null 
                        And Process Is Null 
                        And Div_Code Is Null 
                        And Site_Code Is Null "
        mPersonalDiscountPer = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()

        mQry = "Select IfNull(Max(AdditionalDiscountPer),0) From PersonAdditionalDiscount 
                        Where Person = '" & PartyCode & "' 
                        And ItemGroup Is Null 
                        And ItemCategory Is Null 
                        And Process Is Null 
                        And Div_Code Is Null 
                        And Site_Code Is Null "
        mPersonalAdditionalDiscountPer = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()

        mQry = "Select IfNull(Max(AdditionPer),0) From PersonAddition 
                        Where Person = '" & PartyCode & "' 
                        And ItemGroup Is Null 
                        And ItemCategory Is Null 
                        And Process Is Null 
                        And Div_Code Is Null 
                        And Site_Code Is Null "
        mPersonalAdditionalPer = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()

    End Sub

    Private Sub FGetCurrBal(ByVal Party As String)
        mQry = " Select IfNull(Sum(AmtDr),0) - IfNull(Sum(AmtCr),0) As CurrBal From Ledger  With (NoLock) Where SubCode = '" & Party & "'"
        TxtCurrBal.Text = Format(AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar), "0.00")

        If Val(TxtCurrBal.Text) < 0 Then
            TxtCurrBal.ForeColor = Color.Red
            TxtCurrBal.Text = TxtCurrBal.Text & " Cr."
        ElseIf Val(TxtCurrBal.Text) < 0 Then
            TxtCurrBal.ForeColor = Color.ForestGreen
            TxtCurrBal.Text = TxtCurrBal.Text & " Dr."
        Else
            TxtCurrBal.ForeColor = Color.Black
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from SaleInvoiceSetting With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from SaleInvoiceSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from SaleInvoiceSetting  With (NoLock) Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    End If
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found, Can not continue.")
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If


        TxtStructure.AgSelectedValue = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GcnRead).ExecuteScalar 'AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = EntryNCat

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        BtnFillPartyDetail.Tag = Nothing

        IniGrid()
        'ApplyUISettings(LblV_Type.Tag)
        TabControl1.SelectedTab = TP1
        'TxtRateType.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupParty"))
        'AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtRateType.AgSelectedValue
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

        'TxtGodown.Tag = DtV_TypeSettings.Rows(0)("DEFAULT_Godown")
        'TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" Select Description From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GCn).ExecuteScalar)


        mDimensionSrl = 0
        Dgl1.ReadOnly = False

        If AgL.XNull(DtV_TypeSettings.Rows(0)("SaleInvoicePattern")) = SaleInvoicePattern.PointOfSale Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RevenuePoint")) <> "" Then
                TxtSaleToParty.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RevenuePoint"))
                TxtSaleToParty.Text = AgL.Dman_Execute("Select Name From viewHelpSubgroup  With (NoLock) Where Code = '" & TxtSaleToParty.Tag & "'", AgL.GCn).ExecuteScalar
                Validating_SaleToParty(TxtSaleToParty.Tag, False)
                If TxtBarcode.Visible = True And TxtBarcode.Enabled = True Then
                    TxtBarcode.Focus()
                Else
                    Dgl1.Focus()
                End If
            End If
        End If

        SetAttachmentCaption()

        'If TxtV_Type.AgHelpDataSet.Tables(0).Rows.Count > 1 Then
        '    If AgL.PubServerName <> "" Then
        '        mQry = "SELECT TOP 1 H.V_Type, Vt.Description AS V_TypeDesc  
        '        FROM SaleInvoice H
        '        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
        '        WHERE H.EntryBy = '" & AgL.PubUserName & "' 
        '        And Vt.NCat In ('" & EntryNCat & "')
        '        ORDER BY H.Ent H.V_No Desc  "
        '    Else
        '        mQry = "SELECT H.V_Type, Vt.Description AS V_TypeDesc  
        '        FROM SaleInvoice H
        '        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
        '        WHERE H.EntryBy = '" & AgL.PubUserName & "' 
        '        And Vt.NCat In ('" & EntryNCat & "')
        '        ORDER BY H.EntryDate DESC Limit 1 "
        '    End If
        '    Dim DtLastVoucher_Type As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        '    If DtLastVoucher_Type.Rows.Count > 0 Then
        '        TxtV_Type.Tag = AgL.XNull(DtLastVoucher_Type.Rows(0)("V_Type"))
        '        TxtV_Type.Text = AgL.XNull(DtLastVoucher_Type.Rows(0)("V_TypeDesc"))
        '    End If
        'End If
    End Sub

    Private Sub Validating_ItemCode(ItemCode As String, ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtItem As DataTable = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim DtBarcodeSiteDetail As DataTable = Nothing
        Dim StrReturnTicked As String = ""
        Dim dtInvoices As DataTable = Nothing
        Try




            mQry = "Select I.Code, I.Description, I.ManualCode, I.Unit, IfNull(I.SalesTaxPostingGroup, IC.SalesTaxGroup) as SalesTaxPostingGroup, I.MaintainStockYn, 
                    I.ItemCategory, I.ItemGroup, IC.Description as ItemCategoryName, I.ItemType, IT.Name as ItemTypeName, IG.Description as ItemGroupName,
                    U.ShowDimensionDetailInSales, U.DecimalPlaces as QtyDecimalPlaces, IG.Default_DiscountPerSale ,
                    IG.Default_AdditionalDiscountPerSale, IG.Default_AdditionPerSale, I.PurchaseRate,
                    IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase, I.SalesAc, SAC.Name as SalesAcName
                                From Item I  With (NoLock)
                                Left Join Unit U  With (NoLock) On I.Unit = U.Code 
                                Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                                Left Join ItemType IT With (NoLock) On I.ItemType = IT.Code
                                Left Join viewHelpSubgroup SAC On I.SalesAc = SAC.Code
                                Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Code"))
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtItem.Rows(0)("Description"))
                Call FCheckDuplicate(mRow)
                Dgl1.Item(Col1ItemType, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemType"))
                Dgl1.Item(Col1ItemType, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemTypeName"))
                Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))

                Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Code"))
                Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtItem.Rows(0)("ManualCode"))
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                Dgl1.Item(Col1MaintainStockYn, mRow).Value = AgL.VNull(DtItem.Rows(0)("MaintainStockYn"))
                Dgl1.Item(Col1SalesAc, mRow).Value = AgL.XNull(DtItem.Rows(0)("SalesAcName"))
                Dgl1.Item(Col1SalesAc, mRow).Tag = AgL.XNull(DtItem.Rows(0)("SalesAc"))


                Dgl1.Item(Col1PurchaseRate, mRow).Value = AgL.VNull(DtItem.Rows(0)("PurchaseRate"))
                Dgl1.Item(Col1PurchaseDiscountPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("Default_DiscountPerPurchase"))
                Dgl1.Item(Col1PurchaseAdditionalDiscountPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("Default_AdditionalDiscountPerPurchase"))

                Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("SalesTaxPostingGroup"))
                Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("SalesTaxPostingGroup"))
                If Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = "" Then
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                End If
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtItem.Rows(0)("QtyDecimalPlaces"))

                If Dgl2(Col1Value, rowRateType).Value <> "" Then
                    mQry = "Select IfNull(Max(DiscountPer),0) As Default_DiscountPerSale,
                            IfNull(Max(AdditionalDiscountPer),0) As Default_AdditionalDiscountPerSale,
                            IfNull(Max(AdditionPer),0) As Default_AdditionPerSale
                            From ItemGroupRateType H  With (NoLock) 
                            Where Code = '" & Dgl1.Item(Col1ItemGroup, mRow).Tag & "' 
                            And RateType = '" & Dgl2(Col1Value, rowRateType).Tag & "' "
                    Dim DTDiscounts As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DTDiscounts.Rows.Count > 0 Then
                        Dgl1.Item(Col1DefaultDiscountPer, mRow).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_DiscountPerSale"))
                        Dgl1.Item(Col1DefaultAdditionalDiscountPer, mRow).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionalDiscountPerSale"))
                        Dgl1.Item(Col1DefaultAdditionPer, mRow).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionPerSale"))
                    End If

                    If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") = True Then
                        Dgl1.Item(Col1DefaultDiscountPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("Default_DiscountPerSale"))
                    End If

                    'Dgl1.Item(Col1DefaultDiscountPer, mRow).Value = AgL.Dman_Execute("Select IfNull(Max(Discount),0) From ItemGroupRateType H  With (NoLock) Where Code = '" & Dgl1.Item(Col1ItemGroup, mRow).Tag & "' And RateType = '" & Dgl2(Col1Value, rowRateType).Tag & "' ", AgL.GCn).ExecuteScalar
                Else
                    Dgl1.Item(Col1DefaultDiscountPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("Default_DiscountPerSale"))
                    Dgl1.Item(Col1DefaultAdditionalDiscountPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("Default_AdditionalDiscountPerSale"))
                    Dgl1.Item(Col1DefaultAdditionPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("Default_AdditionPerSale"))
                End If




                Dim DrItemTypeSetting As DataRow
                DrItemTypeSetting = FItemTypeSettings(Dgl1(Col1ItemType, mRow).Tag)
                Dgl1(Col1DiscountCalculationPattern, mRow).Value = AgL.XNull(DrItemTypeSetting("DiscountCalculationPatternSale"))
                Dgl1(Col1AdditionalDiscountCalculationPattern, mRow).Value = AgL.XNull(DrItemTypeSetting("AdditionalDiscountCalculationPatternSale"))
                Dgl1(Col1AdditionCalculationPattern, mRow).Value = AgL.XNull(DrItemTypeSetting("AdditionCalculationPatternSale"))


                If LblV_Type.Tag = Ncat.SaleReturn Then
                    If Dgl1.Item(Col1Barcode, mRow).Tag <> "" Then
                        mQry = "Select B.LastTrnDocID, B.LastTrnSr, 
                                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as ReferenceNo,
                                L.DocQty, L.Qty, L.MasterSaleRate, L.RateDiscountPer, L.Rate, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount,
                                L.DocId As SaleInvoice, L.Sr As SaleInvoiceSr                                                
                                From BarcodeSiteDetail B  With (NoLock) 
                                Left Join SaleInvoiceDetail L  With (NoLock) On B.LastTrnDocID = L.DocID And B.LastTrnSr = L.Sr
                                Left Join SaleInvoice H On L.DocID = H.DocID
                                Left Join Voucher_Type VT On H.V_Type = Vt.V_Type
                                Where B.Code = '" & Dgl1.Item(Col1Barcode, mRow).Tag & "' 
                                And B.Div_Code = '" & TxtDivision.Tag & "' And B.Site_Code = '" & TxtSite_Code.Tag & "' "
                        DtBarcodeSiteDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtBarcodeSiteDetail.Rows.Count > 0 Then
                            Dgl1.Item(Col1ReferenceDocId, mRow).Value = AgL.XNull(DtBarcodeSiteDetail.Rows(0)("LastTrnDocID"))
                            Dgl1.Item(Col1ReferenceDocIdSr, mRow).Value = AgL.XNull(DtBarcodeSiteDetail.Rows(0)("LastTrnSr"))
                            Dgl1.Item(Col1ReferenceNo, mRow).Value = AgL.XNull(DtBarcodeSiteDetail.Rows(0)("ReferenceNo"))
                            Dgl1.Item(Col1SaleInvoice, mRow).Tag = AgL.XNull(DtBarcodeSiteDetail.Rows(0)("SaleInvoice"))
                            Dgl1.Item(Col1SaleInvoiceSr, mRow).Value = AgL.XNull(DtBarcodeSiteDetail.Rows(0)("SaleInvoiceSr"))
                            Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("DocQty"))
                            Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("Qty"))
                            Dgl1.Item(Col1MasterSaleRate, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("MasterSaleRate"))
                            Dgl1.Item(Col1RateDiscountPer, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("RateDiscountPer"))
                            Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("Rate"))
                            Dgl1.Item(Col1DiscountPer, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("DiscountPer"))
                            Dgl1.Item(Col1DiscountAmount, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("DiscountAmount"))
                            Dgl1.Item(Col1AdditionalDiscountPer, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("AdditionalDiscountPer"))
                            Dgl1.Item(Col1AdditionalDiscountAmount, mRow).Value = AgL.VNull(DtBarcodeSiteDetail.Rows(0)("AdditionalDiscountAmount"))
                        End If

                    Else
                        'If AgL.XNull(Dgl2.Item(Col1Value, rowPartyDocNo).Value) = "" Then
                        StrReturnTicked = FHPGD_PendingSaleChallan(ItemCode)
                        If StrReturnTicked <> "" Then
                            FillGridForSaleReturn(StrReturnTicked, True)
                        Else
                            If MsgBox("No Invoice found to return for selected customer. Do you want to continue without invoice references?", vbYesNo) = MsgBoxResult.No Then
                                Dgl1.Rows(Dgl1.CurrentCell.RowIndex).Visible = False
                                StrReturnTicked = "."
                                Dgl1.Rows.Add()
                                'Else
                                '    If mRow = 0 Then
                                '        If AgL.PubServerName <> "" Then
                                '            If AgL.PubServerName = "" Then
                                '                mQry = "Select '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo ManualRefNo, H.DocID, H.V_Date 
                                '                    From SaleInvoice H  With (NoLock) 
                                '                    Left Join Voucher_Type VT On H.V_Type = VT.V_Type
                                '                    Where H.SaleToParty = '" & TxtSaleToParty.Tag & "' And VT.NCAT = '" & Ncat.SaleInvoice & "'  And H.V_Date > DateAdd(D,-15,H.V_Date) Limit 1"
                                '            Else
                                '                mQry = "Select Top 1 '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as ManualRefNo, H.DocID, H.V_Date 
                                '                    From SaleInvoice H  With (NoLock) 
                                '                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type
                                '                    Where H.SaleToParty = '" & TxtSaleToParty.Tag & "' And VT.NCAT = '" & Ncat.SaleInvoice & "' And H.V_Date > DateAdd(D,-15,H.V_Date) "
                                '            End If

                                '            dtInvoices = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
                                '            If dtInvoices.Rows.Count > 0 Then
                                '                Dgl1.Item(Col1ReferenceDocId, mRow).Value = AgL.XNull(dtInvoices.Rows(0)("DocID"))
                                '                Dgl1.Item(Col1ReferenceNo, mRow).Value = AgL.XNull(dtInvoices.Rows(0)("ManualRefNo"))
                                '                Dgl1.Item(Col1ReferenceDate, mRow).Value = ClsMain.FormatDate(AgL.XNull(dtInvoices.Rows(0)("V_Date")))
                                '            End If
                                '        End If
                                '        Dgl1.Item(Col1DocQty, mRow).Value = 1
                                '        Dgl1.Item(Col1Qty, mRow).Value = 1
                                '    Else
                                '        Dgl1.Item(Col1ReferenceDocId, mRow).Value = AgL.XNull(Dgl1.Item(Col1ReferenceDocId, mRow - 1).Value)
                                '        Dgl1.Item(Col1ReferenceNo, mRow).Value = AgL.XNull(Dgl1.Item(Col1ReferenceNo, mRow - 1).Value)
                                '        Dgl1.Item(Col1ReferenceDate, mRow).Value = AgL.XNull(Dgl1.Item(Col1ReferenceDate, mRow - 1).Value)
                                '    End If
                            End If
                        End If
                        'End If
                    End If
                End If

                If StrReturnTicked = "" Then
                    Dgl1.Item(Col1Unit, mRow).Tag = AgL.VNull(DtItem.Rows(0)("ShowDimensionDetailInSales"))



                    Dgl1.Item(Col1DocQty, mRow).Tag = Nothing
                    If (Dgl1.Item(Col1Unit, mRow).Tag) Then
                        Dgl1.Item(Col1DocQty, mRow).Style.ForeColor = Color.Blue
                        ShowSaleInvoiceDimensionDetail(mRow)
                    End If

                    If Dgl2(Col1Value, rowRateType).Value <> "" Then
                        mQry = "select Rate from RateListDetail  With (NoLock) where Item ='" & Dgl1.Item(Col1Item, mRow).Tag & "' and RateType='" & Dgl2(Col1Value, rowRateType).Tag & "'"
                        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtItem.Rows.Count > 0 Then
                            Dgl1.Item(Col1MasterSaleRate, mRow).Value = AgL.VNull(DtItem.Rows(0)("Rate"))
                            Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtItem.Rows(0)("Rate"))
                        End If
                    Else
                        mQry = "select Rate from RateListDetail  With (NoLock) where Item ='" & Dgl1.Item(Col1Item, mRow).Tag & "' and RateType Is Null"
                        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtItem.Rows.Count > 0 Then
                            Dgl1.Item(Col1MasterSaleRate, mRow).Value = AgL.VNull(DtItem.Rows(0)("Rate"))
                            Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtItem.Rows(0)("Rate"))
                        End If
                    End If

                    FSetSalesTaxGroupItemBasedOnRate(mRow)
                    FSetPersonalDiscount(mRow)
                End If

                If LblV_Type.Tag = Ncat.SaleInvoice And
                    CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsApplicable_SaleOrder")), Boolean) = True Then
                    If Dgl1.AgDataRow IsNot Nothing Then
                        If Dgl1.AgDataRow.Cells.Contains(Dgl1.AgDataRow.Cells("SaleInvoiceSr")) Then
                            Dgl1.Item(Col1SaleInvoiceSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("SaleInvoiceSr").Value)
                        Else
                            Dgl1.Item(Col1SaleInvoiceSr, mRow).Value = 1
                        End If
                    Else
                        Dgl1.Item(Col1SaleInvoiceSr, mRow).Value = 1
                    End If
                End If


                FShowTransactionHistory(ItemCode)

                Dgl1.Item(Col1CurrentStock, mRow).Value = ClsMain.FGetCurrentStock(ItemCode, Dgl1.Item(Col1Godown, mRow).Tag, TxtV_Date.Text)
                LblCurrentStock.Text = Val(Dgl1.Item(Col1CurrentStock, mRow).Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub FSetPersonalDiscount(mRow As Integer)
        Dim DtItem As DataTable


        If TxtNature.Text.ToUpper <> "CASH" Then
            If mPersonalAdditionalPer > 0 Then
                Dgl1.Item(Col1DefaultAdditionPer, mRow).Value = Format(mPersonalAdditionalPer, "0.000")
            End If

            mQry = "Select * 
                        from ItemGroupPerson With (NoLock) 
                        Where (ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRow).Tag & "' Or ItemCategory Is Null)
                        And ItemGroup  = '" & Dgl1.Item(Col1ItemGroup, mRow).Tag & "'
                        And Person  = '" & TxtSaleToParty.Tag & "'
                       "
            DtItem = AgL.FillData(mQry, AgL.GCn).tables(0)
            If DtItem.Rows.Count > 0 Then
                If AgL.VNull(DtItem.Rows(0)("DiscountPer")) > 0 Then
                    If Dgl1(Col1DiscountCalculationPattern, mRow).Value.ToString.ToUpper() = AgL.XNull(DtItem.Rows(0)("DiscountCalculationPattern")).toupper() Or Dgl1(Col1DiscountCalculationPattern, mRow).Value.ToString.ToUpper() = "" Then
                        If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") = False Then
                            Dgl1.Item(Col1PersonalDiscountPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("DiscountPer"))
                        End If

                    Else
                        MsgBox("Discount Calculation Pattern is changes since last invoice.")
                    End If
                End If

                If AgL.VNull(DtItem.Rows(0)("AdditionalDiscountPer")) > 0 Then
                    If Dgl1(Col1AdditionalDiscountCalculationPattern, mRow).Value.ToString.ToUpper() = AgL.XNull(DtItem.Rows(0)("AdditionalDiscountCalculationPattern")).toupper() Or Dgl1(Col1AdditionalDiscountCalculationPattern, mRow).Value.ToString.ToUpper() = "" Then
                        Dgl1.Item(Col1PersonalAdditionalDiscountPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("AdditionalDiscountPer"))
                    Else
                        MsgBox("Additional Discount Calculation Pattern is changes since last invoice.")
                    End If
                End If

                If AgL.VNull(DtItem.Rows(0)("AdditionPer")) > 0 Then
                    If Dgl1(Col1AdditionCalculationPattern, mRow).Value.ToString.ToUpper() = AgL.XNull(DtItem.Rows(0)("AdditionCalculationPattern")).toupper() Or Dgl1(Col1AdditionCalculationPattern, mRow).Value.ToString.ToUpper() = "" Then
                        Dgl1.Item(Col1PersonalAdditionPer, mRow).Value = AgL.VNull(DtItem.Rows(0)("AdditionPer"))
                    Else
                        MsgBox("Additional Discount Calculation Pattern is changes since last invoice.")
                    End If
                End If
            End If
        End If


        If AgL.XNull(DtV_TypeSettings.Rows(0)("DiscountSuggestionPattern")).ToUpper() = DiscountSuggestPattern.FillAutomatically.ToUpper Then

            If Val(Dgl1.Item(Col1PersonalDiscountPer, mRow).Value) <> 0 Then
                Dgl1.Item(Col1DiscountPer, mRow).Value = Format(Val(Dgl1.Item(Col1PersonalDiscountPer, mRow).Value), "0.000")
            Else
                Dgl1.Item(Col1DiscountPer, mRow).Value = Format(Val(Dgl1.Item(Col1DefaultDiscountPer, mRow).Value), "0.000")
            End If

            If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") = False Then
                If Val(Dgl1.Item(Col1PersonalAdditionalDiscountPer, mRow).Value) <> 0 Then
                    Dgl1.Item(Col1AdditionalDiscountPer, mRow).Value = Format(Val(Dgl1.Item(Col1PersonalAdditionalDiscountPer, mRow).Value), "0.000")
                Else
                    Dgl1.Item(Col1AdditionalDiscountPer, mRow).Value = Format(Val(Dgl1.Item(Col1DefaultAdditionalDiscountPer, mRow).Value), "0.000")
                End If
            End If

            If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") = True And LblV_Type.Tag = Ncat.SaleReturn Then
            Else
                If Val(Dgl1.Item(Col1PersonalAdditionPer, mRow).Value) <> 0 Then
                    Dgl1.Item(Col1AdditionPer, mRow).Value = Format(Val(Dgl1.Item(Col1PersonalAdditionPer, mRow).Value), "0.000")
                Else
                    Dgl1.Item(Col1AdditionPer, mRow).Value = Format(Val(Dgl1.Item(Col1DefaultAdditionPer, mRow).Value), "0.000")
                End If
            End If
        End If
    End Sub

    Private Sub FSetSalesTaxGroupItemBasedOnRate(mRowIndex As Integer)
        Dim DtMain As DataTable
        If Dgl1.Item(Col1ItemCategory, mRowIndex).Tag <> "" And Val(Dgl1.Item(Col1Rate, mRowIndex).Value) > 0 Then
            If AgL.PubServerName = "" Then
                mQry = "Select SalesTaxGroupItem From ItemCategorySalesTax  With (NoLock)
                Where Code='" & Dgl1.Item(Col1ItemCategory, mRowIndex).Tag & "' 
                And RateGreaterThan < " & Val(Dgl1.Item(Col1Rate, mRowIndex).Value) & " 
                And Date(WEF) <= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & " 
                Order By WEF Desc, RateGreaterThan Desc Limit 1"
            Else
                mQry = "Select Top 1 SalesTaxGroupItem From ItemCategorySalesTax  With (NoLock)
                Where Code='" & Dgl1.Item(Col1ItemCategory, mRowIndex).Tag & "' 
                And RateGreaterThan < " & Val(Dgl1.Item(Col1Rate, mRowIndex).Value) & " 
                And Date(WEF) <= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & " 
                Order By WEF Desc, RateGreaterThan Desc"
            End If
            DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtMain.Rows.Count > 0 Then
                Dgl1.Item(Col1SalesTaxGroup, mRowIndex).Value = AgL.XNull(DtMain.Rows(0)("SalesTaxGroupItem"))
                Dgl1.Item(Col1SalesTaxGroup, mRowIndex).Tag = AgL.XNull(DtMain.Rows(0)("SalesTaxGroupItem"))
            End If
        End If
    End Sub


    Private Sub Validating_ItemCategory(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try
            Dgl1.Item(Col1ItemGroup, mRow).Value = ""
            Dgl1.Item(Col1ItemGroup, mRow).Tag = ""
            Dgl1.Item(Col1Item, mRow).Value = ""
            Dgl1.Item(Col1Item, mRow).Tag = ""
            Dgl1.Item(Col1Unit, mRow).Value = ""
            Dgl1.Item(Col1Dimension1, mRow).Value = ""
            Dgl1.Item(Col1Dimension1, mRow).Tag = ""
            Dgl1.Item(Col1Dimension2, mRow).Value = ""
            Dgl1.Item(Col1Dimension2, mRow).Tag = ""

            Dgl1.AgHelpDataSet(Col1ItemGroup) = Nothing
            Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_ItemCategory Function ")
        End Try
    End Sub

    Private Sub Validating_ItemGroup(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try
            Dgl1.Item(Col1Item, mRow).Value = ""
            Dgl1.Item(Col1Item, mRow).Tag = ""
            Dgl1.Item(Col1Unit, mRow).Value = ""
            Dgl1.Item(Col1Dimension1, mRow).Value = ""
            Dgl1.Item(Col1Dimension1, mRow).Tag = ""
            Dgl1.Item(Col1Dimension2, mRow).Value = ""
            Dgl1.Item(Col1Dimension2, mRow).Tag = ""

            Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_ItemGroup Function ")
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name

                Case Col1Item, Col1ItemCode
                    Validating_ItemCode(Dgl1.Item(mColumnIndex, mRowIndex).Tag, mColumnIndex, mRowIndex)
                Case Col1ItemCategory
                    Validating_ItemCategory(mColumnIndex, mRowIndex)
                Case Col1ItemGroup
                    Validating_ItemGroup(mColumnIndex, mRowIndex)
                Case Col1MasterSaleRate
                    Dgl1.Item(Col1Rate, mRowIndex).Value = Val(Dgl1.Item(Col1MasterSaleRate, mRowIndex).Value)
                    FSetSalesTaxGroupItemBasedOnRate(mRowIndex)
                Case Col1Rate
                    FSetSalesTaxGroupItemBasedOnRate(mRowIndex)
                Case Col1ReferenceNo
                    If Dgl1.Item(Col1ReferenceNo, mRowIndex).Tag <> "" Then
                        Dgl1.Item(Col1ReferenceDocId, mRowIndex).Value = Dgl1.Item(Col1ReferenceNo, mRowIndex).Tag
                        Dgl1.Item(Col1ReferenceDate, mRowIndex).Value = ClsMain.FormatDate(AgL.Dman_Execute("Select V_Date From SaleInvoice Where DocID = '" & Dgl1.Item(Col1ReferenceNo, mRowIndex).Tag & "' ", AgL.GCn).ExecuteScalar())
                    End If

            End Select
            Call Calculation()
            Call Calculation()


            If Val(TxtCreditLimit.Text) > 0 And LblV_Type.Tag = Ncat.SaleInvoice Then
                If Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) + IIf(Topctrl1.Mode = "Add", Val(TxtCurrBal.Text), 0) > Val(TxtCreditLimit.Text) Then
                    If AgL.PubUserName.ToUpper = "SA" Or AgL.PubUserName.ToUpper = "SUPER" Then
                        If Val(TxtCreditLimit.Tag) = 0 Then
                            If MsgBox("Total Balance Of " & TxtSaleToParty.Text & " Is Exceeding Its Credit Limit " & TxtCreditLimit.Text & "., Do you want to continue?", vbYesNo) = MsgBoxResult.Yes Then
                                TxtCreditLimit.Tag = 1
                            End If
                        End If
                    Else
                        If AgL.XNull(AgL.PubDtEnviro.Rows(0)("ActionIfCreditLimitExceeds")) = ActionIfCreditLimitExceeds.AlertAndStopTransaction Then
                            MsgBox("Total Balance Of " & TxtSaleToParty.Text & " Is Exceeding Its Credit Limit " & TxtCreditLimit.Text & ".")
                        ElseIf AgL.XNull(AgL.PubDtEnviro.Rows(0)("ActionIfCreditLimitExceeds")) = ActionIfCreditLimitExceeds.AlertAndAskToContinue Then
                            If Val(TxtCreditLimit.Tag) = 0 Then
                                If MsgBox("Total Balance Of " & TxtSaleToParty.Text & " Is Exceeding Its Credit Limit " & TxtCreditLimit.Text & "., Do you want to continue?", vbYesNo) = MsgBoxResult.Yes Then
                                    TxtCreditLimit.Tag = 1
                                End If
                            End If
                        Else
                            MsgBox("Total Balance Of " & TxtSaleToParty.Text & " Is Exceeding Its Credit Limit " & TxtCreditLimit.Text & ".")
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation

        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub


        'On Editing Transaction Time settings should be used instead of Current Settings


        LblTotalPcs.Text = 0
        LblTotalQty.Text = 0
        LblDealQty.Text = 0
        LblTotalBale.Text = 0
        LblTotalAmount.Text = 0


        For I = 0 To Dgl1.RowCount - 1

            If Dgl1(Col1AdditionalDiscountCalculationPattern, I).Value Is Nothing Then Dgl1(Col1AdditionalDiscountCalculationPattern, I).Value = ""
            If Dgl1(Col1AdditionCalculationPattern, I).Value Is Nothing Then Dgl1(Col1AdditionCalculationPattern, I).Value = ""

            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible Then

                Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value) + Val(Dgl1.Item(Col1FreeQty, I).Value)


                If Val(Dgl1.Item(Col1UnitMultiplier, I).Value) <> 0 Then
                    Dgl1.Item(Col1DealQty, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1UnitMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value) + 2, "0"))
                End If


                If Val(Dgl1.Item(Col1RateDiscountPer, I).Value) <> 0 Then
                    Dgl1.Item(Col1Rate, I).Value = Format(Math.Round(Val(Dgl1.Item(Col1MasterSaleRate, I).Value) - (Val(Dgl1.Item(Col1MasterSaleRate, I).Value) * Val(Dgl1.Item(Col1RateDiscountPer, I).Value) / 100)), "0.00")
                End If


                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                If Val(Dgl1.Item(Col1DiscountPer, I).Value) > 0 Or Dgl1.Columns(Col1DiscountAmount).ReadOnly = True Or Dgl1.Columns(Col1DiscountAmount).Visible = False Then
                    If Dgl1(Col1DiscountCalculationPattern, I).Value.ToUpper = DiscountCalculationPattern.RatePerQty.ToUpper Then
                        Dgl1.Item(Col1DiscountAmount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1DiscountPer, I).Value), "0.00")
                    Else
                        Dgl1.Item(Col1DiscountAmount, I).Value = Format(Val(Dgl1.Item(Col1Amount, I).Value) * Val(Dgl1.Item(Col1DiscountPer, I).Value) / 100, "0.00")
                    End If
                End If


                If Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) > 0 Or Dgl1.Columns(Col1AdditionalDiscountAmount).ReadOnly = True Or Dgl1.Columns(Col1AdditionalDiscountAmount).Visible = False Then
                    If AgL.XNull(Dgl1(Col1AdditionalDiscountCalculationPattern, I).Value).ToUpper = DiscountCalculationPattern.RatePerQty.ToUpper Then
                        Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value), "0.00")
                    ElseIf AgL.XNull(Dgl1(Col1AdditionalDiscountCalculationPattern, I).Value).ToUpper = DiscountCalculationPattern.Percentage.ToUpper Then
                        Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = Format(Val(Dgl1.Item(Col1Amount, I).Value) * Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) / 100, "0.00")
                    Else
                        Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = Format((Val(Dgl1.Item(Col1Amount, I).Value) - Val(Dgl1.Item(Col1DiscountAmount, I).Value)) * Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) / 100, "0.00")
                    End If
                End If


                If Val(Dgl1.Item(Col1AdditionPer, I).Value) > 0 Or Dgl1.Columns(Col1AdditionAmount).ReadOnly = True Or Dgl1.Columns(Col1AdditionAmount).Visible = False Then
                    If AgL.XNull(Dgl1(Col1AdditionCalculationPattern, I).Value).ToUpper = DiscountCalculationPattern.RatePerQty.ToUpper Then
                        Dgl1.Item(Col1AdditionAmount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1AdditionPer, I).Value), "0.00")
                    ElseIf AgL.XNull(Dgl1(Col1AdditionCalculationPattern, I).Value).ToUpper = DiscountCalculationPattern.Percentage.ToUpper Then
                        Dgl1.Item(Col1AdditionAmount, I).Value = Format(Val(Dgl1.Item(Col1Amount, I).Value) * Val(Dgl1.Item(Col1AdditionPer, I).Value) / 100, "0.00")
                    Else
                        Dgl1.Item(Col1AdditionAmount, I).Value = Format((Val(Dgl1.Item(Col1Amount, I).Value) - Val(Dgl1.Item(Col1DiscountAmount, I).Value) - Val(Dgl1.Item(Col1AdditionalDiscountAmount, I).Value)) * Val(Dgl1.Item(Col1AdditionPer, I).Value) / 100, "0.00")
                    End If
                End If


                Dgl1.Item(Col1Amount, I).Value = Val(Dgl1.Item(Col1Amount, I).Value) - Val(Dgl1.Item(Col1DiscountAmount, I).Value) - Val(Dgl1.Item(Col1AdditionalDiscountAmount, I).Value) + Val(Dgl1.Item(Col1AdditionAmount, I).Value)




                If AgL.StrCmp(Dgl1.Item(Col1V_Nature, I).Value, "RETURN") Then
                    Dgl1.Item(Col1Amount, I).Value = -Val(Dgl1.Item(Col1Amount, I).Value)
                End If

                'Footer Calculation
                Dim bQty As Double = 0
                If AgL.StrCmp(Dgl1.Item(Col1V_Nature, I).Value, "RETURN") Then
                    bQty = Val(Dgl1.Item(Col1Qty, I).Value)
                Else
                    bQty = -Val(Dgl1.Item(Col1Qty, I).Value)
                End If

                If Math.Abs(Val(Dgl1.Item(Col1MaintainStockYn, I).Value)) = 0 Then
                    Dgl1.Item(Col1Pcs, I).Value = 0
                ElseIf Dgl1.Item(Col1Unit, I).Value.ToString.ToUpper = "PCS" Then
                    Dgl1.Item(Col1Pcs, I).Value = Dgl1.Item(Col1Qty, I).Value
                ElseIf Val(Dgl1.Item(Col1Pcs, I).Value) = 0 And Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                    Dgl1.Item(Col1Pcs, I).Value = 1
                End If

                If AgL.XNull(Dgl1.Item(Col1ItemType, I).Tag) <> ItemTypeCode.ServiceProduct Then
                    LblTotalPcs.Text = Val(LblTotalPcs.Text) + Val(Dgl1.Item(Col1Pcs, I).Value)
                    LblTotalQty.Text = Val(LblTotalQty.Text) + bQty
                    LblDealQty.Text = Val(LblDealQty.Text) + Val(Dgl1.Item(Col1DealQty, I).Value)
                End If
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                LblTotalBale.Text += 1

                FFormatRateCells(I)
            End If
        Next

        If BtnFillPartyDetail.Tag IsNot Nothing Then
            AgCalcGrid1.AgPostingGroupSalesTaxParty = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowSalesTaxGroup).Value
            AgCalcGrid1.AgPlaceOfSupply = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowPlaceOfSupply).Value
        End If

        AgCalcGrid1.AgVoucherCategory = "SALES"

        AgCalcGrid1.Calculation()


        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblDealQty.Text = Val(LblDealQty.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)

        If BtnFillPartyDetail.Tag IsNot Nothing Then
            CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).InvoiceAmount = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If mFlag_Import = True Then Exit Sub
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean

        Dgl1.EndEdit()
        Dgl2.EndEdit()
        Dgl3.EndEdit()

        If AgL.RequiredField(TxtSaleToParty, LblBuyer.Text) Then passed = False : Exit Sub

        If ClsMain.IsPartyBlocked(TxtSaleToParty.Tag, LblV_Type.Tag) Then
            MsgBox("Party is blocked for " & TxtV_Type.Text & ". Can not continue.")
            passed = False : Exit Sub
        End If

        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
            If ClsMain.FAttachmentFound(mSearchCode) = True Then
                MsgBox("Attchment already found.Delete it.", MsgBoxStyle.Information)
                passed = False : Exit Sub
            End If
        End If

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If Val(TxtCreditLimit.Text) > 0 And LblV_Type.Tag = Ncat.SaleInvoice Then
            If Val(TxtCreditLimit.Tag) = 0 Then
                If Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) + IIf(Topctrl1.Mode = "Add", Val(TxtCurrBal.Text), 0) > Val(TxtCreditLimit.Text) Then
                    If AgL.PubUserName.ToUpper = "SA" Or AgL.PubUserName.ToUpper = "SUPER" Then
                        If MsgBox("Total Balance Of " & TxtSaleToParty.Name & " Is Exceeding Its Credit Limit " & TxtCreditLimit.Text & ". Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            passed = False : Exit Sub
                        End If
                    Else
                        MsgBox("Total Balance Of " & TxtSaleToParty.Name & " Is Exceeding Its Credit Limit " & TxtCreditLimit.Text & ".")
                        passed = False : Exit Sub
                    End If
                End If
            End If
        End If

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1Item, I).Value <> "" Then

                        If LblV_Type.Tag = Ncat.SaleReturn Then
                            If AgL.XNull(Dgl1.Item(Col1ReferenceNo, I).Value) = "" Or AgL.XNull(Dgl1.Item(Col1ReferenceNo, I).Value) = AgL.XNull(Dgl2.Item(Col1Value, rowPartyDocNo).Value) Then
                                Dgl1.Item(Col1ReferenceNo, I).Value = AgL.XNull(Dgl2.Item(Col1Value, rowPartyDocNo).Value)
                                Dgl1.Item(Col1ReferenceDate, I).Value = AgL.XNull(Dgl2.Item(Col1Value, rowPartyDocDate).Value)
                            End If
                        End If


                        If Val(Dgl1(ColSNo, I).Tag) > 0 Then
                            If Dgl1(Col1Item, I).Value = "" Then
                                MsgBox("Item is blank at Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                .CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                        End If


                        If Val(.Item(Col1DocQty, I).Value) = 0 Then
                            MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1DocQty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If

                        If Val(.Item(Col1Rate, I).Value) = 0 Then
                            If AgL.VNull(DtV_TypeSettings.Rows(0)("IsAllowedZeroRate")) = False Then
                                MsgBox("Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                .CurrentCell = .Item(Col1Rate, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                        End If

                        If AgL.VNull(DtV_TypeSettings.Rows(0)("IsAllowedNegativeStock")) = False Then
                            If Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                                Dim bItemStockQty As Double = 0
                                mQry = " Select IfNull(Sum(Qty_Rec), 0) - IfNull(Sum(Qty_Iss), 0) " &
                                          " FROM Stock  With (NoLock) " &
                                          " WHERE Item = '" & Dgl1.Item(Col1Item, I).Tag & "' " &
                                          " And DocId <> '" & mSearchCode & "'"
                                bItemStockQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                                If Val(bItemStockQty) < Val(Dgl1.Item(Col1Qty, I).Value) Then
                                    MsgBox(Dgl1.Item(Col1Item, I).Value & " Have Only " & bItemStockQty.ToString() & " Stock .")
                                    .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus() : MakeGridCurrentCellNothing(Dgl1.Name)
                                    passed = False : Exit Sub
                                End If

                            End If
                        End If

                        If LblV_Type.Tag = Ncat.SaleReturn Then
                            If AgL.XNull(Dgl1.Item(Col1ReferenceNo, I).Value) = "" Or AgL.XNull(Dgl1.Item(Col1ReferenceNo, I).Value) = Dgl2.Item(Col1Value, rowPartyDocNo).Value Then
                                Dgl1.Item(Col1ReferenceNo, I).Value = Dgl2.Item(Col1Value, rowPartyDocNo).Value
                                Dgl1.Item(Col1ReferenceDate, I).Value = Dgl2.Item(Col1Value, rowPartyDocDate).Value
                            End If


                            If AgL.XNull(.Item(Col1ReferenceNo, I).Value) = "" Then
                                MsgBox("Against Inv. No. is blank in some Rows")
                                Dgl2.CurrentCell = Dgl2(Col1Value, rowPartyDocNo) : Dgl2.Focus()
                                passed = False : Exit Sub

                                'MsgBox("Against Inv. No. is blank  at Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                '.CurrentCell = .Item(Col1ReferenceNo, I) : Dgl1.Focus()
                                'passed = False : Exit Sub
                            End If

                            If AgL.XNull(.Item(Col1ReferenceDate, I).Value) = "" Then
                                If AgL.XNull(Dgl2.Item(Col1Value, rowPartyDocNo).Value) = "" Then
                                    MsgBox("Against Inv. Date is blank  at Row No " & Dgl1.Item(ColSNo, I).Value & ". Can not Continue")
                                    .CurrentCell = .Item(Col1ReferenceDate, I) : Dgl1.Focus()
                                Else
                                    MsgBox("Against Inv. Date is blank. Can not continue.")
                                    Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowPartyDocDate) : Dgl2.Focus()
                                End If
                                passed = False : Exit Sub
                            End If
                        End If

                        If .Item(Col1SalesTaxGroup, I).Value = "" Or .Item(Col1SalesTaxGroup, I).Value = Nothing Then
                            MsgBox("Sales Tax Group is not defined for item " & Dgl1.Item(Col1Item, I).Value & ".Define it in master.", MsgBoxStyle.Information)
                            .CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If

                        If AgL.StrCmp(Dgl1.Item(Col1V_Nature, I).Value, "STOCK") Then
                            mQry = " Select IfNull(Sum(Qty_Rec), 0) - IfNull(Sum(Qty_Iss), 0) " &
                                          " FROM Stock  With (NoLock) " &
                                          " WHERE Item = '" & Dgl1.Item(Col1Item, I).Tag & "' " &
                                          " AND  ReferenceDocID = '" & Dgl1.Item(Col1ReferenceDocId, I).Tag & "' " &
                                          " And ReferenceDocIdSr = " & Val(Dgl1.Item(Col1ReferenceDocIdSr, I).Value) & "" &
                                          " And DocId <> '" & mSearchCode & "'"
                            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) < Val(Dgl1.Item(Col1Qty, I).Value) Then
                                MsgBox(" Balance Stock Of Item " & Dgl1.Item(Col1Item, I).Value & " In Purchase No " & Dgl1.Item(Col1ReferenceDocId, I).Value & " Is Less Then " & Dgl1.Item(Col1Qty, I).Value & "", MsgBoxStyle.Information)
                                .CurrentCell = .Item(Col1DocQty, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                        End If

                        If AgL.StrCmp(Dgl1.Item(Col1V_Nature, I).Value, "RETURN") Then
                            mQry = " Select IfNull(Sum(Qty), 0)  " &
                                    " FROM SaleInvoiceDetail L  With (NoLock) " &
                                    " WHERE L.Item = '" & Dgl1.Item(Col1Item, I).Tag & "' " &
                                    " AND L.SaleInvoice = '" & Dgl1.Item(Col1SaleInvoice, I).Tag & "' " &
                                    " AND L.SaleInvoiceSr = " & Val(Dgl1.Item(Col1SaleInvoiceSr, I).Value) & "" &
                                    " And DocId <> '" & mSearchCode & "'"
                            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) < Val(Dgl1.Item(Col1Qty, I).Value) Then
                                MsgBox(" Balance Stock Of Item " & Dgl1.Item(Col1Item, I).Value & " In Sale No " & Dgl1.Item(Col1SaleInvoice, I).Value & " Is Less Then " & Dgl1.Item(Col1Qty, I).Value & "", MsgBoxStyle.Information)
                                .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                        End If
                    End If
                End If
            Next
        End With


        If BtnHeaderDetail.Tag IsNot Nothing Then
            If CType(BtnHeaderDetail.Tag, FrmSaleInvoiceTransport).DataValidation() = False Then
                ShowSaleInvoiceHeader()
                passed = False : Exit Sub
            End If
        Else
            If FrmSaleInvoiceTransport.DataValidationForMainInvoice(mSearchCode, LblV_Type.Tag) = False Then
                ShowSaleInvoiceHeader()
                passed = False : Exit Sub
            End If
        End If



        If ValidateData_Barcode() = False Then Exit Sub

        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "SaleInvoice",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)

        If Not CheckDuplicateRef Then
            TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef

        If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowPanNo).Value) IsNot Nothing Then
            If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowPanNo).Value).ToString.Trim <> "" Then
                If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowPanNo).Value).ToString.Length <> 10 Then
                    MsgBox("Pan No. should be of 10 characters")
                    passed = False : Exit Sub
                End If
            End If
        End If


        If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowAadharNo).Value) IsNot Nothing Then
            If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowAadharNo).Value).ToString.Trim <> "" Then
                If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowAadharNo).Value).ToString.Length <> 12 Then
                    MsgBox("Aadhar No. should be of 12 characters")
                    passed = False : Exit Sub
                End If
            End If
        End If

        If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowSalesTaxNo).Value) IsNot Nothing Then
            If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowSalesTaxNo).Value).ToString.Trim <> "" Then
                If (CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).Dgl1.Item(FrmSaleInvoiceParty.Col1Value, FrmSaleInvoiceParty.rowSalesTaxNo).Value).ToString.Length <> 15 Then
                    MsgBox("GST No. should be of 15 characters")
                    passed = False : Exit Sub
                End If
            End If
        End If


        If AgL.XNull(DtV_TypeSettings.Rows(0)("SaleInvoicePattern")) = SaleInvoicePattern.PointOfSale And LblV_Type.Tag = Ncat.SaleInvoice Then
            If Val(CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).LblBalanceToReceipt.Text) > 0 Then
                ShowSaleInvoiceParty(mSearchCode, TxtSaleToParty.Tag, TxtNature.Text, True)
                If Val(CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).LblBalanceToReceipt.Text) <> 0 Then
                    MsgBox("Complete Amount is Not settled")
                    passed = False
                    Exit Sub
                End If
            End If
        End If


        If Dgl1.Columns(Col1Godown).Visible = False Then
            If Dgl2.Rows(rowGodown).Visible = True Then
                If AgL.XNull(Dgl2(Col1Value, rowGodown).Value) <> "" Then
                    For I = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(Dgl1(Col1Item, I).Value) <> "" Then
                            Dgl1(Col1Godown, I).Value = Dgl2(Col1Value, rowGodown).Value
                            Dgl1(Col1Godown, I).Tag = Dgl2(Col1Value, rowGodown).Tag
                        End If
                    Next
                End If
            End If
        End If


        If Math.Round(Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)), 0) <> Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) Then
            Calculation()
            Calculation()
        End If
    End Sub
    Private Sub MakeGridCurrentCellNothing(GridName As String)
        Try
            If GridName <> Dgl1.Name Then Dgl1.CurrentCell = Nothing
        Catch ex As Exception
        End Try
        Try
            If GridName <> Dgl2.Name Then Dgl2.CurrentCell = Nothing
        Catch ex As Exception
        End Try
        Try
            If GridName <> Dgl3.Name Then Dgl3.CurrentCell = Nothing
        Catch ex As Exception
        End Try
        Try
            If GridName <> AgCalcGrid1.Name Then AgCalcGrid1.CurrentCell = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Function ValidateData_Barcode() As Boolean
        Dim passed As Boolean = True
        Dim I As Integer
        Dim DtBarcodeLastValues As DataTable

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1Item, I).Value <> "" Then
                        If Dgl1.Item(Col1Barcode, I).Tag <> "" Then
                            If Val(Dgl1.Item(ColSNo, I).Tag) = 0 Then
                                mQry = "Select BSD.*, B.BarcodeType From BarcodeSiteDetail BSD With (NoLock) Left Join Barcode B With (NoLock) On BSD.Code = B.Code Where BSD.Code = '" & Dgl1.Item(Col1Barcode, I).Tag & "' And BSD.Div_Code = '" & TxtDivision.Tag & "' And BSD.Site_Code = '" & TxtSite_Code.Tag & "'"
                                DtBarcodeLastValues = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                If DtBarcodeLastValues.Rows.Count > 0 Then
                                    If LblV_Type.Tag = Ncat.SaleInvoice Then
                                        If AgL.XNull(DtBarcodeLastValues.Rows(0)("BarcodeType")) = BarcodeType.UniquePerPcs Then
                                            If Not AgL.XNull(DtBarcodeLastValues.Rows(0)("Status")) = BarcodeStatus.Receive Then
                                                MsgBox("Barcode " & Dgl1.Item(Col1Barcode, I).Value & " Status Is Not Receive. Can't Issue It.")
                                                passed = False : Exit Function
                                            End If
                                        End If
                                    ElseIf LblV_Type.Tag = Ncat.SaleReturn Then
                                        If AgL.XNull(DtBarcodeLastValues.Rows(0)("BarcodeType")) = BarcodeType.UniquePerPcs Then
                                            If Not AgL.XNull(DtBarcodeLastValues.Rows(0)("Status")) = BarcodeStatus.Issue Then
                                                MsgBox("Barcode " & Dgl1.Item(Col1Barcode, I).Value & " Status Is Not Issue. Can't Receive It.")
                                                passed = False : Exit Function
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End With

        ValidateData_Barcode = True
    End Function


    Private Function Validate_Barcode(BarcodeDescription As String) As Boolean
        Dim DtBarcodeLastValues As DataTable

        If CType(AgL.VNull(FGetSettings(SettingFields.AllowToCheckBarcodeStockYn, SettingType.General)), Boolean) = True Then
            mQry = "Select H.Qty, L.* From BarcodeSiteDetail L  With (NoLock) Left Join Barcode H  With (NoLock) On L.Code = H.Code Where H.Description = '" & BarcodeDescription & "' And L.Div_Code = '" & TxtDivision.Tag & "' And L.Site_Code = '" & TxtSite_Code.Tag & "'"
            DtBarcodeLastValues = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtBarcodeLastValues.Rows.Count > 0 Then
                If LblV_Type.Tag = Ncat.SaleInvoice Then
                    If AgL.VNull(DtBarcodeLastValues.Rows(0)("CurrentStock")) <= 0 Then
                        MsgBox("Barcode " & BarcodeDescription & " Is Not In Stock. Can't Issue It.")
                        Exit Function
                    End If
                    'If Not AgL.XNull(DtBarcodeLastValues.Rows(0)("Status")) = BarcodeStatus.Receive Then
                    '    MsgBox("Barcode " & BarcodeDescription & " Status Is Not Receive. Can't Issue It.")
                    '    Exit Function
                    'End If
                ElseIf LblV_Type.Tag = Ncat.SaleReturn Then
                    If AgL.VNull(DtBarcodeLastValues.Rows(0)("CurrentStock")) > 0 And AgL.VNull(DtBarcodeLastValues.Rows(0)("CurrentStock")) <> AgL.VNull(DtBarcodeLastValues.Rows(0)("Qty")) Then
                        MsgBox("Barcode " & BarcodeDescription & " Is Already In Our Stock. Can't Receive It.")
                        Exit Function
                    End If
                    'If Not AgL.XNull(DtBarcodeLastValues.Rows(0)("Status")) = BarcodeStatus.Issue Then
                    '        MsgBox("Barcode " & BarcodeDescription & " Status Is Not Issue. Can't Receive It.")
                    '        Exit Function
                    '    End If
                End If
            End If
        End If

        Validate_Barcode = True
    End Function


    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtSaleToParty.KeyDown, TxtBillToParty.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtSaleToParty.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            FCreateHelpSubgroup()
                        End If
                    End If

                Case TxtBillToParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            FCreateHelpBillToParty()
                            'TxtBillToParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = TxtSaleToParty.AgHelpDataSet
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        If mFlag_Import = True Then
            Dgl1.Rows.Clear()
        Else
            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        End If

        BtnFillPartyDetail.Tag = Nothing
        BtnHeaderDetail.Tag = Nothing
        BtnFillPartyDetail.Tag = Nothing

        UserMovedOverItemCategory = False
        UserMovedOverItemGroup = False
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If
        Try
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If AgL.VNull(Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Tag) And AgL.VNull(Dgl1.Item(Col1DocQty, Dgl1.CurrentCell.RowIndex).Value) <> 0 Then
                        Dgl1.CurrentCell.ReadOnly = True
                    Else
                        Dgl1.CurrentCell.ReadOnly = False
                    End If

                    If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
                        If LblV_Type.Tag = Ncat.SaleOrder Then
                            If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                                    From SaleInvoice H 
                                    LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                                    Where Vt.NCat = '" & Ncat.SaleInvoice & "'
                                    And L.SaleInvoice = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                                Dgl1.CurrentCell.ReadOnly = True
                            Else
                                Dgl1.CurrentCell.ReadOnly = False
                            End If
                        End If
                    End If


                Case Col1ItemCategory
                    UserMovedOverItemCategory = True
                    Try
                        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
                            If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value IsNot Nothing Then
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Tag
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value
                            End If
                        End If
                    Catch ex As Exception
                    End Try

                Case Col1ItemGroup
                    UserMovedOverItemGroup = True
                    Try
                        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
                            If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value IsNot Nothing Then
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Tag
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                Case Col1Qty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                    LblHelp.Visible = False
                Case Col1DocQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                    LblHelp.Visible = False
                    If AgL.XNull(Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Tag) <> "" Then
                        Dgl1.CurrentCell.ReadOnly = Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Tag
                    End If


                Case Col1UnitMultiplier, Col1DealQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1DealUnitDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                    LblHelp.Visible = False

                Case Col1Item
                    Try
                        If e.RowIndex > 0 Then
                            If Dgl1.Item(Col1V_Nature, e.RowIndex).Value = "" Then Dgl1.Item(Col1V_Nature, e.RowIndex).Value = Dgl1.Item(Col1V_Nature, e.RowIndex - 1).Value
                        Else
                            If Dgl1.Item(Col1V_Nature, e.RowIndex).Value = "" Then Dgl1.Item(Col1V_Nature, e.RowIndex).Value = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_V_Nature"))
                        End If
                        'FRotateV_Nature(e.RowIndex)
                        'FRotateOptionButtons(e.RowIndex)
                        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
                        LblHelp.Visible = False
                    Catch ex As Exception
                    End Try


                Case Col1V_Nature
                    LblHelp.Visible = True

                Case Else
                    LblHelp.Visible = False
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempSaleInvoice_BaseFunction_DispText() Handles Me.BaseFunction_DispText
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Topctrl1.Mode = "Browse" Then
            Select Case e.KeyCode
                Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
                Case Else
                    e.Handled = True
            End Select
            Exit Sub
        End If

        If e.KeyCode = Keys.Enter Then
            If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Item Then
                If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value = "" Then
                    If Dgl3.Visible Then
                        'Dgl3.CurrentCell = Dgl3(Col1Value, rowAgent)
                        Dgl3.CurrentCell = Dgl3.FirstDisplayedCell
                        Dgl3.Focus()
                    End If
                End If
            End If
        End If

        If AgL.StrCmp(AgL.PubDBName, "SHADHVINEW") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIKANPURB2") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIjaunpur") Or AgL.StrCmp(AgL.PubDBName, "SHADHVINANDI") Then
            Dgl1.Columns(Col1Rate).ReadOnly = True
        End If

        Dim bAllowRowDeletion As Boolean = True
        If LblV_Type.Tag = Ncat.SaleOrder Then
            If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                                    From SaleInvoice H 
                                    LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                                    Where Vt.NCat = '" & Ncat.SaleInvoice & "'
                                    And L.SaleInvoice = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                bAllowRowDeletion = False
            Else
                bAllowRowDeletion = True
            End If
        End If


        If e.Control And e.KeyCode = Keys.D Then
            If Val(Dgl1.Item(Col1IsRecordLocked, Dgl1.CurrentCell.RowIndex).Value) = 0 And bAllowRowDeletion = True Then
                sender.CurrentRow.visible = False
                Calculation()
            Else
                MsgBox("Row is locked.", MsgBoxStyle.Information)
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If Dgl1.CurrentCell IsNot Nothing Then
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    End If

            End Select
        End If

        If Dgl1.CurrentCell IsNot Nothing Then
            If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1V_Nature Then
                If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value = "" Then
                    Select Case e.KeyCode
                        Case Keys.D
                            Dgl1.Item(Col1V_Nature, Dgl1.CurrentCell.RowIndex).Value = "SALE"
                        Case Keys.S
                            Dgl1.Item(Col1V_Nature, Dgl1.CurrentCell.RowIndex).Value = "STOCK"
                        Case Keys.R
                            Dgl1.Item(Col1V_Nature, Dgl1.CurrentCell.RowIndex).Value = "RETURN"
                    End Select
                    Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag = ""
                    Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value = ""
                    Dgl1.AgHelpDataSet(Col1Item) = Nothing
                Else
                    If e.KeyCode = Keys.D Or e.KeyCode = Keys.O Or e.KeyCode = Keys.C Or e.KeyCode = Keys.S Or e.KeyCode = Keys.R Then
                        MsgBox("Can't Change Nature.First Remove Item From Line.", MsgBoxStyle.Information)
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub ShowSaleInvoiceDimensionDetail(mRow As Integer)
        If Dgl1.Item(Col1DocQty, mRow).Tag IsNot Nothing Then
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmSaleInvoiceDimension).EntryMode = Topctrl1.Mode
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmSaleInvoiceDimension).objFrmSaleInvoice = Me
            Dgl1.Item(Col1DocQty, mRow).Tag.ShowDialog()
            Dgl1.Item(Col1DocQty, mRow).Value = Format(CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmSaleInvoiceDimension).GetTotalQty, "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value) + 2, "0"))
            Dgl1.Item(Col1Qty, mRow).Value = Format(CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmSaleInvoiceDimension).GetTotalQty, "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value) + 2, "0"))
            Dgl1.Item(Col1Pcs, mRow).Value = Val(CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmSaleInvoiceDimension).GetTotalPcs)
        Else
            If Dgl1.Item(Col1Unit, mRow).Tag Then
                Dim FrmObj As FrmSaleInvoiceDimension
                FrmObj = New FrmSaleInvoiceDimension
                FrmObj.ItemName = Dgl1.Item(Col1Item, mRow).Value
                FrmObj.Unit = Dgl1.Item(Col1Unit, mRow).Value
                FrmObj.UnitDecimalPlace = Val(Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value)
                FrmObj.IniGrid(mSearchCode, Val(Dgl1.Item(ColSNo, mRow).Tag))
                FrmObj.EntryMode = Topctrl1.Mode
                FrmObj.objFrmSaleInvoice = Me
                Dgl1.Item(Col1DocQty, mRow).Tag = FrmObj

                Dgl1.Item(Col1DocQty, mRow).Tag.ShowDialog()

                Dgl1.Item(Col1DocQty, mRow).Value = Format(CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmSaleInvoiceDimension).GetTotalQty, "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value) + 2, "0"))
                Dgl1.Item(Col1Qty, mRow).Value = Format(CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmSaleInvoiceDimension).GetTotalQty, "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value) + 2, "0"))
                Dgl1.Item(Col1Pcs, mRow).Value = Val(CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmSaleInvoiceDimension).GetTotalPcs)
            End If
        End If
        Calculation()
    End Sub

    Private Sub FOpenItemMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        Dim objMdi As New MDIMain
        Dim StrUserPermission As String
        Dim DTUP As DataTable

        StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, objMdi.MnuItemMaster.Name, objMdi.MnuItemMaster.Text, DTUP)

        Dim frmObj As FrmItemMaster

        frmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.Item)
        frmObj.EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion
        frmObj.StartPosition = FormStartPosition.CenterParent
        frmObj.IniGrid()
        frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemCategory).Value = Dgl1.Item(Col1ItemCategory, RowIndex).Value
        frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemCategory).Tag = Dgl1.Item(Col1ItemCategory, RowIndex).Tag
        frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemGroup).Value = Dgl1.Item(Col1ItemGroup, RowIndex).Value
        frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemGroup).Tag = Dgl1.Item(Col1ItemGroup, RowIndex).Tag
        frmObj.FillRateTypeForItemGroup(frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemGroup).Tag)
        frmObj.ShowDialog()
        bItemCode = frmObj.mSearchCode
        frmObj = Nothing


        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1DocQty, RowIndex)

        FCreateHelpItem(RowIndex)
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemCode(bItemCode, ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1Item, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub FOpenItemCategoryMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Category Master", TxtV_Type.Tag)
        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1ItemGroup, RowIndex)
        'FCreateHelpItem(Dgl1.Columns(ColumnIndex).Name)
        FCreateHelpItemCategory()
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From ItemCategory  With (NoLock) Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemCategory(ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1ItemCategory, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub FOpenItemGroupMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Group Master", TxtV_Type.Tag)
        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1ItemGroup, RowIndex)
        'FCreateHelpItem(Dgl1.Columns(ColumnIndex).Name)
        FCreateHelpItemGroup(RowIndex)
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From ItemGroup  With (NoLock) Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemGroup(ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1ItemGroup, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuImportFromDos.Visible = False
            MnuImportFromExcel.Visible = False
            MnuImportFromTally.Visible = False
            MnuEditSave.Visible = False
        End If

        If LblV_Type.Tag <> Ncat.SaleInvoice Then
            MnuGenerateEWayBill.Visible = False
        End If
        'AgL.WinSetting(Me, 654, 990, 0, 0)
        'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub RbtInvoiceDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Dgl1.CurrentCell IsNot Nothing Then
                Select Case sender.Name

                End Select
            End If

            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer



        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemGroup) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemGroup).Dispose() : Dgl1.AgHelpDataSet(Col1ItemGroup) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemCategory) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCategory).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCategory) = Nothing

        If TxtSaleToParty.AgHelpDataSet IsNot Nothing Then TxtSaleToParty.AgHelpDataSet.Dispose() : TxtSaleToParty.AgHelpDataSet = Nothing
        If TxtBillToParty.AgHelpDataSet IsNot Nothing Then TxtBillToParty.AgHelpDataSet.Dispose() : TxtBillToParty.AgHelpDataSet = Nothing

        If Dgl1.AgHelpDataSet(Col1SaleInvoice) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1SaleInvoice).Dispose() : Dgl1.AgHelpDataSet(Col1SaleInvoice) = Nothing


        For i = 0 To Dgl2.Rows.Count - 1
            Dgl2(Col1Head, i).Tag = Nothing
        Next

        For i = 0 To Dgl3.Rows.Count - 1
            Dgl3(Col1Head, i).Tag = Nothing
        Next

        If AgL.XNull(TxtSaleToParty.Tag) <> "" Then
            mQry = "Select * From Subgroup  With (NoLock) Where Subcode = '" & TxtSaleToParty.Tag & "'"
            Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                Dgl3(Col1Value, rowCreditDays).Value = AgL.VNull(DtTemp.Rows(0)("CreditDays"))
                TxtCreditLimit.Text = Format(AgL.VNull(DtTemp.Rows(0)("CreditLimit")), "0.00")
            End If
        End If


        Dim TemporaryCreditLimit As Double = AgL.VNull(AgL.Dman_Execute("SELECT IfNull(L.Amount,0) As TotalCreditLimit
                    FROM SubgroupTemporaryCreditLimit L With (NoLock)
                    WHERE L.Subcode = '" & TxtSaleToParty.Tag & "' 
                    AND Date(L.FromDate) <= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & "
                    AND Date(L.ToDate) >= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & "
                    ", AgL.GCn).ExecuteScalar())
        If TemporaryCreditLimit > 0 Then
            'Dim CreditLimit As Double = AgL.VNull(AgL.Dman_Execute("SELECT IfNull(Sg.CreditLimit,0) As CreditLimit
            '        FROM SubGroup Sg 
            '        WHERE Sg.Subcode = '" & DglMain.Item(Col1Value, rowSaleToParty).Tag & "'", AgL.GCn).ExecuteScalar())
            'TxtCreditLimit.Text = CreditLimit + TemporaryCreditLimit
            TxtCreditLimit.Text = TemporaryCreditLimit
            LblCreditLimit.Tag = TemporaryCreditLimit
        End If
    End Sub

    Private Sub BtnFillPartyDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPartyDetail.Click
        If Topctrl1.Mode = "Add" Then
            ShowSaleInvoiceParty("", TxtSaleToParty.Tag, TxtNature.Text, True)
        Else
            ShowSaleInvoiceParty(mSearchCode, "", TxtNature.Text, True)
        End If
    End Sub

    'Private Sub FOpenPartyDetail()
    '    Dim FrmObj As FrmSaleInvoicePartyDetail
    '    Try
    '        If BtnFillPartyDetail.Tag Is Nothing Then
    '            FrmObj = New FrmSaleInvoicePartyDetail
    '            FrmObj.TxtSaleToPartyName.Text = "CASH"
    '        Else
    '            FrmObj = BtnFillPartyDetail.Tag
    '        End If
    '        FrmObj.DispText(IIf(Topctrl1.Mode = "Browse", False, True))
    '        FrmObj.ShowDialog()
    '        If FrmObj.mOkButtonPressed Then BtnFillPartyDetail.Tag = FrmObj
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub FGetUnitMultiplier(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            If Dgl1.Item(Col1Unit, mRow).Value <> "" And Dgl1.Item(Col1DealUnit, mRow).Value <> "" And Val(Dgl1.Item(Col1UnitMultiplier, mRow).Value) <> 0 Then
                mQry = " SELECT Multiplier, Rounding FROM UnitConversion  With (NoLock) WHERE FromUnit = '" & Dgl1.Item(Col1Unit, mRow).Value & "' AND ToUnit =  '" & Dgl1.Item(Col1DealUnit, mRow).Value & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                With DtTemp
                    If .Rows.Count > 0 Then
                        Dgl1.Item(Col1UnitMultiplier, mRow).Value = AgL.VNull(.Rows(0)("Multiplier"))
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGetBaleStr(ByVal SearchCode As String)
        Dim I As Integer
        Dim mBale As String = ""
        Dim tBale As Integer = 0
        Dim fBale As Integer = 0

        Dim DsTemp As DataSet

        mQry = "Select Distinct Convert(INT,BaleNo) as BaleNo From SaleInvoiceDetail  With (NoLock)  Where DocId = '" & SearchCode & "' And IsNumeric(BaleNo) = 1 Order By  Convert(INT,BaleNo) "
        DsTemp = AgL.FillData(mQry, AgL.GcnRead)
        With DsTemp.Tables(0)

            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    If fBale = 0 Then
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                        mBale = AgL.XNull(.Rows(I)("BaleNo"))
                    ElseIf fBale + 1 <> AgL.VNull(.Rows(I)("BaleNo")) Then
                        mBale = mBale & "-" & AgL.XNull(.Rows(I - 1)("BaleNo")) & ", " & AgL.XNull(.Rows(I)("BaleNo"))
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                    Else
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                    End If

                    If I = DsTemp.Tables(0).Rows.Count - 1 Then
                        If fBale <> AgL.VNull(.Rows(I)("BaleNo")) Then
                            mBale = mBale & ", " & AgL.XNull(.Rows(I)("BaleNo")) & ""
                        Else
                            mBale = mBale & "-" & AgL.XNull(.Rows(I)("BaleNo")) & ""
                        End If
                    End If
                Next I
            End If
        End With


        mQry = "Select Distinct BaleNo From SaleInvoiceDetail  With (NoLock)  Where DocId = '" & SearchCode & "' And IsNumeric(BaleNo) = 0 "
        DsTemp = AgL.FillData(mQry, AgL.GcnRead)
        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    If Dgl1.Item(Col1BaleNo, I).Value IsNot Nothing Then
                        If mBale = "" Then
                            mBale += Dgl1.Item(Col1BaleNo, I).Value.ToString
                        Else
                            mBale += "," & Dgl1.Item(Col1BaleNo, I).Value.ToString
                        End If
                    End If
                Next I
            End If
        End With
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim Mdi As MDIMain = New MDIMain
        Dim DtTemp As DataTable
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                'Case Col1SaleInvoice
                '    Call ClsMain.ProcOpenLinkForm(Mdi.MnuQCRequestEntry, Dgl1.Item(Col1SaleQCReq, e.RowIndex).Tag, Me.MdiParent)

                Case Col1ImportStatus
                    MsgBox(Dgl1.Item(Col1ImportStatus, e.RowIndex).ToolTipText, MsgBoxStyle.Information)

                Case Col1Progress
                    If AgL.PubServerName = "" Then
                        mQry = "Select group_concat(H.ManualRefNo,',') as ProgressNo From SaleInvoice H Left Join SaleInvoiceDetail L On H.DocID = L.DocID Where  L.SaleInvoice<>L.DocID And L.SaleInvoice || L.SaleInvoiceSr = '" & mSearchCode & "' || '" & Val(AgL.VNull(Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag)) & "' Group By H.DocID "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            MsgBox(" Invoice No. : " & AgL.XNull(DtTemp.Rows(0)("ProgressNo")))
                        End If
                    Else
                        mQry = "Select H.ManualRefNo + ',' as ProgressNo From SaleInvoice H With (NoLock) Left Join SaleInvoiceDetail L With (NoLock) On H.DocID = L.DocID Where L.SaleInvoice<> L.DocID And L.SaleInvoice = '" & mSearchCode & "' And L.SaleInvoiceSr = " & Val(AgL.VNull(Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag)) & " Group By H.DocID,H.ManualRefNo  FOR XML path('')"
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            MsgBox(" Invoice No. : " & AgL.XNull(DtTemp.Rows(0)("ProgressNo")))
                        End If
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        If AgL.StrCmp(AgL.PubDBName, "SHADHVINEW") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIKANPURB2") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIjaunpur") Or AgL.StrCmp(AgL.PubDBName, "SHADHVINANDI") Then
            Dgl1.Columns(Col1Rate).ReadOnly = True
        End If
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode = Keys.Insert Then
                        Call FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    ElseIf e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            If LblV_Type.Tag = Ncat.SaleInvoice And
                                    CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsItemHelpFromSaleOrder")), Boolean) = True Then
                                FCreateHelpItemFromSaleOrder(Dgl1.CurrentCell.RowIndex)
                            Else
                                FCreateHelpItem(Dgl1.CurrentCell.RowIndex)
                            End If
                        End If
                    End If

                Case Col1ItemCategory
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemCategory) Is Nothing Then
                            FCreateHelpItemCategory()
                        End If
                    End If

                Case Col1ItemGroup
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                            If LblV_Type.Tag = Ncat.SaleInvoice And
                                CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsItemHelpFromSaleOrder")), Boolean) = True Then
                                FCreateHelpItemGroupFromSaleOrder(Dgl1.CurrentCell.RowIndex)
                            Else
                                FCreateHelpItemGroup(Dgl1.CurrentCell.RowIndex)
                            End If
                        End If
                    End If

                Case Col1ReferenceNo
                    If e.KeyCode <> Keys.Enter Then
                        If LblV_Type.Tag = Ncat.SaleReturn And Val(Dgl3.Item(Col1Value, rowCustomerInvoiceCount).Value) > 0 Then
                            If Dgl1.AgHelpDataSet(Col1ReferenceNo) Is Nothing Then
                                mQry = " SELECT H.DocID, '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as [Invoice No], H.V_Date as [Invoice Date]  
                                         FROM SaleInvoice H  With (NoLock)  
                                         Left Join Voucher_Type VT With (NoLock) On H.V_Type = VT.V_Type
                                         Where VT.Ncat = '" & Ncat.SaleInvoice & "' And H.SaleToParty = '" & TxtSaleToParty.Tag & "' 
                                         And H.Div_Code='" & TxtDivision.Tag & "' And H.Site_Code = '" & TxtSite_Code.Tag & "'  
                                         And Date(H.V_Date) <= " & AgL.Chk_Date(TxtV_Date.Text) & "  "
                                Dgl1.AgHelpDataSet(Col1ReferenceNo) = AgL.FillData(mQry, AgL.GCn)
                            End If
                        End If
                    End If

                Case Col1SalesTaxGroup
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = " SELECT H.Description as Code, H.Description FROM PostingGroupSalesTaxItem H  With (NoLock)  Where IfNull(H.Active,1) = 1 Order By H.Description  "
                            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1ItemState
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = " SELECT H.Code, H.Description FROM Item H  With (NoLock)  Where H.V_Type = '" & ItemV_Type.ItemState & "'  And IfNull(H.Status,'Active') = 'Active' Order By H.Description "
                            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


                Case Col1SaleInvoice
                    If e.KeyCode <> Keys.Enter Then
                        If LblV_Type.Tag = Ncat.SaleInvoice And
                                CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsApplicable_SaleOrder")), Boolean) = True Then
                            If Dgl1.AgHelpDataSet(Col1SaleInvoice) Is Nothing Then
                                FCreateHelpSaleOrder()
                            End If
                        End If
                    End If

                Case Col1SalesTaxGroup
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1SaleInvoice) Is Nothing Then
                            mQry = " Select Description As Code, Description  FROM PostingGroupSalesTaxParty "
                            Dgl1.AgHelpDataSet(Col1SaleInvoice) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FOpenMaster(ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim FrmObj As Object = Nothing
        Dim CFOpen As New ClsFunction()
        Dim DtTemp As DataTable = Nothing
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If e.KeyCode = Keys.Insert Then
                If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Item Then
                    If Not AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).Contains(",") Then
                        mQry = " Select MnuName, MnuText From ItemType  With (NoLock) Where Code = '" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            FrmObj = CFOpen.FOpen(DtTemp.Rows(0)("MnuName"), DtTemp.Rows(0)("MnuText"), True)
                            If FrmObj IsNot Nothing Then
                                FrmObj.MdiParent = Me.MdiParent
                                FrmObj.Show()
                                FrmObj.Topctrl1.FButtonClick(0)
                                FrmObj = Nothing
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleQuotation_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        Dim i As Integer

        GBoxImportFromExcel.Enabled = False

        For i = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(i).DefaultCellStyle.BackColor = Dgl1.AgReadOnlyColumnColor Then
                Dgl1.Columns(i).ReadOnly = True
            End If
        Next

        If EntryNCat = Ncat.SaleInvoice Or EntryNCat = Ncat.SaleOrder Then
            BtnHeaderDetail.Visible = True
        Else
            BtnHeaderDetail.Visible = False
        End If

        If EntryNCat = Ncat.SaleOrder Then
            LblV_Type.Text = "Order Type"
            LblReferenceNo.Text = "Order No"
            LblV_Date.Text = "Order Date"
        ElseIf EntryNCat = Ncat.SaleReturn Then
            LblV_Type.Text = "Return Type"
            LblReferenceNo.Text = "Return No"
            LblV_Date.Text = "Return Date"
        End If

        Dgl2.Item(Col1Value, rowSalesTaxNo).ReadOnly = True
        Dgl2.Item(Col1Value, rowAadharNo).ReadOnly = True

        LblBarcode.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BarcodeGunTextbox")), Boolean)
        TxtBarcode.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BarcodeGunTextbox")), Boolean)

        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
            If LblV_Type.Tag = Ncat.SaleInvoice Then
                Dim bInvoiceCreatedFromSaleOrder As Boolean = False
                For i = 0 To Dgl1.Rows.Count - 1
                    If AgL.XNull(Dgl1.Item(Col1SaleInvoice, i).Tag) <> "" And
                        AgL.XNull(Dgl1.Item(Col1SaleInvoice, i).Tag) <> mSearchCode Then
                        bInvoiceCreatedFromSaleOrder = True
                        Exit For
                    End If
                Next
                If bInvoiceCreatedFromSaleOrder = True Then
                    TxtSaleToParty.Enabled = False
                    TxtBillToParty.Enabled = False
                End If
            End If

            If LblV_Type.Tag = Ncat.SaleOrder Then
                If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                            From SaleInvoice H 
                            LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                            LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                            Where Vt.NCat = '" & Ncat.SaleInvoice & "'
                            And L.SaleInvoice = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                    TxtSaleToParty.Enabled = False
                    TxtBillToParty.Enabled = False
                End If
            End If
        End If
    End Sub
    Private Sub FrmSaleInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F11 Then
            LblPurchaseRate.Visible = Not LblPurchaseRate.Visible
        ElseIf e.KeyCode = Keys.F9 Then
            'If Dgl1.CurrentCell IsNot Nothing Then
            '    If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value <> "" Then
            '        FPostInPurchIndent(AgL.GCn, AgL.ECmd, Dgl1.CurrentCell.RowIndex)
            '    End If
            'End If
        ElseIf e.KeyCode = (Keys.W And e.Control) Then
            If Topctrl1.Mode = "Add" Then
                ShowSaleInvoiceParty("", TxtSaleToParty.Tag, TxtNature.Text, True)
            Else
                ShowSaleInvoiceParty(mSearchCode, "", TxtNature.Text, True)
            End If
        End If
    End Sub

    Private Sub FShowTransactionHistory(ByVal Item As String)
        Dim DtTemp As DataTable = Nothing
        Dim CSV_Qry As String = ""
        Dim CSV_QryArr() As String = Nothing
        Dim I As Integer, J As Integer
        Dim IGridWidth As Integer = 0
        Try
            If AgL.PubServerName = "" Then
                mQry = " SELECT H.ManualRefNo [Inv_No], H.V_Date AS [Inv_Date],  " &
                        " L.Rate, L.Qty  " &
                        " FROM SaleInvoiceDetail L  With (NoLock) " &
                        " LEFT JOIN  SaleInvoice H  With (NoLock) ON L.DocId = H.DocId " &
                        " Left Join Voucher_Type Vt  With (NoLock) on H.V_Type = Vt.V_Type " &
                        " Where  NCat = '" & AgLibrary.ClsMain.agConstants.Ncat.SaleInvoice & "' And L.Item = '" & Item & "'" &
                        " And H.DocId <> '" & mSearchCode & "' " &
                        " And H.SaleToParty ='" & TxtSaleToParty.Tag & "' " &
                        " And Date(H.V_Date) <=" & AgL.Chk_Date(TxtV_Date.Text) & " " &
                        " ORDER BY H.V_Date DESC Limit 5  "
            Else
                mQry = " SELECT Top 5 H.ManualRefNo [Inv_No], H.V_Date AS [Inv_Date],  " &
                        " L.Rate, L.Qty  " &
                        " FROM SaleInvoiceDetail L  With (NoLock) " &
                        " LEFT JOIN  SaleInvoice H  With (NoLock) ON L.DocId = H.DocId " &
                        " Left Join Voucher_Type Vt  With (NoLock) on H.V_Type = Vt.V_Type " &
                        " Where NCat = '" & AgLibrary.ClsMain.agConstants.Ncat.SaleInvoice & "' And L.Item = '" & Item & "'" &
                        " And H.DocId <> '" & mSearchCode & "' " &
                        " And H.SaleToParty ='" & TxtSaleToParty.Tag & "' " &
                        " And Date(H.V_Date) <=" & AgL.Chk_Date(TxtV_Date.Text) & " " &
                        " ORDER BY H.V_Date DESC  "
            End If


            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)



            If DtTemp.Rows.Count = 0 Then Dgl.DataSource = Nothing : Dgl.Visible = False : Exit Sub

            Dgl.DataSource = DtTemp
            Dgl.Visible = True

            'Dgl.DataSource.DefaultView.RowFilter = " Item='" & Item & "' "

            Me.Controls.Add(Dgl)
            Dgl.Left = Me.Left + 3
            Dgl.Top = Me.Bottom - Dgl.Height - 100
            Dgl.Height = 130
            Dgl.Width = 450
            Dgl.ColumnHeadersHeight = 40
            Dgl.AllowUserToAddRows = False
            If Dgl.Columns.Count > 0 Then

                If CSV_Qry <> "" Then J = CSV_QryArr.Length

                For I = 0 To Dgl.ColumnCount - 1
                    If CSV_Qry <> "" Then
                        If I < J Then
                            If Val(CSV_QryArr(I)) > 0 Then
                                Dgl.Columns(I).Width = Val(CSV_QryArr(I))
                            Else
                                Dgl.AutoResizeColumn(I)
                                'Dgl.Columns(I).Width = 100
                            End If
                        Else
                            Dgl.AutoResizeColumn(I)
                            'Dgl.Columns(I).Width = 100
                        End If
                    Else
                        Dgl.Columns(I).Width = 100
                    End If
                    Dgl.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                    IGridWidth += Dgl.Columns(I).Width
                Next


                Dgl.Width = IGridWidth + 50


                Dgl.RowHeadersVisible = False
                Dgl.EnableHeadersVisualStyles = False
                Dgl.AllowUserToResizeRows = False
                Dgl.ReadOnly = True
                Dgl.AutoResizeRows()
                Dgl.AutoResizeColumnHeadersHeight()
                Dgl.BackgroundColor = Color.Cornsilk
                Dgl.ColumnHeadersDefaultCellStyle.BackColor = Color.Cornsilk
                Dgl.DefaultCellStyle.BackColor = Color.Cornsilk
                Dgl.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                Dgl.CellBorderStyle = DataGridViewCellBorderStyle.None
                Dgl.Font = New Font(New FontFamily("Verdana"), 8)
                Dgl.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                Dgl.BringToFront()
                Dgl.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        FShowTransactionHistory(Dgl1.Item(Col1Item, e.RowIndex).Tag)
        LblPurchaseRate.Text = Format(Val(Dgl1.Item(Col1PurchaseRate, e.RowIndex).Value), "0.00")

        If AgL.VNull(Dgl1.Item(Col1CurrentStock, e.RowIndex).Value) = 0 Then
            Dgl1.Item(Col1CurrentStock, e.RowIndex).Value = ClsMain.FGetCurrentStock(Dgl1.Item(Col1Item, e.RowIndex).Tag,
                                    Dgl1.Item(Col1Godown, e.RowIndex).Tag, TxtV_Date.Text)
        End If
        LblCurrentStock.Text = Dgl1.Item(Col1CurrentStock, e.RowIndex).Value


        Dim mRow = e.RowIndex
        Try
            If mPrevRowIndex <> e.RowIndex Then
                'FChangeOptions(mRow)
            End If
            mPrevRowIndex = mRow
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        Dgl.Visible = False
    End Sub

    'Private Sub FCheckDuplicate(ByVal mRow As Integer)
    '    Dim I As Integer = 0
    '    Dim Str1 As String = ""
    '    Dim Str2 As String = ""
    '    Try
    '        With Dgl1
    '            For I = 0 To .Rows.Count - 1
    '                If .Item(Col1Item, I).Value <> "" Then
    '                    If mRow <> I Then
    '                        Str1 = Dgl1.Item(Col1Item, I).Value & Dgl1.Item(Col1Specification, I).Value & Dgl1.Item(Col1Dimension1, I).Value & Dgl1.Item(Col1Dimension2, I).Value & Dgl1.Item(Col1Dimension3, I).Value & Dgl1.Item(Col1Dimension4, I).Value & Dgl1.Item(Col1Barcode, I).Value
    '                        Str2 = Dgl1.Item(Col1Item, mRow).Value & Dgl1.Item(Col1Specification, mRow).Value & Dgl1.Item(Col1Dimension1, mRow).Value & Dgl1.Item(Col1Dimension2, mRow).Value & Dgl1.Item(Col1Dimension3, mRow).Value & Dgl1.Item(Col1Dimension4, mRow).Value & Dgl1.Item(Col1Barcode, mRow).Value
    '                        If AgL.StrCmp(Str1, Str2) Then
    '                            If MsgBox("Item " & .Item(Col1Item, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
    '                                Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
    '                            Else
    '                                If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionOnDuplicateItem")).ToString.ToUpper = "DO NOTHING" Then
    '                                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("ActionOnDuplicateItem")).ToString.ToUpper = "GO TO FIRST ITEM" Then
    '                                    Dim mFirstRowIndex As Integer
    '                                    mFirstRowIndex = Val(Dgl1.Item(ColSNo, I).Value) - 1
    '                                    Dgl1.CurrentCell = Dgl1.Item(Col1DocQty, mFirstRowIndex)
    '                                    Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
    '                                End If
    '                            End If
    '                            '.CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
    '                            '.Rows.Remove(.Rows(mRow)) : Exit Sub
    '                        End If
    '                    End If
    '                End If
    '            Next
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Dim Str1 As String = ""
        Dim Str2 As String = ""
        Try
            If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionOnDuplicateItem")).ToString <> ActionOnDuplicateItem.DoNothing Then
                With Dgl1
                    For I = 0 To .Rows.Count - 1
                        If .Item(Col1Item, I).Value <> "" Then
                            If mRow <> I Then
                                Str1 = Dgl1.Item(Col1Item, I).Value & Dgl1.Item(Col1Specification, I).Value & Dgl1.Item(Col1Dimension1, I).Value & Dgl1.Item(Col1Dimension2, I).Value & Dgl1.Item(Col1Dimension3, I).Value & Dgl1.Item(Col1Dimension4, I).Value & Dgl1.Item(Col1Barcode, I).Value
                                Str2 = Dgl1.Item(Col1Item, mRow).Value & Dgl1.Item(Col1Specification, mRow).Value & Dgl1.Item(Col1Dimension1, mRow).Value & Dgl1.Item(Col1Dimension2, mRow).Value & Dgl1.Item(Col1Dimension3, mRow).Value & Dgl1.Item(Col1Dimension4, mRow).Value & Dgl1.Item(Col1Barcode, mRow).Value
                                If AgL.StrCmp(Str1, Str2) Then
                                    If MsgBox("Item " & .Item(Col1Item, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                        Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
                                    Else
                                        If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionOnDuplicateItem")).ToString = ActionOnDuplicateItem.AlertAndAskToContinue Then
                                        ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("ActionOnDuplicateItem")).ToString = ActionOnDuplicateItem.AlertAndGoToFirstItem Then
                                            Dim mFirstRowIndex As Integer
                                            mFirstRowIndex = Val(Dgl1.Item(ColSNo, I).Value) - 1
                                            Dgl1.CurrentCell = Dgl1.Item(Col1DocQty, mFirstRowIndex)
                                            Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FFormatRateCells(ByVal mRow As Integer)
        Dim I As Integer = 0
        Try
            If Val(Dgl1.Item(Col1Rate, mRow).Value) < Val(Dgl1.Item(Col1PurchaseRate, mRow).Value) Then
                Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
                Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Red

            Else
                Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Black
                Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Regular)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FPostInPurchIndent(ByVal Conn As Object, ByVal Cmd As Object, ByVal mRow As Integer)
        Dim mSr As Integer = 0

        mQry = " Select Count(*) From PurchIndent H  With (NoLock) LEFT JOIN PurchIndentDetail L  With (NoLock) ON H.DocId = L.DocId Where H.V_Date = '" & TxtV_Date.Text & "' And L.Item = '" & Dgl1.Item(Col1Item, mRow).Tag & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = 0 Then
            mQry = " Select Count(*) From PurchIndent  With (NoLock)  Where DocId = '" & mSearchCode & "'  "
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = 0 Then
                mQry = " INSERT INTO PurchIndent " &
                            " ( " &
                            " DocID, " &
                            " V_Type, " &
                            " V_Prefix, " &
                            " V_Date, " &
                            " V_No, " &
                            " Div_Code, " &
                            " Site_Code, " &
                            " Remarks, " &
                            " EntryBy, " &
                            " EntryDate) " &
                            " Values ( " &
                            " '" & mSearchCode & "', " &
                            " '" & TxtV_Type.Tag & "', " &
                            " " & AgL.Chk_Text(LblPrefix.Text) & ", " &
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ", " &
                            " " & Val(TxtV_No.Text) & ", " &
                            " " & AgL.Chk_Text(TxtDivision.Tag) & ", " &
                            " " & AgL.Chk_Text(TxtSite_Code.Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl3(Col1Value, rowRemarks).Value) & ", " &
                            " '" & AgL.PubUserName & "', " &
                            " '" & AgL.PubLoginDate & "') "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If




            mQry = " Select Max(Sr) From PurchIndentDetail  With (NoLock)  Where DocId = '" & mSearchCode & "'  "
            mSr = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

            mSr += 1
            mQry = " INSERT INTO PurchIndentDetail(DocId, Sr, Item, IndentQty, Unit) " &
                    " Values('" & mSearchCode & "', " & mSr & ", " & AgL.Chk_Text(Dgl1.Item(Col1Item, mRow).Tag) & ", 1, " &
                    " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, mRow).Value) & ")"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
        Dgl1.Item(ColSNo, mRow).Style.ForeColor = Color.Red
        Dgl1.Item(ColSNo, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If mFlag_Import = True Then
            If Dgl3.Visible = True Then
                Dgl3.CurrentCell = Dgl3(Col1Value, rowRemarks)
                Dgl3.Focus()
                Exit Sub
            End If
        End If
        If Dgl1.Rows.Count > 0 Then
            Dgl1.CurrentCell = Dgl1.Item(Col1Item, Dgl1.Rows.Count - 1) : Dgl1.Focus()
        End If
    End Sub


    Private Sub FCreateHelpSubgroup()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupType")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupType")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupType")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupType")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeType")).ToString.Contains(TreeNodeType.Leaf) Then
                    strCond += " And Sg.Parent Is Not Null "
                End If
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeType")).ToString.Contains(TreeNodeType.Root) Then
                    strCond += " And Sg.Parent Is Null "
                End If
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeType")).ToString.Contains(TreeNodeType.Parent) Then
                    strCond += " And Sg.SubCode In (Select Distinct Parent From SubGroup) "
                End If
            End If
        End If

        If FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || IfNull(Sp.Process,'" & Process.Sales & "'),'" & FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) & "') > 0 "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || IfNull(Sp.Process,'" & Process.Sales & "'),'" & FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) & "') <= 0 "
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowAccountsOfOtherDivisions")) Then
            strCond += " And (Sg.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(Sg.ShowAccountInOtherDivisions,0) = 1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowAccountsOfOtherSites")) Then
            strCond += " And (Sg.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(Sg.ShowAccountInOtherSites,0) = 1) "
        End If



        strCond += " And Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Cash & "','" & ClsMain.SubGroupNature.Bank & "')"

        mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.Address, Ag.GroupName, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " &
                " FROM SubGroup Sg  With (NoLock) " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Left Join SubgroupProcess SP On Sg.Subcode = SP.Subcode " &
                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        mQry += " Union All SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.Address, Ag.GroupName, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " &
                " FROM SubGroup Sg  With (NoLock) " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Left Join SubgroupProcess SP On Sg.Subcode = SP.Subcode " &
                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                " And Sg.Nature In ('" & ClsMain.SubGroupNature.Supplier & "')    "
        If FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) <> "" Then
            mQry += " And CharIndex('+' || IfNull(Sp.Process,'.'),'" & FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) & "') > 0 "
        End If


        TxtSaleToParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Dgl2.Item(Col1Head, rowShipToParty).Tag = TxtSaleToParty.AgHelpDataSet
    End Sub


    Private Sub FCreateHelpBillToParty()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
                strCond += " And CharIndex('-' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupType")) <> "" Then
                strCond += " And CharIndex('+' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupType")) & "') > 0 "
                strCond += " And CharIndex('-' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupType")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) <> "" Then
                strCond += " And CharIndex('+' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) & "') > 0 "
                strCond += " And CharIndex('-' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) & "') <= 0 "
            End If

        End If

        strCond += " And Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Cash & "','" & ClsMain.SubGroupNature.Bank & "')"

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeType")).ToString.Contains(TreeNodeType.Leaf) Then
            strCond += " And Sg.SubCode In (Select Parent From SubGroup Where Subcode = '" & TxtSaleToParty.Tag & "')"
        End If

        mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.Address, Ag.GroupName, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " &
                " FROM SubGroup Sg  With (NoLock) " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Where 1 = 1 " &
                " And IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        TxtBillToParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpItemCategory()
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM ItemCategory I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpSaleOrder()
        Dim strCond As String = ""

        mQry = "SELECT H.DocID, Max(H.V_Type || '-' || H.ManualRefNo) AS OrderNo, Sum(VOrderBalance.OrderBalanceAmount) AS OrderBalanceAmount
                    FROM (" & FGetSaleOrderBalanceQry(CType(AgL.VNull(DtV_TypeSettings.Rows(0)("CalculateContraBalanceOnValueYN")), Boolean), TxtSaleToParty.Tag) & " ) AS VOrderBalance
                LEFT JOIN SaleInvoice H ON VOrderBalance.DocId = H.DocID
                GROUP BY H.DocID "
        Dgl1.AgHelpDataSet(Col1SaleInvoice) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpItemGroup(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
                End If
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null ) "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or I.Div_Code Is Null Or IfNull(I.ShowItemGroupInOtherDivisions,0) =1) "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or I.Site_Code Is Null Or IfNull(I.ShowItemGroupInOtherSites,0) =1) "
        End If


        'mQry = "Select IG.Code, IG.Description 
        '        From Item I  With (NoLock)
        '        Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
        '        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & "
        '        And Ig.Code Is Not Null
        '        Group By I.ItemGroup,IG.Code, IG.Description "



        mQry = "Select I.Code, I.Description
                        FROM ItemGroup I 
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpItem(RowIndex As Integer)
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.V_Type,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.V_Type,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")) & "') <= 0 "
                End If
            Else
                strCond += " And I.V_Type = 'ITEM' "
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemCategory,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemCategory,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) & "') <= 0 "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') <= 0 "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') <= 0 "
                End If
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or I.Div_Code Is Null Or IfNull(I.ShowItemInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or I.Site_Code Is Null Or IfNull(I.ShowItemInOtherSites,0) =1) "
        End If


        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" And Dgl1.Columns(Col1ItemCategory).Visible And UserMovedOverItemCategory Then
            strCond += " And I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' "
        End If


        If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" And Dgl1.Columns(Col1ItemGroup).Visible And UserMovedOverItemGroup Then
            strCond += " And I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' "
        End If




        Dim strSelect As String

        strSelect = "Select I.Code, I.Description, I.Rate "

        Dim mHelpAddtionalColumns As String = FGetSettings(SettingFields.ItemHelpAdditionalColumns, SettingType.General)
        If mHelpAddtionalColumns <> "" Then
            Dim mAdditionalColumns() As String
            mAdditionalColumns = mHelpAddtionalColumns.Split("+")
            For i As Integer = 0 To mAdditionalColumns.Length - 1
                Select Case mAdditionalColumns(i)
                    Case "[Item Group PD]"
                        strSelect += ", IG.PrintingDescription as [Item Group PD] "

                    Case "[Margin]"
                        strSelect += ", I.Default_MarginPer as [Margin] "
                End Select
            Next
        End If


        mQry = strSelect
        mQry += " FROM Item I  With (NoLock) " &
                  " Left Join Item IG On I.ItemGroup = IG.Code " &
                  " Where I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' 
                  And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        mQry += " Union All "
        mQry += strSelect
        mQry += " FROM Item I  With (NoLock) " &
                  " Left Join Item IG On I.ItemGroup = IG.Code " &
                  " Where I.ItemType = '" & ItemTypeCode.ServiceProduct & "' 
                  And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "


        'mQry = "SELECT I.Code, I.Description, IG.PrintingDescription as ItemGroup_PD, I.Rate, I.Default_MarginPer as Margin " &
        '          " FROM Item I  With (NoLock) " &
        '          " Left Join Item IG On I.ItemGroup = IG.Code " &
        '          " Where I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' 
        '          And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond

        'mQry += " UNION ALL "
        'mQry += "SELECT I.Code, I.Description, IG.PrintingDescription as ItemGroup_PD, I.Rate, I.Default_MarginPer as Margin " &
        '          " FROM Item I  With (NoLock) " &
        '          " Left Join Item IG On I.ItemGroup = IG.Code " &
        '          " Where I.ItemType = '" & ItemTypeCode.ServiceProduct & "' 
        '          And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "

        Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpInvoicedItem()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('+' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
                strCond += " And CharIndex('-' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('+' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
                strCond += " And CharIndex('-' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') <= 0 "
            End If
        End If



        mQry = "SELECT I.Code,  I.Description, I.ManualCode, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " &
               " IfNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code, " &
               " I.DealUnit, I.DealQty As UnitMultiplier, I.Rate As Rate, 1 As PendingQty, I.Status, " &
               " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as DealUnitDecimalPlaces " &
               " FROM Item I  With (NoLock) " &
               " LEFT JOIN Unit U  With (NoLock) On I.Unit = U.Code " &
               " LEFT JOIN Unit U1  With (NoLock) On I.DealUnit = U1.Code " &
               " Where 1=1 " &
               " And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & " "
        Dgl1.AgHelpDataSet(Col1Item, 10) = AgL.FillData(mQry, AgL.GcnRead)
    End Sub


    Private Sub FCreateHelpItemFromSaleOrder(RowIndex As Integer)
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('-' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') <= 0 "
                End If
            End If

            If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" And Dgl1.Columns(Col1ItemCategory).Visible Then
                strCond += " And I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' "
            End If

            If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" And Dgl1.Columns(Col1ItemGroup).Visible Then
                strCond += " And I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' "
            End If
        End If



        Dim bItemGroupSaleOrder As Integer = 0
        If Dgl1.Item(Col1SaleInvoice, RowIndex).Tag <> "" Then
            bItemGroupSaleOrder = AgL.VNull(AgL.Dman_Execute(" Select Count(*) As cnt
                    From SaleInvoiceDetail L 
                    LEFT JOIN Item I On L.Item = I.Code 
                    Where L.DocId = '" & Dgl1.Item(Col1SaleInvoice, RowIndex).Tag & "' And I.V_Type = 'IG'", AgL.GCn).ExecuteScalar())
        End If

        If bItemGroupSaleOrder > 0 Then
            mQry = "SELECT I.Code As Code,  Max(I.Description) As Description, Max(I.ManualCode) As ManualCode, 
                Max(I.Unit) As Unit, Max(I.ItemType) As ItemType, 
                Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup, IfNull(Max(I.IsDeleted),0) AS IsDeleted, 
                Max(I.Div_Code) As Div_Code, Max(I.DealUnit) As DealUnit, Max(I.DealQty) As UnitMultiplier, 
                Max(I.Rate) As Rate, 1 As PendingQty, 
                Max(I.Status) As Status, Max(U.DecimalPlaces) as QtyDecimalPlaces, 
                Max(U1.DecimalPlaces) as DealUnitDecimalPlaces,
                Max(L.DocId) As SaleInvoice, Max(L.Sr) As SaleInvoiceSr
                FROM (" & FGetSaleOrderBalanceQry(CType(AgL.VNull(DtV_TypeSettings.Rows(0)("CalculateContraBalanceOnValueYN")), Boolean), TxtSaleToParty.Tag) & ") VSaleOrderBalance
                LEFT JOIN SaleInvoiceDetail L ON VSaleOrderBalance.DocID = L.DocID And VSaleOrderBalance.Sr = L.Sr 
                LEFT JOIN ItemGroup Ig On L.Item = Ig.Code 
                LEFT JOIN Item I ON Ig.Code = I.ItemGroup 
                LEFT JOIN Unit U  With (NoLock) On I.Unit = U.Code 
                LEFT JOIN Unit U1  With (NoLock) On I.DealUnit = U1.Code 
                WHERE 1=1 
                And I.Code Is Not Null " & strCond
            If Dgl1.Item(Col1SaleInvoice, RowIndex).Tag <> "" Then
                mQry += " And L.DocId = '" & Dgl1.Item(Col1SaleInvoice, RowIndex).Tag & "' "
            End If
            mQry += " Group By I.Code "
        Else
            mQry = "SELECT Max(I.Code) As Code,  Max(I.Description) As Description, Max(I.ManualCode) As ManualCode, 
                Max(I.Unit) As Unit, Max(I.ItemType) As ItemType, 
                Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup, IfNull(Max(I.IsDeleted),0) AS IsDeleted, 
                Max(I.Div_Code) As Div_Code, Max(I.DealUnit) As DealUnit, Max(I.DealQty) As UnitMultiplier, 
                Max(I.Rate) As Rate, 1 As PendingQty, 
                Max(I.Status) As Status, Max(U.DecimalPlaces) as QtyDecimalPlaces, 
                Max(U1.DecimalPlaces) as DealUnitDecimalPlaces,
                L.DocId As SaleInvoice, L.Sr As SaleInvoiceSr
                FROM (" & FGetSaleOrderBalanceQry(CType(AgL.VNull(DtV_TypeSettings.Rows(0)("CalculateContraBalanceOnValueYN")), Boolean), TxtSaleToParty.Tag) & ") VSaleOrderBalance
                LEFT JOIN SaleInvoiceDetail L ON VSaleOrderBalance.DocID = L.DocID And VSaleOrderBalance.Sr = L.Sr 
                LEFT JOIN ItemGroup Ig On L.Item = Ig.Code 
                LEFT JOIN Item I ON Ig.Code = I.ItemGroup 
                LEFT JOIN Unit U  With (NoLock) On I.Unit = U.Code 
                LEFT JOIN Unit U1  With (NoLock) On I.DealUnit = U1.Code 
                WHERE 1=1 
                And I.Code Is Not Null " & strCond
            If Dgl1.Item(Col1SaleInvoice, RowIndex).Tag <> "" Then
                mQry += " And L.DocId = '" & Dgl1.Item(Col1SaleInvoice, RowIndex).Tag & "' "
            End If

            mQry += " UNION ALL "
            mQry += "SELECT I.Code, I.Description, I.ManualCode, I.Unit, I.ItemType, I.SalesTaxPostingGroup, I.IsDeleted, I.Div_Code, I.DealUnit, " &
                  "  I.DealQty as Unitmultiplier, I.Rate, 1 as PendingQty, I.Status, 0 as QtyDecimalPlaces, 0 as DealUnitDecimalPlaces, Null as SaleInvoice, Null as SaleInvoiceSr " &
                  " FROM Item I  With (NoLock) " &
                  " Left Join Item IG On I.ItemGroup = IG.Code " &
                  " Where I.ItemType = '" & ItemTypeCode.ServiceProduct & "' 
                  And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "

            mQry += " Group By SaleInvoice, SaleInvoiceSr, I.Code "
        End If
        Dgl1.AgHelpDataSet(Col1Item, 15) = AgL.FillData(mQry, AgL.GcnRead)
    End Sub


    Private Sub FCreateHelpItemGroupFromSaleOrder(RowIndex As Integer)
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') <= 0 "
                End If
            End If

            If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
                strCond += " And (IG.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(IG.ShowItemGroupInOtherDivisions,0) =1) "
            End If

            If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
                strCond += " And (IG.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(IG.ShowItemGroupInOtherSites,0) =1) "
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" And Dgl1.Columns(Col1ItemCategory).Visible Then
            strCond += " And I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' "
        End If

        If Dgl1.Item(Col1SaleInvoice, RowIndex).Tag <> "" And Dgl1.Columns(Col1SaleInvoice).Visible Then
            strCond += " And L.DocId = '" & Dgl1.Item(Col1SaleInvoice, RowIndex).Tag & "' "
        End If

        mQry = "Select IfNull(IG.Code,I.Code) As Code, IfNull(IG.Description,I.Description) As Description 
                From SaleInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
                LEFT JOIN Item I  With (NoLock) On L.Item = I.Code 
                LEFT JOIN ItemGroup Ig On L.Item = Ig.Code 
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & "
                And Ig.Code Is Not Null
                Group By I.ItemGroup,IG.Code, IG.Description "
        Dgl1.AgHelpDataSet(Col1ItemGroup, 0) = AgL.FillData(mQry, AgL.GcnRead)
    End Sub


    'Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn


    '    Dim mPrintTitle As String
    '    mQry = "
    '            Select H.DocID, H.V_Date, VT.Description as Voucher_Type, VT.NCat, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, IfNull(RT.Description,'Super Net') as RateType, Agent.DispName as AgentName, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption,
    '            H.SaleToPartyName, H.SaleToPartyAddress, C.CityName as CityName, State.ManualCode as StateCode, State.Description as StateName, 
    '            H.SaleToPartyMobile, Sg.ContactPerson, H.SaleToPartySalesTaxNo, (Select RegistrationNo From SubgroupRegistration Where RegistrationType='" & SubgroupRegistrationType.AadharNo & "' And Subcode=H.SaleToParty) as SaleToPartyAadharNo,
    '            H.ShipToAddress, H.TermsAndConditions, Transporter.Name as TransporterName, TD.LrNo, TD.LrDate, L.ReferenceNo,
    '            I.Description as ItemName, IG.Description as ItemGroupName, IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, I.HSN,
    '            L.SalesTaxGroupItem, STGI.GrossTaxRate, L.Pcs, L.Qty, L.Rate, L.Unit, TS.DiscountCalculationPattern, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, 
    '            L.Amount,L.Taxable_Amount,L.Tax1_Per, L.Tax1, L.Tax2_Per, L.Tax2, L.Tax3_Per, L.Tax3, L.Tax4_Per, L.Tax4, L.Tax5_Per, L.Tax5, L.Net_Amount,
    '            H.Gross_Amount as H_Gross_Amount,H.Taxable_Amount as H_Taxable_Amount,H.Tax1_Per as H_Tax1_Per, H.Tax1 as H_Tax1, 
    '            H.Tax2_Per as H_Tax2_Per, H.Tax2 as H_Tax2, H.Tax3_Per as H_Tax3_Per, H.Tax3 as H_Tax3, H.Tax4_Per as H_Tax4_Per, H.Tax4 as H_Tax4, 
    '            H.Tax5_Per as H_Tax5_Per, H.Tax5 as H_Tax5, H.Round_Off, H.Net_Amount as H_Net_Amount, '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
    '            (Select '[' || group_concat(cast(cast(pcs as INT) as nvarchar) || ' X ' || cast(qty as nvarchar),', ') || ']' from SaleInvoiceDimensionDetail DL Where DL.DocID = L.DocID And DL.TSr = L.Sr) as DimDetail,
    '            (Select  group_concat(ItemCatName ,', ')   from
    '                (
    '                select ItemCat.Description as ItemCatName
    '                from SaleInvoiceDetail SIL 
    '                Left Join Item On SIL.Item = Item.Code   
    '                Left Join ItemCategory ItemCat On Item.ItemCategory = ItemCat.Code
    '                Where SIL.DocID = SIL.DocID And Item.HSN = I.Hsn
    '                group By ItemCat.Description
    '                )) as HsnDescription
    '            from SaleInvoice H
    '            Left Join SaleInvoiceTrnSetting TS On H.DocId = TS.DocID
    '            Left Join SaleInvoiceDetail L On H.DocID = L.DocID
    '            Left Join Item I On L.Item = I.Code
    '            Left Join ItemGroup IG On I.ItemGroup = IG.Code
    '            Left Join ItemCategory IC On I.ItemCategory = IC.Code
    '            Left Join City C On H.SaleToPartyCity = C.CityCode
    '            Left Join State On C.State = State.Code
    '            Left Join SaleInvoiceTransport TD On H.DocID = TD.DocID
    '            Left Join ViewHelpSubgroup Transporter On TD.Transporter= Transporter.Code
    '            Left Join PostingGroupSalesTaxItem STGI On L.SalesTaxGroupItem = STGI.Description
    '            Left Join Subgroup Sg On H.SaleToParty = Sg.Subcode
    '            Left Join RateType RT on H.RateType = Rt.Code
    '            Left Join Subgroup Agent On H.Agent = Agent.Subcode
    '            Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type
    '            Where H.DocID = '" & mSearchCode & "'
    '            "


    '    If LblV_Type.Tag = Ncat.SaleReturn Then
    '        mPrintTitle = "SALES RETURN"
    '    Else
    '        If Not AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
    '            mPrintTitle = "CHALLAN"
    '        Else
    '            mPrintTitle = "TAX INVOICE"
    '        End If
    '    End If

    '    If ClsMain.IsScopeOfWorkContains("+CLOTH TRADING WHOLESALE") Then
    '        FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "SaleInvoice_Print_Cloth", mPrintTitle, , , , TxtSaleToParty.Tag, TxtV_Date.Text)
    '    Else
    '        FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "SaleInvoice_Print", mPrintTitle, , , , TxtSaleToParty.Tag, TxtV_Date.Text)
    '    End If
    'End Sub

    Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
    End Sub

    Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor,
                         Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        'For SSRS Print Out
        Dim DtTemp As DataTable

        mQry = "SELECT H.DocID  FROM SaleInvoice H With (NoLock)
                LEFT JOIN SaleInvoiceDetail L With (NoLock) ON H.DocID = L.DocID 
                WHERE H.DocID " & IIf(BulkCondStr <> "", " In (" & BulkCondStr & ")", " ='" & SearchCode & "'") &
                " And H.Gross_Amount > 0
                Group BY H.DocID 
                HAVING Round(Sum(L.Amount),2)<>Round(Max(H.Gross_Amount),2)"
        If AgL.FillData(mQry, AgL.GCn).Tables(0).Rows.Count > 0 Then
            MsgBox("Something went wrong with gross amount. Can not print Invoice. Please check once.")
            Exit Sub
        End If

        mQry = "SELECT H.DocID, H.Sr, I.Description as ItemName, Round(Sum(L.TotalQty),2),Round(Max(H.Qty),2)  FROM SaleInvoiceDetail H With (NoLock)
                LEFT JOIN SaleInvoiceDimensionDetail L With (NoLock) ON H.DocID = L.DocID And H.Sr = L.TSr
                Left Join Item I With (NoLock) On H.Item = I.Code
                WHERE H.DocID " & IIf(BulkCondStr <> "", " In (" & BulkCondStr & ")", " ='" & SearchCode & "'") &
                " Group BY H.DocID, H.Sr, I.Description 
                HAVING abs(Round(Sum(L.TotalQty),2))<>abs(Round(Max(H.Qty),2))"

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            MsgBox("Something went wrong with dimension detail at item " & AgL.XNull(DtTemp.Rows(0)("ItemName")) & ". Can not print Invoice. Please check once.")
            Exit Sub
        End If

        'FGetPrintSSRS(mPrintFor)

        If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Then
            FGetPrintCrystal_Aadhat(Me, SearchCode, mPrintFor, IsPrintToPrinter, BulkCondStr, "")
        Else
            FGetPrintCrystal(SearchCode, mPrintFor, IsPrintToPrinter, BulkCondStr)
        End If
    End Sub

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, TxtSite_Code.Tag, VoucherCategory.Sales, LblV_Type.Tag, TxtV_Type.Tag, "", "")
        FGetSettings = mValue
    End Function

    Sub FGetPrintCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer
        Dim sQryPayment As String = ""
        Dim mDocumentNoPattern As String = ""
        Dim DtDoc As DataTable
        Dim dtTemp As DataTable
        Dim mSaleToParty As String
        Dim mSalesTaxGroupParty As String


        mDocumentNoPattern = ClsMain.FGetSettings(ClsMain.SettingFields.DocumentNoPattern, SettingType.General, "", "", "", "", "", "", "")
        mPrintTitle = AgL.Dman_Execute("Select IfNull(PrintingDescription, Description) From Voucher_Type Where V_Type = '" & TxtV_Type.Tag & "' ", AgL.GCn).ExecuteScalar()
        Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        Dim mDocReportFileName As String = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)
        Dim SettingPrintRateType As String = FGetSettings(SettingFields.PrintRateType, SettingType.General)

        AgL.PubTempStr = AgL.PubTempStr & "Start Feching basic header detail of document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        mQry = "Select VT.nCat, H.V_Type, H.Div_Code, H.Site_Code, H.V_date From SaleInvoice H With (NoLock) Left Join Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type Where H.DocID = '" & SearchCode & "'"
        DtDoc = AgL.FillData(mQry, AgL.GCn).Tables(0)
        AgL.PubTempStr = AgL.PubTempStr & "End Feching basic header detail of document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Party Detail from document Header Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        mQry = "Select IfNull(SalesTaxGroupParty,'') as SalesTaxGroupParty, IfNull(SaleToParty,'') as SaleToParty From SaleInvoice Where DocID = '" & SearchCode & "'"
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            mSaleToParty = AgL.XNull(dtTemp.Rows(0)("SaleToParty"))
            mSalesTaxGroupParty = AgL.XNull(dtTemp.Rows(0)("SalesTaxGroupParty"))
        Else
            MsgBox("Party detail can not be fetched for selected invoice. Can't generate print.")
            Exit Sub
        End If
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Party Detail from document Header Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Show Party Balance In Report from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        Dim DocumentPrintShowPartyBalance As String = FGetSettings(SettingFields.DocumentPrintShowPartyBalance, SettingType.General)
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Show Party Balance In Report from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

        Dim mOpeningBalance As Double = 0, mTodaysDr As Double = 0, mTodaysCr As Double = 0, mClosingBalance As Double = 0
        If DocumentPrintShowPartyBalance <> DocumentPrintFieldsVisibilityOptions.Hide Then
            AgL.PubTempStr = AgL.PubTempStr & "Start Feching Party Balance To Print From Ledger Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            mQry = "Select IfNull(Sum(Case When LG.V_Date < " & AgL.Chk_Date(AgL.XNull(DtDoc.Rows(0)("V_Date"))) & " THEN LG.AmtDr-LG.AmtCr ELSE 0 END),0) AS OpeningBalance,
	               IfNull(Sum(CASE WHEN LG.V_Date = " & AgL.Chk_Date(AgL.XNull(DtDoc.Rows(0)("V_Date"))) & " THEN LG.AmtDr ELSE 0 END),0) AS TodaysDr,
	               IfNull(Sum(CASE WHEN LG.V_Date = " & AgL.Chk_Date(AgL.XNull(DtDoc.Rows(0)("V_Date"))) & " THEN LG.AmtCr ELSE 0 END),0) AS TodaysCr,
	               IfNull(Sum(CASE WHEN LG.V_Date <= " & AgL.Chk_Date(AgL.XNull(DtDoc.Rows(0)("V_Date"))) & " THEN LG.AmtDr-LG.AmtCr ELSE 0 END),0) AS ClosingBalance     
                   FROM Ledger LG
                   WHERE LG.SubCode ='" & mSaleToParty & "'"
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If dtTemp.Rows.Count > 0 Then
                mOpeningBalance = AgL.VNull(dtTemp.Rows(0)("OpeningBalance"))
                mTodaysDr = AgL.VNull(dtTemp.Rows(0)("TodaysDr"))
                mTodaysCr = AgL.VNull(dtTemp.Rows(0)("TodaysCr"))
                mClosingBalance = AgL.VNull(dtTemp.Rows(0)("ClosingBalance"))
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End Feching Party Balance To Print From Ledger Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        End If

        If LblV_Type.Tag = Ncat.SaleInvoice Then
            If Not AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
                mPrintTitle = "CHALLAN"
            Else
                mDocNoCaption = "Invoice No."
                mDocDateCaption = "Invoice Date"
            End If
        End If


        Dim bPrimaryQry As String = ""
        If BulkCondStr <> "" Then
            bPrimaryQry = " Select * From SaleInvoice  With (NoLock) Where DocID In (" & BulkCondStr & ")"
            PrintingCopies = FGetSettings(SettingFields.PrintingBulkCopyCaptions, SettingType.General).ToString.Split(",")
        Else
            bPrimaryQry = " Select * From SaleInvoice  With (NoLock) Where DocID = '" & SearchCode & "'"
            PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        End If



        'PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")

        Dim QryRate As String = " " & IIf(SettingPrintRateType = PrintRateType.Rate, " L.Rate ", "(Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then (Case When L.Taxable_Amount >0 And (L.Taxable_Amount <> L.Amount Or L.AdditionAmount > 0 ) Then (L.Taxable_Amount - (L.DiscountAmount + L.AdditionalDiscountAmount))/L.DocQty Else L.Rate End ) Else 0 End)") & " "

        If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
            QryRate = " " & IIf(SettingPrintRateType = PrintRateType.Rate, " L.Rate ", "(Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then (Case When L.Taxable_Amount >0 And (L.Taxable_Amount <> L.Amount Or L.AdditionAmount > 0 ) Then (L.Taxable_Amount)/L.DocQty Else L.Rate End ) Else 0 End)") & " "
        End If

        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            ''" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankQR")) & "' as Default_BankQR,
            '(Case When DP.Prefix Is Not Null Then DP.Prefix || H.ManualRefNo Else H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo End) as InvoiceNo, 
            ''" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as InvoiceNo, 
            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, H.DocID, L.Sr, H.V_Date, H.DeliveryDate, VT.Description as Voucher_Type, VT.NCat,                                 
                Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Site.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')) As InvoiceNo,
                IfNull(RT.Description,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("SaleRate_Caption")) & "') as RateType, 
                '" & FGetSettings(SettingFields.DocumentPrintShowRateType, SettingType.General) & "' as DocumentPrintShowRateType,
                IfNull(Agent.DispName,'') as AgentName, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption, BP.Nature AS BillToPartyNature,
                (Case When BP.Nature = 'Cash' Then BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'') Else H.SaletoPartyName End) as SaleToPartyName, 
                IfNull(H.SaleToPartyAddress,'') as SaleToPartyAddress, IfNull(C.CityName,'') as CityName, IfNull(H.SaleToPartyPincode,'') as SaleToPartyPincode, 
                IfNull(State.ManualCode,'') as StateCode, IfNull(State.Description,'')  as StateName, 
                IfNull(H.SaleToPartyMobile,'') as SaleToPartyMobile, Sg.ContactPerson, IfNull(H.SaleToPartySalesTaxNo,'') as SaleToPartySalesTaxNo, 
                IfNull(H.SaleToPartyAadharNo,'') as SaleToPartyAadharNo, IfNull(H.SaleToPartyPanNo,'') as SaleToPartyPanNo,
                (Case When BP.Nature = 'Cash' Then IfNull(SP.DispName, BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'')) Else IfNull(SP.DispName,H.SaletoPartyName) End) as ShipToPartyName,
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAddress,'') Else IfNull(Sp.Address,'') End) as ShipToPartyAddress, 
                (Case When SP.DispName Is Null Then IfNull(C.CityName,'') Else IfNull(SC.CityName,'') End) as ShipToPartyCity, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPinCode,'') Else IfNull(Sp.Pin,'') End) as ShipToPartyPincode, 
                (Case When SP.DispName Is Null Then IfNull(State.ManualCode,'') Else IfNull(SS.ManualCode,'') End) as ShipToPartyStateCode, 
                (Case When SP.DispName Is Null Then IfNull(State.Description,'') Else IfNull(SS.Description,'') End) as ShipToPartyStateName, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyMobile,'') Else IfNull(Sp.Mobile,'') End) as ShipToPartyMobile, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartySalesTaxNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'),'') End) as ShipToPartySalesTaxNo, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAadharNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.AadharNo & "'),'') End) as ShipToPartyAadharNo, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPanNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.PanNo & "'),'') End) as ShipToPartyPanNo, 
                H.ShipToAddress, H.TermsAndConditions, IfNull(Transporter.Name,'') as TransporterName, IfNull(Transporter.Mobile,'') as TransporterMobile, IfNull(Transporter.Address,'') as TransporterAddress, IfNull(TD.LrNo,'') as LrNo, TD.LrDate, IfNull(TD.PrivateMark,'') PrivateMark, TD.Weight, TD.Freight, TD.ChargedWeight, IfNull(TD.PaymentType,'') as FreightType, 
                IfNull(TD.RoadPermitNo,'') as RoadPermitNo, TD.RoadPermitDate, IfNull(TD.VehicleNo,'') as VehicleNo, IfNull(TD.ShipMethod,'') as ShipMethod, IfNull(TD.PreCarriageBy,'') PreCarriageBy, IfNull(TD.PreCarriagePlace,'') as PreCarriagePlace, IfNull(TD.BookedFrom,'') as BookedFrom, IfNull(TD.BookedTo,'') as BookedTo, IfNull(TD.Destination,'') as Destination, IfNull(TD.DescriptionOfGoods,'') as DescriptionOfGoods, IfNull(TD.DescriptionOfPacking,'') as DescriptionOfPacking, 
                IsNull((Select RegistrationNo From SubgroupRegistration Where Subcode=Transporter.Code And RegistrationType = 'Sales Tax No'),'')  as TransporterSalesTaxNo,
                IfNull(H.SaleToPartyDocNo,IfNull(L.ReferenceNo,'')) as ReferenceNo,
                I.Description as ItemName, " & IIf(mPrintFor = ClsMain.PrintFor.QA, "IG.Description", "IfNull(IG.PrintingDescription,IG.Description)") & " as ItemGroupName, 
                IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, IfNull(I.HSN, IC.HSN) as HSN, IfNull(I.MaintainStockYn, IC.MaintainStockYn) as MaintainStockYn,
                L.SalesTaxGroupItem, STGI.GrossTaxRate, 
                (Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then L.Pcs Else 0 End) as Pcs, 
                (Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then abs(L.Qty) Else 0 End) as Qty,         
                " & QryRate & " as Rate, 
                L.Unit, U.DecimalPlaces as UnitDecimalPlaces, 
                L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, L.AdditionPer, L.AdditionAmount, 
                L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as TotalDiscount, 
                abs(L.Amount)+L.DiscountAmount+L.AdditionalDiscountAmount as AmountBeforeDiscount,
                abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) as Tax1_Per, abs(L.Tax1) as Tax1, 
                abs(L.Tax2_Per) as Tax2_Per, abs(L.Tax2) as Tax2, abs(L.Tax3_Per) as Tax3_Per, abs(L.Tax3) as Tax3, 
                abs(L.Tax4_Per) as Tax4_Per, abs(L.Tax4) as Tax4, abs(L.Tax5_Per) as Tax5_Per, abs(L.Tax5) as Tax5, 
                abs(L.Net_Amount) as Net_Amount, L.Remark as LRemarks, IfNull(H.Remarks,'') as HRemarks, 
                (Select Sum(L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleInvoiceDetail L1 Where L1.DocID = H.DocID) as H_TotalDiscount, 
                (Select Sum(abs(L1.Amount)+L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleinvoiceDetail L1 Where L1.DocID = H.DocId) as H_AmountBeforeDiscount,
                abs(H.Gross_Amount) as H_Gross_Amount, 
                H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount, Abs(H.Taxable_Amount) as H_Taxable_Amount,
                Abs(H.Tax1_Per) as H_Tax1_Per, Abs(H.Tax1) as H_Tax1, H.Tax2_Per as H_Tax2_Per, abs(H.Tax2) as H_Tax2, 
                H.Tax3_Per as H_Tax3_Per, abs(H.Tax3) as H_Tax3, H.Tax4_Per as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                H.Tax5_Per as H_Tax5_Per, abs(H.Tax5) as H_Tax5, H.Deduction_Per as H_Deduction_Per, H.Deduction as H_Deduction, 
                H.Other_Charge_Per as H_Other_Charge_Per, H.Other_Charge as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, 
                IfNull(H.EInvoiceIRN,'') EInvoiceIRN, H.EInvoiceAckNo, H.EInvoiceAckDate,
                '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,'" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_PaymentLink")) & "' as PaymentLink, 
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, IfNull(L.DimensionDetail,'') as DimDetail,
                '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle,
                '" & FGetSettings(SettingFields.DocumentPrintShowPrintDateTimeYn, SettingType.General) & "' as DocumentPrintShowPrintDateTimeYn,
                '" & DocumentPrintShowPartyBalance & "' as DocumentPrintShowPartyBalance, 
                " & mOpeningBalance & " as TodaysOpeningBalance, " & mTodaysDr & " as TodaysDr, " & mTodaysCr & " as TodaysCr, " & mClosingBalance & " as TodaysClosingBalance
                from (" & bPrimaryQry & ") as H
                Left Join SaleInvoiceTrnSetting TS  With (NoLock) On H.DocId = TS.DocID
                Left Join SaleInvoiceDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Item I  With (NoLock) On L.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join Item IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join Item IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.SaleToPartyCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join SaleInvoiceTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.SaleToParty = Sg.Subcode
                Left Join Subgroup BP With (NoLock) On H.BillToParty = BP.Subcode
                Left Join Subgroup SP With (NoLock) On H.ShipToParty = SP.Subcode
                Left Join City SC With (NoLock) On SP.CityCode = SC.CityCode
                Left Join State SS with (NoLock) On SC.State = SS.Code
                Left Join RateType RT  With (NoLock) on H.RateType = Rt.Code
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join DocumentPrefix DP On VT.Category = DP.Category And H.Div_Code = DP.Div_Code                
                Left Join SiteMast Site On H.Site_Code = Site.Code
                LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                "



            If sQryPayment <> "" Then sQryPayment = sQryPayment + " Union All "

            sQryPayment = sQryPayment + "Select '" & I & "' as Copies, H.DocID,
                                    H.Sr, PM.Description AS PaymentModeName, H.Amount, H.ReferenceNo  
                                    FROM SaleInvoicePayment H
                                    LEFT JOIN PaymentMode PM ON H.PaymentMode = PM.Code 
                                    WHERE H.DocID ='" & mSearchCode & "'                                   
                                  "

        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "


        Dim objRepPrint As Object

        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            'objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.SaleToParty = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            'objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.Agent = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            FGetMailConfiguration(objRepPrint, SearchCode)
            'objRepPrint.AttachmentName = "Invoice"
        ElseIf mPrintFor = ClsMain.PrintFor.Whatsapp Then
            objRepPrint = New FrmWhatsapp(AgL)
            FGetWhatsappConfiguration(objRepPrint, SearchCode)
        Else
            objRepPrint = New AgLibrary.RepView(AgL)
        End If


        Dim sQry As String = ""
        Dim sQryRepName As String = ""

        If sQryPayment <> "" Then
            If sQry <> "" Then sQry = sQry & "^"
            If sQryRepName <> "" Then sQryRepName = sQryRepName & "^"
            sQry += sQryPayment
            sQryRepName += "PaymentDetail"
        End If


        If mDocReportFileName = "" Then
            ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, "SaleInvoice_Print.rpt", mPrintTitle, , sQry, sQryRepName, TxtSaleToParty.Tag, TxtV_Date.Text, IsPrintToPrinter,,, mSearchCode)
        Else
            ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, mDocReportFileName, mPrintTitle, , sQry, sQryRepName, TxtSaleToParty.Tag, TxtV_Date.Text, IsPrintToPrinter,,, mSearchCode)
        End If
    End Sub
    Public Sub FGetPrintCrystal_Aadhat(ObjFrm As Object, ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, IsPrintToPrinter As Boolean, BulkCondStr As String, PrintingCopiesStr As String)
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer
        Dim mQry As String
        Dim dtDoc As DataTable
        Dim mDocNoCaption As String = ""
        Dim mDocDateCaption As String = ""
        Dim mDocReportFileName As String = ""
        Dim mDocNoPrefix As String = ""
        Dim mDocPrintHeaderPattern As String = ""
        Dim mTermsAndConditions As String

        mQry = "Select H.DocID, H.Div_Code, H.Site_Code, H.V_Type, H.Structure, VT.NCat, VT.Description as V_TypeDescription,
                H.V_Date, H.SaleToParty 
                From SaleInvoice H With (NoLock) 
                Left Join Voucher_Type Vt With (NoLock) On H.V_Type = VT.V_Type
                Where H.DocID ='" & SearchCode & "' "
        dtDoc = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtDoc.Rows.Count > 0 Then
            mDocNoCaption = ClsMain.FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General, AgL.XNull(dtDoc.Rows(0)("Div_Code")), AgL.XNull(dtDoc.Rows(0)("Site_Code")), VoucherCategory.Sales, AgL.XNull(dtDoc.Rows(0)("NCAT")), AgL.XNull(dtDoc.Rows(0)("V_Type")), "", "")
            mDocDateCaption = ClsMain.FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General, AgL.XNull(dtDoc.Rows(0)("Div_Code")), AgL.XNull(dtDoc.Rows(0)("Site_Code")), VoucherCategory.Sales, AgL.XNull(dtDoc.Rows(0)("NCAT")), AgL.XNull(dtDoc.Rows(0)("V_Type")), "", "")
            mDocReportFileName = ClsMain.FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General, AgL.XNull(dtDoc.Rows(0)("Div_Code")), AgL.XNull(dtDoc.Rows(0)("Site_Code")), VoucherCategory.Sales, AgL.XNull(dtDoc.Rows(0)("NCAT")), AgL.XNull(dtDoc.Rows(0)("V_Type")), "", "")
            mDocNoPrefix = ClsMain.FGetSettings(SettingFields.DocumentPrintEntryNoPrefix, SettingType.General, AgL.XNull(dtDoc.Rows(0)("Div_Code")), AgL.XNull(dtDoc.Rows(0)("Site_Code")), VoucherCategory.Sales, AgL.XNull(dtDoc.Rows(0)("NCAT")), AgL.XNull(dtDoc.Rows(0)("V_Type")), "", "")
            mTermsAndConditions = ClsMain.FGetSettings(SettingFields.TermsAndConditions, SettingType.General, AgL.XNull(dtDoc.Rows(0)("Div_Code")), AgL.XNull(dtDoc.Rows(0)("Site_Code")), VoucherCategory.Sales, AgL.XNull(dtDoc.Rows(0)("NCAT")), AgL.XNull(dtDoc.Rows(0)("V_Type")), "", "")
            mDocPrintHeaderPattern = ClsMain.FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General, AgL.XNull(dtDoc.Rows(0)("Div_Code")), AgL.XNull(dtDoc.Rows(0)("Site_Code")), VoucherCategory.Sales, AgL.XNull(dtDoc.Rows(0)("NCAT")), AgL.XNull(dtDoc.Rows(0)("V_Type")), "", "")
            mPrintTitle = AgL.XNull(dtDoc.Rows(0)("V_TypeDescription"))

            Dim StrRate As String = ""
            If (AgL.StrCmp(AgL.PubDBName, "ShyamaShyam") Or AgL.StrCmp(AgL.PubDBName, "ShyamaShyamV")) And mDocReportFileName = "SaleInvoice_Print_Aadhat.rpt" And AgL.XNull(dtDoc.Rows(0)("Structure")) = "GstSaleMrp" Then
                mDocReportFileName = "SaleInvoice_Print_Aadhat_MRP.rpt"
                StrRate = " Round(L.Rate *100/(100+STGI.GrossTaxRate),2) AS Rate,"
                StrRate = "(Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then (Case When L.Taxable_Amount >0 And (L.Taxable_Amount <> L.Amount Or L.AdditionAmount > 0 ) Then Round(L.Rate *100/(100+STGI.GrossTaxRate),2) Else L.Rate End ) Else 0 End) as Rate, "
            Else
                StrRate = "(Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then (Case When L.Taxable_Amount >0 And (L.Taxable_Amount <> L.Amount Or L.AdditionAmount > 0 ) Then (L.Taxable_Amount - (L.DiscountAmount + L.AdditionalDiscountAmount))/L.DocQty Else L.Rate End ) Else 0 End) as Rate, "
            End If





            If AgL.XNull(dtDoc.Rows(0)("NCAT")) = Ncat.SaleInvoice Then
                    mPrintTitle = "TAX INVOICE"
                    mDocNoCaption = "Invoice No."
                    mDocDateCaption = "Invoice Date"
                End If


                'Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
                'Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
                'Dim mDocReportFileName As String = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)
                'Dim mDocNoPrefix As String = FGetSettings(SettingFields.DocumentPrintEntryNoPrefix, SettingType.General)



                Dim bPrimaryQry As String = ""
                If BulkCondStr <> "" Then
                    bPrimaryQry = " Select * From SaleInvoice  With (NoLock) Where DocID In (" & BulkCondStr & ")"
                    PrintingCopies = PrintingCopiesStr.Split(",") 'FGetSettings(SettingFields.PrintingBulkCopyCaptions, SettingType.General).ToString.Split(",")
                Else
                    bPrimaryQry = " Select * From SaleInvoice  With (NoLock) Where DocID = '" & SearchCode & "'"
                    PrintingCopies = PrintingCopiesStr.Split(",")
                End If

            'PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")


            mQry = ""
                For I = 1 To PrintingCopies.Length
                    If mQry <> "" Then mQry = mQry + " Union All "

                mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, H.DocID, L.Sr, H.V_Date, H.DeliveryDate, VT.Description as Voucher_Type, VT.NCat, '" & mDocNoPrefix & "' || H.ManualRefNo as InvoiceNo, RT.Description as RateType, IfNull(Agent.DispName,'') as AgentName, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption,
                (Case When BP.Nature = 'Cash' Then BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'') Else H.SaletoPartyName  End)  as SaleToPartyName, Sg.ManualCode as SaleToPartyCode,
                IfNull(H.SaleToPartyAddress,'') as SaleToPartyAddress, IfNull(C.CityName,'') as CityName, IfNull(H.SaleToPartyPincode,'') as SaleToPartyPincode, 
                IfNull(State.ManualCode,'') as StateCode, IfNull(State.Description,'')  as StateName, 
                IfNull(H.SaleToPartyMobile,'') as SaleToPartyMobile, Sg.ContactPerson, IfNull(H.SaleToPartySalesTaxNo,'') as SaleToPartySalesTaxNo, 
                IfNull(H.SaleToPartyAadharNo,'') as SaleToPartyAadharNo, IfNull(H.SaleToPartyPanNo,'') as SaleToPartyPanNo,
                (Case When BP.Nature = 'Cash' Then IfNull(SP.DispName, BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'')) Else IfNull(SP.DispName ,H.SaletoPartyName) End) as ShipToPartyName,
                (Case When SP.DispName Is Null Then IfNull(Sg.ManualCode,'') Else IfNull(Sp.ManualCode,'') End) as ShipToPartyManualCode, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAddress,'') Else IfNull(Sp.Address,'') End) as ShipToPartyAddress, 
                (Case When SP.DispName Is Null Then IfNull(C.CityName,'') Else IfNull(SC.CityName,'') End) as ShipToPartyCity, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPinCode,'') Else IfNull(Sp.Pin,'') End) as ShipToPartyPincode, 
                (Case When SP.DispName Is Null Then IfNull(State.ManualCode,'') Else IfNull(SS.ManualCode,'') End) as ShipToPartyStateCode, 
                (Case When SP.DispName Is Null Then IfNull(State.Description,'') Else IfNull(SS.Description,'') End) as ShipToPartyStateName, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyMobile,'') Else IfNull(Sp.Mobile,'') End) as ShipToPartyMobile, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartySalesTaxNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'),'') End) as ShipToPartySalesTaxNo, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAadharNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.AadharNo & "'),'') End) as ShipToPartyAadharNo, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPanNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.PanNo & "'),'') End) as ShipToPartyPanNo, 
                H.ShipToAddress, '" & mTermsAndConditions & "' TermsAndConditions, IfNull(Transporter.DispName,'') as TransporterName, IfNull(TD.LrNo,'') as LrNo, TD.LrDate, TD.NoOfBales, IfNull(TD.PrivateMark,'') PrivateMark, 
                TD.Weight, TD.Freight, TD.ChargedWeight, IfNull(TD.PaymentType,'') as FreightType, IfNull(TD.RoadPermitNo,'') as RoadPermitNo, TD.RoadPermitDate, IfNull(TD.VehicleNo,'') as VehicleNo, 
                IfNull(TD.ShipMethod,'') as ShipMethod, IfNull(TD.PreCarriageBy,'') PreCarriageBy, IfNull(TD.PreCarriagePlace,'') as PreCarriagePlace, IfNull(TD.BookedFrom,'') as BookedFrom, 
                IfNull(TD.BookedTo,'') as BookedTo, 
                IfNull(TD.Destination,(Case When SP.DispName Is Null Then IfNull(C.CityName,'') Else IfNull(SC.CityName,'') End)) as Destination, 
                IfNull(TD.DescriptionOfGoods,'') as DescriptionOfGoods, IfNull(TD.DescriptionOfPacking,'') as DescriptionOfPacking, 
                IfNull(L.ReferenceNo,'') as ReferenceNo, IfNull(Contra.ManualRefNo,'') as ContraDocNo,
                (Select group_concat(C1.ManualRefNo ,',') From SaleInvoice C1 Where C1.DocID = L.SaleInvoice And C1.DocID Is Not Null Group By C1.DocID) as ContraDocNoCsv,
                I.Description as ItemName, " & IIf(mPrintFor = ClsMain.PrintFor.QA, "IG.Description", "IfNull(IG.PrintingDescription,IG.Description)") & " as ItemGroupName, 
                IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, I.HSN, I.MaintainStockYn,
                L.SalesTaxGroupItem, STGI.GrossTaxRate, 
                (Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then L.Pcs Else 0 End) as Pcs, 
                (Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then abs(L.Qty) Else 0 End) as Qty,  " +
                StrRate + " L.Unit, U.DecimalPlaces as UnitDecimalPlaces, 
                Null as DiscountCalculationPattern, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, L.AdditionPer, L.AdditionAmount, 
                L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as TotalDiscount, 
                abs(L.Amount)+L.DiscountAmount+L.AdditionalDiscountAmount as AmountBeforeDiscount,
                abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) as Tax1_Per, abs(L.Tax1) as Tax1, 
                abs(L.Tax2_Per) as Tax2_Per, abs(L.Tax2) as Tax2, abs(L.Tax3_Per) as Tax3_Per, abs(L.Tax3) as Tax3, 
                abs(L.Tax4_Per) as Tax4_Per, abs(L.Tax4) as Tax4, abs(L.Tax5_Per) as Tax5_Per, abs(L.Tax5) as Tax5, 
                abs(L.Net_Amount) as Net_Amount, (Case When L.Remark Is Null And I.ItemType <> '" & ItemTypeCode.TradingProduct & "' Then I.Specification Else L.Remark End) as LRemarks, H.Remarks as HRemarks, 
                (Select Sum(L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleInvoiceDetail L1 Where L1.DocID = H.DocID) as H_TotalDiscount, 
                (Select Sum(abs(L1.Amount)+L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleinvoiceDetail L1 Where L1.DocID = H.DocId) as H_AmountBeforeDiscount,
                abs(H.Gross_Amount) as H_Gross_Amount, 
                H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount, Abs(H.Taxable_Amount) as H_Taxable_Amount,
                Abs(H.Tax1_Per) as H_Tax1_Per, Abs(H.Tax1) as H_Tax1, H.Tax2_Per as H_Tax2_Per, abs(H.Tax2) as H_Tax2, 
                H.Tax3_Per as H_Tax3_Per, abs(H.Tax3) as H_Tax3, H.Tax4_Per as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                H.Tax5_Per as H_Tax5_Per, abs(H.Tax5) as H_Tax5, H.Deduction_Per as H_Deduction_Per, H.Deduction as H_Deduction, 
                H.Other_Charge_Per as H_Other_Charge_Per, H.Other_Charge as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, 
                IfNull(H.EInvoiceIRN,'') EInvoiceIRN, H.EInvoiceAckNo, H.EInvoiceAckDate,
                '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & mDocPrintHeaderPattern & "' as DocumentPrintHeaderPattern, IfNull(L.DimensionDetail,'') as DimDetail,
                '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle,
                
                IfNull(PIH.VendorDocNo,'') as PurchInvoiceNo


                from (" & bPrimaryQry & ") as H
                Left Join SaleInvoiceTrnSetting TS  With (NoLock) On H.DocId = TS.DocID
                Left Join SaleInvoiceDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Item I  With (NoLock) On L.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join Item IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join Item IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.SaleToPartyCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join SaleInvoiceTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join Subgroup Transporter  With (NoLock) On TD.Transporter= Transporter.SubCode
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.SaleToParty = Sg.Subcode
                Left Join Subgroup BP With (NoLock) On H.BillToParty = BP.Subcode
                Left Join Subgroup SP With (NoLock) On H.ShipToParty = SP.Subcode
                Left Join City SC With (NoLock) On SP.CityCode = SC.CityCode
                Left Join State SS with (NoLock) On SC.State = SS.Code
                Left Join RateType RT  With (NoLock) on H.RateType = Rt.Code
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join DocumentPrefix DP On VT.Category = DP.Category And H.Div_Code = DP.Div_Code                
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                Left Join PurchInvoiceDetail PID With (NoLock) On PID.DocID = L.Remarks1 And PID.Sr=1
                Left Join PurchInvoice PIH With (NoLock) On PIH.DocID = PID.DocID
                Left Join SaleInvoice Contra With (NoLock) On L.SaleInvoice = Contra.DocID
                "

            Next
                mQry = mQry + " Order By Copies, H.DocID, L.Sr "


                Dim objRepPrint As Object
                If mPrintFor = ClsMain.PrintFor.EMail Then
                    objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
                    FGetMailConfiguration(objRepPrint, SearchCode)
                    objRepPrint.SearchCode = SearchCode
                    objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.EMail
                    From PurchInvoice H 
                    LEFT JOIN SubGroup Sg  On H.Vendor = Sg.SubCode
                    Where H.GenDocId = '" & SearchCode & "'", AgL.GCn).ExecuteScalar())
                ElseIf mPrintFor = ClsMain.PrintFor.Whatsapp Then
                    objRepPrint = New FrmWhatsapp(AgL)
                    FGetWhatsappConfiguration(objRepPrint, SearchCode)
                Else
                    objRepPrint = New AgLibrary.RepView(AgL)
                End If


                If mDocReportFileName = "" Then
                    ClsMain.FPrintThisDocument(ObjFrm, objRepPrint, AgL.XNull(dtDoc.Rows(0)("V_Type")), mQry, "SaleInvoice_Print.rpt", mPrintTitle, , , , AgL.XNull(dtDoc.Rows(0)("SaleToParty")), AgL.XNull(dtDoc.Rows(0)("V_Date")), IsPrintToPrinter,,, mSearchCode)
                Else
                    ClsMain.FPrintThisDocument(ObjFrm, objRepPrint, AgL.XNull(dtDoc.Rows(0)("V_Type")), mQry, mDocReportFileName, mPrintTitle, , , , AgL.XNull(dtDoc.Rows(0)("SaleToParty")), AgL.XNull(dtDoc.Rows(0)("V_Date")), IsPrintToPrinter,,, mSearchCode)
                End If
            End If
    End Sub

    Public Sub FPrintThisDocument(ByVal objFrm As Object, ByVal objRepFrm As Object, ByVal V_Type As String,
         Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "",
         Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "",
         Optional ByVal SubReport_QueryList As String = "",
         Optional ByVal SubReport_NameList As String = "", Optional ByVal PartyCode As String = "", Optional ByVal V_Date As String = "", Optional ByVal IsPrintToPrinter As Boolean = False
         )

        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String = ""
        Dim mCrd As New ReportDocument
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

            DsRep = AgL.FillData(RepQry, AgL.GCn)
            FReplaceInvoiceVariables(DsRep.Tables(0), TxtDivision.Tag, TxtSite_Code.Tag)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

            If SubReport_QueryList <> "" Then SubReport_QryArr = Split(SubReport_QueryList, "|")
            If SubReport_NameList <> "" Then SubReport_NameArr = Split(SubReport_NameList, "|")

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                If SubReport_QryArr.Length <> SubReport_NameArr.Length Then
                    MsgBox("Number Of SubReport Qries And SubReport Names Are Not Equal.", MsgBoxStyle.Information)
                    Exit Sub
                End If

                For I = 0 To SubReport_QryArr.Length - 1
                    ReDim Preserve SubReport_DataSetArr(I)
                    SubReport_DataSetArr(I) = New DataSet
                    SubReport_DataSetArr(I) = AgL.FillData(SubReport_QryArr(I).ToString, AgL.GCn)
                    AgPL.CreateFieldDefFile1(SubReport_DataSetArr(I), AgL.PubReportPath & "\" & Report_NameList & (I + 1).ToString & ".ttx", True)
                Next
            End If

            mCrd.Load(AgL.PubReportPath & "\" & RepName)
            mCrd.SetDataSource(DsRep.Tables(0))


            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                For I = 0 To SubReport_NameArr.Length - 1
                    mCrd.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
                Next
            End If

            CType(objRepFrm.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            Formula_Set(mCrd, RepTitle)
            ClsMain.Formula_Set(mCrd, CType(objFrm, AgTemplate.TempTransaction).TxtDivision.Tag, CType(objFrm, AgTemplate.TempTransaction).TxtSite_Code.Tag, CType(objFrm, AgTemplate.TempTransaction).TxtV_Type.Tag, RepTitle)
            'AgPL.Show_Report(objRepFrm, "* " & RepTitle & " *", objFrm.MdiParent)

            If IsPrintToPrinter = True Then
                mCrd.PrintToPrinter(1, True, 0, 0)
            Else
                objRepFrm.MdiParent = Me.MdiParent
                objRepFrm.Show()
            End If

            Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub


    Public Sub Formula_Set(ByVal mCRD As ReportDocument, Optional ByVal mRepTitle As String = "", Optional ByVal Date1 As String = "", Optional ByVal Date2 As String = "")
        Dim i As Integer
        For i = 0 To mCRD.DataDefinition.FormulaFields.Count - 1
            Select Case AgL.UTrim(mCRD.DataDefinition.FormulaFields(i).Name)
                Case AgL.UTrim("Title")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & mRepTitle & "'"
                Case AgL.UTrim("comp_name")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompName & "'"
                Case AgL.UTrim("comp_add")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd1 & "'"
                Case AgL.UTrim("RegOffice_FullAddress")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd1 & "'"
                Case AgL.UTrim("RegOffice_City")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd2 & "'"
                Case AgL.UTrim("comp_add1")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd2 & "'"
                Case AgL.UTrim("comp_Pin")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompPinCode & "'"
                Case AgL.UTrim("comp_phone")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompPhone & "'"
                Case AgL.UTrim("comp_city")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompCity & "'"
                Case AgL.UTrim("Title")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & mRepTitle & "'"
                Case AgL.UTrim("Division")
                    If AgL.PubDivName IsNot Nothing Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubDivName.ToUpper & " DIVISION" & "'"
                    End If
                Case AgL.UTrim("Tin_No")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & "TIN NO : " & AgL.PubCompTIN & "'"
                Case AgL.UTrim("DateBetween")
                    If Date1 <> "" And Date2 <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & "From Date " & Date1 & " To " & Date2 & " '"
                    ElseIf Date1 <> "" And Date2 = "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "' " & "For Date : " & Date1 & " '"
                    End If
                Case AgL.UTrim("DocumentHeaderRowFirstLeft")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRowFirstLeft, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRowFirstMiddle")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRowFirstMiddle, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRowFirstRight")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRowFirstRight, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow1Left")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow2Left")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow2Left, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow3Left")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow3Left, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow4Left")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow4Left, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow5Left")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow5Left, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow1Right")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow1Right, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow2Right")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow2Right, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow3Right")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow3Right, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow4Right")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow4Right, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRow5Right")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRow5Right, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRowLastLeft")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRowLastLeft, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRowLastMiddle")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRowLastMiddle, SettingType.General)) & "'"
                    End If
                Case AgL.UTrim("DocumentHeaderRowLastRight")
                    If FGetSettings(SettingFields.DocumentHeaderRow1Left, SettingType.General) <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.XNull(FGetSettings(SettingFields.DocumentHeaderRowLastRight, SettingType.General)) & "'"
                    End If
            End Select
        Next
    End Sub


    Public Function FReplaceInvoiceVariables(ByRef dtTable As DataTable, DivisionCode As String, SiteCode As String) As DataTable
        Dim I As Integer, J As Integer
        For I = 0 To dtTable.Rows.Count - 1
            For J = 0 To dtTable.Columns.Count - 1
                If AgL.XNull(dtTable.Rows(I)(J)) <> "" Then
                    dtTable.Rows(I)(J) = FReplaceInvoiceVariables(dtTable.Rows(I)(J), DivisionCode, SiteCode)
                End If
            Next J
        Next I

        FReplaceInvoiceVariables = dtTable
    End Function

    Public Function FReplaceInvoiceVariables(ByRef mText As String, DivisionCode As String, SiteCode As String) As String
        Dim mQry As String
        Dim dtTemp As DataTable

        If mText.IndexOf("<") >= 0 And mText.IndexOf(">") > 0 Then
            mText = Replace(mText, "<Default_DebtorsInterestRate>", AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")))

            mQry = "Select *
                    From SaleInvoice H  With (NoLock)
                    Where H.DocID = '" & mSearchCode & "'"
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            mText = Replace(mText, "<CreditDays>", AgL.XNull(dtTemp.Rows(0)("CreditDays")))
            ClsMain.FReplacePubVariables(mText, TxtDivision.Tag, TxtSite_Code.Tag)

        End If

        FReplaceInvoiceVariables = mText
    End Function


    Private Sub ShowSaleInvoiceParty(DocID As String, PartyCode As String, AcGroupNature As String, Optional ShowDialogForCash As Boolean = False)
        If AgL.XNull(DtV_TypeSettings.Rows(0)("SaleInvoicePattern")) = SaleInvoicePattern.PointOfSale And LblV_Type.Tag = Ncat.SaleInvoice Then
            AcGroupNature = "Cash"
        ElseIf TxtSaleToParty.Tag.ToString.ToUpper = "TEMPPARTY" Then
            AcGroupNature = "Cash"
        ElseIf AcGroupNature = "BANK" Then
            AcGroupNature = "Cash"
        End If
        If BtnFillPartyDetail.Tag IsNot Nothing Then
            CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).NCAT = LblV_Type.Tag
            CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).EntryMode = Topctrl1.Mode
            CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).DivisionCode = TxtDivision.Tag
            CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).SiteCode = TxtSite_Code.Tag
            CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).DtSaleInvoiceSettings = DtV_TypeSettings
            CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).objFrmSaleInvoice = Me
            CType(BtnFillPartyDetail.Tag, FrmSaleInvoiceParty).InvoiceAmount = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))

            BtnFillPartyDetail.Tag.ShowDialog()
        Else
            Dim FrmObj As FrmSaleInvoiceParty
            FrmObj = New FrmSaleInvoiceParty
            FrmObj.NCAT = LblV_Type.Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DivisionCode = TxtDivision.Tag
            FrmObj.SiteCode = TxtSite_Code.Tag
            FrmObj.DtSaleInvoiceSettings = DtV_TypeSettings
            FrmObj.IniGrid(DocID, PartyCode, AcGroupNature)
            FrmObj.objFrmSaleInvoice = Me
            FrmObj.InvoiceAmount = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
            BtnFillPartyDetail.Tag = FrmObj
            If AcGroupNature.ToUpper = "CASH" And ShowDialogForCash Then
                BtnFillPartyDetail.Tag.ShowDialog()
            End If
        End If
    End Sub


    Private Sub FrmSaleInvoiceDirect_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Dim DtTemp As DataTable

        ShowSaleInvoiceParty(mSearchCode, "", TxtNature.Text)
        Dgl1.ReadOnly = False

        If ClsMain.IsEntryLockedWithLockText("SaleInvoice", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If



        mQry = "Select H.* from SaleInvoiceTrnSetting H  With (NoLock)  Where DocID = '" & mSearchCode & "' "
        DtV_TypeTrnSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select IfNull(Max(Sr),0) From SaleInvoiceDimensionDetail  With (NoLock) Where DocID ='" & mSearchCode & "' "
        mDimensionSrl = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar
        mQry = "Select IfNull(Max(Sr),0) From Stock  With (NoLock) Where DocID ='" & mSearchCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If AgL.VNull(DtTemp.Rows(0)(0)) > mDimensionSrl Then
            mDimensionSrl = AgL.VNull(DtTemp.Rows(0)(0))
        End If

        mQry = " SELECT * FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "' 
                    AND IfNull(T.IsEditingAllowed,0) = 0 "
        Dim DtTransactionReferences As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Dim mTrasactionReferenceMsg As String = ""
        For I As Integer = 0 To DtTransactionReferences.Rows.Count - 1
            mTrasactionReferenceMsg += DtTransactionReferences.Rows(I)("Remark")
        Next
        If mTrasactionReferenceMsg <> "" Then
            MsgBox(mTrasactionReferenceMsg, MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If

        mQry = " SELECT Count(*) AS Cnt FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "'
                    AND IfNull(T.IsEditingAllowed,0) = 0 "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Refrential Entries Exist For This Entry.Can't Modify.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If
    End Sub

    Public Shared Narrowing Operator CType(v As FrmSaleInvoiceDirect_Aadhat) As FrmSaleInvoiceDirect
        Throw New NotImplementedException()
    End Operator

    Private Sub BtnHeaderDetail_Click(sender As Object, e As EventArgs) Handles BtnHeaderDetail.Click
        ShowSaleInvoiceHeader()
    End Sub

    Private Sub ShowSaleInvoiceHeader()
        If BtnHeaderDetail.Tag IsNot Nothing Then
            CType(BtnHeaderDetail.Tag, FrmSaleInvoiceTransport).EntryMode = Topctrl1.Mode
            CType(BtnHeaderDetail.Tag, FrmSaleInvoiceTransport).Ncat = LblV_Type.Tag
            BtnHeaderDetail.Tag.ShowDialog()
        Else
            Dim FrmObj As FrmSaleInvoiceTransport
            FrmObj = New FrmSaleInvoiceTransport
            FrmObj.Ncat = LblV_Type.Tag
            FrmObj.IniGrid(mSearchCode)
            FrmObj.EntryMode = Topctrl1.Mode

            BtnHeaderDetail.Tag = FrmObj
            BtnHeaderDetail.Tag.ShowDialog()
        End If
    End Sub

    Private Sub Dgl1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        Dim mRow As Integer
        mRow = e.RowIndex
        If Dgl1.Columns(e.ColumnIndex).Name = Col1DocQty Then ShowSaleInvoiceDimensionDetail(mRow)
    End Sub

    Private Sub Dgl1_DoubleClick(sender As Object, e As EventArgs) Handles Dgl1.DoubleClick
        If Topctrl1.Mode = "Browse" Then
            Dgl1.CurrentRow.Selected = True
        End If
    End Sub

    Private Sub Dgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dgl1.KeyPress
        If Dgl1.CurrentCell Is Nothing Then Exit Sub

        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1DocQty).Index Then
            If AgL.VNull(Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Tag) Then
                If e.KeyChar = Chr(Keys.Space) Then
                    ShowSaleInvoiceDimensionDetail(Dgl1.CurrentCell.RowIndex)
                    e.Handled = True
                End If
            End If
        End If
    End Sub


    Private Function FHPGD_PendingSaleChallan(Optional ItemCode As String = "") As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable


        mLineCond = " And S.Subcode = '" & TxtSaleToParty.Tag & "' "
        If ItemCode <> "" Then
            mLineCond = " And S.Item = '" & ItemCode & "' "
        End If

        mQry = "
                Select 'o' As Tick, SI.DocID || '#' || Cast(SI.TSr as Varchar) || '#' || Cast(SI.Sr as Varchar) as SearchKey, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNo, 
                H.V_Date as InvoiceDate, 
                SI.Item, I.Description as ItemName, SI.Qty_Iss + IfNull(SR.Qty_Ret,0) Qty_Bal, SI.Unit, SI.Rate  
                From
                    (    
                    select S.DocID, S.Tsr, S.Sr,  S.Item, S.Qty_Iss, S.Unit, S.Rate 
                    from Stock S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.NCat = 'SI' " & mLineCond & "
                    Union All 
                    select S.DocID, S.Tsr, S.Sr,  S.Item, S.Qty_Iss, S.Unit, S.Rate 
                    from StockProcess S With (NoLock)
                    Left Join Voucher_Type Vt With (NoLock) on S.V_Type = VT.V_Type
                    where VT.NCat = 'SI' " & mLineCond & "
                    ) as SI
                Left Join 
                    (
                    select S.ReferenceDocID, S.ReferenceTsr, S.ReferenceDocIDSr, Sum(S.Qty_Iss) as Qty_Ret
                    from Stock S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.nCat='SR'  " & mLineCond & "
                    Group By S.ReferenceDocID, S.ReferenceTsr, S.ReferenceDocIDSr
                    ) As SR On SI.DocID = SR.ReferenceDocID And SI.TSr = SR.ReferenceTSr And SI.Sr = SR.ReferenceDocIDSr
                Left Join SaleInvoice H  With (NoLock) On SI.DocID = H.DocID
                Left Join Item I  With (NoLock) on SI.Item = I.Code
                Left Join Voucher_Type Vt On H.V_Type = VT.V_Type
                Where  H.SaleToParty='" & TxtSaleToParty.Tag & "' And SI.Qty_Iss + IfNull(SR.Qty_Ret,0) >0
                And Date(H.V_Date) <= " & AgL.Chk_Date(TxtV_Date.Text) & "     
                Order By H.V_Date Desc, '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo Desc           
                "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            Exit Function
        End If

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 850, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Invoice No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Invoice Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, , 0, DataGridViewContentAlignment.MiddleLeft, False)
        FRH_Multiple.FFormatColumn(5, "Item", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, "Bal Qty", 80, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(7, "Unit", 70, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(8, "Rate", 70, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingSaleChallan = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FillGridForSaleReturn(strInvoiceLines As String, IsFilledFromLine As Boolean)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim DtItem As DataTable
        Dim mRow As Integer
        Dim I As Integer
        Try

            'H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, 
            mQry = "    Select H.DocID,                    
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNo,
                H.V_Date as InvoiceDate, 
                SI.Item, I.ManualCode as ItemManualCode, I.Description as ItemName, SI.Qty_Iss + IfNull(SR.Qty_Ret,0) Qty_Bal, SI.Unit, L.DiscountPer, L.AdditionalDiscountPer, L.Rate, L.MasterSaleRate, L.RateDiscountPer,
                I.ItemCategory, IC.Description as ItemCategoryName, I.ItemGroup, IG.Description as ItemGroupName,
                U.ShowDimensionDetailInSales, U.DecimalPlaces as QtyDecimalPlaces, IG.Default_DiscountPerSale, L.SalesTaxGroupItem, SI.DocID as StockDocID, SI.TSr as StockTSr, SI.Sr as StockSr ,
                L.DocId As SaleInvoice, L.Sr As SaleInvoiceSr
                From
                    (    
                    select S.DocID, S.Tsr, S.Sr,  S.Item, S.Qty_Iss, S.Unit, S.Rate 
                    from Stock S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.NCat = '" & Ncat.SaleInvoice & "'
                    Union All 
                    select S.DocID, S.Tsr, S.Sr,  S.Item, S.Qty_Iss, S.Unit, S.Rate 
                    from StockProcess S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.NCat = '" & Ncat.SaleInvoice & "'
                    ) as SI
                Left Join 
                    (
                    select S.ReferenceDocID, S.ReferenceTsr, S.ReferenceDocIDSr, Sum(S.Qty_Iss) as Qty_Ret
                    from Stock S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.nCat = '" & Ncat.SaleReturn & "'
                    Group By S.ReferenceDocID, S.ReferenceTsr, S.ReferenceDocIDSr
                    ) As SR On SI.DocID = SR.ReferenceDocID And SI.TSr = SR.ReferenceTSr And SI.Sr = SR.ReferenceDocIDSr
                Left Join SaleInvoice H  With (NoLock) On SI.DocID = H.DocID
                Left Join Item I  With (NoLock) on SI.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code 
                Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join SaleInvoiceDetail L  With (NoLock) On L.DocID = SI.DocID And L.Sr = SI.TSr
                Left Join Voucher_Type Vt On H.V_Type = VT.V_Type
                Where SI.DocID || '#' || Cast(SI.TSr as Varchar) || '#' || Cast(SI.Sr as Varchar) in (" & strInvoiceLines & ")
                "


            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                'Dgl1.Rows(Dgl1.CurrentCell.RowIndex).Visible = False
                For I = 0 To DtTemp.Rows.Count - 1
                    If I = 0 Then
                        mRow = Dgl1.CurrentCell.RowIndex
                        Dgl1.Item(ColSNo, mRow).Value = Dgl1.CurrentCell.RowIndex + 1
                    Else
                        mRow = Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, mRow).Value = Dgl1.Rows.Count - 1
                    End If

                    Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(DtTemp.Rows(I)("ItemCategoryName"))
                    Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupName"))
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("Item"))
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(I)("ItemManualCode"))
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("Item"))
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(I)("ItemName"))
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(I)("Unit"))
                    'Dgl1.Item(Col1Unit, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("ShowDimensionDetailInSales"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("SalesTaxGroupItem"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(DtTemp.Rows(I)("SalesTaxGroupItem"))
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(I)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1DefaultDiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(I)("Default_DiscountPerSale"))
                    Dgl1.Item(Col1DiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(I)("DiscountPer"))
                    Dgl1.Item(Col1AdditionalDiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(I)("AdditionalDiscountPer"))
                    Dgl1.Item(Col1SaleInvoice, mRow).Value = AgL.XNull(DtTemp.Rows(I)("DocID"))
                    Dgl1.Item(Col1MasterSaleRate, mRow).Value = AgL.VNull(DtTemp.Rows(I)("MasterSaleRate"))
                    Dgl1.Item(Col1RateDiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(I)("RateDiscountPer"))
                    If AgL.VNull(DtV_TypeSettings.Rows(0)("PickSaleRateFromMaster")) = True Then
                        If Dgl2(Col1Value, rowRateType).Value <> "" Then
                            mQry = "select Rate from RateListDetail  With (NoLock) where Item ='" & Dgl1.Item(Col1Item, mRow).Tag & "' and RateType='" & Dgl2(Col1Value, rowRateType).Tag & "'"
                            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtItem.Rows.Count > 0 Then
                                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtItem.Rows(0)("Rate"))
                            End If
                        Else
                            mQry = "select Rate from RateListDetail  With (NoLock) where Item ='" & Dgl1.Item(Col1Item, mRow).Tag & "' and RateType Is Null"
                            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtItem.Rows.Count > 0 Then
                                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtItem.Rows(0)("Rate"))
                            End If
                        End If
                        FSetPersonalDiscount(mRow)
                        FSetSalesTaxGroupItemBasedOnRate(mRow)
                    Else
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(I)("Rate"))
                    End If
                    If strInvoiceLines.Contains(",") Then
                        Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(DtTemp.Rows(I)("Qty_Bal"))
                        Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(DtTemp.Rows(I)("Qty_Bal"))
                    Else
                        Dgl1.Item(Col1Qty, mRow).Value = 0 ' AgL.VNull(DtTemp.Rows(I)("Qty_Bal"))
                        Dgl1.Item(Col1DocQty, mRow).Value = 0 ' AgL.VNull(DtTemp.Rows(I)("Qty_Bal"))
                    End If
                    Dgl1.Item(Col1ReferenceNo, mRow).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                    Dgl1.Item(Col1ReferenceDate, mRow).Value = ClsMain.FormatDate(AgL.XNull(DtTemp.Rows(I)("InvoiceDate")))
                    Dgl1.Item(Col1ReferenceDocId, mRow).Value = AgL.XNull(DtTemp.Rows(I)("StockDocID"))
                    Dgl1.Item(Col1ReferenceDocIdTSr, mRow).Value = AgL.XNull(DtTemp.Rows(I)("StockTSr"))
                    Dgl1.Item(Col1ReferenceDocIdSr, mRow).Value = AgL.XNull(DtTemp.Rows(I)("StockSr"))

                    Dgl1.Item(Col1SaleInvoice, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("SaleInvoice"))
                    Dgl1.Item(Col1SaleInvoiceSr, mRow).Value = AgL.XNull(DtTemp.Rows(I)("SaleInvoiceSr"))
                Next

                FShowTransactionHistory(AgL.XNull(DtTemp.Rows(0)("Item")))
                Calculation()
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub TxtBarcode_Validating(sender As Object, e As CancelEventArgs) Handles TxtBarcode.Validating
        Dim DtBarcode As DataTable
        Dim DtBarcodeSiteDetail As DataTable

        If TxtBarcode.Text = "" Then Exit Sub
        If Validate_Barcode(sender.text) = False Then TxtBarcode.Text = "" : e.Cancel = True : Exit Sub

        mQry = "Select * From Barcode  With (NoLock) Where Description = '" & TxtBarcode.Text & "'"
        DtBarcode = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtBarcode.Rows.Count = 0 Then
            MsgBox("Invalid Barcode")
            TxtBarcode.Text = ""
            e.Cancel = True
            Exit Sub
        Else
            If AgL.XNull(DtBarcode.Rows(0)("Div_Code")) <> TxtDivision.Tag Then
                MsgBox("Barcode does not belong to current division. Can not continue.")
                TxtBarcode.Text = ""
                e.Cancel = True
                Exit Sub
            End If

            mQry = "Select * from BarcodeSiteDetail  With (NoLock) Where Code = '" & DtBarcode.Rows(0)("Code") & "' And Div_Code='" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' "
            DtBarcodeSiteDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtBarcodeSiteDetail.Rows.Count = 0 Then
                MsgBox("No record found for barcode for current site. Can not continue.")
                TxtBarcode.Text = ""
                e.Cancel = True
                Exit Sub
            End If


            Dim mRow As Integer
            mRow = Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, mRow).Value = Dgl1.Rows.Count - 1
            Dgl1.Item(Col1Barcode, mRow).Tag = AgL.XNull(DtBarcode.Rows(0)("Code"))
            Dgl1.Item(Col1Barcode, mRow).Value = AgL.XNull(DtBarcode.Rows(0)("Description"))
            Validating_ItemCode(DtBarcode.Rows(0)("Item"), Dgl1.Columns(Col1Item).Index, mRow)
            If Dgl1.Item(Col1Item, mRow).Value = "" Then
                Dgl1.Rows(mRow).Visible = False
            Else
                Dgl1.Item(Col1Qty, mRow).Value = 1 'AgL.VNull(DtBarcode.Rows(0)("Qty"))
                Dgl1.Item(Col1DocQty, mRow).Value = 1 'AgL.VNull(DtBarcode.Rows(0)("Qty"))
            End If
            Calculation()
            Calculation()
            TxtBarcode.Text = ""
            TxtBarcode.Focus()
        End If
    End Sub

    Public Sub FImportFromExcel_Old()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtSaleInvoice As DataTable
        Dim DtSaleInvoiceDetail As DataTable
        Dim DtSaleInvoiceDimensionDetail As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_NO' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_Date' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party City' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Pincode' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Sales Tax No' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bill To Party' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Agent' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate Type' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group Party' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Place Of Supply' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Outside State / Within State' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Doc No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Doc Date' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Terms And Conditions' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Credit Limit' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Credit Days' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group Item' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pcs' as [Field Name], 'Number' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit Multiplier' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Conversion from unit to deal unit.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deal Unit' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'If billing unit is different from unit then that billing unit will be save in deal unit other wise unit will be save here.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deal Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Discount Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Discount Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Additional Discount Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Additional Discount Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bale No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Lot No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Gross_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Taxable_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax1_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'IGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax1' as [Field Name], 'Number' as [Data Type], Null as [Length], 'IGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax2_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'CGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax2' as [Field Name], 'Number' as [Data Type], Null as [Length], 'CGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax3_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'SGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax3' as [Field Name], 'Number' as [Data Type], Null as [Length], 'SGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax4_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax4' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax5_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax5' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'SubTotal1' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deduction_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deduction' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Other_Charge_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Other_Charge' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Round_Off' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Net_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select '' as Srl, 'TSr' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sr' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pcs' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'TotalQty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtSaleInvoiceDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)



        Dim ObjFrmImport As New FrmImportSaleFromExcel
        ObjFrmImport.Text = "Sale Invoice Import From Excel"
        ObjFrmImport.Dgl1.DataSource = DtSaleInvoice
        ObjFrmImport.Dgl2.DataSource = DtSaleInvoiceDetail
        ObjFrmImport.Dgl3.DataSource = DtSaleInvoiceDimensionDetail
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtSaleInvoice = ObjFrmImport.P_DsExcelData_SaleInvoice.Tables(0)
        DtSaleInvoiceDetail = ObjFrmImport.P_DsExcelData_SaleInvoiceDetail.Tables(0)
        DtSaleInvoiceDimensionDetail = ObjFrmImport.P_DsExcelData_SaleInvoiceDimensionDetail.Tables(0)


        Dim DtV_Type = DtSaleInvoice.DefaultView.ToTable(True, "V_Type")
        For I = 0 To DtV_Type.Rows.Count - 1
            If AgL.XNull(DtV_Type.Rows(I)("V_Type")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From Voucher_TYpe where V_Type = '" & AgL.XNull(DtV_Type.Rows(I)("V_Type")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Voucher Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Voucher Types Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)("V_Type")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)("V_Type")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSaleToParty = DtSaleInvoice.DefaultView.ToTable(True, "Sale To Party")
        For I = 0 To DtSaleToParty.Rows.Count - 1
            If AgL.XNull(DtSaleToParty.Rows(I)("Sale To Party")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = '" & AgL.XNull(DtSaleToParty.Rows(I)("Sale To Party")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSaleToParty.Rows(I)("Sale To Party")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSaleToParty.Rows(I)("Sale To Party")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtBillToParty = DtSaleInvoice.DefaultView.ToTable(True, "Bill To Party")
        For I = 0 To DtBillToParty.Rows.Count - 1
            If AgL.XNull(DtBillToParty.Rows(I)("Bill To Party")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = '" & AgL.XNull(DtBillToParty.Rows(I)("Bill To Party")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtBillToParty.Rows(I)("Bill To Party")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtBillToParty.Rows(I)("Bill To Party")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtAgent = DtSaleInvoice.DefaultView.ToTable(True, "Agent")
        For I = 0 To DtAgent.Rows.Count - 1
            If AgL.XNull(DtAgent.Rows(I)("Agent")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = '" & AgL.XNull(DtAgent.Rows(I)("Agent")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Agents Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Agents Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtAgent.Rows(I)("Agent")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtAgent.Rows(I)("Agent")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtRateType = DtSaleInvoice.DefaultView.ToTable(True, "Rate Type")
        For I = 0 To DtRateType.Rows.Count - 1
            If AgL.XNull(DtRateType.Rows(I)("Rate Type")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From RateTYpe where Description = '" & AgL.XNull(DtRateType.Rows(I)("Rate Type")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Rate Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Rate Types Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtRateType.Rows(I)("Rate Type")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtRateType.Rows(I)("Rate Type")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSalesTaxGroupParty = DtSaleInvoice.DefaultView.ToTable(True, "Sales Tax Group Party")
        For I = 0 To DtSalesTaxGroupParty.Rows.Count - 1
            If AgL.XNull(DtSalesTaxGroupParty.Rows(I)("Sales Tax Group Party")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxParty where Description = '" & AgL.XNull(DtSalesTaxGroupParty.Rows(I)("Sales Tax Group Party")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Sales Tax Group Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Sales Tax Group Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSalesTaxGroupParty.Rows(I)("Sales Tax Group Party")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSalesTaxGroupParty.Rows(I)("Sales Tax Group Party")) & ", "
                    End If
                End If
            End If
        Next




        For I = 0 To DtSaleInvoice.Rows.Count - 1
            If AgL.XNull(DtSaleInvoice.Rows(I)("Sale To Party")) = "" Then
                ErrorLog += "Sale To Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtSaleInvoice.Rows(I)("Bill To Party")) = "" Then
                ErrorLog += "Bill To Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtSaleInvoice.Rows(I)("Sales Tax Group Party")) = "" Then
                ErrorLog += "Sales Tax Group Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtSaleInvoice.Rows(I)("V_Date")) = "" Then
                ErrorLog += "V_Date is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtSaleInvoice.Rows(I)("V_Type")) = "" Then
                ErrorLog += "V_Type is blank at row no." + (I + 2).ToString() & vbCrLf
            End If
        Next

        Dim DtItem = DtSaleInvoiceDetail.DefaultView.ToTable(True, "Item Name")
        For I = 0 To DtItem.Rows.Count - 1
            If AgL.XNull(DtItem.Rows(I)("Item Name")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From Item where Description = '" & AgL.XNull(DtItem.Rows(I)("Item Name")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Item Names Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Item Names Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtItem.Rows(I)("Item Name")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtItem.Rows(I)("Item Name")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSalesTaxGroupItem = DtSaleInvoiceDetail.DefaultView.ToTable(True, "Sales Tax Group Item")
        For I = 0 To DtSalesTaxGroupItem.Rows.Count - 1
            If AgL.XNull(DtSalesTaxGroupItem.Rows(I)("Sales Tax Group Item")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxItem where Description = '" & AgL.XNull(DtSalesTaxGroupItem.Rows(I)("Sales Tax Group Item")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These SalesTaxGroupItems Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These SalesTaxGroupItems Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSalesTaxGroupItem.Rows(I)("Sales Tax Group Item")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSalesTaxGroupItem.Rows(I)("Sales Tax Group Item")) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtSaleInvoiceDetail.Rows.Count - 1
            If AgL.XNull(DtSaleInvoiceDetail.Rows(I)("Item Name")) = "" Then
                ErrorLog += "Item Name is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtSaleInvoiceDetail.Rows(I)("Sales Tax Group Item")) = "" Then
                ErrorLog += "Sales Tax Group Item is blank at row no." + (I + 2).ToString() & vbCrLf
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


            For I = 0 To DtSaleInvoice.Rows.Count - 1
                'Dim mDocId = AgL.GetDocId(AgL.XNull(DtSaleInvoice.Rows(I)("V_Type")), CStr(TxtV_No.Text), CDate(AgL.XNull(DtSaleInvoice.Rows(I)("V_Date"))),
                '                          AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)
                Dim mDocId = AgL.CreateDocId(AgL, "SaleInvoice", AgL.XNull(DtSaleInvoice.Rows(I)("V_Type")), CStr(TxtV_No.Text), CDate(AgL.XNull(DtSaleInvoice.Rows(I)("V_Date"))),
                                          AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)

                Dim mV_No As String = Val(AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                Dim mV_Prefix As String = AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

                Dim mSaleToParty As String = ""
                Dim mSaleToPartyName As String = ""
                Dim mSaleToPartyAddress As String = ""
                Dim mSaleToPartyCity As String = ""
                Dim mSaleToPartyMobile As String = ""
                Dim mSaleToPartySalesTaxNo As String = ""

                mQry = "SELECT Sg.SubCode As SaleToParty, Name As SaleToPartyName, Address As SaleToPartyAddress, CityCode As SaleToPartyCity, Mobile As SaleToPartyMobile, Sgr.RegistrationNo As SaleToPartySalesTaxNo
                        FROM Subgroup Sg
                        left join (Select SubCode, RegistrationNo From SubgroupRegistration Where RegistrationType = 'Sales Tax No') As Sgr On Sg.Subcode = Sgr.Subcode
                        Where Sg.Name =  '" & AgL.XNull(DtSaleInvoice.Rows(I)("Sale To Party")) & "'"
                Dim DtAcGroup As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If (DtAcGroup.Rows.Count > 0) Then
                    mSaleToParty = AgL.XNull(DtAcGroup.Rows(0)("SaleToParty"))
                    mSaleToPartyName = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartyName"))
                    mSaleToPartyAddress = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartyAddress"))
                    mSaleToPartyCity = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartyCity"))
                    mSaleToPartyMobile = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartyMobile"))
                    mSaleToPartySalesTaxNo = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartySalesTaxNo"))
                End If



                Dim mBillToParty As String = AgL.Dman_Execute("SELECT Sg.SubCode As BillToParty
                        FROM Subgroup Sg
                        Where Sg.Name =  '" & AgL.XNull(DtSaleInvoice.Rows(I)("Bill To Party")) & "'", AgL.GCn).ExecuteScalar()

                If AgL.Dman_Execute("SELECT Count(*) From SaleInvoice where V_Type = '" & AgL.XNull(DtSaleInvoice.Rows(I)("V_Type")) & "' And ReferenceNo = '" & AgL.XNull(DtSaleInvoice.Rows(I)("Manual Ref No")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                    mQry = " INSERT INTO SaleInvoice (DocID,  V_Type,  V_Prefix, V_Date,  V_No,  Div_Code,  Site_Code,
                             ReferenceNo,  SaleToParty,  BillToParty,  Agent, SaleToPartyName,  SaleToPartyAddress,
                             SaleToPartyCity,  SaleToPartyMobile, SaleToPartySalesTaxNo,  ShipToAddress,
                             RateType,  SalesTaxGroupParty, PlaceOfSupply,  Structure,
                             CustomFields,  SaleToPartyDocNo, SaleToPartyDocDate,  ReferenceDocId,
                             Remarks,  TermsAndConditions, Gross_Amount,  Taxable_Amount,
                             Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
                             Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
                             Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount,  PaidAmt,
                             CreditLimit,  CreditDays,  Status, EntryBy,  EntryDate,  ApproveBy,
                             ApproveDate,  MoveToLog,  MoveToLogDate, UploadDate)
                             Select  " & AgL.Chk_Text(mDocId) & ",  
                             " & AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("V_Type"))) & ",  
                             " & AgL.Chk_Text(mV_Prefix) & ",  
                             " & AgL.Chk_Date(AgL.XNull(DtSaleInvoice.Rows(I)("V_Date"))) & ",  
                             " & AgL.Chk_Text(mV_No) & ",  
                             " & AgL.Chk_Text(AgL.PubDivCode) & ",
                             " & AgL.Chk_Text(AgL.PubSiteCode) & ",  " & AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("Manual Ref No"))) & ",  
                             " & AgL.Chk_Text(mSaleToParty) & ", 
                             (SELECT SubCode  From SubGroup WHERE Name = '" & AgL.XNull(DtSaleInvoice.Rows(I)("Bill To Party")) & "') As BillToParty,
                             (SELECT SubCode  From SubGroup WHERE Name = '" & AgL.XNull(DtSaleInvoice.Rows(I)("Agent")) & "') As Agent,
                             " & AgL.Chk_Text(mSaleToPartyName) & ",
                             " & AgL.Chk_Text(mSaleToPartyAddress) & ",  " & AgL.Chk_Text(mSaleToPartyCity) & ",  
                             " & AgL.Chk_Text(mSaleToPartyMobile) & ", " & AgL.Chk_Text(mSaleToPartySalesTaxNo) & ",  
                             " & AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("Ship To Address"))) & ",  
                             (SELECT Code  From RateType Where Description = '" & AgL.XNull(DtSaleInvoice.Rows(I)("Rate Type")) & "') As RateType,
                             '" & AgL.XNull(DtSaleInvoice.Rows(I)("Sales Tax Group Party")) & "' As SalesTaxGroupParty,
                             " & AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("Place Of Supply"))) & ",  
                             (Select IfNull(Max(Structure),'') From Voucher_Type Where V_Type = '" & AgL.XNull(DtSaleInvoice.Rows(I)("V_Type")) & "') As Structure, 
                             Null As CustomFields,  
                              " & AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("Sale To Party Doc No"))) & ",  
                              " & AgL.Chk_Date(AgL.XNull(DtSaleInvoice.Rows(I)("Sale To Party Doc Date"))) & ",  
                              Null As ReferenceDocId,  " & AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("Remark"))) & ",  
                              " & AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("Terms And Conditions"))) & ", 
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Gross Amount")) & ",  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Taxable_Amount")) & ",  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax1_Per")) & " As Tax1_Per,
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax1")) & " As Tax1,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax2_Per")) & " As Tax2_Per,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax2")) & " As Tax2, 
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax3_Per")) & " As Tax3_Per,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax3")) & " As Tax3,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax4_Per")) & " As Tax4_Per,
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax4")) & " As Tax4,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax5_Per")) & " As Tax5_Per,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Tax5")) & " As Tax5, 
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("SubTotal1")) & " As SubTotal1,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Deduction_Per")) & " As Deduction_Per,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Deduction")) & " As Deduction,
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Other_Charge_Per")) & " As Other_Charge_Per,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Other_Charge")) & " As Other_Charge,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Round_Off")) & " As Round_Off, 
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Net_Amount")) & " As Net_Amount,  
                              0 As PaidAmt,  
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Credit Limit")) & " As CreditLimit,
                              " & AgL.VNull(DtSaleInvoice.Rows(I)("Credit Days")) & " As CreditDays,  
                              'Active' As Status,  
                              " & AgL.Chk_Text(AgL.PubUserName) & " As EntryBy, 
                              " & AgL.Chk_Date(AgL.PubLoginDate) & "  As EntryDate,  
                              Null As ApproveBy,  Null As ApproveDate,
                              Null As MoveToLog,  Null As MoveToLogDate,  Null As UploadDate"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                    Dim DtSaleInvoiceDetail_ForHeader As New DataTable
                    For M = 0 To DtSaleInvoiceDetail.Columns.Count - 1
                        Dim DColumn As New DataColumn
                        DColumn.ColumnName = DtSaleInvoiceDetail.Columns(M).ColumnName
                        DtSaleInvoiceDetail_ForHeader.Columns.Add(DColumn)
                    Next

                    Dim DtRowSaleInvoiceDetail_ForHeader As DataRow() = DtSaleInvoiceDetail.Select("V_Type = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("V_Type"))) + " And [Manual Ref No] = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("Manual Ref No"))))
                    If DtRowSaleInvoiceDetail_ForHeader.Length > 0 Then
                        For M = 0 To DtRowSaleInvoiceDetail_ForHeader.Length - 1
                            DtSaleInvoiceDetail_ForHeader.Rows.Add()
                            For N = 0 To DtSaleInvoiceDetail_ForHeader.Columns.Count - 1
                                DtSaleInvoiceDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDetail_ForHeader(M)(N)
                            Next
                        Next
                    End If

                    For J = 0 To DtSaleInvoiceDetail_ForHeader.Rows.Count - 1
                        mQry = "Insert Into SaleInvoiceDetail(DocId, Sr, Item, Specification, SalesTaxGroupItem, 
                           DocQty, FreeQty, Qty, Unit, Pcs, UnitMultiplier, DealUnit, 
                           DocDealQty, Rate, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount,  
                           Amount, Remark, BaleNo, LotNo,  
                           ReferenceDocId, ReferenceDocIdSr, 
                           SaleInvoice, SaleInvoiceSr, V_Nature, GrossWeight, NetWeight, Gross_Amount, Taxable_Amount,
                           Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, 
                           Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount)
                           Select " & AgL.Chk_Text(mDocId) & ", " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("TSr")) & ", " &
                            " (SELECT Code From Item WHERE Description = '" & AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Item Name")) & "') As Item, " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Specification"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Sales Tax Group Item"))) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Doc Qty")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Free Qty")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Qty")) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Unit"))) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Pcs")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Unit Multiplier")) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deal Unit"))) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Doc Deal Qty")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Discount Per")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Discount Amount")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Additional Discount Per")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Additional Discount Amount")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Amount")) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Remark"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Bale No"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Lot No"))) & ", " &
                            " Null As ReferenceDocId, " &
                            " Null As ReferenceDocIdSr, " &
                            " " & AgL.Chk_Text(mDocId) & " As SaleInvoice, " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("TSr")) & " As Sr, " &
                            " 'Invoice' As V_Nature,
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Gross Weight")) & ", " & "
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Net Weight")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Gross_Amount")) & ", " & "
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SubTotal1")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deduction_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deduction")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Other_Charge_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Other_Charge")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Round_Off")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Net_Amount")) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)






                        Dim DtSaleInvoiceDimensionDetail_ForHeader As New DataTable
                        For M = 0 To DtSaleInvoiceDimensionDetail.Columns.Count - 1
                            Dim DColumn As New DataColumn
                            DColumn.ColumnName = DtSaleInvoiceDimensionDetail.Columns(M).ColumnName
                            DtSaleInvoiceDimensionDetail_ForHeader.Columns.Add(DColumn)
                        Next

                        Dim DtRowSaleInvoiceDimensionDetail_ForHeader As DataRow() = DtSaleInvoiceDimensionDetail.Select("V_Type = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail.Rows(J)("V_Type"))) + " And [Manual Ref No] = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail.Rows(J)("Manual Ref No"))) + " And TSr = " + AgL.XNull(DtSaleInvoiceDetail.Rows(J)("TSr")), "TSr")
                        If DtRowSaleInvoiceDimensionDetail_ForHeader.Length > 0 Then
                            For M = 0 To DtRowSaleInvoiceDetail_ForHeader.Length - 1
                                'DtSaleInvoiceDimensionDetail_ForHeader.Rows.Add(DtRowSaleInvoiceDimensionDetail_ForHeader(M))
                                DtSaleInvoiceDetail_ForHeader.Rows.Add()
                                For N = 0 To DtSaleInvoiceDimensionDetail_ForHeader.Columns.Count - 1
                                    DtSaleInvoiceDimensionDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDimensionDetail_ForHeader(M)(N)
                                Next
                            Next
                        End If




                        For K = 0 To DtSaleInvoiceDimensionDetail_ForHeader.Rows.Count - 1
                            mQry = " INSERT INTO SaleInvoiceDimensionDetail (DocID, TSr, SR, Specification, Pcs, Qty, TotalQty) 
                                    Select " & AgL.Chk_Text(mDocId) & ", 
                                    " & AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("TSr")) & " As Sr, 
                                    " & (K + 1) & ", 
                                    " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Specification"))) & ", 
                                    " & AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Pcs")) & ", 
                                    " & AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Qty")) & ", 
                                    " & AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("TotalQty")) & " "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        Next
                    Next

                    mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                  SubCode, SalesTaxGroupParty,  Item,  LotNo, 
                                  EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                  ReferenceDocID, ReferenceDocIDSr, Rate, Amount, Landed_Value) 
                                  Select L.DocId, L.Sr, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ReferenceNo, 
                                  H.Div_Code, H.Site_Code, H.SaleToParty,  H.SalesTaxGroupParty,  L.Item,
                                  L.LotNo, 'I', 
                                  Case When  IfNull(L.Qty,0) >= 0 Then L.Qty Else 0 End As Qty_Iss, 
                                  Case When  IfNull(L.Qty,0) < 0 Then L.Qty Else 0 End As Qty_Rec, 
                                  L.Unit, L.UnitMultiplier, 
                                  Case When  IfNull(L.DealQty,0) >= 0 Then L.DealQty Else 0 End As DealQty_Iss, 
                                  Case When  IfNull(L.DealQty,0) < 0 Then L.DealQty Else 0 End As DealQty_Rec, 
                                  L.DealUnit,  
                                  L.ReferenceDocId, L.ReferenceDocIdSr, 
                                  L.Amount/L.Qty, L.Amount, L.Amount
                                  FROM SaleInvoiceDetail L    
                                  LEFT JOIN SaleInvoice H On L.DocId = H.DocId 
                                  WHERE L.DocId =  '" & mDocId & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    'For Ledger Posting

                    'mQry = " Select H.*, Vt.NCat As NCat
                    '        From (Select * From SaleInvoice  Where DocID='" & mDocId & "') H 
                    '        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type "
                    'DsTemp = AgL.FillData(mQry, AgL.GCn)


                    'AgCalcGrid1.FrmType = Me.FrmType
                    'AgCalcGrid1.AgStructure = AgL.XNull(DsTemp.Tables(0).Rows(0)("Structure"))
                    'EntryNCat = AgL.XNull(DsTemp.Tables(0).Rows(0)("NCat"))
                    'TxtV_Date.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("V_Date"))
                    'IniGrid()
                    'AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), AgL.XNull(DsTemp.Tables(0).Rows(0)("NCat")), AgL.XNull(DsTemp.Tables(0).Rows(0)("V_Date")))



                    'Dim A As Integer = 0
                    'mQry = "Select L.* From (Select * From SaleInvoiceDetail  Where DocId = '" & mDocId & "') As L 
                    '            Order By L.Sr "
                    'DsTemp = AgL.FillData(mQry, AgL.GCn)
                    'For A = 0 To DsTemp.Tables(0).Rows.Count - 1
                    '    Dgl1.Rows.Add()
                    '    Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), A)
                    'Next

                    'Dim mNarr As String = "Being goods sold To " & TxtSaleToParty.Text & ""
                    'Call ClsFunction.PostStructureLineToAccounts(AgCalcGrid1, mNarr, mDocId, AgL.PubDivCode, AgL.PubSiteCode, AgL.PubDivCode,
                    '       AgL.XNull(DtSaleInvoice.Rows(I)("V_Type")), mV_Prefix, mV_No, AgL.XNull(DtSaleInvoice.Rows(I)("Manual Ref No")),
                    '        mBillToParty, AgL.XNull(DtSaleInvoice.Rows(I)("V_Date")), AgL.GCn, AgL.ECmd)
                    'End For Ledger Posting

                    AgL.UpdateVoucherCounter(mDocId, CDate(AgL.XNull(DtSaleInvoice.Rows(I)("V_Date"))), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)


        For I = 0 To DTMaster.Rows.Count - 1
            BMBMaster.Position = I
            MoveRec()



            Dim mNarrParty As String
            Dim mNarr As String

            mNarrParty = TxtV_Type.Text
            mNarr = TxtV_Type.Text & " : " & TxtSaleToParty.Text


        Next
    End Sub

    Public Sub FImportFromExcel(bImportFor As ImportFor)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtSaleInvoice As DataTable
        Dim DtSaleInvoiceDetail As DataTable
        Dim DtSaleInvoiceDimensionDetail As DataTable
        Dim DtSaleInvoice_DataFields As DataTable
        Dim DtSaleInvoiceDetail_DataFields As DataTable
        Dim DtSaleInvoiceDimensionDetail_DataFields As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Invoice No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale To Party") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale To Party Address") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale To Party City") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale To Party Pincode") & "' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale To Party Sales Tax No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Bill To Party") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Agent") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Rate Type") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sales Tax Group Party") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Place Of Supply") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Outside State / Within State' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale To Party Doc No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale To Party Doc Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Terms And Conditions") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Credit Limit") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Credit Days") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "SubTotal1") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deduction_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deduction") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Other_Charge_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Other_Charge") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Round_Off") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Net_Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "

        DtSaleInvoice_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Invoice No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "TSr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Item Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Specification") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sales Tax Group Item") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Qty") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Unit") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Pcs") & "' as [Field Name], 'Number' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Unit Multiplier") & "' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Conversion from unit to deal unit.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deal Unit") & "' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'If billing unit is different from unit then that billing unit will be save in deal unit other wise unit will be save here.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deal Qty") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Rate") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Discount Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Discount Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Additional Discount Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Additional Discount Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Bale No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Lot No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Gross_Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Taxable_Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax1_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'IGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax1") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'IGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax2_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'CGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax2") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'CGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax3_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'SGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax3") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'SGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax4_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax4") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax5_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Tax5") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "SubTotal1") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtSaleInvoiceDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Invoice No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select '' as Srl, '" & GetFieldAliasName(bImportFor, "TSr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Specification") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Pcs") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Qty") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "TotalQty") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtSaleInvoiceDimensionDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)



        Dim ObjFrmImport As New FrmImportSaleFromExcel
        ObjFrmImport.Text = "Sale Invoice Import"
        ObjFrmImport.Dgl1.DataSource = DtSaleInvoice_DataFields
        ObjFrmImport.Dgl2.DataSource = DtSaleInvoiceDetail_DataFields
        ObjFrmImport.Dgl3.DataSource = DtSaleInvoiceDimensionDetail_DataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtSaleInvoice = ObjFrmImport.P_DsExcelData_SaleInvoice.Tables(0)
        DtSaleInvoiceDetail = ObjFrmImport.P_DsExcelData_SaleInvoiceDetail.Tables(0)
        DtSaleInvoiceDimensionDetail = ObjFrmImport.P_DsExcelData_SaleInvoiceDimensionDetail.Tables(0)

        mFlag_Import = True

        If bImportFor = ImportFor.Dos Then
            'Creation Of Packing Item
            Dim ItemTable As New FrmItemMaster.StructItem
            Dim bItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            Dim bManualCode As String = ""
            If AgL.PubServerName <> "" Then
                bManualCode = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) + 1 FROM Item  WHERE IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
            Else
                bManualCode = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) + 1 FROM Item  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
            End If


            ItemTable.Code = bItemCode
            ItemTable.ManualCode = bManualCode
            ItemTable.DisplayName = "Packing"
            ItemTable.Specification = "Packing"
            ItemTable.ItemGroupDesc = ""
            ItemTable.ItemCategoryDesc = ""
            ItemTable.Description = "Packing"
            ItemTable.ItemType = "TP"
            ItemTable.Unit = "Pcs"
            ItemTable.PurchaseRate = 0
            ItemTable.Rate = 0
            ItemTable.SalesTaxPostingGroup = "GST 5%"
            ItemTable.HSN = ""
            ItemTable.EntryBy = AgL.PubUserName
            ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            ItemTable.EntryType = "Add"
            ItemTable.EntryStatus = LogStatus.LogOpen
            ItemTable.Div_Code = AgL.PubDivCode
            ItemTable.Status = "Active"
            ItemTable.StockYN = 0
            ItemTable.IsSystemDefine = 0
            Dim DTUP As DataTable = AgL.FillData("Select '' As [UP] ", AgL.GCn).Tables(0)
            Dim FrmObj As New FrmItemMaster("", DTUP, ItemV_Type.Item)
            FrmObj.ImportItemTable(ItemTable)


            For I = 0 To DtSaleInvoice.Rows.Count - 1
                DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party")) = DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party")).ToString().Replace(" ", "")

                If DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Place Of Supply")).ToString().Trim() = "EX.U.P." Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Place Of Supply")) = PlaceOfSupplay.OutsideState
                Else
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Place Of Supply")) = PlaceOfSupplay.WithinState
                End If

                If DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party")).ToString().Trim() = "CASH A/C." Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party")) = "CASH A/C"
                End If

                If AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type"))).ToString().Trim() = "DHARA" Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type")) = "Dhara Rate"
                ElseIf AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type"))).ToString().Trim() = "NET" Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type")) = "Nett Rate"
                ElseIf AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type"))).ToString().Trim() = "SUPER NET" Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type")) = "Super Nett Rate"
                End If


                If AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString().Trim() = "N.A" Or
                        AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString().Trim() = "." Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Agent")) = ""
                End If

                If DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "G1" Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SI"
                ElseIf DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "R1" Or DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "R2" Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SR"
                ElseIf DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "S" Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SI"
                ElseIf DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "SD" Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SID"
                End If

                If DtSaleInvoice.Columns.Contains(GetFieldAliasName(bImportFor, "fv_no")) Then
                    DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Remark")) = DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "fv_no"))
                End If
            Next

            For I = 0 To DtSaleInvoiceDetail.Rows.Count - 1
                DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Item")) = DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Item")).ToString().Replace("@ ", "").Replace("@", "").Trim

                Dim bItemDesc As String = ""
                mQry = "Select I.Description 
                        From Item I
                        LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                        LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                        Where IsNull(Specification,'') = " & AgL.Chk_Text(DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")).ToString.Trim) & " 
                        And IsNull(Ig.Description,'') = " & AgL.Chk_Text(DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Group")).ToString.Trim) & " 
                        And IsNull(Ic.Description,'') = " & AgL.Chk_Text(DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Category")).ToString.Trim) & ""
                bItemDesc = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar

                If bItemDesc = "" Then
                    mQry = "Select Description From Item Where Description = " & AgL.Chk_Text(DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")).ToString.Trim) & " "
                    bItemDesc = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar

                    If bItemDesc = "" Then
                        mQry = "Select Description From Item Where Specification = " & AgL.Chk_Text(DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")).ToString.Trim) & " "
                        bItemDesc = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar
                    End If
                End If
                DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")) = bItemDesc

                If DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "G1" Then
                    DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SI"
                ElseIf DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "R1" Then
                    DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SR"
                ElseIf DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "S" Then
                    DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SI"
                ElseIf DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "SD" Then
                    DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SID"
                End If

                If DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString.Trim = "P" Then
                    DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Pcs"
                ElseIf DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString.Trim.ToUpper = "MTR" Then
                    DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Meter"
                ElseIf DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString.Trim.ToUpper = "M" Then
                    DtSaleInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Meter"
                End If




            Next



            For I = 0 To DtSaleInvoiceDimensionDetail.Rows.Count - 1
                If DtSaleInvoiceDimensionDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "G1" Then
                    DtSaleInvoiceDimensionDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SI"
                ElseIf DtSaleInvoiceDimensionDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "R1" Then
                    DtSaleInvoiceDimensionDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SR"
                ElseIf DtSaleInvoiceDimensionDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "S" Then
                    DtSaleInvoiceDimensionDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SI"
                ElseIf DtSaleInvoiceDimensionDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString().Trim = "SD" Then
                    DtSaleInvoiceDimensionDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "SID"
                End If
            Next
        End If



        Dim DtV_Type = DtSaleInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
        For I = 0 To DtV_Type.Rows.Count - 1
            If AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From Voucher_TYpe where V_Type = '" & AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Voucher Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Voucher Types Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSaleToParty = DtSaleInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Sale To Party"))
        For I = 0 To DtSaleToParty.Rows.Count - 1
            If AgL.XNull(DtSaleToParty.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Upper(RTrim(LTrim(Name))) = " & AgL.Chk_Text(AgL.XNull(DtSaleToParty.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party"))).ToString().Trim().ToUpper) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSaleToParty.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSaleToParty.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtBillToParty = DtSaleInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Bill To Party"))
        For I = 0 To DtBillToParty.Rows.Count - 1
            If AgL.XNull(DtBillToParty.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Upper(RTrim(LTrim(Name))) = " & AgL.Chk_Text(AgL.XNull(DtBillToParty.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))).ToString().Trim.ToUpper) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtBillToParty.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtBillToParty.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtAgent = DtSaleInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Agent"))
        For I = 0 To DtAgent.Rows.Count - 1
            If AgL.XNull(DtAgent.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Upper(RTrim(LTrim(Name)))  = '" & AgL.XNull(DtAgent.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString().Trim.ToUpper & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Agents Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Agents Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtAgent.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtAgent.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtRateType = DtSaleInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Rate Type"))
        For I = 0 To DtRateType.Rows.Count - 1
            If AgL.XNull(DtRateType.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From RateTYpe where Upper(RTrim(LTrim(Description)))  = '" & AgL.XNull(DtRateType.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type"))).ToString.Trim.ToUpper & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Rate Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Rate Types Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtRateType.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtRateType.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSalesTaxGroupParty = DtSaleInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Sales Tax Group Party"))
        For I = 0 To DtSalesTaxGroupParty.Rows.Count - 1
            If AgL.XNull(DtSalesTaxGroupParty.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party"))) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxParty where Description = '" & AgL.XNull(DtSalesTaxGroupParty.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party"))) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Sales Tax Group Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Sales Tax Group Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSalesTaxGroupParty.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSalesTaxGroupParty.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party"))) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtSaleInvoice_DataFields.Rows.Count - 1
            If AgL.XNull(DtSaleInvoice_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtSaleInvoice.Columns.Contains(AgL.XNull(DtSaleInvoice_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtSaleInvoice_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSaleInvoice_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If

                'For J = 0 To DtSaleInvoice_DataFields.Rows.Count - 1
                '    If AgL.XNull(DtSaleInvoice.Rows(I)(DtSaleInvoice_DataFields.Rows(J)("Field Name"))) = "" Then
                '        ErrorLog += DtSaleInvoice_DataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
                '    End If
                'Next
            End If
        Next

        'Dim DtItem = DtSaleInvoiceDetail.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Name"))
        'For I = 0 To DtItem.Rows.Count - 1
        '    If AgL.XNull(DtItem.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) <> "" Then
        '        If AgL.Dman_Execute("SELECT Count(*) From Item where Description = " & AgL.Chk_Text(AgL.XNull(DtItem.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")))) & "", AgL.GCn).ExecuteScalar = 0 Then
        '            If ErrorLog.Contains("These Item Names Are Not Present In Master") = False Then
        '                ErrorLog += vbCrLf & "These Item Names Are Not Present In Master" & vbCrLf
        '                ErrorLog += AgL.XNull(DtItem.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) & ", "
        '            Else
        '                ErrorLog += AgL.XNull(DtItem.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) & ", "
        '            End If
        '        End If
        '    End If
        'Next

        mQry = " Select Description From Item  "
        Dim DtItemTable As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Dim DtItem = DtSaleInvoiceDetail.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Name"))
        For I = 0 To DtItem.Rows.Count - 1
            If AgL.XNull(DtItem.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) <> "" Then
                Dim DtRowItem As DataRow() = DtItemTable.Select("Description = " + AgL.Chk_Text(AgL.XNull(DtItem.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")))))
                If DtRowItem.Length = 0 Then
                    If ErrorLog.Contains("These Item Names Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Item Names Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtItem.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtItem.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) & ", "
                    End If
                End If
            End If
        Next


        Dim DtSalesTaxGroupItem = DtSaleInvoiceDetail.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Sales Tax Group Item"))
        For I = 0 To DtSalesTaxGroupItem.Rows.Count - 1
            If AgL.XNull(DtSalesTaxGroupItem.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Item"))) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxItem where Description = '" & AgL.XNull(DtSalesTaxGroupItem.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Item"))) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These SalesTaxGroupItems Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These SalesTaxGroupItems Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSalesTaxGroupItem.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Item"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSalesTaxGroupItem.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Item"))) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtSaleInvoiceDetail_DataFields.Rows.Count - 1
            If AgL.XNull(DtSaleInvoiceDetail_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtSaleInvoiceDetail.Columns.Contains(AgL.XNull(DtSaleInvoiceDetail_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtSaleInvoiceDetail_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSaleInvoiceDetail_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If

                'For J = 0 To DtSaleInvoiceDetail_DataFields.Rows.Count - 1
                '    If AgL.XNull(DtSaleInvoiceDetail.Rows(I)(DtSaleInvoiceDetail_DataFields.Rows(J)("Field Name"))) = "" Then
                '        ErrorLog += DtSaleInvoiceDetail_DataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
                '    End If
                'Next
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


            For I = 0 To DtSaleInvoice.Rows.Count - 1
                Dim Tot_Gross_Amount As Double = 0
                Dim Tot_Taxable_Amount As Double = 0
                Dim Tot_Tax1 As Double = 0
                Dim Tot_Tax2 As Double = 0
                Dim Tot_Tax3 As Double = 0
                Dim Tot_Tax4 As Double = 0
                Dim Tot_Tax5 As Double = 0
                Dim Tot_SubTotal1 As Double = 0

                Dim SaleInvoiceTableList(0) As StructSaleInvoice
                Dim SaleInvoiceDimensionTableList(0) As StructSaleInvoiceDimensionDetail
                Dim SaleInvoiceTable As New StructSaleInvoice

                SaleInvoiceTable.DocID = ""
                SaleInvoiceTable.V_Type = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))
                SaleInvoiceTable.V_Prefix = ""
                SaleInvoiceTable.Site_Code = AgL.PubSiteCode
                SaleInvoiceTable.Div_Code = AgL.PubDivCode
                SaleInvoiceTable.V_No = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                SaleInvoiceTable.V_Date = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))
                SaleInvoiceTable.ManualRefNo = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Invoice No"))).ToString.Trim
                SaleInvoiceTable.SaleToParty = ""
                SaleInvoiceTable.AgentCode = ""
                SaleInvoiceTable.AgentName = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString.Trim
                SaleInvoiceTable.SaleToPartyName = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party"))).ToString.Trim
                SaleInvoiceTable.BillToPartyCode = ""
                SaleInvoiceTable.BillToPartyName = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))).ToString.Trim
                SaleInvoiceTable.SaleToPartyAddress = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party Address"))).ToString.Trim
                SaleInvoiceTable.SaleToPartyCity = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party City"))).ToString.Trim

                If DtSaleInvoice.Columns.Contains(GetFieldAliasName(bImportFor, "Sale To Party Mobile")) = True Then
                    SaleInvoiceTable.SaleToPartyMobile = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party Mobile"))).ToString.Trim
                End If


                SaleInvoiceTable.SaleToPartySalesTaxNo = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party Sales Tax No"))).ToString.Trim
                SaleInvoiceTable.ShipToAddress = ""
                SaleInvoiceTable.RateType = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Rate Type"))).ToString.Trim
                SaleInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party"))).ToString.Trim
                SaleInvoiceTable.PlaceOfSupply = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Place Of Supply"))).ToString.Trim
                SaleInvoiceTable.StructureCode = ""
                SaleInvoiceTable.CustomFields = ""

                If DtSaleInvoice.Columns.Contains(GetFieldAliasName(bImportFor, "Sale To Party Doc No")) = True Then
                    SaleInvoiceTable.SaleToPartyDocNo = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party Doc No"))).ToString.Trim
                End If

                If DtSaleInvoice.Columns.Contains(GetFieldAliasName(bImportFor, "Sale To Party Doc Date")) = True Then
                    SaleInvoiceTable.SaleToPartyDocDate = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sale To Party Doc Date")))
                End If

                SaleInvoiceTable.ReferenceDocId = ""
                SaleInvoiceTable.Remarks = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Remark")))
                SaleInvoiceTable.TermsAndConditions = AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Terms And Conditions")))
                SaleInvoiceTable.PaidAmt = 0
                SaleInvoiceTable.CreditLimit = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Credit Limit")))
                SaleInvoiceTable.CreditDays = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Credit Days")))
                SaleInvoiceTable.Status = "Active"
                SaleInvoiceTable.EntryBy = AgL.PubUserName
                SaleInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                SaleInvoiceTable.ApproveBy = ""
                SaleInvoiceTable.ApproveDate = ""
                SaleInvoiceTable.MoveToLog = ""
                SaleInvoiceTable.MoveToLogDate = ""
                SaleInvoiceTable.UploadDate = ""

                SaleInvoiceTable.Deduction_Per = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Deduction_Per")))
                SaleInvoiceTable.Deduction = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Deduction")))
                SaleInvoiceTable.Other_Charge_Per = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Other_Charge_Per")))
                SaleInvoiceTable.Other_Charge = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Other_Charge")))
                SaleInvoiceTable.Round_Off = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Round_Off")))
                SaleInvoiceTable.Net_Amount = AgL.VNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Net_Amount")))

                If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Deduction = SaleInvoiceTable.Deduction * (-1)
                If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Other_Charge = SaleInvoiceTable.Other_Charge * (-1)
                If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Round_Off = SaleInvoiceTable.Round_Off * (-1)
                If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Net_Amount = SaleInvoiceTable.Net_Amount * (-1)



                Dim DtSaleInvoiceDetail_ForHeader As New DataTable
                For M = 0 To DtSaleInvoiceDetail.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtSaleInvoiceDetail.Columns(M).ColumnName
                    DtSaleInvoiceDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowSaleInvoiceDetail_ForHeader As DataRow() = DtSaleInvoiceDetail.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "Invoice No") & "] = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Invoice No")))))
                If DtRowSaleInvoiceDetail_ForHeader.Length > 0 Then
                    For M = 0 To DtRowSaleInvoiceDetail_ForHeader.Length - 1
                        DtSaleInvoiceDetail_ForHeader.Rows.Add()
                        For N = 0 To DtSaleInvoiceDetail_ForHeader.Columns.Count - 1
                            DtSaleInvoiceDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDetail_ForHeader(M)(N)
                        Next
                    Next
                End If

                For J = 0 To DtSaleInvoiceDetail_ForHeader.Rows.Count - 1
                    SaleInvoiceTable.Line_Sr = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "TSr"))).ToString.Trim
                    SaleInvoiceTable.Line_ItemName = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Item Name"))).ToString.Trim
                    SaleInvoiceTable.Line_Specification = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Specification"))).ToString.Trim
                    SaleInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Sales Tax Group Item"))).ToString.Trim
                    If SaleInvoiceTable.V_Type = "SR" Then
                        SaleInvoiceTable.Line_ReferenceNo = "1"
                    Else
                        SaleInvoiceTable.Line_ReferenceNo = ""
                    End If

                    SaleInvoiceTable.Line_DocQty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Qty")))

                    SaleInvoiceTable.Line_FreeQty = 0
                    SaleInvoiceTable.Line_Qty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Qty")))

                    SaleInvoiceTable.Line_Unit = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Unit"))).ToString.Trim
                    SaleInvoiceTable.Line_Pcs = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Pcs")))
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Pcs = SaleInvoiceTable.Line_Pcs * (-1)

                    If DtSaleInvoiceDetail_ForHeader.Columns.Contains(GetFieldAliasName(bImportFor, "Unit Multiplier")) = True Then
                        SaleInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Unit Multiplier")))
                    Else
                        SaleInvoiceTable.Line_UnitMultiplier = 1
                    End If

                    If DtSaleInvoiceDetail_ForHeader.Columns.Contains(GetFieldAliasName(bImportFor, "Unit Multiplier")) = True Then
                        SaleInvoiceTable.Line_DealUnit = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Deal Unit"))).ToString.Trim
                    Else
                        SaleInvoiceTable.Line_DealUnit = SaleInvoiceTable.Line_Unit
                    End If

                    If DtSaleInvoiceDetail_ForHeader.Columns.Contains(GetFieldAliasName(bImportFor, "Deal Qty")) = True Then
                        SaleInvoiceTable.Line_DocDealQty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Deal Qty")))
                    Else
                        SaleInvoiceTable.Line_DocDealQty = SaleInvoiceTable.Line_Qty
                    End If



                    SaleInvoiceTable.Line_Rate = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Rate")))
                    SaleInvoiceTable.Line_DiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Discount Per")))
                    SaleInvoiceTable.Line_DiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Discount Amount")))
                    SaleInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Additional Discount Per")))
                    SaleInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Additional Discount Amount")))
                    SaleInvoiceTable.Line_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amount")))
                    SaleInvoiceTable.Line_Remark = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Remark")))
                    SaleInvoiceTable.Line_BaleNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Bale No")))
                    SaleInvoiceTable.Line_LotNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Lot No")))
                    SaleInvoiceTable.Line_ReferenceDocId = ""
                    SaleInvoiceTable.Line_ReferenceDocIdSr = ""
                    SaleInvoiceTable.Line_SaleInvoice = ""
                    SaleInvoiceTable.Line_SaleInvoiceSr = ""
                    SaleInvoiceTable.Line_V_Nature = ""
                    SaleInvoiceTable.Line_GrossWeight = 0
                    SaleInvoiceTable.Line_NetWeight = 0
                    SaleInvoiceTable.Line_Gross_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Gross_Amount")))
                    SaleInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Taxable_Amount")))
                    SaleInvoiceTable.Line_Tax1_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax1_Per")))
                    SaleInvoiceTable.Line_Tax1 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax1")))
                    SaleInvoiceTable.Line_Tax2_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax2_Per")))
                    SaleInvoiceTable.Line_Tax2 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax2")))
                    SaleInvoiceTable.Line_Tax3_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax3_Per")))
                    SaleInvoiceTable.Line_Tax3 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax3")))
                    SaleInvoiceTable.Line_Tax4_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax4_Per")))
                    SaleInvoiceTable.Line_Tax4 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax4")))
                    SaleInvoiceTable.Line_Tax5_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax5_Per")))
                    SaleInvoiceTable.Line_Tax5 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax5")))
                    SaleInvoiceTable.Line_SubTotal1 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "SubTotal1")))


                    'For Header Values
                    Tot_Gross_Amount += SaleInvoiceTable.Line_Gross_Amount
                    Tot_Taxable_Amount += SaleInvoiceTable.Line_Taxable_Amount
                    Tot_Tax1 += SaleInvoiceTable.Line_Tax1
                    Tot_Tax2 += SaleInvoiceTable.Line_Tax2
                    Tot_Tax3 += SaleInvoiceTable.Line_Tax3
                    Tot_Tax4 += SaleInvoiceTable.Line_Tax4
                    Tot_Tax5 += SaleInvoiceTable.Line_Tax5
                    Tot_SubTotal1 += SaleInvoiceTable.Line_SubTotal1

                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_DocQty = SaleInvoiceTable.Line_DocQty * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Qty = SaleInvoiceTable.Line_Qty * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_DocDealQty = SaleInvoiceTable.Line_DocDealQty * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Amount = SaleInvoiceTable.Line_Amount * (-1)

                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Gross_Amount = SaleInvoiceTable.Line_Gross_Amount * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Taxable_Amount = SaleInvoiceTable.Line_Taxable_Amount * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Tax1 = SaleInvoiceTable.Line_Tax1 * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Tax2 = SaleInvoiceTable.Line_Tax2 * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Tax3 = SaleInvoiceTable.Line_Tax3 * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Tax4 = SaleInvoiceTable.Line_Tax4 * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_Tax5 = SaleInvoiceTable.Line_Tax5 * (-1)
                    If SaleInvoiceTable.V_Type = "SR" Then SaleInvoiceTable.Line_SubTotal1 = SaleInvoiceTable.Line_SubTotal1 * (-1)


                    Dim DtSaleInvoiceDimensionDetail_ForHeader As New DataTable
                    For M = 0 To DtSaleInvoiceDimensionDetail.Columns.Count - 1
                        Dim DColumn As New DataColumn
                        DColumn.ColumnName = DtSaleInvoiceDimensionDetail.Columns(M).ColumnName
                        DtSaleInvoiceDimensionDetail_ForHeader.Columns.Add(DColumn)
                    Next

                    Dim DtRowSaleInvoiceDimensionDetail_ForHeader As DataRow() = DtSaleInvoiceDimensionDetail.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_Type")))) + " And [" & GetFieldAliasName(bImportFor, "Invoice No") & "] = " + AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Invoice No")))) + " And [" & GetFieldAliasName(bImportFor, "TSr") & "] = " + AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "TSr"))), "TSr")
                    If DtRowSaleInvoiceDimensionDetail_ForHeader.Length > 0 Then
                        For M = 0 To DtRowSaleInvoiceDimensionDetail_ForHeader.Length - 1
                            DtSaleInvoiceDimensionDetail_ForHeader.Rows.Add()
                            For N = 0 To DtSaleInvoiceDimensionDetail_ForHeader.Columns.Count - 1
                                DtSaleInvoiceDimensionDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDimensionDetail_ForHeader(M)(N)
                            Next
                        Next
                    End If




                    For K = 0 To DtSaleInvoiceDimensionDetail_ForHeader.Rows.Count - 1
                        Dim SaleInvoiceDimensionTable As New StructSaleInvoiceDimensionDetail

                        SaleInvoiceDimensionTable.TSr = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "TSr")))
                        SaleInvoiceDimensionTable.Sr = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Sr")))
                        SaleInvoiceDimensionTable.Specification = AgL.XNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Specification")))
                        SaleInvoiceDimensionTable.Pcs = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Pcs")))
                        SaleInvoiceDimensionTable.Qty = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Qty")))
                        SaleInvoiceDimensionTable.TotalQty = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "TotalQty")))

                        SaleInvoiceDimensionTableList(UBound(SaleInvoiceDimensionTableList)) = SaleInvoiceDimensionTable
                        ReDim Preserve SaleInvoiceDimensionTableList(UBound(SaleInvoiceDimensionTableList) + 1)
                    Next

                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                    ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)
                Next


                SaleInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
                SaleInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
                SaleInvoiceTableList(0).Tax1 = Tot_Tax1
                SaleInvoiceTableList(0).Tax2 = Tot_Tax2
                SaleInvoiceTableList(0).Tax3 = Tot_Tax3
                SaleInvoiceTableList(0).Tax4 = Tot_Tax4
                SaleInvoiceTableList(0).Tax5 = Tot_Tax5
                SaleInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
                SaleInvoiceTableList(0).Other_Charge = 0
                SaleInvoiceTableList(0).Deduction = 0
                SaleInvoiceTableList(0).Round_Off = Math.Round(Math.Round(SaleInvoiceTableList(0).SubTotal1) - SaleInvoiceTableList(0).SubTotal1, 2)
                SaleInvoiceTableList(0).Net_Amount = Math.Round(SaleInvoiceTableList(0).SubTotal1)


                Dim Tot_RoundOff As Double = 0
                Dim Tot_NetAmount As Double = 0
                For J = 0 To SaleInvoiceTableList.Length - 1
                    SaleInvoiceTableList(J).Line_Round_Off = Math.Round(SaleInvoiceTableList(0).Round_Off * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                    SaleInvoiceTableList(J).Line_Net_Amount = Math.Round(SaleInvoiceTableList(0).Net_Amount * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                    Tot_RoundOff += SaleInvoiceTableList(J).Line_Round_Off
                    Tot_NetAmount += SaleInvoiceTableList(J).Line_Net_Amount
                Next

                Tot_RoundOff = Math.Round(Tot_RoundOff, 2)

                If Tot_RoundOff <> SaleInvoiceTableList(0).Round_Off Then
                    SaleInvoiceTableList(0).Line_Round_Off = SaleInvoiceTableList(0).Line_Round_Off + (SaleInvoiceTableList(0).Round_Off - Tot_RoundOff)
                End If

                If Tot_NetAmount <> SaleInvoiceTableList(0).Net_Amount Then
                    SaleInvoiceTableList(0).Line_Net_Amount = SaleInvoiceTableList(0).Line_Net_Amount + (SaleInvoiceTableList(0).Net_Amount - Tot_NetAmount)
                End If


                InsertSaleInvoice(SaleInvoiceTableList, SaleInvoiceDimensionTableList)
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

            mFlag_Import = False
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
            mFlag_Import = False
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub

    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuEditSave.Click,
            MnuGenerateEWayBill.Click, MnuReconcileBill.Click, MnuEMail.Click, MnuSendSms.Click, MnuSendWhatsapp.Click, MnuSendWhatsappPDF.Click, MnuWhatsappDocument.Click, MnuReferenceEntries.Click, MnuReport.Click, MnuHistory.Click, MnuPrintBulk.Click, MnuShowLedgerPosting.Click, MnuGenerateEBill.Click
        Select Case sender.name
            Case MnuImportFromExcel.Name
                FImportFromExcel(ImportFor.Excel)

            Case MnuImportFromDos.Name
                FImportFromExcel(ImportFor.Dos)

            Case MnuImportFromTally.Name
                FImportFromTally()

            Case MnuEditSave.Name
                FEditSaveAllEntries()

            Case MnuReferenceEntries.Name
                FShowRefrentialEntries(mSearchCode)

            Case MnuGenerateEWayBill.Name
                'FCreateJSONFile()
                Dim StrSenderText As String = "EWay Bill Generation"
                GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
                GridReportFrm.Filter_IniGrid()
                Dim CRep As ClsReports = New ClsReports(GridReportFrm)
                CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                CRep.Ini_Grid()
                ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
                GridReportFrm.MdiParent = Me.MdiParent
                GridReportFrm.Show()
                CRep.ProcEWayBillGeneration(,, mSearchCode)

            Case MnuHistory.Name
                ClsMain.FShowHistory(mSearchCode, Me)

            Case MnuReconcileBill.Name
                FReconcileBill()

            Case MnuEMail.Name
                FGetPrint(mSearchCode, ClsMain.PrintFor.EMail)

            Case MnuSendSms.Name
                FSendSms()

            Case MnuSendWhatsapp.Name
                FSendWhatsapp()

            Case MnuSendWhatsappPDF.Name
                'FSendWhatsappPDF()
                PostData()

            Case MnuWhatsappDocument.Name
                If AgL.StrCmp(AgL.PubUserName, "Super") Then
                    FGetPrint(mSearchCode, ClsMain.PrintFor.Whatsapp)
                Else
                    MsgBox("This is a paid service. Contact to software vendor.", MsgBoxStyle.Information)
                End If

            Case MnuReport.Name
                Dim StrSenderText As String = "Sale Order Report"
                GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
                GridReportFrm.Filter_IniGrid()
                Dim CRep As ClsReports = New ClsReports(GridReportFrm)
                CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                CRep.Ini_Grid()
                GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 0).Value = "Voucher Type Wise Summary"
                GridReportFrm.MdiParent = Me.MdiParent
                GridReportFrm.Show()
                CRep.ProcSaleReport()

            Case MnuPrintBulk.Name
                FPrintBulk(mSearchCode)

            Case MnuShowLedgerPosting.Name
                FShowLedgerPosting()

            Case MnuGenerateEBill.Name
                Dim StrSenderText As String = Me.Text
                GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
                GridReportFrm.Filter_IniGrid()

                Dim CRep As ClsGenerateEInvoice_URL = New ClsGenerateEInvoice_URL(GridReportFrm, mSearchCode)
                CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                CRep.Ini_Grid()
                ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
                GridReportFrm.MdiParent = Me.MdiParent
                GridReportFrm.Show()
                CRep.ProcGenerateEInvoice()

        End Select
    End Sub

    Public Sub FImportFromTally()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As New DataTable
        Dim I As Integer = 0, J As Integer = 0
        Dim bHeadSubCodeName As String = ""
        Dim FileNameWithPath As String = ""


        OFDMain.Filter = "*.xml|*.XML"
        If OFDMain.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        FileNameWithPath = OFDMain.FileName

        'Dim FileNameWithPath As String = My.Application.Info.DirectoryPath & "\TallyXML\PaymentRegister.xml"
        'Dim FileNameWithPath As String = My.Application.Info.DirectoryPath & "\TallyXML\ReceiptRegister.xml"

        Dim doc As New XmlDocument()
        doc.Load(FileNameWithPath)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            If AgL.Dman_Execute("Select Count(*) From Structure  Where Code = 'GstSaleTally'", AgL.GCn).ExecuteScalar() = 0 Then
                mQry = "INSERT INTO Structure (Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt,U_AE, ModifiedBy,Edit_Date, UploadDate)
                    Select 'GstSaleTally' Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt,U_AE, ModifiedBy,Edit_Date, UploadDate
                    From Structure Where Code = 'GstSale'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "INSERT INTO StructureDetail (Code, Sr, WEF, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, 
                    DrCr, LineItem, AffectCost, InactiveDate, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, 
                    VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LinePerField, LineAmtField, GridDisplayIndex, UploadDate, Active)
                    Select 'GstSaleTally' As Code, Sr, WEF, Charges, Charge_Type, 
                    Case When Charges = 'RO' Then 'FixedValue Changeable' Else Value_Type End Value_Type, Value, 
                    Case When Charges = 'RO' Then Null Else Calculation End Calculation, 
                    BaseColumn, PostAc, PostAcFromColumn, 
                    DrCr, LineItem, AffectCost, InactiveDate, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, 
                    VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LinePerField, LineAmtField, GridDisplayIndex, UploadDate, Active
                    From StructureDetail Where Code = 'GstSale'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "UPDATE Voucher_Type Set Structure = 'GstSaleTally' Where V_Type = 'SI'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            Dim SaleInvoiceElementList As XmlNodeList = doc.GetElementsByTagName("VOUCHER")

            For I = 0 To SaleInvoiceElementList.Count - 1
                Dim SaleInvoiceTableList(0) As StructSaleInvoice
                If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST") IsNot Nothing Then
                    For J = 0 To SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Count - 1
                        Dim SaleInvoiceTable As New StructSaleInvoice

                        SaleInvoiceTable.DocID = ""

                        If SaleInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes.Count > 0 Then
                                If SaleInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "TAX INVOICE(CREDIT)" Then
                                    SaleInvoiceTable.V_Type = "SI"
                                ElseIf SaleInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "TAX INVOICE(CASH)" Then
                                    SaleInvoiceTable.V_Type = "SI"
                                ElseIf SaleInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Credit Note" Then
                                    SaleInvoiceTable.V_Type = "SR"
                                End If
                            End If
                        End If


                        SaleInvoiceTable.V_Prefix = ""
                        SaleInvoiceTable.Site_Code = AgL.PubSiteCode
                        SaleInvoiceTable.Div_Code = AgL.PubDivCode


                        '''''''''''''''''''''''''''''''''''''''''''''''''



                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''





                        If SaleInvoiceElementList(I).SelectSingleNode("VOUCHERNUMBER") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.V_No = SaleInvoiceElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes(0).Value.Replace("G", "")
                            End If
                        End If

                        If SaleInvoiceElementList(I).SelectSingleNode("DATE") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectSingleNode("DATE").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.V_Date = SaleInvoiceElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(6, 2) + "/" +
                                        SaleInvoiceElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(4, 2) + "/" +
                                        SaleInvoiceElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(0, 4)
                            End If
                        End If


                        Dim mManualrefNoPrefix As String = AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix Where V_Type = '" & SaleInvoiceTable.V_Type & "' 
                                And " & AgL.Chk_Date(SaleInvoiceTable.V_Date) & " >= Date(Date_From) 
                                And " & AgL.Chk_Date(SaleInvoiceTable.V_Date) & " <= Date(Date_To) ", AgL.GCn).ExecuteScalar()
                        SaleInvoiceTable.ManualRefNo = mManualrefNoPrefix + SaleInvoiceTable.V_No.ToString().PadLeft(4).Replace(" ", "0")




                        SaleInvoiceTable.SaleToParty = ""
                        SaleInvoiceTable.AgentCode = ""
                        SaleInvoiceTable.AgentName = ""

                        If SaleInvoiceElementList(I).SelectSingleNode("PARTYLEDGERNAME") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectSingleNode("PARTYLEDGERNAME").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.SaleToPartyName = SaleInvoiceElementList(I).SelectSingleNode("PARTYLEDGERNAME").ChildNodes(0).Value
                            End If
                        End If

                        SaleInvoiceTable.BillToPartyCode = ""
                        SaleInvoiceTable.BillToPartyName = SaleInvoiceTable.SaleToPartyName

                        SaleInvoiceTable.SaleToPartyAddress = ""
                        SaleInvoiceTable.SaleToPartyCity = ""
                        SaleInvoiceTable.SaleToPartyMobile = ""
                        SaleInvoiceTable.SaleToPartySalesTaxNo = ""
                        SaleInvoiceTable.ShipToAddress = ""
                        SaleInvoiceTable.RateType = ""

                        If SaleInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.SalesTaxGroupParty = SaleInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value
                            End If
                        End If


                        SaleInvoiceTable.PlaceOfSupply = ""
                        SaleInvoiceTable.StructureCode = ""
                        SaleInvoiceTable.CustomFields = ""
                        SaleInvoiceTable.SaleToPartyDocNo = ""
                        SaleInvoiceTable.SaleToPartyDocDate = ""
                        SaleInvoiceTable.ReferenceDocId = ""
                        SaleInvoiceTable.Remarks = "Bill No : " + SaleInvoiceElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes(0).Value
                        SaleInvoiceTable.TermsAndConditions = ""

                        SaleInvoiceTable.PaidAmt = 0
                        SaleInvoiceTable.CreditLimit = 0
                        SaleInvoiceTable.CreditDays = 0
                        SaleInvoiceTable.Status = "Active"
                        SaleInvoiceTable.EntryBy = AgL.PubUserName
                        SaleInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                        SaleInvoiceTable.ApproveBy = ""
                        SaleInvoiceTable.ApproveDate = ""
                        SaleInvoiceTable.MoveToLog = ""
                        SaleInvoiceTable.MoveToLogDate = ""
                        SaleInvoiceTable.UploadDate = ""
                        SaleInvoiceTable.Line_Sr = J + 1



                        If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("STOCKITEMNAME") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("STOCKITEMNAME").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.Line_ItemName = SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("STOCKITEMNAME").ChildNodes(0).Value
                            End If
                        End If

                        SaleInvoiceTable.Line_Specification = ""
                        SaleInvoiceTable.Line_SalesTaxGroupItem = ""

                        If SaleInvoiceElementList(I).SelectSingleNode("REFERENCE") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectSingleNode("REFERENCE").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.Line_ReferenceNo = SaleInvoiceElementList(I).SelectSingleNode("REFERENCE").ChildNodes(0).Value
                            End If
                        End If


                        If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.Line_DocQty = SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY").ChildNodes(0).Value.ToString()
                            End If
                        End If

                        SaleInvoiceTable.Line_FreeQty = 0

                        If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.Line_Qty = SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes(0).Value.ToString()


                                Dim bUnitName As String = SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes(0).Value.ToString()
                                If bUnitName.Contains("MTR") Then
                                    SaleInvoiceTable.Line_Unit = "Meter"
                                ElseIf bUnitName.Contains("PCS") Then
                                    SaleInvoiceTable.Line_Unit = "Pcs"
                                End If
                            End If
                        End If



                        If SaleInvoiceTable.Line_Unit = "" Or SaleInvoiceTable.Line_Unit Is Nothing Then
                            SaleInvoiceTable.Line_Unit = "Pcs"
                        End If




                        SaleInvoiceTable.Line_Pcs = SaleInvoiceTable.Line_DocQty
                        SaleInvoiceTable.Line_UnitMultiplier = 1
                        SaleInvoiceTable.Line_DealUnit = ""
                        SaleInvoiceTable.Line_DocDealQty = SaleInvoiceTable.Line_DocQty

                        If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.Line_Rate = SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE").ChildNodes(0).Value
                            End If
                        End If



                        SaleInvoiceTable.Line_DiscountPer = 0
                        SaleInvoiceTable.Line_DiscountAmount = 0
                        SaleInvoiceTable.Line_AdditionalDiscountPer = 0
                        SaleInvoiceTable.Line_AdditionalDiscountAmount = 0

                        If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes.Count > 0 Then
                                SaleInvoiceTable.Line_Amount = SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value
                            End If
                        End If

                        If Val(SaleInvoiceTable.Line_Qty) = 0 And Val(SaleInvoiceTable.Line_Rate) = 0 And Val(SaleInvoiceTable.Line_Amount) <> 0 Then
                            SaleInvoiceTable.Line_Qty = 1
                            SaleInvoiceTable.Line_Rate = SaleInvoiceTable.Line_Amount
                        End If

                        If SaleInvoiceTable.Line_DocQty Is Nothing Or Val(SaleInvoiceTable.Line_DocQty) = 0 Then
                            SaleInvoiceTable.Line_DocQty = SaleInvoiceTable.Line_Qty
                        End If

                        If Math.Abs(Math.Round((Math.Abs(Val(SaleInvoiceTable.Line_Amount)) / Val(SaleInvoiceTable.Line_Qty)) - Val(SaleInvoiceTable.Line_Rate), 0)) > 1 Then
                            SaleInvoiceTable.Line_Rate = Val(SaleInvoiceTable.Line_Amount) / Val(SaleInvoiceTable.Line_Qty)
                        End If

                        SaleInvoiceTable.Line_Remark = ""
                        SaleInvoiceTable.Line_BaleNo = ""
                        SaleInvoiceTable.Line_LotNo = ""
                        SaleInvoiceTable.Line_ReferenceDocId = ""
                        SaleInvoiceTable.Line_ReferenceDocIdSr = ""
                        SaleInvoiceTable.Line_SaleInvoice = ""
                        SaleInvoiceTable.Line_SaleInvoiceSr = ""
                        SaleInvoiceTable.Line_V_Nature = ""
                        SaleInvoiceTable.Line_GrossWeight = 0
                        SaleInvoiceTable.Line_NetWeight = 0



                        'SaleInvoiceTable.Line_Tax1_Per = 0
                        'SaleInvoiceTable.Line_Tax1 = 0

                        'If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST") IsNot Nothing Then
                        '    If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST") IsNot Nothing Then
                        '        If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1) IsNot Nothing Then
                        '            If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE") IsNot Nothing Then
                        '                If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE").ChildNodes.Count > 0 Then
                        '                    SaleInvoiceTable.Line_Tax2_Per = SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE").ChildNodes(0).Value
                        '                End If
                        '            End If
                        '        End If
                        '    End If
                        'End If

                        'If SaleInvoiceTable.Line_Tax2_Per = Nothing Then SaleInvoiceTable.Line_Tax2_Per = 0


                        'SaleInvoiceTable.Line_Tax2 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax2_Per / 100, 2)

                        'If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST") IsNot Nothing Then
                        '    If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST") IsNot Nothing Then
                        '        If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1) IsNot Nothing Then
                        '            If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE") IsNot Nothing Then
                        '                If SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE").ChildNodes.Count > 0 Then
                        '                    SaleInvoiceTable.Line_Tax3_Per = SaleInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE").ChildNodes(0).Value
                        '                End If
                        '            End If
                        '        End If
                        '    End If
                        'End If

                        'If SaleInvoiceTable.Line_Tax3_Per = Nothing Then SaleInvoiceTable.Line_Tax3_Per = 0


                        If SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST") IsNot Nothing Then
                            For K As Integer = 0 To SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST").Count
                                If SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K) IsNot Nothing Then
                                    If SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME") IsNot Nothing Then
                                        If SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes.Count > 0 Then
                                            If SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                    SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("5") Then
                                                SaleInvoiceTable.Line_Tax1_Per = 5
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                    SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("2.5") Then
                                                SaleInvoiceTable.Line_Tax2_Per = 2.5
                                                SaleInvoiceTable.Line_Tax3_Per = 2.5
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                    SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("12") Then
                                                SaleInvoiceTable.Line_Tax1_Per = 12
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                    SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("6") Then
                                                SaleInvoiceTable.Line_Tax2_Per = 6
                                                SaleInvoiceTable.Line_Tax3_Per = 6
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                    SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("18") Then
                                                SaleInvoiceTable.Line_Tax1_Per = 18
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                    SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("9") Then
                                                SaleInvoiceTable.Line_Tax2_Per = 9
                                                SaleInvoiceTable.Line_Tax3_Per = 9
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                    SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("28") Then
                                                SaleInvoiceTable.Line_Tax1_Per = 28
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                    SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("14") Then
                                                SaleInvoiceTable.Line_Tax2_Per = 14
                                                SaleInvoiceTable.Line_Tax3_Per = 14
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "CASH DISCOUNT" Then
                                                If J = 0 Then
                                                    SaleInvoiceTable.Line_DiscountAmount = Math.Abs(Convert.ToDouble(SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                End If
                                            ElseIf SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "ROUND OFF" Then
                                                SaleInvoiceTable.Round_Off = Math.Abs(Convert.ToDouble(SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        If SaleInvoiceTable.Line_DiscountAmount > 0 Then
                            SaleInvoiceTable.Line_Amount = SaleInvoiceTable.Line_Amount - SaleInvoiceTable.Line_DiscountAmount
                        End If

                        If SaleInvoiceTable.Line_Tax1_Per = 5 Or SaleInvoiceTable.Line_Tax2_Per = 2.5 Then
                            SaleInvoiceTable.Line_SalesTaxGroupItem = "GST 5%"
                        ElseIf SaleInvoiceTable.Line_Tax1_Per = 12 Or SaleInvoiceTable.Line_Tax2_Per = 6 Then
                            SaleInvoiceTable.Line_SalesTaxGroupItem = "GST 12%"
                        ElseIf SaleInvoiceTable.Line_Tax1_Per = 18 Or SaleInvoiceTable.Line_Tax2_Per = 9 Then
                            SaleInvoiceTable.Line_SalesTaxGroupItem = "GST 18%"
                        ElseIf SaleInvoiceTable.Line_Tax1_Per = 28 Or SaleInvoiceTable.Line_Tax2_Per = 14 Then
                            SaleInvoiceTable.Line_SalesTaxGroupItem = "GST 28%"
                        End If


                        If SaleInvoiceTable.Line_Tax1_Per > 0 Then
                            SaleInvoiceTable.PlaceOfSupply = AgLibrary.ClsMain.agConstants.PlaceOfSupplay.OutsideState
                        Else
                            SaleInvoiceTable.PlaceOfSupply = AgLibrary.ClsMain.agConstants.PlaceOfSupplay.WithinState
                        End If

                        'If SaleInvoiceTable.Line_Tax1_Per > 0 Or SaleInvoiceTable.Line_Tax2_Per > 0 Or SaleInvoiceTable.Line_Tax3_Per > 0 Then
                        '    SaleInvoiceTable.SalesTaxGroupParty = "Registered"
                        'Else
                        '    SaleInvoiceTable.SalesTaxGroupParty = "Unregistered"
                        'End If

                        If SaleInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE") IsNot Nothing Then
                            If SaleInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes.Count > 0 Then
                                If SaleInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value = "Regular" Then
                                    SaleInvoiceTable.SalesTaxGroupParty = "Registered"
                                Else
                                    SaleInvoiceTable.SalesTaxGroupParty = SaleInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value
                                End If
                            End If
                        End If

                        'If SaleInvoiceTable.SalesTaxGroupParty = "" Then SaleInvoiceTable.SalesTaxGroupParty = "Unregistered"

                        SaleInvoiceTable.Line_Gross_Amount = SaleInvoiceTable.Line_Amount
                        SaleInvoiceTable.Line_Taxable_Amount = SaleInvoiceTable.Line_Amount

                        SaleInvoiceTable.Line_Tax1 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax1_Per / 100, 2)
                        SaleInvoiceTable.Line_Tax2 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax2_Per / 100, 2)
                        SaleInvoiceTable.Line_Tax3 = Math.Round(SaleInvoiceTable.Line_Taxable_Amount * SaleInvoiceTable.Line_Tax3_Per / 100, 2)

                        SaleInvoiceTable.Line_Tax4_Per = 0
                        SaleInvoiceTable.Line_Tax4 = 0
                        SaleInvoiceTable.Line_Tax5_Per = 0
                        SaleInvoiceTable.Line_Tax5 = 0
                        SaleInvoiceTable.Line_SubTotal1 = SaleInvoiceTable.Line_Taxable_Amount + SaleInvoiceTable.Line_Tax1 + SaleInvoiceTable.Line_Tax2 + SaleInvoiceTable.Line_Tax3 + SaleInvoiceTable.Line_Tax4 + SaleInvoiceTable.Line_Tax5
                        SaleInvoiceTable.Line_Deduction_Per = 0
                        SaleInvoiceTable.Line_Deduction = 0
                        SaleInvoiceTable.Line_Other_Charge_Per = 0
                        SaleInvoiceTable.Line_Other_Charge = 0
                        SaleInvoiceTable.Line_Round_Off = 0
                        SaleInvoiceTable.Line_Net_Amount = SaleInvoiceTable.Line_SubTotal1


                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                        ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)
                    Next




                    For J = 0 To SaleInvoiceTableList.Length - 1
                        SaleInvoiceTableList(0).Gross_Amount += SaleInvoiceTableList(J).Line_Gross_Amount
                        SaleInvoiceTableList(0).Taxable_Amount += SaleInvoiceTableList(J).Line_Taxable_Amount
                        SaleInvoiceTableList(0).Tax1_Per += 0
                        SaleInvoiceTableList(0).Tax1 += SaleInvoiceTableList(J).Line_Tax1
                        SaleInvoiceTableList(0).Tax2_Per += 0
                        SaleInvoiceTableList(0).Tax2 += SaleInvoiceTableList(J).Line_Tax2
                        SaleInvoiceTableList(0).Tax3_Per += 0
                        SaleInvoiceTableList(0).Tax3 += SaleInvoiceTableList(J).Line_Tax3
                        SaleInvoiceTableList(0).Tax4_Per += 0
                        SaleInvoiceTableList(0).Tax4 += SaleInvoiceTableList(J).Line_Tax4
                        SaleInvoiceTableList(0).Tax5_Per += 0
                        SaleInvoiceTableList(0).Tax5 += SaleInvoiceTableList(J).Line_Tax5
                        SaleInvoiceTableList(0).SubTotal1 += SaleInvoiceTableList(J).Line_SubTotal1
                        SaleInvoiceTableList(0).Deduction_Per += 0
                        SaleInvoiceTableList(0).Deduction += SaleInvoiceTableList(J).Line_Deduction
                        SaleInvoiceTableList(0).Other_Charge_Per += 0
                        SaleInvoiceTableList(0).Other_Charge += SaleInvoiceTableList(J).Line_Other_Charge
                        SaleInvoiceTableList(0).Round_Off = 0
                        SaleInvoiceTableList(0).Net_Amount += SaleInvoiceTableList(J).Line_Net_Amount
                    Next

                    SaleInvoiceTableList(0).Deduction = Math.Round(SaleInvoiceTableList(0).Deduction, 2)
                    SaleInvoiceTableList(0).Other_Charge = Math.Round(SaleInvoiceTableList(0).Other_Charge, 2)

                    SaleInvoiceTableList(0).Net_Amount = Math.Round(SaleInvoiceTableList(0).Net_Amount + SaleInvoiceTableList(0).Round_Off, 2)

                    Dim mTallyNetAmount As Double = 0
                    If SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST") IsNot Nothing Then
                        For J = 0 To SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST").Count - 1
                            If SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST").Item(J).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = SaleInvoiceTableList(0).SaleToPartyName Then
                                mTallyNetAmount = Math.Abs(Convert.ToDouble(SaleInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            End If
                        Next
                    End If

                    If mTallyNetAmount > 0 And SaleInvoiceTableList(0).V_Type = "SR" Then
                        mTallyNetAmount = -mTallyNetAmount
                    End If

                    If mTallyNetAmount < 0 Then
                        If mTallyNetAmount > SaleInvoiceTableList(0).Net_Amount Then
                            SaleInvoiceTableList(0).Deduction += Math.Round(SaleInvoiceTableList(0).Net_Amount - mTallyNetAmount, 2)
                        ElseIf mTallyNetAmount < SaleInvoiceTableList(0).Net_Amount Then
                            SaleInvoiceTableList(0).Other_Charge += Math.Round(mTallyNetAmount - SaleInvoiceTableList(0).Net_Amount, 2)
                        End If
                    Else
                        If mTallyNetAmount > SaleInvoiceTableList(0).Net_Amount Then
                            SaleInvoiceTableList(0).Other_Charge += Math.Round(mTallyNetAmount - SaleInvoiceTableList(0).Net_Amount, 2)
                        ElseIf mTallyNetAmount < SaleInvoiceTableList(0).Net_Amount Then
                            SaleInvoiceTableList(0).Deduction += Math.Round(SaleInvoiceTableList(0).Net_Amount - mTallyNetAmount, 2)
                        End If
                    End If


                    SaleInvoiceTableList(0).Net_Amount = Math.Round(SaleInvoiceTableList(0).Net_Amount + SaleInvoiceTableList(0).Other_Charge - SaleInvoiceTableList(0).Deduction, 2)

                    InsertSaleInvoice(SaleInvoiceTableList)
                End If
            Next I

            mQry = "UPDATE Voucher_Type Set Structure = 'GstSale' Where V_Type = 'SI'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FEditSaveAllEntries()
        mFlag_Import = True
        For I As Integer = 0 To DTMaster.Rows.Count - 1
            BMBMaster.Position = I
            'MoveRec()
            Topctrl1.FButtonClick(1)
            Calculation()
            Topctrl1.FButtonClick(13)
        Next
        mFlag_Import = False
    End Sub


    Public Shared Function InsertSaleInvoice(SaleInvoiceTableList As StructSaleInvoice(), Optional SaleInvoiceDimensionTableList As StructSaleInvoiceDimensionDetail() = Nothing) As String
        Dim mQry As String = ""

        If SaleInvoiceTableList(0).V_Type IsNot Nothing Then
            'SaleInvoiceTableList(0).DocID = AgL.GetDocId(SaleInvoiceTableList(0).V_Type, CStr(SaleInvoiceTableList(0).V_No),
            '                                         CDate(SaleInvoiceTableList(0).V_Date),
            '                                        IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), SaleInvoiceTableList(0).Div_Code, SaleInvoiceTableList(0).Site_Code)
            SaleInvoiceTableList(0).DocID = AgL.CreateDocId(AgL, "SaleInvoice", SaleInvoiceTableList(0).V_Type, CStr(SaleInvoiceTableList(0).V_No),
                                                     CDate(SaleInvoiceTableList(0).V_Date),
                                                    IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), SaleInvoiceTableList(0).Div_Code, SaleInvoiceTableList(0).Site_Code)


            SaleInvoiceTableList(0).V_Prefix = AgL.DeCodeDocID(SaleInvoiceTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            SaleInvoiceTableList(0).V_No = Val(AgL.DeCodeDocID(SaleInvoiceTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))

            Dim bNCat As String = AgL.XNull(AgL.Dman_Execute("Select NCat From Voucher_Type Where V_Type = '" & SaleInvoiceTableList(0).V_Type & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

            'If AgL.Dman_Execute("Select Count(*) From SaleInvoice With (NoLock) Where V_Type = '" & SaleInvoiceTableList(0).V_Type & "'
            '            And ManualRefNo = '" & SaleInvoiceTableList(0).ManualRefNo & "'
            '            And Div_Code = '" & SaleInvoiceTableList(0).Div_Code & "'
            '            And Site_Code = '" & SaleInvoiceTableList(0).Site_Code & "'
            '            And V_Prefix = '" & SaleInvoiceTableList(0).V_Prefix & "'
            '                ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() > 0 Or
            '            SaleInvoiceTableList(0).ManualRefNo = "" Then
            '    Dim mManualrefNoPrefix As String = AgL.XNull(AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix With (NoLock) Where V_Type = '" & SaleInvoiceTableList(0).V_Type & "' 
            '                    And " & AgL.Chk_Date(SaleInvoiceTableList(0).V_Date) & " >= Date(Date_From) 
            '                    And " & AgL.Chk_Date(SaleInvoiceTableList(0).V_Date) & " <= Date(Date_To) ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            '    SaleInvoiceTableList(0).ManualRefNo = mManualrefNoPrefix + SaleInvoiceTableList(0).V_No.ToString().PadLeft(4).Replace(" ", "0")
            'End If

            If SaleInvoiceTableList(0).ManualRefNo = "" Then
                SaleInvoiceTableList(0).ManualRefNo = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleInvoice",
                                SaleInvoiceTableList(0).V_Type, SaleInvoiceTableList(0).V_Date,
                                SaleInvoiceTableList(0).Div_Code, SaleInvoiceTableList(0).Site_Code,
                                AgTemplate.ClsMain.ManualRefType.Max)
            End If

            SaleInvoiceTableList(0).SaleToPartyCity = AgL.Dman_Execute("SELECT C.CityCode FROM City C With (NoLock) Where C.CityName =  '" & SaleInvoiceTableList(0).SaleToPartyCity & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()


            Dim DtSubGroup As DataTable = Nothing
            If SaleInvoiceTableList(0).SaleToParty <> "" Then
                mQry = "SELECT Sg.SubCode As SaleToParty, Name As SaleToPartyName, Address As SaleToPartyAddress, CityCode As SaleToPartyCity, Mobile As SaleToPartyMobile, Sgr.RegistrationNo As SaleToPartySalesTaxNo
                        FROM Subgroup Sg With (NoLock) 
                        left join (Select SubCode, RegistrationNo From SubgroupRegistration Where RegistrationType = 'Sales Tax No') As Sgr On Sg.Subcode = Sgr.Subcode
                        Where Sg.SubCode =  " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToParty) & ""
                DtSubGroup = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            ElseIf SaleInvoiceTableList(0).SaleToPartyName <> "" Then
                mQry = "SELECT Sg.SubCode As SaleToParty, Name As SaleToPartyName, Address As SaleToPartyAddress, CityCode As SaleToPartyCity, Mobile As SaleToPartyMobile, Sgr.RegistrationNo As SaleToPartySalesTaxNo
                        FROM Subgroup Sg With (NoLock) 
                        left join (Select SubCode, RegistrationNo From SubgroupRegistration Where RegistrationType = 'Sales Tax No') As Sgr On Sg.Subcode = Sgr.Subcode
                        Where Upper(RTrim(LTrim(Sg.Name))) =  " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToPartyName.ToString().Trim().ToUpper) & ""
                DtSubGroup = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
            End If
            If DtSubGroup IsNot Nothing Then
                If (DtSubGroup.Rows.Count > 0) Then
                    SaleInvoiceTableList(0).SaleToParty = AgL.XNull(DtSubGroup.Rows(0)("SaleToParty"))
                    SaleInvoiceTableList(0).SaleToPartyName = AgL.XNull(DtSubGroup.Rows(0)("SaleToPartyName"))
                    If SaleInvoiceTableList(0).SaleToPartyAddress = "" Then SaleInvoiceTableList(0).SaleToPartyAddress = AgL.XNull(DtSubGroup.Rows(0)("SaleToPartyAddress"))
                    If SaleInvoiceTableList(0).SaleToPartyCity = "" Then SaleInvoiceTableList(0).SaleToPartyCity = AgL.XNull(DtSubGroup.Rows(0)("SaleToPartyCity"))
                    If SaleInvoiceTableList(0).SaleToPartyMobile = "" Then SaleInvoiceTableList(0).SaleToPartyMobile = AgL.XNull(DtSubGroup.Rows(0)("SaleToPartyMobile"))
                    If SaleInvoiceTableList(0).SaleToPartySalesTaxNo = "" Then SaleInvoiceTableList(0).SaleToPartySalesTaxNo = AgL.XNull(DtSubGroup.Rows(0)("SaleToPartySalesTaxNo"))
                End If
            End If

            If SaleInvoiceTableList(0).SaleToParty <> "" Then
                If AgL.XNull(AgL.Dman_Execute("Select SubGRoupType From SubGroup With (NoLock) Where SubCode = '" & SaleInvoiceTableList(0).SaleToParty & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = "" Then
                    mQry = "UPDATE SubGroup Set SubGroupType = '" & SubgroupType.Customer & "' Where SubCode = '" & SaleInvoiceTableList(0).SaleToParty & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If

            If SaleInvoiceTableList(0).AgentCode <> "" Then
                If AgL.XNull(AgL.Dman_Execute("Select SubGroupType From SubGroup With (NoLock) Where SubCode = '" & SaleInvoiceTableList(0).AgentCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = "" Then
                    mQry = "UPDATE SubGroup Set SubGroupType = '" & SubgroupType.SalesAgent & "' Where SubCode = '" & SaleInvoiceTableList(0).AgentCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If

            If SaleInvoiceTableList(0).BillToPartyCode = "" Then
                SaleInvoiceTableList(0).BillToPartyCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Upper(RTrim(LTrim(Sg.Name))) =  '" & SaleInvoiceTableList(0).BillToPartyName.ToString().Trim().ToUpper & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If SaleInvoiceTableList(0).BillToPartyCode = "" Or SaleInvoiceTableList(0).BillToPartyCode Is Nothing Then
                SaleInvoiceTableList(0).BillToPartyCode = SaleInvoiceTableList(0).SaleToParty
            End If

            If SaleInvoiceTableList(0).AgentCode = "" Then
                SaleInvoiceTableList(0).AgentCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  '" & SaleInvoiceTableList(0).AgentName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If SaleInvoiceTableList(0).StructureCode = "" Then
                SaleInvoiceTableList(0).StructureCode = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type With (NoLock) Where V_Type = '" & SaleInvoiceTableList(0).V_Type & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If SaleInvoiceTableList(0).SaleToPartyMobile.Length > 10 Then
                SaleInvoiceTableList(0).SaleToPartyMobile = SaleInvoiceTableList(0).SaleToPartyMobile.Substring(0, 9)
            End If

            If SaleInvoiceTableList(0).SaleToPartyAddress.Length > 100 Then
                SaleInvoiceTableList(0).SaleToPartyAddress = SaleInvoiceTableList(0).SaleToPartyAddress.Substring(0, 99)
            End If

            If SaleInvoiceTableList(0).SalesTaxGroupParty Is Nothing Or SaleInvoiceTableList(0).SalesTaxGroupParty = "" Then
                SaleInvoiceTableList(0).SalesTaxGroupParty = AgL.Dman_Execute("Select IfNull(SalesTaxPostingGroup,'') From Subgroup With (NoLock) Where SubCode = '" & SaleInvoiceTableList(0).BillToPartyCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If SaleInvoiceTableList(0).SalesTaxGroupParty Is Nothing Or SaleInvoiceTableList(0).SalesTaxGroupParty = "" Then
                SaleInvoiceTableList(0).SalesTaxGroupParty = "Unregistered"
            End If

            SaleInvoiceTableList(0).RateType = AgL.Dman_Execute("Select Code From RateType With (NoLock) Where Description =  '" & SaleInvoiceTableList(0).RateType & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            If AgL.XNull(SaleInvoiceTableList(0).SaleToParty) = "" Then
                Err.Raise(1,, "Sale To Party is Empty.")
            End If
            If AgL.XNull(SaleInvoiceTableList(0).BillToPartyCode) = "" Then
                Err.Raise(1,, "Bill To Party is Empty.")
            End If


            'If AgL.Dman_Execute("SELECT Count(*) From SaleInvoice where V_Type = '" & SaleInvoiceTableList(0).V_Type & "' And ManualRefNo = '" & SaleInvoiceTableList(0).ManualRefNo & "' ", AgL.GCn).ExecuteScalar = 0 Then
            mQry = " INSERT INTO SaleInvoice (DocID,  V_Type,  V_Prefix, V_Date,  V_No,  Div_Code,  Site_Code,
                             ManualRefNo,  SaleToParty,  BillToParty,  Agent, SaleToPartyName,  SaleToPartyAddress,
                             SaleToPartyCity,  SaleToPartyMobile, SaleToPartySalesTaxNo, ShipToParty,  ShipToAddress,
                             RateType,  SalesTaxGroupParty, PlaceOfSupply,  Structure,
                             CustomFields,  SaleToPartyDocNo, SaleToPartyDocDate,  ReferenceDocId,
                             Remarks, Tags,  TermsAndConditions, Gross_Amount,  Taxable_Amount,
                             Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
                             Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
                             Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount,  PaidAmt,
                             CreditLimit,  CreditDays,  Status, EntryBy,  EntryDate,  ApproveBy,
                             ApproveDate,  MoveToLog,  MoveToLogDate, UploadDate, LockText, OMSId)
                             Select  " & AgL.Chk_Text(SaleInvoiceTableList(0).DocID) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).V_Type) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).V_Prefix) & ",  
                             " & AgL.Chk_Date(SaleInvoiceTableList(0).V_Date) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).V_No) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).Div_Code) & ",
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).Site_Code) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).ManualRefNo) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToParty) & ", 
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).BillToPartyCode) & ", 
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).AgentCode) & ", 
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToPartyName) & ",
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToPartyAddress) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToPartyCity) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToPartyMobile) & ", 
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToPartySalesTaxNo) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).ShipToPartyCode) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).ShipToAddress) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).RateType) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).SalesTaxGroupParty) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).PlaceOfSupply) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).StructureCode) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).CustomFields) & ",  
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).SaleToPartyDocNo) & ",  
                             " & AgL.Chk_Date(SaleInvoiceTableList(0).SaleToPartyDocDate) & ",
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).ReferenceDocId) & ",    
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).Remarks) & ",    
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).Tags) & ",    
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).TermsAndConditions) & ",    
                             " & Val(SaleInvoiceTableList(0).Gross_Amount) & ",    
                             " & Val(SaleInvoiceTableList(0).Taxable_Amount) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax1_Per) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax1) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax2_Per) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax2) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax3_Per) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax3) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax4_Per) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax4) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax5_Per) & ",    
                             " & Val(SaleInvoiceTableList(0).Tax5) & ",    
                             " & Val(SaleInvoiceTableList(0).SubTotal1) & ",    
                             " & Val(SaleInvoiceTableList(0).Deduction_Per) & ",    
                             " & Val(SaleInvoiceTableList(0).Deduction) & ",    
                             " & Val(SaleInvoiceTableList(0).Other_Charge_Per) & ",    
                             " & Val(SaleInvoiceTableList(0).Other_Charge) & ",    
                             " & Val(SaleInvoiceTableList(0).Round_Off) & ",    
                             " & Val(SaleInvoiceTableList(0).Net_Amount) & ",    
                             " & Val(SaleInvoiceTableList(0).PaidAmt) & ",    
                             " & Val(SaleInvoiceTableList(0).CreditLimit) & ",    
                             " & Val(SaleInvoiceTableList(0).CreditDays) & ",    
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).Status) & ",    
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).EntryBy) & ",    
                             " & AgL.Chk_Date(SaleInvoiceTableList(0).EntryDate) & ",    
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).ApproveBy) & ",    
                             " & AgL.Chk_Date(SaleInvoiceTableList(0).ApproveDate) & ",    
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).MoveToLog) & ",    
                             " & AgL.Chk_Date(SaleInvoiceTableList(0).MoveToLogDate) & ",    
                             " & AgL.Chk_Date(SaleInvoiceTableList(0).UploadDate) & ",
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).LockText) & ",    
                             " & AgL.Chk_Text(SaleInvoiceTableList(0).OmsId) & ""
            Debug.Print(mQry)
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = " Insert Into SaleInvoiceTrnSetting
                        (DocID, IsPostedInStock, IsPostedInLedger)
                        Values (" & AgL.Chk_Text(SaleInvoiceTableList(0).DocID) & ", 1,1)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            For I As Integer = 0 To SaleInvoiceTableList.Length - 1
                If SaleInvoiceTableList(I).Line_ItemName IsNot Nothing Then

                    If SaleInvoiceTableList(I).Line_ItemCode = "" Then
                        SaleInvoiceTableList(I).Line_ItemCode = AgL.Dman_Execute("SELECT Code FROM Item With (NoLock) Where Description =  " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_ItemName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

                        If SaleInvoiceTableList(I).Line_ItemCode = "" Or SaleInvoiceTableList(I).Line_ItemCode Is Nothing Then
                            SaleInvoiceTableList(I).Line_ItemCode = AgL.Dman_Execute("SELECT Code FROM Item With (NoLock) Where Specification  =  '" & SaleInvoiceTableList(I).Line_ItemName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                        End If

                        If SaleInvoiceTableList(I).Line_ItemCode = "" Then
                            Err.Raise(1,, "Item is saving blank in sale invoice detail for " & SaleInvoiceTableList(0).ManualRefNo)
                        End If
                    End If

                    mQry = "Insert Into SaleInvoiceDetail(DocId, Sr, Item, Specification, SalesTaxGroupItem, 
                           DocQty, FreeQty, Qty, Unit, Pcs, UnitMultiplier, DealUnit, 
                           DocDealQty, Rate, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount,  
                           AdditionPer, AdditionAmount,Amount, Remark, BaleNo, LotNo,  
                           ReferenceDocId, ReferenceDocIdSr, ReferenceNo,
                           SaleInvoice, SaleInvoiceSr, V_Nature, GrossWeight, NetWeight, OMSId, Gross_Amount, Taxable_Amount,
                           Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, 
                           Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount)
                           Select " & AgL.Chk_Text(SaleInvoiceTableList(0).DocID) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Sr) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_ItemCode) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_Specification) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_SalesTaxGroupItem) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_DocQty) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_FreeQty) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Qty) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_Unit) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Pcs) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_UnitMultiplier) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_DealUnit) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_DocDealQty) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Rate) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_DiscountPer) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_DiscountAmount) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_AdditionalDiscountPer) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_AdditionalDiscountAmount) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_AdditionPer) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_AdditionAmount) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Amount) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_Remark) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_BaleNo) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_LotNo) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_ReferenceDocId) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_ReferenceDocIdSr) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_ReferenceNo) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_SaleInvoice) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_SaleInvoiceSr) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_V_Nature) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_GrossWeight) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_NetWeight) & ", 
                            " & AgL.Chk_Text(SaleInvoiceTableList(I).Line_OmsId) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Gross_Amount) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Taxable_Amount) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax1_Per) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax1) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax2_Per) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax2) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax3_Per) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax3) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax4_Per) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax4) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax5_Per) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Tax5) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_SubTotal1) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Deduction_Per) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Deduction) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Other_Charge_Per) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Other_Charge) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Round_Off) & ", 
                            " & Val(SaleInvoiceTableList(I).Line_Net_Amount) & ""
                    Debug.Print(mQry)
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                End If
            Next


            If SaleInvoiceDimensionTableList IsNot Nothing Then
                For K As Integer = 0 To SaleInvoiceDimensionTableList.Length - 1
                    If Val(SaleInvoiceDimensionTableList(K).Qty) > 0 Then
                        mQry = " INSERT INTO SaleInvoiceDimensionDetail (DocID, TSr, SR, Specification, Pcs, Qty, TotalQty) 
                            Select " & AgL.Chk_Text(SaleInvoiceTableList(0).DocID) & ", 
                            " & Val(SaleInvoiceDimensionTableList(K).TSr) & " As TSr, 
                            " & Val(SaleInvoiceDimensionTableList(K).Sr) & " As Sr, 
                            " & AgL.Chk_Text(SaleInvoiceDimensionTableList(K).Specification) & ", 
                            " & Val(SaleInvoiceDimensionTableList(K).Pcs) & ", 
                            " & Val(SaleInvoiceDimensionTableList(K).Qty) & ", 
                            " & Val(SaleInvoiceDimensionTableList(K).TotalQty) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next
            End If

            If bNCat <> Ncat.SaleOrder Then
                mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                        SubCode, SalesTaxGroupParty,  Item,  LotNo, 
                        EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                        ReferenceDocID, ReferenceDocIDSr, Rate, Amount, Landed_Value) 
                        Select L.DocId, L.Sr, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, 
                        H.Div_Code, H.Site_Code, H.SaleToParty,  H.SalesTaxGroupParty,  L.Item,
                        L.LotNo, 'I', 
                        Case When  IfNull(L.Qty,0) >= 0 Then L.Qty Else 0 End As Qty_Iss, 
                        Case When  IfNull(L.Qty,0) < 0 Then L.Qty Else 0 End As Qty_Rec, 
                        L.Unit, L.UnitMultiplier, 
                        Case When  IfNull(L.DealQty,0) >= 0 Then L.DealQty Else 0 End As DealQty_Iss, 
                        Case When  IfNull(L.DealQty,0) < 0 Then L.DealQty Else 0 End As DealQty_Rec, 
                        L.DealUnit,  
                        L.ReferenceDocId, L.ReferenceDocIdSr, 
                        L.Amount/L.Qty, L.Amount, L.Amount
                        FROM SaleInvoiceDetail L    
                        LEFT JOIN SaleInvoice H On L.DocId = H.DocId 
                        WHERE L.DocId =  '" & SaleInvoiceTableList(0).DocID & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                If mFlag_Import = False Then
                    FGetCalculationData(SaleInvoiceTableList(0).DocID, AgL.GCn, AgL.ECmd)
                End If
            End If

            AgL.UpdateVoucherCounter(SaleInvoiceTableList(0).DocID, CDate(SaleInvoiceTableList(0).V_Date), AgL.GCn, AgL.ECmd,
                                     SaleInvoiceTableList(0).Div_Code, SaleInvoiceTableList(0).Site_Code)
            'End If
        End If
        Return SaleInvoiceTableList(0).DocID
    End Function


    Private Sub Dgl1_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellLeave
        'If e.ColumnIndex = Dgl1.Columns(Col1Item).Index Then
        '    If Dgl1.Item(Col1Item, e.RowIndex).Value = "" Then
        '        TxtAgent.Focus()
        '    End If
        'End If
    End Sub

    Public Structure StructSaleInvoice
        Dim DocID As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As String
        Dim Div_Code As String
        Dim Site_Code As String
        Dim ManualRefNo As String
        Dim SaleToParty As String
        Dim BillToPartyCode As String
        Dim BillToPartyName As String
        Dim AgentCode As String
        Dim AgentName As String
        Dim SaleToPartyName As String
        Dim SaleToPartyAddress As String
        Dim SaleToPartyCity As String
        Dim SaleToPartyMobile As String
        Dim SaleToPartySalesTaxNo As String
        Dim ShipToPartyCode As String
        Dim ShipToAddress As String
        Dim RateType As String
        Dim SalesTaxGroupParty As String
        Dim PlaceOfSupply As String
        Dim StructureCode As String
        Dim CustomFields As String
        Dim SaleToPartyDocNo As String
        Dim SaleToPartyDocDate As String
        Dim ReferenceDocId As String
        Dim Remarks As String
        Dim Tags As String
        Dim TermsAndConditions As String
        Dim Gross_Amount As Double
        Dim Taxable_Amount As Double
        Dim Tax1_Per As Double
        Dim Tax1 As Double
        Dim Tax2_Per As Double
        Dim Tax2 As Double
        Dim Tax3_Per As Double
        Dim Tax3 As Double
        Dim Tax4_Per As Double
        Dim Tax4 As Double
        Dim Tax5_Per As Double
        Dim Tax5 As Double
        Dim SubTotal1 As Double
        Dim Deduction_Per As Double
        Dim Deduction As Double
        Dim Other_Charge_Per As Double
        Dim Other_Charge As Double
        Dim Round_Off As Double
        Dim Net_Amount As Double
        Dim PaidAmt As String
        Dim CreditLimit As String
        Dim CreditDays As String
        Dim Status As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim ApproveBy As String
        Dim ApproveDate As String
        Dim MoveToLog As String
        Dim MoveToLogDate As String
        Dim UploadDate As String
        Dim LockText As String
        Dim OmsId As String

        '''''''''''''''''''''''''''''''''Line Detail''''''''''''''''''''''''''''''''''
        Dim Line_Sr As Integer
        Dim Line_ItemName As String
        Dim Line_ItemCode As String
        Dim Line_Specification As String
        Dim Line_ReferenceNo As String
        Dim Line_SalesTaxGroupItem As String
        Dim Line_DocQty As String
        Dim Line_FreeQty As String
        Dim Line_Qty As String
        Dim Line_Unit As String
        Dim Line_Pcs As String
        Dim Line_UnitMultiplier As String
        Dim Line_DealUnit As String
        Dim Line_DocDealQty As String
        Dim Line_Rate As String
        Dim Line_DiscountPer As String
        Dim Line_DiscountAmount As String
        Dim Line_AdditionalDiscountPer As String
        Dim Line_AdditionalDiscountAmount As String
        Dim Line_AdditionPer As String
        Dim Line_AdditionAmount As String
        Dim Line_Amount As String
        Dim Line_Remark As String
        Dim Line_BaleNo As String
        Dim Line_LotNo As String
        Dim Line_ReferenceDocId As String
        Dim Line_ReferenceDocIdSr As String
        Dim Line_SaleInvoice As String
        Dim Line_SaleInvoiceSr As String
        Dim Line_V_Nature As String
        Dim Line_GrossWeight As String
        Dim Line_NetWeight As String
        Dim Line_OmsId As String
        Dim Line_Gross_Amount As Double
        Dim Line_Taxable_Amount As Double
        Dim Line_Tax1_Per As Double
        Dim Line_Tax1 As Double
        Dim Line_Tax2_Per As Double
        Dim Line_Tax2 As Double
        Dim Line_Tax3_Per As Double
        Dim Line_Tax3 As Double
        Dim Line_Tax4_Per As Double
        Dim Line_Tax4 As Double
        Dim Line_Tax5_Per As Double
        Dim Line_Tax5 As Double
        Dim Line_SubTotal1 As Double
        Dim Line_Deduction_Per As Double
        Dim Line_Deduction As Double
        Dim Line_Other_Charge_Per As Double
        Dim Line_Other_Charge As Double
        Dim Line_Round_Off As Double
        Dim Line_Net_Amount As Double
    End Structure


    Public Structure StructSaleInvoiceDimensionDetail
        Dim TSr As Integer
        Dim Sr As Integer
        Dim Specification As String
        Dim Pcs As Integer
        Dim Qty As Double
        Dim TotalQty As Double
    End Structure

    'Private Sub FCreateJSONFileOld()
    'Dim EWayDocumentDetail As New EWayDocumentDetail()
    'EWayDocumentDetail.userGstin = "Captopril"
    'Dim EWayItemDetail As New EWayItemDetail()
    'EWayItemDetail.itemNo = "1"
    'EWayItemDetail.productName = "Hello"
    'EWayDocumentDetail.itemList.Add(EWayItemDetail)
    'Dim jsonString As String = EWayDocumentDetail.ToString()
    'End Sub

    Private Sub FCreateJSONFile()
        mQry = "Select H.ManualRefNo, H.V_Date, I.Description As ItemDesc, I.Specification As ItemSpecification, 
                Sg.DispName As SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyPinCode As SaleToPartyPinCode,
                S.ManualCode As SaleToPartyStateCode, 
                IfNull(VReg.SalesTaxNo,'URP') As SaleToPartySalesTaxNo,  H.Div_Code, IfNull(VDist.Distance,0) As transDistance,
                TSg.DispName As TransporterName, VTranReg.SalesTaxNo As TransporterSalesTaxNo,
                Sit.LRNo As TransDocNo, IfNull(Sit.LRDate,H.V_Date) As TransDocDate,
                Ic.Description As ItemCategoryDesc, I.ManualCode As ItemCode, L.Qty, L.Sr,
                L.Tax1_Per As LineTax1_Per, L.Tax1 As LineTax1, 
                L.Tax2_Per As LineTax2_Per, L.Tax2 As LineTax2, 
                L.Tax3_Per As LineTax3_Per, L.Tax3 As LineTax3, 
                L.Tax4_Per As LineTax4_Per, L.Tax4 As LineTax4, 
                L.Tax5_Per As LineTax5_Per, L.Tax5 As LineTax5, L.Taxable_Amount As LineTaxable_Amount,
                I.HSN, (Case When L.Unit='Meter' Then 'MTR' Else L.Unit End) as Unit, H.Net_Amount As TotalInvoiceValue,
                H.Tax1_Per As HeaderTax1_Per, H.Tax1 As HeaderTax1, 
                H.Tax2_Per As HeaderTax2_Per, H.Tax2 As HeaderTax2, 
                H.Tax3_Per As HeaderTax3_Per, H.Tax3 As HeaderTax3, 
                H.Tax4_Per As HeaderTax4_Per, H.Tax4 As HeaderTax4, 
                H.Tax5_Per As HeaderTax5_Per, H.Tax5 As HeaderTax5, H.Taxable_Amount As HeaderTaxable_Amount, H.Gross_Amount,
                0 As TotNonAdvolVal, 0 As OthValue, 0 As cessNonAdvol
                From SaleInvoice H  With (NoLock)
                LEFT JOIN City C  With (NoLock) On H.SaleToPartyCity = C.CityCode
                LEFT JOIN State S  With (NoLock) On C.State = S.Code
                LEFT JOIN SubGroup Sg  With (NoLock) On H.SaleToParty = Sg.SubCode
                LEFT JOIN SaleInvoiceDetail L  With (NoLock) On H.DocId = L.DocID
                LEFT JOIN Item I  With (NoLock) ON L.Item = I.Code
                LEFT JOIN ItemCategory Ic  With (NoLock) On I.ItemCategory = Ic.Code
                LEFT JOIN SaleInvoiceTransport Sit  With (NoLock) On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail  With (NoLock)
                            Group By SubCode) As Hlt On H.SaleToParty = Hlt.SubCode
                LEFT JOIN SubGroup TSg  With (NoLock) ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration  With (NoLock) 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.SaleToParty = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration  With (NoLock)
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail With (NoLock)
                            Where Site_Code = '" & AgL.PubSiteCode & "'
                            And Div_Code = '" & AgL.PubDivCode & "') As VDist On H.SaleToParty = VDist.SubCode
                Where H.DocId = '" & mSearchCode & "'"
        Dim DTInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select I.HSN, Count(*) As CntHSN
                From SaleInvoiceDetail L  With (NoLock)
                LEFT JOIN Item I  With (NoLock) On L.Item = I.Code
                Where DocId = '" & mSearchCode & "'
                GROUP By I.HSN 
                Order By CntHSN Desc "
        Dim DTMainHSN As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        ClsMain.FCreateJSONFile(DTInvoiceDetail, DTMainHSN)

        'mQry = " Select VReg.SalesTaxNo As DivisionSalesTaxNo, Sg.DispName As DivisionName, Sg.Address As DivisionAddress,
        '        Sg.PIN As DivisionPinCode, S.ManualCode As DivisionStateCode
        '        From Division D
        '        LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
        '        LEFT JOIN City C On Sg.CityCode = C.CityCode
        '        LEFT JOIN State S On C.State = S.Code
        '        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
        '                    From SubgroupRegistration 
        '                    Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
        '        Where D.Div_Code = '" & DTInvoiceDetail.Rows(0)("Div_Code") & "'"
        'Dim DTDivisionDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'If AgL.XNull(DTInvoiceDetail.Rows(0)("SaleToPartyPinCode")) = "" Then
        '    MsgBox("Party Pincode is blank.", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If


        ''Dim strFile As String = My.Application.Info.DirectoryPath + "\" + "Ewaybill_" + DTInvoiceDetail.Rows(0)("ManualRefNo") + "_" + CDate(DTInvoiceDetail.Rows(0)("V_Date")).ToString("ddMMyyyy") + ".json"
        'Dim FilePath As String = ""
        'Dim SaveFileDialogBox As SaveFileDialog
        'Dim sFilePath As String = ""
        'SaveFileDialogBox = New SaveFileDialog

        'SaveFileDialogBox.Title = "File Name"
        'FilePath = My.Computer.FileSystem.SpecialDirectories.Desktop
        'SaveFileDialogBox.InitialDirectory = FilePath
        ''SaveFileDialogBox.DefaultExt = ".json"
        'SaveFileDialogBox.FilterIndex = 1
        'SaveFileDialogBox.FileName = "Ewaybill_" + DTInvoiceDetail.Rows(0)("ManualRefNo") + "_" + CDate(DTInvoiceDetail.Rows(0)("V_Date")).ToString("ddMMyyyy") + ".json"
        'If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'sFilePath = SaveFileDialogBox.FileName

        'Dim fileExists As Boolean = File.Exists(sFilePath)
        'Dim StringTabPresses As String = ""
        'Using sw As New StreamWriter(File.Open(sFilePath, FileMode.OpenOrCreate))
        '    sw.WriteLine("{")
        '    sw.WriteLine(ControlChars.Tab + """version"": ""1.0.0501"",")
        '    sw.WriteLine(ControlChars.Tab + """billLists"": [")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + "{")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """userGstin"": """ & DTDivisionDetail.Rows(0)("DivisionSalesTaxNo") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """supplyType"": ""O"", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """subSupplyType"": 1, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """docType"": ""INV"", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """docNo"": """ & DTInvoiceDetail.Rows(0)("ManualRefNo") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """docDate"": """ & CDate(DTInvoiceDetail.Rows(0)("V_Date")).ToString("dd'/'MM'/'yyyy") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromGstin"": """ & DTDivisionDetail.Rows(0)("DivisionSalesTaxNo") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromTrdName"": """ & DTDivisionDetail.Rows(0)("DivisionName") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromAddr1"": """ & DTDivisionDetail.Rows(0)("DivisionAddress") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromAddr2"": """", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromPlace"": """", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromPincode"": " & DTDivisionDetail.Rows(0)("DivisionPinCode") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromStateCode"": " & Val(DTDivisionDetail.Rows(0)("DivisionStateCode")) & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """actualFromStateCode"": " & Val(DTDivisionDetail.Rows(0)("DivisionStateCode")) & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toGstin"": """ & DTInvoiceDetail.Rows(0)("SaleToPartySalesTaxNo") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toTrdName"": """ & DTInvoiceDetail.Rows(0)("SaleToPartyName") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toAddr1"": """ & DTInvoiceDetail.Rows(0)("SaleToPartyAddress") & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toAddr2"": """", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toPlace"": """", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toPincode"": " & DTInvoiceDetail.Rows(0)("SaleToPartyPinCode") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toStateCode"": " & Val(DTInvoiceDetail.Rows(0)("SaleToPartyStateCode")) & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """actualToStateCode"": " & Val(DTInvoiceDetail.Rows(0)("SaleToPartyStateCode")) & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """totalValue"": " & DTInvoiceDetail.Rows(0)("Gross_Amount") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cgstValue"": " & DTInvoiceDetail.Rows(0)("HeaderTax2") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """sgstValue"": " & DTInvoiceDetail.Rows(0)("HeaderTax3") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """igstValue"": " & DTInvoiceDetail.Rows(0)("HeaderTax1") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cessValue"": " & DTInvoiceDetail.Rows(0)("HeaderTax4") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transMode"": 1, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transDistance"": " & DTInvoiceDetail.Rows(0)("transDistance") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transporterName"": """ & AgL.XNull(DTInvoiceDetail.Rows(0)("TransporterName")) & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transporterId"": """ & AgL.XNull(DTInvoiceDetail.Rows(0)("TransporterSalesTaxNo")) & """, ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transDocNo"": """ & AgL.XNull(DTInvoiceDetail.Rows(0)("TransDocNo")) & """, ")
        '    If AgL.XNull(DTInvoiceDetail.Rows(0)("TransDocDate")) <> "" Then
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transDocDate"": """ & CDate(AgL.XNull(DTInvoiceDetail.Rows(0)("TransDocDate"))).ToString("dd'/'MM'/'yyyy") & """, ")
        '    Else
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transDocDate"": """", ")
        '    End If
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """vehicleNo"": """", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """vehicleType"": ""R"",")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """totInvValue"": " & DTInvoiceDetail.Rows(0)("TotalInvoiceValue") & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """mainHsnCode"": " & AgL.XNull(DTMainHSN.Rows(0)("HSN")) & ", ")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """itemList"": [")

        '    For I As Integer = 0 To DTInvoiceDetail.Rows.Count - 1
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "{")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """itemNo"": " & DTInvoiceDetail.Rows(I)("Sr") & ", ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """productName"": """ & DTInvoiceDetail.Rows(I)("ItemSpecification") & """, ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """productDesc"": """ & DTInvoiceDetail.Rows(I)("ItemCategoryDesc") & """, ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """hsnCode"": " & DTInvoiceDetail.Rows(I)("HSN") & ", ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """quantity"": " & DTInvoiceDetail.Rows(I)("Qty") & ", ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """qtyUnit"": """ & DTInvoiceDetail.Rows(I)("Unit") & """, ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """taxableAmount"": " & DTInvoiceDetail.Rows(I)("LineTaxable_Amount") & ", ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """sgstRate"": " & DTInvoiceDetail.Rows(I)("LineTax3_Per") & ", ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cgstRate"": " & DTInvoiceDetail.Rows(I)("LineTax2_Per") & ", ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """igstRate"": " & DTInvoiceDetail.Rows(I)("LineTax1_Per") & ", ")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cessRate"": " & DTInvoiceDetail.Rows(I)("LineTax4_Per") & "")
        '        sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" + IIf(I < DTInvoiceDetail.Rows.Count - 1, ",", ""))
        '    Next

        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "]")
        '    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + "}")
        '    sw.WriteLine(ControlChars.Tab + "]")
        '    sw.WriteLine("}")
        'End Using
        ''System.Diagnostics.Process.Start("notepad.exe", strFile)
    End Sub

    Public Structure StructEWayBill
        Dim DocID As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As String
        Dim Div_Code As String
        Dim Site_Code As String
        Dim ManualRefNo As String
        Dim SaleToParty As String
        Dim BillToPartyCode As String
        Dim BillToPartyName As String
        Dim AgentCode As String
        Dim AgentName As String
        Dim SaleToPartyName As String
        Dim SaleToPartyAddress As String
        Dim SaleToPartyCity As String
        Dim SaleToPartyMobile As String
        Dim SaleToPartySalesTaxNo As String
        Dim ShipToAddress As String
        Dim RateType As String
        Dim SalesTaxGroupParty As String
        Dim PlaceOfSupply As String
        Dim StructureCode As String
        Dim CustomFields As String
        Dim SaleToPartyDocNo As String
        Dim SaleToPartyDocDate As String
        Dim ReferenceDocId As String
        Dim Remarks As String
        Dim TermsAndConditions As String
        Dim Gross_Amount As Double
        Dim Taxable_Amount As Double
        Dim Tax1_Per As Double
        Dim Tax1 As Double
        Dim Tax2_Per As Double
        Dim Tax2 As Double
        Dim Tax3_Per As Double
        Dim Tax3 As Double
        Dim Tax4_Per As Double
        Dim Tax4 As Double
        Dim Tax5_Per As Double
        Dim Tax5 As Double
        Dim SubTotal1 As Double
        Dim Deduction_Per As Double
        Dim Deduction As Double
        Dim Other_Charge_Per As Double
        Dim Other_Charge As Double
        Dim Round_Off As Double
        Dim Net_Amount As Double
        Dim PaidAmt As String
        Dim CreditLimit As String
        Dim CreditDays As String
        Dim Status As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim ApproveBy As String
        Dim ApproveDate As String
        Dim MoveToLog As String
        Dim MoveToLogDate As String
        Dim UploadDate As String
    End Structure

    Private Sub FReconcileBill()
        Dim FrmObj As New FrmSaleInvoiceReconciliation
        FrmObj.Text = "Reconcile Sale Bill"
        FrmObj.LblDocNo.Text = "Invoice No : " + TxtReferenceNo.Text
        FrmObj.LblParty.Text = "Party : " + TxtSaleToParty.Text
        FrmObj.DtV_TypeSettings = DtV_TypeSettings

        FrmObj.SearchCode = mSearchCode
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()
    End Sub

    Private Sub FPrintBulk(SearchCode As String)
        Dim dtTemp As DataTable
        Dim I As Integer
        Dim FrmObj As New FrmPrintDialog
        FrmObj.IniGrid()
        FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowFromNo).Value = TxtReferenceNo.Text
        FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowToNo).Value = TxtReferenceNo.Text
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()

        If FrmObj.mOkButtonPressed Then
            mQry = "SELECT H.DocID FROM SaleInvoice H WHERE H.V_Type = '" & TxtV_Type.Tag & "' 
                    And H.Div_Code = '" & TxtDivision.Tag & "' And H.Site_Code = '" & TxtSite_Code.Tag & "' And Date(H.V_Date) Between " & AgL.Chk_Date(AgL.PubStartDate) & " And " & AgL.Chk_Date(AgL.PubEndDate) & " "
            If FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowFromNo).Value <> "" Then
                mQry += " AND Cast(H.ManualRefNo AS BIGINT) >= " & Val(FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowFromNo).Value) & " "
            End If
            If FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowToNo).Value <> "" Then
                mQry += " AND Cast(H.ManualRefNo AS BIGINT) <= " & Val(FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowToNo).Value) & ""
            End If
            If FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowFromDate).Value <> "" Then
                mQry += " AND Date(H.V_Date) <= " & AgL.Chk_Date(FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowFromDate).Value) & ""
            End If
            If FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowToDate).Value <> "" Then
                mQry += " AND Date(H.V_Date) >= " & AgL.Chk_Date(FrmObj.Dgl1.Item(FrmPrintDialog.Col1Value, FrmPrintDialog.rowToDate).Value) & ""
            End If

            'FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint,, mQry)

            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If dtTemp.Rows.Count > 0 Then
                For I = 0 To dtTemp.Rows.Count - 1
                    FGetPrint(AgL.XNull(dtTemp.Rows(I)("DocID")), ClsMain.PrintFor.DocumentPrint, True, "")
                Next
            End If
        End If
    End Sub

    Private Sub FGetPrintSSRS(mPrintFor As ClsMain.PrintFor)
        Dim dsMain As DataTable
        Dim dsCompany As DataTable
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer

        If mPrintFor = ClsMain.PrintFor.EMail Or mPrintFor = ClsMain.PrintFor.QA Then
            PrintingCopies = ("").Split(",")
        Else
            'PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")
            PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        End If

        If LblV_Type.Tag = Ncat.SaleReturn Then
            mPrintTitle = TxtV_Type.Text & " (Credit Note)"
        Else
            If Not AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
                mPrintTitle = "CHALLAN"
            Else
                mPrintTitle = TxtV_Type.Text  ' "TAX INVOICE"
            End If
        End If

        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, H.DocID, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNo, 
                IfNull(RT.Description,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("SaleRate_Caption")) & "') as RateType, Agent.DispName as AgentName, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption,
                (Case When BP.Nature = 'Cash' Then BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'') Else H.SaletoPartyName End) as SaleToPartyName, H.SaleToPartyAddress, C.CityName as CityName, State.ManualCode as StateCode, State.Description as StateName, 
                H.SaleToPartyMobile, Sg.ContactPerson, IfNull(H.SaleToPartySalesTaxNo,'') as SaleToPartySalesTaxNo, 
                IfNull(H.SaleToPartyAadharNo,'') as SaleToPartyAadharNo, IfNull(H.SaleToPartyPanNo,'') as PanNo,
                H.ShipToAddress, H.TermsAndConditions, Transporter.Name as TransporterName, TD.LrNo, TD.LrDate, TD.PrivateMark, TD.Weight, TD.Freight, TD.PaymentType as FreightType, TD.RoadPermitNo, TD.RoadPermitDate, IfNull(L.ReferenceNo,'') as ReferenceNo,
                I.Description as ItemName, " & IIf(mPrintFor = ClsMain.PrintFor.QA, "IG.Description", "IfNull(IG.PrintingDescription,IG.Description)") & " as ItemGroupName, IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, I.HSN, I.MaintainStockYn,
                L.SalesTaxGroupItem, STGI.GrossTaxRate, (Case when IfNull(I.MaintainStockYn,1) =1 Then L.Pcs Else 0 End) as Pcs, (Case when IfNull(I.MaintainStockYn,1) =1 Then abs(L.Qty) Else 0 End) as Qty, L.Rate, L.Unit, U.DecimalPlaces as UnitDecimalPlaces, TS.DiscountCalculationPattern, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, 
                abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) as Tax1_Per, abs(L.Tax1) as Tax1, abs(L.Tax2_Per) as Tax2_Per, abs(L.Tax2) as Tax2, abs(L.Tax3_Per) as Tax3_Per, abs(L.Tax3) as Tax3, abs(L.Tax4_Per) as Tax4_Per, abs(L.Tax4) as Tax4, abs(L.Tax5_Per) as Tax5_Per, abs(L.Tax5) as Tax5, abs(L.Net_Amount) as Net_Amount, L.Remark as LRemarks, H.Remarks as HRemarks,
                abs(H.Gross_Amount) as H_Gross_Amount, H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount, Abs(H.Taxable_Amount) as H_Taxable_Amount,Abs(H.Tax1_Per) as H_Tax1_Per, Abs(H.Tax1) as H_Tax1, 
                H.Tax2_Per as H_Tax2_Per, abs(H.Tax2) as H_Tax2, H.Tax3_Per as H_Tax3_Per, abs(H.Tax3) as H_Tax3, H.Tax4_Per as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                H.Tax5_Per as H_Tax5_Per, abs(H.Tax5) as H_Tax5, H.Deduction_Per as H_Deduction_Per, H.Deduction as H_Deduction, H.Other_Charge_Per as H_Other_Charge_Per, H.Other_Charge as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsCenterAlignedCompanyInfoOnDocuments")) & "' as IsCenterAlignedCompanyInfoOnDocuments, IfNull(L.DimensionDetail,'') as DimDetail,
                '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
                from (Select * From SaleInvoice  With (NoLock) Where DocID = '" & mSearchCode & "') as H
                Left Join SaleInvoiceTrnSetting TS  With (NoLock) On H.DocId = TS.DocID
                Left Join SaleInvoiceDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Item I  With (NoLock) On L.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.SaleToPartyCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join SaleInvoiceTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.SaleToParty = Sg.Subcode
                Left Join Subgroup BP With (NoLock) On H.BillToParty = BP.Subcode
                Left Join RateType RT  With (NoLock) on H.RateType = Rt.Code
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join DocumentPrefix DP On VT.Category = DP.Category And H.Div_Code = DP.Div_Code                
                "

        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "

        dsMain = AgL.FillData(mQry, AgL.GCn).Tables(0)


        FReplaceInvoiceVariables(dsMain, TxtDivision.Tag, TxtSite_Code.Tag)

        dsCompany = ClsMain.GetDocumentHeaderDataTable(TxtDivision.Tag, TxtSite_Code.Tag, TxtV_Type.Tag)

        Dim objRepPrint As Object

        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailCompose(AgL)
            objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
                    From SaleInvoice H  With (NoLock)
                    LEFT JOIN SubGroup Sg  With (NoLock) On H.SaleToParty = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
                    From SaleInvoice H  With (NoLock)
                    LEFT JOIN SubGroup Sg  With (NoLock) On H.Agent = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            objRepPrint.AttachmentName = "Invoice"

            objRepPrint.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            objRepPrint.reportViewer1.ZoomMode = ZoomMode.Percent
            objRepPrint.reportViewer1.ZoomPercent = 50
        Else

            objRepPrint = New FrmRepPrint(AgL)


        End If

        objRepPrint.reportViewer1.Visible = True
        Dim id As Integer = 0
        objRepPrint.reportViewer1.ProcessingMode = ProcessingMode.Local

        If AgL.PubUserName.ToUpper = "SUPER" Then
            dsMain = ClsMain.RemoveNullFromDataTable(dsMain)
            dsCompany = ClsMain.RemoveNullFromDataTable(dsCompany)
            dsMain.WriteXml(AgL.PubReportPath + "\SaleInvoice_DsMain.xml")
            dsCompany.WriteXml(AgL.PubReportPath + "\SaleInvoice_DsCompany.xml")
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("ReportFileName")) <> "" Then
            objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\" + AgL.XNull(DtV_TypeSettings.Rows(0)("ReportFileName"))
        Else

            If ClsMain.IsScopeOfWorkContains("+CLOTH TRADING WHOLESALE") Then
                objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\SaleInvoice_Cloth.rdl"
            Else
                objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\SaleInvoice.rdl"
            End If
        End If

        If (dsMain.Rows.Count = 0) Then
            MsgBox("No records found to print.")
        End If
        Dim rds As New ReportDataSource("DsMain", dsMain)
        Dim rdsCompany As New ReportDataSource("DsCompany", dsCompany)



        objRepPrint.reportViewer1.LocalReport.DataSources.Clear()
        objRepPrint.reportViewer1.LocalReport.DataSources.Add(rds)
        objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsCompany)


        objRepPrint.reportViewer1.LocalReport.Refresh()
        objRepPrint.reportViewer1.RefreshReport()
        objRepPrint.MdiParent = Me.MdiParent
        objRepPrint.Show()
    End Sub

    Private Sub FSendSms()
        Dim FrmObj As FrmSendSms
        FrmObj = New FrmSendSms(AgL)

        FGetSmsConfiguration(FrmObj, mSearchCode)

        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()
    End Sub

    Private Sub FSendWhatsapp()
        Dim mDocNoPrefix As String = FGetSettings(SettingFields.DocumentPrintEntryNoPrefix, SettingType.General)
        Dim IsSuccess As Boolean
        Dim ToMobileNo As String
        Dim ToMessage As String
        Dim DtDocData As DataTable = AgL.FillData("Select 
                    Sg.DispName As DivisionName, 
                    Party.DispName As PartyName, Party.Mobile As PartyMobile,
                    Agent.DispName As AgentName, Agent.Mobile As AgentMobile, H.Net_Amount
                    From SaleInvoice H 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SubGroup Agent On H.Agent = Agent.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).Tables(0)


        ToMobileNo = AgL.XNull(DtDocData.Rows(0)("PartyMobile"))
        ToMessage = FGetSettings(SettingFields.SmsMessage, SettingType.General)
        ToMessage = ToMessage.
                Replace("<PartyName>", AgL.XNull(DtDocData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", mDocNoPrefix & TxtReferenceNo.Text).Replace("<EntryDate>", TxtV_Date.Text).
                Replace("<DivisionName>", AgL.XNull(DtDocData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtDocData.Rows(0)("AgentName"))).
                Replace("<NetAmount>", Format(AgL.VNull(DtDocData.Rows(0)("Net_Amount")), "0.00")).
                Replace("&", "And")
        IsSuccess = FSendWhatsappMessage(ToMobileNo, ToMessage, "Message", "")
    End Sub

    Private Sub FSendWhatsappPDF()
        Dim mDocNoPrefix As String = FGetSettings(SettingFields.DocumentPrintEntryNoPrefix, SettingType.General)
        Dim IsSuccess As Boolean
        Dim ToMobileNo As String
        Dim ToMessage As String
        Dim DtDocData As DataTable = AgL.FillData("Select 
                    Sg.DispName As DivisionName, 
                    Party.DispName As PartyName, Party.Mobile As PartyMobile,
                    Agent.DispName As AgentName, Agent.Mobile As AgentMobile, H.Net_Amount
                    From SaleInvoice H 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SubGroup Agent On H.Agent = Agent.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).Tables(0)


        ToMobileNo = AgL.XNull(DtDocData.Rows(0)("PartyMobile"))
        ToMessage = FGetSettings(SettingFields.SmsMessage, SettingType.General)
        ToMessage = ToMessage.
                Replace("<PartyName>", AgL.XNull(DtDocData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", mDocNoPrefix & TxtReferenceNo.Text).Replace("<EntryDate>", TxtV_Date.Text).
                Replace("<DivisionName>", AgL.XNull(DtDocData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtDocData.Rows(0)("AgentName"))).
                Replace("<NetAmount>", Format(AgL.VNull(DtDocData.Rows(0)("Net_Amount")), "0.00")).
                Replace("&", "And")
        UploadFileViaFTP()
        'IsSuccess = FSendWhatsappMessage(ToMobileNo, ToMessage, "PDF", "http://164.52.202.56/~equal2464/Sadhvi/SI6396.pdf")

    End Sub
    Private Function GetFieldAliasName(bImportFor As ImportFor, bFieldName As String)
        Dim bAliasName As String = bFieldName
        If bImportFor = ImportFor.Dos Then
            Select Case bFieldName
                Case "V_TYPE"
                    bAliasName = "V_TYPE"
                Case "V_NO"
                    bAliasName = "V_NO"
                Case "V_Date"
                    bAliasName = "V_DATE"
                Case "Invoice No"
                    bAliasName = "INVOICE_NO"
                Case "Sale To Party"
                    bAliasName = "SALE_PARTY"
                Case "Sale To Party Address"
                    bAliasName = "PARTY_ADD"
                Case "Sale To Party City"
                    bAliasName = "PARTY_CITY"
                Case "Sale To Party Pincode"
                    bAliasName = "PINCODE"
                Case "Sale To Party Sales Tax No"
                    bAliasName = "GSTIN"
                Case "Bill To Party"
                    bAliasName = "BILL_PARTY"
                Case "Agent"
                    bAliasName = "AGENT"
                Case "Transporter"
                    bAliasName = "TRANSPORT"
                Case "Transporter Sales Tax No"
                    bAliasName = "TR_GSTIN"
                Case "Rate Type"
                    bAliasName = "RATE_TYPE"
                Case "Sales Tax Group Party"
                    bAliasName = "TAX_GROUP"
                Case "Place Of Supply"
                    bAliasName = "PLACE_SUPP"
                Case "Sale To Party Doc No"
                    bAliasName = "Sale To Party Doc No"
                Case "Sale To Party Doc Date"
                    bAliasName = "Sale To Party Doc Date"
                Case "Remark"
                    bAliasName = "REMARK"
                Case "Terms And Conditions"
                    bAliasName = "TERMS"
                Case "Credit Limit"
                    bAliasName = "CR_LIMIT"
                Case "Credit Days"
                    bAliasName = "CR_DAYS"
                Case "SubTotal1"
                    bAliasName = "SUBTOTAL1"
                Case "Deduction_Per"
                    bAliasName = "DED_PER"
                Case "Deduction"
                    bAliasName = "DEDUCTION"
                Case "Other_Charge_Per"
                    bAliasName = "OT_CH_PER"
                Case "Other_Charge"
                    bAliasName = "OT_CHARGE"
                Case "Round_Off"
                    bAliasName = "ROUND_OFF"
                Case "Net_Amount"
                    bAliasName = "NET_AMOUNT"


                Case "TSr"
                    bAliasName = "TSR"
                Case "Item Name"
                    bAliasName = "ITEM_NAME"
                Case "Item Group"
                    bAliasName = "make_name"
                Case "Item Category"
                    bAliasName = "catagory"
                Case "Specification"
                    bAliasName = "SPECIFIC"
                Case "Sales Tax Group Item"
                    bAliasName = "TAX_GROUP"
                Case "Qty"
                    bAliasName = "QTY"
                Case "Unit"
                    bAliasName = "UNIT"
                Case "Pcs"
                    bAliasName = "PCS"
                Case "Unit Multiplier"
                    bAliasName = "Unit Multiplier"
                Case "Deal Unit"
                    bAliasName = "Deal Unit"
                Case "Deal Qty"
                    bAliasName = "Deal Qty"
                Case "Rate"
                    bAliasName = "Rate"
                Case "Discount Per"
                    bAliasName = "DISC_PER"
                Case "Discount Amount"
                    bAliasName = "DISC_AMT"
                Case "Additional Discount Per"
                    bAliasName = "ADISP_PER"
                Case "Additional Discount Amount"
                    bAliasName = "ADISC_AMT"
                Case "Amount"
                    bAliasName = "AMOUNT"
                Case "Remark"
                    bAliasName = "REMARK"
                Case "Bale No"
                    bAliasName = "BALE_NO"
                Case "Lot No"
                    bAliasName = "LOT_NO"
                Case "Gross_Amount"
                    bAliasName = "GROSS_AMT"
                Case "Taxable_Amount"
                    bAliasName = "TAXABLEAMT"
                Case "Tax1_Per"
                    bAliasName = "TAX1_PER"
                Case "Tax1"
                    bAliasName = "TAX1"
                Case "Tax2_Per"
                    bAliasName = "TAX2_PER"
                Case "Tax2"
                    bAliasName = "TAX2"
                Case "Tax3_Per"
                    bAliasName = "TAX3_PER"
                Case "Tax3"
                    bAliasName = "TAX3"
                Case "Tax4_Per"
                    bAliasName = "TAX4_PER"
                Case "Tax4"
                    bAliasName = "TAX4"
                Case "Tax5_Per"
                    bAliasName = "TAX5_PER"
                Case "Tax5"
                    bAliasName = "TAX5"

                Case "TotalQty"
                    bAliasName = "totqty"
            End Select

            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function

    Private Sub FrmSaleInvoiceDirect_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select * From ItemTypeSetting"
        DtItemTypeSettingsAll = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

    Private Sub MnuPrintQACopy_Click(sender As Object, e As EventArgs) Handles MnuPrintQACopy.Click
        FGetPrint(mSearchCode, ClsMain.PrintFor.QA)
    End Sub

    Private Sub Dgl2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl2.CurrentCell.ReadOnly = True
            End If

            If Dgl2.CurrentCell.ColumnIndex <> Dgl2.Columns(Col1Value).Index Then Exit Sub


            Dgl2.AgHelpDataSet(Dgl2.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case Dgl2.CurrentCell.RowIndex
                Case rowPartyDocDate, rowDeliveryDate, rowMinDeliveryDate
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Dgl3_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl3.CellEnter
        Try
            If Dgl3.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl3.CurrentCell.ReadOnly = True
            End If

            If Dgl3.CurrentCell.ColumnIndex <> Dgl3.Columns(Col1Value).Index Then Exit Sub


            Dgl3.AgHelpDataSet(Dgl3.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl3.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl3.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case Dgl3.CurrentCell.RowIndex
                Case rowCreditDays
                    CType(Dgl3.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl3.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 3
                    CType(Dgl3.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0

                Case rowTags
                    Dgl3.Item(Col1Value, rowTags).ReadOnly = True

                Case rowEInvoiceACKDate
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Dgl2_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl2.CurrentCell.RowIndex
            bColumnIndex = Dgl2.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl2.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl2.CurrentCell.RowIndex

                Case rowRateType
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Description FROM RateType With (NoLock) Order By Description "
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If
                Case rowGodown
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Godown & "' Order By Name"
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowReferenceNo
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT DocID, 
                                    '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as DocNo                                     
                                    FROM SaleInvoice H With (NoLock)                                     
                                    Left Join Voucher_Type Vt on H.V_Type = VT.V_Type
                                    Where H.Site_Code = '" & TxtSite_Code.Tag & "' And H.Div_Code = '" & TxtDivision.Tag & "' And H.V_Type = '" & TxtV_Type.Tag & "'
                                    Order By '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo "
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowStructure
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Description FROM Structure WHERE Code IN ('GstSale','GstSaleMrp')"
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value,, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowShipToParty
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            FCreateHelpSubgroup()
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value, 6) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub FGetStructureForInputField()
        Dgl2.Item(Col1Value, rowStructure).Tag = TxtStructure.Tag
        Dgl2.Item(Col1Value, rowStructure).Value = AgL.XNull(AgL.Dman_Execute(" Select Description 
                        From Structure Where Code = '" & Dgl2.Item(Col1Value, rowStructure).Tag & "'", AgL.GCn).ExecuteScalar())
    End Sub

    Private Sub Dgl3_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl3.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl3.CurrentCell.RowIndex
            bColumnIndex = Dgl3.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl3.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl3.CurrentCell.RowIndex
                Case rowAgent
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.SalesAgent & "' Order By Name"
                            Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl3.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl3.AgHelpDataSet(Col1Value) = Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowResponsiblePerson
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Sg.Code, Sg.Name From viewHelpSubgroup Sg  With (NoLock) Left Join HRM_Employee Emp On Sg.Code = Emp.Subcode Where sg.SubgroupType ='" & SubgroupType.Employee & "' And Emp.RelievingDate Is Null And Site_Code = '" & TxtSite_Code.Tag & "' "
                            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterResponsiblePersonDesignation")) <> "" Then
                                mQry += " And CharIndex('+' || Emp.Designation,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterResponsiblePersonDesignation")) & "') > 0 "
                                mQry += " And CharIndex('-' || Emp.Designation,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterResponsiblePersonDesignation")) & "') <= 0 "
                            End If
                            mQry += " Order By sg.Name "

                            Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl3.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl3.AgHelpDataSet(Col1Value) = Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowTransporter
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Transporter & "' Order By Name"
                            Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl3.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl3.AgHelpDataSet(Col1Value) = Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowSalesRepresentative
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Sg.Code, Sg.Name From viewHelpSubgroup Sg  With (NoLock) Left Join HRM_Employee Emp On Sg.Code = Emp.Subcode Where sg.SubgroupType ='" & SubgroupType.Employee & "' And Emp.RelievingDate Is Null And Emp.Designation='SREP' And Site_Code = '" & TxtSite_Code.Tag & "' Order By sg.Name "
                            Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl3.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl3.AgHelpDataSet(Col1Value) = Dgl3.Item(Col1Head, Dgl3.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowRemarks
                    If e.KeyCode = Keys.Enter Then
                        If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                            Topctrl1.FButtonClick(13)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Dgl2_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = Dgl2.CurrentCell.RowIndex
        mColumn = Dgl2.CurrentCell.ColumnIndex
        If mColumn = Dgl2.Columns(Col1Value).Index Then
            If Dgl2.Item(Col1Mandatory, mRow).Value <> "" Then
                If Dgl2(Col1Value, mRow).Value = "" Then
                    MsgBox(Dgl2(Col1Head, mRow).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If

        Try
            If mColumn <> Dgl2.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl2.CurrentCell.RowIndex
                Case rowStructure
                    If Dgl1.Rows.Count > 1 Then
                        If MsgBox("If you will change billing type then you will loss line data in this entry.Do you want to continue", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            FChangeStructure()
                        Else
                            FGetStructureForInputField()
                        End If
                    Else
                        FChangeStructure()
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FChangeStructure()
        TxtStructure.Tag = Dgl2.Item(Col1Value, rowStructure).Tag
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
        FGetStructureForInputField()
        Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowStructure)
        Dgl2.Focus()
    End Sub


    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.LblDocNo.Text = "Document No. : " + TxtReferenceNo.Text
        FrmObj.SearchCode = mSearchCode
        FrmObj.TableName = "SubGroupAttachments"
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()
        FrmObj.Dispose()
        FrmObj = Nothing
        SetAttachmentCaption()
    End Sub

    Private Sub SetAttachmentCaption()
        Dim AttachmentPath As String = PubAttachmentPath + mSearchCode + "\"
        If Directory.Exists(AttachmentPath) Then
            Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
            If FileCount > 0 Then BtnAttachments.Text = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else BtnAttachments.Text = "Attachments"
        Else
            BtnAttachments.Text = "Attachments"
        End If
    End Sub
    Private Sub Dgl3_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl3.KeyDown
        Dim bRowIndex As Integer = 0
        Dim bColumnIndex As Integer = 0
        Try
            If Dgl3.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl3.CurrentCell.RowIndex
            bColumnIndex = Dgl3.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub
            If Topctrl1.Mode = "Browse" Then Exit Sub
            If bColumnIndex <> Dgl3.Columns(Col1Value).Index Then Exit Sub
            If e.KeyCode = Keys.Delete Then Dgl3.CurrentCell.Value = "" : Dgl3.CurrentCell.Tag = "" : Exit Sub

            Select Case Dgl3.CurrentCell.RowIndex
                Case rowTags
                    If e.KeyCode <> Keys.Enter Then
                        FHPGD_Tags()
                    End If

                Case rowTermsAndConditions
                    If e.KeyCode = Keys.Enter Then
                        If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                            Topctrl1.FButtonClick(13)
                        End If
                    End If
            End Select

            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(Dgl3)
                If Dgl3.CurrentCell.RowIndex = LastCell.RowIndex Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FHPGD_Tags()
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " Select 'o' As Tick, T.Description, T.Description As Tag From Tag T "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 300, 230, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Tag", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            Dgl3.Item(Col1Value, rowTags).Value = "+" + FRH_Multiple.FFetchData(2, "", "", "+")
        End If

        FRH_Multiple = Nothing
    End Sub

    Public Shared Sub FGetCalculationData(mSearchCode As String, Conn As Object, Cmd As Object)
        Dim mQry As String = ""
        mQry = "SELECT Sd.* 
                FROM SaleInvoice H With (NoLock)
                LEFT JOIN StructureDetail Sd With (NoLock) ON H.Structure = Sd.Code
                WHERE H.DocID = '" & mSearchCode & "'"
        Dim DtCalcHeaderData As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        mQry = "Select * From SaleInvoice With (NoLock) Where DocId = '" & mSearchCode & "'"
        Dim DtTransactionDetail As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        For I As Integer = 0 To DtCalcHeaderData.Rows.Count - 1
            For J As Integer = 0 To DtTransactionDetail.Columns.Count - 1
                If DtCalcHeaderData.Rows(I)("HeaderAmtField") = DtTransactionDetail.Columns(J).ColumnName Then
                    DtCalcHeaderData.Rows(I)("Amount") = (AgL.VNull(DtTransactionDetail.Rows(0)(DtTransactionDetail.Columns(J).ColumnName)))
                End If
            Next
        Next

        mQry = " SELECT H.SalesTaxGroupParty, H.PlaceOfSupply, L.SalesTaxGroupItem,
                Sd.Charges, Pst.*
                FROM SaleInvoice H With (NoLock)
                LEFT JOIN SaleInvoiceDetail L With (NoLock) ON H.DocID = L.DocID
                LEFT JOIN PostingGroupSalesTax Pst With (NoLock) ON H.SalesTaxGroupParty = Pst.PostingGroupSalesTaxParty
	                AND H.PlaceOfSupply = Pst.PlaceOfSupply
	                AND L.SalesTaxGroupItem = Pst.PostingGroupSalesTaxItem
	                AND Pst.Process = 'Sales'
                LEFT JOIN StructureDetail Sd ON H.Structure = Sd.Code
	                AND Pst.ChargeType = Sd.Charge_Type
                WHERE H.DocID = '" & mSearchCode & "'"
        Dim DtPostingGroupSalesTax As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)



        mQry = "Select "
        For I As Integer = 0 To DtCalcHeaderData.Rows.Count - 1
            mQry += " (" & AgL.XNull(DtCalcHeaderData.Rows(I)("LineAmtField")) + ") As [" + GetColName(DtCalcHeaderData.Rows(I)("Charges")) + "],"
            mQry += " 0.00  As [" + GetColNamePer(DtCalcHeaderData.Rows(I)("Charges")) + "],"
            mQry += " '' As [" + GetColNamePostAc(DtCalcHeaderData.Rows(I)("Charges")) + "],"
            mQry += AgL.Chk_Text(AgL.XNull(DtCalcHeaderData.Rows(I)("ContraAc"))) + " As [" + GetColNameContraAc(DtCalcHeaderData.Rows(I)("Charges")) + "]" + IIf(I = DtCalcHeaderData.Rows.Count - 1, "", ",")
        Next
        mQry += " From SaleInvoiceDetail With (NoLock) Where DocId = '" & mSearchCode & "'"
        Dim DtCalcLineData As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        For I As Integer = 0 To DtCalcLineData.Rows.Count - 1
            For J As Integer = 0 To DtCalcLineData.Columns.Count - 1
                For K As Integer = 0 To DtPostingGroupSalesTax.Rows.Count - 1
                    If DtCalcLineData.Columns(J).ColumnName = GetColNamePostAc(DtPostingGroupSalesTax.Rows(K)("Charges")) Then
                        DtCalcLineData.Rows(I)(J) = AgL.XNull(DtPostingGroupSalesTax.Rows(K)("LedgerAc"))
                    ElseIf DtCalcLineData.Columns(J).ColumnName = GetColNamePer(DtPostingGroupSalesTax.Rows(K)("Charges")) Then
                        DtCalcLineData.Rows(I)(J) = AgL.VNull(DtPostingGroupSalesTax.Rows(K)("Percentage"))
                    End If
                Next
            Next
        Next


        Dim mMultiplyWithMinus As Boolean = False
        Dim mNarrationParty As String
        Dim mNarration As String
        If AgL.XNull(AgL.Dman_Execute("Select NCat 
                        From Voucher_Type 
                        Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar) = Ncat.SaleReturn Then
            mNarrationParty = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type"))
            mNarrationParty += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
            mNarration = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & " : " & DtTransactionDetail.Rows(0)("SaleToPartyName") & ""
            mNarration += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
            mMultiplyWithMinus = True
        Else
            If DtTransactionDetail.Rows(0)("ManualRefNo") <> "" Then
                mNarrationParty = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & " : " & DtTransactionDetail.Rows(0)("ManualRefNo") & " Dated " & DtTransactionDetail.Rows(0)("V_Date")
                mNarrationParty += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
                mNarration = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & " : " & DtTransactionDetail.Rows(0)("SaleToPartyName") & " Invoice No. " & DtTransactionDetail.Rows(0)("ManualRefNo") & " Dated " & DtTransactionDetail.Rows(0)("V_Date")
                mNarration += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
            Else
                mNarrationParty = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type"))
                mNarrationParty += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
                mNarration = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & " : " & DtTransactionDetail.Rows(0)("SaleToPartyName") & ""
                mNarration += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
            End If
            mMultiplyWithMinus = False
        End If

        Dim DtSettings As DataTable
        mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
        DtSettings = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
        If DtSettings.Rows.Count = 0 Then
            mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
            DtSettings = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
            If DtSettings.Rows.Count = 0 Then
                mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                DtSettings = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
                If DtSettings.Rows.Count = 0 Then
                    mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "' And Div_Code  Is Null And Site_Code Is Null "
                    DtSettings = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
                    If DtSettings.Rows.Count = 0 Then
                        mQry = "Select * from SaleInvoiceSetting  With (NoLock)  Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                        DtSettings = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
                    End If
                End If
            End If
        End If



        Dim bPartyLedgerPostingAc As String = ""
        Dim bLinkedPartyAc As String = ""
        If AgL.StrCmp(AgL.XNull(DtSettings.Rows(0)("LedgerPostingPartyAcType")), SaleInvoiceLedgerPostingPartyAcType.SaleToParty) Then
            bPartyLedgerPostingAc = AgL.XNull(DtTransactionDetail.Rows(0)("SaleToParty"))
            bLinkedPartyAc = AgL.XNull(DtTransactionDetail.Rows(0)("BillToParty"))
        Else
            bPartyLedgerPostingAc = AgL.XNull(DtTransactionDetail.Rows(0)("BillToParty"))
            bLinkedPartyAc = AgL.XNull(DtTransactionDetail.Rows(0)("SaleToParty"))
        End If

        ClsMain.PostStructureLineToAccounts(DtCalcHeaderData, DtCalcLineData, mNarrationParty, mNarration, mSearchCode, AgL.XNull(DtTransactionDetail.Rows(0)("Div_Code")),
                                    AgL.XNull(DtTransactionDetail.Rows(0)("Site_Code")),
                                    AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")), AgL.XNull(DtTransactionDetail.Rows(0)("V_Prefix")), AgL.VNull(DtTransactionDetail.Rows(0)("V_No")),
                                    AgL.XNull(DtTransactionDetail.Rows(0)("ManualRefNo")), bPartyLedgerPostingAc,
                                    AgL.XNull(DtTransactionDetail.Rows(0)("V_Date")), Conn, Cmd,, mMultiplyWithMinus, bLinkedPartyAc)
    End Sub

    Private Sub FrmSaleInvoiceDirect_BaseEvent_Topctrl_tbMore() Handles Me.BaseEvent_Topctrl_tbMore
        MnuOptions.Show(Topctrl1, Topctrl1.btbSite.Rectangle.X, Topctrl1.btbSite.Rectangle.Y + Topctrl1.btbSite.Rectangle.Size.Height)
    End Sub
    Private Sub Dgl2_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = LastDisplayedCell(Dgl2)
                If Dgl2.CurrentCell.RowIndex = LastCell.RowIndex And Dgl2.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                    If Dgl1.Visible Then
                        Dgl1.CurrentCell = Dgl1.FirstDisplayedCell
                        Dgl1.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function LastDisplayedCell(Dgl As AgControls.AgDataGrid) As DataGridViewCell
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0

        For I As Integer = Dgl.Columns.Count - 1 To 0 Step -1
            If Dgl.Columns(I).Visible = True Then
                bColumnIndex = I
                Exit For
            End If
        Next

        For I As Integer = Dgl.Rows.Count - 1 To 0 Step -1
            If Dgl.Rows(I).Visible = True Then
                bRowIndex = I
                Exit For
            End If
        Next
        LastDisplayedCell = Dgl.Item(bColumnIndex, bRowIndex)
    End Function
    Private Sub FShowRefrentialEntries(bDocId As String)
        Dim FrmObj As New FrmReferenceEntries()
        FrmObj.SearchCode = bDocId
        FrmObj.LblDocNo.Text = "Entry No : " + TxtReferenceNo.Text
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.MdiParent = Me.MdiParent
        FrmObj.Show()
    End Sub
    Private Shared Sub FGetMailConfiguration(objRepPrint As Object, SearchCode As String)
        Dim DtMailData As DataTable = AgL.FillData("Select H.Div_Code, H.Site_Code, H.V_Type, VT.NCat, VT.Category, 
                    H.ManualRefNo, H.V_Date, Sg.DispName As DivisionName, 
                    Party.DispName As PartyName, Party.EMail As PartyEMail,
                    Agent.DispName As AgentName, Agent.EMail As AgentEMail
                    From SaleInvoice H 
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SubGroup Agent On H.Agent = Agent.SubCode
                    Where H.DocId = '" & SearchCode & "'", AgL.GCn).Tables(0)

        objRepPrint.TxtToEmail.Text = ClsMain.FGetSettings(SettingFields.MailTo, SettingType.General, AgL.XNull(DtMailData.Rows(0)("Div_Code")), AgL.XNull(DtMailData.Rows(0)("Site_Code")), AgL.XNull(DtMailData.Rows(0)("Category")), AgL.XNull(DtMailData.Rows(0)("NCat")), AgL.XNull(DtMailData.Rows(0)("V_Type")), "", "")
        objRepPrint.TxtToEmail.Text = objRepPrint.TxtToEmail.Text.Replace("<PartyEMail>", AgL.XNull(DtMailData.Rows(0)("PartyEMail"))).
                Replace("<AgentEMail>", AgL.XNull(DtMailData.Rows(0)("AgentEMail")))

        objRepPrint.TxtCcEmail.Text = ClsMain.FGetSettings(SettingFields.MailCc, SettingType.General, AgL.XNull(DtMailData.Rows(0)("Div_Code")), AgL.XNull(DtMailData.Rows(0)("Site_Code")), AgL.XNull(DtMailData.Rows(0)("Category")), AgL.XNull(DtMailData.Rows(0)("NCat")), AgL.XNull(DtMailData.Rows(0)("V_Type")), "", "")
        objRepPrint.TxtCcEmail.Text = objRepPrint.TxtCcEmail.Text.Replace("<PartyEMail>", AgL.XNull(DtMailData.Rows(0)("PartyEMail"))).
                Replace("<AgentEMail>", AgL.XNull(DtMailData.Rows(0)("AgentEMail")))

        objRepPrint.TxtSubject.Text =
        objRepPrint.TxtSubject.Text = objRepPrint.TxtSubject.Text.Replace("<PartyName>", AgL.XNull(DtMailData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", AgL.XNull(DtMailData.Rows(0)("ManualRefNo"))).Replace("<EntryDate>", AgL.XNull(DtMailData.Rows(0)("V_Date"))).
                Replace("<DivisionName>", AgL.XNull(DtMailData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtMailData.Rows(0)("AgentName")))

        objRepPrint.TxtMessage.Text = ClsMain.FGetSettings(SettingFields.MailMessage, SettingType.General, AgL.XNull(DtMailData.Rows(0)("Div_Code")), AgL.XNull(DtMailData.Rows(0)("Site_Code")), AgL.XNull(DtMailData.Rows(0)("Category")), AgL.XNull(DtMailData.Rows(0)("NCat")), AgL.XNull(DtMailData.Rows(0)("V_Type")), "", "")
        objRepPrint.TxtMessage.Text = objRepPrint.TxtMessage.Text.Replace("<PartyName>", AgL.XNull(DtMailData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", AgL.XNull(DtMailData.Rows(0)("ManualRefNo"))).Replace("<EntryDate>", AgL.XNull(DtMailData.Rows(0)("V_Date"))).
                Replace("<DivisionName>", AgL.XNull(DtMailData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtMailData.Rows(0)("AgentName")))
        objRepPrint.AttachmentName = AgL.XNull(DtMailData.Rows(0)("ManualRefNo"))
    End Sub


    Private Sub FGetSmsConfiguration(objFrmSendSms As Object, SearchCode As String)
        Dim mDocNoPrefix As String = FGetSettings(SettingFields.DocumentPrintEntryNoPrefix, SettingType.General)

        Dim DtDocData As DataTable = AgL.FillData("Select 
                    Sg.DispName As DivisionName, 
                    Party.DispName As PartyName, Party.Mobile As PartyMobile,
                    Agent.DispName As AgentName, Agent.Mobile As AgentMobile, H.Net_Amount
                    From SaleInvoice H 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SubGroup Agent On H.Agent = Agent.SubCode
                    Where H.DocId = '" & SearchCode & "'", AgL.GCn).Tables(0)


        objFrmSendSms.TxtToMobile.text = AgL.XNull(DtDocData.Rows(0)("PartyMobile"))
        objFrmSendSms.TxtMessage.Text = FGetSettings(SettingFields.SmsMessage, SettingType.General)
        objFrmSendSms.TxtMessage.Text = objFrmSendSms.TxtMessage.Text.
                Replace("<PartyName>", AgL.XNull(DtDocData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", mDocNoPrefix & TxtReferenceNo.Text).Replace("<EntryDate>", TxtV_Date.Text).
                Replace("<DivisionName>", AgL.XNull(DtDocData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtDocData.Rows(0)("AgentName"))).
                Replace("<NetAmount>", Format(AgL.VNull(DtDocData.Rows(0)("Net_Amount")), "0.00")).
                Replace("&", "And")
    End Sub
    Private Sub FrmSaleInvoiceDirect_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
            If FGetSettings(SettingFields.ActionToPrintOnAdd, SettingType.General) = ActionToPrint.AskAndPrintOnScreen Then
                If MsgBox("Do you want to print ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
                End If
            ElseIf FGetSettings(SettingFields.ActionToPrintOnAdd, SettingType.General) = ActionToPrint.AskAndPrintToPrinter Then
                If MsgBox("Do you want to print ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint, True)
                End If
            ElseIf FGetSettings(SettingFields.ActionToPrintOnAdd, SettingType.General) = ActionToPrint.PrintOnScreen Then
                FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
            ElseIf FGetSettings(SettingFields.ActionToPrintOnAdd, SettingType.General) = ActionToPrint.PrintToPrinter Then
                FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint, True)
            End If
        End If
    End Sub
    Private Sub Dgl2_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl2.CellBeginEdit
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl2.CurrentCell.RowIndex
                Case rowSalesTaxNo, rowAadharNo
                    e.Cancel = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmSaleInvoiceDirect_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If ClsMain.IsEntryLockedWithLockText("SaleInvoice", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        Passed = Not FGetRelationalData()

        mQry = " SELECT Count(*) AS Cnt FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "' 
                    AND IfNull(T.IsDeletingAllowed,0) = 0 "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Refrential Entries Exist For This Entry.Can't Delete.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If
    End Sub

    Private Function FGetRelationalData() As Boolean
        Dim DtRelationalData As DataTable
        Try
            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From SaleInvoiceDetail L
                        LEFT JOIN SaleInvoice H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.SaleInvoice = '" & mSearchCode & "' 
                        And L.SaleInvoice <> L.DocId "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Text + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
    Private Sub FGetWhatsappConfiguration(objFrmSendWhatsapp As Object, SearchCode As String)
        Dim mDocNoPrefix As String = FGetSettings(SettingFields.DocumentPrintEntryNoPrefix, SettingType.General)

        Dim DtDocData As DataTable = AgL.FillData("Select 
                    Sg.DispName As DivisionName, 
                    (Case When IfNull(H.SaleToPartyName,'')='' Then 'Customer' Else IfNull(H.SaleToPartyName,'') End) As PartyName, H.SaleToPartyMobile As PartyMobile,
                    Agent.DispName As AgentName, Agent.Mobile As AgentMobile, H.Net_Amount,
                    Tranport.DispName As TransporterName, Sit.LrNo, Sit.LrDate 
                    From SaleInvoice H 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SubGroup Agent On H.Agent = Agent.SubCode
                    LEFT JOIN SaleInvoiceTransport Sit ON H.DocId = Sit.DocId
                    LEFT JOIN SubGroup Tranport On Sit.Transporter = Tranport.SubCode
                    Where H.DocId = '" & SearchCode & "'", AgL.GCn).Tables(0)


        objFrmSendWhatsapp.TxtToMobile.text = AgL.XNull(DtDocData.Rows(0)("PartyMobile"))
        objFrmSendWhatsapp.TxtMessage.Text = FGetSettings(SettingFields.SmsMessage, SettingType.General)
        objFrmSendWhatsapp.TxtMessage.Text = objFrmSendWhatsapp.TxtMessage.Text.
                Replace("<PartyName>", AgL.XNull(DtDocData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", mDocNoPrefix & TxtReferenceNo.Text).Replace("<EntryDate>", TxtV_Date.Text).
                Replace("<DivisionName>", AgL.XNull(DtDocData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtDocData.Rows(0)("AgentName"))).
                Replace("<TransporterName>", AgL.XNull(DtDocData.Rows(0)("TransporterName"))).
                Replace("<LrNo>", AgL.XNull(DtDocData.Rows(0)("LrNo"))).
                Replace("<LrDate>", AgL.XNull(DtDocData.Rows(0)("LrDate"))).
                Replace("<NetAmount>", Format(AgL.VNull(DtDocData.Rows(0)("Net_Amount")), "0.00")).
                Replace("&", "And")
        objFrmSendWhatsapp.AttachmentName = TxtReferenceNo.Text
    End Sub
End Class
