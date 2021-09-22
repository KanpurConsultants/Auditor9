Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmSaleInvoice
    Inherits AgTemplate.TempTransaction
    Dim mQry$


    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1SaleChallan As String = "KOT"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Protected Const Col1Outlet As String = "Outlet"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"

    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2Select As String = "Select"
    Protected Const Col2SaleChallan As String = "KOT No"

    Dim mTable$ = ""

    Private Const PaymentMode_Cash As String = "Cash"
    Private Const PaymentMode_Credit As String = "Credit"
    Protected WithEvents TxtToPayAmount As AgControls.AgTextBox
    Protected WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Private Const PaymentMode_Complementary As String = "Complementary"

    Public Class HelpDataSet
        Public Shared SaleChallan As DataSet = Nothing
        Public Shared Table As DataSet = Nothing
        Public Shared Steward As DataSet = Nothing
        Public Shared KotNature As DataSet = Nothing
        Public Shared Item As DataSet = Nothing
        Public Shared ItemCode As DataSet = Nothing
        Public Shared OutLet As DataSet = Nothing
        Public Shared AgStructure As DataSet = Nothing
        Public Shared SalesTaxGroupParty As DataSet = Nothing
        Public Shared PaymentMode As DataSet = Nothing
        Public Shared PositingAc As DataSet = Nothing
    End Class

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        Me.EntryNCat = AgTemplate.ClsMain.Temp_NCat.SaleInvoice
    End Sub

    Public Property Table() As String
        Get
            Table = mTable
        End Get
        Set(ByVal value As String)
            mTable = value
        End Set
    End Property

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.LblTableReq = New System.Windows.Forms.Label
        Me.TxtTable = New AgControls.AgTextBox
        Me.LblTable = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.PnlCShowGrid2 = New System.Windows.Forms.Panel
        Me.PnlCShowGrid = New System.Windows.Forms.Panel
        Me.LblReferenceNoReq = New System.Windows.Forms.Label
        Me.TxtReferenceNo = New AgControls.AgTextBox
        Me.LblReferenceNo = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.LblRemark = New System.Windows.Forms.Label
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtStructure = New AgControls.AgTextBox
        Me.TxtPaymentMode = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtAccount = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtToPayAmount = New AgControls.AgTextBox
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel
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
        Me.GroupBox2.Location = New System.Drawing.Point(758, 550)
        Me.GroupBox2.Size = New System.Drawing.Size(148, 40)
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Location = New System.Drawing.Point(3, 19)
        Me.TxtStatus.Size = New System.Drawing.Size(142, 18)
        Me.TxtStatus.Tag = ""
        '
        'CmdStatus
        '
        Me.CmdStatus.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(653, 581)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(368, 581)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 19)
        Me.TxtApproveBy.Size = New System.Drawing.Size(142, 18)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(231, 550)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 550)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 546)
        Me.GroupBox1.Size = New System.Drawing.Size(928, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(487, 550)
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
        Me.TxtDocId.Location = New System.Drawing.Point(841, 287)
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblV_No
        '
        Me.LblV_No.Location = New System.Drawing.Point(215, 287)
        Me.LblV_No.Size = New System.Drawing.Size(80, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Voucher No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(323, 286)
        Me.TxtV_No.Size = New System.Drawing.Size(170, 18)
        Me.TxtV_No.TabIndex = 2
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(109, 61)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(8, 56)
        Me.LblV_Date.Size = New System.Drawing.Size(87, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Voucher Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(109, 41)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(128, 55)
        Me.TxtV_Date.Size = New System.Drawing.Size(168, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(8, 36)
        Me.LblV_Type.Size = New System.Drawing.Size(88, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Voucher Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(128, 35)
        Me.TxtV_Type.Size = New System.Drawing.Size(168, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(108, 21)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(7, 16)
        Me.LblSite_Code.Size = New System.Drawing.Size(53, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch "
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(127, 15)
        Me.TxtSite_Code.Size = New System.Drawing.Size(168, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Location = New System.Drawing.Point(794, 289)
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(275, 287)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-5, 19)
        Me.TabControl1.Size = New System.Drawing.Size(915, 300)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LinkLabel3)
        Me.TP1.Controls.Add(Me.TxtToPayAmount)
        Me.TP1.Controls.Add(Me.PnlCalcGrid)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.Pnl2)
        Me.TP1.Controls.Add(Me.LblReferenceNoReq)
        Me.TP1.Controls.Add(Me.TxtReferenceNo)
        Me.TP1.Controls.Add(Me.LinkLabel1)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Controls.Add(Me.LblTableReq)
        Me.TP1.Controls.Add(Me.TxtTable)
        Me.TP1.Controls.Add(Me.LblTable)
        Me.TP1.Controls.Add(Me.TxtAccount)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.Label27)
        Me.TP1.Controls.Add(Me.TxtPaymentMode)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.LblRemark)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(907, 274)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblRemark, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPaymentMode, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtAccount, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblTable, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtTable, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblTableReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtToPayAmount, 0)
        Me.TP1.Controls.SetChildIndex(Me.LinkLabel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(910, 41)
        Me.Topctrl1.TabIndex = 3
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
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'LblTableReq
        '
        Me.LblTableReq.AutoSize = True
        Me.LblTableReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblTableReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblTableReq.Location = New System.Drawing.Point(109, 101)
        Me.LblTableReq.Name = "LblTableReq"
        Me.LblTableReq.Size = New System.Drawing.Size(10, 7)
        Me.LblTableReq.TabIndex = 694
        Me.LblTableReq.Text = "Ä"
        '
        'TxtTable
        '
        Me.TxtTable.AgAllowUserToEnableMasterHelp = False
        Me.TxtTable.AgMandatory = True
        Me.TxtTable.AgMasterHelp = False
        Me.TxtTable.AgNumberLeftPlaces = 8
        Me.TxtTable.AgNumberNegetiveAllow = False
        Me.TxtTable.AgNumberRightPlaces = 2
        Me.TxtTable.AgPickFromLastValue = False
        Me.TxtTable.AgRowFilter = ""
        Me.TxtTable.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTable.AgSelectedValue = Nothing
        Me.TxtTable.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTable.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTable.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTable.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTable.Location = New System.Drawing.Point(128, 95)
        Me.TxtTable.MaxLength = 0
        Me.TxtTable.Name = "TxtTable"
        Me.TxtTable.Size = New System.Drawing.Size(168, 18)
        Me.TxtTable.TabIndex = 4
        '
        'LblTable
        '
        Me.LblTable.AutoSize = True
        Me.LblTable.BackColor = System.Drawing.Color.Transparent
        Me.LblTable.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTable.Location = New System.Drawing.Point(8, 96)
        Me.LblTable.Name = "LblTable"
        Me.LblTable.Size = New System.Drawing.Size(39, 16)
        Me.LblTable.TabIndex = 693
        Me.LblTable.Text = "Table"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Location = New System.Drawing.Point(2, 524)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(902, 23)
        Me.Panel1.TabIndex = 694
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
        Me.LblTotalAmount.Location = New System.Drawing.Point(465, 4)
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
        Me.LblTotalQtyText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(361, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(101, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(0, 343)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(902, 180)
        Me.Pnl1.TabIndex = 2
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(301, 15)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(241, 20)
        Me.LinkLabel1.TabIndex = 20
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "KOT Details"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCShowGrid2
        '
        Me.PnlCShowGrid2.Location = New System.Drawing.Point(643, 569)
        Me.PnlCShowGrid2.Name = "PnlCShowGrid2"
        Me.PnlCShowGrid2.Size = New System.Drawing.Size(7, 16)
        Me.PnlCShowGrid2.TabIndex = 741
        Me.PnlCShowGrid2.Visible = False
        '
        'PnlCShowGrid
        '
        Me.PnlCShowGrid.Location = New System.Drawing.Point(682, 566)
        Me.PnlCShowGrid.Name = "PnlCShowGrid"
        Me.PnlCShowGrid.Size = New System.Drawing.Size(12, 16)
        Me.PnlCShowGrid.TabIndex = 740
        Me.PnlCShowGrid.Visible = False
        '
        'LblReferenceNoReq
        '
        Me.LblReferenceNoReq.AutoSize = True
        Me.LblReferenceNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblReferenceNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblReferenceNoReq.Location = New System.Drawing.Point(109, 81)
        Me.LblReferenceNoReq.Name = "LblReferenceNoReq"
        Me.LblReferenceNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblReferenceNoReq.TabIndex = 752
        Me.LblReferenceNoReq.Text = "Ä"
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtReferenceNo.AgMandatory = True
        Me.TxtReferenceNo.AgMasterHelp = False
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
        Me.TxtReferenceNo.Location = New System.Drawing.Point(128, 75)
        Me.TxtReferenceNo.MaxLength = 0
        Me.TxtReferenceNo.Name = "TxtReferenceNo"
        Me.TxtReferenceNo.Size = New System.Drawing.Size(168, 18)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.AutoSize = True
        Me.LblReferenceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblReferenceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(8, 76)
        Me.LblReferenceNo.Name = "LblReferenceNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(90, 16)
        Me.LblReferenceNo.TabIndex = 751
        Me.LblReferenceNo.Text = "Reference No."
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtRemarks.Location = New System.Drawing.Point(127, 155)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(414, 18)
        Me.TxtRemarks.TabIndex = 7
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(301, 36)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(241, 90)
        Me.Pnl2.TabIndex = 8
        '
        'LblRemark
        '
        Me.LblRemark.AutoSize = True
        Me.LblRemark.BackColor = System.Drawing.Color.Transparent
        Me.LblRemark.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemark.Location = New System.Drawing.Point(8, 156)
        Me.LblRemark.Name = "LblRemark"
        Me.LblRemark.Size = New System.Drawing.Size(53, 16)
        Me.LblRemark.TabIndex = 753
        Me.LblRemark.Text = "Remark"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Location = New System.Drawing.Point(0, 322)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(123, 20)
        Me.LinkLabel2.TabIndex = 742
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Item Detail"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSalesTaxGroupParty
        '
        Me.TxtSalesTaxGroupParty.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(568, 286)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(92, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 5
        Me.TxtSalesTaxGroupParty.Visible = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(499, 289)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(105, 16)
        Me.Label27.TabIndex = 755
        Me.Label27.Text = "Sales Tax Group"
        Me.Label27.Visible = False
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(547, 15)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(359, 253)
        Me.PnlCalcGrid.TabIndex = 744
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(647, 288)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 757
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'TxtStructure
        '
        Me.TxtStructure.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtStructure.Location = New System.Drawing.Point(716, 287)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(72, 18)
        Me.TxtStructure.TabIndex = 756
        Me.TxtStructure.Text = "TxtStructure"
        Me.TxtStructure.Visible = False
        '
        'TxtPaymentMode
        '
        Me.TxtPaymentMode.AgAllowUserToEnableMasterHelp = False
        Me.TxtPaymentMode.AgMandatory = False
        Me.TxtPaymentMode.AgMasterHelp = False
        Me.TxtPaymentMode.AgNumberLeftPlaces = 8
        Me.TxtPaymentMode.AgNumberNegetiveAllow = False
        Me.TxtPaymentMode.AgNumberRightPlaces = 2
        Me.TxtPaymentMode.AgPickFromLastValue = False
        Me.TxtPaymentMode.AgRowFilter = ""
        Me.TxtPaymentMode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPaymentMode.AgSelectedValue = Nothing
        Me.TxtPaymentMode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPaymentMode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPaymentMode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPaymentMode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPaymentMode.Location = New System.Drawing.Point(128, 115)
        Me.TxtPaymentMode.MaxLength = 20
        Me.TxtPaymentMode.Name = "TxtPaymentMode"
        Me.TxtPaymentMode.Size = New System.Drawing.Size(168, 18)
        Me.TxtPaymentMode.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 759
        Me.Label1.Text = "Payment Mode"
        '
        'TxtAccount
        '
        Me.TxtAccount.AgAllowUserToEnableMasterHelp = False
        Me.TxtAccount.AgMandatory = False
        Me.TxtAccount.AgMasterHelp = False
        Me.TxtAccount.AgNumberLeftPlaces = 8
        Me.TxtAccount.AgNumberNegetiveAllow = False
        Me.TxtAccount.AgNumberRightPlaces = 2
        Me.TxtAccount.AgPickFromLastValue = False
        Me.TxtAccount.AgRowFilter = ""
        Me.TxtAccount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAccount.AgSelectedValue = Nothing
        Me.TxtAccount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAccount.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.Location = New System.Drawing.Point(127, 135)
        Me.TxtAccount.MaxLength = 20
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.Size = New System.Drawing.Size(414, 18)
        Me.TxtAccount.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 16)
        Me.Label3.TabIndex = 761
        Me.Label3.Text = "Ledger Account"
        '
        'TxtToPayAmount
        '
        Me.TxtToPayAmount.AgAllowUserToEnableMasterHelp = False
        Me.TxtToPayAmount.AgMandatory = False
        Me.TxtToPayAmount.AgMasterHelp = False
        Me.TxtToPayAmount.AgNumberLeftPlaces = 8
        Me.TxtToPayAmount.AgNumberNegetiveAllow = False
        Me.TxtToPayAmount.AgNumberRightPlaces = 2
        Me.TxtToPayAmount.AgPickFromLastValue = False
        Me.TxtToPayAmount.AgRowFilter = ""
        Me.TxtToPayAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToPayAmount.AgSelectedValue = Nothing
        Me.TxtToPayAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToPayAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToPayAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToPayAmount.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToPayAmount.Location = New System.Drawing.Point(302, 227)
        Me.TxtToPayAmount.MaxLength = 20
        Me.TxtToPayAmount.Name = "TxtToPayAmount"
        Me.TxtToPayAmount.Size = New System.Drawing.Size(240, 37)
        Me.TxtToPayAmount.TabIndex = 762
        Me.TxtToPayAmount.TabStop = False
        Me.TxtToPayAmount.Text = "88888888.88"
        Me.TxtToPayAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LinkLabel3
        '
        Me.LinkLabel3.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel3.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel3.LinkColor = System.Drawing.Color.White
        Me.LinkLabel3.Location = New System.Drawing.Point(301, 204)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(241, 20)
        Me.LinkLabel3.TabIndex = 763
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "To Pay Amount"
        Me.LinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmSaleInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(910, 603)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.PnlCShowGrid2)
        Me.Controls.Add(Me.PnlCShowGrid)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmSaleInvoice"
        Me.Text = "  "
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.PnlCShowGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlCShowGrid2, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel2, 0)
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

    End Sub
    Protected WithEvents LblTable As System.Windows.Forms.Label
    Protected WithEvents TxtTable As AgControls.AgTextBox
    Protected WithEvents LblTableReq As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents PnlCShowGrid2 As System.Windows.Forms.Panel
    Protected WithEvents PnlCShowGrid As System.Windows.Forms.Panel
    Protected WithEvents LblReferenceNoReq As System.Windows.Forms.Label
    Protected WithEvents TxtReferenceNo As AgControls.AgTextBox
    Protected WithEvents LblReferenceNo As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Pnl2 As System.Windows.Forms.Panel
    Protected WithEvents LblRemark As System.Windows.Forms.Label
    Protected WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents Label27 As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents TxtPaymentMode As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtAccount As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
#End Region

    Private Sub FrmSaleInvoice_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim I As Integer = 0

        mQry = " UPDATE SaleChallan Set SaleInvoice = NULL Where SaleInvoice = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl2
            For I = 0 To .Rows.Count - 1
                If AgL.StrCmp(.Item(Col2Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                    mQry = " UPDATE SaleChallan Set SaleInvoice = '" & SearchCode & "' Where DocId = '" & .AgSelectedValue(Col2SaleChallan, I) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

        If AgL.StrCmp(TxtPaymentMode.Text, PaymentMode_Complementary) Then
            AccountPosting()
        End If
    End Sub

    Private Sub FrmSaleInvoice_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " UPDATE SaleChallan Set SaleInvoice = NULL Where SaleInvoice = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "saleinvoice"
        MainLineTableCsv = "saleinvoiceDetail"

        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl2)
        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        mQry = "Select DocID As SearchCode " & _
                " From saleinvoice H " & _
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
                " Where IsNull(IsDeleted,0)=0  " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = "SELECT H.DocId as SearchCode, Vt.Description AS [Entry Type], " & _
                            " H.V_Date AS [Entry Date], H.V_No AS [Entry No], H.ReferenceNo  " & _
                            " FROM saleinvoice H " & _
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                            " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SaleChallan, 90, 0, Col1SaleChallan, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, True, True)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 130, 0, Col1SalesTaxGroup, False, False)
            .AddAgTextColumn(Dgl1, Col1Outlet, 100, 0, Col1Outlet, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 3, False, Col1Qty, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True

        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgCheckColumn(Dgl2, Col2Select, 50, Col2Select, True)
            .AddAgTextColumn(Dgl2, Col2SaleChallan, 170, 0, Col2SaleChallan, True, True)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 25
        Dgl2.AllowUserToAddRows = False
        Dgl2.AgSkipReadOnlyColumns = True

        AgCalcGrid1.Ini_Grid(TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index

        FrmKOT_BaseFunction_FIniList()
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "  Update SaleInvoice " & _
                " SET  " & _
                " ReferenceNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " & _
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " & _
                " SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.AgSelectedValue) & ", " & _
                " TableCode = " & AgL.Chk_Text(TxtTable.AgSelectedValue) & ", " & _
                " PaymentMode = " & AgL.Chk_Text(TxtPaymentMode.Text) & ", " & _
                " PostingAc = " & AgL.Chk_Text(TxtAccount.AgSelectedValue) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " TotalAmount = " & Val(LblTotalAmount.Text) & " " & _
                " Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        AgCalcGrid1.FUpdateFooterTable("SaleInvoice", "DocId", SearchCode, Conn, Cmd)
        'AgCalcGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)

        mQry = "Delete From SaleInvoiceDetail Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                mQry = "Insert Into SaleInvoiceDetail(DocId, Sr, SaleChallan, Item, SalesTaxGroupItem, Outlet, Qty, Rate, Amount) " & _
                        " Values( " & _
                        " " & AgL.Chk_Text(SearchCode) & ", " & mSr & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1SaleChallan, I)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1SalesTaxGroup, I)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Outlet, I)) & ", " & _
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & " " & _
                        " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                AgCalcGrid1.FUpdateLineTable("SaleInvoiceDetail", "DocId", "Sr", SearchCode, mSr, I, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.* " & _
                " From saleinvoice H " & _
                " Where H.DocID='" & SearchCode & "'"
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

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtTable.AgSelectedValue = AgL.XNull(.Rows(0)("TableCode"))
                TxtPaymentMode.Text = AgL.XNull(.Rows(0)("PaymentMode"))
                TxtAccount.AgSelectedValue = AgL.XNull(.Rows(0)("PostingAc"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))



                'AgCalcGrid1.MoveRec_TransFooter(SearchCode)
                AgCalcGrid1.FMoveRecFooterTable("SaleInvoice", "DocID", SearchCode, TxtV_Date.Text)

                mQry = "Select Distinct Id.SaleChallan, C.V_Date As SaleChallanDate " & _
                            " from SaleInvoiceDetail Id " & _
                            " LEFT JOIN SaleChallan C On Id.SaleChallan = C.DocId " & _
                            " where Id.DocId = '" & SearchCode & "' "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl2.RowCount = 1
                    Dgl2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            If AgL.XNull(.Rows(I)("SaleChallan")) <> "" Then
                                Dgl2.Rows.Add()
                                Dgl2.Item(Col2Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
                                Dgl2.AgSelectedValue(Col2SaleChallan, I) = AgL.XNull(.Rows(I)("SaleChallan"))
                            End If
                        Next I
                    End If
                End With


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = "Select L.* " & _
                            " from saleinvoiceDetail L " & _
                            " Where L.DocId = '" & SearchCode & "' " & _
                            " Order By L.Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1SaleChallan, I) = AgL.XNull(.Rows(I)("SaleChallan"))
                            Dgl1.AgSelectedValue(Col1ItemCode, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.AgSelectedValue(Col1Outlet, I) = AgL.XNull(.Rows(I)("Outlet"))
                            Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                            Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")

                            AgCalcGrid1.FMoveRecLineTable("SaleInvoiceDetail", "DocID", "Sr", SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                            'Call AgCalcGrid1.MoveRec_TransLine(mSearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If
                End With
                '-------------------------------------------------------------
            End If
        End With
        TxtToPayAmount.Text = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 631, 916, 0, 0)
        Topctrl1.ChangeAgGridState(Dgl1, False)
        Topctrl1.ChangeAgGridState(Dgl2, False)
        AgCalcGrid1.FrmType = Me.FrmType
        If mTable <> "" Then
            Topctrl1.FButtonClick(0)
            TxtTable.AgSelectedValue = mTable
            Call ProcFillPendingChallans(TxtTable.AgSelectedValue)
            Call ProcFillItems()
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtTable.Validating, TxtReferenceNo.Validating, TxtPaymentMode.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    AgCalcGrid1.AgNCat = LblV_Type.Tag
                    IniGrid()

                Case TxtReferenceNo.Name
                    e.Cancel = FIsDuplicateReferenceNo()

                Case TxtTable.Name
                    Call ProcFillPendingChallans(TxtTable.AgSelectedValue)
                    ProcFillItems()
                Case TxtPaymentMode.Name
                    Select Case TxtPaymentMode.Text
                        Case PaymentMode_Cash
                            TxtAccount.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("CashAc"))
                        Case PaymentMode_Credit
                            TxtAccount.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("PostingAc"))
                        Case Else
                            TxtAccount.AgSelectedValue = ""
                    End Select
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "SaleInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = LblV_Type.Tag
        IniGrid()
        TxtSalesTaxGroupParty.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim bItemCode$ = ""
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
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
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")

                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        AgCalcGrid1.Calculation()
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.00")
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")
        TxtToPayAmount.Text = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtTable, LblTable.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" & Dgl1.Columns(Col1SaleChallan).Index & "," & Dgl1.Columns(Col1Item).Index & "") Then passed = False : Exit Sub

        If FIsDuplicateReferenceNo() Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1Rate, I).Value) = 0 Then
                        MsgBox("Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Rate, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub TempsaleinvoiceCommon_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = " Select H.Code, H.Description From HT_Table H "
        HelpDataSet.Table = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select Sg.SubCode as Code, Sg.DispName As Steward From SubGroup Sg "
        HelpDataSet.Steward = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select H.Code, H.Description From KotNature H "
        HelpDataSet.KotNature = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select H.Code, H.Description As Outlet From Outlet H "
        HelpDataSet.OutLet = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select H.Code, H.Description As Item, H.Outlet, H.Rate From Item H "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select H.Code, H.ManualCode As ItemCode, H.Outlet, H.Rate From Item H "
        HelpDataSet.ItemCode = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select H.DocId As Code, H.ReferenceNo As KOT From SaleChallan H "
        HelpDataSet.SaleChallan = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Description  FROM Structure ORDER BY Description "
        HelpDataSet.AgStructure = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Description AS Code, Description, IsNull(Active,0)  FROM PostingGroupSalesTaxParty "
        HelpDataSet.SalesTaxGroupParty = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select '" & PaymentMode_Cash & "' As Code, '" & PaymentMode_Cash & "' As Description " & _
                " UNION ALL " & _
                " Select '" & PaymentMode_Credit & "' As Code, '" & PaymentMode_Credit & "' As Description  " & _
                " UNION ALL " & _
                " Select '" & PaymentMode_Complementary & "' As Code, '" & PaymentMode_Complementary & "' As Description  "
        HelpDataSet.PaymentMode = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select Sg.SubCode As Code, Sg.DispName As Name From SubGroup Sg  "
        HelpDataSet.PositingAc = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Function FIsDuplicateReferenceNo() As Boolean

        mQry = "Select Count(*) from saleinvoice H " & _
               " Where H.ReferenceNo = '" & TxtReferenceNo.Text & "' " & _
               " And H.DocID <> '" & TxtDocId.Text & "' " & _
               " And H.Site_Code ='" & TxtSite_Code.AgSelectedValue & "' " & _
               " And H.Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
               " And H.V_Type = '" & TxtV_Type.AgSelectedValue & "'  " & _
               " And IsNull(H.IsDeleted,0)=0  "

        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar > 0 Then
            MsgBox("Reference No. already exists")
            FIsDuplicateReferenceNo = True
        End If
    End Function

    Private Sub FrmKOT_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        TxtTable.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Table
        Dgl1.AgHelpDataSet(Col1Item) = HelpDataSet.Item
        Dgl1.AgHelpDataSet(Col1ItemCode) = HelpDataSet.ItemCode
        Dgl1.AgHelpDataSet(Col1Outlet) = HelpDataSet.OutLet
        Dgl1.AgHelpDataSet(Col1SaleChallan) = HelpDataSet.SaleChallan
        Dgl2.AgHelpDataSet(Col2SaleChallan, 8) = HelpDataSet.SaleChallan
        TxtStructure.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.AgStructure
        TxtSalesTaxGroupParty.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.SalesTaxGroupParty
        TxtPaymentMode.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.PaymentMode
        TxtAccount.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.PositingAc
    End Sub



    Private Sub ProcFillPendingChallans(ByVal bTable As String)
        Dim DtTemp As DataTable = Nothing
        Dim bConStr$ = ""
        Dim I As Integer = 0
        Try
            mQry = "SELECT H.DocId As ChallanNo  " & _
                    " FROM SaleChallan H  " & _
                    " WHERE H.TableCode = '" & bTable & "'  " & _
                    " And SaleInvoice Is Null "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl2.RowCount = 1
                Dgl2.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl2.Rows.Add()
                        Dgl2.Item(Col2Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
                        Dgl2.AgSelectedValue(Col2SaleChallan, I) = AgL.XNull(.Rows(I)("ChallanNo"))
                    Next I
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillItems()
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim bChallanStr$ = ""
        Try
            With Dgl2
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        If .Item(Col2SaleChallan, I).Value <> "" And AgL.StrCmp(.Item(Col2Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                            If bChallanStr = "" Then
                                bChallanStr = "'" & .AgSelectedValue(Col2SaleChallan, I) & "'"
                            Else
                                bChallanStr &= "," & "'" & .AgSelectedValue(Col2SaleChallan, I) & "'"
                            End If
                        End If
                    Next
                End If
            End With

            mQry = " Select L.*, I.SalesTaxPostingGroup " & _
                   " From SaleChallanDetail L " & _
                   " Left Join Item I On L.Item = I.Code " & _
                   " Where L.DocId In (" & bChallanStr & ") "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.AgSelectedValue(Col1SaleChallan, I) = AgL.XNull(.Rows(I)("DocId"))
                        Dgl1.AgSelectedValue(Col1ItemCode, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.AgSelectedValue(Col1Outlet, I) = AgL.XNull(.Rows(I)("Outlet"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                        Dgl1.AgSelectedValue(Col1SalesTaxGroup, I) = AgL.XNull(DtTemp.Rows(I)("SalesTaxPostingGroup"))
                        If AgL.StrCmp(Dgl1.Item(Col1SalesTaxGroup, I).Tag, "") Then
                            If AgL.PubDtEnviro.Rows.Count > 0 Then
                                Dgl1.AgSelectedValue(Col1SalesTaxGroup, I) = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                            End If
                        End If
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call ProcFillItems()
    End Sub

    Private Sub DGL2_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl2.CellMouseUp
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex

            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col2Select
                    Try
                        Call AgL.ProcSetCheckColumnCellValue(sender, Dgl2.CurrentCell.ColumnIndex)
                        ProcFillItems()
                    Catch ex As Exception
                    End Try
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl2.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Dim mRowIndex As Integer = 0, mColumnIndex As Integer = 0
        Try
            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col2Select
                    If e.KeyCode = Keys.Space Then
                        Try
                            Call AgL.ProcSetCheckColumnCellValue(sender, Dgl2.CurrentCell.ColumnIndex)
                            ProcFillItems()
                        Catch ex As Exception
                        End Try
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchaseEntry_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
        Dim bTableName As String = "", bSecTableName As String = "", bCondstr As String = ""
        Dim bStructJoin As String = ""

        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "Sale Invoice"
            RepName = "Ht_SaleInvoice_Print" : RepTitle = "Sale Invoice"
            bTableName = "SaleInvoice" : bSecTableName = "SaleInvoiceDetail L ON L.DocID =H.DocID"
            bCondstr = "WHERE H.DocID='" & SearchCode & "'"

            bStructJoin = " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQueryFooter(AgL, AgTemplate.ClsMain.Temp_NCat.SaleInvoice, FrmType) & ") As SF On H.DocId = SF.DocId " & _
                           " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQuery(AgL, AgTemplate.ClsMain.Temp_NCat.SaleInvoice, FrmType) & ") As SL On L.DocId = SL.DocId And L.Sr = Sl.TSr "


            mQry = " SELECT  H.DocID, H.V_Type + ' - ' +convert(NVARCHAR(5),H.V_No) AS VoucherNo, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, " & _
                    " H.ReferenceNo, H.SaleToParty, H.SaleChallan, H.Currency, H.SalesTaxGroupParty, H.Structure, " & _
                    " H.BillingType, H.Remarks, H.TotalQty, H.TotalMeasure, H.TotalAmount, " & _
                    " H.EntryBy, H.EntryDate,  " & _
                    " H.SaleToPartyAddress, H.SaleToPartyMobile," & _
                    " H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, H.UID, " & _
                    " L.SaleChallan, L.Item, L.Specification, L.SalesTaxGroupItem, L.DocQty, L.Qty,SM.Name AS SiteName,H.SaleToPartyName AS SaleToPartyName,C.CityName , " & _
                    " L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalDocMeasure, L.TotalMeasure, L.Rate, L.Amount, " & _
                    " PC.V_Type AS PCV_Type,PC.V_No AS PCV_No,PC.ReferenceNo AS PCVoucherNo, I.ManualCode As ItemCode,I.Description AS ItemDesc, " & _
                    " T.Description As TableDesc,  " & _
                    " H.Gross_Amount, H.Discount_Pre_Tax_Per, H.Discount_Pre_Tax, H.Other_Additions_Pre_Tax_Per,  " & _
                    " H.Other_Additions_Pre_Tax, " & _
                    " H.Sales_Tax_Taxable_Amt, H.Vat_Per, H.Vat, H.Sat_Per, H.Sat, H.Discount_Per, H.Discount,  " & _
                    " H.Other_Charges_Per, H.Other_Charges, H.Round_Off, H.Net_Amount, H.Landed_Value " & _
                    " FROM " & bTableName & " H " & _
                    " LEFT JOIN " & bSecTableName & "  " & _
                    " LEFT JOIN SiteMast SM ON SM.Code =H.Site_Code  " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode=H.SaleToParty  " & _
                    " LEFT JOIN City C ON C.CityCode =H.SaleToPartyCity  " & _
                    " LEFT JOIN SaleChallan PC ON PC.DocID=L.SaleChallan  " & _
                    " LEFT JOIN Item I ON I.Code=L.Item  " & _
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type= H.V_Type " & _
                    " LEFT JOIN Ht_Table T On H.TableCode = T.Code " & _
                    " " & bCondstr & ""

            AgL.ADMain = New SqlClient.SqlDataAdapter(mQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))
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

    Private Function AccountPosting() As Boolean
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, J As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim mNarr As String = "", mCommonNarr$ = ""
        Dim mNetAmount As Double, mRoundOff As Double = 0
        Dim GcnRead As SqlClient.SqlConnection
        GcnRead = New SqlClient.SqlConnection
        GcnRead.ConnectionString = AgL.Gcn_ConnectionString
        GcnRead.Open()

        mNetAmount = 0
        mCommonNarr = ""
        mCommonNarr = ""
        If mCommonNarr.Length > 255 Then mCommonNarr = AgL.MidStr(mCommonNarr, 0, 255)

        ReDim Preserve LedgAry(I)
        I = UBound(LedgAry) + 1
        ReDim Preserve LedgAry(I)
        LedgAry(I).SubCode = TxtAccount.AgSelectedValue
        LedgAry(I).ContraSub = AgL.XNull(AgL.PubDtEnviro.Rows(0)("SaleAc"))
        LedgAry(I).AmtCr = 0
        LedgAry(I).AmtDr = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
        If mNarr.Length > 255 Then mNarr = AgL.MidStr(mNarr, 0, 255)
        LedgAry(I).Narration = mNarr

        I = UBound(LedgAry) + 1
        ReDim Preserve LedgAry(I)
        LedgAry(I).SubCode = AgL.XNull(AgL.PubDtEnviro.Rows(0)("SaleAc"))
        LedgAry(I).ContraSub = TxtAccount.AgSelectedValue
        LedgAry(I).AmtCr = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
        LedgAry(I).AmtDr = 0
        LedgAry(I).Narration = mNarr

        If AgL.PubManageOfflineData Then
            If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GcnSite, AgL.ECmdSite, mSearchCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.GcnSite_ConnectionString) = False Then
                AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
            Else
            End If
        End If

        If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GCn, AgL.ECmd, mSearchCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.Gcn_ConnectionString) = False Then
            AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
        End If
        GcnRead.Close()
        GcnRead.Dispose()
    End Function


    Private Sub TxtRemarks_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtRemarks.KeyDown
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If

    End Sub
End Class
