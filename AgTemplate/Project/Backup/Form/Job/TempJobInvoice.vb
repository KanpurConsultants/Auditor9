Public Class TempJobInvoice
    Inherits AgTemplate.TempTransaction
    Dim mQry$
    Dim DsMain As DataSet
    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Public WithEvents AgCShowGrid1 As New AgStructure.AgCalcShowGrid
    Public WithEvents AgCShowGrid2 As New AgStructure.AgCalcShowGrid
    Protected WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1JobReceive As String = "Job Receive"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1DocQty As String = "Doc Qty"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1BillQty As String = "Bill Qty"
    Protected Const Col1LossPer As String = "Loss Per"
    Protected Const Col1LossQty As String = "Loss Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1DocMeasure As String = "Doc Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1LossMeasure As String = "Loss Measure"
    Protected Const Col1BillMeasure As String = "Bill Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1JobOrder As String = "Job Order"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1JobIssueDocId As String = "Job Issue DocId"
    Protected Const Col1ProdOrder As String = "Prod Order"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1LotNo As String = "LotNo"

    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2Select As String = "Select"
    Protected Const Col2ManualRefNo As String = "Manual Ref No"
    Protected Const Col2JobReceive As String = "Job Receive"
    Protected Const Col2JobReceiveDate As String = "Job Receive Date"

    Public WithEvents Dgl3 As New AgControls.AgDataGrid
    Protected Const Col3Item As String = "Item"
    Protected Const Col3Qty As String = "Qty"
    Protected Const Col3Unit As String = "Unit"
    Protected Const Col3MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col3TotalMeasure As String = "Total Measure"
    Protected Const Col3MeasureUnit As String = "Measure Unit"
    Protected Const Col3Rate As String = "Rate"
    Protected Const Col3Amount As String = "Amount"

    Public Class HelpDataSet
        Public Shared Process As DataSet = Nothing
        Public Shared JobWorker As DataSet = Nothing
        Public Shared BillingType As DataSet = Nothing
        Public Shared JobReceive As DataSet = Nothing
        Public Shared AgStructure As DataSet = Nothing
        Public Shared Item As DataSet = Nothing
        Public Shared JobOrder As DataSet = Nothing
        Public Shared JobIssue As DataSet = Nothing
        Public Shared ProdOrder As DataSet = Nothing
    End Class

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtBillingType = New AgControls.AgTextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalLossQty = New System.Windows.Forms.Label
        Me.LblTotalLossQtyText = New System.Windows.Forms.Label
        Me.LblTotalBillQty = New System.Windows.Forms.Label
        Me.LblTotalBillQtyText = New System.Windows.Forms.Label
        Me.LblTotalDocQty = New System.Windows.Forms.Label
        Me.LblTotalDocQtyText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtRemarks = New AgControls.AgTextBox
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
        Me.LblJobInvoiceDetail = New System.Windows.Forms.LinkLabel
        Me.LblRemark1 = New System.Windows.Forms.Label
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.BtnFill = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.LblTotalLossMeasure = New System.Windows.Forms.Label
        Me.LblTotalLossMeasureText = New System.Windows.Forms.Label
        Me.LblTotalBillMeasure = New System.Windows.Forms.Label
        Me.LblTotalBillMeasureText = New System.Windows.Forms.Label
        Me.LblTotalDocMeasure = New System.Windows.Forms.Label
        Me.LblTotalDocMeasureText = New System.Windows.Forms.Label
        Me.TxtJobWorkerDocNo = New AgControls.AgTextBox
        Me.LblJobWorkerDocNo = New System.Windows.Forms.Label
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.LblManualRefNoReq = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.LblTotalBomAmount = New System.Windows.Forms.Label
        Me.LblTotalBomAmountText = New System.Windows.Forms.Label
        Me.PnlCShowGrid2 = New System.Windows.Forms.Panel
        Me.PnlCShowGrid = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.LblNetPaybleAmount = New System.Windows.Forms.Label
        Me.LblNetPaybleAmountText = New System.Windows.Forms.Label
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
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(746, 577)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 577)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 577)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 577)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 577)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 573)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 577)
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
        Me.LblV_No.Location = New System.Drawing.Point(240, 30)
        Me.LblV_No.Size = New System.Drawing.Size(101, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Job Receive No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(365, 29)
        Me.TxtV_No.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(116, 35)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(7, 30)
        Me.LblV_Date.Size = New System.Drawing.Size(108, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Job Receive Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(349, 15)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(134, 29)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(240, 11)
        Me.LblV_Type.Size = New System.Drawing.Size(109, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Job Receive Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(365, 9)
        Me.TxtV_Type.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(116, 15)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(7, 10)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(134, 9)
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
        Me.LblPrefix.Location = New System.Drawing.Point(673, 237)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 18)
        Me.TabControl1.Size = New System.Drawing.Size(989, 139)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LblManualRefNoReq)
        Me.TP1.Controls.Add(Me.TxtJobWorkerDocNo)
        Me.TP1.Controls.Add(Me.LblJobWorkerDocNo)
        Me.TP1.Controls.Add(Me.Pnl2)
        Me.TP1.Controls.Add(Me.LblRemark1)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtBillingType)
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.LblProcess)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(981, 113)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillingType, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.LblRemark1, 0)
        Me.TP1.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorkerDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNoReq, 0)
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
        Me.TxtBillingType.Location = New System.Drawing.Point(439, 306)
        Me.TxtBillingType.MaxLength = 20
        Me.TxtBillingType.Name = "TxtBillingType"
        Me.TxtBillingType.Size = New System.Drawing.Size(66, 18)
        Me.TxtBillingType.TabIndex = 50
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalLossQty)
        Me.Panel1.Controls.Add(Me.LblTotalLossQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalBillQty)
        Me.Panel1.Controls.Add(Me.LblTotalBillQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalDocQty)
        Me.Panel1.Controls.Add(Me.LblTotalDocQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(2, 344)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(980, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalLossQty
        '
        Me.LblTotalLossQty.AutoSize = True
        Me.LblTotalLossQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalLossQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalLossQty.Location = New System.Drawing.Point(836, 3)
        Me.LblTotalLossQty.Name = "LblTotalLossQty"
        Me.LblTotalLossQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalLossQty.TabIndex = 672
        Me.LblTotalLossQty.Text = "."
        Me.LblTotalLossQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalLossQtyText
        '
        Me.LblTotalLossQtyText.AutoSize = True
        Me.LblTotalLossQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalLossQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalLossQtyText.Location = New System.Drawing.Point(690, 3)
        Me.LblTotalLossQtyText.Name = "LblTotalLossQtyText"
        Me.LblTotalLossQtyText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalLossQtyText.TabIndex = 671
        Me.LblTotalLossQtyText.Text = "Total Loss Qty :"
        '
        'LblTotalBillQty
        '
        Me.LblTotalBillQty.AutoSize = True
        Me.LblTotalBillQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBillQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBillQty.Location = New System.Drawing.Point(572, 3)
        Me.LblTotalBillQty.Name = "LblTotalBillQty"
        Me.LblTotalBillQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBillQty.TabIndex = 670
        Me.LblTotalBillQty.Text = "."
        Me.LblTotalBillQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalBillQtyText
        '
        Me.LblTotalBillQtyText.AutoSize = True
        Me.LblTotalBillQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBillQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalBillQtyText.Location = New System.Drawing.Point(435, 3)
        Me.LblTotalBillQtyText.Name = "LblTotalBillQtyText"
        Me.LblTotalBillQtyText.Size = New System.Drawing.Size(98, 16)
        Me.LblTotalBillQtyText.TabIndex = 669
        Me.LblTotalBillQtyText.Text = "Total Bill Qty :"
        '
        'LblTotalDocQty
        '
        Me.LblTotalDocQty.AutoSize = True
        Me.LblTotalDocQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDocQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalDocQty.Location = New System.Drawing.Point(145, 3)
        Me.LblTotalDocQty.Name = "LblTotalDocQty"
        Me.LblTotalDocQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDocQty.TabIndex = 668
        Me.LblTotalDocQty.Text = "."
        Me.LblTotalDocQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalDocQtyText
        '
        Me.LblTotalDocQtyText.AutoSize = True
        Me.LblTotalDocQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDocQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDocQtyText.Location = New System.Drawing.Point(2, 3)
        Me.LblTotalDocQtyText.Name = "LblTotalDocQtyText"
        Me.LblTotalDocQtyText.Size = New System.Drawing.Size(101, 16)
        Me.LblTotalDocQtyText.TabIndex = 667
        Me.LblTotalDocQtyText.Text = "Total Doc Qty :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(344, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(233, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(343, 4)
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
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(232, 4)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(106, 16)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Total Measure :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(2, 181)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(980, 163)
        Me.Pnl1.TabIndex = 2
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
        Me.TxtRemarks.Location = New System.Drawing.Point(134, 89)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(356, 18)
        Me.TxtRemarks.TabIndex = 7
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(134, 49)
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
        Me.LblManualRefNo.Location = New System.Drawing.Point(7, 50)
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
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(116, 76)
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
        Me.TxtJobWorker.Location = New System.Drawing.Point(134, 69)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(356, 18)
        Me.TxtJobWorker.TabIndex = 6
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(7, 70)
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
        Me.TxtProcess.Location = New System.Drawing.Point(652, 253)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(125, 18)
        Me.TxtProcess.TabIndex = 5
        Me.TxtProcess.Visible = False
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(614, 274)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 737
        Me.LblProcess.Text = "Process"
        Me.LblProcess.Visible = False
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(638, 393)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(341, 156)
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
        Me.TxtStructure.Location = New System.Drawing.Point(676, 317)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(43, 18)
        Me.TxtStructure.TabIndex = 742
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(606, 317)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 743
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'LblJobInvoiceDetail
        '
        Me.LblJobInvoiceDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobInvoiceDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobInvoiceDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobInvoiceDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobInvoiceDetail.LinkColor = System.Drawing.Color.White
        Me.LblJobInvoiceDetail.Location = New System.Drawing.Point(2, 160)
        Me.LblJobInvoiceDetail.Name = "LblJobInvoiceDetail"
        Me.LblJobInvoiceDetail.Size = New System.Drawing.Size(136, 20)
        Me.LblJobInvoiceDetail.TabIndex = 733
        Me.LblJobInvoiceDetail.TabStop = True
        Me.LblJobInvoiceDetail.Text = "Job Invoice Detail"
        Me.LblJobInvoiceDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblRemark1
        '
        Me.LblRemark1.AutoSize = True
        Me.LblRemark1.BackColor = System.Drawing.Color.Transparent
        Me.LblRemark1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemark1.Location = New System.Drawing.Point(7, 90)
        Me.LblRemark1.Name = "LblRemark1"
        Me.LblRemark1.Size = New System.Drawing.Size(60, 16)
        Me.LblRemark1.TabIndex = 745
        Me.LblRemark1.Text = "Remarks"
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(577, 9)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(398, 98)
        Me.Pnl2.TabIndex = 8
        '
        'BtnFill
        '
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.Location = New System.Drawing.Point(930, 159)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(51, 21)
        Me.BtnFill.TabIndex = 1
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel2.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel2.Controls.Add(Me.LblTotalMeasure)
        Me.Panel2.Controls.Add(Me.LblTotalLossMeasure)
        Me.Panel2.Controls.Add(Me.LblTotalLossMeasureText)
        Me.Panel2.Controls.Add(Me.LblTotalBillMeasure)
        Me.Panel2.Controls.Add(Me.LblTotalBillMeasureText)
        Me.Panel2.Controls.Add(Me.LblTotalDocMeasure)
        Me.Panel2.Controls.Add(Me.LblTotalDocMeasureText)
        Me.Panel2.Location = New System.Drawing.Point(2, 367)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(980, 23)
        Me.Panel2.TabIndex = 748
        '
        'LblTotalLossMeasure
        '
        Me.LblTotalLossMeasure.AutoSize = True
        Me.LblTotalLossMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalLossMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalLossMeasure.Location = New System.Drawing.Point(835, 3)
        Me.LblTotalLossMeasure.Name = "LblTotalLossMeasure"
        Me.LblTotalLossMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalLossMeasure.TabIndex = 672
        Me.LblTotalLossMeasure.Text = "."
        Me.LblTotalLossMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalLossMeasureText
        '
        Me.LblTotalLossMeasureText.AutoSize = True
        Me.LblTotalLossMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalLossMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalLossMeasureText.Location = New System.Drawing.Point(689, 3)
        Me.LblTotalLossMeasureText.Name = "LblTotalLossMeasureText"
        Me.LblTotalLossMeasureText.Size = New System.Drawing.Size(138, 16)
        Me.LblTotalLossMeasureText.TabIndex = 671
        Me.LblTotalLossMeasureText.Text = "Total Loss Measure :"
        '
        'LblTotalBillMeasure
        '
        Me.LblTotalBillMeasure.AutoSize = True
        Me.LblTotalBillMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBillMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBillMeasure.Location = New System.Drawing.Point(571, 3)
        Me.LblTotalBillMeasure.Name = "LblTotalBillMeasure"
        Me.LblTotalBillMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBillMeasure.TabIndex = 670
        Me.LblTotalBillMeasure.Text = "."
        Me.LblTotalBillMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalBillMeasureText
        '
        Me.LblTotalBillMeasureText.AutoSize = True
        Me.LblTotalBillMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBillMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalBillMeasureText.Location = New System.Drawing.Point(434, 3)
        Me.LblTotalBillMeasureText.Name = "LblTotalBillMeasureText"
        Me.LblTotalBillMeasureText.Size = New System.Drawing.Size(131, 16)
        Me.LblTotalBillMeasureText.TabIndex = 669
        Me.LblTotalBillMeasureText.Text = "Total Bill Measure :"
        '
        'LblTotalDocMeasure
        '
        Me.LblTotalDocMeasure.AutoSize = True
        Me.LblTotalDocMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDocMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalDocMeasure.Location = New System.Drawing.Point(145, 3)
        Me.LblTotalDocMeasure.Name = "LblTotalDocMeasure"
        Me.LblTotalDocMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDocMeasure.TabIndex = 668
        Me.LblTotalDocMeasure.Text = "."
        Me.LblTotalDocMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalDocMeasureText
        '
        Me.LblTotalDocMeasureText.AutoSize = True
        Me.LblTotalDocMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDocMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDocMeasureText.Location = New System.Drawing.Point(2, 3)
        Me.LblTotalDocMeasureText.Name = "LblTotalDocMeasureText"
        Me.LblTotalDocMeasureText.Size = New System.Drawing.Size(134, 16)
        Me.LblTotalDocMeasureText.TabIndex = 667
        Me.LblTotalDocMeasureText.Text = "Total Doc Measure :"
        '
        'TxtJobWorkerDocNo
        '
        Me.TxtJobWorkerDocNo.AgMandatory = True
        Me.TxtJobWorkerDocNo.AgMasterHelp = False
        Me.TxtJobWorkerDocNo.AgNumberLeftPlaces = 8
        Me.TxtJobWorkerDocNo.AgNumberNegetiveAllow = False
        Me.TxtJobWorkerDocNo.AgNumberRightPlaces = 2
        Me.TxtJobWorkerDocNo.AgPickFromLastValue = False
        Me.TxtJobWorkerDocNo.AgRowFilter = ""
        Me.TxtJobWorkerDocNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobWorkerDocNo.AgSelectedValue = Nothing
        Me.TxtJobWorkerDocNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobWorkerDocNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobWorkerDocNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobWorkerDocNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobWorkerDocNo.Location = New System.Drawing.Point(365, 49)
        Me.TxtJobWorkerDocNo.MaxLength = 20
        Me.TxtJobWorkerDocNo.Name = "TxtJobWorkerDocNo"
        Me.TxtJobWorkerDocNo.Size = New System.Drawing.Size(125, 18)
        Me.TxtJobWorkerDocNo.TabIndex = 5
        '
        'LblJobWorkerDocNo
        '
        Me.LblJobWorkerDocNo.AutoSize = True
        Me.LblJobWorkerDocNo.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorkerDocNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorkerDocNo.Location = New System.Drawing.Point(240, 50)
        Me.LblJobWorkerDocNo.Name = "LblJobWorkerDocNo"
        Me.LblJobWorkerDocNo.Size = New System.Drawing.Size(121, 16)
        Me.LblJobWorkerDocNo.TabIndex = 748
        Me.LblJobWorkerDocNo.Text = "Job Worker Doc No"
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(2, 416)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(631, 133)
        Me.Pnl3.TabIndex = 3
        '
        'LblManualRefNoReq
        '
        Me.LblManualRefNoReq.AutoSize = True
        Me.LblManualRefNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualRefNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualRefNoReq.Location = New System.Drawing.Point(116, 52)
        Me.LblManualRefNoReq.Name = "LblManualRefNoReq"
        Me.LblManualRefNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualRefNoReq.TabIndex = 749
        Me.LblManualRefNoReq.Text = "Ä"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(2, 395)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(168, 20)
        Me.LinkLabel1.TabIndex = 750
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Job Invoice Bom Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel3.Controls.Add(Me.LblTotalBomAmount)
        Me.Panel3.Controls.Add(Me.LblTotalBomAmountText)
        Me.Panel3.Location = New System.Drawing.Point(2, 549)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(631, 23)
        Me.Panel3.TabIndex = 695
        '
        'LblTotalBomAmount
        '
        Me.LblTotalBomAmount.AutoSize = True
        Me.LblTotalBomAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBomAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBomAmount.Location = New System.Drawing.Point(509, 3)
        Me.LblTotalBomAmount.Name = "LblTotalBomAmount"
        Me.LblTotalBomAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBomAmount.TabIndex = 668
        Me.LblTotalBomAmount.Text = "."
        Me.LblTotalBomAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalBomAmountText
        '
        Me.LblTotalBomAmountText.AutoSize = True
        Me.LblTotalBomAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBomAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalBomAmountText.Location = New System.Drawing.Point(402, 3)
        Me.LblTotalBomAmountText.Name = "LblTotalBomAmountText"
        Me.LblTotalBomAmountText.Size = New System.Drawing.Size(101, 16)
        Me.LblTotalBomAmountText.TabIndex = 667
        Me.LblTotalBomAmountText.Text = "Total Amount :"
        '
        'PnlCShowGrid2
        '
        Me.PnlCShowGrid2.Location = New System.Drawing.Point(436, 587)
        Me.PnlCShowGrid2.Name = "PnlCShowGrid2"
        Me.PnlCShowGrid2.Size = New System.Drawing.Size(61, 19)
        Me.PnlCShowGrid2.TabIndex = 752
        '
        'PnlCShowGrid
        '
        Me.PnlCShowGrid.Location = New System.Drawing.Point(514, 587)
        Me.PnlCShowGrid.Name = "PnlCShowGrid"
        Me.PnlCShowGrid.Size = New System.Drawing.Size(37, 19)
        Me.PnlCShowGrid.TabIndex = 751
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel4.Controls.Add(Me.LblNetPaybleAmount)
        Me.Panel4.Controls.Add(Me.LblNetPaybleAmountText)
        Me.Panel4.Location = New System.Drawing.Point(638, 549)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(343, 23)
        Me.Panel4.TabIndex = 696
        '
        'LblNetPaybleAmount
        '
        Me.LblNetPaybleAmount.AutoSize = True
        Me.LblNetPaybleAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetPaybleAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblNetPaybleAmount.Location = New System.Drawing.Point(241, 3)
        Me.LblNetPaybleAmount.Name = "LblNetPaybleAmount"
        Me.LblNetPaybleAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblNetPaybleAmount.TabIndex = 668
        Me.LblNetPaybleAmount.Text = "."
        Me.LblNetPaybleAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblNetPaybleAmountText
        '
        Me.LblNetPaybleAmountText.AutoSize = True
        Me.LblNetPaybleAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetPaybleAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblNetPaybleAmountText.Location = New System.Drawing.Point(97, 3)
        Me.LblNetPaybleAmountText.Name = "LblNetPaybleAmountText"
        Me.LblNetPaybleAmountText.Size = New System.Drawing.Size(138, 16)
        Me.LblNetPaybleAmountText.TabIndex = 667
        Me.LblNetPaybleAmountText.Text = "Net Payble Amount :"
        '
        'TempJobInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 618)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.PnlCShowGrid2)
        Me.Controls.Add(Me.PnlCShowGrid)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LblJobInvoiceDetail)
        Me.Controls.Add(Me.BtnFill)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "TempJobInvoice"
        Me.Text = "Template Job Receive"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.BtnFill, 0)
        Me.Controls.SetChildIndex(Me.LblJobInvoiceDetail, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Pnl3, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.Panel3, 0)
        Me.Controls.SetChildIndex(Me.PnlCShowGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlCShowGrid2, 0)
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
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtBillingType As AgControls.AgTextBox
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
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
    Protected WithEvents LblJobInvoiceDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents Pnl2 As System.Windows.Forms.Panel
    Protected WithEvents BtnFill As System.Windows.Forms.Button
    Protected WithEvents LblRemark1 As System.Windows.Forms.Label
    Protected WithEvents LblTotalDocQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalDocQtyText As System.Windows.Forms.Label
    Protected WithEvents LblTotalBillQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalBillQtyText As System.Windows.Forms.Label
    Protected WithEvents LblTotalLossQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalLossQtyText As System.Windows.Forms.Label
    Protected WithEvents Panel2 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalLossMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalLossMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalBillMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalBillMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalDocMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDocMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtJobWorkerDocNo As AgControls.AgTextBox
    Protected WithEvents LblJobWorkerDocNo As System.Windows.Forms.Label
    Protected WithEvents Pnl3 As System.Windows.Forms.Panel
    Protected WithEvents LblManualRefNoReq As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalBomAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalBomAmountText As System.Windows.Forms.Label
    Protected WithEvents PnlCShowGrid2 As System.Windows.Forms.Panel
    Protected WithEvents PnlCShowGrid As System.Windows.Forms.Panel
    Protected WithEvents Panel4 As System.Windows.Forms.Panel
    Protected WithEvents LblNetPaybleAmount As System.Windows.Forms.Label
    Protected WithEvents LblNetPaybleAmountText As System.Windows.Forms.Label
