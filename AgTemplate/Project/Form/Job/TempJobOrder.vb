Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class TempJobOrder
    Inherits AgTemplate.TempTransaction
    Public mQry$
    Dim DsMain As DataSet = Nothing
    Public Event BaseFunction_PostOrderGridFill()
    Public Event BaseFunction_ConsumptionGridFill()
    Public Event BaseFunction_PostConsumptionGridFill()

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)

    Protected Const ColSNo As String = "S.No."
    Protected WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1JobworkerRateGroup As String = "Rate Group"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1FromProcess As String = "From Process"
    Protected Const Col1CurrStock As String = "Curr.Stock"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1ProdPlan As String = "Production Plan"
    Protected Const Col1ProdOrder As String = "Production Order"
    Protected Const Col1LossPer As String = "Loss %"
    Protected Const Col1Loss As String = "Loss"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1ReceiveQty As String = "Receive Qty"
    Protected Const Col1ReceiveMeasure As String = "Receive Measure"
    Protected Const Col1CancelQty As String = "Cancel Qty"
    Protected Const Col1CancelMeasure As String = "Cancel Measure"
    Protected Const Col1ReceiveLoss As String = "Receive Loss"
    Protected Const Col1BOM As String = "BOM"
    Protected Const Col1ItemGroup As String = "ItemGroup"
    Protected Const Col1ItemCategory As String = "ItemCategory"

    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2Item As String = "Item"
    Protected Const Col2Qty As String = "Qty"
    Protected Const Col2Unit As String = "Unit"
    Protected Const Col2PrevProcess As String = "Prev Process"
    Protected Const Col2MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col2TotalMeasure As String = "Total Measure"
    Protected Const Col2MeasureUnit As String = "Measure Unit"
    Protected Const Col2IssuedQty As String = "Issued Qty"
    Protected Const Col2IssuedMeasure As String = "Issued Measure"
    Protected Const Col2ReturnQty As String = "Return Qty"
    Protected Const Col2ReturnMeasure As String = "Return Measure"
    Protected Const Col2ConsumedQty As String = "Consumed Qty"
    Protected Const Col2ConsumedMeasure As String = "Consumed Measure"
    Protected Const Col2ActualConsumedQty As String = "Actual Consumed Qty"
    Protected Const Col2ActualConsumedMeasure As String = "Actual Consumed Measure"
    Protected Const Col2CancelQty As String = "Cancel Qty"
    Protected Const Col2CancelMeasure As String = "Cancel Measure"

    Public WithEvents Dgl3 As New AgControls.AgDataGrid
    Protected Const Col3Parameter As String = "Parameter"
    Protected Const Col3StdValue As String = "Standard Value"

    Public WithEvents Dgl4 As New AgControls.AgDataGrid
    Protected Const Col4Item As String = "Item"
    Protected Const Col4Qty As String = "Qty"
    Protected Const Col4CancelQty As String = "CancelQty"
    Protected Const Col4Unit As String = "Unit"
    Protected Const Col4MeasurePerPcs As String = "MeasurePerPcs"
    Protected Const Col4TotalMeasure As String = "TotalMeasure"
    Protected Const Col4CancelMeasure As String = "CancelMeasure"
    Protected Const Col4MeasureUnit As String = "MeasureUnit"
    Protected Const Col4Process As String = "Process"
    Protected Const Col4ReceivedQty As String = "ReceivedQty"
    Protected Const Col4ReceivedMeasure As String = "ReceivedMeasure"


    Public mJobWithMaterialYN As ClsMain.JobWithMaterialYN = ClsMain.JobWithMaterialYN.NA
    Protected mPrevProcess$ = "", mLastOrderBy$ = "", mJobProcess$ = ""
    Protected WithEvents TxtRate As AgControls.AgTextBox
    Protected WithEvents LblRate As System.Windows.Forms.Label
    

    Dim mJobOrderType As ClsMain.JobOrderType
    Dim mAffectStockProcess As Boolean = False
    Dim mAutoCreateProdOrder As Boolean = False

    Public Class HelpDataSet
        Public Shared Item As DataSet = Nothing
        Public Shared JobWorker As DataSet = Nothing
        Public Shared JobOrderFor As DataSet = Nothing
        Public Shared ProdPlan As DataSet = Nothing
        Public Shared ProdOrder As DataSet = Nothing
        Public Shared InsideOutside As DataSet = Nothing
        Public Shared Process As DataSet = Nothing
        Public Shared BillingType As DataSet = Nothing
        Public Shared OrderBy As DataSet = Nothing
        Public Shared Bom As DataSet = Nothing
        Public Shared Godown As DataSet = Nothing
        Public Shared ItemFromProdOrder As DataSet = Nothing
        Public Shared JobOrder As DataSet = Nothing
        Public Shared ItemFromJobOrder As DataSet = Nothing
        Public Shared JobRate As DataSet = Nothing
        Public Shared JobWorkerRateGroup As DataSet = Nothing
    End Class

    Public Property AffectStockProcess() As Boolean
        Get
            AffectStockProcess = mAffectStockProcess
        End Get
        Set(ByVal value As Boolean)
            mAffectStockProcess = value
        End Set
    End Property

    Public Property AutoCreateProdOrder() As Boolean
        Get
            AutoCreateProdOrder = mAutoCreateProdOrder
        End Get
        Set(ByVal value As Boolean)
            mAutoCreateProdOrder = value
        End Set
    End Property

    Public Property JobOrderType() As ClsMain.JobOrderType
        Get
            JobOrderType = mJobOrderType
        End Get
        Set(ByVal value As ClsMain.JobOrderType)
            mJobOrderType = value
        End Set
    End Property

    Public Property JobWithMaterialYN() As ClsMain.JobWithMaterialYN
        Get
            JobWithMaterialYN = mJobWithMaterialYN
        End Get
        Set(ByVal value As ClsMain.JobWithMaterialYN)
            mJobWithMaterialYN = value
        End Set
    End Property

    Public Property JobProcess() As String
        Get
            JobProcess = mJobProcess
        End Get
        Set(ByVal value As String)
            mJobProcess = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.BtnFill = New System.Windows.Forms.Button
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.LblTotalBomMeasure = New System.Windows.Forms.Label
        Me.LblTotalConsumptionMeasureText = New System.Windows.Forms.Label
        Me.LblTotalBomQty = New System.Windows.Forms.Label
        Me.LblTotalConsumptionQtyText = New System.Windows.Forms.Label
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.TxtJobOrderFor = New AgControls.AgTextBox
        Me.LblJobOrderFor = New System.Windows.Forms.Label
        Me.TxtDueDate = New AgControls.AgTextBox
        Me.LblDueDate = New System.Windows.Forms.Label
        Me.TxtTermsAndConditions = New AgControls.AgTextBox
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.LblJobInstructions = New System.Windows.Forms.LinkLabel
        Me.TxtInsideOutside = New AgControls.AgTextBox
        Me.LblInsideOutside = New System.Windows.Forms.Label
        Me.TxtBillingType = New AgControls.AgTextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.TxtOrderBy = New AgControls.AgTextBox
        Me.LblOrderBy = New System.Windows.Forms.Label
        Me.LblOrderByReq = New System.Windows.Forms.Label
        Me.LblDueDateReq = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.LblTotalByProductMeasure = New System.Windows.Forms.Label
        Me.LblTotalByProductMeasureText = New System.Windows.Forms.Label
        Me.LblTotalByProductQty = New System.Windows.Forms.Label
        Me.LblTotalByProductQtyText = New System.Windows.Forms.Label
        Me.LblJobByProduct = New System.Windows.Forms.LinkLabel
        Me.Pnl4 = New System.Windows.Forms.Panel
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.BtnFillJobOrderDetail = New System.Windows.Forms.Button
        Me.TxtJobOrderNo = New AgControls.AgTextBox
        Me.LblJobOrderNo = New System.Windows.Forms.Label
        Me.LblWithMaterialYN = New System.Windows.Forms.Label
        Me.TxtWithMaterialYN = New AgControls.AgTextBox
        Me.TxtRate = New AgControls.AgTextBox
        Me.LblRate = New System.Windows.Forms.Label
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
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(829, 585)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(648, 585)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(467, 585)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Tag = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(168, 585)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 585)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 581)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(320, 585)
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
        Me.LblV_No.Location = New System.Drawing.Point(243, 28)
        Me.LblV_No.Size = New System.Drawing.Size(88, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Job Order No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(365, 27)
        Me.TxtV_No.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(121, 33)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(10, 28)
        Me.LblV_Date.Size = New System.Drawing.Size(95, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Job Order Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(349, 13)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(137, 27)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(243, 9)
        Me.LblV_Type.Size = New System.Drawing.Size(96, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Job Order Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(365, 7)
        Me.TxtV_Type.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(121, 13)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(10, 9)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(137, 7)
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
        Me.LblPrefix.Location = New System.Drawing.Point(303, 28)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 19)
        Me.TabControl1.Size = New System.Drawing.Size(991, 115)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtRate)
        Me.TP1.Controls.Add(Me.LblRate)
        Me.TP1.Controls.Add(Me.LblWithMaterialYN)
        Me.TP1.Controls.Add(Me.TxtWithMaterialYN)
        Me.TP1.Controls.Add(Me.TxtJobOrderNo)
        Me.TP1.Controls.Add(Me.LblJobOrderNo)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Controls.Add(Me.LblDueDateReq)
        Me.TP1.Controls.Add(Me.TxtOrderBy)
        Me.TP1.Controls.Add(Me.LblOrderBy)
        Me.TP1.Controls.Add(Me.LblOrderByReq)
        Me.TP1.Controls.Add(Me.TxtInsideOutside)
        Me.TP1.Controls.Add(Me.LblInsideOutside)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.Label32)
        Me.TP1.Controls.Add(Me.TxtBillingType)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtDueDate)
        Me.TP1.Controls.Add(Me.LblDueDate)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtJobOrderFor)
        Me.TP1.Controls.Add(Me.LblJobOrderFor)
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(983, 89)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobOrderFor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobOrderFor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDueDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDueDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillingType, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label32, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblInsideOutside, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtInsideOutside, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblOrderByReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblOrderBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtOrderBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDueDateReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtWithMaterialYN, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblWithMaterialYN, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblRate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRate, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 4
        '
        'Dgl1
        '
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
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgMandatory = False
        Me.TxtManualRefNo.AgMasterHelp = False
        Me.TxtManualRefNo.AgNumberLeftPlaces = 8
        Me.TxtManualRefNo.AgNumberNegetiveAllow = False
        Me.TxtManualRefNo.AgNumberRightPlaces = 2
        Me.TxtManualRefNo.AgPickFromLastValue = False
        Me.TxtManualRefNo.AgRowFilter = ""
        Me.TxtManualRefNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualRefNo.AgSelectedValue = Nothing
        Me.TxtManualRefNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualRefNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualRefNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualRefNo.Location = New System.Drawing.Point(137, 47)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(100, 18)
        Me.TxtManualRefNo.TabIndex = 4
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(10, 47)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(101, 16)
        Me.LblManualRefNo.TabIndex = 706
        Me.LblManualRefNo.Text = "Manual Ref. No."
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(4, 277)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(972, 21)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(747, 2)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 672
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(638, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 16)
        Me.Label1.TabIndex = 671
        Me.Label1.Text = "Total Amount :"
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
        Me.Label33.Size = New System.Drawing.Size(106, 16)
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
        Me.LblTotalQtyText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalQtyText.TabIndex = 667
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 154)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 123)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(729, 69)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(823, 68)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(149, 18)
        Me.TxtRemarks.TabIndex = 11
        '
        'BtnFill
        '
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.Location = New System.Drawing.Point(460, 299)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(53, 21)
        Me.BtnFill.TabIndex = 2
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = True
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(5, 320)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(508, 120)
        Me.Pnl2.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel2.Controls.Add(Me.LblTotalBomMeasure)
        Me.Panel2.Controls.Add(Me.LblTotalConsumptionMeasureText)
        Me.Panel2.Controls.Add(Me.LblTotalBomQty)
        Me.Panel2.Controls.Add(Me.LblTotalConsumptionQtyText)
        Me.Panel2.Location = New System.Drawing.Point(5, 440)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(508, 22)
        Me.Panel2.TabIndex = 696
        '
        'LblTotalBomMeasure
        '
        Me.LblTotalBomMeasure.AutoSize = True
        Me.LblTotalBomMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBomMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBomMeasure.Location = New System.Drawing.Point(311, 3)
        Me.LblTotalBomMeasure.Name = "LblTotalBomMeasure"
        Me.LblTotalBomMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBomMeasure.TabIndex = 674
        Me.LblTotalBomMeasure.Text = "."
        Me.LblTotalBomMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalConsumptionMeasureText
        '
        Me.LblTotalConsumptionMeasureText.AutoSize = True
        Me.LblTotalConsumptionMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalConsumptionMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalConsumptionMeasureText.Location = New System.Drawing.Point(200, 3)
        Me.LblTotalConsumptionMeasureText.Name = "LblTotalConsumptionMeasureText"
        Me.LblTotalConsumptionMeasureText.Size = New System.Drawing.Size(106, 16)
        Me.LblTotalConsumptionMeasureText.TabIndex = 673
        Me.LblTotalConsumptionMeasureText.Text = "Total Measure :"
        '
        'LblTotalBomQty
        '
        Me.LblTotalBomQty.AutoSize = True
        Me.LblTotalBomQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBomQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBomQty.Location = New System.Drawing.Point(93, 3)
        Me.LblTotalBomQty.Name = "LblTotalBomQty"
        Me.LblTotalBomQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBomQty.TabIndex = 672
        Me.LblTotalBomQty.Text = "."
        Me.LblTotalBomQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalConsumptionQtyText
        '
        Me.LblTotalConsumptionQtyText.AutoSize = True
        Me.LblTotalConsumptionQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalConsumptionQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalConsumptionQtyText.Location = New System.Drawing.Point(8, 3)
        Me.LblTotalConsumptionQtyText.Name = "LblTotalConsumptionQtyText"
        Me.LblTotalConsumptionQtyText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalConsumptionQtyText.TabIndex = 671
        Me.LblTotalConsumptionQtyText.Text = "Total Qty :"
        '
        'LinkLabel5
        '
        Me.LinkLabel5.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel5.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel5.LinkColor = System.Drawing.Color.White
        Me.LinkLabel5.Location = New System.Drawing.Point(4, 301)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(229, 17)
        Me.LinkLabel5.TabIndex = 730
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Text = "Consumption Detail For Above Items"
        Me.LinkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.LinkLabel1.Size = New System.Drawing.Size(190, 17)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Job Order For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.AutoSize = True
        Me.LblJobWorkerReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobWorkerReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(608, 34)
        Me.LblJobWorkerReq.Name = "LblJobWorkerReq"
        Me.LblJobWorkerReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobWorkerReq.TabIndex = 732
        Me.LblJobWorkerReq.Text = "Ä"
        '
        'TxtJobWorker
        '
        Me.TxtJobWorker.AgMandatory = True
        Me.TxtJobWorker.AgMasterHelp = False
        Me.TxtJobWorker.AgNumberLeftPlaces = 8
        Me.TxtJobWorker.AgNumberNegetiveAllow = False
        Me.TxtJobWorker.AgNumberRightPlaces = 2
        Me.TxtJobWorker.AgPickFromLastValue = False
        Me.TxtJobWorker.AgRowFilter = ""
        Me.TxtJobWorker.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobWorker.AgSelectedValue = Nothing
        Me.TxtJobWorker.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobWorker.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobWorker.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobWorker.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobWorker.Location = New System.Drawing.Point(622, 28)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(105, 18)
        Me.TxtJobWorker.TabIndex = 9
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(525, 28)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(74, 16)
        Me.LblJobWorker.TabIndex = 731
        Me.LblJobWorker.Text = "Job Worker"
        '
        'TxtJobOrderFor
        '
        Me.TxtJobOrderFor.AgMandatory = False
        Me.TxtJobOrderFor.AgMasterHelp = False
        Me.TxtJobOrderFor.AgNumberLeftPlaces = 0
        Me.TxtJobOrderFor.AgNumberNegetiveAllow = False
        Me.TxtJobOrderFor.AgNumberRightPlaces = 0
        Me.TxtJobOrderFor.AgPickFromLastValue = False
        Me.TxtJobOrderFor.AgRowFilter = ""
        Me.TxtJobOrderFor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobOrderFor.AgSelectedValue = Nothing
        Me.TxtJobOrderFor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobOrderFor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobOrderFor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobOrderFor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobOrderFor.Location = New System.Drawing.Point(622, 48)
        Me.TxtJobOrderFor.MaxLength = 0
        Me.TxtJobOrderFor.Name = "TxtJobOrderFor"
        Me.TxtJobOrderFor.Size = New System.Drawing.Size(105, 18)
        Me.TxtJobOrderFor.TabIndex = 10
        '
        'LblJobOrderFor
        '
        Me.LblJobOrderFor.AutoSize = True
        Me.LblJobOrderFor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobOrderFor.Location = New System.Drawing.Point(525, 49)
        Me.LblJobOrderFor.Name = "LblJobOrderFor"
        Me.LblJobOrderFor.Size = New System.Drawing.Size(87, 16)
        Me.LblJobOrderFor.TabIndex = 734
        Me.LblJobOrderFor.Text = "Job Order For"
        '
        'TxtDueDate
        '
        Me.TxtDueDate.AgMandatory = True
        Me.TxtDueDate.AgMasterHelp = False
        Me.TxtDueDate.AgNumberLeftPlaces = 0
        Me.TxtDueDate.AgNumberNegetiveAllow = False
        Me.TxtDueDate.AgNumberRightPlaces = 0
        Me.TxtDueDate.AgPickFromLastValue = False
        Me.TxtDueDate.AgRowFilter = ""
        Me.TxtDueDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDueDate.AgSelectedValue = Nothing
        Me.TxtDueDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDueDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDueDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDueDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDueDate.Location = New System.Drawing.Point(365, 67)
        Me.TxtDueDate.MaxLength = 0
        Me.TxtDueDate.Name = "TxtDueDate"
        Me.TxtDueDate.Size = New System.Drawing.Size(149, 18)
        Me.TxtDueDate.TabIndex = 7
        '
        'LblDueDate
        '
        Me.LblDueDate.AutoSize = True
        Me.LblDueDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDueDate.Location = New System.Drawing.Point(243, 68)
        Me.LblDueDate.Name = "LblDueDate"
        Me.LblDueDate.Size = New System.Drawing.Size(62, 16)
        Me.LblDueDate.TabIndex = 736
        Me.LblDueDate.Text = "Due Date"
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
        Me.TxtTermsAndConditions.Location = New System.Drawing.Point(7, 485)
        Me.TxtTermsAndConditions.MaxLength = 255
        Me.TxtTermsAndConditions.Multiline = True
        Me.TxtTermsAndConditions.Name = "TxtTermsAndConditions"
        Me.TxtTermsAndConditions.Size = New System.Drawing.Size(307, 90)
        Me.TxtTermsAndConditions.TabIndex = 3
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(637, 465)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(339, 111)
        Me.PnlCalcGrid.TabIndex = 736
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
        Me.TxtStructure.Location = New System.Drawing.Point(643, 172)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(43, 18)
        Me.TxtStructure.TabIndex = 746
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(569, 175)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 747
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'LinkLabel2
        '
        Me.LinkLabel2.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Location = New System.Drawing.Point(4, 465)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(162, 17)
        Me.LinkLabel2.TabIndex = 748
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Job Terms && Conditions"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(320, 485)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(298, 91)
        Me.Pnl3.TabIndex = 749
        '
        'LblJobInstructions
        '
        Me.LblJobInstructions.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobInstructions.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobInstructions.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobInstructions.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobInstructions.LinkColor = System.Drawing.Color.White
        Me.LblJobInstructions.Location = New System.Drawing.Point(317, 467)
        Me.LblJobInstructions.Name = "LblJobInstructions"
        Me.LblJobInstructions.Size = New System.Drawing.Size(114, 17)
        Me.LblJobInstructions.TabIndex = 750
        Me.LblJobInstructions.TabStop = True
        Me.LblJobInstructions.Text = "Job Instructions"
        Me.LblJobInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtInsideOutside
        '
        Me.TxtInsideOutside.AgMandatory = False
        Me.TxtInsideOutside.AgMasterHelp = False
        Me.TxtInsideOutside.AgNumberLeftPlaces = 8
        Me.TxtInsideOutside.AgNumberNegetiveAllow = False
        Me.TxtInsideOutside.AgNumberRightPlaces = 2
        Me.TxtInsideOutside.AgPickFromLastValue = False
        Me.TxtInsideOutside.AgRowFilter = ""
        Me.TxtInsideOutside.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtInsideOutside.AgSelectedValue = Nothing
        Me.TxtInsideOutside.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtInsideOutside.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtInsideOutside.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtInsideOutside.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInsideOutside.Location = New System.Drawing.Point(365, 47)
        Me.TxtInsideOutside.MaxLength = 50
        Me.TxtInsideOutside.Name = "TxtInsideOutside"
        Me.TxtInsideOutside.Size = New System.Drawing.Size(149, 18)
        Me.TxtInsideOutside.TabIndex = 5
        '
        'LblInsideOutside
        '
        Me.LblInsideOutside.AutoSize = True
        Me.LblInsideOutside.BackColor = System.Drawing.Color.Transparent
        Me.LblInsideOutside.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInsideOutside.Location = New System.Drawing.Point(243, 47)
        Me.LblInsideOutside.Name = "LblInsideOutside"
        Me.LblInsideOutside.Size = New System.Drawing.Size(91, 16)
        Me.LblInsideOutside.TabIndex = 749
        Me.LblInsideOutside.Text = "Inside/Outside"
        '
        'TxtBillingType
        '
        Me.TxtBillingType.AgMandatory = False
        Me.TxtBillingType.AgMasterHelp = False
        Me.TxtBillingType.AgNumberLeftPlaces = 0
        Me.TxtBillingType.AgNumberNegetiveAllow = False
        Me.TxtBillingType.AgNumberRightPlaces = 0
        Me.TxtBillingType.AgPickFromLastValue = False
        Me.TxtBillingType.AgRowFilter = ""
        Me.TxtBillingType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillingType.AgSelectedValue = Nothing
        Me.TxtBillingType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillingType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillingType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillingType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillingType.Location = New System.Drawing.Point(137, 67)
        Me.TxtBillingType.MaxLength = 20
        Me.TxtBillingType.Name = "TxtBillingType"
        Me.TxtBillingType.Size = New System.Drawing.Size(101, 18)
        Me.TxtBillingType.TabIndex = 6
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(10, 67)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(64, 16)
        Me.Label32.TabIndex = 729
        Me.Label32.Text = "Billing On"
        '
        'TxtOrderBy
        '
        Me.TxtOrderBy.AgMandatory = True
        Me.TxtOrderBy.AgMasterHelp = False
        Me.TxtOrderBy.AgNumberLeftPlaces = 8
        Me.TxtOrderBy.AgNumberNegetiveAllow = False
        Me.TxtOrderBy.AgNumberRightPlaces = 2
        Me.TxtOrderBy.AgPickFromLastValue = False
        Me.TxtOrderBy.AgRowFilter = ""
        Me.TxtOrderBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOrderBy.AgSelectedValue = Nothing
        Me.TxtOrderBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOrderBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOrderBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOrderBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOrderBy.Location = New System.Drawing.Point(622, 8)
        Me.TxtOrderBy.MaxLength = 20
        Me.TxtOrderBy.Name = "TxtOrderBy"
        Me.TxtOrderBy.Size = New System.Drawing.Size(350, 18)
        Me.TxtOrderBy.TabIndex = 8
        '
        'LblOrderBy
        '
        Me.LblOrderBy.AutoSize = True
        Me.LblOrderBy.BackColor = System.Drawing.Color.Transparent
        Me.LblOrderBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOrderBy.Location = New System.Drawing.Point(525, 8)
        Me.LblOrderBy.Name = "LblOrderBy"
        Me.LblOrderBy.Size = New System.Drawing.Size(60, 16)
        Me.LblOrderBy.TabIndex = 751
        Me.LblOrderBy.Text = "Order By"
        '
        'LblOrderByReq
        '
        Me.LblOrderByReq.AutoSize = True
        Me.LblOrderByReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblOrderByReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblOrderByReq.Location = New System.Drawing.Point(608, 14)
        Me.LblOrderByReq.Name = "LblOrderByReq"
        Me.LblOrderByReq.Size = New System.Drawing.Size(10, 7)
        Me.LblOrderByReq.TabIndex = 752
        Me.LblOrderByReq.Text = "Ä"
        '
        'LblDueDateReq
        '
        Me.LblDueDateReq.AutoSize = True
        Me.LblDueDateReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDueDateReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDueDateReq.Location = New System.Drawing.Point(349, 74)
        Me.LblDueDateReq.Name = "LblDueDateReq"
        Me.LblDueDateReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDueDateReq.TabIndex = 753
        Me.LblDueDateReq.Text = "Ä"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel3.Controls.Add(Me.LblTotalByProductMeasure)
        Me.Panel3.Controls.Add(Me.LblTotalByProductMeasureText)
        Me.Panel3.Controls.Add(Me.LblTotalByProductQty)
        Me.Panel3.Controls.Add(Me.LblTotalByProductQtyText)
        Me.Panel3.Location = New System.Drawing.Point(519, 440)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(457, 22)
        Me.Panel3.TabIndex = 756
        '
        'LblTotalByProductMeasure
        '
        Me.LblTotalByProductMeasure.AutoSize = True
        Me.LblTotalByProductMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalByProductMeasure.Location = New System.Drawing.Point(310, 3)
        Me.LblTotalByProductMeasure.Name = "LblTotalByProductMeasure"
        Me.LblTotalByProductMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalByProductMeasure.TabIndex = 674
        Me.LblTotalByProductMeasure.Text = "."
        Me.LblTotalByProductMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalByProductMeasureText
        '
        Me.LblTotalByProductMeasureText.AutoSize = True
        Me.LblTotalByProductMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalByProductMeasureText.Location = New System.Drawing.Point(199, 3)
        Me.LblTotalByProductMeasureText.Name = "LblTotalByProductMeasureText"
        Me.LblTotalByProductMeasureText.Size = New System.Drawing.Size(106, 16)
        Me.LblTotalByProductMeasureText.TabIndex = 673
        Me.LblTotalByProductMeasureText.Text = "Total Measure :"
        '
        'LblTotalByProductQty
        '
        Me.LblTotalByProductQty.AutoSize = True
        Me.LblTotalByProductQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalByProductQty.Location = New System.Drawing.Point(93, 3)
        Me.LblTotalByProductQty.Name = "LblTotalByProductQty"
        Me.LblTotalByProductQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalByProductQty.TabIndex = 672
        Me.LblTotalByProductQty.Text = "."
        Me.LblTotalByProductQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalByProductQtyText
        '
        Me.LblTotalByProductQtyText.AutoSize = True
        Me.LblTotalByProductQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalByProductQtyText.Location = New System.Drawing.Point(8, 3)
        Me.LblTotalByProductQtyText.Name = "LblTotalByProductQtyText"
        Me.LblTotalByProductQtyText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalByProductQtyText.TabIndex = 671
        Me.LblTotalByProductQtyText.Text = "Total Qty :"
        '
        'LblJobByProduct
        '
        Me.LblJobByProduct.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobByProduct.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobByProduct.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobByProduct.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobByProduct.LinkColor = System.Drawing.Color.White
        Me.LblJobByProduct.Location = New System.Drawing.Point(519, 301)
        Me.LblJobByProduct.Name = "LblJobByProduct"
        Me.LblJobByProduct.Size = New System.Drawing.Size(114, 18)
        Me.LblJobByProduct.TabIndex = 755
        Me.LblJobByProduct.TabStop = True
        Me.LblJobByProduct.Text = "Job By Product"
        Me.LblJobByProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pnl4
        '
        Me.Pnl4.Location = New System.Drawing.Point(519, 320)
        Me.Pnl4.Name = "Pnl4"
        Me.Pnl4.Size = New System.Drawing.Size(457, 120)
        Me.Pnl4.TabIndex = 754
        '
        'TxtGodown
        '
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
        Me.TxtGodown.Location = New System.Drawing.Point(823, 48)
        Me.TxtGodown.MaxLength = 255
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(149, 18)
        Me.TxtGodown.TabIndex = 756
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(727, 48)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 757
        Me.LblGodown.Text = "Godown"
        '
        'BtnFillJobOrderDetail
        '
        Me.BtnFillJobOrderDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillJobOrderDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillJobOrderDetail.Location = New System.Drawing.Point(923, 139)
        Me.BtnFillJobOrderDetail.Name = "BtnFillJobOrderDetail"
        Me.BtnFillJobOrderDetail.Size = New System.Drawing.Size(53, 17)
        Me.BtnFillJobOrderDetail.TabIndex = 757
        Me.BtnFillJobOrderDetail.Text = "Fill"
        Me.BtnFillJobOrderDetail.UseVisualStyleBackColor = True
        '
        'TxtJobOrderNo
        '
        Me.TxtJobOrderNo.AgMandatory = False
        Me.TxtJobOrderNo.AgMasterHelp = False
        Me.TxtJobOrderNo.AgNumberLeftPlaces = 8
        Me.TxtJobOrderNo.AgNumberNegetiveAllow = False
        Me.TxtJobOrderNo.AgNumberRightPlaces = 2
        Me.TxtJobOrderNo.AgPickFromLastValue = False
        Me.TxtJobOrderNo.AgRowFilter = ""
        Me.TxtJobOrderNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobOrderNo.AgSelectedValue = Nothing
        Me.TxtJobOrderNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobOrderNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobOrderNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobOrderNo.Location = New System.Drawing.Point(823, 28)
        Me.TxtJobOrderNo.MaxLength = 50
        Me.TxtJobOrderNo.Name = "TxtJobOrderNo"
        Me.TxtJobOrderNo.Size = New System.Drawing.Size(149, 18)
        Me.TxtJobOrderNo.TabIndex = 758
        '
        'LblJobOrderNo
        '
        Me.LblJobOrderNo.AutoSize = True
        Me.LblJobOrderNo.BackColor = System.Drawing.Color.Transparent
        Me.LblJobOrderNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobOrderNo.Location = New System.Drawing.Point(729, 28)
        Me.LblJobOrderNo.Name = "LblJobOrderNo"
        Me.LblJobOrderNo.Size = New System.Drawing.Size(88, 16)
        Me.LblJobOrderNo.TabIndex = 759
        Me.LblJobOrderNo.Text = "Job Order No."
        '
        'LblWithMaterialYN
        '
        Me.LblWithMaterialYN.AutoSize = True
        Me.LblWithMaterialYN.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWithMaterialYN.Location = New System.Drawing.Point(296, 174)
        Me.LblWithMaterialYN.Name = "LblWithMaterialYN"
        Me.LblWithMaterialYN.Size = New System.Drawing.Size(111, 16)
        Me.LblWithMaterialYN.TabIndex = 761
        Me.LblWithMaterialYN.Text = "With Material Y/N"
        Me.LblWithMaterialYN.Visible = False
        '
        'TxtWithMaterialYN
        '
        Me.TxtWithMaterialYN.AgMandatory = False
        Me.TxtWithMaterialYN.AgMasterHelp = False
        Me.TxtWithMaterialYN.AgNumberLeftPlaces = 0
        Me.TxtWithMaterialYN.AgNumberNegetiveAllow = False
        Me.TxtWithMaterialYN.AgNumberRightPlaces = 0
        Me.TxtWithMaterialYN.AgPickFromLastValue = False
        Me.TxtWithMaterialYN.AgRowFilter = ""
        Me.TxtWithMaterialYN.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtWithMaterialYN.AgSelectedValue = Nothing
        Me.TxtWithMaterialYN.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtWithMaterialYN.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtWithMaterialYN.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtWithMaterialYN.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWithMaterialYN.Location = New System.Drawing.Point(413, 172)
        Me.TxtWithMaterialYN.MaxLength = 20
        Me.TxtWithMaterialYN.Name = "TxtWithMaterialYN"
        Me.TxtWithMaterialYN.Size = New System.Drawing.Size(48, 18)
        Me.TxtWithMaterialYN.TabIndex = 760
        Me.TxtWithMaterialYN.Visible = False
        '
        'TxtRate
        '
        Me.TxtRate.AgMandatory = False
        Me.TxtRate.AgMasterHelp = False
        Me.TxtRate.AgNumberLeftPlaces = 0
        Me.TxtRate.AgNumberNegetiveAllow = False
        Me.TxtRate.AgNumberRightPlaces = 0
        Me.TxtRate.AgPickFromLastValue = False
        Me.TxtRate.AgRowFilter = ""
        Me.TxtRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRate.AgSelectedValue = Nothing
        Me.TxtRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(622, 68)
        Me.TxtRate.MaxLength = 0
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(105, 18)
        Me.TxtRate.TabIndex = 762
        Me.TxtRate.Text = "TxtRate"
        Me.TxtRate.Visible = False
        '
        'LblRate
        '
        Me.LblRate.AutoSize = True
        Me.LblRate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRate.Location = New System.Drawing.Point(525, 69)
        Me.LblRate.Name = "LblRate"
        Me.LblRate.Size = New System.Drawing.Size(35, 16)
        Me.LblRate.TabIndex = 763
        Me.LblRate.Text = "Rate"
        Me.LblRate.Visible = False
        '
        'TempJobOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 626)
        Me.Controls.Add(Me.BtnFillJobOrderDetail)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.LblJobByProduct)
        Me.Controls.Add(Me.Pnl4)
        Me.Controls.Add(Me.LblJobInstructions)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TxtTermsAndConditions)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.LinkLabel5)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.BtnFill)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Name = "TempJobOrder"
        Me.Text = "Template Job Order"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.BtnFill, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel5, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtTermsAndConditions, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel2, 0)
        Me.Controls.SetChildIndex(Me.Pnl3, 0)
        Me.Controls.SetChildIndex(Me.LblJobInstructions, 0)
        Me.Controls.SetChildIndex(Me.Pnl4, 0)
        Me.Controls.SetChildIndex(Me.LblJobByProduct, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.BtnFillJobOrderDetail, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
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
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents BtnFill As System.Windows.Forms.Button
    Protected WithEvents Pnl2 As System.Windows.Forms.Panel
    Protected WithEvents Panel2 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents TxtDueDate As AgControls.AgTextBox
    Protected WithEvents LblDueDate As System.Windows.Forms.Label
    Protected WithEvents TxtJobOrderFor As AgControls.AgTextBox
    Protected WithEvents LblJobOrderFor As System.Windows.Forms.Label
    Protected WithEvents TxtTermsAndConditions As AgControls.AgTextBox
    Protected WithEvents LblTotalBomMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalConsumptionMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalBomQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalConsumptionQtyText As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Protected WithEvents Pnl3 As System.Windows.Forms.Panel
    Protected WithEvents LblJobInstructions As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtInsideOutside As AgControls.AgTextBox
    Protected WithEvents LblInsideOutside As System.Windows.Forms.Label
    Protected WithEvents TxtBillingType As AgControls.AgTextBox
    Protected WithEvents Label32 As System.Windows.Forms.Label
    Protected WithEvents TxtOrderBy As AgControls.AgTextBox
    Protected WithEvents LblOrderBy As System.Windows.Forms.Label
    Protected WithEvents LblOrderByReq As System.Windows.Forms.Label
    Protected WithEvents LblDueDateReq As System.Windows.Forms.Label
    Protected WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalByProductMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductQtyText As System.Windows.Forms.Label
    Protected WithEvents LblJobByProduct As System.Windows.Forms.LinkLabel
    Protected WithEvents Pnl4 As System.Windows.Forms.Panel
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents BtnFillJobOrderDetail As System.Windows.Forms.Button
    Protected WithEvents TxtJobOrderNo As AgControls.AgTextBox
    Protected WithEvents LblJobOrderNo As System.Windows.Forms.Label
    Protected WithEvents LblWithMaterialYN As System.Windows.Forms.Label
    Protected WithEvents TxtWithMaterialYN As AgControls.AgTextBox
#End Region

    Private Sub TempJobOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        If mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
            Call ProcUpDateProdOrderForCancel(SearchCode, Conn, Cmd)
        End If
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobOrder"
        LogTableName = "JobOrder_Log"
        MainLineTableCsv = "JobOrderdetail,JobOrderBOM,JobOrderQCInstruction,Structure_TransFooter,Structure_TransLine,JobOrderByProduct"
        LogLineTableCsv = "JobOrderdetail_Log,JobOrderBOM_Log,JobOrderQCInstruction_Log,Structure_TransFooter_Log,Structure_TransLine_Log,JobOrderByProduct_Log"

        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl2)
        AgL.GridDesign(Dgl3)
        AgL.GridDesign(Dgl4)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
        AgCalcGrid1.AgIsMaster = True
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("M.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("M.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "M.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select M.DocID As SearchCode " & _
            " From JobOrder M " & _
            " Left Join Voucher_Type Vt On M.V_Type = Vt.V_Type  " & _
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By M.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("M.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("M.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "M.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select M.UID As SearchCode " & _
               " From JobOrder_Log M " & _
               " Left Join Voucher_Type Vt On M.V_Type = Vt.V_Type  " & _
               " Where M.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By M.EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " And IfNull(H.IsDeleted,0)=0 " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = "SELECT P.DocID, Vt.Description AS [Entry Type], P.V_Date AS [Entry Date], " & _
        '                    " P.V_No AS [Entry No], S.DispName As JobWorker, P.ManualRefNo, P.DueDate " & _
        '                    " FROM JobOrder P " & _
        '                    " LEFT JOIN Voucher_type Vt ON P.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup S On P.JobWorker = S.SubCode" & _
        '                    " Where 1=1  " & mCondStr

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Type AS [ORDER Type], H.V_Date AS [ORDER Date],  " & _
                    " H.ManualRefNo AS [Order No], H.DueDate AS [Due Date], H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure], H.TotalBomQty AS [Total Bom Qty], H.TotalBomMeasure AS [Total Bom Measure], " & _
                    " H.Remarks, H.JobInstructions AS [Job Instructions], H.TermsAndConditions AS [Terms And Conditions], H.LastIssueDate AS [LAST Issue Date], H.LastReceiveDate AS [LAST Receive Date],  " & _
                    " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date] , H.Status, H.JobOrderFor AS [Job Order For],  " & _
                    " H.totalAmount AS [total Amount], H.InsideOutside AS [Inside/Outside], H.JobWithMaterialYN AS [Job With Material], H.BillingType AS [Billing Type], " & _
                    " H.ClothWeight AS [Cloth Weight], D.Div_Name AS Division, SM.Name AS [Site Name], SGJ.DispName AS [Job Worker Name], SGO.DispName AS [ORDER BY],JO.ManualRefNo AS [Job ORDER Manual No],G.Description AS Godown, SF.*   " & _
                    " FROM JobOrder H " & _
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
                    " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " & _
                    " LEFT JOIN SubGroup SGJ ON SGJ.SubCode=H.JobWorker  " & _
                    " LEFT JOIN SubGroup SGO ON SGO.SubCode = H.OrderBy  " & _
                    " LEFT JOIN JobOrder  JO ON H.JobOrder  =JO.DocID  " & _
                    " LEFT JOIN Godown G ON G.Code = H.Godown   " & _
                    " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQueryFooter(AgL, EntryNCat, FrmType) & ") As SF On H.DocId = SF.DocId " & _
                    " Where 1=1  " & mCondStr

        AgL.PubFindQryOrdBy = "[Order Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT P.UID as SearchCode, P.DocId, Vt.Description AS [Entry Type], " & _
        '                    " P.V_Date AS [Entry Date], P.V_No AS [Entry No], S.DispName As JobWorker, " & _
        '                    " P.ManualRefNo, P.DueDate " & _
        '                    " FROM JobOrder_Log P " & _
        '                    " LEFT JOIN Voucher_Type Vt ON P.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup S On P.JobWorker = S.SubCode" & _
        '                    " Where P.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr
        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [ORDER Type], H.V_Prefix AS Prefix, H.V_Date AS [ORDER Date], H.V_No AS [ORDER No], " & _
                            " H.ManualRefNo AS [Manual No], H.DueDate AS [Due Date], H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure], H.TotalBomQty AS [Total Bom Qty], H.TotalBomMeasure AS [Total Bom Measure], " & _
                            " H.Remarks, H.JobInstructions AS [Job Instructions], H.TermsAndConditions AS [Terms And Conditions], H.LastIssueDate AS [LAST Issue Date], H.LastReceiveDate AS [LAST Receive Date],  " & _
                            " H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status],  " & _
                            " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date] , H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date],  H.Status, H.JobOrderFor AS [Job Order For],  " & _
                            " H.totalAmount AS [total Amount], H.Structure, H.InsideOutside AS [Inside/Outside], H.JobWithMaterialYN AS [Job With Material], H.BillingType AS [Billing Type], H.PurjaNo AS [Purja No],  " & _
                            " H.ClothWeight AS [Cloth Weight], D.Div_Name AS Division, SM.Name AS [Site Name], SGJ.DispName AS [Job Worker Name], SGO.DispName AS [ORDER BY],JO.ManualRefNo AS [Job ORDER Manual No],G.Description AS Godown, SF.*   " & _
                            " FROM JobOrder_Log H " & _
                            " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
                            " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " & _
                            " LEFT JOIN SubGroup SGJ ON SGJ.SubCode=H.JobWorker  " & _
                            " LEFT JOIN SubGroup SGO ON SGO.SubCode = H.OrderBy  " & _
                            " LEFT JOIN JobOrder  JO ON H.JobOrder  =JO.DocID  " & _
                            " LEFT JOIN Godown G ON G.Code = H.Godown   " & _
                            " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQueryFooter(AgL, EntryNCat, FrmType) & ") As SF On H.UID = SF.UId " & _
                            " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr
        AgL.PubFindQryOrdBy = "[Order Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1JobworkerRateGroup, 90, 0, Col1JobworkerRateGroup, False, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 190, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 100, 0, Col1FromProcess, False, False, False)
            .AddAgTextColumn(Dgl1, Col1CurrStock, 100, 0, Col1CurrStock, False, False, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 40, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 4, False, Col1TotalMeasure, True, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, True, True)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 110, 0, Col1ProdOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1ProdPlan, 110, 0, Col1ProdPlan, False, True)
            .AddAgNumberColumn(Dgl1, Col1LossPer, 70, 3, 4, False, Col1LossPer, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Loss, 70, 8, 4, False, Col1Loss, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReceiveQty, 70, 8, 4, False, Col1ReceiveQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1ReceiveMeasure, 70, 8, 4, False, Col1ReceiveMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1CancelQty, 70, 8, 4, False, Col1CancelQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1CancelMeasure, 70, 8, 4, False, Col1CancelMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReceiveLoss, 70, 8, 4, False, Col1ReceiveLoss, False, False, True)
            .AddAgTextColumn(Dgl1, Col1BOM, 100, 0, Col1BOM, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.Columns(Col1ProdOrder).CellTemplate.Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Underline)
        Dgl1.Columns(Col1ProdOrder).CellTemplate.Style.ForeColor = Color.Blue


        If Dgl1.Rows.Count > 0 Then
            Dgl1.Item(Col1ProdOrder, 0).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Underline)
            Dgl1.Item(Col1ProdOrder, 0).Style.ForeColor = Color.Blue
            Dgl1.Columns(Col1CurrStock).CellTemplate.Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Underline)
            Dgl1.Columns(Col1CurrStock).CellTemplate.Style.ForeColor = Color.Blue
        End If





        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2Item, 250, 0, Col2Item, True, False)
            .AddAgNumberColumn(Dgl2, Col2Qty, 100, 8, 4, False, Col2Qty, True, False, True)
            .AddAgTextColumn(Dgl2, Col2Unit, 100, 0, Col2Unit, True, True)
            .AddAgTextColumn(Dgl2, Col2PrevProcess, 100, 0, Col2PrevProcess, False, True)
            .AddAgNumberColumn(Dgl2, Col2MeasurePerPcs, 70, 8, 4, False, Col2MeasurePerPcs, False, True, True)
            .AddAgNumberColumn(Dgl2, Col2TotalMeasure, 70, 8, 4, False, Col2TotalMeasure, False, True, True)
            .AddAgTextColumn(Dgl2, Col2MeasureUnit, 70, 0, Col2MeasureUnit, False, True)
            .AddAgNumberColumn(Dgl2, Col2IssuedQty, 70, 8, 4, False, Col2IssuedQty, False, False, True)
            .AddAgNumberColumn(Dgl2, Col2IssuedMeasure, 70, 8, 4, False, Col2IssuedMeasure, False, True, True)
            .AddAgNumberColumn(Dgl2, Col2ReturnQty, 70, 8, 4, False, Col2ReturnQty, False, False, True)
            .AddAgNumberColumn(Dgl2, Col2ReturnMeasure, 70, 8, 4, False, Col2ReturnMeasure, False, True, True)
            .AddAgNumberColumn(Dgl2, Col2ConsumedQty, 70, 8, 4, False, Col2ConsumedQty, False, False, True)
            .AddAgNumberColumn(Dgl2, Col2ConsumedMeasure, 70, 8, 4, False, Col2ConsumedMeasure, False, True, True)
            .AddAgNumberColumn(Dgl2, Col2ActualConsumedQty, 100, 8, 4, False, Col2ActualConsumedQty, False, False, True)
            .AddAgNumberColumn(Dgl2, Col2ActualConsumedMeasure, 110, 8, 4, False, Col2ActualConsumedMeasure, False, True, True)
            .AddAgNumberColumn(Dgl2, Col2CancelQty, 70, 8, 4, False, Col2CancelQty, False, False, True)
            .AddAgNumberColumn(Dgl2, Col2CancelMeasure, 70, 8, 4, False, Col2CancelMeasure, False, True, True)

        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 25
        Dgl2.AllowUserToAddRows = False
        Dgl2.AgSkipReadOnlyColumns = True

        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl3, Col3Parameter, 180, 0, Col3Parameter, True, True)
            .AddAgTextColumn(Dgl3, Col3StdValue, 100, 0, Col3StdValue, True, False)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 20
        Dgl3.AllowUserToAddRows = False
        Dgl3.AgSkipReadOnlyColumns = True


        Dgl4.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl4, ColSNo, 40, 5, ColSNo, True, True)
            .AddAgTextColumn(Dgl4, Col4Item, 250, 5, Col4Item, True, False)
            .AddAgNumberColumn(Dgl4, Col4Qty, 70, 5, 4, False, Col4Qty, True, False)
            .AddAgNumberColumn(Dgl4, Col4CancelQty, 50, 5, 4, False, Col4CancelQty, False, False)
            .AddAgTextColumn(Dgl4, Col4Unit, 70, 5, Col4Unit, True, True)
            .AddAgNumberColumn(Dgl4, Col4MeasurePerPcs, 50, 5, 4, False, Col4MeasurePerPcs, False, True)
            .AddAgNumberColumn(Dgl4, Col4TotalMeasure, 50, 5, 4, False, Col4TotalMeasure, False, True)
            .AddAgNumberColumn(Dgl4, Col4CancelMeasure, 50, 5, 4, False, Col4CancelMeasure, False, True)
            .AddAgTextColumn(Dgl4, Col4MeasureUnit, 50, 5, Col4MeasureUnit, False, True)
            .AddAgTextColumn(Dgl4, Col4Process, 50, 5, Col4Process, False, False)
            .AddAgNumberColumn(Dgl4, Col4ReceivedQty, 50, 5, 4, False, Col4ReceivedQty, False, True)
            .AddAgNumberColumn(Dgl4, Col4ReceivedMeasure, 50, 5, 4, False, Col4ReceivedMeasure, False, True)
        End With
        AgL.AddAgDataGrid(Dgl4, Pnl4)
        Dgl4.EnableHeadersVisualStyles = False
        Dgl4.ColumnHeadersHeight = 18
        Dgl4.AgSkipReadOnlyColumns = True

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)
        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index

        FrmProductionOrder_BaseFunction_FIniList()



        'Dgl1.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl1.DefaultCellStyle.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl1.RowTemplate.Height = 18
        'Dgl1.ColumnHeadersHeight = 30

        'Dgl2.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl2.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl2.DefaultCellStyle.Font = New Font(Dgl2.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl2.RowTemplate.Height = 20
        'Dgl2.ColumnHeadersHeight = 18

        'Dgl3.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl3.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl3.DefaultCellStyle.Font = New Font(Dgl3.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl3.RowTemplate.Height = 20
        'Dgl3.ColumnHeadersHeight = 18

        'Dgl4.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl4.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl4.DefaultCellStyle.Font = New Font(Dgl4.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl4.RowTemplate.Height = 20
        'Dgl4.ColumnHeadersHeight = 18
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE JobOrder_Log " & _
                " SET " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " JobWorker = " & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " & _
                " OrderBy = " & AgL.Chk_Text(TxtOrderBy.AgSelectedValue) & ", " & _
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " & _
                " DueDate = " & AgL.ConvertDate(TxtDueDate.Text) & ", " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " Rate = " & Val(TxtRate.Text) & ", " & _
                " TotalAmount = " & Val(LblTotalAmount.Text) & ", " & _
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " & _
                " TotalBomQty = " & Val(LblTotalBomQty.Text) & ", " & _
                " TotalBomMeasure = " & Val(LblTotalBomMeasure.Text) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                " TermsAndConditions = " & AgL.Chk_Text(TxtTermsAndConditions.Text) & ", " & _
                " JobOrderFor = " & AgL.Chk_Text(TxtJobOrderFor.Text) & ",  " & _
                " BillingType = " & AgL.Chk_Text(TxtBillingType.Text) & ",  " & _
                " InsideOutside = " & AgL.Chk_Text(TxtInsideOutside.Text) & ",  " & _
                " JobWithMaterialYN = " & IIf(AgL.StrCmp(TxtWithMaterialYN.Text, "Yes"), 1, 0) & ", " & _
                " Godown = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & _
                " JobOrder = " & AgL.Chk_Text(TxtJobOrderNo.AgSelectedValue) & " " & _
                " Where UID = '" & mSearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        AgCalcGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)

        mQry = "Delete From JobOrderDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from JobIssRecUID_Log Where UID='" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)




        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mSr += 1
                    mQry = "  INSERT INTO JobOrderDetail_Log(UID, DocId, Sr, " & _
                            " JobWorkerRateGroup, Item, FromProcess, CurrStock, Qty, Unit, MeasurePerPcs, TotalMeasure, MeasureUnit, " & _
                            " ProdPlan, ProdOrder, LossPer, Loss, Rate, Amount, ReceiveQty, ReceiveMeasure, CancelQty, CancelMeasure, ReceiveLoss, Bom, JobOrder) " & _
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", 	" & _
                            " " & mSr & ", " & AgL.Chk_Text(.AgSelectedValue(Col1JobworkerRateGroup, I)) & ", " & AgL.Chk_Text(.AgSelectedValue(Col1Item, I)) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1FromProcess, I)) & ", " & _
                            " " & Val(.Item(Col1CurrStock, I).Value) & ", " & Val(.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(.Item(Col1Unit, I).Value) & ",	" & _
                            " " & Val(.Item(Col1MeasurePerPcs, I).Value) & ", " & Val(.Item(Col1TotalMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1MeasureUnit, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1ProdPlan, I)) & ",	" & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1ProdOrder, I)) & ",	" & _
                            " " & Val(.Item(Col1LossPer, I).Value) & "," & Val(.Item(Col1Loss, I).Value) & ", " & Val(.Item(Col1Rate, I).Value) & ",	" & _
                            " " & Val(.Item(Col1Amount, I).Value) & ", " & Val(.Item(Col1ReceiveQty, I).Value) & ", " & _
                            " " & Val(.Item(Col1ReceiveMeasure, I).Value) & ", " & Val(.Item(Col1CancelQty, I).Value) & ", " & Val(.Item(Col1CancelMeasure, I).Value) & ", " & Val(.Item(Col1ReceiveLoss, I).Value) & "," & AgL.Chk_Text(.AgSelectedValue(Col1BOM, I)) & ", " & AgL.Chk_Text(TxtJobOrderNo.Tag) & ") "

                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
                    AgCalcGrid1.Save_TransLine(mInternalCode, mSr, I, Conn, Cmd, SearchCode)
                End If
            Next
        End With






        mQry = "Delete From JobOrderBOM_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl2
            For I = 0 To .RowCount - 1
                If .Item(Col2Item, I).Value <> "" Then
                    mSr += 1
                    mQry = "INSERT INTO JobOrderBOM_Log(UID, DocId, Sr, " & _
                            " Item, Qty, Unit, MeasurePerPcs, TotalMeasure, " & _
                            " MeasureUnit, IssuedQty, IssuedMeasure, ReturnQty, ReturnMeasure, ConsumedQty, " & _
                            " ConsumedMeasure, ActualConsumedQty, ActualConsumedMeasure, CancelQty, CancelMeasure, PrevProcess) " & _
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " & mSr & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col2Item, I)) & ",	" & _
                            " " & Val(.Item(Col2Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col2Unit, I).Value) & ", " & _
                            " " & Val(.Item(Col2MeasurePerPcs, I).Value) & ", " & _
                            " " & Val(.Item(Col2TotalMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col2MeasureUnit, I).Value) & ", " & _
                            " " & Val(.Item(Col2IssuedQty, I).Value) & ", " & _
                            " " & Val(.Item(Col2IssuedMeasure, I).Value) & ", " & _
                            " " & Val(.Item(Col2ReturnQty, I).Value) & ",	" & _
                            " " & Val(.Item(Col2ReturnMeasure, I).Value) & ",	" & _
                            " " & Val(.Item(Col2ConsumedQty, I).Value) & ",	" & _
                            " " & Val(.Item(Col2ConsumedMeasure, I).Value) & ",	" & _
                            " " & Val(.Item(Col2ActualConsumedQty, I).Value) & ",	" & _
                            " " & Val(.Item(Col2ActualConsumedMeasure, I).Value) & ", " & _
                            " " & Val(.Item(Col2CancelQty, I).Value) & ", " & _
                            " " & Val(.Item(Col2CancelMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col2PrevProcess, I)) & " " & _
                            " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

        mQry = "Delete From JobOrderQCInstruction_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl3
            For I = 0 To .RowCount - 1
                If .Item(Col3Parameter, I).Value <> "" Then
                    mSr += 1
                    mQry = " INSERT INTO JobOrderQCInstruction_Log (DocId, " & _
                            " Sr, Parameter, StdValue, UID) " & _
                            " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3Parameter, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3StdValue, I).Value) & ", " & _
                            " '" & mSearchCode & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With


        mQry = " Delete From JobOrderByProduct_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl4
            For I = 0 To .Rows.Count - 1
                If .Item(Col4Item, I).Value <> "" Then
                    mSr += 1
                    mQry = " INSERT INTO JobOrderByProduct_Log (DocId, " & _
                            " Sr, " & _
                            " Item, " & _
                            " Qty, " & _
                            " CancelQty, " & _
                            " Unit, " & _
                            " MeasurePerPcs, " & _
                            " TotalMeasure, " & _
                            " CancelMeasure, " & _
                            " MeasureUnit, " & _
                            " Process, " & _
                            " ReceivedQty, " & _
                            " ReceivedMeasure, " & _
                            " UID) " & _
                            " VALUES (" & AgL.Chk_Text(mInternalCode) & ", " & _
                            " " & Val(mSr) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col4Item, I)) & ", " & _
                            " " & Val(.Item(Col4Qty, I).Value) & ", " & _
                            " " & Val(.Item(Col4CancelQty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col4Unit, I).Value) & ", " & _
                            " " & Val(.Item(Col4MeasurePerPcs, I).Value) & ", " & _
                            " " & Val(.Item(Col4TotalMeasure, I).Value) & ", " & _
                            " " & Val(.Item(Col4CancelMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col4MeasureUnit, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col4Process, I).Value) & ", " & _
                            " " & Val(.Item(Col4ReceivedQty, I).Value) & ", " & _
                            " " & Val(.Item(Col4ReceivedMeasure, I).Value) & ", " & _
                            " '" & mSearchCode & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With


        mLastOrderBy = TxtOrderBy.AgSelectedValue
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select P.* " & _
                " From JobOrder P " & _
                " Where P.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select P.* " & _
                " From JobOrder_Log P " & _
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

                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtJobWorker.AgSelectedValue = AgL.XNull(.Rows(0)("JobWorker"))
                TxtOrderBy.AgSelectedValue = AgL.XNull(.Rows(0)("OrderBy"))
                TxtDueDate.Text = AgL.XNull(.Rows(0)("DueDate"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))
                TxtJobOrderFor.Text = AgL.XNull(.Rows(0)("JobOrderFor"))
                TxtBillingType.Text = AgL.XNull(.Rows(0)("BillingType"))
                TxtInsideOutside.Text = AgL.XNull(.Rows(0)("InsideOutside"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                TxtRate.Text = AgL.VNull(.Rows(0)("Rate"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                LblTotalBomQty.Text = AgL.VNull(.Rows(0)("TotalBomQty"))
                LblTotalBomMeasure.Text = AgL.VNull(.Rows(0)("TotalBomMeasure"))
                TxtGodown.AgSelectedValue = AgL.XNull(.Rows(0)("Godown"))
                TxtJobOrderNo.AgSelectedValue = AgL.XNull(.Rows(0)("JobOrder"))
                TxtWithMaterialYN.Text = IIf(AgL.VNull(.Rows(0)("JobWithMaterialYN")) = 0, "No", "Yes")
                Call AgCalcGrid1.MoveRec_TransFooter(SearchCode)

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from JobOrderDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from JobOrderDetail_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1JobworkerRateGroup, I) = AgL.XNull(.Rows(I)("JobWorkerRateGroup"))
                            Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.AgSelectedValue(Col1FromProcess, I) = AgL.XNull(.Rows(I)("FromProcess"))
                            Dgl1.Item(Col1CurrStock, I).Value = AgL.VNull(.Rows(I)("CurrStock"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.AgSelectedValue(Col1BOM, I) = AgL.XNull(.Rows(I)("BOM"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.AgSelectedValue(Col1ProdPlan, I) = AgL.XNull(.Rows(I)("ProdPlan"))
                            Dgl1.AgSelectedValue(Col1ProdOrder, I) = AgL.XNull(.Rows(I)("ProdOrder"))
                            Dgl1.Item(Col1LossPer, I).Value = Format(AgL.VNull(.Rows(I)("LossPer")), "0.".PadRight(CType(Dgl1.Columns(Col1LossPer), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1Loss, I).Value = Format(AgL.VNull(.Rows(I)("Loss")), "0.".PadRight(CType(Dgl1.Columns(Col1Loss), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1ReceiveQty, I).Value = AgL.VNull(.Rows(I)("ReceiveQty"))
                            Dgl1.Item(Col1ReceiveMeasure, I).Value = AgL.VNull(.Rows(I)("ReceiveMeasure"))
                            Dgl1.Item(Col1CancelQty, I).Value = AgL.VNull(.Rows(I)("CancelQty"))
                            Dgl1.Item(Col1CancelMeasure, I).Value = AgL.VNull(.Rows(I)("CancelMeasure"))
                            Dgl1.Item(Col1ReceiveLoss, I).Value = AgL.VNull(.Rows(I)("ReceiveLoss"))
                            Call AgCalcGrid1.MoveRec_TransLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                            RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If
                End With
                AgCalcGrid1.MoveRec_TransFooter(SearchCode)
                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from JobOrderBOM where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from JobOrderBOM_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl2.RowCount = 1
                    Dgl2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            Dgl2.Rows.Add()
                            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
                            Dgl2.AgSelectedValue(Col2Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl2.Item(Col2Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.000")
                            Dgl2.Item(Col2Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl2.AgSelectedValue(Col2PrevProcess, I) = AgL.XNull(.Rows(I)("PrevProcess"))
                            Dgl2.Item(Col2MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                            Dgl2.Item(Col2TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.000")
                            Dgl2.Item(Col2MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl2.Item(Col2IssuedQty, I).Value = Format(AgL.VNull(.Rows(I)("IssuedQty")), "0.000")
                            Dgl2.Item(Col2IssuedMeasure, I).Value = Format(AgL.VNull(.Rows(I)("IssuedMeasure")), "0.000")
                            Dgl2.Item(Col2ReturnQty, I).Value = Format(AgL.VNull(.Rows(I)("ReturnQty")), "0.000")
                            Dgl2.Item(Col2ReturnMeasure, I).Value = Format(AgL.VNull(.Rows(I)("ReturnMeasure")), "0.000")
                            Dgl2.Item(Col2ConsumedQty, I).Value = Format(AgL.VNull(.Rows(I)("ConsumedQty")), "0.000")
                            Dgl2.Item(Col2ConsumedMeasure, I).Value = Format(AgL.VNull(.Rows(I)("ConsumedMeasure")), "0.000")
                            Dgl2.Item(Col2ActualConsumedQty, I).Value = Format(AgL.VNull(.Rows(I)("ActualConsumedQty")), "0.000")
                            Dgl2.Item(Col2ActualConsumedMeasure, I).Value = Format(AgL.VNull(.Rows(I)("ActualConsumedMeasure")), "0.000")
                            Dgl2.Item(Col2CancelQty, I).Value = Format(AgL.VNull(.Rows(I)("CancelQty")), "0.000")
                            Dgl2.Item(Col2CancelMeasure, I).Value = Format(AgL.VNull(.Rows(I)("CancelMeasure")), "0.000")

                        Next I
                    End If
                End With


                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from JobOrderQCInstruction where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from JobOrderQCInstruction_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl3.RowCount = 1
                    Dgl3.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            Dgl3.Rows.Add()
                            Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count
                            Dgl3.Item(Col3Parameter, I).Value = AgL.XNull(.Rows(I)("Parameter"))
                            Dgl3.Item(Col3StdValue, I).Value = AgL.XNull(.Rows(I)("StdValue"))
                        Next I
                    End If
                End With

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from JobOrderByProduct where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from JobOrderByProduct_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl4.RowCount = 1
                    Dgl4.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl4.Rows.Add()
                            Dgl4.Item(ColSNo, I).Value = Dgl4.Rows.Count - 1
                            Dgl4.AgSelectedValue(Col4Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl4.Item(Col4Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl4.Item(Col4CancelQty, I).Value = AgL.VNull(.Rows(I)("CancelQty"))
                            Dgl4.Item(Col4Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl4.Item(Col4MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl4.Item(Col4TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                            Dgl4.Item(Col4CancelMeasure, I).Value = AgL.VNull(.Rows(I)("CancelMeasure"))
                            Dgl4.Item(Col4MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl4.Item(Col4Process, I).Value = AgL.XNull(.Rows(I)("Process"))
                            Dgl4.Item(Col4ReceivedQty, I).Value = AgL.VNull(.Rows(I)("ReceivedQty"))
                            Dgl4.Item(Col4ReceivedMeasure, I).Value = AgL.VNull(.Rows(I)("ReceivedMeasure"))
                        Next I
                    End If
                End With


                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        Topctrl1.ChangeAgGridState(Dgl2, False)
        Topctrl1.ChangeAgGridState(Dgl3, False)
        Topctrl1.ChangeAgGridState(Dgl4, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        Call IniItemList()
        'Dgl1.AgHelpDataSet(Col1Item, 8) = HelpDataSet.Item
        Dgl2.AgHelpDataSet(Col2Item, 8) = HelpDataSet.Item
        Dgl4.AgHelpDataSet(Col4Item, 9) = HelpDataSet.Item
        Dgl1.AgHelpDataSet(Col1BOM, 8) = HelpDataSet.Bom
        TxtJobWorker.AgHelpDataSet(5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobWorker
        TxtJobOrderFor.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobOrderFor
        Dgl1.AgHelpDataSet(Col1ProdPlan, 3) = HelpDataSet.ProdPlan
        Dgl1.AgHelpDataSet(Col1ProdOrder, 5) = HelpDataSet.ProdOrder
        Dgl1.AgHelpDataSet(Col1FromProcess) = HelpDataSet.Process
        TxtInsideOutside.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.InsideOutside
        TxtOrderBy.AgHelpDataSet(2, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.OrderBy
        Dgl2.AgHelpDataSet(Col2PrevProcess, 3) = HelpDataSet.Process
        TxtBillingType.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.BillingType
        TxtGodown.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Godown
        TxtJobOrderNo.AgHelpDataSet(5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobOrder
        Dgl1.AgHelpDataSet(Col1JobworkerRateGroup, 2) = HelpDataSet.JobWorkerRateGroup
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim FrmObj As Form = Nothing
        Try

            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1CurrStock
                    FrmObj = New FrmLotWiseStock()
                    CType(FrmObj, FrmLotWiseStock).Item = Dgl1.AgSelectedValue(Col1Item, e.RowIndex)
                    CType(FrmObj, FrmLotWiseStock).ItemName = Dgl1.Item(Col1Item, e.RowIndex).Value
                    CType(FrmObj, FrmLotWiseStock).Qty = Val(Dgl1.Item(Col1CurrStock, e.RowIndex).Value)
                    CType(FrmObj, FrmLotWiseStock).V_Date = TxtV_Date.Text
                    FrmObj.ShowDialog()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                If mJobOrderType = ClsMain.JobOrderType.JobOrder Or mJobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Then
                    If AgL.StrCmp(TxtJobOrderFor.Text, ClsMain.JobOrderFor.Stock) Then
                        Call IniItemList()
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0  " & _
                            " AND Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                            " And Status <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                    Else
                        Call IniItemList(False)
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 " & _
                            " AND Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                            " AND Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " & _
                            " And Status <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " And PendingQty > 0 "
                    End If
                ElseIf mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
                    Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 " & _
                            " And Status <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " And " & AgTemplate.ClsMain.RetDivFilterStr & " " & _
                            " And JobOrder = '" & TxtJobOrderNo.AgSelectedValue & "' " & _
                            " AND Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                            " And PendingQty > 0 "
                End If

            Case Col1JobworkerRateGroup
                Dgl1.AgRowFilter(Dgl1.Columns(Col1JobworkerRateGroup).Index) = " IsDeleted = 0  " & _
                            " And Status <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " And Process ='" & LblV_Type.Tag & "' "
        End Select
    End Sub

    Private Sub Dgl2_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
            Case Col2Item
                Dgl2.AgRowFilter(Dgl2.Columns(Col2Item).Index) = " IsDeleted = 0  " & _
                        " And Status <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
        End Select
    End Sub

    Private Sub Dgl2_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Calculation()
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl2.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub DGL4_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl4.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0 : LblTotalMeasure.Text = 0 : LblTotalAmount.Text = 0
        LblTotalBomMeasure.Text = 0 : LblTotalBomQty.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                If AgL.StrCmp(TxtBillingType.Text, "Qty") Or TxtBillingType.Text = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else : AgL.StrCmp(TxtBillingType.Text, "Area")
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next

        For I = 0 To Dgl2.RowCount - 1
            If Dgl2.Item(Col2Item, I).Value <> "" Then
                Dgl2.Item(Col2TotalMeasure, I).Value = Format(Val(Dgl2.Item(Col2Qty, I).Value) * Val(Dgl2.Item(Col2MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col2TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'Footer Calculation
                LblTotalBomQty.Text = Val(LblTotalBomQty.Text) + Val(Dgl2.Item(Col2Qty, I).Value)
                LblTotalBomMeasure.Text = Val(LblTotalBomMeasure.Text) + Val(Dgl2.Item(Col2TotalMeasure, I).Value)
            End If
        Next
        AgCalcGrid1.Calculation()
        LblTotalMeasure.Text = Val(LblTotalMeasure.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
        LblTotalBomMeasure.Text = Val(LblTotalBomMeasure.Text)


        LblTotalByProductQty.Text = 0 : LblTotalByProductMeasure.Text = 0

        For I = 0 To Dgl4.RowCount - 4
            If Dgl4.Item(Col4Item, I).Value <> "" Then
                Dgl4.Item(Col4TotalMeasure, I).Value = Format(Val(Dgl4.Item(Col4Qty, I).Value) * Val(Dgl4.Item(Col4MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col4TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'Footer Calculation
                LblTotalByProductQty.Text = Val(LblTotalByProductQty.Text) + Val(Dgl4.Item(Col4Qty, I).Value)
                LblTotalByProductMeasure.Text = Val(LblTotalByProductMeasure.Text) + Val(Dgl4.Item(Col4TotalMeasure, I).Value)
            End If
        Next
        LblTotalByProductQty.Text = Val(LblTotalByProductQty.Text)
        LblTotalByProductMeasure.Text = Val(LblTotalByProductMeasure.Text)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim mCurrStock As Double
        Dim StrMessage As String = ""

        passed = FCheckDuplicateRefNo()

        If AgL.RequiredField(TxtJobWorker, LblJobWorker.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtDueDate, LblDueDate.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        'If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1ProdOrder).Index.ToString + "") Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        If StrMessage <> "" Then StrMessage += vbCrLf
                        StrMessage += "Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & ""
                    End If
                    If StrMessage <> "" Then
                        MsgBox(StrMessage)
                        passed = False : Exit Sub
                    End If



                    StrMessage = ""
                    If JobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Then
                        If AgL.PubDtEnviro.Rows(0)("IsNegetiveStockAllowed") Then
                            mCurrStock = ClsMain.FunRetStock(.AgSelectedValue(Col1Item, I), mInternalCode, , TxtGodown.AgSelectedValue, .AgSelectedValue(Col1FromProcess, I), ClsMain.StockStatus.Standard, TxtV_Date.Text)
                            If mCurrStock < Val(.Item(Col1Qty, I).Value) Then
                                If StrMessage <> "" Then StrMessage += vbCrLf
                                StrMessage += "Qty of " & .Item(Col1Item, I).Value & " In " & TxtGodown.Text & " is less than " & Dgl1.Item(Col1Qty, I).Value & vbCrLf & " Current Stock Is : " & mCurrStock & "."
                            End If
                        End If
                    End If

                    If StrMessage <> "" Then
                        If MsgBox(StrMessage & vbCrLf & "Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With


        StrMessage = ""
        With Dgl2
            For I = 0 To .Rows.Count - 1
                If .Item(Col2Item, I).Value <> "" Then
                    If Val(.Item(Col2Qty, I).Value) = 0 Then
                        StrMessage += "Qty Is 0 At Row No " & Dgl2.Item(ColSNo, I).Value & ""
                    End If
                End If
            Next
        End With
        If StrMessage <> "" Then
            MsgBox(StrMessage)
            passed = False : Exit Sub
        End If

    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM JobOrder_Log WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " & _
                        " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then TxtManualRefNo.Text = ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, ClsMain.ManualRefType.Max) : MsgBox("Reference No. Already Exists New Reference No. Alloted : " & TxtManualRefNo.Text)

            'mQry = " SELECT COUNT(*) FROM JobOrder WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " & _
            '     " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0  "
            'If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM JobOrder_Log WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " & _
                " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And EntryStatus ='" & ClsMain.LogStatus.LogOpen & "' AND DocID <>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()

            mQry = " SELECT COUNT(*) FROM JobOrder WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'  " & _
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function


    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()
        Dgl4.RowCount = 1 : Dgl4.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0 : LblTotalAmount.Text = 0
        LblTotalBomMeasure.Text = 0 : LblTotalBomQty.Text = 0
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, _
                TxtManualRefNo.Validating, TxtV_Date.Validating, TxtJobOrderNo.Validating, TxtJobWorker.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            Select Case sender.name
                Case TxtV_Date.Name
                    If TxtV_Date.Text <> "" And TxtDueDate.Text = "" And AgL.PubDtEnviro.Rows.Count > 0 Then
                        TxtDueDate.Text = DateAdd(DateInterval.Day, AgL.VNull(AgL.PubDtEnviro.Rows(0)("DefaultDueDays")), CDate(TxtV_Date.Text))
                    End If

                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, ClsMain.ManualRefType.Max)
                    End If

                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    IniGrid()
                    TxtTermsAndConditions.Text = ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, ClsMain.ManualRefType.Max)
                    End If
                Case TxtJobOrderNo.Name
                    If TxtJobOrderNo.AgSelectedValue <> "" Then
                        Call ProcFillOrderForCancel(TxtJobOrderNo.AgSelectedValue)
                    End If

                Case TxtManualRefNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()

                Case TxtJobWorker.Name
                    If TxtJobWorker.AgSelectedValue <> "" Then
                        mQry = "Select IfNull(H.JobWithMaterialYN,0) As JobWithMaterialYN, H.InsideOutside " & _
                                " From JobWorker H " & _
                                " Where H.SubCode = '" & TxtJobWorker.AgSelectedValue & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        With DtTemp
                            If .Rows.Count > 0 Then
                                If AgL.XNull(.Rows(0)("InsideOutside")) <> "" Then TxtInsideOutside.Text = AgL.XNull(.Rows(0)("InsideOutside"))
                                TxtWithMaterialYN.Text = IIf(AgL.VNull(.Rows(0)("JobWithMaterialYN")) = 0, "No", "Yes")
                            End If
                        End With
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtManualRefNo.Enter, TxtJobOrderNo.Enter, TxtJobWorker.Enter, TxtGodown.Enter, TxtOrderBy.Enter
        Try
            Select Case sender.name
                Case TxtGodown.Name
                    TxtGodown.AgRowFilter = " IsDeleted = 0 " & _
                                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                            " And " & ClsMain.RetDivFilterStr & " " & _
                                            " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'"
                Case TxtJobOrderNo.Name
                    TxtJobOrderNo.AgRowFilter = " IsDeleted = 0 " & _
                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " And " & AgTemplate.ClsMain.RetDivFilterStr & " " & _
                            " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'" & _
                            " And NCat = '" & mJobProcess & "' "

                Case TxtJobWorker.Name
                    TxtJobWorker.AgRowFilter = " IsDeleted = 0 " & _
                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " AND Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                            " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'" & _
                            " And IfNull(Process,'') = '" & mJobProcess & "' "

                Case TxtOrderBy.Name
                    TxtOrderBy.AgRowFilter = " Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " & _
                                            " And " & ClsMain.RetDivFilterStr & " "


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempProductionOrder_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
            TxtJobWorker.Enabled = False
            TxtOrderBy.Enabled = False
            TxtRemarks.Enabled = False
            TxtTermsAndConditions.Enabled = False
            TxtBillingType.Enabled = False
            TxtDueDate.Enabled = False
            TxtInsideOutside.Enabled = False
            TxtJobOrderFor.Enabled = False
            TxtGodown.Enabled = False
            Dgl3.Visible = False
        End If
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.Item(Col1ProdPlan, mRow).Value = ""
                Dgl1.Item(Col1ProdOrder, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    'DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroup").Value)
                    Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCategory").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    If Not AgL.StrCmp(TxtJobOrderFor.Text, "Stock") Then
                        Dgl1.AgSelectedValue(Col1ProdOrder, mRow) = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                        Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("PendingQty").Value)
                        Dgl1.Item(Col1TotalMeasure, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("PendingMeasure").Value)
                    Else
                        Dgl1.AgSelectedValue(Col1ProdOrder, mRow) = ""
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Protected Sub Validating_Material(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl2.Item(Col2Item, mRow).Value.ToString.Trim = "" Or Dgl2.AgSelectedValue(Col2Item, mRow).ToString.Trim = "" Then
                Dgl2.Item(Col2Unit, mRow).Value = ""
                Dgl2.AgSelectedValue(Col2PrevProcess, mRow) = ""
                Dgl2.Item(Col2MeasurePerPcs, mRow).Value = 0
                Dgl2.Item(Col2MeasureUnit, mRow).Value = ""
            Else
                If Dgl2.AgHelpDataSet(Col2Item) IsNot Nothing Then
                    DrTemp = Dgl2.AgHelpDataSet(Col2Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl2.Item(Col2Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl2.AgSelectedValue(Col2PrevProcess, mRow) = mPrevProcess
                    Dgl2.Item(Col2MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("Measure"))
                    Dgl2.Item(Col2MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub Validating_ByProductItem(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl4.Item(Col4Item, mRow).Value.ToString.Trim = "" Or Dgl4.AgSelectedValue(Col4Item, mRow).ToString.Trim = "" Then
                Dgl4.Item(Col4Unit, mRow).Value = ""
                Dgl4.Item(Col4MeasurePerPcs, mRow).Value = 0
                Dgl4.Item(Col4MeasureUnit, mRow).Value = ""
            Else
                If Dgl4.AgDataRow IsNot Nothing Then
                    Dgl4.Item(Col4Unit, mRow).Value = AgL.XNull(Dgl4.AgDataRow.Cells("Unit").Value)
                    Dgl4.Item(Col4MeasurePerPcs, mRow).Value = AgL.VNull(Dgl4.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl4.Item(Col4MeasureUnit, mRow).Value = AgL.XNull(Dgl4.AgDataRow.Cells("MeasureUnit").Value)
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
                    If mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
                        Validating_JobOrderItemForCancel(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mRowIndex)
                    ElseIf mJobOrderType = ClsMain.JobOrderType.JobOrder Or mJobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Then
                        Validating_Item(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mRowIndex)
                    End If
                    If mJobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Then
                        Dgl1.AgSelectedValue(Col1FromProcess, mRowIndex) = mPrevProcess
                        Dgl1.Item(Col1CurrStock, mRowIndex).Value = ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mInternalCode, , TxtGodown.AgSelectedValue, Dgl1.AgSelectedValue(Col1FromProcess, mRowIndex), ClsMain.StockStatus.Standard, TxtV_Date.Text)
                    End If
                Case Col1FromProcess
                    If mJobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Then
                        Dgl1.Item(Col1CurrStock, mRowIndex).Value = ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mInternalCode, , TxtGodown.AgSelectedValue, Dgl1.AgSelectedValue(Col1FromProcess, mRowIndex), ClsMain.StockStatus.Standard, TxtV_Date.Text)
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl4_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl4.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl4.CurrentCell.RowIndex
            mColumnIndex = Dgl4.CurrentCell.ColumnIndex
            If Dgl4.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl4.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl4.Columns(Dgl4.CurrentCell.ColumnIndex).Name
                Case Col4Item
                    Validating_ByProductItem(Dgl4.AgSelectedValue(Col4Item, mRowIndex), mRowIndex)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempJobOrder_BaseEvent_Approve_PreTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Approve_PreTrans
        If mJobOrderType = ClsMain.JobOrderType.JobOrder Then
            mQry = " SELECT H.DocID, L.Item, L.Qty, L.TotalMeasure, L.ProdOrder " & _
                    " FROM JobOrder H  " & _
                    " LEFT JOIN JobOrderDetail L  ON H.DocID=L.DocId  " & _
                    " WHERE H.DocID = " & AgL.Chk_Text(mInternalCode) & " "
            DsMain = AgL.FillData(mQry, AgL.GcnRead)
        ElseIf mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
            mQry = " SELECT H.DocID, H.JobOrder As JobOrder, L.Item,L.Qty, L.TotalMeasure ,L.ProdOrder " & _
                    " FROM JobOrder H  " & _
                    " LEFT JOIN JobOrderDetail L  ON H.DocID = L.DocId  " & _
                    " WHERE H.DocID = " & AgL.Chk_Text(mInternalCode) & " "
            DsMain = AgL.FillData(mQry, AgL.GcnRead)
        End If
    End Sub

    Private Sub TempJobOrder_BaseEvent_ApproveDeletion_PreTrans(ByVal SearchCode As String) Handles Me.BaseEvent_ApproveDeletion_PreTrans
        If mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
            mQry = " SELECT H.DocID, H.JobOrder As JobOrder, L.Item,L.Qty, L.TotalMeasure ,L.ProdOrder " & _
                   " FROM JobOrder H  " & _
                   " LEFT JOIN JobOrderDetail L  ON H.DocID = L.DocId  " & _
                   " WHERE H.DocID = " & AgL.Chk_Text(mInternalCode) & " "
            DsMain = AgL.FillData(mQry, AgL.GcnRead)
        ElseIf mJobOrderType = ClsMain.JobOrderType.JobOrder Or mJobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Then
            mQry = " SELECT H.DocID, L.Item,L.Qty, L.TotalMeasure ,L.ProdOrder " & _
            " FROM JobOrder H  " & _
            " LEFT JOIN JobOrderDetail L  ON H.DocID=L.DocId  " & _
            " WHERE H.DocID = " & AgL.Chk_Text(mInternalCode) & " "
            DsMain = AgL.FillData(mQry, AgL.GcnRead)

        End If
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim Stock As ClsMain.StructStock = Nothing, StockProcess As ClsMain.StructStock = Nothing
        Dim mSr As Integer, I As Integer

        If mAffectStockProcess Then
            mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    mSr += 1

                    With StockProcess
                        .UID = mSearchCode
                        .DocID = mInternalCode
                        .Sr = mSr
                        .V_Type = TxtV_Type.AgSelectedValue
                        .V_Prefix = LblPrefix.Text
                        .V_Date = TxtV_Date.Text
                        .V_No = TxtV_No.Text
                        .RecID = TxtManualRefNo.Text
                        .Div_Code = TxtDivision.AgSelectedValue
                        .Site_Code = TxtSite_Code.AgSelectedValue
                        .SubCode = TxtJobWorker.AgSelectedValue
                        .Item = Dgl1.AgSelectedValue(Col1Item, I)
                        .Godown = TxtGodown.AgSelectedValue
                        .Qty_Rec = Dgl1.Item(Col1Qty, I).Value
                        .Unit = Dgl1.Item(Col1Unit, I).Value
                        .MeasurePerPcs = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value)
                        .Measure_Rec = Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                        .MeasureUnit = Dgl1.Item(Col1MeasureUnit, I).Value
                        .Status = ClsMain.StockStatus.Standard
                        .Process = LblV_Type.Tag
                    End With
                    Call ClsMain.ProcStockPost("StockProcess", StockProcess, Conn, Cmd)
                End If
            Next
        End If

        If mJobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Then
            mQry = "Delete From JobIssueDetail Where DocID = '" & mInternalCode & "'  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Delete From JobIssRec Where DocID = '" & mInternalCode & "'  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " INSERT INTO JobIssRec(DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, " & _
                   " ManualRefNo, Process, JobWorker, Godown, DueDate, IssQty, IssMeasure,  " & _
                   " Remarks, Structure, EntryBy, EntryDate, 	EntryType, " & _
                   " EntryStatus, ApproveBy,	ApproveDate,	MoveToLog,MoveToLogDate, " & _
                   " IsDeleted,	Status,	UID,	JobOrder,	BillingType,	OrderBy) " & _
                   " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H. " & _
                   " ManualRefNo, Vt.NCat , H.JobWorker, H.Godown, H.DueDate, H.TotalQty, H.TotalMeasure, H. " & _
                   " Remarks, H.Structure, H.EntryBy, H.EntryDate, H.EntryType, H. " & _
                   " EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, H. " & _
                   " IsDeleted, H.Status, H.UID , H.DocID , H.BillingType, H.OrderBy " & _
                   " FROM JobOrder H " & _
                   " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                   " Where H.DocId = '" & mInternalCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " INSERT INTO JobIssueDetail (DocId, Sr, Item, Qty, Unit, MeasurePerPcs, " & _
                    " TotalMeasure, MeasureUnit, JobOrder, UID) " & _
                    " SELECT L.DocId, L.Sr, L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.TotalMeasure,  " & _
                    " L.MeasureUnit, L.DocId, L.UID " & _
                    " FROM JobOrderDetail L " & _
                    " Where L.DocId = '" & mInternalCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            mQry = "Delete From Stock Where DocId = '" & mInternalCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    mSr += 1
                    With Stock
                        .UID = mSearchCode
                        .DocID = mInternalCode
                        .Sr = mSr
                        .V_Type = TxtV_Type.AgSelectedValue
                        .V_Prefix = LblPrefix.Text
                        .V_Date = TxtV_Date.Text
                        .V_No = TxtV_No.Text
                        .RecID = TxtManualRefNo.Text
                        .Div_Code = TxtDivision.AgSelectedValue
                        .Site_Code = TxtSite_Code.AgSelectedValue
                        .SubCode = TxtJobWorker.AgSelectedValue
                        .Item = Dgl1.AgSelectedValue(Col1Item, I)
                        .Process = Dgl1.AgSelectedValue(Col1FromProcess, I)
                        .Godown = TxtGodown.AgSelectedValue
                        .Qty_Iss = Dgl1.Item(Col1Qty, I).Value
                        .Unit = Dgl1.Item(Col1Unit, I).Value
                        .MeasurePerPcs = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value)
                        .Measure_Iss = Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                        .MeasureUnit = Dgl1.Item(Col1MeasureUnit, I).Value
                        .Status = ClsMain.StockStatus.Standard
                    End With
                    Call ClsMain.ProcStockPost("Stock", Stock, Conn, Cmd)

                End If
            Next

        End If


        If mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
            Call ProcUpDateProdOrderForCancel(SearchCode, Conn, Cmd)
        End If

        If mJobOrderType = ClsMain.JobOrderType.JobOrder Or mJobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Or mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
            Call ProcUpDateProdOrder(SearchCode, Conn, Cmd)
        End If

        ProcUpDateJobOrder(SearchCode, Conn, Cmd)
    End Sub

    Private Sub ProcUpDateProdOrder(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)
        Dim I As Integer = 0
        If DsMain Is Nothing Then Exit Sub
        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsMain.Tables(0).Rows.Count - 1
                    mQry = " UPDATE MaterialPlanDetail " & _
                            " SET JobOrderQty = (SELECT IfNull(Sum(L.Qty),0)-IfNull(Sum(L.CancelQty),0)  " & _
                            " 	                 FROM JobOrderDetail L   " & _
                            "                    Left Join JobOrder H   On L.DocID = H.DocID " & _
                            "                    Left Join Voucher_Type V   On H.V_Type = V.V_Type " & _
                            " 	                 WHERE L.ProdOrder = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
                            " 	                 AND Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
                            "                    And V.NCat Not In (Select ProcessCancelNCat from Process    Where ProcessCancelNCat is not Null)), " & _
                            " JobOrderMeasure = (SELECT IfNull(Sum(L.TotalMeasure),0)-IfNull(Sum(L.CancelMeasure),0) " & _
                            " 	                 FROM JobOrderDetail L    " & _
                            "                    Left Join JobOrder H   On L.DocID = H.DocID " & _
                            "                    Left Join Voucher_Type V   On H.V_Type = V.V_Type " & _
                            " 	                 WHERE L.ProdOrder =  '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
                            " 	                 AND Item = '" & AgL.XNull(.Rows(I)("Item")) & "'  " & _
                            "                    And V.NCat Not In (Select ProcessCancelNCat from Process    Where ProcessCancelNCat is not Null) ) " & _
                            " FROM MaterialPlan " & _
                            " WHERE MaterialPlan.DocId = MaterialPlanDetail.DocId   " & _
                            " And MaterialPlan.ProdOrder =  '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
                            " And Item =  '" & AgL.XNull(.Rows(I)("Item")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Next
            End If
        End With

        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mQry = " UPDATE MaterialPlanDetail " & _
                            " SET JobOrderQty = (SELECT IfNull(Sum(L.Qty),0)-IfNull(Sum(L.CancelQty),0)  " & _
                            " 	                 FROM JobOrderDetail L  " & _
                            "                    Left Join JobOrder H   On L.DocID = H.DocID " & _
                            "                    Left Join Voucher_Type V   On H.V_Type = V.V_Type " & _
                            " 	                 WHERE L.ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                            " 	                 AND Item = '" & .AgSelectedValue(Col1Item, I) & "' And V.NCat Not In (Select ProcessCancelNCat from Process    Where ProcessCancelNCat is not Null)) , " & _
                            " JobOrderMeasure = (SELECT IfNull(Sum(L.TotalMeasure),0)-IfNull(Sum(L.CancelMeasure),0) " & _
                            " 	                 FROM JobOrderDetail L  " & _
                            "                    Left Join JobOrder H   On L.DocID = H.DocID " & _
                            "                    Left Join Voucher_Type V   On H.V_Type = V.V_Type " & _
                            " 	                 WHERE L.ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                            " 	                 AND Item = '" & .AgSelectedValue(Col1Item, I) & "' And V.NCat Not In (Select ProcessCancelNCat from Process    Where ProcessCancelNCat is not Null)) " & _
                            " FROM MaterialPlan " & _
                            " WHERE MaterialPlan.DocId = MaterialPlanDetail.DocId   " & _
                            " And MaterialPlan.ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                            " And Item =  '" & .AgSelectedValue(Col1Item, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Sub


    Private Sub ProcUpDateJobOrder(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)
        Dim I As Integer = 0
        Dim DsMain As DataSet

        mQry = " SELECT L.DocId as JobOrder, L.Item,L.ProdOrder  " & _
        " FROM JobOrderDetail L  " & _
        " WHERE L.DocID = " & AgL.Chk_Text(mInternalCode) & " "
        DsMain = AgL.FillData(mQry, AgL.GcnRead)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsMain.Tables(0).Rows.Count - 1
                    mQry = " UPDATE JobOrderDetail " & _
                            " SET ReceiveQty = (SELECT IfNull(Sum(L.Qty),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE 1=1 " & _
                            "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' And IfNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' And IfNull(H.IsDeleted,0)=0), " & _
                            " ReceiveMeasure = (SELECT IfNull(Sum(L.TotalMeasure),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE 1=1 " & _
                            "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' And IfNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' And IfNull(H.IsDeleted,0)=0), " & _
                            " ReceiveLoss = (SELECT IfNull(Sum(L.LossQty),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE 1=1 " & _
                            "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' And IfNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' And IfNull(H.IsDeleted,0)=0) " & _
                            " Where DocId = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " And Item = '" & AgL.XNull(.Rows(I)("Item")) & "'  And IfNull(ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Next
            End If
        End With


    End Sub

    Private Sub TempJobOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
        Call ProcFillJobValues()
        Call ProcCheckForDefaultProperties()
        mPrevProcess = FunGetPrevProcess(mJobProcess)
        'TxtManualRefNo.Text = TxtV_No.Text.ToString
        TxtManualRefNo.Text = ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, ClsMain.ManualRefType.Max)
        TxtTermsAndConditions.Text = ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
        TxtOrderBy.AgSelectedValue = mLastOrderBy
    End Sub

    Private Sub ProcFillJobValues()
        Dim I As Integer
        Dim DtTemp As DataTable = Nothing
        Try
            mQry = " SELECT L.Parameter, L.StdValue  " & _
                    " FROM QcGroupDetail L  " & _
                    " LEFT JOIN QcGroup H ON L.Code = H.Code " & _
                    " Where L.Code = (SELECT P.QcGroup FROM Process P WHERE P.NCat = '" & mJobProcess & "') " & _
                    " And H.Div_Code = '" & AgL.PubDivCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl3.RowCount = 1
                Dgl3.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl3.Rows.Add()
                        Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count
                        Dgl3.Item(Col3Parameter, I).Value = AgL.XNull(.Rows(I)("Parameter"))
                        Dgl3.Item(Col3StdValue, I).Value = AgL.XNull(.Rows(I)("StdValue"))
                    Next
                End If
            End With

            mQry = " SELECT H.InsideOutside,  H.DefaultJobOrderFor, H.DefaultBillingType " & _
                    " FROM Process H " & _
                    " WHERE H.NCat = '" & mJobProcess & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                If .Rows.Count > 0 Then
                    TxtInsideOutside.Text = AgL.XNull(.Rows(0)("InsideOutside"))
                    TxtJobOrderFor.Text = AgL.XNull(.Rows(0)("DefaultJobOrderFor"))
                    TxtBillingType.Text = AgL.XNull(.Rows(0)("DefaultBillingType"))
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcCheckForDefaultProperties()
        Dim bMsgStr$ = ""
        Try
            If mJobWithMaterialYN = ClsMain.JobWithMaterialYN.NA Then
                bMsgStr &= "Set The Default property Named ""JobWithMaterialYN""." & vbCrLf
            End If
            If TxtInsideOutside.Text = "" Then
                bMsgStr &= "Set the Default value for ""Inside/Outside"" In Process Master." & vbCrLf
            End If
            If TxtJobOrderFor.Text = "" Then
                bMsgStr &= "Set the Default value for ""Job Order For"" In Process Master." & vbCrLf
            End If
            If TxtBillingType.Text = "" Then
                bMsgStr &= "Set the Default value for ""Billing Type"" In Process Master."
            End If
            If bMsgStr <> "" Then
                MsgBox(bMsgStr, MsgBoxStyle.Exclamation)
                Topctrl1.FButtonClick(14, True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl1.CellMouseMove
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1ProdOrder
                    Dgl1.Cursor = Cursors.Hand
                Case Else
                    Dgl1.Cursor = Cursors.Default
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TempJobOrder_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = "SELECT I.Code, I.Description, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " & _
                " IfNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code, I.Status, " & _
                " I.Measure As Measure,  I.MeasureUnit,  I.Measure  AS MeasurePerPcs, I.ItemGroup, I.ItemCategory " & _
                " FROM Item I  "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT H.Code, H.Description,  " & _
                " IfNull(H.IsDeleted ,0) AS IsDeleted, H.Div_Code, H.Status " & _
                " FROM BOM H   "
        HelpDataSet.Bom = AgL.FillData(mQry, AgL.GCn)



        'mQry = "SELECT S.SubCode AS Code, S.DispName  AS JobWorker " & _
        '        " FROM JobWorker J " & _
        '        " LEFT JOIN SubGroup S ON J.SubCode = S.SubCode "

        mQry = " SELECT J.SubCode AS Code, Sg.Name AS JobWorker, H.Process, " & _
                " IfNull(Sg.IsDeleted,0) AS IsDeleted,  SG.Div_Code, SG.Site_Code, " & _
                " IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
                " FROM JobWorker J   " & _
                " LEFT JOIN JobWorkerProcess H   On J.SubCode = H.SubCode  " & _
                " LEFT JOIN SubGroup Sg   ON J.SubCode = Sg.SubCode "
        HelpDataSet.JobWorker = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT '" & ClsMain.JobOrderFor.ProductionOrder & "' As Code, '" & ClsMain.JobOrderFor.ProductionOrder & "' As JobOrderFor " & _
                " UNION ALL " & _
                " SELECT '" & ClsMain.JobOrderFor.Stock & "'  As Code, '" & ClsMain.JobOrderFor.Stock & "'  As JobOrderFor  "
        HelpDataSet.JobOrderFor = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Pp.DocId AS Code,  " & _
                " Vt.Description + '-' + Convert(NVARCHAR,Pp.V_No) AS ProductionPlanNo   " & _
                " FROM MaterialPlan  Pp   " & _
                " LEFT JOIN Voucher_Type Vt   On Pp.V_Type = Vt.V_Type "
        HelpDataSet.ProdPlan = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Po.DocId AS Code, " & _
                " ManualRefNo As [Prod.Order No Manual], PO.V_Type + '-' + Convert(nVarchar,Po.V_No) as [Prod.Order No.], " & _
                " PO.Div_Code, PO.Site_Code " & _
                " FROM ProdOrder Po   "
        HelpDataSet.ProdOrder = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select '" & ClsMain.JobType.Inside & "' As Code, '" & ClsMain.JobType.Inside & "' As JobType   " & _
                " UNION ALL " & _
                " Select '" & ClsMain.JobType.Outside & "' As Code, '" & ClsMain.JobType.Outside & "' As JobType   "
        HelpDataSet.InsideOutside = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.NCat AS Code, L.NCatDescription AS Process " & _
                " FROM Process H   " & _
                " LEFT JOIN VoucherCat L   ON H.NCat = L.NCat "
        HelpDataSet.Process = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT L.SubCode AS Code, L.DispName AS OrderBy, " & _
                " L.Div_Code, L.Site_Code " & _
                " FROM Employee H   " & _
                " LEFT JOIN SubGroup L   ON H.SubCode = L.SubCode "
        HelpDataSet.OrderBy = AgL.FillData(mQry, AgL.GCn)

        HelpDataSet.BillingType = AgL.FillData(ClsMain.HelpQueries.BillingType, AgL.GCn)

        mQry = " SELECT H.Code, H.Description AS Godown, " & _
                " H.Div_Code, IfNull(H.IsDeleted,0) As IsDeleted, " & _
                " IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, " & _
                " H.Site_Code " & _
                " FROM Godown H    "
        HelpDataSet.Godown = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT I.Code, I.Description AS Item, Po.V_Date AS [Prod.Order Date], PO.ManualRefNo as [Prod.Order No.], Po.DueDate As [Prod Order Due Date], " & _
                " IfNull(L.UserMaterialPlanQty,0) - (IfNull(L.ProdOrdQty,0) + IfNull(L.JobOrderQty,0) + IfNull(L.PurchOrdQty,0)) AS PendingQty, " & _
                " I.Unit, I.ItemType, I.ItemGroup, I.ItemCategory, I.SalesTaxPostingGroup, " & _
                " IfNull(I.IsDeleted ,0) AS IsDeleted, H.ProdOrder,  H.Div_Code, H.Site_Code, " & _
                " IfNull(I.Status, '" & ClsMain.EntryStatus.Active & "' ) As Status,  " & _
                " L.MeasurePerPcs, I.MeasureUnit, " & _
                " IfNull(L.UserMaterialPlanMeasure,0) - (IfNull(L.ProdOrdMeasure,0)  + IfNull(L.JobOrderMeasure,0)  + IfNull(L.PurchOrdMeasure,0)) as PendingMeasure" & _
                " FROM (Select * from MaterialPlan    Where IfNull(IsDeleted,0)=0) H  " & _
                " LEFT JOIN MaterialPlanDetail L    ON L.DocId = H.DocID " & _
                " LEFT JOIN (Select * from Item    Where IfNull(IsDeleted,0)=0) I ON L.Item = I.Code " & _
                " LEFT JOIN (Select * from ProdOrder    Where IfNull(IsDeleted,0)=0) Po ON H.ProdOrder = Po.DocID " & _
                " WHERE H.ProdOrder Is Not NULL "
        HelpDataSet.ItemFromProdOrder = AgL.FillData(mQry, AgL.GCn)


        mQry = " SELECT I.Code, I.Description As Item, L.ProdOrder, IfNull(L.Qty,0) - IfNull(L.CancelQty,0) As PendingQty, " & _
                " L.Unit, L.MeasurePerPcs, I.ItemType, I.ItemGroup, I.ItemCategory, " & _
                " IfNull(L.TotalMeasure,0) - IfNull(L.CancelMeasure,0) As PendingMeasure , L.MeasureUnit, L.ProdPlan, " & _
                " L.LossPer, L.Loss, L.Rate, L.Incentive, " & _
                " L.Amount, L.BOM, L.DocId As JobOrder, " & _
                " IfNull(H.IsDeleted,0) As IsDeleted, H.Div_Code, H.Site_Code, " & _
                " IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
                " FROM JobOrderDetail L   " & _
                " LEFT JOIN Item I   On L.Item = I.Code " & _
                " LEFT JOIN JobOrder H   On L.DocId = H.DocId "
        HelpDataSet.ItemFromJobOrder = AgL.FillData(mQry, AgL.GcnRead)

        mQry = "SELECT J.DocId, J.ManualRefNo As JobOrderNo, " & _
                " IfNull(J.IsDeleted,0) As IsDeleted, J.Div_Code,Vt.NCat,  J.Site_Code,  " & _
                " IfNull(J.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
                " FROM JobOrder J   " & _
                " LEFT JOIN Voucher_Type Vt   ON J.V_Type = Vt.V_Type "
        HelpDataSet.JobOrder = AgL.FillData(mQry, AgL.GcnRead)

        mQry = "Select * from JobWorkerRateDetail  Where Process In ('" & EntryNCat & "')"
        HelpDataSet.JobRate = AgL.FillData(mQry, AgL.GcnRead)

        mQry = " SELECT H.Code AS Code, H.Description, H.Process, IfNull(H.IsDeleted,0) as IsDeleted, IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status FROM JobWorkerRateGroup H    "
        HelpDataSet.JobWorkerRateGroup = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Function FunGetPrevProcess(ByVal Process As String) As String
        Try
            mQry = " SELECT H.PrevProcess FROM Process H WHERE H.NCat = '" & Process & "' "
            FunGetPrevProcess = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            FunGetPrevProcess = ""
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        RaiseEvent BaseFunction_PostOrderGridFill()
        RaiseEvent BaseFunction_ConsumptionGridFill()
        RaiseEvent BaseFunction_PostConsumptionGridFill()
        Calculation()
    End Sub

    Private Sub TempJobOrder_BaseFunction_ConsumptionGridFill() Handles Me.BaseFunction_ConsumptionGridFill
        Dim I As Integer = 0
        Dim bQry$ = ""
        Dim DsTemp As DataSet = Nothing
        Dim bTempTable$ = ""
        Try
            bTempTable = AgL.GetGUID(AgL.GCn).ToString
            mQry = "CREATE TABLE [#" & bTempTable & "] " & _
                    " (Item NVARCHAR(10), TotalConsumptionQty Float)  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            With Dgl1
                If AgL.StrCmp(TxtBillingType.Text, "Qty") Or AgL.StrCmp(TxtBillingType.Text, "") Then
                    For I = 0 To .Rows.Count - 1
                        mQry = "INSERT INTO [#" & bTempTable & "] (Item,TotalConsumptionQty) " & _
                                " SELECT Bd.Item, Bd.Qty * " & Val(.Item(Col1Qty, I).Value) & " " & _
                                " FROM Bom B  " & _
                                " LEFT JOIN BomDetail Bd ON B.Code = Bd.Code " & _
                                " WHERE B.Code = '" & Dgl1.AgSelectedValue(Col1BOM, I) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    Next
                Else
                    For I = 0 To .Rows.Count - 1
                        mQry = "INSERT INTO [#" & bTempTable & "] (Item,TotalConsumptionQty) " & _
                                " SELECT Bd.Item, Bd.Qty * " & Val(.Item(Col1TotalMeasure, I).Value) & " " & _
                                " FROM Bom B  " & _
                                " LEFT JOIN BomDetail Bd ON B.Code = Bd.Code " & _
                                " WHERE B.Code = '" & Dgl1.AgSelectedValue(Col1BOM, I) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    Next
                End If
            End With


            mQry = "SELECT T.Item, Sum(IfNull(TotalConsumptionQty,0)) As BomQty " & _
                    " From [#" & bTempTable & "] T " & _
                    " Group By T.Item " & _
                    " HAVING Sum(IfNull(TotalConsumptionQty,0)) > 0 "

            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                Dgl2.RowCount = 1
                Dgl2.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        Dgl2.Rows.Add()
                        Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
                        Dgl2.AgSelectedValue(Col2Item, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl2.Item(Col2Qty, I).Value = Format(AgL.VNull(.Rows(I)("BomQty")), "0.000")
                        Validating_Material(Dgl2.AgSelectedValue(Col2Item, I), I)
                    Next I
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub IniItemList(Optional ByVal All_Records As Boolean = True)
        If mJobOrderType = ClsMain.JobOrderType.JobOrder_Cancel Then
            Dgl1.AgHelpDataSet(Col1Item, 15) = HelpDataSet.ItemFromJobOrder
        ElseIf mJobOrderType = ClsMain.JobOrderType.JobOrder Or mJobOrderType = ClsMain.JobOrderType.JobOrder_With_Issue Then
            If All_Records Then
                Dgl1.AgHelpDataSet(Col1Item, 8) = HelpDataSet.Item
            Else
                Dgl1.AgHelpDataSet(Col1Item, 10) = HelpDataSet.ItemFromProdOrder
            End If
        End If
    End Sub

    Private Sub ProcFillOrderForCancel(ByVal bOrderDocId As String)
        Dim DsTemp As DataSet
        Dim I As Integer = 0

        mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ManualRefNo, " & _
                " H.JobWorker, H.DueDate, H.TotalQty, H.TotalMeasure, H.TotalBomQty, H.TotalBomMeasure, H.Remarks, " & _
                " H.JobInstructions, H.TermsAndConditions, H.LastIssueDate, H.LastReceiveDate, H.EntryBy, H.EntryDate, " & _
                " H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, H.IsDeleted, H.Status, " & _
                " H.UID, H.JobOrderFor, H.totalAmount, H.Structure, H.OrderBy, H.InsideOutside, " & _
                " H.JobWithMaterialYN, H.BillingType, H.PurjaNo, H.JobOrder, H.Godown " & _
                " FROM JobOrder H " & _
                " WHERE H.DocID= '" & bOrderDocId & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtJobWorker.AgSelectedValue = AgL.XNull(.Rows(0)("JobWorker"))
                TxtOrderBy.AgSelectedValue = AgL.XNull(.Rows(0)("OrderBy"))
                TxtDueDate.Text = AgL.XNull(.Rows(0)("DueDate"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))
                TxtJobOrderFor.Text = AgL.XNull(.Rows(0)("JobOrderFor"))
                TxtBillingType.Text = AgL.XNull(.Rows(0)("BillingType"))
                TxtInsideOutside.Text = AgL.XNull(.Rows(0)("InsideOutside"))
                TxtGodown.AgSelectedValue = AgL.XNull(.Rows(0)("Godown"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                LblTotalBomQty.Text = AgL.VNull(.Rows(0)("TotalBomQty"))
                LblTotalBomMeasure.Text = AgL.VNull(.Rows(0)("TotalBomMeasure"))
            End If
        End With
    End Sub

    Private Sub ProcFillOrderDetailForCancel(ByVal bOrderDocId As String, Optional ByVal Item As String = "", _
                                    Optional ByVal ProdOrder As String = "", Optional ByVal RowIndex As Integer = 0)
        Dim I As Integer
        Dim DtTemp As DataTable = Nothing
        Dim bConStr$ = ""


        bConStr = " Where L.Qty - L.CancelQty > 0 "
        If bOrderDocId <> "" Then bConStr += " And L.DocId = '" & bOrderDocId & "' "
        If Item <> "" Then bConStr += " And L.Item = '" & Item & "'  "
        If ProdOrder <> "" Then bConStr += " And L.ProdOrder = '" & ProdOrder & "'  "

        mQry = " SELECT L.DocId, L.Sr, L.Item, L.Qty - L.CancelQty As PendingQty, L.Unit, L.MeasurePerPcs, " & _
                " L.TotalMeasure - L.CancelMeasure As PendingMeasure , L.MeasureUnit, L.ProdPlan, " & _
                " L.ProdOrder, L.LossPer, L.Loss, L.Rate, L.Incentive, " & _
                " L.Amount, L.BOM " & _
                " FROM JobOrderDetail L " & bConStr
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If Item = "" Then
            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("PendingQty")), "0.000")
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.0000")
                        Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("PendingMeasure"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.AgSelectedValue(Col1ProdPlan, I) = AgL.XNull(.Rows(I)("ProdPlan"))
                        Dgl1.AgSelectedValue(Col1ProdOrder, I) = AgL.XNull(.Rows(I)("ProdOrder"))
                        Dgl1.Item(Col1LossPer, I).Value = Format(AgL.VNull(.Rows(I)("LossPer")), "0.00")
                        Dgl1.Item(Col1Loss, I).Value = Format(AgL.VNull(.Rows(I)("Loss")), "0.00")
                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                        Dgl1.AgSelectedValue(Col1BOM, I) = AgL.XNull(.Rows(I)("BOM"))
                        AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("DocID")), Dgl1, I, AgL.VNull(.Rows(I)("Sr")))
                    Next I
                End If
            End With
        Else
            With DtTemp
                If .Rows.Count > 0 Then
                    Dgl1.Item(Col1Qty, RowIndex).Value = Format(AgL.VNull(.Rows(0)("PendingQty")), "0.000")
                    Dgl1.Item(Col1Unit, RowIndex).Value = AgL.XNull(.Rows(0)("Unit"))
                    Dgl1.Item(Col1MeasurePerPcs, RowIndex).Value = Format(AgL.VNull(.Rows(0)("MeasurePerPcs")), "0.0000")
                    Dgl1.Item(Col1TotalMeasure, RowIndex).Value = AgL.VNull(.Rows(0)("PendingMeasure"))
                    Dgl1.Item(Col1MeasureUnit, RowIndex).Value = AgL.XNull(.Rows(0)("MeasureUnit"))
                    Dgl1.AgSelectedValue(Col1ProdPlan, RowIndex) = AgL.XNull(.Rows(0)("ProdPlan"))
                    Dgl1.AgSelectedValue(Col1ProdOrder, RowIndex) = AgL.XNull(.Rows(0)("ProdOrder"))
                    Dgl1.Item(Col1LossPer, RowIndex).Value = Format(AgL.VNull(.Rows(0)("LossPer")), "0.00")
                    Dgl1.Item(Col1Loss, RowIndex).Value = Format(AgL.VNull(.Rows(0)("Loss")), "0.00")
                    Dgl1.Item(Col1Rate, RowIndex).Value = Format(AgL.VNull(.Rows(0)("Rate")), "0.00")
                    Dgl1.Item(Col1Amount, RowIndex).Value = Format(AgL.VNull(.Rows(0)("Amount")), "0.00")
                    Dgl1.AgSelectedValue(Col1BOM, RowIndex) = AgL.XNull(.Rows(0)("BOM"))
                    AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(0)("DocID")), Dgl1, I, AgL.VNull(.Rows(I)("Sr")))
                End If
            End With
        End If
        Calculation()
    End Sub

    Private Sub ProcUpDateProdOrderForCancel(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)
        Dim I As Integer = 0
        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsMain.Tables(0).Rows.Count - 1
                    mQry = " UPDATE JobOrderDetail " & _
                            " SET CancelQty = (SELECT IfNull(Sum(L.Qty),0)  " & _
                            " 	               FROM JobOrderDetail L  " & _
                            "                  LEFT JOIN JobOrder H On L.DocId = H.DocId " & _
                            " 	               WHERE H.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " 	               AND Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
                            "                  And ProdOrder = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
                            "                  And IfNull(H.IsDeleted,0) = 0), " & _
                            " CancelMeasure = (SELECT IfNull(Sum(L.TotalMeasure),0) " & _
                            " 	               FROM JobOrderDetail L  " & _
                            "                  LEFT JOIN JobOrder H On L.DocId = H.DocId " & _
                            " 	               WHERE H.JobOrder =  '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " 	               AND Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
                            "                  And ProdOrder = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
                            "                  And IfNull(H.IsDeleted,0) = 0 ) " & _
                            " WHERE DocId =  '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " And Item =  '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
                            " And IfNull(ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Next
            End If
        End With

        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mQry = " UPDATE JobOrderDetail " & _
                             " SET CancelQty = (SELECT IfNull(Sum(L.Qty),0)  " & _
                             " 	               FROM JobOrderDetail L  " & _
                             "                 LEFT JOIN JobOrder H On L.DocId = H.DocId " & _
                             " 	               WHERE H.JobOrder = '" & TxtJobOrderNo.AgSelectedValue & "' " & _
                             " 	               AND L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             "                 And L.ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                             "                 And IfNull(H.IsDeleted,0) = 0), " & _
                             " CancelMeasure = (SELECT IfNull(Sum(L.TotalMeasure),0) " & _
                             " 	               FROM JobOrderDetail L  " & _
                             "                 LEFT JOIN JobOrder H On L.DocId = H.DocId  " & _
                             " 	               WHERE H.JobOrder =  '" & TxtJobOrderNo.AgSelectedValue & "' " & _
                             " 	               AND L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             "                 And L.ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                             "                 And IfNull(H.IsDeleted,0) = 0 ) " & _
                             " WHERE DocId =  '" & TxtJobOrderNo.AgSelectedValue & "' " & _
                             " And Item =  '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             " And IfNull(ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

        With Dgl2
            For I = 0 To .RowCount - 1
                If .Item(Col2Item, I).Value <> "" Then
                    mQry = " UPDATE JobOrderBOM " & _
                             " SET CancelQty = (SELECT IfNull(Sum(L.Qty),0)  " & _
                             " 	               FROM JobOrderBOM L  " & _
                             "                 LEFT JOIN JobOrder H On L.DocId = H.DocId  " & _
                             " 	               WHERE H.JobOrder = '" & TxtJobOrderNo.AgSelectedValue & "' " & _
                             " 	               AND Item = '" & .AgSelectedValue(Col2Item, I) & "' " & _
                             "                 And IfNull(H.IsDeleted,0) = 0), " & _
                             " CancelMeasure = (SELECT IfNull(Sum(L.TotalMeasure),0) " & _
                             " 	               FROM JobOrderDetail L  " & _
                             "                 LEFT JOIN JobOrder H On L.DocId = H.DocId  " & _
                             " 	               WHERE H.JobOrder =  '" & TxtJobOrderNo.AgSelectedValue & "' " & _
                             " 	               AND Item = '" & .AgSelectedValue(Col2Item, I) & "' " & _
                             "                 And IfNull(H.IsDeleted,0) = 0 ) " & _
                             " WHERE DocId =  '" & TxtJobOrderNo.AgSelectedValue & "' " & _
                             " And Item =  '" & .AgSelectedValue(Col2Item, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Sub

    Private Sub BtnFillJobOrderDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillJobOrderDetail.Click
        If TxtJobOrderNo.AgSelectedValue <> "" Then
            Call ProcFillOrderDetailForCancel(TxtJobOrderNo.AgSelectedValue)
        End If
    End Sub

    Private Sub Validating_JobOrderItemForCancel(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
                Dgl1.AgSelectedValue(Col1Item, mRow) = ""
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.AgSelectedValue(Col1BOM, mRow) = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1TotalMeasure, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.AgSelectedValue(Col1ProdPlan, mRow) = ""
                Dgl1.AgSelectedValue(Col1ProdOrder, mRow) = ""
                Dgl1.Item(Col1Loss, mRow).Value = ""
                Dgl1.Item(Col1Rate, mRow).Value = 0
                Dgl1.Item(Col1Amount, mRow).Value = 0
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.AgSelectedValue(Col1ProdOrder, mRow) = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Call ProcFillOrderDetailForCancel(TxtJobOrderNo.AgSelectedValue, Dgl1.AgSelectedValue(Col1Item, mRow), Dgl1.AgSelectedValue(Col1ProdOrder, mRow), mRow)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_JobOrderItem Function")
        End Try
    End Sub
End Class
