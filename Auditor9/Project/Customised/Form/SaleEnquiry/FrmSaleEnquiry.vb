Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq
Imports Customised.ClsMain

Public Class FrmSaleEnquiry
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1PartyItem As String = "Party Item"
    Public Const Col1PartyItemSpecification1 As String = "Party Item Specification1"
    Public Const Col1PartyItemSpecification2 As String = "Party Item Specification2"
    Public Const Col1PartyItemSpecification3 As String = "Party Item Specification3"
    Public Const Col1PartyItemSpecification4 As String = "Party Item Specification4"
    Public Const Col1PartyItemSpecification5 As String = "Party Item Specification5"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1BtnBarcodeDetail As String = "Barcode"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1Remark As String = "Remark"

    Public Const Col1ItemType As String = "Item Type"
    Public Const Col1SKU As String = "SKU"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1SaleEnquiryMappingDocId As String = "SaleEnquiryMappingDocId"
    Public Const Col1SaleOrderDocId As String = "SaleOrderDocId"
    Public Const Col1SaleEnquirySr As String = "SaleEnquiry Sr"
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

    Dim rowParty As Integer = 6
    Dim rowPartyDocNo As Integer = 7
    Dim rowPartyDocDate As Integer = 8

    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay
    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Dim rowDeliveryDate As Integer = 0
    Dim rowAgent As Integer = 1
    Dim rowCurrency As Integer = 2
    Dim rowRemarks As Integer = 3
    Dim rowTermsAndConditions As Integer = 4


    Public Const hcParty As String = "Party"

    Public Const hcDeliveryDate As String = "Delivery Date"
    Public Const hcPartyDocNo As String = "Party Doc No"
    Public Const hcPartyDocDate As String = "Party Doc Date"
    Public Const hcAgent As String = "Agent"
    Public Const hcCurrency As String = "Currency"
    Public Const hcRemarks As String = "Remarks"
    Public Const hcTermsAndConditions As String = "Terms & Conditions"


    Dim mPrevRowIndex As Integer = 0
    Dim Dgl As New AgControls.AgDataGrid
    Public WithEvents BtnFillPartyDetail As Button
    Public WithEvents LblPartyDetail As Label
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuHistory As ToolStripMenuItem
    Friend WithEvents MnuReport As ToolStripMenuItem
    Friend WithEvents MnuMapping As ToolStripMenuItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSaleEnquiry))
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
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalAmount = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalAmountText = New System.Windows.Forms.Label()
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button()
        Me.LblPartyDetail = New System.Windows.Forms.Label()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMapping = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReport = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(829, 581)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(205, 581)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(628, 581)
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
        Me.GBoxDivision.Location = New System.Drawing.Point(421, 581)
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
        Me.TabControl1.Size = New System.Drawing.Size(992, 224)
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
        Me.TP1.Size = New System.Drawing.Size(984, 198)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.PnlMain, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.Pnl2, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'PnlMain
        '
        Me.PnlMain.Location = New System.Drawing.Point(1, 3)
        Me.PnlMain.Size = New System.Drawing.Size(490, 192)
        Me.PnlMain.TabIndex = 0
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(502, 164)
        Me.LblV_Type.Size = New System.Drawing.Size(86, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Visible = False
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
        Me.Dgl1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
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
        Me.Pnl1.Location = New System.Drawing.Point(4, 264)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 287)
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
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 243)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(217, 20)
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
        Me.LblNature.Location = New System.Drawing.Point(622, 163)
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
        Me.Panel3.Location = New System.Drawing.Point(4, 212)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 134)
        Me.Panel3.TabIndex = 2
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(493, 3)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(491, 192)
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
        'BtnAttachments
        '
        Me.BtnAttachments.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAttachments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAttachments.Location = New System.Drawing.Point(12, 0)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(129, 23)
        Me.BtnAttachments.TabIndex = 3020
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Add Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.BtnAttachments)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(4, 551)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 695
        '
        'LblTotalQty
        '
        Me.LblTotalQty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(545, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(460, 3)
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
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.BackColor = System.Drawing.Color.White
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.Black
        Me.BtnFillPartyDetail.Image = Global.Customised.My.Resources.Resources._41104_200
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(552, 245)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(25, 16)
        Me.BtnFillPartyDetail.TabIndex = 746
        Me.BtnFillPartyDetail.TabStop = False
        Me.BtnFillPartyDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPartyDetail.UseVisualStyleBackColor = False
        Me.BtnFillPartyDetail.Visible = False
        '
        'LblPartyDetail
        '
        Me.LblPartyDetail.AutoSize = True
        Me.LblPartyDetail.BackColor = System.Drawing.Color.Transparent
        Me.LblPartyDetail.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyDetail.ForeColor = System.Drawing.Color.SteelBlue
        Me.LblPartyDetail.Location = New System.Drawing.Point(237, 243)
        Me.LblPartyDetail.Name = "LblPartyDetail"
        Me.LblPartyDetail.Size = New System.Drawing.Size(89, 16)
        Me.LblPartyDetail.TabIndex = 747
        Me.LblPartyDetail.Text = "PartyDetail"
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuHistory, Me.MnuMapping, Me.MnuReport})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(123, 70)
        '
        'MnuHistory
        '
        Me.MnuHistory.Name = "MnuHistory"
        Me.MnuHistory.Size = New System.Drawing.Size(122, 22)
        Me.MnuHistory.Text = "History"
        '
        'MnuMapping
        '
        Me.MnuMapping.Name = "MnuMapping"
        Me.MnuMapping.Size = New System.Drawing.Size(122, 22)
        Me.MnuMapping.Text = "Mapping"
        '
        'MnuReport
        '
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(122, 22)
        Me.MnuReport.Text = "Report"
        '
        'FrmSaleEnquiry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.BtnFillPartyDetail)
        Me.Controls.Add(Me.LblPartyDetail)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmSaleEnquiry"
        Me.Text = "SaleEnquiry Entry"
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
        Me.Controls.SetChildIndex(Me.LblPartyDetail, 0)
        Me.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
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
    Protected WithEvents BtnAttachments As Button
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalQty As Label
    Public WithEvents LblTotalAmount As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LblTotalAmountText As Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SaleEnquiry"
        LogTableName = "SaleEnquiry_Log"
        MainLineTableCsv = "SaleEnquiryDetail,SaleEnquiryMapping,SaleEnquiryMappingSku"
        LogLineTableCsv = "SaleEnquiryDetail_Log,SaleEnquiryMapping_Log,SaleEnquiryMappingSku_Log"

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        mQry = "Select DocID As SearchCode 
                From SaleEnquiry H  With (NoLock)
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  
                Where 1 = 1  " & mCondStr & "  Order By V_Date , V_No  "

        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [SaleEnquiry_Type], Cast(strftime('%d/%m/%Y', H.V_Date) As nvarchar) AS Date, SGV.Name AS [Party], " &
                            " H.ManualRefNo AS [Manual_No], H.SaleToPartyDocNo AS PartyDocNo, Cast(strftime('%d/%m/%Y', H.SaleToPartyDocDate) As nvarchar) AS PartyDocDate, Cast(strftime('%d/%m/%Y', H.DeliveryDate) As nvarchar) AS DeliveryDate, H.Remarks,  " &
                            " H.EntryBy AS [Entry_By], Cast(strftime('%d/%m/%Y', H.EntryDate) As nvarchar) AS [Entry_Date] " &
                            " FROM SaleEnquiry H  With (NoLock) " &
                            " LEFT JOIN Voucher_Type Vt  With (NoLock) ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN ViewHelpSubgroup SGV  With (NoLock) ON SGV.Code  = H.SaleToParty " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1PartyItem, 200, 0, Col1PartyItem, True, False)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification1, 100, 0, Col1PartyItemSpecification1, True, False)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification2, 100, 0, Col1PartyItemSpecification2, True, False)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification3, 100, 0, Col1PartyItemSpecification3, True, False)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification4, 100, 0, Col1PartyItemSpecification4, True, False)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification5, 100, 0, Col1PartyItemSpecification5, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, True, False, True)
            .AddAgButtonColumn(Dgl1, Col1BtnBarcodeDetail, 50, Col1BtnBarcodeDetail, True, False)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)

            .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, Col1ItemType, False, False)
            .AddAgTextColumn(Dgl1, Col1SKU, 200, 0, Col1SKU, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 200, 0, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 200, 0, Col1ItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 200, 0, Col1Specification, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1SaleEnquirySr, 150, 255, Col1SaleEnquirySr, False, False)
            .AddAgTextColumn(Dgl1, Col1SaleEnquiryMappingDocId, 150, 255, Col1SaleEnquiryMappingDocId, False)
            .AddAgTextColumn(Dgl1, Col1SaleOrderDocId, 150, 255, Col1SaleOrderDocId, False)
            .AddAgTextColumn(Dgl1, Col1IsRecordLocked, 150, 255, Col1IsRecordLocked, False, False)

            .AddAgTextColumn(Dgl1, Col1MItemCategory, 100, 0, Col1MItemCategory, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemGroup, 100, 0, Col1MItemGroup, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemSpecification, 100, 0, Col1MItemSpecification, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension1, 100, 0, "M " & AgL.PubCaptionDimension1, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension2, 100, 0, "M " & AgL.PubCaptionDimension2, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension3, 100, 0, "M " & AgL.PubCaptionDimension3, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension4, 100, 0, "M " & AgL.PubCaptionDimension4, False, False, False)
            .AddAgTextColumn(Dgl1, Col1MSize, 100, 0, Col1MSize, False, False, False)

        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl1)
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
        Dgl1.AgMandatoryColumn = Dgl1.Columns(Col1PartyItem).Index
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Name = "Dgl1"
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)

        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If


        DglMain.Rows.Add(4)
        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next
        DglMain.Item(Col1Head, rowParty).Value = hcParty
        DglMain.Item(Col1Head, rowPartyDocNo).Value = hcPartyDocNo
        DglMain.Item(Col1Head, rowPartyDocDate).Value = hcPartyDocDate
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None


        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col1Head, 140, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl2, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl2, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl2, Col1Value, 320, 255, Col1Value, True, False)
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


        Dgl2.Rows.Add(7)
        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2.Rows(I).Visible = False
        Next


        Dgl2.Item(Col1Head, rowDeliveryDate).Value = hcDeliveryDate
        Dgl2.Item(Col1Head, rowAgent).Value = hcAgent
        Dgl2.Item(Col1Head, rowCurrency).Value = hcCurrency
        Dgl2.Item(Col1Head, rowTermsAndConditions).Value = hcTermsAndConditions
        Dgl2.Rows(rowTermsAndConditions).Height = 50
        Dgl2.Item(Col1Head, rowRemarks).Value = hcRemarks
        Dgl2.Rows(rowRemarks).Height = 50

        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2(Col1HeadOriginal, I).Value = Dgl2(Col1Head, I).Value
        Next


        Dgl2.Name = "Dgl2"
        Dgl2.Tag = "VerticalGrid"

        ApplyUISetting()

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCustomGrid1.Name = "AgCustomGrid1"

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bSaleEnquirySelectionQry$ = "", bHelpValuesSelectionQry$ = ""

        mQry = " Update SaleEnquiry " &
                " SET  " &
                " SaleToParty = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " DeliveryDate = " & AgL.Chk_Date(Dgl2.Item(Col1Value, rowDeliveryDate).Value) & ", " &
                " SaleToPartyDocNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowPartyDocNo).Value) & ", " &
                " SaleToPartyDocDate = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowPartyDocDate).Value) & ", " &
                " Agent = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowAgent).Tag) & ", " &
                " Remarks =  " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowRemarks).Value) & ", " &
                " TermsAndConditions =  " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowTermsAndConditions).Value) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        CType(BtnFillPartyDetail.Tag, FrmSaleEnquiryParty).FSave(mSearchCode, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From SaleEnquiryDetail  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1PartyItem, I).Value <> "" And AgL.VNull(Dgl1.Item(Col1Qty, I).Value) <> 0 Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    InsertSaleEnquiryDetail(mSearchCode, mSr, I, Conn, Cmd)
                    FCreateSaleOrder(SearchCode, mSr, I, Conn, Cmd)
                    If Dgl1.Item(Col1BtnBarcodeDetail, I).Tag IsNot Nothing Then
                        CType(Dgl1.Item(Col1BtnBarcodeDetail, I).Tag, FrmSaleEnquiryBarcode).FSave(mSearchCode, mSr, Conn, Cmd)
                    End If
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        UpdateSaleEnquiryDetail(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
                        FCreateSaleOrder(SearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
                        If Dgl1.Item(Col1BtnBarcodeDetail, I).Tag IsNot Nothing Then
                            CType(Dgl1.Item(Col1BtnBarcodeDetail, I).Tag, FrmSaleEnquiryBarcode).FSave(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), Conn, Cmd)
                        End If
                    Else
                        DeleteLineData(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
                    End If
                End If
            End If
        Next

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub
    Private Sub DeleteLineData(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Val(Dgl1.Item(ColSNo, LineGridRowIndex).Tag) > 0 Then
            mQry = " Delete From SaleEnquiryBarcode Where DocId = '" & DocID & "' And TSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleEnquiryDetail Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleEnquiryMappingSku Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleEnquiryMapping Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertSaleEnquiryDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "INSERT INTO SaleEnquiryDetail (DocID, Sr, PartyItem, PartyItemSpecification1, PartyItemSpecification2, PartyItemSpecification3, PartyItemSpecification4, PartyItemSpecification5,
                Qty, Unit, Rate, Amount, Remark) "
        mQry += " VALUES (" & AgL.Chk_Text(DocID) & ", " & Sr & ",
                    " & AgL.Chk_Text(Dgl1.Item(Col1PartyItem, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification1, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification2, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification3, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification4, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification5, LineGridRowIndex).Value) & ", 
                    " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & ")"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    'Private Sub InsertSaleEnquiryMapping(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
    '    mQry = "INSERT INTO SaleEnquiryMapping (DocID, Sr, Item, Specification, Dimension1, Dimension2, Dimension3, Dimension4, Remark) "
    '    mQry += " VALUES (" & AgL.Chk_Text(DocID) & ", " & Sr & ",
    '                " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", 
    '                " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", 
    '                " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & ", 
    '                " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & ", 
    '                " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & ", 
    '                " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & ", 
    '                " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & ")"
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    'End Sub
    Private Sub UpdateSaleEnquiryDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Dgl1.Rows(LineGridRowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
            mQry = "UPDATE SaleEnquiryDetail
                    SET PartyItem = " & AgL.Chk_Text(Dgl1.Item(Col1PartyItem, LineGridRowIndex).Value) & ",
	                PartyItemSpecification1 = " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification1, LineGridRowIndex).Value) & ",
	                PartyItemSpecification2 = " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification2, LineGridRowIndex).Value) & ",
	                PartyItemSpecification3 = " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification3, LineGridRowIndex).Value) & ",
	                PartyItemSpecification4 = " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification4, LineGridRowIndex).Value) & ",
	                PartyItemSpecification5 = " & AgL.Chk_Text(Dgl1.Item(Col1PartyItemSpecification5, LineGridRowIndex).Value) & ",
	                Qty = " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ",
	                Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ",
	                Rate = " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ",
	                Amount = " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",
	                Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & "
                    Where DocID = '" & mSearchCode & "' 
                    And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    'Private Sub UpdateSaleEnquiryMapping(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
    '    If Dgl1.Rows(LineGridRowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
    '        mQry = "UPDATE SaleEnquiryMapping
    '                SET Item = " & AgL.Chk_Text(Dgl1.Item(Col1PartyItem, LineGridRowIndex).Tag) & ",
    '             Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & ",
    '             Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & ",
    '             Dimension3 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & ",
    '             Dimension4 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & ",
    '             Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & "
    '                Where DocID = '" & mSearchCode & "' 
    '                And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag
    '        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '    End If
    'End Sub
    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Topctrl1.Mode = "Browse" Then
            Select Case e.KeyCode
                Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
                Case Else
                    e.Handled = True
            End Select
            Exit Sub
        End If

        If e.Control And e.KeyCode = Keys.D Then
            If Val(Dgl1.Item(Col1IsRecordLocked, Dgl1.CurrentCell.RowIndex).Value) = 0 Then
                sender.CurrentRow.visible = False
                Calculation()
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

    End Sub

    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            If Dgl1.Item(Col1Qty, bRowIndex).Value = 0 Or Dgl1.Item(Col1PartyItem, bRowIndex).Value = "" Then Exit Sub
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1BtnBarcodeDetail
                    ShowBarcodeDetail(bRowIndex)


            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl1_CellContentClick function")
        End Try
    End Sub

    Private Sub ShowBarcodeDetail(mRow As Integer, Optional ShowDialog As Boolean = True)
        If Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag IsNot Nothing Then
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmSaleEnquiryBarcode).InvoiceNo = DglMain.Item(Col1Value, rowReferenceNo).Value & " Item : " & Dgl1.Item(Col1PartyItem, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmSaleEnquiryBarcode).EntryMode = Topctrl1.Mode
            If ShowDialog Then Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag.ShowDialog()
            'Dgl1.Item(Col1ItemDeductions, mRow).Value = CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmSaleEnquiryBarcode).GetDeductions
        Else

            Dim FrmObj As FrmSaleEnquiryBarcode
            FrmObj = New FrmSaleEnquiryBarcode
            FrmObj.InvoiceNo = DglMain.Item(Col1Value, rowReferenceNo).Value & " Item : " & Dgl1.Item(Col1PartyItem, mRow).Value

            FrmObj.IniGrid(mSearchCode, mRow + 1, Val(Dgl1.Item(Col1Qty, mRow).Value))
            FrmObj.EntryMode = Topctrl1.Mode
            Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag = FrmObj
            If ShowDialog Then Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag.ShowDialog()
            'Dgl1.Item(Col2ItemDeductions, mRow).Value = CType(Dgl1.Item(Col2BtnItemDetail, mRow).Tag, FrmSaleEnquiryBarcode).GetDeductions
        End If
        Calculation()

    End Sub

    'Private Sub ApplyUISettings(NCAT As String)
    '    Dim mQry As String
    '    Dim DtTemp As DataTable
    '    Dim I As Integer, J As Integer
    '    Dim mDgl2RowCount As Integer
    '    Dim mDglMainRowCount As Integer

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
    '        If mDgl2RowCount = 0 Then
    '            Dgl2.Visible = False
    '        Else
    '            Dgl2.Visible = True
    '        End If


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
        GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting(Dgl2, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False

        Dim DsMain As DataSet

        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0

        mQry = "SELECT H.DocID, H.V_Date, H.DeliveryDate, H.ManualRefNo, H.SaleToParty, SP.Name  AS SalePartyName, H.BillToParty, BP.name AS BillToPartyName, H.Agent, SA.name AS AgentName, H.SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyPinCode, H.SaleToPartyCity, H.SaleToPartyMobile, H.SaleToPartyEmail, H.SaleToPartySalesTaxNo, H.SaleToPartyAadharNo, 
                H.SaleToPartyPanNo, H.ShipToAddress, H.SaleToPartyDocNo, H.SaleToPartyDocDate, H.Remarks, H.TermsAndConditions, isnull(SaleToPartyName,'')  +' ' +isnull(SaleToPartyAddress,'')  +' '+isnull(SaleToPartyMobile,'')  +' ' AS PartyDetail  
                FROM SaleEnquiry H
                LEFT JOIN ViewHelpSubgroup SP ON SP.code = H.SaleToParty
                LEFT JOIN ViewHelpSubgroup BP ON BP.code = H.BillToParty
                LEFT JOIN ViewHelpSubgroup SA ON SA.code = H.Agent Where DocID='" & SearchCode & "'"

        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                'TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))

                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))
                DglMain.Item(Col1Value, rowParty).Tag = AgL.XNull(.Rows(0)("SaleToParty"))
                DglMain(Col1Value, rowParty).Value = AgL.XNull(.Rows(0)("SalePartyName"))


                Dgl2.Item(Col1Value, rowDeliveryDate).Value = AgL.XNull(.Rows(0)("DeliveryDate"))
                DglMain(Col1Value, rowPartyDocNo).Value = AgL.XNull(AgL.XNull(.Rows(0)("SaleToPartyDocNo")))
                DglMain(Col1Value, rowPartyDocDate).Value = AgL.XNull(AgL.XNull(.Rows(0)("SaleToPartyDocDate")))

                Dgl2(Col1Value, rowAgent).Tag = AgL.XNull(.Rows(0)("Agent"))
                Dgl2(Col1Value, rowAgent).Value = AgL.XNull(.Rows(0)("AgentName"))
                Dgl2(Col1Value, rowRemarks).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks")))
                Dgl2(Col1Value, rowTermsAndConditions).Value = AgL.XNull(AgL.XNull(.Rows(0)("TermsAndConditions")))

                LblPartyDetail.Text = AgL.XNull(.Rows(0)("PartyDetail"))

                If DglMain(Col1Value, rowParty).Value = "FreshEnquiry" Then
                    LblPartyDetail.Visible = True
                Else
                    LblPartyDetail.Visible = False
                End If

                AgCustomGrid1.FMoveRecFooterTable(DsMain.Tables(0))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                'mQry = "SELECT L.DocID, L.Sr, L.PartyItem, L.PartyItemSpecification1, L.PartyItemSpecification2, 
                '        L.PartyItemSpecification3, L.PartyItemSpecification4, L.PartyItemSpecification5, 
                '        L.Qty, L.Unit, L.Rate, L.Amount,  L.Remark, I.Description As ItemDesc, 
                '        MappedItem.BaseItem As Item, MappedItem.Dimension1, MappedItem.Dimension2, 
                '        MappedItem.Dimension3, MappedItem.Dimension4, MappedItem.Size,
                '        D1.Description As Dimension1Desc, D2.Description As Dimension2Desc,
                '        D3.Description As Dimension3Desc, D4.Description As Dimension4Desc, 
                '        S.Description As SizeDesc, 
                '        Sem.DocId As SaleEnquiryMappingDocId, Sid.DocId As SaleOrderDocId
                '        FROM SaleEnquiryDetail L 
                '        LEFT JOIN SaleEnquiryMapping Sem On L.DocId = Sem.DocId And L.Sr = Sem.Sr
                '        LEFT JOIN Item MappedItem On Sem.Item = MappedItem.Code
                '        LEFT JOIN Item I On MappedItem.BaseItem = I.Code
                '        LEFT JOIN Dimension1 D1 On MappedItem.Dimension1 = D1.Code
                '        LEFT JOIN Dimension2 D2 On MappedItem.Dimension2 = D2.Code
                '        LEFT JOIN Dimension3 D3 On MappedItem.Dimension3 = D3.Code
                '        LEFT JOIN Dimension4 D4 On MappedItem.Dimension4 = D4.Code
                '        LEFT JOIN Size S On MappedItem.Size = S.Code
                '        LEFT JOIN SaleInvoiceDetail Sid WITH (Nolock) On L.DocId = Sid.GenDocId And L.Sr = Sid.GenDocIdSr 
                '        Where L.DocID ='" & SearchCode & "'
                '        Order By L.Sr "

                mQry = "SELECT L.DocID, L.Sr, L.PartyItem, L.PartyItemSpecification1, L.PartyItemSpecification2, 
                        L.PartyItemSpecification3, L.PartyItemSpecification4, L.PartyItemSpecification5, 
                        L.Qty, L.Unit, L.Rate, L.Amount,  L.Remark, 
                        Sem.Item, I.Description As ItemDesc, I.ManualCode, 
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
                        Sem.DocId As SaleEnquiryMappingDocId, Sid.DocId As SaleOrderDocId
                        FROM SaleEnquiryDetail L 
                        LEFT JOIN SaleEnquiryMapping Sem On L.DocId = Sem.DocId And L.Sr = Sem.Sr
                        LEFT JOIN SaleEnquiryMappingSku Sids With (NoLock) On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                        LEFT JOIN Item MappedItem On Sem.Item = MappedItem.Code
                        LEFT JOIN Item Sku ON Sku.Code = Sem.Item
                        LEFT JOIN ItemType It On Sku.ItemType = It.Code
                        Left Join Item IC On Sids.ItemCategory = IC.Code
                        Left Join Item IG On Sids.ItemGroup = IG.Code
                        LEFT JOIN Item I ON Sids.Item = I.Code
                        LEFT JOIN Item D1 ON Sids.Dimension1 = D1.Code
                        LEFT JOIN Item D2 ON Sids.Dimension2 = D2.Code
                        LEFT JOIN Item D3 ON Sids.Dimension3 = D3.Code
                        LEFT JOIN Item D4 ON Sids.Dimension4 = D4.Code
                        LEFT JOIN Item Size ON Sids.Size = Size.Code
                        LEFT JOIN SaleInvoiceDetail Sid WITH (Nolock) On L.DocId = Sid.GenDocId And L.Sr = Sid.GenDocIdSr 
                        Where L.DocID ='" & SearchCode & "'
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

                            Dgl1.Item(Col1PartyItem, I).Tag = AgL.XNull(.Rows(I)("PartyItem"))
                            Dgl1.Item(Col1PartyItem, I).Value = AgL.XNull(.Rows(I)("PartyItem"))


                            Dgl1.Item(Col1PartyItemSpecification1, I).Value = AgL.XNull(.Rows(I)("PartyItemSpecification1"))
                            Dgl1.Item(Col1PartyItemSpecification2, I).Value = AgL.XNull(.Rows(I)("PartyItemSpecification2"))
                            Dgl1.Item(Col1PartyItemSpecification3, I).Value = AgL.XNull(.Rows(I)("PartyItemSpecification3"))
                            Dgl1.Item(Col1PartyItemSpecification4, I).Value = AgL.XNull(.Rows(I)("PartyItemSpecification4"))
                            Dgl1.Item(Col1PartyItemSpecification5, I).Value = AgL.XNull(.Rows(I)("PartyItemSpecification5"))




                            Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl1.Item(Col1Unit, I).Tag = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))

                            Dgl1.Item(Col1SKU, I).Tag = AgL.XNull(.Rows(I)("SkuCode"))
                            Dgl1.Item(Col1SKU, I).Value = AgL.XNull(.Rows(I)("SkuDescription"))


                            Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(.Rows(I)("ItemType"))
                            Dgl1.Item(Col1ItemType, I).Value = AgL.XNull(.Rows(I)("ItemTypeDesc"))
                            Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
                            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
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

                            Dgl1.Item(Col1MItemCategory, I).Tag = AgL.XNull(.Rows(I)("MItemCategory"))
                            Dgl1.Item(Col1MItemGroup, I).Tag = AgL.XNull(.Rows(I)("MItemGroup"))
                            Dgl1.Item(Col1MItemSpecification, I).Value = AgL.XNull(.Rows(I)("MItemSpecification"))
                            Dgl1.Item(Col1MDimension1, I).Tag = AgL.XNull(.Rows(I)("MDimension1"))
                            Dgl1.Item(Col1MDimension2, I).Tag = AgL.XNull(.Rows(I)("MDimension2"))
                            Dgl1.Item(Col1MDimension3, I).Tag = AgL.XNull(.Rows(I)("MDimension3"))
                            Dgl1.Item(Col1MDimension4, I).Tag = AgL.XNull(.Rows(I)("MDimension4"))
                            Dgl1.Item(Col1MSize, I).Tag = AgL.XNull(.Rows(I)("MSize"))

                            Dgl1.Item(Col1SaleEnquiryMappingDocId, I).Value = AgL.XNull(.Rows(I)("SaleEnquiryMappingDocId"))
                            Dgl1.Item(Col1SaleOrderDocId, I).Value = AgL.XNull(.Rows(I)("SaleOrderDocId"))

                            If Val(Dgl1.Item(Col1IsRecordLocked, I).Value) > 0 Then
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True
                            End If

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                        Next I
                    End If
                End With

                DglMain.DefaultCellStyle.WrapMode = DataGridViewTriState.True
                Dgl2.DefaultCellStyle.WrapMode = DataGridViewTriState.True

                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
                '-------------------------------------------------------------
            End If
        End With
        SetAttachmentCaption()
        Dgl1.ReadOnly = True
        BtnFillPartyDetail.Tag = Nothing
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub
    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        'Dim FrmObj As New FrmSaleEnquiryPartyDetail


        Try
            Select Case sender.NAME
                'Case TxtV_Type.Name
                '    If DglMain.Item(Col1Value, rowV_Type).Tag = "" Then Exit Sub




                '    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GcnRead)
                '    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                '    IniGrid()
                '    ApplyUISettings(LblV_Type.Tag)
                '    DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleEnquiry", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

                'Case TxtParty.Name
                '    If DglMain(Col1Value, rowParty).Value = "freshEnquiry,ACHALDA" Then
                '        LblPartyDetail.Visible = True
                '    Else
                '        LblPartyDetail.Visible = False
                '    End If
                '    Validating_SaleToParty(DglMain.Item(Col1Value, rowParty).Tag)



                'Case TxtReferenceNo.Name
                '    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "SaleEnquiry",
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


                TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GcnRead)
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                IniGrid()
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

            Case rowParty
                Validating_SaleToParty(DglMain.Item(Col1Value, rowParty).Tag)

            Case rowReferenceNo
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead",
                                DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)
        End Select
    End Sub
    Private Sub Validating_SaleToParty(Subcode As String, Optional ShowDialogForFreshEnquiryParty As Boolean = True)
        Dim DtTemp As DataTable
        If DglMain.Item(Col1Value, rowV_Date).Value <> "" And DglMain(Col1Value, rowParty).Value <> "" Then

            'If TxtParty.AgLastValueTag <> DglMain.Item(Col1Value, rowParty).Tag Or Topctrl1.Mode = "Add" Then

            mQry = "Select H.*, RT.Description as RateTypeName, Agent.Name as AgentName, Transporter.Name as TransporterName 
                                    From SubgroupSiteDivisionDetail H  With (NoLock)
                                    Left Join RateType RT With (NoLock) on H.RateType = RT.Code
                                    Left Join viewHelpSubgroup agent With (NoLock) On H.Agent = Agent.Code
                                    Left Join viewHelpSubgroup Transporter With (NoLock) On H.Transporter = Transporter.Code
                                    Where H.Subcode = '" & Subcode & "' And H.Site_Code='" & DglMain.Item(Col1Value, rowSite_Code).Tag & "' And H.Div_Code='" & TxtDivision.Tag & "'"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                Dgl2(Col1Value, rowAgent).Tag = AgL.XNull(DtTemp.Rows(0)("Agent"))
                Dgl2(Col1Value, rowAgent).Value = AgL.XNull(DtTemp.Rows(0)("AgentName"))

                If AgL.XNull(DtTemp.Rows(0)("TermsAndConditions")) <> "" Then
                    Dgl2(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtTemp.Rows(0)("TermsAndConditions"))
                Else

                End If
            Else
            End If



            'FGetCurrBal(Subcode)


            BtnFillPartyDetail.Tag = Nothing
            ShowSaleInvoiceParty("", Subcode, "", ShowDialogForFreshEnquiryParty)
        End If
        'End If

    End Sub
    Private Sub BtnFillPartyDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPartyDetail.Click
        If Topctrl1.Mode = "Add" Then
            ShowSaleInvoiceParty("", DglMain.Item(Col1Value, rowParty).Tag, "", True)
        Else
            ShowSaleInvoiceParty(mSearchCode, "", "", True)
        End If
    End Sub

    Private Sub ShowSaleInvoiceParty(DocID As String, PartyCode As String, AcGroupNature As String, Optional ShowDialogForFreshEnquiry As Boolean = False)
        If LblV_Type.Tag = Ncat.SaleOrder Then
            If PartyCode = "D100003305" Then
                AcGroupNature = "FreshEnquiry"
            End If
        End If

        If BtnFillPartyDetail.Tag IsNot Nothing Then
            CType(BtnFillPartyDetail.Tag, FrmSaleEnquiryParty).EntryMode = Topctrl1.Mode
            CType(BtnFillPartyDetail.Tag, FrmSaleEnquiryParty).DivisionCode = TxtDivision.Tag
            CType(BtnFillPartyDetail.Tag, FrmSaleEnquiryParty).SiteCode = DglMain.Item(Col1Value, rowSite_Code).Tag
            CType(BtnFillPartyDetail.Tag, FrmSaleEnquiryParty).objFrmSaleEnquiry = Me
            'CType(BtnFillPartyDetail.Tag, FrmSaleEnquiryParty).InvoiceAmount = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))

            BtnFillPartyDetail.Tag.ShowDialog()
        Else
            Dim FrmObj As FrmSaleEnquiryParty
            FrmObj = New FrmSaleEnquiryParty
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DivisionCode = TxtDivision.Tag
            FrmObj.SiteCode = DglMain.Item(Col1Value, rowSite_Code).Tag
            FrmObj.IniGrid(DocID, PartyCode, AcGroupNature)
            'FrmObj.objFrmSaleInvoice = Me
            'FrmObj.InvoiceAmount = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
            BtnFillPartyDetail.Tag = FrmObj
            If AcGroupNature.ToUpper = "FreshEnquiry" And ShowDialogForFreshEnquiry Then
                BtnFillPartyDetail.Tag.ShowDialog()
            End If
        End If

        If BtnFillPartyDetail.Tag IsNot Nothing Then
            LblPartyDetail.Text = CType(BtnFillPartyDetail.Tag, FrmSaleEnquiryParty).Dgl1(Col1Value, FrmSaleEnquiryParty.rowPartyName).Value
        End If

        If DglMain(Col1Value, rowParty).Value = "freshEnquiry,ACHALDA" Then
            LblPartyDetail.Visible = True
        Else
            LblPartyDetail.Visible = False
        End If

    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        BtnFillPartyDetail.Tag = Nothing
        IniGrid()
        'ApplyUISettings(LblV_Type.Tag)

        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleEnquiry", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

        Dgl1.ReadOnly = False

        If DglMain.Visible = True Then
            If DglMain.FirstDisplayedCell IsNot Nothing Then
                DglMain.CurrentCell = DglMain(Col1Value, DglMain.FirstDisplayedCell.RowIndex)
                DglMain.Focus()
            End If
        End If


        SetAttachmentCaption()
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean

        Dgl1.EndEdit()
        Dgl2.EndEdit()
        DglMain.EndEdit()

        'If AgL.RequiredField(TxtParty, LblBuyer.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1PartyItem).Index) Then passed = False : Exit Sub


        If AgL.XNull(DglMain.Item(Col1Value, rowPartyDocDate).Value) <> "" Then
            If CDate(DglMain.Item(Col1Value, rowPartyDocDate).Value) > CDate(DglMain.Item(Col1Value, rowV_Date).Value) Then
                MsgBox("Party Doc Date can not exceed entry date.", MsgBoxStyle.Information)
                passed = False
                Exit Sub
            End If
        End If

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1PartyItem, I).Value <> "" Then
                        If AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) <> "" _
                            Or AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) <> "" _
                            Or AgL.XNull(Dgl1.Item(Col1Item, I).Value) <> "" _
                            Or AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) <> "" _
                            Or AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) <> "" _
                            Or AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) <> "" _
                            Or AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) <> "" _
                            Or AgL.XNull(Dgl1.Item(Col1Size, I).Value) <> "" _
                       Then
                            Dgl1.Item(Col1SKU, I).Tag = ClsMain.FGetSKUCode(I + 1, Dgl1.Item(Col1ItemType, I).Tag _
                                           , AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Tag), AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Tag), AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1Item, I).Tag), AgL.XNull(Dgl1.Item(Col1Item, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1Dimension1, I).Tag), AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1Dimension2, I).Tag), AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1Dimension3, I).Tag), AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1Dimension4, I).Tag), AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1Size, I).Tag), AgL.XNull(Dgl1.Item(Col1Size, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1MItemCategory, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1MItemGroup, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1MItemSpecification, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1MDimension1, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1MDimension2, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1MDimension3, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1MDimension4, I).Value) _
                                           , AgL.XNull(Dgl1.Item(Col1MSize, I).Value)
                                           )
                            If Dgl1.Item(Col1SKU, I).Tag = "" Then
                                MsgBox("Item Combination is not allowed...!", MsgBoxStyle.Information)
                                passed = False
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            Next
        End With


        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "SaleEnquiry",
                                    DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                    DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                    DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)

        If Not CheckDuplicateRef Then
            DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleEnquiry", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                'Case TxtParty.Name
                '    If e.KeyCode <> Keys.Enter Then
                '        If sender.AgHelpDataset Is Nothing Then
                '            FCreateHelpSubgroup()
                '        End If
                '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            End Select

            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                If DglMain.CurrentCell.RowIndex = LastCell.RowIndex And DglMain.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                    If CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value Then
                        DglMain.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    End If

                    If Dgl2.Visible Then
                        Dgl2.CurrentCell = Dgl2.Item(Col1Value, Dgl2.FirstDisplayedCell.RowIndex)
                        Dgl2.Focus()
                    Else
                        Dgl1.CurrentCell = Dgl1.FirstDisplayedCell
                        Dgl1.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Try
            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Catch ex As Exception
        End Try

        LblPartyDetail.Visible = False
        BtnFillPartyDetail.Tag = Nothing
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dim mRow As Integer, mCol As Integer
        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If
        Try
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            mRow = Dgl1.CurrentCell.RowIndex
            mCol = Dgl1.CurrentCell.ColumnIndex
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1PartyItem, Col1PartyItemSpecification1, Col1PartyItemSpecification2, Col1PartyItemSpecification3, Col1PartyItemSpecification4, Col1PartyItemSpecification5, Col1Unit
                    If Dgl1.CurrentCell.RowIndex > 0 Then
                        If AgL.XNull(Dgl1.Item(mCol, mRow).Value) = "" Then
                            If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                                Dgl1.Item(mCol, mRow).Value = Dgl1.Item(mCol, mRow - 1).Value
                            End If
                        End If
                    End If

                Case Col1ItemCategory
                    If Dgl1.CurrentCell.RowIndex > 0 Then
                        If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                            If AgL.XNull(Dgl1.Item(mCol, mRow).Value) = "" Then
                                Dgl1.Item(mCol, mRow).Value = Dgl1.Item(mCol, mRow - 1).Value
                                Dgl1.Item(mCol, mRow).Tag = Dgl1.Item(mCol, mRow - 1).Tag
                            End If
                        End If
                    End If

                Case Col1Dimension1, Col1Dimension2, Col1Dimension3, Col1Dimension4, Col1Size
                    If Dgl1.CurrentCell.RowIndex > 0 Then
                        If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                            If AgL.XNull(Dgl1.Item(mCol, mRow).Value) = "" Then
                                FCopyDimensionsInNextRow(mRow, mCol)
                            End If
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FCopyDimensionsInNextRow(mRow As Integer, mCol As Integer)
        If Dgl1.Columns(Col1PartyItem).HeaderText.Replace("Party", "").Replace(" ", "") = Dgl1.Columns(mCol).HeaderText Then
            If Dgl1.Item(Col1PartyItem, mRow).Value = Dgl1.Item(Col1PartyItem, mRow - 1).Value Then
                Dgl1.Item(mCol, mRow).Value = Dgl1.Item(mCol, mRow - 1).Value
                Dgl1.Item(mCol, mRow).Tag = Dgl1.Item(mCol, mRow - 1).Tag
            End If
        ElseIf Dgl1.Columns(Col1PartyItemSpecification1).HeaderText.Replace("Party", "").Replace(" ", "") = Dgl1.Columns(mCol).HeaderText Then
            If Dgl1.Item(Col1PartyItemSpecification1, mRow).Value = Dgl1.Item(Col1PartyItemSpecification1, mRow - 1).Value Then
                Dgl1.Item(mCol, mRow).Value = Dgl1.Item(mCol, mRow - 1).Value
                Dgl1.Item(mCol, mRow).Tag = Dgl1.Item(mCol, mRow - 1).Tag
            End If
        ElseIf Dgl1.Columns(Col1PartyItemSpecification2).HeaderText.Replace("Party", "").Replace(" ", "") = Dgl1.Columns(mCol).HeaderText Then
            If Dgl1.Item(Col1PartyItemSpecification2, mRow).Value = Dgl1.Item(Col1PartyItemSpecification2, mRow - 1).Value Then
                Dgl1.Item(mCol, mRow).Value = Dgl1.Item(mCol, mRow - 1).Value
                Dgl1.Item(mCol, mRow).Tag = Dgl1.Item(mCol, mRow - 1).Tag
            End If
        ElseIf Dgl1.Columns(Col1PartyItemSpecification3).HeaderText.Replace("Party", "").Replace(" ", "") = Dgl1.Columns(mCol).HeaderText Then
            If Dgl1.Item(Col1PartyItemSpecification3, mRow).Value = Dgl1.Item(Col1PartyItemSpecification3, mRow - 1).Value Then
                Dgl1.Item(mCol, mRow).Value = Dgl1.Item(mCol, mRow - 1).Value
                Dgl1.Item(mCol, mRow).Tag = Dgl1.Item(mCol, mRow - 1).Tag
            End If
        ElseIf Dgl1.Columns(Col1PartyItemSpecification4).HeaderText.Replace("Party", "").Replace(" ", "") = Dgl1.Columns(mCol).HeaderText Then
            If Dgl1.Item(Col1PartyItemSpecification4, mRow).Value = Dgl1.Item(Col1PartyItemSpecification4, mRow - 1).Value Then
                Dgl1.Item(mCol, mRow).Value = Dgl1.Item(mCol, mRow - 1).Value
                Dgl1.Item(mCol, mRow).Tag = Dgl1.Item(mCol, mRow - 1).Tag
            End If
        ElseIf Dgl1.Columns(Col1PartyItemSpecification5).HeaderText.Replace("Party", "").Replace(" ", "") = Dgl1.Columns(mCol).HeaderText Then
            If Dgl1.Item(Col1PartyItemSpecification5, mRow).Value = Dgl1.Item(Col1PartyItemSpecification5, mRow - 1).Value Then
                Dgl1.Item(mCol, mRow).Value = Dgl1.Item(mCol, mRow - 1).Value
                Dgl1.Item(mCol, mRow).Tag = Dgl1.Item(mCol, mRow - 1).Tag
            End If
        End If
    End Sub
    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)

        'Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmSaleEnquiry_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer
        If Dgl1.AgHelpDataSet(Col1PartyItem) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1PartyItem).Dispose() : Dgl1.AgHelpDataSet(Col1PartyItem) = Nothing
        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification1) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1PartyItemSpecification1).Dispose() : Dgl1.AgHelpDataSet(Col1PartyItemSpecification1) = Nothing
        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification2) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1PartyItemSpecification2).Dispose() : Dgl1.AgHelpDataSet(Col1PartyItemSpecification2) = Nothing
        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification3) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1PartyItemSpecification3).Dispose() : Dgl1.AgHelpDataSet(Col1PartyItemSpecification3) = Nothing
        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification4) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1PartyItemSpecification4).Dispose() : Dgl1.AgHelpDataSet(Col1PartyItemSpecification4) = Nothing
        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification5) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1PartyItemSpecification5).Dispose() : Dgl1.AgHelpDataSet(Col1PartyItemSpecification5) = Nothing

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


        For i = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(i).DefaultCellStyle.BackColor = Dgl1.AgReadOnlyColumnColor Then
                Dgl1.Columns(i).ReadOnly = True
            End If
        Next
    End Sub
    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
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
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            Dgl1.CurrentCell = Dgl1.Item(Col1PartyItem, Dgl1.Rows.Count - 1) : Dgl1.Focus()
        End If
    End Sub
    Private Function FCreateHelpSubgroup() As DataSet
        Dim strCond As String = ""

        Dim FilterInclude_AcGroup As String = FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General)
        If FilterInclude_AcGroup <> "" Then
            strCond += " And CharIndex('+' || Sg.GroupCode,'" & FilterInclude_AcGroup & "') > 0 "
            strCond += " And CharIndex('-' || Sg.GroupCode,'" & FilterInclude_AcGroup & "') <= 0 "
        End If

        Dim FilterInclude_SubgroupType As String = FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General)
        If FilterInclude_AcGroup <> "" Then
            strCond += " And CharIndex('+' || Sg.SubgroupType,'" & FilterInclude_SubgroupType & "') > 0 "
            strCond += " And CharIndex('-' || Sg.SubgroupType,'" & FilterInclude_SubgroupType & "') <= 0 "
        End If

        Dim FilterInclude_Nature As String = FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General)
        If FilterInclude_AcGroup <> "" Then
            strCond += " And CharIndex('+' || Sg.Nature,'" & FilterInclude_Nature & "') > 0 "
            strCond += " And CharIndex('-' || Sg.Nature,'" & FilterInclude_Nature & "') <= 0 "
        End If

        strCond += " And Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Bank & "')"

        mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.Address, Ag.GroupName, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " &
                " FROM Subgroup Sg  With (NoLock) " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'TxtParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Sub FrmSaleEnquiry_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        'For SSRS Print Out

        mQry = "SELECT H.DocID  FROM SaleEnquiry H With (NoLock)
                LEFT JOIN SaleEnquiryDetail L With (NoLock) ON H.DocID = L.DocID 
                WHERE H.DocID ='" & SearchCode & "' And H.Gross_Amount > 0
                GROUP BY H.DocID 
                HAVING Sum(L.Amount)<>Max(H.Gross_Amount)"
        If AgL.FillData(mQry, AgL.GCn).Tables(0).Rows.Count > 0 Then
            MsgBox("Something went wrong with gross amount. Can not print Invoice. Please check once.")
            Exit Sub
        End If

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
                    From SaleEnquiry H  With (NoLock)
                    Where H.DocID = '" & mSearchCode & "'"
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            mText = Replace(mText, "<CreditDays>", AgL.XNull(dtTemp.Rows(0)("CreditDays")))
            ClsMain.FReplacePubVariables(mText, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag)

        End If

        FReplaceInvoiceVariables = mText
    End Function
    Private Sub FrmSaleEnquiry_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Dgl1.ReadOnly = False
        ShowSaleInvoiceParty(mSearchCode, "", "")
    End Sub
    Private Sub Dgl1_DoubleClick(sender As Object, e As EventArgs) Handles Dgl1.DoubleClick
        If Topctrl1.Mode = "Browse" Then
            Dgl1.CurrentRow.Selected = True
        End If
    End Sub
    Private Sub FSendSms()
        Dim FrmObj As FrmSendSms
        FrmObj = New FrmSendSms(AgL)
        FrmObj.TxtToMobile.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Mobile
                    From SaleEnquiry H  With (NoLock)
                    LEFT JOIN ViewHelpSubgroup Sg  With (NoLock) On H.Party = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()
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
                Case rowDeliveryDate, rowPartyDocDate
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


                Case rowAgent
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.SalesAgent & "' Order By Name"
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
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
                Case Col1ItemCategory
                    Validating_ItemCategory(mColumnIndex, mRowIndex)
            End Select
            FGeterateSkuName(mRowIndex)
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub




    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
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
            If FileCount > 0 Then BtnAttachments.Text = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else BtnAttachments.Text = "Attachments"
        Else
            BtnAttachments.Text = "Attachments"
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub

        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Rows(I).Visible Then
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1PartyItem
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1PartyItem) Is Nothing Then
                            mQry = "SELECT L.PartyItem AS Code, L.PartyItem
                                    FROM SaleEnquiry H
                                    LEFT JOIN saleenquirydetail L ON L.DocID = H.DocID 
                                    WHERE L.PartyItem IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                                    GROUP BY L.PartyItem "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                    End If


                Case Col1PartyItemSpecification1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification1) Is Nothing Then
                            mQry = "SELECT L.PartyItemSpecification1 AS Code, L.PartyItemSpecification1
                                    FROM SaleEnquiry H
                                    LEFT JOIN saleenquirydetail L ON L.DocID = H.DocID 
                                    WHERE L.PartyItemSpecification1 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                                    GROUP BY L.PartyItemSpecification1 "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                    End If

                Case Col1PartyItemSpecification2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification2) Is Nothing Then
                            mQry = "SELECT L.PartyItemSpecification2 AS Code, L.PartyItemSpecification2
                                    FROM SaleEnquiry H
                                    LEFT JOIN saleenquirydetail L ON L.DocID = H.DocID 
                                    WHERE L.PartyItemSpecification2 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                                    GROUP BY L.PartyItemSpecification2 "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                    End If


                Case Col1PartyItemSpecification3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification3) Is Nothing Then
                            mQry = "SELECT L.PartyItemSpecification3 AS Code, L.PartyItemSpecification3
                                    FROM SaleEnquiry H
                                    LEFT JOIN saleenquirydetail L ON L.DocID = H.DocID 
                                    WHERE L.PartyItemSpecification3 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                                    GROUP BY L.PartyItemSpecification3 "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                    End If

                Case Col1PartyItemSpecification4
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification4) Is Nothing Then
                            mQry = "SELECT L.PartyItemSpecification4 AS Code, L.PartyItemSpecification4
                                    FROM SaleEnquiry H
                                    LEFT JOIN saleenquirydetail L ON L.DocID = H.DocID 
                                    WHERE L.PartyItemSpecification4 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                                    GROUP BY L.PartyItemSpecification4 "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                    End If

                Case Col1PartyItemSpecification5
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1PartyItemSpecification5) Is Nothing Then
                            mQry = "SELECT L.PartyItemSpecification5 AS Code, L.PartyItemSpecification5
                                    FROM SaleEnquiry H
                                    LEFT JOIN saleenquirydetail L ON L.DocID = H.DocID 
                                    WHERE L.PartyItemSpecification5 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                                    GROUP BY L.PartyItemSpecification5 "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                    End If

                Case Col1Unit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                            mQry = "SELECT Code, Code as Description FROM Unit "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
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


                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension1 H Order By H.Description "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension2 H Order By H.Description "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension3) Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension3 H Order By H.Description "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension4
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension4) Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension4 H Order By H.Description "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Size
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Size) Is Nothing Then
                            mQry = " Select H.Code, H.Description From Size H Order By H.Description "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FCreateHelpItem(RowIndex As Integer)
        Dim strCond As String = ""

        mQry = "SELECT I.Code, I.Description, IG.PrintingDescription as ItemGroup_PD, I.Rate " &
                  " FROM Item I  With (NoLock) " &
                  " Left Join Item IG On I.ItemGroup = IG.Code " &
                  " Where I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' 
                  And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
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
                    I.ItemCategory, I.ItemGroup, IC.Description as ItemCategoryName, IG.Description as ItemGroupName,
                    U.ShowDimensionDetailInSales, U.DecimalPlaces as QtyDecimalPlaces, IG.Default_DiscountPerSale ,
                    IG.Default_AdditionalDiscountPerSale, IG.Default_AdditionPerSale, I.PurchaseRate,
                    IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase
                                From Item I  With (NoLock)
                                Left Join Unit U  With (NoLock) On I.Unit = U.Code 
                                Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                                Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                Dgl1.Item(Col1PartyItem, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Code"))
                Dgl1.Item(Col1PartyItem, mRow).Value = AgL.XNull(DtItem.Rows(0)("Description"))
                Call FCheckDuplicate(mRow)
                Dgl1.Item(Col1PartyItemSpecification1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1PartyItemSpecification1, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))
                Dgl1.Item(Col1PartyItemSpecification2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                Dgl1.Item(Col1PartyItemSpecification2, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                'Dgl1.Item(Col1PartyItemCode, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Code"))
                'Dgl1.Item(Col1PartyItemCode, mRow).Value = AgL.XNull(DtItem.Rows(0)("ManualCode"))
                'Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                'Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtItem.Rows(0)("QtyDecimalPlaces"))
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
                    If .Item(Col1PartyItem, I).Value <> "" Then
                        If mRow <> I Then
                            'Str1 = Dgl1.Item(Col1Item, I).Value & Dgl1.Item(Col1Specification, I).Value & Dgl1.Item(Col1Dimension1, I).Value & Dgl1.Item(Col1Dimension2, I).Value & Dgl1.Item(Col1Dimension3, I).Value & Dgl1.Item(Col1Dimension4, I).Value & Dgl1.Item(Col1Item, I).Value
                            'Str2 = Dgl1.Item(Col1Item, mRow).Value & Dgl1.Item(Col1Specification, mRow).Value & Dgl1.Item(Col1Dimension1, mRow).Value & Dgl1.Item(Col1Dimension2, mRow).Value & Dgl1.Item(Col1Dimension3, mRow).Value & Dgl1.Item(Col1Dimension4, mRow).Value & Dgl1.Item(Col1Item, mRow).Value
                            If AgL.StrCmp(Str1, Str2) Then
                                If MsgBox("Item " & .Item(Col1PartyItem, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    Dgl1.Item(Col1PartyItem, mRow).Tag = "" : Dgl1.Item(Col1PartyItem, mRow).Value = ""
                                Else
                                    If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) = ActionOnDuplicateItem.DoNothing Then
                                    ElseIf FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) = ActionOnDuplicateItem.AlertAndGoToFirstItem Then
                                        Dim mFirstRowIndex As Integer
                                        mFirstRowIndex = Val(Dgl1.Item(ColSNo, I).Value) - 1
                                        Dgl1.CurrentCell = Dgl1.Item(Col1Qty, mFirstRowIndex)
                                        Dgl1.Item(Col1PartyItem, mRow).Tag = "" : Dgl1.Item(Col1PartyItem, mRow).Value = ""
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

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM ItemCategory I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpItemGroup(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        If Dgl1.Item(Col1PartyItemSpecification1, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1PartyItemSpecification1, RowIndex).Tag & "' Or I.ItemCategory Is Null ) "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
            strCond += " And (IG.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(IG.ShowItemGroupInOtherDivisions,0) =1) "
        End If


        mQry = "Select IG.Code, IG.Description 
                From Item I  With (NoLock)
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & "
                Group By I.ItemGroup,IG.Code, IG.Description "
        Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
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
                        Else
                            Dgl1.CurrentCell = Dgl1.FirstDisplayedCell
                            Dgl1.Focus()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuMapping.Click, MnuHistory.Click
        Select Case sender.name
            Case MnuMapping.Name
                FMappingWizard()

            Case MnuHistory.Name
                ClsMain.FShowHistory(mSearchCode, Me)
        End Select
    End Sub
    Private Sub FMappingWizard()
        Dim StrSenderText As String = "Sale Enquiry Mapping"
        GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
        GridReportFrm.Filter_IniGrid()
        Dim CRep As ClsSaleEnquiryMapping = New ClsSaleEnquiryMapping(GridReportFrm)
        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
        CRep.Ini_Grid()
        ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
        GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 0).Value = AgL.PubLoginDate
        GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 1).Value = AgL.PubLoginDate
        GridReportFrm.MdiParent = Me.MdiParent
        GridReportFrm.Show()
        CRep.ProcSaleEnquiryMapping()
    End Sub
    Private Sub FrmSaleInvoiceDirect_BaseEvent_Topctrl_tbMore() Handles Me.BaseEvent_Topctrl_tbMore
        MnuOptions.Show(Topctrl1, Topctrl1.btbSite.Rectangle.X, Topctrl1.btbSite.Rectangle.Y + Topctrl1.btbSite.Rectangle.Size.Height)
    End Sub
    Public Sub FCreateSaleOrder(mDocId As String, mSr As Integer, mRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim mDescription As String = ""
        Dim mSaleOrderDocId As String = ""
        Dim mV_Type As String = ""
        Dim mV_No As String
        Dim mV_Prefix As String
        Dim mSaleOrderSr As Integer = 0

        'mV_Type = FGetSettings(SettingFields.GeneratedEntryV_Type, SettingType.General)
        If mV_Type = "" Then
            mV_Type = Ncat.SaleOrder
        End If

        If AgL.XNull(Dgl1.Item(Col1SKU, mRowIndex).Value) <> "" Then
            If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                                From SaleEnquiryMapping With (NoLock)
                                Where DocId = " & AgL.Chk_Text(mDocId) & "
                                And Sr = " & Val(mSr) & "", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                mQry = "INSERT INTO SaleEnquiryMapping (DocID, Sr, Item, Specification)
                            SELECT " & AgL.Chk_Text(mDocId) & ", " & mSr & " Sr, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1SKU, mRowIndex).Tag) & " Item, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Specification, mRowIndex).Value) & " Specification "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = "INSERT INTO SaleEnquiryMappingSku (DocID, Sr, ItemCategory, ItemGroup, Item, 
                            Dimension1, Dimension2, Dimension3, Dimension4, Size)
                            SELECT " & AgL.Chk_Text(mDocId) & ", " & mSr & " Sr, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, mRowIndex).Tag) & " ItemCategory, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, mRowIndex).Tag) & " ItemGroup, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Item, mRowIndex).Tag) & " Item, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, mRowIndex).Tag) & " Dimension1, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, mRowIndex).Tag) & " Dimension2, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, mRowIndex).Tag) & " Dimension3, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, mRowIndex).Tag) & " Dimension4, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Size, mRowIndex).Tag) & " Size "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            Else
                mQry = "UPDATE SaleEnquiryMapping 
                            Set 
                            Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, mRowIndex).Tag) & ", 
                            Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, mRowIndex).Value) & " 
                            Where DocId = " & AgL.Chk_Text(mDocId) & " 
                            And Sr = " & mSr & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = "UPDATE SaleEnquiryMappingSku 
                            Set 
                            ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, mRowIndex).Tag) & ", 
                            ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, mRowIndex).Tag) & ", 
                            Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, mRowIndex).Tag) & ", 
                            Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, mRowIndex).Tag) & ", 
                            Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, mRowIndex).Tag) & ", 
                            Dimension3 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, mRowIndex).Tag) & ", 
                            Dimension4 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, mRowIndex).Tag) & ",
                            Size = " & AgL.Chk_Text(Dgl1.Item(Col1Size, mRowIndex).Tag) & "  
                            Where DocId = " & AgL.Chk_Text(mDocId) & " 
                            And Sr = " & mSr & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            mQry = "SELECT DocID FROM SaleInvoice WITH (Nolock) 
                            WHERE GenDocId =" & AgL.Chk_Text(mDocId) & ""
            mSaleOrderDocId = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar)

            If mSaleOrderDocId = "" Then
                'mSaleOrderDocId = AgL.GetDocId(mV_Type, CStr(0), CDate(DglMain.Item(Col1Value, rowV_Date).Value), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                mSaleOrderDocId = AgL.CreateDocId(AgL, "SaleInvoice", mV_Type, CStr(0), CDate(DglMain.Item(Col1Value, rowV_Date).Value), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                mV_No = Val(AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                mV_Prefix = AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
                mQry = "INSERT INTO SaleInvoice (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, 
                                ManualRefNo, SaleToParty, BillToParty,  Agent, SaleToPartyName, SaleToPartyAddress, SaleToPartyPinCode, 
                                SaleToPartyCity, SaleToPartyMobile, SaleToPartySalesTaxNo, SaleToPartyDocNo, 
                                SaleToPartyDocDate, Remarks, TermsAndConditions, Status, EntryBy, EntryDate, 
                                SpecialDiscount_Per, SpecialDiscount, DeliveryDate, GenDocId, LockText)
                                SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & AgL.Chk_Text(mV_Type) & ", 
                                " & AgL.Chk_Text(mV_No) & ", H.V_Date, " & AgL.Chk_Text(mV_Prefix) & ", H.Div_Code, 
                                H.Site_Code, H.ManualRefNo, H.SaleToParty, H.SaleToParty As BillToParty, H.Agent, H.SaleToPartyName, 
                                H.SaleToPartyAddress, H.SaleToPartyPinCode, H.SaleToPartyCity, H.SaleToPartyMobile, 
                                H.SaleToPartySalesTaxNo, H.SaleToPartyDocNo, H.SaleToPartyDocDate, H.Remarks, 
                                H.TermsAndConditions, 'Active' Status, EntryBy, EntryDate, 0 SpecialDiscount_Per, 
                                0 SpecialDiscount, H.DeliveryDate, H.DocID As GenDocId, 'Createed From Sale Enquiry.'
                                FROM SaleEnquiry H WHERE H.DocID =" & AgL.Chk_Text(mDocId) & ""
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                AgL.UpdateVoucherCounter(mSaleOrderDocId, CDate(DglMain.Item(Col1Value, rowV_Date).Value), Conn, Cmd, AgL.PubDivCode, AgL.PubSiteCode)
            Else
                mQry = " Update SaleInvoice " &
                            " SET  " &
                            " SaleToParty = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " &
                            " BillToParty = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " &
                            " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                            " DeliveryDate = " & AgL.Chk_Date(Dgl2.Item(Col1Value, rowDeliveryDate).Value) & ", " &
                            " SaleToPartyDocNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowPartyDocNo).Value) & ", " &
                            " SaleToPartyDocDate = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowPartyDocDate).Value) & ", " &
                            " Agent = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowAgent).Tag) & ", " &
                            " Remarks =  " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowRemarks).Value) & ", " &
                            " TermsAndConditions =  " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowTermsAndConditions).Value) & " " &
                            " Where DocId = '" & mSaleOrderDocId & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            End If

            If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                                From SaleInvoiceDetail With (NoLock)
                                Where GenDocId = " & AgL.Chk_Text(mDocId) & "
                                And GenDocIdSr = " & Val(mSr) & "", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()) = 0 Then

                mSaleOrderSr = AgL.VNull(AgL.Dman_Execute("Select IsNull(Max(Sr),0) + 1 From SaleInvoiceDetail With (NoLock)
                                    Where DOcID = " & AgL.Chk_Text(mSaleOrderDocId) & "", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())

                mQry = "INSERT INTO SaleInvoiceDetail (DocID, Sr, Item, Specification, 
                            Pcs, DocQty, Qty, Unit, UnitMultiplier, 
                            DocDealQty, DealQty, DealUnit, Rate, Amount, Remark, 
                            SaleInvoice, SaleInvoiceSr, GenDocId, GenDocIdSr)
                            SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & mSaleOrderSr & " Sr, 
                            Sem.Item As Item, Sem.Specification As Specification, 
                            L.Qty As Pcs, L.Qty As DocQty, L.Qty As Qty, 'Pcs' As Unit, 1 As UnitMultiplier, 1 As DocDealQty, 
                            1 As DealQty, 'Pcs' As DealUnit, L.Rate, L.Amount, L.Remark, 
                            " & AgL.Chk_Text(mSaleOrderDocId) & ", " & mSaleOrderSr & " Sr, 
                            L.Docid GenDocId, L.Sr GenDocIdSr
                            FROM SaleEnquiryDetail L 
                            LEFT JOIN SaleEnquiryMapping Sem oN L.DocId = Sem.Docid And L.Sr = Sem.Sr
                            WHERE L.DocID =" & AgL.Chk_Text(mDocId) & " 
                            AND L.Sr =" & mSr & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = "Insert Into SaleInvoiceDetailSku
                        (DocId, Sr, ItemCategory, ItemGroup, Item, Dimension1, 
                        Dimension2, Dimension3, Dimension4, Size) "
                mQry += " Values(" & AgL.Chk_Text(mSaleOrderDocId) & ", " & mSaleOrderSr & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, mRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, mRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, mRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, mRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, mRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, mRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, mRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Size, mRowIndex).Tag) & ")"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Else
                mQry = "UPDATE SaleInvoiceDetail 
                            Set 
                            Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, mRowIndex).Tag) & ", 
                            Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, mRowIndex).Value) & ", 
                            DocQty = " & Val(Dgl1.Item(Col1Qty, mRowIndex).Value) & ", 
                            Qty = " & Val(Dgl1.Item(Col1Qty, mRowIndex).Value) & ", 
                            Rate = " & Val(Dgl1.Item(Col1Rate, mRowIndex).Value) & ", 
                            Amount = " & Val(Dgl1.Item(Col1Amount, mRowIndex).Value) & " 
                            Where GenDocId = " & AgL.Chk_Text(mDocId) & " 
                            And GenDocIdSr = " & mSr & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Select L.DocId, L.Sr From SaleInvoiceDetail L With (NoLock)
                            Where L.GenDocId = " & AgL.Chk_Text(mDocId) & " 
                            And GenDocIdSr = " & mSr & " "
                Dim DtSaleInvoiceDetail As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "",AgL.GCn,AgL.GcnRead)).Tables(0)

                mQry = "Update SaleInvoiceDetailSku " &
                        " SET ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, mRowIndex).Tag) & ", " &
                        " ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, mRowIndex).Tag) & ", " &
                        " Item = " & AgL.Chk_Text(Dgl1.Item(Col1ITem, mRowIndex).Tag) & ", " &
                        " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, mRowIndex).Tag) & ", " &
                        " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, mRowIndex).Tag) & ", " &
                        " Dimension3 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, mRowIndex).Tag) & ", " &
                        " Dimension4 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, mRowIndex).Tag) & ", " &
                        " Size = " & AgL.Chk_Text(Dgl1.Item(Col1Size, mRowIndex).Tag) & " " &
                        " Where DocId = '" & AgL.XNull(DtSaleInvoiceDetail.Rows(0)("DocId")) & "' " &
                        " And Sr = " & AgL.VNull(DtSaleInvoiceDetail.Rows(0)("Sr")) & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Else
            If AgL.XNull(Dgl1.Item(Col1SaleEnquiryMappingDocId, mRowIndex).Value) <> "" Then
                mQry = " Delete From SaleEnquiryMappingSku
                            Where DocId = " & AgL.Chk_Text(mDocId) & "
                            And Sr = " & mSr & ""
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " Delete From SaleEnquiryMapping 
                            Where DocId = " & AgL.Chk_Text(mDocId) & "
                            And Sr = " & mSr & ""
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            If AgL.XNull(Dgl1.Item(Col1SaleOrderDocId, mRowIndex).Value) <> "" Then
                mQry = " Select L.DocId, L.Sr From SaleInvoiceDetail L 
                            Where L.GenDocId = " & AgL.Chk_Text(mDocId) & " 
                            And GenDocIdSr = " & mSr & " "
                Dim DtSaleInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                mQry = " Delete From SaleInvoiceDetailSku 
                            Where DocId = '" & AgL.XNull(DtSaleInvoiceDetail.Rows(0)("DocId")) & "' 
                            And Sr = " & AgL.VNull(DtSaleInvoiceDetail.Rows(0)("Sr")) & " "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                mQry = " Delete From SaleInvoiceDetail 
                            Where GenDocId = " & AgL.Chk_Text(mDocId) & "
                            And GenDocIdSr = " & mSr & ""
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag, VoucherCategory.Sales, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", "")
        FGetSettings = mValue
    End Function

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
    'Public Sub FPostMapping(bDocId As String, bSr As Integer, LineGridRowIndex As Integer, Conn As Object, Cmd As Object)
    '    Dim mCode As Integer = 0
    '    Dim mPrimaryCode As Integer = 0
    '    Dim mTrans As String = ""
    '    Dim DtTemp As DataTable = Nothing
    '    Dim mDescription As String = ""
    '    Dim mSaleOrderDocId As String = ""
    '    Dim mV_Type As String = Ncat.SaleOrder
    '    Dim mV_No As String
    '    Dim mV_Prefix As String
    '    Dim mSr As Integer = 0

    '    If AgL.XNull(Dgl1.Item(Col1SKU, LineGridRowIndex).Value) <> "" Then
    '        If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
    '                            From SaleEnquiryMapping With (NoLock)
    '                            Where DocId = " & AgL.Chk_Text(bDocId) & "
    '                            And Sr = " & Val(bSr) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
    '            mQry = "INSERT INTO SaleEnquiryMapping (DocID, Sr, Item)
    '                        SELECT " & AgL.Chk_Text(bDocId) & ", " & bSr & " Sr, 
    '                        " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Value) & " Item
    '                        FROM SaleEnquiryDetail L 
    '                        WHERE L.DocID =" & AgL.Chk_Text(bDocId) & " 
    '                        AND L.Sr =" & bSr & " "
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    '        Else
    '            mQry = "UPDATE SaleEnquiryMapping 
    '                        Set 
    '                        Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Value) & " 
    '                        Where DocId = " & AgL.Chk_Text(bDocId) & " 
    '                        And Sr = " & bSr & " "
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    '        End If

    '        mQry = "SELECT DocID FROM SaleInvoice WITH (Nolock) 
    '                        WHERE GenDocId =" & AgL.Chk_Text(bDocId) & ""
    '        mSaleOrderDocId = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

    '        If mSaleOrderDocId = "" Then
    '            mSaleOrderDocId = AgL.GetDocId(mV_Type, CStr(0), CDate(DglMain.Item(Col1Value, rowV_Date).Value), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
    '            mV_No = Val(AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
    '            mV_Prefix = AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
    '            mQry = "INSERT INTO SaleInvoice (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, 
    '                ManualRefNo, SaleToParty, BillToParty,  Agent, SaleToPartyName, SaleToPartyAddress, SaleToPartyPinCode, 
    '                SaleToPartyCity, SaleToPartyMobile, SaleToPartySalesTaxNo, SaleToPartyDocNo, 
    '                SaleToPartyDocDate, Remarks, TermsAndConditions, Status, EntryBy, EntryDate, 
    '                SpecialDiscount_Per, SpecialDiscount, DeliveryDate, GenDocId)
    '                SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & AgL.Chk_Text(mV_Type) & ", 
    '                " & AgL.Chk_Text(mV_No) & ", H.V_Date, " & AgL.Chk_Text(mV_Prefix) & ", H.Div_Code, 
    '                H.Site_Code, H.ManualRefNo, H.SaleToParty, H.SaleToParty As BillToParty, H.Agent, H.SaleToPartyName, 
    '                H.SaleToPartyAddress, H.SaleToPartyPinCode, H.SaleToPartyCity, H.SaleToPartyMobile, 
    '                H.SaleToPartySalesTaxNo, H.SaleToPartyDocNo, H.SaleToPartyDocDate, H.Remarks, 
    '                H.TermsAndConditions, 'Active' Status, EntryBy, EntryDate, 0 SpecialDiscount_Per, 
    '                0 SpecialDiscount, H.DeliveryDate, H.DocID As GenDocId
    '                FROM SaleEnquiry H WHERE H.DocID =" & AgL.Chk_Text(bDocId) & ""
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    '            AgL.UpdateVoucherCounter(mSaleOrderDocId, CDate(DglMain.Item(Col1Value, rowV_Date).Value), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
    '        End If

    '        If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
    '                            From SaleInvoiceDetail With (NoLock)
    '                            Where GenDocId = " & AgL.Chk_Text(bDocId) & "
    '                            And GenDocIdSr = " & Val(bSr) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then

    '            mSr = AgL.VNull(AgL.Dman_Execute("Select IsNull(Max(Sr),0) + 1 From SaleInvoiceDetail With (NoLock)
    '                                Where DOcID = " & AgL.Chk_Text(mSaleOrderDocId) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

    '            mQry = "INSERT INTO SaleInvoiceDetail (DocID, Sr, Item, Pcs, DocQty, Qty, Unit, UnitMultiplier, 
    '                        DocDealQty, DealQty, DealUnit, Rate, Amount, Remark, GenDocId, GenDocIdSr, SaleInvoice, SaleInvoiceSr)
    '                        SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & mSr & " Sr, 
    '                        Sem.Item As Item, 
    '                        L.Qty As Pcs, L.Qty As DocQty, L.Qty As Qty, 'Pcs' As Unit, 1 As UnitMultiplier, 1 As DocDealQty, 
    '                        1 As DealQty, 'Pcs' As DealUnit, L.Rate, L.Amount, L.Remark, 
    '                        L.Docid GenDocId, L.Sr GenDocIdSr,
    '                        " & AgL.Chk_Text(mSaleOrderDocId) & " As SaleInvoice, " & mSr & " SaleInvoiceSr
    '                        FROM SaleEnquiryDetail L 
    '                        LEFT JOIN SaleEnquiryMapping Sem oN L.DocId = Sem.Docid And L.Sr = Sem.Sr
    '                        WHERE L.DocID =" & AgL.Chk_Text(bDocId) & " 
    '                        AND L.Sr =" & bSr & " "
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


    '            mQry = "Insert Into SaleInvoiceDetailSku
    '                    (DocId, Sr, ItemCategory, ItemGroup, Item, Dimension1, 
    '                    Dimension2, Dimension3, Dimension4, Size) "
    '            mQry += " Values(" & AgL.Chk_Text(bDocId) & ", " & bSr & ", " &
    '                    " " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & ", " &
    '                    " " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & ", " &
    '                    " " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
    '                    " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & ", " &
    '                    " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & ", " &
    '                    " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & ", " &
    '                    " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & ", " &
    '                    " " & AgL.Chk_Text(Dgl1.Item(Col1Size, LineGridRowIndex).Tag) & ")"
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        Else
    '            mQry = "UPDATE SaleInvoiceDetail 
    '                        Set 
    '                        Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Value) & " 
    '                        Where GenDocId = " & AgL.Chk_Text(bDocId) & " 
    '                        And GenDocIdSr = " & bSr & " "
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    '            mQry = " Select L.DocId, L.Sr From SaleInvoiceDetail L 
    '                        Where L.GenDocId = " & AgL.Chk_Text(bDocId) & " 
    '                        And GenDocIdSr = " & bSr & " "
    '            Dim DtSaleInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '            mQry = "Update SaleInvoiceDetailSku " &
    '                    " SET ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & ", " &
    '                    " ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & ", " &
    '                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
    '                    " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & ", " &
    '                    " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & ", " &
    '                    " Dimension3 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & ", " &
    '                    " Dimension4 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & ", " &
    '                    " Size = " & AgL.Chk_Text(Dgl1.Item(Col1Size, LineGridRowIndex).Tag) & " " &
    '                    " Where DocId = '" & AgL.XNull(DtSaleInvoiceDetail.Rows(0)("DocId")) & "' " &
    '                    " And Sr = " & AgL.VNull(DtSaleInvoiceDetail.Rows(0)("Sr")) & " "
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        End If
    '    Else
    '        If AgL.XNull(Dgl1.Item(Col1SaleEnquiryMappingDocId, LineGridRowIndex).Value) <> "" Then
    '            mQry = " Delete From SaleEnquiryMapping 
    '                        Where DocId = " & AgL.Chk_Text(bDocId) & "
    '                        And Sr = " & bSr & ""
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    '        End If

    '        If AgL.XNull(Dgl1.Item(Col1SaleOrderDocId, LineGridRowIndex).Value) <> "" Then
    '            mQry = " Select L.DocId, L.Sr From SaleInvoiceDetail L 
    '                        Where L.GenDocId = " & AgL.Chk_Text(bDocId) & " 
    '                        And GenDocIdSr = " & bSr & " "
    '            Dim DtSaleInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '            mQry = " Delete From SaleInvoiceDetailSku 
    '                        Where DocId = '" & AgL.XNull(DtSaleInvoiceDetail.Rows(0)("DocId")) & "' 
    '                        And Sr = " & AgL.VNull(DtSaleInvoiceDetail.Rows(0)("Sr")) & " "
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    '            mQry = " Delete From SaleInvoiceDetail 
    '                        Where GenDocId = " & AgL.Chk_Text(bDocId) & "
    '                        And GenDocIdSr = " & bSr & ""
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    '        End If
    '    End If
    'End Sub
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
        Else
            Dgl1.Item(Col1SKU, bRowIndex).Value = ""
        End If
    End Sub
    Private Sub FrmSaleEnquiry_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim bSaleOrderDocId = AgL.xNull(AgL.Dman_Execute(" Select DocId From SaleInvoice Where GenDocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
        If bSaleOrderDocId <> "" Then
            mQry = " Delete From SaleInvoiceDetailSku Where DocId = '" & bSaleOrderDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleInvoiceDetail Where DocId = '" & bSaleOrderDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleInvoice Where DocId = '" & bSaleOrderDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Function FGetRelationalData() As Boolean
        Try
            Dim mGeneratedSaleOrder As String = AgL.XNull(AgL.Dman_Execute("Select DocId 
                                From SaleOrder 
                                Where GenDocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            If mGeneratedSaleOrder <> "" Then

            End If

            Dim bRData As String
            '// Check for relational data in PurchPlan
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo 
                        FROM PurchPlan H WITH (NoLock)
                        LEFT JOIN PurchPlanDetailBaseSaleOrder L WITH (NoLock) ON H.DocID = L.GenDocId 
                        WHERE L.SaleInvoice = '" & mGeneratedSaleOrder & "' ) AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox(" Plan " & bRData & " created against Enquiry No. " & DglMain.Item(Col1Value, rowV_Type).Tag & "-" & DglMain.Item(Col1Value, rowReferenceNo).Value & ". Can't Modify Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
            FGetRelationalData = True
        End Try
    End Function
    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub
    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = Not FGetRelationalData()
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
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_ItemCategory Function ")
        End Try
    End Sub
End Class
