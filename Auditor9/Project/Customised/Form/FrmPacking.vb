Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq

Public Class FrmPacking
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1BaleNo As String = "Bale No"
    Public Const Col1Barcode As String = "Barcode"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1StockHeadSr As String = "StockHead Sr"
    Public Const Col1PartyItem As String = "PartyItem"
    Public Const Col1PartyItemSpecification1 As String = "PartyItemSpecification1"
    Public Const Col1PartyItemSpecification2 As String = "PartyItemSpecification2"
    Public Const Col1PartyItemSpecification3 As String = "PartyItemSpecification3"
    Public Const Col1PartyItemSpecification4 As String = "PartyItemSpecification4"
    Public Const Col1PartyItemSpecification5 As String = "PartyItemSpecification5"
    Public Const Col1Length As String = "Length"
    Public Const Col1Width As String = "Width"
    Public Const Col1UnitMultiplier As String = "UnitMultiplier"
    Public Const Col1DealUnit As String = "DealUnit"
    Public Const Col1Weight As String = "Weight"
    Public Const Col1GrossWeight As String = "GrossWeight"
    Public Const Col1SaleOrder As String = "SaleOrder"
    Public Const Col1SaleOrderDocId As String = "SaleOrderDocId"
    Public Const Col1IsRecordLocked As String = "Is Record Locked"



    '========================================================================

    Dim rowParty As Integer = 5

    Public Const hcParty As String = "Party"

    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public Const hcGodown As String = "Godown"
    Public Const hcDealUnit As String = "Deal Unit"
    Public Const hcRemarks As String = "Remark"

    Dim rowGodown As Integer = 0
    Dim rowDealUnit As Integer = 1
    Dim rowRemarks As Integer = 2

    Dim mPrevRowIndex As Integer = 0
    Dim Dgl As New AgControls.AgDataGrid
    Protected WithEvents BtnAddNew As Button
    Dim DtV_TypeTrnSettings As DataTable
    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        'mQry = "Select H.* from StockHeadSetting H  With (NoLock) Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') Or H.V_Type Is Null "
        'DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        'If DtV_TypeSettings.Rows.Count = 0 Then
        '    MsgBox("Voucher Type Settings Not Found")
        'End If
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPacking))
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
        Me.LblTotalDealQty = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalDealQtyText = New System.Windows.Forms.Label()
        Me.BtnAddNew = New System.Windows.Forms.Button()
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
        Me.TabControl1.Size = New System.Drawing.Size(992, 222)
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
        Me.TP1.Size = New System.Drawing.Size(984, 196)
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
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(465, 217)
        Me.LblV_Type.Size = New System.Drawing.Size(86, 16)
        Me.LblV_Type.Tag = ""
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
        Me.Pnl1.Location = New System.Drawing.Point(1, 266)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(981, 288)
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
        Me.LinkLabel1.Location = New System.Drawing.Point(1, 245)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
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
        Me.Panel3.Location = New System.Drawing.Point(4, 201)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 145)
        Me.Panel3.TabIndex = 11
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(493, 3)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(491, 192)
        Me.Pnl2.TabIndex = 5
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
        Me.PnlTotals.Controls.Add(Me.LblTotalDealQty)
        Me.PnlTotals.Controls.Add(Me.BtnAttachments)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalDealQtyText)
        Me.PnlTotals.Location = New System.Drawing.Point(4, 554)
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
        'LblTotalDealQty
        '
        Me.LblTotalDealQty.AutoSize = True
        Me.LblTotalDealQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDealQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalDealQty.Location = New System.Drawing.Point(868, 4)
        Me.LblTotalDealQty.Name = "LblTotalDealQty"
        Me.LblTotalDealQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDealQty.TabIndex = 662
        Me.LblTotalDealQty.Text = "."
        Me.LblTotalDealQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'LblTotalDealQtyText
        '
        Me.LblTotalDealQtyText.AutoSize = True
        Me.LblTotalDealQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDealQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDealQtyText.Location = New System.Drawing.Point(771, 3)
        Me.LblTotalDealQtyText.Name = "LblTotalDealQtyText"
        Me.LblTotalDealQtyText.Size = New System.Drawing.Size(101, 16)
        Me.LblTotalDealQtyText.TabIndex = 661
        Me.LblTotalDealQtyText.Text = "Total Deal Qty:"
        '
        'BtnAddNew
        '
        Me.BtnAddNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddNew.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAddNew.Location = New System.Drawing.Point(234, 242)
        Me.BtnAddNew.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAddNew.Name = "BtnAddNew"
        Me.BtnAddNew.Size = New System.Drawing.Size(129, 23)
        Me.BtnAddNew.TabIndex = 3021
        Me.BtnAddNew.TabStop = False
        Me.BtnAddNew.Text = "Add New"
        Me.BtnAddNew.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAddNew.UseVisualStyleBackColor = True
        '
        'FrmPacking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.BtnAddNew)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmPacking"
        Me.Text = "Packing Entry"
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
        Me.Controls.SetChildIndex(Me.BtnAddNew, 0)
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
    Public WithEvents LblTotalDealQty As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LblTotalDealQtyText As Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        'LogTableName = "StockHead_Log"
        MainLineTableCsv = "StockHeadDetail"
        'LogLineTableCsv = "StockHeadDetail_Log"

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
            .AddAgNumberColumn(Dgl1, Col1BaleNo, 80, 8, 2, False, Col1BaleNo, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Barcode, 100, 0, Col1Barcode, True, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 0, Col1LotNo, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 100, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 0, Col1Specification, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, False)
            .AddAgNumberColumn(Dgl1, Col1DealQty, 80, 8, 4, False, Col1DealQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1StockHeadSr, 150, 255, Col1StockHeadSr, False, False)
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
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top

        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If

        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col1Head, 150, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl2, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl2, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl2, Col1Value, 240, 255, Col1Value, True, False)
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


        Dgl2.Rows.Add(15)
        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2.Rows(I).Visible = False
        Next


        Dgl2.Item(Col1Head, rowGodown).Value = hcGodown
        Dgl2.Item(Col1Head, rowDealUnit).Value = hcDealUnit
        Dgl2.Item(Col1Head, rowRemarks).Value = hcRemarks

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
        mQry = " Update StockHead " &
                " SET  " &
                " SubCode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & ", " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " Remarks = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowRemarks).Value) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From StockHeadDetail  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag <> "" Then
                    If Dgl1.Rows(I).Visible = False Then
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
            mQry = " Delete From Stock Where DocId = '" & DocID & "' And ReferenceDocIDSr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From StockHeadDetail Where DocId = '" & DocID & "' And Sr = " & Sr & "  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertStockHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "INSERT INTO StockHeadDetail (DocID, Sr, PartyItem, PartyItemSpecification1, PartyItemSpecification2, PartyItemSpecification3, PartyItemSpecification4, PartyItemSpecification5,
                Qty, Unit, Rate, Amount, Remark) "
        mQry += " VALUES (" & AgL.Chk_Text(DocID) & ", " & Sr & ",
                    " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ", 
                    " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Tag) & "," & Val(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ", " & Val(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & ")"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub UpdateStockHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Dgl1.Rows(LineGridRowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
            mQry = "UPDATE StockHeadDetail
                    SET PartyItem = " & AgL.Chk_Text(Dgl1.Item(Col1Item, LineGridRowIndex).Value) & ",
	                PartyItemSpecification1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, LineGridRowIndex).Value) & ",
	                PartyItemSpecification2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, LineGridRowIndex).Value) & ",
	                PartyItemSpecification3 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, LineGridRowIndex).Value) & ",
	                PartyItemSpecification4 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, LineGridRowIndex).Value) & ",
	                PartyItemSpecification5 = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, LineGridRowIndex).Value) & ",
	                Qty = " & Val(Dgl1.Item(Col1Qty, LineGridRowIndex).Value) & ",
	                Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, LineGridRowIndex).Tag) & ",
	                Rate = " & Val(Dgl1.Item(Col1BaleNo, LineGridRowIndex).Value) & ",
	                Amount = " & Val(Dgl1.Item(Col1LotNo, LineGridRowIndex).Value) & ",
	                Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, LineGridRowIndex).Value) & "
                    Where DocID = '" & mSearchCode & "' 
                    And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            If Dgl1.Item(Col1Qty, bRowIndex).Value = 0 Or Dgl1.Item(Col1Item, bRowIndex).Value = "" Then Exit Sub
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                'Case Col1BtnBarcodeDetail
                'ShowBarcodeDetail(bRowIndex)


            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl1_CellContentClick function")
        End Try
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

        If e.Control And e.KeyCode = Keys.D Then
            If Val(Dgl1.Item(Col1IsRecordLocked, Dgl1.CurrentCell.RowIndex).Value) = 0 Then
                sender.CurrentRow.visible = False
                Calculation()
            End If
        ElseIf e.Control And e.KeyCode = Keys.O Then
            If Val(Dgl1.Item(Col1IsRecordLocked, Dgl1.CurrentCell.RowIndex).Value) = 0 And Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag <> "" Then
                ShowPackingDetail(mSearchCode, Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag)
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

    End Sub

    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl2RowCount As Integer
        Dim mDglMainRowCount As Integer
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
            If mDgl2RowCount = 0 Then
                Dgl2.Visible = False
            Else
                Dgl2.Visible = True
            End If

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
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    End If
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found.")
        End If


        LblTotalQty.Text = 0
        LblTotalDealQty.Text = 0

        mQry = "SELECT H.DocID, H.V_Date, H.ManualRefNo, H.SubCode, SP.DispName AS PartyName, H.Remarks
                FROM StockHead H WITH (Nolock)
                LEFT JOIN Subgroup SP WITH (Nolock) ON SP.Subcode = H.Subcode
                Where H.DocID='" & SearchCode & "'"

        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))
                DglMain.Item(Col1Value, rowParty).Tag = AgL.XNull(.Rows(0)("SubCode"))
                DglMain.Item(Col1Value, rowParty).Value = AgL.XNull(.Rows(0)("PartyName"))

                Dgl2(Col1Value, rowRemarks).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks")))

                AgCustomGrid1.FMoveRecFooterTable(DsMain.Tables(0))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "SELECT B.Description AS BarcodeName, L.DocID, L.Sr, L.Qty, L.Unit, L.Rate, L.Amount,  L.BaleNo, L.LotNo, L.Remarks ,  L.Barcode, L.DealQty, L.Godown, G.Name AS GodownName, L.DealUnit, L.Item, I.Description AS ItemName    
                        FROM StockHeadDetail L WITH (Nolock)
                        LEFT JOIN Item I WITH (Nolock) ON I.Code = L.Item
                        LEFT JOIN Subgroup G WITH (Nolock) ON G.Subcode = L.Godown
                        LEFT JOIN Barcode B WITH (Nolock) ON B.Code = L.Barcode 
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


                            Dgl2(Col1Value, rowGodown).Tag = AgL.XNull(.Rows(I)("Godown"))
                            Dgl2(Col1Value, rowGodown).Value = AgL.XNull(.Rows(I)("GodownName"))

                            Dgl2(Col1Value, rowDealUnit).Tag = AgL.XNull(.Rows(I)("DealUnit"))
                            Dgl2(Col1Value, rowDealUnit).Value = AgL.XNull(.Rows(I)("DealUnit"))
                            Dgl1.Item(Col1Barcode, I).Tag = AgL.XNull(.Rows(I)("Barcode"))
                            Dgl1.Item(Col1Barcode, I).Value = AgL.XNull(.Rows(I)("BarcodeName"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl1.Item(Col1DealQty, I).Value = AgL.VNull(.Rows(I)("DealQty"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemName"))

                            If Val(Dgl1.Item(Col1IsRecordLocked, I).Value) > 0 Then
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True
                            End If

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalDealQty.Text = Val(LblTotalDealQty.Text) + Val(Dgl1.Item(Col1DealQty, I).Value)
                        Next I
                    End If
                End With

                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
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
        'Dim FrmObj As New FrmPackingPartyDetail


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

            Case rowReferenceNo
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead",
                                DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)
        End Select
    End Sub
    Private Sub Validating_SaleToParty(Subcode As String, Optional ShowDialogForFreshEnquiryParty As Boolean = True)
        Dim DtTemp As DataTable
        If DglMain.Item(Col1Value, rowV_Date).Value <> "" And DglMain.Item(Col1Value, rowParty).Value <> "" Then
            'If TxtParty.AgLastValueTag <> DglMain.Item(Col1Value, rowParty).Tag Or Topctrl1.Mode = "Add" Then

            mQry = "Select H.*, RT.Description as RateTypeName, Agent.Name as AgentName, Transporter.Name as TransporterName 
                                    From SubgroupSiteDivisionDetail H  With (NoLock)
                                    Left Join RateType RT With (NoLock) on H.RateType = RT.Code
                                    Left Join viewHelpSubgroup agent With (NoLock) On H.Agent = Agent.Code
                                    Left Join viewHelpSubgroup Transporter With (NoLock) On H.Transporter = Transporter.Code
                                    Where H.Subcode = '" & Subcode & "' And H.Site_Code='" & DglMain.Item(Col1Value, rowSite_Code).Tag & "' And H.Div_Code='" & TxtDivision.Tag & "'"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then

                    'Dgl2(Col1Value, rowRateType).Tag = AgL.XNull(DtTemp.Rows(0)("RateType"))
                    'Dgl2(Col1Value, rowRateType).Value = AgL.XNull(DtTemp.Rows(0)("RateTypeName"))
                    'Dgl3(Col1Value, rowAgent).Tag = AgL.XNull(DtTemp.Rows(0)("Agent"))
                    'Dgl3(Col1Value, rowAgent).Value = AgL.XNull(DtTemp.Rows(0)("AgentName"))
                    'Dgl3(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("Transporter"))
                    'Dgl3(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("TransporterName"))







                    'If AgL.XNull(DtTemp.Rows(0)("TermsAndConditions")) <> "" Then
                    '    Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtTemp.Rows(0)("TermsAndConditions"))
                    'Else
                    '    Dgl3(Col1Value, rowTermsAndConditions).Value = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditions"))
                    'End If
                Else
                    'TxtRateType.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_RateType"))
                    'If TxtRateType.Tag <> "" Then
                    '    TxtRateType.Text = AgL.Dman_Execute("Select Description from RateType Where Code ='" & TxtRateType.Tag & "'", AgL.GCn).ExecuteScalar
                    'End If
                    'TxtTermsAndConditions.Text = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_TermsAndConditions"))
                End If



                'FGetCurrBal(Subcode)


                'BtnFillPartyDetail.Tag = Nothing
                'ShowStockHeadParty("", Subcode, "", ShowDialogForFreshEnquiryParty)
            End If
        'End If

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
                        mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
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

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        'BtnFillPartyDetail.Tag = Nothing
        IniGrid()
        ApplyUISettings(LblV_Type.Tag)
        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

        Dgl1.ReadOnly = False

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

        'If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub



        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1Item, I).Value <> "" Then

                    End If
                End If
            Next
        End With


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
    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        'LblPartyDetail.Visible = False
        'BtnFillPartyDetail.Tag = Nothing
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
    Private Sub FrmPacking_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        'If TxtParty.AgHelpDataSet IsNot Nothing Then TxtParty.AgHelpDataSet.Dispose() : TxtParty.AgHelpDataSet = Nothing

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
    Private Sub FrmPacking_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

        strCond += " And Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Bank & "')"

        mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.Address, Ag.GroupName, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " &
                " FROM SubGroup Sg  With (NoLock) " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'TxtParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Sub FrmPacking_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
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
    Private Sub FrmPacking_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
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
                I.Description as ItemName, " & IIf(mPrintFor = ClsMain.PrintFor.QA, "IG.Description", "IFNull(IG.PrintingDescription,IG.Description)") & " as ItemGroupName, IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, I.HSN, I.MaintainStockHeadYn,
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

        If ClsMain.IsScopeOfWorkContains("CLOTH") Then
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

            If Dgl2.CurrentCell.ColumnIndex <> Dgl2.Columns(Col1Value).Index Then Exit Sub


            Dgl2.AgHelpDataSet(Dgl2.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case Dgl2.CurrentCell.RowIndex
                'Case rowGodown, rowRemark
                '    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
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
                Case rowDealUnit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Code AS Unit  FROM Unit  Order By Code"
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



            End Select
            Call Calculation1()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Calculation1()
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub


        LblTotalQty.Text = 0



        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Rows(I).Visible Then


                Dgl1.Item(Col1LotNo, I).Value = Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1BaleNo, I).Value)


                'Footer Calculation
                Dim bQty As Double = 0



                bQty = Val(Dgl1.Item(Col1Qty, I).Value)

                LblTotalQty.Text = Val(LblTotalQty.Text) + bQty

            End If
        Next
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
    Private Sub FrmPacking_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        LblTotalQty.Text = 0
        LblTotalDealQty.Text = 0
        For I As Integer = 0 To Dgl1.RowCount - 1
            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
            LblTotalDealQty.Text = Val(LblTotalDealQty.Text) + Val(Dgl1.Item(Col1LotNo, I).Value)
        Next
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        'sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                'Case Col1Item
                '    If e.KeyCode <> Keys.Enter Then
                '        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                '            mQry = "SELECT L.PartyItem AS Code, L.PartyItem
                '                    FROM StockHead H
                '                    LEFT JOIN StockHeaddetail L ON L.DocID = H.DocID 
                '                    WHERE L.PartyItem IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                '                    GROUP BY L.PartyItem "
                '            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                '    End If


                'Case Col1Dimension1
                '    If e.KeyCode <> Keys.Enter Then
                '        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                '            mQry = "SELECT L.PartyItemSpecification1 AS Code, L.PartyItemSpecification1
                '                    FROM StockHead H
                '                    LEFT JOIN StockHeaddetail L ON L.DocID = H.DocID 
                '                    WHERE L.PartyItemSpecification1 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                '                    GROUP BY L.PartyItemSpecification1 "
                '            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                '    End If

                'Case Col1Dimension2
                '    If e.KeyCode <> Keys.Enter Then
                '        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                '            mQry = "SELECT L.PartyItemSpecification2 AS Code, L.PartyItemSpecification2
                '                    FROM StockHead H
                '                    LEFT JOIN StockHeaddetail L ON L.DocID = H.DocID 
                '                    WHERE L.PartyItemSpecification2 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                '                    GROUP BY L.PartyItemSpecification2 "
                '            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                '    End If


                'Case Col1Dimension3
                '    If e.KeyCode <> Keys.Enter Then
                '        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                '            mQry = "SELECT L.PartyItemSpecification3 AS Code, L.PartyItemSpecification3
                '                    FROM StockHead H
                '                    LEFT JOIN StockHeaddetail L ON L.DocID = H.DocID 
                '                    WHERE L.PartyItemSpecification3 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                '                    GROUP BY L.PartyItemSpecification3 "
                '            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                '    End If

                'Case Col1Dimension4
                '    If e.KeyCode <> Keys.Enter Then
                '        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                '            mQry = "SELECT L.PartyItemSpecification4 AS Code, L.PartyItemSpecification4
                '                    FROM StockHead H
                '                    LEFT JOIN StockHeaddetail L ON L.DocID = H.DocID 
                '                    WHERE L.PartyItemSpecification4 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                '                    GROUP BY L.PartyItemSpecification4 "
                '            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                '    End If

                'Case Col1Specification
                '    If e.KeyCode <> Keys.Enter Then
                '        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                '            mQry = "SELECT L.PartyItemSpecification5 AS Code, L.PartyItemSpecification5
                '                    FROM StockHead H
                '                    LEFT JOIN StockHeaddetail L ON L.DocID = H.DocID 
                '                    WHERE L.PartyItemSpecification5 IS NOT NULL AND H.SaleToParty = '" & DglMain.Item(Col1Value, rowParty).Tag & "'
                '                    GROUP BY L.PartyItemSpecification5 "
                '            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '        CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                '    End If

                'Case Col1Unit
                '    If e.KeyCode <> Keys.Enter Then
                '        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                '            mQry = "SELECT Code, Code as Description FROM Unit "
                '            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
                Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))
                Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
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
                    If .Item(Col1Item, I).Value <> "" Then
                        If mRow <> I Then
                            'Str1 = Dgl1.Item(Col1Item, I).Value & Dgl1.Item(Col1Specification, I).Value & Dgl1.Item(Col1Dimension1, I).Value & Dgl1.Item(Col1Dimension2, I).Value & Dgl1.Item(Col1Dimension3, I).Value & Dgl1.Item(Col1Dimension4, I).Value & Dgl1.Item(Col1Item, I).Value
                            'Str2 = Dgl1.Item(Col1Item, mRow).Value & Dgl1.Item(Col1Specification, mRow).Value & Dgl1.Item(Col1Dimension1, mRow).Value & Dgl1.Item(Col1Dimension2, mRow).Value & Dgl1.Item(Col1Dimension3, mRow).Value & Dgl1.Item(Col1Dimension4, mRow).Value & Dgl1.Item(Col1Item, mRow).Value
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
                strCond += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM ItemCategory I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpItemGroup(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If
        End If

        If Dgl1.Item(Col1Dimension1, RowIndex).Value <> "" Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1Dimension1, RowIndex).Tag & "' Or I.ItemCategory Is Null ) "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
            strCond += " And (IG.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(IG.ShowItemGroupInOtherDivisions,0) =1) "
        End If


        mQry = "Select IG.Code, IG.Description 
                From Item I  With (NoLock)
                Left Join ItemGroup IG  With (NoLock) On I.ItemGroup = IG.Code
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & "
                Group By I.ItemGroup,IG.Code, IG.Description "
        Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub BtnAddNew_Click(sender As Object, e As EventArgs) Handles BtnAddNew.Click
        If Topctrl1.Mode = "Add" Or Topctrl1.Mode = "Edit" Then
            ShowPackingDetail(mSearchCode, 0)
        End If
    End Sub

    Private Sub ShowPackingDetail(DocID As String, Sr As Integer)
        If mSearchCode IsNot Nothing Then
            Dim FrmObj As FrmPackingDetail
            FrmObj = New FrmPackingDetail
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DivisionCode = TxtDivision.Tag
            FrmObj.SiteCode = DglMain.Item(Col1Value, rowSite_Code).Tag
            FrmObj.NCat = DglMain.Item(Col1Value, rowV_Type).Tag
            FrmObj.PackingDocId = DocID
            FrmObj.PackingDocIdSr = Sr
            FrmObj.Godown = Dgl2(Col1Value, rowGodown).Tag
            FrmObj.DealUnit = Dgl2(Col1Value, rowDealUnit).Tag
            FrmObj.IniGrid()
            FrmObj.FMoveRec(DocID, Sr)
            FrmObj.objFrmPacking = Me
            BtnAddNew.Tag = FrmObj
            BtnAddNew.Tag.ShowDialog()
        End If
    End Sub

    Private Sub FrmPacking_BaseEvent_ApproveDeletion_PreTrans(SearchCode As String) Handles Me.BaseEvent_ApproveDeletion_PreTrans
        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmPacking_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        Topctrl1.FButtonClick(1, True)
    End Sub
End Class
