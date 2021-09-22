Imports System.IO
Imports System.Data.SQLite
Public Class FrmStoreReceive
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1ItemCategory As String = "Item Category"
    Protected Const Col1ItemGroup As String = "Item Group"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"


    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"

    Protected Const Col1Specification As String = "Specification"

    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1BaleNo As String = "Bale No"
    Protected Const Col1Process As String = "Process"
    Protected Const Col1ProdOrder As String = "Prod. Order"
    Protected Const Col1CurrentStock As String = "Current Stock"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1UnitMultiplier As String = "Unit Multiplier"
    Protected Const Col1DealQty As String = "Deal Qty"
    Protected Const Col1DealUnit As String = "Deal Unit"
    Protected Const Col1DealUnitDecimalPlaces As String = "Deal Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remarks As String = "Remarks"
    Protected WithEvents LblCurrentStock As System.Windows.Forms.Label
    Protected WithEvents LblCurrentStockText As System.Windows.Forms.Label

    Dim BlnIsLotNoVisible As Boolean = False
    Dim ImportMessegeStr$ = ""
    Dim ImportMode As Boolean = False
    Dim ImportAction_NewImport As String = "New Import"
    Dim ImportAction_ClearImport As String = "Clear Import"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal NCatStr As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = NCatStr
        mQry = "Select H.* from StockHeadSetting H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select H.* from StockHeadSetting H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select H.* from StockHeadSetting H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code Is Null And H.Site_Code Is Null "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select H.* from StockHeadSetting H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where H.V_Type Is Null And H.Div_Code Is Null And H.Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings are not defined. Can't Continue!")
        End If

    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblCurrentStock = New System.Windows.Forms.Label()
        Me.LblCurrentStockText = New System.Windows.Forms.Label()
        Me.LblDealQty = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TxtRemarks = New AgControls.AgTextBox()
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel()
        Me.LblReq_SubCode = New System.Windows.Forms.Label()
        Me.TxtParty = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtProcess = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
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
        Me.GroupBox2.Location = New System.Drawing.Point(733, 531)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(175, 531)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(127, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(121, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(538, 531)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 594)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 531)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 527)
        Me.GroupBox1.Size = New System.Drawing.Size(907, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(357, 531)
        Me.GBoxDivision.Size = New System.Drawing.Size(136, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Size = New System.Drawing.Size(130, 18)
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
        Me.LblV_No.Location = New System.Drawing.Point(348, 232)
        Me.LblV_No.Size = New System.Drawing.Size(78, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Transfer No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(470, 231)
        Me.TxtV_No.Size = New System.Drawing.Size(217, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(295, 54)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Date.Location = New System.Drawing.Point(157, 49)
        Me.LblV_Date.Size = New System.Drawing.Size(94, 14)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Receive Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(536, 36)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(313, 48)
        Me.TxtV_Date.Size = New System.Drawing.Size(120, 16)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(439, 32)
        Me.LblV_Type.Size = New System.Drawing.Size(95, 14)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Receive Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Type.Location = New System.Drawing.Point(550, 30)
        Me.TxtV_Type.Size = New System.Drawing.Size(178, 16)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(295, 36)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(157, 31)
        Me.LblSite_Code.Size = New System.Drawing.Size(95, 14)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(313, 30)
        Me.TxtSite_Code.Size = New System.Drawing.Size(120, 16)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(711, 192)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-9, 5)
        Me.TabControl1.Size = New System.Drawing.Size(907, 173)
        Me.TabControl1.TabIndex = 1
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.LblReq_SubCode)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(899, 147)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReq_SubCode, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(889, 41)
        Me.Topctrl1.TabIndex = 0
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
        Me.Dgl1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblCurrentStock)
        Me.Panel1.Controls.Add(Me.LblCurrentStockText)
        Me.Panel1.Controls.Add(Me.LblDealQty)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(5, 430)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(879, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblCurrentStock
        '
        Me.LblCurrentStock.AutoSize = True
        Me.LblCurrentStock.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStock.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblCurrentStock.Location = New System.Drawing.Point(133, 3)
        Me.LblCurrentStock.Name = "LblCurrentStock"
        Me.LblCurrentStock.Size = New System.Drawing.Size(12, 16)
        Me.LblCurrentStock.TabIndex = 668
        Me.LblCurrentStock.Text = "."
        Me.LblCurrentStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCurrentStockText
        '
        Me.LblCurrentStockText.AutoSize = True
        Me.LblCurrentStockText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStockText.ForeColor = System.Drawing.Color.Maroon
        Me.LblCurrentStockText.Location = New System.Drawing.Point(25, 3)
        Me.LblCurrentStockText.Name = "LblCurrentStockText"
        Me.LblCurrentStockText.Size = New System.Drawing.Size(102, 16)
        Me.LblCurrentStockText.TabIndex = 667
        Me.LblCurrentStockText.Text = "Current Stock :"
        '
        'LblDealQty
        '
        Me.LblDealQty.AutoSize = True
        Me.LblDealQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblDealQty.Location = New System.Drawing.Point(759, 3)
        Me.LblDealQty.Name = "LblDealQty"
        Me.LblDealQty.Size = New System.Drawing.Size(12, 16)
        Me.LblDealQty.TabIndex = 666
        Me.LblDealQty.Text = "."
        Me.LblDealQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Maroon
        Me.Label33.Location = New System.Drawing.Point(648, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(105, 16)
        Me.Label33.TabIndex = 665
        Me.Label33.Text = "Total Deal Qty :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(451, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(366, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 201)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(880, 227)
        Me.Pnl1.TabIndex = 2
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(157, 103)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(65, 14)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgAllowUserToEnableMasterHelp = False
        Me.TxtRemarks.AgLastValueTag = Nothing
        Me.TxtRemarks.AgLastValueText = Nothing
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
        Me.TxtRemarks.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(313, 102)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(415, 16)
        Me.TxtRemarks.TabIndex = 8
        '
        'TxtManualRefNo
        '
        Me.TxtReferenceNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtReferenceNo.AgLastValueTag = Nothing
        Me.TxtReferenceNo.AgLastValueText = Nothing
        Me.TxtReferenceNo.AgMandatory = True
        Me.TxtReferenceNo.AgMasterHelp = False
        Me.TxtReferenceNo.AgNumberLeftPlaces = 8
        Me.TxtReferenceNo.AgNumberNegetiveAllow = False
        Me.TxtReferenceNo.AgNumberRightPlaces = 2
        Me.TxtReferenceNo.AgPickFromLastValue = False
        Me.TxtReferenceNo.AgRowFilter = ""
        Me.TxtReferenceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReferenceNo.AgSelectedValue = Nothing
        Me.TxtReferenceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReferenceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReferenceNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferenceNo.Location = New System.Drawing.Point(550, 48)
        Me.TxtReferenceNo.MaxLength = 50
        Me.TxtReferenceNo.Name = "TxtManualRefNo"
        Me.TxtReferenceNo.Size = New System.Drawing.Size(178, 16)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblReferenceNo.AutoSize = True
        Me.LblReferenceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblReferenceNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(439, 48)
        Me.LblReferenceNo.Name = "LblManualRefNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(85, 14)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Receive No."
        '
        'LblMaterialPlanForFollowingItems
        '
        Me.LblMaterialPlanForFollowingItems.BackColor = System.Drawing.Color.SteelBlue
        Me.LblMaterialPlanForFollowingItems.DisabledLinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaterialPlanForFollowingItems.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblMaterialPlanForFollowingItems.LinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Location = New System.Drawing.Point(4, 180)
        Me.LblMaterialPlanForFollowingItems.Name = "LblMaterialPlanForFollowingItems"
        Me.LblMaterialPlanForFollowingItems.Size = New System.Drawing.Size(128, 19)
        Me.LblMaterialPlanForFollowingItems.TabIndex = 804
        Me.LblMaterialPlanForFollowingItems.TabStop = True
        Me.LblMaterialPlanForFollowingItems.Text = "Received Items"
        Me.LblMaterialPlanForFollowingItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(536, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 732
        Me.Label1.Text = "Ä"
        '
        'LblReq_SubCode
        '
        Me.LblReq_SubCode.AutoSize = True
        Me.LblReq_SubCode.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblReq_SubCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblReq_SubCode.Location = New System.Drawing.Point(295, 73)
        Me.LblReq_SubCode.Name = "LblReq_SubCode"
        Me.LblReq_SubCode.Size = New System.Drawing.Size(10, 7)
        Me.LblReq_SubCode.TabIndex = 735
        Me.LblReq_SubCode.Text = "Ä"
        '
        'TxtParty
        '
        Me.TxtParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtParty.AgLastValueTag = Nothing
        Me.TxtParty.AgLastValueText = Nothing
        Me.TxtParty.AgMandatory = True
        Me.TxtParty.AgMasterHelp = False
        Me.TxtParty.AgNumberLeftPlaces = 8
        Me.TxtParty.AgNumberNegetiveAllow = False
        Me.TxtParty.AgNumberRightPlaces = 2
        Me.TxtParty.AgPickFromLastValue = False
        Me.TxtParty.AgRowFilter = ""
        Me.TxtParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtParty.AgSelectedValue = Nothing
        Me.TxtParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtParty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParty.Location = New System.Drawing.Point(313, 66)
        Me.TxtParty.MaxLength = 20
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(415, 16)
        Me.TxtParty.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(157, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 14)
        Me.Label4.TabIndex = 734
        Me.Label4.Text = "Receive From (A/c)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(295, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 738
        Me.Label3.Text = "Ä"
        '
        'TxtProcess
        '
        Me.TxtProcess.AgAllowUserToEnableMasterHelp = False
        Me.TxtProcess.AgLastValueTag = Nothing
        Me.TxtProcess.AgLastValueText = Nothing
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
        Me.TxtProcess.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProcess.Location = New System.Drawing.Point(313, 84)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(415, 16)
        Me.TxtProcess.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(157, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 14)
        Me.Label5.TabIndex = 737
        Me.Label5.Text = "Process"
        '
        'FrmStoreReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(889, 572)
        Me.Controls.Add(Me.LblMaterialPlanForFollowingItems)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmStoreReceive"
        Me.Text = "Material Issue from Store Entry"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.LblMaterialPlanForFollowingItems, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
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
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblDealQty As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Protected WithEvents LblReq_SubCode As System.Windows.Forms.Label
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents Label5 As System.Windows.Forms.Label

#End Region

    Private Sub FrmStoreReceive_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim I As Integer = 0
        'For I = 0 To Dgl1.Rows.Count - 1
        '    If Dgl1.Item(Col1Item_UID, I).Tag <> "" Then
        '        AgTemplate.ClsMain.FUpdateItem_UidOnDelete(Dgl1.Item(Col1Item_UID, I).Tag, mSearchCode, Conn, Cmd)
        '    End If
        'Next


        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "Delete From StockProcess Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "Stock"
        MainLineTableCsv = "Stock,StockHeadDetail"
        LogLineTableCsv = "Stock_LOG,StockHeadDetail_Log"

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'If IsApplyVTypePermission Then
        '    mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        'End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Issue_Type], H.V_Date AS Date, " &
                " H.ManualRefNo, Sg.Name + (Case When IfNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName, " &
                " GF.Name as To_Godown, H.Remarks,   P.Description as Process, " &
                " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type], H.ApproveBy as Approved_By, H.ApproveDate as Approve_Date  " &
                " FROM  StockHead H   " &
                " Left Join Subgroup Sg  on H.SubCode = Sg.SubCode " &
                " Left Join City C  on Sg.CityCode = C.CityCode " &
                " LEFT JOIN Division D  ON D.Div_Code=H.Div_Code  " &
                " LEFT JOIN Process P ON H.Process=P.NCat  " &
                " LEFT JOIN SiteMast SM  ON SM.Code=H.Site_Code  " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type " &
                " LEFT JOIN Subgroup GF  ON GF.SubCode = H.FromGodown  " &
                " Where IfNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$ = ""
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'If IsApplyVTypePermission Then
        '    mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        'End If

        mQry = " Select H.DocID As SearchCode " &
            " From StockHead H " &
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date, H.V_No  "

        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCategory")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemGroup")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, Col1Item, True, False, False)


            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgTemplate.ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgTemplate.ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)

            .AddAgTextColumn(Dgl1, Col1Specification, 100, 0, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 0, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Process, 100, 0, Col1Process, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProcessLine")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 100, 0, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 100, 8, 4, False, Col1CurrentStock, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1UnitMultiplier, 70, 8, 3, False, Col1UnitMultiplier, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_UnitMultiplier")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_UnitMultiplier")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1DealQty, 70, 8, 3, False, Col1DealQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_DealQty")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_DealQty")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1DealUnit, 50, 0, Col1DealUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_DealUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_DealUnit")), Boolean))
            .AddAgTextColumn(Dgl1, Col1DealUnitDecimalPlaces, 50, 0, Col1DealUnitDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 90, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 8, 2, False, Col1Amount, False, False, False)
            .AddAgTextColumn(Dgl1, Col1Remarks, 250, 0, Col1Remarks, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl1)

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE StockHead " &
                " SET " &
                " TotalQty = " & Val(LblTotalQty.Text) & ", " &
                " TotalDealQty = " & Val(LblDealQty.Text) & ", " &
                " SubCode = " & AgL.Chk_Text(TxtParty.Tag) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " &
                " ManualRefNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'If Topctrl1.Mode <> "Add" Then
        '    mQry = " SELECT Item_UID FROM StockHeadDetail  WHERE DocId = '" & mSearchCode & "' And Item_Uid Is Not Null "
        '    Dim DtItem_Uid As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        '    If DtItem_Uid.Rows.Count > 0 Then
        '        For I = 0 To DtItem_Uid.Rows.Count - 1
        '            AgTemplate.ClsMain.FUpdateItem_UidOnDelete(DtItem_Uid.Rows(I)("Item_Uid"), mSearchCode, Conn, Cmd)
        '        Next
        '    End If
        'End If

        mQry = "Delete From StockHeadDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Stock Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "Delete From StockProcess Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                mQry = " INSERT INTO StockHeadDetail ( DocID, Sr, Item, Dimension1, Dimension2, Specification, LotNo, BaleNo, Qty, Unit, " &
                        " UnitMultiplier, DealQty, DealUnit, Rate, Amount, Remarks, Process, " &
                        " CurrentStock ) " &
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " &
                        " " & mSr & ", " &
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ",  " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ",  " &
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",  " &
                        " " & Val(Dgl1.Item(Col1UnitMultiplier, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DealQty, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1DealUnit, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            End If
        Next

        mQry = " INSERT INTO Stock (DocId,TSr, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " &
                " Item, Dimension1, Dimension2,  Godown, Qty_Rec, Unit,  UnitMultiplier, DealQty_Rec, DealUnit,  Rate, Amount, Landed_Value, EType_IR, " &
                " Cost, LotNo, BaleNo, Process, Remarks, ReferenceDocId, ReferenceDocIdSr)  " &
                " SELECT H.DocID, Max(L.Sr) as TSr, Max(L.Sr) as Sr, max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId, " &
                " max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code, max(H.SubCode) AS SubCode, L.Item, L.Dimension1, L.Dimension2, max(H.FromGodown) AS Godown, " &
                " sum(L.Qty) AS Qty_Rec, Max(L.Unit) AS Unit, max(L.UnitMultiplier) AS  UnitMultiplier, sum(L.DealQty) AS DealQty_Rec, max(L.DealUnit) AS DealUnit, max(L.Rate) AS Rate, " &
                " sum(L.Amount) AS Amount, sum(L.Amount) AS Amount, 'R', max(L.CostCenter) AS Cost, L.LotNo, L.BaleNo, L.Process, max(H.Remarks) AS Remarks, H.DocID ReferenceDocId, Max(L.Sr) as ReferenceDocIdSr " &
                " FROM StockHeadDetail L " &
                " LEFT JOIN StockHead H ON H.DocID = L.DocID " &
                " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " " &
                " GROUP BY H.DocID, L.Item, L.Dimension1, L.Dimension2, L.LotNo,L.BaleNo, L.Process "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = " INSERT INTO StockProcess (DocId, TSr, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " &
                    " Item, Dimension1, Dimension2,  Godown, Qty_Iss, Unit,  UnitMultiplier, DealQty_Iss, DealUnit,  Rate, Amount, " &
                    " Cost, LotNo, BaleNo, Process, Remarks, ReferenceDocId, ReferenceDocIdSr)  " &
                    " SELECT H.DocID, Max(L.Sr) as TSr, Max(L.Sr) as Sr, max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId, " &
                    " max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code, max(H.SubCode) AS SubCode, L.Item, L.Dimension1, L.Dimension2, max(H.FromGodown) AS Godown, " &
                    " sum(L.Qty) AS Qty_Iss, Max(L.Unit) AS Unit, max(L.UnitMultiplier) AS  UnitMultiplier, sum(L.DealQty) AS DealQty_Iss, max(L.DealUnit) AS DealUnit, max(L.Rate) AS Rate, " &
                    " sum(L.Amount) AS Amount, max(L.CostCenter) AS Cost, L.LotNo, L.BaleNo, H.Process, max(H.Remarks) AS Remarks, H.DocID, Max(L.Sr) as Sr " &
                    " FROM StockHeadDetail L " &
                    " LEFT JOIN StockHead H ON H.DocID = L.DocID " &
                    " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " " &
                    " GROUP BY H.DocID, L.Item, L.Dimension1, L.Dimension2, L.LotNo,L.BaleNo, H.Process "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If




        If ImportMode = True Then
            mQry = " UPDATE StockHead Set EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImport & "' Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub


    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*, P.Description as ProcessDesc, " &
               " Sg.Name || (Case When IfNull(Sg.CityCode,'')<>'' Then ', ' || C.CityName Else '' End) as PartyName " &
                " From StockHead H " &
                " Left Join Subgroup Sg on H.SubCode = Sg.SubCode " &
                " Left Join City C on Sg.CityCode = C.CityCode " &
                " Left Join Process P on H.Process = P.NCat " &
                " Where H.DocID='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))
                TxtParty.Tag = AgL.XNull(.Rows(0)("SubCode"))
                TxtParty.Text = AgL.XNull(.Rows(0)("PartyName"))
                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblDealQty.Text = AgL.VNull(.Rows(0)("TotalDealQty"))
                IniGrid()
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select S.Item Item, S.Specification Specification, S.Qty Qty, S.Unit Unit, S.UnitMultiplier UnitMultiplier, 
                       S.DealQty DealQty, S.DealUnit DealUnit, S.Rate Rate, S.Amount Amount, S.LotNo LotNo, S.BaleNo BaleNo, S.Process Process, 
                       S.Remarks Remarks, S.CurrentStock CurrentStock, 
                       S.Dimension1 Dimension1, S.Dimension2 Dimension2, 
                       I.ManualCode As Item_No, I.Description As Item_Desc, 
                       U.DecimalPlaces As QtyDecimalPlaces, MU.DecimalPlaces As DealDecimalPlaces, 
                       I.ItemGroup, IG.Description As ItemGroupDesc, 
                       I.ItemCategory, IC.Description as ItemCategoryDesc, P.Description As Process_Desc,                        
                       D1.Description As Dimension1Desc, D2.Description As Dimension2Desc 
                       from (Select * From StockHeadDetail  where DocId = '" & SearchCode & "') S 
                       Left Join Item I  On S.Item = I.Code 
                       LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup 
                       LEFT JOIN ItemCategory IC On IC.Code = I.ItemCategory
                       Left Join Unit U  On I.Unit = U.Code 
                       Left Join Unit MU  On I.DealUnit = MU.Code 
                       Left Join Dimension1 D1   On S.Dimension1 = D1.Code 
                       Left Join Dimension2 D2   On S.Dimension2 = D2.Code 
                       Left Join Process P  On S.Process = P.NCat                                
                       Order By S.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                            Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
                            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("Item_No"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Item_Desc"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))


                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1UnitMultiplier, I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DealQty, I).Value = Format(AgL.VNull(.Rows(I)("DealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                            Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DealDecimalPlaces"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.Item(Col1Process, I).Value = AgL.XNull(.Rows(I)("Process_Desc"))
                            Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                            Dgl1.Item(Col1CurrentStock, I).Value = AgL.VNull(.Rows(I)("CurrentStock"))
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AgL.WinSetting(Me, 610, 905)
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub

    Private Sub TxtFromGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtParty.KeyDown, TxtProcess.KeyDown
        Select Case sender.Name

            Case TxtParty.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        FCreateHelpSubgroup()
                    End If
                End If


            Case TxtProcess.Name
                'If e.KeyCode <> Keys.Enter Then
                '    If sender.AgHelpDataSet Is Nothing Then
                '        mQry = " SELECT H.NCat AS Code, H.Description AS Process FROM Process H "
                '        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                '    End If
                'End If


                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataSet Is Nothing Then
                        If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                            mQry = "Select NCat, Description from Process Where NCat IN (" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "'") & ")  "
                        Else
                            mQry = " SELECT H.NCat AS Code, H.Description AS Process FROM Process H "
                        End If
                        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtReferenceNo.Validating, TxtProcess.Validating, TxtParty.Validating

        Select Case sender.NAME
            Case TxtV_Type.Name

                mQry = "Select * from StockHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code='" & TxtDivision.Tag & "' And Site_Code ='" & TxtSite_Code.Tag & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from StockHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code='" & TxtDivision.Tag & "' And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from StockHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code Is '" & TxtSite_Code.Tag & "' "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtV_TypeSettings.Rows.Count = 0 Then
                            mQry = "Select * from StockHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code Is Null "
                            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtV_TypeSettings.Rows.Count = 0 Then
                                mQry = "Select * from StockHeadSetting  Where V_Type Is Null And Div_Code Is Null And Site_Code Is Null "
                                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            End If
                        End If
                    End If
                End If
                If DtV_TypeSettings.Rows.Count = 0 Then
                    MsgBox("Settings not found. Can not continue")
                    e.Cancel = True
                End If

                TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                IniGrid()
                FAsignProcess()
                If TxtV_Type.AgLastValueTag <> TxtV_Type.Tag Then
                    TxtParty.AgHelpDataSet = Nothing
                    Dgl1.AgHelpDataSet(Col1Item) = Nothing
                End If

            Case TxtParty.Name, TxtProcess.Name
                Dgl1.AgHelpDataSet(Col1Item) = Nothing

        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        mQry = "Select * from StockHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code='" & TxtDivision.Tag & "' And Site_Code ='" & TxtSite_Code.Tag & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from StockHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code='" & TxtDivision.Tag & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from StockHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code = '" & TxtSite_Code.Tag & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from StockHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from StockHeadSetting  Where V_Type Is Null And Div_Code Is Null And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    End If
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings are not defined. Can't Continue!")
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If



        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        'If AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) <> "" Then
        '    TxtGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown"))
        '    TxtGodown.Text = AgL.Dman_Execute("Select Description from Godown Where Code = '" & AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) & "' ", AgL.GCn).ExecuteScalar
        'End If
        FAsignProcess()

        'TxtGodown.Tag = PubDefaultGodownCode
        'TxtGodown.Text = PubDefaultGodownName
    End Sub

    Private Sub FAsignProcess()
        Dim DtTemp As DataTable = Nothing
        TxtProcess.Enabled = False
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
            If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                mQry = "Select NCat, Description from Process Where NCat IN (" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "'") & ")  "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    If DtTemp.Rows.Count = 1 Then
                        TxtProcess.Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                        TxtProcess.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                        TxtProcess.Enabled = False
                    Else
                        TxtProcess.Enabled = True
                        TxtProcess.Tag = ""
                        TxtProcess.Text = ""
                    End If
                End If
            Else
                TxtProcess.Enabled = True
                TxtProcess.Tag = ""
                TxtProcess.Text = ""
            End If
        Else
            TxtProcess.Enabled = False
            TxtProcess.Tag = ""
            TxtProcess.Text = ""
            TxtProcess.AgHelpDataSet = Nothing
        End If

    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Qty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1UnitMultiplier, Col1DealQty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1DealUnitDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1ProdOrder
                Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer = 0
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    FCreateHelpLotNo()
                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    FCreateHelpLotNo()
                Case Col1ItemGroup
                    Validating_ItemGroup(mColumnIndex, mRowIndex)
                Case Col1ItemCategory
                    Validating_ItemCategory(mColumnIndex, mRowIndex)
                Case Col1Process
                    If Dgl1.Item(Col1Process, mRowIndex).Value <> "" Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = mRowIndex To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item, I).Value <> "" Then
                                    Dgl1.Item(Col1Process, I).Tag = Dgl1.Item(Col1Process, mRowIndex).Tag
                                    Dgl1.Item(Col1Process, I).Value = Dgl1.Item(Col1Process, mRowIndex).Value
                                End If
                            Next
                        End If
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1CurrentStock, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item_Name").Value)

                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension1Caption()).Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension2Caption()).Value)

                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCategory").Value)
                    Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCategoryDescription").Value)

                    Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroup").Value)
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroupDesc").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item_No").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1UnitMultiplier, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DealQty").Value)
                    Dgl1.Item(Col1DealUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DealUnit").Value)
                    Dgl1.Item(Col1DealUnitDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("DealDecimalPlaces").Value)
                    Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , , , , TxtV_Date.Text)
                    If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                        LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode, , , , , TxtV_Date.Text), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value + 2, "0"))
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        LblTotalQty.Text = 0
        LblDealQty.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1DealQty, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1UnitMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value) + 2, "0"))
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblDealQty.Text = Val(LblDealQty.Text) + Val(Dgl1.Item(Col1DealQty, I).Value)
            End If
        Next
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
        LblDealQty.Text = Format(Val(LblDealQty.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim BalQty As Double = 0

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
            If AgL.RequiredField(TxtProcess, "Process") Then passed = False : Exit Sub
        End If

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, CStr(Dgl1.Columns(Col1Item).Index) & "," & CStr(Dgl1.Columns(Col1LotNo).Index) & "," & CStr(Dgl1.Columns(Col1Dimension1).Index) & "," & CStr(Dgl1.Columns(Col1Dimension2).Index) & "," & CStr(Dgl1.Columns(Col1Specification).Index) & "," & CStr(Dgl1.Columns(Col1Process).Index)) = True Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    ' For Validation of Stock Process 
                    If AgL.XNull(DtV_TypeSettings.Rows(0)("ItemHelpType")) = "For Stock" Then
                        mQry = "SELECT IfNull(sum(H.Qty_Rec),0) - IfNull(sum(H.Qty_Iss),0) AS BalQty " &
                                " FROM StockProcess H  " &
                                " WHERE H.DocID <> " & AgL.Chk_Text(mSearchCode) & " AND H.SubCode = " & AgL.Chk_Text(TxtParty.Tag) & " " &
                                " AND H.Item = " & AgL.Chk_Text(.Item(Col1Item, I).Tag) & " AND H.Process = " & AgL.Chk_Text(TxtProcess.Tag) & " " &
                                " AND IfNull(H.LotNo,'') = '" & .Item(Col1LotNo, I).Value & "' AND IfNull(H.Dimension1,'') = '" & .Item(Col1Dimension1, I).Tag & "' AND IfNull(H.Dimension2,'') = '" & .Item(Col1Dimension2, I).Tag & "'" &
                                " GROUP BY H.Item, IfNull(H.LotNo,''), IfNull(H.Dimension1,''), IfNull(H.Dimension2,'') "
                        BalQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
                        If Math.Round(BalQty, 4) < Math.Round(Val(.Item(Col1Qty, I).Value), 4) Then
                            MsgBox("Balance Qty of " & Dgl1.Item(Col1Item, I).Value & " is " & BalQty & " For Lot No = '" & Dgl1.Item(Col1LotNo, I).Value & "'")
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_ProcessLine")), Boolean) Then
                        If Dgl1.Item(Col1Process, I).Value = "" Then
                            MsgBox(" Process Is Required At Line No " & Dgl1.Item(ColSNo, I).Value & "")
                            Dgl1.CurrentCell = Dgl1.Item(Col1Process, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With

        Dim StrMsg1$ = ""

        If StrMsg1 <> "" Then
            If ImportMode = True Then
                ImportMessegeStr += StrMsg1
            Else
                MsgBox(StrMsg1)
            End If
            passed = False : Exit Sub
        End If

    End Sub


    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblDealQty.Text = 0 : LblTotalQty.Text = 0
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub TempStockTransferIssue_BaseFunction_Create() Handles Me.BaseFunction_CreateHelpDataSet

    End Sub

    Private Sub FrmYarnSKUOpeningStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AgL.WinSetting(Me, 566, 895)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub


    Private Sub FCreateHelpSubgroup()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('+' || H.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
                strCond += " And CharIndex('-' || H.GroupCode,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') <= 0 "
            End If
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) <> "" Then
            strCond += " And CharIndex('+' || H.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) & "') > 0 "
            strCond += " And CharIndex('-' || H.Nature,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Nature")) & "') <= 0 "
        End If


        If TxtProcess.Text = "" Then
            mQry = " SELECT H.SubCode AS Code, H.Name  || (Case When IfNull(H.CityCode,'')<>'' Then ', ' || City.CityName Else '' End) as Name " &
                    " FROM Subgroup H   " &
                    " Left Join City On H.CityCode = City.CityCode" &
                    " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            TxtParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " SELECT H.SubCode AS Code, H.Name  || (Case When IfNull(H.CityCode,'')<>'' Then ', ' || City.CityName Else '' End) AS Name " &
                    " FROM Subgroup H   " &
                    " Left Join City On H.CityCode = City.CityCode" &
                    " Left Join JobworkerProcess JP On H.SubCode = JP.SubCode" &
                    " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "' And JP.Process = '" & TxtProcess.Tag & "' " & strCond
            TxtParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FCreateHelpItem(RowIndex As Integer)
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('+' || H.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                strCond += " And CharIndex('-' || H.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('+' || H.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
                strCond += " And CharIndex('-' || H.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') <= 0 "
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('+' || H.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
                strCond += " And CharIndex('-' || H.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') <= 0 "
            End If

        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (H.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(H.ShowItemInOtherDivisions,0) =1)  "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (H.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(H.ShowItemInOtherSites,0) =1)  "
        End If


        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And H.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' "
        End If

        If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" Then
            strCond += " And H.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' "
        End If


        mQry = "SELECT H.Code, H.Description as Item_Name, H.ManualCode as Item_No, H.Unit, H.ItemGroup, IG.Description AS ItemGroupDesc, 
            H.ItemCategory, IC.Description as ItemCategoryDescription, H.DealQty, H.DealUnit, U.DecimalPlaces As QtyDecimalPlaces, MU.DecimalPlaces As DealDecimalPlaces, 
            NULL As BalQty, NULL As Process, NULL As LotNo, NULL As Dimension1, NULL As Dimension2,  NULL As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", NULL As " & AgTemplate.ClsMain.FGetDimension2Caption() & " 
            FROM Item H 
            LEFT JOIN ItemGroup IG On Ig.Code = H.ItemGroup 
            LEFT JOIN ItemCategory IC On IC.Code = H.ItemCategory
            Left Join Unit U On H.Unit = U.Code 
            Left Join Unit MU On H.DealUnit = MU.Code 
            Where IfNull(H.IsDeleted ,0)  = 0 And 
            IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 14) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpItemFromStockProcess()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('+' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
                strCond += " And CharIndex('-' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('+' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
                strCond += " And CharIndex('-' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') <= 0 "
            End If


        End If

        mQry = " SELECT H.Item AS Code, I.Description as Item_Name, H.LotNo, D1.Description AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", D2.Description AS " & AgTemplate.ClsMain.FGetDimension2Caption() & " , Round(H.BalQty,4) AS BalQty, I.ManualCode as Item_No, I.Unit, IG.Description AS ItemGroupDesc, " &
                " I.DealQty, I.DealUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as DealUnitDecimalPlaces, H.SubCode, H.Process, H.Dimension1, H.Dimension2 " &
                " FROM " &
                " ( " &
                " SELECT S.SubCode, S.process, S.Item, S.Dimension1, S.Dimension2, S.LotNo, IfNull(sum(S.Qty_Rec),0) - IfNull(sum(S.Qty_Iss),0) AS BalQty  " &
                " FROM StockProcess S " &
                " Where S.SubCode = " & AgL.Chk_Text(TxtParty.Tag) & " AND S.Process = " & AgL.Chk_Text(TxtProcess.Tag) & " " &
                " GROUP BY S.Item ,S.SubCode, S.process, S.Dimension1, S.Dimension2, S.LotNo " &
                " HAVING IfNull(sum(S.Qty_Rec),0) - IfNull(sum(S.Qty_Iss),0) > 0   " &
                " ) H " &
                " LEFT JOIN Item I ON I.Code = H.Item  " &
                " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup  " &
                " Left Join Unit U On I.Unit = U.Code  " &
                " Left Join Unit MU On I.DealUnit = MU.Code  " &
                " LEFT JOIN Dimension1 D1 ON D1.Code = H.Dimension1  " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = H.Dimension2  " &
                " Where IfNull(I.IsDeleted ,0)  = 0 " &
                " AND IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 9) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name

            Case Col1ItemCategory
                If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                    If Dgl1.AgHelpDataSet(Col1ItemCategory) Is Nothing Then
                        FCreateHelpItemCategory()
                    End If
                ElseIf e.KeyCode = Keys.Insert Then
                    FOpenItemCategoryMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                End If

            Case Col1ItemGroup
                If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                    If Dgl1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                        FCreateHelpItemGroup(Dgl1.CurrentCell.RowIndex)
                    End If
                ElseIf e.KeyCode = Keys.Insert Then
                    FOpenItemGroupMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                End If

            'Case Col1Item
            '    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
            '        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
            '            FCreateHelpItem(Dgl1.CurrentCell.RowIndex)
            '        End If
            '    ElseIf e.KeyCode = Keys.Insert Then
            '        FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
            '    End If


            Case Col1ItemCode
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
                            FCreateHelpItemFromStockProcess()
                        Else
                            FCreateHelpItem(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If
                End If

            Case Col1Item
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        If AgL.XNull(DtV_TypeSettings.Rows(0)("ItemHelpType")) = "For Stock" Then
                            FCreateHelpItemFromStockProcess()
                        Else
                            FCreateHelpItem(Dgl1.CurrentCell.RowIndex)
                        End If
                    End If
                End If


            Case Col1ProdOrder
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = "SELECT H.DocId AS Code, " &
                                " Max(H.ManualRefNo) As Prod_Order_No, Max(H.V_Type) as Prod_Order_Type " &
                                " FROM (Select DocID From ProdOrderDetail  Where Item ='" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "') L " &
                                " Left Join ProdOrder H  On L.DocID = H.DocId  " &
                                " Group By H.DocId"
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Process
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = " SELECT P.NCat AS Code, P.Description  " &
                                " FROM Process P  "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Unit
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = " SELECT H.Code, H.Code as Description  " &
                                " FROM Unit H Order by H.Code  "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Dimension1
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                        mQry = " SELECT Code, Description  FROM Dimension1  "
                        Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Dimension2
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                        mQry = " SELECT Code, Description  FROM Dimension2  "
                        Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub FCreateHelpItemCategory()
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM ItemCategory I 
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpItemGroup(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If
        End If

        If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" Then
            strCond += " And I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' "
        End If


        mQry = "SELECT I.Code, I.Description
                        FROM ItemGroup I 
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub FOpenItemMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Master", TxtV_Type.Tag)
        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1Qty, RowIndex)
        'FCreateHelpItem(Dgl1.Columns(ColumnIndex).Name)
        FCreateHelpItem(RowIndex)
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemCode(ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1Item, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub FOpenItemCategoryMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Category Master", TxtV_Type.Tag)
        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1ItemGroup, RowIndex)
        'FCreateHelpItem(Dgl1.Columns(ColumnIndex).Name)
        FCreateHelpItemCategory()
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From ItemCategory Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemCategory(ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1ItemCategory, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub FOpenItemGroupMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Group Master", TxtV_Type.Tag)
        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1ItemGroup, RowIndex)
        'FCreateHelpItem(Dgl1.Columns(ColumnIndex).Name)
        FCreateHelpItemGroup(RowIndex)
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From ItemGroup Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemGroup(ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1ItemGroup, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub Validating_ItemCategory(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try
            Dgl1.Item(Col1ItemGroup, mRow).Value = ""
            Dgl1.Item(Col1ItemGroup, mRow).Tag = ""
            Dgl1.Item(Col1Item, mRow).Value = ""
            Dgl1.Item(Col1Item, mRow).Tag = ""
            Dgl1.Item(Col1Unit, mRow).Value = ""
            Dgl1.Item(Col1Dimension1, mRow).Value = ""
            Dgl1.Item(Col1Dimension1, mRow).Tag = ""
            Dgl1.Item(Col1Dimension2, mRow).Value = ""
            Dgl1.Item(Col1Dimension2, mRow).Tag = ""

            Dgl1.AgHelpDataSet(Col1ItemGroup) = Nothing
            Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_ItemCategory Function ")
        End Try
    End Sub

    Private Sub Validating_ItemGroup(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try
            Dgl1.Item(Col1Item, mRow).Value = ""
            Dgl1.Item(Col1Item, mRow).Tag = ""
            Dgl1.Item(Col1Unit, mRow).Value = ""
            Dgl1.Item(Col1Dimension1, mRow).Value = ""
            Dgl1.Item(Col1Dimension1, mRow).Tag = ""
            Dgl1.Item(Col1Dimension2, mRow).Value = ""
            Dgl1.Item(Col1Dimension2, mRow).Tag = ""

            Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_ItemGroup Function ")
        End Try
    End Sub

    Private Sub FCreateHelpLotNo()
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            If AgL.VNull(AgL.Dman_Execute("Select IsRequired_LotNo From ItemSiteDetail L Where Code = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "' ", AgL.GcnRead).ExecuteScalar) <> 0 Then
                mQry = " SELECT L.LotNo AS Code, L.LotNo, IfNull(Sum(L.Qty_Rec), 0) - IfNull(Sum(L.Qty_Iss), 0) AS Qty " &
                        " FROM StockProcess L  " &
                        " WHERE L.Item = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' AND IfNull(l.LotNo,'') <> '' " &
                        " AND L.SubCode='" & TxtParty.Tag & "'" &
                        " GROUP BY L.LotNo " &
                        " HAVING IfNull(Sum(L.Qty_Rec), 0) - IfNull(Sum(L.Qty_Iss), 0) <> 0 "
                Dgl1.AgHelpDataSet(Col1LotNo) = AgL.FillData(mQry, AgL.GCn)
            End If
        Else
            Dgl1.AgHelpDataSet(Col1LotNo) = Nothing
        End If

    End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DOCID, H.V_TYPE, H.V_DATE, H.V_NO, H.MANUALREFNO, H.REMARKS, H.ENTRYBY, H.ENTRYDATE, " &
                " H.ENTRYTYPE, H.ENTRYSTATUS,  H.APPROVEBY, H.APPROVEDATE,  H.STATUS,  " &
                " L.SR, L.ITEM, IfNull(L.QTY,0) AS QTY, L.UNIT, L.REMARKS AS LINEREMARKS,  L.LOTNO, S.NAME AS JOBWORKERNAME, S.DISPNAME AS JOBWORKERDISPNAME,   S.ADD1, " &
                " S.ADD2,S.ADD3,C.CITYNAME,S.MOBILE,S.PHONE, S.PAN,  G.DESCRIPTION AS GODOWNDESC,  I.DESCRIPTION AS ITEMDESC,   " &
                " '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Caption_Dimension1,  '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Caption_Dimension2, " &
                " D1.Description AS D1Desc,  D2.Description AS D2Desc, U.DecimalPlaces, " &
                " I.ITEMGROUP ,   I.ITEMTYPE, IG.DESCRIPTION AS ITEMGROUPDESC,P.Description AS ProcessDesc, FP.Description AS FromProcessDesc, CM.NAME AS COSTCENTERNAME " &
                " FROM STOCKHEAD H   " &
                " LEFT JOIN STOCKHEADDETAIL L ON H.DOCID = L.DOCID   " &
                " LEFT JOIN VOUCHER_TYPE VT ON H.V_TYPE = VT.V_TYPE   " &
                " LEFT JOIN SUBGROUP S ON H.SUBCODE = S.SUBCODE   " &
                " LEFT JOIN CITY C ON S.CITYCODE = C.CITYCODE   " &
                " LEFT JOIN GODOWN G ON H.FROMGODOWN = G.CODE   " &
                " LEFT JOIN ITEM I ON L.ITEM = I.CODE   " &
                " LEFT JOIN ITEMGROUP  IG ON I.ITEMGROUP = IG.CODE  " &
                " LEFT JOIN COSTCENTERMAST CM ON L.COSTCENTER = CM.CODE " &
                " LEFT JOIN Process P ON P.NCat = H.Process " &
                " LEFT JOIN Enviro E ON E.Site_Code= H.Site_Code AND E.Div_Code = H.Div_Code " &
                " LEFT JOIN Unit U ON U.Code = L.Unit " &
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                " LEFT JOIN Process FP ON FP.NCat = L.Process " &
                " WHERE H.DocID =  '" & mSearchCode & "'  Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Store_Receive_Print", "Store Receive")
    End Sub

    Private Sub FrmStoreReceiveNew_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        TxtParty.AgHelpDataSet = Nothing
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            LblCurrentStock.Visible = True : LblCurrentStockText.Visible = True
            LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, e.RowIndex).Tag, mSearchCode, , , , , TxtV_Date.Text), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, e.RowIndex).Value + 2, "0"))
        Else
            LblCurrentStock.Visible = False : LblCurrentStockText.Visible = False
        End If
    End Sub

    Private Sub FrmStoreReceive_BaseFunction_DispText() Handles Me.BaseFunction_DispText

    End Sub
End Class
