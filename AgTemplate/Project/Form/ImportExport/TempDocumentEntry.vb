Imports System.Data.SQLite
Public Class TempDocumentEntry
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1AmendDate As String = "Amend Date"
    Protected Const Col1ExpiryDateImport As String = "Expiry Date Import"
    Protected Const Col1ExpiryDateExport As String = "Expiry Date Export"

    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2Type As String = "Type"
    Protected Const Col2ItemImportExportGroup As String = "Item Import Export Group"
    Protected Const Col2Qty As String = "Qty"
    Protected Const Col2Unit As String = "Unit"
    Protected Const Col2AmountFC As String = "Amount (FC)"
    Protected Const Col2AmountINR As String = "Amount (INR)"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtDocumentName = New AgControls.AgTextBox
        Me.LblDocumentName = New System.Windows.Forms.Label
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.LblOpenedDateReq = New System.Windows.Forms.Label
        Me.TxtOpenedDate = New AgControls.AgTextBox
        Me.LblOpenedDate = New System.Windows.Forms.Label
        Me.LblDocumentNameReq = New System.Windows.Forms.Label
        Me.LblDocumentNoReq = New System.Windows.Forms.Label
        Me.TxtDocumentNo = New AgControls.AgTextBox
        Me.LblDocumentNo = New System.Windows.Forms.Label
        Me.TxtCurrency = New AgControls.AgTextBox
        Me.LblCurrency = New System.Windows.Forms.Label
        Me.TxtFileNo = New AgControls.AgTextBox
        Me.LblFileNo = New System.Windows.Forms.Label
        Me.TxtFileDate = New AgControls.AgTextBox
        Me.LblFileDate = New System.Windows.Forms.Label
        Me.TxtTollerance = New AgControls.AgTextBox
        Me.LblTollerance = New System.Windows.Forms.Label
        Me.TxtPeriod = New AgControls.AgTextBox
        Me.LblPreiod = New System.Windows.Forms.Label
        Me.TxtImpPayMode = New AgControls.AgTextBox
        Me.LblImpPayMode = New System.Windows.Forms.Label
        Me.TxtExpPayMode = New AgControls.AgTextBox
        Me.LblExpPayMode = New System.Windows.Forms.Label
        Me.LblPartyNameReq = New System.Windows.Forms.Label
        Me.TxtPartyName = New AgControls.AgTextBox
        Me.LblPartyName = New System.Windows.Forms.Label
        Me.TxtBankName = New AgControls.AgTextBox
        Me.LblBankName = New System.Windows.Forms.Label
        Me.TxtRemark = New AgControls.AgTextBox
        Me.LblRemark = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtImportExpiryDate = New AgControls.AgTextBox
        Me.LblImportExpiryDate = New System.Windows.Forms.Label
        Me.TxtExportExpiryDate = New AgControls.AgTextBox
        Me.LblExportExpiryDate = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(756, 475)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(596, 475)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(421, 475)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(145, 475)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(11, 475)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 471)
        Me.GroupBox1.Size = New System.Drawing.Size(1010, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(287, 475)
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
        Me.TxtDocId.Location = New System.Drawing.Point(803, 227)
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblV_No
        '
        Me.LblV_No.Location = New System.Drawing.Point(297, 13)
        Me.LblV_No.Tag = ""
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(406, 13)
        Me.TxtV_No.Size = New System.Drawing.Size(122, 18)
        Me.TxtV_No.TabIndex = 1
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(133, 19)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(18, 14)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(709, 233)
        Me.LblV_TypeReq.Tag = ""
        Me.LblV_TypeReq.Visible = False
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(148, 13)
        Me.TxtV_Date.Size = New System.Drawing.Size(143, 18)
        Me.TxtV_Date.TabIndex = 0
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(629, 229)
        Me.LblV_Type.Size = New System.Drawing.Size(88, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Voucher Type"
        Me.LblV_Type.Visible = False
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(359, 3)
        Me.TxtV_Type.Size = New System.Drawing.Size(25, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        Me.TxtV_Type.Visible = False
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(584, 227)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(473, 227)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        Me.LblSite_Code.Visible = False
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(600, 225)
        Me.TxtSite_Code.Size = New System.Drawing.Size(23, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        Me.TxtSite_Code.Visible = False
        '
        'LblDocId
        '
        Me.LblDocId.Location = New System.Drawing.Point(756, 229)
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(858, 228)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 19)
        Me.TabControl1.Size = New System.Drawing.Size(998, 228)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Pnl1)
        Me.TP1.Controls.Add(Me.TxtExportExpiryDate)
        Me.TP1.Controls.Add(Me.LblExportExpiryDate)
        Me.TP1.Controls.Add(Me.TxtImportExpiryDate)
        Me.TP1.Controls.Add(Me.LblImportExpiryDate)
        Me.TP1.Controls.Add(Me.TxtRemark)
        Me.TP1.Controls.Add(Me.LblRemark)
        Me.TP1.Controls.Add(Me.TxtBankName)
        Me.TP1.Controls.Add(Me.LblBankName)
        Me.TP1.Controls.Add(Me.LblPartyNameReq)
        Me.TP1.Controls.Add(Me.TxtPartyName)
        Me.TP1.Controls.Add(Me.LblPartyName)
        Me.TP1.Controls.Add(Me.TxtExpPayMode)
        Me.TP1.Controls.Add(Me.LblExpPayMode)
        Me.TP1.Controls.Add(Me.TxtImpPayMode)
        Me.TP1.Controls.Add(Me.LblImpPayMode)
        Me.TP1.Controls.Add(Me.TxtTollerance)
        Me.TP1.Controls.Add(Me.LblTollerance)
        Me.TP1.Controls.Add(Me.TxtPeriod)
        Me.TP1.Controls.Add(Me.LblPreiod)
        Me.TP1.Controls.Add(Me.TxtFileDate)
        Me.TP1.Controls.Add(Me.LblFileDate)
        Me.TP1.Controls.Add(Me.TxtFileNo)
        Me.TP1.Controls.Add(Me.LblFileNo)
        Me.TP1.Controls.Add(Me.TxtCurrency)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.LblDocumentNoReq)
        Me.TP1.Controls.Add(Me.TxtDocumentNo)
        Me.TP1.Controls.Add(Me.LblDocumentNo)
        Me.TP1.Controls.Add(Me.LblDocumentNameReq)
        Me.TP1.Controls.Add(Me.LblOpenedDateReq)
        Me.TP1.Controls.Add(Me.TxtOpenedDate)
        Me.TP1.Controls.Add(Me.LblOpenedDate)
        Me.TP1.Controls.Add(Me.TxtDocumentName)
        Me.TP1.Controls.Add(Me.LblDocumentName)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(990, 202)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocumentName, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocumentName, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblOpenedDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtOpenedDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblOpenedDateReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocumentNameReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocumentNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocumentNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocumentNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFileNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFileNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFileDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFileDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPreiod, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPeriod, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblTollerance, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtTollerance, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblImpPayMode, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtImpPayMode, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblExpPayMode, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtExpPayMode, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyName, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyNameReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBankName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBankName, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblRemark, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemark, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblImportExpiryDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtImportExpiryDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblExportExpiryDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtExportExpiryDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.Pnl1, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(992, 41)
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
        'TxtDocumentName
        '
        Me.TxtDocumentName.AgMandatory = True
        Me.TxtDocumentName.AgMasterHelp = False
        Me.TxtDocumentName.AgNumberLeftPlaces = 8
        Me.TxtDocumentName.AgNumberNegetiveAllow = False
        Me.TxtDocumentName.AgNumberRightPlaces = 2
        Me.TxtDocumentName.AgPickFromLastValue = False
        Me.TxtDocumentName.AgRowFilter = ""
        Me.TxtDocumentName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDocumentName.AgSelectedValue = Nothing
        Me.TxtDocumentName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDocumentName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDocumentName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDocumentName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDocumentName.Location = New System.Drawing.Point(148, 33)
        Me.TxtDocumentName.MaxLength = 50
        Me.TxtDocumentName.Name = "TxtDocumentName"
        Me.TxtDocumentName.Size = New System.Drawing.Size(143, 18)
        Me.TxtDocumentName.TabIndex = 2
        '
        'LblDocumentName
        '
        Me.LblDocumentName.AutoSize = True
        Me.LblDocumentName.BackColor = System.Drawing.Color.Transparent
        Me.LblDocumentName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocumentName.Location = New System.Drawing.Point(18, 34)
        Me.LblDocumentName.Name = "LblDocumentName"
        Me.LblDocumentName.Size = New System.Drawing.Size(105, 16)
        Me.LblDocumentName.TabIndex = 706
        Me.LblDocumentName.Text = "Document Name"
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl2.Location = New System.Drawing.Point(7, 256)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(976, 213)
        Me.Pnl2.TabIndex = 1
        '
        'LblOpenedDateReq
        '
        Me.LblOpenedDateReq.AutoSize = True
        Me.LblOpenedDateReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblOpenedDateReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblOpenedDateReq.Location = New System.Drawing.Point(133, 59)
        Me.LblOpenedDateReq.Name = "LblOpenedDateReq"
        Me.LblOpenedDateReq.Size = New System.Drawing.Size(10, 7)
        Me.LblOpenedDateReq.TabIndex = 732
        Me.LblOpenedDateReq.Text = "Ä"
        '
        'TxtOpenedDate
        '
        Me.TxtOpenedDate.AgMandatory = True
        Me.TxtOpenedDate.AgMasterHelp = False
        Me.TxtOpenedDate.AgNumberLeftPlaces = 8
        Me.TxtOpenedDate.AgNumberNegetiveAllow = False
        Me.TxtOpenedDate.AgNumberRightPlaces = 2
        Me.TxtOpenedDate.AgPickFromLastValue = False
        Me.TxtOpenedDate.AgRowFilter = ""
        Me.TxtOpenedDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOpenedDate.AgSelectedValue = Nothing
        Me.TxtOpenedDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOpenedDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtOpenedDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOpenedDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOpenedDate.Location = New System.Drawing.Point(148, 53)
        Me.TxtOpenedDate.MaxLength = 20
        Me.TxtOpenedDate.Name = "TxtOpenedDate"
        Me.TxtOpenedDate.Size = New System.Drawing.Size(143, 18)
        Me.TxtOpenedDate.TabIndex = 4
        '
        'LblOpenedDate
        '
        Me.LblOpenedDate.AutoSize = True
        Me.LblOpenedDate.BackColor = System.Drawing.Color.Transparent
        Me.LblOpenedDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOpenedDate.Location = New System.Drawing.Point(18, 54)
        Me.LblOpenedDate.Name = "LblOpenedDate"
        Me.LblOpenedDate.Size = New System.Drawing.Size(84, 16)
        Me.LblOpenedDate.TabIndex = 731
        Me.LblOpenedDate.Text = "Opened Date"
        '
        'LblDocumentNameReq
        '
        Me.LblDocumentNameReq.AutoSize = True
        Me.LblDocumentNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDocumentNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDocumentNameReq.Location = New System.Drawing.Point(133, 38)
        Me.LblDocumentNameReq.Name = "LblDocumentNameReq"
        Me.LblDocumentNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDocumentNameReq.TabIndex = 733
        Me.LblDocumentNameReq.Text = "Ä"
        '
        'LblDocumentNoReq
        '
        Me.LblDocumentNoReq.AutoSize = True
        Me.LblDocumentNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDocumentNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDocumentNoReq.Location = New System.Drawing.Point(390, 37)
        Me.LblDocumentNoReq.Name = "LblDocumentNoReq"
        Me.LblDocumentNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDocumentNoReq.TabIndex = 738
        Me.LblDocumentNoReq.Text = "Ä"
        '
        'TxtDocumentNo
        '
        Me.TxtDocumentNo.AgMandatory = True
        Me.TxtDocumentNo.AgMasterHelp = False
        Me.TxtDocumentNo.AgNumberLeftPlaces = 8
        Me.TxtDocumentNo.AgNumberNegetiveAllow = False
        Me.TxtDocumentNo.AgNumberRightPlaces = 2
        Me.TxtDocumentNo.AgPickFromLastValue = False
        Me.TxtDocumentNo.AgRowFilter = ""
        Me.TxtDocumentNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDocumentNo.AgSelectedValue = Nothing
        Me.TxtDocumentNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDocumentNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDocumentNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDocumentNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDocumentNo.Location = New System.Drawing.Point(406, 33)
        Me.TxtDocumentNo.MaxLength = 30
        Me.TxtDocumentNo.Name = "TxtDocumentNo"
        Me.TxtDocumentNo.Size = New System.Drawing.Size(122, 18)
        Me.TxtDocumentNo.TabIndex = 3
        '
        'LblDocumentNo
        '
        Me.LblDocumentNo.AutoSize = True
        Me.LblDocumentNo.BackColor = System.Drawing.Color.Transparent
        Me.LblDocumentNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocumentNo.Location = New System.Drawing.Point(297, 33)
        Me.LblDocumentNo.Name = "LblDocumentNo"
        Me.LblDocumentNo.Size = New System.Drawing.Size(87, 16)
        Me.LblDocumentNo.TabIndex = 737
        Me.LblDocumentNo.Text = "Document No"
        '
        'TxtCurrency
        '
        Me.TxtCurrency.AgMandatory = False
        Me.TxtCurrency.AgMasterHelp = False
        Me.TxtCurrency.AgNumberLeftPlaces = 8
        Me.TxtCurrency.AgNumberNegetiveAllow = False
        Me.TxtCurrency.AgNumberRightPlaces = 2
        Me.TxtCurrency.AgPickFromLastValue = False
        Me.TxtCurrency.AgRowFilter = ""
        Me.TxtCurrency.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCurrency.AgSelectedValue = Nothing
        Me.TxtCurrency.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCurrency.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCurrency.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCurrency.Location = New System.Drawing.Point(406, 53)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(122, 18)
        Me.TxtCurrency.TabIndex = 5
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(297, 53)
        Me.LblCurrency.Name = "LblCurrency"
        Me.LblCurrency.Size = New System.Drawing.Size(60, 16)
        Me.LblCurrency.TabIndex = 740
        Me.LblCurrency.Text = "Currency"
        '
        'TxtFileNo
        '
        Me.TxtFileNo.AgMandatory = False
        Me.TxtFileNo.AgMasterHelp = False
        Me.TxtFileNo.AgNumberLeftPlaces = 8
        Me.TxtFileNo.AgNumberNegetiveAllow = False
        Me.TxtFileNo.AgNumberRightPlaces = 2
        Me.TxtFileNo.AgPickFromLastValue = False
        Me.TxtFileNo.AgRowFilter = ""
        Me.TxtFileNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFileNo.AgSelectedValue = Nothing
        Me.TxtFileNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFileNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFileNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFileNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFileNo.Location = New System.Drawing.Point(148, 73)
        Me.TxtFileNo.MaxLength = 20
        Me.TxtFileNo.Name = "TxtFileNo"
        Me.TxtFileNo.Size = New System.Drawing.Size(143, 18)
        Me.TxtFileNo.TabIndex = 6
        '
        'LblFileNo
        '
        Me.LblFileNo.AutoSize = True
        Me.LblFileNo.BackColor = System.Drawing.Color.Transparent
        Me.LblFileNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFileNo.Location = New System.Drawing.Point(18, 74)
        Me.LblFileNo.Name = "LblFileNo"
        Me.LblFileNo.Size = New System.Drawing.Size(53, 16)
        Me.LblFileNo.TabIndex = 742
        Me.LblFileNo.Text = "File No."
        '
        'TxtFileDate
        '
        Me.TxtFileDate.AgMandatory = False
        Me.TxtFileDate.AgMasterHelp = False
        Me.TxtFileDate.AgNumberLeftPlaces = 8
        Me.TxtFileDate.AgNumberNegetiveAllow = False
        Me.TxtFileDate.AgNumberRightPlaces = 2
        Me.TxtFileDate.AgPickFromLastValue = False
        Me.TxtFileDate.AgRowFilter = ""
        Me.TxtFileDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFileDate.AgSelectedValue = Nothing
        Me.TxtFileDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFileDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtFileDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFileDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFileDate.Location = New System.Drawing.Point(406, 73)
        Me.TxtFileDate.MaxLength = 20
        Me.TxtFileDate.Name = "TxtFileDate"
        Me.TxtFileDate.Size = New System.Drawing.Size(122, 18)
        Me.TxtFileDate.TabIndex = 7
        '
        'LblFileDate
        '
        Me.LblFileDate.AutoSize = True
        Me.LblFileDate.BackColor = System.Drawing.Color.Transparent
        Me.LblFileDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFileDate.Location = New System.Drawing.Point(297, 73)
        Me.LblFileDate.Name = "LblFileDate"
        Me.LblFileDate.Size = New System.Drawing.Size(60, 16)
        Me.LblFileDate.TabIndex = 744
        Me.LblFileDate.Text = "File Date"
        '
        'TxtTollerance
        '
        Me.TxtTollerance.AgMandatory = False
        Me.TxtTollerance.AgMasterHelp = False
        Me.TxtTollerance.AgNumberLeftPlaces = 8
        Me.TxtTollerance.AgNumberNegetiveAllow = False
        Me.TxtTollerance.AgNumberRightPlaces = 0
        Me.TxtTollerance.AgPickFromLastValue = False
        Me.TxtTollerance.AgRowFilter = ""
        Me.TxtTollerance.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTollerance.AgSelectedValue = Nothing
        Me.TxtTollerance.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTollerance.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTollerance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTollerance.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTollerance.Location = New System.Drawing.Point(405, 93)
        Me.TxtTollerance.MaxLength = 20
        Me.TxtTollerance.Name = "TxtTollerance"
        Me.TxtTollerance.Size = New System.Drawing.Size(122, 18)
        Me.TxtTollerance.TabIndex = 9
        '
        'LblTollerance
        '
        Me.LblTollerance.AutoSize = True
        Me.LblTollerance.BackColor = System.Drawing.Color.Transparent
        Me.LblTollerance.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTollerance.Location = New System.Drawing.Point(297, 93)
        Me.LblTollerance.Name = "LblTollerance"
        Me.LblTollerance.Size = New System.Drawing.Size(67, 16)
        Me.LblTollerance.TabIndex = 748
        Me.LblTollerance.Text = "Tollerance"
        '
        'TxtPeriod
        '
        Me.TxtPeriod.AgMandatory = False
        Me.TxtPeriod.AgMasterHelp = False
        Me.TxtPeriod.AgNumberLeftPlaces = 8
        Me.TxtPeriod.AgNumberNegetiveAllow = False
        Me.TxtPeriod.AgNumberRightPlaces = 0
        Me.TxtPeriod.AgPickFromLastValue = False
        Me.TxtPeriod.AgRowFilter = ""
        Me.TxtPeriod.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPeriod.AgSelectedValue = Nothing
        Me.TxtPeriod.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPeriod.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtPeriod.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPeriod.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPeriod.Location = New System.Drawing.Point(148, 93)
        Me.TxtPeriod.MaxLength = 20
        Me.TxtPeriod.Name = "TxtPeriod"
        Me.TxtPeriod.Size = New System.Drawing.Size(143, 18)
        Me.TxtPeriod.TabIndex = 8
        '
        'LblPreiod
        '
        Me.LblPreiod.AutoSize = True
        Me.LblPreiod.BackColor = System.Drawing.Color.Transparent
        Me.LblPreiod.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPreiod.Location = New System.Drawing.Point(18, 94)
        Me.LblPreiod.Name = "LblPreiod"
        Me.LblPreiod.Size = New System.Drawing.Size(45, 16)
        Me.LblPreiod.TabIndex = 746
        Me.LblPreiod.Text = "Period"
        '
        'TxtImpPayMode
        '
        Me.TxtImpPayMode.AgMandatory = False
        Me.TxtImpPayMode.AgMasterHelp = False
        Me.TxtImpPayMode.AgNumberLeftPlaces = 8
        Me.TxtImpPayMode.AgNumberNegetiveAllow = False
        Me.TxtImpPayMode.AgNumberRightPlaces = 2
        Me.TxtImpPayMode.AgPickFromLastValue = False
        Me.TxtImpPayMode.AgRowFilter = ""
        Me.TxtImpPayMode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtImpPayMode.AgSelectedValue = Nothing
        Me.TxtImpPayMode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtImpPayMode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtImpPayMode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtImpPayMode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtImpPayMode.Location = New System.Drawing.Point(148, 113)
        Me.TxtImpPayMode.MaxLength = 20
        Me.TxtImpPayMode.Name = "TxtImpPayMode"
        Me.TxtImpPayMode.Size = New System.Drawing.Size(143, 18)
        Me.TxtImpPayMode.TabIndex = 10
        '
        'LblImpPayMode
        '
        Me.LblImpPayMode.AutoSize = True
        Me.LblImpPayMode.BackColor = System.Drawing.Color.Transparent
        Me.LblImpPayMode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblImpPayMode.Location = New System.Drawing.Point(18, 114)
        Me.LblImpPayMode.Name = "LblImpPayMode"
        Me.LblImpPayMode.Size = New System.Drawing.Size(96, 16)
        Me.LblImpPayMode.TabIndex = 750
        Me.LblImpPayMode.Text = "Imp. Pay Mode"
        '
        'TxtExpPayMode
        '
        Me.TxtExpPayMode.AgMandatory = False
        Me.TxtExpPayMode.AgMasterHelp = False
        Me.TxtExpPayMode.AgNumberLeftPlaces = 8
        Me.TxtExpPayMode.AgNumberNegetiveAllow = False
        Me.TxtExpPayMode.AgNumberRightPlaces = 2
        Me.TxtExpPayMode.AgPickFromLastValue = False
        Me.TxtExpPayMode.AgRowFilter = ""
        Me.TxtExpPayMode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExpPayMode.AgSelectedValue = Nothing
        Me.TxtExpPayMode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExpPayMode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExpPayMode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExpPayMode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExpPayMode.Location = New System.Drawing.Point(405, 113)
        Me.TxtExpPayMode.MaxLength = 20
        Me.TxtExpPayMode.Name = "TxtExpPayMode"
        Me.TxtExpPayMode.Size = New System.Drawing.Size(122, 18)
        Me.TxtExpPayMode.TabIndex = 11
        '
        'LblExpPayMode
        '
        Me.LblExpPayMode.AutoSize = True
        Me.LblExpPayMode.BackColor = System.Drawing.Color.Transparent
        Me.LblExpPayMode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblExpPayMode.Location = New System.Drawing.Point(297, 113)
        Me.LblExpPayMode.Name = "LblExpPayMode"
        Me.LblExpPayMode.Size = New System.Drawing.Size(98, 16)
        Me.LblExpPayMode.TabIndex = 752
        Me.LblExpPayMode.Text = "Exp. Pay Mode"
        '
        'LblPartyNameReq
        '
        Me.LblPartyNameReq.AutoSize = True
        Me.LblPartyNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblPartyNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblPartyNameReq.Location = New System.Drawing.Point(133, 139)
        Me.LblPartyNameReq.Name = "LblPartyNameReq"
        Me.LblPartyNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblPartyNameReq.TabIndex = 755
        Me.LblPartyNameReq.Text = "Ä"
        '
        'TxtPartyName
        '
        Me.TxtPartyName.AgMandatory = True
        Me.TxtPartyName.AgMasterHelp = False
        Me.TxtPartyName.AgNumberLeftPlaces = 8
        Me.TxtPartyName.AgNumberNegetiveAllow = False
        Me.TxtPartyName.AgNumberRightPlaces = 2
        Me.TxtPartyName.AgPickFromLastValue = False
        Me.TxtPartyName.AgRowFilter = ""
        Me.TxtPartyName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyName.AgSelectedValue = Nothing
        Me.TxtPartyName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyName.Location = New System.Drawing.Point(148, 133)
        Me.TxtPartyName.MaxLength = 50
        Me.TxtPartyName.Name = "TxtPartyName"
        Me.TxtPartyName.Size = New System.Drawing.Size(379, 18)
        Me.TxtPartyName.TabIndex = 12
        '
        'LblPartyName
        '
        Me.LblPartyName.AutoSize = True
        Me.LblPartyName.BackColor = System.Drawing.Color.Transparent
        Me.LblPartyName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyName.Location = New System.Drawing.Point(18, 134)
        Me.LblPartyName.Name = "LblPartyName"
        Me.LblPartyName.Size = New System.Drawing.Size(77, 16)
        Me.LblPartyName.TabIndex = 754
        Me.LblPartyName.Text = "Party Name"
        '
        'TxtBankName
        '
        Me.TxtBankName.AgMandatory = False
        Me.TxtBankName.AgMasterHelp = False
        Me.TxtBankName.AgNumberLeftPlaces = 8
        Me.TxtBankName.AgNumberNegetiveAllow = False
        Me.TxtBankName.AgNumberRightPlaces = 2
        Me.TxtBankName.AgPickFromLastValue = False
        Me.TxtBankName.AgRowFilter = ""
        Me.TxtBankName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBankName.AgSelectedValue = Nothing
        Me.TxtBankName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBankName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBankName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankName.Location = New System.Drawing.Point(148, 153)
        Me.TxtBankName.MaxLength = 50
        Me.TxtBankName.Name = "TxtBankName"
        Me.TxtBankName.Size = New System.Drawing.Size(379, 18)
        Me.TxtBankName.TabIndex = 13
        '
        'LblBankName
        '
        Me.LblBankName.AutoSize = True
        Me.LblBankName.BackColor = System.Drawing.Color.Transparent
        Me.LblBankName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBankName.Location = New System.Drawing.Point(18, 154)
        Me.LblBankName.Name = "LblBankName"
        Me.LblBankName.Size = New System.Drawing.Size(76, 16)
        Me.LblBankName.TabIndex = 757
        Me.LblBankName.Text = "Bank Name"
        '
        'TxtRemark
        '
        Me.TxtRemark.AgMandatory = False
        Me.TxtRemark.AgMasterHelp = False
        Me.TxtRemark.AgNumberLeftPlaces = 8
        Me.TxtRemark.AgNumberNegetiveAllow = False
        Me.TxtRemark.AgNumberRightPlaces = 2
        Me.TxtRemark.AgPickFromLastValue = False
        Me.TxtRemark.AgRowFilter = ""
        Me.TxtRemark.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRemark.AgSelectedValue = Nothing
        Me.TxtRemark.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemark.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemark.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemark.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemark.Location = New System.Drawing.Point(148, 173)
        Me.TxtRemark.MaxLength = 255
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.Size = New System.Drawing.Size(379, 18)
        Me.TxtRemark.TabIndex = 14
        '
        'LblRemark
        '
        Me.LblRemark.AutoSize = True
        Me.LblRemark.BackColor = System.Drawing.Color.Transparent
        Me.LblRemark.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemark.Location = New System.Drawing.Point(18, 174)
        Me.LblRemark.Name = "LblRemark"
        Me.LblRemark.Size = New System.Drawing.Size(53, 16)
        Me.LblRemark.TabIndex = 759
        Me.LblRemark.Text = "Remark"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(562, 37)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(411, 155)
        Me.Pnl1.TabIndex = 17
        '
        'TxtImportExpiryDate
        '
        Me.TxtImportExpiryDate.AgMandatory = False
        Me.TxtImportExpiryDate.AgMasterHelp = False
        Me.TxtImportExpiryDate.AgNumberLeftPlaces = 8
        Me.TxtImportExpiryDate.AgNumberNegetiveAllow = False
        Me.TxtImportExpiryDate.AgNumberRightPlaces = 2
        Me.TxtImportExpiryDate.AgPickFromLastValue = False
        Me.TxtImportExpiryDate.AgRowFilter = ""
        Me.TxtImportExpiryDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtImportExpiryDate.AgSelectedValue = Nothing
        Me.TxtImportExpiryDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtImportExpiryDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtImportExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtImportExpiryDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtImportExpiryDate.Location = New System.Drawing.Point(672, 11)
        Me.TxtImportExpiryDate.MaxLength = 20
        Me.TxtImportExpiryDate.Name = "TxtImportExpiryDate"
        Me.TxtImportExpiryDate.Size = New System.Drawing.Size(88, 18)
        Me.TxtImportExpiryDate.TabIndex = 15
        '
        'LblImportExpiryDate
        '
        Me.LblImportExpiryDate.AutoSize = True
        Me.LblImportExpiryDate.BackColor = System.Drawing.Color.Transparent
        Me.LblImportExpiryDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblImportExpiryDate.Location = New System.Drawing.Point(559, 12)
        Me.LblImportExpiryDate.Name = "LblImportExpiryDate"
        Me.LblImportExpiryDate.Size = New System.Drawing.Size(103, 16)
        Me.LblImportExpiryDate.TabIndex = 762
        Me.LblImportExpiryDate.Text = "Exp. Date (Imp.)"
        '
        'TxtExportExpiryDate
        '
        Me.TxtExportExpiryDate.AgMandatory = False
        Me.TxtExportExpiryDate.AgMasterHelp = False
        Me.TxtExportExpiryDate.AgNumberLeftPlaces = 8
        Me.TxtExportExpiryDate.AgNumberNegetiveAllow = False
        Me.TxtExportExpiryDate.AgNumberRightPlaces = 2
        Me.TxtExportExpiryDate.AgPickFromLastValue = False
        Me.TxtExportExpiryDate.AgRowFilter = ""
        Me.TxtExportExpiryDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExportExpiryDate.AgSelectedValue = Nothing
        Me.TxtExportExpiryDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExportExpiryDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtExportExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExportExpiryDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExportExpiryDate.Location = New System.Drawing.Point(872, 11)
        Me.TxtExportExpiryDate.MaxLength = 20
        Me.TxtExportExpiryDate.Name = "TxtExportExpiryDate"
        Me.TxtExportExpiryDate.Size = New System.Drawing.Size(101, 18)
        Me.TxtExportExpiryDate.TabIndex = 16
        '
        'LblExportExpiryDate
        '
        Me.LblExportExpiryDate.AutoSize = True
        Me.LblExportExpiryDate.BackColor = System.Drawing.Color.Transparent
        Me.LblExportExpiryDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblExportExpiryDate.Location = New System.Drawing.Point(763, 12)
        Me.LblExportExpiryDate.Name = "LblExportExpiryDate"
        Me.LblExportExpiryDate.Size = New System.Drawing.Size(105, 16)
        Me.LblExportExpiryDate.TabIndex = 764
        Me.LblExportExpiryDate.Text = "Exp. Date (Exp.)"
        '
        'FrmDocumentEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(992, 516)
        Me.Controls.Add(Me.Pnl2)
        Me.Name = "FrmDocumentEntry"
        Me.Text = "Template Purchase Indent"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
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
    Protected WithEvents TxtDocumentName As AgControls.AgTextBox
    Protected WithEvents LblDocumentName As System.Windows.Forms.Label
    Protected WithEvents Pnl2 As System.Windows.Forms.Panel
    Protected WithEvents LblOpenedDateReq As System.Windows.Forms.Label
    Protected WithEvents TxtOpenedDate As AgControls.AgTextBox
    Protected WithEvents LblOpenedDate As System.Windows.Forms.Label
    Protected WithEvents LblDocumentNameReq As System.Windows.Forms.Label
    Protected WithEvents LblDocumentNoReq As System.Windows.Forms.Label
    Protected WithEvents TxtDocumentNo As AgControls.AgTextBox
    Protected WithEvents LblDocumentNo As System.Windows.Forms.Label
    Protected WithEvents TxtTollerance As AgControls.AgTextBox
    Protected WithEvents LblTollerance As System.Windows.Forms.Label
    Protected WithEvents TxtPeriod As AgControls.AgTextBox
    Protected WithEvents LblPreiod As System.Windows.Forms.Label
    Protected WithEvents TxtFileDate As AgControls.AgTextBox
    Protected WithEvents LblFileDate As System.Windows.Forms.Label
    Protected WithEvents TxtFileNo As AgControls.AgTextBox
    Protected WithEvents LblFileNo As System.Windows.Forms.Label
    Protected WithEvents TxtCurrency As AgControls.AgTextBox
    Protected WithEvents LblCurrency As System.Windows.Forms.Label
    Protected WithEvents TxtExpPayMode As AgControls.AgTextBox
    Protected WithEvents LblExpPayMode As System.Windows.Forms.Label
    Protected WithEvents TxtImpPayMode As AgControls.AgTextBox
    Protected WithEvents LblImpPayMode As System.Windows.Forms.Label
    Protected WithEvents TxtBankName As AgControls.AgTextBox
    Protected WithEvents LblBankName As System.Windows.Forms.Label
    Protected WithEvents LblPartyNameReq As System.Windows.Forms.Label
    Protected WithEvents TxtPartyName As AgControls.AgTextBox
    Protected WithEvents LblPartyName As System.Windows.Forms.Label
    Protected WithEvents TxtExportExpiryDate As AgControls.AgTextBox
    Protected WithEvents LblExportExpiryDate As System.Windows.Forms.Label
    Protected WithEvents TxtImportExpiryDate As AgControls.AgTextBox
    Protected WithEvents LblImportExpiryDate As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemark As AgControls.AgTextBox
    Protected WithEvents LblRemark As System.Windows.Forms.Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "IE_DocEntry"
        LogTableName = "IE_DocEntry_Log"
        MainLineTableCsv = "IE_DocAmend,IE_DocItem"
        LogLineTableCsv = "IE_DocAmend_Log,IE_DocItem_Log"
        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl2)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                       " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " &
                " From IE_DocEntry H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where IfNull(H.IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select H.UID As SearchCode " &
               " From IE_DocEntry_Log H " &
               " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
               " Where H.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By H.EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = "SELECT H.DocID, Vt.Description AS [Entry Type], H.V_Date AS [Entry Date], " &
                            " H.V_No AS [Entry No] " &
                            " FROM IE_DocEntry H " &
                            " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.UID as SearchCode, H.DocId, Vt.Description AS [Entry Type], " &
                            " H.V_Date AS [Entry Date], H.V_No AS [Entry No] " &
                            " FROM IE_DocEntry_Log H " &
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                            " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgDateColumn(Dgl1, Col1AmendDate, 90, Col1AmendDate, True)
            .AddAgDateColumn(Dgl1, Col1ExpiryDateImport, 130, Col1ExpiryDateImport, True)
            .AddAgDateColumn(Dgl1, Col1ExpiryDateExport, 130, Col1ExpiryDateExport, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 30

        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2Type, 150, 20, Col2Type, True, False, False)
            .AddAgTextColumn(Dgl2, Col2ItemImportExportGroup, 200, 20, Col2ItemImportExportGroup, True, False, False)
            .AddAgNumberColumn(Dgl2, Col2Qty, 150, 8, 2, False, Col2Qty, True, False)
            .AddAgTextColumn(Dgl2, Col2Unit, 100, 20, Col2Unit, True, False, False)
            .AddAgNumberColumn(Dgl2, Col2AmountFC, 120, 8, 2, False, Col2AmountFC, True, False)
            .AddAgNumberColumn(Dgl2, Col2AmountINR, 120, 8, 2, False, Col2AmountINR, True, False)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 30
        'Ini_List()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        mQry = "UPDATE IE_DocEntry_Log " &
                " SET " &
                " Document = " & AgL.Chk_Text(TxtDocumentName.AgSelectedValue) & ", " &
                " DocumentNo = " & AgL.Chk_Text(TxtDocumentNo.Text) & ", " &
                " OpenedDate = " & AgL.Chk_Text(TxtOpenedDate.Text) & ", " &
                " Currency = " & AgL.Chk_Text(TxtCurrency.AgSelectedValue) & ", " &
                " FileNo = " & AgL.Chk_Text(TxtFileNo.Text) & ", " &
                " FileDate = " & AgL.Chk_Text(TxtFileDate.Text) & ", " &
                " Period = " & Val(TxtPeriod.Text) & ", " &
                " Tollerance = " & Val(TxtTollerance.Text) & ", " &
                " ImportPayMode = " & AgL.Chk_Text(TxtImpPayMode.Text) & ", " &
                " ExportPayMode = " & AgL.Chk_Text(TxtExpPayMode.Text) & ", " &
                " Party = " & AgL.Chk_Text(TxtPartyName.AgSelectedValue) & ", " &
                " Bank = " & AgL.Chk_Text(TxtBankName.AgSelectedValue) & ", " &
                " Remark = " & AgL.Chk_Text(TxtRemark.Text) & ", " &
                " ExpiryDateImport = " & AgL.Chk_Text(TxtImportExpiryDate.Text) & ", " &
                " ExpiryDateExport = " & AgL.Chk_Text(TxtExportExpiryDate.Text) & " " &
                " Where UID = '" & mSearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From IE_DocAmend_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From IE_DocItem_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1AmendDate, I).Value <> "" Then
                    mSr += 1
                    mQry = "INSERT INTO IE_DocAmend_Log(UID, DocId, Sr, AmendDate, ExpiryDateImport, ExpiryDateExport) " &
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & AgL.Chk_Text(mInternalCode) & ", " & mSr & ", " &
                            " " & AgL.Chk_Text(.Item(Col1AmendDate, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ExpiryDateImport, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ExpiryDateExport, I).Value) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

        With Dgl2
            For I = 0 To .RowCount - 1
                If .Item(Col2Type, I).Value <> "" Then
                    mSr += 1
                    mQry = "INSERT INTO IE_DocItem_Log(UID, DocId, Sr, Type, ItemImportExportGroup, Qty, Unit, " &
                            " AmountFC, AmountINR) " &
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " &
                            " " & mSr & ", " & AgL.Chk_Text(.Item(Col2Type, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col2ItemImportExportGroup, I).Value) & ", " &
                            " " & Val(.Item(Col2Qty, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col2Unit, I).Value) & ", " &
                            " " & Val(.Item(Col2AmountFC, I).Value) & ", " &
                            " " & Val(.Item(Col2AmountINR, I).Value) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select P.* " &
                " From IE_DocEntry P " &
                " Where P.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select P.* " &
                " From IE_DocEntry_Log P " &
                " Where P.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtDocumentName.AgSelectedValue = AgL.XNull(.Rows(0)("Document"))
                TxtDocumentNo.Text = AgL.XNull(.Rows(0)("DocumentNo"))
                TxtOpenedDate.Text = AgL.XNull(.Rows(0)("OpenedDate"))
                TxtCurrency.Text = AgL.XNull(.Rows(0)("Currency"))
                TxtFileNo.Text = AgL.XNull(.Rows(0)("FileNo"))
                TxtFileDate.Text = AgL.XNull(.Rows(0)("FileDate"))
                TxtPeriod.Text = AgL.VNull(.Rows(0)("Period"))
                TxtTollerance.Text = AgL.VNull(.Rows(0)("Tollerance"))
                TxtImpPayMode.Text = AgL.XNull(.Rows(0)("ImportPayMode"))
                TxtExpPayMode.Text = AgL.XNull(.Rows(0)("ExportPayMode"))
                TxtPartyName.AgSelectedValue = AgL.XNull(.Rows(0)("Party"))
                TxtBankName.AgSelectedValue = AgL.XNull(.Rows(0)("Bank"))
                TxtRemark.Text = AgL.XNull(.Rows(0)("Remark"))
                TxtImportExpiryDate.Text = AgL.XNull(.Rows(0)("ExpiryDateImport"))
                TxtExportExpiryDate.Text = AgL.XNull(.Rows(0)("ExpiryDateExport"))
                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from IE_DocAmend where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from IE_DocAmend_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1AmendDate, I).Value = AgL.XNull(.Rows(I)("AmendDate"))
                            Dgl1.Item(Col1ExpiryDateImport, I).Value = AgL.XNull(.Rows(I)("ExpiryDateImport"))
                            Dgl1.Item(Col1ExpiryDateExport, I).Value = AgL.XNull(.Rows(I)("ExpiryDateExport"))
                        Next I
                    End If
                End With


                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from IE_DocItem where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from IE_DocItem_Log where UID = '" & SearchCode & "' Order By Sr"
                End If
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl2.RowCount = 1
                    Dgl2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl2.Rows.Add()
                            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                            Dgl2.Item(Col2Type, I).Value = AgL.XNull(.Rows(I)("Type"))
                            Dgl2.Item(Col2ItemImportExportGroup, I).Value = AgL.XNull(.Rows(I)("ItemImportExportGroup"))
                            Dgl2.Item(Col2Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl2.Item(Col2Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl2.Item(Col2AmountFC, I).Value = AgL.VNull(.Rows(I)("AmountFC"))
                            Dgl2.Item(Col2AmountINR, I).Value = AgL.VNull(.Rows(I)("AmountINR"))
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
        mQry = " SELECT D.Code AS Code, D.Description AS Document FROM IE_Doc D "
        TxtDocumentName.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT C.Code AS Code , C.Code AS Currency FROM Currency C"
        TxtCurrency.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select Sg.SubCode As Code, Sg.DispName As PartyName From SubGroup Sg "
        TxtPartyName.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select Sg.SubCode As Code, Sg.DispName As PartyName From SubGroup Sg " &
                " Where Sg.Nature = 'Bank' "
        TxtBankName.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select 'CIF' As Code, 'CIF' As Description " &
                " UNION ALL " &
                " Select 'FOB' As Code, 'FOB' As Description " &
                " UNION ALL " &
                " Select 'FOR' As Code, 'FOR' As Description "
        TxtImpPayMode.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        TxtExpPayMode.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = TxtImpPayMode.AgHelpDataSet

        mQry = " Select 'Export' As Code, 'Export' As Type Union All Select 'Import' As Code, 'Import' As Type "
        Dgl2.AgHelpDataSet(Col2Type) = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select U.Code As Code, U.Code As Unit From Unit U"
        Dgl2.AgHelpDataSet(Col2Unit) = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select Distinct ItemImportExportGroup As Code, ItemImportExportGroup As Decription " &
                " From Item I " &
                " Where ItemImportExportGroup Is Not Null "
        Dgl2.AgHelpDataSet(Col2ItemImportExportGroup) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl2.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtV_Date, LblV_Date.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtDocumentName, LblDocumentName.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtDocumentNo, LblDocumentNo.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtOpenedDate, LblOpenedDate.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtPartyName, LblPartyName.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl2, Dgl2.Columns(Col2Type).Index) Then passed = False : Exit Sub

        With Dgl2
            For I = 0 To .Rows.Count - 1
                If .Item(Col2Type, I).Value <> "" Then
                    If Val(.Item(Col2Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl2.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col2Qty, I) : Dgl2.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
    End Sub
End Class
