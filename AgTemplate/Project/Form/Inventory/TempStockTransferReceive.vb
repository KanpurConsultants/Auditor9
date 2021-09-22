Imports System.Data.SQLite
Public Class TempStockTransferReceive
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)

    Protected WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Process As String = "Process"
    Protected Const Col1Status As String = "Status"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1DocQty As String = "DocQty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "MeasureUnit"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Rate As String = "Rate"
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected Const Col1Amount As String = "Amount"

    Public Class HelpDataSet
        Public Shared Item As DataSet = Nothing
        Public Shared Godown As DataSet = Nothing
        Public Shared Process As DataSet = Nothing
        Public Shared Status As DataSet = Nothing
        Public Shared AgStructure As DataSet = Nothing
        Public Shared IssueNo As DataSet = Nothing
    End Class

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "Stock,StockProcess,Structure_TransFooter"
        LogLineTableCsv = "Stock_LOG,StockProcess_LOG,Structure_TransFooter_Log"
        AgL.GridDesign(Dgl1)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT H.UID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " H.V_Date AS [Entry Date], H.V_No AS [Entry No], " & _
        '                    " G.Description as FromGodown, G1.Description as ToGodown " & _
        '                    " FROM StockHead_Log H " & _
        '                    " LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN Godown G ON H.FromGodown = G.Code " & _
        '                    " LEFT JOIN Godown G1 ON H.ToGodown = G1.Code " & _
        '                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Adjustment Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Entry No], " &
                " H.ManualRefNo, H.FromProcess AS [FROM Process], H.ToProcess AS [TO Process], H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure],  " &
                " H.Amount, H.Addition, H.Deduction, H.NetAmount, H.Remarks,  H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type],  " &
                " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log],  " &
                " H.MoveToLogDate AS [Move To Log Date], H.Status, H.ReferenceDocID AS [Reference No], H.Structure, H.OrderBy AS [ORDER By], " &
                " D.Div_Name AS Division,SM.Name AS [Site Name],GF.Description AS [FROM Godown], GT.Description AS [To Godown] " &
                " FROM  StockHead_Log H  " &
                " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " &
                " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " &
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                " LEFT JOIN Godown GF ON GF.Code = H.FromGodown  " &
                " LEFT JOIN Godown GT ON GT.Code = H.ToGodown  " &
                " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT H.DocID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " H.V_Date AS [Entry Date], H.V_No AS [Entry No], " & _
        '                    " G.Description as FromGodown, G1.Description as ToGodown " & _
        '                    " FROM StockHead H " & _
        '                    " LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN Godown G ON H.FromGodown = G.Code " & _
        '                    " LEFT JOIN Godown G1 ON H.ToGodown = G1.Code " & _
        '                    " Where IfNull(H.IsDeleted,0) = 0   " & mCondStr

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Adjustment Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Entry No], " &
                        " H.ManualRefNo, H.FromProcess AS [FROM Process], H.ToProcess AS [TO Process], H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure],  " &
                        " H.Amount, H.Addition, H.Deduction, H.NetAmount, H.Remarks,  H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type],  " &
                        " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log],  " &
                        " H.MoveToLogDate AS [Move To Log Date], H.Status, H.ReferenceDocID AS [Reference No], H.Structure, H.OrderBy AS [ORDER By], " &
                        " D.Div_Name AS Division,SM.Name AS [Site Name],GF.Description AS [FROM Godown], GT.Description AS [To Godown] " &
                        " FROM  StockHead H  " &
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " &
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " &
                        " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                        " LEFT JOIN Godown GF ON GF.Code = H.FromGodown  " &
                        " LEFT JOIN Godown GT ON GT.Code = H.ToGodown  " &
                        " Where IfNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " &
            " From StockHead H " &
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And EntryStatus='" & LogStatus.LogOpen & "' "

        mQry = " Select H.UID As SearchCode " &
            " From StockHead_Log H " &
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
            " Where 1=1  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtFromGodown = New AgControls.AgTextBox
        Me.LblFromGodown = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LblFromGodownReq = New System.Windows.Forms.Label
        Me.LblToGodownReq = New System.Windows.Forms.Label
        Me.TxtToGodown = New AgControls.AgTextBox
        Me.LblToGodown = New System.Windows.Forms.Label
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.LblIssueNoReq = New System.Windows.Forms.Label
        Me.TxtIssueNo = New AgControls.AgTextBox
        Me.LblIssueNo = New System.Windows.Forms.Label
        Me.BtnFill = New System.Windows.Forms.Button
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(746, 521)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 521)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 521)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 521)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 521)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 517)
        Me.GroupBox1.Size = New System.Drawing.Size(919, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 521)
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
        Me.LblV_No.Location = New System.Drawing.Point(408, 28)
        Me.LblV_No.Size = New System.Drawing.Size(78, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Transfer No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(515, 27)
        Me.TxtV_No.Size = New System.Drawing.Size(188, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(286, 33)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(190, 28)
        Me.LblV_Date.Size = New System.Drawing.Size(85, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Transfer Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(499, 13)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(302, 27)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(408, 9)
        Me.LblV_Type.Size = New System.Drawing.Size(86, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Transfer Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(515, 7)
        Me.TxtV_Type.Size = New System.Drawing.Size(188, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(286, 13)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(190, 8)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(302, 7)
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
        Me.LblPrefix.Location = New System.Drawing.Point(770, 38)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 18)
        Me.TabControl1.Size = New System.Drawing.Size(908, 158)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtToGodown)
        Me.TP1.Controls.Add(Me.LblToGodown)
        Me.TP1.Controls.Add(Me.BtnFill)
        Me.TP1.Controls.Add(Me.LblToGodownReq)
        Me.TP1.Controls.Add(Me.LblIssueNoReq)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.TxtIssueNo)
        Me.TP1.Controls.Add(Me.LblIssueNo)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.LblFromGodownReq)
        Me.TP1.Controls.Add(Me.TxtFromGodown)
        Me.TP1.Controls.Add(Me.LblFromGodown)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(900, 132)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFromGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFromGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFromGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIssueNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtIssueNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIssueNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblToGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFill, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblToGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtToGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(901, 41)
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
        'TxtFromGodown
        '
        Me.TxtFromGodown.AgMandatory = True
        Me.TxtFromGodown.AgMasterHelp = False
        Me.TxtFromGodown.AgNumberLeftPlaces = 8
        Me.TxtFromGodown.AgNumberNegetiveAllow = False
        Me.TxtFromGodown.AgNumberRightPlaces = 2
        Me.TxtFromGodown.AgPickFromLastValue = False
        Me.TxtFromGodown.AgRowFilter = ""
        Me.TxtFromGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromGodown.AgSelectedValue = Nothing
        Me.TxtFromGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFromGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromGodown.Location = New System.Drawing.Point(302, 67)
        Me.TxtFromGodown.MaxLength = 20
        Me.TxtFromGodown.Name = "TxtFromGodown"
        Me.TxtFromGodown.Size = New System.Drawing.Size(401, 18)
        Me.TxtFromGodown.TabIndex = 5
        '
        'LblFromGodown
        '
        Me.LblFromGodown.AutoSize = True
        Me.LblFromGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblFromGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFromGodown.Location = New System.Drawing.Point(190, 69)
        Me.LblFromGodown.Name = "LblFromGodown"
        Me.LblFromGodown.Size = New System.Drawing.Size(89, 16)
        Me.LblFromGodown.TabIndex = 706
        Me.LblFromGodown.Text = "From Godown"
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(19, 371)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(853, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(424, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(116, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(31, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(19, 188)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(853, 183)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(16, 397)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(19, 415)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(377, 100)
        Me.TxtRemarks.TabIndex = 6
        '
        'LblFromGodownReq
        '
        Me.LblFromGodownReq.AutoSize = True
        Me.LblFromGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFromGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFromGodownReq.Location = New System.Drawing.Point(284, 74)
        Me.LblFromGodownReq.Name = "LblFromGodownReq"
        Me.LblFromGodownReq.Size = New System.Drawing.Size(10, 7)
        Me.LblFromGodownReq.TabIndex = 724
        Me.LblFromGodownReq.Text = "Ä"
        '
        'LblToGodownReq
        '
        Me.LblToGodownReq.AutoSize = True
        Me.LblToGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblToGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblToGodownReq.Location = New System.Drawing.Point(284, 55)
        Me.LblToGodownReq.Name = "LblToGodownReq"
        Me.LblToGodownReq.Size = New System.Drawing.Size(10, 7)
        Me.LblToGodownReq.TabIndex = 727
        Me.LblToGodownReq.Text = "Ä"
        '
        'TxtToGodown
        '
        Me.TxtToGodown.AgMandatory = True
        Me.TxtToGodown.AgMasterHelp = False
        Me.TxtToGodown.AgNumberLeftPlaces = 8
        Me.TxtToGodown.AgNumberNegetiveAllow = False
        Me.TxtToGodown.AgNumberRightPlaces = 2
        Me.TxtToGodown.AgPickFromLastValue = False
        Me.TxtToGodown.AgRowFilter = ""
        Me.TxtToGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToGodown.AgSelectedValue = Nothing
        Me.TxtToGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToGodown.Location = New System.Drawing.Point(302, 47)
        Me.TxtToGodown.MaxLength = 20
        Me.TxtToGodown.Name = "TxtToGodown"
        Me.TxtToGodown.Size = New System.Drawing.Size(401, 18)
        Me.TxtToGodown.TabIndex = 4
        '
        'LblToGodown
        '
        Me.LblToGodown.AutoSize = True
        Me.LblToGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblToGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToGodown.Location = New System.Drawing.Point(190, 49)
        Me.LblToGodown.Name = "LblToGodown"
        Me.LblToGodown.Size = New System.Drawing.Size(73, 16)
        Me.LblToGodown.TabIndex = 726
        Me.LblToGodown.Text = "To Godown"
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PnlCalcGrid.Location = New System.Drawing.Point(489, 399)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(383, 116)
        Me.PnlCalcGrid.TabIndex = 724
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
        Me.TxtStructure.Location = New System.Drawing.Point(796, 67)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(43, 18)
        Me.TxtStructure.TabIndex = 728
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(729, 67)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 729
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'LblIssueNoReq
        '
        Me.LblIssueNoReq.AutoSize = True
        Me.LblIssueNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblIssueNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblIssueNoReq.Location = New System.Drawing.Point(284, 95)
        Me.LblIssueNoReq.Name = "LblIssueNoReq"
        Me.LblIssueNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblIssueNoReq.TabIndex = 735
        Me.LblIssueNoReq.Text = "Ä"
        '
        'TxtIssueNo
        '
        Me.TxtIssueNo.AgMandatory = True
        Me.TxtIssueNo.AgMasterHelp = False
        Me.TxtIssueNo.AgNumberLeftPlaces = 8
        Me.TxtIssueNo.AgNumberNegetiveAllow = False
        Me.TxtIssueNo.AgNumberRightPlaces = 2
        Me.TxtIssueNo.AgPickFromLastValue = False
        Me.TxtIssueNo.AgRowFilter = ""
        Me.TxtIssueNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIssueNo.AgSelectedValue = Nothing
        Me.TxtIssueNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIssueNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtIssueNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIssueNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIssueNo.Location = New System.Drawing.Point(302, 87)
        Me.TxtIssueNo.MaxLength = 20
        Me.TxtIssueNo.Name = "TxtIssueNo"
        Me.TxtIssueNo.Size = New System.Drawing.Size(401, 18)
        Me.TxtIssueNo.TabIndex = 6
        '
        'LblIssueNo
        '
        Me.LblIssueNo.AutoSize = True
        Me.LblIssueNo.BackColor = System.Drawing.Color.Transparent
        Me.LblIssueNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIssueNo.Location = New System.Drawing.Point(190, 89)
        Me.LblIssueNo.Name = "LblIssueNo"
        Me.LblIssueNo.Size = New System.Drawing.Size(59, 16)
        Me.LblIssueNo.TabIndex = 734
        Me.LblIssueNo.Text = "Issue No"
        '
        'BtnFill
        '
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Location = New System.Drawing.Point(732, 94)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(107, 23)
        Me.BtnFill.TabIndex = 736
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = True
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(302, 107)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(100, 18)
        Me.TxtManualRefNo.TabIndex = 737
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(190, 108)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(101, 16)
        Me.LblManualRefNo.TabIndex = 738
        Me.LblManualRefNo.Text = "Manual Ref. No."
        '
        'TempStockTransferReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(901, 562)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label30)
        Me.Name = "TempStockTransferReceive"
        Me.Text = "Template Sale Order"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.Label30, 0)
        Me.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
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
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtFromGodown As AgControls.AgTextBox
    Protected WithEvents LblFromGodown As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblFromGodownReq As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents LblToGodownReq As System.Windows.Forms.Label
    Protected WithEvents TxtToGodown As AgControls.AgTextBox
    Protected WithEvents LblToGodown As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents LblIssueNoReq As System.Windows.Forms.Label
    Protected WithEvents TxtIssueNo As AgControls.AgTextBox
    Protected WithEvents LblIssueNo As System.Windows.Forms.Label
    Protected WithEvents BtnFill As System.Windows.Forms.Button
#End Region

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Process, 100, 0, Col1Process, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Status, 70, 0, Col1Status, False, True, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 70, 20, Col1LotNo, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 90, 8, 4, False, Col1DocQty, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 80, 8, 4, False, Col1MeasurePerPcs, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, True, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 100, 50, Col1MeasureUnit, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 4, False, Col1Qty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 90, 8, 2, False, Col1Rate, False, False, False)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 8, 2, False, Col1Amount, False, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Anchor = AnchorStyles.None
        Panel1.Anchor = Dgl1.Anchor
        FrmProductionOrder_BaseFunction_FIniList()
        Dgl1.ColumnHeadersHeight = 35

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)
        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index

        Dgl1.AgSkipReadOnlyColumns = True

        Ini_List()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE StockHead_Log " &
                " SET " &
                " TotalQty = " & Val(LblTotalQty.Text) & ", " &
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " &
                " FromGodown = " & AgL.Chk_Text(TxtFromGodown.AgSelectedValue) & ", " &
                " ToGodown = " & AgL.Chk_Text(TxtToGodown.AgSelectedValue) & ", " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " ReferenceDocId = " & AgL.Chk_Text(TxtIssueNo.AgSelectedValue) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " Where UID = '" & mSearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        AgCalcGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)

        mQry = "Delete From Stock_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                mQry = "INSERT INTO Stock_Log (UID, DocId, Sr, V_Type, V_Prefix, " &
                        " V_Date, V_No, Div_Code, Site_Code, " &
                        " Item, Process, Godown, Qty_Rec, Unit, LotNo," &
                        " MeasurePerPcs, Measure_Rec, MeasureUnit, Status, ReferenceDocId, Doc_Qty, Rate, Amount, Cost) " &
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " &
                        " " & mSr & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " &
                        " " & AgL.Chk_Text(TxtV_Date.Text) & ", " & Val(TxtV_No.Text) & ", " &
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",  " &
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Process, I)) & ", " &
                        " " & AgL.Chk_Text(TxtToGodown.AgSelectedValue) & ", " &
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " &
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1MeasureUnit, I)) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Status, I).Value) & ", " & AgL.Chk_Text(TxtIssueNo.AgSelectedValue) & ", " &
                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & " " &
                        " ) "

                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                AgCalcGrid1.Save_TransLine(mInternalCode, mSr, I, Conn, Cmd, SearchCode)

                mQry = "INSERT INTO StockProcess_Log (UID, DocId, Sr, V_Type, V_Prefix, " &
                        " V_Date, V_No, Div_Code, Site_Code, " &
                        " Item, Process, Qty_Iss, Unit, " &
                        " MeasurePerPcs, Measure_Iss, " &
                        " MeasureUnit, Status, ReferenceDocId, Doc_Qty, Rate, Amount, Cost) " &
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " &
                        " " & mSr & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " &
                        " " & AgL.Chk_Text(TxtV_Date.Text) & ", " & Val(TxtV_No.Text) & ", " &
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",  " &
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Process, I)) & ", " &
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1MeasureUnit, I)) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Status, I).Value) & ", " & AgL.Chk_Text(TxtIssueNo.AgSelectedValue) & ",  " &
                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & " " &
                        " ) "

                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select H.* " &
                " From StockHead H " &
                " Where H.DocID='" & SearchCode & "'"
        Else
            mQry = "Select H.* " &
                " From StockHead_Log H " &
                " Where H.UID='" & SearchCode & "'"

        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.AgSelectedValue = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                IniGrid()
                TxtFromGodown.AgSelectedValue = AgL.XNull(.Rows(0)("FromGodown"))
                TxtToGodown.AgSelectedValue = AgL.XNull(.Rows(0)("ToGodown"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                TxtIssueNo.AgSelectedValue = AgL.XNull(.Rows(0)("ReferenceDocID"))

                LblIssueNo.Tag = TxtFromGodown.AgSelectedValue
                Dgl1.Tag = TxtIssueNo.AgSelectedValue

                AgCalcGrid1.MoveRec_TransFooter(SearchCode)

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from Stock where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from Stock_Log where UID = '" & SearchCode & "' Order By Sr"
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
                            Dgl1.AgSelectedValue(Col1Process, I) = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.Item(Col1Status, I).Value = AgL.XNull(.Rows(I)("Status"))
                            Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty_Rec"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("Measure_Rec"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1DocQty, I).Value = AgL.VNull(.Rows(I)("Doc_Qty"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))

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
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtFromGodown.Validating, TxtToGodown.Validating, TxtIssueNo.Validating, TxtManualRefNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    IniGrid()

                Case TxtIssueNo.Name
                    e.Cancel = Not Validate_IssueNo()
                    If TxtIssueNo.Text <> "" Then
                        DrTemp = TxtIssueNo.AgHelpDataSet.Tables(0).Select(" Code = '" & TxtIssueNo.AgSelectedValue & "' ")
                        If DrTemp.Length > 0 Then
                            LblIssueNo.Tag = AgL.XNull(DrTemp(0)("FromGodown"))
                        Else
                            LblIssueNo.Tag = ""
                        End If
                    End If

                Case TxtToGodown.Name
                    TxtManualRefNo.Text = FGetManualRefNo("ManualRefNo", "StockHead_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtToGodown.AgSelectedValue, ClsMain.ManualRefType.Max)

                Case TxtManualRefNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()
            End Select

            If Not AgL.StrCmp(TxtFromGodown.AgSelectedValue, LblIssueNo.Tag) Then
                TxtIssueNo.AgSelectedValue = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
        'TxtManualRefNo.Text = TxtV_No.Text
        TxtManualRefNo.Text = FGetManualRefNo("ManualRefNo", "StockHead_Log", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtToGodown.AgSelectedValue, ClsMain.ManualRefType.Max)

    End Sub

    Public Function FGetManualRefNo(ByVal FieldName As String, ByVal TableName As String, ByVal V_Type As String, ByVal V_Date As String, ByVal Div_Code As String, ByVal Site_Code As String, ByVal Godown_Code As String, Optional ByVal RefType As ClsMain.ManualRefType = ClsMain.ManualRefType.Max) As String
        Dim mQry$
        Dim mStartDate As String, mEndDate As String
        If CDate(V_Date) >= CDate("01/Apr/2012") And CDate(V_Date) <= CDate("31/Mar/2013") Then
            mStartDate = "01/Apr/2012"
            mEndDate = "31/Mar/2013"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2011") And CDate(V_Date) <= CDate("31/Mar/2012") Then
            mStartDate = "01/Apr/2011"
            mEndDate = "31/Mar/2012"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2010") And CDate(V_Date) <= CDate("31/Mar/2011") Then
            mStartDate = "01/Apr/2010"
            mEndDate = "31/Mar/2011"
        Else
            mStartDate = "01/Apr/2009"
            mEndDate = "31/Mar/2010"
        End If

        Select Case RefType
            Case Else
                mQry = "Select IfNull(Max(Convert(Numeric," & FieldName & ")),0)+1 From " & TableName & "  WHERE isnumeric(" & FieldName & ")>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "' and ToGodown = '" & Godown_Code & "' And V_Date Between '" & mStartDate & "' and  '" & mEndDate & "'  "
                FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
        End Select
    End Function

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        Dgl1.AgHelpDataSet(Col1Item, 7) = HelpDataSet.Item
        TxtFromGodown.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Godown
        TxtToGodown.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Godown
        Dgl1.AgHelpDataSet(Col1Process, 1) = HelpDataSet.Process
        Dgl1.AgHelpDataSet(Col1Status) = HelpDataSet.Status
        TxtStructure.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.AgStructure
        TxtIssueNo.AgHelpDataSet(7, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.IssueNo
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' "

            Case Col1Process
                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " Div_Code = '" & TxtDivision.AgSelectedValue & "' "

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
            Else
                If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                    DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(DrTemp(0)("Measure"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl1.Item(Col1Status, mRow).Value = "Standard"
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

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next
        AgCalcGrid1.Calculation()
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtFromGodown, "From Godown") Then passed = False : Exit Sub
        If AgL.RequiredField(TxtToGodown, "To Godown") Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, CStr(Dgl1.Columns(Col1Item).Index) + "," + CStr(Dgl1.Columns(Col1Process).Index) + "," + CStr(Dgl1.Columns(Col1Status).Index) + "," + CStr(Dgl1.Columns(Col1LotNo).Index)) = True Then passed = False : Exit Sub

        If Validate_IssueNo() = False Then passed = False : Exit Sub

        If AgL.RequiredField(TxtIssueNo, LblIssueNo.Text) Then passed = False : Exit Sub

        If AgL.StrCmp(TxtFromGodown.AgSelectedValue, TxtToGodown.AgSelectedValue) Then
            MsgBox("From And To Godown Can't Be Same", MsgBoxStyle.Information)
            TxtToGodown.Focus()
            passed = False : Exit Sub
        End If

        If Not AgL.StrCmp(TxtFromGodown.AgSelectedValue, LblIssueNo.Tag) Then
            MsgBox("Issue No " & TxtIssueNo.Text & " does not belong to " & TxtFromGodown.Text & "", MsgBoxStyle.Information)
            TxtFromGodown.Focus()
            passed = False : Exit Sub
        End If

        If Not AgL.StrCmp(TxtIssueNo.AgSelectedValue, Dgl1.Tag) Then
            MsgBox("Data In Grid does not belong to " & TxtIssueNo.Text & " ", MsgBoxStyle.Information)
            TxtIssueNo.Focus()
            passed = False : Exit Sub
        End If


        passed = FCheckDuplicateRefNo()

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1Qty, I).Value) > Val(.Item(Col1DocQty, I).Value) Then
                        MsgBox("Qty Is Greater Than Doc Qty At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM StockHead WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' AND ToGodown = '" & TxtToGodown.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM StockHead WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'  " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' AND ToGodown = '" & TxtToGodown.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function



    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFromGodown.Enter, TxtToGodown.Enter, TxtIssueNo.Enter
        Select Case sender.name
            Case TxtFromGodown.Name
                TxtFromGodown.AgRowFilter = " Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " &
                          " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                          " And IsDeleted = 0 "


            Case TxtToGodown.Name
                TxtToGodown.AgRowFilter = " Div_Code = '" & AgL.PubDivCode & "' " &
                                            " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " &
                                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                            " And IsDeleted = 0 "
            Case TxtIssueNo.Name
                TxtIssueNo.AgRowFilter = " IsDeleted = 0 And IssueDate <= '" & TxtV_Date.Text & "' And FromGodown = '" & TxtFromGodown.AgSelectedValue & "' And ReceiveDocId Is Null "

        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0
        Dgl1.Tag = "" : LblIssueNo.Tag = ""
    End Sub

    Protected Sub ProcFill(ByVal bIssueDocId As String)
        Dim I As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Try
            mQry = "SELECT S.Item, S.Process, S.Status, S.Qty_Rec, S.Unit, " &
                    " S.MeasurePerPcs,S.Measure_Rec, S.MeasureUnit, S.LotNo " &
                    " FROM StockProcess S " &
                    " WHERE DocId = '" & bIssueDocId & "'"
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.AgSelectedValue(Col1Process, I) = AgL.XNull(.Rows(I)("Process"))
                        Dgl1.Item(Col1Status, I).Value = AgL.XNull(.Rows(I)("Status"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty_Rec"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("Measure_Rec"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1DocQty, I).Value = AgL.VNull(.Rows(I)("Qty_Rec"))
                        Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                    Next I
                End If
            End With
            Calculation()
            Dgl1.Tag = TxtIssueNo.AgSelectedValue
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        ProcFill(TxtIssueNo.AgSelectedValue)
    End Sub

    Private Sub TempStockTransfer_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
            BtnFill.Enabled = False
        Else
            BtnFill.Enabled = True
        End If
    End Sub

    Private Function Validate_IssueNo() As Boolean
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet = Nothing
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Function
            If TxtIssueNo.Text <> "" Then
                DrTemp = TxtIssueNo.AgHelpDataSet.Tables(0).Select("Code = '" & TxtIssueNo.AgSelectedValue & "' ")
                If DrTemp.Length > 0 Then
                    If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("MoveToLog")), "") Then
                        MsgBox("Currently Issue No """ & TxtIssueNo.Text & """ Is In Log For Modification." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                        If AgL.StrCmp(Topctrl1.Mode, "Add") Then TxtIssueNo.Text = ""
                        Exit Function
                    End If

                    If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("Status")), AgTemplate.ClsMain.EntryStatus.Active) Then
                        MsgBox("Currently Issue No """ & TxtIssueNo.Text & """ Is Not In Active State." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                        If AgL.StrCmp(Topctrl1.Mode, "Add") Then TxtIssueNo.Text = ""
                        Exit Function
                    End If
                End If

                mQry = "SELECT Count(Sh.DocID)  " &
                        " FROM StockHead_Log Sh " &
                        " LEFT JOIN Voucher_Type Vt On Sh.V_Type = Vt.V_Type " &
                        " WHERE Sh.ReferenceDocId = '" & TxtIssueNo.AgSelectedValue & "' " &
                        " AND Sh.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "' " &
                        " AND " & IIf(AgL.StrCmp(Topctrl1.Mode, "Edit"), "Sh.DocId <> '" & mInternalCode & "'", "1=1") & " " &
                        " AND Vt.NCat = '" & EntryNCat & "'  "

                If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar() > 0 Then
                    MsgBox("A Receive Against """ & TxtIssueNo.Text & """ Already Exists In Log." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                    Exit Function
                End If
            End If
            Validate_IssueNo = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub TempStockTransferReceive_BaseFunction_CreateHelpDataSet() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = "SELECT I.Code, I.Description, I.Unit, I.ItemType, I.SalesTaxPostingGroup, Measure, MeasureUnit , IfNull(I.IsDeleted ,0) AS IsDeleted, IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status, I.Div_Code FROM Item I"
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

        HelpDataSet.Status = AgL.FillData(ClsMain.HelpQueries.StockStatus, AgL.GCn)

        mQry = "SELECT Code, Description  FROM Structure ORDER BY Description "
        HelpDataSet.AgStructure = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT S.DocID AS Code, S.ManualRefNo, Vt.Description + '/' + convert(NVARCHAR,S.V_No)  AS IssueNo, " &
                " IfNull(S.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status, " &
                " S.MoveToLog, IfNull(S.IsDeleted ,0) AS IsDeleted, " &
                " S.V_Date AS IssueDate, S.Div_Code, S.FromGodown, V1.DocId AS ReceiveDocId " &
                " FROM StockHead S " &
                " LEFT JOIN ( " &
                "   Select St.DocId, St.ReferenceDocID " &
                " 	FROM StockHead St " &
                " 	LEFT JOIN Voucher_Type V ON St.V_Type = V.V_Type " &
                " 	WHERE IfNull(St.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                " 	AND V.NCat = '" & EntryNCat & "' " &
                " ) AS V1 ON S.DocId = V1.ReferenceDocId " &
                " LEFT JOIN Voucher_Type Vt ON S.V_Type = Vt.V_Type " &
                " WHERE Vt.NCat = '" & ClsMain.Temp_NCat.StockTransferIssue & "' "
        HelpDataSet.IssueNo = AgL.FillData(mQry, AgL.GCn)
    End Sub
End Class
