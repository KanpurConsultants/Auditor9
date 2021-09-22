Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq
Imports Customised.ClsMain

Public Class FrmFeeReceiptEntry
    Inherits AgTemplate.TempTransaction1
    Dim mQry$


    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid


    Public Const ColSNo As String = "S.No."
    Public Const Col1Comp_Code As String = "Session"
    Public Const Col1Class As String = "Class"
    Public Const Col1Fee As String = "Fee"
    Public Const Col1SubHead As String = "Sub Head"
    Public Const Col1DueDate As String = "Due Date"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1AdjustedAmount As String = "Adjusted Amount"
    Public Const Col1IsFeeDueExplicitly As String = "Is Fee Due Explicitly"
    '========================================================================

    Public Const hcClass As String = "Class"
    Public Const hcStudent As String = "Student"
    Public Const hcPaymentAc As String = "Payment A/c"
    Public Const hcRemarks As String = "Remarks"

    Dim rowClass As Integer = 6
    Dim rowStudent As Integer = 7
    Dim rowPaymentAc As Integer = 8
    Dim rowRemarks As Integer = 9

    Dim rowTotalDueAmount As Integer = 0
    Dim rowLateFeeCalculated As Integer = 1
    Dim rowLateFee As Integer = 2
    Dim rowDiscount As Integer = 3
    Dim rowPayableAmount As Integer = 4
    Dim rowPaidAmount As Integer = 5

    Public Const hcTotalDueAmount As String = "Total Due Amount"
    Public Const hcLateFeeCalculated As String = "Late Fee Calculated"
    Public Const hcLateFee As String = "Late Fee"
    Public Const hcDiscount As String = "Discount"
    Public Const hcPayableAmount As String = "Payable Amount"
    Friend WithEvents ChkAdjustAmountManually As CheckBox
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalAdjustedAmount As Label
    Public WithEvents LblTotalAdjustedAmountText As Label
    Public Const hcPaidAmount As String = "Paid Amount"
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFeeReceiptEntry))
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.LblCurrency = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LblNature = New System.Windows.Forms.Label()
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox()
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.ChkAdjustAmountManually = New System.Windows.Forms.CheckBox()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalAdjustedAmount = New System.Windows.Forms.Label()
        Me.LblTotalAdjustedAmountText = New System.Windows.Forms.Label()
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
        Me.TabControl1.Size = New System.Drawing.Size(992, 241)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Pnl2)
        Me.TP1.Controls.Add(Me.LblNature)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 215)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblNCatNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.PnlMain, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
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
        Me.PnlMain.Size = New System.Drawing.Size(490, 209)
        Me.PnlMain.TabIndex = 0
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(465, 222)
        Me.LblV_Type.Size = New System.Drawing.Size(86, 16)
        Me.LblV_Type.Tag = ""
        '
        'ChkTemporarilySaved
        '
        Me.ChkTemporarilySaved.Location = New System.Drawing.Point(78, 587)
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
        Me.Pnl1.Location = New System.Drawing.Point(4, 281)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 272)
        Me.Pnl1.TabIndex = 0
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
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 260)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(131, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Fee Receipt Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNature
        '
        Me.LblNature.AutoSize = True
        Me.LblNature.BackColor = System.Drawing.Color.Transparent
        Me.LblNature.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNature.Location = New System.Drawing.Point(622, 219)
        Me.LblNature.Name = "LblNature"
        Me.LblNature.Size = New System.Drawing.Size(46, 16)
        Me.LblNature.TabIndex = 745
        Me.LblNature.Text = "Nature"
        Me.LblNature.Visible = False
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
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(493, 3)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(490, 209)
        Me.Pnl2.TabIndex = 3005
        '
        'ChkAdjustAmountManually
        '
        Me.ChkAdjustAmountManually.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChkAdjustAmountManually.AutoSize = True
        Me.ChkAdjustAmountManually.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAdjustAmountManually.Location = New System.Drawing.Point(793, 261)
        Me.ChkAdjustAmountManually.Name = "ChkAdjustAmountManually"
        Me.ChkAdjustAmountManually.Size = New System.Drawing.Size(184, 17)
        Me.ChkAdjustAmountManually.TabIndex = 3021
        Me.ChkAdjustAmountManually.Text = "Adjust Amount Manually"
        Me.ChkAdjustAmountManually.UseVisualStyleBackColor = True
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalAdjustedAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalAdjustedAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(5, 554)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(972, 23)
        Me.PnlTotals.TabIndex = 3022
        '
        'LblTotalAdjustedAmount
        '
        Me.LblTotalAdjustedAmount.AutoSize = True
        Me.LblTotalAdjustedAmount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAdjustedAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAdjustedAmount.Location = New System.Drawing.Point(195, 4)
        Me.LblTotalAdjustedAmount.Name = "LblTotalAdjustedAmount"
        Me.LblTotalAdjustedAmount.Size = New System.Drawing.Size(13, 16)
        Me.LblTotalAdjustedAmount.TabIndex = 662
        Me.LblTotalAdjustedAmount.Text = "."
        Me.LblTotalAdjustedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAdjustedAmountText
        '
        Me.LblTotalAdjustedAmountText.AutoSize = True
        Me.LblTotalAdjustedAmountText.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAdjustedAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAdjustedAmountText.Location = New System.Drawing.Point(15, 3)
        Me.LblTotalAdjustedAmountText.Name = "LblTotalAdjustedAmountText"
        Me.LblTotalAdjustedAmountText.Size = New System.Drawing.Size(165, 14)
        Me.LblTotalAdjustedAmountText.TabIndex = 661
        Me.LblTotalAdjustedAmountText.Text = "Total Adjusted Amount :"
        '
        'FrmFeeReceiptEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.ChkAdjustAmountManually)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmFeeReceiptEntry"
        Me.Text = "Fee Receipt Entry"
        Me.Controls.SetChildIndex(Me.ChkTemporarilySaved, 0)
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.ChkAdjustAmountManually, 0)
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
    Public WithEvents LblCurrency As System.Windows.Forms.Label
    Public WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Public WithEvents LblNature As System.Windows.Forms.Label
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Private components As System.ComponentModel.IContainer
    Public mDimensionSrl As Integer
    Public WithEvents PnlCustomGrid As Panel
    Protected WithEvents BtnAttachments As Button
    Public WithEvents Pnl2 As Panel
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "LedgerHead"
        LogTableName = "LedgerHead_Log"
        MainLineTableCsv = "LedgerHeadDetail"
        LogLineTableCsv = "LedgerHeadDetail_Log"
    End Sub
    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        mQry = "Select DocID As SearchCode 
                From LedgerHead H  With (NoLock)
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

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [LedgerHead_Type], Cast(strftime('%d/%m/%Y', H.V_Date) As nvarchar) AS Date, SGV.Name AS [Party], " &
                            " H.ManualRefNo AS [Manual_No], H.Remarks,  " &
                            " H.EntryBy AS [Entry_By], Cast(strftime('%d/%m/%Y', H.EntryDate) As nvarchar) AS [Entry_Date] " &
                            " FROM LedgerHead H  With (NoLock) " &
                            " LEFT JOIN Voucher_Type Vt  With (NoLock) ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN ViewHelpSubgroup SGV  With (NoLock) ON SGV.Code  = H.SubCode " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Comp_Code, 100, 0, Col1Comp_Code, True, True)
            .AddAgTextColumn(Dgl1, Col1Class, 100, 0, Col1Class, True, True)
            .AddAgTextColumn(Dgl1, Col1Fee, 100, 0, Col1Fee, True, True)
            .AddAgTextColumn(Dgl1, Col1SubHead, 100, 0, Col1SubHead, True, True)
            .AddAgTextColumn(Dgl1, Col1DueDate, 100, 0, Col1DueDate, True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 110, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1AdjustedAmount, 110, 8, 2, False, Col1AdjustedAmount, True, False, True)
            .AddAgTextColumn(Dgl1, Col1IsFeeDueExplicitly, 100, 0, Col1IsFeeDueExplicitly, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl1)
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AgLastColumn = Dgl1.Columns(Col1Amount).Index
        Dgl1.AgMandatoryColumn = Dgl1.Columns(Col1Fee).Index
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.BackgroundColor = Me.BackColor

        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If


        DglMain.Columns(Col1BtnDetail).ReadOnly = True
        DglMain.Columns(Col1BtnDetail).Visible = False
        DglMain.Columns(Col1Head).Width = 105
        DglMain.Rows.Add(4)
        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next
        DglMain.Item(Col1Head, rowClass).Value = hcClass
        DglMain.Item(Col1Head, rowStudent).Value = hcStudent
        DglMain.Item(Col1Head, rowPaymentAc).Value = hcPaymentAc
        DglMain.Item(Col1Head, rowRemarks).Value = hcRemarks


        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None

        For I = 0 To DglMain.Rows.Count - 1
            If AgL.XNull(DglMain(Col1HeadOriginal, I).Value) = "" Then
                DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
            End If
        Next

        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col1Head, 250, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl2, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl2, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl2, Col1Value, 170, 255, Col1Value, True, False)
            .AddAgTextColumn(Dgl2, Col1LastValue, 170, 255, Col1LastValue, False, False)
            .AddAgButtonColumn(Dgl2, Col1BtnDetail, 35, Col1BtnDetail, False, True)
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
        Dgl2.BackgroundColor = Me.BackColor
        Dgl2.BorderStyle = BorderStyle.None

        Dgl2.Rows.Add(6)

        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2.Rows(I).Visible = False
        Next

        Dgl2.Item(Col1Head, rowTotalDueAmount).Value = hcTotalDueAmount
        Dgl2.Item(Col1Head, rowLateFeeCalculated).Value = hcLateFeeCalculated
        Dgl2.Item(Col1Head, rowLateFee).Value = hcLateFee
        Dgl2.Item(Col1Head, rowDiscount).Value = hcDiscount
        Dgl2.Item(Col1Head, rowPayableAmount).Value = hcPayableAmount
        Dgl2.Item(Col1Head, rowPaidAmount).Value = hcPaidAmount

        Dgl2.Name = "Dgl2"
        Dgl2.Tag = "VerticalGrid"
        Dgl2.Columns(Col1Value).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Dgl2.Item(Col1Value, rowPaidAmount).Style.Font = New Font(Dgl2.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold)
        Dgl2.Item(Col1Value, rowPaidAmount).Style.ForeColor = Color.Blue
        Dgl2.Item(Col1Head, rowPaidAmount).Style.Font = New Font(Dgl2.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold)
        Dgl2.Item(Col1Head, rowPaidAmount).Style.ForeColor = Color.Blue


        For I = 0 To Dgl2.Rows.Count - 1
            If AgL.XNull(Dgl2(Col1HeadOriginal, I).Value) = "" Then
                Dgl2(Col1HeadOriginal, I).Value = Dgl2(Col1Head, I).Value
            End If
        Next

        ApplyUISetting()

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bStockSelectionQry$ = "", bHelpValuesSelectionQry$ = ""

        mQry = " Update LedgerHead " &
                " SET  " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " SubCode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & ", " &
                " LateFeeCalculated = " & Val(Dgl2.Item(Col1Value, rowLateFeeCalculated).Value) & ", " &
                " LateFee = " & Val(Dgl2.Item(Col1Value, rowLateFee).Value) & ", " &
                " Discount = " & Val(Dgl2.Item(Col1Value, rowDiscount).Value) & ", " &
                " Remarks = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowRemarks).Value) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From LedgerHeadDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into LedgerHeadDetail(DocId, Sr, SubCode, Amount) 
                Select " & AgL.Chk_Text(mSearchCode) & ", 1, " &
                " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowPaymentAc).Tag) & " As SubCode, " &
                " " & Val(Dgl2.Item(Col1Value, rowPaidAmount).Value) & " As Amount "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From FeeAdjustmentDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        'FAdjustFee()
        InsertLateFeeAdjustment(mSearchCode, Conn, Cmd)
        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From LedgerHeadDetail  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If AgL.XNull(Dgl1.Item(Col1Fee, I).Tag) <> "" Then
                mSr += 1
                InsertFeeAdjustmentDetail(mSearchCode, mSr, I, Conn, Cmd)
            End If
        Next

        FPostInLedger(mSearchCode, Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub
    Private Sub InsertLateFeeAdjustment(DocID As String, ByRef Conn As Object, ByRef Cmd As Object)
        If Val(Dgl2.Item(Col1Value, rowLateFee).Value) > 0 Then
            mQry = "Insert Into FeeAdjustmentDetail(DocId, Sr, Comp_Code, Class, Fee, SubHead, DueDate, Amount, AdjustedAmount, IsFeeDueExplicitly) 
                    Select " & AgL.Chk_Text(DocID) & ", 0, " &
                    " Null As Comp_Code,  
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowClass).Tag) & " As Class, 
                    " & AgL.Chk_Text(ClsSchool.Fee_LateFee) & " As Fee, " &
                    " Null As SubHead, " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As DueDate, " &
                    " " & Val(Dgl2.Item(Col1Value, rowLateFee).Value) & " As Amount, " &
                    " " & Val(Dgl2.Item(Col1Value, rowLateFee).Value) & " As AdjustedAmount, " &
                    " 0 As IsFeeDueExplicitly "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertFeeAdjustmentDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into FeeAdjustmentDetail(DocId, Sr, Comp_Code, Class, Fee, SubHead, DueDate, Amount, AdjustedAmount, IsFeeDueExplicitly) 
                Select " & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Comp_Code, LineGridRowIndex).Tag) & " As Comp_Code, " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Class, LineGridRowIndex).Tag) & " As Class, " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1Fee, LineGridRowIndex).Tag) & " As Fee, " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1SubHead, LineGridRowIndex).Tag) & " As SubHead, " &
                " " & AgL.Chk_Date(Dgl1.Item(Col1DueDate, LineGridRowIndex).Value) & " As DueDate, " &
                " " & Val(Dgl1.Item(Col1Amount, LineGridRowIndex).Value) & " As Amount, " &
                " " & Val(Dgl1.Item(Col1AdjustedAmount, LineGridRowIndex).Value) & " As AdjustedAmount, " &
                " " & Val(Dgl1.Item(Col1IsFeeDueExplicitly, LineGridRowIndex).Value) & " As IsFeeDueExplicitly "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False

        Dim DsMain As DataSet

        mQry = " Select H.*, Sg.Name As StudentName, Class.SubCode As Class, 
                Class.Name As ClassName,
                L.SubCode As PaymentAcCode, LSg.Name As PaymentAcName, L.Amount
                From (Select * From LedgerHead With (NoLock) Where DocID='" & SearchCode & "') H 
                LEFT JOIN ViewHelpSubgroup Sg ON H.SubCode = Sg.Code 
                LEFT JOIN LedgerHeadDetail L On H.DocId = L.DocId 
                LEFT JOIN SubGroup LSg On L.SubCode = LSg.SubCode 
                LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad ON Sg.Code = Sgad.SubCode 
                LEFT JOIN SubGroup Class On Sgad.Class = Class.SubCode "
        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))
                DglMain.Item(Col1Value, rowStudent).Tag = AgL.XNull(.Rows(0)("SubCode"))
                DglMain.Item(Col1Value, rowStudent).Value = AgL.XNull(.Rows(0)("StudentName"))
                DglMain.Item(Col1Value, rowClass).Tag = AgL.XNull(.Rows(0)("Class"))
                DglMain.Item(Col1Value, rowClass).Value = AgL.XNull(.Rows(0)("ClassName"))
                DglMain.Item(Col1Value, rowPaymentAc).Tag = AgL.XNull(.Rows(0)("PaymentAcCode"))
                DglMain.Item(Col1Value, rowPaymentAc).Value = AgL.XNull(.Rows(0)("PaymentAcName"))
                DglMain.Item(Col1Value, rowRemarks).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks")))


                Dgl2.Item(Col1Value, rowLateFeeCalculated).Value = AgL.VNull(.Rows(0)("LateFeeCalculated"))
                Dgl2.Item(Col1Value, rowLateFee).Value = AgL.VNull(.Rows(0)("LateFee"))
                Dgl2.Item(Col1Value, rowDiscount).Value = AgL.VNull(.Rows(0)("Discount"))
                Dgl2.Item(Col1Value, rowPaidAmount).Value = AgL.XNull(.Rows(0)("Amount"))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                Dgl2.Item(Col1Value, rowTotalDueAmount).Value = "0"
                LblTotalAdjustedAmount.Text = "0"

                mQry = "Select L.*, C.Comp_Name As Comp_Name, Class.Name As ClassName, 
                        Fee.Name As FeeName, SubHead.Name As SubHeadName
                        From (Select * From FeeAdjustmentDetail  With (NoLock)  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN Company C On L.Comp_Code = C.Comp_Code
                        LEFT JOIN SubGroup Class On L.Class = Class.SubCode
                        LEFT JOIN SubGroup Fee On L.Fee = Fee.SubCode
                        LEFT JOIN SubGroup SubHead On L.SubHead = SubHead.SubCode
                        Where L.Fee <> '" & ClsSchool.Fee_LateFee & "'
                        Order By L.Sr "
                DsMain = AgL.FillData(mQry, AgL.GCn)
                With DsMain.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsMain.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))

                            Dgl1.Item(Col1Comp_Code, I).Tag = AgL.XNull(.Rows(I)("Comp_Code"))
                            Dgl1.Item(Col1Comp_Code, I).Value = AgL.XNull(.Rows(I)("Comp_Name"))

                            Dgl1.Item(Col1Class, I).Tag = AgL.XNull(.Rows(I)("Class"))
                            Dgl1.Item(Col1Class, I).Value = AgL.XNull(.Rows(I)("ClassName"))

                            Dgl1.Item(Col1Fee, I).Tag = AgL.XNull(.Rows(I)("Fee"))
                            Dgl1.Item(Col1Fee, I).Value = AgL.XNull(.Rows(I)("FeeName"))

                            Dgl1.Item(Col1SubHead, I).Tag = AgL.XNull(.Rows(I)("SubHead"))
                            Dgl1.Item(Col1SubHead, I).Value = AgL.XNull(.Rows(I)("SubHeadName"))

                            Dgl1.Item(Col1DueDate, I).Value = AgL.XNull(.Rows(I)("DueDate"))

                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1AdjustedAmount, I).Value = AgL.VNull(.Rows(I)("AdjustedAmount"))

                            Dgl1.Item(Col1IsFeeDueExplicitly, I).Value = AgL.VNull(.Rows(I)("IsFeeDueExplicitly"))

                            Dgl2.Item(Col1Value, rowTotalDueAmount).Value = Val(Dgl2.Item(Col1Value, rowTotalDueAmount).Value) + Val(Dgl1.Item(Col1Amount, I).Value)
                            LblTotalAdjustedAmount.Text = Val(LblTotalAdjustedAmount.Text) + Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
                        Next I
                    End If
                End With
                '-------------------------------------------------------------
            End If
        End With
        Dgl2.Item(Col1Value, rowPayableAmount).Value = Val(Dgl2.Item(Col1Value, rowTotalDueAmount).Value) + Val(Dgl2.Item(Col1Value, rowLateFee).Value) - Val(Dgl2.Item(Col1Value, rowDiscount).Value)
        SetAttachmentCaption()
        Dgl1.ReadOnly = True
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub
    Private Sub FrmLedgerHeadEntry_BaseEvent_DglMainEditingControlValidating(sender As Object, e As CancelEventArgs) Handles Me.BaseEvent_DglMainEditingControlValidating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = DglMain.CurrentCell.RowIndex
        mColumn = DglMain.CurrentCell.ColumnIndex

        Select Case mRow
            Case rowV_Type
                If DglMain.Item(Col1Value, rowV_Type).Tag = "" Then Exit Sub
                IniGrid()
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

            Case rowReferenceNo
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "LedgerHead",
                                DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)

            Case rowClass
                DglMain.Item(Col1Head, rowStudent).Tag = Nothing
                DglMain.Item(Col1Value, rowStudent).Tag = ""
                DglMain.Item(Col1Value, rowStudent).Value = ""
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()

            Case rowStudent, rowV_Date
                If AgL.XNull(DglMain.Item(Col1Value, rowStudent).Tag) <> "" And
                    AgL.XNull(DglMain.Item(Col1Value, rowV_Date).Value) <> "" Then
                    FFillFeeDetail(DglMain.Item(Col1Value, rowStudent).Tag)
                End If
        End Select
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        IniGrid()
        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)


        Dgl1.ReadOnly = False
        ChkAdjustAmountManually.Checked = False

        SetAttachmentCaption()

        DglMain.CurrentCell = DglMain.Item(Col1Value, rowClass)
        DglMain.Focus()
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Fee).Index) Then passed = False : Exit Sub

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

        If Val(Dgl2.Item(Col1Value, rowPaidAmount).Value) + Val(Dgl2.Item(Col1Value, rowDiscount).Value) > Val(Dgl2.Item(Col1Value, rowTotalDueAmount).Value) + Val(Dgl2.Item(Col1Value, rowLateFee).Value) Then
            MsgBox("Paid Amount can not be greater then " & Val(Dgl2.Item(Col1Value, rowTotalDueAmount).Value) + Val(Dgl2.Item(Col1Value, rowLateFee).Value), MsgBoxStyle.Information)
            Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowPaidAmount) : DglMain.Focus()
            passed = False : Exit Sub
        End If

        If Val(Dgl2.Item(Col1Value, rowPaidAmount).Value) = 0 Then
            MsgBox("Paid Amount can not be 0.", MsgBoxStyle.Information)
            Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowPaidAmount) : Dgl2.Focus()
            passed = False : Exit Sub
        End If

        If Val(Dgl2.Item(Col1Value, rowPaidAmount).Value) < Val(Dgl2.Item(Col1Value, rowLateFee).Value) Then
            MsgBox("Paid Amount can not be less then Late Fee.", MsgBoxStyle.Information)
            Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowPaidAmount) : DglMain.Focus()
            passed = False : Exit Sub
        End If

        Dim bTotalAdjustedAmount As Double = 0
        For I = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1AdjustedAmount, I).Value) > 0 Then
                bTotalAdjustedAmount += Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
            End If
        Next
        bTotalAdjustedAmount = bTotalAdjustedAmount + Val(Dgl2.Item(Col1Value, rowLateFee).Value)

        If Val(Dgl2.Item(Col1Value, rowPaidAmount).Value) + Val(Dgl2.Item(Col1Value, rowDiscount).Value) <> bTotalAdjustedAmount Then
            MsgBox("Paid Amount + Discount should be equal to TotalAdjustedAmount + Late Fee. ", MsgBoxStyle.Information)
            Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowPaidAmount) : DglMain.Focus()
            passed = False : Exit Sub
        End If





        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "LedgerHead",
                                    DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                    DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                    DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)

        If Not CheckDuplicateRef Then
            DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.Item(Col1Value, rowTotalDueAmount).Value = "0"
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
                Case Col1AdjustedAmount
                    If ChkAdjustAmountManually.Checked = True Then
                        Dgl1.Item(Col1AdjustedAmount, Dgl1.CurrentCell.RowIndex).ReadOnly = False
                    Else
                        Dgl1.Item(Col1AdjustedAmount, Dgl1.CurrentCell.RowIndex).ReadOnly = True
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
    Private Sub FrmFeeReceiptEntry_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer
        If Dgl1.AgHelpDataSet(Col1Fee) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Fee).Dispose() : Dgl1.AgHelpDataSet(Col1Fee) = Nothing

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, i).Tag = Nothing
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
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            Dgl1.CurrentCell = Dgl1.Item(Col1Fee, Dgl1.Rows.Count - 1) : Dgl1.Focus()
        End If
    End Sub

    Private Sub FrmFeeReceiptEntry_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
    End Sub
    Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer

        mPrintTitle = AgL.Dman_Execute("Select IfNull(PrintingDescription, Description) From Voucher_Type Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' ", AgL.GCn).ExecuteScalar()

        Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        Dim mDocReportFileName As String = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)


        Dim bPrimaryQry As String = ""
        If BulkCondStr <> "" Then
            bPrimaryQry = " Select * From LedgerHead  With (NoLock) Where DocID In (" & BulkCondStr & ")"
            PrintingCopies = FGetSettings(SettingFields.PrintingBulkCopyCaptions, SettingType.General).ToString.Split(",")
        Else
            bPrimaryQry = " Select * From LedgerHead  With (NoLock) Where DocID = '" & SearchCode & "'"
            PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        End If


        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            mQry = mQry + "
                    Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, 
                    '" & mDocNoCaption & "' as DocNoCaption, 
                    '" & mDocDateCaption & "' as DocDateCaption, 
                    SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, 
                    H.DocID, Fad.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat,                                 
                    '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as ReceiptNo,                 
                    Sg.Name As StudemtName, Class.Name As ClassName, L.Amount As PaidAmount,
                    H.Discount, Fad.DueDate As FeeDueDate,
                    Fee.Name As FeeName, SubHead.Name As SubHeadName, Fad.Amount As FeeDueAmount, Fad.AdjustedAmount As FeeAdjustedAmount,
                    '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                    '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle,
                    '" & FGetSettings(SettingFields.DocumentPrintShowPrintDateTimeYn, SettingType.General) & "' as DocumentPrintShowPrintDateTimeYn,
                    IfNull(L.ChqRefNo,'') as ChqRefNo, IfNull(L.ChqRefDate,'') as ChqRefDate, IfNull(L.Remarks,'') as LRemarks, IfNull(H.Remarks,'') as HRemarks                 
                    From (Select * From LedgerHead  With (NoLock) Where DocID = '" & SearchCode & "') As H 
                    LEFT JOIN LedgerHeadDetail L On H.DocId = L.DocId
                    LEFT JOIN ViewHelpSubgroup Sg On H.SubCode = Sg.Code
                    LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad ON Sg.Code = Sgad.SubCode 
                    LEFT JOIN SubGroup Class On Sgad.Class = Class.SubCode
                    LEFT JOIN FeeAdjustmentDetail Fad On H.DocId = Fad.DocId
                    LEFT JOIN SubGroup Fee ON Fad.Fee = Fee.SubCode 
                    LEFT JOIN SubGroup SubHead On Fad.SubHead = SubHead.SubCode
                    LEFT JOIN Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type 
                    Left Join SiteMast Site On H.Site_Code = Site.Code 
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                    Left Join State SiteState On SiteCity.State = SiteState.Code
                    Where IfNull(Fad.AdjustedAmount,0) <> 0 "
        Next
        mQry = mQry + " Order By Copies, H.DocID, Fad.Sr "


        Dim objRepPrint As Object
        objRepPrint = New AgLibrary.RepView(AgL)

        ClsMain.FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, "FeeReceipt_Print.rpt", mPrintTitle, , , , DglMain.Item(Col1Value, rowStudent).Value, DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
    End Sub
    Private Sub FrmFeeReceiptEntry_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
    End Sub
    Private Sub Dgl1_DoubleClick(sender As Object, e As EventArgs) Handles Dgl1.DoubleClick
        If Topctrl1.Mode = "Browse" Then
            Dgl1.CurrentRow.Selected = True
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

    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub FrmFeeReceiptEntry_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From FeeAdjustmentDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmLedgerHeadEntry_BaseEvent_DglMainEditingControlKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainEditingControlKeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex

            Select Case mRow
                Case rowClass
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                    FROM Subgroup Sg With (NoLock)
                                    Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Class & "' 
                                    And IfNull(Sg.Status,'Active') = 'Active'"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowStudent
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " SELECT Sg.Code AS Code, Sg.Name
                                    FROM ViewHelpSubgroup Sg With (NoLock)
                                    LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad On Sg.Code = Sgad.SubCode
                                    Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Student & "' 
                                    And IfNull(Sg.Status,'Active') = 'Active'
                                    And Sgad.Class = '" & DglMain.Item(Col1Value, rowClass).Tag & "'"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowPaymentAc
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                    FROM Subgroup Sg With (NoLock)
                                    Where Sg.Nature In ('Bank','Cash') 
                                    And IfNull(Sg.Status,'Active') = 'Active'"
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
    Private Sub FrmSaleInvoiceDirect_BaseEvent_DglMainKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainKeyDown
        Try
            If DglMain.CurrentCell IsNot Nothing Then
                If e.KeyCode = Keys.Enter Then
                    Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                    If DglMain.CurrentCell.RowIndex = LastCell.RowIndex Then
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
    Private Sub ApplyUISetting()
        GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(Dgl2, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub FAdjustFee()
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item(Col1AdjustedAmount, I).Value = 0
        Next

        Dim bAmount As Decimal = Val(Dgl2.Item(Col1Value, rowPaidAmount).Value) + Val(Dgl2.Item(Col1Value, rowDiscount).Value)

        If bAmount > 0 And Val(Dgl2.Item(Col1Value, rowLateFee).Value) > 0 Then
            bAmount = bAmount - Val(Dgl2.Item(Col1Value, rowLateFee).Value)
        End If

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If bAmount > 0 And Val(Dgl1.Item(Col1Amount, I).Value) > 0 Then
                If bAmount > Val(Dgl1.Item(Col1Amount, I).Value) Then
                    Dgl1.Item(Col1AdjustedAmount, I).Value = Val(Dgl1.Item(Col1Amount, I).Value)
                ElseIf bAmount < Val(Dgl1.Item(Col1Amount, I).Value) Then
                    Dgl1.Item(Col1AdjustedAmount, I).Value = bAmount
                ElseIf bAmount = Val(Dgl1.Item(Col1Amount, I).Value) Then
                    Dgl1.Item(Col1AdjustedAmount, I).Value = bAmount
                End If
                bAmount = bAmount - Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
            End If
        Next
    End Sub

    Private Sub FFillFeeDetail(bStudent As String)
        mQry = " Select L.Comp_Code, C.Comp_Name, L.Class, Class.Name As ClassName, 
                L.Fee, Fee.Name As FeeName, L.SubHead, SubHead.Name As SubHeadName, L.DueDate, 
                L.FeeAmount, L.ReceivedAmount, L.BalanceAmount, L.IsFeeDueExplicitly, Sg.Discount
                From FeeDueDetail L 
                LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
                LEFT JOIN Company C On L.Comp_Code = C.Comp_Code
                LEFT JOIN SubGroup Class ON L.Class = Class.SubCode
                LEFT JOIN SubGroup Fee On L.Fee = Fee.SubCode
                LEFT JOIN SuBGroup SubHead On L.SubHead = SubHead.SubCode
                Where L.SubCode = '" & bStudent & "'
                And Date(L.DueDate) <= " & AgL.Chk_Date(CDate(DglMain.Item(Col1Value, rowV_Date).Value)) & "
                And IfNull(L.BalanceAmount,0) > 0
                Order By L.DueDate "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        If DtTemp.Rows.Count > 0 Then
            For I As Integer = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count

                Dgl1.Item(Col1Comp_Code, I).Tag = AgL.XNull(DtTemp.Rows(I)("Comp_Code"))
                Dgl1.Item(Col1Comp_Code, I).Value = AgL.XNull(DtTemp.Rows(I)("Comp_Name"))

                Dgl1.Item(Col1Class, I).Tag = AgL.XNull(DtTemp.Rows(I)("Class"))
                Dgl1.Item(Col1Class, I).Value = AgL.XNull(DtTemp.Rows(I)("ClassName"))

                Dgl1.Item(Col1Fee, I).Tag = AgL.XNull(DtTemp.Rows(I)("Fee"))
                Dgl1.Item(Col1Fee, I).Value = AgL.XNull(DtTemp.Rows(I)("FeeName"))

                Dgl1.Item(Col1SubHead, I).Tag = AgL.XNull(DtTemp.Rows(I)("SubHead"))
                Dgl1.Item(Col1SubHead, I).Value = AgL.XNull(DtTemp.Rows(I)("SubHeadName"))

                Dgl1.Item(Col1DueDate, I).Value = AgL.XNull(DtTemp.Rows(I)("DueDate"))

                Dgl1.Item(Col1Amount, I).Value = AgL.VNull(DtTemp.Rows(I)("BalanceAmount"))

                Dgl1.Item(Col1IsFeeDueExplicitly, I).Value = AgL.VNull(DtTemp.Rows(I)("IsFeeDueExplicitly"))

                Dgl2.Item(Col1Value, rowDiscount).Value = AgL.VNull(DtTemp.Rows(I)("Discount"))
            Next I
        End If
        FCalculateLateFee()
        Calculation()
    End Sub
    Private Sub FPostInLedger(SearchCode As String, Conn As Object, Cmd As Object)
        Dim mSr As Integer = 0
        mQry = " Delete From Ledger Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        If Val(Dgl2.Item(Col1Value, rowDiscount).Value) > 0 Then
            mSr = mSr + 1
            mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                SELECT '" & SearchCode & "' As DocId, " & mSr & " As V_SNo, " & Val(DglMain.Item(Col1Value, rowV_No).Value) & " As V_No, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As V_Date, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & " As SubCode, 
                " & AgL.Chk_Text(ClsSchool.Account_FeeDiscount) & " As ContraSub, 
                0 As AmtDr, 
                " & Val(Dgl2.Item(Col1Value, rowDiscount).Value) & " As AmtCr, 
                Null As Chq_No, Null As Chq_Date, 
                " & AgL.Chk_Text("Being Fee Due For " + AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Value)) & " As Narration, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt, 
                '" & TxtDivision.Tag & "' As DivCode, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As RecId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mSr = mSr + 1
            mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                    AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                    SELECT '" & SearchCode & "' As DocId, " & mSr & " As V_SNo, " & Val(DglMain.Item(Col1Value, rowV_No).Value) & " As V_No, 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                    " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                    " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As V_Date, 
                    " & AgL.Chk_Text(ClsSchool.Account_FeeDiscount) & " As SubCode, 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & " As ContraSub, 
                    " & Val(Dgl2.Item(Col1Value, rowDiscount).Value) & " As AmtDr, 
                    0 As AmtCr, 
                    Null As Chq_No, Null As Chq_Date, 
                    " & AgL.Chk_Text("Being Fee Due For " + AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Value)) & " As Narration, 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                    " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                    " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt, 
                    '" & TxtDivision.Tag & "' As DivCode, 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As RecId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        If Val(Dgl2.Item(Col1Value, rowLateFee).Value) > 0 Then
            mSr = mSr + 1
            mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                SELECT '" & SearchCode & "' As DocId, " & mSr & " As V_SNo, " & Val(DglMain.Item(Col1Value, rowV_No).Value) & " As V_No, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As V_Date, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & " As SubCode, 
                " & AgL.Chk_Text(ClsSchool.Fee_LateFee) & " As ContraSub, 
                " & Val(Dgl2.Item(Col1Value, rowLateFee).Value) & " As AmtDr, 
                0 As AmtCr, Null As Chq_No, Null As Chq_Date, 
                " & AgL.Chk_Text("Being Fee Due For " + AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Value)) & " As Narration, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt, 
                '" & TxtDivision.Tag & "' As DivCode, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As RecId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mSr = mSr + 1
            mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                        AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                        SELECT '" & SearchCode & "' As DocId, " & mSr & " As V_SNo, " & Val(DglMain.Item(Col1Value, rowV_No).Value) & " As V_No, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                        " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As V_Date, 
                        " & AgL.Chk_Text(ClsSchool.Fee_LateFee) & " As SubCode, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & " As ContraSub, 
                        0 As AmtDr, 
                        " & Val(Dgl2.Item(Col1Value, rowLateFee).Value) & " As AmtCr, 
                        Null As Chq_No, Null As Chq_Date, 
                        " & AgL.Chk_Text("Being Fee Due For " + AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Value)) & " As Narration, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                        " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                        " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt, 
                        '" & TxtDivision.Tag & "' As DivCode, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As RecId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1IsFeeDueExplicitly, I).Value) = 0 Then
                mSr = mSr + 1
                mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                        AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                        SELECT '" & SearchCode & "' As DocId, " & mSr & " As V_SNo, " & Val(DglMain.Item(Col1Value, rowV_No).Value) & " As V_No, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                        " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As V_Date, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & " As SubCode, 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Fee, I).Tag) & " As ContraSub, 
                        " & Val(Dgl1.Item(Col1AdjustedAmount, I).Value) & " As AmtDr, 
                        0 As AmtCr, Null As Chq_No, Null As Chq_Date, 
                        " & AgL.Chk_Text("Being Fee Due For " + AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Value)) & " As Narration, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                        " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                        " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt, 
                        '" & TxtDivision.Tag & "' As DivCode, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As RecId "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mSr = mSr + 1
                mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                        AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                        SELECT '" & SearchCode & "' As DocId, " & mSr & " As V_SNo, " & Val(DglMain.Item(Col1Value, rowV_No).Value) & " As V_No, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                        " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As V_Date, 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Fee, I).Tag) & " As SubCode, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & " As ContraSub, 
                        0 As AmtDr, 
                        " & Val(Dgl1.Item(Col1AdjustedAmount, I).Value) & " As AmtCr, 
                        Null As Chq_No, Null As Chq_Date, 
                        " & AgL.Chk_Text("Being Fee Due For " + AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Value)) & " As Narration, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                        " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                        " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt, 
                        '" & TxtDivision.Tag & "' As DivCode, 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As RecId "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next


        mSr = mSr + 1
        mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                SELECT '" & SearchCode & "' As DocId, " & mSr & " As V_SNo, " & Val(DglMain.Item(Col1Value, rowV_No).Value) & " As V_No, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As V_Date, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowPaymentAc).Tag) & " As SubCode, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & " As ContraSub, 
                " & Val(Dgl2.Item(Col1Value, rowPaidAmount).Value) & " As AmtDr, 
                0 As AmtCr, Null As Chq_No, Null As Chq_Date, 
                " & AgL.Chk_Text("Being Fee Received From " + AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Value)) & " As Narration, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt, 
                '" & TxtDivision.Tag & "' As DivCode, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As RecId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = mSr + 1
        mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                SELECT '" & SearchCode & "' As DocId, " & mSr & " As V_SNo, " & Val(DglMain.Item(Col1Value, rowV_No).Value) & " As V_No, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & " As V_Type, 
                " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & " As V_Date, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Tag) & " As SubCode, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowPaymentAc).Tag) & " As ContraSub, 
                0 As AmtDr, 
                " & Val(Dgl2.Item(Col1Value, rowPaidAmount).Value) & " As AmtCr, 
                Null As Chq_No, Null As Chq_Date, 
                " & AgL.Chk_Text("Being Fee Received From " + AgL.Chk_Text(DglMain.Item(Col1Value, rowStudent).Value)) & " As Narration, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & " As Site_Code, 
                " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                " & AgL.Chk_Date(AgL.PubLoginDate) & " As U_EntDt, 
                '" & TxtDivision.Tag & "' As DivCode, 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & " As RecId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub

        LblTotalAdjustedAmount.Text = "0"
        Dgl2.Item(Col1Value, rowTotalDueAmount).Value = 0
        For I = 0 To Dgl1.RowCount - 1
            Dgl2.Item(Col1Value, rowTotalDueAmount).Value = Val(Dgl2.Item(Col1Value, rowTotalDueAmount).Value) + Val(Dgl1.Item(Col1Amount, I).Value)
            LblTotalAdjustedAmount.Text = Val(LblTotalAdjustedAmount.Text) + Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
        Next

        If ChkAdjustAmountManually.Checked = True Then
            Dim bTotalAdjustedAmount As Double = 0
            For I = 0 To Dgl1.Rows.Count - 1
                If Val(Dgl1.Item(Col1AdjustedAmount, I).Value) > 0 Then
                    bTotalAdjustedAmount += Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
                End If
            Next
            bTotalAdjustedAmount = bTotalAdjustedAmount + Val(Dgl2.Item(Col1Value, rowLateFee).Value)
            Dgl2.Item(Col1Value, rowPaidAmount).Value = bTotalAdjustedAmount - Val(Dgl2.Item(Col1Value, rowDiscount).Value)
        End If

        Dgl2.Item(Col1Value, rowPayableAmount).Value = Val(Dgl2.Item(Col1Value, rowTotalDueAmount).Value) + Val(Dgl2.Item(Col1Value, rowLateFee).Value) - Val(Dgl2.Item(Col1Value, rowDiscount).Value)
    End Sub

    Private Sub FrmFeeReceiptEntry_BaseEvent_DglMainCellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainCellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then DglMain.CurrentCell.ReadOnly = True

            Select Case DglMain.CurrentCell.RowIndex
                Case rowV_Date, rowClass, rowStudent
                    If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
                        DglMain.Item(Col1Value, DglMain.CurrentCell.RowIndex).ReadOnly = True
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FCalculateLateFee()
        Dim Setting_LateFeeAfterDays As Integer = FGetSettings(ClsSchool.SettingFields_LateFeeAfterDays, SettingType.General)
        Dim Setting_LateFeeAmount As Integer = FGetSettings(ClsSchool.SettingFields_LateFeeAmount, SettingType.General)
        Dim Setting_LateFeeRecurrence As String = FGetSettings(ClsSchool.SettingFields_LateFeeRecurrence, SettingType.General)

        Dim mLateFeeApplicable As Double = 0
        Dim mDateDiff As Integer = 0
        Dim mLateFeeAmount As Double = 0


        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.StrCmp(AgL.XNull(Dgl1.Item(Col1Fee, I).Tag), ClsSchool.Fee_TuitionFee) Then
                mDateDiff = DateDiff(DateInterval.Day, CDate(Dgl1.Item(Col1DueDate, I).Value), CDate(AgL.PubLoginDate))
                If mDateDiff > Setting_LateFeeAfterDays Then
                    If Setting_LateFeeRecurrence = ClsSchool.Recurrence_Monthly Then
                        mLateFeeAmount = mLateFeeAmount + ((Convert.ToInt32((mDateDiff - Setting_LateFeeAfterDays) / 30)) * Setting_LateFeeAmount)
                    Else
                        mLateFeeAmount = Setting_LateFeeAmount
                    End If
                End If
            End If
        Next

        Dgl2.Item(Col1Value, rowLateFeeCalculated).Value = mLateFeeAmount
        Dgl2.Item(Col1Value, rowLateFee).Value = Dgl2.Item(Col1Value, rowLateFeeCalculated).Value
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Debug.Print("Before FGetSettings " & AgL.PubStopWatch.ElapsedMilliseconds.ToString)
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag, VoucherCategory.Sales, LblV_Type.Tag, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag)
        FGetSettings = mValue
        Debug.Print("After FGetSettings " & AgL.PubStopWatch.ElapsedMilliseconds.ToString)
    End Function
    Private Sub Dgl2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case Col1Value
                    Select Case Dgl2.CurrentCell.RowIndex
                        Case rowTotalDueAmount, rowPayableAmount
                            Dgl2.CurrentCell.ReadOnly = True
                        Case rowPaidAmount
                            If ChkAdjustAmountManually.Checked = True Then
                                Dgl2.CurrentCell.ReadOnly = True
                            Else
                                Dgl2.CurrentCell.ReadOnly = False
                            End If
                    End Select
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

        If Dgl2.Columns(mColumn).Name = Col1Value Then
            Select Case mRow
                Case rowPaidAmount
                    FAdjustFee()
                Case rowDiscount
                    If ChkAdjustAmountManually.Checked = False Then
                        FAdjustFee()
                    End If
            End Select
        End If
        Calculation()
    End Sub
    Private Sub ChkAdjustAmountManually_Click(sender As Object, e As EventArgs) Handles ChkAdjustAmountManually.Click
        'If ChkAdjustAmountManually.Checked = True Then
        '    Dgl2.Item(Col1Value, rowPaidAmount).ReadOnly = True
        '    Dgl1.Columns(Col1AdjustedAmount).ReadOnly = False
        'Else
        '    Dgl2.Item(Col1Value, rowPaidAmount).ReadOnly = False
        '    Dgl1.Columns(Col1AdjustedAmount).ReadOnly = True
        'End If
        Calculation()
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Calculation()
    End Sub
End Class
