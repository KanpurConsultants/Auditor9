Imports System.Data.SQLite
Public Class TempStockAdjustment
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)

    Public WithEvents Dgl1 As AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item As String = "Item"
    Protected Const Col1FromProcess As String = "From Process"
    Protected Const Col1ToProcess As String = "To Process"
    Protected Const Col1Status As String = "Status"
    Protected Const Col1CurrentStock As String = "Curr. Stock"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "MeasureUnit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"

    Dim mGodownField As String = "ToGodown"
    Dim mQtyField As String = "Qty_Rec"
    Dim mMeasureField As String = "Measure_Rec"
    Protected WithEvents TxtToProcess As AgControls.AgTextBox
    Protected WithEvents LblToProcess As System.Windows.Forms.Label
    Protected WithEvents TxtFromProcess As AgControls.AgTextBox
    Protected WithEvents LblFromProcess As System.Windows.Forms.Label

    Dim mStockNature As StockEffect = StockEffect.StkIn

    Public Class HelpDataSet
        Public Shared Item As DataSet = Nothing
        Public Shared Godown As DataSet = Nothing
        Public Shared AcName As DataSet = Nothing
        Public Shared Process As DataSet = Nothing
        Public Shared Status As DataSet = Nothing
    End Class

    Enum StockEffect
        None = 0
        StkIn = 1
        StkOut = 2
        StkInOut = 3
    End Enum

    Public Property StockNature() As StockEffect
        Get
            Return mStockNature
        End Get
        Set(ByVal value As StockEffect)
            mStockNature = value
            If value = StockEffect.StkIn Then
                mGodownField = "ToGodown"
                mQtyField = "Qty_Rec"
                mMeasureField = "Measure_Rec"
            Else
                mGodownField = "FromGodown"
                mQtyField = "Qty_Iss"
                mMeasureField = "Measure_Iss"
            End If
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT H.UID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " H.V_Date AS [Entry Date], H.V_No AS [Entry No], G.Description as Godown " & _
        '                    " FROM StockHead_Log H " & _
        '                    " LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN Godown G ON H." & mGodownField & " = G.Code " & _
        '                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Date AS Date, H.V_No AS [Entry No], " &
                " H.FromProcess AS [FROM Process], H.ToProcess AS [TO Process], H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure],  " &
                " H.Amount, H.Addition, H.Deduction, H.NetAmount, H.Remarks,  H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], " &
                " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], " &
                " H.ReferenceDocID AS [Reference No], H.OrderBy AS [ORDER By], " &
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
        '                    " H.V_Date AS [Entry Date], H.V_No AS [Entry No], G.Description as Godown " & _
        '                    " FROM StockHead H " & _
        '                    " LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN Godown G ON H." & mGodownField & " = G.Code " & _
        '                    " Where IfNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Date AS Date, H.V_No AS [Entry No], " &
                        " H.FromProcess AS [FROM Process], H.ToProcess AS [TO Process], H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure],  " &
                        " H.Amount, H.Addition, H.Deduction, H.NetAmount, H.Remarks,  H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], " &
                        " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], " &
                        " H.ReferenceDocID AS [Reference No], H.OrderBy AS [ORDER By], " &
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

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "Stock"
        LogLineTableCsv = "Stock_LOG"
        AgL.GridDesign(Dgl1)
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
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblSaleOrderNo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtAcName = New AgControls.AgTextBox
        Me.LblAcName = New System.Windows.Forms.Label
        Me.TxtToProcess = New AgControls.AgTextBox
        Me.LblToProcess = New System.Windows.Forms.Label
        Me.TxtFromProcess = New AgControls.AgTextBox
        Me.LblFromProcess = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(746, 417)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 417)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 417)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 417)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 417)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 413)
        Me.GroupBox1.Size = New System.Drawing.Size(919, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 417)
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
        Me.LblV_No.Location = New System.Drawing.Point(422, 40)
        Me.LblV_No.Size = New System.Drawing.Size(98, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Adjustment No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(544, 39)
        Me.TxtV_No.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(300, 45)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(204, 40)
        Me.LblV_Date.Size = New System.Drawing.Size(105, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Adjustment Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(528, 25)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(316, 39)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(422, 21)
        Me.LblV_Type.Size = New System.Drawing.Size(99, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Ajustment Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(544, 19)
        Me.TxtV_Type.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(300, 25)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(204, 20)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(316, 19)
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
        Me.LblPrefix.Location = New System.Drawing.Point(710, 39)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 19)
        Me.TabControl1.Size = New System.Drawing.Size(905, 158)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtToProcess)
        Me.TP1.Controls.Add(Me.LblToProcess)
        Me.TP1.Controls.Add(Me.TxtFromProcess)
        Me.TP1.Controls.Add(Me.LblFromProcess)
        Me.TP1.Controls.Add(Me.TxtAcName)
        Me.TP1.Controls.Add(Me.LblAcName)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblSaleOrderNo)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(897, 132)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSaleOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblAcName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtAcName, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFromProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFromProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblToProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtToProcess, 0)
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
        Me.TxtGodown.Location = New System.Drawing.Point(316, 79)
        Me.TxtGodown.MaxLength = 20
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(377, 18)
        Me.TxtGodown.TabIndex = 5
        '
        'LblSaleOrderNo
        '
        Me.LblSaleOrderNo.AutoSize = True
        Me.LblSaleOrderNo.BackColor = System.Drawing.Color.Transparent
        Me.LblSaleOrderNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSaleOrderNo.Location = New System.Drawing.Point(204, 81)
        Me.LblSaleOrderNo.Name = "LblSaleOrderNo"
        Me.LblSaleOrderNo.Size = New System.Drawing.Size(55, 16)
        Me.LblSaleOrderNo.TabIndex = 706
        Me.LblSaleOrderNo.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(25, 386)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(853, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(732, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 668
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Maroon
        Me.Label4.Location = New System.Drawing.Point(621, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 16)
        Me.Label4.TabIndex = 667
        Me.Label4.Text = "Total Amount :"
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
        Me.Pnl1.Location = New System.Drawing.Point(25, 209)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(853, 177)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(204, 101)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(316, 99)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(377, 18)
        Me.TxtRemarks.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(298, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 724
        Me.Label1.Text = "Ä"
        '
        'TxtAcName
        '
        Me.TxtAcName.AgMandatory = False
        Me.TxtAcName.AgMasterHelp = False
        Me.TxtAcName.AgNumberLeftPlaces = 0
        Me.TxtAcName.AgNumberNegetiveAllow = False
        Me.TxtAcName.AgNumberRightPlaces = 0
        Me.TxtAcName.AgPickFromLastValue = False
        Me.TxtAcName.AgRowFilter = ""
        Me.TxtAcName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcName.AgSelectedValue = Nothing
        Me.TxtAcName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcName.Location = New System.Drawing.Point(316, 59)
        Me.TxtAcName.MaxLength = 255
        Me.TxtAcName.Name = "TxtAcName"
        Me.TxtAcName.Size = New System.Drawing.Size(377, 18)
        Me.TxtAcName.TabIndex = 4
        '
        'LblAcName
        '
        Me.LblAcName.AutoSize = True
        Me.LblAcName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcName.Location = New System.Drawing.Point(204, 61)
        Me.LblAcName.Name = "LblAcName"
        Me.LblAcName.Size = New System.Drawing.Size(68, 16)
        Me.LblAcName.TabIndex = 726
        Me.LblAcName.Text = "A/C Name"
        '
        'TxtToProcess
        '
        Me.TxtToProcess.AgMandatory = True
        Me.TxtToProcess.AgMasterHelp = False
        Me.TxtToProcess.AgNumberLeftPlaces = 8
        Me.TxtToProcess.AgNumberNegetiveAllow = False
        Me.TxtToProcess.AgNumberRightPlaces = 2
        Me.TxtToProcess.AgPickFromLastValue = False
        Me.TxtToProcess.AgRowFilter = ""
        Me.TxtToProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToProcess.AgSelectedValue = Nothing
        Me.TxtToProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToProcess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToProcess.Location = New System.Drawing.Point(102, 75)
        Me.TxtToProcess.MaxLength = 20
        Me.TxtToProcess.Name = "TxtToProcess"
        Me.TxtToProcess.Size = New System.Drawing.Size(64, 18)
        Me.TxtToProcess.TabIndex = 733
        Me.TxtToProcess.Visible = False
        '
        'LblToProcess
        '
        Me.LblToProcess.AutoSize = True
        Me.LblToProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblToProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToProcess.Location = New System.Drawing.Point(6, 77)
        Me.LblToProcess.Name = "LblToProcess"
        Me.LblToProcess.Size = New System.Drawing.Size(74, 16)
        Me.LblToProcess.TabIndex = 735
        Me.LblToProcess.Text = "To Process"
        Me.LblToProcess.Visible = False
        '
        'TxtFromProcess
        '
        Me.TxtFromProcess.AgMandatory = True
        Me.TxtFromProcess.AgMasterHelp = False
        Me.TxtFromProcess.AgNumberLeftPlaces = 8
        Me.TxtFromProcess.AgNumberNegetiveAllow = False
        Me.TxtFromProcess.AgNumberRightPlaces = 2
        Me.TxtFromProcess.AgPickFromLastValue = False
        Me.TxtFromProcess.AgRowFilter = ""
        Me.TxtFromProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromProcess.AgSelectedValue = Nothing
        Me.TxtFromProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFromProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromProcess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromProcess.Location = New System.Drawing.Point(102, 55)
        Me.TxtFromProcess.MaxLength = 20
        Me.TxtFromProcess.Name = "TxtFromProcess"
        Me.TxtFromProcess.Size = New System.Drawing.Size(64, 18)
        Me.TxtFromProcess.TabIndex = 732
        Me.TxtFromProcess.Visible = False
        '
        'LblFromProcess
        '
        Me.LblFromProcess.AutoSize = True
        Me.LblFromProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblFromProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFromProcess.Location = New System.Drawing.Point(6, 57)
        Me.LblFromProcess.Name = "LblFromProcess"
        Me.LblFromProcess.Size = New System.Drawing.Size(90, 16)
        Me.LblFromProcess.TabIndex = 734
        Me.LblFromProcess.Text = "From Process"
        Me.LblFromProcess.Visible = False
        '
        'TempStockAdjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(901, 458)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "TempStockAdjustment"
        Me.Text = "TempStockAdjustment"
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
    Protected WithEvents LblSaleOrderNo As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents TxtAcName As AgControls.AgTextBox
    Protected WithEvents LblAcName As System.Windows.Forms.Label
#End Region

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 100, 0, Col1FromProcess, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ToProcess, 100, 0, Col1ToProcess, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Status, 100, 0, Col1Status, False, False, False)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 100, 8, 4, False, Col1CurrentStock, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, True, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 100, 50, Col1MeasureUnit, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 100, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 4, False, Col1MeasurePerPcs, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Anchor = AnchorStyles.None
        Panel1.Anchor = Dgl1.Anchor
        FrmProductionOrder_BaseFunction_FIniList()
        Dgl1.ColumnHeadersHeight = 35

        Dgl1.AgSkipReadOnlyColumns = True
        FrmProductionOrder_BaseFunction_FIniList()
    End Sub


    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE StockHead_Log " &
                " SET " &
                " Subcode = " & AgL.Chk_Text(TxtAcName.AgSelectedValue) & ", " &
                " FromProcess = " & AgL.Chk_Text(TxtFromProcess.AgSelectedValue) & ", " &
                " ToProcess = " & AgL.Chk_Text(TxtToProcess.AgSelectedValue) & ", " &
                " TotalQty = " & Val(LblTotalQty.Text) & ", " &
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " &
                " Amount = " & Val(LblTotalAmount.Text) & ", " &
                " " & mGodownField & " = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " Where UID = '" & mSearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Stock_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        If StockNature = StockEffect.StkIn Or StockNature = StockEffect.StkInOut Then
            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    mSr += 1

                    mQry = "INSERT INTO Stock_Log (UID, DocId, Sr, V_Type, V_Prefix, " &
                            " V_Date, V_No, Div_Code, Site_Code, SubCode, " &
                            " Item, Process, ToProcess, Godown, Qty_Rec, Unit, " &
                            " MeasurePerPcs, Measure_Rec, MeasureUnit, Rate, Amount, Status, Cost, RecId) " &
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " & mSr & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " &
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ", " & Val(TxtV_No.Text) & "," & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",  " & AgL.Chk_Text(TxtAcName.AgSelectedValue) & ", " &
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1FromProcess, I)) & "," & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1ToProcess, I)) & "," & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & "," &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1MeasureUnit, I)) & ", " & Val(Dgl1.Item(Col1Rate, I).Value) & ",  " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Status, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                            " " & AgL.Chk_Text(TxtV_No.Text) & " )"

                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
                End If
            Next
        End If

        If StockNature = StockEffect.StkOut Or StockNature = StockEffect.StkInOut Then
            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    mSr += 1

                    mQry = "INSERT INTO Stock_Log (UID, DocId, Sr, V_Type, V_Prefix, " &
                            " V_Date, V_No, Div_Code, Site_Code, SubCode, " &
                            " Item, Process, Godown, Qty_Iss, Unit, " &
                            " MeasurePerPcs, Measure_Iss, MeasureUnit, Rate, Amount, Status, Cost, RecId) " &
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " & mSr & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " &
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ", " & Val(TxtV_No.Text) & "," & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",  " & AgL.Chk_Text(TxtAcName.AgSelectedValue) & ", " &
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1ToProcess, I)) & "," & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & "," &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1MeasureUnit, I)) & ", " & Val(Dgl1.Item(Col1Rate, I).Value) & ",  " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Status, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                            " " & AgL.Chk_Text(TxtV_No.Text) & ") "

                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
                End If
            Next
        End If
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
                IniGrid()
                TxtAcName.AgSelectedValue = AgL.XNull(.Rows(0)("Subcode"))
                TxtGodown.AgSelectedValue = AgL.XNull(.Rows(0)(mGodownField))
                TxtFromProcess.AgSelectedValue = AgL.XNull(.Rows(0)("FromProcess"))
                TxtToProcess.AgSelectedValue = AgL.XNull(.Rows(0)("ToProcess"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("Amount"))


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
                            Dgl1.AgSelectedValue(Col1FromProcess, I) = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.AgSelectedValue(Col1ToProcess, I) = AgL.XNull(.Rows(I)("ToProcess"))
                            Dgl1.Item(Col1Status, I).Value = AgL.XNull(.Rows(I)("Status"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)(mQtyField)), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)(mMeasureField))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.XNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.XNull(.Rows(I)("Amount"))

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
        Dgl1.AgHelpDataSet(Col1Item, 7) = HelpDataSet.Item
        TxtGodown.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.Godown
        TxtAcName.AgHelpDataSet(2, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.AcName
        Dgl1.AgHelpDataSet(Col1FromProcess, 1) = HelpDataSet.Process
        Dgl1.AgHelpDataSet(Col1ToProcess, 1) = HelpDataSet.Process
        Dgl1.AgHelpDataSet(Col1Status) = HelpDataSet.Status
        TxtFromProcess.AgHelpDataSet = HelpDataSet.Process
        TxtToProcess.AgHelpDataSet = HelpDataSet.Process
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' "

            Case Col1FromProcess, Col1ToProcess
                'Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " Div_Code = '" & TxtDivision.AgSelectedValue & "' "

        End Select
    End Sub

    Protected Overridable Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
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
                    Dgl1.Item(Col1CurrentStock, mRowIndex).Value = AgTemplate.ClsMain.FunRetStock(
Dgl1.AgSelectedValue(Col1Item, mRowIndex), mInternalCode, , TxtGodown.AgSelectedValue, ,
AgTemplate.ClsMain.StockStatus.Standard, TxtV_Date.Text)
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
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl1.Item(Col1Status, mRow).Value = "Standard"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalAmount.Text = ""


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")

                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next

        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        With Dgl1
            If AgL.RequiredField(TxtGodown, "Godown") Then passed = False : Exit Sub
            If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
            If AgCL.AgIsDuplicate(Dgl1, CStr(Dgl1.Columns(Col1Item).Index) + "," + CStr(Dgl1.Columns(Col1FromProcess).Index) + "," + CStr(Dgl1.Columns(Col1Status).Index)) = True Then passed = False : Exit Sub

            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
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
                TxtGodown.AgRowFilter = " Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsDeleted = 0 "

        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub TempStockAdjustment_BaseFunction_Create() Handles Me.BaseFunction_CreateHelpDataSet
        mQry = "SELECT I.Code, I.Description, I.Unit, I.ItemType, I.ItemGroup, I.ItemCategory, I.SalesTaxPostingGroup, Measure, MeasureUnit , IfNull(I.IsDeleted ,0) AS IsDeleted, IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status, I.Div_Code FROM Item I"
        HelpDataSet.Item = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Description, Site_Code, IfNull(IsDeleted,0) as IsDeleted, IfNull(Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status FROM Godown Order By Description"
        HelpDataSet.Godown = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT S.SubCode AS Code, S.Name, IfNull(S.IsDeleted ,0) AS IsDeleted, IfNull(S.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status  FROM SubGroup S"
        HelpDataSet.AcName = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select P.NCat As Code, Vc.NCatDescription As Process, P.Div_Code " &
                " From Process P " &
                " LEFT JOIN VoucherCat Vc On P.NCat  = Vc.NCat " &
                " Order By Vc.NCatDescription "
        HelpDataSet.Process = AgL.FillData(mQry, AgL.GCn)

        HelpDataSet.Status = AgL.FillData(ClsMain.HelpQueries.StockStatus, AgL.GCn)
    End Sub

    Private Sub Dgl1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles Dgl1.RowsRemoved

    End Sub
End Class
