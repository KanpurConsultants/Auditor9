Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Drawing.Printing
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmChequeData
    Inherits AgTemplate.TempMaster

    Dim mQry$
    Friend WithEvents brnPrint As Button
    Public WithEvents Label10 As Label
    Public WithEvents Label9 As Label
    Public WithEvents Label1 As Label
    Friend WithEvents TxtChqY4 As AgControls.AgTextBox
    Friend WithEvents TxtChqY3 As AgControls.AgTextBox
    Friend WithEvents LblAcPayee As Label
    Friend WithEvents cbdAcPayee As CheckBox
    Dim LastBankPrinted As String = ""

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmChequeData))
        Me.TxtVoucherNo = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.txtChqD2 = New AgControls.AgTextBox()
        Me.txtChqM1 = New AgControls.AgTextBox()
        Me.lblChequeStatus = New System.Windows.Forms.Label()
        Me.txtChequeNo = New AgControls.AgTextBox()
        Me.txtChqM2 = New AgControls.AgTextBox()
        Me.txtChqD1 = New AgControls.AgTextBox()
        Me.txtChqY2 = New AgControls.AgTextBox()
        Me.pnDate = New System.Windows.Forms.Panel()
        Me.TxtChqY4 = New AgControls.AgTextBox()
        Me.TxtChqY3 = New AgControls.AgTextBox()
        Me.txtChqY1 = New AgControls.AgTextBox()
        Me.lblChequeNo = New System.Windows.Forms.Label()
        Me.txtParticulars = New AgControls.AgTextBox()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlParent = New System.Windows.Forms.Panel()
        Me.LblAcPayee = New System.Windows.Forms.Label()
        Me.txtChqName = New AgControls.AgTextBox()
        Me.txtChqAmount = New AgControls.AgTextBox()
        Me.lbChqAmountInWord1 = New System.Windows.Forms.Label()
        Me.lbChqAmountInWord2 = New System.Windows.Forms.Label()
        Me.imgChequePreview = New System.Windows.Forms.PictureBox()
        Me.txtPayeeName = New AgControls.AgTextBox()
        Me.txtBankName = New AgControls.AgTextBox()
        Me.lblIssuedDate = New System.Windows.Forms.Label()
        Me.cmbChequeStatus = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cbdAcPayee = New System.Windows.Forms.CheckBox()
        Me.ckbamount = New System.Windows.Forms.CheckBox()
        Me.ckbPayeename = New System.Windows.Forms.CheckBox()
        Me.ckbdate = New System.Windows.Forms.CheckBox()
        Me.lblPayeeName = New System.Windows.Forms.Label()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.txtAmount = New AgControls.AgTextBox()
        Me.TextBox3 = New AgControls.AgTextBox()
        Me.TextBox2 = New AgControls.AgTextBox()
        Me.TextBox1 = New AgControls.AgTextBox()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.TxtChqDate = New AgControls.AgTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtIssueDate = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.brnPrint = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnDate.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlParent.SuspendLayout()
        CType(Me.imgChequePreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(1474, 41)
        Me.Topctrl1.TabIndex = 9999
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 707)
        Me.GroupBox1.Size = New System.Drawing.Size(1516, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 711)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 774)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(221, 711)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 711)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(136, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(704, 711)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(457, 711)
        Me.GBoxDivision.Size = New System.Drawing.Size(135, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(129, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'TxtVoucherNo
        '
        Me.TxtVoucherNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtVoucherNo.AgLastValueTag = Nothing
        Me.TxtVoucherNo.AgLastValueText = Nothing
        Me.TxtVoucherNo.AgMandatory = False
        Me.TxtVoucherNo.AgMasterHelp = True
        Me.TxtVoucherNo.AgNumberLeftPlaces = 0
        Me.TxtVoucherNo.AgNumberNegetiveAllow = False
        Me.TxtVoucherNo.AgNumberRightPlaces = 0
        Me.TxtVoucherNo.AgPickFromLastValue = False
        Me.TxtVoucherNo.AgRowFilter = ""
        Me.TxtVoucherNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVoucherNo.AgSelectedValue = Nothing
        Me.TxtVoucherNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVoucherNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVoucherNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVoucherNo.Location = New System.Drawing.Point(130, 60)
        Me.TxtVoucherNo.MaxLength = 50
        Me.TxtVoucherNo.Name = "TxtVoucherNo"
        Me.TxtVoucherNo.Size = New System.Drawing.Size(67, 18)
        Me.TxtVoucherNo.TabIndex = 1001
        Me.TxtVoucherNo.TabStop = False
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(23, 61)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(79, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Voucher No."
        '
        'txtChqD2
        '
        Me.txtChqD2.AgAllowUserToEnableMasterHelp = False
        Me.txtChqD2.AgLastValueTag = Nothing
        Me.txtChqD2.AgLastValueText = Nothing
        Me.txtChqD2.AgMandatory = False
        Me.txtChqD2.AgMasterHelp = False
        Me.txtChqD2.AgNumberLeftPlaces = 0
        Me.txtChqD2.AgNumberNegetiveAllow = False
        Me.txtChqD2.AgNumberRightPlaces = 0
        Me.txtChqD2.AgPickFromLastValue = False
        Me.txtChqD2.AgRowFilter = ""
        Me.txtChqD2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChqD2.AgSelectedValue = Nothing
        Me.txtChqD2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChqD2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChqD2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChqD2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqD2.ForeColor = System.Drawing.Color.Black
        Me.txtChqD2.Location = New System.Drawing.Point(23, 0)
        Me.txtChqD2.Name = "txtChqD2"
        Me.txtChqD2.Size = New System.Drawing.Size(20, 22)
        Me.txtChqD2.TabIndex = 44
        Me.txtChqD2.TabStop = False
        '
        'txtChqM1
        '
        Me.txtChqM1.AgAllowUserToEnableMasterHelp = False
        Me.txtChqM1.AgLastValueTag = Nothing
        Me.txtChqM1.AgLastValueText = Nothing
        Me.txtChqM1.AgMandatory = False
        Me.txtChqM1.AgMasterHelp = False
        Me.txtChqM1.AgNumberLeftPlaces = 0
        Me.txtChqM1.AgNumberNegetiveAllow = False
        Me.txtChqM1.AgNumberRightPlaces = 0
        Me.txtChqM1.AgPickFromLastValue = False
        Me.txtChqM1.AgRowFilter = ""
        Me.txtChqM1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChqM1.AgSelectedValue = Nothing
        Me.txtChqM1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChqM1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChqM1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChqM1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqM1.ForeColor = System.Drawing.Color.Black
        Me.txtChqM1.Location = New System.Drawing.Point(46, 0)
        Me.txtChqM1.Name = "txtChqM1"
        Me.txtChqM1.Size = New System.Drawing.Size(20, 22)
        Me.txtChqM1.TabIndex = 45
        Me.txtChqM1.TabStop = False
        '
        'lblChequeStatus
        '
        Me.lblChequeStatus.AutoSize = True
        Me.lblChequeStatus.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChequeStatus.Location = New System.Drawing.Point(595, 69)
        Me.lblChequeStatus.Name = "lblChequeStatus"
        Me.lblChequeStatus.Size = New System.Drawing.Size(103, 17)
        Me.lblChequeStatus.TabIndex = 92
        Me.lblChequeStatus.Text = "Cheque Status"
        Me.lblChequeStatus.Visible = False
        '
        'txtChequeNo
        '
        Me.txtChequeNo.AgAllowUserToEnableMasterHelp = False
        Me.txtChequeNo.AgLastValueTag = Nothing
        Me.txtChequeNo.AgLastValueText = Nothing
        Me.txtChequeNo.AgMandatory = True
        Me.txtChequeNo.AgMasterHelp = False
        Me.txtChequeNo.AgNumberLeftPlaces = 0
        Me.txtChequeNo.AgNumberNegetiveAllow = False
        Me.txtChequeNo.AgNumberRightPlaces = 0
        Me.txtChequeNo.AgPickFromLastValue = False
        Me.txtChequeNo.AgRowFilter = ""
        Me.txtChequeNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChequeNo.AgSelectedValue = Nothing
        Me.txtChequeNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChequeNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChequeNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChequeNo.Location = New System.Drawing.Point(371, 155)
        Me.txtChequeNo.MaxLength = 10
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(143, 18)
        Me.txtChequeNo.TabIndex = 5
        '
        'txtChqM2
        '
        Me.txtChqM2.AgAllowUserToEnableMasterHelp = False
        Me.txtChqM2.AgLastValueTag = Nothing
        Me.txtChqM2.AgLastValueText = Nothing
        Me.txtChqM2.AgMandatory = False
        Me.txtChqM2.AgMasterHelp = False
        Me.txtChqM2.AgNumberLeftPlaces = 0
        Me.txtChqM2.AgNumberNegetiveAllow = False
        Me.txtChqM2.AgNumberRightPlaces = 0
        Me.txtChqM2.AgPickFromLastValue = False
        Me.txtChqM2.AgRowFilter = ""
        Me.txtChqM2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChqM2.AgSelectedValue = Nothing
        Me.txtChqM2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChqM2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChqM2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChqM2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqM2.ForeColor = System.Drawing.Color.Black
        Me.txtChqM2.Location = New System.Drawing.Point(69, 0)
        Me.txtChqM2.Name = "txtChqM2"
        Me.txtChqM2.Size = New System.Drawing.Size(20, 22)
        Me.txtChqM2.TabIndex = 46
        Me.txtChqM2.TabStop = False
        '
        'txtChqD1
        '
        Me.txtChqD1.AgAllowUserToEnableMasterHelp = False
        Me.txtChqD1.AgLastValueTag = Nothing
        Me.txtChqD1.AgLastValueText = Nothing
        Me.txtChqD1.AgMandatory = False
        Me.txtChqD1.AgMasterHelp = False
        Me.txtChqD1.AgNumberLeftPlaces = 0
        Me.txtChqD1.AgNumberNegetiveAllow = False
        Me.txtChqD1.AgNumberRightPlaces = 0
        Me.txtChqD1.AgPickFromLastValue = False
        Me.txtChqD1.AgRowFilter = ""
        Me.txtChqD1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChqD1.AgSelectedValue = Nothing
        Me.txtChqD1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChqD1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChqD1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChqD1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqD1.ForeColor = System.Drawing.Color.Black
        Me.txtChqD1.Location = New System.Drawing.Point(0, 0)
        Me.txtChqD1.Name = "txtChqD1"
        Me.txtChqD1.Size = New System.Drawing.Size(20, 22)
        Me.txtChqD1.TabIndex = 43
        Me.txtChqD1.TabStop = False
        '
        'txtChqY2
        '
        Me.txtChqY2.AgAllowUserToEnableMasterHelp = False
        Me.txtChqY2.AgLastValueTag = Nothing
        Me.txtChqY2.AgLastValueText = Nothing
        Me.txtChqY2.AgMandatory = False
        Me.txtChqY2.AgMasterHelp = False
        Me.txtChqY2.AgNumberLeftPlaces = 0
        Me.txtChqY2.AgNumberNegetiveAllow = False
        Me.txtChqY2.AgNumberRightPlaces = 0
        Me.txtChqY2.AgPickFromLastValue = False
        Me.txtChqY2.AgRowFilter = ""
        Me.txtChqY2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChqY2.AgSelectedValue = Nothing
        Me.txtChqY2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChqY2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChqY2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChqY2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqY2.ForeColor = System.Drawing.Color.Black
        Me.txtChqY2.Location = New System.Drawing.Point(114, 0)
        Me.txtChqY2.Name = "txtChqY2"
        Me.txtChqY2.Size = New System.Drawing.Size(20, 22)
        Me.txtChqY2.TabIndex = 42
        Me.txtChqY2.TabStop = False
        '
        'pnDate
        '
        Me.pnDate.BackColor = System.Drawing.Color.Transparent
        Me.pnDate.Controls.Add(Me.TxtChqY4)
        Me.pnDate.Controls.Add(Me.TxtChqY3)
        Me.pnDate.Controls.Add(Me.txtChqD1)
        Me.pnDate.Controls.Add(Me.txtChqY2)
        Me.pnDate.Controls.Add(Me.txtChqD2)
        Me.pnDate.Controls.Add(Me.txtChqM1)
        Me.pnDate.Controls.Add(Me.txtChqM2)
        Me.pnDate.Controls.Add(Me.txtChqY1)
        Me.pnDate.Location = New System.Drawing.Point(620, 50)
        Me.pnDate.Name = "pnDate"
        Me.pnDate.Size = New System.Drawing.Size(180, 25)
        Me.pnDate.TabIndex = 52
        '
        'TxtChqY4
        '
        Me.TxtChqY4.AgAllowUserToEnableMasterHelp = False
        Me.TxtChqY4.AgLastValueTag = Nothing
        Me.TxtChqY4.AgLastValueText = Nothing
        Me.TxtChqY4.AgMandatory = False
        Me.TxtChqY4.AgMasterHelp = False
        Me.TxtChqY4.AgNumberLeftPlaces = 0
        Me.TxtChqY4.AgNumberNegetiveAllow = False
        Me.TxtChqY4.AgNumberRightPlaces = 0
        Me.TxtChqY4.AgPickFromLastValue = False
        Me.TxtChqY4.AgRowFilter = ""
        Me.TxtChqY4.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChqY4.AgSelectedValue = Nothing
        Me.TxtChqY4.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChqY4.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtChqY4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChqY4.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChqY4.ForeColor = System.Drawing.Color.Black
        Me.TxtChqY4.Location = New System.Drawing.Point(160, 0)
        Me.TxtChqY4.Name = "TxtChqY4"
        Me.TxtChqY4.Size = New System.Drawing.Size(20, 22)
        Me.TxtChqY4.TabIndex = 49
        Me.TxtChqY4.TabStop = False
        '
        'TxtChqY3
        '
        Me.TxtChqY3.AgAllowUserToEnableMasterHelp = False
        Me.TxtChqY3.AgLastValueTag = Nothing
        Me.TxtChqY3.AgLastValueText = Nothing
        Me.TxtChqY3.AgMandatory = False
        Me.TxtChqY3.AgMasterHelp = False
        Me.TxtChqY3.AgNumberLeftPlaces = 0
        Me.TxtChqY3.AgNumberNegetiveAllow = False
        Me.TxtChqY3.AgNumberRightPlaces = 0
        Me.TxtChqY3.AgPickFromLastValue = False
        Me.TxtChqY3.AgRowFilter = ""
        Me.TxtChqY3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChqY3.AgSelectedValue = Nothing
        Me.TxtChqY3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChqY3.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtChqY3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChqY3.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChqY3.ForeColor = System.Drawing.Color.Black
        Me.TxtChqY3.Location = New System.Drawing.Point(137, 0)
        Me.TxtChqY3.Name = "TxtChqY3"
        Me.TxtChqY3.Size = New System.Drawing.Size(20, 22)
        Me.TxtChqY3.TabIndex = 48
        Me.TxtChqY3.TabStop = False
        '
        'txtChqY1
        '
        Me.txtChqY1.AgAllowUserToEnableMasterHelp = False
        Me.txtChqY1.AgLastValueTag = Nothing
        Me.txtChqY1.AgLastValueText = Nothing
        Me.txtChqY1.AgMandatory = False
        Me.txtChqY1.AgMasterHelp = False
        Me.txtChqY1.AgNumberLeftPlaces = 0
        Me.txtChqY1.AgNumberNegetiveAllow = False
        Me.txtChqY1.AgNumberRightPlaces = 0
        Me.txtChqY1.AgPickFromLastValue = False
        Me.txtChqY1.AgRowFilter = ""
        Me.txtChqY1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChqY1.AgSelectedValue = Nothing
        Me.txtChqY1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChqY1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChqY1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChqY1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqY1.ForeColor = System.Drawing.Color.Black
        Me.txtChqY1.Location = New System.Drawing.Point(91, 0)
        Me.txtChqY1.Name = "txtChqY1"
        Me.txtChqY1.Size = New System.Drawing.Size(20, 22)
        Me.txtChqY1.TabIndex = 47
        Me.txtChqY1.TabStop = False
        '
        'lblChequeNo
        '
        Me.lblChequeNo.AutoSize = True
        Me.lblChequeNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChequeNo.Location = New System.Drawing.Point(258, 156)
        Me.lblChequeNo.Name = "lblChequeNo"
        Me.lblChequeNo.Size = New System.Drawing.Size(72, 16)
        Me.lblChequeNo.TabIndex = 90
        Me.lblChequeNo.Text = "Cheque No"
        '
        'txtParticulars
        '
        Me.txtParticulars.AgAllowUserToEnableMasterHelp = False
        Me.txtParticulars.AgLastValueTag = Nothing
        Me.txtParticulars.AgLastValueText = Nothing
        Me.txtParticulars.AgMandatory = True
        Me.txtParticulars.AgMasterHelp = False
        Me.txtParticulars.AgNumberLeftPlaces = 0
        Me.txtParticulars.AgNumberNegetiveAllow = False
        Me.txtParticulars.AgNumberRightPlaces = 0
        Me.txtParticulars.AgPickFromLastValue = False
        Me.txtParticulars.AgRowFilter = ""
        Me.txtParticulars.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtParticulars.AgSelectedValue = Nothing
        Me.txtParticulars.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtParticulars.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtParticulars.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtParticulars.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParticulars.Location = New System.Drawing.Point(130, 179)
        Me.txtParticulars.Name = "txtParticulars"
        Me.txtParticulars.Size = New System.Drawing.Size(384, 18)
        Me.txtParticulars.TabIndex = 6
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 200
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'pnlParent
        '
        Me.pnlParent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlParent.Controls.Add(Me.LblAcPayee)
        Me.pnlParent.Controls.Add(Me.pnDate)
        Me.pnlParent.Controls.Add(Me.txtChqName)
        Me.pnlParent.Controls.Add(Me.txtChqAmount)
        Me.pnlParent.Controls.Add(Me.lbChqAmountInWord1)
        Me.pnlParent.Controls.Add(Me.lbChqAmountInWord2)
        Me.pnlParent.Controls.Add(Me.imgChequePreview)
        Me.pnlParent.Location = New System.Drawing.Point(25, 219)
        Me.pnlParent.Name = "pnlParent"
        Me.pnlParent.Size = New System.Drawing.Size(1054, 337)
        Me.pnlParent.TabIndex = 56
        '
        'LblAcPayee
        '
        Me.LblAcPayee.BackColor = System.Drawing.Color.Transparent
        Me.LblAcPayee.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcPayee.ForeColor = System.Drawing.Color.Black
        Me.LblAcPayee.Location = New System.Drawing.Point(86, 14)
        Me.LblAcPayee.Name = "LblAcPayee"
        Me.LblAcPayee.Size = New System.Drawing.Size(141, 47)
        Me.LblAcPayee.TabIndex = 90
        Me.LblAcPayee.Text = "-------------------------" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "       A/C PAYEE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-------------------------"
        '
        'txtChqName
        '
        Me.txtChqName.AgAllowUserToEnableMasterHelp = False
        Me.txtChqName.AgLastValueTag = Nothing
        Me.txtChqName.AgLastValueText = Nothing
        Me.txtChqName.AgMandatory = False
        Me.txtChqName.AgMasterHelp = False
        Me.txtChqName.AgNumberLeftPlaces = 0
        Me.txtChqName.AgNumberNegetiveAllow = False
        Me.txtChqName.AgNumberRightPlaces = 0
        Me.txtChqName.AgPickFromLastValue = False
        Me.txtChqName.AgRowFilter = ""
        Me.txtChqName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChqName.AgSelectedValue = Nothing
        Me.txtChqName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChqName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChqName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChqName.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqName.ForeColor = System.Drawing.Color.Black
        Me.txtChqName.Location = New System.Drawing.Point(100, 75)
        Me.txtChqName.Name = "txtChqName"
        Me.txtChqName.Size = New System.Drawing.Size(340, 18)
        Me.txtChqName.TabIndex = 49
        Me.txtChqName.TabStop = False
        '
        'txtChqAmount
        '
        Me.txtChqAmount.AgAllowUserToEnableMasterHelp = False
        Me.txtChqAmount.AgLastValueTag = Nothing
        Me.txtChqAmount.AgLastValueText = Nothing
        Me.txtChqAmount.AgMandatory = False
        Me.txtChqAmount.AgMasterHelp = False
        Me.txtChqAmount.AgNumberLeftPlaces = 0
        Me.txtChqAmount.AgNumberNegetiveAllow = False
        Me.txtChqAmount.AgNumberRightPlaces = 0
        Me.txtChqAmount.AgPickFromLastValue = False
        Me.txtChqAmount.AgRowFilter = ""
        Me.txtChqAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtChqAmount.AgSelectedValue = Nothing
        Me.txtChqAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtChqAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtChqAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChqAmount.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqAmount.ForeColor = System.Drawing.Color.Black
        Me.txtChqAmount.Location = New System.Drawing.Point(652, 120)
        Me.txtChqAmount.Name = "txtChqAmount"
        Me.txtChqAmount.Size = New System.Drawing.Size(164, 22)
        Me.txtChqAmount.TabIndex = 48
        Me.txtChqAmount.TabStop = False
        '
        'lbChqAmountInWord1
        '
        Me.lbChqAmountInWord1.BackColor = System.Drawing.Color.White
        Me.lbChqAmountInWord1.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbChqAmountInWord1.ForeColor = System.Drawing.Color.Black
        Me.lbChqAmountInWord1.Location = New System.Drawing.Point(65, 158)
        Me.lbChqAmountInWord1.Name = "lbChqAmountInWord1"
        Me.lbChqAmountInWord1.Size = New System.Drawing.Size(547, 30)
        Me.lbChqAmountInWord1.TabIndex = 48
        '
        'lbChqAmountInWord2
        '
        Me.lbChqAmountInWord2.BackColor = System.Drawing.Color.White
        Me.lbChqAmountInWord2.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbChqAmountInWord2.ForeColor = System.Drawing.Color.Black
        Me.lbChqAmountInWord2.Location = New System.Drawing.Point(44, 210)
        Me.lbChqAmountInWord2.Name = "lbChqAmountInWord2"
        Me.lbChqAmountInWord2.Size = New System.Drawing.Size(547, 30)
        Me.lbChqAmountInWord2.TabIndex = 53
        '
        'imgChequePreview
        '
        Me.imgChequePreview.BackColor = System.Drawing.Color.Transparent
        Me.imgChequePreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgChequePreview.Location = New System.Drawing.Point(3, 0)
        Me.imgChequePreview.Name = "imgChequePreview"
        Me.imgChequePreview.Size = New System.Drawing.Size(1035, 320)
        Me.imgChequePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgChequePreview.TabIndex = 89
        Me.imgChequePreview.TabStop = False
        '
        'txtPayeeName
        '
        Me.txtPayeeName.AgAllowUserToEnableMasterHelp = False
        Me.txtPayeeName.AgLastValueTag = Nothing
        Me.txtPayeeName.AgLastValueText = Nothing
        Me.txtPayeeName.AgMandatory = True
        Me.txtPayeeName.AgMasterHelp = False
        Me.txtPayeeName.AgNumberLeftPlaces = 0
        Me.txtPayeeName.AgNumberNegetiveAllow = False
        Me.txtPayeeName.AgNumberRightPlaces = 0
        Me.txtPayeeName.AgPickFromLastValue = False
        Me.txtPayeeName.AgRowFilter = ""
        Me.txtPayeeName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtPayeeName.AgSelectedValue = Nothing
        Me.txtPayeeName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtPayeeName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtPayeeName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPayeeName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayeeName.Location = New System.Drawing.Point(130, 107)
        Me.txtPayeeName.Name = "txtPayeeName"
        Me.txtPayeeName.Size = New System.Drawing.Size(384, 18)
        Me.txtPayeeName.TabIndex = 1
        '
        'txtBankName
        '
        Me.txtBankName.AgAllowUserToEnableMasterHelp = False
        Me.txtBankName.AgLastValueTag = Nothing
        Me.txtBankName.AgLastValueText = Nothing
        Me.txtBankName.AgMandatory = True
        Me.txtBankName.AgMasterHelp = False
        Me.txtBankName.AgNumberLeftPlaces = 0
        Me.txtBankName.AgNumberNegetiveAllow = False
        Me.txtBankName.AgNumberRightPlaces = 0
        Me.txtBankName.AgPickFromLastValue = False
        Me.txtBankName.AgRowFilter = ""
        Me.txtBankName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtBankName.AgSelectedValue = Nothing
        Me.txtBankName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtBankName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.Location = New System.Drawing.Point(130, 83)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(384, 18)
        Me.txtBankName.TabIndex = 0
        '
        'lblIssuedDate
        '
        Me.lblIssuedDate.AutoSize = True
        Me.lblIssuedDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssuedDate.Location = New System.Drawing.Point(23, 156)
        Me.lblIssuedDate.Name = "lblIssuedDate"
        Me.lblIssuedDate.Size = New System.Drawing.Size(77, 16)
        Me.lblIssuedDate.TabIndex = 94
        Me.lblIssuedDate.Text = "Issued Date"
        '
        'cmbChequeStatus
        '
        Me.cmbChequeStatus.FormattingEnabled = True
        Me.cmbChequeStatus.Items.AddRange(New Object() {"Draft Cheque", "Issued Cheque", "Cancel Cheque"})
        Me.cmbChequeStatus.Location = New System.Drawing.Point(701, 68)
        Me.cmbChequeStatus.Name = "cmbChequeStatus"
        Me.cmbChequeStatus.Size = New System.Drawing.Size(105, 21)
        Me.cmbChequeStatus.TabIndex = 7
        Me.cmbChequeStatus.Text = "Draft Cheque"
        Me.cmbChequeStatus.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cbdAcPayee)
        Me.GroupBox3.Controls.Add(Me.ckbamount)
        Me.GroupBox3.Controls.Add(Me.ckbPayeename)
        Me.GroupBox3.Controls.Add(Me.ckbdate)
        Me.GroupBox3.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(543, 91)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(297, 59)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Display"
        '
        'cbdAcPayee
        '
        Me.cbdAcPayee.AutoSize = True
        Me.cbdAcPayee.Checked = True
        Me.cbdAcPayee.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbdAcPayee.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbdAcPayee.Location = New System.Drawing.Point(13, 22)
        Me.cbdAcPayee.Name = "cbdAcPayee"
        Me.cbdAcPayee.Size = New System.Drawing.Size(80, 19)
        Me.cbdAcPayee.TabIndex = 3
        Me.cbdAcPayee.TabStop = False
        Me.cbdAcPayee.Text = "A/c Payee"
        Me.cbdAcPayee.UseVisualStyleBackColor = True
        '
        'ckbamount
        '
        Me.ckbamount.AutoSize = True
        Me.ckbamount.Checked = True
        Me.ckbamount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckbamount.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbamount.Location = New System.Drawing.Point(221, 22)
        Me.ckbamount.Name = "ckbamount"
        Me.ckbamount.Size = New System.Drawing.Size(69, 19)
        Me.ckbamount.TabIndex = 2
        Me.ckbamount.TabStop = False
        Me.ckbamount.Text = "Amount"
        Me.ckbamount.UseVisualStyleBackColor = True
        '
        'ckbPayeename
        '
        Me.ckbPayeename.AutoSize = True
        Me.ckbPayeename.Checked = True
        Me.ckbPayeename.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckbPayeename.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbPayeename.Location = New System.Drawing.Point(154, 22)
        Me.ckbPayeename.Name = "ckbPayeename"
        Me.ckbPayeename.Size = New System.Drawing.Size(57, 19)
        Me.ckbPayeename.TabIndex = 1
        Me.ckbPayeename.TabStop = False
        Me.ckbPayeename.Text = "Name"
        Me.ckbPayeename.UseVisualStyleBackColor = True
        '
        'ckbdate
        '
        Me.ckbdate.AutoSize = True
        Me.ckbdate.Checked = True
        Me.ckbdate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckbdate.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbdate.Location = New System.Drawing.Point(97, 22)
        Me.ckbdate.Name = "ckbdate"
        Me.ckbdate.Size = New System.Drawing.Size(51, 19)
        Me.ckbdate.TabIndex = 0
        Me.ckbdate.TabStop = False
        Me.ckbdate.Text = "Date"
        Me.ckbdate.UseVisualStyleBackColor = True
        '
        'lblPayeeName
        '
        Me.lblPayeeName.AutoSize = True
        Me.lblPayeeName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayeeName.Location = New System.Drawing.Point(23, 111)
        Me.lblPayeeName.Name = "lblPayeeName"
        Me.lblPayeeName.Size = New System.Drawing.Size(83, 16)
        Me.lblPayeeName.TabIndex = 2
        Me.lblPayeeName.Text = "Payee Name"
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(23, 86)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(76, 16)
        Me.lblBankName.TabIndex = 0
        Me.lblBankName.Text = "Bank Name"
        '
        'txtAmount
        '
        Me.txtAmount.AgAllowUserToEnableMasterHelp = False
        Me.txtAmount.AgLastValueTag = Nothing
        Me.txtAmount.AgLastValueText = Nothing
        Me.txtAmount.AgMandatory = True
        Me.txtAmount.AgMasterHelp = False
        Me.txtAmount.AgNumberLeftPlaces = 0
        Me.txtAmount.AgNumberNegetiveAllow = False
        Me.txtAmount.AgNumberRightPlaces = 0
        Me.txtAmount.AgPickFromLastValue = False
        Me.txtAmount.AgRowFilter = ""
        Me.txtAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtAmount.AgSelectedValue = Nothing
        Me.txtAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(371, 131)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(143, 18)
        Me.txtAmount.TabIndex = 3
        '
        'TextBox3
        '
        Me.TextBox3.AgAllowUserToEnableMasterHelp = False
        Me.TextBox3.AgLastValueTag = Nothing
        Me.TextBox3.AgLastValueText = Nothing
        Me.TextBox3.AgMandatory = False
        Me.TextBox3.AgMasterHelp = False
        Me.TextBox3.AgNumberLeftPlaces = 0
        Me.TextBox3.AgNumberNegetiveAllow = False
        Me.TextBox3.AgNumberRightPlaces = 0
        Me.TextBox3.AgPickFromLastValue = False
        Me.TextBox3.AgRowFilter = ""
        Me.TextBox3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TextBox3.AgSelectedValue = Nothing
        Me.TextBox3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TextBox3.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TextBox3.Location = New System.Drawing.Point(405, 57)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(75, 20)
        Me.TextBox3.TabIndex = 3
        Me.TextBox3.Text = "-580"
        Me.TextBox3.Visible = False
        '
        'TextBox2
        '
        Me.TextBox2.AgAllowUserToEnableMasterHelp = False
        Me.TextBox2.AgLastValueTag = Nothing
        Me.TextBox2.AgLastValueText = Nothing
        Me.TextBox2.AgMandatory = False
        Me.TextBox2.AgMasterHelp = False
        Me.TextBox2.AgNumberLeftPlaces = 0
        Me.TextBox2.AgNumberNegetiveAllow = False
        Me.TextBox2.AgNumberRightPlaces = 0
        Me.TextBox2.AgPickFromLastValue = False
        Me.TextBox2.AgRowFilter = ""
        Me.TextBox2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TextBox2.AgSelectedValue = Nothing
        Me.TextBox2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TextBox2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TextBox2.Location = New System.Drawing.Point(324, 57)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(75, 20)
        Me.TextBox2.TabIndex = 2
        Me.TextBox2.Text = "-10"
        Me.TextBox2.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.AgAllowUserToEnableMasterHelp = False
        Me.TextBox1.AgLastValueTag = Nothing
        Me.TextBox1.AgLastValueText = Nothing
        Me.TextBox1.AgMandatory = False
        Me.TextBox1.AgMasterHelp = False
        Me.TextBox1.AgNumberLeftPlaces = 0
        Me.TextBox1.AgNumberNegetiveAllow = False
        Me.TextBox1.AgNumberRightPlaces = 0
        Me.TextBox1.AgPickFromLastValue = False
        Me.TextBox1.AgRowFilter = ""
        Me.TextBox1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TextBox1.AgSelectedValue = Nothing
        Me.TextBox1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TextBox1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TextBox1.Location = New System.Drawing.Point(243, 57)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(75, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "90"
        Me.TextBox1.Visible = False
        '
        'PrintDocument1
        '
        '
        'TxtChqDate
        '
        Me.TxtChqDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtChqDate.AgLastValueTag = Nothing
        Me.TxtChqDate.AgLastValueText = Nothing
        Me.TxtChqDate.AgMandatory = True
        Me.TxtChqDate.AgMasterHelp = False
        Me.TxtChqDate.AgNumberLeftPlaces = 0
        Me.TxtChqDate.AgNumberNegetiveAllow = False
        Me.TxtChqDate.AgNumberRightPlaces = 0
        Me.TxtChqDate.AgPickFromLastValue = False
        Me.TxtChqDate.AgRowFilter = ""
        Me.TxtChqDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChqDate.AgSelectedValue = Nothing
        Me.TxtChqDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChqDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtChqDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChqDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChqDate.Location = New System.Drawing.Point(130, 131)
        Me.TxtChqDate.Name = "TxtChqDate"
        Me.TxtChqDate.Size = New System.Drawing.Size(114, 18)
        Me.TxtChqDate.TabIndex = 2
        Me.TxtChqDate.Text = "ChqDate"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 16)
        Me.Label2.TabIndex = 673
        Me.Label2.Text = "Cheque Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(258, 132)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 16)
        Me.Label3.TabIndex = 674
        Me.Label3.Text = "Cheque Amount"
        '
        'TxtIssueDate
        '
        Me.TxtIssueDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtIssueDate.AgLastValueTag = Nothing
        Me.TxtIssueDate.AgLastValueText = Nothing
        Me.TxtIssueDate.AgMandatory = True
        Me.TxtIssueDate.AgMasterHelp = False
        Me.TxtIssueDate.AgNumberLeftPlaces = 0
        Me.TxtIssueDate.AgNumberNegetiveAllow = False
        Me.TxtIssueDate.AgNumberRightPlaces = 0
        Me.TxtIssueDate.AgPickFromLastValue = False
        Me.TxtIssueDate.AgRowFilter = ""
        Me.TxtIssueDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIssueDate.AgSelectedValue = Nothing
        Me.TxtIssueDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIssueDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtIssueDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIssueDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIssueDate.Location = New System.Drawing.Point(130, 155)
        Me.TxtIssueDate.Name = "TxtIssueDate"
        Me.TxtIssueDate.Size = New System.Drawing.Size(114, 18)
        Me.TxtIssueDate.TabIndex = 4
        Me.TxtIssueDate.Text = "ChqDate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(111, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 676
        Me.Label4.Text = "Ä"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(112, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 677
        Me.Label5.Text = "Ä"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(112, 139)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 678
        Me.Label6.Text = "Ä"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(355, 139)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 7)
        Me.Label7.TabIndex = 679
        Me.Label7.Text = "Ä"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(22, 181)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 16)
        Me.Label8.TabIndex = 680
        Me.Label8.Text = "Particulars"
        '
        'brnPrint
        '
        Me.brnPrint.BackColor = System.Drawing.Color.White
        Me.brnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.brnPrint.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.brnPrint.Image = Global.Customised.My.Resources.Resources.print_cheque_32
        Me.brnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.brnPrint.Location = New System.Drawing.Point(554, 156)
        Me.brnPrint.Name = "brnPrint"
        Me.brnPrint.Size = New System.Drawing.Size(83, 48)
        Me.brnPrint.TabIndex = 1002
        Me.brnPrint.Text = "&PRINT"
        Me.brnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.brnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.brnPrint.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(111, 163)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 1003
        Me.Label1.Text = "Ä"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(111, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 7)
        Me.Label9.TabIndex = 1004
        Me.Label9.Text = "Ä"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(355, 163)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 7)
        Me.Label10.TabIndex = 1005
        Me.Label10.Text = "Ä"
        '
        'FrmChequeData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1474, 755)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.brnPrint)
        Me.Controls.Add(Me.pnlParent)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtParticulars)
        Me.Controls.Add(Me.TxtIssueDate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtChqDate)
        Me.Controls.Add(Me.lblIssuedDate)
        Me.Controls.Add(Me.txtPayeeName)
        Me.Controls.Add(Me.cmbChequeStatus)
        Me.Controls.Add(Me.lblChequeStatus)
        Me.Controls.Add(Me.TxtVoucherNo)
        Me.Controls.Add(Me.txtChequeNo)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.lblChequeNo)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.LblDescription)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.lblBankName)
        Me.Controls.Add(Me.lblPayeeName)
        Me.MaximizeBox = True
        Me.MinimizeBox = False
        Me.Name = "FrmChequeData"
        Me.Text = "Quality Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.lblPayeeName, 0)
        Me.Controls.SetChildIndex(Me.lblBankName, 0)
        Me.Controls.SetChildIndex(Me.txtAmount, 0)
        Me.Controls.SetChildIndex(Me.LblDescription, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
        Me.Controls.SetChildIndex(Me.TextBox1, 0)
        Me.Controls.SetChildIndex(Me.TextBox2, 0)
        Me.Controls.SetChildIndex(Me.TextBox3, 0)
        Me.Controls.SetChildIndex(Me.lblChequeNo, 0)
        Me.Controls.SetChildIndex(Me.txtBankName, 0)
        Me.Controls.SetChildIndex(Me.txtChequeNo, 0)
        Me.Controls.SetChildIndex(Me.TxtVoucherNo, 0)
        Me.Controls.SetChildIndex(Me.lblChequeStatus, 0)
        Me.Controls.SetChildIndex(Me.cmbChequeStatus, 0)
        Me.Controls.SetChildIndex(Me.txtPayeeName, 0)
        Me.Controls.SetChildIndex(Me.lblIssuedDate, 0)
        Me.Controls.SetChildIndex(Me.TxtChqDate, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtIssueDate, 0)
        Me.Controls.SetChildIndex(Me.txtParticulars, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.pnlParent, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.brnPrint, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnDate.ResumeLayout(False)
        Me.pnDate.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlParent.ResumeLayout(False)
        Me.pnlParent.PerformLayout()
        CType(Me.imgChequePreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents TxtVoucherNo As AgControls.AgTextBox
    Friend WithEvents txtChqD2 As AgControls.AgTextBox
    Friend WithEvents txtChqM1 As AgControls.AgTextBox
    Friend WithEvents lblChequeStatus As Label
    Friend WithEvents txtChequeNo As AgControls.AgTextBox
    Friend WithEvents txtChqM2 As AgControls.AgTextBox
    Friend WithEvents txtChqD1 As AgControls.AgTextBox
    Friend WithEvents txtChqY2 As AgControls.AgTextBox
    Friend WithEvents pnDate As Panel
    Friend WithEvents txtChqY1 As AgControls.AgTextBox
    Friend WithEvents lblChequeNo As Label
    Friend WithEvents txtParticulars As AgControls.AgTextBox
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents Timer1 As Timer
    Private components As System.ComponentModel.IContainer
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents txtPayeeName As AgControls.AgTextBox
    Friend WithEvents txtBankName As AgControls.AgTextBox
    Friend WithEvents lblIssuedDate As Label
    Friend WithEvents cmbChequeStatus As ComboBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ckbamount As CheckBox
    Friend WithEvents ckbPayeename As CheckBox
    Friend WithEvents ckbdate As CheckBox
    Friend WithEvents lblPayeeName As Label
    Friend WithEvents lblBankName As Label
    Friend WithEvents txtAmount As AgControls.AgTextBox
    Friend WithEvents pnlParent As Panel
    Friend WithEvents txtChqName As AgControls.AgTextBox
    Friend WithEvents txtChqAmount As AgControls.AgTextBox
    Friend WithEvents lbChqAmountInWord1 As Label
    Friend WithEvents lbChqAmountInWord2 As Label
    Friend WithEvents imgChequePreview As PictureBox
    Friend WithEvents TextBox3 As AgControls.AgTextBox
    Friend WithEvents TextBox2 As AgControls.AgTextBox
    Friend WithEvents TextBox1 As AgControls.AgTextBox
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtChqDate As AgControls.AgTextBox
    Friend WithEvents TxtIssueDate As AgControls.AgTextBox
    Friend WithEvents Label8 As Label
    Public WithEvents Label7 As Label
    Public WithEvents Label6 As Label
    Public WithEvents Label5 As Label
    Public WithEvents Label4 As Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation

        If txtChqAmount.Text.Trim() = "" Then
            MsgBox("Enter Amount")
            txtChqAmount.Focus()
            Exit Sub
        ElseIf txtChequeNo.Text.Trim() = "" Or txtChequeNo.Text.Length < 6 Then
            MsgBox("Please Enter The Valid Cheque Number")
            txtChequeNo.Focus()
            Exit Sub
        ElseIf db.PrintData.Data("VoucherNo", String.Format("ChequeNo='{0}' and BankName='{1}' and CompanyName='{2}'", txtChequeNo.Text, txtBankName.Text, db.CompanyDetails.IdByCode(CompName))) <> "" Then
            MsgBox("Cheque Number is already used")
            txtChequeNo.Focus()
            Exit Sub
        Else
            If DateDiff(DateInterval.Month, Now, CDate(TxtChqDate.Text)) > 6 Then
                If MsgBox(String.Format("Seems To Be The Date After Six Month!!!{0}Are You Wish To Proceed?", vbCrLf), MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    TxtChqDate.Focus()
                    Exit Sub
                End If
            End If
        End If



        If TxtVoucherNo.Text = "" Then
            TxtVoucherNo.Text = GetMaxVoucherNo()
        End If

        If AgL.RequiredField(TxtVoucherNo, LblDescription.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From ChequeData Where VoucherNo='" & TxtVoucherNo.Text & "' And Div_Code='" & TxtDivision.Tag & "' And Site_Code='" & AgL.PubSiteCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From ChequeData Where VoucherNo='" & TxtVoucherNo.Text & "' And Code <> '" & mInternalCode & "'  And Div_Code='" & TxtDivision.Tag & "' And Site_Code='" & AgL.PubSiteCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where 1=1  "

        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.VoucherNo, I.ChequeDate, I.PayeeName, I.Amount, I.Particulars, I.ChequeNo, U.Description as BankName  
                          FROM ChequeData I 
                          Left Join ChequeUI U On I.ChequeUI = U.Code  "
        AgL.PubFindQryOrdBy = "[VoucherNo]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "ChequeData"
        mQry = "Select Code, Description From ChequeUI order By description"
        txtBankName.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE ChequeData 
                SET
                VoucherNo = " & AgL.Chk_Text(TxtVoucherNo.Text) & ",
                PayeeName = " & AgL.Chk_Text(txtPayeeName.Text) & ",
                Particulars = " & AgL.Chk_Text(txtParticulars.Text) & ",
                Amount = " & Val(txtChqAmount.Text) & ",
                ChequeNo = " & AgL.Chk_Text(txtChequeNo.Text) & ",
                ChequeDate = " & AgL.Chk_Date(TxtChqDate.Text) & ",
                IssueDate = " & AgL.Chk_Date(TxtIssueDate.Text) & ",
                ChequeStatus = " & AgL.Chk_Text(TxtStatus.Text) & ",
                ChequeUI = " & AgL.Chk_Text(txtBankName.Tag) & ",
                DisplayDate = " & Val(ckbdate.Checked) & ",
                DisplayAcPayee = " & Val(cbdAcPayee.Checked) & ",
                DisplayPayeeName = " & Val(ckbPayeename.Checked) & ",
                DisplayAmount = " & Val(ckbamount.Checked) & "
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList

    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, cu.Description as ChequeUiName 
                From ChequeData H 
                Left Join ChequeUI cu on H.ChequeUI = cu.Code
                Where H.Code ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtVoucherNo.Text = AgL.XNull(.Rows(0)("VoucherNo"))
                txtPayeeName.Text = AgL.XNull(.Rows(0)("PayeeName"))
                txtParticulars.Text = AgL.XNull(.Rows(0)("Particulars"))
                txtAmount.Text = AgL.VNull(.Rows(0)("Amount"))
                txtChequeNo.Text = AgL.XNull(.Rows(0)("ChequeNo"))
                TxtChqDate.Text = AgL.XNull(.Rows(0)("ChequeDate"))
                SetChqDate()
                TxtIssueDate.Text = AgL.XNull(.Rows(0)("IssueDate"))
                cmbChequeStatus.Text = AgL.XNull(.Rows(0)("ChequeStatus"))
                txtBankName.Text = AgL.XNull(.Rows(0)("ChequeUiName"))
                txtBankName.Tag = AgL.XNull(.Rows(0)("ChequeUi"))
                SetChequePreview()
                ckbamount.Checked = AgL.VNull(.Rows(0)("DisplayAmount"))
                cbdAcPayee.Checked = AgL.VNull(.Rows(0)("DisplayAcPayee"))
                ckbdate.Checked = AgL.VNull(.Rows(0)("DisplayDate"))
                ckbPayeename.Checked = AgL.VNull(.Rows(0)("DisplayPayeeName"))
            End If
        End With
        cmbChequeStatus.Enabled = True
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        txtBankName.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtVoucherNo.Focus()
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVoucherNo.KeyDown
        Select Case sender.Name
            Case TxtVoucherNo.Name
                If e.KeyCode = Keys.Enter Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
        End Select
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mQry = "Select I.Code As SearchCode " &
                " From ChequeData I " & mConStr &
                " Order By I.VoucherNo "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmChequeData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 300, 885)
        'txtChqName.BackColor = Color.Transparent
        'txtChqAmount.BackColor = Color.Transparent
        lbChqAmountInWord1.BackColor = Color.Transparent
        lbChqAmountInWord2.BackColor = Color.Transparent
        'txtChqD1.BackColor = Color.Transparent
        'txtChqD2.BackColor = Color.Transparent
        'txtChqM1.BackColor = Color.Transparent
        'txtChqM2.BackColor = Color.Transparent
        'txtChqY1.BackColor = Color.Transparent
        'txtChqY2.BackColor = Color.Transparent
    End Sub

    Private Sub TxtManualCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVoucherNo.KeyDown
        Select Case sender.NAME
            Case TxtVoucherNo.Name
                'If e.KeyCode = Keys.Enter Then
                '    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                '        Topctrl1.FButtonClick(13)
                '        e.Handled = True
                '    End If
                'End If
        End Select
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            'mQry = " Select Count(*) From ItemCategory Where ChequeData = '" & mSearchCode & "'"
            'If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
            '    MsgBox(" Data Exists For Item " & TxtDescription.Text & " In Category Master . Can't Delete Entry", MsgBoxStyle.Information)
            '    FGetRelationalData = True
            '    Exit Function
            'End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function

    Private Sub FrmChequeData_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim obj As Object
        For Each obj In Me.Controls
            If TypeOf obj Is TextBox Then
                If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then
                    DirectCast(obj, TextBox).CharacterCasing = CharacterCasing.Upper
                ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
                    DirectCast(obj, TextBox).CharacterCasing = CharacterCasing.Lower
                End If
            End If
        Next

        imgChequePreview.Image = Nothing

    End Sub
    Public Shared Sub ImportChequeDataTable(ChequeDataTable As StructChequeData)
        Dim mQry As String = ""
        If AgL.Dman_Execute("SELECT Count(*) From ChequeData With (NoLock) Where Description = '" & ChequeDataTable.Description & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = " INSERT INTO ChequeData(Code, Description, EntryBy, EntryDate, EntryType, EntryStatus, OMSId)
                    Select '" & ChequeDataTable.Code & "' As ChequeDataCode, " & AgL.Chk_Text(ChequeDataTable.Description) & " As ChequeData, 
                    '" & ChequeDataTable.EntryBy & "' As EntryBy, " & AgL.Chk_Date(ChequeDataTable.EntryDate) & " As EntryDate, 
                    '" & ChequeDataTable.EntryType & "' As EntryType, '" & ChequeDataTable.EntryStatus & "' As EntryStatus, 
                    '" & ChequeDataTable.OMSId & "' As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE ChequeData Set OMSId = '" & ChequeDataTable.OMSId & "' 
                    Where Description = '" & ChequeDataTable.Description & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Public Structure StructChequeData
        Dim Code As String
        Dim Description As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim OMSId As String
    End Structure


    Private Sub BankName_Selected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBankName.Leave
    End Sub

    Public Function GetMaxVoucherNo() As String
        mQry = "Select isnull(Max(CAST(L.VoucherNo AS INTEGER)),0)+1 FROM ChequeData L With (Nolock) WHERE ABS(L.VoucherNo)>0"
        GetMaxVoucherNo = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()
    End Function

    Sub SetChequeNo()
        If txtChequeNo.Text = "" Then
            mQry = "Select isnull(Max(CAST(L.ChequeNo AS INTEGER)),0)+1 FROM ChequeData L With (Nolock) WHERE ABS(L.ChequeNo)>0"
            Dim CNo As Double = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()
            txtChequeNo.Text = String.Format("{0:000000}", CNo)
        End If
    End Sub

    Private Sub txtChqAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqAmount.KeyDown
        Select Case e.KeyCode
            Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9,
                    Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete,
                    Keys.Left, Keys.Right
                If e.Shift = True Then
                    e.SuppressKeyPress = True
                    Exit Sub
                End If
                e.SuppressKeyPress = False
            Case Else
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub txtInpAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChqAmount.TextChanged
        lbChqAmountInWord1.Text = Number_to_Word.ConvertCurrencyToEnglish(txtChqAmount.Text)
        lbChqAmountInWord1.Text = "** " + lbChqAmountInWord1.Text.ToUpper() + " **"
        Dim NoOfChar As Integer = 45
        Dim Str1, Str2 As String
        lbChqAmountInWord2.Text = ""
        If lbChqAmountInWord1.Text.Length > NoOfChar Then
            Str1 = lbChqAmountInWord1.Text.Substring(0, Mid(lbChqAmountInWord1.Text, 1, NoOfChar).LastIndexOf(" "))
            Str2 = lbChqAmountInWord1.Text.Substring(Mid(lbChqAmountInWord1.Text, 1, NoOfChar).LastIndexOf(" "))
            lbChqAmountInWord1.Text = Str1.ToUpper()
            lbChqAmountInWord2.Text = Str2.ToUpper()
        End If
    End Sub

    Private Sub PayeeName_SelectedInd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPayeeName.TextChanged

        Try
            txtChqName.Text = txtPayeeName.Text
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub dgvChequeIssue_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvChequeIssue.CellClick
    '    Try
    '        If dgvChequeIssue.Columns(e.ColumnIndex).Name = "btnVoucher" And e.RowIndex >= 0 Then
    '            Dim VoucherNo As String = dgvChequeIssue.Item("VoucherNo", e.RowIndex).Value
    '            Dim PayFor As String = dgvChequeIssue.Item("Particulars", e.RowIndex).Value
    '            If PayFor.Trim() = "" Then
    '                MsgBox("Please Enter the Pay For Details...")

    '                dgvChequeIssue.Item("Particulars", e.RowIndex).Selected = True
    '                Exit Sub
    '            End If

    '            Dim ChequeStatus As String = dgvChequeIssue.Item("ChequeStatus", e.RowIndex).Value
    '            Dim Particulars As String = dgvChequeIssue.Item("Particulars", e.RowIndex).Value
    '            Dim Qry As String = String.Format("update printdata set ChequeStatus = '{0}',Particulars='{1}' where VoucherNo='{2}'", ChequeStatus, Particulars.Replace("'", "''"), VoucherNo)
    '            db.ExecuteQuery(Qry)

    '            Printvoucher(VoucherNo)
    '        End If
    '        'If dgvChequeIssue.Columns(e.ColumnIndex).Name = "ChequeStatus" And e.RowIndex >= 0 Then
    '        '    Dim VoucherNo As String = dgvChequeIssue.Item("VoucherNo", e.RowIndex).Value
    '        '    Dim ChequeStatus As String = dgvChequeIssue.Item("ChequeStatus", e.RowIndex).Value
    '        '    Dim Qry As String = String.Format("update printdata set ChequeStatus = '{0}' where VoucherNo='{1}'", ChequeStatus, VoucherNo)
    '        '    db.ExecuteQuery(Qry)
    '        'End If
    '    Catch ex As Exception

    '    End Try

    'End Sub


    Sub Printvoucher(ByVal VoucherNo As String)
        PrintDialog1.AllowSomePages = True
        If PrintDialog1.ShowDialog = DialogResult.OK Then

        End If
        Dim ds As New DataSet
        Dim DTPrint As New DataTable()
        Dim DTCompany As New DataTable()

        'DTPrint = db.PrintData.Grid("*", String.Format("VoucherNo='{0}' and CompanyName = '{1}'", VoucherNo, db.CompanyDetails.IdByCode(CompName))).ToTable("PrintData")
        Dim dv As New DataView()
        'dv = db.ExecuteQuery(String.Format("SELECT 'OFFICE COPY' AS SNO, VoucherNo, PayeeName, Amount, ChequeDate, BankName, CompanyName, IssueDate, ChequeNo, ChequeStatus, DisplayDate, DisplayPayeename, Displayamount, VoucherNewNo, Particulars FROM            PrintData where VoucherNo='{0}' and CompanyName = '{1}' UNION SELECT        'CUSTOMER COPY' AS SNO, VoucherNo, PayeeName, Amount, ChequeDate, BankName, CompanyName, IssueDate, ChequeNo, ChequeStatus, DisplayDate, DisplayPayeename, Displayamount, VoucherNewNo, Particulars FROM            PrintData AS PrintData_1 where VoucherNo='{0}' and CompanyName = '{1}'", VoucherNo, db.CompanyDetails.IdByCode(CompName)))
        dv = db.ExecuteQuery(String.Format("SELECT 'OFFICE COPY' AS SNO, VoucherNo, PayeeName, Amount, ChequeDate, BankName, CompanyName, IssueDate, ChequeNo, ChequeStatus, DisplayDate, DisplayPayeename, Displayamount, VoucherNewNo, Particulars FROM            PrintData where VoucherNo='{0}' and CompanyName = '{1}' ", VoucherNo, db.CompanyDetails.IdByCode(CompName)))
        DTPrint = dv.ToTable("PrintData")

        DTCompany = db.CompanyDetails.Grid("*", String.Format("CompanyName='{0}'", CompName)).ToTable("CompanyDetails")
        ds.Clear()

        ds.Tables.Add(DTPrint)
        ds.Tables.Add(DTCompany)



        ReportPrint("cryVoucher.rpt", ds, Nothing, PrintDialog1.PrinterSettings.PrinterName)
    End Sub

    Private Sub txtBankName_Validating(sender As Object, e As CancelEventArgs) Handles txtBankName.Validating
        Try
            SetChequePreview()

            If TxtChqDate.Text = "" Then TxtChqDate.Text = AgL.PubLoginDate
            If TxtIssueDate.Text = "" Then TxtIssueDate.Text = AgL.PubLoginDate
            SetChequeNo()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub SetChequePreview()
        Dim dtTemp As DataTable
        'txtChqAmount.Text = ""
        'lbChqAmountInWord1.Text = ""
        'lbChqAmountInWord2.Text = ""
        'txtChqName.Text = ""

        'txtChqD1.Text = ""
        'txtChqD2.Text = ""
        'txtChqM1.Text = ""
        'txtChqM2.Text = ""
        'txtChqY1.Text = ""
        'txtChqY2.Text = ""

        dtTemp = AgL.FillData("Select * From ChequeUI Where Code = '" & txtBankName.Tag & "'", AgL.GCn).Tables(0)


        If dtTemp.Rows.Count > 0 Then

            'Dim img_buffer() As Byte
            'img_buffer = CType(dtTemp.Rows(0)("BankImg"), Byte())
            'Dim img_stream As New IO.MemoryStream(img_buffer, True)
            'img_stream.Write(img_buffer, 0, img_buffer.Length)
            'imgChequePreview.Image = New Bitmap(img_stream)
            'imgChequePreview.SizeMode = PictureBoxSizeMode.StretchImage
            'imgChequePreview.Width = UnitConversion((imgChequePreview.Image.Width / imgChequePreview.Image.HorizontalResolution), Units.Inch, Units.Pixel)
            'imgChequePreview.Height = UnitConversion((imgChequePreview.Image.Height / imgChequePreview.Image.VerticalResolution), Units.Inch, Units.Pixel)

            'imgChequePreview.Width = ((Val(dtTemp.Rows(0)("chqWidth")) / oneInch2MM) * 96) * (96 / 72)
            'imgChequePreview.Height = ((Val(dtTemp.Rows(0)("chqHeight")) / oneInch2MM) * 96) * (96 / 72)


            'img_stream.Close()
        End If


        'Dim dv As New DataView
        'dv = db.Bank.Grid("RPTNameLeft,RPTNameTop,RPTAmountLeft,RPTAmountTop,RPTDateLeft,RPTDateTop,RPTWordsLeft,RPTWordsTop", String.Format("BankName='{0}'", txtBankName.Text))


        LblAcPayee.Left = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTAcPayeeLeft").ToString(), Units.MilliMeter, Units.Pixel)))
        LblAcPayee.Top = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTAcPayeeTop").ToString(), Units.MilliMeter, Units.Pixel)))

        txtChqName.Left = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTNameLeft").ToString(), Units.MilliMeter, Units.Pixel)))
        txtChqName.Top = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTNameTop").ToString(), Units.MilliMeter, Units.Pixel)))
        txtChqAmount.Left = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTAmountLeft").ToString(), Units.MilliMeter, Units.Pixel)))
        txtChqAmount.Top = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTAmountTop").ToString(), Units.MilliMeter, Units.Pixel)))
        pnDate.Left = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTDateLeft").ToString(), Units.MilliMeter, Units.Pixel)))
        pnDate.Top = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTDateTop").ToString(), Units.MilliMeter, Units.Pixel)))

        lbChqAmountInWord1.Left = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTWordsLeft").ToString(), Units.MilliMeter, Units.Pixel)))
        lbChqAmountInWord1.Top = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTWordsTop").ToString(), Units.MilliMeter, Units.Pixel)))

        lbChqAmountInWord2.Left = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTWordsLeft").ToString(), Units.MilliMeter, Units.Pixel))) - 30
        lbChqAmountInWord2.Top = Math.Round(Val(UnitConversion(dtTemp.Rows(0)("RPTWordsTop").ToString(), Units.MilliMeter, Units.Pixel))) + 30

        LblAcPayee.Visible = True
        txtChqName.Visible = True
        txtChqAmount.Visible = True
        lbChqAmountInWord1.Visible = True

        txtChqD1.Visible = True
        txtChqD2.Visible = True
        txtChqM1.Visible = True
        txtChqM2.Visible = True
        txtChqY1.Visible = True
        txtChqY2.Visible = True
        TxtChqY3.Visible = True
        TxtChqY4.Visible = True
    End Sub

    Private Sub TxtChqDate_Validating(sender As Object, e As CancelEventArgs) Handles TxtChqDate.Validating
        SetChqDate()
    End Sub

    Private Sub SetChqDate()
        Dim mChqDate As Date
        mChqDate = CDate(TxtChqDate.Text)
        txtChqD1.Text = mChqDate.Day \ 10
        txtChqD2.Text = Val(mChqDate.Day) Mod 10
        txtChqM1.Text = mChqDate.Month \ 10
        txtChqM2.Text = Val(mChqDate.Month) Mod 10
        'txtChqY1.Text = (mChqDate.Year Mod 100) \ 10
        'txtChqY2.Text = Val(mChqDate.Year) Mod 10
        txtChqY1.Text = mChqDate.Year.ToString.Substring(0, 1)
        txtChqY2.Text = mChqDate.Year.ToString.Substring(1, 1)
        TxtChqY3.Text = mChqDate.Year.ToString.Substring(2, 1)
        TxtChqY4.Text = mChqDate.Year.ToString.Substring(3, 1)
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        txtChqAmount.Text = sender.Text
    End Sub

    Private Sub brnPrint_Click(sender As Object, e As EventArgs)
        'txtBayerName.Text = txtChqName.Text
        'txtAmount.Text = txtChqAmount.Text
        'txtInputWords.Text = lbChqAmountInWord1.Text

        If txtChqAmount.Text.Trim() = "" Then
            MsgBox("Enter Amount")
            txtChqAmount.Focus()
        ElseIf txtChequeNo.Text.Trim() = "" Or txtChequeNo.Text.Length < 6 Then
            MsgBox("Please Enter The Valid Cheque Number")
            txtChequeNo.Focus()
        ElseIf db.PrintData.Data("VoucherNo", String.Format("ChequeNo='{0}' and BankName='{1}' and CompanyName='{2}'", txtChequeNo.Text, txtBankName.Text, db.CompanyDetails.IdByCode(CompName))) <> "" Then
            MsgBox("Cheque Number is already used")
            txtChequeNo.Focus()
        Else

            If DateDiff(DateInterval.Month, Now, CDate(TxtChqDate.Text)) > 6 Then
                If MsgBox(String.Format("Seems To Be The Date After Six Month!!!{0}Are You Wish To Proceed?", vbCrLf), MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    TxtChqDate.Focus()
                    Exit Sub
                End If
            End If

            PrintCheque()
            'If MsgDialog.ShowMsgDlg("Do you Save this Record?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then
            '    db.PrintData.SaveRecord()
            'End If
            'NewForm()
        End If
        'lbChqAmountInWord1.Text = ""

    End Sub

    Sub PrintCheque()
        Dim dtChequeUi As DataTable
        PrintDialog1.AllowSomePages = True
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings

        If PrintDialog1.ShowDialog = DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings

            'Dim dv As New DataView
            'dv = db.Bank.Grid("LeftMargin,TopMargin,ChqWidth,ChqHeight", String.Format("BankName='{0}'", txtBankName.Text))
            mQry = "Select * From ChequeUI Where Code = '" & txtBankName.Tag & "'"
            dtChequeUi = AgL.FillData(mQry, AgL.GCn).Tables(0)

            LastBankPrinted = txtBankName.Text
            Dim PageSetup As New Printing.PageSettings
            With PageSetup
                .Margins.Left = Val(UnitConversion(dtChequeUi.Rows(0)("LeftMargin").ToString(), Units.MilliMeter, Units.Pixel))
                .Margins.Top = Val(UnitConversion(dtChequeUi.Rows(0)("TopMargin").ToString(), Units.MilliMeter, Units.Pixel))
                .Margins.Right = 0
                .Margins.Bottom = 0
            End With
            Dim xDPI, yDPI As Integer

            PrintDocument1.DefaultPageSettings = PageSetup
            PrintDocument1.DefaultPageSettings.PaperSize = New PaperSize("ChequeSize", Val(UnitConversion(dtChequeUi.Rows(0)("ChqWidth").ToString(), Units.MilliMeter, Units.Pixel)), Val(UnitConversion(dtChequeUi.Rows(0)("ChqHeight").ToString(), Units.MilliMeter, Units.Pixel)))
            'PrintDocument1.DefaultPageSettings.PaperSize = New PaperSize("ChequeSize", Val(UnitConversion("210", Units.MilliMeter, Units.Pixel)), Val(UnitConversion("297", Units.MilliMeter, Units.Pixel)))
            PrintDocument1.DefaultPageSettings.Landscape = False
            PrintPreviewDialog1.Document = PrintDocument1
            'PrintPreviewDialog1.ShowDialog()
            PrintDocument1.Print()
        End If

    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim dtChequeUi As DataTable
        Dim PrintPage As System.Drawing.Graphics = e.Graphics
        Dim fnt As New Font("Arial", 12, FontStyle.Regular)
        Dim PrintBrush As System.Drawing.Brush
        PrintBrush = Brushes.DarkRed

        'Dim dv As New DataView
        'dv = db.Bank.Grid("ChqWidth,ChqHeight,RPTNameLeft,RPTNameTop,RPTAmountLeft,RPTAmountTop,RPTDateLeft,RPTDateTop,RPTWordsLeft,RPTWordsTop", String.Format("BankName='{0}'", txtBankName.Text))
        mQry = "Select * From ChequeUI Where Code = '" & txtBankName.Tag & "'"
        dtChequeUi = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim dd1Left, dd1Top, dd2Left, dd2Top, mm1Left, mm1Top, mm2Left, mm2Top, yy1Left, yy1Top, yy2Left, yy2Top, yy3Left, yy3Top, yy4Left, yy4Top As Integer
        Dim ddSpace As Integer = 30



        dd1Left = Val(UnitConversion(dtChequeUi.Rows(0)("RPTDateLeft").ToString(), Units.MilliMeter, Units.Pixel))
        dd2Left = dd1Left + (ddSpace * 1) * 0.65
        mm1Left = dd1Left + (ddSpace * 2) * 0.65
        mm2Left = dd1Left + (ddSpace * 3) * 0.65
        yy1Left = dd1Left + (ddSpace * 4) * 0.65
        yy2Left = dd1Left + (ddSpace * 5) * 0.65
        yy3Left = dd1Left + (ddSpace * 6) * 0.65
        yy4Left = dd1Left + (ddSpace * 7) * 0.65


        dd1Top = Val(UnitConversion(dtChequeUi.Rows(0)("RPTDateTop").ToString(), Units.MilliMeter, Units.Pixel))
        dd2Top = dd1Top
        mm1Top = dd1Top
        mm2Top = dd1Top
        yy1Top = dd1Top
        yy2Top = dd1Top
        yy3Top = dd1Top
        yy4Top = dd1Top


        Dim nameLeft, nameTop, AmountLeft, AmountTop, AmountInWordLeft, AmountInWordTop, AcPayeeLeft, AcPayeeTop As Integer

        nameLeft = Val(UnitConversion(dtChequeUi.Rows(0)("RPTNameLeft").ToString(), Units.MilliMeter, Units.Pixel))
        nameTop = Val(UnitConversion(dtChequeUi.Rows(0)("RPTNameTop").ToString(), Units.MilliMeter, Units.Pixel))

        AmountLeft = Val(UnitConversion(dtChequeUi.Rows(0)("RPTAmountLeft").ToString(), Units.MilliMeter, Units.Pixel))
        AmountTop = Val(UnitConversion(dtChequeUi.Rows(0)("RPTAmountTop").ToString(), Units.MilliMeter, Units.Pixel))

        AcPayeeLeft = Val(UnitConversion(dtChequeUi.Rows(0)("RPTAcPayeeLeft").ToString(), Units.MilliMeter, Units.Pixel))
        AcPayeeTop = Val(UnitConversion(dtChequeUi.Rows(0)("RPTAcPayeeTop").ToString(), Units.MilliMeter, Units.Pixel))

        AmountInWordLeft = Val(UnitConversion(dtChequeUi.Rows(0)("RPTWordsLeft").ToString(), Units.MilliMeter, Units.Pixel))
        AmountInWordTop = Val(UnitConversion(dtChequeUi.Rows(0)("RPTWordsTop").ToString(), Units.MilliMeter, Units.Pixel))


        With PrintPage
            'If Not ckbIsRotate90Degree.Checked Then
            .RotateTransform(90)
            .TranslateTransform(-10, -580)
            'End If
            '.DrawRectangle(Pens.Red, 0, 0, CInt(Val(UnitConversion(dtChequeUI.Rows(0)("ChqWidth").ToString(), Units.MilliMeter, Units.Pixel))), CInt(Val(UnitConversion(dtChequeUI.Rows(0)("ChqHeight").ToString(), Units.MilliMeter, Units.Pixel))))
            If ckbdate.Checked = True Then
                .DrawString(txtChqD1.Text, txtChqD1.Font, New System.Drawing.SolidBrush(txtChqD1.ForeColor), dd1Left, dd1Top)
                .DrawString(txtChqD2.Text, txtChqD2.Font, New System.Drawing.SolidBrush(txtChqD2.ForeColor), dd2Left, dd2Top)
                .DrawString(txtChqM1.Text, txtChqM1.Font, New System.Drawing.SolidBrush(txtChqM1.ForeColor), mm1Left, mm1Top)
                .DrawString(txtChqM2.Text, txtChqM2.Font, New System.Drawing.SolidBrush(txtChqM2.ForeColor), mm2Left, mm2Top)
                .DrawString(txtChqY1.Text, txtChqY1.Font, New System.Drawing.SolidBrush(txtChqY1.ForeColor), yy1Left, yy1Top)
                .DrawString(txtChqY2.Text, txtChqY2.Font, New System.Drawing.SolidBrush(txtChqY2.ForeColor), yy2Left, yy2Top)
                .DrawString(TxtChqY3.Text, TxtChqY3.Font, New System.Drawing.SolidBrush(TxtChqY3.ForeColor), yy3Left, yy3Top)
                .DrawString(TxtChqY4.Text, TxtChqY4.Font, New System.Drawing.SolidBrush(TxtChqY4.ForeColor), yy4Left, yy4Top)
            End If

            If ckbamount.Checked = True Then
                .DrawString(lbChqAmountInWord1.Text, lbChqAmountInWord1.Font, New System.Drawing.SolidBrush(lbChqAmountInWord1.ForeColor), AmountInWordLeft, AmountInWordTop)
                .DrawString(lbChqAmountInWord2.Text, lbChqAmountInWord2.Font, New System.Drawing.SolidBrush(lbChqAmountInWord2.ForeColor), AmountInWordLeft, AmountInWordTop + 30)

                .DrawString("** " + txtChqAmount.Text + " **", txtChqAmount.Font, New System.Drawing.SolidBrush(txtChqAmount.ForeColor), AmountLeft, AmountTop)
            End If

            If ckbPayeename.Checked = True Then
                .DrawString(txtChqName.Text, txtChqName.Font, New System.Drawing.SolidBrush(txtChqName.ForeColor), nameLeft, nameTop)
            End If

            If cbdAcPayee.Checked = True Then
                .DrawString(LblAcPayee.Text, LblAcPayee.Font, New System.Drawing.SolidBrush(LblAcPayee.ForeColor), AcPayeeLeft, AcPayeeTop)
            End If

        End With
    End Sub



    Private Sub ckbdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbdate.CheckedChanged

        If ckbdate.Checked = True Then
            txtChqD1.ForeColor = Color.Black
            txtChqD2.ForeColor = Color.Black
            txtChqM1.ForeColor = Color.Black
            txtChqM2.ForeColor = Color.Black
            txtChqY1.ForeColor = Color.Black
            txtChqY2.ForeColor = Color.Black
            TxtChqY3.ForeColor = Color.Black
            TxtChqY4.ForeColor = Color.Black
        Else
            txtChqD1.ForeColor = Color.Red
            txtChqD2.ForeColor = Color.Red
            txtChqM1.ForeColor = Color.Red
            txtChqM2.ForeColor = Color.Red
            txtChqY1.ForeColor = Color.Red
            txtChqY2.ForeColor = Color.Red
            TxtChqY3.ForeColor = Color.Red
            TxtChqY4.ForeColor = Color.Red

        End If
    End Sub

    Private Sub ckbPayeename_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbPayeename.CheckedChanged

        If ckbPayeename.Checked = True Then
            txtChqName.ForeColor = Color.Black
        Else
            txtChqName.ForeColor = Color.Red
        End If
    End Sub

    Private Sub cbdAcPayee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbdAcPayee.CheckedChanged

        If cbdAcPayee.Checked = True Then
            LblAcPayee.ForeColor = Color.Black
        Else
            LblAcPayee.ForeColor = Color.Red
        End If
    End Sub

    Private Sub ckbamount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbamount.CheckedChanged



        If ckbamount.Checked = True Then
            txtChqAmount.ForeColor = Color.Black
            lbChqAmountInWord1.ForeColor = Color.Black
            lbChqAmountInWord2.ForeColor = Color.Black
        Else
            txtChqAmount.ForeColor = Color.Red
            lbChqAmountInWord1.ForeColor = Color.Red
            lbChqAmountInWord2.ForeColor = Color.Red
        End If
        txtChqAmount.Text = String.Format("{0}", txtChqAmount.Text)
    End Sub



    Private Sub FrmLedgerHead_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
    End Sub

    Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        FGetPrintCrystal(SearchCode, mPrintFor, IsPrintToPrinter, BulkCondStr)
    End Sub

    Sub FGetPrintCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer



        'mPrintTitle = AgL.Dman_Execute("Select IfNull(PrintingDescription, Description) From Voucher_Type Where V_Type = '" & TxtV_Type.Tag & "' ", AgL.GCn).ExecuteScalar()
        mPrintTitle = "Receipt Voucher"

        Dim mDocNoCaption As String = "Voucher No" 'FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        Dim mDocDateCaption As String = "Voucher Date" 'FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        Dim mDocReportFileName As String = "ChequeVoucher.rpt" 'FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)


        Dim bPrimaryQry As String = ""
        If BulkCondStr <> "" Then
            bPrimaryQry = " Select * From LedgerHead  With (NoLock) Where DocID In (" & BulkCondStr & ")"
            PrintingCopies = FGetSettings(SettingFields.PrintingBulkCopyCaptions, SettingType.General).ToString.Split(",")
        Else
            bPrimaryQry = " Select * From LedgerHead  With (NoLock) Where DocID = '" & SearchCode & "'"
            PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        End If


        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            mQry = mQry + "
                Select '1' as DocID, '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, 
                '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, 
                SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, 
                H.Code, H.VoucherNo, H.PayeeName, H.Amount, H.IssueDate, H.ChequeDate, H.ChequeNo, H.Particulars, 
                CUI.Description as BankName,                
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle,
                '" & FGetSettings(SettingFields.DocumentPrintShowPrintDateTimeYn, SettingType.General) & "' as DocumentPrintShowPrintDateTimeYn                
                from ChequeData as H                
                Left Join ChequeUI CUI On H.ChequeUI= CUI.Code
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                Where H.Code = '" & SearchCode & "' "
        Next
        mQry = mQry + " Order By Copies, H.VoucherNo "


        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            'objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.Party = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            'objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.Agent = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            '            FGetMailConfiguration(objRepPrint, SearchCode)
            'objRepPrint.AttachmentName = "Invoice"
        Else
            objRepPrint = New AgLibrary.RepView(AgL)
        End If


        'If mDocReportFileName = "" Then
        ClsMain.FPrintThisDocument(Me, objRepPrint, "", mQry, "ChequeVoucher.rpt", mPrintTitle, , , , "", TxtIssueDate.Text, IsPrintToPrinter)
        'Else
        'ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, mDocReportFileName, mPrintTitle, , , , TxtPartyName.Tag, TxtV_Date.Text, IsPrintToPrinter)
        'End If
    End Sub

    Private Sub brnPrint_Click_1(sender As Object, e As EventArgs) Handles brnPrint.Click
        PrintCheque()
    End Sub

    Private Sub cmbChequeStatus_ValueMemberChanged(sender As Object, e As EventArgs) Handles cmbChequeStatus.ValueMemberChanged
    End Sub






    Private Sub FrmChequeData_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        cmbChequeStatus.Text = "Draft Cheque"
    End Sub

    Private Sub cmbChequeStatus_TextChanged(sender As Object, e As EventArgs) Handles cmbChequeStatus.TextChanged
        If Topctrl1.Mode.ToUpper = "BROWSE" Then
            mQry = "Update Chequedata set ChequeStatus='" & cmbChequeStatus.Text & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
    End Sub
End Class