#End Region


    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobInvoice"
        LogTableName = "JobInvoice_Log"
        MainLineTableCsv = "JobInvoiceDetail,JobInvoiceBom,Structure_TransFooter,Structure_TransLine"
        LogLineTableCsv = "JobInvoiceDetail_Log,JobInvoiceBom_Log,Structure_TransFooter_Log,Structure_TransLine_Log"
        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl2)
        AgL.GridDesign(Dgl3)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgL.AddAgDataGrid(AgCShowGrid1, PnlCShowGrid)
        AgL.AddAgDataGrid(AgCShowGrid2, PnlCShowGrid2)
        AgCShowGrid1.Visible = False
        AgCShowGrid2.Visible = False

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT J.UID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " J.V_Date AS [Entry Date], J.V_No AS [Entry No], " & _
        '                    " J.ManualRefNo, Sg.DispName As JobWorker " & _
        '                    " FROM JobInvoice_Log J " & _
        '                    " LEFT JOIN Voucher_type Vt ON J.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup Sg On J.JobWorker = Sg.SubCode " & _
        '                    " Where J.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Invoice Type], H.V_Prefix AS Prefix, H.V_Date AS [Invoice Date], H.V_No AS [Invoice No], " & _
                " H.ManualRefNo AS [Manual No], H.JobWorkerDocNo AS [Job Worker Doc No], H.BillingType AS [Billing Type],  H.TotalDocQty AS [Total Doc Qty],  " & _
                " H.TotalQty AS [Total Qty], H.TotalBillQty AS [Total Bill Qty], H.TotalLoss AS [Total Loss], H.TotalDocMeasure AS [Total Doc Measure], H.TotalMeasure AS [Total Measure],  " & _
                " H.TotalBillMeasure AS [Total Bill Measure], H.TotalLossMeasure AS [Total Loss Measure], H.Amount, H.NetAmount AS [Net Amount], H.Remarks, H.Structure,  " & _
                " H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By],  " & _
                " H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, H.Process, H.TotalBomAmount AS [Total Bom Amount], H.NetPaybleAmount AS [Net Payble Amount], " & _
                " D.Div_Name AS Division,SM.Name AS [Site Name],SGJ.DispName AS [Worker Name] " & _
                " FROM  JobInvoice_Log H " & _
                " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN SubGroup SGJ ON SGJ.SubCode  = H.JobWorker  " & _
                " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT J.DocID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " J.V_Date AS [Entry Date], J.V_No AS [Entry No], " & _
        '                    " J.ManualRefNo, Sg.DispName As JobWorker " & _
        '                    " FROM JobInvoice J " & _
        '                    " LEFT JOIN voucher_type Vt ON J.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup Sg On J.JobWorker = Sg.SubCode " & _
        '                    " Where 1=1  " & mCondStr

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Invoice Type], H.V_Prefix AS Prefix, H.V_Date AS [Invoice Date], H.V_No AS [Invoice No], " & _
                        " H.ManualRefNo AS [Manual No], H.JobWorkerDocNo AS [Job Worker Doc No], H.BillingType AS [Billing Type],  H.TotalDocQty AS [Total Doc Qty],  " & _
                        " H.TotalQty AS [Total Qty], H.TotalBillQty AS [Total Bill Qty], H.TotalLoss AS [Total Loss], H.TotalDocMeasure AS [Total Doc Measure], H.TotalMeasure AS [Total Measure],  " & _
                        " H.TotalBillMeasure AS [Total Bill Measure], H.TotalLossMeasure AS [Total Loss Measure], H.Amount, H.NetAmount AS [Net Amount], H.Remarks, H.Structure,  " & _
                        " H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By],  " & _
                        " H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, H.Process, H.TotalBomAmount AS [Total Bom Amount], H.NetPaybleAmount AS [Net Payble Amount], " & _
                        " D.Div_Name AS Division,SM.Name AS [Site Name],SGJ.DispName AS [Worker Name] " & _
                        " FROM  JobInvoice H " & _
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                        " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                        " LEFT JOIN SubGroup SGJ ON SGJ.SubCode  = H.JobWorker  " & _
                        " Where 1=1  " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select J.DocID As SearchCode " & _
                " From JobInvoice J " & _
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
            " From JobInvoice_Log J " & _
            " Left Join Voucher_Type Vt On J.V_Type = Vt.V_Type  " & _
            " Where 1=1  " & mCondStr & "  Order By J.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1JobReceive, 80, 5, Col1JobReceive, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 5, Col1Item, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 5, 4, False, Col1DocQty, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 5, 4, False, Col1Qty, True, True)
            .AddAgNumberColumn(Dgl1, Col1BillQty, 70, 5, 4, False, Col1BillQty, True, False)
            .AddAgNumberColumn(Dgl1, Col1LossPer, 70, 5, 4, False, Col1LossPer, True, True)
            .AddAgNumberColumn(Dgl1, Col1LossQty, 70, 5, 4, False, Col1LossQty, True, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 70, 5, Col1Unit, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 5, 4, False, Col1MeasurePerPcs, True, True)
            .AddAgNumberColumn(Dgl1, Col1DocMeasure, 70, 5, 4, False, Col1DocMeasure, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 5, 4, False, Col1TotalMeasure, True, True)
            .AddAgNumberColumn(Dgl1, Col1LossMeasure, 70, 5, 4, False, Col1LossMeasure, True, True)
            .AddAgNumberColumn(Dgl1, Col1BillMeasure, 70, 5, 4, False, Col1BillMeasure, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 5, Col1MeasureUnit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1JobOrder, 80, 5, Col1JobOrder, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 70, 5, 2, False, Col1Rate, True, False)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 5, 2, False, Col1Amount, True, True)
            .AddAgTextColumn(Dgl1, Col1JobIssueDocId, 100, 5, Col1JobIssueDocId, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 100, 5, Col1ProdOrder, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 5, Col1Remark, True, False, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 5, Col1LotNo, True, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.AllowUserToOrderColumns = True

        Call AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1JobReceive)
        Call AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1JobOrder)
        Call AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1JobIssueDocId)
        Call AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1ProdOrder)

        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgCheckColumn(Dgl2, Col2Select, 50, Col2Select, True)
            .AddAgTextColumn(Dgl2, Col2ManualRefNo, 100, 0, Col2ManualRefNo, True, True)
            .AddAgTextColumn(Dgl2, Col2JobReceive, 100, 0, Col2JobReceive, False, True)
            .AddAgDateColumn(Dgl2, Col2JobReceiveDate, 127, Col2JobReceiveDate, True, True)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 25
        Dgl2.AllowUserToAddRows = False


        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl3, Col3Item, 150, 5, Col3Item, True, True, False)
            .AddAgNumberColumn(Dgl3, Col3Qty, 70, 5, 3, False, Col3Qty, True, True)
            .AddAgTextColumn(Dgl3, Col3Unit, 40, 5, Col3Unit, True, True)
            .AddAgNumberColumn(Dgl3, Col3MeasurePerPcs, 60, 5, 3, False, Col3MeasurePerPcs, True, True)
            .AddAgNumberColumn(Dgl3, Col3TotalMeasure, 60, 5, 3, False, Col3TotalMeasure, True, True)
            .AddAgTextColumn(Dgl3, Col3MeasureUnit, 50, 5, Col3MeasureUnit, True, True)
            .AddAgNumberColumn(Dgl3, Col3Rate, 60, 5, 2, False, Col3Rate, True, False)
            .AddAgNumberColumn(Dgl3, Col3Amount, 80, 5, 2, False, Col3Amount, True, True)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 35
        Dgl3.AllowUserToAddRows = False
        Dgl3.AgSkipReadOnlyColumns = True

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)
        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index

        AgCalcGrid1.AgFixedRows = 6
        AgCShowGrid1.AgIsFixedRows = True
        AgCShowGrid1.AgParentCalcGrid = AgCalcGrid1
        AgCShowGrid2.AgParentCalcGrid = AgCalcGrid1
        If AgCalcGrid1.RowCount > 0 Then
            AgCShowGrid1.Ini_Grid()
            AgCShowGrid2.Ini_Grid()
        End If

        AgCL.GridSetiingShowXml(Me.Text + Dgl1.Name, Dgl1)

        FrmProductionOrder_BaseFunction_FIniList()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = " UPDATE JobInvoice_Log " & _
                " SET " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " JobWorker =" & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " & _
                " JobWorkerDocNo = " & AgL.Chk_Text(TxtJobWorkerDocNo.Text) & ", " & _
                " BillingType = " & AgL.Chk_Text(TxtBillingType.Text) & ", " & _
                " TotalDocQty = " & Val(LblTotalDocQty.Text) & ", " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " TotalBillQty = " & Val(LblTotalBillQty.Text) & ", " & _
                " TotalLoss = " & Val(LblTotalLossQty.Text) & ", " & _
                " TotalDocMeasure = " & Val(LblTotalDocMeasure.Text) & ", " & _
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " & _
                " TotalBillMeasure = " & Val(LblTotalBillMeasure.Text) & ", " & _
                " TotalLossMeasure = " & Val(LblTotalLossMeasure.Text) & ", " & _
                " TotalBomAmount = " & Val(LblTotalBomAmount.Text) & ", " & _
                " Amount = " & Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.GROSSAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) & ", " & _
                " NetAmount = " & Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) & ", " & _
                " NetPaybleAmount = " & Val(LblNetPaybleAmount.Text) & ", " & _
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & " " & _
                " Where UID = '" & mSearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        AgCalcGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)

        mQry = "Delete From JobInvoiceDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobInvoiceBom_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    mSr += 1
                    mQry = " INSERT INTO JobInvoiceDetail_Log (DocId, Sr, JobReceive, Item, " & _
                            " DocQty, Qty, BillQty, LossPer, LossQty, " & _
                            " Unit, MeasurePerPcs, DocMeasure, TotalMeasure, LossMeasure, " & _
                            " BillMeasure, MeasureUnit, JobOrder, Rate, " & _
                            " Amount, NetAmount, JobIssueDocId, ProdOrder, " & _
                            " Remark, LotNo, UID) " & _
                            " VALUES ('" & mInternalCode & "', " & _
                            " " & Val(mSr) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1JobReceive, I)) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1Item, I)) & ", " & _
                            " " & Val(.Item(Col1DocQty, I).Value) & ", " & _
                            " " & Val(.Item(Col1Qty, I).Value) & ", " & _
                            " " & Val(.Item(Col1BillQty, I).Value) & ", " & _
                            " " & Val(.Item(Col1LossPer, I).Value) & ", " & _
                            " " & Val(.Item(Col1LossQty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1Unit, I).Value) & ", " & _
                            " " & Val(.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                            " " & Val(.Item(Col1DocMeasure, I).Value) & ", " & _
                            " " & Val(.Item(Col1TotalMeasure, I).Value) & ", " & _
                            " " & Val(.Item(Col1LossMeasure, I).Value) & ", " & _
                            " " & Val(.Item(Col1BillMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1MeasureUnit, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1JobOrder, I)) & ", " & _
                            " " & Val(.Item(Col1Rate, I).Value) & ", " & _
                            " " & Val(.Item(Col1Amount, I).Value) & ", " & _
                            " " & Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, I, AgStructure.AgCalcGrid.LineColumnType.Amount)) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1JobIssueDocId, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1ProdOrder, I)) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1Remark, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1LotNo, I).Value) & ", " & _
                            " '" & mSearchCode & "')"

                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    AgCalcGrid1.Save_TransLine(mInternalCode, mSr, I, Conn, Cmd, SearchCode)
                    RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
                End If
            Next
        End With

        With Dgl3
            For I = 0 To Dgl3.RowCount - 1
                If Dgl3.Item(Col3Item, I).Value <> "" Then
                    mSr += 3
                    mQry = " INSERT INTO JobInvoiceBom_Log (DocId, " & _
                            " Sr, " & _
                            " Item, " & _
                            " Qty, " & _
                            " Unit, " & _
                            " MeasurePerPcs, " & _
                            " TotalMeasure, " & _
                            " MeasureUnit, " & _
                            " Rate, " & _
                            " Amount, " & _
                            " UID) " & _
                            " VALUES (" & AgL.Chk_Text(mInternalCode) & ", " & _
                            " " & Val(mSr) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col3Item, I)) & ", " & _
                            " " & Val(.Item(Col3Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3Unit, I).Value) & ", " & _
                            " " & Val(.Item(Col3MeasurePerPcs, I).Value) & ", " & _
                            " " & Val(.Item(Col3TotalMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col3MeasureUnit, I).Value) & ", " & _
                            " " & Val(.Item(Col3Rate, I).Value) & ", " & _
                            " " & Val(.Item(Col3Amount, I).Value) & ", " & _
                            " '" & mSearchCode & "')"

                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        Dgl1.AgHelpDataSet(Col1Item) = HelpDataSet.Item

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select J.* " & _
                " From JobInvoice J " & _
                " Where J.DocID='" & SearchCode & "'"
        Else
            mQry = "Select J.* " & _
                " From JobInvoice_Log J " & _
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

                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtJobWorker.AgSelectedValue = AgL.XNull(.Rows(0)("JobWorker"))
                TxtJobWorkerDocNo.Text = AgL.XNull(.Rows(0)("JobWorkerDocNo"))
                TxtBillingType.Text = AgL.XNull(.Rows(0)("BillingType"))
                TxtProcess.AgSelectedValue = AgL.XNull(.Rows(0)("Process"))

                LblTotalDocQty.Text = AgL.VNull(.Rows(0)("TotalDocQty"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalBillQty.Text = AgL.VNull(.Rows(0)("TotalBillQty"))
                LblTotalLossQty.Text = AgL.VNull(.Rows(0)("TotalLoss"))
                LblTotalDocMeasure.Text = AgL.VNull(.Rows(0)("TotalDocMeasure"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                LblTotalBillMeasure.Text = AgL.VNull(.Rows(0)("TotalBillMeasure"))
                LblTotalLossMeasure.Text = AgL.VNull(.Rows(0)("TotalLossMeasure"))
                LblTotalBomAmount.Text = AgL.VNull(.Rows(0)("TotalBomAmount"))
                LblNetPaybleAmount.Text = AgL.VNull(.Rows(0)("NetPaybleAmount"))

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtStructure.Text = AgL.XNull(.Rows(0)("Structure"))

                AgCalcGrid1.MoveRec_TransFooter(SearchCode)

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from JobInvoiceDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from JobInvoiceDetail_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.AgSelectedValue(Col1JobReceive, I) = AgL.XNull(.Rows(I)("JobReceive"))
                            Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(CType(Dgl1.Columns(Col1DocQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1BillQty, I).Value = Format(AgL.VNull(.Rows(I)("BillQty")), "0.".PadRight(CType(Dgl1.Columns(Col1BillQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1LossPer, I).Value = Format(AgL.VNull(.Rows(I)("LossPer")), "0.".PadRight(CType(Dgl1.Columns(Col1LossPer), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1LossQty, I).Value = Format(AgL.VNull(.Rows(I)("LossQty")), "0.".PadRight(CType(Dgl1.Columns(Col1LossQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(CType(Dgl1.Columns(Col1MeasurePerPcs), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1DocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("DocMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1DocMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1LossMeasure, I).Value = Format(AgL.VNull(.Rows(I)("LossMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1LossMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1BillMeasure, I).Value = Format(AgL.VNull(.Rows(I)("BillMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1BillMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.AgSelectedValue(Col1JobOrder, I) = AgL.XNull(.Rows(I)("JobOrder"))
                            Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.".PadRight(CType(Dgl1.Columns(Col1Rate), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1JobIssueDocId, I).Value = AgL.XNull(.Rows(I)("JobIssueDocId"))
                            Dgl1.AgSelectedValue(Col1ProdOrder, I) = AgL.XNull(.Rows(I)("ProdOrder"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))

                            AgCalcGrid1.MoveRec_TransLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                            RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If

                    If FrmType = ClsMain.EntryPointType.Main Then
                        mQry = "Select Distinct L.JobReceive, R.ManualRefNo, R.V_Date As JobReceiveDate " & _
                                " from JobInvoiceDetail L " & _
                                " LEFT JOIN JobIssRec R On L.JobReceive = R.DocId " & _
                                " where L.DocId = '" & SearchCode & "' "
                    Else
                        mQry = "Select Distinct L.JobReceive, R.ManualRefNo, R.V_Date As JobReceiveDate " & _
                                " from JobInvoiceDetail_Log L " & _
                                " LEFT JOIN JobIssRec R On L.JobReceive = R.DocId " & _
                                " where L.UID = '" & SearchCode & "' "
                    End If
                    DsTemp = AgL.FillData(mQry, AgL.GCn)
                    With DsTemp.Tables(0)
                        Dgl2.RowCount = 1
                        Dgl2.Rows.Clear()
                        If .Rows.Count > 0 Then
                            For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                                If AgL.XNull(.Rows(I)("JobReceive")) <> "" Then
                                    Dgl2.Rows.Add()
                                    Dgl2.Item(Col2Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
                                    Dgl2.Item(Col2ManualRefNo, I).Value = AgL.XNull(.Rows(I)("ManualRefNo"))
                                    Dgl2.AgSelectedValue(Col2JobReceive, I) = AgL.XNull(.Rows(I)("JobReceive"))
                                    Dgl2.Item(Col2JobReceiveDate, I).Value = AgL.XNull(.Rows(I)("JobReceiveDate"))
                                End If
                            Next I
                        End If
                    End With


                    If FrmType = ClsMain.EntryPointType.Main Then
                        mQry = "Select L.* " & _
                                " from JobInvoiceBom L " & _
                                " Where L.DocId = '" & SearchCode & "' "
                    Else
                        mQry = "Select L.* " & _
                                " from JobInvoiceBom_Log L " & _
                                " Where L.UID = '" & SearchCode & "' "
                    End If
                    DsTemp = AgL.FillData(mQry, AgL.GCn)
                    With DsTemp.Tables(0)
                        Dgl3.RowCount = 1
                        Dgl3.Rows.Clear()
                        If .Rows.Count > 0 Then
                            For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                                Dgl3.Rows.Add()
                                Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count
                                Dgl3.AgSelectedValue(Col3Item, I) = AgL.XNull(.Rows(I)("Item"))
                                Dgl3.Item(Col3Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                                Dgl3.Item(Col3Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                                Dgl3.Item(Col3MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(CType(Dgl1.Columns(Col1MeasurePerPcs), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                                Dgl3.Item(Col3TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                                Dgl3.Item(Col3MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                                Dgl3.Item(Col3Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.".PadRight(CType(Dgl1.Columns(Col1Rate), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                                Dgl3.Item(Col3Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Next I
                        End If
                    End With
                End With
                AgCShowGrid1.MoveRec_FromCalcGrid()
                AgCShowGrid2.MoveRec_FromCalcGrid()
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        Topctrl1.ChangeAgGridState(Dgl2, False)
        Topctrl1.ChangeAgGridState(Dgl3, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating
        Select Case sender.NAME
            Case TxtV_Type.Name
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                IniGrid()
            Case TxtManualRefNo.Name
                e.Cancel = Not FCheckDuplicateRefNo()
        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
        TxtProcess.AgSelectedValue = AgL.Dman_Execute(" SELECT H.NCat FROM Process H WHERE H.ProcessInvoiceNCat = '" & EntryNCat & "' ", AgL.GCn).ExecuteScalar
        TxtBillingType.AgSelectedValue = AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar
        TxtManualRefNo.Text = TxtV_No.Text.ToString
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        TxtProcess.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Process
        TxtJobWorker.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobWorker
        TxtBillingType.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.BillingType
        TxtStructure.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.AgStructure
        Dgl1.AgHelpDataSet(Col1JobReceive) = HelpDataSet.JobReceive
        Dgl2.AgHelpDataSet(Col2JobReceive) = HelpDataSet.JobReceive
        Dgl1.AgHelpDataSet(Col1Item) = HelpDataSet.Item
        Dgl1.AgHelpDataSet(Col1JobOrder) = HelpDataSet.JobOrder
        Dgl1.AgHelpDataSet(Col1JobIssueDocId) = HelpDataSet.JobIssue
        Dgl3.AgHelpDataSet(Col3Item) = HelpDataSet.Item
        Dgl1.AgHelpDataSet(Col1ProdOrder, 3) = HelpDataSet.ProdOrder
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0  " & _
                    " And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                    " And JobWorker = '" & TxtJobWorker.AgSelectedValue & "'"
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
                Case Col1DocQty
                    Dgl1.Item(Col1Qty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1DocQty, mRowIndex).Value), "0.00")
                    Dgl1.Item(Col1BillQty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1DocQty, mRowIndex).Value), "0.00")
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalDocQty.Text = 0 : LblTotalQty.Text = 0 : LblTotalBillQty.Text = 0 : LblTotalLossQty.Text = 0
        LblTotalDocMeasure.Text = 0 : LblTotalMeasure.Text = 0 : LblTotalBillMeasure.Text = 0 : LblTotalLossMeasure.Text = 0

        LblTotalBomAmount.Text = 0 : LblNetPaybleAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1LossPer, I).Value) > 0 Then
                    Dgl1.Item(Col1LossQty, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1LossPer, I).Value) / 100, "0.000")
                End If

                Dgl1.Item(Col1DocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")
                Dgl1.Item(Col1BillMeasure, I).Value = Format(Val(Dgl1.Item(Col1BillQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")

                If AgL.StrCmp(TxtBillingType.Text, "Qty") Or TxtBillingType.Text = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1BillQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1BillMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                End If

                LblTotalDocQty.Text = Val(LblTotalDocQty.Text) + Val(Dgl1.Item(Col1DocQty, I).Value)
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalBillQty.Text = Val(LblTotalBillQty.Text) + Val(Dgl1.Item(Col1BillQty, I).Value)
                LblTotalLossQty.Text = Val(LblTotalLossQty.Text) + Val(Dgl1.Item(Col1LossQty, I).Value)

                LblTotalDocMeasure.Text = Val(LblTotalDocMeasure.Text) + Val(Dgl1.Item(Col1DocMeasure, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalBillMeasure.Text = Val(LblTotalBillMeasure.Text) + Val(Dgl1.Item(Col1BillMeasure, I).Value)
                LblTotalLossMeasure.Text = Val(LblTotalLossMeasure.Text) + Val(Dgl1.Item(Col1LossMeasure, I).Value)
            End If
        Next

        For I = 0 To Dgl3.Rows.Count - 1
            If Dgl3.Item(Col3Item, I).Value <> "" Then
                If AgL.StrCmp(TxtBillingType.Text, "Qty") Or TxtBillingType.Text = "" Then
                    Dgl3.Item(Col3Amount, I).Value = Format(Val(Dgl3.Item(Col3Qty, I).Value) * Val(Dgl3.Item(Col3Rate, I).Value), "0.00")
                Else
                    Dgl3.Item(Col3Amount, I).Value = Format(Val(Dgl3.Item(Col3TotalMeasure, I).Value) * Val(Dgl3.Item(Col3Rate, I).Value), "0.00")
                End If
                LblTotalBomAmount.Text = Val(LblTotalBomAmount.Text) + Val(Dgl3.Item(Col3Amount, I).Value)
            End If
        Next

        LblNetPaybleAmount.Text = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) - Val(LblTotalBomAmount.Text)

        AgCalcGrid1.Calculation()
        LblTotalDocQty.Text = Format(Val(LblTotalDocQty.Text), "0.000")
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
        LblTotalBillQty.Text = Format(Val(LblTotalBillQty.Text), "0.000")
        LblTotalLossQty.Text = Format(Val(LblTotalLossQty.Text), "0.000")

        LblTotalDocMeasure.Text = Format(Val(LblTotalDocMeasure.Text), "0.000")
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
        LblTotalBillMeasure.Text = Format(Val(LblTotalBillMeasure.Text), "0.000")
        LblTotalLossMeasure.Text = Format(Val(LblTotalLossMeasure.Text), "0.000")

        LblTotalBomAmount.Text = Format(Val(LblTotalBomAmount.Text), "0.00")
        LblNetPaybleAmount.Text = Format(Val(LblNetPaybleAmount.Text), "0.00")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim DrTemp() As DataRow = Nothing

        If AgL.RequiredField(TxtManualRefNo, LblManualRefNo.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtJobWorker, LblJobWorker.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1BillQty, I).Value) = 0 Then
                        MsgBox("Bill Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1BillQty, I) : Dgl1.Focus()
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


        passed = FCheckDuplicateRefNo()
        'With Dgl3
        '    For I = 0 To .Rows.Count - 1
        '        If .Item(Col3Item, I).Value <> "" Then
        '            If Val(.Item(Col3Rate, I).Value) = 0 Then
        '                MsgBox("Rate Is 0 At Row No " & Dgl3.Item(ColSNo, I).Value & "")
        '                .CurrentCell = .Item(Col3Rate, I) : Dgl3.Focus()
        '                passed = False : Exit Sub
        '            End If
        '        End If
        '    Next
        'End With
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM JobInvoice WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " & _
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM JobInvoice WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'  " & _
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function


    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtJobWorker.Enter
        Try
            Select Case sender.name
                Case TxtJobWorker.Name
                    TxtJobWorker.AgRowFilter = " IsDeleted = 0 " & _
                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " And Process = '" & TxtProcess.AgSelectedValue & "' "

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()

        LblTotalDocMeasure.Text = 0 : LblTotalMeasure.Text = 0 : LblTotalBillMeasure.Text = 0 : LblTotalLossMeasure.Text = 0
        LblTotalDocQty.Text = 0 : LblTotalQty.Text = 0 : LblTotalBillQty.Text = 0 : LblTotalLossQty.Text = 0

        LblTotalBomAmount.Text = 0 : LblNetPaybleAmount.Text = 0
    End Sub

    Private Sub TempJobReceive_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = "Select P.NCat As Code, Vc.NCatDescription As Process, P.Div_Code " & _
                " From Process P " & _
                " LEFT JOIN VoucherCat Vc On P.NCat  = Vc.NCat " & _
                " Order By Vc.NCatDescription "
        HelpDataSet.Process = AgL.FillData(mQry, AgL.GCn)

        'mQry = "SELECT Sg.SubCode AS Code, Sg.DispName  AS JobWorker, " & _
        '      " IsNull(Sg.IsDeleted,0) As IsDeleted, Sg.Div_Code, " & _
        '      " IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
        '      " FROM JobWorker J " & _
        '      " LEFT JOIN SubGroup Sg ON J.SubCode = Sg.SubCode "

        mQry = " SELECT J.SubCode AS Code, Sg.DispName AS JobWorker, H.Process, " & _
               " IsNull(Sg.IsDeleted,0) AS IsDeleted,  " & _
               " IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
               " FROM JobWorker J " & _
               " LEFT JOIN JobWorkerProcess H On J.SubCode = H.SubCode  " & _
               " LEFT JOIN SubGroup Sg ON J.SubCode = Sg.SubCode "
        HelpDataSet.JobWorker = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Description  " & _
                " FROM Structure ORDER BY Description "
        HelpDataSet.AgStructure = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT  H.Code, H.Description, H.Unit, H.ItemType, IsNull(H.IsDeleted,0) As IsDeleted, " & _
                " H.UpcCode, H.Bom, " & _
                " IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, " & _
                " H.Div_Code, H.SalesTaxPostingGroup, H.Measure, H.MeasureUnit, " & _
                " H.ItemGroup, H.Rate " & _
                " FROM Item H "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.DocID AS Code, H.ManualRefNo, H.V_Type + '-' + Convert(NVARCHAR,H.V_No) AS JobReceiveNo, " & _
                " IsNull(H.IsDeleted,0) AS IsDeleted, H.Div_Code, " & _
                " IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
                " FROM JobIssRec H "
        HelpDataSet.JobReceive = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT 'Qty' AS Code, 'Qty' AS Name " & _
                " Union ALL " & _
                " SELECT 'Measure' AS Code, 'Measure' AS Name"
        HelpDataSet.BillingType = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.DocID AS Code, H.ManualRefNo AS JobOrderNo FROM JobOrder H "
        HelpDataSet.JobOrder = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.DocID AS Code, H.ManualRefNo AS JobOrderNo FROM JobIssRec H "
        HelpDataSet.JobIssue = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Po.DocId AS Code, Po.ManualRefNo As [Prod.Order No.]  " & _
                " FROM ProdOrder Po "
        HelpDataSet.ProdOrder = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        Dim I As Integer = 0
        Dim bJobReceiveNoStr$ = ""
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        With Dgl2
            If .Rows.Count > 0 Then
                For I = 0 To .Rows.Count - 1
                    If .Item(Col2JobReceive, I).Value <> "" And AgL.StrCmp(.Item(Col2Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                        If bJobReceiveNoStr = "" Then
                            bJobReceiveNoStr = "'" & .AgSelectedValue(Col2JobReceive, I) & "'"
                        Else
                            bJobReceiveNoStr &= "," & "'" & .AgSelectedValue(Col2JobReceive, I) & "'"
                        End If
                    End If
                Next
                Call ProcFill(bJobReceiveNoStr)
                Call ProcFillJobInvoiceBom()
            End If
        End With
    End Sub

    Private Sub ProcFill(ByVal bJobReceiveNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bJobReceiveNoStr = "" Then Exit Sub
            mQry = " SELECT L.DocId AS JobReceive, L.Sr, L.Item, L.DocQty, L.Qty, " & _
                      " L.BillQty, L.LossPer, L.LossQty, L.Unit, " & _
                      " L.MeasurePerPcs, L.DocMeasure, L.TotalMeasure, 0 As LossMeasure, L.BillMeasure, L.MeasureUnit, " & _
                      " L.JobOrder, L.Rate, L.Amount, L.NetAmount, L.JobIssueDocId, L.ProdOrder, L.Remark, L.LotNo " & _
                      " FROM JobReceiveDetail L " & _
                      " WHERE L.DocId IN (" & bJobReceiveNoStr & ")"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                        Dgl1.AgSelectedValue(Col1JobReceive, I) = AgL.XNull(.Rows(I)("JobReceive"))
                        Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(CType(Dgl1.Columns(Col1DocQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1BillQty, I).Value = Format(AgL.VNull(.Rows(I)("BillQty")), "0.".PadRight(CType(Dgl1.Columns(Col1BillQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1LossPer, I).Value = Format(AgL.VNull(.Rows(I)("LossPer")), "0.".PadRight(CType(Dgl1.Columns(Col1LossPer), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1LossQty, I).Value = Format(AgL.VNull(.Rows(I)("LossQty")), "0.".PadRight(CType(Dgl1.Columns(Col1LossQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(CType(Dgl1.Columns(Col1MeasurePerPcs), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1DocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("DocMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1DocMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1LossMeasure, I).Value = Format(AgL.VNull(.Rows(I)("LossMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1LossMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1BillMeasure, I).Value = Format(AgL.VNull(.Rows(I)("BillMeasure")), "0.".PadRight(CType(Dgl1.Columns(Col1BillMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.AgSelectedValue(Col1JobOrder, I) = AgL.XNull(.Rows(I)("JobOrder"))
                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.".PadRight(CType(Dgl1.Columns(Col1Rate), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                        Dgl1.Item(Col1JobIssueDocId, I).Value = AgL.XNull(.Rows(I)("JobIssueDocId"))
                        Dgl1.AgSelectedValue(Col1ProdOrder, I) = AgL.XNull(.Rows(I)("ProdOrder"))
                        Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                        Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))

                        AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("JobReceive")), Dgl1, I, AgL.VNull(.Rows(I)("Sr")))
                    Next I
                End If
            End With
            AgCalcGrid1.Calculation(True)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillPendingJobReceive(ByVal bJobWorker As String, ByVal bV_Date As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            mQry = "SELECT H.DocId As JobReceive, Max(H.ManualRefNo) As ManualRefNo, " & _
                    " Max(H.V_Date) As JobReceiveDate " & _
                    " FROM JobReceiveDetail L " & _
                    " LEFT JOIN JobIssRec H On L.DocId = H.DocId  " & _
                    " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                    " WHERE H.JobWorker = '" & bJobWorker & "'  " & _
                    " And H.V_Date <= '" & bV_Date & "' " & _
                    " And H.Process = '" & TxtProcess.AgSelectedValue & "' " & _
                    " And IsNull(H.IsDeleted,0) = 0 " & _
                    " And ISNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                    " GROUP BY H.DocId " & _
                    " HAVING IsNull(Sum(L.InvoiceQty), 0) = 0 "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl2.RowCount = 1
                Dgl2.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl2.Rows.Add()
                        Dgl2.Item(Col2Select, I).Value = AgLibrary.ClsConstant.StrUnCheckedValue
                        Dgl2.Item(Col2ManualRefNo, I).Value = AgL.XNull(.Rows(I)("ManualRefNo"))
                        Dgl2.AgSelectedValue(Col2JobReceive, I) = AgL.XNull(.Rows(I)("JobReceive"))
                        Dgl2.Item(Col2JobReceiveDate, I).Value = AgL.XNull(.Rows(I)("JobReceiveDate"))
                    Next I
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtJobWorker_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtJobWorker.Validating
        Call ProcFillPendingJobReceive(TxtJobWorker.AgSelectedValue, TxtV_Date.Text)
    End Sub

    Private Sub DGL2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl2.KeyDown
        Try
            If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col2Select
                    If e.KeyCode = Keys.Space Then
                        Try
                            AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col2Select).Index)
                        Catch ex As Exception
                        End Try
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL2_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl2.CellMouseUp
        If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub
        Try
            If Dgl2.Rows.Count = 0 Then Exit Sub
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col2Select
                    Try
                        Call AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col2Select).Index)
                    Catch ex As Exception
                    End Try
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub ProcFillJobInvoiceBom()
    '    Dim I As Integer = 0, J As Integer = 0, K As Integer = 0
    '    Dim DtTemp As DataTable = Nothing
    '    Try
    '        If Dgl1.Rows.Count = 0 Then Exit Sub

    '        If Dgl1.Rows.Count > 0 Then
    '            Dgl3.RowCount = 1
    '            Dgl3.Rows.Clear()
    '            For I = 0 To Dgl1.Rows.Count - 1
    '                If Dgl1.Item(Col1JobOrder, I).Value <> "" Then
    '                    mQry = " SELECT L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.TotalMeasure, L.MeasureUnit, " & _
    '                            " I.Rate " & _
    '                            " FROM JobIssueDetail  L " & _
    '                            " LEFT JOIN JobOrder H On L.JobOrder = H.DocId " & _
    '                            " LEFT JOIN Item I On L.Item = I.Code" & _
    '                            " Where L.JobOrder = '" & Dgl1.Item(Col1JobOrder, I).Value & "' " & _
    '                            " And IsNull(H.JobWithMaterialYN,0) <> 0 "
    '                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '                    With DtTemp
    '                        If .Rows.Count > 0 Then
    '                            For J = 0 To .Rows.Count - 1
    '                                Dgl3.Rows.Add()
    '                                Dgl3.Item(ColSNo, K).Value = Dgl3.Rows.Count
    '                                Dgl3.AgSelectedValue(Col3Item, K) = AgL.XNull(.Rows(J)("Item"))
    '                                Dgl3.Item(Col3Qty, K).Value = AgL.VNull(.Rows(J)("Qty"))
    '                                Dgl3.Item(Col3Unit, K).Value = AgL.XNull(.Rows(J)("Unit"))
    '                                Dgl3.Item(Col3MeasurePerPcs, K).Value = AgL.VNull(.Rows(J)("MeasurePerPcs"))
    '                                Dgl3.Item(Col3TotalMeasure, K).Value = AgL.VNull(.Rows(J)("TotalMeasure"))
    '                                Dgl3.Item(Col3MeasureUnit, K).Value = AgL.XNull(.Rows(J)("MeasureUnit"))
    '                                Dgl3.Item(Col3Rate, K).Value = AgL.VNull(.Rows(J)("Rate"))
    '                                'Dgl3.Item(Col3Amount, k).Value = AgL.VNull(.Rows(J)("Amount"))
    '                                K = K + 1
    '                            Next J
    '                        End If
    '                    End With
    '                End If
    '            Next
    '        End If
    '        Calculation()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub ProcFillJobInvoiceBom()
        Dim I As Integer = 0, J As Integer = 0, K As Integer = 0
        Dim bQry$ = ""
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Rows.Count = 0 Then Exit Sub

            If Dgl1.Rows.Count > 0 Then
                For I = 0 To Dgl1.Rows.Count - 1
                    If Dgl1.Item(Col1JobOrder, I).Value <> "" Then
                        If bQry = "" Then
                            bQry = " SELECT L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.TotalMeasure, L.MeasureUnit " & _
                                      " FROM JobIssueDetail  L " & _
                                      " LEFT JOIN JobOrder H On L.JobOrder = H.DocId " & _
                                      " Where L.JobOrder = '" & Dgl1.AgSelectedValue(Col1JobOrder, I) & "' " & _
                                      " And IsNull(H.JobWithMaterialYN,0) <> 0 "
                        Else
                            bQry += " UNION ALL "
                            bQry += " SELECT L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.TotalMeasure, L.MeasureUnit " & _
                                      " FROM JobIssueDetail  L " & _
                                      " LEFT JOIN JobOrder H On L.JobOrder = H.DocId " & _
                                      " Where L.JobOrder = '" & Dgl1.AgSelectedValue(Col1JobOrder, I) & "' " & _
                                      " And IsNull(H.JobWithMaterialYN,0) <> 0 "
                        End If
                    End If
                Next
            End If

            mQry = " Select V1.Item, Sum(V1.Qty) As Qty, Max(V1.Unit) As Unit, " & _
                    " Max(V1.MeasurePerPcs) As MeasurePerPcs, Sum(V1.TotalMeasure) As TotalMeasure, " & _
                    " Max(V1.MeasureUnit) As MeasureUnit " & _
                    " From (" & bQry & ") As V1 " & _
                    " Group By V1.Item "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl3.RowCount = 1
                Dgl3.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl3.Rows.Add()
                        Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count
                        Dgl3.AgSelectedValue(Col3Item, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl3.Item(Col3Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(3 + 2, "0"))
                        Dgl3.Item(Col3Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl3.Item(Col3MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(3 + 2, "0"))
                        Dgl3.Item(Col3TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(3 + 2, "0"))
                        Dgl3.Item(Col3MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempJobInvoice_BaseEvent_Save_PostTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        AgCL.GridSetiingWriteXml(Me.Text + Dgl1.Name, Dgl1)
    End Sub

    Private Sub AgCalcGrid1_Calculated() Handles AgCalcGrid1.Calculated
        AgCShowGrid1.MoveRec_FromCalcGrid()
        AgCShowGrid2.MoveRec_FromCalcGrid()
    End Sub

    Private Sub Dgl3_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl3.EditingControl_Validating
        Call Calculation()
    End Sub

    Private Sub Dgl1_CellMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl1.CellMouseMove
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1JobReceive, Col1JobOrder, Col1JobIssueDocId, Col1ProdOrder
                    Dgl1.Cursor = Cursors.Hand
                Case Else
                    Dgl1.Cursor = Cursors.Default
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim I As Integer

        'With DsMain.Tables(0)
        '    If .Rows.Count > 0 Then
        '        For I = 0 To DsMain.Tables(0).Rows.Count - 1
        '            mQry = " UPDATE JobReceiveDetail " & _
        '                    " SET InvoiceQty = (SELECT IsNull(Sum(L.Qty),0)  " & _
        '                    " 				   FROM JobInvoiceDetail L " & _
        '                    "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
        '                    "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
        '                    " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
        '                    "                  And L.JobReceive = '" & AgL.XNull(.Rows(I)("JobReceive")) & "' " & _
        '                    "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
        '                    " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
        '                    "                  And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
        '                    "                  And IsNull(H.IsDeleted,0)=0), " & _
        '                    " InvoiceMeasure = (SELECT IsNull(Sum(L.TotalMeasure),0)  " & _
        '                    " 				   FROM JobInvoiceDetail L " & _
        '                    "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
        '                    "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
        '                    " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
        '                    "                  And L.JobReceive = '" & AgL.XNull(.Rows(I)("JobReceive")) & "' " & _
        '                    "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
        '                    " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
        '                    "                  And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
        '                    "                  And IsNull(H.IsDeleted,0)=0), " & _
        '                    " InvoiceAmount = (SELECT IsNull(Sum(L.LossQty),0)  " & _
        '                    " 				   FROM JobInvoiceDetail L " & _
        '                    "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
        '                    "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
        '                    " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
        '                    "                  And L.JobReceive = '" & AgL.XNull(.Rows(I)("JobReceive")) & "' " & _
        '                    "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
        '                    " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
        '                    "                  And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
        '                    "                  And IsNull(H.IsDeleted,0)=0) " & _
        '                    " Where DocId = '" & AgL.XNull(.Rows(I)("JobReceive")) & "' " & _
        '                    " And JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
        '                    " And Item = '" & AgL.XNull(.Rows(I)("Item")) & "'  " & _
        '                    " And IsNull(ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' "
        '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        '            mQry = " UPDATE JobOrderDetail " & _
        '                    " SET InvoiceQty = (SELECT IsNull(Sum(L.Qty),0)  " & _
        '                    " 				   FROM JobInvoiceDetail L " & _
        '                    "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
        '                    "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
        '                    " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
        '                    "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
        '                    " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
        '                    "                  And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
        '                    "                  And IsNull(H.IsDeleted,0)=0), " & _
        '                    " InvoiceMeasure = (SELECT IsNull(Sum(L.TotalMeasure),0)  " & _
        '                    " 				   FROM JobInvoiceDetail L " & _
        '                    "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
        '                    "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
        '                    " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
        '                    "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
        '                    " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
        '                    "                  And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
        '                    "                  And IsNull(H.IsDeleted,0)=0), " & _
        '                    " InvoiceAmount = (SELECT IsNull(Sum(L.LossQty),0)  " & _
        '                    " 				   FROM JobInvoiceDetail L " & _
        '                    "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
        '                    "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
        '                    " 				   WHERE Vt.NCat = '" & EntryNCat & "' " & _
        '                    "                  And L.JobOrder = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
        '                    " 				   And L.Item = '" & AgL.XNull(.Rows(I)("Item")) & "' " & _
        '                    "                  And IsNull(L.ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' " & _
        '                    "                  And IsNull(H.IsDeleted,0)=0) " & _
        '                    " Where DocId = '" & AgL.XNull(.Rows(I)("JobOrder")) & "' " & _
        '                    " And Item = '" & AgL.XNull(.Rows(I)("Item")) & "'  " & _
        '                    " And IsNull(ProdOrder,'') = '" & AgL.XNull(.Rows(I)("ProdOrder")) & "' "
        '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '        Next
        '    End If
        'End With

        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mQry = " UPDATE JobReceiveDetail " & _
                             " SET InvoiceQty = (SELECT IsNull(Sum(L.Qty),0)  " & _
                             " 				    FROM JobInvoiceDetail L " & _
                             "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
                             "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                             " 				    WHERE Vt.NCat = '" & EntryNCat & "' " & _
                             "                  And L.JobReceive = '" & .AgSelectedValue(Col1JobReceive, I) & "' " & _
                             "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrder, I) & "' " & _
                             " 				    And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             "                  And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                             "                  And IsNull(H.IsDeleted,0)=0), " & _
                             " InvoiceMeasure = (SELECT IsNull(Sum(L.TotalMeasure),0)  " & _
                             " 				    FROM JobInvoiceDetail L " & _
                             "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
                             "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                             " 				    WHERE Vt.NCat = '" & EntryNCat & "' " & _
                             "                  And L.JobReceive = '" & .AgSelectedValue(Col1JobReceive, I) & "' " & _
                             "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrder, I) & "' " & _
                             " 				    And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             "                  And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                             "                  And IsNull(H.IsDeleted,0)=0), " & _
                             " InvoiceAmount = (SELECT IsNull(Sum(L.LossQty),0)  " & _
                             " 				    FROM JobInvoiceDetail L " & _
                             "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
                             "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                             " 				    WHERE Vt.NCat = '" & EntryNCat & "' " & _
                             "                  And L.JobReceive = '" & .AgSelectedValue(Col1JobReceive, I) & "' " & _
                             "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrder, I) & "' " & _
                             " 				    And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             "                  And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                             "                  And IsNull(H.IsDeleted,0)=0) " & _
                             " Where DocId = '" & .AgSelectedValue(Col1JobReceive, I) & "' " & _
                             " And JobOrder = '" & .AgSelectedValue(Col1JobOrder, I) & "' " & _
                             " And Item = '" & .AgSelectedValue(Col1Item, I) & "'  " & _
                             " And IsNull(ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = " UPDATE JobOrderDetail " & _
                             " SET InvoiceQty = (SELECT IsNull(Sum(L.Qty),0)  " & _
                             " 				    FROM JobInvoiceDetail L " & _
                             "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
                             "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                             " 				    WHERE Vt.NCat = '" & EntryNCat & "' " & _
                             "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrder, I) & "' " & _
                             " 				    And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             "                  And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                             "                  And IsNull(H.IsDeleted,0)=0), " & _
                             " InvoiceMeasure = (SELECT IsNull(Sum(L.TotalMeasure),0)  " & _
                             " 				    FROM JobInvoiceDetail L " & _
                             "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
                             "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                             " 				    WHERE Vt.NCat = '" & EntryNCat & "' " & _
                             "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrder, I) & "' " & _
                             " 				    And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             "                  And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                             "                  And IsNull(H.IsDeleted,0)=0), " & _
                             " InvoiceAmount = (SELECT IsNull(Sum(L.LossQty),0)  " & _
                             " 				    FROM JobInvoiceDetail L " & _
                             "                  LEFT JOIN JobInvoice H On L.DocId  = H.DocId " & _
                             "                  LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                             " 				    WHERE Vt.NCat = '" & EntryNCat & "' " & _
                             "                  And L.JobOrder = '" & .AgSelectedValue(Col1JobOrder, I) & "' " & _
                             " 				    And L.Item = '" & .AgSelectedValue(Col1Item, I) & "' " & _
                             "                  And IsNull(L.ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' " & _
                             "                  And IsNull(H.IsDeleted,0)=0) " & _
                             " Where DocId = '" & .AgSelectedValue(Col1JobOrder, I) & "' " & _
                             " And Item = '" & .AgSelectedValue(Col1Item, I) & "'  " & _
                             " And IsNull(ProdOrder,'') = '" & .AgSelectedValue(Col1ProdOrder, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Sub
End Class
