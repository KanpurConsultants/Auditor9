Imports CrystalDecisions.CrystalReports.Engine
Public Class TempPurchQuotation
    Inherits AgTemplate.TempTransaction
    Public mQry$
    'Dim DsItem As DataSet
    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Public WithEvents AgCShowGrid1 As New AgStructure.AgCalcShowGrid
    Public WithEvents AgCShowGrid2 As New AgStructure.AgCalcShowGrid

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1IndentNo As String = "Indent No"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1OrdQty As String = "Ord Qty"
    Protected Const Col1OrdMeasure As String = "Ord Measure"
    Protected Const Col1PurchQty As String = "Purch Qty"
    Protected Const Col1PurchMeasure As String = "Purch Measure"
    Protected Const Col1SalesTaxGroup As String = "Item Sales Tax Group"


    Protected Const Col1QuotSelection As String = "QuotSelection"
    Protected Const Col1QuotSelectionIndex As String = "QuotSelectionIndex"
    Protected Const Col1QuotSelectionV_Type As String = "QuotSelectionV_Type"
    Protected Const Col1QuotSelectionV_No As String = "QuotSelectionV_No"
    Protected Const Col1QuotSelectionV_Date As String = "QuotSelectionV_Date"

    Public Class HelpDataSet
        Public Shared Vendor As DataSet = Nothing
        Public Shared Currency As DataSet = Nothing
        Public Shared BillingType As DataSet = Nothing
        Public Shared SalesTaxGroupParty As DataSet = Nothing
        Public Shared IndentNo As DataSet = Nothing
        Public Shared Item As DataSet = Nothing
    End Class

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtVendor = New AgControls.AgTextBox
        Me.LblVendor = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.TxtVendorCity = New AgControls.AgTextBox
        Me.LblVendorCity = New System.Windows.Forms.Label
        Me.LblVendorReq = New System.Windows.Forms.Label
        Me.TxtVendorCountry = New AgControls.AgTextBox
        Me.LblVendorCountry = New System.Windows.Forms.Label
        Me.LblCurrency = New System.Windows.Forms.Label
        Me.TxtCurrency = New AgControls.AgTextBox
        Me.LblVendorQuoteNo = New System.Windows.Forms.Label
        Me.TxtVendorQuoteNo = New AgControls.AgTextBox
        Me.LblVendorQuoteDate = New System.Windows.Forms.Label
        Me.TxtVendorQuoteDate = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtTermsAndConditions = New AgControls.AgTextBox
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtBillingType = New AgControls.AgTextBox
        Me.LblBillingType = New System.Windows.Forms.Label
        Me.LblPostingGroupSalesTaxParty = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.LblIndentNo = New System.Windows.Forms.Label
        Me.TxtIndentNo = New AgControls.AgTextBox
        Me.PnlCShowGrid2 = New System.Windows.Forms.Panel
        Me.PnlCShowGrid = New System.Windows.Forms.Panel
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
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(809, 581)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(631, 581)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(142, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'CmdMoveToLog
        '
        Me.CmdMoveToLog.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(453, 581)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(160, 581)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(11, 581)
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
        Me.GroupBox1.Size = New System.Drawing.Size(990, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(309, 581)
        Me.GBoxDivision.Size = New System.Drawing.Size(114, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
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
        Me.LblV_No.Location = New System.Drawing.Point(227, 39)
        Me.LblV_No.Size = New System.Drawing.Size(88, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Quotation No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(342, 38)
        Me.TxtV_No.Size = New System.Drawing.Size(167, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(104, 44)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(6, 39)
        Me.LblV_Date.Size = New System.Drawing.Size(95, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Quotation Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(327, 24)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(120, 38)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(226, 20)
        Me.LblV_Type.Size = New System.Drawing.Size(95, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Quotation Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(342, 18)
        Me.TxtV_Type.Size = New System.Drawing.Size(167, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(104, 24)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(6, 20)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(120, 18)
        Me.TxtSite_Code.Size = New System.Drawing.Size(100, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(42, 78)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 19)
        Me.TabControl1.Size = New System.Drawing.Size(974, 144)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LblIndentNo)
        Me.TP1.Controls.Add(Me.TxtIndentNo)
        Me.TP1.Controls.Add(Me.LblPostingGroupSalesTaxParty)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.TxtBillingType)
        Me.TP1.Controls.Add(Me.LblBillingType)
        Me.TP1.Controls.Add(Me.LblVendorQuoteNo)
        Me.TP1.Controls.Add(Me.TxtVendorQuoteNo)
        Me.TP1.Controls.Add(Me.LblVendorQuoteDate)
        Me.TP1.Controls.Add(Me.TxtVendorQuoteDate)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.TxtCurrency)
        Me.TP1.Controls.Add(Me.LblVendor)
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.LblVendorCity)
        Me.TP1.Controls.Add(Me.TxtVendorCity)
        Me.TP1.Controls.Add(Me.LblVendorReq)
        Me.TP1.Controls.Add(Me.LblVendorCountry)
        Me.TP1.Controls.Add(Me.TxtVendorCountry)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(966, 118)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorCountry, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorCountry, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorCity, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorCity, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorQuoteDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorQuoteDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorQuoteNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorQuoteNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBillingType, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillingType, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPostingGroupSalesTaxParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtIndentNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIndentNo, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(972, 41)
        Me.Topctrl1.TabIndex = 3
        '
        'Dgl1
        '
        Me.Dgl1.AgMandatoryColumn = 0
        Me.Dgl1.AgReadOnlyColumnColor = System.Drawing.Color.Ivory
        Me.Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.CancelEditingControlValidating = False
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'TxtVendor
        '
        Me.TxtVendor.AgMandatory = True
        Me.TxtVendor.AgMasterHelp = False
        Me.TxtVendor.AgNumberLeftPlaces = 8
        Me.TxtVendor.AgNumberNegetiveAllow = False
        Me.TxtVendor.AgNumberRightPlaces = 2
        Me.TxtVendor.AgPickFromLastValue = False
        Me.TxtVendor.AgRowFilter = ""
        Me.TxtVendor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendor.AgSelectedValue = Nothing
        Me.TxtVendor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendor.Location = New System.Drawing.Point(120, 58)
        Me.TxtVendor.MaxLength = 50
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(389, 18)
        Me.TxtVendor.TabIndex = 4
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(6, 58)
        Me.LblVendor.Name = "LblVendor"
        Me.LblVendor.Size = New System.Drawing.Size(48, 16)
        Me.LblVendor.TabIndex = 706
        Me.LblVendor.Text = "Vendor"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(3, 374)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(964, 21)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(818, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 672
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(707, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 671
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(432, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 670
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Maroon
        Me.Label33.Location = New System.Drawing.Point(321, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(105, 16)
        Me.Label33.TabIndex = 669
        Me.Label33.Text = "Total Measure :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(94, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 668
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(9, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 667
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(3, 186)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(964, 188)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(519, 79)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'TxtRemarks
        '
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
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(628, 78)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(328, 18)
        Me.TxtRemarks.TabIndex = 13
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(3, 165)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(260, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Quotation For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtVendorCity
        '
        Me.TxtVendorCity.AgMandatory = False
        Me.TxtVendorCity.AgMasterHelp = False
        Me.TxtVendorCity.AgNumberLeftPlaces = 8
        Me.TxtVendorCity.AgNumberNegetiveAllow = False
        Me.TxtVendorCity.AgNumberRightPlaces = 2
        Me.TxtVendorCity.AgPickFromLastValue = False
        Me.TxtVendorCity.AgRowFilter = ""
        Me.TxtVendorCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorCity.AgSelectedValue = Nothing
        Me.TxtVendorCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorCity.Location = New System.Drawing.Point(120, 78)
        Me.TxtVendorCity.MaxLength = 20
        Me.TxtVendorCity.Name = "TxtVendorCity"
        Me.TxtVendorCity.Size = New System.Drawing.Size(100, 18)
        Me.TxtVendorCity.TabIndex = 5
        '
        'LblVendorCity
        '
        Me.LblVendorCity.AutoSize = True
        Me.LblVendorCity.BackColor = System.Drawing.Color.Transparent
        Me.LblVendorCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendorCity.Location = New System.Drawing.Point(6, 78)
        Me.LblVendorCity.Name = "LblVendorCity"
        Me.LblVendorCity.Size = New System.Drawing.Size(31, 16)
        Me.LblVendorCity.TabIndex = 731
        Me.LblVendorCity.Text = "City"
        '
        'LblVendorReq
        '
        Me.LblVendorReq.AutoSize = True
        Me.LblVendorReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblVendorReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblVendorReq.Location = New System.Drawing.Point(104, 64)
        Me.LblVendorReq.Name = "LblVendorReq"
        Me.LblVendorReq.Size = New System.Drawing.Size(10, 7)
        Me.LblVendorReq.TabIndex = 733
        Me.LblVendorReq.Text = "Ä"
        '
        'TxtVendorCountry
        '
        Me.TxtVendorCountry.AgMandatory = False
        Me.TxtVendorCountry.AgMasterHelp = False
        Me.TxtVendorCountry.AgNumberLeftPlaces = 8
        Me.TxtVendorCountry.AgNumberNegetiveAllow = False
        Me.TxtVendorCountry.AgNumberRightPlaces = 2
        Me.TxtVendorCountry.AgPickFromLastValue = False
        Me.TxtVendorCountry.AgRowFilter = ""
        Me.TxtVendorCountry.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorCountry.AgSelectedValue = Nothing
        Me.TxtVendorCountry.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorCountry.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorCountry.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorCountry.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorCountry.Location = New System.Drawing.Point(342, 78)
        Me.TxtVendorCountry.MaxLength = 20
        Me.TxtVendorCountry.Name = "TxtVendorCountry"
        Me.TxtVendorCountry.Size = New System.Drawing.Size(167, 18)
        Me.TxtVendorCountry.TabIndex = 6
        '
        'LblVendorCountry
        '
        Me.LblVendorCountry.AutoSize = True
        Me.LblVendorCountry.BackColor = System.Drawing.Color.Transparent
        Me.LblVendorCountry.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendorCountry.Location = New System.Drawing.Point(227, 80)
        Me.LblVendorCountry.Name = "LblVendorCountry"
        Me.LblVendorCountry.Size = New System.Drawing.Size(53, 16)
        Me.LblVendorCountry.TabIndex = 735
        Me.LblVendorCountry.Text = "Country"
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(742, 59)
        Me.LblCurrency.Name = "LblCurrency"
        Me.LblCurrency.Size = New System.Drawing.Size(60, 16)
        Me.LblCurrency.TabIndex = 737
        Me.LblCurrency.Text = "Currency"
        '
        'TxtCurrency
        '
        Me.TxtCurrency.AgMandatory = False
        Me.TxtCurrency.AgMasterHelp = False
        Me.TxtCurrency.AgNumberLeftPlaces = 8
        Me.TxtCurrency.AgNumberNegetiveAllow = False
        Me.TxtCurrency.AgNumberRightPlaces = 2
        Me.TxtCurrency.AgPickFromLastValue = False
        Me.TxtCurrency.AgRowFilter = ""
        Me.TxtCurrency.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCurrency.AgSelectedValue = Nothing
        Me.TxtCurrency.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCurrency.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCurrency.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCurrency.Location = New System.Drawing.Point(846, 58)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(110, 18)
        Me.TxtCurrency.TabIndex = 12
        '
        'LblVendorQuoteNo
        '
        Me.LblVendorQuoteNo.AutoSize = True
        Me.LblVendorQuoteNo.BackColor = System.Drawing.Color.Transparent
        Me.LblVendorQuoteNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendorQuoteNo.Location = New System.Drawing.Point(519, 19)
        Me.LblVendorQuoteNo.Name = "LblVendorQuoteNo"
        Me.LblVendorQuoteNo.Size = New System.Drawing.Size(107, 16)
        Me.LblVendorQuoteNo.TabIndex = 741
        Me.LblVendorQuoteNo.Text = "Vendor Quote No"
        '
        'TxtVendorQuoteNo
        '
        Me.TxtVendorQuoteNo.AgMandatory = False
        Me.TxtVendorQuoteNo.AgMasterHelp = False
        Me.TxtVendorQuoteNo.AgNumberLeftPlaces = 8
        Me.TxtVendorQuoteNo.AgNumberNegetiveAllow = False
        Me.TxtVendorQuoteNo.AgNumberRightPlaces = 2
        Me.TxtVendorQuoteNo.AgPickFromLastValue = False
        Me.TxtVendorQuoteNo.AgRowFilter = ""
        Me.TxtVendorQuoteNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorQuoteNo.AgSelectedValue = Nothing
        Me.TxtVendorQuoteNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorQuoteNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorQuoteNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorQuoteNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorQuoteNo.Location = New System.Drawing.Point(628, 18)
        Me.TxtVendorQuoteNo.MaxLength = 20
        Me.TxtVendorQuoteNo.Name = "TxtVendorQuoteNo"
        Me.TxtVendorQuoteNo.Size = New System.Drawing.Size(110, 18)
        Me.TxtVendorQuoteNo.TabIndex = 7
        '
        'LblVendorQuoteDate
        '
        Me.LblVendorQuoteDate.AutoSize = True
        Me.LblVendorQuoteDate.BackColor = System.Drawing.Color.Transparent
        Me.LblVendorQuoteDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendorQuoteDate.Location = New System.Drawing.Point(745, 19)
        Me.LblVendorQuoteDate.Name = "LblVendorQuoteDate"
        Me.LblVendorQuoteDate.Size = New System.Drawing.Size(74, 16)
        Me.LblVendorQuoteDate.TabIndex = 743
        Me.LblVendorQuoteDate.Text = "Quote Date"
        '
        'TxtVendorQuoteDate
        '
        Me.TxtVendorQuoteDate.AgMandatory = False
        Me.TxtVendorQuoteDate.AgMasterHelp = False
        Me.TxtVendorQuoteDate.AgNumberLeftPlaces = 8
        Me.TxtVendorQuoteDate.AgNumberNegetiveAllow = False
        Me.TxtVendorQuoteDate.AgNumberRightPlaces = 2
        Me.TxtVendorQuoteDate.AgPickFromLastValue = False
        Me.TxtVendorQuoteDate.AgRowFilter = ""
        Me.TxtVendorQuoteDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorQuoteDate.AgSelectedValue = Nothing
        Me.TxtVendorQuoteDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorQuoteDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtVendorQuoteDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorQuoteDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorQuoteDate.Location = New System.Drawing.Point(846, 18)
        Me.TxtVendorQuoteDate.MaxLength = 20
        Me.TxtVendorQuoteDate.Name = "TxtVendorQuoteDate"
        Me.TxtVendorQuoteDate.Size = New System.Drawing.Size(110, 18)
        Me.TxtVendorQuoteDate.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 402)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 16)
        Me.Label1.TabIndex = 745
        Me.Label1.Text = "Terms && Conditions"
        '
        'TxtTermsAndConditions
        '
        Me.TxtTermsAndConditions.AgMandatory = False
        Me.TxtTermsAndConditions.AgMasterHelp = False
        Me.TxtTermsAndConditions.AgNumberLeftPlaces = 0
        Me.TxtTermsAndConditions.AgNumberNegetiveAllow = False
        Me.TxtTermsAndConditions.AgNumberRightPlaces = 0
        Me.TxtTermsAndConditions.AgPickFromLastValue = False
        Me.TxtTermsAndConditions.AgRowFilter = ""
        Me.TxtTermsAndConditions.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTermsAndConditions.AgSelectedValue = Nothing
        Me.TxtTermsAndConditions.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTermsAndConditions.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTermsAndConditions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTermsAndConditions.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTermsAndConditions.Location = New System.Drawing.Point(6, 421)
        Me.TxtTermsAndConditions.MaxLength = 0
        Me.TxtTermsAndConditions.Multiline = True
        Me.TxtTermsAndConditions.Name = "TxtTermsAndConditions"
        Me.TxtTermsAndConditions.Size = New System.Drawing.Size(368, 116)
        Me.TxtTermsAndConditions.TabIndex = 2
        '
        'TxtStructure
        '
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
        Me.TxtStructure.Location = New System.Drawing.Point(827, 166)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(137, 18)
        Me.TxtStructure.TabIndex = 746
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(760, 166)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 747
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PnlCalcGrid.Location = New System.Drawing.Point(827, 421)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(137, 134)
        Me.PnlCalcGrid.TabIndex = 748
        '
        'TxtBillingType
        '
        Me.TxtBillingType.AgMandatory = True
        Me.TxtBillingType.AgMasterHelp = False
        Me.TxtBillingType.AgNumberLeftPlaces = 8
        Me.TxtBillingType.AgNumberNegetiveAllow = False
        Me.TxtBillingType.AgNumberRightPlaces = 2
        Me.TxtBillingType.AgPickFromLastValue = False
        Me.TxtBillingType.AgRowFilter = ""
        Me.TxtBillingType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillingType.AgSelectedValue = Nothing
        Me.TxtBillingType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillingType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillingType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillingType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillingType.Location = New System.Drawing.Point(846, 38)
        Me.TxtBillingType.MaxLength = 20
        Me.TxtBillingType.Name = "TxtBillingType"
        Me.TxtBillingType.Size = New System.Drawing.Size(110, 18)
        Me.TxtBillingType.TabIndex = 10
        Me.TxtBillingType.Visible = False
        '
        'LblBillingType
        '
        Me.LblBillingType.AutoSize = True
        Me.LblBillingType.BackColor = System.Drawing.Color.Transparent
        Me.LblBillingType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBillingType.Location = New System.Drawing.Point(743, 39)
        Me.LblBillingType.Name = "LblBillingType"
        Me.LblBillingType.Size = New System.Drawing.Size(74, 16)
        Me.LblBillingType.TabIndex = 750
        Me.LblBillingType.Text = "Billing Type"
        Me.LblBillingType.Visible = False
        '
        'LblPostingGroupSalesTaxParty
        '
        Me.LblPostingGroupSalesTaxParty.AutoSize = True
        Me.LblPostingGroupSalesTaxParty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPostingGroupSalesTaxParty.Location = New System.Drawing.Point(519, 59)
        Me.LblPostingGroupSalesTaxParty.Name = "LblPostingGroupSalesTaxParty"
        Me.LblPostingGroupSalesTaxParty.Size = New System.Drawing.Size(104, 16)
        Me.LblPostingGroupSalesTaxParty.TabIndex = 752
        Me.LblPostingGroupSalesTaxParty.Text = "Sales Tax Group"
        '
        'TxtSalesTaxGroupParty
        '
        Me.TxtSalesTaxGroupParty.AgMandatory = False
        Me.TxtSalesTaxGroupParty.AgMasterHelp = False
        Me.TxtSalesTaxGroupParty.AgNumberLeftPlaces = 0
        Me.TxtSalesTaxGroupParty.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxGroupParty.AgNumberRightPlaces = 0
        Me.TxtSalesTaxGroupParty.AgPickFromLastValue = False
        Me.TxtSalesTaxGroupParty.AgRowFilter = ""
        Me.TxtSalesTaxGroupParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxGroupParty.AgSelectedValue = Nothing
        Me.TxtSalesTaxGroupParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxGroupParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxGroupParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxGroupParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(628, 58)
        Me.TxtSalesTaxGroupParty.MaxLength = 255
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(110, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 11
        '
        'LblIndentNo
        '
        Me.LblIndentNo.AutoSize = True
        Me.LblIndentNo.BackColor = System.Drawing.Color.Transparent
        Me.LblIndentNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIndentNo.Location = New System.Drawing.Point(519, 39)
        Me.LblIndentNo.Name = "LblIndentNo"
        Me.LblIndentNo.Size = New System.Drawing.Size(67, 16)
        Me.LblIndentNo.TabIndex = 754
        Me.LblIndentNo.Text = "Indent No."
        '
        'TxtIndentNo
        '
        Me.TxtIndentNo.AgMandatory = False
        Me.TxtIndentNo.AgMasterHelp = False
        Me.TxtIndentNo.AgNumberLeftPlaces = 8
        Me.TxtIndentNo.AgNumberNegetiveAllow = False
        Me.TxtIndentNo.AgNumberRightPlaces = 2
        Me.TxtIndentNo.AgPickFromLastValue = False
        Me.TxtIndentNo.AgRowFilter = ""
        Me.TxtIndentNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIndentNo.AgSelectedValue = Nothing
        Me.TxtIndentNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIndentNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtIndentNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIndentNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIndentNo.Location = New System.Drawing.Point(628, 38)
        Me.TxtIndentNo.MaxLength = 20
        Me.TxtIndentNo.Name = "TxtIndentNo"
        Me.TxtIndentNo.Size = New System.Drawing.Size(110, 18)
        Me.TxtIndentNo.TabIndex = 9
        '
        'PnlCShowGrid2
        '
        Me.PnlCShowGrid2.Location = New System.Drawing.Point(476, 421)
        Me.PnlCShowGrid2.Name = "PnlCShowGrid2"
        Me.PnlCShowGrid2.Size = New System.Drawing.Size(175, 140)
        Me.PnlCShowGrid2.TabIndex = 750
        '
        'PnlCShowGrid
        '
        Me.PnlCShowGrid.Location = New System.Drawing.Point(657, 421)
        Me.PnlCShowGrid.Name = "PnlCShowGrid"
        Me.PnlCShowGrid.Size = New System.Drawing.Size(151, 140)
        Me.PnlCShowGrid.TabIndex = 749
        '
        'TempPurchQuotation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(972, 622)
        Me.Controls.Add(Me.PnlCShowGrid2)
        Me.Controls.Add(Me.PnlCShowGrid)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.TxtStructure)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtTermsAndConditions)
        Me.Controls.Add(Me.Label1)
        Me.Name = "TempPurchQuotation"
        Me.Text = "Template Purchase Quotation"
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.TxtTermsAndConditions, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Label25, 0)
        Me.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlCShowGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlCShowGrid2, 0)
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
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtVendor As AgControls.AgTextBox
    Protected WithEvents LblVendor As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtVendorCity As AgControls.AgTextBox
    Protected WithEvents LblVendorCity As System.Windows.Forms.Label
    Protected WithEvents LblVendorReq As System.Windows.Forms.Label
    Protected WithEvents TxtVendorCountry As AgControls.AgTextBox
    Protected WithEvents LblVendorCountry As System.Windows.Forms.Label
    Protected WithEvents LblCurrency As System.Windows.Forms.Label
    Protected WithEvents TxtCurrency As AgControls.AgTextBox
    Protected WithEvents LblVendorQuoteNo As System.Windows.Forms.Label
    Protected WithEvents TxtVendorQuoteNo As AgControls.AgTextBox
    Protected WithEvents LblVendorQuoteDate As System.Windows.Forms.Label
    Protected WithEvents TxtVendorQuoteDate As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtTermsAndConditions As AgControls.AgTextBox
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents TxtBillingType As AgControls.AgTextBox
    Protected WithEvents LblBillingType As System.Windows.Forms.Label
    Protected WithEvents LblPostingGroupSalesTaxParty As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents LblIndentNo As System.Windows.Forms.Label
    Protected WithEvents TxtIndentNo As AgControls.AgTextBox
    Protected WithEvents PnlCShowGrid2 As System.Windows.Forms.Panel
    Protected WithEvents PnlCShowGrid As System.Windows.Forms.Panel
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchQuotation"
        LogTableName = "PurchQuotation_Log"
        MainLineTableCsv = "PurchQuotationDetail,Structure_TransFooter,Structure_TransLine"
        LogLineTableCsv = "PurchQuotationDetail_Log,Structure_TransFooter_Log,Structure_TransLine_Log"
        AgL.GridDesign(Dgl1)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgL.AddAgDataGrid(AgCShowGrid1, PnlCShowGrid)
        AgL.AddAgDataGrid(AgCShowGrid2, PnlCShowGrid2)

        AgCShowGrid1.Visible = False
        AgCShowGrid2.Visible = False

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select P.DocID As SearchCode " & _
            " From PurchQuotation P " & _
            " Left Join Voucher_Type Vt On P.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By P.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select P.UID As SearchCode " & _
               " From PurchQuotation_Log P " & _
               " Left Join Voucher_Type Vt On P.V_Type = Vt.V_Type  " & _
               " Where P.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By P.EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = "SELECT P.DocID, Vt.Description AS [Entry Type], P.V_Date AS [Entry Date], " & _
        '                    " P.V_No AS [Entry No], Sg.DispName As Vendor, P.VendorQuoteNo, P.VendorQuoteDate " & _
        '                    " FROM PurchQuotation P " & _
        '                    " LEFT JOIN Voucher_type Vt ON P.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup Sg On P.Vendor = Sg.SubCode " & _
        '                    " Where IsNull(P.IsDeleted,0) = 0   " & mCondStr

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Quotation Type], H.V_Prefix AS [Prefix], H.V_Date AS Date, H.V_No AS [Quotation No], " & _
                            " H.VendorName AS [Vendor Name], H.Currency, H.Structure, H.BillingType AS [Billing Type], H.VendorQuoteNo AS [Vendor Quote No], H.VendorQuoteDate AS [Vendor Quote Date],  " & _
                            " H.TermsAndConditions AS [Terms And Conditions], H.Remarks, H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure], H.TotalAmount AS [Total Amount],  " & _
                            " H.PostingGroupSalesTaxParty AS [Posting Group Sales Tax Party], H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status],  " & _
                            " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status,  H.PriceMode AS [Price Mode], " & _
                            " D.Div_Name AS Division, SM.Name AS [Site Name], PI.V_Type +'-'+ convert(NVARCHAR,PI.V_No) AS [Indent No], SF.* " & _
                            " FROM  PurchQuotation H " & _
                            " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
                            " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " & _
                            " LEFT JOIN PurchIndent PI ON PI.DocID=H.PurchIndent  " & _
                            " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQueryFooter(AgL, EntryNCat, FrmType) & ") As SF On H.DocId = SF.DocId " & _
                            " Where IsNull(H.IsDeleted,0) = 0   " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT P.UID as SearchCode, P.DocId, Vt.Description AS [Entry Type], " & _
        '                    " P.V_Date AS [Entry Date], P.V_No AS [Entry No], " & _
        '                    " P.VendorQuoteNo, P.VendorQuoteDate, Sg.DispName As Vendor " & _
        '                    " FROM PurchQuotation_Log P " & _
        '                    " LEFT JOIN Voucher_Type Vt ON P.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup Sg On P.Vendor = Sg.SubCode " & _
        '                    " Where P.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Quotation Type], H.V_Prefix AS [Prefix], H.V_Date AS Date, H.V_No AS [Quotation No], " & _
                    " H.VendorName AS [Vendor Name], H.Currency, H.Structure, H.BillingType AS [Billing Type], H.VendorQuoteNo AS [Vendor Quote No], H.VendorQuoteDate AS [Vendor Quote Date],  " & _
                    " H.TermsAndConditions AS [Terms And Conditions], H.Remarks, H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure], H.TotalAmount AS [Total Amount],  " & _
                    " H.PostingGroupSalesTaxParty AS [Posting Group Sales Tax Party], H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status],  " & _
                    " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status,  H.PriceMode AS [Price Mode], " & _
                    " D.Div_Name AS Division, SM.Name AS [Site Name], PI.V_Type +'-'+ convert(NVARCHAR,PI.V_No) AS [Indent No], SF.* " & _
                    " FROM  PurchQuotation H " & _
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
                    " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " & _
                    " LEFT JOIN PurchIndent PI ON PI.DocID=H.PurchIndent  " & _
                    " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQueryFooter(AgL, EntryNCat, FrmType) & ") As SF On H.UID = SF.UId " & _
                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr
        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1IndentNo, 150, 0, Col1IndentNo, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, Col1Item, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 120, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 4, False, Col1MeasurePerPcs, True, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 100, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 120, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1OrdQty, 100, 8, 4, False, Col1OrdQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1OrdMeasure, 100, 8, 4, False, Col1OrdMeasure, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1PurchQty, 100, 8, 4, False, Col1PurchQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1PurchMeasure, 100, 8, 4, False, Col1PurchMeasure, False, False, True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, False, True)


            .AddAgTextColumn(Dgl1, Col1QuotSelection, 100, 0, Col1QuotSelection, false, True)
            .AddAgNumberColumn(Dgl1, Col1QuotSelectionIndex, 100, 5, 0, False, Col1QuotSelectionIndex, False, True)
            .AddAgTextColumn(Dgl1, Col1QuotSelectionV_Type, 100, 0, Col1QuotSelectionV_Type, False, True)
            .AddAgNumberColumn(Dgl1, Col1QuotSelectionV_No, 100, 0, 0, False, Col1QuotSelectionV_No, False, True)
            .AddAgTextColumn(Dgl1, Col1QuotSelectionV_Date, 100, 0, Col1QuotSelectionV_Date, False, True)

        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        'Dgl1.Anchor = AnchorStyles.None
        'Panel1.Anchor = Dgl1.Anchor

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)
        AgCalcGrid1.AgFixedRows = 6
        AgCShowGrid1.AgIsFixedRows = True
        AgCShowGrid1.AgParentCalcGrid = AgCalcGrid1
        AgCShowGrid2.AgParentCalcGrid = AgCalcGrid1
        If AgCalcGrid1.RowCount > 0 Then
            AgCShowGrid1.Ini_Grid()
            AgCShowGrid2.Ini_Grid()
        End If


        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index

        Dgl1.AgSkipReadOnlyColumns = True

        FrmProductionOrder_BaseFunction_FIniList()
        'Ini_List()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE PurchQuotation_Log " & _
                " SET " & _
                " Vendor = " & AgL.Chk_Text(TxtVendor.AgSelectedValue) & ", " & _
                " VendorName = " & AgL.Chk_Text(TxtVendor.Text) & ", " & _
                " Currency = " & AgL.Chk_Text(TxtCurrency.AgSelectedValue) & ", " & _
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " & _
                " VendorQuoteNo = " & AgL.Chk_Text(TxtVendorQuoteNo.Text) & ", " & _
                " VendorQuoteDate = " & AgL.ConvertDate(TxtVendorQuoteDate.Text) & ", " & _
                " TermsAndConditions = " & AgL.Chk_Text(TxtTermsAndConditions.Text) & ", " & _
                " BillingType = " & AgL.Chk_Text(TxtBillingType.AgSelectedValue) & ", " & _
                " PostingGroupSalesTaxParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.AgSelectedValue) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " & _
                " TotalAmount = " & Val(LblTotalAmount.Text) & ", " & _
                " PurchIndent = " & AgL.Chk_Text(TxtIndentNo.AgSelectedValue) & " " & _
                " Where UID = '" & mSearchCode & "'"


        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        AgCalcGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)

        mQry = "Delete From PurchQuotationDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mSr += 1
                    mQry = "Insert Into PurchQuotationDetail_Log(UID, DocId, Sr, PurchIndent, Item, Qty, Unit, MeasurePerPcs, " & _
                            " MeasureUnit, TotalMeasure, Rate, Amount, OrdQty, OrdMeasure, " & _
                            " PurchQty, PurchMeasure, PostingGroupSalesTaxItem, " & _
                            " QuotSelection, QuotSelectionIndex, QuotSelectionV_Type, " & _
                            " QuotSelectionV_No, QuotSelectionV_Date)" & _
                            " Values(" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " & _
                            " " & mSr & ", " & _
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1IndentNo, I)) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1OrdQty, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1OrdMeasure, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1PurchQty, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1PurchMeasure, I).Value) & "," & _
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1SalesTaxGroup, I)) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1QuotSelection, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1QuotSelectionIndex, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1QuotSelectionV_Type, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1QuotSelectionV_No, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1QuotSelectionV_Date, I).Value) & " " & _
                            " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    AgCalcGrid1.Save_TransLine(mInternalCode, mSr, I, Conn, Cmd, SearchCode)
                    RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select P.* " & _
                " From PurchQuotation P " & _
                " Where P.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select P.* " & _
                " From PurchQuotation_Log P " & _
                " Where P.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.AgSelectedValue = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue

                IniGrid()

                TxtVendor.AgSelectedValue = AgL.XNull(.Rows(0)("Vendor"))
                TxtCurrency.AgSelectedValue = AgL.XNull(.Rows(0)("Currency"))
                TxtStructure.AgSelectedValue = AgL.XNull(.Rows(0)("Structure"))
                TxtBillingType.AgSelectedValue = AgL.XNull(.Rows(0)("BillingType"))
                TxtVendorQuoteNo.Text = AgL.XNull(.Rows(0)("VendorQuoteNo"))
                TxtVendorQuoteDate.Text = AgL.XNull(.Rows(0)("VendorQuoteDate"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))
                TxtIndentNo.AgSelectedValue = AgL.XNull(.Rows(0)("PurchIndent"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtSalesTaxGroupParty.AgSelectedValue = AgL.XNull(.Rows(0)("PostingGroupSalesTaxParty"))

                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                LblTotalAmount.Text = AgL.XNull(.Rows(0)("TotalAmount"))

                Call Validate_Vendor()

                AgCalcGrid1.MoveRec_TransFooter(SearchCode)

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from PurchQuotationDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from PurchQuotationDetail_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1IndentNo, I) = AgL.XNull(.Rows(I)("PurchIndent"))
                            Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))

                            'Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))


                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1OrdQty, I).Value = AgL.VNull(.Rows(I)("OrdQty"))
                            Dgl1.Item(Col1OrdMeasure, I).Value = AgL.VNull(.Rows(I)("OrdMeasure"))
                            Dgl1.Item(Col1PurchQty, I).Value = AgL.VNull(.Rows(I)("PurchQty"))
                            Dgl1.Item(Col1PurchMeasure, I).Value = AgL.VNull(.Rows(I)("PurchMeasure"))
                            Dgl1.AgSelectedValue(Col1SalesTaxGroup, I) = AgL.XNull(.Rows(I)("PostingGroupSalesTaxItem"))

                            Dgl1.Item(Col1QuotSelection, I).Value = AgL.XNull(.Rows(I)("QuotSelection"))
                            Dgl1.Item(Col1QuotSelectionIndex, I).Value = AgL.VNull(.Rows(I)("QuotSelectionIndex"))
                            Dgl1.Item(Col1QuotSelectionV_Type, I).Value = AgL.XNull(.Rows(I)("QuotSelectionV_Type"))
                            Dgl1.Item(Col1QuotSelectionV_No, I).Value = AgL.VNull(.Rows(I)("QuotSelectionV_No"))
                            Dgl1.Item(Col1QuotSelectionV_Date, I).Value = AgL.XNull(.Rows(I)("QuotSelectionV_Date"))

                            Call AgCalcGrid1.MoveRec_TransLine(mSearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                            RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If
                End With
                AgCShowGrid1.MoveRec_FromCalcGrid()
                AgCShowGrid2.MoveRec_FromCalcGrid()
                'Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        TxtVendor.AgHelpDataSet(7, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Vendor
        TxtCurrency.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Currency
        TxtBillingType.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.BillingType
        TxtSalesTaxGroupParty.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.SalesTaxGroupParty
        TxtIndentNo.AgHelpDataSet(5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.IndentNo
        Dgl1.AgHelpDataSet(Col1IndentNo, 5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.IndentNo
        Dgl1.AgHelpDataSet(Col1Item, 5) = HelpDataSet.Item
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                If Dgl1.Item(Col1IndentNo, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                    mQry = " SELECT PID.Item AS Code ,I.Description AS Item,PID.IndentQty AS Qty,PID.Unit, " & _
                            " I.ItemType,IsNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code,I.Measure, I.MeasureUnit, I.SalesTaxPostingGroup," & _
                            " isnull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status " & _
                            " FROM PurchIndentDetail PID " & _
                            " LEFT JOIN Item I ON I.Code=PID.Item  " & _
                            " WHERE PID.DocId = " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1IndentNo, Dgl1.CurrentCell.RowIndex)) & " "
                    Dgl1.AgHelpDataSet(Col1Item, 9) = AgL.FillData(mQry, AgL.GCn)
                Else
                    Dgl1.AgHelpDataSet(Col1Item, 9) = HelpDataSet.Item
                End If
                'Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0  And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' "
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0 : LblTotalMeasure.Text = 0 : LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                If AgL.StrCmp(TxtBillingType.Text, "Qty") Or TxtBillingType.Text = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else ': AgL.StrCmp(TxtBillingType.Text, "Area")
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Amount, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                End If

                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        AgCalcGrid1.Calculation()
        LblTotalMeasure.Text = Val(LblTotalMeasure.Text)
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtVendor, LblVendor.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If TxtIndentNo.AgSelectedValue <> "" Then
            If Validate_PurchIndent(TxtIndentNo) = False Then passed = False : Exit Sub
        End If

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With

        passed = FCheckDuplicateVendorNo()
    End Sub

    Private Function FCheckDuplicateVendorNo() As Boolean
        FCheckDuplicateVendorNo = True

        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM PurchQuotation WHERE Vendor = '" & TxtVendor.AgSelectedValue & "' And VendorQuoteNo = '" & TxtVendorQuoteNo.Text & "'   " & _
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateVendorNo = False : MsgBox("Vendor Quotation No. Already Exists") : TxtVendorQuoteNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM PurchQuotation WHERE Vendor = '" & TxtVendor.AgSelectedValue & "' And VendorQuoteNo = '" & TxtVendorQuoteNo.Text & "'  " & _
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateVendorNo = False : MsgBox("Vendor Quotation No. Already Exists") : TxtVendorQuoteNo.Focus()
        End If
    End Function

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0 : LblTotalAmount.Text = 0
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
                TxtV_Type.Validating, TxtVendor.Validating, TxtBillingType.Validating, _
                TxtRemarks.Validating, TxtTermsAndConditions.Validating, TxtV_Date.Validating, _
                TxtVendorQuoteDate.Validating, TxtVendorQuoteNo.Validating, TxtSalesTaxGroupParty.Validating, TxtIndentNo.Validating
        Try
            Select Case sender.name
                Case TxtVendor.Name
                    Call Validate_Vendor()

                Case TxtSalesTaxGroupParty.Name
                    AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
                    Calculation()

                Case TxtIndentNo.Name
                    e.Cancel = Not Validate_PurchIndent(TxtIndentNo)
                    If e.Cancel = False Then
                        Call ProcFillPurchIndentDetail(TxtIndentNo.AgSelectedValue)
                    End If

                Case TxtV_Type.Name
                    Call ProcFillTermCondition()

                Case TxtVendorQuoteNo.Name
                    e.Cancel = Not FCheckDuplicateVendorNo()

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validate_Vendor()
        Dim DrTemp As DataRow() = Nothing
        Try
            DrTemp = TxtVendor.AgHelpDataSet.Tables(0).Select("Code = '" & TxtVendor.AgSelectedValue & "' ")
            If DrTemp.Length > 0 Then
                TxtVendorCity.Text = AgL.XNull(DrTemp(0)("VendorCity"))
                TxtVendorCountry.Text = AgL.XNull(DrTemp(0)("VendorCountry"))
                If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("SalesTaxPostingGroup")), "") Then
                    TxtSalesTaxGroupParty.AgSelectedValue = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                    AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtVendor.Enter, TxtCurrency.Enter, TxtBillingType.Enter, TxtRemarks.Enter, TxtTermsAndConditions.Enter, TxtV_Date.Enter, TxtSalesTaxGroupParty.Enter, TxtIndentNo.Enter
        Try
            Select Case sender.name
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    IniGrid()

                Case TxtVendor.Name
                    'TxtVendor.AgRowFilter = " IsDeleted = 0  " & _
                    '    " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                    '    " And " & ClsMain.RetDivFilterStr & ""
                    TxtVendor.AgRowFilter = " IsDeleted = 0  " & _
                        " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "

                Case TxtCurrency.Name
                    TxtCurrency.AgRowFilter = " IsDeleted = 0 "

                Case TxtSalesTaxGroupParty.Name
                    TxtSalesTaxGroupParty.AgRowFilter = " Active = 1 "

                Case TxtIndentNo.Name
                    TxtIndentNo.AgRowFilter = " IsDeleted = 0 " & _
                        " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                        " And " & ClsMain.RetDivFilterStr & " "
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
            Else
                If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                    DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("Measure"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow) = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                        Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow) = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                    End If

                    If Dgl1.Item(Col1IndentNo, mRow).Value <> "" Then
                        Dgl1.Item(Col1Qty, mRow).Value = AgL.XNull(DrTemp(0)("Qty"))
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
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
                Case Col1Item
                    Validating_Item(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mRowIndex)

                Case Col1IndentNo
                    e.Cancel = Not Validate_PurchIndent(Dgl1, Dgl1.CurrentCell.RowIndex)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempPurchQuotation_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        ProcFillTermCondition()
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
        TxtBillingType.AgSelectedValue = "Qty"
        TxtSalesTaxGroupParty.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue

    End Sub

    Private Sub TempPurchQuotation_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtVendorCity.Enabled = False
        TxtVendorCountry.Enabled = False
    End Sub

    'Private Sub TempPurchQuotation_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
    '    Dim mCrd As New ReportDocument
    '    Dim ReportView As New AgLibrary.RepView
    '    Dim DsRep As New DataSet
    '    Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
    '    Dim bTableName As String = "", bSecTableName As String = "", bCondstr As String = ""
    '    Try
    '        Me.Cursor = Cursors.WaitCursor

    '        If FrmType = ClsMain.EntryPointType.Main Then
    '            AgL.PubReportTitle = "Purchase Quotation"
    '            RepName = "Rug_PurchQuotation_Print" : RepTitle = "Purchase Quotation"
    '            bTableName = "PurchQuotation" : bSecTableName = "PurchQuotationDetail L ON H.DocID = L.DocID"
    '            bCondstr = "WHERE H.DocID='" & SearchCode & "'"
    '        Else
    '            AgL.PubReportTitle = "Purchase Quotation Log"
    '            RepName = "Rug_PurchQuotation_Print" : RepTitle = "Purchase Quotation Log"
    '            bTableName = "PurchQuotation_Log" : bSecTableName = "PurchQuotationDetail_Log  L ON H.UID = L.UID "
    '            bCondstr = "WHERE H.UID='" & SearchCode & "'"
    '        End If

    '        strQry = "SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, " & _
    '                " H.Vendor, H.VendorName, H.Currency, H.Structure, H.BillingType, H.VendorQuoteNo, " & _
    '                " H.VendorQuoteDate, H.TermsAndConditions, H.Remarks, H.TotalQty, H.TotalMeasure As HeaderTotalMeasure, " & _
    '                " H.TotalAmount, H.EntryBy, H.EntryDate, H.EntryType, H.EntryStatus, H.ApproveBy, " & _
    '                " H.ApproveDate, H.MoveToLog, H.MoveToLogDate, H.IsDeleted, H.Status, H.UID, " & _
    '                " H.PriceMode, H.PostingGroupSalesTaxParty, " & _
    '                " L.Sr, L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit,  " & _
    '                " L.TotalMeasure, L.Rate, L.Amount, L.OrdQty, L.OrdMeasure, L.PurchQty, " & _
    '                " L.PurchMeasure, L.PostingGroupSalesTaxItem,  " & _
    '                " Vt.Description + '/' + Convert(NVARCHAR, H.V_No) AS PurchaseQuotationNo, " & _
    '                " H.VendorName, C.CityName, C.Country, I.Description AS ItemDescription, SF.*, SL.* " & _
    '                " FROM " & bTableName & " H  " & _
    '                " LEFT JOIN " & bSecTableName & " " & _
    '                " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQueryFooter(AgL, EntryNCat) & ") As SF On H.DocId = SF.DocId " & _
    '                " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQuery(AgL, EntryNCat) & ") As SL On L.DocId = SL.DocId And L.Sr = Sl.TSr " & _
    '                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
    '                " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " & _
    '                " LEFT JOIN City C ON Sg.CityCode = C.CityCode " & _
    '                " LEFT JOIN Item I ON L.Item = I.Code " & _
    '                "  " & bCondstr & ""


    '        AgL.ADMain = New SqlClient.SqlDataAdapter(strQry, AgL.GCn)
    '        AgL.ADMain.Fill(DsRep)
    '        AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
    '        mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
    '        mCrd.SetDataSource(DsRep.Tables(0))
    '        CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
    '        AgPL.Formula_Set(mCrd, RepTitle)
    '        AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)

    '        Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
    '    Catch Ex As Exception
    '        MsgBox(Ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub ProcFillPurchIndentDetail(ByVal PurchIndent As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            Dgl1.AgHelpDataSet(Col1Item, 9) = HelpDataSet.Item

            mQry = "SELECT L.*, I.SalesTaxPostingGroup " & _
                    " FROM PurchIndentDetail L " & _
                    " LEFT JOIN Item I On L.Item = I.Code " & _
                    " WHERE L.DocId = '" & PurchIndent & "'"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.AgSelectedValue(Col1IndentNo, I) = AgL.XNull(.Rows(I)("DocId"))
                        Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("IndentQty"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalIndentMeasure")), "0.000")
                        Dgl1.AgSelectedValue(Col1SalesTaxGroup, I) = AgL.XNull(.Rows(I)("SalesTaxPostingGroup"))

                        If AgL.StrCmp(Dgl1.Item(Col1SalesTaxGroup, I).Value, "") Then
                            Dgl1.AgSelectedValue(Col1SalesTaxGroup, I) = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                        End If

                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AgCalcGrid1_Calculated() Handles AgCalcGrid1.Calculated
        AgCShowGrid1.MoveRec_FromCalcGrid()
        AgCShowGrid2.MoveRec_FromCalcGrid()
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try

            Dim bRData As String
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo + ', ' FROM (SELECT DISTINCT H.V_Type + '-' + Convert(VARCHAR,H.V_No) AS VNo FROM PurchOrder  H  WHERE H.PurchQuotaion  = '" & TxtDocId.Text & "') AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox(" Purchase Order " & bRData & " created against Quotation No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
                FGetRelationalData = True
                Exit Function
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
            FGetRelationalData = True
        End Try
    End Function


    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = Not FGetRelationalData()
    End Sub

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub

    Private Sub TempPurchQuotation_BaseFunction_Create() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = "Select V.SubCode As Code, Sg.DispName As Vendor, Sg.ManualCode , " & _
                " C.CityName As VendorCity, C.Country As VendorCountry, " & _
                " IsNull(Sg.IsDeleted,0) As IsDeleted,  " & _
                " IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) As Status,  " & _
                " Sg.SalesTaxPostingGroup, Sg.Div_Code  " & _
                " From Vendor V " & _
                " LEFT JOIN SubGroup Sg On V.SubCode = Sg.SubCode " & _
                " LEFT JOIN City C On Sg.CityCode = C.CityCode "
        HelpDataSet.Vendor = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT C.Code, C.Code AS Currency, IsNull(C.IsDeleted,0) as IsDeleted   FROM Currency C"
        HelpDataSet.Currency = AgL.FillData(mQry, AgL.GCn)

        HelpDataSet.BillingType = AgL.FillData(ClsMain.HelpQueries.BillingType, AgL.GCn)

        mQry = "SELECT DISTINCT Description AS Code, Description, IsNull(Active,0) As Active  FROM PostingGroupSalesTaxParty "
        HelpDataSet.SalesTaxGroupParty = AgL.FillData(mQry, AgL.GCn)

        'Start Code Change By Satyam on 18/11/2011
        mQry = " SELECT P.DocID AS Code,P.V_Type + '-' +convert(NVARCHAR(5),P.V_No) AS [Indent No], " & _
                " SG.DispName AS [Indent By],P.V_Date AS [Indent Date], " & _
                " isnull(P.IsDeleted,0) AS IsDeleted, P.Div_Code , " & _
                " isnull(P.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status, Vt.NCat, " & _
                " P.MoveToLog " & _
                " FROM PurchIndent  P " & _
                " LEFT JOIN SubGroup SG On SG.SubCode = P.Indentor " & _
                " LEFT JOIN Voucher_Type Vt On P.V_Type = Vt.V_Type "
        HelpDataSet.IndentNo = AgL.FillData(mQry, AgL.GCn)
        'End Code Change By Satyam on 18/11/2011
        mQry = "SELECT I.Code, I.Description, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " & _
                " IsNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code,  " & _
                " IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, I.Measure, " & _
                " I.MeasureUnit, I.SalesTaxPostingGroup " & _
                " FROM Item I "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Function Validate_PurchIndent(ByVal Sender As Object, Optional ByVal RowIndex As Integer = 0) As Boolean
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet = Nothing
        Try
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Function
            Select Case Sender.Name
                Case Dgl1.Name
                    If Dgl1.AgSelectedValue(Col1IndentNo, RowIndex) <> "" Then
                        DrTemp = Dgl1.AgHelpDataSet(Col1IndentNo).Tables(0).Select("Code = '" & Dgl1.AgSelectedValue(Col1IndentNo, RowIndex) & "' ")
                        If DrTemp.Length > 0 Then
                            If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("MoveToLog")), "") Then
                                MsgBox("Currently Purchase Indent """ & Dgl1.Item(Col1IndentNo, RowIndex).Value & """ Is In Log For Modification." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                                Dgl1.AgSelectedValue(Col1IndentNo, RowIndex) = ""
                                Exit Function
                            End If

                            If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("Status")), AgTemplate.ClsMain.EntryStatus.Active) Then
                                MsgBox("Currently Purchase Indent """ & Dgl1.Item(Col1IndentNo, RowIndex).Value & """ Is Not In Active State." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                                Dgl1.AgSelectedValue(Col1IndentNo, RowIndex) = ""
                                Exit Function
                            End If
                        End If
                    End If
                    Validate_PurchIndent = True

                Case TxtIndentNo.Name
                    If TxtIndentNo.AgSelectedValue <> "" Then
                        DrTemp = TxtIndentNo.AgHelpDataSet().Tables(0).Select("Code = '" & TxtIndentNo.AgSelectedValue & "' ")
                        If DrTemp.Length > 0 Then
                            If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("MoveToLog")), "") Then
                                MsgBox("Currently Purchase Indent """ & TxtIndentNo.Text & """ Is In Log For Modification." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                                TxtIndentNo.AgSelectedValue = "" : Exit Function
                            End If

                            If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("Status")), AgTemplate.ClsMain.EntryStatus.Active) Then
                                MsgBox("Currently Purchase Indent """ & TxtIndentNo.Text & """ Is Not In Active State." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                                TxtIndentNo.AgSelectedValue = "" : Exit Function
                            End If
                        End If
                    End If
                    Validate_PurchIndent = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    'Start Code By Satyam on 19/11/2011
    Private Sub ProcFillTermCondition()
        If TxtTermsAndConditions.Text = "" Then
            If TxtV_Type.Text.ToString.Trim <> "" Or TxtV_Type.AgSelectedValue.Trim <> "" Then
                mQry = " SELECT ISNULL(H.Description,'')  FROM TermsCondition H " & _
                        " WHERE H.Code ='" & TxtV_Type.AgSelectedValue & "' "
                TxtTermsAndConditions.Text = AgL.Dman_Execute(mQry, AgL.GCn, AgL.ECmd).ExecuteScalar
            End If
        End If
    End Sub
    'End Code By Satyam on 19/11/2011

End Class
