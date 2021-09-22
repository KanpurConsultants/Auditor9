Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Imports System.Xml
Imports System.IO
Imports Customised.ClsMain
Imports System.Linq
Imports System.ComponentModel
Public Class FrmOrderSettlement_Kirana
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As Object, ByVal Cmd As Object)

    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay

    Dim mV_Type As String = ""

    Public Const ColSNo As String = "S.No."

    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public rowSettlementType As Integer = 6
    Public rowSubCode As Integer = 7

    Public rowProduct As Integer = 0
    Public rowOrderNo As Integer = 1
    Public rowOrderBalance As Integer = 2
    Public rowOrderRate As Integer = 3
    Public rowSettlementQty As Integer = 4
    Public rowSettlementRate As Integer = 5
    Public rowDifferenceRate As Integer = 6
    Public rowDifferenceAmount As Integer = 7
    Public rowLessDiscountPer As Integer = 8
    Public rowLessDiscountAmount As Integer = 9
    Public rowLessBrokeragePer As Integer = 10
    Public rowLessBrokerageAmount As Integer = 11
    Public rowNetDifferenceAmount As Integer = 12
    Public rowRemarks As Integer = 13

    Dim mIsEntryLocked As Boolean = False

    Public Const hcSettlementType As String = "Sett.Type"
    Public Const hcSubCode As String = "Party"

    Public Const hcProduct As String = "Product"
    Public Const hcOrderNo As String = "Order No"
    Public Const hcOrderBalance As String = "Order Balance"
    Public Const hcOrderRate As String = "Order Rate"
    Public Const hcSettlementQty As String = "Settlement Qty"
    Public Const hcSettlementRate As String = "Settlement Rate"
    Public Const hcDifferenceRate As String = "Difference Rate"
    Public Const hcDifferenceAmount As String = "Difference Amt"
    Public Const hcLessDiscountPer As String = "Less Discount %"
    Public Const hcLessDiscountAmount As String = "Less Disc.Amt"
    Public Const hcLessBrokeragePer As String = "Less Brokerage %"
    Public Const hcLessBrokerageAmount As String = "Less Brok.Amt"
    Public Const hcNetDifferenceAmount As String = "Net Diff.Amt"
    Public Const hcRemarks As String = "Remarks"

    Private Const SettlementType_Qty As String = "Qty"
    Private Const SettlementType_Value As String = "Value"

    Dim mHeaderTable As String = ""
    Dim mLineTable As String = ""
    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String, Optional ByVal strCustomUI As String = "")
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat
        mCustomUI = strCustomUI

        If strNCat = Ncat.PurchaseOrderCancel Then
            mHeaderTable = "PurchInvoice"
            mLineTable = "PurchInvoiceDetail"
        Else
            mHeaderTable = "SaleInvoice"
            mLineTable = "SaleInvoiceDetail"
        End If
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TxtStructure = New AgControls.AgTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.TxtNature = New AgControls.AgTextBox()
        Me.TP2 = New System.Windows.Forms.TabPage()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportOpeningFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEditSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuGenerateEWayBill = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrintBarcode = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRequestForPermission = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReferenceEntries = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuWizard = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuShowLedgerPosting = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(625, 411)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(143, 412)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 411)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(168, 471)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 412)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 405)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(299, 411)
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
        Me.TxtDocId.Location = New System.Drawing.Point(855, 366)
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblDocId
        '
        Me.LblDocId.Location = New System.Drawing.Point(808, 368)
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(336, 509)
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
        Me.TabControl1.Size = New System.Drawing.Size(1002, 386)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.Controls.SetChildIndex(Me.TP2, 0)
        Me.TabControl1.Controls.SetChildIndex(Me.TP1, 0)
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Pnl2)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(994, 360)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblNCatNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.PnlMain, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
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
        Me.PnlMain.Location = New System.Drawing.Point(1, 1)
        Me.PnlMain.Size = New System.Drawing.Size(490, 353)
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(463, 363)
        Me.LblV_Type.Size = New System.Drawing.Size(92, 14)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Invoice Type"
        '
        'LblNCatNature
        '
        Me.LblNCatNature.Location = New System.Drawing.Point(777, 371)
        '
        'ChkTemporarilySaved
        '
        Me.ChkTemporarilySaved.Location = New System.Drawing.Point(776, 576)
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
        Me.TxtStructure.Location = New System.Drawing.Point(641, 369)
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
        Me.Label25.Location = New System.Drawing.Point(569, 370)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 715
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlCustomGrid.Location = New System.Drawing.Point(357, 425)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(44, 27)
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
        Me.TxtNature.Location = New System.Drawing.Point(896, 172)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(81, 15)
        Me.TxtNature.TabIndex = 1208
        Me.TxtNature.Text = "TxtNature"
        Me.TxtNature.Visible = False
        '
        'TP2
        '
        Me.TP2.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Padding = New System.Windows.Forms.Padding(3)
        Me.TP2.Size = New System.Drawing.Size(994, 297)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "TabPage1"
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
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuImportOpeningFromExcel, Me.MnuEditSave, Me.MnuGenerateEWayBill, Me.MnuPrintBarcode, Me.MnuRequestForPermission, Me.MnuReferenceEntries, Me.MnuWizard, Me.MnuShowLedgerPosting, Me.MnuHistory})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(220, 268)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(219, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(219, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(219, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
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
        'MnuGenerateEWayBill
        '
        Me.MnuGenerateEWayBill.Name = "MnuGenerateEWayBill"
        Me.MnuGenerateEWayBill.Size = New System.Drawing.Size(219, 22)
        Me.MnuGenerateEWayBill.Text = "Generate EWay Bill"
        '
        'MnuPrintBarcode
        '
        Me.MnuPrintBarcode.Name = "MnuPrintBarcode"
        Me.MnuPrintBarcode.Size = New System.Drawing.Size(219, 22)
        Me.MnuPrintBarcode.Text = "Print Barcode"
        '
        'MnuRequestForPermission
        '
        Me.MnuRequestForPermission.Name = "MnuRequestForPermission"
        Me.MnuRequestForPermission.Size = New System.Drawing.Size(219, 22)
        Me.MnuRequestForPermission.Text = "Request For Permission"
        '
        'MnuReferenceEntries
        '
        Me.MnuReferenceEntries.Name = "MnuReferenceEntries"
        Me.MnuReferenceEntries.Size = New System.Drawing.Size(219, 22)
        Me.MnuReferenceEntries.Text = "Reference Entries"
        '
        'MnuWizard
        '
        Me.MnuWizard.Name = "MnuWizard"
        Me.MnuWizard.Size = New System.Drawing.Size(219, 22)
        Me.MnuWizard.Text = "Wizard"
        '
        'MnuShowLedgerPosting
        '
        Me.MnuShowLedgerPosting.Name = "MnuShowLedgerPosting"
        Me.MnuShowLedgerPosting.Size = New System.Drawing.Size(219, 22)
        Me.MnuShowLedgerPosting.Text = "Show Ledger Posting"
        '
        'MnuHistory
        '
        Me.MnuHistory.Name = "MnuHistory"
        Me.MnuHistory.Size = New System.Drawing.Size(219, 22)
        Me.MnuHistory.Text = "History"
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(493, 1)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(490, 353)
        Me.Pnl2.TabIndex = 743
        '
        'FrmOrderSettlement_Kirana
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 458)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.TxtNature)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.MaximizeBox = True
        Me.Name = "FrmOrderSettlement_Kirana"
        Me.Text = "Payment Settlement"
        Me.Controls.SetChildIndex(Me.ChkTemporarilySaved, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.TxtNature, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
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
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents TxtStructure As AgControls.AgTextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents TxtNature As AgControls.AgTextBox
    Friend WithEvents TP2 As TabPage
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
    Friend WithEvents MnuRequestForPermission As ToolStripMenuItem
    Friend WithEvents MnuReferenceEntries As ToolStripMenuItem
    Friend WithEvents MnuHistory As ToolStripMenuItem
    Public WithEvents Pnl2 As Panel
    Friend WithEvents MnuWizard As ToolStripMenuItem
    Friend WithEvents MnuPrintBarcode As ToolStripMenuItem
    Friend WithEvents MnuImportOpeningFromExcel As ToolStripMenuItem
    Friend WithEvents MnuShowLedgerPosting As ToolStripMenuItem
#End Region

    Private Sub FrmOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From " & mLineTable & " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From " & mHeaderTable & " Where DocId ='" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub ApplyUISetting()
        Dim bNCat As String = ""
        If LblV_Type.Tag <> "" Then bNCat = LblV_Type.Tag Else bNCat = EntryNCat
        GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(Dgl2, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        'GetUISetting_WithDataTables(DglCalc, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
    End Sub
    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = mHeaderTable
        MainLineTableCsv = mLineTable
        LogTableName = mHeaderTable & "_Log"
        LogLineTableCsv = mLineTable & "_Log"

        If OpenDocId = "" Then
            If CType(AgL.VNull(ClsMain.FGetSettings(SettingFields.AskVoucherTypeBeforeOpeningEntry, SettingType.General, TxtDivision.Tag, AgL.PubSiteCode, "PURCH", EntryNCat, "", "", "")), Boolean) = True Then
                FShowVoucherTypeHelp()
            End If
        End If
    End Sub
    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        If mFlag_Import = True And DTMaster.Rows.Count > 0 Then Exit Sub

        mCondStr = " And ( Date(H.V_Date) >= " & AgL.Chk_Date(AgL.PubStartDate) & " And  Date(H.V_Date) <= " & AgL.Chk_Date(AgL.PubEndDate) & " Or Vt.NCat='" & Ncat.OpeningStock & "') And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And IfNull(Vt.CustomUI,'') = '" & mCustomUI & "'"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP  With (NoLock) Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        If mV_Type <> "" Then
            mCondStr += " And H.V_Type = '" & mV_Type & "' "
        End If


        mQry = "Select DocID As SearchCode " &
                " From " & mHeaderTable & " H  With (NoLock) " &
                " Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  " &
                " Where 1=1  " & mCondStr & "  Order By V_Date , V_No "

        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And IfNull(Vt.CustomUI,'') = '" & mCustomUI & "'"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP  With (NoLock) Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        If mV_Type <> "" Then
            mCondStr += " And H.V_Type = '" & mV_Type & "' "
        End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Invoice_Type], H.V_Date AS Date, 
                             H.ManualRefNo As [Manual_No], SGV.DispName As Vendor, H.SalesTaxGroupParty As [Sales_Tax_Group_Party], H.VendorDocNo As [Vendor_Doc_No],  
                             H.VendorDocDate As [Vendor_Doc_Date], H.Remarks,
                             H.EntryBy As [Entry_By], H.EntryDate As [Entry_Date] 
                             From " & mHeaderTable & " H   With (NoLock)
                             LEFT Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type 
                             Left Join SubGroup SGV  With (NoLock) On SGV.SubCode  = H.Vendor  
                             Where 1 = 1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub
    Private Sub Frm_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim errRow As Integer = 0
        Dim I As Integer = 0
        Try
            DglMain.Columns(Col1BtnDetail).ReadOnly = True
            DglMain.Columns(Col1BtnDetail).Visible = False
            DglMain.Columns(Col1Head).Width = 105
            DglMain.Rows.Add(8)
            For I = 0 To DglMain.Rows.Count - 1
                DglMain.Rows(I).Visible = False
                If I <> rowSubCode Then
                    DglMain.Item(Col1BtnDetail, I) = New DataGridViewTextBoxCell
                End If
            Next
            DglMain.Item(Col1Head, rowSettlementType).Value = hcSettlementType
            DglMain.Item(Col1Head, rowSubCode).Value = hcSubCode

            DglMain.AgSkipReadOnlyColumns = True
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
                .AddAgTextColumn(Dgl2, Col1Head, 140, 255, Col1Head, True, True)
                .AddAgTextColumn(Dgl2, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
                .AddAgTextColumn(Dgl2, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
                .AddAgTextColumn(Dgl2, Col1Value, 320, 255, Col1Value, True, False)
                .AddAgTextColumn(Dgl2, Col1LastValue, 170, 255, Col1LastValue, False, False)
            End With
            AgL.AddAgDataGrid(Dgl2, Pnl2)
            AgL.GridDesign(Dgl2)
            Dgl2.EnableHeadersVisualStyles = False
            Dgl2.ColumnHeadersHeight = 35
            Dgl2.AgSkipReadOnlyColumns = True
            Dgl2.AllowUserToAddRows = False
            Dgl2.RowHeadersVisible = False
            Dgl2.ColumnHeadersVisible = False
            Dgl2.AgSkipReadOnlyColumns = True
            Dgl2.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
            Dgl2.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
            Dgl2.BackgroundColor = Me.BackColor
            Dgl2.BorderStyle = BorderStyle.None

            Dgl2.Rows.Add(14)
            For I = 0 To Dgl2.Rows.Count - 1
                Dgl2.Rows(I).Visible = False
            Next

            Dgl2.Name = "Dgl2"
            Dgl2.Tag = "VerticalGrid"

            Dgl2.Item(Col1Head, rowProduct).Value = hcProduct
            Dgl2.Item(Col1Head, rowOrderNo).Value = hcOrderNo
            Dgl2.Item(Col1Head, rowOrderBalance).Value = hcOrderBalance
            Dgl2.Item(Col1Head, rowOrderRate).Value = hcOrderRate
            Dgl2.Item(Col1Head, rowSettlementQty).Value = hcSettlementQty
            Dgl2.Item(Col1Head, rowSettlementRate).Value = hcSettlementRate
            Dgl2.Item(Col1Head, rowDifferenceRate).Value = hcDifferenceRate
            Dgl2.Item(Col1Head, rowDifferenceAmount).Value = hcDifferenceAmount
            Dgl2.Item(Col1Head, rowLessDiscountPer).Value = hcLessDiscountPer
            Dgl2.Item(Col1Head, rowLessDiscountAmount).Value = hcLessDiscountAmount
            Dgl2.Item(Col1Head, rowLessBrokeragePer).Value = hcLessBrokeragePer
            Dgl2.Item(Col1Head, rowLessBrokerageAmount).Value = hcLessBrokerageAmount
            Dgl2.Item(Col1Head, rowNetDifferenceAmount).Value = hcNetDifferenceAmount
            Dgl2.Item(Col1Head, rowRemarks).Value = hcRemarks




            For I = 0 To Dgl2.Rows.Count - 1
                If AgL.XNull(Dgl2(Col1HeadOriginal, I).Value) = "" Then
                    Dgl2(Col1HeadOriginal, I).Value = Dgl2(Col1Head, I).Value
                End If
            Next



            ApplyUISetting()
        Catch ex As Exception
            MsgBox(ex.Message & "[ Frm_BaseFunction_IniGrid ] " + errRow.ToString)
        End Try
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer, J As Integer = 0
        Dim bSelectionQry$ = "", bSelectionSkuQry$ = ""
        Dim bSalesTaxGroupParty As String = ""
        Dim mMultiplyWithMinus As Boolean = False



        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            mMultiplyWithMinus = True
        End If

        If DglMain.Item(Col1BtnDetail, rowSubCode).Tag IsNot Nothing Then
            If DglMain.Item(Col1BtnDetail, rowSubCode).Tag.Dgl1.Rows.Count > 0 Then
                bSalesTaxGroupParty = DglMain.Item(Col1BtnDetail, rowSubCode).Tag.Dgl1.Item(DglMain.Item(Col1BtnDetail, rowSubCode).Tag.Col1Value, DglMain.Item(Col1BtnDetail, rowSubCode).Tag.rowSalesTaxGroup).Value
            End If
        End If


        If DglMain.Item(Col1BtnDetail, rowSubCode).Tag Is Nothing Then DglMain.Item(Col1BtnDetail, rowSubCode).Tag = New FrmPurchaseInvoiceParty

        mQry = " Update " & mHeaderTable & " " &
                " Set  " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " SettlementType = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSettlementType).Value) & ", "

        If mHeaderTable = "PurchInvoice" Then
            mQry += " Vendor = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ", " &
                    " VendorName = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Value) & ", "
        Else
            mQry += " SaleToParty = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ", " &
                    " SaleToPartyName = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Value) & ", "
        End If
        mQry += " Remarks = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowRemarks).Value) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        CType(DglMain.Item(Col1BtnDetail, rowSubCode).Tag, FrmPurchaseInvoiceParty).FSave(mSearchCode, Conn, Cmd)

        If AgL.StrCmp(Topctrl1.Mode, "Add") = True Then
            mQry = "Insert Into " & mLineTable & "(DocId, Sr, Item, "
            If mHeaderTable = "PurchInvoice" Then
                mQry += "PurchInvoice, PurchInvoiceSr,"
            Else
                mQry += "SaleInvoice, SaleInvoiceSr,"
            End If
            mQry += " OrderBalance, OrderRate, Qty, Rate, DifferenceRate, DifferenceAmount, DiscountPer, DiscountAmount, 
                AdditionalDiscountPer, AdditionalDiscountAmount, Amount) "
            mQry += " Select " & AgL.Chk_Text(mSearchCode) & ", 1 As Sr, 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowProduct).Tag) & ", 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowOrderNo).Tag) & ", 1 As OrderSr,
                " & Val(Dgl2.Item(Col1Value, rowOrderBalance).Value) & " As OrderBalance, 
                " & Val(Dgl2.Item(Col1Value, rowOrderRate).Value) & " As OrderRate, 
                " & Val(Dgl2.Item(Col1Value, rowSettlementQty).Value) & " As Qty, 
                " & Val(Dgl2.Item(Col1Value, rowSettlementRate).Value) & " As Rate, 
                " & Val(Dgl2.Item(Col1Value, rowDifferenceRate).Value) & " As DifferenceRate, 
                " & Val(Dgl2.Item(Col1Value, rowDifferenceAmount).Value) & " As DifferenceAmount, 
                " & Val(Dgl2.Item(Col1Value, rowLessDiscountPer).Value) & " As DiscountPer, 
                " & Val(Dgl2.Item(Col1Value, rowLessDiscountAmount).Value) & " As DiscountAmount, 
                " & Val(Dgl2.Item(Col1Value, rowLessBrokeragePer).Value) & " As AdditionalDiscountPer, 
                " & Val(Dgl2.Item(Col1Value, rowLessBrokerageAmount).Value) & " As AdditionalDiscountAmount, 
                " & Val(Dgl2.Item(Col1Value, rowNetDifferenceAmount).Value) & " As Amount "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Else
            mQry = "Update " & mLineTable & " 
                Set Item = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowProduct).Tag) & ", "
            If mHeaderTable = "PurchInvoice" Then
                mQry += "PurchInvoice = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowOrderNo).Tag) & ", 
                        PurchInvoiceSr = 1,"
            Else
                mQry += "SaleInvoice = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowOrderNo).Tag) & ", 
                        SaleInvoiceSr = 1,"
            End If
            mQry += " OrderBalance = " & Val(Dgl2.Item(Col1Value, rowOrderBalance).Value) & ", 
                OrderRate = " & Val(Dgl2.Item(Col1Value, rowOrderRate).Value) & ", 
                Qty = " & Val(Dgl2.Item(Col1Value, rowSettlementQty).Value) & ", 
                Rate = " & Val(Dgl2.Item(Col1Value, rowSettlementRate).Value) & ", 
                DifferenceRate = " & Val(Dgl2.Item(Col1Value, rowDifferenceRate).Value) & ", 
                DifferenceAmount = " & Val(Dgl2.Item(Col1Value, rowDifferenceAmount).Value) & ", 
                DiscountPer = " & Val(Dgl2.Item(Col1Value, rowLessDiscountPer).Value) & ", 
                DiscountAmount = " & Val(Dgl2.Item(Col1Value, rowLessDiscountAmount).Value) & ", 
                AdditionalDiscountPer = " & Val(Dgl2.Item(Col1Value, rowLessBrokeragePer).Value) & ", 
                AdditionalDiscountAmount = " & Val(Dgl2.Item(Col1Value, rowLessBrokerageAmount).Value) & ", 
                Amount  = " & Val(Dgl2.Item(Col1Value, rowNetDifferenceAmount).Value) & "
                Where DocId = " & AgL.Chk_Text(mSearchCode) & ""
        End If

        If DglMain.Item(Col1Value, rowSettlementType).Value = SettlementType_Value Then
            FPostInLedger(SearchCode, Conn, Cmd)
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet
        Dim mMultiplyWithMinus As Boolean = False

        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            mMultiplyWithMinus = True
        End If

        mIsEntryLocked = False


        mQry = " SELECT H.*, L.*, So.ManualRefNo As OrderNo, I.Description As ItemDesc
                 From (Select * From " & mHeaderTable & "  With (NoLock) Where DocID='" & SearchCode & "') H 
                 LEFT JOIN " & mLineTable & "   L ON H.DocID = L.DocID
                 LEFT JOIN Item I ON L.Item = I.Code "
        If mHeaderTable = "PurchInvoice" Then
            mQry += " Left JOIN PurchInvoice So On L.PurchInvoice = So.DocId "
        Else
            mQry += " Left JOIN SaleInvoice So On L.SaleInvoice = So.DocId "
        End If

        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))
                DglMain.Item(Col1Value, rowSettlementType).Value = AgL.XNull(.Rows(0)("SettlementType"))
                If mHeaderTable = "PurchInvoice" Then
                    DglMain.Item(Col1Value, rowSubCode).Tag = AgL.XNull(.Rows(0)("Vendor"))
                    DglMain.Item(Col1Value, rowSubCode).Value = AgL.XNull(.Rows(0)("VendorName"))
                Else
                    DglMain.Item(Col1Value, rowSubCode).Tag = AgL.XNull(.Rows(0)("SaleToParty"))
                    DglMain.Item(Col1Value, rowSubCode).Value = AgL.XNull(.Rows(0)("SaleToPartyName"))
                End If

                Dgl2.Item(Col1Value, rowProduct).Tag = AgL.XNull(.Rows(0)("Item"))
                Dgl2.Item(Col1Value, rowProduct).Value = AgL.XNull(.Rows(0)("ItemDesc"))
                If mHeaderTable = "PurchInvoice" Then
                    Dgl2.Item(Col1Value, rowOrderNo).Tag = AgL.XNull(.Rows(0)("PurchInvoice"))
                Else
                    Dgl2.Item(Col1Value, rowOrderNo).Tag = AgL.XNull(.Rows(0)("SaleInvoice"))
                End If
                Dgl2.Item(Col1Value, rowOrderNo).Value = AgL.XNull(.Rows(0)("OrderNo"))
                Dgl2.Item(Col1Value, rowOrderBalance).Value = AgL.VNull(.Rows(0)("OrderBalance"))
                Dgl2.Item(Col1Value, rowOrderRate).Value = AgL.VNull(.Rows(0)("OrderRate"))
                Dgl2.Item(Col1Value, rowSettlementQty).Value = AgL.VNull(.Rows(0)("Qty"))
                Dgl2.Item(Col1Value, rowSettlementRate).Value = AgL.VNull(.Rows(0)("Rate"))
                Dgl2.Item(Col1Value, rowDifferenceRate).Value = AgL.VNull(.Rows(0)("DifferenceRate"))
                Dgl2.Item(Col1Value, rowDifferenceAmount).Value = AgL.VNull(.Rows(0)("DifferenceAmount"))
                Dgl2.Item(Col1Value, rowLessDiscountPer).Value = AgL.VNull(.Rows(0)("DiscountPer"))
                Dgl2.Item(Col1Value, rowLessDiscountAmount).Value = AgL.VNull(.Rows(0)("DifferenceAmount"))
                Dgl2.Item(Col1Value, rowLessBrokeragePer).Value = AgL.VNull(.Rows(0)("AdditionalDiscountPer"))
                Dgl2.Item(Col1Value, rowLessBrokerageAmount).Value = AgL.VNull(.Rows(0)("AdditionalDiscountAmount"))
                Dgl2.Item(Col1Value, rowRemarks).Value = AgL.XNull(.Rows(0)("Remarks"))
            End If
        End With


        Calculation()
        SetAttachmentCaption()
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Private Sub FrmPurchPlanEntry_BaseEvent_DglMainEditingControlValidating(sender As Object, e As CancelEventArgs) Handles Me.BaseEvent_DglMainEditingControlValidating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = DglMain.CurrentCell.RowIndex
        mColumn = DglMain.CurrentCell.ColumnIndex

        Select Case mRow
            Case rowV_Type
                TxtStructure.AgSelectedValue = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "'", AgL.GcnRead).ExecuteScalar
                IniGrid()
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", mHeaderTable, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

            Case rowSettingGroup
                If AgL.StrCmp(Topctrl1.Mode, "Add") Then
                    IniGrid()
                    If DglMain(Col1Value, rowSubCode).Visible = True Then
                        DglMain.CurrentCell = DglMain(Col1Value, rowSubCode)
                        DglMain.Focus()
                    End If
                Else
                    e.Cancel = True
                End If

            Case rowReferenceNo
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", mHeaderTable,
                                DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)

            Case rowSubCode
                If DglMain.Item(Col1Value, rowSubCode).Value <> "" Then
                    TxtNature.Text = AgL.XNull(AgL.Dman_Execute(" Select Nature From SubGroup Where SubCode = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "'", AgL.GCn).ExecuteScalar())

                    If ClsMain.IsPartyBlocked(DglMain.Item(Col1Value, rowSubCode).Tag, LblV_Type.Tag) Then
                        MsgBox("Party is blocked for " & DglMain.Item(Col1Value, rowV_Type).Value & ". Record will not be saved")
                    End If

                    FValidateSalesTaxGroup()

                    DglMain.Item(Col1BtnDetail, rowSubCode).Tag = Nothing
                End If
        End Select
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "'", AgL.GcnRead).ExecuteScalar

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GCn)

        DglMain.Item(Col1Value, rowSettingGroup).Tag = AgL.XNull(FGetSettings(SettingFields.DefaultSettingGroup, SettingType.General))
        DglMain.Item(Col1Value, rowSettingGroup).Value = AgL.XNull(AgL.Dman_Execute(" Select Name 
                        From SettingGroup 
                        Where Code = '" & DglMain.Item(Col1Value, rowSettingGroup).Tag & "'", AgL.GCn).ExecuteScalar())

        IniGrid()

        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", mHeaderTable, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
        mDimensionSrl = 0

        If mV_Type = "" Then
            If DtVoucher_TypeHelpDataSet.Tables(0).Rows.Count > 1 Then
                mQry = "SELECT " & IIf(AgL.PubServerName <> "", "Top 1", "") & " H.V_Type, Vt.Description AS V_TypeDesc  
                FROM " & mHeaderTable & " H
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE H.EntryBy = '" & AgL.PubUserName & "' 
                And Vt.NCat In ('" & EntryNCat & "')
                ORDER BY H.EntryDate DESC " & IIf(AgL.PubServerName = "", "Limit 1", "") & " "
                Dim DtLastVoucher_Type As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtLastVoucher_Type.Rows.Count > 0 Then
                    DglMain.Item(Col1Value, rowV_Type).Tag = AgL.XNull(DtLastVoucher_Type.Rows(0)("V_Type"))
                    DglMain.Item(Col1Value, rowV_Type).Value = AgL.XNull(DtLastVoucher_Type.Rows(0)("V_TypeDesc"))
                End If
            End If
        End If



        If DglMain.Visible = True Then
            If DglMain.FirstDisplayedCell IsNot Nothing Then
                If DglMain(Col1Value, rowSettingGroup).Visible = True And DglMain(Col1Value, rowSettingGroup).Value = "" Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowSettingGroup)
                ElseIf DglMain(Col1Value, rowSettlementType).Visible = True Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowSettlementType)
                Else
                    DglMain.CurrentCell = DglMain(Col1Value, DglMain.FirstDisplayedCell.RowIndex)
                End If
                DglMain.Focus()
            End If
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        'If Topctrl1.Mode = "Browse" Then Exit Sub

        Dgl2.Item(Col1Value, rowDifferenceRate).Value = Val(Dgl2.Item(Col1Value, rowOrderRate).Value) - Val(Dgl2.Item(Col1Value, rowSettlementRate).Value)
        Dgl2.Item(Col1Value, rowDifferenceAmount).Value = Val(Dgl2.Item(Col1Value, rowSettlementQty).Value) * Val(Dgl2.Item(Col1Value, rowDifferenceRate).Value)

        Dgl2.Item(Col1Value, rowLessDiscountAmount).Value = Math.Round(Val(Dgl2.Item(Col1Value, rowDifferenceAmount).Value) * Val(Dgl2.Item(Col1Value, rowLessDiscountPer).Value) / 100, 0)
        Dgl2.Item(Col1Value, rowLessBrokerageAmount).Value = Math.Round(Val(Dgl2.Item(Col1Value, rowDifferenceAmount).Value) * Val(Dgl2.Item(Col1Value, rowLessBrokeragePer).Value) / 100, 0)

        Dgl2.Item(Col1Value, rowNetDifferenceAmount).Value = Val(Dgl2.Item(Col1Value, rowDifferenceAmount).Value) - Val(Dgl2.Item(Col1Value, rowLessDiscountAmount).Value) - Val(Dgl2.Item(Col1Value, rowLessBrokerageAmount).Value)
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If mFlag_Import = True Then Exit Sub
        Dim I As Integer = 0
        Dim CheckDuplicateRef As Boolean


        If ClsMain.IsPartyBlocked(DglMain.Item(Col1Value, rowSubCode).Tag, LblV_Type.Tag) Then
            MsgBox("Party is blocked for " & DglMain.Item(Col1Value, rowV_Type).Value & ". Can not continue.")
            passed = False : Exit Sub
        End If

        If FValidateSalesTaxGroup() = False Then
            passed = False : Exit Sub
        End If

        If Dgl2.Visible = True Then
            For I = 0 To Dgl2.Rows.Count - 1
                If Dgl2.Rows(I).Visible = True Then
                    If Dgl2.Item(Col1Mandatory, I).Value <> "" Then
                        If (AgL.XNull(Dgl2.Item(Col1Value, I).Value) = "" Or Dgl2.Item(Col1Value, I).Value Is Nothing) Then
                            MsgBox(Dgl2.Item(Col1Head, I).Value & " is blank...!", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col1Value, I) : Dgl2.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End If

        If DglMain.Visible = True Then
            For I = 0 To DglMain.Rows.Count - 1
                If DglMain.Rows(I).Visible = True Then
                    If DglMain.Item(Col1Mandatory, I).Value <> "" Then
                        If (AgL.XNull(DglMain.Item(Col1Value, I).Value) = "" Or DglMain.Item(Col1Value, I).Value Is Nothing) Then
                            MsgBox(DglMain.Item(Col1Head, I).Value & " is blank...!", MsgBoxStyle.Information)
                            DglMain.CurrentCell = DglMain.Item(Col1Value, I) : DglMain.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End If




        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", mHeaderTable,
                                    DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                    DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                    DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)
        If Not CheckDuplicateRef Then
            DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", mHeaderTable, DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
    End Sub
    Private Sub TempOrder_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        Dim I As Integer
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
    Private Function FGetRelationalData() As Boolean
        Dim DtRelationalData As DataTable
        Try


        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
    Private Sub FrmPaymentSettlement_Kirana_BaseEvent_Topctrl_tbPreEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbPreEdit

    End Sub
    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Dim DtTemp As DataTable


        'DglMain.ReadOnly = False
        'Dgl2.ReadOnly = False
    End Sub
    Private Sub FrmPaymentSettlement_Kirana_BaseEvent_Topctrl_tbPreDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbPreDel
        Dim mQry As String


        If ClsMain.IsEntryLockedWithLockText(mHeaderTable, "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        Passed = Not FGetRelationalData()


    End Sub
    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
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
    Private Sub FrmOrder_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        If mFlag_Import = True Then Exit Sub

        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next

        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2(Col1Head, I).Tag = Nothing
        Next
        ClsMain.FCreateItemDataTable()
    End Sub
    Public Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim bNCat As String = "", bCategory As String = ""
        If LblV_Type.Tag <> "" Then bNCat = LblV_Type.Tag Else bNCat = EntryNCat

        If bNCat = Ncat.StockIssue Or bNCat = Ncat.StockReceive Then
            bCategory = "Stock"
        Else
            bCategory = "Purch"
        End If

        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag, bCategory, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag)
        FGetSettings = mValue
    End Function




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
            FReplaceInvoiceVariables(DsRep.Tables(0), TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag)
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
            'ClsMain.Formula_Set(mCrd, CType(objFrm, AgTemplate.TempTransaction).TxtDivision.Tag, CType(objFrm, AgTemplate.TempTransaction1).DglMain.Item(Col1Value, rowSite_Code).Tag)
            ClsMain.Formula_Set(mCrd, CType(objFrm, AgTemplate.TempTransaction1).TxtDivision.Tag, CType(objFrm, AgTemplate.TempTransaction1).DglMain.Item(Col1Value, rowSite_Code).Tag, CType(objFrm, AgTemplate.TempTransaction1).DglMain.Item(Col1Value, rowV_Type).Tag, RepTitle)
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

            FReplaceInvoiceVariables(DsRep.Tables(0), TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag)
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
            'ClsMain.Formula_Set(mCrd, CType(objFrm, AgTemplate.TempTransaction).TxtDivision.Tag, CType(objFrm, AgTemplate.TempTransaction1).DglMain.Item(Col1Value, rowSite_Code).Tag)
            ClsMain.Formula_Set(mCrd, CType(objFrm, AgTemplate.TempTransaction1).TxtDivision.Tag, CType(objFrm, AgTemplate.TempTransaction1).DglMain.Item(Col1Value, rowSite_Code).Tag, CType(objFrm, AgTemplate.TempTransaction1).DglMain.Item(Col1Value, rowV_Type).Tag, RepTitle)
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
                    From " & mHeaderTable & " H  With (NoLock)
                    Where H.DocID = '" & mSearchCode & "'"
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            'mText = Replace(mText, "<CreditDays>", AgL.XNull(dtTemp.Rows(0)("CreditDays")))
            ClsMain.FReplacePubVariables(mText, TxtDivision.Tag, DglMain.Item(Col1Value, rowSite_Code).Tag)

        End If

        FReplaceInvoiceVariables = mText
    End Function

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub
    Private Sub FrmStockHeadEntry_BaseEvent_DglMainEditingControlKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainEditingControlKeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex
            Select Case mRow
                Case rowSettlementType
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT 'Value' As Code, 'Value' As Name
                                    UNION ALL 
                                    SELECT 'Qty' As Code, 'Qty' As Name "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
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
    Private Sub Dgl2_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.EditingControl_KeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer

            If Dgl2.CurrentCell Is Nothing Then Exit Sub

            mRow = Dgl2.CurrentCell.RowIndex
            mColumn = Dgl2.CurrentCell.ColumnIndex
            Select Case mRow
                Case rowProduct
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Description FROM ItemCategory "
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowOrderNo
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select VMain.DocID As Code, VMain.OrderNo As [Order No], 
                                    VMain.OrderDate As [Order Date],
                                    VMain.BalanceAmount As [Balance Amount],
                                    VMain.BalanceQty AS [Balance Qty],
                                    VMain.DeliveryDate As [Due Date] 
                                    From (" & FOrderBalance(Dgl2.Item(Col1Value, rowProduct).Tag) & ") As VMain "
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


    Private Function FCreateHelpSubgroup() As DataSet
        Dim strCond As String = ""


        Dim bFilterInclude_Process As String = FGetSettings(SettingFields.FilterInclude_Process, SettingType.General)

        If bFilterInclude_Process <> "" Then
            If bFilterInclude_Process.ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IfNull(Sp.Process,'" & Process.Purchase & "'),'" & bFilterInclude_Process & "') > 0
                                   Or CharIndex('+' || IfNull(P.Parent,'" & Process.Purchase & "'),'" & bFilterInclude_Process & "') > 0) "
            ElseIf bFilterInclude_Process.ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IfNull(Sp.Process,'" & Process.Purchase & "'),'" & bFilterInclude_Process & "') <= 0 
                                   CharIndex('-' || IfNull(P.Parent,'" & Process.Purchase & "'),'" & bFilterInclude_Process & "') <= 0)  "
            End If
        End If

        Dim bFilterInclude_SubGroupType As String = FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General)
        If bFilterInclude_SubGroupType <> "" Then
            If bFilterInclude_SubGroupType.ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || H.SubGroupType,'" & bFilterInclude_SubGroupType & "') > 0 "
            ElseIf bFilterInclude_SubGroupType.ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || H.SubGroupType,'" & bFilterInclude_SubGroupType & "') <= 0 "
            End If
        End If



        'strCond += " And H.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Cash & "')"

        mQry = " SELECT Distinct H.SubCode, H.Name || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS [Party], " &
                " H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  With (NoLock) " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " Left Join SubgroupProcess SP On H.Subcode = SP.Subcode " &
                " Left Join SubGroup P On Sp.Process = P.Subcode " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        mQry += " Union All SELECT Distinct H.SubCode, H.Name || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS [Party], " &
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
    Private Function FCreateHelpLinkedParty() As DataSet
        Dim strCond As String = ""

        strCond += " And Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Cash & "','" & ClsMain.SubGroupNature.Bank & "')"
        strCond += " And Sg.Parent = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "' "

        mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.Address, Ag.GroupName, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " &
                " FROM SubGroup Sg  With (NoLock) " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Where 1 = 1 " &
                " And IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'TxtBillToParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportOpeningFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuEditSave.Click, MnuGenerateEWayBill.Click, MnuRequestForPermission.Click, MnuReferenceEntries.Click, MnuHistory.Click, MnuWizard.Click, MnuPrintBarcode.Click, MnuShowLedgerPosting.Click
        Select Case sender.name
            Case MnuEditSave.Name
                FEditSaveAllEntries()

            Case MnuRequestForPermission.Name
                FRequestForPermission(EntryAction.Edit)

            Case MnuReferenceEntries.Name
                FShowRefrentialEntries(mSearchCode)

            Case MnuHistory.Name
                FShowHistory(mSearchCode)

            Case MnuPrintBarcode.Name
                Dim FrmObj As FrmPrintBarcode
                FrmObj = New FrmPrintBarcode()
                FrmObj.DocId = mSearchCode
                FrmObj.LblTitle.Text = DglMain.Item(Col1Value, rowV_Type).Value + " - " + DglMain.Item(Col1Value, rowReferenceNo).Value
                FrmObj.StartPosition = FormStartPosition.CenterParent

                FrmObj.ShowDialog()

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

            Case MnuWizard.Name
                FWizard()

            Case MnuShowLedgerPosting.Name
                FShowLedgerPosting()
        End Select
    End Sub
    Private Sub FWizard()
        Dim StrSenderText As String = Me.Text
        GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
        GridReportFrm.Filter_IniGrid()

        Dim CRep As ClsPurchaseInvoiceWizard = New ClsPurchaseInvoiceWizard(GridReportFrm)
        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
        CRep.V_Type = mV_Type
        CRep.ObjFrm = Me
        CRep.Ini_Grid()
        'GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 0).Value = AgL.PubStartDate
        'GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 1).Value = AgL.PubLoginDate
        ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
        GridReportFrm.MdiParent = Me.MdiParent
        GridReportFrm.Show()
        'CRep.ProcPurchaseInvoiceWizard()
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
    Private Sub FrmOrderDirect_BaseEvent_Topctrl_tbMore() Handles Me.BaseEvent_Topctrl_tbMore
        MnuOptions.Show(Topctrl1, Topctrl1.btbSite.Rectangle.X, Topctrl1.btbSite.Rectangle.Y + Topctrl1.btbSite.Rectangle.Size.Height)
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
        'If Directory.Exists(AttachmentPath) Then
        '    Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
        '    If FileCount > 0 Then Dgl2.Item(Col1Value, rowBtnAttachments).Value = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else Dgl2.Item(Col1Value, rowBtnAttachments).Value = ""
        'Else
        '    Dgl2.Item(Col1Value, rowBtnAttachments).Value = ""
        'End If
    End Sub
    Private Sub FShowRefrentialEntries(bDocId As String)
        Dim FrmObj As New FrmReferenceEntries()
        FrmObj.SearchCode = bDocId
        FrmObj.LblDocNo.Text = "Entry No : " + DglMain.Item(Col1Value, rowReferenceNo).Value
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
        ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
        GridReportFrm.MdiParent = Me.MdiParent
        GridReportFrm.Show()
        CRep.ProcLogReport(,, SearchCode)
    End Sub
    Private Sub Dgl2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = Dgl2.CurrentCell.RowIndex
            mColumn = Dgl2.CurrentCell.ColumnIndex

            Dgl2.AgHelpDataSet(Dgl2.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case mRow
                Case rowProduct
                    If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
                        Dgl2.Item(Col1Value, Dgl2.CurrentCell.RowIndex).ReadOnly = True
                    End If

                Case rowOrderBalance, rowOrderRate
                    Dgl2.Item(Col1Value, Dgl2.CurrentCell.RowIndex).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmOrderDirect_BaseEvent_DglMainContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainContentClick
        Try
            Select Case DglMain.Columns(e.ColumnIndex).Name
                Case Col1BtnDetail

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub




    Private Sub FrmOrderDirect_BaseEvent_DglMainKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainKeyDown
        Try
            If DglMain.CurrentCell IsNot Nothing Then
                If e.KeyCode = Keys.Enter Then
                    Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                    If DglMain.CurrentCell.RowIndex = LastCell.RowIndex Then
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
    Private Sub FrmOrderDirect_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
        End If
    End Sub
    Private Function FValidateSalesTaxGroup() As Boolean
        Dim bAllowedSalesTaxGroupParty As String = FGetSettings(SettingFields.AllowedSalesTaxGroupParty, SettingType.General)
        If bAllowedSalesTaxGroupParty <> "" Then
            Dim bSalesTaxPostingGroup As String = AgL.XNull(AgL.Dman_Execute("Select SalesTaxPostingGroup 
                                        From SubGroup 
                                        Where SubCode = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "'", AgL.GCn).ExecuteScalar())
            If bAllowedSalesTaxGroupParty.ToUpper.Contains("+" + bSalesTaxPostingGroup.ToUpper) = False Then
                MsgBox(bSalesTaxPostingGroup + " Parties are not allowed for " & DglMain.Item(Col1Value, rowV_Type).Value & "...!", MsgBoxStyle.Information)
                FValidateSalesTaxGroup = False : Exit Function
            End If
        End If
        FValidateSalesTaxGroup = True
    End Function
    Private Sub Dgl2_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl2.CellBeginEdit
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl2.CurrentCell.RowIndex
            End Select
            Dgl2.Item(Col1LastValue, Dgl2.CurrentCell.RowIndex).Tag = Dgl2.Item(Col1Value, Dgl2.CurrentCell.RowIndex).Tag
            Dgl2.Item(Col1LastValue, Dgl2.CurrentCell.RowIndex).Value = Dgl2.Item(Col1Value, Dgl2.CurrentCell.RowIndex).Value
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        mQry = " Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub CheckBoxFullCalculate_CheckedChanged(sender As Object, e As EventArgs)
        Calculation()
    End Sub

    Private Sub FShowVoucherTypeHelp()
        mQry = " SELECT V_Type As Code, Description FROM Voucher_Type WHERE NCat In ('" & EntryNCat & "') And IfNull(Status,'Active') = 'Active'"
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 350, 400, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Type", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            mV_Type = FRH_Single.DRReturn("Code")
        Else
            Me.BeginInvoke(New MethodInvoker(AddressOf Close))
        End If
    End Sub
    Private Sub FrmOrderDirect_WithDimension_BaseEvent_DglMainCellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainCellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then DglMain.CurrentCell.ReadOnly = True

            Select Case DglMain.CurrentCell.RowIndex
                Case rowSubCode
                    If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
                        DglMain.Item(Col1Value, rowSubCode).ReadOnly = True
                    End If
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FPostInLedger(SearchCode As String, Conn As Object, Cmd As Object)
        Dim bSaleAc As String = "SALE"
        Dim bPurchaseAc As String = "PURCH"

        mQry = " Delete From Ledger Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Dim bAmountField As String = ""
        Dim bNarrationField As String = ""
        If LblV_Type.Tag = Ncat.Payment Then
            bAmountField = "0 AS AmtCr, Sum(L.Amount) AS AmtDr, "
        End If

        mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                    AmtDr, AmtCr, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                    SELECT H.DocId, 1 AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                    Max(H.V_Date) AS V_Date, "

        If LblV_Type.Tag = Ncat.PurchaseOrderCancel Then
            mQry += " Max(H.Vendor) AS SubCode, '" & bPurchaseAc & "' AS ContraSub, 
                    Case When Sum(L.Amount) < 0 Then Abs(Sum(L.Amount)) End AS AmtDr, 
                    Case When Sum(L.Amount) >= 0 Then Abs(Sum(L.Amount)) End AS AmtCr, 
                    'Being Amount Received From ' || Max(H.VendorName) AS Narration, "
        Else
            mQry += " Max(H.SaleToParty) AS SubCode, '" & bSaleAc & "' AS ContraSub, 
                      Case When Sum(L.Amount) >= 0 Then Abs(Sum(L.Amount)) End AS AmtDr, 
                      Case When Sum(L.Amount) < 0 Then Abs(Sum(L.Amount)) End AS AmtCr, 
                    'Being Amount Paid To ' || Max(H.SaleToPartyName) AS Narration, "
        End If

        mQry += " Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                    Max(H.ManualRefNo) AS RecId
                    FROM " & mHeaderTable & " H With (NoLock)
                    LEFT JOIN " & mLineTable & " L With (NoLock) ON H.DocID = L.DocID
                    WHERE H.DocId = '" & SearchCode & "'
                    GROUP BY H.DocID	

                    UNION ALL

                    SELECT H.DocId, 2 AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                    Max(H.V_Date) AS V_Date, "

        If LblV_Type.Tag = Ncat.PurchaseOrderCancel Then
            mQry += " '" & bPurchaseAc & "' AS SubCode, Max(H.Vendor) AS ContraSub,  
                    Case When Sum(L.Amount) >= 0 Then Abs(Sum(L.Amount)) End AS AmtDr, 
                    Case When Sum(L.Amount) < 0 Then Abs(Sum(L.Amount)) End AS AmtCr, 
                    'Being Amount Received From ' || Max(H.VendorName) AS Narration, "
        Else
            mQry += " '" & bSaleAc & "' AS SubCode, Max(H.SaleToParty) AS ContraSub,
                    Case When Sum(L.Amount) < 0 Then Abs(Sum(L.Amount)) End AS AmtDr, 
                    Case When Sum(L.Amount) >= 0 Then Abs(Sum(L.Amount)) End As AmtCr, 
                    'Being Amount Paid To ' || Max(H.SaleToPartyName) AS Narration, "
        End If

        mQry += "Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                    Max(H.ManualRefNo) AS RecId
                    FROM " & mHeaderTable & " H With (NoLock)
                    LEFT JOIN " & mLineTable & " L With (NoLock) ON H.DocID = L.DocID
                    WHERE H.DocId = '" & SearchCode & "'
                    GROUP BY H.DocID"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub Dgl2_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = Dgl2.CurrentCell.RowIndex
        mColumn = Dgl2.CurrentCell.ColumnIndex

        Select Case mRow
            Case rowOrderNo
                mQry = "Select VMain.*
                        From (" & FOrderBalance(Dgl2.Item(Col1Value, rowProduct).Tag) & ") As VMain 
                        Where VMain.DocId = '" & Dgl2.Item(Col1Value, rowOrderNo).Tag & "'"
                Dim DtOrderDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtOrderDetail.Rows.Count > 0 Then
                    Dgl2.Item(Col1Value, rowOrderBalance).Value = DtOrderDetail.Rows(0)("BalanceQty")
                    Dgl2.Item(Col1Value, rowOrderRate).Value = DtOrderDetail.Rows(0)("OrderRate")
                End If
        End Select
        Calculation()
    End Sub
    Private Function FOrderBalance(ItemCode As String) As String
        If mHeaderTable = "PurchInvoice" Then
            mQry = "Select L.DocID, H.V_Type || '-' || H.ManualRefNo As OrderNo, 
                H.V_Date As OrderDate, L.Rate As OrderRate,
                L.Amount - IfNull(VOrderCancel.OrderCancelAmount,0) - IfNull(VInvoice.InvoiceAmount,0) - 
                    IfNull(VInvoiceReturn.ReturnAmount, 0) As BalanceAmount,
                L.Qty - IfNull(VOrderCancel.OrderCancelQty, 0) - IfNull(VInvoice.InvoiceQty, 0) -
                    IfNull(VInvoiceReturn.ReturnQty, 0) AS BalanceQty,
                H.DeliveryDate
                From PurchInvoice H 
	            Left Join PurchInvoiceDetail L ON H.DocID = L.DocID
                Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                Left Join(
		            SELECT L.PurchInvoice, L.PurchInvoiceSr, Sum(L.Qty) As OrderCancelQty,
                    Sum(L.Amount) As OrderCancelAmount
		            From PurchInvoice H 
		            Left Join PurchInvoiceDetail L ON H.DocID = L.DocID
                    Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE Vt.NCat = '" & Ncat.PurchaseOrderCancel & "'	
                    Group BY L.PurchInvoice, L.PurchInvoiceSr
	            ) AS VOrderCancel ON L.DocID = VOrderCancel.PurchInvoice And L.Sr = VOrderCancel.PurchInvoiceSr
	            Left Join(
		            SELECT L.PurchInvoice, L.PurchInvoiceSr, Sum(L.Qty) As InvoiceQty,
                    Sum(L.Amount) As InvoiceAmount
		            From PurchInvoice H 
		            Left Join PurchInvoiceDetail L ON H.DocID = L.DocID
                    Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE Vt.NCat = '" & Ncat.PurchaseInvoice & "'	
                    Group BY L.PurchInvoice, L.PurchInvoiceSr
	            ) AS VInvoice ON L.DocID = VInvoice.PurchInvoice And L.Sr = VInvoice.PurchInvoiceSr
	            Left Join(
		            SELECT L.PurchInvoice, L.PurchInvoiceSr, Sum(L.Qty) As ReturnQty,
                    Sum(Sid.Amount) As ReturnAmount
		            From PurchInvoice H 
		            Left Join PurchInvoiceDetail L ON H.DocID = L.DocID
                    Left Join PurchInvoiceDetail Sid ON L.PurchInvoice = Sid.DocId And L.PurchInvoiceSr = Sid.Sr
                    Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE Vt.NCat = '" & Ncat.PurchaseReturn & "'	
                    Group BY L.PurchInvoice, L.PurchInvoiceSr
	            ) AS VInvoiceReturn ON L.DocID = VInvoiceReturn.PurchInvoice And L.Sr = VInvoiceReturn.PurchInvoiceSr
	            WHERE 1=1 
                And H.Vendor = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "'
                And Vt.NCat = '" & Ncat.PurchaseOrder & "' 
                And L.Item = '" & ItemCode & "'"
            mQry += " And L.Qty - IfNull(VOrderCancel.OrderCancelQty,0) - IfNull(VInvoice.InvoiceQty,0) - 
			                            IfNull(VInvoiceReturn.ReturnQty,0) > 0 "
        Else
            mQry = "Select L.DocID, H.V_Type || '-' || H.ManualRefNo As OrderNo, 
                H.V_Date As OrderDate, L.Rate As OrderRate,
                L.Amount - IfNull(VOrderCancel.OrderCancelAmount,0) - IfNull(VInvoice.InvoiceAmount,0) - 
                    IfNull(VInvoiceReturn.ReturnAmount, 0) As BalanceAmount,
                L.Qty - IfNull(VOrderCancel.OrderCancelQty, 0) - IfNull(VInvoice.InvoiceQty, 0) -
                    IfNull(VInvoiceReturn.ReturnQty, 0) AS BalanceQty,
                H.DeliveryDate
                From SaleInvoice H 
	            Left Join SaleInvoiceDetail L ON H.DocID = L.DocID
                Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                Left Join(
		            SELECT L.SaleInvoice, L.SaleInvoiceSr, Sum(L.Qty) As OrderCancelQty,
                    Sum(L.Amount) As OrderCancelAmount
		            From SaleInvoice H 
		            Left Join SaleInvoiceDetail L ON H.DocID = L.DocID
                    Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE Vt.NCat = '" & Ncat.SaleOrderCancel & "'	
                    Group BY L.SaleInvoice, L.SaleInvoiceSr
	            ) AS VOrderCancel ON L.DocID = VOrderCancel.SaleInvoice And L.Sr = VOrderCancel.SaleInvoiceSr
	            Left Join(
		            SELECT L.SaleInvoice, L.SaleInvoiceSr, Sum(L.Qty) As InvoiceQty,
                    Sum(L.Amount) As InvoiceAmount
		            From SaleInvoice H 
		            Left Join SaleInvoiceDetail L ON H.DocID = L.DocID
                    Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE Vt.NCat = '" & Ncat.SaleInvoice & "'	
                    Group BY L.SaleInvoice, L.SaleInvoiceSr
	            ) AS VInvoice ON L.DocID = VInvoice.SaleInvoice And L.Sr = VInvoice.SaleInvoiceSr
	            Left Join(
		            SELECT L.SaleInvoice, L.SaleInvoiceSr, Sum(L.Qty) As ReturnQty,
                    Sum(Sid.Amount) As ReturnAmount
		            From SaleInvoice H 
		            Left Join SaleInvoiceDetail L ON H.DocID = L.DocID
                    Left Join SaleInvoiceDetail Sid ON L.SaleInvoice = Sid.DocId And L.SaleInvoiceSr = Sid.Sr
                    Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE Vt.NCat = '" & Ncat.SaleReturn & "'	
                    Group BY L.SaleInvoice, L.SaleInvoiceSr
	            ) AS VInvoiceReturn ON L.DocID = VInvoiceReturn.SaleInvoice And L.Sr = VInvoiceReturn.SaleInvoiceSr
	            WHERE 1=1 
                And H.SaleToParty = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "'
                And Vt.NCat = '" & Ncat.SaleOrder & "' 
                And L.Item = '" & ItemCode & "'"
            mQry += " And L.Qty - IfNull(VOrderCancel.OrderCancelQty,0) - IfNull(VInvoice.InvoiceQty,0) - 
			                            IfNull(VInvoiceReturn.ReturnQty,0) > 0 "
        End If
        FOrderBalance = mQry
    End Function
End Class
