Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq
Imports Customised.ClsMain

Public Class FrmPurchPlan
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1Process As String = "Process"
    Public Col1ItemType As String = AgL.PubCaptionItemType
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
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1UnitMultiplier As String = "Unit Multiplier"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1DealUnit As String = "Deal Unit"
    Public Const Col1DealUnitDecimalPlaces As String = "Deal Decimal Places"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1BtnBasePlanDetail As String = "Base Plan Detail"
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

    Dim rowProcess As Integer = 6
    Dim rowResponsiblePerson As Integer = 7
    Dim rowRemarks As Integer = 8
    Dim rowRemarks1 As Integer = 9
    Dim rowRemarks2 As Integer = 10

    Public Const hcProcess As String = "Process"
    Public Const hcResponsiblePerson As String = "Responsible Person"
    Public Const hcRemarks As String = "Remarks"
    Public Const hcRemarks1 As String = "Remarks1"
    Public Const hcRemarks2 As String = "Remarks2"

    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay


    Public Shared mFlag_Import As Boolean = False
    Dim mPrevRowIndex As Integer = 0
    Dim Dgl As New AgControls.AgDataGrid
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuHistory As ToolStripMenuItem
    Friend WithEvents MnuReport As ToolStripMenuItem
    Friend WithEvents MnuWizard As ToolStripMenuItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPurchPlan))
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
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblDealQty = New System.Windows.Forms.Label()
        Me.LblDealQtyText = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuWizard = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.TabControl1.Size = New System.Drawing.Size(992, 164)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LblNature)
        Me.TP1.Controls.Add(Me.Panel3)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 138)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblNCatNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblNature, 0)
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
        Me.PnlMain.Size = New System.Drawing.Size(983, 134)
        Me.PnlMain.TabIndex = 0
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
        Me.Pnl1.Location = New System.Drawing.Point(1, 201)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(980, 354)
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
        Me.LinkLabel1.Location = New System.Drawing.Point(1, 180)
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
        Me.BtnAttachments.Location = New System.Drawing.Point(138, 595)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(93, 23)
        Me.BtnAttachments.TabIndex = 3020
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblDealQty)
        Me.PnlTotals.Controls.Add(Me.LblDealQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Location = New System.Drawing.Point(7, 555)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 695
        '
        'LblDealQty
        '
        Me.LblDealQty.AutoSize = True
        Me.LblDealQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblDealQty.Location = New System.Drawing.Point(411, 3)
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
        Me.LblDealQtyText.Location = New System.Drawing.Point(300, 3)
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
        Me.LblTotalQty.Location = New System.Drawing.Point(116, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(31, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
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
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuHistory, Me.MnuWizard, Me.MnuReport})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(153, 92)
        '
        'MnuHistory
        '
        Me.MnuHistory.Name = "MnuHistory"
        Me.MnuHistory.Size = New System.Drawing.Size(152, 22)
        Me.MnuHistory.Text = "History"
        '
        'MnuWizard
        '
        Me.MnuWizard.Name = "MnuWizard"
        Me.MnuWizard.Size = New System.Drawing.Size(152, 22)
        Me.MnuWizard.Text = "Wizard"
        '
        'MnuReport
        '
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(152, 22)
        Me.MnuReport.Text = "Report"
        '
        'FrmPurchPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmPurchPlan"
        Me.Text = "PurchPlan Entry"
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
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
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
    Public WithEvents PnlCustomGrid As Panel
    Protected WithEvents BtnAttachments As Button
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalQty As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LblDealQty As Label
    Public WithEvents LblDealQtyText As Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchPlan"
        LogTableName = "PurchPlan_Log"
        MainLineTableCsv = "PurchPlanDetail,PurchPlanDetailBase,PurchPlanDetailSku"
        LogLineTableCsv = "PurchPlanDetail_Log,PurchPlanDetailBase_Log,PurchPlanDetailSku_Log"

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
                From PurchPlan H  With (NoLock)
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

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [PurchPlan_Type], Cast(strftime('%d/%m/%Y', H.V_Date) As nvarchar) AS Date, " &
                            " H.ManualRefNo AS [Manual_No], H.Remarks,  " &
                            " H.EntryBy AS [Entry_By], Cast(strftime('%d/%m/%Y', H.EntryDate) As nvarchar) AS [Entry_Date] " &
                            " FROM PurchPlan H  With (NoLock) " &
                            " LEFT JOIN Voucher_Type Vt  With (NoLock) ON H.V_Type = Vt.V_Type " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Process, 100, 0, Col1Process, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, Col1ItemType, False, False)
            .AddAgTextColumn(Dgl1, Col1SKU, 300, 0, Col1SKU, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 400, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 130, 0, Col1Specification, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1UnitMultiplier, 70, 8, 4, False, Col1UnitMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DealQty, 70, 8, 3, False, Col1DealQty, False, True, True)
            .AddAgTextColumn(Dgl1, Col1DealUnit, 60, 0, Col1DealUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1DealUnitDecimalPlaces, 50, 0, Col1DealUnitDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgButtonColumn(Dgl1, Col1BtnBasePlanDetail, 45, Col1BtnBasePlanDetail, True, False)
            .AddAgTextColumn(Dgl1, Col1IsRecordLocked, 150, 255, Col1IsRecordLocked, False, False)

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
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.BackgroundColor = Me.BackColor

        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)

        DglMain.Rows.Add(5)
        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next

        DglMain.Columns(Col1Head).Width = 300
        DglMain.Columns(Col1Value).Width = 650

        DglMain.Item(Col1Head, rowProcess).Value = hcProcess
        DglMain.Item(Col1Head, rowResponsiblePerson).Value = hcResponsiblePerson
        DglMain.Item(Col1Head, rowRemarks).Value = hcRemarks
        DglMain.Item(Col1Head, rowRemarks1).Value = hcRemarks1
        DglMain.Item(Col1Head, rowRemarks2).Value = hcRemarks2
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCustomGrid1.Name = "AgCustomGrid1"

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bPurchPlanSelectionQry$ = "", bHelpValuesSelectionQry$ = ""

        mQry = " Update PurchPlan " &
                " SET  " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " ResponsiblePerson = " & AgL.Chk_Text(DglMain(Col1Value, rowResponsiblePerson).Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(DglMain(Col1Value, rowRemarks).Value) & ", " &
                " Remarks1 = " & AgL.Chk_Text(DglMain(Col1Value, rowRemarks1).Value) & ", " &
                " Remarks2 = " & AgL.Chk_Text(DglMain(Col1Value, rowRemarks2).Value) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchPlanDetail  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1SKU, I).Tag <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    InsertPurchPlanDetail(mSearchCode, mSr, I, Conn, Cmd)
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        UpdatePurchPlanDetail(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
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
            mQry = " Delete From PurchPlanDetailSku Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From PurchPlanDetail Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertPurchPlanDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into PurchPlanDetail(DocId, Sr, Process, Item, 
                           Specification, Qty, Unit, UnitMultiplier, DealUnit, DealQty, Remark) "
        mQry += " Values( " & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Process, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & " " &
                        " ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO PurchPlanDetailSku (DocID, Sr, ItemCategory, ItemGroup, Item, 
                            Dimension1, Dimension2, Dimension3, Dimension4, Size)
                            Select " & AgL.Chk_Text(DocID) & ", " & Sr & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, LineGridRowIndex).Tag) & " ItemCategory, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, LineGridRowIndex).Tag) & " ItemGroup, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & " Item, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Tag) & " Dimension1, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Tag) & " Dimension2, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Tag) & " Dimension3, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Tag) & " Dimension4, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Size, LineGridRowIndex).Tag) & " Size "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    End Sub
    Private Sub UpdatePurchPlanDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Dgl1.Rows(LineGridRowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
            mQry = " UPDATE PurchPlanDetail " &
                    " Set " &
                    " Process = " & AgL.Chk_Text(Dgl1.Item(Col1Process, LineGridRowIndex).Tag) & ", " &
                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1SKU, LineGridRowIndex).Tag) & ", " &
                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", " &
                    " Qty = " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " &
                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ", " &
                    " UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ", " &
                    " DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " &
                    " DealQty = " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", " &
                    " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & " " &
                    " Where DocId = '" & mSearchCode & "' " &
                    " And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag & " "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Update PurchPlanDetailSku " &
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
    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDglMainRowCount As Integer
        Try
            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & DglMain.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To DglMain.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DglMain.Item(Col1Head, J).Value Then
                            DglMain.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglMainRowCount += 1
                            DglMain.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                DglMain.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If
            If mDglMainRowCount = 0 Then DglMain.Visible = False Else DglMain.Visible = True



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
                            Dgl1.Columns(J).ReadOnly = Not AgL.VNull(DtTemp.Rows(I)("IsEditable"))
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

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False
        Dim mQryStockSr As String

        Dim DsMain As DataSet



        LblTotalQty.Text = 0
        LblDealQty.Text = 0

        mQry = " Select H.*, ResponsiblePerson.Name As ResponsiblePersonDesc 
                From (Select * From PurchPlan With (NoLock) Where DocID='" & SearchCode & "') H 
                LEFT JOIN ViewHelpSubGroup ResponsiblePerson With (NoLock) On H.ResponsiblePerson = ResponsiblePerson.Code "
        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))

                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))



                DglMain(Col1Value, rowResponsiblePerson).Tag = AgL.XNull(.Rows(0)("ResponsiblePerson"))
                DglMain(Col1Value, rowResponsiblePerson).Value = AgL.XNull(.Rows(0)("ResponsiblePersonDesc"))

                DglMain(Col1Value, rowRemarks).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks")))
                DglMain(Col1Value, rowRemarks1).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks1")))
                DglMain(Col1Value, rowRemarks2).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks2")))

                AgCustomGrid1.FMoveRecFooterTable(DsMain.Tables(0))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select L.*, Prc.Name As ProcessDesc,
                        I.Description As ItemDesc, I.ManualCode, 
                        U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, U.ShowDimensionDetailInPurchase,
                        MU.DecimalPlaces As DealUnitDecimalPlaces,
                        Sku.Code As SkuCode, Sku.Description As SkuDescription, 
                        It.Code As ItemType, It.Name As ItemTypeDesc,
                        IG.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, 
                        Pids.Item As ItemCode, Pids.ItemCategory, Pids.ItemGroup, 
                        Pids.Dimension1, Pids.Dimension2, 
                        Pids.Dimension3, Pids.Dimension4, Pids.Size, 
                        D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                        D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                        I.ItemCategory as MItemCategory, I.ItemGroup as MItemGroup, I.Specification as MItemSpecification, 
                        I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MSize 
                        From (Select * From PurchPlanDetail  With (NoLock)  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN PurchPlanDetailSku Pids With (NoLock) On L.DocId = Pids.DocId And L.Sr = Pids.Sr
                        LEFT JOIN SubGroup Prc On L.Process = Prc.SubCode
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
                        Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                        Left Join Unit MU  With (NoLock) On L.DealUnit = MU.Code 
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

                            Dgl1.Item(Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.Item(Col1Process, I).Value = AgL.XNull(.Rows(I)("ProcessDesc"))

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

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))


                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))

                            Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DealUnitDecimalPlaces"))
                            Dgl1.Item(Col1UnitMultiplier, I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                            Dgl1.Item(Col1DealQty, I).Value = Format(AgL.VNull(.Rows(I)("DealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))

                            Dgl1.Item(Col1MItemCategory, I).Tag = AgL.XNull(.Rows(I)("MItemCategory"))
                            Dgl1.Item(Col1MItemGroup, I).Tag = AgL.XNull(.Rows(I)("MItemGroup"))
                            Dgl1.Item(Col1MItemSpecification, I).Value = AgL.XNull(.Rows(I)("MItemSpecification"))
                            Dgl1.Item(Col1MDimension1, I).Tag = AgL.XNull(.Rows(I)("MDimension1"))
                            Dgl1.Item(Col1MDimension2, I).Tag = AgL.XNull(.Rows(I)("MDimension2"))
                            Dgl1.Item(Col1MDimension3, I).Tag = AgL.XNull(.Rows(I)("MDimension3"))
                            Dgl1.Item(Col1MDimension4, I).Tag = AgL.XNull(.Rows(I)("MDimension4"))
                            Dgl1.Item(Col1MSize, I).Tag = AgL.XNull(.Rows(I)("MSize"))

                            If Val(Dgl1.Item(Col1IsRecordLocked, I).Value) > 0 Then
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True
                            End If

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                        Next I
                    End If
                End With

                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

                If AgL.Dman_Execute("Select Count(Distinct Process) From PurchPlanDetail Where DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
                    DglMain.Item(Col1Value, rowProcess).Tag = Dgl1.Item(Col1Process, 0).Tag
                    DglMain.Item(Col1Value, rowProcess).Value = Dgl1.Item(Col1Process, 0).Value
                End If

                '-------------------------------------------------------------
            End If
        End With
        ApplyUISettings(LblV_Type.Tag)
        SetAttachmentCaption()
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub
    Private Sub FrmPurchPlanEntry_BaseEvent_DglMainEditingControlValidating(sender As Object, e As CancelEventArgs) Handles Me.BaseEvent_DglMainEditingControlValidating
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
                ApplyUISettings(LblV_Type.Tag)
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchPlan", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

            Case rowProcess
                For I As Integer = 0 To Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Process, I).Tag = DglMain.Item(Col1Value, rowProcess).Tag
                    Dgl1.Item(Col1Process, I).Value = DglMain.Item(Col1Value, rowProcess).Value
                Next
        End Select
    End Sub
    Private Sub FrmPurchPlanEntry_BaseEvent_DglMainEditingControlKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainEditingControlKeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex
            Select Case mRow
                Case rowProcess
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpProcess()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowResponsiblePerson
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Sg.Code, Sg.Name From viewHelpSubgroup Sg  With (NoLock) 
                                    Left Join HRM_Employee Emp On Sg.Code = Emp.Subcode 
                                    Where sg.SubgroupType ='" & SubgroupType.Employee & "' 
                                    And Emp.RelievingDate Is Null 
                                    And Site_Code = '" & DglMain.Item(Col1Value, rowSite_Code).Tag & "' 
                                    Order By sg.Name "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        IniGrid()
        ApplyUISettings(LblV_Type.Tag)
        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchPlan", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

        Dgl1.ReadOnly = False



        'DglMain.CurrentCell = DglMain.Item(Col1Value, rowRemarks)
        DglMain.Focus()

        SetAttachmentCaption()
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean

        Dgl1.EndEdit()
        DglMain.EndEdit()


        'If AgL.RequiredField(TxtParty, LblBuyer.Text) Then passed = False : Exit Sub

        'If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub


        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1Item, I).Value <> "" Then

                        If Val(Dgl1(ColSNo, I).Tag) > 0 Then
                            If Dgl1(Col1Item, I).Value = "" Then
                                MsgBox("Item is blank at Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                .CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                        End If


                        If Val(.Item(Col1Qty, I).Value) = 0 Then
                            MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If

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
                                           , Dgl1.Item(Col1MItemCategory, I).Value _
                                           , Dgl1.Item(Col1MItemGroup, I).Value _
                                           , Dgl1.Item(Col1MItemSpecification, I).Value _
                                           , Dgl1.Item(Col1MDimension1, I).Value _
                                           , Dgl1.Item(Col1MDimension2, I).Value _
                                           , Dgl1.Item(Col1MDimension3, I).Value _
                                           , Dgl1.Item(Col1MDimension4, I).Value _
                                           , Dgl1.Item(Col1MSize, I).Value
                                           )
                        If Dgl1.Item(Col1SKU, I).Tag = "" Then
                            passed = False
                            Exit Sub
                        End If
                    End If
                End If
            Next
        End With

        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "SaleInvoice",
                                    DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                    DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                    DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)

        If Not CheckDuplicateRef Then
            DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleInvoice", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If
        Try
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty
                    If LblV_Type.Tag = Ncat.LrEntry Then
                        CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = 3
                    End If
            End Select
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
    Private Sub FrmPurchPlanEntry_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, i).Tag = Nothing
        Next
    End Sub
    Private Sub FrmSaleQuotation_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        Dim i As Integer

        GBoxImportFromExcel.Enabled = False

        If Dgl1.Columns.Count > 0 Then
            If Dgl1.Columns(Col1DealQty).Visible = False Then
                LblDealQty.Visible = False
                LblDealQtyText.Visible = False
            End If
        End If


        For i = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(i).DefaultCellStyle.BackColor = Dgl1.AgReadOnlyColumnColor Then
                Dgl1.Columns(i).ReadOnly = True
            End If
        Next
    End Sub
    Private Sub FrmPurchPlanEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            Dgl1.CurrentCell = Dgl1.FirstDisplayedCell : Dgl1.Focus()
        End If
    End Sub
    Private Function FCreateHelpProcess() As DataSet
        Dim strCond As String = ""

        Dim FilterInclude_Process As String = FGetSettings(SettingFields.FilterInclude_Process, SettingType.General)
        If FilterInclude_Process <> "" Then
            strCond += " And CharIndex('+' || Sg.Code,'" & FilterInclude_Process & "') > 0 "
            strCond += " And CharIndex('-' || Sg.Code,'" & FilterInclude_Process & "') <= 0 "
        End If

        mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' 
                And IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpProcess = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Sub FCreateHelpItem(RowIndex As Integer)
        Dim strCond As String = ""
        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        Dim FilterInclude_ItemV_Type As String = FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General)
        If FilterInclude_ItemV_Type <> "" Then
            strCond += " And CharIndex('+' || I.V_Type,'" & FilterInclude_ItemV_Type & "') > 0 "
            strCond += " And CharIndex('-' || I.V_Type,'" & FilterInclude_ItemV_Type & "') <= 0 "
        End If

        Dim FilterInclude_ItemGroup As String = FGetSettings(SettingFields.FilterInclude_ItemGroup, SettingType.General)
        If FilterInclude_ItemGroup <> "" Then
            strCond += " And CharIndex('+' || I.ItemGroup,'" & FilterInclude_ItemGroup & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemGroup,'" & FilterInclude_ItemGroup & "') <= 0 "
        End If

        Dim FilterInclude_Item As String = FGetSettings(SettingFields.FilterInclude_Item, SettingType.General)
        If FilterInclude_Item <> "" Then
            strCond += " And CharIndex('+' || I.Code,'" & FilterInclude_Item & "') > 0 "
            strCond += " And CharIndex('-' || I.Code,'" & FilterInclude_Item & "') <= 0 "
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
    Private Sub FrmPurchPlanEntry_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
    End Sub

    Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor,
                         Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")

        FGetPrintCrystal(SearchCode, mPrintFor, IsPrintToPrinter, BulkCondStr)
    End Sub

    Sub FGetPrintCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer
        Dim sQry As String



        mPrintTitle = AgL.Dman_Execute("Select IfNull(PrintingDescription, Description) From Voucher_Type Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' ", AgL.GCn).ExecuteScalar()

        Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        Dim mDocReportFileName As String = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)




        Dim bPrimaryQry As String = ""
        If BulkCondStr <> "" Then
            bPrimaryQry = " Select * From SaleInvoice  With (NoLock) Where DocID In (" & BulkCondStr & ")"
            PrintingCopies = FGetSettings(SettingFields.PrintingBulkCopyCaptions, SettingType.General).ToString.Split(",")
        Else
            bPrimaryQry = " Select * From SaleInvoice  With (NoLock) Where DocID = '" & SearchCode & "'"
            PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        End If



        'PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")

        mQry = "" : sQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            '(Case When DP.Prefix Is Not Null Then DP.Prefix || H.ManualRefNo Else H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo End) as InvoiceNo, 

            mQry = mQry + "
                            Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, H.DocID, L.Sr, H.V_Date, H.DeliveryDate, VT.Description as Voucher_Type, VT.NCat,                                 
                            '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as DocNo, 
                            P.Name AS ProcessName, I.Description AS ItemName, I.Specification as ItemSpecification, D1.Specification AS Dimension1Name, D2.Specification AS Dimension2Name,
                            D3.Specification AS Dimension3Name, D4.Specification AS Dimension4Name, Size.Description AS SizeName,
                            IG.Description AS ItemGroupName, IC.Description AS ItemCategoryName, L.Specification, L.Qty, L.Unit, L.UnitMultiplier,
                            L.DealQty, L.DealUnit,
                            '" & AgL.PubCaptionDimension1 & "' as D1Caption, '" & AgL.PubCaptionDimension2 & "' as D2Caption, '" & AgL.PubCaptionDimension3 & "' as D3Caption, '" & AgL.PubCaptionDimension4 & "' as D4Caption,
                            '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                            '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle                                           
                            FROM PurchPlan H 
                            LEFT JOIN PurchPlanDetail L ON H.DocID = L.DocID 
                            LEFT JOIN PurchPlanDetailSku LS ON L.DocID = LS.DocID AND L.Sr = LS.Sr 
                            LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type 
                            LEFT JOIN subgroup P ON L.Process = P.Subcode 
                            LEFT JOIN Item I ON LS.Item = I.Code
                            LEFT JOIN Item D1 ON LS.Dimension1 = D1.Code 
                            LEFT JOIN Item D2 ON LS.Dimension2 = D2.Code 
                            LEFT JOIN Item D3 ON LS.Dimension3 = D3.Code 
                            LEFT JOIN Item D4 ON LS.Dimension4 = D4.Code 
                            LEFT JOIN Item Size ON LS.Size = Size.Code 
                            LEFT JOIN Item IG ON LS.ItemGroup = IG.Code 
                            LEFT JOIN Item IC ON LS.ItemCategory = IC.Code 
                            Where H.DocID = '" & mSearchCode & "'
                        "



            sQry = sQry + "Select '" & I & "' as Copies, Max(H.DocID) DocID,
                            Max(I.Description) AS ItemName, Max(D1.Description) AS Dimension1Name, Max(D2.Description) AS Dimension2Name,
                            Max(D3.Description) AS Dimension3Name, Max(D4.Description) AS Dimension4Name, Max(Size.Description) AS SizeName,
                            Max(IG.Description) AS ItemGroupName, Max(IC.Description) AS ItemCategoryName, Max(L.Specification) AS Specification, 
                            Max(L.Qty) AS Qty, Max(L.Unit) AS Unit, Max(L.UnitMultiplier) AS UnitMultiplier,
                            Max(L.DealQty) AS DealQty, Max(L.DealUnit) DealUnit,
                            '" & AgL.PubCaptionDimension1 & "' as D1Caption, '" & AgL.PubCaptionDimension2 & "' as D2Caption, '" & AgL.PubCaptionDimension3 & "' as D3Caption, '" & AgL.PubCaptionDimension4 & "' as D4Caption               
                            FROM PurchPlan H 
                            LEFT JOIN PurchPlanDetailBase LB ON H.DocID = LB.DocID 
                            LEFT JOIN PurchPlanDetail L ON LB.PurchPlan = L.DocID AND LB.PurchPlanSr = L.Sr 
                            LEFT JOIN PurchPlanDetailSku LS ON L.DocID = LS.DocID AND L.Sr = LS.Sr 
                            LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type 
                            LEFT JOIN subgroup P ON L.Process = P.Subcode 
                            LEFT JOIN Item I ON LS.Item = I.Code 
                            LEFT JOIN Item D1 ON LS.Dimension1 = D1.Code 
                            LEFT JOIN Item D2 ON LS.Dimension2 = D2.Code 
                            LEFT JOIN Item D3 ON LS.Dimension3 = D3.Code 
                            LEFT JOIN Item D4 ON LS.Dimension4 = D4.Code 
                            LEFT JOIN Item Size ON LS.Size = Size.Code 
                            LEFT JOIN Item IG ON LS.ItemGroup = IG.Code 
                            LEFT JOIN Item IC ON LS.ItemCategory = IC.Code
                            WHERE H.DocID = '" & mSearchCode & "' And LB.PurchPlan IS NOT NULL 
                            GROUP BY LB.PurchPlan, LB.PurchPlanSr   
                          "

            'mQry = mQry + "
            '    Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, H.DocID, L.Sr, H.V_Date, H.DeliveryDate, VT.Description as Voucher_Type, VT.NCat,                                 
            '    '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as InvoiceNo, 
            '    IfNull(Agent.DispName,'') as AgentName, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption,
            '    (Case When BP.Nature = 'Cash' Then BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'') Else H.SaletoPartyName End) as SaleToPartyName, 
            '    IfNull(H.SaleToPartyAddress,'') as SaleToPartyAddress, IfNull(C.CityName,'') as CityName, IfNull(H.SaleToPartyPincode,'') as SaleToPartyPincode, 
            '    IfNull(State.ManualCode,'') as StateCode, IfNull(State.Description,'')  as StateName, 
            '    IfNull(H.SaleToPartyMobile,'') as SaleToPartyMobile, Sg.ContactPerson, IfNull(H.SaleToPartySalesTaxNo,'') as SaleToPartySalesTaxNo, 
            '    IfNull(H.SaleToPartyAadharNo,'') as SaleToPartyAadharNo, IfNull(H.SaleToPartyPanNo,'') as SaleToPartyPanNo,
            '    (Case When BP.Nature = 'Cash' Then IfNull(SP.DispName, BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'')) Else IfNull(SP.DispName,H.SaletoPartyName) End) as ShipToPartyName,
            '    (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAddress,'') Else IfNull(Sp.Address,'') End) as ShipToPartyAddress, 
            '    (Case When SP.DispName Is Null Then IfNull(C.CityName,'') Else IfNull(SC.CityName,'') End) as ShipToPartyCity, 
            '    (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPinCode,'') Else IfNull(Sp.Pin,'') End) as ShipToPartyPincode, 
            '    (Case When SP.DispName Is Null Then IfNull(State.ManualCode,'') Else IfNull(SS.ManualCode,'') End) as ShipToPartyStateCode, 
            '    (Case When SP.DispName Is Null Then IfNull(State.Description,'') Else IfNull(SS.Description,'') End) as ShipToPartyStateName, 
            '    (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyMobile,'') Else IfNull(Sp.Mobile,'') End) as ShipToPartyMobile, 
            '    (Case When SP.DispName Is Null Then IfNull(H.SaleToPartySalesTaxNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'),'') End) as ShipToPartySalesTaxNo, 
            '    (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAadharNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.AadharNo & "'),'') End) as ShipToPartyAadharNo, 
            '    (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPanNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.PanNo & "'),'') End) as ShipToPartyPanNo, 
            '    H.ShipToAddress, H.TermsAndConditions, IfNull(Transporter.Name,'') as TransporterName, IfNull(TD.LrNo,'') as LrNo, TD.LrDate, IfNull(TD.PrivateMark,'') PrivateMark, TD.Weight, TD.Freight, TD.ChargedWeight, IfNull(TD.PaymentType,'') as FreightType, 
            '    IfNull(TD.RoadPermitNo,'') as RoadPermitNo, TD.RoadPermitDate, IfNull(TD.VehicleNo,'') as VehicleNo, IfNull(TD.ShipMethod,'') as ShipMethod, IfNull(TD.PreCarriageBy,'') PreCarriageBy, IfNull(TD.PreCarriagePlace,'') as PreCarriagePlace, IfNull(TD.BookedFrom,'') as BookedFrom, IfNull(TD.BookedTo,'') as BookedTo, IfNull(TD.Destination,'') as Destination, IfNull(TD.DescriptionOfGoods,'') as DescriptionOfGoods, IfNull(TD.DescriptionOfPacking,'') as DescriptionOfPacking, 
            '    IfNull(L.ReferenceNo,'') as ReferenceNo,
            '    I.Description as ItemName, " & IIf(mPrintFor = ClsMain.PrintFor.QA, "IG.Description", "IfNull(IG.PrintingDescription,IG.Description)") & " as ItemGroupName, 
            '    IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, IfNull(I.HSN,IC.HSN) as HSN, I.MaintainStockYn,
            '    D1.Specification as D1Spec, D2.Specification as D2Spec, D3.Specification as D3Spec, D4.Specification as D4Spec, Size.Specification as SizeSpec,
            '    '" & AgL.PubCaptionDimension1 & "' as D1Caption, '" & AgL.PubCaptionDimension2 & "' as D2Caption, '" & AgL.PubCaptionDimension3 & "' as D3Caption, '" & AgL.PubCaptionDimension4 & "' as D4Caption, 
            '    L.SalesTaxGroupItem, STGI.GrossTaxRate, 
            '    (Case when IfNull(I.MaintainStockYn,1) =1 AND IfNull(I.ItemType,Ic.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then L.Pcs Else 0 End) as Pcs, 
            '    (Case when IfNull(I.MaintainStockYn,1) =1 AND IfNull(I.ItemType,Ic.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then abs(L.Qty) Else 0 End) as Qty, 
            '    (Case when IfNull(I.MaintainStockYn,1) =1 AND IfNull(I.ItemType,Ic.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then L.Rate Else 0 End) as Rate, 
            '    L.Unit, U.DecimalPlaces as UnitDecimalPlaces, 
            '    L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, L.AdditionPer, L.AdditionAmount, 
            '    L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as TotalDiscount, 
            '    abs(L.Amount)+L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as AmountBeforeDiscount,
            '    abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) as Tax1_Per, abs(L.Tax1) as Tax1, 
            '    abs(L.Tax2_Per) as Tax2_Per, abs(L.Tax2) as Tax2, abs(L.Tax3_Per) as Tax3_Per, abs(L.Tax3) as Tax3, 
            '    abs(L.Tax4_Per) as Tax4_Per, abs(L.Tax4) as Tax4, abs(L.Tax5_Per) as Tax5_Per, abs(L.Tax5) as Tax5, 
            '    abs(L.Net_Amount) as Net_Amount, L.Remark as LRemarks, IfNull(H.Remarks,'') as HRemarks, 
            '    (Select Sum(L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleInvoiceDetail L1 Where L1.DocID = H.DocID) as H_TotalDiscount, 
            '    (Select Sum(abs(L1.Amount)+L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleinvoiceDetail L1 Where L1.DocID = H.DocId) as H_AmountBeforeDiscount,
            '    abs(H.Gross_Amount) as H_Gross_Amount, 
            '    H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount, Abs(H.Taxable_Amount) as H_Taxable_Amount,
            '    Abs(H.Tax1_Per) as H_Tax1_Per, Abs(H.Tax1) as H_Tax1, H.Tax2_Per as H_Tax2_Per, abs(H.Tax2) as H_Tax2, 
            '    H.Tax3_Per as H_Tax3_Per, abs(H.Tax3) as H_Tax3, H.Tax4_Per as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
            '    H.Tax5_Per as H_Tax5_Per, abs(H.Tax5) as H_Tax5, H.Deduction_Per as H_Deduction_Per, H.Deduction as H_Deduction, 
            '    H.Other_Charge_Per as H_Other_Charge_Per, H.Other_Charge as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, 
            '    '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
            '    '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, IfNull(L.DimensionDetail,'') as DimDetail,
            '    '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle
            '    from (" & bPrimaryQry & ") as H
            '    Left Join SaleInvoiceTrnSetting TS  With (NoLock) On H.DocId = TS.DocID
            '    Left Join SaleInvoiceDetail L  With (NoLock) On H.DocID = L.DocID
            '    Left Join SaleInvoiceDetailSku LS  With (NoLock) On LS.DocID = L.DocID And LS.Sr = L.Sr
            '    Left Join Item I  With (NoLock) On LS.Item = I.Code
            '    Left Join Dimension1 D1  With (NoLock) On LS.Dimension1 = D1.Code
            '    Left Join Dimension2 D2  With (NoLock) On LS.Dimension2 = D2.Code
            '    Left Join Dimension3 D3  With (NoLock) On LS.Dimension3 = D3.Code
            '    Left Join Dimension4 D4  With (NoLock) On LS.Dimension4 = D4.Code   
            '    Left Join Size  With (NoLock) On LS.Size = Size.Code
            '    Left Join Unit U  With (NoLock) On I.Unit = U.Code
            '    Left Join Item IG  With (NoLock) On LS.ItemGroup = IG.Code
            '    Left Join Item IC  With (NoLock) On LS.ItemCategory = IC.Code
            '    Left Join City C  With (NoLock) On H.SaleToPartyCity = C.CityCode
            '    Left Join State  With (NoLock) On C.State = State.Code
            '    Left Join SaleInvoiceTransport TD  With (NoLock) On H.DocID = TD.DocID
            '    Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
            '    Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
            '    Left Join Subgroup Sg  With (NoLock) On H.SaleToParty = Sg.Subcode
            '    Left Join Subgroup BP With (NoLock) On H.BillToParty = BP.Subcode
            '    Left Join Subgroup SP With (NoLock) On H.ShipToParty = SP.Subcode
            '    Left Join City SC With (NoLock) On SP.CityCode = SC.CityCode
            '    Left Join State SS with (NoLock) On SC.State = SS.Code
            '    Left Join RateType RT  With (NoLock) on H.RateType = Rt.Code
            '    Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
            '    Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
            '    Left Join DocumentPrefix DP On VT.Category = DP.Category And H.Div_Code = DP.Div_Code                
            '    Left Join SiteMast Site On H.Site_Code = Site.Code
            '    Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
            '    Left Join State SiteState On SiteCity.State = SiteState.Code
            '    "

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
            'FGetMailConfiguration(objRepPrint, SearchCode)
            'objRepPrint.AttachmentName = "Invoice"
        Else
            objRepPrint = New AgLibrary.RepView()
        End If


        'If mDocReportFileName = "" Then
        FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, "PurchPlan_Print.rpt", mPrintTitle, , sQry, "BASEDETAIL", "", DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
        'Else
        'FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, mDocReportFileName, mPrintTitle, , , , "", DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
        'End If
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
                    From PurchPlan H  With (NoLock)
                    Where H.DocID = '" & mSearchCode & "'"
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            mText = Replace(mText, "<CreditDays>", AgL.XNull(dtTemp.Rows(0)("CreditDays")))
            ClsMain.FReplacePubVariables(mText, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag)

        End If

        FReplaceInvoiceVariables = mText
    End Function
    Private Sub Dgl1_DoubleClick(sender As Object, e As EventArgs) Handles Dgl1.DoubleClick
        If Topctrl1.Mode = "Browse" Then
            Dgl1.CurrentRow.Selected = True
        End If
    End Sub
    Private Sub FSendSms()
        Dim FrmObj As FrmSendSms
        FrmObj = New FrmSendSms(AgL)
        FrmObj.TxtToMobile.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Mobile
                    From PurchPlan H  With (NoLock)
                    LEFT JOIN SubGroup Sg  With (NoLock) On H.Party = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()
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
        LblDealQty.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible Then

                If Val(Dgl1.Item(Col1UnitMultiplier, I).Value) <> 0 Then
                    Dgl1.Item(Col1DealQty, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1UnitMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value) + 2, "0"))
                End If

                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblDealQty.Text = Val(LblDealQty.Text) + Val(Dgl1.Item(Col1DealQty, I).Value)
            End If
        Next
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblDealQty.Text = Val(LblDealQty.Text)
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0

        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

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

                Case Col1Process
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Process) Is Nothing Then
                            FCreateHelpProcess()
                        End If
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            FCreateHelpDimension1(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            FCreateHelpDimension2(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Dimension3
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Dimension3) Is Nothing Then
                            FCreateHelpDimension3(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Dimension4
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Dimension4) Is Nothing Then
                            FCreateHelpDimension4(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1Size
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
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
            mQry = "Select I.Code, I.Description, I.ManualCode, I.Unit, U.DecimalPlaces as QtyDecimalPlaces, 
                    I.Specification, I.ItemType
                    , I.ItemCategory, IC.Description as ItemCategoryName
                    , I.ItemGroup, IG.Description as ItemGroupName
                    , I.Dimension1, D1.Description as Dimension1Name
                    , I.Dimension2, D2.Description as Dimension2Name
                    , I.Dimension3, D3.Description as Dimension3Name
                    , I.Dimension4, D4.Description as Dimension4Name
                    , I.Size, Size.Description as SizeName 
                    From Item I  With (NoLock)
                    Left Join Item IC With (NoLock) On I.ItemCategory = IC.Code
                    Left Join Item IG With (NoLock) On I.ItemGroup = IG.Code
                    Left Join Item D1 With (NoLock) On I.Dimension1 = D1.Code
                    Left Join Item D2 With (NoLock) On I.Dimension2 = D2.Code
                    Left Join Item D3 With (NoLock) On I.Dimension3 = D3.Code
                    Left Join Item D4 With (NoLock) On I.Dimension4 = D1.Code
                    Left Join Item Size With (NoLock) On I.Size = Size.Code
                    Left Join Unit U  With (NoLock) On I.Unit = U.Code 
                    Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
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

                Dgl1.Item(Col1MItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1MItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))
                Dgl1.Item(Col1MItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                Dgl1.Item(Col1MItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                Dgl1.Item(Col1MItemSpecification, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Specification"))
                Dgl1.Item(Col1MDimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                Dgl1.Item(Col1MDimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                Dgl1.Item(Col1MDimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                Dgl1.Item(Col1MDimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                Dgl1.Item(Col1MDimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                Dgl1.Item(Col1MDimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                Dgl1.Item(Col1MDimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                Dgl1.Item(Col1MDimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                Dgl1.Item(Col1MSize, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                Dgl1.Item(Col1MSize, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
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
                            Str1 = Dgl1.Item(Col1Item, I).Value & Dgl1.Item(Col1Specification, I).Value & Dgl1.Item(Col1Dimension1, I).Value & Dgl1.Item(Col1Dimension2, I).Value & Dgl1.Item(Col1Dimension3, I).Value & Dgl1.Item(Col1Dimension4, I).Value
                            Str2 = Dgl1.Item(Col1Item, mRow).Value & Dgl1.Item(Col1Specification, mRow).Value & Dgl1.Item(Col1Dimension1, mRow).Value & Dgl1.Item(Col1Dimension2, mRow).Value & Dgl1.Item(Col1Dimension3, mRow).Value & Dgl1.Item(Col1Dimension4, mRow).Value
                            If AgL.StrCmp(Str1, Str2) Then
                                If MsgBox("Item " & .Item(Col1Item, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
                                Else
                                    If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) = ActionOnDuplicateItem.DoNothing Then
                                    ElseIf FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) = ActionOnDuplicateItem.AlertAndGoToFirstItem Then
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
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
                Dgl1.Item(Col1ItemType, mRow).Tag = AgL.XNull(DtItemCategory.Rows(0)("ItemType"))
                Dgl1.Item(Col1ItemType, mRow).Value = AgL.XNull(DtItemCategory.Rows(0)("ItemTypeName"))
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
    Private Sub FrmSaleOrderPlan_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From PurchPlanDetailBaseSaleOrder Where GenDocId = '" & mSearchCode & "'"
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
    Private Sub FrmSaleOrderPlan_BaseEvent_DglMainCellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainCellEnter
        Select Case DglMain.CurrentCell.RowIndex
        End Select
    End Sub
    Private Sub FrmSaleOrderPlan_BaseEvent_DglMainKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainKeyDown
        Try
            If DglMain.CurrentCell IsNot Nothing Then
                If e.KeyCode = Keys.Enter Then
                    Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                    If DglMain.CurrentCell.RowIndex = LastCell.RowIndex And DglMain.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                        'If Dgl2.Visible Then
                        '    Dgl2.CurrentCell = Dgl2.Item(Col1Value, Dgl2.FirstDisplayedCell.RowIndex)
                        '    Dgl2.Focus()
                        'Else
                        Dgl1.CurrentCell = Dgl1.FirstDisplayedCell
                        Dgl1.Focus()
                        'End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
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
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuWizard.Click, MnuHistory.Click
        Select Case sender.name
            Case MnuWizard.Name
                FWizard()

            Case MnuHistory.Name
                ClsMain.FShowHistory(mSearchCode, Me)
        End Select
    End Sub
    Private Sub FWizard()
        Dim StrSenderText As String = Me.Text
        GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
        GridReportFrm.Filter_IniGrid()

        If EntryNCat = Ncat.FinishedMaterialPlan Then
            Dim CRep As ClsFinishedMaterialPlan = New ClsFinishedMaterialPlan(GridReportFrm)
            CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
            CRep.V_Type = EntryNCat
            CRep.ObjFrm = Me
            CRep.Ini_Grid()
            GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 0).Value = AgL.PubStartDate
            GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 1).Value = AgL.PubLoginDate
            ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
            GridReportFrm.MdiParent = Me.MdiParent
            GridReportFrm.Show()
            CRep.ProcSaleOrderPlan()
        Else
            Dim CRep As ClsRawMaterialPlan = New ClsRawMaterialPlan(GridReportFrm)
            CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
            CRep.V_Type = EntryNCat
            CRep.ObjFrm = Me
            CRep.Ini_Grid()
            GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 0).Value = AgL.PubStartDate
            GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 1).Value = AgL.PubLoginDate
            ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
            GridReportFrm.MdiParent = Me.MdiParent
            GridReportFrm.Show()
            CRep.ProcProcessPlan()
        End If
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag, VoucherCategory.Sales, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", "")
        FGetSettings = mValue
    End Function
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1BtnBasePlanDetail
                    ShowBasePlanDetail(bRowIndex)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl1_CellContentClick function")
        End Try
    End Sub
    Private Sub ShowBasePlanDetail(mRow As Integer)
        If Dgl1.Item(Col1BtnBasePlanDetail, mRow).Tag IsNot Nothing Then
            Dgl1.Item(Col1BtnBasePlanDetail, mRow).Tag.StartPosition = FormStartPosition.CenterParent
            Dgl1.Item(Col1BtnBasePlanDetail, mRow).Tag.ShowDialog()
        Else
            Dim FrmObj As FrmPurchPlanBase
            FrmObj = New FrmPurchPlanBase
            FrmObj.SearchCode = mSearchCode
            FrmObj.Sr = Dgl1.Item(ColSNo, mRow).Tag
            FrmObj.EntryNCat = LblV_Type.Tag
            FrmObj.LblDocNo.Text = "Plan No." & DglMain.Item(Col1Value, rowReferenceNo).Value
            Dgl1.Item(Col1BtnBasePlanDetail, mRow).Tag = FrmObj
            Dgl1.Item(Col1BtnBasePlanDetail, mRow).Tag.StartPosition = FormStartPosition.CenterParent
            Dgl1.Item(Col1BtnBasePlanDetail, mRow).Tag.ShowDialog()
        End If
    End Sub
    Private Sub FCreateHelpDimension1(RowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' Or I.ItemGroup Is Null) "
        End If

        mQry = " Select I.Code, I.Description
                From Item I With (Nolock)
                Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension1 & "' " & strCond & "
                Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpDimension2(RowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' Or I.ItemGroup Is Null) "
        End If

        mQry = " Select I.Code, I.Description
                From Item I With (Nolock)
                Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension2 & "' " & strCond & "
                Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpDimension3(RowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' Or I.ItemGroup Is Null) "
        End If

        mQry = " Select I.Code, I.Description
                From Item I With (Nolock)
                Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension3 & "' " & strCond & "
                Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Dimension3) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpDimension4(RowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' Or I.ItemGroup Is Null) "
        End If

        mQry = " Select I.Code, I.Description
                From Item I With (Nolock)
                Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension4 & "' " & strCond & "
                Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Dimension4) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpSize(RowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' Or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' Or I.ItemGroup Is Null) "
        End If

        mQry = " Select I.Code, I.Description
                From Item I With (Nolock)
                Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.SIZE & "' " & strCond & "
                Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Size) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Function FGetRelationalData() As Boolean
        Dim DtRelationalData As DataTable
        Try
            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From PurchInvoiceDetail L
                        LEFT JOIN PurchInvoice H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PurchPlan = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & DglMain(Col1Value, rowV_Type).Value + "-" + DglMain(Col1Value, rowReferenceNo).Value & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From PurchPlanDetail L
                        LEFT JOIN PurchPlan H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PurchPlan = '" & mSearchCode & "' 
                        And L.PurchPlan <> L.DocId "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & DglMain(Col1Value, rowV_Type).Value + "-" + DglMain(Col1Value, rowReferenceNo).Value & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From PurchPlanDetailBase L
                        LEFT JOIN PurchPlan H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PurchPlan = '" & mSearchCode & "' 
                        And L.PurchPlan <> L.DocId "
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
End Class
