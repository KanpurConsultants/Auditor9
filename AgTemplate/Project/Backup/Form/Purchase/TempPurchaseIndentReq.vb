Public Class TempPurchIndentReq
    Inherits AgTemplate.TempTransaction
    Public mQry$
    Dim mTransFlag As Boolean = False

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1Item As String = "Item"
    Protected Const Col1CurrentStock As String = "Current Stock"
    Protected Const Col1ReqQty As String = "Requisition Qty"
    Protected Const Col1IndentQty As String = "Indent Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1TotalReqMeasure As String = "Total Requisition Measure"
    Protected Const Col1TotalIndentMeasure As String = "Total Indent Measure"
    Protected Const Col1OrdQty As String = "Order Qty"
    Protected Const Col1OrdMeasure As String = "Order Measure"
    Protected Const Col1PurchQty As String = "Purch Qty"
    Protected Const Col1PurchMeasure As String = "Purch Measure"

    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2RequisionNo As String = "Requision No."
    Protected Const Col2Item As String = "Item"
    Protected Const Col2ReqQty As String = "Req. Qty"
    Protected Const Col2Unit As String = "Unit"
    Protected Const Col2MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col2MeasureUnit As String = "Measure Unit"
    Protected Const Col2TotalReqMeasure As String = "Total Req Measure"
    Protected Const Col2ReqDate As String = "Req. Date"
    Protected Const Col2TempRequisionNo As String = "Temp Requision No."
    Protected Const Col2TempItem As String = "Temp Item"

    Protected WithEvents PnlReq As System.Windows.Forms.Panel
    Protected WithEvents BtnFillIndentDetail As System.Windows.Forms.Button
    Protected WithEvents BtnFillRequisition As System.Windows.Forms.Button
    Protected WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Protected WithEvents Panel2 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalReqMeasureQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureTextReq As System.Windows.Forms.Label
    Protected WithEvents LblTotalReqQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalReqText As System.Windows.Forms.Label
    Protected Const Col1RequireDate As String = "Require Date"

    Dim mRequistionNCat$ = ""

    Protected ItemHelpDataSet As DataSet = Nothing
    Protected DepartmentHelpDataSet As DataSet = Nothing
    Protected IndentorHelpDataSet As DataSet = Nothing

    Public Property RequistionNCat() As String
        Get
            RequistionNCat = mRequistionNCat
        End Get
        Set(ByVal value As String)
            mRequistionNCat = value
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
        Me.TxtDepartment = New AgControls.AgTextBox
        Me.LblDepartment = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LblIndentorReq = New System.Windows.Forms.Label
        Me.TxtIndentor = New AgControls.AgTextBox
        Me.LblIndentor = New System.Windows.Forms.Label
        Me.LblDepartmentReq = New System.Windows.Forms.Label
        Me.PnlReq = New System.Windows.Forms.Panel
        Me.BtnFillIndentDetail = New System.Windows.Forms.Button
        Me.BtnFillRequisition = New System.Windows.Forms.Button
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.LblTotalReqMeasureQty = New System.Windows.Forms.Label
        Me.LblTotalMeasureTextReq = New System.Windows.Forms.Label
        Me.LblTotalReqQty = New System.Windows.Forms.Label
        Me.LblTotalReqText = New System.Windows.Forms.Label
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
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(756, 525)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(596, 525)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(421, 525)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(145, 525)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(11, 525)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 521)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(287, 525)
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
        Me.LblV_No.Location = New System.Drawing.Point(413, 37)
        Me.LblV_No.Size = New System.Drawing.Size(67, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Indent No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(509, 36)
        Me.TxtV_No.Size = New System.Drawing.Size(208, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(291, 42)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(180, 37)
        Me.LblV_Date.Size = New System.Drawing.Size(74, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Indent Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(493, 22)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(307, 36)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(413, 18)
        Me.LblV_Type.Size = New System.Drawing.Size(75, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Indent Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(509, 16)
        Me.TxtV_Type.Size = New System.Drawing.Size(208, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(291, 22)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(180, 18)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(307, 16)
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
        Me.LblPrefix.Location = New System.Drawing.Point(23, 7)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(4, 20)
        Me.TabControl1.Size = New System.Drawing.Size(971, 139)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LblDepartmentReq)
        Me.TP1.Controls.Add(Me.LblIndentorReq)
        Me.TP1.Controls.Add(Me.TxtIndentor)
        Me.TP1.Controls.Add(Me.LblIndentor)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtDepartment)
        Me.TP1.Controls.Add(Me.LblDepartment)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(963, 113)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDepartment, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDepartment, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIndentor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtIndentor, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIndentorReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDepartmentReq, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
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
        'TxtDepartment
        '
        Me.TxtDepartment.AgMandatory = True
        Me.TxtDepartment.AgMasterHelp = False
        Me.TxtDepartment.AgNumberLeftPlaces = 8
        Me.TxtDepartment.AgNumberNegetiveAllow = False
        Me.TxtDepartment.AgNumberRightPlaces = 2
        Me.TxtDepartment.AgPickFromLastValue = False
        Me.TxtDepartment.AgRowFilter = ""
        Me.TxtDepartment.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDepartment.AgSelectedValue = Nothing
        Me.TxtDepartment.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDepartment.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDepartment.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDepartment.Location = New System.Drawing.Point(33, 45)
        Me.TxtDepartment.MaxLength = 50
        Me.TxtDepartment.Name = "TxtDepartment"
        Me.TxtDepartment.Size = New System.Drawing.Size(35, 18)
        Me.TxtDepartment.TabIndex = 4
        Me.TxtDepartment.Visible = False
        '
        'LblDepartment
        '
        Me.LblDepartment.AutoSize = True
        Me.LblDepartment.BackColor = System.Drawing.Color.Transparent
        Me.LblDepartment.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDepartment.Location = New System.Drawing.Point(9, 28)
        Me.LblDepartment.Name = "LblDepartment"
        Me.LblDepartment.Size = New System.Drawing.Size(75, 16)
        Me.LblDepartment.TabIndex = 706
        Me.LblDepartment.Text = "Department"
        Me.LblDepartment.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(7, 499)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(986, 21)
        Me.Panel1.TabIndex = 694
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
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(7, 360)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(986, 138)
        Me.Pnl1.TabIndex = 200
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(180, 78)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(307, 76)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(410, 18)
        Me.TxtRemarks.TabIndex = 5
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(7, 339)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(248, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Indent For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblIndentorReq
        '
        Me.LblIndentorReq.AutoSize = True
        Me.LblIndentorReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblIndentorReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblIndentorReq.Location = New System.Drawing.Point(291, 61)
        Me.LblIndentorReq.Name = "LblIndentorReq"
        Me.LblIndentorReq.Size = New System.Drawing.Size(10, 7)
        Me.LblIndentorReq.TabIndex = 732
        Me.LblIndentorReq.Text = "Ä"
        '
        'TxtIndentor
        '
        Me.TxtIndentor.AgMandatory = True
        Me.TxtIndentor.AgMasterHelp = False
        Me.TxtIndentor.AgNumberLeftPlaces = 8
        Me.TxtIndentor.AgNumberNegetiveAllow = False
        Me.TxtIndentor.AgNumberRightPlaces = 2
        Me.TxtIndentor.AgPickFromLastValue = False
        Me.TxtIndentor.AgRowFilter = ""
        Me.TxtIndentor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIndentor.AgSelectedValue = Nothing
        Me.TxtIndentor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIndentor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtIndentor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIndentor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIndentor.Location = New System.Drawing.Point(307, 56)
        Me.TxtIndentor.MaxLength = 20
        Me.TxtIndentor.Name = "TxtIndentor"
        Me.TxtIndentor.Size = New System.Drawing.Size(410, 18)
        Me.TxtIndentor.TabIndex = 4
        '
        'LblIndentor
        '
        Me.LblIndentor.AutoSize = True
        Me.LblIndentor.BackColor = System.Drawing.Color.Transparent
        Me.LblIndentor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIndentor.Location = New System.Drawing.Point(180, 56)
        Me.LblIndentor.Name = "LblIndentor"
        Me.LblIndentor.Size = New System.Drawing.Size(54, 16)
        Me.LblIndentor.TabIndex = 731
        Me.LblIndentor.Text = "Indentor"
        '
        'LblDepartmentReq
        '
        Me.LblDepartmentReq.AutoSize = True
        Me.LblDepartmentReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDepartmentReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDepartmentReq.Location = New System.Drawing.Point(10, 46)
        Me.LblDepartmentReq.Name = "LblDepartmentReq"
        Me.LblDepartmentReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDepartmentReq.TabIndex = 733
        Me.LblDepartmentReq.Text = "Ä"
        Me.LblDepartmentReq.Visible = False
        '
        'PnlReq
        '
        Me.PnlReq.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PnlReq.Location = New System.Drawing.Point(5, 188)
        Me.PnlReq.Name = "PnlReq"
        Me.PnlReq.Size = New System.Drawing.Size(986, 122)
        Me.PnlReq.TabIndex = 1
        '
        'BtnFillIndentDetail
        '
        Me.BtnFillIndentDetail.BackColor = System.Drawing.Color.Silver
        Me.BtnFillIndentDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillIndentDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillIndentDetail.ForeColor = System.Drawing.Color.Black
        Me.BtnFillIndentDetail.Location = New System.Drawing.Point(860, 337)
        Me.BtnFillIndentDetail.Name = "BtnFillIndentDetail"
        Me.BtnFillIndentDetail.Size = New System.Drawing.Size(140, 21)
        Me.BtnFillIndentDetail.TabIndex = 2
        Me.BtnFillIndentDetail.Text = "Fill Indent Detail"
        Me.BtnFillIndentDetail.UseVisualStyleBackColor = False
        '
        'BtnFillRequisition
        '
        Me.BtnFillRequisition.BackColor = System.Drawing.Color.Silver
        Me.BtnFillRequisition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillRequisition.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillRequisition.ForeColor = System.Drawing.Color.Black
        Me.BtnFillRequisition.Location = New System.Drawing.Point(857, 162)
        Me.BtnFillRequisition.Name = "BtnFillRequisition"
        Me.BtnFillRequisition.Size = New System.Drawing.Size(140, 23)
        Me.BtnFillRequisition.TabIndex = 1
        Me.BtnFillRequisition.Text = "Fill Requisition Detail"
        Me.BtnFillRequisition.UseVisualStyleBackColor = False
        '
        'LinkLabel2
        '
        Me.LinkLabel2.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Location = New System.Drawing.Point(5, 167)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(126, 20)
        Me.LinkLabel2.TabIndex = 789
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Requisition Details"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel2.Controls.Add(Me.LblTotalReqMeasureQty)
        Me.Panel2.Controls.Add(Me.LblTotalMeasureTextReq)
        Me.Panel2.Controls.Add(Me.LblTotalReqQty)
        Me.Panel2.Controls.Add(Me.LblTotalReqText)
        Me.Panel2.Location = New System.Drawing.Point(7, 313)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(986, 21)
        Me.Panel2.TabIndex = 793
        Me.Panel2.Visible = False
        '
        'LblTotalReqMeasureQty
        '
        Me.LblTotalReqMeasureQty.AutoSize = True
        Me.LblTotalReqMeasureQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalReqMeasureQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalReqMeasureQty.Location = New System.Drawing.Point(432, 3)
        Me.LblTotalReqMeasureQty.Name = "LblTotalReqMeasureQty"
        Me.LblTotalReqMeasureQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalReqMeasureQty.TabIndex = 670
        Me.LblTotalReqMeasureQty.Text = "."
        Me.LblTotalReqMeasureQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalMeasureTextReq
        '
        Me.LblTotalMeasureTextReq.AutoSize = True
        Me.LblTotalMeasureTextReq.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureTextReq.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureTextReq.Location = New System.Drawing.Point(321, 3)
        Me.LblTotalMeasureTextReq.Name = "LblTotalMeasureTextReq"
        Me.LblTotalMeasureTextReq.Size = New System.Drawing.Size(106, 16)
        Me.LblTotalMeasureTextReq.TabIndex = 669
        Me.LblTotalMeasureTextReq.Text = "Total Measure :"
        '
        'LblTotalReqQty
        '
        Me.LblTotalReqQty.AutoSize = True
        Me.LblTotalReqQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalReqQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalReqQty.Location = New System.Drawing.Point(94, 3)
        Me.LblTotalReqQty.Name = "LblTotalReqQty"
        Me.LblTotalReqQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalReqQty.TabIndex = 668
        Me.LblTotalReqQty.Text = "."
        Me.LblTotalReqQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalReqText
        '
        Me.LblTotalReqText.AutoSize = True
        Me.LblTotalReqText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalReqText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalReqText.Location = New System.Drawing.Point(9, 3)
        Me.LblTotalReqText.Name = "LblTotalReqText"
        Me.LblTotalReqText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalReqText.TabIndex = 667
        Me.LblTotalReqText.Text = "Total Qty :"
        '
        'TempPurchIndentReq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 566)
        Me.Controls.Add(Me.PnlReq)
        Me.Controls.Add(Me.BtnFillIndentDetail)
        Me.Controls.Add(Me.BtnFillRequisition)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "TempPurchIndentReq"
        Me.Text = "Template Sale Order"
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
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel2, 0)
        Me.Controls.SetChildIndex(Me.BtnFillRequisition, 0)
        Me.Controls.SetChildIndex(Me.BtnFillIndentDetail, 0)
        Me.Controls.SetChildIndex(Me.PnlReq, 0)
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
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtDepartment As AgControls.AgTextBox
    Protected WithEvents LblDepartment As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblIndentorReq As System.Windows.Forms.Label
    Protected WithEvents TxtIndentor As AgControls.AgTextBox
    Protected WithEvents LblIndentor As System.Windows.Forms.Label
    Protected WithEvents LblDepartmentReq As System.Windows.Forms.Label
#End Region

    Private Sub TempPurchIndentReq_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim I As Integer, mSr As Integer
        If TxtEntryType.Text = "Delete" Then
            With Dgl2
                For I = 0 To .RowCount - 1
                    If .Item(Col2Item, I).Value <> "" Then
                        mSr += 1
                        If .AgSelectedValue(Col2TempRequisionNo, I) <> "" Then
                            mQry = " UPDATE RequisitionDetail SET PurchaseIndent = NULL " & _
                                    " WHERE DocId = " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2TempRequisionNo, I)) & "  " & _
                                    " AND Item = " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2TempItem, I)) & " "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If
                    End If
                Next
            End With
        Else
            With Dgl2
                For I = 0 To .RowCount - 1
                    If .Item(Col2Item, I).Value <> "" Then
                        mSr += 1
                        If .AgSelectedValue(Col2TempRequisionNo, I) <> "" Then
                            mQry = " UPDATE RequisitionDetail SET PurchaseIndent = NULL " & _
                                    " WHERE DocId = " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2TempRequisionNo, I)) & "  " & _
                                    " AND Item = " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2TempItem, I)) & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If

                        If .AgSelectedValue(Col2RequisionNo, I) <> "" Then
                            mQry = " UPDATE RequisitionDetail SET PurchaseIndent = " & AgL.Chk_Text(mInternalCode) & " " & _
                                    " WHERE DocId = " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2RequisionNo, I)) & "  " & _
                                    " AND Item = " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2Item, I)) & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchIndent"
        LogTableName = "PurchIndent_Log"
        MainLineTableCsv = "PurchIndentDetail,PurchIndentReq"
        LogLineTableCsv = "PurchIndentDetail_Log,PurchIndentReq_Log"
        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl2)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select P.DocID As SearchCode " & _
            " From PurchIndent P " & _
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
               " From PurchIndent_Log P " & _
               " Left Join Voucher_Type Vt On P.V_Type = Vt.V_Type  " & _
               " Where P.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By P.EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = "SELECT P.DocID, Vt.Description AS [Entry Type], P.V_Date AS [Entry Date], " & _
                            " P.V_No AS [Entry No], Sg.DispName As Indentor, D.Description As Department " & _
                            " FROM PurchIndent P " & _
                            " LEFT JOIN Voucher_type Vt ON P.V_Type = Vt.V_Type " & _
                            " LEFT JOIN Department D On P.Department = D.Code " & _
                            " Left Join SubGroup Sg On P.Indentor = Sg.SubCode " & _
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT P.UID as SearchCode, P.DocId, Vt.Description AS [Entry Type], " & _
                            " P.V_Date AS [Entry Date], P.V_No AS [Entry No], Sg.DispName As Indentor, D.Description As Department " & _
                            " FROM PurchIndent_Log P " & _
                            " LEFT JOIN Voucher_Type Vt ON P.V_Type = Vt.V_Type " & _
                            " LEFT JOIN Department D On P.Department = D.Code " & _
                            " Left Join SubGroup Sg On P.Indentor = Sg.SubCode " & _
                            " Where P.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub


    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, True)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 100, 8, 4, False, Col1CurrentStock, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReqQty, 80, 8, 4, False, Col1ReqQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1IndentQty, 80, 8, 4, False, Col1IndentQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 4, False, Col1MeasurePerPcs, True, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalReqMeasure, 90, 8, 4, False, Col1TotalReqMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalIndentMeasure, 120, 8, 4, False, Col1TotalIndentMeasure, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1OrdQty, 70, 8, 4, False, Col1OrdQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1OrdMeasure, 70, 8, 4, False, Col1OrdMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1PurchQty, 70, 8, 4, False, Col1PurchQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1PurchMeasure, 70, 8, 4, False, Col1PurchMeasure, False, True, True)
            .AddAgDateColumn(Dgl1, Col1RequireDate, 80, Col1RequireDate, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        'Dgl1.AllowUserToAddRows = False
        'Dgl1.ReadOnly = True
        'Dgl1.Anchor = AnchorStyles.None
        'Panel1.Anchor = Dgl1.Anchor


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2RequisionNo, 80, 0, Col2RequisionNo, True, False)
            .AddAgTextColumn(Dgl2, Col2Item, 200, 0, Col2Item, True, False)
            .AddAgNumberColumn(Dgl2, Col2ReqQty, 80, 8, 3, False, Col2ReqQty, True, False, True)
            .AddAgTextColumn(Dgl2, Col2Unit, 70, 0, Col2Unit, True, True)
            .AddAgNumberColumn(Dgl2, Col2MeasurePerPcs, 80, 8, 3, False, Col2MeasurePerPcs, True, True, True)
            .AddAgTextColumn(Dgl2, Col2MeasureUnit, 100, 0, Col2MeasureUnit, True, True)
            .AddAgNumberColumn(Dgl2, Col2TotalReqMeasure, 80, 8, 3, False, Col2TotalReqMeasure, True, True, True)
            .AddAgDateColumn(Dgl2, Col2ReqDate, 80, Col2ReqDate, True, False)
            .AddAgTextColumn(Dgl2, Col2TempRequisionNo, 120, 0, Col2TempRequisionNo, False, False)
            .AddAgTextColumn(Dgl2, Col2TempItem, 250, 0, Col2TempItem, False, False)
        End With
        AgL.AddAgDataGrid(Dgl2, PnlReq)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 35
        'Dgl2.Anchor = AnchorStyles.None
        'PnlReq.Anchor = Dgl2.Anchor
        'Ini_List()

        Dgl1.Columns(Col1CurrentStock).Visible = False
        Dgl1.Columns(Col1ReqQty).Visible = True
        Dgl1.Columns(Col1ReqQty).ReadOnly = True
        Dgl1.Columns(Col1ReqQty).Visible = True
        Dgl1.Columns(Col1Item).ReadOnly = True
        Dgl1.Columns(Col1RequireDate).ReadOnly = True
        Dgl1.Columns(Col1ReqQty).ReadOnly = True
        Dgl1.Columns(Col1IndentQty).HeaderText = "Approved Qty."
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        mQry = "UPDATE PurchIndent_Log " & _
                " SET Department = " & AgL.Chk_Text(TxtDepartment.AgSelectedValue) & ", " & _
                " Indentor = " & AgL.Chk_Text(TxtIndentor.AgSelectedValue) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From PurchIndentDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mSr += 1
                    mQry = "Insert Into PurchIndentDetail_Log(UID, DocId, Sr, Item, CurrentStock, ReqQty, IndentQty, " & _
                            " Unit, MeasurePerPcs, MeasureUnit, TotalReqMeasure, TotalIndentMeasure, OrdQty,  " & _
                            " OrdMeasure, PurchQty, PurchMeasure,RequireDate) " & _
                            " Values(" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " & _
                            " " & mSr & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                            " " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1ReqQty, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1IndentQty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ",  " & _
                            " " & Val(Dgl1.Item(Col1TotalReqMeasure, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1TotalIndentMeasure, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1OrdQty, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1OrdMeasure, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1PurchQty, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1PurchMeasure, I).Value) & ", " & _
                            " " & AgL.ConvertDate(Dgl1.Item(Col1RequireDate, I).Value.ToString) & " )"

                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
                End If
            Next
        End With


        mQry = "Delete From PurchIndentReq_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl2
            For I = 0 To .RowCount - 1
                If .Item(Col2Item, I).Value <> "" Then
                    mSr += 1
                    mQry = " INSERT INTO PurchIndentReq_Log	( DocId, Sr, Requisition, Item, Qty,	" & _
                            " Unit,	MeasurePerPcs,	MeasureUnit, TotalMeasure, RequireDate,	UID	)	" & _
                            " VALUES ( " & AgL.Chk_Text(mInternalCode) & ",	" & mSr & "," & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2RequisionNo, I)) & ", " & _
                            " " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2Item, I)) & "," & Val(Dgl2.Item(Col2ReqQty, I).Value) & ",	" & _
                            " " & AgL.Chk_Text(Dgl2.Item(Col2Unit, I).Value) & ", " & Val(Dgl2.Item(Col2ReqQty, I).Value) & "," & AgL.Chk_Text(Dgl2.Item(Col2MeasureUnit, I).Value) & ", " & _
                            " " & Val(Dgl2.Item(Col2TotalReqMeasure, I).Value) & "," & AgL.ConvertDate(Dgl2.Item(Col2ReqDate, I).Value.ToString) & "," & AgL.Chk_Text(mSearchCode) & "	)"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                End If
            Next
        End With


    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        IniRequisitionHelp(True)
        IniItemHelp(True)

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select P.* " & _
                " From PurchIndent P " & _
                " Where P.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select P.* " & _
                " From PurchIndent_Log P " & _
                " Where P.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtDepartment.AgSelectedValue = AgL.XNull(.Rows(0)("Department"))
                TxtIndentor.AgSelectedValue = AgL.XNull(.Rows(0)("Indentor"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from PurchIndentDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from PurchIndentDetail_Log where UID = '" & SearchCode & "' Order By Sr"
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
                            Dgl1.Item(Col1CurrentStock, I).Value = AgL.VNull(.Rows(I)("CurrentStock"))
                            Dgl1.Item(Col1ReqQty, I).Value = AgL.VNull(.Rows(I)("ReqQty"))
                            Dgl1.Item(Col1IndentQty, I).Value = AgL.VNull(.Rows(I)("IndentQty"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1TotalReqMeasure, I).Value = AgL.VNull(.Rows(I)("TotalReqMeasure"))
                            Dgl1.Item(Col1TotalIndentMeasure, I).Value = AgL.VNull(.Rows(I)("TotalIndentMeasure"))
                            Dgl1.Item(Col1OrdQty, I).Value = AgL.VNull(.Rows(I)("OrdQty"))
                            Dgl1.Item(Col1OrdMeasure, I).Value = AgL.VNull(.Rows(I)("OrdMeasure"))
                            Dgl1.Item(Col1PurchQty, I).Value = AgL.VNull(.Rows(I)("PurchQty"))
                            Dgl1.Item(Col1PurchMeasure, I).Value = AgL.VNull(.Rows(I)("PurchMeasure"))
                            Dgl1.Item(Col1RequireDate, I).Value = AgL.XNull(.Rows(I)("RequireDate"))

                            RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If
                End With

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from PurchIndentReq where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from PurchIndentReq_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl2.RowCount = 1
                    Dgl2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl2.Rows.Add()
                            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                            Dgl2.AgSelectedValue(Col2RequisionNo, I) = AgL.XNull(.Rows(I)("Requisition"))
                            Dgl2.AgSelectedValue(Col2Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl2.Item(Col2ReqQty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl2.Item(Col2Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl2.Item(Col2MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl2.Item(Col2MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl2.Item(Col2TotalReqMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                            Dgl2.Item(Col2ReqDate, I).Value = AgL.XNull(.Rows(I)("RequireDate"))
                            Dgl2.AgSelectedValue(Col2TempRequisionNo, I) = AgL.XNull(.Rows(I)("Requisition"))
                            Dgl2.AgSelectedValue(Col2TempItem, I) = AgL.XNull(.Rows(I)("Item"))
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
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        Dgl1.AgHelpDataSet(Col1Item, 8) = ItemHelpDataSet
        TxtDepartment.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = DepartmentHelpDataSet
        TxtIndentor.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = IndentorHelpDataSet

        IniRequisitionHelp(True)
        IniItemHelp(True)
    End Sub

    Public Sub IniItemHelp(Optional ByVal All_Records As Boolean = True, Optional ByVal bRequisitionDocId As String = "")
        If All_Records = True Then
            mQry = " SELECT I.Code, I.Description, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " & _
                    " IsNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code, ISNULL(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status, " & _
                    " I.Measure AS MeasurePerPcs, MeasureUnit, 0 AS Qty,0 AS TotalMeasure,'' AS RequireDate " & _
                    " FROM Item I"
            Dgl2.AgHelpDataSet(Col2Item, 11) = AgL.FillData(mQry, AgL.GCn)
        Else
            If bRequisitionDocId <> "" Then
                mQry = " SELECT RD.Item AS Code,I.Description AS [Item Name],I.ItemType,RD.Qty ,RD.Unit,RD.MeasurePerPcs , " & _
                        " RD.MeasureUnit, RD.TotalMeasure, RD.RequireDate ,0 As IsDeleted, '" & AgTemplate.ClsMain.EntryStatus.Active & "' AS  Status, " & _
                        " '" & TxtDivision.AgSelectedValue & "' AS Div_Code " & _
                        " FROM RequisitionDetail RD " & _
                        " LEFT JOIN Item I ON I.Code=RD.Item " & _
                        " WHERE RD.DocId = '" & bRequisitionDocId & "' " & _
                        " AND ( RD.PurchaseIndent IS NULL OR RD.PurchaseIndent= " & AgL.Chk_Text(mInternalCode) & ")"
                Dgl2.AgHelpDataSet(Col2Item, 10) = AgL.FillData(mQry, AgL.GCn)
            End If
        End If
    End Sub

    Public Sub IniRequisitionHelp(Optional ByVal All_Records As Boolean = True, Optional ByVal bIndentDocId As String = "")
        If All_Records = True Then
            mQry = " SELECT R.DocID AS Code,R.V_Type + '-' +convert(NVARCHAR(5),R.V_No) AS Requisition ," & _
                    " isnull(R.IsDeleted,0) AS IsDeleted, R.Div_Code ," & _
                    " isnull(R.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status, " & _
                    " 1 AS CountItem, Vt.NCat " & _
                    " FROM Requisition R " & _
                    " LEFT JOIN Voucher_Type Vt On R.V_Type = Vt.V_Type "
            Dgl2.AgHelpDataSet(Col2RequisionNo, 5) = AgL.FillData(mQry, AgL.GCn)
        Else
            If bIndentDocId <> "" Then
                mQry = " SELECT R.DocID AS Code,R.V_Type + '-' +convert(NVARCHAR(5),R.V_No) AS Requisition ," & _
                        " isnull(R.IsDeleted,0) AS IsDeleted, R.Div_Code ," & _
                        " isnull(R.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status, " & _
                        " V1.CountItem, Vt.NCat  " & _
                        " FROM Requisition R " & _
                        " LEFT JOIN  " & _
                        " ( " & _
                        " SELECT COUNT(RD.Item) AS CountItem ,RD.DocId " & _
                        " FROM RequisitionDetail RD " & _
                        " WHERE RD.PurchaseIndent Is NULL OR RD.PurchaseIndent = " & AgL.Chk_Text(bIndentDocId) & "" & _
                        " GROUP BY RD.DocId " & _
                        " ) V1 ON V1.DocId = R.DocID " & _
                        " LEFT JOIN Voucher_Type Vt On R.V_Type = Vt.V_Type "
                Dgl2.AgHelpDataSet(Col2RequisionNo, 5) = AgL.FillData(mQry, AgL.GCn)
            End If
        End If
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                'Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' "

        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0 : LblTotalMeasure.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalReqMeasure, I).Value = Format(Val(Dgl1.Item(Col1ReqQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.00")
                Dgl1.Item(Col1TotalIndentMeasure, I).Value = Format(Val(Dgl1.Item(Col1IndentQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.00")
                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1IndentQty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalIndentMeasure, I).Value)
            End If
        Next
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")


        LblTotalReqQty.Text = 0 : LblTotalReqMeasureQty.Text = 0
        For I = 0 To Dgl2.RowCount - 1
            If Dgl2.Item(Col2Item, I).Value <> "" Then
                Dgl2.Item(Col2TotalReqMeasure, I).Value = Format(Val(Dgl2.Item(Col2ReqQty, I).Value) * Val(Dgl2.Item(Col2MeasurePerPcs, I).Value), "0.00")
                'Footer Calculation
                LblTotalReqQty.Text = Val(LblTotalReqQty.Text) + Val(Dgl2.Item(Col2ReqQty, I).Value)
                LblTotalReqMeasureQty.Text = Val(LblTotalReqMeasureQty.Text) + Val(Dgl2.Item(Col2TotalReqMeasure, I).Value)
            End If
        Next
        LblTotalReqMeasureQty.Text = Format(Val(LblTotalReqMeasureQty.Text), "0.000")
        LblTotalReqQty.Text = Format(Val(LblTotalReqQty.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtIndentor, LblIndentor.Text) Then passed = False : Exit Sub
        ' If AgL.RequiredField(TxtDepartment, LblDepartment.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl2, Dgl2.Columns(Col2Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl2, " " & Dgl2.Columns(Col2Item).Index & " , " & Dgl2.Columns(Col2RequisionNo).Index & " ") Then passed = False : Exit Sub

        With Dgl2
            For I = 0 To .Rows.Count - 1
                If .Item(Col2Item, I).Value <> "" Then
                    If Val(.Item(Col2ReqQty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl2.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col2ReqQty, I) : Dgl2.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1IndentQty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1IndentQty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtDepartment.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.name
                Case TxtV_Type.Name

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDepartment.Enter
        Try
            Select Case sender.name
                Case TxtDepartment.Name

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
                Dgl1.Item(Col1CurrentStock, mRow).Value = ""
            Else
                If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                    DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("Measure"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl1.Item(Col1CurrentStock, mRow).Value = ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, mRow), mInternalCode, , , , , TxtV_Date.Text)
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
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillRequisitionDetail()
        IniItemHelp()
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim I As Integer = 0
        Dim DsTemp As DataSet
        Dim bConStr$ = ""

        If mRequistionNCat <> "" Then
            bConStr = " Vt.NCat In (" & mRequistionNCat & ") "
        Else
            bConStr = " 1 = 1 "
        End If

        mQry = " SELECT R.DocID, RD.Item ,RD.Unit,RD.Qty ,RD.MeasurePerPcs,RD.MeasureUnit, " & _
                " RD.TotalMeasure,RD.RequireDate  " & _
                " FROM Requisition R " & _
                " LEFT JOIN RequisitionDetail RD ON RD.DocId=R.DocID  " & _
                " LEFT JOIN Voucher_Type Vt On R.V_Type = VT.V_Type " & _
                " WHERE " & bConStr & " And isnull(R.IsDeleted, 0) = 0 And R.V_Date <='" & TxtV_Date.Text & "' " & _
                " And Isnull(R.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                " AND ( RD.PurchaseIndent IS NULL OR RD.PurchaseIndent = " & AgL.Chk_Text(mInternalCode) & " ) "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl2.RowCount = 1
            Dgl2.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl2.Rows.Add()
                    Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                    Dgl2.AgSelectedValue(Dgl2.Columns(Col2RequisionNo).Index, I) = AgL.XNull(.Rows(I)("DocID"))
                    Dgl2.AgSelectedValue(Dgl2.Columns(Col2Item).Index, I) = AgL.XNull(.Rows(I)("Item"))
                    Dgl2.Item(Dgl2.Columns(Col2ReqQty).Index, I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl2.Item(Dgl2.Columns(Col2Unit).Index, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl2.Item(Dgl2.Columns(Col2MeasurePerPcs).Index, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                    Dgl2.Item(Dgl2.Columns(Col2MeasureUnit).Index, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl2.Item(Dgl2.Columns(Col2TotalReqMeasure).Index, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                    Dgl2.Item(Dgl2.Columns(Col2ReqDate).Index, I).Value = AgL.XNull(.Rows(I)("RequireDate"))


                Next I
            End If
        End With
    End Sub

    Private Sub BtnFillRequisition_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillRequisition.Click
        If TxtV_Date.Text <> "" Then
            Call ProcFillRequisitionDetail()
        Else
            MsgBox("Please fill Indent date to proceed.")
        End If
    End Sub


    Private Sub Dgl2_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        mRowIndex = Dgl2.CurrentCell.RowIndex
        mColumnIndex = Dgl2.CurrentCell.ColumnIndex

        Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
            Case Col2RequisionNo
                Call IniRequisitionHelp(False, mInternalCode)
                Dgl2.AgRowFilter(Dgl2.Columns(Col2RequisionNo).Index) = " IsDeleted = 0 And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' AND CountItem IS NOT NULL "
            Case Col2Item
                If Dgl2.AgSelectedValue(Col2RequisionNo, mRowIndex) <> "" Then
                    Call IniItemHelp(False, Dgl2.AgSelectedValue(Col2RequisionNo, mRowIndex))
                Else
                    Call IniItemHelp()
                    'Dgl2.AgRowFilter(Dgl2.Columns(Col2Item).Index) = " IsDeleted = 0 And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                End If
        End Select
    End Sub

    Private Sub DGL2_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl2.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
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
                    Validating_ReqItem(Dgl2.AgSelectedValue(Col2Item, mRowIndex), mRowIndex)
                Case Col2RequisionNo
                    If Dgl2.AgSelectedValue(Col2RequisionNo, mRowIndex) <> "" Then
                        Dgl2.AgSelectedValue(Col2Item, mRowIndex) = ""
                        Validating_ReqItem(Dgl2.AgSelectedValue(Col2Item, mRowIndex), mRowIndex)
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ReqItem(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl2.Item(Col2Item, mRow).Value.ToString.Trim = "" Or Dgl2.AgSelectedValue(Col2Item, mRow).ToString.Trim = "" Then
                Dgl2.Item(Col2Unit, mRow).Value = ""
                Dgl2.Item(Col2ReqQty, mRow).Value = 0
                Dgl2.Item(Col2MeasurePerPcs, mRow).Value = 0
                Dgl2.Item(Col2MeasureUnit, mRow).Value = ""
                Dgl2.Item(Col2TotalReqMeasure, mRow).Value = 0
                Dgl2.Item(Col2ReqDate, mRow).Value = ""
            Else
                If Dgl2.AgHelpDataSet(Col2Item) IsNot Nothing Then
                    DrTemp = Dgl2.AgHelpDataSet(Col2Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl2.Item(Col2ReqQty, mRow).Value = AgL.VNull(DrTemp(0)("Qty"))
                    Dgl2.Item(Col2Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl2.Item(Col2MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("MeasurePerPcs"))
                    Dgl2.Item(Col2MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl2.Item(Col2TotalReqMeasure, mRow).Value = AgL.VNull(DrTemp(0)("TotalMeasure"))
                    Dgl2.Item(Col2ReqDate, mRow).Value = AgL.XNull(DrTemp(0)("RequireDate"))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_ReqItem Function ")
        End Try
    End Sub

    Private Sub ProcFillIndentDetail()
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim I As Integer = 0
        Dim DsTemp As DataSet

        mQry = "  Declare @TmpTable as Table " & _
                    " ( " & _
                    " RequisionNo nVarchar(21), " & _
                    " Item nVarchar(10), " & _
                    " Qty Float, " & _
                    " Unit nVarchar(10), " & _
                    " MeasurePerPcs Float, " & _
                    " MeasureUnit nVarchar(10), " & _
                    " TotalReqMeasure Float, " & _
                    " ReqDate SmallDateTime " & _
                    " )"

        For I = 0 To Dgl2.RowCount - 1
            mQry += " Insert Into @TmpTable(  " & _
                " RequisionNo, " & _
                " Item,   " & _
                " Qty,   " & _
                " Unit,   " & _
                " MeasurePerPcs,    " & _
                " MeasureUnit,   " & _
                " TotalReqMeasure , " & _
                " ReqDate " & _
                " ) " & _
                " Values ( " & _
                " " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2RequisionNo, I)) & ", " & _
                " " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2Item, I)) & ", " & _
                " " & Val(Dgl2.Item(Col2ReqQty, I).Value) & ", " & _
                " " & AgL.Chk_Text(Dgl2.Item(Col2Unit, I).Value) & ", " & _
                " " & Val(Dgl2.Item(Col2MeasurePerPcs, I).Value) & ", " & _
                " " & AgL.Chk_Text(Dgl2.Item(Col2MeasureUnit, I).Value) & ", " & _
                " " & Val(Dgl2.Item(Col2TotalReqMeasure, I).Value) & ", " & _
                " " & AgL.Chk_Text(Dgl2.Item(Col2ReqDate, I).Value) & " " & _
                ")"
        Next

        mQry += " Select Item,sum(isnull(Qty,0)) AS Qty,max(unit) AS Unit, " & _
                " max(MeasurePerPcs) AS MeasurePerPcs,max(MeasureUnit) AS MeasureUnit, " & _
                " sum(TotalReqMeasure) AS TotalReqMeasure,min(ReqDate) AS ReqDate " & _
                " from @TmpTable " & _
                " WHERE Item IS NOT NULL " & _
                " GROUP BY Item "

        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.AgSelectedValue(Dgl1.Columns(Col1Item).Index, I) = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Dgl1.Columns(Col1IndentQty).Index, I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl1.Item(Dgl1.Columns(Col1ReqQty).Index, I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl1.Item(Dgl1.Columns(Col1Unit).Index, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Dgl1.Columns(Col1MeasurePerPcs).Index, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                    Dgl1.Item(Dgl1.Columns(Col1MeasureUnit).Index, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl1.Item(Dgl1.Columns(Col1TotalReqMeasure).Index, I).Value = AgL.VNull(.Rows(I)("TotalReqMeasure"))
                    Dgl1.Item(Dgl1.Columns(Col1RequireDate).Index, I).Value = AgL.XNull(.Rows(I)("ReqDate"))



                Next I
            End If
        End With
        Call Calculation()
    End Sub

    Private Sub BtnFillIndentDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillIndentDetail.Click
        Call ProcFillIndentDetail()
    End Sub


    Private Sub TempPurchIndentReq_BaseEvent_Topctrl_tbDel() Handles Me.BaseEvent_Topctrl_tbDel
        Dim I As Integer, mSr As Integer
        With Dgl2
            For I = 0 To .RowCount - 1
                If .Item(Col2Item, I).Value <> "" Then
                    mSr += 1
                    If .AgSelectedValue(Col2TempRequisionNo, I) <> "" Then
                        mQry = " UPDATE RequisitionDetail SET PurchaseIndent = NULL " & _
                                " WHERE DocId = " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2TempRequisionNo, I)) & "  " & _
                                " AND Item = " & AgL.Chk_Text(Dgl2.AgSelectedValue(Col2TempItem, I)) & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next
        End With
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
            mQry += " SELECT  @Temp=@Temp +  X.VNo + ', ' FROM (SELECT DISTINCT H.V_Type + '-' + Convert(VARCHAR,H.V_No) AS VNo From PurchQuotationDetail  L LEFT JOIN PurchQuotation H ON L.DocId = H.DocID WHERE L.PurchIndent  = '" & TxtDocId.Text & "') AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox(" Purchase Quotation " & bRData & " created against Indent No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
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

    Private Sub TempPurchIndentReq_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = "SELECT I.Code, I.Description, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " & _
               " IsNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code, isnull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status, I.Measure, MeasureUnit " & _
               " FROM Item I"
        ItemHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select D.Code As Code, D.Description As Department, D.Div_Code  From Department D "
        DepartmentHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT SG.SubCode AS Code,SG.DispName ,isnull(SG.IsDeleted,0) AS IsDeleted, SG.Div_Code , " & _
               " isnull(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status " & _
               " FROM Employee E " & _
               " LEFT JOIN SubGroup SG ON SG.SubCode=E.SubCode "
        IndentorHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub
End Class
