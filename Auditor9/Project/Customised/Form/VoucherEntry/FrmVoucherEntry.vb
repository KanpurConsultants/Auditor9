﻿Imports Microsoft.Reporting.WinForms
Imports System.Xml
Imports Customised.ClsMain
Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Linq

Public Class FrmVoucherEntry
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1EffectiveDate As String = "Effective Date"
    Public Const Col1Barcode As String = "Barcode"
    Public Const Col1Subcode As String = "Subcode"
    Public Const Col1LinkedSubcode As String = "LinkedSubcode"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Public Const Col1HSN As String = "HSN"
    Public Const Col1ReferenceNo As String = "Reference No"
    Public Const Col1SpecificationDocId As String = "Specification DocId"
    Public Const Col1SpecificationDocIdSr As String = "Specification DocId Sr"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1ChqRefNo As String = "Chq/Ref No"
    Public Const Col1ChqRefDate As String = "Chq/Ref Date"
    Public Const Col1Deduction As String = "Deduction"
    Public Const Col1OtherCharges As String = "Other Charges"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1ReconcileDate As String = "Reconcile Date"
    Public Const Col1CurrentBalance As String = "Current Balance"
    Public Const Col1IsRecordLocked As String = "Is Record Locked"
    Public Const Col1AmountInWords As String = "Amount In Words"
    Public Const Col1FormattedDate As String = "FormattedDate"
    Public Const Col1Nature As String = "Nature"
    Public Const Col1SubgroupType As String = "A/c Type"
    Public Const Col1TempSno As String = "Temp Sno"


    Public Const Col1TempAmountForTdsCalculation As String = "Temp Amount For Tds Calculation"
    Public Const Col1TdsCategory As String = "Tds Category"
    Public Const Col1TdsGroup As String = "Tds Group"
    Public Const Col1TdsLedgerAccount As String = "Tds Ledger Account"
    Public Const Col1TdsMonthlyLimit As String = "Tds Month Limit"
    Public Const Col1TdsYearlyLimit As String = "Tds Year Limit"
    Public Const Col1PartyMonthTransaction As String = "Party Month Transaction"
    Public Const Col1PartyYearTransaction As String = "Party Year Transaction"
    Public Const Col1TdsTaxableAmount As String = "Tds Taxable Amount"
    Public Const Col1TdsPer As String = "Tds Per"
    Public Const Col1TdsAmount As String = "Tds Amount"


    '========================================================================

    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2DocDate As String = "Doc. Dt."
    Protected Const Col2DocNo As String = "Doc No."
    Protected Const Col2Narration As String = "Narration"
    Protected Const Col2DocAmount As String = "Doc Amt"
    Protected Const Col2BalAmount As String = "Bal Amt"
    Protected Const Col2RunningBalance As String = "Run. Bal."
    Protected Const Col2DrCr As String = "DrCr"

    Public Const hcType As String = "Type"

    Dim mIsEntryLocked As Boolean = False

    Dim SettingFields_CopyRemarkInNextLineYn As Boolean = False
    Dim SettingFields_MaximumItemLimit As Integer = 0


    Dim mPrevRowIndex As Integer = 0
    Protected WithEvents LblCurrentBalance As Label
    Public WithEvents TxtVoucherCategory As AgControls.AgTextBox
    Protected WithEvents Label3 As Label
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuEditSave As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Dim Dgl As New AgControls.AgDataGrid
    Protected WithEvents PnlFifo As Panel
    Friend WithEvents MnuCancelEntry As ToolStripMenuItem
    Friend WithEvents MnuReport As ToolStripMenuItem
    Public Shared mFlag_Import As Boolean = False
    Friend WithEvents MnuImportGSTDataFromDos As ToolStripMenuItem
    Friend WithEvents MnuImportGSTDataFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Protected WithEvents TxtPartyDocDate As AgControls.AgTextBox
    Protected WithEvents LblPartyDocDate As Label
    Protected WithEvents TxtPartyDocNo As AgControls.AgTextBox
    Protected WithEvents LblPartyDocNo As Label
    Public WithEvents TxtBank As AgControls.AgTextBox
    Protected WithEvents LblBank As Label
    Friend WithEvents MnuPrintCheque As ToolStripMenuItem
    Protected WithEvents LblType As Label
    Public WithEvents TxtType As AgControls.AgTextBox
    Protected WithEvents BtnAttachments As Button
    Friend WithEvents MnuBankFormat As ToolStripMenuItem
    Friend WithEvents MnuShowLedgerPosting As ToolStripMenuItem
    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay

    Class OutstandingBill
        Public DocNo As String
        Public DocDate As Date
        Public Narration As String
        Public DocAmount As Double
        Public BalAmount As Double
        Public DrCr As String
    End Class


    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from LedgerHeadSetting H  With (NoLock) Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "')  "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVoucherEntry))
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtPartyName = New AgControls.AgTextBox()
        Me.LblPartyName = New System.Windows.Forms.Label()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalAmount = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalAmountText = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.TxtStructure = New AgControls.AgTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TxtRemarks = New AgControls.AgTextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.LblCurrency = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.PnlCalcGrid = New System.Windows.Forms.Panel()
        Me.TxtNature = New AgControls.AgTextBox()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox()
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LblCurrentBalance = New System.Windows.Forms.Label()
        Me.TxtVoucherCategory = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportGSTDataFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportGSTDataFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEditSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCancelEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrintCheque = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBankFormat = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.PnlFifo = New System.Windows.Forms.Panel()
        Me.TxtPartyDocNo = New AgControls.AgTextBox()
        Me.LblPartyDocNo = New System.Windows.Forms.Label()
        Me.TxtPartyDocDate = New AgControls.AgTextBox()
        Me.LblPartyDocDate = New System.Windows.Forms.Label()
        Me.TxtBank = New AgControls.AgTextBox()
        Me.LblBank = New System.Windows.Forms.Label()
        Me.TxtType = New AgControls.AgTextBox()
        Me.LblType = New System.Windows.Forms.Label()
        Me.BtnAttachments = New System.Windows.Forms.Button()
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
        Me.PnlTotals.SuspendLayout()
        Me.Pnl1.SuspendLayout()
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(192, 581)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(131, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(125, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(608, 581)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(180, 645)
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
        Me.GBoxDivision.Location = New System.Drawing.Point(399, 581)
        Me.GBoxDivision.Size = New System.Drawing.Size(133, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Size = New System.Drawing.Size(127, 18)
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
        Me.Label2.Location = New System.Drawing.Point(363, 31)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Date.Location = New System.Drawing.Point(258, 26)
        Me.LblV_Date.Size = New System.Drawing.Size(77, 14)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(597, 12)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(380, 25)
        Me.TxtV_Date.Size = New System.Drawing.Size(122, 16)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(505, 8)
        Me.LblV_Type.Size = New System.Drawing.Size(78, 14)
        Me.LblV_Type.Tag = ""
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Type.Location = New System.Drawing.Point(615, 6)
        Me.TxtV_Type.Size = New System.Drawing.Size(179, 16)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(363, 12)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(258, 7)
        Me.LblSite_Code.Size = New System.Drawing.Size(95, 14)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(380, 6)
        Me.TxtSite_Code.Size = New System.Drawing.Size(122, 16)
        Me.TxtSite_Code.TabIndex = 0
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
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 17)
        Me.TabControl1.Size = New System.Drawing.Size(992, 149)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.BtnAttachments)
        Me.TP1.Controls.Add(Me.LblType)
        Me.TP1.Controls.Add(Me.TxtType)
        Me.TP1.Controls.Add(Me.TxtBank)
        Me.TP1.Controls.Add(Me.LblBank)
        Me.TP1.Controls.Add(Me.TxtPartyDocDate)
        Me.TP1.Controls.Add(Me.LblPartyDocDate)
        Me.TP1.Controls.Add(Me.TxtPartyDocNo)
        Me.TP1.Controls.Add(Me.LblPartyDocNo)
        Me.TP1.Controls.Add(Me.TxtVoucherCategory)
        Me.TP1.Controls.Add(Me.TxtNature)
        Me.TP1.Controls.Add(Me.Panel3)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.Panel2)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtPartyName)
        Me.TP1.Controls.Add(Me.LblPartyName)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 123)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyName, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel2, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.TxtVoucherCategory, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyDocDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyDocDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBank, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBank, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtType, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblType, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnAttachments, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(597, 31)
        Me.Label1.TabIndex = 737
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.AgMandatory = True
        Me.TxtReferenceNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferenceNo.Location = New System.Drawing.Point(615, 25)
        Me.TxtReferenceNo.Size = New System.Drawing.Size(179, 16)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(505, 25)
        Me.LblReferenceNo.Size = New System.Drawing.Size(68, 14)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Entry No."
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
        Me.Label4.Location = New System.Drawing.Point(363, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 694
        Me.Label4.Text = "Ä"
        '
        'TxtPartyName
        '
        Me.TxtPartyName.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyName.AgLastValueTag = Nothing
        Me.TxtPartyName.AgLastValueText = Nothing
        Me.TxtPartyName.AgMandatory = True
        Me.TxtPartyName.AgMasterHelp = False
        Me.TxtPartyName.AgNumberLeftPlaces = 8
        Me.TxtPartyName.AgNumberNegetiveAllow = False
        Me.TxtPartyName.AgNumberRightPlaces = 2
        Me.TxtPartyName.AgPickFromLastValue = False
        Me.TxtPartyName.AgRowFilter = ""
        Me.TxtPartyName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyName.AgSelectedValue = Nothing
        Me.TxtPartyName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyName.Location = New System.Drawing.Point(380, 44)
        Me.TxtPartyName.MaxLength = 0
        Me.TxtPartyName.Name = "TxtPartyName"
        Me.TxtPartyName.Size = New System.Drawing.Size(414, 16)
        Me.TxtPartyName.TabIndex = 4
        '
        'LblPartyName
        '
        Me.LblPartyName.AutoSize = True
        Me.LblPartyName.BackColor = System.Drawing.Color.Transparent
        Me.LblPartyName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyName.Location = New System.Drawing.Point(258, 44)
        Me.LblPartyName.Name = "LblPartyName"
        Me.LblPartyName.Size = New System.Drawing.Size(85, 14)
        Me.LblPartyName.TabIndex = 693
        Me.LblPartyName.Text = "Party Name"
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(4, 386)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 694
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(97, 3)
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
        Me.LblTotalAmount.Location = New System.Drawing.Point(900, 4)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(12, 3)
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
        Me.LblTotalAmountText.Location = New System.Drawing.Point(796, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Controls.Add(Me.PnlCustomGrid)
        Me.Pnl1.Location = New System.Drawing.Point(4, 190)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 195)
        Me.Pnl1.TabIndex = 9
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(810, 114)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(17, 108)
        Me.PnlCustomGrid.TabIndex = 3
        Me.PnlCustomGrid.Visible = False
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
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemarks.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(380, 100)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(414, 16)
        Me.TxtRemarks.TabIndex = 7
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(258, 101)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(65, 14)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
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
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 169)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Detail For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCalcGrid.Location = New System.Drawing.Point(651, 413)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(327, 157)
        Me.PnlCalcGrid.TabIndex = 10
        '
        'TxtNature
        '
        Me.TxtNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtNature.AgLastValueTag = Nothing
        Me.TxtNature.AgLastValueText = Nothing
        Me.TxtNature.AgMandatory = False
        Me.TxtNature.AgMasterHelp = False
        Me.TxtNature.AgNumberLeftPlaces = 8
        Me.TxtNature.AgNumberNegetiveAllow = False
        Me.TxtNature.AgNumberRightPlaces = 2
        Me.TxtNature.AgPickFromLastValue = False
        Me.TxtNature.AgRowFilter = ""
        Me.TxtNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNature.AgSelectedValue = Nothing
        Me.TxtNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNature.Location = New System.Drawing.Point(736, 162)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(95, 18)
        Me.TxtNature.TabIndex = 10
        Me.TxtNature.Visible = False
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(486, 639)
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
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(678, 640)
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
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(58, 9)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnImprtFromExcel.TabIndex = 669
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(4, 119)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(973, 227)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(4, 119)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 227)
        Me.Panel3.TabIndex = 8
        '
        'LblCurrentBalance
        '
        Me.LblCurrentBalance.AutoSize = True
        Me.LblCurrentBalance.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrentBalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentBalance.Location = New System.Drawing.Point(379, 172)
        Me.LblCurrentBalance.Name = "LblCurrentBalance"
        Me.LblCurrentBalance.Size = New System.Drawing.Size(38, 14)
        Me.LblCurrentBalance.TabIndex = 3004
        Me.LblCurrentBalance.Text = "0.00"
        Me.LblCurrentBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtVoucherCategory
        '
        Me.TxtVoucherCategory.AgAllowUserToEnableMasterHelp = False
        Me.TxtVoucherCategory.AgLastValueTag = Nothing
        Me.TxtVoucherCategory.AgLastValueText = Nothing
        Me.TxtVoucherCategory.AgMandatory = False
        Me.TxtVoucherCategory.AgMasterHelp = True
        Me.TxtVoucherCategory.AgNumberLeftPlaces = 8
        Me.TxtVoucherCategory.AgNumberNegetiveAllow = False
        Me.TxtVoucherCategory.AgNumberRightPlaces = 2
        Me.TxtVoucherCategory.AgPickFromLastValue = False
        Me.TxtVoucherCategory.AgRowFilter = ""
        Me.TxtVoucherCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVoucherCategory.AgSelectedValue = Nothing
        Me.TxtVoucherCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVoucherCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVoucherCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVoucherCategory.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVoucherCategory.Location = New System.Drawing.Point(68, 31)
        Me.TxtVoucherCategory.MaxLength = 20
        Me.TxtVoucherCategory.Name = "TxtVoucherCategory"
        Me.TxtVoucherCategory.Size = New System.Drawing.Size(166, 16)
        Me.TxtVoucherCategory.TabIndex = 738
        Me.TxtVoucherCategory.Text = "VoucherCategory"
        Me.TxtVoucherCategory.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(261, 172)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 14)
        Me.Label3.TabIndex = 3005
        Me.Label3.Text = "Current Balance :"
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportGSTDataFromDos, Me.MnuImportGSTDataFromExcel, Me.MnuImportFromTally, Me.MnuImportFromDos, Me.MnuEditSave, Me.MnuCancelEntry, Me.MnuShowLedgerPosting, Me.MnuReport, Me.MnuPrintCheque, Me.MnuBankFormat})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(222, 268)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportGSTDataFromDos
        '
        Me.MnuImportGSTDataFromDos.Name = "MnuImportGSTDataFromDos"
        Me.MnuImportGSTDataFromDos.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportGSTDataFromDos.Text = "Import GST Data From Dos"
        '
        'MnuImportGSTDataFromExcel
        '
        Me.MnuImportGSTDataFromExcel.Name = "MnuImportGSTDataFromExcel"
        Me.MnuImportGSTDataFromExcel.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportGSTDataFromExcel.Text = "Import GST Data From Excel"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuEditSave
        '
        Me.MnuEditSave.Name = "MnuEditSave"
        Me.MnuEditSave.Size = New System.Drawing.Size(221, 22)
        Me.MnuEditSave.Text = "Edit & Save"
        '
        'MnuCancelEntry
        '
        Me.MnuCancelEntry.Name = "MnuCancelEntry"
        Me.MnuCancelEntry.Size = New System.Drawing.Size(221, 22)
        Me.MnuCancelEntry.Text = "Cancel Entry"
        '
        'MnuReport
        '
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(221, 22)
        Me.MnuReport.Text = "Report"
        '
        'MnuPrintCheque
        '
        Me.MnuPrintCheque.Name = "MnuPrintCheque"
        Me.MnuPrintCheque.Size = New System.Drawing.Size(221, 22)
        Me.MnuPrintCheque.Text = "Print Cheque"
        '
        'MnuBankFormat
        '
        Me.MnuBankFormat.Name = "MnuBankFormat"
        Me.MnuBankFormat.Size = New System.Drawing.Size(221, 22)
        Me.MnuBankFormat.Text = "Bank Format"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'PnlFifo
        '
        Me.PnlFifo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlFifo.Location = New System.Drawing.Point(4, 413)
        Me.PnlFifo.Name = "PnlFifo"
        Me.PnlFifo.Size = New System.Drawing.Size(641, 159)
        Me.PnlFifo.TabIndex = 3006
        '
        'TxtPartyDocNo
        '
        Me.TxtPartyDocNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyDocNo.AgLastValueTag = Nothing
        Me.TxtPartyDocNo.AgLastValueText = Nothing
        Me.TxtPartyDocNo.AgMandatory = False
        Me.TxtPartyDocNo.AgMasterHelp = False
        Me.TxtPartyDocNo.AgNumberLeftPlaces = 0
        Me.TxtPartyDocNo.AgNumberNegetiveAllow = False
        Me.TxtPartyDocNo.AgNumberRightPlaces = 0
        Me.TxtPartyDocNo.AgPickFromLastValue = False
        Me.TxtPartyDocNo.AgRowFilter = ""
        Me.TxtPartyDocNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyDocNo.AgSelectedValue = Nothing
        Me.TxtPartyDocNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyDocNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyDocNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyDocNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyDocNo.Location = New System.Drawing.Point(380, 63)
        Me.TxtPartyDocNo.MaxLength = 255
        Me.TxtPartyDocNo.Name = "TxtPartyDocNo"
        Me.TxtPartyDocNo.Size = New System.Drawing.Size(122, 16)
        Me.TxtPartyDocNo.TabIndex = 5
        '
        'LblPartyDocNo
        '
        Me.LblPartyDocNo.AutoSize = True
        Me.LblPartyDocNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyDocNo.Location = New System.Drawing.Point(258, 65)
        Me.LblPartyDocNo.Name = "LblPartyDocNo"
        Me.LblPartyDocNo.Size = New System.Drawing.Size(102, 14)
        Me.LblPartyDocNo.TabIndex = 740
        Me.LblPartyDocNo.Text = "Party Doc. No."
        '
        'TxtPartyDocDate
        '
        Me.TxtPartyDocDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyDocDate.AgLastValueTag = Nothing
        Me.TxtPartyDocDate.AgLastValueText = Nothing
        Me.TxtPartyDocDate.AgMandatory = False
        Me.TxtPartyDocDate.AgMasterHelp = False
        Me.TxtPartyDocDate.AgNumberLeftPlaces = 0
        Me.TxtPartyDocDate.AgNumberNegetiveAllow = False
        Me.TxtPartyDocDate.AgNumberRightPlaces = 0
        Me.TxtPartyDocDate.AgPickFromLastValue = False
        Me.TxtPartyDocDate.AgRowFilter = ""
        Me.TxtPartyDocDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyDocDate.AgSelectedValue = Nothing
        Me.TxtPartyDocDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyDocDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtPartyDocDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyDocDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyDocDate.Location = New System.Drawing.Point(630, 63)
        Me.TxtPartyDocDate.MaxLength = 255
        Me.TxtPartyDocDate.Name = "TxtPartyDocDate"
        Me.TxtPartyDocDate.Size = New System.Drawing.Size(163, 16)
        Me.TxtPartyDocDate.TabIndex = 6
        '
        'LblPartyDocDate
        '
        Me.LblPartyDocDate.AutoSize = True
        Me.LblPartyDocDate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyDocDate.Location = New System.Drawing.Point(508, 65)
        Me.LblPartyDocDate.Name = "LblPartyDocDate"
        Me.LblPartyDocDate.Size = New System.Drawing.Size(111, 14)
        Me.LblPartyDocDate.TabIndex = 742
        Me.LblPartyDocDate.Text = "Party Doc. Date"
        '
        'TxtBank
        '
        Me.TxtBank.AgAllowUserToEnableMasterHelp = False
        Me.TxtBank.AgLastValueTag = Nothing
        Me.TxtBank.AgLastValueText = Nothing
        Me.TxtBank.AgMandatory = False
        Me.TxtBank.AgMasterHelp = False
        Me.TxtBank.AgNumberLeftPlaces = 8
        Me.TxtBank.AgNumberNegetiveAllow = False
        Me.TxtBank.AgNumberRightPlaces = 2
        Me.TxtBank.AgPickFromLastValue = False
        Me.TxtBank.AgRowFilter = ""
        Me.TxtBank.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBank.AgSelectedValue = Nothing
        Me.TxtBank.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBank.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBank.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBank.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBank.Location = New System.Drawing.Point(382, 63)
        Me.TxtBank.MaxLength = 0
        Me.TxtBank.Name = "TxtBank"
        Me.TxtBank.Size = New System.Drawing.Size(412, 16)
        Me.TxtBank.TabIndex = 5
        Me.TxtBank.Visible = False
        '
        'LblBank
        '
        Me.LblBank.AutoSize = True
        Me.LblBank.BackColor = System.Drawing.Color.Transparent
        Me.LblBank.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBank.Location = New System.Drawing.Point(259, 63)
        Me.LblBank.Name = "LblBank"
        Me.LblBank.Size = New System.Drawing.Size(68, 14)
        Me.LblBank.TabIndex = 744
        Me.LblBank.Text = "Bank A/c"
        Me.LblBank.Visible = False
        '
        'TxtType
        '
        Me.TxtType.AgAllowUserToEnableMasterHelp = False
        Me.TxtType.AgLastValueTag = Nothing
        Me.TxtType.AgLastValueText = Nothing
        Me.TxtType.AgMandatory = True
        Me.TxtType.AgMasterHelp = False
        Me.TxtType.AgNumberLeftPlaces = 8
        Me.TxtType.AgNumberNegetiveAllow = False
        Me.TxtType.AgNumberRightPlaces = 2
        Me.TxtType.AgPickFromLastValue = False
        Me.TxtType.AgRowFilter = ""
        Me.TxtType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtType.AgSelectedValue = Nothing
        Me.TxtType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtType.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtType.Location = New System.Drawing.Point(380, 81)
        Me.TxtType.MaxLength = 0
        Me.TxtType.Name = "TxtType"
        Me.TxtType.Size = New System.Drawing.Size(414, 16)
        Me.TxtType.TabIndex = 6
        Me.TxtType.Visible = False
        '
        'LblType
        '
        Me.LblType.AutoSize = True
        Me.LblType.BackColor = System.Drawing.Color.Transparent
        Me.LblType.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblType.Location = New System.Drawing.Point(258, 82)
        Me.LblType.Name = "LblType"
        Me.LblType.Size = New System.Drawing.Size(39, 14)
        Me.LblType.TabIndex = 695
        Me.LblType.Text = "Type"
        Me.LblType.Visible = False
        '
        'BtnAttachments
        '
        Me.BtnAttachments.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAttachments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAttachments.Location = New System.Drawing.Point(803, 92)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(134, 23)
        Me.BtnAttachments.TabIndex = 3019
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Add Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'MnuShowLedgerPosting
        '
        Me.MnuShowLedgerPosting.Name = "MnuShowLedgerPosting"
        Me.MnuShowLedgerPosting.Size = New System.Drawing.Size(221, 22)
        Me.MnuShowLedgerPosting.Text = "Show Ledger Posting"
        '
        'FrmVoucherEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.PnlFifo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblCurrentBalance)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmVoucherEntry"
        Me.Text = "Sale Invoice"
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
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
        Me.Controls.SetChildIndex(Me.LblCurrentBalance, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.PnlFifo, 0)
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
        Me.Pnl1.ResumeLayout(False)
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents LblPartyName As System.Windows.Forms.Label
    Public WithEvents TxtPartyName As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents PnlTotals As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblCurrency As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtNature As AgControls.AgTextBox
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Protected WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents Panel2 As System.Windows.Forms.Panel

