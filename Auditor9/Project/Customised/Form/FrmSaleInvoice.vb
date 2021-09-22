Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmSaleInvoice
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1TransactionType As String = "Type"
    Protected Const Col1ImportStatus As String = "Import Status"
    Protected Const Col1SaleOrder As String = "Sale Order"
    Protected Const Col1SaleOrderSr As String = "Sale Order Sr"
    Protected Const Col1SaleOrderRatePerQty As String = "Sale Order Rate Per Qty"
    Protected Const Col1SaleOrderRatePerMeasure As String = "Sale Order Rate Per Measure"
    Protected Const Col1Item_UID As String = "Item_UID"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Item_Invoiced As String = "Item Invoiced"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1RateType As String = "Rate Type"
    Protected Const Col1DeliveryMeasure As String = "Delivery Measure"
    Protected Const Col1BaleNo As String = "Bale No"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1DocQty As String = "Doc Qty"
    Protected Const Col1FreeQty As String = "Free Qty"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalDocMeasure As String = "Total Doc Measure"
    Protected Const Col1TotalFreeMeasure As String = "Total Free Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1DeliveryMeasurePerPcs As String = "Delivery Measure Per Qty"
    Protected Const Col1TotalDocDeliveryMeasure As String = "Total Doc Delivery Measure"
    Protected Const Col1TotalFreeDeliveryMeasure As String = "Total Free Delivery Measure"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1RatePerQty As String = "Rate Per Qty"
    Protected Const Col1RatePerMeasure As String = "Rate Per Measure"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1MRP As String = "MRP"
    Protected Const Col1ExpiryDate As String = "Expiry Date"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1ReferenceDocId As String = "Purchase No"
    Protected Const Col1ReferenceDocIdSr As String = "Reference DocId Sr"
    Protected Const Col1PurchaseRate As String = "Purchase Rate"
    Protected Const Col1SaleChallan As String = "Sale Challan"
    Protected Const Col1SaleChallanSr As String = "Sale Challan Sr"

    '========================================================================

    Private Const ChallanType_Sale = "Sale"
    Private Const ChallanType_Return = "Return"
    Private Const ChallanType_DirectReturn = "Direct Ret"

    Dim HelpDataSet_Sale As DataSet = Nothing
    Dim HelpDataSet_Return As DataSet = Nothing
    Dim HelpDataSet_DirectReturn As DataSet = Nothing
    Protected WithEvents RbtSaleOrder As System.Windows.Forms.RadioButton

    Dim Dgl As New AgControls.AgDataGrid

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSaleInvoice))
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSaleToParty = New AgControls.AgTextBox
        Me.LblBuyer = New System.Windows.Forms.Label
        Me.PnlTotals = New System.Windows.Forms.Panel
        Me.LblTotalBale = New System.Windows.Forms.Label
        Me.LblTotalBaleText = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasure = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasureText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtReferenceNo = New AgControls.AgTextBox
        Me.LblReferenceNo = New System.Windows.Forms.Label
        Me.LblCurrency = New System.Windows.Forms.Label
        Me.TxtCurrency = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtCreditDays = New AgControls.AgTextBox
        Me.LblCreditDays = New System.Windows.Forms.Label
        Me.TxtCreditLimit = New AgControls.AgTextBox
        Me.LblCreditLimit = New System.Windows.Forms.Label
        Me.TxtCurrBal = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblNature = New System.Windows.Forms.Label
        Me.TxtNature = New AgControls.AgTextBox
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtBillToParty = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button
        Me.LblGodown = New System.Windows.Forms.Label
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblPurchaseRate = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.TxtPaidAmt = New AgControls.AgTextBox
        Me.LblPaidAmt = New System.Windows.Forms.Label
        Me.LblHelp = New System.Windows.Forms.Label
        Me.LblAgent = New System.Windows.Forms.Label
        Me.TxtAgent = New AgControls.AgTextBox
        Me.TxtUpLine = New AgControls.AgTextBox
        Me.GrpDirectChallan = New System.Windows.Forms.GroupBox
        Me.RbtForSaleChallan = New System.Windows.Forms.RadioButton
        Me.RbtDirect = New System.Windows.Forms.RadioButton
        Me.BtnFillSaleChallan = New System.Windows.Forms.Button
        Me.RbtSaleOrder = New System.Windows.Forms.RadioButton
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
        Me.GrpDirectChallan.SuspendLayout()
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(648, 581)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(29, 19)
        Me.TxtMoveToLog.Tag = ""
        '
        'CmdMoveToLog
        '
        Me.CmdMoveToLog.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(467, 581)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
        Me.TxtApproveBy.Size = New System.Drawing.Size(116, 18)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(168, 581)
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
        Me.GBoxDivision.Location = New System.Drawing.Point(320, 581)
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
        Me.Label2.Location = New System.Drawing.Point(99, 32)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(11, 27)
        Me.LblV_Date.Size = New System.Drawing.Size(78, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Invoice Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(311, 12)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(115, 26)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(221, 8)
        Me.LblV_Type.Size = New System.Drawing.Size(78, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Invoice Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(329, 6)
        Me.TxtV_Type.Size = New System.Drawing.Size(200, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(99, 12)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(11, 7)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(115, 6)
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
        Me.LblPrefix.Location = New System.Drawing.Point(336, 267)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 17)
        Me.TabControl1.Size = New System.Drawing.Size(992, 116)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtUpLine)
        Me.TP1.Controls.Add(Me.TxtCreditDays)
        Me.TP1.Controls.Add(Me.LblAgent)
        Me.TP1.Controls.Add(Me.LblCreditDays)
        Me.TP1.Controls.Add(Me.TxtCreditLimit)
        Me.TP1.Controls.Add(Me.LblCreditLimit)
        Me.TP1.Controls.Add(Me.TxtAgent)
        Me.TP1.Controls.Add(Me.TxtCurrBal)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtBillToParty)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.LblNature)
        Me.TP1.Controls.Add(Me.TxtNature)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.Panel3)
        Me.TP1.Controls.Add(Me.Panel2)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtSaleToParty)
        Me.TP1.Controls.Add(Me.LblBuyer)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtReferenceNo)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Controls.Add(Me.Label27)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 90)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBuyer, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSaleToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel2, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCurrBal, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtAgent, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCreditLimit, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCreditLimit, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCreditDays, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblAgent, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCreditDays, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtUpLine, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 5
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(99, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 694
        Me.Label4.Text = "Ä"
        '
        'TxtSaleToParty
        '
        Me.TxtSaleToParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleToParty.AgLastValueTag = Nothing
        Me.TxtSaleToParty.AgLastValueText = Nothing
        Me.TxtSaleToParty.AgMandatory = True
        Me.TxtSaleToParty.AgMasterHelp = False
        Me.TxtSaleToParty.AgNumberLeftPlaces = 8
        Me.TxtSaleToParty.AgNumberNegetiveAllow = False
        Me.TxtSaleToParty.AgNumberRightPlaces = 2
        Me.TxtSaleToParty.AgPickFromLastValue = False
        Me.TxtSaleToParty.AgRowFilter = ""
        Me.TxtSaleToParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleToParty.AgSelectedValue = Nothing
        Me.TxtSaleToParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleToParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleToParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleToParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleToParty.Location = New System.Drawing.Point(115, 46)
        Me.TxtSaleToParty.MaxLength = 0
        Me.TxtSaleToParty.Name = "TxtSaleToParty"
        Me.TxtSaleToParty.Size = New System.Drawing.Size(383, 18)
        Me.TxtSaleToParty.TabIndex = 4
        '
        'LblBuyer
        '
        Me.LblBuyer.AutoSize = True
        Me.LblBuyer.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyer.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyer.Location = New System.Drawing.Point(11, 46)
        Me.LblBuyer.Name = "LblBuyer"
        Me.LblBuyer.Size = New System.Drawing.Size(39, 16)
        Me.LblBuyer.TabIndex = 693
        Me.LblBuyer.Text = "Party"
        '
        'PnlTotals
        '
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalBale)
        Me.PnlTotals.Controls.Add(Me.LblTotalBaleText)
        Me.PnlTotals.Controls.Add(Me.LblTotalDeliveryMeasure)
        Me.PnlTotals.Controls.Add(Me.LblTotalDeliveryMeasureText)
        Me.PnlTotals.Controls.Add(Me.LblTotalMeasure)
        Me.PnlTotals.Controls.Add(Me.LblTotalMeasureText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(4, 386)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 694
        '
        'LblTotalBale
        '
        Me.LblTotalBale.AutoSize = True
        Me.LblTotalBale.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBale.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBale.Location = New System.Drawing.Point(725, 4)
        Me.LblTotalBale.Name = "LblTotalBale"
        Me.LblTotalBale.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBale.TabIndex = 716
        Me.LblTotalBale.Text = "."
        Me.LblTotalBale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalBaleText
        '
        Me.LblTotalBaleText.AutoSize = True
        Me.LblTotalBaleText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBaleText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalBaleText.Location = New System.Drawing.Point(633, 3)
        Me.LblTotalBaleText.Name = "LblTotalBaleText"
        Me.LblTotalBaleText.Size = New System.Drawing.Size(86, 16)
        Me.LblTotalBaleText.TabIndex = 715
        Me.LblTotalBaleText.Text = "Total Bales :"
        '
        'LblTotalDeliveryMeasure
        '
        Me.LblTotalDeliveryMeasure.AutoSize = True
        Me.LblTotalDeliveryMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(537, 3)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 714
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.AutoSize = True
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(370, 3)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(161, 16)
        Me.LblTotalDeliveryMeasureText.TabIndex = 713
        Me.LblTotalDeliveryMeasureText.Text = "Total Deilvery Measure :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(282, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.AutoSize = True
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(171, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Total Measure :"
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
        Me.Pnl1.Location = New System.Drawing.Point(4, 158)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 227)
        Me.Pnl1.TabIndex = 1
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
        'TxtSalesTaxGroupParty
        '
        Me.TxtSalesTaxGroupParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxGroupParty.AgLastValueTag = Nothing
        Me.TxtSalesTaxGroupParty.AgLastValueText = Nothing
        Me.TxtSalesTaxGroupParty.AgMandatory = False
        Me.TxtSalesTaxGroupParty.AgMasterHelp = False
        Me.TxtSalesTaxGroupParty.AgNumberLeftPlaces = 8
        Me.TxtSalesTaxGroupParty.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxGroupParty.AgNumberRightPlaces = 2
        Me.TxtSalesTaxGroupParty.AgPickFromLastValue = False
        Me.TxtSalesTaxGroupParty.AgRowFilter = ""
        Me.TxtSalesTaxGroupParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxGroupParty.AgSelectedValue = Nothing
        Me.TxtSalesTaxGroupParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxGroupParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxGroupParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxGroupParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(649, 27)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(123, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 7
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(539, 28)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(104, 16)
        Me.Label27.TabIndex = 717
        Me.Label27.Text = "Sales Tax Group"
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
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(66, 433)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(320, 18)
        Me.TxtRemarks.TabIndex = 2
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(1, 435)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtReferenceNo.AgLastValueTag = Nothing
        Me.TxtReferenceNo.AgLastValueText = Nothing
        Me.TxtReferenceNo.AgMandatory = False
        Me.TxtReferenceNo.AgMasterHelp = True
        Me.TxtReferenceNo.AgNumberLeftPlaces = 8
        Me.TxtReferenceNo.AgNumberNegetiveAllow = False
        Me.TxtReferenceNo.AgNumberRightPlaces = 2
        Me.TxtReferenceNo.AgPickFromLastValue = False
        Me.TxtReferenceNo.AgRowFilter = ""
        Me.TxtReferenceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReferenceNo.AgSelectedValue = Nothing
        Me.TxtReferenceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReferenceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReferenceNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferenceNo.Location = New System.Drawing.Point(329, 26)
        Me.TxtReferenceNo.MaxLength = 20
        Me.TxtReferenceNo.Name = "TxtReferenceNo"
        Me.TxtReferenceNo.Size = New System.Drawing.Size(200, 18)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.AutoSize = True
        Me.LblReferenceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblReferenceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(221, 26)
        Me.LblReferenceNo.Name = "LblReferenceNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(71, 16)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Invoice No."
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
        'TxtCurrency
        '
        Me.TxtCurrency.AgAllowUserToEnableMasterHelp = False
        Me.TxtCurrency.AgLastValueTag = Nothing
        Me.TxtCurrency.AgLastValueText = Nothing
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
        Me.TxtCurrency.Location = New System.Drawing.Point(445, 482)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(84, 18)
        Me.TxtCurrency.TabIndex = 6
        Me.TxtCurrency.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 137)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Sale Invoice For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(670, 413)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(308, 157)
        Me.PnlCalcGrid.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(311, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 737
        Me.Label1.Text = "Ä"
        '
        'TxtCreditDays
        '
        Me.TxtCreditDays.AgAllowUserToEnableMasterHelp = False
        Me.TxtCreditDays.AgLastValueTag = Nothing
        Me.TxtCreditDays.AgLastValueText = Nothing
        Me.TxtCreditDays.AgMandatory = False
        Me.TxtCreditDays.AgMasterHelp = False
        Me.TxtCreditDays.AgNumberLeftPlaces = 8
        Me.TxtCreditDays.AgNumberNegetiveAllow = False
        Me.TxtCreditDays.AgNumberRightPlaces = 0
        Me.TxtCreditDays.AgPickFromLastValue = False
        Me.TxtCreditDays.AgRowFilter = ""
        Me.TxtCreditDays.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCreditDays.AgSelectedValue = Nothing
        Me.TxtCreditDays.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCreditDays.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCreditDays.BackColor = System.Drawing.Color.White
        Me.TxtCreditDays.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCreditDays.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.TxtCreditDays.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditDays.Location = New System.Drawing.Point(870, 47)
        Me.TxtCreditDays.MaxLength = 20
        Me.TxtCreditDays.Name = "TxtCreditDays"
        Me.TxtCreditDays.ReadOnly = True
        Me.TxtCreditDays.Size = New System.Drawing.Size(104, 18)
        Me.TxtCreditDays.TabIndex = 10
        Me.TxtCreditDays.TabStop = False
        Me.TxtCreditDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCreditDays.UseWaitCursor = True
        '
        'LblCreditDays
        '
        Me.LblCreditDays.AutoSize = True
        Me.LblCreditDays.BackColor = System.Drawing.Color.Transparent
        Me.LblCreditDays.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditDays.Location = New System.Drawing.Point(778, 49)
        Me.LblCreditDays.Name = "LblCreditDays"
        Me.LblCreditDays.Size = New System.Drawing.Size(76, 16)
        Me.LblCreditDays.TabIndex = 739
        Me.LblCreditDays.Text = "Credit Days"
        '
        'TxtCreditLimit
        '
        Me.TxtCreditLimit.AgAllowUserToEnableMasterHelp = False
        Me.TxtCreditLimit.AgLastValueTag = Nothing
        Me.TxtCreditLimit.AgLastValueText = Nothing
        Me.TxtCreditLimit.AgMandatory = False
        Me.TxtCreditLimit.AgMasterHelp = False
        Me.TxtCreditLimit.AgNumberLeftPlaces = 8
        Me.TxtCreditLimit.AgNumberNegetiveAllow = False
        Me.TxtCreditLimit.AgNumberRightPlaces = 0
        Me.TxtCreditLimit.AgPickFromLastValue = False
        Me.TxtCreditLimit.AgRowFilter = ""
        Me.TxtCreditLimit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCreditLimit.AgSelectedValue = Nothing
        Me.TxtCreditLimit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCreditLimit.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCreditLimit.BackColor = System.Drawing.Color.White
        Me.TxtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCreditLimit.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.TxtCreditLimit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditLimit.Location = New System.Drawing.Point(649, 47)
        Me.TxtCreditLimit.MaxLength = 20
        Me.TxtCreditLimit.Name = "TxtCreditLimit"
        Me.TxtCreditLimit.ReadOnly = True
        Me.TxtCreditLimit.Size = New System.Drawing.Size(123, 18)
        Me.TxtCreditLimit.TabIndex = 9
        Me.TxtCreditLimit.TabStop = False
        Me.TxtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCreditLimit.UseWaitCursor = True
        '
        'LblCreditLimit
        '
        Me.LblCreditLimit.AutoSize = True
        Me.LblCreditLimit.BackColor = System.Drawing.Color.Transparent
        Me.LblCreditLimit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditLimit.Location = New System.Drawing.Point(539, 48)
        Me.LblCreditLimit.Name = "LblCreditLimit"
        Me.LblCreditLimit.Size = New System.Drawing.Size(74, 16)
        Me.LblCreditLimit.TabIndex = 741
        Me.LblCreditLimit.Text = "Credit Limit"
        '
        'TxtCurrBal
        '
        Me.TxtCurrBal.AgAllowUserToEnableMasterHelp = False
        Me.TxtCurrBal.AgLastValueTag = Nothing
        Me.TxtCurrBal.AgLastValueText = Nothing
        Me.TxtCurrBal.AgMandatory = False
        Me.TxtCurrBal.AgMasterHelp = False
        Me.TxtCurrBal.AgNumberLeftPlaces = 8
        Me.TxtCurrBal.AgNumberNegetiveAllow = False
        Me.TxtCurrBal.AgNumberRightPlaces = 2
        Me.TxtCurrBal.AgPickFromLastValue = False
        Me.TxtCurrBal.AgRowFilter = ""
        Me.TxtCurrBal.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCurrBal.AgSelectedValue = Nothing
        Me.TxtCurrBal.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCurrBal.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCurrBal.BackColor = System.Drawing.Color.White
        Me.TxtCurrBal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCurrBal.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.TxtCurrBal.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCurrBal.Location = New System.Drawing.Point(870, 27)
        Me.TxtCurrBal.MaxLength = 20
        Me.TxtCurrBal.Name = "TxtCurrBal"
        Me.TxtCurrBal.ReadOnly = True
        Me.TxtCurrBal.Size = New System.Drawing.Size(104, 18)
        Me.TxtCurrBal.TabIndex = 8
        Me.TxtCurrBal.TabStop = False
        Me.TxtCurrBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCurrBal.UseWaitCursor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(778, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 16)
        Me.Label3.TabIndex = 743
        Me.Label3.Text = "Curr. Balance"
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
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(503, 44)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(26, 20)
        Me.BtnFillPartyDetail.TabIndex = 5
        Me.BtnFillPartyDetail.TabStop = False
        Me.BtnFillPartyDetail.Text = "F"
        Me.BtnFillPartyDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPartyDetail.UseVisualStyleBackColor = True
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(4, 457)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(382, 113)
        Me.PnlCustomGrid.TabIndex = 3
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(486, 594)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1011
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(99, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 3003
        Me.Label5.Text = "Ä"
        '
        'TxtBillToParty
        '
        Me.TxtBillToParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtBillToParty.AgLastValueTag = Nothing
        Me.TxtBillToParty.AgLastValueText = Nothing
        Me.TxtBillToParty.AgMandatory = True
        Me.TxtBillToParty.AgMasterHelp = False
        Me.TxtBillToParty.AgNumberLeftPlaces = 8
        Me.TxtBillToParty.AgNumberNegetiveAllow = False
        Me.TxtBillToParty.AgNumberRightPlaces = 2
        Me.TxtBillToParty.AgPickFromLastValue = False
        Me.TxtBillToParty.AgRowFilter = ""
        Me.TxtBillToParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillToParty.AgSelectedValue = Nothing
        Me.TxtBillToParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillToParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillToParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillToParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillToParty.Location = New System.Drawing.Point(115, 66)
        Me.TxtBillToParty.MaxLength = 0
        Me.TxtBillToParty.Name = "TxtBillToParty"
        Me.TxtBillToParty.Size = New System.Drawing.Size(414, 18)
        Me.TxtBillToParty.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 16)
        Me.Label6.TabIndex = 3002
        Me.Label6.Text = "Post to A/c"
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
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(58, 9)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnImprtFromExcel.TabIndex = 669
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(1, 414)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 3005
        Me.LblGodown.Text = "Godown"
        '
        'TxtGodown
        '
        Me.TxtGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtGodown.AgLastValueTag = Nothing
        Me.TxtGodown.AgLastValueText = Nothing
        Me.TxtGodown.AgMandatory = False
        Me.TxtGodown.AgMasterHelp = False
        Me.TxtGodown.AgNumberLeftPlaces = 0
        Me.TxtGodown.AgNumberNegetiveAllow = False
        Me.TxtGodown.AgNumberRightPlaces = 0
        Me.TxtGodown.AgPickFromLastValue = False
        Me.TxtGodown.AgRowFilter = ""
        Me.TxtGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGodown.AgSelectedValue = Nothing
        Me.TxtGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGodown.Location = New System.Drawing.Point(66, 413)
        Me.TxtGodown.MaxLength = 255
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(320, 18)
        Me.TxtGodown.TabIndex = 1
        '
        'LblPurchaseRate
        '
        Me.LblPurchaseRate.AutoSize = True
        Me.LblPurchaseRate.Location = New System.Drawing.Point(562, 426)
        Me.LblPurchaseRate.Name = "LblPurchaseRate"
        Me.LblPurchaseRate.Size = New System.Drawing.Size(39, 13)
        Me.LblPurchaseRate.TabIndex = 1014
        Me.LblPurchaseRate.Text = "Label7"
        Me.LblPurchaseRate.Visible = False
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
        Me.Panel3.TabIndex = 11
        '
        'TxtPaidAmt
        '
        Me.TxtPaidAmt.AgAllowUserToEnableMasterHelp = False
        Me.TxtPaidAmt.AgLastValueTag = Nothing
        Me.TxtPaidAmt.AgLastValueText = Nothing
        Me.TxtPaidAmt.AgMandatory = False
        Me.TxtPaidAmt.AgMasterHelp = False
        Me.TxtPaidAmt.AgNumberLeftPlaces = 0
        Me.TxtPaidAmt.AgNumberNegetiveAllow = False
        Me.TxtPaidAmt.AgNumberRightPlaces = 0
        Me.TxtPaidAmt.AgPickFromLastValue = False
        Me.TxtPaidAmt.AgRowFilter = ""
        Me.TxtPaidAmt.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPaidAmt.AgSelectedValue = Nothing
        Me.TxtPaidAmt.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPaidAmt.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtPaidAmt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPaidAmt.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPaidAmt.Location = New System.Drawing.Point(553, 552)
        Me.TxtPaidAmt.MaxLength = 255
        Me.TxtPaidAmt.Name = "TxtPaidAmt"
        Me.TxtPaidAmt.Size = New System.Drawing.Size(115, 18)
        Me.TxtPaidAmt.TabIndex = 1015
        '
        'LblPaidAmt
        '
        Me.LblPaidAmt.AutoSize = True
        Me.LblPaidAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaidAmt.Location = New System.Drawing.Point(479, 553)
        Me.LblPaidAmt.Name = "LblPaidAmt"
        Me.LblPaidAmt.Size = New System.Drawing.Size(61, 16)
        Me.LblPaidAmt.TabIndex = 1016
        Me.LblPaidAmt.Text = "Paid Amt"
        '
        'LblHelp
        '
        Me.LblHelp.AutoSize = True
        Me.LblHelp.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHelp.Location = New System.Drawing.Point(736, 140)
        Me.LblHelp.Name = "LblHelp"
        Me.LblHelp.Size = New System.Drawing.Size(241, 16)
        Me.LblHelp.TabIndex = 3006
        Me.LblHelp.Text = "S - Sale  R - Return  D - Direct Return"
        Me.LblHelp.Visible = False
        '
        'LblAgent
        '
        Me.LblAgent.AutoSize = True
        Me.LblAgent.BackColor = System.Drawing.Color.Transparent
        Me.LblAgent.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAgent.Location = New System.Drawing.Point(539, 8)
        Me.LblAgent.Name = "LblAgent"
        Me.LblAgent.Size = New System.Drawing.Size(42, 16)
        Me.LblAgent.TabIndex = 3005
        Me.LblAgent.Text = "Agent"
        '
        'TxtAgent
        '
        Me.TxtAgent.AgAllowUserToEnableMasterHelp = False
        Me.TxtAgent.AgLastValueTag = Nothing
        Me.TxtAgent.AgLastValueText = Nothing
        Me.TxtAgent.AgMandatory = False
        Me.TxtAgent.AgMasterHelp = False
        Me.TxtAgent.AgNumberLeftPlaces = 8
        Me.TxtAgent.AgNumberNegetiveAllow = False
        Me.TxtAgent.AgNumberRightPlaces = 2
        Me.TxtAgent.AgPickFromLastValue = False
        Me.TxtAgent.AgRowFilter = ""
        Me.TxtAgent.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAgent.AgSelectedValue = Nothing
        Me.TxtAgent.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAgent.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAgent.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAgent.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAgent.Location = New System.Drawing.Point(649, 7)
        Me.TxtAgent.MaxLength = 20
        Me.TxtAgent.Name = "TxtAgent"
        Me.TxtAgent.Size = New System.Drawing.Size(325, 18)
        Me.TxtAgent.TabIndex = 6
        '
        'TxtUpLine
        '
        Me.TxtUpLine.AgAllowUserToEnableMasterHelp = False
        Me.TxtUpLine.AgLastValueTag = Nothing
        Me.TxtUpLine.AgLastValueText = Nothing
        Me.TxtUpLine.AgMandatory = False
        Me.TxtUpLine.AgMasterHelp = False
        Me.TxtUpLine.AgNumberLeftPlaces = 8
        Me.TxtUpLine.AgNumberNegetiveAllow = False
        Me.TxtUpLine.AgNumberRightPlaces = 2
        Me.TxtUpLine.AgPickFromLastValue = False
        Me.TxtUpLine.AgRowFilter = ""
        Me.TxtUpLine.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUpLine.AgSelectedValue = Nothing
        Me.TxtUpLine.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUpLine.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUpLine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUpLine.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUpLine.Location = New System.Drawing.Point(649, 67)
        Me.TxtUpLine.MaxLength = 20
        Me.TxtUpLine.Name = "TxtUpLine"
        Me.TxtUpLine.Size = New System.Drawing.Size(325, 18)
        Me.TxtUpLine.TabIndex = 3006
        Me.TxtUpLine.Visible = False
        '
        'GrpDirectChallan
        '
        Me.GrpDirectChallan.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectChallan.Controls.Add(Me.RbtSaleOrder)
        Me.GrpDirectChallan.Controls.Add(Me.RbtForSaleChallan)
        Me.GrpDirectChallan.Controls.Add(Me.RbtDirect)
        Me.GrpDirectChallan.Location = New System.Drawing.Point(237, 129)
        Me.GrpDirectChallan.Name = "GrpDirectChallan"
        Me.GrpDirectChallan.Size = New System.Drawing.Size(338, 27)
        Me.GrpDirectChallan.TabIndex = 3008
        Me.GrpDirectChallan.TabStop = False
        '
        'RbtForSaleChallan
        '
        Me.RbtForSaleChallan.AutoSize = True
        Me.RbtForSaleChallan.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForSaleChallan.Location = New System.Drawing.Point(125, 7)
        Me.RbtForSaleChallan.Name = "RbtForSaleChallan"
        Me.RbtForSaleChallan.Size = New System.Drawing.Size(131, 17)
        Me.RbtForSaleChallan.TabIndex = 0
        Me.RbtForSaleChallan.TabStop = True
        Me.RbtForSaleChallan.Text = "For Sale Challan"
        Me.RbtForSaleChallan.UseVisualStyleBackColor = True
        '
        'RbtDirect
        '
        Me.RbtDirect.AutoSize = True
        Me.RbtDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtDirect.Location = New System.Drawing.Point(260, 7)
        Me.RbtDirect.Name = "RbtDirect"
        Me.RbtDirect.Size = New System.Drawing.Size(64, 17)
        Me.RbtDirect.TabIndex = 743
        Me.RbtDirect.TabStop = True
        Me.RbtDirect.Text = "Direct"
        Me.RbtDirect.UseVisualStyleBackColor = True
        '
        'BtnFillSaleChallan
        '
        Me.BtnFillSaleChallan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillSaleChallan.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillSaleChallan.Location = New System.Drawing.Point(577, 135)
        Me.BtnFillSaleChallan.Name = "BtnFillSaleChallan"
        Me.BtnFillSaleChallan.Size = New System.Drawing.Size(29, 21)
        Me.BtnFillSaleChallan.TabIndex = 3007
        Me.BtnFillSaleChallan.Text = "..."
        Me.BtnFillSaleChallan.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillSaleChallan.UseVisualStyleBackColor = True
        '
        'RbtSaleOrder
        '
        Me.RbtSaleOrder.AutoSize = True
        Me.RbtSaleOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtSaleOrder.Location = New System.Drawing.Point(4, 7)
        Me.RbtSaleOrder.Name = "RbtSaleOrder"
        Me.RbtSaleOrder.Size = New System.Drawing.Size(120, 17)
        Me.RbtSaleOrder.TabIndex = 744
        Me.RbtSaleOrder.TabStop = True
        Me.RbtSaleOrder.Text = "For Sale Order"
        Me.RbtSaleOrder.UseVisualStyleBackColor = True
        '
        'FrmSaleInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.LblHelp)
        Me.Controls.Add(Me.BtnFillSaleChallan)
        Me.Controls.Add(Me.LblPaidAmt)
        Me.Controls.Add(Me.TxtPaidAmt)
        Me.Controls.Add(Me.LblPurchaseRate)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtGodown)
        Me.Controls.Add(Me.LblGodown)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.TxtCurrency)
        Me.Controls.Add(Me.GrpDirectChallan)
        Me.Name = "FrmSaleInvoice"
        Me.Text = "Sale Invoice"
        Me.Controls.SetChildIndex(Me.GrpDirectChallan, 0)
        Me.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.Controls.SetChildIndex(Me.Label30, 0)
        Me.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.LblPurchaseRate, 0)
        Me.Controls.SetChildIndex(Me.TxtPaidAmt, 0)
        Me.Controls.SetChildIndex(Me.LblPaidAmt, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.BtnFillSaleChallan, 0)
        Me.Controls.SetChildIndex(Me.LblHelp, 0)
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
        Me.GrpDirectChallan.ResumeLayout(False)
        Me.GrpDirectChallan.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents LblBuyer As System.Windows.Forms.Label
    Protected WithEvents TxtSaleToParty As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents PnlTotals As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents Label27 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Public WithEvents TxtReferenceNo As AgControls.AgTextBox
    Protected WithEvents LblReferenceNo As System.Windows.Forms.Label
    Protected WithEvents TxtCurrency As AgControls.AgTextBox
    Protected WithEvents LblCurrency As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtCreditDays As AgControls.AgTextBox
    Protected WithEvents LblCreditDays As System.Windows.Forms.Label
    Protected WithEvents TxtCreditLimit As AgControls.AgTextBox
    Protected WithEvents LblCreditLimit As System.Windows.Forms.Label
    Protected WithEvents LblNature As System.Windows.Forms.Label
    Protected WithEvents TxtNature As AgControls.AgTextBox
    Protected WithEvents TxtCurrBal As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalBale As System.Windows.Forms.Label
    Protected WithEvents LblTotalBaleText As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents TxtBillToParty As AgControls.AgTextBox
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents TxtPaidAmt As AgControls.AgTextBox
    Protected WithEvents LblPaidAmt As System.Windows.Forms.Label
    Protected WithEvents LblPurchaseRate As System.Windows.Forms.Label
    Protected WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents Panel2 As System.Windows.Forms.Panel
    Protected WithEvents LblHelp As System.Windows.Forms.Label
    Protected WithEvents LblAgent As System.Windows.Forms.Label
    Protected WithEvents TxtAgent As AgControls.AgTextBox
    Protected WithEvents TxtUpLine As AgControls.AgTextBox
    Protected WithEvents GrpDirectChallan As System.Windows.Forms.GroupBox
    Protected WithEvents RbtForSaleChallan As System.Windows.Forms.RadioButton
    Protected WithEvents RbtDirect As System.Windows.Forms.RadioButton
    Protected WithEvents BtnFillSaleChallan As System.Windows.Forms.Button
