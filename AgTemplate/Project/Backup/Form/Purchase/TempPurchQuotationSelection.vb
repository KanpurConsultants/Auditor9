Imports CrystalDecisions.CrystalReports.Engine
Public Class TempPurchQuotSelectionSelection
    Inherits AgTemplate.TempTransaction
    Public mQry$
    'Dim DsItem As DataSet
    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1Select As String = "Select"
    Protected Const Col1SelectionIndex As String = "Selection Index"
    Protected Const Col1Remarks As String = "Remarks"
    Protected Const Col1PurchIndent As String = "Indent  No"
    Protected Const Col1PurchQuot As String = "Quotation No"
    Protected Const Col1PurchQuotV_Type As String = "PurchQuotV_Type"
    Protected Const Col1PurchQuotV_No As String = "PurchQuotV_No"
    Protected Const Col1PurchQuotV_Date As String = "PurchQuotV_Date"
    Protected Const Col1Vendor As String = "Vendor"
    Protected Const Col1Currency As String = "Currency"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1VendorQuoteNo As String = "Vendor Quote No"
    Protected Const Col1VendorQuoteDate As String = "Vendor Quote Date"
    Protected Const Col1TermsAndConditions As String = "Terms And Conditions"
    Protected Const Col1TotalQty As String = "Total Qty"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1TotalAmount As String = "Total Amount"
    Protected Const Col1NetAmount As String = "Net Amount"
    Protected Const Col1PriceMode As String = "Price Mode"

    'Dim mLastSelectionIndex As Integer = 0

    Public Class HelpDataSet
        Public Shared Employee As DataSet = Nothing
        Public Shared Item As DataSet = Nothing
        Public Shared PurchIndent As DataSet = Nothing
        Public Shared PurchQuotation As DataSet = Nothing
        Public Shared Vendor As DataSet = Nothing
    End Class

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtEmployee = New AgControls.AgTextBox
        Me.LblEmployee = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.LblPurchaseQuotationDetail = New System.Windows.Forms.LinkLabel
        Me.TxtItem = New AgControls.AgTextBox
        Me.LblItem = New System.Windows.Forms.Label
        Me.LblEmployeeReq = New System.Windows.Forms.Label
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.BtnFill = New System.Windows.Forms.Button
        Me.LblItemReq = New System.Windows.Forms.Label
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
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(809, 425)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(631, 425)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(453, 425)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(160, 425)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(11, 425)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 421)
        Me.GroupBox1.Size = New System.Drawing.Size(990, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(309, 425)
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
        Me.LblV_No.Location = New System.Drawing.Point(455, 36)
        Me.LblV_No.Tag = ""
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(547, 35)
        Me.TxtV_No.Size = New System.Drawing.Size(151, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(330, 41)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(228, 36)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(531, 21)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(346, 35)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(454, 17)
        Me.LblV_Type.Tag = ""
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(547, 15)
        Me.TxtV_Type.Size = New System.Drawing.Size(151, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(330, 21)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(228, 17)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(346, 15)
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
        Me.LblPrefix.Location = New System.Drawing.Point(823, 32)
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
        Me.TP1.Controls.Add(Me.LblItemReq)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.LblEmployee)
        Me.TP1.Controls.Add(Me.TxtEmployee)
        Me.TP1.Controls.Add(Me.LblItem)
        Me.TP1.Controls.Add(Me.TxtItem)
        Me.TP1.Controls.Add(Me.LblEmployeeReq)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(966, 118)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblEmployeeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtItem, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblItem, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtEmployee, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblEmployee, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblItemReq, 0)
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
        'TxtEmployee
        '
        Me.TxtEmployee.AgMandatory = True
        Me.TxtEmployee.AgMasterHelp = False
        Me.TxtEmployee.AgNumberLeftPlaces = 8
        Me.TxtEmployee.AgNumberNegetiveAllow = False
        Me.TxtEmployee.AgNumberRightPlaces = 2
        Me.TxtEmployee.AgPickFromLastValue = False
        Me.TxtEmployee.AgRowFilter = ""
        Me.TxtEmployee.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtEmployee.AgSelectedValue = Nothing
        Me.TxtEmployee.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEmployee.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEmployee.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtEmployee.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmployee.Location = New System.Drawing.Point(346, 55)
        Me.TxtEmployee.MaxLength = 50
        Me.TxtEmployee.Name = "TxtEmployee"
        Me.TxtEmployee.Size = New System.Drawing.Size(352, 18)
        Me.TxtEmployee.TabIndex = 4
        '
        'LblEmployee
        '
        Me.LblEmployee.AutoSize = True
        Me.LblEmployee.BackColor = System.Drawing.Color.Transparent
        Me.LblEmployee.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEmployee.Location = New System.Drawing.Point(228, 55)
        Me.LblEmployee.Name = "LblEmployee"
        Me.LblEmployee.Size = New System.Drawing.Size(66, 16)
        Me.LblEmployee.TabIndex = 706
        Me.LblEmployee.Text = "Employee"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(3, 190)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(964, 230)
        Me.Pnl1.TabIndex = 2
        '
        'LblPurchaseQuotationDetail
        '
        Me.LblPurchaseQuotationDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblPurchaseQuotationDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblPurchaseQuotationDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPurchaseQuotationDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblPurchaseQuotationDetail.LinkColor = System.Drawing.Color.White
        Me.LblPurchaseQuotationDetail.Location = New System.Drawing.Point(3, 169)
        Me.LblPurchaseQuotationDetail.Name = "LblPurchaseQuotationDetail"
        Me.LblPurchaseQuotationDetail.Size = New System.Drawing.Size(185, 20)
        Me.LblPurchaseQuotationDetail.TabIndex = 731
        Me.LblPurchaseQuotationDetail.TabStop = True
        Me.LblPurchaseQuotationDetail.Text = "Purchase Quotation Detail"
        Me.LblPurchaseQuotationDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItem
        '
        Me.TxtItem.AgMandatory = True
        Me.TxtItem.AgMasterHelp = False
        Me.TxtItem.AgNumberLeftPlaces = 8
        Me.TxtItem.AgNumberNegetiveAllow = False
        Me.TxtItem.AgNumberRightPlaces = 2
        Me.TxtItem.AgPickFromLastValue = False
        Me.TxtItem.AgRowFilter = ""
        Me.TxtItem.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItem.AgSelectedValue = Nothing
        Me.TxtItem.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItem.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItem.Location = New System.Drawing.Point(346, 75)
        Me.TxtItem.MaxLength = 20
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(352, 18)
        Me.TxtItem.TabIndex = 5
        '
        'LblItem
        '
        Me.LblItem.AutoSize = True
        Me.LblItem.BackColor = System.Drawing.Color.Transparent
        Me.LblItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItem.Location = New System.Drawing.Point(228, 75)
        Me.LblItem.Name = "LblItem"
        Me.LblItem.Size = New System.Drawing.Size(33, 16)
        Me.LblItem.TabIndex = 731
        Me.LblItem.Text = "Item"
        '
        'LblEmployeeReq
        '
        Me.LblEmployeeReq.AutoSize = True
        Me.LblEmployeeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblEmployeeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblEmployeeReq.Location = New System.Drawing.Point(330, 61)
        Me.LblEmployeeReq.Name = "LblEmployeeReq"
        Me.LblEmployeeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblEmployeeReq.TabIndex = 733
        Me.LblEmployeeReq.Text = "Ä"
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
        Me.TxtStructure.Location = New System.Drawing.Point(816, 73)
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
        Me.Label25.Location = New System.Drawing.Point(749, 73)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 747
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(0, 0)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(200, 100)
        Me.PnlCalcGrid.TabIndex = 0
        '
        'BtnFill
        '
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.Location = New System.Drawing.Point(892, 167)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(75, 22)
        Me.BtnFill.TabIndex = 1
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = True
        '
        'LblItemReq
        '
        Me.LblItemReq.AutoSize = True
        Me.LblItemReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblItemReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblItemReq.Location = New System.Drawing.Point(330, 81)
        Me.LblItemReq.Name = "LblItemReq"
        Me.LblItemReq.Size = New System.Drawing.Size(10, 7)
        Me.LblItemReq.TabIndex = 748
        Me.LblItemReq.Text = "Ä"
        '
        'TempPurchQuotSelectionSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(972, 466)
        Me.Controls.Add(Me.LblPurchaseQuotationDetail)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.BtnFill)
        Me.Name = "TempPurchQuotSelectionSelection"
        Me.Text = "Template Purchase Quotation"
        Me.Controls.SetChildIndex(Me.BtnFill, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LblPurchaseQuotationDetail, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
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
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtEmployee As AgControls.AgTextBox
    Protected WithEvents LblEmployee As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblPurchaseQuotationDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtItem As AgControls.AgTextBox
    Protected WithEvents LblItem As System.Windows.Forms.Label
    Protected WithEvents LblEmployeeReq As System.Windows.Forms.Label
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents BtnFill As System.Windows.Forms.Button
    Protected WithEvents LblItemReq As System.Windows.Forms.Label
#End Region

    Private Sub TempGr_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim I As Integer

        '------------------------------------------------------------------------
        'Updating Purchase Quotation Selection Detail In Purchase Quotation As Null
        '-------------------------------------------------------------------------
        mQry = " UPDATE PurchQuotationDetail " & _
                " SET QuotSelection = NULL, " & _
                " QuotSelectionIndex = NULL, " & _
                " QuotSelectionV_Type = NULL, " & _
                " QuotSelectionV_No = NULL, " & _
                " QuotSelectionV_Date = NULL " & _
                " WHERE QuotSelection = '" & mInternalCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        '------------------------------------------------------------------------
        'Updating Purchase Quotation Selection Detail In Purchase Quotation
        '-------------------------------------------------------------------------
        With Dgl1
            For I = 0 To .RowCount - 1
                If AgL.StrCmp(.Item(Col1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                    mQry = " UPDATE PurchQuotationDetail " & _
                            " SET QuotSelection = " & AgL.Chk_Text(mInternalCode) & ", " & _
                            " QuotSelectionIndex = " & Val(.Item(Col1SelectionIndex, I).Value) & ", " & _
                            " QuotSelectionV_Type = " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & _
                            " QuotSelectionV_No = " & Val(TxtV_No.Text) & ", " & _
                            " QuotSelectionV_Date = " & AgL.Chk_Text(TxtV_Date.Text) & " " & _
                            " WHERE DocId = '" & .AgSelectedValue(Col1PurchQuot, I) & "' " & _
                            " And Item = '" & TxtItem.AgSelectedValue & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
        '-------------------------------------------------------------------------
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchQuotSelection"
        LogTableName = "PurchQuotSelection_Log"
        MainLineTableCsv = "PurchQuotSelectionDetail"
        LogLineTableCsv = "PurchQuotSelectionDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " & _
            " From PurchQuotSelection H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select H.UID As SearchCode " & _
               " From PurchQuotSelection_Log H " & _
               " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
               " Where H.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By H.EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = "SELECT H.DocID, Vt.Description AS [Entry Type], H.V_Date AS [Entry Date], " & _
        '                    " H.V_No AS [Entry No], Sg.DispName As Employee " & _
        '                    " FROM PurchQuotSelection H " & _
        '                    " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup Sg On H.Employee = Sg.SubCode " & _
        '                    " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr
        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Selection Type], H.V_Prefix AS Prefix, H.V_Date AS [Selection Date], H.V_No AS [Selection No], " & _
                            " H.Structure, H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status],  " & _
                            " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, " & _
                            " D.Div_Name AS Division,SM.Name AS [Site Name], SGE.DispName AS [Employee Name], I.Description AS  Item " & _
                            " FROM  PurchQuotSelection H " & _
                            " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                            " LEFT JOIN SubGroup SGE ON SGE.SubCode  = H.Employee  " & _
                            " LEFT JOIN Item I ON I.Code=H.Item  " & _
                            " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT H.UID as SearchCode, H.DocId, Vt.Description AS [Entry Type], " & _
        '                    " H.V_Date AS [Entry Date], H.V_No AS [Entry No], " & _
        '                    " Sg.DispName As Employee " & _
        '                    " FROM PurchQuotSelection_Log H " & _
        '                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup Sg On H.Employee = Sg.SubCode " & _
        '                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Selection Type], H.V_Prefix AS Prefix, H.V_Date AS [Selection Date], H.V_No AS [Selection No], " & _
                    " H.Structure, H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status],  " & _
                    " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, " & _
                    " D.Div_Name AS Division,SM.Name AS [Site Name], SGE.DispName AS [Employee Name], I.Description AS  Item " & _
                    " FROM  PurchQuotSelection_Log H " & _
                    " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                    " LEFT JOIN SubGroup SGE ON SGE.SubCode  = H.Employee  " & _
                    " LEFT JOIN Item I ON I.Code=H.Item  " & _
                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgCheckColumn(Dgl1, Col1Select, 45, Col1Select, True)
            .AddAgNumberColumn(Dgl1, Col1SelectionIndex, 60, 5, 0, False, Col1SelectionIndex, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Remarks, 200, 255, Col1Remarks, True, False, False)
            .AddAgTextColumn(Dgl1, Col1PurchIndent, 80, 5, Col1PurchIndent, True, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchQuot, 80, 5, Col1PurchQuot, True, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchQuotV_Type, 100, 5, Col1PurchQuotV_Type, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1PurchQuotV_No, 100, 5, 0, False, Col1PurchQuotV_No, False, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchQuotV_Date, 100, 5, Col1PurchQuotV_Date, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Vendor, 150, 5, Col1Vendor, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Currency, 70, 5, Col1Currency, True, True, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 70, 5, Col1BillingType, True, True, False)
            .AddAgTextColumn(Dgl1, Col1VendorQuoteNo, 70, 5, Col1VendorQuoteNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1VendorQuoteDate, 80, 5, Col1VendorQuoteDate, True, True, False)
            .AddAgTextColumn(Dgl1, Col1TermsAndConditions, 200, 5, Col1TermsAndConditions, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1TotalQty, 100, 5, 4, False, Col1TotalQty, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 5, 4, False, Col1TotalMeasure, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1TotalAmount, 100, 5, 2, False, Col1TotalAmount, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1NetAmount, 100, 5, 2, False, Col1NetAmount, True, True, False)
            .AddAgTextColumn(Dgl1, Col1PriceMode, 100, 5, Col1PriceMode, True, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AllowUserToAddRows = False

        Dgl1.Columns(Col1PurchQuot).CellTemplate.Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Underline)
        Dgl1.Columns(Col1PurchQuot).CellTemplate.Style.ForeColor = Color.Blue

        Dgl1.Columns(Col1PurchIndent).CellTemplate.Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Underline)
        Dgl1.Columns(Col1PurchIndent).CellTemplate.Style.ForeColor = Color.Blue

        Dgl1.AgSkipReadOnlyColumns = True

        FrmProductionOrder_BaseFunction_FIniList()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = " UPDATE PurchQuotSelection_Log " & _
                " SET Employee = " & AgL.Chk_Text(TxtEmployee.AgSelectedValue) & ", " & _
                " Item = " & AgL.Chk_Text(TxtItem.AgSelectedValue) & ", " & _
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & " " & _
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From PurchQuotSelectionDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If AgL.StrCmp(.Item(Col1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                    mSr += 1
                    mQry = " INSERT INTO PurchQuotSelectionDetail_Log(UID, DocId, Sr, SelectionIndex, " & _
                            " Remarks, PurchIndent, " & _
                            " PurchQuot, PurchQuotV_Type, PurchQuotV_No, PurchQuotV_Date, Vendor, VendorName, " & _
                            " Currency, BillingType, VendorQuoteNo, VendorQuoteDate, TermsAndConditions, " & _
                            " TotalQty, TotalMeasure, TotalAmount, NetAmount, PriceMode) " & _
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " & mSr & ", " & _
                            " " & Val(.Item(Col1SelectionIndex, I).Value) & ", " & AgL.Chk_Text(.Item(Col1Remarks, I).Value) & ",  " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1PurchIndent, I)) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1PurchQuot, I)) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1PurchQuotV_Type, I).Value) & ", " & _
                            " " & Val(.Item(Col1PurchQuotV_No, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1PurchQuotV_Date, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1Vendor, I)) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1Vendor, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1Currency, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1BillingType, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1VendorQuoteNo, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1VendorQuoteDate, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1TermsAndConditions, I).Value) & ", " & _
                            " " & Val(.Item(Col1TotalQty, I).Value) & ", " & _
                            " " & Val(.Item(Col1TotalMeasure, I).Value) & ", " & _
                            " " & Val(.Item(Col1TotalAmount, I).Value) & ", " & _
                            " " & Val(.Item(Col1NetAmount, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1PriceMode, I).Value) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
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
                    " From PurchQuotSelection P " & _
                    " Where P.DocID = '" & SearchCode & "'"
            Else
                mQry = "Select P.* " & _
                    " From PurchQuotSelection_Log P " & _
                    " Where P.UID='" & SearchCode & "'"
            End If
            DsTemp = AgL.FillData(mQry, AgL.GCn)

            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)

                    If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                        TxtStructure.AgSelectedValue = AgL.XNull(.Rows(0)("Structure"))
                    End If

                IniGrid()

                TxtEmployee.AgSelectedValue = AgL.XNull(.Rows(0)("Employee"))
                TxtItem.AgSelectedValue = AgL.XNull(.Rows(0)("Item"))
                TxtStructure.AgSelectedValue = AgL.XNull(.Rows(0)("Structure"))

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from PurchQuotSelectionDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from PurchQuotSelectionDetail_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(Col1Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
                            Dgl1.Item(Col1SelectionIndex, I).Value = AgL.VNull(.Rows(I)("SelectionIndex"))
                            Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                            Dgl1.AgSelectedValue(Col1PurchIndent, I) = AgL.XNull(.Rows(I)("PurchIndent"))
                            Dgl1.AgSelectedValue(Col1PurchQuot, I) = AgL.XNull(.Rows(I)("PurchQuot"))
                            Dgl1.Item(Col1PurchQuotV_Type, I).Value = AgL.XNull(.Rows(I)("PurchQuotV_Type"))
                            Dgl1.Item(Col1PurchQuotV_No, I).Value = AgL.VNull(.Rows(I)("PurchQuotV_No"))
                            Dgl1.Item(Col1PurchQuotV_Date, I).Value = AgL.XNull(.Rows(I)("PurchQuotV_Date"))
                            Dgl1.AgSelectedValue(Col1Vendor, I) = AgL.XNull(.Rows(I)("Vendor"))
                            Dgl1.Item(Col1Currency, I).Value = AgL.XNull(.Rows(I)("Currency"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1VendorQuoteNo, I).Value = AgL.XNull(.Rows(I)("VendorQuoteNo"))
                            Dgl1.Item(Col1VendorQuoteDate, I).Value = AgL.XNull(.Rows(I)("VendorQuoteDate"))
                            Dgl1.Item(Col1TermsAndConditions, I).Value = AgL.XNull(.Rows(I)("TermsAndConditions"))
                            Dgl1.Item(Col1TotalQty, I).Value = AgL.VNull(.Rows(I)("TotalQty"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                            Dgl1.Item(Col1TotalAmount, I).Value = AgL.VNull(.Rows(I)("TotalAmount"))
                            Dgl1.Item(Col1NetAmount, I).Value = AgL.VNull(.Rows(I)("NetAmount"))
                            Dgl1.Item(Col1PriceMode, I).Value = AgL.XNull(.Rows(I)("PriceMode"))

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

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        TxtEmployee.AgHelpDataSet(5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Employee
        TxtItem.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Item
        Dgl1.AgHelpDataSet(Col1PurchIndent, 3) = HelpDataSet.PurchIndent
        Dgl1.AgHelpDataSet(Col1PurchQuot, 3) = HelpDataSet.PurchQuotation
        Dgl1.AgHelpDataSet(Col1Vendor, 3) = HelpDataSet.Vendor
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bIsSelected As Boolean = False
        If AgL.RequiredField(TxtEmployee, LblEmployee.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtItem, LblItem.Text) Then passed = False : Exit Sub
        If Dgl1.Rows.Count = 0 Then MsgBox("No Transaction Data...!", MsgBoxStyle.Information) : passed = False : Exit Sub
        With Dgl1
            For I = 0 To .Rows.Count - 1
                If AgL.StrCmp(.Item(Col1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                    bIsSelected = True : Exit For
                End If
            Next
        End With
        If bIsSelected = False Then MsgBox("No Quotation Is Selected...!", MsgBoxStyle.Information) : passed = False : Exit Sub
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        'mLastSelectionIndex = 0
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtEmployee.Enter, TxtItem.Enter
        Try
            Select Case sender.name
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    IniGrid()

                Case TxtEmployee.Name
                    TxtEmployee.AgRowFilter = " IsDeleted = 0 And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' And " & ClsMain.RetDivFilterStr & "  "

                Case TxtItem.Name
                    'TxtItem.AgRowFilter = " IsDeleted = 0 And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' And " & ClsMain.RetDivFilterStr & " "
                    TxtItem.AgRowFilter = " IsDeleted = 0 And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempPurchQuotSelection_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        IniGrid()
    End Sub

    Private Sub ProcFillPurchIndentDetail()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            mQry = " SELECT H.PurchIndent, H.DocID AS PurchQuot, H.V_Type AS PurchQuotV_Type, " & _
                        " H.V_No AS PurchQuotV_No, H.V_Date AS PurchQuotV_Date, H.Vendor, H.Currency, " & _
                        " H.BillingType, H.VendorQuoteNo, H.VendorQuoteDate, H.TermsAndConditions, H.TotalQty, " & _
                        " H.TotalMeasure, H.TotalAmount, H.PriceMode, " & _
                        " Sh.Amount As NetAmount " & _
                        " FROM PurchQuotation H " & _
                        " LEFT JOIN PurchQuotationDetail  L ON H.DocID = L.DocId " & _
                        " LEFT JOIN Structure_TransFooter Sh On H.DocId = Sh.DocId And Sh.Charges = '" & ClsMain.Charges.NETAMOUNT & "'" & _
                        " WHERE L.Item = '" & TxtItem.AgSelectedValue & "' " & _
                        " AND H.V_Date <= '" & TxtV_Date.Text & "'  "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                        Dgl1.Item(Col1Select, I).Value = AgLibrary.ClsConstant.StrUnCheckedValue
                        Dgl1.AgSelectedValue(Col1PurchIndent, I) = AgL.XNull(.Rows(I)("PurchIndent"))
                        Dgl1.AgSelectedValue(Col1PurchQuot, I) = AgL.XNull(.Rows(I)("PurchQuot"))
                        Dgl1.Item(Col1PurchQuotV_Type, I).Value = AgL.XNull(.Rows(I)("PurchQuotV_Type"))
                        Dgl1.Item(Col1PurchQuotV_No, I).Value = AgL.VNull(.Rows(I)("PurchQuotV_No"))
                        Dgl1.Item(Col1PurchQuotV_Date, I).Value = AgL.XNull(.Rows(I)("PurchQuotV_Date"))
                        Dgl1.AgSelectedValue(Col1Vendor, I) = AgL.XNull(.Rows(I)("Vendor"))
                        Dgl1.Item(Col1Currency, I).Value = AgL.XNull(.Rows(I)("Currency"))
                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                        Dgl1.Item(Col1VendorQuoteNo, I).Value = AgL.XNull(.Rows(I)("VendorQuoteNo"))
                        Dgl1.Item(Col1VendorQuoteDate, I).Value = AgL.XNull(.Rows(I)("VendorQuoteDate"))
                        Dgl1.Item(Col1TermsAndConditions, I).Value = AgL.XNull(.Rows(I)("TermsAndConditions"))
                        Dgl1.Item(Col1TotalQty, I).Value = AgL.VNull(.Rows(I)("TotalQty"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                        Dgl1.Item(Col1TotalAmount, I).Value = AgL.VNull(.Rows(I)("TotalAmount"))
                        Dgl1.Item(Col1NetAmount, I).Value = AgL.VNull(.Rows(I)("NetAmount"))
                        Dgl1.Item(Col1PriceMode, I).Value = AgL.XNull(.Rows(I)("PriceMode"))
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempPurchQuotSelection_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = "Select H.SubCode As Code, Sg.DispName As Employee, Sg.ManualCode , " & _
                " C.CityName As EmployeeCity, " & _
                " IsNull(Sg.IsDeleted,0) As IsDeleted, " & _
                " IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) As Status, " & _
                " Sg.Div_Code " & _
                " From Employee H " & _
                " LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode " & _
                " LEFT JOIN City C On Sg.CityCode = C.CityCode "
        HelpDataSet.Employee = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code AS Code, H.Description AS Item, " & _
                " IsNull(H.IsDeleted,0) As IsDeleted, " & _
                " IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) As Status, " & _
                " H.Div_Code, H.ItemType " & _
                " FROM Item H  "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.DocID AS Code,H.V_Type + '-' + Convert(NVARCHAR(5),H.V_No) AS IndentNo , " & _
                  " isnull(H.IsDeleted,0) AS IsDeleted, H.Div_Code , " & _
                  " isnull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status, " & _
                  " H.V_Date As IndentDate, Vt.NCat, H.MoveToLog " & _
                  " FROM PurchIndent  H " & _
                  " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type "
        HelpDataSet.PurchIndent = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.DocID AS Code,H.V_Type + '-' + Convert(NVARCHAR(5),H.V_No) AS QuatationNo , " & _
                " isnull(H.IsDeleted,0) AS IsDeleted, H.Div_Code , " & _
                " isnull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status , " & _
                " H.V_Date As QuotationDate, Vt.NCat, H.MoveToLog  " & _
                " FROM PurchQuotation  H " & _
                " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type "
        HelpDataSet.PurchQuotation = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Sg.SubCode, Sg.DispName AS [Name], " & _
               " IsNull(Sg.IsDeleted,0) AS IsDeleted, " & _
               " isnull(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status, " & _
               " Sg.Div_Code " & _
               " FROM Vendor H " & _
               " LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
        HelpDataSet.Vendor = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Dgl1_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl1.CellMouseUp
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex
            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    Call AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col1Select).Index)
                    Call ProcSetSelectionIndex(mColumnIndex, mRowIndex)
            End Select
            Calculation()
        Catch ex As Exception
            Call ProcSetSelectionIndex(mColumnIndex, mRowIndex)
            Calculation()
        End Try
    End Sub

    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        If Dgl1.Rows.Count = 0 Then Exit Sub
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        Try
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        Call AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col1Select).Index)
                        Call ProcSetSelectionIndex(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex)
                    End If
            End Select
        Catch ex As Exception
            Call ProcSetSelectionIndex(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex)
            Calculation()
        End Try
    End Sub

    Private Sub ProcSetSelectionIndex(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim I As Integer = 0
        Dim bMaxSelectionIndex As Integer = 0
        Try
            With Dgl1
                If AgL.StrCmp(Dgl1.Item(Col1Select, RowIndex).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                    For I = 0 To .Rows.Count - 1
                        If Val(.Item(Col1SelectionIndex, I).Value) > bMaxSelectionIndex Then
                            bMaxSelectionIndex = Val(.Item(Col1SelectionIndex, I).Value)
                        End If
                    Next
                    Dgl1.Item(Col1SelectionIndex, RowIndex).Value = bMaxSelectionIndex + 1
                    'bMaxSelectionIndex = bMaxSelectionIndex + 1
                Else
                    For I = 0 To Dgl1.Rows.Count - 1
                        If Val(.Item(Col1SelectionIndex, I).Value) > Val(.Item(Col1SelectionIndex, RowIndex).Value) Then
                            Dgl1.Item(Col1SelectionIndex, I).Value = Val(.Item(Col1SelectionIndex, I).Value) - 1
                        End If
                    Next
                    Dgl1.Item(Col1SelectionIndex, RowIndex).Value = ""
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        Call ProcFillPurchIndentDetail()
    End Sub

    Private Sub TempPurchQuotSelectionSelection_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
            BtnFill.Enabled = True
        Else
            BtnFill.Enabled = False
        End If
    End Sub

    Private Sub TempPurchQuotSelectionSelection_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " UPDATE PurchQuotationDetail " & _
                " SET QuotSelection = NULL, " & _
                " QuotSelectionIndex = NULL, " & _
                " QuotSelectionV_Type = NULL, " & _
                " QuotSelectionV_No = NULL, " & _
                " QuotSelectionV_Date = NULL " & _
                " WHERE QuotSelection = '" & mInternalCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub Dgl1_CellMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl1.CellMouseMove
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1PurchQuot, Col1PurchIndent
                    Dgl1.Cursor = Cursors.Hand

                Case Else
                    Dgl1.Cursor = Cursors.Default
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Function Validate_PurchQuotation(ByVal RowIndex As Integer) As Boolean
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet = Nothing
        Try
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Function

            If Dgl1.AgSelectedValue(Col1PurchQuot, RowIndex) <> "" Then
                DrTemp = Dgl1.AgHelpDataSet(Col1PurchQuot).Tables(0).Select("Code = '" & Dgl1.AgSelectedValue(Col1PurchQuot, RowIndex) & "' ")
                If DrTemp.Length > 0 Then
                    If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("MoveToLog")), "") Then
                        MsgBox("Currently Purchase Quotation """ & Dgl1.Item(Col1PurchQuot, RowIndex).Value & """ Is In Log For Modification." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                        Exit Function
                    End If

                    If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("Status")), AgTemplate.ClsMain.EntryStatus.Active) Then
                        MsgBox("Currently Purchase Quotation """ & Dgl1.Item(Col1PurchQuot, RowIndex).Value & """ Is Not In Active State." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                        Exit Function
                    End If
                End If
            End If
            Validate_PurchQuotation = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub TempPurchInvoice_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        With Dgl1
            For I = 0 To .Rows.Count - 1
                If AgL.StrCmp(.Item(Col1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                    If Validate_PurchQuotation(I) = False Then passed = False : Exit Sub
                End If
            Next
        End With
    End Sub
End Class
