Imports Microsoft.Reporting.WinForms
Imports System.Xml
Imports Customised.ClsMain
Imports System.IO
Imports AgLibrary.ClsMain.agConstants

Public Class FrmReverseChargeEntry
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1Subcode As String = "Subcode"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Public Const Col1HSN As String = "HSN"
    Public Const Col1SpecificationDocId As String = "Voucher No"
    Public Const Col1SpecificationDocIdDate As String = "Voucher Date"
    Public Const Col1TaxableValue As String = "Taxable Value"
    Public Const Col1Tax1_Per As String = "Integrated Tax Per"
    Public Const Col1Tax1 As String = "Integrated Tax"
    Public Const Col1Tax2_Per As String = "Central Tax Per"
    Public Const Col1Tax2 As String = "Central Tax"
    Public Const Col1Tax3_Per As String = "State Tax Per"
    Public Const Col1Tax3 As String = "State Tax"
    Public Const Col1TaxAmount As String = "Tax Amount"
    Public Const Col1Remark As String = "Remark"
    '========================================================================

    Dim mPrevRowIndex As Integer = 0
    Friend WithEvents MnuWizard As ToolStripMenuItem

    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay



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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReverseChargeEntry))
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtPartyName = New AgControls.AgTextBox()
        Me.LblPartyName = New System.Windows.Forms.Label()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalTaxableAmount = New System.Windows.Forms.Label()
        Me.LblTotalTax = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalTaxText = New System.Windows.Forms.Label()
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
        Me.MnuWizard = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
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
        Me.TabControl1.Size = New System.Drawing.Size(992, 132)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
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
        Me.TP1.Size = New System.Drawing.Size(984, 106)
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
        Me.LblPartyName.Size = New System.Drawing.Size(73, 14)
        Me.LblPartyName.TabIndex = 693
        Me.LblPartyName.Text = "A/c Name"
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalTaxableAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalTax)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalTaxText)
        Me.PnlTotals.Location = New System.Drawing.Point(4, 548)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 694
        '
        'LblTotalTaxableAmount
        '
        Me.LblTotalTaxableAmount.AutoSize = True
        Me.LblTotalTaxableAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalTaxableAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalTaxableAmount.Location = New System.Drawing.Point(401, 3)
        Me.LblTotalTaxableAmount.Name = "LblTotalTaxableAmount"
        Me.LblTotalTaxableAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalTaxableAmount.TabIndex = 660
        Me.LblTotalTaxableAmount.Text = "."
        Me.LblTotalTaxableAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalTax
        '
        Me.LblTotalTax.AutoSize = True
        Me.LblTotalTax.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalTax.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalTax.Location = New System.Drawing.Point(850, 4)
        Me.LblTotalTax.Name = "LblTotalTax"
        Me.LblTotalTax.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalTax.TabIndex = 662
        Me.LblTotalTax.Text = "."
        Me.LblTotalTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(235, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(155, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Taxable Amount :"
        '
        'LblTotalTaxText
        '
        Me.LblTotalTaxText.AutoSize = True
        Me.LblTotalTaxText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalTaxText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalTaxText.Location = New System.Drawing.Point(766, 3)
        Me.LblTotalTaxText.Name = "LblTotalTaxText"
        Me.LblTotalTaxText.Size = New System.Drawing.Size(74, 16)
        Me.LblTotalTaxText.TabIndex = 661
        Me.LblTotalTaxText.Text = "Total Tax :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(4, 173)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 373)
        Me.Pnl1.TabIndex = 9
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(561, 589)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(22, 32)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(380, 64)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(414, 16)
        Me.TxtRemarks.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(258, 66)
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
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 152)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Detail For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(773, 589)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(50, 23)
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
        Me.Panel3.TabIndex = 6
        '
        'LblCurrentBalance
        '
        Me.LblCurrentBalance.AutoSize = True
        Me.LblCurrentBalance.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrentBalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentBalance.Location = New System.Drawing.Point(379, 155)
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
        Me.Label3.Location = New System.Drawing.Point(261, 155)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 14)
        Me.Label3.TabIndex = 3005
        Me.Label3.Text = "Current Balance :"
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportGSTDataFromDos, Me.MnuImportGSTDataFromExcel, Me.MnuImportFromTally, Me.MnuImportFromDos, Me.MnuEditSave, Me.MnuWizard, Me.MnuReport})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(222, 202)
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
        'MnuWizard
        '
        Me.MnuWizard.Name = "MnuWizard"
        Me.MnuWizard.Size = New System.Drawing.Size(221, 22)
        Me.MnuWizard.Text = "Wizard"
        '
        'MnuReport
        '
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(221, 22)
        Me.MnuReport.Text = "Report"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'FrmReverseChargeEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblCurrentBalance)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.Name = "FrmReverseChargeEntry"
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
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
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
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents LblPartyName As System.Windows.Forms.Label
    Public WithEvents TxtPartyName As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents PnlTotals As System.Windows.Forms.Panel
    Protected WithEvents LblTotalTaxableAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents LblTotalTax As System.Windows.Forms.Label
    Protected WithEvents LblTotalTaxText As System.Windows.Forms.Label
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
    Protected WithEvents LblCurrentBalance As Label
    Public WithEvents TxtVoucherCategory As AgControls.AgTextBox
    Protected WithEvents Label3 As Label
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuEditSave As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Dim Dgl As New AgControls.AgDataGrid
    Friend WithEvents MnuReport As ToolStripMenuItem
    Public Shared mFlag_Import As Boolean = False
    Friend WithEvents MnuImportGSTDataFromDos As ToolStripMenuItem
    Friend WithEvents MnuImportGSTDataFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
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
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1ColumnCount += 1
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                        End If
                    Next
                Next
            End If
            If mDgl1ColumnCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True





        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub
    Private Sub FrmLedgerHead_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim DsTemp As DataTable

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
        mCondStr = mCondStr & " Order By Cast(H.ManualRefNo as BigInt)"


        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Entry_Type], H.V_Date AS Date, SGV.Name AS [Party], " &
                            " H.ManualRefNo AS [Entry_No], H.SalesTaxGroupParty AS [Sales_Tax_Group_Party], " &
                            " H.Remarks,  " &
                            " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date] " &
                            " FROM LedgerHead H  With (NoLock) " &
                            " LEFT JOIN Voucher_Type Vt With (NoLock) ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup SGV With (NoLock) ON SGV.SubCode  = H.Subcode " &
                            " Where 1=1 " & mCondStr
        AgL.PubFindQry = AgL.GetBackendBasedQuery(AgL.PubFindQry)
        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SpecificationDocId, 100, 5, Col1SpecificationDocId, False, True, False)
            .AddAgDateColumn(Dgl1, Col1SpecificationDocIdDate, 90, Col1SpecificationDocIdDate, True, True)
            .AddAgTextColumn(Dgl1, Col1Subcode, 400, 0, Col1Subcode, True, True)
            .AddAgNumberColumn(Dgl1, Col1HSN, 80, 8, 0, False, Col1HSN, False, True, True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, True, True)
            .AddAgNumberColumn(Dgl1, Col1TaxableValue, 100, 8, 2, False, Col1TaxableValue, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Tax1_Per, 100, 8, 2, False, Col1Tax1_Per, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Tax1, 100, 8, 2, False, Col1Tax1, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Tax2_Per, 100, 8, 2, False, Col1Tax2_Per, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Tax2, 100, 8, 2, False, Col1Tax2, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Tax3_Per, 100, 8, 2, False, Col1Tax3_Per, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Tax3, 100, 8, 2, False, Col1Tax3, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TaxAmount, 100, 8, 2, False, Col1TaxAmount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, True)
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
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top


        If LblV_Type.Tag <> "" Then
            ApplyUISetting(LblV_Type.Tag)
        Else
            ApplyUISetting(EntryNCat)
        End If


        AgCalcGrid1.Ini_Grid(EntryNCat, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Subcode).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1TaxAmount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingPartyAc = TxtPartyName.AgSelectedValue

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
                    " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
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


                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1

                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1HSN, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                            " " & Val(Dgl1.Item(Col1TaxAmount, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                            " " & AgL.Chk_Text(Dgl1.Item(Col1SpecificationDocId, I).Value) & ""


                    If TxtStructure.Tag <> "" Then
                        If bChargesSelectionQry <> "" Then bChargesSelectionQry += " UNION ALL "
                        bChargesSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & AgCalcGrid1.FLineTableFieldValuesStr(I, mMultiplyWithMinus)
                    End If
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                            mQry = " UPDATE LedgerHeadDetail " &
                                        " Set " &
                                        " Subcode = " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                        " HSN = " & AgL.Chk_Text(Dgl1.Item(Col1HSN, I).Value) & ", " &
                                        " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                        " Amount = " & Val(Dgl1.Item(Col1TaxAmount, I).Value) & ", " &
                                        " Remarks = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                        " SpecificationDocId = " & AgL.Chk_Text(Dgl1.Item(Col1SpecificationDocId, I).Value) & ", " &
                                        " UploadDate = Null " &
                                        " Where DocId = '" & mSearchCode & "' " &
                                        " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If

                        If TxtStructure.Tag <> "" Then
                            mQry = "Insert Into LedgerHeadDetailCharges(DocID, Sr, " & AgCalcGrid1.FLineTableFieldNameStr() & ") 
                                   Values ('" & mSearchCode & "'," & Val(Dgl1.Item(ColSNo, I).Tag) & ", " & AgCalcGrid1.FLineTableFieldValuesStr(I, mMultiplyWithMinus) & ")"
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If

                    Else
                        Dim DtDocID As DataTable
                        mQry = "Select DocID From LedgerHeadDetail with (Nolock) Where ReferenceDocID = '" & mSearchCode & "' And ReferenceDocIdSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & ""
                        DtDocID = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

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


            End If
        Next

        If bSelectionQry <> "" Then
            mQry = "Insert Into LedgerHeadDetail(DocId, Sr, Subcode, HSN, SalesTaxGroupItem, " &
                       " Amount, Remarks, " &
                       " SpecificationDocId) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            If TxtStructure.Tag <> "" Then
                mQry = "Insert Into LedgerHeadDetailCharges(DocID, Sr, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bChargesSelectionQry
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If




        Dim mNarr As String = ""
        Dim mNarrParty As String = ""


        If TxtStructure.Tag <> "" Then
            Call PostStructureLineToAccounts(AgCalcGrid1, mNarrParty, mNarr, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue,
                               TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtReferenceNo.Text, TxtPartyName.AgSelectedValue, TxtV_Date.Text, Conn, Cmd,, mMultiplyWithMinus)
        End If
        PostGridToAccounts(mSearchCode, mMultiplyWithMinus, Conn, Cmd)



        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub
    Sub PostGridToAccounts(DocID As String, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
        Dim mLedgerPostingData As String = ""
        Dim I As Integer
        Dim mHeaderAccountDrCr As String
        Dim DtTemp As DataTable
        Dim mNarration As String = ""




        mQry = "Select HeaderAccountDrCr From Voucher_Type with (NoLock) Where V_Type = '" & TxtV_Type.Tag & "'"
        mHeaderAccountDrCr = AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).executeScalar

        If mHeaderAccountDrCr.ToUpper <> "DR" And mHeaderAccountDrCr.ToUpper <> "CR" Then Exit Sub

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Rows(I).Visible = True And Val(Dgl1(Col1TaxAmount, I).Value) <> 0 And Dgl1.Item(Col1TaxAmount, I).Style.ForeColor <> Color.Blue Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mNarration = TxtV_Type.Text & " : " & TxtPartyName.Text & ". " & Dgl1(Col1Remark, I).Value
                mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & " as Subcode, " & AgL.Chk_Text(TxtPartyName.Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1TaxAmount, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1TaxAmount, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration "

                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mNarration = TxtV_Type.Text & " : " & Dgl1(Col1Subcode, I).Value & ". " & Dgl1(Col1Remark, I).Value
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtPartyName.Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(Dgl1(Col1TaxAmount, I).Value), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(Dgl1(Col1TaxAmount, I).Value), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration "
            End If
        Next



        'If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
        'mNarration = TxtV_Type.Text & " : " & mNarration
        'mLedgerPostingData += " Select " & AgL.Chk_Text(TxtPartyName.Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtPartyName.Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(LblTotalAmount.Text), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(LblTotalAmount.Text), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, null as ChqNo, Null as ChqDate, Null as EffectiveDate "

        If mLedgerPostingData = "" Then Exit Sub

        mLedgerPostingData = "Select SubCode, LinkedSubcode, ContraAc, Narration, AmtDr*1.0 as AmtDr, AmtCr*1.0 as AmtCr, ChqNo, ChqDate, EffectiveDate 
                              From (" & mLedgerPostingData & ") as X  "
        DtTemp = AgL.FillData(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                mQry = "INSERT INTO Ledger
                        (DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, 
                        V_Date, SubCode, LinkedSubcode, ContraSub, AmtDr, AmtCr, 
                        Chq_No, Chq_Date, EffectiveDate, Narration, Site_Code, DivCode, 
                        U_Name, U_EntDt, U_AE)
                        VALUES('" & DocID & "', " & I + 1 & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                        " & AgL.Chk_Date(TxtV_Date.Text) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Subcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("LinkedSubcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ContraAc"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtDr"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtCr"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqNo"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqDate"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("EffectiveDate"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Narration"))) & "," & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",
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
                        If Dgl1.Item(Col1TaxAmount, J).Style.ForeColor = Color.Blue And FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag.ToString.ToUpper = "GAMT" Then
                            ' Not Fore Colour = Blue Means This Entry is Splitted into several Cash Entries

                        Else
                            If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                            bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, 
                        '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As ContraAc, 
                        Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                             When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount"

                            If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc)) <> "" Then
                                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                                bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As PostAc, 
                            '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As ContraAc, 
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount"
                            End If
                        End If
                    ElseIf Trim(AgL.XNull(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value)) <> "" Then
                        If Dgl1.Item(Col1TaxAmount, J).Style.ForeColor = Color.Blue And FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag.ToString.ToUpper = "GAMT" Then
                            ' Not Fore Colour = Blue Means This Entry is Splitted into several Cash Entries
                        Else
                            If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                            bSelectionQry += " Select 1 as TmpCol,'" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc,
                            '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As ContraAc,
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount"


                            If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc)) <> "" Then
                                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                                bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As PostAc, 
                            '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As ContraAc, 
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount"
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

        LblTotalTaxableAmount.Text = 0
        LblTotalTax.Text = 0

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
                'Call FGetCurrBal(TxtPartyName.AgSelectedValue)


                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), EntryNCat, TxtV_Date.Text, mMultiplyWithMinus)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))




                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select L.*, IfNull(Pi.V_Type || '-' || Pi.ManualRefNo, Lh.V_Type || '-' || Lh.ManualRefNo) As SpecificationDocNo, 
                        IfNull(Pi.V_Date, Lh.V_Date) As SpecificationDocDate, 
                        Sg.Name as AccountName, Sg.Nature, Sg.SubgroupType, LC.* 
                        From (Select * From LedgerHeadDetail  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN viewHelpSubgroup Sg  With (NoLock) ON L.Subcode = Sg.Code 
                        LEFT JOIN PurchInvoice PI On L.SpecificationDocId = Pi.DocId
                        LEFT JOIN LedgerHead LH On L.SpecificationDocId = Lh.DocId
                        Left Join LedgerHeadDetailCharges LC  With (NoLock) on L.DocID = LC.DocID And L.Sr = LC.Sr
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

                            Dgl1.Item(Col1HSN, I).Value = AgL.XNull(.Rows(I)("HSN"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))

                            Dgl1.Item(Col1SpecificationDocId, I).Tag = AgL.XNull(.Rows(I)("SpecificationDocId"))
                            Dgl1.Item(Col1SpecificationDocId, I).Value = AgL.XNull(.Rows(I)("SpecificationDocNo"))

                            Dgl1.Item(Col1SpecificationDocIdDate, I).Value = AgL.XNull(.Rows(I)("SpecificationDocDate"))


                            Dgl1.Item(Col1TaxableValue, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Taxable_Amount"))), "0.00")
                            Dgl1.Item(Col1Tax1_Per, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Tax1_Per"))), "0.00")
                            Dgl1.Item(Col1Tax1, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Tax1"))), "0.00")
                            Dgl1.Item(Col1Tax2_Per, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Tax2_Per"))), "0.00")
                            Dgl1.Item(Col1Tax2, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Tax2"))), "0.00")
                            Dgl1.Item(Col1Tax3_Per, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Tax3_Per"))), "0.00")
                            Dgl1.Item(Col1Tax3, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Tax3"))), "0.00")
                            Dgl1.Item(Col1TaxAmount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")


                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))


                            If TxtNature.Text.ToUpper = "CASH" Then
                                mQry = "Select Count(*) From Ledger  With (NoLock) Where ReferenceDocID = '" & SearchCode & "' And ReferenceDocIDSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & ""
                                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                                    Dgl1.Item(Col1TaxAmount, I).Style.ForeColor = Color.Blue
                                End If
                            End If


                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I, mMultiplyWithMinus)

                            LblTotalTaxableAmount.Text = Val(LblTotalTaxableAmount.Text) + Val(Dgl1.Item(Col1TaxableValue, I).Value)
                            LblTotalTax.Text = Val(LblTotalTax.Text) + Val(Dgl1.Item(Col1TaxAmount, I).Value)
                        Next I
                    End If
                End With
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

                '-------------------------------------------------------------




            End If
        End With
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

                    If TxtRemarks.Visible Then TxtRemarks.Focus()


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

        'TxtGodown.Tag = DtV_TypeSettings.Rows(0)("DEFAULT_Godown")
        'TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" Select Description From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GCn).ExecuteScalar)


        'TxtSaleToParty.Focus()
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
        End If
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        If Topctrl1.Mode = "Browse" Then Exit Sub

        LblTotalTaxableAmount.Text = 0
        LblTotalTax.Text = 0


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Subcode, I).Value <> "" Then
                LblTotalTaxableAmount.Text = Val(LblTotalTaxableAmount.Text) + Val(Dgl1.Item(Col1TaxableValue, I).Value)
                LblTotalTax.Text = Val(LblTotalTax.Text) + Val(Dgl1.Item(Col1TaxAmount, I).Value)
            End If
        Next






        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1TaxAmount).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1TaxAmount).Index
        If AgL.VNull(AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable")) = True Then
            AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        Else
            AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = -1
        End If
        AgCalcGrid1.AgPostingPartyAc = TxtPartyName.Tag


        AgCalcGrid1.AgVoucherCategory = TxtVoucherCategory.Text.ToUpper

        AgCalcGrid1.Calculation()





        LblTotalTaxableAmount.Text = Val(LblTotalTaxableAmount.Text)
        LblTotalTax.Text = Val(LblTotalTax.Text)
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


        If TxtReferenceNo.Text = "" Then
            MsgBox("Entry No. Can Not Be Blank")
            TxtReferenceNo.Focus()
            Exit Sub
        End If


        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Subcode, I).Value <> "" Then
                    If Dgl1.Columns(Col1HSN).Visible = True Then
                        If AgL.XNull(Dgl1.Item(Col1HSN, I).Value) = "" Then
                            MsgBox("HSN Is blank at Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1HSN, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
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





        If TxtStructure.Text <> "" Then
            If Math.Round(Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)), 0) <> Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) Then
                Calculation()
                Calculation()
            End If
        End If
    End Sub

    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPartyName.KeyDown
        Try



            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtPartyName.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            FCreateHelpSubgroupHeader()
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
                Case Col1TaxAmount
                    Dgl1.CurrentCell.ReadOnly = IIf(Dgl1.Item(Col1TaxAmount, Dgl1.CurrentCell.RowIndex).Tag Is Nothing, False, True)
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
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



                Case Col1SalesTaxGroup
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = "Select Description as Code, Description as Name From PostingGroupSalesTaxItem  With (NoLock) Where Active=1"
                            Dgl1.AgHelpDataSet(Col1SalesTaxGroup) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


            End Select
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



    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            If Dgl1.Columns(Col1Subcode).Visible = True Then
                Dgl1.CurrentCell = Dgl1.Item(Col1Subcode, Dgl1.Rows.Count - 1) : Dgl1.Focus()
            End If
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


        mQry = "SELECT Sg.Code, Sg.Name, Sg.Address, Ag.GroupName
                FROM viewHelpSubGroup Sg  With (NoLock)                       
                Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Subcode) = AgL.FillData(mQry, AgL.GCn)
    End Sub









    Private Sub FrmLedgerHeadDirect_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        MsgBox("Editing is not allowed.You can only Editing this entry.", MsgBoxStyle.Information)
        Passed = False
        Exit Sub

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

        If AgL.Dman_Execute("Select Count(*) From Ledger where DocID = '" & mSearchCode & "' And Clg_Date Is Not Null ", AgL.GCn).ExecuteScalar > 0 Then
            MsgBox("Some / All lines of this document are reconciled. Can't modify entry")
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("LedgerHead", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        mQry = " SELECT Count(*) AS Cnt FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "' 
                    AND IfNull(T.IsEditingAllowed,0) = 0 "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Refrential Entries Exist For This Entry.Can't Modify.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If


        TxtPartyName.AgHelpDataSet = Nothing
    End Sub


    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuEditSave.Click, MnuImportGSTDataFromExcel.Click, MnuImportGSTDataFromDos.Click, MnuReport.Click, MnuWizard.Click
        Select Case sender.name

            Case MnuEditSave.Name
                FEditSaveAllEntries()

            Case MnuWizard.Name
                FWizard()


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
        End Select
    End Sub


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

    Private Sub FWizard()
        Dim StrSenderText As String = Me.Text
        GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
        GridReportFrm.Filter_IniGrid()

        Dim CRep As ClsReverseChargeWizard = New ClsReverseChargeWizard(GridReportFrm)
        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
        CRep.V_Type = EntryNCat
        CRep.ObjFrm = Me
        CRep.Ini_Grid()
        'GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 0).Value = AgL.PubStartDate
        'GridReportFrm.FilterGrid.Item(AgLibrary.FrmRepDisplay.GFilter, 1).Value = AgL.PubLoginDate
        ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
        GridReportFrm.MdiParent = Me.MdiParent
        GridReportFrm.Show()
        CRep.ProcReverseCharge()
    End Sub
    Private Function FGetRelationalData() As Boolean
        Dim DtRelationalData As DataTable
        Try
            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From PurchInvoiceDetail L
                        LEFT JOIN PurchInvoice H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.PurchInvoice = '" & mSearchCode & "' 
                        And L.PurchInvoice <> L.DocId "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Tag + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From PurchInvoiceDetail L
                        LEFT JOIN PurchInvoice H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.ReferenceDocId = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Tag + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From StockHeadDetail L
                        LEFT JOIN StockHead H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L.ReferenceDocId = '" & mSearchCode & "' "
            DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtRelationalData.Rows.Count > 0 Then
                MsgBox("Data Exists For " & TxtV_Type.Tag + "-" + TxtReferenceNo.Text & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

End Class
