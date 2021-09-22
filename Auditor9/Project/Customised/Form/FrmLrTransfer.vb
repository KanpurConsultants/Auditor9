Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Imports System.Linq

Public Class FrmLrTransfer
    Inherits AgTemplate.TempTransaction1
    Dim mQry$

    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Select As String = "Tick"
    Public Const ColSNo As String = "S.No."
    Public Const Col1LrBaleNo As String = "Lr Bale No"
    Public Const Col1LrDate As String = "Lr Date"
    Public Const Col1Transporter As String = "Transporter"
    Public Const Col1FromLocation As String = "Current Location"
    Public Const Col1Remark As String = "Remark"
    '========================================================================

    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public Const Type As String = "Type"
    Public Const ToLocation As String = "To Location"
    Public Const FromDate As String = "From Date"
    Public Const ToDate As String = "To Date"
    Public Const Remarks As String = "Remarks"

    Dim rowEntryType As Integer = 0
    Dim rowToLocation As Integer = 1
    Dim rowFromDate As Integer = 2
    Dim rowToDate As Integer = 3
    Dim rowRemarks As Integer = 4

    Dim mPrevRowIndex As Integer = 0
    Dim DtV_TypeTrnSettings As DataTable

    Private Const Type_TransporterToGodown As String = "Transporter To Godown"
    Private Const Type_TransporterToMukhadim As String = "Transporter To Mukhadim"
    Private Const Type_GodownToShop As String = "Godown To Shop"
    Private Const Type_GodownToGodown As String = "Godown To Godown"
    Private Const Type_MukhadimToGodown As String = "Mukhadim To Godown"
    'Private Const Type_ShopToGodown As String = "Shop To Godown"
    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from StockHeadSetting H  With (NoLock) Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') Or H.V_Type Is Null "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found")
        End If
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLrTransfer))
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.LblCurrency = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LblNature = New System.Windows.Forms.Label()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox()
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.BtnFill = New System.Windows.Forms.Button()
        Me.BtnAttachments = New System.Windows.Forms.Button()
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
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(829, 581)
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
        Me.GBoxMoveToLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(205, 581)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(140, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(134, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxApprove.Location = New System.Drawing.Point(628, 581)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(168, 635)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 581)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 577)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBoxDivision.Location = New System.Drawing.Point(421, 581)
        Me.GBoxDivision.Size = New System.Drawing.Size(146, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Size = New System.Drawing.Size(140, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtDocId
        '
        Me.TxtDocId.AgSelectedValue = ""
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(336, 267)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 17)
        Me.TabControl1.Size = New System.Drawing.Size(992, 221)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Pnl2)
        Me.TP1.Controls.Add(Me.LblNature)
        Me.TP1.Controls.Add(Me.Panel3)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 195)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.PnlMain, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.Pnl2, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 1
        '
        'PnlMain
        '
        Me.PnlMain.Location = New System.Drawing.Point(1, 3)
        Me.PnlMain.Size = New System.Drawing.Size(490, 192)
        Me.PnlMain.TabIndex = 0
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(465, 209)
        Me.LblV_Type.Size = New System.Drawing.Size(86, 16)
        Me.LblV_Type.Tag = ""
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
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(4, 261)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 314)
        Me.Pnl1.TabIndex = 0
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(569, 222)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 715
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(332, 219)
        Me.LblCurrency.Name = "LblCurrency"
        Me.LblCurrency.Size = New System.Drawing.Size(60, 16)
        Me.LblCurrency.TabIndex = 735
        Me.LblCurrency.Text = "Currency"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 240)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(121, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Lr Bale Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNature
        '
        Me.LblNature.AutoSize = True
        Me.LblNature.BackColor = System.Drawing.Color.Transparent
        Me.LblNature.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNature.Location = New System.Drawing.Point(622, 163)
        Me.LblNature.Name = "LblNature"
        Me.LblNature.Size = New System.Drawing.Size(46, 16)
        Me.LblNature.TabIndex = 745
        Me.LblNature.Text = "Nature"
        Me.LblNature.Visible = False
        '
        'TxtCustomFields
        '
        Me.TxtCustomFields.AgAllowUserToEnableMasterHelp = False
        Me.TxtCustomFields.AgLastValueTag = Nothing
        Me.TxtCustomFields.AgLastValueText = Nothing
        Me.TxtCustomFields.AgMandatory = False
        Me.TxtCustomFields.AgMasterHelp = False
        Me.TxtCustomFields.AgNumberLeftPlaces = 8
        Me.TxtCustomFields.AgNumberNegetiveAllow = False
        Me.TxtCustomFields.AgNumberRightPlaces = 2
        Me.TxtCustomFields.AgPickFromLastValue = False
        Me.TxtCustomFields.AgRowFilter = ""
        Me.TxtCustomFields.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCustomFields.AgSelectedValue = Nothing
        Me.TxtCustomFields.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCustomFields.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCustomFields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCustomFields.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomFields.Location = New System.Drawing.Point(486, 636)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1011
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'GBoxImportFromExcel
        '
        Me.GBoxImportFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxImportFromExcel.BackColor = System.Drawing.Color.Transparent
        Me.GBoxImportFromExcel.Controls.Add(Me.BtnImprtFromExcel)
        Me.GBoxImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxImportFromExcel.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxImportFromExcel.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(678, 576)
        Me.GBoxImportFromExcel.Name = "GBoxImportFromExcel"
        Me.GBoxImportFromExcel.Size = New System.Drawing.Size(99, 47)
        Me.GBoxImportFromExcel.TabIndex = 1013
        Me.GBoxImportFromExcel.TabStop = False
        Me.GBoxImportFromExcel.Tag = "UP"
        Me.GBoxImportFromExcel.Text = "Import From Excel"
        Me.GBoxImportFromExcel.Visible = False
        '
        'BtnImprtFromExcel
        '
        Me.BtnImprtFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromExcel.Image = CType(resources.GetObject("BtnImprtFromExcel.Image"), System.Drawing.Image)
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(69, 11)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(25, 32)
        Me.BtnImprtFromExcel.TabIndex = 669
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(4, 201)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 145)
        Me.Panel3.TabIndex = 2
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(493, 3)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(491, 192)
        Me.Pnl2.TabIndex = 1
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.PnlCustomGrid.Location = New System.Drawing.Point(573, 600)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(33, 22)
        Me.PnlCustomGrid.TabIndex = 3019
        '
        'BtnFill
        '
        Me.BtnFill.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.ForeColor = System.Drawing.Color.Black
        Me.BtnFill.Location = New System.Drawing.Point(923, 238)
        Me.BtnFill.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(54, 22)
        Me.BtnFill.TabIndex = 1
        Me.BtnFill.TabStop = False
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFill.UseVisualStyleBackColor = True
        '
        'BtnAttachments
        '
        Me.BtnAttachments.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAttachments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAttachments.Location = New System.Drawing.Point(155, 600)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(34, 23)
        Me.BtnAttachments.TabIndex = 3020
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Add Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'FrmLrTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.BtnFill)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.MaximizeBox = True
        Me.Name = "FrmLrTransfer"
        Me.Text = "Lr Bale Transfer Entry"
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.BtnFill, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
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
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents LblCurrency As System.Windows.Forms.Label
    Public WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Public WithEvents LblNature As System.Windows.Forms.Label
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Public WithEvents Panel3 As System.Windows.Forms.Panel
    Private components As System.ComponentModel.IContainer
    Public mDimensionSrl As Integer
    Public WithEvents Pnl2 As Panel
    Public WithEvents PnlCustomGrid As Panel
    Protected WithEvents BtnFill As Button
    Protected WithEvents BtnAttachments As Button
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "StockHeadDetail"
        LogLineTableCsv = "StockHeadDetail_Log"

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        mQry = "Select DocID As SearchCode 
                From StockHead H  With (NoLock)
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  
                Where 1 = 1  " & mCondStr & "  Order By V_Date , V_No  "

        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [StockHead_Type], Cast(strftime('%d/%m/%Y', H.V_Date) As nvarchar) AS Date, SGV.Name AS [Party], " &
                            " H.ManualRefNo AS [Manual_No], H.Remarks,  " &
                            " H.EntryBy AS [Entry_By], Cast(strftime('%d/%m/%Y', H.EntryDate) As nvarchar) AS [Entry_Date] " &
                            " FROM StockHead H  With (NoLock) " &
                            " LEFT JOIN Voucher_Type Vt  With (NoLock) ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup SGV  With (NoLock) ON SGV.SubCode  = H.SubCode " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            If DtV_TypeSettings Is Nothing Then Exit Sub
            If DtV_TypeSettings.Rows.Count = 0 Then Exit Sub

            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Select, 50, 0, Col1Select, True, True, False)
            .AddAgTextColumn(Dgl1, Col1LrBaleNo, 100, 0, Col1LrBaleNo, True, True)
            .AddAgTextColumn(Dgl1, Col1LrDate, 120, 0, Col1LrDate, True, True)
            .AddAgTextColumn(Dgl1, Col1Transporter, 220, 255, Col1Transporter, True, True)
            .AddAgTextColumn(Dgl1, Col1FromLocation, 210, 255, Col1FromLocation, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 210, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl1)
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
        Dgl1.AgMandatoryColumn = Dgl1.Columns(Col1LrBaleNo).Index
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.Columns(Col1Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.BackgroundColor = Me.BackColor

        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If

        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None

        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col1Head, 150, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl2, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl2, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl2, Col1Value, 240, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        AgL.GridDesign(Dgl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 35
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AllowUserToAddRows = False
        Dgl2.RowHeadersVisible = False
        Dgl2.ColumnHeadersVisible = False
        Dgl2.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl2.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Dgl2.BackgroundColor = Me.BackColor
        Dgl2.BorderStyle = BorderStyle.None

        Dgl2.Rows.Add(5)
        For I = 0 To Dgl2.Rows.Count - 1
            Dgl2.Rows(I).Visible = False
        Next

        Dgl2.Item(Col1Head, rowEntryType).Value = Type
        Dgl2.Item(Col1Head, rowToLocation).Value = ToLocation
        Dgl2.Item(Col1Head, rowFromDate).Value = FromDate
        Dgl2.Item(Col1Head, rowToDate).Value = ToDate
        Dgl2.Item(Col1Head, rowRemarks).Value = Remarks
        Dgl2.Name = "Dgl2"
        Dgl2.Tag = "VerticalGrid"

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCustomGrid1.Name = "AgCustomGrid1"

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bStockSelectionQry$ = "", bHelpValuesSelectionQry$ = ""

        mQry = " Update StockHead " &
                " SET  " &
                " ManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", " &
                " Remarks = " & AgL.Chk_Text(Dgl2(Col1Value, rowRemarks).Value) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From StockHeadDetailTransfer Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From StockHeadDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From StockHeadDetail  With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Select, I).Value = "þ" Then
                mSr += 1
                InsertStockHeadDetail(mSearchCode, mSr, I, Conn, Cmd)
                InsertStock(mSearchCode, mSr, I, Conn, Cmd)
                InsertStockHeadDetailTransfer(mSearchCode, mSr, I, Conn, Cmd)
            End If
        Next

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub
    Private Sub InsertStockHeadDetail(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into StockHeadDetail(DocId, Sr, Barcode, Item, Qty, Unit, Remark) 
                Select " & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1LrBaleNo, LineGridRowIndex).Tag) & " As BarCode, " &
                " " & AgL.Chk_Text(ItemCode.LrBale) & " As Item, " &
                " 1 As Qty, " &
                " 'Nos' As Unit, " &
                " " & AgL.Chk_Text(Dgl1.Item("Remark", LineGridRowIndex).Value) & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO StockHeadDetailBarCodeValues (DocID, Sr, BarcodeLastTrnDocID, BarcodeLastTrnSr, 
                BarcodeLastTrnV_Type, BarcodeLastTrnManualRefNo, BarcodeLastTrnSubcode, BarcodeLastTrnProcess, 
                BarcodeCurrentGodown, BarcodeStatus)
                Select '" & DocID & "' As DocId, " & Sr & " As Sr, Bs.LastTrnDocID As BarcodeLastTrnDocID, 
                Bs.LastTrnSr As BarcodeLastTrnSr, Bs.LastTrnV_Type As BarcodeLastTrnV_Type, 
                Bs.LastTrnManualRefNo As BarcodeLastTrnManualRefNo, Bs.LastTrnSubcode As BarcodeLastTrnSubcode, 
                Bs.LastTrnProcess As BarcodeLastTrnProcess, Bs.CurrentGodown As BarcodeCurrentGodown, 
                Bs.Status As BarcodeStatus
                From Barcode B
                LEFT JOIN (SELECT * FROM BarcodeSiteDetail WHERE Div_Code = '" & AgL.PubDivCode & "' 
                            AND Site_Code = '" & AgL.PubSiteCode & "') AS Bs ON B.Code = Bs.Code
                Where B.Code = " & AgL.Chk_Text(Dgl1.Item(Col1LrBaleNo, LineGridRowIndex).Tag) & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub InsertStock(DocID As String, TSr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                Barcode, Item, Godown,
                EType_IR, Qty_Iss, Qty_Rec, Unit) 
                Select '" & DocID & "', " & TSr & ", 1 As Sr, " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1LrBaleNo, LineGridRowIndex).Tag) & ", 
                " & AgL.Chk_Text(ItemCode.LrBale) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1FromLocation, LineGridRowIndex).Tag) & ", 
                'I' As EType_IR, 1 As Qty_Iss, 0 As Qty_Rec, 
                'Nos' As Unit "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                Barcode, Item, Godown,
                EType_IR, Qty_Iss, Qty_Rec, Unit) 
                Select '" & DocID & "', " & TSr & ", 2 As Sr, " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",
                " & AgL.Chk_Date(DglMain.Item(Col1Value, rowV_Date).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_No).Value) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ",  " & AgL.Chk_Text(TxtDivision.Tag) & ", 
                " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1LrBaleNo, LineGridRowIndex).Tag) & ", 
                " & AgL.Chk_Text(ItemCode.LrBale) & ", 
                " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowToLocation).Tag) & ", 
                'R' As EType_IR, 0 As Qty_Iss, 1 As Qty_Rec, 
                'Nos' As Unit "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Dgl1.Item(Col1LrBaleNo, LineGridRowIndex).Tag <> "" Then
            mQry = " UPDATE BarCodeSiteDetail 
                    Set LastTrnDocID = " & AgL.Chk_Text(DocID) & ", 
                    LastTrnSr = " & TSr & ", 
                    LastTrnV_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Tag) & ", 
                    LastTrnManualRefNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowReferenceNo).Value) & ", 
                    LastTrnSubcode = Null, 
                    LastTrnProcess = Null, 
                    CurrentGodown = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowToLocation).Tag) & ", 
                    Status = 'Receive' 
                    Where Code = " & AgL.Chk_Text(Dgl1.Item(Col1LrBaleNo, LineGridRowIndex).Tag) & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub InsertStockHeadDetailTransfer(DocID As String, Sr As Integer, LineGridRowIndex As Integer, ByRef Conn As Object, ByRef Cmd As Object)
        mQry = "Insert Into StockHeadDetailTransfer(DocId, Sr, FromGodown, ToGodown) 
                Select " & AgL.Chk_Text(DocID) & ", " & Sr & ", " &
                " " & AgL.Chk_Text(Dgl1.Item(Col1FromLocation, LineGridRowIndex).Tag) & ", " &
                " " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowToLocation).Tag) & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl2RowCount As Integer
        Dim mDgl1ColumnCount As Integer
        Dim mDglMainRowCount As Integer
        Try

            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & Dgl2.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl2.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl2.Item(Col1Head, J).Value Then
                            Dgl2.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl2RowCount += 1
                            Dgl2.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl2.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If
            If mDgl2RowCount = 0 Then
                Dgl2.Visible = False
            Else
                Dgl2.Visible = True
            End If

            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & DglMain.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To DglMain.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DglMain.Item(Col1Head, J).Value Then
                            DglMain.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglMainRowCount += 1
                            DglMain.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                DglMain.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If
            If mDglMainRowCount = 0 Then DglMain.Visible = False Else DglMain.Visible = True

            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='" & Me.Name & "' And NCat = '" & NCAT & "' 
                    And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
                            Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False

        Dim DsMain As DataSet

        mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    End If
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found.")
        End If


        mQry = " Select H.*
                From (Select * From StockHead With (NoLock) Where DocID='" & SearchCode & "') H "
        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))

                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                DglMain.Item(Col1Value, rowReferenceNo).Value = AgL.XNull(.Rows(0)("ManualRefNo"))

                Dgl2(Col1Value, rowRemarks).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks")))


                AgCustomGrid1.FMoveRecFooterTable(DsMain.Tables(0))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select L.*, LrBale.LrBaleNo, LrBale.LrDate, Shdt.FromGodown, Shdt.ToGodown,
                        FSg.Name As FromGodownName, TSg.Name As ToGodownName, Transporter.Name As TransporterName
                        From (Select * From StockHeadDetail  With (NoLock)  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN LrBale LrBale On L.BarCode = LrBale.Code
                        LEFT JOIN StockHeadDetailTransfer Shdt On L.DocId = Shdt.DocId And L.Sr = Shdt.Sr
                        LEFT JOIN SubGroup FSg On Shdt.FromGodown = FSg.SubCode
                        LEFT JOIN SubGroup TSg On Shdt.ToGodown = TSg.SubCode
                        LEFT JOIN SubGroup Transporter On LrBale.Transporter = Transporter.SubCode
                        Order By L.Sr "
                DsMain = AgL.FillData(mQry, AgL.GCn)
                With DsMain.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsMain.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1Select, I).Value = "þ"
                            Dgl1.Item(Col1LrBaleNo, I).Tag = AgL.XNull(.Rows(I)("Barcode"))
                            Dgl1.Item(Col1LrBaleNo, I).Value = AgL.XNull(.Rows(I)("LrBaleNo"))
                            Dgl1.Item(Col1LrDate, I).Value = AgL.XNull(.Rows(I)("LrDate"))
                            Dgl1.Item(Col1FromLocation, I).Tag = AgL.XNull(.Rows(I)("FromGodown"))
                            Dgl1.Item(Col1FromLocation, I).Value = AgL.XNull(.Rows(I)("FromGodownName"))
                            Dgl1.Item(Col1Transporter, I).Value = AgL.XNull(.Rows(I)("TransporterName"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                        Next I

                        Dgl2(Col1Value, rowToLocation).Tag = AgL.XNull(.Rows(0)("ToGodown"))
                        Dgl2(Col1Value, rowToLocation).Value = AgL.XNull(.Rows(0)("ToGodownName"))
                    End If
                End With

                mQry = " SELECT Min(LrBale.LrDate) AS FromDate, Max(LrBale.LrDate) AS ToDate
                        FROM (Select * From StockHeadDetail Where DocId = '" & mSearchCode & "') As L 
                        LEFT JOIN LrBale LrBale ON L.Barcode = LrBale.Code "
                DsMain = AgL.FillData(mQry, AgL.GCn)
                With DsMain.Tables(0)
                    If .Rows.Count > 0 Then
                        Dgl2.Item(Col1Value, rowFromDate).Value = AgL.XNull(.Rows(0)("FromDate"))
                        Dgl2.Item(Col1Value, rowToDate).Value = AgL.XNull(.Rows(0)("ToDate"))
                    End If
                End With

                Dim mToLocationSubGroupType As String = AgL.XNull(AgL.Dman_Execute("Select SubGroupType From SubGroup Where SubCode = '" & Dgl2(Col1Value, rowToLocation).Tag & "'", AgL.GCn).ExecuteScalar())
                Dim mFromLocationSubGroupType As String = AgL.XNull(AgL.Dman_Execute("Select SubGroupType From SubGroup Where SubCode = '" & Dgl1.Item(Col1FromLocation, 0).Tag & "'", AgL.GCn).ExecuteScalar())

                If mFromLocationSubGroupType = SubgroupType.Transporter And mToLocationSubGroupType = SubgroupType.Godown Then
                    Dgl2.Item(Col1Value, rowEntryType).Value = Type_TransporterToGodown
                ElseIf mFromLocationSubGroupType = SubgroupType.Transporter And mToLocationSubGroupType = SubgroupType.Employee Then
                    Dgl2.Item(Col1Value, rowEntryType).Value = Type_TransporterToMukhadim
                ElseIf mFromLocationSubGroupType = SubgroupType.Godown And mToLocationSubGroupType = SubgroupType.Shop Then
                    Dgl2.Item(Col1Value, rowEntryType).Value = Type_GodownToShop
                ElseIf mFromLocationSubGroupType = SubgroupType.Godown And mToLocationSubGroupType = SubgroupType.Godown Then
                    Dgl2.Item(Col1Value, rowEntryType).Value = Type_GodownToGodown
                ElseIf mFromLocationSubGroupType = SubgroupType.Employee And mToLocationSubGroupType = SubgroupType.Godown Then
                    Dgl2.Item(Col1Value, rowEntryType).Value = Type_MukhadimToGodown
                    'ElseIf mFromLocationSubGroupType = SubgroupType.Shop And mToLocationSubGroupType = SubgroupType.Godown Then
                    '    Dgl2.Item(Col1Value, rowEntryType).Value = Type_ShopToGodown
                End If


                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
                '-------------------------------------------------------------
            End If
        End With
        ApplyUISettings(LblV_Type.Tag)
        SetAttachmentCaption()
        Dgl1.ReadOnly = True
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub
    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        'Dim FrmObj As New FrmLrTransferPartyDetail

        Try
            Select Case sender.NAME
                'Case TxtV_Type.Name
                '    If DglMain.Item(Col1Value, rowV_Type).Tag = "" Then Exit Sub

                '    mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
                '    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '    If DtV_TypeSettings.Rows.Count = 0 Then
                '        mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
                '        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '        If DtV_TypeSettings.Rows.Count = 0 Then
                '            mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                '            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '            If DtV_TypeSettings.Rows.Count = 0 Then
                '                mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                '                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '                If DtV_TypeSettings.Rows.Count = 0 Then
                '                    mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                '                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '                End If
                '            End If
                '        End If
                '    End If
                '    If DtV_TypeSettings.Rows.Count = 0 Then
                '        MsgBox("Voucher Type Settings Not Found, Can not continue.")
                '        Topctrl1.FButtonClick(14, True)
                '        Exit Sub
                '    End If

                '    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GcnRead)
                '    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                '    IniGrid()
                '    ApplyUISettings(LblV_Type.Tag)
                '    DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

                'Case TxtReferenceNo.Name
                '    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead",
                '                    DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                '                    DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                '                    DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmStockHeadEntry_BaseEvent_DglMainEditingControlValidating(sender As Object, e As CancelEventArgs) Handles Me.BaseEvent_DglMainEditingControlValidating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = DglMain.CurrentCell.RowIndex
        mColumn = DglMain.CurrentCell.ColumnIndex

        Select Case mRow
            Case rowV_Type
                If DglMain.Item(Col1Value, rowV_Type).Tag = "" Then Exit Sub


                TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GcnRead)
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                IniGrid()
                ApplyUISettings(LblV_Type.Tag)
                DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)

            Case rowReferenceNo
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead",
                                DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)
        End Select
    End Sub
    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            mQry = "Select * from StockHeadSetting  With (NoLock)  Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code Is Null "
            DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtV_TypeSettings.Rows.Count = 0 Then
                mQry = "Select * from StockHeadSetting With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code='" & AgL.PubSiteCode & "' "
                DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtV_TypeSettings.Rows.Count = 0 Then
                    mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Tag & "' And Div_Code  Is Null And Site_Code Is Null "
                    DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        mQry = "Select * from StockHeadSetting  With (NoLock) Where V_Type Is Null And Div_Code  Is Null And Site_Code Is Null "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    End If
                End If
            End If
        End If
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings Not Found, Can not continue.")
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(DglMain.Item(Col1Value, rowV_Type).Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        IniGrid()
        ApplyUISettings(LblV_Type.Tag)
        TabControl1.SelectedTab = TP1
        DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)


        Dgl1.ReadOnly = False

        SetAttachmentCaption()
    End Sub



    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1LrBaleNo).Index) Then passed = False : Exit Sub



        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1LrBaleNo, I).Value <> "" Then
                        If Dgl1.Item(Col1FromLocation, I).Tag = Dgl2.Item(Col1Value, rowToLocation).Tag Then
                            MsgBox("From location and to location should not be equal...!", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col1Value, rowToLocation)
                            passed = False
                            Exit Sub
                        End If
                    End If
                End If
            Next
        End With


        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead",
                                    DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue,
                                    DglMain.Item(Col1Value, rowSite_Code).Tag, Topctrl1.Mode,
                                    DglMain.Item(Col1Value, rowReferenceNo).Value, mSearchCode)

        If Not CheckDuplicateRef Then
            DglMain.Item(Col1Value, rowReferenceNo).Value = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", DglMain.Item(Col1Value, rowV_Type).Tag, DglMain.Item(Col1Value, rowV_Date).Value, TxtDivision.AgSelectedValue, DglMain.Item(Col1Value, rowSite_Code).Tag, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsAdvanceSearchOnItem")) = True Then
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Else
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        End If
        Try
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name

            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)

        'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmLrTransfer_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer
        If Dgl1.AgHelpDataSet(Col1LrBaleNo) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1LrBaleNo).Dispose() : Dgl1.AgHelpDataSet(Col1LrBaleNo) = Nothing

        For i = 0 To Dgl2.Rows.Count - 1
            Dgl2(Col1Head, i).Tag = Nothing
        Next
    End Sub
    Private Sub FrmSaleQuotation_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        Dim i As Integer

        GBoxImportFromExcel.Enabled = False


        For i = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(i).DefaultCellStyle.BackColor = Dgl1.AgReadOnlyColumnColor Then
                Dgl1.Columns(i).ReadOnly = True
            End If
        Next
    End Sub
    Private Sub FrmLrTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    End Sub
    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        Dim mRow = e.RowIndex
        Try
            If mPrevRowIndex <> e.RowIndex Then
                'FChangeOptions(mRow)
            End If
            mPrevRowIndex = mRow
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            Dgl1.CurrentCell = Dgl1.Item(Col1LrBaleNo, Dgl1.Rows.Count - 1) : Dgl1.Focus()
        End If
    End Sub

    Private Sub FrmLrTransfer_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        'For SSRS Print Out

        mQry = "SELECT H.DocID  FROM StockHead H With (NoLock)
                LEFT JOIN StockHeadDetail L With (NoLock) ON H.DocID = L.DocID 
                WHERE H.DocID ='" & SearchCode & "' And H.Gross_Amount > 0
                GROUP BY H.DocID 
                HAVING Sum(L.Amount)<>Max(H.Gross_Amount)"
        If AgL.FillData(mQry, AgL.GCn).Tables(0).Rows.Count > 0 Then
            MsgBox("Something went wrong with gross amount. Can not print Invoice. Please check once.")
            Exit Sub
        End If
    End Sub
    Private Sub FrmLrTransfer_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        mQry = " Select B.*
                From Barcode B With (NoLock)
                LEFT JOIN BarcodeSiteDetail Bs With (NoLock) On B.Code = Bs.Code
                Where B.Code In (Select Barcode from StockHeadDetail Where DocID = '" & mSearchCode & "' And Barcode Is Not Null)
                And Bs.LastTrnDocID <> '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            MsgBox("Bale No " + AgL.XNull(DtTemp.Rows(0)("Specification1")) + " processed to another Process.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If


        Dgl1.ReadOnly = False
    End Sub
    Private Sub Dgl1_DoubleClick(sender As Object, e As EventArgs) Handles Dgl1.DoubleClick
        If Topctrl1.Mode = "Browse" Then
            Dgl1.CurrentRow.Selected = True
        End If
    End Sub
    Private Sub Dgl2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl2.CurrentCell.ReadOnly = True
            End If

            If Dgl2.CurrentCell.ColumnIndex <> Dgl2.Columns(Col1Value).Index Then Exit Sub


            Dgl2.AgHelpDataSet(Dgl2.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case Dgl2.CurrentCell.RowIndex
                Case rowFromDate
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowToDate
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Dgl2_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl2.CurrentCell.RowIndex
            bColumnIndex = Dgl2.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl2.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl2.CurrentCell.RowIndex
                Case rowEntryType
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT '" & Type_TransporterToGodown & "' Code, '" & Type_TransporterToGodown & "' As Name "
                            mQry += " UNION ALL "
                            mQry += "SELECT '" & Type_TransporterToMukhadim & "' Code, '" & Type_TransporterToMukhadim & "' As Name "
                            mQry += " UNION ALL "
                            mQry += "SELECT '" & Type_GodownToShop & "' Code, '" & Type_GodownToShop & "' As Name "
                            mQry += " UNION ALL "
                            mQry += "SELECT '" & Type_GodownToGodown & "' Code, '" & Type_GodownToGodown & "' As Name "
                            mQry += " UNION ALL "
                            mQry += "SELECT '" & Type_MukhadimToGodown & "' Code, '" & Type_MukhadimToGodown & "' As Name "
                            'mQry += " UNION ALL "
                            'mQry += "SELECT '" & Type_ShopToGodown & "' Code, '" & Type_ShopToGodown & "' As Name "
                            Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl2.AgHelpDataSet(Col1Value) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowToLocation
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.Item(Col1Value, rowEntryType).Value IsNot Nothing And Dgl2.Item(Col1Value, rowEntryType).Value <> "" Then
                            If Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag Is Nothing Then
                                mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) "
                                If AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_TransporterToGodown) Then
                                    mQry += "Where SubgroupType ='" & SubgroupType.Godown & "'"
                                ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_TransporterToMukhadim) Then
                                    mQry += "Where SubgroupType ='" & SubgroupType.Employee & "'"
                                ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_GodownToShop) Then
                                    mQry += "Where SubgroupType ='" & SubgroupType.Shop & "'"
                                ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_GodownToGodown) Then
                                    mQry += "Where SubgroupType ='" & SubgroupType.Godown & "'"
                                ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_MukhadimToGodown) Then
                                    mQry += "Where SubgroupType ='" & SubgroupType.Godown & "'"
                                    'ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_ShopToGodown) Then
                                    '    mQry += "Where SubgroupType ='" & SubgroupType.Godown & "'"
                                End If
                                mQry += " Order By Name"
                                Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                            End If

                            If Dgl2.AgHelpDataSet(Col1Value) Is Nothing Then
                                Dgl2.AgHelpDataSet(Col1Value) = Dgl2.Item(Col1Head, Dgl2.CurrentCell.RowIndex).Tag
                            End If
                        Else
                            Dgl2(Col1Head, rowToLocation).Tag = Nothing
                            MsgBox("Select Type First...!", MsgBoxStyle.Information)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl2_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = Dgl2.CurrentCell.RowIndex
        mColumn = Dgl2.CurrentCell.ColumnIndex
        If mColumn = Dgl2.Columns(Col1Value).Index Then
            If Dgl2.Item(Col1Mandatory, mRow).Value <> "" Then
                If Dgl2(Col1Value, mRow).Value = "" Then
                    MsgBox(Dgl2(Col1Head, mRow).Value & " can Not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If

        Select Case Dgl2.CurrentCell.RowIndex
            Case rowEntryType
                Dgl2(Col1Head, rowToLocation).Tag = Nothing
                Dgl2(Col1Value, rowToLocation).Tag = Nothing
                Dgl2(Col1Value, rowToLocation).Value = ""
            Case rowRemarks
                If Dgl1.Rows.Count > 1 Then
                    If Dgl1.Item(Col1LrBaleNo, 1).Value <> "" Then
                        FHPGD_PendingLR()
                    End If
                End If
        End Select



    End Sub
    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.LblDocNo.Text = "Document No. : " + DglMain.Item(Col1Value, rowReferenceNo).Value
        FrmObj.SearchCode = mSearchCode
        FrmObj.TableName = "SubGroupAttachments"
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()
        FrmObj.Dispose()
        FrmObj = Nothing
        SetAttachmentCaption()
    End Sub
    Private Sub SetAttachmentCaption()
        Dim AttachmentPath As String = PubAttachmentPath + mSearchCode + "\"
        If Directory.Exists(AttachmentPath) Then
            Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
            If FileCount > 0 Then BtnAttachments.Text = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else BtnAttachments.Text = "Attachments"
        Else
            BtnAttachments.Text = "Attachments"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnFill.Click
        FHPGD_PendingLR()
    End Sub
    Private Sub Dgl1_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgl1.MouseUp
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Select).Index Then
                            ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1LrBaleNo).Index)
                        End If
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1LrBaleNo).Index)
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub FHPGD_PendingLR(Optional ItemCode As String = "")
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " Select 'o' As Tick, H.Code, H.LrBaleNo, H.LrDate, 
                Sg1.Name As TransporterName
                From LrBale H
                LEFT JOIN (Select * From LrBaleSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "' 
                                        And Div_Code = '" & AgL.PubDivCode & "') As L On H.Code = L.Code
                LEFT JOIN SubGroup Sg On L.CurrentGodown = Sg.SubCode 
                LEFT JOIN SubGroup Sg1 On H.Transporter = Sg1.SubCode "


        If AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_TransporterToGodown) Then
            mQry += "Where Sg.SubgroupType ='" & SubgroupType.Transporter & "'"
        ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_TransporterToMukhadim) Then
            mQry += "Where Sg.SubgroupType ='" & SubgroupType.Transporter & "'"
        ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_GodownToShop) Then
            mQry += "Where Sg.SubgroupType ='" & SubgroupType.Godown & "'"
        ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_GodownToGodown) Then
            mQry += "Where Sg.SubgroupType ='" & SubgroupType.Godown & "'"
        ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_MukhadimToGodown) Then
            mQry += "Where Sg.SubgroupType ='" & SubgroupType.Employee & "'"
            'ElseIf AgL.StrCmp(Dgl2.Item(Col1Value, rowEntryType).Value, Type_ShopToGodown) Then
            '    mQry += "Where Sg.SubgroupType ='" & SubgroupType.Shop & "'"
        End If

        If Dgl2.Item(Col1Value, rowFromDate).Value <> "" Then mQry += " And Date(H.LrDate) >= '" & Dgl2.Item(Col1Value, rowFromDate).Value & "'"
        If Dgl2.Item(Col1Value, rowToDate).Value <> "" Then mQry += " And Date(H.LrDate) <= '" & Dgl2.Item(Col1Value, rowToDate).Value & "'"

        mQry += " Order By H.LrDate, H.LrBaleNo "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 530, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Lr Bale No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Lr Bale Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Transporter", 200, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FRH_Multiple = Nothing

        Dim I As Integer = 0
        If StrRtn <> "" Then
            mQry = " Select H.Code, H.LrBaleNo, H.LrDate, L.CurrentGodown, Sg.Name As CurrentGodownName,
                Sg1.Name As TransporterName
                From LrBale H
                LEFT JOIN (Select * From LrBaleSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "' 
                                        And Div_Code = '" & AgL.PubDivCode & "') As L On H.Code = L.Code
                LEFT JOIN SubGroup Sg On L.CurrentGodown = Sg.SubCode 
                LEFT JOIN SubGroup Sg1 On H.Transporter = Sg1.SubCode
                wHERE H.Code In (" & StrRtn & ")
                Order By H.LrDate, H.LrBaleNo "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1Select, I).Value = "þ"
                    Dgl1.Item(Col1LrBaleNo, I).Tag = AgL.XNull(DtTemp.Rows(I)("Code"))
                    Dgl1.Item(Col1LrBaleNo, I).Value = AgL.XNull(DtTemp.Rows(I)("LrBaleNo"))
                    Dgl1.Item(Col1LrDate, I).Value = AgL.XNull(DtTemp.Rows(I)("LrDate"))
                    Dgl1.Item(Col1Transporter, I).Value = AgL.XNull(DtTemp.Rows(I)("TransporterName"))
                    Dgl1.Item(Col1FromLocation, I).Tag = AgL.XNull(DtTemp.Rows(I)("CurrentGodown"))
                    Dgl1.Item(Col1FromLocation, I).Value = AgL.XNull(DtTemp.Rows(I)("CurrentGodownName"))
                Next I
            End If
        End If
    End Sub

    Private Sub FrmLrTransfer_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From StockHeadDetailTransfer Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Select L.Barcode, Lb.BarcodeLastTrnDocID, Lb.BarcodeLastTrnSr, Lb.BarcodeLastTrnV_Type, 
                Lb.BarcodeLastTrnManualRefNo, Lb.BarcodeLastTrnSubcode, Lb.BarcodeLastTrnProcess, 
                Lb.BarcodeCurrentGodown, Lb.BarcodeStatus
                From StockHeadDetail L with (NoLock)
                LEFT JOIN StockHeadDetailBarCodeValues Lb with (NoLock) On L.DocId = Lb.DocId And L.Sr = Lb.Sr
                Where L.DocId = '" & mSearchCode & "' And L.Barcode Is Not Null "
        Dim DtLastTransaction As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        For I As Integer = 0 To DtLastTransaction.Rows.Count - 1
            mQry = " UPDATE BarcodeSiteDetail Set  
                    LastTrnDocID = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("BarcodeLastTrnDocID"))) & ", 
                    LastTrnSr = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("BarcodeLastTrnSr"))) & ", 
                    LastTrnV_Type = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("BarcodeLastTrnV_Type"))) & ", 
                    LastTrnManualRefNo = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("BarcodeLastTrnManualRefNo"))) & ", 
                    LastTrnSubcode = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("BarcodeLastTrnSubcode"))) & ", 
                    LastTrnProcess = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("BarcodeLastTrnProcess"))) & ", 
                    CurrentGodown = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("BarcodeCurrentGodown"))) & ", 
                    Status = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("BarcodeStatus"))) & " 
                    Where Code = " & AgL.Chk_Text(AgL.XNull(DtLastTransaction.Rows(I)("Barcode"))) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Next

        mQry = " Delete From StockHeadDetailBarCodeValues Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmLrTransfer_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        mQry = " Select B.*
                From Barcode B With (NoLock)
                LEFT JOIN BarcodeSiteDetail Bs With (NoLock) On B.Code = Bs.Code
                Where B.Code In (Select Barcode from StockHeadDetail Where DocID = '" & mSearchCode & "' And Barcode Is Not Null)
                And Bs.LastTrnDocID <> '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            MsgBox("Bale No " + AgL.XNull(DtTemp.Rows(0)("Specification1")) + " processed to another Process.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If
    End Sub
    Private Sub FrmStockHeadEntry_BaseEvent_DglMainEditingControlKeyDown(sender As Object, e As KeyEventArgs) Handles Me.BaseEvent_DglMainEditingControlKeyDown
        Try
            Dim mRow As Integer
            Dim mColumn As Integer
            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(DglMain)
                If DglMain.CurrentCell.RowIndex = LastCell.RowIndex And DglMain.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                    If Dgl2.Visible Then
                        Dgl2.CurrentCell = Dgl2.FirstDisplayedCell
                        Dgl2.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl2_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(Dgl2)
                If Dgl2.CurrentCell.RowIndex = LastCell.RowIndex And Dgl2.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                    If Dgl1.Visible Then
                        Dgl1.CurrentCell = Dgl1.FirstDisplayedCell
                        Dgl1.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmLrTransfer_BaseEvent_Approve_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_Approve_InTrans

    End Sub
End Class
