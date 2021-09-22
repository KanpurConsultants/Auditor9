Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq

Public Class FrmLrEntry
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1Barcode As String = "Barcode"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1ItemState As String = "Item State"
    Public Const Col1BaleNo As String = "Bale No"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1ReferenceDate As String = "Reference Date"
    Public Const Col1Godown As String = "Godown"
    Public Const Col1DocQty As String = "Doc. Qty"
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
    Public Const Col1Remark As String = "Remark"
    Public Const Col1StockSr As String = "Stock Sr"
    Public Const Col1IsRecordLocked As String = "Is Record Locked"
    '========================================================================

    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Dim rowParty As Integer = 6
    Dim rowPartyDocNo As Integer = 7
    Dim rowPartyDocDate As Integer = 8
    Dim rowTransporter As Integer = 9

    Dim rowGodown As Integer = 0
    Dim rowFromGodown As Integer = 1
    Dim rowToGodown As Integer = 2
    Dim rowResponsiblePerson As Integer = 3
    Dim rowInsurancePolicyNo As Integer = 4
    Dim rowInsuranceBalanceValue As Integer = 5
    Dim rowInsuredValue As Integer = 6
    Dim rowReason As Integer = 7
    Dim rowRemarks As Integer = 8
    Dim rowRemarks1 As Integer = 9
    Dim rowRemarks2 As Integer = 10


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
    Public Const hcReason As String = "Reason"
    Public Const hcRemarks As String = "Remarks"
    Public Const hcRemarks1 As String = "Remarks1"
    Public Const hcRemarks2 As String = "Remarks2"



    Dim bInsuranceNoBarcodeSr As Integer = -1
    Dim bLrNoBarcodeSr As Integer = 0

    Public Shared mFlag_Import As Boolean = False
    Dim mPrevRowIndex As Integer = 0
    Dim Dgl As New AgControls.AgDataGrid
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuHistory As ToolStripMenuItem
    Friend WithEvents MnuReport As ToolStripMenuItem
    Dim DtV_TypeTrnSettings As DataTable
    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from StockHeadSetting H  With (NoLock) Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') Or H.V_Type Is Null "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found")
        End If
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLrEntry))
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
        Me.MnuHistory = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.Pnl1.Size = New System.Drawing.Size(973, 293)
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
        Me.PnlTotals.Controls.Add(Me.LblTotalBale)
        Me.PnlTotals.Controls.Add(Me.LblTotalBaleText)
        Me.PnlTotals.Controls.Add(Me.LblDealQty)
        Me.PnlTotals.Controls.Add(Me.LblDealQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(7, 555)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 695
        '
        'LblTotalBale
        '
        Me.LblTotalBale.AutoSize = True
        Me.LblTotalBale.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBale.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBale.Location = New System.Drawing.Point(634, 4)
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
        Me.LblTotalBaleText.Location = New System.Drawing.Point(542, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(31, 3)
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
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuHistory, Me.MnuReport})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(153, 70)
        '
        'MnuHistory
        '
        Me.MnuHistory.Name = "MnuHistory"
        Me.MnuHistory.Size = New System.Drawing.Size(152, 22)
        Me.MnuHistory.Text = "History"
        '
        'MnuReport
        '
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(152, 22)
        Me.MnuReport.Text = "Report"
        '
        'FrmLrEntry
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
        Me.Name = "FrmLrEntry"
        Me.Text = "StockHead Entry"
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
    Public WithEvents Pnl2 As Panel
    Public WithEvents PnlCustomGrid As Panel
    Protected WithEvents BtnAttachments As Button
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalQty As Label
    Public WithEvents LblTotalAmount As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LblTotalAmountText As Label
    Public WithEvents LblDealQty As Label
    Public WithEvents LblDealQtyText As Label
    Public WithEvents LblTotalBale As Label
    Public WithEvents LblTotalBaleText As Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "StockHeadDetail"
        LogLineTableCsv = "StockHeadDetail_Log"

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
                From StockHead H  With (NoLock)
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

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [StockHead_Type], Cast(strftime('%d/%m/%Y', H.V_Date) As nvarchar) AS Date, SGV.Name AS [Party], " &
                            " H.ManualRefNo AS [Manual_No], H.Remarks,  " &
                            " H.EntryBy AS [Entry_By], Cast(strftime('%d/%m/%Y', H.EntryDate) As nvarchar) AS [Entry_Date] " &
                            " FROM StockHead H  With (NoLock) " &
                            " LEFT JOIN Voucher_Type Vt  With (NoLock) ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup SGV  With (NoLock) ON SGV.SubCode  = H.SubCode " &
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
            .AddAgTextColumn(Dgl1, Col1Barcode, 100, 0, Col1Barcode, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 130, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 130, 0, Col1Specification, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemState, 130, 0, Col1ItemState, True, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 60, 255, Col1BaleNo, True, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 60, 255, Col1LotNo, True, False)
            .AddAgDateColumn(Dgl1, Col1ReferenceDate, 100, Col1ReferenceDate, False, False, False)
            .AddAgTextColumn(Dgl1, Col1Godown, 60, 255, Col1Godown, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 80, 8, 4, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Pcs, 80, 8, 4, False, Col1Pcs, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1UnitMultiplier, 70, 8, 4, False, Col1UnitMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DealQty, 70, 8, 3, False, Col1DealQty, False, True, True)
            .AddAgTextColumn(Dgl1, Col1DealUnit, 60, 0, Col1DealUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1DealUnitDecimalPlaces, 50, 0, Col1DealUnitDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1StockSr, 150, 255, Col1StockSr, False, False)
            .AddAgTextColumn(Dgl1, Col1IsRecordLocked, 150, 255, Col1IsRecordLocked, False, False)
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

        If LblV_Type.Tag <> Ncat.LrEntry Then
            Dgl1.Columns(Col1Amount).ReadOnly = True
            Dgl1.Columns(Col1Amount).DefaultCellStyle.BackColor = Color.White
        End If


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
        DglMain.Item(Col1Head, rowTransporter).Value = hcTransporter
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None

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

        Dgl2.Rows.Add(11)
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
        Dgl2.Item(Col1Head, rowReason).Value = hcReason
        Dgl2.Item(Col1Head, rowRemarks).Value = hcRemarks
        Dgl2.Item(Col1Head, rowRemarks1).Value = hcRemarks1
        Dgl2.Item(Col1Head, rowRemarks2).Value = hcRemarks2
        Dgl2.Name = "Dgl2"
        Dgl2.Tag = "VerticalGrid"

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCustomGrid1.Name = "AgCustomGrid1"

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bStockHeadSelectionQry$ = "", bHelpValuesSelectionQry$ = ""

        mQry = " Update StockHead " &
                " SET  " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
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

        FSaveTransferDetail(mSearchCode, Conn, Cmd)
        FSaveInsuranceDetail(mSearchCode, Conn, Cmd)
        FSaveLRDetail(mSearchCode, Conn, Cmd)


        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From StockHeadDetail  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If Dgl1.Item(Col1Barcode, I).Value <> "" Then
                        Dgl1.Item(Col1Barcode, I).Tag = FInsertBarCode(Conn, Cmd, mSearchCode, mSr,
                            Dgl1.Item(Col1Barcode, I).Value, Dgl1.Item(Col1Item, I).Tag, "", DglMain.Item(Col1Value, rowV_Date).Value, "", "", "", "", False)
                    End If
                    InsertStockHeadDetail(mSearchCode, mSr, I, Conn, Cmd)
                    InsertLRBaleDetail(mSearchCode, mSr, I, Conn, Cmd)
                    InsertStock(mSearchCode, mSr, mDimensionSrl, I, Conn, Cmd)
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Item(Col1Barcode, I).Tag <> "" And Dgl1.Item(Col1Barcode, I).Tag IsNot Nothing Then
                            FUpdateBarCode(Conn, Cmd, mSearchCode, mSr, Dgl1.Item(Col1Barcode, I).Tag, Dgl1.Item(Col1Barcode, I).Value, Dgl1.Item(Col1Item, I).Tag,
                                       "", DglMain.Item(Col1Value, rowV_Date).Value, "", "", "", False)
                        End If
                        UpdateStockHeadDetail(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), I, Conn, Cmd)
                        UpdateLRBaleDetail(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), Dgl1.Item(Col1BaleNo, I).Tag, I, Conn, Cmd)
                        UpdateStock(mSearchCode, Val(Dgl1.Item(ColSNo, I).Tag), Val(Dgl1.Item(Col1StockSr, I).Value), I, Conn, Cmd)
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
                    0 AS Rate, 0 AS Amount, 0 AS Landed_Value, NULL AS ReferenceDocID, NULL AS ReferenceTSr, 
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
            mQry = " Delete From StockHeadDetail Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From Barcode Where GenDocId = '" & DocID & "' And GenSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertStockHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into StockHeadDetail(DocId, Sr, Barcode, Item, 
                           Specification, ItemState, BaleNo, LotNo, ReferenceDate, Godown,
                           DocQty, Qty, Unit, Pcs, UnitMultiplier, DealUnit, DealQty,
                           Rate, Amount, Remark) "
        mQry += " Values( " & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1DocQty, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Pcs, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & " " &
                        " ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub UpdateStockHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Dgl1.Rows(LineGridRowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
            mQry = " UPDATE StockHeadDetail " &
                    " Set " &
                    " Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", " &
                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", " &
                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", " &
                    " ItemState = " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", " &
                    " BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ", " &
                    " LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ", " &
                    " ReferenceDate = " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, LineGridRowIndex).Value) & ", " &
                    " Godown = " & AgL.Chk_Text(Dgl1.Item(Col1Godown, LineGridRowIndex).Tag) & " ," &
                    " DocQty = " & Val(Dgl1.Item(Col1DocQty, LineGridRowIndex).Value) & ", " &
                    " Qty = " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " &
                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & ", " &
                    " Pcs = " & Val(Dgl1.Item(Col1Pcs, LineGridRowIndex).Value) & ", " &
                    " UnitMultiplier = " & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ", " &
                    " DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " &
                    " DealQty = " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", " &
                    " Rate = " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " &
                    " Amount = " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ", " &
                    " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & " " &
                    " Where DocId = '" & mSearchCode & "' " &
                    " And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag & " "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertStock(DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""

        If LblV_Type.Tag <> Ncat.LrEntry Then
            Dim bQty_Issue As Double = 0
            Dim bQty_Receive As Double = 0

            If LblV_Type.Tag = Ncat.StockReceive Then
                bQty_Issue = 0
                bQty_Receive = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
            Else
                bQty_Issue = Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value)
                bQty_Receive = 0
            End If



            mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                SubCode, SalesTaxGroupParty, Barcode, Item, ItemState,
                SalesTaxGroupItem,  LotNo, EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                Values
                (
                    '" & DocID & "', " & TSr & ", " & Sr & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                    " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & " , " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", 
                    Null, " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).ErrorText) & ",
                    'I', " & Val(bQty_Issue) & "," & Val(bQty_Receive) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                    " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",0,
                    Null, Null, Null
                ) "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub UpdateStock(DocID As String, TSr As Integer, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        Dim bSalesTaxGroupParty As String = ""
        If LblV_Type.Tag <> Ncat.LrEntry Then
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
                        SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ",
                        Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                        Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", 
                        ItemState = " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, LineGridRowIndex).Tag) & ", 
                        SalesTaxGroupItem = Null, 
                        LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
                        BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ",
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
                        ReferenceDocId = Null, 
                        ReferenceTSr = Null, 
                        ReferenceDocIdSr = NUll
                        Where DocId = '" & DocID & "' and TSr =" & TSr & " And Sr =" & Sr & "
                    "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Else
                mDimensionSrl += 1
                mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                    SubCode, SalesTaxGroupParty, Barcode, Item, SalesTaxGroupItem,  LotNo, 
                    EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                    Rate, Amount, Landed_Value, ReferenceDocID, ReferenceTSr, ReferenceDocIDSr) 
                    Values
                    (
                        '" & DocID & "', " & TSr & ", " & mDimensionSrl & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & " , " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, LineGridRowIndex).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Tag) & ", 
                        Null, " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, LineGridRowIndex).ErrorText) & ",
                        'I', " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ",0, " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Value) & "," & Val(Dgl1.Item(Col1UnitMultiplier, LineGridRowIndex).Value) & ",
                        " & Val(Dgl1.Item(Col1DealQty, LineGridRowIndex).Value) & ", 0,  " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Rate, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & ",0,
                        Null, Null, Null
                    )"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub
    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDglMainRowCount As Integer
        Dim mDgl2RowCount As Integer
        Try

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
                        End If
                    Next

                Next
            End If
            If mDgl2RowCount = 0 Then Dgl2.Visible = False Else Dgl2.Visible = True


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

        mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    mQry = "Select * from StockHeadSetting  With (NoLock) Where NCat = '" & LblV_Type.Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        If DtV_TypeSettings.Rows.Count = 0 Then
                            mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        End If
                    End If
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found.")
        End If


        LblTotalQty.Text = 0
        LblDealQty.Text = 0
        LblTotalBale.Text = 0
        LblTotalAmount.Text = 0

        mQry = " Select H.*, Sg.Name || ',' || IfNull(C1.CityName,'') As PartyDesc, 
                ResponsiblePerson.Name || ',' || IfNull(ResponsiblePersonCity.CityName,'') As ResponsiblePersonDesc, 
                Transporter.Name || ',' || IfNull(TransporterCity.CityName,'') As TransporterDesc, Inp.Description As InsurancePolicyNoBarcodeDesc
                From (Select * From StockHead With (NoLock) Where DocID='" & SearchCode & "') H 
                LEFT JOIN SubGroup Sg With (NoLock) ON H.SubCode = Sg.SubCode 
                LEFT JOIN City C1  With (NoLock) On Sg.CityCode = C1.CityCode 
                LEFT JOIN SubGroup ResponsiblePerson With (NoLock) On H.ResponsiblePerson = ResponsiblePerson.SubCode 
                LEFT JOIN City ResponsiblePersonCity  With (NoLock) On ResponsiblePerson.CityCode = ResponsiblePersonCity.CityCode 
                LEFT JOIN SubGroup Transporter With (NoLock) On H.Transporter = Transporter.SubCode 
                LEFT JOIN City TransporterCity With (NoLock) On Transporter.CityCode = TransporterCity.CityCode  
                LEFT JOIN BarCode Inp On H.InsurancePolicyNo = Inp.Code "
        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))

                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))
                DglMain.Item(Col1Value, rowParty).Tag = AgL.XNull(.Rows(0)("SubCode"))
                DglMain.Item(Col1Value, rowParty).Value = AgL.XNull(.Rows(0)("PartyDesc"))

                DglMain(Col1Value, rowPartyDocNo).Value = AgL.XNull(.Rows(0)("PartyDocNo"))
                DglMain(Col1Value, rowPartyDocDate).Value = AgL.XNull(.Rows(0)("PartyDocDate"))

                Dgl2(Col1Value, rowInsurancePolicyNo).Tag = AgL.XNull(.Rows(0)("InsurancePolicyNo"))
                Dgl2(Col1Value, rowInsurancePolicyNo).Value = AgL.XNull(.Rows(0)("InsurancePolicyNoBarcodeDesc"))
                Dgl2(Col1Value, rowInsuredValue).Value = AgL.XNull(.Rows(0)("InsuredValue"))

                mQry = "SELECT IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) As BalanceInsuranceValue
                        FROM Stock L 
                        WHERE L.BarCode = '" & Dgl2.Item(Col1Value, rowInsurancePolicyNo).Tag & "' 
                        AND L.DocID <> '" & mSearchCode & "'
                        GROUP BY L.Barcode "
                Dim DtInsurance As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtInsurance.Rows.Count > 0 Then
                    Dgl2.Item(Col1Value, rowInsuranceBalanceValue).Value = AgL.VNull(DtInsurance.Rows(0)("BalanceInsuranceValue"))
                End If

                DglMain(Col1Value, rowTransporter).Tag = AgL.XNull(.Rows(0)("Transporter"))
                DglMain(Col1Value, rowTransporter).Value = AgL.XNull(.Rows(0)("TransporterDesc"))

                Dgl2(Col1Value, rowResponsiblePerson).Tag = AgL.XNull(.Rows(0)("ResponsiblePerson"))
                Dgl2(Col1Value, rowResponsiblePerson).Value = AgL.XNull(.Rows(0)("ResponsiblePersonDesc"))

                Dgl2(Col1Value, rowRemarks).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks")))
                Dgl2(Col1Value, rowRemarks1).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks1")))
                Dgl2(Col1Value, rowRemarks2).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks2")))

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

                mQry = "Select L.*, Barcode.Description as BarcodeName, 
                        I.Description As ItemDesc, I.ManualCode, 
                        U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, 
                        MU.DecimalPlaces As DealUnitDecimalPlaces, 
                        IG.Description As ItemGroupName, I.ItemCategory, I.ItemGroup, 
                        IC.Description As ItemCategoryName, G.Name As GodownDesc, Ist.Description As ItemStateDesc,
                        (" & mQryStockSr & ") as StockSr
                        From (Select * From StockHeadDetail  With (NoLock)  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN Item I  With (NoLock) On L.Item = I.Code 
                        Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code 
                        Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code 
                        LEFT JOIN Item Ist On L.ItemState = Ist.Code
                        Left Join Barcode  With (NoLock) On L.Barcode = Barcode.Code
                        Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                        Left Join Unit MU  With (NoLock) On L.DealUnit = MU.Code 
                        LEFT JOIN SubGroup G On L.Godown = G.SubCode
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

                            Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryName"))

                            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))

                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))

                            Dgl1.Item(Col1ItemState, I).Tag = AgL.XNull(.Rows(I)("ItemState"))
                            Dgl1.Item(Col1ItemState, I).Value = AgL.XNull(.Rows(I)("ItemStateDesc"))

                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Pcs, I).Value = AgL.VNull(.Rows(I)("Pcs"))

                            Dgl1.Item(Col1Godown, I).Tag = AgL.XNull(.Rows(I)("Godown"))
                            Dgl1.Item(Col1Godown, I).Value = AgL.XNull(.Rows(I)("GodownDesc"))


                            Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DealUnitDecimalPlaces"))
                            Dgl1.Item(Col1UnitMultiplier, I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                            Dgl1.Item(Col1DealQty, I).Value = Format(AgL.VNull(.Rows(I)("DealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))

                            mQry = " Select Code From Barcode Where GenDocId = '" & mSearchCode & "' 
                                    And GenSr = " & Dgl1.Item(ColSNo, I).Tag & " 
                                    And Item = '" & ItemCode.LrBale & "'"
                            Dgl1.Item(Col1BaleNo, I).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())

                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1ReferenceDate, I).Value = AgL.XNull(.Rows(I)("ReferenceDate"))



                            If Val(Dgl1.Item(Col1IsRecordLocked, I).Value) > 0 Then
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True
                            End If

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                            LblTotalBale.Text += 1
                        Next I
                    End If
                End With

                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False


                If AgL.Dman_Execute("Select Count(Distinct Godown) From StockHeadDetail Where DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
                    Dgl2.Item(Col1Value, rowGodown).Tag = Dgl1.Item(Col1Godown, 0).Tag
                    Dgl2.Item(Col1Value, rowGodown).Value = Dgl1.Item(Col1Godown, 0).Value
                End If
                '-------------------------------------------------------------
            End If
        End With
        ApplyUISettings(LblV_Type.Tag)
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

                '    mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
                '    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '    If DtV_TypeSettings.Rows.Count = 0 Then
                '        mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
                '        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '        If DtV_TypeSettings.Rows.Count = 0 Then
                '            mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                '            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '            If DtV_TypeSettings.Rows.Count = 0 Then
                '                mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                '                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '                If DtV_TypeSettings.Rows.Count = 0 Then
                '                    mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                '                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '                End If
                '            End If
                '        End If
                '    End If
                '    If DtV_TypeSettings.Rows.Count = 0 Then
                '        MsgBox("Voucher Type Settings Not Found, Can not continue.")
                '        Topctrl1.FButtonClick(14, True)
                '        Exit Sub
                '    End If


                '    Dgl2(Col1Value, rowFromGodown).Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RateType"))
                '    If Dgl2(Col1Value, rowFromGodown).Tag <> "" Then
                '        Dgl2(Col1Value, rowFromGodown).Value = AgL.Dman_Execute("Select Description from RateType  With (NoLock) Where Code ='" & Dgl2(Col1Value, rowFromGodown).Tag & "'", AgL.GCn).ExecuteScalar
                '    End If


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

                mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtV_TypeSettings.Rows.Count = 0 Then
                            mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtV_TypeSettings.Rows.Count = 0 Then
                                mQry = "Select * from StockHeadSetting  With (NoLock) Where NCat = '" & LblV_Type.Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                If DtV_TypeSettings.Rows.Count = 0 Then
                                    mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                End If
                            End If
                        End If
                    End If
                End If
                If DtV_TypeSettings.Rows.Count = 0 Then
                    MsgBox("Voucher Type Settings Not Found, Can not continue.")
                    Topctrl1.FButtonClick(14, True)
                    Exit Sub
                End If

                TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GcnRead)
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                IniGrid()
                ApplyUISettings(LblV_Type.Tag)
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

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
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
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
            End Select

            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                If DglMain.CurrentCell.RowIndex = LastCell.RowIndex And DglMain.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                    If Dgl2.Visible Then
                        Dgl2.CurrentCell = Dgl2.FirstDisplayedCell
                        Dgl2.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Validating_SaleToParty(Subcode As String, Optional ShowDialogForCashParty As Boolean = True)
        Dim DtTemp As DataTable
        If DglMain.Item(Col1Value, rowV_Date).Value <> "" And DglMain.Item(Col1Value, rowParty).Value <> "" Then
            If ClsMain.IsPartyBlocked(DglMain.Item(Col1Value, rowParty).Tag, LblV_Type.Tag) Then
                MsgBox("Party is blocked for " & DglMain.Item(Col1Value, rowV_Type).Value & ". Record will not be saved")
            End If

            If DglMain.Item(Col1LastValue, rowParty).Tag <> DglMain.Item(Col1Value, rowParty).Tag Or Topctrl1.Mode = "Add" Then
                mQry = "Select H.Address
                        From SubGroup H  With (NoLock)
                        Where H.Subcode = '" & Subcode & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Dgl2(Col1Value, rowFromGodown).Value = AgL.XNull(DtTemp.Rows(0)("Address"))
                End If
            End If
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from StockHeadSetting With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from StockHeadSetting  With (NoLock) Where NCat = '" & LblV_Type.Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtV_TypeSettings.Rows.Count = 0 Then
                            mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        End If
                    End If
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found, Can not continue.")
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        IniGrid()
        ApplyUISettings(LblV_Type.Tag)
        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

        Dgl1.ReadOnly = False

        If LblV_Type.Tag = Ncat.LrEntry Then
            Dgl2.Item(Col1Value, rowGodown).Tag = "TRANSPORT"
            Dgl2.Item(Col1Value, rowGodown).Value = "TRANSPORT"
        End If

        If DglMain.Visible = True Then
            DglMain.CurrentCell = DglMain.Item(Col1Value, rowParty)
            DglMain.Focus()
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


        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

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
                Case Col1Qty
                    If LblV_Type.Tag = Ncat.LrEntry Then
                        CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = 3
                    End If
                Case Col1Pcs
                    If LblV_Type.Tag = Ncat.LrEntry Then
                        CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    End If
            End Select

            If Dgl2.Item(Col1Value, rowGodown).Value <> "" Then
                Dgl1.Item(Col1Godown, Dgl1.CurrentCell.RowIndex).Tag = Dgl2.Item(Col1Value, rowGodown).Tag
                Dgl1.Item(Col1Godown, Dgl1.CurrentCell.RowIndex).Value = Dgl2.Item(Col1Value, rowGodown).Value
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
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing

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
            Dgl1.CurrentCell = Dgl1.Item(Col1Item, Dgl1.Rows.Count - 1) : Dgl1.Focus()
        End If
    End Sub
    Private Function FCreateHelpSubgroup() As DataSet
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")).ToString.Substring(0, 1) = "-" Then
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

        End If

        strCond += " And Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Cash & "','" & ClsMain.SubGroupNature.Bank & "')"

        mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.Address, Ag.GroupName, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " &
                " FROM SubGroup Sg  With (NoLock)  " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'TxtParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
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
        'For SSRS Print Out

        mQry = "SELECT H.DocID  FROM StockHead H With (NoLock)
                LEFT JOIN StockHeadDetail L With (NoLock) ON H.DocID = L.DocID 
                WHERE H.DocID ='" & SearchCode & "' And H.Gross_Amount > 0
                GROUP BY H.DocID 
                HAVING Sum(L.Amount)<>Max(H.Gross_Amount)"
        If AgL.FillData(mQry, AgL.GCn).Tables(0).Rows.Count > 0 Then
            MsgBox("Something went wrong with gross amount. Can not print Invoice. Please check once.")
            Exit Sub
        End If

        FGetPrint(ClsMain.PrintFor.DocumentPrint)
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


        Dgl1.ReadOnly = False
    End Sub
    Private Sub Dgl1_DoubleClick(sender As Object, e As EventArgs) Handles Dgl1.DoubleClick
        If Topctrl1.Mode = "Browse" Then
            Dgl1.CurrentRow.Selected = True
        End If
    End Sub
    Private Sub FGetPrint(mPrintFor As ClsMain.PrintFor)
        Dim dsMain As DataTable
        Dim dsCompany As DataTable
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer

        If mPrintFor = ClsMain.PrintFor.EMail Or mPrintFor = ClsMain.PrintFor.QA Then
            PrintingCopies = ("").Split(",")
        Else
            PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")
        End If

        If LblV_Type.Tag = Ncat.SaleReturn Then
            mPrintTitle = DglMain.Item(Col1Value, rowV_Type).Value & " (Credit Note)"
        Else
            If Not AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
                mPrintTitle = "CHALLAN"
            Else
                mPrintTitle = DglMain.Item(Col1Value, rowV_Type).Value  ' "TAX INVOICE"
            End If
        End If

        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, H.DocID, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, IfNull(RT.Description,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("SaleRate_Caption")) & "') as RateType, Agent.DispName as AgentName, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption,
                H.SaleToPartyName, H.SaleToPartyAddress, C.CityName as CityName, State.ManualCode as StateCode, State.Description as StateName, 
                H.SaleToPartyMobile, Sg.ContactPerson, H.SaleToPartySalesTaxNo, 
                (Select RegistrationNo From SubgroupRegistration Where RegistrationType='" & SubgroupRegistrationType.AadharNo & "' And Subcode=H.SaleToParty) as SaleToPartyAadharNo,
                (Select RegistrationNo From SubgroupRegistration Where RegistrationType='" & SubgroupRegistrationType.PanNo & "' And Subcode=H.SaleToParty) as PanNo,
                H.ShipToAddress, H.TermsAndConditions, Transporter.Name as TransporterName, TD.LrNo, TD.LrDate, TD.PrivateMark, TD.Weight, TD.Freight, TD.PaymentType as FreightType, TD.RoadPermitNo, TD.RoadPermitDate, IfNull(L.ReferenceNo,'') as ReferenceNo,
                I.Description as ItemName, " & IIf(mPrintFor = ClsMain.PrintFor.QA, "IG.Description", "IfNull(IG.PrintingDescription,IG.Description)") & " as ItemGroupName, IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, I.HSN, I.MaintainStockHeadYn,
                L.SalesTaxGroupItem, STGI.GrossTaxRate, L.Pcs, abs(L.Qty) as Qty, L.Rate, L.Unit, U.DecimalPlaces as UnitDecimalPlaces, TS.DiscountCalculationPattern, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, 
                abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) as Tax1_Per, abs(L.Tax1) as Tax1, abs(L.Tax2_Per) as Tax2_Per, abs(L.Tax2) as Tax2, abs(L.Tax3_Per) as Tax3_Per, abs(L.Tax3) as Tax3, abs(L.Tax4_Per) as Tax4_Per, abs(L.Tax4) as Tax4, abs(L.Tax5_Per) as Tax5_Per, abs(L.Tax5) as Tax5, abs(L.Net_Amount) as Net_Amount, L.Remark as LRemarks, H.Remarks as HRemarks,
                abs(H.Gross_Amount) as H_Gross_Amount, H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount, Abs(H.Taxable_Amount) as H_Taxable_Amount,Abs(H.Tax1_Per) as H_Tax1_Per, Abs(H.Tax1) as H_Tax1, 
                H.Tax2_Per as H_Tax2_Per, abs(H.Tax2) as H_Tax2, H.Tax3_Per as H_Tax3_Per, abs(H.Tax3) as H_Tax3, H.Tax4_Per as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                H.Tax5_Per as H_Tax5_Per, abs(H.Tax5) as H_Tax5, H.Deduction_Per as H_Deduction_Per, H.Deduction as H_Deduction, H.Other_Charge_Per as H_Other_Charge_Per, H.Other_Charge as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsCenterAlignedCompanyInfoOnDocuments")) & "' as IsCenterAlignedCompanyInfoOnDocuments, IfNull(L.DimensionDetail,'') as DimDetail,
                '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
                from (Select * From StockHead  With (NoLock) Where DocID = '" & mSearchCode & "') as H
                Left Join StockHeadTrnSetting TS  With (NoLock) On H.DocId = TS.DocID
                Left Join StockHeadDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Item I  With (NoLock) On L.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join ItemCategory IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.SaleToPartyCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join StockHeadTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.SaleToParty = Sg.Subcode
                Left Join RateType RT  With (NoLock) on H.RateType = Rt.Code
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type                
                "

        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "

        dsMain = AgL.FillData(mQry, AgL.GCn).Tables(0)


        FReplaceInvoiceVariables(dsMain, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag)

        dsCompany = ClsMain.GetDocumentHeaderDataTable(TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag, DglMain.Item(Col1Value, rowV_Type).Tag)

        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailCompose(AgL)
            objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
                    From StockHead H  With (NoLock)
                    LEFT JOIN SubGroup Sg  With (NoLock) On H.SaleToParty = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
                    From StockHead H  With (NoLock)
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
            dsMain.WriteXml(AgL.PubReportPath + "\StockHead_DsMain.xml")
            dsCompany.WriteXml(AgL.PubReportPath + "\StockHead_DsCompany.xml")
        End If

        If ClsMain.IsScopeOfWorkContains("+CLOTH TRADING WHOLESALE") Then
            objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\StockHead_Cloth.rdl"
        Else
            objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\StockHead.rdl"
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
        FrmObj.TxtToMobile.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Mobile
                    From StockHead H  With (NoLock)
                    LEFT JOIN SubGroup Sg  With (NoLock) On H.Party = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()
    End Sub
    Private Sub MnuPrintQACopy_Click(sender As Object, e As EventArgs)
        FGetPrint(ClsMain.PrintFor.QA)
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
        LblTotalBale.Text = 0
        LblTotalAmount.Text = 0
        Dgl2.Item(Col1Value, rowInsuredValue).Value = 0


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible Then

                If Val(Dgl1.Item(Col1UnitMultiplier, I).Value) <> 0 Then
                    Dgl1.Item(Col1DealQty, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1UnitMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value) + 2, "0"))
                End If

                If Val(Dgl1.Item(Col1Amount, I).Value) <> 0 And Dgl1.Columns(Col1Amount).ReadOnly = False Then
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If AgL.VNull(Dgl1.Item(Col1Qty, I).Value) = 0 Or AgL.VNull(Dgl1.Item(Col1Qty, I).Value) = AgL.VNull(Dgl1.Item(Col1DocQty, I).Value) Then
                    Dgl1.Item(Col1Qty, I).Value = Dgl1.Item(Col1DocQty, I).Value
                End If

                If LblV_Type.Tag = Ncat.LrEntry Then
                    Dgl2.Item(Col1Value, rowInsuredValue).Value += Val(Dgl1.Item(Col1Pcs, I).Value)
                End If

                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblDealQty.Text = Val(LblDealQty.Text) + Val(Dgl1.Item(Col1DealQty, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                LblTotalBale.Text += 1
            End If
        Next
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblDealQty.Text = Val(LblDealQty.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
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
                                    If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionOnDuplicateItem")).ToString.ToUpper = "DO NOTHING" Then
                                    ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("ActionOnDuplicateItem")).ToString.ToUpper = "GO TO FIRST ITEM" Then
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
    Private Sub FrmLrEntry_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        If AgL.Dman_Execute("Select Count(*) From StockHeadDetailTransfer With (NoLock) Where DocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar > 0 Then
            mQry = "Delete From StockHeadDetailTransfer Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If AgL.Dman_Execute("Select Count(*) From Barcode With (NoLock) Where GenDocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar > 0 Then
            mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

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
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
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
    Private Sub FrmLrEntry_BaseEvent_DglMainCellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainCellEnter
        Select Case DglMain.CurrentCell.RowIndex
            Case rowPartyDocDate
                CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
        End Select
    End Sub
    Private Sub FrmLrEntry_BaseEvent_DglMainKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainKeyDown
        Try
            If DglMain.CurrentCell IsNot Nothing Then
                If e.KeyCode = Keys.Enter Then
                    Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                    If DglMain.CurrentCell.RowIndex = LastCell.RowIndex And DglMain.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                        If Dgl2.Visible Then
                            Dgl2.CurrentCell = Dgl2.FirstDisplayedCell
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
        mQry = mQry + "Union All Select  '' as Srl,'Entry No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Party Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Party Doc No' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Party Doc Date' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Transporter' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
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
                    StockEntryTable.Line_Item = ""
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
        Dim Line_Item As String
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
                If StockHeadTableList(I).Line_Amount IsNot Nothing And StockHeadTableList(I).Line_Amount <> 0 Then
                    If Trim(StockHeadTableList(I).SubCodeName) <> Trim(StockHeadTableList(I).Line_SubCodeName) Then
                        StockHeadTableList(I).Line_SubCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  " & AgL.Chk_Text(AgL.XNull(StockHeadTableList(I).Line_SubCodeName)) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

                        mQry = "Insert Into StockHeadDetail(DocId, Sr, Item, Specification, BaleNo, LotNo, " &
                           " Qty, Unit, Pcs, Rate, Amount, Remark)
                            Select " & AgL.Chk_Text(StockHeadTableList(0).DocID) & ", 
                            " & Val(StockHeadTableList(I).Line_Sr) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_Item) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_Specification) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_BaleNo) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_LotNo) & ", 
                            " & Val(StockHeadTableList(I).Line_Qty) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_Unit) & ", 
                            " & Val(StockHeadTableList(I).Line_Pcs) & ", 
                            " & Val(StockHeadTableList(I).Line_Rate) & ", 
                            " & Val(StockHeadTableList(I).Line_Amount) & ", 
                            " & AgL.Chk_Text(StockHeadTableList(I).Line_Remarks) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next
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
End Class
