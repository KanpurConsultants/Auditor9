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
Public Class FrmPaymentReceiptSettlement_Kirana
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As Object, ByVal Cmd As Object)

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Select As String = "Tick"
    Public Const ColSNo As String = "S.No."
    Public Const Col1VoucherType As String = "Voucher Type"
    Public Const Col1TransactionDocID As String = "Entry No"
    Public Const Col1LinkedSubCode As String = "LinkedSubCode"
    Public Const Col1AmountDr As String = "Amount Dr"
    Public Const Col1AmountCr As String = "Amount Cr"
    Public Const Col1InterestPer As String = "Interest Per"
    Public Const Col1InterestAmount As String = "Interest"
    Public Const Col1DiscountPer As String = "Discount Per"
    Public Const Col1DiscountAmount As String = "Discount"
    Public Const Col1SubTotal As String = "Sub Total"
    Public Const Col1BrokeragePer As String = "Brokerage Per"
    Public Const Col1BrokerageAmount As String = "Brokerage"
    Public Const Col1Remark As String = "Remark"

    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay

    Dim mV_Type As String = ""

    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public rowSubCode As Integer = 6
    Public rowLinkedSubCode As Integer = 7

    Public rowLineSubCode As Integer = 0
    Public rowIsFinalPayment As Integer = 1
    Public rowAmount As Integer = 2
    Public rowRemarks As Integer = 3
    Public rowBtnAttachments As Integer = 4

    Dim mIsEntryLocked As Boolean = False


    Public Const hcSubCode As String = "Party"
    Public Const hcLinkedSubCode As String = "Linked Party"

    Public Const hcLineSubCode As String = "LineSubCode"
    Public Const hcIsFinalPayment As String = "Is Final Payment"
    Public Const hcAmount As String = "Amount"
    Public Const hcRemarks As String = "Remarks"














    Public WithEvents DglCalc As New AgControls.AgDataGrid

    Public rowTotalBillAmt As Integer = 0
    Public rowLessPartyPayment As Integer = 1
    Public rowNetBillAmt As Integer = 2
    Public rowAddInterest As Integer = 3
    Public rowLessDicount As Integer = 4
    Public rowSubTotal As Integer = 5
    Public rowLessBrokerage As Integer = 6
    Public rowNetReceivable As Integer = 7


    Public hcTotalBillAmt As String = "Total Bill Amt"
    Public hcLessPartyPayment As String = "Less Party Payment"
    Public hcNetBillAmt As String = "Net Bill Amt"
    Public hcAddInterest As String = "Add Interest"
    Public hcLessDicount As String = "Less Dicount"
    Public hcSubTotal As String = "Sub Total"
    Public hcLessBrokerage As String = "Less Brokerage"
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblInstructions As Label
    Friend WithEvents MnuShowLedgerPosting As ToolStripMenuItem
    Public hcNetReceivable As String = "Net Receivable"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String, Optional ByVal strCustomUI As String = "")
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat
        mCustomUI = strCustomUI

        mQry = "Select H.* from LedgerHeadSetting H  With (NoLock) Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') Or H.V_Type Is Null  "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.TxtStructure = New AgControls.AgTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.PnlCalcGrid = New System.Windows.Forms.Panel()
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
        Me.MnuHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.CheckBoxFullCalculate = New System.Windows.Forms.CheckBox()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblInstructions = New System.Windows.Forms.Label()
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
        Me.MnuOptions.SuspendLayout()
        Me.PnlTotals.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(625, 575)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(143, 576)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 575)
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
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(299, 575)
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
        Me.TabControl1.Size = New System.Drawing.Size(1002, 187)
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
        Me.TP1.Size = New System.Drawing.Size(994, 161)
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
        Me.PnlMain.Size = New System.Drawing.Size(490, 157)
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(463, 168)
        Me.LblV_Type.Size = New System.Drawing.Size(92, 14)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Invoice Type"
        '
        'ChkTemporarilySaved
        '
        Me.ChkTemporarilySaved.Location = New System.Drawing.Point(776, 576)
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
        Me.Pnl1.Location = New System.Drawing.Point(3, 227)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(651, 315)
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
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCalcGrid.Location = New System.Drawing.Point(661, 227)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(320, 341)
        Me.PnlCalcGrid.TabIndex = 16
        Me.PnlCalcGrid.Visible = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlCustomGrid.Location = New System.Drawing.Point(357, 589)
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
        Me.TP2.Size = New System.Drawing.Size(994, 161)
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
        Me.MnuOptions.Size = New System.Drawing.Size(220, 290)
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
        Me.Pnl2.Size = New System.Drawing.Size(490, 157)
        Me.Pnl2.TabIndex = 743
        '
        'CheckBoxFullCalculate
        '
        Me.CheckBoxFullCalculate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFullCalculate.AutoSize = True
        Me.CheckBoxFullCalculate.Location = New System.Drawing.Point(811, 599)
        Me.CheckBoxFullCalculate.Name = "CheckBoxFullCalculate"
        Me.CheckBoxFullCalculate.Size = New System.Drawing.Size(89, 17)
        Me.CheckBoxFullCalculate.TabIndex = 0
        Me.CheckBoxFullCalculate.Text = "Full Calculate"
        Me.CheckBoxFullCalculate.UseVisualStyleBackColor = True
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblInstructions)
        Me.PnlTotals.Location = New System.Drawing.Point(2, 545)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(650, 23)
        Me.PnlTotals.TabIndex = 1209
        '
        'LblInstructions
        '
        Me.LblInstructions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblInstructions.AutoSize = True
        Me.LblInstructions.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInstructions.ForeColor = System.Drawing.Color.Maroon
        Me.LblInstructions.Location = New System.Drawing.Point(5, 3)
        Me.LblInstructions.Name = "LblInstructions"
        Me.LblInstructions.Size = New System.Drawing.Size(12, 16)
        Me.LblInstructions.TabIndex = 659
        Me.LblInstructions.Text = "."
        '
        'MnuShowLedgerPosting
        '
        Me.MnuShowLedgerPosting.Name = "MnuShowLedgerPosting"
        Me.MnuShowLedgerPosting.Size = New System.Drawing.Size(219, 22)
        Me.MnuShowLedgerPosting.Text = "Show Ledger Posting"
        '
        'FrmPaymentReceiptSettlement_Kirana
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.CheckBoxFullCalculate)
        Me.Controls.Add(Me.TxtNature)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmPaymentReceiptSettlement_Kirana"
        Me.Text = "Payment Settlement"
        Me.Controls.SetChildIndex(Me.ChkTemporarilySaved, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
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
        Me.Controls.SetChildIndex(Me.CheckBoxFullCalculate, 0)
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
        Me.MnuOptions.ResumeLayout(False)
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents TxtStructure As AgControls.AgTextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents PnlCalcGrid As System.Windows.Forms.Panel
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
    Friend WithEvents CheckBoxFullCalculate As CheckBox
    Friend WithEvents MnuImportOpeningFromExcel As ToolStripMenuItem
#End Region

    Private Sub FrmLedgerHead_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From LedgerSettlement Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From LedgerSettlement Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From LedgerHeadDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From LedgerHead Where DocId ='" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub ApplyUISetting()
        Dim bNCat As String = ""
        If LblV_Type.Tag <> "" Then bNCat = LblV_Type.Tag Else bNCat = EntryNCat
        GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(Dgl2, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        'GetUISetting_WithDataTables(DglCalc, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bNCat, DglMain.Item(Col1Value, rowV_Type).Tag, "", DglMain.Item(Col1Value, rowSettingGroup).Tag, ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "LedgerHead"
        MainLineTableCsv = "LedgerHeadDetail"
        LogTableName = "LedgerHead_Log"
        LogLineTableCsv = "LedgerHeadDetail_Log"

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
                " From LedgerHead H  With (NoLock) " &
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
                             From LedgerHead H   With (NoLock)
                             LEFT Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type 
                             Left Join SubGroup SGV  With (NoLock) On SGV.SubCode  = H.Vendor  
                             Where 1 = 1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub
    Private Sub Frm_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim errRow As Integer = 0
        Dim I As Integer = 0
        Try
            If DtV_TypeSettings Is Nothing Then Exit Sub
            If DtV_TypeSettings.Rows.Count = 0 Then Exit Sub
            Dgl1.ColumnCount = 0
            With AgCL
                .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
                .AddAgTextColumn(Dgl1, Col1Select, 35, 0, Col1Select, True, True, False)
                .AddAgTextColumn(Dgl1, Col1VoucherType, 120, 0, Col1VoucherType, False, False)
                .AddAgTextColumn(Dgl1, Col1TransactionDocID, 120, 0, Col1TransactionDocID, False, False)
                .AddAgTextColumn(Dgl1, Col1LinkedSubCode, 200, 0, Col1LinkedSubCode, False, False)
                .AddAgNumberColumn(Dgl1, Col1AmountDr, 140, 8, 4, False, Col1AmountDr, True, False, True)
                .AddAgNumberColumn(Dgl1, Col1AmountCr, 140, 8, 3, False, Col1AmountCr, False, False, True)

                .AddAgNumberColumn(Dgl1, Col1InterestPer, 60, 8, 3, False, Col1InterestPer, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1InterestAmount, 60, 8, 3, False, Col1InterestAmount, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1DiscountPer, 60, 8, 3, False, Col1DiscountPer, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1DiscountAmount, 60, 8, 3, False, Col1DiscountAmount, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1SubTotal, 60, 8, 3, False, Col1SubTotal, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1BrokeragePer, 60, 8, 3, False, Col1BrokeragePer, False, False, True)
                .AddAgNumberColumn(Dgl1, Col1BrokerageAmount, 60, 8, 3, False, Col1BrokerageAmount, False, False, True)
                .AddAgTextColumn(Dgl1, Col1Remark, 100, 0, Col1Remark, False, False)
            End With
            AgL.AddAgDataGrid(Dgl1, Pnl1)
            Dgl1.EnableHeadersVisualStyles = False
            Dgl1.ColumnHeadersHeight = 40
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
            AgL.GridDesign(Dgl1)
            Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
            Dgl1.BackgroundColor = Me.BackColor
            Dgl1.Columns(Col1Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)
            AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)

            DglMain.Columns(Col1BtnDetail).ReadOnly = True
            DglMain.Columns(Col1BtnDetail).Visible = False
            DglMain.Columns(Col1Head).Width = 105
            DglMain.Rows.Add(7)
            For I = 0 To DglMain.Rows.Count - 1
                DglMain.Rows(I).Visible = False
                If I <> rowSubCode Then
                    DglMain.Item(Col1BtnDetail, I) = New DataGridViewTextBoxCell
                End If
            Next
            DglMain.Item(Col1Head, rowSubCode).Value = hcSubCode
            DglMain.Item(Col1Head, rowLinkedSubCode).Value = hcLinkedSubCode

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

            Dgl2.Rows.Add(5)
            For I = 0 To Dgl2.Rows.Count - 1
                Dgl2.Rows(I).Visible = False
            Next

            Dgl2.Name = "Dgl2"
            Dgl2.Tag = "VerticalGrid"

            Dgl2.Item(Col1Head, rowLineSubCode).Value = hcLineSubCode
            Dgl2.Item(Col1Head, rowIsFinalPayment).Value = hcIsFinalPayment
            Dgl2.Item(Col1Head, rowAmount).Value = hcAmount
            Dgl2.Item(Col1Head, rowRemarks).Value = hcRemarks


            For I = 0 To Dgl2.Rows.Count - 1
                If AgL.XNull(Dgl2(Col1HeadOriginal, I).Value) = "" Then
                    Dgl2(Col1HeadOriginal, I).Value = Dgl2(Col1Head, I).Value
                End If
            Next


            DglCalc.ColumnCount = 0
            With AgCL
                .AddAgTextColumn(DglCalc, ColSNo, 35, 5, ColSNo, False, True, False)
                .AddAgTextColumn(DglCalc, Col1Head, 170, 255, Col1Head, True, True)
                .AddAgTextColumn(DglCalc, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
                .AddAgTextColumn(DglCalc, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
                .AddAgNumberColumn(DglCalc, Col1Value, 120, 8, 3, False, Col1Value, True, True, True)
                .AddAgTextColumn(DglCalc, Col1LastValue, 170, 255, Col1LastValue, False, False)
            End With
            AgL.AddAgDataGrid(DglCalc, PnlCalcGrid)
            AgL.GridDesign(DglCalc)
            DglCalc.EnableHeadersVisualStyles = False
            DglCalc.ColumnHeadersHeight = 35
            DglCalc.AgSkipReadOnlyColumns = True
            DglCalc.AllowUserToAddRows = False
            DglCalc.RowHeadersVisible = False
            DglCalc.ColumnHeadersVisible = False
            DglCalc.AgSkipReadOnlyColumns = True
            DglCalc.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
            DglCalc.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
            DglCalc.BackgroundColor = Me.BackColor
            DglCalc.Anchor = AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Top

            DglCalc.Rows.Add(8)
            'For I = 0 To DglCalc.Rows.Count - 1
            '    DglCalc.Rows(I).Visible = False
            'Next

            DglCalc.Name = "DglCalc"
            DglCalc.Tag = "VerticalGrid"

            DglCalc.Item(Col1Head, rowTotalBillAmt).Value = hcTotalBillAmt
            DglCalc.Item(Col1Head, rowLessPartyPayment).Value = hcLessPartyPayment
            DglCalc.Item(Col1Head, rowNetBillAmt).Value = hcNetBillAmt
            DglCalc.Item(Col1Head, rowAddInterest).Value = hcAddInterest
            DglCalc.Item(Col1Head, rowLessDicount).Value = hcLessDicount
            DglCalc.Item(Col1Head, rowSubTotal).Value = hcSubTotal
            DglCalc.Item(Col1Head, rowLessBrokerage).Value = hcLessBrokerage
            DglCalc.Item(Col1Head, rowNetReceivable).Value = hcNetReceivable


            For I = 0 To DglCalc.Rows.Count - 1
                If AgL.XNull(DglCalc(Col1HeadOriginal, I).Value) = "" Then
                    DglCalc(Col1HeadOriginal, I).Value = DglCalc(Col1Head, I).Value
                End If
            Next



            ApplyUISetting()

            Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
            AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
            Dgl1.AgSkipReadOnlyColumns = True
            Dgl1.AllowUserToOrderColumns = True
            Dgl1.AllowUserToAddRows = False
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

        mQry = " Update LedgerHead " &
                " Set  " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " SubCode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ", " &
                " LinkedSubCode = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowLinkedSubCode).Tag) & ", " &
                " PartyName = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Value) & ", " &
                " PaidAmount = " & Val(Dgl2.Item(Col1Value, rowAmount).Value) & ", " &
                " IsFinalPayment = " & IIf(AgL.XNull(Dgl2.Item(Col1Value, rowIsFinalPayment).Value) = "Yes", 1, 0) & ", " &
                " Remarks = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowRemarks).Value) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        CType(DglMain.Item(Col1BtnDetail, rowSubCode).Tag, FrmPurchaseInvoiceParty).FSave(mSearchCode, Conn, Cmd)

        mQry = "Delete from LedgerHeadDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into LedgerHeadDetail (DocId, Sr, SubCode, Amount) "
        mQry += " Select " & AgL.Chk_Text(mSearchCode) & ", 1, " &
                " " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowLineSubCode).Tag) & ", " &
                " " & Val(Dgl2.Item(Col1Value, rowAmount).Value) & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from LedgerSettlement Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from TransactionReferences Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Select, I).Value = "þ" Then
                mSr += 1
                mQry = "Insert Into LedgerSettlement (DocId, Sr, 
                    TransactionDocID, LinkedSubCode, AmountDr, AmountCr,
                    Addition1Per, Addition1, Deduction1Per, Deduction2, SubTotal,
                    Addition2Per, Addition2, Remarks)
                    Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1TransactionDocID, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubCode, I).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1AmountDr, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1AmountCr, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1InterestPer, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1InterestAmount, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DiscountPer, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DiscountAmount, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1SubTotal, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1BrokeragePer, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1BrokerageAmount, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & " 
                    "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = "Insert Into TransactionReferences (DocID, DocIDSr, ReferenceDocID, Remark) 
                            Values ('" & mSearchCode & "', " & mSr & ", " & AgL.Chk_Text(Dgl1.Item(Col1TransactionDocID, I).Tag) & ", 
                        'Settlement Entry No." & DglMain.Item(Col1Value, rowReferenceNo).Value & " dated " & DglMain.Item(Col1Value, rowV_Date).Value & " is done for this entry. Can not Modify / Delete.') "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next

        If AgL.StrCmp(Dgl2.Item(Col1Value, rowIsFinalPayment).Value, "Yes") Then
            mQry = "Insert Into LedgerSettlement (DocId, Sr, TransactionDocID)
                Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr + 1 & ", " &
                " " & AgL.Chk_Text(mSearchCode) & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        FPostInLedger(SearchCode, Conn, Cmd)




        If mFlag_Import = False Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
                AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            End If
        End If
    End Sub
    'Private Sub InsertLedgerHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
    '    mQry = "Insert Into LedgerHeadDetail (DocId, Sr, SubCode, Amount) "
    '    mQry += " Select " & AgL.Chk_Text(mSearchCode) & ", 1, " &
    '            " " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowLineSubCode).Tag) & ", " &
    '            " " & Val(Dgl2.Item(Col1Value, rowAmount).Value) & " "
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    'End Sub
    Private Sub UpdateLedgerHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Update LedgerHeadDetail " &
                " SET SubCode = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowLineSubCode).Tag) & ", " &
                " Amount = " & Val(Dgl2.Item(Col1Value, rowAmount).Value) & ", " &
                " Where DocId = '" & mSearchCode & "' " &
                " And Sr = " & Dgl1.Item(ColSNo, LineGridRowIndex).Tag & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet
        Dim mMultiplyWithMinus As Boolean = False

        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            mMultiplyWithMinus = True
        End If

        mIsEntryLocked = False



        mQry = " SELECT H.*, L.SubCode As LineSubCode, LSg.Name As LineAccountName, LkSg.Name As LinkedPartyName, L.Amount
                 From (Select * From LedgerHead  With (NoLock) Where DocID='" & SearchCode & "') H 
                 LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID
                 LEFT JOIN Subgroup LSg ON L.Subcode = LSg.Subcode
                 LEFT JOIN Subgroup LkSg ON H.LinkedSubcode = LkSg.Subcode "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then


                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))
                DglMain.Item(Col1Value, rowSubCode).Tag = AgL.XNull(.Rows(0)("SubCode"))
                DglMain.Item(Col1Value, rowSubCode).Value = AgL.XNull(.Rows(0)("PartyName"))
                DglMain.Item(Col1Value, rowLinkedSubCode).Tag = AgL.XNull(.Rows(0)("LinkedSubCode"))
                DglMain.Item(Col1Value, rowLinkedSubCode).Value = AgL.XNull(.Rows(0)("LinkedPartyName"))
                Dgl2.Item(Col1Value, rowLineSubCode).Tag = AgL.XNull(.Rows(0)("LineSubCode"))
                Dgl2.Item(Col1Value, rowLineSubCode).Value = AgL.XNull(.Rows(0)("LineAccountName"))
                Dgl2.Item(Col1Value, rowAmount).Value = AgL.VNull(.Rows(0)("PaidAmount"))

                Dgl2.Item(Col1Value, rowIsFinalPayment).Value = IIf(AgL.VNull(.Rows(0)("IsFinalPayment")) = 0, "No", "Yes")
                Dgl2.Item(Col1Value, rowRemarks).Value = AgL.XNull(.Rows(0)("Remarks"))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select L.*, IfNull(IfNull(Si.V_Type, Pi.V_Type), Lh.V_Type) As V_Type, Vt.Description As V_TypeDesc,
                        IfNull(IfNull(Si.V_Type || '-' || Si.ManualRefNo, Pi.V_Type || '-' || Pi.ManualRefNo), Lh.V_Type || '-' || Lh.ManualRefNo) As TransactionNo,
                        LSg.Name As LinkedPartyName
                        From (Select * From LedgerSettlement  With (NoLock)  Where DocId = '" & SearchCode & "' And TransactionDocId <> '" & SearchCode & "' ) As L 
                        LEFT JOIN SaleInvoice Si On L.TransactionDocId = Si.DocId
                        LEFT JOIN PurchInvoice Pi On L.TransactionDocId = Pi.DocId
                        LEFT JOIN LedgerHead Lh On L.TransactionDocId = Lh.DocId
                        LEFT JOIN SubGroup LSg On L.LinkedSubCode = LSg.SubCode
                        LEFT JOIN Voucher_Type Vt On IfNull(IfNull(Si.V_Type, Pi.V_Type), Lh.V_Type) = Vt.V_Type
                        Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1VoucherType, I).Tag = AgL.XNull(.Rows(I)("V_Type"))
                            Dgl1.Item(Col1VoucherType, I).Value = AgL.XNull(.Rows(I)("V_TypeDesc"))
                            Dgl1.Item(Col1TransactionDocID, I).Tag = AgL.XNull(.Rows(I)("TransactionDocID"))
                            Dgl1.Item(Col1TransactionDocID, I).Value = AgL.XNull(.Rows(I)("TransactionNo"))
                            Dgl1.Item(Col1LinkedSubCode, I).Tag = AgL.XNull(.Rows(I)("LinkedSubCode"))
                            Dgl1.Item(Col1LinkedSubCode, I).Value = AgL.XNull(.Rows(I)("LinkedPartyName"))
                            Dgl1.Item(Col1AmountDr, I).Value = AgL.VNull(.Rows(I)("AmountDr"))
                            Dgl1.Item(Col1AmountCr, I).Value = AgL.VNull(.Rows(I)("AmountCr"))

                            Dgl1.Item(Col1InterestPer, I).Value = AgL.VNull(.Rows(I)("Addition1Per"))
                            Dgl1.Item(Col1InterestAmount, I).Value = AgL.VNull(.Rows(I)("Addition1Amount"))

                            Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(.Rows(I)("Deduction1Per"))
                            Dgl1.Item(Col1DiscountAmount, I).Value = AgL.VNull(.Rows(I)("Deduction1Amount"))

                            Dgl1.Item(Col1SubTotal, I).Value = AgL.VNull(.Rows(I)("AmountCr"))

                            Dgl1.Item(Col1BrokeragePer, I).Value = AgL.VNull(.Rows(I)("Addition2Per"))
                            Dgl1.Item(Col1BrokerageAmount, I).Value = AgL.VNull(.Rows(I)("Addition2Amount"))
                            Dgl1.Item(Col1Select, I).Value = "þ"
                        Next I
                    End If
                End With
                '-------------------------------------------------------------
            End If
        End With

        If AgL.XNull(Dgl2.Item(Col1Value, rowIsFinalPayment).Value) = "Yes" Then
            Dgl1.Visible = True
            DglCalc.Visible = True
        Else
            Dgl1.Visible = False
            DglCalc.Visible = False
        End If

        Calculation()
        SetAttachmentCaption()
        Dgl1.ReadOnly = True
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
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
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

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
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "LedgerHead",
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

                    FGetPendingTransactionOfParty()
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
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
        mDimensionSrl = 0
        Dgl1.ReadOnly = False

        CheckBoxFullCalculate.Checked = False

        If mV_Type = "" Then
            If DtVoucher_TypeHelpDataSet.Tables(0).Rows.Count > 1 Then
                mQry = "SELECT " & IIf(AgL.PubServerName <> "", "Top 1", "") & " H.V_Type, Vt.Description AS V_TypeDesc  
                FROM LedgerHead H
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

        If AgL.XNull(Dgl2.Item(Col1Value, rowIsFinalPayment).Value) = "Yes" Then
            Dgl1.Visible = True
            DglCalc.Visible = True
        Else
            Dgl1.Visible = False
            DglCalc.Visible = False
        End If


        If DglMain.Visible = True Then
            If DglMain.FirstDisplayedCell IsNot Nothing Then
                If DglMain(Col1Value, rowSettingGroup).Visible = True And DglMain(Col1Value, rowSettingGroup).Value = "" Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowSettingGroup)
                ElseIf DglMain(Col1Value, rowSubCode).Visible = True Then
                    DglMain.CurrentCell = DglMain(Col1Value, rowSubCode)
                Else
                    DglMain.CurrentCell = DglMain(Col1Value, DglMain.FirstDisplayedCell.RowIndex)
                End If
                DglMain.Focus()
            End If
        End If
    End Sub
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
            End Select

            Call Calculation()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        'sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        'sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        'If Topctrl1.Mode = "Browse" Then Exit Sub

        DglCalc.Item(Col1Value, rowTotalBillAmt).Value = "0"
        DglCalc.Item(Col1Value, rowLessPartyPayment).Value = "0"
        DglCalc.Item(Col1Value, rowNetBillAmt).Value = "0"
        DglCalc.Item(Col1Value, rowAddInterest).Value = "0"
        DglCalc.Item(Col1Value, rowLessDicount).Value = "0"
        DglCalc.Item(Col1Value, rowSubTotal).Value = "0"
        DglCalc.Item(Col1Value, rowLessBrokerage).Value = "0"
        DglCalc.Item(Col1Value, rowNetReceivable).Value = "0"

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Select, I).Value = "þ" Then
                If LblV_Type.Tag = Ncat.Payment Then
                    If Dgl1.Rows(I).Tag Is Nothing Then
                        Dgl1.Item(Col1SubTotal, I).Value = Dgl1.Item(Col1AmountCr, I).Value - Dgl1.Item(Col1AmountDr, I).Value
                    End If
                    DglCalc.Item(Col1Value, rowTotalBillAmt).Value = Val(DglCalc.Item(Col1Value, rowTotalBillAmt).Value) + Val(Dgl1.Item(Col1AmountCr, I).Value)
                    DglCalc.Item(Col1Value, rowLessPartyPayment).Value = Val(DglCalc.Item(Col1Value, rowLessPartyPayment).Value) + Val(Dgl1.Item(Col1AmountDr, I).Value)
                Else
                    If Dgl1.Rows(I).Tag Is Nothing Then
                        Dgl1.Item(Col1SubTotal, I).Value = Dgl1.Item(Col1AmountDr, I).Value - Dgl1.Item(Col1AmountCr, I).Value
                    End If
                    DglCalc.Item(Col1Value, rowTotalBillAmt).Value = Val(DglCalc.Item(Col1Value, rowTotalBillAmt).Value) + Val(Dgl1.Item(Col1AmountDr, I).Value)
                    DglCalc.Item(Col1Value, rowLessPartyPayment).Value = Val(DglCalc.Item(Col1Value, rowLessPartyPayment).Value) + Val(Dgl1.Item(Col1AmountCr, I).Value)
                End If

                DglCalc.Item(Col1Value, rowAddInterest).Value = Val(DglCalc.Item(Col1Value, rowAddInterest).Value) + Val(Dgl1.Item(Col1InterestAmount, I).Value)
                DglCalc.Item(Col1Value, rowLessDicount).Value = Val(DglCalc.Item(Col1Value, rowLessDicount).Value) + Val(Dgl1.Item(Col1DiscountAmount, I).Value)
                DglCalc.Item(Col1Value, rowSubTotal).Value = Val(DglCalc.Item(Col1Value, rowSubTotal).Value) + Val(Dgl1.Item(Col1SubTotal, I).Value)
                DglCalc.Item(Col1Value, rowLessBrokerage).Value = Val(DglCalc.Item(Col1Value, rowLessBrokerage).Value) + Val(Dgl1.Item(Col1BrokerageAmount, I).Value)
            End If
        Next I

        DglCalc.Item(Col1Value, rowNetBillAmt).Value = Val(DglCalc.Item(Col1Value, rowTotalBillAmt).Value) - Val(DglCalc.Item(Col1Value, rowLessPartyPayment).Value)
        DglCalc.Item(Col1Value, rowNetReceivable).Value = Val(DglCalc.Item(Col1Value, rowSubTotal).Value) - Val(DglCalc.Item(Col1Value, rowLessBrokerage).Value)

        DglCalc.Item(Col1Value, rowTotalBillAmt).Value = Val(DglCalc.Item(Col1Value, rowTotalBillAmt).Value)
        DglCalc.Item(Col1Value, rowLessPartyPayment).Value = Val(DglCalc.Item(Col1Value, rowLessPartyPayment).Value)
        DglCalc.Item(Col1Value, rowNetBillAmt).Value = Val(DglCalc.Item(Col1Value, rowNetBillAmt).Value)
        DglCalc.Item(Col1Value, rowAddInterest).Value = Val(DglCalc.Item(Col1Value, rowAddInterest).Value)
        DglCalc.Item(Col1Value, rowLessDicount).Value = Val(DglCalc.Item(Col1Value, rowLessDicount).Value)
        DglCalc.Item(Col1Value, rowSubTotal).Value = Val(DglCalc.Item(Col1Value, rowSubTotal).Value)
        DglCalc.Item(Col1Value, rowLessBrokerage).Value = Val(DglCalc.Item(Col1Value, rowLessBrokerage).Value)
        DglCalc.Item(Col1Value, rowNetReceivable).Value = Val(DglCalc.Item(Col1Value, rowNetReceivable).Value)

        If Val(DglCalc.Item(Col1Value, rowNetReceivable).Value) > 0 Then
            Dgl2.Item(Col1Value, rowAmount).Value = Val(DglCalc.Item(Col1Value, rowNetReceivable).Value)
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If mFlag_Import = True Then Exit Sub
        Dim I As Integer = 0
        Dim CheckDuplicateRef As Boolean


        Dgl1.EndEdit()



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
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If
        If mFlag_Import = True Then Exit Sub
        If Dgl1.Focus = True Then
            LblInstructions.Text = "Press F9 To Open Details."
        End If
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Dgl1.CurrentCell.ReadOnly = True
            Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case ColSNo
                    'SendKeys.Send("{Tab}")
                Case Col1TransactionDocID
                Case Col1AmountDr
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempLedgerHead_BaseFunction_DispText() Handles Me.BaseFunction_DispText
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

        If e.KeyCode = Keys.F9 Then
            If Dgl1.CurrentCell IsNot Nothing Then
                FOpenLineDetail(Dgl1.CurrentCell.RowIndex)
            End If
        End If

        If Dgl1.CurrentCell IsNot Nothing Then
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1TransactionDocID).Index)
                        Calculation()
                    End If
            End Select
        End If


        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If Dgl1.CurrentCell IsNot Nothing Then
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1AmountDr
            End Select
        End If
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
            'mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
            '            From LedgerHeadDetail L
            '            LEFT JOIN LedgerHead H On L.DocId = H.DocId
            '            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            '            Where L.LedgerHead = '" & mSearchCode & "' 
            '            And L.LedgerHead <> L.DocId "
            'DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            'If DtRelationalData.Rows.Count > 0 Then
            '    MsgBox("Data Exists For " & DglMain(Col1Value, rowV_Type).Value + "-" + DglMain(Col1Value, rowReferenceNo).Value & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Modify Entry", MsgBoxStyle.Information)
            '    FGetRelationalData = True
            '    Exit Function
            'End If

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


        If ClsMain.IsEntryLockedWithLockText("LedgerHead", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        Passed = Not FGetRelationalData()

        mQry = "Select Count(*) 
                From Barcode H With (NoLock) 
                Left Join BarcodeSiteDetail L With (NoLock) On H.Code = L.Code
                Where H.GenDocID <> L.LastTrnDocID and H.GenDocID = '" & mSearchCode & "'
                And BarcodeType <> '" & BarcodeType.Fixed & "'
               "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Barcodes are in transaction. Can not continue.")
            Passed = False
        End If
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
    Private Sub FrmLedgerHead_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
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
    Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor,
                         Optional ByVal IsPrintToPrinter As Boolean = False)
        'For SSRS Print Out
        Dim DtTemp As DataTable

        mQry = "SELECT H.DocID  FROM LedgerHead H With (NoLock)
                LEFT JOIN LedgerHeadDetail L With (NoLock) ON H.DocID = L.DocID 
                WHERE H.DocID ='" & SearchCode & "' And H.Gross_Amount > 0
                GROUP BY H.DocID 
                HAVING Round(Sum(L.Amount),2)<>Round(Max(H.Gross_Amount),2)"
        If AgL.FillData(mQry, AgL.GCn).Tables(0).Rows.Count > 0 Then
            MsgBox("Something went wrong with gross amount. Can not print Invoice. Please check once.")
            Exit Sub
        End If

        mQry = "SELECT H.DocID, H.Sr, I.Description as ItemName, Round(Sum(L.TotalQty),2),Round(Max(H.Qty),2)  
                FROM LedgerHeadDetail H With (NoLock)
                LEFT JOIN LedgerHeadDimensionDetail L With (NoLock) ON H.DocID = L.DocID And H.Sr = L.TSr
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
        Dim sQryBom As String = ""
        Dim sQryMaterialIssue As String = ""
        Dim mMaterialIssueDocIDs As String = ""


        mPrintTitle = DglMain.Item(Col1Value, rowV_Type).Value

        Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        Dim mDocReportFileName As String = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)
        Dim mBomExists As Integer = AgL.Dman_Execute("Select IsNull(Count(*),0) from LedgerHeadDetailBom Where DocID = '" & mSearchCode & "'", AgL.GcnRead).executescalar()
        Dim mVoucherCategory As String = AgL.Dman_Execute("Select IfNull(Max(Category),'') From Voucher_Type where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "'", AgL.GcnRead).ExecuteScalar()

        If AgL.PubServerName <> "" Then
            'mMaterialIssueDocIDs = AgL.Dman_Execute("Select DocId + ',' From StockHead Where ReferenceDocId = '" & mSearchCode & "' for xml path('')", AgL.GcnRead).executescalar()
            'mMaterialIssueDocIDs = AgL.Dman_Execute("Select DocId + ',' From StockHeadDetail Where ReferenceDocId = '" & mSearchCode & "' for xml path('')", AgL.GcnRead).executescalar()
            mMaterialIssueDocIDs = AgL.Dman_Execute("Select H.DocId + ',' 
                        From LedgerHead H 
                        LEFT JOIN LedgerHeadDetail L On H.DocId = L.DocID 
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        Where Vt.NCat In ('" & Ncat.StockIssue & "')
                        And L.ReferenceDocId = '" & mSearchCode & "' for xml path('')", AgL.GcnRead).executescalar()
        End If



        If LblV_Type.Tag = Ncat.PurchaseReturn Then
            'If AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
            If CType(AgL.VNull(FGetSettings(SettingFields.SalesTaxApplicableInPurchase, SettingType.General)), Boolean) = True Then
                mPrintTitle = DglMain.Item(Col1Value, rowV_Type).Value & " (Debit Note)"
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
                IfNull(Transporter.Name,IfNull(MTransporter.Name,'')) as TransporterName, IfNull(TD.LrNo,'') LrNo, TD.LrDate, IfNull(TD.PrivateMark,'') PrivateMark, TD.Weight, TD.Freight, IfNull(TD.PaymentType,'') as FreightType, IfNull(TD.RoadPermitNo,'') RoadPermitNo, TD.RoadPermitDate, IfNull(L.ReferenceNo,'') as ReferenceNo,
                I.Description as ItemName, IG.Description as ItemGroupName, IC.Description as ItemCatName, 
                I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, IfNull(I.HSN,IC.HSN) as HSN,
                D1.Specification as D1Spec, D2.Specification as D2Spec, D3.Specification as D3Spec, D4.Specification as D4Spec, Size.Specification as SizeSpec,
                '" & AgL.PubCaptionItemType & "' as ItemTypeCaption,'" & AgL.PubCaptionItemCategory & "' as ItemCategoryCaption,
                '" & AgL.PubCaptionItemGroup & "' as ItemGroupCaption,'" & AgL.PubCaptionItem & "' as ItemCaption,'" & AgL.PubCaptionBarcode & "' as BarcodeCaption,
                '" & AgL.PubCaptionDimension1 & "' as D1Caption, '" & AgL.PubCaptionDimension2 & "' as D2Caption, '" & AgL.PubCaptionDimension3 & "' as D3Caption, '" & AgL.PubCaptionDimension4 & "' as D4Caption, 
                L.SalesTaxGroupItem, STGI.GrossTaxRate, 
                (Case when IfNull(Sku.MaintainStockYn,1) =1 AND Sku.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then L.Pcs Else 0 End) as Pcs, 
                (Case when IfNull(Sku.MaintainStockYn,1) =1 AND Sku.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then abs(L.Qty) Else 0 End) as Qty, 
                (Case when IfNull(Sku.MaintainStockYn,1) =1 AND Sku.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then L.Rate Else 0 End) as Rate, 
                L.Unit, U.DecimalPlaces as UnitDecimalPlaces, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, 
                L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as TotalDiscount, 
                abs(L.Amount)+L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as AmountBeforeDiscount,
                Abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) Tax1_Per, Abs(L.Tax1) as Tax1, Abs(L.Tax2_Per) as Tax2_Per, Abs(L.Tax2) as Tax2, Abs(L.Tax3_Per) as Tax3_Per, Abs(L.Tax3) as Tax3, Abs(L.Tax4_Per) as Tax4_Per, Abs(L.Tax4) as Tax4, Abs(L.Tax5_Per) as Tax5_Per, Abs(L.Tax5) as Tax5, Abs(L.Net_Amount) as Net_Amount,
                IfNull(H.Remarks,'') as HRemarks, IfNull(L.Remark,'') as LRemarks,
                abs(H.Gross_Amount) as H_Gross_Amount, H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount,abs(H.Taxable_Amount) as H_Taxable_Amount,abs(H.Tax1_Per) as H_Tax1_Per, abs(H.Tax1) as H_Tax1, 
                abs(H.Tax2_Per) as H_Tax2_Per, abs(H.Tax2) as H_Tax2, abs(H.Tax3_Per) as H_Tax3_Per, abs(H.Tax3) as H_Tax3, abs(H.Tax4_Per) as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                abs(H.Tax5_Per) as H_Tax5_Per, abs(H.Tax5) as H_Tax5, abs(H.Deduction_Per) as H_Deduction_Per, abs(H.Deduction) as H_Deduction, abs(H.Other_Charge_Per) as H_Other_Charge_Per, abs(H.Other_Charge) as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, 
                (Select Sum(L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From LedgerHeadDetail L1 Where L1.DocID = H.DocID) as H_TotalDiscount, 
                (Select Sum(abs(L1.Amount)+L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From LedgerHeadDetail L1 Where L1.DocID = H.DocId) as H_AmountBeforeDiscount,
                '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                L.DimensionDetail as DimDetail, '' as HsnDescription, '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
                from LedgerHead H   With (NoLock)              
                Left Join LedgerHeadDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Item Sku  With (NoLock) On L.Item = Sku.Code
                Left Join Item I  With (NoLock) On LS.Item = I.Code
                Left Join Item D1  With (NoLock) On LS.Dimension1 = D1.Code
                Left Join Item D2  With (NoLock) On LS.Dimension2 = D2.Code
                Left Join Item D3  With (NoLock) On LS.Dimension3 = D3.Code
                Left Join Item D4  With (NoLock) On LS.Dimension4 = D4.Code   
                Left Join Item Size  With (NoLock) On LS.Size = Size.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join Item IG  With (NoLock) On LS.ItemGroup = IG.Code
                Left Join Item IC  With (NoLock) On LS.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.VendorCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join LedgerHeadTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.Vendor = Sg.Subcode     
                Left Join Subgroup SP With (NoLock) On H.ShipToParty = SP.Subcode           
                Left Join Subgroup BP With (NoLock) On H.BillToParty = BP.Subcode           
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join City SC With (NoLock) On SP.CityCode = SC.CityCode
                Left Join State SS with (NoLock) On SC.State = SS.Code
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                Left Join SubgroupSiteDivisionDetail SSD On H.Vendor = SSD.Subcode And H.Div_Code = SSD.Div_Code And H.Site_Code = SSD.Site_Code
                Left Join ViewHelpSubgroup MTransporter  With (NoLock) On SSD.Transporter= MTransporter.Code
                Where H.DocID = '" & mSearchCode & "'
                And L.SubRecordType Is Null
                "


            'If mBomExists > 0 Then
            If sQryBom <> "" Then sQryBom = sQryBom + " Union All "

            sQryBom = sQryBom + "Select '" & I & "' as Copies, Max(H.DocID) DocID,
                                    Max(I.Description) AS ItemName, Max(D1.Description) AS Dimension1Name, Max(D2.Description) AS Dimension2Name,
                                    Max(D3.Description) AS Dimension3Name, Max(D4.Description) AS Dimension4Name, Max(Size.Description) AS SizeName,
                                    Max(IG.Description) AS ItemGroupName, Max(IC.Description) AS ItemCategoryName, 
                                    Max(L.Qty) AS Qty, Max(L.Unit) AS Unit, Max(U.DecimalPlaces) as UnitDecimalPlaces,
                                    '" & AgL.PubCaptionItemType & "' as ItemTypeCaption,'" & AgL.PubCaptionItemCategory & "' as ItemCategoryCaption,
                                    '" & AgL.PubCaptionItemGroup & "' as ItemGroupCaption,'" & AgL.PubCaptionItem & "' as ItemCaption,'" & AgL.PubCaptionBarcode & "' as BarcodeCaption,
                                    '" & AgL.PubCaptionDimension1 & "' as D1Caption, '" & AgL.PubCaptionDimension2 & "' as D2Caption, '" & AgL.PubCaptionDimension3 & "' as D3Caption, '" & AgL.PubCaptionDimension4 & "' as D4Caption               
                                    FROM LedgerHead H  With (NoLock) 
                                    LEFT JOIN LedgerHeadDetail LB With (NoLock)  ON H.DocID = LB.DocID 
                                    LEFT JOIN LedgerHeadDetailBom L With (NoLock)  ON LB.DocID = L.DocID AND LB.Sr = L.TSr 
                                    LEFT JOIN LedgerHeadDetailBomSku LS With (NoLock)  ON L.DocID = LS.DocID AND L.Sr = LS.Sr 
                                    LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type                             
                                    LEFT JOIN Item I With (NoLock)  ON LS.Item = I.Code 
                                    LEFT JOIN Item D1 With (NoLock)  ON LS.Dimension1 = D1.Code 
                                    LEFT JOIN Item D2 With (NoLock)  ON LS.Dimension2 = D2.Code 
                                    LEFT JOIN Item D3 With (NoLock)  ON LS.Dimension3 = D3.Code 
                                    LEFT JOIN Item D4 With (NoLock)  ON LS.Dimension4 = D4.Code 
                                    LEFT JOIN Item Size ON LS.Size = Size.Code 
                                    LEFT JOIN Item IG ON LS.ItemGroup = IG.Code 
                                    LEFT JOIN Item IC ON LS.ItemCategory = IC.Code
                                    Left Join Unit U  With (NoLock) On IfNull(I.Unit,IC.Unit) = U.Code
                                    WHERE H.DocID = '" & mSearchCode & "' And LB.LedgerHead IS NOT NULL 
                                    GROUP BY LB.LedgerHead, LB.LedgerHeadSr   
                                  "
            'End If


            'If mMaterialIssueDocIDs <> "" Then
            If sQryMaterialIssue <> "" Then sQryMaterialIssue = sQryMaterialIssue + " Union All "

            sQryMaterialIssue = sQryMaterialIssue + "Select '" & I & "' as Copies, Max(H.DocID) DocID, Max(H.V_Date) as DocDate,
                                    Max(I.Description) AS ItemName, Max(D1.Description) AS Dimension1Name, Max(D2.Description) AS Dimension2Name,
                                    Max(D3.Description) AS Dimension3Name, Max(D4.Description) AS Dimension4Name, Max(Size.Description) AS SizeName,
                                    Max(IG.Description) AS ItemGroupName, Max(IfNull(IC.Description,I.Description)) AS ItemCategoryName, Max(L.DimensionDetail) as DimensionDetail,
                                    Max(L.Qty) AS Qty, Max(L.Unit) AS Unit, Max(U.DecimalPlaces) as UnitDecimalPlaces,
                                    '" & AgL.PubCaptionItemType & "' as ItemTypeCaption,'" & AgL.PubCaptionItemCategory & "' as ItemCategoryCaption,
                                    '" & AgL.PubCaptionItemGroup & "' as ItemGroupCaption,'" & AgL.PubCaptionItem & "' as ItemCaption,'" & AgL.PubCaptionBarcode & "' as BarcodeCaption,
                                    '" & AgL.PubCaptionDimension1 & "' as D1Caption, '" & AgL.PubCaptionDimension2 & "' as D2Caption, '" & AgL.PubCaptionDimension3 & "' as D3Caption, '" & AgL.PubCaptionDimension4 & "' as D4Caption               
                                    FROM LedgerHead H  With (NoLock)                                     
                                    LEFT JOIN LedgerHeadDetail L With (NoLock)  ON H.DocID = L.DocID
                                    LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type                             
                                    LEFT JOIN Item I With (NoLock)  ON LS.Item = I.Code 
                                    LEFT JOIN Item D1 With (NoLock)  ON LS.Dimension1 = D1.Code 
                                    LEFT JOIN Item D2 With (NoLock)  ON LS.Dimension2 = D2.Code 
                                    LEFT JOIN Item D3 With (NoLock)  ON LS.Dimension3 = D3.Code 
                                    LEFT JOIN Item D4 With (NoLock)  ON LS.Dimension4 = D4.Code 
                                    LEFT JOIN Item Size ON LS.Size = Size.Code 
                                    LEFT JOIN Item IG ON LS.ItemGroup = IG.Code 
                                    LEFT JOIN Item IC ON LS.ItemCategory = IC.Code
                                    Left Join Unit U  With (NoLock) On IfNull(I.Unit,IC.Unit) = U.Code
                                    WHERE (H.DocID In ('" & Replace(mMaterialIssueDocIDs, ",", "','") & "')
                                        Or (H.DocID = '" & mSearchCode & "' And L.SubRecordType = '" & mSubRecordType_StockIssue & "'))
                                    GROUP BY L.DocID, L.Sr   
                                  "
            'End If

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


        Dim sQry As String = ""
        Dim sQryRepName As String = ""
        If mVoucherCategory = VoucherCategory.Production Then
            If sQryBom <> "" Then
                If sQry <> "" Then sQry = sQry & "^"
                If sQryRepName <> "" Then sQryRepName = sQryRepName & "^"
                sQry += sQryBom
                sQryRepName += "BomDetail"
            End If
            If sQryMaterialIssue <> "" Then
                If sQry <> "" Then sQry = sQry & "^"
                If sQryRepName <> "" Then sQryRepName = sQryRepName & "^"
                sQry += sQryMaterialIssue
                sQryRepName += "MaterialIssueDetail"
            End If
        End If



        If mDocReportFileName = "" Then
            If mVoucherCategory = VoucherCategory.Production Then
                ClsMain.FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, "JobInvoice_Print.rpt", mPrintTitle, , sQry, sQryRepName, DglMain.Item(Col1Value, rowSubCode).Tag, DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
            Else
                ClsMain.FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, "LedgerHead_Print.rpt", mPrintTitle, , sQry, sQryRepName, DglMain.Item(Col1Value, rowSubCode).Tag, DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
            End If
        Else
            ClsMain.FPrintThisDocument(Me, objRepPrint, DglMain.Item(Col1Value, rowV_Type).Tag, mQry, mDocReportFileName, mPrintTitle, , sQry, sQryRepName, DglMain.Item(Col1Value, rowSubCode).Tag, DglMain.Item(Col1Value, rowV_Date).Value, IsPrintToPrinter)
        End If

    End Sub
    Private Sub FrmLedgerHead_StoreItem_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
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
                    From LedgerHead H  With (NoLock)
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
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim DsTemp As DataSet
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
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
                Case rowSubCode
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpSubgroup()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowLinkedSubCode
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpLinkedParty()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
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
                Case rowIsFinalPayment
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT 1 As Code, 'Yes' As Name
                                    UNION ALL 
                                    SELECT 0 As Code, 'No' As Name "
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowLineSubCode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select Subcode, Name From Subgroup Where Nature In ('Bank','Cash') Order By Name"
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value, 0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select

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
    Private Function FHPGD_UnRelatedStockIn(ByRef Code As String, ByRef Description As String) As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = "SELECT 'o' As Tick, L.DocID AS Code, Max(L.V_Type + '-' + L.RecId) AS StockInNo
                FROM Stock L
                LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                LEFT JOIN (
                    Select Pis.StockInDocId
                    From LedgerHeadUnRelatedStockIn Pis
                    Where Pis.DocId <> '" & mSearchCode & "'
                ) As VPis On L.DocId = VPis.StockInDocId
                WHERE Vt.NCat = '" & Ncat.PurchaseGoodsReceipt & "'
                And VPis.StockInDocId Is Null
                GROUP BY L.DocID "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 300, 230, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Stock In No", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            Code = FRH_Multiple.FFetchData(1, "", "", ",")
            Description = FRH_Multiple.FFetchData(2, "", "", ",")
        Else
            Code = ""
            Description = ""
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



    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        DGL.Visible = False
    End Sub
    Private Sub FGetPurchIndent(ByVal ItemCode As String, ByRef PurchIndent As String)
        mQry = " Select H.DocId From PurchIndent H  With (NoLock) LEFT JOIN PurchIndentDetail L  With (NoLock) On H.DocId = L.DocId " &
                " Where L.Item = '" & ItemCode & "' " &
                " And H.V_Date <= '" & DglMain.Item(Col1Value, rowV_Date).Value & "' " &
                " Order By H.V_Date  "
        PurchIndent = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    End Sub
    Private Sub Dgl1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        Dim mRow As Integer
        mRow = e.RowIndex
        If Dgl1.CurrentCell IsNot Nothing Then
            FOpenLineDetail(Dgl1.CurrentCell.RowIndex)
        End If
    End Sub
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
    Private Sub FrmLedgerHeadDirect_BaseEvent_Topctrl_tbMore() Handles Me.BaseEvent_Topctrl_tbMore
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
                Case rowIsFinalPayment
                    If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
                        Dgl2.Item(Col1Value, rowIsFinalPayment).ReadOnly = True
                    End If

                Case rowAmount
                    If AgL.StrCmp(Dgl2.Item(Col1Value, rowIsFinalPayment).Value, "Yes") Then
                        Dgl2.Item(Col1Value, rowAmount).ReadOnly = True
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmLedgerHeadDirect_BaseEvent_DglMainContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainContentClick
        Try
            Select Case DglMain.Columns(e.ColumnIndex).Name
                Case Col1BtnDetail

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub




    Private Sub FrmLedgerHeadDirect_BaseEvent_DglMainKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainKeyDown
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
    Private Sub InsertLedgerHeadBarcodeLastTransactionDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        If Dgl1.Item(Col1TransactionDocID, LineGridRowIndex).Tag <> "" Then
            mQry = "
                        INSERT INTO LedgerHeadBarcodeLastTransactionValues 
                        (DocID, Sr, LastTrnDiv_Code, LastTrnSite_Code, LastTrnDocID, LastTrnSr, LastTrnV_Type, LastTrnManualRefNo, LastTrnSubcode, LastTrnProcess, CurrentGodown, Status)
                        select '" & DocID & "' DocID, " & Sr & " Sr, Div_Code, Site_Code, LastTrnDocID, LastTrnSr, LastTrnV_Type, LastTrnManualRefNo, LastTrnSubcode, LastTrnProcess, CurrentGodown, Status
                        From BarCodeSiteDetail  With (NoLock)
                        WHERE CODE='" & Dgl1.Item(Col1TransactionDocID, LineGridRowIndex).Tag & "' 
                        AND Div_Code='" & TxtDivision.Tag & "' 
                        And Site_code='" & DglMain.Item(Col1Value, rowSite_Code).Tag & "'                    
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

        If Dgl1.Item(Col1TransactionDocID, LineGridRowIndex).Tag <> "" And mBarcodeStatus <> "" Then
            mQry = "Update BarcodeSiteDetail Set
                                LastTrnDocID = " & AgL.Chk_Text(DocID) & ",
                                LastTrnSr=" & AgL.Chk_Text(Sr) & ",
                                LastTrnV_Type=" & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ",
                                LastTrnManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",
                                LastTrnSubcode=" & AgL.Chk_Text(DglMain.Item(Col1Value, rowSubCode).Tag) & ",
                                LastTrnProcess=" & AgL.Chk_Text(Process.Purchase) & ",
                                Status = " & AgL.Chk_Text(mBarcodeStatus) & "
                                WHERE CODE='" & Dgl1.Item(Col1TransactionDocID, LineGridRowIndex).Tag & "' 
                                AND Div_Code='" & TxtDivision.Tag & "' 
                                And Site_code='" & DglMain.Item(Col1Value, rowSite_Code).Tag & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub FrmLedgerHeadDirect_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
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
    Private Sub CheckBoxFullCalculate_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFullCalculate.CheckedChanged
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
    Private Sub FrmSaleInvoiceDirect_WithDimension_BaseEvent_DglMainCellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Me.BaseEvent_DglMainCellEnter
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
    Private Function FGetNarrationStr(bDocId As String, Conn As Object, Cmd As Object, SettingFieldName As String) As String
        Dim NarrationStrColumns As String = ""
        Dim NarrationStr As String = ""

        NarrationStrColumns = FGetSettings(SettingFieldName, SettingType.General)
        If NarrationStrColumns <> "" Then
            mQry = " Select L.DocID "
            If NarrationStrColumns.ToUpper.Contains("PARTY NAME") Then
                mQry += " ,H.VendorName As PartyName "
            End If
            If NarrationStrColumns.ToUpper.Contains("PARTY DOC.NO.") Then
                mQry += " ,IfNull(H.VendorDocNo,'') As PartyDocNo "
            End If
            If NarrationStrColumns.ToUpper.Contains("PARTY DOC.DATE") Then
                mQry += " ,IfNull(H.VendorDocDate,'') As PartyDocDate "
            End If
            If NarrationStrColumns.ToUpper.Contains("ITEM CATEGORY") Then
                mQry += " ,IfNull(Ic.Description,'') As ItemCategory "
            End If
            mQry += " ,Sum(L.Qty) As Qty 
                From LedgerHead H With (NoLock)
                LEFT JOIN LedgerHeadDetail L  With (NoLock) On H.DocId = L.DocId
                LEFT JOIN Item I With (NoLock) ON L.Item = I.Code
                LEFT JOIN Item Ic  With (NoLock) On I.ItemCategory = Ic.Code
                Where L.DocId = '" & bDocId & "'
                Group By L.DocId "
            If NarrationStrColumns.ToUpper.Contains("PARTY NAME") Then
                mQry += " ,H.VendorName "
            End If
            If NarrationStrColumns.ToUpper.Contains("PARTY DOC.NO.") Then
                mQry += " ,IfNull(H.VendorDocNo,'')  "
            End If
            If NarrationStrColumns.ToUpper.Contains("PARTY DOC.DATE") Then
                mQry += " ,IfNull(H.VendorDocDate,'') "
            End If
            If NarrationStrColumns.ToUpper.Contains("ITEM CATEGORY") Then
                mQry += " ,IfNull(Ic.Description,'') "
            End If
            Dim DtSalesTaxSummary As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

            For I As Integer = 0 To DtSalesTaxSummary.Rows.Count - 1
                If NarrationStrColumns.ToUpper.Contains("PARTY NAME") And I = 0 Then
                    If AgL.XNull(DtSalesTaxSummary.Rows(I)("PartyName")) <> "" Then
                        NarrationStr += AgL.XNull(DtSalesTaxSummary.Rows(I)("PartyName")) + " "
                    End If
                End If
                If NarrationStrColumns.ToUpper.Contains("PARTY DOC.NO.") And I = 0 Then
                    If AgL.XNull(DtSalesTaxSummary.Rows(I)("PartyDocNo")) <> "" Then
                        NarrationStr += "Doc.No." & AgL.XNull(DtSalesTaxSummary.Rows(I)("PartyDocNo")) + " "
                    End If
                End If
                If NarrationStrColumns.ToUpper.Contains("PARTY DOC.DATE") And I = 0 Then
                    If AgL.XNull(DtSalesTaxSummary.Rows(I)("PartyDocNo")) <> "" Then
                        NarrationStr += "Doc.Date " & AgL.XNull(DtSalesTaxSummary.Rows(I)("PartyDocDate")) + " "
                    End If
                End If
                If NarrationStrColumns.ToUpper.Contains("ITEM CATEGORY") Then
                    If AgL.XNull(DtSalesTaxSummary.Rows(I)("ItemCategory")) <> "" Then
                        NarrationStr += "Item " & AgL.XNull(DtSalesTaxSummary.Rows(I)("ItemCategory")) + " "
                    End If
                End If
                If NarrationStrColumns.ToUpper.Contains("QTY") Then
                    NarrationStr += " Qty. " & AgL.XNull(DtSalesTaxSummary.Rows(I)("Qty"))
                End If
                If I < DtSalesTaxSummary.Rows.Count - 1 Then
                    NarrationStr += ", "
                End If
            Next
        End If
        FGetNarrationStr = NarrationStr
    End Function
    Private Sub FGetPendingTransactionOfParty()
        If AgL.XNull(Dgl2.Item(Col1Value, rowIsFinalPayment).Value) = "Yes" Then
            Dgl1.Visible = True
            DglCalc.Visible = True

            mQry = "SELECT H.DocID, Max(H.V_Type) As V_Type, Max(Vt.Description) As V_TypeDesc, Max(H.V_Type || '-' || H.ManualRefNo) AS InvoiceNo,
                H.LinkedParty As LinkedSubcode, Sg.Name As LinkedPartyName,
                Sum(CASE WHEN Vt.NCat IN ('" & Ncat.SaleInvoice & "') THEN L.Net_Amount ELSE 0 END) AS AmtDr,
                Sum(CASE WHEN Vt.NCat IN ('" & Ncat.SaleReturn & "') THEN L.Net_Amount ELSE 0 END) AS AmtCr
                FROM SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Subgroup SG ON H.LinkedParty = Sg.Subcode	
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN LedgerSettlement Ls ON H.DocId = Ls.TransactionDocId
                WHERE H.BillToParty = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "'
                AND Vt.NCat IN ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "')
                AND Ls.DocId IS NULL
                GROUP BY H.DocID

                UNION ALL

                SELECT H.DocID, Max(H.V_Type) As V_Type, Max(Vt.Description) As V_TypeDesc, Max(H.V_Type || '-' || H.ManualRefNo) AS InvoiceNo, 
                H.LinkedParty As LinkedSubcode, Sg.Name As LinkedPartyName,
                Sum(CASE WHEN Vt.NCat IN ('" & Ncat.PurchaseReturn & "') THEN L.Net_Amount ELSE 0 END) AS AmtDr,
                Sum(CASE WHEN Vt.NCat IN ('" & Ncat.PurchaseInvoice & "') THEN L.Net_Amount ELSE 0 END) AS AmtCr
                FROM PurchInvoice H 
                LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Subgroup SG ON H.LinkedParty = Sg.Subcode	
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN LedgerSettlement Ls ON H.DocId = Ls.TransactionDocId
                WHERE H.BillToParty = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "'
                AND Vt.NCat IN ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "')
                AND Ls.DocId IS NULL
                GROUP BY H.DocID

                UNION ALL 

                SELECT H.DocID, Max(H.V_Type) As V_Type, Max(Vt.Description) As V_TypeDesc, Max(H.V_Type || '-' || H.ManualRefNo) AS InvoiceNo, 
                H.LinkedSubcode, Sg.Name As LinkedPartyName,
                Sum(CASE WHEN Vt.NCat IN ('" & Ncat.Payment & "') THEN (CASE WHEN IsNull(Lc.Net_Amount,0) <> 0 THEN IsNull(Lc.Net_Amount,0) ELSE IsNull(L.Amount,0) END) ELSE 0 END) AS AmtDr,
                Sum(CASE WHEN Vt.NCat IN ('" & Ncat.Receipt & "') THEN (CASE WHEN IsNull(Lc.Net_Amount,0) <> 0 THEN IsNull(Lc.Net_Amount,0) ELSE IsNull(L.Amount,0) END) ELSE 0 END) AS AmtCr
                FROM LedgerHead H 
                LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID
                LEFT JOIN LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID AND L.Sr = Lc.Sr
                LEFT JOIN Subgroup SG ON H.LinkedSubcode = Sg.Subcode	
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN LedgerSettlement Ls ON H.DocId = Ls.TransactionDocId
                WHERE H.SubCode = '" & DglMain.Item(Col1Value, rowSubCode).Tag & "'
                AND Vt.NCat IN ('" & Ncat.Payment & "','" & Ncat.Receipt & "')
                AND Ls.DocId IS NULL
                GROUP BY H.DocID "
            Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If DtTemp.Rows.Count > 0 Then
                For I As Integer = 0 To DtTemp.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1Select, I).Value = "o"
                    Dgl1.Item(Col1VoucherType, I).Tag = AgL.XNull(DtTemp.Rows(I)("V_Type"))
                    Dgl1.Item(Col1VoucherType, I).Value = AgL.XNull(DtTemp.Rows(I)("V_TypeDesc"))
                    Dgl1.Item(Col1TransactionDocID, I).Tag = AgL.XNull(DtTemp.Rows(I)("DocID"))
                    Dgl1.Item(Col1TransactionDocID, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                    Dgl1.Item(Col1LinkedSubCode, I).Tag = AgL.XNull(DtTemp.Rows(I)("LinkedSubcode"))
                    Dgl1.Item(Col1LinkedSubCode, I).Value = AgL.XNull(DtTemp.Rows(I)("LinkedPartyName"))
                    Dgl1.Item(Col1AmountDr, I).Value = AgL.VNull(DtTemp.Rows(I)("AmtDr"))
                    Dgl1.Item(Col1AmountCr, I).Value = AgL.VNull(DtTemp.Rows(I)("AmtCr"))
                Next I
            End If
        Else
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()

            Dgl1.Visible = False
            DglCalc.Visible = False
        End If
        Calculation()
    End Sub
    Private Sub Dgl1_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgl1.MouseUp
        Dim mRow As Integer
        Try
            If Dgl1.CurrentCell IsNot Nothing Then mRow = Dgl1.CurrentCell.RowIndex
            If e.Button = Windows.Forms.MouseButtons.Left Then
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Select).Index Then
                    ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1TransactionDocID).Index)
                End If
            End If
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub FOpenLineDetail(mRowIndex As Integer)
        mQry = " Select NCat From Voucher_Type Where V_Type = '" & Dgl1.Item(Col1VoucherType, mRowIndex).Tag & "'"
        Dim bNCat = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
        If bNCat <> Ncat.SaleInvoice And bNCat <> Ncat.PurchaseInvoice Then
            Exit Sub
        End If

        If Dgl1.Rows(mRowIndex).Tag IsNot Nothing Then
            CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).EntryMode = Topctrl1.Mode
            CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).ShowDialog()
        Else
            Dim FrmObj As New FrmPaymentReceiptSettlementLine_Kirana
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.IniGrid(Dgl1.Item(Col1TransactionDocID, mRowIndex).Tag)
            FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowBillType).Tag = Dgl1.Item(Col1VoucherType, mRowIndex).Tag
            FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowBillType).Value = Dgl1.Item(Col1VoucherType, mRowIndex).Value
            FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowBillNo).Tag = Dgl1.Item(Col1TransactionDocID, mRowIndex).Tag
            FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowBillNo).Value = Dgl1.Item(Col1TransactionDocID, mRowIndex).Value
            FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowPartyName).Tag = Dgl1.Item(Col1LinkedSubCode, mRowIndex).Tag
            FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowPartyName).Value = Dgl1.Item(Col1LinkedSubCode, mRowIndex).Value
            If Val(Dgl1.Item(Col1AmountCr, mRowIndex).Value) <> 0 Then
                FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowAmount).Value = Dgl1.Item(Col1AmountCr, mRowIndex).Value
            ElseIf Val(Dgl1.Item(Col1AmountDr, mRowIndex).Value) <> 0 Then
                FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowAmount).Value = Dgl1.Item(Col1AmountDr, mRowIndex).Value
            End If
            FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowSubTotal).Value = FrmObj.DglMain.Item(Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowAmount).Value
            FrmObj.FMoveRecForGrid()
            Dgl1.Rows(mRowIndex).Tag = FrmObj
            FrmObj.ShowDialog()
        End If
        Dgl1.Item(Col1InterestPer, mRowIndex).Value = CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).DglMain.Item(FrmPaymentReceiptSettlement_Kirana.Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowInterestPer).Value
        Dgl1.Item(Col1InterestAmount, mRowIndex).Value = CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).DglMain.Item(FrmPaymentReceiptSettlement_Kirana.Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowInterestAmount).Value
        Dgl1.Item(Col1DiscountPer, mRowIndex).Value = CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).DglMain.Item(FrmPaymentReceiptSettlement_Kirana.Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowDiscountPer).Value
        Dgl1.Item(Col1DiscountAmount, mRowIndex).Value = CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).DglMain.Item(FrmPaymentReceiptSettlement_Kirana.Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowDiscountAmount).Value
        Dgl1.Item(Col1SubTotal, mRowIndex).Value = CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).DglMain.Item(FrmPaymentReceiptSettlement_Kirana.Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowSubTotal).Value
        Dgl1.Item(Col1BrokeragePer, mRowIndex).Value = CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).DglMain.Item(FrmPaymentReceiptSettlement_Kirana.Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowBrokeragePer).Value
        Dgl1.Item(Col1BrokerageAmount, mRowIndex).Value = CType(Dgl1.Rows(mRowIndex).Tag, FrmPaymentReceiptSettlementLine_Kirana).DglMain.Item(FrmPaymentReceiptSettlement_Kirana.Col1Value, FrmPaymentReceiptSettlementLine_Kirana.rowBrokerageAmount).Value
        Calculation()
    End Sub
    Private Sub FPostInLedger(SearchCode As String, Conn As Object, Cmd As Object)
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
                    Max(H.V_Date) AS V_Date, Max(H.SubCode) AS SubCode, Max(L.SubCode) AS ContraSub, "

        If LblV_Type.Tag = Ncat.Payment Then
            mQry += " Sum(L.Amount) AS AmtDr, 0 AS AmtCr, 
                    'Being Amount Paid To ' || Max(H.PartyName) AS Narration, "
        Else
            mQry += " 0 AS AmtDr, Sum(L.Amount) AS AmtCr, 
                    'Being Amount Received From ' || Max(H.PartyName) AS Narration, "
        End If

        mQry += " Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                    Max(H.ManualRefNo) AS RecId
                    FROM LedgerHead H With (NoLock)
                    LEFT JOIN LedgerHeadDetail L With (NoLock) ON H.DocID = L.DocID
                    WHERE H.DocId = '" & SearchCode & "'
                    GROUP BY H.DocID	

                    UNION ALL

                    SELECT H.DocId, 2 AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                    Max(H.V_Date) AS V_Date, Max(L.SubCode) AS SubCode, Max(H.SubCode) AS ContraSub,  "

        If LblV_Type.Tag = Ncat.Payment Then
            mQry += "Sum(L.Amount) AS AmtCr, 0 AS AmtDr, 
                    'Being Amount Paid To ' || Max(H.PartyName) AS Narration, "
        Else
            mQry += "Sum(L.Amount) AS AmtCr, 0 AS AmtDr, 
                    'Being Amount Received From ' || Max(H.PartyName) AS Narration, "
        End If

        mQry += "Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                    Max(H.ManualRefNo) AS RecId
                    FROM LedgerHead H With (NoLock)
                    LEFT JOIN LedgerHeadDetail L With (NoLock) ON H.DocID = L.DocID
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
            Case rowIsFinalPayment
                FGetPendingTransactionOfParty()
        End Select
    End Sub
    Private Sub Dgl1_LostFocus(sender As Object, e As EventArgs) Handles Dgl1.LostFocus
        LblInstructions.Text = ""
    End Sub
End Class
