Imports System.IO
Imports System.Data.SQLite
Imports Microsoft.Reporting.WinForms
Imports Customised.ClsMain
Imports AgLibrary.ClsMain.agConstants

Public Class FrmCustomerAcSettlementAadhat
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Protected Const ColSNo As String = "Srl."
    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1Subcode As String = "Cash / Bank A/c"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1DrCr As String = "DrCr"
    Protected Const Col1ChqRefNo As String = "Chq/Ref No"
    Protected Const Col1ChqRefDate As String = "Chq/Ref Date"
    Protected Const Col1Remarks As String = "Remarks"

    Protected WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected WithEvents Dgl4 As New AgControls.AgDataGrid
    Protected Const Col2InvoiceNo As String = "Inv. No"
    Protected Const Col2InvoiceSr As String = "Inv. Sr"
    Protected Const Col2InvoiceDate As String = "Inv. Date"
    Protected Const Col2TaxableAmount As String = "Taxable Amount"
    Protected Const Col2InvoiceAmount As String = "Inv. Amount"
    Protected Const Col2SettlementAddition As String = "Settled Additions"
    Protected Const Col2SettlementDeduction As String = "Settled Deductions"
    Protected Const Col2ItemDeductions As String = "Item Deductions"
    Protected Const Col2SettlementInvoiceAmount As String = "Settled Inv. Amt."
    Protected Const Col2BtnItemDetail As String = "Item ADJ"
    Protected Const Col2BtnAdjDetail As String = "ADJ"
    Protected Const Col2SettlementRemark As String = "Settlement Remark"
    Protected Const Col2AdjustedAmount As String = "Adjusted Amount"
    Protected Const Col2PInvoiceNo As String = "PInv.No."
    Protected Const Col2PWInvoiceAmount As String = "PW Inv Amt"

    Protected WithEvents Dgl3 As New AgControls.AgDataGrid
    Protected WithEvents Dgl5 As New AgControls.AgDataGrid
    Protected Const Col3Select As String = "Tick"
    Protected Const Col3PaymentNo As String = "Payment No"
    Protected Const Col3PaymentSr As String = "Payment Sr"
    Protected Const Col3PaymentDate As String = "Payment Date"
    Protected Const Col3Subcode As String = "Payment Mode"
    Protected Const Col3PaymentRemark As String = "Payment Remark"
    Protected Const Col3Amount As String = "Payment Amount"
    Protected Const Col3AdjustedAmount As String = "Adjusted Amount"


    Protected WithEvents TxtUptoDate As AgControls.AgTextBox
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As LinkLabel
    Protected WithEvents Panel2 As Panel
    Protected WithEvents LblPaidAmt_A As Label
    Protected WithEvents Label5 As Label
    Protected WithEvents Pnl2 As Panel
    Protected WithEvents LinkLabel2 As LinkLabel
    Protected WithEvents Panel4 As Panel
    Protected WithEvents LblSettlementAmt As Label
    Protected WithEvents Label8 As Label
    Protected WithEvents BtnFill As Button
    Protected WithEvents TxtDrCr As AgControls.AgTextBox
    Protected WithEvents Label3 As Label
    Protected WithEvents LblTotalSettledInvoiceAmount_A As Label
    Protected WithEvents Label9 As Label
    Protected WithEvents Pnl3 As Panel
    Protected WithEvents LblDifferenceAmount_A As Label
    Protected WithEvents Label10 As Label
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuEMail As ToolStripMenuItem
    Friend WithEvents MnuSendSms As ToolStripMenuItem
    Friend WithEvents MnuImportPartPaymentFromDos As ToolStripMenuItem
    Friend WithEvents MnuEditSave As ToolStripMenuItem
    Dim DtSettings As DataTable
    Friend WithEvents MnuImportFinalPaymentFromDos As ToolStripMenuItem
    Protected WithEvents Label7 As Label
    Protected WithEvents TxtDifferenceJVDocNo_A As AgControls.AgTextBox
    Protected WithEvents Pnl4 As Panel
    Protected WithEvents Pnl5 As Panel
    Protected WithEvents LblTotalSettledInvoiceAmount_W As Label
    Protected WithEvents Label12 As Label
    Protected WithEvents LblInvoiceAmt_W As Label
    Protected WithEvents Label14 As Label
    Protected WithEvents LblMaterialPlanForFollowingItems As LinkLabel
    Protected WithEvents LblPaidAmt_W As Label
    Protected WithEvents Label13 As Label
    Protected WithEvents LblDifferenceAmount_W As Label
    Protected WithEvents Label15 As Label
    Protected WithEvents Label11 As Label
    Protected WithEvents TxtDifferenceJVDocNo_W As AgControls.AgTextBox
    Protected WithEvents Label16 As Label
    Public WithEvents TxtLinkedParty As AgControls.AgTextBox
    Protected WithEvents Label17 As Label
    Protected WithEvents Label18 As Label
    Protected WithEvents LblTotalSettledInvoiceAmount_PW As Label
    Protected WithEvents Label20 As Label
    Protected WithEvents LblNotMappedInvoices As Label
    Dim mFlag_Import As Boolean = False

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal NCatStr As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = NCatStr
        'mQry = "Select H.* from LedgerHeadSetting H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        'DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        'If DtV_TypeSettings.Rows.Count = 0 Then
        '    mQry = "Select H.* from LedgerHeadSetting H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code is Null "
        '    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '    If DtV_TypeSettings.Rows.Count = 0 Then
        '        mQry = "Select H.* from LedgerHeadSetting H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Site_Code ='" & AgL.PubSiteCode & "'  And H.Div_Code Is Null "
        '        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '        If DtV_TypeSettings.Rows.Count = 0 Then
        '            mQry = "Select H.* from LedgerHeadSetting H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Site_Code Is Null  And H.Div_Code Is Null "
        '            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '        End If
        '    End If
        'End If
        'If DtV_TypeSettings.Rows.Count = 0 Then MsgBox("Voucher Type Settings are not defined. Can't Continue!")

        mQry = "Select * from Cloth_SupplierSettlementSetting Where Div_Code='" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' "
        DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtSettings.Rows.Count = 0 Then
            mQry = "Select * from Cloth_SupplierSettlementSetting Where Div_Code='" & AgL.PubDivCode & "' And Site_Code Is Null "
            DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtSettings.Rows.Count = 0 Then
                mQry = "Select * from Cloth_SupplierSettlementSetting Where Site_Code='" & AgL.PubSiteCode & "' And Div_Code is Null "
                DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtSettings.Rows.Count = 0 Then
                    mQry = "Select * from Cloth_SupplierSettlementSetting Where Div_Code Is Null And Site_Code Is Null "
                    DtSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                End If
            End If
        End If
        If DtSettings.Rows.Count = 0 Then MsgBox("Settings not found.")
    End Sub

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        If LblV_Type.Tag.ToString.ToUpper = Ncat.PaymentSettlement Then
            mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, TxtSite_Code.Tag, VoucherCategory.Payment, LblV_Type.Tag, TxtV_Type.Tag, "", "")
        Else
            mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, TxtSite_Code.Tag, VoucherCategory.Receipt, LblV_Type.Tag, TxtV_Type.Tag, "", "")
        End If
        FGetSettings = mValue
    End Function


