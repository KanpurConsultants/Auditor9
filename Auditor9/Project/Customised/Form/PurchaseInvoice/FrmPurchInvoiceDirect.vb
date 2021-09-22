Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Imports System.Xml
Imports System.IO
Imports Customised.ClsMain
Imports System.Linq

Public Class FrmPurchInvoiceDirect

    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As Object, ByVal Cmd As Object)

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Col1ItemType As String = AgL.PubCaptionItemType
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1BaleNo As String = "Bale No"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Public Const Col1DocQty As String = "Doc Qty"
    Public Const Col1FreeQty As String = "Free Qty"
    Public Const Col1RejQty As String = "Rej Qty"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Pcs As String = "Pcs"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1UnitMultiplier As String = "Unit Multiplier"
    Public Const Col1PcsPerMeasure As String = "Pcs Per Measure"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1DealUnit As String = "Deal Unit"
    Public Const Col1DealDecimalPlaces As String = "Deal Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1DiscountPer As String = "Disc. %"
    Public Const Col1DiscountAmount As String = "Disc. Amt"
    Public Const Col1AdditionalDiscountPer As String = "Add. Disc. %"
    Public Const Col1AdditionalDiscountAmount As String = "Add. Disc. Amt"
    Public Const Col1AdditionPer As String = "Addition %"
    Public Const Col1AdditionAmount As String = "Addition Amt"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1ExpiryDate As String = "Expiry Date"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1MRP As String = "MRP"
    Public Const Col1Deal As String = "Deal"
    Public Const Col1ProfitMarginPer As String = "Profit Margin %"
    Public Const Col1SaleRate As String = "Sale Rate"
    Public Const Col1LRNo As String = "L.R. No."
    Public Const Col1LRDate As String = "L.R. Date"
    Public Const Col1ReferenceNo As String = "Reference No"
    Public Const Col1ReferenceDate As String = "Reference Date"
    Public Const Col1ReferenceDocID As String = "Reference DocID"
    Public Const Col1ReferenceTSr As String = "Reference TSr"
    Public Const Col1ReferenceSr As String = "Reference Sr"
    Public Const Col1PurchaseInvoice As String = "Purchase Invoice DocID"
    Public Const Col1PurchaseInvoiceSr As String = "Purchase Invoice Sr"
    Public Const Col1DefaultDiscountPer As String = "Default Discount %"
    Public Const Col1DefaultAdditionalDiscountPer As String = "Default Additional Discount %"
    Public Const Col1DefaultAdditionPer As String = "Default Addition %"
    Public Const Col1PersonalDiscountPer As String = "Personal Discount %"
    Public Const Col1PersonalAdditionalDiscountPer As String = "Personal Additional Discount %"
    Public Const Col1PersonalAdditionPer As String = "Personal Addition %"
    Public Const Col1DiscountCalculationPattern As String = "Discount Calculation Pattern"
    Public Const Col1AdditionalDiscountCalculationPattern As String = "Additional Discount Calculation Pattern"
    Public Const Col1AdditionCalculationPattern As String = "Additional Calculation Pattern"
    Public Const Col1StockSr As String = "Stock Sr"

    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay

    Dim IsSameUnit As Boolean = True
    Dim IsSameDealUnit As Boolean = True
    Dim IsSameDeliveryDealUnit As Boolean = True

    Dim intQtyDecimalPlaces As Integer = 0
    Dim intDealDecimalPlaces As Integer = 0
    Dim intDeliveryDealDecimalPlaces As Integer = 0

    Dim DtItemTypeSettingsAll As DataTable

    Dim UserMovedOverItemGroup As Boolean
    Dim UserMovedOverItemCategory As Boolean

    Dim mIsEntryLocked As Boolean = False
    Public WithEvents TxtProcess As AgControls.AgTextBox
    Public WithEvents LblProcess As System.Windows.Forms.Label
    Friend WithEvents TP2 As TabPage
    Protected WithEvents BtnHeaderDetail As Button
    Protected WithEvents TxtAgent As AgControls.AgTextBox
    Protected WithEvents Label3 As Label
    Public WithEvents BtnBarcode As Button
    Dim DGL As New AgControls.AgDataGrid
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Friend WithEvents MnuEditSave As ToolStripMenuItem
    Public mDimensionSrl As Integer
    Friend WithEvents MnuGenerateEWayBill As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Public Shared mFlag_Import As Boolean = False
    Public WithEvents Label6 As Label
    Public WithEvents LblCurrentBalance As Label
    Public WithEvents TxtTags As AgControls.AgTextBox
    Public WithEvents LblTags As Label
    Friend WithEvents MnuRequestForPermission As ToolStripMenuItem
    Protected WithEvents BtnAttachments As Button
    Friend WithEvents MnuReferenceEntries As ToolStripMenuItem
    Friend WithEvents MnuHistory As ToolStripMenuItem
    Public WithEvents TxtShipToParty As AgControls.AgTextBox
    Public WithEvents LblShipToParty As Label
    Friend WithEvents MnuShowLedgerPosting As ToolStripMenuItem
    Dim mFirstInvoiceForSelectedParty As Boolean = False

    Dim mFullItemListInHelp As Boolean = False

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from PurchaseInvoiceSetting H  With (NoLock) Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') Or H.V_Type Is Null  "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtVendor = New AgControls.AgTextBox()
        Me.LblVendor = New System.Windows.Forms.Label()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalDealQty = New System.Windows.Forms.Label()
        Me.LblTotalDealQtyText = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalAmount = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalAmountText = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.TxtStructure = New AgControls.AgTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TxtRemarks = New AgControls.AgTextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.LblVendorDocNo = New System.Windows.Forms.Label()
        Me.TxtVendorDocNo = New AgControls.AgTextBox()
        Me.LblVendorDocDate = New System.Windows.Forms.Label()
        Me.TxtVendorDocDate = New AgControls.AgTextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.PnlCalcGrid = New System.Windows.Forms.Panel()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtBillToParty = New AgControls.AgTextBox()
        Me.LblPostToAc = New System.Windows.Forms.Label()
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button()
        Me.TxtNature = New AgControls.AgTextBox()
        Me.TxtProcess = New AgControls.AgTextBox()
        Me.LblProcess = New System.Windows.Forms.Label()
        Me.TP2 = New System.Windows.Forms.TabPage()
        Me.BtnHeaderDetail = New System.Windows.Forms.Button()
        Me.TxtAgent = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnBarcode = New System.Windows.Forms.Button()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEditSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuGenerateEWayBill = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRequestForPermission = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReferenceEntries = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuShowLedgerPosting = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblCurrentBalance = New System.Windows.Forms.Label()
        Me.TxtTags = New AgControls.AgTextBox()
        Me.LblTags = New System.Windows.Forms.Label()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.TxtShipToParty = New AgControls.AgTextBox()
        Me.LblShipToParty = New System.Windows.Forms.Label()
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
        Me.TP2.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(820, 575)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(195, 576)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(142, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(620, 575)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
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
        Me.GrpUP.Location = New System.Drawing.Point(16, 576)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 569)
        Me.GroupBox1.Size = New System.Drawing.Size(992, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(404, 575)
        Me.GBoxDivision.Size = New System.Drawing.Size(151, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Size = New System.Drawing.Size(145, 18)
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
        Me.Label2.Location = New System.Drawing.Point(333, 30)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Date.Location = New System.Drawing.Point(231, 25)
        Me.LblV_Date.Size = New System.Drawing.Size(96, 14)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Voucher Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(545, 12)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(349, 24)
        Me.TxtV_Date.Size = New System.Drawing.Size(100, 16)
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(455, 8)
        Me.LblV_Type.Size = New System.Drawing.Size(92, 14)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Invoice Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgLastValueTag = ""
        Me.TxtV_Type.AgLastValueText = ""
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Type.Location = New System.Drawing.Point(563, 6)
        Me.TxtV_Type.Size = New System.Drawing.Size(182, 16)
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(333, 12)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(231, 7)
        Me.LblSite_Code.Size = New System.Drawing.Size(95, 14)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(349, 6)
        Me.TxtSite_Code.Size = New System.Drawing.Size(100, 16)
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
        Me.TabControl1.Controls.Add(Me.TP2)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 19)
        Me.TabControl1.Size = New System.Drawing.Size(992, 148)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.Controls.SetChildIndex(Me.TP2, 0)
        Me.TabControl1.Controls.SetChildIndex(Me.TP1, 0)
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtShipToParty)
        Me.TP1.Controls.Add(Me.LblShipToParty)
        Me.TP1.Controls.Add(Me.BtnHeaderDetail)
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtBillToParty)
        Me.TP1.Controls.Add(Me.LblPostToAc)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.LblVendor)
        Me.TP1.Controls.Add(Me.TxtVendorDocNo)
        Me.TP1.Controls.Add(Me.LblVendorDocNo)
        Me.TP1.Controls.Add(Me.TxtVendorDocDate)
        Me.TP1.Controls.Add(Me.LblVendorDocDate)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 122)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorDocDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorDocDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPostToAc, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnHeaderDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblShipToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtShipToParty, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(974, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(545, 30)
        Me.Label1.TabIndex = 737
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferenceNo.Location = New System.Drawing.Point(563, 24)
        Me.TxtReferenceNo.Size = New System.Drawing.Size(182, 16)
        Me.TxtReferenceNo.TabIndex = 4
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(455, 24)
        Me.LblReferenceNo.Size = New System.Drawing.Size(87, 14)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Voucher No."
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
        Me.Label4.Location = New System.Drawing.Point(333, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 694
        Me.Label4.Text = "Ä"
        '
        'TxtVendor
        '
        Me.TxtVendor.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendor.AgLastValueTag = Nothing
        Me.TxtVendor.AgLastValueText = Nothing
        Me.TxtVendor.AgMandatory = True
        Me.TxtVendor.AgMasterHelp = False
        Me.TxtVendor.AgNumberLeftPlaces = 8
        Me.TxtVendor.AgNumberNegetiveAllow = False
        Me.TxtVendor.AgNumberRightPlaces = 2
        Me.TxtVendor.AgPickFromLastValue = False
        Me.TxtVendor.AgRowFilter = ""
        Me.TxtVendor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendor.AgSelectedValue = Nothing
        Me.TxtVendor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendor.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendor.Location = New System.Drawing.Point(349, 42)
        Me.TxtVendor.MaxLength = 0
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(367, 16)
        Me.TxtVendor.TabIndex = 5
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(231, 42)
        Me.LblVendor.Name = "LblVendor"
        Me.LblVendor.Size = New System.Drawing.Size(62, 14)
        Me.LblVendor.TabIndex = 693
        Me.LblVendor.Text = "Supplier"
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalDealQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalDealQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(3, 373)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(967, 23)
        Me.PnlTotals.TabIndex = 694
        '
        'LblTotalDealQty
        '
        Me.LblTotalDealQty.AutoSize = True
        Me.LblTotalDealQty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDealQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalDealQty.Location = New System.Drawing.Point(576, 3)
        Me.LblTotalDealQty.Name = "LblTotalDealQty"
        Me.LblTotalDealQty.Size = New System.Drawing.Size(13, 16)
        Me.LblTotalDealQty.TabIndex = 666
        Me.LblTotalDealQty.Text = "."
        Me.LblTotalDealQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalDealQtyText
        '
        Me.LblTotalDealQtyText.AutoSize = True
        Me.LblTotalDealQtyText.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDealQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDealQtyText.Location = New System.Drawing.Point(465, 3)
        Me.LblTotalDealQtyText.Name = "LblTotalDealQtyText"
        Me.LblTotalDealQtyText.Size = New System.Drawing.Size(111, 14)
        Me.LblTotalDealQtyText.TabIndex = 665
        Me.LblTotalDealQtyText.Text = "Total Deal Qty :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(91, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(13, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(332, 4)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(13, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(12, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(77, 14)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(228, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(103, 14)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(3, 191)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(968, 184)
        Me.Pnl1.TabIndex = 10
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
        'TxtRemarks
        '
        Me.TxtRemarks.AgAllowUserToEnableMasterHelp = False
        Me.TxtRemarks.AgLastValueTag = Nothing
        Me.TxtRemarks.AgLastValueText = Nothing
        Me.TxtRemarks.AgMandatory = False
        Me.TxtRemarks.AgMasterHelp = False
        Me.TxtRemarks.AgNumberLeftPlaces = 0
        Me.TxtRemarks.AgNumberNegetiveAllow = False
        Me.TxtRemarks.AgNumberRightPlaces = 0
        Me.TxtRemarks.AgPickFromLastValue = False
        Me.TxtRemarks.AgRowFilter = ""
        Me.TxtRemarks.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRemarks.AgSelectedValue = Nothing
        Me.TxtRemarks.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemarks.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemarks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemarks.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(76, 418)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(238, 16)
        Me.TxtRemarks.TabIndex = 12
        '
        'Label30
        '
        Me.Label30.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(3, 419)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(65, 14)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'LblVendorDocNo
        '
        Me.LblVendorDocNo.AutoSize = True
        Me.LblVendorDocNo.BackColor = System.Drawing.Color.Transparent
        Me.LblVendorDocNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendorDocNo.Location = New System.Drawing.Point(230, 79)
        Me.LblVendorDocNo.Name = "LblVendorDocNo"
        Me.LblVendorDocNo.Size = New System.Drawing.Size(117, 14)
        Me.LblVendorDocNo.TabIndex = 706
        Me.LblVendorDocNo.Text = "Supplier Doc No."
        '
        'TxtVendorDocNo
        '
        Me.TxtVendorDocNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendorDocNo.AgLastValueTag = Nothing
        Me.TxtVendorDocNo.AgLastValueText = Nothing
        Me.TxtVendorDocNo.AgMandatory = False
        Me.TxtVendorDocNo.AgMasterHelp = True
        Me.TxtVendorDocNo.AgNumberLeftPlaces = 8
        Me.TxtVendorDocNo.AgNumberNegetiveAllow = False
        Me.TxtVendorDocNo.AgNumberRightPlaces = 2
        Me.TxtVendorDocNo.AgPickFromLastValue = False
        Me.TxtVendorDocNo.AgRowFilter = ""
        Me.TxtVendorDocNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorDocNo.AgSelectedValue = Nothing
        Me.TxtVendorDocNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorDocNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorDocNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorDocNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorDocNo.Location = New System.Drawing.Point(349, 78)
        Me.TxtVendorDocNo.MaxLength = 20
        Me.TxtVendorDocNo.Name = "TxtVendorDocNo"
        Me.TxtVendorDocNo.Size = New System.Drawing.Size(145, 16)
        Me.TxtVendorDocNo.TabIndex = 7
        '
        'LblVendorDocDate
        '
        Me.LblVendorDocDate.AutoSize = True
        Me.LblVendorDocDate.BackColor = System.Drawing.Color.Transparent
        Me.LblVendorDocDate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendorDocDate.Location = New System.Drawing.Point(500, 80)
        Me.LblVendorDocDate.Name = "LblVendorDocDate"
        Me.LblVendorDocDate.Size = New System.Drawing.Size(114, 14)
        Me.LblVendorDocDate.TabIndex = 708
        Me.LblVendorDocDate.Text = "Supplier Doc Dt."
        '
        'TxtVendorDocDate
        '
        Me.TxtVendorDocDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendorDocDate.AgLastValueTag = Nothing
        Me.TxtVendorDocDate.AgLastValueText = Nothing
        Me.TxtVendorDocDate.AgMandatory = False
        Me.TxtVendorDocDate.AgMasterHelp = True
        Me.TxtVendorDocDate.AgNumberLeftPlaces = 8
        Me.TxtVendorDocDate.AgNumberNegetiveAllow = False
        Me.TxtVendorDocDate.AgNumberRightPlaces = 2
        Me.TxtVendorDocDate.AgPickFromLastValue = False
        Me.TxtVendorDocDate.AgRowFilter = ""
        Me.TxtVendorDocDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorDocDate.AgSelectedValue = Nothing
        Me.TxtVendorDocDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorDocDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtVendorDocDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorDocDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorDocDate.Location = New System.Drawing.Point(620, 78)
        Me.TxtVendorDocDate.MaxLength = 20
        Me.TxtVendorDocDate.Name = "TxtVendorDocDate"
        Me.TxtVendorDocDate.Size = New System.Drawing.Size(125, 16)
        Me.TxtVendorDocDate.TabIndex = 8
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(3, 170)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(271, 19)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Invoice For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCalcGrid.Location = New System.Drawing.Point(651, 399)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(320, 164)
        Me.PnlCalcGrid.TabIndex = 16
        Me.PnlCalcGrid.Visible = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlCustomGrid.Location = New System.Drawing.Point(320, 399)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(325, 164)
        Me.PnlCustomGrid.TabIndex = 15
        Me.PnlCustomGrid.Visible = False
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(522, 654)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1012
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(333, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 3006
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
        Me.TxtBillToParty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillToParty.Location = New System.Drawing.Point(349, 60)
        Me.TxtBillToParty.MaxLength = 0
        Me.TxtBillToParty.Name = "TxtBillToParty"
        Me.TxtBillToParty.Size = New System.Drawing.Size(396, 16)
        Me.TxtBillToParty.TabIndex = 6
        '
        'LblPostToAc
        '
        Me.LblPostToAc.AutoSize = True
        Me.LblPostToAc.BackColor = System.Drawing.Color.Transparent
        Me.LblPostToAc.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPostToAc.Location = New System.Drawing.Point(231, 61)
        Me.LblPostToAc.Name = "LblPostToAc"
        Me.LblPostToAc.Size = New System.Drawing.Size(81, 14)
        Me.LblPostToAc.TabIndex = 3005
        Me.LblPostToAc.Text = "Post to A/c"
        '
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Britannic Bold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.Black
        Me.BtnFillPartyDetail.Image = Global.Customised.My.Resources.Resources._41104_200
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(719, 42)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(27, 16)
        Me.BtnFillPartyDetail.TabIndex = 3007
        Me.BtnFillPartyDetail.TabStop = False
        Me.BtnFillPartyDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPartyDetail.UseVisualStyleBackColor = True
        '
        'TxtNature
        '
        Me.TxtNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtNature.AgLastValueTag = Nothing
        Me.TxtNature.AgLastValueText = Nothing
        Me.TxtNature.AgMandatory = True
        Me.TxtNature.AgMasterHelp = False
        Me.TxtNature.AgNumberLeftPlaces = 0
        Me.TxtNature.AgNumberNegetiveAllow = False
        Me.TxtNature.AgNumberRightPlaces = 0
        Me.TxtNature.AgPickFromLastValue = False
        Me.TxtNature.AgRowFilter = ""
        Me.TxtNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNature.AgSelectedValue = Nothing
        Me.TxtNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNature.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNature.Location = New System.Drawing.Point(889, 171)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(81, 15)
        Me.TxtNature.TabIndex = 1208
        Me.TxtNature.Text = "TxtNature"
        Me.TxtNature.Visible = False
        '
        'TxtProcess
        '
        Me.TxtProcess.AgAllowUserToEnableMasterHelp = False
        Me.TxtProcess.AgLastValueTag = Nothing
        Me.TxtProcess.AgLastValueText = ""
        Me.TxtProcess.AgMandatory = False
        Me.TxtProcess.AgMasterHelp = False
        Me.TxtProcess.AgNumberLeftPlaces = 8
        Me.TxtProcess.AgNumberNegetiveAllow = False
        Me.TxtProcess.AgNumberRightPlaces = 2
        Me.TxtProcess.AgPickFromLastValue = False
        Me.TxtProcess.AgRowFilter = ""
        Me.TxtProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcess.AgSelectedValue = Nothing
        Me.TxtProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProcess.Location = New System.Drawing.Point(720, 17)
        Me.TxtProcess.MaxLength = 0
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(188, 18)
        Me.TxtProcess.TabIndex = 9
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(598, 17)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 3009
        Me.LblProcess.Text = "Process"
        '
        'TP2
        '
        Me.TP2.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP2.Controls.Add(Me.TxtProcess)
        Me.TP2.Controls.Add(Me.LblProcess)
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Padding = New System.Windows.Forms.Padding(3)
        Me.TP2.Size = New System.Drawing.Size(984, 122)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "TabPage1"
        '
        'BtnHeaderDetail
        '
        Me.BtnHeaderDetail.BackColor = System.Drawing.Color.Transparent
        Me.BtnHeaderDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnHeaderDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHeaderDetail.ForeColor = System.Drawing.Color.Black
        Me.BtnHeaderDetail.Location = New System.Drawing.Point(755, 59)
        Me.BtnHeaderDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnHeaderDetail.Name = "BtnHeaderDetail"
        Me.BtnHeaderDetail.Size = New System.Drawing.Size(81, 36)
        Me.BtnHeaderDetail.TabIndex = 9
        Me.BtnHeaderDetail.TabStop = False
        Me.BtnHeaderDetail.Text = "Transport Detail"
        Me.BtnHeaderDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnHeaderDetail.UseVisualStyleBackColor = False
        '
        'TxtAgent
        '
        Me.TxtAgent.AgAllowUserToEnableMasterHelp = False
        Me.TxtAgent.AgLastValueTag = Nothing
        Me.TxtAgent.AgLastValueText = Nothing
        Me.TxtAgent.AgMandatory = False
        Me.TxtAgent.AgMasterHelp = False
        Me.TxtAgent.AgNumberLeftPlaces = 8
        Me.TxtAgent.AgNumberNegetiveAllow = False
        Me.TxtAgent.AgNumberRightPlaces = 2
        Me.TxtAgent.AgPickFromLastValue = False
        Me.TxtAgent.AgRowFilter = ""
        Me.TxtAgent.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAgent.AgSelectedValue = Nothing
        Me.TxtAgent.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAgent.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAgent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtAgent.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAgent.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAgent.Location = New System.Drawing.Point(76, 400)
        Me.TxtAgent.MaxLength = 20
        Me.TxtAgent.Name = "TxtAgent"
        Me.TxtAgent.Size = New System.Drawing.Size(238, 16)
        Me.TxtAgent.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 400)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 14)
        Me.Label3.TabIndex = 3012
        Me.Label3.Text = "Agent"
        '
        'BtnBarcode
        '
        Me.BtnBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBarcode.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBarcode.Location = New System.Drawing.Point(6, 544)
        Me.BtnBarcode.Name = "BtnBarcode"
        Me.BtnBarcode.Size = New System.Drawing.Size(84, 23)
        Me.BtnBarcode.TabIndex = 3013
        Me.BtnBarcode.Text = "Barcode"
        Me.BtnBarcode.UseVisualStyleBackColor = True
        Me.BtnBarcode.Visible = False
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuEditSave, Me.MnuGenerateEWayBill, Me.MnuRequestForPermission, Me.MnuReferenceEntries, Me.MnuShowLedgerPosting, Me.MnuHistory})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(198, 202)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(197, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(197, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(197, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuEditSave
        '
        Me.MnuEditSave.Name = "MnuEditSave"
        Me.MnuEditSave.Size = New System.Drawing.Size(197, 22)
        Me.MnuEditSave.Text = "Edit & Save"
        '
        'MnuGenerateEWayBill
        '
        Me.MnuGenerateEWayBill.Name = "MnuGenerateEWayBill"
        Me.MnuGenerateEWayBill.Size = New System.Drawing.Size(197, 22)
        Me.MnuGenerateEWayBill.Text = "Generate EWay Bill"
        '
        'MnuRequestForPermission
        '
        Me.MnuRequestForPermission.Name = "MnuRequestForPermission"
        Me.MnuRequestForPermission.Size = New System.Drawing.Size(197, 22)
        Me.MnuRequestForPermission.Text = "Request For Permission"
        '
        'MnuReferenceEntries
        '
        Me.MnuReferenceEntries.Name = "MnuReferenceEntries"
        Me.MnuReferenceEntries.Size = New System.Drawing.Size(197, 22)
        Me.MnuReferenceEntries.Text = "Reference Entries"
        '
        'MnuShowLedgerPosting
        '
        Me.MnuShowLedgerPosting.Name = "MnuShowLedgerPosting"
        Me.MnuShowLedgerPosting.Size = New System.Drawing.Size(197, 22)
        Me.MnuShowLedgerPosting.Text = "Show Ledger Posting"
        '
        'MnuHistory
        '
        Me.MnuHistory.Name = "MnuHistory"
        Me.MnuHistory.Size = New System.Drawing.Size(197, 22)
        Me.MnuHistory.Text = "History"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(622, 171)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 16)
        Me.Label6.TabIndex = 3015
        Me.Label6.Text = "Curr. Balance"
        '
        'LblCurrentBalance
        '
        Me.LblCurrentBalance.AutoSize = True
        Me.LblCurrentBalance.BackColor = System.Drawing.Color.White
        Me.LblCurrentBalance.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentBalance.Location = New System.Drawing.Point(734, 171)
        Me.LblCurrentBalance.Name = "LblCurrentBalance"
        Me.LblCurrentBalance.Size = New System.Drawing.Size(103, 16)
        Me.LblCurrentBalance.TabIndex = 3016
        Me.LblCurrentBalance.Text = "99999999.99"
        '
        'TxtTags
        '
        Me.TxtTags.AgAllowUserToEnableMasterHelp = False
        Me.TxtTags.AgLastValueTag = Nothing
        Me.TxtTags.AgLastValueText = Nothing
        Me.TxtTags.AgMandatory = False
        Me.TxtTags.AgMasterHelp = False
        Me.TxtTags.AgNumberLeftPlaces = 0
        Me.TxtTags.AgNumberNegetiveAllow = False
        Me.TxtTags.AgNumberRightPlaces = 0
        Me.TxtTags.AgPickFromLastValue = False
        Me.TxtTags.AgRowFilter = ""
        Me.TxtTags.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTags.AgSelectedValue = Nothing
        Me.TxtTags.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTags.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTags.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTags.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTags.Location = New System.Drawing.Point(76, 436)
        Me.TxtTags.MaxLength = 255
        Me.TxtTags.Name = "TxtTags"
        Me.TxtTags.Size = New System.Drawing.Size(238, 16)
        Me.TxtTags.TabIndex = 3017
        '
        'LblTags
        '
        Me.LblTags.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblTags.AutoSize = True
        Me.LblTags.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTags.Location = New System.Drawing.Point(1, 437)
        Me.LblTags.Name = "LblTags"
        Me.LblTags.Size = New System.Drawing.Size(38, 14)
        Me.LblTags.TabIndex = 3018
        Me.LblTags.Text = "Tags"
        '
        'BtnAttachments
        '
        Me.BtnAttachments.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAttachments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAttachments.Location = New System.Drawing.Point(6, 518)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(134, 23)
        Me.BtnAttachments.TabIndex = 3019
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Add Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'TxtShipToParty
        '
        Me.TxtShipToParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtShipToParty.AgLastValueTag = Nothing
        Me.TxtShipToParty.AgLastValueText = Nothing
        Me.TxtShipToParty.AgMandatory = False
        Me.TxtShipToParty.AgMasterHelp = False
        Me.TxtShipToParty.AgNumberLeftPlaces = 8
        Me.TxtShipToParty.AgNumberNegetiveAllow = False
        Me.TxtShipToParty.AgNumberRightPlaces = 2
        Me.TxtShipToParty.AgPickFromLastValue = False
        Me.TxtShipToParty.AgRowFilter = ""
        Me.TxtShipToParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipToParty.AgSelectedValue = Nothing
        Me.TxtShipToParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipToParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShipToParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipToParty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipToParty.Location = New System.Drawing.Point(350, 97)
        Me.TxtShipToParty.MaxLength = 0
        Me.TxtShipToParty.Name = "TxtShipToParty"
        Me.TxtShipToParty.Size = New System.Drawing.Size(395, 16)
        Me.TxtShipToParty.TabIndex = 9
        Me.TxtShipToParty.Visible = False
        '
        'LblShipToParty
        '
        Me.LblShipToParty.AutoSize = True
        Me.LblShipToParty.BackColor = System.Drawing.Color.Transparent
        Me.LblShipToParty.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShipToParty.Location = New System.Drawing.Point(231, 98)
        Me.LblShipToParty.Name = "LblShipToParty"
        Me.LblShipToParty.Size = New System.Drawing.Size(96, 14)
        Me.LblShipToParty.TabIndex = 3009
        Me.LblShipToParty.Text = "Ship To Party"
        Me.LblShipToParty.Visible = False
        '
        'FrmPurchInvoiceDirect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(974, 622)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.TxtTags)
        Me.Controls.Add(Me.LblTags)
        Me.Controls.Add(Me.LblCurrentBalance)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BtnBarcode)
        Me.Controls.Add(Me.TxtAgent)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtNature)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label30)
        Me.MaximizeBox = True
        Me.Name = "FrmPurchInvoiceDirect"
        Me.Text = "Purchase Invoice"
        Me.Controls.SetChildIndex(Me.Label30, 0)
        Me.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.TxtNature, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtAgent, 0)
        Me.Controls.SetChildIndex(Me.BtnBarcode, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.LblCurrentBalance, 0)
        Me.Controls.SetChildIndex(Me.LblTags, 0)
        Me.Controls.SetChildIndex(Me.TxtTags, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
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
        Me.TP2.ResumeLayout(False)
        Me.TP2.PerformLayout()
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents LblVendor As System.Windows.Forms.Label
    Public WithEvents TxtVendor As AgControls.AgTextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents PnlTotals As System.Windows.Forms.Panel
    Public WithEvents LblTotalQty As System.Windows.Forms.Label
    Public WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents TxtStructure As AgControls.AgTextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents LblTotalAmount As System.Windows.Forms.Label
    Public WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Public WithEvents TxtRemarks As AgControls.AgTextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents LblTotalDealQty As System.Windows.Forms.Label
    Public WithEvents LblTotalDealQtyText As System.Windows.Forms.Label

    Public WithEvents TxtVendorDocDate As AgControls.AgTextBox
    Public WithEvents LblVendorDocDate As System.Windows.Forms.Label
    Public WithEvents TxtVendorDocNo As AgControls.AgTextBox
    Public WithEvents LblVendorDocNo As System.Windows.Forms.Label
    Public WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Public WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Public WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents TxtBillToParty As AgControls.AgTextBox
    Public WithEvents LblPostToAc As System.Windows.Forms.Label
    Public WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Public WithEvents TxtNature As AgControls.AgTextBox
#End Region

    Private Sub FrmPurchInvoice_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceTransport Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceDimensionDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From BarcodeSiteDetail Where Code In (Select Code From Barcode Where  GenDocID ='" & mSearchCode & "' ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Barcode Where GenDocID ='" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Stock Where DocID = (Select DocID From StockHead Where GenDocID ='" & mSearchCode & "') "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From BarcodeSiteDetail Where Code In (
                            Select Bc.Code
                            From StockHead H
                            LEFT JOIN BarCode Bc ON H.DocId = Bc.GenDocId
                            Where H.GenDocID ='" & mSearchCode & "') "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Update PurchInvoiceDetail Set LrCode = Null, LrBaleCode = Null Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From BarCode Where Code In (
                            Select Bc.Code
                            From StockHead H
                            LEFT JOIN BarCode Bc ON H.DocId = Bc.GenDocId
                            Where H.GenDocID ='" & mSearchCode & "') "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From StockHeadDetailBarCodeValues Where DocID = (Select DocID From StockHead Where GenDocID ='" & mSearchCode & "') "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From StockHeadDetail Where DocID = (Select DocID From StockHead Where GenDocID ='" & mSearchCode & "') "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From StockHead Where GenDocID ='" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    End Sub


    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Try

            LblTags.Visible = False
            TxtTags.Visible = False

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


            If Topctrl1.Mode = "Browse" Then
                mQry = "Select IsNull(Max(Cast(IT.IsApplicable_Barcode as Int)),0) from ItemTypeSetting IT where ItemType in (Select ItemType From Item Where Code in (Select Item From PurchInvoiceDetail Where DocID = '" & mSearchCode & "'))"
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                    BtnBarcode.Visible = True
                Else
                    BtnBarcode.Visible = False
                End If
            End If


            If ClsMain.FDivisionNameForCustomization(11) = "SHIVA SAREE" Then
                If LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.PurchaseReturn Then
                    LblShipToParty.Visible = True
                    TxtShipToParty.Visible = True
                End If
            Else
                LblShipToParty.Visible = False
                TxtShipToParty.Visible = False
            End If


        Catch ex As Exception
            MsgBox(ex.Message & " [ApplyUISettings]")
        End Try
    End Sub

    Public Function FItemTypeSettings(ItemType As String) As DataRow
        Dim DrItemTypeSetting As DataRow()

        DrItemTypeSetting = DtItemTypeSettingsAll.Select("ItemType='" & ItemType & "' And Div_Code='" & TxtDivision.Tag & "'")
        If DrItemTypeSetting.Length <= 0 Then
            DrItemTypeSetting = DtItemTypeSettingsAll.Select("ItemType='" & ItemType & "'")
        End If

        FItemTypeSettings = DrItemTypeSetting(0)
    End Function

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchInvoice"
        MainLineTableCsv = "PurchInvoiceDetail,PurchInvoiceDetailSku"
        LogTableName = "PurchInvoice_Log"
        LogLineTableCsv = "PurchInvoiceDetail_Log,PurchInvoiceDetailSku_Log"


        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL

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

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP  With (NoLock) Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        '       mCondStr = mCondStr & " AND H.DocID IN ('D1    PI 2018     346', 'D1   WPI 2018    3244', 'D1   WPI 2018    3246', 'D2    PI 2018      16', 'D2    PI 2018      18', 'D2    PI 2018      20', 'D2    PI 2018      26', 'D2    PI 2018      27', 'D2    PI 2018      28', 'D2    PI 2018      29', 'D2    PI 2018      30', 'D2    PI 2018     159', 'D2    PI 2018     547', 'D2    PI 2018     595', 'D2    PI 2018     597', 'D2   WPI 2018    2890', 'D2   WPI 2018    3083', 'D5    PI 2018      38', 'D5    PI 2018      40', 'D5    PI 2018      42', 'D5    PI 2018      44', 'D5    PI 2018      45', 'D5    PI 2018      47', 'D5    PI 2018      48', 'D5    PI 2018      50', 'D5    PI 2018      51', 'D5    PI 2018      56')
        '"

        mQry = "Select DocID As SearchCode " &
                " From PurchInvoice H  With (NoLock) " &
                " Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  " &
                " Where 1=1  " & mCondStr & "  Order By V_Date , V_No "

        'mQry = "Select H.DocID As SearchCode 
        '        From PurchInvoice H  With (NoLock)
        '        Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  
        '        LEFT JOIN Ledger L With (NoLock) On H.DocId = L.DocId
        '        Where L.DocId Is Null  " & mCondStr & "  Order By H.V_Date , H.V_No  "
        mQry = mQry.Replace("T00:00:00", "")
        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP  With (NoLock) Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If
        mCondStr = mCondStr.Replace("T00:00:00", "")

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Invoice_Type], H.V_Date AS Date, 
                             H.ManualRefNo As [Manual_No], SGV.DispName As Vendor, H.SalesTaxGroupParty As [Sales_Tax_Group_Party], H.VendorDocNo As [Vendor_Doc_No],  
                             H.VendorDocDate As [Vendor_Doc_Date], H.Remarks, Pt.LrNo as [LR No], Pt.LrDate [LR Date],
                             H.EntryBy As [Entry_By], H.EntryDate As [Entry_Date] 
                             From PurchInvoice H   With (NoLock)
                             Left Join PurchInvoiceTransport Pt With (NoLock) On H.DocID = Pt.DocID
                             LEFT Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type 
                             Left Join SubGroup SGV  With (NoLock) On SGV.SubCode  = H.Vendor  
                             Where 1 = 1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub Frm_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim errRow As Integer = 0
        Try
            If DtV_TypeSettings Is Nothing Then Exit Sub
            If DtV_TypeSettings.Rows.Count = 0 Then Exit Sub
            Dgl1.ColumnCount = 0
            With AgCL
                .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
                .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, Col1ItemType, False, False)
                .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, True, False)
                .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, False)
                .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, False, False)
                .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False)
                .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, False, False)
                .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, False, False)
                .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, False, False)
                .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, False, False)
                .AddAgTextColumn(Dgl1, Col1Specification, 100, 255, Col1Specification, False, False, False)
                .AddAgTextColumn(Dgl1, Col1BaleNo, 50, 0, Col1BaleNo, False, False)
                .AddAgTextColumn(Dgl1, Col1LotNo, 50, 0, Col1LotNo, False, False)
                .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 8, 4, False, Col1DocQty, True, False, True)
                .AddAgNumberColumn(Dgl1, Col1FreeQty, 60, 8, 3, False, Col1FreeQty, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1RejQty, 70, 8, 4, False, Col1RejQty, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, False, Col1Qty, False, True, True)
                .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
                .AddAgNumberColumn(Dgl1, Col1Pcs, 70, 8, 0, False, Col1Pcs, False, True, True)
                .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
                .AddAgNumberColumn(Dgl1, Col1UnitMultiplier, 70, 8, 3, False, Col1UnitMultiplier, False, True, True)
                .AddAgNumberColumn(Dgl1, Col1PcsPerMeasure, 70, 8, 3, False, Col1PcsPerMeasure, False, True, True)
                .AddAgNumberColumn(Dgl1, Col1DealQty, 70, 8, 3, False, Col1DealQty, False, True, True)
                .AddAgTextColumn(Dgl1, Col1DealUnit, 60, 0, Col1DealUnit, False, True)
                .AddAgTextColumn(Dgl1, Col1DealDecimalPlaces, 50, 0, Col1DealDecimalPlaces, False, True, False)
                .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 3, False, Col1Rate, True, False, True)
                .AddAgNumberColumn(Dgl1, Col1DiscountPer, 80, 2, 3, False, Col1DiscountPer, True, False, True)
                .AddAgNumberColumn(Dgl1, Col1DiscountAmount, 100, 8, 3, False, Col1DiscountAmount, True, False, True)
                .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountPer, 80, 2, 3, False, Col1AdditionalDiscountPer, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountAmount, 100, 8, 3, False, Col1AdditionalDiscountAmount, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1AdditionPer, 80, 2, 3, False, Col1AdditionPer, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1AdditionAmount, 100, 8, 3, False, Col1AdditionAmount, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
                .AddAgNumberColumn(Dgl1, Col1MRP, 80, 8, 2, False, Col1MRP, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1SaleRate, 80, 8, 2, False, Col1SaleRate, False, False, True)
                .AddAgDateColumn(Dgl1, Col1ExpiryDate, 90, Col1ExpiryDate, False, False)
                .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
                .AddAgTextColumn(Dgl1, Col1LRNo, 90, 50, Col1LRNo, False, False)
                .AddAgDateColumn(Dgl1, Col1LRDate, 90, Col1LRDate, False, False)
                .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 60, 0, Col1SalesTaxGroup, True, False)
                .AddAgTextColumn(Dgl1, Col1Deal, 70, 255, Col1Deal, False, False)
                .AddAgNumberColumn(Dgl1, Col1ProfitMarginPer, 100, 8, 2, False, Col1ProfitMarginPer, False, False, True)
                .AddAgTextColumn(Dgl1, Col1ReferenceNo, 80, 0, Col1ReferenceNo, True, False)
                .AddAgDateColumn(Dgl1, Col1ReferenceDate, 80, Col1ReferenceDate, True, False)
                .AddAgTextColumn(Dgl1, Col1ReferenceDocID, 80, 0, Col1ReferenceDocID, False, False)
                .AddAgTextColumn(Dgl1, Col1ReferenceTSr, 80, 0, Col1ReferenceTSr, False, False)
                .AddAgTextColumn(Dgl1, Col1ReferenceSr, 80, 0, Col1ReferenceSr, False, False)
                .AddAgTextColumn(Dgl1, Col1PurchaseInvoice, 80, 0, Col1PurchaseInvoice, False, False)
                .AddAgTextColumn(Dgl1, Col1PurchaseInvoiceSr, 80, 0, Col1PurchaseInvoiceSr, False, False)
                .AddAgTextColumn(Dgl1, Col1DefaultDiscountPer, 150, 255, Col1DefaultDiscountPer, False, False)
                .AddAgTextColumn(Dgl1, Col1DefaultAdditionalDiscountPer, 150, 255, Col1DefaultAdditionalDiscountPer, False, False)
                .AddAgTextColumn(Dgl1, Col1DefaultAdditionPer, 150, 255, Col1DefaultAdditionPer, False, False)
                .AddAgTextColumn(Dgl1, Col1PersonalDiscountPer, 150, 255, Col1PersonalDiscountPer, False, False)
                .AddAgTextColumn(Dgl1, Col1PersonalAdditionalDiscountPer, 150, 255, Col1PersonalAdditionalDiscountPer, False, False)
                .AddAgTextColumn(Dgl1, Col1PersonalAdditionPer, 150, 255, Col1PersonalAdditionPer, False, False)
                .AddAgTextColumn(Dgl1, Col1DiscountCalculationPattern, 150, 255, Col1DiscountCalculationPattern, False, False)
                .AddAgTextColumn(Dgl1, Col1AdditionalDiscountCalculationPattern, 150, 255, Col1AdditionalDiscountCalculationPattern, False, False)
                .AddAgTextColumn(Dgl1, Col1AdditionCalculationPattern, 150, 255, Col1AdditionCalculationPattern, False, False)
                .AddAgTextColumn(Dgl1, Col1StockSr, 80, 0, Col1StockSr, False, False)
            End With
            AgL.AddAgDataGrid(Dgl1, Pnl1)
            Dgl1.EnableHeadersVisualStyles = False
            Dgl1.ColumnHeadersHeight = 40
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
            AgL.GridDesign(Dgl1)
            Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top






            If LblV_Type.Tag <> "" Then
                ApplyUISettings(LblV_Type.Tag)
            Else
                ApplyUISettings(EntryNCat)
            End If


            AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)
            AgCalcGrid1.AgFixedRows = 6
            AgCalcGrid1.AgLineGrid = Dgl1
            AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
            AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
            If AgL.VNull(AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable")) = True Then
                AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
            Else
                AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = -1
            End If
            AgCalcGrid1.AgPostingPartyAc = TxtVendor.AgSelectedValue
            AgCalcGrid1.Anchor = AnchorStyles.Bottom + AnchorStyles.Right

            AgCustomGrid1.Ini_Grid(mSearchCode)
            AgCustomGrid1.SplitGrid = False


            Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
            AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
            Dgl1.AgSkipReadOnlyColumns = True
            Dgl1.AllowUserToOrderColumns = True


        Catch ex As Exception
            MsgBox(ex.Message & "[ Frm_BaseFunction_IniGrid ] " + errRow.ToString)
        End Try

    End Sub

    Private Sub InsertPurchInvoiceDetailHelpValues(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "
                Insert Into PurchInvoiceDetailHelpValues 
                (DocID, Sr,  DefaultDiscountPer, DefaultAdditionalDiscountPer, DefaultAdditionPer, 
                PersonalDiscountPer, PersonalAdditionalDiscountPer, PersonalAdditionPer, 
                DiscountCalculationPattern, AdditionalDiscountCalculationPattern, AdditionCalculationPattern 
                ) 
                Values('" & DocID & "', " & Sr & ", 
                " & Val(Dgl1.Item(Col1DefaultDiscountPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1DefaultAdditionalDiscountPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1DefaultAdditionPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1PersonalAdditionalDiscountPer, LineGridRowIndex).Value) & "),
                " & Val(Dgl1.Item(Col1PersonalAdditionPer, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1DiscountCalculationPattern, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1AdditionalDiscountCalculationPattern, LineGridRowIndex).Value) & ", 
                " & Val(Dgl1.Item(Col1AdditionCalculationPattern, LineGridRowIndex).Value) & " 
               "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub


    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer, J As Integer = 0
        Dim bSelectionQry$ = ""
        Dim bSalesTaxGroupParty As String = ""
        Dim mMultiplyWithMinus As Boolean = False


        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            mMultiplyWithMinus = True
        End If

        If BtnFillPartyDetail.Tag IsNot Nothing Then
            bSalesTaxGroupParty = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowSalesTaxGroup).Value
        End If


        If BtnFillPartyDetail.Tag Is Nothing Then BtnFillPartyDetail.Tag = New FrmPurchPartyDetail


        If Topctrl1.Mode.ToUpper = "EDIT" Then
            mQry = "Delete from Ledger where docId='" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        mQry = " Update PurchInvoice " &
                " Set  " &
                " ManualRefNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                " Agent = " & AgL.Chk_Text(TxtAgent.Tag) & ", " &
                " Vendor = " & AgL.Chk_Text(TxtVendor.Tag) & ", " &
                " BillToParty = " & AgL.Chk_Text(TxtBillToParty.Tag) & ", " &
                " ShipToParty = " & AgL.Chk_Text(TxtShipToParty.Tag) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                " VendorDocNo = " & AgL.Chk_Text(TxtVendorDocNo.Text) & ", " &
                " VendorDocDate = " & AgL.Chk_Date(TxtVendorDocDate.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " &
                " UploadDate = Null, " &
                " Tags = " & AgL.Chk_Text(TxtTags.Text) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & IIf(TxtStructure.Tag = "", "", ", ") &
                " " & AgCalcGrid1.FFooterTableUpdateStr(mMultiplyWithMinus) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        CType(BtnFillPartyDetail.Tag, FrmPurchaseInvoiceParty).FSave(mSearchCode, Conn, Cmd)

        If BtnHeaderDetail.Tag IsNot Nothing Then
            CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).FSave(mSearchCode, Conn, Cmd)
        End If

        'mQry = "Delete From PurchInvoiceDetail Where DocId = '" & SearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)



        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchInvoiceDetail With (NoLock)  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then

                If mMultiplyWithMinus Then
                    Dgl1.Item(Col1Qty, I).Value = -Math.Abs(Val(Dgl1.Item(Col1Qty, I).Value))
                    Dgl1.Item(Col1DocQty, I).Value = -Math.Abs(Val(Dgl1.Item(Col1DocQty, I).Value))
                    Dgl1.Item(Col1Amount, I).Value = -Math.Abs(Val(Dgl1.Item(Col1Amount, I).Value))
                End If



                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1

                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                        " " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                        " " & Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1RejQty, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Pcs, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DealQty, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DiscountPer, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DiscountAmount, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1AdditionalDiscountAmount, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1AdditionPer, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1AdditionAmount, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1SaleRate, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1LRNo, I).Value) & ", " &
                                        " " & AgL.Chk_Date(Dgl1.Item(Col1LRDate, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, I).Value) & ", " &
                                        " " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocID, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceTSr, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceSr, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                        " " & AgL.Chk_Date(Dgl1.Item(Col1ExpiryDate, I).Value) & IIf(TxtStructure.Tag = "", "", ",") & AgCalcGrid1.FLineTableFieldValuesStr(I, mMultiplyWithMinus) & " "
                    Call FUpdateDeal(I, Conn, Cmd)



                    If Dgl1.Item(Col1DocQty, I).Tag IsNot Nothing Then
                        CType(Dgl1.Item(Col1DocQty, I).Tag, FrmPurchaseInvoiceDimension).FSave(mSearchCode, mSr, I, Conn, Cmd, mMultiplyWithMinus)
                    Else
                        mDimensionSrl += 1
                        mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                SubCode, SalesTaxGroupParty, Item, SalesTaxGroupItem,  LotNo, 
                                EType_IR, Qty_Iss, Qty_Rec, Pcs_Iss, Pcs_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                                Values
                                (
                                    '" & mSearchCode & "', " & mSr & ", " & mDimensionSrl & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                                    " & AgL.Chk_Date(TxtV_Date.Text) & ", " & AgL.Chk_Text(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                                    " & AgL.Chk_Text(TxtVendor.Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).ErrorText) & ",
                                    'I', 0," & Val(Dgl1.Item(Col1Qty, I).Value) & ", 0, " & Val(Dgl1.Item(Col1Pcs, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, I).Value) & ",
                                    " & Val(Dgl1.Item(Col1DealQty, I).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, I).Value) & ", " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & ",0,
                                    " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocID, I).Value) & ", " & Val(Dgl1.Item(Col1ReferenceTSr, I).Value) & ", " & Val(Dgl1.Item(Col1ReferenceSr, I).Value) & "
                                )"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    End If
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        'If Dgl1.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                        mQry = "Update PurchInvoiceDetail " &
                                " SET Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                " ProfitMarginPer = " & Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) & ", " &
                                " DocQty = " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                " RejQty = " & Val(Dgl1.Item(Col1RejQty, I).Value) & ", " &
                                " 	FreeQty = " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                " 	Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                " 	Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                " 	Pcs = " & Val(Dgl1.Item(Col1Pcs, I).Value) & ", " &
                                " 	DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, I).Value) & ", " &
                                " 	DocDealQty = " & Val(Dgl1.Item(Col1DealQty, I).Value) & ", " &
                                " 	Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                " 	DiscountPer = " & Val(Dgl1.Item(Col1DiscountPer, I).Value) & ", " &
                                " 	DiscountAmount = " & Val(Dgl1.Item(Col1DiscountAmount, I).Value) & ", " &
                                " 	AdditionalDiscountPer = " & Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) & ", " &
                                " 	AdditionalDiscountAmount = " & Val(Dgl1.Item(Col1AdditionalDiscountAmount, I).Value) & ", " &
                                " 	AdditionPer = " & Val(Dgl1.Item(Col1AdditionPer, I).Value) & ", " &
                                " 	AdditionAmount = " & Val(Dgl1.Item(Col1AdditionAmount, I).Value) & ", " &
                                " 	Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                " 	Sale_Rate = " & Val(Dgl1.Item(Col1SaleRate, I).Value) & ", " &
                                " 	MRP = " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                " 	Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                " 	LRNo = " & AgL.Chk_Text(Dgl1.Item(Col1LRNo, I).Value) & ", " &
                                " 	LRDate = " & AgL.Chk_Date(Dgl1.Item(Col1LRDate, I).Value) & ", " &
                                " 	LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                                " 	ReferenceNo = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, I).Value) & ", " &
                                " 	ReferenceDate = " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, I).Value) & ", " &
                                " 	ReferenceDocID = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocID, I).Value) & ", " &
                                " 	ReferenceTSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceTSr, I).Value) & ", " &
                                " 	ReferenceSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceSr, I).Value) & ", " &
                                " 	BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " &
                                " 	LrBaleCode = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Tag) & ", " &
                                " 	ExpiryDate = " & AgL.Chk_Date(Dgl1.Item(Col1ExpiryDate, I).Value) & ", " &
                                "   UploadDate = Null, " &
                                " 	Deal = " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & IIf(TxtStructure.Tag = "", "", ",") &
                                " " & AgCalcGrid1.FLineTableUpdateStr(I, mMultiplyWithMinus) & " " &
                                "   Where DocId = '" & mSearchCode & "' " &
                                "   And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        If Dgl1.Item(Col1DocQty, I).Tag IsNot Nothing Then
                            CType(Dgl1.Item(Col1DocQty, I).Tag, FrmPurchaseInvoiceDimension).FSave(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd, mMultiplyWithMinus)
                        Else
                            If Dgl1.Item(Col1StockSr, I).Value <> "" Then
                                If Dgl1.Item(Col1StockSr, I).Value.ToString.Contains(",") = 0 Then

                                    mQry = "Update Stock Set
                                    V_Type = " & AgL.Chk_Text(TxtV_Type.Tag) & ", 
                                    V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                                    V_Date = " & AgL.Chk_Date(TxtV_Date.Text) & ", 
                                    V_No = " & AgL.Chk_Text(TxtV_No.Text) & ", 
                                    RecId = " & AgL.Chk_Text(TxtReferenceNo.Text) & ",  
                                    Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                                    Site_Code = " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                                    Subcode = " & AgL.Chk_Text(TxtVendor.Tag) & ", 
                                    SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ", 
                                    Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", 
                                    SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", 
                                    LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).ErrorText) & ",
                                    EType_IR = 'I', 
                                    Qty_Rec = " & Val(Dgl1.Item(Col1Qty, I).Value) & ",
                                    Qty_Iss = 0, 
                                    Pcs_Rec = " & Val(Dgl1.Item(Col1Pcs, I).Value) & ",
                                    Pcs_Iss = 0, 
                                    Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",
                                    UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, I).Value) & ",
                                    DealQty_Iss = " & Val(Dgl1.Item(Col1DealQty, I).Value) & ", 
                                    DealQty_Rec =0,  
                                    DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, I).Value) & ", 
                                    Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", 
                                    Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ",
                                    Landed_Value = 0,
                                    ReferenceDocId = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocID, I).Value) & ", 
                                    ReferenceTSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceTSr, I).Value) & ", 
                                    ReferenceDocIdSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceSr, I).Value) & " 
                                    Where DocId = '" & SearchCode & "' and TSr =" & Dgl1.Item(ColSNo, I).Tag & " And Sr =" & Dgl1.Item(Col1StockSr, I).Value & "
                                    "
                                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                                End If
                            Else
                                mDimensionSrl += 1
                                mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                    SubCode, SalesTaxGroupParty, Item, SalesTaxGroupItem,  LotNo, 
                                    EType_IR, Qty_Iss, Qty_Rec, Pcs_Iss, Pcs_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                    Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                                    Values
                                    (
                                    '" & mSearchCode & "', " & mSr & ", " & mDimensionSrl & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                                    " & AgL.Chk_Date(TxtV_Date.Text) & ", " & AgL.Chk_Text(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                                    " & AgL.Chk_Text(TxtVendor.Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).ErrorText) & ",
                                    'I', 0," & Val(Dgl1.Item(Col1Qty, I).Value) & ", 0," & Val(Dgl1.Item(Col1Pcs, I).Value) & "," & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, I).Value) & ",
                                    " & Val(Dgl1.Item(Col1DealQty, I).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, I).Value) & ", " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & ",0,
                                    " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocID, I).Value) & ", " & Val(Dgl1.Item(Col1ReferenceTSr, I).Value) & ", " & Val(Dgl1.Item(Col1ReferenceSr, I).Value) & "
                                    )"
                                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                            End If

                        End If

                        'End If
                    Else
                        If Dgl1.Item(ColSNo, I).Tag IsNot Nothing Then
                            mQry = "Delete From Stock Where DocId = '" & SearchCode & "' and TSr =" & Dgl1.Item(ColSNo, I).Tag & ""
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            mQry = "Delete From PurchInvoiceDimensionDetail Where DocId = '" & SearchCode & "' and Sr =" & Val(Dgl1.Item(ColSNo, I).Tag) & ""
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            mQry = " Delete From PurchInvoiceDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                End If


                UpdateItemGroupPerson(I, Conn, Cmd)
            End If
        Next

        mQry = "Insert Into PurchInvoiceDetail
                (DocId, Sr, PurchInvoice, PurchInvoiceSr, 
                Item, Specification, 
                BaleNo, LrBaleCode, SalesTaxGroupItem, ProfitMarginPer, DocQty, 
                FreeQty, RejQty, Qty, Unit, Pcs,
                DealUnit, DocDealQty, 
                Rate, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount, AdditionPer, AdditionAmount, 
                Amount, Sale_Rate, MRP, 
                Remark, LRNo, LRDate, LotNo, ReferenceNo, ReferenceDate, ReferenceDocID, ReferenceTSr, ReferenceSr,
                Deal, ExpiryDate " & IIf(TxtStructure.Tag = "", "", ",") & AgCalcGrid1.FLineTableFieldNameStr() & ") "

        mQry = mQry + bSelectionQry
        If bSelectionQry <> "" Then
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
        'Call FPostInStock(Conn, Cmd)

        GenerateAndInsertBarcode(mSearchCode, Conn, Cmd)


        If Topctrl1.Mode.ToUpper = "EDIT" Then
            mQry = "Delete From LedgerHead Where DocID = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        mQry = "INSERT INTO LedgerHead (
                           DocID,
                           V_Type,
                           V_Prefix,
                           V_Date,
                           V_No,
                           Div_Code,
                           Site_Code,
                           ManualRefNo,
                           Subcode,
                           DrCr,
                           UptoDate,
                           Remarks,
                           Status,
                           SalesTaxGroupParty,
                           PlaceOfSupply,
                           PartySalesTaxNo,
                           Structure,
                           CustomFields,
                           PartyDocNo,
                           PartyDocDate,
                           EntryBy,
                           EntryDate,
                           ApproveBy,
                           ApproveDate,
                           MoveToLog,
                           MoveToLogDate,
                           UploadDate
                       )
                       VALUES (
                           '" & SearchCode & "',
                           " & AgL.Chk_Text(TxtV_Type.Tag) & ",
                           " & AgL.Chk_Text(LblPrefix.Text) & ",
                           " & AgL.Chk_Date(TxtV_Date.Text) & ",
                           " & Val(TxtV_No.Text) & ",
                           " & AgL.Chk_Text(TxtDivision.Tag) & ",
                           " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                           " & AgL.Chk_Text(TxtReferenceNo.Tag) & ",
                           " & AgL.Chk_Text(TxtVendor.Tag) & ",
                           'Cr',
                           Null,
                           " & AgL.Chk_Text(TxtRemarks.Text) & ",
                           Null,
                           " & AgL.Chk_Text(CType(BtnFillPartyDetail.Tag, FrmPurchaseInvoiceParty).Dgl1.Item(FrmPurchaseInvoiceParty.Col1Value, FrmPurchaseInvoiceParty.rowSalesTaxGroup).Value) & ",
                           " & AgL.Chk_Text(CType(BtnFillPartyDetail.Tag, FrmPurchaseInvoiceParty).Dgl1.Item(FrmPurchaseInvoiceParty.Col1Value, FrmPurchaseInvoiceParty.rowPlaceOfSupply).Value) & ",
                           " & AgL.Chk_Text(CType(BtnFillPartyDetail.Tag, FrmPurchaseInvoiceParty).Dgl1.Item(FrmPurchaseInvoiceParty.Col1Value, FrmPurchaseInvoiceParty.rowSalesTaxNo).Value) & ",
                           " & AgL.Chk_Text(TxtStructure.Tag) & ",
                           " & AgL.Chk_Text(TxtCustomFields.Tag) & ",
                           " & AgL.Chk_Text(TxtVendorDocNo.Text) & ",
                           " & AgL.Chk_Date(TxtVendorDocDate.Text) & ",
                           " & AgL.Chk_Text(AgL.PubUserName) & ",
                           " & AgL.Chk_Date(AgL.PubLoginDate) & ",
                           Null,
                           Null,
                           " & AgL.Chk_Text(AgL.PubUserName) & ",
                           " & AgL.Chk_Date(AgL.PubLoginDate) & ",
                           Null
                       );
"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If AgL.StrCmp(TxtV_Type.Tag, Ncat.PurchaseInvoice) Then
            If Topctrl1.Mode.ToUpper = "Add" Then
                If AgL.StrCmp(AgL.XNull(AgL.PubDtEnviro.Rows(0)("LrGenerationPattern")), LrGenerationPattern.FromLrEntry) Then
                    If BtnHeaderDetail.Tag IsNot Nothing Then
                        If CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowFill).Tag IsNot Nothing Then
                            mQry = " UPDATE PurchInvoiceDetail Set LrCode = '" & CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowFill).Tag & "'
                                    Where DocId = '" & mSearchCode & "'"
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                ElseIf AgL.StrCmp(AgL.XNull(AgL.PubDtEnviro.Rows(0)("LrGenerationPattern")), LrGenerationPattern.FromPurchaseInvoice) Then
                    FInsertLRDetail(mSearchCode, Conn, Cmd)
                End If
            ElseIf Topctrl1.Mode.ToUpper = "Edit" Then
                If AgL.StrCmp(AgL.XNull(AgL.PubDtEnviro.Rows(0)("LrGenerationPattern")), LrGenerationPattern.FromPurchaseInvoice) Then
                    FUpdateLRDetail(mSearchCode, Conn, Cmd)
                End If
            End If
        End If

        Dim mNarrationParty As String
        Dim mNarration As String
        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            mNarrationParty = TxtV_Type.Text
            mNarration = TxtV_Type.Text & " : " & TxtVendor.Text & ""
        Else
            If TxtVendorDocNo.Text <> "" Then
                mNarrationParty = TxtV_Type.Text & " : " & TxtVendorDocNo.Text & " Dated " & TxtVendorDocDate.Text
                mNarration = TxtV_Type.Text & " : " & TxtVendor.Text & " Invoice No. " & TxtVendorDocNo.Text & " Dated " & TxtVendorDocDate.Text
            Else
                mNarrationParty = TxtV_Type.Text
                mNarration = TxtV_Type.Text & " : " & TxtVendor.Text & ""
            End If
        End If


        Dim bPartyLedgerPostingAc As String = ""
        Dim bLinkedPartyAc As String = ""
        If AgL.StrCmp(AgL.XNull(DtV_TypeSettings.Rows(0)("LedgerPostingPartyAcType")), PurchInvoiceLedgerPostingPartyAcType.Vendor) Then
            bPartyLedgerPostingAc = TxtVendor.AgSelectedValue
            bLinkedPartyAc = TxtBillToParty.AgSelectedValue
        Else
            bPartyLedgerPostingAc = TxtBillToParty.AgSelectedValue
            bLinkedPartyAc = TxtVendor.AgSelectedValue
        End If

        Call ClsFunction.PostStructureLineToAccounts(AgCalcGrid1, mNarrationParty, mNarration, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue,
                                             TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtReferenceNo.Text, bPartyLedgerPostingAc, TxtV_Date.Text, Conn, Cmd,, mMultiplyWithMinus, bLinkedPartyAc)



        Dim DtItem As DataTable
        mQry = "Select I.Code as ItemCode, I.Barcode
                From Item I With (NoLock)  
                Left Join Item IG With (NoLock) On I.ItemGroup = IG.Code
                Where I.Code In (Select Item From PurchInvoiceDetail L With (NoLock) Where DocID ='" & mSearchCode & "') 
                And IG.BarcodeType='" & BarcodeType.Fixed & "' And I.Barcode Is Not Null"
        DtItem = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        For I = 0 To DtItem.Rows.Count - 1
            ClsMain.UpdateCurrentStockAtBarcodeFixed(AgL.XNull(DtItem.Rows(I)("barCode")), AgL.XNull(DtItem.Rows(I)("ItemCode")), TxtSite_Code.Tag, Conn, Cmd)
        Next


        If mFlag_Import = False Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
                AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            End If
        End If
    End Sub
    Private Sub UpdateItemGroupPerson(LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        'We will  record personal discount in ItemGroupPerson table only if we are not providing default discount                
        'If Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) > 0 Then
        If Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) = 0 And Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) <> Val(Dgl1.Item(Col1DefaultDiscountPer, LineGridRowIndex).Value) Then

                If AgL.Dman_Execute("Select Count(*) From ItemGroupPerson With (NoLock) Where ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & "
                    And ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & "
                    And Person = " & AgL.Chk_Text(TxtVendor.Tag) & "", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar = 0 Then


                    mQry = " Insert Into ItemGroupPerson
                            (ItemCategory, ItemGroup, Person, DiscountCalculationPattern, DiscountPer, AdditionalDiscountCalculationPattern, AdditionalDiscountPer, AdditionCalculationPattern, AdditionPer)
                            Values
                            (" & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & ", " & AgL.Chk_Text(TxtVendor.Tag) & ",
                             " & AgL.Chk_Text(Dgl1(Col1DiscountCalculationPattern, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1(Col1AdditionalDiscountCalculationPattern, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1AdditionalDiscountPer, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1(Col1AdditionCalculationPattern, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1AdditionPer, LineGridRowIndex).Value) & "
                            )
                           "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
                'ElseIf Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) > 0 And Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) <> Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) Then
            ElseIf Val(Dgl1.Item(Col1PersonalDiscountPer, LineGridRowIndex).Value) <> Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) Or
                Val(Dgl1.Item(Col1PersonalAdditionalDiscountPer, LineGridRowIndex).Value) <> Val(Dgl1.Item(Col1AdditionalDiscountPer, LineGridRowIndex).Value) Then
                mQry = "
                                Update ItemGroupPerson 
                                Set 
                                DiscountCalculationPattern = " & AgL.Chk_Text(Dgl1(Col1DiscountCalculationPattern, LineGridRowIndex).Value) & ",
                                DiscountPer=" & Val(Dgl1.Item(Col1DiscountPer, LineGridRowIndex).Value) & ",
                                AdditionalDiscountPer=" & Val(Dgl1.Item(Col1AdditionalDiscountPer, LineGridRowIndex).Value) & ",
                                AdditionalDiscountCalculationPattern = " & AgL.Chk_Text(Dgl1(Col1AdditionalDiscountCalculationPattern, LineGridRowIndex).Value) & ",
                                AdditionPer=" & Val(Dgl1.Item(Col1AdditionPer, LineGridRowIndex).Value) & ",
                                AdditionCalculationPattern = " & AgL.Chk_Text(Dgl1(Col1AdditionCalculationPattern, LineGridRowIndex).Value) & "
                                Where ItemCategory=" & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & "
                                And ItemGroup=" & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & "
                                And Person=" & AgL.Chk_Text(TxtVendor.Tag) & "
                               "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        'End If
    End Sub

    Private Sub GenerateAndInsertBarcode(DocID As String, ByRef Conn As Object, ByRef Cmd As Object)
        Dim DtStock As DataTable
        Dim I As Integer
        mQry = "Select * From Stock With (NoLock) Where DocID = '" & DocID & "'"
        DtStock = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        If DtStock.Rows.Count > 0 Then
            For I = 0 To DtStock.Rows.Count - 1
                Dim BarcodeCntForDocIdSr As Integer = 0
                mQry = "Select Ig.BarCodeType, Ig.BarCodePattern From Item I  With (NoLock) LEFT JOIN ItemGroup Ig  With (NoLock) On I.ItemGroup = Ig.Code Where I.Code = '" & AgL.XNull(DtStock.Rows(I)("Item")) & "'"
                Dim DtBarcodeType As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
                If (AgL.XNull(DtBarcodeType.Rows(0)("BarCodePattern")) = AgLibrary.ClsMain.agConstants.BarcodePattern.Auto) Then
                    BarcodeCntForDocIdSr = AgL.Dman_Execute("Select Count(*) From BarCode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & "", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar
                    If BarcodeCntForDocIdSr = 0 Then
                        If AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.UniquePerPcs Then
                            InsertBarCodes(DocID, AgL.VNull(DtStock.Rows(I)("Sr")), AgL.XNull(DtStock.Rows(I)("Item")), AgL.VNull(DtStock.Rows(I)("Qty_Rec")), 1, AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")), Conn, Cmd)
                        ElseIf AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.LotWise Then
                            InsertBarCodes(DocID, AgL.VNull(DtStock.Rows(I)("Sr")), AgL.XNull(DtStock.Rows(I)("Item")), 1, AgL.VNull(DtStock.Rows(I)("Qty_Rec")), AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")), Conn, Cmd)
                        End If
                    Else
                        If AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.UniquePerPcs Then
                            If BarcodeCntForDocIdSr < AgL.VNull(DtStock.Rows(I)("Qty_Rec")) Then
                                InsertBarCodes(DocID, AgL.VNull(DtStock.Rows(I)("Sr")), AgL.XNull(DtStock.Rows(I)("Item")), AgL.VNull(DtStock.Rows(I)("Qty_Rec")) - BarcodeCntForDocIdSr, 1, AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")), Conn, Cmd)
                            ElseIf BarcodeCntForDocIdSr > AgL.VNull(DtStock.Rows(I)("Qty_Rec")) Then
                                mQry = " DELETE From BarcodeSiteDetail Where Code in
                                        (Select Code From Barcode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " 
                                        LIMIT " & BarcodeCntForDocIdSr - AgL.VNull(DtStock.Rows(I)("Qty_Rec")) & ") "
                                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                                mQry = " DELETE From Barcode Where Code in
                                        ( Code From Barcode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " 
                                        LIMIT " & BarcodeCntForDocIdSr - AgL.VNull(DtStock.Rows(I)("Qty_Rec")) & ") "
                                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                            End If
                        ElseIf AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.LotWise Then
                            mQry = "UPDATE Barcode Set Qty = " & AgL.VNull(DtStock.Rows(I)("Qty_Rec")) & " Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                    If (AgL.Dman_Execute("Select Count(*) From BarCode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " 
                                    And Item <> '" & AgL.XNull(DtStock.Rows(I)("Item")) & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar) Then
                        mQry = "UPDATE Barcode Set Item = '" & AgL.XNull(DtStock.Rows(I)("Item")) & "' Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub InsertBarCodes(mDocId As String, mSr As Integer, mItemCode As String, mQty As Integer, mLotQty As Integer, BarcodeType As String, ByRef Conn As Object, ByRef Cmd As Object)
        Dim J As Integer = 0
        Dim mBarcodeCode$ = ""
        Dim mBarcodeDesc$ = ""

        For J = 0 To mQty - 1
            mBarcodeCode = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode  With (NoLock)", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()
            mBarcodeDesc = AgL.Dman_Execute("Select IfNull(Max(CAST(Description as BIGINT)),0) + 1 From BarCode  With (NoLock)", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()
            mQry = " INSERT INTO Barcode (Code, Description, Div_Code, Item, GenDocID, GenSr, Qty, BarcodeType)
                    VALUES (" & AgL.Chk_Text(mBarcodeCode) & ", " & AgL.Chk_Text(mBarcodeDesc) & ", " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(mItemCode) & ",
                    " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & mLotQty & ", " & AgL.Chk_Text(BarcodeType) & ") "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)



            mQry = " INSERT INTO BarcodeSiteDetail (Code,Div_Code, Site_Code, LastTrnDocID,
                    LastTrnSr, LastTrnV_Type, LastTrnManualRefNo,
                    LastTrnSubcode, LastTrnProcess, CurrentGodown, Status, CurrentStock)
                    VALUES (" & AgL.Chk_Text(mBarcodeCode) & ", " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                    " & AgL.Chk_Text(mSearchCode) & ", " & Val(mSr) & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ",
                    " & AgL.Chk_Text(TxtVendor.Tag) & ", " & AgL.Chk_Text(TxtProcess.Tag) & ", Null, 'Receive', " & mLotQty & ") "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Next
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet
        Dim mMultiplyWithMinus As Boolean = False

        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            mMultiplyWithMinus = True
        End If

        mIsEntryLocked = False

        mQry = " Select H.*, Sg.Name AS  VendorDispName, sParty.Name as ShipToPartyName, Sg.Nature, Sg1.Name AS  BillToPartyName,
                 Vt.Category As Voucher_Category, VC.CityName as VendorCityName, VC.State as VendorStateCode, VS.Description as VendorStateName,
                 Agent.Name As AgentName
                 From (Select * From PurchInvoice  With (NoLock) Where DocID='" & SearchCode & "') H 
                 LEFT JOIN viewHelpSubGroup Sg  With (NoLock) ON H.Vendor = Sg.Code 
                 LEFT JOIN City C  With (NoLock) On Sg.CityCode = C.CityCode                   
                 LEFT JOIN viewHelpSubGroup Sg1  With (NoLock) On H.BillToParty = Sg1.Code 
                 LEFT JOIN viewHelpSubGroup sParty  With (NoLock) On H.ShipToParty = sParty.Code 
                 LEFT JOIN City C2  With (NoLock) On Sg1.CityCode = C2.CityCode                   
                 Left Join viewHelpSubgroup Agent  With (NoLock) On H.Agent = Agent.Code                  
                 Left Join City VC  With (NoLock) on H.VendorCity = VC.CityCode
                 Left Join State VS  With (NoLock) on VC.State = VS.Code
                 Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type 
                 "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                'TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag
                AgCalcGrid1.AgVoucherCategory = "PURCH"

                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                IniGrid()

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtVendor.Tag = AgL.XNull(.Rows(0)("Vendor"))
                TxtVendor.Text = AgL.XNull(.Rows(0)("VendorDispName"))
                FGetCurrBal(TxtVendor.Tag)
                'TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                'TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))

                TxtNature.Text = AgL.XNull(.Rows(0)("Nature"))

                TxtBillToParty.Tag = AgL.XNull(.Rows(0)("BillToParty"))
                TxtBillToParty.Text = AgL.XNull(.Rows(0)("BillToPartyName"))

                TxtShipToParty.Tag = AgL.XNull(.Rows(0)("ShipToParty"))
                TxtShipToParty.Text = AgL.XNull(.Rows(0)("ShipToPartyName"))

                TxtVendorDocNo.Text = AgL.XNull(.Rows(0)("VendorDocNo"))
                TxtVendorDocDate.Text = ClsMain.FormatDate(AgL.XNull(.Rows(0)("VendorDocDate")))

                'Topctrl1.tAdd = False

                TxtAgent.Tag = AgL.XNull(.Rows(0)("Agent"))
                TxtAgent.Text = AgL.XNull(.Rows(0)("AgentName"))

                BtnFillPartyDetail.Tag = Nothing
                'Dim FrmObj As New FrmPurchPartyDetail
                'FrmObj.TxtVendorMobile.Text = AgL.XNull(.Rows(0)("VendorMobile"))
                'FrmObj.TxtVendorName.Text = AgL.XNull(.Rows(0)("VendorName"))
                'FrmObj.TxtVendorAdd1.Text = AgL.XNull(.Rows(0)("VendorAddress"))
                'FrmObj.TxtVendorCity.Tag = AgL.XNull(.Rows(0)("VendorCity"))
                'FrmObj.TxtVendorCity.Text = AgL.XNull(.Rows(0)("VendorCityName"))
                'FrmObj.TxtState.Tag = AgL.XNull(.Rows(0)("VendorStateCode"))
                'FrmObj.TxtState.Text = AgL.XNull(.Rows(0)("VendorStateName"))

                'BtnFillPartyDetail.Tag = FrmObj

                'AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtTags.Text = AgL.XNull(.Rows(0)("Tags"))
                BtnHeaderDetail.Tag = Nothing
                ShowPurchInvoiceHeader(False)

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text, mMultiplyWithMinus)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))


                LblTotalQty.Text = "0"
                LblTotalAmount.Text = "0"
                LblTotalDealQty.Text = "0"



                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                Dim strQryPurchaseShipped$ = "Select L.ReferenceDocId, L.ReferenceDocIdSr, Sum(L.Qty) As Qty " &
                                             "FROM SaleInvoiceDetail L  With (NoLock) " &
                                             "Where L.ReferenceDocId = '" & mSearchCode & "' " &
                                             "GROUP BY L.ReferenceDocId, L.ReferenceDocIdSr "

                Dim strQryPurchaseReturn$ = "SELECT L.PurchInvoice, L.PurchInvoiceSr, Sum(L.Qty) AS Qty " &
                         "FROM PurchInvoiceDetail L  With (NoLock) " &
                         "Where L.PurchInvoice = '" & SearchCode & "' And L.PurchInvoice <> L.DocId " &
                         "GROUP BY L.PurchInvoice, L.PurchInvoiceSr  "

                Dim mQryStockSr As String
                If AgL.PubServerName = "" Then
                    mQryStockSr = "Select  group_concat(Sr ,',') from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr"
                Else
                    mQryStockSr = "Select  Convert(Varchar,Sr) + ',' from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr for xml path('')"
                End If


                mQry = "Select L.*, I.Description As ItemDesc, I.ManualCode, I.ItemGroup as ItemGroupCode, IG.Description as ItemGroupName, 
                        I.ItemCategory as ItemCategoryCode, IC.Description as ItemCategoryName,  I.ItemType as ItemTypeCode, IT.Name as ItemTypeName,
                         U.DecimalPlaces as QtyDecimalPlaces, U.showdimensiondetailInPurchase, MU.DecimalPlaces as DealDecimalPlaces,                          
                         HV.*,
                         (Case When IfNull(PurShipped.Qty,0) <> 0 Or IfNull(PurReturn.Qty,0) <> 0 Then 1 Else 0 End) As RowLocked,
                        (" & mQryStockSr & ") as StockSr 
                         From (Select * From PurchInvoiceDetail  With (NoLock) Where DocId = '" & SearchCode & "') As L 
                         Left Join PurchInvoiceDetailHelpValues HV  With (NoLock) On L.DocID = HV.DocId And L.Sr = HV.Sr
                         Left Join Item I  With (NoLock) ON L.Item = I.Code 
                         Left join ItemGroup IG  With (NoLock) on I.ItemGroup = IG.Code
                         Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                         Left Join ItemType IT  With (NoLock) On I.ItemType = IT.Code
                         LEFT JOIN Unit U  With (NoLock) On L.Unit = U.Code 
                         Left Join Unit MU  With (NoLock) ON L.DealUnit = MU.Code                          
                         Left Join(" & strQryPurchaseShipped & ") as PurShipped On L.DocID = PurShipped.ReferenceDocID And L.Sr = PurShipped.ReferenceDocIDSr 
                         Left Join (" & strQryPurchaseReturn & ") as PurReturn On L.DocID = PurReturn.PurchInvoice And L.Sr = PurReturn.PurchInvoiceSr 
                         Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1StockSr, I).Value = AgL.XNull(.Rows(I)("StockSr"))
                            If Dgl1.Item(Col1StockSr, I).Value <> "" Then
                                If Dgl1.Item(Col1StockSr, I).Value.ToString.Substring(Dgl1.Item(Col1StockSr, I).Value.ToString.Length - 1, 1) = "," Then
                                    Dgl1.Item(Col1StockSr, I).Value = Dgl1.Item(Col1StockSr, I).Value.ToString.Substring(0, Dgl1.Item(Col1StockSr, I).Value.ToString.Length - 1)
                                End If
                            End If
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroupCode"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))
                            Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategoryCode"))
                            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryName"))
                            Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(.Rows(I)("ItemTypeCode"))
                            Dgl1.Item(Col1ItemType, I).Value = AgL.XNull(.Rows(I)("ItemTypeName"))

                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1BaleNo, I).Tag = AgL.XNull(.Rows(I)("LrBaleCode"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1ProfitMarginPer, I).Value = AgL.VNull(.Rows(I)("ProfitMarginPer"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("DocQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1FreeQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("FreeQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1RejQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("RejQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))

                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Unit, I).Tag = AgL.XNull(.Rows(I)("ShowDimensionDetailInPurchase"))
                            Dgl1.Item(Col1DealDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DealDecimalPlaces"))
                            'Dgl1.Item(Col1UnitMultiplier, I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealDecimalPlaces")) + 2, "0"))                            
                            Dgl1.Item(Col1DealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                            Dgl1.Item(Col1DealQty, I).Value = Format(AgL.VNull(.Rows(I)("DocDealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Rate")))
                            Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(.Rows(I)("DiscountPer"))
                            Dgl1.Item(Col1DiscountAmount, I).Value = AgL.VNull(.Rows(I)("DiscountAmount"))
                            Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(.Rows(I)("AdditionalDiscountPer"))
                            Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = AgL.VNull(.Rows(I)("AdditionalDiscountAmount"))
                            Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(.Rows(I)("AdditionPer"))
                            Dgl1.Item(Col1AdditionAmount, I).Value = AgL.VNull(.Rows(I)("AdditionAmount"))
                            Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                            Dgl1.Item(Col1SaleRate, I).Value = AgL.VNull(.Rows(I)("Sale_Rate"))
                            Dgl1.Item(Col1MRP, I).Value = AgL.VNull(.Rows(I)("MRP"))
                            Dgl1.Item(Col1ExpiryDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ExpiryDate")))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1LRNo, I).Value = AgL.XNull(.Rows(I)("LRNo"))
                            Dgl1.Item(Col1LRDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("LRDate")))
                            Dgl1.Item(Col1Deal, I).Value = AgL.XNull(.Rows(I)("Deal"))
                            Dgl1.Item(Col1ReferenceNo, I).Value = AgL.XNull(.Rows(I)("ReferenceNo"))
                            Dgl1.Item(Col1ReferenceDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ReferenceDate")))
                            Dgl1.Item(Col1ReferenceDocID, I).Value = AgL.XNull(.Rows(I)("ReferenceDocID"))
                            Dgl1.Item(Col1ReferenceTSr, I).Value = AgL.XNull(.Rows(I)("ReferenceTSr"))
                            Dgl1.Item(Col1ReferenceSr, I).Value = AgL.XNull(.Rows(I)("ReferenceSr"))
                            Dgl1.Item(Col1DefaultDiscountPer, I).Value = AgL.VNull(.Rows(I)("DefaultDiscountPer"))
                            Dgl1.Item(Col1DefaultAdditionalDiscountPer, I).Value = AgL.VNull(.Rows(I)("DefaultAdditionalDiscountPer"))
                            Dgl1.Item(Col1DefaultAdditionPer, I).Value = AgL.VNull(.Rows(I)("DefaultAdditionPer"))
                            Dgl1.Item(Col1PersonalDiscountPer, I).Value = AgL.VNull(.Rows(I)("PersonalDiscountPer"))
                            Dgl1.Item(Col1PersonalAdditionalDiscountPer, I).Value = AgL.VNull(.Rows(I)("PersonalAdditionalDiscountPer"))
                            Dgl1.Item(Col1PersonalAdditionPer, I).Value = AgL.VNull(.Rows(I)("PersonalAdditionPer"))
                            Dgl1.Item(Col1DiscountCalculationPattern, I).Value = AgL.XNull(.Rows(I)("DiscountCalculationPattern"))
                            If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Then
                                Dgl1.Item(Col1DiscountCalculationPattern, I).Value = "Rate Per Qty"
                            End If
                            Dgl1.Item(Col1AdditionalDiscountCalculationPattern, I).Value = AgL.XNull(.Rows(I)("AdditionalDiscountCalculationPattern"))
                            Dgl1.Item(Col1AdditionCalculationPattern, I).Value = AgL.XNull(.Rows(I)("AdditionCalculationPattern"))


                            If Dgl1.Item(Col1Unit, I).Tag Then
                                Dgl1.Item(Col1DocQty, I).Style.ForeColor = Color.Blue
                            End If

                            'If .Rows(I)("RowLocked") > 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked


                            If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1DealUnit, I).Value, Dgl1.Item(Col1DealUnit, 0).Value) Then IsSameDealUnit = False

                            If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                            If intDealDecimalPlaces < Val(Dgl1.Item(Col1DealDecimalPlaces, I).Value) Then intDealDecimalPlaces = Val(Dgl1.Item(Col1DealDecimalPlaces, I).Value)

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                            If .Rows(I)("RowLocked") > 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True : mIsEntryLocked = True



                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I, mMultiplyWithMinus)

                        Next I
                    End If
                End With
                AgCalcGrid1.FMoveRecLineLedgerAc()
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

                'Calculation()
                '-------------------------------------------------------------
            End If
        End With
        SetAttachmentCaption()
        Dgl1.ReadOnly = True
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtVendor.Validating, TxtReferenceNo.Validating, TxtV_Date.Validating, TxtVendorDocDate.Validating, TxtVendorDocNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        If mFlag_Import = True Then Exit Sub
        Dim FrmObj As New FrmPurchPartyDetail
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    If TxtV_Type.Tag = "" Then Exit Sub

                    mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtV_TypeSettings.Rows.Count = 0 Then
                            mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code = '" & TxtSite_Code.Tag & "' "
                            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtV_TypeSettings.Rows.Count = 0 Then
                                mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code is Null "
                                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                If DtV_TypeSettings.Rows.Count = 0 Then
                                    mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type Is Null And Div_Code Is Null And Site_Code is Null "
                                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                End If
                            End If
                        End If
                    End If
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        MsgBox("Voucher Type settings not found")
                        Topctrl1.FButtonClick(14, True)
                        Exit Sub
                    End If

                    TxtStructure.AgSelectedValue = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GcnRead).ExecuteScalar
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    AgCalcGrid1.AgNCat = LblV_Type.Tag

                    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                    IniGrid()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

                Case TxtVendor.Name
                    If TxtVendor.Text <> "" Then
                        If sender.AgDataRow IsNot Nothing Then
                            TxtNature.Text = AgL.XNull(sender.AgDataRow.Cells("Nature").Value)
                        End If

                        If ClsMain.IsPartyBlocked(TxtVendor.Tag, LblV_Type.Tag) Then
                            MsgBox("Party is blocked for " & TxtV_Type.Text & ". Record will not be saved")
                        End If

                        FValidateSalesTaxGroup()

                        If TxtBillToParty.Text = "" Then
                            TxtBillToParty.Tag = TxtVendor.Tag
                            TxtBillToParty.Text = TxtVendor.Text
                        End If

                        If AgL.XNull(TxtVendor.Tag) <> "" Then
                            If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Leaf.ToUpper) Then
                                mQry = "Select Par.Code, Par.Name
                                        From SubGroup Sg
                                        LEFT JOIN ViewHelpSubGroup Par On Sg.Parent = Par.Code
                                        Where Sg.SubCode = '" & TxtVendor.Tag & "'"
                                Dim DtBillToParty As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                If DtBillToParty.Rows.Count > 0 Then
                                    TxtBillToParty.Tag = AgL.XNull(DtBillToParty.Rows(0)("Code"))
                                    TxtBillToParty.Text = AgL.XNull(DtBillToParty.Rows(0)("Name"))
                                End If
                            End If
                        End If


                        BtnFillPartyDetail.Tag = Nothing
                            ShowPurchaseInvoiceParty("", TxtVendor.Tag, TxtNature.Text, True)


                            mQry = "Select H.Agent,Agent.Name as AgentName
                                    From SubgroupSiteDivisionDetail H  With (NoLock)                                   
                                    Left Join viewHelpSubgroup agent  With (NoLock) On H.Agent = Agent.Code                                    
                                    Where Subcode = '" & TxtVendor.Tag & "'"
                            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtTemp.Rows.Count > 0 Then

                                TxtAgent.Tag = AgL.XNull(DtTemp.Rows(0)("Agent"))
                                TxtAgent.Text = AgL.XNull(DtTemp.Rows(0)("AgentName"))
                            End If

                            Dgl1.AgHelpDataSet(Col1ReferenceNo) = Nothing
                            If Val(LblTotalAmount.Text) > 0 Then Calculation()


                            FGetCurrBal(TxtVendor.Tag)

                        End If

                        Case TxtReferenceNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "PurchInvoice",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)

                Case TxtReferenceNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()
                Case TxtVendorDocDate.Name
                    If LblV_Type.Tag = Ncat.PurchaseInvoice Then
                        ShowPurchInvoiceHeader()
                    End If
                Case TxtVendorDocNo.Name
                    If TxtVendorDocNo.Text <> "" Then
                        If mFlag_Import = False Then
                            e.Cancel = Not ClsMain.FCheckDuplicatePartyDocNo("VendorDocNo", "PurchInvoice",
                                TxtV_Type.AgSelectedValue, TxtVendorDocNo.Text, mSearchCode, "Vendor", TxtVendor.Tag)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd

        mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code = '" & TxtSite_Code.Tag & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type Is Null And Div_Code Is Null And Site_Code is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    End If
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type settings not found")
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If



        TxtStructure.AgSelectedValue = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GcnRead).ExecuteScalar
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = LblV_Type.Tag

        mIsEntryLocked = False

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        'Try
        '    TxtGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("DEFAULT_Godown"))
        '    TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" Select Description From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GCn).ExecuteScalar)
        'Catch ex As Exception
        '    MsgBox("Default Godown Is Not Set In Enviro", MsgBoxStyle.Information)
        'End Try


        IniGrid()

        TabControl1.SelectedTab = TP1
        'AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        'TxtVendor.Focus()
        mDimensionSrl = 0
        Dgl1.ReadOnly = False


    End Sub

    Private Sub Dgl1_EditingControl_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.EditingControl_LostFocus
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Rate
                    Calculation()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    'Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim DtTemp As DataTable = Nothing
    '    Try
    '        If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
    '            Dgl1.Item(Col1Unit, mRow).Value = ""
    '            Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
    '            Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
    '            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = ""
    '            Dgl1.Item(Col1Rate, mRow).Value = ""
    '            Dgl1.Item(Col1DocQty, mRow).Value = ""
    '        Else
    '            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
    '                DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
    '                Call FSetColumnDecimalPlace(Dgl1.AgSelectedValue(Col1Item, mRow), mRow)
    '                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
    '                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
    '                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("MeasurePerPcs"))
    '                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DrTemp(0)("Rate"))
    '                Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow) = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
    '                If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
    '                    Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow) = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
    '                End If

    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " On Validating_Item Function ")
    '    End Try
    'End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex, DrTemp)
                    Call FGetUnitMultiplier(mRowIndex)

                    'If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
                    FShowTransactionHistory(Dgl1.Item(Col1Item, mRowIndex).Tag)
                    'End If

                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex, DrTemp)
                    Call FGetUnitMultiplier(mRowIndex)

                    'If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
                    FShowTransactionHistory(Dgl1.Item(Col1Item, mRowIndex).Tag)
                    'End If

                Case Col1LRNo
                    If Dgl1.Item(Col1LRNo, mRowIndex).Value <> "" Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = mRowIndex To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item, I).Value <> "" Then
                                    Dgl1.Item(Col1LRNo, I).Value = Dgl1.Item(Col1LRNo, mRowIndex).Value
                                    Dgl1.Item(Col1LRNo, I).Value = Dgl1.Item(Col1LRNo, mRowIndex).Value
                                End If
                            Next
                        End If
                    End If
                Case Col1ItemCategory
                    Validating_ItemCategory(mColumnIndex, mRowIndex)
                Case Col1ItemGroup
                    Validating_ItemGroup(mColumnIndex, mRowIndex)
                Case Col1Rate
                    FSetSalesTaxGroupItemBasedOnRate(mRowIndex)
                Case Col1ReferenceNo
                    If Dgl1.Item(Col1ReferenceNo, mRowIndex).Tag <> "" Then
                        Dgl1.Item(Col1ReferenceDocID, mRowIndex).Value = Dgl1.Item(Col1ReferenceNo, mRowIndex).Tag
                        Dgl1.Item(Col1ReferenceDate, mRowIndex).Value = ClsMain.FormatDate(AgL.Dman_Execute("Select IfNull(VendorDocDate,V_Date) From PurchInvoice Where DocID = '" & Dgl1.Item(Col1ReferenceNo, mRowIndex).Tag & "'", AgL.GCn).executescalar())
                    End If
            End Select
            Call Calculation()
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        'sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        'sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1

    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0
        LblTotalDealQty.Text = 0

        LblTotalAmount.Text = 0

        Dim DEALARR() As String = Nothing
        Dim DEALRATE As Double

        Dim MRATE As Double = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible Then

                Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value) - Val(Dgl1.Item(Col1RejQty, I).Value) + Val(Dgl1.Item(Col1FreeQty, I).Value)



                DEALRATE = 0
                If Dgl1.Item(Col1Deal, I).Value <> "" Then
                    DEALARR = Split(Dgl1.Item(Col1Deal, I).Value.ToString, "+", 2)
                    If DEALARR.Length = 2 Then
                        DEALRATE = Format((Val(Dgl1.Item(Col1Rate, I).Value) * Val(DEALARR(0))) / (Val(DEALARR(0)) + Val(DEALARR(1))), "0.00")
                    End If
                End If


                If DEALRATE <> 0 Then
                    MRATE = DEALRATE
                Else
                    MRATE = Val(Dgl1.Item(Col1Rate, I).Value)
                End If


                'If In Item Master Unit Multiplier Is Defined then this calculation will be executed.
                'For Example In Carpet Area Per Pcs Is Defined in Item Master and Total Area will be calculated
                'with that Area per pcs. 
                If Val(Dgl1.Item(Col1UnitMultiplier, I).Value) <> 0 Then
                    Dgl1.Item(Col1DealQty, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1UnitMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DealDecimalPlaces, I).Value) + 2, "0"))
                End If

                'If in item master Pcs Per Measure is defined this calculation will be executed.
                'for example in case of soap user will feed how many cartons he purchased in the measure field and
                'qty will be calculated on the basis of the pcs per measure.
                If Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) <> 0 Then
                    Dgl1.Item(Col1DocQty, I).Value = Format(Val(Dgl1.Item(Col1DealQty, I).Value) * Val(Dgl1.Item(Col1PcsPerMeasure, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) + 2, "0"))
                End If

                'if the qty unit and mesure units are equal then qty will auto come in mesure fields
                'for example yarn's unit and measure unit is Kg
                'In this case same figure will be copied in the measure.
                If AgL.StrCmp(Dgl1.Item(Col1DealUnit, I).Value, Dgl1.Item(Col1Unit, I).Value) Then
                    Dgl1.Item(Col1DealQty, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DealDecimalPlaces, I).Value) + 2, "0"))
                End If




                'If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Doc Measure") Then
                'Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'Else
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'End If

                If Val(Dgl1.Item(Col1DiscountPer, I).Value) > 0 Then
                    If Dgl1(Col1DiscountCalculationPattern, I).Value.ToUpper = DiscountCalculationPattern.RatePerQty.ToUpper Then
                        Dgl1.Item(Col1DiscountAmount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1DiscountPer, I).Value), "0.00")
                    Else
                        Dgl1.Item(Col1DiscountAmount, I).Value = Format(Val(Dgl1.Item(Col1Amount, I).Value) * Val(Dgl1.Item(Col1DiscountPer, I).Value) / 100, "0.00")
                    End If
                End If


                If Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) > 0 Then
                    If AgL.XNull(Dgl1(Col1AdditionalDiscountCalculationPattern, I).Value).ToUpper = DiscountCalculationPattern.RatePerQty.ToUpper Then
                        Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value), "0.00")
                    ElseIf agl.XNull(Dgl1(Col1AdditionalDiscountCalculationPattern, I).Value).ToUpper = DiscountCalculationPattern.Percentage.ToUpper Then
                        Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = Format(Val(Dgl1.Item(Col1Amount, I).Value) * Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) / 100, "0.00")
                    Else
                        Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = Format((Val(Dgl1.Item(Col1Amount, I).Value) - Val(Dgl1.Item(Col1DiscountAmount, I).Value)) * Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) / 100, "0.00")
                    End If
                End If


                If Val(Dgl1.Item(Col1AdditionPer, I).Value) > 0 Then
                    If AgL.XNull(Dgl1(Col1AdditionCalculationPattern, I).Value).ToUpper = DiscountCalculationPattern.RatePerQty.ToUpper Then
                        Dgl1.Item(Col1AdditionAmount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1AdditionPer, I).Value), "0.00")
                    ElseIf agl.XNull(Dgl1(Col1AdditionCalculationPattern, I).Value).ToUpper = DiscountCalculationPattern.Percentage.ToUpper Then
                        Dgl1.Item(Col1AdditionAmount, I).Value = Format(Val(Dgl1.Item(Col1Amount, I).Value) * Val(Dgl1.Item(Col1AdditionPer, I).Value) / 100, "0.00")
                    Else
                        Dgl1.Item(Col1AdditionAmount, I).Value = Format((Val(Dgl1.Item(Col1Amount, I).Value) - Val(Dgl1.Item(Col1DiscountAmount, I).Value) - Val(Dgl1.Item(Col1AdditionalDiscountAmount, I).Value)) * Val(Dgl1.Item(Col1AdditionPer, I).Value) / 100, "0.00")
                    End If
                End If


                Dgl1.Item(Col1Amount, I).Value = Val(Dgl1.Item(Col1Amount, I).Value) - Val(Dgl1.Item(Col1DiscountAmount, I).Value) - Val(Dgl1.Item(Col1AdditionalDiscountAmount, I).Value) + Val(Dgl1.Item(Col1AdditionAmount, I).Value)

                'Footer Calculation
                If AgL.XNull(Dgl1.Item(Col1ItemType, I).Tag) <> ItemTypeCode.ServiceProduct Then
                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                    LblTotalDealQty.Text = Val(LblTotalDealQty.Text) + Val(Dgl1.Item(Col1DealQty, I).Value)
                End If
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next

        If BtnFillPartyDetail.Tag IsNot Nothing Then
            AgCalcGrid1.AgPostingGroupSalesTaxParty = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowSalesTaxGroup).Value
            AgCalcGrid1.AgPlaceOfSupply = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowPlaceOfSupply).Value
        End If

        AgCalcGrid1.AgVoucherCategory = "PURCH"
        AgCalcGrid1.Calculation()

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) > 0 Then
                    Dgl1.Item(Col1SaleRate, I).Value = GetSaleRate(I) 'Format((Val(AgCalcGrid1.AgChargesValue("LV", I, AgStructure.AgCalcGrid.LineColumnType.Amount)) + (Val(AgCalcGrid1.AgChargesValue("LV", I, AgStructure.AgCalcGrid.LineColumnType.Amount)) * Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) / 100)) / Val(Dgl1.Item(Col1Qty, I).Value), "0.00")
                End If
            End If
        Next I


        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalDealQty.Text = Val(LblTotalDealQty.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If mFlag_Import = True Then Exit Sub
        Dim I As Integer = 0
        Dim CheckDuplicateRef As Boolean


        Dgl1.EndEdit()

        If AgL.RequiredField(TxtVendor, LblVendor.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtBillToParty, LblPostToAc.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If ClsMain.IsPartyBlocked(TxtVendor.Tag, LblV_Type.Tag) Then
            MsgBox("Party is blocked for " & TxtV_Type.Text & ". Can not continue.")
            passed = False : Exit Sub
        End If

        If FValidateSalesTaxGroup() = False Then
            passed = False : Exit Sub
        End If


        'If mFlag_Import = False Then
        '    If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1Specification).Index.ToString + "," + Dgl1.Columns(Col1LotNo).Index.ToString + "," + Dgl1.Columns(Col1BaleNo).Index.ToString + "," & Dgl1.Columns(Col1Dimension1).Index & "," & Dgl1.Columns(Col1Dimension2).Index & "") = True Then passed = False : Exit Sub
        'End If



        With Dgl1
            For I = 0 To .Rows.Count - 1

                If Val(Dgl1(ColSNo, I).Tag) > 0 Then
                    If Dgl1(Col1Item, I).Value = "" Then
                        MsgBox("Item is blank at Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If

                If .Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1DocQty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    'If Val(.Item(Col1Rate, I).Value) = 0 Then
                    '    MsgBox("Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                    '    .CurrentCell = .Item(Col1Rate, I) : Dgl1.Focus()
                    '    passed = False : Exit Sub
                    'End If

                    If LblV_Type.Tag = Ncat.PurchaseReturn Then
                        If AgL.XNull(Dgl1.Item(Col1ReferenceNo, I).Value) = "" Or AgL.XNull(Dgl1.Item(Col1ReferenceNo, I).Value) = TxtVendorDocNo.Text Then
                            Dgl1.Item(Col1ReferenceNo, I).Value = TxtVendorDocNo.Text
                            Dgl1.Item(Col1ReferenceDate, I).Value = TxtVendorDocDate.Text
                        End If
                    End If



                    If LblV_Type.Tag = Ncat.PurchaseReturn Then
                        If .Item(Col1ReferenceNo, I).Value = "" Then
                            MsgBox("Against Invoice No. is blank for some rows")
                            TxtVendorDocNo.Focus()
                            passed = False : Exit Sub

                            'MsgBox("Against Invoice No. is blank  at Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            '.CurrentCell = .Item(Col1ReferenceNo, I) : Dgl1.Focus()
                            'passed = False : Exit Sub
                        End If


                        If .Item(Col1ReferenceDate, I).Value = "" Then
                            If TxtVendorDocNo.Text = "" Then
                                MsgBox("Against Inv. Date is blank  at Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                .CurrentCell = .Item(Col1ReferenceNo, I) : Dgl1.Focus()
                            Else
                                MsgBox("Against Inv. Date is blank. Can not continue")
                                TxtVendorDocDate.Focus()
                            End If
                            passed = False : Exit Sub
                            End If

                        End If
                End If

                If BtnHeaderDetail.Tag IsNot Nothing Then
                    If CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowLrNo).Value <> "" Then
                        If Dgl1.Item(Col1LRNo, I).Value = "" Or Dgl1.Columns(Col1LRNo).Visible = False Then
                            Dgl1.Item(Col1LRNo, I).Value = CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowLrNo).Value
                            Dgl1.Item(Col1LRDate, I).Value = CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowLrDate).Value
                        End If

                        If Dgl1.Item(Col1BaleNo, I).Value = "" Or Dgl1.Columns(Col1BaleNo).Visible = False Then
                            Dgl1.Item(Col1BaleNo, I).Value = CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowLrNo).Value
                        End If
                    End If
                End If
            Next
        End With


        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "PurchInvoice",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)
        If Not CheckDuplicateRef Then
            TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef

        If TxtVendorDocNo.Text <> "" Then
            If mFlag_Import = False Then
                passed = ClsMain.FCheckDuplicatePartyDocNo("VendorDocNo", "PurchInvoice",
                TxtV_Type.AgSelectedValue, TxtVendorDocNo.Text, mSearchCode, "Vendor", TxtVendor.Tag)
            End If
        End If

        If BtnHeaderDetail.Tag IsNot Nothing Then
            If CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowLrNo).Value <> "" Then
                mQry = "Select Count(*) From PurchInvoiceTransport 
                    Where LrNo = '" & CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowLrNo).Value & "'
                    And Transporter = '" & CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowTransporter).Tag & "'
                    And DocId <> '" & mSearchCode & "'"
                If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) > 0 Then
                    MsgBox("LR No " & CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowLrNo).Value & " is alredy entered for " & CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowTransporter).Value, MsgBoxStyle.Information)
                    passed = False
                    Exit Sub
                End If
            End If
        End If



        If Math.Round(Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)), 0) <> Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) Then
            Calculation()
            Calculation()
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        BtnHeaderDetail.Tag = Nothing

        mFullItemListInHelp = False
        UserMovedOverItemGroup = False
        UserMovedOverItemCategory = False
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If
        If mFlag_Import = True Then Exit Sub
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty, Col1RejQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                Case Col1DocQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                    If Dgl1.CurrentCell.Tag IsNot Nothing Then Dgl1.CurrentCell.ReadOnly = True

                Case Col1UnitMultiplier, Col1DealQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1DealDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1Rate
                    If Topctrl1.Mode = "Edit" Then Dgl1.CurrentCell.ReadOnly = False
                Case Col1ItemCategory
                    UserMovedOverItemCategory = True
                    Try
                        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
                            If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value IsNot Nothing Then
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Tag
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value

                                If mFullItemListInHelp = True Then
                                    Dgl1.AgHelpDataSet(Col1Item) = Nothing
                                End If
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

                                If mFullItemListInHelp = True Then
                                    Dgl1.AgHelpDataSet(Col1Item) = Nothing
                                End If
                            End If
                        End If
                    Catch ex As Exception
                    End Try

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempPurchInvoice_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        Dim I As Integer

        For I = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(I).DefaultCellStyle.BackColor = Dgl1.AgReadOnlyColumnColor Then
                Dgl1.Columns(I).ReadOnly = True
            End If
        Next



    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell IsNot Nothing Then
            If e.Control And e.KeyCode = Keys.D And Dgl1.Rows(Dgl1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                sender.CurrentRow.Visible = False
                Calculation()
            End If
        End If

        If e.KeyCode = Keys.Enter Then
            If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1DocQty Then
                If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value = "" And Val(Dgl1.Item(Col1DocQty, Dgl1.CurrentCell.RowIndex).Value) = 0 Then
                    TxtRemarks.Focus()
                End If
            End If
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                If sender.Rows(sender.currentcell.rowindex).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked Then
                    MsgBox("Locked Row is not allowed to select.")
                    e.Handled = True
                Else
                    sender.Rows(sender.currentcell.rowindex).Visible = False
                    Calculation()
                    e.Handled = True
                End If
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If Dgl1.CurrentCell IsNot Nothing Then
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    End If
                Case Col1DocQty
                    If e.KeyCode = Keys.Space Then ShowPurchInvoiceDimensionDetail(Dgl1.CurrentCell.RowIndex)

            End Select
        End If
        'If e.KeyCode = Keys.Enter Then
        '    If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Item Then
        '        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value Is Nothing Then Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = ""
        '        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
        '            AgCalcGrid1.Focus()
        '        End If
        '    End If
        'End If


        'Call FOpenMaster(e)
    End Sub

    Public Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String,
                                       ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2
        StrContraName = AgL.XNull(AgL.Dman_Execute("Select Name from Subgroup  With (NoLock)  Where SubCode = '" & StrContraName & "'  ", AgL.GcnRead).ExecuteScalar)

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub


    'Private Function FGetRelationalData() As Boolean
    '    Try
    '        Dim bRData As String
    '        '// Check for relational data in Purchase Return
    '        mQry = " DECLARE @Temp NVARCHAR(Max); "
    '        mQry += " SET @Temp=''; "
    '        mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo From PurchInvoiceDetail  L  With (NoLock) LEFT JOIN PurchInvoice H  With (NoLock) ON L.DocId = H.DocID WHERE L.ReferenceDocID  = '" & TxtDocId.Text & "' ) AS X  "
    '        mQry += " SELECT @Temp as RelationalData "
    '        bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    '        If bRData.Trim <> "" Then
    '            MsgBox(" Purchase Return " & bRData & " created against Invoice No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
    '            FGetRelationalData = True
    '            Exit Function
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
    '        FGetRelationalData = True
    '    End Try
    'End Function
    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Dim DtTemp As DataTable

        Dim DtRelationalData As DataTable
        mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From Cloth_SupplierSettlementInvoices L
                        LEFT JOIN LedgerHead H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PurchaseInvoiceDocId = '" & mSearchCode & "' "
        DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtRelationalData.Rows.Count > 0 Then
            MsgBox("Data Exists For " & TxtV_Type.Text + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Edit Entry", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If

        If mIsEntryLocked Then
            If AgL.PubUserName.ToUpper = "SA" Or AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName Then
                If MsgBox("Referential data exist. Do you want to modify record?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Passed = False
                    Exit Sub
                Else
                    TxtVendor.Enabled = False
                End If
            Else
                MsgBox("Referential data exist. Can't modify record.")
                Passed = False
                Exit Sub
            End If
        End If
        ShowPurchaseInvoiceParty(mSearchCode, "", TxtNature.Text)

        If ClsMain.IsEntryLockedWithLockText("PurchInvoice", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        mQry = "Select IfNull(Max(Sr),0) From PurchInvoiceDimensionDetail  With (NoLock) Where DocID ='" & mSearchCode & "' "
        mDimensionSrl = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar
        mQry = "Select IfNull(Max(Sr),0) From Stock  With (NoLock) Where DocID ='" & mSearchCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If AgL.VNull(DtTemp.Rows(0)(0)) > mDimensionSrl Then
            mDimensionSrl = AgL.VNull(DtTemp.Rows(0)(0))
        End If


        mQry = " SELECT Count(*) AS Cnt FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "' 
                    AND IfNull(T.IsEditingAllowed,0) = 0 "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Refrential Entries Exist For This Entry.Can't Modify.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If

        Dgl1.ReadOnly = False
    End Sub

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Dim mQry As String
        If mIsEntryLocked Then
            MsgBox("Referential data exist. Can't delete record.")
            Passed = False
        End If

        Passed = Not FGetRelationalData()

        mQry = "Select Count(*) 
                From Barcode H With (NoLock) 
                Left Join BarcodeSiteDetail L With (NoLock) On H.Code = L.Code
                Where H.GenDocID <> L.LastTrnDocID and H.GenDocID = '" & mSearchCode & "'
               "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Barcodes are in transaction. Can not continue.")
            Passed = False
        End If
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True

        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM PurchInvoice  With (NoLock) WHERE ManualRefNo = '" & TxtReferenceNo.Text & "'   " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'   "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtReferenceNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM PurchInvoice  With (NoLock) WHERE ManualRefNo = '" & TxtReferenceNo.Text & "'  " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  AND DocID <>'" & mSearchCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtReferenceNo.Focus()
        End If

        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM PurchInvoice  With (NoLock) WHERE VendorDocNo = '" & TxtVendorDocNo.Text & "' And Vendor = '" & TxtVendor.AgSelectedValue & "'  " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'   "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Vendor Doc. No. Already Exists") : TxtReferenceNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM PurchInvoice  With (NoLock) WHERE VendorDocNo = '" & TxtVendorDocNo.Text & "'  And Vendor = '" & TxtVendor.AgSelectedValue & "'  " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  AND DocID <>'" & mSearchCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Vendor Doc No. Already Exists") : TxtReferenceNo.Focus()
        End If
    End Function

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        AgCustomGrid1.FrmType = Me.FrmType

        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuImportFromDos.Visible = False
            MnuImportFromExcel.Visible = False
            MnuImportFromTally.Visible = False
            MnuEditSave.Visible = False
        End If

        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            MnuGenerateEWayBill.Visible = True
        Else
            MnuGenerateEWayBill.Visible = False
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub




    'Private Sub FPostInStock(ByVal Conn As Object, ByVal Cmd As Object)
    '    Dim I As Integer = 0, Cnt As Integer = 0
    '    Dim bSelectionQry$ = ""

    '    mQry = " Delete From Stock Where DocId = '" & mSearchCode & "' "
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '    mQry = " INSERT INTO  Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code,  
    '             SubCode,  SalesTaxGroupParty, Structure, Item,  
    '             Godown,EType_IR, Qty_Iss, Qty_Rec, Unit, LotNo, DealQty_Iss, DealQty_Rec, DealUnit, 
    '             Rate, Amount, Remarks, RecId, ReferenceDocId, ReferenceDocIdSr, ExpiryDate, Sale_Rate, MRP, Process) 
    '             Select L.DocId, L.Sr, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, 
    '             H.Vendor, H.SalesTaxGroupParty, H.Structure, L.Item, L.Godown,'R', 0, L.Qty, 
    '             L.Unit, L.LotNo,0, L.DealQty, L.DealUnit, L.Rate, L.Amount, 
    '             L.Remark, H.ManualRefNo, L.DocId, L.Sr, L.ExpiryDate, L.Sale_Rate, L.MRP, Process 
    '             FROM PurchInvoiceDetail L  
    '             LEFT JOIN PurchInvoice H On L.DocId = H.DocId 
    '             Where L.DocId = '" & mSearchCode & "' "
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    'End Sub

    Private Sub FrmPurchInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        If mFlag_Import = True Then Exit Sub
        Try
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
        End Try
        Try
            If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
        Catch ex As Exception
        End Try
        Try
            If Dgl1.AgHelpDataSet(Col1BaleNo) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1BaleNo).Dispose() : Dgl1.AgHelpDataSet(Col1BaleNo) = Nothing
        Catch ex As Exception
        End Try


        Try
            If BtnHeaderDetail.Tag IsNot Nothing Then
                For I = 0 To CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Rows.Count - 1
                    CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Head, I).Tag = Nothing
                Next
            End If
        Catch ex As Exception
        End Try


        If TxtVendor.AgHelpDataSet IsNot Nothing Then TxtVendor.AgHelpDataSet.Dispose() : TxtVendor.AgHelpDataSet = Nothing
        If TxtAgent.AgHelpDataSet IsNot Nothing Then TxtAgent.AgHelpDataSet.Dispose() : TxtAgent.AgHelpDataSet = Nothing

    End Sub

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, TxtSite_Code.Tag, "PURCH", LblV_Type.Tag, TxtV_Type.Tag, "", "")
        FGetSettings = mValue
    End Function

    Private Sub BtnFillPartyDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPartyDetail.Click
        If Topctrl1.Mode = "Add" Then
            ShowPurchaseInvoiceParty("", TxtVendor.Tag, TxtNature.Text)
        Else
            ShowPurchaseInvoiceParty(mSearchCode, "", TxtNature.Text)
        End If
    End Sub

    'Private Sub FOpenPartyDetail()
    '    Dim FrmObj As FrmPurchPartyDetail
    '    Try
    '        If BtnFillPartyDetail.Tag Is Nothing Then
    '            FrmObj = New FrmPurchPartyDetail
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
    Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor,
                         Optional ByVal IsPrintToPrinter As Boolean = False)
        'For SSRS Print Out
        Dim DtTemp As DataTable

        mQry = "SELECT H.DocID  FROM PurchInvoice H With (NoLock)
                LEFT JOIN PurchInvoiceDetail L With (NoLock) ON H.DocID = L.DocID 
                WHERE H.DocID ='" & SearchCode & "' And H.Gross_Amount > 0
                GROUP BY H.DocID 
                HAVING Round(Sum(L.Amount),2)<>Round(Max(H.Gross_Amount),2)"
        If AgL.FillData(mQry, AgL.GCn).Tables(0).Rows.Count > 0 Then
            MsgBox("Something went wrong with gross amount. Can not print Invoice. Please check once.")
            Exit Sub
        End If

        mQry = "SELECT H.DocID, H.Sr, I.Description as ItemName, Round(Sum(L.TotalQty),2),Round(Max(H.Qty),2)  
                FROM PurchInvoiceDetail H With (NoLock)
                LEFT JOIN PurchInvoiceDimensionDetail L With (NoLock) ON H.DocID = L.DocID And H.Sr = L.TSr
                Left Join Item I With (NoLock) On H.Item = I.Code
                WHERE H.DocID ='" & SearchCode & "' 
                GROUP BY H.DocID, H.Sr, I.Description 
                HAVING abs(Round(Sum(L.TotalQty),2))<>abs(Round(Max(H.Qty),2))"

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            MsgBox("Something went wrong with dimension detail at item " & AgL.XNull(DtTemp.Rows(0)("ItemName")) & ". Can not print Invoice. Please check once.")
            Exit Sub
        End If

        'FGetPrintSSRS(mPrintFor)
        FGetPrintCrystal(SearchCode, mPrintFor, IsPrintToPrinter)
        'If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Then
        '    FGetPrintCrystal_Aadhat(SearchCode, mPrintFor)
        'Else
        '    FGetPrintCrystal(SearchCode, mPrintFor)
        'End If
    End Sub
    Sub FGetPrintCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False)
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer

        mPrintTitle = TxtV_Type.Text

        Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        Dim mDocReportFileName As String = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)


        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            If AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
                mPrintTitle = TxtV_Type.Text & " (Debit Note)"
            End If
        ElseIf LblV_Type.Tag = Ncat.PurchaseInvoice Then
            mDocNoCaption = "Invoice No."
            mDocDateCaption = "Invoice Date"
        End If

        PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")
        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "


            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, H.DocID, SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as DocNo,  Agent.DispName as AgentName, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption,
                H.VendorName as PartyName, IfNull(H.VendorAddress,'') as PartyAddress, IfNull(H.VendorPinCode,'') as PartyPinCode, IfNull(C.CityName,'') as PartyCityName, IfNull(State.ManualCode,'') as PartyStateCode, IfNull(State.Description,'') as PartyStateName, 
                IfNull(H.VendorMobile,'') as PartyMobile, IfNull(Sg.ContactPerson,'') as ContactPerson, IfNull(H.VendorSalesTaxNo,'') as PartySalesTaxNo, IfNull((Select RegistrationNo From SubgroupRegistration With (NoLock) Where RegistrationType='" & SubgroupRegistrationType.AadharNo & "' And Subcode=H.Vendor),'') as PartyAadharNo,
                (Select RegistrationNo From SubgroupRegistration  With (NoLock) Where RegistrationType='" & SubgroupRegistrationType.PanNo & "' And Subcode=H.Vendor) as PartyPanNo,
                (Case When BP.Nature = 'Cash' Then IfNull(SP.DispName, BP.DispName || ' - ' || IsNull(H.VendorName,'')) Else IfNull(SP.DispName,H.VendorName) End) as ShipToPartyName,
                (Case When SP.DispName Is Null Then IfNull(H.VendorAddress,'') Else IfNull(Sp.Address,'') End) as ShipToPartyAddress, 
                (Case When SP.DispName Is Null Then IfNull(C.CityName,'') Else IfNull(SC.CityName,'') End) as ShipToPartyCity, 
                (Case When SP.DispName Is Null Then IfNull(H.VendorPinCode,'') Else IfNull(Sp.Pin,'') End) as ShipToPartyPincode, 
                (Case When SP.DispName Is Null Then IfNull(State.ManualCode,'') Else IfNull(SS.ManualCode,'') End) as ShipToPartyStateCode, 
                (Case When SP.DispName Is Null Then IfNull(State.Description,'') Else IfNull(SS.Description,'') End) as ShipToPartyStateName, 
                (Case When SP.DispName Is Null Then IfNull(H.VendorMobile,'') Else IfNull(Sp.Mobile,'') End) as ShipToPartyMobile, 
                (Case When SP.DispName Is Null Then IfNull(H.VendorSalesTaxNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'),'') End) as ShipToPartySalesTaxNo, 
                (Case When SP.DispName Is Null Then IfNull(H.VendorAadharNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.AadharNo & "'),'') End) as ShipToPartyAadharNo, 
                (Case When SP.DispName Is Null Then IfNull(H.VendorPanNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.PanNo & "'),'') End) as ShipToPartyPanNo, 
                IfNull(Transporter.Name,IfNull(MTransporter.Name,'')) as TransporterName, IfNull(TD.LrNo,'') LrNo, TD.LrDate, IfNull(TD.PrivateMark,'') PrivateMark, TD.Weight, TD.Freight, IfNull(TD.PaymentType,'') as FreightType, IfNull(TD.RoadPermitNo,'') RoadPermitNo, TD.RoadPermitDate, IfNull(IfNull(H.VendorDocNo,L.ReferenceNo),'') as ReferenceNo,
                I.Description as ItemName, IG.Description as ItemGroupName, IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, IfNull(I.HSN, IC.HSN) as HSN,
                L.SalesTaxGroupItem, STGI.GrossTaxRate, 
                (Case when IfNull(I.MaintainStockYn,1) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then L.Pcs Else 0 End) as Pcs, 
                (Case when IfNull(I.MaintainStockYn,1) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then abs(L.Qty) Else 0 End) as Qty, 
                (Case when IfNull(I.MaintainStockYn,1) =1 AND I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then L.Rate Else 0 End) as Rate, 
                L.Unit, U.DecimalPlaces as UnitDecimalPlaces, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, 
                L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as TotalDiscount, 
                abs(L.Amount)+L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as AmountBeforeDiscount,
                Abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) Tax1_Per, Abs(L.Tax1) as Tax1, Abs(L.Tax2_Per) as Tax2_Per, Abs(L.Tax2) as Tax2, Abs(L.Tax3_Per) as Tax3_Per, Abs(L.Tax3) as Tax3, Abs(L.Tax4_Per) as Tax4_Per, Abs(L.Tax4) as Tax4, Abs(L.Tax5_Per) as Tax5_Per, Abs(L.Tax5) as Tax5, Abs(L.Net_Amount) as Net_Amount,
                IfNull(H.Remarks,'') as HRemarks, IfNull(L.Remark,'') as LRemarks,
                abs(H.Gross_Amount) as H_Gross_Amount, H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount,abs(H.Taxable_Amount) as H_Taxable_Amount,abs(H.Tax1_Per) as H_Tax1_Per, abs(H.Tax1) as H_Tax1, 
                abs(H.Tax2_Per) as H_Tax2_Per, abs(H.Tax2) as H_Tax2, abs(H.Tax3_Per) as H_Tax3_Per, abs(H.Tax3) as H_Tax3, abs(H.Tax4_Per) as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                abs(H.Tax5_Per) as H_Tax5_Per, abs(H.Tax5) as H_Tax5, abs(H.Deduction_Per) as H_Deduction_Per, abs(H.Deduction) as H_Deduction, abs(H.Other_Charge_Per) as H_Other_Charge_Per, abs(H.Other_Charge) as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, 
                (Select Sum(L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From PurchInvoiceDetail L1 Where L1.DocID = H.DocID) as H_TotalDiscount, 
                (Select Sum(abs(L1.Amount)+L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From PurchInvoiceDetail L1 Where L1.DocID = H.DocId) as H_AmountBeforeDiscount,
                '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                L.DimensionDetail as DimDetail, '' as HsnDescription, '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
                from PurchInvoice H   With (NoLock)              
                Left Join PurchInvoiceDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Item I  With (NoLock) On L.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join Item IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join Item IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.VendorCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join PurchInvoiceTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.Vendor = Sg.Subcode    
                Left Join Subgroup BP With (NoLock) On H.BillToParty = BP.Subcode            
                Left Join Subgroup SP With (NoLock) On H.ShipToParty = SP.Subcode
                Left Join City SC With (NoLock) On SP.CityCode = SC.CityCode
                Left Join State SS with (NoLock) On SC.State = SS.Code
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                Left Join SubgroupSiteDivisionDetail SSD On H.Vendor = SSD.Subcode And H.Div_Code = SSD.Div_Code And H.Site_Code = SSD.Site_Code
                Left Join ViewHelpSubgroup MTransporter  With (NoLock) On SSD.Transporter= MTransporter.Code
                Where H.DocID = '" & mSearchCode & "'
                "






        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "


        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
                    From SaleInvoice H  With (NoLock)
                    LEFT JOIN SubGroup Sg  With (NoLock) On H.SaleToParty = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
                    From SaleInvoice H  With (NoLock)
                    LEFT JOIN SubGroup Sg  With (NoLock) On H.Agent = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            objRepPrint.AttachmentName = "Invoice"
        Else
            objRepPrint = New AgLibrary.RepView(AgL)
        End If


        If mDocReportFileName = "" Then
            ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, "PurchInvoice_Print.rpt", mPrintTitle, , , , TxtVendor.Tag, TxtV_Date.Text, IsPrintToPrinter)
        Else
            ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, mDocReportFileName, mPrintTitle, , , , TxtVendor.Tag, TxtV_Date.Text, IsPrintToPrinter)
        End If

    End Sub


    Private Sub FGetPrintSSRS(mPrintFor As ClsMain.PrintFor)
        Dim dsMain As DataTable
        Dim dsCompany As DataTable
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer

        PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")



        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            mPrintTitle = TxtV_Type.Text & " (Debit Note)"
        Else
            If Not AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
                mPrintTitle = "CHALLAN"
            Else
                mPrintTitle = "Purchase Invoice"
            End If
        End If

        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "


            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, H.DocID, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as DocNo,  Agent.DispName as AgentName, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption,
                H.VendorName as PartyName, H.VendorAddress as PartyAddress, C.CityName as CityName, State.ManualCode as StateCode, State.Description as StateName, 
                H.VendorMobile as PartyMobile, Sg.ContactPerson, H.VendorSalesTaxNo as PartySalesTaxNo, (Select RegistrationNo From SubgroupRegistration With (NoLock) Where RegistrationType='" & SubgroupRegistrationType.AadharNo & "' And Subcode=H.Vendor) as PartyAadharNo,
                (Select RegistrationNo From SubgroupRegistration  With (NoLock) Where RegistrationType='" & SubgroupRegistrationType.PanNo & "' And Subcode=H.Vendor) as PanNo,
                Transporter.Name as TransporterName, TD.LrNo, TD.LrDate, TD.PrivateMark, TD.Weight, TD.Freight, TD.PaymentType as FreightType, TD.RoadPermitNo, TD.RoadPermitDate, L.ReferenceNo,
                I.Description as ItemName, IG.Description as ItemGroupName, IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, I.HSN,
                L.SalesTaxGroupItem, STGI.GrossTaxRate, L.Pcs, Abs(L.Qty) as Qty, L.Rate, L.Unit, U.DecimalPlaces as UnitDecimalPlaces, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, 
                Abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) Tax1_Per, Abs(L.Tax1) as Tax1, Abs(L.Tax2_Per) as Tax2_Per, Abs(L.Tax2) as Tax2, Abs(L.Tax3_Per) as Tax3_Per, Abs(L.Tax3) as Tax3, Abs(L.Tax4_Per) as Tax4_Per, Abs(L.Tax4) as Tax4, Abs(L.Tax5_Per) as Tax5_Per, Abs(L.Tax5) as Tax5, Abs(L.Net_Amount) as Net_Amount,
                H.Remarks as HRemarks, IfNull(L.Remark,'') as LRemarks,
                abs(H.Gross_Amount) as H_Gross_Amount,abs(H.Taxable_Amount) as H_Taxable_Amount,abs(H.Tax1_Per) as H_Tax1_Per, abs(H.Tax1) as H_Tax1, 
                abs(H.Tax2_Per) as H_Tax2_Per, abs(H.Tax2) as H_Tax2, abs(H.Tax3_Per) as H_Tax3_Per, abs(H.Tax3) as H_Tax3, abs(H.Tax4_Per) as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                abs(H.Tax5_Per) as H_Tax5_Per, abs(H.Tax5) as H_Tax5, abs(H.Deduction_Per) as H_Deduction_Per, abs(H.Deduction) as H_Deduction, abs(H.Other_Charge_Per) as H_Other_Charge_Per, abs(H.Other_Charge) as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsCenterAlignedCompanyInfoOnDocuments")) & "' as IsCenterAlignedCompanyInfoOnDocuments, 
                L.DimensionDetail as DimDetail, '' as HsnDescription, '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
                from PurchInvoice H   With (NoLock)              
                Left Join PurchInvoiceDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Item I  With (NoLock) On L.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.VendorCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join PurchInvoiceTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.Vendor = Sg.Subcode                
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Where H.DocID = '" & mSearchCode & "'
                "

        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "







        dsMain = AgL.FillData(mQry, AgL.GCn).Tables(0)


        FReplaceInvoiceVariables(dsMain, TxtDivision.Tag, TxtSite_Code.Tag)

        dsCompany = ClsMain.GetDocumentHeaderDataTable(TxtDivision.Tag, TxtSite_Code.Tag, TxtV_Type.Tag)

        Dim objRepPrint As FrmRepPrint
        objRepPrint = New FrmRepPrint(AgL)
        objRepPrint.reportViewer1.Visible = True
        Dim id As Integer = 0
        objRepPrint.reportViewer1.ProcessingMode = ProcessingMode.Local


        If AgL.PubUserName.ToUpper = "SUPER" Then
            dsMain = ClsMain.RemoveNullFromDataTable(dsMain)
            dsCompany = ClsMain.RemoveNullFromDataTable(dsCompany)
            dsMain.WriteXml(AgL.PubReportPath + "\PurchaseInvoice_DsMain.xml")
            dsCompany.WriteXml(AgL.PubReportPath + "\PurchaseInvoice_DsCompany.xml")
        End If

        If ClsMain.IsScopeOfWorkContains("+CLOTH TRADING WHOLESALE") Then
            'objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\PurchaseInvoice_Cloth.rdl"
            objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\PurchInvoice_Cloth.rdl"
        Else
            objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\PurchaseInvoice.rdl"
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


    Private Sub FrmPurchInvoice_StoreItem_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FGetPrint(SearchCode, PrintFor.DocumentPrint)
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

            End Select
        Next
    End Sub


    Public Sub FPrintThisDocumentPurch(ByVal objFrm As Object, ByVal V_Type As String,
         Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "",
         Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "",
         Optional ByVal SubReport_QueryList As String = "",
         Optional ByVal SubReport_NameList As String = "", Optional ByVal PartyCode As String = "", Optional ByVal V_Date As String = "")

        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
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

            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                For I = 0 To SubReport_NameArr.Length - 1
                    mCrd.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
                Next
            End If

            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            ClsMain.Formula_Set(mCrd, CType(objFrm, AgTemplate.TempTransaction).TxtDivision.Tag, CType(objFrm, AgTemplate.TempTransaction).TxtSite_Code.Tag, CType(objFrm, AgTemplate.TempTransaction).TxtV_Type.Tag, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", objFrm.MdiParent)

            Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
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
                    From PurchInvoice H  With (NoLock)
                    Where H.DocID = '" & mSearchCode & "'"
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            'mText = Replace(mText, "<CreditDays>", AgL.XNull(dtTemp.Rows(0)("CreditDays")))
            ClsMain.FReplacePubVariables(mText, TxtDivision.Tag, TxtSite_Code.Tag)

        End If

        FReplaceInvoiceVariables = mText
    End Function

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtRemarks.KeyDown
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Private Function AccountPosting() As Boolean
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, J As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim mNarr As String = "", mCommonNarr$ = ""
        Dim mNetAmount As Double, mRoundOff As Double = 0
        Dim GcnRead As Object
        GcnRead = New Object
        GcnRead.ConnectionString = AgL.Gcn_ConnectionString
        GcnRead.Open()

        mNetAmount = 0
        mCommonNarr = ""
        mCommonNarr = ""
        If mCommonNarr.Length > 255 Then mCommonNarr = AgL.MidStr(mCommonNarr, 0, 255)

        ReDim Preserve LedgAry(I)
        I = UBound(LedgAry) + 1
        ReDim Preserve LedgAry(I)
        LedgAry(I).SubCode = AgL.XNull(AgL.PubDtEnviro.Rows(0)("PurchaseAc"))
        LedgAry(I).ContraSub = TxtVendor.AgSelectedValue
        LedgAry(I).AmtCr = 0
        LedgAry(I).AmtDr = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
        If mNarr.Length > 255 Then mNarr = AgL.MidStr(mNarr, 0, 255)
        LedgAry(I).Narration = mNarr

        I = UBound(LedgAry) + 1
        ReDim Preserve LedgAry(I)
        LedgAry(I).SubCode = TxtVendor.AgSelectedValue
        LedgAry(I).ContraSub = AgL.XNull(AgL.PubDtEnviro.Rows(0)("PurchaseAc"))
        LedgAry(I).AmtCr = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
        LedgAry(I).AmtDr = 0
        LedgAry(I).Narration = mNarr

        If AgL.PubManageOfflineData Then
            If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GcnSite, AgL.ECmdSite, mSearchCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.GcnSite_ConnectionString) = False Then
                AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
            Else
            End If
        End If

        If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GCn, AgL.ECmd, mSearchCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.Gcn_ConnectionString) = False Then
            AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
        End If
        GcnRead.Close()
        GcnRead.Dispose()
    End Function

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim DsTemp As DataSet
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemCategory
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemCategory) Is Nothing Then
                            FCreateHelpItemCategory()
                        End If
                    ElseIf e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    End If

                Case Col1ItemGroup
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                            FCreateHelpItemGroup(Dgl1.CurrentCell.RowIndex)
                        End If
                    ElseIf e.KeyCode = Keys.Insert Then
                        FOpenItemGroupMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    End If

                Case Col1Item
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem(Dgl1.CurrentCell.RowIndex)
                        End If
                    ElseIf e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    End If

                Case Col1DealQty
                    If Dgl1.AgHelpDataSet(Col1DealQty) Is Nothing Then
                        mQry = " SELECT Code, Code AS Description, DecimalPlaces FROM Unit  With (NoLock) "
                        Dgl1.AgHelpDataSet(Col1DealQty, 1) = AgL.FillData(mQry, AgL.GCn)
                    End If
                Case Col1SalesTaxGroup
                    If Dgl1.AgHelpDataSet(Col1SalesTaxGroup) Is Nothing Then
                        mQry = " SELECT Description as Code, Description FROM PostingGroupSalesTaxItem  With (NoLock) "
                        Dgl1.AgHelpDataSet(Col1SalesTaxGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension1  With (NoLock)  "
                            Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension2  With (NoLock)  "
                            Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension3) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension3  With (NoLock)  "
                            Dgl1.AgHelpDataSet(Col1Dimension3) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension4
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension4) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension4  With (NoLock)  "
                            Dgl1.AgHelpDataSet(Col1Dimension4) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1ReferenceNo
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1ReferenceNo) Is Nothing And mFirstInvoiceForSelectedParty = False Then
                            mQry = " SELECT H.DocID, IfNull(H.VendorDocNo, H.ManualRefNo) as [Party Doc No], H.ManualRefNo as [Invoice No], H.V_Date as [Invoice Date]  
                                     FROM PurchInvoice H  With (NoLock) 
                                     Left Join Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type  
                                     Where H.Vendor = '" & TxtVendor.Tag & "' and Vt.NCat = '" & Ncat.PurchaseInvoice & "'  
                                     And Date(H.V_Date) <= " & AgL.Chk_Date(TxtV_Date.Text) & "  
                                     And H.Div_Code = '" & TxtDivision.Tag & "' And H.Site_Code = '" & TxtSite_Code.Tag & "' "
                            DsTemp = AgL.FillData(mQry, AgL.GCn)
                            If DsTemp.Tables(0).Rows.Count > 0 Then
                                Dgl1.AgHelpDataSet(Col1ReferenceNo) = DsTemp
                            Else
                                Dgl1.AgHelpDataSet(Col1ReferenceNo) = Nothing
                            End If
                        End If
                    End If


                Case Col1BaleNo
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1BaleNo) Is Nothing Then
                            If BtnHeaderDetail.Tag IsNot Nothing Then
                                If AgL.XNull(CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowFill).Tag) <> "" Then
                                    mQry = " SELECT Code, LrBaleNo FROM LrBale WHERE GenDocID = '" & AgL.XNull(CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowFill).Tag) & "'  "
                                    Dgl1.AgHelpDataSet(Col1BaleNo) = AgL.FillData(mQry, AgL.GCn)
                                End If
                            End If
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub FOpenMaster(ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Dim FrmObj As Object = Nothing
    '    Dim CFOpen As New ClsFunction
    '    Dim DtTemp As DataTable = Nothing
    '    Try
    '        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

    '        If e.KeyCode = Keys.Insert Then
    '            If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Item Then
    '                If Not mItemType.Contains(",") Then
    '                    mQry = " Select MnuName, MnuText From ItemType Where Code = '" & mItemType & "' "
    '                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                    If DtTemp.Rows.Count > 0 Then
    '                        FrmObj = CFOpen.FOpen(DtTemp.Rows(0)("MnuName"), DtTemp.Rows(0)("MnuText"), True)
    '                        If FrmObj IsNot Nothing Then
    '                            FrmObj.MdiParent = Me.MdiParent
    '                            FrmObj.Show()
    '                            FrmObj.Topctrl1.FButtonClick(0)
    '                            FrmObj = Nothing
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer, ByVal DrTemp As DataRow())
        Dim DtTemp As DataTable = Nothing
        Dim dtInvoices As DataTable
        Dim dtItem As DataTable
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1Dimension1, mRow).Value = ""
                Dgl1.Item(Col1Dimension1, mRow).Tag = ""
                Dgl1.Item(Col1Dimension2, mRow).Value = ""
                Dgl1.Item(Col1Dimension2, mRow).Tag = ""
            Else
                If DrTemp IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DrTemp(0)("Code"))
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DrTemp(0)("Description"))
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DrTemp(0)("Code"))
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DrTemp(0)("ManualCode"))
                    Call FCheckDuplicate(mRow)
                    Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(DrTemp(0)("Specification"))
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl1.Item(Col1Unit, mRow).Tag = AgL.XNull(DrTemp(0)("showdimensiondetailInPurchase"))
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DrTemp(0)("Rate"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                    End If
                    'Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    'Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension1Caption() & "").Value)
                    'Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    'Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension2Caption() & "").Value)

                    Dgl1.Item(Col1UnitMultiplier, mRow).Value = AgL.VNull(DrTemp(0)("UnitMultiplier"))
                    Dgl1.Item(Col1DealUnit, mRow).Value = AgL.XNull(DrTemp(0)("DealUnit"))
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1DealDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("DealDecimalPlaces"))

                    'Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(DrTemp(0)("Bal.DocQty"))
                    'Dgl1.Item(Col1FreeQty, mRow).Value = AgL.VNull(DrTemp(0)("Bal.FreeQty"))
                    'Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(DrTemp(0)("Bal.Qty"))
                Else
                    If Dgl1.AgDataRow IsNot Nothing Then
                        Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                        Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Description").Value)
                        Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                        Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ManualCode").Value)
                        Call FCheckDuplicate(mRow)
                        Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Specification").Value)
                        Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                        Dgl1.Item(Col1Unit, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("showdimensiondetailInPurchase").Value)
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Rate").Value)

                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                        If AgL.StrCmp(Dgl1.Item(Col1SalesTaxGroup, mRow).Tag, "") Then
                            Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                        End If
                        Dgl1.Item(Col1UnitMultiplier, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("UnitMultiplier").Value)
                        Dgl1.Item(Col1DealUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DealUnit").Value)
                        Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                        Dgl1.Item(Col1DealDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("DealDecimalPlaces").Value)


                        If Val(Dgl1.Item(Col1Rate, mRow).Value) = 0 Then
                            If AgL.PubServerName = "" Then
                                mQry = " Select L.Rate, L.MRP From PurchInvoiceDetail L  With (NoLock) LEFT JOIN PurchInvoice H  With (NoLock) ON L.DocId = H.DocId Where L.Item = '" & Dgl1.Item(Col1Item, mRow).Tag & "' Order By H.V_Date Desc Limit 1 "
                            Else
                                mQry = " Select Top 1 L.Rate, L.MRP From PurchInvoiceDetail L  With (NoLock) LEFT JOIN PurchInvoice H ON L.DocId = H.DocId Where L.Item = '" & Dgl1.Item(Col1Item, mRow).Tag & "' Order By H.V_Date Desc  "
                            End If
                            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                            If DtTemp.Rows.Count > 0 Then
                                Dgl1.Item(Col1MRP, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MRP"))
                                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Rate"))
                            End If
                        End If
                    End If
                End If
            End If


            If Dgl1.Item(Col1Item, mRow).Value <> "" Then
                mQry = "Select I.ProfitMarginPer, I.ItemGroup as ItemGroupCode, IG.Description as ItemGroupName, 
                        I.ItemCategory as ItemCategoryCode, IC.Description as ItemCategoryName, 
                        IG.Default_DiscountPerPurchase,IG.Default_AdditionalDiscountPerPurchase,IG.Default_AdditionPerPurchase,
                        I.ItemType, IT.Name as ItemTypeName 
                        From Item I  With (NoLock)
                        Left Join ItemGroup IG  With (NoLock) on I.ItemGroup = IG.Code
                        Left Join ItemCategory IC  With (NoLock) on I.ItemCategory = IC.Code
                        Left Join ItemType IT With (NoLock) On I.ItemType = IT.Code
                        Where I.Code = '" & AgL.XNull(Dgl1.Item(Col1Item, mRow).Tag) & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1ProfitMarginPer, mRow).Value = AgL.VNull(DtTemp.Rows(0)("ProfitMarginPer"))
                    Dgl1.Item(Col1ItemType, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemType"))
                    Dgl1.Item(Col1ItemType, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ItemTypeName"))
                    Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemCategoryCode"))
                    Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ItemCategoryName"))
                    Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemGroupCode"))
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ItemGroupName"))
                    Dgl1.Item(Col1DefaultDiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Default_DiscountPerPurchase"))
                    Dgl1.Item(Col1DefaultAdditionalDiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                    Dgl1.Item(Col1DefaultAdditionPer, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Default_AdditionPerPurchase"))
                End If
            End If

            Dim DrItemTypeSetting As DataRow
            DrItemTypeSetting = FItemTypeSettings(Dgl1(Col1ItemType, mRow).Tag)
            Dgl1(Col1DiscountCalculationPattern, mRow).Value = AgL.XNull(DrItemTypeSetting("DiscountCalculationPatternPurchase"))
            Dgl1(Col1AdditionalDiscountCalculationPattern, mRow).Value = AgL.XNull(DrItemTypeSetting("AdditionalDiscountCalculationPatternPurchase"))
            Dgl1(Col1AdditionCalculationPattern, mRow).Value = AgL.XNull(DrItemTypeSetting("AdditionCalculationPatternPurchase"))



            Dim strReturnTicked As String
            mFirstInvoiceForSelectedParty = False
            If LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.PurchaseReturn Then
                'If TxtVendorDocNo.Text = "" Then
                strReturnTicked = FHPGD_PendingSaleChallan(Dgl1.Item(Col1Item, mRow).Tag)
                    If strReturnTicked <> "" Then
                        FillGridForSaleReturn(strReturnTicked, True)
                    Else
                        If MsgBox("No Invoice found to return for selected customer. Do you want to continue without invoice references?", vbYesNo) = MsgBoxResult.No Then
                            Dgl1.Rows(Dgl1.CurrentCell.RowIndex).Visible = False
                            strReturnTicked = "."
                            Dgl1.Rows.Add()
                        'Else
                        '    If Dgl1.CurrentCell.RowIndex = 0 Then

                        '        If AgL.PubServerName = "" Then
                        '            mQry = "Select IfNull(H.VendorDocNo,H.ManualRefNo) as ManualRefNo, IfNull(H.VendorDocDate,H.V_Date) as DocDate, H.DocID From PurchInvoice H  With (NoLock) Left Join Voucher_Type Vt With (NoLock) On H.V_Type= Vt.V_Type Where Vt.NCat='" & Ncat.PurchaseInvoice & "' And H.Vendor = '" & TxtVendor.Tag & "' And H.V_Date > Date(H.V_Date,'-15 days') Order By H.V_Date Desc Limit 1"
                        '        Else
                        '            mQry = "Select Top 1 IfNull(H.VendorDocNo,H.ManualRefNo) as ManualRefNo, IfNull(H.VendorDocDate,H.V_Date) as DocDate, H.DocID From PurchInvoice H  With (NoLock)  Left Join Voucher_Type Vt With (NoLock) On H.V_Type= Vt.V_Type Where Vt.NCat='" & Ncat.PurchaseInvoice & "' And  H.Vendor = '" & TxtVendor.Tag & "' And H.V_Date > DateAdd(D,-15,H.V_Date)  Order By H.V_Date Desc"
                        '        End If

                        '        dtInvoices = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
                        '        If dtInvoices.Rows.Count > 0 Then
                        '            Dgl1.Item(Col1ReferenceDocID, mRow).Value = AgL.XNull(dtInvoices.Rows(0)("DocID"))
                        '            Dgl1.Item(Col1ReferenceNo, mRow).Value = AgL.XNull(dtInvoices.Rows(0)("ManualRefNo"))
                        '            Dgl1.Item(Col1ReferenceDate, mRow).Value = ClsMain.FormatDate(AgL.XNull(dtInvoices.Rows(0)("DocDate")))
                        '        Else
                        '            mFirstInvoiceForSelectedParty = True
                        '        End If
                        '    Else
                        '        Dgl1.Item(Col1ReferenceDocID, mRow).Value = AgL.XNull(Dgl1.Item(Col1ReferenceDocID, mRow - 1).Value)
                        '        Dgl1.Item(Col1ReferenceNo, mRow).Value = AgL.XNull(Dgl1.Item(Col1ReferenceNo, mRow - 1).Value)
                        '        Dgl1.Item(Col1ReferenceDate, mRow).Value = AgL.XNull(Dgl1.Item(Col1ReferenceDate, mRow - 1).Value)
                        '    End If
                        '    Dgl1.Item(Col1DocQty, mRow).Value = 1
                        '    Dgl1.Item(Col1Qty, mRow).Value = 1
                    End If
                    End If
                'End If
            End If



            FSetSalesTaxGroupItemBasedOnRate(mRow)
            mQry = "Select * from ItemGroupPerson  With (NoLock) Where ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRow).Tag & "' 
                        And ItemGroup  = '" & Dgl1.Item(Col1ItemGroup, mRow).Tag & "'
                        And Person  = '" & TxtVendor.Tag & "'
                       "
            dtItem = AgL.FillData(mQry, AgL.GCn).tables(0)
            If dtItem.Rows.Count > 0 Then
                If AgL.VNull(dtItem.Rows(0)("DiscountPer")) > 0 Then
                    If Dgl1(Col1DiscountCalculationPattern, mRow).Value.ToString.ToUpper = AgL.XNull(dtItem.Rows(0)("DiscountCalculationPattern")).toupper() Or Dgl1(Col1DiscountCalculationPattern, mRow).Value.ToString.ToUpper = "" Then
                        Dgl1.Item(Col1PersonalDiscountPer, mRow).Value = AgL.VNull(dtItem.Rows(0)("DiscountPer"))
                    Else
                        MsgBox("Discount Calculation Pattern is changes since last invoice.")
                    End If
                End If

                If AgL.VNull(dtItem.Rows(0)("AdditionalDiscountPer")) > 0 Then
                    If Dgl1(Col1AdditionalDiscountCalculationPattern, mRow).Value.ToString.ToUpper = AgL.XNull(dtItem.Rows(0)("AdditionalDiscountCalculationPattern")).toupper() Or Dgl1(Col1AdditionalDiscountCalculationPattern, mRow).Value.ToString.ToUpper = "" Then
                        Dgl1.Item(Col1PersonalAdditionalDiscountPer, mRow).Value = AgL.VNull(dtItem.Rows(0)("AdditionalDiscountPer"))
                    Else
                        MsgBox("Additional Discount Calculation Pattern is changes since last invoice.")
                    End If
                End If

                If AgL.VNull(dtItem.Rows(0)("AdditionPer")) > 0 Then
                    If Dgl1(Col1AdditionCalculationPattern, mRow).Value.ToString.ToUpper = AgL.XNull(dtItem.Rows(0)("AdditionCalculationPattern")).toupper() Or Dgl1(Col1AdditionCalculationPattern, mRow).Value.ToString.ToUpper = "" Then
                        Dgl1.Item(Col1PersonalAdditionPer, mRow).Value = AgL.VNull(dtItem.Rows(0)("AdditionPer"))
                    Else
                        MsgBox("Additional Discount Calculation Pattern is changes since last invoice.")
                    End If
                End If
            End If



            If AgL.XNull(DtV_TypeSettings.Rows(0)("DiscountSuggestionPattern")).ToUpper() = DiscountSuggestPattern.FillAutomatically.ToUpper Then
                If Val(Dgl1.Item(Col1PersonalDiscountPer, mRow).Value) <> 0 Then
                    Dgl1.Item(Col1DiscountPer, mRow).Value = Format(Val(Dgl1.Item(Col1PersonalDiscountPer, mRow).Value), "0.000")
                    Dgl1.Item(Col1AdditionalDiscountPer, mRow).Value = Format(Val(Dgl1.Item(Col1PersonalAdditionalDiscountPer, mRow).Value), "0.000")
                    Dgl1.Item(Col1AdditionPer, mRow).Value = Format(Val(Dgl1.Item(Col1PersonalAdditionPer, mRow).Value), "0.000")
                Else
                    Dgl1.Item(Col1DiscountPer, mRow).Value = Format(Val(Dgl1.Item(Col1DefaultDiscountPer, mRow).Value), "0.000")
                    Dgl1.Item(Col1AdditionalDiscountPer, mRow).Value = Format(Val(Dgl1.Item(Col1DefaultAdditionalDiscountPer, mRow).Value), "0.000")
                    Dgl1.Item(Col1AdditionPer, mRow).Value = Format(Val(Dgl1.Item(Col1DefaultAdditionPer, mRow).Value), "0.000")
                End If
            End If



            If mRow > 1 Then
                If Dgl1.Item(Col1LRNo, mRow - 1).Value <> "" Then
                    Dgl1.Item(Col1LRNo, mRow).Value = Dgl1.Item(Col1LRNo, mRow - 1).Value
                    Dgl1.Item(Col1LRDate, mRow).Value = Dgl1.Item(Col1LRDate, mRow - 1).Value
                End If
            End If

            Dgl1.Item(Col1DocQty, mRow).Tag = Nothing
            If (Dgl1.Item(Col1Unit, mRow).Tag) Then
                Dgl1.Item(Col1DocQty, mRow).Style.ForeColor = Color.Blue
                ShowPurchInvoiceDimensionDetail(mRow)
            End If


        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Function FHPGD_PendingSaleChallan(Optional ItemCode As String = "") As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable


        mLineCond = " And S.Subcode = '" & TxtVendor.Tag & "' "
        If ItemCode <> "" Then
            mLineCond = " And S.Item = '" & ItemCode & "' "
        End If

        mQry = "
                Select 'o' As Tick, SI.DocID || '#' || Cast(SI.TSr as Varchar) || '#' || Cast(SI.Sr as Varchar) as SearchKey, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, IfNull(H.VendorDocNo,'') as PartyDocNo, H.V_Date as InvoiceDate, 
                SI.Item, I.Description as ItemName, SI.Qty_Rec + IfNull(SR.Qty_Ret,0) Qty_Bal, SI.Unit,SI.Rate  
                From
                    (    
                    select S.DocID, S.Tsr, S.Sr,  S.Item, S.Qty_Rec, S.Unit, S.Rate 
                    from Stock S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.NCat = '" & AgLibrary.ClsMain.agConstants.Ncat.PurchaseInvoice & "' " & mLineCond & "
                    Union All 
                    select S.DocID, S.Tsr, S.Sr,  S.Item, S.Qty_Rec, S.Unit, S.Rate 
                    from StockProcess S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.NCat = '" & AgLibrary.ClsMain.agConstants.Ncat.PurchaseInvoice & "' " & mLineCond & "
                    ) as SI
                Left Join 
                    (
                    select S.ReferenceDocID, S.ReferenceTsr, S.ReferenceDocIDSr, Sum(S.Qty_Rec) as Qty_Ret
                    from Stock S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.nCat='" & AgLibrary.ClsMain.agConstants.Ncat.PurchaseReturn & "'  " & mLineCond & "
                    Group By S.ReferenceDocID, S.ReferenceTsr, S.ReferenceDocIDSr
                    ) As SR On SI.DocID = SR.ReferenceDocID And SI.TSr = SR.ReferenceTSr And SI.Sr = SR.ReferenceDocIDSr
                Left Join PurchInvoice H  With (NoLock) On SI.DocID = H.DocID
                Left Join Item I  With (NoLock) on SI.Item = I.Code
                Where Vendor='" & TxtVendor.Tag & "' And SI.Qty_REC - IfNull(SR.Qty_Ret,0) >0
                And Date(H.V_Date) <= " & AgL.Chk_Date(TxtV_Date.Text) & "                
                Order By H.V_Date Desc "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            Exit Function
        End If

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 950, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Invoice No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Party Doc No", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Invoice Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, , 0, DataGridViewContentAlignment.MiddleLeft, False)
        FRH_Multiple.FFormatColumn(6, "Item", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(7, "Bal Qty", 80, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(8, "Unit", 70, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(9, "Rate", 70, DataGridViewContentAlignment.MiddleLeft)

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


            mQry = "    Select  H.DocID,  H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, IfNull(H.VendorDocNo,'') as PartyDocNo, H.VendorDocDate as PartyDocDate, H.V_Date as InvoiceDate, 
                SI.Item, I.ManualCode as ItemManualCode, I.Description as ItemName, SI.Qty_Rec + IfNull(SR.Qty_Ret,0) Qty_Bal, SI.Unit, L.DiscountPer, L.AdditionalDiscountPer, L.Rate,
                I.ItemCategory, IC.Description as ItemCategoryName, I.ItemGroup, IG.Description as ItemGroupName,
                U.ShowDimensionDetailInSales, U.DecimalPlaces as QtyDecimalPlaces, IG.Default_DiscountPerPurchase, L.SalesTaxGroupItem, SI.DocID as StockDocID, SI.TSr as StockTSr, SI.Sr as StockSr 
                From
                    (    
                    select S.DocID, S.Tsr, S.Sr,  S.Item, S.Qty_Rec, S.Unit, S.Rate 
                    from Stock S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.NCat = '" & AgLibrary.ClsMain.agConstants.Ncat.PurchaseInvoice & "'
                    Union All 
                    select S.DocID, S.Tsr, S.Sr,  S.Item, S.Qty_Rec, S.Unit, S.Rate 
                    from StockProcess S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.NCat = '" & AgLibrary.ClsMain.agConstants.Ncat.PurchaseInvoice & "'
                    ) as SI
                Left Join 
                    (
                    select S.ReferenceDocID, S.ReferenceTsr, S.ReferenceDocIDSr, Sum(S.Qty_Rec) as Qty_Ret
                    from Stock S  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) on S.V_Type = VT.V_Type
                    where VT.nCat='" & AgLibrary.ClsMain.agConstants.Ncat.PurchaseReturn & "'
                    Group By S.ReferenceDocID, S.ReferenceTsr, S.ReferenceDocIDSr
                    ) As SR On SI.DocID = SR.ReferenceDocID And SI.TSr = SR.ReferenceTSr And SI.Sr = SR.ReferenceDocIDSr
                Left Join PurchInvoice H  With (NoLock) On SI.DocID = H.DocID
                Left Join Item I  With (NoLock) on SI.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code 
                Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join PurchInvoiceDetail L  With (NoLock) On L.DocID = SI.DocID And L.Sr = SI.TSr
                Where SI.DocID || '#' || Cast(SI.TSr as varchar) || '#' || Cast(SI.Sr as Varchar) in (" & strInvoiceLines & ")
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
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("SalesTaxGroupItem"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(DtTemp.Rows(I)("SalesTaxGroupItem"))
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(I)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1DefaultDiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(I)("Default_DiscountPerPurchase"))
                    Dgl1.Item(Col1DiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(I)("DiscountPer"))
                    Dgl1.Item(Col1AdditionalDiscountPer, mRow).Value = AgL.VNull(DtTemp.Rows(I)("AdditionalDiscountPer"))
                    Dgl1.Item(Col1PurchaseInvoice, mRow).Value = AgL.XNull(DtTemp.Rows(I)("DocID"))
                    If AgL.VNull(DtV_TypeSettings.Rows(0)("PickPurchaseRateFromMaster")) = True Then
                        'mQry = "select Rate from RateListDetail  With (NoLock) where Item ='" & Dgl1.Item(Col1Item, mRow).Tag & "' and RateType Is Null"
                        mQry = "select PurchaseRate as Rate from Item With (NoLock) where Code ='" & Dgl1.Item(Col1Item, mRow).Tag & "'"
                        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtItem.Rows.Count > 0 Then
                            Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtItem.Rows(0)("Rate"))
                        End If
                        FSetSalesTaxGroupItemBasedOnRate(mRow)
                    Else
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(I)("Rate"))
                    End If
                    If DtTemp.Rows.Count > 1 Then
                        Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(DtTemp.Rows(I)("Qty_Bal"))
                        Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(DtTemp.Rows(I)("Qty_Bal"))
                    Else
                        Dgl1.Item(Col1Qty, mRow).Value = 0 'AgL.VNull(DtTemp.Rows(I)("Qty_Bal"))
                        Dgl1.Item(Col1DocQty, mRow).Value = 0 'AgL.VNull(DtTemp.Rows(I)("Qty_Bal"))
                    End If
                    Dgl1.Item(Col1ReferenceNo, mRow).Value = IIf(AgL.XNull(DtTemp.Rows(I)("PartyDocNo")) = "", AgL.XNull(DtTemp.Rows(I)("InvoiceNo")), AgL.XNull(DtTemp.Rows(I)("PartyDocNo")))
                    Dgl1.Item(Col1ReferenceDate, mRow).Value = IIf(AgL.XNull(DtTemp.Rows(I)("PartyDocDate")) = "", AgL.XNull(DtTemp.Rows(I)("InvoiceDate")), AgL.XNull(DtTemp.Rows(I)("PartyDocDate")))
                    Dgl1.Item(Col1ReferenceDocID, mRow).Value = AgL.XNull(DtTemp.Rows(I)("StockDocID"))
                    Dgl1.Item(Col1ReferenceTSr, mRow).Value = AgL.XNull(DtTemp.Rows(I)("StockTSr"))
                    Dgl1.Item(Col1ReferenceSr, mRow).Value = AgL.XNull(DtTemp.Rows(I)("StockSr"))

                Next

                FShowTransactionHistory(AgL.XNull(DtTemp.Rows(0)("Item")))
                Calculation()
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
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


    Private Function GetSaleRate(RowIndex As Integer) As Double
        Dim mPricePerUnit As Double
        Dim mSaleRate As Double = 0
        If Val(Dgl1.Item(Col1ProfitMarginPer, RowIndex).Value) > 0 Then
            mPricePerUnit = Val(Dgl1.Item(Col1Amount, RowIndex).Value) / Val(Dgl1.Item(Col1Qty, RowIndex).Value)
            mSaleRate = Math.Round(mPricePerUnit + mPricePerUnit * Val(Dgl1.Item(Col1ProfitMarginPer, RowIndex).Value) / 100, 2)
        End If
        GetSaleRate = mSaleRate
    End Function

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

    Private Sub FGetUnitMultiplier(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try


            'If Dgl1.Item(Col1DealUnit, mRow).Value <> "" And Dgl1.Item(Col1TotalDocDealQty, mRow).Value <> "" Then
            '    If Dgl1.Item(Col1MeasureUnit, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow).Value Then
            '        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
            '    Else
            '        mQry = " SELECT Multiplier, Rounding FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1MeasureUnit, mRow).Value & "' AND ToUnit =  '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "' "
            '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            '        With DtTemp
            '            If .Rows.Count > 0 Then
            '                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(.Rows(0)("Multiplier"))
            '            Else
            '                MsgBox("Define Multiplier In Unit Conversion To Convert " & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & " From " & Dgl1.Item(Col1MeasureUnit, mRow).Value & " ", MsgBoxStyle.Information)
            '                Dgl1.Item(Col1DeliveryMeasure, mRow).Value = ""
            '            End If
            '        End With
            '    End If
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVendor.KeyDown, TxtBillToParty.KeyDown, TxtProcess.KeyDown, TxtAgent.KeyDown, TxtShipToParty.KeyDown
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case sender.name

                Case TxtVendor.Name
                    If TxtVendor.AgHelpDataSet Is Nothing Then
                        FCreateHelpSubgroup(sender)
                    End If


                Case TxtBillToParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then


                            mQry = "SELECT Sg.Code As Code, Sg.Name As Account_Name, Sg.SubgroupType as [A/C Type] " &
                                    " FROM viewHelpSubGroup Sg  With (NoLock) " &
                                    " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                                    " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Leaf.ToUpper) Then
                                mQry += " And Sg.Parent Is Null "
                            End If
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


                Case TxtShipToParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Sg.Code, Sg.Name
                                    FROM viewHelpSubGroup Sg  With (NoLock) 
                                    Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' 
                                    And Sg.Nature In ('Customer','Supplier')
                                    "
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


                Case TxtAgent.Name
                    If TxtAgent.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Code, Name From ViewHelpSubgroup  With (NoLock) Where SubgroupType = '" & SubgroupType.PurchaseAgent & "' Order By Name "
                        TxtAgent.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If


                Case TxtProcess.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtProcess.AgHelpDataSet Is Nothing Then
                            mQry = "Select P.NCat As Code, P.Description As Process, P.CostCenter, CCM.Name as CostCenterDesc, P.DefaultBillingType, P.Div_Code " &
                                  " From Process P  With (NoLock)  " &
                                  " Left Join CostCenterMast CCM On P.CostCenter = CCM.Code " &
                                  " Order By P.Description "
                            TxtProcess.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtTags.Name
                    If e.KeyCode <> Keys.Enter Then
                        TxtTags.Text = FHPGD_Tags()
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FHPGD_Tags() As String
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
            FHPGD_Tags = "+" + FRH_Multiple.FFetchData(2, "", "", "+")
        Else
            FHPGD_Tags = ""
        End If
        FRH_Multiple = Nothing
    End Function

    Private Sub FCreateHelpSubgroup(ByVal sender As AgControls.AgTextBox)
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || H.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || H.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') <= 0 "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || H.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || H.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) & "') <= 0 "
                End If
            End If
            End If

        If FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || IfNull(Sp.Process,'" & Process.Purchase & "'),'" & FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) & "') > 0 "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || IfNull(Sp.Process,'" & Process.Purchase & "'),'" & FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) & "') <= 0 "
            End If
        End If


        If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Leaf.ToUpper) Then
                strCond += " And H.Parent Is Not Null "
            End If
            If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Root.ToUpper) Then
                strCond += " And H.Parent Is Null "
            End If
            If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Parent.ToUpper) Then
                strCond += " And H.SubCode In (Select Distinct Parent From SubGroup) "
            End If
        End If

        'strCond += " And H.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Cash & "')"

        mQry = " SELECT H.SubCode, H.Name || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS [Party], " &
                " H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  With (NoLock) " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " Left Join SubgroupProcess SP On H.Subcode = SP.Subcode " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        mQry += " Union All SELECT H.SubCode, H.Name || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS [Party], " &
                " H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  With (NoLock) " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " Left Join SubgroupProcess SP On H.Subcode = SP.Subcode " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                " And H.Nature In ('" & ClsMain.SubGroupNature.Customer & "')    "
        If FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) <> "" Then
            mQry += " And CharIndex('+' || IfNull(Sp.Process,'.'),'" & FGetSettings(SettingFields.FilterInclude_Process, SettingType.General) & "') > 0 "
        End If

        sender.AgHelpDataSet(2, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If mFlag_Import = True Then Exit Sub
        'If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
        FShowTransactionHistory(Dgl1.Item(Col1Item, e.RowIndex).Tag)
        'End If
    End Sub

    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        DGL.Visible = False
    End Sub

    'Private Sub FCheckDuplicate(ByVal mRow As Integer)
    '    Dim I As Integer = 0
    '    Try
    '        With Dgl1
    '            For I = 0 To .Rows.Count - 1
    '                If .Item(Col1Item, I).Value <> "" Then
    '                    If mRow <> I Then
    '                        If AgL.StrCmp(.Item(Col1Item, I).Value, .Item(Col1Item, mRow).Value) Then
    '                            If MsgBox("Item " & .Item(Col1Item, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
    '                                Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
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
                                Str1 = Dgl1.Item(Col1Item, I).Value & Dgl1.Item(Col1Specification, I).Value & Dgl1.Item(Col1Dimension1, I).Value & Dgl1.Item(Col1Dimension2, I).Value & Dgl1.Item(Col1Dimension3, I).Value & Dgl1.Item(Col1Dimension4, I).Value
                                Str2 = Dgl1.Item(Col1Item, mRow).Value & Dgl1.Item(Col1Specification, mRow).Value & Dgl1.Item(Col1Dimension1, mRow).Value & Dgl1.Item(Col1Dimension2, mRow).Value & Dgl1.Item(Col1Dimension3, mRow).Value & Dgl1.Item(Col1Dimension4, mRow).Value
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

    Private Sub FUpdateDeal(ByVal mRow As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        Dim UPDATEQRY$ = ""
        If AgL.PubServerName = "" Then
            UPDATEQRY = " UPDATE Item Set " &
                " Deal = (Select L.DEAL From PURCHINVOICEDETAIL L  With (NoLock) LEFT JOIN PURCHINVOICE H  With (NoLock) ON L.DOCID = H.DOCID ORDER BY V_DATE DESC Limit 1) " &
                " Where Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "'"
        Else
            UPDATEQRY = " UPDATE Item Set " &
                " Deal = (Select Top 1 L.DEAL From PURCHINVOICEDETAIL L  With (NoLock) LEFT JOIN PURCHINVOICE H  With (NoLock) ON L.DOCID = H.DOCID ORDER BY V_DATE DESC) " &
                " Where Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "'"
        End If
        AgL.Dman_ExecuteNonQry(UPDATEQRY, Conn, Cmd)
    End Sub

    'Private Sub FOpenItemMaster()
    '    Dim FrmObj As Object = Nothing
    '    Dim CFOpen As New ClsFunction
    '    Dim MDI As New MDIMain
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
    '    Dim bItemCode$ = ""
    '    Try
    '        bRowIndex = Dgl1.CurrentCell.RowIndex
    '        bColumnIndex = Dgl1.CurrentCell.ColumnIndex

    '        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
    '            Case Col1Item
    '                FrmObj = CFOpen.FOpen("MnuItemMaster", "Item Master", True)
    '                If FrmObj IsNot Nothing Then
    '                    FrmObj.StartPosition = FormStartPosition.Manual
    '                    FrmObj.IsReturnValue = True
    '                    FrmObj.Top = 50
    '                    FrmObj.ShowDialog()
    '                    bItemCode = FrmObj.mItemCode
    '                    FrmObj = Nothing

    '                    Dgl1.Item(Col1Item, bRowIndex).Value = ""
    '                    Dgl1.Item(Col1Item, bRowIndex).Tag = ""

    '                    Dgl1.CurrentCell = Dgl1.Item(Col1DocQty, bRowIndex)

    '                    mQry = "SELECT I.Code, I.Description, I.ManualCode, I.Specification, I.Unit, I.SalesTaxPostingGroup, I.Measure As MeasurePerPcs, " & _
    '                              " I.MeasureUnit, I.Rate, " & _
    '                              " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces " & _
    '                              " FROM Item I " & _
    '                              " LEFT JOIN Unit U On I.Unit = U.Code " & _
    '                              " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
    '                              " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
    '                    Dgl1.AgHelpDataSet(Col1Item, 7) = AgL.FillData(mQry, AgL.GCn)

    '                    If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
    '                        DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & bItemCode & "'")
    '                        If DrTemp.Length > 0 Then
    '                            Dgl1.Item(Col1Item, bRowIndex).Tag = AgL.XNull(DrTemp(0)("Code"))
    '                            Dgl1.Item(Col1Item, bRowIndex).Value = AgL.XNull(DrTemp(0)("Description"))
    '                            Dgl1.Item(Col1ItemCode, bRowIndex).Tag = AgL.XNull(DrTemp(0)("Code"))
    '                            Dgl1.Item(Col1ItemCode, bRowIndex).Value = AgL.XNull(DrTemp(0)("ManualCode"))
    '                            Dgl1.Item(Col1Specification, bRowIndex).Value = AgL.XNull(DrTemp(0)("Specification"))
    '                            Dgl1.Item(Col1Unit, bRowIndex).Value = AgL.XNull(DrTemp(0)("Unit"))
    '                            Dgl1.Item(Col1QtyDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("QtyDecimalPlaces"))
    '                            Dgl1.Item(Col1MeasurePerPcs, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasurePerPcs"))
    '                            Dgl1.Item(Col1MeasureUnit, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
    '                            Dgl1.Item(Col1MeasureDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))
    '                            Dgl1.Item(Col1DeliveryMeasure, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
    '                            Dgl1.Item(Col1DeliveryMeasureMultiplier, bRowIndex).Value = 1
    '                            Dgl1.Item(Col1Rate, bRowIndex).Value = AgL.XNull(DrTemp(0)("Rate"))
    '                            Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
    '                            Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Value = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
    '                            If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, bRowIndex), "") Then
    '                                Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
    '                                Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub FGetPurchIndent(ByVal ItemCode As String, ByRef PurchIndent As String)
        mQry = " Select H.DocId From PurchIndent H  With (NoLock) LEFT JOIN PurchIndentDetail L  With (NoLock) On H.DocId = L.DocId " &
                " Where L.Item = '" & ItemCode & "' " &
                " And Date(H.V_Date) <= '" & TxtV_Date.Text & "' " &
                " Order By H.V_Date  "
        PurchIndent = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    End Sub


    Private Sub TxtVendorDocDate_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtVendorDocDate.Enter
        Try
            Select Case sender.Name
                Case TxtVendorDocDate.Name
                    If LblV_Type.Tag <> Ncat.PurchaseReturn Then
                        If TxtVendorDocDate.Text = "" Then
                            TxtVendorDocDate.Text = TxtV_Date.Text
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpItem(RowIndex As Integer)
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

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemCategory,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemCategory,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) & "') <= 0 "
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


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') <= 0 "
                End If
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or I.Div_Code Is Null Or IfNull(I.ShowItemInOtherDivisions,0) =1)  "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or I.Site_Code Is Null Or IfNull(I.ShowItemInOtherSites,0) =1)  "
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" And UserMovedOverItemCategory Then
            strCond += " And I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' "
        End If

        If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" And UserMovedOverItemGroup Then
            strCond += " And I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' "
        End If

        mQry = "SELECT I.Code, I.Description, IfNull(I.HSN, IC.HSN) as HSN, I.ManualCode,  
                        I.Unit, I.PurchaseRate as Rate, I.SalesTaxPostingGroup , 
                        I.DealQty As UnitMultiplier, I.DealUnit, 
                        U.DecimalPlaces As QtyDecimalPlaces, U.showdimensiondetailInPurchase, U1.DecimalPlaces As DealDecimalPlaces, I.Specification
                        FROM Item I  With (NoLock)
                        LEFT JOIN Item Ic With (NoLock) On I.ItemCategory = Ic.Code
                        Left JOIN Unit U  With (NoLock) On I.Unit = U.Code
                        LEFT JOIN Unit U1  With (NoLock) On I.DealUnit = U1.Code 
                        Where I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' 
                        And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond


        mQry += " UNION ALL "
        mQry += "SELECT I.Code, I.Description, IfNull(I.HSN, IC.HSN) as HSN, I.ManualCode,  
                        I.Unit, I.PurchaseRate as Rate, I.SalesTaxPostingGroup , 
                        I.DealQty As UnitMultiplier, I.DealUnit, 
                        U.DecimalPlaces As QtyDecimalPlaces, U.showdimensiondetailInPurchase, U1.DecimalPlaces As DealDecimalPlaces, I.Specification
                        FROM Item I  With (NoLock)
                        LEFT JOIN Item Ic With (NoLock) On I.ItemCategory = Ic.Code
                        Left JOIN Unit U  With (NoLock) On I.Unit = U.Code
                        LEFT JOIN Unit U1  With (NoLock) On I.DealUnit = U1.Code 
                        Where I.ItemType = '" & ItemTypeCode.ServiceProduct & "' 
                        And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "


        Dgl1.AgHelpDataSet(Col1Item, 7) = AgL.FillData(mQry, AgL.GCn)

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value = "" And Dgl1.Item(Col1ItemGroup, RowIndex).Value = "" Then
            mFullItemListInHelp = True
        Else
            mFullItemListInHelp = False
        End If
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
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' OR I.ItemCategory Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or I.Div_Code Is Null Or IfNull(I.ShowItemGroupInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or I.Site_Code Is Null Or IfNull(I.ShowItemGroupInOtherSites,0) =1) "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM ItemGroup I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub FOpenItemMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""


        Dim DtTemp As DataTable = Nothing

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
        frmObj.ShowDialog()
        bItemCode = frmObj.mSearchCode
        frmObj = Nothing






        'bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Master", TxtV_Type.Tag)
        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1DocQty, RowIndex)
        'FCreateHelpItem(Dgl1.Columns(ColumnIndex).Name)
        FCreateHelpItem(0)
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item  With (NoLock) Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemCode(ColumnIndex, RowIndex, DrTemp)
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

    Private Sub FShowTransactionHistory(ByVal ItemCode As String)
        If AgL.PubServerName = "" Then
            mQry = " SELECT L.Item, IfNull(H.VendorDocNo, H.ManualRefNo) as [Inv_No], H.V_Date AS [Inv_Date], Sg.DispName As Vendor, " &
                " L.Rate, L.Qty " &
                " FROM PurchInvoiceDetail L  With (NoLock) " &
                " LEFT JOIN  PurchInvoice H  With (NoLock) ON L.DocId = H.DocId " &
                " LEFT JOIN SubGroup Sg  With (NoLock) ON H.Vendor = Sg.SubCode " &
                " Left Join Voucher_Type Vt  With (NoLock) on H.V_Type = Vt.V_Type " &
                " Where NCat = '" & AgLibrary.ClsMain.agConstants.Ncat.PurchaseInvoice & "' And L.Item = '" & ItemCode & "'" &
                " And H.DocId <> '" & mSearchCode & "'" &
                " ORDER BY H.V_Date DESC Limit 5"
        Else
            mQry = " SELECT Top 5 L.Item, IfNull(H.VendorDocNo, H.ManualRefNo) as [Inv_No], H.V_Date AS [Inv_Date], Sg.DispName As Vendor, " &
                " L.Rate, L.Qty " &
                " FROM PurchInvoiceDetail L  With (NoLock)  " &
                " LEFT JOIN  PurchInvoice H  With (NoLock) ON L.DocId = H.DocId " &
                " LEFT JOIN SubGroup Sg  With (NoLock) ON H.Vendor = Sg.SubCode " &
                " Left Join Voucher_Type Vt  With (NoLock) on H.V_Type = Vt.V_Type " &
                " Where  NCat = '" & AgLibrary.ClsMain.agConstants.Ncat.PurchaseInvoice & "' And  L.Item = '" & ItemCode & "'" &
                " And H.DocId <> '" & mSearchCode & "'" &
                " ORDER BY H.V_Date DESC "
        End If
        FGetTransactionHistory(Me, mSearchCode, mQry, DGL, DtV_TypeSettings, ItemCode)
    End Sub

    Private Sub BtnHeaderDetail_Click(sender As Object, e As EventArgs) Handles BtnHeaderDetail.Click
        ShowPurchInvoiceHeader()
    End Sub


    Private Sub Dgl1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        Dim mRow As Integer
        mRow = e.RowIndex
        If Dgl1.Columns(e.ColumnIndex).Name = Col1DocQty Then ShowPurchInvoiceDimensionDetail(mRow)
    End Sub

    Private Sub ShowPurchInvoiceDimensionDetail(mRow As Integer)
        If mRow < 0 Then Exit Sub
        If Dgl1.Item(Col1DocQty, mRow).Tag IsNot Nothing Then
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmPurchaseInvoiceDimension).EntryMode = Topctrl1.Mode
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmPurchaseInvoiceDimension).objFrmPurchInvoice = Me
            Dgl1.Item(Col1DocQty, mRow).Tag.ShowDialog()
            Dgl1.Item(Col1DocQty, mRow).Value = CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmPurchaseInvoiceDimension).GetTotalQty
            Dgl1.Item(Col1Qty, mRow).Value = CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmPurchaseInvoiceDimension).GetTotalQty
        Else
            If Dgl1.Item(Col1Unit, mRow).Tag Then
                Dim FrmObj As FrmPurchaseInvoiceDimension
                FrmObj = New FrmPurchaseInvoiceDimension
                FrmObj.ItemName = Dgl1.Item(Col1Item, mRow).Value
                FrmObj.Unit = Dgl1.Item(Col1Unit, mRow).Value
                FrmObj.UnitDecimalPlace = Val(Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value)
                FrmObj.IniGrid(mSearchCode, Val(Dgl1.Item(ColSNo, mRow).Tag))
                FrmObj.EntryMode = Topctrl1.Mode
                FrmObj.objFrmPurchInvoice = Me
                Dgl1.Item(Col1DocQty, mRow).Tag = FrmObj

                Dgl1.Item(Col1DocQty, mRow).Tag.ShowDialog()

                Dgl1.Item(Col1DocQty, mRow).Value = CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmPurchaseInvoiceDimension).GetTotalQty
                Dgl1.Item(Col1Qty, mRow).Value = CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmPurchaseInvoiceDimension).GetTotalQty
            End If
        End If
        Calculation()
    End Sub

    Private Sub ShowPurchInvoiceHeader(Optional ShowDialog As Boolean = True)
        If BtnHeaderDetail.Tag IsNot Nothing Then
            CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).PartyCode = TxtVendor.Tag
            CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).V_Type = LblV_Type.Tag
            CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).EntryMode = Topctrl1.Mode
            If ShowDialog Then BtnHeaderDetail.Tag.ShowDialog()
        Else
            Dim FrmObj As FrmPurchaseInvoiceHeader
            FrmObj = New FrmPurchaseInvoiceHeader
            FrmObj.PartyCode = TxtVendor.Tag
            FrmObj.V_Type = LblV_Type.Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.IniGrid(mSearchCode)
            BtnHeaderDetail.Tag = FrmObj
            If ShowDialog Then BtnHeaderDetail.Tag.ShowDialog()
        End If
        If Dgl1.AgHelpDataSet(Col1BaleNo) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1BaleNo).Dispose() : Dgl1.AgHelpDataSet(Col1BaleNo) = Nothing
        Dgl1.Focus()
        If CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowLrNo).Value <> "" Then
            BtnHeaderDetail.BackColor = Color.SkyBlue
        Else
            BtnHeaderDetail.BackColor = Color.Transparent
        End If

    End Sub
    Private Sub ShowPurchaseInvoiceParty(DocID As String, PartyCode As String, AcGroupNature As String, Optional ShowDialogForCash As Boolean = False)
        If BtnFillPartyDetail.Tag IsNot Nothing Then
            CType(BtnFillPartyDetail.Tag, FrmPurchaseInvoiceParty).EntryMode = Topctrl1.Mode
            BtnFillPartyDetail.Tag.ShowDialog()
        Else
            Dim FrmObj As FrmPurchaseInvoiceParty
            FrmObj = New FrmPurchaseInvoiceParty
            FrmObj.IniGrid(DocID, PartyCode, AcGroupNature)
            FrmObj.EntryMode = Topctrl1.Mode
            BtnFillPartyDetail.Tag = FrmObj
            If AcGroupNature.ToUpper = "CASH" And ShowDialogForCash Then
                BtnFillPartyDetail.Tag.ShowDialog()
            End If
        End If
    End Sub
    Private Sub BtnBarcode_Click(sender As Object, e As EventArgs) Handles BtnBarcode.Click
        Dim FrmObj As FrmPrintBarcode
        FrmObj = New FrmPrintBarcode()
        FrmObj.DocId = mSearchCode
        FrmObj.MdiParent = Me.MdiParent
        FrmObj.Show()
    End Sub
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuEditSave.Click, MnuGenerateEWayBill.Click, MnuRequestForPermission.Click, MnuReferenceEntries.Click, MnuHistory.Click, MnuShowLedgerPosting.Click
        Select Case sender.name
            Case MnuImportFromExcel.Name
                FImportFromExcel(ImportFor.Excel)

            Case MnuImportFromDos.Name
                FImportFromExcel(ImportFor.Dos)

            Case MnuImportFromTally.Name
                FImportFromTally()

            Case MnuEditSave.Name
                FEditSaveAllEntries()

            Case MnuRequestForPermission.Name
                FRequestForPermission(EntryAction.Edit)

            Case MnuReferenceEntries.Name
                FShowRefrentialEntries(mSearchCode)

            Case MnuHistory.Name
                FShowHistory(mSearchCode)

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

            Case MnuShowLedgerPosting.Name
                FShowLedgerPosting()
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

        mFlag_Import = True

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim PurchInvoiceElementList As XmlNodeList = doc.GetElementsByTagName("VOUCHER")

            For I = 0 To PurchInvoiceElementList.Count - 1
                Dim PurchInvoiceTableList(0) As StructPurchInvoice
                If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST") IsNot Nothing Then
                    For J = 0 To PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Count - 1
                        Dim PurchInvoiceTable As New StructPurchInvoice

                        PurchInvoiceTable.DocID = ""

                        If PurchInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes.Count > 0 Then
                                If PurchInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "TAX INVOICE(PURCHASE)" Then
                                    PurchInvoiceTable.V_Type = "PI"
                                ElseIf PurchInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Purchase" Then
                                    PurchInvoiceTable.V_Type = "PI"
                                ElseIf PurchInvoiceElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Debit Note" Then
                                    PurchInvoiceTable.V_Type = "PR"
                                End If
                            End If
                        End If


                        PurchInvoiceTable.V_Prefix = ""
                        PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                        PurchInvoiceTable.Div_Code = AgL.PubDivCode








                        If PurchInvoiceElementList(I).SelectSingleNode("VOUCHERNUMBER") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.V_No = PurchInvoiceElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes(0).Value.Replace("G", "")
                            End If
                        End If

                        If PurchInvoiceElementList(I).SelectSingleNode("DATE") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectSingleNode("DATE").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.V_Date = PurchInvoiceElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(6, 2) + "/" +
                                        PurchInvoiceElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(4, 2) + "/" +
                                        PurchInvoiceElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(0, 4)
                            End If
                        End If



                        Dim mManualrefNoPrefix As String = AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix  With (NoLock) Where V_Type = '" & PurchInvoiceTable.V_Type & "' 
                                And " & AgL.Chk_Date(PurchInvoiceTable.V_Date) & " >= Date(Date_From) 
                                And " & AgL.Chk_Date(PurchInvoiceTable.V_Date) & " <= Date(Date_To) ", AgL.GCn).ExecuteScalar()
                        PurchInvoiceTable.ManualRefNo = mManualrefNoPrefix + PurchInvoiceTable.V_No.ToString().PadLeft(4).Replace(" ", "0")



                        PurchInvoiceTable.Vendor = ""
                        PurchInvoiceTable.AgentCode = ""
                        PurchInvoiceTable.AgentName = ""

                        If PurchInvoiceElementList(I).SelectSingleNode("PARTYLEDGERNAME") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectSingleNode("PARTYLEDGERNAME").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.VendorName = PurchInvoiceElementList(I).SelectSingleNode("PARTYLEDGERNAME").ChildNodes(0).Value
                            End If
                        End If

                        PurchInvoiceTable.BillToPartyCode = ""
                        PurchInvoiceTable.BillToPartyName = PurchInvoiceTable.VendorName

                        PurchInvoiceTable.VendorAddress = ""
                        PurchInvoiceTable.VendorCity = ""
                        PurchInvoiceTable.VendorMobile = ""
                        PurchInvoiceTable.VendorSalesTaxNo = ""


                        If PurchInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes.Count > 0 Then
                                If PurchInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value = "Regular" Then
                                    PurchInvoiceTable.SalesTaxGroupParty = "Registered"
                                Else
                                    PurchInvoiceTable.SalesTaxGroupParty = PurchInvoiceElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value
                                End If
                            End If
                        End If



                        PurchInvoiceTable.StructureCode = ""
                        PurchInvoiceTable.CustomFields = ""

                        If PurchInvoiceElementList(I).SelectSingleNode("REFERENCE") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectSingleNode("REFERENCE").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.VendorDocNo = PurchInvoiceElementList(I).SelectSingleNode("REFERENCE").ChildNodes(0).Value
                            End If
                        End If

                        If PurchInvoiceElementList(I).SelectSingleNode("REFERENCEDATE") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectSingleNode("REFERENCEDATE").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.VendorDocDate = PurchInvoiceElementList(I).SelectSingleNode("REFERENCEDATE").ChildNodes(0).Value.ToString.Substring(6, 2) + "/" +
                                        PurchInvoiceElementList(I).SelectSingleNode("REFERENCEDATE").ChildNodes(0).Value.ToString.Substring(4, 2) + "/" +
                                        PurchInvoiceElementList(I).SelectSingleNode("REFERENCEDATE").ChildNodes(0).Value.ToString.Substring(0, 4)
                            End If
                        End If


                        PurchInvoiceTable.ReferenceDocId = ""
                        PurchInvoiceTable.Remarks = ""

                        PurchInvoiceTable.Status = "Active"
                        PurchInvoiceTable.EntryBy = AgL.PubUserName
                        PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                        PurchInvoiceTable.ApproveBy = ""
                        PurchInvoiceTable.ApproveDate = ""
                        PurchInvoiceTable.MoveToLog = ""
                        PurchInvoiceTable.MoveToLogDate = ""
                        PurchInvoiceTable.UploadDate = ""
                        PurchInvoiceTable.Line_Sr = J + 1



                        If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("STOCKITEMNAME") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("STOCKITEMNAME").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.Line_ItemName = PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("STOCKITEMNAME").ChildNodes(0).Value
                            End If
                        End If

                        PurchInvoiceTable.Line_Specification = ""



                        If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.Line_DocQty = PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY").ChildNodes(0).Value.ToString()
                            End If
                        End If

                        PurchInvoiceTable.Line_FreeQty = 0

                        If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.Line_Qty = PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes(0).Value.ToString()

                                Dim bUnitName As String = PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes(0).Value.ToString()
                                If bUnitName.Contains("MTR") Then
                                    PurchInvoiceTable.Line_Unit = "Meter"
                                ElseIf bUnitName.Contains("PCS") Then
                                    PurchInvoiceTable.Line_Unit = "Pcs"
                                End If
                            End If
                        End If

                        If PurchInvoiceTable.Line_DocQty Is Nothing Or Val(PurchInvoiceTable.Line_DocQty) = 0 Then
                            PurchInvoiceTable.Line_DocQty = PurchInvoiceTable.Line_Qty
                        End If


                        PurchInvoiceTable.Line_Pcs = PurchInvoiceTable.Line_DocQty
                        PurchInvoiceTable.Line_UnitMultiplier = 1
                        PurchInvoiceTable.Line_DealUnit = ""
                        PurchInvoiceTable.Line_DocDealQty = PurchInvoiceTable.Line_DocQty

                        If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.Line_Rate = PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE").ChildNodes(0).Value
                            End If
                        End If

                        PurchInvoiceTable.Line_DiscountPer = 0
                        PurchInvoiceTable.Line_DiscountAmount = 0

                        If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("DISCOUNT") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("DISCOUNT").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.Line_DiscountPer = PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("DISCOUNT").ChildNodes(0).Value
                                PurchInvoiceTable.Line_DiscountAmount = Math.Round(Val(PurchInvoiceTable.Line_Qty) * Val(PurchInvoiceTable.Line_Rate) * PurchInvoiceTable.Line_DiscountPer / 100, 2)
                            End If
                        End If


                        PurchInvoiceTable.Line_AdditionalDiscountPer = 0
                        PurchInvoiceTable.Line_AdditionalDiscountAmount = 0

                        If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT") IsNot Nothing Then
                            If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes.Count > 0 Then
                                PurchInvoiceTable.Line_Amount = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            End If
                        End If


                        PurchInvoiceTable.Line_Remark = ""
                        PurchInvoiceTable.Line_BaleNo = ""
                        PurchInvoiceTable.Line_LotNo = ""
                        PurchInvoiceTable.Line_ReferenceDocId = ""
                        PurchInvoiceTable.Line_ReferenceSr = ""
                        PurchInvoiceTable.Line_ReferenceTSr = ""
                        PurchInvoiceTable.Line_PurchInvoice = ""
                        PurchInvoiceTable.Line_PurchInvoiceSr = ""
                        PurchInvoiceTable.Line_GrossWeight = 0
                        PurchInvoiceTable.Line_NetWeight = 0





                        If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST") IsNot Nothing Then
                            For K As Integer = 0 To PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST").Count
                                If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K) IsNot Nothing Then
                                    If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME") IsNot Nothing Then
                                        If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes.Count > 0 Then
                                            If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                    PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("5") Then
                                                PurchInvoiceTable.Line_Tax1_Per = 5
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                    PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("2.5") Then
                                                PurchInvoiceTable.Line_Tax2_Per = 2.5
                                                PurchInvoiceTable.Line_Tax3_Per = 2.5
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                    PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("12") Then
                                                PurchInvoiceTable.Line_Tax1_Per = 12
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                    PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("6") Then
                                                PurchInvoiceTable.Line_Tax2_Per = 6
                                                PurchInvoiceTable.Line_Tax3_Per = 6
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                    PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("18") Then
                                                PurchInvoiceTable.Line_Tax1_Per = 18
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                    PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("9") Then
                                                PurchInvoiceTable.Line_Tax2_Per = 9
                                                PurchInvoiceTable.Line_Tax3_Per = 9
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                    PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("28") Then
                                                PurchInvoiceTable.Line_Tax1_Per = 28
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                    PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("14") Then
                                                PurchInvoiceTable.Line_Tax2_Per = 14
                                                PurchInvoiceTable.Line_Tax3_Per = 14
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "FRGT INWARD FRM UNRAGISTER" Then
                                                If PurchInvoiceTable.Line_ItemName = "" Then
                                                    If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT") IsNot Nothing Then
                                                        PurchInvoiceTable.Line_ItemName = "FRGT INWARD FRM UNRAGISTER"
                                                        PurchInvoiceTable.Line_Amount = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                        PurchInvoiceTable.Line_Rate = PurchInvoiceTable.Line_Amount
                                                        PurchInvoiceTable.Line_DocQty = 1
                                                        PurchInvoiceTable.Line_Qty = 1
                                                        PurchInvoiceTable.Line_Unit = "Pcs"
                                                    End If
                                                End If
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "FRIEGHT INWARD FROM UNRAGITER" Then
                                                If PurchInvoiceTable.Line_ItemName = "" Then
                                                    If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT") IsNot Nothing Then
                                                        PurchInvoiceTable.Line_ItemName = "FRIEGHT INWARD FROM UNRAGITER"
                                                        PurchInvoiceTable.Line_Amount = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                        PurchInvoiceTable.Line_Rate = PurchInvoiceTable.Line_Amount
                                                        PurchInvoiceTable.Line_DocQty = 1
                                                        PurchInvoiceTable.Line_Qty = 1
                                                        PurchInvoiceTable.Line_Unit = "Pcs"
                                                    End If
                                                End If
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "ELECTRICITY EXP." Then
                                                If PurchInvoiceTable.Line_ItemName = "" Then
                                                    If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT") IsNot Nothing Then
                                                        PurchInvoiceTable.Line_ItemName = "ELECTRICITY EXP."
                                                        PurchInvoiceTable.Line_Amount = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                        PurchInvoiceTable.Line_Rate = PurchInvoiceTable.Line_Amount
                                                        PurchInvoiceTable.Line_DocQty = 1
                                                        PurchInvoiceTable.Line_Qty = 1
                                                        PurchInvoiceTable.Line_Unit = "Pcs"
                                                    End If
                                                End If
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "BANK CHARGE" + ControlChars.Quote + "S" Then
                                                If PurchInvoiceTable.Line_ItemName = "" Then
                                                    If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT") IsNot Nothing Then
                                                        PurchInvoiceTable.Line_ItemName = "BANK CHARGE" + ControlChars.Quote + "S"
                                                        PurchInvoiceTable.Line_Amount = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                        PurchInvoiceTable.Line_Rate = PurchInvoiceTable.Line_Amount
                                                        PurchInvoiceTable.Line_DocQty = 1
                                                        PurchInvoiceTable.Line_Qty = 1
                                                        PurchInvoiceTable.Line_Unit = "Pcs"
                                                    End If
                                                End If
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "CASH DISCOUNT" Then
                                                If J = 0 Then
                                                    If PurchInvoiceTable.Line_DiscountAmount = 0 Then
                                                        PurchInvoiceTable.Line_DiscountAmount = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                        PurchInvoiceTable.Line_Amount = PurchInvoiceTable.Line_Amount - PurchInvoiceTable.Line_DiscountAmount
                                                    Else
                                                        PurchInvoiceTable.Line_DiscountAmount = PurchInvoiceTable.Line_DiscountAmount + Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                        PurchInvoiceTable.Line_Amount = PurchInvoiceTable.Line_Amount - Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                    End If
                                                End If
                                            ElseIf PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "ROUND OFF" Then
                                                If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT") IsNot Nothing Then
                                                    PurchInvoiceTable.Round_Off = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If


                        If Val(PurchInvoiceTable.Line_Qty) = 0 And Val(PurchInvoiceTable.Line_Rate) = 0 And Val(PurchInvoiceTable.Line_Amount) <> 0 Then
                            PurchInvoiceTable.Line_Qty = 1
                            PurchInvoiceTable.Line_Rate = PurchInvoiceTable.Line_Amount
                        End If

                        If PurchInvoiceTable.Line_DocQty Is Nothing Or Val(PurchInvoiceTable.Line_DocQty) = 0 Then
                            PurchInvoiceTable.Line_DocQty = PurchInvoiceTable.Line_Qty
                        End If

                        PurchInvoiceTable.Line_Gross_Amount = PurchInvoiceTable.Line_Amount
                        PurchInvoiceTable.Line_Taxable_Amount = PurchInvoiceTable.Line_Amount

                        If PurchInvoiceTable.Line_Tax1_Per = 5 Or PurchInvoiceTable.Line_Tax2_Per = 2.5 Then
                            PurchInvoiceTable.Line_SalesTaxGroupItem = "GST 5%"
                        ElseIf PurchInvoiceTable.Line_Tax1_Per = 12 Or PurchInvoiceTable.Line_Tax2_Per = 6 Then
                            PurchInvoiceTable.Line_SalesTaxGroupItem = "GST 12%"
                        ElseIf PurchInvoiceTable.Line_Tax1_Per = 18 Or PurchInvoiceTable.Line_Tax2_Per = 9 Then
                            PurchInvoiceTable.Line_SalesTaxGroupItem = "GST 18%"
                        ElseIf PurchInvoiceTable.Line_Tax1_Per = 28 Or PurchInvoiceTable.Line_Tax2_Per = 14 Then
                            PurchInvoiceTable.Line_SalesTaxGroupItem = "GST 28%"
                        End If

                        If PurchInvoiceTable.Line_Tax1_Per > 0 Then
                            PurchInvoiceTable.PlaceOfSupply = AgLibrary.ClsMain.agConstants.PlaceOfSupplay.OutsideState
                        Else
                            PurchInvoiceTable.PlaceOfSupply = AgLibrary.ClsMain.agConstants.PlaceOfSupplay.WithinState
                        End If

                        If PurchInvoiceTable.Line_Tax1_Per > 0 Or PurchInvoiceTable.Line_Tax2_Per > 0 Or PurchInvoiceTable.Line_Tax3_Per > 0 Then
                            PurchInvoiceTable.SalesTaxGroupParty = "Registered"
                        Else
                            PurchInvoiceTable.SalesTaxGroupParty = "Unregistered"
                        End If

                        If PurchInvoiceTable.Line_Unit = "" Or PurchInvoiceTable.Line_Unit Is Nothing Then
                            PurchInvoiceTable.Line_Unit = "Pcs"
                        End If

                        'If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST") IsNot Nothing Then
                        '    If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST") IsNot Nothing Then
                        '        If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1) IsNot Nothing Then
                        '            If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE") IsNot Nothing Then
                        '                If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE").ChildNodes.Count > 0 Then
                        '                    PurchInvoiceTable.Line_Tax2_Per = PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE").ChildNodes(0).Value
                        '                End If
                        '            End If
                        '        End If
                        '    End If
                        'End If



                        'If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST") IsNot Nothing Then
                        '    If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST") IsNot Nothing Then
                        '        If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1) IsNot Nothing Then
                        '            If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE") IsNot Nothing Then
                        '                If PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE").ChildNodes.Count > 0 Then
                        '                    PurchInvoiceTable.Line_Tax3_Per = PurchInvoiceElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectNodes("RATEDETAILS.LIST")(1).SelectSingleNode("GSTRATE").ChildNodes(0).Value
                        '                End If
                        '            End If
                        '        End If
                        '    End If
                        'End If

                        PurchInvoiceTable.Line_Tax1 = Math.Round(PurchInvoiceTable.Line_Taxable_Amount * PurchInvoiceTable.Line_Tax1_Per / 100, 2)
                        PurchInvoiceTable.Line_Tax2 = Math.Round(PurchInvoiceTable.Line_Taxable_Amount * PurchInvoiceTable.Line_Tax2_Per / 100, 2)
                        PurchInvoiceTable.Line_Tax3 = Math.Round(PurchInvoiceTable.Line_Taxable_Amount * PurchInvoiceTable.Line_Tax3_Per / 100, 2)


                        PurchInvoiceTable.Line_Tax4_Per = 0
                        PurchInvoiceTable.Line_Tax4 = 0
                        PurchInvoiceTable.Line_Tax5_Per = 0
                        PurchInvoiceTable.Line_Tax5 = 0
                        PurchInvoiceTable.Line_SubTotal1 = PurchInvoiceTable.Line_Taxable_Amount + PurchInvoiceTable.Line_Tax1 + PurchInvoiceTable.Line_Tax2 + PurchInvoiceTable.Line_Tax3 + PurchInvoiceTable.Line_Tax4 + PurchInvoiceTable.Line_Tax5
                        PurchInvoiceTable.Line_Deduction_Per = 0
                        PurchInvoiceTable.Line_Deduction = 0
                        PurchInvoiceTable.Line_Other_Charge_Per = 0
                        PurchInvoiceTable.Line_Other_Charge = 0
                        PurchInvoiceTable.Line_Round_Off = 0
                        PurchInvoiceTable.Line_Net_Amount = PurchInvoiceTable.Line_SubTotal1


                        PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                        ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                    Next

                    For J = 0 To PurchInvoiceTableList.Length - 1
                        PurchInvoiceTableList(0).Gross_Amount += PurchInvoiceTableList(J).Line_Gross_Amount
                        PurchInvoiceTableList(0).Taxable_Amount += PurchInvoiceTableList(J).Line_Taxable_Amount
                        PurchInvoiceTableList(0).Tax1_Per += 0
                        PurchInvoiceTableList(0).Tax1 += PurchInvoiceTableList(J).Line_Tax1
                        PurchInvoiceTableList(0).Tax2_Per += 0
                        PurchInvoiceTableList(0).Tax2 += PurchInvoiceTableList(J).Line_Tax2
                        PurchInvoiceTableList(0).Tax3_Per += 0
                        PurchInvoiceTableList(0).Tax3 += PurchInvoiceTableList(J).Line_Tax3
                        PurchInvoiceTableList(0).Tax4_Per += 0
                        PurchInvoiceTableList(0).Tax4 += PurchInvoiceTableList(J).Line_Tax4
                        PurchInvoiceTableList(0).Tax5_Per += 0
                        PurchInvoiceTableList(0).Tax5 += PurchInvoiceTableList(J).Line_Tax5
                        PurchInvoiceTableList(0).SubTotal1 += PurchInvoiceTableList(J).Line_SubTotal1
                        PurchInvoiceTableList(0).Deduction_Per += 0
                        PurchInvoiceTableList(0).Deduction += PurchInvoiceTableList(J).Line_Deduction
                        PurchInvoiceTableList(0).Other_Charge_Per += 0
                        PurchInvoiceTableList(0).Other_Charge += PurchInvoiceTableList(J).Line_Other_Charge
                        'PurchInvoiceTableList(0).Round_Off = 0
                        'PurchInvoiceTableList(0).Net_Amount += PurchInvoiceTableList(J).Line_Net_Amount
                    Next

                    PurchInvoiceTableList(0).Net_Amount = PurchInvoiceTableList(0).SubTotal1 -
                            PurchInvoiceTableList(0).Deduction +
                            PurchInvoiceTableList(0).Other_Charge + PurchInvoiceTableList(0).Round_Off

                    Dim mTallyNetAmount As Double = 0
                    If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST") IsNot Nothing Then
                        For J = 0 To PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST").Count - 1
                            If PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST").Item(J).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = PurchInvoiceTableList(0).VendorName Then
                                mTallyNetAmount = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            End If
                        Next
                    End If

                    If mTallyNetAmount > PurchInvoiceTableList(0).Net_Amount Then
                        PurchInvoiceTableList(0).Other_Charge += Math.Round(mTallyNetAmount - PurchInvoiceTableList(0).Net_Amount, 2)
                    ElseIf mTallyNetAmount < PurchInvoiceTableList(0).Net_Amount Then
                        PurchInvoiceTableList(0).Deduction += Math.Round(PurchInvoiceTableList(0).Net_Amount - mTallyNetAmount, 2)
                    End If

                    PurchInvoiceTableList(0).Net_Amount = Math.Round(PurchInvoiceTableList(0).Net_Amount + PurchInvoiceTableList(0).Other_Charge - PurchInvoiceTableList(0).Deduction, 2)


                    InsertPurchInvoice(PurchInvoiceTableList)
                End If
            Next I
            AgL.ETrans.Commit()
            mTrans = "Commit"
            mFlag_Import = False
        Catch ex As Exception
            AgL.ETrans.Rollback()
            mFlag_Import = False
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

    Public Shared Function InsertPurchInvoice(PurchInvoiceTableList As StructPurchInvoice(), Optional PurchInvoiceDimensionTableList As StructPurchInvoiceDimensionDetail() = Nothing) As String
        Dim mQry As String = ""

        If PurchInvoiceTableList(0).V_Date IsNot Nothing Then
            'PurchInvoiceTableList(0).DocID = AgL.GetDocId(PurchInvoiceTableList(0).V_Type, CStr(PurchInvoiceTableList(0).V_No),
            '                                         CDate(PurchInvoiceTableList(0).V_Date),
            '                                        IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), PurchInvoiceTableList(0).Div_Code, PurchInvoiceTableList(0).Site_Code)
            PurchInvoiceTableList(0).DocID = AgL.CreateDocId(AgL, "PurchInvoice", PurchInvoiceTableList(0).V_Type, CStr(PurchInvoiceTableList(0).V_No),
                                                     CDate(PurchInvoiceTableList(0).V_Date),
                                                    IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), PurchInvoiceTableList(0).Div_Code, PurchInvoiceTableList(0).Site_Code)

            PurchInvoiceTableList(0).V_Prefix = AgL.DeCodeDocID(PurchInvoiceTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            PurchInvoiceTableList(0).V_No = Val(AgL.DeCodeDocID(PurchInvoiceTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))

            'If AgL.Dman_Execute("Select Count(*) From PurchInvoice With (NoLock) Where V_Type = '" & PurchInvoiceTableList(0).V_Type & "'
            '            And ManualRefNo = '" & PurchInvoiceTableList(0).ManualRefNo & "'
            '            And Div_Code = '" & PurchInvoiceTableList(0).Div_Code & "'
            '            And Site_Code = '" & PurchInvoiceTableList(0).Site_Code & "'
            '            And V_Prefix = '" & PurchInvoiceTableList(0).V_Prefix & "'
            '                ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() > 0 Or
            '            PurchInvoiceTableList(0).ManualRefNo = "" Then
            '    Dim mManualrefNoPrefix As String = AgL.XNull(AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix Where V_Type = '" & PurchInvoiceTableList(0).V_Type & "' 
            '                    And " & AgL.Chk_Date(PurchInvoiceTableList(0).V_Date) & " >= Date(Date_From) 
            '                    And " & AgL.Chk_Date(PurchInvoiceTableList(0).V_Date) & " <= Date(Date_To) ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            '    PurchInvoiceTableList(0).ManualRefNo = mManualrefNoPrefix + PurchInvoiceTableList(0).V_No.ToString().PadLeft(4).Replace(" ", "0")
            'End If

            If PurchInvoiceTableList(0).ManualRefNo = "" Then
                PurchInvoiceTableList(0).ManualRefNo = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchInvoice",
                                PurchInvoiceTableList(0).V_Type, PurchInvoiceTableList(0).V_Date,
                                PurchInvoiceTableList(0).Div_Code, PurchInvoiceTableList(0).Site_Code,
                                AgTemplate.ClsMain.ManualRefType.Max)
            End If


            Dim DtSubGroup As DataTable = Nothing
            If AgL.XNull(PurchInvoiceTableList(0).Vendor) <> "" Then
                mQry = "SELECT Sg.SubCode As Vendor, Name As VendorName, Address As VendorAddress, CityCode As VendorCity, Mobile As VendorMobile, Sgr.RegistrationNo As SaleToPartySalesTaxNo
                        FROM Subgroup Sg
                        left join (Select SubCode, RegistrationNo From SubgroupRegistration Where RegistrationType = 'Sales Tax No') As Sgr On Sg.Subcode = Sgr.Subcode
                        Where Sg.SubCode =  " & AgL.Chk_Text(PurchInvoiceTableList(0).Vendor) & ""
                DtSubGroup = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
            ElseIf PurchInvoiceTableList(0).VendorName <> "" Then
                mQry = "SELECT Sg.SubCode As Vendor, Name As VendorName, Address As VendorAddress, CityCode As VendorCity, Mobile As VendorMobile, Sgr.RegistrationNo As SaleToPartySalesTaxNo
                        FROM Subgroup Sg
                        left join (Select SubCode, RegistrationNo From SubgroupRegistration Where RegistrationType = 'Sales Tax No') As Sgr On Sg.Subcode = Sgr.Subcode
                        Where Upper(RTrim(LTrim(Sg.Name)))  =  " & AgL.Chk_Text(PurchInvoiceTableList(0).VendorName.ToString().Trim().ToUpper) & ""
                DtSubGroup = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
            End If
            If DtSubGroup IsNot Nothing Then
                If (DtSubGroup.Rows.Count > 0) Then
                    PurchInvoiceTableList(0).Vendor = AgL.XNull(DtSubGroup.Rows(0)("Vendor"))
                    PurchInvoiceTableList(0).VendorName = AgL.XNull(DtSubGroup.Rows(0)("VendorName"))
                    If PurchInvoiceTableList(0).VendorAddress = "" Then PurchInvoiceTableList(0).VendorAddress = AgL.XNull(DtSubGroup.Rows(0)("VendorAddress"))
                    If PurchInvoiceTableList(0).VendorCity = "" Then PurchInvoiceTableList(0).VendorCity = AgL.XNull(DtSubGroup.Rows(0)("VendorCity"))
                    If PurchInvoiceTableList(0).VendorMobile = "" Then PurchInvoiceTableList(0).VendorMobile = AgL.XNull(DtSubGroup.Rows(0)("VendorMobile"))
                    If PurchInvoiceTableList(0).VendorSalesTaxNo = "" Then PurchInvoiceTableList(0).VendorSalesTaxNo = AgL.XNull(DtSubGroup.Rows(0)("SaleToPartySalesTaxNo"))
                End If
            End If

            If PurchInvoiceTableList(0).Vendor <> "" Then
                If AgL.XNull(AgL.Dman_Execute("Select SubGRoupType From SubGroup With (NoLock) Where SubCode = '" & PurchInvoiceTableList(0).Vendor & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = "" Then
                    mQry = "UPDATE SubGroup Set SubGroupType = '" & SubgroupType.Supplier & "' Where SubCode = '" & PurchInvoiceTableList(0).Vendor & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If




            If PurchInvoiceTableList(0).BillToPartyCode = "" Then
                PurchInvoiceTableList(0).BillToPartyCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg Where Sg.Name =  '" & PurchInvoiceTableList(0).BillToPartyName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If PurchInvoiceTableList(0).BillToPartyCode = "" Or PurchInvoiceTableList(0).BillToPartyCode Is Nothing Then
                PurchInvoiceTableList(0).BillToPartyCode = PurchInvoiceTableList(0).Vendor
            End If

            If PurchInvoiceTableList(0).AgentCode = "" Then
                PurchInvoiceTableList(0).AgentCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  '" & PurchInvoiceTableList(0).AgentName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If PurchInvoiceTableList(0).AgentCode <> "" Then
                If AgL.XNull(AgL.Dman_Execute("Select SubGroupType From SubGroup With (NoLock) Where SubCode = '" & PurchInvoiceTableList(0).AgentCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = "" Then
                    mQry = "UPDATE SubGroup Set SubGroupType = '" & SubgroupType.PurchaseAgent & "' Where SubCode = '" & PurchInvoiceTableList(0).AgentCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If

            If PurchInvoiceTableList(0).StructureCode = "" Then
                PurchInvoiceTableList(0).StructureCode = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type With (NoLock) Where V_Type = '" & PurchInvoiceTableList(0).V_Type & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If PurchInvoiceTableList(0).SalesTaxGroupParty Is Nothing Or PurchInvoiceTableList(0).SalesTaxGroupParty = "" Then
                PurchInvoiceTableList(0).SalesTaxGroupParty = AgL.Dman_Execute("Select IfNull(SalesTaxPostingGroup,'') From Subgroup With (NoLock) Where SubCode = '" & PurchInvoiceTableList(0).BillToPartyCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If PurchInvoiceTableList(0).SalesTaxGroupParty Is Nothing Or PurchInvoiceTableList(0).SalesTaxGroupParty = "" Or
                PurchInvoiceTableList(0).SalesTaxGroupParty = "Unregistered" Then
            End If

            If PurchInvoiceTableList(0).VendorCity <> "" Then
                PurchInvoiceTableList(0).VendorCity = AgL.Dman_Execute("SELECT CityCode From City With (NoLock) where CityName = '" & PurchInvoiceTableList(0).VendorCity & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            End If

            If PurchInvoiceTableList(0).PlaceOfSupply = "" Then
                PurchInvoiceTableList(0).PlaceOfSupply = PlaceOfSupplay.WithinState
            End If

            If PurchInvoiceTableList(0).VendorAddress.Length > 100 Then
                PurchInvoiceTableList(0).VendorAddress = PurchInvoiceTableList(0).VendorAddress.Substring(0, 99)
            End If

            If AgL.XNull(PurchInvoiceTableList(0).Vendor) = "" Then
                Err.Raise(1,, "Vendor is Empty.")
            End If
            If AgL.XNull(PurchInvoiceTableList(0).BillToPartyCode) = "" Then
                Err.Raise(1,, "Bill To Party is Empty.")
            End If

            'If AgL.Dman_Execute("SELECT Count(*) From PurchInvoice where V_Type = '" & PurchInvoiceTableList(0).V_Type & "' And ManualRefNo = '" & PurchInvoiceTableList(0).ManualRefNo & "' ", AgL.GCn).ExecuteScalar = 0 Then
            mQry = " INSERT INTO PurchInvoice (DocID,  V_Type,  V_Prefix, V_Date,  V_No,  Div_Code,  Site_Code,
                             ManualRefNo,  Vendor,  BillToParty,  Agent, VendorName,  VendorAddress,
                             VendorCity,  VendorMobile, 
                             SalesTaxGroupParty, PlaceOfSupply,  Structure,
                             CustomFields,  VendorDocNo, VendorDocDate, VendorSalesTaxNo,  ReferenceDocId, Tags,
                             Remarks, GenDocId, GenDocIdSr,  Gross_Amount,  Taxable_Amount,
                             Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
                             Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
                             Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount,  
                             Status, EntryBy,  EntryDate,  ApproveBy,
                             ApproveDate,  MoveToLog,  MoveToLogDate, UploadDate, LockText, OmsId)
                             Select  " & AgL.Chk_Text(PurchInvoiceTableList(0).DocID) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).V_Type) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).V_Prefix) & ",  
                             " & AgL.Chk_Date(PurchInvoiceTableList(0).V_Date) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).V_No) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).Div_Code) & ",
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).Site_Code) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).ManualRefNo) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).Vendor) & ", 
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).BillToPartyCode) & ", 
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).AgentCode) & ", 
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).VendorName) & ",
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).VendorAddress) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).VendorCity) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).VendorMobile) & ",                              
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).SalesTaxGroupParty) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).PlaceOfSupply) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).StructureCode) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).CustomFields) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).VendorDocNo) & ",  
                             " & AgL.Chk_Date(PurchInvoiceTableList(0).VendorDocDate) & ",
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).VendorSalesTaxNo) & ",  
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).ReferenceDocId) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).Tags) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).Remarks) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).GenDocId) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).GenDocIdSr) & ",    
                             " & Val(PurchInvoiceTableList(0).Gross_Amount) & ",    
                             " & Val(PurchInvoiceTableList(0).Taxable_Amount) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax1_Per) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax1) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax2_Per) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax2) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax3_Per) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax3) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax4_Per) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax4) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax5_Per) & ",    
                             " & Val(PurchInvoiceTableList(0).Tax5) & ",    
                             " & Val(PurchInvoiceTableList(0).SubTotal1) & ",    
                             " & Val(PurchInvoiceTableList(0).Deduction_Per) & ",    
                             " & Val(PurchInvoiceTableList(0).Deduction) & ",    
                             " & Val(PurchInvoiceTableList(0).Other_Charge_Per) & ",    
                             " & Val(PurchInvoiceTableList(0).Other_Charge) & ",    
                             " & Val(PurchInvoiceTableList(0).Round_Off) & ",    
                             " & Val(PurchInvoiceTableList(0).Net_Amount) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).Status) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).EntryBy) & ",    
                             " & AgL.Chk_Date(PurchInvoiceTableList(0).EntryDate) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).ApproveBy) & ",    
                             " & AgL.Chk_Date(PurchInvoiceTableList(0).ApproveDate) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).MoveToLog) & ",    
                             " & AgL.Chk_Date(PurchInvoiceTableList(0).MoveToLogDate) & ",    
                             " & AgL.Chk_Date(PurchInvoiceTableList(0).UploadDate) & ",
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).LockText) & ",    
                             " & AgL.Chk_Text(PurchInvoiceTableList(0).OmsId) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            For I As Integer = 0 To PurchInvoiceTableList.Length - 1
                If PurchInvoiceTableList(I).Line_ItemName IsNot Nothing Then
                    If PurchInvoiceTableList(I).Line_ItemCode = "" Or PurchInvoiceTableList(I).Line_ItemCode Is Nothing Then
                        PurchInvoiceTableList(I).Line_ItemCode = AgL.Dman_Execute("SELECT Code FROM Item Where Description =  " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_ItemName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                        If PurchInvoiceTableList(I).Line_ItemCode = "" Or PurchInvoiceTableList(I).Line_ItemCode Is Nothing Then
                            PurchInvoiceTableList(I).Line_ItemCode = AgL.Dman_Execute("SELECT Code FROM Item Where Specification =  " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_ItemName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                        End If
                    End If


                    If PurchInvoiceTableList(I).Line_ItemCode = "" Or PurchInvoiceTableList(I).Line_ItemCode Is Nothing Then
                        If PurchInvoiceTableList(I).Line_ItemName <> "" Then
                            Dim ItemTable As New FrmItemMaster.StructItem
                            Dim bItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                            Dim bManualCode As String = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) FROM Item  WHERE ABS(ManualCode)>0", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

                            ItemTable.Code = bItemCode
                            ItemTable.ManualCode = bManualCode
                            ItemTable.DisplayName = PurchInvoiceTableList(I).Line_ItemName
                            ItemTable.Specification = PurchInvoiceTableList(I).Line_ItemName
                            ItemTable.ItemGroupDesc = ""
                            ItemTable.ItemCategoryDesc = ""
                            ItemTable.Description = PurchInvoiceTableList(I).Line_ItemName
                            ItemTable.ItemType = "TP"
                            ItemTable.Unit = "Pcs"
                            ItemTable.PurchaseRate = 0
                            ItemTable.Rate = 0
                            ItemTable.SalesTaxPostingGroup = "GST 5%"
                            ItemTable.HSN = ""
                            ItemTable.EntryBy = AgL.PubUserName
                            ItemTable.EntryDate = AgL.GetDateTime(IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead))
                            ItemTable.EntryType = "Add"
                            ItemTable.EntryStatus = LogStatus.LogOpen
                            ItemTable.Div_Code = AgL.PubDivCode
                            ItemTable.Status = "Active"
                            ItemTable.StockYN = 0
                            ItemTable.IsSystemDefine = 0

                            Dim DTUP As DataTable = AgL.FillData("Select '' As [UP] ", AgL.GCn).Tables(0)
                            Dim FrmObj As New FrmItemMaster("", DTUP, ItemV_Type.Item)
                            FrmObj.ImportItemTable(ItemTable)
                        End If
                    End If

                    If PurchInvoiceTableList(I).Line_ItemCode = "" Then
                        Err.Raise(1,, "Item is saving blank in purch invoice detail for " & PurchInvoiceTableList(0).ManualRefNo)
                    End If

                    mQry = "Insert Into PurchInvoiceDetail(DocId, Sr, Item, Specification, SalesTaxGroupItem, 
                           DocQty, FreeQty, Qty, Unit, Pcs, UnitMultiplier, DealUnit, 
                           DocDealQty, Rate, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount,  
                           Amount, Remark, BaleNo, LotNo,  
                           ReferenceDocId, ReferenceTSr, ReferenceSr, 
                           PurchInvoice, PurchInvoiceSr, GrossWeight, NetWeight, OmsId, Gross_Amount, Taxable_Amount,
                           Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, 
                           Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount)
                           Select " & AgL.Chk_Text(PurchInvoiceTableList(0).DocID) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Sr) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_ItemCode) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_Specification) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_SalesTaxGroupItem) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_DocQty) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_FreeQty) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Qty) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_Unit) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Pcs) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_UnitMultiplier) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_DealUnit) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_DocDealQty) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Rate) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_DiscountPer) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_DiscountAmount) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_AdditionalDiscountPer) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_AdditionalDiscountAmount) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Amount) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_Remark) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_BaleNo) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_LotNo) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_ReferenceDocId) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_ReferenceTSr) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_ReferenceSr) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_PurchInvoice) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_PurchInvoiceSr) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_GrossWeight) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_NetWeight) & ", 
                            " & AgL.Chk_Text(PurchInvoiceTableList(I).Line_OmsId) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Gross_Amount) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Taxable_Amount) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax1_Per) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax1) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax2_Per) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax2) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax3_Per) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax3) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax4_Per) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax4) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax5_Per) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Tax5) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_SubTotal1) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Deduction_Per) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Deduction) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Other_Charge_Per) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Other_Charge) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Round_Off) & ", 
                            " & Val(PurchInvoiceTableList(I).Line_Net_Amount) & ""
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                End If
            Next

            If PurchInvoiceDimensionTableList IsNot Nothing Then
                For K As Integer = 0 To PurchInvoiceDimensionTableList.Length - 1
                    If Val(PurchInvoiceDimensionTableList(K).Qty) > 0 Then
                        mQry = " INSERT INTO SaleInvoiceDimensionDetail (DocID, TSr, SR, Specification, Pcs, Qty, TotalQty) 
                            Select " & AgL.Chk_Text(PurchInvoiceTableList(0).DocID) & ", 
                            " & Val(PurchInvoiceDimensionTableList(K).TSr) & " As TSr, 
                            " & Val(PurchInvoiceDimensionTableList(K).Sr) & " As Sr, 
                            " & AgL.Chk_Text(PurchInvoiceDimensionTableList(K).Specification) & ", 
                            " & Val(PurchInvoiceDimensionTableList(K).Pcs) & ", 
                            " & Val(PurchInvoiceDimensionTableList(K).Qty) & ", 
                            " & Val(PurchInvoiceDimensionTableList(K).TotalQty) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next
            End If

            mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                  SubCode, SalesTaxGroupParty,  Item,  LotNo, 
                                  EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                  ReferenceDocID, ReferenceDocIDSr, Rate, Amount, Landed_Value) 
                                  Select L.DocId, L.Sr, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, 
                                  H.Div_Code, H.Site_Code, H.Vendor,  H.SalesTaxGroupParty,  L.Item,
                                  L.LotNo, 'I', 
                                  Case When  IfNull(L.Qty,0) < 0 Then L.Qty Else 0 End As Qty_Iss, 
                                  Case When  IfNull(L.Qty,0) >= 0 Then L.Qty Else 0 End As Qty_Rec, 
                                  L.Unit, L.UnitMultiplier, 
                                  Case When  IfNull(L.DealQty,0) < 0 Then L.DealQty Else 0 End As DealQty_Iss, 
                                  Case When  IfNull(L.DealQty,0) >= 0 Then L.DealQty Else 0 End As DealQty_Rec, 
                                  L.DealUnit,  
                                  L.ReferenceDocId, L.ReferenceSr, 
                                  L.Amount/L.Qty, L.Amount, L.Amount
                                  FROM PurchInvoiceDetail L    
                                  LEFT JOIN PurchInvoice H On L.DocId = H.DocId 
                                  WHERE L.DocId =  '" & PurchInvoiceTableList(0).DocID & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If mFlag_Import = False Then
                FGetCalculationData(PurchInvoiceTableList(0).DocID, AgL.GCn, AgL.ECmd)
            End If
            AgL.UpdateVoucherCounter(PurchInvoiceTableList(0).DocID, CDate(PurchInvoiceTableList(0).V_Date), AgL.GCn, AgL.ECmd,
                                     PurchInvoiceTableList(0).Div_Code, PurchInvoiceTableList(0).Site_Code)
        End If
        Return PurchInvoiceTableList(0).DocID
    End Function

    Private Sub FGetCurrBal(ByVal Party As String)
        mQry = " Select IfNull(Sum(AmtDr),0) - IfNull(Sum(AmtCr),0) As CurrBal From Ledger  With (NoLock) Where SubCode = '" & Party & "'"
        LblCurrentBalance.Text = Format(AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar), "0.00")
        LblCurrentBalance.BackColor = Color.White
        If Val(LblCurrentBalance.Text) < 0 Then
            LblCurrentBalance.ForeColor = Color.Red
            LblCurrentBalance.Text = LblCurrentBalance.Text & " Cr."
        ElseIf Val(LblCurrentBalance.Text) < 0 Then
            LblCurrentBalance.ForeColor = Color.ForestGreen
            LblCurrentBalance.Text = LblCurrentBalance.Text & " Dr."
        Else
            LblCurrentBalance.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Dgl1_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellLeave
        'If e.ColumnIndex = Dgl1.Columns(Col1Item).Index Then
        '    If Dgl1.Item(Col1Item, e.RowIndex).Value = "" Then
        '        TxtAgent.Focus()
        '    End If
        'End If
    End Sub

    Public Structure StructPurchInvoice
        Dim DocID As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As String
        Dim Div_Code As String
        Dim Site_Code As String
        Dim ManualRefNo As String
        Dim Vendor As String
        Dim BillToPartyName As String
        Dim BillToPartyCode As String
        Dim SalesTaxGroupParty As String
        Dim PlaceOfSupply As String
        Dim StructureCode As String
        Dim CustomFields As String
        Dim VendorDocNo As String
        Dim VendorDocDate As String
        Dim ReferenceDocId As String
        Dim GenDocId As String
        Dim GenDocIdSr As String
        Dim Tags As String
        Dim Remarks As String
        Dim Process As String
        Dim AgentCode As String
        Dim AgentName As String
        Dim VendorName As String
        Dim VendorAddress As String
        Dim VendorCity As String
        Dim VendorMobile As String
        Dim VendorSalesTaxNo As String
        Dim Remarks1 As String
        Dim Remarks2 As String
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
        Dim Line_Sr As String
        Dim Line_ReferenceNo As String
        Dim Line_ItemName As String
        Dim Line_ItemCode As String
        Dim Line_Specification As String
        Dim Line_Dimension1 As String
        Dim Line_Dimension2 As String
        Dim Line_Dimension3 As String
        Dim Line_Dimension4 As String
        Dim Line_SalesTaxGroupItem As String
        Dim Line_LotNo As String
        Dim Line_BaleNo As String
        Dim Line_Deal As String
        Dim Line_ExpiryDate As String
        Dim Line_LrNo As String
        Dim Line_LrDate As String
        Dim Line_Pcs As String
        Dim Line_DocQty As String
        Dim Line_FreeQty As String
        Dim Line_Qty As String
        Dim Line_RejQty As String
        Dim Line_Unit As String
        Dim Line_UnitMultiplier As String
        Dim Line_DocDealQty As String
        Dim Line_DealQty As String
        Dim Line_RejDealQty As String
        Dim Line_DealUnit As String
        Dim Line_Rate As String
        Dim Line_MRP As String
        Dim Line_DiscountPer As String
        Dim Line_DiscountAmount As String
        Dim Line_AdditionalDiscountPer As String
        Dim Line_AdditionalDiscountAmount As String
        Dim Line_Amount As String
        Dim Line_ProfitMarginPer As String
        Dim Line_Sale_Rate As String
        Dim Line_ReferenceDocId As String
        Dim Line_ReferenceTSr As String
        Dim Line_ReferenceSr As String
        Dim Line_PurchInvoice As String
        Dim Line_PurchInvoiceSr As String
        Dim Line_Godown As String
        Dim Line_Remark As String
        Dim Line_GrossWeight As Double
        Dim Line_NetWeight As Double
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
        Dim Line_UploadDate As String
        Dim Line_Barcode As String
        Dim Line_Remarks1 As String
        Dim Line_Remarks2 As String
        Dim Line_OmsId As String
    End Structure
    Public Structure StructPurchInvoiceDimensionDetail
        Dim TSr As Integer
        Dim Sr As Integer
        Dim Specification As String
        Dim Pcs As Integer
        Dim Qty As Double
        Dim TotalQty As Double
    End Structure

    Private Sub FCreateJSONFile()
        mQry = "Select H.ManualRefNo, H.V_Date, I.Description As ItemDesc, I.Specification As ItemSpecification, 
                Sg.DispName As SaleToPartyName, H.VendorAddress As SaleToPartyAddress, H.VendorPinCode As SaleToPartyPinCode,
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
                From PurchInvoice H  With (NoLock)
                LEFT JOIN City C  With (NoLock) On H.VendorCity = C.CityCode
                LEFT JOIN State S  With (NoLock) On C.State = S.Code
                LEFT JOIN SubGroup Sg  With (NoLock) On H.Vendor = Sg.SubCode
                LEFT JOIN PurchInvoiceDetail L  With (NoLock) On H.DocId = L.DocID
                LEFT JOIN Item I  With (NoLock) ON L.Item = I.Code
                LEFT JOIN ItemCategory Ic  With (NoLock) On I.ItemCategory = Ic.Code
                LEFT JOIN PurchInvoiceTransport Sit  With (NoLock) On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) as Transporter 
                            From SubgroupSiteDivisionDetail  With (NoLock)
                            Group By SubCode) As Hlt On H.Vendor = Hlt.SubCode
                LEFT JOIN SubGroup TSg  With (NoLock) ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration  With (NoLock)
                            Where RegistrationType = 'Sales Tax No') As VReg On H.Vendor = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration  With (NoLock)
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail  With (NoLock)
                            Where Site_Code = '" & AgL.PubSiteCode & "'
                            And Div_Code = '" & AgL.PubDivCode & "') As VDist On H.Vendor = VDist.SubCode
                Where H.DocId = '" & mSearchCode & "'"
        Dim DTInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select I.HSN, Count(*) As CntHSN
                From PurchInvoiceDetail L  With (NoLock)
                LEFT JOIN Item I  With (NoLock) On L.Item = I.Code
                Where DocId = '" & mSearchCode & "'
                GROUP By I.HSN 
                Order By CntHSN Desc "
        Dim DTMainHSN As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        ClsMain.FCreateJSONFile(DTInvoiceDetail, DTMainHSN)
    End Sub
    Public Sub FImportFromExcel_Old()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtFile1 As DataTable
        Dim DTFile2 As DataTable
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
        mQry = mQry + "Union All Select  '' as Srl,'Vendor' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor City' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Pincode' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Mobile' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Sales Tax No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Vendor GST No.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Doc No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Doc Date' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bill To Party' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Agent' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group Party' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Place Of Supply' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Outside State / Within State' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Ship To Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'SubTotal1' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deduction_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deduction' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Other_Charge_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Other_Charge' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Round_Off' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Net_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtFile1 = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bale No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group Item' as [Field Name], 'Text' as [Data Type],  20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Profit Margin Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deal Unit' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, If billing unit is different from unit then that billing unit will be save in deal unit other wise unit will be save here.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deal Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Discount Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Discount Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Additional Discount Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Additional Discount Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'MRP' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'LR No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'LR Date' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
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
        DTFile2 = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportPurchaseFromExcel
        ObjFrmImport.Text = "Purchase Invoice Import From Excel"
        ObjFrmImport.Dgl1.DataSource = DtFile1
        ObjFrmImport.Dgl2.DataSource = DTFile2
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtFile1 = ObjFrmImport.P_DsExcelData_PurchInvoice.Tables(0)
        DTFile2 = ObjFrmImport.P_DsExcelData_PurchInvoiceDetail.Tables(0)


        Dim DtV_Type = DtFile1.DefaultView.ToTable(True, "V_Type")
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

        Dim DtSaleToParty = DtFile1.DefaultView.ToTable(True, "Sale To Party")
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

        Dim DtBillToParty = DtFile1.DefaultView.ToTable(True, "Bill To Party")
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

        Dim DtAgent = DtFile1.DefaultView.ToTable(True, "Agent")
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

        Dim DtRateType = DtFile1.DefaultView.ToTable(True, "Rate Type")
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

        Dim DtSalesTaxGroupParty = DtFile1.DefaultView.ToTable(True, "Sales Tax Group Party")
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




        For I = 0 To DtFile1.Rows.Count - 1
            If AgL.XNull(DtFile1.Rows(I)("Sale To Party")) = "" Then
                ErrorLog += "Sale To Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("Bill To Party")) = "" Then
                ErrorLog += "Bill To Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("Sales Tax Group Party")) = "" Then
                ErrorLog += "Sales Tax Group Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("V_Date")) = "" Then
                ErrorLog += "V_Date is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("V_Type")) = "" Then
                ErrorLog += "V_Type is blank at row no." + (I + 2).ToString() & vbCrLf
            End If
        Next

        Dim DtItem = DTFile2.DefaultView.ToTable(True, "Item Name")
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

        Dim DtSalesTaxGroupItem = DTFile2.DefaultView.ToTable(True, "Sales Tax Group Item")
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

        For I = 0 To DTFile2.Rows.Count - 1
            If AgL.XNull(DTFile2.Rows(I)("Item Name")) = "" Then
                ErrorLog += "Item Name is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DTFile2.Rows(I)("Sales Tax Group Item")) = "" Then
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


            For I = 0 To DtFile1.Rows.Count - 1
                'Dim mDocId = AgL.GetDocId(AgL.XNull(DtFile1.Rows(I)("V_Type")), CStr(TxtV_No.Text), CDate(AgL.XNull(DtFile1.Rows(I)("V_Date"))),
                '                          AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)
                Dim mDocId = AgL.CreateDocId(AgL, "SaleInvoice", AgL.XNull(DtFile1.Rows(I)("V_Type")), CStr(TxtV_No.Text), CDate(AgL.XNull(DtFile1.Rows(I)("V_Date"))),
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
                        Where Sg.Name =  '" & AgL.XNull(DtFile1.Rows(I)("Sale To Party")) & "'"
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
                        Where Sg.Name =  '" & AgL.XNull(DtFile1.Rows(I)("Bill To Party")) & "'", AgL.GCn).ExecuteScalar()

                If AgL.Dman_Execute("SELECT Count(*) From SaleInvoice where V_Type = '" & AgL.XNull(DtFile1.Rows(I)("V_Type")) & "' And ReferenceNo = '" & AgL.XNull(DtFile1.Rows(I)("Manual Ref No")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                    mQry = " INSERT INTO SaleInvoice (DocID,  V_Type,  V_Prefix, V_Date,  V_No,  Div_Code,  Site_Code,
                             ReferenceNo,  SaleToParty,  BillToParty,  Agent, SaleToPartyName,  SaleToPartyAddress,
                             SaleToPartyCity,  SaleToPartyMobile, SaleToPartySalesTaxNo, 
                             RateType,  SalesTaxGroupParty, PlaceOfSupply,  Structure,
                             CustomFields,  SaleToPartyDocNo, SaleToPartyDocDate,  ReferenceDocId,
                             Remarks,  TermsAndConditions, Gross_Amount,  Taxable_Amount,
                             Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
                             Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
                             Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount,  PaidAmt,
                             CreditLimit,  CreditDays,  Status, EntryBy,  EntryDate,  ApproveBy,
                             ApproveDate,  MoveToLog,  MoveToLogDate, UploadDate)
                             Select  " & AgL.Chk_Text(mDocId) & ",  
                             " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("V_Type"))) & ",  
                             " & AgL.Chk_Text(mV_Prefix) & ",  
                             " & AgL.Chk_Date(AgL.XNull(DtFile1.Rows(I)("V_Date"))) & ",  
                             " & AgL.Chk_Text(mV_No) & ",  
                             " & AgL.Chk_Text(AgL.PubDivCode) & ",
                             " & AgL.Chk_Text(AgL.PubSiteCode) & ",  " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Manual Ref No"))) & ",  
                             " & AgL.Chk_Text(mSaleToParty) & ", 
                             (SELECT SubCode  From SubGroup WHERE Name = '" & AgL.XNull(DtFile1.Rows(I)("Bill To Party")) & "') As BillToParty,
                             (SELECT SubCode  From SubGroup WHERE Name = '" & AgL.XNull(DtFile1.Rows(I)("Agent")) & "') As Agent,
                             " & AgL.Chk_Text(mSaleToPartyName) & ",
                             " & AgL.Chk_Text(mSaleToPartyAddress) & ",  " & AgL.Chk_Text(mSaleToPartyCity) & ",  
                             " & AgL.Chk_Text(mSaleToPartyMobile) & ", " & AgL.Chk_Text(mSaleToPartySalesTaxNo) & ",                               
                             (SELECT Code  From RateType Where Description = '" & AgL.XNull(DtFile1.Rows(I)("Rate Type")) & "') As RateType,
                             '" & AgL.XNull(DtFile1.Rows(I)("Sales Tax Group Party")) & "' As SalesTaxGroupParty,
                             " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Place Of Supply"))) & ",  
                             (Select IfNull(Max(Structure),'') From Voucher_Type Where V_Type = '" & AgL.XNull(DtFile1.Rows(I)("V_Type")) & "') As Structure, 
                             Null As CustomFields,  
                              " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Sale To Party Doc No"))) & ",  
                              " & AgL.Chk_Date(AgL.XNull(DtFile1.Rows(I)("Sale To Party Doc Date"))) & ",  
                              Null As ReferenceDocId,  " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Remark"))) & ",  
                              " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Terms And Conditions"))) & ", 
                              " & AgL.VNull(DtFile1.Rows(I)("Gross Amount")) & ",  
                              " & AgL.VNull(DtFile1.Rows(I)("Taxable_Amount")) & ",  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax1_Per")) & " As Tax1_Per,
                              " & AgL.VNull(DtFile1.Rows(I)("Tax1")) & " As Tax1,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax2_Per")) & " As Tax2_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax2")) & " As Tax2, 
                              " & AgL.VNull(DtFile1.Rows(I)("Tax3_Per")) & " As Tax3_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax3")) & " As Tax3,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax4_Per")) & " As Tax4_Per,
                              " & AgL.VNull(DtFile1.Rows(I)("Tax4")) & " As Tax4,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax5_Per")) & " As Tax5_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax5")) & " As Tax5, 
                              " & AgL.VNull(DtFile1.Rows(I)("SubTotal1")) & " As SubTotal1,  
                              " & AgL.VNull(DtFile1.Rows(I)("Deduction_Per")) & " As Deduction_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Deduction")) & " As Deduction,
                              " & AgL.VNull(DtFile1.Rows(I)("Other_Charge_Per")) & " As Other_Charge_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Other_Charge")) & " As Other_Charge,  
                              " & AgL.VNull(DtFile1.Rows(I)("Round_Off")) & " As Round_Off, 
                              " & AgL.VNull(DtFile1.Rows(I)("Net_Amount")) & " As Net_Amount,  
                              0 As PaidAmt,  
                              " & AgL.VNull(DtFile1.Rows(I)("Credit Limit")) & " As CreditLimit,
                              " & AgL.VNull(DtFile1.Rows(I)("Credit Days")) & " As CreditDays,  
                              'Active' As Status,  
                              " & AgL.Chk_Text(AgL.PubUserName) & " As EntryBy, 
                              " & AgL.Chk_Date(AgL.PubLoginDate) & "  As EntryDate,  
                              Null As ApproveBy,  Null As ApproveDate,
                              Null As MoveToLog,  Null As MoveToLogDate,  Null As UploadDate"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                    Dim DtSaleInvoiceDetail_ForHeader As New DataTable
                    For M = 0 To DTFile2.Columns.Count - 1
                        Dim DColumn As New DataColumn
                        DColumn.ColumnName = DTFile2.Columns(M).ColumnName
                        DtSaleInvoiceDetail_ForHeader.Columns.Add(DColumn)
                    Next

                    Dim DtRowSaleInvoiceDetail_ForHeader As DataRow() = DTFile2.Select("V_Type = " + AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("V_Type"))) + " And [Manual Ref No] = " + AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Manual Ref No"))))
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

                        Dim DtRowSaleInvoiceDimensionDetail_ForHeader As DataRow() = DtSaleInvoiceDimensionDetail.Select("V_Type = " + AgL.Chk_Text(AgL.XNull(DTFile2.Rows(J)("V_Type"))) + " And [Manual Ref No] = " + AgL.Chk_Text(AgL.XNull(DTFile2.Rows(J)("Manual Ref No"))) + " And TSr = " + AgL.XNull(DTFile2.Rows(J)("TSr")), "TSr")
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


                    AgL.UpdateVoucherCounter(mDocId, CDate(AgL.XNull(DtFile1.Rows(I)("V_Date"))), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
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
            'mNarr = TxtV_Type.Text & " : " & mSaleToParty

        Next
    End Sub
    Public Sub FImportFromExcel(bImportFor As ImportFor)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtPurchaseInvoice As DataTable
        Dim DtPurchaseInvoiceDetail As DataTable
        Dim DtPurchaseInvoiceDimensionDetail As DataTable
        Dim DtPurchInvoice_DataFields As DataTable
        Dim DtPurchInvoiceDetail_DataFields As DataTable
        Dim DtPurchInvoiceDimensionDetail_DataFields As DataTable
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
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Address") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor City") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Pincode") & "' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Mobile") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Sales Tax No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Vendor GST No.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Doc No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Doc Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Bill To Party") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Agent") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sales Tax Group Party") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Place Of Supply") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Outside State / Within State' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Ship To Address") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "SubTotal1") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deduction_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deduction") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Other_Charge_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Other_Charge") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Round_Off") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Net_Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtPurchInvoice_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Invoice No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select '' as Srl, '" & GetFieldAliasName(bImportFor, "TSr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Item Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Specification") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Bale No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sales Tax Group Item") & "' as [Field Name], 'Text' as [Data Type],  20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Profit Margin Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Qty") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Unit") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Pcs") & "' as [Field Name], 'Number' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deal Unit") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'If billing unit is different from unit then that billing unit will be save in deal unit other wise unit will be save here.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deal Qty") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Rate") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Discount Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Discount Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Additional Discount Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Additional Discount Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale Rate") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "MRP") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "LR No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "LR Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
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
        DtPurchInvoiceDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Invoice No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select '' as Srl, '" & GetFieldAliasName(bImportFor, "TSr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Specification") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Pcs") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Qty") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "TotalQty") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtPurchInvoiceDimensionDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim ObjFrmImport As New FrmImportSaleFromExcel
        ObjFrmImport.Text = "Purchase Invoice Import"
        ObjFrmImport.Dgl1.DataSource = DtPurchInvoice_DataFields
        ObjFrmImport.Dgl2.DataSource = DtPurchInvoiceDetail_DataFields
        ObjFrmImport.Dgl3.DataSource = DtPurchInvoiceDimensionDetail_DataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtPurchaseInvoice = ObjFrmImport.P_DsExcelData_SaleInvoice.Tables(0)
        DtPurchaseInvoiceDetail = ObjFrmImport.P_DsExcelData_SaleInvoiceDetail.Tables(0)
        DtPurchaseInvoiceDimensionDetail = ObjFrmImport.P_DsExcelData_SaleInvoiceDimensionDetail.Tables(0)

        mFlag_Import = True

        If bImportFor = ImportFor.Dos Then
            ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
            Dim DtPurchaseInvoice_Filtered As New DataTable
            DtPurchaseInvoice_Filtered = DtPurchaseInvoice.Clone
            Dim DtPurchaseInvoiceRows_Filtered As DataRow() = DtPurchaseInvoice.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('GP','GR')")
            For I = 0 To DtPurchaseInvoiceRows_Filtered.Length - 1
                DtPurchaseInvoice_Filtered.ImportRow(DtPurchaseInvoiceRows_Filtered(I))
            Next
            DtPurchaseInvoice = DtPurchaseInvoice_Filtered
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            For I = 0 To DtPurchaseInvoice.Rows.Count - 1
                DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party")) = DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party")).ToString().Replace(" ", "")

                If DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Place Of Supply")).ToString().Trim() = "EX.U.P." Then
                    DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Place Of Supply")) = PlaceOfSupplay.OutsideState
                Else
                    DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Place Of Supply")) = PlaceOfSupplay.WithinState
                End If

                If DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor")).ToString().Trim() = "CASH A/C." Then
                    DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor")) = "CASH A/C"
                End If

                If DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Doc Date")).ToString().Trim() = "30/Dec/1899 12:00:00 AM" Then
                    DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Doc Date")) = DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))
                End If

                'PurchInvoiceTableList(0).VendorDocDate <> "12:00:00 AM"

                If AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString().Trim() = "N.A" Or
                        AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString().Trim() = "." Then
                    DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Agent")) = ""
                End If

                If DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "GP" Then
                    DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PI"
                ElseIf DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "GR" Then
                    DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PR"
                End If

                If DtPurchaseInvoice.Columns.Contains(GetFieldAliasName(bImportFor, "fv_no")) Then
                    DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Remark")) = DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "fv_no"))
                End If
            Next


            DtPurchaseInvoiceDetail.Columns.Add(GetFieldAliasName(bImportFor, "TSr"))

            For I = 0 To DtPurchaseInvoiceDetail.Rows.Count - 1
                DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Item")) = DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Item")).ToString().Replace("@ ", "").Replace("@", "").Trim

                'mQry = "Select Description From Item Where Specification = " & AgL.Chk_Text(DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")).ToString.Trim) & " "
                'DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")) = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar

                Dim bItemDesc As String = ""
                mQry = "Select I.Description 
                        From Item I
                        LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                        LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                        Where IsNull(Specification,'') = " & AgL.Chk_Text(DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")).ToString.Trim) & " 
                        And IsNull(Ig.Description,'') = " & AgL.Chk_Text(DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Group")).ToString.Trim) & " 
                        And IsNull(Ic.Description,'') = " & AgL.Chk_Text(DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Category")).ToString.Trim) & ""
                bItemDesc = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar

                If bItemDesc = "" Then
                    mQry = "Select Description From Item Where Description = " & AgL.Chk_Text(DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")).ToString.Trim) & " "
                    bItemDesc = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar

                    If bItemDesc = "" Then
                        mQry = "Select Description From Item Where Specification = " & AgL.Chk_Text(DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")).ToString.Trim) & " "
                        bItemDesc = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar
                    End If
                End If
                DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")) = bItemDesc


                If DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "GP" Or
                        DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CO" Or
                        DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DO" Or
                        DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "MP" Then
                    DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PI"
                ElseIf DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "GR" Then
                    DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PR"
                End If

                If DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString.Trim = "P" Then
                    DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Pcs"
                ElseIf DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString.Trim.ToUpper = "MTR" Then
                    DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Meter"
                ElseIf DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString.Trim.ToUpper = "M" Then
                    DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Meter"
                End If


                If DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "TSr")).ToString.Trim = "" Then
                    DtPurchaseInvoiceDetail.Rows(I)(GetFieldAliasName(bImportFor, "TSr")) = I + 1
                End If
            Next
        End If



        Dim DtV_Type = DtPurchaseInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
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

        Dim DtSaleToParty = DtPurchaseInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Vendor"))
        For I = 0 To DtSaleToParty.Rows.Count - 1
            If AgL.XNull(DtSaleToParty.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Upper(RTrim(LTrim(Name))) = " & AgL.Chk_Text(AgL.XNull(DtSaleToParty.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))).ToString().Trim().ToUpper) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSaleToParty.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSaleToParty.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtBillToParty = DtPurchaseInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Bill To Party"))
        For I = 0 To DtBillToParty.Rows.Count - 1
            If AgL.XNull(DtBillToParty.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Upper(RTrim(LTrim(Name)))  = '" & AgL.XNull(DtBillToParty.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))).ToString().Trim.ToUpper & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtBillToParty.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtBillToParty.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))) & ", "
                    End If
                End If
            End If
        Next

        'Dim DtAgent = DtPurchaseInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Agent"))
        'For I = 0 To DtAgent.Rows.Count - 1
        '    If AgL.XNull(DtAgent.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString().Trim <> "" Then
        '        If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = '" & AgL.XNull(DtAgent.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString().Trim & "'", AgL.GCn).ExecuteScalar = 0 Then
        '            If ErrorLog.Contains("These Agents Are Not Present In Master") = False Then
        '                ErrorLog += vbCrLf & "These Agents Are Not Present In Master" & vbCrLf
        '                ErrorLog += AgL.XNull(DtAgent.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))) & ", "
        '            Else
        '                ErrorLog += AgL.XNull(DtAgent.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))) & ", "
        '            End If
        '        End If
        '    End If
        'Next



        Dim DtSalesTaxGroupParty = DtPurchaseInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Sales Tax Group Party"))
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

        Dim DtCity = DtPurchaseInvoice.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Vendor City"))
        For I = 0 To DtCity.Rows.Count - 1
            If AgL.XNull(DtCity.Rows(I)(GetFieldAliasName(bImportFor, "Vendor City"))).ToString().Trim().ToUpper <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From City where CityName = '" & AgL.XNull(DtCity.Rows(I)(GetFieldAliasName(bImportFor, "Vendor City")).ToString().Trim().ToUpper) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Cities Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Cities Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtCity.Rows(I)(GetFieldAliasName(bImportFor, "Vendor City"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtCity.Rows(I)(GetFieldAliasName(bImportFor, "Vendor City"))) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtPurchInvoice_DataFields.Rows.Count - 1
            If AgL.XNull(DtPurchInvoice_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtPurchaseInvoice.Columns.Contains(AgL.XNull(DtPurchInvoice_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtPurchInvoice_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtPurchInvoice_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If

                'For J = 0 To DtPurchInvoice_DataFields.Rows.Count - 1
                '    If AgL.XNull(DtPurchaseInvoice.Rows(I)(DtPurchInvoice_DataFields.Rows(J)("Field Name"))) = "" Then
                '        ErrorLog += DtPurchInvoice_DataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
                '    End If
                'Next
            End If
        Next

        'Dim DtItem = DtPurchaseInvoiceDetail.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Name"))
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
        Dim DtItem = DtPurchaseInvoiceDetail.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Name"))
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

        Dim DtSalesTaxGroupItem = DtPurchaseInvoiceDetail.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Sales Tax Group Item"))
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

        For I = 0 To DtPurchInvoiceDetail_DataFields.Rows.Count - 1
            If AgL.XNull(DtPurchInvoiceDetail_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtPurchaseInvoiceDetail.Columns.Contains(AgL.XNull(DtPurchInvoiceDetail_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtPurchInvoiceDetail_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtPurchInvoiceDetail_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If

                'For J = 0 To DtPurchInvoiceDetail_DataFields.Rows.Count - 1
                '    If AgL.XNull(DtPurchaseInvoiceDetail.Rows(I)(DtPurchInvoiceDetail_DataFields.Rows(J)("Field Name"))) = "" Then
                '        ErrorLog += DtPurchInvoiceDetail_DataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
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


            For I = 0 To DtPurchaseInvoice.Rows.Count - 1
                Dim Tot_Gross_Amount As Double = 0
                Dim Tot_Taxable_Amount As Double = 0
                Dim Tot_Tax1 As Double = 0
                Dim Tot_Tax2 As Double = 0
                Dim Tot_Tax3 As Double = 0
                Dim Tot_Tax4 As Double = 0
                Dim Tot_Tax5 As Double = 0
                Dim Tot_SubTotal1 As Double = 0


                Dim PurchInvoiceTableList(0) As StructPurchInvoice
                Dim PurchInvoiceDimensionTableList(0) As StructPurchInvoiceDimensionDetail
                Dim PurchInvoiceTable As New StructPurchInvoice

                PurchInvoiceTable.DocID = ""
                PurchInvoiceTable.V_Type = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))
                PurchInvoiceTable.V_Prefix = ""
                PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                PurchInvoiceTable.Div_Code = AgL.PubDivCode
                PurchInvoiceTable.V_No = AgL.VNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                PurchInvoiceTable.V_Date = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))
                PurchInvoiceTable.ManualRefNo = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Invoice No"))).ToString.Trim
                PurchInvoiceTable.Vendor = ""
                PurchInvoiceTable.AgentCode = ""
                PurchInvoiceTable.AgentName = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString.Trim
                PurchInvoiceTable.VendorName = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))).ToString.Trim
                PurchInvoiceTable.BillToPartyCode = ""
                PurchInvoiceTable.BillToPartyName = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Bill To Party"))).ToString.Trim
                PurchInvoiceTable.VendorAddress = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Address"))).ToString.Trim
                PurchInvoiceTable.VendorCity = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor City"))).ToString.Trim

                If DtPurchaseInvoice.Columns.Contains(GetFieldAliasName(bImportFor, "Vendor Mobile")) = True Then
                    PurchInvoiceTable.VendorMobile = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Mobile"))).ToString.Trim
                End If


                PurchInvoiceTable.VendorSalesTaxNo = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Sales Tax No"))).ToString.Trim

                PurchInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group Party"))).ToString.Trim
                PurchInvoiceTable.PlaceOfSupply = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Place Of Supply"))).ToString.Trim
                PurchInvoiceTable.StructureCode = ""
                PurchInvoiceTable.CustomFields = ""

                If DtPurchaseInvoice.Columns.Contains(GetFieldAliasName(bImportFor, "Vendor Doc No")) = True Then
                    PurchInvoiceTable.VendorDocNo = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Doc No"))).ToString.Trim
                End If

                If DtPurchaseInvoice.Columns.Contains(GetFieldAliasName(bImportFor, "Vendor Doc Date")) = True Then
                    PurchInvoiceTable.VendorDocDate = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Doc Date")))
                End If

                PurchInvoiceTable.ReferenceDocId = ""
                PurchInvoiceTable.Remarks = AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Remark")))
                PurchInvoiceTable.Status = "Active"
                PurchInvoiceTable.EntryBy = AgL.PubUserName
                PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                PurchInvoiceTable.ApproveBy = ""
                PurchInvoiceTable.ApproveDate = ""
                PurchInvoiceTable.MoveToLog = ""
                PurchInvoiceTable.MoveToLogDate = ""
                PurchInvoiceTable.UploadDate = ""

                PurchInvoiceTable.Deduction_Per = AgL.VNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Deduction_Per")))
                PurchInvoiceTable.Deduction = AgL.VNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Deduction")))
                PurchInvoiceTable.Other_Charge_Per = AgL.VNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Other_Charge_Per")))
                PurchInvoiceTable.Other_Charge = AgL.VNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Other_Charge")))
                PurchInvoiceTable.Round_Off = AgL.VNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Round_Off")))
                PurchInvoiceTable.Net_Amount = AgL.VNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Net_Amount")))


                If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Deduction = PurchInvoiceTable.Deduction * (-1)
                If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Other_Charge = PurchInvoiceTable.Other_Charge * (-1)
                If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Round_Off = PurchInvoiceTable.Round_Off * (-1)
                If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Net_Amount = PurchInvoiceTable.Net_Amount * (-1)



                Dim DtPurchInvoiceDetail_ForHeader As New DataTable
                For M = 0 To DtPurchaseInvoiceDetail.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtPurchaseInvoiceDetail.Columns(M).ColumnName
                    DtPurchInvoiceDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowPurchInvoiceDetail_ForHeader As DataRow() = DtPurchaseInvoiceDetail.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtPurchaseInvoice.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "Invoice No") & "] = " + AgL.Chk_Text(AgL.XNull(DtPurchaseInvoice.Rows(I)(GetFieldAliasName(bImportFor, "Invoice No")))))
                If DtRowPurchInvoiceDetail_ForHeader.Length > 0 Then
                    For M = 0 To DtRowPurchInvoiceDetail_ForHeader.Length - 1
                        DtPurchInvoiceDetail_ForHeader.Rows.Add()
                        For N = 0 To DtPurchInvoiceDetail_ForHeader.Columns.Count - 1
                            DtPurchInvoiceDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDetail_ForHeader(M)(N)
                        Next
                    Next
                End If

                For J = 0 To DtPurchInvoiceDetail_ForHeader.Rows.Count - 1
                    PurchInvoiceTable.Line_Sr = J + 1
                    PurchInvoiceTable.Line_ItemName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Item Name"))).ToString.Trim
                    PurchInvoiceTable.Line_Specification = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Specification"))).ToString.Trim
                    PurchInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Sales Tax Group Item"))).ToString.Trim
                    PurchInvoiceTable.Line_ReferenceNo = ""
                    PurchInvoiceTable.Line_DocQty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Qty")))
                    PurchInvoiceTable.Line_FreeQty = 0
                    PurchInvoiceTable.Line_Qty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Qty")))
                    PurchInvoiceTable.Line_Unit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Unit"))).ToString.Trim
                    PurchInvoiceTable.Line_Pcs = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Pcs")))

                    If DtPurchInvoiceDetail_ForHeader.Columns.Contains(GetFieldAliasName(bImportFor, "Unit Multiplier")) = True Then
                        PurchInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Unit Multiplier")))
                    Else
                        PurchInvoiceTable.Line_UnitMultiplier = 1
                    End If

                    If DtPurchInvoiceDetail_ForHeader.Columns.Contains(GetFieldAliasName(bImportFor, "Unit Multiplier")) = True Then
                        PurchInvoiceTable.Line_DealUnit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Deal Unit"))).ToString.Trim
                    Else
                        PurchInvoiceTable.Line_DealUnit = PurchInvoiceTable.Line_Unit
                    End If

                    If DtPurchInvoiceDetail_ForHeader.Columns.Contains(GetFieldAliasName(bImportFor, "Deal Qty")) = True Then
                        PurchInvoiceTable.Line_DocDealQty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Deal Qty")))
                    Else
                        PurchInvoiceTable.Line_DocDealQty = PurchInvoiceTable.Line_Qty
                    End If



                    PurchInvoiceTable.Line_Rate = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Rate")))
                    PurchInvoiceTable.Line_DiscountPer = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Discount Per")))
                    PurchInvoiceTable.Line_DiscountAmount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Discount Amount")))
                    PurchInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Additional Discount Per")))
                    PurchInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Additional Discount Amount")))
                    PurchInvoiceTable.Line_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amount")))
                    PurchInvoiceTable.Line_Remark = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Remark")))
                    PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Bale No")))
                    PurchInvoiceTable.Line_LotNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Lot No")))
                    PurchInvoiceTable.Line_ReferenceDocId = ""
                    PurchInvoiceTable.Line_ReferenceSr = ""
                    PurchInvoiceTable.Line_PurchInvoice = ""
                    PurchInvoiceTable.Line_PurchInvoiceSr = ""
                    PurchInvoiceTable.Line_GrossWeight = 0
                    PurchInvoiceTable.Line_NetWeight = 0
                    PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Gross_Amount")))
                    PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Taxable_Amount")))
                    PurchInvoiceTable.Line_Tax1_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax1_Per")))
                    PurchInvoiceTable.Line_Tax1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax1")))
                    PurchInvoiceTable.Line_Tax2_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax2_Per")))
                    PurchInvoiceTable.Line_Tax2 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax2")))
                    PurchInvoiceTable.Line_Tax3_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax3_Per")))
                    PurchInvoiceTable.Line_Tax3 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax3")))
                    PurchInvoiceTable.Line_Tax4_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax4_Per")))
                    PurchInvoiceTable.Line_Tax4 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax4")))
                    PurchInvoiceTable.Line_Tax5_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax5_Per")))
                    PurchInvoiceTable.Line_Tax5 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax5")))
                    PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "SubTotal1")))


                    'For Header Values
                    Tot_Gross_Amount += PurchInvoiceTable.Line_Gross_Amount
                    Tot_Taxable_Amount += PurchInvoiceTable.Line_Taxable_Amount
                    Tot_Tax1 += PurchInvoiceTable.Line_Tax1
                    Tot_Tax2 += PurchInvoiceTable.Line_Tax2
                    Tot_Tax3 += PurchInvoiceTable.Line_Tax3
                    Tot_Tax4 += PurchInvoiceTable.Line_Tax4
                    Tot_Tax5 += PurchInvoiceTable.Line_Tax5
                    Tot_SubTotal1 += PurchInvoiceTable.Line_SubTotal1


                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_DocQty = PurchInvoiceTable.Line_DocQty * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Qty = PurchInvoiceTable.Line_Qty * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_DocDealQty = PurchInvoiceTable.Line_DocDealQty * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Amount = PurchInvoiceTable.Line_Amount * (-1)

                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Gross_Amount = PurchInvoiceTable.Line_Gross_Amount * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Taxable_Amount = PurchInvoiceTable.Line_Taxable_Amount * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Tax1 = PurchInvoiceTable.Line_Tax1 * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Tax2 = PurchInvoiceTable.Line_Tax2 * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Tax3 = PurchInvoiceTable.Line_Tax3 * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Tax4 = PurchInvoiceTable.Line_Tax4 * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_Tax5 = PurchInvoiceTable.Line_Tax5 * (-1)
                    If PurchInvoiceTable.V_Type = "PR" Then PurchInvoiceTable.Line_SubTotal1 = PurchInvoiceTable.Line_SubTotal1 * (-1)

                    Dim DtPurchInvoiceDimensionDetail_ForHeader As New DataTable
                    For M = 0 To DtPurchaseInvoiceDimensionDetail.Columns.Count - 1
                        Dim DColumn As New DataColumn
                        DColumn.ColumnName = DtPurchaseInvoiceDimensionDetail.Columns(M).ColumnName
                        DtPurchInvoiceDimensionDetail_ForHeader.Columns.Add(DColumn)
                    Next

                    Dim DtRowPurchInvoiceDimensionDetail_ForHeader As DataRow() = DtPurchaseInvoiceDimensionDetail.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtPurchaseInvoiceDetail.Rows(J)(GetFieldAliasName(bImportFor, "V_Type")))) + " And [" & GetFieldAliasName(bImportFor, "Invoice No") & "] = " + AgL.Chk_Text(AgL.XNull(DtPurchaseInvoiceDetail.Rows(J)(GetFieldAliasName(bImportFor, "Invoice No")))) + " And [" & GetFieldAliasName(bImportFor, "TSr") & "] = " + AgL.XNull(DtPurchaseInvoiceDetail.Rows(J)(GetFieldAliasName(bImportFor, "TSr"))), GetFieldAliasName(bImportFor, "TSr"))
                    If DtRowPurchInvoiceDimensionDetail_ForHeader.Length > 0 Then
                        For M = 0 To DtRowPurchInvoiceDetail_ForHeader.Length - 1
                            DtPurchInvoiceDetail_ForHeader.Rows.Add()
                            For N = 0 To DtPurchInvoiceDimensionDetail_ForHeader.Columns.Count - 1
                                DtPurchInvoiceDimensionDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDimensionDetail_ForHeader(M)(N)
                            Next
                        Next
                    End If

                    For K = 0 To DtPurchInvoiceDimensionDetail_ForHeader.Rows.Count - 1
                        Dim PurchInvoiceDimensionTable As New StructPurchInvoiceDimensionDetail

                        PurchInvoiceDimensionTable.TSr = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "TSr")))
                        PurchInvoiceDimensionTable.Sr = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Sr")))
                        PurchInvoiceDimensionTable.Specification = AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Specification")))
                        PurchInvoiceDimensionTable.Pcs = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Pcs")))
                        PurchInvoiceDimensionTable.Qty = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Qty")))
                        PurchInvoiceDimensionTable.TotalQty = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "TotalQty")))

                        PurchInvoiceDimensionTableList(UBound(PurchInvoiceDimensionTableList)) = PurchInvoiceDimensionTable
                        ReDim Preserve PurchInvoiceDimensionTableList(UBound(PurchInvoiceDimensionTableList) + 1)
                    Next

                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                    ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                Next

                PurchInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
                PurchInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
                PurchInvoiceTableList(0).Tax1 = Tot_Tax1
                PurchInvoiceTableList(0).Tax2 = Tot_Tax2
                PurchInvoiceTableList(0).Tax3 = Tot_Tax3
                PurchInvoiceTableList(0).Tax4 = Tot_Tax4
                PurchInvoiceTableList(0).Tax5 = Tot_Tax5
                PurchInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
                PurchInvoiceTableList(0).Other_Charge = 0
                PurchInvoiceTableList(0).Deduction = 0
                PurchInvoiceTableList(0).Round_Off = Math.Round(Math.Round(PurchInvoiceTableList(0).SubTotal1) - PurchInvoiceTableList(0).SubTotal1, 2)
                PurchInvoiceTableList(0).Net_Amount = Math.Round(PurchInvoiceTableList(0).SubTotal1)


                Dim Tot_RoundOff As Double = 0
                Dim Tot_NetAmount As Double = 0
                For J = 0 To PurchInvoiceTableList.Length - 1
                    PurchInvoiceTableList(J).Line_Round_Off = Math.Round(PurchInvoiceTableList(0).Round_Off * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                    PurchInvoiceTableList(J).Line_Net_Amount = Math.Round(PurchInvoiceTableList(0).Net_Amount * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                    Tot_RoundOff += PurchInvoiceTableList(J).Line_Round_Off
                    Tot_NetAmount += PurchInvoiceTableList(J).Line_Net_Amount
                Next

                Tot_RoundOff = Math.Round(Tot_RoundOff, 2)

                If Tot_RoundOff <> PurchInvoiceTableList(0).Round_Off Then
                    PurchInvoiceTableList(0).Line_Round_Off = PurchInvoiceTableList(0).Line_Round_Off + (PurchInvoiceTableList(0).Round_Off - Tot_RoundOff)
                End If

                If Tot_NetAmount <> PurchInvoiceTableList(0).Net_Amount Then
                    PurchInvoiceTableList(0).Line_Net_Amount = PurchInvoiceTableList(0).Line_Net_Amount + (PurchInvoiceTableList(0).Net_Amount - Tot_NetAmount)
                End If

                InsertPurchInvoice(PurchInvoiceTableList, PurchInvoiceDimensionTableList)
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


    Private Function GetFieldAliasName(bImportFor As ImportFor, bFieldName As String)
        Dim bAliasName As String = bFieldName
        If bImportFor = ImportFor.Dos Then
            Select Case bFieldName




                Case "V_TYPE"
                    bAliasName = "V_TYPE"
                Case "V_NO"
                    bAliasName = "v_no"
                Case "V_Date"
                    bAliasName = "v_date"
                Case "Invoice No"
                    bAliasName = "invoice_no"
                Case "Vendor"
                    bAliasName = "vendor"
                Case "Vendor Address"
                    bAliasName = "vendor_add"
                Case "Vendor City"
                    bAliasName = "vendorcity"
                Case "Vendor Pincode"
                    bAliasName = "pincode"
                Case "Vendor Mobile"
                    bAliasName = "mobile"
                Case "Vendor Sales Tax No"
                    bAliasName = "gstin"
                Case "Vendor Doc No"
                    bAliasName = "doc_no"
                Case "Vendor Doc Date"
                    bAliasName = "doc_date"
                Case "Bill To Party"
                    bAliasName = "bill_party"
                Case "Agent"
                    bAliasName = "agent"
                Case "Sales Tax Group Party"
                    bAliasName = "tax_group"
                Case "Place Of Supply"
                    bAliasName = "place_supp"
                Case "Remark"
                    bAliasName = "remark"
                Case "SubTotal1"
                    bAliasName = "subtotal1"
                Case "Deduction_Per"
                    bAliasName = "ded_per"
                Case "Deduction"
                    bAliasName = "deduction"
                Case "Other_Charge_Per"
                    bAliasName = "ot_ch_per"
                Case "Other_Charge"
                    bAliasName = "ot_charge"
                Case "Round_Off"
                    bAliasName = "round_off"
                Case "Net_Amount"
                    bAliasName = "net_amount"




                Case "TSr"
                    bAliasName = "TSR"
                Case "Item Name"
                    bAliasName = "item_name"
                Case "Item Group"
                    bAliasName = "make_name"
                Case "Item Category"
                    bAliasName = "catagory"
                Case "Specification"
                    bAliasName = "specific"
                Case "Bale No"
                    bAliasName = "bale_no"
                Case "Sales Tax Group Item"
                    bAliasName = "tax_group"
                Case "Qty"
                    bAliasName = "qty"
                Case "Unit"
                    bAliasName = "unit"
                Case "Rate"
                    bAliasName = "Rate"
                Case "Discount Per"
                    bAliasName = "disc_per"
                Case "Discount Amount"
                    bAliasName = "disc_amt"
                Case "Additional Discount Per"
                    bAliasName = "adisc_per"
                Case "Additional Discount Amount"
                    bAliasName = "adisc_amt"
                Case "Amount"
                    bAliasName = "amount"
                Case "Remark"
                    bAliasName = "remark"
                Case "LR No"
                    bAliasName = "lr_no"
                Case "LR Date"
                    bAliasName = "lr_date"
                Case "Lot No"
                    bAliasName = "lot_no"
                Case "Gross_Amount"
                    bAliasName = "gross_amt"
                Case "Taxable_Amount"
                    bAliasName = "taxableamt"
                Case "Tax1_Per"
                    bAliasName = "tax1_per"
                Case "Tax1"
                    bAliasName = "tax1"
                Case "Tax2_Per"
                    bAliasName = "tax2_per"
                Case "Tax2"
                    bAliasName = "tax2"
                Case "Tax3_Per"
                    bAliasName = "tax3_per"
                Case "Tax3"
                    bAliasName = "tax3"
                Case "Tax4_Per"
                    bAliasName = "tax4_per"
                Case "Tax4"
                    bAliasName = "tax4"
                Case "Tax5_Per"
                    bAliasName = "tax5_per"
                Case "Tax5"
                    bAliasName = "tax5"
                Case "SubTotal1"
                    bAliasName = "subtotal1"





            End Select

            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function

    '-------------------------------------------------------------------------------------

    Public Shared Sub FGetCalculationData(mSearchCode As String, Conn As Object, Cmd As Object)
        Dim mQry As String = ""
        mQry = "SELECT Sd.* 
                FROM PurchInvoice H With (NoLock)
                LEFT JOIN StructureDetail Sd With (NoLock) ON H.Structure = Sd.Code
                WHERE H.DocID = '" & mSearchCode & "'"
        Dim DtCalcHeaderData As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        mQry = "Select * From PurchInvoice With (NoLock) Where DocId = '" & mSearchCode & "'"
        Dim DtTransactionDetail As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        For I As Integer = 0 To DtCalcHeaderData.Rows.Count - 1
            For J As Integer = 0 To DtTransactionDetail.Columns.Count - 1
                If DtCalcHeaderData.Rows(I)("HeaderAmtField") = DtTransactionDetail.Columns(J).ColumnName Then
                    DtCalcHeaderData.Rows(I)("Amount") = (AgL.VNull(DtTransactionDetail.Rows(0)(DtTransactionDetail.Columns(J).ColumnName)))
                End If
            Next
        Next

        mQry = " SELECT Sd.Charges, Pst.*
                FROM PurchInvoice H With (NoLock)
                LEFT JOIN PurchInvoiceDetail L With (NoLock) ON H.DocID = L.DocID
                LEFT JOIN PostingGroupSalesTax Pst With (NoLock) ON H.SalesTaxGroupParty = Pst.PostingGroupSalesTaxParty
	                AND H.PlaceOfSupply = Pst.PlaceOfSupply
	                AND L.SalesTaxGroupItem = Pst.PostingGroupSalesTaxItem
	                AND Pst.Process = 'PURCH'
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
        mQry += " From PurchInvoiceDetail With (NoLock) Where DocId = '" & mSearchCode & "'"
        Dim DtCalcLineData As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        For I As Integer = 0 To DtCalcLineData.Rows.Count - 1
            For J As Integer = 0 To DtCalcLineData.Columns.Count - 1
                For K As Integer = 0 To DtPostingGroupSalesTax.Rows.Count - 1
                    If DtCalcLineData.Columns(J).ColumnName = GetColNamePostAc(AgL.XNull(DtPostingGroupSalesTax.Rows(K)("Charges"))) Then
                        DtCalcLineData.Rows(I)(J) = AgL.XNull(DtPostingGroupSalesTax.Rows(K)("LedgerAc"))
                    ElseIf DtCalcLineData.Columns(J).ColumnName = GetColNamePer(AgL.XNull(DtPostingGroupSalesTax.Rows(K)("Charges"))) Then
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
                        Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar) = Ncat.PurchaseReturn Then
            mNarrationParty = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type"))
            mNarrationParty += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
            mNarration = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & " : " & DtTransactionDetail.Rows(0)("VendorName") & ""
            mNarration += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
            mMultiplyWithMinus = True
        Else
            If AgL.XNull(DtTransactionDetail.Rows(0)("VendorDocNo")) <> "" Then
                mNarrationParty = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & " : " & DtTransactionDetail.Rows(0)("VendorDocNo") & " Dated " & DtTransactionDetail.Rows(0)("VendorDocDate")
                mNarrationParty += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
                mNarration = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & " : " & DtTransactionDetail.Rows(0)("VendorName") & " Invoice No. " & DtTransactionDetail.Rows(0)("VendorDocNo") & " Dated " & DtTransactionDetail.Rows(0)("VendorDocDate")
                mNarration += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
            Else
                mNarrationParty = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type"))
                mNarrationParty += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
                mNarration = AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & " : " & DtTransactionDetail.Rows(0)("VendorName") & ""
                mNarration += ", " + AgL.XNull(DtTransactionDetail.Rows(0)("Remarks"))
            End If
            mMultiplyWithMinus = False
        End If

        Dim DtSettings As DataTable
        mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
        DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtSettings.Rows.Count = 0 Then
            mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
            DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtSettings.Rows.Count = 0 Then
                mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtSettings.Rows.Count = 0 Then
                    mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type = '" & AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) & "' And Div_Code  Is Null And Site_Code Is Null "
                    DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtSettings.Rows.Count = 0 Then
                        mQry = "Select * from PurchaseInvoiceSetting  With (NoLock)  Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                        DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    End If
                End If
            End If
        End If



        Dim bPartyLedgerPostingAc As String = ""
        Dim bLinkedPartyAc As String = ""
        If AgL.StrCmp(AgL.XNull(DtSettings.Rows(0)("LedgerPostingPartyAcType")), PurchInvoiceLedgerPostingPartyAcType.Vendor) Then
            bPartyLedgerPostingAc = AgL.XNull(DtTransactionDetail.Rows(0)("Vendor"))
            bLinkedPartyAc = AgL.XNull(DtTransactionDetail.Rows(0)("BillToParty"))
        Else
            bPartyLedgerPostingAc = AgL.XNull(DtTransactionDetail.Rows(0)("BillToParty"))
            bLinkedPartyAc = AgL.XNull(DtTransactionDetail.Rows(0)("Vendor"))
        End If

        ClsMain.PostStructureLineToAccounts(DtCalcHeaderData, DtCalcLineData, mNarrationParty, mNarration, mSearchCode, AgL.XNull(DtTransactionDetail.Rows(0)("Div_Code")),
                                    AgL.XNull(DtTransactionDetail.Rows(0)("Site_Code")),
                                    AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")), AgL.XNull(DtTransactionDetail.Rows(0)("V_Prefix")), AgL.VNull(DtTransactionDetail.Rows(0)("V_No")),
                                    AgL.XNull(DtTransactionDetail.Rows(0)("ManualRefNo")), AgL.XNull(DtTransactionDetail.Rows(0)("Vendor")),
                                    AgL.XNull(DtTransactionDetail.Rows(0)("V_Date")), Conn, Cmd,, mMultiplyWithMinus, AgL.XNull(DtTransactionDetail.Rows(0)("BillToParty")))
    End Sub

    Private Sub FrmPurchInvoiceDirect_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select * From ItemTypeSetting "
        DtItemTypeSettingsAll = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FInsertLRDetail(DocID As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bDescription As String = ""
        Dim bSpecification1 As String = ""
        Dim bMfgDate As String = ""
        Dim bSpecification3 As Decimal = 0
        Dim bSpecification4 As String = ""
        Dim bSpecification5 As String = ""

        mQry = " Select IsNull(L.LRNo, Pit.LRNo) As LRNo, Max(Sg.Name) As TransporterName, Max(Pit.Transporter) As TransporterCode, 
                    Max(L.LRDate) As LRDate, Sum(L.Qty) As Qty, Max(H.Vendor) As Vendor
                    From PurchInvoice H With (NoLock)
                    LEFT JOIN PurchInvoiceDetail L With (NoLock) ON H.DocId = L.DocId
                    LEFT JOIN PurchInvoiceTransport Pit With (NoLock) On H.DocId = Pit.DocId
                    LEFT JOIN SubGroup Sg On Pit.Transporter = Sg.SubCode
                    LEFT JOIN Item I On L.Item = I.Code
                    Where L.DocId = '" & DocID & "'
                    And IsNull(L.LRNo, Pit.LRNo) Is Not Null
                    Group By IsNull(L.LRNo, Pit.LRNo) "
        Dim DtLrHeader As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        For I As Integer = 0 To DtLrHeader.Rows.Count - 1
            Dim bStockHeadDocId As String = ""
            Dim bV_Prefix As String = ""
            Dim bV_No As String = ""
            Dim bV_Type As String = Ncat.LrEntry
            Dim bV_Date As String = TxtV_Date.Text
            Dim bManualRefNo As String = ""

            'bStockHeadDocId = AgL.GetDocId(bV_Type, CStr(bV_No), CDate(bV_Date), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
            bStockHeadDocId = AgL.CreateDocId(AgL, "StockHead", bV_Type, CStr(bV_No), CDate(bV_Date), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
            bV_Prefix = AgL.DeCodeDocID(bStockHeadDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            bV_No = Val(AgL.DeCodeDocID(bStockHeadDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))

            Dim bManualrefNoPrefix As String = AgL.Dman_Execute("Select IfNull(Ref_Prefix,'') From Voucher_Prefix With (NoLock) Where V_Type = '" & bV_Type & "' 
                                And " & AgL.Chk_Date(bV_Date) & " >= Date(Date_From) 
                                And " & AgL.Chk_Date(bV_Date) & " <= Date(Date_To) ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            bManualRefNo = bManualrefNoPrefix + bV_No.ToString().PadLeft(4).Replace(" ", "0")

            mQry = "INSERT INTO StockHead (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, SubCode, Transporter, 
                        PartyDocNo, PartyDocDate, GenDocId, EntryBy, EntryDate, LockText)
                        Select " & AgL.Chk_Text(bStockHeadDocId) & " As DocID, " & AgL.Chk_Text(bV_Type) & " As V_Type, 
                        " & AgL.Chk_Text(bV_Prefix) & " As V_Prefix, " & AgL.Chk_Date(bV_Date) & " As V_Date, 
                        " & bV_No & " As V_No, " & AgL.Chk_Text(AgL.PubDivCode) & " As Div_Code, 
                        " & AgL.Chk_Text(AgL.PubSiteCode) & " As Site_Code, " & AgL.Chk_Text(bManualRefNo) & " As ManualRefNo, 
                        " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("Vendor"))) & " As SubCode, 
                        " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("TransporterCode"))) & " As Transporter, 
                        " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("LrNo"))) & " As PartyDocNo, 
                        " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("LrDate"))) & " As PartyDocDate, 
                        " & AgL.Chk_Text(DocID) & " As GenDocId, 
                        " & AgL.Chk_Text(AgL.PubUserName) & " As EntryBy, 
                        " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate,  
                        " & AgL.Chk_Text("Auto Generated From " & TxtV_Type.Text & " " & TxtReferenceNo.Text) & " As LockText  
                        "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            bDescription = AgL.XNull(DtLrHeader.Rows(I)("LRNo")) + " -" + AgL.XNull(DtLrHeader.Rows(I)("TransporterName"))
            bSpecification1 = AgL.XNull(DtLrHeader.Rows(I)("LRNo"))
            bMfgDate = AgL.XNull(DtLrHeader.Rows(I)("LRDate"))
            bSpecification3 = AgL.VNull(DtLrHeader.Rows(I)("Qty"))

            Dim bLrCode As Integer = FInsertBarCode(Conn, Cmd, bStockHeadDocId, 0, AgL.XNull(DtLrHeader.Rows(I)("TransporterCode")), bDescription, ItemCode.Lr, bSpecification1, bMfgDate, bSpecification3,
                               bSpecification4, bSpecification5, "")

            mQry = " UPDATE PurchInvoiceDetail Set LrCode = '" & bLrCode & "' Where DocId = '" & DocID & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Select Max(H.DocId) As DocId, Max(H.ManualRefNo) As InvoiceNo, IsNull(L.LRNo, Pit.LRNo) As LRNo, Max(L.LRDate) As LRDate, 
                            IsNull(L.BaleNo,Pit.LRNo) As BaleNo, Max(I.ItemCategory) As ItemCategory, Max(L.Godown) As Godown,
                            Max(Sg.Name) As TransporterName, Max(Pit.Transporter) As TransporterCode, 
                            Max(Pit.Weight) As Qty, Max(Pit.Freight) as Amount, Max(H.Net_Amount) As InvoiceAmount, Max(Pit.PrivateMark) As Specification
                            From PurchInvoice H With (NoLock)
                            LEFT JOIN PurchInvoiceDetail L With (NoLock) On H.DocId = L.DocId
                            LEFT JOIN PurchInvoiceTransport Pit With (NoLock) On H.DocId = Pit.DocId
                            LEFT JOIN Item I With (NoLock) On L.Item = I.Code
                            LEFT JOIN SubGroup Sg With (NoLock) On Pit.Transporter = Sg.SubCode
                            Where L.DocId = '" & DocID & "'
                            And IsNull(L.LRNo, Pit.LRNo) Is Not Null And IsNull(L.BaleNo,Pit.LRNo) Is Not Null
                            Group By IsNull(L.LRNo, Pit.LRNo), IsNull(L.BaleNo,Pit.LRNo) "
            Dim DtLrLine As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

            Dim bSr As Integer = 0
            For J As Integer = 0 To DtLrLine.Rows.Count - 1
                bSr += 1
                mQry = "Insert Into StockHeadDetail(DocId, Sr, Item, BaleNo, LotNo, Godown, Specification, Pcs, Qty, Unit,Amount) "
                mQry += " Select " & AgL.Chk_Text(bStockHeadDocId) & ", " & bSr & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("ItemCategory"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("BaleNo"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("InvoiceNo"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("Godown"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("Specification"))) & ", " &
                            " " & Val(AgL.VNull(DtLrLine.Rows(J)("InvoiceAmount"))) & ", " &
                            " " & Val(AgL.VNull(DtLrLine.Rows(J)("Qty"))) & ", " &
                            " 'Kg', " & Val(AgL.VNull(DtLrLine.Rows(J)("Amount"))) & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                InsertLRBaleDetail(bStockHeadDocId, bSr, DtLrLine, J, Conn, Cmd, bLrCode)
            Next

            AgL.UpdateVoucherCounter(bStockHeadDocId, CDate(bV_Date), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
        Next
    End Sub
    Private Function FInsertBarCode(Conn As Object, Cmd As Object, DocId As String, Sr As Integer,
                                    bTransporterCode As String,
                                    BarCodeDesc As String,
                                    bItemCode As String,
                                   bSpecification1 As String, bMfgDate As String,
                                   bSpecification3 As String, bSpecification4 As String,
                                   bSpecification5 As String, Optional Parent As String = "") As Integer
        Dim bMaxCode As Integer = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode With (NoLock)", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
        mQry = " INSERT INTO Barcode (Code, Description, Div_Code, Item, 
                    GenDocID, GenSr, Qty, ExpiryDate, 
                    GenSubcode, Specification1, Mfgdate, Specification3, Specification4, Specification5, Parent)
                    Select " & bMaxCode & ", " & AgL.Chk_Text(BarCodeDesc) & ", 
                    " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(bItemCode) & ",                     
                    " & AgL.Chk_Text(DocId) & " As GenDocID, " & Sr & " As gensr, 1 As qty, 
                    Null As expirydate, 
                    " & AgL.Chk_Text(bTransporterCode) & " As gensubcode, 
                    " & AgL.Chk_Text(bSpecification1) & " As Specification1, 
                    " & AgL.Chk_Date(bMfgDate) & " As Mfgdate, 
                    " & AgL.Chk_Text(bSpecification3) & " As Specification3, 
                    " & AgL.Chk_Text(bSpecification4) & " As Specification4, 
                    " & AgL.Chk_Text(bSpecification5) & " As Specification5,
                    " & AgL.Chk_Text(Parent) & " As Parent "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO BarcodeSiteDetail (Code, Div_Code, Site_Code, LastTrnDocID, LastTrnSr, 
                        LastTrnV_Type, LastTrnManualRefNo, LastTrnSubcode, LastTrnProcess, CurrentGodown, Status)
                        Select " & bMaxCode & ", " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ", 
                        " & AgL.Chk_Text(DocId) & " As lasttrndocid, " & Sr & " As lasttrnsr,
                        " & AgL.Chk_Text(TxtV_Type.Tag) & " As lasttrnv_type, 
                        " & AgL.Chk_Text(TxtReferenceNo.Text) & " As lasttrnmanualrefno, 
                        " & AgL.Chk_Text(bTransporterCode) & " As LastTrnSubcode, 
                        Null As lasttrnprocess, 
                        " & AgL.Chk_Text(bTransporterCode) & " As currentgodown, 
                        'Receive' As status "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                        SubCode, SalesTaxGroupParty, Godown, Barcode, Item, 
                        SalesTaxGroupItem,  LotNo, EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                        Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                        Select B.GenDocId AS DocID, B.GenSr AS TSr, B.GenSr AS Sr, " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", 
                        " & AgL.Chk_Date(TxtV_Date.Text) & ", " & AgL.Chk_Text(TxtV_No.Text) & ", 
                        " & AgL.Chk_Text(TxtReferenceNo.Text) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                        " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                        B.GenSubCode AS SubCode, NULL AS SalesTaxGroupParty, 
                        " & AgL.Chk_Text(bTransporterCode) & " As Godown,
                        B.Code AS Barcode, B.Item,                         
                        NULL AS SalesTaxGroupItem,  NULL AS LotNo, NULL AS EType_IR, 0 AS Qty_Iss, 1 AS Qty_Rec, 
                        I.Unit, NULL AS UnitMultiplier, 0 AS DealQty_Iss , 0 AS DealQty_Rec, NULL AS DealUnit, 
                        0 AS Rate, 0 AS Amount, 0 AS Landed_Value, NULL AS ReferenceDocID, NULL AS ReferenceTSr, 
                        NULL AS ReferenceDocIDSr
                        From Barcode B  
                        LEFT JOIN Item I ON B.Item = I.Code
                        Where B.GenDocId = '" & DocId & "' And B.GenSr = " & Sr & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO StockHeadDetailBarCodeValues (DocID, Sr, BarcodeLastTrnDocID, BarcodeLastTrnSr, 
                BarcodeLastTrnV_Type, BarcodeLastTrnManualRefNo, BarcodeLastTrnSubcode, BarcodeLastTrnProcess, 
                BarcodeCurrentGodown, BarcodeStatus)
                Select B.GenDocId As DocId, B.GenSr As Sr, Bs.LastTrnDocID As BarcodeLastTrnDocID, 
                Bs.LastTrnSr As BarcodeLastTrnSr, Bs.LastTrnV_Type As BarcodeLastTrnV_Type, 
                Bs.LastTrnManualRefNo As BarcodeLastTrnManualRefNo, Bs.LastTrnSubcode As BarcodeLastTrnSubcode, 
                Bs.LastTrnProcess As BarcodeLastTrnProcess, Bs.CurrentGodown As BarcodeCurrentGodown, 
                Bs.Status As BarcodeStatus
                From Barcode B
                LEFT JOIN (SELECT * FROM BarcodeSiteDetail WHERE Div_Code = '" & AgL.PubDivCode & "' 
                            AND Site_Code = '" & AgL.PubSiteCode & "') AS Bs ON B.Code = Bs.Code
                Where B.GenDocId = '" & DocId & "' And B.GenSr = " & Sr & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Return bMaxCode
    End Function
    Private Sub FUpdateBarCode(Conn As Object, Cmd As Object, DocId As String, Sr As Integer,
                               bTransporterCode As String,
                               BarCodeDesc As String,
                               bItemCode As String,
                               bSpecification1 As String, bMfgDate As String,
                               bSpecification3 As String, bSpecification4 As String,
                               bSpecification5 As String)
        mQry = " UPDATE Barcode
                        SET Description = " & AgL.Chk_Text(BarCodeDesc) & ",
	                        Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ",
	                        Item = " & AgL.Chk_Text(bItemCode) & ",
	                        Qty = 1,
	                        ExpiryDate = Null,
	                        GenSubcode = " & AgL.Chk_Text(bTransporterCode) & ",
	                        Specification1 = " & AgL.Chk_Text(bSpecification1) & ",
	                        MfgDate = " & AgL.Chk_Date(bMfgDate) & ",
	                        Specification3 = " & AgL.Chk_Text(bSpecification3) & ",
	                        Specification4 = " & AgL.Chk_Text(bSpecification4) & ",
	                        Specification5 = " & AgL.Chk_Text(bSpecification5) & "
                            Where GenDocId = '" & DocId & "' And GenSr = " & Sr & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Update Stock Set
                        V_Type = " & AgL.Chk_Text(TxtV_Type.Tag) & ", 
                        V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                        V_Date = " & AgL.Chk_Date(TxtV_Date.Text) & ", 
                        V_No = " & AgL.Chk_Text(TxtV_No.Text) & ", 
                        RecId = " & AgL.Chk_Text(TxtReferenceNo.Text) & ",  
                        Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                        Site_Code = " & AgL.Chk_Text(TxtSite_Code.Tag) & ",
                        Subcode = " & AgL.Chk_Text(TxtVendor.Tag) & ", 
                        Godown = " & AgL.Chk_Text(bTransporterCode) & ", 
                        SalesTaxGroupParty = Null,
                        Item = " & AgL.Chk_Text(bItemCode) & ", 
                        SalesTaxGroupItem = Null, 
                        LotNo = Null,
                        BaleNo = Null,
                        EType_IR = 'I', 
                        Qty_Iss = 0,
                        Qty_Rec = 1, 
                        Unit = 'Nos',
                        UnitMultiplier = 1,
                        DealQty_Iss = 0, 
                        DealQty_Rec =0,  
                        DealUnit = Null ,
                        Rate = 0, 
                        Amount = 0,
                        Landed_Value = 0,
                        ReferenceDocId = Null, 
                        ReferenceTSr = Null, 
                        ReferenceDocIdSr = NUll
                        Where DocId = '" & DocId & "' And Sr = " & Sr & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub InsertLRBaleDetail(DocID As String, Sr As Integer, DtLrBaleDetail As DataTable,
                                        bRowIndex As Integer, ByVal Conn As Object, ByVal Cmd As Object,
                                        LrCode As Integer)
        Dim bDescription As String = ""
        Dim bSpecification1 As String = ""
        Dim bMfgDate As String = ""
        Dim bSpecification3 As Decimal = 0
        Dim bSpecification4 As String = ""
        Dim bSpecification5 As String = ""

        bDescription = AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("LrNo")) + " -" + AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("TransporterName")) + "-" + "Bale No : " + AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("BaleNo"))
        bSpecification1 = AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("BaleNo"))
        bMfgDate = AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("LrDate"))
        bSpecification3 = AgL.VNull(DtLrBaleDetail.Rows(bRowIndex)("Qty"))
        bSpecification5 = AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("LrNo"))

        Dim bLrBaleCode As Integer = FInsertBarCode(Conn, Cmd, DocID, Sr, AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("TransporterCode")),
                                   bDescription, ItemCode.LrBale, bSpecification1, bMfgDate, bSpecification3,
                                   bSpecification4, bSpecification5, LrCode)

        mQry = " UPDATE PurchInvoiceDetail Set LrBaleCode = '" & bLrBaleCode & "'
                Where DocId = '" & AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("DocId")) & "'
                And BaleNo = '" & AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("BaleNo")) & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FUpdateLRDetail(DocID As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bDescription As String = ""
        Dim bSpecification1 As String = ""
        Dim bMfgDate As String = ""
        Dim bSpecification3 As Decimal = 0
        Dim bSpecification4 As String = ""
        Dim bSpecification5 As String = ""

        mQry = " Select L.LRCode, Max(Bc.GenDocId) As StockHeadDocId, 
                    Max(IsNull(L.LRNo, Pit.LRNo)) As LRNo, Max(Sg.Name) As TransporterName, Max(Pit.Transporter) As TransporterCode, 
                    Max(L.LRDate) As LRDate, Sum(L.Qty) As Qty, Max(H.Vendor) As Vendor, Max(H.V_Date) As InvoiceDate
                    From PurchInvoice H With (NoLock)
                    LEFT JOIN PurchInvoiceDetail L With (NoLock) ON H.DocId = L.DocId
                    LEFT JOIN PurchInvoiceTransport Pit With (NoLock) On H.DocId = Pit.DocId
                    LEFT JOIN SubGroup Sg On Pit.Transporter = Sg.SubCode
                    LEFT JOIN Item I On L.Item = I.Code
                    LEFT JOIN Barcode Bc On L.LRCode = Bc.Code
                    Where L.DocId = '" & DocID & "'
                    And L.LRCode Is Not Null
                    Group By L.LRCode "
        Dim DtLrHeader As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        For I As Integer = 0 To DtLrHeader.Rows.Count - 1
            mQry = " UPDATE StockHead
                    Set V_Date = " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("InvoiceDate"))) & ", 
                        SubCode = " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("Vendor"))) & ", 
                        Transporter = " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("TransporterCode"))) & ", 
                        PartyDocNo = " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("LrNo"))) & ", 
                        PartyDocDate = " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("LrDate"))) & ", 
                        GenDocId = " & AgL.Chk_Text(DocID) & ", 
                        EntryBy = " & AgL.Chk_Text(AgL.PubUserName) & ", 
                        EntryDate = " & AgL.Chk_Date(AgL.PubLoginDate) & ",  
                        LockText = " & AgL.Chk_Text("Auto Generated From " & TxtV_Type.Text & " " & TxtReferenceNo.Text) & "
                        Where DocId = " & AgL.Chk_Text(AgL.XNull(DtLrHeader.Rows(I)("StockHeadDocId"))) & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            bDescription = AgL.XNull(DtLrHeader.Rows(I)("LRNo")) + " -" + AgL.XNull(DtLrHeader.Rows(I)("TransporterName"))
            bSpecification1 = AgL.XNull(DtLrHeader.Rows(I)("LRNo"))
            bMfgDate = AgL.XNull(DtLrHeader.Rows(I)("LRDate"))
            bSpecification3 = AgL.VNull(DtLrHeader.Rows(I)("Qty"))

            FUpdateBarCode(Conn, Cmd, AgL.XNull(DtLrHeader.Rows(I)("StockHeadDocId")), 0,
                                    AgL.XNull(DtLrHeader.Rows(I)("TransporterCode")), bDescription, ItemCode.Lr, bSpecification1, bMfgDate, bSpecification3,
                                    bSpecification4, bSpecification5)

            mQry = " Select L.LrBaleCode, Max(Bc.GenDocId) As StockHeadDocId, Max(Bc.GenSr) As StockHeadSr,
                            Max(H.ManualRefNo) As InvoiceNo, Max(IsNull(L.LRNo, Pit.LRNo)) As LRNo, Max(L.LRDate) As LRDate, 
                            Max(IsNull(L.BaleNo,Pit.LRNo)) As BaleNo, Max(I.ItemCategory) As ItemCategory, Max(L.Godown) As Godown,
                            Max(Sg.Name) As TransporterName, Max(Pit.Transporter) As TransporterCode, 
                            Max(Pit.Weight) As Qty, Max(Pit.Freight) as Amount, Max(H.Net_Amount) As InvoiceAmount, Max(Pit.PrivateMark) As Specification
                            From PurchInvoice H With (NoLock)
                            LEFT JOIN PurchInvoiceDetail L With (NoLock) On H.DocId = L.DocId
                            LEFT JOIN PurchInvoiceTransport Pit With (NoLock) On H.DocId = Pit.DocId
                            LEFT JOIN Item I With (NoLock) On L.Item = I.Code
                            LEFT JOIN SubGroup Sg With (NoLock) On Pit.Transporter = Sg.SubCode
                            LEFT JOIN Barcode Bc With (NoLock) On L.LrBaleCode = Bc.Code
                            Where L.DocId = '" & DocID & "'
                            And L.LrBaleCode Is Not Null
                            Group By L.LrBaleCode "
            Dim DtLrLine As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

            Dim bSr As Integer = 0
            For J As Integer = 0 To DtLrLine.Rows.Count - 1
                bSr += 1
                mQry = " UPDATE StockHeadDetail
                            Set Item = " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("ItemCategory"))) & ", 
                            BaleNo = " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("BaleNo"))) & ", 
                            LotNo = " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("InvoiceNo"))) & ", 
                            Godown = " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("Godown"))) & ", 
                            Specification = " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(J)("Specification"))) & ", 
                            Pcs = " & Val(AgL.VNull(DtLrLine.Rows(J)("InvoiceAmount"))) & ", 
                            Qty = " & Val(AgL.VNull(DtLrLine.Rows(J)("Qty"))) & ", 
                            Unit = 'Kg', 
                            Amount = " & Val(AgL.VNull(DtLrLine.Rows(J)("Amount"))) & " 
                            Where DocId = " & AgL.Chk_Text(AgL.XNull(DtLrLine.Rows(I)("StockHeadDocId"))) & "
                            And Sr = " & Val(AgL.VNull(DtLrLine.Rows(I)("StockHeadSr"))) & ""
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                UpdateLRBaleDetail(AgL.XNull(DtLrHeader.Rows(I)("StockHeadDocId")),
                                   AgL.VNull(DtLrLine.Rows(I)("StockHeadSr")),
                                   DtLrLine, J, Conn, Cmd)
            Next
        Next
    End Sub
    Private Sub UpdateLRBaleDetail(DocID As String, Sr As Integer, DtLrBaleDetail As DataTable,
                                   bRowIndex As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bDescription As String = ""
        Dim bSpecification1 As String = ""
        Dim bMfgDate As String = ""
        Dim bSpecification3 As Decimal = 0
        Dim bSpecification4 As String = ""
        Dim bSpecification5 As Decimal = 0

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                bDescription = Dgl1.Item(Col1LRNo, I).Value + " -" + CType(BtnHeaderDetail.Tag, FrmPurchaseInvoiceHeader).Dgl1.Item(FrmPurchaseInvoiceHeader.Col1Value, FrmPurchaseInvoiceHeader.rowTransporter).Value + "-" + "Bale No : " + Dgl1.Item(Col1BaleNo, bRowIndex).Value
                bSpecification1 = Dgl1.Item(Col1BaleNo, bRowIndex).Value
                bMfgDate = Dgl1.Item(Col1LRDate, I).Value
                bSpecification3 = Val(Dgl1.Item(Col1Qty, bRowIndex).Value)
                bSpecification5 = Dgl1.Item(Col1LRNo, I).Value

                FUpdateBarCode(Conn, Cmd, DocID, Sr, AgL.XNull(DtLrBaleDetail.Rows(bRowIndex)("TransporterCode")),
                                   bDescription, ItemCode.LrBale, bSpecification1, bMfgDate, bSpecification3,
                                   bSpecification4, bSpecification5)
            End If
        Next
    End Sub
    Private Sub FrmPurchInvoiceDirect_BaseEvent_Topctrl_tbMore() Handles Me.BaseEvent_Topctrl_tbMore
        MnuOptions.Show(Topctrl1, Topctrl1.btbSite.Rectangle.X, Topctrl1.btbSite.Rectangle.Y + Topctrl1.btbSite.Rectangle.Size.Height)
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
    Private Sub FShowRefrentialEntries(bDocId As String)
        Dim FrmObj As New FrmReferenceEntries()
        FrmObj.SearchCode = bDocId
        FrmObj.LblDocNo.Text = "Entry No : " + TxtReferenceNo.Text
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.MdiParent = Me.MdiParent
        FrmObj.Show()
    End Sub
    Private Sub FShowHistory(SearchCode As String)
        Dim StrSenderText As String = "Log Report"
        GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
        GridReportFrm.Filter_IniGrid()
        Dim CRep As ClsReports = New ClsReports(GridReportFrm)
        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
        CRep.Ini_Grid()
        GridReportFrm.MdiParent = Me.MdiParent
        GridReportFrm.Show()
        CRep.ProcLogReport(,, SearchCode)
    End Sub
    Private Sub FrmPurchInvoiceDirect_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = (Keys.W And e.Control) Then
            If Topctrl1.Mode = "Add" Then
                ShowPurchaseInvoiceParty("", TxtVendor.Tag, TxtNature.Text)
            Else
                ShowPurchaseInvoiceParty(mSearchCode, "", TxtNature.Text)
            End If
        End If
    End Sub
    Private Sub FrmSaleInvoiceDirect_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If ClsMain.IsEntryLockedWithLockText("PurchInvoice", "DocId", mSearchCode) = True Then
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
    Private Sub FrmPurchInvoiceDirect_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
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
    Private Function FValidateSalesTaxGroup() As Boolean
        Dim bAllowedSalesTaxGroupParty As String = FGetSettings(SettingFields.AllowedSalesTaxGroupParty, SettingType.General)
        If bAllowedSalesTaxGroupParty <> "" Then
            Dim bSalesTaxPostingGroup As String = AgL.XNull(AgL.Dman_Execute("Select SalesTaxPostingGroup 
                                        From SubGroup 
                                        Where SubCode = '" & TxtVendor.Tag & "'", AgL.GCn).ExecuteScalar())
            If bAllowedSalesTaxGroupParty.ToUpper.Contains("+" + bSalesTaxPostingGroup.ToUpper) = False Then
                MsgBox(bSalesTaxPostingGroup + " Parties are not allowed for " & TxtV_Type.Text & "...!", MsgBoxStyle.Information)
                FValidateSalesTaxGroup = False : Exit Function
            End If
        End If
        FValidateSalesTaxGroup = True
    End Function
    Private Function FGetRelationalData() As Boolean
        Dim DtRelationalData As DataTable
        Try
            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From PurchInvoiceDetail L
                        LEFT JOIN PurchInvoice H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PurchInvoice = '" & mSearchCode & "' 
                        And L.PurchInvoice <> L.DocId "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Text + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From PurchInvoiceDetail L
                        LEFT JOIN PurchInvoice H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.ReferenceDocId = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Text + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From StockHeadDetail L
                        LEFT JOIN StockHead H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.ReferenceDocId = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Text + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From LedgerHeadDetail L
                        LEFT JOIN LedgerHead H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.SpecificationDocId = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Text + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From Cloth_SupplierSettlementPayments L
                        LEFT JOIN LedgerHead H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PaymentDocId = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Text + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From Cloth_SupplierSettlementInvoices L
                        LEFT JOIN LedgerHead H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PurchaseInvoiceDocId = '" & mSearchCode & "' "
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
    Private Sub FGetTransactionHistory(ByVal FrmObj As Form, ByVal mSearchCode As String, ByVal mQry As String,
                                     ByVal DGL As AgControls.AgDataGrid, ByVal DtV_TypeSettings As DataTable, ByVal Item As String)
        Dim DtTemp As DataTable = Nothing
        Dim CSV_Qry As String = ""
        Dim CSV_QryArr() As String = Nothing
        Dim I As Integer, J As Integer
        Dim IGridWidth As Integer = 0
        Try
            'If DtV_TypeSettings.Rows.Count <> 0 Then
            '    If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery")) <> "" Then
            '        mQry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery"))
            '        mQry = Replace(mQry.ToString.ToUpper, "`<ITEMCODE>`", "'" & Item & "'")
            '        mQry = Replace(mQry.ToString.ToUpper, "`<SEARCHCODE>`", "'" & mSearchCode & "'")
            '    End If

            '    If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv")) <> "" Then
            '        CSV_Qry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv"))
            '    End If
            'End If

            If CSV_Qry <> "" Then CSV_QryArr = Split(CSV_Qry, ",")
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count = 0 Then DGL.DataSource = Nothing : DGL.Visible = False : Exit Sub

            DGL.DataSource = DtTemp
            DGL.Visible = True
            FrmObj.Controls.Add(DGL)
            DGL.Left = FrmObj.Left + 3
            'DGL.Top = FrmObj.Bottom - DGL.Height - 130
            DGL.Top = PnlTotals.Bottom
            DGL.Height = 130
            DGL.Width = 450
            DGL.ColumnHeadersHeight = 40
            DGL.AllowUserToAddRows = False

            If DGL.Columns.Count > 0 Then
                If CSV_Qry <> "" Then J = CSV_QryArr.Length
                For I = 0 To DGL.ColumnCount - 1
                    If CSV_Qry <> "" Then
                        If I < J Then
                            If Val(CSV_QryArr(I)) > 0 Then
                                DGL.Columns(I).Width = Val(CSV_QryArr(I))
                            Else
                                DGL.Columns(I).Width = 100
                            End If
                        Else
                            DGL.Columns(I).Width = 100
                        End If
                    Else
                        DGL.Columns(I).Width = 100
                    End If
                    DGL.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                    IGridWidth += DGL.Columns(I).Width
                Next
                DGL.Columns(0).Width = 0

                DGL.ScrollBars = ScrollBars.None
                DGL.Width = IGridWidth - 50
                DGL.RowHeadersVisible = False
                DGL.EnableHeadersVisualStyles = False
                DGL.AllowUserToResizeRows = False
                DGL.ReadOnly = True
                DGL.AutoResizeRows()
                DGL.AutoResizeColumnHeadersHeight()
                DGL.BackgroundColor = Color.Cornsilk
                DGL.ColumnHeadersDefaultCellStyle.BackColor = Color.Cornsilk
                DGL.DefaultCellStyle.BackColor = Color.Cornsilk
                DGL.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                DGL.CellBorderStyle = DataGridViewCellBorderStyle.None
                DGL.Font = New Font(New FontFamily("Verdana"), 8)
                DGL.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                DGL.BringToFront()
                DGL.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
