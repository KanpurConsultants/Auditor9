Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq
Imports Customised.ClsMain

Public Class FrmStockEntry
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1Barcode As String = "Barcode"
    Public Const Col1ItemType As String = "Item Type"
    Public Const Col1SKU As String = "SKU"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1ItemState As String = "Item State"
    Public Const Col1BaleNo As String = "Bale No"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1RawMaterial As String = "Raw Material"
    Public Const Col1RawMaterialConsumptionQty As String = "Raw Material Consumption Qty"
    Public Const Col1Godown As String = "Godown"
    Public Const Col1DocQty As String = "Doc Qty"
    Public Const Col1LossQty As String = "Loss Qty"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1Pcs As String = "Pcs"
    Public Const Col1UnitMultiplier As String = "Unit Multiplier"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1DealUnit As String = "Deal Unit"
    Public Const Col1DealUnitDecimalPlaces As String = "Deal Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1FromProcess As String = "From Process"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1ReferenceDocId As String = "Reference DocID"
    Public Const Col1ReferenceDocIdTSr As String = "Reference TSr"
    Public Const Col1ReferenceDocIdSr As String = "Reference Sr"
    Public Const Col1ReferenceDocIdDate As String = "Reference DocID Date"
    Public Const Col1ReferenceDocIdBalanceQty As String = "Reference DocID Balance Qty"
    Public Const Col1StockSr As String = "Stock Sr"
    Public Const Col1StockProcess As String = "Stock Process"
    Public Const Col1StockProcessTSr As String = "Stock Process TSr"
    Public Const Col1StockProcessSr As String = "Stock Process Sr"
    Public Const Col1BtnBaseDetail As String = "Base Detail"
    Public Const Col1StockQty As String = "Stock Qty"
    Public Const Col1StockProcessQty As String = "Stock Process Qty"
    Public Const Col1IsRecordLocked As String = "Is Record Locked"

    Public Const Col1MItemCategory As String = "M Item Category"
    Public Const Col1MItemGroup As String = "M Item Group"
    Public Const Col1MItemSpecification As String = "M Item Specification"
    Public Const Col1MDimension1 As String = "M Dimension 1"
    Public Const Col1MDimension2 As String = "M Dimension 2"
    Public Const Col1MDimension3 As String = "M Dimension 3"
    Public Const Col1MDimension4 As String = "M Dimension 4"
    Public Const Col1MSize As String = "M Size"
    '========================================================================

    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public rowProcess As Integer = 6
    Public rowFirstProcessOfCombination As Integer = 7
    Public rowLastProcessOfCombination As Integer = 8
    Public rowParty As Integer = 9
    Public rowPartyDocNo As Integer = 10
    Public rowPartyDocDate As Integer = 11
    Public rowTransporter As Integer = 12

    Public rowGodown As Integer = 0
    Public rowFromGodown As Integer = 1
    Public rowToGodown As Integer = 2
    Public rowResponsiblePerson As Integer = 3
    Public rowInsurancePolicyNo As Integer = 4
    Public rowInsuranceBalanceValue As Integer = 5
    Public rowInsuredValue As Integer = 6
    Public rowReferenceDocId As Integer = 7
    Public rowReason As Integer = 8
    Public rowRemarks As Integer = 9
    Public rowRemarks1 As Integer = 10
    Public rowRemarks2 As Integer = 11
    Public rowBtnPendingPurchOrder As Integer = 12
    Public rowBtnStockBalance As Integer = 13
    Public rowBtnStandardConsumption As Integer = 14
    Public rowBtnAttachments As Integer = 15


    Public Const hcProcess As String = "Process"
    Public Const hcFirstProcessOfCombination As String = "First Process Of Combination"
    Public Const hcLastProcessOfCombination As String = "Last Process Of Combination"
    Public Const hcParty As String = "Party"
    Public Const hcPartyDocNo As String = "Party Doc No"
    Public Const hcPartyDocDate As String = "Party Doc Date"
    Public Const hcGodown As String = "Godown"
    Public Const hcFromGodown As String = "From Godown"
    Public Const hcToGodown As String = "To Godown"
    Public Const hcResponsiblePerson As String = "Responsible Person"
    Public Const hcTransporter As String = "Transporter"
    Public Const hcInsurancePolicyNo As String = "Ins.Policy No"
    Public Const hcInsuranceBalanceValue As String = "Ins.Balance Value"
    Public Const hcInsuredValue As String = "Insured Value"
    Public Const hcReferenceDocId As String = "Reference No."
    Public Const hcReason As String = "Reason"
    Public Const hcRemarks As String = "Remarks"
    Public Const hcRemarks1 As String = "Remarks1"
    Public Const hcRemarks2 As String = "Remarks2"
    Public Const hcBtnPendingPurchOrder As String = "Pending Orders"
    Public Const hcBtnStockBalance As String = "Stock Balance"
    Public Const hcBtnStandardConsumption As String = "Fill Consumption"
    Public Const hcBtnAttachments As String = "Attachments"

    Dim mLinkedDocId As String = ""

    Dim bInsuranceNoBarcodeSr As Integer = -1
    Dim bLrNoBarcodeSr As Integer = 0

    Dim DtV_TypeTrnSettings As DataTable

    Public WithEvents Dgl4 As New AgControls.AgDataGrid
    Public Const Col4Sku As String = "Sku"
    Public Const Col4ItemCategory As String = "Item Category"
    Public Const Col4Item As String = "Item"
    Public Const Col4Dimension1 As String = "Dimension1"
    Public Const Col4Dimension2 As String = "Dimension2"
    Public Const Col4Dimension3 As String = "Dimension3"
    Public Const Col4Dimension4 As String = "Dimension4"
    Public Const Col4Qty As String = "Qty"
    Public Const Col4Unit As String = "Unit"
    Public Const Col4StockProcessQty As String = "Stock Process Qty"

    Dim mV_Type As String = ""
    Dim DtItemRelation As DataTable
    Public Shared mFlag_Import As Boolean = False
    Dim mPrevRowIndex As Integer = 0
    Friend WithEvents MnuShowLedgerPosting As ToolStripMenuItem
    Dim Dgl As New AgControls.AgDataGrid
    Public Property LinkedDocId() As String
        Get
            LinkedDocId = mLinkedDocId
        End Get
        Set(ByVal value As String)
            mLinkedDocId = value
        End Set
    End Property

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmStockEntry))
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.LblCurrency = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LblNature = New System.Windows.Forms.Label()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox()
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblStockProcess = New System.Windows.Forms.Label()
        Me.LblStock = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblTotalBale = New System.Windows.Forms.Label()
        Me.LblTotalBaleText = New System.Windows.Forms.Label()
        Me.LblDealQty = New System.Windows.Forms.Label()
        Me.LblDealQtyText = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalAmount = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalAmountText = New System.Windows.Forms.Label()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportOpeningFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEditSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuWizard = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrintQACopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pnl4 = New System.Windows.Forms.Panel()
        Me.PnlConsumptionTotal = New System.Windows.Forms.Panel()
        Me.LblTotalBomQty = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtNature = New AgControls.AgTextBox()
        Me.MnuShowLedgerPosting = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.PnlTotals.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.PnlConsumptionTotal.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(644, 581)
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
        Me.GBoxMoveToLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(144, 581)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(140, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(134, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxApprove.Location = New System.Drawing.Point(482, 581)
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
        Me.GrpUP.Location = New System.Drawing.Point(16, 581)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 577)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxDivision.Location = New System.Drawing.Point(310, 581)
        Me.GBoxDivision.Size = New System.Drawing.Size(146, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Size = New System.Drawing.Size(140, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtDocId
        '
        Me.TxtDocId.AgSelectedValue = ""
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
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
        Me.TabControl1.Size = New System.Drawing.Size(992, 219)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Pnl2)
        Me.TP1.Controls.Add(Me.LblNature)
        Me.TP1.Controls.Add(Me.Panel3)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 193)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblNCatNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.PnlMain, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'PnlMain
        '
        Me.PnlMain.Location = New System.Drawing.Point(1, 3)
        Me.PnlMain.Size = New System.Drawing.Size(490, 187)
        Me.PnlMain.TabIndex = 0
        '
        'ChkTemporarilySaved
        '
        Me.ChkTemporarilySaved.Location = New System.Drawing.Point(795, 599)
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
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(4, 261)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 189)
        Me.Pnl1.TabIndex = 2
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
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 240)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(197, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Detail For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNature
        '
        Me.LblNature.AutoSize = True
        Me.LblNature.BackColor = System.Drawing.Color.Transparent
        Me.LblNature.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNature.Location = New System.Drawing.Point(622, 211)
        Me.LblNature.Name = "LblNature"
        Me.LblNature.Size = New System.Drawing.Size(46, 16)
        Me.LblNature.TabIndex = 745
        Me.LblNature.Text = "Nature"
        Me.LblNature.Visible = False
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
        'GBoxImportFromExcel
        '
        Me.GBoxImportFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxImportFromExcel.BackColor = System.Drawing.Color.Transparent
        Me.GBoxImportFromExcel.Controls.Add(Me.BtnImprtFromExcel)
        Me.GBoxImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxImportFromExcel.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxImportFromExcel.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(678, 576)
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
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(69, 11)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(25, 32)
        Me.BtnImprtFromExcel.TabIndex = 669
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(4, 221)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 187)
        Me.Panel3.TabIndex = 2
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(493, 3)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(491, 187)
        Me.Pnl2.TabIndex = 1
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.PnlCustomGrid.Location = New System.Drawing.Point(573, 600)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(33, 22)
        Me.PnlCustomGrid.TabIndex = 3019
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblStockProcess)
        Me.PnlTotals.Controls.Add(Me.LblStock)
        Me.PnlTotals.Controls.Add(Me.Label3)
        Me.PnlTotals.Controls.Add(Me.Label2)
        Me.PnlTotals.Controls.Add(Me.LblTotalBale)
        Me.PnlTotals.Controls.Add(Me.LblTotalBaleText)
        Me.PnlTotals.Controls.Add(Me.LblDealQty)
        Me.PnlTotals.Controls.Add(Me.LblDealQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(7, 450)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 695
        '
        'LblStockProcess
        '
        Me.LblStockProcess.AutoSize = True
        Me.LblStockProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStockProcess.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblStockProcess.Location = New System.Drawing.Point(929, 4)
        Me.LblStockProcess.Name = "LblStockProcess"
        Me.LblStockProcess.Size = New System.Drawing.Size(12, 16)
        Me.LblStockProcess.TabIndex = 3028
        Me.LblStockProcess.Text = "."
        Me.LblStockProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblStock
        '
        Me.LblStock.AutoSize = True
        Me.LblStock.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStock.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblStock.Location = New System.Drawing.Point(769, 4)
        Me.LblStock.Name = "LblStock"
        Me.LblStock.Size = New System.Drawing.Size(12, 16)
        Me.LblStock.TabIndex = 3026
        Me.LblStock.Text = "."
        Me.LblStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(829, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 3027
        Me.Label3.Text = "Stock Process"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(724, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 16)
        Me.Label2.TabIndex = 3025
        Me.Label2.Text = "Stock"
        '
        'LblTotalBale
        '
        Me.LblTotalBale.AutoSize = True
        Me.LblTotalBale.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBale.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBale.Location = New System.Drawing.Point(439, 4)
        Me.LblTotalBale.Name = "LblTotalBale"
        Me.LblTotalBale.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBale.TabIndex = 3024
        Me.LblTotalBale.Text = "."
        Me.LblTotalBale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalBaleText
        '
        Me.LblTotalBaleText.AutoSize = True
        Me.LblTotalBaleText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBaleText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalBaleText.Location = New System.Drawing.Point(347, 3)
        Me.LblTotalBaleText.Name = "LblTotalBaleText"
        Me.LblTotalBaleText.Size = New System.Drawing.Size(86, 16)
        Me.LblTotalBaleText.TabIndex = 3023
        Me.LblTotalBaleText.Text = "Total Bales :"
        '
        'LblDealQty
        '
        Me.LblDealQty.AutoSize = True
        Me.LblDealQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblDealQty.Location = New System.Drawing.Point(261, 3)
        Me.LblDealQty.Name = "LblDealQty"
        Me.LblDealQty.Size = New System.Drawing.Size(12, 16)
        Me.LblDealQty.TabIndex = 3022
        Me.LblDealQty.Text = "."
        Me.LblDealQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblDealQtyText
        '
        Me.LblDealQtyText.AutoSize = True
        Me.LblDealQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblDealQtyText.Location = New System.Drawing.Point(155, 3)
        Me.LblDealQtyText.Name = "LblDealQtyText"
        Me.LblDealQtyText.Size = New System.Drawing.Size(105, 16)
        Me.LblDealQtyText.TabIndex = 3021
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
        Me.LblTotalQty.Location = New System.Drawing.Point(80, 3)
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
        Me.LblTotalAmount.Location = New System.Drawing.Point(631, 4)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(5, 3)
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
        Me.LblTotalAmountText.Location = New System.Drawing.Point(523, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
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
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportOpeningFromExcel, Me.MnuEditSave, Me.MnuWizard, Me.MnuHistory, Me.MnuShowLedgerPosting, Me.MnuReport, Me.MnuPrintQACopy})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(220, 202)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(219, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportOpeningFromExcel
        '
        Me.MnuImportOpeningFromExcel.Name = "MnuImportOpeningFromExcel"
        Me.MnuImportOpeningFromExcel.Size = New System.Drawing.Size(219, 22)
        Me.MnuImportOpeningFromExcel.Text = "Import Opening From Excel"
        '
        'MnuEditSave
        '
        Me.MnuEditSave.Name = "MnuEditSave"
        Me.MnuEditSave.Size = New System.Drawing.Size(219, 22)
        Me.MnuEditSave.Text = "Edit & Save"
        '
        'MnuWizard
        '
        Me.MnuWizard.Name = "MnuWizard"
        Me.MnuWizard.Size = New System.Drawing.Size(219, 22)
        Me.MnuWizard.Text = "Wizard"
        '
        'MnuHistory
        '
        Me.MnuHistory.Name = "MnuHistory"
        Me.MnuHistory.Size = New System.Drawing.Size(219, 22)
        Me.MnuHistory.Text = "History"
        '
        'MnuReport
        '
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(219, 22)
        Me.MnuReport.Text = "Report"
        '
        'MnuPrintQACopy
        '
        Me.MnuPrintQACopy.Name = "MnuPrintQACopy"
        Me.MnuPrintQACopy.Size = New System.Drawing.Size(219, 22)
        Me.MnuPrintQACopy.Text = "Print QA Copy"
        '
        'Pnl4
        '
        Me.Pnl4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl4.Location = New System.Drawing.Point(7, 475)
        Me.Pnl4.Name = "Pnl4"
        Me.Pnl4.Size = New System.Drawing.Size(652, 86)
        Me.Pnl4.TabIndex = 3023
        Me.Pnl4.Visible = False
        '
        'PnlConsumptionTotal
        '
        Me.PnlConsumptionTotal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlConsumptionTotal.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlConsumptionTotal.Controls.Add(Me.LblTotalBomQty)
        Me.PnlConsumptionTotal.Controls.Add(Me.Label5)
        Me.PnlConsumptionTotal.Location = New System.Drawing.Point(7, 562)
        Me.PnlConsumptionTotal.Name = "PnlConsumptionTotal"
        Me.PnlConsumptionTotal.Size = New System.Drawing.Size(652, 20)
        Me.PnlConsumptionTotal.TabIndex = 3024
        '
        'LblTotalBomQty
        '
        Me.LblTotalBomQty.AutoSize = True
        Me.LblTotalBomQty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBomQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBomQty.Location = New System.Drawing.Point(91, 3)
        Me.LblTotalBomQty.Name = "LblTotalBomQty"
        Me.LblTotalBomQty.Size = New System.Drawing.Size(13, 16)
        Me.LblTotalBomQty.TabIndex = 660
        Me.LblTotalBomQty.Text = "."
        Me.LblTotalBomQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(12, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 14)
        Me.Label5.TabIndex = 659
        Me.Label5.Text = "Total Qty :"
        '
        'TxtNature
        '
        Me.TxtNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtNature.AgLastValueTag = Nothing
        Me.TxtNature.AgLastValueText = Nothing
        Me.TxtNature.AgMandatory = False
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
        Me.TxtNature.Location = New System.Drawing.Point(896, 243)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(81, 15)
        Me.TxtNature.TabIndex = 3025
        Me.TxtNature.Text = "TxtNature"
        Me.TxtNature.Visible = False
        '
        'MnuShowLedgerPosting
        '
        Me.MnuShowLedgerPosting.Name = "MnuShowLedgerPosting"
        Me.MnuShowLedgerPosting.Size = New System.Drawing.Size(219, 22)
        Me.MnuShowLedgerPosting.Text = "Show Ledger Posting"
        '
        'FrmStockEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.TxtNature)
        Me.Controls.Add(Me.PnlConsumptionTotal)
        Me.Controls.Add(Me.Pnl4)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmStockEntry"
        Me.Text = "StockHead Entry"
        Me.Controls.SetChildIndex(Me.ChkTemporarilySaved, 0)
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
        Me.Controls.SetChildIndex(Me.Pnl4, 0)
        Me.Controls.SetChildIndex(Me.PnlConsumptionTotal, 0)
        Me.Controls.SetChildIndex(Me.TxtNature, 0)
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
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.MnuOptions.ResumeLayout(False)
        Me.PnlConsumptionTotal.ResumeLayout(False)
        Me.PnlConsumptionTotal.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents LblCurrency As System.Windows.Forms.Label
    Public WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Public WithEvents LblNature As System.Windows.Forms.Label
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Public WithEvents Panel3 As System.Windows.Forms.Panel
    Private components As System.ComponentModel.IContainer
    Public mDimensionSrl As Integer
    Public WithEvents Pnl2 As Panel
    Public WithEvents PnlCustomGrid As Panel
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalQty As Label
    Public WithEvents LblTotalAmount As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LblTotalAmountText As Label
    Public WithEvents LblDealQty As Label
    Public WithEvents LblDealQtyText As Label
    Public WithEvents LblTotalBale As Label
    Public WithEvents LblTotalBaleText As Label
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuHistory As ToolStripMenuItem
    Friend WithEvents MnuReport As ToolStripMenuItem
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuEditSave As ToolStripMenuItem
    Friend WithEvents MnuImportOpeningFromExcel As ToolStripMenuItem
    Friend WithEvents MnuWizard As ToolStripMenuItem
    Public WithEvents Pnl4 As Panel
    Public WithEvents PnlConsumptionTotal As Panel
    Public WithEvents LblTotalBomQty As Label
    Public WithEvents Label5 As Label
    Public WithEvents LblStock As Label
    Public WithEvents Label2 As Label
    Public WithEvents LblStockProcess As Label
    Public WithEvents Label3 As Label
    Public WithEvents TxtNature As AgControls.AgTextBox
    Friend WithEvents MnuPrintQACopy As ToolStripMenuItem
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "StockHeadDetail,StockHeadDetailBase,StockHeadDetailSku"
        LogLineTableCsv = "StockHeadDetail_Log,StockHeadDetailBase_Log,StockHeadDetailSku_Log"

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name

        If CType(AgL.VNull(ClsMain.FGetSettings(SettingFields.AskVoucherTypeBeforeOpeningEntry, SettingType.General, TxtDivision.Tag, AgL.PubSiteCode, "PURCH", EntryNCat, "", "", "")), Boolean) = True Then
            FShowVoucherTypeHelp()
        End If
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        'mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
        '                " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = " And ( Date(H.V_Date) >= " & AgL.Chk_Date(AgL.PubStartDate) & " And  Date(H.V_Date) <= " & AgL.Chk_Date(AgL.PubEndDate) & " Or Vt.NCat='" & Ncat.OpeningStock & "') And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        If mV_Type <> "" Then
            mCondStr += " And H.V_Type = '" & mV_Type & "' "
        End If

        mQry = "Select DocID As SearchCode 
                From StockHead H  With (NoLock)
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  
                Where 1 = 1  " & mCondStr & "  Order By V_Date , V_No  "

        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        'mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
        '                " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = " And ( Date(H.V_Date) >= " & AgL.Chk_Date(AgL.PubStartDate) & " And Date(H.V_Date) <= " & AgL.Chk_Date(AgL.PubEndDate) & " OR VT.NCAT='" & Ncat.OpeningStock & "' )"
        mCondStr += " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        If mV_Type <> "" Then
            mCondStr += " And H.V_Type = '" & mV_Type & "' "
        End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [StockHead_Type], Cast(strftime('%d/%m/%Y', H.V_Date) As nvarchar) AS Date, SGV.Name AS [Party], " &
                            " H.ManualRefNo AS [Manual_No], H.Remarks,  " &
                            " I.Description as ItemName, L.Qty, H.EntryBy AS [Entry_By], Cast(strftime('%d/%m/%Y', H.EntryDate) As nvarchar) AS [Entry_Date] " &
                            " FROM StockHead H  With (NoLock) " &
                            " LEFT JOIN StockHeadDetail L  With (NoLock) ON H.DocID = L.DocID " &
                            " LEFT JOIN Item I  With (NoLock) ON L.Item = I.Code " &
                            " LEFT JOIN Voucher_Type Vt  With (NoLock) ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup SGV  With (NoLock) ON SGV.SubCode  = H.SubCode " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Barcode, 100, 0, AgL.PubCaptionBarcode, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, AgL.PubCaptionItemType, False, False)
            .AddAgTextColumn(Dgl1, Col1SKU, 300, 0, Col1SKU, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, AgL.PubCaptionItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, AgL.PubCaptionItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, AgL.PubCaptionItem & " Code", True, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 230, 0, AgL.PubCaptionItem, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 130, 0, Col1Specification, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemState, 130, 0, Col1ItemState, True, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 60, 255, Col1BaleNo, True, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 60, 255, Col1LotNo, True, False)
            .AddAgTextColumn(Dgl1, Col1RawMaterial, 100, 0, Col1RawMaterial, True, False)
            .AddAgNumberColumn(Dgl1, Col1RawMaterialConsumptionQty, 80, 10, 4, False, Col1RawMaterialConsumptionQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Godown, 60, 255, Col1Godown, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 80, 10, 4, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1LossQty, 70, 8, 4, False, Col1LossQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 10, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Pcs, 80, 8, 4, False, Col1Pcs, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1UnitMultiplier, 70, 8, 4, False, Col1UnitMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DealQty, 70, 8, 3, False, Col1DealQty, False, True, True)
            .AddAgTextColumn(Dgl1, Col1DealUnit, 60, 0, Col1DealUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1DealUnitDecimalPlaces, 50, 0, Col1DealUnitDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 100, 255, Col1FromProcess, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocId, 100, 0, Col1ReferenceDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocIdTSr, 40, 5, Col1ReferenceDocIdTSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocIdSr, 40, 5, Col1ReferenceDocIdSr, False, True, False)
            .AddAgDateColumn(Dgl1, Col1ReferenceDocIdDate, 80, Col1ReferenceDocIdDate, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReferenceDocIdBalanceQty, 70, 8, 3, False, Col1ReferenceDocIdBalanceQty, False, True, True)
            .AddAgButtonColumn(Dgl1, Col1BtnBaseDetail, 45, Col1BtnBaseDetail, True, False)

            .AddAgNumberColumn(Dgl1, Col1StockQty, 80, 8, 4, False, Col1StockQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1StockProcessQty, 80, 8, 4, False, Col1StockProcessQty, True, False, True)

            .AddAgTextColumn(Dgl1, Col1StockSr, 150, 255, Col1StockSr, False, False)
            .AddAgTextColumn(Dgl1, Col1IsRecordLocked, 150, 255, Col1IsRecordLocked, False, False)


            .AddAgTextColumn(Dgl1, Col1StockProcess, 150, 255, Col1StockProcess, False, False)
            .AddAgTextColumn(Dgl1, Col1StockProcessTSr, 150, 255, Col1StockProcessTSr, False, False)
            .AddAgTextColumn(Dgl1, Col1StockProcessSr, 150, 255, Col1StockProcessSr, False, False)

            .AddAgTextColumn(Dgl1, Col1MItemCategory, 100, 0, Col1MItemCategory, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemGroup, 100, 0, Col1MItemGroup, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemSpecification, 100, 0, Col1MItemSpecification, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension1, 100, 0, "M " & AgL.PubCaptionDimension1, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension2, 100, 0, "M " & AgL.PubCaptionDimension2, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension3, 100, 0, "M " & AgL.PubCaptionDimension3, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension4, 100, 0, "M " & AgL.PubCaptionDimension4, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MSize, 100, 0, Col1MSize, True, False, False)
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
        Dgl1.BackgroundColor = Me.BackColor
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)
        AgL.ProcCreateLink(Dgl1, Col1ReferenceDocId)

        If LblV_Type.Tag <> Ncat.LrEntry Then
            Dgl1.Columns(Col1Amount).ReadOnly = True
            Dgl1.Columns(Col1Amount).DefaultCellStyle.BackColor = Color.White
        End If


        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If

        Dgl4.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl4, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl4, Col4Sku, 150, 0, Col4Sku, True, True)
            .AddAgTextColumn(Dgl4, Col4ItemCategory, 150, 0, Col4ItemCategory, True, True)
            .AddAgTextColumn(Dgl4, Col4Item, 150, 0, Col4Item, True, True)
            .AddAgTextColumn(Dgl4, Col4Dimension1, 150, 0, Col4Dimension1, True, True)
            .AddAgTextColumn(Dgl4, Col4Dimension2, 150, 0, Col4Dimension2, True, True)
            .AddAgTextColumn(Dgl4, Col4Dimension3, 150, 0, Col4Dimension3, True, True)
            .AddAgTextColumn(Dgl4, Col4Dimension4, 150, 0, Col4Dimension4, True, True)
            .AddAgNumberColumn(Dgl4, Col4Qty, 100, 8, 4, False, Col4Qty, True, True, True)
            .AddAgTextColumn(Dgl4, Col4Unit, 50, 0, Col4Unit, True, True)
            .AddAgNumberColumn(Dgl4, Col4StockProcessQty, 100, 8, 4, False, Col4StockProcessQty, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl4, Pnl4)
        Dgl4.EnableHeadersVisualStyles = False
        Dgl4.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl4)
        Dgl4.AgSkipReadOnlyColumns = True
        Dgl4.AgAllowFind = False
        Dgl4.AllowUserToOrderColumns = True
        Dgl4.AllowUserToAddRows = False
        Dgl4.BackgroundColor = Me.BackColor
        Dgl4.Name = "Dgl4"
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl4, AgL)
        Dgl4.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right


        DglMain.Rows.Add(7)
        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next
        DglMain.Item(Col1Head, rowProcess).Value = hcProcess
        DglMain.Item(Col1Head, rowFirstProcessOfCombination).Value = hcFirstProcessOfCombination
        DglMain.Item(Col1Head, rowLastProcessOfCombination).Value = hcLastProcessOfCombination
        DglMain.Item(Col1Head, rowParty).Value = hcParty
        DglMain.Item(Col1Head, rowPartyDocNo).Value = hcPartyDocNo
        DglMain.Item(Col1Head, rowPartyDocDate).Value = hcPartyDocDate
        DglMain.Item(Col1Head, rowTransporter).Value = hcTransporter
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None

        DglMain.Columns(Col1BtnDetail).Visible = True
        DglMain.Columns(Col1Head).Width = 105

        For I = 0 To DglMain.Rows.Count - 1
            If AgL.XNull(DglMain(Col1HeadOriginal, I).Value) = "" Then
                DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
            End If
            If I <> rowParty Then
                DglMain.Item(Col1BtnDetail, I) = New DataGridViewTextBoxCell
            End If
        Next

        If Dgl2.Columns.Count = 0 Then
            Dgl2.ColumnCount = 0
            With AgCL
                .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
                .AddAgTextColumn(Dgl2, Col1Head, 140, 255, Col1Head, True, True)
                .AddAgTextColumn(Dgl2, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
                .AddAgTextColumn(Dgl2, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
                .AddAgTextColumn(Dgl2, Col1Value, 320, 255, Col1Value, True, False)
                .AddAgTextColumn(Dgl2, Col1LastValue, 170, 255, Col1Value, False, False)
            End With
            AgL.AddAgDataGrid(Dgl2, Pnl2)
            AgL.GridDesign(Dgl2)
            Dgl2.EnableHeadersVisualStyles = False
            Dgl2.ColumnHeadersHeight = 35
            Dgl2.AgSkipReadOnlyColumns = True
            Dgl2.AllowUserToAddRows = False
            Dgl2.RowHeadersVisible = False
            Dgl2.ColumnHeadersVisible = False
            Dgl2.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
            Dgl2.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
            Dgl2.BackgroundColor = Me.BackColor
            Dgl2.BorderStyle = BorderStyle.None
        End If

        Dgl2.Rows.Add(16)
        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2.Rows(I).Visible = False
        Next


        Dgl2.Item(Col1Head, rowGodown).Value = hcGodown
        Dgl2.Item(Col1Head, rowFromGodown).Value = hcFromGodown
        Dgl2.Item(Col1Head, rowToGodown).Value = hcToGodown
        Dgl2.Item(Col1Head, rowResponsiblePerson).Value = hcResponsiblePerson
        Dgl2.Item(Col1Head, rowInsurancePolicyNo).Value = hcInsurancePolicyNo
        Dgl2.Item(Col1Head, rowInsuranceBalanceValue).Value = hcInsuranceBalanceValue
        Dgl2.Item(Col1Head, rowInsuredValue).Value = hcInsuredValue
        Dgl2.Item(Col1Head, rowReferenceDocId).Value = hcReferenceDocId
        Dgl2.Item(Col1Head, rowReason).Value = hcReason
        Dgl2.Item(Col1Head, rowRemarks).Value = hcRemarks
        Dgl2.Item(Col1Head, rowRemarks1).Value = hcRemarks1
        Dgl2.Item(Col1Head, rowRemarks2).Value = hcRemarks2
        Dgl2.Item(Col1Head, rowBtnPendingPurchOrder).Value = hcBtnPendingPurchOrder
        Dgl2.Item(Col1Value, rowBtnPendingPurchOrder) = New DataGridViewButtonCell
        Dgl2.Item(Col1Head, rowBtnStockBalance).Value = hcBtnStockBalance
        Dgl2.Item(Col1Value, rowBtnStockBalance) = New DataGridViewButtonCell
        Dgl2.Item(Col1Head, rowBtnStandardConsumption).Value = hcBtnStandardConsumption
        Dgl2.Item(Col1Value, rowBtnStandardConsumption) = New DataGridViewButtonCell
        Dgl2.Item(Col1Head, rowBtnAttachments).Value = hcBtnAttachments
        Dgl2.Item(Col1Value, rowBtnAttachments) = New DataGridViewButtonCell



        Dgl2.Name = "Dgl2"
        Dgl2.Tag = "VerticalGrid"

        For I = 0 To Dgl2.Rows.Count - 1
            If AgL.XNull(Dgl2(Col1HeadOriginal, I).Value) = "" Then
                Dgl2(Col1HeadOriginal, I).Value = Dgl2(Col1Head, I).Value
            End If
        Next

        ApplyUISetting()

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCustomGrid1.Name = "AgCustomGrid1"

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & Dgl4.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl4, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bStockHeadSelectionQry$ = "", bHelpValuesSelectionQry$ = ""

        If DglMain.Item(Col1BtnDetail, rowParty).Tag Is Nothing Then DglMain.Item(Col1BtnDetail, rowParty).Tag = New FrmStockEntryParty

        mQry = " Update StockHead " &
                " SET  " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " Process = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & ", " &
                " SubCode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " &
                " PartyDocNo = " & AgL.Chk_Text(DglMain(Col1Value, rowPartyDocNo).Value) & ", " &
                " PartyDocDate = " & AgL.Chk_Date(DglMain(Col1Value, rowPartyDocDate).Value) & ", " &
                " ResponsiblePerson = " & AgL.Chk_Text(Dgl2(Col1Value, rowResponsiblePerson).Tag) & ", " &
                " Transporter = " & AgL.Chk_Text(DglMain(Col1Value, rowTransporter).Tag) & ", " &
                " InsurancePolicyNo = " & AgL.Chk_Text(Dgl2(Col1Value, rowInsurancePolicyNo).Tag) & ", " &
                " InsuredValue = " & Val(Dgl2(Col1Value, rowInsuredValue).Value) & ", " &
                " Reason = " & AgL.Chk_Text(Dgl2(Col1Value, rowReason).Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(Dgl2(Col1Value, rowRemarks).Value) & ", " &
                " Remarks1 = " & AgL.Chk_Text(Dgl2(Col1Value, rowRemarks1).Value) & ", " &
                " Remarks2 = " & AgL.Chk_Text(Dgl2(Col1Value, rowRemarks2).Value) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If DglMain.Rows(rowParty).Visible = True Then
            CType(DglMain.Item(Col1BtnDetail, rowParty).Tag, FrmStockEntryParty).FSave(mSearchCode, Conn, Cmd)
        End If

        FSaveTransferDetail(mSearchCode, Conn, Cmd)
        FSaveInsuranceDetail(mSearchCode, Conn, Cmd)
        FSaveLRDetail(mSearchCode, Conn, Cmd)

        If AgL.Dman_Execute("Select Count(*) From StockHeadDetailBom With (NoLock) Where DocId = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() > 0 Then
            mQry = "Delete From StockHeadDetailBomSku Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Delete From StockHeadDetailBom Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From StockHeadDetail  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1SKU, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If Dgl1.Item(Col1Barcode, I).Value <> "" Then
                        Dgl1.Item(Col1Barcode, I).Tag = FInsertBarCode(Conn, Cmd, mSearchCode, mSr,
                            Dgl1.Item(Col1Barcode, I).Value, Dgl1.Item(Col1Item, I).Tag, "", DglMain.Item(Col1Value, rowV_Date).Value, "", "", "", "", False)
                    End If
                    InsertStockHeadDetail(mSearchCode, mSr, I, Conn, Cmd)
                    InsertLRBaleDetail(mSearchCode, mSr, I, Conn, Cmd)

                    If Dgl1.Item(Col1DocQty, I).Tag IsNot Nothing Then
                        CType(Dgl1.Item(Col1DocQty, I).Tag, FrmStockEntryDimension).FSave(mSearchCode, mSr, I, Conn, Cmd)
                    Else
                        mDimensionSrl += 1
                        InsertStock(mSearchCode, mSr, mDimensionSrl, I, Conn, Cmd)
                        InsertStockProcess(mSearchCode, mSr, mDimensionSrl, I, Conn, Cmd)
                    End If

                    If CType(AgL.VNull(FGetSettings(SettingFields.PostConsumptionYn, SettingType.General)), Boolean) = True Then
                        If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                            FPostConsumption_Garment(mSearchCode, mSr, I, Conn, Cmd)
                        Else
                            FPostConsumption(mSearchCode, mSr, I, Conn, Cmd)
                        End If
                    End If
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Item(Col1Barcode, I).Tag <> "" And Dgl1.Item(Col1Barcode, I).Tag IsNot Nothing Then
                            FUpdateBarCode(Conn, Cmd, mSearchCode, mSr, Dgl1.Item(Col1Barcode, I).Tag, Dgl1.Item(Col1Barcode, I).Value, Dgl1.Item(Col1Item, I).Tag,
                                       "", DglMain.Item(Col1Value, rowV_Date).Value, "", "", "", False)
                        End If
                        UpdateStockHeadDetail(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
                        UpdateLRBaleDetail(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), Val(Dgl1.Item(Col1BaleNo, I).Tag), I, Conn, Cmd)

                        If Dgl1.Item(Col1DocQty, I).Tag IsNot Nothing Then
                            CType(Dgl1.Item(Col1DocQty, I).Tag, FrmStockEntryDimension).FSave(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
                        Else
                            UpdateStock(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), Val(Dgl1.Item(Col1StockSr, I).Value), I, Conn, Cmd)
                            UpdateStockProcess(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), Val(Dgl1.Item(Col1StockSr, I).Value), I, Conn, Cmd)
                        End If

                        If CType(AgL.VNull(FGetSettings(SettingFields.PostConsumptionYn, SettingType.General)), Boolean) = True Then
                            If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                                FPostConsumption_Garment(mSearchCode, Dgl1.Item(ColSNo, I).Tag, I, Conn, Cmd)
                            Else
                                FPostConsumption(mSearchCode, Dgl1.Item(ColSNo, I).Tag, I, Conn, Cmd)
                            End If
                        End If
                    Else
                        DeleteLineData(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
                    End If
                End If
            End If
        Next

        FPostInLedger(mSearchCode, Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & Dgl4.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl4)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub
    Private Sub FSaveTransferDetail(DocID As String, ByVal Conn As Object, ByVal Cmd As Object)
        If LblV_Type.Tag = Ncat.StockTransfer Then
            If AgL.Dman_Execute("Select Count(*) From StockHeadTransfer With (NoLock) Where DocId = '" & DocID & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
                mQry = "INSERT INTO StockHeadTransfer(DocId, FromGodown, ToGodown)
                    Values(" & mSearchCode & ", 
                    " & AgL.Chk_Text(Dgl2(Col1Value, rowFromGodown).Tag) & ", 
                    " & AgL.Chk_Text(Dgl2(Col1Value, rowToGodown).Tag) & ")"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Else
                mQry = "UPDATE StockHeadTransfer Set 
                        FromGodown = " & AgL.Chk_Text(Dgl2(Col1Value, rowFromGodown).Tag) & ",
                        ToGodown = " & AgL.Chk_Text(Dgl2(Col1Value, rowToGodown).Tag) & "
                        Where DocID = '" & DocID & "' "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub
    Private Sub FSaveInsuranceDetail(DocID As String, ByVal Conn As Object, ByVal Cmd As Object)
        If Val(Dgl2.Item(Col1Value, rowInsuredValue).Value) > 0 Then
            If AgL.Dman_Execute("Select Count(*) From Stock With (NoLock) Where DocId = '" & DocID & "' And Item = '" & ItemCode.GoodsInsurance & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
                mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                    SubCode, SalesTaxGroupParty, Godown, Barcode, Item, 
                    SalesTaxGroupItem,  LotNo, EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                    Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                    Select '" & mSearchCode & "' AS DocID, " & Val(bInsuranceNoBarcodeSr) & " AS TSr, " & Val(bInsuranceNoBarcodeSr) & " AS Sr, " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                    " & AgL.Chk_Text(LblPrefix.Text) & ", 
                    " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & " AS SubCode, NULL AS SalesTaxGroupParty, 
                    " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowGodown).Tag) & " As Godown,
                    B.Code AS Barcode, B.Item, 
                    NULL AS SalesTaxGroupItem,  NULL AS LotNo, NULL AS EType_IR, 
                    " & Dgl2.Item(Col1Value, rowInsuredValue).Value & " AS Qty_Iss, 0 AS Qty_Rec, 
                    I.Unit, NULL AS UnitMultiplier, 0 AS DealQty_Iss , 0 AS DealQty_Rec, NULL AS DealUnit, 
                    0 AS Rate, 0 AS Amount, 0 AS Landed_Value, 
                    NULL AS ReferenceDocID, NULL AS ReferenceTSr, 
                    NULL AS ReferenceDocIDSr
                    From Barcode B 
                    LEFT JOIN Item I ON B.Item = I.Code
                    Where B.Code = '" & Dgl2.Item(Col1Value, rowInsurancePolicyNo).Tag & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Else
                mQry = " Select * From Barcode With (NoLock) Where Code = '" & Dgl2.Item(Col1Value, rowInsurancePolicyNo).Tag & "'"
                Dim DtInsurancePolicyBarcode As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

                If DtInsurancePolicyBarcode.Rows.Count > 0 Then
                    mQry = "Update Stock Set
                        V_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                        V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                        V_Date = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", 
                        V_No = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                        RecId = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  
                        Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                        Site_Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                        Subcode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", 
                        SalesTaxGroupParty = Null,
                        Barcode = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowInsurancePolicyNo).Tag) & ", 
                        Item = " & AgL.Chk_Text(AgL.XNull(DtInsurancePolicyBarcode.Rows(0)("Item"))) & ", 
                        SalesTaxGroupItem = Null, 
                        LotNo = Null,
                        BaleNo = Null,
                        EType_IR = 'I', 
                        Qty_Iss = " & Dgl2.Item(Col1Value, rowInsuredValue).Value & ",
                        Qty_Rec = 0, 
                        UnitMultiplier = 0,
                        DealQty_Iss = Null, 
                        DealQty_Rec =0,  
                        DealUnit = Null, 
                        Rate = 0, 
                        Amount = 0,
                        Landed_Value = 0,
                        ReferenceDocId = Null, 
                        ReferenceTSr = Null, 
                        ReferenceDocIdSr = NUll
                        Where DocId = '" & DocID & "' And Item = '" & ItemCode.GoodsInsurance & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        End If
    End Sub
    Private Sub FSaveLRDetail(DocID As String, ByVal Conn As Object, ByVal Cmd As Object)
        If LblV_Type.Tag = Ncat.LrEntry Then
            Dim bDescription As String = ""
            Dim bSpecification1 As String = ""
            Dim bMfgDate As String = ""
            Dim bSpecification3 As Decimal = 0
            Dim bSpecification4 As String = ""
            Dim bSpecification5 As String = ""

            bDescription = DglMain.Item(Col1Value, rowPartyDocNo).Value + " -" + DglMain.Item(Col1Value, rowTransporter).Value
            bSpecification1 = DglMain.Item(Col1Value, rowPartyDocNo).Value
            bMfgDate = DglMain.Item(Col1Value, rowPartyDocDate).Value
            For I As Integer = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    bSpecification3 += Val(Dgl1.Item(Col1Qty, I).Value)
                    bSpecification4 += Dgl1.Item(Col1Specification, I).Value + ","
                End If
            Next

            If DglMain.Item(Col1Value, rowPartyDocNo).Tag Is Nothing Or DglMain.Item(Col1Value, rowPartyDocNo).Tag = "" Then
                DglMain.Item(Col1Value, rowPartyDocNo).Tag = FInsertBarCode(Conn, Cmd, DocID, bLrNoBarcodeSr, bDescription, ItemCode.Lr, bSpecification1, bMfgDate, bSpecification3,
                               bSpecification4, bSpecification5, "", True)
            Else
                FUpdateBarCode(Conn, Cmd, DocID, bLrNoBarcodeSr, DglMain.Item(Col1Value, rowPartyDocNo).Tag, bDescription, ItemCode.Lr, bSpecification1, bMfgDate, bSpecification3,
                    bSpecification4, bSpecification5, True)
            End If
        End If
    End Sub
    Private Sub InsertLRBaleDetail(DocID As String, Sr As Integer, bRowIndex As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        If LblV_Type.Tag = Ncat.LrEntry Then
            Dim bDescription As String = ""
            Dim bSpecification1 As String = ""
            Dim bMfgDate As String = ""
            Dim bSpecification3 As Decimal = 0
            Dim bSpecification4 As String = ""
            Dim bSpecification5 As String = ""

            bDescription = DglMain.Item(Col1Value, rowPartyDocNo).Value + " -" + DglMain.Item(Col1Value, rowTransporter).Value + "-" + "Bale No : " + Dgl1.Item(Col1BaleNo, bRowIndex).Value
            bSpecification1 = Dgl1.Item(Col1BaleNo, bRowIndex).Value
            bMfgDate = DglMain.Item(Col1Value, rowPartyDocDate).Value
            bSpecification3 = Val(Dgl1.Item(Col1Qty, bRowIndex).Value)
            bSpecification4 = Dgl1.Item(Col1Specification, bRowIndex).Value
            bSpecification5 = DglMain.Item(Col1Value, rowPartyDocNo).Value

            Dgl1.Item(Col1Barcode, bRowIndex).Tag = FInsertBarCode(Conn, Cmd, DocID, Sr, bDescription, ItemCode.LrBale, bSpecification1, bMfgDate, bSpecification3,
                               bSpecification4, bSpecification5, DglMain.Item(Col1Value, rowPartyDocNo).Tag, True)
        End If
    End Sub
    Private Sub UpdateLRBaleDetail(DocID As String, Sr As Integer, bBarCode As Integer, bRowIndex As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        If LblV_Type.Tag = Ncat.LrEntry Then
            Dim bDescription As String = ""
            Dim bSpecification1 As String = ""
            Dim bMfgDate As String = ""
            Dim bSpecification3 As Decimal = 0
            Dim bSpecification4 As String = ""
            Dim bSpecification5 As Decimal = 0

            bDescription = DglMain.Item(Col1Value, rowPartyDocNo).Value + " -" + DglMain.Item(Col1Value, rowTransporter).Value + "-" + "Bale No : " + Dgl1.Item(Col1BaleNo, bRowIndex).Value
            bSpecification1 = Dgl1.Item(Col1BaleNo, bRowIndex).Value
            bMfgDate = DglMain.Item(Col1Value, rowPartyDocDate).Value
            bSpecification3 = Val(Dgl1.Item(Col1Qty, bRowIndex).Value)
            bSpecification4 = Dgl1.Item(Col1Specification, bRowIndex).Value
            bSpecification5 = DglMain.Item(Col1Value, rowPartyDocNo).Value

            FUpdateBarCode(Conn, Cmd, DocID, Sr, bBarCode, bDescription, ItemCode.LrBale, bSpecification1, bMfgDate, bSpecification3,
                    bSpecification4, bSpecification5, True)
        End If
    End Sub
    Private Sub DeleteLRBaleDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Val(Dgl1.Item(ColSNo, LineGridRowIndex).Tag) > 0 Then
            mQry = " Delete From BarCode Where GenDocId = '" & DocID & "' And GenSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Function FInsertBarCode(Conn As Object, Cmd As Object, DocId As String, Sr As Integer, BarCodeDesc As String,
                                    bItemCode As String, bSpecification1 As String, bMfgDate As String,
                                    bSpecification3 As String, bSpecification4 As String, bSpecification5 As String,
                                    Parent As String, bIsPostedInStock As Boolean) As String

        Dim bMaxCode As Integer = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode With (NoLock)", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
        mQry = " INSERT INTO Barcode (Code, Description, Div_Code, Item, 
                    GenDocID, GenSr, Qty, ExpiryDate, 
                    GenSubcode, Specification1, Mfgdate, Specification3, Specification4, Specification5, Parent)
                    Select " & bMaxCode & ", " & AgL.Chk_Text(BarCodeDesc) & ", 
                    " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(bItemCode) & ", 
                    " & AgL.Chk_Text(DocId) & " As GenDocID, " & Sr & " As gensr, 1 As qty, 
                    Null As expirydate, " & AgL.Chk_Text(DglMain.Item(Col1Value, rowTransporter).Tag) & " As gensubcode, 
                    " & AgL.Chk_Text(bSpecification1) & " As Specification1, 
                    " & AgL.Chk_Date(bMfgDate) & " As Mfgdate, 
                    " & AgL.Chk_Text(bSpecification3) & " As Specification3, 
                    " & AgL.Chk_Text(bSpecification4) & " As Specification4, 
                    " & AgL.Chk_Text(bSpecification5) & " As Specification5,
                    " & AgL.Chk_Text(Parent) & " As Parent "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO BarcodeSiteDetail (Code, Div_Code, Site_Code, LastTrnDocID, LastTrnSr, 
                        LastTrnV_Type, LastTrnManualRefNo, LastTrnSubcode, LastTrnProcess, CurrentGodown, Status)
                        Select " & bMaxCode & ", " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ", 
                        " & AgL.Chk_Text(DocId) & " As lasttrndocid, " & Sr & " As lasttrnsr,
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As lasttrnv_type, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As lasttrnmanualrefno, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowTransporter).Tag) & " As LastTrnSubcode, 
                        Null As lasttrnprocess, " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowGodown).Tag) & " As currentgodown, 
                        'Receive' As status "
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

        If bIsPostedInStock = True Then
            mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                        SubCode, SalesTaxGroupParty, Godown, Barcode, Item, 
                        SalesTaxGroupItem,  LotNo, EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                        Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                        Select B.GenDocId AS DocID, B.GenSr AS TSr, B.GenSr AS Sr, " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", 
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                        B.GenSubCode AS SubCode, NULL AS SalesTaxGroupParty, 
                        " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowGodown).Tag) & " As Godown,
                        B.Code AS Barcode, B.Item, 
                        NULL AS SalesTaxGroupItem,  NULL AS LotNo, NULL AS EType_IR, 0 AS Qty_Iss, 1 AS Qty_Rec, 
                        I.Unit, NULL AS UnitMultiplier, 0 AS DealQty_Iss , 0 AS DealQty_Rec, NULL AS DealUnit, 
                        0 AS Rate, 0 AS Amount, 0 AS Landed_Value, NULL AS ReferenceDocID, NULL AS ReferenceTSr, 
                        NULL AS ReferenceDocIDSr
                        From Barcode B  
                        LEFT JOIN Item I ON B.Item = I.Code
                        Where B.GenDocId = '" & DocId & "' And B.GenSr = " & Sr & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
        Return bMaxCode
    End Function
    Private Sub FUpdateBarCode(Conn As Object, Cmd As Object, DocId As String, Sr As Integer, bBarCode As Integer, BarCodeDesc As String,
                               bItemCode As String,
                               bSpecification1 As String, bMfgDate As String,
                               bSpecification3 As String, bSpecification4 As String,
                               bSpecification5 As String, bIsPostedInStock As Boolean)
        mQry = " UPDATE Barcode
                        SET Description = " & AgL.Chk_Text(BarCodeDesc) & ",
	                        Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ",
	                        Item = " & AgL.Chk_Text(bItemCode) & ",
	                        Qty = 1,
	                        ExpiryDate = Null,
	                        GenSubcode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowTransporter).Tag) & ",
	                        Specification1 = " & AgL.Chk_Text(bSpecification1) & ",
	                        MfgDate = " & AgL.Chk_Date(bMfgDate) & ",
	                        Specification3 = " & AgL.Chk_Text(bSpecification3) & ",
	                        Specification4 = " & AgL.Chk_Text(bSpecification4) & ",
	                        Specification5 = " & AgL.Chk_Text(bSpecification5) & "
                            Where GenDocId = '" & DocId & "' And GenSr = " & Sr & " And Code = " & bBarCode & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If bIsPostedInStock = True Then
            mQry = "Update Stock Set
                        V_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                        V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                        V_Date = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", 
                        V_No = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                        RecId = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  
                        Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                        Site_Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                        Subcode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", 
                        Godown = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowGodown).Tag) & ", 
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
                        Where DocId = '" & DocId & "' And Sr = " & Sr & " And Barcode = " & bBarCode & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub DeleteLineData(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Val(Dgl1.Item(ColSNo, LineGridRowIndex).Tag) > 0 Then
            mQry = " Delete From StockHeadDetailBase Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From StockHeadDetailSku Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From StockHeadDetail Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From Stock Where DocId = '" & DocID & "' And TSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From StockProcess Where DocId = '" & DocID & "' And TSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From Barcode Where GenDocId = '" & DocID & "' And GenSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertStockHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into StockHeadDetail(DocId, Sr, Barcode, Item, 
                           Specification, ItemState, BaleNo, LotNo, RawMaterial, RawMaterialConsumptionQty, Godown,
                           DocQty, LossQty, Qty, Unit, Pcs, UnitMultiplier, DealUnit, DealQty,
                           Rate, Amount, ReferenceNo, ReferenceDocID, ReferenceTSr, ReferenceSr, ReferenceDocIdBalanceQty, Remark) "
        mQry += " Values( " & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1RawMaterial, LineGridRowIndex).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1RawMaterialConsumptionQty, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1DocQty, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1LossQty, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Pcs, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Value) & ",  " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Tag) & ",  " &
                        " " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ",  " &
                        " " & Val(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1ReferenceDocIdBalanceQty, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & " " &
                        " ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into StockHeadDetailSku
                (DocId, Sr, ItemCategory, ItemGroup, Item, Dimension1, 
                Dimension2, Dimension3, Dimension4, Size) "
        mQry += " Values(" & AgL.Chk_Text(mSearchCode) & ", " & Sr & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Size, LineGridRowIndex).Tag) & ")"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub UpdateStockHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Dgl1.Rows(LineGridRowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
            mQry = " UPDATE StockHeadDetail " &
                    " Set " &
                    " Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", " &
                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", " &
                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", " &
                    " ItemState = " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", " &
                    " BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ", " &
                    " LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ", " &
                    " RawMaterial = " & AgL.Chk_Text(Dgl1.Item(Col1RawMaterial, LineGridRowIndex).Tag) & ", " &
                    " RawMaterialConsumptionQty = " & Val(Dgl1.Item(Col1RawMaterialConsumptionQty, LineGridRowIndex).Value) & ", " &
                    " Godown = " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & " ," &
                    " DocQty = " & Val(Dgl1.Item(Col1DocQty, LineGridRowIndex).Value) & ", " &
                    " LossQty = " & Val(Dgl1.Item(Col1LossQty, LineGridRowIndex).Value) & ", " &
                    " Qty = " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " &
                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ", " &
                    " Pcs = " & Val(Dgl1.Item(Col1Pcs, LineGridRowIndex).Value) & ", " &
                    " UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ", " &
                    " DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " &
                    " DealQty = " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", " &
                    " Rate = " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " &
                    " Amount = " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ", " &
                    " ReferenceNo = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Value) & ", " &
                    " ReferenceDocId = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Tag) & ", " &
                    " ReferenceTSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ",  " &
                    " ReferenceSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & ", " &
                    " ReferenceDocIdBalanceQty = " & Val(Dgl1.Item(Col1ReferenceDocIdBalanceQty, LineGridRowIndex).Value) & ", " &
                    " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & " " &
                    " Where DocId = '" & mSearchCode & "' " &
                    " And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag & " "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Update StockHeadDetailSku " &
                    " SET ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & ", " &
                    " ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & ", " &
                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
                    " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & ", " &
                    " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & ", " &
                    " Dimension3 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & ", " &
                    " Dimension4 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & ", " &
                    " Size = " & AgL.Chk_Text(Dgl1.Item(Col1Size, LineGridRowIndex).Tag) & " " &
                    " Where DocId = '" & mSearchCode & "' " &
                    " And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag & " "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertStock(DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""

        If LblV_Type.Tag <> Ncat.LrEntry Then
            If CType(AgL.VNull(FGetSettings(SettingFields.PostInStockYn, SettingType.General)), Boolean) = True Then
                Dim bQty_Issue As Double = 0
                Dim bQty_Receive As Double = 0
                Dim bProcess As String = DglMain.Item(Col1Value, rowProcess).Tag

                If LblNCatNature.Tag = NCatNature.Receive Then
                    bQty_Issue = 0
                    bQty_Receive = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                    If DglMain.Item(Col1Value, rowLastProcessOfCombination).Tag <> "" Then bProcess = DglMain.Item(Col1Value, rowLastProcessOfCombination).Tag
                Else
                    bQty_Issue = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                    bQty_Receive = 0
                    bProcess = Dgl1.Item(Col1FromProcess, LineGridRowIndex).Tag
                End If

                mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                SubCode, Process, Godown, SalesTaxGroupParty, Barcode, Item, ItemState,
                SalesTaxGroupItem,  LotNo, EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                Values
                (
                    '" & DocID & "', " & TSr & ", " & Sr & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                    " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", 
                    " & AgL.Chk_Text(bProcess) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & ", 
                    " & AgL.Chk_Text(bSalesTaxGroupParty) & " , 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", 
                    Null, " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                    'I', " & Val(bQty_Issue) & "," & Val(bQty_Receive) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ",
                    " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                    " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 0,  
                    " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", 
                    " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", 
                    " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",0,
                    " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Tag) & ", 
                    " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ", 
                    " & Val(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & "
                ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub
    Private Sub UpdateStock(DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""
        If LblV_Type.Tag <> Ncat.LrEntry Then
            If CType(AgL.VNull(FGetSettings(SettingFields.PostInStockYn, SettingType.General)), Boolean) = True Then
                Dim bQty_Issue As Double = 0
                Dim bQty_Receive As Double = 0
                Dim bProcess As String = DglMain.Item(Col1Value, rowProcess).Tag

                If LblNCatNature.Tag = NCatNature.Receive Then
                    bQty_Issue = 0
                    bQty_Receive = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                    If DglMain.Item(Col1Value, rowLastProcessOfCombination).Tag <> "" Then bProcess = DglMain.Item(Col1Value, rowLastProcessOfCombination).Tag
                Else
                    bQty_Issue = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                    bQty_Receive = 0
                    bProcess = Dgl1.Item(Col1FromProcess, LineGridRowIndex).Tag
                End If

                If Dgl1.Item(Col1StockSr, LineGridRowIndex).Value <> "" Then
                    If Dgl1.Item(Col1StockSr, LineGridRowIndex).Value.ToString.Contains(",") = 0 Then
                        mQry = "Update Stock Set
                        V_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                        V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                        V_Date = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", 
                        V_No = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                        RecId = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  
                        Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                        Site_Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                        Subcode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", 
                        Process = " & AgL.Chk_Text(bProcess) & ", 
                        Godown = " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & ", 
                        SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ",
                        Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                        Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", 
                        ItemState = " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", 
                        SalesTaxGroupItem = Null, 
                        LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                        BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ",
                        EType_IR = 'I', 
                        Qty_Iss = " & Val(bQty_Issue) & ",
                        Qty_Rec = " & Val(bQty_Receive) & ",
                        Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ",
                        UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                        DealQty_Iss = " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 
                        DealQty_Rec =0,  
                        DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", 
                        Rate = " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", 
                        Amount = " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",
                        Landed_Value = 0,
                        ReferenceDocId = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Tag) & ", 
                        ReferenceTSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ", 
                        ReferenceDocIdSr = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & " 
                        Where DocId = '" & DocID & "' and TSr =" & TSr & " And Sr =" & Sr & "
                    "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Else
                    mDimensionSrl += 1
                    mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                    SubCode, Process, SalesTaxGroupParty, Barcode, Item, SalesTaxGroupItem,  LotNo, 
                    EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                    Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                    Values
                    (
                        '" & DocID & "', " & TSr & ", " & mDimensionSrl & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", 
                        " & AgL.Chk_Text(bProcess) & ", 
                        " & AgL.Chk_Text(bSalesTaxGroupParty) & " , " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", 
                        Null, " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                        'I', " & Val(bQty_Issue) & ", " & Val(bQty_Receive) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                        " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",0,
                        " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Value) & ", 
                        " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ", 
                        " & Val(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & "
                    )"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        End If
    End Sub
    Private Sub InsertStockProcess(DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""

        If LblV_Type.Tag <> Ncat.LrEntry Then
            If CType(AgL.VNull(FGetSettings(SettingFields.PostInStockProcessYn, SettingType.General)), Boolean) = True Then
                Dim bQty_Issue As Double = 0
                Dim bQty_Receive As Double = 0

                If LblNCatNature.Tag = NCatNature.Receive Then
                    bQty_Issue = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                    bQty_Receive = 0
                Else
                    bQty_Issue = 0
                    bQty_Receive = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                End If

                If AgL.XNull(Dgl1.Item(Col1StockProcess, LineGridRowIndex).Tag) = "" Then
                    Dgl1.Item(Col1StockProcess, LineGridRowIndex).Tag = DocID
                    Dgl1.Item(Col1StockProcessTSr, LineGridRowIndex).Value = TSr
                    Dgl1.Item(Col1StockProcessSr, LineGridRowIndex).Value = Sr
                End If

                mQry = "Insert Into StockProcess(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                SubCode, SalesTaxGroupParty, Barcode, Item, ItemState,
                SalesTaxGroupItem,  LotNo, EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                Rate, Amount, Landed_Value, Process, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr, 
                StockProcess, StockProcessTSr, StockProcessSr) 
                Values
                (
                    '" & DocID & "', " & TSr & ", " & Sr & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                    " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & " , 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", 
                    Null, " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                    'I', " & Val(bQty_Issue) & "," & Val(bQty_Receive) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                    " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",0,
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & ",
                    " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Tag) & ",
                    " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ",
                    " & Val(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & ",
                    " & AgL.Chk_Text(Dgl1.Item(Col1StockProcess, LineGridRowIndex).Tag) & ",
                    " & Val(Dgl1.Item(Col1StockProcessTSr, LineGridRowIndex).Value) & ",
                    " & Val(Dgl1.Item(Col1StockProcessSr, LineGridRowIndex).Value) & "
                ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub
    Private Sub UpdateStockProcess(DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""
        If LblV_Type.Tag <> Ncat.LrEntry Then
            If CType(AgL.VNull(FGetSettings(SettingFields.PostInStockProcessYn, SettingType.General)), Boolean) = True Then
                Dim bQty_Issue As Double = 0
                Dim bQty_Receive As Double = 0

                If LblNCatNature.Tag = NCatNature.Receive Then
                    bQty_Issue = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                    bQty_Receive = 0
                Else
                    bQty_Issue = 0
                    bQty_Receive = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                End If

                If Dgl1.Item(Col1StockSr, LineGridRowIndex).Value <> "" Then
                    If Dgl1.Item(Col1StockSr, LineGridRowIndex).Value.ToString.Contains(",") = 0 Then
                        mQry = "Update StockProcess Set
                        V_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                        V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                        V_Date = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", 
                        V_No = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                        RecId = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  
                        Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                        Site_Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                        Subcode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", 
                        Process = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & ", 
                        SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ",
                        Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                        Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", 
                        ItemState = " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", 
                        SalesTaxGroupItem = Null, 
                        LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                        BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ",
                        EType_IR = 'I', 
                        Qty_Iss = " & Val(bQty_Issue) & ",
                        Qty_Rec = " & Val(bQty_Receive) & ",
                        Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ",
                        UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                        DealQty_Iss = " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 
                        DealQty_Rec =0,  
                        DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", 
                        Rate = " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", 
                        Amount = " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",
                        Landed_Value = 0,
                        ReferenceDocId = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Tag) & ", 
                        ReferenceTSr = " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Tag) & ", 
                        ReferenceDocIdSr = " & Val(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Tag) & " 
                        Where DocId = '" & DocID & "' and TSr =" & TSr & " And Sr =" & Sr & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Else
                    mDimensionSrl += 1
                    mQry = "Insert Into StockProcess(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                    SubCode, SalesTaxGroupParty, Barcode, Item, SalesTaxGroupItem,  LotNo, 
                    EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                    Rate, Amount, Landed_Value, Process, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                    Values
                    (
                        '" & DocID & "', " & TSr & ", " & mDimensionSrl & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & " , " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", 
                        Null, " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                        'I', " & Val(bQty_Issue) & "," & Val(bQty_Receive) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                        " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",0,
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, LineGridRowIndex).Tag) & ",
                        " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, LineGridRowIndex).Value) & ",
                        " & Val(Dgl1.Item(Col1ReferenceDocIdSr, LineGridRowIndex).Value) & "
                    )"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        End If
    End Sub
    'Private Sub ApplyUISettings(NCAT As String)
    '    Dim mQry As String
    '    Dim DtTemp As DataTable
    '    Dim I As Integer, J As Integer
    '    Dim mDglMainRowCount As Integer
    '    Dim mDgl2RowCount As Integer
    '    Try

    '        mQry = "Select H.*
    '                from EntryHeaderUISetting H                   
    '                Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & Dgl2.Name & "' "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If DtTemp.Rows.Count > 0 Then
    '            For I = 0 To DtTemp.Rows.Count - 1
    '                For J = 0 To Dgl2.Rows.Count - 1
    '                    If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl2.Item(Col1Head, J).Value Then
    '                        Dgl2.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
    '                        If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl2RowCount += 1
    '                        Dgl2.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
    '                        If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
    '                            Dgl2.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
    '                        End If
    '                    End If
    '                Next

    '            Next
    '        End If
    '        If mDgl2RowCount = 0 Then Dgl2.Visible = False Else Dgl2.Visible = True


    '        mQry = "Select H.*
    '                from EntryHeaderUISetting H                   
    '                Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & DglMain.Name & "' "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If DtTemp.Rows.Count > 0 Then
    '            For I = 0 To DtTemp.Rows.Count - 1
    '                For J = 0 To DglMain.Rows.Count - 1
    '                    If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DglMain.Item(Col1Head, J).Value Then
    '                        DglMain.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
    '                        If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglMainRowCount += 1
    '                        DglMain.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
    '                        If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
    '                            DglMain.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
    '                        End If
    '                    End If
    '                Next
    '            Next
    '        End If
    '        If mDglMainRowCount = 0 Then DglMain.Visible = False Else DglMain.Visible = True



    '        For I = 1 To Dgl1.Columns.Count - 1
    '            Dgl1.Columns(I).Visible = False
    '        Next

    '        mQry = "Select H.*
    '                from EntryLineUISetting H                    
    '                Where EntryName='" & Me.Name & "' And NCat = '" & NCAT & "' 
    '                And GridName ='" & Dgl1.Name & "' "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


    '        If DtTemp.Rows.Count > 0 Then
    '            For I = 0 To DtTemp.Rows.Count - 1
    '                For J = 0 To Dgl1.Columns.Count - 1
    '                    If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
    '                        Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
    '                        Dgl1.Columns(J).ReadOnly = Not AgL.VNull(DtTemp.Rows(I)("IsEditable"))
    '                        If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
    '                            Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
    '                        End If
    '                        If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
    '                            Dgl1.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
    '                        End If
    '                    End If
    '                Next
    '            Next
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
    '    End Try
    'End Sub
    Private Sub ApplyUISetting()
        Dim bNCat As String = ""
        If LblV_Type.Tag <> "" Then bNCat = LblV_Type.Tag Else bNCat = EntryNCat
        GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowProcess).Tag, DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting(Dgl2, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowProcess).Tag, DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowProcess).Tag, DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.HorizontalGrid)
        GetUISetting(Dgl4, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowProcess).Tag, DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.HorizontalGrid)
        If Dgl4.Visible = False Then PnlConsumptionTotal.Visible = False
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False
        Dim mQryStockSr As String

        Dim DsMain As DataSet



        LblTotalQty.Text = 0
        LblDealQty.Text = 0
        LblTotalBale.Text = 0
        LblTotalAmount.Text = 0

        LblTotalBomQty.Text = "0"

        mQry = " Select H.*, Prc.Name As ProcessName, Prcd.FirstProcessOfCombination, Prcd.LastProcessOfCombination, 
                Sg.Name || ',' || IfNull(C1.CityName,'') As PartyDesc, 
                ResponsiblePerson.Name || ',' || IfNull(ResponsiblePersonCity.CityName,'') As ResponsiblePersonDesc, 
                Transporter.Name || ',' || IfNull(TransporterCity.CityName,'') As TransporterDesc, Inp.Description As InsurancePolicyNoBarcodeDesc, Vt.Nature As NCatNature, Sg.Nature
                From (Select * From StockHead With (NoLock) Where DocID='" & SearchCode & "') H 
                LEFT JOIN SubGroup Prc On H.Process = Prc.SubCode
                LEFT JOIN ProcessDetail Prcd On Prc.SubCode = Prcd.SubCode
                LEFT JOIN SubGroup Sg With (NoLock) ON H.SubCode = Sg.SubCode 
                LEFT JOIN City C1  With (NoLock) On Sg.CityCode = C1.CityCode 
                LEFT JOIN SubGroup ResponsiblePerson With (NoLock) On H.ResponsiblePerson = ResponsiblePerson.SubCode 
                LEFT JOIN City ResponsiblePersonCity  With (NoLock) On ResponsiblePerson.CityCode = ResponsiblePersonCity.CityCode 
                LEFT JOIN SubGroup Transporter With (NoLock) On H.Transporter = Transporter.SubCode 
                LEFT JOIN City TransporterCity With (NoLock) On Transporter.CityCode = TransporterCity.CityCode  
                LEFT JOIN BarCode Inp On H.InsurancePolicyNo = Inp.Code 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type "
        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))

                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                DglMain.Item(Col1Value, rowProcess).Tag = AgL.XNull(.Rows(0)("Process"))
                DglMain.Item(Col1Value, rowProcess).Value = AgL.XNull(.Rows(0)("ProcessName"))

                DglMain.Item(Col1Value, rowFirstProcessOfCombination).Tag = AgL.XNull(.Rows(0)("FirstProcessOfCombination"))
                DglMain.Item(Col1Value, rowLastProcessOfCombination).Tag = AgL.XNull(.Rows(0)("LastProcessOfCombination"))

                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))
                DglMain.Item(Col1Value, rowParty).Tag = AgL.XNull(.Rows(0)("SubCode"))
                DglMain.Item(Col1Value, rowParty).Value = AgL.XNull(.Rows(0)("PartyDesc"))

                DglMain(Col1Value, rowPartyDocNo).Value = AgL.XNull(.Rows(0)("PartyDocNo"))
                DglMain(Col1Value, rowPartyDocDate).Value = AgL.XNull(.Rows(0)("PartyDocDate"))

                Dgl2(Col1Value, rowInsurancePolicyNo).Tag = AgL.XNull(.Rows(0)("InsurancePolicyNo"))
                Dgl2(Col1Value, rowInsurancePolicyNo).Value = AgL.XNull(.Rows(0)("InsurancePolicyNoBarcodeDesc"))
                Dgl2(Col1Value, rowInsuredValue).Value = AgL.XNull(.Rows(0)("InsuredValue"))


                If AgL.XNull(Dgl2.Item(Col1Value, rowInsurancePolicyNo).Tag) <> "" Then
                    mQry = "SELECT IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) As BalanceInsuranceValue
                        FROM Stock L 
                        WHERE L.BarCode = '" & Dgl2.Item(Col1Value, rowInsurancePolicyNo).Tag & "' 
                        AND L.DocID <> '" & mSearchCode & "'
                        GROUP BY L.Barcode "
                    Dim DtInsurance As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtInsurance.Rows.Count > 0 Then
                        Dgl2.Item(Col1Value, rowInsuranceBalanceValue).Value = AgL.VNull(DtInsurance.Rows(0)("BalanceInsuranceValue"))
                    End If
                End If

                DglMain(Col1Value, rowTransporter).Tag = AgL.XNull(.Rows(0)("Transporter"))
                DglMain(Col1Value, rowTransporter).Value = AgL.XNull(.Rows(0)("TransporterDesc"))

                Dgl2(Col1Value, rowResponsiblePerson).Tag = AgL.XNull(.Rows(0)("ResponsiblePerson"))
                Dgl2(Col1Value, rowResponsiblePerson).Value = AgL.XNull(.Rows(0)("ResponsiblePersonDesc"))

                Dgl2(Col1Value, rowReferenceDocId).Tag = AgL.XNull(AgL.XNull(.Rows(0)("ReferenceDocId")))
                Dgl2(Col1Value, rowRemarks).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks")))
                Dgl2(Col1Value, rowRemarks1).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks1")))
                Dgl2(Col1Value, rowRemarks2).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks2")))

                TxtNature.Text = AgL.XNull(.Rows(0)("Nature"))

                DglMain.Item(Col1BtnDetail, rowParty).Tag = Nothing

                If LblV_Type.Tag = Ncat.LrEntry Then
                    Dim bLRNoBarcode As String = AgL.XNull(AgL.Dman_Execute("Select Code From BarCode 
                                                Where GenDocId = '" & SearchCode & "' 
                                                And GenSr = " & bLrNoBarcodeSr & "", AgL.GCn).ExecuteScalar)
                    DglMain(Col1Value, rowPartyDocNo).Tag = bLRNoBarcode
                End If


                AgCustomGrid1.FMoveRecFooterTable(DsMain.Tables(0))


                If AgL.PubServerName = "" Then
                    mQryStockSr = "Select  group_concat(Sr ,',') from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr"
                Else
                    mQryStockSr = "Select  Cast(Sr as Varchar) + ',' from Stock  With (NoLock) Where DocID = L.DocID And TSr = L.Sr for xml path('')"
                End If
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                'mQry = "Select L.*, Barcode.Description as BarcodeName, 
                '        I.Description As ItemDesc, I.ManualCode, 
                '        U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, 
                '        MU.DecimalPlaces As DealUnitDecimalPlaces, 
                '        IG.Description As ItemGroupName, I.ItemCategory, I.ItemGroup, 
                '        IC.Description As ItemCategoryName, G.Name As GodownDesc, Ist.Description As ItemStateDesc,
                '        (" & mQryStockSr & ") as StockSr
                '        From (Select * From StockHeadDetail  With (NoLock)  Where DocId = '" & SearchCode & "') As L 
                '        LEFT JOIN Item I  With (NoLock) On L.Item = I.Code 
                '        Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code 
                '        Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code 
                '        LEFT JOIN Item Ist On L.ItemState = Ist.Code
                '        Left Join Barcode  With (NoLock) On L.Barcode = Barcode.Code
                '        Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                '        Left Join Unit MU  With (NoLock) On L.DealUnit = MU.Code 
                '        LEFT JOIN SubGroup G On L.Godown = G.SubCode
                '        Order By L.Sr "

                'mQry = "Select L.*, 
                '        Pi.V_Type || '-' || Pi.ManualRefNo As PurchInvoiceNo, Barcode.Description as BarcodeName, 
                '        I.Description As ItemDesc, I.ManualCode, 
                '        U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, U.ShowDimensionDetailInPurchase,
                '        MU.DecimalPlaces As DealUnitDecimalPlaces,
                '        Sku.Code As SkuCode, Sku.Description As SkuDescription, 
                '        It.Code As ItemType, It.Name As ItemTypeDesc,
                '        IG.Description As ItemGroupDesc, I.ItemCategory, I.ItemGroup, 
                '        IC.Description As ItemCategoryDesc, 
                '        SKU.Dimension1, SKU.Dimension2, 
                '        Sku.Dimension3, Sku.Dimension4, Sku.Size, 
                '        D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                '        D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                '        I.ItemCategory as MItemCategory, I.ItemGroup as MItemGroup, I.Specification as MItemSpecification, 
                '        I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MSize, 
                '        Ist.Description As ItemStateDesc, G.Name As GodownDesc, 
                '        (" & mQryStockSr & ") as StockSr 
                '        From (Select * From StockHeadDetail  With (NoLock)  Where DocId = '" & SearchCode & "') As L 
                '        LEFT JOIN Stock St On L.DocId = St.DocId And L.Sr = St.TSr 
                '        LEFT JOIN PurchInvoice Pi  With (NoLock) On St.ReferenceDocId = Pi.DocId 
                '        LEFT JOIN Voucher_Type Vt With (NoLock) On Pi.V_Type = Vt.V_Type
                '        LEFT JOIN Item Sku ON Sku.Code = L.Item
                '        LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
                '        LEFT JOIN ItemType It On Sku.ItemType = It.Code
                '        Left Join Item IC On Sku.ItemCategory = IC.Code
                '        Left Join Item IG On Sku.ItemGroup = IG.Code
                '        LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                '        LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                '        LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                '        LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                '        LEFT JOIN Item Size ON Size.Code = Sku.Size
                '        LEFT JOIN Item Ist On L.ItemState = Ist.Code
                '        LEFT JOIN Barcode  With (NoLock) On L.Barcode = Barcode.Code
                '        LEFT JOIN SubGroup G On L.Godown = G.SubCode
                '        Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                '        Left Join Unit MU  With (NoLock) On L.DealUnit = MU.Code 
                '        Order By L.Sr "

                mQry = " Select Code, GenSr From Barcode Where GenDocId = '" & mSearchCode & "' 
                        And Item = '" & ItemCode.LrBale & "'"
                Dim DtBale As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                Dim strQryPurchase$ = "Select Max(L.DocId) As DocId, L.ReferenceDocId, L.ReferenceTSr, Sum(L.Qty) As Qty " &
                                    "FROM PurchInvoiceDetail L  With (NoLock) " &
                                    "Where L.ReferenceDocId = '" & mSearchCode & "' " &
                                    "GROUP BY L.ReferenceDocId, L.ReferenceTSr "

                Dim strQryStock$ = "Select Max(L.DocId) As DocId, L.ReferenceDocId, L.ReferenceTSr, Sum(L.Qty) As Qty " &
                                    "FROM StockHeadDetail L  With (NoLock) " &
                                    "Where L.ReferenceDocId = '" & mSearchCode & "' " &
                                    "GROUP BY L.ReferenceDocId, L.ReferenceTSr "



                mQry = "Select L.*, 
                        Pi.V_Type || '-' || Pi.ManualRefNo As PurchInvoiceNo, Pi.V_Date As PurchInvoiceDate, Barcode.Description as BarcodeName, 
                        I.Description As ItemDesc, I.ManualCode, 
                        U.ShowDimensionDetailInSales, U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, U.ShowDimensionDetailInPurchase,
                        MU.DecimalPlaces As DealUnitDecimalPlaces,
                        Sku.Code As SkuCode, Sku.Description As SkuDescription, 
                        It.Code As ItemType, It.Name As ItemTypeDesc,
                        IG.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, 
                        Sids.Item As ItemCode, Sids.ItemCategory, Sids.ItemGroup, 
                        Sids.Dimension1, Sids.Dimension2, 
                        Sids.Dimension3, Sids.Dimension4, Sids.Size, 
                        D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                        D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                        I.ItemCategory as MItemCategory, I.ItemGroup as MItemGroup, I.Specification as MItemSpecification, 
                        I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MSize, 
                        Godown.Name as GodownName, ISt.Description as ItemStateName, RawMaterial.Description As RawMaterialDesc, 
                        CASE WHEN Pid.DocID IS NOT NULL OR Shd.DocID IS NOT NULL THEN 1 ELSE 0 END As RowLocked,
                        (" & mQryStockSr & ") as StockSr 
                        From (Select * From StockHeadDetail  With (NoLock)  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN StockHeadDetailSku Sids With (NoLock) On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                        LEFT JOIN PurchInvoice Pi  With (NoLock) On L.ReferenceDocId = Pi.DocId 
                        LEFT JOIN Voucher_Type Vt With (NoLock) On Pi.V_Type = Vt.V_Type
                        LEFT JOIN Item Sku ON Sku.Code = L.Item
                        LEFT JOIN ItemType It On Sku.ItemType = It.Code
                        Left Join Item IC On Sids.ItemCategory = IC.Code
                        Left Join Item IG On Sids.ItemGroup = IG.Code
                        LEFT JOIN Item I ON Sids.Item = I.Code
                        LEFT JOIN Item D1 ON Sids.Dimension1 = D1.Code
                        LEFT JOIN Item D2 ON Sids.Dimension2 = D2.Code
                        LEFT JOIN Item D3 ON Sids.Dimension3 = D3.Code
                        LEFT JOIN Item D4 ON Sids.Dimension4 = D4.Code
                        LEFT JOIN Item Size ON Sids.Size = Size.Code
                        LEFT JOIN Item Ist On L.ItemState = Ist.Code
                        LEFT JOIN Barcode  With (NoLock) On L.Barcode = Barcode.Code
                        LEFT JOIN SubGroup G On L.Godown = G.SubCode
                        Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                        Left Join Unit MU  With (NoLock) On L.DealUnit = MU.Code 
                        Left Join Subgroup Godown On L.Godown = Godown.Subcode
                        LEFT JOIN Item RawMaterial ON L.RawMaterial = RawMaterial.Code
                        LEFT JOIN (" & strQryPurchase & ") Pid ON L.DocID = Pid.ReferenceDocID AND L.Sr = Pid.ReferenceTSr
                        LEFT JOIN (" & strQryStock & ") Shd ON L.DocID = Shd.ReferenceDocID AND L.Sr = Shd.ReferenceTSr
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

                            Dgl1.Item(Col1SKU, I).Tag = AgL.XNull(.Rows(I)("SkuCode"))
                            Dgl1.Item(Col1SKU, I).Value = AgL.XNull(.Rows(I)("SkuDescription"))

                            Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(.Rows(I)("ItemType"))
                            Dgl1.Item(Col1ItemType, I).Value = AgL.XNull(.Rows(I)("ItemTypeDesc"))

                            Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))

                            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))

                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                            Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                            Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))

                            Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                            Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))

                            Dgl1.Item(Col1Size, I).Tag = AgL.XNull(.Rows(I)("Size"))
                            Dgl1.Item(Col1Size, I).Value = AgL.XNull(.Rows(I)("SizeDesc"))

                            Dgl1.Item(Col1RawMaterial, I).Tag = AgL.XNull(.Rows(I)("RawMaterial"))
                            Dgl1.Item(Col1RawMaterial, I).Value = AgL.XNull(.Rows(I)("RawMaterialDesc"))

                            Dgl1.Item(Col1RawMaterialConsumptionQty, I).Value = AgL.XNull(.Rows(I)("RawMaterialConsumptionQty"))


                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))

                            Dgl1.Item(Col1ItemState, I).Tag = AgL.XNull(.Rows(I)("ItemState"))
                            Dgl1.Item(Col1ItemState, I).Value = AgL.XNull(.Rows(I)("ItemStateName"))

                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1DocQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("DocQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1LossQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("LossQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Pcs, I).Value = AgL.VNull(.Rows(I)("Pcs"))

                            Dgl1.Item(Col1Godown, I).Tag = AgL.XNull(.Rows(I)("Godown"))
                            Dgl1.Item(Col1Godown, I).Value = AgL.XNull(.Rows(I)("GodownName"))


                            Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DealUnitDecimalPlaces"))
                            Dgl1.Item(Col1UnitMultiplier, I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                            Dgl1.Item(Col1DealQty, I).Value = Format(AgL.VNull(.Rows(I)("DealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))

                            Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(.Rows(I)("ReferenceDocId"))
                            Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(.Rows(I)("PurchInvoiceNo"))
                            Dgl1.Item(Col1ReferenceDocIdTSr, I).Value = AgL.XNull(.Rows(I)("ReferenceTSr"))
                            Dgl1.Item(Col1ReferenceDocIdSr, I).Value = AgL.XNull(.Rows(I)("ReferenceSr"))
                            Dgl1.Item(Col1ReferenceDocIdDate, I).Value = AgL.XNull(.Rows(I)("PurchInvoiceDate"))
                            Dgl1.Item(Col1ReferenceDocIdBalanceQty, I).Value = AgL.XNull(.Rows(I)("ReferenceDocIdBalanceQty"))

                            'mQry = " Select Code From Barcode Where GenDocId = '" & mSearchCode & "' 
                            '        And GenSr = " & Dgl1.Item(ColSNo, I).Tag & " 
                            '        And Item = '" & ItemCode.LrBale & "'"
                            'Dgl1.Item(Col1BaleNo, I).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())

                            Dim DtRowBale As DataRow() = DtBale.Select("GenSr = " + Dgl1.Item(ColSNo, I).Tag)
                            If DtRowBale.Length > 0 Then
                                Dgl1.Item(Col1BaleNo, I).Tag = DtRowBale(0)("Code")
                            End If


                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))


                            Dgl1.Item(Col1MItemCategory, I).Tag = AgL.XNull(.Rows(I)("MItemCategory"))
                            Dgl1.Item(Col1MItemGroup, I).Tag = AgL.XNull(.Rows(I)("MItemGroup"))
                            Dgl1.Item(Col1MItemSpecification, I).Value = AgL.XNull(.Rows(I)("MItemSpecification"))
                            Dgl1.Item(Col1MDimension1, I).Tag = AgL.XNull(.Rows(I)("MDimension1"))
                            Dgl1.Item(Col1MDimension2, I).Tag = AgL.XNull(.Rows(I)("MDimension2"))
                            Dgl1.Item(Col1MDimension3, I).Tag = AgL.XNull(.Rows(I)("MDimension3"))
                            Dgl1.Item(Col1MDimension4, I).Tag = AgL.XNull(.Rows(I)("MDimension4"))
                            Dgl1.Item(Col1MSize, I).Tag = AgL.XNull(.Rows(I)("MSize"))



                            Dgl1.Item(Col1Unit, I).Tag = AgL.VNull(.Rows(I)("ShowDimensionDetailInSales"))
                            If AgL.VNull(Dgl1.Item(Col1Unit, I).Tag) Then
                                Dgl1.Item(Col1DocQty, I).Style.ForeColor = Color.Blue
                                ShowStockEntryDimensionDetail(I, False)
                            End If

                            Dgl1.Item(Col1IsRecordLocked, I).Value = AgL.VNull(.Rows(I)("RowLocked"))
                            If Dgl1.Item(Col1IsRecordLocked, I).Value <> 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True


                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                            LblTotalBale.Text += 1
                        Next I
                    End If
                End With

                mQry = "SELECT Max(I.Description) As ItemDesc, 
                        Max(U.DecimalPlaces), Max(U.DecimalPlaces) As QtyDecimalPlaces, 
                        Max(Sku.Code) As SkuCode, Max(Sku.Description) As SkuDescription, 
                        Max(It.Code) As ItemType, Max(It.Name) As ItemTypeDesc,
                        Max(IG.Description) As ItemGroupDesc, Max(IC.Description) As ItemCategoryDesc, 
                        Pids.Item As ItemCode, Pids.ItemCategory, Pids.ItemGroup, 
                        Pids.Dimension1, Pids.Dimension2, 
                        Pids.Dimension3, Pids.Dimension4, Pids.Size, 
                        Max(D1.Description) as Dimension1Desc, Max(D2.Description) as Dimension2Desc,
                        Max(D3.Description) as Dimension3Desc, Max(D4.Description) as Dimension4Desc, Max(Size.Description) as SizeDesc,
                        Sum(L.Qty) AS Qty, Max(L.Unit) AS Unit
                        FROM (Select * From StockHeadDetailBom Where DocId = '" & mSearchCode & "') As L 
                        LEFT JOIN StockHeadDetailBomSku Pids ON L.DocID = Pids.DocID AND L.TSr = Pids.TSr AND L.Sr = Pids.Sr
                        LEFT JOIN Item Sku ON Sku.Code = L.Item
                        LEFT JOIN ItemType It On Sku.ItemType = It.Code
                        Left Join Item IC On Pids.ItemCategory = IC.Code
                        Left Join Item IG On Pids.ItemGroup = IG.Code
                        LEFT JOIN Item I ON Pids.Item = I.Code
                        LEFT JOIN Item D1 ON Pids.Dimension1 = D1.Code
                        LEFT JOIN Item D2 ON Pids.Dimension2 = D2.Code
                        LEFT JOIN Item D3 ON Pids.Dimension3 = D3.Code
                        LEFT JOIN Item D4 ON Pids.Dimension4 = D4.Code
                        LEFT JOIN Item Size ON Pids.Size = Size.Code
                        Left Join Unit U With (NoLock) On L.Unit = U.Code 
                        GROUP BY Pids.ItemCategory, Pids.ItemGroup, Pids.Item, Pids.Dimension1, Pids.Dimension2, Pids.Dimension3, Pids.Dimension4, Pids.Size   "
                DsMain = AgL.FillData(mQry, AgL.GCn)
                With DsMain.Tables(0)
                    Dgl4.RowCount = 1
                    Dgl4.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsMain.Tables(0).Rows.Count - 1
                            Dgl4.Rows.Add()
                            Dgl4.Item(ColSNo, I).Value = Dgl4.Rows.Count

                            Dgl4.Item(Col4Sku, I).Tag = AgL.XNull(.Rows(I)("SkuCode"))
                            Dgl4.Item(Col4Sku, I).Value = AgL.XNull(.Rows(I)("SkuDescription"))

                            Dgl4.Item(Col4ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                            Dgl4.Item(Col4ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))

                            Dgl4.Item(Col4Item, I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                            Dgl4.Item(Col4Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl4.Item(Col4Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl4.Item(Col4Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl4.Item(Col4Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl4.Item(Col4Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                            Dgl4.Item(Col4Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                            Dgl4.Item(Col4Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))

                            Dgl4.Item(Col4Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                            Dgl4.Item(Col4Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))

                            Dgl4.Item(Col4Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl4.Item(Col4Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))

                            Dgl4.Item(Col4StockProcessQty, I).Value = FGetStockProcess(Dgl4.Item(Col4Sku, I).Tag, DglMain.Item(Col1Value, rowParty).Tag)

                            LblTotalBomQty.Text = Val(LblTotalBomQty.Text) + Val(Dgl4.Item(Col4Qty, I).Value)
                        Next I
                    End If
                End With


                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False


                If AgL.Dman_Execute("Select Count(Distinct Godown) From StockHeadDetail Where DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
                    Dgl2.Item(Col1Value, rowGodown).Tag = Dgl1.Item(Col1Godown, 0).Tag
                    Dgl2.Item(Col1Value, rowGodown).Value = Dgl1.Item(Col1Godown, 0).Value
                End If

                If AgL.Dman_Execute("Select Count(Distinct ReferenceDocId) From StockHeadDetail Where DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
                    Dgl2.Item(Col1Value, rowReferenceDocId).Tag = Dgl1.Item(Col1ReferenceDocId, 0).Tag
                    Dgl2.Item(Col1Value, rowReferenceDocId).Value = Dgl1.Item(Col1ReferenceDocId, 0).Value
                End If
                '-------------------------------------------------------------
            End If
        End With
        'ApplyUISettings(LblV_Type.Tag)
        SetAttachmentCaption()
        Dgl1.ReadOnly = True
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub
    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        'Dim FrmObj As New FrmStockHeadEntryPartyDetail


        Try
            Select Case sender.NAME
                'Case TxtV_Type.Name
                '    If DglMain.Item(Col1Value, rowV_Type).Tag = "" Then Exit Sub





                '    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GcnRead)
                '    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                '    IniGrid()
                '    ApplyUISettings(LblV_Type.Tag)
                '    DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

                'Case TxtParty.Name
                '    Validating_SaleToParty(DglMain.Item(Col1Value, rowParty).Tag)

                'Case TxtReferenceNo.Name
                '    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead",
                '                    DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                '                    DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                '                    DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmStockHeadEntry_BaseEvent_DglMainEditingControlValidating(sender As Object, e As CancelEventArgs) Handles Me.BaseEvent_DglMainEditingControlValidating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = DglMain.CurrentCell.RowIndex
        mColumn = DglMain.CurrentCell.ColumnIndex

        Select Case mRow
            Case rowV_Type
                If DglMain.Item(Col1Value, rowV_Type).Tag = "" Then Exit Sub

                FGetProcessFromVoucher_Type()

                TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GcnRead)
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                IniGrid()
                'ApplyUISettings(LblV_Type.Tag)
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

            Case rowSettingGroup
                If AgL.StrCmp(Topctrl1.Mode, "Add") Then
                    IniGrid()
                    If DglMain(Col1Value, rowParty).Visible = True Then
                        DglMain.CurrentCell = DglMain(Col1Value, rowParty)
                        DglMain.Focus()
                    ElseIf Dgl2(Col1Value, rowGodown).Visible = True Then
                        Dgl2.CurrentCell = Dgl2(Col1Value, rowGodown)
                        Dgl2.Focus()
                    End If
                Else
                    e.Cancel = True
                End If

            Case rowProcess
                If AgL.StrCmp(Topctrl1.Mode, "Add") Then
                    IniGrid()
                    If DglMain(Col1Value, rowParty).Visible = True Then
                        DglMain.CurrentCell = DglMain(Col1Value, rowParty)
                        DglMain.Focus()

                        DglMain.Item(Col1Value, rowParty).Tag = ""
                        DglMain.Item(Col1Value, rowParty).Value = ""

                        DglMain.Item(Col1Head, rowParty).Tag = Nothing
                    ElseIf Dgl2(Col1Value, rowGodown).Visible = True Then
                        Dgl2.CurrentCell = Dgl2(Col1Value, rowGodown)
                        Dgl2.Focus()
                    End If

                    If DglMain.Item(Col1Value, rowProcess).Tag <> "" Then
                        mQry = " Select FirstProcessOfCombination, LastProcessOfCombination 
                            From ProcessDetail 
                            Where SubCode = '" & DglMain.Item(Col1Value, rowProcess).Tag & "' "
                        Dim DtProcessDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtProcessDetail.Rows.Count > 0 Then
                            DglMain.Item(Col1Value, rowFirstProcessOfCombination).Tag = AgL.XNull(DtProcessDetail.Rows(0)("FirstProcessOfCombination"))
                            DglMain.Item(Col1Value, rowLastProcessOfCombination).Tag = AgL.XNull(DtProcessDetail.Rows(0)("LastProcessOfCombination"))
                        End If
                    End If
                Else
                    e.Cancel = True
                End If


            Case rowParty
                Validating_SaleToParty(DglMain.Item(Col1Value, rowParty).Tag)

            Case rowTransporter
                If LblV_Type.Tag = Ncat.LrEntry Then
                    Dgl2.Item(Col1Value, rowGodown).Tag = DglMain.Item(Col1Value, rowTransporter).Tag
                    Dgl2.Item(Col1Value, rowGodown).Value = DglMain.Item(Col1Value, rowTransporter).Value

                    For I As Integer = 0 To Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1Godown, I).Tag = Dgl2.Item(Col1Value, rowGodown).Tag
                        Dgl1.Item(Col1Godown, I).Value = Dgl2.Item(Col1Value, rowGodown).Value
                    Next
                End If
        End Select
    End Sub
    Private Sub FrmStockHeadEntry_BaseEvent_DglMainEditingControlKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainEditingControlKeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex
            Select Case mRow
                Case rowParty
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpSubgroup()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowTransporter
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Transporter & "' Order By Name"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowProcess
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpProcess()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select

            'If e.KeyCode = Keys.Enter Then
            '    Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
            '    If DglMain.CurrentCell.RowIndex = LastCell.RowIndex And DglMain.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
            '        If Dgl2.Visible Then
            '            Dgl2.CurrentCell = Dgl2.Item(Col1Value, Dgl2.FirstDisplayedCell.RowIndex)
            '            Dgl2.Focus()
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FCreateHelpProcess() As DataSet
        Dim strCond As String = ""

        Dim FilterInclude_Process As String = FGetSettings(SettingFields.FilterInclude_Process, SettingType.General)
        If FilterInclude_Process <> "" Then
            strCond += " And (CharIndex('+' || Sg.Code,'" & FilterInclude_Process & "') > 0 Or
                                CharIndex('+' || Sg.Parent,'" & FilterInclude_Process & "') > 0) "
        End If

        mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' 
                And IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpProcess = AgL.FillData(mQry, AgL.GCn)
    End Function
    Public Sub Validating_SaleToParty(Subcode As String, Optional ShowDialogForCashParty As Boolean = True)
        Dim DtTemp As DataTable
        If DglMain.Item(Col1Value, rowV_Date).Value <> "" And DglMain.Item(Col1Value, rowParty).Value <> "" Then
            If ClsMain.IsPartyBlocked(DglMain.Item(Col1Value, rowParty).Tag, LblV_Type.Tag) Then
                MsgBox("Party is blocked for " & DglMain.Item(Col1Value, rowV_Type).Value & ". Record will not be saved")
            End If
            TxtNature.Text = AgL.XNull(AgL.Dman_Execute(" Select Nature 
                                From SubGroup 
                                Where SubCode = '" & DglMain.Item(Col1Value, rowParty).Tag & "'", AgL.GCn).ExecuteScalar())

            DglMain.Item(Col1BtnDetail, rowParty).Tag = Nothing
            ShowStockHeadParty("", DglMain.Item(Col1Value, rowParty).Tag, TxtNature.Text, True)

            If DglMain.Item(Col1LastValue, rowParty).Tag <> DglMain.Item(Col1Value, rowParty).Tag Or Topctrl1.Mode = "Add" Then
                mQry = "Select H.Address
                        From SubGroup H  With (NoLock)
                        Where H.Subcode = '" & Subcode & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Dgl2(Col1Value, rowFromGodown).Value = AgL.XNull(DtTemp.Rows(0)("Address"))
                End If

                Dgl2(Col1Value, rowReferenceDocId).Tag = ""
                Dgl2(Col1Value, rowReferenceDocId).Value = ""
                Dgl2(Col1Head, rowReferenceDocId).Tag = Nothing
            End If
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd

        FGetProcessFromVoucher_Type()

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        IniGrid()
        'ApplyUISettings(LblV_Type.Tag)
        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

        Dgl1.ReadOnly = False

        If LblV_Type.Tag = Ncat.LrEntry Then
            Dgl2.Item(Col1Value, rowGodown).Tag = "TRANSPORT"
            Dgl2.Item(Col1Value, rowGodown).Value = "TRANSPORT"
        End If

        If DglMain.Visible = True Then
            If DglMain.FirstDisplayedCell IsNot Nothing Then
                If DglMain(Col1Value, rowProcess).Visible = True Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowProcess)
                ElseIf DglMain(Col1Value, rowSettingGroup).Visible = True Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowSettingGroup)
                ElseIf DglMain(Col1Value, rowParty).Visible = True Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowParty)
                Else
                    DglMain.CurrentCell = DglMain(Col1Value, DglMain.FirstDisplayedCell.RowIndex)
                End If
                DglMain.Focus()
            End If
        End If

        mQry = "Select SubCode As Code, Name 
                From SubGroup 
                Where SubGroupType = '" & SubgroupType.Godown & "'"
        Dim DtGodown As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtGodown.Rows.Count = 1 Then
            Dgl2.Item(Col1Value, rowGodown).Tag = AgL.XNull(DtGodown.Rows(0)("Code"))
            Dgl2.Item(Col1Value, rowGodown).Value = AgL.XNull(DtGodown.Rows(0)("Name"))
        End If

        FInitInsurancePolicyNo()

        SetAttachmentCaption()
    End Sub
    Private Sub FInitInsurancePolicyNo()
        mQry = "SELECT Bc.Code, Max(Bc.Description) As PolicyNo, 
                IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) As BalancePolicyAmount
                FROM Stock L 
                LEFT JOIN Barcode Bc On L.BarCode = Bc.Code
                WHERE L.Item = '" & ItemCode.GoodsInsurance & "' 
                GROUP BY Bc.Code
                HAVING IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) > 0 "
        Dim DtInsurancePolicy As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtInsurancePolicy.Rows.Count = 1 Then
            Dgl2.Item(Col1Value, rowInsurancePolicyNo).Tag = AgL.XNull(DtInsurancePolicy.Rows(0)("Code"))
            Dgl2.Item(Col1Value, rowInsurancePolicyNo).Value = AgL.XNull(DtInsurancePolicy.Rows(0)("PolicyNo"))
            Dgl2.Item(Col1Value, rowInsuranceBalanceValue).Value = AgL.VNull(DtInsurancePolicy.Rows(0)("BalancePolicyAmount"))
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean
        'If AgL.RequiredField(TxtParty, LblBuyer.Text) Then passed = False : Exit Sub

        If ClsMain.IsPartyBlocked(DglMain.Item(Col1Value, rowParty).Tag, LblV_Type.Tag) Then
            MsgBox("Party is blocked for " & DglMain.Item(Col1Value, rowV_Type).Value & ". Record will not be saved")
            passed = False
            Exit Sub
        End If


        'If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If Dgl2.Visible = True Then
            For I = 0 To Dgl2.Rows.Count - 1
                If Dgl2.Rows(I).Visible = True Then
                    If Dgl2.Item(Col1Mandatory, I).Value <> "" Then
                        If (Dgl2.Item(Col1Value, I).Value = "" Or Dgl2.Item(Col1Value, I).Value Is Nothing) Then
                            MsgBox(Dgl2.Item(Col1Head, I).Value & " is blank...!", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col1Value, I) : Dgl2.Focus()
                            passed = False : Exit Sub
                        End If
                    End If




                    'If Dgl2.Item(Col1Mandatory, I).Value <> "" And (Dgl2.Item(Col1Value, I).Value = "" Or
                    '     Dgl2.Item(Col1Value, I).Value Is Nothing) Then
                    '    MsgBox(Dgl2.Item(Col1Head, I).Value & " is blank...!", MsgBoxStyle.Information)
                    '    Dgl2.CurrentCell = Dgl2.Item(Col1Value, I) : Dgl2.Focus()
                    '    passed = False : Exit Sub
                    'End If
                End If
            Next
        End If

        If DglMain.Visible = True Then
            For I = 0 To DglMain.Rows.Count - 1
                If DglMain.Rows(I).Visible = True Then
                    If DglMain.Item(Col1Mandatory, I).Value <> "" Then
                        If (DglMain.Item(Col1Value, I).Value = "" Or DglMain.Item(Col1Value, I).Value Is Nothing) Then
                            MsgBox(DglMain.Item(Col1Head, I).Value & " is blank...!", MsgBoxStyle.Information)
                            DglMain.CurrentCell = DglMain.Item(Col1Value, I) : DglMain.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End If




        If Val(Dgl2.Item(Col1Value, rowInsuredValue).Value) > Val(Dgl2.Item(Col1Value, rowInsuranceBalanceValue).Value) Then
            MsgBox("Insured Balance value can not be greater then incurance balance value...!", MsgBoxStyle.Information)
            Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowInsuredValue) : Dgl2.Focus()
            passed = False : Exit Sub
        End If

        Dim bTableName As String = "[" + Guid.NewGuid().ToString() + "]"

        If AgL.IsTableExist(bTableName.Replace("[", "").Replace("]", ""), AgL.GCn) Then
            mQry = "Drop Table " + bTableName
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        mQry = " CREATE TABLE " & bTableName & "(ReferenceDocId nvarchar(21), ReferenceDocIdTSr Int, ReferenceDocIdSr Int, Qty Float, RowIndex Int) "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1SKU, I).Value <> "" And Dgl1.Rows(I).Visible Then
                    If LblV_Type.Tag = Ncat.PurchaseGoodsReceipt Or LblV_Type.Tag = Ncat.JobReceive Then
                        If AgL.XNull(Dgl1.Item(Col1ReferenceDocId, I).Tag) <> "" Then
                            mQry = " INSERT INTO " & bTableName & "(ReferenceDocId, ReferenceDocIdTSr, ReferenceDocIdSr, Qty, RowIndex)"
                            mQry += " Select " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, I).Tag) & " As ReferenceDocId, 
                            " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, I).Value) & " As ReferenceDocIdTSr, 
                            " & Val(Dgl1.Item(Col1ReferenceDocIdSr, I).Value) & " As ReferenceDocIdSr,
                            " & Val(Dgl1.Item(Col1Qty, I).Value) & " As Qty,
                            " & Val(Dgl1.Item(ColSNo, I).Value) & " As RowIndex "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                        End If
                    End If

                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1ReferenceDocId, I).Tag) = "" Then
                    Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(Dgl2.Item(Col1Value, rowReferenceDocId).Tag)
                End If

                If CType(AgL.VNull(ClsMain.FGetSettings(SettingFields.SkuManagementApplicableYN, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, Dgl1.Item(Col1ItemType, I).Tag, "", ItemV_Type.SKU, "", "")), Boolean) = True Then
                    If AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Item, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Size, I).Value) <> "" _
                    Then
                        Dgl1.Item(Col1SKU, I).Tag = ClsMain.FGetSKUCode(Dgl1.Item(ColSNo, I).Value, Dgl1.Item(Col1ItemType, I).Tag, Dgl1.Item(Col1ItemCategory, I).Tag, Dgl1.Item(Col1ItemCategory, I).Value _
                                                       , Dgl1.Item(Col1ItemGroup, I).Tag, Dgl1.Item(Col1ItemGroup, I).Value _
                                                       , Dgl1.Item(Col1Item, I).Tag, Dgl1.Item(Col1Item, I).Value _
                                                       , Dgl1.Item(Col1Dimension1, I).Tag, Dgl1.Item(Col1Dimension1, I).Value _
                                                       , Dgl1.Item(Col1Dimension2, I).Tag, Dgl1.Item(Col1Dimension2, I).Value _
                                                       , Dgl1.Item(Col1Dimension3, I).Tag, Dgl1.Item(Col1Dimension3, I).Value _
                                                       , Dgl1.Item(Col1Dimension4, I).Tag, Dgl1.Item(Col1Dimension4, I).Value _
                                                       , Dgl1.Item(Col1Size, I).Tag, Dgl1.Item(Col1Size, I).Value _
                                                       , Dgl1.Item(Col1MItemCategory, I).Tag _
                                                       , Dgl1.Item(Col1MItemGroup, I).Tag _
                                                       , Dgl1.Item(Col1MItemSpecification, I).Tag _
                                                       , Dgl1.Item(Col1MDimension1, I).Tag _
                                                       , Dgl1.Item(Col1MDimension2, I).Tag _
                                                       , Dgl1.Item(Col1MDimension3, I).Tag _
                                                       , Dgl1.Item(Col1MDimension4, I).Tag _
                                                       , Dgl1.Item(Col1MSize, I).Tag
                                                       )
                        If Dgl1.Item(Col1SKU, I).Tag = "" Then
                            passed = False
                            Exit Sub
                        End If


                        If Dgl1.Item(Col1DocQty, I).Tag IsNot Nothing Then
                            If CType(Dgl1.Item(Col1DocQty, I).Tag, FrmStockEntryDimension).FData_Validation() = False Then
                                passed = False
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    Dgl1.Item(Col1SKU, I).Tag = Dgl1.Item(Col1Item, I).Tag
                End If
            Next
        End With

        mQry = " Select * From " & bTableName & ""
        Dim DtTempTable As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTempTable.Rows.Count > 0 Then
            Dim bPendingOrderQry As String = " SELECT VOrder.PurchOrder, VOrder.PurchOrderSr, IsNull(VOrder.OrderQty,0) - IsNull(VReceive.ReceiveQty,0) AS BalanceQty
                FROM (
                    SELECT L.PurchOrder, L.PurchOrderSr, Sum(L.Qty) AS OrderQty
                    FROM PurchOrder H 
                    LEFT JOIN PurchOrderDetail L ON H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    Where H.Vendor = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                    And H.Process = '" & DglMain.Item(Col1Value, rowProcess).Tag & "'
	                GROUP BY L.PurchOrder, L.PurchOrderSr
                ) AS VOrder
                LEFT JOIN (
                    SELECT L.ReferenceDocId As PurchOrder, L.ReferenceTSr As PurchOrderSr, Sum(L.Qty_Rec) AS ReceiveQty
                    FROM Stock L 
                    Where L.DocId <> '" & mSearchCode & "'
                    GROUP BY L.ReferenceDocId, L.ReferenceTSr	
                ) AS VReceive ON VOrder.PurchOrder = VReceive.PurchOrder AND VOrder.PurchOrderSr = VReceive.PurchOrderSr 
                WHERE 1=1 
                And IsNull(VOrder.OrderQty,0) - IsNull(VReceive.ReceiveQty,0) > 0 "

            mQry = " Select Temp.RowIndex, Temp.Qty, VPendingOrder.BalanceQty
                From (
                    SELECT IfNull(L.ReferenceDocId,'') As ReferenceDocId, 
                    IfNull(L.ReferenceDocIdTSr,0) As ReferenceDocIdTSr, 
                    Max(L.RowIndex) As RowIndex,
                    IsNull(Sum(L.Qty),0) AS Qty
                    FROM " & bTableName & " L 
                    GROUP BY L.ReferenceDocId, L.ReferenceDocIdTSr) As Temp
                LEFT JOIN (" & bPendingOrderQry & ") As VPendingOrder On Temp.ReferenceDocId = VPendingOrder.PurchOrder
                        And Temp.ReferenceDocIdTSr = VPendingOrder.PurchOrderSr 
                Where IfNull(Temp.Qty,0) > IfNull(VPendingOrder.BalanceQty,0)"
            Dim DtOrerBalance As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtOrerBalance.Rows.Count > 0 Then
                If AgL.VNull(DtOrerBalance.Rows(0)("Qty")) - AgL.VNull(DtOrerBalance.Rows(0)("BalanceQty")) > 5 Then
                    MsgBox("Qty entered is greater then balance qty at row number " & DtOrerBalance.Rows(0)("RowIndex") & "...!", MsgBoxStyle.Information)
                    passed = False
                    Exit Sub
                End If
            End If
        End If

        If AgL.IsTableExist(bTableName.Replace("[", "").Replace("]", ""), AgL.GCn) Then
            mQry = "Drop Table " + bTableName
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If Dgl1.Columns(Col1Godown).Visible = False Then
            If Dgl2.Rows(rowGodown).Visible = True Then
                If AgL.XNull(Dgl2(Col1Value, rowGodown).Value) <> "" Then
                    For I = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(Dgl1(Col1SKU, I).Value) <> "" Then
                            Dgl1(Col1Godown, I).Value = Dgl2(Col1Value, rowGodown).Value
                            Dgl1(Col1Godown, I).Tag = Dgl2(Col1Value, rowGodown).Tag
                        End If
                    Next
                End If
            End If
        End If

        If Dgl1.Columns(Col1ReferenceDocId).Visible = False Then
            If Dgl2.Rows(rowReferenceDocId).Visible = True Then
                If AgL.XNull(Dgl2(Col1Value, rowReferenceDocId).Value) <> "" Then
                    For I = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(Dgl1(Col1SKU, I).Value) <> "" Then
                            Dgl1(Col1ReferenceDocId, I).Value = Dgl2(Col1Value, rowReferenceDocId).Value
                            Dgl1(Col1ReferenceDocId, I).Tag = Dgl2(Col1Value, rowReferenceDocId).Tag
                        End If
                    Next
                End If
            End If
        End If



        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead",
                                    DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                    DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                    DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)

        If Not CheckDuplicateRef Then
            DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef
    End Sub
    'Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If e.KeyCode = Keys.Enter Then Exit Sub
    '        Select Case sender.name
    '            Case TxtParty.Name
    '                If e.KeyCode <> Keys.Enter Then
    '                    If sender.AgHelpDataset Is Nothing Then
    '                        FCreateHelpSubgroup()
    '                    End If
    '                End If
    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        Dgl4.RowCount = 1 : Dgl4.Rows.Clear()
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

            If AgL.VNull(Dgl1.Item(Col1IsRecordLocked, Dgl1.CurrentCell.RowIndex).Value) <> 0 Then
                Dgl1.CurrentCell.ReadOnly = True
                Exit Sub
            End If

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty
                    If LblV_Type.Tag = Ncat.LrEntry Then
                        CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = 3
                    End If
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                Case Col1Pcs
                    If LblV_Type.Tag = Ncat.LrEntry Then
                        CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    End If
                Case Col1DocQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                    If AgL.XNull(Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Tag) <> "" Then
                        Dgl1.CurrentCell.ReadOnly = Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                Case Col1Item
                    If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                        If AgL.StrCmp(DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsGarmentProduction.SettingGroup_RawAndOtherMaterial) Then
                            If AgL.StrCmp(Dgl1.Item(Col1ItemType, Dgl1.CurrentCell.RowIndex).Tag, ItemTypeCode.RawProduct) Then
                                Dgl1.CurrentCell.ReadOnly = True
                            ElseIf AgL.StrCmp(Dgl1.Item(Col1ItemType, Dgl1.CurrentCell.RowIndex).Tag, ItemTypeCode.OtherRawProduct) Then
                                Dgl1.CurrentCell.ReadOnly = False
                            Else
                                Dgl1.CurrentCell.ReadOnly = True
                            End If
                        End If
                    End If
                Case Col1Dimension1, Col1Dimension2, Col1Dimension3, Col1Dimension4
                    If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                        If AgL.StrCmp(DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsGarmentProduction.SettingGroup_RawAndOtherMaterial) Then
                            If AgL.StrCmp(Dgl1.Item(Col1ItemType, Dgl1.CurrentCell.RowIndex).Tag, ItemTypeCode.RawProduct) Then
                                Dgl1.CurrentCell.ReadOnly = False
                            ElseIf AgL.StrCmp(Dgl1.Item(Col1ItemType, Dgl1.CurrentCell.RowIndex).Tag, ItemTypeCode.OtherProduct) Then
                                Dgl1.CurrentCell.ReadOnly = True
                            Else
                                Dgl1.CurrentCell.ReadOnly = True
                            End If
                        End If
                    End If
            End Select

            If Dgl2.Rows.Count > 0 Then
                If Dgl2.Item(Col1Value, rowGodown).Value <> "" Then
                    Dgl1.Item(Col1Godown, Dgl1.CurrentCell.RowIndex).Tag = Dgl2.Item(Col1Value, rowGodown).Tag
                    Dgl1.Item(Col1Godown, Dgl1.CurrentCell.RowIndex).Value = Dgl2.Item(Col1Value, rowGodown).Value
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)

        'Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmStockHeadEntry_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer
        If Dgl1.AgHelpDataSet(Col1ItemCategory) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCategory).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCategory) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemGroup) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemGroup).Dispose() : Dgl1.AgHelpDataSet(Col1ItemGroup) = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing

        If Dgl1.AgHelpDataSet(Col1Dimension1) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension1).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension1) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension2) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension2).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension2) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension3) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension3).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension3) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension4) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension4).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension4) = Nothing
        If Dgl1.AgHelpDataSet(Col1Size) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Size).Dispose() : Dgl1.AgHelpDataSet(Col1Size) = Nothing

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, i).Tag = Nothing
        Next

        For i = 0 To Dgl2.Rows.Count - 1
            Dgl2(Col1Head, i).Tag = Nothing
        Next
    End Sub
    Private Sub FrmSaleQuotation_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        Dim i As Integer

        GBoxImportFromExcel.Enabled = False

        If Dgl1.Columns(Col1DealQty).Visible = False Then
            LblDealQty.Visible = False
            LblDealQtyText.Visible = False
        End If

        If Dgl1.Columns(Col1BaleNo).Visible = False Then
            LblTotalBale.Visible = False
            LblTotalBaleText.Visible = False
        End If


        For i = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(i).DefaultCellStyle.BackColor = Dgl1.AgReadOnlyColumnColor Then
                Dgl1.Columns(i).ReadOnly = True
            End If
        Next
    End Sub
    Private Sub FrmStockHeadEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    End Sub
    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        Dim mRow = e.RowIndex
        Try
            If mPrevRowIndex <> e.RowIndex Then
                'FChangeOptions(mRow)
                If AgL.VNull(Dgl1.Item(Col1StockQty, mRow).Value) = 0 Then
                    Dgl1.Item(Col1StockQty, mRow).Value = FGetStock(mRow)
                End If
                If AgL.VNull(Dgl1.Item(Col1StockProcessQty, mRow).Value) = 0 Then
                    Dgl1.Item(Col1StockProcessQty, mRow).Value = FGetStockProcess(Dgl1.Item(Col1SKU, mRow).Tag, DglMain.Item(Col1Value, rowParty).Tag)
                End If
                LblStock.Text = Dgl1.Item(Col1StockQty, mRow).Value
                LblStockProcess.Text = Dgl1.Item(Col1StockProcessQty, mRow).Value
            End If
            mPrevRowIndex = mRow
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        Dgl.Visible = False
    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            If Dgl1.Columns(Col1Item).Visible = True Then
                Dgl1.CurrentCell = Dgl1.Item(Col1Item, Dgl1.Rows.Count - 1)
            Else
                Dgl1.CurrentCell = Dgl1.Item(Dgl1.FirstDisplayedCell.ColumnIndex, Dgl1.Rows.Count - 1)
            End If
            Dgl1.Focus()
        End If
    End Sub
    Private Function FCreateHelpSubgroup() As DataSet
        Dim strCond As String = ""

        Dim bFilterInclude_AcGroup As String = FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General)
        If bFilterInclude_AcGroup <> "" Then
            If bFilterInclude_AcGroup.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.AcGroup,'" & bFilterInclude_AcGroup & "') > 0 "
            ElseIf bFilterInclude_AcGroup.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.AcGroup,'" & bFilterInclude_AcGroup & "') <= 0 "
            End If
        End If

        Dim bFilterInclude_SubGroupType As String = FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General)
        If bFilterInclude_SubGroupType <> "" Then
            If bFilterInclude_SubGroupType.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.SubGroupType,'" & bFilterInclude_SubGroupType & "') > 0 "
            ElseIf bFilterInclude_SubGroupType.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.SubGroupType,'" & bFilterInclude_SubGroupType & "') <= 0 "
            End If
        End If

        Dim bFilterInclude_Nature As String = FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General)
        If bFilterInclude_Nature <> "" Then
            If bFilterInclude_Nature.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.Nature,'" & bFilterInclude_Nature & "') > 0 "
            ElseIf bFilterInclude_Nature.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.Nature,'" & bFilterInclude_Nature & "') <= 0 "
            End If
        End If

        Dim bFilterInclude_Process As String = FGetSettings(SettingFields.FilterInclude_Process, SettingType.General)
        If AgL.XNull(DglMain.Item(Col1Value, rowProcess).Tag) <> "" Then
            bFilterInclude_Process = "+" + AgL.XNull(DglMain.Item(Col1Value, rowProcess).Tag)
        End If
        If bFilterInclude_Process <> "" Then
            If bFilterInclude_Process.ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IfNull(Sp.Process,'" & Process.Purchase & "'),'" & bFilterInclude_Process & "') > 0
                                   Or CharIndex('+' || IfNull(P.Parent,'" & Process.Purchase & "'),'" & bFilterInclude_Process & "') > 0) "
            ElseIf bFilterInclude_Process.ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IfNull(Sp.Process,'" & Process.Purchase & "'),'" & bFilterInclude_Process & "') <= 0 
                                   CharIndex('-' || IfNull(P.Parent,'" & Process.Purchase & "'),'" & bFilterInclude_Process & "') <= 0)  "
            End If
        End If

        mQry = "SELECT Distinct Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.Address, Ag.GroupName, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " &
                " FROM SubGroup Sg  With (NoLock)  " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Left Join SubgroupProcess SP On Sg.Subcode = SP.Subcode " &
                " Left Join SubGroup P On Sp.Process = P.Subcode " &
                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'TxtParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Sub FCreateHelpItem(RowIndex As Integer)
        Dim strCond As String = ""

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 "
            End If
        End If

        Dim bFilterInclude_ItemV_Type As String = FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General)
        If bFilterInclude_ItemV_Type <> "" Then
            If bFilterInclude_ItemV_Type.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.V_Type,'" & bFilterInclude_ItemV_Type & "') > 0 "
            ElseIf bFilterInclude_ItemV_Type.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.V_Type,'" & bFilterInclude_ItemV_Type & "') <= 0 "
            End If
        Else
            strCond += " And I.V_Type = 'ITEM' "
        End If

        Dim bFilterInclude_ItemGroup As String = FGetSettings(SettingFields.FilterInclude_ItemGroup, SettingType.General)
        If bFilterInclude_ItemGroup <> "" Then
            If bFilterInclude_ItemGroup.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.ItemGroup,'" & bFilterInclude_ItemGroup & "') > 0 "
            ElseIf bFilterInclude_ItemGroup.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.ItemGroup,'" & bFilterInclude_ItemGroup & "') <= 0 "
            End If
        End If

        Dim bFilterInclude_ItemDivision As String = FGetSettings(SettingFields.FilterInclude_ItemDivision, SettingType.General)
        If bFilterInclude_ItemDivision <> "" Then
            If bFilterInclude_ItemDivision.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.Div_Code,'" & bFilterInclude_ItemDivision & "') > 0 "
            ElseIf bFilterInclude_ItemDivision.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.Div_Code,'" & bFilterInclude_ItemDivision & "') <= 0 "
            End If
        End If

        Dim bFilterInclude_Item As String = FGetSettings(SettingFields.FilterInclude_Item, SettingType.General)
        If bFilterInclude_Item <> "" Then
            If bFilterInclude_Item.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.Code,'" & bFilterInclude_Item & "') > 0 "
            ElseIf bFilterInclude_Item.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.Code,'" & bFilterInclude_Item & "') <= 0 "
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1) "
        End If


        mQry = "SELECT I.Code, I.Description, I.ManualCode as ItemCode, I.Rate " &
                  " FROM Item I  With (NoLock) " &
                  " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FrmStockHeadEntry_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FGetPrint(SearchCode, PrintFor.DocumentPrint)
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
                    From StockHead H  With (NoLock)
                    Where H.DocID = '" & mSearchCode & "'"
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            mText = Replace(mText, "<CreditDays>", AgL.XNull(dtTemp.Rows(0)("CreditDays")))
            ClsMain.FReplacePubVariables(mText, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag)

        End If

        FReplaceInvoiceVariables = mText
    End Function
    Private Sub FrmStockHeadEntry_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        mQry = " Select B.*
                From Barcode B With (NoLock)
                LEFT JOIN BarcodeSiteDetail Bs With (NoLock) On B.Code = Bs.Code
                Where B.GenDocId = '" & mSearchCode & "'
                And B.GenDocId <> Bs.LastTrnDocID "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            If LblV_Type.Tag = Ncat.LrEntry Then
                MsgBox("Bale No " + AgL.XNull(DtTemp.Rows(0)("Specification1")) + " processed to another Process.", MsgBoxStyle.Information)
                Passed = False
                Exit Sub
            End If
        End If

        ShowStockHeadParty("", DglMain.Item(Col1Value, rowParty).Tag, TxtNature.Text, False)
        Dgl1.ReadOnly = False
    End Sub
    Private Sub Dgl1_DoubleClick(sender As Object, e As EventArgs) Handles Dgl1.DoubleClick
        If Topctrl1.Mode = "Browse" Then
            Dgl1.CurrentRow.Selected = True
        End If
    End Sub
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



        mPrintTitle = DglMain.Item(Col1Value, rowV_Type).Value

        Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        Dim mDocReportFileName As String = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)
        Dim mVoucherCategory As String = AgL.Dman_Execute("Select IfNull(Max(Category),'') From Voucher_Type where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "'", AgL.GcnRead).ExecuteScalar()




        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            If AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
                mPrintTitle = DglMain.Item(Col1Value, rowV_Type).Value & " (Debit Note)"
            End If
        ElseIf LblV_Type.Tag = Ncat.PurchaseInvoice Then
            mDocNoCaption = "Invoice No."
            mDocDateCaption = "Invoice Date"
        End If

        PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "

            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, H.DocID, SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as DocNo,
                H.PartyName as PartyName, IfNull(H.PartyAddress,'') as PartyAddress, IfNull(H.PartyPinCode,'') as PartyPinCode, IfNull(C.CityName,'') as PartyCityName, IfNull(State.ManualCode,'') as PartyStateCode, IfNull(State.Description,'') as PartyStateName, 
                IfNull(H.PartyMobile,'') as PartyMobile, IfNull(Sg.ContactPerson,'') as ContactPerson, IfNull(H.PartySalesTaxNo,'') as PartySalesTaxNo, IfNull(H.PartyAadharNo,'') as PartyAadharNo, IfNull(H.PartyPanNo,'') as PartyPanNo,
                IfNull(Transporter.Name,IfNull(MTransporter.Name,'')) as TransporterName, IfNull(TD.LrNo,'') LrNo, TD.LrDate, IfNull(TD.PrivateMark,'') PrivateMark, TD.Weight, TD.Freight, IfNull(TD.PaymentType,'') as FreightType, IfNull(TD.RoadPermitNo,'') RoadPermitNo, TD.RoadPermitDate, IfNull(L.ReferenceNo,'') as ReferenceNo,
                I.Description as ItemName, " & IIf(mPrintFor = ClsMain.PrintFor.QA, "IG.Description", "IfNull(IG.PrintingDescription,IG.Description)") & " as ItemGroupName, IC.Description as ItemCatName, 
                I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, IfNull(I.HSN,IC.HSN) as HSN,
                IfNull(D1.Specification,'') as D1Spec, IfNull(D2.Specification,'') as D2Spec, IfNull(D3.Specification,'') as D3Spec, IfNull(D4.Specification,'') as D4Spec, IfNull(Size.Specification,'') as SizeSpec,
                '" & AgL.PubCaptionDimension1 & "' as D1Caption, '" & AgL.PubCaptionDimension2 & "' as D2Caption, '" & AgL.PubCaptionDimension3 & "' as D3Caption, '" & AgL.PubCaptionDimension4 & "' as D4Caption, 
                L.SalesTaxGroupItem, STGI.GrossTaxRate, L.Pcs, Abs(L.Qty) as Qty, L.Rate, L.Unit, U.DecimalPlaces as UnitDecimalPlaces,  
                Abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) Tax1_Per, Abs(L.Tax1) as Tax1, Abs(L.Tax2_Per) as Tax2_Per, Abs(L.Tax2) as Tax2, Abs(L.Tax3_Per) as Tax3_Per, Abs(L.Tax3) as Tax3, Abs(L.Tax4_Per) as Tax4_Per, Abs(L.Tax4) as Tax4, Abs(L.Tax5_Per) as Tax5_Per, Abs(L.Tax5) as Tax5, Abs(L.Net_Amount) as Net_Amount,
                IfNull(H.Remarks,'') as HRemarks, IfNull(L.Remark,'') as LRemarks,
                abs(H.Gross_Amount) as H_Gross_Amount, H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount,abs(H.Taxable_Amount) as H_Taxable_Amount,abs(H.Tax1_Per) as H_Tax1_Per, abs(H.Tax1) as H_Tax1, 
                abs(H.Tax2_Per) as H_Tax2_Per, abs(H.Tax2) as H_Tax2, abs(H.Tax3_Per) as H_Tax3_Per, abs(H.Tax3) as H_Tax3, abs(H.Tax4_Per) as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                abs(H.Tax5_Per) as H_Tax5_Per, abs(H.Tax5) as H_Tax5, abs(H.Deduction_Per) as H_Deduction_Per, abs(H.Deduction) as H_Deduction, abs(H.Other_Charge_Per) as H_Other_Charge_Per, abs(H.Other_Charge) as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, 
                '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                L.DimensionDetail as DimDetail, '' as HsnDescription, '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
                from StockHead H   With (NoLock)              
                Left Join StockHeadDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join StockHeadDetailSku LS  With (NoLock) On LS.DocID = L.DocID And LS.Sr = L.Sr
                Left Join Item I  With (NoLock) On LS.Item = I.Code
                Left Join Item D1  With (NoLock) On LS.Dimension1 = D1.Code
                Left Join Item D2  With (NoLock) On LS.Dimension2 = D2.Code
                Left Join Item D3  With (NoLock) On LS.Dimension3 = D3.Code
                Left Join Item D4  With (NoLock) On LS.Dimension4 = D4.Code   
                Left Join Item Size  With (NoLock) On LS.Size = Size.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join Item IG  With (NoLock) On LS.ItemGroup = IG.Code
                Left Join Item IC  With (NoLock) On LS.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.PartyCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join StockHeadTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.Subcode = Sg.Subcode                                
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                Left Join SubgroupSiteDivisionDetail SSD On H.Subcode = SSD.Subcode And H.Div_Code = SSD.Div_Code And H.Site_Code = SSD.Site_Code
                Left Join ViewHelpSubgroup MTransporter  With (NoLock) On SSD.Transporter= MTransporter.Code
                Where H.DocID = '" & mSearchCode & "'
                "


        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "

        mQry = AgL.GetBackendBasedQuery(mQry)
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
            objRepPrint = New AgLibrary.RepView()
        End If


        Dim sQry As String = ""
        Dim sQryRepName As String = ""



        If mDocReportFileName = "" Then
            If mVoucherCategory = VoucherCategory.Production Then
                ClsMain.FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, "JobReceive_Print_Apparel.rpt", mPrintTitle, , sQry, sQryRepName, DglMain.Item(Col1Value, rowParty).Tag, DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
            Else
                ClsMain.FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, "StockHead_Print.rpt", mPrintTitle, , sQry, sQryRepName, DglMain.Item(Col1Value, rowParty).Tag, DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
            End If
        Else
            ClsMain.FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, mDocReportFileName, mPrintTitle, , sQry, sQryRepName, DglMain.Item(Col1Value, rowParty).Tag, DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
        End If

    End Sub

    Private Sub FSendSms()
        Dim FrmObj As FrmSendSms
        FrmObj = New FrmSendSms(AgL)
        FrmObj.TxtToMobile.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Mobile
                    From StockHead H  With (NoLock)
                    LEFT JOIN SubGroup Sg  With (NoLock) On H.Party = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()
    End Sub
    Private Sub MnuPrintQACopy_Click(sender As Object, e As EventArgs)
        FGetPrint(mSearchCode, PrintFor.QA)
    End Sub
    Private Sub Dgl2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl2.CurrentCell.ReadOnly = True
            End If

            If Me.Visible And Dgl2.ReadOnly = False And Dgl2.CurrentCell.RowIndex > 0 Then
                If Dgl2.CurrentCell.ColumnIndex = Dgl2.Columns(Col1Head).Index Or
                    Dgl2.CurrentCell.ColumnIndex = Dgl2.Columns(Col1Mandatory).Index Then
                    SendKeys.Send("{Tab}")
                End If
            End If

            If Dgl2.CurrentCell.ColumnIndex <> Dgl2.Columns(Col1Value).Index Then Exit Sub


            Dgl2.AgHelpDataSet(Dgl2.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0



            Select Case Dgl2.CurrentCell.RowIndex
                Case rowInsuranceBalanceValue
                    Dgl2.CurrentCell.ReadOnly = True
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
                Case rowFromGodown, rowToGodown, rowGodown
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Godown & "' Order By Name"
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowReferenceDocId
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT H.DocId As Code, Vt.V_Type || '-' || H.ManualRefNo As DocNo, H.V_Date As DocDate
                                    From PurchInvoice H With (NoLock) 
                                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                                    Where Vt.NCat = '" & Ncat.JobOrder & "' 
                                    And H.Vendor = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                                    Order By H.V_Date "
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowResponsiblePerson
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Employee & "' Order By Name"
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If



                Case rowInsurancePolicyNo
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Bc.Code, Max(Bc.Description) As PolicyNo
                                    FROM Stock L 
                                    LEFT JOIN Barcode Bc On L.BarCode = Bc.Code
                                    WHERE L.Item = '" & ItemCode.GoodsInsurance & "' 
                                    AND L.DocID <> '" & mSearchCode & "'
                                    GROUP BY Bc.Code
                                    HAVING IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) > 0 "
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
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

        Select Case mRow
            Case rowGodown
                For I As Integer = 0 To Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Godown, I).Tag = Dgl2.Item(Col1Value, rowGodown).Tag
                    Dgl1.Item(Col1Godown, I).Value = Dgl2.Item(Col1Value, rowGodown).Value
                Next

            Case rowInsurancePolicyNo
                mQry = "SELECT IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) As BalanceInsuranceValue
                        FROM Stock L 
                        WHERE L.BarCode = '" & Dgl2.Item(Col1Value, rowInsurancePolicyNo).Tag & "' 
                        AND L.DocID <> '" & mSearchCode & "'
                        GROUP BY L.Barcode
                        HAVING IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) > 0 "
                Dim DtInsurance As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtInsurance.Rows.Count > 0 Then
                    Dgl2.Item(Col1Value, rowInsuranceBalanceValue).Value = AgL.VNull(DtInsurance.Rows(0)("BalanceInsuranceValue"))
                End If
        End Select
    End Sub
    Private Sub ShowAttachments()
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.LblDocNo.Text = "Document No. : " + DglMain.Item(Col1Value, rowReferenceNo).Value
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
            If FileCount > 0 Then Dgl2.Item(Col1Value, rowBtnAttachments).Value = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else Dgl2.Item(Col1Value, rowBtnAttachments).Value = ""
        Else
            Dgl2.Item(Col1Value, rowBtnAttachments).Value = ""
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub




        LblTotalQty.Text = 0
        LblDealQty.Text = 0
        LblTotalBale.Text = 0
        LblTotalAmount.Text = 0

        LblTotalBomQty.Text = 0

        Dgl2.Item(Col1Value, rowInsuredValue).Value = 0


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1SKU, I).Value <> "" And Dgl1.Rows(I).Visible Then
                Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value) - Val(Dgl1.Item(Col1LossQty, I).Value)


                If Val(Dgl1.Item(Col1UnitMultiplier, I).Value) <> 0 Then
                    Dgl1.Item(Col1DealQty, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1UnitMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value) + 2, "0"))
                End If

                'If Val(Dgl1.Item(Col1Amount, I).Value) <> 0 And Dgl1.Columns(Col1Amount).ReadOnly = False Then
                'Else
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'End If



                If LblV_Type.Tag = Ncat.LrEntry Then
                    Dgl2.Item(Col1Value, rowInsuredValue).Value += Val(Dgl1.Item(Col1Pcs, I).Value)
                End If

                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblDealQty.Text = Val(LblDealQty.Text) + Val(Dgl1.Item(Col1DealQty, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                LblTotalBale.Text += 1
            End If
        Next

        For I = 0 To Dgl4.Rows.Count - 1
            LblTotalBomQty.Text = Val(LblTotalBomQty.Text) + Val(Dgl4.Item(Col4Qty, I).Value)
        Next


        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblDealQty.Text = Val(LblDealQty.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)

        LblTotalBomQty.Text = Val(LblTotalBomQty.Text)
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode = Keys.Insert Then
                        Call FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    ElseIf e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem(Dgl1.CurrentCell.RowIndex)
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
                            FCreateHelpItemGroup(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1ItemState
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemState) Is Nothing Then
                            mQry = " SELECT Code, Description FROM Item Where V_Type = '" & ItemV_Type.ItemState & "' "
                            Dgl1.AgHelpDataSet(Col1ItemState) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            FCreateHelpDimension1(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            FCreateHelpDimension2(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Dimension3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension3) Is Nothing Then
                            FCreateHelpDimension3(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Dimension4
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension4) Is Nothing Then
                            FCreateHelpDimension4(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Size
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Size) Is Nothing Then
                            FCreateHelpSize(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

        frmObj.ShowDialog()
        bItemCode = frmObj.mSearchCode
        frmObj = Nothing

        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1Qty, RowIndex)

        FCreateHelpItem(RowIndex)
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemCode(bItemCode, ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1Item, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub
    Private Sub Validating_ItemCode(ItemCode As String, ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtItem As DataTable = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim DtBarcodeSiteDetail As DataTable = Nothing
        Dim StrReturnTicked As String = ""
        Dim dtInvoices As DataTable = Nothing
        Try
            mQry = "Select I.Code, I.Description, I.ManualCode, I.Unit, IfNull(I.SalesTaxPostingGroup, IC.SalesTaxGroup) as SalesTaxPostingGroup, 
                    I.ItemType, It.Name As ItemTypeName, I.ItemCategory, I.ItemGroup, IC.Description as ItemCategoryName, IG.Description as ItemGroupName,
                    U.ShowDimensionDetailInSales, U.DecimalPlaces as QtyDecimalPlaces, IG.Default_DiscountPerSale ,
                    IG.Default_AdditionalDiscountPerSale, IG.Default_AdditionPerSale, I.PurchaseRate,
                    IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase
                                From Item I  With (NoLock)
                                Left Join ItemType It On I.ItemType = It.Code
                                Left Join Unit U  With (NoLock) On I.Unit = U.Code 
                                Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                                Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                Dgl1.Item(Col1ItemType, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemType"))
                Dgl1.Item(Col1ItemType, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemTypeName"))
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Code"))
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtItem.Rows(0)("Description"))
                Call FCheckDuplicate(mRow)
                Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))
                Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Code"))
                Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtItem.Rows(0)("ManualCode"))
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtItem.Rows(0)("QtyDecimalPlaces"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub
    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Dim Str1 As String = ""
        Dim Str2 As String = ""
        Try
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
                                    If AgL.XNull(FGetSettings(SettingFields.ActionOnDuplicateItem, SettingType.General)).ToString.ToUpper = "DO NOTHING" Then
                                    ElseIf AgL.XNull(FGetSettings(SettingFields.ActionOnDuplicateItem, SettingType.General)).ToString.ToUpper = "GO TO FIRST ITEM" Then
                                        Dim mFirstRowIndex As Integer
                                        mFirstRowIndex = Val(Dgl1.Item(ColSNo, I).Value) - 1
                                        Dgl1.CurrentCell = Dgl1.Item(Col1Qty, mFirstRowIndex)
                                        Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FCreateHelpItemCategory()
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 "
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

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.Code,'" & bFilterInclude_ItemType & "') > 0 "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.Code,'" & bFilterInclude_ItemType & "') <= 0 "
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null ) "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
            strCond += " And (IG.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(IG.ShowItemGroupInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
            strCond += " And (IG.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(IG.ShowItemGroupInOtherSites,0) =1) "
        End If


        mQry = "Select IG.Code, IG.Description 
                From Item I  With (NoLock)
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & "
                Group By I.ItemGroup,IG.Code, IG.Description "
        Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpDimension1(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null Or I.BaseItem Is Not Null) "
        End If

        If Dgl1.Item(Col1Item, RowIndex).Value <> "" Then
            strCond += " And (I.BaseItem = '" & Dgl1.Item(Col1Item, RowIndex).Tag & "' Or I.BaseItem Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension1 & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dgl1.Item(Col1Dimension3, RowIndex).Tag) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.Dimension1 & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dgl1.Item(Col1Dimension3, RowIndex).Tag & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpDimension2(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null Or I.BaseItem Is Not Null) "
        End If

        If Dgl1.Item(Col1Item, RowIndex).Value <> "" Then
            strCond += " And (I.BaseItem = '" & Dgl1.Item(Col1Item, RowIndex).Tag & "' Or I.BaseItem Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension2 & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dgl1.Item(Col1Dimension3, RowIndex).Tag) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.Dimension2 & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dgl1.Item(Col1Dimension3, RowIndex).Tag & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpDimension3(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null Or I.BaseItem Is Not Null) "
        End If

        If Dgl1.Item(Col1Item, RowIndex).Value <> "" Then
            strCond += " And (I.BaseItem = '" & Dgl1.Item(Col1Item, RowIndex).Tag & "' Or I.BaseItem Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension3 & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dgl1.Item(Col1Dimension3, RowIndex).Tag) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.Dimension3 & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dgl1.Item(Col1Dimension3, RowIndex).Tag & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Dimension3) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpDimension4(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null Or I.BaseItem Is Not Null) "
        End If

        If Dgl1.Item(Col1Item, RowIndex).Value <> "" Then
            strCond += " And (I.BaseItem = '" & Dgl1.Item(Col1Item, RowIndex).Tag & "' Or I.BaseItem Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Dimension4 & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dgl1.Item(Col1Dimension3, RowIndex).Tag) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.Dimension4 & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dgl1.Item(Col1Dimension3, RowIndex).Tag & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Dimension4) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpSize(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || I.ItemType,'" & bFilterInclude_ItemType & "') > 0 Or I.ItemType Is Null) "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || I.ItemType,'" & bFilterInclude_ItemType & "') <= 0 Or I.ItemType Is Null) "
            End If
        End If


        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.SIZE & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(Dgl1.Item(Col1Dimension3, RowIndex).Tag) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.SIZE & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & Dgl1.Item(Col1Dimension3, RowIndex).Tag & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    If bFilterItems <> "" Then
                        strCond += " And I.Code In (" & bFilterItems & ") "
                    Else
                        strCond += " And I.Code In ('') "
                    End If
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Size) = AgL.FillData(mQry, AgL.GCn)
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
                    If Dgl1.Item(Col1Unit, mRowIndex).Tag Then ShowStockEntryDimensionDetail(Dgl1.CurrentCell.RowIndex, False)
                Case Col1ItemGroup
                    Validating_ItemGroup(mColumnIndex, mRowIndex)
                Case Col1Dimension1
                    Validating_Dimension1(mColumnIndex, mRowIndex)
                    If Dgl1.Item(Col1Unit, mRowIndex).Tag Then ShowStockEntryDimensionDetail(Dgl1.CurrentCell.RowIndex, False)
                Case Col1Dimension2
                    Validating_Dimension2(mColumnIndex, mRowIndex)
                    If Dgl1.Item(Col1Unit, mRowIndex).Tag Then ShowStockEntryDimensionDetail(Dgl1.CurrentCell.RowIndex, False)
                Case Col1Dimension3
                    Validating_Dimension3(mColumnIndex, mRowIndex)
                    If Dgl1.Item(Col1Unit, mRowIndex).Tag Then ShowStockEntryDimensionDetail(Dgl1.CurrentCell.RowIndex, False)
                Case Col1Dimension4
                    Validating_Dimension4(mColumnIndex, mRowIndex)
                    If Dgl1.Item(Col1Unit, mRowIndex).Tag Then ShowStockEntryDimensionDetail(Dgl1.CurrentCell.RowIndex, False)
            End Select


            FGeterateSkuName(mRowIndex)
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item, Col1ItemCode, Col1ItemCategory, Col1ItemGroup, Col1Item, Col1Dimension1, Col1Dimension2, Col1Dimension3, Col1Dimension3, Col1Dimension4, Col1Size
                    If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                        If AgL.XNull(Dgl1.Item(Col1SKU, mRowIndex).Tag) <> "" Then
                            Dgl1.Item(Col1Rate, mRowIndex).Value = FGetLastPurchaseRate(mRowIndex)
                        End If
                    End If

                    If FDivisionNameForCustomization(6) = "SADHVI" Then
                        If AgL.XNull(Dgl1.Item(Col1Item, mRowIndex).Tag) <> "" Then
                            'Dgl1.Item(Col1Rate, mRowIndex).Value = FGetLastSaleRate(mRowIndex)
                            Dgl1.Item(Col1Rate, mRowIndex).Value = AgL.VNull(AgL.Dman_Execute("Select Rate 
                                    From Item Where Code = '" & AgL.XNull(Dgl1.Item(Col1Item, mRowIndex).Tag) & "'", AgL.GCn).ExecuteScalar())
                        End If
                    End If

            End Select

            'Only For opening Pending contra window
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemCategory, Col1ItemGroup, Col1Item, Col1Dimension1, Col1Dimension2, Col1Dimension3, Col1Dimension4
                    If Dgl1.Columns(mColumnIndex).Name = AgL.XNull(ClsMain.FGetSettings(SettingFields.MultiLineUIWindowBaseField, SettingType.Item,
                                                TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag,
                                                "", Dgl1.Item(Col1ItemType, mRowIndex).Tag, "", "", "")) Then
                        Select Case LblV_Type.Tag
                            Case Ncat.OpeningStock
                                FOpenMultiLineUI(mRowIndex)
                        End Select
                    End If


                    If Dgl1.Columns(mColumnIndex).Name = AgL.XNull(ClsMain.FGetSettings(SettingFields.ContraWindowBaseField, SettingType.Item,
                                                TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag,
                                                "", Dgl1.Item(Col1ItemType, mRowIndex).Tag, "", "", "")) Then
                        If LblV_Type.Tag <> Ncat.OpeningStock Then
                            FOpenPurchOrderForPurchReceive(mRowIndex)
                        End If
                    End If
            End Select




            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Validating_Dimension1(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim DtItem As DataTable = Nothing
        Dim StrReturnTicked As String = ""
        Try
            FOpenDimensionWindow(mRow, Col1Dimension1)
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Dimension1 Function ")
        End Try
    End Sub
    Private Sub Validating_Dimension2(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim DtItem As DataTable = Nothing
        Dim StrReturnTicked As String = ""
        Try
            FOpenDimensionWindow(mRow, Col1Dimension2)
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_DImension2 Function ")
        End Try
    End Sub
    Private Sub Validating_Dimension3(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim DtItem As DataTable = Nothing
        Dim StrReturnTicked As String = ""
        Try
            FOpenDimensionWindow(mRow, Col1Dimension3)
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_DImension3 Function ")
        End Try
    End Sub
    Private Sub Validating_Dimension4(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim DtItem As DataTable = Nothing
        Dim StrReturnTicked As String = ""
        Try
            FOpenDimensionWindow(mRow, Col1Dimension4)
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_DImension4 Function ")
        End Try
    End Sub
    Private Sub FOpenDimensionWindow(mRow As Integer, mColumnName As String)
        Dim bDimensionWindowBaseField As String = AgL.XNull(ClsMain.FGetSettings(SettingFields.DimensionWindowBaseField, SettingType.Item,
                TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag,
                "", Dgl1.Item(Col1ItemType, mRow).Tag, "", "", ""))

        If bDimensionWindowBaseField = "None" Or bDimensionWindowBaseField = "" Then
            bDimensionWindowBaseField = Col1Item
        End If

        If bDimensionWindowBaseField = mColumnName Then
            Dgl1.Item(Col1DocQty, mRow).Tag = Nothing
            If (Dgl1.Item(Col1Unit, mRow).Tag) Then
                Dgl1.Item(Col1DocQty, mRow).Style.ForeColor = Color.Blue
                ShowStockEntryDimensionDetail(mRow)
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

            mQry = " Select Ic.Unit, Ic.ItemType, It.Name As ItemTypeName, U.ShowDimensionDetailInSales, 
                    U.DecimalPlaces as QtyDecimalPlaces, Ic.* 
                    From ItemCategory Ic 
                    LEFT JOIN ItemType It On Ic.ItemType = It.Code
                    Left Join Unit U  With (NoLock) On Ic.Unit = U.Code 
                    Where Ic.Code = '" & Dgl1.Item(Col1ItemCategory, mRow).Tag & "'"
            Dim DtItemCategory As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItemCategory.Rows.Count > 0 Then
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtItemCategory.Rows(0)("Unit"))
                Dgl1.Item(Col1Unit, mRow).Tag = AgL.VNull(DtItemCategory.Rows(0)("ShowDimensionDetailInSales"))
                Dgl1.Item(Col1ItemType, mRow).Tag = AgL.XNull(DtItemCategory.Rows(0)("ItemType"))
                Dgl1.Item(Col1ItemType, mRow).Value = AgL.XNull(DtItemCategory.Rows(0)("ItemTypeName"))
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtItemCategory.Rows(0)("QtyDecimalPlaces"))
            End If
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
    Private Sub FrmStockEntry_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If AgL.Dman_Execute("Select Count(*) From StockHeadDetailTransfer With (NoLock) Where DocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar > 0 Then
            mQry = "Delete From StockHeadDetailTransfer Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If AgL.Dman_Execute("Select Count(*) From Barcode With (NoLock) Where GenDocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar > 0 Then
            'mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
            'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Select Code From Barcode With (NoLock) Where GenDocId = '" & mSearchCode & "' Order By Code Desc "
            Dim DtBarcode As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            If DtBarcode.Rows.Count > 0 Then
                mQry = "UPDATE StockHeadDetail Set Barcode = Null Where DocId = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            For I As Integer = 0 To DtBarcode.Rows.Count - 1
                mQry = "Delete From BarcodeSiteDetail Where Code = '" & AgL.XNull(DtBarcode.Rows(I)("Code")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = "Delete From Barcode Where Code = '" & AgL.XNull(DtBarcode.Rows(I)("Code")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Next

            mQry = " Delete From StockHeadDetailBarCodeValues Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = " Delete From StockHeadDimensionDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete from StockHeadDimensionDetailSku where DocID='" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From StockHeadDetailBomSku Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From StockHeadDetailBom Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()

        mQry = " Select B.*
                From Barcode B With (NoLock)
                LEFT JOIN BarcodeSiteDetail Bs With (NoLock) On B.Code = Bs.Code
                Where B.GenDocId = '" & mSearchCode & "'
                And B.GenDocId <> Bs.LastTrnDocID "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            If LblV_Type.Tag = Ncat.LrEntry Then
                MsgBox("Bale No " + AgL.XNull(DtTemp.Rows(0)("Specification1")) + " processed to another Process.", MsgBoxStyle.Information) : Passed = False : Exit Sub
            End If
        End If
    End Sub
    Private Sub FrmStockEntry_BaseEvent_DglMainCellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainCellEnter
        Select Case DglMain.CurrentCell.RowIndex
            Case rowPartyDocDate
                CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
        End Select
    End Sub
    Private Sub FrmStockEntry_BaseEvent_DglMainKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainKeyDown
        Try
            If DglMain.CurrentCell IsNot Nothing Then
                If e.KeyCode = Keys.Enter Then
                    Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                    If DglMain.CurrentCell.RowIndex = LastCell.RowIndex And DglMain.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                        If Dgl2.Visible Then
                            Dgl2.CurrentCell = Dgl2.Item(Col1Value, Dgl2.FirstDisplayedCell.RowIndex)
                            Dgl2.Focus()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FImportFromExcel()
        Dim mQry As String = ""
        Dim bHeadSubCodeName As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtStockHead As DataTable
        Dim DtStockHead_DataFields As DataTable
        Dim DtStockHeadDetail As DataTable = Nothing
        Dim DtStockHeadDetail_DataFields As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_NO' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_Date' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Godown' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bale No' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Lot No' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amount' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "


        DtStockHead_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Entry No' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bale No' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Lot No' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Text' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pcs' as [Field Name], 'Text' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 10 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Text' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amount' as [Field Name], 'Text' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        DtStockHeadDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As FrmImportPurchaseFromExcel
        ObjFrmImport = New FrmImportPurchaseFromExcel
        ObjFrmImport.Dgl1.DataSource = DtStockHead_DataFields
        ObjFrmImport.Dgl2.DataSource = DtStockHeadDetail_DataFields
        ObjFrmImport.Text = "Voucher Entry Import"
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtStockHead = ObjFrmImport.P_DsExcelData_PurchInvoice.Tables(0)
        DtStockHeadDetail = ObjFrmImport.P_DsExcelData_PurchInvoiceDetail.Tables(0)

        mFlag_Import = True

        Dim DtV_Date = DtStockHead.DefaultView.ToTable(True, "V_Date")
        For I = 0 To DtV_Date.Rows.Count - 1
            If AgL.XNull(DtV_Date.Rows(I)("V_Date")) <> "" Then
                If CDate(AgL.XNull(DtV_Date.Rows(I)("V_Date"))).Year < "2010" Then
                    If ErrorLog.Contains("These Dates are not valid") = False Then
                        ErrorLog += vbCrLf & "These Dates are not valid" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)("V_Date")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)("V_Date")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtV_Type = DtStockHead.DefaultView.ToTable(True, "V_Type")
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

        Dim DtParty = DtStockHead.DefaultView.ToTable(True, "Party Name")
        For I = 0 To DtParty.Rows.Count - 1
            If AgL.XNull(DtParty.Rows(I)("Party Name")).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = " & AgL.Chk_Text(AgL.XNull(DtParty.Rows(I)("Party Name")).ToString().Trim()) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtParty.Rows(I)("Party Name")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtParty.Rows(I)("Party Name")) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtStockHead_DataFields.Rows.Count - 1
            If AgL.XNull(DtStockHead_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtStockHead.Columns.Contains(AgL.XNull(DtStockHead_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtStockHead_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtStockHead_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
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


            For I = 0 To DtStockHead.Rows.Count - 1
                bHeadSubCodeName = ""
                Dim StockEntryTableList(0) As StructStockHead
                Dim StockEntryTable As New StructStockHead


                StockEntryTable.DocID = ""
                StockEntryTable.V_Type = AgL.XNull(DtStockHead.Rows(I)("V_Type"))
                StockEntryTable.V_Prefix = ""
                StockEntryTable.V_Date = AgL.XNull(DtStockHead.Rows(I)("V_Date"))
                StockEntryTable.V_No = AgL.VNull(DtStockHead.Rows(I)("V_No"))
                StockEntryTable.Div_Code = AgL.PubDivCode
                StockEntryTable.Site_Code = AgL.PubSiteCode
                StockEntryTable.ManualRefNo = AgL.VNull(DtStockHead.Rows(I)("Entry No"))
                StockEntryTable.SubCode = ""
                StockEntryTable.SubCodeName = AgL.XNull(DtStockHead.Rows(I)("Party Name"))
                StockEntryTable.Remarks = ""
                StockEntryTable.Status = "Active"
                StockEntryTable.StructureCode = ""
                StockEntryTable.CustomFields = ""
                StockEntryTable.PartyDocNo = ""
                StockEntryTable.PartyDocDate = ""
                StockEntryTable.EntryBy = AgL.PubUserName
                StockEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                StockEntryTable.ApproveBy = ""
                StockEntryTable.ApproveDate = ""
                StockEntryTable.MoveToLog = ""
                StockEntryTable.MoveToLogDate = ""



                Dim DtStockHeadDetail_ForHeader As New DataTable
                For M = 0 To DtStockHeadDetail.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtStockHeadDetail.Columns(M).ColumnName
                    DtStockHeadDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowStockHeadDetail_ForHeader As DataRow() = DtStockHeadDetail.Select("[V_Type] = " + AgL.Chk_Text(AgL.XNull(DtStockHead.Rows(I)("V_Type"))) + " And [Entry No] = " + AgL.Chk_Text(AgL.XNull(DtStockHead.Rows(I)("Entry No"))))
                If DtRowStockHeadDetail_ForHeader.Length > 0 Then
                    For M = 0 To DtRowStockHeadDetail_ForHeader.Length - 1
                        DtStockHeadDetail_ForHeader.Rows.Add()
                        For N = 0 To DtStockHeadDetail_ForHeader.Columns.Count - 1
                            DtStockHeadDetail_ForHeader.Rows(M)(N) = DtRowStockHeadDetail_ForHeader(M)(N)
                        Next
                    Next
                End If

                For J = 0 To DtStockHeadDetail_ForHeader.Rows.Count - 1
                    StockEntryTable.Line_Sr = J + 1
                    StockEntryTable.Line_SubCode = ""
                    StockEntryTable.Line_SubCodeName = ""


                    StockEntryTable.Line_ItemCode = ""
                    StockEntryTable.Line_ItemName = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Name"))
                    StockEntryTable.Line_Specification = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Specification"))
                    StockEntryTable.Line_LotNo = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Lot No"))
                    StockEntryTable.Line_BaleNo = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Bale No"))
                    'StockEntryTable.Line_ItemState = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item State"))
                    StockEntryTable.Line_Qty = AgL.VNull(DtStockHeadDetail_ForHeader.Rows(J)("Qty"))
                    StockEntryTable.Line_Pcs = AgL.VNull(DtStockHeadDetail_ForHeader.Rows(J)("Pcs"))
                    StockEntryTable.Line_Unit = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Unit"))
                    If DtStockHeadDetail_ForHeader.Columns.Contains("Rate") Then
                        StockEntryTable.Line_Rate = AgL.VNull(DtStockHeadDetail_ForHeader.Rows(J)("Rate"))
                    End If
                    If DtStockHeadDetail_ForHeader.Columns.Contains("Amount") Then
                        StockEntryTable.Line_Amount = AgL.VNull(DtStockHeadDetail_ForHeader.Rows(J)("Amount"))
                    End If
                    StockEntryTable.Line_Remarks = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Remark"))


                    StockEntryTableList(UBound(StockEntryTableList)) = StockEntryTable
                    ReDim Preserve StockEntryTableList(UBound(StockEntryTableList) + 1)
                Next

                InsertStockHead(StockEntryTableList)
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
    Public Structure StructStockHead
        Dim DocID As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As String
        Dim Div_Code As String
        Dim Site_Code As String
        Dim ManualRefNo As String
        Dim SubCode As String
        Dim SubCodeName As String
        Dim Process As String
        Dim Remarks As String
        Dim IsDeleted As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim ApproveBy As String
        Dim ApproveDate As String
        Dim MoveToLog As String
        Dim MoveToLogDate As String
        Dim Status As String
        Dim UID As String
        Dim ReferenceDocID As String
        Dim StructureCode As String
        Dim InUseBy As String
        Dim InUseToken As String
        Dim ResponsiblePerson As String
        Dim Transporter As String
        Dim InsurancePolicyNo As String
        Dim InsuredValue As String
        Dim Reason As String
        Dim PartyDocNo As String
        Dim PartyDocDate As String
        Dim CustomFields As String
        Dim GenDocId As String
        Dim GenDocIdSr As String
        Dim Remarks1 As String
        Dim Remarks2 As String
        Dim UploadDate As String

        '''''''''''''''''''''''''''''''''Line Detail''''''''''''''''''''''''''''''''''
        Dim Line_DocID As String
        Dim Line_Sr As String
        Dim Line_ItemCode As String
        Dim Line_ItemName As String
        Dim Line_Item_UID As String
        Dim Line_LotNo As String
        Dim Line_BaleNo As String
        Dim Line_Godown As String
        Dim Line_Qty As String
        Dim Line_Unit As String
        Dim Line_UnitMultiplier As String
        Dim Line_DealQty As String
        Dim Line_DealUnit As String
        Dim Line_Rate As String
        Dim Line_Amount As String
        Dim Line_Remarks As String
        Dim Line_Process As String
        Dim Line_Status As String
        Dim Line_CostCenter As String
        Dim Line_CurrentStock As String
        Dim Line_CurrentStockDealQty As String
        Dim Line_SubCode As String
        Dim Line_SubCodeName As String
        Dim Line_UID As String
        Dim Line_ReferenceNo As String
        Dim Line_ReferenceDocID As String
        Dim Line_ReferenceTSr As String
        Dim Line_ReferenceSr As String
        Dim Line_DifferenceQty As String
        Dim Line_DifferenceDealQty As String
        Dim Line_V_Nature As String
        Dim Line_Requisition As String
        Dim Line_RequisitionSr As String
        Dim Line_Manufacturer As String
        Dim Line_Pcs As String
        Dim Line_Length As String
        Dim Line_Width As String
        Dim Line_Thickness As String
        Dim Line_Weight As String
        Dim Line_GrossWeight As String
        Dim Line_DocQty As String
        Dim Line_Barcode As String
        Dim Line_Tag As String
        Dim Line_Specification As String
        Dim Line_ItemState As String
        Dim Line_Dimension1 As String
        Dim Line_Dimension2 As String
        Dim Line_Dimension3 As String
        Dim Line_Dimension4 As String
    End Structure
    Public Shared Function InsertStockHead(StockHeadTableList As StructStockHead()) As String
        Dim mQry As String = ""
        If StockHeadTableList(0).V_Type IsNot Nothing Then
            'StockHeadTableList(0).DocID = AgL.GetDocId(StockHeadTableList(0).V_Type, CStr(StockHeadTableList(0).V_No),
            '                                         CDate(StockHeadTableList(0).V_Date),
            '                                        IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), StockHeadTableList(0).Div_Code, StockHeadTableList(0).Site_Code)
            StockHeadTableList(0).DocID = AgL.CreateDocId(AgL, "StockHead", StockHeadTableList(0).V_Type, CStr(StockHeadTableList(0).V_No),
                                                     CDate(StockHeadTableList(0).V_Date),
                                                    IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), StockHeadTableList(0).Div_Code, StockHeadTableList(0).Site_Code)


            StockHeadTableList(0).V_Prefix = AgL.DeCodeDocID(StockHeadTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            StockHeadTableList(0).V_No = Val(AgL.DeCodeDocID(StockHeadTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))

            If AgL.Dman_Execute("Select Count(*) From StockHead With (NoLock) Where V_Type = '" & StockHeadTableList(0).V_Type & "'
                        And ManualRefNo = '" & StockHeadTableList(0).ManualRefNo & "'
                        And Div_Code = '" & StockHeadTableList(0).Div_Code & "'
                        And Site_Code = '" & StockHeadTableList(0).Site_Code & "'
                            ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() > 0 Or
                            StockHeadTableList(0).ManualRefNo = "" Then
                Dim mManualrefNoPrefix As String = AgL.XNull(AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix With (NoLock) Where V_Type = '" & StockHeadTableList(0).V_Type & "' 
                                And " & AgL.Chk_Date(StockHeadTableList(0).V_Date) & " >= Date(Date_From) 
                                And " & AgL.Chk_Date(StockHeadTableList(0).V_Date) & " <= Date(Date_To) ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
                StockHeadTableList(0).ManualRefNo = mManualrefNoPrefix + StockHeadTableList(0).V_No.ToString().PadLeft(4).Replace(" ", "0")
            End If

            StockHeadTableList(0).SubCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  '" & StockHeadTableList(0).SubCodeName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            StockHeadTableList(0).StructureCode = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type With (NoLock) Where V_Type = '" & StockHeadTableList(0).V_Type & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            mQry = "INSERT INTO StockHead (DocID,  V_Type, V_Prefix, V_Date, V_No,
                           Div_Code, Site_Code, ManualRefNo, Subcode, Transporter, Remarks, Status, 
                           Structure, CustomFields, PartyDocNo, PartyDocDate, EntryBy, EntryDate,
                           ApproveBy, ApproveDate, MoveToLog,
                           MoveToLogDate, UploadDate)
                            Select " & AgL.Chk_Text(StockHeadTableList(0).DocID) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).V_Type) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).V_Prefix) & ",  
                            " & AgL.Chk_Date(StockHeadTableList(0).V_Date) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).V_No) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).Div_Code) & ",
                            " & AgL.Chk_Text(StockHeadTableList(0).Site_Code) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).ManualRefNo) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).SubCode) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(0).Transporter) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(0).Remarks) & ",
                            " & AgL.Chk_Text(StockHeadTableList(0).Status) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).StructureCode) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).CustomFields) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).PartyDocNo) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).PartyDocDate) & ",  
                            " & AgL.Chk_Text(StockHeadTableList(0).EntryBy) & ",    
                            " & AgL.Chk_Date(StockHeadTableList(0).EntryDate) & ",    
                            " & AgL.Chk_Text(StockHeadTableList(0).ApproveBy) & ",    
                            " & AgL.Chk_Date(StockHeadTableList(0).ApproveDate) & ",    
                            " & AgL.Chk_Text(StockHeadTableList(0).MoveToLog) & ",    
                            " & AgL.Chk_Date(StockHeadTableList(0).MoveToLogDate) & ",    
                            " & AgL.Chk_Date(StockHeadTableList(0).UploadDate) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)




            For I As Integer = 0 To StockHeadTableList.Length - 1
                If StockHeadTableList(I).Line_Qty IsNot Nothing And StockHeadTableList(I).Line_Qty <> 0 Then

                    If StockHeadTableList(I).Line_ItemCode = "" Then
                        StockHeadTableList(I).Line_ItemCode = AgL.Dman_Execute("SELECT Code FROM Item With (NoLock) Where Description =  " & AgL.Chk_Text(StockHeadTableList(I).Line_ItemName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

                        If StockHeadTableList(I).Line_ItemCode = "" Or StockHeadTableList(I).Line_ItemCode Is Nothing Then
                            StockHeadTableList(I).Line_ItemCode = AgL.Dman_Execute("SELECT Code FROM Item With (NoLock) Where Specification  =  '" & StockHeadTableList(I).Line_ItemName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                        End If

                        If StockHeadTableList(I).Line_ItemCode = "" Then
                            Err.Raise(1,, "Item is saving blank in Stock Head detail for " & StockHeadTableList(0).ManualRefNo)
                        End If
                    End If

                    mQry = "Insert Into StockHeadDetail(DocId, Sr, Item, Specification, BaleNo, LotNo, " &
                       " DocQty, Qty, Unit, Pcs, Rate, Amount, Remark)
                            Select " & AgL.Chk_Text(StockHeadTableList(0).DocID) & ", 
                            " & Val(StockHeadTableList(I).Line_Sr) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_ItemCode) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_Specification) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_BaleNo) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_LotNo) & ", 
                            " & Val(StockHeadTableList(I).Line_DocQty) & ", 
                            " & Val(StockHeadTableList(I).Line_Qty) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_Unit) & ", 
                            " & Val(StockHeadTableList(I).Line_Pcs) & ", 
                            " & Val(StockHeadTableList(I).Line_Rate) & ", 
                            " & Val(StockHeadTableList(I).Line_Amount) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_Remarks) & ""
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                End If
            Next

            mQry = "Insert Into StockHeadDetailSku
                            (DocId, Sr, ItemCategory, ItemGroup, Item, Dimension1, 
                            Dimension2, Dimension3, Dimension4, Size) "
            mQry += " Select L.DocId, L.Sr, Null As ItemCategory, Null As ItemGroup, L.Item, Null As Dimension1, 
                        Null As Dimension2, Null As Dimension3, Null As Dimension4, Null As Size
                        From StockHeadDetail L 
                        Where L.DocId = '" & StockHeadTableList(0).DocID & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            Dim DtNCat As DataTable = AgL.FillData("Select NCat, Nature As NCatNature
                From Voucher_Type 
                Where V_Type = '" & AgL.XNull(StockHeadTableList(0).V_Type) & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            If CType(AgL.VNull(ClsMain.FGetSettings(SettingFields.PostInStockYn, SettingType.General, StockHeadTableList(0).Div_Code,
                    StockHeadTableList(0).Site_Code, "", AgL.XNull(DtNCat.Rows(0)("NCat")),
                    StockHeadTableList(0).V_Type, "", "")), Boolean) = True Then
                mQry = "INSERT INTO Stock (DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, SubCode, Item, Godown, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss, DealQty_Rec, DealUnit, Rate, Amount, Remarks, RecId, Barcode)
                        SELECT L.DocID, L.Sr AS TSr, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.SubCode, L.Item, L.Godown, 
                        Case When '" & AgL.XNull(DtNCat.Rows(0)("NCatNature")) & "' = '" & NCatNature.Issue & "' Then L.Qty Else 0 End As Qty_Iss, 
                        Case When '" & AgL.XNull(DtNCat.Rows(0)("NCatNature")) & "' = '" & NCatNature.Receive & "' Then L.Qty Else 0 End As Qty_Rec, 
                        L.Unit, L.UnitMultiplier, 
                        Case When '" & AgL.XNull(DtNCat.Rows(0)("NCatNature")) & "' = '" & NCatNature.Issue & "' Then L.DealQty Else 0 End As DealQty_Iss, 
                        Case When '" & AgL.XNull(DtNCat.Rows(0)("NCatNature")) & "' = '" & NCatNature.Receive & "' Then L.DealQty Else 0 End As DealQty_Rec, 
                        L.DealUnit, L.Rate, L.Amount, L.Remarks, 
                        H.ManualRefNo AS RecId, L.Barcode
                        FROM StockHeadDetail L
                        LEFT JOIN StockHead H ON L.DocID = H.DocID
                        WHERE L.DocId = '" & StockHeadTableList(0).DocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            AgL.UpdateVoucherCounter(StockHeadTableList(0).DocID, CDate(StockHeadTableList(0).V_Date), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
        End If
        Return StockHeadTableList(0).DocID
    End Function
    Private Sub FrmSaleInvoiceDirect_BaseEvent_Topctrl_tbMore() Handles Me.BaseEvent_Topctrl_tbMore
        MnuOptions.Show(Topctrl1, Topctrl1.btbSite.Rectangle.X, Topctrl1.btbSite.Rectangle.Y + Topctrl1.btbSite.Rectangle.Size.Height)
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
    Private Sub Dgl2_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(Dgl2)
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
    Private Sub FGeterateSkuName(bRowIndex As Integer)
        If Dgl1.Item(Col1ItemCategory, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1ItemGroup, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Item, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Dimension1, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Dimension2, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Dimension3, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Dimension4, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Size, bRowIndex).Value <> "" Then
            Dgl1.Item(Col1SKU, bRowIndex).Value = Dgl1.Item(Col1ItemCategory, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1ItemGroup, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Item, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Dimension1, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Dimension2, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Dimension3, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Dimension4, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Size, bRowIndex).Value

            If Dgl1.Item(Col1Item, bRowIndex).Tag <> "" And
                       Dgl1.Item(Col1Dimension1, bRowIndex).Tag = "" And
                       Dgl1.Item(Col1Dimension2, bRowIndex).Tag = "" And
                       Dgl1.Item(Col1Dimension3, bRowIndex).Tag = "" And
                       Dgl1.Item(Col1Dimension4, bRowIndex).Tag = "" And
                       Dgl1.Item(Col1Size, bRowIndex).Tag = "" Then
                Dgl1.Item(Col1SKU, bRowIndex).Tag = Dgl1.Item(Col1Item, bRowIndex).Tag
            Else
                Dim DrSKU As DataRow() = AgL.PubDtItem.Select(" IsNull(ItemCategory,'') = '" & Dgl1.Item(Col1ItemCategory, bRowIndex).Tag & "'
                                    And IsNull(ItemGroup,'') = '" & Dgl1.Item(Col1ItemGroup, bRowIndex).Tag & "'
                                    And IsNull(BaseItem,'') = '" & Dgl1.Item(Col1Item, bRowIndex).Tag & "'
                                    And IsNull(Dimension1,'') = '" & Dgl1.Item(Col1Dimension1, bRowIndex).Tag & "'
                                    And IsNull(Dimension2,'') = '" & Dgl1.Item(Col1Dimension2, bRowIndex).Tag & "'
                                    And IsNull(Dimension3,'') = '" & Dgl1.Item(Col1Dimension3, bRowIndex).Tag & "'
                                    And IsNull(Dimension4,'') = '" & Dgl1.Item(Col1Dimension4, bRowIndex).Tag & "'
                                    And IsNull(Size,'') = '" & Dgl1.Item(Col1Size, bRowIndex).Tag & "'")
                If DrSKU.Length > 0 Then
                    Dgl1.Item(Col1SKU, bRowIndex).Tag = AgL.XNull(DrSKU(0)("Code"))
                End If
            End If
        Else
            Dgl1.Item(Col1SKU, bRowIndex).Value = ""
        End If
    End Sub
    Private Sub FOpenPurchOrderForPurchReceive(mRow As Integer)
        Dim DtTemp As DataTable
        Dim StrRtn As String = ""
        Dim bPendingOrderQry As String = ""

        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        bPendingOrderQry = " SELECT VOrder.PurchOrder, VOrder.PurchOrderSr, IsNull(VOrder.OrderQty,0) - IsNull(VReceive.ReceiveQty,0) AS BalanceQty
                FROM (
                    SELECT L.PurchOrder, L.PurchOrderSr, Sum(L.Qty) AS OrderQty
                    FROM PurchOrder H 
                    LEFT JOIN PurchOrderDetail L ON H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    Where H.Vendor = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                    And H.Process = '" & DglMain.Item(Col1Value, rowProcess).Tag & "'
	                GROUP BY L.PurchOrder, L.PurchOrderSr
                ) AS VOrder
                LEFT JOIN (
                    SELECT L.ReferenceDocId As PurchOrder, L.ReferenceTSr As PurchOrderSr, Sum(L.Qty_Rec) AS ReceiveQty
                    FROM Stock L 
                    GROUP BY L.ReferenceDocId, L.ReferenceTSr	
                ) AS VReceive ON VOrder.PurchOrder = VReceive.PurchOrder AND VOrder.PurchOrderSr = VReceive.PurchOrderSr 
                WHERE 1=1 
                And IsNull(VOrder.OrderQty,0) - IsNull(VReceive.ReceiveQty,0) > 0 "

        mQry = " Select 'o' As Tick, L.DocID || '#' || Cast(L.Sr as Varchar) As SearchKey, 
                H.V_Type || '-' || H.ManualRefNo As PurchOrderNo, H.V_Date As PurchOrderDate, 
                Ic.Description As ItemCategory, Ig.Description As ItemGroup, I.Description As Item,
                D1.Description As Dimension1, D2.Description As Dimension2, 
                D3.Description As Dimension3, D4.Description As Dimension4,
                Size.Description As Size, VPendingOrder.BalanceQty, L.Unit,
                Sku.Code As SkuCode, Ic.Code As ItemCategoryCode, Ig.Code As ItemGroupCode, I.Code As ItemCode,
                D1.Code As Dimension1Code, D2.Code As Dimension2Code, 
                D3.Code As Dimension3Code, D4.Code As Dimension4Code,
                Size.Code As SizeCode, It.Code As ItemTypeCode, It.Name As ItemType, 
                Rm.Code As RawMaterial, Rm.Description As RawMaterialDesc,
                VPendingOrder.PurchOrder, VPendingOrder.PurchOrderSr,
                L.DealUnit, L.UnitMultiplier, L.UnitMultiplier * VPendingOrder.BalanceQty As DealQty,
                L.Barcode, Bc.Description As BarcodeDesc
                FROM (" & bPendingOrderQry & ") As VPendingOrder
                LEFT JOIN PurchOrderDetail L On VPendingOrder.PurchOrder = L.DocId And VPendingOrder.PurchOrderSr = L.Sr 
                LEFT JOIN PurchOrder H On L.DocId = H.DocId 
                LEFT JOIN Item Sku ON Sku.Code = L.Item
                LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                LEFT JOIN Item IC On Sku.ItemCategory = IC.Code
                LEFT JOIN Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size 
                LEFT JOIN Item Rm ON Rm.Code = L.RawMaterial
                LEFT JOIN Barcode Bc On L.Barcode = Bc.Code
                Where 1=1 "

        If mRow >= 0 Then
            If AgL.XNull(Dgl1.Item(Col1ItemCategory, mRow).Tag) <> "" Then
                mQry += " And Ic.Code = '" & Dgl1.Item(Col1ItemCategory, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1ItemGroup, mRow).Tag) <> "" Then
                mQry += " And Ig.Code = '" & Dgl1.Item(Col1ItemGroup, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Item, mRow).Tag) <> "" Then
                mQry += " And I.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Dimension1, mRow).Tag) <> "" Then
                mQry += " And D1.Code = '" & Dgl1.Item(Col1Dimension1, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Dimension2, mRow).Tag) <> "" Then
                mQry += " And D2.Code = '" & Dgl1.Item(Col1Dimension2, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Dimension3, mRow).Tag) <> "" Then
                mQry += " And D3.Code = '" & Dgl1.Item(Col1Dimension3, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Dimension4, mRow).Tag) <> "" Then
                mQry += " And D4.Code = '" & Dgl1.Item(Col1Dimension4, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Size, mRow).Tag) <> "" Then
                mQry += " And Size.Code = '" & Dgl1.Item(Col1Size, mRow).Tag & "'"
            End If
        End If
        mQry += " Order By H.V_Date, H.ManualRefNo "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count = 0 Then Exit Sub

        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 990, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item Category", 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[ItemCategory] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(5, "Item Group", 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[ItemGroup] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(6, "Item", 200, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Item] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(7, AgL.PubCaptionDimension1, 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension1] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(8, AgL.PubCaptionDimension2, 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension2] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(9, AgL.PubCaptionDimension3, 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension3] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(10, AgL.PubCaptionDimension4, 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension4] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(11, "Size", 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Size] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(12, "Bal Qty", 100, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(13, "Unit", 70, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.FFormatColumn(14, , 0, , False)
        FRH_Multiple.FFormatColumn(15, , 0, , False)
        FRH_Multiple.FFormatColumn(16, , 0, , False)
        FRH_Multiple.FFormatColumn(17, , 0, , False)
        FRH_Multiple.FFormatColumn(18, , 0, , False)
        FRH_Multiple.FFormatColumn(19, , 0, , False)
        FRH_Multiple.FFormatColumn(20, , 0, , False)
        FRH_Multiple.FFormatColumn(21, , 0, , False)
        FRH_Multiple.FFormatColumn(22, , 0, , False)
        FRH_Multiple.FFormatColumn(23, , 0, , False)
        FRH_Multiple.FFormatColumn(24, , 0, , False)
        FRH_Multiple.FFormatColumn(25, , 0, , False)
        FRH_Multiple.FFormatColumn(26, , 0, , False)
        FRH_Multiple.FFormatColumn(27, , 0, , False)
        FRH_Multiple.FFormatColumn(28, , 0, , False)
        FRH_Multiple.FFormatColumn(29, , 0, , False)
        FRH_Multiple.FFormatColumn(30, , 0, , False)
        FRH_Multiple.FFormatColumn(31, , 0, , False)
        FRH_Multiple.FFormatColumn(32, , 0, , False)
        FRH_Multiple.FFormatColumn(33, , 0, , False)


        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()


        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If

        Dim DrSelected As DataRow()
        If FRH_Multiple.ChkAll.Checked = True Then
            DrSelected = DtTemp.Select("1=1")
        ElseIf StrRtn <> "" Then
            DrSelected = DtTemp.Select("SearchKey In (" & StrRtn & ")")
        Else
            Exit Sub
        End If

        If mRow < 0 Then
            If Dgl1.Rows.Count > 1 Then
                If MsgBox("Do you want to overwrite existing data in grid ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'Dgl1.Rows.Clear() : Dgl1.RowCount = 1 : mRow = 0
                    For I As Integer = 0 To Dgl1.Rows.Count - 1
                        If AgL.VNull(Dgl1.Item(Col1IsRecordLocked, I).Value) = 0 Then
                            If Not Dgl1.Rows(I).IsNewRow Then
                                Dgl1.Rows(I).Visible = False
                            End If
                        End If
                    Next
                    mRow = Dgl1.Rows.Count - 1
                Else
                    mRow = Dgl1.Rows.Count - 1
                End If
            Else
                mRow = 0
            End If
        End If


        If Dgl1.Rows(mRow).IsNewRow = False Then
            Dgl1.Rows.Remove(Dgl1.Rows(mRow))
        End If
        Dgl1.Rows.Insert(mRow, DrSelected.Length)
        For I As Integer = 0 To DrSelected.Length - 1
            Dgl1.Item(Col1ReferenceDocId, mRow + I).Tag = AgL.XNull(DrSelected(I)("PurchOrder"))
            Dgl1.Item(Col1ReferenceDocId, mRow + I).Value = AgL.XNull(DrSelected(I)("PurchOrderNo"))
            Dgl1.Item(Col1ReferenceDocIdTSr, mRow + I).Value = AgL.XNull(DrSelected(I)("PurchOrderSr"))
            Dgl1.Item(Col1ReferenceDocIdSr, mRow + I).Value = 0
            Dgl1.Item(Col1ReferenceDocIdDate, mRow + I).Value = AgL.XNull(DrSelected(I)("PurchOrderDate"))

            Dgl1.Item(Col1Barcode, mRow + I).Tag = AgL.XNull(DrSelected(I)("Barcode"))
            Dgl1.Item(Col1Barcode, mRow + I).Value = AgL.XNull(DrSelected(I)("BarcodeDesc"))

            Dgl1.Item(Col1SKU, mRow + I).Tag = AgL.XNull(DrSelected(I)("SkuCode"))

            Dgl1.Item(Col1ItemCategory, mRow + I).Tag = AgL.XNull(DrSelected(I)("ItemCategoryCode"))
            Dgl1.Item(Col1ItemCategory, mRow + I).Value = AgL.XNull(DrSelected(I)("ItemCategory"))

            Dgl1.Item(Col1ItemGroup, mRow + I).Tag = AgL.XNull(DrSelected(I)("ItemGroupCode"))
            Dgl1.Item(Col1ItemGroup, mRow + I).Value = AgL.XNull(DrSelected(I)("ItemGroup"))

            Dgl1.Item(Col1Item, mRow + I).Tag = AgL.XNull(DrSelected(I)("ItemCode"))
            Dgl1.Item(Col1Item, mRow + I).Value = AgL.XNull(DrSelected(I)("Item"))

            Dgl1.Item(Col1Dimension1, mRow + I).Tag = AgL.XNull(DrSelected(I)("Dimension1Code"))
            Dgl1.Item(Col1Dimension1, mRow + I).Value = AgL.XNull(DrSelected(I)("Dimension1"))

            Dgl1.Item(Col1Dimension2, mRow + I).Tag = AgL.XNull(DrSelected(I)("Dimension2Code"))
            Dgl1.Item(Col1Dimension2, mRow + I).Value = AgL.XNull(DrSelected(I)("Dimension2"))

            Dgl1.Item(Col1Dimension3, mRow + I).Tag = AgL.XNull(DrSelected(I)("Dimension3Code"))
            Dgl1.Item(Col1Dimension3, mRow + I).Value = AgL.XNull(DrSelected(I)("Dimension3"))

            Dgl1.Item(Col1Dimension4, mRow + I).Tag = AgL.XNull(DrSelected(I)("Dimension4Code"))
            Dgl1.Item(Col1Dimension4, mRow + I).Value = AgL.XNull(DrSelected(I)("Dimension4"))

            Dgl1.Item(Col1Size, mRow + I).Tag = AgL.XNull(DrSelected(I)("SizeCode"))
            Dgl1.Item(Col1Size, mRow + I).Value = AgL.XNull(DrSelected(I)("Size"))


            Dgl1.Item(Col1Qty, mRow + I).Value = AgL.XNull(DrSelected(I)("BalanceQty"))
            Dgl1.Item(Col1DocQty, mRow + I).Value = AgL.XNull(DrSelected(I)("BalanceQty"))
            Dgl1.Item(Col1Unit, mRow + I).Value = AgL.XNull(DrSelected(I)("Unit"))

            Dgl1.Item(Col1UnitMultiplier, mRow + I).Value = AgL.VNull(DrSelected(I)("UnitMultiplier"))
            Dgl1.Item(Col1DealQty, mRow + I).Value = AgL.VNull(DrSelected(I)("DealQty"))
            Dgl1.Item(Col1DealUnit, mRow + I).Value = AgL.XNull(DrSelected(I)("DealUnit"))

            Dgl1.Item(Col1RawMaterial, mRow + I).Tag = AgL.XNull(DrSelected(I)("RawMaterial"))
            Dgl1.Item(Col1RawMaterial, mRow + I).Value = AgL.XNull(DrSelected(I)("RawMaterialDesc"))
            Dgl1.Item(Col1ReferenceDocIdBalanceQty, mRow + I).Value = AgL.XNull(DrSelected(I)("BalanceQty"))

            Dgl1.Item(Col1RawMaterialConsumptionQty, mRow + I).Value = FGetFabricConsumption_Garment(mRow + I)
        Next

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item(ColSNo, I).Value = I + 1
            FGeterateSkuName(I)
        Next
        Calculation()
    End Sub
    Private Sub Dgl2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellContentClick
        If e.ColumnIndex = Dgl2.Columns(Col1Value).Index And TypeOf (Dgl2(Col1Value, e.RowIndex)) Is DataGridViewButtonCell Then
            Select Case e.RowIndex
                Case rowBtnPendingPurchOrder
                    FOpenPenginPurchaseOrderOption()
                Case rowBtnStockBalance
                    FOpenStockBalance(-1)
                Case rowBtnStandardConsumption
                    If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                        FFillStandardConsumption_Garment()
                    Else
                        FFillStandardConsumption()
                    End If
                Case rowBtnAttachments
                    ShowAttachments()
            End Select
        End If
    End Sub
    Public Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag, "", LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowProcess).Tag, DglMain.Item(Col1Value, rowSettingGroup).Tag)
        FGetSettings = mValue
    End Function
    Private Sub FGetProcessFromVoucher_Type()
        If DglMain.Item(Col1Head, rowProcess).Tag Is Nothing Then
            DglMain.Item(Col1Head, rowProcess).Tag = FCreateHelpProcess()
        End If

        If CType(DglMain.Item(Col1Head, rowProcess).Tag, DataSet).Tables(0).Rows.Count = 1 Then
            DglMain.Item(Col1Value, rowProcess).Tag = CType(DglMain.Item(Col1Head, rowProcess).Tag, DataSet).Tables(0).Rows(0)("Code")
        End If

        If AgL.XNull(DglMain.Item(Col1Value, rowProcess).Tag) <> "" Then
            DglMain.Item(Col1Value, rowProcess).Value = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup 
                                Where SubCode = '" & DglMain.Item(Col1Value, rowProcess).Tag & "'", AgL.GCn).ExecuteScalar())
        End If
    End Sub
    Private Sub FrmStockEntry_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "SELECT Ir.*, I.V_Type As ItemV_Type, RI.V_Type As RelatedItemV_Type 
                FROM ItemRelation Ir 
                LEFT JOIN Item I On Ir.Item = I.Code 
                LEFT JOIN Item RI On Ir.RelatedItem = Ri.Code "
        DtItemRelation = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If mV_Type <> "" Then
            mQry = "Select V_Type as Code, Description, NCat, Category, Nature, IsFutureDateTransactionAllowed " &
                   "From Voucher_Type " &
                   "Where V_Type = '" & mV_Type & "' "
            DtVoucher_TypeHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub
    Private Function FGetRelationalData() As Boolean
        Dim DtRelationalData As DataTable
        Try
            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From PurchInvoiceDetail L
                        LEFT JOIN PurchInvoice H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.ReferenceDocId = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & DglMain(Col1Value, rowV_Type).Value + "-" + DglMain(Col1Value, rowReferenceNo).Value & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
    Private Sub FOpenMultiLineUI(mRow As Integer)
        Dim FrmObj As FrmStockEntryMultiLineUI = New FrmStockEntryMultiLineUI
        FrmObj.DglRow = Dgl1.Rows(mRow)
        FrmObj.EntryMode = Topctrl1.Mode
        FrmObj.VType = DglMain.Item(Col1Value, rowV_Type).Tag
        FrmObj.Site = DglMain.Item(Col1Value, rowSite_Code).Tag
        FrmObj.SettingGroup = DglMain.Item(Col1Value, rowSettingGroup).Tag
        FrmObj.PartyCode = DglMain.Item(Col1Value, rowParty).Tag
        FrmObj.DivCode = TxtDivision.Tag
        FrmObj.IniGrid(mSearchCode)
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()

        If FrmObj.mOkButtonPressed = True Then
            Dim DglCurrentRow As DataGridViewRow = Dgl1.Rows(mRow)

            If FrmObj.Dgl1.Rows.Count > 0 Then
                Dgl1.Rows(mRow).Visible = False

                Dgl1.Rows.Insert(mRow, FrmObj.Dgl1.Rows.Count - 1)
                For I As Integer = 0 To FrmObj.Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1ItemType, mRow + I).Tag = AgL.XNull(DglCurrentRow.Cells(Col1ItemType).Tag)
                    Dgl1.Item(Col1ItemType, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1ItemType).Value)

                    Dgl1.Item(Col1ItemCategory, mRow + I).Tag = AgL.XNull(DglCurrentRow.Cells(Col1ItemCategory).Tag)
                    Dgl1.Item(Col1ItemCategory, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1ItemCategory).Value)

                    Dgl1.Item(Col1ItemGroup, mRow + I).Tag = AgL.XNull(DglCurrentRow.Cells(Col1ItemGroup).Tag)
                    Dgl1.Item(Col1ItemGroup, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1ItemGroup).Value)

                    Dgl1.Item(Col1Item, mRow + I).Tag = AgL.XNull(DglCurrentRow.Cells(Col1Item).Tag)
                    Dgl1.Item(Col1Item, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1Item).Value)

                    Dgl1.Item(Col1Dimension1, mRow + I).Tag = AgL.XNull(DglCurrentRow.Cells(Col1Dimension1).Tag)
                    Dgl1.Item(Col1Dimension1, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1Dimension1).Value)

                    Dgl1.Item(Col1Dimension2, mRow + I).Tag = AgL.XNull(DglCurrentRow.Cells(Col1Dimension2).Tag)
                    Dgl1.Item(Col1Dimension2, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1Dimension2).Value)

                    Dgl1.Item(Col1Dimension3, mRow + I).Tag = AgL.XNull(DglCurrentRow.Cells(Col1Dimension3).Tag)
                    Dgl1.Item(Col1Dimension3, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1Dimension3).Value)

                    Dgl1.Item(Col1Dimension4, mRow + I).Tag = AgL.XNull(DglCurrentRow.Cells(Col1Dimension4).Tag)
                    Dgl1.Item(Col1Dimension4, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1Dimension4).Value)

                    Dgl1.Item(Col1Unit, mRow + I).Value = AgL.XNull(DglCurrentRow.Cells(Col1Unit).Value)

                    Dgl1.Item(Col1Size, mRow + I).Tag = FrmObj.Dgl1.Item(FrmStockEntryMultiLineUI.Col1Size, I).Tag
                    Dgl1.Item(Col1Size, mRow + I).Value = FrmObj.Dgl1.Item(FrmStockEntryMultiLineUI.Col1Size, I).Value

                    Dgl1.Item(Col1DocQty, mRow + I).Value = FrmObj.Dgl1.Item(FrmStockEntryMultiLineUI.Col1Qty, I).Value
                    Dgl1.Item(Col1Qty, mRow + I).Value = Dgl1.Item(Col1DocQty, mRow + I).Value

                    Dgl1.Item(Col1Rate, mRow + I).Value = FrmObj.Dgl1.Item(FrmStockEntryMultiLineUI.Col1Rate, I).Value

                Next

                For I As Integer = 0 To Dgl1.Rows.Count - 1
                    Dgl1.Item(ColSNo, I).Value = I + 1

                    FGeterateSkuName(I)
                Next
                Calculation()
            End If
        End If
        Dgl1.CurrentCell = Dgl1.Item(Dgl1.FirstDisplayedCell.ColumnIndex, Dgl1.Rows.Count - 1)
    End Sub
    Private Sub ShowStockEntryDimensionDetail(mRow As Integer, Optional IsShowFrm As Boolean = True)
        If mRow < 0 Then Exit Sub
        If Dgl1.Item(Col1DocQty, mRow).Tag IsNot Nothing Then
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).EntryMode = Topctrl1.Mode
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).EntryNCatNature = LblNCatNature.Tag
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).objFrmStockEntry = Me
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).DglRow = Dgl1.Rows(mRow)
            CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).FReInitializeDimensionColumns()

            If IsShowFrm = True Then
                Dgl1.Item(Col1DocQty, mRow).Tag.ShowDialog()
                Dgl1.Item(Col1DocQty, mRow).Value = CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).GetTotalQty
                Dgl1.Item(Col1Qty, mRow).Value = CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).GetTotalQty
            End If
        Else
            If Dgl1.Item(Col1Unit, mRow).Tag Then
                Dim FrmObj As FrmStockEntryDimension
                FrmObj = New FrmStockEntryDimension
                FrmObj.ItemName = Dgl1.Item(Col1Item, mRow).Value
                FrmObj.Unit = Dgl1.Item(Col1Unit, mRow).Value
                FrmObj.UnitDecimalPlace = Val(Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value)
                FrmObj.DglRow = Dgl1.Rows(mRow)
                FrmObj.EntryMode = Topctrl1.Mode
                FrmObj.EntryNCatNature = LblNCatNature.Tag
                FrmObj.objFrmStockEntry = Me
                FrmObj.IniGrid(mSearchCode, Val(Dgl1.Item(ColSNo, mRow).Tag))
                FrmObj.FReInitializeDimensionColumns()
                Dgl1.Item(Col1DocQty, mRow).Tag = FrmObj

                If IsShowFrm = True Then
                    Dgl1.Item(Col1DocQty, mRow).Tag.ShowDialog()
                    Dgl1.Item(Col1DocQty, mRow).Value = CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).GetTotalQty
                    Dgl1.Item(Col1Qty, mRow).Value = CType(Dgl1.Item(Col1DocQty, mRow).Tag, FrmStockEntryDimension).GetTotalQty
                End If
            End If
        End If
    End Sub
    Private Sub Dgl1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        Dim mRow As Integer
        mRow = e.RowIndex
        If Dgl1.Columns(e.ColumnIndex).Name = Col1DocQty Then ShowStockEntryDimensionDetail(mRow)
    End Sub
    Private Sub Dgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dgl1.KeyPress
        If Dgl1.CurrentCell Is Nothing Then Exit Sub

        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1DocQty).Index Then
            If AgL.VNull(Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Tag) Then
                If e.KeyChar = Chr(Keys.Space) Then
                    ShowStockEntryDimensionDetail(Dgl1.CurrentCell.RowIndex)
                    e.Handled = True
                End If
            End If
        End If
    End Sub
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportOpeningFromExcel.Click, MnuReport.Click, MnuHistory.Click, MnuWizard.Click, MnuPrintQACopy.Click, MnuShowLedgerPosting.Click
        Select Case sender.name
            Case MnuImportFromExcel.Name
                FImportFromExcel()

            Case MnuImportOpeningFromExcel.Name
                FImportOpeningFromExcel()

            Case MnuPrintQACopy.Name
                FGetPrint(mSearchCode, PrintFor.QA)

            Case MnuEditSave.Name
                FEditSaveAllEntries()

            Case MnuHistory.Name
                ClsMain.FShowHistory(mSearchCode, Me)

            Case MnuWizard.Name
                FWizard()

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

            Case MnuShowLedgerPosting.Name
                FShowLedgerPosting()
        End Select
    End Sub
    Private Sub FWizard()
        Dim StrSenderText As String = Me.Text
        GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
        GridReportFrm.Filter_IniGrid()

        Dim CRep As ClsStockIssue = New ClsStockIssue(GridReportFrm)
        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
        If AgL.XNull(DglMain.Item(Col1Value, rowV_Type).Tag) <> "" Then
            CRep.V_Type = AgL.XNull(DglMain.Item(Col1Value, rowV_Type).Tag)
        Else
            CRep.V_Type = AgL.XNull(AgL.Dman_Execute(" Select V_Type From Voucher_Type Where NCat = '" & EntryNCat & "'", AgL.GCn).ExecuteScalar())
        End If
        CRep.ObjFrm = Me
        CRep.Ini_Grid()
        GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 0).Value = AgL.PubStartDate
        GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 1).Value = AgL.PubLoginDate
        ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
        GridReportFrm.MdiParent = Me.MdiParent
        GridReportFrm.Show()
        CRep.ProcStockIssue()
    End Sub
    Public Sub FImportOpeningFromExcel()
        Dim mQry As String = ""
        Dim bHeadSubCodeName As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtStock As DataTable
        Dim DtStock_DataFields As DataTable
        Dim DtMain As DataTable = Nothing
        Dim DtItem As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_No' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_Date' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Group' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Category' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bale No' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Lot No' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Text' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pcs' as [Field Name], 'Text' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 10 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Text' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amount' as [Field Name], 'Text' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        DtStock_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As FrmImportFromExcel
        ObjFrmImport = New FrmImportFromExcel
        ObjFrmImport.Dgl1.DataSource = DtStock_DataFields
        ObjFrmImport.Text = "Opening Stock Import"
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtStock = ObjFrmImport.P_DsExcelData.Tables(0)

        mFlag_Import = True

        Dim DtV_Date = DtStock.DefaultView.ToTable(True, "V_Date")
        For I = 0 To DtV_Date.Rows.Count - 1
            If AgL.XNull(DtV_Date.Rows(I)("V_Date")) <> "" Then
                If CDate(AgL.XNull(DtV_Date.Rows(I)("V_Date"))).Year < "2010" Then
                    If ErrorLog.Contains("These Dates are not valid") = False Then
                        ErrorLog += vbCrLf & "These Dates are not valid" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)("V_Date")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)("V_Date")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtV_Type = DtStock.DefaultView.ToTable(True, "V_Type")
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


        For I = 0 To DtStock_DataFields.Rows.Count - 1
            If AgL.XNull(DtStock_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtStock.Columns.Contains(AgL.XNull(DtStock_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtStock_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtStock_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
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

        mQry = "Select I.Code, I.Specification, Ig.Description As ItemGroupDesc, 
                Ic.Description As ItemCategoryDesc, I.Description As ItemDesc 
                From Item I 
                LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                Where I.V_Type = '" & ItemV_Type.Item & "'"
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim DtStockHeader As DataTable
            DtStockHeader = DtStock.DefaultView.ToTable(True, "V_Type", "V_No", "V_Date")


            For I = 0 To DtStockHeader.Rows.Count - 1
                bHeadSubCodeName = ""
                Dim StockEntryTableList(0) As StructStockHead
                Dim StockEntryTable As New StructStockHead


                StockEntryTable.DocID = ""
                StockEntryTable.V_Type = AgL.XNull(DtStockHeader.Rows(I)("V_Type"))
                StockEntryTable.V_Prefix = ""
                StockEntryTable.V_Date = AgL.XNull(DtStockHeader.Rows(I)("V_Date"))
                StockEntryTable.V_No = AgL.VNull(DtStockHeader.Rows(I)("V_No"))
                StockEntryTable.Div_Code = AgL.PubDivCode
                StockEntryTable.Site_Code = AgL.PubSiteCode
                StockEntryTable.ManualRefNo = AgL.VNull(DtStockHeader.Rows(I)("V_No"))
                StockEntryTable.SubCode = ""
                StockEntryTable.SubCodeName = ""
                StockEntryTable.Remarks = ""
                StockEntryTable.Status = "Active"
                StockEntryTable.StructureCode = ""
                StockEntryTable.CustomFields = ""
                StockEntryTable.PartyDocNo = ""
                StockEntryTable.PartyDocDate = ""
                StockEntryTable.EntryBy = AgL.PubUserName
                StockEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                StockEntryTable.ApproveBy = ""
                StockEntryTable.ApproveDate = ""
                StockEntryTable.MoveToLog = ""
                StockEntryTable.MoveToLogDate = ""



                Dim DtStockHeadDetail_ForHeader As New DataTable
                For M = 0 To DtStock.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtStock.Columns(M).ColumnName
                    DtStockHeadDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowStockHeadDetail_ForHeader As DataRow() = DtStock.Select("[V_Type] = " + AgL.Chk_Text(AgL.XNull(DtStock.Rows(I)("V_Type"))) + " And [V_No] = " + AgL.Chk_Text(AgL.XNull(DtStock.Rows(I)("V_No"))))
                If DtRowStockHeadDetail_ForHeader.Length > 0 Then
                    For M = 0 To DtRowStockHeadDetail_ForHeader.Length - 1
                        DtStockHeadDetail_ForHeader.Rows.Add()
                        For N = 0 To DtStockHeadDetail_ForHeader.Columns.Count - 1
                            DtStockHeadDetail_ForHeader.Rows(M)(N) = DtRowStockHeadDetail_ForHeader(M)(N)
                        Next
                    Next
                End If

                For J = 0 To DtStockHeadDetail_ForHeader.Rows.Count - 1
                    StockEntryTable.Line_Sr = J + 1
                    StockEntryTable.Line_SubCode = ""
                    StockEntryTable.Line_SubCodeName = ""

                    Dim DtRowItem As DataRow() = DtItem.Select("Specification = " + AgL.Chk_Text(AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Name"))) +
                                " And ItemGroupDesc = " + AgL.Chk_Text(AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Group"))) +
                                " And ItemCategoryDesc = " + AgL.Chk_Text(AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Category"))))

                    If DtRowItem.Length > 0 Then
                        StockEntryTable.Line_ItemCode = DtRowItem(0)("Code")
                    Else
                        DtRowItem = DtItem.Select("ItemDesc = " + AgL.Chk_Text(AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Name")) + Space(10) +
                            "[" + AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Group")) +
                            " | " + AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Category")) + "]"))
                        If DtRowItem.Length > 0 Then
                            StockEntryTable.Line_ItemCode = DtRowItem(0)("Code")
                        Else
                            Dim ItemTable As New FrmItemMaster.StructItem
                            Dim bItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                            ItemTable.Code = bItemCode
                            ItemTable.ManualCode = bItemCode
                            ItemTable.DisplayName = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Name")) + Space(10) +
                                "[" + AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Group")) +
                                " | " + AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Category")) + "]"
                            ItemTable.Specification = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Name"))
                            ItemTable.ItemGroupDesc = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Group"))
                            ItemTable.ItemCategoryDesc = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Category"))
                            ItemTable.Description = ItemTable.DisplayName
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
                            ItemTable.Status = "InActive"
                            ItemTable.StockYN = 0
                            ItemTable.IsSystemDefine = 0
                            Dim DTUP As DataTable = AgL.FillData("Select '' As [UP] ", AgL.GcnRead).Tables(0)
                            Dim FrmObj As New FrmItemMaster("", DTUP, ItemV_Type.Item)
                            FrmObj.ImportItemTable(ItemTable)
                            StockEntryTable.Line_ItemCode = ItemTable.Code
                        End If
                    End If

                    'StockEntryTable.Line_ItemCode = ""
                    'StockEntryTable.Line_ItemName = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item Name"))
                    'StockEntryTable.Line_Specification = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Specification"))
                    StockEntryTable.Line_LotNo = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Lot No"))
                    StockEntryTable.Line_BaleNo = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Bale No"))
                    'StockEntryTable.Line_ItemState = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Item State"))
                    StockEntryTable.Line_DocQty = AgL.VNull(DtStockHeadDetail_ForHeader.Rows(J)("Qty"))
                    StockEntryTable.Line_Qty = AgL.VNull(DtStockHeadDetail_ForHeader.Rows(J)("Qty"))
                    StockEntryTable.Line_Pcs = 0
                    StockEntryTable.Line_Unit = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Unit"))
                    If DtStockHeadDetail_ForHeader.Columns.Contains("Rate") Then
                        StockEntryTable.Line_Rate = AgL.VNull(DtStockHeadDetail_ForHeader.Rows(J)("Rate"))
                    End If
                    If DtStockHeadDetail_ForHeader.Columns.Contains("Amount") Then
                        StockEntryTable.Line_Amount = AgL.VNull(DtStockHeadDetail_ForHeader.Rows(J)("Amount"))
                    End If
                    StockEntryTable.Line_Remarks = AgL.XNull(DtStockHeadDetail_ForHeader.Rows(J)("Remark"))


                    StockEntryTableList(UBound(StockEntryTableList)) = StockEntryTable
                    ReDim Preserve StockEntryTableList(UBound(StockEntryTableList) + 1)
                Next

                InsertStockHead(StockEntryTableList)
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
    Private Sub Dgl2_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl2.CellBeginEdit
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub
            Dgl2.Item(Col1LastValue, Dgl2.CurrentCell.RowIndex).Tag = Dgl2.Item(Col1Value, Dgl2.CurrentCell.RowIndex).Tag
            Dgl2.Item(Col1LastValue, Dgl2.CurrentCell.RowIndex).Value = Dgl2.Item(Col1Value, Dgl2.CurrentCell.RowIndex).Value
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FOpenStockBalance(mRow As Integer)
        Dim DtTemp As DataTable
        Dim StrRtn As String = ""
        Dim bPendingStockQry As String = ""

        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        bPendingStockQry = " SELECT L.Process, L.Item, 
	                IsNull(Sum(L.Qty_Iss),0) - IsNull(Sum(L.Qty_Rec),0) AS BalanceQty
	                FROM Stock L " &
                    IIf(AgL.XNull(Dgl2.Item(Col1Value, rowGodown).Tag) = "", "", "Where L.Godown = '" & Dgl2.Item(Col1Value, rowGodown).Tag & "'") &
                    " GROUP BY L.Process, L.Item
                    HAVING IsNull(Sum(L.Qty_Iss),0) - IsNull(Sum(L.Qty_Rec),0) > 0 "

        mQry = " SELECT 'o' As Tick, VPendingStock.Item As SearchKey, 
                Prs.Name As Process, Ic.Description As ItemCategory, Ig.Description As ItemGroup, I.Description As Item,
                D1.Description As Dimension1, D2.Description As Dimension2, 
                D3.Description As Dimension3, D4.Description As Dimension4,
                Size.Description As Size, VPendingStock.BalanceQty AS BalanceQty, Sku.Unit,
                Ic.Code As ItemCategoryCode, Ig.Code As ItemGroupCode, I.Code As ItemCode,
                D1.Code As Dimension1Code, D2.Code As Dimension2Code, 
                D3.Code As Dimension3Code, D4.Code As Dimension4Code,
                Size.Code As SizeCode, It.Code As ItemTypeCode, It.Name As ItemType,
                U.ShowDimensionDetailInSales 
                FROM (" & bPendingStockQry & ") AS VPendingStock
                LEFT JOIN SubGroup Prs ON VPendingStock.Process = Prs.SubCode
                LEFT JOIN Item Sku ON Sku.Code = VPendingStock.Item
                LEFT JOIN Item I ON I.Code = Sku.BaseItem
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                LEFT JOIN Item IC On Sku.ItemCategory = IC.Code
                LEFT JOIN Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size 
                Left Join Unit U  With (NoLock) On Sku.Unit = U.Code 
                Where 1=1 "

        If mRow >= 0 Then
            If AgL.XNull(Dgl1.Item(Col1ItemCategory, mRow).Tag) <> "" Then
                mQry += " And Ic.Code = '" & Dgl1.Item(Col1ItemCategory, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1ItemGroup, mRow).Tag) <> "" Then
                mQry += " And Ig.Code = '" & Dgl1.Item(Col1ItemGroup, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Item, mRow).Tag) <> "" Then
                mQry += " And I.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Dimension1, mRow).Tag) <> "" Then
                mQry += " And D1.Code = '" & Dgl1.Item(Col1Dimension1, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Dimension2, mRow).Tag) <> "" Then
                mQry += " And D2.Code = '" & Dgl1.Item(Col1Dimension2, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Dimension3, mRow).Tag) <> "" Then
                mQry += " And D3.Code = '" & Dgl1.Item(Col1Dimension3, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Dimension4, mRow).Tag) <> "" Then
                mQry += " And D4.Code = '" & Dgl1.Item(Col1Dimension4, mRow).Tag & "'"
            End If
            If AgL.XNull(Dgl1.Item(Col1Size, mRow).Tag) <> "" Then
                mQry += " And Size.Code = '" & Dgl1.Item(Col1Size, mRow).Tag & "'"
            End If
        End If

        Dim bFilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If bFilterInclude_ItemType <> "" Then
            If bFilterInclude_ItemType.ToString.Substring(0, 1) = "+" Then
                mQry += " And CharIndex('+' || Sku.ItemType,'" & bFilterInclude_ItemType & "') > 0 "
            ElseIf bFilterInclude_ItemType.ToString.Substring(0, 1) = "-" Then
                mQry += " And CharIndex('-' || Sku.ItemType,'" & bFilterInclude_ItemType & "') <= 0 "
            End If
        End If

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 990, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Process", 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Process] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(3, "Item Category", 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[ItemCategory] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(4, "Item Group", 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[ItemGroup] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(5, "Item", 200, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Item] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(6, AgL.PubCaptionDimension1, 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension1] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(7, AgL.PubCaptionDimension2, 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension2] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(8, AgL.PubCaptionDimension3, 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension3] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(9, AgL.PubCaptionDimension4, 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Dimension4] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(10, "Size", 100, DataGridViewContentAlignment.MiddleLeft, IIf(DtTemp.Select("[Size] <> '' ").Length = 0, False, True))
        FRH_Multiple.FFormatColumn(11, "Bal Qty", 100, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(12, "Unit", 70, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.FFormatColumn(13, , 0, , False)
        FRH_Multiple.FFormatColumn(14, , 0, , False)
        FRH_Multiple.FFormatColumn(15, , 0, , False)
        FRH_Multiple.FFormatColumn(16, , 0, , False)
        FRH_Multiple.FFormatColumn(17, , 0, , False)
        FRH_Multiple.FFormatColumn(18, , 0, , False)
        FRH_Multiple.FFormatColumn(19, , 0, , False)
        FRH_Multiple.FFormatColumn(20, , 0, , False)
        FRH_Multiple.FFormatColumn(21, , 0, , False)
        FRH_Multiple.FFormatColumn(22, , 0, , False)
        FRH_Multiple.FFormatColumn(23, , 0, , False)


        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If

        Dim DrSelected As DataRow()
        If StrRtn <> "" Then
            DrSelected = DtTemp.Select("SearchKey In (" & StrRtn & ")")

            If mRow < 0 Then
                If Dgl1.Rows.Count > 1 Then
                    If MsgBox("Do you want to overwrite existing data in grid ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        'Dgl1.Rows.Clear() : Dgl1.RowCount = 1 : mRow = 0
                        For I As Integer = 0 To Dgl1.Rows.Count - 1
                            If AgL.VNull(Dgl1.Item(Col1IsRecordLocked, I).Value) = 0 Then
                                If Not Dgl1.Rows(I).IsNewRow Then
                                    Dgl1.Rows(I).Visible = False
                                End If
                            End If
                        Next
                        mRow = Dgl1.Rows.Count - 1
                    Else
                        mRow = Dgl1.Rows.Count - 1
                    End If
                Else
                    mRow = 0
                End If
            End If

            If Dgl1.Rows(mRow).IsNewRow = False Then
                Dgl1.Rows.Remove(Dgl1.Rows(mRow))
            End If
            Dgl1.Rows.Insert(mRow, DrSelected.Length)
            For I As Integer = 0 To DrSelected.Length - 1
                Dgl1.Item(Col1ItemType, mRow + I).Tag = AgL.XNull(DrSelected(I)("ItemTypeCode"))
                Dgl1.Item(Col1ItemType, mRow + I).Value = AgL.XNull(DrSelected(I)("ItemType"))

                Dgl1.Item(Col1ItemCategory, mRow + I).Tag = AgL.XNull(DrSelected(I)("ItemCategoryCode"))
                Dgl1.Item(Col1ItemCategory, mRow + I).Value = AgL.XNull(DrSelected(I)("ItemCategory"))

                Dgl1.Item(Col1ItemGroup, mRow + I).Tag = AgL.XNull(DrSelected(I)("ItemGroupCode"))
                Dgl1.Item(Col1ItemGroup, mRow + I).Value = AgL.XNull(DrSelected(I)("ItemGroup"))

                Dgl1.Item(Col1Item, mRow + I).Tag = AgL.XNull(DrSelected(I)("ItemCode"))
                Dgl1.Item(Col1Item, mRow + I).Value = AgL.XNull(DrSelected(I)("Item"))

                Dgl1.Item(Col1Dimension1, mRow + I).Tag = AgL.XNull(DrSelected(I)("Dimension1Code"))
                Dgl1.Item(Col1Dimension1, mRow + I).Value = AgL.XNull(DrSelected(I)("Dimension1"))

                Dgl1.Item(Col1Dimension2, mRow + I).Tag = AgL.XNull(DrSelected(I)("Dimension2Code"))
                Dgl1.Item(Col1Dimension2, mRow + I).Value = AgL.XNull(DrSelected(I)("Dimension2"))

                Dgl1.Item(Col1Dimension3, mRow + I).Tag = AgL.XNull(DrSelected(I)("Dimension3Code"))
                Dgl1.Item(Col1Dimension3, mRow + I).Value = AgL.XNull(DrSelected(I)("Dimension3"))

                Dgl1.Item(Col1Dimension4, mRow + I).Tag = AgL.XNull(DrSelected(I)("Dimension4Code"))
                Dgl1.Item(Col1Dimension4, mRow + I).Value = AgL.XNull(DrSelected(I)("Dimension4"))

                Dgl1.Item(Col1Size, mRow + I).Tag = AgL.XNull(DrSelected(I)("SizeCode"))
                Dgl1.Item(Col1Size, mRow + I).Value = AgL.XNull(DrSelected(I)("Size"))

                Dgl1.Item(Col1Qty, mRow + I).Value = AgL.VNull(DrSelected(I)("BalanceQty"))
                Dgl1.Item(Col1DocQty, mRow + I).Value = AgL.VNull(DrSelected(I)("BalanceQty"))
                Dgl1.Item(Col1Unit, mRow + I).Value = AgL.XNull(DrSelected(I)("Unit"))
                Dgl1.Item(Col1Unit, mRow + I).Tag = AgL.VNull(DrSelected(I)("ShowDimensionDetailInSales"))

                If (Dgl1.Item(Col1Unit, mRow + I).Tag) Then
                    Dgl1.Item(Col1DocQty, mRow + I).Style.ForeColor = Color.Blue
                    ShowStockEntryDimensionDetail(mRow + I, False)
                    CType(Dgl1.Item(Col1DocQty, mRow + I).Tag, FrmStockEntryDimension).Dgl1.Item(FrmStockEntryDimension.Col1Qty, 0).Value = Dgl1.Item(Col1Qty, mRow + I).Value
                    CType(Dgl1.Item(Col1DocQty, mRow + I).Tag, FrmStockEntryDimension).Dgl1.Item(FrmStockEntryDimension.Col1TotalQty, 0).Value = Dgl1.Item(Col1Qty, mRow + I).Value
                    CType(Dgl1.Item(Col1DocQty, mRow + I).Tag, FrmStockEntryDimension).Calculation()
                End If
            Next

            For I As Integer = 0 To Dgl1.Rows.Count - 1
                Dgl1.Item(ColSNo, I).Value = I + 1
                FGeterateSkuName(I)
            Next
        End If
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
                    If Dgl2.Visible Then
                        Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowRemarks)
                        Dgl2.Focus()
                    End If
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
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1BtnBaseDetail
                    ShowBaseDetail(bRowIndex)
                Case Col1ReferenceDocId
                    ClsMain.FOpenForm(Dgl1.Item(Col1ReferenceDocId, bRowIndex).Tag, Me)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl1_CellContentClick function")
        End Try
    End Sub
    Private Sub ShowBaseDetail(mRow As Integer)
        If Dgl1.Item(Col1BtnBaseDetail, mRow).Tag IsNot Nothing Then
            Dgl1.Item(Col1BtnBaseDetail, mRow).Tag.StartPosition = FormStartPosition.CenterParent
            Dgl1.Item(Col1BtnBaseDetail, mRow).Tag.ShowDialog()
        Else
            Dim FrmObj As FrmStockHeadDetailBase
            FrmObj = New FrmStockHeadDetailBase
            FrmObj.SearchCode = mSearchCode
            FrmObj.Sr = Dgl1.Item(ColSNo, mRow).Tag
            FrmObj.EntryNCat = LblV_Type.Tag
            FrmObj.LblDocNo.Text = "Entry No." & DglMain.Item(Col1Value, rowReferenceNo).Value
            Dgl1.Item(Col1BtnBaseDetail, mRow).Tag = FrmObj
            Dgl1.Item(Col1BtnBaseDetail, mRow).Tag.StartPosition = FormStartPosition.CenterParent
            Dgl1.Item(Col1BtnBaseDetail, mRow).Tag.ShowDialog()
        End If
    End Sub
    Private Sub FPostConsumption(mDocId As String, mSr As Integer, mRow As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        mQry = " Select RawMaterial 
                From PurchInvoiceDetail With (NoLock)
                Where DocId = '" & Dgl1.Item(Col1ReferenceDocId, mRow).Tag & "'
                And Sr = " & Dgl1.Item(Col1ReferenceDocIdTSr, mRow).Value & ""
        Dim bRawMaterial As String = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

        Dim bBomItem As String = ClsMain.FGetBomWithBomPattern("", "",
                    "", "", "", "", "", "", "", bRawMaterial, Dgl1.Item(Col1SKU, mRow).Tag, "")

        mQry = " INSERT INTO StockProcess(DocId, TSr, Sr, Item, Qty_Iss, Qty_Rec, Unit)
                    Select " & AgL.Chk_Text(mDocId) & " As DocId, 
                    " & Val(mSr) & " As TSr, L.Sr, L.Item, 
                    " & Val(Dgl1.Item(Col1Qty, mRow).Value) & " * L.Qty, 0 As Qty_Rec, I.Unit
                    From (Select * From BomDetail Where Code = '" & bBomItem & "') As L 
                    LEFT JOIN Item I On L.Item = I.Code "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FPostConsumption_Garment(mDocId As String, mSr As Integer, mRow As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bBomItemForCloth As String = AgL.Dman_Execute(" Select * From Item With (NoLock) Where V_Type = '" & ItemV_Type.BOM & "'
                And ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRow).Tag & "'
                And RawMaterial = '" & Dgl1.Item(Col1RawMaterial, mRow).Tag & "'
                And Size = '" & Dgl1.Item(Col1Size, mRow).Tag & "' ", AgL.GcnRead).ExecuteScalar()

        If DglMain.Item(Col1Value, rowProcess).Tag = ClsGarmentProduction.Process_Cutting Or DglMain.Item(Col1Value, rowProcess).Tag = ClsGarmentProduction.Process_CuttingAndStitching Then
            If bBomItemForCloth = "" Then
                Err.Raise(1, "", "Consumption not found for line no." & mRow + 1)
            End If
        End If

        Dim bBomItem As String = AgL.Dman_Execute(" Select * From Item With (NoLock) Where V_Type = '" & ItemV_Type.BOM & "'
                And ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRow).Tag & "'
                And RawMaterial Is Null
                And Size Is Null
                And Dimension3 Is Null ", AgL.GcnRead).ExecuteScalar()

        Dim bBomItemException As String = AgL.Dman_Execute(" Select * From Item With (NoLock) Where V_Type = '" & ItemV_Type.BOM & "'
                And ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRow).Tag & "'
                And Dimension3 = '" & Dgl1.Item(Col1Dimension3, mRow).Tag & "'
                And Size Is Null ", AgL.GcnRead).ExecuteScalar()

        Dim bProcessCondStr$ = ""
        If DglMain.Item(Col1Value, rowProcess).Tag = ClsGarmentProduction.Process_CuttingAndStitching Then
            bProcessCondStr = " And Process In ('" & ClsGarmentProduction.Process_Cutting & "','" & ClsGarmentProduction.Process_Stitching & "')"
        Else
            bProcessCondStr = " And Process = '" & DglMain.Item(Col1Value, rowProcess).Tag & "'"
        End If


        mQry = " Select " & AgL.Chk_Text(mDocId) & " As DocId, 
                    " & Val(mSr) & " As TSr, L.Sr, '' As Sku, 
                    Ic.ItemType, L.Item As ItemCategory, Ic.Description As ItemCategoryDesc, 
                    '" & Dgl1.Item(Col1Dimension1, mRow).Tag & "' As Dimension1, 
                    '" & Dgl1.Item(Col1Dimension1, mRow).Value & "' As Dimension1Desc, 
                    '" & Dgl1.Item(Col1Dimension2, mRow).Tag & "' As Dimension2, 
                    '" & Dgl1.Item(Col1Dimension2, mRow).Value & "' As Dimension2Desc, 
                    '" & Dgl1.Item(Col1Dimension3, mRow).Tag & "' As Dimension3, 
                    '" & Dgl1.Item(Col1Dimension3, mRow).Value & "' As Dimension3Desc, 
                    '" & Dgl1.Item(Col1RawMaterial, mRow).Tag & "' As Dimension4, 
                    '" & Dgl1.Item(Col1RawMaterial, mRow).Value & "' As Dimension4Desc, 
                    " & Val(Dgl1.Item(Col1Qty, mRow).Value) & " * L.Qty As Qty, Ic.Unit
                    From (Select * From BomDetail With (NoLock) Where Code = '" & bBomItemForCloth & "'" & bProcessCondStr & ") As L 
                    LEFT JOIN Item Ic On L.Item = Ic.Code "

        mQry += " UNION ALL "

        mQry += " Select " & AgL.Chk_Text(mDocId) & " As DocId, 
                    " & Val(mSr) & " As TSr, L.Sr, L.Item As Sku, 
                    Ic.ItemType, '' As ItemCategory, '' As ItemCategoryDesc, 
                    '' As Dimension1, 
                    '' As Dimension1Desc, 
                    '' As Dimension2, 
                    '' As Dimension2Desc, 
                    '' As Dimension3, 
                    '' As Dimension3Desc, 
                    '' As Dimension4, 
                    '' As Dimension4Desc, 
                    " & Val(Dgl1.Item(Col1Qty, mRow).Value) & " * L.Qty As Qty, Ic.Unit
                    From (Select * From BomDetail With (NoLock) Where Code In ('" & bBomItem & "','" & bBomItemException & "') " & bProcessCondStr & ") As L 
                    LEFT JOIN Item Ic On L.Item = Ic.Code "
        Dim DtBomDetail As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)



        For I As Integer = 0 To DtBomDetail.Rows.Count - 1
            If AgL.XNull(DtBomDetail.Rows(I)("Sku")) = "" Then
                DtBomDetail.Rows(I)("Sku") = ClsMain.FGetSKUCode(Dgl1.Item(ColSNo, mRow).Value, AgL.XNull(DtBomDetail.Rows(0)("ItemType")) _
                        , AgL.XNull(DtBomDetail.Rows(I)("ItemCategory")), AgL.XNull(DtBomDetail.Rows(I)("ItemCategoryDesc")) _
                        , "", "", "", "" _
                        , AgL.XNull(DtBomDetail.Rows(I)("Dimension1")), AgL.XNull(DtBomDetail.Rows(I)("Dimension1Desc")) _
                        , AgL.XNull(DtBomDetail.Rows(I)("Dimension2")), AgL.XNull(DtBomDetail.Rows(I)("Dimension2Desc")) _
                        , AgL.XNull(DtBomDetail.Rows(I)("Dimension3")), AgL.XNull(DtBomDetail.Rows(I)("Dimension3Desc")) _
                        , AgL.XNull(DtBomDetail.Rows(I)("Dimension4")), AgL.XNull(DtBomDetail.Rows(I)("Dimension4Desc")) _
                        , "", "", "", "", "", "", "", "", "", "")
            End If

            If AgL.VNull(AgL.Dman_Execute(" Select ConsiderInIssueYN
                    From PurchInvoiceDetailBom L 
                    Where L.DocId = '" & Dgl1.Item(Col1ReferenceDocId, mRow).Tag & "'
                    And L.Item = '" & AgL.XNull(DtBomDetail.Rows(I)("Sku")) & "'", AgL.GcnRead).ExecuteScalar()) <> 0 Then
                mQry = " INSERT INTO StockHeadDetailBom(DocId, TSr, Sr, Item, Qty, Unit)
                    Select " & AgL.Chk_Text(mDocId) & " As DocId, 
                    " & Val(mSr) & " As TSr, " & Val(I + 1) & " As Sr, 
                    '" & AgL.XNull(DtBomDetail.Rows(I)("Sku")) & "' As Item, 
                    " & AgL.VNull(DtBomDetail.Rows(I)("Qty")) & " As Qty, 
                    '" & AgL.XNull(DtBomDetail.Rows(I)("Unit")) & "' As Unit "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next

        mQry = " INSERT INTO StockHeadDetailBomSku(DocId, TSr, Sr, ItemCategory, ItemGroup, 
                    Item, Dimension1, Dimension2, Dimension3, Dimension4, Size)
                    Select L.DocId, L.TSr, L.Sr, I.ItemCategory, I.ItemGroup, 
                    Case When I.V_Type <> '" & ItemV_Type.SKU & "' Then I.Code Else Null End As Item, 
                    I.Dimension1, I.Dimension2, I.Dimension3, I.Dimension4, I.Size
                    From StockHeadDetailBom L 
                    LEFT JOIN Item I ON L.Item = I.Code 
                    Where L.DocId = '" & mDocId & "' ANd L.TSr = " & Val(mSr) & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Dim mMaxSr As Integer = AgL.VNull(AgL.Dman_Execute("Select Max(L.Sr) As MaxSr
                        From StockProcess L With (NoLock)
                        Where L.DocId = '" & mSearchCode & "'
                        And L.TSr = " & Val(mSr) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

        mQry = " INSERT INTO StockProcess(DocId, TSr, Sr, 
                    V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, SubCode,
                    Item, Qty_Iss, Qty_Rec, Unit,
                    UnitMultiplier, DealQty_Iss, DealQty_Rec, 
                    Rate, Amount, Process, RecId)
                    Select L.DocId, L.TSr, L.Sr + " & Val(mMaxSr) & ", 
                    H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.SubCode,
                    L.Item, L.Qty As Qty_Iss, 
                    0 As Qty_Rec, L.Unit,
                    0 As UnitMultiplier, 0 As DealQty_Iss, 0 As DealQty_Rec, 
                    0 As Rate, 0 As Amount, H.Process, H.ManualRefNo As RecId
                    From StockHeadDetailBom L 
                    LEFT JOIN StockHead H On L.DocId = H.DocId
                    Where L.DocId = '" & mDocId & "'
                    And L.TSr = " & Val(mSr) & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Function FGetStock(mRowIndex As Integer) As Double
        Dim bCondStr As String = ""

        If AgL.XNull(Dgl1.Item(Col1ItemCategory, mRowIndex).Tag) <> "" Then
            bCondStr += " And I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRowIndex).Tag & "'"
        End If
        If AgL.XNull(Dgl1.Item(Col1ItemGroup, mRowIndex).Tag) <> "" Then
            bCondStr += " And I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, mRowIndex).Tag & "'"
        End If
        If AgL.XNull(Dgl1.Item(Col1Item, mRowIndex).Tag) <> "" Then
            bCondStr += " And I.BaseItem = '" & Dgl1.Item(Col1Item, mRowIndex).Tag & "'"
        End If
        If AgL.XNull(Dgl1.Item(Col1Dimension1, mRowIndex).Tag) <> "" Then
            bCondStr += " And I.Dimension1 = '" & Dgl1.Item(Col1Dimension1, mRowIndex).Tag & "'"
        End If
        If AgL.XNull(Dgl1.Item(Col1Dimension2, mRowIndex).Tag) <> "" Then
            bCondStr += " And I.Dimension2 = '" & Dgl1.Item(Col1Dimension2, mRowIndex).Tag & "'"
        End If
        If AgL.XNull(Dgl1.Item(Col1Dimension3, mRowIndex).Tag) <> "" Then
            bCondStr += " And I.Dimension3 = '" & Dgl1.Item(Col1Dimension3, mRowIndex).Tag & "'"
        End If
        If AgL.XNull(Dgl1.Item(Col1Dimension4, mRowIndex).Tag) <> "" Then
            bCondStr += " And I.Dimension4 = '" & Dgl1.Item(Col1Dimension4, mRowIndex).Tag & "'"
        End If
        If AgL.XNull(Dgl1.Item(Col1Size, mRowIndex).Tag) <> "" Then
            bCondStr += " And I.Size = '" & Dgl1.Item(Col1Size, mRowIndex).Tag & "'"
        End If

        If bCondStr <> "" Then
            mQry = "SELECT IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) AS Qty
                FROM Stock L 
                LEFT JOIN Item I ON L.Item = I.Code
                WHERE L.Godown = '" & Dgl2.Item(Col1Value, rowGodown).Tag & "'" & bCondStr
            FGetStock = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
        Else
            FGetStock = 0
        End If
    End Function
    Private Function FGetStockProcess(SkuCode As String, SubCode As String) As Double
        mQry = "SELECT IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) AS Qty
                FROM StockProcess L 
                LEFT JOIN Item I ON L.Item = I.Code
                WHERE L.SubCode = '" & SubCode & "'
                And L.Item = '" & SkuCode & "' "
        FGetStockProcess = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
    End Function
    Private Sub FFillStandardConsumption()
        Dim I As Integer = 0
        Dim DsTemp As DataSet

        mQry = "SELECT Max(I.Description) As ItemDesc, 
                        Max(U.DecimalPlaces) AS DecimalPlaces, Max(U.DecimalPlaces) As QtyDecimalPlaces, 
                        Max(Sku.Code) As SkuCode, Max(Sku.Description) As SkuDescription, 
                        Max(It.Code) As ItemType, Max(It.Name) As ItemTypeDesc,
                        Max(IG.Description) As ItemGroupDesc, Max(IC.Description) As ItemCategoryDesc, 
                        Pids.Item As ItemCode, Pids.ItemCategory, Pids.ItemGroup, 
                        Pids.Dimension1, Pids.Dimension2, 
                        Pids.Dimension3, Pids.Dimension4, Pids.Size, 
                        Max(D1.Description) as Dimension1Desc, Max(D2.Description) as Dimension2Desc,
                        Max(D3.Description) as Dimension3Desc, Max(D4.Description) as Dimension4Desc, Max(Size.Description) as SizeDesc,
                        Sum(L.Qty) AS Qty, Max(L.Unit) As Unit
                        FROM (Select * From PurchInvoiceDetailBom Where DocId = '" & Dgl2(Col1Value, rowReferenceDocId).Tag & "') As L 
                        LEFT JOIN PurchInvoiceDetailBomSku Pids ON L.DocID = Pids.DocID AND L.TSr = Pids.TSr AND L.Sr = Pids.Sr
                        LEFT JOIN Item Sku ON Sku.Code = L.Item
                        LEFT JOIN ItemType It On Sku.ItemType = It.Code
                        Left Join Item IC On Pids.ItemCategory = IC.Code
                        Left Join Item IG On Pids.ItemGroup = IG.Code
                        LEFT JOIN Item I ON Pids.Item = I.Code
                        LEFT JOIN Item D1 ON Pids.Dimension1 = D1.Code
                        LEFT JOIN Item D2 ON Pids.Dimension2 = D2.Code
                        LEFT JOIN Item D3 ON Pids.Dimension3 = D3.Code
                        LEFT JOIN Item D4 ON Pids.Dimension4 = D4.Code
                        LEFT JOIN Item Size ON Pids.Size = Size.Code
                        Left Join Unit U With (NoLock) On L.Unit = U.Code 
                        GROUP BY Pids.ItemCategory, Pids.ItemGroup, Pids.Item, Pids.Dimension1, Pids.Dimension2, Pids.Dimension3, Pids.Dimension4, Pids.Size  "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count

                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))

                    Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                    Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                    Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))

                    Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))

                    Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                Next I
            End If
        End With
        Calculation()
    End Sub
    Private Sub FFillStandardConsumption_Garment()
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim DsTemp As DataSet
        Dim DtLine As DataTable

        mQry = "SELECT Max(I.Description) As ItemDesc, 
                Max(U.DecimalPlaces) AS DecimalPlaces, Max(U.DecimalPlaces) As QtyDecimalPlaces, 
                Max(Sku.Code) As SkuCode, Max(Sku.Description) As SkuDescription, 
                Max(It.Code) As ItemType, Max(It.Name) As ItemTypeDesc,
                Max(IG.Description) As ItemGroupDesc, Max(IC.Description) As ItemCategoryDesc, 
                Pids.Item As ItemCode, Pids.ItemCategory, Pids.ItemGroup, 
                Pids.Dimension1, Max(Pids.Dimension2) AS Dimension2, 
                Pids.Dimension3, Pids.Dimension4, Pids.Size, 
                Max(D1.Description) as Dimension1Desc, Max(D2.Description) as Dimension2Desc,
                Max(D3.Description) as Dimension3Desc, Max(D4.Description) as Dimension4Desc, Max(Size.Description) as SizeDesc,
                Sum(L.Qty) AS Qty, Max(L.Unit) As Unit, Max(Cast(U.ShowDimensionDetailInSales As BIGINT)) As ShowDimensionDetailInSales 
                FROM (Select * From PurchInvoiceDetailBom Where DocId = '" & Dgl2.Item(Col1Value, rowReferenceDocId).Tag & "' And IsNull(ConsiderInIssueYN,1) <> 0) As L 
                LEFT JOIN PurchInvoiceDetailBomSku Pids ON L.DocID = Pids.DocID AND L.TSr = Pids.TSr AND L.Sr = Pids.Sr
                LEFT JOIN Item Sku ON Sku.Code = L.Item
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                Left Join Item IC On Pids.ItemCategory = IC.Code
                Left Join Item IG On Pids.ItemGroup = IG.Code
                LEFT JOIN Item I ON Pids.Item = I.Code
                LEFT JOIN Item D1 ON Pids.Dimension1 = D1.Code
                LEFT JOIN Item D2 ON Pids.Dimension2 = D2.Code
                LEFT JOIN Item D3 ON Pids.Dimension3 = D3.Code
                LEFT JOIN Item D4 ON Pids.Dimension4 = D4.Code
                LEFT JOIN Item Size ON Pids.Size = Size.Code
                Left Join Unit U With (NoLock) On L.Unit = U.Code 
                GROUP BY Pids.ItemCategory, Pids.ItemGroup, Pids.Item, Pids.Dimension1, Pids.Dimension3, Pids.Dimension4, Pids.Size"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        Dim mRow As Integer = 0

        If Dgl1.Rows.Count > 1 Then
            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1IsRecordLocked, I).Value = 0 Then
                    If Not Dgl1.Rows(I).IsNewRow Then
                        Dgl1.Rows(I).Visible = False
                    End If
                End If
            Next
            mRow = Dgl1.Rows.Count - 1
        End If


        With DsTemp.Tables(0)

            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, mRow + I).Value = I + 1

                    Dgl1.Item(Col1ItemCategory, mRow + I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, mRow + I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))

                    Dgl1.Item(Col1Item, mRow + I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                    Dgl1.Item(Col1Item, mRow + I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                    Dgl1.Item(Col1Dimension1, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                    Dgl1.Item(Col1Dimension1, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                    Dgl1.Item(Col1Dimension3, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))

                    Dgl1.Item(Col1Dimension4, mRow + I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, mRow + I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))

                    Dgl1.Item(Col1DocQty, mRow + I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl1.Item(Col1Qty, mRow + I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl1.Item(Col1Unit, mRow + I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Unit, mRow + I).Tag = AgL.VNull(.Rows(I)("ShowDimensionDetailInSales"))

                    Dgl1.Item(Col1QtyDecimalPlaces, mRow + I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                    If AgL.XNull(.Rows(I)("Dimension4")) <> "" Then
                        mQry = "SELECT Max(I.Description) As ItemDesc, 
                            Max(U.DecimalPlaces) AS DecimalPlaces, Max(U.DecimalPlaces) As QtyDecimalPlaces, 
                            Max(Sku.Code) As SkuCode, Max(Sku.Description) As SkuDescription, 
                            Max(It.Code) As ItemType, Max(It.Name) As ItemTypeDesc,
                            Max(IG.Description) As ItemGroupDesc, Max(IC.Description) As ItemCategoryDesc, 
                            Pids.Item As ItemCode, Pids.ItemCategory, Pids.ItemGroup, 
                            Pids.Dimension1, Pids.Dimension2 AS Dimension2, 
                            Pids.Dimension3, Pids.Dimension4, Pids.Size, 
                            Max(D1.Description) as Dimension1Desc, Max(D2.Description) as Dimension2Desc,
                            Max(D3.Description) as Dimension3Desc, Max(D4.Description) as Dimension4Desc, Max(Size.Description) as SizeDesc,
                            Sum(L.Qty) AS Qty, Max(L.Unit) As Unit
                            FROM (Select * From PurchInvoiceDetailBom Where DocId = '" & Dgl2.Item(Col1Value, rowReferenceDocId).Tag & "' And IsNull(ConsiderInIssueYN,1) <> 0) As L 
                            LEFT JOIN PurchInvoiceDetailBomSku Pids ON L.DocID = Pids.DocID AND L.TSr = Pids.TSr AND L.Sr = Pids.Sr
                            LEFT JOIN Item Sku ON Sku.Code = L.Item
                            LEFT JOIN ItemType It On Sku.ItemType = It.Code
                            Left Join Item IC On Pids.ItemCategory = IC.Code
                            Left Join Item IG On Pids.ItemGroup = IG.Code
                            LEFT JOIN Item I ON Pids.Item = I.Code
                            LEFT JOIN Item D1 ON Pids.Dimension1 = D1.Code
                            LEFT JOIN Item D2 ON Pids.Dimension2 = D2.Code
                            LEFT JOIN Item D3 ON Pids.Dimension3 = D3.Code
                            LEFT JOIN Item D4 ON Pids.Dimension4 = D4.Code
                            LEFT JOIN Item Size ON Pids.Size = Size.Code
                            Left Join Unit U With (NoLock) On L.Unit = U.Code 
                            Where IsNull(Pids.ItemCategory,'') = '" & Dgl1.Item(Col1ItemCategory, mRow + I).Tag & "'
                            And IsNull(Pids.ItemGroup,'') = '" & Dgl1.Item(Col1ItemGroup, mRow + I).Tag & "'
                            And IsNull(Pids.Dimension1,'') = '" & Dgl1.Item(Col1Dimension1, mRow + I).Tag & "'
                            And IsNull(Pids.Dimension3,'') = '" & Dgl1.Item(Col1Dimension3, mRow + I).Tag & "'
                            And IsNull(Pids.Dimension4,'') = '" & Dgl1.Item(Col1Dimension4, mRow + I).Tag & "'
                            GROUP BY Pids.ItemCategory, Pids.ItemGroup, Pids.Item, Pids.Dimension1, Pids.Dimension2, Pids.Dimension3, Pids.Dimension4, Pids.Size"
                        DtLine = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        If DtLine.Rows.Count > 0 Then
                            Dim FrmObj As FrmStockEntryDimension
                            FrmObj = New FrmStockEntryDimension
                            FrmObj.ItemName = Dgl1.Item(Col1Item, mRow + I).Value
                            FrmObj.Unit = Dgl1.Item(Col1Unit, mRow + I).Value
                            FrmObj.UnitDecimalPlace = Val(Dgl1.Item(Col1QtyDecimalPlaces, mRow + I).Value)
                            FrmObj.DglRow = Dgl1.Rows(mRow + I)
                            FrmObj.EntryMode = Topctrl1.Mode
                            FrmObj.objFrmStockEntry = Me
                            FrmObj.IniGrid(mSearchCode, Val(Dgl1.Item(ColSNo, mRow + I).Tag))
                            FrmObj.FReInitializeDimensionColumns()
                            Dgl1.Item(Col1DocQty, mRow + I).Tag = FrmObj
                            Dgl1.Item(Col1DocQty, mRow + I).ReadOnly = True
                            Dgl1.Item(Col1DocQty, mRow + I).Style.ForeColor = Color.Blue

                            For J = 0 To DtLine.Rows.Count - 1
                                FrmObj.Dgl1.Rows.Add()
                                FrmObj.Dgl1.Item(FrmStockEntryDimension.ColSNo, J).Value = FrmObj.Dgl1.Rows.Count - 1
                                FrmObj.Dgl1.Item(FrmStockEntryDimension.Col1Dimension2, J).Tag = AgL.XNull(DtLine.Rows(J)("Dimension2"))
                                FrmObj.Dgl1.Item(FrmStockEntryDimension.Col1Dimension2, J).Value = AgL.XNull(DtLine.Rows(J)("Dimension2Desc"))
                                FrmObj.Dgl1.Item(FrmStockEntryDimension.Col1Pcs, J).Value = 1
                                FrmObj.Dgl1.Item(FrmStockEntryDimension.Col1Qty, J).Value = AgL.VNull(DtLine.Rows(J)("Qty"))
                                FrmObj.Dgl1.Item(FrmStockEntryDimension.Col1TotalQty, J).Value = AgL.VNull(DtLine.Rows(J)("Qty"))
                                FrmObj.Calculation()
                            Next
                        End If
                    End If
                    FGeterateSkuName(mRow + I)
                    Dgl1.Item(Col1Rate, mRow + I).Value = FGetLastPurchaseRate(mRow + I)
                Next I
            End If
        End With
        Calculation()
    End Sub
    Private Function FGetFabricConsumption_Garment(mRow As Integer) As Double
        Dim bBomItem As String = ClsMain.FGetBomWithBomPattern("", "",
                "", "", "", "", "", "", "",
                Dgl1.Item(Col1RawMaterial, mRow).Tag, Dgl1.Item(Col1SKU, mRow).Tag, "")

        mQry = " Select Qty From BomDetail With (NoLock) Where Code = '" & bBomItem & "'"
        Dim DtBomDetail As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtBomDetail.Rows.Count > 0 Then
            FGetFabricConsumption_Garment = AgL.VNull(DtBomDetail.Rows(0)("Qty"))
        Else
            FGetFabricConsumption_Garment = 0
        End If
    End Function
    Private Sub ShowStockHeadParty(DocID As String, PartyCode As String, AcGroupNature As String, Optional ShowDialogForCash As Boolean = False)
        If DglMain.Item(Col1BtnDetail, rowParty).Tag IsNot Nothing Then
            CType(DglMain.Item(Col1BtnDetail, rowParty).Tag, FrmStockEntryParty).EntryMode = Topctrl1.Mode
            DglMain.Item(Col1BtnDetail, rowParty).Tag.ShowDialog()
        Else
            Dim FrmObj As FrmStockEntryParty
            FrmObj = New FrmStockEntryParty
            FrmObj.IniGrid(DocID, PartyCode, AcGroupNature)
            FrmObj.EntryMode = Topctrl1.Mode
            DglMain.Item(Col1BtnDetail, rowParty).Tag = FrmObj
            If AcGroupNature.ToUpper = "CASH" And ShowDialogForCash Then
                DglMain.Item(Col1BtnDetail, rowParty).Tag.ShowDialog()
            End If
        End If
    End Sub
    Private Sub FrmPurchInvoiceDirect_BaseEvent_DglMainContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainContentClick
        Try
            Select Case DglMain.Columns(e.ColumnIndex).Name
                Case Col1BtnDetail
                    If Topctrl1.Mode = "Add" Then
                        ShowStockHeadParty("", DglMain.Item(Col1Value, rowParty).Tag, TxtNature.Text)
                    Else
                        ShowStockHeadParty(mSearchCode, "", TxtNature.Text)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetLastPurchaseRate(bRowIndex As Integer) As Double
        Dim bItemCode As String = ""
        If AgL.XNull(Dgl1.Item(Col1SKU, bRowIndex).Tag) <> "" Then
            bItemCode = AgL.XNull(Dgl1.Item(Col1SKU, bRowIndex).Tag)
        Else
            bItemCode = AgL.XNull(Dgl1.Item(Col1Item, bRowIndex).Tag)
        End If
        mQry = "SELECT VAllPurchaseRate.Rate
                FROM (
	                SELECT L.Item, Max(H.V_Date) AS LastPurchaseDate
	                FROM PurchInvoice H 
	                LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
	                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                WHERE Vt.NCat = '" & Ncat.PurchaseInvoice & "'
                    And L.Item = '" & bItemCode & "'
	                GROUP BY L.Item
                ) AS VLastPurchase
                LEFT JOIN (
	                SELECT L.Item, H.V_Date AS PurchaseDate, L.Rate 
	                FROM PurchInvoice H 
	                LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
	                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                WHERE Vt.NCat = '" & Ncat.PurchaseInvoice & "'
                    And L.Item = '" & bItemCode & "'
                ) AS VAllPurchaseRate ON VLastPurchase.Item = VAllPurchaseRate.Item 
			                AND VLastPurchase.LastPurchaseDate = VAllPurchaseRate.PurchaseDate "
        FGetLastPurchaseRate = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
    End Function
    Private Function FGetLastSaleRate(bRowIndex As Integer) As Double
        Dim bItemCode As String = ""
        If AgL.XNull(Dgl1.Item(Col1SKU, bRowIndex).Tag) <> "" Then
            bItemCode = AgL.XNull(Dgl1.Item(Col1SKU, bRowIndex).Tag)
        Else
            bItemCode = AgL.XNull(Dgl1.Item(Col1Item, bRowIndex).Tag)
        End If
        mQry = "SELECT VAllSaleRate.Rate
                FROM (
	                SELECT L.Item, Max(H.V_Date) AS LastSaleDate
	                FROM SaleInvoice H 
	                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
	                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                WHERE Vt.NCat = '" & Ncat.SaleInvoice & "'
                    And L.Item = '" & bItemCode & "'
	                GROUP BY L.Item
                ) AS VLastSale
                LEFT JOIN (
	                SELECT L.Item, H.V_Date AS SaleDate, L.Rate 
	                FROM SaleInvoice H 
	                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
	                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                WHERE Vt.NCat = '" & Ncat.SaleInvoice & "'
                    And L.Item = '" & bItemCode & "'
                ) AS VAllSaleRate ON VLastSale.Item = VAllSaleRate.Item 
			                AND VLastSale.LastSaleDate = VAllSaleRate.SaleDate "
        FGetLastSaleRate = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
    End Function
    Private Sub FOpenPenginPurchaseOrderOption()
        mQry = "Select 'Show Order Balance' As Code, 'Show Order Balance' As Name 
                UNION ALL 
                Select 'Show Selected Order Balance' As Code, 'Show Selected Order Balance' As Name "
        Dim DtOptions As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(DtOptions), "", 350, 300, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Option", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bChoosenOption As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            bChoosenOption = FRH_Single.DRReturn(1)
        End If

        If bChoosenOption = "Show Order Balance" Then
            FOpenPurchOrderForPurchReceive(-1)
        ElseIf bChoosenOption = "Show Selected Order Balance" Then
            FOpenSelectedPurchOrderBalanceForPurchReceive()
        End If
    End Sub
    Private Sub FOpenSelectedPurchOrderBalanceForPurchReceive(Optional FrmObj As FrmStockEntryOrderSelection = Nothing)
        If FrmObj Is Nothing Then
            FrmObj = New FrmStockEntryOrderSelection
            FrmObj.PartyCode = DglMain.Item(Col1Value, rowParty).Tag
            FrmObj.V_Type = LblV_Type.Tag
            FrmObj.PartyCode = DglMain.Item(Col1Value, rowParty).Tag
            FrmObj.ProcessCode = DglMain.Item(Col1Value, rowProcess).Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.IniGrid(mSearchCode)
            Dgl2.Item(Col1Value, rowBtnStockBalance).Tag = FrmObj
            FrmObj.StartPosition = FormStartPosition.CenterScreen
            FrmObj.ShowDialog()
        Else
            FrmObj.FFillOrderBalance()
            FrmObj.ShowDialog()
        End If

        If FrmObj.mOkButtonPressed = False Then Exit Sub

        Dim mRow As Integer = -1

        If mRow < 0 Then
            If Dgl1.Rows.Count > 1 Then
                mRow = Dgl1.Rows.Count - 1
            Else
                mRow = 0
            End If
        End If

        If Dgl1.Rows(mRow).IsNewRow = False Then
            Dgl1.Rows.Remove(Dgl1.Rows(mRow))
        End If
        Dim bRowCount As Integer = 0
        For I As Integer = 0 To FrmObj.Dgl1.Rows.Count - 1
            If Val(FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReceiveQty, I).Value) > 0 Then
                bRowCount += 1
            End If
        Next

        If bRowCount = 0 Then Exit Sub

        Dim Cnt As Integer = 0
        Dgl1.Rows.Insert(mRow, bRowCount)
        For I As Integer = 0 To FrmObj.Dgl1.Rows.Count - 1
            If Val(FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReceiveQty, I).Value) > 0 Then
                Dgl1.Item(Col1ItemType, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ItemType, I).Tag
                Dgl1.Item(Col1ItemType, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ItemType, I).Value

                Dgl1.Item(Col1SKU, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Sku, I).Tag
                Dgl1.Item(Col1SKU, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Sku, I).Value

                Dgl1.Item(Col1ItemCategory, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ItemCategory, I).Tag
                Dgl1.Item(Col1ItemCategory, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ItemCategory, I).Value

                Dgl1.Item(Col1ItemGroup, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ItemGroup, I).Tag
                Dgl1.Item(Col1ItemGroup, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ItemGroup, I).Value

                Dgl1.Item(Col1Item, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Item, I).Tag
                Dgl1.Item(Col1Item, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Item, I).Value

                Dgl1.Item(Col1Dimension1, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Dimension1, I).Tag
                Dgl1.Item(Col1Dimension1, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Dimension1, I).Value

                Dgl1.Item(Col1Dimension2, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Dimension2, I).Tag
                Dgl1.Item(Col1Dimension2, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Dimension2, I).Value

                Dgl1.Item(Col1Dimension3, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Dimension3, I).Tag
                Dgl1.Item(Col1Dimension3, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Dimension3, I).Value

                Dgl1.Item(Col1Dimension4, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Dimension4, I).Tag
                Dgl1.Item(Col1Dimension4, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Dimension4, I).Value

                Dgl1.Item(Col1Size, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Size, I).Tag
                Dgl1.Item(Col1Size, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Size, I).Value


                Dgl1.Item(Col1Qty, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReceiveQty, I).Value
                Dgl1.Item(Col1DocQty, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReceiveQty, I).Value
                Dgl1.Item(Col1Unit, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Unit, I).Value
                Dgl1.Item(Col1Unit, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1Unit, I).Tag

                Dgl1.Item(Col1RawMaterial, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1RawMaterial, I).Tag
                Dgl1.Item(Col1RawMaterial, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1RawMaterial, I).Value
                Dgl1.Item(Col1ReferenceDocIdBalanceQty, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1BalanceQty, I).Value

                Dgl1.Item(Col1ReferenceDocId, mRow + Cnt).Tag = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReferenceDocId, I).Tag
                Dgl1.Item(Col1ReferenceDocId, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReferenceDocId, I).Value
                Dgl1.Item(Col1ReferenceDocIdTSr, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReferenceDocIdTSr, I).Value
                Dgl1.Item(Col1ReferenceDocIdSr, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReferenceDocIdSr, I).Value

                Dgl1.Item(Col1ReferenceDocIdDate, mRow + Cnt).Value = FrmObj.Dgl1.Item(FrmStockEntryOrderSelection.Col1ReferenceDocIdDate, I).Value

                Dgl1.Item(Col1RawMaterialConsumptionQty, mRow + Cnt).Value = FGetFabricConsumption_Garment(mRow + Cnt)


                If (Dgl1.Item(Col1Unit, mRow + Cnt).Tag) Then
                    Dgl1.Item(Col1DocQty, mRow + Cnt).Style.ForeColor = Color.Blue
                    ShowStockEntryDimensionDetail(mRow + Cnt, False)
                    CType(Dgl1.Item(Col1DocQty, mRow + Cnt).Tag, FrmPurchaseInvoiceDimension_WithDimension).Dgl1.Item(FrmPurchaseInvoiceDimension_WithDimension.Col1Qty, 0).Value = Dgl1.Item(Col1Qty, mRow + Cnt).Value
                    CType(Dgl1.Item(Col1DocQty, mRow + Cnt).Tag, FrmPurchaseInvoiceDimension_WithDimension).Dgl1.Item(FrmPurchaseInvoiceDimension_WithDimension.Col1TotalQty, 0).Value = Dgl1.Item(Col1Qty, mRow + Cnt).Value
                    CType(Dgl1.Item(Col1DocQty, mRow + Cnt).Tag, FrmPurchaseInvoiceDimension_WithDimension).Calculation()
                End If

                mQry = "INSERT INTO StockVirtual (DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, SubCode, Process, 
                        Godown, Item, Qty_Iss, Qty_Rec, Unit, ReferenceDocId, ReferenceTSr) 
                        Select '" & mSearchCode & "' As DocID, " & I & " As TSr, " & I & " As Sr, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                        " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Date).Tag) & " As V_Date, 
                        " & Val(DglMain.Item(Col1Value, rowV_No).Tag) & " As V_No, 
                        " & AgL.Chk_Text(TxtDivision.Tag) & " As Div_Code, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & " As SubCode, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & " As Process, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowGodown).Tag) & " As Godown, 
                        " & AgL.Chk_Text(Dgl1.Item(Col1SKU, mRow + Cnt).Tag) & " As Item, 
                        0 As Qty_Iss, 
                        " & Val(Dgl1.Item(Col1Qty, mRow + Cnt).Value) & " As Qty_Rec, 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Unit, mRow + Cnt).Value) & " As Unit, 
                        " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, mRow + Cnt).Tag) & " As ReferenceDocId,
                        " & Val(Dgl1.Item(Col1ReferenceDocIdTSr, mRow + Cnt).Value) & " As ReferenceTSr "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                Cnt = Cnt + 1
            End If
        Next

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item(ColSNo, I).Value = I + 1
            FGeterateSkuName(I)
        Next
        Calculation()

        FOpenSelectedPurchOrderBalanceForPurchReceive(CType(Dgl2.Item(Col1Value, rowBtnPendingPurchOrder).Tag, FrmStockEntryOrderSelection))
    End Sub
    Private Sub FShowVoucherTypeHelp()
        mQry = " SELECT V_Type As Code, Description  FROM Voucher_Type WHERE NCat In ('" & EntryNCat & "') And IfNull(Status,'Active') = 'Active'"
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 350, 300, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Type", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            mV_Type = FRH_Single.DRReturn("Code")
        End If
    End Sub
    Private Sub FPostInLedger(SearchCode As String, Conn As Object, Cmd As Object)
        If FDivisionNameForCustomization(6) = "SADHVI" And AgL.StrCmp(AgL.PubDBName, "Sadhvi") Then
            Dim bGoodsAc As String = "GOODS"

            mQry = " Delete From Ledger Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                    AmtDr, AmtCr, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                    SELECT H.DocId, 1 AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                    Max(H.V_Date) AS V_Date, Max(H.SubCode) AS SubCode, 'GOODS' AS ContraSub, 
                    " & IIf(AgL.StrCmp(LblV_Type.Tag, Ncat.StockReceive), "0", "Sum(L.Amount)") & "  AS AmtDr, 
                    " & IIf(AgL.StrCmp(LblV_Type.Tag, Ncat.StockReceive), "Sum(L.Amount)", "0") & "  AS AmtCr, 
                    'Being Goods Transfered To ' + Max(H.PartyName) AS Narration, 
                    Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                    Max(H.ManualRefNo) AS RecId
                    FROM StockHead H With (NoLock)
                    LEFT JOIN StockHeadDetail L With (NoLock) ON H.DocID = L.DocID
                    WHERE H.DocId = '" & SearchCode & "'
                    GROUP BY H.DocID	
                    UNION ALL
                    SELECT H.DocId, 2 AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                    Max(H.V_Date) AS V_Date, 'GOODS' AS SubCode, Max(H.SubCode) AS ContraSub, 
                    " & IIf(AgL.StrCmp(LblV_Type.Tag, Ncat.StockReceive), "Sum(L.Amount)", "0") & "  AS AmtDr, 
                    " & IIf(AgL.StrCmp(LblV_Type.Tag, Ncat.StockReceive), "0", "Sum(L.Amount)") & "  AS AmtCr, 
                    'Being Goods Transfered To ' + Max(H.PartyName) AS Narration, 
                    Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                    Max(H.ManualRefNo) AS RecId
                    FROM StockHead H With (NoLock)
                    LEFT JOIN StockHeadDetail L With (NoLock) ON H.DocID = L.DocID
                    WHERE H.DocId = '" & SearchCode & "'
                    GROUP BY H.DocID	
                    ORDER BY DocID, V_SNo "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
End Class