#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblTotalSettledInvoiceAmount_W = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.LblInvoiceAmt_W = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.LblTotalSettledInvoiceAmount_A = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LblInvoiceAmt_A = New System.Windows.Forms.Label()
        Me.LblBillAmountText = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TxtRemarks = New AgControls.AgTextBox()
        Me.LblReq_SubCode = New System.Windows.Forms.Label()
        Me.TxtParty = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtUptoDate = New AgControls.AgTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LblPaidAmt_W = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.LblPaidAmt_A = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LblSettlementAmt = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Pnl3 = New System.Windows.Forms.Panel()
        Me.BtnFill = New System.Windows.Forms.Button()
        Me.TxtDrCr = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblDifferenceAmount_A = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportPartPaymentFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFinalPaymentFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEditSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEMail = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSendSms = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtDifferenceJVDocNo_A = New AgControls.AgTextBox()
        Me.Pnl4 = New System.Windows.Forms.Panel()
        Me.Pnl5 = New System.Windows.Forms.Panel()
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel()
        Me.LblDifferenceAmount_W = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtDifferenceJVDocNo_W = New AgControls.AgTextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TxtLinkedParty = New AgControls.AgTextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.LblTotalSettledInvoiceAmount_PW = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.LblNotMappedInvoices = New System.Windows.Forms.Label()
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
        Me.Panel4.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(733, 571)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 571)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(142, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 571)
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
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 567)
        Me.GroupBox1.Size = New System.Drawing.Size(1218, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 571)
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
        Me.Label2.Location = New System.Drawing.Point(193, 41)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Date.Location = New System.Drawing.Point(94, 36)
        Me.LblV_Date.Size = New System.Drawing.Size(77, 14)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(1074, 57)
        Me.LblV_TypeReq.Tag = ""
        Me.LblV_TypeReq.Visible = False
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(211, 35)
        Me.TxtV_Date.Size = New System.Drawing.Size(120, 16)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(998, 52)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Visible = False
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(1099, 53)
        Me.TxtV_Type.Size = New System.Drawing.Size(59, 18)
        Me.TxtV_Type.TabIndex = 4
        Me.TxtV_Type.Tag = ""
        Me.TxtV_Type.Visible = False
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(193, 23)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(94, 18)
        Me.LblSite_Code.Size = New System.Drawing.Size(95, 14)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(211, 17)
        Me.TxtSite_Code.Size = New System.Drawing.Size(120, 16)
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
        Me.TabControl1.Size = New System.Drawing.Size(1209, 100)
        Me.TabControl1.TabIndex = 1
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label16)
        Me.TP1.Controls.Add(Me.TxtLinkedParty)
        Me.TP1.Controls.Add(Me.Label17)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtDrCr)
        Me.TP1.Controls.Add(Me.BtnFill)
        Me.TP1.Controls.Add(Me.TxtUptoDate)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.LblReq_SubCode)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(1201, 74)
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
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReq_SubCode, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtUptoDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFill, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDrCr, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label17, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtLinkedParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label16, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(1200, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(195, 60)
        Me.Label1.TabIndex = 732
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.AgMandatory = True
        Me.TxtReferenceNo.AgMasterHelp = False
        Me.TxtReferenceNo.AgNumberRightPlaces = 2
        Me.TxtReferenceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReferenceNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferenceNo.Location = New System.Drawing.Point(211, 54)
        Me.TxtReferenceNo.MaxLength = 50
        Me.TxtReferenceNo.Size = New System.Drawing.Size(120, 16)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(95, 54)
        Me.LblReferenceNo.Size = New System.Drawing.Size(68, 14)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Entry No."
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
        Me.Panel1.Controls.Add(Me.LblTotalSettledInvoiceAmount_PW)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.LblTotalSettledInvoiceAmount_W)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.LblTotalSettledInvoiceAmount_A)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Location = New System.Drawing.Point(7, 105)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1193, 21)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalSettledInvoiceAmount_W
        '
        Me.LblTotalSettledInvoiceAmount_W.AutoSize = True
        Me.LblTotalSettledInvoiceAmount_W.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalSettledInvoiceAmount_W.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalSettledInvoiceAmount_W.Location = New System.Drawing.Point(711, 3)
        Me.LblTotalSettledInvoiceAmount_W.Name = "LblTotalSettledInvoiceAmount_W"
        Me.LblTotalSettledInvoiceAmount_W.Size = New System.Drawing.Size(11, 14)
        Me.LblTotalSettledInvoiceAmount_W.TabIndex = 666
        Me.LblTotalSettledInvoiceAmount_W.Text = "."
        Me.LblTotalSettledInvoiceAmount_W.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Maroon
        Me.Label12.Location = New System.Drawing.Point(522, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(192, 14)
        Me.Label12.TabIndex = 665
        Me.Label12.Text = "Total Settled Inv. Amt (W) :"
        '
        'LblInvoiceAmt_W
        '
        Me.LblInvoiceAmt_W.AutoSize = True
        Me.LblInvoiceAmt_W.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInvoiceAmt_W.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblInvoiceAmt_W.Location = New System.Drawing.Point(1059, 479)
        Me.LblInvoiceAmt_W.Name = "LblInvoiceAmt_W"
        Me.LblInvoiceAmt_W.Size = New System.Drawing.Size(11, 14)
        Me.LblInvoiceAmt_W.TabIndex = 664
        Me.LblInvoiceAmt_W.Text = "."
        Me.LblInvoiceAmt_W.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblInvoiceAmt_W.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Maroon
        Me.Label14.Location = New System.Drawing.Point(927, 478)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(141, 14)
        Me.Label14.TabIndex = 663
        Me.Label14.Text = "Total Inv. Amt (W) :"
        Me.Label14.Visible = False
        '
        'LblTotalSettledInvoiceAmount_A
        '
        Me.LblTotalSettledInvoiceAmount_A.AutoSize = True
        Me.LblTotalSettledInvoiceAmount_A.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalSettledInvoiceAmount_A.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalSettledInvoiceAmount_A.Location = New System.Drawing.Point(312, 2)
        Me.LblTotalSettledInvoiceAmount_A.Name = "LblTotalSettledInvoiceAmount_A"
        Me.LblTotalSettledInvoiceAmount_A.Size = New System.Drawing.Size(11, 14)
        Me.LblTotalSettledInvoiceAmount_A.TabIndex = 662
        Me.LblTotalSettledInvoiceAmount_A.Text = "."
        Me.LblTotalSettledInvoiceAmount_A.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Maroon
        Me.Label9.Location = New System.Drawing.Point(127, 2)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(187, 14)
        Me.Label9.TabIndex = 661
        Me.Label9.Text = "Total Settled Inv. Amt (A) :"
        '
        'LblInvoiceAmt_A
        '
        Me.LblInvoiceAmt_A.AutoSize = True
        Me.LblInvoiceAmt_A.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInvoiceAmt_A.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblInvoiceAmt_A.Location = New System.Drawing.Point(1063, 460)
        Me.LblInvoiceAmt_A.Name = "LblInvoiceAmt_A"
        Me.LblInvoiceAmt_A.Size = New System.Drawing.Size(11, 14)
        Me.LblInvoiceAmt_A.TabIndex = 660
        Me.LblInvoiceAmt_A.Text = "."
        Me.LblInvoiceAmt_A.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblInvoiceAmt_A.Visible = False
        '
        'LblBillAmountText
        '
        Me.LblBillAmountText.AutoSize = True
        Me.LblBillAmountText.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBillAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblBillAmountText.Location = New System.Drawing.Point(927, 460)
        Me.LblBillAmountText.Name = "LblBillAmountText"
        Me.LblBillAmountText.Size = New System.Drawing.Size(136, 14)
        Me.LblBillAmountText.TabIndex = 659
        Me.LblBillAmountText.Text = "Total Inv. Amt (A) :"
        Me.LblBillAmountText.Visible = False
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 478)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(593, 86)
        Me.Pnl1.TabIndex = 12
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(808, 19)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(876, 19)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(281, 16)
        Me.TxtRemarks.TabIndex = 7
        '
        'LblReq_SubCode
        '
        Me.LblReq_SubCode.AutoSize = True
        Me.LblReq_SubCode.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblReq_SubCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblReq_SubCode.Location = New System.Drawing.Point(452, 23)
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
        Me.TxtParty.Location = New System.Drawing.Point(468, 18)
        Me.TxtParty.MaxLength = 0
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(335, 16)
        Me.TxtParty.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(339, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 14)
        Me.Label4.TabIndex = 734
        Me.Label4.Text = "Party Name (A)"
        '
        'TxtUptoDate
        '
        Me.TxtUptoDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtUptoDate.AgLastValueTag = Nothing
        Me.TxtUptoDate.AgLastValueText = Nothing
        Me.TxtUptoDate.AgMandatory = False
        Me.TxtUptoDate.AgMasterHelp = False
        Me.TxtUptoDate.AgNumberLeftPlaces = 0
        Me.TxtUptoDate.AgNumberNegetiveAllow = False
        Me.TxtUptoDate.AgNumberRightPlaces = 0
        Me.TxtUptoDate.AgPickFromLastValue = False
        Me.TxtUptoDate.AgRowFilter = ""
        Me.TxtUptoDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUptoDate.AgSelectedValue = Nothing
        Me.TxtUptoDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUptoDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtUptoDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUptoDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUptoDate.Location = New System.Drawing.Point(468, 56)
        Me.TxtUptoDate.MaxLength = 100
        Me.TxtUptoDate.Name = "TxtUptoDate"
        Me.TxtUptoDate.Size = New System.Drawing.Size(98, 16)
        Me.TxtUptoDate.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(337, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 14)
        Me.Label6.TabIndex = 743
        Me.Label6.Text = "Bills Upto Date"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(5, 274)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(219, 19)
        Me.LinkLabel1.TabIndex = 807
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Advance Payments / Debit Notes"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel2.Controls.Add(Me.LblPaidAmt_W)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.LblPaidAmt_A)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Location = New System.Drawing.Point(7, 273)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1193, 21)
        Me.Panel2.TabIndex = 806
        '
        'LblPaidAmt_W
        '
        Me.LblPaidAmt_W.AutoSize = True
        Me.LblPaidAmt_W.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaidAmt_W.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblPaidAmt_W.Location = New System.Drawing.Point(809, 3)
        Me.LblPaidAmt_W.Name = "LblPaidAmt_W"
        Me.LblPaidAmt_W.Size = New System.Drawing.Size(11, 14)
        Me.LblPaidAmt_W.TabIndex = 662
        Me.LblPaidAmt_W.Text = "."
        Me.LblPaidAmt_W.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Maroon
        Me.Label13.Location = New System.Drawing.Point(664, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(148, 14)
        Me.Label13.TabIndex = 661
        Me.Label13.Text = "Total Paid Amt (W) : "
        '
        'LblPaidAmt_A
        '
        Me.LblPaidAmt_A.AutoSize = True
        Me.LblPaidAmt_A.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaidAmt_A.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblPaidAmt_A.Location = New System.Drawing.Point(394, 3)
        Me.LblPaidAmt_A.Name = "LblPaidAmt_A"
        Me.LblPaidAmt_A.Size = New System.Drawing.Size(11, 14)
        Me.LblPaidAmt_A.TabIndex = 660
        Me.LblPaidAmt_A.Text = "."
        Me.LblPaidAmt_A.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(249, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(143, 14)
        Me.Label5.TabIndex = 659
        Me.Label5.Text = "Total Paid Amt (A) : "
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(4, 127)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(508, 145)
        Me.Pnl2.TabIndex = 10
        '
        'LinkLabel2
        '
        Me.LinkLabel2.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Location = New System.Drawing.Point(5, 457)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(247, 19)
        Me.LinkLabel2.TabIndex = 810
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Final Payments"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel4.Controls.Add(Me.LblSettlementAmt)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Location = New System.Drawing.Point(7, 456)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(590, 21)
        Me.Panel4.TabIndex = 809
        '
        'LblSettlementAmt
        '
        Me.LblSettlementAmt.AutoSize = True
        Me.LblSettlementAmt.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSettlementAmt.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblSettlementAmt.Location = New System.Drawing.Point(402, 4)
        Me.LblSettlementAmt.Name = "LblSettlementAmt"
        Me.LblSettlementAmt.Size = New System.Drawing.Size(11, 14)
        Me.LblSettlementAmt.TabIndex = 660
        Me.LblSettlementAmt.Text = "."
        Me.LblSettlementAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Maroon
        Me.Label8.Location = New System.Drawing.Point(248, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(155, 14)
        Me.Label8.TabIndex = 659
        Me.Label8.Text = "Total Settlement Amt :"
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(4, 295)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(594, 157)
        Me.Pnl3.TabIndex = 11
        '
        'BtnFill
        '
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFill.Location = New System.Drawing.Point(813, 47)
        Me.BtnFill.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(60, 23)
        Me.BtnFill.TabIndex = 8
        Me.BtnFill.TabStop = False
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFill.UseVisualStyleBackColor = True
        '
        'TxtDrCr
        '
        Me.TxtDrCr.AgAllowUserToEnableMasterHelp = False
        Me.TxtDrCr.AgLastValueTag = Nothing
        Me.TxtDrCr.AgLastValueText = Nothing
        Me.TxtDrCr.AgMandatory = True
        Me.TxtDrCr.AgMasterHelp = False
        Me.TxtDrCr.AgNumberLeftPlaces = 8
        Me.TxtDrCr.AgNumberNegetiveAllow = False
        Me.TxtDrCr.AgNumberRightPlaces = 2
        Me.TxtDrCr.AgPickFromLastValue = False
        Me.TxtDrCr.AgRowFilter = ""
        Me.TxtDrCr.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDrCr.AgSelectedValue = Nothing
        Me.TxtDrCr.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDrCr.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDrCr.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDrCr.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDrCr.Location = New System.Drawing.Point(677, 56)
        Me.TxtDrCr.MaxLength = 20
        Me.TxtDrCr.Name = "TxtDrCr"
        Me.TxtDrCr.Size = New System.Drawing.Size(126, 16)
        Me.TxtDrCr.TabIndex = 6
        Me.TxtDrCr.Text = "DrCr"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(573, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 14)
        Me.Label3.TabIndex = 813
        Me.Label3.Text = "Debit / Credit"
        '
        'LblDifferenceAmount_A
        '
        Me.LblDifferenceAmount_A.AutoSize = True
        Me.LblDifferenceAmount_A.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDifferenceAmount_A.ForeColor = System.Drawing.Color.Black
        Me.LblDifferenceAmount_A.Location = New System.Drawing.Point(610, 540)
        Me.LblDifferenceAmount_A.Name = "LblDifferenceAmount_A"
        Me.LblDifferenceAmount_A.Size = New System.Drawing.Size(13, 16)
        Me.LblDifferenceAmount_A.TabIndex = 812
        Me.LblDifferenceAmount_A.Text = "."
        Me.LblDifferenceAmount_A.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(607, 526)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 14)
        Me.Label10.TabIndex = 811
        Me.Label10.Text = "Diff. Amt (A) :"
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportPartPaymentFromDos, Me.MnuImportFinalPaymentFromDos, Me.MnuEditSave, Me.MnuEMail, Me.MnuSendSms})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(243, 114)
        '
        'MnuImportPartPaymentFromDos
        '
        Me.MnuImportPartPaymentFromDos.Name = "MnuImportPartPaymentFromDos"
        Me.MnuImportPartPaymentFromDos.Size = New System.Drawing.Size(242, 22)
        Me.MnuImportPartPaymentFromDos.Text = "Import Part Payment From Dos"
        '
        'MnuImportFinalPaymentFromDos
        '
        Me.MnuImportFinalPaymentFromDos.Name = "MnuImportFinalPaymentFromDos"
        Me.MnuImportFinalPaymentFromDos.Size = New System.Drawing.Size(242, 22)
        Me.MnuImportFinalPaymentFromDos.Text = "Import Final Payment From Dos"
        '
        'MnuEditSave
        '
        Me.MnuEditSave.Name = "MnuEditSave"
        Me.MnuEditSave.Size = New System.Drawing.Size(242, 22)
        Me.MnuEditSave.Text = "Edit & Save"
        '
        'MnuEMail
        '
        Me.MnuEMail.Name = "MnuEMail"
        Me.MnuEMail.Size = New System.Drawing.Size(242, 22)
        Me.MnuEMail.Text = "E-Mail"
        '
        'MnuSendSms
        '
        Me.MnuSendSms.Name = "MnuSendSms"
        Me.MnuSendSms.Size = New System.Drawing.Size(242, 22)
        Me.MnuSendSms.Text = "Send Sms"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(875, 526)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(137, 14)
        Me.Label7.TabIndex = 815
        Me.Label7.Text = "Diff. JV Doc.No. (A)"
        '
        'TxtDifferenceJVDocNo_A
        '
        Me.TxtDifferenceJVDocNo_A.AgAllowUserToEnableMasterHelp = False
        Me.TxtDifferenceJVDocNo_A.AgLastValueTag = Nothing
        Me.TxtDifferenceJVDocNo_A.AgLastValueText = Nothing
        Me.TxtDifferenceJVDocNo_A.AgMandatory = False
        Me.TxtDifferenceJVDocNo_A.AgMasterHelp = False
        Me.TxtDifferenceJVDocNo_A.AgNumberLeftPlaces = 0
        Me.TxtDifferenceJVDocNo_A.AgNumberNegetiveAllow = False
        Me.TxtDifferenceJVDocNo_A.AgNumberRightPlaces = 0
        Me.TxtDifferenceJVDocNo_A.AgPickFromLastValue = False
        Me.TxtDifferenceJVDocNo_A.AgRowFilter = ""
        Me.TxtDifferenceJVDocNo_A.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDifferenceJVDocNo_A.AgSelectedValue = Nothing
        Me.TxtDifferenceJVDocNo_A.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDifferenceJVDocNo_A.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDifferenceJVDocNo_A.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDifferenceJVDocNo_A.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDifferenceJVDocNo_A.Location = New System.Drawing.Point(878, 545)
        Me.TxtDifferenceJVDocNo_A.MaxLength = 255
        Me.TxtDifferenceJVDocNo_A.Name = "TxtDifferenceJVDocNo_A"
        Me.TxtDifferenceJVDocNo_A.Size = New System.Drawing.Size(147, 16)
        Me.TxtDifferenceJVDocNo_A.TabIndex = 814
        '
        'Pnl4
        '
        Me.Pnl4.Location = New System.Drawing.Point(517, 127)
        Me.Pnl4.Name = "Pnl4"
        Me.Pnl4.Size = New System.Drawing.Size(681, 145)
        Me.Pnl4.TabIndex = 11
        '
        'Pnl5
        '
        Me.Pnl5.Location = New System.Drawing.Point(604, 295)
        Me.Pnl5.Name = "Pnl5"
        Me.Pnl5.Size = New System.Drawing.Size(593, 157)
        Me.Pnl5.TabIndex = 12
        '
        'LblMaterialPlanForFollowingItems
        '
        Me.LblMaterialPlanForFollowingItems.BackColor = System.Drawing.Color.SteelBlue
        Me.LblMaterialPlanForFollowingItems.DisabledLinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaterialPlanForFollowingItems.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblMaterialPlanForFollowingItems.LinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Location = New System.Drawing.Point(5, 106)
        Me.LblMaterialPlanForFollowingItems.Name = "LblMaterialPlanForFollowingItems"
        Me.LblMaterialPlanForFollowingItems.Size = New System.Drawing.Size(90, 19)
        Me.LblMaterialPlanForFollowingItems.TabIndex = 804
        Me.LblMaterialPlanForFollowingItems.TabStop = True
        Me.LblMaterialPlanForFollowingItems.Text = "Invoices"
        Me.LblMaterialPlanForFollowingItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblDifferenceAmount_W
        '
        Me.LblDifferenceAmount_W.AutoSize = True
        Me.LblDifferenceAmount_W.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDifferenceAmount_W.ForeColor = System.Drawing.Color.Black
        Me.LblDifferenceAmount_W.Location = New System.Drawing.Point(753, 541)
        Me.LblDifferenceAmount_W.Name = "LblDifferenceAmount_W"
        Me.LblDifferenceAmount_W.Size = New System.Drawing.Size(13, 16)
        Me.LblDifferenceAmount_W.TabIndex = 817
        Me.LblDifferenceAmount_W.Text = "."
        Me.LblDifferenceAmount_W.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Maroon
        Me.Label15.Location = New System.Drawing.Point(750, 527)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(106, 14)
        Me.Label15.TabIndex = 816
        Me.Label15.Text = "Diff. Amt (W) :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1035, 526)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(142, 14)
        Me.Label11.TabIndex = 819
        Me.Label11.Text = "Diff. JV Doc.No. (W)"
        '
        'TxtDifferenceJVDocNo_W
        '
        Me.TxtDifferenceJVDocNo_W.AgAllowUserToEnableMasterHelp = False
        Me.TxtDifferenceJVDocNo_W.AgLastValueTag = Nothing
        Me.TxtDifferenceJVDocNo_W.AgLastValueText = Nothing
        Me.TxtDifferenceJVDocNo_W.AgMandatory = False
        Me.TxtDifferenceJVDocNo_W.AgMasterHelp = False
        Me.TxtDifferenceJVDocNo_W.AgNumberLeftPlaces = 0
        Me.TxtDifferenceJVDocNo_W.AgNumberNegetiveAllow = False
        Me.TxtDifferenceJVDocNo_W.AgNumberRightPlaces = 0
        Me.TxtDifferenceJVDocNo_W.AgPickFromLastValue = False
        Me.TxtDifferenceJVDocNo_W.AgRowFilter = ""
        Me.TxtDifferenceJVDocNo_W.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDifferenceJVDocNo_W.AgSelectedValue = Nothing
        Me.TxtDifferenceJVDocNo_W.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDifferenceJVDocNo_W.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDifferenceJVDocNo_W.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDifferenceJVDocNo_W.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDifferenceJVDocNo_W.Location = New System.Drawing.Point(1038, 545)
        Me.TxtDifferenceJVDocNo_W.MaxLength = 255
        Me.TxtDifferenceJVDocNo_W.Name = "TxtDifferenceJVDocNo_W"
        Me.TxtDifferenceJVDocNo_W.Size = New System.Drawing.Size(149, 16)
        Me.TxtDifferenceJVDocNo_W.TabIndex = 818
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(452, 42)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(10, 7)
        Me.Label16.TabIndex = 816
        Me.Label16.Text = "Ä"
        '
        'TxtLinkedParty
        '
        Me.TxtLinkedParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtLinkedParty.AgLastValueTag = Nothing
        Me.TxtLinkedParty.AgLastValueText = Nothing
        Me.TxtLinkedParty.AgMandatory = True
        Me.TxtLinkedParty.AgMasterHelp = False
        Me.TxtLinkedParty.AgNumberLeftPlaces = 8
        Me.TxtLinkedParty.AgNumberNegetiveAllow = False
        Me.TxtLinkedParty.AgNumberRightPlaces = 2
        Me.TxtLinkedParty.AgPickFromLastValue = False
        Me.TxtLinkedParty.AgRowFilter = ""
        Me.TxtLinkedParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLinkedParty.AgSelectedValue = Nothing
        Me.TxtLinkedParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLinkedParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtLinkedParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLinkedParty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLinkedParty.Location = New System.Drawing.Point(468, 37)
        Me.TxtLinkedParty.MaxLength = 0
        Me.TxtLinkedParty.Name = "TxtLinkedParty"
        Me.TxtLinkedParty.Size = New System.Drawing.Size(335, 16)
        Me.TxtLinkedParty.TabIndex = 814
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(339, 39)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(117, 14)
        Me.Label17.TabIndex = 815
        Me.Label17.Text = "Party Name (W)"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(601, 478)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(177, 14)
        Me.Label18.TabIndex = 820
        Me.Label18.Text = "Dr. to receive / Cr. to pay"
        '
        'LblTotalSettledInvoiceAmount_PW
        '
        Me.LblTotalSettledInvoiceAmount_PW.AutoSize = True
        Me.LblTotalSettledInvoiceAmount_PW.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalSettledInvoiceAmount_PW.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalSettledInvoiceAmount_PW.Location = New System.Drawing.Point(1068, 3)
        Me.LblTotalSettledInvoiceAmount_PW.Name = "LblTotalSettledInvoiceAmount_PW"
        Me.LblTotalSettledInvoiceAmount_PW.Size = New System.Drawing.Size(11, 14)
        Me.LblTotalSettledInvoiceAmount_PW.TabIndex = 668
        Me.LblTotalSettledInvoiceAmount_PW.Text = "."
        Me.LblTotalSettledInvoiceAmount_PW.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Maroon
        Me.Label20.Location = New System.Drawing.Point(861, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(211, 14)
        Me.Label20.TabIndex = 667
        Me.Label20.Text = "Total Settled Inv. Amt (A+W) :"
        '
        'LblNotMappedInvoices
        '
        Me.LblNotMappedInvoices.AutoSize = True
        Me.LblNotMappedInvoices.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNotMappedInvoices.ForeColor = System.Drawing.Color.Red
        Me.LblNotMappedInvoices.Location = New System.Drawing.Point(816, 460)
        Me.LblNotMappedInvoices.Name = "LblNotMappedInvoices"
        Me.LblNotMappedInvoices.Size = New System.Drawing.Size(11, 14)
        Me.LblNotMappedInvoices.TabIndex = 821
        Me.LblNotMappedInvoices.Text = "."
        Me.LblNotMappedInvoices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmCustomerAcSettlementAadhat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(1200, 612)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.LblNotMappedInvoices)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.LblInvoiceAmt_W)
        Me.Controls.Add(Me.TxtDifferenceJVDocNo_W)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.LblDifferenceAmount_W)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.LblInvoiceAmt_A)
        Me.Controls.Add(Me.LblBillAmountText)
        Me.Controls.Add(Me.Pnl5)
        Me.Controls.Add(Me.Pnl4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.LblDifferenceAmount_A)
        Me.Controls.Add(Me.TxtDifferenceJVDocNo_A)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.LblMaterialPlanForFollowingItems)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmCustomerAcSettlementAadhat"
        Me.Text = "Material Issue from Store Entry"
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LblMaterialPlanForFollowingItems, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.Pnl3, 0)
        Me.Controls.SetChildIndex(Me.Panel4, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel2, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.TxtDifferenceJVDocNo_A, 0)
        Me.Controls.SetChildIndex(Me.LblDifferenceAmount_A, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Pnl4, 0)
        Me.Controls.SetChildIndex(Me.Pnl5, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.LblBillAmountText, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.LblInvoiceAmt_A, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Controls.SetChildIndex(Me.LblDifferenceAmount_W, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.TxtDifferenceJVDocNo_W, 0)
        Me.Controls.SetChildIndex(Me.LblInvoiceAmt_W, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.Label18, 0)
        Me.Controls.SetChildIndex(Me.LblNotMappedInvoices, 0)
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
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblInvoiceAmt_A As System.Windows.Forms.Label
    Protected WithEvents LblBillAmountText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblReq_SubCode As System.Windows.Forms.Label
    Public WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label

#End Region

    Private Sub FrmStoreReceive_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim I As Integer = 0
        mQry = "Delete From LedgerAdj Where ReferenceDocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From TransactionReferences Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Ledger Where ReferenceDocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "LedgerHead"
        MainLineTableCsv = "Ledger,Cloth_SupplierSettlementInvoicesLine,Cloth_SupplierSettlementInvoicesAdjustment,Cloth_SupplierSettlementInvoices,Cloth_SupplierSettlementPayments,LedgerHeadDetail"

        AgL.GridDesign(Dgl1) : AgL.GridDesign(Dgl2) : AgL.GridDesign(Dgl3)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'If IsApplyVTypePermission Then
        '    mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        'End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Entry_Type], H.V_Date AS Voucher_Date, " &
                " H.ManualRefNo, Sg.Name  as PartyName, " &
                " H.Remarks,  " &
                " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.ApproveBy as Approved_By, H.ApproveDate as Approve_Date  " &
                " FROM  LedgerHead H   " &
                " Left Join viewHelpSubgroup Sg  on H.SubCode = Sg.Code " &
                " LEFT JOIN Division D  ON D.Div_Code=H.Div_Code  " &
                " LEFT JOIN SiteMast SM  ON SM.Code=H.Site_Code  " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type " &
                " Where 1=1  " & mCondStr

        AgL.PubFindQryOrdBy = "[Voucher_Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$ = ""
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'If IsApplyVTypePermission Then
        '    mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        'End If

        mQry = " Select H.DocID As SearchCode " &
            " From LedgerHead H " &
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
            " Where 1=1  " & mCondStr & "  Order By H.V_Date , H.V_No  "

        'mQry = " Select H.DocID As SearchCode " &
        '    " From LedgerHead H " &
        '    " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
        '    " Left JOIN Ledger L On H.DocId = L.DocId " &
        '    " Where 1=1 And L.DocId Is Null   " & mCondStr & "  Order By H.V_Date , H.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Subcode, 150, 0, Col1Subcode, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 8, 2, False, Col1Amount, True, False, False)
            .AddAgTextColumn(Dgl1, Col1DrCr, 150, 0, Col1DrCr, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ChqRefNo, 80, 20, Col1ChqRefNo, False, False, False)
            .AddAgDateColumn(Dgl1, Col1ChqRefDate, 90, Col1ChqRefDate, False, False)
            .AddAgTextColumn(Dgl1, Col1Remarks, 100, 0, Col1Remarks, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 30
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgL.GridDesign(Dgl1)
        Dgl1.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Bold)
        Dgl1.DefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Regular)


        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)




        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2InvoiceNo, 90, 0, Col2InvoiceNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2InvoiceSr, 90, 0, Col2InvoiceSr, False, True, False)
            .AddAgDateColumn(Dgl2, Col2InvoiceDate, 90, Col2InvoiceDate, True, True)
            .AddAgNumberColumn(Dgl2, Col2TaxableAmount, 80, 8, 2, False, Col2TaxableAmount, True, True, True)
            .AddAgNumberColumn(Dgl2, Col2InvoiceAmount, 80, 8, 2, False, Col2InvoiceAmount, True, True, True)
            .AddAgNumberColumn(Dgl2, Col2SettlementAddition, 80, 8, 2, False, Col2SettlementAddition, False, True, True)
            .AddAgNumberColumn(Dgl2, Col2SettlementDeduction, 80, 8, 2, False, Col2SettlementDeduction, False, True, True)
            .AddAgNumberColumn(Dgl2, Col2ItemDeductions, 80, 8, 2, False, Col2ItemDeductions, False, True, True)
            .AddAgNumberColumn(Dgl2, Col2SettlementInvoiceAmount, 80, 8, 2, False, Col2SettlementInvoiceAmount, False, True, True)
            .AddAgButtonColumn(Dgl2, Col2BtnItemDetail, 35, Col2BtnItemDetail, False, False)
            .AddAgButtonColumn(Dgl2, Col2BtnAdjDetail, 35, Col2BtnAdjDetail, False, False)
            .AddAgTextColumn(Dgl2, Col2SettlementRemark, 100, 0, Col2SettlementRemark, True, False, False)
            .AddAgNumberColumn(Dgl2, Col2AdjustedAmount, 90, 8, 2, False, Col2AdjustedAmount, False, True, False)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 35
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AllowUserToOrderColumns = True
        AgL.GridDesign(Dgl2)
        Dgl2.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Bold)
        Dgl2.DefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Regular)
        Dgl2.MultiSelect = True


        Dgl4.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl4, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl4, Col2InvoiceNo, 90, 0, Col2InvoiceNo, True, True, False)
            .AddAgTextColumn(Dgl4, Col2InvoiceSr, 90, 0, Col2InvoiceSr, False, True, False)
            .AddAgDateColumn(Dgl4, Col2InvoiceDate, 90, Col2InvoiceDate, True, True)
            .AddAgNumberColumn(Dgl4, Col2TaxableAmount, 80, 8, 2, False, Col2TaxableAmount, True, True, True)
            .AddAgNumberColumn(Dgl4, Col2InvoiceAmount, 80, 8, 2, False, Col2InvoiceAmount, True, True, True)
            .AddAgNumberColumn(Dgl4, Col2SettlementAddition, 80, 8, 2, False, Col2SettlementAddition, False, True, True)
            .AddAgNumberColumn(Dgl4, Col2SettlementDeduction, 80, 8, 2, False, Col2SettlementDeduction, False, True, True)
            .AddAgNumberColumn(Dgl4, Col2ItemDeductions, 80, 8, 2, False, Col2ItemDeductions, False, True, True)
            .AddAgNumberColumn(Dgl4, Col2SettlementInvoiceAmount, 80, 8, 2, False, Col2SettlementInvoiceAmount, False, True, True)
            .AddAgButtonColumn(Dgl4, Col2BtnItemDetail, 35, Col2BtnItemDetail, False, False)
            .AddAgButtonColumn(Dgl4, Col2BtnAdjDetail, 35, Col2BtnAdjDetail, False, False)
            .AddAgTextColumn(Dgl4, Col2SettlementRemark, 90, 0, Col2SettlementRemark, True, False, False)
            .AddAgNumberColumn(Dgl4, Col2AdjustedAmount, 90, 8, 2, False, Col2AdjustedAmount, False, True, False)
            .AddAgTextColumn(Dgl4, Col2PInvoiceNo, 90, 0, Col2PInvoiceNo, True, True, False)
            .AddAgNumberColumn(Dgl4, Col2PWInvoiceAmount, 90, 8, 2, False, Col2PWInvoiceAmount, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl4, Pnl4)
        Dgl4.EnableHeadersVisualStyles = False
        Dgl4.ColumnHeadersHeight = 35
        Dgl4.AgSkipReadOnlyColumns = True
        Dgl4.AllowUserToOrderColumns = True
        AgL.GridDesign(Dgl4)
        Dgl4.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Bold)
        Dgl4.DefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Regular)
        Dgl4.MultiSelect = True


        AgCL.GridSetiingShowXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2, False)



        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl3, Col3Select, 34, 0, Col3Select, True, True, False)
            .AddAgTextColumn(Dgl3, Col3PaymentNo, 100, 0, Col3PaymentNo, True, True, False)
            .AddAgTextColumn(Dgl3, Col3PaymentSr, 50, 0, Col3PaymentSr, False, True, False)
            .AddAgDateColumn(Dgl3, Col3PaymentDate, 85, Col3PaymentDate, True, True)
            .AddAgTextColumn(Dgl3, Col3Subcode, 135, 0, Col3Subcode, True, True, False)
            .AddAgNumberColumn(Dgl3, Col3Amount, 90, 8, 2, False, Col3Amount, True, True, False)
            .AddAgNumberColumn(Dgl3, Col3PaymentRemark, 90, 8, 2, False, Col3PaymentRemark, True, True, False)
            .AddAgNumberColumn(Dgl3, Col3AdjustedAmount, 90, 8, 2, False, Col3AdjustedAmount, False, True, False)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 35
        Dgl3.AgSkipReadOnlyColumns = True
        Dgl3.AllowUserToOrderColumns = True
        AgL.GridDesign(Dgl3)
        Dgl3.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Bold)
        Dgl3.DefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Regular)
        Dgl3.MultiSelect = True
        Dgl3.Columns(Col3Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)



        Dgl5.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl5, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl5, Col3Select, 34, 0, Col3Select, True, True, False)
            .AddAgTextColumn(Dgl5, Col3PaymentNo, 100, 0, Col3PaymentNo, True, True, False)
            .AddAgTextColumn(Dgl5, Col3PaymentSr, 50, 0, Col3PaymentSr, False, True, False)
            .AddAgDateColumn(Dgl5, Col3PaymentDate, 85, Col3PaymentDate, True, True)
            .AddAgTextColumn(Dgl5, Col3Subcode, 135, 0, Col3Subcode, True, True, False)
            .AddAgNumberColumn(Dgl5, Col3Amount, 90, 8, 2, False, Col3Amount, True, True, False)
            .AddAgNumberColumn(Dgl5, Col3PaymentRemark, 90, 8, 2, False, Col3PaymentRemark, True, True, False)
            .AddAgNumberColumn(Dgl5, Col3AdjustedAmount, 90, 8, 2, False, Col3AdjustedAmount, False, True, False)
        End With
        AgL.AddAgDataGrid(Dgl5, Pnl5)
        Dgl5.EnableHeadersVisualStyles = False
        Dgl5.ColumnHeadersHeight = 35
        Dgl5.AgSkipReadOnlyColumns = True
        Dgl5.AllowUserToOrderColumns = True
        AgL.GridDesign(Dgl5)
        Dgl5.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Bold)
        Dgl5.DefaultCellStyle.Font = New Font("Verdana", 8, FontStyle.Regular)
        Dgl5.MultiSelect = True
        Dgl5.Columns(Col3Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)

        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Dgl2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        Dgl4.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Dgl5.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple


        AgCL.GridSetiingShowXml(Me.Text & Dgl3.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl3, False)
    End Sub
    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, J As Integer, mSr As Integer
        Dim DtTemp As DataTable
        Dim mLedgerPostingData As String = ""
        Dim mTotalAmount As Double
        Dim mNarration As String
        Dim mInvoiceList As String = ""

        mQry = "UPDATE LedgerHead 
                SET 
                ManualRefNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", 
                SubCode = " & AgL.Chk_Text(TxtParty.Tag) & ", 
                LinkedSubCode = " & AgL.Chk_Text(TxtLinkedParty.Tag) & ", 
                UptoDate = " & AgL.Chk_Date(TxtUptoDate.Text) & ", 
                DrCr = " & AgL.Chk_Text(IIf(TxtDrCr.Text = "Debit", "Dr", "Cr")) & ", 
                Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " 
                Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        'mQry = "Delete From LedgerHeadDetail Where DocId = '" & SearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "Delete From Cloth_SupplierSettlementInvoices Where DocID ='" & mSearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "Delete From Cloth_SupplierSettlementPayments Where DocID ='" & mSearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerAdj Where ReferenceDocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "Delete From TransactionReferences Where ReferenceDocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)




        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From LedgerHeadDetail  Where DocID = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Subcode, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1

                    If AgL.XNull(Dgl1.Item(Col1DrCr, I).Value) = "Cr" Then
                        mQry = "Insert Into LedgerHeadDetail (DocID, Sr, Subcode, AmountCr, ChqRefNo, ChqRefDate, Remarks)
                            Values ('" & mSearchCode & "', " & mSr & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ",
                            " & Val(Dgl1.Item(Col1Amount, I).Value) & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Value) & ",
                            " & AgL.Chk_Date(Dgl1.Item(Col1ChqRefDate, I).Value) & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    Else
                        mQry = "Insert Into LedgerHeadDetail (DocID, Sr, Subcode, Amount, ChqRefNo, ChqRefDate, Remarks)
                            Values ('" & mSearchCode & "', " & mSr & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ",
                            " & Val(Dgl1.Item(Col1Amount, I).Value) & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Value) & ",
                            " & AgL.Chk_Date(Dgl1.Item(Col1ChqRefDate, I).Value) & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If AgL.XNull(Dgl1.Item(Col1DrCr, I).Value) = "Cr" Then
                            mQry = "Update LedgerHeadDetail Set
                                Subcode =" & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ",
                                AmountCr =" & Val(Dgl1.Item(Col1Amount, I).Value) & ",
                                Amount =0,
                                ChqRefNo =" & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Value) & ",
                                ChqRefDate=" & AgL.Chk_Date(Dgl1.Item(Col1ChqRefDate, I).Value) & ",
                                Remarks=" & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & "
                                Where DocID = '" & mSearchCode & "'
                                And Sr = " & Dgl1.Item(ColSNo, I).Tag & " 
                                "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        Else
                            mQry = "Update LedgerHeadDetail Set
                                Subcode =" & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ",
                                Amount =" & Val(Dgl1.Item(Col1Amount, I).Value) & ",
                                AmountCr =0,
                                ChqRefNo =" & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Value) & ",
                                ChqRefDate=" & AgL.Chk_Date(Dgl1.Item(Col1ChqRefDate, I).Value) & ",
                                Remarks=" & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & "
                                Where DocID = '" & mSearchCode & "'
                                And Sr = " & Dgl1.Item(ColSNo, I).Tag & " 
                                "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        mQry = "Delete From LedgerHeadDetail Where DocId = '" & SearchCode & "' and Sr =" & Dgl1.Item(ColSNo, I).Tag & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If

                If Dgl1.Rows(I).Visible = True Then
                    If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                    mNarration = "Payment Settlement for Party " & TxtParty.Text

                    'If TxtDrCr.Text = "Credit" Then
                    If AgL.XNull(Dgl1.Item(Col1DrCr, I).Value) = "Cr" Then
                        mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Val(Dgl1.Item(Col1Amount, I).Value) & " as AmtCr, '" & mNarration & "' as Narration, " & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Tag) & " ChqNo, " & AgL.Chk_Date(Dgl1.Item(Col1ChqRefDate, I).Tag) & " ChqDate "
                    Else
                        mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, " & Val(Dgl1.Item(Col1Amount, I).Value) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, " & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Tag) & " ChqNo, " & AgL.Chk_Date(Dgl1.Item(Col1ChqRefDate, I).Tag) & " ChqDate "
                    End If
                    'Else
                    '    mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & " as Subcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, " & Val(Dgl1.Item(Col1Amount, I).Value) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, " & AgL.Chk_Text(Dgl1.Item(Col1ChqRefNo, I).Tag) & " ChqNo, " & AgL.Chk_Text(Dgl1.Item(Col1ChqRefDate, I).Tag) & " ChqDate "
                    'End If
                    If (TxtDrCr.Text = "Credit" And AgL.XNull(Dgl1.Item(Col1DrCr, I).Value) = "Cr") Or (TxtDrCr.Text = "Debit" And AgL.XNull(Dgl1.Item(Col1DrCr, I).Value) = "Dr") Then
                        mTotalAmount += Val(Dgl1.Item(Col1Amount, I).Value)
                    Else
                        mTotalAmount -= Val(Dgl1.Item(Col1Amount, I).Value)
                    End If
                End If
            End If
        Next

        If LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.ReceiptSettlement Then
            If Val(LblDifferenceAmount_A.Text) > 0 Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text("SUSPENSE") & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, " & Val(LblDifferenceAmount_A.Text) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, 'A' ChqNo, Null as ChqDate "
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, " & AgL.Chk_Text("SUSPENSE") & " as ContraAc, 0 as AmtDr, " & Val(LblDifferenceAmount_A.Text) & " as AmtCr, '" & mNarration & "' as Narration, 'A' ChqNo, Null as ChqDate "
            ElseIf Val(LblDifferenceAmount_A.Text) < 0 Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text("SUSPENSE") & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Math.Abs(Val(LblDifferenceAmount_A.Text)) & " as AmtCr, '" & mNarration & "' as Narration, 'A' ChqNo, Null as ChqDate "
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, " & AgL.Chk_Text("SUSPENSE") & " as ContraAc, " & Math.Abs(Val(LblDifferenceAmount_A.Text)) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, 'A' ChqNo, Null as ChqDate "
            End If

            If Val(LblDifferenceAmount_W.Text) > 0 Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text("SUSPENSE") & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, " & Val(LblDifferenceAmount_W.Text) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, 'W' ChqNo, Null as ChqDate "
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select  " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, " & AgL.Chk_Text("SUSPENSE") & " as ContraAc, 0 as AmtDr, " & Val(LblDifferenceAmount_W.Text) & " as AmtCr, '" & mNarration & "' as Narration, 'W' ChqNo, Null as ChqDate "
                'mTotalAmount += Val(LblDifferenceAmount_W.Text)
            ElseIf Val(LblDifferenceAmount_W.Text) < 0 Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text("SUSPENSE") & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Math.Abs(Val(LblDifferenceAmount_W.Text)) & " as AmtCr, '" & mNarration & "' as Narration, 'W' ChqNo, Null as ChqDate "
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select  " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, " & AgL.Chk_Text("SUSPENSE") & " as ContraAc, " & Math.Abs(Val(LblDifferenceAmount_W.Text)) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, 'W' ChqNo, Null as ChqDate "
            End If
        Else
            If Val(LblDifferenceAmount_A.Text) > 0 Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text("SUSPENSE") & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Val(LblDifferenceAmount_A.Text) & " as AmtCr, '" & mNarration & "' as Narration, 'A' ChqNo, Null as ChqDate "
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, " & AgL.Chk_Text("SUSPENSE") & " as ContraAc, " & Val(LblDifferenceAmount_A.Text) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, 'A' ChqNo, Null as ChqDate "
                'mTotalAmount += Val(LblDifferenceAmount_A.Text)
            ElseIf Val(LblDifferenceAmount_A.Text) < 0 Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text("SUSPENSE") & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, " & Math.Abs(Val(LblDifferenceAmount_A.Text)) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, 'A' ChqNo, Null as ChqDate "
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, " & AgL.Chk_Text("SUSPENSE") & " as ContraAc, 0 as AmtDr, " & Val(LblDifferenceAmount_A.Text) & " as AmtCr, '" & mNarration & "' as Narration, 'A' ChqNo, Null as ChqDate "
                'mTotalAmount += Val(LblDifferenceAmount_A.Text)
            End If

            If Val(LblDifferenceAmount_W.Text) > 0 Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text("SUSPENSE") & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Val(LblDifferenceAmount_W.Text) & " as AmtCr, '" & mNarration & "' as Narration, 'W' ChqNo, Null as ChqDate "
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, " & AgL.Chk_Text("SUSPENSE") & " as ContraAc, " & Val(LblDifferenceAmount_W.Text) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, 'W' ChqNo, Null as ChqDate "
                'mTotalAmount += Val(LblDifferenceAmount_A.Text)
            ElseIf Val(LblDifferenceAmount_W.Text) > 0 Then
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text("SUSPENSE") & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, " & Math.Abs(Val(LblDifferenceAmount_W.Text)) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, 'W' ChqNo, Null as ChqDate "
                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, " & AgL.Chk_Text("SUSPENSE") & " as ContraAc, 0 as AmtDr, " & Val(LblDifferenceAmount_W.Text) & " as AmtCr, '" & mNarration & "' as Narration, 'W' ChqNo, Null as ChqDate "
                'mTotalAmount += Val(LblDifferenceAmount_A.Text)
            End If
        End If


        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From Cloth_SupplierSettlementInvoices  Where DocID = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar)
        For I = 0 To Dgl2.RowCount - 1
            If Dgl2.Item(Col2InvoiceNo, I).Value <> "" Then
                If Dgl2.Item(ColSNo, I).Tag Is Nothing And Dgl2.Rows(I).Visible = True Then

                    mSr += 1

                    mQry = "Insert Into Cloth_SupplierSettlementInvoices (DocID, Sr, PurchaseInvoiceDocID, PurchaseInvoiceDocIDSr, InvoiceAmount, LineDeduction, SettlementAddition, 
                            SettlementDeduction, SettlementInvoiceAmount,SettlementRemark,AdjustedAmount, Tags)
                            Values ('" & (mSearchCode) & "', " & mSr & ",
                            " & AgL.Chk_Text(Dgl2.Item(Col2InvoiceNo, I).Tag) & ",
                            " & AgL.Chk_Text(Dgl2.Item(Col2InvoiceSr, I).Value) & ",
                            " & Val(Dgl2.Item(Col2InvoiceAmount, I).Value) & ",
                            " & Val(Dgl2.Item(Col2ItemDeductions, I).Value) & ",
                            " & Val(Dgl2.Item(Col2SettlementAddition, I).Value) & ",
                            " & Val(Dgl2.Item(Col2SettlementDeduction, I).Value) & ",
                            " & Val(Dgl2.Item(Col2SettlementInvoiceAmount, I).Value) & ",
                            " & AgL.Chk_Text(Dgl2.Item(Col2SettlementRemark, I).Value) & ",
                            " & Val(Dgl2.Item(Col2AdjustedAmount, I).Value) & ", '+A')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                    If Dgl2.Item(Col2BtnItemDetail, I).Tag IsNot Nothing Then
                        CType(Dgl2.Item(Col2BtnItemDetail, I).Tag, FrmPartyAcSettlementInvoiceLine).FSave(mSearchCode, mSr, Conn, Cmd)
                    End If

                    If Dgl2.Item(Col2BtnAdjDetail, I).Tag IsNot Nothing Then
                        CType(Dgl2.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).FSave(mSearchCode, mSr, Conn, Cmd)
                    End If

                    mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, ReferenceSr, Remark) 
                            Values (" & AgL.Chk_Text(Dgl2.Item(Col2InvoiceNo, I).Tag) & ", '" & mSearchCode & "', " & mSr & ", 'Supplier Settlement Entry No." & TxtReferenceNo.Text & " dated " & TxtV_Date.Text & " is done for this invoice. Can not Modify / Delete.') "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    If Dgl2.Rows(I).Visible = True Then
                        mQry = "Update Cloth_SupplierSettlementInvoices Set
                            PurchaseInvoiceDocId = " & AgL.Chk_Text(Dgl2.Item(Col2InvoiceNo, I).Tag) & ",
                            InvoiceAmount = " & Val(Dgl2.Item(Col2InvoiceAmount, I).Value) & ",
                            LineDeduction = " & Val(Dgl2.Item(Col2ItemDeductions, I).Value) & ",
                            SettlementAddition = " & Val(Dgl2.Item(Col2SettlementAddition, I).Value) & ",
                            SettlementDeduction = " & Val(Dgl2.Item(Col2SettlementDeduction, I).Value) & ",
                            SettlementInvoiceAmount = " & Val(Dgl2.Item(Col2SettlementInvoiceAmount, I).Value) & ",
                            SettlementRemark = " & AgL.Chk_Text(Dgl2.Item(Col2SettlementRemark, I).Value) & ",
                            AdjustedAmount = " & Val(Dgl2.Item(Col2AdjustedAmount, I).Value) & "                                
                            Where DocID = '" & mSearchCode & "'
                            And Sr = " & Dgl2.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        If Dgl2.Item(Col2BtnItemDetail, I).Tag IsNot Nothing Then
                            CType(Dgl2.Item(Col2BtnItemDetail, I).Tag, FrmPartyAcSettlementInvoiceLine).FSave(mSearchCode, Val(Dgl2.Item(ColSNo, I).Tag), Conn, Cmd)
                        End If

                        If Dgl2.Item(Col2BtnAdjDetail, I).Tag IsNot Nothing Then
                            CType(Dgl2.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).FSave(mSearchCode, Val(Dgl2.Item(ColSNo, I).Tag), Conn, Cmd)
                        End If

                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, ReferenceSr, Remark) 
                            Values (" & AgL.Chk_Text(Dgl2.Item(Col2InvoiceNo, I).Tag) & ", '" & mSearchCode & "'," & AgL.Chk_Text(Dgl2.Item(ColSNo, I).Tag) & ", 'Supplier Settlement Entry No." & TxtReferenceNo.Text & " dated " & TxtV_Date.Text & " is done for this invoice. Can not Modify / Delete.') "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        mQry = "Delete From Cloth_SupplierSettlementInvoicesLine Where DocId = '" & SearchCode & "' and TSr =" & Val(Dgl2.Item(ColSNo, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        mQry = "Delete From Cloth_SupplierSettlementInvoicesAdjustment Where DocId = '" & SearchCode & "' and TSr =" & Val(Dgl2.Item(ColSNo, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        mQry = "Delete From Cloth_SupplierSettlementInvoices Where DocId = '" & SearchCode & "' and Sr =" & Val(Dgl2.Item(ColSNo, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If




                If Dgl2.Rows(I).Visible = True Then
                    Dim obj As FrmPartyAcSettlementInvoiceAdj

                    mInvoiceList += IIf(mInvoiceList = "", "", ", ")
                    mInvoiceList += Dgl2.Item(Col2InvoiceNo, I).Value

                    If Dgl2.Item(Col2BtnAdjDetail, I).Tag IsNot Nothing Then
                        obj = Dgl2.Item(Col2BtnAdjDetail, I).Tag
                        For J = 0 To obj.Dgl1.Rows.Count - 1
                            If obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Head, J).Value <> "" And Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) > 0 Then
                                If Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Rate, J).Value) > 0 Then
                                    mNarration = "Payment Settlement for Party " & TxtParty.Text & ", " & obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Head, J).Value & " Availed at " & obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1RateCalculationType, J).Value & " of " & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Rate, J).Value) & ".  Amount of Rs." & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) & ""
                                Else
                                    mNarration = "Payment Settlement for Party " & TxtParty.Text & ", " & obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Head, J).Value & " Amount of Rs." & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) & ""
                                End If

                                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                                If obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1AdditionDeduction, J).Value.ToString.ToUpper = "DEDUCTION" Then
                                    mLedgerPostingData += " Select " & AgL.Chk_Text(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1PostingAc, J).Value) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) & " as AmtCr, '" & mNarration & "' as Narration, Null ChqNo, Null ChqDate  "
                                    mTotalAmount += Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value)
                                Else
                                    mLedgerPostingData += " Select " & AgL.Chk_Text(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1PostingAc, J).Value) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, " & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, Null ChqNo, Null ChqDate  "
                                    mTotalAmount -= Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value)
                                End If
                            End If
                        Next
                    End If

                    If Val(Dgl2.Item(Col2ItemDeductions, I).Value) > 0 Then
                        If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                        mNarration = "Payment Settlement for Party " & TxtParty.Text & ", Item Deductions : " & Format(Val(Dgl2.Item(Col2ItemDeductions, I).Value), "0.00")

                        mLedgerPostingData += " Select " & AgL.Chk_Text(DtSettings.Rows(0)("ItemDeductionPostingAc")) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Val(Dgl2.Item(Col2ItemDeductions, I).Value) & " as AmtCr, '" & mNarration & "', Null ChqNo, Null ChqDate "
                        mTotalAmount += Val(Dgl2.Item(Col2ItemDeductions, I).Value)
                    End If
                End If
            End If
        Next






        For I = 0 To Dgl4.RowCount - 1
            If Dgl4.Item(Col2InvoiceNo, I).Value <> "" Then
                If Dgl4.Item(ColSNo, I).Tag Is Nothing And Dgl4.Rows(I).Visible = True Then

                    mSr += 1

                    mQry = "Insert Into Cloth_SupplierSettlementInvoices (DocID, Sr, PurchaseInvoiceDocID, PurchaseInvoiceDocIDSr, InvoiceAmount, LineDeduction, SettlementAddition, 
                            SettlementDeduction, SettlementInvoiceAmount,SettlementRemark,AdjustedAmount,Tags,Remarks1,Remarks2)
                            Values ('" & (mSearchCode) & "', " & mSr & ",
                            " & AgL.Chk_Text(Dgl4.Item(Col2InvoiceNo, I).Tag) & ",
                            " & AgL.Chk_Text(Dgl4.Item(Col2InvoiceSr, I).Value) & ",
                            " & Val(Dgl4.Item(Col2InvoiceAmount, I).Value) & ",
                            " & Val(Dgl4.Item(Col2ItemDeductions, I).Value) & ",
                            " & Val(Dgl4.Item(Col2SettlementAddition, I).Value) & ",
                            " & Val(Dgl4.Item(Col2SettlementDeduction, I).Value) & ",
                            " & Val(Dgl4.Item(Col2SettlementInvoiceAmount, I).Value) & ",
                            " & AgL.Chk_Text(Dgl4.Item(Col2SettlementRemark, I).Value) & ",
                            " & Val(Dgl4.Item(Col2AdjustedAmount, I).Value) & ",'+W', " & AgL.Chk_Text(Dgl4.Item(Col2PInvoiceNo, I).Value) & ", " & AgL.Chk_Text(Dgl4.Item(Col2PWInvoiceAmount, I).Value) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                    If Dgl4.Item(Col2BtnItemDetail, I).Tag IsNot Nothing Then
                        CType(Dgl4.Item(Col2BtnItemDetail, I).Tag, FrmPartyAcSettlementInvoiceLine).FSave(mSearchCode, mSr, Conn, Cmd)
                    End If

                    If Dgl4.Item(Col2BtnAdjDetail, I).Tag IsNot Nothing Then
                        CType(Dgl4.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).FSave(mSearchCode, mSr, Conn, Cmd)
                    End If

                    mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, ReferenceSr, Remark) 
                            Values (" & AgL.Chk_Text(Dgl4.Item(Col2InvoiceNo, I).Tag) & ", '" & mSearchCode & "', " & mSr & ", 'Supplier Settlement Entry No." & TxtReferenceNo.Text & " dated " & TxtV_Date.Text & " is done for this invoice. Can not Modify / Delete.') "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    If Dgl4.Rows(I).Visible = True Then
                        mQry = "Update Cloth_SupplierSettlementInvoices Set
                            PurchaseInvoiceDocId = " & AgL.Chk_Text(Dgl4.Item(Col2InvoiceNo, I).Tag) & ",
                            InvoiceAmount = " & Val(Dgl4.Item(Col2InvoiceAmount, I).Value) & ",
                            LineDeduction = " & Val(Dgl4.Item(Col2ItemDeductions, I).Value) & ",
                            SettlementAddition = " & Val(Dgl4.Item(Col2SettlementAddition, I).Value) & ",
                            SettlementDeduction = " & Val(Dgl4.Item(Col2SettlementDeduction, I).Value) & ",
                            SettlementInvoiceAmount = " & Val(Dgl4.Item(Col2SettlementInvoiceAmount, I).Value) & ",
                            SettlementRemark = " & AgL.Chk_Text(Dgl4.Item(Col2SettlementRemark, I).Value) & ",
                            AdjustedAmount = " & Val(Dgl4.Item(Col2AdjustedAmount, I).Value) & "                                
                            Where DocID = '" & mSearchCode & "'
                            And Sr = " & Dgl4.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        If Dgl4.Item(Col2BtnItemDetail, I).Tag IsNot Nothing Then
                            CType(Dgl4.Item(Col2BtnItemDetail, I).Tag, FrmPartyAcSettlementInvoiceLine).FSave(mSearchCode, Val(Dgl4.Item(ColSNo, I).Tag), Conn, Cmd)
                        End If

                        If Dgl4.Item(Col2BtnAdjDetail, I).Tag IsNot Nothing Then
                            CType(Dgl4.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).FSave(mSearchCode, Val(Dgl4.Item(ColSNo, I).Tag), Conn, Cmd)
                        End If

                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, ReferenceSr, Remark) 
                            Values (" & AgL.Chk_Text(Dgl4.Item(Col2InvoiceNo, I).Tag) & ", '" & mSearchCode & "'," & AgL.Chk_Text(Dgl4.Item(ColSNo, I).Tag) & ", 'Supplier Settlement Entry No." & TxtReferenceNo.Text & " dated " & TxtV_Date.Text & " is done for this invoice. Can not Modify / Delete.') "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        mQry = "Delete From Cloth_SupplierSettlementInvoicesLine Where DocId = '" & SearchCode & "' and TSr =" & Val(Dgl4.Item(ColSNo, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        mQry = "Delete From Cloth_SupplierSettlementInvoicesAdjustment Where DocId = '" & SearchCode & "' and TSr =" & Val(Dgl4.Item(ColSNo, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        mQry = "Delete From Cloth_SupplierSettlementInvoices Where DocId = '" & SearchCode & "' and Sr =" & Val(Dgl4.Item(ColSNo, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If




                If Dgl4.Rows(I).Visible = True Then
                    Dim obj As FrmPartyAcSettlementInvoiceAdj

                    mInvoiceList += IIf(mInvoiceList = "", "", ", ")
                    mInvoiceList += Dgl4.Item(Col2InvoiceNo, I).Value

                    If Dgl4.Item(Col2BtnAdjDetail, I).Tag IsNot Nothing Then
                        obj = Dgl4.Item(Col2BtnAdjDetail, I).Tag
                        For J = 0 To obj.Dgl1.Rows.Count - 1
                            If obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Head, J).Value <> "" And Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) > 0 Then
                                If Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Rate, J).Value) > 0 Then
                                    mNarration = "Payment Settlement for Party " & TxtParty.Text & ", " & obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Head, J).Value & " Availed at " & obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1RateCalculationType, J).Value & " of " & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Rate, J).Value) & ".  Amount of Rs." & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) & ""
                                Else
                                    mNarration = "Payment Settlement for Party " & TxtParty.Text & ", " & obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Head, J).Value & " Amount of Rs." & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) & ""
                                End If

                                If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                                If obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1AdditionDeduction, J).Value.ToString.ToUpper = "DEDUCTION" Then
                                    mLedgerPostingData += " Select " & AgL.Chk_Text(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1PostingAc, J).Value) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) & " as AmtCr, '" & mNarration & "' as Narration, Null ChqNo, Null ChqDate  "
                                    mTotalAmount += Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value)
                                Else
                                    mLedgerPostingData += " Select " & AgL.Chk_Text(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1PostingAc, J).Value) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, " & Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, Null ChqNo, Null ChqDate  "
                                    mTotalAmount -= Val(obj.Dgl1.Item(FrmPartyAcSettlementInvoiceAdj.Col1Amount, J).Value)
                                End If
                            End If
                        Next
                    End If

                    If Val(Dgl4.Item(Col2ItemDeductions, I).Value) > 0 Then
                        If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                        mNarration = "Payment Settlement for Party " & TxtParty.Text & ", Item Deductions : " & Format(Val(Dgl4.Item(Col2ItemDeductions, I).Value), "0.00")

                        mLedgerPostingData += " Select " & AgL.Chk_Text(DtSettings.Rows(0)("ItemDeductionPostingAc")) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtParty.Tag) & " as ContraAc, 0 as AmtDr, " & Val(Dgl4.Item(Col2ItemDeductions, I).Value) & " as AmtCr, '" & mNarration & "', Null ChqNo, Null ChqDate "
                        mTotalAmount += Val(Dgl4.Item(Col2ItemDeductions, I).Value)
                    End If
                End If
            End If
        Next



        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From Cloth_SupplierSettlementPayments  Where DocID = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar)
        For I = 0 To Dgl3.RowCount - 1
            If Dgl3.Item(Col3PaymentNo, I).Value <> "" Then
                If Dgl3.Item(ColSNo, I).Tag Is Nothing And Dgl3.Rows(I).Visible = True Then
                    If Dgl3.Item(Col3Select, I).Value = "þ" Then
                        mSr += 1
                        mQry = "Insert Into Cloth_SupplierSettlementPayments (DocID, Sr, PaymentDocID, PaymentDocIdSr, PaidAmount, AdjustedAmount,Tags)
                            Values ('" & mSearchCode & "', " & mSr & ",
                            " & AgL.Chk_Text(Dgl3.Item(Col3PaymentNo, I).Tag) & ",
                            " & AgL.Chk_Text(Dgl3.Item(Col3PaymentSr, I).Value) & ",
                            " & Val(Dgl3.Item(Col3Amount, I).Value) & ",
                            " & Val(Dgl3.Item(Col3AdjustedAmount, I).Value) & ",'+A')"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, Remark) 
                            Values (" & AgL.Chk_Text(Dgl3.Item(Col3PaymentNo, I).Tag) & ", '" & mSearchCode & "', 'Supplier Settlement Entry No." & TxtReferenceNo.Text & " dated " & TxtV_Date.Text & " is done for this Payment. Can not Modify / Delete.') "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    End If
                Else
                    If Dgl3.Rows(I).Visible = True And Dgl3.Item(Col3Select, I).Value = "þ" Then
                        mQry = "Update Cloth_SupplierSettlementPayments Set
                            PaymentDocId = " & AgL.Chk_Text(Dgl3.Item(Col3PaymentNo, I).Tag) & ",
                            PaymentDocIdSr = " & AgL.Chk_Text(Dgl3.Item(Col3PaymentSr, I).Value) & ",
                            PaidAmount = " & Val(Dgl3.Item(Col3Amount, I).Value) & ",
                            AdjustedAmount = " & Val(Dgl3.Item(Col3AdjustedAmount, I).Value) & "
                            Where DocID = '" & mSearchCode & "'
                            And Sr = " & Dgl3.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, Remark) 
                            Values (" & AgL.Chk_Text(Dgl3.Item(Col3PaymentNo, I).Tag) & ", '" & mSearchCode & "', 'Supplier Settlement Entry No." & TxtReferenceNo.Text & " dated " & TxtV_Date.Text & " is done for this Payment. Can not Modify / Delete.') "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        mQry = "Delete From Cloth_SupplierSettlementPayments Where DocId = '" & SearchCode & "' and Sr =" & Dgl3.Item(ColSNo, I).Tag & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next



        For I = 0 To Dgl5.RowCount - 1
            If Dgl5.Item(Col3PaymentNo, I).Value <> "" Then
                If Dgl5.Item(ColSNo, I).Tag Is Nothing And Dgl5.Rows(I).Visible = True Then
                    If Dgl5.Item(Col3Select, I).Value = "þ" Then
                        mSr += 1
                        mQry = "Insert Into Cloth_SupplierSettlementPayments (DocID, Sr, PaymentDocID, PaymentDocIdSr, PaidAmount, AdjustedAmount,Tags)
                            Values ('" & mSearchCode & "', " & mSr & ",
                            " & AgL.Chk_Text(Dgl5.Item(Col3PaymentNo, I).Tag) & ",
                            " & AgL.Chk_Text(Dgl5.Item(Col3PaymentSr, I).Value) & ",
                            " & Val(Dgl5.Item(Col3Amount, I).Value) & ",
                            " & Val(Dgl5.Item(Col3AdjustedAmount, I).Value) & ",'+W')"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, Remark) 
                            Values (" & AgL.Chk_Text(Dgl5.Item(Col3PaymentNo, I).Tag) & ", '" & mSearchCode & "', 'Supplier Settlement Entry No." & TxtReferenceNo.Text & " dated " & TxtV_Date.Text & " is done for this Payment. Can not Modify / Delete.') "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    End If
                Else
                    If Dgl5.Rows(I).Visible = True And Dgl5.Item(Col3Select, I).Value = "þ" Then
                        mQry = "Update Cloth_SupplierSettlementPayments Set
                            PaymentDocId = " & AgL.Chk_Text(Dgl5.Item(Col3PaymentNo, I).Tag) & ",
                            PaymentDocIdSr = " & AgL.Chk_Text(Dgl5.Item(Col3PaymentSr, I).Value) & ",
                            PaidAmount = " & Val(Dgl5.Item(Col3Amount, I).Value) & ",
                            AdjustedAmount = " & Val(Dgl5.Item(Col3AdjustedAmount, I).Value) & "
                            Where DocID = '" & mSearchCode & "'
                            And Sr = " & Dgl5.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, Remark) 
                            Values (" & AgL.Chk_Text(Dgl5.Item(Col3PaymentNo, I).Tag) & ", '" & mSearchCode & "', 'Supplier Settlement Entry No." & TxtReferenceNo.Text & " dated " & TxtV_Date.Text & " is done for this Payment. Can not Modify / Delete.') "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        mQry = "Delete From Cloth_SupplierSettlementPayments Where DocId = '" & SearchCode & "' and Sr =" & Dgl5.Item(ColSNo, I).Tag & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next


        If mTotalAmount > 0 Then
            If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
            mNarration = "Payment Settlement for Invoices : " & mInvoiceList
            If TxtDrCr.Text = "Credit" Then
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, Null as ContraAc, " & mTotalAmount & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, Null as ChqNo, Null as ChqDate "
            Else
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, Null as ContraAc, 0 as AmtDr, " & mTotalAmount & " as AmtCr, '" & mNarration & "' as Narration, Null as ChqNo, Null as ChqDate "
            End If
        ElseIf mTotalAmount < 0 Then
            If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
            mNarration = "Payment Settlement for Invoices : " & mInvoiceList
            If TxtDrCr.Text = "Credit" Then
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, Null as ContraAc, 0 as AmtDr, " & Math.Abs(mTotalAmount) & " as AmtCr, '" & mNarration & "' as Narration, Null as ChqNo, Null as ChqDate "
            Else
                mLedgerPostingData += " Select " & AgL.Chk_Text(TxtParty.Tag) & " as Subcode, " & AgL.Chk_Text(TxtLinkedParty.Tag) & " as LinkedSubcode, Null as ContraAc, " & Math.Abs(mTotalAmount) & " as AmtDr, 0 as AmtCr, '" & mNarration & "' as Narration, Null as ChqNo, Null as ChqDate "
            End If
        End If



        If mLedgerPostingData <> "" Then
            mLedgerPostingData = "Select SubCode, LinkedSubcode, ContraAc, Narration, Sum(AmtDr)*1.0 as AmtDr, Sum(AmtCr)*1.0 as AmtCr, ChqNo, ChqDate 
                              From (" & mLedgerPostingData & ") as X Group By SubCode, ContraAc, Narration, ChqNo, ChqDate "
            DtTemp = AgL.FillData(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    If Val(AgL.VNull(DtTemp.Rows(I)("AmtDr"))) + Val(AgL.VNull(DtTemp.Rows(I)("AmtCr"))) = Val(LblDifferenceAmount_A.Text) And AgL.XNull(DtTemp.Rows(I)("ChqNo")) = "A" Then
                        mQry = "INSERT INTO Ledger
                        (DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, 
                        V_Date, SubCode, LinkedSubcode, ContraSub, AmtDr, AmtCr, 
                        Chq_No, Chq_Date, Narration, Site_Code, DivCode, 
                        U_Name, U_EntDt, U_AE)
                        VALUES('" & mSearchCode & "', " & I + 1 & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text("RS") & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                        " & AgL.Chk_Date(TxtV_Date.Text) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Subcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("LinkedSubcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ContraAc"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtDr"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtCr"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqNo"))) & "," & AgL.Chk_Date(AgL.XNull(DtTemp.Rows(I)("ChqDate"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Narration"))) & "," & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",
                        " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'A'
                        )"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        mQry = "INSERT INTO Ledger
                        (DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, 
                        V_Date, SubCode, LinkedSubcode, ContraSub, AmtDr, AmtCr, 
                        Chq_No, Chq_Date, Narration, Site_Code, DivCode, 
                        U_Name, U_EntDt, U_AE)
                        VALUES('" & mSearchCode & "', " & I + 1 & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                        " & AgL.Chk_Date(TxtV_Date.Text) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Subcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("LinkedSubcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ContraAc"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtDr"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtCr"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqNo"))) & "," & AgL.Chk_Date(AgL.XNull(DtTemp.Rows(I)("ChqDate"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Narration"))) & "," & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",
                        " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'A'
                        )"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Next
            End If
        End If

        If Val(LblDifferenceAmount_A.Text) <> 0 Then
            FPostDifferenceJV_A(mSearchCode, Conn, Cmd)
        End If

        If Val(LblDifferenceAmount_W.Text) <> 0 Then
            FPostDifferenceJV_W(mSearchCode, Conn, Cmd)
        End If

        SaveFifoAdjustment(Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Public Sub FPostDifferenceJV_A(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim mTrans As String = ""

        Dim mDebitAmt As Double
        Dim mCreditAmt As Double
        Dim mDebitNarration As String = ""
        Dim mCreditNarration As String = ""
        Dim mV_Type As String = "JV"
        Dim mRecId As String




        If Val(LblDifferenceAmount_A.Text) > 0 Then
            If TxtDifferenceJVDocNo_A.Text <> "" Then
                StrDocID = TxtDifferenceJVDocNo_A.Tag
                mRecId = AgL.Dman_Execute("Select ManualRefNo From LedgerHead With (NoLock) Where DocId = '" & StrDocID & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar

                mQry = "Delete From TransactionReferences Where DocID = '" & SearchCode & "' And ReferenceDocID = " & AgL.Chk_Text(TxtDifferenceJVDocNo_A.Tag) & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                mQry = "Delete From Ledger Where DocId = '" & StrDocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                mQry = "Delete From LedgerHeadDetail Where DocId = '" & StrDocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                mQry = "Delete From LedgerHead Where DocId = '" & StrDocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                mQry = "Delete From LedgerM Where DocId = '" & StrDocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Else
                'StrDocID = AgL.GetDocId(mV_Type, CStr(0), CDate(TxtV_Date.Text), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                StrDocID = AgL.CreateDocId(AgL, "LedgerHead", mV_Type, CStr(0), CDate(TxtV_Date.Text), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                mRecId = AgTemplate.ClsMain.FGetManualRefNo("RecId", "LedgerM", mV_Type, TxtV_Date.Text, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
            End If
            Dim mV_No As String = Val(AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))
            Dim mV_Prefix As String = AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)


            mQry = "Insert Into LedgerM(DocId,V_Type,v_Prefix,Site_Code, Div_Code,V_No,V_Date,SubCode,
                    Narration,PostedBy,RecId,
                    U_Name,U_EntDt,U_AE,PreparedBy) Values 
                    ('" & (StrDocID) & "','" & mV_Type & "','" & mV_Prefix & "','" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', 
                    '" & mV_No & "'," & AgL.Chk_Date(TxtV_Date.Text) & ",Null, 
                    Null,'" & AgL.PubUserName & "','" & mRecId & "',
                    '" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "',
                    'A','" & AgL.PubUserName & "')"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            mQry = "INSERT INTO LedgerHead (DocID, V_Type, V_Prefix, V_Date, V_No, 
                                            Div_Code, Site_Code, ManualRefNo, Subcode, PartyName, DrCr,
                                            UptoDate, Remarks, Status, SalesTaxGroupParty, PlaceOfSupply,                                            
                                            EntryBy, EntryDate, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate
                                           )
                       VALUES ('" & StrDocID & "', " & AgL.Chk_Text(mV_Type) & ", " & AgL.Chk_Text(mV_Prefix) & ", " & AgL.Chk_Date(TxtV_Date.Text) & ", " & Val(mV_No) & ",
                           " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ", " & AgL.Chk_Text(mRecId) & ", " & AgL.Chk_Text("") & ", " & AgL.Chk_Text("") & ", 'Dr',
                           Null, Null, Null, Null, Null,                           
                           " & AgL.Chk_Text(AgL.PubUserName) & "," & AgL.Chk_Date(AgL.PubLoginDate) & ", Null, Null, " & AgL.Chk_Text(AgL.PubUserName) & ",
                           " & AgL.Chk_Date(AgL.PubLoginDate) & "
                       );"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)



            mQry = "Insert Into LedgerHeadDetail (DocID, Sr, Subcode, Amount, Remarks, EffectiveDate,ReferenceDocID, ReferenceDocIdSr)
                            Values ('" & StrDocID & "', 1,
                            " & AgL.Chk_Text(TxtParty.Tag) & ",
                            " & Val(LblDifferenceAmount_A.Text) & ",
                            " & AgL.Chk_Text(TxtRemarks.Text) & ", Null, " & AgL.Chk_Text(SearchCode) & ", Null)"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            If LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.ReceiptSettlement Then
                mDebitAmt = Val(LblDifferenceAmount_A.Text)
                mCreditAmt = 0
                mDebitNarration = "Receipt Received"
                mCreditNarration = "Receipt Settlement of " & TxtParty.Text
            Else
                mDebitAmt = 0
                mCreditAmt = Val(LblDifferenceAmount_A.Text)
                mDebitNarration = "Payment Settlement of " & TxtParty.Text
                mCreditNarration = "Payment Settlement"
            End If


            mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode, LinkedSubcode,ContraSub,AmtDr,AmtCr,
                          Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,
                          System_Generated, EffectiveDate, ReferenceDocId, ReferenceDocIdSr) Values 
                          ('" & StrDocID & "','" & mRecId & "',1," & AgL.Chk_Date(TxtV_Date.Text) & "," & AgL.Chk_Text(TxtParty.Tag) & "," & AgL.Chk_Text(TxtLinkedParty.Tag) & "," & AgL.Chk_Text("SUSPENSE") & ", 
                          " & mDebitAmt & "," & mCreditAmt & ", 
                          " & AgL.Chk_Text(mDebitNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "',
                          " & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",'Y', Null, " & AgL.Chk_Text(SearchCode) & ", Null)"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode, LinkedSubcode,ContraSub,AmtDr,AmtCr,
                          Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,
                          System_Generated, EffectiveDate, ReferenceDocId, ReferenceDocIdSr) Values 
                          ('" & StrDocID & "','" & mRecId & "',2," & AgL.Chk_Date(TxtV_Date.Text) & "," & AgL.Chk_Text("SUSPENSE") & ", Null," & AgL.Chk_Text(TxtParty.Tag) & ", 
                          " & mCreditAmt & "," & mDebitAmt & ", 
                          " & AgL.Chk_Text(mCreditNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "',
                          " & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",'Y', Null, " & AgL.Chk_Text(SearchCode) & ", Null)"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            mQry = "INSERT INTO TransactionReferences(DocId, DocIDSr, ReferenceDocId, ReferenceSr, Type, Remark) 
                    Select '" & SearchCode & "', Null, '" & StrDocID & "', Null, " & AgL.Chk_Text(ClsMain.TransactionReferenceTypeConstants.SettlementDifference) & ",
                    " & AgL.Chk_Text(TxtRemarks.Text) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            mQry = "Update voucher_prefix set start_srl_no = " & Val(mV_No) & " 
                    where v_type = " & AgL.Chk_Text(mV_Type) & " 
                    and prefix=" & AgL.Chk_Text(mV_Prefix) & " 
                    And Site_Code = '" & TxtSite_Code.Tag & "' 
                    And Div_Code = '" & TxtDivision.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

    End Sub

    Public Sub FPostDifferenceJV_W(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim mTrans As String = ""

        Dim mDebitAmt As Double
        Dim mCreditAmt As Double
        Dim mDebitNarration As String = ""
        Dim mCreditNarration As String = ""
        Dim mV_Type As String = "WJV"
        Dim mRecId As String




        If Val(LblDifferenceAmount_W.Text) > 0 Then
            If TxtDifferenceJVDocNo_W.Text <> "" Then
                StrDocID = TxtDifferenceJVDocNo_W.Tag
                mRecId = AgL.Dman_Execute("Select ManualRefNo From LedgerHead With (NoLock) Where DocId = '" & StrDocID & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar

                mQry = "Delete From TransactionReferences Where DocID = '" & SearchCode & "' And ReferenceDocID = " & AgL.Chk_Text(TxtDifferenceJVDocNo_W.Tag) & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                mQry = "Delete From Ledger Where DocId = '" & StrDocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                mQry = "Delete From LedgerHeadDetail Where DocId = '" & StrDocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                mQry = "Delete From LedgerHead Where DocId = '" & StrDocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                mQry = "Delete From LedgerM Where DocId = '" & StrDocID & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Else
                'StrDocID = AgL.GetDocId(mV_Type, CStr(0), CDate(TxtV_Date.Text), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                StrDocID = AgL.CreateDocId(AgL, "LedgerHead", mV_Type, CStr(0), CDate(TxtV_Date.Text), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                mRecId = AgTemplate.ClsMain.FGetManualRefNo("RecId", "LedgerM", mV_Type, TxtV_Date.Text, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
            End If
            Dim mV_No As String = Val(AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))
            Dim mV_Prefix As String = AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)


            mQry = "Insert Into LedgerM(DocId,V_Type,v_Prefix,Site_Code, Div_Code,V_No,V_Date,SubCode,
                    Narration,PostedBy,RecId,
                    U_Name,U_EntDt,U_AE,PreparedBy) Values 
                    ('" & (StrDocID) & "','" & mV_Type & "','" & mV_Prefix & "','" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', 
                    '" & mV_No & "'," & AgL.Chk_Date(TxtV_Date.Text) & ",Null, 
                    Null,'" & AgL.PubUserName & "','" & mRecId & "',
                    '" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "',
                    'A','" & AgL.PubUserName & "')"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            mQry = "INSERT INTO LedgerHead (DocID, V_Type, V_Prefix, V_Date, V_No, 
                                            Div_Code, Site_Code, ManualRefNo, Subcode, PartyName, DrCr,
                                            UptoDate, Remarks, Status, SalesTaxGroupParty, PlaceOfSupply,                                            
                                            EntryBy, EntryDate, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate
                                           )
                       VALUES ('" & StrDocID & "', " & AgL.Chk_Text(mV_Type) & ", " & AgL.Chk_Text(mV_Prefix) & ", " & AgL.Chk_Date(TxtV_Date.Text) & ", " & Val(mV_No) & ",
                           " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ", " & AgL.Chk_Text(mRecId) & ", " & AgL.Chk_Text("") & ", " & AgL.Chk_Text("") & ", 'Dr',
                           Null, Null, Null, Null, Null,                           
                           " & AgL.Chk_Text(AgL.PubUserName) & "," & AgL.Chk_Date(AgL.PubLoginDate) & ", Null, Null, " & AgL.Chk_Text(AgL.PubUserName) & ",
                           " & AgL.Chk_Date(AgL.PubLoginDate) & "
                       );"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)



            mQry = "Insert Into LedgerHeadDetail (DocID, Sr, Subcode, Amount, Remarks, EffectiveDate,ReferenceDocID, ReferenceDocIdSr)
                            Values ('" & StrDocID & "', 1,
                            " & AgL.Chk_Text(TxtParty.Tag) & ",
                            " & Val(LblDifferenceAmount_W.Text) & ",
                            " & AgL.Chk_Text(TxtRemarks.Text) & ", Null, " & AgL.Chk_Text(SearchCode) & ", Null)"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            If LblV_Type.Tag = AgLibrary.ClsMain.agConstants.Ncat.ReceiptSettlement Then
                mDebitAmt = Val(LblDifferenceAmount_W.Text)
                mCreditAmt = 0
                mDebitNarration = "Receipt Received"
                mCreditNarration = "Receipt Settlement of " & TxtParty.Text
            Else
                mDebitAmt = 0
                mCreditAmt = Val(LblDifferenceAmount_W.Text)
                mDebitNarration = "Payment Settlement of " & TxtParty.Text
                mCreditNarration = "Payment Settlement"
            End If


            mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode, LinkedSubcode,ContraSub,AmtDr,AmtCr,
                          Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,
                          System_Generated, EffectiveDate, ReferenceDocId, ReferenceDocIdSr) Values 
                          ('" & StrDocID & "','" & mRecId & "',1," & AgL.Chk_Date(TxtV_Date.Text) & "," & AgL.Chk_Text(TxtParty.Tag) & "," & AgL.Chk_Text(TxtLinkedParty.Tag) & "," & AgL.Chk_Text("SUSPENSE") & ", 
                          " & mDebitAmt & "," & mCreditAmt & ", 
                          " & AgL.Chk_Text(mDebitNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "',
                          " & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",'Y', Null, " & AgL.Chk_Text(SearchCode) & ", Null)"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode, LinkedSubcode,ContraSub,AmtDr,AmtCr,
                          Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,
                          System_Generated, EffectiveDate, ReferenceDocId, ReferenceDocIdSr) Values 
                          ('" & StrDocID & "','" & mRecId & "',2," & AgL.Chk_Date(TxtV_Date.Text) & "," & AgL.Chk_Text("SUSPENSE") & ",Null," & AgL.Chk_Text(TxtParty.Tag) & ", 
                          " & mCreditAmt & "," & mDebitAmt & ", 
                          " & AgL.Chk_Text(mCreditNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "',
                          " & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",'Y', Null, " & AgL.Chk_Text(SearchCode) & ", Null)"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


            mQry = "INSERT INTO TransactionReferences(DocId, DocIDSr, ReferenceDocId, ReferenceSr, Type, Remark) 
                    Select '" & SearchCode & "', Null, '" & StrDocID & "', Null, " & AgL.Chk_Text(ClsMain.TransactionReferenceTypeConstants.SettlementDifference_W) & ",
                    " & AgL.Chk_Text(TxtRemarks.Text) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            mQry = "Update voucher_prefix set start_srl_no = " & Val(mV_No) & " 
                    where v_type = " & AgL.Chk_Text(mV_Type) & " 
                    and prefix=" & AgL.Chk_Text(mV_Prefix) & " 
                    And Site_Code = '" & TxtSite_Code.Tag & "' 
                    And Div_Code = '" & TxtDivision.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

    End Sub

    Sub SaveFifoAdjustment(ByRef Conn As Object, ByRef Cmd As Object)

        Dim intInvRowIndex As Integer
        Dim intPmtRowIndex As Integer
        Dim dblAdjQty As Double
        Dim DtAdj As DataTable
        Dim mQry1 As String


        Try
            mQry = "Drop Table #Adj "
            AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
        Catch ex As Exception
        End Try


        mQry = "Create Temporary Table #Adj 
                    (
                    InvDocID nVarchar(21),
                    InvSr nVarchar(10),
                    PmtDocId nVarchar(21),
                    PmtSr nVarchar(10),
                    Amt Float,
                    Div_Code nVarchar(1),
                    Site_Code nVarchar(2)
                    );
                    Select * From #Adj With (NoLock) Where 1=2;
                    "
        DtAdj = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)



        Dim objProgressbar As New AgLibrary.FrmProgressBar
        objProgressbar.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog

        Dim DtPayment As DataTable
        Dim DtInvoice As DataTable
        mQry = "Select L.*, L.AmtDr+L.AmtCr as Amount,IfNull(Adj.AdjAmt,0) as AdjAmt, 0 as BalAmt, 
                L.DivCode, L.Site_Code, (Case When L.AmtDr > 0 Then 'Dr' Else 'Cr' End) as AdjType 
                from ledger L With (NoLock)
                Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                            abs(Sum(Amount)) as AdjAmt 
                            From LedgerAdj LA  With (NoLock)
                            Left Join Ledger L1  With (NoLock) On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                            Group By Adj_DocID, Adj_V_Sno
                            Union All 
                            Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                            abs(Sum(Amount)) as AdjAmt 
                            From LedgerAdj LA  With (NoLock)
                            Left Join Ledger L1  With (NoLock) On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                            Group By Vr_DocID, Vr_V_Sno                    
                            ) as Adj On L.DocID = Adj.DocID And L.V_Sno = Adj.V_Sno                
                Where  Substr(L.V_Type,1,1)<> 'W' And (L.DocId = '" & mSearchCode & "' 
                Or L.DocId  || Cast(L.V_SNo as NVarchar) In (Select PurchaseInvoiceDocId || Cast(IfNull(PurchaseInvoiceDocIdSr,L.V_SNo) as NVarchar) From Cloth_SupplierSettlementInvoices  With (NoLock) Where DocID = '" & mSearchCode & "') 
                Or L.DocID In (Select PaymentDocID From Cloth_SupplierSettlementPayments  With (NoLock) Where DocID='" & mSearchCode & "')
                Or L.DocID In (Select ReferenceDocID From TransactionReferences  With (NoLock) Where DocID='" & mSearchCode & "')
                ) 
                And L.Subcode = " & AgL.Chk_Text(TxtParty.Tag) & "  "
        If TxtDrCr.Text = "Credit" Then
            mQry1 = mQry + " And L.AmtDr > 0 Order By L.V_Date, L.RecId"
            DtPayment = AgL.FillData(mQry1, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            DtPayment.Columns("BalAmt").Expression = "Amount - [AdjAmt]"

            mQry1 = mQry + "  And L.AmtCr > 0  Order By L.V_Date, L.RecId"
            DtInvoice = AgL.FillData(mQry1, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            DtInvoice.Columns("BalAmt").Expression = "Amount - [AdjAmt]"
        Else
            mQry1 = mQry + "  And L.AmtCr > 0  Order By L.V_Date, L.RecId"
            DtPayment = AgL.FillData(mQry1, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            DtPayment.Columns("BalAmt").Expression = "Amount - [AdjAmt]"

            mQry1 = mQry + "  And L.AmtDr > 0  Order By L.V_Date, L.RecId"
            DtInvoice = AgL.FillData(mQry1, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            DtInvoice.Columns("BalAmt").Expression = "Amount - [AdjAmt]"
        End If

        Dim DrDtAdj As DataRow

        For intInvRowIndex = 0 To DtInvoice.Rows.Count - 1
            If Val(DtInvoice.Rows(intInvRowIndex)("BalAmt")) > 0 Then
                DtPayment.DefaultView.RowFilter = Nothing
                DtPayment.DefaultView.RowFilter = " Subcode = '" & AgL.XNull(DtInvoice.Rows(intInvRowIndex)("Subcode")) & "' and [BalAmt]>0 "

                For intPmtRowIndex = 0 To DtPayment.DefaultView.Count - 1
                    dblAdjQty = 0
                    DtPayment.DefaultView.RowFilter = Nothing
                    DtPayment.DefaultView.RowFilter = " Subcode = '" & AgL.XNull(DtInvoice.Rows(intInvRowIndex)("Subcode")) & "' and [BalAmt]>0 "

                    If Val(DtInvoice.Rows(intInvRowIndex)("BalAmt")) <= 0 Then Continue For
                    If AgL.XNull(DtInvoice.Rows(intInvRowIndex)("Subcode")) = AgL.XNull(DtPayment.DefaultView(0)("Subcode")) Then
                        If Val(DtInvoice.Rows(intInvRowIndex)("BalAmt")) <= Val(DtPayment.DefaultView(0)("BalAmt")) Then
                            dblAdjQty = Val(DtInvoice.Rows(intInvRowIndex)("BalAmt"))
                            DrDtAdj = DtAdj.NewRow
                            DrDtAdj("InvDocID") = DtInvoice.Rows(intInvRowIndex)("DocID")
                            DrDtAdj("InvSr") = DtInvoice.Rows(intInvRowIndex)("V_SNo")
                            DrDtAdj("PmtDocID") = DtPayment.DefaultView(0)("DocID")
                            DrDtAdj("PmtSr") = DtPayment.DefaultView(0)("V_Sno")
                            DrDtAdj("Div_Code") = DtPayment.DefaultView(0)("DivCode")
                            DrDtAdj("Site_Code") = DtPayment.DefaultView(0)("Site_Code")
                            DrDtAdj("Amt") = dblAdjQty

                            DtAdj.Rows.Add(DrDtAdj)

                            DtInvoice.Rows(intInvRowIndex)("AdjAmt") = AgL.VNull(DtInvoice.Rows(intInvRowIndex)("AdjAmt")) + dblAdjQty
                            DtPayment.DefaultView(0)("AdjAmt") = AgL.VNull(DtPayment.DefaultView(0)("AdjAmt")) + dblAdjQty

                            DtInvoice.AcceptChanges()
                            DtPayment.AcceptChanges()
                        ElseIf Val(DtInvoice.Rows(intInvRowIndex)("BalAmt")) > Val(DtPayment.DefaultView(0)("BalAmt")) Then
                            dblAdjQty = Val(DtPayment.DefaultView(0)("BalAmt"))
                            DrDtAdj = DtAdj.NewRow
                            DrDtAdj("InvDocID") = DtInvoice.Rows(intInvRowIndex)("DocID")
                            DrDtAdj("InvSr") = DtInvoice.Rows(intInvRowIndex)("V_SNo")
                            DrDtAdj("PmtDocID") = DtPayment.DefaultView(0)("DocID")
                            DrDtAdj("PmtSr") = DtPayment.DefaultView(0)("V_Sno")
                            DrDtAdj("Div_Code") = DtPayment.DefaultView(0)("DivCode")
                            DrDtAdj("Site_Code") = DtPayment.DefaultView(0)("Site_Code")
                            DrDtAdj("Amt") = dblAdjQty

                            DtAdj.Rows.Add(DrDtAdj)

                            DtInvoice.Rows(intInvRowIndex)("AdjAmt") = AgL.VNull(DtInvoice.Rows(intInvRowIndex)("AdjAmt")) + dblAdjQty
                            DtPayment.DefaultView(0)("AdjAmt") = AgL.VNull(DtPayment.DefaultView(0)("AdjAmt")) + dblAdjQty

                            DtInvoice.AcceptChanges()
                            DtPayment.AcceptChanges()
                        End If
                    Else
                        MsgBox("Items of stock out and stock in doesn't match")
                    End If
                Next

            End If

            DtAdj.AcceptChanges()
        Next





        mQry = "Select L.*, L.AmtDr+L.AmtCr as Amount,IfNull(Adj.AdjAmt,0) as AdjAmt, 0 as BalAmt, 
                L.DivCode, L.Site_Code, (Case When L.AmtDr > 0 Then 'Dr' Else 'Cr' End) as AdjType 
                from ledger L With (NoLock)
                Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                            abs(Sum(Amount)) as AdjAmt 
                            From LedgerAdj LA  With (NoLock)
                            Left Join Ledger L1  With (NoLock) On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                            Group By Adj_DocID, Adj_V_Sno
                            Union All 
                            Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                            abs(Sum(Amount)) as AdjAmt 
                            From LedgerAdj LA  With (NoLock)
                            Left Join Ledger L1  With (NoLock) On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                            Group By Vr_DocID, Vr_V_Sno                    
                            ) as Adj On L.DocID = Adj.DocID And L.V_Sno = Adj.V_Sno                
                Where  Substr(L.V_Type,1,1) = 'W' And (L.DocId = '" & mSearchCode & "' 
                Or L.DocId  || Cast(L.V_SNo as NVarchar) In (Select PurchaseInvoiceDocId || Cast(IfNull(PurchaseInvoiceDocIdSr,L.V_SNo) as NVarchar) From Cloth_SupplierSettlementInvoices  With (NoLock) Where DocID = '" & mSearchCode & "') 
                Or L.DocID In (Select PaymentDocID From Cloth_SupplierSettlementPayments  With (NoLock) Where DocID='" & mSearchCode & "')
                Or L.DocID In (Select ReferenceDocID From TransactionReferences  With (NoLock) Where DocID='" & mSearchCode & "')
                ) 
                And L.Subcode = " & AgL.Chk_Text(TxtParty.Tag) & "  "
        If TxtDrCr.Text = "Credit" Then
            mQry1 = mQry + "  And L.AmtDr > 0 Order By L.V_Date, L.RecId"
            DtPayment = AgL.FillData(mQry1, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            DtPayment.Columns("BalAmt").Expression = "Amount - [AdjAmt]"

            mQry1 = mQry + "  And L.AmtCr > 0  Order By L.V_Date, L.RecId"
            DtInvoice = AgL.FillData(mQry1, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            DtInvoice.Columns("BalAmt").Expression = "Amount - [AdjAmt]"
        Else
            mQry1 = mQry + "  And L.AmtCr > 0  Order By L.V_Date, L.RecId"
            DtPayment = AgL.FillData(mQry1, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            DtPayment.Columns("BalAmt").Expression = "Amount - [AdjAmt]"

            mQry1 = mQry + "  And L.AmtDr > 0  Order By L.V_Date, L.RecId"
            DtInvoice = AgL.FillData(mQry1, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
            DtInvoice.Columns("BalAmt").Expression = "Amount - [AdjAmt]"
        End If



        For intInvRowIndex = 0 To DtInvoice.Rows.Count - 1
            If Val(DtInvoice.Rows(intInvRowIndex)("BalAmt")) > 0 Then
                DtPayment.DefaultView.RowFilter = Nothing
                DtPayment.DefaultView.RowFilter = " Subcode = '" & AgL.XNull(DtInvoice.Rows(intInvRowIndex)("Subcode")) & "' and [BalAmt]>0 "

                For intPmtRowIndex = 0 To DtPayment.DefaultView.Count - 1
                    dblAdjQty = 0
                    DtPayment.DefaultView.RowFilter = Nothing
                    DtPayment.DefaultView.RowFilter = " Subcode = '" & AgL.XNull(DtInvoice.Rows(intInvRowIndex)("Subcode")) & "' and [BalAmt]>0 "

                    If Val(DtInvoice.Rows(intInvRowIndex)("BalAmt")) <= 0 Then Continue For
                    If AgL.XNull(DtInvoice.Rows(intInvRowIndex)("Subcode")) = AgL.XNull(DtPayment.DefaultView(0)("Subcode")) Then
                        If Val(DtInvoice.Rows(intInvRowIndex)("BalAmt")) <= Val(DtPayment.DefaultView(0)("BalAmt")) Then
                            dblAdjQty = Val(DtInvoice.Rows(intInvRowIndex)("BalAmt"))
                            DrDtAdj = DtAdj.NewRow
                            DrDtAdj("InvDocID") = DtInvoice.Rows(intInvRowIndex)("DocID")
                            DrDtAdj("InvSr") = DtInvoice.Rows(intInvRowIndex)("V_SNo")
                            DrDtAdj("PmtDocID") = DtPayment.DefaultView(0)("DocID")
                            DrDtAdj("PmtSr") = DtPayment.DefaultView(0)("V_Sno")
                            DrDtAdj("Div_Code") = DtPayment.DefaultView(0)("DivCode")
                            DrDtAdj("Site_Code") = DtPayment.DefaultView(0)("Site_Code")
                            DrDtAdj("Amt") = dblAdjQty

                            DtAdj.Rows.Add(DrDtAdj)

                            DtInvoice.Rows(intInvRowIndex)("AdjAmt") = AgL.VNull(DtInvoice.Rows(intInvRowIndex)("AdjAmt")) + dblAdjQty
                            DtPayment.DefaultView(0)("AdjAmt") = AgL.VNull(DtPayment.DefaultView(0)("AdjAmt")) + dblAdjQty

                            DtInvoice.AcceptChanges()
                            DtPayment.AcceptChanges()
                        ElseIf Val(DtInvoice.Rows(intInvRowIndex)("BalAmt")) > Val(DtPayment.DefaultView(0)("BalAmt")) Then
                            dblAdjQty = Val(DtPayment.DefaultView(0)("BalAmt"))
                            DrDtAdj = DtAdj.NewRow
                            DrDtAdj("InvDocID") = DtInvoice.Rows(intInvRowIndex)("DocID")
                            DrDtAdj("InvSr") = DtInvoice.Rows(intInvRowIndex)("V_SNo")
                            DrDtAdj("PmtDocID") = DtPayment.DefaultView(0)("DocID")
                            DrDtAdj("PmtSr") = DtPayment.DefaultView(0)("V_Sno")
                            DrDtAdj("Div_Code") = DtPayment.DefaultView(0)("DivCode")
                            DrDtAdj("Site_Code") = DtPayment.DefaultView(0)("Site_Code")
                            DrDtAdj("Amt") = dblAdjQty

                            DtAdj.Rows.Add(DrDtAdj)

                            DtInvoice.Rows(intInvRowIndex)("AdjAmt") = AgL.VNull(DtInvoice.Rows(intInvRowIndex)("AdjAmt")) + dblAdjQty
                            DtPayment.DefaultView(0)("AdjAmt") = AgL.VNull(DtPayment.DefaultView(0)("AdjAmt")) + dblAdjQty

                            DtInvoice.AcceptChanges()
                            DtPayment.AcceptChanges()
                        End If
                    Else
                        MsgBox("Items of stock out and stock in doesn't match")
                    End If
                Next

            End If

            DtAdj.AcceptChanges()
        Next





        Dim I As Integer
        For I = 0 To DtAdj.Rows.Count - 1
            If AgL.XNull(DtAdj.Rows(I)("InvDocID")) <> "" Then
                If TxtDrCr.Text = "Debit" Then
                    mQry = "Insert Into LedgerAdj(Vr_DocID, Vr_V_SNo, Adj_DocID, Adj_V_SNo, Amount, Site_Code, Div_Code, Adj_Type,ReferenceDocID)
                                Values (" & AgL.Chk_Text(DtAdj.Rows(I)("PmtDocID")) & "," & AgL.Chk_Text(DtAdj.Rows(I)("PmtSr")) & ", " & AgL.Chk_Text(DtAdj.Rows(I)("InvDocID")) & ", " & AgL.Chk_Text(DtAdj.Rows(I)("InvSr")) & ", " & -1.0 * Val(DtAdj.Rows(I)("Amt")) & ", " & AgL.Chk_Text(DtAdj.Rows(I)("Site_Code")) & ", " & AgL.Chk_Text(DtAdj.Rows(I)("Div_Code")) & ", 'Adjustment', " & AgL.Chk_Text(mSearchCode) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    mQry = "Insert Into LedgerAdj(Vr_DocID, Vr_V_SNo, Adj_DocID, Adj_V_SNo, Amount, Site_Code, Div_Code, Adj_Type, ReferenceDocID)
                                Values (" & AgL.Chk_Text(DtAdj.Rows(I)("PmtDocID")) & "," & AgL.Chk_Text(DtAdj.Rows(I)("PmtSr")) & ", " & AgL.Chk_Text(DtAdj.Rows(I)("InvDocID")) & ", " & AgL.Chk_Text(DtAdj.Rows(I)("InvSr")) & ", " & Val(DtAdj.Rows(I)("Amt")) & ", " & AgL.Chk_Text(DtAdj.Rows(I)("Site_Code")) & ", " & AgL.Chk_Text(DtAdj.Rows(I)("Div_Code")) & ", 'Adjustment', " & AgL.Chk_Text(mSearchCode) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next
    End Sub



    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*,  
                Sg.Name as PartyNameMast, LSg.Name as LinkedPartyNameMast 
                From LedgerHead H 
                Left Join viewHelpSubgroup Sg On H.SubCode = Sg.Code 
                Left Join viewHelpSubgroup LSg On H.LinkedSubCode = LSg.Code 
                Left Join City C On Sg.CityCode = C.CityCode 
                Where H.DocID ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtParty.Tag = AgL.XNull(.Rows(0)("SubCode"))
                TxtParty.Text = AgL.XNull(.Rows(0)("PartyNameMast"))
                TxtLinkedParty.Tag = AgL.XNull(.Rows(0)("LinkedSubCode"))
                TxtLinkedParty.Text = AgL.XNull(.Rows(0)("LinkedPartyNameMast"))

                TxtUptoDate.Text = ClsMain.FormatDate(AgL.XNull(.Rows(0)("UptoDate")))
                TxtDrCr.Tag = AgL.XNull(.Rows(0)("DrCr"))
                TxtDrCr.Text = IIf(TxtDrCr.Tag = "Dr", "Debit", "Credit")

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                IniGrid()
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select L.*, Sg.Name as SubcodeName
                       from (Select * From LedgerHeadDetail  where DocId = '" & SearchCode & "') L 
                       Left Join viewHelpSubgroup Sg  On L.Subcode = Sg.Code 
                       Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1Subcode, I).Tag = AgL.XNull(.Rows(I)("Subcode"))
                            Dgl1.Item(Col1Subcode, I).Value = AgL.XNull(.Rows(I)("SubcodeName"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount")) + AgL.VNull(.Rows(I)("AmountCr"))
                            Dgl1.Item(Col1DrCr, I).Value = IIf(AgL.VNull(.Rows(I)("AmountCr")) > 0, "Cr", "Dr")
                            Dgl1.Item(Col1ChqRefNo, I).Value = AgL.XNull(.Rows(I)("ChqRefNo"))
                            Dgl1.Item(Col1ChqRefDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ChqRefDate")))
                            Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))

                        Next I
                    End If
                End With

                mQry = "Select ReferenceDocID From TransactionReferences With (NoLock) Where DocID = '" & SearchCode & "' And Type = '" & ClsMain.TransactionReferenceTypeConstants.SettlementDifference & "' "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                If DsTemp.Tables(0).Rows.Count > 0 Then
                    TxtDifferenceJVDocNo_A.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("ReferenceDocID"))
                    TxtDifferenceJVDocNo_A.Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("ReferenceDocID"))
                End If

                mQry = "Select ReferenceDocID From TransactionReferences With (NoLock) Where DocID = '" & SearchCode & "' And Type = '" & ClsMain.TransactionReferenceTypeConstants.SettlementDifference_W & "' "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                If DsTemp.Tables(0).Rows.Count > 0 Then
                    TxtDifferenceJVDocNo_W.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("ReferenceDocID"))
                    TxtDifferenceJVDocNo_W.Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("ReferenceDocID"))
                End If


                mQry = "Select L.*, Sg.Name as SubcodeName, (Case When PH.PartyDocNo Is Null Then PI.DivCode || PI.Site_Code || '-' || PI.V_Type || '-' || PI.RecId Else PH.PartyDocNo End) As PurchaseInvoiceNo,
                       PI.V_Date as PurchaseInvoiceDate, L.InvoiceAmount Taxable_Amount
                       from (Select * From Cloth_SupplierSettlementInvoices  where DocId = '" & SearchCode & "' And Tags='+A' ) L 
                       Left Join LedgerHead LH On LH.DocID = L.DocID 
                       Left Join Ledger PI on L.PurchaseInvoiceDocId = PI.DocID And IfNull(L.PurchaseInvoiceDocIDSr,PI.V_SNo) = PI.V_SNo And LH.Subcode = PI.Subcode
                       Left Join LedgerHead PH on PI.DocID = PH.DocID 
                       Left Join viewHelpSubgroup Sg  On PI.Subcode = Sg.Code 
                       Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl2.RowCount = 1
                    Dgl2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl2.Rows.Add()
                            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                            Dgl2.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl2.Item(Col2InvoiceNo, I).Tag = AgL.XNull(.Rows(I)("PurchaseInvoiceDocId"))
                            Dgl2.Item(Col2InvoiceNo, I).Value = AgL.XNull(.Rows(I)("PurchaseInvoiceNo"))
                            Dgl2.Item(Col2InvoiceSr, I).Value = AgL.XNull(.Rows(I)("PurchaseInvoiceDocIdSr"))
                            Dgl2.Item(Col2InvoiceDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("PurchaseInvoiceDate")))
                            Dgl2.Item(Col2TaxableAmount, I).Value = AgL.VNull(.Rows(I)("Taxable_Amount"))
                            Dgl2.Item(Col2InvoiceAmount, I).Value = AgL.VNull(.Rows(I)("InvoiceAmount"))
                            Dgl2.Item(Col2ItemDeductions, I).Value = AgL.VNull(.Rows(I)("LineDeduction"))
                            Dgl2.Item(Col2SettlementAddition, I).Value = AgL.VNull(.Rows(I)("SettlementAddition"))
                            Dgl2.Item(Col2SettlementDeduction, I).Value = AgL.VNull(.Rows(I)("SettlementDeduction"))
                            Dgl2.Item(Col2SettlementInvoiceAmount, I).Value = AgL.VNull(.Rows(I)("SettlementInvoiceAmount"))
                            Dgl2.Item(Col2AdjustedAmount, I).Value = AgL.VNull(.Rows(I)("AdjustedAmount"))
                            Dgl2.Item(Col2SettlementRemark, I).Value = AgL.XNull(.Rows(I)("SettlementRemark"))
                        Next I
                    End If
                End With




                mQry = "Select L.*, Sg.Name as SubcodeName, (Case When PH.PartyDocNo Is Null Then PI.DivCode || PI.Site_Code || '-' || PI.V_Type || '-' || PI.RecId Else PH.PartyDocNo End) As PurchaseInvoiceNo,
                       PI.V_Date as PurchaseInvoiceDate, L.InvoiceAmount Taxable_Amount
                       from (Select * From Cloth_SupplierSettlementInvoices  where DocId = '" & SearchCode & "' And Tags='+W' ) L 
                       Left Join LedgerHead LH On LH.DocID = L.DocID 
                       Left Join Ledger PI on L.PurchaseInvoiceDocId = PI.DocID And IfNull(L.PurchaseInvoiceDocIDSr,PI.V_SNo) = PI.V_SNo And LH.Subcode = PI.Subcode
                       Left Join LedgerHead PH on PI.DocID = PH.DocID 
                       Left Join viewHelpSubgroup Sg  On PI.Subcode = Sg.Code 
                       Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl4.RowCount = 1
                    Dgl4.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl4.Rows.Add()
                            Dgl4.Item(ColSNo, I).Value = Dgl4.Rows.Count - 1
                            Dgl4.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl4.Item(Col2InvoiceNo, I).Tag = AgL.XNull(.Rows(I)("PurchaseInvoiceDocId"))
                            Dgl4.Item(Col2InvoiceNo, I).Value = AgL.XNull(.Rows(I)("PurchaseInvoiceNo"))
                            Dgl4.Item(Col2InvoiceSr, I).Value = AgL.XNull(.Rows(I)("PurchaseInvoiceDocIdSr"))
                            Dgl4.Item(Col2InvoiceDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("PurchaseInvoiceDate")))
                            Dgl4.Item(Col2TaxableAmount, I).Value = AgL.VNull(.Rows(I)("Taxable_Amount"))
                            Dgl4.Item(Col2InvoiceAmount, I).Value = AgL.VNull(.Rows(I)("InvoiceAmount"))
                            Dgl4.Item(Col2ItemDeductions, I).Value = AgL.VNull(.Rows(I)("LineDeduction"))
                            Dgl4.Item(Col2SettlementAddition, I).Value = AgL.VNull(.Rows(I)("SettlementAddition"))
                            Dgl4.Item(Col2SettlementDeduction, I).Value = AgL.VNull(.Rows(I)("SettlementDeduction"))
                            Dgl4.Item(Col2SettlementInvoiceAmount, I).Value = AgL.VNull(.Rows(I)("SettlementInvoiceAmount"))
                            Dgl4.Item(Col2AdjustedAmount, I).Value = AgL.VNull(.Rows(I)("AdjustedAmount"))
                            Dgl4.Item(Col2SettlementRemark, I).Value = AgL.XNull(.Rows(I)("SettlementRemark"))
                            Dgl4.Item(Col2PInvoiceNo, I).Value = AgL.XNull(.Rows(I)("Remarks1"))
                            Dgl4.Item(Col2PWInvoiceAmount, I).Value = AgL.XNull(.Rows(I)("Remarks2"))
                        Next I
                    End If
                End With



                mQry = "Select L.*, IFNULL(Ledger.DivCode,'') || Ledger.Site_Code || '-' || Ledger.V_Type || '-' || Ledger.RecId As PaymentNo, Ledger.V_Date, cSg.Name as ContraName
                       from (Select * From Cloth_SupplierSettlementPayments  where DocId = '" & SearchCode & "' And Tags='+A') L 
                       Left Join ledger on L.PaymentDocId = Ledger.DocID and L.PaymentDocIdSr = Ledger.V_SNo
                       Left Join viewHelpSubgroup cSg On Ledger.ContraSub = cSg.Code                        
                       Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl3.RowCount = 1
                    Dgl3.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl3.Rows.Add()
                            Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count - 1
                            Dgl3.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl3.Item(Col3PaymentNo, I).Tag = AgL.XNull(.Rows(I)("PaymentDocId"))
                            Dgl3.Item(Col3PaymentSr, I).Value = AgL.XNull(.Rows(I)("PaymentDocIdSr"))
                            Dgl3.Item(Col3PaymentNo, I).Value = AgL.XNull(.Rows(I)("PaymentNo"))
                            Dgl3.Item(Col3PaymentDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("V_Date")))
                            Dgl3.Item(Col3Amount, I).Value = AgL.VNull(.Rows(I)("PaidAmount"))
                            Dgl3.Item(Col3Subcode, I).Value = AgL.XNull(.Rows(I)("ContraName"))
                            Dgl3.Item(Col3AdjustedAmount, I).Value = AgL.VNull(.Rows(I)("AdjustedAmount"))
                            Dgl3.Item(Col3Select, I).Value = "þ"
                        Next I
                    End If
                End With



                mQry = "Select L.*, IFNULL(Ledger.DivCode,'') || Ledger.Site_Code || '-' || Ledger.V_Type || '-' || Ledger.RecId As PaymentNo, Ledger.V_Date, cSg.Name as ContraName
                       from (Select * From Cloth_SupplierSettlementPayments  where DocId = '" & SearchCode & "' And Tags='+W') L 
                       Left Join ledger on L.PaymentDocId = Ledger.DocID and L.PaymentDocIdSr = Ledger.V_SNo
                       Left Join viewHelpSubgroup cSg On Ledger.ContraSub = cSg.Code                        
                       Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl5.RowCount = 1
                    Dgl5.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl5.Rows.Add()
                            Dgl5.Item(ColSNo, I).Value = Dgl5.Rows.Count - 1
                            Dgl5.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl5.Item(Col3PaymentNo, I).Tag = AgL.XNull(.Rows(I)("PaymentDocId"))
                            Dgl5.Item(Col3PaymentSr, I).Value = AgL.XNull(.Rows(I)("PaymentDocIdSr"))
                            Dgl5.Item(Col3PaymentNo, I).Value = AgL.XNull(.Rows(I)("PaymentNo"))
                            Dgl5.Item(Col3PaymentDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("V_Date")))
                            Dgl5.Item(Col3Amount, I).Value = AgL.VNull(.Rows(I)("PaidAmount"))
                            Dgl5.Item(Col3Subcode, I).Value = AgL.XNull(.Rows(I)("ContraName"))
                            Dgl5.Item(Col3AdjustedAmount, I).Value = AgL.VNull(.Rows(I)("AdjustedAmount"))
                            Dgl5.Item(Col3Select, I).Value = "þ"
                        Next I
                    End If
                End With


                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 650, 905)
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub

    Private Sub TxtFromGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtParty.KeyDown, TxtUptoDate.KeyDown, TxtDrCr.KeyDown, TxtLinkedParty.KeyDown
        Select Case sender.Name

            Case TxtParty.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        FCreateHelpSubgroupHeader()
                    End If
                End If
            Case TxtLinkedParty.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        FCreateHelpLinkedSubgroup()
                    End If
                End If

            Case TxtDrCr.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.aghelpdataset Is Nothing Then
                        mQry = "Select 'Cr' as Code, 'Credit' as Name Union All Select 'Dr' as Code, 'Debit' as Name"
                        TxtDrCr.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

        End Select
    End Sub

    Private Sub FCreateHelpLinkedSubgroup()
        Dim strCond As String = ""

        mQry = "SELECT Sg.Code, Sg.Name, Sg.Address
                FROM viewHelpSubGroup Sg  With (NoLock)                
                Where Sg.Code In (Select LinkedSubcode From Ledger Where Subcode='" & TxtParty.Tag & "') or Sg.Code =(Select Parent From Subgroup Where Subcode ='" & TxtParty.Tag & "') "
        TxtLinkedParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top) = AgL.FillData(mQry, AgL.GCn)
        If TxtLinkedParty.AgHelpDataSet.Tables(0).Rows.Count = 1 Then
            TxtLinkedParty.Tag = TxtLinkedParty.AgHelpDataSet.Tables(0).Rows(0)("Code")
            TxtLinkedParty.Text = TxtLinkedParty.AgHelpDataSet.Tables(0).Rows(0)("Name")
        End If
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtReferenceNo.Validating, TxtParty.Validating

        Select Case sender.NAME
            Case TxtV_Type.Name
                'mQry = "Select * from LedgerHeadSetting  Where Voucher_Type = '" & TxtV_Type.Tag & "' And Division='" & TxtDivision.Tag & "' And Site ='" & TxtSite_Code.Tag & "' "
                'DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                'If DtV_TypeSettings.Rows.Count = 0 Then
                '    mQry = "Select * from LedgerHeadSetting  Where Voucher_Type = '" & TxtV_Type.Tag & "' And Division='" & TxtDivision.Tag & "' And Site Is Null "
                '    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '    If DtV_TypeSettings.Rows.Count = 0 Then
                '        mQry = "Select * from LedgerHeadSetting  Where Voucher_Type = '" & TxtV_Type.Tag & "' And Division Is Null And Site ='" & TxtSite_Code.Tag & "' "
                '        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '        If DtV_TypeSettings.Rows.Count = 0 Then
                '            mQry = "Select * from LedgerHeadSetting  Where Voucher_Type = '" & TxtV_Type.Tag & "' And Division Is Null And Site Is Null "
                '            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '        End If
                '    End If
                'End If
                'If DtV_TypeSettings.Rows.Count = 0 Then
                '    MsgBox("Voucher Type Settings Not Defined, Can't Continue.")
                'End If


                TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                IniGrid()

                If TxtV_Type.AgLastValueTag <> TxtV_Type.Tag Then
                    TxtParty.AgHelpDataSet = Nothing
                    Dgl1.AgHelpDataSet(Col1Subcode) = Nothing
                End If

            Case TxtParty.Name
                Dgl1.AgHelpDataSet(Col1Subcode) = Nothing
                FCreateHelpLinkedSubgroup()
                If LblV_Type.Tag = Ncat.PaymentSettlement Then
                    TxtDrCr.Text = "Credit"
                Else
                    TxtDrCr.Text = "Debit"
                End If
                FillPendingData()
        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        'mQry = "Select * from LedgerHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And Site_Code ='" & TxtSite_Code.Tag & "' "
        'DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        'If DtV_TypeSettings.Rows.Count = 0 Then
        '    mQry = "Select * from LedgerHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And Site_Code is Null "
        '    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '    If DtV_TypeSettings.Rows.Count = 0 Then
        '        mQry = "Select * from LedgerHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code ='" & TxtSite_Code.Tag & "' "
        '        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '        If DtV_TypeSettings.Rows.Count = 0 Then
        '            mQry = "Select * from LedgerHeadSetting  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code Is Null And Site_Code Is Null "
        '            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '        End If
        '    End If
        'End If

        'If DtV_TypeSettings.Rows.Count = 0 Then
        '    MsgBox("Voucher Type Settings are not defined. Can't Continue!")
        '    Topctrl1.FButtonClick(14, True)
        '    Exit Sub
        'End If

        'If DtSettings.Rows.Count = 0 Then
        '    MsgBox("Settings are not defined. Can't Continue!")
        '    Topctrl1.FButtonClick(14, True)
        '    Exit Sub
        'End If

        If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            TxtV_Type.Tag = "WRS"
            TxtV_Type.Text = "W Receipt Settlement"
            LblV_Type.Tag = EntryNCat
        End If

        TxtUptoDate.Text = AgL.PubLoginDate
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtV_Date.Focus()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            'Case Col1Qty
            '    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
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
                'Case Col1Process
                '    If Dgl1.Item(Col1Process, mRowIndex).Value <> "" Then
                '        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                '            For I = mRowIndex To Dgl1.Rows.Count - 1
                '                If Dgl1.Item(Col1Subcode, I).Value <> "" Then
                '                    Dgl1.Item(Col1Process, I).Tag = Dgl1.Item(Col1Process, mRowIndex).Tag
                '                    Dgl1.Item(Col1Process, I).Value = Dgl1.Item(Col1Process, mRowIndex).Value
                '                End If
                '            Next
                '        End If
                '    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        LblInvoiceAmt_A.Text = 0 : LblInvoiceAmt_W.Text = 0
        LblTotalSettledInvoiceAmount_A.Text = 0 : LblTotalSettledInvoiceAmount_W.Text = 0 : LblTotalSettledInvoiceAmount_PW.Text = 0
        LblPaidAmt_A.Text = 0 : LblPaidAmt_W.Text = 0
        LblSettlementAmt.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Rows(I).Visible Then
                If Val(Dgl1.Item(Col1Amount, I).Value) > 0 And AgL.XNull(Dgl1.Item(Col1DrCr, I).Value) <> "" Then
                    If (TxtDrCr.Text = "Credit" And AgL.XNull(Dgl1.Item(Col1DrCr, I).Value) = "Cr") Or (TxtDrCr.Text = "Debit" And AgL.XNull(Dgl1.Item(Col1DrCr, I).Value) = "Dr") Then
                        LblSettlementAmt.Text = Val(LblSettlementAmt.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                    Else
                        LblSettlementAmt.Text = Val(LblSettlementAmt.Text) - Val(Dgl1.Item(Col1Amount, I).Value)
                    End If
                End If
            End If
        Next

        For I = 0 To Dgl2.RowCount - 1
            If Dgl2.Rows(I).Visible Then
                If Val(Dgl2.Item(Col2InvoiceAmount, I).Value) > 0 Then
                    If Dgl2.Rows(I).Visible Then
                        If Dgl2.Item(Col2BtnAdjDetail, I).Tag IsNot Nothing Then
                            CType(Dgl2.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).Calculation()
                            Dgl2.Item(Col2SettlementAddition, I).Value = CType(Dgl2.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).GetAdditions
                            Dgl2.Item(Col2SettlementDeduction, I).Value = CType(Dgl2.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).GetDeductions
                        End If
                        Dgl2.Item(Col2SettlementInvoiceAmount, I).Value = Format(Val(Dgl2.Item(Col2InvoiceAmount, I).Value) + Val(Dgl2.Item(Col2SettlementAddition, I).Value) - Val(Dgl2.Item(Col2SettlementDeduction, I).Value) - Val(Dgl2.Item(Col2ItemDeductions, I).Value), "0.00")
                        LblInvoiceAmt_A.Text = Val(LblInvoiceAmt_A.Text) + Val(Dgl2.Item(Col2InvoiceAmount, I).Value)
                        LblTotalSettledInvoiceAmount_A.Text = Val(LblTotalSettledInvoiceAmount_A.Text) + Val(Dgl2.Item(Col2SettlementInvoiceAmount, I).Value)
                    End If
                End If
            End If
        Next


        For I = 0 To Dgl4.RowCount - 1
            If Dgl4.Rows(I).Visible Then
                If Val(Dgl4.Item(Col2InvoiceAmount, I).Value) > 0 Then
                    If Dgl4.Rows(I).Visible Then
                        If Dgl4.Item(Col2BtnAdjDetail, I).Tag IsNot Nothing Then
                            CType(Dgl4.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).Calculation()
                            Dgl4.Item(Col2SettlementAddition, I).Value = CType(Dgl4.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).GetAdditions
                            Dgl4.Item(Col2SettlementDeduction, I).Value = CType(Dgl4.Item(Col2BtnAdjDetail, I).Tag, FrmPartyAcSettlementInvoiceAdj).GetDeductions
                        End If
                        Dgl4.Item(Col2SettlementInvoiceAmount, I).Value = Format(Val(Dgl4.Item(Col2InvoiceAmount, I).Value) + Val(Dgl4.Item(Col2SettlementAddition, I).Value) - Val(Dgl4.Item(Col2SettlementDeduction, I).Value) - Val(Dgl4.Item(Col2ItemDeductions, I).Value), "0.00")
                        LblInvoiceAmt_W.Text = Val(LblInvoiceAmt_W.Text) + Val(Dgl4.Item(Col2InvoiceAmount, I).Value)
                        LblTotalSettledInvoiceAmount_W.Text = Val(LblTotalSettledInvoiceAmount_W.Text) + Val(Dgl4.Item(Col2SettlementInvoiceAmount, I).Value)
                    End If
                End If
                If Val(Dgl4.Item(Col2PWInvoiceAmount, I).Value) > 0 Then
                    If Dgl4.Rows(I).Visible Then
                        LblTotalSettledInvoiceAmount_PW.Text = Val(LblTotalSettledInvoiceAmount_PW.Text) + Val(Dgl4.Item(Col2PWInvoiceAmount, I).Value)
                    End If
                End If
            End If
        Next


        For I = 0 To Dgl3.RowCount - 1
            If Dgl3.Rows(I).Visible Then
                If Val(Dgl3.Item(Col3Amount, I).Value) > 0 Then
                    If Dgl3.Item(Col3Select, I).Value = "þ" Then
                        LblPaidAmt_A.Text = Val(LblPaidAmt_A.Text) + Val(Dgl3.Item(Col3Amount, I).Value)
                    End If
                End If
            End If
        Next

        For I = 0 To Dgl5.RowCount - 1
            If Dgl5.Rows(I).Visible Then
                If Val(Dgl5.Item(Col3Amount, I).Value) > 0 Then
                    If Dgl5.Item(Col3Select, I).Value = "þ" Then
                        LblPaidAmt_W.Text = Val(LblPaidAmt_W.Text) + Val(Dgl5.Item(Col3Amount, I).Value)
                    End If
                End If
            End If
        Next


        LblSettlementAmt.Text = Format(Val(LblSettlementAmt.Text), "0.00")
        LblInvoiceAmt_A.Text = Format(Val(LblInvoiceAmt_A.Text), "0.00")
        LblInvoiceAmt_W.Text = Format(Val(LblInvoiceAmt_W.Text), "0.00")
        LblTotalSettledInvoiceAmount_A.Text = Format(Val(LblTotalSettledInvoiceAmount_A.Text), "0.00")
        LblTotalSettledInvoiceAmount_W.Text = Format(Val(LblTotalSettledInvoiceAmount_W.Text), "0.00")
        LblPaidAmt_A.Text = Format(Val(LblPaidAmt_A.Text), "0.00")
        LblPaidAmt_W.Text = Format(Val(LblPaidAmt_W.Text), "0.00")

        LblDifferenceAmount_A.Text = Format(Val(LblTotalSettledInvoiceAmount_A.Text) - Val(LblPaidAmt_A.Text), "0.00")
        LblDifferenceAmount_W.Text = Format(Val(LblTotalSettledInvoiceAmount_W.Text) - Val(LblPaidAmt_W.Text) - Val(LblSettlementAmt.Text), "0.00")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim BalQty As Double = 0



        If AgL.XNull(DtSettings.Rows(0)("ItemDeductionPostingAc")) = "" Then MsgBox("Item Deduction Posting Account Not Defined.") : passed = False : Exit Sub


        'If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Subcode).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, CStr(Dgl1.Columns(Col1Subcode).Index) & "," & CStr(Dgl1.Columns(Col1ChqRefNo).Index)) = True Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Subcode, I).Value <> "" Then
                    If Val(.Item(Col1Amount, I).Value) = 0 Then
                        MsgBox("Amount Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Amount, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With



        If Val(LblDifferenceAmount_A.Text) <> 0 Then
            If FGetSettings(SettingFields.ActionIfDifferenceInPaymentSettlement, SettingType.General) = ActionIfDifferenceInPaymentSettlement.AlertAndStopTransaction Then
                MsgBox("Selected Invoices and Payment is not equal. Can't save record.")
                passed = False : Exit Sub
            ElseIf FGetSettings(SettingFields.ActionIfDifferenceInPaymentSettlement, SettingType.General) = ActionIfDifferenceInPaymentSettlement.AlertAndAskToContinue Then
                If MsgBox("Selected Invoices and Payment is not equal. Do you want to save record.", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    passed = False : Exit Sub
                End If
            End If
        End If




        If Val(LblDifferenceAmount_W.Text) <> 0 Then
            If FGetSettings(SettingFields.ActionIfDifferenceInPaymentSettlement, SettingType.General) = ActionIfDifferenceInPaymentSettlement.AlertAndStopTransaction Then
                MsgBox("Selected W Invoices and W Payment is not equal. Can't save record.")
                passed = False : Exit Sub
            ElseIf FGetSettings(SettingFields.ActionIfDifferenceInPaymentSettlement, SettingType.General) = ActionIfDifferenceInPaymentSettlement.AlertAndAskToContinue Then
                If MsgBox("Selected W Invoices and W Payment is not equal. Do you want to save record.", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    passed = False : Exit Sub
                End If
            End If
        End If

    End Sub


    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()
        Dgl4.RowCount = 1 : Dgl4.Rows.Clear()
        Dgl5.RowCount = 1 : Dgl5.Rows.Clear()
        LblSettlementAmt.Text = 0 : LblPaidAmt_A.Text = 0 : LblInvoiceAmt_A.Text = 0
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown, Dgl2.KeyDown, Dgl3.KeyDown, Dgl4.KeyDown, Dgl5.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            'sender.CurrentRow.Selected = True
            sender.Rows(sender.currentcell.rowindex).Visible = False
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        'If e.KeyCode = Keys.Delete Then
        '    If sender.currentrow.selected Then
        '        sender.Rows(sender.currentcell.rowindex).Visible = False
        '        Calculation()
        '        e.Handled = True
        '    End If
        'End If

        'Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
        '    Case Col2Select
        '        If e.KeyCode = Keys.Space Then
        '            ClsMain.FManageTick(Dgl2, Dgl2.CurrentCell.ColumnIndex, Dgl2.Columns(Col2InvoiceNo).Index)
        '        End If
        'End Select
    End Sub

    Private Sub TempStockTransferIssue_BaseFunction_Create() Handles Me.BaseFunction_CreateHelpDataSet

    End Sub

    Private Sub FrmYarnSKUOpeningStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuImportPartPaymentFromDos.Visible = False
            MnuEditSave.Visible = False
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FCreateHelpSubgroupHeader()
        Dim strCond As String = ""


        If FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.SubgroupType,'" & FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General) & "') > 0 "
            ElseIf FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.SubgroupType,'" & FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General) & "') <= 0 "
            End If
        End If


        If FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.Nature,'" & FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General) & "') > 0 "
            ElseIf FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.Nature,'" & FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General) & "') <= 0 "
            End If
        End If


        If FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.GroupCode,'" & FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General) & "') > 0 "
            ElseIf FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.GroupCode,'" & FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General) & "') <= 0 "
            End If
        End If


        mQry = "SELECT Sg.Code, Sg.Name
                FROM viewHelpSubGroup Sg                 
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        TxtParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpSubgroupLine()
        Dim strCond As String = ""
        If FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.SubgroupType,'" & AgL.XNull(FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General)) & "') > 0 "
            ElseIf FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.SubgroupType,'" & AgL.XNull(FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General)) & "') <= 0 "
            End If
        End If


        If FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.GroupCode,'" & AgL.XNull(FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General)) & "') > 0 "
            ElseIf FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.GroupCode,'" & AgL.XNull(FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General)) & "') <= 0 "
            End If
        End If


        If FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.Nature,'" & AgL.XNull(FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General)) & "') > 0 "
            ElseIf FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.Nature,'" & AgL.XNull(FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General)) & "') <= 0 "
            End If
        End If




        mQry = "SELECT Sg.Code, Sg.Name
                FROM viewHelpSubGroup Sg                 
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond

        'mQry = "SELECT Sg.Code, Sg.Name
        '        FROM viewHelpSubGroup Sg                 
        '        Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " ' & strCond

        Dgl1.AgHelpDataSet(Col1Subcode) = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub FCreateHelpItem()
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

        mQry = "SELECT H.Code, H.Description as Item_Name, H.ManualCode as Item_No, H.Unit, IG.Description AS ItemGroupDesc, " &
            " H.Measure, H.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
            " NULL AS BalQty, NULL AS Process, NULL AS LotNo, NULL AS Dimension1, NULL AS Dimension2,  NULL AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", NULL AS " & AgTemplate.ClsMain.FGetDimension2Caption() & " " &
            " FROM Item H " &
            " LEFT JOIN ItemGroup IG On Ig.Code = H.ItemGroup " &
            "Left Join Unit U On H.Unit = U.Code " &
            "Left Join Unit MU On H.MeasureUnit = MU.Code " &
            "Where IfNull(H.IsDeleted ,0)  = 0 And " &
            "IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 11) = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Subcode
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        FCreateHelpSubgroupLine()
                        'mQry = "Select Code, Name From viewHelpSubgroup Where Nature in ('Cash','Bank') Order By Name"
                        'Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
            Case Col1DrCr
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = "Select 'Dr' as Code, 'Dr' as Name Union All Select 'Cr' as Code, 'Cr' as Name "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

        End Select
    End Sub


    'Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
    '    Dim dsMain As DataTable
    '    Dim dsInvoice As DataTable
    '    Dim dsPayment As DataTable
    '    Dim dsCompany As DataTable
    '    Dim mPrintTitle As String


    '    mPrintTitle = "Payment Settlement"

    '    mQry = "select H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as DocNo, H.V_Date, HSg.DispName as HSubcodeName, 
    '            HSg.Address, C.CityName, S.ManualCode as StateCode, S.Description as StateName, HSg.Mobile, HSg.Phone,
    '            (Select RegistrationNo From SubgroupRegistration Where RegistrationType='Sales Tax No' And Subcode=Hsg.Subcode) as PartyGSTNo, 
    '            (Select RegistrationNo From SubgroupRegistration Where RegistrationType='AADHAR NO' And Subcode=Hsg.Subcode) as PartyAadharNo,
    '            H.UpToDate, H.DrCr, H.Remarks as HRemarks, 
    '            LSg.Name as LSubcodeName, L.Amount, L.ChqRefNo, L.ChqRefDate, L.Remarks as LRemarks, '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
    '            from ledgerHead H
    '            left Join LedgerHeadDetail L On H.DocID = L.DocId
    '            Left Join Subgroup HSg On H.Subcode = HSg.Subcode
    '            Left Join City C on HSg.CityCode = C.CityCode
    '            Left Join State S On C.State = S.Code
    '            Left Join Subgroup Lsg on L.Subcode = LSg.Subcode
    '            where H.DocID ='" & mSearchCode & "'"
    '    dsMain = AgL.FillData(mQry, AgL.GCn).Tables(0)


    '    mQry = "select (Case When IH.PartyDocNo Is Null Then L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId Else IH.PartyDocNo End) As InvoiceNo,
    '            L.V_Date, H.InvoiceAmount, H.SettlementAddition, H.SettlementDeduction, H.LineDeduction,H.SettlementInvoiceAmount, H.SettlementRemark
    '            from Cloth_SupplierSettlementInvoices H
    '            Left Join LedgerHead LH On H.DocID = LH.DocId
    '            Left Join Ledger L On H.PurchaseInvoiceDocID =  L.DocID And LH.Subcode = L.Subcode
    '            Left Join LedgerHead IH On L.DocID = IH.DocID
    '            where H.DocID ='" & mSearchCode & "'"

    '    dsInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '    mQry = "select (Case When IH.PartyDocNo Is Null Then L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId Else IH.PartyDocNo End) As PaymentNo,
    '            L.V_Date, L.Narration, H.PaidAmount, cSg.Name as ContraName
    '            from Cloth_SupplierSettlementPayments H
    '            Left Join LedgerHead LH On H.DocID = LH.DocId
    '            Left Join Ledger L On H.PaymentDocId =  L.DocID And LH.Subcode = L.Subcode
    '            Left Join LedgerHead IH On L.DocID = IH.DocID
    '            Left Join viewHelpSubgroup cSg On L.ContraSub = cSg.Code                        
    '            Where H.DocID ='" & mSearchCode & "'
    '            "
    '    dsPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '    dsCompany = ClsMain.GetDocumentHeaderDataTable(TxtDivision.Tag, TxtSite_Code.Tag)

    '    Dim objRepPrint As FrmRepPrint
    '    objRepPrint = New FrmRepPrint(AgL)
    '    objRepPrint.reportViewer1.Visible = True
    '    Dim id As Integer = 0
    '    objRepPrint.reportViewer1.ProcessingMode = ProcessingMode.Local
    '    dsMain.WriteXml(AgL.PubReportPath + "\PaymentSettlement_DsMain.xml")
    '    dsInvoice.WriteXml(AgL.PubReportPath + "\PaymentSettlement_DsInvoice.xml")
    '    dsPayment.WriteXml(AgL.PubReportPath + "\PaymentSettlement_DsPayment.xml")
    '    dsCompany.WriteXml(AgL.PubReportPath + "\PaymentSettlement_DsCompany.xml")
    '    objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\PaymentSettlement.rdl"

    '    If (dsMain.Rows.Count = 0) Then
    '        MsgBox("No records found to print.")
    '    End If
    '    Dim rds As New ReportDataSource("DsMain", dsMain)
    '    Dim rdsInvoice As New ReportDataSource("DsInvoice", dsInvoice)
    '    Dim rdsPayment As New ReportDataSource("DsPayment", dsPayment)
    '    Dim rdsCompany As New ReportDataSource("DsCompany", dsCompany)

    '    objRepPrint.reportViewer1.LocalReport.DataSources.Clear()
    '    objRepPrint.reportViewer1.LocalReport.DataSources.Add(rds)
    '    objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsInvoice)
    '    objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsPayment)
    '    objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsCompany)


    '    objRepPrint.reportViewer1.LocalReport.Refresh()
    '    objRepPrint.reportViewer1.RefreshReport()
    '    objRepPrint.MdiParent = Me.MdiParent
    '    objRepPrint.Show()



    'End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FGetPrint(ClsMain.PrintFor.DocumentPrint)
    End Sub

    Private Sub FrmStoreReceiveNew_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        TxtParty.AgHelpDataSet = Nothing
        Dgl1.AgHelpDataSet(Col1Subcode) = Nothing
    End Sub



    Private Sub BtnFill_Click(sender As Object, e As EventArgs) Handles BtnFill.Click
        FillPendingData()
    End Sub

    Sub FillPendingData()
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mQry As String

        LblNotMappedInvoices.Text = ""

        Try
            mQry = "select H.DocID, H.V_Date, H.Taxable_Amount, H.Net_Amount, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo As InvoiceNo
                       
                    from PurchInvoice H
                    Left Join Cloth_SupplierSettlementInvoices SB On H.DocId = SB.PurchaseInvoiceDocId
                    where  H.Vendor = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtUptoDate.Text)).ToString("s")) & " 
                    And SB.DocId Is Null
                    "
            If TxtDrCr.Text = "Credit" Then
                mQry = "
                    Select H.DocID, H.V_SNo, H.V_Date, H.AmtCr as Inv_Amount, 
                    H.AmtCr-IfNull(Adj.AdjAmt,0) as Bal_Amount, (Case When HH.PartyDocNo is Null Then H.DivCode || H.Site_Code || '-' || H.V_Type || '-' || H.RecId Else HH.PartyDocNo End) As InvoiceNo,
                    0 as NotMapped  
                    from Ledger H
                    Left Join LedgerHead HH On H.DocID = HH.DocID
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                                    
                    where Substr(H.V_Type,1,1)<> 'W' And H.DivCode = '" & AgL.PubDivCode & "' And  H.AmtCr>0 And H.SubCode = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtUptoDate.Text)).ToString("s")) & " 
                    And H.AmtCr - IfNull(Adj.AdjAmt,0) > 0   
                    Order By H.V_Date, (Case When HH.PartyDocNo is Null Then H.DivCode || H.Site_Code || '-' || H.V_Type || '-' || H.RecId Else HH.PartyDocNo End)
                    "

                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Else

                mQry = "
                    Select H.DocID, H.V_SNo, H.V_Date, H.AmtDr as Inv_Amount, 
                    H.AmtDr-IfNull(Adj.AdjAmt,0) as Bal_Amount, IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId As InvoiceNo,
                    (Case When Ge.Code Is Null Then 1 else 0 End) as NotMapped 
                    from Ledger H
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                
                    Left Join SaleInvoiceGeneratedEntries GE On H.DocID = GE.DocID
                    where Substr(H.V_Type,1,1)<> 'W' And H.DivCode = '" & AgL.PubDivCode & "' And  H.AmtDr>0 And H.SubCode = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtUptoDate.Text)).ToString("s")) & " 
                    And H.AmtDr - IfNull(Adj.AdjAmt,0)>0
                    Order by H.V_Date, (IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId)
                    "

                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            End If
            With DtTemp
                Dgl2.RowCount = 1
                Dgl2.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl2.Rows.Add()
                        Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                        Dgl2.Item(Col2InvoiceNo, I).Tag = AgL.XNull(.Rows(I)("DocId"))
                        Dgl2.Item(Col2InvoiceNo, I).Value = AgL.XNull(.Rows(I)("InvoiceNo"))
                        Dgl2.Item(Col2InvoiceSr, I).Value = AgL.XNull(.Rows(I)("V_SNo"))
                        Dgl2.Item(Col2InvoiceDate, I).Value = ClsMain.FormatDate(AgL.XNull(DtTemp.Rows(I)("V_Date")))
                        Dgl2.Item(Col2TaxableAmount, I).Value = AgL.VNull(.Rows(I)("Inv_Amount"))
                        Dgl2.Item(Col2InvoiceAmount, I).Value = AgL.VNull(.Rows(I)("Bal_Amount"))
                        If AgL.VNull(DtTemp.Rows(I)("NotMapped")) > 0 Then
                            If LblNotMappedInvoices.Text <> "" Then LblNotMappedInvoices.Text = LblNotMappedInvoices.Text & ", "
                            LblNotMappedInvoices.Text = LblNotMappedInvoices.Text & AgL.XNull(.Rows(I)("InvoiceNo"))
                        End If
                    Next I
                End If
            End With




            If TxtDrCr.Text = "Credit" Then
                mQry = "
                    Select H.DocID, H.V_SNo, H.V_Date, H.AmtCr as Inv_Amount,                           
                    '.' as PInvNo, 0 as PWInvAmt,              
                    H.AmtCr-IfNull(Adj.AdjAmt,0) as Bal_Amount, (Case When HH.PartyDocNo is Null Then H.DivCode || H.Site_Code || '-' || H.V_Type || '-' || H.RecId Else HH.PartyDocNo End) As InvoiceNo 
                    from Ledger H
                    Left Join LedgerHead HH On H.DocID = HH.DocID
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                                    
                    where Substr(H.V_Type,1,1) = 'W' And H.DivCode = '" & AgL.PubDivCode & "' And  H.AmtCr>0 And H.SubCode = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtUptoDate.Text)).ToString("s")) & " 
                    And H.AmtCr - IfNull(Adj.AdjAmt,0) > 0   
                    Order By H.V_Date, (Case When HH.PartyDocNo is Null Then H.DivCode || H.Site_Code || '-' || H.V_Type || '-' || H.RecId Else HH.PartyDocNo End)
                    "

                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Else

                mQry = "
                    Select H.DocID, H.V_SNo, H.V_Date, H.AmtDr as Inv_Amount, 
                    IfNull(pInv.ManualRefNo,'') as PInvNo, IfNull(pInv.Net_Amount,0) + H.AmtDr as PWInvAmt,
                    H.AmtDr-IfNull(Adj.AdjAmt,0) as Bal_Amount, IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId As InvoiceNo 
                    from Ledger H
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                
                    Left Join (
                                SELECT ge.DocID, s.Div_Code || s.Site_Code || '-' ||  s.V_type || '-' || s.ManualRefNo as ManualRefNo, s.Net_Amount 
                                FROM SaleInvoiceGeneratedEntries GE
                                LEFT JOIN SaleInvoiceGeneratedEntries GS ON GE.Code = GS.Code AND GS.V_Type ='SI'
                                LEFT JOIN saleinvoice s ON gs.DocId = s.DocID 
                                WHERE GE.V_Type ='WSI' and gs.Code Is Not Null
                              ) as pInv on H.DocID = pInv.DocID
                    where Substr(H.V_Type,1,1) = 'W' And H.DivCode = '" & AgL.PubDivCode & "' And  H.AmtCr=0 And H.SubCode = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtUptoDate.Text)).ToString("s")) & " 
                    And (H.AmtDr - IfNull(Adj.AdjAmt,0)>0 Or Adj.AdjAmt Is Null )
                    And H.DocID || H.V_SNo Not In (Select PurchaseInvoiceDocID || PurchaseInvoiceDocIDSr From Cloth_SupplierSettlementInvoices Where DocID <> '" & mSearchCode & "')
                    Order by H.V_Date, (IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId)
                    "

                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            End If
            With DtTemp
                Dgl4.RowCount = 1
                Dgl4.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl4.Rows.Add()
                        Dgl4.Item(ColSNo, I).Value = Dgl4.Rows.Count - 1
                        Dgl4.Item(Col2InvoiceNo, I).Tag = AgL.XNull(.Rows(I)("DocId"))
                        Dgl4.Item(Col2InvoiceNo, I).Value = AgL.XNull(.Rows(I)("InvoiceNo"))
                        Dgl4.Item(Col2InvoiceSr, I).Value = AgL.XNull(.Rows(I)("V_SNo"))
                        Dgl4.Item(Col2InvoiceDate, I).Value = ClsMain.FormatDate(AgL.XNull(DtTemp.Rows(I)("V_Date")))
                        Dgl4.Item(Col2TaxableAmount, I).Value = AgL.VNull(.Rows(I)("Inv_Amount"))
                        Dgl4.Item(Col2InvoiceAmount, I).Value = AgL.VNull(.Rows(I)("Bal_Amount"))
                        Dgl4.Item(Col2PInvoiceNo, I).Value = AgL.XNull(.Rows(I)("PInvNo"))
                        Dgl4.Item(Col2PWInvoiceAmount, I).Value = AgL.VNull(.Rows(I)("PWInvAmt"))
                    Next I
                End If
            End With



            If TxtDrCr.Text = "Credit" Then

                mQry = "
                    Select H.DocID, H.V_SNo, H.V_Date, H.AmtDr-IfNull(Adj.AdjAmt,0) as AmtDr, IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId As PaymentNo 
                    from Ledger H
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                                    
                    where Substr(H.V_Type,1,1) <> 'W' And H.DivCode = '" & AgL.PubDivCode & "' And   H.AmtDr>0 And H.SubCode = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtV_Date.Text)).ToString("s")) & " 
                    And H.AmtDr - IfNull(Adj.AdjAmt,0) >0
                    Order By H.V_Date
                    "

                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Else
                mQry = "
                    Select H.DocID, H.V_SNo, H.V_Date, H.AmtCr-IfNull(Adj.AdjAmt,0) as AmtDr, IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId As PaymentNo 
                    from Ledger H
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                                    
                    where Substr(H.V_Type,1,1) <> 'W' And H.DivCode = '" & AgL.PubDivCode & "' And  H.AmtCr>0 And H.SubCode = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtV_Date.Text)).ToString("s")) & " 
                    And H.AmtCr-IfNull(Adj.AdjAmt,0) >0
                    Order By H.V_Date
                    "

                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            End If

            With DtTemp
                Dgl3.RowCount = 1
                Dgl3.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl3.Rows.Add()
                        Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count - 1
                        Dgl3.Item(Col3Select, I).Value = "þ"
                        Dgl3.Item(Col3PaymentNo, I).Tag = AgL.XNull(.Rows(I)("DocId"))
                        Dgl3.Item(Col3PaymentNo, I).Value = AgL.XNull(.Rows(I)("PaymentNo"))
                        Dgl3.Item(Col3PaymentSr, I).Value = AgL.XNull(.Rows(I)("V_SNo"))
                        Dgl3.Item(Col3PaymentDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("V_Date")))
                        Dgl3.Item(Col3Amount, I).Value = AgL.VNull(.Rows(I)("AmtDr"))
                    Next I
                End If
            End With



            If TxtDrCr.Text = "Credit" Then

                mQry = "
                    Select H.DocID, H.V_SNo, H.V_Date, H.AmtDr-IfNull(Adj.AdjAmt,0) as AmtDr, IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId As PaymentNo 
                    from Ledger H
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                                    
                    where Substr(H.V_Type,1,1) = 'W' And H.DivCode = '" & AgL.PubDivCode & "' And   H.AmtDr>0 And H.SubCode = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtV_Date.Text)).ToString("s")) & " 
                    And H.AmtDr - IfNull(Adj.AdjAmt,0) >0
                    Order By H.V_Date
                    "

                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Else
                mQry = "
                    Select H.DocID, H.V_SNo, H.V_Date, H.AmtCr-IfNull(Adj.AdjAmt,0) as AmtDr, IfNull(H.DivCode,'') || H.Site_Code || '-' || H.V_Type || '-' || H.RecId As PaymentNo 
                    from Ledger H
                    Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Adj_DocID, Adj_V_Sno
                               Union All 
                               Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                               abs(Sum(Amount)) as AdjAmt 
                               From LedgerAdj LA
                               Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                               Group By Vr_DocID, Vr_V_Sno                    
                              ) as Adj On H.DocID = Adj.DocID And H.V_Sno = Adj.V_Sno                                    
                    where Substr(H.V_Type,1,1) = 'W' And H.DivCode = '" & AgL.PubDivCode & "' And  H.AmtCr>0 And H.SubCode = '" & TxtParty.Tag & "' 
                    And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(AgL.RetDate(TxtV_Date.Text)).ToString("s")) & " 
                    And H.AmtCr-IfNull(Adj.AdjAmt,0) >0
                    Order By H.V_Date
                    "

                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            End If

            With DtTemp
                Dgl5.RowCount = 1
                Dgl5.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl5.Rows.Add()
                        Dgl5.Item(ColSNo, I).Value = Dgl5.Rows.Count - 1
                        Dgl5.Item(Col3Select, I).Value = "þ"
                        Dgl5.Item(Col3PaymentNo, I).Tag = AgL.XNull(.Rows(I)("DocId"))
                        Dgl5.Item(Col3PaymentNo, I).Value = AgL.XNull(.Rows(I)("PaymentNo"))
                        Dgl5.Item(Col3PaymentSr, I).Value = AgL.XNull(.Rows(I)("V_SNo"))
                        Dgl5.Item(Col3PaymentDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("V_Date")))
                        Dgl5.Item(Col3Amount, I).Value = AgL.VNull(.Rows(I)("AmtDr"))
                    Next I
                End If
            End With



            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message & " [FillPendingData] ")
        End Try

    End Sub

    Private Sub Dgl2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellContentClick
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl2.CurrentCell.ColumnIndex
            bRowIndex = Dgl2.CurrentCell.RowIndex
            If Dgl2.Item(Col2InvoiceNo, bRowIndex).Value = "" Then Exit Sub
            Select Case Dgl2.Columns(e.ColumnIndex).Name
                Case Col2BtnAdjDetail
                    ShowSupplierSattlementAdjustment(bRowIndex)

                Case Col2BtnItemDetail
                    ShowSupplierSattlementInvoiceLines(bRowIndex)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl2_CellContentClick function")
        End Try
    End Sub

    Private Sub ShowSupplierSattlementAdjustment(mRow As Integer, Optional ShowDialog As Boolean = True)
        If Dgl2.Item(Col2BtnAdjDetail, mRow).Tag IsNot Nothing Then
            CType(Dgl2.Item(Col2BtnAdjDetail, mRow).Tag, FrmPartyAcSettlementInvoiceAdj).InvoiceNo = Dgl2.Item(Col2InvoiceNo, mRow).Value & "        Dated : " & Dgl2.Item(Col2InvoiceDate, mRow).Value
            CType(Dgl2.Item(Col2BtnAdjDetail, mRow).Tag, FrmPartyAcSettlementInvoiceAdj).InvoiceDocID = Dgl2.Item(Col2InvoiceNo, mRow).Tag
            CType(Dgl2.Item(Col2BtnAdjDetail, mRow).Tag, FrmPartyAcSettlementInvoiceAdj).EntryMode = Topctrl1.Mode
            If ShowDialog Then Dgl2.Item(Col2BtnAdjDetail, mRow).Tag.ShowDialog()
            Dgl2.Item(Col2SettlementAddition, mRow).Value = CType(Dgl2.Item(Col2BtnAdjDetail, mRow).Tag, FrmPartyAcSettlementInvoiceAdj).GetAdditions
            Dgl2.Item(Col2SettlementDeduction, mRow).Value = CType(Dgl2.Item(Col2BtnAdjDetail, mRow).Tag, FrmPartyAcSettlementInvoiceAdj).GetDeductions
        Else

            Dim FrmObj As FrmPartyAcSettlementInvoiceAdj
            FrmObj = New FrmPartyAcSettlementInvoiceAdj
            FrmObj.InvoiceNo = Dgl2.Item(Col2InvoiceNo, mRow).Value & "        Dated : " & Dgl2.Item(Col2InvoiceDate, mRow).Value
            FrmObj.InvoiceDocID = Dgl2.Item(Col2InvoiceNo, mRow).Tag
            FrmObj.IniGrid(mSearchCode, Val(Dgl2.Item(ColSNo, mRow).Tag))
            FrmObj.EntryMode = Topctrl1.Mode
            Dgl2.Item(Col2BtnAdjDetail, mRow).Tag = FrmObj
            If ShowDialog Then Dgl2.Item(Col2BtnAdjDetail, mRow).Tag.ShowDialog()
            Dgl2.Item(Col2SettlementAddition, mRow).Value = CType(Dgl2.Item(Col2BtnAdjDetail, mRow).Tag, FrmPartyAcSettlementInvoiceAdj).GetAdditions
            Dgl2.Item(Col2SettlementDeduction, mRow).Value = CType(Dgl2.Item(Col2BtnAdjDetail, mRow).Tag, FrmPartyAcSettlementInvoiceAdj).GetDeductions
        End If
        Calculation()

    End Sub

    Private Sub ShowSupplierSattlementInvoiceLines(mRow As Integer, Optional ShowDialog As Boolean = True)
        If Dgl2.Item(Col2BtnItemDetail, mRow).Tag IsNot Nothing Then
            CType(Dgl2.Item(Col2BtnItemDetail, mRow).Tag, FrmPartyAcSettlementInvoiceLine).InvoiceNo = Dgl2.Item(Col2InvoiceNo, mRow).Value & "        Dated : " & Dgl2.Item(Col2InvoiceDate, mRow).Value
            CType(Dgl2.Item(Col2BtnItemDetail, mRow).Tag, FrmPartyAcSettlementInvoiceLine).EntryMode = Topctrl1.Mode
            If ShowDialog Then Dgl2.Item(Col2BtnItemDetail, mRow).Tag.ShowDialog()
            Dgl2.Item(Col2ItemDeductions, mRow).Value = CType(Dgl2.Item(Col2BtnItemDetail, mRow).Tag, FrmPartyAcSettlementInvoiceLine).GetDeductions
        Else

            Dim FrmObj As FrmPartyAcSettlementInvoiceLine
            FrmObj = New FrmPartyAcSettlementInvoiceLine
            FrmObj.InvoiceNo = Dgl2.Item(Col2InvoiceNo, mRow).Value & "        Dated : " & Dgl2.Item(Col2InvoiceDate, mRow).Value

            FrmObj.IniGrid(mSearchCode, Val(Dgl2.Item(ColSNo, mRow).Tag), Dgl2.Item(Col2InvoiceNo, mRow).Tag)
            FrmObj.EntryMode = Topctrl1.Mode
            Dgl2.Item(Col2BtnItemDetail, mRow).Tag = FrmObj
            If ShowDialog Then Dgl2.Item(Col2BtnItemDetail, mRow).Tag.ShowDialog()
            Dgl2.Item(Col2ItemDeductions, mRow).Value = CType(Dgl2.Item(Col2BtnItemDetail, mRow).Tag, FrmPartyAcSettlementInvoiceLine).GetDeductions
        End If
        Calculation()

    End Sub

    Private Sub FrmClothSupplierSettlement_BaseEvent_Save_PreTrans(SearchCode As String) Handles Me.BaseEvent_Save_PreTrans
        Dim I As Integer
        If Topctrl1.Mode.ToUpper = "EDIT" Then
            For I = 0 To Dgl2.Rows.Count - 1
                If Val(Dgl2.Item(Col2ItemDeductions, I).Value) > 0 Then
                    If Dgl2.Item(Col2BtnItemDetail, I).Tag Is Nothing Then
                        ShowSupplierSattlementInvoiceLines(I, False)
                    End If
                End If

                If Val(Dgl2.Item(Col2SettlementAddition, I).Value) > 0 Or Val(Dgl2.Item(Col2SettlementDeduction, I).Value) > 0 Then
                    If Dgl2.Item(Col2BtnAdjDetail, I).Tag Is Nothing Then
                        ShowSupplierSattlementAdjustment(I, False)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub FrmClothSupplierSettlement_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList

    End Sub

    Private Sub FrmClothSupplierSettlement_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        If DtSettings.Rows.Count = 0 Then
            MsgBox("Settings are not defined. Can't Continue!")
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("LedgerHead", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub

    Private Sub Topctrl1_Load(sender As Object, e As EventArgs) Handles Topctrl1.Load
    End Sub


    Private Sub Dgl3_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgl3.MouseUp
        Try
            If e.Button = Windows.Forms.MouseButtons.Left Then
                If Dgl3.CurrentCell.ColumnIndex = Dgl3.Columns(Col3Select).Index Then
                    ClsMain.FManageTick(Dgl3, Dgl3.CurrentCell.ColumnIndex, Dgl3.Columns(Col3PaymentNo).Index)
                End If
            End If
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub Dgl5_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgl5.MouseUp
        Try
            If e.Button = Windows.Forms.MouseButtons.Left Then
                If Dgl5.CurrentCell.ColumnIndex = Dgl5.Columns(Col3Select).Index Then
                    ClsMain.FManageTick(Dgl5, Dgl5.CurrentCell.ColumnIndex, Dgl5.Columns(Col3PaymentNo).Index)
                End If
            End If
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuEMail.Click, MnuSendSms.Click, MnuImportPartPaymentFromDos.Click, MnuImportFinalPaymentFromDos.Click, MnuEditSave.Click
        Select Case sender.name
            Case MnuEMail.Name
                FGetPrint(ClsMain.PrintFor.EMail)
            Case MnuSendSms.Name
                FSendSms()
            Case MnuImportPartPaymentFromDos.Name
                FImportFromExcel(ImportFor.Dos, "Part")
            Case MnuImportFinalPaymentFromDos.Name
                FImportFromExcel(ImportFor.Dos, "Final")
            Case MnuEditSave.Name
                FEditSaveAllEntries()
        End Select
    End Sub
    Private Sub FGetPrint(mPrintFor As ClsMain.PrintFor)
        Dim dsMain As DataTable
        Dim dsInvoice As DataTable
        Dim dsPayment As DataTable
        Dim dsCompany As DataTable
        Dim mPrintTitle As String


        mPrintTitle = "Payment Settlement"

        mQry = "select H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as DocNo, H.V_Date, HSg.DispName as HSubcodeName, 
                HSg.Address, C.CityName, S.ManualCode as StateCode, S.Description as StateName, HSg.Mobile, HSg.Phone,
                (Select RegistrationNo From SubgroupRegistration Where RegistrationType='Sales Tax No' And Subcode=Hsg.Subcode) as PartyGSTNo, 
                (Select RegistrationNo From SubgroupRegistration Where RegistrationType='AADHAR NO' And Subcode=Hsg.Subcode) as PartyAadharNo,
                Agent.DispName as AgentName,
                H.UpToDate, H.DrCr, H.Remarks as HRemarks, 
                LSg.Name as LSubcodeName, L.Amount, L.ChqRefNo, L.ChqRefDate, L.Remarks as LRemarks, '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
                from ledgerHead H
                left Join LedgerHeadDetail L On H.DocID = L.DocId
                Left Join Subgroup HSg On H.Subcode = HSg.Subcode
                Left Join City C on HSg.CityCode = C.CityCode
                Left Join State S On C.State = S.Code
                Left Join Subgroup Lsg on L.Subcode = LSg.Subcode
                Left Join SubgroupSiteDivisionDetail SSDD On H.Subcode = SSDD.Subcode And H.Site_Code = SSDD.Site_Code And H.Div_Code = SSDD.Div_Code
                Left Join Subgroup Agent On SSDD.Agent = Agent.Subcode
                where H.DocID ='" & mSearchCode & "'"
        dsMain = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "select (Case When IH.PartyDocNo Is Null Then L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId Else IH.PartyDocNo End) As InvoiceNo,
                L.V_Date, H.InvoiceAmount, H.SettlementAddition, H.SettlementDeduction, H.LineDeduction,H.SettlementInvoiceAmount, H.SettlementRemark
                from Cloth_SupplierSettlementInvoices H
                Left Join LedgerHead LH On H.DocID = LH.DocId
                Left Join Ledger L On H.PurchaseInvoiceDocID =  L.DocID  And IfNull(H.PurchaseInvoiceDocIDSr,L.V_SNo) = L.V_SNo And LH.Subcode = L.Subcode
                Left Join LedgerHead IH On L.DocID = IH.DocID
                where H.DocID ='" & mSearchCode & "'"

        dsInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "select (Case When IH.PartyDocNo Is Null Then L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId Else IH.PartyDocNo End) As PaymentNo,
                L.V_Date, L.Narration, H.PaidAmount, cSg.Name as ContraName
                from Cloth_SupplierSettlementPayments H
                Left Join LedgerHead LH On H.DocID = LH.DocId
                Left Join Ledger L On H.PaymentDocId =  L.DocID And LH.Subcode = L.Subcode
                Left Join LedgerHead IH On L.DocID = IH.DocID
                Left Join viewHelpSubgroup cSg On L.ContraSub = cSg.Code                        
                Where H.DocID ='" & mSearchCode & "'
                "
        dsPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

        dsCompany = ClsMain.GetDocumentHeaderDataTable(TxtDivision.Tag, TxtSite_Code.Tag, TxtV_Type.Tag)


        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailCompose(AgL)
            objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
                    From ledgerHead H 
                    LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            objRepPrint.AttachmentName = "Payment Settlement"

            objRepPrint.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            objRepPrint.reportViewer1.ZoomMode = ZoomMode.Percent
            objRepPrint.reportViewer1.ZoomPercent = 50
        Else
            objRepPrint = New FrmRepPrint(AgL)
        End If

        objRepPrint.reportViewer1.Visible = True
        Dim id As Integer = 0
        objRepPrint.reportViewer1.ProcessingMode = ProcessingMode.Local
        dsMain.WriteXml(AgL.PubReportPath + "\PaymentSettlement_DsMain.xml")
        dsInvoice.WriteXml(AgL.PubReportPath + "\PaymentSettlement_DsInvoice.xml")
        dsPayment.WriteXml(AgL.PubReportPath + "\PaymentSettlement_DsPayment.xml")
        dsCompany.WriteXml(AgL.PubReportPath + "\PaymentSettlement_DsCompany.xml")
        objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\PaymentSettlement.rdl"

        If (dsMain.Rows.Count = 0) Then
            MsgBox("No records found to print.")
        End If
        Dim rds As New ReportDataSource("DsMain", dsMain)
        Dim rdsInvoice As New ReportDataSource("DsInvoice", dsInvoice)
        Dim rdsPayment As New ReportDataSource("DsPayment", dsPayment)
        Dim rdsCompany As New ReportDataSource("DsCompany", dsCompany)

        objRepPrint.reportViewer1.LocalReport.DataSources.Clear()
        objRepPrint.reportViewer1.LocalReport.DataSources.Add(rds)
        objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsInvoice)
        objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsPayment)
        objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsCompany)


        objRepPrint.reportViewer1.LocalReport.Refresh()
        objRepPrint.reportViewer1.RefreshReport()
        objRepPrint.MdiParent = Me.MdiParent
        objRepPrint.Show()
    End Sub

    Private Sub FSendSms()
        Dim FrmObj As FrmSendSms
        FrmObj = New FrmSendSms(AgL)
        FrmObj.TxtToMobile.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Mobile
                    From LedgerHead H 
                    LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                    Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()
    End Sub

    Public Sub FImportFromExcel(bImportFor As ImportFor, PaymentType As String)
        Dim mQry As String = ""
        Dim bHeadSubCodeName As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtLedgerHead As DataTable
        Dim DtLedgerHeadDetail As DataTable
        Dim DtLedgerHead_DataFields As DataTable
        Dim DtLedgerHeadDetail_DataFields As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Dr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Cr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Is Final Payment") & "' as [Field Name], 'Text' as [Data Type], 1 as [Length], 'Y/N' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Final Payment V_No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        DtLedgerHead_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amount") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        DtLedgerHeadDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportPurchaseFromExcel
        ObjFrmImport.Text = "Voucher Entry Import"
        ObjFrmImport.Dgl1.DataSource = DtLedgerHead_DataFields
        ObjFrmImport.Dgl2.DataSource = DtLedgerHeadDetail_DataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtLedgerHead = ObjFrmImport.P_DsExcelData_PurchInvoice.Tables(0)
        DtLedgerHeadDetail = ObjFrmImport.P_DsExcelData_PurchInvoiceDetail.Tables(0)


        If bImportFor = ImportFor.Dos Then
            ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
            Dim DtLedgerHead_Filtered As New DataTable
            DtLedgerHead_Filtered = DtLedgerHead.Clone
            Dim DtLedgerHeadRows_Filtered As DataRow() = DtLedgerHead.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('ZD','ZC','ZR','ZH')", "V_No")
            For I = 0 To DtLedgerHeadRows_Filtered.Length - 1
                DtLedgerHead_Filtered.ImportRow(DtLedgerHeadRows_Filtered(I))
            Next
            DtLedgerHead = DtLedgerHead_Filtered
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            For I = 0 To DtLedgerHeadDetail.Rows.Count - 1
                If DtLedgerHeadDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZR" Then
                    Dim DtRowLedgerHeadDetail_ForHeader As DataRow() = DtLedgerHead.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeadDetail.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "V_No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeadDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))))
                    If DtRowLedgerHeadDetail_ForHeader.Length > 0 Then
                        If AgL.XNull(DtRowLedgerHeadDetail_ForHeader(0)("final")).ToString.Trim = "Y" Then
                            DtLedgerHeadDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PS"
                        Else
                            DtLedgerHeadDetail.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PMT"
                        End If
                    End If
                ElseIf DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZD" Then
                    DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                ElseIf DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZC" Then
                    DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNC"
                End If
            Next

            For I = 0 To DtLedgerHead.Rows.Count - 1
                If DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZR" Then
                    If DtLedgerHead.Rows(I)("final").ToString.Trim = "Y" Then
                        DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PS"
                    Else
                        DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PMT"
                    End If
                ElseIf DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZD" Then
                    DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                ElseIf DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZC" Then
                    DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNS"
                End If

                If DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "ZH" Then
                    If AgL.VNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                        DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                    Else
                        DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNS"
                    End If
                End If
            Next
        End If


        ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
        Dim DtLedgerHead_Filtered_ForPaymentType As New DataTable
        DtLedgerHead_Filtered_ForPaymentType = DtLedgerHead.Clone
        Dim DtLedgerHeadRows_Filtered_ForPaymentType As DataRow() = Nothing
        If PaymentType = "Final" Then
            DtLedgerHeadRows_Filtered_ForPaymentType = DtLedgerHead.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('PS')", "V_No")
        ElseIf PaymentType = "Part" Then
            DtLedgerHeadRows_Filtered_ForPaymentType = DtLedgerHead.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('PMT')", "V_No")
        End If
        For I = 0 To DtLedgerHeadRows_Filtered_ForPaymentType.Length - 1
            DtLedgerHead_Filtered_ForPaymentType.ImportRow(DtLedgerHeadRows_Filtered_ForPaymentType(I))
        Next
        DtLedgerHead = DtLedgerHead_Filtered_ForPaymentType
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        Dim DtV_Date = DtLedgerHead.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Date"))
        For I = 0 To DtV_Date.Rows.Count - 1
            If AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) <> "" Then
                If CDate(AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))).Year < "2010" Then
                    If ErrorLog.Contains("These Dates are not valid") = False Then
                        ErrorLog += vbCrLf & "These Dates are not valid" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtV_Type = DtLedgerHead.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
        For I = 0 To DtV_Type.Rows.Count - 1
            If AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From Voucher_TYpe where V_Type = '" & AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Voucher Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Voucher Types Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtVendor = DtLedgerHead.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Vendor Name"))
        For I = 0 To DtVendor.Rows.Count - 1
            If AgL.XNull(DtVendor.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = " & AgL.Chk_Text(AgL.XNull(DtVendor.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))).ToString().Trim()) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Vendors Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Vendors Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtVendor.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtVendor.Rows(I)(GetFieldAliasName(bImportFor, "Vendor"))) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtLedgerHead_DataFields.Rows.Count - 1
            If AgL.XNull(DtLedgerHead_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtLedgerHead.Columns.Contains(AgL.XNull(DtLedgerHead_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedgerHead_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedgerHead_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
            End If
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            For I = 0 To DtLedgerHead.Rows.Count - 1
                bHeadSubCodeName = ""
                Dim VoucherEntryTableList(0) As StructLedgerHead
                Dim VoucherEntryTable As New StructLedgerHead

                If AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) = "PS" Or
                        AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) = "PMT" Then

                    VoucherEntryTable.DocID = ""
                    VoucherEntryTable.V_Type = AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))
                    VoucherEntryTable.V_Prefix = ""
                    VoucherEntryTable.V_Date = AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))
                    VoucherEntryTable.V_No = AgL.VNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                    VoucherEntryTable.Div_Code = AgL.PubDivCode
                    VoucherEntryTable.Site_Code = AgL.PubSiteCode
                    VoucherEntryTable.ManualRefNo = AgL.VNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                    VoucherEntryTable.Subcode = ""
                    If VoucherEntryTable.V_Type = "PS" Then
                        VoucherEntryTable.SubcodeName = AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Name"))).ToString.Trim
                    End If
                    VoucherEntryTable.Subcode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =   " & AgL.Chk_Text(VoucherEntryTable.SubcodeName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                    VoucherEntryTable.DrCr = ""

                    VoucherEntryTable.UptoDate = ""
                    VoucherEntryTable.Remarks = AgL.XNull(DtLedgerHead.Rows(I)("fv_no")).ToString.Trim

                    VoucherEntryTable.Status = "Active"
                    VoucherEntryTable.SalesTaxGroupParty = ""
                    VoucherEntryTable.PlaceOfSupply = ""
                    VoucherEntryTable.PartySalesTaxNo = ""
                    VoucherEntryTable.StructureCode = ""
                    VoucherEntryTable.CustomFields = ""
                    VoucherEntryTable.PartyDocNo = ""
                    VoucherEntryTable.PartyDocDate = ""
                    VoucherEntryTable.EntryBy = AgL.PubUserName
                    VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    VoucherEntryTable.ApproveBy = ""
                    VoucherEntryTable.ApproveDate = ""
                    VoucherEntryTable.MoveToLog = ""
                    VoucherEntryTable.MoveToLogDate = ""
                    VoucherEntryTable.UploadDate = ""

                    VoucherEntryTable.Gross_Amount = 0
                    VoucherEntryTable.Taxable_Amount = 0
                    VoucherEntryTable.Tax1_Per = 0
                    VoucherEntryTable.Tax1 = 0
                    VoucherEntryTable.Tax2_Per = 0
                    VoucherEntryTable.Tax2 = 0
                    VoucherEntryTable.Tax3_Per = 0
                    VoucherEntryTable.Tax3 = 0
                    VoucherEntryTable.Tax4_Per = 0
                    VoucherEntryTable.Tax4 = 0
                    VoucherEntryTable.Tax5_Per = 0
                    VoucherEntryTable.Tax5 = 0
                    VoucherEntryTable.SubTotal1 = 0
                    VoucherEntryTable.Deduction_Per = 0
                    VoucherEntryTable.Deduction = 0
                    VoucherEntryTable.Other_Charge_Per = 0
                    VoucherEntryTable.Other_Charge = 0
                    VoucherEntryTable.Round_Off = 0
                    VoucherEntryTable.Net_Amount = 0

                    Dim DtLedgerHeadDetail_ForHeader As New DataTable
                    For M = 0 To DtLedgerHeadDetail.Columns.Count - 1
                        Dim DColumn As New DataColumn
                        DColumn.ColumnName = DtLedgerHeadDetail.Columns(M).ColumnName
                        DtLedgerHeadDetail_ForHeader.Columns.Add(DColumn)
                    Next

                    Dim DtRowLedgerHeadDetail_ForHeader As DataRow() = DtLedgerHeadDetail.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHead.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "V_No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))))
                    If DtRowLedgerHeadDetail_ForHeader.Length > 0 Then
                        For M = 0 To DtRowLedgerHeadDetail_ForHeader.Length - 1
                            DtLedgerHeadDetail_ForHeader.Rows.Add()
                            For N = 0 To DtLedgerHeadDetail_ForHeader.Columns.Count - 1
                                DtLedgerHeadDetail_ForHeader.Rows(M)(N) = DtRowLedgerHeadDetail_ForHeader(M)(N)
                            Next
                        Next
                    End If

                    For J = 0 To DtLedgerHeadDetail_ForHeader.Rows.Count - 1
                        VoucherEntryTable.Line_Sr = J + 1
                        VoucherEntryTable.Line_SubCode = ""
                        VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim

                        If VoucherEntryTable.V_Type = "PMT" Then
                            VoucherEntryTable.SubcodeName = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                            VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "Vendor Name"))).ToString.Trim
                        End If

                        VoucherEntryTable.Line_SpecificationDocID = ""
                        VoucherEntryTable.Line_SpecificationDocIDSr = ""
                        VoucherEntryTable.Line_Specification = ""
                        VoucherEntryTable.Line_SalesTaxGroupItem = ""
                        VoucherEntryTable.Line_Qty = 0
                        VoucherEntryTable.Line_Unit = ""
                        VoucherEntryTable.Line_Rate = 0
                        VoucherEntryTable.Line_Amount = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amount")))
                        VoucherEntryTable.Line_ChqRefNo = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq No"))).ToString.Trim
                        VoucherEntryTable.Line_ChqRefDate = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq Date"))).ToString.Trim
                        VoucherEntryTable.Line_Remarks = ""
                        VoucherEntryTable.Line_Gross_Amount = 0
                        VoucherEntryTable.Line_Taxable_Amount = 0
                        VoucherEntryTable.Line_Tax1_Per = 0
                        VoucherEntryTable.Line_Tax1 = 0
                        VoucherEntryTable.Line_Tax2_Per = 0
                        VoucherEntryTable.Line_Tax2 = 0
                        VoucherEntryTable.Line_Tax3_Per = 0
                        VoucherEntryTable.Line_Tax3 = 0
                        VoucherEntryTable.Line_Tax4_Per = 0
                        VoucherEntryTable.Line_Tax4 = 0
                        VoucherEntryTable.Line_Tax5_Per = 0
                        VoucherEntryTable.Line_Tax5 = 0
                        VoucherEntryTable.Line_SubTotal1 = 0
                        VoucherEntryTable.Line_Deduction_Per = 0
                        VoucherEntryTable.Line_Deduction = 0
                        VoucherEntryTable.Line_Other_Charge_Per = 0
                        VoucherEntryTable.Line_Other_Charge = 0
                        VoucherEntryTable.Line_Round_Off = 0
                        VoucherEntryTable.Line_Net_Amount = 0

                        VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                        ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
                    Next

                    Dim SupplierSettlementInvoicesTableList(0) As StructSupplierSettlementInvoices
                    Dim SupplierSettlementPaymentsTableList(0) As StructSupplierSettlementPayments

                    If AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) = "PS" Then

                        Dim DtInvoiceDetail_ForHeader As New DataTable
                        For M = 0 To DtLedgerHead.Columns.Count - 1
                            Dim DColumn As New DataColumn
                            DColumn.ColumnName = DtLedgerHead.Columns(M).ColumnName
                            DtInvoiceDetail_ForHeader.Columns.Add(DColumn)
                        Next

                        Dim DtRowInvoiceDetail_ForHeader As DataRow() = DtLedgerHead.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))) + " And [" & GetFieldAliasName(bImportFor, "V_No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHead.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))), "V_No")
                        If DtRowInvoiceDetail_ForHeader.Length > 0 Then
                            For M = 0 To DtRowInvoiceDetail_ForHeader.Length - 1
                                DtInvoiceDetail_ForHeader.Rows.Add()
                                For N = 0 To DtInvoiceDetail_ForHeader.Columns.Count - 1
                                    DtInvoiceDetail_ForHeader.Rows(M)(N) = DtRowInvoiceDetail_ForHeader(M)(N)
                                Next
                            Next
                        End If


                        Dim bSr As Integer = 0

                        For K = 0 To DtInvoiceDetail_ForHeader.Rows.Count - 1
                            If AgL.XNull(DtInvoiceDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Final Payment V_No"))).ToString.Trim <> "" Then
                                Dim SupplierSettlementInvoicesTable As New StructSupplierSettlementInvoices

                                mQry = " Select H.DocId, H.V_Date, L.AmtCr
                            From LedgerHead H With (NoLock)
                            LEFT JOIN Ledger L With (NoLock) On H.DocId = L.DocId And L.SubCode = '" & VoucherEntryTable.Subcode & "'
                            Where Remarks = " & AgL.Chk_Text(AgL.XNull(DtInvoiceDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Final Payment V_No")))) & " 
                            And IfNull(L.AmtCr,0) > 0 "
                                Dim DtInvoiceInfo As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                                If DtInvoiceInfo.Rows.Count > 0 Then
                                    For A As Integer = 0 To DtInvoiceInfo.Rows.Count - 1
                                        bSr += 1
                                        SupplierSettlementInvoicesTable.Sr = bSr
                                        SupplierSettlementInvoicesTable.PurchaseInvoiceDocId = AgL.XNull(DtInvoiceInfo.Rows(A)("DocId"))
                                        SupplierSettlementInvoicesTable.InvoiceAmount = AgL.VNull(DtInvoiceInfo.Rows(A)("AmtCr"))
                                        SupplierSettlementInvoicesTable.SettlementAddition = 0
                                        SupplierSettlementInvoicesTable.SettlementDeduction = 0
                                        SupplierSettlementInvoicesTable.LineDeduction = 0
                                        'SupplierSettlementInvoicesTable.SettlementInvoiceAmount = AgL.VNull(DtInvoiceDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Amt Dr")))
                                        SupplierSettlementInvoicesTable.SettlementInvoiceAmount = AgL.VNull(DtInvoiceInfo.Rows(A)("AmtCr"))
                                        SupplierSettlementInvoicesTable.SettlementRemark = ""
                                        SupplierSettlementInvoicesTable.AdjustedAmount = 0


                                        SupplierSettlementInvoicesTableList(UBound(SupplierSettlementInvoicesTableList)) = SupplierSettlementInvoicesTable
                                        ReDim Preserve SupplierSettlementInvoicesTableList(UBound(SupplierSettlementInvoicesTableList) + 1)
                                    Next
                                End If

                                Dim SupplierSettlementPaymentsTable As New StructSupplierSettlementPayments

                                mQry = " Select H.DocId, H.V_Date, L.V_Sno As Sr, IfNull(L.AmtDr,0) As AmtDr
                            From LedgerHead H With (NoLock) 
                            LEFT JOIN Ledger L With (NoLock) On H.DocId = L.DocId And L.SubCode = '" & VoucherEntryTable.Subcode & "'
                            Where Remarks = " & AgL.Chk_Text(AgL.XNull(DtInvoiceDetail_ForHeader.Rows(K)(GetFieldAliasName(bImportFor, "Final Payment V_No")))) & " 
                            And IfNull(L.AmtDr,0) > 0 "
                                Dim DtPaymentInfo As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                                If DtPaymentInfo.Rows.Count > 0 Then
                                    For A As Integer = 0 To DtPaymentInfo.Rows.Count - 1
                                        bSr += 1
                                        SupplierSettlementPaymentsTable.Sr = bSr
                                        SupplierSettlementPaymentsTable.PaymentDocId = AgL.XNull(DtPaymentInfo.Rows(A)("DocId"))
                                        SupplierSettlementPaymentsTable.PaymentDocIdSr = AgL.VNull(DtPaymentInfo.Rows(A)("Sr"))
                                        SupplierSettlementPaymentsTable.PaidAmount = AgL.VNull(DtPaymentInfo.Rows(A)("AmtDr"))
                                        SupplierSettlementPaymentsTable.AdjustedAmount = AgL.VNull(DtPaymentInfo.Rows(A)("AmtDr"))

                                        SupplierSettlementPaymentsTableList(UBound(SupplierSettlementPaymentsTableList)) = SupplierSettlementPaymentsTable
                                        ReDim Preserve SupplierSettlementPaymentsTableList(UBound(SupplierSettlementPaymentsTableList) + 1)
                                    Next
                                End If
                            End If
                        Next
                    End If
                    InsertLedgerHead(VoucherEntryTableList, SupplierSettlementInvoicesTableList, SupplierSettlementPaymentsTableList)
                End If
            Next


            mQry = " Select VMain.DocId, Max(VMain.ManualRefNo) As ManualRefNo, IfNull(Sum(VMain.PaidAmount),0) As PaidAmount, 
                    IfNull(Sum(VMain.InvoiceAmount),0) As InvoiceAmount
                    From (
                        Select L.DocId, Max(H.ManualRefNo) As ManualRefNo, IfNull(Sum(L.Amount),0) As PaidAmount, 0 As InvoiceAmount From LedgerHead H With (NoLock) LEFT JOIN LedgerHeadDetail L With (NoLock) On H.DocId = L.DocId Where H.V_Type = 'PS' Group By L.DocId
                        UNION ALL 
                        Select L.DocId, Max(H.ManualRefNo) As ManualRefNo, IfNull(Sum(L.AdjustedAmount),0) As PaidAmount, 0 As InvoiceAmount From LedgerHead H With (NoLock) LEFT JOIN Cloth_SupplierSettlementPayments L With (NoLock) On H.DocId = L.DocId Where H.V_Type = 'PS' Group By L.DocId
                        UNION ALL 
                        Select L.DocId, Max(H.ManualRefNo) As ManualRefNo, 0 As PaidAmount, IfNull(Sum(L.SettlementInvoiceAmount),0) As InvoiceAmount From LedgerHead H With (NoLock) LEFT JOIN Cloth_SupplierSettlementInvoices L With (NoLock) On H.DocId = L.DocId Where H.V_Type = 'PS'  Group By L.DocId
                    ) As VMain
                    Group By VMain.DocId 
                    Having IfNull(Sum(VMain.PaidAmount),0) <> IfNull(Sum(VMain.InvoiceAmount),0)"
            Dim DtCheck As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            If DtCheck.Rows.Count > 0 Then
                If MsgBox("Some Entries have difference between Invoice amount and paid amount.Do you want to continue ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            AgL.ETrans.Commit()
            mTrans = "Commit"




            For A As Integer = 0 To DtCheck.Rows.Count - 1
                If AgL.VNull(DtCheck.Rows(A)("PaidAmount")) <> AgL.VNull(DtCheck.Rows(A)("InvoiceAmount")) Then
                    ErrorLog += vbCrLf + "Entry No :  " + AgL.XNull(DtCheck.Rows(A)("ManualRefNo")) + " Paid Amount " +
                        " and Invoice Amounts are not equal."
                End If
            Next


            If ErrorLog <> "" Then
                If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
                Else
                    File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
                End If
                System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
                Exit Sub
            End If

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub

    Private Function GetFieldAliasName(bImportFor As ImportFor, bFieldName As String)
        Dim bAliasName As String = bFieldName
        If bImportFor = ImportFor.Dos Then
            Select Case bFieldName

                Case "V_TYPE"
                    bAliasName = "V_TYPE"
                Case "V_NO"
                    bAliasName = "V_NO"
                Case "V_Date"
                    bAliasName = "V_DATE"
                Case "Ledger Account Name"
                    bAliasName = "bank_name"
                Case "Vendor Name"
                    bAliasName = "vendor"
                Case "Narration"
                    bAliasName = "narration"
                Case "Chq No"
                    bAliasName = "chq_no"
                Case "Chq Date"
                    bAliasName = "chq_date"
                Case "Amt Dr"
                    bAliasName = "dr"
                Case "Amt Cr"
                    bAliasName = "cr"
                Case "Final Payment V_No"
                    bAliasName = "fv_no"
            End Select

            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function
    Public Structure StructLedgerHead
        Dim DocID As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As String
        Dim Div_Code As String
        Dim Site_Code As String
        Dim ManualRefNo As String
        Dim Subcode As String
        Dim SubcodeName As String
        Dim DrCr As String
        Dim UptoDate As String
        Dim Remarks As String
        Dim Status As String
        Dim SalesTaxGroupParty As String
        Dim PlaceOfSupply As String
        Dim PartySalesTaxNo As String
        Dim StructureCode As String
        Dim CustomFields As String
        Dim PartyDocNo As String
        Dim PartyDocDate As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim ApproveBy As String
        Dim ApproveDate As String
        Dim MoveToLog As String
        Dim MoveToLogDate As String
        Dim UploadDate As String

        Dim Gross_Amount As Double
        Dim Taxable_Amount As Double
        Dim Tax1_Per As Double
        Dim Tax1 As Double
        Dim Tax2_Per As Double
        Dim Tax2 As Double
        Dim Tax3_Per As Double
        Dim Tax3 As Double
        Dim Tax4_Per As Double
        Dim Tax4 As Double
        Dim Tax5_Per As Double
        Dim Tax5 As Double
        Dim SubTotal1 As Double
        Dim Deduction_Per As Double
        Dim Deduction As Double
        Dim Other_Charge_Per As Double
        Dim Other_Charge As Double
        Dim Round_Off As Double
        Dim Net_Amount As Double

        '''''''''''''''''''''''''''''''''Line Detail''''''''''''''''''''''''''''''''''
        Dim Line_DocID As String
        Dim Line_Sr As String
        Dim Line_SubCode As String
        Dim Line_SubCodeName As String
        Dim Line_SpecificationDocID As String
        Dim Line_SpecificationDocIDSr As String
        Dim Line_Specification As String
        Dim Line_SalesTaxGroupItem As String
        Dim Line_Qty As String
        Dim Line_Unit As String
        Dim Line_Rate As String
        Dim Line_Amount As String
        Dim Line_ChqRefNo As String
        Dim Line_ChqRefDate As String
        Dim Line_Remarks As String

        Dim Line_Gross_Amount As Double
        Dim Line_Taxable_Amount As Double
        Dim Line_Tax1_Per As Double
        Dim Line_Tax1 As Double
        Dim Line_Tax2_Per As Double
        Dim Line_Tax2 As Double
        Dim Line_Tax3_Per As Double
        Dim Line_Tax3 As Double
        Dim Line_Tax4_Per As Double
        Dim Line_Tax4 As Double
        Dim Line_Tax5_Per As Double
        Dim Line_Tax5 As Double
        Dim Line_SubTotal1 As Double
        Dim Line_Deduction_Per As Double
        Dim Line_Deduction As Double
        Dim Line_Other_Charge_Per As Double
        Dim Line_Other_Charge As Double
        Dim Line_Round_Off As Double
        Dim Line_Net_Amount As Double
    End Structure
    Public Structure StructSupplierSettlementInvoices
        Dim Sr As String
        Dim PurchaseInvoiceDocId As String
        Dim InvoiceAmount As Double
        Dim SettlementAddition As Double
        Dim SettlementDeduction As Double
        Dim LineDeduction As Double
        Dim SettlementInvoiceAmount As Double
        Dim SettlementRemark As String
        Dim AdjustedAmount As Double
    End Structure
    Public Structure StructSupplierSettlementPayments
        Dim Sr As String
        Dim PaymentDocId As String
        Dim PaymentDocIdSr As String
        Dim PaidAmount As Double
        Dim AdjustedAmount As Double
    End Structure

    Private Sub InsertLedgerHead(LedgerHeadTableList As StructLedgerHead(),
                                 SupplierSettlementInvoicesTableList As StructSupplierSettlementInvoices(),
                                 SupplierSettlementPaymentsTableList As StructSupplierSettlementPayments())
        If LedgerHeadTableList(0).V_Type IsNot Nothing And LedgerHeadTableList(0).V_Type <> "" And SupplierSettlementPaymentsTableList.Length > 1 Then
            'LedgerHeadTableList(0).DocID = AgL.GetDocId(LedgerHeadTableList(0).V_Type, CStr(LedgerHeadTableList(0).V_No),
            '                                         CDate(LedgerHeadTableList(0).V_Date),
            '                                        IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), LedgerHeadTableList(0).Div_Code, LedgerHeadTableList(0).Site_Code)
            LedgerHeadTableList(0).DocID = AgL.CreateDocId(AgL, "LedgerHead", LedgerHeadTableList(0).V_Type, CStr(LedgerHeadTableList(0).V_No),
                                                     CDate(LedgerHeadTableList(0).V_Date),
                                                    IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), LedgerHeadTableList(0).Div_Code, LedgerHeadTableList(0).Site_Code)

            LedgerHeadTableList(0).V_Prefix = AgL.DeCodeDocID(LedgerHeadTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            LedgerHeadTableList(0).V_No = Val(AgL.DeCodeDocID(LedgerHeadTableList(0).DocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))

            If AgL.Dman_Execute("Select Count(*) From LedgerHead With (NoLock) Where V_Type = '" & LedgerHeadTableList(0).V_Type & "'
                        And ManualRefNo = '" & LedgerHeadTableList(0).ManualRefNo & "'
                        And Div_Code = '" & LedgerHeadTableList(0).Div_Code & "'
                        And Site_Code = '" & LedgerHeadTableList(0).Site_Code & "'
                            ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() > 0 Then
                Dim mManualrefNoPrefix As String = AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix With (NoLock) Where V_Type = '" & LedgerHeadTableList(0).V_Type & "' 
                                And " & AgL.Chk_Date(LedgerHeadTableList(0).V_Date) & " >= Date(Date_From) 
                                And " & AgL.Chk_Date(LedgerHeadTableList(0).V_Date) & " <= Date(Date_To) ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                LedgerHeadTableList(0).ManualRefNo = mManualrefNoPrefix + LedgerHeadTableList(0).V_No.ToString().PadLeft(4).Replace(" ", "0")
            End If

            LedgerHeadTableList(0).Subcode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =   " & AgL.Chk_Text(LedgerHeadTableList(0).SubcodeName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            LedgerHeadTableList(0).StructureCode = AgL.Dman_Execute("Select IfNull(Max(Structure),'') From Voucher_Type With (NoLock) Where V_Type = '" & LedgerHeadTableList(0).V_Type & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            If LedgerHeadTableList(0).SalesTaxGroupParty Is Nothing Or LedgerHeadTableList(0).SalesTaxGroupParty = "" Then
                LedgerHeadTableList(0).SalesTaxGroupParty = AgL.Dman_Execute("Select IfNull(SalesTaxPostingGroup,'') From Subgroup With (NoLock) Where SubCode = '" & LedgerHeadTableList(0).Subcode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            If LedgerHeadTableList(0).SalesTaxGroupParty Is Nothing Or LedgerHeadTableList(0).SalesTaxGroupParty = "" Then
                LedgerHeadTableList(0).SalesTaxGroupParty = "Unregistered"
            End If

            'If AgL.Dman_Execute("SELECT Count(*) From SaleInvoice where V_Type = '" & LedgerHeadTableList(0).V_Type & "' And ManualRefNo = '" & LedgerHeadTableList(0).ManualRefNo & "' ", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO LedgerHead (DocID,  V_Type, V_Prefix, V_Date, V_No,
                           Div_Code, Site_Code, ManualRefNo, Subcode, PartyName,
                           DrCr, UptoDate, Remarks, Status, SalesTaxGroupParty, PlaceOfSupply,
                           PartySalesTaxNo, Structure, CustomFields, PartyDocNo, PartyDocDate, EntryBy, EntryDate,
                           ApproveBy, ApproveDate, MoveToLog,
                           MoveToLogDate, UploadDate)
                            Select " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).V_Type) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).V_Prefix) & ",  
                            " & AgL.Chk_Date(LedgerHeadTableList(0).V_Date) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).V_No) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Div_Code) & ",
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Site_Code) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).ManualRefNo) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Subcode) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(0).SubcodeName) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(0).DrCr) & ", 
                            " & AgL.Chk_Date(LedgerHeadTableList(0).UptoDate) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Remarks) & ",
                            " & AgL.Chk_Text(LedgerHeadTableList(0).Status) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).SalesTaxGroupParty) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).PlaceOfSupply) & ", 
                            " & AgL.Chk_Text(LedgerHeadTableList(0).PartySalesTaxNo) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).StructureCode) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).CustomFields) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).PartyDocNo) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).PartyDocDate) & ",  
                            " & AgL.Chk_Text(LedgerHeadTableList(0).EntryBy) & ",    
                            " & AgL.Chk_Date(LedgerHeadTableList(0).EntryDate) & ",    
                            " & AgL.Chk_Text(LedgerHeadTableList(0).ApproveBy) & ",    
                            " & AgL.Chk_Date(LedgerHeadTableList(0).ApproveDate) & ",    
                            " & AgL.Chk_Text(LedgerHeadTableList(0).MoveToLog) & ",    
                            " & AgL.Chk_Date(LedgerHeadTableList(0).MoveToLogDate) & ",    
                            " & AgL.Chk_Date(LedgerHeadTableList(0).UploadDate) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            mQry = " INSERT INTO LedgerHeadCharges (DocID,  Gross_Amount,  Taxable_Amount,
                             Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
                             Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
                             Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount)
                             Select  " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ", 
                            " & Val(LedgerHeadTableList(0).Gross_Amount) & ",    
                             " & Val(LedgerHeadTableList(0).Taxable_Amount) & ",    
                             " & Val(LedgerHeadTableList(0).Tax1_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax1) & ",    
                             " & Val(LedgerHeadTableList(0).Tax2_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax2) & ",    
                             " & Val(LedgerHeadTableList(0).Tax3_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax3) & ",    
                             " & Val(LedgerHeadTableList(0).Tax4_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax4) & ",    
                             " & Val(LedgerHeadTableList(0).Tax5_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Tax5) & ",    
                             " & Val(LedgerHeadTableList(0).SubTotal1) & ",    
                             " & Val(LedgerHeadTableList(0).Deduction_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Deduction) & ",    
                             " & Val(LedgerHeadTableList(0).Other_Charge_Per) & ",    
                             " & Val(LedgerHeadTableList(0).Other_Charge) & ",    
                             " & Val(LedgerHeadTableList(0).Round_Off) & ",    
                             " & Val(LedgerHeadTableList(0).Net_Amount) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            'For I As Integer = 0 To LedgerHeadTableList.Length - 1
            '    If LedgerHeadTableList(I).Line_Amount IsNot Nothing And LedgerHeadTableList(I).Line_Amount <> 0 Then
            '        If Trim(LedgerHeadTableList(I).SubcodeName) <> Trim(LedgerHeadTableList(I).Line_SubCodeName) Then
            '            LedgerHeadTableList(I).Line_SubCode = AgL.Dman_Execute("SELECT Sg.SubCode FROM Subgroup Sg With (NoLock) Where Sg.Name =  '" & LedgerHeadTableList(I).Line_SubCodeName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            '            mQry = "Insert Into LedgerHeadDetail(DocId, Sr, Subcode, Specification, SalesTaxGroupItem, " &
            '           " Qty, Unit, Rate, Amount, ChqRefNo, ChqRefDate, Remarks, " &
            '           " SpecificationDocId, SpecificationDocIdSr)
            '        Select " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Sr) & ", 
            '        " & AgL.Chk_Text(LedgerHeadTableList(I).Line_SubCode) & ", 
            '        " & AgL.Chk_Text(LedgerHeadTableList(I).Line_Specification) & ", 
            '        " & AgL.Chk_Text(LedgerHeadTableList(I).Line_SalesTaxGroupItem) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Qty) & ", 
            '        " & AgL.Chk_Text(LedgerHeadTableList(I).Line_Unit) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Rate) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Amount) & ", 
            '        " & AgL.Chk_Text(LedgerHeadTableList(I).Line_ChqRefNo) & ", 
            '        " & AgL.Chk_Date(LedgerHeadTableList(I).Line_ChqRefDate) & ", 
            '        " & AgL.Chk_Text(LedgerHeadTableList(I).Line_Remarks) & ", 
            '        " & AgL.Chk_Text(LedgerHeadTableList(I).Line_SpecificationDocID) & ", 
            '        " & AgL.Chk_Text(LedgerHeadTableList(I).Line_SpecificationDocIDSr) & ""
            '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            '            mQry = "Insert Into LedgerHeadDetailCharges(DocID, Sr,  Gross_Amount,  Taxable_Amount,
            '        Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
            '        Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
            '        Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount)
            '        Select " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Sr) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Gross_Amount) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Taxable_Amount) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax1_Per) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax1) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax2_Per) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax2) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax3_Per) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax3) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax4_Per) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax4) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax5_Per) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Tax5) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_SubTotal1) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Deduction_Per) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Deduction) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Other_Charge_Per) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Other_Charge) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Round_Off) & ", 
            '        " & Val(LedgerHeadTableList(I).Line_Net_Amount) & ""
            '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            '        End If
            '    End If
            'Next

            For I As Integer = 0 To SupplierSettlementInvoicesTableList.Length - 1
                If SupplierSettlementInvoicesTableList(I).PurchaseInvoiceDocId IsNot Nothing And SupplierSettlementInvoicesTableList(I).PurchaseInvoiceDocId <> "" Then
                    mQry = "INSERT INTO Cloth_SupplierSettlementInvoices (DocID, Sr, PurchaseInvoiceDocId, InvoiceAmount,
                        SettlementAddition, SettlementDeduction, LineDeduction, SettlementInvoiceAmount,
                        SettlementRemark, AdjustedAmount )
                        Select " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ", 
                        " & Val(SupplierSettlementInvoicesTableList(I).Sr) & ", 
                        " & AgL.Chk_Text(SupplierSettlementInvoicesTableList(I).PurchaseInvoiceDocId) & ", 
                        " & Val(SupplierSettlementInvoicesTableList(I).InvoiceAmount) & ",
                        " & Val(SupplierSettlementInvoicesTableList(I).SettlementAddition) & ", 
                        " & Val(SupplierSettlementInvoicesTableList(I).SettlementDeduction) & ", 
                        " & Val(SupplierSettlementInvoicesTableList(I).LineDeduction) & ",
                        " & Val(SupplierSettlementInvoicesTableList(I).SettlementInvoiceAmount) & ", 
                        " & AgL.Chk_Text(SupplierSettlementInvoicesTableList(I).SettlementRemark) & ", 
                        " & Val(SupplierSettlementInvoicesTableList(I).AdjustedAmount) & ""
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next

            For I As Integer = 0 To SupplierSettlementPaymentsTableList.Length - 1
                If SupplierSettlementPaymentsTableList(I).PaymentDocId IsNot Nothing And SupplierSettlementPaymentsTableList(I).PaymentDocId <> "" Then
                    mQry = "INSERT INTO Cloth_SupplierSettlementPayments (DocID, Sr, PaymentDocId, PaymentDocIdSr, 
                        PaidAmount, AdjustedAmount )
                        Select " & AgL.Chk_Text(LedgerHeadTableList(0).DocID) & ", 
                        " & Val(SupplierSettlementPaymentsTableList(I).Sr) & ", 
                        " & AgL.Chk_Text(SupplierSettlementPaymentsTableList(I).PaymentDocId) & ", 
                        " & Val(SupplierSettlementPaymentsTableList(I).PaymentDocIdSr) & ", 
                        " & Val(SupplierSettlementPaymentsTableList(I).PaidAmount) & ", 
                        " & Val(SupplierSettlementPaymentsTableList(I).AdjustedAmount) & ""
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next

            AgL.UpdateVoucherCounter(LedgerHeadTableList(0).DocID, CDate(LedgerHeadTableList(0).V_Date), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
        End If
    End Sub

    Private Sub FEditSaveAllEntries()
        mFlag_Import = True
        For I As Integer = 0 To DTMaster.Rows.Count - 1
            BMBMaster.Position = I
            MoveRec()
            Topctrl1.FButtonClick(1)
            Calculation()
            Topctrl1.FButtonClick(13)
        Next
        mFlag_Import = False
    End Sub

    Private Sub FrmCustomerAcSettlementAadhat_BaseFunction_DispText() Handles Me.BaseFunction_DispText

    End Sub

    Private Sub Dgl3_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl3.CellEnter
        Dgl3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
    End Sub
    Private Sub FrmCustomerAcSettlementAadhat_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If ClsMain.IsEntryLockedWithLockText("LedgerHead", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub
End Class
