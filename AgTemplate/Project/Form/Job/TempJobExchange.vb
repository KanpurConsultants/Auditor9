Imports System.Data.SQLite
Public Class TempJobExchange
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1ReceiveItem As String = "Receive Item"
    Protected Const Col1ReceiveQty As String = "Receive Qty"
    Protected Const Col1ReceiveItemUnit As String = "Receive Item Unit"
    Protected Const Col1ReceiveItemMeasurePerPcs As String = "Receive Item Measure Per Pcs"
    Protected Const Col1ReceiveItemTotalMeasure As String = "Receive Item Total Measure"
    Protected Const Col1ReceiveItemMeasureUnit As String = "Receive Item Measure Unit"
    Protected Const Col1IssueItem As String = "Issue Item"
    Protected Const Col1IssueQty As String = "Issue Qty"
    Protected Const Col1IssueItemUnit As String = "Issue Item Unit"
    Protected Const Col1IssueItemMeasurePerPcs As String = "Issue Item Measure Per Pcs"
    Protected Const Col1IssueItemTotalMeasure As String = "Issue Item Total Measure"
    Protected Const Col1IssueItemMeasureUnit As String = "Issue Item Measure Unit"
    Protected Const Col1LotNo As String = "Lot No"

    Public Class HelpDataSet
        Public Shared Item As DataSet = Nothing
        Public Shared Godown As DataSet = Nothing
        Public Shared Process As DataSet = Nothing
        Public Shared JobWorker As DataSet = Nothing
        Public Shared JobOrder As DataSet = Nothing
        Public Shared LotNo As DataSet = Nothing
    End Class

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
        Me.LblTotalReceivedMeasure = New System.Windows.Forms.Label
        Me.LblTotalIssMeasure = New System.Windows.Forms.Label
        Me.LblTotalIssuedMeasureText = New System.Windows.Forms.Label
        Me.LblTotalReceivedMeasureText = New System.Windows.Forms.Label
        Me.LblTotalIssQty = New System.Windows.Forms.Label
        Me.LblTotalReceivedQty = New System.Windows.Forms.Label
        Me.LblTotalIssuedQtyText = New System.Windows.Forms.Label
        Me.LblTotalReceivedQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LblGodownReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.LblJobExchangeDetail = New System.Windows.Forms.LinkLabel
        Me.TxtJobOrderNo = New AgControls.AgTextBox
        Me.LblJobOrderNo = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
        Me.LblJobOrderNoReq = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(746, 451)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 451)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 451)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 451)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 451)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 447)
        Me.GroupBox1.Size = New System.Drawing.Size(921, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 451)
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
        Me.LblV_No.Location = New System.Drawing.Point(449, 36)
        Me.LblV_No.Size = New System.Drawing.Size(90, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Exchange No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(562, 35)
        Me.TxtV_No.Size = New System.Drawing.Size(130, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(313, 41)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(206, 36)
        Me.LblV_Date.Size = New System.Drawing.Size(97, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Exchange Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(546, 21)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(331, 35)
        Me.TxtV_Date.Size = New System.Drawing.Size(110, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(449, 17)
        Me.LblV_Type.Size = New System.Drawing.Size(98, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Exchange Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(562, 15)
        Me.TxtV_Type.Size = New System.Drawing.Size(130, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(313, 21)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(206, 16)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(331, 15)
        Me.TxtSite_Code.Size = New System.Drawing.Size(110, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(808, 48)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 18)
        Me.TabControl1.Size = New System.Drawing.Size(906, 187)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LblJobOrderNoReq)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.LblProcess)
        Me.TP1.Controls.Add(Me.TxtJobOrderNo)
        Me.TP1.Controls.Add(Me.LblJobOrderNo)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.LblGodownReq)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(898, 161)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobOrderNoReq, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(903, 41)
        Me.Topctrl1.TabIndex = 2
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
        Me.TxtGodown.Location = New System.Drawing.Point(331, 95)
        Me.TxtGodown.MaxLength = 20
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(361, 18)
        Me.TxtGodown.TabIndex = 7
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(206, 96)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 706
        Me.LblGodown.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalReceivedMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalIssMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalIssuedMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalReceivedMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalIssQty)
        Me.Panel1.Controls.Add(Me.LblTotalReceivedQty)
        Me.Panel1.Controls.Add(Me.LblTotalIssuedQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalReceivedQtyText)
        Me.Panel1.Location = New System.Drawing.Point(17, 412)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(869, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalReceivedMeasure
        '
        Me.LblTotalReceivedMeasure.AutoSize = True
        Me.LblTotalReceivedMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalReceivedMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalReceivedMeasure.Location = New System.Drawing.Point(813, 3)
        Me.LblTotalReceivedMeasure.Name = "LblTotalReceivedMeasure"
        Me.LblTotalReceivedMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalReceivedMeasure.TabIndex = 671
        Me.LblTotalReceivedMeasure.Text = "."
        Me.LblTotalReceivedMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalIssMeasure
        '
        Me.LblTotalIssMeasure.AutoSize = True
        Me.LblTotalIssMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalIssMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalIssMeasure.Location = New System.Drawing.Point(365, 3)
        Me.LblTotalIssMeasure.Name = "LblTotalIssMeasure"
        Me.LblTotalIssMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalIssMeasure.TabIndex = 666
        Me.LblTotalIssMeasure.Text = "."
        Me.LblTotalIssMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalIssuedMeasureText
        '
        Me.LblTotalIssuedMeasureText.AutoSize = True
        Me.LblTotalIssuedMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalIssuedMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalIssuedMeasureText.Location = New System.Drawing.Point(210, 3)
        Me.LblTotalIssuedMeasureText.Name = "LblTotalIssuedMeasureText"
        Me.LblTotalIssuedMeasureText.Size = New System.Drawing.Size(150, 16)
        Me.LblTotalIssuedMeasureText.TabIndex = 665
        Me.LblTotalIssuedMeasureText.Text = "Total Issued Measure :"
        '
        'LblTotalReceivedMeasureText
        '
        Me.LblTotalReceivedMeasureText.AutoSize = True
        Me.LblTotalReceivedMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalReceivedMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalReceivedMeasureText.Location = New System.Drawing.Point(637, 3)
        Me.LblTotalReceivedMeasureText.Name = "LblTotalReceivedMeasureText"
        Me.LblTotalReceivedMeasureText.Size = New System.Drawing.Size(169, 16)
        Me.LblTotalReceivedMeasureText.TabIndex = 669
        Me.LblTotalReceivedMeasureText.Text = "Total Received Measure :"
        '
        'LblTotalIssQty
        '
        Me.LblTotalIssQty.AutoSize = True
        Me.LblTotalIssQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalIssQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalIssQty.Location = New System.Drawing.Point(130, 3)
        Me.LblTotalIssQty.Name = "LblTotalIssQty"
        Me.LblTotalIssQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalIssQty.TabIndex = 660
        Me.LblTotalIssQty.Text = "."
        Me.LblTotalIssQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalReceivedQty
        '
        Me.LblTotalReceivedQty.AutoSize = True
        Me.LblTotalReceivedQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalReceivedQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalReceivedQty.Location = New System.Drawing.Point(572, 3)
        Me.LblTotalReceivedQty.Name = "LblTotalReceivedQty"
        Me.LblTotalReceivedQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalReceivedQty.TabIndex = 668
        Me.LblTotalReceivedQty.Text = "."
        Me.LblTotalReceivedQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalIssuedQtyText
        '
        Me.LblTotalIssuedQtyText.AutoSize = True
        Me.LblTotalIssuedQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalIssuedQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalIssuedQtyText.Location = New System.Drawing.Point(7, 3)
        Me.LblTotalIssuedQtyText.Name = "LblTotalIssuedQtyText"
        Me.LblTotalIssuedQtyText.Size = New System.Drawing.Size(117, 16)
        Me.LblTotalIssuedQtyText.TabIndex = 659
        Me.LblTotalIssuedQtyText.Text = "Total Issued Qty :"
        '
        'LblTotalReceivedQtyText
        '
        Me.LblTotalReceivedQtyText.AutoSize = True
        Me.LblTotalReceivedQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalReceivedQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalReceivedQtyText.Location = New System.Drawing.Point(431, 3)
        Me.LblTotalReceivedQtyText.Name = "LblTotalReceivedQtyText"
        Me.LblTotalReceivedQtyText.Size = New System.Drawing.Size(136, 16)
        Me.LblTotalReceivedQtyText.TabIndex = 667
        Me.LblTotalReceivedQtyText.Text = "Total Received Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(17, 234)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(869, 178)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(206, 136)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(331, 135)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(361, 18)
        Me.TxtRemarks.TabIndex = 9
        '
        'LblGodownReq
        '
        Me.LblGodownReq.AutoSize = True
        Me.LblGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblGodownReq.Location = New System.Drawing.Point(313, 101)
        Me.LblGodownReq.Name = "LblGodownReq"
        Me.LblGodownReq.Size = New System.Drawing.Size(10, 7)
        Me.LblGodownReq.TabIndex = 724
        Me.LblGodownReq.Text = "Ä"
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(331, 55)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(361, 18)
        Me.TxtManualRefNo.TabIndex = 5
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(206, 55)
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
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(313, 80)
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
        Me.TxtJobWorker.Location = New System.Drawing.Point(331, 75)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(361, 18)
        Me.TxtJobWorker.TabIndex = 6
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(206, 75)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(74, 16)
        Me.LblJobWorker.TabIndex = 734
        Me.LblJobWorker.Text = "Job Worker"
        '
        'LblJobExchangeDetail
        '
        Me.LblJobExchangeDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobExchangeDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobExchangeDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobExchangeDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobExchangeDetail.LinkColor = System.Drawing.Color.White
        Me.LblJobExchangeDetail.Location = New System.Drawing.Point(16, 213)
        Me.LblJobExchangeDetail.Name = "LblJobExchangeDetail"
        Me.LblJobExchangeDetail.Size = New System.Drawing.Size(143, 20)
        Me.LblJobExchangeDetail.TabIndex = 732
        Me.LblJobExchangeDetail.TabStop = True
        Me.LblJobExchangeDetail.Text = "Job Exchange Detail"
        Me.LblJobExchangeDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtJobOrderNo
        '
        Me.TxtJobOrderNo.AgMandatory = True
        Me.TxtJobOrderNo.AgMasterHelp = False
        Me.TxtJobOrderNo.AgNumberLeftPlaces = 0
        Me.TxtJobOrderNo.AgNumberNegetiveAllow = False
        Me.TxtJobOrderNo.AgNumberRightPlaces = 0
        Me.TxtJobOrderNo.AgPickFromLastValue = False
        Me.TxtJobOrderNo.AgRowFilter = ""
        Me.TxtJobOrderNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobOrderNo.AgSelectedValue = Nothing
        Me.TxtJobOrderNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobOrderNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobOrderNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobOrderNo.Location = New System.Drawing.Point(331, 115)
        Me.TxtJobOrderNo.MaxLength = 255
        Me.TxtJobOrderNo.Name = "TxtJobOrderNo"
        Me.TxtJobOrderNo.Size = New System.Drawing.Size(361, 18)
        Me.TxtJobOrderNo.TabIndex = 8
        '
        'LblJobOrderNo
        '
        Me.LblJobOrderNo.AutoSize = True
        Me.LblJobOrderNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobOrderNo.Location = New System.Drawing.Point(206, 116)
        Me.LblJobOrderNo.Name = "LblJobOrderNo"
        Me.LblJobOrderNo.Size = New System.Drawing.Size(84, 16)
        Me.LblJobOrderNo.TabIndex = 741
        Me.LblJobOrderNo.Text = "Job Order No"
        '
        'TxtProcess
        '
        Me.TxtProcess.AgMandatory = False
        Me.TxtProcess.AgMasterHelp = False
        Me.TxtProcess.AgNumberLeftPlaces = 0
        Me.TxtProcess.AgNumberNegetiveAllow = False
        Me.TxtProcess.AgNumberRightPlaces = 0
        Me.TxtProcess.AgPickFromLastValue = False
        Me.TxtProcess.AgRowFilter = ""
        Me.TxtProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcess.AgSelectedValue = Nothing
        Me.TxtProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProcess.Location = New System.Drawing.Point(766, 113)
        Me.TxtProcess.MaxLength = 255
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(100, 18)
        Me.TxtProcess.TabIndex = 742
        Me.TxtProcess.Visible = False
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(765, 91)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 743
        Me.LblProcess.Text = "Process"
        Me.LblProcess.Visible = False
        '
        'LblJobOrderNoReq
        '
        Me.LblJobOrderNoReq.AutoSize = True
        Me.LblJobOrderNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobOrderNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobOrderNoReq.Location = New System.Drawing.Point(313, 122)
        Me.LblJobOrderNoReq.Name = "LblJobOrderNoReq"
        Me.LblJobOrderNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobOrderNoReq.TabIndex = 744
        Me.LblJobOrderNoReq.Text = "Ä"
        '
        'TempJobExchange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(903, 492)
        Me.Controls.Add(Me.LblJobExchangeDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "TempJobExchange"
        Me.Text = "Template Job Exchange"
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
        Me.Controls.SetChildIndex(Me.LblJobExchangeDetail, 0)
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
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalIssQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalIssuedQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalIssMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblGodownReq As System.Windows.Forms.Label
    Protected WithEvents LblTotalIssuedMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents LblJobExchangeDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtJobOrderNo As AgControls.AgTextBox
    Protected WithEvents LblJobOrderNo As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents LblProcess As System.Windows.Forms.Label
    Protected WithEvents LblTotalReceivedMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalReceivedMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalReceivedQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalReceivedQtyText As System.Windows.Forms.Label
    Protected WithEvents LblJobOrderNoReq As System.Windows.Forms.Label
#End Region

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobIssRec"
        LogTableName = "JobIssRec_Log"
        MainLineTableCsv = "JobExchangeDetail"
        LogLineTableCsv = "JobExchangeDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT J.UID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " J.V_Date AS [Entry Date], J.V_No AS [Entry No], " & _
        '                    " J.ManualRefNo, J.DueDate, " & _
        '                    " Sg.DispName As JobWorker, " & _
        '                    " G.Description As Godown, J.Remarks " & _
        '                    " FROM JobIssRec_Log J " & _
        '                    " LEFT JOIN Voucher_type Vt ON J.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup Sg On J.JobWorker = Sg.SubCode " & _
        '                    " LEFT JOIN Godown G On J.Godown = G.Code " & _
        '                    " Where J.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Exchange Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Exchange No], " &
                        " H.ManualRefNo AS [Manual No], H.Process, H.DueDate AS [Due Date], H.IssQty AS [Issue Qty], H.IssMeasure AS [Issue Measure],  " &
                        " H.RecQty AS [Rec Qty], H.RecMeasure AS [Rec Measure], H.JobReceiveFor AS [Job Receive For], H.Remarks, H.Structure, H.EntryBy AS [Entry By],  " &
                        " H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date],  " &
                        " H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, H.BillingType AS [Billing Type], H.OrderBy AS [ORDER By],  " &
                        " H.TotalWeight AS [Total Weight], H.JobWorkerDocNo AS [Job Worker DocNo], H.TotalConsumptionQty AS [Total Consumption Qty], H.TotalConsumptionMeasure AS [Total Consumption Measure],  " &
                        " H.TotalByProductQty AS [Total By Product Qty], H.TotalByProductMeasure AS [Total By Product Measure], " &
                        " D.Div_Name AS Division, SM.Name AS [Site Name], SGJ.DispName AS [Job Worker Name], G.Description AS Godown, JO.ManualRefNo AS [Job ORDER No] " &
                        " FROM JobIssRec_Log H " &
                        " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
                        " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " &
                        " LEFT JOIN SubGroup SGJ ON SGJ.SubCode=H.JobWorker  " &
                        " LEFT JOIN Godown G ON G.Code = H.Godown   " &
                        " LEFT JOIN JobOrder  JO ON H.JobOrder   =JO.DocID  " &
                        " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT J.DocID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " J.V_Date AS [Entry Date], J.V_No AS [Entry No], " & _
        '                    " J.ManualRefNo, J.DueDate, " & _
        '                    " Sg.DispName As JobWorker, " & _
        '                    " G.Description As Godown, J.Remarks " & _
        '                    " FROM JobIssRec J " & _
        '                    " LEFT JOIN Voucher_type Vt ON J.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup Sg On J.JobWorker = Sg.SubCode " & _
        '                    " LEFT JOIN Godown G On J.Godown = G.Code " & _
        '                    " Where 1=1  " & mCondStr

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Exchange Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Exchange No], " &
                        " H.ManualRefNo AS [Manual No], H.Process, H.DueDate AS [Due Date], H.IssQty AS [Issue Qty], H.IssMeasure AS [Issue Measure],  " &
                        " H.RecQty AS [Rec Qty], H.RecMeasure AS [Rec Measure], H.JobReceiveFor AS [Job Receive For], H.Remarks, H.Structure, H.EntryBy AS [Entry By],  " &
                        " H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date],  " &
                        " H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, H.BillingType AS [Billing Type], H.OrderBy AS [ORDER By],  " &
                        " H.TotalWeight AS [Total Weight], H.JobWorkerDocNo AS [Job Worker DocNo], H.TotalConsumptionQty AS [Total Consumption Qty], H.TotalConsumptionMeasure AS [Total Consumption Measure],  " &
                        " H.TotalByProductQty AS [Total By Product Qty], H.TotalByProductMeasure AS [Total By Product Measure], " &
                        " D.Div_Name AS Division, SM.Name AS [Site Name], SGJ.DispName AS [Job Worker Name], G.Description AS Godown, JO.ManualRefNo AS [Job ORDER No] " &
                        " FROM JobIssRec H " &
                        " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
                        " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " &
                        " LEFT JOIN SubGroup SGJ ON SGJ.SubCode=H.JobWorker  " &
                        " LEFT JOIN Godown G ON G.Code = H.Godown   " &
                        " LEFT JOIN JobOrder  JO ON H.JobOrder   =JO.DocID  " &
                        " Where 1=1  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select J.DocID As SearchCode " &
                " From JobIssRec J " &
                " Left Join Voucher_Type Vt On J.V_Type = Vt.V_Type  " &
                " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By J.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And J.EntryStatus='" & LogStatus.LogOpen & "' "

        mQry = " Select J.UID As SearchCode " &
            " From JobIssRec_Log J " &
            " Left Join Voucher_Type Vt On J.V_Type = Vt.V_Type  " &
            " Where 1=1  " & mCondStr & "  Order By J.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ReceiveItem, 200, 5, Col1ReceiveItem, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1ReceiveQty, 100, 5, 4, False, Col1ReceiveQty, True, False)
            .AddAgTextColumn(Dgl1, Col1ReceiveItemUnit, 100, 5, Col1ReceiveItemUnit, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1ReceiveItemMeasurePerPcs, 100, 5, 4, False, Col1ReceiveItemMeasurePerPcs, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReceiveItemTotalMeasure, 100, 5, 4, False, Col1ReceiveItemTotalMeasure, True, True)
            .AddAgTextColumn(Dgl1, Col1ReceiveItemMeasureUnit, 100, 5, Col1ReceiveItemMeasureUnit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1IssueItem, 200, 5, Col1IssueItem, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1IssueQty, 100, 5, 4, False, Col1IssueQty, True, False)
            .AddAgTextColumn(Dgl1, Col1IssueItemUnit, 100, 5, Col1IssueItemUnit, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1IssueItemMeasurePerPcs, 100, 5, 4, False, Col1IssueItemMeasurePerPcs, True, True)
            .AddAgNumberColumn(Dgl1, Col1IssueItemTotalMeasure, 100, 5, 4, False, Col1IssueItemTotalMeasure, True, True)
            .AddAgTextColumn(Dgl1, Col1IssueItemMeasureUnit, 100, 5, Col1IssueItemMeasureUnit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 5, Col1LotNo, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        FrmProductionOrder_BaseFunction_FIniList()
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True

        FrmProductionOrder_BaseFunction_FIniList()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE JobIssRec_Log " &
                " SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " JobWorker = " & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " &
                " Godown = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " IssQty = " & Val(LblTotalIssQty.Text) & ", " &
                " IssMeasure = " & Val(LblTotalIssMeasure.Text) & ", " &
                " RecQty = " & Val(LblTotalReceivedQty.Text) & ", " &
                " RecMeasure = " & Val(LblTotalReceivedMeasure.Text) & ", " &
                " JobOrder = " & AgL.Chk_Text(TxtJobOrderNo.AgSelectedValue) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & " " &
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobExchangeDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Item(Col1ReceiveItem, I).Value <> "" Then
                    mSr += 1
                    mQry = " INSERT INTO JobExchangeDetail_Log (UID, DocId, Sr, ReceiveItem, " &
                            " ReceiveQty, ReceiveItemUnit, " &
                            " ReceiveItemMeasurePerPcs, ReceiveItemTotalMeasure, " &
                            " ReceiveItemMeasureUnit, IssueItem, IssueQty, IssueItemUnit, " &
                            " IssueItemMeasurePerPcs, IssueItemTotalMeasure, IssueItemMeasureUnit, " &
                            " JobOrder) " &
                            " Values (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & "," &
                            " " & mSr & ", " & AgL.Chk_Text(.AgSelectedValue(Col1ReceiveItem, I)) & ", " &
                            " " & Val(.Item(Col1ReceiveQty, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ReceiveItemUnit, I).Value) & ", " &
                            " " & Val(.Item(Col1ReceiveItemMeasurePerPcs, I).Value) & ", " &
                            " " & Val(.Item(Col1ReceiveItemTotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ReceiveItemMeasureUnit, I).Value) & ", " &
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1IssueItem, I)) & ", " &
                            " " & Val(.Item(Col1IssueQty, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1IssueItemUnit, I).Value) & ", " &
                            " " & Val(.Item(Col1IssueItemMeasurePerPcs, I).Value) & ", " &
                            " " & Val(.Item(Col1IssueItemTotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1IssueItemMeasureUnit, I).Value) & ", " &
                            " " & AgL.Chk_Text(TxtJobOrderNo.AgSelectedValue) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select J.* " &
                " From JobIssRec J " &
                " Where J.DocID='" & SearchCode & "'"
        Else
            mQry = "Select J.* " &
                " From JobIssRec_Log J " &
                " Where J.UID='" & SearchCode & "'"

        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                IniGrid()
                TxtGodown.AgSelectedValue = AgL.XNull(.Rows(0)("Godown"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtJobWorker.AgSelectedValue = AgL.XNull(.Rows(0)("JobWorker"))
                TxtProcess.AgSelectedValue = AgL.XNull(.Rows(0)("Process"))
                TxtJobOrderNo.AgSelectedValue = AgL.XNull(.Rows(0)("JobOrder"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalIssQty.Text = AgL.VNull(.Rows(0)("IssQty"))
                LblTotalIssMeasure.Text = AgL.VNull(.Rows(0)("IssMeasure"))
                LblTotalReceivedQty.Text = AgL.VNull(.Rows(0)("RecQty"))
                LblTotalReceivedMeasure.Text = AgL.VNull(.Rows(0)("RecMeasure"))

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from JobExchangeDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from JobExchangeDetail_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1ReceiveItem, I) = AgL.XNull(.Rows(I)("ReceiveItem"))
                            Dgl1.Item(Col1ReceiveQty, I).Value = AgL.VNull(.Rows(I)("ReceiveQty"))
                            Dgl1.Item(Col1ReceiveItemUnit, I).Value = AgL.XNull(.Rows(I)("ReceiveItemUnit"))
                            Dgl1.Item(Col1ReceiveItemMeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("ReceiveItemMeasurePerPcs"))
                            Dgl1.Item(Col1ReceiveItemTotalMeasure, I).Value = AgL.VNull(.Rows(I)("ReceiveItemTotalMeasure"))
                            Dgl1.Item(Col1ReceiveItemMeasureUnit, I).Value = AgL.XNull(.Rows(I)("ReceiveItemMeasureUnit"))
                            Dgl1.AgSelectedValue(Col1IssueItem, I) = AgL.XNull(.Rows(I)("IssueItem"))
                            Dgl1.Item(Col1IssueQty, I).Value = AgL.VNull(.Rows(I)("IssueQty"))
                            Dgl1.Item(Col1IssueItemUnit, I).Value = AgL.XNull(.Rows(I)("IssueItemUnit"))
                            Dgl1.Item(Col1IssueItemMeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("IssueItemMeasurePerPcs"))
                            Dgl1.Item(Col1IssueItemTotalMeasure, I).Value = AgL.VNull(.Rows(I)("IssueItemTotalMeasure"))
                            Dgl1.Item(Col1IssueItemMeasureUnit, I).Value = AgL.XNull(.Rows(I)("IssueItemMeasureUnit"))

                            RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
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
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating
        Select Case sender.NAME
            Case TxtV_Type.Name
                IniGrid()
        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        IniGrid()
        TxtManualRefNo.Text = TxtV_No.Text.ToString
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        Dgl1.AgHelpDataSet(Col1ReceiveItem, 8) = HelpDataSet.Item
        Dgl1.AgHelpDataSet(Col1IssueItem, 8) = HelpDataSet.Item
        TxtGodown.AgHelpDataSet(5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Godown
        TxtJobWorker.AgHelpDataSet(5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobWorker
        TxtJobOrderNo.AgHelpDataSet(7, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobOrder
        TxtProcess.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Process
        Dgl1.AgHelpDataSet(Col1LotNo) = HelpDataSet.LotNo
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
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
                Case Col1ReceiveItem
                    If Dgl1.Item(Col1ReceiveItem, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1ReceiveItem, mRowIndex).ToString.Trim = "" Then
                        Dgl1.Item(Col1ReceiveItemUnit, mRowIndex).Value = ""
                        Dgl1.Item(Col1ReceiveItemMeasurePerPcs, mRowIndex).Value = 0
                        Dgl1.Item(Col1ReceiveItemMeasureUnit, mRowIndex).Value = ""
                    Else
                        If Dgl1.AgDataRow IsNot Nothing Then
                            Dgl1.Item(Col1ReceiveItemUnit, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                            Dgl1.Item(Col1ReceiveItemMeasurePerPcs, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Measure").Value)
                            Dgl1.Item(Col1ReceiveItemMeasureUnit, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                        End If
                    End If

                Case Col1IssueItem
                    If Dgl1.Item(Col1IssueItem, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1IssueItem, mRowIndex).ToString.Trim = "" Then
                        Dgl1.Item(Col1IssueItemUnit, mRowIndex).Value = ""
                        Dgl1.Item(Col1IssueItemMeasurePerPcs, mRowIndex).Value = 0
                        Dgl1.Item(Col1IssueItemMeasureUnit, mRowIndex).Value = ""
                    Else
                        If Dgl1.AgDataRow IsNot Nothing Then
                            Dgl1.Item(Col1IssueItemUnit, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                            Dgl1.Item(Col1IssueItemMeasurePerPcs, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Measure").Value)
                            Dgl1.Item(Col1IssueItemMeasureUnit, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                            If Val(Dgl1.Item(Col1IssueQty, mRowIndex).Value) = 0 Then Dgl1.Item(Col1IssueQty, mRowIndex).Value = Val(Dgl1.Item(Col1ReceiveQty, mRowIndex).Value)
                        End If
                    End If
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

        LblTotalIssQty.Text = 0 : LblTotalIssMeasure.Text = 0
        LblTotalReceivedQty.Text = 0 : LblTotalReceivedMeasure.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1ReceiveItem, I).Value <> "" Then
                Dgl1.Item(Col1ReceiveItemTotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1ReceiveQty, I).Value) * Val(Dgl1.Item(Col1ReceiveItemMeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1ReceiveItemTotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Dgl1.Item(Col1IssueItemTotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1IssueQty, I).Value) * Val(Dgl1.Item(Col1IssueItemMeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1IssueItemTotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                'Footer Calculation
                LblTotalIssQty.Text = Val(LblTotalIssQty.Text) + Val(Dgl1.Item(Col1IssueQty, I).Value)
                LblTotalIssMeasure.Text = Val(LblTotalIssMeasure.Text) + Val(Dgl1.Item(Col1IssueItemTotalMeasure, I).Value)
                LblTotalReceivedQty.Text = Val(LblTotalReceivedQty.Text) + Val(Dgl1.Item(Col1ReceiveQty, I).Value)
                LblTotalReceivedMeasure.Text = Val(LblTotalReceivedMeasure.Text) + Val(Dgl1.Item(Col1ReceiveItemTotalMeasure, I).Value)
            End If
        Next
        LblTotalIssQty.Text = Val(LblTotalIssQty.Text)
        LblTotalIssMeasure.Text = Val(LblTotalIssMeasure.Text)
        LblTotalReceivedQty.Text = Val(LblTotalReceivedQty.Text)
        LblTotalReceivedMeasure.Text = Val(LblTotalReceivedMeasure.Text)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtGodown, LblGodown.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtJobWorker, LblJobWorker.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtJobOrderNo, LblJobOrderNo.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1ReceiveItem).Index) = True Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1ReceiveItem, I).Value <> "" Then
                    If .Item(Col1IssueItem, I).Value = "" Then
                        MsgBox("Issued Item Is Blank At Row No " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                        .CurrentCell = .Item(Col1IssueItem, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1ReceiveQty, I).Value) = 0 Then
                        MsgBox("Receive Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                        .CurrentCell = .Item(Col1ReceiveQty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1IssueQty, I).Value) = 0 Then
                        MsgBox("Issue Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                        .CurrentCell = .Item(Col1IssueQty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1ReceiveQty, I).Value) <> Val(.Item(Col1IssueQty, I).Value) Then
                        If MsgBox("Received Qty And Issued Qty Are Not Equal At Line No " & Dgl1.Item(ColSNo, I).Value & "." & vbCrLf & "Do You Want To Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then
                            .CurrentCell = .Item(Col1IssueQty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If

                    If AgTemplate.ClsMain.FunRetStock(.AgSelectedValue(Col1IssueItem, I), mInternalCode, , TxtGodown.AgSelectedValue, , AgTemplate.ClsMain.StockStatus.Standard, TxtV_Date.Text) < Val(.Item(Col1IssueQty, I).Value) Then
                        If MsgBox("Qty of " & .Item(Col1IssueItem, I).Value & " In " & TxtGodown.Text & " is less than " & Dgl1.Item(Col1IssueQty, I).Value & "", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            .CurrentCell = .Item(Col1IssueQty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtGodown.Enter
        Select Case sender.name
            Case TxtGodown.Name
                TxtGodown.AgRowFilter = " Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " &
                    " And IsDeleted = 0 " &
                    " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                    " And " & AgTemplate.ClsMain.RetDivFilterStr & " "
        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalIssMeasure.Text = 0 : LblTotalIssQty.Text = 0
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim I As Integer = 0, mSr As Integer = 0
        Dim Stock As AgTemplate.ClsMain.StructStock = Nothing

        mQry = "Delete From Stock Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1ReceiveItem, I).Value <> "" Then
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
                    .Currency = ""
                    .SalesTaxGroupParty = ""
                    '.Structure = ""
                    .BillingType = ""
                    .Item = Dgl1.AgSelectedValue(Col1IssueItem, I)
                    .ProcessGroup = ""
                    .Godown = TxtGodown.AgSelectedValue
                    .Qty_Iss = Dgl1.Item(Col1IssueQty, I).Value
                    .Qty_Rec = 0
                    .Unit = Dgl1.Item(Col1IssueItemUnit, I).Value
                    .MeasurePerPcs = Val(Dgl1.Item(Col1IssueItemMeasurePerPcs, I).Value)
                    .Measure_Iss = Val(Dgl1.Item(Col1IssueItemTotalMeasure, I).Value)
                    .Measure_Rec = 0
                    .MeasureUnit = Dgl1.Item(Col1IssueItemMeasureUnit, I).Value
                    .Rate = 0
                    .Amount = 0
                    .Addition = 0
                    .Deduction = 0
                    .NetAmount = 0
                    .Remarks = TxtRemarks.Text
                    .Status = AgTemplate.ClsMain.StockStatus.Standard
                    .Process = ""
                    .FIFORate = 0
                    .FIFOAmt = 0
                    .AVGRate = 0
                    .AVGAmt = 0
                    .Cost = 0
                    .Doc_Qty = 0
                    .ReferenceDocID = ""
                    '.Item_UID = ""
                    .LotNo = ""
                End With
                Call AgTemplate.ClsMain.ProcStockPost("Stock", Stock, Conn, Cmd)

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
                    .Currency = ""
                    .SalesTaxGroupParty = ""
                    '.Structure = ""
                    .BillingType = ""
                    .Item = Dgl1.AgSelectedValue(Col1ReceiveItem, I)
                    .ProcessGroup = ""
                    .Godown = TxtGodown.AgSelectedValue
                    .Qty_Iss = 0
                    .Qty_Rec = Dgl1.Item(Col1ReceiveQty, I).Value
                    .Unit = Dgl1.Item(Col1IssueItemUnit, I).Value
                    .MeasurePerPcs = Val(Dgl1.Item(Col1IssueItemMeasurePerPcs, I).Value)
                    .Measure_Iss = 0
                    .Measure_Rec = Val(Dgl1.Item(Col1IssueItemTotalMeasure, I).Value)
                    .MeasureUnit = Dgl1.Item(Col1IssueItemMeasureUnit, I).Value
                    .Rate = 0
                    .Amount = 0
                    .Addition = 0
                    .Deduction = 0
                    .NetAmount = 0
                    .Remarks = TxtRemarks.Text
                    .Status = AgTemplate.ClsMain.StockStatus.Standard
                    .Process = ""
                    .FIFORate = 0
                    .FIFOAmt = 0
                    .AVGRate = 0
                    .AVGAmt = 0
                    .Cost = 0
                    .Doc_Qty = 0
                    .ReferenceDocID = ""
                    '.Item_UID = ""
                    .LotNo = ""
                End With
                Call AgTemplate.ClsMain.ProcStockPost("Stock", Stock, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub TempJobOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "Delete From Stock Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub TempJobIssue_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = " SELECT I.Code, I.Description AS Item, I.ItemType, I.SalesTaxPostingGroup, I.Unit, " &
                " I.Measure, I.MeasureUnit, IfNull(I.IsDeleted,0) AS IsDeleted, " &
                " IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status , I.Div_Code " &
                " FROM Item I  "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT G.Code, G.Description, Sm.ManualCode As Site, G.Site_Code, G.Div_Code, IfNull(G.IsDeleted,0) as IsDeleted, " &
                " IfNull(G.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status " &
                " FROM Godown G " &
                " LEFT JOIN SiteMast Sm On G.Site_Code = Sm.Code " &
                " Order By G.Description"
        HelpDataSet.Godown = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select P.NCat As Code, Vc.NCatDescription As Process, P.Div_Code " &
                " From Process P " &
                " LEFT JOIN VoucherCat Vc On P.NCat  = Vc.NCat " &
                " Order By Vc.NCatDescription "
        HelpDataSet.Process = AgL.FillData(mQry, AgL.GCn)

        'mQry = "SELECT S.SubCode AS Code, S.DispName AS JobWorker " & _
        '      " FROM JobWorker J " & _
        '      " LEFT JOIN SubGroup S ON J.SubCode = S.SubCode "

        mQry = " SELECT J.SubCode AS Code, Sg.Name AS JobWorker, H.Process, " &
                " IfNull(Sg.IsDeleted,0) AS IsDeleted, SG.Div_Code, SG.Site_Code, " &
                " IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " &
                " FROM JobWorker J " &
                " LEFT JOIN JobWorkerProcess H On J.SubCode = H.SubCode  " &
                " LEFT JOIN SubGroup Sg ON J.SubCode = Sg.SubCode "
        HelpDataSet.JobWorker = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT J.DocId, J.ManualRefNo As JobOrderNo, " &
                " IfNull(J.IsDeleted,0) As IsDeleted, J.Div_Code, J.Site_Code, " &
                " IfNull(J.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, " &
                " J.JobWorker, Vt.NCat, J.V_Date As JobOrderDate " &
                " FROM JobOrder J " &
                " LEFT JOIN Voucher_Type Vt ON J.V_Type = Vt.V_Type "
        HelpDataSet.JobOrder = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT DISTINCT S.LotNo AS Code, S.LotNo FROM Stock S WHERE S.LotNo IS NOT NULL  "
        HelpDataSet.LotNo = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ReceiveItem, Col1IssueItem
                    Dgl1.AgRowFilter(Dgl1.Columns(Col1ReceiveItem).Index) = " IsDeleted = 0 " &
                        " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  "
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtJobOrderNo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtJobOrderNo.Enter, TxtJobWorker.Enter
        Try
            Select Case sender.Name
                Case TxtJobOrderNo.Name
                    TxtJobOrderNo.AgRowFilter = " IsDeleted = 0 " &
                        " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                        " And " & AgTemplate.ClsMain.RetDivFilterStr & " " &
                        " And Site_Code = '" & AgL.PubSiteCode & "' " &
                        " And JobOrderDate <= '" & TxtV_Date.Text & "' " &
                        " And JobWorker = '" & TxtJobWorker.AgSelectedValue & "'"

                Case TxtJobWorker.Name
                    TxtJobWorker.AgRowFilter = " IsDeleted = 0 " &
                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                            " And " & AgTemplate.ClsMain.RetDivFilterStr & " " &
                            " And Site_Code = '" & AgL.PubSiteCode & "' " &
                            " And Process = '" & TxtProcess.AgSelectedValue & "' "
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