#End Region



    Private Sub ApplyUISetting(ItemType As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1ColumnCount As Integer
        Try


            For I = 1 To Dgl1.Columns.Count - 1
                Dgl1.Columns(I).Visible = False
            Next

            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='" & Me.Name & "' And NCat In ('" & ItemType & "') And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
                            Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            Dgl1.Columns(J).ReadOnly = Not CType(AgL.VNull(DtTemp.Rows(I)("IsEditable")), Boolean)
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1ColumnCount += 1
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                        End If
                    Next
                Next
            End If
            If mDgl1ColumnCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True

            If LblV_Type.Tag = Ncat.Payment Or LblV_Type.Tag = Ncat.Receipt Then
                LblPartyName.Text = "Cash/Bank A/c"
            Else
                LblPartyName.Text = "Party Name"
            End If

            If TxtV_Type.Tag = Ncat.ExpenseVoucher Then
                Dgl1.Columns(Col1HSN).Visible = True
            Else
                Dgl1.Columns(Col1HSN).Visible = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try

        'Dgl1.Columns(Col1TempAmountForTdsCalculation).Visible = True
        'Dgl1.Columns(Col1TdsCategory).Visible = True
        'Dgl1.Columns(Col1TdsGroup).Visible = True
        'Dgl1.Columns(Col1TdsLedgerAccount).Visible = True
        'Dgl1.Columns(Col1TdsMonthlyLimit).Visible = True
        'Dgl1.Columns(Col1TdsYearlyLimit).Visible = True
        'Dgl1.Columns(Col1PartyMonthTransaction).Visible = True
        'Dgl1.Columns(Col1PartyYearTransaction).Visible = True
        'Dgl1.Columns(Col1TdsTaxableAmount).Visible = True
        'Dgl1.Columns(Col1TdsPer).Visible = True
        'Dgl1.Columns(Col1TdsAmount).Visible = True
    End Sub


    Private Sub FrmLedgerHead_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim DsTemp As DataTable

        mQry = " Delete From LedgerAdj Where Vr_DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If AgL.PubServerName = "" Then
            mQry = "select group_concat(DocID,',') from LedgerHeadDetail with (Nolock) where ReferencedocId='" & SearchCode & "' group by ReferenceDocId"
        Else
            mQry = "select DocID + ',' from LedgerHeadDetail With (NoLock) where ReferencedocId='" & SearchCode & "' group by ReferenceDocId, DocID for xml path('')"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        mQry = "Delete From Ledger Where ReferenceDocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerHeadDetail Where ReferenceDocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If DsTemp.Rows.Count > 0 Then
            mQry = "Delete From LedgerHead Where DocID in ('" & Replace(DsTemp.Rows(0)(0), ",", "','") & "')"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete From LedgerM Where DocID In ('" & Replace(DsTemp.Rows(0)(0), ",", "','") & "')"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "Delete From TransactionReferences Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SchemeQulified Where GeneratedDocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerHeadDetailTds Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "LedgerHead"
        MainLineTableCsv = "LedgerHeadDetail,LedgerHeadCharges,LedgerHeadDetailCharges,Ledger"



        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False

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

        'If Want Then To Edit Save Record which has Not Ledger Posted
        'mCondStr = mCondStr & " And H.DocId In (SELECT H.DocID FROM LedgerHead H LEFT JOIN Ledger L ON H.DocID = L.DocId WHERE L.DocId IS NULL) "

        Dim mIsShowOnlySelfRecords As Boolean = False
        If AgL.VNull(AgL.Dman_Execute("Select IfNull(IsShowOnlySelfRecords,0) From UserMast With (NoLock) 
                Where USER_NAME = '" & AgL.PubUserName & "'", AgL.GCn).ExecuteScalar()) = 1 Then
            mIsShowOnlySelfRecords = True
        End If

        If mIsShowOnlySelfRecords = True And AgL.StrCmp(AgL.PubUserName, "Super") = False Then
            mCondStr = mCondStr & " And H.EntryBy = '" & AgL.PubUserName & "'"
        End If


        mQry = "Select DocID As SearchCode " &
                " From LedgerHead H  With (NoLock) " &
                " Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  " &
                " Where 1 = 1  " & mCondStr & "  Order By V_Date , V_No    "


        'mQry = "Select H.DocID As SearchCode " &
        '        " From LedgerHead H  With (NoLock) " &
        '        " Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  
        '        LEFT JOIN Ledger L With (NoLock) On H.DocId = L.DocId " &
        '        " Where L.DocId Is Null  " & mCondStr & "  Order By H.V_Date , H.V_No    "

        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        Dim mIsShowOnlySelfRecords As Boolean = False
        If AgL.VNull(AgL.Dman_Execute("Select IfNull(IsShowOnlySelfRecords,0) From UserMast With (NoLock) 
                Where USER_NAME = '" & AgL.PubUserName & "'", AgL.GCn).ExecuteScalar()) = 1 Then
            mIsShowOnlySelfRecords = True
        End If

        If mIsShowOnlySelfRecords = True And AgL.StrCmp(AgL.PubUserName, "Super") = False Then
            mCondStr = mCondStr & " And H.EntryBy = '" & AgL.PubUserName & "'"
        End If

        mCondStr = mCondStr & " Order By Cast(H.ManualRefNo as BigInt)"


        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Entry_Type], H.V_Date AS Date, SGV.Name AS [Account], LSGV.Name as [Party Name], L.Amount,
                            H.ManualRefNo AS [Entry_No], H.SalesTaxGroupParty AS [Sales_Tax_Group_Party], 
                            H.Remarks,  
                            H.EntryBy As [Entry_By], H.EntryDate As [Entry_Date] 
                            FROM LedgerHead H  With (NoLock) 
                            Left Join LedgerHeadDetail L With (NoLock) On H.DocId = L.DocID
                            LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type 
                            LEFT JOIN SubGroup SGV With (NoLock) On SGV.SubCode  = H.Subcode 
                            Left Join Subgroup LSGV With (NoLock) On L.Subcode = LSGV.Subcode
                            Where 1=1 " & mCondStr
        AgL.PubFindQry = AgL.GetBackendBasedQuery(AgL.PubFindQry)
        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgDateColumn(Dgl1, Col1EffectiveDate, 115, Col1EffectiveDate, True, False)
            .AddAgTextColumn(Dgl1, Col1Subcode, 400, 0, Col1Subcode, True, False)
            .AddAgTextColumn(Dgl1, Col1LinkedSubcode, 250, 0, Col1LinkedSubcode, False, False)
            .AddAgTextColumn(Dgl1, Col1Barcode, 130, 0, Col1Barcode, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 130, 0, Col1Specification, True, False)
            .AddAgNumberColumn(Dgl1, Col1HSN, 80, 8, 0, False, Col1HSN, False, False, True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, False, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, False, True)
            .AddAgTextColumn(Dgl1, Col1ChqRefNo, 100, 255, Col1ChqRefNo, True, False)
            .AddAgDateColumn(Dgl1, Col1ChqRefDate, 100, Col1ChqRefDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1Deduction, 100, 8, 2, False, Col1Deduction, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1OtherCharges, 100, 8, 2, False, Col1OtherCharges, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1ReconcileDate, 150, 255, Col1ReconcileDate, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceNo, 40, 5, Col1ReferenceNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1SpecificationDocId, 40, 5, Col1SpecificationDocId, False, True, False)
            .AddAgTextColumn(Dgl1, Col1SpecificationDocIdSr, 40, 5, Col1SpecificationDocIdSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1CurrentBalance, 150, 255, Col1CurrentBalance, False, False)
            .AddAgTextColumn(Dgl1, Col1IsRecordLocked, 150, 255, Col1IsRecordLocked, False, False)
            .AddAgTextColumn(Dgl1, Col1AmountInWords, 150, 255, Col1AmountInWords, False, False)
            .AddAgTextColumn(Dgl1, Col1Nature, 150, 255, Col1Nature, False, False)
            .AddAgTextColumn(Dgl1, Col1SubgroupType, 150, 255, Col1SubgroupType, False, False)
            .AddAgTextColumn(Dgl1, Col1TempSno, 40, 5, Col1TempSno, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1TempAmountForTdsCalculation, 100, 8, 2, False, Col1TempAmountForTdsCalculation, True, False, True)
            .AddAgTextColumn(Dgl1, Col1TdsCategory, 40, 5, Col1TdsCategory, False, True, False)
            .AddAgTextColumn(Dgl1, Col1TdsGroup, 40, 5, Col1TdsGroup, False, True, False)
            .AddAgTextColumn(Dgl1, Col1TdsLedgerAccount, 40, 5, Col1TdsLedgerAccount, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1TdsMonthlyLimit, 100, 8, 2, False, Col1TdsMonthlyLimit, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1TdsYearlyLimit, 100, 8, 2, False, Col1TdsYearlyLimit, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1PartyMonthTransaction, 100, 8, 2, False, Col1PartyMonthTransaction, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1PartyYearTransaction, 100, 8, 2, False, Col1PartyYearTransaction, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1TdsTaxableAmount, 100, 8, 2, False, Col1TdsTaxableAmount, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1TdsPer, 100, 8, 2, False, Col1TdsPer, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1TdsAmount, 100, 8, 2, False, Col1TdsAmount, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl1)
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Dgl1.Anchor = Pnl1.Anchor

        If LblV_Type.Tag <> "" Then
            ApplyUISetting(LblV_Type.Tag)
        Else
            ApplyUISetting(EntryNCat)
        End If


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 30, 5, ColSNo, True, True, False)
            .AddAgDateColumn(Dgl2, Col2DocDate, 75, Col2DocDate, True, True)
            .AddAgTextColumn(Dgl2, Col2DocNo, 90, 0, Col2DocNo, True, True)
            .AddAgTextColumn(Dgl2, Col2Narration, 100, 0, Col2Narration, True, True)
            .AddAgNumberColumn(Dgl2, Col2DocAmount, 90, 8, 2, False, Col2DocAmount, True, True, True)
            .AddAgNumberColumn(Dgl2, Col2BalAmount, 90, 8, 2, False, Col2BalAmount, True, True, True)
            .AddAgNumberColumn(Dgl2, Col2RunningBalance, 90, 8, 2, False, Col2RunningBalance, True, True, True)
            .AddAgTextColumn(Dgl2, Col2DrCr, 40, 0, Col2DrCr, True, True)
        End With
        AgL.AddAgDataGrid(Dgl2, PnlFifo)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 25
        AgL.GridDesign(Dgl2)
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AgAllowFind = False
        Dgl2.AllowUserToOrderColumns = True
        Dgl2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Dgl2.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Bold)
        Dgl2.DefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Regular)
        Dgl2.Anchor = PnlFifo.Anchor


        AgCalcGrid1.Ini_Grid(EntryNCat, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Subcode).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingPartyAc = TxtPartyName.AgSelectedValue
        AgCalcGrid1.Anchor = PnlCalcGrid.Anchor

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False



        AgCalcGrid1.Name = "AgCalcGrid1"
        AgCustomGrid1.Name = "AgCustomGrid1"



        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)

        'For I As Integer = 0 To Dgl1.Columns.Count - 1
        '    Dgl1.Columns(I).Visible = True
        'Next
        'LblPartyName.Text = AgL.XNull(DtV_TypeSettings.Rows(0)("Caption_SubcodeHead"))
        If Not TxtV_Type.Tag = Ncat.ExpenseVoucher Then
            LblPartyDocNo.Visible = False
            TxtPartyDocNo.Visible = False
            LblPartyDocDate.Visible = False
            TxtPartyDocDate.Visible = False
        Else
            LblPartyDocNo.Visible = True
            TxtPartyDocNo.Visible = True
            LblPartyDocDate.Visible = True
            TxtPartyDocDate.Visible = True
        End If

        TxtType.Visible = IsTypeVisible()
        LblType.Visible = TxtType.Visible
    End Sub


    Function GetOutstandingBillsFifoList(strSubcode As String, strUptoDate As String) As List(Of OutstandingBill)
        Dim DtMain As DataTable
        Dim mBalance As Double
        Dim mRemainingBalance As Double
        Dim i As Integer
        Dim OutstandingBills As New List(Of OutstandingBill)
        Dim objOutstandingBill As OutstandingBill


        'mQry = "Select IfNull(Sum(IfNull(L.AmtDr,0))- Sum(IfNull(L.AmtCr,0)),0) From Ledger L  With (NoLock) Where L.DivCode='" & TxtDivision.Tag & "' And L.Site_Code = '" & TxtSite_Code.Tag & "' And L.Subcode = '" & strSubcode & "' And L.V_Date <= '" & CDate(strUptoDate).ToString("s") & "'"
        mQry = "Select IfNull(Sum(IfNull(L.AmtDr,0))- Sum(IfNull(L.AmtCr,0)),0) From Ledger L  With (NoLock) Where L.DivCode='" & TxtDivision.Tag & "' And L.Site_Code = '" & TxtSite_Code.Tag & "' And L.Subcode = '" & strSubcode & "'"
        mBalance = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar
        mRemainingBalance = Math.Abs(mBalance)
        If mBalance > 0 Then
            'mQry = "Select L.DocID, L.DivCode, L.Site_Code, L.V_Type, Vt.Description as V_TypeDesc, L.RecId, L.V_Date, L.Narration, L.AmtDr as Amount,
            '        L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo
            '        From Ledger L  With (NoLock)
            '        Left Join Voucher_Type Vt  With (NoLock) On L.V_Type = Vt.V_Type
            '        Where L.DivCode='" & TxtDivision.Tag & "' And L.Site_Code = '" & TxtSite_Code.Tag & "' 
            '        And L.Subcode = '" & strSubcode & "' And L.V_Date <= '" & CDate(strUptoDate).ToString("s") & "' And L.AmtDr > 0 
            '        Order By L.V_Date Desc, L.RecId desc"
            mQry = "Select L.DocID, L.DivCode, L.Site_Code, L.V_Type, Vt.Description as V_TypeDesc, L.RecId, IfNull(L.EffectiveDate,L.V_Date) as V_Date, L.Narration, L.AmtDr as Amount,
                    L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo
                    From Ledger L  With (NoLock)
                    Left Join Voucher_Type Vt  With (NoLock) On L.V_Type = Vt.V_Type
                    Where L.DivCode='" & TxtDivision.Tag & "' And L.Site_Code = '" & TxtSite_Code.Tag & "' 
                    And L.Subcode = '" & strSubcode & "'  And L.AmtDr > 0 
                    Order By IfNull(L.EffectiveDate,L.V_Date) Desc, L.RecId desc"

        Else
            'mQry = "Select L.DocID, L.DivCode, L.Site_Code, L.V_Type, Vt.Description as V_TypeDesc, L.RecId, L.V_Date, L.Narration, L.AmtCr as Amount,
            '        L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo
            '        From Ledger L  With (NoLock)
            '        Left Join Voucher_Type Vt On L.V_Type = Vt.V_Type
            '        Where L.DivCode='" & TxtDivision.Tag & "' And L.Site_Code = '" & TxtSite_Code.Tag & "' 
            '        And L.Subcode = '" & strSubcode & "' And L.V_Date <= '" & CDate(strUptoDate).ToString("s") & "' And L.AmtCr > 0 
            '        Order By L.V_Date Desc, L.RecId desc"
            mQry = "Select L.DocID, L.DivCode, L.Site_Code, L.V_Type, Vt.Description as V_TypeDesc, L.RecId, IfNull(L.EffectiveDate,L.V_Date) V_Date, L.Narration, L.AmtCr as Amount,
                    L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo
                    From Ledger L  With (NoLock)
                    Left Join Voucher_Type Vt On L.V_Type = Vt.V_Type
                    Where L.DivCode='" & TxtDivision.Tag & "' And L.Site_Code = '" & TxtSite_Code.Tag & "' 
                    And L.Subcode = '" & strSubcode & "'  And L.AmtCr > 0 
                    Order By IfNull(L.EffectiveDate,L.V_Date) Desc, L.RecId desc"

        End If
        DtMain = AgL.FillData(mQry, AgL.GCn).tables(0)
        Dgl2.Rows.Clear()
        If DtMain.Rows.Count > 0 Then
            For i = 0 To DtMain.Rows.Count - 1
                If mRemainingBalance > 0 Then

                    objOutstandingBill = New OutstandingBill
                    objOutstandingBill.DocNo = AgL.XNull(DtMain.Rows(i)("DocNo"))
                    objOutstandingBill.DocDate = AgL.XNull(DtMain.Rows(i)("V_Date"))
                    objOutstandingBill.Narration = IIf(AgL.XNull(DtMain.Rows(i)("Narration")) = "", AgL.XNull(DtMain.Rows(i)("V_TypeDesc")), AgL.XNull(DtMain.Rows(i)("Narration")))
                    objOutstandingBill.DocAmount = AgL.VNull(DtMain.Rows(i)("Amount"))
                    If mRemainingBalance > AgL.VNull(DtMain.Rows(i)("Amount")) Then
                        objOutstandingBill.BalAmount = Format(AgL.VNull(DtMain.Rows(i)("Amount")), "0.00")
                        mRemainingBalance = mRemainingBalance - AgL.VNull(DtMain.Rows(i)("Amount"))
                    Else
                        objOutstandingBill.BalAmount = Format(mRemainingBalance, "0.00")
                        mRemainingBalance = mRemainingBalance - mRemainingBalance
                    End If
                    objOutstandingBill.DrCr = IIf(mBalance > 0, "Dr", "Cr")

                    OutstandingBills.Add(objOutstandingBill)


                    'Dgl2.Rows.Add()
                    'Dgl2.Item(ColSNo, i).Value = Dgl2.Rows.Count - 1
                    'Dgl2.Item(Col2DocNo, i).Value = AgL.XNull(DtMain.Rows(i)("DocNo"))
                    'Dgl2.Item(Col2DocDate, i).Value = Format(CDate(AgL.XNull(DtMain.Rows(i)("V_Date"))), "dd-MMM-yy")
                    'Dgl2.Item(Col2Narration, i).Value = IIf(AgL.XNull(DtMain.Rows(i)("Narration")) = "", AgL.XNull(DtMain.Rows(i)("V_TypeDesc")), AgL.XNull(DtMain.Rows(i)("Narration")))
                    'Dgl2.Item(Col2DocAmount, i).Value = Format(AgL.VNull(DtMain.Rows(i)("Amount")), "0.00")
                    'If mRemainingBalance > AgL.VNull(DtMain.Rows(i)("Amount")) Then
                    '    Dgl2.Item(Col2BalAmount, i).Value = Format(AgL.VNull(DtMain.Rows(i)("Amount")), "0.00")
                    '    mRemainingBalance = mRemainingBalance - AgL.VNull(DtMain.Rows(i)("Amount"))
                    'Else
                    '    Dgl2.Item(Col2BalAmount, i).Value = Format(mRemainingBalance, "0.00")
                    '    mRemainingBalance = mRemainingBalance - mRemainingBalance
                    'End If
                    'Dgl2.Item(Col2DrCr, i).Value = IIf(mBalance > 0, "Dr", "Cr")
                End If
            Next
        End If
        GetOutstandingBillsFifoList = OutstandingBills
    End Function

    Sub FillOutstandingGrid(OutstandingBills As List(Of OutstandingBill))
        Dim i As Integer
        Dim mRow As Integer
        Dim objOutstandingBill As New OutstandingBill
        Dim mRunningTotal As Double = 0
        Dgl2.Rows.Clear()
        If OutstandingBills.Count > 0 Then
            For i = OutstandingBills.Count - 1 To 0 Step -1
                objOutstandingBill = OutstandingBills(i)
                mRow = Dgl2.Rows.Count - 1
                Dgl2.Rows.Add()
                Dgl2.Item(ColSNo, mRow).Value = Dgl2.Rows.Count - 1
                Dgl2.Item(Col2DocNo, mRow).Value = AgL.XNull(objOutstandingBill.DocNo)
                Dgl2.Item(Col2DocDate, mRow).Value = ClsMain.FormatDate(AgL.XNull(objOutstandingBill.DocDate))
                Dgl2.Item(Col2Narration, mRow).Value = objOutstandingBill.Narration
                Dgl2.Item(Col2DocAmount, mRow).Value = Format(objOutstandingBill.DocAmount, "0.00")
                Dgl2.Item(Col2BalAmount, mRow).Value = Format(objOutstandingBill.BalAmount, "0.00")
                mRunningTotal = mRunningTotal + objOutstandingBill.BalAmount
                Dgl2.Item(Col2RunningBalance, mRow).Value = Format(mRunningTotal, "0.00")
                Dgl2.Item(Col2DrCr, mRow).Value = objOutstandingBill.DrCr
            Next
        End If
        Dgl2.Visible = True
    End Sub

    Private Sub HandlePendingLR(Transporter As String)
        mQry = "Select SubgroupType From Subgroup Where Subcode = '" & Transporter & "'"
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar).ToString.ToUpper = SubgroupType.Transporter.ToUpper Then
            mQry = " 
                Select Count(*) 
                From Barcode B
                Left Join LedgerHeadDetail L On B.Code = L.Barcode
                Where GenSubcode='" & Transporter & "' and L.DocId Is Null 
                "
            If AgL.Dman_Execute(mQry, AgL.GCn).executeScalar > 0 Then


                Dim StrTicked As String
                StrTicked = FHPGD_PendingLR(Transporter)
                If StrTicked <> "" Then
                    FillGridForLR(StrTicked)
                End If

            End If
        End If

    End Sub

    Private Function FHPGD_PendingLR(Transporter As String) As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable


        mQry = "
                Select 'o' As Tick, B.Code as SearchKey, B.Specification1 as LRNo, B.Specification2 as LRDate, B.Specification5 as Freight                  
                From Barcode B
                Left Join LedgerHeadDetail L On B.Code = L.Barcode
                Where GenSubcode='" & Transporter & "' and L.DocId Is Null 
                "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            Exit Function
        End If

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 820, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "L.R. No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "L.R. Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Freight", 320, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingLR = StrRtn

        FRH_Multiple = Nothing
    End Function


    Private Sub FillGridForLR(strBarcodes As String)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim mRow As Integer
        Dim I As Integer
        Try


            mQry = "
                Select  B.Code, B.Description, B.Specification1 as LRNo, B.Specification2 as LRDate, B.Specification5 as Freight, Sg.SubCode as ExpenseAc, Sg.Name as ExpenseAcName                  
                From Barcode B
                Left Join Item I On B.Item = I.Code
                Left Join Subgroup Sg On I.Subcode = Sg.Subcode
                Left Join LedgerHeadDetail L On B.Code = L.Barcode
                Where B.Code In (" & strBarcodes & ") 
                "


            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                'Dgl1.Rows(Dgl1.CurrentCell.RowIndex).Visible = False
                Dgl1.CurrentCell = Dgl1(Col1Subcode, Dgl1.Rows.Count - 1)
                For I = 0 To DtTemp.Rows.Count - 1
                    If I = 0 Then
                        If Dgl1.CurrentCell IsNot Nothing Then
                            If Dgl1(Col1Subcode, Dgl1.CurrentCell.RowIndex).Value = "" Then
                                mRow = Dgl1.CurrentCell.RowIndex
                            Else
                                mRow = Dgl1.Rows.Add()
                            End If
                        Else
                            mRow = 0
                        End If
                        Dgl1.Item(ColSNo, mRow).Value = mRow
                    Else
                        mRow = Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, mRow).Value = Dgl1.Rows.Count - 1
                    End If

                    Dgl1.Item(Col1Barcode, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("Code"))
                    Dgl1.Item(Col1Barcode, mRow).Value = AgL.XNull(DtTemp.Rows(I)("Description"))
                    Dgl1.Item(Col1Subcode, mRow).Tag = AgL.XNull(DtTemp.Rows(I)("ExpenseAc"))
                    Dgl1.Item(Col1Subcode, mRow).Value = AgL.XNull(DtTemp.Rows(I)("ExpenseAcName"))
                    Dgl1.Item(Col1Amount, mRow).Value = AgL.XNull(DtTemp.Rows(I)("Freight"))
                Next

                Calculation()
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On FillGridForLR Function ")
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bStockSelectionQry$ = ""
        Dim bChargesSelectionQry$ = ""
        Dim mMultiplyWithMinus As Boolean = False

        If (LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.DebitNoteSupplier And TxtVoucherCategory.Text.ToUpper = AgLibrary.ClsMain.agConstants.VoucherCategory.Purchase) Or
                (LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.CreditNoteCustomer And TxtVoucherCategory.Text.ToUpper = AgLibrary.ClsMain.agConstants.VoucherCategory.Sales) Then
            mMultiplyWithMinus = True
        End If


        If Topctrl1.Mode.ToUpper = "EDIT" Then
            mQry = "Delete from Ledger where docId='" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = " Update LedgerHead " &
                    " SET  " &
                    " ManualRefNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                    " Subcode = " & AgL.Chk_Text(TxtPartyName.Tag) & ", " &
                    " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                    " PartyDocNo = " & AgL.Chk_Text(TxtPartyDocNo.Text) & ", " &
                    " PartyDocDate = " & AgL.Chk_Date(TxtPartyDocDate.Text) & ", " &
                    " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                    " BankAc = " & AgL.Chk_Text(TxtBank.Tag) & ", " &
                    " Type = " & AgL.Chk_Text(TxtType.Text) & ", " &
                    " UploadDate = Null, " &
                    " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) &
                    " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                    " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        If UCase(Topctrl1.Mode) = "ADD" Then
            mQry = "Insert Into LedgerHeadCharges(DocID) Values('" & mSearchCode & "') "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If TxtStructure.Tag <> "" Then
            mQry = "Update LedgerHeadCharges Set " & AgCalcGrid1.FFooterTableUpdateStr(mMultiplyWithMinus) & " Where DocID ='" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Delete from LedgerHeadDetailCharges Where DocID = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If




        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From LedgerHeadDetail  With (NoLock)  Where DocID = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If IIf(LblV_Type.Tag = Ncat.CreditNoteCustomer Or LblV_Type.Tag = Ncat.DebitNoteCustomer Or LblV_Type.Tag = Ncat.DebitNoteSupplier Or LblV_Type.Tag = Ncat.CreditNoteSupplier, Dgl1.Item(Col1Remark, I).Value, Dgl1.Item(Col1Subcode, I).Value) <> "" Then
                If mMultiplyWithMinus Then
                    Dgl1.Item(Col1Qty, I).Value = -Math.Abs(Val(Dgl1.Item(Col1Qty, I).Value))
                    Dgl1.Item(Col1Amount, I).Value = -Math.Abs(Val(Dgl1.Item(Col1Amount, I).Value))
                End If

                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1

                    Dgl1.Item(Col1TempSno, I).Value = mSr

                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, I).Tag) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1HSN, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Value) & ", " &
                                            " " & AgL.Chk_Date(Dgl1.Item(Col1ChqRefDate, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1AmountInWords, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1SpecificationDocId, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1SpecificationDocIdSr, I).Value) & ", " &
                                            " " & AgL.Chk_Date(Dgl1.Item(Col1EffectiveDate, I).Value) & ""



                    If bChargesSelectionQry <> "" Then bChargesSelectionQry += " UNION ALL "
                    bChargesSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & Val(Dgl1.Item(Col1Deduction, I).Value) & " As Deduction, " & Val(Dgl1.Item(Col1OtherCharges, I).Value) & " as Other_Charge "
                    'If TxtStructure.Tag <> "" Then
                    '    If bChargesSelectionQry <> "" Then bChargesSelectionQry += " UNION ALL "
                    '    bChargesSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & AgCalcGrid1.FLineTableFieldValuesStr(I, mMultiplyWithMinus)
                    'End If

                    If Val(Dgl1.Item(Col1TdsAmount, I).Value) > 0 Then
                        mQry = "INSERT INTO LedgerHeadDetailTds (DocID, Sr, TdsCategory, TdsGroup, TdsLedgerAccount, 
                                    TdsMonthlyLimit, TdsYearlyLimit, PartyMonthTransaction, PartyYearTransaction, 
                                    TdsTaxableAmount, TdsPer, TdsAmount)
                                    VALUES ('" & mSearchCode & "'," & Val(mSr) & ", 
                                    " & AgL.Chk_Text(Dgl1.Item(Col1TdsCategory, I).Tag) & ", 
                                    " & AgL.Chk_Text(Dgl1.Item(Col1TdsGroup, I).Tag) & ", 
                                    " & AgL.Chk_Text(Dgl1.Item(Col1TdsLedgerAccount, I).Tag) & ", 
                                    " & Val(Dgl1.Item(Col1TdsMonthlyLimit, I).Value) & ", 
                                    " & Val(Dgl1.Item(Col1TdsYearlyLimit, I).Value) & ", 
                                    " & Val(Dgl1.Item(Col1PartyMonthTransaction, I).Value) & ", 
                                    " & Val(Dgl1.Item(Col1PartyYearTransaction, I).Value) & ", 
                                    " & Val(Dgl1.Item(Col1TdsTaxableAmount, I).Value) & ", 
                                    " & Val(Dgl1.Item(Col1TdsPer, I).Value) & ", 
                                    " & Val(Dgl1.Item(Col1TdsAmount, I).Value) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        Dgl1.Item(Col1TempSno, I).Value = Dgl1.Item(ColSNo, I).Tag
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                            mQry = " UPDATE LedgerHeadDetail " &
                                        " Set " &
                                        " Subcode = " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                        " LinkedSubcode = " & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                                        " Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, I).Tag) & ", " &
                                        " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                        " HSN = " & AgL.Chk_Text(Dgl1.Item(Col1HSN, I).Value) & ", " &
                                        " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                        " Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                        " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                        " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                        " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                        " ChqRefNo = " & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Value) & ", " &
                                        " ChqRefdate = " & AgL.Chk_Date(Dgl1.Item(Col1ChqRefDate, I).Value) & ", " &
                                        " Remarks = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                        " AmountInWords = " & AgL.Chk_Text(Dgl1.Item(Col1AmountInWords, I).Value) & ", " &
                                        " ReferenceNo = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, I).Value) & ", " &
                                        " SpecificationDocId = " & AgL.Chk_Text(Dgl1.Item(Col1SpecificationDocId, I).Value) & ", " &
                                        " SpecificationDocIdSr = " & AgL.Chk_Text(Dgl1.Item(Col1SpecificationDocIdSr, I).Value) & ", " &
                                        " UploadDate = Null, " &
                                        " EffectiveDate = " & AgL.Chk_Date(Dgl1.Item(Col1EffectiveDate, I).Value) & " " &
                                        " Where DocId = '" & mSearchCode & "' " &
                                        " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If

                        If TxtStructure.Tag <> "" Then
                            mQry = "Insert Into LedgerHeadDetailCharges(DocID, Sr, Deduction, Other_Charge) 
                                   Values ('" & mSearchCode & "'," & Val(Dgl1.Item(ColSNo, I).Tag) & ", " & Val(Dgl1.Item(Col1Deduction, I).Value) & ", " & Val(Dgl1.Item(Col1OtherCharges, I).Value) & ")"
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If

                        mQry = "UPDATE LedgerHeadDetailTds
                                SET TdsCategory = " & AgL.Chk_Text(Dgl1.Item(Col1TdsCategory, I).Tag) & ",
	                            TdsGroup = " & AgL.Chk_Text(Dgl1.Item(Col1TdsGroup, I).Tag) & ",
	                            TdsLedgerAccount = " & AgL.Chk_Text(Dgl1.Item(Col1TdsLedgerAccount, I).Tag) & ",
	                            TdsMonthlyLimit = " & Val(Dgl1.Item(Col1TdsMonthlyLimit, I).Value) & ",
	                            TdsYearlyLimit = " & Val(Dgl1.Item(Col1TdsYearlyLimit, I).Value) & ",
	                            PartyMonthTransaction = " & Val(Dgl1.Item(Col1PartyMonthTransaction, I).Value) & ",
	                            PartyYearTransaction = " & Val(Dgl1.Item(Col1PartyYearTransaction, I).Value) & ",
	                            TdsTaxableAmount = " & Val(Dgl1.Item(Col1TdsTaxableAmount, I).Value) & ",
	                            TdsPer = " & Val(Dgl1.Item(Col1TdsPer, I).Value) & ",
	                            TdsAmount = " & Val(Dgl1.Item(Col1TdsAmount, I).Value) & "
                                Where DocId = '" & mSearchCode & "'
                                And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        Dim DtDocID As DataTable
                        mQry = "Select DocID From LedgerHeadDetail With (Nolock) Where ReferenceDocID = '" & mSearchCode & "' And ReferenceDocIdSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & ""
                        DtDocID = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

                        mQry = " Delete From LedgerHeadDetailTds Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = " Delete From LedgerHeadDetailCharges Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = " Delete From LedgerHeadDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Delete From Ledger Where ReferenceDocID = '" & SearchCode & "' And ReferenceDocIdSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Delete From TransactionReferences Where DocID = '" & SearchCode & "' And DocIDSr=" & Val(Dgl1.Item(ColSNo, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Delete From LedgerHeadDetail Where ReferenceDocID = '" & SearchCode & "' And ReferenceDocIdSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        If DtDocID.Rows.Count > 0 Then
                            mQry = "Delete From LedgerHead Where DocID = '" & DtDocID.Rows(0)(0) & "' "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            mQry = "Delete From LedgerM Where DocID = '" & DtDocID.Rows(0)(0) & "' "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                End If

                If Dgl1.Rows(I).Visible = True Then
                    If Dgl1.Item(Col1Amount, I).Tag IsNot Nothing Then
                        CType(Dgl1.Item(Col1Amount, I).Tag, FrmVoucherEntryCash).FSave(mSearchCode, IIf(Val(Dgl1.Item(ColSNo, I).Tag) = 0, mSr, Val(Dgl1.Item(ColSNo, I).Tag)), Conn, Cmd)
                    End If
                End If
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = "Insert Into LedgerHeadDetail(DocId, Sr, Subcode, LinkedSubcode, Barcode, Specification, HSN, SalesTaxGroupItem, " &
                       " Qty, Unit, Rate, Amount, ChqRefNo, ChqRefDate, Remarks, AmountInWords, " &
                       " ReferenceNo, SpecificationDocId, SpecificationDocIdSr, EffectiveDate) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            'If TxtStructure.Tag <> "" Then
            mQry = "Insert Into LedgerHeadDetailCharges(DocID, Sr, Deduction, Other_Charge) " & bChargesSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            'End If


        End If






        Dim mNarr As String = ""
        Dim mNarrParty As String = ""



        'If TxtStructure.Tag <> "" Then
        '    Call PostStructureLineToAccounts(AgCalcGrid1, mNarrParty, mNarr, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue,
        '                       TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtReferenceNo.Text, TxtPartyName.AgSelectedValue, TxtV_Date.Text, Conn, Cmd,, mMultiplyWithMinus)
        'End If
        PostGridToAccounts(mSearchCode, mMultiplyWithMinus, Conn, Cmd)

        FLedgerPostTds(Conn, Cmd)

        If LblV_Type.Tag = Ncat.Receipt Or LblV_Type.Tag = "VR" Then
            mQry = "Delete From LedgerAdj Where Vr_DocID = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Select * From Ledger With (NoLock) Where DocId = '" & mSearchCode & "' And ReferenceDocId Is Not Null And IfNull(AmtCr,0) > 0 "
            Dim DtAdj As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

            For I = 0 To DtAdj.Rows.Count - 1
                If AgL.XNull(DtAdj.Rows(I)("ReferenceDocId")) <> "" Then
                    mQry = "Insert Into LedgerAdj(Vr_DocID, Vr_V_SNo, Adj_DocID, Adj_V_SNo, Amount, Site_Code, Div_Code, Adj_Type,ReferenceDocID)
                        Values (" & AgL.Chk_Text(DtAdj.Rows(I)("DocId")) & ",
                        " & AgL.Chk_Text(DtAdj.Rows(I)("V_SNo")) & ", 
                        " & AgL.Chk_Text(DtAdj.Rows(I)("ReferenceDocId")) & ", 
                        " & AgL.Chk_Text(DtAdj.Rows(I)("ReferenceDocIdSr")) & ", 
                        " & Val(DtAdj.Rows(I)("AmtCr")) & ", 
                        " & AgL.Chk_Text(DtAdj.Rows(I)("Site_Code")) & ", 
                        " & AgL.Chk_Text(DtAdj.Rows(I)("DivCode")) & ", 'Adjustment', 
                        " & AgL.Chk_Text(mSearchCode) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End If

        FPostEntryForBranch(mSearchCode, Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub
    Sub PostGridToAccounts(DocID As String, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
        Dim mLedgerPostingData As String = ""
        Dim I As Integer
        Dim mSr As Integer
        Dim mHeaderAccountDrCr As String
        Dim DtTemp As DataTable
        Dim mNarration As String = ""

        Dim mPostingAcDeduction As String = FGetSettings(SettingFields.PostingAcDeductions, SettingType.General)
        Dim mPostingAcOtherCharges As String = FGetSettings(SettingFields.PostingAcOtherCharges, SettingType.General)

        mQry = "Select HeaderAccountDrCr From Voucher_Type with (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'"
        mHeaderAccountDrCr = AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).executeScalar

        If mHeaderAccountDrCr.ToUpper <> "DR" And mHeaderAccountDrCr.ToUpper <> "CR" Then Exit Sub

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Rows(I).Visible = True And Val(Dgl1(Col1Amount, I).Value) <> 0 And Dgl1.Item(Col1Amount, I).Style.ForeColor <> Color.Blue Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mNarration = TxtPartyName.Text
                If LblV_Type.Tag = Ncat.Receipt And TxtBank.Text <> "" Then
                    mNarration = mNarration & " deposited cash to " & TxtBank.Text & ". "
                End If
                mNarration = mNarration & Dgl1(Col1Remark, I).Value

                mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1TempSno, I).Value) & " as TSr, " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & " as Subcode, " & AgL.Chk_Text(Dgl1(Col1LinkedSubcode, I).Tag) & " as LinkedSubcode, " & AgL.Chk_Text(TxtPartyName.Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1Amount, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1Amount, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, " & AgL.Chk_Text(Dgl1(Col1ChqRefNo, I).Value) & " as ChqNo, " & AgL.Chk_Date(Dgl1(Col1ChqRefDate, I).Value) & " as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate, 
                        " & AgL.Chk_Text(Dgl1(Col1SpecificationDocId, I).Value) & " as ReferenceDocId, " & AgL.Chk_Text(Dgl1(Col1SpecificationDocIdSr, I).Value) & " as ReferenceDocIdSr "

                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mNarration = Dgl1(Col1Subcode, I).Value
                If LblV_Type.Tag = Ncat.Receipt And TxtBank.Text <> "" Then
                    mNarration = mNarration & " deposited cash to " & TxtBank.Text & ". "
                End If
                mNarration = mNarration & Dgl1(Col1Remark, I).Value

                mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1TempSno, I).Value) & "  as TSr, " & AgL.Chk_Text(TxtPartyName.Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1Amount, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1Amount, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, " & AgL.Chk_Text(Dgl1(Col1ChqRefNo, I).Value) & " as ChqNo, " & AgL.Chk_Date(Dgl1(Col1ChqRefDate, I).Value) & " as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                        " & AgL.Chk_Text(Dgl1(Col1SpecificationDocId, I).Value) & " as ReferenceDocId, " & AgL.Chk_Text(Dgl1(Col1SpecificationDocIdSr, I).Value) & " as ReferenceDocIdSr"

                If Val(Dgl1(Col1Deduction, I).Value) > 0 Then
                    If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                    mNarration = " Amount deducted."
                    mNarration = mNarration & Dgl1(Col1Remark, I).Value

                    mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1TempSno, I).Value) & " as TSr, " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & " as Subcode, " & AgL.Chk_Text(Dgl1(Col1LinkedSubcode, I).Tag) & " as LinkedSubcode, " & AgL.Chk_Text(mPostingAcDeduction) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1Deduction, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1Deduction, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, " & AgL.Chk_Text(Dgl1(Col1ChqRefNo, I).Value) & " as ChqNo, " & AgL.Chk_Date(Dgl1(Col1ChqRefDate, I).Value) & " as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                            " & AgL.Chk_Text(Dgl1(Col1SpecificationDocId, I).Value) & " as ReferenceDocId, " & AgL.Chk_Text(Dgl1(Col1SpecificationDocIdSr, I).Value) & " as ReferenceDocIdSr"

                    If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                    mNarration = Dgl1(Col1Subcode, I).Value & ". Amount deducted."
                    mNarration = mNarration & Dgl1(Col1Remark, I).Value

                    mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1TempSno, I).Value) & "  as TSr, " & AgL.Chk_Text(mPostingAcDeduction) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1Deduction, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1Deduction, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, " & AgL.Chk_Text(Dgl1(Col1ChqRefNo, I).Value) & " as ChqNo, " & AgL.Chk_Date(Dgl1(Col1ChqRefDate, I).Value) & " as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                        " & AgL.Chk_Text(Dgl1(Col1SpecificationDocId, I).Value) & " as ReferenceDocId, " & AgL.Chk_Text(Dgl1(Col1SpecificationDocIdSr, I).Value) & " as ReferenceDocIdSr "
                End If


                If Val(Dgl1(Col1OtherCharges, I).Value) > 0 Then
                    If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                    mNarration = TxtPartyName.Text & ". Other Charges."
                    mNarration = mNarration & Dgl1(Col1Remark, I).Value

                    mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1TempSno, I).Value) & " as TSr, " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & " as Subcode, " & AgL.Chk_Text(Dgl1(Col1LinkedSubcode, I).Tag) & " as LinkedSubcode, " & AgL.Chk_Text(mPostingAcOtherCharges) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1OtherCharges, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1OtherCharges, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, " & AgL.Chk_Text(Dgl1(Col1ChqRefNo, I).Value) & " as ChqNo, " & AgL.Chk_Date(Dgl1(Col1ChqRefDate, I).Value) & " as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                            " & AgL.Chk_Text(Dgl1(Col1SpecificationDocId, I).Value) & " as ReferenceDocId, " & AgL.Chk_Text(Dgl1(Col1SpecificationDocIdSr, I).Value) & " as ReferenceDocIdSr "

                    If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                    mNarration = Dgl1(Col1Subcode, I).Value & ". Other Charges."
                    mNarration = mNarration & Dgl1(Col1Remark, I).Value

                    mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1TempSno, I).Value) & "  as TSr, " & AgL.Chk_Text(mPostingAcOtherCharges) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1OtherCharges, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1OtherCharges, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, " & AgL.Chk_Text(Dgl1(Col1ChqRefNo, I).Value) & " as ChqNo, " & AgL.Chk_Date(Dgl1(Col1ChqRefDate, I).Value) & " as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                        " & AgL.Chk_Text(Dgl1(Col1SpecificationDocId, I).Value) & " as ReferenceDocId, " & AgL.Chk_Text(Dgl1(Col1SpecificationDocIdSr, I).Value) & " as ReferenceDocIdSr "
                End If




                If LblV_Type.Tag = Ncat.Receipt Then
                    If TxtBank.Text <> "" Then
                        If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                        mNarration = Dgl1(Col1Subcode, I).Value & " deposited cash to " & TxtBank.Text & ". " & Dgl1(Col1Remark, I).Value
                        mLedgerPostingData += " Select Null as TSr, " & AgL.Chk_Text(TxtBank.Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtPartyName.Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1Amount, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1Amount, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, " & AgL.Chk_Text(Dgl1(Col1ChqRefNo, I).Value) & " as ChqNo, " & AgL.Chk_Date(Dgl1(Col1ChqRefDate, I).Value) & " as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                                " & AgL.Chk_Text(Dgl1(Col1SpecificationDocId, I).Value) & " as ReferenceDocId, " & AgL.Chk_Text(Dgl1(Col1SpecificationDocIdSr, I).Value) & " as ReferenceDocIdSr "


                        If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                        mNarration = Dgl1(Col1Subcode, I).Value & " deposited cash to " & TxtBank.Text & ". " & Dgl1(Col1Remark, I).Value
                        mLedgerPostingData += " Select Null as TSr, " & AgL.Chk_Text(TxtPartyName.Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtBank.Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1Amount, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1Amount, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, " & AgL.Chk_Text(Dgl1(Col1ChqRefNo, I).Value) & " as ChqNo, " & AgL.Chk_Date(Dgl1(Col1ChqRefDate, I).Value) & " as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                                " & AgL.Chk_Text(Dgl1(Col1SpecificationDocId, I).Value) & " as ReferenceDocId, " & AgL.Chk_Text(Dgl1(Col1SpecificationDocIdSr, I).Value) & " as ReferenceDocIdSr "
                    End If
                End If
            End If
        Next



        'If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
        'mNarration = TxtV_Type.Text & " : " & mNarration
        'mLedgerPostingData += " Select " & AgL.Chk_Text(TxtPartyName.Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtPartyName.Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(LblTotalAmount.Text), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(LblTotalAmount.Text), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, null as ChqNo, Null as ChqDate, Null as EffectiveDate "

        If mLedgerPostingData = "" Then Exit Sub

        mSr = AgL.Dman_Execute("Select IfNull(Max(V_SNo),1) From Ledger With (NoLock) Where DocId = '" & DocID & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        mLedgerPostingData = "Select TSr, SubCode, LinkedSubcode, ContraAc, Narration, AmtDr*1.0 as AmtDr, AmtCr*1.0 as AmtCr, ChqNo, ChqDate, EffectiveDate, ReferenceDocId, ReferenceDocIdSr 
                              From (" & mLedgerPostingData & ") as X  "
        DtTemp = AgL.FillData(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        Dim StrLedgerDate As String = TxtV_Date.Text

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("ChqDate")) <> Nothing Then
                    If AgL.XNull(DtTemp.Rows(I)("ChqDate")) <> "" Then
                        If Date.Parse(TxtV_Date.Text) < Date.Parse(DtTemp.Rows(I)("ChqDate")) Then
                            StrLedgerDate = DtTemp.Rows(I)("ChqDate")
                        End If
                    End If
                End If

                mQry = "INSERT INTO Ledger
                        (DocId, V_SNo, TSr, V_No, V_Type, RecID, V_Prefix, 
                        V_Date, SubCode, LinkedSubcode, ContraSub, AmtDr, AmtCr, 
                        Chq_No, Chq_Date, EffectiveDate, ReferenceDocId, ReferenceDocIdSr, Narration, Site_Code, DivCode, 
                        U_Name, U_EntDt, U_AE)
                        VALUES('" & DocID & "', " & I + mSr + 1 & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("TSr"))) & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                        " & AgL.Chk_Date(StrLedgerDate) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Subcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("LinkedSubcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ContraAc"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtDr"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtCr"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqNo"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqDate"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("EffectiveDate"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ReferenceDocId"))) & ",
                        " & Val(AgL.VNull(DtTemp.Rows(I)("ReferenceDocIdSr"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Narration"))) & "," & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",
                        " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'A'
                        )"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Next
        End If
    End Sub

    Public Sub PostStructureLineToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarrParty As String, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String,
                                                   ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer,
                                                   ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String,
                                                   ByVal Conn As Object, ByVal Cmd As Object, Optional ByVal mCostCenter As String = "", Optional MultiplyWithMinus As Boolean = False)
        Dim StrContraTextJV As String = ""
        Dim mPostSubCode = ""
        Dim mPostContraSub = ""
        Dim I As Integer, J As Integer
        Dim mQry$ = "", bSelectionQry$ = ""
        Dim DtTemp As DataTable = Nothing

        bSelectionQry = ""
        For I = 0 To FGMain.Rows.Count - 1
            For J = 0 To FGMain.AgLineGrid.Rows.Count - 1
                If FGMain.AgLineGrid.Rows(J).Visible Then
                    If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc)) <> "" Then
                        If Dgl1.Item(Col1Amount, J).Style.ForeColor = Color.Blue And FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag.ToString.ToUpper = "GAMT" Then
                            ' Not Fore Colour = Blue Means This Entry is Splitted into several Cash Entries

                        Else
                            If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                            bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, 
                        '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As ContraAc, 
                        Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                             When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount,  
                        " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, J).Value) & " as EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, J).Value) & " as Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, J).Value) & " as ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, J).Value) & " as ChqDate "

                            If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc)) <> "" Then
                                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                                bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As PostAc, 
                            '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As ContraAc, 
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount,  
                            " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, J).Value) & " as EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, J).Value) & " as Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, J).Value) & " as ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, J).Value) & " as ChqDate "
                            End If
                        End If
                    ElseIf Trim(AgL.XNull(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value)) <> "" Then
                        If Dgl1.Item(Col1Amount, J).Style.ForeColor = Color.Blue And FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag.ToString.ToUpper = "GAMT" Then
                            ' Not Fore Colour = Blue Means This Entry is Splitted into several Cash Entries
                        Else
                            If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                            bSelectionQry += " Select 1 as TmpCol,'" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc,
                            '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As ContraAc,
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount,
                            " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, J).Value) & " as EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, J).Value) & " as Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, J).Value) & " as ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, J).Value) & " as ChqDate "


                            If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc)) <> "" Then
                                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                                bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As PostAc, 
                            '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As ContraAc, 
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount,  
                            " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, J).Value) & " as EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, J).Value) & " as Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, J).Value) & " as ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, J).Value) & " as ChqDate "
                            End If
                        End If
                    End If

                    If Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) <> 0 Then
                        If AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) Is Nothing Then
                            Err.Raise(1, , "Error In Ledger Posting. Dr/Cr Not defined for any value.")
                        End If
                    End If
                End If
            Next
        Next

        If bSelectionQry = "" Then Exit Sub


        mQry = " Select Count(*)  " &
                    " From (" & bSelectionQry & ") As V1 Group by tmpCol " &
                    " Having Round(Sum(Case When IfNull(V1.Amount*1.0,0) > 0 Then IfNull(V1.Amount*1.0,0) Else 0 End),3) <> Round(abs(Sum(Case When IfNull(V1.Amount*1.0,0) < 0 Then IfNull(V1.Amount*1.0,0) Else 0 End)),3)  "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            If AgL.VNull(DtTemp.Rows(0)(0)) > 0 Then
                Console.Write(mQry)
                Err.Raise(1, , "Error In Ledger Posting. Debit And Credit balances are Not equal.")
            End If
        End If


        If MultiplyWithMinus Then
            mQry = " Select V1.EffectiveDate, V1.Narration, V1.ChqNo, V1.ChqDate,V1.PostAc, V1.ContraAc, cSg.Name as ContraName, IfNull(Sum(Cast(V1.Amount as Float)),0) As Amount, 
                Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Cr' 
                     When IfNull(Sum(V1.Amount),0) < 0 Then 'Dr' End As DrCr 
                From (" & bSelectionQry & ") As V1 
                Left Join Subgroup cSg  on V1.ContraAc = cSg.Subcode
                Group BY V1.PostAc, V1.ContraAc, cSg.Name, V1.EffectiveDate, V1.Narration, V1.ChqNo, V1.ChqDate "
        Else
            mQry = " Select V1.EffectiveDate, V1.Narration, V1.ChqNo, V1.ChqDate,V1.PostAc, V1.ContraAc, cSg.Name as ContraName, IfNull(Sum(Cast(V1.Amount As Float)),0) As Amount, 
                 Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Dr' 
                      When IfNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr 
                From(" & bSelectionQry & ") As V1 
                Left Join Subgroup cSg  on V1.ContraAc = cSg.Subcode
                Group BY V1.PostAc, V1.ContraAc, cSg.Name, V1.EffectiveDate, V1.Narration, V1.ChqNo, V1.ChqDate "
        End If

        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    Else
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    End If
                End If
            Next
        End With

        Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double
        Dim mNarration As String = ""
        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
                    mSrl += 1

                    mDebit = 0 : mCredit = 0
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        mPostSubCode = PostingPartyAc
                    Else
                        mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
                    End If

                    If AgL.StrCmp(AgL.XNull(.Rows(I)("ContraAc")), "|PARTY|") Then
                        mPostContraSub = PostingPartyAc
                    Else
                        mPostContraSub = AgL.XNull(.Rows(I)("ContraAc"))
                    End If


                    If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
                        mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
                        mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    End If





                    mNarration = AgL.XNull(AgL.Dman_Execute("Select Max(Name) From Subgroup  With (NoLock) Where Subcode = '" & mPostContraSub & "'", AgL.GcnRead).ExecuteScalar)
                    If IIf(AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|"), mNarrParty, AgL.XNull(.Rows(I)("Narration"))) <> "" Then mNarration = mNarration & vbCrLf
                    mNarration = mNarration & IIf(AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|"), mNarrParty, AgL.XNull(.Rows(I)("Narration")))



                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                         " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                         " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText, CostCenter,EffectiveDate) Values " &
                         " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.Chk_Text(CDate(mV_Date).ToString("s")) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text(mPostContraSub) & ", " &
                         " " & mDebit & "," & mCredit & ", " &
                         " " & AgL.Chk_Text(mNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                         " '" & mSite_Code & "','" & mDiv_Code & "'," & AgL.Chk_Text(AgL.XNull(.Rows(I)("ChqNo"))) & "," &
                         " " & AgL.Chk_Date(AgL.XNull(.Rows(I)("ChqDate"))) & "," & AgL.Chk_Text("") & "," &
                         " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "'," & AgL.Chk_Text(StrContraTextJV) & ", " & AgL.Chk_Text(mCostCenter) & ", " & AgL.Chk_Date(AgL.XNull(.Rows(I)("EffectiveDate"))) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next I
        End With
    End Sub


    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False
        Dim DsTemp As DataSet

        mIsEntryLocked = False

        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0

        mQry = " Select H.*, Sg.Name as AccountName, Sg.Nature, VT.Category as VoucherCategory, Bank.Name as BankAcName, HC.*                                 
                From (Select * From LedgerHead  With (NoLock) Where DocID='" & SearchCode & "') H 
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join LedgerHeadCharges Hc With (NoLock) on H.DocID = HC.DocID
                LEFT JOIN viewHelpSubgroup Sg  With (NoLock) ON H.Subcode = Sg.Code
                LEFT JOIN viewHelpSubgroup Bank  With (NoLock) ON H.BankAc = Bank.Code
                "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                'TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                'TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)


                TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                TxtVoucherCategory.Text = AgL.XNull(.Rows(0)("VoucherCategory"))

                If (LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.DebitNoteSupplier And TxtVoucherCategory.Text.ToUpper = AgLibrary.ClsMain.agConstants.VoucherCategory.Purchase) Or
                       (LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.CreditNoteCustomer And TxtVoucherCategory.Text.ToUpper = AgLibrary.ClsMain.agConstants.VoucherCategory.Sales) Then
                    mMultiplyWithMinus = True
                End If

                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag

                TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))

                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtPartyName.Tag = AgL.XNull(.Rows(0)("Subcode"))
                TxtPartyName.Text = AgL.XNull(.Rows(0)("AccountName"))
                TxtPartyName.AgLastValueTag = TxtPartyName.Tag
                TxtPartyName.AgLastValueText = TxtPartyName.Text
                TxtNature.Text = AgL.XNull(.Rows(0)("Nature"))
                TxtPartyDocNo.Text = AgL.XNull(.Rows(0)("PartyDocNo"))
                TxtPartyDocDate.Text = AgL.XNull(.Rows(0)("PartyDocDate"))
                TxtBank.Tag = AgL.XNull(.Rows(0)("BankAc"))
                TxtBank.Text = AgL.XNull(.Rows(0)("BankAcName"))
                TxtType.Text = AgL.XNull(.Rows(0)("Type"))
                ShowChqRefNo()
                'Call FGetCurrBal(TxtPartyName.AgSelectedValue)


                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), EntryNCat, TxtV_Date.Text, mMultiplyWithMinus)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))




                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                Dim strQryPaymentSettlement$ = "Select Max(L.DocId) As DocId, L.PaymentDocId, H.SubCode, Sum(L.PaidAmount) As PaidAmount " &
                                    "FROM Cloth_SupplierSettlementPayments L  With (NoLock) " &
                                    "LEFT JOIN LedgerHead H With (NoLock) On L.DocId = H.DocId " &
                                    "Where L.PaymentDocId = '" & mSearchCode & "' " &
                                    "And L.DocId <> L.PaymentDocId " &
                                    "GROUP BY L.PaymentDocId, H.SubCode "


                mQry = "Select L.*, Sg.Name as AccountName, Lsg.Name as LinkedAccountName, Barcode.Description as BarcodeDescription,
                        U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, Sg.Nature, Sg.SubgroupType, LC.* , LTds.*,
                        Tg.Description AS TdsGroupDesc, Tc.Description AS TdsCategoryDesc, TSg.Name As TdsLedgerAccountName,
                        (Case When VSettlement.DocId Is Not Null Then 1 Else 0 End) As RowLocked
                        From (Select * From LedgerHeadDetail  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN viewHelpSubgroup Sg  With (NoLock) ON L.Subcode = Sg.Code 
                        LEFT JOIN viewHelpSubgroup Lsg  With (NoLock) ON L.LinkedSubcode = Lsg.Code 
                        Left Join Unit U  With (NoLock) On L.Unit = U.Code 
                        Left Join Barcode on L.Barcode = Barcode.Code 
                        Left Join LedgerHeadDetailCharges LC  With (NoLock) on L.DocID = LC.DocID And L.Sr = LC.Sr
                        Left Join LedgerHeadDetailTds LTds  With (NoLock) on L.DocID = LTds.DocID And L.Sr = LTds.Sr
                        LEFT JOIN TdsGroup Tg ON LTds.TdsGroup = Tg.Code
                        LEFT JOIN TdsCategory Tc ON LTds.TdsCategory = Tc.Code
                        LEFT JOIN SubGroup TSg On LTds.TdsLedgerAccount = TSg.SubCode
                        Left Join(" & strQryPaymentSettlement & ") As VSettlement On L.DocId = VSettlement.PaymentDocId And L.SubCode = VSettlement.SubCode
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


                            Dgl1.Item(Col1Subcode, I).Tag = AgL.XNull(.Rows(I)("Subcode"))
                            Dgl1.Item(Col1Subcode, I).Value = AgL.XNull(.Rows(I)("AccountName"))

                            Dgl1.Item(Col1LinkedSubcode, I).Tag = AgL.XNull(.Rows(I)("LinkedSubcode"))
                            Dgl1.Item(Col1LinkedSubcode, I).Value = AgL.XNull(.Rows(I)("LinkedAccountName"))

                            Dgl1.Item(Col1Nature, I).Value = AgL.XNull(.Rows(I)("Nature"))
                            Dgl1.Item(Col1SubgroupType, I).Value = AgL.XNull(.Rows(I)("SubgroupType"))


                            Dgl1.Item(Col1CurrentBalance, I).Value = FGetCurrBal(Dgl1.Item(Col1Subcode, I).Tag, TxtV_Date.Text)
                            FShowCurrBal(I)
                            Dgl1.Item(Col1CurrentBalance, I).Tag = GetOutstandingBillsFifoList(Dgl1.Item(Col1Subcode, I).Tag, TxtV_Date.Text)
                            Dgl1.Item(Col1Barcode, I).Tag = AgL.XNull(.Rows(I)("Barcode"))
                            Dgl1.Item(Col1Barcode, I).Value = AgL.XNull(.Rows(I)("BarcodeDescription"))

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1HSN, I).Value = AgL.XNull(.Rows(I)("HSN"))
                            Dgl1.Item(Col1ReferenceNo, I).Value = AgL.XNull(.Rows(I)("ReferenceNo"))
                            Dgl1.Item(Col1SpecificationDocId, I).Value = AgL.XNull(.Rows(I)("SpecificationDocId"))
                            Dgl1.Item(Col1SpecificationDocIdSr, I).Value = AgL.VNull(.Rows(I)("SpecificationDocIdSr"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                            Dgl1.Item(Col1Deduction, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Deduction"))), "0.00")
                            Dgl1.Item(Col1OtherCharges, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Other_Charge"))), "0.00")
                            Dgl1.Item(Col1ChqRefNo, I).Value = AgL.XNull(.Rows(I)("ChqRefNo"))
                            Dgl1.Item(Col1ChqRefDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ChqRefDate")))
                            Dgl1.Item(Col1EffectiveDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("EffectiveDate")))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))

                            Dgl1.Item(Col1TdsCategory, I).Tag = AgL.XNull(.Rows(I)("TdsCategory"))
                            Dgl1.Item(Col1TdsCategory, I).Value = AgL.XNull(.Rows(I)("TdsCategoryDesc"))
                            Dgl1.Item(Col1TdsGroup, I).Tag = AgL.XNull(.Rows(I)("TdsGroup"))
                            Dgl1.Item(Col1TdsGroup, I).Value = AgL.XNull(.Rows(I)("TdsGroupDesc"))
                            Dgl1.Item(Col1TdsLedgerAccount, I).Tag = AgL.XNull(.Rows(I)("TdsLedgerAccount"))
                            Dgl1.Item(Col1TdsLedgerAccount, I).Value = AgL.XNull(.Rows(I)("TdsLedgerAccountName"))
                            Dgl1.Item(Col1TdsMonthlyLimit, I).Value = AgL.VNull(.Rows(I)("TdsMonthlyLimit"))
                            Dgl1.Item(Col1TdsYearlyLimit, I).Value = AgL.VNull(.Rows(I)("TdsYearlyLimit"))
                            Dgl1.Item(Col1TdsPer, I).Value = AgL.VNull(.Rows(I)("TdsPer"))
                            Dgl1.Item(Col1PartyMonthTransaction, I).Value = AgL.VNull(.Rows(I)("PartyMonthTransaction"))
                            Dgl1.Item(Col1PartyYearTransaction, I).Value = AgL.VNull(.Rows(I)("PartyYearTransaction"))
                            Dgl1.Item(Col1TdsAmount, I).Value = AgL.VNull(.Rows(I)("TdsAmount"))
                            Dgl1.Item(Col1TempAmountForTdsCalculation, I).Value = Val(Dgl1.Item(Col1Amount, I).Value) + Val(Dgl1.Item(Col1TdsAmount, I).Value)

                            If Dgl1.Item(Col1TdsAmount, I).Value > 0 Then
                                Dgl1.Item(Col1TdsAmount, I).Style.BackColor = Color.LightPink
                            End If

                            Dgl1.Item(Col1IsRecordLocked, I).Value = AgL.VNull(.Rows(I)("RowLocked"))
                            If Dgl1.Item(Col1IsRecordLocked, I).Value > 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True : mIsEntryLocked = True


                            If TxtNature.Text.ToUpper = "CASH" Then
                                mQry = "Select Count(*) From Ledger  With (NoLock) Where ReferenceDocID = '" & SearchCode & "' And ReferenceDocIDSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & ""
                                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                                    Dgl1.Item(Col1Amount, I).Style.ForeColor = Color.Blue
                                    ShowVoucherEntryCash(I, False)
                                End If
                            End If


                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I, mMultiplyWithMinus)

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                            Dim dtReconcileDate As DataTable
                            mQry = "Select Max(Clg_Date) as Clg_Date  From Ledger Where DocID = '" & AgL.XNull(.Rows(I)("DocID")) & "' And TSr = '" & AgL.XNull(.Rows(I)("Sr")) & "' "
                            dtReconcileDate = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If dtReconcileDate.Rows.Count > 0 Then
                                Dgl1.Item(Col1ReconcileDate, I).Value = ClsMain.FormatDate(AgL.XNull(dtReconcileDate.Rows(0)("Clg_Date")))
                            End If

                            If AgL.Dman_Execute("Select Count(*) From TransactionReferences  With (NoLock) Where ReferenceDocId = '" & mSearchCode & "'
                                        And ReferenceSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  And Type = '" & TransactionReferenceTypeConstants.Cancelled & "' ", AgL.GCn).ExecuteScalar() > 0 Then
                                Dgl1.Item(Col1IsRecordLocked, I).Value = 1
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = ColorConstants.Cancelled
                                Dgl1.Rows(I).ReadOnly = True
                            End If
                        Next I
                    End If
                End With
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

                '-------------------------------------------------------------

                If Dgl1.CurrentCell IsNot Nothing Then
                    If Dgl1.Item(Col1CurrentBalance, Dgl1.CurrentCell.RowIndex).Tag IsNot Nothing Then
                        FillOutstandingGrid(Dgl1.Item(Col1CurrentBalance, Dgl1.CurrentCell.RowIndex).Tag)
                    Else
                        Dgl2.Visible = False
                    End If
                Else
                    Dgl2.Visible = False
                End If


            End If
        End With
        SetAttachmentCaption()
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub



    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtPartyName.Validating, TxtReferenceNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim FrmObj As New FrmSaleInvoiceParty_WithDimension
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    If TxtV_Type.Tag = "" Then Exit Sub

                    mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtV_TypeSettings.Rows.Count = 0 Then
                            mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code = '" & TxtSite_Code.Tag & "' "
                            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtV_TypeSettings.Rows.Count = 0 Then
                                mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code is Null "
                                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                If DtV_TypeSettings.Rows.Count = 0 Then
                                    mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code Is Null And Site_Code is Null "
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
                    TxtPartyName.AgHelpDataSet = Nothing


                    TxtStructure.AgSelectedValue = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GcnRead).ExecuteScalar
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    AgCalcGrid1.AgNCat = LblV_Type.Tag

                    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                    TxtVoucherCategory.Text = AgL.Dman_Execute("Select Category From Voucher_Type  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GCn).ExecuteScalar

                    IniGrid()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    FMaintainPreviousRecordParty()
                    FGetSettingVariableValuesForAddAndEdit()

                Case TxtReferenceNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "LedgerHead",
                                        TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                        TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                        TxtReferenceNo.Text, mSearchCode)

                Case TxtPartyName.Name
                    If LblV_Type.Tag = Ncat.DebitNoteSupplier Or LblV_Type.Tag = Ncat.DebitNoteCustomer Or LblV_Type.Tag = Ncat.CreditNoteCustomer Or LblV_Type.Tag = Ncat.CreditNoteSupplier Then
                        If ClsMain.IsPartyBlocked(TxtPartyName.Tag, LblV_Type.Tag) Then
                            MsgBox("Party is blocked for " & TxtV_Type.Text & ". Record will not be saved.")
                        End If
                    End If

                    TxtNature.Text = AgL.Dman_Execute("Select IfNull(Nature,'') From Subgroup  With (NoLock) Where Subcode = '" & TxtPartyName.Tag & "'", AgL.GCn).ExecuteScalar
                    ShowChqRefNo()

                    If TxtRemarks.Visible Then TxtRemarks.Focus()
                    If TxtBank.Visible Then TxtBank.Focus()
                    If TxtPartyDocNo.Visible Then TxtPartyDocNo.Focus()
                    If TxtType.Visible Then TxtType.Focus()


                    If TxtPartyName.Text <> TxtPartyName.AgLastValueText Then
                        Dgl1.AgHelpDataSet(Col1Specification) = Nothing
                    End If
                    HandlePendingLR(sender.tag)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, TxtSite_Code.Tag, TxtVoucherCategory.Tag, LblV_Type.Tag, TxtV_Type.Tag, "", "")
        FGetSettings = mValue
    End Function

    Sub ShowChqRefNo()
        If TxtNature.Text.ToUpper = "BANK" Then
            Dgl1.Columns(Col1ChqRefNo).Visible = True
            Dgl1.Columns(Col1ChqRefDate).Visible = True
        Else
            Dgl1.Columns(Col1ChqRefNo).Visible = False
            Dgl1.Columns(Col1ChqRefDate).Visible = False
        End If

        If FGetSettings(SettingFields.PartyCanDepositCashAtBankYn, SettingType.General) Then
            If TxtNature.Text.ToUpper = "CASH" Then
                LblBank.Visible = True
                TxtBank.Visible = True
            Else
                LblBank.Visible = False
                TxtBank.Visible = False
            End If
        Else
            LblBank.Visible = False
            TxtBank.Visible = False
        End If
    End Sub


    Private Function FShowCurrBal(rowIndex As Integer) As Double
        LblCurrentBalance.Text = Format(Val(Dgl1.Item(Col1CurrentBalance, rowIndex).Value), "0.00")
        FShowCurrBal = Val(Dgl1.Item(Col1CurrentBalance, rowIndex).Value)
    End Function

    Private Function FGetCurrBal(Subcode As String, V_Date As Date) As Double
        'mQry = " Select IfNull(Sum(AmtDr),0) - IfNull(Sum(AmtCr),0) As CurrBal 
        '        From Ledger 
        '        Where SubCode = '" & Subcode & "' 
        '        And Date(V_Date) <= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & "
        '        And Ledger.Site_Code = '" & TxtSite_Code.Tag & "'
        '        And Ledger.DivCode = '" & TxtDivision.Tag & "'"

        mQry = " Select IfNull(Sum(AmtDr),0) - IfNull(Sum(AmtCr),0) As CurrBal 
                From Ledger 
                Where SubCode = '" & Subcode & "' 
                And Date(V_Date) <= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & ""

        FGetCurrBal = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    End Function

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code = '" & TxtSite_Code.Tag & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code Is Null And Site_Code is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    End If
                End If
            End If
        End If
        'If DtV_TypeSettings.Rows.Count = 0 Then
        '    MsgBox("Voucher Type settings not found")
        '    Topctrl1.FButtonClick(14, True)
        '    Exit Sub
        'End If

        mIsEntryLocked = False

        TxtStructure.Tag = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GcnRead).ExecuteScalar 'AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = EntryNCat

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        TxtVoucherCategory.Text = AgL.Dman_Execute("Select Category From Voucher_Type  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GCn).ExecuteScalar



        IniGrid()
        TabControl1.SelectedTab = TP1

        'AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtRateType.AgSelectedValue
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtPartyName.Text = TxtPartyName.AgLastValueText
        TxtPartyName.Tag = TxtPartyName.AgLastValueTag

        Application.DoEvents()

        'If Me.ActiveControl Is TxtV_Date Then
        '    TxtV_Date.SelectionStart = 0
        '    TxtV_Date.SelectionLength = TxtV_Date.Text.Length - 1
        'End If

        'TxtGodown.Tag = DtV_TypeSettings.Rows(0)("DEFAULT_Godown")
        'TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" Select Description From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GCn).ExecuteScalar)




        'TxtSaleToParty.Focus()
        SetAttachmentCaption()
        FGetSettingVariableValuesForAddAndEdit()

        If SettingFields_MaximumItemLimit = 1 Then
            Dgl1.AllowUserToAddRows = False
            Dgl1.Rows.Clear()
            Dgl1.Rows.Add(1)
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
                Case Col1Subcode
                    ValidateSubcodeLine(mRowIndex)
                    If SettingFields_CopyRemarkInNextLineYn = True Then
                        If mRowIndex > 0 Then
                            If Dgl1.Item(Col1Remark, mRowIndex).Value = "" And Dgl1.Item(Col1Remark, mRowIndex - 1).Value <> "" Then
                                Dgl1.Item(Col1Remark, mRowIndex).Value = Dgl1.Item(Col1Remark, mRowIndex - 1).Value
                            End If
                        End If
                    End If
                Case Col1Specification
                    FValidate_ReferenceNo(mRowIndex)
                Case Col1Amount
                    ShowVoucherEntryCash(mRowIndex)
                    If Val(Dgl1.Item(Col1Amount, mRowIndex).Value) + Val(Dgl1.Item(Col1TdsAmount, mRowIndex).Value) <> Val(Dgl1.Item(Col1TempAmountForTdsCalculation, mRowIndex).Value) Then
                        Dgl1.Item(Col1TempAmountForTdsCalculation, mRowIndex).Value = Dgl1.Item(Col1Amount, mRowIndex).Value
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ValidateSubcodeLine(mRowIndex As Integer)
        Dim DtTemp As DataTable


        If Not (LblV_Type.Tag = Ncat.DebitNoteCustomer Or LblV_Type.Tag = Ncat.DebitNoteSupplier Or LblV_Type.Tag = Ncat.CreditNoteCustomer Or LblV_Type.Tag = Ncat.CreditNoteSupplier) Then
            If ClsMain.IsPartyBlocked(Dgl1.Item(Col1Subcode, mRowIndex).Tag, LblV_Type.Tag) Then
                MsgBox("Party is blocked for " & TxtV_Type.Text & ". Record will not be saved.")
            End If
        End If


        mQry = "Select Sg.Nature, Sg.SubgroupType, Sg.HSN From Subgroup Sg Where Sg.Subcode = '" & Dgl1.Item(Col1Subcode, mRowIndex).Tag & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            Dgl1(Col1HSN, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("HSN"))
            Dgl1(Col1SubgroupType, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("SubgroupType"))
            Dgl1(Col1Nature, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Nature"))
        End If

        Dgl1.Item(Col1CurrentBalance, mRowIndex).Value = FGetCurrBal(Dgl1.Item(Col1Subcode, mRowIndex).Tag, TxtV_Date.Text)
        Dgl1.Item(Col1CurrentBalance, mRowIndex).Tag = GetOutstandingBillsFifoList(Dgl1.Item(Col1Subcode, mRowIndex).Tag, TxtV_Date.Text)
        FShowCurrBal(mRowIndex)
        FillOutstandingGrid(Dgl1.Item(Col1CurrentBalance, mRowIndex).Tag)
        FCreateHelpLinkedSubgroupLine()

        If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Then
            If AgL.XNull(DtTemp.Rows(0)("SubgroupType")) = "Master Customer" Or AgL.XNull(DtTemp.Rows(0)("SubgroupType")) = "Master Supplier" Then
                Dgl1(Col1LinkedSubcode, mRowIndex).Tag = Dgl1(Col1Subcode, mRowIndex).Tag
                Dgl1(Col1LinkedSubcode, mRowIndex).Value = Dgl1(Col1Subcode, mRowIndex).Value
            Else
                mQry = "Select Sg.Code, Sg.Name From viewHelpSubgroup Sg Where Sg.code = (Select Parent From Subgroup Where Subcode = '" & Dgl1(Col1Subcode, mRowIndex).Tag & "')"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Dgl1(Col1LinkedSubcode, mRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                    Dgl1(Col1LinkedSubcode, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Name"))
                End If
            End If
        End If

        If LblV_Type.Tag = Ncat.Payment Then
            FGetTdsParameters(mRowIndex)
        End If

        If CType(AgL.VNull(FGetSettings(SettingFields.ShowContraWindowYn, SettingType.General)), Boolean) = True Then
            FOpengPendingLedgerAdj(mRowIndex)
        End If
    End Sub


    Private Sub ShowVoucherEntryCash(mRowIndex As Integer, Optional ShowDialog As Boolean = True)
        Dim objFrmVoucherEntryCash As FrmVoucherEntryCash
        If TxtNature.Text.ToUpper = "CASH" Then
            'If Val(Dgl1.Item(Col1Amount, mRowIndex).Value) > AgL.VNull(AgL.PubDtEnviro.Rows(0)("MaximumCashTransactionLimit")) And Not AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) = ActionsOfMaximumCashTransactionLimitExceeds.None Then
            If Val(Dgl1.Item(Col1Amount, mRowIndex).Value) > 0 And Not AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) = ActionsOfMaximumCashTransactionLimitExceeds.None Then
                If Dgl1.Item(Col1Amount, mRowIndex).Tag IsNot Nothing Then
                    CType(Dgl1.Item(Col1Amount, mRowIndex).Tag, FrmVoucherEntryCash).StartPosition = FormStartPosition.CenterScreen
                    CType(Dgl1.Item(Col1Amount, mRowIndex).Tag, FrmVoucherEntryCash).ShowDialog()
                    Dgl1.Item(Col1Amount, mRowIndex).Value = CType(Dgl1.Item(Col1Amount, mRowIndex).Tag, FrmVoucherEntryCash).LblTotalAmount.Text
                    If CType(Dgl1.Item(Col1Amount, mRowIndex).Tag, FrmVoucherEntryCash).IsDeleteAllButtonPressed Then
                        Dgl1.Item(Col1Amount, mRowIndex).Tag = Nothing
                        Dgl1.Item(Col1Amount, mRowIndex).Style.ForeColor = Color.Black
                    Else
                        Dgl1.Item(Col1Amount, mRowIndex).Style.ForeColor = Color.Blue
                    End If
                Else
                    objFrmVoucherEntryCash = New FrmVoucherEntryCash
                    objFrmVoucherEntryCash.Text = "Voucher Entry Cash"
                    objFrmVoucherEntryCash.LblDocNo.Text = "Entry No : " + TxtReferenceNo.Text
                    objFrmVoucherEntryCash.DtV_TypeSettings = DtV_TypeSettings
                    objFrmVoucherEntryCash.VoucherCategory = TxtVoucherCategory.Text
                    objFrmVoucherEntryCash.SearchCode = mSearchCode
                    objFrmVoucherEntryCash.EntryNCat = EntryNCat
                    objFrmVoucherEntryCash.V_Date = TxtV_Date.Text
                    objFrmVoucherEntryCash.HeaderAccount = TxtPartyName.Tag
                    objFrmVoucherEntryCash.HeaderAccountName = TxtPartyName.Text
                    objFrmVoucherEntryCash.Effective_Date = Dgl1.Item(Col1EffectiveDate, mRowIndex).Value
                    objFrmVoucherEntryCash.SiteCode = TxtSite_Code.Tag
                    objFrmVoucherEntryCash.DivisionCode = TxtDivision.Tag
                    objFrmVoucherEntryCash.PartyAccount = Dgl1.Item(Col1Subcode, mRowIndex).Tag
                    objFrmVoucherEntryCash.PartyName = Dgl1.Item(Col1Subcode, mRowIndex).Value
                    objFrmVoucherEntryCash.TotalAmount = Val(Dgl1.Item(Col1Amount, mRowIndex).Value)
                    objFrmVoucherEntryCash.Ini_Grid()
                    objFrmVoucherEntryCash.MoveRec(mSearchCode, Val(Dgl1.Item(ColSNo, mRowIndex).Tag))
                    Dgl1.Item(Col1Amount, mRowIndex).Tag = objFrmVoucherEntryCash
                    If ShowDialog Then
                        objFrmVoucherEntryCash.StartPosition = FormStartPosition.CenterScreen
                        objFrmVoucherEntryCash.ShowDialog()
                        Dgl1.Item(Col1Amount, mRowIndex).Value = CType(Dgl1.Item(Col1Amount, mRowIndex).Tag, FrmVoucherEntryCash).LblTotalAmount.Text
                        If CType(Dgl1.Item(Col1Amount, mRowIndex).Tag, FrmVoucherEntryCash).IsDeleteAllButtonPressed Then
                            Dgl1.Item(Col1Amount, mRowIndex).Tag = Nothing
                            Dgl1.Item(Col1Amount, mRowIndex).Style.ForeColor = Color.Black
                        Else
                            Dgl1.Item(Col1Amount, mRowIndex).Style.ForeColor = Color.Blue
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        If Topctrl1.Mode = "Browse" Then Exit Sub

        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Subcode, I).Value <> "" Then

                If Dgl1.Item(Col1Rate, I).Value > 0 Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If Val(Dgl1.Item(Col1Amount, I).Value) > 0 Then
                    If AgL.XNull(Dgl1.Item(Col1TdsCategory, I).Tag) <> "" And AgL.XNull(Dgl1.Item(Col1TdsGroup, I).Tag) <> "" Then
                        If Val(Dgl1.Item(Col1PartyMonthTransaction, I).Value) + Val(Dgl1.Item(Col1TempAmountForTdsCalculation, I).Value) > Val(Dgl1.Item(Col1TdsMonthlyLimit, I).Value) Or
                            Val(Dgl1.Item(Col1PartyYearTransaction, I).Value) + Val(Dgl1.Item(Col1TempAmountForTdsCalculation, I).Value) > Val(Dgl1.Item(Col1TdsYearlyLimit, I).Value) Then
                            Dgl1.Item(Col1TdsTaxableAmount, I).Value = Val(Dgl1.Item(Col1PartyYearTransaction, I).Value) + Val(Dgl1.Item(Col1TempAmountForTdsCalculation, I).Value)
                            If Val(Dgl1.Item(Col1TdsTaxableAmount, I).Value) > 0 Then
                                Dgl1.Item(Col1TdsAmount, I).Value = Val(Dgl1.Item(Col1TdsTaxableAmount, I).Value) * Val(Dgl1.Item(Col1TdsPer, I).Value) / 100
                                If Val(Dgl1.Item(Col1TempAmountForTdsCalculation, I).Value) - Val(Dgl1.Item(Col1TdsAmount, I).Value) > 0 Then
                                    Dgl1.Item(Col1Amount, I).Value = Val(Dgl1.Item(Col1TempAmountForTdsCalculation, I).Value) - Val(Dgl1.Item(Col1TdsAmount, I).Value)
                                Else
                                    Dgl1.Item(Col1Amount, I).Value = 0
                                End If
                            End If
                        End If
                    End If
                    If Val(Dgl1.Item(Col1TdsAmount, I).Value) > 0 And
                        Dgl1.Item(Col1TdsAmount, I).Style.BackColor <> Color.LightPink Then
                        MsgBox("Tds amount will be due for " & Dgl1.Item(Col1Subcode, I).Value, MsgBoxStyle.Information)
                        Dgl1.Item(Col1TdsAmount, I).Style.BackColor = Color.LightPink
                    End If
                End If


                'Footer Calculation
                Dim bQty As Double = 0
                bQty = Val(Dgl1.Item(Col1Qty, I).Value)

                LblTotalQty.Text = Val(LblTotalQty.Text) + bQty
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

            End If
        Next






        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        If AgL.VNull(AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable")) = True Then
            AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        Else
            AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = -1
        End If
        AgCalcGrid1.AgPostingPartyAc = TxtPartyName.Tag
        AgCalcGrid1.AgVoucherCategory = TxtVoucherCategory.Text.ToUpper
        AgCalcGrid1.Calculation()





        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub


    Private Sub FValidate_ReferenceNo(RowNumber As Integer)
        If LblV_Type.Tag = Ncat.DebitNoteSupplier Or LblV_Type.Tag = Ncat.DebitNoteCustomer Or LblV_Type.Tag = Ncat.CreditNoteSupplier Or LblV_Type.Tag = Ncat.CreditNoteCustomer Then
            FValidate_ReferenceNoDebitNote(RowNumber)
        End If
    End Sub

    Private Sub FValidate_ReferenceNoDebitNote(RowNumber As Integer)
        Dim mQry As String
        Dim DtRef As DataTable
        mQry = "select L.DocID, L.V_SNo as DocIDSr, I.SalesTaxPostingGroup  
                from Ledger L  With (NoLock)
                Left Join Stock S  With (NoLock) On L.DocID = S.DocId
                Left Join Item I  With (NoLock) On S.Item = I.Code
                Left Join viewHelpSubgroup Sg  With (NoLock) On L.Subcode = Sg.code
                where L.DocID || '+' || Cast(L.V_SNo as nVarchar) = '" & Dgl1.Item(Col1Specification, RowNumber).Tag & "'"
        DtRef = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtRef.Rows.Count > 0 Then
            Dgl1.Item(Col1SpecificationDocId, RowNumber).Value = AgL.XNull(DtRef.Rows(0)("DocID"))
            Dgl1.Item(Col1SpecificationDocIdSr, RowNumber).Value = AgL.XNull(DtRef.Rows(0)("DocIDSr"))
            Dgl1.Item(Col1SalesTaxGroup, RowNumber).Value = AgL.XNull(DtRef.Rows(0)("SalesTaxPostingGroup"))
            Dgl1.Item(Col1SalesTaxGroup, RowNumber).Tag = AgL.XNull(DtRef.Rows(0)("SalesTaxPostingGroup"))
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean

        If AgL.RequiredField(TxtPartyName, LblPartyName.Text) Then passed = False : Exit Sub
        If LblV_Type.Tag = Ncat.DebitNoteSupplier Or LblV_Type.Tag = Ncat.CreditNoteCustomer Or LblV_Type.Tag = Ncat.CreditNoteSupplier Or LblV_Type.Tag = Ncat.DebitNoteCustomer Then
            If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Remark).Index) Then passed = False : Exit Sub
        Else
            If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Subcode).Index) Then passed = False : Exit Sub
        End If

        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
            If ClsMain.FAttachmentFound(mSearchCode) = True Then
                MsgBox("Attchment already found.Delete it.", MsgBoxStyle.Information)
                passed = False : Exit Sub
            End If
        End If


        If LblV_Type.Tag = Ncat.DebitNoteSupplier Or LblV_Type.Tag = Ncat.CreditNoteCustomer Or LblV_Type.Tag = Ncat.CreditNoteSupplier Or LblV_Type.Tag = Ncat.DebitNoteCustomer Then
            If ClsMain.IsPartyBlocked(TxtPartyName.Tag, LblV_Type.Tag) Then
                MsgBox("Party is blocked for " & TxtV_Type.Text & ". Can not continue.")
                passed = False : Exit Sub
            End If
        Else
            For I = 0 To Dgl1.Rows.Count - 1
                If AgL.XNull(Dgl1.Item(Col1Subcode, I).Value) <> "" Then
                    If ClsMain.IsPartyBlocked(AgL.XNull(Dgl1.Item(Col1Subcode, I).Tag), LblV_Type.Tag) Then
                        MsgBox("Party is blocked for " & TxtV_Type.Text & ". Can not continue.")
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End If


        If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) <> ActionsOfMaximumCashTransactionLimitExceeds.None Then

        End If



        If TxtReferenceNo.Text = "" Then
            MsgBox("Entry No. Can Not Be Blank")
            TxtReferenceNo.Focus()
            Exit Sub
        End If

        Dim bCntItemCount As Integer = 0
        If SettingFields_MaximumItemLimit > 0 Then
            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Rows(I).Visible = True And
                    Dgl1.Item(Col1Subcode, I).Value <> "" Then
                    bCntItemCount += 1
                End If
            Next
            If SettingFields_MaximumItemLimit < bCntItemCount Then
                If AgL.StrCmp(Topctrl1.Mode, "Add") Then
                    MsgBox("Maximum item limit is " & SettingFields_MaximumItemLimit.ToString & ". Can not continue.", MsgBoxStyle.Information)
                    passed = False : Exit Sub
                ElseIf AgL.StrCmp(Topctrl1.Mode, "Edit") Then
                    Dim bExistingItemCount As Integer = AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                            From LedgerHeadDetail L 
                            Where L.DocId = '" & mSearchCode & "' ", AgL.GCn).ExecuteScalar())
                    If bCntItemCount > bExistingItemCount Then
                        MsgBox("Maximum item limit is " & SettingFields_MaximumItemLimit.ToString & ". Can not continue.", MsgBoxStyle.Information)
                        passed = False : Exit Sub
                    End If
                End If
            End If
        End If


        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Subcode, I).Value <> "" Then
                    If Dgl1.Columns(Col1Qty).Visible = True Then
                        If Val(.Item(Col1Qty, I).Value) = 0 Then
                            MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                    If Dgl1.Columns(Col1HSN).Visible = True Then
                        If AgL.XNull(Dgl1.Item(Col1HSN, I).Value) = "" Then
                            MsgBox("HSN Is blank at Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1HSN, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If

                    If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Then
                        Select Case Dgl1.Item(Col1SubgroupType, I).Value.ToString.ToUpper
                            Case SubgroupType.Customer.ToUpper, SubgroupType.Supplier.ToUpper
                                If AgL.XNull(Dgl1.Item(Col1LinkedSubcode, I).Value) = "" Then
                                    MsgBox("Linked Account can not be blank.")
                                    Dgl1.CurrentCell = Dgl1.Item(Col1LinkedSubcode, I)
                                    Dgl1.Focus()
                                    passed = False : Exit Sub
                                End If
                        End Select
                    End If

                End If
            Next
        End With



        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "LedgerHead",
                                        TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                        TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                        TxtReferenceNo.Text, mSearchCode)


        If Not CheckDuplicateRef Then
            TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "LedgerHead",
                                        TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                        TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                        TxtReferenceNo.Text, mSearchCode)
        End If

        passed = CheckDuplicateRef


        TxtPartyName.AgLastValueText = TxtPartyName.Text
        TxtPartyName.AgLastValueTag = TxtPartyName.Tag



        If TxtStructure.Text <> "" Then
            If Math.Round(Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)), 0) <> Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) Then
                Calculation()
                Calculation()
            End If
        End If
    End Sub

    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPartyName.KeyDown, TxtBank.KeyDown, TxtType.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtPartyName.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            FCreateHelpSubgroupHeader()
                        End If
                    End If

                Case TxtBank.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "Select Subcode, Name From Subgroup Where Nature = 'Bank' Order By Name"
                            sender.agHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtType.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "Select 'Advance' As Code, 'Advance' As Name "
                            sender.agHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                Case Col1Amount
                    Dgl1.CurrentCell.ReadOnly = IIf(Dgl1.Item(Col1Amount, Dgl1.CurrentCell.RowIndex).Tag Is Nothing, False, True)
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempLedgerHead_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'If BlnIsTotalDeliveryMeasureVisible = False Then LblTotalDeliveryMeasure.Visible = False : LblTotalDeliveryMeasureText.Visible = False
        'If BlnIsMeasureVisible = False Then LblTotalMeasure.Visible = False : LblTotalMeasureText.Visible = False
        'If BlnIsBaleNoVisible = False Then LblTotalBale.Visible = False : LblTotalBaleText.Visible = False
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown

        If e.Control And e.KeyCode = Keys.D Then
            If Val(Dgl1.Item(Col1IsRecordLocked, Dgl1.CurrentCell.RowIndex).Value) = 0 Then
                sender.CurrentRow.visible = False
            End If
        End If
        'If e.Control And e.KeyCode = Keys.D Then
        '    sender.CurrentRow.Selected = True
        'End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If Dgl1.CurrentCell IsNot Nothing Then
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Subcode

            End Select
        End If
    End Sub

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AgL.WinSetting(Me, 654, 990, 0, 0)

        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuEditSave.Visible = False
            MnuImportGSTDataFromDos.Visible = False
            MnuImportFromDos.Visible = False
            MnuImportFromExcel.Visible = False
            MnuImportGSTDataFromExcel.Visible = False
            MnuImportFromTally.Visible = False
        End If

        If EntryNCat = Ncat.Receipt Or EntryNCat = Ncat.VisitReceipt Then
            MnuCancelEntry.Visible = True
        Else
            MnuCancelEntry.Visible = False
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub RbtInvoiceDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Dgl1.CurrentCell IsNot Nothing Then
                Select Case sender.Name

                End Select
            End If

            If Dgl1.AgHelpDataSet(Col1Subcode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Subcode) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmLedgerHead_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1Subcode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Subcode).Dispose() : Dgl1.AgHelpDataSet(Col1Subcode) = Nothing
        If TxtPartyName.AgHelpDataSet IsNot Nothing Then TxtPartyName.AgHelpDataSet.Dispose() : TxtPartyName.AgHelpDataSet = Nothing
    End Sub


    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Subcode
                    'If e.KeyCode = Keys.Insert Then Call FOpenLedgerHead()
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Subcode) Is Nothing Then
                            FCreateHelpSubgroupLine()
                        End If
                    End If

                Case Col1LinkedSubcode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1LinkedSubcode) Is Nothing Then
                            FCreateHelpLinkedSubgroupLine()
                        End If
                    End If


                Case Col1SalesTaxGroup
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = "Select Description as Code, Description as Name From PostingGroupSalesTaxItem  With (NoLock) Where Active=1"
                            Dgl1.AgHelpDataSet(Col1SalesTaxGroup) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Specification
                    FCreateHelpReferenceNo()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FOpenMaster(ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim FrmObj As Object = Nothing
        Dim CFOpen As New ClsFunction
        Dim DtTemp As DataTable = Nothing
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If e.KeyCode = Keys.Insert Then
                If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Subcode Then
                    If Not AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).Contains(",") Then
                        mQry = " Select MnuName, MnuText From ItemType  With (NoLock) Where Code = '" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            FrmObj = CFOpen.FOpen(DtTemp.Rows(0)("MnuName"), DtTemp.Rows(0)("MnuText"), True)
                            If FrmObj IsNot Nothing Then
                                FrmObj.MdiParent = Me.MdiParent
                                FrmObj.Show()
                                FrmObj.Topctrl1.FButtonClick(0)
                                FrmObj = Nothing
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleQuotation_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        GBoxImportFromExcel.Enabled = False

    End Sub

    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        Dgl.Visible = False
    End Sub

    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Subcode, I).Value <> "" Then
                        If mRow <> I Then
                            If AgL.StrCmp(.Item(Col1Subcode, I).Value, .Item(Col1Subcode, mRow).Value) Then
                                If MsgBox("Item " & .Item(Col1Subcode, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    Dgl1.Item(Col1Subcode, mRow).Tag = "" : Dgl1.Item(Col1Subcode, mRow).Value = ""
                                End If
                                '.CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                                '.Rows.Remove(.Rows(mRow)) : Exit Sub
                            End If
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            If Dgl1.Columns(Col1Subcode).Visible = True Then
                Dgl1.CurrentCell = Dgl1.Item(Col1Subcode, Dgl1.Rows.Count - 1) : Dgl1.Focus()
            End If
        End If
    End Sub
    Private Sub FCreateHelpReferenceNo()
        If LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.DebitNoteSupplier Or LblV_Type.Tag = Ncat.CreditNoteCustomer Or LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.CreditNoteSupplier Or LblV_Type.Tag = Ncat.DebitNoteCustomer Then
            FCreateHelpReferenceNoDebitNote()
        End If
    End Sub


    Private Sub FCreateHelpReferenceNoDebitNote()
        Dim mCondStr As String

        If Dgl1.AgHelpDataSet(Col1Specification) Is Nothing Then

            If TxtVoucherCategory.Text.ToUpper = "PURCH" Then
                mQry = "select L.DocID || '+' || Cast(L.V_SNo as nVarchar) as SearchKey, IfNull(Pi.VendorDocNo, L.RecID) as PartyDocNo, L.RecID as InvoiceNo, Vt.Description as DocType, L.V_Date, I.Description as ItemName, S.Qty_Rec + S.Qty_Iss as Qty, S.Rate
                from Ledger L  With (NoLock)
                Left Join PurchInvoice PI With (NoLock) On L.DocID = PI.DocID
                Left Join Voucher_Type Vt On L.V_type = Vt.V_Type
                Left Join Stock S  With (NoLock) On L.DocID = S.DocId
                Left Join Item I  With (NoLock) On S.Item = I.Code
                Left Join viewHelpSubgroup Sg  With (NoLock) On L.Subcode = Sg.Code
                where L.DivCode='" & TxtDivision.Tag & "' And L.Site_Code = '" & TxtSite_Code.Tag & "' 
                And  L.Subcode = '" & TxtPartyName.Tag & "' 
                And Date(L.V_Date)<=" & AgL.Chk_Text(CDate(TxtV_Date.Text).ToString("s")) & "                 
                And L.AmtCr > 0 
                "
            Else
                mQry = "select L.DocID || '+' || Cast(L.V_SNo as nVarchar) as SearchKey, L.RecId as InvoiceNo,Vt.Description as DocType, L.V_Date, I.Description as ItemName, S.Qty_Rec + S.Qty_Iss as Qty, S.Rate
                from Ledger L  With (NoLock)
                Left Join Voucher_Type Vt On L.V_type = Vt.V_Type
                Left Join Stock S  With (NoLock) On L.DocID = S.DocId
                Left Join Item I  With (NoLock) On S.Item = I.Code
                Left Join viewHelpSubgroup Sg  With (NoLock) On L.Subcode = Sg.Code
                Where L.DivCode='" & AgL.PubDivCode & "' And L.Site_Code = '" & TxtSite_Code.Tag & "' 
                And  L.Subcode = '" & TxtPartyName.Tag & "' 
                And Date(L.V_Date)<=" & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & " 
                And L.AmtDr > 0 
                "
            End If

            Dgl1.AgHelpDataSet(Col1Specification) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub
    Private Sub FMaintainPreviousRecordParty()

        If TxtPartyName.Text = "" Then Exit Sub

        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")) & "') <= 0 "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")) & "') <= 0 "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")) & "') <= 0 "
                End If
            End If
        End If

        strCond += " And sg.Code = '" & TxtPartyName.Tag & "' "

        'strCond += " And (Sg.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(Sg.ShowAccountInOtherDivisions,0) =1) "




        mQry = "SELECT Count(*)
                FROM viewHelpSubGroup Sg  With (NoLock) 
                Left Join AcGroup AG With (NoLock) On Sg.GroupCode = Ag.GroupCode
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        If AgL.Dman_Execute(mQry, AgL.GCn).executescalar() = 0 Then
            TxtPartyName.Text = ""
            TxtPartyName.Tag = ""
        End If
    End Sub


    Private Sub FCreateHelpSubgroupHeader()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeHead")) & "') <= 0 "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupHead")) & "') <= 0 "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureHead")) & "') <= 0 "
                End If
            End If
        End If


        'strCond += " And (Sg.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(Sg.ShowAccountInOtherDivisions,0) =1) "




        mQry = "SELECT Sg.Code, Sg.Name, Ag.GroupName, Sg.SubgroupType as [A/c Type]
                FROM viewHelpSubGroup Sg  With (NoLock) 
                Left Join AcGroup AG With (NoLock) On Sg.GroupCode = Ag.GroupCode
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        TxtPartyName.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpSubgroupLine()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeLine")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeLine")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And (CharIndex('+' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeLine")) & "') > 0 Or Sg.SubgroupType Is Null) "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeLine")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And (CharIndex('-' || Sg.SubgroupType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupTypeLine")) & "') <= 0 Or Sg.SubgroupType Is Null) "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupLine")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupLine")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupLine")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupLine")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroupLine")) & "') <= 0 "
                End If
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureLine")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureLine")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureLine")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureLine")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || Sg.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_NatureLine")) & "') <= 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeTypeLine")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeTypeLine")).ToString.Contains(TreeNodeType.Leaf) Then
                    strCond += " And Sg.Parent Is Not Null "
                End If
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeTypeLine")).ToString.Contains(TreeNodeType.Root) Then
                    strCond += " And Sg.Parent Is Null "
                End If
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcTreeNodeTypeLine")).ToString.Contains(TreeNodeType.Parent) Then
                    strCond += " And Sg.SubCode In (Select Distinct Parent From SubGroup) "
                End If
            End If
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowAccountsOfOtherDivisions")) Then
            strCond += " And (Sg.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(Sg.ShowAccountInOtherDivisions,0) = 1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowAccountsOfOtherSites")) Then
            strCond += " And (Sg.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(Sg.ShowAccountInOtherSites,0) = 1) "
        End If


        mQry = "SELECT Sg.SubCode AS Code, Sg1.Name, Sg.Address, Ag.GroupName
                FROM SubGroup Sg  With (NoLock)  
                LEFT JOIN viewHelpSubGroup SG1 On SG1.Code = SG.SubCode                    
                Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        mQry = mQry & " And Sg.SubgroupType Not In ('Master Customer','Master Supplier', 'Ship To Party')"
        Dgl1.AgHelpDataSet(Col1Subcode) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpLinkedSubgroupLine()
        Dim strCond As String = ""

        If AgL.StrCmp(AgL.PubDBName, "RVN") Or AgL.StrCmp(AgL.PubDBName, "RVN1") Or AgL.StrCmp(AgL.PubDBName, "RVN2") Or AgL.StrCmp(AgL.PubDBName, "MLAW") Then
            mQry = "SELECT Sg.Code, Sg.Name, Ag.GroupName
                FROM viewHelpSubGroup Sg
                Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode                 
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            mQry = mQry & " And Sg.SubgroupType Not In ('Master Customer','Master Supplier', 'Ship To Party')"
        Else
            mQry = "SELECT Sg.Code, Sg.Name, Sg.Address
                FROM viewHelpSubGroup Sg  With (NoLock)                
                Where Sg.Code In (Select LinkedSubcode From Ledger Where Subcode='" & Dgl1(Col1Subcode, Dgl1.CurrentCell.RowIndex).Tag & "') or Sg.Code =(Select Parent From Subgroup Where Subcode ='" & Dgl1(Col1Subcode, Dgl1.CurrentCell.RowIndex).Tag & "') "
        End If

        Dgl1.AgHelpDataSet(Col1LinkedSubcode) = AgL.FillData(mQry, AgL.GCn)
        'If Dgl1.AgHelpDataSet(Col1LinkedSubcode).Tables(0).Rows.Count = 1 Then
        '    Dgl1(Col1LinkedSubcode, Dgl1.CurrentCell.RowIndex).Tag = Dgl1.AgHelpDataSet(Col1LinkedSubcode).Tables(0).Rows(0)("Code")
        '    Dgl1(Col1LinkedSubcode, Dgl1.CurrentCell.RowIndex).Value = Dgl1.AgHelpDataSet(Col1LinkedSubcode).Tables(0).Rows(0)("Name")
        'End If
    End Sub


    'Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor,
    '                     Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")

    '    If LblV_Type.Tag = Ncat.Receipt Or LblV_Type.Tag = Ncat.Payment Then
    '        FGetPrintReceiptPaymentCrystal(SearchCode, mPrintFor, IsPrintToPrinter, BulkCondStr)
    '    End If

    'End Sub

    Private Sub FrmLedgerHead_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        'FGetPrintSSRS(ClsMain.PrintFor.DocumentPrint)
        FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
    End Sub

    Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        FGetPrintCrystal(SearchCode, mPrintFor, IsPrintToPrinter, BulkCondStr)
    End Sub

    Sub FGetPrintCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer



        mPrintTitle = AgL.Dman_Execute("Select IfNull(PrintingDescription, Description) From Voucher_Type Where V_Type = '" & TxtV_Type.Tag & "' ", AgL.GCn).ExecuteScalar()

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
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, H.DocID, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat,                                 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as InvoiceNo,                 
                BP.Subcode as PartySubcode, 
                (Case When SI.DocID Is Not Null Then SI.SaleToPartyName Else BP.DispName End) as PartyName, 
                (Case When SI.DocID Is Not Null then IfNull(SI.SaleToPartyAddress,'') Else IfNull(BP.Address,'') End) as PartyAddress, 
                (Case When SI.DocID Is Not Null then SIC.CityName Else IfNull(C.CityName,'') End) as CityName, 
                (Case When SI.DocID Is Not Null then IfNull(SI.SaleToPartyPinCode,'') Else IfNull(BP.Pin,'') End) as PartyPincode, 
                (Case When SI.DocID Is Not Null then IfNull(SICS.ManualCode,'') Else IfNull(State.ManualCode,'') End) as StateCode, 
                (Case When SI.DocID Is Not Null then IfNull(SICS.Description,'') Else IfNull(State.Description,'') End) as StateName, 
                (Case When SI.DocID Is Not Null then IfNull(SI.SaleToPartyMobile,'') Else IfNull(BP.Mobile,'') End) as PartyMobile, 
                BP.ContactPerson, 
                IfNull((SELECT RegistrationNo FROM SubgroupRegistration WHERE RegistrationType ='Sales Tax No' AND Subcode = (Case When RC.RowCnt <=1 Then L.Subcode else '' End)),'') as PartySalesTaxNo, 
                IfNull((SELECT RegistrationNo FROM SubgroupRegistration WHERE RegistrationType ='AADHAR NO' AND Subcode = (Case When RC.RowCnt <=1 Then L.Subcode else '' End)),'')  as PartyAadharNo, 
                IfNull((SELECT RegistrationNo FROM SubgroupRegistration WHERE RegistrationType ='PAN No' AND Subcode = (Case When RC.RowCnt <=1 Then L.Subcode else '' End)),'')  as PartyPanNo,
                '" & FGetSettings(SettingFields.TermsAndConditions, SettingType.General) & "' TermsAndConditions,       
                IfNull(H.PartyDocNo,IfNull(L.ReferenceNo,'')) as ReferenceNo,
                I.Name as LineAccountName, L.Specification as LineSpecification, IfNull(LRef.V_Date, IfNull(L.EffectiveDate,'')) as EffectiveDate,                
                IfNull(LRef.AmtDr+LRef.AmtCr,abs(L.Amount)) as Amount, IfNull(Lc.Deduction,0) as L_Deduction, IfNull(LC.Other_Charge,0) as L_OtherCharge, 
                IfNull(LRef.AmtDr+LRef.AmtCr,abs(L.Amount)) + IfNull(Lc.Deduction,0) - IfNull(LC.Other_Charge,0) as L_NetAmount, 
                IfNull(L.ChqRefNo,'') as ChqRefNo, IfNull(L.ChqRefDate,'') as ChqRefDate, IfNull(L.Remarks,'') as LRemarks, IfNull(H.Remarks,'') as HRemarks,                               
                '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle,
                '" & FGetSettings(SettingFields.DocumentPrintShowPrintDateTimeYn, SettingType.General) & "' as DocumentPrintShowPrintDateTimeYn                
                from (" & bPrimaryQry & ") as H                
                Left Join LedgerHeadDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join LedgerHeadCharges HC On H.DocID = HC.DocID
                Left Join LedgerHeadDetailCharges LC On L.DocID = LC.DocID And L.Sr = LC.Sr
                Left Join Ledger LRef On L.DocID = LRef.ReferenceDocID And L.Sr = LRef.ReferenceDocIDSr And L.Subcode = LRef.Subcode
                Left Join SaleInvoice SI On L.SpecificationDocId = SI.DocId
                Left Join City SIC On SI.SaleToPartyCity = SIC.CityCode
                Left Join State SICS On SIC.State = SICS.Code
                Left Join (Select srL.DocID, Count(srL.DocId) as RowCnt From LedgerHeadDetail srL Where srL.DocID in ('" & IIf(BulkCondStr = "", SearchCode, BulkCondStr) & "') Group By srL.DocId) as RC On H.DocId = RC.DocId
                Left Join viewHelpSubgroup I  With (NoLock) On (Case When RC.RowCnt <=1 Then H.Subcode else L.Subcode End) = I.Code
                Left Join Subgroup BP With (NoLock) On (Case When RC.RowCnt <=1 Then L.Subcode else H.Subcode End) = BP.Subcode                                
                Left Join City C  With (NoLock) On BP.CityCode = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code                                
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description                                
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join DocumentPrefix DP On VT.Category = DP.Category And H.Div_Code = DP.Div_Code                
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                "
        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "


        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            'objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.Party = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            'objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.Agent = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            FGetMailConfiguration(objRepPrint, SearchCode)
            'objRepPrint.AttachmentName = "Invoice"
        Else
            objRepPrint = New AgLibrary.RepView(AgL)
        End If


        If mDocReportFileName = "" Then
            ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, "ReceiptVoucher_Print.rpt", mPrintTitle, , , , TxtPartyName.Tag, TxtV_Date.Text, IsPrintToPrinter)
        Else
            ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, mDocReportFileName, mPrintTitle, , , , TxtPartyName.Tag, TxtV_Date.Text, IsPrintToPrinter)
        End If
    End Sub



    Sub FGetPrintReceiptPaymentCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer


        mPrintTitle = AgL.Dman_Execute("Select IfNull(PrintingDescription, Description) From Voucher_Type Where V_Type = '" & TxtV_Type.Tag & "' ", AgL.GCn).ExecuteScalar()

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



        'PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")

        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            '(Case When DP.Prefix Is Not Null Then DP.Prefix || H.ManualRefNo Else H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo End) as InvoiceNo, 
            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, 
                '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, 
                SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, 
                H.DocID, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat,                                 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as DocNo,                 
                (Case When BP.Nature = 'Cash' Then BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'') Else H.SaletoPartyName End) as SaleToPartyName, 
                IfNull(H.SaleToPartyAddress,'') as SaleToPartyAddress, IfNull(C.CityName,'') as CityName, IfNull(H.SaleToPartyPincode,'') as SaleToPartyPincode, 
                IfNull(State.ManualCode,'') as StateCode, IfNull(State.Description,'')  as StateName, 
                IfNull(H.SaleToPartyMobile,'') as SaleToPartyMobile, Sg.ContactPerson, IfNull(H.SaleToPartySalesTaxNo,'') as SaleToPartySalesTaxNo, 
                IfNull(H.SaleToPartyAadharNo,'') as SaleToPartyAadharNo, IfNull(H.SaleToPartyPanNo,'') as SaleToPartyPanNo,
                (Case When BP.Nature = 'Cash' Then IfNull(SP.DispName, BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'')) Else IfNull(SP.DispName,H.SaletoPartyName) End) as ShipToPartyName,
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAddress,'') Else IfNull(Sp.Address,'') End) as ShipToPartyAddress, 
                (Case When SP.DispName Is Null Then IfNull(C.CityName,'') Else IfNull(SC.CityName,'') End) as ShipToPartyCity, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPinCode,'') Else IfNull(Sp.Pin,'') End) as ShipToPartyPincode, 
                (Case When SP.DispName Is Null Then IfNull(State.ManualCode,'') Else IfNull(SS.ManualCode,'') End) as ShipToPartyStateCode, 
                (Case When SP.DispName Is Null Then IfNull(State.Description,'') Else IfNull(SS.Description,'') End) as ShipToPartyStateName, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyMobile,'') Else IfNull(Sp.Mobile,'') End) as ShipToPartyMobile, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartySalesTaxNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'),'') End) as ShipToPartySalesTaxNo, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAadharNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.AadharNo & "'),'') End) as ShipToPartyAadharNo, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPanNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.PanNo & "'),'') End) as ShipToPartyPanNo, 
                 
                
                
                
                (Case when IfNull(I.MaintainStockYn,1) =1 Then abs(L.Qty) Else 0 End) as Qty, (Case when IfNull(I.MaintainStockYn,1) =1 Then L.Rate Else 0 End) as Rate, L.Unit, U.DecimalPlaces as UnitDecimalPlaces, 
                L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, L.AdditionPer, L.AdditionAmount, 
                L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as TotalDiscount, 
                abs(L.Amount)+L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as AmountBeforeDiscount,
                abs(L.Amount) as Amount,Abs(L.Taxable_Amount) as Taxable_Amount,Abs(L.Tax1_Per) as Tax1_Per, abs(L.Tax1) as Tax1, 
                abs(L.Tax2_Per) as Tax2_Per, abs(L.Tax2) as Tax2, abs(L.Tax3_Per) as Tax3_Per, abs(L.Tax3) as Tax3, 
                abs(L.Tax4_Per) as Tax4_Per, abs(L.Tax4) as Tax4, abs(L.Tax5_Per) as Tax5_Per, abs(L.Tax5) as Tax5, 
                abs(L.Net_Amount) as Net_Amount, L.Remark as LRemarks, IfNull(H.Remarks,'') as HRemarks, 
                (Select Sum(L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleInvoiceDetail L1 Where L1.DocID = H.DocID) as H_TotalDiscount, 
                (Select Sum(abs(L1.Amount)+L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleinvoiceDetail L1 Where L1.DocID = H.DocId) as H_AmountBeforeDiscount,
                abs(H.Gross_Amount) as H_Gross_Amount, 
                H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount, Abs(H.Taxable_Amount) as H_Taxable_Amount,
                Abs(H.Tax1_Per) as H_Tax1_Per, Abs(H.Tax1) as H_Tax1, H.Tax2_Per as H_Tax2_Per, abs(H.Tax2) as H_Tax2, 
                H.Tax3_Per as H_Tax3_Per, abs(H.Tax3) as H_Tax3, H.Tax4_Per as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                H.Tax5_Per as H_Tax5_Per, abs(H.Tax5) as H_Tax5, H.Deduction_Per as H_Deduction_Per, H.Deduction as H_Deduction, 
                H.Other_Charge_Per as H_Other_Charge_Per, H.Other_Charge as H_Other_Charge, H.Round_Off, abs(H.Net_Amount) as H_Net_Amount,                 
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle
                from (" & bPrimaryQry & ") as H
                Left Join SaleInvoiceTrnSetting TS  With (NoLock) On H.DocId = TS.DocID
                Left Join SaleInvoiceDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Item I  With (NoLock) On L.Item = I.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join Item IG  With (NoLock) On I.ItemGroup = IG.Code
                Left Join Item IC  With (NoLock) On I.ItemCategory = IC.Code
                Left Join City C  With (NoLock) On H.SaleToPartyCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join SaleInvoiceTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.SaleToParty = Sg.Subcode
                Left Join Subgroup BP With (NoLock) On H.BillToParty = BP.Subcode
                Left Join Subgroup SP With (NoLock) On H.ShipToParty = SP.Subcode
                Left Join City SC With (NoLock) On SP.CityCode = SC.CityCode
                Left Join State SS with (NoLock) On SC.State = SS.Code
                Left Join RateType RT  With (NoLock) on H.RateType = Rt.Code
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join DocumentPrefix DP On VT.Category = DP.Category And H.Div_Code = DP.Div_Code                
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                "

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
            FGetMailConfiguration(objRepPrint, SearchCode)
            'objRepPrint.AttachmentName = "Invoice"
        Else
            objRepPrint = New AgLibrary.RepView(AgL)
        End If


        If mDocReportFileName = "" Then
            FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, "SaleInvoice_Print.rpt", mPrintTitle, , , , TxtPartyName.Tag, TxtV_Date.Text, IsPrintToPrinter)
        Else
            FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, mDocReportFileName, mPrintTitle, , , , TxtPartyName.Tag, TxtV_Date.Text, IsPrintToPrinter)
        End If
    End Sub

    Private Sub FGetMailConfiguration(objRepPrint As Object, SearchCode As String)
        Dim DtMailData As DataTable = AgL.FillData("Select Sg.DispName As DivisionName, 
                    Party.DispName As PartyName, Party.EMail As PartyEMail,
                    Agent.DispName As AgentName, Agent.EMail As AgentEMail
                    From SaleInvoice H 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SubGroup Agent On H.Agent = Agent.SubCode
                    Where H.DocId = '" & SearchCode & "'", AgL.GCn).Tables(0)

        objRepPrint.TxtToEmail.Text = FGetSettings(SettingFields.MailTo, SettingType.General)
        objRepPrint.TxtToEmail.Text = objRepPrint.TxtToEmail.Text.Replace("<PartyEMail>", AgL.XNull(DtMailData.Rows(0)("PartyEMail"))).
                Replace("<AgentEMail>", AgL.XNull(DtMailData.Rows(0)("AgentEMail")))

        objRepPrint.TxtCcEmail.Text = FGetSettings(SettingFields.MailCc, SettingType.General)
        objRepPrint.TxtCcEmail.Text = objRepPrint.TxtCcEmail.Text.Replace("<PartyEMail>", AgL.XNull(DtMailData.Rows(0)("PartyEMail"))).
                Replace("<AgentEMail>", AgL.XNull(DtMailData.Rows(0)("AgentEMail")))

        objRepPrint.TxtSubject.Text =
        objRepPrint.TxtSubject.Text = objRepPrint.TxtSubject.Text.Replace("<PartyName>", AgL.XNull(DtMailData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", TxtReferenceNo.Text).Replace("<EntryDate>", TxtV_Date.Text).
                Replace("<DivisionName>", AgL.XNull(DtMailData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtMailData.Rows(0)("AgentName")))

        objRepPrint.TxtMessage.Text = FGetSettings(SettingFields.MailMessage, SettingType.General)
        objRepPrint.TxtMessage.Text = objRepPrint.TxtMessage.Text.Replace("<PartyName>", AgL.XNull(DtMailData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", TxtReferenceNo.Text).Replace("<EntryDate>", TxtV_Date.Text).
                Replace("<DivisionName>", AgL.XNull(DtMailData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtMailData.Rows(0)("AgentName")))
        objRepPrint.AttachmentName = TxtReferenceNo.Text
    End Sub

    Private Sub FGetPrintSSRS(mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False)
        Dim dsMain As DataTable
        Dim dsCompany As DataTable
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer
        Dim mPrintThisDocId As String
        Dim dtReferenceDocID As DataTable

        If mPrintFor = ClsMain.PrintFor.EMail Then
            PrintingCopies = ("").Split(",")
        Else
            PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")
        End If



        mQry = "Select L.DocId FROM LedgerHeadDetail L  With (NoLock) WHERE ReferenceDocID ='" & mSearchCode & "' GROUP BY L.DocID "
        dtReferenceDocID = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If dtReferenceDocID.Rows.Count <= 0 Then
            mQry = "Select '" & mSearchCode & "' as DocID"
            dtReferenceDocID = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        End If

        mPrintTitle = TxtV_Type.Text

        mQry = ""
        For J = 0 To dtReferenceDocID.Rows.Count - 1
            mPrintThisDocId = dtReferenceDocID.Rows(J)("DocID")
            For I = 1 To PrintingCopies.Length
                If mQry <> "" Then mQry = mQry + " Union All "
                mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, H.DocID, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as VoucherNo,  
                Sg.DispName PartyName, H.PartyAddress, C.CityName as CityName, State.ManualCode as StateCode, State.Description as StateName, 
                H.PartyMobile, Sg.ContactPerson, H.PartySalesTaxNo, (Select RegistrationNo From SubgroupRegistration Where RegistrationType='" & SubgroupRegistrationType.AadharNo & "' And Subcode=H.Subcode) as PartyAadharNo,
                SL.DispName as AccountNameLine, SL.Address as AccountLineAddress, CL.CityName as AccountLineCity, SL.Mobile as AccountLineMobile, L.Specification as LineSpecification, SL.HSN,
                L.SalesTaxGroupItem, STGI.GrossTaxRate, abs(L.Qty) as Qty, L.Rate, L.Unit, U.DecimalPlaces as UnitDecimalPlaces,  
                abs(L.Amount) as Amount,Abs(LC.Taxable_Amount) as Taxable_Amount,Abs(Lc.Tax1_Per) as Tax1_Per, abs(Lc.Tax1) as Tax1, abs(Lc.Tax2_Per) as Tax2_Per, abs(Lc.Tax2) as Tax2, abs(Lc.Tax3_Per) as Tax3_Per, abs(Lc.Tax3) as Tax3, abs(Lc.Tax4_Per) as Tax4_Per, abs(Lc.Tax4) as Tax4, abs(Lc.Tax5_Per) as Tax5_Per, abs(Lc.Tax5) as Tax5, abs(Lc.Net_Amount) as Net_Amount, L.Remarks LRemarks, H.Remarks as HRemarks,
                abs(Hc.Gross_Amount) as H_Gross_Amount, Abs(Hc.Taxable_Amount) as H_Taxable_Amount,Abs(Hc.Tax1_Per) as H_Tax1_Per, Abs(Hc.Tax1) as H_Tax1, 
                Hc.Tax2_Per as H_Tax2_Per, abs(Hc.Tax2) as H_Tax2, Hc.Tax3_Per as H_Tax3_Per, abs(Hc.Tax3) as H_Tax3, Hc.Tax4_Per as H_Tax4_Per, abs(Hc.Tax4) as H_Tax4, 
                Hc.Tax5_Per as H_Tax5_Per, abs(Hc.Tax5) as H_Tax5, Hc.Deduction_Per as H_Deduction_Per, Hc.Deduction as H_Deduction, Hc.Other_Charge_Per as H_Other_Charge_Per, Hc.Other_Charge as H_Other_Charge, Hc.Round_Off, abs(Hc.Net_Amount) as H_Net_Amount, '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                (Select IfNull(Sum(AmtDr),0) - IfNull(Sum(AmtCr),0) As CurrBal From Ledger Where SubCode = SL.Subcode) as Current_Balance, 
                '" & FGetSettings(SettingFields.DocumentPrintShowPartyBalance, SettingType.General) & "' as DocumentPrintShowPartyBalance, 
                '" & AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsCenterAlignedCompanyInfoOnDocuments")) & "' as IsCenterAlignedCompanyInfoOnDocuments,
                '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
                from (Select * From LedgerHead  With (NoLock) Where DocID = '" & mPrintThisDocId & "') as H                
                Left Join LedgerHeadDetail L  With (NoLock) On H.DocID = L.DocID
                Left JOIN LedgerHeadCharges HC  With (NoLock) ON H.DocID = HC.DocId
                Left JOIN LedgerHeadDetailCharges LC  With (NoLock) ON L.DocID = LC.DocId AND L.Sr = LC.Sr
                Left Join Unit U  With (NoLock) On L.Unit = U.Code           
                Left Join City C  With (NoLock) On H.PartyCity = C.CityCode                   
                Left Join Subgroup SL  With (NoLock) On L.Subcode = SL.Subcode
                Left Join City CL  With (NoLock) On SL.CityCode = CL.CityCode
                Left Join State  With (NoLock) On C.State = State.Code                                
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.Subcode = Sg.Subcode
                Left Join Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type                
                "
            Next
        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "

        dsMain = AgL.FillData(mQry, AgL.GCn).Tables(0)





        'FReplaceInvoiceVariables(dsMain, TxtDivision.Tag, TxtSite_Code.Tag)

        dsCompany = ClsMain.GetDocumentHeaderDataTable(TxtDivision.Tag, TxtSite_Code.Tag, TxtV_Type.Tag)

        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            'objRepPrint = New FrmMailCompose(AgL)
            'objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From StockHeadDetail H 
            '        LEFT JOIN SubGroup Sg On H.Subcode = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            'objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H 
            '        LEFT JOIN SubGroup Sg On H.Agent = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            'objRepPrint.AttachmentName = "Invoice"
        Else
            objRepPrint = New FrmRepPrint(AgL)
        End If

        objRepPrint.reportViewer1.Visible = True
        Dim id As Integer = 0
        objRepPrint.reportViewer1.ProcessingMode = ProcessingMode.Local
        If AgL.PubUserName.ToUpper = "SUPER" Then
            dsMain = ClsMain.RemoveNullFromDataTable(dsMain)
            dsCompany = ClsMain.RemoveNullFromDataTable(dsCompany)
            dsMain.WriteXml(AgL.PubReportPath + "\VoucherEntry_DsMain.xml")
            dsCompany.WriteXml(AgL.PubReportPath + "\VoucherEntry_DsCompany.xml")
        End If


        If TxtVoucherCategory.Text.ToUpper = AgLibrary.ClsMain.agConstants.VoucherCategory.Purchase Or TxtVoucherCategory.Text.ToUpper = AgLibrary.ClsMain.agConstants.VoucherCategory.Sales Then
            objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\DebitCreditNote.rdl"
        Else
            objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\VoucherEntry.rdl"
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
    Private Sub FrmLedgerHeadDirect_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = Not FGetRelationalData()
        mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code ='" & TxtDivision.Tag & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code = '" & TxtSite_Code.Tag & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from LedgerHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code Is Null And Site_Code is Null "
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

        If AgL.Dman_Execute("Select Count(*) From Ledger where DocID = '" & mSearchCode & "' And Subcode = '" & TxtPartyName.Tag & "' And Clg_Date Is Not Null ", AgL.GCn).ExecuteScalar > 0 Then
            MsgBox("Some / All lines of this document are reconciled. Can't modify entry")
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("LedgerHead", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        If Not AgL.StrCmp(FDivisionNameForCustomization(11), "MAA VAISHNO") Then
            mQry = " SELECT Count(*) AS Cnt FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "' 
                    AND IfNull(T.IsEditingAllowed,0) = 0 "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
                MsgBox("Some Refrential Entries Exist For This Entry.Can't Modify.", MsgBoxStyle.Information)
                Passed = False
                Exit Sub
            End If
        End If

        TxtPartyName.AgHelpDataSet = Nothing

        ApplyUISetting(LblV_Type.Tag)

        ShowChqRefNo()
        FGetSettingVariableValuesForAddAndEdit()

        If SettingFields_MaximumItemLimit = 1 Then
            Dgl1.AllowUserToAddRows = False
        End If
    End Sub

    Private Sub Dgl1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        FShowCurrBal(e.RowIndex)
        If Dgl1.Item(Col1CurrentBalance, e.RowIndex).Tag IsNot Nothing Then
            FillOutstandingGrid(Dgl1.Item(Col1CurrentBalance, e.RowIndex).Tag)
        Else
            Dgl2.Visible = False
        End If

        If Dgl1.Item(Col1IsRecordLocked, e.RowIndex).Value = 1 Then
            Dgl1.Rows(e.RowIndex).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked
            Dgl1.Rows(e.RowIndex).ReadOnly = True
        End If
    End Sub
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuEditSave.Click, MnuCancelEntry.Click, MnuImportGSTDataFromExcel.Click, MnuImportGSTDataFromDos.Click, MnuReport.Click, MnuPrintCheque.Click, MnuBankFormat.Click, MnuShowLedgerPosting.Click
        Select Case sender.name
            Case MnuImportFromExcel.Name
                FImportFromExcel(ImportFor.Excel)

            Case MnuImportGSTDataFromExcel.Name
                FImportGSTDataFromExcel(ImportFor.Excel)

            Case MnuImportGSTDataFromDos.Name
                FImportGSTDataFromExcel(ImportFor.Dos)

            Case MnuImportFromDos.Name
                FImportFromExcel(ImportFor.Dos)

            Case MnuImportFromTally.Name
                FImportFromTally()

            Case MnuEditSave.Name
                FEditSaveAllEntries()

            Case MnuCancelEntry.Name
                FCancelEntry()

            Case MnuPrintCheque.Name
                'FGetPrintCheque(mSearchCode, False, "")
                Dim StrSenderText As String = "Cheque Printing"
                GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
                GridReportFrm.Filter_IniGrid()
                Dim CRep As ClsChequePrinting = New ClsChequePrinting(GridReportFrm)
                CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                CRep.Ini_Grid()
                GridReportFrm.MdiParent = Me.MdiParent
                GridReportFrm.Show()
                CRep.ProcMain(,, mSearchCode, LblV_Type.Tag)

            Case MnuBankFormat.Name
                ExportToBankFormat()
            Case MnuReport.Name

                Dim StrSenderText As String = "Debit Credit Note Report"
                GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
                GridReportFrm.Filter_IniGrid()
                Dim CRep As ClsReports = New ClsReports(GridReportFrm)
                CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                CRep.Ini_Grid()
                GridReportFrm.MdiParent = Me.MdiParent
                GridReportFrm.Show()
                CRep.ProcDebitCreditNoteReport()

            Case MnuShowLedgerPosting.Name
                FShowLedgerPosting()
        End Select
    End Sub

    Sub ExportToBankFormat()






        Dim mQry As String
        Dim dtTemp As DataTable
        Dim DTB = New DataTable, RWS As Integer, CLS As Integer


        mQry = "SELECT Sg.DispName || (CASE WHEN C.CityName IS NULL THEN '' ELSE ', ' || C.CityName End ) AS Name, BA.BankName, BA.BankAccount, BA.BankIFSC, L.Amount, H.Remarks  
                FROM LedgerHead H
                LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID 
                LEFT JOIN SubgroupBankAccount BA ON L.Subcode = BA.Subcode AND BA.Sr = 0
                LEFT JOIN Subgroup sg ON L.Subcode = Sg.Subcode 
                LEFT JOIN city C ON Sg.CityCode = C.CityCode 
                WHERE H.DocID ='" & mSearchCode & "' Order By L.Sr "
        dtTemp = AgL.FillData(mQry, AgL.GCn).tables(0)




        For CLS = 0 To dtTemp.Columns.Count - 1 ' COLUMNS OF DTB
            'MsgBox(DGV.Columns(CLS).Name.ToString)
            DTB.Columns.Add(dtTemp.Columns(CLS).ColumnName.ToString)
        Next

        Dim DRW As DataRow

        For RWS = 0 To dtTemp.Rows.Count - 1 ' FILL DTB WITH DATAGRIDVIEW
            DRW = DTB.NewRow
            For CLS = 0 To dtTemp.Columns.Count - 1
                DRW(dtTemp.Columns(CLS).ColumnName.ToString) = dtTemp.Rows(RWS)(CLS).ToString
            Next

            DTB.Rows.Add(DRW)
        Next

        DTB.AcceptChanges()


        Dim DST As New DataSet
        DST.Tables.Add(DTB)
        Dim FLE As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\tmp.xml" ' PATH AND FILE NAME WHERE THE XML WIL BE CREATED (EXEMPLE: C:\REPS\XML.xml)
        DTB.WriteXml(FLE)
        Dim EXL As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\excel.exe", "Path", "Key does not exist") ' PATH OF/ EXCEL.EXE IN YOUR MICROSOFT OFFICE
        EXL = EXL & "EXCEL.EXE"
        Shell(Chr(34) & EXL & Chr(34) & " " & Chr(34) & FLE & Chr(34), vbNormalFocus) ' OPEN XML WITH EXCEL
    End Sub

    Public Structure StructLedgerHead
        Dim DocID As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As String
        Dim Div_Code As String
        Dim Site_Code As String
        Dim ManualRefNo As String
        Dim Subcode As String
        Dim SubcodeName As String
        Dim LinkedSubcode As String
        Dim LinkedSubcodeName As String
        Dim DrCr As String
        Dim UptoDate As String
        Dim Remarks As String
        Dim Status As String
        Dim SalesTaxGroupParty As String
        Dim PlaceOfSupply As String
        Dim PartySalesTaxNo As String
        Dim StructureCode As String
        Dim CustomFields As String
        Dim PartyDocNo As String
        Dim PartyDocDate As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim ApproveBy As String
        Dim ApproveDate As String
        Dim MoveToLog As String
        Dim MoveToLogDate As String
        Dim UploadDate As String
        Dim LockText As String
        Dim GenDocId As String
        Dim OMSId As String

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

        '''''''''''''''''''''''''''''''''Line Detail''''''''''''''''''''''''''''''''''
        Dim Line_DocID As String
        Dim Line_Sr As String
        Dim Line_SubCode As String
        Dim Line_SubCodeName As String
        Dim Line_LinkedSubCode As String
        Dim Line_LinkedSubCodeName As String
        Dim Line_SpecificationDocID As String
        Dim Line_SpecificationDocIDSr As String
        Dim Line_ReferenceNo As String
        Dim Line_ReferenceDate As String
        Dim Line_Specification As String
        Dim Line_HSN As String
        Dim Line_SalesTaxGroupItem As String
        Dim Line_Qty As String
        Dim Line_Unit As String
        Dim Line_Rate As String
        Dim Line_Amount As String
        Dim Line_Amount_Cr As String
        Dim Line_ChqRefNo As String
        Dim Line_ChqRefDate As String
        Dim Line_Remarks As String
        Dim Line_OMSId As String

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
    End Structure
    Private Sub FEditSaveAllEntries()
        mFlag_Import = True
        For I As Integer = 0 To DTMaster.Rows.Count - 1
            BMBMaster.Position = I
            MoveRec()
            Topctrl1.FButtonClick(1)
            Calculation()
            Topctrl1.FButtonClick(13)
        Next
        mFlag_Import = False
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

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            If AgL.Dman_Execute("Select Count(*) From Structure Where Code = 'GstSaleTally'", AgL.GCn).ExecuteScalar() = 0 Then
                mQry = "INSERT INTO Structure (Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt,U_AE, ModifiedBy,Edit_Date, UploadDate)
                    Select 'GstSaleTally' Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt,U_AE, ModifiedBy,Edit_Date, UploadDate
                    From Structure Where Code = 'GstSale'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "INSERT INTO StructureDetail (Code, Sr, WEF, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, 
                    DrCr, LineItem, AffectCost, InactiveDate, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, 
                    VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LinePerField, LineAmtField, GridDisplayIndex, UploadDate, Active)
                    Select 'GstSaleTally' As Code, Sr, WEF, Charges, Charge_Type, 
                    Case When Charges = 'RO' Then 'FixedValue Changeable' Else Value_Type End Value_Type, Value, 
                    Case When Charges = 'RO' Then Null Else Calculation End Calculation, 
                    BaseColumn, PostAc, PostAcFromColumn, 
                    DrCr, LineItem, AffectCost, InactiveDate, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, 
                    VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LinePerField, LineAmtField, GridDisplayIndex, UploadDate, Active
                    From StructureDetail Where Code = 'GstSale'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "UPDATE Voucher_Type Set Structure = 'GstSaleTally' Where NCat In ('" & EntryNCat & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            Dim LedgerHeadElementList As XmlNodeList = doc.GetElementsByTagName("VOUCHER")

            For I = 0 To LedgerHeadElementList.Count - 1
                Dim LedgerHeadTableList(0) As StructLedgerHead
                If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST") IsNot Nothing Then
                    For J = 0 To LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Count - 1
                        Dim LedgerHeadTable As New StructLedgerHead

                        LedgerHeadTable.DocID = ""

                        If LedgerHeadElementList(I).SelectSingleNode("VOUCHERTYPENAME") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes.Count > 0 Then
                                If LedgerHeadElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Credit Note" Then
                                    LedgerHeadTable.V_Type = "CNC"
                                ElseIf LedgerHeadElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Debit Note" Then
                                    LedgerHeadTable.V_Type = "DNS"
                                End If
                            End If
                        End If


                        LedgerHeadTable.V_Prefix = ""
                        LedgerHeadTable.Site_Code = AgL.PubSiteCode
                        LedgerHeadTable.Div_Code = AgL.PubDivCode


                        '''''''''''''''''''''''''''''''''''''''''''''''''



                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''








                        If LedgerHeadElementList(I).SelectSingleNode("VOUCHERNUMBER") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes.Count > 0 Then
                                LedgerHeadTable.V_No = LedgerHeadElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes(0).Value.Replace("G", "")
                            End If
                        End If

                        If LedgerHeadElementList(I).SelectSingleNode("DATE") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectSingleNode("DATE").ChildNodes.Count > 0 Then
                                LedgerHeadTable.V_Date = LedgerHeadElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(6, 2) + "/" +
                                            LedgerHeadElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(4, 2) + "/" +
                                            LedgerHeadElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(0, 4)
                            End If
                        End If


                        Dim mManualrefNoPrefix As String = AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix Where V_Type = '" & LedgerHeadTable.V_Type & "' 
                                And " & AgL.Chk_Date(LedgerHeadTable.V_Date) & " >= Date(Date_From) 
                                And " & AgL.Chk_Date(LedgerHeadTable.V_Date) & " <= Date(Date_To) ", AgL.GCn).ExecuteScalar()
                        LedgerHeadTable.ManualRefNo = mManualrefNoPrefix + LedgerHeadTable.V_No.ToString().PadLeft(4).Replace(" ", "0")





                        If LedgerHeadElementList(I).SelectSingleNode("PARTYLEDGERNAME") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectSingleNode("PARTYLEDGERNAME").ChildNodes.Count > 0 Then
                                LedgerHeadTable.SubcodeName = LedgerHeadElementList(I).SelectSingleNode("PARTYLEDGERNAME").ChildNodes(0).Value
                            End If
                        End If

                        If LedgerHeadElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes.Count > 0 Then
                                LedgerHeadTable.SalesTaxGroupParty = LedgerHeadElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value
                            End If
                        End If

                        LedgerHeadTable.PlaceOfSupply = ""
                        LedgerHeadTable.PartySalesTaxNo = ""
                        LedgerHeadTable.StructureCode = ""
                        LedgerHeadTable.CustomFields = ""
                        LedgerHeadTable.PartyDocNo = ""
                        LedgerHeadTable.PartyDocDate = ""

                        LedgerHeadTable.Status = "Active"
                        LedgerHeadTable.EntryBy = AgL.PubUserName
                        LedgerHeadTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                        LedgerHeadTable.ApproveBy = ""
                        LedgerHeadTable.ApproveDate = ""
                        LedgerHeadTable.MoveToLog = ""
                        LedgerHeadTable.MoveToLogDate = ""
                        LedgerHeadTable.UploadDate = ""


                        LedgerHeadTable.Line_Sr = J + 1


                        If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectSingleNode("LEDGERNAME") IsNot Nothing Then
                                If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectSingleNode("LEDGERNAME").ChildNodes.Count > 0 Then
                                    LedgerHeadTable.Line_SubCodeName = LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACCOUNTINGALLOCATIONS.LIST").SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString()
                                End If
                            End If
                        End If

                        'LedgerHeadTable.Line_Subcode = ""
                        LedgerHeadTable.Line_SpecificationDocID = ""
                        LedgerHeadTable.Line_SpecificationDocIDSr = ""
                        LedgerHeadTable.Line_Specification = ""
                        LedgerHeadTable.Line_SalesTaxGroupItem = ""
                        LedgerHeadTable.Line_ChqRefNo = ""
                        LedgerHeadTable.Line_ChqRefDate = ""
                        LedgerHeadTable.Line_Remarks = ""





                        If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY").ChildNodes.Count > 0 Then
                                LedgerHeadTable.Line_Qty = LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("ACTUALQTY").ChildNodes(0).Value.ToString()
                            End If
                        End If


                        If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes.Count > 0 Then
                                LedgerHeadTable.Line_Qty = LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes(0).Value.ToString()


                                Dim bUnitName As String = LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("BILLEDQTY").ChildNodes(0).Value.ToString()
                                If bUnitName.Contains("MTR") Then
                                    LedgerHeadTable.Line_Unit = "Meter"
                                ElseIf bUnitName.Contains("PCS") Then
                                    LedgerHeadTable.Line_Unit = "Pcs"
                                End If
                            End If
                        End If


                        If LedgerHeadTable.Line_Unit = "" Or LedgerHeadTable.Line_Unit Is Nothing Then
                            LedgerHeadTable.Line_Unit = "Pcs"
                        End If


                        If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE").ChildNodes.Count > 0 Then
                                LedgerHeadTable.Line_Rate = LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("RATE").ChildNodes(0).Value
                            End If
                        End If


                        If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes.Count > 0 Then
                                LedgerHeadTable.Line_Amount = Math.Abs(Convert.ToDouble(LedgerHeadElementList(I).SelectNodes("ALLINVENTORYENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            End If
                        End If

                        If Val(LedgerHeadTable.Line_Qty) = 0 And Val(LedgerHeadTable.Line_Rate) = 0 And Val(LedgerHeadTable.Line_Amount) <> 0 Then
                            LedgerHeadTable.Line_Qty = 1
                            LedgerHeadTable.Line_Rate = LedgerHeadTable.Line_Amount
                        End If

                        If Math.Abs(Math.Round((Val(LedgerHeadTable.Line_Amount) / Val(LedgerHeadTable.Line_Qty)) - Val(LedgerHeadTable.Line_Rate), 0)) > 1 Then
                            LedgerHeadTable.Line_Rate = Val(LedgerHeadTable.Line_Amount) / Val(LedgerHeadTable.Line_Qty)
                        End If

                        If LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST") IsNot Nothing Then
                            For K As Integer = 0 To LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST").Count
                                If LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K) IsNot Nothing Then
                                    If LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME") IsNot Nothing Then
                                        If LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes.Count > 0 Then
                                            If LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                        LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("5") Then
                                                LedgerHeadTable.Line_Tax1_Per = 5
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                        LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("2.5") Then
                                                LedgerHeadTable.Line_Tax2_Per = 2.5
                                                LedgerHeadTable.Line_Tax3_Per = 2.5
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                        LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("12") Then
                                                LedgerHeadTable.Line_Tax1_Per = 12
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                        LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("6") Then
                                                LedgerHeadTable.Line_Tax2_Per = 6
                                                LedgerHeadTable.Line_Tax3_Per = 6
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                        LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("18") Then
                                                LedgerHeadTable.Line_Tax1_Per = 18
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                        LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("9") Then
                                                LedgerHeadTable.Line_Tax2_Per = 9
                                                LedgerHeadTable.Line_Tax3_Per = 9
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("IGST") And
                                                        LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("28") Then
                                                LedgerHeadTable.Line_Tax1_Per = 28
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("CGST") And
                                                        LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value.ToString().Contains("14") Then
                                                LedgerHeadTable.Line_Tax2_Per = 14
                                                LedgerHeadTable.Line_Tax3_Per = 14
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "ROUND OFF" Then
                                                LedgerHeadTable.Round_Off = Math.Abs(Convert.ToDouble(LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                            ElseIf LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = "CASH DISCOUNT" Then
                                                'If J = 0 Then
                                                '    If LedgerHeadTable.Line_DiscountAmount = 0 Then
                                                '        LedgerHeadTable.Line_DiscountAmount = Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                '        LedgerHeadTable.Line_Amount = PurchInvoiceTable.Line_Amount - PurchInvoiceTable.Line_DiscountAmount
                                                '    Else
                                                '        LedgerHeadTable.Line_DiscountAmount = PurchInvoiceTable.Line_DiscountAmount + Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                '        LedgerHeadTable.Line_Amount = PurchInvoiceTable.Line_Amount - Math.Abs(Convert.ToDouble(PurchInvoiceElementList(I).SelectNodes("LEDGERENTRIES.LIST")(K).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                                '    End If
                                                'End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        If LedgerHeadTable.Line_Tax1_Per = 5 Or LedgerHeadTable.Line_Tax2_Per = 2.5 Then
                            LedgerHeadTable.Line_SalesTaxGroupItem = "GST 5%"
                        ElseIf LedgerHeadTable.Line_Tax1_Per = 12 Or LedgerHeadTable.Line_Tax2_Per = 6 Then
                            LedgerHeadTable.Line_SalesTaxGroupItem = "GST 12%"
                        ElseIf LedgerHeadTable.Line_Tax1_Per = 18 Or LedgerHeadTable.Line_Tax2_Per = 9 Then
                            LedgerHeadTable.Line_SalesTaxGroupItem = "GST 18%"
                        ElseIf LedgerHeadTable.Line_Tax1_Per = 28 Or LedgerHeadTable.Line_Tax2_Per = 14 Then
                            LedgerHeadTable.Line_SalesTaxGroupItem = "GST 28%"
                        End If


                        If LedgerHeadTable.Line_Tax1_Per > 0 Then
                            LedgerHeadTable.PlaceOfSupply = AgLibrary.ClsMain.agConstants.PlaceOfSupplay.OutsideState
                        Else
                            LedgerHeadTable.PlaceOfSupply = AgLibrary.ClsMain.agConstants.PlaceOfSupplay.WithinState
                        End If


                        If LedgerHeadElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE") IsNot Nothing Then
                            If LedgerHeadElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes.Count > 0 Then
                                If LedgerHeadElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value = "Regular" Then
                                    LedgerHeadTable.SalesTaxGroupParty = "Registered"
                                Else
                                    LedgerHeadTable.SalesTaxGroupParty = LedgerHeadElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value
                                End If
                            End If
                        End If


                        LedgerHeadTable.Line_Gross_Amount = LedgerHeadTable.Line_Amount
                        LedgerHeadTable.Line_Taxable_Amount = LedgerHeadTable.Line_Amount

                        LedgerHeadTable.Line_Tax1 = Math.Round(LedgerHeadTable.Line_Taxable_Amount * LedgerHeadTable.Line_Tax1_Per / 100, 2)
                        LedgerHeadTable.Line_Tax2 = Math.Round(LedgerHeadTable.Line_Taxable_Amount * LedgerHeadTable.Line_Tax2_Per / 100, 2)
                        LedgerHeadTable.Line_Tax3 = Math.Round(LedgerHeadTable.Line_Taxable_Amount * LedgerHeadTable.Line_Tax3_Per / 100, 2)

                        LedgerHeadTable.Line_Tax4_Per = 0
                        LedgerHeadTable.Line_Tax4 = 0
                        LedgerHeadTable.Line_Tax5_Per = 0
                        LedgerHeadTable.Line_Tax5 = 0
                        LedgerHeadTable.Line_SubTotal1 = LedgerHeadTable.Line_Taxable_Amount + LedgerHeadTable.Line_Tax1 + LedgerHeadTable.Line_Tax2 + LedgerHeadTable.Line_Tax3 + LedgerHeadTable.Line_Tax4 + LedgerHeadTable.Line_Tax5
                        LedgerHeadTable.Line_Deduction_Per = 0
                        LedgerHeadTable.Line_Deduction = 0
                        LedgerHeadTable.Line_Other_Charge_Per = 0
                        LedgerHeadTable.Line_Other_Charge = 0
                        LedgerHeadTable.Line_Round_Off = 0
                        LedgerHeadTable.Line_Net_Amount = LedgerHeadTable.Line_SubTotal1


                        LedgerHeadTableList(UBound(LedgerHeadTableList)) = LedgerHeadTable
                        ReDim Preserve LedgerHeadTableList(UBound(LedgerHeadTableList) + 1)
                    Next




                    For J = 0 To LedgerHeadTableList.Length - 1
                        LedgerHeadTableList(0).Gross_Amount += LedgerHeadTableList(J).Line_Gross_Amount
                        LedgerHeadTableList(0).Taxable_Amount += LedgerHeadTableList(J).Line_Taxable_Amount
                        LedgerHeadTableList(0).Tax1_Per += 0
                        LedgerHeadTableList(0).Tax1 += LedgerHeadTableList(J).Line_Tax1
                        LedgerHeadTableList(0).Tax2_Per += 0
                        LedgerHeadTableList(0).Tax2 += LedgerHeadTableList(J).Line_Tax2
                        LedgerHeadTableList(0).Tax3_Per += 0
                        LedgerHeadTableList(0).Tax3 += LedgerHeadTableList(J).Line_Tax3
                        LedgerHeadTableList(0).Tax4_Per += 0
                        LedgerHeadTableList(0).Tax4 += LedgerHeadTableList(J).Line_Tax4
                        LedgerHeadTableList(0).Tax5_Per += 0
                        LedgerHeadTableList(0).Tax5 += LedgerHeadTableList(J).Line_Tax5
                        LedgerHeadTableList(0).SubTotal1 += LedgerHeadTableList(J).Line_SubTotal1
                        LedgerHeadTableList(0).Deduction_Per += 0
                        LedgerHeadTableList(0).Deduction += LedgerHeadTableList(J).Line_Deduction
                        LedgerHeadTableList(0).Other_Charge_Per += 0
                        LedgerHeadTableList(0).Other_Charge += LedgerHeadTableList(J).Line_Other_Charge
                        LedgerHeadTableList(0).Round_Off = 0
                        LedgerHeadTableList(0).Net_Amount += LedgerHeadTableList(J).Line_Net_Amount
                    Next

                    LedgerHeadTableList(0).Deduction = Math.Round(LedgerHeadTableList(0).Deduction, 2)
                    LedgerHeadTableList(0).Other_Charge = Math.Round(LedgerHeadTableList(0).Other_Charge, 2)

                    LedgerHeadTableList(0).Net_Amount = Math.Round(LedgerHeadTableList(0).Net_Amount + LedgerHeadTableList(0).Round_Off, 2)

                    Dim mTallyNetAmount As Double = 0
                    If LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST") IsNot Nothing Then
                        For J = 0 To LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST").Count - 1
                            If LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST").Item(J).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value = LedgerHeadTableList(0).SubcodeName Then
                                mTallyNetAmount = Math.Abs(Convert.ToDouble(LedgerHeadElementList(I).SelectNodes("LEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            End If
                        Next
                    End If

                    If mTallyNetAmount > LedgerHeadTableList(0).Net_Amount Then
                        LedgerHeadTableList(0).Other_Charge += Math.Round(mTallyNetAmount - LedgerHeadTableList(0).Net_Amount, 2)
                    ElseIf mTallyNetAmount < LedgerHeadTableList(0).Net_Amount Then
                        LedgerHeadTableList(0).Deduction += Math.Round(LedgerHeadTableList(0).Net_Amount - mTallyNetAmount, 2)
                    End If

                    LedgerHeadTableList(0).Net_Amount = Math.Round(LedgerHeadTableList(0).Net_Amount + LedgerHeadTableList(0).Other_Charge - LedgerHeadTableList(0).Deduction, 2)

                    InsertLedgerHead(LedgerHeadTableList)
                End If
            Next I

            mQry = "UPDATE Voucher_Type Set Structure = 'GstSale' Where NCat In ('" & EntryNCat & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Shared Function InsertLedgerHead(LedgerHeadTableList As StructLedgerHead()) As String
        Dim mQry As String = ""
        If LedgerHeadTableList(0).V_Type IsNot Nothing Then
            'LedgerHeadTableList(0).DocID = AgL.GetDocId(LedgerHeadTableList(0).V_Type, CStr(LedgerHeadTableList(0).V_No),
            '                                         CDate(LedgerHeadTableList(0).V_Date),
            '                                        IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), LedgerHeadTableList(0).Div_Code, LedgerHeadTableList(0).Site_Code)
            LedgerHeadTableList(0).DocID = AgL.CreateDocId(AgL, "LedgerHead", LedgerHeadTableList(0).V_Type, CStr(LedgerHeadTableList(0).V_No),
                                                     CDate(LedgerHeadTableList(0).V_Date),
                                                    IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), LedgerHeadTableList(0).Div_Code, LedgerHeadTableList(0).Site_Code)

            LedgerHeadTableList(0).V_Prefix = AgL.DeCodeDocID(LedgerHeadTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            LedgerHeadTableList(0).V_No = Val(AgL.DeCodeDocID(LedgerHeadTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))

            If AgL.Dman_Execute("Select Count(*) From LedgerHead With (NoLock) Where V_Type = '" & LedgerHeadTableList(0).V_Type & "'
                        And ManualRefNo = '" & LedgerHeadTableList(0).ManualRefNo & "'
                        And Div_Code = '" & LedgerHeadTableList(0).Div_Code & "'
                        And Site_Code = '" & LedgerHeadTableList(0).Site_Code & "'
                        And V_Prefix = '" & LedgerHeadTableList(0).V_Prefix & "'
                            ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() > 0 Or
                            LedgerHeadTableList(0).ManualRefNo = "" Then
                Dim mManualrefNoPrefix As String = AgL.XNull(AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix With (NoLock) Where V_Type = '" & LedgerHeadTableList(0).V_Type & "' 
                                And " & AgL.Chk_Date(LedgerHeadTableList(0).V_Date) & " >= Date(Date_From) 
                                And " & AgL.Chk_Date(LedgerHeadTableList(0).V_Date) & " <= Date(Date_To) ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
                'LedgerHeadTableList(0).ManualRefNo = mManualrefNoPrefix + LedgerHeadTableList(0).V_No.ToString().PadLeft(4).Replace(" ", "0")
                LedgerHeadTableList(0).ManualRefNo = mManualrefNoPrefix + LedgerHeadTableList(0).V_No.ToString()
            End If

            If LedgerHeadTableList(0).Subcode Is Nothing Or LedgerHeadTableList(0).Subcode = "" Then
                LedgerHeadTableList(0).Subcode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  '" & LedgerHeadTableList(0).SubcodeName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If LedgerHeadTableList(0).LinkedSubcode Is Nothing Or LedgerHeadTableList(0).LinkedSubcode = "" Then
                LedgerHeadTableList(0).LinkedSubcode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  '" & LedgerHeadTableList(0).LinkedSubcodeName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            LedgerHeadTableList(0).StructureCode = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type With (NoLock) Where V_Type = '" & LedgerHeadTableList(0).V_Type & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            If LedgerHeadTableList(0).SalesTaxGroupParty Is Nothing Or LedgerHeadTableList(0).SalesTaxGroupParty = "" Then
                LedgerHeadTableList(0).SalesTaxGroupParty = AgL.Dman_Execute("Select IfNull(SalesTaxPostingGroup,'') From Subgroup With (NoLock) Where SubCode = '" & LedgerHeadTableList(0).Subcode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If LedgerHeadTableList(0).SalesTaxGroupParty Is Nothing Or LedgerHeadTableList(0).SalesTaxGroupParty = "" Then
                LedgerHeadTableList(0).SalesTaxGroupParty = "Unregistered"
            End If

            'If AgL.Dman_Execute("SELECT Count(*) From SaleInvoice where V_Type = '" & LedgerHeadTableList(0).V_Type & "' And ManualRefNo = '" & LedgerHeadTableList(0).ManualRefNo & "' ", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO LedgerHead (DocID,  V_Type, V_Prefix, V_Date, V_No,
                           Div_Code, Site_Code, ManualRefNo, Subcode, LinkedSubcode,
                           DrCr, UptoDate, Remarks, Status, SalesTaxGroupParty, PlaceOfSupply,
                           PartySalesTaxNo, Structure, CustomFields, PartyDocNo, PartyDocDate, EntryBy, EntryDate,
                           ApproveBy, ApproveDate, MoveToLog,
                           MoveToLogDate, OMSId, LockText, GenDocId, UploadDate)
                            Select " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).V_Type) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).V_Prefix) & ",  
                            " & AgL.Chk_Date(LedgerHeadTableList(0).V_Date) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).V_No) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Div_Code) & ",
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Site_Code) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).ManualRefNo) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Subcode) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(0).LinkedSubcode) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(0).DrCr) & ", 
                            " & AgL.Chk_Date(LedgerHeadTableList(0).UptoDate) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Remarks) & ",
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Status) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).SalesTaxGroupParty) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).PlaceOfSupply) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(0).PartySalesTaxNo) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).StructureCode) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).CustomFields) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).PartyDocNo) & ",  
                            " & AgL.Chk_Date(LedgerHeadTableList(0).PartyDocDate) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).EntryBy) & ",    
                            " & AgL.Chk_Date(LedgerHeadTableList(0).EntryDate) & ",    
                            " & AgL.Chk_Text(LedgerHeadTableList(0).ApproveBy) & ",    
                            " & AgL.Chk_Date(LedgerHeadTableList(0).ApproveDate) & ",    
                            " & AgL.Chk_Text(LedgerHeadTableList(0).MoveToLog) & ",    
                            " & AgL.Chk_Date(LedgerHeadTableList(0).MoveToLogDate) & ",
                            " & AgL.Chk_Text(LedgerHeadTableList(0).OMSId) & ",        
                            " & AgL.Chk_Text(LedgerHeadTableList(0).LockText) & ",        
                            " & AgL.Chk_Text(LedgerHeadTableList(0).GenDocId) & ",        
                            " & AgL.Chk_Date(LedgerHeadTableList(0).UploadDate) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            mQry = " INSERT INTO LedgerHeadCharges (DocID,  Gross_Amount,  Taxable_Amount,
                             Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
                             Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
                             Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount)
                             Select  " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ", 
                            " & Val(LedgerHeadTableList(0).Gross_Amount) & ",    
                             " & Val(LedgerHeadTableList(0).Taxable_Amount) & ",    
                             " & Val(LedgerHeadTableList(0).Tax1_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax1) & ",    
                             " & Val(LedgerHeadTableList(0).Tax2_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax2) & ",    
                             " & Val(LedgerHeadTableList(0).Tax3_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax3) & ",    
                             " & Val(LedgerHeadTableList(0).Tax4_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax4) & ",    
                             " & Val(LedgerHeadTableList(0).Tax5_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax5) & ",    
                             " & Val(LedgerHeadTableList(0).SubTotal1) & ",    
                             " & Val(LedgerHeadTableList(0).Deduction_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Deduction) & ",    
                             " & Val(LedgerHeadTableList(0).Other_Charge_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Other_Charge) & ",    
                             " & Val(LedgerHeadTableList(0).Round_Off) & ",    
                             " & Val(LedgerHeadTableList(0).Net_Amount) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            For I As Integer = 0 To LedgerHeadTableList.Length - 1
                If AgL.VNull(LedgerHeadTableList(I).Line_Amount) <> 0 Or AgL.VNull(LedgerHeadTableList(I).Line_Amount_Cr) <> 0 Then
                    If Trim(LedgerHeadTableList(I).SubcodeName) <> Trim(LedgerHeadTableList(I).Line_SubCodeName) Or
                        LedgerHeadTableList(I).Subcode <> LedgerHeadTableList(I).Line_SubCode Then
                        If LedgerHeadTableList(I).Line_SubCode Is Nothing Or LedgerHeadTableList(I).Line_SubCode = "" Then
                            LedgerHeadTableList(I).Line_SubCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  " & AgL.Chk_Text(AgL.XNull(LedgerHeadTableList(I).Line_SubCodeName)) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                        End If

                        If LedgerHeadTableList(I).Line_LinkedSubCode Is Nothing Or LedgerHeadTableList(I).Line_LinkedSubCode = "" Then
                            LedgerHeadTableList(I).Line_LinkedSubCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  " & AgL.Chk_Text(AgL.XNull(LedgerHeadTableList(I).Line_LinkedSubCodeName)) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                        End If

                        mQry = "Insert Into LedgerHeadDetail(DocId, Sr, Subcode, LinkedSubcode, Specification, SalesTaxGroupItem, HSN, " &
                           " Qty, Unit, Rate, Amount, AmountCr, ChqRefNo, ChqRefDate, Remarks, " &
                           " SpecificationDocId, SpecificationDocIdSr, ReferenceNo, ReferenceDate, OMSId)
                            Select " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ", 
                            " & Val(LedgerHeadTableList(I).Line_Sr) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_SubCode) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_LinkedSubCode) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_Specification) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_SalesTaxGroupItem) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_HSN) & ", 
                            " & Val(LedgerHeadTableList(I).Line_Qty) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_Unit) & ", 
                            " & Val(LedgerHeadTableList(I).Line_Rate) & ", 
                            " & Val(LedgerHeadTableList(I).Line_Amount) & ", 
                            " & Val(LedgerHeadTableList(I).Line_Amount_Cr) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_ChqRefNo) & ", 
                            " & AgL.Chk_Date(LedgerHeadTableList(I).Line_ChqRefDate) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_Remarks) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_SpecificationDocID) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_SpecificationDocIDSr) & ",
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_ReferenceNo) & ",
                            " & AgL.Chk_Date(LedgerHeadTableList(I).Line_ReferenceDate) & ",
                            " & AgL.Chk_Text(LedgerHeadTableList(I).Line_OMSId) & "
                            "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                        mQry = "Insert Into LedgerHeadDetailCharges(DocID, Sr,  Gross_Amount,  Taxable_Amount,
                                Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
                                Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
                                Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount)
                                Select " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Sr) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Gross_Amount) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Taxable_Amount) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax1_Per) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax1) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax2_Per) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax2) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax3_Per) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax3) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax4_Per) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax4) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax5_Per) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Tax5) & ", 
                                " & Val(LedgerHeadTableList(I).Line_SubTotal1) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Deduction_Per) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Deduction) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Other_Charge_Per) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Other_Charge) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Round_Off) & ", 
                                " & Val(LedgerHeadTableList(I).Line_Net_Amount) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next
            If mFlag_Import = False Then
                FGetCalculationData(LedgerHeadTableList(0).DocID, AgL.GCn, AgL.ECmd)

                'If AgL.XNull(LedgerHeadTableList(0).DocID) <> "" And AgL.XNull(LedgerHeadTableList(0).Ledger_RecId) <> "" Then
                '    mQry = " UPDATE Ledger Set RecId = '" & LedgerHeadTableList(0).Ledger_RecId & "'
                '            Where DocId = '" & AgL.XNull(LedgerHeadTableList(0).DocID) & "'"
                '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                'End If
            End If

            AgL.UpdateVoucherCounter(LedgerHeadTableList(0).DocID, CDate(LedgerHeadTableList(0).V_Date), AgL.GCn, AgL.ECmd,
                                     LedgerHeadTableList(0).Div_Code, LedgerHeadTableList(0).Site_Code)
        End If
        Return LedgerHeadTableList(0).DocID
    End Function

    Private Sub FCancelEntry()
        Dim FrmObj As New FrmVoucherEntryCancel
        FrmObj.Text = "Voucher Entry Cancel"
        FrmObj.LblDocNo.Text = "Entry No : " + TxtReferenceNo.Text
        FrmObj.DtV_TypeSettings = DtV_TypeSettings

        FrmObj.SearchCode = mSearchCode
        FrmObj.EntryNCat = EntryNCat
        FrmObj.V_Date = TxtV_Date.Text
        FrmObj.Party = TxtPartyName.Tag
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()
        MoveRec()
    End Sub

    Private Sub FrmVouhcerEntry_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        Dim mRemark As String
        Dim dtTemp As DataTable

        If Not AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) = ActionsOfMaximumCashTransactionLimitExceeds.None Then
            If AgL.PubServerName = "" Then
                mQry = "SELECT Group_ConCat(L.recid, ', ') FROM (Select RecID From Ledger With (NoLock) WHERE ReferenceDocId ='" & SearchCode & "' GROUP BY docId, recId) as L "
            Else
                mQry = "SELECT L.recid + ', ' FROM Ledger L With (NoLock) WHERE ReferenceDocId ='" & SearchCode & "' GROUP BY l.docId, L.recId For Xml Path ('')"
            End If

            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If dtTemp.Rows.Count > 0 Then
                mRemark = AgL.XNull(dtTemp.Rows(0)(0))
                mQry = "Update LedgerHead Set Remarks='" & mRemark & "' Where DocID ='" & SearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        End If


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

    Private Sub Dgl1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        If e.RowIndex >= 0 Then
            If Dgl1.Columns(e.ColumnIndex).Name = Col1Amount And Dgl1.Item(Col1Amount, e.RowIndex).Style.ForeColor = Color.Blue Then ShowVoucherEntryCash(e.RowIndex)
        End If
    End Sub

    Private Sub Dgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dgl1.KeyPress
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Amount).Index Then
            If Dgl1.Item(Col1Amount, Dgl1.CurrentCell.RowIndex).Tag IsNot Nothing Then
                If e.KeyChar = Chr(Keys.Space) Then
                    ShowVoucherEntryCash(Dgl1.CurrentCell.RowIndex)
                    e.Handled = True
                End If
            End If
        End If
    End Sub

    Public Sub FImportFromExcel(bImportFor As ImportFor)
        Dim mQry As String = ""
        Dim bHeadSubCodeName As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtLedger As DataTable
        Dim DtLedger_DataFields As DataTable
        Dim DtPurchInvoice As DataTable = Nothing
        Dim DtPurchInvoice_DataFields As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Contra Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Narration") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Dr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Cr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        DtLedger_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        DtPurchInvoice_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As Object
        If bImportFor = ImportFor.Dos Then
            ObjFrmImport = New FrmImportPurchaseFromExcel
            ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
            ObjFrmImport.Dgl2.DataSource = DtPurchInvoice_DataFields
        Else
            ObjFrmImport = New FrmImportFromExcel
            ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
        End If

        ObjFrmImport.Text = "Voucher Entry Import"
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        If bImportFor = ImportFor.Dos Then
            DtLedger = ObjFrmImport.P_DsExcelData_PurchInvoice.Tables(0)
            DtPurchInvoice = ObjFrmImport.P_DsExcelData_PurchInvoiceDetail.Tables(0)
        Else
            DtLedger = ObjFrmImport.P_DsExcelData.Tables(0)
        End If

        mFlag_Import = True

        Dim DtLedger_Original As DataTable = DtLedger
        If bImportFor = ImportFor.Dos Then
            ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
            Dim DtLedger_Filtered As New DataTable
            DtLedger_Filtered = DtLedger.Clone
            Dim DtLedgerRows_Filtered As DataRow() = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('ZD','ZC','ZH','PR','MP','ZR','JV','OB') 
                        And Trim([" & GetFieldAliasName(bImportFor, "Narration") & "]) <> 'DISCOUNT' ")
            For I = 0 To DtLedgerRows_Filtered.Length - 1
                DtLedger_Filtered.ImportRow(DtLedgerRows_Filtered(I))
            Next
            DtLedger = DtLedger_Filtered
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            DtLedger.Columns.Add("File_V_Type")
            For I = 0 To DtLedger.Rows.Count - 1
                DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "File_V_Type")) = DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim
                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZR" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PMT"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZD" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZC" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNC"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "PR" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "VR"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "MP" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "EV"
                End If

                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "ZH" Then
                    If AgL.VNull(DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                        DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                    Else
                        DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNS"
                    End If
                End If

                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")) = "CASH A/C"
                End If

                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name")) = "CASH A/C"
                End If
            Next
        End If





        Dim DtV_Date = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Date"))
        For I = 0 To DtV_Date.Rows.Count - 1
            If AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) <> "" Then
                If CDate(AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))).Year < "2010" Then
                    If ErrorLog.Contains("These Dates are not valid") = False Then
                        ErrorLog += vbCrLf & "These Dates are not valid" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtV_Type = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
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

        Dim DtLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Ledger Account Name"))
        For I = 0 To DtLedgerAccount.Rows.Count - 1
            If AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where LTRIM(RTRIM(Name)) = " & AgL.Chk_Text(AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim()) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Ledger Accounts Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtContraLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))
        For I = 0 To DtContraLedgerAccount.Rows.Count - 1
            If AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where LTRIM(RTRIM(Name)) = " & AgL.Chk_Text(AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString().Trim) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Ledger Accounts Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtLedger_DataFields.Rows.Count - 1
            If AgL.XNull(DtLedger_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtLedger.Columns.Contains(AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString() & ", "
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



            Dim DtLedgerHeader = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"),
                                                                  GetFieldAliasName(bImportFor, "V_No"),
                                                                  GetFieldAliasName(bImportFor, "V_Date"))

            For I = 0 To DtLedgerHeader.Rows.Count - 1
                bHeadSubCodeName = ""
                Dim VoucherEntryTableList(0) As StructLedgerHead
                Dim VoucherEntryTable As New StructLedgerHead


                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.V_Date = AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))
                VoucherEntryTable.V_No = AgL.VNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.ManualRefNo = AgL.VNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                VoucherEntryTable.Subcode = ""
                VoucherEntryTable.SubcodeName = ""


                If VoucherEntryTable.V_Type = "DNS" Or VoucherEntryTable.V_Type = "DNC" Then
                    VoucherEntryTable.DrCr = "Dr"
                ElseIf VoucherEntryTable.V_Type = "CNS" Or VoucherEntryTable.V_Type = "CNC" Then
                    VoucherEntryTable.DrCr = "Cr"
                End If

                If VoucherEntryTable.V_Type = "JV" Or VoucherEntryTable.V_Type = "OB" Then
                    mFlag_Import = False
                Else
                    mFlag_Import = True
                End If



                VoucherEntryTable.UptoDate = ""
                VoucherEntryTable.Remarks = ""
                VoucherEntryTable.Status = "Active"
                VoucherEntryTable.SalesTaxGroupParty = ""
                VoucherEntryTable.PlaceOfSupply = ""
                VoucherEntryTable.PartySalesTaxNo = ""
                VoucherEntryTable.StructureCode = ""
                VoucherEntryTable.CustomFields = ""
                VoucherEntryTable.PartyDocNo = ""
                VoucherEntryTable.PartyDocDate = ""
                VoucherEntryTable.EntryBy = AgL.PubUserName
                VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                VoucherEntryTable.ApproveBy = ""
                VoucherEntryTable.ApproveDate = ""
                VoucherEntryTable.MoveToLog = ""
                VoucherEntryTable.MoveToLogDate = ""
                VoucherEntryTable.UploadDate = ""

                VoucherEntryTable.Gross_Amount = 0
                VoucherEntryTable.Taxable_Amount = 0
                VoucherEntryTable.Tax1_Per = 0
                VoucherEntryTable.Tax1 = 0
                VoucherEntryTable.Tax2_Per = 0
                VoucherEntryTable.Tax2 = 0
                VoucherEntryTable.Tax3_Per = 0
                VoucherEntryTable.Tax3 = 0
                VoucherEntryTable.Tax4_Per = 0
                VoucherEntryTable.Tax4 = 0
                VoucherEntryTable.Tax5_Per = 0
                VoucherEntryTable.Tax5 = 0
                VoucherEntryTable.SubTotal1 = 0
                VoucherEntryTable.Deduction_Per = 0
                VoucherEntryTable.Deduction = 0
                VoucherEntryTable.Other_Charge_Per = 0
                VoucherEntryTable.Other_Charge = 0
                VoucherEntryTable.Round_Off = 0
                VoucherEntryTable.Net_Amount = 0

                Dim DtLedger_ForHeader As New DataTable
                For M = 0 To DtLedger.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLedger.Columns(M).ColumnName
                    DtLedger_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowLedger_ForHeader As DataRow() = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "V_No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))) + " And [" & GetFieldAliasName(bImportFor, "V_Date") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))))
                If DtRowLedger_ForHeader.Length > 0 Then
                    For M = 0 To DtRowLedger_ForHeader.Length - 1
                        DtLedger_ForHeader.Rows.Add()
                        For N = 0 To DtLedger_ForHeader.Columns.Count - 1
                            DtLedger_ForHeader.Rows(M)(N) = DtRowLedger_ForHeader(M)(N)
                        Next
                    Next
                End If

                For J = 0 To DtLedger_ForHeader.Rows.Count - 1
                    If Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("CGST") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("SGST") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("IGST") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("BANK") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("DEDUCTION") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString.Trim.Contains("DEDUCTION") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("ROUND") Then

                        VoucherEntryTable.Line_Sr = J + 1
                        VoucherEntryTable.Line_SubCode = ""
                        VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                        VoucherEntryTable.Line_SpecificationDocID = ""
                        VoucherEntryTable.Line_SpecificationDocIDSr = ""
                        VoucherEntryTable.Line_Specification = ""
                        VoucherEntryTable.Line_SalesTaxGroupItem = ""
                        VoucherEntryTable.Line_Qty = 0
                        VoucherEntryTable.Line_Unit = ""
                        VoucherEntryTable.Line_Rate = 0

                        If VoucherEntryTable.V_Type = "JV" Or VoucherEntryTable.V_Type = "OB" Then
                            VoucherEntryTable.Line_Amount = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr")))
                            VoucherEntryTable.Line_Amount_Cr = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr")))
                        Else
                            If AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                VoucherEntryTable.Line_Amount = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr")))
                            ElseIf AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                VoucherEntryTable.Line_Amount = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr")))
                            End If
                        End If


                        VoucherEntryTable.Line_ChqRefNo = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq No"))).ToString.Trim
                        VoucherEntryTable.Line_ChqRefDate = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq Date"))).ToString.Trim
                        VoucherEntryTable.Line_Remarks = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Narration")))
                        VoucherEntryTable.Line_Gross_Amount = 0
                        VoucherEntryTable.Line_Taxable_Amount = 0
                        VoucherEntryTable.Line_Tax1_Per = 0
                        VoucherEntryTable.Line_Tax1 = 0
                        VoucherEntryTable.Line_Tax2_Per = 0
                        VoucherEntryTable.Line_Tax2 = 0
                        VoucherEntryTable.Line_Tax3_Per = 0
                        VoucherEntryTable.Line_Tax3 = 0
                        VoucherEntryTable.Line_Tax4_Per = 0
                        VoucherEntryTable.Line_Tax4 = 0
                        VoucherEntryTable.Line_Tax5_Per = 0
                        VoucherEntryTable.Line_Tax5 = 0
                        VoucherEntryTable.Line_SubTotal1 = 0
                        VoucherEntryTable.Line_Deduction_Per = 0
                        VoucherEntryTable.Line_Deduction = 0

                        If bImportFor = ImportFor.Dos Then
                            Dim DtRowDiscount As DataRow() = Nothing
                            DtRowDiscount = DtLedger_Original.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [V_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))) +
                                                            " And Trim([" & GetFieldAliasName(bImportFor, "Narration") & "]) = 'DISCOUNT'")
                            If DtRowDiscount IsNot Nothing Then
                                If DtRowDiscount.Length > 0 Then
                                    If AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                        VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Cr")))
                                    ElseIf AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                        VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Dr")))
                                    End If
                                Else
                                    DtRowDiscount = DtLedger_Original.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [V_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))) +
                                                            " And Trim([" & GetFieldAliasName(bImportFor, "Contra Ledger Account Name") & "]) = 'PURCHASE DEDUCTION'")
                                    If DtRowDiscount.Length > 0 Then
                                        If AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                            VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Cr")))
                                        ElseIf AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                            VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Dr")))
                                        End If
                                    End If
                                End If
                            End If


                            Dim DtRowIGST As DataRow() = Nothing
                            DtRowIGST = DtLedger_Original.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [V_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))) +
                                                            " And Trim([" & GetFieldAliasName(bImportFor, "Narration") & "]) = 'DISCOUNT'")
                            If DtRowIGST IsNot Nothing Then
                                If DtRowIGST.Length > 0 Then
                                    If AgL.VNull(DtRowIGST(0)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                        VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowIGST(0)(GetFieldAliasName(bImportFor, "Amt Cr")))
                                    ElseIf AgL.VNull(DtRowIGST(0)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                        VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowIGST(0)(GetFieldAliasName(bImportFor, "Amt Dr")))
                                    End If
                                End If
                            End If
                        End If





                        VoucherEntryTable.Line_Other_Charge_Per = 0
                        VoucherEntryTable.Line_Other_Charge = 0
                        VoucherEntryTable.Line_Round_Off = 0
                        VoucherEntryTable.Line_Net_Amount = 0

                        If bHeadSubCodeName = "" Then
                            If VoucherEntryTable.V_Type = "DNS" Or VoucherEntryTable.V_Type = "DNC" Or VoucherEntryTable.V_Type = "VR" Then
                                If AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                    bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                                End If
                            ElseIf VoucherEntryTable.V_Type = "CNS" Or VoucherEntryTable.V_Type = "CNC" Or VoucherEntryTable.V_Type = "EV" Then
                                If AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                    bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                                End If
                            ElseIf VoucherEntryTable.V_Type = "PMT" Then
                                If AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                    bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString.Trim
                                End If
                            End If
                        End If



                        If DtPurchInvoice IsNot Nothing Then
                            Dim DtRowPurchInvoice_ForHeader As DataRow() = Nothing
                            DtRowPurchInvoice_ForHeader = DtPurchInvoice.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [V_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))))
                            If DtRowPurchInvoice_ForHeader IsNot Nothing Then
                                If DtRowPurchInvoice_ForHeader.Length > 0 Then VoucherEntryTable.Remarks = DtRowPurchInvoice_ForHeader(0)("fv_no")
                            End If
                        End If

                        VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                        ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
                    End If
                Next


                For J = 0 To VoucherEntryTableList.Length - 1
                    If bHeadSubCodeName <> "" Then
                        VoucherEntryTableList(J).SubcodeName = bHeadSubCodeName
                    End If
                Next
                InsertLedgerHead(VoucherEntryTableList)
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
                    bAliasName = "V_NO"
                Case "V_Date"
                    bAliasName = "V_DATE"
                Case "Ledger Account Name"
                    bAliasName = "ledgername"
                Case "Contra Ledger Account Name"
                    bAliasName = "contraname"
                Case "Narration"
                    bAliasName = "narration"
                Case "Chq No"
                    bAliasName = "chq_no"
                Case "Chq Date"
                    bAliasName = "chq_date"
                Case "Amt Dr"
                    bAliasName = "amt_dr"
                Case "Amt Cr"
                    bAliasName = "amt_cr"




                Case "Party Name"
                    bAliasName = "vendor"
                Case "Line Ledger Account Name"
                    bAliasName = "item_name"
                Case "Entry No"
                    bAliasName = "V_No"
                Case "SubTotal1"
                    bAliasName = "SUBTOTAL1"
                Case "Deduction_Per"
                    bAliasName = "DED_PER"
                Case "Deduction"
                    bAliasName = "DEDUCTION"
                Case "Other_Charge_Per"
                    bAliasName = "OT_CH_PER"
                Case "Other_Charge"
                    bAliasName = "OT_CHARGE"
                Case "Round_Off"
                    bAliasName = "ROUND_OFF"
                Case "Net_Amount"
                    bAliasName = "NET_AMOUNT"
                Case "Gross_Amount"
                    bAliasName = "GROSS_AMT"
                Case "Taxable_Amount"
                    bAliasName = "TAXABLEAMT"
                Case "Tax1_Per"
                    bAliasName = "TAX1_PER"
                Case "Tax1"
                    bAliasName = "TAX1"
                Case "Tax2_Per"
                    bAliasName = "TAX2_PER"
                Case "Tax2"
                    bAliasName = "TAX2"
                Case "Tax3_Per"
                    bAliasName = "TAX3_PER"
                Case "Tax3"
                    bAliasName = "TAX3"
                Case "Tax4_Per"
                    bAliasName = "TAX4_PER"
                Case "Tax4"
                    bAliasName = "TAX4"
                Case "Tax5_Per"
                    bAliasName = "TAX5_PER"
                Case "Tax5"
                    bAliasName = "TAX5"
            End Select

            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function

    Public Sub FImportGSTDataFromExcel(bImportFor As ImportFor)
        Dim mQry As String = ""
        Dim bHeadSubCodeName As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtLedgerHead As DataTable
        Dim DtLedgerHead_DataFields As DataTable
        Dim DtLedgerHeadDetail As DataTable = Nothing
        Dim DtLedgerHeadDetail_DataFields As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Entry No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Party Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "SubTotal1") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deduction_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Deduction") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Other_Charge_Per") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Other_Charge") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Round_Off") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Net_Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "


        DtLedgerHead_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Entry No") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Line Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Specification") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Qty") & "' as [Field Name], 'Text' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Unit") & "' as [Field Name], 'Text' as [Data Type], 10 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Rate") & "' as [Field Name], 'Text' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amount") & "' as [Field Name], 'Text' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq No") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq Date") & "' as [Field Name], 'Date' as [Data Type], NUll as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
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
        DtLedgerHeadDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As FrmImportPurchaseFromExcel
        ObjFrmImport = New FrmImportPurchaseFromExcel
        ObjFrmImport.Dgl1.DataSource = DtLedgerHead_DataFields
        ObjFrmImport.Dgl2.DataSource = DtLedgerHeadDetail_DataFields
        ObjFrmImport.Text = "Voucher Entry Import"
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtLedgerHead = ObjFrmImport.P_DsExcelData_PurchInvoice.Tables(0)
        DtLedgerHeadDetail = ObjFrmImport.P_DsExcelData_PurchInvoiceDetail.Tables(0)

        mFlag_Import = True



        If bImportFor = ImportFor.Dos Then
            ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
            Dim DtLedger_Filtered As New DataTable
            DtLedger_Filtered = DtLedgerHead.Clone
            Dim DtLedgerRows_Filtered As DataRow() = DtLedgerHead.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('ZD','ZC','ZH','GD')")
            For I = 0 To DtLedgerRows_Filtered.Length - 1
                DtLedger_Filtered.ImportRow(DtLedgerRows_Filtered(I))
            Next
            DtLedgerHead = DtLedger_Filtered
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            DtLedgerHead.Columns.Add("File_V_Type")
            For I = 0 To DtLedgerHead.Rows.Count - 1
                DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "File_V_Type")) = DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim
                If DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZD" Then
                    DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                ElseIf DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZC" Then
                    DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNC"
                End If

                If DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "ZH" Then
                    If AgL.VNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                        DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                    Else
                        DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNS"
                    End If
                End If


                If DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "GD" Then
                    DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                End If


            Next


            For I = 0 To DtLedgerHeadDetail.Rows.Count - 1
                If DtLedgerHeadDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "GD" Then
                    DtLedgerHeadDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                End If
            Next
        End If


        Dim DtV_Date = DtLedgerHead.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Date"))
        For I = 0 To DtV_Date.Rows.Count - 1
            If AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) <> "" Then
                If CDate(AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))).Year < "2010" Then
                    If ErrorLog.Contains("These Dates are not valid") = False Then
                        ErrorLog += vbCrLf & "These Dates are not valid" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtV_Type = DtLedgerHead.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
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

        Dim DtParty = DtLedgerHead.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Party Name"))
        For I = 0 To DtParty.Rows.Count - 1
            If AgL.XNull(DtParty.Rows(I)(GetFieldAliasName(bImportFor, "Party Name"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = " & AgL.Chk_Text(AgL.XNull(DtParty.Rows(I)(GetFieldAliasName(bImportFor, "Party Name"))).ToString().Trim()) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtParty.Rows(I)(GetFieldAliasName(bImportFor, "Party Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtParty.Rows(I)(GetFieldAliasName(bImportFor, "Party Name"))) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtLedgerHead_DataFields.Rows.Count - 1
            If AgL.XNull(DtLedgerHead_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtLedgerHead.Columns.Contains(AgL.XNull(DtLedgerHead_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedgerHead_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedgerHead_DataFields.Rows(I)("Field Name")).ToString() & ", "
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


            For I = 0 To DtLedgerHead.Rows.Count - 1
                bHeadSubCodeName = ""
                Dim VoucherEntryTableList(0) As StructLedgerHead
                Dim VoucherEntryTable As New StructLedgerHead


                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.V_Date = AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))
                VoucherEntryTable.V_No = AgL.VNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.ManualRefNo = AgL.VNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "Entry No")))
                VoucherEntryTable.Subcode = ""
                VoucherEntryTable.SubcodeName = AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "Party Name")))


                If VoucherEntryTable.V_Type = "DNS" Or VoucherEntryTable.V_Type = "DNC" Then
                    VoucherEntryTable.DrCr = "Dr"
                ElseIf VoucherEntryTable.V_Type = "CNS" Or VoucherEntryTable.V_Type = "CNC" Then
                    VoucherEntryTable.DrCr = "Cr"
                End If


                VoucherEntryTable.UptoDate = ""
                VoucherEntryTable.Remarks = ""
                VoucherEntryTable.Status = "Active"
                VoucherEntryTable.SalesTaxGroupParty = ""
                VoucherEntryTable.PlaceOfSupply = ""
                VoucherEntryTable.PartySalesTaxNo = ""
                VoucherEntryTable.StructureCode = ""
                VoucherEntryTable.CustomFields = ""
                VoucherEntryTable.PartyDocNo = ""
                VoucherEntryTable.PartyDocDate = ""
                VoucherEntryTable.EntryBy = AgL.PubUserName
                VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                VoucherEntryTable.ApproveBy = ""
                VoucherEntryTable.ApproveDate = ""
                VoucherEntryTable.MoveToLog = ""
                VoucherEntryTable.MoveToLogDate = ""
                VoucherEntryTable.UploadDate = ""

                VoucherEntryTable.Gross_Amount = 0
                VoucherEntryTable.Taxable_Amount = 0
                VoucherEntryTable.Tax1_Per = 0
                VoucherEntryTable.Tax1 = 0
                VoucherEntryTable.Tax2_Per = 0
                VoucherEntryTable.Tax2 = 0
                VoucherEntryTable.Tax3_Per = 0
                VoucherEntryTable.Tax3 = 0
                VoucherEntryTable.Tax4_Per = 0
                VoucherEntryTable.Tax4 = 0
                VoucherEntryTable.Tax5_Per = 0
                VoucherEntryTable.Tax5 = 0
                VoucherEntryTable.SubTotal1 = 0
                VoucherEntryTable.Deduction_Per = 0
                VoucherEntryTable.Deduction = 0
                VoucherEntryTable.Other_Charge_Per = 0
                VoucherEntryTable.Other_Charge = 0
                VoucherEntryTable.Round_Off = 0
                VoucherEntryTable.Net_Amount = 0

                Dim DtLedgerHeadDetail_ForHeader As New DataTable
                For M = 0 To DtLedgerHeadDetail.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLedgerHeadDetail.Columns(M).ColumnName
                    DtLedgerHeadDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowLedgerHeadDetail_ForHeader As DataRow() = DtLedgerHeadDetail.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHead.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "Entry No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "Entry No")))))
                If DtRowLedgerHeadDetail_ForHeader.Length > 0 Then
                    For M = 0 To DtRowLedgerHeadDetail_ForHeader.Length - 1
                        DtLedgerHeadDetail_ForHeader.Rows.Add()
                        For N = 0 To DtLedgerHeadDetail_ForHeader.Columns.Count - 1
                            DtLedgerHeadDetail_ForHeader.Rows(M)(N) = DtRowLedgerHeadDetail_ForHeader(M)(N)
                        Next
                    Next
                End If

                For J = 0 To DtLedgerHeadDetail_ForHeader.Rows.Count - 1
                    VoucherEntryTable.Line_Sr = J + 1
                    VoucherEntryTable.Line_SubCode = ""
                    VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Line Ledger Account Name"))).ToString.Trim
                    VoucherEntryTable.Line_SpecificationDocID = ""
                    VoucherEntryTable.Line_SpecificationDocIDSr = ""
                    VoucherEntryTable.Line_Specification = ""
                    VoucherEntryTable.Line_SalesTaxGroupItem = ""
                    VoucherEntryTable.Line_Qty = 0
                    VoucherEntryTable.Line_Unit = ""
                    VoucherEntryTable.Line_Rate = 0

                    VoucherEntryTable.Line_Amount = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amount")))

                    'VoucherEntryTable.Line_ChqRefNo = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq No"))).ToString.Trim
                    '                    VoucherEntryTable.Line_ChqRefDate = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq Date"))).ToString.Trim
                    VoucherEntryTable.Line_Remarks = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Remark")))
                    VoucherEntryTable.Line_Gross_Amount = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Gross_Amount")))
                    VoucherEntryTable.Line_Taxable_Amount = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Taxable_Amount")))
                    VoucherEntryTable.Line_Tax1_Per = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax1_Per")))
                    VoucherEntryTable.Line_Tax1 = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax1")))
                    VoucherEntryTable.Line_Tax2_Per = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax2_Per")))
                    VoucherEntryTable.Line_Tax2 = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax2")))
                    VoucherEntryTable.Line_Tax3_Per = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax3_Per")))
                    VoucherEntryTable.Line_Tax3 = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax3")))
                    VoucherEntryTable.Line_Tax4_Per = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax4_Per")))
                    VoucherEntryTable.Line_Tax4 = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax4")))
                    VoucherEntryTable.Line_Tax5_Per = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax5_Per")))
                    VoucherEntryTable.Line_Tax5 = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Tax5")))
                    VoucherEntryTable.Line_SubTotal1 = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "SubTotal1")))
                    VoucherEntryTable.Line_Deduction_Per = 0
                    VoucherEntryTable.Line_Deduction = 0
                    VoucherEntryTable.Line_Other_Charge_Per = 0
                    VoucherEntryTable.Line_Other_Charge = 0
                    VoucherEntryTable.Line_Round_Off = 0
                    VoucherEntryTable.Line_Net_Amount = 0

                    VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                    ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
                Next

                InsertLedgerHead(VoucherEntryTableList)
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
    Public Shared Sub FGetCalculationData(mSearchCode As String, Conn As Object, Cmd As Object)
        Dim mQry As String = ""
        mQry = "SELECT H.Structure, Sd.* 
                FROM LedgerHead H With (NoLock)
                LEFT JOIN StructureDetail Sd With (NoLock) ON H.Structure = Sd.Code
                WHERE H.DocID = '" & mSearchCode & "'"
        Dim DtCalcHeaderData As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

        If AgL.XNull(DtCalcHeaderData.Rows(0)("Structure")) <> "" Then
            mQry = "Select H.*, (Select Max(Remarks) From LedgerHeadDetail With (NoLock) Where DocId = H.DocId) as LineRemarks From LedgerHead H With (NoLock) Where H.DocId = '" & mSearchCode & "'"
            Dim DtTransactionDetail As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            For I As Integer = 0 To DtCalcHeaderData.Rows.Count - 1
                For J As Integer = 0 To DtTransactionDetail.Columns.Count - 1
                    If DtCalcHeaderData.Rows(I)("HeaderAmtField") = DtTransactionDetail.Columns(J).ColumnName Then
                        DtCalcHeaderData.Rows(I)("Amount") = DtTransactionDetail.Rows(0)(DtTransactionDetail.Columns(J).ColumnName)
                    End If
                Next
            Next

            Dim bProcess As String = ""
            If AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) = "DNC" Or AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")) = "CNC" Then
                bProcess = "SALES"
            Else
                bProcess = "PURCH"
            End If


            mQry = " SELECT H.SalesTaxGroupParty, H.PlaceOfSupply, L.SalesTaxGroupItem,
                Sd.Charges, Sd.PostAcFromColumn, L.SubCode As LineSubCode, Pst.*
                FROM LedgerHead H With (NoLock)
                LEFT JOIN LedgerHeadDetail L With (NoLock) ON H.DocID = L.DocID
                LEFT JOIN PostingGroupSalesTax Pst With (NoLock) ON H.SalesTaxGroupParty = Pst.PostingGroupSalesTaxParty
	                AND H.PlaceOfSupply = Pst.PlaceOfSupply
	                AND L.SalesTaxGroupItem = Pst.PostingGroupSalesTaxItem
	                AND Pst.Process = '" & bProcess & "'
                LEFT JOIN StructureDetail Sd With (NoLock) ON H.Structure = Sd.Code
	                AND Pst.ChargeType = Sd.Charge_Type
                WHERE H.DocID = '" & mSearchCode & "'"
            Dim DtPostingGroupSalesTax As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)



            mQry = "Select "
            For I As Integer = 0 To DtCalcHeaderData.Rows.Count - 1
                mQry += AgL.XNull(DtCalcHeaderData.Rows(I)("LineAmtField")) + " As [" + GetColName(DtCalcHeaderData.Rows(I)("Charges")) + "],"
                mQry += " 0.00  As [" + GetColNamePer(DtCalcHeaderData.Rows(I)("Charges")) + "],"
                mQry += " '' As [" + GetColNamePostAc(DtCalcHeaderData.Rows(I)("Charges")) + "],"
                mQry += AgL.Chk_Text(AgL.XNull(DtCalcHeaderData.Rows(I)("ContraAc"))) + " As [" + GetColNameContraAc(DtCalcHeaderData.Rows(I)("Charges")) + "]" + IIf(I = DtCalcHeaderData.Rows.Count - 1, "", ",")
            Next
            mQry += " From LedgerHeadDetailCharges With (NoLock) Where DocId = '" & mSearchCode & "'"
            Dim DtCalcLineData As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            For I As Integer = 0 To DtCalcLineData.Rows.Count - 1
                For J As Integer = 0 To DtCalcLineData.Columns.Count - 1
                    For K As Integer = 0 To DtPostingGroupSalesTax.Rows.Count - 1
                        If AgL.XNull(DtPostingGroupSalesTax.Rows(K)("Charges")) <> "" Then
                            If DtCalcLineData.Columns(J).ColumnName = GetColNamePostAc(AgL.XNull(DtPostingGroupSalesTax.Rows(K)("Charges"))) Then
                                DtCalcLineData.Rows(I)(J) = AgL.XNull(DtPostingGroupSalesTax.Rows(K)("LedgerAc"))
                            ElseIf DtCalcLineData.Columns(J).ColumnName = GetColNamePer(AgL.XNull(DtPostingGroupSalesTax.Rows(K)("Charges"))) Then
                                DtCalcLineData.Rows(I)(J) = AgL.VNull(DtPostingGroupSalesTax.Rows(K)("Percentage"))
                            End If
                        Else
                            If AgL.XNull(DtPostingGroupSalesTax.Rows(K)("ChargeType")) = "TAXABLE AMOUNT" Then
                                If DtCalcLineData.Columns(J).ColumnName = "STTAPostAc" Then
                                    DtCalcLineData.Rows(I)(J) = AgL.XNull(DtPostingGroupSalesTax.Rows(K)("LineSubCode"))
                                End If
                            End If
                        End If
                    Next
                Next
            Next


            Dim mMultiplyWithMinus As Boolean = False
            Dim mNarrationParty As String
            Dim mNarration As String
            If DtTransactionDetail.Rows(0)("ManualRefNo") <> "" Then
                mNarrationParty = AgL.XNull(DtTransactionDetail.Rows(0)("LineRemarks")) & " : " & DtTransactionDetail.Rows(0)("ManualRefNo") & " Dated " & DtTransactionDetail.Rows(0)("V_Date")
                mNarration = AgL.XNull(DtTransactionDetail.Rows(0)("LineRemarks")) & " : " & DtTransactionDetail.Rows(0)("PartyName") & " ReferenceNo No. " & DtTransactionDetail.Rows(0)("ManualRefNo") & " Dated " & DtTransactionDetail.Rows(0)("V_Date")
            Else
                mNarrationParty = AgL.XNull(DtTransactionDetail.Rows(0)("LineRemarks"))
                mNarration = AgL.XNull(DtTransactionDetail.Rows(0)("LineRemarks")) & " : " & DtTransactionDetail.Rows(0)("PartyName") & ""
            End If
            mMultiplyWithMinus = False

            ClsMain.PostStructureLineToAccounts(DtCalcHeaderData, DtCalcLineData, mNarrationParty, mNarration, mSearchCode, AgL.XNull(DtTransactionDetail.Rows(0)("Div_Code")),
                                        AgL.XNull(DtTransactionDetail.Rows(0)("Site_Code")),
                                        AgL.XNull(DtTransactionDetail.Rows(0)("V_Type")), AgL.XNull(DtTransactionDetail.Rows(0)("V_Prefix")), AgL.VNull(DtTransactionDetail.Rows(0)("V_No")),
                                        AgL.XNull(DtTransactionDetail.Rows(0)("ManualRefNo")), AgL.XNull(DtTransactionDetail.Rows(0)("SubCode")),
                                        AgL.XNull(DtTransactionDetail.Rows(0)("V_Date")), Conn, Cmd,, mMultiplyWithMinus, AgL.XNull(DtTransactionDetail.Rows(0)("LinkedSubCode")))
        Else
            Dim mHeaderAccountDrCr As String = ""
            Dim mNarration As String = ""
            Dim mLedgerPostingData As String = ""
            Dim DtTemp As DataTable

            mQry = "Select * From LedgerHead With (NoLock) Where DocId = '" & mSearchCode & "'"
            Dim DtTransactionHead As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            mQry = "Select Sg.Name As PartyName, L.* 
                    From LedgerHeadDetail L With (NoLock) 
                    LEFT JOIN SubGroup Sg With (NoLock) On L.SubCode = Sg.SubCode
                    Where L.DocId = '" & mSearchCode & "'"
            Dim DtTransactionHeadDetail As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)


            mQry = "Select HeaderAccountDrCr From Voucher_Type with (NoLock) Where V_Type = '" & AgL.XNull(DtTransactionHead.Rows(0)("V_Type")) & "'"
            mHeaderAccountDrCr = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).executeScalar)

            'If mHeaderAccountDrCr.ToUpper <> "DR" And mHeaderAccountDrCr.ToUpper <> "CR" Then Exit Sub

            If mHeaderAccountDrCr.ToUpper <> "DR" And mHeaderAccountDrCr.ToUpper <> "CR" Then
                If AgL.XNull(DtTransactionHead.Rows(0)("V_Type")) = "CR" Then
                    mHeaderAccountDrCr = "DR"
                ElseIf AgL.XNull(DtTransactionHead.Rows(0)("V_Type")) = "PS" Then
                    mHeaderAccountDrCr = "DR"
                End If
            End If


            Dim bTableName As String = "[" + Guid.NewGuid().ToString() + "]"

            If AgL.IsTableExist(bTableName.Replace("[", "").Replace("]", ""), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)) Then
                mQry = "Drop Table " + bTableName
                AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
            End If

            mQry = " CREATE TABLE " & bTableName & "(Subcode NVARCHAR(10), LinkedSubcode NVARCHAR(10), ContraAc NVARCHAR(10), 
                            AmtDr Float, AmtCr Float, Narration NVARCHAR(255), 
                            ChqNo NVARCHAR(20), ChqDate DateTime, EffectiveDate DateTime, 
                            ReferenceNo NVARCHAR(20), ReferenceDate DateTime) "
            AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))

            If mHeaderAccountDrCr = "" Or AgL.StrCmp(mHeaderAccountDrCr, "N/A") = True Then
                'For Journal Entry And Opening Balance
                For I As Integer = 0 To DtTransactionHeadDetail.Rows.Count - 1
                    If AgL.VNull(DtTransactionHeadDetail.Rows(I)("Amount")) <> 0 Or
                        AgL.VNull(DtTransactionHeadDetail.Rows(I)("AmountCr")) <> 0 Then
                        mNarration = AgL.XNull(DtTransactionHead.Rows(0)("V_Type")) & " : " & AgL.XNull(DtTransactionHead.Rows(0)("Remarks"))

                        'If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                        mLedgerPostingData = " INSERT INTO " & bTableName & "(Subcode, LinkedSubcode, ContraAc, 
                            AmtDr, AmtCr, Narration, ChqNo, ChqDate, EffectiveDate, ReferenceNo, ReferenceDate)"
                        mLedgerPostingData += " Select " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("SubCode"))) & " As Subcode, 
                            " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("LinkedSubcode"))) & " as LinkedSubcode, 
                            Null as ContraAc, 
                            " & AgL.VNull(DtTransactionHeadDetail.Rows(I)("Amount")) & " as AmtDr, 
                            " & AgL.VNull(DtTransactionHeadDetail.Rows(I)("AmountCr")) & " as AmtCr, 
                            " & AgL.Chk_Text(mNarration) & " as Narration, 
                            " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ChqRefNo"))) & " as ChqNo, 
                            " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ChqRefDate"))) & " as ChqDate, 
                            " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("EffectiveDate"))) & " as EffectiveDate,
                            " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ReferenceNo"))) & " as ReferenceNo,
                            " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ReferenceDate"))) & " as ReferenceDate "
                        AgL.Dman_ExecuteNonQry(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
                    End If
                Next
            Else
                'For Debit Note, Credit Note, Expense Voucher, Income Voucher
                For I As Integer = 0 To DtTransactionHeadDetail.Rows.Count - 1
                    If AgL.VNull(DtTransactionHeadDetail.Rows(I)("Amount")) <> 0 Then
                        mNarration = AgL.XNull(DtTransactionHead.Rows(0)("V_Type")) & " : " & AgL.XNull(DtTransactionHead.Rows(0)("PartyName")) & ". " & AgL.XNull(DtTransactionHead.Rows(0)("Remarks"))

                        'If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                        mLedgerPostingData = " INSERT INTO " & bTableName & "(Subcode, LinkedSubcode, ContraAc, 
                            AmtDr, AmtCr, Narration, ChqNo, ChqDate, EffectiveDate, ReferenceNo, ReferenceDate)"
                        mLedgerPostingData += " Select " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("SubCode"))) & " As Subcode, 
                            " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("LinkedSubcode"))) & " as LinkedSubcode, 
                            " & AgL.Chk_Text(AgL.XNull(DtTransactionHead.Rows(0)("SubCode"))) & "  as ContraAc, 
                            " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(AgL.VNull(DtTransactionHeadDetail.Rows(I)("Amount"))), 0) & " as AmtDr, 
                            " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(AgL.VNull(DtTransactionHeadDetail.Rows(I)("Amount"))), 0) & " as AmtCr, 
                            " & AgL.Chk_Text(mNarration) & " as Narration, 
                            " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ChqRefNo"))) & " as ChqNo, 
                            " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ChqRefDate"))) & " as ChqDate, 
                            " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("EffectiveDate"))) & " as EffectiveDate,
                            " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ReferenceNo"))) & " as ReferenceNo,
                            " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ReferenceDate"))) & " as ReferenceDate "
                        AgL.Dman_ExecuteNonQry(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))

                        mNarration = AgL.XNull(DtTransactionHead.Rows(0)("V_Type")) & " : " & AgL.XNull(DtTransactionHeadDetail.Rows(I)("PartyName")) & ". " & AgL.XNull(DtTransactionHeadDetail.Rows(I)("Remarks"))

                        'If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                        mLedgerPostingData = " INSERT INTO " & bTableName & "(Subcode, LinkedSubcode, ContraAc, 
                            AmtDr, AmtCr, Narration, ChqNo, ChqDate, EffectiveDate, ReferenceNo, ReferenceDate)"
                        mLedgerPostingData += " Select " & AgL.Chk_Text(AgL.XNull(DtTransactionHead.Rows(0)("SubCode"))) & " as Subcode, 
                                " & AgL.Chk_Text(AgL.XNull(DtTransactionHead.Rows(0)("LinkedSubCode"))) & " as LinkedSubcode, " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("SubCode"))) & "  as ContraAc, 
                                " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(AgL.VNull(DtTransactionHeadDetail.Rows(I)("Amount"))), 0) & " as AmtDr, 
                                " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(AgL.VNull(DtTransactionHeadDetail.Rows(I)("Amount"))), 0) & " as AmtCr, 
                                " & AgL.Chk_Text(mNarration) & " as Narration, 
                                " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ChqRefNo"))) & " as ChqNo, 
                                " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ChqRefDate"))) & " as ChqDate, 
                                " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("EffectiveDate"))) & " as EffectiveDate,
                                " & AgL.Chk_Text(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ReferenceNo"))) & " as ReferenceNo,
                                " & AgL.Chk_Date(AgL.XNull(DtTransactionHeadDetail.Rows(I)("ReferenceDate"))) & " as ReferenceDate "
                        AgL.Dman_ExecuteNonQry(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
                    End If
                Next
            End If



            'If mLedgerPostingData = "" Then Exit Sub
            mLedgerPostingData = " Select * From " & bTableName & " With (NoLock) "

            mLedgerPostingData = "Select SubCode, LinkedSubcode, ContraAc, Narration, AmtDr*1.0 as AmtDr, AmtCr*1.0 as AmtCr, ChqNo, ChqDate, EffectiveDate, ReferenceNo, ReferenceDate 
                              From (" & mLedgerPostingData & ") as X  "
            DtTemp = AgL.FillData(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                For I As Integer = 0 To DtTemp.Rows.Count - 1
                    mQry = "INSERT INTO Ledger
                        (DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, 
                        V_Date, SubCode, LinkedSubcode, ContraSub, AmtDr, AmtCr, 
                        Chq_No, Chq_Date, EffectiveDate, Narration, Site_Code, DivCode, 
                        U_Name, U_EntDt, U_AE)
                        VALUES(" & AgL.Chk_Text(AgL.XNull(DtTransactionHead.Rows(0)("DocId"))) & ", " & I + 1 & ", 
                        " & Val(AgL.VNull(DtTransactionHead.Rows(0)("V_No"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtTransactionHead.Rows(0)("V_Type"))) & ", 
                        " & AgL.Chk_Text(IIf(AgL.XNull(DtTemp.Rows(I)("ReferenceNo")) <> "", AgL.XNull(DtTemp.Rows(I)("ReferenceNo")), AgL.XNull(DtTransactionHead.Rows(0)("ManualRefNo")))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtTransactionHead.Rows(0)("V_Prefix"))) & ",
                        " & AgL.Chk_Date(IIf(AgL.XNull(DtTemp.Rows(I)("ReferenceDate")) <> "", AgL.XNull(DtTemp.Rows(I)("ReferenceDate")), AgL.XNull(DtTransactionHead.Rows(0)("V_Date")))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Subcode"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("LinkedSubcode"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ContraAc"))) & ", 
                        " & Val(AgL.VNull(DtTemp.Rows(I)("AmtDr"))) & ", 
                        " & Val(AgL.VNull(DtTemp.Rows(I)("AmtCr"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqNo"))) & ",
                        " & AgL.Chk_Date(AgL.XNull(DtTemp.Rows(I)("ChqDate"))) & ",
                        " & AgL.Chk_Date(AgL.XNull(DtTemp.Rows(I)("EffectiveDate"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Narration"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTransactionHead.Rows(0)("Site_Code"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTransactionHead.Rows(0)("Div_Code"))) & ",
                        " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'A'
                        )"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Next
            End If

            If AgL.IsTableExist(bTableName.Replace("[", "").Replace("]", ""), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)) Then
                mQry = "Drop Table " + bTableName
                AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
            End If
        End If
    End Sub

    Private Sub FrmVoucherEntry_BaseEvent_Topctrl_tbMore() Handles Me.BaseEvent_Topctrl_tbMore
        MnuOptions.Show(Topctrl1, Topctrl1.btbSite.Rectangle.X, Topctrl1.btbSite.Rectangle.Y + Topctrl1.btbSite.Rectangle.Size.Height)
    End Sub
    Private Sub FrmVoucherEntry_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()

        mQry = "Select H.RecId
                From TransactionReferences Tr With (NoLock) 
                LEFT JOIN LedgerM H With (NoLock) on Tr.DocId = H.DocId
                Where Tr.ReferenceDocId = '" & mSearchCode & "'
                And Tr.Type = '" & TransactionReferenceTypeConstants.Cancelled & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        Dim bRecIdStr As String = ""
        For I As Integer = 0 To DtTemp.Rows.Count - 1
            If bRecIdStr = "" Then bRecIdStr += ","
            bRecIdStr = AgL.XNull(DtTemp.Rows(I)("RecId"))
        Next

        If bRecIdStr <> "" Then
            MsgBox("Entry is cancelled with reference no. " + bRecIdStr, MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If

        If AgL.Dman_Execute("Select Count(*) From Ledger where DocID = '" & mSearchCode & "' And Clg_Date Is Not Null ", AgL.GCn).ExecuteScalar > 0 Then
            MsgBox("Some / All lines of this document are reconciled. Can't delete entry")
            Topctrl1.FButtonClick(14, True)
            Passed = False
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("LedgerHead", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        mQry = " SELECT Count(*) AS Cnt FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "' 
                    AND IfNull(T.IsDeletingAllowed,0) = 0 "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Refrential Entries Exist For This Entry.Can't Delete.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If
    End Sub
    Private Sub Dgl1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl1.CellBeginEdit
        If Topctrl1.Mode.ToUpper = "BROWSE" Then
            e.Cancel = True
        End If


        If Dgl1.CurrentCell IsNot Nothing Then
            If Dgl1.Item(Col1IsRecordLocked, Dgl1.CurrentCell.RowIndex).Value = 1 Then
                e.Cancel = True
            End If
        End If
    End Sub
    Private Function FGetRelationalData() As Boolean
        Dim DtRelationalData As DataTable
        Try
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


            If Not AgL.StrCmp(FDivisionNameForCustomization(11), "MAA VAISHNO") Then
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
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From Cloth_SupplierSettlementInvoices L
                        LEFT JOIN LedgerHead H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PurchaseInvoiceDocId = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Text + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Edit Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
    Private Function IsTypeVisible() As Boolean
        Dim val As Integer = 0
        mQry = " Select IsVisible From EntryHeaderUISetting Where NCat = '" & LblV_Type.Tag & "'
                    And FieldName = 'Type'"
        val = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
        If val = 0 Then
            IsTypeVisible = False
        Else
            IsTypeVisible = True
        End If
    End Function

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
    Private Sub FGetTdsParameters(bRowIndex As Integer)
        mQry = "SELECT Tg.Description AS TdsGroupDesc, Tc.Description AS TdsCategoryDesc, Tp.TdsCategory, Tp.TdsGroup, 
                Tp.TdsMonthlyLimit, Tp.TdsYearlyLimit, Tp.TdsPer, Tp.LedgerAccount, TSg.Name As LedgerAccountName,
                VPartyTransaction.PartyMonthTransaction, VPartyTransaction.PartyYearTransaction
                FROM Subgroup Sg 
                LEFT JOIN TdsGroup Tg ON Sg.TdsGroup = Tg.Code
                LEFT JOIN TdsCategory Tc ON Sg.TdsCategory = Tc.Code
                LEFT JOIN TdsParameters Tp ON Sg.TdsGroup = Tp.TdsGroup AND Sg.TdsCategory = Tp.TdsCategory
                LEFT JOIN SubGroup TSg On Tp.LedgerAccount = TSg.SubCode
                LEFT JOIN (
	                SELECT L.SubCode, 
	                Sum(CASE WHEN L.V_Date BETWEEN " & AgL.Chk_Date(CDate(AgL.RetMonthStartDate(TxtV_Date.Text))) & " AND " & AgL.Chk_Date(CDate(AgL.RetMonthEndDate(TxtV_Date.Text))) & " THEN L.AmtDr END) AS PartyMonthTransaction,
	                Sum(CASE WHEN L.V_Date BETWEEN " & AgL.Chk_Date(CDate(AgL.PubStartDate)) & " AND " & AgL.Chk_Date(CDate(AgL.PubEndDate)) & " THEN L.AmtDr END) AS PartyYearTransaction
	                FROM Ledger L 
	                WHERE L.SubCode = '" & Dgl1.Item(Col1Subcode, bRowIndex).Tag & "'
                    And L.DocId <> '" & mSearchCode & "'
	                GROUP BY L.SubCode) AS VPartyTransaction ON Sg.SubCode = VPartyTransaction.SubCode
                WHERE Sg.Subcode = '" & Dgl1.Item(Col1Subcode, bRowIndex).Tag & "'"
        Dim DtTDS As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTDS.Rows.Count > 0 Then
            Dgl1.Item(Col1TdsCategory, bRowIndex).Tag = AgL.XNull(DtTDS.Rows(0)("TdsCategory"))
            Dgl1.Item(Col1TdsCategory, bRowIndex).Value = AgL.XNull(DtTDS.Rows(0)("TdsCategoryDesc"))
            Dgl1.Item(Col1TdsGroup, bRowIndex).Tag = AgL.XNull(DtTDS.Rows(0)("TdsGroup"))
            Dgl1.Item(Col1TdsGroup, bRowIndex).Value = AgL.XNull(DtTDS.Rows(0)("TdsGroupDesc"))
            Dgl1.Item(Col1TdsLedgerAccount, bRowIndex).Tag = AgL.XNull(DtTDS.Rows(0)("LedgerAccount"))
            Dgl1.Item(Col1TdsLedgerAccount, bRowIndex).Value = AgL.XNull(DtTDS.Rows(0)("LedgerAccountName"))
            Dgl1.Item(Col1TdsMonthlyLimit, bRowIndex).Value = AgL.VNull(DtTDS.Rows(0)("TdsMonthlyLimit"))
            Dgl1.Item(Col1TdsYearlyLimit, bRowIndex).Value = AgL.VNull(DtTDS.Rows(0)("TdsYearlyLimit"))
            Dgl1.Item(Col1TdsPer, bRowIndex).Value = AgL.VNull(DtTDS.Rows(0)("TdsPer"))
            Dgl1.Item(Col1PartyMonthTransaction, bRowIndex).Value = AgL.VNull(DtTDS.Rows(0)("PartyMonthTransaction"))
            Dgl1.Item(Col1PartyYearTransaction, bRowIndex).Value = AgL.VNull(DtTDS.Rows(0)("PartyYearTransaction"))
            Calculation()
        End If
    End Sub
    Private Sub FLedgerPostTds(Conn As Object, Cmd As Object)
        Dim mMaxSr As Integer = AgL.VNull(AgL.Dman_Execute("Select IfNull(Max(V_SNo),0) 
                        From Ledger With (NoLock)
                        Where DocId = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1Subcode, I).Tag) <> "" And Val(Dgl1.Item(Col1TdsAmount, I).Value) > 0 Then
                mMaxSr += 1
                mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                    AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                    SELECT '" & mSearchCode & "' As DocId, " & mMaxSr & " As V_SNo, " & Val(TxtV_No.Text) & " As V_No, 
                    " & AgL.Chk_Text(TxtV_Type.Tag) & " As V_Type, 
                    " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                    " & AgL.Chk_Text(TxtV_Date.Text) & " As V_Date, 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & " As SubCode, 
                    " & AgL.Chk_Text(Dgl1.Item(Col1TdsLedgerAccount, I).Tag) & " As ContraSub, 
                    " & Val(Dgl1.Item(Col1TdsAmount, I).Value) & " As AmtDr, 
                    0 As AmtCr, 
                    Null As Chq_No, Null As Chq_Date, 
                    " & AgL.Chk_Text("Being Tds deducted for " + Dgl1.Item(Col1Subcode, I).Value) & " As Narration, 
                    " & AgL.Chk_Text(TxtSite_Code.Tag) & " As Site_Code, 
                    " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                    " & AgL.Chk_Text(AgL.PubLoginDate) & " As U_EntDt, 
                    '" & TxtDivision.Tag & "' As DivCode, 
                    " & AgL.Chk_Text(TxtReferenceNo.Text) & " As RecId "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mMaxSr += 1
                mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                    AmtDr, AmtCr, Chq_No, Chq_Date, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                    SELECT '" & mSearchCode & "' As DocId, " & mMaxSr & " As V_SNo, " & Val(TxtV_No.Text) & " As V_No, 
                    " & AgL.Chk_Text(TxtV_Type.Tag) & " As V_Type, 
                    " & AgL.Chk_Text(LblPrefix.Text) & " As V_Prefix, 
                    " & AgL.Chk_Text(TxtV_Date.Text) & " As V_Date, 
                    " & AgL.Chk_Text(Dgl1.Item(Col1TdsLedgerAccount, I).Tag) & " As SubCode, 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & " As ContraSub, 
                    0 As AmtDr, 
                    " & Val(Dgl1.Item(Col1TdsAmount, I).Value) & " As AmtCr, 
                    Null As Chq_No, Null As Chq_Date, 
                    " & AgL.Chk_Text("Being Tds deducted for " + Dgl1.Item(Col1Subcode, I).Value) & " As Narration, 
                    " & AgL.Chk_Text(TxtSite_Code.Tag) & " As Site_Code, 
                    " & AgL.Chk_Text(AgL.PubUserName) & " As U_Name, 
                    " & AgL.Chk_Text(AgL.PubLoginDate) & " As U_EntDt, 
                    '" & TxtDivision.Tag & "' As DivCode, 
                    " & AgL.Chk_Text(TxtReferenceNo.Text) & " As RecId "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub
    Private Sub FOpengPendingLedgerAdj(mRow As Integer)
        Dim DtTemp As DataTable = Nothing

        mQry = " Select 'o' As Tick, H.DocID || '#' || Cast(H.V_SNo as Varchar) As SearchKey, 
                    H.RecId As InvoiceNo , 
                    H.V_Date, Si.SaleToPartyName,
                    H.AmtDr as Inv_Amount, 
                    H.AmtDr-IfNull(Adj.AdjAmt,0) as Bal_Amount, 
                    H.SubCode, Sg.Name As PartyName, H.DocId, H.V_SNo
                    from Ledger H
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                
                    LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                    LEFT JOIN SaleInvoice Si On H.DocId = Si.DocId
                    where H.DivCode = '" & AgL.PubDivCode & "' And  H.AmtDr>0 
                    And H.SubCode = '" & Dgl1.Item(Col1Subcode, mRow).Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtV_Date.Text)).ToString("s")) & " 
                    And H.AmtDr - IfNull(Adj.AdjAmt,0)>0
                    Order by H.V_Date, (IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId)
                    "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 690, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Invoice No.", 90, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Invoice Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Party Name", 90, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, "Invoice Amount", 90, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, "Balance Amount", 90, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.FFormatColumn(7, , 0, , False)
        FRH_Multiple.FFormatColumn(8, , 0, , False)
        FRH_Multiple.FFormatColumn(9, , 0, , False)
        FRH_Multiple.FFormatColumn(10, , 0, , False)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        Dim StrRtn As String = ""
        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If

        Dim DrSelected As DataRow()
        If StrRtn <> "" Then
            DrSelected = DtTemp.Select("SearchKey In (" & StrRtn & ")")

            If mRow < 0 Then
                If Dgl1.Rows.Count > 1 Then
                    If MsgBox("Do you want to overwrite existing data in grid ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        For I As Integer = 0 To Dgl1.Rows.Count - 1
                            If Dgl1.Item(Col1IsRecordLocked, I).Value = 0 Then
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
                Dgl1.Item(Col1Subcode, mRow + I).Tag = AgL.XNull(DrSelected(I)("SubCode"))
                Dgl1.Item(Col1Subcode, mRow + I).Value = AgL.XNull(DrSelected(I)("PartyName"))
                Dgl1.Item(Col1ReferenceNo, mRow + I).Value = AgL.XNull(DrSelected(I)("InvoiceNo"))
                Dgl1.Item(Col1SpecificationDocId, mRow + I).Value = AgL.XNull(DrSelected(I)("DocId"))
                Dgl1.Item(Col1SpecificationDocIdSr, mRow + I).Value = AgL.XNull(DrSelected(I)("V_SNo"))
                Dgl1.Item(Col1Amount, mRow + I).Value = AgL.XNull(DrSelected(I)("Bal_Amount"))
            Next
        End If
        Calculation()
    End Sub
    Private Sub FPostEntryForBranch(SearchCode As String, Conn As Object, Cmd As Object)
        Dim dtLine As DataTable
        If FDivisionNameForCustomization(6) = "SADHVI" And (AgL.StrCmp(AgL.PubDBName, "SHADHVINEW") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIKNP2") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIJaunpur") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIJNP2")) Then
            If (LblV_Type.Tag = Ncat.Receipt Or LblV_Type.Tag = Ncat.VisitReceipt Or
                LblV_Type.Tag = Ncat.Payment) And TxtNature.Text.ToUpper = "BANK" Then
                Dim bSadhviHO As String = ""
                If AgL.PubDivCode = "E" Then
                    bSadhviHO = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                            Where Name = 'SADHVI EMBROIDERY'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                Else
                    bSadhviHO = AgL.XNull(AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock)
                            Where Name = 'SADHVI ENTERPRISES'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                End If

                mQry = "Select Sr From LedgerHeadDetail with (NoLock) Where DocId = '" & SearchCode & "'"
                dtLine = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
                If dtLine.Rows.Count > 0 Then
                    For I As Integer = 0 To dtLine.Rows.Count - 1
                        Dim mMaxSr As Integer = AgL.XNull(AgL.Dman_Execute("Select Max(V_SNo) As V_SNo From Ledger 
                            Where DocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())

                        Dim mDebitAmount As String = ""
                        Dim mCreditAmount As String = ""

                        If LblV_Type.Tag = Ncat.Payment Then
                            mDebitAmount = " 0 "
                            mCreditAmount = " Sum(L.Amount) "
                        Else
                            mDebitAmount = " Sum(L.Amount) "
                            mCreditAmount = " 0 "
                        End If

                        mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, 
                        AmtDr, AmtCr, Narration, Site_Code, U_Name, U_EntDt, DivCode, RecId)
                        SELECT H.DocId, " & mMaxSr + 1 & " AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                        Max(H.V_Date) AS V_Date, '" & bSadhviHO & "' AS SubCode, Max(H.SubCode) AS ContraSub, 
                        " & mDebitAmount & " AS AmtDr, " & mCreditAmount & " AS AmtCr, 'Being Payment Transfered To HO' AS Narration, 
                        Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                        Max(H.ManualRefNo) AS RecId
                        FROM LedgerHead H With (NoLock)
                        LEFT JOIN LedgerHeadDetail L With (NoLock) ON H.DocID = L.DocID
                        WHERE H.DocId = '" & SearchCode & "' And L.Sr = " & AgL.VNull(dtLine.Rows(I)("Sr")) & "
                        GROUP BY H.DocID	
                        UNION ALL
                        SELECT H.DocId, " & mMaxSr + 2 & " AS V_SNo, Max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, 
                        Max(H.V_Date) AS V_Date, Max(H.SubCode) AS SubCode, '" & bSadhviHO & "' AS ContraSub, 
                        " & mCreditAmount & " AS AmtDr, " & mDebitAmount & " AS AmtCr, 'Being Goods Transfered To HO' AS Narration, 
                        Max(H.Site_Code) AS Site_Code, Max(H.EntryBy) AS U_Name, Max(H.EntryDate) U_EntDt, Max(H.Div_Code) AS DivCode, 
                        Max(H.ManualRefNo) AS RecId
                        FROM LedgerHead H With (NoLock)
                        LEFT JOIN LedgerHeadDetail L With (NoLock) ON H.DocID = L.DocID
                        WHERE H.DocId = '" & SearchCode & "' And L.Sr = " & AgL.VNull(dtLine.Rows(I)("Sr")) & "
                        GROUP BY H.DocID "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Next I
                End If
            End If
        End If
    End Sub
    Private Sub FGetSettingVariableValuesForAddAndEdit()
        SettingFields_CopyRemarkInNextLineYn = CType(AgL.VNull(FGetSettings(SettingFields.CopyRemarkInNextLineYn, SettingType.General)), Boolean)
        SettingFields_MaximumItemLimit = AgL.VNull(FGetSettings(SettingFields.MaximumItemLimit, SettingType.General))
    End Sub
End Class
