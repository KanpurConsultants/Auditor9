Imports System.Data.SQLite
Public Class TempJobWorkerRateMaster
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1WEF As String = "WEF"
    Protected Const Col1JobWorker As String = "Job Worker"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1RateOutSide As String = "Rate Outside"

    Public Class HelpDataSet
        Public Shared JobWorker As DataSet = Nothing
        Public Shared Process As DataSet = Nothing
        Public Shared ItemCategory As DataSet = Nothing
        Public Shared ItemGroup As DataSet = Nothing
        Public Shared JobWorkerRateGroup As DataSet = Nothing
        Public Shared Item As DataSet = Nothing
    End Class

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblProcessReq = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.LblPaymentDetail = New System.Windows.Forms.LinkLabel
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.LblStructure = New System.Windows.Forms.Label
        Me.TxtItemCategory = New AgControls.AgTextBox
        Me.LblItemCategory = New System.Windows.Forms.Label
        Me.TxtItemGroup = New AgControls.AgTextBox
        Me.LblItemGroup = New System.Windows.Forms.Label
        Me.TxtItem = New AgControls.AgTextBox
        Me.LblItem = New System.Windows.Forms.Label
        Me.TxtGeneralRate = New AgControls.AgTextBox
        Me.LblGeneralRate = New System.Windows.Forms.Label
        Me.TxtGeneralRateWithoutMaterial = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtJobWorkerRateGroup = New AgControls.AgTextBox
        Me.LblRateGroup = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(863, 41)
        Me.Topctrl1.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 443)
        Me.GroupBox1.Size = New System.Drawing.Size(865, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(4, 450)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(145, 450)
        Me.GBoxEntryType.Size = New System.Drawing.Size(121, 44)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Size = New System.Drawing.Size(115, 18)
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(590, 450)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(121, 44)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 23)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(115, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(427, 450)
        Me.GBoxApprove.Size = New System.Drawing.Size(143, 44)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Size = New System.Drawing.Size(85, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(114, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(731, 450)
        Me.GroupBox2.Size = New System.Drawing.Size(121, 44)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(286, 450)
        Me.GBoxDivision.Size = New System.Drawing.Size(121, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(115, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Size = New System.Drawing.Size(89, 18)
        Me.TxtStatus.Tag = ""
        '
        'LblProcessReq
        '
        Me.LblProcessReq.AutoSize = True
        Me.LblProcessReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblProcessReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblProcessReq.Location = New System.Drawing.Point(299, 59)
        Me.LblProcessReq.Name = "LblProcessReq"
        Me.LblProcessReq.Size = New System.Drawing.Size(10, 7)
        Me.LblProcessReq.TabIndex = 677
        Me.LblProcessReq.Text = "Ä"
        '
        'TxtProcess
        '
        Me.TxtProcess.AgMandatory = True
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
        Me.TxtProcess.Location = New System.Drawing.Point(318, 52)
        Me.TxtProcess.MaxLength = 5
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(345, 18)
        Me.TxtProcess.TabIndex = 0
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(199, 53)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 675
        Me.LblProcess.Text = "Process"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(126, 211)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(634, 217)
        Me.Pnl1.TabIndex = 7
        '
        'LblPaymentDetail
        '
        Me.LblPaymentDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblPaymentDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblPaymentDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaymentDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblPaymentDetail.LinkColor = System.Drawing.Color.White
        Me.LblPaymentDetail.Location = New System.Drawing.Point(126, 189)
        Me.LblPaymentDetail.Name = "LblPaymentDetail"
        Me.LblPaymentDetail.Size = New System.Drawing.Size(119, 20)
        Me.LblPaymentDetail.TabIndex = 734
        Me.LblPaymentDetail.TabStop = True
        Me.LblPaymentDetail.Text = "Job Worker Rate"
        Me.LblPaymentDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(5, 71)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(73, 36)
        Me.PnlCalcGrid.TabIndex = 737
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
        Me.TxtStructure.Location = New System.Drawing.Point(46, 113)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(33, 18)
        Me.TxtStructure.TabIndex = 738
        Me.TxtStructure.Visible = False
        '
        'LblStructure
        '
        Me.LblStructure.AutoSize = True
        Me.LblStructure.BackColor = System.Drawing.Color.Transparent
        Me.LblStructure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStructure.Location = New System.Drawing.Point(43, 134)
        Me.LblStructure.Name = "LblStructure"
        Me.LblStructure.Size = New System.Drawing.Size(61, 16)
        Me.LblStructure.TabIndex = 739
        Me.LblStructure.Text = "Structure"
        Me.LblStructure.Visible = False
        '
        'TxtItemCategory
        '
        Me.TxtItemCategory.AgMandatory = False
        Me.TxtItemCategory.AgMasterHelp = False
        Me.TxtItemCategory.AgNumberLeftPlaces = 0
        Me.TxtItemCategory.AgNumberNegetiveAllow = False
        Me.TxtItemCategory.AgNumberRightPlaces = 0
        Me.TxtItemCategory.AgPickFromLastValue = False
        Me.TxtItemCategory.AgRowFilter = ""
        Me.TxtItemCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemCategory.AgSelectedValue = Nothing
        Me.TxtItemCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemCategory.Location = New System.Drawing.Point(318, 72)
        Me.TxtItemCategory.MaxLength = 5
        Me.TxtItemCategory.Name = "TxtItemCategory"
        Me.TxtItemCategory.Size = New System.Drawing.Size(345, 18)
        Me.TxtItemCategory.TabIndex = 1
        '
        'LblItemCategory
        '
        Me.LblItemCategory.AutoSize = True
        Me.LblItemCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemCategory.Location = New System.Drawing.Point(199, 74)
        Me.LblItemCategory.Name = "LblItemCategory"
        Me.LblItemCategory.Size = New System.Drawing.Size(89, 16)
        Me.LblItemCategory.TabIndex = 741
        Me.LblItemCategory.Text = "Item Category"
        '
        'TxtItemGroup
        '
        Me.TxtItemGroup.AgMandatory = False
        Me.TxtItemGroup.AgMasterHelp = False
        Me.TxtItemGroup.AgNumberLeftPlaces = 0
        Me.TxtItemGroup.AgNumberNegetiveAllow = False
        Me.TxtItemGroup.AgNumberRightPlaces = 0
        Me.TxtItemGroup.AgPickFromLastValue = False
        Me.TxtItemGroup.AgRowFilter = ""
        Me.TxtItemGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemGroup.AgSelectedValue = Nothing
        Me.TxtItemGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemGroup.Location = New System.Drawing.Point(318, 92)
        Me.TxtItemGroup.MaxLength = 5
        Me.TxtItemGroup.Name = "TxtItemGroup"
        Me.TxtItemGroup.Size = New System.Drawing.Size(345, 18)
        Me.TxtItemGroup.TabIndex = 2
        '
        'LblItemGroup
        '
        Me.LblItemGroup.AutoSize = True
        Me.LblItemGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemGroup.Location = New System.Drawing.Point(199, 94)
        Me.LblItemGroup.Name = "LblItemGroup"
        Me.LblItemGroup.Size = New System.Drawing.Size(72, 16)
        Me.LblItemGroup.TabIndex = 744
        Me.LblItemGroup.Text = "Item Group"
        '
        'TxtItem
        '
        Me.TxtItem.AgMandatory = False
        Me.TxtItem.AgMasterHelp = False
        Me.TxtItem.AgNumberLeftPlaces = 0
        Me.TxtItem.AgNumberNegetiveAllow = False
        Me.TxtItem.AgNumberRightPlaces = 0
        Me.TxtItem.AgPickFromLastValue = False
        Me.TxtItem.AgRowFilter = ""
        Me.TxtItem.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItem.AgSelectedValue = Nothing
        Me.TxtItem.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItem.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItem.Location = New System.Drawing.Point(318, 112)
        Me.TxtItem.MaxLength = 5
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(345, 18)
        Me.TxtItem.TabIndex = 3
        '
        'LblItem
        '
        Me.LblItem.AutoSize = True
        Me.LblItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItem.Location = New System.Drawing.Point(199, 114)
        Me.LblItem.Name = "LblItem"
        Me.LblItem.Size = New System.Drawing.Size(33, 16)
        Me.LblItem.TabIndex = 747
        Me.LblItem.Text = "Item"
        '
        'TxtGeneralRate
        '
        Me.TxtGeneralRate.AgMandatory = False
        Me.TxtGeneralRate.AgMasterHelp = False
        Me.TxtGeneralRate.AgNumberLeftPlaces = 7
        Me.TxtGeneralRate.AgNumberNegetiveAllow = False
        Me.TxtGeneralRate.AgNumberRightPlaces = 2
        Me.TxtGeneralRate.AgPickFromLastValue = False
        Me.TxtGeneralRate.AgRowFilter = ""
        Me.TxtGeneralRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGeneralRate.AgSelectedValue = Nothing
        Me.TxtGeneralRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGeneralRate.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtGeneralRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGeneralRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGeneralRate.Location = New System.Drawing.Point(318, 152)
        Me.TxtGeneralRate.MaxLength = 10
        Me.TxtGeneralRate.Name = "TxtGeneralRate"
        Me.TxtGeneralRate.Size = New System.Drawing.Size(104, 18)
        Me.TxtGeneralRate.TabIndex = 5
        '
        'LblGeneralRate
        '
        Me.LblGeneralRate.AutoSize = True
        Me.LblGeneralRate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGeneralRate.Location = New System.Drawing.Point(199, 154)
        Me.LblGeneralRate.Name = "LblGeneralRate"
        Me.LblGeneralRate.Size = New System.Drawing.Size(116, 16)
        Me.LblGeneralRate.TabIndex = 749
        Me.LblGeneralRate.Text = "Rate With Material"
        '
        'TxtGeneralRateWithoutMaterial
        '
        Me.TxtGeneralRateWithoutMaterial.AgMandatory = False
        Me.TxtGeneralRateWithoutMaterial.AgMasterHelp = False
        Me.TxtGeneralRateWithoutMaterial.AgNumberLeftPlaces = 7
        Me.TxtGeneralRateWithoutMaterial.AgNumberNegetiveAllow = False
        Me.TxtGeneralRateWithoutMaterial.AgNumberRightPlaces = 2
        Me.TxtGeneralRateWithoutMaterial.AgPickFromLastValue = False
        Me.TxtGeneralRateWithoutMaterial.AgRowFilter = ""
        Me.TxtGeneralRateWithoutMaterial.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGeneralRateWithoutMaterial.AgSelectedValue = Nothing
        Me.TxtGeneralRateWithoutMaterial.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGeneralRateWithoutMaterial.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtGeneralRateWithoutMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGeneralRateWithoutMaterial.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGeneralRateWithoutMaterial.Location = New System.Drawing.Point(558, 152)
        Me.TxtGeneralRateWithoutMaterial.MaxLength = 10
        Me.TxtGeneralRateWithoutMaterial.Name = "TxtGeneralRateWithoutMaterial"
        Me.TxtGeneralRateWithoutMaterial.Size = New System.Drawing.Size(105, 18)
        Me.TxtGeneralRateWithoutMaterial.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(425, 154)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 16)
        Me.Label1.TabIndex = 751
        Me.Label1.Text = "Rate Without Material"
        '
        'TxtJobWorkerRateGroup
        '
        Me.TxtJobWorkerRateGroup.AgMandatory = False
        Me.TxtJobWorkerRateGroup.AgMasterHelp = False
        Me.TxtJobWorkerRateGroup.AgNumberLeftPlaces = 0
        Me.TxtJobWorkerRateGroup.AgNumberNegetiveAllow = False
        Me.TxtJobWorkerRateGroup.AgNumberRightPlaces = 0
        Me.TxtJobWorkerRateGroup.AgPickFromLastValue = False
        Me.TxtJobWorkerRateGroup.AgRowFilter = ""
        Me.TxtJobWorkerRateGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobWorkerRateGroup.AgSelectedValue = Nothing
        Me.TxtJobWorkerRateGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobWorkerRateGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobWorkerRateGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobWorkerRateGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobWorkerRateGroup.Location = New System.Drawing.Point(318, 132)
        Me.TxtJobWorkerRateGroup.MaxLength = 5
        Me.TxtJobWorkerRateGroup.Name = "TxtJobWorkerRateGroup"
        Me.TxtJobWorkerRateGroup.Size = New System.Drawing.Size(345, 18)
        Me.TxtJobWorkerRateGroup.TabIndex = 4
        Me.TxtJobWorkerRateGroup.Text = "TxtJobWorkerRateGroup"
        '
        'LblRateGroup
        '
        Me.LblRateGroup.AutoSize = True
        Me.LblRateGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRateGroup.Location = New System.Drawing.Point(199, 134)
        Me.LblRateGroup.Name = "LblRateGroup"
        Me.LblRateGroup.Size = New System.Drawing.Size(74, 16)
        Me.LblRateGroup.TabIndex = 753
        Me.LblRateGroup.Text = "Rate Group"
        '
        'TempJobWorkerRateMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(863, 494)
        Me.Controls.Add(Me.TxtJobWorkerRateGroup)
        Me.Controls.Add(Me.LblRateGroup)
        Me.Controls.Add(Me.TxtGeneralRateWithoutMaterial)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtGeneralRate)
        Me.Controls.Add(Me.LblGeneralRate)
        Me.Controls.Add(Me.TxtItem)
        Me.Controls.Add(Me.LblItem)
        Me.Controls.Add(Me.TxtItemGroup)
        Me.Controls.Add(Me.LblItemGroup)
        Me.Controls.Add(Me.TxtItemCategory)
        Me.Controls.Add(Me.LblItemCategory)
        Me.Controls.Add(Me.TxtStructure)
        Me.Controls.Add(Me.LblStructure)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LblPaymentDetail)
        Me.Controls.Add(Me.LblProcessReq)
        Me.Controls.Add(Me.TxtProcess)
        Me.Controls.Add(Me.LblProcess)
        Me.Name = "TempJobWorkerRateMaster"
        Me.Text = "Term Conditions"
        Me.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.Controls.SetChildIndex(Me.LblProcessReq, 0)
        Me.Controls.SetChildIndex(Me.LblPaymentDetail, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.LblStructure, 0)
        Me.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.Controls.SetChildIndex(Me.LblItemCategory, 0)
        Me.Controls.SetChildIndex(Me.TxtItemCategory, 0)
        Me.Controls.SetChildIndex(Me.LblItemGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtItemGroup, 0)
        Me.Controls.SetChildIndex(Me.LblItem, 0)
        Me.Controls.SetChildIndex(Me.TxtItem, 0)
        Me.Controls.SetChildIndex(Me.LblGeneralRate, 0)
        Me.Controls.SetChildIndex(Me.TxtGeneralRate, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.TxtGeneralRateWithoutMaterial, 0)
        Me.Controls.SetChildIndex(Me.LblRateGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtJobWorkerRateGroup, 0)
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents LblProcess As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblPaymentDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents LblProcessReq As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents LblStructure As System.Windows.Forms.Label
    Protected WithEvents TxtItemCategory As AgControls.AgTextBox
    Protected WithEvents LblItemCategory As System.Windows.Forms.Label
    Protected WithEvents TxtItemGroup As AgControls.AgTextBox
    Protected WithEvents LblItemGroup As System.Windows.Forms.Label
    Protected WithEvents TxtItem As AgControls.AgTextBox
    Protected WithEvents LblItem As System.Windows.Forms.Label
    Protected WithEvents TxtGeneralRate As AgControls.AgTextBox
    Protected WithEvents LblGeneralRate As System.Windows.Forms.Label
    Protected WithEvents TxtGeneralRateWithoutMaterial As AgControls.AgTextBox
    Protected WithEvents TxtJobWorkerRateGroup As AgControls.AgTextBox
    Protected WithEvents LblRateGroup As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub FrmJobWorkerRateMaster_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtProcess, LblProcess.Text) Then passed = False : Exit Sub

        If AgCL.AgIsDuplicate(Dgl1, "" & Dgl1.Columns(Col1WEF).Index & "," & Dgl1.Columns(Col1JobWorker).Index & "") Then
            passed = False : Exit Sub
        End If

        If TxtItem.AgSelectedValue = "" And TxtItemGroup.AgSelectedValue = "" And TxtItemCategory.AgSelectedValue = "" And TxtJobWorkerRateGroup.Text = "" Then
            mQry = "Select count(*) From JobWorkerRate " &
                    " Where Process ='" & TxtProcess.AgSelectedValue & "' " &
                    " And JobWorkerRateGroup Is Null " &
                    " And Item Is Null " &
                    " And ItemGroup Is Null " &
                    " And ItemCategory Is Null " &
                    " And Code <> '" & mInternalCode & "' " &
                    " And Div_Code = '" & AgL.PubDivCode & "'"
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                    MsgBox("Rate For " & TxtProcess.Text & " Already Exist!", MsgBoxStyle.Information)
                    passed = False : Exit Sub
                End If
            End If
        End If


        If TxtItem.AgSelectedValue = "" And TxtItemGroup.AgSelectedValue = "" And TxtItemCategory.AgSelectedValue = "" And TxtJobWorkerRateGroup.Text <> "" Then
            mQry = "Select count(*) From JobWorkerRate " &
                    " Where Process ='" & TxtProcess.AgSelectedValue & "' " &
                    " And JobWorkerRateGroup ='" & TxtJobWorkerRateGroup.AgSelectedValue & "'  " &
                    " And Item Is Null " &
                    " And ItemGroup Is Null " &
                    " And ItemCategory Is Null " &
                    " And Code <> '" & mInternalCode & "'  " &
                    " And Division = '" & TxtDivision.AgSelectedValue & "'"
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                    MsgBox("Rate For " & TxtProcess.Text & " Already Exist!", MsgBoxStyle.Information)
                    passed = False : Exit Sub
                End If
            End If
        End If


        If TxtItem.AgSelectedValue <> "" And TxtJobWorkerRateGroup.Text = "" Then
            mQry = "Select count(*) From JobWorkerRate " &
                    " Where Process ='" & TxtProcess.AgSelectedValue & "' " &
                    " And Item = '" & TxtItem.AgSelectedValue & "' " &
                    " And JobWorkerRateGroup Is Null " &
                    " And Code <> '" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                    MsgBox("Rate For " & TxtProcess.Text & " And " & TxtItem.Text & " Already Exist!", MsgBoxStyle.Information)
                    passed = False : Exit Sub
                End If
            End If
        End If


        If TxtItem.AgSelectedValue <> "" And TxtJobWorkerRateGroup.Text <> "" Then
            mQry = "Select count(*) From JobWorkerRate " &
                    " Where Process ='" & TxtProcess.AgSelectedValue & "' " &
                    " And Item = '" & TxtItem.AgSelectedValue & "' " &
                    " And JobWorkerRateGroup = '" & TxtJobWorkerRateGroup.AgSelectedValue & "' " &
                    " And Code <> '" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                    MsgBox("Rate For " & TxtProcess.Text & " And " & TxtItem.Text & " Already Exist!", MsgBoxStyle.Information)
                    passed = False : Exit Sub
                End If
            End If
        End If




        If TxtItemGroup.AgSelectedValue <> "" And TxtItem.AgSelectedValue = "" And TxtJobWorkerRateGroup.Text = "" Then
            mQry = "Select count(*) From JobWorkerRate " &
                    " Where Process ='" & TxtProcess.AgSelectedValue & "' " &
                    " And ItemGroup = '" & TxtItemGroup.AgSelectedValue & "' " &
                    " And Item Is Null " &
                    " And JobWorkerRateGroup Is Null " &
                    " And Code <> '" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                MsgBox("Rate For " & TxtProcess.Text & " And " & TxtItemGroup.Text & "  Already Exist!", MsgBoxStyle.Information)
                passed = False : Exit Sub
            End If
        End If

        If TxtItemGroup.AgSelectedValue <> "" And TxtItem.AgSelectedValue = "" And TxtJobWorkerRateGroup.Text <> "" Then
            mQry = "Select count(*) From JobWorkerRate " &
                    " Where Process ='" & TxtProcess.AgSelectedValue & "' " &
                    " And ItemGroup = '" & TxtItemGroup.AgSelectedValue & "' " &
                    " And Item Is Null " &
                    " And JobWorkerRateGroup = '" & TxtJobWorkerRateGroup.AgSelectedValue & "' " &
                    " And Code <> '" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                MsgBox("Rate For " & TxtProcess.Text & " And " & TxtItemGroup.Text & "  Already Exist!", MsgBoxStyle.Information)
                passed = False : Exit Sub
            End If
        End If


        If TxtItemCategory.AgSelectedValue <> "" And TxtItemGroup.AgSelectedValue = "" And TxtItem.AgSelectedValue = "" And TxtJobWorkerRateGroup.Text = "" Then
            mQry = "Select count(*) From JobWorkerRate " &
                    " Where Process ='" & TxtProcess.AgSelectedValue & "' " &
                    " And ItemCategory = '" & TxtItemCategory.AgSelectedValue & "' " &
                    " And ItemCategory Is Not Null " &
                    " And ItemGroup Is Null " &
                    " And Item Is  Null " &
                    " And JobWorkerRateGroup Is  Null " &
                    " And Code <> '" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                MsgBox("Rate For " & TxtProcess.Text & " And " & TxtItemCategory.Text & "  Already Exist!", MsgBoxStyle.Information)
                passed = False : Exit Sub
            End If
        End If


        If TxtItemCategory.AgSelectedValue <> "" And TxtItemGroup.AgSelectedValue = "" And TxtItem.AgSelectedValue = "" And TxtJobWorkerRateGroup.Text <> "" Then
            mQry = "Select count(*) From JobWorkerRate " &
                    " Where Process ='" & TxtProcess.AgSelectedValue & "' " &
                    " And ItemCategory = '" & TxtItemCategory.AgSelectedValue & "' " &
                    " And ItemCategory Is Not Null " &
                    " And ItemGroup Is Null " &
                    " And Item Is  Null " &
                    " And JobWorkerRateGroup = '" & TxtJobWorkerRateGroup.AgSelectedValue & "' " &
                    " And Code <> '" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                MsgBox("Rate For " & TxtProcess.Text & " And " & TxtItemCategory.Text & "  Already Exist!", MsgBoxStyle.Information)
                passed = False : Exit Sub
            End If
        End If

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1JobWorker, I).Value <> "" Then
                    If Val(.Item(Col1Rate, I).Value) = 0 Then
                        MsgBox("Rate Is 0 At Row No. " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                        Dgl1.CurrentCell = Dgl1.Item(Col1Rate, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "J.Div_Code") & " AND IfNull(J.IsDeleted,0) = 0 And J.MasterType = '" & ClsMain.RateMasterType.General & "'"
        AgL.PubFindQry = "SELECT H.UID, Vt.Description As Process, I.Description As Item, " &
                        " Ig.Description As ItemGroup, Ic.Description As ItemCategory, H.Rate, H.RateOutSide, G.Description as RateGroup " &
                        " FROM JobWorkerRateDetail_Log H " &
                        " LEFT JOIN JobWorkerRate_Log J ON H.UID = J.UID " &
                        " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.Process " &
                        " LEFT JOIN Item I On H.Item  = I.Code " &
                        " LEFT JOIN ItemGroup Ig On H.ItemGroup  = Ig.Code " &
                        " LEFT JOIN JobWorkerRateGroup G On G.Code  = H.JobWorkerRateGroup " &
                        " LEFT JOIN ItemCategory Ic On H.ItemCategory = I.Code " & mConStr &
                        " And J.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "' "
        AgL.PubFindQryOrdBy = "[Process]"
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1  And L.MasterType = '" & ClsMain.RateMasterType.General & "'" '& AgL.RetDivisionCondition(AgL, "H.Div_Code") & " AND IfNull(H.IsDeleted,0) = 0  
        AgL.PubFindQry = "SELECT H.Code, Vt.Description As Process,  I.Description As Item, " &
                        " Ig.Description As ItemGroup, Ic.Description As ItemCategory, H.Rate, H.RateOutSide, G.Description as RateGroup " &
                        " FROM JobWorkerRateDetail H  " &
                        " LEFT JOIN JobWorkerRate L ON H.Code = L.Code " &
                        " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.Process " &
                        " LEFT JOIN Item I On H.Item  = I.Code " &
                        " LEFT JOIN ItemGroup Ig On H.ItemGroup  = Ig.Code " &
                        " LEFT JOIN ItemCategory Ic On H.ItemCategory = I.Code " &
                        " LEFT JOIN Subgroup J On H.JobWorker  = J.SubCode " &
                        " LEFT JOIN JobWorkerRateGroup G On G.Code  = H.JobWorkerRateGroup " &
                        "  " & mConStr
        AgL.PubFindQryOrdBy = "[Process]"
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobWorkerRate"
        LogTableName = "JobWorkerRate_Log"
        MainLineTableCsv = "JobWorkerRateDetail,Structure_TransFooter,Structure_TransLine"
        LogLineTableCsv = "JobWorkerRateDetail_Log,Structure_TransFooter_Log,Structure_TransLine_Log"
        LineTableSearchKeyCsv = "Code,DocId,DocId"

        AgL.GridDesign(Dgl1)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
    End Sub

    Public Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgDateColumn(Dgl1, Col1WEF, 90, Col1WEF, True, False)
            .AddAgTextColumn(Dgl1, Col1JobWorker, 250, 5, Col1JobWorker, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 5, 2, False, Col1Rate, True, False)
            .AddAgNumberColumn(Dgl1, Col1RateOutSide, 90, 5, 2, False, Col1RateOutSide, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 25
        Dgl1.AgSkipReadOnlyColumns = True

        AgCalcGrid1.Ini_Grid("", AgL.PubLoginDate)
        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1JobWorker).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1JobWorker).Index
        AgCalcGrid1.AgIsMaster = True

        FrmJobWorkerRateMaster_BaseFunction_FIniList()
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer = 0, mSr As Integer = 0
        mQry = " Update JobWorkerRate_LOG " &
                " SET  " &
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                " MasterType = " & AgL.Chk_Text(ClsMain.RateMasterType.General) & ", " &
                " JobWorkerRateGroup = " & AgL.Chk_Text(TxtJobWorkerRateGroup.AgSelectedValue) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " &
                " Item = " & AgL.Chk_Text(TxtItem.AgSelectedValue) & ", " &
                " ItemGroup = " & AgL.Chk_Text(TxtItemGroup.AgSelectedValue) & ", " &
                " ItemCategory = " & AgL.Chk_Text(TxtItemCategory.AgSelectedValue) & ", " &
                " GeneralRate = " & Val(TxtGeneralRate.Text) & ", " &
                " GeneralRateOutside = " & Val(TxtGeneralRateWithoutMaterial.Text) & " " &
                " Where UID = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        AgCalcGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)

        mQry = "Delete From JobWorkerRateDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mSr += 1
        mQry = " INSERT INTO JobWorkerRateDetail_Log (Code,Sr, " &
                " Process, " &
                " JobWorkerRateGroup, " &
                " Item, " &
                " ItemGroup, " &
                " ItemCategory, " &
                " Rate, " &
                " RateOutside, " &
                " UID) " &
                " VALUES (" & AgL.Chk_Text(mInternalCode) & ", " & Val(mSr) & ", " &
                " " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(TxtJobWorkerRateGroup.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(TxtItem.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(TxtItemGroup.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(TxtItemCategory.AgSelectedValue) & ", " &
                " " & Val(TxtGeneralRate.Text) & ", " &
                " " & Val(TxtGeneralRateWithoutMaterial.Text) & ", " &
                " '" & mSearchCode & "')"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1JobWorker, I).Value <> "" Then
                    mSr += 1
                    mQry = " INSERT INTO JobWorkerRateDetail_Log (Code,Sr, " &
                            " WEF, " &
                            " JobWorker, " &
                            " Process, " &
                            " JobWorkerRateGroup, " &
                            " Item, " &
                            " ItemGroup, " &
                            " ItemCategory, " &
                            " Rate, " &
                            " RateOutside, " &
                            " UID) " &
                            " VALUES (" & AgL.Chk_Text(mInternalCode) & ", " & Val(mSr) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1WEF, I).Value) & ", " &
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1JobWorker, I)) & ", " &
                            " " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                            " " & AgL.Chk_Text(TxtJobWorkerRateGroup.AgSelectedValue) & ", " &
                            " " & AgL.Chk_Text(TxtItem.AgSelectedValue) & ", " &
                            " " & AgL.Chk_Text(TxtItemGroup.AgSelectedValue) & ", " &
                            " " & AgL.Chk_Text(TxtItemCategory.AgSelectedValue) & ", " &
                            " " & Val(.Item(Col1Rate, I).Value) & ", " &
                            " " & Val(.Item(Col1RateOutSide, I).Value) & ", " &
                            " '" & mSearchCode & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        TxtProcess.AgHelpDataSet = HelpDataSet.Process
        Dgl1.AgHelpDataSet(Col1JobWorker, 3) = HelpDataSet.JobWorker
        TxtItemCategory.AgHelpDataSet = HelpDataSet.ItemCategory
        TxtItemGroup.AgHelpDataSet = HelpDataSet.ItemGroup
        TxtItem.AgHelpDataSet(3) = HelpDataSet.Item
        TxtJobWorkerRateGroup.AgHelpDataSet(2) = HelpDataSet.JobWorkerRateGroup
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " AND IfNull(IsDeleted,0) = 0   And MasterType = '" & ClsMain.RateMasterType.General & "'"
        mQry = "Select Code As SearchCode " &
            " From JobWorkerRate " & mConStr &
            " And IfNull(IsDeleted,0)=0 "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & "  And MasterType = '" & ClsMain.RateMasterType.General & "'"
        mQry = "Select UID As SearchCode " &
               " From JobWorkerRate_log " & mConStr &
               " And EntryStatus='" & LogStatus.LogOpen & "' "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer = 0
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = " Select H.*  " &
                    " From JobWorkerRate H " &
                    " Where H.Code='" & SearchCode & "'"
        Else
            mQry = " Select H.*  " &
                " From JobWorkerRate_Log H " &
                " Where H.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtProcess.AgSelectedValue = AgL.XNull(.Rows(0)("Process"))
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(TxtProcess.AgSelectedValue, AgL.GcnRead)
                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.AgSelectedValue = AgL.XNull(.Rows(0)("Structure"))
                End If
                TxtJobWorkerRateGroup.AgSelectedValue = AgL.XNull(.Rows(0)("JobWorkerRateGroup"))
                TxtItem.AgSelectedValue = AgL.XNull(.Rows(0)("Item"))
                TxtItemCategory.AgSelectedValue = AgL.XNull(.Rows(0)("ItemCategory"))
                TxtItemGroup.AgSelectedValue = AgL.XNull(.Rows(0)("ItemGroup"))
                TxtGeneralRate.Text = AgL.VNull(.Rows(0)("GeneralRate"))
                TxtGeneralRateWithoutMaterial.Text = AgL.VNull(.Rows(0)("GeneralRateOutside"))

                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                IniGrid()

                AgCalcGrid1.MoveRec_TransFooter(SearchCode)

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from JobWorkerRateDetail where Code = '" & SearchCode & "'  And JobWorker Is Not Null Order By Sr"
                Else
                    mQry = "Select * from JobWorkerRateDetail_Log where UID = '" & SearchCode & "'  And JobWorker Is Not Null Order By Sr"
                End If
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1WEF, I).Value = AgL.XNull(.Rows(I)("WEF"))
                            Dgl1.AgSelectedValue(Col1JobWorker, I) = AgL.XNull(.Rows(I)("JobWorker"))
                            Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                            Dgl1.Item(Col1RateOutSide, I).Value = Format(AgL.VNull(.Rows(I)("RateOutside")), "0.00")
                        Next I
                    End If
                End With
            End If
        End With

        Topctrl1.tPrn = False
        AgCalcGrid1.Visible = False
    End Sub

    Private Sub FrmJobWorkerRateMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtProcess.Focus()
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = " SELECT H.SubCode, Sg.DispName AS JobWorker, IfNull(Sg.IsDeleted,0) AS IsDeleted, " &
                " IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, Sg.Div_Code " &
                " FROM JobWorker H " &
                " LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
        HelpDataSet.JobWorker = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.NCat, L.NCatDescription AS Process " &
                " FROM Process H " &
                " LEFT JOIN VoucherCat L ON H.NCat = L.NCat "
        HelpDataSet.Process = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code, H.Description AS Item, H.ItemGroup, IfNull(H.IsDeleted,0) AS IsDeleted, " &
                " IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, H.Div_Code " &
                " FROM Item H "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code AS Code, H.Description AS ItemCategory FROM ItemCategory H  "
        HelpDataSet.ItemCategory = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code AS Code, H.Description AS ItemGroup, H.ItemCategory FROM ItemGroup H  "
        HelpDataSet.ItemGroup = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code AS Code, H.Description,  H.Process, IfNull(H.IsDeleted,0) as IsDeleted, IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status FROM JobWorkerRateGroup H  "
        HelpDataSet.JobWorkerRateGroup = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1JobWorker
                Dgl1.AgRowFilter(Dgl1.Columns(Col1JobWorker).Index) = " IsDeleted = 0 " &
                    " And Status <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                    " And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        'TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(TxtProcess.AgSelectedValue, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.Visible = False
        AgCalcGrid1.Visible = False
        IniGrid()
    End Sub

    Private Sub FrmJobWorkerRateMaster_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        AgCalcGrid1.Calculation()
    End Sub

    Private Sub TxtProcess_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtProcess.Validating
        Select Case sender.NAME
            Case TxtProcess.Name
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(TxtProcess.AgSelectedValue, AgL.GcnRead)
                AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                AgCalcGrid1.Visible = False
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Try
            If Val(Dgl1.Item(Col1Rate, Dgl1.CurrentCell.RowIndex).Value) = 0 And Val(TxtGeneralRate.Text) <> 0 Then
                Dgl1.Item(Col1Rate, Dgl1.CurrentCell.RowIndex).Value = Val(TxtGeneralRate.Text)
                Dgl1.Item(Col1RateOutSide, Dgl1.CurrentCell.RowIndex).Value = Val(TxtGeneralRateWithoutMaterial.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtItemGroup_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtItemGroup.Enter, TxtItem.Enter, TxtJobWorkerRateGroup.Enter
        Select Case sender.name
            Case TxtItemGroup.Name
                If TxtItemCategory.AgSelectedValue <> "" Then
                    TxtItemGroup.AgRowFilter = " IfNull(ItemCategory,'') = '" & TxtItemCategory.AgSelectedValue & "' "
                End If

            Case TxtItem.Name
                If TxtItemGroup.AgSelectedValue <> "" Then
                    TxtItem.AgRowFilter = " IfNull(ItemGroup,'') = '" & TxtItemGroup.AgSelectedValue & "' "
                End If

            Case TxtJobWorkerRateGroup.Name
                If TxtProcess.AgSelectedValue <> "" Then
                    TxtJobWorkerRateGroup.AgRowFilter = " Process = '" & TxtProcess.AgSelectedValue & "' "
                End If
        End Select
    End Sub
End Class