#End Region

    Private Sub FrmSaleInvoice_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim DtSaleInvoice As DataTable = Nothing
        Dim I As Integer = 0

        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From SaleChallanDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From SaleChallan Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SaleInvoice"
        LogTableName = "SaleInvoice_Log"
        MainLineTableCsv = "SaleInvoiceDetail"
        LogLineTableCsv = "SaleInvoiceDetail_Log"

        AgL.GridDesign(Dgl1)
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

        mQry = "Select DocID As SearchCode " &
                " From SaleInvoice H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where 1 = 1  " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Invoice_Type], H.V_Date AS Date, Supplier.Name as Supplier_Name, SGV.Name AS [Party], " &
                            " H.ReferenceNo AS [Manual_No], H.SalesTaxGroupParty AS [Sales_Tax_Group_Party], " &
                            " H.Remarks, H.TotalQty AS [Total_Qty], H.TotalMeasure AS [Total_Measure], H.TotalAmount AS [Total_Amount],  " &
                            " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type] " &
                            " FROM SaleInvoice H " &
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup Supplier ON Supplier.SubCode  = H.Supplier " &
                            " LEFT JOIN SubGroup SGV ON SGV.SubCode  = H.SaleToParty " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1TransactionType, 50, 0, Col1TransactionType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionType")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1ImportStatus, 50, 0, Col1ImportStatus, False, True)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 80, 0, Col1Item_UID, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 130, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_Invoiced, 130, 0, Col1Item_Invoiced, False, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 130, 0, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1SaleOrder, 60, 0, Col1SaleOrder, False, True)
            .AddAgTextColumn(Dgl1, Col1SaleOrderSr, 40, 5, Col1SaleOrderSr, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1SaleOrderRatePerQty, 80, 8, 4, False, Col1SaleOrderRatePerQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1SaleOrderRatePerMeasure, 80, 8, 4, False, Col1SaleOrderRatePerMeasure, False, True, True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, False, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 70, 50, Col1BillingType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BillingType")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1RateType, 100, 50, Col1RateType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RateType")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 60, 255, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 60, 255, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 8, 4, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1FreeQty, 80, 8, 4, False, Col1FreeQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeQty")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Qty")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1RatePerQty, 100, 8, 2, False, Col1RatePerQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1RatePerMeasure, 100, 8, 2, False, Col1RatePerMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1MRP, 70, 8, 2, False, Col1MRP, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MRP")), Boolean), True, True)
            .AddAgDateColumn(Dgl1, Col1ExpiryDate, 90, Col1ExpiryDate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ExpiryDate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocMeasure, 70, 8, 3, False, Col1TotalDocMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeMeasure, 70, 8, 4, False, Col1TotalFreeMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeMeasure")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 4, False, Col1TotalMeasure, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 60, 0, Col1MeasureUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 100, 8, 4, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasurePerPcs, 110, 8, 4, False, Col1DeliveryMeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocDeliveryMeasure, 120, 8, 3, False, Col1TotalDocDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeDeliveryMeasure, 70, 8, 3, False, Col1TotalFreeDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeMeasure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 85, 8, 4, False, Col1TotalDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True, True)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 50, Col1DeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 50, 0, Col1DeliveryMeasureDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocId, 80, 0, Col1ReferenceDocId, True, True)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocIdSr, 40, 5, Col1ReferenceDocIdSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchaseRate, 150, 255, Col1PurchaseRate, False, False)
            .AddAgTextColumn(Dgl1, Col1SaleChallan, 150, 255, Col1SaleChallan, False, True)
            .AddAgTextColumn(Dgl1, Col1SaleChallanSr, 40, 5, Col1SaleChallanSr, False, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35

        AgCalcGrid1.Ini_Grid(EntryNCat, TxtV_Date.Text)


        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingPartyAc = TxtSaleToParty.AgSelectedValue

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgL.ProcCreateLink(Dgl1, Col1SaleOrder)
        AgL.ProcCreateLink(Dgl1, Col1ImportStatus)

        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.AgAllowFind = False

        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index

        Dgl1.AllowUserToOrderColumns = True

        AgCalcGrid1.Name = "AgCalcGrid1"
        AgCustomGrid1.Name = "AgCustomGrid1"

        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bStockSelectionQry$ = ""

        mQry = " Update SaleInvoice " &
                " SET  " &
                " ReferenceNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                " SaleToParty = " & AgL.Chk_Text(TxtSaleToParty.Tag) & ", " &
                " SaleToPartyName = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyName.Text & "', " &
                " SaleToPartyAdd1 = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyAdd1.Text & "', " &
                " SaleToPartyAdd2 = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyAdd2.Text & "', " &
                " SaleToPartyCity = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyCity.AgSelectedValue & "', " &
                " SaleToPartyMobile = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyMobile.Text & "', " &
                " BillToParty = " & AgL.Chk_Text(TxtBillToParty.Tag) & ", " &
                " Currency = " & AgL.Chk_Text(TxtCurrency.Tag) & ", " &
                " SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Text) & ", " &
                " Agent = " & AgL.Chk_Text(TxtAgent.Tag) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " CreditDays = " & Val(TxtCreditDays.Text) & ", " &
                " CreditLimit = " & Val(TxtCreditLimit.Text) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                " InvoiceGenType = " & AgL.Chk_Text(bInvoiceType) & ", " &
                " Godown = " & AgL.Chk_Text(TxtGodown.Tag) & ", " &
                " UpLine = " & AgL.Chk_Text(TxtUpLine.Text) & ", " &
                " PaidAmt = " & Val(TxtPaidAmt.Text) & ", " &
                " TotalQty = " & Val(LblTotalQty.Text) & ", " &
                " TotalAmount = " & Val(LblTotalAmount.Text) & ", " &
                " TotalBale = " & Val(LblTotalBale.Text) & ", " &
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " &
                " TotalDeliveryMeasure = " & Val(LblTotalDeliveryMeasure.Text) & ", " &
                " " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = " UPDATE SaleInvoice Set SaleInvoice.SaleToPartyTinNo = SubGroup.TINNo, " &
        '        " SaleInvoice.SaleToPartyCstNo = SubGroup.CSTNo , SaleInvoice.SaleToPartyLstNo  = SubGroup.LSTNo " &
        '        " From SubGroup " &
        '        " Where SaleInvoice.SaleToParty = SubGroup.SubCode And SaleInvoice.DocId = '" & mSearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From SaleInvoiceDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1

                If AgL.StrCmp(Dgl1.Item(Col1TransactionType, I).Value, ChallanType_Return) Or AgL.StrCmp(Dgl1.Item(Col1TransactionType, I).Value, ChallanType_DirectReturn) Then
                    Dgl1.Item(Col1Qty, I).Value = -Math.Abs(Val(Dgl1.Item(Col1Qty, I).Value))
                End If

                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1TransactionType, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1SaleOrder, I).Tag) & ", " &
                                    " " & Val(Dgl1.Item(Col1SaleOrderSr, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Item_Invoiced, I).Tag) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                    " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1RatePerQty, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1RatePerMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1ExpiryDate, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & " , " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & " , " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & " , " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, I).Tag) & ", " &
                                    " " & Val(Dgl1.Item(Col1ReferenceDocIdSr, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1SaleChallan, I).Tag) & ", " &
                                    " " & Val(Dgl1.Item(Col1SaleChallanSr, I).Value) & ", " &
                                    " " & AgL.Chk_Text(mSearchCode) & ", " & Val(mSr) & ", " &
                                    " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
            End If
        Next


        mQry = "Insert Into SaleInvoiceDetail(DocId, Sr, TransactionType, SaleOrder, SaleOrderSr, Item_Uid, Item, ItemInvoiced, Specification, SalesTaxGroupItem, " &
               " DocQty, FreeQty, Qty, Unit, MeasurePerPcs, MeasureUnit, " &
               " TotalDocMeasure, TotalFreeMeasure, TotalMeasure, Rate, RatePerQty, RatePerMeasure, Amount, MRP, ExpiryDate, Remark, " &
               " BillingType, RateType, BaleNo, LotNo, DeliveryMeasure, " &
               " DeliveryMeasureMultiplier, DeliveryMeasurePerPcs, TotalDocDeliveryMeasure, TotalFreeDeliveryMeasure, TotalDeliveryMeasure, ReferenceDocId, ReferenceDocIdSr, " &
               " SaleChallan, SaleChallanSr, SaleInvoice, SaleInvoiceSr, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bSelectionQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        Call FPostInSaleChallan(Conn, Cmd)

        Dim mNarr As String = "Being goods sold to " & TxtSaleToParty.Text & ""
        Call AgTemplate.ClsMain.PostStructureLineToAccounts(AgCalcGrid1, mNarr, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue,
                           TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtReferenceNo.Text, TxtBillToParty.AgSelectedValue, TxtV_Date.Text, Conn, Cmd)

        mQry = " UPDATE Ledger Set CreditDays = " & Val(TxtCreditDays.Text) & " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        If Val(TxtPaidAmt.Text) <> 0 And (Not AgL.StrCmp(TxtNature.Text, "Cash")) Then
            Call AccountPosting(Conn, Cmd)
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = " Select H.*, Sg.Name || ',' || IfNull(C1.CityName,'') As SaleToPartyDesc, " &
               " BillToParty.Name || ',' || IfNull(BillToPartyCity.CityName,'') As BillToPartyDesc, " &
               " C.Description As CurrencyDesc, C1.CityName As SaleToPartyCityName, Agent.Name As AgentName, BillToParty.Nature " &
               " From (Select * From SaleInvoice  Where DocID='" & SearchCode & "') H " &
               " LEFT JOIN SubGroup Sg  ON H.SaleToParty = Sg.SubCode " &
               " LEFT JOIN SubGroup BillToParty  ON H.BillToParty = BillToParty.SubCode " &
               " LEFT JOIN Currency C  ON H.Currency = C.Code " &
               " LEFT JOIN City C1  On H.SaleToPartyCity = C1.CityCode " &
               " LEFT JOIN City BillToPartyCity  On BillToParty.CityCode = BillToPartyCity.CityCode " &
               " LEFT JOIN SubGroup Agent  On H.Agent = Agent.SubCode "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag

                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtSaleToParty.Tag = AgL.XNull(.Rows(0)("SaleToParty"))
                TxtSaleToParty.Text = AgL.XNull(.Rows(0)("SaleToPartyDesc"))
                TxtBillToParty.Tag = AgL.XNull(.Rows(0)("BillToParty"))
                TxtBillToParty.Text = AgL.XNull(.Rows(0)("BillToPartyDesc"))
                TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                TxtCurrency.Text = AgL.XNull(.Rows(0)("CurrencyDesc"))

                TxtAgent.Tag = AgL.XNull(.Rows(0)("Agent"))
                TxtAgent.Text = AgL.XNull(.Rows(0)("AgentName"))

                TxtNature.Text = AgL.XNull(.Rows(0)("BillToParty.Nature"))
                TxtUpLine.Text = AgL.XNull(.Rows(0)("UpLine"))

                Call FGetCurrBal(TxtSaleToParty.AgSelectedValue)

                TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtSalesTaxGroupParty.Text = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtCreditDays.Text = AgL.VNull(.Rows(0)("CreditDays"))
                TxtCreditLimit.Text = AgL.VNull(.Rows(0)("CreditLimit"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalBale.Text = AgL.VNull(.Rows(0)("TotalBale"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                LblTotalDeliveryMeasure.Text = AgL.VNull(.Rows(0)("TotalDeliveryMeasure"))

                TxtPaidAmt.Text = AgL.VNull(.Rows(0)("PaidAmt"))

                Dim FrmObj As New FrmSaleInvoicePartyDetail
                FrmObj.TxtSaleToPartyMobile.Text = AgL.XNull(.Rows(0)("SaleToPartyMobile"))
                FrmObj.TxtSaleToPartyName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                FrmObj.TxtSaleToPartyAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd1"))
                FrmObj.TxtSaleToPartyAdd2.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd2"))
                FrmObj.TxtSaleToPartyCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                FrmObj.TxtSaleToPartyCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))

                BtnFillPartyDetail.Tag = FrmObj

                'AgCustomGrid1.MoveRec_TransFooter(SearchCode)

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), EntryNCat, TxtV_Date.Text)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                'mQry = "Select L.*, I.Description As ItemDesc, I.ManualCode, " & _
                '        " O.V_Type || '-' || O.ReferenceNo As OrderRefNo, " & _
                '        " Pc.V_Type || '-' || Pc.ReferenceNo As PurchaseNo, " & _
                '        " Sch.V_Type || '-' || Sch.ReferenceNo As SaleChallanNo, " & _
                '        " OD.RatePerQty as SaleOrderRatePerQty, OD.RatePerMeasure As SaleOrderRatePerMeasure, " & _
                '        " U.DecimalPlaces, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DMU.DecimalPlaces as DeliveryMeasureDecimalPlaces, " & _
                '        " Pcl.Landed_Value/Pcl.Qty as PurchaseRate, Pid.DocId As PurchIndent " & _
                '        " From (Select * From SaleInvoiceDetail  Where DocId = '" & SearchCode & "') As L " & _
                '        " LEFT JOIN Item I  ON L.Item = I.Code " & _
                '        " LEFT JOIN SaleInvoice C  On L.DocId = C.DocId " & _
                '        " LEFT JOIN SaleOrder O  On L.SaleOrder = O.DocId " & _
                '        " LEFT JOIN SaleOrderDetail OD  On L.SaleOrder = OD.DocId And L.SaleOrderSr = OD.Sr " & _
                '        " LEFT JOIN PurchChallan Pc On L.ReferenceDocId = Pc.DocId " & _
                '        " LEFT JOIN PurchChallanDetail pcl on L.ReferenceDocId = pcl.docid And l.ReferenceDocIdSr = Pcl.Sr  " & _
                '        " LEFT JOIN PurchIndentDetail Pid On L.DocId = Pid.DocId And L.Item = Pid.Item " & _
                '        " LEFT JOIN SaleChallan Sch On L.SaleChallan = Sch.DocId " & _
                '        " Left Join Unit U On L.Unit = U.Code " & _
                '        " Left Join Unit MU On L.MeasureUnit = MU.Code " & _
                '        " LEFT JOIN Unit Dmu On L.DeliveryMeasure = Dmu.Code " & _
                '        " Order By L.Sr "

                mQry = "Select L.*, I.Description As ItemDesc, II.Description As ItemInvoicedDesc , I.ManualCode, " &
                        " O.V_Type || '-' || O.ReferenceNo As OrderRefNo, " &
                        " Stock.V_Type || '-' || Stock.RecID As PurchaseNo, " &
                        " Sch.V_Type || '-' || Sch.ReferenceNo As SaleChallanNo, " &
                        " OD.RatePerQty as SaleOrderRatePerQty, OD.RatePerMeasure As SaleOrderRatePerMeasure, " &
                        " U.DecimalPlaces, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DMU.DecimalPlaces as DeliveryMeasureDecimalPlaces, " &
                        " (Stock.Landed_Value/Stock.Qty_Rec) + (Stock.Landed_Value/Stock.Qty_Rec)*1/100 as PurchaseRate, Pid.DocId As PurchIndent " &
                        " From (Select * From SaleInvoiceDetail  Where DocId = '" & SearchCode & "') As L " &
                        " LEFT JOIN Item I  ON L.Item = I.Code " &
                        " LEFT JOIN Item II  ON L.ItemInvoiced = II.Code " &
                        " LEFT JOIN SaleInvoice C  On L.DocId = C.DocId " &
                        " LEFT JOIN SaleOrder O  On L.SaleOrder = O.DocId " &
                        " LEFT JOIN SaleOrderDetail OD  On L.SaleOrder = OD.DocId And L.SaleOrderSr = OD.Sr " &
                        " LEFT JOIN Stock on L.ReferenceDocId = Stock.docid And l.ReferenceDocIdSr = Stock.Sr  " &
                        " LEFT JOIN PurchIndentDetail Pid On L.DocId = Pid.DocId And L.Item = Pid.Item " &
                        " LEFT JOIN SaleChallan Sch On L.SaleChallan = Sch.DocId " &
                        " Left Join Unit U On L.Unit = U.Code " &
                        " Left Join Unit MU On L.MeasureUnit = MU.Code " &
                        " LEFT JOIN Unit Dmu On L.DeliveryMeasure = Dmu.Code " &
                        " Order By L.Sr "

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                            Dgl1.Item(Col1TransactionType, I).Value = AgL.XNull(.Rows(I)("TransactionType"))

                            Dgl1.Item(Col1SaleOrder, I).Tag = AgL.XNull(.Rows(I)("SaleOrder"))
                            Dgl1.Item(Col1SaleOrder, I).Value = AgL.XNull(.Rows(I)("OrderRefNo"))
                            Dgl1.Item(Col1SaleOrderSr, I).Value = AgL.VNull(.Rows(I)("SaleOrderSr"))
                            Dgl1.Item(Col1SaleOrderRatePerQty, I).Value = AgL.VNull(.Rows(I)("SaleOrderRatePerQty"))
                            Dgl1.Item(Col1SaleOrderRatePerMeasure, I).Value = AgL.VNull(.Rows(I)("SaleOrderRatePerMeasure"))

                            Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UID"))

                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("I.ManualCode"))

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl1.Item(Col1Item_Invoiced, I).Tag = AgL.XNull(.Rows(I)("ItemInvoiced"))
                            Dgl1.Item(Col1Item_Invoiced, I).Value = AgL.XNull(.Rows(I)("ItemInvoicedDesc"))


                            If AgL.XNull(.Rows(I)("PurchIndent")) <> "" Then
                                Dgl1.Item(ColSNo, I).Style.ForeColor = Color.Red
                                Dgl1.Item(ColSNo, I).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
                            End If

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))

                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))

                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1FreeQty, I).Value = Format(AgL.VNull(.Rows(I)("FreeQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1RatePerQty, I).Value = AgL.VNull(.Rows(I)("RatePerQty"))
                            Dgl1.Item(Col1RatePerMeasure, I).Value = AgL.VNull(.Rows(I)("RatePerMeasure"))
                            Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")

                            Dgl1.Item(Col1MRP, I).Value = Format(AgL.VNull(.Rows(I)("MRP")), "0.00")
                            Dgl1.Item(Col1ExpiryDate, I).Value = AgL.XNull(.Rows(I)("ExpiryDate"))

                            Dgl1.Item(Col1PurchaseRate, I).Value = Format(AgL.VNull(.Rows(I)("PurchaseRate")), "0.00")

                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("RateType"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))

                            Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(.Rows(I)("ReferenceDocId"))
                            Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(.Rows(I)("PurchaseNo"))
                            Dgl1.Item(Col1ReferenceDocIdSr, I).Value = AgL.VNull(.Rows(I)("ReferenceDocIdSr"))

                            Dgl1.Item(Col1SaleChallan, I).Tag = AgL.XNull(.Rows(I)("SaleChallan"))
                            Dgl1.Item(Col1SaleChallan, I).Value = AgL.XNull(.Rows(I)("SaleChallanNo"))
                            Dgl1.Item(Col1SaleChallanSr, I).Value = AgL.VNull(.Rows(I)("SaleChallanSr"))

                            FFormatRateCells(I)

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                End With
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

                '-------------------------------------------------------------

                Dgl1.Columns(Col1ImportStatus).Visible = False

            End If
        End With

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtSaleToParty.Validating, TxtSalesTaxGroupParty.Validating, TxtReferenceNo.Validating, TxtBillToParty.Validating, TxtAgent.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim FrmObj As New FrmSaleInvoicePartyDetail
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue

                    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                    IniGrid()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "SaleInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

                Case TxtSaleToParty.Name
                    If TxtV_Date.Text <> "" And TxtSaleToParty.Text <> "" Then
                        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                        TxtCurrency.Tag = AgL.XNull(DrTemp(0)("Currency"))
                        TxtCurrency.Text = AgL.XNull(DrTemp(0)("CurrencyDesc"))

                        TxtSalesTaxGroupParty.Text = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                        TxtSalesTaxGroupParty.Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))

                        TxtCreditDays.Text = AgL.VNull(DrTemp(0)("CreditDays"))
                        TxtCreditLimit.Text = AgL.VNull(DrTemp(0)("CreditLimit"))

                        TxtNature.Text = AgL.XNull(DrTemp(0)("Nature"))

                        FGetCurrBal(TxtSaleToParty.AgSelectedValue)
                        If AgL.StrCmp(TxtNature.Text, "Cash") Then
                            FOpenPartyDetail()
                        Else
                            mQry = " Select Mobile As SaleToPartyMobile, DispName As SaleToPartyName, " &
                                    " IfNull(Add1,'') As SaleToPartyAdd1, IfNull(Add2,'') As SaleToPartyAdd2, " &
                                    " Sg.CityCode As SaleToPartyCity, C.CityName As SaleToPartyCityName " &
                                    " From SubGroup Sg " &
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode " &
                                    " Where Sg.SubCode = '" & TxtSaleToParty.AgSelectedValue & "'  "
                            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                            With DtTemp
                                FrmObj.TxtSaleToPartyMobile.Text = AgL.XNull(.Rows(0)("SaleToPartyMobile"))
                                FrmObj.TxtSaleToPartyName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                                FrmObj.TxtSaleToPartyAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd1"))
                                FrmObj.TxtSaleToPartyAdd2.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd2"))
                                FrmObj.TxtSaleToPartyCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                                FrmObj.TxtSaleToPartyCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))
                            End With
                            BtnFillPartyDetail.Tag = FrmObj
                        End If
                        TxtBillToParty.Tag = TxtSaleToParty.Tag
                        TxtBillToParty.Text = TxtSaleToParty.Text
                    End If

                Case TxtSalesTaxGroupParty.Name
                    AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
                    Calculation()

                Case TxtReferenceNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "SaleInvoice",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)

                Case TxtBillToParty.Name
                    If TxtBillToParty.Text <> "" Then
                        If TxtBillToParty.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtNature.Text = AgL.XNull(DrTemp(0)("Nature"))
                        End If
                    End If

                Case TxtAgent.Name
                    If TxtAgent.Text <> "" Then
                        If TxtAgent.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtUpLine.Text = AgL.XNull(DrTemp(0)("UpLine"))
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGetCurrBal(ByVal Party As String)
        mQry = " Select IfNull(Sum(AmtDr),0) - IfNull(Sum(AmtCr),0) As CurrBal From Ledger Where SubCode = '" & Party & "' And V_Date <= '" & TxtV_Date.Text & "'"
        TxtCurrBal.Text = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = EntryNCat

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        BtnFillPartyDetail.Tag = Nothing

        RbtDirect.Checked = True
        BtnFillSaleChallan.Enabled = False

        IniGrid()
        TabControl1.SelectedTab = TP1
        TxtSalesTaxGroupParty.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupParty"))
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "SaleInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

        TxtGodown.Tag = DtV_TypeSettings.Rows(0)("DEFAULT_Godown")
        TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" Select Description From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GCn).ExecuteScalar)


        'TxtSaleToParty.Focus()
    End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            mQry = " SELECT I.Code, I.Description, I.Unit, I.ManualCode, I.MeasureUnit, I.Measure As MeasurePerPcs, " &
                   " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, UI.Code as ItemUIDCode " &
                   " FROM (Select Item, Code From Item_UID Where Item_Uid = '" & Dgl1.Item(Col1Item_UID, mRow).Value & "') UI " &
                   " Left Join Item I  On UI.Item  = I.Code " &
                   " Left Join Unit U  On I.Unit = U.Code " &
                   " Left Join Unit MU  On I.MeasureUnit = MU.Code "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemUIDCode"))
                Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
                Dgl1.Item(Col1Qty, mRow).Value = 1
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = Format(AgL.VNull(DtTemp.Rows(0)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces")) + 2, "0"))
                Dgl1.Item(Col1TotalMeasure, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasurePerPcs"))
                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))
            Else
                MsgBox("Invalid Item UID", MsgBoxStyle.Information)
                Dgl1.Item(Col1Item_UID, mRow).Value = ""
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item_Uid Function ")
        End Try
    End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Description").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ManualCode").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)

                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)

                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)

                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    'Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1

                    Dgl1.Item(Col1BillingType, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("BillingType").Value)

                    Dgl1.Item(Col1ReferenceDocId, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ReferenceDocId").Value)
                    Dgl1.Item(Col1ReferenceDocId, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchaseNo").Value)
                    Dgl1.Item(Col1ReferenceDocIdSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ReferenceDocIdSr").Value)

                    Dgl1.Item(Col1SaleChallan, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SaleInvoice").Value)
                    Dgl1.Item(Col1SaleChallan, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("SaleInvoiceNo").Value)
                    Dgl1.Item(Col1SaleChallanSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("SaleInvoiceSr").Value)

                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)

                    Dgl1.Item(Col1ExpiryDate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ExpiryDate").Value)
                    Dgl1.Item(Col1MRP, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MRP").Value)

                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                    End If
                    If Dgl1.Item(Col1MeasureUnit, mRow).Value = "" Then Dgl1.Item(Col1TotalMeasure, mRow).ReadOnly = True

                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Sale_Rate").Value)
                    Dgl1.Item(Col1PurchaseRate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("PurchaseRate").Value)
                    LblPurchaseRate.Text = Format(Val(Dgl1.Item(Col1PurchaseRate, mRow).Value), "0.00")

                    'Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)

                End If
                Try
                    If Dgl1.Item(Col1DeliveryMeasure, mRow).Value = "" Then Dgl1.Item(Col1DeliveryMeasure, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow - 1).Value
                    If Dgl1.Item(Col1BillingType, mRow).Value = "" Then Dgl1.Item(Col1BillingType, mRow).Value = Dgl1.Item(Col1BillingType, mRow - 1).Value
                    If Dgl1.Item(Col1RateType, mRow).Value = "" Then Dgl1.Item(Col1RateType, mRow).Value = Dgl1.Item(Col1RateType, mRow - 1).Value
                Catch ex As Exception
                End Try
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
                Case Col1Item_UID
                    Validating_Item_Uid(Dgl1.Item(Col1Item_UID, mRowIndex).Value, mRowIndex)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)
                    Call FCheckDuplicate(mRowIndex)
                    FShowTransactionHistory(Dgl1.Item(Col1Item, mRowIndex).Tag)

                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1DeliveryMeasure
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1RateType
                    ClsMain.FGetItemRate(Dgl1.Item(Col1Item, mRowIndex).Tag, Dgl1.Item(Col1RateType, mRowIndex).Tag, TxtV_Date.Text, TxtSaleToParty.Tag, "", Dgl1.Item(Col1Rate, mRowIndex).Value, Dgl1.Item(Col1RatePerQty, mRowIndex).Value, Dgl1.Item(Col1RatePerMeasure, mRowIndex).Value)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        If Topctrl1.Mode = "Browse" Then Exit Sub

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalBale.Text = 0
        LblTotalAmount.Text = 0

        AgCalcGrid1.AgVoucherCategory = "SALES"

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value) + Val(Dgl1.Item(Col1FreeQty, I).Value)

                If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                    'Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(Val(Dgl1.Item(Col1FreeQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalFreeMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) + Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))

                If Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                End If
                Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) + Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))

                If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Or Dgl1.Item(Col1BillingType, I).Value = "" Then
                    Dgl1.Item(Col1RatePerQty, I).Value = Val(Dgl1.Item(Col1Rate, I).Value)

                    If Val(Dgl1.Item(Col1TotalMeasure, I).Value) <> 0 Then
                        Dgl1.Item(Col1RatePerMeasure, I).Value = Math.Round(Val(Dgl1.Item(Col1Amount, I).Value) / Val(Dgl1.Item(Col1TotalMeasure, I).Value), 2)
                    End If
                Else : AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure")
                    Dgl1.Item(Col1RatePerMeasure, I).Value = Val(Dgl1.Item(Col1Rate, I).Value)

                    If Val(Dgl1.Item(Col1Qty, I).Value) <> 0 Then
                        Dgl1.Item(Col1RatePerQty, I).Value = Math.Round(Val(Dgl1.Item(Col1Amount, I).Value) / Val(Dgl1.Item(Col1Qty, I).Value), 2)
                    End If
                End If

                If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Or Dgl1.Item(Col1BillingType, I).Value = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If AgL.StrCmp(Dgl1.Item(Col1TransactionType, I).Value, ChallanType_Return) Or AgL.StrCmp(Dgl1.Item(Col1TransactionType, I).Value, ChallanType_DirectReturn) Then
                    Dgl1.Item(Col1Amount, I).Value = -Val(Dgl1.Item(Col1Amount, I).Value)
                End If

                'Footer Calculation
                Dim bQty As Double = 0
                If AgL.StrCmp(Dgl1.Item(Col1TransactionType, I).Value, ChallanType_Sale) Then
                    bQty = Val(Dgl1.Item(Col1Qty, I).Value)
                Else
                    bQty = -Val(Dgl1.Item(Col1Qty, I).Value)
                End If

                LblTotalQty.Text = Val(LblTotalQty.Text) + bQty
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                LblTotalBale.Text += 1

                FFormatRateCells(I)

            End If
        Next
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgVoucherCategory = "Sales"
        AgCalcGrid1.Calculation()

        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalMeasure.Text = Val(LblTotalMeasure.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        If AgL.RequiredField(TxtSaleToParty, LblBuyer.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If Val(TxtCreditLimit.Text) > 0 Then
            If Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) + Val(TxtCurrBal.Text) > Val(TxtCreditLimit.Text) Then
                MsgBox("Total Balance Of " & TxtSaleToParty.Name & " Is Exceeding Its Credit Limit " & TxtCreditLimit.Text & ".")
                passed = False : Exit Sub
            End If
        End If

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1DocQty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If AgL.StrCmp(Dgl1.Item(Col1TransactionType, I).Value, ChallanType_Sale) Then
                        If Dgl1.Item(Col1SaleChallan, I).Value = "" Or Dgl1.Item(Col1SaleChallan, I).Tag = mSearchCode Then
                            mQry = " Select IfNull(Sum(Qty_Rec), 0) - IfNull(Sum(Qty_Iss), 0) " &
                                      " FROM Stock " &
                                      " WHERE Item = '" & Dgl1.Item(Col1Item, I).Tag & "' " &
                                      " AND  ReferenceDocID = '" & Dgl1.Item(Col1ReferenceDocId, I).Tag & "' " &
                                      " And ReferenceDocIdSr = " & Val(Dgl1.Item(Col1ReferenceDocIdSr, I).Value) & " " &
                                      " And Site_Code = '" & TxtSite_Code.Tag & "'" &
                                      " And DocId <> '" & mSearchCode & "'"
                            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) < Val(Dgl1.Item(Col1Qty, I).Value) Then
                                MsgBox(" Balance Stock Of Item " & Dgl1.Item(Col1Item, I).Value & " In Purchase No " & Dgl1.Item(Col1ReferenceDocId, I).Value & " Is Less Then " & Dgl1.Item(Col1Qty, I).Value & "", MsgBoxStyle.Information)
                                .CurrentCell = .Item(Col1DocQty, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                        End If
                    End If

                    If AgL.StrCmp(Dgl1.Item(Col1TransactionType, I).Value, ChallanType_Return) Then
                        mQry = " Select IfNull(Sum(Qty), 0)  " &
                                " FROM SaleInvoiceDetail L " &
                                " WHERE L.Item = '" & Dgl1.Item(Col1Item, I).Tag & "' " &
                                " AND L.SaleInvoice = '" & Dgl1.Item(Col1SaleChallan, I).Tag & "' " &
                                " AND L.SaleInvoiceSr = " & Val(Dgl1.Item(Col1SaleChallanSr, I).Value) & "" &
                                " And DocId <> '" & mSearchCode & "'"
                        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) < Val(Dgl1.Item(Col1Qty, I).Value) Then
                            MsgBox(" Balance Stock Of Item " & Dgl1.Item(Col1Item, I).Value & " In Sale No " & Dgl1.Item(Col1SaleChallan, I).Value & " Is Less Then " & Dgl1.Item(Col1Qty, I).Value & "", MsgBoxStyle.Information)
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "SaleInvoice",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)

    End Sub

    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtSaleToParty.KeyDown, TxtCurrency.KeyDown, TxtSalesTaxGroupParty.KeyDown, TxtBillToParty.KeyDown, TxtGodown.KeyDown, TxtAgent.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtCurrency.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Code, Code AS Currency, IfNull(IsDeleted,0) AS IsDeleted " &
                                    " FROM Currency " &
                                    " ORDER BY Code "
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtSaleToParty.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            FCreateHelpSubgroup()
                        End If
                    End If

                Case TxtBillToParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            FCreateHelpSubgroup()
                            TxtBillToParty.AgHelpDataSet = TxtSaleToParty.AgHelpDataSet
                        End If
                    End If

                Case TxtAgent.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Account_Name, Sg.Nature, Sg.UpLine " &
                                    " FROM SubGroup Sg " &
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " &
                                    " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                    " And Sg.MasterType = '" & AgTemplate.ClsMain.SubgroupType.Agent & "'"
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtSalesTaxGroupParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Description AS Code, Description FROM PostingGroupSalesTaxParty Where IfNull(Active,0)=1 "
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtGodown.Name
                    If TxtGodown.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT H.Code, H.Description " &
                                " FROM Godown H " &
                                " Where H.Div_Code = '" & TxtDivision.Tag & "' " &
                                " And H.Site_Code = '" & TxtSite_Code.Tag & "' " &
                                " And IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                " Order By H.Description"
                        TxtGodown.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
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
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty, Col1DocQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                    LblHelp.Visible = False

                    If Not AgL.StrCmp(Dgl1.Item(Col1SaleChallan, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode) And Dgl1.Item(Col1SaleChallan, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Item(Col1Qty, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                    End If

                Case Col1MeasurePerPcs, Col1TotalDocMeasure, Col1TotalMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                    LblHelp.Visible = False

                Case Col1DeliveryMeasurePerPcs, Col1TotalDocDeliveryMeasure, Col1TotalFreeDeliveryMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                    LblHelp.Visible = False


                Case Col1Item
                    Try
                        If Dgl1.Item(Col1TransactionType, e.RowIndex).Value = "" Then
                            Dgl1.Item(Col1TransactionType, e.RowIndex).Value = ChallanType_Sale
                        End If
                        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
                        LblHelp.Visible = False

                        If Not AgL.StrCmp(Dgl1.Item(Col1SaleChallan, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode) And Dgl1.Item(Col1SaleChallan, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                            Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                        End If

                    Catch ex As Exception
                    End Try


                Case Col1TransactionType
                    LblHelp.Visible = True

                Case Else
                    LblHelp.Visible = False
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FFillItemsForOrder(ByVal bOrderNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bOrderNoStr = "" Then Exit Sub

            mQry = " SELECT Max(L.Item) As Code, Max(I.Description) as Description, " &
                    " Max(I.ManualCode) As ManualCode,   " &
                    " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS SaleOrderRefNo,   " &
                    " Max(H.V_Date) as SaleOrderDate,  " &
                    " Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " &
                    " Max(I.Unit) as Unit,   " &
                    " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " &
                    " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,   " &
                    " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup, L.SaleOrder,   " &
                    " Max(L.MeasurePerPcs) as MeasurePerPcs, L.SaleOrderSr,   " &
                    " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                    " Max(U1.DecimalPlaces) as MeasureDecimalPlaces,   " &
                    " Max(L.DeliveryMeasure) As DeliveryMeasure, " &
                    " Max(L.BillingType) As BillingType, " &
                    " Max(L.DeliveryMeasureMultiplier) As DeliveryMeasureMultiplier, " &
                    " Max(L.TotalDeliveryMeasure) As TotalDeliveryMeasure " &
                    " FROM (  " &
                    "     SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                    "     FROM SaleOrder    " &
                    "     WHERE SaleToParty ='" & TxtSaleToParty.Tag & "'   " &
                    "     And Div_Code = '" & TxtDivision.Tag & "'   " &
                    "     AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    "     AND V_Date <= '" & TxtV_Date.Text & "'   " &
                    "     ) H   " &
                    " LEFT JOIN SaleOrderDetail L  ON H.DocID = L.DocId    " &
                    " Left Join Item I  On L.Item  = I.Code   " &
                    " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                    " Left Join (   " &
                    "     SELECT L.SaleOrder, L.SaleOrderSr, sum (L.Qty) AS Qty, Sum(L.TotalMeasure) as TotalMeasure      " &
                    " 	  FROM SaleInvoiceDetail L     " &
                    " 	  GROUP BY L.SaleOrder, L.SaleOrderSr   " &
                    " 	) AS CD ON L.SaleOrder = CD.SaleOrder AND L.SaleOrderSr = CD.SaleOrderSr   " &
                    " LEFT JOIN Unit U On L.Unit = U.Code   " &
                    " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                    " WHERE L.Qty - IfNull(Cd.Qty, 0) > 0 " &
                    " GROUP BY L.SaleOrder, L.SaleOrderSr  " &
                    " Order By SaleOrderDate  "

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1SaleOrder, I).Tag = AgL.XNull(.Rows(I)("SaleOrder"))
                        Dgl1.Item(Col1SaleOrder, I).Value = AgL.XNull(.Rows(I)("SaleOrderRefNo"))
                        Dgl1.Item(Col1SaleOrderSr, I).Value = AgL.XNull(.Rows(I)("SaleOrderSr"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Code"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Description"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxPostingGroup"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Bal.Qty"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Measure")), "0.000")
                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                        Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                        Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))

                        'AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("SaleOrder")), Dgl1, I, AgL.VNull(.Rows(I)("Sr")))
                    Next I
                End If
            End With
            AgCalcGrid1.Calculation(True)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempSaleInvoice_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'If BlnIsTotalDeliveryMeasureVisible = False Then LblTotalDeliveryMeasure.Visible = False : LblTotalDeliveryMeasureText.Visible = False
        'If BlnIsMeasureVisible = False Then LblTotalMeasure.Visible = False : LblTotalMeasureText.Visible = False
        'If BlnIsBaleNoVisible = False Then LblTotalBale.Visible = False : LblTotalBaleText.Visible = False

    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        If e.KeyCode = Keys.Insert Then
            FOpenSaleInvoice()
        End If

        If Dgl1.CurrentCell IsNot Nothing Then
            If e.KeyCode = Keys.S Or e.KeyCode = Keys.D Or e.KeyCode = Keys.R Then
                If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1TransactionType Then
                    If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value = "" Then
                        If e.KeyCode = Keys.S Then
                            Dgl1.Item(Col1TransactionType, Dgl1.CurrentCell.RowIndex).Value = ChallanType_Sale
                        ElseIf e.KeyCode = Keys.D Then
                            Dgl1.Item(Col1TransactionType, Dgl1.CurrentCell.RowIndex).Value = ChallanType_DirectReturn
                        ElseIf e.KeyCode = Keys.R Then
                            Dgl1.Item(Col1TransactionType, Dgl1.CurrentCell.RowIndex).Value = ChallanType_Return
                        End If
                        Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag = ""
                        Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value = ""
                        Dgl1.AgHelpDataSet(Col1Item) = Nothing
                    End If
                End If
            End If
        End If
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            Dim bRData As String
            '// Check for relational data in Sale Return
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo From SaleInvoiceDetail  L LEFT JOIN SaleInvoice H ON L.DocId = H.DocID WHERE L.SaleInvoice = '" & TxtDocId.Text & "' ) AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox(" Sale Return " & bRData & " created against Invoice No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        'Passed = Not FGetRelationalData()
        RbtDirect.Checked = True
        BtnFillSaleChallan.Enabled = False
    End Sub

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        'Passed = Not FGetRelationalData()
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True

        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM SaleInvoice WHERE ReferenceNo = '" & TxtReferenceNo.Text & "'   " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtReferenceNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM SaleInvoice WHERE ReferenceNo = '" & TxtReferenceNo.Text & "'  " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0 AND DocID <>'" & mSearchCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtReferenceNo.Focus()
        End If
    End Function

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 654, 990, 0, 0)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    'New Code For Multiple Selection

    Private Sub BtnFillSaleInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            StrTicked = FHPGD_PendingSaleOrder()
            If StrTicked <> "" Then
                FFillItemsForOrder(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingSaleOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""


        mQry = " SELECT 'o' As Tick, L.SaleOrder, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS SaleOrderNo, " &
                " Max(H.V_Date) AS SaleOrderDate  " &
                " FROM (  " &
                "      SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                "      FROM SaleOrder    " &
                "      WHERE SaleToParty = '" & TxtSaleToParty.Tag & "'   " &
                "      And Div_Code = '" & TxtDivision.Tag & "'   " &
                "      AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                "      AND V_Date <= '" & TxtV_Date.Text & "'  " &
                "      ) H   " &
                " LEFT JOIN SaleOrderDetail L  ON H.DocID = L.DocId    " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                "      SELECT L.SaleOrder, L.SaleOrderSr, Sum(L.Qty) AS Qty    " &
                "  	 FROM SaleInvoiceDetail L     " &
                "  	 GROUP BY L.SaleOrder, L.SaleOrderSr   " &
                "  	) AS CD ON L.SaleOrder = Cd.SaleOrder AND L.SaleOrderSr = Cd.SaleOrderSr  " &
                " WHERE L.Qty - IfNull(Cd.Qty, 0) > 0 " &
                " GROUP BY L.SaleOrder  " &
                " Order By SaleOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 600, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingSaleOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub RbtInvoiceDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Dgl1.CurrentCell IsNot Nothing Then
                Select Case sender.Name

                End Select
            End If

            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1BillingType) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1BillingType).Dispose() : Dgl1.AgHelpDataSet(Col1BillingType) = Nothing
        If TxtCurrency.AgHelpDataSet IsNot Nothing Then TxtCurrency.AgHelpDataSet.Dispose() : TxtCurrency.AgHelpDataSet = Nothing
        If TxtSaleToParty.AgHelpDataSet IsNot Nothing Then TxtSaleToParty.AgHelpDataSet.Dispose() : TxtSaleToParty.AgHelpDataSet = Nothing
        If TxtBillToParty.AgHelpDataSet IsNot Nothing Then TxtBillToParty.AgHelpDataSet.Dispose() : TxtBillToParty.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet.Dispose() : TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
        If TxtAgent.AgHelpDataSet IsNot Nothing Then TxtAgent.AgHelpDataSet.Dispose() : TxtAgent.AgHelpDataSet = Nothing
    End Sub

    'Private Sub PrintDocument(ByVal SearchCode As String)
    '    Dim mCrd As New ReportDocument
    '    Dim ReportView As New AgLibrary.RepView
    '    Dim DsRep As New DataSet
    '    Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
    '    Dim bCondstr As String = ""

    '    Try
    '        Me.Cursor = Cursors.Default

    '        RepName = "SaleInvoice_Print" : RepTitle = "Material Issue Slip"

    '        mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, H.SaleToParty, " & _
    '                    " H.SaleToPartyMobile, H.Currency,  " & _
    '                    " H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, H.ReferenceDocId, H.Remarks, H.TotalQty,  " & _
    '                    " H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate,  " & _
    '                    " H.Godown, H.Vendor, H.SaleToPartyTinNo, H.SaleToPartyCstNo,  " & _
    '                    " H.Transporter, H.Vehicle, H.VehicleDescription, H.Driver, H.DriverName, H.DriverContactNo, H.LrNo, H.LrDate,  " & _
    '                    " H.CreditDays, L.DocId, L.Sr, L.SaleOrder, " & _
    '                    " L.SaleOrderSr, L.SaleInvoice, L.SaleInvoiceSr, L.Item, L.Specification,  " & _
    '                    " L.SalesTaxGroupItem, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure,  " & _
    '                    " L.Rate, L.Amount, L.ReferenceDocId, L.LotNo, L.UID, L.BaleNo, " & _
    '                    " Sg.DispName AS SaleToPartyName, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName AS SaleToPartyCity , " & _
    '                    " G.Description AS GodownDesc, I.Description As ItemDesc, " & AgCalcGrid1.FLineTableFieldNameStr("H.", "H_") & ", " & _
    '                    " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " & _
    '                    " FROM (SELECT * FROM SaleInvoice  WHERE DocId = '" & mSearchCode & "') As H  " & _
    '                    " LEFT JOIN (SELECT * FROM SaleInvoiceDetail   WHERE DocId = '" & mSearchCode & "') AS  L  ON H.DocID = L.DocId " & _
    '                    " LEFT JOIN SubGroup Sg   ON H.SaleToParty = Sg.SubCode  " & _
    '                    " LEFT JOIN City C   ON Sg.CityCode = C.CityCode " & _
    '                    " LEFT JOIN Godown G   ON H.Godown = G.Code " & _
    '                    " LEFT JOIN Item I   On L.Item = I.Code " & _
    '                    " Where H.DocId = '" & mSearchCode & "'"

    '        '" & AgCalcGrid1.FLineTableFieldNameStr("H.", "H_") & "
    '        AgL.ADMain = New SqlClient.SqlDataAdapter(mQry, AgL.GcnRead)
    '        AgL.ADMain.Fill(DsRep)
    '        AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
    '        mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
    '        mCrd.SetDataSource(DsRep.Tables(0))
    '        CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
    '        'AgPL.Formula_Set(mCrd, RepTitle)
    '        AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)


    '        Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
    '    Catch Ex As Exception
    '        MsgBox(Ex.Message)
    '    End Try
    'End Sub

    Private Sub BtnFillPartyDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPartyDetail.Click
        FOpenPartyDetail()
    End Sub

    Private Sub FOpenPartyDetail()
        Dim FrmObj As FrmSaleInvoicePartyDetail
        Try
            If BtnFillPartyDetail.Tag Is Nothing Then
                FrmObj = New FrmSaleInvoicePartyDetail
                FrmObj.TxtSaleToPartyName.Text = "CASH"
            Else
                FrmObj = BtnFillPartyDetail.Tag
            End If
            FrmObj.DispText(IIf(Topctrl1.Mode = "Browse", False, True))
            FrmObj.ShowDialog()
            If FrmObj.mOkButtonPressed Then BtnFillPartyDetail.Tag = FrmObj
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGetDeliveryMeasureMultiplier(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            If Dgl1.Item(Col1Unit, mRow).Value <> "" And Dgl1.Item(Col1DeliveryMeasure, mRow).Value <> "" And Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value) <> 0 Then
                mQry = " SELECT Multiplier, Rounding FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1MeasureUnit, mRow).Value & "' AND ToUnit =  '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                With DtTemp
                    If .Rows.Count > 0 Then
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(.Rows(0)("Multiplier"))
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGetBaleStr(ByVal SearchCode As String)
        Dim I As Integer
        Dim mBale As String = ""
        Dim tBale As Integer = 0
        Dim fBale As Integer = 0

        Dim DsTemp As DataSet

        mQry = "Select Distinct Convert(INT,BaleNo) as BaleNo From SaleInvoiceDetail  Where DocId = '" & SearchCode & "' And IsNumeric(BaleNo) = 1 Order By  Convert(INT,BaleNo) "
        DsTemp = AgL.FillData(mQry, AgL.GcnRead)
        With DsTemp.Tables(0)

            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    If fBale = 0 Then
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                        mBale = AgL.XNull(.Rows(I)("BaleNo"))
                    ElseIf fBale + 1 <> AgL.VNull(.Rows(I)("BaleNo")) Then
                        mBale = mBale & "-" & AgL.XNull(.Rows(I - 1)("BaleNo")) & ", " & AgL.XNull(.Rows(I)("BaleNo"))
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                    Else
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                    End If

                    If I = DsTemp.Tables(0).Rows.Count - 1 Then
                        If fBale <> AgL.VNull(.Rows(I)("BaleNo")) Then
                            mBale = mBale & ", " & AgL.XNull(.Rows(I)("BaleNo")) & ""
                        Else
                            mBale = mBale & "-" & AgL.XNull(.Rows(I)("BaleNo")) & ""
                        End If
                    End If
                Next I
            End If
        End With


        mQry = "Select Distinct BaleNo From SaleInvoiceDetail  Where DocId = '" & SearchCode & "' And IsNumeric(BaleNo) = 0 "
        DsTemp = AgL.FillData(mQry, AgL.GcnRead)
        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    If Dgl1.Item(Col1BaleNo, I).Value IsNot Nothing Then
                        If mBale = "" Then
                            mBale += Dgl1.Item(Col1BaleNo, I).Value.ToString
                        Else
                            mBale += "," & Dgl1.Item(Col1BaleNo, I).Value.ToString
                        End If
                    End If
                Next I
            End If
        End With
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim Mdi As MDIMain = New MDIMain
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                'Case Col1SaleInvoice
                '    Call ClsMain.ProcOpenLinkForm(Mdi.MnuQCRequestEntry, Dgl1.Item(Col1SaleQCReq, e.RowIndex).Tag, Me.MdiParent)

                Case Col1ImportStatus
                    MsgBox(Dgl1.Item(Col1ImportStatus, e.RowIndex).ToolTipText, MsgBoxStyle.Information)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode = Keys.Insert Then Call FOpenSaleInvoice()
                    If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                        FCreateHelpItem()
                    End If

                Case Col1Item_Invoiced
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item_Invoiced) Is Nothing Then
                            FCreateHelpInvoicedItem()
                        End If
                    End If

                Case Col1BillingType
                    If Dgl1.AgHelpDataSet(Col1BillingType) Is Nothing Then
                        mQry = " SELECT 'Qty' AS Code, 'Qty' AS Name " &
                                " Union ALL " &
                                " SELECT 'Measure' AS Code, 'Measure' AS Name "
                        Dgl1.AgHelpDataSet(Col1BillingType) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1DeliveryMeasure
                    If Dgl1.AgHelpDataSet(Col1DeliveryMeasure) Is Nothing Then
                        mQry = " SELECT Code, Code AS Description FROM Unit "
                        Dgl1.AgHelpDataSet(Col1DeliveryMeasure) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1RateType
                    If Dgl1.AgHelpDataSet(Col1RateType) Is Nothing Then
                        mQry = " SELECT H.Code, H.Description  FROM RateType H " &
                                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        Dgl1.AgHelpDataSet(Col1RateType) = AgL.FillData(mQry, AgL.GCn)
                    End If
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
                If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Item Then
                    If Not AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).Contains(",") Then
                        mQry = " Select MnuName, MnuText From ItemType Where Code = '" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "' "
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

    'Private Sub BtnImprtFromExcel_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprtFromExcel.Click
    '    ProcImportFromExcel()
    'End Sub

    'Private Sub ProcImportFromExcel()
    '    Dim DtMain, DtLine As DataTable
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim DtItem As DataTable = Nothing
    '    Dim mQry$ = "", ErrorLog$ = "", bFileName$ = ""
    '    Dim I As Integer
    '    Dim ShowErrMsg As Boolean = False
    '    'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
    '    Dim StrErrLog As String = ""
    '    Try
    '        mQry = "Select '' as Srl, 'Item_Name' as [Field Name], 'Text' as [Data Type], 255 as [Length] "
    '        mQry = mQry + "Union All Select  '' as Srl,'Party_Order_No' as [Field Name], 'Text' as [Data Type], '20' as [Length] "
    '        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], '' as [Length] "
    '        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Number' as [Data Type], '' as [Length] "
    '        mQry = mQry + "Union All Select  '' as Srl,'Bale_No' as [Field Name], 'Text' as [Data Type], '20' as [Length] "

    '        DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        Dim ObjFrmImport As New FrmImportFromExcel
    '        ObjFrmImport.LblTitle.Text = "Sale Invoice Import"
    '        ObjFrmImport.Dgl1.DataSource = DtMain

    '        ObjFrmImport.ShowDialog()
    '        bFileName = ObjFrmImport.TxtExcelPath.Text

    '        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

    '        DtLine = ObjFrmImport.P_DsExcelData.Tables(0)

    '        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    '        For I = 0 To DtLine.Rows.Count - 1
    '            Dgl1.Rows.Add()
    '            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

    '            Dgl1.Item(Col1Item, I).Value = AgL.XNull(DtLine.Rows(I)("Item_Name"))
    '            mQry = " Select I.Code As ItemCode From Item I Where I.Description = '" & AgL.XNull(DtLine.Rows(I)("Item_Name")) & "'"
    '            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    '            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

    '            mQry = " Select I.BillingOn  From Item I Where I.Description = '" & AgL.XNull(DtLine.Rows(I)("Item_Name")) & "'"
    '            Dgl1.Item(Col1BillingType, I).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

    '            Dgl1.Item(Col1SaleOrder, I).Value = AgTemplate.ClsMain.Temp_NCat.SaleOrder & "-" & AgL.XNull(DtLine.Rows(I)("Party_Order_No"))
    '            mQry = " SELECT DocID FROM SaleOrder  WHERE PartyOrderNo = '" & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & "' And SaleToParty = '" & TxtSaleToParty.Tag & "'"
    '            Dgl1.Item(Col1SaleOrder, I).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

    '            mQry = " SELECT ReferenceNo FROM SaleOrder  WHERE PartyOrderNo = '" & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & "' And SaleToParty = '" & TxtSaleToParty.Tag & "'"
    '            Dgl1.Item(Col1SaleOrder, I).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

    '            Dgl1.Item(Col1Qty, I).Value = AgL.VNull(DtLine.Rows(I)("Qty"))

    '            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(DtLine.Rows(I)("Bale_No"))


    '            mQry = " Select L.Rate, L.Qty, L.Unit, L.MeasureUnit, L.MeasurePerPcs, L.DeliveryMeasure, L.DeliveryMeasureMultiplier, L.BillingType, L.SaleOrderSr, " & _
    '                    " L.RatePerQty, L.RatePerMeasure " & _
    '                    " From SaleOrderDetail L " & _
    '                    " Where L.DocId = '" & Dgl1.Item(Col1SaleOrder, I).Tag & "' And L.SaleOrder = '" & Dgl1.Item(Col1SaleOrder, I).Tag & "' " & _
    '                    " And L.Item = '" & Dgl1.Item(Col1Item, I).Tag & "' "
    '            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '            With DtItem
    '                If .Rows.Count > 0 Then
    '                    Dgl1.Item(Col1SaleOrderRatePerQty, I).Value = AgL.XNull(DtItem.Rows(0)("RatePerQty"))
    '                    Dgl1.Item(Col1SaleOrderRatePerMeasure, I).Value = AgL.XNull(DtItem.Rows(0)("RatePerMeasure"))
    '                    Dgl1.Item(Col1Rate, I).Value = AgL.XNull(DtItem.Rows(0)("Rate"))
    '                    Dgl1.Item(Col1SaleOrderSr, I).Value = AgL.XNull(DtItem.Rows(0)("SaleOrderSr"))
    '                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
    '                    Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(DtItem.Rows(0)("BillingType"))
    '                    Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(DtItem.Rows(0)("MeasureUnit"))
    '                    Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(DtItem.Rows(0)("DeliveryMeasure"))
    '                    Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(DtItem.Rows(0)("DeliveryMeasureMultiplier"))
    '                    Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(DtItem.Rows(0)("MeasurePerPcs"))
    '                End If
    '            End With

    '            If Val(Dgl1.Item(Col1Rate, I).Value) = 0 Then
    '                ClsMain.FGetItemRate(Dgl1.Item(Col1Item, I).Tag, "", TxtV_Date.Text, TxtSaleToParty.Tag, "", Dgl1.Item(Col1Rate, I).Value, Dgl1.Item(Col1RatePerQty, I).Value, Dgl1.Item(Col1RatePerMeasure, I).Value)
    '            End If

    '            If AgL.VNull(DtLine.Rows(I)("Rate")) > 0 Then
    '                Dgl1.Item(Col1Rate, I).Value = AgL.VNull(DtLine.Rows(I)("Rate"))
    '            End If
    '        Next
    '        Calculation()

    '        For I = 0 To DtLine.Rows.Count - 1
    '            If AgL.XNull(DtLine.Rows(I)("Item_Name")) <> "" Then
    '                mQry = " Select Count(*) From Item Where Description = " & AgL.Chk_Text(AgL.XNull(DtLine.Rows(I)("Item_Name"))) & " "
    '                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
    '                    ErrorLog += "Item """ & AgL.XNull(DtLine.Rows(I)("Item_Name")) & """ Is Not Valid." & vbCrLf
    '                    Dgl1.Item(Col1ImportStatus, I).ToolTipText = "Item """ & AgL.XNull(DtLine.Rows(I)("Item_Name")) & """ Is Not Valid."
    '                    Dgl1.Item(Col1ImportStatus, I).Value = "Error"
    '                    ShowErrMsg = True
    '                End If
    '            End If
    '        Next

    '        For I = 0 To DtLine.Rows.Count - 1
    '            If AgL.XNull(DtLine.Rows(I)("Party_Order_No")) <> "" Then
    '                mQry = " Select Count(*) From SaleOrder Where PartyOrderNo = " & AgL.Chk_Text(AgL.XNull(DtLine.Rows(I)("Party_Order_No"))) & " And SaleToParty = '" & TxtSaleToParty.Tag & "' "
    '                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
    '                    ErrorLog += "Sale Order """ & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & """ Is Not Valid." & vbCrLf
    '                    Dgl1.Item(Col1ImportStatus, I).ToolTipText = "Item """ & AgL.XNull(DtLine.Rows(I)("Item_Name")) & """ Is Not Valid."
    '                    Dgl1.Item(Col1ImportStatus, I).Value = "Error"
    '                    ShowErrMsg = True
    '                End If
    '            End If
    '        Next

    '        For I = 0 To Dgl1.Rows.Count - 1
    '            If Dgl1.Item(Col1SaleOrder, I).Tag <> "" And Dgl1.Item(Col1Item, I).Tag <> "" Then
    '                mQry = " SELECT H.SaleToParty FROM SaleOrder H WHERE H.DocId = '" & Dgl1.Item(Col1SaleOrder, I).Tag & "' "
    '                If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) <> TxtSaleToParty.Tag Then
    '                    ErrorLog += "Sale Order """ & Dgl1.Item(Col1SaleOrder, I).Value & """ Does Not Belong To """ & TxtSaleToParty.Text & """." & vbCrLf
    '                    Dgl1.Item(Col1ImportStatus, I).ToolTipText = "Sale Order """ & Dgl1.Item(Col1SaleOrder, I).Value & """ Does Not Belong To """ & TxtSaleToParty.Text & """." & vbCrLf
    '                    Dgl1.Item(Col1ImportStatus, I).Value = "Error"
    '                    ShowErrMsg = True
    '                End If

    '            End If
    '        Next

    '        For I = 0 To DtLine.Rows.Count - 1
    '            If AgL.XNull(DtLine.Rows(I)("Item_Name")) <> "" And AgL.XNull(DtLine.Rows(I)("Party_Order_No")) <> "" Then
    '                mQry = " SELECT Count(*) " & _
    '                        " FROM SaleOrderDetail L " & _
    '                        " LEFT JOIN SaleOrder H On L.DocId = H.DocId " & _
    '                        " LEFT JOIN Item I ON L.Item = I.Code " & _
    '                        " WHERE H.PartyOrderNo = '" & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & "' " & _
    '                        " And H.SaleToParty = '" & TxtSaleToParty.Tag & "' " & _
    '                        " AND I.Description = '" & AgL.XNull(DtLine.Rows(I)("Item_Name")) & "' "
    '                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
    '                    ErrorLog += "Item """ & AgL.XNull(DtLine.Rows(I)("Item_Name")) & """ Does Not Belong To Sale Order """ & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & """" & vbCrLf
    '                    Dgl1.Item(Col1ImportStatus, I).ToolTipText = "Item """ & AgL.XNull(DtLine.Rows(I)("Item_Name")) & """ Does Not Belong To Sale Order """ & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & """" & vbCrLf
    '                    Dgl1.Item(Col1ImportStatus, I).Value = "Error"
    '                End If
    '            End If
    '        Next

    '        For I = 0 To DtLine.Rows.Count - 1
    '            If AgL.XNull(DtLine.Rows(I)("Item_Name")) <> "" And AgL.XNull(DtLine.Rows(I)("Party_Order_No")) <> "" Then
    '                mQry = " SELECT L.Qty - IfNull(VChallan.ChallanQty,0) - IfNull(VCancel.CancelQty,0) " & _
    '                        " FROM SaleOrderDetail L  " & _
    '                        " LEFT JOIN SaleOrder H ON L.DocId = H.DocID " & _
    '                        " LEFT JOIN Item I ON L.Item = I.Code " & _
    '                        " LEFT JOIN ( " & _
    '                        " 	SELECT Cd.SaleOrder, Cd.Item, Sum(Cd.Qty) AS ChallanQty " & _
    '                        " 	FROM SaleInvoiceDetail Cd " & _
    '                        " 	LEFT JOIN SaleOrder So ON Cd.SaleOrder = So.DocID " & _
    '                        " 	LEFT JOIN Item I ON Cd.Item = I.Code " & _
    '                        " 	WHERE So.PartyOrderNo = '" & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & "' " & _
    '                        "   And So.SaleToParty = '" & TxtSaleToParty.Tag & "'" & _
    '                        "   AND I.Description = '" & AgL.XNull(DtLine.Rows(I)("Item_Name")) & "' " & _
    '                        " 	GROUP BY Cd.SaleOrder, Cd.Item " & _
    '                        " ) AS VChallan ON L.DocId = VChallan.SaleOrder AND L.Item = VChallan.Item " & _
    '                        " LEFT JOIN ( " & _
    '                        " 	SELECT Socd.SaleOrder, Socd.Item, Sum(Socd.Qty) AS CancelQty " & _
    '                        " 	FROM SaleOrderDetail Socd " & _
    '                        " 	LEFT JOIN SaleOrder So ON Socd.SaleOrder = So.DocID " & _
    '                        " 	LEFT JOIN Item I ON Socd.Item = I.Code " & _
    '                        " 	LEFT JOIN Voucher_Type Vt ON SO.V_Type = Vt.V_Type " & _
    '                        " 	WHERE So.PartyOrderNo = '" & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & "' " & _
    '                        "   And So.SaleToParty = '" & TxtSaleToParty.Tag & "'" & _
    '                        "   AND I.Description = '" & AgL.XNull(DtLine.Rows(I)("Item_Name")) & "' " & _
    '                        " 	AND Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.SaleOrderCancel & "' " & _
    '                        " 	GROUP BY Socd.SaleOrder, Socd.Item  " & _
    '                        " ) AS VCancel ON L.DocId = VCancel.SaleOrder AND L.Item = VCancel.Item " & _
    '                        " WHERE H.PartyOrderNo = '" & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & "' " & _
    '                        " And H.SaleToParty = '" & TxtSaleToParty.Tag & "'" & _
    '                        " AND I.Description = '" & AgL.XNull(DtLine.Rows(I)("Item_Name")) & "' "
    '                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar < Val(Dgl1.Item(Col1Qty, I).Value) Then
    '                    ErrorLog += "Qty Of """ & AgL.XNull(DtLine.Rows(I)("Item_Name")) & """ In Sale Order """ & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & """ Is Exceding The Balamce Qty." & vbCrLf
    '                    Dgl1.Item(Col1ImportStatus, I).ToolTipText = "Qty Of """ & AgL.XNull(DtLine.Rows(I)("Item_Name")) & """ In Sale Order """ & AgL.XNull(DtLine.Rows(I)("Party_Order_No")) & """ Is Exceding The Balamce Qty."
    '                    Dgl1.Item(Col1ImportStatus, I).Value = "Error"
    '                    ShowErrMsg = True
    '                End If
    '            End If
    '        Next

    '        Dgl1.Columns(Col1ImportStatus).Visible = True

    '        If ShowErrMsg Then
    '            Clipboard.SetText(ErrorLog, TextDataFormat.Text)
    '            MsgBox(ErrorLog) : Exit Sub
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally
    '        'FW.Dispose()
    '    End Try
    'End Sub

    Private Sub FrmSaleQuotation_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        GBoxImportFromExcel.Enabled = False
    End Sub

    'Private Sub FPostInSaleInvoice(ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)
    '    Dim DtTemp As DataTable = Nothing
    '    Dim DtSaleInvoice As DataTable = Nothing
    '    Dim I As Integer = 0
    '    Dim V_Type$ = "", DocId$ = "", V_Date$ = "", V_Prefix$ = "", ManualRefNo$ = ""
    '    Dim V_No As Integer = 0

    '    mQry = " Select Distinct DocId From SaleInvoiceDetail  Where SaleInvoice = '" & mSearchCode & "'"
    '    DtSaleInvoice = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

    '    If DtSaleInvoice.Rows.Count > 0 Then
    '        For I = 0 To DtSaleInvoice.Rows.Count - 1
    '            mQry = " Delete From SaleInvoiceDetail Where DocId = '" & DtSaleInvoice.Rows(I)("DocId") & "'"
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '            mQry = " Delete From SaleInvoice Where DocId = '" & DtSaleInvoice.Rows(I)("DocId") & "'"
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        Next
    '    End If

    '    mQry = " SELECT Vt.ContraV_Type, Sum(L.Qty) AS TotalQty, Sum(L.TotalMeasure) AS TotalMeasure, " & _
    '            " Sum(L.TotalDeliveryMeasure) AS TotalDeliveryMeasure, Sum(L.Amount) As TotalAmount " & _
    '            " FROM SaleInvoiceDetail L   " & _
    '            " LEFT JOIN PurchChallan H   ON L.ReferenceDocId = H.DocID  " & _
    '            " LEFT JOIN Voucher_Type Vt   ON H.V_Type = Vt.V_Type " & _
    '            " Where L.DocId = '" & mSearchCode & "'" & _
    '            " GROUP BY Vt.ContraV_Type  "
    '    DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

    '    If DtTemp.Rows.Count > 0 Then
    '        For I = 0 To DtTemp.Rows.Count - 1
    '            V_Type = AgL.XNull(DtTemp.Rows(I)("ContraV_Type"))
    '            V_Date = TxtV_Date.Text
    '            DocId = AgL.GetDocId(V_Type, CStr(V_No), CDate(V_Date), AgL.GcnRead, AgL.PubDivCode, AgL.PubSiteCode)
    '            AgL.UpdateVoucherCounter(DocId, CDate(V_Date), AgL.GcnRead, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
    '            V_No = Val(AgL.DeCodeDocID(DocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
    '            V_Prefix = AgL.DeCodeDocID(DocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
    '            ManualRefNo = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "SaleInvoice", V_Type, V_Date, TxtDivision.Tag, TxtSite_Code.Tag, AgTemplate.ClsMain.ManualRefType.Max)

    '            mQry = "INSERT INTO SaleInvoice(DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, " & _
    '                    " ReferenceNo , " & _
    '                    " SaleToParty , " & _
    '                    " BillToParty , " & _
    '                    " Currency , " & _
    '                    " SalesTaxGroupParty , " & _
    '                    " Structure , " & _
    '                    " Remarks , " & _
    '                    " CreditDays , " & _
    '                    " CreditLimit , " & _
    '                    " CustomFields , " & _
    '                    " Godown , " & _
    '                    " TotalQty , " & _
    '                    " TotalAmount , " & _
    '                    " TotalMeasure , " & _
    '                    " TotalDeliveryMeasure , " & _
    '                    " EntryBy, EntryDate,  EntryType, EntryStatus, Status) " & _
    '                    " VALUES (" & AgL.Chk_Text(DocId) & ", '" & TxtDivision.AgSelectedValue & "',  " & _
    '                    " " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & "," & AgL.ConvertDate(V_Date) & ", " & _
    '                    " " & AgL.Chk_Text(V_Type) & ", " & AgL.Chk_Text(V_Prefix) & ",  " & Val(V_No) & ", " & _
    '                    " " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " & _
    '                    " " & AgL.Chk_Text(TxtSaleToParty.Tag) & ", " & _
    '                    " " & AgL.Chk_Text(TxtBillToParty.Tag) & ", " & _
    '                    " " & AgL.Chk_Text(TxtCurrency.Tag) & ", " & _
    '                    " " & AgL.Chk_Text(TxtSalesTaxGroupParty.Text) & ", " & _
    '                    " " & AgL.Chk_Text(TxtStructure.Tag) & ", " & _
    '                    " " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
    '                    " " & Val(TxtCreditDays.Text) & ", " & _
    '                    " " & Val(TxtCreditLimit.Text) & ", " & _
    '                    " " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " & _
    '                    " " & AgL.Chk_Text(TxtGodown.Tag) & ", " & _
    '                    " " & AgL.VNull(DtTemp.Rows(I)("TotalQty")) & ", " & _
    '                    " " & AgL.VNull(DtTemp.Rows(I)("TotalAmount")) & ", " & _
    '                    " " & AgL.VNull(DtTemp.Rows(I)("TotalMeasure")) & ", " & _
    '                    " " & AgL.VNull(DtTemp.Rows(I)("TotalDeliveryMeasure")) & ", " & _
    '                    " " & AgL.Chk_Text(AgL.PubUserName) & ", " & _
    '                    " " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & AgL.Chk_Text(Topctrl1.Mode) & ", " & _
    '                    " " & AgL.Chk_Text(LogStatus.LogOpen) & ", " & AgL.Chk_Text(TxtStatus.Text) & " )"
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '            mQry = " INSERT INTO SaleInvoiceDetail(DocId, Sr, SaleOrder, Item, Specification, " & _
    '                    " SalesTaxGroupItem, DocQty, RejQty, Qty, Unit, MeasurePerPcs, MeasureUnit,  " & _
    '                    " TotalDocMeasure, TotalRejMeasure, TotalMeasure, BaleNo,  " & _
    '                    " Rate, Amount, Remark,  " & _
    '                    " UID, LotNo, SaleOrderSr, RateType, SaleInvoice, SaleInvoiceSr,  " & _
    '                    " Item_UID, FreeQty, RatePerQty, RatePerMeasure, MRP, ExpiryDate,  " & _
    '                    " BillingType, Supplier, DeliveryMeasure, DeliveryMeasureMultiplier, TotalDeliveryMeasure,  " & _
    '                    " TotalFreeMeasure, ReferenceDocId, ReferenceDocIdSr, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & _
    '                    " SELECT '" & DocId & "', Row_Number() Over (Order By L.Sr), L.SaleOrder, L.Item, L.Specification, " & _
    '                    " L.SalesTaxGroupItem, L.DocQty, L.RejQty, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit,  " & _
    '                    " L.TotalDocMeasure, L.TotalRejMeasure, L.TotalMeasure, L.BaleNo,  " & _
    '                    " L.Rate, L.Amount, L.Remark,  " & _
    '                    " L.UID, L.LotNo, L.SaleOrderSr, L.RateType, L.SaleInvoice, L.SaleInvoiceSr,  " & _
    '                    " L.Item_UID, L.FreeQty, L.RatePerQty, L.RatePerMeasure, L.MRP, L.ExpiryDate,  " & _
    '                    " L.BillingType, L.Supplier, L.DeliveryMeasure, L.DeliveryMeasureMultiplier, L.TotalDeliveryMeasure,  " & _
    '                    " L.TotalFreeMeasure, L.ReferenceDocId, L.ReferenceDocIdSr, " & AgCalcGrid1.FLineTableFieldNameStr("L.", "Sl_") & _
    '                    " FROM SaleInvoiceDetail L    " & _
    '                    " LEFT JOIN PurchChallan P   ON L.ReferenceDocId = P.DocID " & _
    '                    " LEFT JOIN Voucher_Type Vt   ON P.V_Type = Vt.V_Type " & _
    '                    " WHERE Vt.ContraV_Type =  '" & AgL.XNull(DtTemp.Rows(I)("ContraV_Type")) & "' " & _
    '                    " And L.DocId = '" & mSearchCode & "'"
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '            Call FSavePartyDetail("SaleInvoice", mSearchCode, Conn, Cmd)

    '            mQry = AgStructure.ClsMain.FUpdateFooterDataFromLineDataStr(TxtStructure.Tag, DocId, "SaleInvoice", "DocId", "SaleInvoiceDetail", "DocId")
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        Next
    '    End If
    'End Sub

    Private Sub FPostInSaleChallan(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        mQry = " Select Count(*) From SaleInvoiceDetail  Where DocId = '" & mSearchCode & "' And (SaleChallan = '" & mSearchCode & "' Or SaleChallan Is Null)"
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) > 0 Then

            mQry = " UPDATE SaleInvoiceDetail Set SaleChallan = Null, SaleChallanSr = Null Where DocId = '" & mSearchCode & "' And SaleChallan = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleChallanDetail Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleChallan Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO SaleChallan(DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, " &
                    " ReferenceNo , " &
                    " SaleToParty , " &
                    " SaleToPartyName, " &
                    " SaleToPartyAdd1, " &
                    " SaleToPartyAdd2, " &
                    " SaleToPartyCity, " &
                    " SaleToPartyMobile, " &
                    " BillToParty , " &
                    " Agent, " &
                    " Currency , " &
                    " SalesTaxGroupParty , " &
                    " Structure , " &
                    " Remarks , " &
                    " CreditDays , " &
                    " CreditLimit , " &
                    " CustomFields , " &
                    " Godown , " &
                    " UpLine, " &
                    " EntryBy, EntryDate,  EntryType, EntryStatus, Status) " &
                    " Select DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, " &
                    " ReferenceNo , " &
                    " SaleToParty , " &
                    " SaleToPartyName, " &
                    " SaleToPartyAdd1, " &
                    " SaleToPartyAdd2, " &
                    " SaleToPartyCity, " &
                    " SaleToPartyMobile, " &
                    " BillToParty , " &
                    " Agent, " &
                    " Currency , " &
                    " SalesTaxGroupParty , " &
                    " Structure , " &
                    " Remarks , " &
                    " CreditDays , " &
                    " CreditLimit , " &
                    " CustomFields , " &
                    " Godown , " &
                    " UpLine, " &
                    " EntryBy, EntryDate,  EntryType, EntryStatus, Status  " &
                    " From SaleInvoice Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " INSERT INTO SaleChallanDetail(DocId, Sr, SaleOrder, Item, Specification, " &
                    " SalesTaxGroupItem, DocQty, Qty, Unit, MeasurePerPcs, MeasureUnit,  " &
                    " TotalDocMeasure, TotalFreeMeasure, TotalMeasure, BaleNo,  " &
                    " Rate, Amount, Remark,  " &
                    " UID, LotNo, SaleOrderSr, RateType, SaleChallan, SaleChallanSr,  " &
                    " Item_UID, FreeQty, RatePerQty, RatePerMeasure, MRP, ExpiryDate,  " &
                    " BillingType, Supplier, DeliveryMeasure, DeliveryMeasureMultiplier, " &
                    " TotalDocDeliveryMeasure, TotalFreeDeliveryMeasure, TotalDeliveryMeasure,  " &
                    " ReferenceDocId, ReferenceDocIdSr) " &
                    " SELECT DocId, Sr, L.SaleOrder, L.Item, L.Specification, " &
                    " L.SalesTaxGroupItem, L.DocQty, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit,  " &
                    " L.TotalDocMeasure, L.TotalFreeMeasure, L.TotalMeasure, L.BaleNo,  " &
                    " L.Rate, L.Amount, L.Remark,  " &
                    " L.UID, L.LotNo, L.SaleOrderSr, L.RateType, L.SaleInvoice, L.SaleInvoiceSr,  " &
                    " L.Item_UID, L.FreeQty, L.RatePerQty, L.RatePerMeasure, L.MRP, L.ExpiryDate,  " &
                    " L.BillingType, L.Supplier, L.DeliveryMeasure, L.DeliveryMeasureMultiplier, " &
                    " TotalDocDeliveryMeasure, TotalFreeDeliveryMeasure, L.TotalDeliveryMeasure,  " &
                    " L.ReferenceDocId, L.ReferenceDocIdSr " &
                    " FROM SaleInvoiceDetail L    " &
                    " WHERE L.DocId =  '" & mSearchCode & "' And L.SaleChallan Is Null "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Insert Into Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                     " SubCode, Currency, SalesTaxGroupParty, BillingType, Item, Item_Uid, LotNo, " &
                     " Godown, EType_IR, Qty_Iss, Qty_Rec, Unit, MeasurePerPcs, Measure_Iss , Measure_Rec, MeasureUnit, " &
                     " ReferenceDocID, ReferenceDocIDSr) " &
                     " SELECT L.DocId, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ReferenceNo, " &
                     " H.Div_Code, H.Site_Code, H.SaleToParty, H.Currency, H.SalesTaxGroupParty, L.BillingType, L.Item," &
                     " L.Item_Uid, L.LotNo, H.Godown, 'I', " &
                     " Case When  IfNull(L.Qty,0) >= 0 Then L.Qty Else 0 End As Qty_Iss, " &
                     " Case When  IfNull(L.Qty,0) < 0 Then L.Qty Else 0 End As Qty_Rec, " &
                     " L.Unit, L.MeasurePerPcs, " &
                     " Case When  IfNull(L.TotalMeasure,0) >= 0 Then L.TotalMeasure Else 0 End As Measure_Iss, " &
                     " Case When  IfNull(L.TotalMeasure,0) < 0 Then L.TotalMeasure Else 0 End As Measure_Rec, " &
                     " L.MeasureUnit,  " &
                     " L.ReferenceDocId, L.ReferenceDocIdSr  " &
                     " FROM SaleChallanDetail L    " &
                     " LEFT JOIN SaleChallan H On L.DocId = H.DocId " &
                     " WHERE L.DocId =  '" & mSearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            'mQry = AgStructure.ClsMain.FUpdateFooterDataFromLineDataStr(TxtStructure.Tag, mSearchCode, "SaleChallan", "DocId", "SaleChallanDetail", "DocId")
            'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " UPDATE SaleInvoiceDetail Set SaleChallan = DocId, SaleChallanSr = Sr Where DocId = '" & mSearchCode & "' And SaleChallan Is Null "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    'Private Sub FrmSpinningPayment_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
    '    Dim mCrd As New ReportDocument
    '    Dim ReportView As New AgLibrary.RepView
    '    Dim DsRep As New DataSet, DsRep1 As DataSet, DsRep2 As New DataSet
    '    Dim RepName As String = "", RepTitle As String = ""
    '    Dim bTableName As String = "", bSecTableName As String = "", bCondstr As String = ""
    '    Dim mOtherFields$ = ""
    '    Dim mBalanceQry$ = ""
    '    Try
    '        Me.Cursor = Cursors.WaitCursor

    '        AgL.PubReportTitle = "Estimate"
    '        RepName = "Med_SaleInvoice_Print" : RepTitle = "Sale Invoice"



    '        mQry = "SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, " & _
    '                "H.Godown, H.Vendor, H.SaleToParty, Sg.DispName As SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyCity, C.CityName As SaleToPartyCityName, " & _
    '                "H.SaleToPartyMobile, H.SaleToPartyTinNo, H.SaleToPartyCstNo, H.SaleToPartyLstNo, H.ShipToParty, H.ShipToPartyName, " & _
    '                "H.ShipToPartyAddress, H.ShipToPartyCity, H.ShipToPartyMobile, H.SaleOrder, " & _
    '                "H.Currency, H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, " & _
    '                "H.Transporter, H.Vehicle, H.VehicleDescription, H.Driver, H.DriverName, " & _
    '                "H.DriverContactNo, H.LrNo, H.LrDate, H.PrivateMark, H.PortOfLoading, " & _
    '                "H.DestinationPort, H.FinalPlaceOfDelivery, H.PreCarriageBy, H.PlaceOfPreCarriage, " & _
    '                "H.ShipmentThrough, H.CreditDays, H.ReferenceDocId, H.Remarks, " & _
    '                "H.TotalQty, H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, " & _
    '                "H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, " & _
    '                "H.MoveToLogDate, H.IsDeleted, H.Status, H.UID, H.PaymentMode, H.TableCode, " & _
    '                "H.PostingAc, H.SaleToPartyAdd1, H.SaleToPartyAdd2, H.CustomFields, " & _
    '                "H.CreditLimit, H.BaleNoStr, H.InvoiceGenType, H.TotalDeliveryMeasure, H.TotalBale," & _
    '                "L.SaleOrder, L.SaleOrderSr, L.SaleInvoice, L.SaleInvoiceSr, L.Item, L.Specification, " & _
    '                "L.SalesTaxGroupItem, L.DocQty, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, " & _
    '                "L.TotalDocMeasure, L.TotalMeasure, L.Rate, L.Amount, L.ReferenceDocId, " & _
    '                "L.LotNo, L.UID, L.BaleNo, L.Remark As LineRemark, L.FreeQty, " & _
    '                "L.BillingType, L.Item_UID, L.ItemInvoiceGroup, L.SaleInvoice, " & _
    '                "L.SaleInvoiceSr, L.DeliveryMeasure, L.DeliveryMeasureMultiplier, " & _
    '                "L.TotalDeliveryMeasure, L.RateType, " & _
    '                "I.Description AS ItemDesc, I.ManualCode As ItemManualCode, L.ExpiryDate, " & _
    '                "G.Description AS GodownDesc, Sg.DispName As PartyName, " & _
    '                "Sg.ManualCode As PartyManualCode, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName, L.MRP, Sg.DrugLicenseNo As PartyDrugLicenseNo, " & _
    '                "Sg1.DispName As AgentName, Sm.TIN, Sm.PAN, Sm.DrugLicenseNo,  " & _
    '                " " & AgCalcGrid1.FFooterTableFieldNameStr("H.", "H_") & ", " & _
    '                " " & AgCalcGrid1.FLineTableFieldNameStr("L.", "L_") & " " & _
    '                " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " & _
    '                "FROM SaleInvoice H   " & _
    '                "LEFT JOIN SaleInvoiceDetail L   ON H.DocID = L.DocId " & _
    '                "LEFT JOIN Godown G   ON H.Godown = G.Code " & _
    '                "LEFT JOIN Item I   ON L.Item = I.Code " & _
    '                "LEFT JOIN SubGroup Sg   ON H.SaleToParty = Sg.SubCode " & _
    '                "LEFT JOIN City C   ON Sg.CityCode = C.CityCode " & _
    '                "LEFT JOIN SubGroup Sg1 On H.Agent = Sg1.SubCode " & _
    '                "LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code " & _
    '                "WHERE H.DocID = '" & mSearchCode & "'"

    '        mBalanceQry = " SELECT V1.SubCode, Sum(V1.CurrBillAmt) AS CurrBillAmt, Sum(V1.OtherTodaysBill) AS OtherTodaysBill, " & _
    '                    " Sum(V1.OldBalance) AS OldBalance, Sum(V1.TodaysBalance) AS TodaysPayment, " & _
    '                    " Sum(V1.OldBalance) + Sum(V1.OtherTodaysBill) + Sum(V1.CurrBillAmt)-Sum(V1.TodaysBalance) As NetBalance " & _
    '                    " FROM ( " & _
    '                    " SELECT L.SubCode, IfNull(Sum(L.AmtDr),0) AS CurrBillAmt, 0 AS OtherTodaysBill,0 AS OldBalance, 0 AS TodaysBalance  " & _
    '                    " FROM Ledger L WHERE L.DocId = '" & mSearchCode & "' AND L.SubCode = '" & TxtBillToParty.Tag & "' GROUP BY L.SubCode " & _
    '                    " UNION ALL  " & _
    '                    " SELECT L.SubCode, 0 AS CurrBillAmt, IfNull(Sum(L.AmtDr),0) AS OtherTodaysBill,0 AS OldBalance, 0 AS TodaysBalance  " & _
    '                    " FROM Ledger L WHERE L.V_Date = '" & TxtV_Date.Text & "' And L.DocId <> '" & mSearchCode & "' AND L.SubCode = '" & TxtBillToParty.Tag & "'  GROUP BY L.SubCode " & _
    '                    " UNION ALL  " & _
    '                    " SELECT L.SubCode, 0 AS CurrBillAmt, 0 AS OtherTodaysBill,IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0)  AS OldBalance, 0 AS TodaysBalance  " & _
    '                    " FROM Ledger L WHERE L.V_Date < '" & TxtV_Date.Text & "' AND L.SubCode = '" & TxtBillToParty.Tag & "' GROUP BY L.SubCode " & _
    '                    " UNION ALL  " & _
    '                    " SELECT L.SubCode, 0 AS CurrBillAmt, 0 AS OtherTodaysBill,0 AS OldBalance, IfNull(Sum(L.AmtCr),0) AS TodaysBalance  " & _
    '                    " FROM Ledger L WHERE  L.V_Date = '" & TxtV_Date.Text & "' AND L.SubCode = '" & TxtBillToParty.Tag & "' GROUP BY L.SubCode " & _
    '                    " ) AS V1  " & _
    '                    " GROUP BY V1.SubCode "

    '        AgL.ADMain = New SqlClient.SqlDataAdapter(mBalanceQry, AgL.GCn)
    '        AgL.ADMain.Fill(DsRep2)

    '        'End If
    '        AgL.ADMain = New SqlClient.SqlDataAdapter(mQry, AgL.GCn)
    '        AgL.ADMain.Fill(DsRep)

    '        AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

    '        mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
    '        mCrd.SetDataSource(DsRep.Tables(0))

    '        AgPL.CreateFieldDefFile1(DsRep2, AgL.PubReportPath & "\" & RepName & "1.ttx", True)
    '        mCrd.OpenSubreport("SUBREP1").Database.Tables(0).SetDataSource(DsRep.Tables(0))
    '        mCrd.OpenSubreport("SUBREP2").Database.Tables(0).SetDataSource(DsRep2.Tables(0))


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

    Private Sub FrmSaleInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F11 Then
            LblPurchaseRate.Visible = Not LblPurchaseRate.Visible
        ElseIf e.KeyCode = Keys.F9 Then
            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                    FPostInPurchIndent(AgL.GCn, AgL.ECmd, Dgl1.CurrentCell.RowIndex)
                End If
            End If
        End If
    End Sub


    Private Sub FShowTransactionHistory(ByVal Item As String)
        Dim DtTemp As DataTable = Nothing
        Dim CSV_Qry As String = ""
        Dim CSV_QryArr() As String = Nothing
        Dim I As Integer, J As Integer
        Dim IGridWidth As Integer = 0
        Try
            mQry = " SELECT H.V_Date AS [Sale_Date],  " &
                        " L.Rate, L.Qty, Round(L.Landed_Value/L.Qty,2) As Landed_Rate  " &
                        " FROM SaleInvoiceDetail L  " &
                        " LEFT JOIN  SaleInvoice H ON L.DocId = H.DocId " &
                        " Where L.Item = '" & Item & "'" &
                        " And H.DocId <> '" & mSearchCode & "' " &
                        " And H.SaleToParty ='" & TxtSaleToParty.Tag & "' " &
                        " And H.V_Date <= '" & TxtV_Date.Text & "' " &
                        " ORDER BY H.V_Date DESC	Limit 5 "

            'If DtV_TypeSettings.Rows.Count <> 0 Then
            '    If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery")) <> "" Then
            '        mQry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery"))
            '        mQry = Replace(mQry.ToString.ToUpper, "`<ITEMCODE>`", "'" & Item & "'")
            '        mQry = Replace(mQry.ToString.ToUpper, "`<PARTYCODE>`", "'" & TxtSaleToParty.Tag & "'")
            '        mQry = Replace(mQry.ToString.ToUpper, "`<SEARCHCODE>`", "'" & mSearchCode & "'")
            '        mQry = Replace(mQry.ToString.ToUpper, "`<VOUCHERDATE>`", "'" & TxtV_Date.Text & "'")
            '        mQry = Replace(mQry.ToString.ToUpper, "`<DIVISIONCODE>`", "'" & TxtDivision.Tag & "'")
            '        mQry = Replace(mQry.ToString.ToUpper, "`<SITECODE>`", "'" & TxtSite_Code.Tag & "'")
            '    End If

            '    If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv")) <> "" Then
            '        CSV_Qry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv"))
            '    End If
            'End If

            'If CSV_Qry <> "" Then CSV_QryArr = Split(CSV_Qry, ",")

            Try
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Catch EX As Exception
                If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery")) <> "" Then
                    MsgBox(EX.Message, , "Error in Transaction History Sql Query (Voucher Type Settings) Execution")
                Else
                    MsgBox(EX.Message)
                End If
            End Try


            If DtTemp.Rows.Count = 0 Then Dgl.DataSource = Nothing : Dgl.Visible = False : Exit Sub

            Dgl.DataSource = DtTemp
            Dgl.Visible = True

            'Dgl.DataSource.DefaultView.RowFilter = " Item = '" & Item & "' "

            Me.Controls.Add(Dgl)
            Dgl.Left = Me.Left + 3
            Dgl.Top = Me.Bottom - Dgl.Height - 100
            Dgl.Height = 130
            Dgl.Width = 450
            Dgl.ColumnHeadersHeight = 40
            Dgl.AllowUserToAddRows = False
            If Dgl.Columns.Count > 0 Then

                If CSV_Qry <> "" Then J = CSV_QryArr.Length

                For I = 0 To Dgl.ColumnCount - 1
                    If CSV_Qry <> "" Then
                        If I < J Then
                            If Val(CSV_QryArr(I)) > 0 Then
                                Dgl.Columns(I).Width = Val(CSV_QryArr(I))
                            Else
                                Dgl.AutoResizeColumn(I)
                                'Dgl.Columns(I).Width = 100
                            End If
                        Else
                            Dgl.AutoResizeColumn(I)
                            'Dgl.Columns(I).Width = 100
                        End If
                    Else
                        Dgl.Columns(I).Width = 100
                    End If
                    Dgl.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                    IGridWidth += Dgl.Columns(I).Width
                Next


                Dgl.Width = IGridWidth + 50


                Dgl.RowHeadersVisible = False
                Dgl.EnableHeadersVisualStyles = False
                Dgl.AllowUserToResizeRows = False
                Dgl.ReadOnly = True
                Dgl.AutoResizeRows()
                Dgl.AutoResizeColumnHeadersHeight()
                Dgl.BackgroundColor = Color.Cornsilk
                Dgl.ColumnHeadersDefaultCellStyle.BackColor = Color.Cornsilk
                Dgl.DefaultCellStyle.BackColor = Color.Cornsilk
                Dgl.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                Dgl.CellBorderStyle = DataGridViewCellBorderStyle.None
                Dgl.Font = New Font(New FontFamily("Verdana"), 8)
                Dgl.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                Dgl.BringToFront()
                Dgl.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FShowLastRates(ByVal Item As String)
        Dim DtTemp As DataTable = Nothing
        Try
            mQry = " SELECT TOP 5 H.V_Date AS [Sale_Date], L.Item, " &
                        " L.Rate, L.Qty, Round(L.Landed_Value/L.Qty,2) As Landed_Rate  " &
                        " FROM SaleInvoiceDetail L  " &
                        " LEFT JOIN  SaleInvoice H ON L.DocId = H.DocId " &
                        " Where L.Item = '" & Item & "'" &
                        " And H.DocId <> '" & mSearchCode & "' " &
                        " And H.SaleToParty ='" & TxtSaleToParty.Tag & "' " &
                        " And H.V_Date <= '" & TxtV_Date.Text & "' " &
                        " ORDER BY H.V_Date DESC	 "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count = 0 Then Dgl.DataSource = Nothing : Dgl.Visible = False : Exit Sub

            Dgl.DataSource = DtTemp
            Dgl.DataSource.DefaultView.RowFilter = " Item = '" & Item & "' "

            Me.Controls.Add(Dgl)
            Dgl.Left = Me.Left + 3
            Dgl.Top = Me.Bottom - Dgl.Height - 100
            Dgl.Height = 130
            Dgl.Width = 350
            Dgl.ColumnHeadersHeight = 40
            Dgl.AllowUserToAddRows = False
            If Dgl.Columns.Count > 0 Then
                Dgl.Columns("Sale_Date").Width = 82
                Dgl.Columns("Rate").Width = 60
                Dgl.Columns("Qty").Width = 60
                Dgl.Columns("Sale_Date").SortMode = DataGridViewColumnSortMode.NotSortable
                Dgl.Columns("Rate").SortMode = DataGridViewColumnSortMode.NotSortable
                Dgl.Columns("Qty").SortMode = DataGridViewColumnSortMode.NotSortable
                'Dgl.Columns("Rate").CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                'Dgl.Columns("Qty").CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                'Dgl.Columns("Rate").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                'Dgl.Columns("Qty").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Dgl.RowHeadersVisible = False
                Dgl.EnableHeadersVisualStyles = False
                Dgl.AllowUserToResizeRows = False
                Dgl.ReadOnly = True
                Dgl.Columns("Item").Visible = False
                Dgl.AutoResizeRows()
                Dgl.AutoResizeColumnHeadersHeight()
                Dgl.BackgroundColor = Color.Cornsilk
                Dgl.ColumnHeadersDefaultCellStyle.BackColor = Color.Cornsilk
                Dgl.DefaultCellStyle.BackColor = Color.Cornsilk
                Dgl.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                Dgl.CellBorderStyle = DataGridViewCellBorderStyle.None
                Dgl.Font = New Font(New FontFamily("Verdana"), 8)
                Dgl.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                Dgl.BringToFront()
                Dgl.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        FShowTransactionHistory(Dgl1.Item(Col1Item, e.RowIndex).Tag)
        LblPurchaseRate.Text = Format(Val(Dgl1.Item(Col1PurchaseRate, e.RowIndex).Value), "0.00")
    End Sub

    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        Dgl.Visible = False
    End Sub

    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Value <> "" Then
                        If mRow <> I Then
                            If AgL.StrCmp(.Item(Col1Item, I).Value, .Item(Col1Item, mRow).Value) Then
                                If MsgBox("Item " & .Item(Col1Item, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
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

    Private Sub FFormatRateCells(ByVal mRow As Integer)
        Dim I As Integer = 0
        Try
            If Val(Dgl1.Item(Col1Rate, mRow).Value) < Val(Dgl1.Item(Col1PurchaseRate, mRow).Value) Then
                Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
                Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Red

            Else
                Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Black
                Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Regular)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function AccountPosting(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) As Boolean
        Dim J As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim mNarr As String = "", mCommonNarr$ = ""
        Dim mNetAmount As Double, mRoundOff As Double = 0
        Dim mSr As Integer = 0

        mNetAmount = 0
        mCommonNarr = ""
        mCommonNarr = ""
        If mCommonNarr.Length > 255 Then mCommonNarr = AgL.MidStr(mCommonNarr, 0, 255)

        mSr = AgL.XNull(AgL.Dman_Execute(" Select Max(V_SNo) From Ledger  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

        mSr += 1
        mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                 " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode) " &
                 " Values ('" & mSearchCode & "','" & TxtReferenceNo.Text & "'," & mSr & ", " &
                 " " & AgL.Chk_Text(TxtV_Date.Text) & "," & AgL.Chk_Text(AgL.XNull(AgL.PubDtEnviro.Rows(0)("CashAc"))) & ", " &
                 " " & AgL.Chk_Text(TxtBillToParty.Tag) & ", " &
                 " " & Val(TxtPaidAmt.Text) & ", 0, " &
                 " " & AgL.Chk_Text(mNarr) & ",'" & TxtV_Type.AgSelectedValue & "'," & Val(TxtV_No.Text) & ", " &
                 " '" & LblPrefix.Text & "','" & TxtSite_Code.Tag & "','" & TxtDivision.Tag & "')"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr += 1
        mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                 " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode) " &
                 " Values ('" & mSearchCode & "','" & TxtReferenceNo.Text & "'," & mSr & ", " &
                 " " & AgL.Chk_Text(TxtV_Date.Text) & "," & AgL.Chk_Text(TxtBillToParty.Tag) & ", " &
                 " " & AgL.Chk_Text(AgL.XNull(AgL.PubDtEnviro.Rows(0)("CashAc"))) & ", " &
                 " 0, " & Val(TxtPaidAmt.Text) & ", " &
                 " " & AgL.Chk_Text(mNarr) & ",'" & TxtV_Type.AgSelectedValue & "'," & Val(TxtV_No.Text) & ", " &
                 " '" & LblPrefix.Text & "','" & TxtSite_Code.Tag & "','" & TxtDivision.Tag & "')"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Function

    Private Sub FPostInPurchIndent(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand, ByVal mRow As Integer)
        Dim mSr As Integer = 0

        mQry = " Select Count(*) From PurchIndent H LEFT JOIN PurchIndentDetail L ON H.DocId = L.DocId Where H.V_Date = '" & TxtV_Date.Text & "' And L.Item = '" & Dgl1.Item(Col1Item, mRow).Tag & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = 0 Then
            mQry = " Select Count(*) From PurchIndent  Where DocId = '" & mSearchCode & "'  "
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = 0 Then
                mQry = " INSERT INTO PurchIndent " &
                            " ( " &
                            " DocID, " &
                            " V_Type, " &
                            " V_Prefix, " &
                            " V_Date, " &
                            " V_No, " &
                            " Div_Code, " &
                            " Site_Code, " &
                            " Remarks, " &
                            " EntryBy, " &
                            " EntryDate) " &
                            " Values ( " &
                            " '" & mSearchCode & "', " &
                            " '" & TxtV_Type.Tag & "', " &
                            " " & AgL.Chk_Text(LblPrefix.Text) & ", " &
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ", " &
                            " " & Val(TxtV_No.Text) & ", " &
                            " " & AgL.Chk_Text(TxtDivision.Tag) & ", " &
                            " " & AgL.Chk_Text(TxtSite_Code.Tag) & ", " &
                            " " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                            " '" & AgL.PubUserName & "', " &
                            " '" & AgL.PubLoginDate & "') "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If




            mQry = " Select Max(Sr) From PurchIndentDetail  Where DocId = '" & mSearchCode & "'  "
            mSr = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

            mSr += 1
            mQry = " INSERT INTO PurchIndentDetail(DocId, Sr, Item, IndentQty, Unit) " &
                    " Values('" & mSearchCode & "', " & mSr & ", " & AgL.Chk_Text(Dgl1.Item(Col1Item, mRow).Tag) & ", 1, " &
                    " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, mRow).Value) & ")"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
        Dgl1.Item(ColSNo, mRow).Style.ForeColor = Color.Red
        Dgl1.Item(ColSNo, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            Dgl1.CurrentCell = Dgl1.Item(Col1Item, Dgl1.Rows.Count - 1) : Dgl1.Focus()
        End If
    End Sub

    Private Sub FOpenSaleInvoice()
        Dim FrmObj As Object = Nothing
        Dim CFOpen As New ClsFunction
        Dim MDI As New MDIMain
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim mReferenceDocId$ = ""
        Dim mReferenceDocIdSr As Integer = 0
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    'FrmObj = New FrmPurchChallanForSingleItem()
                    If FrmObj IsNot Nothing Then
                        FrmObj.StartPosition = FormStartPosition.CenterScreen

                        FrmObj.Owner = Me

                        FrmObj.ShowDialog()
                        mReferenceDocId = FrmObj.mReferenceDocId
                        mReferenceDocIdSr = FrmObj.mReferenceDocIdSr
                        FrmObj = Nothing

                        Dgl1.AgHelpDataSet(Col1Item) = Nothing

                        Dgl1.Item(Col1Item, bRowIndex).Value = ""
                        Dgl1.Item(Col1Item, bRowIndex).Tag = ""

                        Dgl1.CurrentCell = Dgl1.Item(Col1Rate, bRowIndex)

                        mQry = " SELECT Max(I.Code) AS Code, Max(I.Description) AS Description, " &
                                 " IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) AS [Bal.Qty], Max(I.Unit) As Unit, " &
                                 " Max(P.V_Type || '-' || P.Referenceno) As PurchaseNo, Max(P.V_Date) AS Purchase_Date,  " &
                                 " Max(Pid.Sale_Rate) As Sale_Rate, Max(Pid.MRP) AS MRP, Max(L.ExpiryDate) AS ExpiryDate, Max(L.LotNo) AS LotNo,  " &
                                 " Max(I.ManualCode) AS ManualCode, " &
                                 " Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup, Max(L.MeasureUnit) As MeasureUnit, " &
                                 " Max(L.MeasurePerPcs) As MeasurePerPcs,  Max(Sg.Name) AS Vendor, " &
                                 " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                                 " Max(I.BillingOn) as BillingType, " &
                                 " L.ReferenceDocID, L.ReferenceDocIDSr,  " &
                                 " Max((PID.Landed_Value/PID.Qty) + ((PID.Landed_Value/PID.Qty) * 1/100)) as PurchaseRate " &
                                 " FROM Stock L  " &
                                 " LEFT JOIN PurchChallanDetail Pid ON L.ReferenceDocId = Pid.DocId And L.ReferenceDocIdSr = Pid.Sr " &
                                 " LEFT JOIN PurchChallan P ON Pid.docid = P.DocId " &
                                 " LEFT JOIN Item I ON L.Item = I.Code " &
                                 " LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode  " &
                                 " LEFT JOIN Unit U On I.Unit = U.Code " &
                                 " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                                 " Where L.ReferenceDocId = '" & mReferenceDocId & "' " &
                                 " And L.ReferenceDocIdSr = " & mReferenceDocIdSr & " " &
                                 " And L.DocId <> '" & mSearchCode & "'" &
                                 " GROUP BY L.ReferenceDocID, L.ReferenceDocIDSr " &
                                 " Having IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) > 0 "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        If DtTemp.Rows.Count > 0 Then
                            Dgl1.Item(Col1Item, bRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                            Dgl1.Item(Col1Item, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
                            Dgl1.Item(Col1ItemCode, bRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                            Dgl1.Item(Col1ItemCode, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
                            Dgl1.Item(Col1Unit, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, bRowIndex).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1Qty, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Bal.Qty"))

                            Dgl1.Item(Col1MeasurePerPcs, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("MeasurePerPcs"))

                            Dgl1.Item(Col1MeasureUnit, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, bRowIndex).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))

                            Dgl1.Item(Col1DeliveryMeasure, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, bRowIndex).Value = 1

                            Dgl1.Item(Col1BillingType, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("BillingType"))

                            Dgl1.Item(Col1ReferenceDocId, bRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("ReferenceDocId"))
                            Dgl1.Item(Col1ReferenceDocIdSr, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("ReferenceDocIdSr"))

                            Dgl1.Item(Col1ReferenceDocId, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("PurchaseNo"))

                            Dgl1.Item(Col1LotNo, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("LotNo"))

                            Dgl1.Item(Col1ExpiryDate, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("ExpiryDate"))
                            Dgl1.Item(Col1MRP, bRowIndex).Value = AgL.VNull(DtTemp.Rows(0)("MRP"))

                            Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("SalesTaxPostingGroup"))
                            Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("SalesTaxPostingGroup"))
                            If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, bRowIndex), "") Then
                                Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                                Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
                            End If
                            If Dgl1.Item(Col1MeasureUnit, bRowIndex).Value = "" Then Dgl1.Item(Col1TotalMeasure, bRowIndex).ReadOnly = True

                            Dgl1.Item(Col1Rate, bRowIndex).Value = AgL.VNull(DtTemp.Rows(0)("Sale_Rate"))
                            Dgl1.Item(Col1PurchaseRate, bRowIndex).Value = AgL.VNull(DtTemp.Rows(0)("PurchaseRate"))
                            LblPurchaseRate.Text = Format(Val(Dgl1.Item(Col1PurchaseRate, bRowIndex).Value), "0.00")
                        End If
                    End If

                    Call Calculation()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillSaleChallan.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            StrTicked = FHPGD_PendingSaleChallan()
            If StrTicked <> "" Then
                ProcFillItems(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingSaleChallan() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, VMain.SaleChallan As SaleChallan, " &
                " Max(VMain.SaleChallanNo) AS SaleChallanNo, " &
                " Max(VMain.SaleChallanDate) as SaleChallanDate, " &
                " IfNull(Sum(VMain.Qty), 0) As [Qty]    " &
                " FROM ( " & FRetFillItemWiseQry("WHERE V_Date <= '" & TxtV_Date.Text & "' And SaleToParty = '" & TxtSaleToParty.Tag & "'", "") & " ) As VMain " &
                " GROUP BY VMain.SaleChallan " &
                " Order By SaleChallanDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 730, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Challan No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Challan Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Bal Qty", 100, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingSaleChallan = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub ProcFillItems(ByVal bChallanNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bChallanNoStr = "" Then Exit Sub

            mQry = FRetFillItemWiseQry(" WHERE DocId In (" & bChallanNoStr & ") ", "")
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1TransactionType, I).Value = AgL.XNull(.Rows(I)("TransactionType"))

                        Dgl1.Item(Col1SaleOrder, I).Tag = AgL.XNull(.Rows(I)("SaleOrder"))
                        Dgl1.Item(Col1SaleOrder, I).Value = AgL.XNull(.Rows(I)("SaleOrderNo"))
                        Dgl1.Item(Col1SaleOrderSr, I).Value = AgL.VNull(.Rows(I)("SaleOrderSr"))
                        Dgl1.Item(Col1SaleOrderRatePerQty, I).Value = AgL.VNull(.Rows(I)("SaleOrderRatePerQty"))
                        Dgl1.Item(Col1SaleOrderRatePerMeasure, I).Value = AgL.VNull(.Rows(I)("SaleOrderRatePerMeasure"))

                        Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UID"))

                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemManualCode"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Item_Name"))


                        Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))

                        Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))

                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                        Dgl1.Item(Col1FreeQty, I).Value = Format(AgL.VNull(.Rows(I)("FreeQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                        Dgl1.Item(Col1RatePerQty, I).Value = AgL.VNull(.Rows(I)("RatePerQty"))
                        Dgl1.Item(Col1RatePerMeasure, I).Value = AgL.VNull(.Rows(I)("RatePerMeasure"))

                        Dgl1.Item(Col1MRP, I).Value = Format(AgL.VNull(.Rows(I)("MRP")), "0.00")
                        Dgl1.Item(Col1ExpiryDate, I).Value = AgL.XNull(.Rows(I)("ExpiryDate"))

                        Dgl1.Item(Col1PurchaseRate, I).Value = Format(AgL.VNull(.Rows(I)("PurchaseRate")), "0.00")

                        Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                        Dgl1.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("RateType"))
                        Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                        Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))

                        Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(.Rows(I)("ReferenceDocId"))
                        Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(.Rows(I)("PurchaseNo"))
                        Dgl1.Item(Col1ReferenceDocIdSr, I).Value = AgL.VNull(.Rows(I)("ReferenceDocIdSr"))

                        Dgl1.Item(Col1SaleChallan, I).Tag = AgL.XNull(.Rows(I)("SaleChallan"))
                        Dgl1.Item(Col1SaleChallan, I).Value = AgL.XNull(.Rows(I)("SaleChallanNo"))
                        Dgl1.Item(Col1SaleChallanSr, I).Value = AgL.VNull(.Rows(I)("SaleChallanSr"))

                        FFormatRateCells(I)

                        AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("SaleChallan")), Dgl1, I, AgL.VNull(.Rows(I)("SaleChallan")))

                        Dgl1.Item(Col1Item, I).ReadOnly = True
                        Dgl1.Item(Col1Qty, I).ReadOnly = True
                    Next I
                End If
            End With
            AgCalcGrid1.Calculation(True)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FRetFillItemWiseQry(ByVal HeaderConStr As String, ByVal LineConStr As String) As String
        FRetFillItemWiseQry = "SELECT Max(L.TransactionType) As TransactionType, Max(L.SaleOrder) As SaleOrder, Max(So.ReferenceNo) As SaleOrderNo, " &
                        " Max(L.SaleOrderSr) As SaleOrderSr, " &
                        " Max(OD.RatePerQty) as SaleOrderRatePerQty, Max(OD.RatePerMeasure) As SaleOrderRatePerMeasure, " &
                        " Max(L.Item_UID) As Item_UID, " &
                        " Max(L.Item) As Item, Max(I.ManualCode) as ItemManualCode,  Max(I.Description) as Item_Name, " &
                        " Max(L.Specification) as Specification," &
                        " Max(L.SalesTaxGroupItem) SalesTaxGroupItem, " &
                        " Max(L.BillingType) As BillingType, Max(L.RateType) As RateType, " &
                        " Max(L.DeliveryMeasure) As DeliveryMeasure, Max(L.BaleNo) As BaleNo, " &
                        " Max(L.LotNo) As LotNo, " &
                        " Max(L.FreeQty) As FreeQty, " &
                        " Sum(L.Qty) - IfNull(Max(Cd.Qty), 0) as Qty,   " &
                        " Max(L.Unit) as Unit, Max(U.DecimalPlaces) As QtyDecimalPlaces, " &
                        " Max(L.MeasurePerPcs) As MeasurePerPcs, Max(L.MeasureUnit) As MeasureUnit,   " &
                        " Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                        " Max(L.DeliveryMeasureMultiplier) As DeliveryMeasureMultiplier, " &
                        " Max(L.Rate) as Rate, Max(L.RatePerQty) As RatePerQty, Max(L.RatePerMeasure) As RatePerMeasure,  " &
                        " Max(L.MRP) As MRP, Max(L.ExpiryDate) As ExpiryDate, " &
                        " Max(L.ReferenceDocId) As ReferenceDocId, Max(L.ReferenceDocIdSr) As ReferenceDocIdSr, " &
                        " L.SaleChallan, L.SaleChallanSr, Max(Pcl.Landed_Value/Pcl.Qty) as PurchaseRate, " &
                        " Max(Pc.V_Type || '-' || Pc.ReferenceNo) As PurchaseNo, " &
                        " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS SaleChallanNo, " &
                        " Max(H.V_Date) As SaleChallanDate   " &
                        " FROM (  " &
                        "    SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                        "    FROM SaleChallan  " & HeaderConStr & " " &
                        " ) AS  H   " &
                        " LEFT JOIN SaleChallanDetail L  ON H.DocID = L.SaleChallan    " &
                        " Left Join Item I  On L.Item  = I.Code   " &
                        " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                        " Left Join (   " &
                        "    SELECT L.SaleChallan, L.SaleChallanSr, Sum (L.Qty) AS Qty  " &
                        "    FROM SaleInvoiceDetail L     " &
                        "    Where L.DocId <> '" & mSearchCode & "'   " &
                        "    GROUP BY L.SaleChallan, L.SaleChallanSr " &
                        " ) AS CD ON L.DocId = CD.SaleChallan AND L.Sr = CD.SaleChallanSr " &
                        " LEFT JOIN Unit U On L.Unit = U.Code   " &
                        " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                        " LEFT JOIN SaleOrder So On L.SaleOrder = So.DocId " &
                        " LEFT JOIN SaleOrderDetail OD  On L.SaleOrder = OD.DocId And L.SaleOrderSr = OD.Sr " &
                        " LEFT JOIN PurchChallan Pc On L.ReferenceDocId = Pc.DocId " &
                        " LEFT JOIN PurchChallanDetail pcl on L.ReferenceDocId = pcl.Docid And l.ReferenceDocIdSr = Pcl.Sr  " &
                        " GROUP BY L.SaleChallan, L.SaleChallanSr " &
                        " Having Sum(L.Qty) - IfNull(Max(Cd.Qty), 0) > 0  "

    End Function

    Private Sub RbtDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtDirect.Click, RbtForSaleChallan.Click
        Try
            Select Case sender.Name
                Case RbtDirect.Name
                    BtnFillSaleChallan.Enabled = False

                Case RbtForSaleChallan.Name
                    BtnFillSaleChallan.Enabled = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpSubgroup()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And instr('|' || Sg.GroupCode || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) <> "" Then
                strCond += " And instr('|' || Sg.GroupCode || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupDivision")) <> "" Then
                strCond += " And instr('|' || Sg.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupSite")) <> "" Then
                strCond += " And instr('|' || Sg.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupSite")) & "') > 0 "
            End If
        End If

        strCond += " And Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Cash & "','" & ClsMain.SubGroupNature.Bank & "')"

        mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party, Sg.SalesTaxPostingGroup, " &
                " Sg.SalesTaxPostingGroup, Sg.Currency, " &
                " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature, Cu.Description As CurrencyDesc " &
                " FROM SubGroup Sg " &
                " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " &
                " LEFT JOIN Currency Cu On Sg.Currency = Cu.Code " &
                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        TxtSaleToParty.AgHelpDataSet(8, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpItem()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")) <> "" Then
                strCond += " And CharIndex('|' || I.V_Type || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                strCond += " And CharIndex('|' || I.Item || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' || I.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' || I.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If AgL.StrCmp(Dgl1.Item(Col1TransactionType, Dgl1.CurrentCell.RowIndex).Value, ChallanType_Return) Then
            mQry = " SELECT Max(L.Item) As Code, " &
                    " Max(I.Description) As Description,  " &
                    " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS SaleInvoiceNo,     " &
                    " Max(H.V_Date) as SaleInvoiceDate,    " &
                    " Sum(L.Qty) AS [Bal.Qty],     " &
                    " Max(I.Unit) as Unit, Max(L.Rate) as Rate,     " &
                    " Max(P.V_Type || '-' || P.Referenceno) As PurchaseNo, " &
                    " Max(Pid.ExpiryDate) As ExpiryDate, Max(Pid.MRP) As MRP, " &
                    " Sum(L.TotalMeasure)  AS [Bal.Measure],     " &
                    " Max(I.MeasureUnit) MeasureUnit,     " &
                    " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup, " &
                    " Max(L.MeasurePerPcs) as MeasurePerPcs, L.SaleInvoiceSr, L.SaleInvoice,     " &
                    " Max(U.DecimalPlaces) as QtyDecimalPlaces,    " &
                    " Max(L.BillingType) As BillingType,    " &
                    " Max(U1.DecimalPlaces) as MeasureDecimalPlaces,   " &
                    " Max(L.RatePerQty) as RatePerQty,   " &
                    " Max(L.RatePerMeasure) as RatePerMeasure, " &
                    " Max(L.ReferenceDocId) As ReferenceDocId, " &
                    " Max(L.ReferenceDocIdSr) As ReferenceDocIdSr, " &
                    " Max(Pid.LotNo) As LotNo, " &
                    " Max(I.ManualCode) as ManualCode,  " &
                    " Max(Pid.Sale_Rate) As Sale_Rate, " &
                    " Max((PID.Landed_Value/PID.Qty) + ((PID.Landed_Value/PID.Qty) * 1/100)) as PurchaseRate " &
                    " FROM (    " &
                    "     SELECT DocID, V_Type, ReferenceNo, V_Date " &
                    "     FROM SaleInvoice      " &
                    "     WHERE SaleToParty = '" & TxtSaleToParty.Tag & "'     " &
                    "     And Div_Code = '" & TxtDivision.Tag & "'     " &
                    "     AND Site_Code = '" & TxtSite_Code.Tag & "'     " &
                    "     AND V_Date <= '" & TxtV_Date.Text & "' " &
                    "     AND DocId <> '" & mSearchCode & "'  " &
                    "     ) H     " &
                    " LEFT JOIN SaleInvoiceDetail L  ON H.DocID = L.DocId  " &
                    " LEFT JOIN PurchChallan P  On L.ReferenceDocId = P.DocId " &
                    " LEFT JOIN PurchChallanDetail Pid On L.ReferenceDocId = Pid.DocId And L.ReferenceDocIdSr = Pid.Sr " &
                    " Left Join Item I  On L.Item  = I.Code     " &
                    " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type      " &
                    " LEFT JOIN Unit U On L.Unit = U.Code     " &
                    " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                    " Where 1=1 " & strCond &
                    " GROUP BY L.SaleInvoice, L.SaleInvoiceSr    " &
                    " HAVING  Sum(L.Qty) > 0 " &
                    " Order By SaleInvoiceDate  "
            Dgl1.AgHelpDataSet(Col1Item, 17) = AgL.FillData(mQry, AgL.GCn)
        ElseIf AgL.StrCmp(Dgl1.Item(Col1TransactionType, Dgl1.CurrentCell.RowIndex).Value, ChallanType_DirectReturn) Then
            mQry = "SELECT I.Code, I.Description, I.ManualCode, '' As SaleInvoiceNo, " &
                  " 0 As [Bal.Qty], I.Unit, 0 As [Bal.Measure], " &
                  " I.MeasureUnit, I.Rate As Rate, I.SalesTaxPostingGroup ,0 As MeasurePerPcs, " &
                  " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, I.BillingOn as BillingType, " &
                  " '' As SaleInvoice, '' As SaleInvoiceNo, 0 As SaleInvoiceSr, " &
                  " '' As ReferenceDocId, " &
                  " 0 As ReferenceDocIdSr, " &
                  " '' As PurchaseNo, " &
                  " '' As ExpiryDate, 0 As MRP, " &
                  " '' As LotNo, " &
                  " 0 As Sale_Rate, " &
                  " 0 As PurchaseRate " &
                  " FROM Item I " &
                  " LEFT JOIN Unit U On I.Unit = U.Code " &
                  " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                  " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            Dgl1.AgHelpDataSet(Col1Item, 23) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " SELECT Max(I.Code) AS Code, Max(I.Description) AS Description, " &
                     " IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) AS [Bal.Qty], Max(I.Unit) As Unit, " &
                     " Max(H.V_Type || '-' || H.RecId) As PurchaseNo, Max(H.V_Date) AS Purchase_Date,  " &
                     " Max(H.Sale_Rate) As Sale_Rate, Max(H.MRP) AS MRP, Max(H.ExpiryDate) AS ExpiryDate, Max(H.LotNo) AS LotNo,  " &
                     " Max(I.ManualCode) AS ManualCode, " &
                     " Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup, Max(H.MeasureUnit) As MeasureUnit, " &
                     " Max(H.MeasurePerPcs) As MeasurePerPcs,  Max(Sg.Name) AS Vendor, " &
                     " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                     " Max(I.BillingOn) as BillingType, '' As SaleInvoice, '' As SaleInvoiceNo, 0 As SaleInvoiceSr, " &
                     " L.ReferenceDocID, L.ReferenceDocIDSr,  " &
                     " Max((H.Landed_Value/H.Qty_Rec) + ((H.Landed_Value/H.Qty_Rec) * 1/100)) as PurchaseRate " &
                     " FROM Stock L  " &
                     " LEFT JOIN Stock H On L.ReferenceDocId = H.DocId And L.ReferenceDocIdSr = H.Sr " &
                     " LEFT JOIN Item I ON L.Item = I.Code " &
                     " LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode  " &
                     " LEFT JOIN Unit U On I.Unit = U.Code " &
                     " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                     " Where L.DocId <> '" & mSearchCode & "' And L.Site_Code ='" & TxtSite_Code.Tag & "' " & strCond &
                     " GROUP BY L.ReferenceDocID, L.ReferenceDocIDSr " &
                     " Having IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) > 0 "
            Dgl1.AgHelpDataSet(Col1Item, 14) = AgL.FillData(mQry, AgL.GCn)
        End If

        'If RbtDirect.Checked Then
        '    mQry = "SELECT I.Code, I.Description, I.ManualCode, Null As SaleInvoiceNo, " & _
        '              " 0 As [Bal.Qty], I.Unit, 0 As [Bal.Measure], " & _
        '              " I.MeasureUnit, I.Rate As Rate, I.SalesTaxPostingGroup ,0 As MeasurePerPcs, " & _
        '              " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, I.BillingOn as BillingType, " & _
        '              " Null As SaleInvoice, Null As SaleInvoiceNo, Null As SaleInvoiceSr, " & _
        '              " Null As ReferenceDocId, " & _
        '              " Null As ReferenceDocIdSr, " & _
        '              " Null As PurchaseNo, " & _
        '              " Null As ExpiryDate, 0 As MRP, " & _
        '              " Null As LotNo, " & _
        '              " Null As Sale_Rate, " & _
        '              " Null As PurchaseRate " & _
        '              " FROM Item I " & _
        '              " LEFT JOIN Unit U On I.Unit = U.Code " & _
        '              " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
        '              " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        '    Dgl1.AgHelpDataSet(Col1Item, 23) = AgL.FillData(mQry, AgL.GCn)
        'Else
        '    mQry = " SELECT Max(L.Item) As Code, Max(I.Description) as Description, " & _
        '            " Max(I.ManualCode) As ManualCode,   " & _
        '            " Max(H.PartyOrderNo) PartyOrderNo,   " & _
        '            " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS SaleOrderRefNo,   " & _
        '            " Max(H.V_Date) as SaleOrderDate,  " & _
        '            " Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " & _
        '            " Max(I.Unit) as Unit,   " & _
        '            " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " & _
        '            " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,   " & _
        '            " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup, L.SaleOrder,   " & _
        '            " Max(L.MeasurePerPcs) as MeasurePerPcs, L.SaleOrderSr,   " & _
        '            " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " & _
        '            " Max(L.BillingType) As BillingType,  " & _
        '            " Max(U1.DecimalPlaces) as MeasureDecimalPlaces,   " & _
        '            " Max(L.RatePerQty) as RatePerQty, " & _
        '            " Max(L.RatePerMeasure) as RatePerMeasure " & _
        '            " FROM (  " & _
        '            "     SELECT DocID, V_Type, ReferenceNo, V_Date , PartyOrderNo  " & _
        '            "     FROM SaleOrder    " & _
        '            "     WHERE SaleToParty ='" & TxtSaleToParty.Tag & "'   " & _
        '            "     And Div_Code = '" & TxtDivision.Tag & "'   " & _
        '            "     AND Site_Code = '" & TxtSite_Code.Tag & "'   " & _
        '            "     AND V_Date <= '" & TxtV_Date.Text & "'   " & _
        '            "     ) H   " & _
        '            " LEFT JOIN SaleOrderDetail L  ON H.DocID = L.DocId    " & _
        '            " Left Join Item I  On L.Item  = I.Code   " & _
        '            " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " & _
        '            " Left Join (   " & _
        '            "     SELECT L.SaleOrder, L.SaleOrderSr, sum (L.Qty) AS Qty, Sum(L.TotalMeasure) as TotalMeasure      " & _
        '            " 	  FROM SaleInvoiceDetail L     " & _
        '            " 	  GROUP BY L.SaleOrder, L.SaleOrderSr   " & _
        '            " 	) AS CD ON L.SaleOrder = CD.SaleOrder AND L.SaleOrderSr = CD.SaleOrderSr   " & _
        '            " LEFT JOIN Unit U On L.Unit = U.Code   " & _
        '            " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " & _
        '            " WHERE L.Qty - IfNull(Cd.Qty, 0) > 0   " & _
        '            " And L.Supplier = '" & TxtSupplier.Tag & "'" & _
        '            " GROUP BY L.SaleOrder, L.SaleOrderSr  " & _
        '            " Order By SaleOrderDate  "
        '    Dgl1.AgHelpDataSet(Col1Item, 10) = AgL.FillData(mQry, AgL.GCn)
        'End If

    End Sub

    Private Sub FCreateHelpInvoicedItem()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                strCond += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' || I.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' || I.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        mQry = "SELECT I.Code,  I.Description, I.ManualCode, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " &
               " IfNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code, " &
               " I.MeasureUnit, I.Measure As MeasurePerPcs, I.Rate As Rate, 1 As PendingQty, I.Status, " &
               " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as MeasureDecimalPlaces, I.BillingOn as BillingType " &
               " FROM Item I " &
               " LEFT JOIN Unit U On I.Unit = U.Code " &
               " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
               " Where 1=1 " &
               " And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & " "
        Dgl1.AgHelpDataSet(Col1Item_Invoiced, 12) = AgL.FillData(mQry, AgL.GcnRead)
    End Sub

    Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "SaleInvoice_Print", "Estimate", , , , TxtSaleToParty.Tag, TxtV_Date.Text)
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet, DsRep1 As New DataSet
        Dim RepName As String = "", RepTitle As String = ""
        Dim bTableName As String = "", bSecTableName As String = "", bCondstr As String = ""
        Dim mOtherFields$ = ""
        Dim mBalanceQry$ = ""
        Try
            Me.Cursor = Cursors.WaitCursor

            AgL.PubReportTitle = "Estimate"
            RepName = "SaleInvoice_Print" : RepTitle = "Estimate"

            mQry = "SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, " &
                    "H.Godown, H.Vendor, H.SaleToParty, Sg.DispName As SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyCity, C.CityName As SaleToPartyCityName, " &
                    "H.SaleToPartyMobile, H.SaleToPartyTinNo, H.SaleToPartyCstNo, H.ShipToParty, H.ShipToPartyName, " &
                    "H.ShipToPartyAddress, H.ShipToPartyCity, H.ShipToPartyMobile, H.SaleOrder, H.SaleChallan, " &
                    "H.Currency, H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, " &
                    "H.Transporter, H.Vehicle, H.VehicleDescription, H.Driver, H.DriverName, " &
                    "H.DriverContactNo, H.LrNo, H.LrDate, H.PrivateMark, H.PortOfLoading, " &
                    "H.DestinationPort, H.FinalPlaceOfDelivery, H.PreCarriageBy, H.PlaceOfPreCarriage, " &
                    "H.ShipmentThrough, H.CreditDays, H.ReferenceDocId, H.Remarks, " &
                    "H.TotalQty, H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, " &
                    "H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, " &
                    "H.MoveToLogDate, H.IsDeleted, H.Status, H.UID, H.PaymentMode, H.TableCode, " &
                    "H.PostingAc, H.SaleToPartyAdd1, H.SaleToPartyAdd2, H.CustomFields, " &
                    "H.CreditLimit, H.BaleNoStr, H.InvoiceGenType, H.TotalDeliveryMeasure, H.TotalBale," &
                    "L.SaleOrder, L.SaleOrderSr, L.SaleChallan, L.SaleChallanSr, L.Item, L.Specification, " &
                    "L.SalesTaxGroupItem, L.DocQty, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, " &
                    "L.TotalDocMeasure, L.TotalMeasure, L.Rate, L.Amount, L.ReferenceDocId, " &
                    "L.LotNo, L.UID, L.BaleNo, L.Remark, " &
                    "L.BillingType, L.Item_UID, L.ItemInvoiceGroup, L.SaleInvoice, " &
                    "L.SaleInvoiceSr, L.DeliveryMeasure, L.DeliveryMeasureMultiplier, " &
                    "L.TotalDeliveryMeasure, L.RateType, " &
                    "I.Description AS ItemDesc, I.ManualCode As ItemManualCode, L.ExpiryDate, " &
                    "G.Description AS GodownDesc, Sg.DispName As PartyName, " &
                    "Sg.ManualCode As PartyManualCode, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName, L.MRP, " &
                    " " & AgCalcGrid1.FFooterTableFieldNameStr("H.", "H_") & ", " &
                    " " & AgCalcGrid1.FLineTableFieldNameStr("L.", "L_") & " " &
                    " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " &
                    "FROM SaleInvoice H   " &
                    "LEFT JOIN SaleInvoiceDetail L   ON H.DocID = L.DocId " &
                    "LEFT JOIN Godown G   ON H.Godown = G.Code " &
                    "LEFT JOIN Item I   ON L.Item = I.Code " &
                    "LEFT JOIN SubGroup Sg   ON H.SaleToParty = Sg.SubCode " &
                    "LEFT JOIN City C   ON Sg.CityCode = C.CityCode " &
                    "WHERE H.DocID = '" & mSearchCode & "'"
            'If Not AgL.StrCmp(TxtNature.Text, "Cash") Then
            mBalanceQry = " SELECT V1.SubCode, Sum(V1.CurrBillAmt) AS CurrBillAmt, Sum(V1.OtherTodaysBill) AS OtherTodaysBill, " &
                        " Sum(V1.OldBalance) AS OldBalance, Sum(V1.TodaysBalance) AS TodaysPayment, " &
                        " Sum(V1.OldBalance) + Sum(V1.OtherTodaysBill) + Sum(V1.CurrBillAmt)-Sum(V1.TodaysBalance) As NetBalance " &
                        " FROM ( " &
                        " SELECT L.SubCode, IfNull(Sum(L.AmtDr),0) AS CurrBillAmt, 0 AS OtherTodaysBill,0 AS OldBalance, 0 AS TodaysBalance  " &
                        " FROM Ledger L WHERE L.DocId = '" & mSearchCode & "' AND L.SubCode = '" & TxtBillToParty.Tag & "' GROUP BY L.SubCode " &
                        " UNION ALL  " &
                        " SELECT L.SubCode, 0 AS CurrBillAmt, IfNull(Sum(L.AmtDr),0) AS OtherTodaysBill,0 AS OldBalance, 0 AS TodaysBalance  " &
                        " FROM Ledger L WHERE L.V_Date = '" & TxtV_Date.Text & "' And L.DocId <> '" & mSearchCode & "' AND L.SubCode = '" & TxtBillToParty.Tag & "'  GROUP BY L.SubCode " &
                        " UNION ALL  " &
                        " SELECT L.SubCode, 0 AS CurrBillAmt, 0 AS OtherTodaysBill,IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0)  AS OldBalance, 0 AS TodaysBalance  " &
                        " FROM Ledger L WHERE L.V_Date < '" & TxtV_Date.Text & "' AND L.SubCode = '" & TxtBillToParty.Tag & "' GROUP BY L.SubCode " &
                        " UNION ALL  " &
                        " SELECT L.SubCode, 0 AS CurrBillAmt, 0 AS OtherTodaysBill,0 AS OldBalance, IfNull(Sum(L.AmtCr),0) AS TodaysBalance  " &
                        " FROM Ledger L WHERE  L.V_Date = '" & TxtV_Date.Text & "' AND L.SubCode = '" & TxtBillToParty.Tag & "' GROUP BY L.SubCode " &
                        " ) AS V1  " &
                        " GROUP BY V1.SubCode "


            DsRep1 = AgL.FillData(mBalanceQry, AgL.GCn)


            DsRep = AgL.FillData(mQry, AgL.GCn)

            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))

            AgPL.CreateFieldDefFile1(DsRep1, AgL.PubReportPath & "\" & RepName & "1.ttx", True)
            mCrd.OpenSubreport("SUBREP2").Database.Tables(0).SetDataSource(DsRep1.Tables(0))


            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd

            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)
            Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
End Class
