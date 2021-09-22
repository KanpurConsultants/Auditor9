Public Class TempFormReceive
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1Form As String = "Form"
    Protected Const Col1FormNo As String = "Form No"
    Protected Const Col1ReceiveDate As String = "Receive Date"
    Protected Const Col1ReceiveFrom As String = "Receive From"
    Protected Const Col1IsInStock As String = "IsInStock"
    Protected Const Col1Status As String = "Status"
    Protected Const Col1FormNoSerial As String = "FormNoSerial"
    Protected Const Col1Code As String = "Code"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtForm = New AgControls.AgTextBox
        Me.LblForm = New System.Windows.Forms.Label
        Me.LblFormReq = New System.Windows.Forms.Label
        Me.LblFormPrefixReq = New System.Windows.Forms.Label
        Me.TxtFormPrefix = New AgControls.AgTextBox
        Me.LblFormPrefix = New System.Windows.Forms.Label
        Me.TxtFormNoUpto = New AgControls.AgTextBox
        Me.LblFormNoUpto = New System.Windows.Forms.Label
        Me.LblFileNo = New System.Windows.Forms.Label
        Me.TxtParty = New AgControls.AgTextBox
        Me.LblPartyName = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.LblFormNoUptoReq = New System.Windows.Forms.Label
        Me.LblFormNoFromReq = New System.Windows.Forms.Label
        Me.TxtFormNoFrom = New AgControls.AgTextBox
        Me.LblFormNoFrom = New System.Windows.Forms.Label
        Me.RbtFromParty = New System.Windows.Forms.RadioButton
        Me.RbtFromDepartment = New System.Windows.Forms.RadioButton
        Me.BtnFill = New System.Windows.Forms.Button
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
        Me.GroupBox2.Location = New System.Drawing.Point(681, 475)
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
        Me.GroupBox1.Size = New System.Drawing.Size(879, 4)
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
        Me.LblV_No.Location = New System.Drawing.Point(287, 14)
        Me.LblV_No.Tag = ""
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(396, 14)
        Me.TxtV_No.Size = New System.Drawing.Size(122, 18)
        Me.TxtV_No.TabIndex = 1
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(122, 20)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(8, 15)
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
        Me.TxtV_Date.Location = New System.Drawing.Point(138, 14)
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
        Me.TxtV_Type.Location = New System.Drawing.Point(803, 20)
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
        Me.TabControl1.Size = New System.Drawing.Size(864, 135)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.BtnFill)
        Me.TP1.Controls.Add(Me.RbtFromDepartment)
        Me.TP1.Controls.Add(Me.RbtFromParty)
        Me.TP1.Controls.Add(Me.LblFormNoFromReq)
        Me.TP1.Controls.Add(Me.TxtFormNoFrom)
        Me.TP1.Controls.Add(Me.LblFormNoFrom)
        Me.TP1.Controls.Add(Me.LblFormNoUptoReq)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.LblPartyName)
        Me.TP1.Controls.Add(Me.LblFileNo)
        Me.TP1.Controls.Add(Me.TxtFormNoUpto)
        Me.TP1.Controls.Add(Me.LblFormNoUpto)
        Me.TP1.Controls.Add(Me.LblFormPrefixReq)
        Me.TP1.Controls.Add(Me.TxtFormPrefix)
        Me.TP1.Controls.Add(Me.LblFormPrefix)
        Me.TP1.Controls.Add(Me.LblFormReq)
        Me.TP1.Controls.Add(Me.TxtForm)
        Me.TP1.Controls.Add(Me.LblForm)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(856, 109)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblForm, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtForm, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFormReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFormPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFormPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFormPrefixReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFormNoUpto, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFormNoUpto, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFileNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFormNoUptoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFormNoFrom, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFormNoFrom, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFormNoFromReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.RbtFromParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.RbtFromDepartment, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFill, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(861, 41)
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
        'TxtForm
        '
        Me.TxtForm.AgMandatory = True
        Me.TxtForm.AgMasterHelp = False
        Me.TxtForm.AgNumberLeftPlaces = 8
        Me.TxtForm.AgNumberNegetiveAllow = False
        Me.TxtForm.AgNumberRightPlaces = 2
        Me.TxtForm.AgPickFromLastValue = False
        Me.TxtForm.AgRowFilter = ""
        Me.TxtForm.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtForm.AgSelectedValue = Nothing
        Me.TxtForm.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtForm.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtForm.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtForm.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtForm.Location = New System.Drawing.Point(138, 34)
        Me.TxtForm.MaxLength = 50
        Me.TxtForm.Name = "TxtForm"
        Me.TxtForm.Size = New System.Drawing.Size(143, 18)
        Me.TxtForm.TabIndex = 2
        '
        'LblForm
        '
        Me.LblForm.AutoSize = True
        Me.LblForm.BackColor = System.Drawing.Color.Transparent
        Me.LblForm.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblForm.Location = New System.Drawing.Point(8, 35)
        Me.LblForm.Name = "LblForm"
        Me.LblForm.Size = New System.Drawing.Size(38, 16)
        Me.LblForm.TabIndex = 706
        Me.LblForm.Text = "Form"
        '
        'LblFormReq
        '
        Me.LblFormReq.AutoSize = True
        Me.LblFormReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFormReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFormReq.Location = New System.Drawing.Point(122, 39)
        Me.LblFormReq.Name = "LblFormReq"
        Me.LblFormReq.Size = New System.Drawing.Size(10, 7)
        Me.LblFormReq.TabIndex = 733
        Me.LblFormReq.Text = "Ä"
        '
        'LblFormPrefixReq
        '
        Me.LblFormPrefixReq.AutoSize = True
        Me.LblFormPrefixReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFormPrefixReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFormPrefixReq.Location = New System.Drawing.Point(380, 38)
        Me.LblFormPrefixReq.Name = "LblFormPrefixReq"
        Me.LblFormPrefixReq.Size = New System.Drawing.Size(10, 7)
        Me.LblFormPrefixReq.TabIndex = 738
        Me.LblFormPrefixReq.Text = "Ä"
        '
        'TxtFormPrefix
        '
        Me.TxtFormPrefix.AgMandatory = True
        Me.TxtFormPrefix.AgMasterHelp = False
        Me.TxtFormPrefix.AgNumberLeftPlaces = 8
        Me.TxtFormPrefix.AgNumberNegetiveAllow = False
        Me.TxtFormPrefix.AgNumberRightPlaces = 2
        Me.TxtFormPrefix.AgPickFromLastValue = False
        Me.TxtFormPrefix.AgRowFilter = ""
        Me.TxtFormPrefix.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFormPrefix.AgSelectedValue = Nothing
        Me.TxtFormPrefix.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFormPrefix.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFormPrefix.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFormPrefix.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormPrefix.Location = New System.Drawing.Point(396, 34)
        Me.TxtFormPrefix.MaxLength = 30
        Me.TxtFormPrefix.Name = "TxtFormPrefix"
        Me.TxtFormPrefix.Size = New System.Drawing.Size(122, 18)
        Me.TxtFormPrefix.TabIndex = 3
        '
        'LblFormPrefix
        '
        Me.LblFormPrefix.AutoSize = True
        Me.LblFormPrefix.BackColor = System.Drawing.Color.Transparent
        Me.LblFormPrefix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFormPrefix.Location = New System.Drawing.Point(287, 34)
        Me.LblFormPrefix.Name = "LblFormPrefix"
        Me.LblFormPrefix.Size = New System.Drawing.Size(75, 16)
        Me.LblFormPrefix.TabIndex = 737
        Me.LblFormPrefix.Text = "Form Prefix"
        '
        'TxtFormNoUpto
        '
        Me.TxtFormNoUpto.AgMandatory = True
        Me.TxtFormNoUpto.AgMasterHelp = False
        Me.TxtFormNoUpto.AgNumberLeftPlaces = 8
        Me.TxtFormNoUpto.AgNumberNegetiveAllow = False
        Me.TxtFormNoUpto.AgNumberRightPlaces = 0
        Me.TxtFormNoUpto.AgPickFromLastValue = False
        Me.TxtFormNoUpto.AgRowFilter = ""
        Me.TxtFormNoUpto.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFormNoUpto.AgSelectedValue = Nothing
        Me.TxtFormNoUpto.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFormNoUpto.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtFormNoUpto.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFormNoUpto.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormNoUpto.Location = New System.Drawing.Point(396, 54)
        Me.TxtFormNoUpto.MaxLength = 20
        Me.TxtFormNoUpto.Name = "TxtFormNoUpto"
        Me.TxtFormNoUpto.Size = New System.Drawing.Size(122, 18)
        Me.TxtFormNoUpto.TabIndex = 5
        '
        'LblFormNoUpto
        '
        Me.LblFormNoUpto.AutoSize = True
        Me.LblFormNoUpto.BackColor = System.Drawing.Color.Transparent
        Me.LblFormNoUpto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFormNoUpto.Location = New System.Drawing.Point(287, 54)
        Me.LblFormNoUpto.Name = "LblFormNoUpto"
        Me.LblFormNoUpto.Size = New System.Drawing.Size(93, 16)
        Me.LblFormNoUpto.TabIndex = 740
        Me.LblFormNoUpto.Text = "Form No. Upto"
        '
        'LblFileNo
        '
        Me.LblFileNo.AutoSize = True
        Me.LblFileNo.BackColor = System.Drawing.Color.Transparent
        Me.LblFileNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFileNo.Location = New System.Drawing.Point(8, 74)
        Me.LblFileNo.Name = "LblFileNo"
        Me.LblFileNo.Size = New System.Drawing.Size(53, 16)
        Me.LblFileNo.TabIndex = 742
        Me.LblFileNo.Text = "File No."
        '
        'TxtParty
        '
        Me.TxtParty.AgMandatory = False
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
        Me.TxtParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParty.Location = New System.Drawing.Point(138, 74)
        Me.TxtParty.MaxLength = 50
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(380, 18)
        Me.TxtParty.TabIndex = 6
        '
        'LblPartyName
        '
        Me.LblPartyName.AutoSize = True
        Me.LblPartyName.BackColor = System.Drawing.Color.Transparent
        Me.LblPartyName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyName.Location = New System.Drawing.Point(8, 75)
        Me.LblPartyName.Name = "LblPartyName"
        Me.LblPartyName.Size = New System.Drawing.Size(77, 16)
        Me.LblPartyName.TabIndex = 754
        Me.LblPartyName.Text = "Party Name"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(11, 160)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(839, 305)
        Me.Pnl1.TabIndex = 1
        '
        'LblFormNoUptoReq
        '
        Me.LblFormNoUptoReq.AutoSize = True
        Me.LblFormNoUptoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFormNoUptoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFormNoUptoReq.Location = New System.Drawing.Point(380, 60)
        Me.LblFormNoUptoReq.Name = "LblFormNoUptoReq"
        Me.LblFormNoUptoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblFormNoUptoReq.TabIndex = 765
        Me.LblFormNoUptoReq.Text = "Ä"
        '
        'LblFormNoFromReq
        '
        Me.LblFormNoFromReq.AutoSize = True
        Me.LblFormNoFromReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFormNoFromReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFormNoFromReq.Location = New System.Drawing.Point(122, 58)
        Me.LblFormNoFromReq.Name = "LblFormNoFromReq"
        Me.LblFormNoFromReq.Size = New System.Drawing.Size(10, 7)
        Me.LblFormNoFromReq.TabIndex = 768
        Me.LblFormNoFromReq.Text = "Ä"
        '
        'TxtFormNoFrom
        '
        Me.TxtFormNoFrom.AgMandatory = True
        Me.TxtFormNoFrom.AgMasterHelp = False
        Me.TxtFormNoFrom.AgNumberLeftPlaces = 8
        Me.TxtFormNoFrom.AgNumberNegetiveAllow = False
        Me.TxtFormNoFrom.AgNumberRightPlaces = 0
        Me.TxtFormNoFrom.AgPickFromLastValue = False
        Me.TxtFormNoFrom.AgRowFilter = ""
        Me.TxtFormNoFrom.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFormNoFrom.AgSelectedValue = Nothing
        Me.TxtFormNoFrom.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFormNoFrom.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtFormNoFrom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFormNoFrom.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormNoFrom.Location = New System.Drawing.Point(138, 54)
        Me.TxtFormNoFrom.MaxLength = 30
        Me.TxtFormNoFrom.Name = "TxtFormNoFrom"
        Me.TxtFormNoFrom.Size = New System.Drawing.Size(143, 18)
        Me.TxtFormNoFrom.TabIndex = 4
        '
        'LblFormNoFrom
        '
        Me.LblFormNoFrom.AutoSize = True
        Me.LblFormNoFrom.BackColor = System.Drawing.Color.Transparent
        Me.LblFormNoFrom.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFormNoFrom.Location = New System.Drawing.Point(8, 54)
        Me.LblFormNoFrom.Name = "LblFormNoFrom"
        Me.LblFormNoFrom.Size = New System.Drawing.Size(96, 16)
        Me.LblFormNoFrom.TabIndex = 767
        Me.LblFormNoFrom.Text = "Form No. From"
        '
        'RbtFromParty
        '
        Me.RbtFromParty.AutoSize = True
        Me.RbtFromParty.Location = New System.Drawing.Point(542, 13)
        Me.RbtFromParty.Name = "RbtFromParty"
        Me.RbtFromParty.Size = New System.Drawing.Size(88, 17)
        Me.RbtFromParty.TabIndex = 769
        Me.RbtFromParty.TabStop = True
        Me.RbtFromParty.Text = "From Party"
        Me.RbtFromParty.UseVisualStyleBackColor = True
        '
        'RbtFromDepartment
        '
        Me.RbtFromDepartment.AutoSize = True
        Me.RbtFromDepartment.Location = New System.Drawing.Point(542, 34)
        Me.RbtFromDepartment.Name = "RbtFromDepartment"
        Me.RbtFromDepartment.Size = New System.Drawing.Size(126, 17)
        Me.RbtFromDepartment.TabIndex = 770
        Me.RbtFromDepartment.TabStop = True
        Me.RbtFromDepartment.Text = "From Department"
        Me.RbtFromDepartment.UseVisualStyleBackColor = True
        '
        'BtnFill
        '
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Location = New System.Drawing.Point(794, 84)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(56, 22)
        Me.BtnFill.TabIndex = 7
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = True
        '
        'FrmFormReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(861, 516)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmFormReceive"
        Me.Text = "From Receive Entry"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
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
    Protected WithEvents TxtForm As AgControls.AgTextBox
    Protected WithEvents LblForm As System.Windows.Forms.Label
    Protected WithEvents LblFormReq As System.Windows.Forms.Label
    Protected WithEvents LblFormPrefixReq As System.Windows.Forms.Label
    Protected WithEvents TxtFormPrefix As AgControls.AgTextBox
    Protected WithEvents LblFormPrefix As System.Windows.Forms.Label
    Protected WithEvents LblFileNo As System.Windows.Forms.Label
    Protected WithEvents TxtFormNoUpto As AgControls.AgTextBox
    Protected WithEvents LblFormNoUpto As System.Windows.Forms.Label
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents LblPartyName As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblFormNoUptoReq As System.Windows.Forms.Label
    Protected WithEvents LblFormNoFromReq As System.Windows.Forms.Label
    Protected WithEvents TxtFormNoFrom As AgControls.AgTextBox
    Protected WithEvents LblFormNoFrom As System.Windows.Forms.Label
    Protected WithEvents RbtFromDepartment As System.Windows.Forms.RadioButton
    Protected WithEvents RbtFromParty As System.Windows.Forms.RadioButton
    Protected WithEvents BtnFill As System.Windows.Forms.Button
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Form_Receive"
        LogTableName = "Form_Receive_Log"
        MainLineTableCsv = "Form_ReceiveDetail"
        LogLineTableCsv = "Form_ReceiveDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " & _
                " From Form_Receive H " & _
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
                " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select H.UID As SearchCode " & _
               " From Form_Receive_Log H " & _
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
        '                    " H.V_No AS [Entry No] " & _
        '                    " FROM Form_Receive H " & _
        '                    " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " Where 1=1 " & mCondStr

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Receive Type], H.V_Prefix AS Prefix, H.V_Date AS [Receive Date], H.V_No AS [Receive No], " & _
                    " H.FormPrefix AS [Form Prefix], H.FormNoFrom AS [Form No From], H.FormNoUpTo AS [Form No Up To], H.IsFromDepartment AS [IS From Department],  " & _
                    " H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By],  " & _
                    " H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, " & _
                    " D.Div_Name AS Division,SM.Name AS [Site Name], FM.Description AS Form, SGP.DispName AS [Party Name] " & _
                    " FROM Form_Receive H " & _
                    " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                    " LEFT JOIN Form_Master FM ON FM.Code=H.Form  " & _
                    " LEFT JOIN SubGroup SGP ON SGP.SubCode  = H.Party  " & _
                    " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT H.UID as SearchCode, H.DocId, Vt.Description AS [Entry Type], " & _
        '                    " H.V_Date AS [Entry Date], H.V_No AS [Entry No] " & _
        '                    " FROM Form_Receive_Log H " & _
        '                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Receive Type], H.V_Prefix AS Prefix, H.V_Date AS [Receive Date], H.V_No AS [Receive No], " & _
            " H.FormPrefix AS [Form Prefix], H.FormNoFrom AS [Form No From], H.FormNoUpTo AS [Form No Up To], H.IsFromDepartment AS [IS From Department],  " & _
            " H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By],  " & _
            " H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, " & _
            " D.Div_Name AS Division,SM.Name AS [Site Name], FM.Description AS Form, SGP.DispName AS [Party Name] " & _
            " FROM Form_Receive_Log H " & _
            " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
            " LEFT JOIN Form_Master FM ON FM.Code=H.Form  " & _
            " LEFT JOIN SubGroup SGP ON SGP.SubCode  = H.Party  " & _
            " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Form, 100, 5, Col1Form, True, True, False)
            .AddAgTextColumn(Dgl1, Col1FormNo, 165, 5, Col1FormNo, True, True, False)
            .AddAgDateColumn(Dgl1, Col1ReceiveDate, 100, Col1ReceiveDate, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ReceiveFrom, 210, 5, Col1ReceiveFrom, True, True, False)
            .AddAgTextColumn(Dgl1, Col1IsInStock, 100, 5, Col1IsInStock, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Status, 100, 5, Col1Status, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1FormNoSerial, 100, 5, 0, False, Col1FormNoSerial, False, False, False)
            .AddAgTextColumn(Dgl1, Col1Code, 100, 5, Col1Code, False, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 30
        Dgl1.AllowUserToAddRows = False
        'Ini_List()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bLineCode$ = ""
        mQry = "UPDATE Form_Receive_Log " & _
                " SET " & _
                " Form = " & AgL.Chk_Text(TxtForm.AgSelectedValue) & ", " & _
                " FormPrefix = " & AgL.Chk_Text(TxtFormPrefix.Text) & ", " & _
                " FormNoFrom = " & AgL.Chk_Text(TxtFormNoFrom.Text) & ", " & _
                " FormNoUpTo = " & AgL.Chk_Text(TxtFormNoUpto.Text) & ", " & _
                " IsFromDepartment = " & Val(IIf(RbtFromDepartment.Checked, 1, 0)) & ", " & _
                " Party = " & AgL.Chk_Text(TxtParty.AgSelectedValue) & " " & _
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1FormNo, I).Value <> "" Then
                    If .Item(Col1Code, I).Value = "" Then
                        bLineCode = AgL.GetMaxId("Form_ReceiveDetail_Log", "Code", AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue, 8, True, True)
                        mSr += 1
                        mQry = " INSERT INTO Form_ReceiveDetail_Log(UID, DocId, Sr, Form, FormNo, ReceiveDate, ReceiveFrom, " & _
                                    " IsInStock, Status, FormPrefix, FormNoSerial, Code) " & _
                                    " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ",  " & _
                                    " " & mSr & ", " & AgL.Chk_Text(.AgSelectedValue(Col1Form, I)) & ", " & _
                                    " " & AgL.Chk_Text(.Item(Col1FormNo, I).Value) & ",  " & _
                                    " " & AgL.Chk_Text(.Item(Col1ReceiveDate, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(.AgSelectedValue(Col1ReceiveFrom, I)) & ", " & _
                                    " " & Val(.Item(Col1IsInStock, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(.Item(Col1Status, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(TxtFormPrefix.Text) & ", " & _
                                    " " & Val(.Item(Col1FormNoSerial, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(bLineCode) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        mQry = " UPDATE Form_ReceiveDetail_Log SET " & _
                                " Status = " & AgL.Chk_Text(.Item(Col1Status, I).Value) & " " & _
                                " WHERE Code = " & AgL.Chk_Text(.Item(Col1Code, I).Value) & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select H.* " & _
                " From Form_Receive H " & _
                " Where H.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select H.* " & _
                " From Form_Receive_Log H " & _
                " Where H.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtForm.AgSelectedValue = AgL.XNull(.Rows(0)("Form"))
                TxtFormPrefix.Text = AgL.XNull(.Rows(0)("FormPrefix"))
                TxtFormNoFrom.Text = AgL.XNull(.Rows(0)("FormNoFrom"))
                TxtFormNoUpto.Text = AgL.XNull(.Rows(0)("FormNoUpTo"))
                TxtParty.AgSelectedValue = AgL.XNull(.Rows(0)("Party"))
                RbtFromDepartment.Checked = AgL.VNull(.Rows(0)("IsFromDepartment"))
                RbtFromParty.Checked = Not RbtFromDepartment.Checked

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from Form_ReceiveDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from Form_ReceiveDetail_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(Col1Code, I).Value = AgL.XNull(.Rows(I)("Code"))
                            Dgl1.AgSelectedValue(Col1Form, I) = AgL.XNull(.Rows(I)("Form"))
                            Dgl1.Item(Col1FormNo, I).Value = AgL.XNull(.Rows(I)("FormNo"))
                            Dgl1.Item(Col1ReceiveDate, I).Value = AgL.XNull(.Rows(I)("ReceiveDate"))
                            Dgl1.AgSelectedValue(Col1ReceiveFrom, I) = AgL.XNull(.Rows(I)("ReceiveFrom"))
                            Dgl1.Item(Col1IsInStock, I).Value = AgL.XNull(.Rows(I)("IsInStock"))
                            Dgl1.Item(Col1Status, I).Value = AgL.XNull(.Rows(I)("Status"))
                            Dgl1.Item(Col1FormNoSerial, I).Value = AgL.XNull(.Rows(I)("FormNoSerial"))
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
        mQry = " SELECT F.Code AS Code, F.Description AS Form FROM Form_Master F "
        TxtForm.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Dgl1.AgHelpDataSet(Col1Form) = TxtForm.AgHelpDataSet

        mQry = "SELECT Sg.SubCode AS Code, Sg.DispName AS Name, Sg.ManualCode " & _
            " FROM SubGroup Sg " & _
            " Where Sg.Nature In ('Customer', 'Supplier') "
        TxtParty.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Dgl1.AgHelpDataSet(Col1ReceiveFrom) = TxtParty.AgHelpDataSet

        mQry = " Select 'Stock' As Code, 'Stock' As Status " & _
                " UNION ALL " & _
                " Select 'Lost' As Code, 'Lost' As Status " & _
                " UNION ALL " & _
                " Select 'Issued' As Code, 'Issued' As Status "
        Dgl1.AgHelpDataSet(Col1Status) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtV_Date, LblV_Date.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtForm, LblForm.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtFormPrefix, LblFormPrefix.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtFormNoFrom, LblFormNoFrom.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtFormNoUpto, LblFormNoUpto.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1FormNo).Index) Then passed = False : Exit Sub

    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub ProcFill()
        Dim I As Integer = 0, J As Integer = 0
        Try
            With Dgl1
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                For I = 0 To Val(TxtFormNoUpto.Text) - Val(TxtFormNoFrom.Text)
                    Dgl1.Rows.Add()
                    J = Val(TxtFormNoFrom.Text) + I
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.AgSelectedValue(Col1Form, I) = TxtForm.AgSelectedValue
                    Dgl1.Item(Col1FormNo, I).Value = TxtFormPrefix.Text & J.ToString
                    Dgl1.Item(Col1ReceiveDate, I).Value = TxtV_Date.Text
                    Dgl1.AgSelectedValue(Col1ReceiveFrom, I) = TxtParty.AgSelectedValue
                    Dgl1.Item(Col1IsInStock, I).Value = 1
                    Dgl1.Item(Col1Status, I).Value = "Stock"
                    Dgl1.Item(Col1FormNoSerial, I).Value = J
                Next I
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        ProcFill()
    End Sub

    Private Sub RbtFromDepartment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtFromParty.Click, RbtFromDepartment.Click
        Try
            If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub
            Select Case sender.Name
                Case RbtFromDepartment.Name
                    TxtParty.AgSelectedValue = ""
                    TxtParty.Enabled = False

                Case RbtFromParty.Name
                    TxtParty.Enabled = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmFormReceive_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
            TxtParty.Enabled = False
            BtnFill.Enabled = False
        Else
            TxtParty.Enabled = True
            BtnFill.Enabled = True
            RbtFromParty.Checked = True
        End If
    End Sub
End Class
