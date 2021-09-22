Public Class TempJobReceive
    Inherits AgTemplate.TempTransaction
    Dim mQry$
    Dim DsMain As DataSet
    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Protected WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item As String = "Item"
    Protected Const Col1DocQty As String = "Doc.Qty"
    Protected Const Col1RetQty As String = "Ret.Qty"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1BillQty As String = "Bill Qty"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1FillQcParameter As String = "QC Detail"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1DocMeasure As String = "Doc.Measure"
    Protected Const Col1RetMeasure As String = "Ret.Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1BillMeasure As String = "Bill Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1JobOrderDocID As String = "Job Order No"
    Protected Const Col1JobIssueDocId As String = "Job Issue No"
    Protected Const Col1ProdOrder As String = "Prod.Order No"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1LossPer As String = "Loss Per"
    Protected Const Col1LossQty As String = "Loss Qty"
    Protected Const Col1BOM As String = "BOM"
    Protected Const Col1JobWithMaterialYN As String = "JobWithMaterialYN"

    Protected WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2Item As String = "Item"
    Protected Const Col2StockItem As String = "Stock Item"
    Protected Const Col2BOMItem As String = "BOM Item"
    Protected Const Col2Qty As String = "Qty"
    Protected Const Col2Unit As String = "Unit"
    Protected Const Col2ProdOrder As String = "Prod Order"
    Protected Const Col2JobOrder As String = "Job Order"
    Protected Const Col2LotNo As String = "Lot No"
    Protected Const Col2MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col2TotalMeasure As String = "Total Measure"
    Protected Const Col2MeasureUnit As String = "Measure Unit"


    Protected WithEvents Dgl3 As New AgControls.AgDataGrid
    Protected Const Col3Item As String = "Item"
    Protected Const Col3StockItem As String = "Stock Item"
    Protected Const Col3Qty As String = "Qty"
    Protected Const Col3LotNo As String = "Lot No"
    Protected Const Col3Unit As String = "Unit"
    Protected Const Col3MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col3TotalMeasure As String = "Total Measure"
    Protected Const Col3MeasureUnit As String = "Measure Unit"
    Protected Const Col3Rate As String = "Rate"
    Protected Const Col3Amount As String = "Amount"
    Protected Const Col3IsDelivered As String = "Delivered"
    Public Event BaseFunction_PostOrderGridFill()
    Public Event BaseFunction_ConsumptionGridFill()
    Public Event BaseFunction_PostConsumptionGridFill()

    Protected mPrevProcess$ = ""
    Protected mJobReceiveFor$ = ""
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtJobOrderNo As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label

    Dim mBillPosting As ClsMain.JobReceiveBillPosting = ClsMain.JobReceiveBillPosting.None

    Public Class HelpDataSet
        Public Shared Godown As DataSet = Nothing
        Public Shared Process As DataSet = Nothing
        Public Shared JobWorker As DataSet = Nothing
        Public Shared JobOrder As DataSet = Nothing
        Public Shared JobIssue As DataSet = Nothing
        Public Shared AgStructure As DataSet = Nothing
        Public Shared Item As DataSet = Nothing
        Public Shared ItemFromJobOrder As DataSet = Nothing
        Public Shared ItemFromJobIssue As DataSet = Nothing
        Public Shared ProdOrder As DataSet = Nothing
        Public Shared BOM As DataSet = Nothing
    End Class

    Public Property BillPosting() As ClsMain.JobReceiveBillPosting
        Get
            BillPosting = mBillPosting
        End Get
        Set(ByVal value As ClsMain.JobReceiveBillPosting)
            mBillPosting = value
        End Set
    End Property

    Public Property JobReceiveFor() As String
        Get
            JobReceiveFor = mJobReceiveFor
        End Get
        Set(ByVal value As String)
            mJobReceiveFor = value
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
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalRecMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalRecQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LblGodownReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.LblJobReceiveDetail = New System.Windows.Forms.LinkLabel
        Me.TxtBillingOn = New AgControls.AgTextBox
        Me.LblRemark1 = New System.Windows.Forms.Label
        Me.LblManualRefNoReq = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.BtnFill = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.LblCTotalMeasure = New System.Windows.Forms.Label
        Me.LblCTotalQtyMeasureText = New System.Windows.Forms.Label
        Me.LblCTotalQty = New System.Windows.Forms.Label
        Me.LblCTotalQtyText = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.LblTotalByProductMeasure = New System.Windows.Forms.Label
        Me.LblTotalByProductMeasureText = New System.Windows.Forms.Label
        Me.LblTotalByProductQty = New System.Windows.Forms.Label
        Me.LblTotalByProductQtyText = New System.Windows.Forms.Label
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.LblByProductDetail = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtJobOrderNo = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.BtnFillJobOrderDetails = New System.Windows.Forms.Button
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
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(746, 575)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 575)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 575)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 575)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 575)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 571)
        Me.GroupBox1.Size = New System.Drawing.Size(983, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 575)
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
        Me.LblV_No.Location = New System.Drawing.Point(16, 108)
        Me.LblV_No.Size = New System.Drawing.Size(101, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Job Receive No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(141, 107)
        Me.TxtV_No.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(131, 39)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(24, 34)
        Me.LblV_Date.Size = New System.Drawing.Size(108, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Job Receive Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(364, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(149, 33)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(255, 15)
        Me.LblV_Type.Size = New System.Drawing.Size(108, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Job Receive Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(380, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(131, 19)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(24, 14)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(149, 13)
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
        Me.LblPrefix.Location = New System.Drawing.Point(788, 138)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 18)
        Me.TabControl1.Size = New System.Drawing.Size(970, 114)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.BtnFillJobOrderDetails)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtJobOrderNo)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.LblManualRefNoReq)
        Me.TP1.Controls.Add(Me.LblRemark1)
        Me.TP1.Controls.Add(Me.TxtBillingOn)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblGodownReq)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.LblProcess)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(962, 88)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillingOn, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblRemark1, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillJobOrderDetails, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(965, 41)
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
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'TxtGodown
        '
        Me.TxtGodown.AgMandatory = True
        Me.TxtGodown.AgMasterHelp = False
        Me.TxtGodown.AgNumberLeftPlaces = 8
        Me.TxtGodown.AgNumberNegetiveAllow = False
        Me.TxtGodown.AgNumberRightPlaces = 2
        Me.TxtGodown.AgPickFromLastValue = False
        Me.TxtGodown.AgRowFilter = ""
        Me.TxtGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGodown.AgSelectedValue = Nothing
        Me.TxtGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGodown.Location = New System.Drawing.Point(622, 33)
        Me.TxtGodown.MaxLength = 20
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(317, 18)
        Me.TxtGodown.TabIndex = 7
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(521, 34)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 706
        Me.LblGodown.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalRecMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalRecQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(1, 264)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(961, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalRecMeasure
        '
        Me.LblTotalRecMeasure.AutoSize = True
        Me.LblTotalRecMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalRecMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalRecMeasure.Location = New System.Drawing.Point(424, 3)
        Me.LblTotalRecMeasure.Name = "LblTotalRecMeasure"
        Me.LblTotalRecMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalRecMeasure.TabIndex = 666
        Me.LblTotalRecMeasure.Text = "."
        Me.LblTotalRecMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTotalRecMeasure.Visible = False
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.AutoSize = True
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(313, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Total Measure :"
        Me.LblTotalMeasureText.Visible = False
        '
        'LblTotalRecQty
        '
        Me.LblTotalRecQty.AutoSize = True
        Me.LblTotalRecQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalRecQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalRecQty.Location = New System.Drawing.Point(116, 3)
        Me.LblTotalRecQty.Name = "LblTotalRecQty"
        Me.LblTotalRecQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalRecQty.TabIndex = 660
        Me.LblTotalRecQty.Text = "."
        Me.LblTotalRecQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(31, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(1, 149)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(962, 115)
        Me.Pnl1.TabIndex = 1
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
        Me.TxtRemarks.Location = New System.Drawing.Point(622, 53)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(317, 18)
        Me.TxtRemarks.TabIndex = 8
        '
        'LblGodownReq
        '
        Me.LblGodownReq.AutoSize = True
        Me.LblGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblGodownReq.Location = New System.Drawing.Point(604, 41)
        Me.LblGodownReq.Name = "LblGodownReq"
        Me.LblGodownReq.Size = New System.Drawing.Size(10, 7)
        Me.LblGodownReq.TabIndex = 724
        Me.LblGodownReq.Text = "Ä"
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgMandatory = True
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(380, 33)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(125, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(253, 33)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(101, 16)
        Me.LblManualRefNo.TabIndex = 726
        Me.LblManualRefNo.Text = "Manual Ref. No."
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.AutoSize = True
        Me.LblJobWorkerReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobWorkerReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(131, 60)
        Me.LblJobWorkerReq.Name = "LblJobWorkerReq"
        Me.LblJobWorkerReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobWorkerReq.TabIndex = 735
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
        Me.TxtJobWorker.Location = New System.Drawing.Point(149, 53)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(356, 18)
        Me.TxtJobWorker.TabIndex = 4
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(24, 54)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(74, 16)
        Me.LblJobWorker.TabIndex = 734
        Me.LblJobWorker.Text = "Job Worker"
        '
        'TxtProcess
        '
        Me.TxtProcess.AgMandatory = True
        Me.TxtProcess.AgMasterHelp = False
        Me.TxtProcess.AgNumberLeftPlaces = 8
        Me.TxtProcess.AgNumberNegetiveAllow = False
        Me.TxtProcess.AgNumberRightPlaces = 2
        Me.TxtProcess.AgPickFromLastValue = False
        Me.TxtProcess.AgRowFilter = ""
        Me.TxtProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcess.AgSelectedValue = Nothing
        Me.TxtProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProcess.Location = New System.Drawing.Point(855, 120)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(84, 18)
        Me.TxtProcess.TabIndex = 5
        Me.TxtProcess.Visible = False
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(793, 120)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 737
        Me.LblProcess.Text = "Process"
        Me.LblProcess.Visible = False
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(621, 450)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(341, 118)
        Me.PnlCalcGrid.TabIndex = 725
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
        Me.TxtStructure.Location = New System.Drawing.Point(855, 158)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(84, 18)
        Me.TxtStructure.TabIndex = 742
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(785, 158)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 743
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'LblJobReceiveDetail
        '
        Me.LblJobReceiveDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobReceiveDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobReceiveDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobReceiveDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobReceiveDetail.LinkColor = System.Drawing.Color.White
        Me.LblJobReceiveDetail.Location = New System.Drawing.Point(1, 131)
        Me.LblJobReceiveDetail.Name = "LblJobReceiveDetail"
        Me.LblJobReceiveDetail.Size = New System.Drawing.Size(136, 17)
        Me.LblJobReceiveDetail.TabIndex = 733
        Me.LblJobReceiveDetail.TabStop = True
        Me.LblJobReceiveDetail.Text = "Job Receive Detail"
        Me.LblJobReceiveDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtBillingOn
        '
        Me.TxtBillingOn.AgMandatory = False
        Me.TxtBillingOn.AgMasterHelp = False
        Me.TxtBillingOn.AgNumberLeftPlaces = 8
        Me.TxtBillingOn.AgNumberNegetiveAllow = False
        Me.TxtBillingOn.AgNumberRightPlaces = 2
        Me.TxtBillingOn.AgPickFromLastValue = False
        Me.TxtBillingOn.AgRowFilter = ""
        Me.TxtBillingOn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillingOn.AgSelectedValue = Nothing
        Me.TxtBillingOn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillingOn.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillingOn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillingOn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillingOn.Location = New System.Drawing.Point(855, 139)
        Me.TxtBillingOn.MaxLength = 20
        Me.TxtBillingOn.Name = "TxtBillingOn"
        Me.TxtBillingOn.Size = New System.Drawing.Size(84, 18)
        Me.TxtBillingOn.TabIndex = 744
        Me.TxtBillingOn.Visible = False
        '
        'LblRemark1
        '
        Me.LblRemark1.AutoSize = True
        Me.LblRemark1.BackColor = System.Drawing.Color.Transparent
        Me.LblRemark1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemark1.Location = New System.Drawing.Point(521, 54)
        Me.LblRemark1.Name = "LblRemark1"
        Me.LblRemark1.Size = New System.Drawing.Size(60, 16)
        Me.LblRemark1.TabIndex = 745
        Me.LblRemark1.Text = "Remarks"
        '
        'LblManualRefNoReq
        '
        Me.LblManualRefNoReq.AutoSize = True
        Me.LblManualRefNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualRefNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualRefNoReq.Location = New System.Drawing.Point(362, 40)
        Me.LblManualRefNoReq.Name = "LblManualRefNoReq"
        Me.LblManualRefNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualRefNoReq.TabIndex = 746
        Me.LblManualRefNoReq.Text = "Ä"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(1, 291)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(126, 17)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Consumption Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(1, 309)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(962, 99)
        Me.Pnl2.TabIndex = 3
        '
        'BtnFill
        '
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.Location = New System.Drawing.Point(133, 289)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(49, 20)
        Me.BtnFill.TabIndex = 2
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel2.Controls.Add(Me.LblCTotalMeasure)
        Me.Panel2.Controls.Add(Me.LblCTotalQtyMeasureText)
        Me.Panel2.Controls.Add(Me.LblCTotalQty)
        Me.Panel2.Controls.Add(Me.LblCTotalQtyText)
        Me.Panel2.Location = New System.Drawing.Point(1, 408)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(961, 23)
        Me.Panel2.TabIndex = 741
        '
        'LblCTotalMeasure
        '
        Me.LblCTotalMeasure.AutoSize = True
        Me.LblCTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblCTotalMeasure.Location = New System.Drawing.Point(424, 3)
        Me.LblCTotalMeasure.Name = "LblCTotalMeasure"
        Me.LblCTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblCTotalMeasure.TabIndex = 666
        Me.LblCTotalMeasure.Text = "."
        Me.LblCTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCTotalMeasure.Visible = False
        '
        'LblCTotalQtyMeasureText
        '
        Me.LblCTotalQtyMeasureText.AutoSize = True
        Me.LblCTotalQtyMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCTotalQtyMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblCTotalQtyMeasureText.Location = New System.Drawing.Point(313, 3)
        Me.LblCTotalQtyMeasureText.Name = "LblCTotalQtyMeasureText"
        Me.LblCTotalQtyMeasureText.Size = New System.Drawing.Size(105, 16)
        Me.LblCTotalQtyMeasureText.TabIndex = 665
        Me.LblCTotalQtyMeasureText.Text = "Total Measure :"
        Me.LblCTotalQtyMeasureText.Visible = False
        '
        'LblCTotalQty
        '
        Me.LblCTotalQty.AutoSize = True
        Me.LblCTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblCTotalQty.Location = New System.Drawing.Point(116, 3)
        Me.LblCTotalQty.Name = "LblCTotalQty"
        Me.LblCTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblCTotalQty.TabIndex = 660
        Me.LblCTotalQty.Text = "."
        Me.LblCTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCTotalQtyText
        '
        Me.LblCTotalQtyText.AutoSize = True
        Me.LblCTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblCTotalQtyText.Location = New System.Drawing.Point(31, 3)
        Me.LblCTotalQtyText.Name = "LblCTotalQtyText"
        Me.LblCTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblCTotalQtyText.TabIndex = 659
        Me.LblCTotalQtyText.Text = "Total Qty :"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel4.Controls.Add(Me.LblTotalByProductMeasure)
        Me.Panel4.Controls.Add(Me.LblTotalByProductMeasureText)
        Me.Panel4.Controls.Add(Me.LblTotalByProductQty)
        Me.Panel4.Controls.Add(Me.LblTotalByProductQtyText)
        Me.Panel4.Location = New System.Drawing.Point(0, 544)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(613, 23)
        Me.Panel4.TabIndex = 743
        '
        'LblTotalByProductMeasure
        '
        Me.LblTotalByProductMeasure.AutoSize = True
        Me.LblTotalByProductMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalByProductMeasure.Location = New System.Drawing.Point(284, 3)
        Me.LblTotalByProductMeasure.Name = "LblTotalByProductMeasure"
        Me.LblTotalByProductMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalByProductMeasure.TabIndex = 666
        Me.LblTotalByProductMeasure.Text = "."
        Me.LblTotalByProductMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTotalByProductMeasure.Visible = False
        '
        'LblTotalByProductMeasureText
        '
        Me.LblTotalByProductMeasureText.AutoSize = True
        Me.LblTotalByProductMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalByProductMeasureText.Location = New System.Drawing.Point(176, 3)
        Me.LblTotalByProductMeasureText.Name = "LblTotalByProductMeasureText"
        Me.LblTotalByProductMeasureText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalByProductMeasureText.TabIndex = 665
        Me.LblTotalByProductMeasureText.Text = "Total Measure :"
        Me.LblTotalByProductMeasureText.Visible = False
        '
        'LblTotalByProductQty
        '
        Me.LblTotalByProductQty.AutoSize = True
        Me.LblTotalByProductQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalByProductQty.Location = New System.Drawing.Point(116, 3)
        Me.LblTotalByProductQty.Name = "LblTotalByProductQty"
        Me.LblTotalByProductQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalByProductQty.TabIndex = 660
        Me.LblTotalByProductQty.Text = "."
        Me.LblTotalByProductQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalByProductQtyText
        '
        Me.LblTotalByProductQtyText.AutoSize = True
        Me.LblTotalByProductQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalByProductQtyText.Location = New System.Drawing.Point(31, 3)
        Me.LblTotalByProductQtyText.Name = "LblTotalByProductQtyText"
        Me.LblTotalByProductQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalByProductQtyText.TabIndex = 659
        Me.LblTotalByProductQtyText.Text = "Total Qty :"
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(0, 450)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(613, 94)
        Me.Pnl3.TabIndex = 4
        '
        'LblByProductDetail
        '
        Me.LblByProductDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblByProductDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblByProductDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblByProductDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblByProductDetail.LinkColor = System.Drawing.Color.White
        Me.LblByProductDetail.Location = New System.Drawing.Point(0, 432)
        Me.LblByProductDetail.Name = "LblByProductDetail"
        Me.LblByProductDetail.Size = New System.Drawing.Size(128, 17)
        Me.LblByProductDetail.TabIndex = 744
        Me.LblByProductDetail.TabStop = True
        Me.LblByProductDetail.Text = "By Product Detail"
        Me.LblByProductDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(604, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 749
        Me.Label1.Text = "Ä"
        Me.Label1.Visible = False
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
        Me.TxtJobOrderNo.Location = New System.Drawing.Point(622, 13)
        Me.TxtJobOrderNo.MaxLength = 50
        Me.TxtJobOrderNo.Name = "TxtJobOrderNo"
        Me.TxtJobOrderNo.Size = New System.Drawing.Size(262, 18)
        Me.TxtJobOrderNo.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(521, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 748
        Me.Label3.Text = "Job Order No."
        '
        'BtnFillJobOrderDetails
        '
        Me.BtnFillJobOrderDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillJobOrderDetails.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillJobOrderDetails.Location = New System.Drawing.Point(890, 10)
        Me.BtnFillJobOrderDetails.Name = "BtnFillJobOrderDetails"
        Me.BtnFillJobOrderDetails.Size = New System.Drawing.Size(49, 20)
        Me.BtnFillJobOrderDetails.TabIndex = 6
        Me.BtnFillJobOrderDetails.Text = "Fill"
        Me.BtnFillJobOrderDetails.UseVisualStyleBackColor = True
        Me.BtnFillJobOrderDetails.Visible = False
        '
        'TempJobReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(965, 616)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.LblByProductDetail)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.BtnFill)
        Me.Controls.Add(Me.LblJobReceiveDetail)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "TempJobReceive"
        Me.Text = "Template Job Receive"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.LblJobReceiveDetail, 0)
        Me.Controls.SetChildIndex(Me.BtnFill, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.LblByProductDetail, 0)
        Me.Controls.SetChildIndex(Me.Pnl3, 0)
        Me.Controls.SetChildIndex(Me.Panel4, 0)
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
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalRecQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalRecMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents LblGodownReq As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents LblProcess As System.Windows.Forms.Label
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents LblJobReceiveDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtBillingOn As AgControls.AgTextBox
    Protected WithEvents LblRemark1 As System.Windows.Forms.Label
    Protected WithEvents LblManualRefNoReq As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents Pnl2 As System.Windows.Forms.Panel
    Protected WithEvents BtnFill As System.Windows.Forms.Button
    Protected WithEvents Panel2 As System.Windows.Forms.Panel
    Protected WithEvents LblCTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents LblCTotalQtyMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblCTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblCTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Panel4 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalByProductMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl3 As System.Windows.Forms.Panel
    Protected WithEvents LblByProductDetail As System.Windows.Forms.LinkLabel

    Protected WithEvents BtnFillJobOrderDetails As System.Windows.Forms.Button
#End Region

    Private Sub TempJobReceive_BaseEvent_Approve_PreTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Approve_PreTrans
        mQry = " SELECT H.DocId,L.Item,L.Qty,L.JobOrder,L.TotalMeasure,L.ProdOrder  " & _
                " FROM JobIssRec H With (Nolock) " & _
                " LEFT JOIN JobReceiveDetail L With (Nolock) ON H.DocID=L.DocId  " & _
                " WHERE H.DocID = " & AgL.Chk_Text(mInternalCode) & " "
        DsMain = AgL.FillData(mQry, AgL.GcnRead)
    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobIssRec"
        LogTableName = "JobIssRec_Log"
        MainLineTableCsv = "JobReceiveDetail,QCParameterDetail,Structure_TransFooter,Structure_TransLine,JobReceiveBOM,JobReceiveByProduct"
        LogLineTableCsv = "JobReceiveDetail_Log,QCParameterDetail_Log,Structure_TransFooter_Log,Structure_TransLine_Log,JobReceiveBOM_Log,JobReceiveByProduct_Log"
        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl2)
        AgL.GridDesign(Dgl3)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Receive Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Receive No], " & _
                    " H.ManualRefNo AS [Manual No], H.Process, H.DueDate AS [Due Date], H.IssQty AS [Issue Qty], H.IssMeasure AS [Issue Measure],  " & _
                    " H.RecQty AS [Rec Qty], H.RecMeasure AS [Rec Measure], H.JobReceiveFor AS [Job Receive For], H.Remarks, H.Structure, H.EntryBy AS [Entry By],  " & _
                    " H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date],  " & _
                    " H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, H.BillingType AS [Billing Type], H.OrderBy AS [ORDER By],  " & _
                    " H.TotalWeight AS [Total Weight], H.JobWorkerDocNo AS [Job Worker DocNo], H.TotalConsumptionQty AS [Total Consumption Qty], H.TotalConsumptionMeasure AS [Total Consumption Measure],  " & _
                    " H.TotalByProductQty AS [Total By Product Qty], H.TotalByProductMeasure AS [Total By Product Measure], " & _
                    " D.Div_Name AS Division, SM.Name AS [Site Name], SGJ.DispName AS [Job Worker Name], G.Description AS Godown, JO.ManualRefNo AS [Job ORDER No], SF.*  " & _
                    " FROM JobIssRec_Log H " & _
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
                    " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " & _
                    " LEFT JOIN SubGroup SGJ ON SGJ.SubCode=H.JobWorker  " & _
                    " LEFT JOIN Godown G ON G.Code = H.Godown   " & _
                    " LEFT JOIN JobOrder  JO ON H.JobOrder   =JO.DocID  " & _
                    " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQueryFooter(AgL, EntryNCat, FrmType) & ") As SF On H.UID = SF.UId " & _
                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " And IsNull(H.IsDeleted,0)=0  " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Receive Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Receive No], " & _
                            " H.ManualRefNo AS [Manual No], H.Process, H.DueDate AS [Due Date], H.IssQty AS [Issue Qty], H.IssMeasure AS [Issue Measure],  " & _
                            " H.RecQty AS [Rec Qty], H.RecMeasure AS [Rec Measure], H.JobReceiveFor AS [Job Receive For], H.Remarks, H.Structure, H.EntryBy AS [Entry By],  " & _
                            " H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date],  " & _
                            " H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, H.BillingType AS [Billing Type], H.OrderBy AS [ORDER By],  " & _
                            " H.TotalWeight AS [Total Weight], H.JobWorkerDocNo AS [Job Worker DocNo], H.TotalConsumptionQty AS [Total Consumption Qty], H.TotalConsumptionMeasure AS [Total Consumption Measure],  " & _
                            " H.TotalByProductQty AS [Total By Product Qty], H.TotalByProductMeasure AS [Total By Product Measure], " & _
                            " D.Div_Name AS Division, SM.Name AS [Site Name], SGJ.DispName AS [Job Worker Name], G.Description AS Godown, JO.ManualRefNo AS [Job ORDER No], SF.* " & _
                            " FROM JobIssRec H " & _
                            " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
                            " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " & _
                            " LEFT JOIN SubGroup SGJ ON SGJ.SubCode=H.JobWorker  " & _
                            " LEFT JOIN Godown G ON G.Code = H.Godown   " & _
                            " LEFT JOIN JobOrder  JO ON H.JobOrder   =JO.DocID  " & _
                            " LEFT JOIN (" & AgStructure.AgCalcGrid.AgStructureSubQueryFooter(AgL, EntryNCat, FrmType) & ") As SF On H.DocId = SF.DocId " & _
                            " Where 1=1  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select J.DocID As SearchCode " & _
                " From JobIssRec J " & _
                " Left Join Voucher_Type Vt On J.V_Type = Vt.V_Type  " & _
                " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By J.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And J.EntryStatus='" & LogStatus.LogOpen & "' "

        mQry = " Select J.UID As SearchCode " & _
            " From JobIssRec_Log J " & _
            " Left Join Voucher_Type Vt On J.V_Type = Vt.V_Type  " & _
            " Where 1=1  " & mCondStr & "  Order By J.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 8, 4, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1BillQty, 70, 8, 4, False, Col1BillQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1LotNo, 70, 0, Col1LotNo, False, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, False, Col1Qty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1LossPer, 70, 8, 4, False, Col1LossPer, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1LossQty, 70, 8, 4, False, Col1LossQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1RetQty, 70, 8, 4, False, Col1RetQty, True, False, True)
            .AddAgButtonColumn(Dgl1, Col1FillQcParameter, 60, Col1FillQcParameter, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 4, False, Col1MeasurePerPcs, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1DocMeasure, 100, 8, 4, False, Col1DocMeasure, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1RetMeasure, 100, 8, 4, False, Col1RetMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1BillMeasure, 100, 8, 4, False, Col1BillMeasure, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, True, True)
            .AddAgTextColumn(Dgl1, Col1JobOrderDocID, 100, 0, Col1JobOrderDocID, IIf(JobReceiveFor = AgTemplate.ClsMain.JobReceiveFor.JobOrder, True, False), True)
            .AddAgTextColumn(Dgl1, Col1JobIssueDocId, 100, 0, Col1JobIssueDocId, IIf(JobReceiveFor = AgTemplate.ClsMain.JobReceiveFor.JobIssue, True, False), True)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 100, 0, Col1ProdOrder, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 70, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1BOM, 100, 0, Col1BOM, False, True)
            .AddAgNumberColumn(Dgl1, Col1JobWithMaterialYN, 70, 8, 2, False, Col1JobWithMaterialYN, False, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False

        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True)
            .AddAgTextColumn(Dgl2, Col2Item, 180, 5, Col2Item, True, True)
            .AddAgTextColumn(Dgl2, Col2BOMItem, 160, 5, Col2BOMItem, True, False)
            .AddAgTextColumn(Dgl2, Col2StockItem, 160, 5, Col2StockItem, False, False)
            .AddAgNumberColumn(Dgl2, Col2Qty, 70, 5, 4, False, Col2Qty, True, False)
            .AddAgTextColumn(Dgl2, Col2Unit, 70, 5, Col2Unit, True, True)
            .AddAgTextColumn(Dgl2, Col2LotNo, 70, 5, Col2LotNo, IIf(AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsLotNoApplicable")) = 0, False, True), False)
            .AddAgNumberColumn(Dgl2, Col2MeasurePerPcs, 70, 5, 4, False, Col2MeasurePerPcs, True, True)
            .AddAgNumberColumn(Dgl2, Col2TotalMeasure, 70, 5, 4, False, Col2TotalMeasure, True, True)
            .AddAgTextColumn(Dgl2, Col2MeasureUnit, 70, 5, Col2MeasureUnit, True, True)
            .AddAgTextColumn(Dgl2, Col2ProdOrder, 100, 5, Col2ProdOrder, True, True)
            .AddAgTextColumn(Dgl2, Col2JobOrder, 100, 5, Col2JobOrder, True, True)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 35
        Dgl2.AgSkipReadOnlyColumns = True


        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 40, 5, ColSNo, True, True)
            .AddAgTextColumn(Dgl3, Col3Item, 200, 5, Col3Item, True, False)
            .AddAgTextColumn(Dgl3, Col3StockItem, 100, 5, Col3StockItem, False, False)
            .AddAgNumberColumn(Dgl3, Col3Qty, 70, 5, 4, False, Col3Qty, True, False)
            .AddAgTextColumn(Dgl3, Col3LotNo, 70, 5, Col3LotNo, IIf(AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsLotNoApplicable")) = 0, False, True), False)
            .AddAgTextColumn(Dgl3, Col3Unit, 50, 5, Col3Unit, True, True)
            .AddAgNumberColumn(Dgl3, Col3MeasurePerPcs, 100, 5, 4, False, Col3MeasurePerPcs, False, False)
            .AddAgNumberColumn(Dgl3, Col3TotalMeasure, 100, 5, 4, False, Col3TotalMeasure, False, False)
            .AddAgTextColumn(Dgl3, Col3MeasureUnit, 100, 5, Col3MeasureUnit, False, False)
            .AddAgNumberColumn(Dgl3, Col3Rate, 70, 5, 2, False, Col3Rate, True, False)
            .AddAgNumberColumn(Dgl3, Col3Amount, 80, 5, 2, False, Col3Amount, True, True)
            .AddAgListColumn(Dgl3, "Yes,No", Col3IsDelivered, 70, , Col3IsDelivered, True, False)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 25
        Dgl3.AgSkipReadOnlyColumns = True

        'Dgl1.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl1.DefaultCellStyle.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl1.RowTemplate.Height = 18
        'Dgl1.ColumnHeadersHeight = 30

        'Dgl2.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl2.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl2.DefaultCellStyle.Font = New Font(Dgl2.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl2.RowTemplate.Height = 18
        'Dgl2.ColumnHeadersHeight = 18

        'Dgl3.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl3.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl3.DefaultCellStyle.Font = New Font(Dgl3.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular)
        'Dgl3.RowTemplate.Height = 18
        'Dgl3.ColumnHeadersHeight = 18



        FrmProductionOrder_BaseFunction_FIniList()


        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)
        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index

        FrmProductionOrder_BaseFunction_FIniList()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim Stock As ClsMain.StructStock = Nothing, StockProcess As ClsMain.StructStock = Nothing

        mQry = "UPDATE JobIssRec_Log " & _
                " SET " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " & _
                " JobWorker = " & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " & _
                " Godown = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & _
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                " RecQty = " & Val(LblTotalRecQty.Text) & ", " & _
                " RecMeasure = " & Val(LblTotalRecMeasure.Text) & ", " & _
                " JobReceiveFor = " & AgL.Chk_Text(mJobReceiveFor) & ", " & _
                " TotalConsumptionQty = " & Val(LblCTotalQty.Text) & ", " & _
                " TotalConsumptionMeasure = " & Val(LblCTotalMeasure.Text) & ", " & _
                " TotalByProductQty = " & Val(LblTotalByProductQty.Text) & ", " & _
                " TotalByProductMeasure = " & Val(LblTotalByProductMeasure.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        AgCalcGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)

        mQry = "Delete From JobReceiveDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From QcParameterDetail Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "Delete from JobIssRecUID_Log Where UID='" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                mQry = "INSERT INTO JobReceiveDetail_Log(UID, DocId, Sr, Item,DocQty, RetQty, Qty, BillQty, Unit, MeasurePerPcs,DocMeasure, RetMeasure, TotalMeasure, BillMeasure, " & _
                        " MeasureUnit, JobOrder, Rate, Amount, Netamount, Remark, JobIssueDocId, " & _
                        " ProdOrder, LossPer, LossQty, LotNo, BOM) " & _
                        " Values (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & "," & _
                        " " & mSr & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " & Val(Dgl1.Item(Col1RetQty, I).Value) & ", " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & Val(Dgl1.Item(Col1BillQty, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1DocMeasure, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1RetMeasure, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1BillMeasure, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1JobOrderDocID, I)) & ", " & _
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                        " " & Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, I, AgStructure.AgCalcGrid.LineColumnType.Amount)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1JobIssueDocId, I)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1ProdOrder, I)) & ", " & _
                        " " & Val(Dgl1.Item(Col1LossPer, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1LossQty, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1BOM, I)) & " " & _
                        " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)                

                If Dgl1.Item(Col1FillQcParameter, I).Tag IsNot Nothing Then
                    Call CType(Dgl1.Item(Col1FillQcParameter, I).Tag, FrmJobQcDetail).ProcSaveQcParameterDetail(Conn, Cmd, mInternalCode, mSearchCode, mSr)
                End If

                AgCalcGrid1.Save_TransLine(mInternalCode, mSr, I, Conn, Cmd, SearchCode)
                RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
            End If
        Next

        Save_BomDetail(SearchCode, Conn, Cmd)
        Save_ByProductDetail(SearchCode, Conn, Cmd)
    End Sub

    Private Sub Save_BomDetail(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand)
        Dim I As Integer, mSr As Integer
        mQry = " Delete From JobReceiveBOM_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl2
            For I = 0 To .Rows.Count - 2
                If .Item(Col2Item, I).Value <> "" Then
                    mSr += 2
                    mQry = " INSERT INTO JobReceiveBOM_Log(DocId, " & _
                            " Sr, " & _
                            " Item, " & _
                            " BOMItem, " & _
                            " StockItem, " & _
                            " Qty, " & _
                            " LotNo, " & _
                            " Unit, " & _
                            " MeasurePerPcs, " & _
                            " TotalMeasure, " & _
                            " MeasureUnit, " & _
                            " PrevProcess, " & _
                            " ProdOrder, " & _
                            " JobOrder, " & _
                            " UID) " & _
                            " VALUES (" & AgL.Chk_Text(mInternalCode) & ", " & _
                            " " & Val(mSr) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col2Item, I)) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col2BOMItem, I)) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col2StockItem, I)) & ", " & _
                            " " & Val(.Item(Col2Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col2LotNo, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col2Unit, I).Value) & ", " & _
                            " " & Val(.Item(Col2MeasurePerPcs, I).Value) & ", " & _
                            " " & Val(.Item(Col2TotalMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col2MeasureUnit, I).Value) & ", " & _
                            " " & AgL.Chk_Text(mPrevProcess) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col2ProdOrder, I)) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col2JobOrder, I)) & ", " & _
                            " '" & mSearchCode & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

    End Sub

    Private Sub Save_ByProductDetail(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand)
        Dim I As Integer, mSr As Integer

        mQry = " Delete From JobReceiveByProduct_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl3
            For I = 0 To .Rows.Count - 1
                If .Item(Col3Item, I).Value <> "" Then
                    mSr += 3
                    mQry = " INSERT INTO JobReceiveByProduct_Log(DocId, " & _
                            " Sr, " & _
                            " Item, " & _
                            " StockItem, " & _
                            " Qty, " & _
                            " LotNo, " & _
                            " Unit, " & _
                            " MeasurePerPcs, " & _
                            " TotalMeasure, " & _
                            " MeasureUnit, " & _
                            " Process, " & _
                            " Rate, " & _
                            " Amount,  " & _
                            " IsDelivered,  " & _
                            " UID) " & _
                            " VALUES (" & AgL.Chk_Text(mInternalCode) & ", " & _
                            " " & Val(mSr) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col3Item, I)) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col3StockItem, I)) & ", " & _
                            " " & Val(.Item(Col3Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3LotNo, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3Unit, I).Value) & ", " & _
                            " " & Val(.Item(Col3MeasurePerPcs, I).Value) & ", " & _
                            " " & Val(.Item(Col3TotalMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3MeasureUnit, I).Value) & ", " & _
                            " " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " & _
                            " " & Val(.Item(Col3Rate, I).Value) & ", " & _
                            " " & Val(.Item(Col3Amount, I).Value) & ", " & _
                            " " & IIf(AgL.StrCmp(.Item(Col3IsDelivered, I).Value, "Yes"), 1, 0) & ", " & _
                            " '" & mSearchCode & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select J.* " & _
                " From JobIssRec J " & _
                " Where J.DocID='" & SearchCode & "'"
        Else
            mQry = "Select J.* " & _
                " From JobIssRec_Log J " & _
                " Where J.UID='" & SearchCode & "'"
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
                TxtGodown.AgSelectedValue = AgL.XNull(.Rows(0)("Godown"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtProcess.AgSelectedValue = AgL.XNull(.Rows(0)("Process"))
                TxtBillingOn.Text = AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar
                TxtJobWorker.AgSelectedValue = AgL.XNull(.Rows(0)("JobWorker"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalRecQty.Text = AgL.VNull(.Rows(0)("RecQty"))
                LblTotalRecMeasure.Text = AgL.VNull(.Rows(0)("RecMeasure"))
                LblCTotalQty.Text = AgL.VNull(.Rows(0)("IssQty"))
                LblCTotalMeasure.Text = AgL.VNull(.Rows(0)("IssMeasure"))
                LblCTotalQty.Text = AgL.VNull(.Rows(0)("TotalConsumptionQty"))
                LblCTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalConsumptionMeasure"))
                LblTotalByProductQty.Text = AgL.VNull(.Rows(0)("TotalByProductQty"))
                LblTotalByProductMeasure.Text = AgL.VNull(.Rows(0)("TotalByProductMeasure"))

                AgCalcGrid1.MoveRec_TransFooter(SearchCode)

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select L.*, IsNull(J.JobWithMaterialYN,0) As JobWithMaterialYN " & _
                            " from JobReceiveDetail L " & _
                            " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " & _
                            " where L.DocId = '" & SearchCode & "' Order By L.Sr"
                Else
                    mQry = "Select L.*, IsNull(J.JobWithMaterialYN,0) As JobWithMaterialYN " & _
                            " from JobReceiveDetail_Log L " & _
                            " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " & _
                            " where L.UID = '" & SearchCode & "' Order By L.Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(CType(Dgl1.Columns(Col1DocQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1RetQty, I).Value = Format(AgL.VNull(.Rows(I)("RetQty")), "0.".PadRight(CType(Dgl1.Columns(Col1RetQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1BillQty, I).Value = Format(AgL.VNull(.Rows(I)("BillQty")), "0.".PadRight(CType(Dgl1.Columns(Col1BillQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl1.Item(Col1DocMeasure, I).Value = AgL.VNull(.Rows(I)("DocMeasure"))
                            Dgl1.Item(Col1RetMeasure, I).Value = AgL.VNull(.Rows(I)("RetMeasure"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                            Dgl1.Item(Col1BillMeasure, I).Value = AgL.VNull(.Rows(I)("BillMeasure"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.AgSelectedValue(Col1JobOrderDocID, I) = AgL.XNull(.Rows(I)("JobOrder"))
                            Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.AgSelectedValue(Col1JobIssueDocId, I) = AgL.XNull(.Rows(I)("JobIssueDocId"))
                            Dgl1.AgSelectedValue(Col1ProdOrder, I) = AgL.XNull(.Rows(I)("ProdOrder"))

                            Dgl1.Item(Col1LossPer, I).Value = AgL.VNull(.Rows(I)("LossPer"))
                            Dgl1.Item(Col1LossQty, I).Value = AgL.VNull(.Rows(I)("LossQty"))
                            Dgl1.Item(Col1JobWithMaterialYN, I).Value = AgL.VNull(.Rows(I)("JobWithMaterialYN"))

                            Dgl1.AgSelectedValue(Col1BOM, I) = AgL.XNull(.Rows(I)("Bom"))

                            Dim FrmObj As New FrmJobQcDetail()
                            Dgl1.Item(Col1FillQcParameter, I).Tag = FrmObj.FunRetQcParameterDetail(SearchCode, AgL.VNull(.Rows(I)("Sr")), FrmType)
                            CType(Dgl1.Item(Col1FillQcParameter, I).Tag, FrmJobQcDetail).LblItemName.Text = Dgl1.Item(Col1Item, I).Value

                            AgCalcGrid1.MoveRec_TransLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                            RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If
                End With

                '-------------------------------------------------------------
                MoveRec_BomDetail(SearchCode)
                MoveRec_ByProductDetail(SearchCode)
            End If
        End With
    End Sub

    Private Sub MoveRec_BomDetail(ByVal SearchCode As String)
        Dim I As Integer
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select * from JobReceiveBom where DocId = '" & SearchCode & "' Order By Sr"
        Else
            mQry = "Select * from JobReceiveBom_Log where UID = '" & SearchCode & "' Order By Sr"
        End If

        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl2.RowCount = 1
            Dgl2.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl2.Rows.Add()
                    Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                    Dgl2.AgSelectedValue(Col2Item, I) = AgL.XNull(.Rows(I)("Item"))
                    Dgl2.AgSelectedValue(Col2BOMItem, I) = AgL.XNull(.Rows(I)("BOMItem"))
                    Dgl2.AgSelectedValue(Col2StockItem, I) = AgL.XNull(.Rows(I)("StockItem"))
                    Dgl2.Item(Col2Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(CType(Dgl2.Columns(Col2Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    Dgl2.Item(Col2LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                    Dgl2.Item(Col2Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl2.Item(Col2MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                    Dgl2.Item(Col2TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                    Dgl2.Item(Col2MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl2.AgSelectedValue(Col2ProdOrder, I) = AgL.XNull(.Rows(I)("ProdOrder"))
                    Dgl2.AgSelectedValue(Col2JobOrder, I) = AgL.XNull(.Rows(I)("JobOrder"))
                    mPrevProcess = AgL.XNull(.Rows(I)("PrevProcess"))
                Next I
            End If
        End With

    End Sub


    Private Sub MoveRec_ByProductDetail(ByVal SearchCode As String)
        Dim I As Integer
        Dim DsTemp As DataSet


        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select * from JobReceiveByProduct where DocId = '" & SearchCode & "' Order By Sr"
        Else
            mQry = "Select * from JobReceiveByProduct_Log where UID = '" & SearchCode & "' Order By Sr"
        End If

        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl3.RowCount = 1
            Dgl3.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl3.Rows.Add()
                    Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count - 1
                    Dgl3.AgSelectedValue(Col3Item, I) = AgL.XNull(.Rows(I)("Item"))
                    Dgl3.AgSelectedValue(Col3StockItem, I) = AgL.XNull(.Rows(I)("StockItem"))
                    Dgl3.Item(Col3Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl3.Item(Col3LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                    Dgl3.Item(Col3Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl3.Item(Col3MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                    Dgl3.Item(Col3TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                    Dgl3.Item(Col3MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl3.Item(Col3Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                    Dgl3.Item(Col3Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                    Dgl3.Item(Col3IsDelivered, I).Value = IIf(AgL.VNull(.Rows(I)("IsDelivered")) = 0, "No", "Yes")
                Next I
            End If
        End With

    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        Topctrl1.ChangeAgGridState(Dgl2, False)
        Topctrl1.ChangeAgGridState(Dgl3, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtV_Date.Validating, TxtManualRefNo.Validating
        Select Case sender.NAME
            Case TxtV_Type.Name
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                IniGrid()
                If Topctrl1.Mode = "Add" Then
                    TxtManualRefNo.Text = ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, ClsMain.ManualRefType.Max)
                End If

            Case TxtV_Date.Name
                If Topctrl1.Mode = "Add" Then
                    TxtManualRefNo.Text = ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, ClsMain.ManualRefType.Max)
                End If

            Case TxtManualRefNo.Name
                e.Cancel = Not FCheckDuplicateRefNo()
        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
        If mJobReceiveFor = "" Then MsgBox("Job Receive Property Is Not Set", MsgBoxStyle.Exclamation) : Topctrl1.FButtonClick(14, True)
        If mBillPosting = ClsMain.JobReceiveBillPosting.None Then MsgBox("Bill Posting Property Is Not Set", MsgBoxStyle.Exclamation) : Topctrl1.FButtonClick(14, True)
        TxtProcess.AgSelectedValue = AgL.Dman_Execute(" SELECT H.NCat FROM Process H WHERE H.ProcessReceiveNCat = '" & EntryNCat & "' ", AgL.GCn).ExecuteScalar
        TxtBillingOn.Text = AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar
        TxtManualRefNo.Text = ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, ClsMain.ManualRefType.Max)
        mPrevProcess = FunGetPrevProcess(TxtProcess.AgSelectedValue)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        TxtGodown.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Godown
        TxtProcess.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Process
        TxtJobWorker.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobWorker
        TxtJobOrderNo.AgHelpDataSet(5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobOrder

        Dgl1.AgHelpDataSet(Col1JobOrderDocID) = HelpDataSet.JobOrder
        Dgl1.AgHelpDataSet(Col1JobIssueDocId) = HelpDataSet.JobIssue
        Dgl1.AgHelpDataSet(Col1ProdOrder) = HelpDataSet.ProdOrder
        Dgl1.AgHelpDataSet(Col1BOM) = HelpDataSet.BOM

        TxtStructure.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.AgStructure
        IniItemHelpList()


        '=============Bom Detail Ini List===============
        Dgl2.AgHelpDataSet(Col2Item, 12) = HelpDataSet.Item
        Dgl2.AgHelpDataSet(Col2BOMItem, 12) = HelpDataSet.Item
        Dgl2.AgHelpDataSet(Col2StockItem, 12) = HelpDataSet.Item

        Dgl2.AgHelpDataSet(Col2ProdOrder) = HelpDataSet.ProdOrder
        Dgl2.AgHelpDataSet(Col2JobOrder) = HelpDataSet.JobOrder

        '=============ByProduct Detail Ini List===============
        Dgl3.AgHelpDataSet(Col1Item, 12) = HelpDataSet.Item
    End Sub

    Protected Sub IniItemHelpList()
        Try
            If AgL.StrCmp(mJobReceiveFor, ClsMain.JobReceiveFor.JobOrder) Then
                Dgl1.AgHelpDataSet(Col1Item, 18) = HelpDataSet.ItemFromJobOrder
            Else
                Dgl1.AgHelpDataSet(Col1Item, 16) = HelpDataSet.ItemFromJobIssue
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl2.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl3.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub


    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                If AgL.StrCmp(Topctrl1.Mode, "Add") Then
                    Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0  " & _
                    " And Status='" & ClsMain.EntryStatus.Active & "' " & _
                    " And BalanceQty > 0 " & _
                    " And JobWorker = '" & TxtJobWorker.AgSelectedValue & "'"
                Else
                    Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0  " & _
                        " And Status='" & ClsMain.EntryStatus.Active & "' " & _
                        " And JobWorker = '" & TxtJobWorker.AgSelectedValue & "'"
                End If
                
        End Select
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
                    Dgl1.Item(Col1Qty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1DocQty, mRowIndex).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    Dgl1.Item(Col1BillQty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1DocQty, mRowIndex).Value), "0.".PadRight(CType(Dgl1.Columns(Col1BillQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                Case Col1DocQty
                    Dgl1.Item(Col1Qty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1DocQty, mRowIndex).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    Dgl1.Item(Col1BillQty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1DocQty, mRowIndex).Value), "0.".PadRight(CType(Dgl1.Columns(Col1BillQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
            End Select
            If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Dgl2_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl2.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex
            If Dgl2.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl2.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case Col2Item
                    Validating_BomMaterial(Dgl2.AgSelectedValue(Col2Item, mRowIndex), mRowIndex)
            End Select
            If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl3_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl3.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl3.CurrentCell.RowIndex
            mColumnIndex = Dgl3.CurrentCell.ColumnIndex
            If Dgl3.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl3.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl3.Columns(Dgl3.CurrentCell.ColumnIndex).Name
                Case Col3Item
                    Validating_ByProduct(Dgl3.AgSelectedValue(Col3Item, mRowIndex), mRowIndex)
            End Select
            If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then Call Calculation()
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
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.AgSelectedValue(Col1JobOrderDocID, mRow) = ""
                Dgl1.AgSelectedValue(Col1JobIssueDocId, mRow) = ""
                Dgl1.AgSelectedValue(Col1BOM, mRow) = ""
                Dgl1.Item(Col1FillQcParameter, mRow).Tag = Nothing
                Dgl1.Item(Col1JobWithMaterialYN, mRow).Value = 0
                Dgl1.Item(Col1Rate, mRow).Value = 0
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    If AgL.StrCmp(Topctrl1.Mode, "Add") Then
                        Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalanceQty").Value)
                        Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalanceQty").Value)
                    End If
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Measure").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.AgSelectedValue(Col1JobOrderDocID, mRow) = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrder").Value)
                    Dgl1.AgSelectedValue(Col1JobIssueDocId, mRow) = AgL.XNull(Dgl1.AgDataRow.Cells("JobIssueDocId").Value)
                    Dgl1.AgSelectedValue(Col1ProdOrder, mRow) = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Dgl1.AgSelectedValue(Col1BOM, mRow) = AgL.XNull(Dgl1.AgDataRow.Cells("BOM").Value)
                    Dgl1.Item(Col1JobWithMaterialYN, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("JobWithMaterialYN").Value)
                    If Dgl1.Item(Col1FillQcParameter, mRow).Tag Is Nothing Then ProcFillQcParameterDetail(mRow)
                    AgCalcGrid1.FCopyStructureLine(AgL.XNull(Dgl1.AgDataRow.Cells("JobOrder").Value), Dgl1, mRow, AgL.XNull(Dgl1.AgDataRow.Cells("LineSr").Value))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub DGL2_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl2.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub DGL3_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl3.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalRecQty.Text = 0
        LblTotalRecMeasure.Text = 0
        LblCTotalQty.Text = 0 : LblCTotalMeasure.Text = 0
        LblTotalByProductQty.Text = 0 : LblTotalByProductMeasure.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1LossPer, I).Value) > 0 Then
                    Dgl1.Item(Col1LossQty, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1LossPer, I).Value) / 100, "0.".PadRight(CType(Dgl1.Columns(Col1LossQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If
                Dgl1.Item(Col1DocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1DocMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Dgl1.Item(Col1RetMeasure, I).Value = Format(Val(Dgl1.Item(Col1RetQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1RetMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                Dgl1.Item(Col1BillMeasure, I).Value = Format(Val(Dgl1.Item(Col1BillQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1BillMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                If AgL.StrCmp(TxtBillingOn.Text, "Qty") Or TxtBillingOn.Text = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1BillQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1BillMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If




                LblTotalRecQty.Text = Val(LblTotalRecQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalRecMeasure.Text = Val(LblTotalRecMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)

            End If
        Next
        LblTotalRecQty.Text = Val(LblTotalRecQty.Text)
        LblTotalRecMeasure.Text = Val(LblTotalRecMeasure.Text)



        '=============Calculation of BOM Detail   =================
        For I = 0 To Dgl2.RowCount - 1
            Dgl2.Item(Col2TotalMeasure, I).Value = Format(Val(Dgl2.Item(Col2Qty, I).Value) * Val(Dgl2.Item(Col2MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col2TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
            LblCTotalQty.Text = Val(LblCTotalQty.Text) + Val(Dgl2.Item(Col2Qty, I).Value)
            LblCTotalMeasure.Text = Val(LblCTotalMeasure.Text) + Val(Dgl2.Item(Col2TotalMeasure, I).Value)
        Next
        LblCTotalQty.Text = Val(LblCTotalQty.Text)
        LblCTotalMeasure.Text = Val(LblCTotalMeasure.Text)


        '=============Calculation of ByProduct Detail   =================
        For I = 0 To Dgl3.RowCount - 1
            If Dgl3.Item(Col3Item, I).Value <> "" Then
                Dgl3.Item(Col3TotalMeasure, I).Value = Format(Val(Dgl3.Item(Col3Qty, I).Value) * Val(Dgl3.Item(Col3MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col3TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Dgl3.Item(Col3Amount, I).Value = Format(Val(Dgl3.Item(Col3Qty, I).Value) * Val(Dgl3.Item(Col3Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col3Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'Footer Calculation
                LblTotalByProductQty.Text = Val(LblTotalByProductQty.Text) + Val(Dgl3.Item(Col3Qty, I).Value)
                LblTotalByProductMeasure.Text = Val(LblTotalByProductMeasure.Text) + Val(Dgl3.Item(Col3TotalMeasure, I).Value)
            End If
        Next

        LblTotalByProductQty.Text = Val(LblTotalByProductQty.Text)
        LblTotalByProductMeasure.Text = Val(LblTotalByProductMeasure.Text)


        AgCalcGrid1.Calculation()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim DrTemp() As DataRow = Nothing

        If AgL.RequiredField(TxtGodown, LblGodown.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtManualRefNo, LblManualRefNo.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub

        passed = FCheckDuplicateRefNo()

        With Dgl1
            For I = 0 To .Rows.Count - 1
                'If .Item(Col1Item, I).Value <> "" Then
                '    If Val(.Item(Col1Qty, I).Value) = 0 Then
                '        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                '        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                '        passed = False : Exit Sub
                '    End If
                'End If

                'If JobReceiveFor = ClsMain.JobReceiveFor.JobOrder Then
                '    DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Dgl1.AgSelectedValue(Col1Item, I) & "' And JobOrder = '" & Dgl1.AgSelectedValue(Col1JobOrderDocID, I) & "'")
                'Else
                '    DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Dgl1.AgSelectedValue(Col1Item, I) & "' And JobIssueDocId = '" & Dgl1.AgSelectedValue(Col1JobIssueDocId, I) & "'")
                'End If
                'If Val(.Item(Col1Qty, I).Value) > 0 Then
                '    If AgL.VNull(Dgl1.Item(Col1Qty, I).Value) > AgL.VNull(DrTemp(0)("BalanceQty")) Then
                '        If MsgBox("Quantity Is Greater Than Balance Qty At Row No" & Dgl1.Item(ColSNo, I).Value & ".Do You Want To Continue.", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then
                '            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                '            passed = False : Exit Sub
                '        End If
                '    End If
                'End If
            Next
        End With
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then

            mQry = " SELECT COUNT(*) FROM JobIssRec_Log WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " & _
                        " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And EntryStatus <> 'Discard' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then TxtManualRefNo.Text = ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, ClsMain.ManualRefType.Max) : MsgBox("Reference No. Already Exists New Reference No. Alloted : " & TxtManualRefNo.Text)


            'mQry = " SELECT COUNT(*) FROM JobIssRec WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " & _
            '        " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0  "
            'If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtV_Date.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM JobIssRec WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'  " & _
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtGodown.Enter, TxtJobWorker.Enter, TxtJobOrderNo.Enter
        Try
            Select Case sender.name
                Case TxtGodown.Name
                    TxtGodown.AgRowFilter = " Div_Code = '" & AgL.PubDivCode & "' " & _
                        " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                        " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " & _
                        " And IsDeleted = 0 "

                Case TxtJobWorker.Name
                    TxtJobWorker.AgRowFilter = " IsDeleted = 0 " & _
                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " AND Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                            " AND Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " & _
                            " And Process = '" & TxtProcess.AgSelectedValue & "' "

                Case TxtJobOrderNo.Name
                    TxtJobOrderNo.AgRowFilter = " IsDeleted = 0 " & _
                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " And " & AgTemplate.ClsMain.RetDivFilterStr() & " " & _
                            " And Process = '" & TxtProcess.AgSelectedValue & "' " & _
                            " And JobWorker = '" & TxtJobWorker.AgSelectedValue & "'"

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()


        LblCTotalQty.Text = 0 : LblCTotalMeasure.Text = 0
        LblTotalRecMeasure.Text = 0 : LblTotalRecQty.Text = 0
        LblTotalByProductQty.Text = 0 : LblTotalByProductMeasure.Text = 0
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim I As Integer, mSr As Integer
        Dim Stock As ClsMain.StructStock = Nothing, StockProcess As ClsMain.StructStock = Nothing
        Dim StructDues As ClsMain.Dues = Nothing, StructToBeBiiled As ClsMain.ToBeBilled = Nothing

        Call ProcUpDateJobOrder(SearchCode, Conn, Cmd)

        mQry = "Delete From Stock Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
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
                    .Godown = TxtGodown.AgSelectedValue
                    .Qty_Rec = Dgl1.Item(Col1Qty, I).Value
                    .LotNo = Dgl1.Item(Col1LotNo, I).Value
                    .Unit = Dgl1.Item(Col1Unit, I).Value
                    .MeasurePerPcs = Dgl1.Item(Col1MeasurePerPcs, I).Value
                    .Measure_Rec = Dgl1.Item(Col1TotalMeasure, I).Value
                    .MeasureUnit = Dgl1.Item(Col1MeasureUnit, I).Value
                    .Status = ClsMain.StockStatus.Standard
                    .Rate = Dgl1.Item(Col1Rate, I).Value
                    .Amount = Dgl1.Item(Col1Amount, I).Value
                    .Process = TxtProcess.AgSelectedValue
                End With
                Call ClsMain.ProcStockPost("Stock", Stock, Conn, Cmd)

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
                    .Qty_Iss = Dgl1.Item(Col1Qty, I).Value
                    .LotNo = Dgl1.Item(Col1LotNo, I).Value
                    .Unit = Dgl1.Item(Col1Unit, I).Value
                    .MeasurePerPcs = Dgl1.Item(Col1MeasurePerPcs, I).Value
                    .Measure_Iss = Dgl1.Item(Col1TotalMeasure, I).Value
                    .MeasureUnit = Dgl1.Item(Col1MeasureUnit, I).Value
                    .Status = ClsMain.StockStatus.Standard
                    .Process = TxtProcess.AgSelectedValue
                    .Rate = Dgl1.Item(Col1Rate, I).Value
                    .Amount = Dgl1.Item(Col1Amount, I).Value
                End With
                Call ClsMain.ProcStockPost("StockProcess", StockProcess, Conn, Cmd)
            End If
        Next

        If BillPosting = ClsMain.JobReceiveBillPosting.Dues_JobOrderWise Then
            FPost_JobOrderWiseDue(Conn, Cmd)
        End If

        If BillPosting = ClsMain.JobReceiveBillPosting.Dues Then
            With StructDues
                .UID = mSearchCode
                .DocID = mInternalCode
                .Sr = 1
                .V_Type = TxtV_Type.AgSelectedValue
                .V_Prefix = LblPrefix.Text
                .V_Date = TxtV_Date.Text
                .V_No = Val(TxtV_No.Text)
                .Div_Code = TxtDivision.AgSelectedValue
                .Site_Code = TxtSite_Code.AgSelectedValue
                .CashCredit = ""
                .SubCode = TxtJobWorker.AgSelectedValue
                .Narration = TxtRemarks.Text
                .ReferenceDocID = mInternalCode 'IIf(Dgl1.AgSelectedValue(Col1JobOrderDocID, I) <> "", Dgl1.AgSelectedValue(Col1JobOrderDocID, I), Dgl1.AgSelectedValue(Col1JobIssueDocId, I))
                .RefV_Type = ""
                .RefV_No = 0
                .RefPartyName = TxtJobWorker.Text
                .RefPartyAddress = ""
                .RefPartyCity = ""
                .PaybleAmount = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
                .ReceivableAmount = 0
                .AdjustedAmount = 0
                .EntryBy = TxtEntryBy.Text
                .EntryDate = AgL.GetDateTime(AgL.GcnRead)
                .EntryType = TxtEntryType.Text
                .EntryStatus = LogStatus.LogOpen
                .ApproveBy = TxtApproveBy.Text
                .ApproveDate = ""
                .MoveToLog = ""
                .MoveToLogDate = ""
                .IsDeleted = 0
                .Status = TxtStatus.Text
                Call ClsMain.ProcGetPartyAddress(TxtJobWorker.AgSelectedValue, .RefPartyAddress, .RefPartyCity, AgL.GcnRead)
                Call ProcGetVType(.ReferenceDocID, .RefV_Type, .RefV_No, AgL.GcnRead)
            End With
            Call ClsMain.ProcPostInDues(Conn, Cmd, StructDues)
        End If

        If BillPosting = ClsMain.JobReceiveBillPosting.ToBeBilled Then
            With StructToBeBiiled
                .UID = mSearchCode
                .DocID = mInternalCode
                .V_Type = TxtV_Type.AgSelectedValue
                .V_Prefix = LblPrefix.Text
                .V_Date = TxtV_Date.Text
                .V_No = Val(TxtV_No.Text)
                .Div_Code = TxtDivision.AgSelectedValue
                .Site_Code = TxtSite_Code.AgSelectedValue
                .ReferenceNo = mInternalCode 'IIf(Dgl1.Item(Col1JobOrderDocID, I).Value <> "", Dgl1.Item(Col1JobOrderDocID, I).Value, Dgl1.Item(Col1JobIssueDocId, I).Value)
                .SubCode = TxtJobWorker.AgSelectedValue
                .PartyName = TxtJobWorker.Text
                .PartyAddress = ""
                .PartyCity = ""
                .TotalQty = Val(LblTotalRecQty.Text)
                .ReceivableAmount = 0
                .PaybleAmount = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
                .AdjustedAmount = 0
                .BilledAmount = 0
                .PaidAmount = 0
                .EntryBy = TxtEntryBy.Text
                .EntryDate = AgL.GetDateTime(AgL.GcnRead)
                .EntryType = TxtEntryType.Text
                .EntryStatus = LogStatus.LogOpen
                .ApproveBy = TxtApproveBy.Text
                .ApproveDate = ""
                .MoveToLog = ""
                .MoveToLogDate = ""
                .IsDeleted = 0
                .Status = TxtStatus.Text
                Call ClsMain.ProcGetPartyAddress(TxtJobWorker.AgSelectedValue, .PartyAddress, .PartyCity, AgL.GcnRead)
            End With
            Call ClsMain.ProcPostInToBeBilled(Conn, Cmd, StructToBeBiiled)
        End If

        'Dim StockProcess As AgTemplate.ClsMain.StructStock = Nothing

        'mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With StockProcess
            For I = 0 To Dgl2.Rows.Count - 1
                If Dgl2.Item(Col2Item, I).Value <> "" Then
                    mSr += 1
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
                    .Item = Dgl2.AgSelectedValue(Col2StockItem, I)
                    .Godown = TxtGodown.AgSelectedValue
                    .Qty_Iss = Dgl2.Item(Col2Qty, I).Value
                    .LotNo = Dgl2.Item(Col2LotNo, I).Value
                    .Unit = Dgl2.Item(Col2Unit, I).Value
                    .MeasurePerPcs = Dgl2.Item(Col2MeasurePerPcs, I).Value
                    .Measure_Iss = Dgl2.Item(Col2TotalMeasure, I).Value
                    .MeasureUnit = Dgl2.Item(Col2MeasureUnit, I).Value
                    .Status = AgTemplate.ClsMain.StockStatus.Standard
                    .Process = mPrevProcess
                    Call AgTemplate.ClsMain.ProcStockPost("StockProcess", StockProcess, Conn, Cmd)
                End If
            Next
        End With

        Dim bTableName$ = ""

        'mQry = "Delete From Stock Where DocId = '" & mInternalCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Stock
            For I = 0 To Dgl3.Rows.Count - 1
                If Dgl3.Item(Col3Item, I).Value <> "" Then
                    mSr += 1
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
                    .Item = Dgl3.AgSelectedValue(Col3StockItem, I)
                    .Godown = TxtGodown.AgSelectedValue
                    .Qty_Rec = Dgl3.Item(Col3Qty, I).Value
                    .LotNo = Dgl3.Item(Col3LotNo, I).Value
                    .Unit = Dgl3.Item(Col3Unit, I).Value
                    .MeasurePerPcs = Dgl3.Item(Col3MeasurePerPcs, I).Value
                    .Measure_Rec = Dgl3.Item(Col3TotalMeasure, I).Value
                    .MeasureUnit = Dgl3.Item(Col3MeasureUnit, I).Value
                    .Status = AgTemplate.ClsMain.StockStatus.Standard
                    .Process = TxtProcess.AgSelectedValue
                    bTableName = IIf(AgL.StrCmp(Dgl3.Item(Col3IsDelivered, I).Value, "Yes"), "Stock", "StockProcess")
                    Call AgTemplate.ClsMain.ProcStockPost(bTableName, Stock, Conn, Cmd)
                End If
            Next
        End With
    End Sub


    Private Sub FPost_JobOrderWiseDue(ByRef Conn As SqlClient.SqlConnection, ByRef Cmd As SqlClient.SqlCommand)
        Dim StructDues As ClsMain.Dues = Nothing
        Dim DtTemp As DataTable
        Dim mSr As Integer, I As Integer


        mQry = "SELECT L.JobOrder, L.DocId, Sum(NetAmount) AS NetAmount  FROM JobReceiveDetail  L With (NoLock) " & _
               " WHERE DocID ='" & mInternalCode & "' " & _
               " GROUP BY L.JobOrder, L.DocId "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)


        For I = 0 To DtTemp.Rows.Count - 1
            mSr += 1
            With StructDues
                .UID = mSearchCode
                .DocID = mInternalCode
                .Sr = mSr
                .V_Type = TxtV_Type.AgSelectedValue
                .V_Prefix = LblPrefix.Text
                .V_Date = TxtV_Date.Text
                .V_No = Val(TxtV_No.Text)
                .Div_Code = TxtDivision.AgSelectedValue
                .Site_Code = TxtSite_Code.AgSelectedValue
                .CashCredit = ""
                .SubCode = TxtJobWorker.AgSelectedValue
                .Narration = Dgl1.Item(Col1Remark, I).Value
                .ReferenceDocID = AgL.XNull(DtTemp.Rows(I)("JobOrder")) 'IIf(Dgl1.AgSelectedValue(Col1JobOrderDocID, I) <> "", Dgl1.AgSelectedValue(Col1JobOrderDocID, I), Dgl1.AgSelectedValue(Col1JobIssueDocId, I))
                .RefV_Type = ""
                .RefV_No = 0
                .RefPartyName = TxtJobWorker.Text
                .RefPartyAddress = ""
                .RefPartyCity = ""
                .PaybleAmount = AgL.VNull(DtTemp.Rows(I)("NetAmount"))    'Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, I, AgStructure.AgCalcGrid.LineColumnType.Amount))
                .ReceivableAmount = 0
                .AdjustedAmount = 0
                .EntryBy = TxtEntryBy.Text
                .EntryDate = AgL.GetDateTime(AgL.GcnRead)
                .EntryType = TxtEntryType.Text
                .EntryStatus = LogStatus.LogOpen
                .ApproveBy = TxtApproveBy.Text
                .ApproveDate = ""
                .MoveToLog = ""
                .MoveToLogDate = ""
                .IsDeleted = 0
                .Status = TxtStatus.Text
                Call ClsMain.ProcGetPartyAddress(TxtJobWorker.AgSelectedValue, .RefPartyAddress, .RefPartyCity, AgL.GcnRead)
                Call ProcGetVType(.ReferenceDocID, .RefV_Type, .RefV_No, AgL.GcnRead)
            End With
            Call ClsMain.ProcPostInDues(Conn, Cmd, StructDues)

        Next
    End Sub


    Private Sub TempJobOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Call ProcUpDateJobOrder(SearchCode, Conn, Cmd)

        mQry = "Delete From Stock Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub ProcUpDateJobOrder(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand)
        Dim I As Integer = 0
        Dim bTableName$ = "", bDocId$ = ""


        If AgL.StrCmp(mJobReceiveFor, ClsMain.JobReceiveFor.JobOrder) Then
            bTableName = "JobOrderDetail"
            bDocId = Dgl1.AgSelectedValue(Col1JobOrderDocID, I)
        Else
            bTableName = "JobIssueDetail"
            bDocId = Dgl1.AgSelectedValue(Col1JobIssueDocId, I)
        End If


        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsMain.Tables(0).Rows.Count - 1
                    mQry = " UPDATE " & bTableName & " " & _
                            " SET ReceiveQty = (SELECT IsNull(Sum(L.Qty),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
                            "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' And IsNull(H.IsDeleted,0)=0), " & _
                            " ReceiveMeasure = (SELECT IsNull(Sum(L.TotalMeasure),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
                            "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' And IsNull(H.IsDeleted,0)=0), " & _
                            " ReceiveLoss = (SELECT IsNull(Sum(L.LossQty),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
                            "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' And IsNull(H.IsDeleted,0)=0) " & _
                            " Where DocId = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
                            " And Item = '" & AgL.XNull(.Rows(I)("Item")) & ") '  And IsNull(ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Next
            End If
        End With


        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mQry = " UPDATE " & bTableName & " " & _
                            " SET ReceiveQty = (SELECT IsNull(Sum(L.Qty),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
                            "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrderDocID, I) & "' " & _
                            " 				   And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "'  And IsNull(H.IsDeleted,0)=0), " & _
                            " ReceiveMeasure = (SELECT IsNull(Sum(L.TotalMeasure),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
                            "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrderDocID, I) & "' " & _
                            " 				   And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' And IsNull(H.IsDeleted,0)=0), " & _
                            " ReceiveLoss = (SELECT IsNull(Sum(L.LossQty),0)  " & _
                            " 				   FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H On L.DocId  = H.DocId " & _
                            "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
                            "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrderDocID, I) & "' " & _
                            " 				   And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' And IsNull(H.IsDeleted,0)=0) " & _
                            " Where DocId = '" & .AgSelectedValue(Col1JobOrderDocID, I) & "' " & _
                            " And Item = '" & .AgSelectedValue(Col1Item, I) & "'   And IsNull(ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = "UPDATE JobOrder " & _
                            " SET " & _
                            " LastReceiveDate = (SELECT TOP 1 H.V_Date  " & _
                            "                  FROM JobReceiveDetail L " & _
                            "                  LEFT JOIN JobIssRec H ON H.DocID = L.DocId " & _
                            "                  WHERE L.JobOrder = '" & .AgSelectedValue(Col1JobOrderDocID, I) & "'  " & _
                            "                  ORDER BY H.V_Date DESC) " & _
                            " Where DocId = '" & .AgSelectedValue(Col1JobOrderDocID, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = " UPDATE MaterialPlanDetail " & _
                            " SET JobRecQty  = ( " & _
                            " 	    SELECT IsNull(Sum(L.Qty),0)    " & _
                            " 	    FROM JobReceiveDetail L With (NoLock)  " & _
                            " 	    LEFT JOIN JobIssRec H  With (NoLock) On L.DocId  = H.DocId   " & _
                            " 	    LEFT JOIN Voucher_Type Vt  With (NoLock)  On H.V_Type = Vt.V_Type   " & _
                            " 	    WHERE Vt.NCat = '" & EntryNCat & "'    " & _
                            " 	    And L.ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                            " 	    And IsNull(H.IsDeleted,0) = 0  " & _
                            " 	    And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                            "       ) " & _
                            " WHERE ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                            " AND Item = '" & .AgSelectedValue(Col1Item, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Sub

    Private Sub TempJobReceive_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = "SELECT G.Code, G.Description, Sm.ManualCode As Site, G.Site_Code, G.Div_Code, IsNull(G.IsDeleted,0) as IsDeleted, " & _
                " IsNull(G.Status,'" & ClsMain.EntryStatus.Active & "') AS Status " & _
                " FROM Godown G " & _
                " LEFT JOIN SiteMast Sm On G.Site_Code = Sm.Code " & _
                " Order By G.Description"
        HelpDataSet.Godown = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select P.NCat As Code, Vc.NCatDescription As Process, P.Div_Code " & _
                " From Process P " & _
                " LEFT JOIN VoucherCat Vc On P.NCat  = Vc.NCat " & _
                " Order By Vc.NCatDescription "
        HelpDataSet.Process = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Po.DocId AS Code, " & _
                " ManualRefNo As [Prod.Order No. Manual], PO.V_Type + '-' + Convert(nVarchar,PO.V_No) as [Prod.Order No.]  " & _
                " FROM ProdOrder Po "
        HelpDataSet.ProdOrder = AgL.FillData(mQry, AgL.GCn)


        'mQry = "SELECT S.SubCode AS Code, S.DispName  AS JobWorker " & _
        '      " FROM JobWorker J " & _
        '      " LEFT JOIN SubGroup S ON J.SubCode = S.SubCode "

        mQry = " SELECT J.SubCode AS Code, Sg.Name AS JobWorker, H.Process, " & _
              " IsNull(Sg.IsDeleted,0) AS IsDeleted, SG.Div_Code, SG.Site_Code, " & _
              " IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
              " FROM JobWorker J " & _
              " LEFT JOIN JobWorkerProcess H On J.SubCode = H.SubCode  " & _
              " LEFT JOIN SubGroup Sg ON J.SubCode = Sg.SubCode "

        HelpDataSet.JobWorker = AgL.FillData(mQry, AgL.GCn)

        'mQry = "SELECT J.DocId, J.ManualRefNo  " & _
        '        " FROM JobOrder J " & _
        '        " LEFT JOIN Voucher_Type Vt ON J.V_Type = Vt.V_Type "
        'HelpDataSet.JobOrder = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.DocID AS Code, H.ManualRefNo FROM JobIssRec H "
        HelpDataSet.JobIssue = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Description  FROM Structure ORDER BY Description "
        HelpDataSet.AgStructure = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT I.Code AS Code, I.Description AS Item, H.ManualRefNo As JobOrderNo, " & _
                " H.V_Date As JobOrderDate, H.DueDate, " & _
                " IsNull(L.Qty,0) - (IsNull(L.ReceiveQty,0)+IsNull(L.CancelQty,0)+IsNull(L.ReceiveLoss,0)) AS BalanceQty, H.Remarks, " & _
                " '' As JobIssueDocId, L.DocId AS JobOrder, I.ItemType, I.SalesTaxPostingGroup,  " & _
                " L.Unit, L.MeasurePerPcs AS Measure, L.MeasureUnit, L.Rate, I.BOM,  " & _
                " IsNull(H.IsDeleted,0) AS IsDeleted, " & _
                " IsNull(H.Status,'" & ClsMain.EntryStatus.Active & "') AS Status, " & _
                " H.Div_Code, Vt.NCat, H.JobWorker, L.ProdOrder, " & _
                " L.Sr As LineSr, IsNull(H.JobWithMaterialYN,0) As JobWithMaterialYN, H.Remarks " & _
                " FROM JobOrderDetail L  " & _
                " LEFT JOIN Item I ON L.Item = I.Code " & _
                " LEFT JOIN JobOrder H ON L.DocId = H.DocID " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
        HelpDataSet.ItemFromJobOrder = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT I.Code, I.Description AS Item, H.ManualRefNo As JobIssueNo, " & _
                " H.V_Date As JobIssueDate, H.DueDate, " & _
                " IsNull(L.Qty,0) - IsNull(L.ReceiveQty,0)  AS BalanceQty, H.Remarks, " & _
                " L.DocId AS JobIssueDocId, '' As JobOrder, I.ItemType, " & _
                " I.SalesTaxPostingGroup, L.Unit, L.MeasurePerPcs As Measure, I.BOM,    " & _
                " L.MeasureUnit,  IsNull(H.IsDeleted ,0) AS IsDeleted,   " & _
                " IsNull(H.Status,'" & ClsMain.EntryStatus.Active & "') AS Status,  " & _
                " H.Div_Code, Vt.NCat, H.JobWorker, Null as ProdOrder, I.Rate, " & _
                " L.Sr as LineSr, 0 As JobWithMaterialYN" & _
                " FROM JobIssueDetail L  " & _
                " LEFT JOIN Item I ON L.Item = I.Code " & _
                " LEFT JOIN JobIssRec H ON L.DocId = H.DocID " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type   "
        HelpDataSet.ItemFromJobIssue = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT  H.Code, H.Description, H.Unit, H.ItemType, IsNull(H.IsDeleted,0) As IsDeleted, " & _
                " H.UpcCode, H.Bom, H.Status, '' AS JobIssueDocId, '' As JobOrder, " & _
                " H.Div_Code, H.SalesTaxPostingGroup, H.Measure, H.MeasureUnit, " & _
                " H.ItemGroup, H.Rate " & _
                " FROM Item H "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code, H.Description AS BOM FROM BOM H  "
        HelpDataSet.BOM = AgL.FillData(mQry, AgL.GCn)
        mQry = "SELECT H.DocID AS Code, H.ManualRefNo AS JobOrderNo , " & _
                " IsNull(H.IsDeleted,0) AS IsDeleted, " & _
                " IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, " & _
                " H.Div_Code, Vt.NCat As Process, H.JobWorker " & _
                " FROM JobOrder H " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
        HelpDataSet.JobOrder = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub ProcFillQcParameterDetail(ByVal RowIndex As Integer)
        Dim FrmObj As Form = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            FrmObj = New FrmJobQcDetail
            Call CType(FrmObj, FrmJobQcDetail).IniGrid()

            If Dgl1.AgSelectedValue(Col1JobOrderDocID, RowIndex) <> "" Then
                mQry = " SELECT H.Parameter, H.StdValue " & _
                        " FROM JobOrderQCInstruction H " & _
                        " WHERE H.DocId = '" & Dgl1.AgSelectedValue(Col1JobOrderDocID, RowIndex) & "'  "
            Else
                mQry = "SELECT Qd.Parameter, Qd.StdValue " & _
                        " FROM Process P " & _
                        " LEFT JOIN QcGroup Q ON P.QcGroup  = Q.Code " & _
                        " LEFT JOIN QcGroupDetail Qd ON Q.Code = Qd.Code " & _
                        " WHERE P.NCat = '" & TxtProcess.AgSelectedValue & "' "
            End If
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With CType(FrmObj, FrmJobQcDetail).Dgl1
                .RowCount = 1 : .Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    For I = 0 To DtTemp.Rows.Count - 1
                        .Rows.Add()
                        .Item(ColSNo, I).Value = .Rows.Count
                        .Item(FrmJobQcDetail.Col1Parameter, I).Value = AgL.XNull(DtTemp.Rows(I)("Parameter"))
                        .Item(FrmJobQcDetail.Col1StdValue, I).Value = AgL.XNull(DtTemp.Rows(I)("StdValue"))
                        .Item(FrmJobQcDetail.Col1ActValue, I).Value = AgL.XNull(DtTemp.Rows(I)("StdValue"))
                        .Item(FrmJobQcDetail.Col1Qty, I).Value = Val(Dgl1.Item(Col1Qty, RowIndex).Value)
                        .Item(FrmJobQcDetail.Col1PassedQty, I).Value = Val(Dgl1.Item(Col1Qty, RowIndex).Value)
                        .Item(FrmJobQcDetail.Col1Unit, I).Value = Dgl1.Item(Col1Unit, RowIndex).Value
                        .Item(FrmJobQcDetail.Col1MeasurePerPcs, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, RowIndex).Value)
                        .Item(FrmJobQcDetail.Col1MeasureUnit, I).Value = Dgl1.Item(Col1MeasureUnit, RowIndex).Value
                        .Item(FrmJobQcDetail.Col1TotalMeasure, I).Value = Val(Dgl1.Item(Col1TotalMeasure, RowIndex).Value)
                    Next I
                End If
            End With
            CType(FrmObj, FrmJobQcDetail).LblItemName.Text = Dgl1.Item(Col1Item, RowIndex).Value
            'CType(FrmObj, FrmJobQcDetail).LblQty.Text = Dgl1.Item(Col1Qty, RowIndex).Value
            'CType(FrmObj, FrmJobQcDetail).LblQcQty.Text = Dgl1.Item(Col1Qty, RowIndex).Value
            Dgl1.Item(Col1FillQcParameter, RowIndex).Tag = FrmObj
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim FrmObj As FrmJobQcDetail = Nothing
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            If Dgl1.Item(Col1Item, bRowIndex).Value = "" Then Exit Sub
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1FillQcParameter
                    FrmObj = Dgl1.Item(Col1FillQcParameter, bRowIndex).Tag
                    FrmObj.FrmReadonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
                    FrmObj.LblQty.Text = Dgl1.Item(Col1Qty, bRowIndex).Value
                    FrmObj.LblQcQty.Text = Dgl1.Item(Col1Qty, bRowIndex).Value
                    FrmObj.ShowDialog()
                    If FrmObj.mOkButtonPressed Then
                        Dgl1.Item(Col1FillQcParameter, bRowIndex).Tag = FrmObj
                    End If
            End Select
            If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then Call Calculation()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ProcGetVType(ByVal DocId As String, ByRef V_Type As String, ByRef V_No As Long, ByVal Conn As SqlClient.SqlConnection)
        Dim DtTemp As DataTable = Nothing
        Dim bTable As String = ""
        Try
            If JobReceiveFor = ClsMain.JobReceiveFor.JobOrder Then
                bTable = "JobOrder"
            Else
                bTable = "JobIssRec"
            End If
            mQry = " SELECT H.V_Type, H.V_No FROM JobOrder H With (NoLock) WHERE H.DocID = '" & DocId & "' "
            DtTemp = AgL.FillData(mQry, Conn).Tables(0)
            With DtTemp
                If .Rows.Count > 0 Then
                    V_Type = AgL.XNull(DtTemp.Rows(0)("V_Type"))
                    V_No = AgL.VNull(DtTemp.Rows(0)("V_No"))
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempJobReceive_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        ' DsMain.Clear()
    End Sub

    Private Sub TempJobReceive_BaseEvent_ApproveDeletion_PreTrans(ByVal SearchCode As String) Handles Me.BaseEvent_ApproveDeletion_PreTrans
        mQry = " SELECT H.DocId,L.Item,L.Qty,L.JobOrder,L.TotalMeasure,L.ProdOrder  " & _
        " FROM JobIssRec H With (Nolock) " & _
        " LEFT JOIN JobReceiveDetail L With (Nolock) ON H.DocID=L.DocId  " & _
        " WHERE H.DocID = " & AgL.Chk_Text(mInternalCode) & " "
        DsMain = AgL.FillData(mQry, AgL.GcnRead)
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

    Protected Sub Validating_BomMaterial(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl2.Item(Col2Item, mRow).Value.ToString.Trim = "" Or Dgl2.AgSelectedValue(Col2Item, mRow).ToString.Trim = "" Then
                Dgl2.Item(Col2Unit, mRow).Value = ""
                Dgl2.Item(Col2MeasurePerPcs, mRow).Value = 0
                Dgl2.Item(Col2MeasureUnit, mRow).Value = ""
                Dgl2.AgSelectedValue(Col2StockItem, mRow) = ""
            Else
                If Dgl2.AgHelpDataSet(Col2Item) IsNot Nothing Then
                    DrTemp = Dgl2.AgHelpDataSet(Col2Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl2.Item(Col2Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl2.Item(Col2MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("Measure"))
                    Dgl2.Item(Col2MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl2.AgSelectedValue(Col2StockItem, mRow) = Dgl2.AgSelectedValue(Col2Item, mRow)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub


    Private Sub TempJobOrder_BaseFunction_ConsumptionGridFill() Handles Me.BaseFunction_ConsumptionGridFill
        Dim I As Integer = 0
        Dim bQry$ = ""
        Dim DsTemp As DataSet = Nothing
        Dim bTempTable$ = ""
        Try
            bTempTable = AgL.GetGUID(AgL.GCn).ToString
            mQry = "CREATE TABLE [#" & bTempTable & "] " & _
                    " (ProdOrder nVarChar(21), JobOrder nVarChar(21), Item NVARCHAR(10), " & _
                    " BOMItem NVARCHAR(10), TotalConsumptionQty Float)  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Value <> "" Then
                        mQry = "INSERT INTO [#" & bTempTable & "](ProdOrder, JobOrder, " & _
                                " Item, BOMItem, TotalConsumptionQty) " & _
                                " SELECT " & AgL.Chk_Text(.AgSelectedValue(Col1ProdOrder, I)) & " As ProdOrder, " & _
                                " " & AgL.Chk_Text(.AgSelectedValue(Col1JobOrderDocID, I)) & " As JobOrder, " & _
                                " " & AgL.Chk_Text(.AgSelectedValue(Col1Item, I)) & " As Item, " & _
                                " Bd.Item As BOMItem, Bd.Qty * " & Val(.Item(Col1DocQty, I).Value) & " " & _
                                " FROM Bom B  " & _
                                " LEFT JOIN BomDetail Bd ON B.Code = Bd.Code " & _
                                " WHERE B.Code = '" & Dgl1.AgSelectedValue(Col1BOM, I) & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If
                Next
            End With

            mQry = "SELECT T.ProdOrder, T.JobOrder, T.Item, T.BOMItem, Sum(IsNull(TotalConsumptionQty,0)) As BomQty " & _
                    " From [#" & bTempTable & "] T " & _
                    " Group By T.ProdOrder, T.JobOrder, T.Item, T.BOMItem " & _
                    " HAVING Sum(IsNull(TotalConsumptionQty,0)) > 0 "

            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                Dgl2.RowCount = 1
                Dgl2.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        Dgl2.Rows.Add()
                        Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                        Dgl2.AgSelectedValue(Col2ProdOrder, I) = AgL.XNull(.Rows(I)("ProdOrder"))
                        Dgl2.AgSelectedValue(Col2JobOrder, I) = AgL.XNull(.Rows(I)("JobOrder"))
                        Dgl2.AgSelectedValue(Col2Item, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl2.AgSelectedValue(Col2BOMItem, I) = AgL.XNull(.Rows(I)("BOMItem"))
                        Dgl2.AgSelectedValue(Col2StockItem, I) = AgL.XNull(.Rows(I)("BOMItem"))
                        Dgl2.Item(Col2Qty, I).Value = Format(AgL.VNull(.Rows(I)("BomQty")), "0.000")
                        Validating_BomMaterial(Dgl2.AgSelectedValue(Col2Item, I), I)
                    Next I
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        RaiseEvent BaseFunction_PostOrderGridFill()
        RaiseEvent BaseFunction_ConsumptionGridFill()
        RaiseEvent BaseFunction_PostConsumptionGridFill()
        Calculation()
    End Sub

    Protected Sub Validating_ByProduct(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl3.Item(Col3Item, mRow).Value.ToString.Trim = "" Or Dgl3.AgSelectedValue(Col3Item, mRow).ToString.Trim = "" Then
                Dgl3.Item(Col3Unit, mRow).Value = ""
                Dgl3.Item(Col3MeasurePerPcs, mRow).Value = 0
                Dgl3.Item(Col3MeasureUnit, mRow).Value = ""
                Dgl3.AgSelectedValue(Col3StockItem, mRow) = ""
                Dgl3.Item(Col3IsDelivered, mRow).Value = ""
            Else
                If Dgl3.AgHelpDataSet(Col3Item) IsNot Nothing Then
                    DrTemp = Dgl3.AgHelpDataSet(Col3Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl3.Item(Col3Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl3.Item(Col3MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("Measure"))
                    Dgl3.Item(Col3MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl3.AgSelectedValue(Col3StockItem, mRow) = Dgl3.AgSelectedValue(Col3Item, mRow)
                    Dgl3.Item(Col3IsDelivered, mRow).Value = "Yes"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_ByProduct Function ")
        End Try
    End Sub
    
    Private Sub ProcFillJobOrderDetails(ByVal JobOrderNo As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            mQry = "  SELECT I.Code AS Code, L.DocId AS JobOrder , " & _
                        " IsNull(L.Qty,0) - (IsNull(L.ReceiveQty,0)+IsNull(L.CancelQty,0)+IsNull(L.ReceiveLoss,0)) AS BalanceQty,   " & _
                        " IsNull(L.TotalMeasure,0) - (IsNull(L.ReceiveMeasure,0)+IsNull(L.CancelMeasure,0)+IsNull(L.ReceiveMeasure,0)) AS BalanceMeasure,   " & _
                        " I.ItemType, I.SalesTaxPostingGroup, L.LossPer, L.Loss As LossQty,    " & _
                        " L.Unit, L.MeasurePerPcs , L.MeasureUnit, " & _
                        " L.Rate, I.BOM, H.JobWorker, L.ProdOrder, L.Amount, L.Remark   " & _
                        " FROM JobOrderDetail L    " & _
                        " LEFT JOIN Item I ON L.Item = I.Code   " & _
                        " LEFT JOIN JobOrder H ON L.DocId = H.DocID   " & _
                        " Where L.DocId = '" & TxtJobOrderNo.AgSelectedValue & "' " & _
                        " And IsNull(L.Qty,0) - (IsNull(L.ReceiveQty,0)+IsNull(L.CancelQty,0)+IsNull(L.ReceiveLoss,0))  > 0"

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Code"))
                        Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("BalanceQty")), "0.".PadRight(CType(Dgl1.Columns(Col1DocQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("BalanceQty")), "0.".PadRight(CType(Dgl1.Columns(Col1DocQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1BillQty, I).Value = Format(AgL.VNull(.Rows(I)("BalanceQty")), "0.".PadRight(CType(Dgl1.Columns(Col1DocQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                        Dgl1.Item(Col1DocMeasure, I).Value = AgL.VNull(.Rows(I)("BalanceMeasure"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("BalanceMeasure"))
                        Dgl1.Item(Col1BillMeasure, I).Value = AgL.VNull(.Rows(I)("BalanceMeasure"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.AgSelectedValue(Col1JobOrderDocID, I) = AgL.XNull(.Rows(I)("JobOrder"))
                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                        Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                        Dgl1.AgSelectedValue(Col1ProdOrder, I) = AgL.XNull(.Rows(I)("ProdOrder"))
                        Dgl1.Item(Col1LossPer, I).Value = AgL.VNull(.Rows(I)("LossPer"))
                        Dgl1.Item(Col1LossQty, I).Value = AgL.VNull(.Rows(I)("LossQty"))
                        Dgl1.AgSelectedValue(Col1BOM, I) = AgL.XNull(.Rows(I)("Bom"))

                        ProcFillQcParameterDetail(I)
                    Next I
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFillJobOrderDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillJobOrderDetails.Click
        Call ProcFillJobOrderDetails(TxtJobOrderNo.AgSelectedValue)
        TxtGodown.Focus()
    End Sub
End Class
