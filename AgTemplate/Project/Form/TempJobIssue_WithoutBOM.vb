Public Class TempJobIssue_WithoutBOM
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1JobOrder As String = "Job Order No"
    Protected Const Col1ReceiveQty As String = "Receive Qty"
    Protected Const Col1ReceiveMeasure As String = "Receive Measure"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub TempJobIssue_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim I As Integer

        '------------------------------------------------------------------------
        'Updating Job Order Qty In Material Plan Detail
        '-------------------------------------------------------------------------
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mQry = "UPDATE JobOrder " & _
                                            " SET " & _
                                            " LastIssueDate = " & AgL.ConvertDate(TxtV_Date) & " " & _
                                            " Where DocId = '" & .AgSelectedValue(Col1JobOrder, I) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobIssRec"
        LogTableName = "JobIssRec_Log"
        MainLineTableCsv = "JobIssueDetail,Stock,StockProcess"
        LogLineTableCsv = "JobIssueDetail_Log,Stock_LOG,StockProcess_LOG"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT J.UID as SearchCode, Vt.Description AS [Entry Type], " & _
                            " J.V_Date AS [Entry Date], J.V_No AS [Entry No], " & _
                            " J.ManualRefNo, J.DueDate " & _
                            " FROM JobIssRec_Log J " & _
                            " LEFT JOIN voucher_type Vt ON J.V_Type = Vt.V_Type " & _
                            " Where J.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT J.DocID as SearchCode, Vt.Description AS [Entry Type], " & _
                            " J.V_Date AS [Entry Date], J.V_No AS [Entry No], " & _
                            " J.ManualRefNo, J.DueDate " & _
                            " FROM JobIssRec J " & _
                            " LEFT JOIN voucher_type Vt ON J.V_Type = Vt.V_Type " & _
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

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalIssMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalIssQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LblGodownReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
        Me.TxtDueDate = New AgControls.AgTextBox
        Me.LblDueDate = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(746, 489)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 489)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 489)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 489)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 489)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 485)
        Me.GroupBox1.Size = New System.Drawing.Size(921, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 489)
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
        Me.LblV_No.Location = New System.Drawing.Point(378, 42)
        Me.LblV_No.Size = New System.Drawing.Size(87, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Job Issue No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(503, 41)
        Me.TxtV_No.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(254, 47)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(148, 42)
        Me.LblV_Date.Size = New System.Drawing.Size(94, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Job Issue Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(487, 27)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(272, 41)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(378, 23)
        Me.LblV_Type.Size = New System.Drawing.Size(95, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Job Issue Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(503, 21)
        Me.TxtV_Type.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(254, 27)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(148, 22)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(272, 21)
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
        Me.LblPrefix.Location = New System.Drawing.Point(647, 63)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(17, 44)
        Me.TabControl1.Size = New System.Drawing.Size(869, 207)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtDueDate)
        Me.TP1.Controls.Add(Me.LblDueDate)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.LblProcess)
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
        Me.TP1.Size = New System.Drawing.Size(861, 181)
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
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDueDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDueDate, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(903, 41)
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
        Me.TxtGodown.Location = New System.Drawing.Point(272, 81)
        Me.TxtGodown.MaxLength = 20
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(356, 18)
        Me.TxtGodown.TabIndex = 6
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(148, 82)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 706
        Me.LblGodown.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalIssMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalIssQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(17, 455)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(869, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalIssMeasure
        '
        Me.LblTotalIssMeasure.AutoSize = True
        Me.LblTotalIssMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalIssMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalIssMeasure.Location = New System.Drawing.Point(424, 3)
        Me.LblTotalIssMeasure.Name = "LblTotalIssMeasure"
        Me.LblTotalIssMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalIssMeasure.TabIndex = 666
        Me.LblTotalIssMeasure.Text = "."
        Me.LblTotalIssMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Maroon
        Me.Label33.Location = New System.Drawing.Point(313, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(106, 16)
        Me.Label33.TabIndex = 665
        Me.Label33.Text = "Total Measure :"
        '
        'LblTotalIssQty
        '
        Me.LblTotalIssQty.AutoSize = True
        Me.LblTotalIssQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalIssQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalIssQty.Location = New System.Drawing.Point(116, 3)
        Me.LblTotalIssQty.Name = "LblTotalIssQty"
        Me.LblTotalIssQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalIssQty.TabIndex = 660
        Me.LblTotalIssQty.Text = "."
        Me.LblTotalIssQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(31, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(17, 268)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(869, 186)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(148, 122)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(272, 121)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(356, 18)
        Me.TxtRemarks.TabIndex = 8
        '
        'LblGodownReq
        '
        Me.LblGodownReq.AutoSize = True
        Me.LblGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblGodownReq.Location = New System.Drawing.Point(254, 88)
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(503, 61)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(125, 18)
        Me.TxtManualRefNo.TabIndex = 5
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(378, 61)
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
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(254, 109)
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
        Me.TxtJobWorker.Location = New System.Drawing.Point(272, 101)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(356, 18)
        Me.TxtJobWorker.TabIndex = 7
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(148, 102)
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
        Me.TxtProcess.Location = New System.Drawing.Point(724, 122)
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
        Me.LblProcess.Location = New System.Drawing.Point(662, 123)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 737
        Me.LblProcess.Text = "Process"
        Me.LblProcess.Visible = False
        '
        'TxtDueDate
        '
        Me.TxtDueDate.AgMandatory = True
        Me.TxtDueDate.AgMasterHelp = False
        Me.TxtDueDate.AgNumberLeftPlaces = 8
        Me.TxtDueDate.AgNumberNegetiveAllow = False
        Me.TxtDueDate.AgNumberRightPlaces = 2
        Me.TxtDueDate.AgPickFromLastValue = False
        Me.TxtDueDate.AgRowFilter = ""
        Me.TxtDueDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDueDate.AgSelectedValue = Nothing
        Me.TxtDueDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDueDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDueDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDueDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDueDate.Location = New System.Drawing.Point(272, 61)
        Me.TxtDueDate.MaxLength = 20
        Me.TxtDueDate.Name = "TxtDueDate"
        Me.TxtDueDate.Size = New System.Drawing.Size(100, 18)
        Me.TxtDueDate.TabIndex = 4
        '
        'LblDueDate
        '
        Me.LblDueDate.AutoSize = True
        Me.LblDueDate.BackColor = System.Drawing.Color.Transparent
        Me.LblDueDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDueDate.Location = New System.Drawing.Point(148, 62)
        Me.LblDueDate.Name = "LblDueDate"
        Me.LblDueDate.Size = New System.Drawing.Size(66, 16)
        Me.LblDueDate.TabIndex = 739
        Me.LblDueDate.Text = "Due  Date"
        '
        'TempJobIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(903, 530)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "TempJobIssue"
        Me.Text = "Template Sale Order"
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
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
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalIssMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblGodownReq As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents TxtDueDate As AgControls.AgTextBox
    Protected WithEvents LblDueDate As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents LblProcess As System.Windows.Forms.Label
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
#End Region

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 3, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 3, False, Col1MeasurePerPcs, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 3, False, Col1TotalMeasure, True, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, True, True)
            .AddAgTextColumn(Dgl1, Col1JobOrder, 100, 0, Col1JobOrder, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReceiveQty, 70, 8, 3, False, Col1ReceiveQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1ReceiveMeasure, 70, 8, 3, False, Col1ReceiveMeasure, False, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Anchor = AnchorStyles.None
        Panel1.Anchor = Dgl1.Anchor
        FrmProductionOrder_BaseFunction_FIniList()
        Dgl1.ColumnHeadersHeight = 35

        Ini_List()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim Stock As AgTemplate.ClsMain.StructStock = Nothing, StockProcess As AgTemplate.ClsMain.StructStock = Nothing

        mQry = "UPDATE JobIssRec_Log " & _
                " SET " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " & _
                " JobWorker = " & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " & _
                " Godown = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & _
                " DueDate = " & AgL.ConvertDate(TxtDueDate.Text) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                " IssQty = " & Val(LblTotalIssQty.Text) & ", " & _
                " IssMeasure = " & Val(LblTotalIssMeasure.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobIssueDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Stock_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                mQry = "INSERT INTO JobIssueDetail_Log(UID, DocId, Sr, Item, Qty, Unit, MeasurePerPcs, TotalMeasure, " & _
                        " MeasureUnit, JobOrder, ReceiveQty, ReceiveMeasure) " & _
                        " Values (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & "," & _
                        " " & mSr & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1JobOrder, I)) & ", " & _
                        " " & Val(Dgl1.Item(Col1ReceiveQty, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1ReceiveMeasure, I).Value) & "" & _
                        " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                With Stock
                    .UID = mSearchCode
                    .DocID = mInternalCode
                    .Sr = mSr
                    .V_Type = TxtV_Type.AgSelectedValue
                    .V_Prefix = LblPrefix.Text
                    .V_Date = TxtV_Date.Text
                    .V_No = TxtV_No.Text
                    .Div_Code = TxtDivision.AgSelectedValue
                    .Site_Code = TxtSite_Code.AgSelectedValue
                    .SubCode = TxtJobWorker.AgSelectedValue
                    .Item = Dgl1.AgSelectedValue(Col1Item, I)
                    .Godown = TxtGodown.AgSelectedValue
                    .Qty_Iss = Dgl1.Item(Col1Qty, I).Value
                    .Unit = Dgl1.Item(Col1Unit, I).Value
                    .MeasurePerPcs = Dgl1.Item(Col1MeasurePerPcs, I).Value
                    .Measure_Iss = Dgl1.Item(Col1TotalMeasure, I).Value
                    .MeasureUnit = Dgl1.Item(Col1MeasureUnit, I).Value
                    .Status = AgTemplate.ClsMain.StockStatus.Standard
                End With

                Call AgTemplate.ClsMain.ProcStockPost("Stock_Log", Stock, Conn, Cmd)

                With StockProcess
                    .UID = mSearchCode
                    .DocID = mInternalCode
                    .Sr = mSr
                    .V_Type = TxtV_Type.AgSelectedValue
                    .V_Prefix = LblPrefix.Text
                    .V_Date = TxtV_Date.Text
                    .V_No = TxtV_No.Text
                    .Div_Code = TxtDivision.AgSelectedValue
                    .Site_Code = TxtSite_Code.AgSelectedValue
                    .SubCode = TxtJobWorker.AgSelectedValue
                    .Item = Dgl1.AgSelectedValue(Col1Item, I)
                    .Godown = TxtGodown.AgSelectedValue
                    .Qty_Rec = Dgl1.Item(Col1Qty, I).Value
                    .Unit = Dgl1.Item(Col1Unit, I).Value
                    .MeasurePerPcs = Dgl1.Item(Col1MeasurePerPcs, I).Value
                    .Measure_Rec = Dgl1.Item(Col1TotalMeasure, I).Value
                    .MeasureUnit = Dgl1.Item(Col1MeasureUnit, I).Value
                    .Status = AgTemplate.ClsMain.StockStatus.Standard
                    .Process = TxtProcess.AgSelectedValue
                End With

                Call AgTemplate.ClsMain.ProcStockPost("StockProcess_Log", StockProcess, Conn, Cmd)

                RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
            End If
        Next
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
                IniGrid()
                TxtGodown.AgSelectedValue = AgL.XNull(.Rows(0)("Godown"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtProcess.AgSelectedValue = AgL.XNull(.Rows(0)("Process"))
                TxtJobWorker.AgSelectedValue = AgL.XNull(.Rows(0)("JobWorker"))
                TxtDueDate.Text = AgL.XNull(.Rows(0)("DueDate"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalIssQty.Text = AgL.VNull(.Rows(0)("IssQty"))
                LblTotalIssMeasure.Text = AgL.VNull(.Rows(0)("IssMeasure"))

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from JobIssueDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from JobIssueDetail_Log where UID = '" & SearchCode & "' Order By Sr"
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
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.000")
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.000")
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.AgSelectedValue(Col1JobOrder, I) = AgL.XNull(.Rows(I)("JobOrder"))
                            Dgl1.Item(Col1ReceiveQty, I).Value = Format(AgL.VNull(.Rows(I)("ReceiveQty")), "0.000")
                            Dgl1.Item(Col1ReceiveMeasure, I).Value = Format(AgL.VNull(.Rows(I)("ReceiveMeasure")), "0.000")
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
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        'mQry = "SELECT J.Item As Code, I.Description As Item, J.Qty, J.IssuedQty, " & _
        '        " I.ItemType, I.SalesTaxPostingGroup, J.Unit, J.MeasurePerPcs As Measure, J.MeasureUnit, " & _
        '        " IsNull(Jo.IsDeleted ,0) AS IsDeleted, " & _
        '        " IsNull(Jo.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status, Jo.Div_Code, Vt.NCat " & _
        '        " FROM JobOrderBOM J " & _
        '        " LEFT JOIN Item I On J.Item = I.Code " & _
        '        " LEFT JOIN JobOrder Jo On J.DocId = Jo.DocId " & _
        '        " LEFT JOIN Voucher_Type Vt On Jo.V_Type = Vt.V_Type "
        mQry = "SELECT I.Code As Code, I.Description As Item, I.ItemType, I.SalesTaxPostingGroup, I.Unit, I.Measure,   IsNull(I.IsDeleted ,0) AS IsDeleted,  IsNull(I.Status,'Active') AS Status FROM  Item I "
        Dgl1.AgHelpDataSet(Col1Item, 6) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT G.Code, G.Description, Sm.ManualCode As Site, G.Site_Code, G.Div_Code, IsNull(G.IsDeleted,0) as IsDeleted, " & _
                " IsNull(G.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status " & _
                " FROM Godown G " & _
                " LEFT JOIN SiteMast Sm On G.Site_Code = Sm.Code " & _
                " Order By G.Description"
        TxtGodown.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select P.NCat As Code, Vc.NCatDescription As Process, P.Div_Code " & _
                " From Process P " & _
                " LEFT JOIN VoucherCat Vc On P.NCat  = Vc.NCat " & _
                " Order By Vc.NCatDescription "
        TxtProcess.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT S.SubCode AS Code, S.Name  AS JobWorker " & _
              " FROM JobWorker J " & _
              " LEFT JOIN SubGroup S ON J.SubCode = S.SubCode "
        TxtJobWorker.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        'mQry = "SELECT J.DocId, Vt.Description + '/' + Convert(NVARCHAR,J.V_No) " & _
        '        " FROM JobOrder J " & _
        '        " LEFT JOIN Voucher_Type Vt ON J.V_Type = Vt.V_Type "
        'Dgl1.AgHelpDataSet(Col1JobOrder) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' And BalanceQty > 0 "
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
                Dgl1.Item(Col1Unit, mRow).Value = ""
                ' Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                'Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                'Dgl1.Item(Col1JobOrder, mRow).Value = ""
            Else
                If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                    DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    'Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(DrTemp(0)("Measure"))
                    'Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    'Dgl1.AgSelectedValue(Col1JobOrder, mRow) = AgL.XNull(DrTemp(0)("JobOrderNo"))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalIssQty.Text = 0
        LblTotalIssMeasure.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")
                LblTotalIssQty.Text = Val(LblTotalIssQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalIssMeasure.Text = Val(LblTotalIssMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next
        LblTotalIssQty.Text = Format(Val(LblTotalIssQty.Text), "0.000")
        LblTotalIssMeasure.Text = Format(Val(LblTotalIssMeasure.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtGodown, LblGodown.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub

        If TxtDueDate.Text <> "" And TxtV_Date.Text <> "" Then
            If CDate(TxtDueDate.Text) < CDate(TxtV_Date.Text) Then
                MsgBox("Due date Can't be Less Than Job Order Date", MsgBoxStyle.Information)
                TxtDueDate.Focus()
                passed = False : Exit Sub
            End If
        End If

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If AgTemplate.ClsMain.FunCheckStock(.AgSelectedValue(Col1Item, I), mInternalCode, TxtGodown.AgSelectedValue, , AgTemplate.ClsMain.StockStatus.Standard) < Val(.Item(Col1Qty, I).Value) Then
                        MsgBox("Qty of " & .Item(Col1Item, I).Value & " In " & TxtGodown.Text & " is less than " & Dgl1.Item(Col1Qty, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtGodown.Enter
        Select Case sender.name
            Case TxtGodown.Name
                TxtGodown.AgRowFilter = " Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsDeleted = 0 "
        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalIssMeasure.Text = 0 : LblTotalIssQty.Text = 0
    End Sub



End Class
