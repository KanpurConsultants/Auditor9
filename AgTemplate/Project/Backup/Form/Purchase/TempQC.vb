Public Class TempQC
    Inherits AgTemplate.TempTransaction
    Public mQry$
    Dim mTransFlag As Boolean = False
    Public mMainSr As Integer
    Dim StrTblParamIni As String = " Declare @TblParm AS Table(TSr INT, Sr INT, Parameter NVARCHAR(50), StdValue NVARCHAR(50), " & _
                                    " ActValue NVARCHAR(50), Qty FLOAT, QcQty FLOAT, PassedQty FLOAT, RejectQty Float, " & _
                                    " Unit NVARCHAR (10), MeasurePerPcs FLOAT , MeasureUnit NVARCHAR (10) ,TotalMeasure FLOAT ," & _
                                    " TotalPassedMeasure FLOAT ,	TotalRejectMeasure FLOAT ,	Remarks  NVARCHAR (255) , " & _
                                    " UID UNIQUEIDENTIFIER )"
    Dim StrTblParam As String = ""


    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1ChallanNo As String = "Challan No."
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1QCDetail As String = "QC Detail"
    Protected Const Col1QCQty As String = "QC Qty"
    Protected Const Col1TempQCQty As String = "Temp QC Qty"
    Protected Const Col1PassedQty As String = "Passed Qty"
    Protected Const Col1RejectedQty As String = "Rejected Qty"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1TempChallanNo As String = "Temp Challan No"
    Protected Const Col1TempItem As String = "Temp Item"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1TotalPassedMeasure As String = "Total Passed Measure"
    Protected Const Col1TotalRejectMeasure As String = "Total Reject Measure"

    Public Class HelpDataSet
        Public Shared Vendor As DataSet = Nothing
        Public Shared QcBy As DataSet = Nothing
        Public Shared ChallanNo As DataSet = Nothing
        Public Shared Item As DataSet = Nothing
        Public Shared ItemFromChallan As DataSet = Nothing
    End Class

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtQCBy = New AgControls.AgTextBox
        Me.LblQCBY = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblValTotalQCQty = New System.Windows.Forms.Label
        Me.LblTextTotalQCQty = New System.Windows.Forms.Label
        Me.LblValTotalRejected = New System.Windows.Forms.Label
        Me.LblValueTotalPassed = New System.Windows.Forms.Label
        Me.LblTextTotalRejected = New System.Windows.Forms.Label
        Me.LblTextTotalPassedQty = New System.Windows.Forms.Label
        Me.LblValTotalQty = New System.Windows.Forms.Label
        Me.LblTextTotalQty = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LblChallanNoReq = New System.Windows.Forms.Label
        Me.TxtChallanNo = New AgControls.AgTextBox
        Me.LblChallanNo = New System.Windows.Forms.Label
        Me.TxtVendor = New AgControls.AgTextBox
        Me.LblVendor = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(756, 505)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(596, 505)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(421, 505)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(145, 505)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(11, 505)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 496)
        Me.GroupBox1.Size = New System.Drawing.Size(930, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(287, 505)
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
        Me.LblV_No.Location = New System.Drawing.Point(479, 37)
        Me.LblV_No.Size = New System.Drawing.Size(51, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "QC No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(597, 35)
        Me.TxtV_No.Size = New System.Drawing.Size(135, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(306, 42)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(168, 37)
        Me.LblV_Date.Size = New System.Drawing.Size(58, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "QC Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(581, 23)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(322, 35)
        Me.TxtV_Date.Size = New System.Drawing.Size(135, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(479, 18)
        Me.LblV_Type.Size = New System.Drawing.Size(59, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "QC Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(597, 16)
        Me.TxtV_Type.Size = New System.Drawing.Size(135, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(306, 22)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(168, 18)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(322, 16)
        Me.TxtSite_Code.Size = New System.Drawing.Size(135, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(20, 35)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(3, 20)
        Me.TabControl1.Size = New System.Drawing.Size(908, 152)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.LblVendor)
        Me.TP1.Controls.Add(Me.LblChallanNo)
        Me.TP1.Controls.Add(Me.LblChallanNoReq)
        Me.TP1.Controls.Add(Me.TxtChallanNo)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtQCBy)
        Me.TP1.Controls.Add(Me.LblQCBY)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(900, 126)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblQCBY, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtQCBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtChallanNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblChallanNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblChallanNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(912, 41)
        Me.Topctrl1.TabIndex = 2
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
        'TxtQCBy
        '
        Me.TxtQCBy.AgMandatory = True
        Me.TxtQCBy.AgMasterHelp = False
        Me.TxtQCBy.AgNumberLeftPlaces = 8
        Me.TxtQCBy.AgNumberNegetiveAllow = False
        Me.TxtQCBy.AgNumberRightPlaces = 2
        Me.TxtQCBy.AgPickFromLastValue = False
        Me.TxtQCBy.AgRowFilter = ""
        Me.TxtQCBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtQCBy.AgSelectedValue = Nothing
        Me.TxtQCBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtQCBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtQCBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtQCBy.Enabled = False
        Me.TxtQCBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtQCBy.Location = New System.Drawing.Point(322, 73)
        Me.TxtQCBy.MaxLength = 50
        Me.TxtQCBy.Name = "TxtQCBy"
        Me.TxtQCBy.Size = New System.Drawing.Size(410, 18)
        Me.TxtQCBy.TabIndex = 6
        '
        'LblQCBY
        '
        Me.LblQCBY.AutoSize = True
        Me.LblQCBY.BackColor = System.Drawing.Color.Transparent
        Me.LblQCBY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblQCBY.Location = New System.Drawing.Point(168, 75)
        Me.LblQCBY.Name = "LblQCBY"
        Me.LblQCBY.Size = New System.Drawing.Size(47, 16)
        Me.LblQCBY.TabIndex = 706
        Me.LblQCBY.Text = "QC By"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblValTotalQCQty)
        Me.Panel1.Controls.Add(Me.LblTextTotalQCQty)
        Me.Panel1.Controls.Add(Me.LblValTotalRejected)
        Me.Panel1.Controls.Add(Me.LblValueTotalPassed)
        Me.Panel1.Controls.Add(Me.LblTextTotalRejected)
        Me.Panel1.Controls.Add(Me.LblTextTotalPassedQty)
        Me.Panel1.Controls.Add(Me.LblValTotalQty)
        Me.Panel1.Controls.Add(Me.LblTextTotalQty)
        Me.Panel1.Location = New System.Drawing.Point(14, 470)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(886, 21)
        Me.Panel1.TabIndex = 694
        '
        'LblValTotalQCQty
        '
        Me.LblValTotalQCQty.AutoSize = True
        Me.LblValTotalQCQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblValTotalQCQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblValTotalQCQty.Location = New System.Drawing.Point(310, 4)
        Me.LblValTotalQCQty.Name = "LblValTotalQCQty"
        Me.LblValTotalQCQty.Size = New System.Drawing.Size(12, 16)
        Me.LblValTotalQCQty.TabIndex = 738
        Me.LblValTotalQCQty.Text = "."
        Me.LblValTotalQCQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTextTotalQCQty
        '
        Me.LblTextTotalQCQty.AutoSize = True
        Me.LblTextTotalQCQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTextTotalQCQty.ForeColor = System.Drawing.Color.Maroon
        Me.LblTextTotalQCQty.Location = New System.Drawing.Point(206, 3)
        Me.LblTextTotalQCQty.Name = "LblTextTotalQCQty"
        Me.LblTextTotalQCQty.Size = New System.Drawing.Size(96, 16)
        Me.LblTextTotalQCQty.TabIndex = 737
        Me.LblTextTotalQCQty.Text = "Total QC Qty :"
        '
        'LblValTotalRejected
        '
        Me.LblValTotalRejected.AutoSize = True
        Me.LblValTotalRejected.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblValTotalRejected.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblValTotalRejected.Location = New System.Drawing.Point(807, 4)
        Me.LblValTotalRejected.Name = "LblValTotalRejected"
        Me.LblValTotalRejected.Size = New System.Drawing.Size(12, 16)
        Me.LblValTotalRejected.TabIndex = 736
        Me.LblValTotalRejected.Text = "."
        Me.LblValTotalRejected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblValueTotalPassed
        '
        Me.LblValueTotalPassed.AutoSize = True
        Me.LblValueTotalPassed.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblValueTotalPassed.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblValueTotalPassed.Location = New System.Drawing.Point(545, 4)
        Me.LblValueTotalPassed.Name = "LblValueTotalPassed"
        Me.LblValueTotalPassed.Size = New System.Drawing.Size(12, 16)
        Me.LblValueTotalPassed.TabIndex = 672
        Me.LblValueTotalPassed.Text = "."
        Me.LblValueTotalPassed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTextTotalRejected
        '
        Me.LblTextTotalRejected.AutoSize = True
        Me.LblTextTotalRejected.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTextTotalRejected.ForeColor = System.Drawing.Color.Maroon
        Me.LblTextTotalRejected.Location = New System.Drawing.Point(692, 3)
        Me.LblTextTotalRejected.Name = "LblTextTotalRejected"
        Me.LblTextTotalRejected.Size = New System.Drawing.Size(108, 16)
        Me.LblTextTotalRejected.TabIndex = 735
        Me.LblTextTotalRejected.Text = "Total Rejected :"
        '
        'LblTextTotalPassedQty
        '
        Me.LblTextTotalPassedQty.AutoSize = True
        Me.LblTextTotalPassedQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTextTotalPassedQty.ForeColor = System.Drawing.Color.Maroon
        Me.LblTextTotalPassedQty.Location = New System.Drawing.Point(447, 3)
        Me.LblTextTotalPassedQty.Name = "LblTextTotalPassedQty"
        Me.LblTextTotalPassedQty.Size = New System.Drawing.Size(97, 16)
        Me.LblTextTotalPassedQty.TabIndex = 671
        Me.LblTextTotalPassedQty.Text = "Total Passed :"
        '
        'LblValTotalQty
        '
        Me.LblValTotalQty.AutoSize = True
        Me.LblValTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblValTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblValTotalQty.Location = New System.Drawing.Point(82, 4)
        Me.LblValTotalQty.Name = "LblValTotalQty"
        Me.LblValTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblValTotalQty.TabIndex = 668
        Me.LblValTotalQty.Text = "."
        Me.LblValTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTextTotalQty
        '
        Me.LblTextTotalQty.AutoSize = True
        Me.LblTextTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTextTotalQty.ForeColor = System.Drawing.Color.Maroon
        Me.LblTextTotalQty.Location = New System.Drawing.Point(9, 3)
        Me.LblTextTotalQty.Name = "LblTextTotalQty"
        Me.LblTextTotalQty.Size = New System.Drawing.Size(73, 16)
        Me.LblTextTotalQty.TabIndex = 667
        Me.LblTextTotalQty.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(14, 199)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(886, 271)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(168, 94)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(322, 92)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(410, 18)
        Me.TxtRemarks.TabIndex = 7
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(14, 178)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(96, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "QC Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblChallanNoReq
        '
        Me.LblChallanNoReq.AutoSize = True
        Me.LblChallanNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblChallanNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblChallanNoReq.Location = New System.Drawing.Point(581, 62)
        Me.LblChallanNoReq.Name = "LblChallanNoReq"
        Me.LblChallanNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblChallanNoReq.TabIndex = 732
        Me.LblChallanNoReq.Text = "Ä"
        '
        'TxtChallanNo
        '
        Me.TxtChallanNo.AgMandatory = True
        Me.TxtChallanNo.AgMasterHelp = False
        Me.TxtChallanNo.AgNumberLeftPlaces = 8
        Me.TxtChallanNo.AgNumberNegetiveAllow = False
        Me.TxtChallanNo.AgNumberRightPlaces = 2
        Me.TxtChallanNo.AgPickFromLastValue = False
        Me.TxtChallanNo.AgRowFilter = ""
        Me.TxtChallanNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChallanNo.AgSelectedValue = Nothing
        Me.TxtChallanNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChallanNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtChallanNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChallanNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChallanNo.Location = New System.Drawing.Point(597, 54)
        Me.TxtChallanNo.MaxLength = 20
        Me.TxtChallanNo.Name = "TxtChallanNo"
        Me.TxtChallanNo.Size = New System.Drawing.Size(135, 18)
        Me.TxtChallanNo.TabIndex = 5
        '
        'LblChallanNo
        '
        Me.LblChallanNo.AutoSize = True
        Me.LblChallanNo.BackColor = System.Drawing.Color.Transparent
        Me.LblChallanNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblChallanNo.Location = New System.Drawing.Point(479, 55)
        Me.LblChallanNo.Name = "LblChallanNo"
        Me.LblChallanNo.Size = New System.Drawing.Size(75, 16)
        Me.LblChallanNo.TabIndex = 731
        Me.LblChallanNo.Text = "Challan No."
        '
        'TxtVendor
        '
        Me.TxtVendor.AgMandatory = False
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
        Me.TxtVendor.Location = New System.Drawing.Point(322, 54)
        Me.TxtVendor.MaxLength = 50
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(135, 18)
        Me.TxtVendor.TabIndex = 4
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(170, 55)
        Me.LblVendor.Name = "LblVendor"
        Me.LblVendor.Size = New System.Drawing.Size(49, 16)
        Me.LblVendor.TabIndex = 734
        Me.LblVendor.Text = "Vendor"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(306, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 735
        Me.Label1.Text = "Ä"
        '
        'TempQC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(912, 546)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "TempQC"
        Me.Text = "Template QC "
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
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
    Protected WithEvents TxtQCBy As AgControls.AgTextBox
    Protected WithEvents LblQCBY As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblValTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTextTotalQty As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblChallanNoReq As System.Windows.Forms.Label
    Protected WithEvents TxtChallanNo As AgControls.AgTextBox
    Protected WithEvents LblChallanNo As System.Windows.Forms.Label
    Protected WithEvents TxtVendor As AgControls.AgTextBox
    Protected WithEvents LblVendor As System.Windows.Forms.Label
    Protected WithEvents LblValueTotalPassed As System.Windows.Forms.Label
    Protected WithEvents LblValTotalRejected As System.Windows.Forms.Label
    Protected WithEvents LblTextTotalRejected As System.Windows.Forms.Label
    Protected WithEvents LblValTotalQCQty As System.Windows.Forms.Label
    Protected WithEvents LblTextTotalQCQty As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LblTextTotalPassedQty As System.Windows.Forms.Label
#End Region

    Private Sub TempBookIssue_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim I As Integer, mSr As Integer
        If TxtEntryType.Text = "Delete" Then
            With Dgl1
                For I = 0 To .RowCount - 1
                    If .AgSelectedValue(Col1Item, I) <> "" And .AgSelectedValue(Col1ChallanNo, I) <> "" Then
                        mSr += 1

                        mQry = " UPDATE PurchChallanDetail SET QcQty = (  " & _
                                " SELECT ISNULL(QCQty,0) FROM PurchChallanDetail  " & _
                                " WHERE DocId =" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1TempChallanNo, I)) & " " & _
                                " AND  Item=" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1TempItem, I)) & "  " & _
                                " ) - " & Val(Dgl1.Item(Col1TempQCQty, I).Value) & "" & _
                                " WHERE DocId =" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1TempChallanNo, I)) & " " & _
                                " AND  Item=" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1TempItem, I)) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                    End If
                Next
            End With
        Else

            With Dgl1
                For I = 0 To .RowCount - 1
                    If .AgSelectedValue(Col1Item, I) = Nothing Then .AgSelectedValue(Col1Item, I) = ""
                    If .AgSelectedValue(Col1ChallanNo, I) = Nothing Then .AgSelectedValue(Col1ChallanNo, I) = ""

                    If .AgSelectedValue(Col1Item, I) <> "" And .AgSelectedValue(Col1ChallanNo, I) <> "" Then
                        mSr += 1
                        mQry = " UPDATE PurchChallanDetail SET QcQty = (  " & _
                                " SELECT ISNULL(QCQty,0) FROM PurchChallanDetail  " & _
                                " WHERE DocId =" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1TempChallanNo, I)) & " " & _
                                " AND  Item=" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1TempItem, I)) & "  " & _
                                " ) - " & Val(Dgl1.Item(Col1TempQCQty, I).Value) & "" & _
                                " WHERE DocId =" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1TempChallanNo, I)) & " " & _
                                " AND  Item=" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1TempItem, I)) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = " UPDATE PurchChallanDetail SET QcQty = (  " & _
                                " SELECT ISNULL(QCQty,0) FROM PurchChallanDetail  " & _
                                " WHERE DocId =" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1ChallanNo, I)) & " " & _
                                " AND  Item=" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & "  " & _
                                " ) + " & Val(Dgl1.Item(Col1QCQty, I).Value) & "" & _
                                " WHERE DocId =" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1ChallanNo, I)) & " " & _
                                " AND  Item=" & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    End If
                Next
            End With
        End If
    End Sub

    Private Sub TempRequisition_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "QC"
        LogTableName = "QC_Log"
        MainLineTableCsv = "QcDetail,QcParameterDetail"
        LogLineTableCsv = "QcDetail_Log,QcParameterDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmTempRequisition_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
               " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " & _
            " From QC H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmTempRequisition_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select H.UID As SearchCode " & _
               " From QC_Log H " & _
               " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
               " Where H.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By H.EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmTempRequisition_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT H.DocID AS SearchCode,H.V_Type + ' - ' + convert(NVARCHAR(5),H.V_No) AS [QC No.],H.V_Date AS [QC Date], " & _
        '                    " PC.V_Type + ' - ' + convert(NVARCHAR(5),PC.V_No) AS [Challan No.],  " & _
        '                    " SG.DispName AS [Vendor Name],SGQ.DispName AS [QC By],H.TotalQty AS [Total Qty.], " & _
        '                    " H.TotalPassedQty AS [Total Passed Qty.], H.TotalRejectQty AS [Total Rejected Qty.], " & _
        '                    " H.Remarks " & _
        '                    " FROM QC H " & _
        '                    " LEFT JOIN PurchChallan PC ON PC.DocID =H.PurchChallan  " & _
        '                    " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN SubGroup  SG ON SG.SubCode =PC.Vendor  " & _
        '                    " LEFT JOIN subGroup SGQ ON SGQ.SubCode=H.QcBy " & _
        '                    " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.DeliveryMeasure AS [Delivery Measure], H.V_Type AS [QC Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [QC No] , " & _
                            " SG.DispName AS [QC By], H.Remarks, H.TotalQty AS [Total Qty], H.TotalPassedQty AS [Total Passed Qty], H.TotalRejectQty AS [Total Reject Qty], H.TotalMeasure AS [Total Measure], " & _
                            " H.TotalPassedMeasure AS [Total Passed Measure], H.TotalRejectMeasure AS [Total Reject Measure], H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type],  " & _
                            " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date],  H.Status,  " & _
                            " SM.Name AS [Site Name], D.Div_Name AS Division, DE.Description AS Department, PC.ReferenceNo AS [Challan No] " & _
                            " FROM  QC H  " & _
                            " LEFT JOIN SiteMast SM ON SM.Code =H.Site_Code   " & _
                            " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                            " LEFT JOIN Department DE ON DE.Code =H.Department " & _
                            " LEFT JOIN PurchChallan PC ON PC.DocID =H.PurchChallan   " & _
                            " LEFT JOIN SubGroup SG ON SG.SubCode=H.QcBy   " & _
                             " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " & _
                            " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr
        AgL.PubFindQryOrdBy = "[Date]"
    End Sub

    Private Sub FrmTempRequisition_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"


        'AgL.PubFindQry = " SELECT H.DocID AS SearchCode,H.V_Type + ' - ' + convert(NVARCHAR(5),H.V_No) AS [QC No.],H.V_Date AS [QC Date], " & _
        '            " PC.V_Type + ' - ' + convert(NVARCHAR(5),PC.V_No) AS [Challan No.],  " & _
        '            " SG.DispName AS [Vendor Name],SGQ.DispName AS [QC By],H.TotalQty AS [Total Qty.], " & _
        '            " H.TotalPassedQty AS [Total Passed Qty.], H.TotalRejectQty AS [Total Rejected Qty.], " & _
        '            " H.Remarks " & _
        '            " FROM QC_Log H " & _
        '            " LEFT JOIN PurchChallan PC ON PC.DocID =H.PurchChallan  " & _
        '            " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '            " LEFT JOIN SubGroup  SG ON SG.SubCode =PC.Vendor  " & _
        '            " LEFT JOIN subGroup SGQ ON SGQ.SubCode=H.QcBy " & _
        '            " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.DeliveryMeasure AS [Delivery Measure], H.V_Type AS [QC Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [QC No] , " & _
                    " SG.DispName AS [QC By], H.Remarks, H.TotalQty AS [Total Qty], H.TotalPassedQty AS [Total Passed Qty], H.TotalRejectQty AS [Total Reject Qty], H.TotalMeasure AS [Total Measure], " & _
                    " H.TotalPassedMeasure AS [Total Passed Measure], H.TotalRejectMeasure AS [Total Reject Measure], H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type],  " & _
                    " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date],  H.Status,  " & _
                    " SM.Name AS [Site Name], D.Div_Name AS Division, DE.Description AS Department, PC.ReferenceNo AS [Challan No]   " & _
                    " FROM  QC_Log H  " & _
                    " LEFT JOIN SiteMast SM ON SM.Code =H.Site_Code   " & _
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                    " LEFT JOIN Department DE ON DE.Code =H.Department " & _
                    " LEFT JOIN PurchChallan PC ON PC.DocID =H.PurchChallan   " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode=H.QcBy   " & _
                    " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " & _
                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr


        AgL.PubFindQryOrdBy = "[Date]"
    End Sub

    Private Sub FrmTempRequisition_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ChallanNo, 100, 20, Col1ChallanNo, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 170, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 20, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 65, 5, 4, False, Col1MeasurePerPcs, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 80, 20, Col1MeasureUnit, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 65, 5, 4, False, Col1TotalMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalPassedMeasure, 65, 5, 4, False, Col1TotalPassedMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalRejectMeasure, 65, 5, 4, False, Col1TotalRejectMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 65, 5, 4, False, Col1Qty, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1QCQty, 65, 5, 4, False, Col1QCQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1TempQCQty, 65, 5, 4, False, Col1TempQCQty, False, False, True)
            .AddAgButtonColumn(Dgl1, Col1QCDetail, 50, Col1QCDetail, True, False, , , , "Webdings", 10, "6")
            .AddAgNumberColumn(Dgl1, Col1PassedQty, 65, 5, 4, False, Col1PassedQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1RejectedQty, 65, 5, 4, False, Col1RejectedQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1TempChallanNo, 100, 20, Col1TempChallanNo, False, False)
            .AddAgTextColumn(Dgl1, Col1TempItem, 100, 20, Col1TempItem, False, False)

        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35

        Dgl1.AgSkipReadOnlyColumns = True
        'Dgl1.Anchor = AnchorStyles.None
        'Panel1.Anchor = Dgl1.Anchor
    End Sub

    Private Sub FrmTempRequisition_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim dsTemp As DataSet = Nothing
        ' Dim bUID As String
        mQry = " UPDATE QC_Log SET " & _
                    " PurchChallan = " & AgL.Chk_Text(TxtChallanNo.AgSelectedValue) & ", " & _
                    " QcBy = " & AgL.Chk_Text(TxtQCBy.AgSelectedValue) & ", " & _
                    " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                    " TotalQty = " & Val(LblValTotalQty.Text) & ", " & _
                    " TotalPassedQty = " & Val(LblValueTotalPassed.Text) & ", " & _
                    " TotalRejectQty = " & Val(LblValTotalRejected.Text) & " " & _
                    " WHERE UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From QCDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mSr += 1

                    mQry = " INSERT INTO QCDetail_Log (DocId, Sr, Item, Qty, QcQty, PassedQty, " & _
                            " RejectQty, Unit, MeasurePerPcs, MeasureUnit, TotalMeasure,  " & _
                            " TotalPassedMeasure, TotalRejectMeasure, Remarks, UID, PurchChallan) " & _
                            " VALUES (" & AgL.Chk_Text(mInternalCode) & "," & mSr & ", " & _
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & Val(Dgl1.Item(Col1QCQty, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1PassedQty, I).Value) & ", " & Val(Dgl1.Item(Col1RejectedQty, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ",  " & _
                            " " & Val(Dgl1.Item(Col1TotalPassedMeasure, I).Value) & ", " & Val(Dgl1.Item(Col1TotalRejectMeasure, I).Value) & ",  " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", 	" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1ChallanNo, I)) & " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                End If
            Next
        End With

        '-------------------------------- < To Save Parameter Detail >-----------------------------------------------------

        mQry = StrTblParam & " SELECT * FROM @TblParm  "
        dsTemp = AgL.FillData(mQry, AgL.GcnRead)
        With DsTemp.Tables(0)

            mQry = "Delete From QCParameterDetail_Log Where UID = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            If .Rows.Count > 0 Then
                For I = 0 To dsTemp.Tables(0).Rows.Count - 1
                    mQry = " INSERT INTO QCParameterDetail_Log (DocId, TSr, Sr, Parameter, Qty, " & _
                            " QcQty, PassedQty, RejectQty, Unit, MeasurePerPcs, MeasureUnit,  " & _
                            " TotalMeasure, TotalPassedMeasure, TotalRejectMeasure, Remarks,  " & _
                            " UID, StdValue, ActValue) " & _
                            " VALUES (" & AgL.Chk_Text(mInternalCode) & "," & Val(AgL.VNull(.Rows(I)("TSr"))) & ", " & Val(AgL.VNull(.Rows(I)("Sr"))) & ", " & _
                            " " & AgL.Chk_Text(AgL.XNull(.Rows(I)("Parameter"))) & "," & Val(AgL.VNull(.Rows(I)("Qty"))) & ", " & Val(AgL.VNull(.Rows(I)("QCQty"))) & ",  " & _
                            " " & Val(AgL.VNull(.Rows(I)("PassedQty"))) & ", " & Val(AgL.VNull(.Rows(I)("RejectQty"))) & ", " & AgL.Chk_Text(AgL.XNull(.Rows(I)("Unit"))) & ", " & Val(AgL.VNull(.Rows(I)("MeasurePerPcs"))) & ",  " & _
                            " " & AgL.Chk_Text(AgL.XNull(.Rows(I)("MeasureUnit"))) & ", " & Val(AgL.VNull(.Rows(I)("TotalMeasure"))) & ", " & Val(AgL.VNull(.Rows(I)("TotalPassedMeasure"))) & ",  " & _
                            " " & Val(AgL.VNull(.Rows(I)("TotalRejectMeasure"))) & ", " & AgL.Chk_Text(AgL.XNull(.Rows(I)("Remarks"))) & ", '" & SearchCode & "', " & AgL.Chk_Text(AgL.XNull(.Rows(I)("StdValue"))) & ", " & AgL.Chk_Text(AgL.XNull(.Rows(I)("ActValue"))) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                Next I
            End If
        End With

    End Sub

    Private Sub FrmTempRequisition_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        IniItemHelp(True)

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select H.* " & _
                " From QC H " & _
                " Where H.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select H.* " & _
                " From QC_Log H " & _
                " Where H.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtChallanNo.AgSelectedValue = AgL.XNull(.Rows(0)("PurchChallan"))
                TxtQCBy.AgSelectedValue = AgL.XNull(.Rows(0)("QcBy"))

                LblValTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblValueTotalPassed.Text = AgL.VNull(.Rows(0)("TotalPassedQty"))
                LblValTotalRejected.Text = AgL.VNull(.Rows(0)("TotalRejectQty"))

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                Validating_ChallanNo(TxtChallanNo.AgSelectedValue)

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * FROM QcDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from QcDetail_Log where UID = '" & SearchCode & "' Order By Sr"
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
                            Dgl1.AgSelectedValue(Col1TempItem, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.AgSelectedValue(Col1ChallanNo, I) = AgL.XNull(.Rows(I)("PurchChallan"))
                            Dgl1.AgSelectedValue(Col1TempChallanNo, I) = AgL.XNull(.Rows(I)("PurchChallan"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.000")
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QCQty, I).Value = AgL.VNull(.Rows(I)("QcQty"))
                            Dgl1.Item(Col1TempQCQty, I).Value = AgL.VNull(.Rows(I)("QcQty"))
                            Dgl1.Item(Col1PassedQty, I).Value = AgL.VNull(.Rows(I)("PassedQty"))
                            Dgl1.Item(Col1RejectedQty, I).Value = AgL.VNull(.Rows(I)("RejectQty"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                            Dgl1.Item(Col1TotalPassedMeasure, I).Value = AgL.VNull(.Rows(I)("TotalPassedMeasure"))
                            Dgl1.Item(Col1TotalRejectMeasure, I).Value = AgL.VNull(.Rows(I)("TotalRejectMeasure"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                        Next I
                    End If
                End With

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from QcParameterDetail Where Docid = '" & mSearchCode & "'"
                Else
                    mQry = "Select * from QcParameterDetail_Log Where UID = '" & mSearchCode & "'"
                End If
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                StrTblParam = StrTblParamIni
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            StrTblParam += " INSERT INTO @TblParm  ( TSr, Sr, Parameter, Qty, QcQty, " & _
                                            " PassedQty, RejectQty, Unit, MeasurePerPcs, MeasureUnit, TotalMeasure,  " & _
                                            " TotalPassedMeasure, TotalRejectMeasure, Remarks,  " & _
                                            " UID, StdValue, ActValue) " & _
                                            " VALUES ( " & Val(AgL.VNull(.Rows(I)("TSr"))) & ", " & Val(AgL.VNull(.Rows(I)("Sr"))) & ", " & _
                                            " " & AgL.Chk_Text(AgL.XNull(.Rows(I)("Parameter"))) & ", " & Val(AgL.VNull(.Rows(I)("Qty"))) & ", " & Val(AgL.VNull(.Rows(I)("QCQty"))) & ",  " & _
                                            " " & Val(AgL.VNull(.Rows(I)("PassedQty"))) & ", " & Val(AgL.VNull(.Rows(I)("RejectQty"))) & ", " & _
                                            " " & AgL.Chk_Text(AgL.XNull(.Rows(I)("Unit"))) & ", " & Val(AgL.VNull(.Rows(I)("MeasurePerPcs"))) & ", " & AgL.Chk_Text(AgL.XNull(.Rows(I)("MeasureUnit"))) & ",  " & _
                                            " " & Val(AgL.VNull(.Rows(I)("TotalMeasure"))) & ", " & Val(AgL.VNull(.Rows(I)("TotalPassedMeasure"))) & ", " & _
                                            " " & Val(AgL.VNull(.Rows(I)("TotalRejectMeasure"))) & ",  " & _
                                            " " & AgL.Chk_Text(AgL.XNull(.Rows(I)("Remarks"))) & ", " & AgL.Chk_Text(.Rows(I)("UID").ToString()) & ", " & _
                                            " " & AgL.Chk_Text(AgL.XNull(.Rows(I)("StdValue"))) & ", " & AgL.Chk_Text(AgL.XNull(.Rows(I)("ActValue"))) & ") "
                        Next I
                    End If
                End With

                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmTempRequisition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub

    Private Sub Validating_ChallanNo(ByVal ChallanDocId As String)
        Dim DrTemp As DataRow() = Nothing
        If TxtChallanNo.AgSelectedValue <> "" Then
            DrTemp = TxtChallanNo.AgHelpDataSet.Tables(0).Select(" Code = '" & ChallanDocId & "' ")
            If DrTemp.Length > 0 Then
                TxtVendor.AgSelectedValue = AgL.XNull(DrTemp(0)("Vendor"))
            Else
                TxtVendor.AgSelectedValue = ""
            End If
        End If
    End Sub

    Private Sub FrmTempRequisition_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        TxtVendor.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Vendor
        TxtQCBy.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.QcBy
        TxtChallanNo.AgHelpDataSet(7, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.ChallanNo
        Dgl1.AgHelpDataSet(Col1ChallanNo, 7) = HelpDataSet.ChallanNo
        Dgl1.AgHelpDataSet(Col1TempChallanNo, 6) = HelpDataSet.ChallanNo
        Dgl1.AgHelpDataSet(Col1TempItem, 4) = HelpDataSet.Item
        IniItemHelp(True)
    End Sub

    Public Sub IniItemHelp(Optional ByVal All_Records As Boolean = True)
        If All_Records = True Then
            Dgl1.AgHelpDataSet(Col1Item, 4) = HelpDataSet.Item
        Else
            Dgl1.AgHelpDataSet(Col1Item, 10) = HelpDataSet.ItemFromChallan
        End If
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If TxtChallanNo.AgSelectedValue <> "" And Dgl1.AgSelectedValue(Col1ChallanNo, Dgl1.CurrentCell.RowIndex) = "" Then
                        Dgl1.AgSelectedValue(Col1ChallanNo, Dgl1.CurrentCell.RowIndex) = TxtChallanNo.AgSelectedValue
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " Code = " & AgL.Chk_Text(Dgl1.AgSelectedValue(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex)) & "  " & _
                                " OR IsDeleted = 0 " & _
                                " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                                " And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                " And BalQty > 0  "
                    End If
                    If Dgl1.AgSelectedValue(Col1ChallanNo, Dgl1.CurrentCell.RowIndex) <> "" Then
                        Call IniItemHelp(False)
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " Code = " & AgL.Chk_Text(Dgl1.AgSelectedValue(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex)) & "  " & _
                                " OR IsDeleted = 0 " & _
                                " And ChallanDocId = '" & Dgl1.AgSelectedValue(Col1ChallanNo, Dgl1.CurrentCell.RowIndex) & "'" & _
                                " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                                " And Status ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                " And BalQty > 0  "
                    End If
                    
                Case Col1ChallanNo
                    If TxtChallanNo.AgSelectedValue <> "" Then
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1ChallanNo).Index) = " Code = " & AgL.Chk_Text(Dgl1.AgSelectedValue(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex)) & " OR  " & _
                            " IsDeleted = 0 " & _
                            " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                            " And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " AND Code=" & AgL.Chk_Text(TxtChallanNo.AgSelectedValue) & "  " & _
                            " AND BalQty > 0 "
                    Else
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1ChallanNo).Index) = " Code = " & AgL.Chk_Text(Dgl1.AgSelectedValue(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex)) & " OR " & _
                            " IsDeleted = 0 " & _
                            " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " & _
                            " And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "'" & _
                            " AND  BalQty > 0 "
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmTempRequisition_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        Dim bTotalQty As Double = 0, bTotalQCQty As Double = 0, bTotalPassedQty As Double = 0, bTotalRejectedQty As Double = 0
        Dim bTotalMeasure As Double = 0, bTotalPassedMeasure As Double = 0, bTotalRejectMeasure As Double = 0


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                bTotalQty = bTotalQty + Val(Dgl1.Item(Col1Qty, I).Value)
                bTotalQCQty = bTotalQCQty + Val(Dgl1.Item(Col1QCQty, I).Value)
                bTotalPassedQty = bTotalPassedQty + Val(Dgl1.Item(Col1PassedQty, I).Value)
                bTotalRejectedQty = bTotalRejectedQty + Val(Dgl1.Item(Col1RejectedQty, I).Value)

                Dgl1.Item(Col1TotalMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1Qty, I).Value)
                Dgl1.Item(Col1TotalPassedMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1PassedQty, I).Value)
                Dgl1.Item(Col1TotalRejectMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1RejectedQty, I).Value)

                bTotalMeasure = bTotalMeasure + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                bTotalPassedMeasure = bTotalPassedMeasure + Val(Dgl1.Item(Col1TotalPassedMeasure, I).Value)
                bTotalRejectMeasure = bTotalRejectMeasure + Val(Dgl1.Item(Col1TotalRejectMeasure, I).Value)

            End If
        Next

        LblValTotalQty.Text = Format(Val(bTotalQty), "0.000")
        LblValTotalQCQty.Text = Format(Val(bTotalQCQty), "0.000")
        LblValueTotalPassed.Text = Format(Val(bTotalPassedQty), "0.000")
        LblValTotalRejected.Text = Format(Val(bTotalRejectedQty), "0.000")
    End Sub

    Private Sub FrmTempRequisition_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtChallanNo, LblChallanNo.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtQCBy, LblQCBY.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1ChallanNo, I).Value <> "" Then
                    If Validate_PurchChallan(Dgl1, I) = False Then passed = False : Exit Sub
                End If
            Next
        End With
    End Sub

    Private Sub FrmTempRequisition_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblValTotalQty.Text = 0 : LblValTotalQCQty.Text = 0
        LblValueTotalPassed.Text = 0 : LblValTotalRejected.Text = 0
        LblChallanNoReq.Tag = ""
        LblVendor.Tag = ""

    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtQCBy.Validating, TxtChallanNo.Validating, TxtVendor.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.name
                Case TxtChallanNo.Name
                    If TxtChallanNo.AgSelectedValue.ToString.Trim = "" Or TxtChallanNo.Text.ToString.Trim = "" Then
                        TxtVendor.AgSelectedValue = ""
                    Else
                        If TxtChallanNo.AgHelpDataSet IsNot Nothing Then
                            e.Cancel = Not Validate_PurchChallan(TxtChallanNo)
                            If e.Cancel = False Then
                                DrTemp = TxtChallanNo.AgHelpDataSet.Tables(0).Select(" Code = '" & TxtChallanNo.AgSelectedValue & "' ")
                                TxtVendor.AgSelectedValue = AgL.XNull(DrTemp(0)("Vendor"))
                            End If
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtChallanNo.Enter, TxtQCBy.Enter
        Try
            Select Case sender.name
                Case TxtChallanNo.Name
                    If TxtVendor.AgSelectedValue <> "" Then
                        sender.AgRowFilter = " IsDeleted = 0  " & _
                            " And Status ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " AND " & ClsMain.RetDivFilterStr & " " & _
                            " AND Vendor = " & AgL.Chk_Text(TxtVendor.AgSelectedValue) & " " & _
                            " AND BalQty > 0  "
                    Else
                        sender.AgRowFilter = "  IsDeleted = 0 " & _
                                " And Status ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                " AND " & ClsMain.RetDivFilterStr & "" & _
                                " AND BalQty > 0  "
                    End If

                Case TxtVendor.Name, TxtQCBy.Name
                    sender.AgRowFilter = "  IsDeleted = 0 " & _
                        " And Status ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                        " AND " & ClsMain.RetDivFilterStr & " "

            End Select
        Catch ex As Exception
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
                Case Col1ChallanNo
                    e.Cancel = Not Validate_PurchChallan(Dgl1, Dgl1.CurrentCell.RowIndex)

                Case Col1Item
                    Validating_Item(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mRowIndex)

                Case Col1PassedQty
                    If Val(Dgl1.Item(Col1PassedQty, mRowIndex).Value) > Val(Dgl1.Item(Col1QCQty, mRowIndex).Value) Then
                        MsgBox(" Qty Is Invalid !") : Dgl1.Item(Col1PassedQty, mRowIndex).Value = Dgl1.Item(Col1QCQty, mRowIndex).Value
                        Dgl1.Item(Col1RejectedQty, mRowIndex).Value = Val(Dgl1.Item(Col1QCQty, mRowIndex).Value) - Val(Dgl1.Item(Col1PassedQty, mRowIndex).Value)
                    Else
                        Dgl1.Item(Col1RejectedQty, mRowIndex).Value = Val(Dgl1.Item(Col1QCQty, mRowIndex).Value) - Val(Dgl1.Item(Col1PassedQty, mRowIndex).Value)
                    End If

                Case Col1RejectedQty
                    If Val(Dgl1.Item(Col1RejectedQty, mRowIndex).Value) > Val(Dgl1.Item(Col1QCQty, mRowIndex).Value) Then
                        MsgBox(" Qty Is Invalid !") : Dgl1.Item(Col1RejectedQty, mRowIndex).Value = Dgl1.Item(Col1QCQty, mRowIndex).Value
                        Dgl1.Item(Col1PassedQty, mRowIndex).Value = Val(Dgl1.Item(Col1QCQty, mRowIndex).Value) - Val(Dgl1.Item(Col1RejectedQty, mRowIndex).Value)
                    Else
                        Dgl1.Item(Col1PassedQty, mRowIndex).Value = Val(Dgl1.Item(Col1QCQty, mRowIndex).Value) - Val(Dgl1.Item(Col1RejectedQty, mRowIndex).Value)
                    End If

            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
                Dgl1.AgSelectedValue(Col1ChallanNo, mRow) = ""
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1Qty, mRow).Value = "0"
            Else
                If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                    DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl1.AgSelectedValue(Col1ChallanNo, mRow) = AgL.XNull(DrTemp(0)("ChallanDocId"))
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(DrTemp(0)("Qty"))
                    Dgl1.Item(Col1QCQty, mRow).Value = AgL.VNull(DrTemp(0)("BalQty"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("Measure"))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub FrmIssueDonatedBook_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'TxtQCBy.Enabled = False
        'TxtVendor.Enabled = False

    End Sub

    Private Sub TempBookIssue_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        StrTblParam = StrTblParamIni
        TxtVendor.Focus()
    End Sub

    Private Sub DGL1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim DsTemp As DataSet
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub

        Dim FrmObj As TempQCParameterDetail

        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1QCDetail

                    mQry = StrTblParam & " Select * FROM @TblParm WHERE TSr = " & Val(Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Value) & " "
                    DsTemp = AgL.FillData(mQry, AgL.GCn)

                    FrmObj = New TempQCParameterDetail()
                    CType(FrmObj, TempQCParameterDetail).StrQCDocId = mInternalCode
                    CType(FrmObj, TempQCParameterDetail).StrItemCode = Dgl1.AgSelectedValue(Col1Item, mRowIndex)
                    CType(FrmObj, TempQCParameterDetail).DblItemQty = Val(Dgl1.Item(Col1Qty, mRowIndex).Value)
                    CType(FrmObj, TempQCParameterDetail).DblQCQty = Val(Dgl1.Item(Col1QCQty, mRowIndex).Value)
                    CType(FrmObj, TempQCParameterDetail).DataGridValue = DsTemp.Tables(0)

                    FrmObj.ShowDialog()
                    If FrmObj.mOkButtonPressed Then

                        Dgl1.Item(Col1PassedQty, Dgl1.CurrentCell.RowIndex).Value = Val(FrmObj.Dgl1.Item(FrmObj.Col1PassedQty, 0).Value)
                        Dgl1.Item(Col1RejectedQty, Dgl1.CurrentCell.RowIndex).Value = Val(FrmObj.Dgl1.Item(FrmObj.Col1RejectQty, 0).Value)


                        StrTblParam += "Delete from @TblParm Where Tsr=" & Val(Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Value) & " "
                        If FrmObj.Dgl1.RowCount > 0 Then
                            With FrmObj.Dgl1
                                For I = 0 To FrmObj.Dgl1.RowCount - 1
                                    StrTblParam += " INSERT INTO @TblParm  ( TSr, Sr, Parameter, Qty, QcQty, " & _
                                                " PassedQty, RejectQty, Unit, MeasurePerPcs, MeasureUnit, TotalMeasure,  " & _
                                                " TotalPassedMeasure, TotalRejectMeasure, Remarks, " & _
                                                " StdValue, ActValue) " & _
                                                " VALUES ( " & Val(Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Value) & ", " & I + 1 & ", " & _
                                                " " & AgL.Chk_Text(.Item(FrmObj.Col1Parameter, I).Value) & ", " & Val(.Item(FrmObj.Col1Qty, I).Value) & ", " & Val(.Item(FrmObj.Col1QcQty, I).Value) & ",  " & _
                                                " " & Val(.Item(FrmObj.Col1PassedQty, I).Value) & ", " & Val(.Item(FrmObj.Col1RejectQty, I).Value) & ", " & _
                                                " " & AgL.Chk_Text(.Item(FrmObj.Col1Unit, I).Value) & ", " & Val(.Item(FrmObj.Col1MeasurePerPcs, I).Value) & ", " & AgL.Chk_Text(.Item(FrmObj.Col1MeasureUnit, I).Value) & ",  " & _
                                                " " & Val(.Item(FrmObj.Col1TotalMeasure, I).Value) & ", " & Val(.Item(FrmObj.Col1TotalPassedMeasure, I).Value) & ", " & _
                                                " " & Val(.Item(FrmObj.Col1TotalRejectMeasure, I).Value) & ", " & AgL.Chk_Text(.Item(FrmObj.Col1Remarks, I).Value) & " , " & _
                                                " " & AgL.Chk_Text(.Item(FrmObj.Col1StdValue, I).Value) & ", " & AgL.Chk_Text(.Item(FrmObj.Col1ActualValue, I).Value) & ") "
                                Next
                            End With
                        End If
                    End If
                    FrmObj = Nothing
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempQC_BaseEvent_Topctrl_tbEdit() Handles Me.BaseEvent_Topctrl_tbEdit
        TxtVendor.Focus()
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub TempQC_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = " SELECT  V.SubCode AS Code,SG.DispName AS [Vendor Name], " & _
                     " Isnull(SG.IsDeleted,0) AS IsDeleted, " & _
                     " IsNull(SG.Status , '" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status,SG.Div_Code " & _
                     " FROM Vendor V  " & _
                     " LEFT JOIN SubGroup SG ON SG.SubCode=V.SubCode "
        HelpDataSet.Vendor = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT E.SubCode AS Code, SG.DispName AS Name, " & _
                " Isnull(SG.IsDeleted,0) AS IsDeleted, " & _
                " IsNull(SG.Status , '" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status,SG.Div_Code " & _
                " FROM Employee E  " & _
                " LEFT JOIN SubGroup SG  ON SG.SubCode = E.SubCode "
        HelpDataSet.QcBy = AgL.FillData(mQry, AgL.GCn)

        'Start Code Change By Satyam on 18/11/2011
        mQry = " SELECT H.DocID AS Code,H.V_Type + '-' + Convert(NVARCHAR(5),H.V_No ) AS ChllanNo, " & _
                " H.V_Date AS [Challan Date ],H.ReferenceNo AS [Manual No],SG.DispName AS [Vendor Name], " & _
                " H.VendorDocNo AS [Vendor Doc. No],H.vendor, " & _
                " Isnull(H.IsDeleted,0) AS IsDeleted,   " & _
                " IsNull(H.Status , '" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status, " & _
                " H.Div_Code , Vt.NCat, H.TotalQty - isnull(V1.QCQty,0) AS BalQty, H.MoveToLog " & _
                " FROM PurchChallan H " & _
                " LEFT JOIN  " & _
                " ( " & _
                " SELECT L.DocId, Sum(isnull(L.QcQty,0))  AS QCQty " & _
                " FROM PurchChallanDetail L  " & _
                " GROUP BY  L.DocId " & _
                " ) V1 ON V1.DocId =H.DocId " & _
                " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & _
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.Vendor "
        HelpDataSet.ChallanNo = AgL.FillData(mQry, AgL.GCn)
        'End Code Change By Satyam on 18/11/2011

        mQry = " SELECT I.Code, I.Description AS Item,I.Unit,Isnull(I.IsDeleted,0) AS IsDeleted, '' AS ChallanDocId, " & _
                    " IsNull(I.Status , '" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status, " & _
                    " I.Div_Code,I.ItemType,0 AS BalQty  " & _
                    " FROM Item I "
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT L.Item AS Code,  I.Description AS Item,I.Unit,H.DocId AS ChallanDocId, " & _
                    " Isnull(H.IsDeleted,0) AS IsDeleted, " & _
                    " IsNull(H.Status ,'" & AgTemplate.ClsMain.EntryStatus.Active & "' ) AS Status, " & _
                    " H.Div_Code , L.Qty, I.Measure, I.MeasureUnit, L.Qty - ISNULL(L.QcQty,0) AS balQty,I.ItemType " & _
                    " FROM PurchChallanDetail L " & _
                    " LEFT JOIN PurchChallan H ON H.DocID = L.DocId  " & _
                    " LEFT JOIN Item I ON I.Code=L.Item  " & _
                    " Left Join " & _
                    " ( " & _
                    " SELECT QD.Item,sum(isnull(QD.QcQty,0)) AS  QcQty ,QD.PurchChallan  " & _
                    " FROM QcDetail QD " & _
                    " GROUP BY QD.Item,QD.PurchChallan  " & _
                    " ) V1 ON V1.Item = L.Item AND V1.PurchChallan=H.DocId  "
        HelpDataSet.ItemFromChallan = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Function Validate_PurchChallan(ByVal Sender As Object, Optional ByVal RowIndex As Integer = 0) As Boolean
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet = Nothing
        Try
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Function
            Select Case Sender.Name
                Case Dgl1.Name
                    If Dgl1.AgSelectedValue(Col1ChallanNo, RowIndex) <> "" Then
                        DrTemp = Dgl1.AgHelpDataSet(Col1ChallanNo).Tables(0).Select("Code = '" & Dgl1.AgSelectedValue(Col1ChallanNo, RowIndex) & "' ")
                        If DrTemp.Length > 0 Then
                            If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("MoveToLog")), "") Then
                                MsgBox("Currently Purchase Challan """ & Dgl1.Item(Col1ChallanNo, RowIndex).Value & """ Is In Log For Modification." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                                Dgl1.AgSelectedValue(Col1ChallanNo, RowIndex) = ""
                                Exit Function
                            End If

                            If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("Status")), AgTemplate.ClsMain.EntryStatus.Active) Then
                                MsgBox("Currently Purchase Challan """ & Dgl1.Item(Col1ChallanNo, RowIndex).Value & """ Is Not In Active State." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                                Dgl1.AgSelectedValue(Col1ChallanNo, RowIndex) = ""
                                Exit Function
                            End If
                        End If
                    End If
                    Validate_PurchChallan = True

                Case TxtChallanNo.Name
                    If TxtChallanNo.AgSelectedValue <> "" Then
                        DrTemp = TxtChallanNo.AgHelpDataSet().Tables(0).Select("Code = '" & TxtChallanNo.AgSelectedValue & "' ")
                        If DrTemp.Length > 0 Then
                            If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("MoveToLog")), "") Then
                                MsgBox("Currently Purchase Challan """ & TxtChallanNo.Text & """ Is In Log For Modification." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                                TxtChallanNo.AgSelectedValue = "" : Exit Function
                            End If

                            If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("Status")), AgTemplate.ClsMain.EntryStatus.Active) Then
                                MsgBox("Currently Purchase Challan """ & TxtChallanNo.Text & """ Is Not In Active State." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                                TxtChallanNo.AgSelectedValue = "" : Exit Function
                            End If
                        End If
                    End If
                    Validate_PurchChallan = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
End Class
