Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq
Imports Customised.ClsMain

Public Class FrmWeavingReceive
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1SubCode As String = "Penalty"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1Remark As String = "Remark"
    '========================================================================

    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public rowProcess As Integer = 6
    Public rowSubCode As Integer = 7
    Public rowGodown As Integer = 8
    Public rowItemCategory As Integer = 9
    Public rowDimension1 As Integer = 10
    Public rowDimension2 As Integer = 11
    Public rowDimension3 As Integer = 12
    Public rowSize As Integer = 13

    Public rowBuyer As Integer = 0
    Public rowJobOrder As Integer = 1
    Public rowJobOrderSr As Integer = 2
    Public rowSku As Integer = 3
    Public rowQty As Integer = 4
    Public rowUnit As Integer = 5
    Public rowUnitMultiplier As Integer = 6
    Public rowDealQty As Integer = 7
    Public rowDealUnit As Integer = 8
    Public rowBarcode As Integer = 9
    Public rowWeight As Integer = 10
    Public rowActualLength As Integer = 11
    Public rowActualWidth As Integer = 12
    Public rowRemarks As Integer = 13

    Public Const hcProcess As String = "Process"
    Public Const hcSubCode As String = "Party"
    Public Const hcGodown As String = "Godown"
    Public Const hcItemCategory As String = "Item Category"
    Public Const hcDimension1 As String = "Dimension1"
    Public Const hcDimension2 As String = "Dimension2"
    Public Const hcDimension3 As String = "Dimension3"
    Public Const hcSize As String = "Size"
    Public Const hcBuyer As String = "Buyer"
    Public Const hcJobOrder As String = "Job Order"
    Public Const hcJobOrderSr As String = "Job Order Sr"
    Public Const hcQty As String = "Qty"
    Public Const hcUnit As String = "Unit"
    Public Const hcUnitMultiplier As String = "Unit Multiplier"
    Public Const hcDealQty As String = "Deal Qty"
    Public Const hcDealUnit As String = "Deal Unit"
    Public Const hcBarcode As String = "Barcode"
    Public Const hcWeight As String = "Weight"
    Public Const hcActualLength As String = "Actual Length"
    Public Const hcActualWidth As String = "Actual Width"
    Public Const hcRemarks As String = "Remarks"

    Dim mPrevRowIndex As Integer = 0
    Protected WithEvents PnlTotals As Panel
    Protected WithEvents LblTotalAmount As Label
    Protected WithEvents LblTotalAmountText As Label
    Dim DtV_TypeTrnSettings As DataTable
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWeavingReceive))
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LblNature = New System.Windows.Forms.Label()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox()
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalAmount = New System.Windows.Forms.Label()
        Me.LblTotalAmountText = New System.Windows.Forms.Label()
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
        Me.TxtDocId.Location = New System.Drawing.Point(829, 369)
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblDocId
        '
        Me.LblDocId.Location = New System.Drawing.Point(782, 369)
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(914, 350)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 17)
        Me.TabControl1.Size = New System.Drawing.Size(992, 364)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Pnl2)
        Me.TP1.Controls.Add(Me.LblNature)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 338)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblNCatNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.PnlMain, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
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
        Me.Topctrl1.TabIndex = 1
        '
        'PnlMain
        '
        Me.PnlMain.Location = New System.Drawing.Point(1, 3)
        Me.PnlMain.Size = New System.Drawing.Size(490, 329)
        Me.PnlMain.TabIndex = 0
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(913, 366)
        Me.LblV_Type.Size = New System.Drawing.Size(86, 16)
        Me.LblV_Type.Tag = ""
        '
        'LblNCatNature
        '
        Me.LblNCatNature.Location = New System.Drawing.Point(837, 341)
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
        Me.Pnl1.Location = New System.Drawing.Point(4, 405)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 146)
        Me.Pnl1.TabIndex = 0
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(911, 334)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 715
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 383)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(121, 21)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Penalty Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNature
        '
        Me.LblNature.AutoSize = True
        Me.LblNature.BackColor = System.Drawing.Color.Transparent
        Me.LblNature.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNature.Location = New System.Drawing.Point(777, 334)
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
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(493, 3)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(491, 329)
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
        Me.BtnAttachments.Location = New System.Drawing.Point(155, 600)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(34, 23)
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
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(4, 552)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(973, 23)
        Me.PnlTotals.TabIndex = 3021
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(123, 4)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(19, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'FrmWeavingReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmWeavingReceive"
        Me.Text = "Lr Bale Transfer Entry"
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Public WithEvents LblNature As System.Windows.Forms.Label
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Private components As System.ComponentModel.IContainer
    Public mDimensionSrl As Integer
    Public WithEvents Pnl2 As Panel
    Public WithEvents PnlCustomGrid As Panel
    Protected WithEvents BtnAttachments As Button
#End Region
    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "StockHeadDetail,StockHeadDetailSku"
        LogLineTableCsv = "StockHeadDetail_Log,StockHeadDetailSku_Log"

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

            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SubCode, 100, 0, Col1SubCode, True, False)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 210, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl1)
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
        Dgl1.AgMandatoryColumn = Dgl1.Columns(Col1SubCode).Index
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.BackgroundColor = Me.BackColor

        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If

        DglMain.Rows.Add(9)
        DglMain.Item(Col1Head, rowGodown).Value = hcGodown
        DglMain.Item(Col1Head, rowSubCode).Value = hcSubCode

        DglMain.Item(Col1Head, rowItemCategory).Value = hcItemCategory
        DglMain.Item(Col1Head, rowDimension1).Value = hcDimension1
        DglMain.Item(Col1Head, rowDimension2).Value = hcDimension2
        DglMain.Item(Col1Head, rowDimension3).Value = hcDimension3
        DglMain.Item(Col1Head, rowSize).Value = hcSize

        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None

        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        DglMain.Item(Col1Head, rowDimension1).Value = IIf(AgL.PubCaptionDimension1 <> "", AgL.PubCaptionDimension1, hcDimension1)
        DglMain.Item(Col1Head, rowDimension2).Value = IIf(AgL.PubCaptionDimension1 <> "", AgL.PubCaptionDimension2, hcDimension2)
        DglMain.Item(Col1Head, rowDimension3).Value = IIf(AgL.PubCaptionDimension1 <> "", AgL.PubCaptionDimension3, hcDimension3)


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col1Head, 150, 255, Col1Head, True, True)
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

        Dgl2.Rows.Add(18)
        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2.Rows(I).Visible = False
        Next

        Dgl2.Item(Col1Head, rowBarcode).Value = hcBarcode
        Dgl2.Item(Col1Head, rowJobOrder).Value = hcJobOrder
        Dgl2.Item(Col1Head, rowBuyer).Value = hcBuyer
        Dgl2.Item(Col1Head, rowQty).Value = hcQty
        Dgl2.Item(Col1Head, rowUnit).Value = hcUnit
        Dgl2.Item(Col1Head, rowUnitMultiplier).Value = hcUnitMultiplier
        Dgl2.Item(Col1Head, rowDealQty).Value = hcDealQty
        Dgl2.Item(Col1Head, rowDealUnit).Value = hcDealUnit
        Dgl2.Item(Col1Head, rowWeight).Value = hcWeight
        Dgl2.Item(Col1Head, rowActualLength).Value = hcActualLength
        Dgl2.Item(Col1Head, rowActualWidth).Value = hcActualWidth
        Dgl2.Item(Col1Head, rowRemarks).Value = hcRemarks
        Dgl2.Rows(rowRemarks).Height = 50
        Dgl2.Name = "Dgl2"
        Dgl2.Tag = "VerticalGrid"




        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2(Col1HeadOriginal, I).Value = Dgl2(Col1Head, I).Value
        Next



        ApplyUISetting()



        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCustomGrid1.Name = "AgCustomGrid1"

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = " Update StockHead " &
                " SET  " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " Process = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & ", " &
                " SubCode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(Dgl2(Col1Value, rowRemarks).Value) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If AgL.Dman_Execute("Select Count(*) From StockHeadDetail L Where L.DocId = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then
            InsertStockHeadDetail(mSearchCode, 1, Conn, Cmd)
            InsertStock(mSearchCode, 1, 1, Conn, Cmd)
        Else
            UpdateStockHeadDetail(mSearchCode, 1, Conn, Cmd)
            UpdateStock(mSearchCode, 1, 1, Conn, Cmd)
        End If

        GenerateAndInsertBarcode(mSearchCode, Conn, Cmd)
        InsertLedgerHeadDetail(mSearchCode, Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub
    Private Sub InsertStockHeadDetail(DocID As String, Sr As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into StockHeadDetail(DocId, Sr, Item, Qty, Unit, UnitMultiplier, DealQty, DealUnit, Weight,
                ActualLength, ActualWidth,
                Godown, ReferenceDocID, ReferenceTSr, Remarks) 
                Select " & AgL.Chk_Text(mSearchCode) & ", 1 As Sr, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowSku).Tag) & " As Item, 
                " & Val(Dgl2.Item(Col1Value, rowQty).Value) & " As Qty, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowUnit).Value) & " As Unit, 
                " & Val(Dgl2.Item(Col1Value, rowUnitMultiplier).Value) & " As UnitMultiplier, 
                " & Val(Dgl2.Item(Col1Value, rowDealQty).Value) & " As DealQty, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowDealUnit).Value) & " As DealUnit, 
                " & Val(Dgl2.Item(Col1Value, rowWeight).Value) & " As Weight, 
                " & Val(Dgl2.Item(Col1Value, rowActualLength).Value) & " As ActualLength, 
                " & Val(Dgl2.Item(Col1Value, rowActualWidth).Value) & " As ActualWidth, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowGodown).Tag) & " As Godown, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowJobOrder).Tag) & " As ReferenceDocID, 
                " & Val(Dgl2.Item(Col1Value, rowJobOrderSr).Value) & " As ReferenceTSr, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowRemarks).Value) & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into StockHeadDetailSku
                (DocId, Sr, ItemCategory, Dimension1, Dimension2, Dimension3, Size) "
        mQry += " Values(" & AgL.Chk_Text(mSearchCode) & ", " & Sr & ", " &
                " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowItemCategory).Tag) & ", " &
                " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension1).Tag) & ", " &
                " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension2).Tag) & ", " &
                " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension3).Tag) & ", " &
                " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSize).Tag) & ")"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "INSERT INTO StockHeadDetailBarCodeValues (DocID, Sr, BarcodeLastTrnDocID, BarcodeLastTrnSr, 
                BarcodeLastTrnV_Type, BarcodeLastTrnManualRefNo, BarcodeLastTrnSubcode, BarcodeLastTrnProcess, 
                BarcodeCurrentGodown, BarcodeStatus)
                Select '" & DocID & "' As DocId, " & Sr & " As Sr, Bs.LastTrnDocID As BarcodeLastTrnDocID, 
                Bs.LastTrnSr As BarcodeLastTrnSr, Bs.LastTrnV_Type As BarcodeLastTrnV_Type, 
                Bs.LastTrnManualRefNo As BarcodeLastTrnManualRefNo, Bs.LastTrnSubcode As BarcodeLastTrnSubcode, 
                Bs.LastTrnProcess As BarcodeLastTrnProcess, Bs.CurrentGodown As BarcodeCurrentGodown, 
                Bs.Status As BarcodeStatus
                From Barcode B
                LEFT JOIN (SELECT * FROM BarcodeSiteDetail WHERE Div_Code = '" & AgL.PubDivCode & "' 
                            AND Site_Code = '" & AgL.PubSiteCode & "') AS Bs ON B.Code = Bs.Code
                Where B.Code = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowBarcode).Tag) & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub InsertStock(DocID As String, TSr As Integer, Sr As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, Process,
                Barcode, Item, Godown,
                EType_IR, Qty_Iss, Qty_Rec, Unit, ReferenceDocID, ReferenceTSr, UnitMultiplier, DealUnit) 
                Select '" & DocID & "', " & TSr & ", " & Sr & " As Sr, " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ",
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & ",
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowBarcode).Tag) & ", 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowSku).Tag) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowGodown).Tag) & ", 
                'R' As EType_IR, 1 As Qty_Iss, 0 As Qty_Rec, 'Nos' As Unit, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowJobOrder).Tag) & " As ReferenceDocID, 
                " & Val(Dgl2.Item(Col1Value, rowJobOrderSr).Value) & " As ReferenceTSr,
                " & Val(Dgl2.Item(Col1Value, rowUnitMultiplier).Value) & " As UnitMultiplier, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowDealUnit).Value) & " As DealUnit "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into StockProcess(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, Process,
                Barcode, Item, 
                EType_IR, Qty_Iss, Qty_Rec, Unit, ReferenceDocID, ReferenceTSr, 
                StockProcess, StockProcessTSr, StockProcessSr, UnitMultiplier, DealUnit) 
                Select '" & DocID & "', " & TSr & ", " & Sr & " As Sr, " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ",
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & ",
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowBarcode).Tag) & ", 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowSku).Tag) & ", 
                'I' As EType_IR, " & Val(Dgl2.Item(Col1Value, rowQty).Value) & " As Qty_Iss, 
                0 As Qty_Rec, 'Nos' As Unit, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowJobOrder).Tag) & " As ReferenceDocID, 
                " & Val(Dgl2.Item(Col1Value, rowJobOrderSr).Value) & " As ReferenceTSr,
                '" & DocID & "', " & TSr & ", " & Sr & " As Sr ,
                " & Val(Dgl2.Item(Col1Value, rowUnitMultiplier).Value) & " As UnitMultiplier, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowDealUnit).Value) & " As DealUnit "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        If Dgl2.Item(Col1Value, rowBarcode).Tag <> "" Then
            mQry = " UPDATE BarCodeSiteDetail 
                    Set LastTrnDocID = " & AgL.Chk_Text(DocID) & ", 
                    LastTrnSr = " & TSr & ", 
                    LastTrnV_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                    LastTrnManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", 
                    LastTrnSubcode = Null, 
                    LastTrnProcess = Null, 
                    CurrentGodown = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ", 
                    Status = 'Receive' 
                    Where Code = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowBarcode).Tag) & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub UpdateStockHeadDetail(DocID As String, Sr As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = " UPDATE StockHeadDetail " &
                " Set " &
                " Barcode = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowBarcode).Tag) & ", " &
                " Item = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowSku).Tag) & ", " &
                " Qty = " & Val(Dgl2.Item(Col1Value, rowQty).Value) & ", " &
                " Unit = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowUnit).Value) & ", " &
                " UnitMultiplier = " & Val(Dgl2.Item(Col1Value, rowUnitMultiplier).Value) & ", " &
                " DealUnit = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowDealUnit).Value) & ", " &
                " DealQty = " & Val(Dgl2.Item(Col1Value, rowDealQty).Value) & ", " &
                " Weight = " & Val(Dgl2.Item(Col1Value, rowWeight).Value) & ", " &
                " ActualLength = " & Val(Dgl2.Item(Col1Value, rowActualLength).Value) & ", " &
                " ActualWidth = " & Val(Dgl2.Item(Col1Value, rowActualWidth).Value) & ", " &
                " Godown = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowGodown).Tag) & " " &
                " Where DocId = '" & mSearchCode & "' " &
                " And Sr = " & Sr & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Update StockHeadDetailSku " &
                    " SET ItemCategory = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowItemCategory).Tag) & ", " &
                    " Dimension1 = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension1).Tag) & ", " &
                    " Dimension2 = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension2).Tag) & ", " &
                    " Dimension3 = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension3).Tag) & ", " &
                    " Size = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSize).Tag) & " " &
                    " Where DocId = '" & mSearchCode & "' " &
                    " And Sr = " & Sr & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub UpdateStock(DocID As String, TSr As Integer, Sr As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Update Stock Set
                V_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                V_Date = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", 
                V_No = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                RecId = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  
                Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                Site_Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                Subcode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ", 
                Barcode = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowBarcode).Tag) & ", 
                Item = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowSku).Tag) & ", 
                Godown = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowGodown).Tag) & ",
                EType_IR = 'R', 
                Qty_Iss = 0,
                Qty_Rec = " & Val(Dgl2.Item(Col1Value, rowQty).Value) & ",
                Unit = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowUnit).Value) & ",
                UnitMultiplier = " & Val(Dgl2.Item(Col1Value, rowUnitMultiplier).Value) & ",
                DealUnit = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowDealUnit).Value) & ", 
                ReferenceDocID = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowJobOrder).Tag) & ", 
                ReferenceTSr = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowJobOrderSr).Value) & " 
                Where DocId = '" & DocID & "' and TSr =" & TSr & " And Sr =" & Sr & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Update StockProcess Set
                V_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                V_Prefix = " & AgL.Chk_Text(LblPrefix.Text) & ",
                V_Date = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", 
                V_No = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                RecId = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  
                Div_Code = " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                Site_Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                Subcode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ", 
                Barcode = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowBarcode).Tag) & ", 
                Item = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowSku).Tag) & ", 
                EType_IR = 'I', 
                Qty_Iss = " & Val(Dgl2.Item(Col1Value, rowQty).Value) & ",
                Qty_Rec = 0,
                Unit = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowUnit).Value) & ",
                UnitMultiplier = " & Val(Dgl2.Item(Col1Value, rowUnitMultiplier).Value) & ",
                DealUnit = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowDealUnit).Value) & ", 
                ReferenceDocID = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowJobOrder).Tag) & ", 
                ReferenceTSr = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowJobOrderSr).Value) & " 
                Where DocId = '" & DocID & "' and TSr =" & TSr & " And Sr =" & Sr & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub InsertLedgerHeadDetail(DocID As String, ByRef Conn As Object, ByRef Cmd As Object)
        Dim mSr As Integer = 0

        mQry = "Delete From LedgerHeadDetail Where DocId = '" & DocID & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1SubCode, I).Tag) <> "" And Dgl1.Rows(I).Visible = True Then
                mSr += 1
                mQry = "Insert Into LedgerHeadDetail(DocId, Sr, Subcode, Amount, Remarks) "
                mQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                    " " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Tag) & ", " &
                    " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                    " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ""
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub ApplyUISetting()
        If LblV_Type.Tag <> "" Then
            GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
            GetUISetting(Dgl2, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
            GetUISetting(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.HorizontalGrid)


        End If
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False

        Dim DsMain As DataSet
        Dim DsTemp As DataSet

        mQry = " Select P.Name As ProcessName, Sg.Name As PartyName, H.*,
                L.*,  G.Name As GodownName, Sku.Code As SkuCode, Sku.Description As SkuDescription, 
                IC.Description As ItemCategoryDesc, 
                Shds.ItemCategory, Shds.Dimension1, Shds.Dimension2, 
                Shds.Dimension3, Shds.Size, 
                D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                D3.Description as Dimension3Desc, S.Description as SizeDesc, 
                Bc.Description As BarcodeDesc,
                Po.V_Type + '-' + '-' + Po.ManualRefNo As JobOrderNo, Sg1.Name AS SaleToPartyName
                From (Select * From StockHead With (NoLock) Where DocID = '" & mSearchCode & "') H 
                LEFT JOIN SubGroup P On H.Process = P.SubCode 
                LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                LEFT JOIN StockHeadDetail L On H.DocId = L.DocId 
                LEFT JOIN StockHeadDetailSku Shds With (NoLock) On L.DocId = Shds.DocId And L.Sr = Shds.Sr
                LEFT JOIN SubGroup G On L.Godown = G.SubCode
                LEFT JOIN Item Sku ON  L.Item = Sku.Code 
                LEFT JOIN ItemCategory Ic On Shds.ItemCategory = Ic.Code 
                LEFT JOIN Dimension1 D1 On Shds.Dimension1 = D1.Code 
                LEFT JOIN Dimension2 D2 On Shds.Dimension2 = D2.Code 
                LEFT JOIN Dimension3 D3 On Shds.Dimension3 = D3.Code 
                LEFT JOIN Size S On Sku.Size = S.Code  
                LEFT JOIN PurchOrder Po On L.ReferenceDocId = Po.DocId 
                LEFT JOIN Barcode Bc On L.Barcode = Bc.Code 
                LEFT JOIN PurchOrderDetail Pod ON L.ReferenceDocId = Pod.DocId AND L.ReferenceTSr = Pod.Sr
                LEFT JOIN PurchPlanDetail Ppd ON Pod.PurchPlan = Ppd.DocId AND Pod.PurchPlanSr = Ppd.Sr
                LEFT JOIN (
	                SELECT L.GenDocID, L.GenSr, Max(So.SaleToParty) AS SaleToParty
	                FROM PurchPlanDetailBaseSaleOrder L 
	                LEFT JOIN SaleOrder So ON L.SaleInvoice = So.DocID
	                GROUP BY L.GenDocID, L.GenSr
                ) AS VPlanBaseSaleOrder ON Ppd.DocId = VPlanBaseSaleOrder.GenDocID AND Ppd.Sr = VPlanBaseSaleOrder.GenSr
                LEFT JOIN SubGroup Sg1 ON VPlanBaseSaleOrder.SaleToParty = Sg1.Subcode "
        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))

                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))
                DglMain.Item(Col1Value, rowProcess).Tag = AgL.XNull(.Rows(0)("Process"))
                DglMain.Item(Col1Value, rowProcess).Value = AgL.XNull(.Rows(0)("ProcessName"))
                DglMain.Item(Col1Value, rowSubCode).Tag = AgL.XNull(.Rows(0)("SubCode"))
                DglMain.Item(Col1Value, rowSubCode).Value = AgL.XNull(.Rows(0)("PartyName"))
                DglMain.Item(Col1Value, rowGodown).Tag = AgL.XNull(.Rows(0)("Godown"))
                DglMain.Item(Col1Value, rowGodown).Value = AgL.XNull(.Rows(0)("GodownName"))
                DglMain.Item(Col1Value, rowItemCategory).Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                DglMain.Item(Col1Value, rowItemCategory).Value = AgL.XNull(.Rows(0)("ItemCategoryDesc"))
                DglMain.Item(Col1Value, rowDimension1).Tag = AgL.XNull(.Rows(0)("Dimension1"))
                DglMain.Item(Col1Value, rowDimension1).Value = AgL.XNull(.Rows(0)("Dimension1Desc"))
                DglMain.Item(Col1Value, rowDimension2).Tag = AgL.XNull(.Rows(0)("Dimension2"))
                DglMain.Item(Col1Value, rowDimension2).Value = AgL.XNull(.Rows(0)("Dimension2Desc"))
                DglMain.Item(Col1Value, rowDimension3).Tag = AgL.XNull(.Rows(0)("Dimension3"))
                DglMain.Item(Col1Value, rowDimension3).Value = AgL.XNull(.Rows(0)("Dimension3Desc"))
                DglMain.Item(Col1Value, rowSize).Tag = AgL.XNull(.Rows(0)("Size"))
                DglMain.Item(Col1Value, rowSize).Value = AgL.XNull(.Rows(0)("SizeDesc"))

                Dgl2.Item(Col1Value, rowBuyer).Value = AgL.XNull(.Rows(0)("SaleToPartyName"))
                Dgl2.Item(Col1Value, rowJobOrder).Tag = AgL.XNull(.Rows(0)("ReferenceDocId"))
                Dgl2.Item(Col1Value, rowJobOrder).Value = AgL.XNull(.Rows(0)("JobOrderNo"))
                Dgl2.Item(Col1Value, rowJobOrderSr).Value = AgL.XNull(.Rows(0)("ReferenceTSr"))
                Dgl2.Item(Col1Value, rowSku).Tag = AgL.XNull(.Rows(0)("SkuCode"))
                Dgl2.Item(Col1Value, rowSku).Value = AgL.XNull(.Rows(0)("SkuDescription"))
                Dgl2.Item(Col1Value, rowBarcode).Tag = AgL.XNull(.Rows(0)("Barcode"))
                Dgl2.Item(Col1Value, rowBarcode).Value = AgL.XNull(.Rows(0)("BarcodeDesc"))
                Dgl2.Item(Col1Value, rowQty).Value = AgL.VNull(.Rows(0)("Qty"))
                Dgl2.Item(Col1Value, rowUnit).Value = AgL.XNull(.Rows(0)("Unit"))
                Dgl2.Item(Col1Value, rowUnitMultiplier).Value = AgL.VNull(.Rows(0)("UnitMultiplier"))
                Dgl2.Item(Col1Value, rowDealQty).Value = AgL.VNull(.Rows(0)("DealQty"))
                Dgl2.Item(Col1Value, rowDealUnit).Value = AgL.XNull(.Rows(0)("DealUnit"))
                Dgl2.Item(Col1Value, rowActualLength).Value = AgL.VNull(.Rows(0)("ActualLength"))
                Dgl2.Item(Col1Value, rowActualWidth).Value = AgL.VNull(.Rows(0)("ActualWidth"))
                Dgl2.Item(Col1Value, rowWeight).Value = AgL.VNull(.Rows(0)("Weight"))
                Dgl2.Item(Col1Value, rowRemarks).Value = AgL.XNull(.Rows(0)("Remarks"))

                AgCustomGrid1.FMoveRecFooterTable(DsMain.Tables(0))

                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
                '-------------------------------------------------------------
            End If
        End With

        mQry = "Select L.*, Sg.Name as AccountName
                From (Select * From LedgerHeadDetail  Where DocId = '" & SearchCode & "') As L 
                LEFT JOIN viewHelpSubgroup Sg  With (NoLock) ON L.Subcode = Sg.Code 
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
                    Dgl1.Item(Col1SubCode, I).Tag = AgL.XNull(.Rows(I)("Subcode"))
                    Dgl1.Item(Col1SubCode, I).Value = AgL.XNull(.Rows(I)("AccountName"))
                    Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                Next I
            End If
        End With

        SetAttachmentCaption()
        Dgl1.ReadOnly = True
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCustomGrid1.FrmType = Me.FrmType
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

                FGetProcessFromVoucher_Type()

                IniGrid()
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

            Case rowReferenceNo
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead",
                                DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)

            Case rowItemCategory, rowDimension1, rowDimension2, rowDimension3, rowSize
                'If DglMain.Item(Col1Value, rowItemCategory).Tag <> "" And
                '        DglMain.Item(Col1Value, rowDimension1).Tag <> "" And
                '        DglMain.Item(Col1Value, rowDimension2).Tag <> "" And
                '        DglMain.Item(Col1Value, rowDimension3).Tag <> "" And
                '        DglMain.Item(Col1Value, rowSize).Tag <> "" Then
                '    FOpenPurchOrderForReceipt()
                'End If
                If DglMain.Item(Col1Value, rowItemCategory).Tag <> "" Then
                    FOpenPurchOrderForReceipt()
                End If
        End Select
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        IniGrid()
        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
        FGetProcessFromVoucher_Type()

        Dgl1.ReadOnly = False

        If DglMain.Visible = True Then
            If DglMain.FirstDisplayedCell IsNot Nothing Then
                If DglMain(Col1Value, rowSettingGroup).Visible = True Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowSettingGroup)
                ElseIf DglMain(Col1Value, rowSubCode).Visible = True Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowSubCode)
                Else
                    DglMain.CurrentCell = DglMain(Col1Value, DglMain.FirstDisplayedCell.RowIndex)
                End If
                DglMain.Focus()
            End If
        End If

        If Dgl2.Rows.Count > 0 Then
            Dgl2.Item(Col1Value, rowBuyer).ReadOnly = True
            Dgl2.Item(Col1Value, rowJobOrder).ReadOnly = True
            Dgl2.Item(Col1Value, rowUnit).ReadOnly = True
            Dgl2.Item(Col1Value, rowDealQty).ReadOnly = True
            Dgl2.Item(Col1Value, rowDealUnit).ReadOnly = True
        End If

        SetAttachmentCaption()
    End Sub



    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean

        'If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1SubCode).Index) Then passed = False : Exit Sub


        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1SubCode, I).Value <> "" Then

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
    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
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
    Private Sub FrmWeavingReceive_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer
        If Dgl1.AgHelpDataSet(Col1SubCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1SubCode).Dispose() : Dgl1.AgHelpDataSet(Col1SubCode) = Nothing

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
    Private Sub FrmWeavingReceive_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            Dgl1.CurrentCell = Dgl1.Item(Col1SubCode, Dgl1.Rows.Count - 1) : Dgl1.Focus()
        End If
    End Sub

    Private Sub FrmWeavingReceive_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
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
    End Sub
    Private Sub FrmWeavingReceive_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        mQry = " Select B.*
                From Barcode B With (NoLock)
                LEFT JOIN BarcodeSiteDetail Bs With (NoLock) On B.Code = Bs.Code
                Where B.Code In (Select Barcode from StockHeadDetail Where DocID = '" & mSearchCode & "' And Barcode Is Not Null)
                And Bs.LastTrnDocID <> '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            MsgBox("Bale No " + AgL.XNull(DtTemp.Rows(0)("Specification1")) + " processed to another Process.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If


        Dgl1.ReadOnly = False

        If Dgl2.Rows.Count > 0 Then
            Dgl2.Item(Col1Value, rowBuyer).ReadOnly = True
            Dgl2.Item(Col1Value, rowJobOrder).ReadOnly = True
            Dgl2.Item(Col1Value, rowUnit).ReadOnly = True
            Dgl2.Item(Col1Value, rowDealQty).ReadOnly = True
            Dgl2.Item(Col1Value, rowDealUnit).ReadOnly = True
        End If
    End Sub
    Private Sub Dgl1_DoubleClick(sender As Object, e As EventArgs) Handles Dgl1.DoubleClick
        If Topctrl1.Mode = "Browse" Then
            Dgl1.CurrentRow.Selected = True
        End If
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
                If AgL.XNull(Dgl2(Col1Value, mRow).Value) = "" Then
                    MsgBox(Dgl2(Col1Head, mRow).Value & " can Not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
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
    Private Sub FrmWeavingReceive_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From StockProcess Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From StockHeadDetailBarCodeValues Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From BarcodeSiteDetail 
                Where Code In (Select Code From Barcode Where GenDocId = '" & mSearchCode & "')"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " UPDATE StockHeadDetail Set Barcode = Null Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Barcode Where GenDocID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmWeavingReceive_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        mQry = " Select B.*
                From Barcode B With (NoLock)
                LEFT JOIN BarcodeSiteDetail Bs With (NoLock) On B.Code = Bs.Code
                Where B.Code In (Select Barcode from StockHeadDetail Where DocID = '" & mSearchCode & "' And Barcode Is Not Null)
                And Bs.LastTrnDocID <> '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            MsgBox("Bale No " + AgL.XNull(DtTemp.Rows(0)("Specification1")) + " processed to another Process.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If
    End Sub
    Private Sub FrmStockHeadEntry_BaseEvent_DglMainEditingControlKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainEditingControlKeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex

            Select Case DglMain.CurrentCell.RowIndex
                Case rowProcess
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpProcess()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowSubCode
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpSubgroup()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowGodown
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Godown & "' Order By Name"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowItemCategory
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpItemCategory()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowDimension1
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpDimension1()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowDimension2
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpDimension2()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowDimension3
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpDimension3()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowSize
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpSize()
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
    Private Function FCreateHelpSubgroup() As DataSet
        Dim strCond As String = ""

        Dim bFilterInclude_AcGroup As String = FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General)
        If bFilterInclude_AcGroup <> "" Then
            If bFilterInclude_AcGroup.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || H.GroupCode,'" & bFilterInclude_AcGroup & "') > 0 "
            ElseIf bFilterInclude_AcGroup.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || H.GroupCode,'" & bFilterInclude_AcGroup & "') <= 0 "
            End If
        End If

        Dim bFilterInclude_Nature As String = FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General)
        If bFilterInclude_Nature <> "" Then
            If bFilterInclude_Nature.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || H.Nature,'" & bFilterInclude_Nature & "') > 0 "
            ElseIf bFilterInclude_Nature.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || H.Nature,'" & bFilterInclude_Nature & "') <= 0 "
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

        mQry = " SELECT H.SubCode, H.Name || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS [Party], " &
                " H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  With (NoLock) " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " Left Join SubgroupProcess SP On H.Subcode = SP.Subcode " &
                " Left Join SubGroup P On Sp.Process = P.Subcode " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        mQry += " Union All SELECT H.SubCode, H.Name || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS [Party], " &
                " H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  With (NoLock) " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " Left Join SubgroupProcess SP On H.Subcode = SP.Subcode " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                " And H.Nature In ('" & ClsMain.SubGroupNature.Customer & "')    "
        If bFilterInclude_Process <> "" Then
            mQry += " And CharIndex('+' || IfNull(Sp.Process,'.'),'" & bFilterInclude_Process & "') > 0 "
        End If

        'sender.AgHelpDataSet(2, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim bNCat As String = ""
        If LblV_Type.Tag <> "" Then bNCat = LblV_Type.Tag Else bNCat = EntryNCat

        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag, "PURCH", bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowProcess).Tag, "")
        FGetSettings = mValue
    End Function
    Private Function FCreateHelpDimension1() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Dimension1 I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension2() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Dimension2 I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension3() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Dimension3 I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension4() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Dimension4 I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpItemCategory() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""



        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            If FilterInclude_ItemType.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            ElseIf FilterInclude_ItemType.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM ItemCategory I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpSize() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            If FilterInclude_ItemType.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            ElseIf FilterInclude_ItemType.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
            End If
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.SIZE & "' "

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1Size) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Sub FOpenPurchOrderForReceipt()
        Dim DtTemp As DataTable
        Dim StrRtn As String = ""
        Dim bPendingOrderQry As String = ""

        bPendingOrderQry = " SELECT VOrder.PurchOrder, VOrder.PurchOrderSr, IsNull(VOrder.OrderQty,0) - IsNull(VReceive.ReceiveQty,0) AS BalanceQty
                FROM (
                    SELECT L.PurchOrder, L.PurchOrderSr, Sum(L.Qty) AS OrderQty
                    FROM PurchOrder H 
                    LEFT JOIN PurchOrderDetail L ON H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    Where H.Vendor = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "'
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

        mQry = " Select L.DocID || '#' || Cast(L.Sr as Varchar) As SearchKey, 
                L.DocId, L.Sr,
                H.V_Type || '-' || H.ManualRefNo As PurchOrderNo, H.V_Date As PurchOrderDate, 
                Sku.ItemCategory As ItemCategoryCode, Ic.Description As ItemCategory,
                Sku.Dimension1 As Dimension1Code, D1.Description As Dimension1,
                Sku.Dimension2 As Dimension2Code, D2.Description As Dimension2,
                Sku.Dimension3 As Dimension3Code, D3.Description As Dimension3,
                Sku.Size As SizeCode, Size.Description As Size,
                L.Item, VPendingOrder.BalanceQty, L.Unit, L.UnitMultiplier, L.DealUnit,
                VPendingOrder.BalanceQty * L.UnitMultiplier As DealQty, Sg1.Name AS SaleToPartyName
                FROM (" & bPendingOrderQry & ") As VPendingOrder
                LEFT JOIN PurchOrderDetail L On VPendingOrder.PurchOrder = L.DocId And VPendingOrder.PurchOrderSr = L.Sr 
                LEFT JOIN PurchOrder H On L.DocId = H.DocId 
                LEFT JOIN Item Sku ON Sku.Code = L.Item
                Left Join Item IC On Sku.ItemCategory = IC.Code
                LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                LEFT JOIN Item Size ON Sku.Size = Size.Code
                LEFT JOIN PurchPlanDetail Ppd ON L.PurchPlan = Ppd.DocId AND L.PurchPlanSr = Ppd.Sr
                LEFT JOIN (
	                SELECT L.GenDocID, L.GenSr, Max(So.SaleToParty) AS SaleToParty
	                FROM PurchPlanDetailBaseSaleOrder L 
	                LEFT JOIN SaleOrder So ON L.SaleInvoice = So.DocID
	                GROUP BY L.GenDocID, L.GenSr
                ) AS VPlanBaseSaleOrder ON Ppd.DocId = VPlanBaseSaleOrder.GenDocID AND Ppd.Sr = VPlanBaseSaleOrder.GenSr
                LEFT JOIN SubGroup Sg1 ON VPlanBaseSaleOrder.SaleToParty = Sg1.Subcode
                Where 1=1 "

        If AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag) <> "" Then
            mQry += " And Sku.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowDimension1).Tag) <> "" Then
            mQry += " And Sku.Dimension1 = '" & DglMain.Item(Col1Value, rowDimension1).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowDimension2).Tag) <> "" Then
            mQry += " And Sku.Dimension2 = '" & DglMain.Item(Col1Value, rowDimension2).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowDimension3).Tag) <> "" Then
            mQry += " And Sku.Dimension3 = '" & DglMain.Item(Col1Value, rowDimension3).Tag & "'"
        End If
        If AgL.XNull(DglMain.Item(Col1Value, rowSize).Tag) <> "" Then
            mQry += " And Sku.Size = '" & DglMain.Item(Col1Value, rowSize).Tag & "'"
        End If

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(DtTemp), "", 400, 950, , , False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, , 0, , False)
        FRH_Single.FFormatColumn(2, , 0, , False)
        FRH_Single.FFormatColumn(3, "Order No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(4, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(5, , 0, , False)
        FRH_Single.FFormatColumn(6, "Item Category", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(7, , 0, , False)
        FRH_Single.FFormatColumn(8, AgL.PubCaptionDimension1, 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(9, , 0, , False)
        FRH_Single.FFormatColumn(10, AgL.PubCaptionDimension2, 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(11, , 0, , False)
        FRH_Single.FFormatColumn(12, AgL.PubCaptionDimension3, 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(13, , 0, , False)
        FRH_Single.FFormatColumn(14, "Size", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(15, , 0, , False)
        FRH_Single.FFormatColumn(16, "Bal Qty", 80, DataGridViewContentAlignment.MiddleRight)
        FRH_Single.FFormatColumn(17, "Unit", 70, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(18, , 0, , False)
        FRH_Single.FFormatColumn(19, , 0, , False)
        FRH_Single.FFormatColumn(20, , 0, , False)
        FRH_Single.FFormatColumn(21, , 0, , False)

        FRH_Single.StartPosition = FormStartPosition.CenterScreen
        FRH_Single.ShowDialog()

        If FRH_Single.BytBtnValue = 0 Then
            StrRtn = FRH_Single.DRReturn("SearchKey")
        End If

        Dim DrSelected As DataRow()
        If StrRtn <> "" Then
            DrSelected = DtTemp.Select("SearchKey = '" & StrRtn & "'")

            Dgl2.Item(Col1Value, rowJobOrder).Tag = AgL.XNull(DrSelected(0)("DocId"))
            Dgl2.Item(Col1Value, rowJobOrder).Value = AgL.XNull(DrSelected(0)("PurchOrderNo"))
            Dgl2.Item(Col1Value, rowJobOrderSr).Value = AgL.XNull(DrSelected(0)("Sr"))
            DglMain.Item(Col1Value, rowItemCategory).Tag = AgL.XNull(DrSelected(0)("ItemCategoryCode"))
            DglMain.Item(Col1Value, rowItemCategory).Value = AgL.XNull(DrSelected(0)("ItemCategory"))
            DglMain.Item(Col1Value, rowDimension1).Tag = AgL.XNull(DrSelected(0)("Dimension1Code"))
            DglMain.Item(Col1Value, rowDimension1).Value = AgL.XNull(DrSelected(0)("Dimension1"))
            DglMain.Item(Col1Value, rowDimension2).Tag = AgL.XNull(DrSelected(0)("Dimension2Code"))
            DglMain.Item(Col1Value, rowDimension2).Value = AgL.XNull(DrSelected(0)("Dimension2"))
            DglMain.Item(Col1Value, rowDimension3).Tag = AgL.XNull(DrSelected(0)("Dimension3Code"))
            DglMain.Item(Col1Value, rowDimension3).Value = AgL.XNull(DrSelected(0)("Dimension3"))
            DglMain.Item(Col1Value, rowSize).Tag = AgL.XNull(DrSelected(0)("SizeCode"))
            DglMain.Item(Col1Value, rowSize).Value = AgL.XNull(DrSelected(0)("Size"))
            Dgl2.Item(Col1Value, rowSku).Tag = AgL.XNull(DrSelected(0)("Item"))
            Dgl2.Item(Col1Value, rowQty).Value = AgL.XNull(DrSelected(0)("BalanceQty"))
            Dgl2.Item(Col1Value, rowUnit).Value = AgL.XNull(DrSelected(0)("Unit"))
            Dgl2.Item(Col1Value, rowUnitMultiplier).Value = AgL.XNull(DrSelected(0)("UnitMultiplier"))
            Dgl2.Item(Col1Value, rowDealQty).Value = AgL.XNull(DrSelected(0)("DealQty"))
            Dgl2.Item(Col1Value, rowDealUnit).Value = AgL.XNull(DrSelected(0)("DealUnit"))
            Dgl2.Item(Col1Value, rowBuyer).Value = AgL.XNull(DrSelected(0)("SaleToPartyName"))
        End If
    End Sub
    Private Sub FGetProcessFromVoucher_Type()
        If DglMain.Item(Col1Head, rowProcess).Tag Is Nothing Then
            DglMain.Item(Col1Head, rowProcess).Tag = FCreateHelpProcess()
        End If

        If CType(DglMain.Item(Col1Head, rowProcess).Tag, DataSet).Tables(0).Rows.Count = 1 Then
            DglMain.Item(Col1Value, rowProcess).Tag = CType(DglMain.Item(Col1Head, rowProcess).Tag, DataSet).Tables(0).Rows(0)("Code")
        Else
            If LblV_Type.Tag = Ncat.PurchaseOrder Or LblV_Type.Tag = Ncat.PurchaseOrderCancel Or
                LblV_Type.Tag = Ncat.PurchaseInvoice Or LblV_Type.Tag = Ncat.PurchaseReturn Then
                DglMain.Item(Col1Value, rowProcess).Tag = Process.Purchase
            End If
        End If

        If AgL.XNull(DglMain.Item(Col1Value, rowProcess).Tag) <> "" Then
            DglMain.Item(Col1Value, rowProcess).Value = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup 
                                Where SubCode = '" & DglMain.Item(Col1Value, rowProcess).Tag & "'", AgL.GCn).ExecuteScalar())
        End If
    End Sub
    Private Function FCreateHelpProcess() As DataSet
        Dim strCond As String = ""

        Dim FilterInclude_Process As String = FGetSettings(SettingFields.FilterInclude_Process, SettingType.General)
        If FilterInclude_Process <> "" Then
            strCond += " And (CharIndex('+' || Sg.Code,'" & FilterInclude_Process & "') > 0 Or
                                CharIndex('+' || Sg.Parent,'" & FilterInclude_Process & "') > 0) "
        End If

        mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) 
                Where SubgroupType ='" & SubgroupType.Process & "' 
                And IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        FCreateHelpProcess = AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Sub GenerateAndInsertBarcode(DocID As String, ByRef Conn As Object, ByRef Cmd As Object)
        Dim DtStock As DataTable
        Dim I As Integer
        Dim mBarcodeCode$ = ""
        mQry = "Select * From Stock With (NoLock) Where DocID = '" & DocID & "'"
        DtStock = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        If DtStock.Rows.Count > 0 Then
            For I = 0 To DtStock.Rows.Count - 1
                Dim BarcodeCntForDocIdSr As Integer = 0
                BarcodeCntForDocIdSr = AgL.Dman_Execute("Select Count(*) From BarCode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & "", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar
                If BarcodeCntForDocIdSr = 0 Then
                    mBarcodeCode = InsertBarCodes(DocID, AgL.VNull(DtStock.Rows(I)("Sr")), AgL.XNull(DtStock.Rows(I)("Item")), AgL.VNull(DtStock.Rows(I)("Qty_Rec")), 1, BarcodeType.UniquePerPcs, Conn, Cmd)

                    mQry = " UPDATE StockHeadDetail Set Barcode = '" & mBarcodeCode & "' Where DocId = '" & DocID & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = " UPDATE Stock Set Barcode = '" & mBarcodeCode & "' Where DocId = '" & DocID & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = " UPDATE StockProcess Set Barcode = '" & mBarcodeCode & "' Where DocId = '" & DocID & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    mQry = "UPDATE Barcode 
                            Set Description = '" & Dgl2.Item(Col1Value, rowBarcode).Value & "',
                            Item = '" & AgL.XNull(DtStock.Rows(I)("Item")) & "'
                            Where GenDocId = '" & DocID & "' 
                            And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End If
    End Sub
    Private Function InsertBarCodes(mDocId As String, mSr As Integer, mItemCode As String, mQty As Integer, mLotQty As Integer, BarcodeType As String, ByRef Conn As Object, ByRef Cmd As Object) As String
        Dim J As Integer = 0
        Dim mBarcodeCode$ = ""
        Dim mBarcodeDesc$ = Dgl2.Item(Col1Value, rowBarcode).Value

        mBarcodeCode = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode  With (NoLock)", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()
        mQry = " INSERT INTO Barcode (Code, Description, Div_Code, Item, GenDocID, GenSr, Qty, BarcodeType)
                    VALUES (" & AgL.Chk_Text(mBarcodeCode) & ", " & AgL.Chk_Text(mBarcodeDesc) & ", " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(mItemCode) & ",
                    " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & mLotQty & ", " & AgL.Chk_Text(BarcodeType) & ") "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO BarcodeSiteDetail (Code,Div_Code, Site_Code, LastTrnDocID,
                LastTrnSr, LastTrnV_Type, LastTrnManualRefNo,
                LastTrnSubcode, LastTrnProcess, CurrentGodown, Status, CurrentStock)
                VALUES (" & AgL.Chk_Text(mBarcodeCode) & ", " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                " & AgL.Chk_Text(mSearchCode) & ", " & Val(mSr) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowProcess).Tag) & ", Null, 'Receive', " & mLotQty & ") "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        InsertBarCodes = mBarcodeCode
    End Function
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1SubCode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1SubCode) Is Nothing Then
                            FCreateHelpPenalty(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FCreateHelpPenalty(RowIndex As Integer)
        Dim strCond As String = ""

        Dim bFilterInclude_AcGroup As String = FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General)
        If bFilterInclude_AcGroup <> "" Then
            If bFilterInclude_AcGroup.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || H.GroupCode,'" & bFilterInclude_AcGroup & "') > 0 "
            ElseIf bFilterInclude_AcGroup.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || H.GroupCode,'" & bFilterInclude_AcGroup & "') <= 0 "
            End If
        End If

        strCond += " And H.SubGroupType = '" & SubgroupType.LedgerAccount & "' "

        mQry = "SELECT H.Code, H.Name
                FROM ViewHelpSubgroup H  With (NoLock)
                Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1SubCode) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        If Topctrl1.Mode = "Browse" Then Exit Sub
        LblTotalAmount.Text = 0
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1SubCode, I).Value <> "" Then
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.visible = False
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Calculation()
    End Sub
End Class
