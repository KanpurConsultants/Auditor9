Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmChequeUI

    Inherits AgTemplate.TempMaster

    Dim mQry$

    Dim oneMM2Pixel As Double = 3.779528
    Dim onePixel2Point As Double = 0.75

    Dim IsDrag As Boolean = False
    Dim DateX, DateY As Integer
    Friend WithEvents LblDragAcPayee As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents TxtAcPayeeLeft As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents TxtAcPayeeTop As TextBox
    Friend WithEvents TxtInpAcPayeeTop As TextBox
    Friend WithEvents TxtInpAcPayeeLeft As TextBox
    Friend WithEvents btnSave As Button


#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnSearch1 = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.lblInWordsLeft = New System.Windows.Forms.Label()
        Me.txtInWordLeft = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblInWordsTop = New System.Windows.Forms.Label()
        Me.GPAmount = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblAmountLeft = New System.Windows.Forms.Label()
        Me.txtAmtLeft = New System.Windows.Forms.TextBox()
        Me.lblAmountTop = New System.Windows.Forms.Label()
        Me.txtAmtTop = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTop = New System.Windows.Forms.Label()
        Me.txtTop = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblLeft = New System.Windows.Forms.Label()
        Me.txtLeft = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtInWordTop = New System.Windows.Forms.TextBox()
        Me.GPAmountInWords = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.pnlForm = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtInpWordsLeft = New System.Windows.Forms.TextBox()
        Me.txtFromTop = New System.Windows.Forms.TextBox()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.txtInpNameLeft = New System.Windows.Forms.TextBox()
        Me.txtFromLeft = New System.Windows.Forms.TextBox()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.txtInpAmountTop = New System.Windows.Forms.TextBox()
        Me.txtInpAmountLeft = New System.Windows.Forms.TextBox()
        Me.txtInpNameTop = New System.Windows.Forms.TextBox()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.txtSearchbox = New System.Windows.Forms.TextBox()
        Me.txtPnlDateLeft = New System.Windows.Forms.TextBox()
        Me.txtPnlDateTop = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TxtAcPayeeLeft = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TxtAcPayeeTop = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LblDragAcPayee = New System.Windows.Forms.Label()
        Me.lblDragAmount = New System.Windows.Forms.Label()
        Me.lblDragAmountWord = New System.Windows.Forms.Label()
        Me.lblDragName = New System.Windows.Forms.Label()
        Me.lblDragDate = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.panelVertical = New System.Windows.Forms.Panel()
        Me.RulerControl2 = New Lyquidity.UtilityLibrary.Controls.RulerControl()
        Me.imgChequePreview = New System.Windows.Forms.PictureBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.panelHorizantal = New System.Windows.Forms.Panel()
        Me.RulerControl1 = New Lyquidity.UtilityLibrary.Controls.RulerControl()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.GPChequeSize = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblWidth = New System.Windows.Forms.Label()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.lblHeight = New System.Windows.Forms.Label()
        Me.txtHeight = New System.Windows.Forms.TextBox()
        Me.GPDate = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDateLeft = New System.Windows.Forms.Label()
        Me.txtDateLeft = New System.Windows.Forms.TextBox()
        Me.lblDateTop = New System.Windows.Forms.Label()
        Me.txtDateTop = New System.Windows.Forms.TextBox()
        Me.GPName = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblPayeeNameLeft = New System.Windows.Forms.Label()
        Me.txtNameLeft = New System.Windows.Forms.TextBox()
        Me.lblNameTop = New System.Windows.Forms.Label()
        Me.txtNameTop = New System.Windows.Forms.TextBox()
        Me.txtInpWordsTop = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TxtInpAcPayeeLeft = New System.Windows.Forms.TextBox()
        Me.TxtInpAcPayeeTop = New System.Windows.Forms.TextBox()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPAmount.SuspendLayout()
        Me.GPAmountInWords.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlForm.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.imgChequePreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.GPChequeSize.SuspendLayout()
        Me.GPDate.SuspendLayout()
        Me.GPName.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(1574, 41)
        Me.Topctrl1.TabIndex = 3
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 605)
        Me.GroupBox1.Size = New System.Drawing.Size(1616, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 609)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 672)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(221, 609)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 609)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 609)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(457, 609)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(221, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "Ä"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgAllowUserToEnableMasterHelp = False
        Me.TxtDescription.AgLastValueTag = Nothing
        Me.TxtDescription.AgLastValueText = Nothing
        Me.TxtDescription.AgMandatory = True
        Me.TxtDescription.AgMasterHelp = False
        Me.TxtDescription.AgNumberLeftPlaces = 0
        Me.TxtDescription.AgNumberNegetiveAllow = False
        Me.TxtDescription.AgNumberRightPlaces = 0
        Me.TxtDescription.AgPickFromLastValue = False
        Me.TxtDescription.AgRowFilter = ""
        Me.TxtDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDescription.AgSelectedValue = Nothing
        Me.TxtDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(166, 23)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(309, 22)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(99, 69)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Description"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(105, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 16)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "MM"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(105, 27)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(30, 16)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "MM"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.Customised.My.Resources.Resources.exit_32
        Me.btnExit.Location = New System.Drawing.Point(1149, 330)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(99, 49)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = " E&XIT"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        Me.btnExit.Visible = False
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.White
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBrowse.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = Global.Customised.My.Resources.Resources.browse_32
        Me.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBrowse.Location = New System.Drawing.Point(498, 10)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(105, 49)
        Me.btnBrowse.TabIndex = 22
        Me.btnBrowse.Text = "BROWSE CHEQUE IMAGE"
        Me.btnBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'btnSearch1
        '
        Me.btnSearch1.BackColor = System.Drawing.Color.White
        Me.btnSearch1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch1.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch1.Image = Global.Customised.My.Resources.Resources.search_32
        Me.btnSearch1.Location = New System.Drawing.Point(1149, 387)
        Me.btnSearch1.Name = "btnSearch1"
        Me.btnSearch1.Size = New System.Drawing.Size(99, 49)
        Me.btnSearch1.TabIndex = 5
        Me.btnSearch1.Text = "SEARC&H"
        Me.btnSearch1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch1.UseVisualStyleBackColor = False
        Me.btnSearch1.Visible = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.White
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.Customised.My.Resources.Resources.new_32
        Me.btnNew.Location = New System.Drawing.Point(1149, 110)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(99, 49)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "&NEW"
        Me.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNew.UseVisualStyleBackColor = False
        Me.btnNew.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.White
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.Customised.My.Resources.Resources.delete_32
        Me.btnDelete.Location = New System.Drawing.Point(1149, 220)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(99, 49)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "&DELETE"
        Me.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnView
        '
        Me.btnView.BackColor = System.Drawing.Color.White
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnView.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold)
        Me.btnView.Image = Global.Customised.My.Resources.Resources.report_32
        Me.btnView.Location = New System.Drawing.Point(1149, 275)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(99, 49)
        Me.btnView.TabIndex = 3
        Me.btnView.Text = " &VIEW"
        Me.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnView.UseVisualStyleBackColor = False
        Me.btnView.Visible = False
        '
        'lblInWordsLeft
        '
        Me.lblInWordsLeft.AutoSize = True
        Me.lblInWordsLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblInWordsLeft.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInWordsLeft.ForeColor = System.Drawing.Color.Black
        Me.lblInWordsLeft.Location = New System.Drawing.Point(16, 27)
        Me.lblInWordsLeft.Name = "lblInWordsLeft"
        Me.lblInWordsLeft.Size = New System.Drawing.Size(32, 16)
        Me.lblInWordsLeft.TabIndex = 0
        Me.lblInWordsLeft.Text = "Left"
        '
        'txtInWordLeft
        '
        Me.txtInWordLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInWordLeft.Location = New System.Drawing.Point(56, 24)
        Me.txtInWordLeft.MaxLength = 50
        Me.txtInWordLeft.Name = "txtInWordLeft"
        Me.txtInWordLeft.Size = New System.Drawing.Size(49, 22)
        Me.txtInWordLeft.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = Global.Customised.My.Resources.Resources.save_32
        Me.btnSave.Location = New System.Drawing.Point(1149, 165)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 49)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&SAVE"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = False
        Me.btnSave.Visible = False
        '
        'lblInWordsTop
        '
        Me.lblInWordsTop.AutoSize = True
        Me.lblInWordsTop.BackColor = System.Drawing.Color.Transparent
        Me.lblInWordsTop.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInWordsTop.ForeColor = System.Drawing.Color.Black
        Me.lblInWordsTop.Location = New System.Drawing.Point(16, 57)
        Me.lblInWordsTop.Name = "lblInWordsTop"
        Me.lblInWordsTop.Size = New System.Drawing.Size(31, 16)
        Me.lblInWordsTop.TabIndex = 3
        Me.lblInWordsTop.Text = "Top"
        '
        'GPAmount
        '
        Me.GPAmount.Controls.Add(Me.Label7)
        Me.GPAmount.Controls.Add(Me.Label8)
        Me.GPAmount.Controls.Add(Me.lblAmountLeft)
        Me.GPAmount.Controls.Add(Me.txtAmtLeft)
        Me.GPAmount.Controls.Add(Me.lblAmountTop)
        Me.GPAmount.Controls.Add(Me.txtAmtTop)
        Me.GPAmount.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPAmount.Location = New System.Drawing.Point(611, 465)
        Me.GPAmount.Name = "GPAmount"
        Me.GPAmount.Size = New System.Drawing.Size(145, 87)
        Me.GPAmount.TabIndex = 98
        Me.GPAmount.TabStop = False
        Me.GPAmount.Text = "Amount Position"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(105, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 16)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "MM"
        '
        'lblAmountLeft
        '
        Me.lblAmountLeft.AutoSize = True
        Me.lblAmountLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblAmountLeft.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountLeft.ForeColor = System.Drawing.Color.Black
        Me.lblAmountLeft.Location = New System.Drawing.Point(16, 27)
        Me.lblAmountLeft.Name = "lblAmountLeft"
        Me.lblAmountLeft.Size = New System.Drawing.Size(32, 16)
        Me.lblAmountLeft.TabIndex = 0
        Me.lblAmountLeft.Text = "Left"
        '
        'txtAmtLeft
        '
        Me.txtAmtLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmtLeft.Location = New System.Drawing.Point(56, 24)
        Me.txtAmtLeft.MaxLength = 50
        Me.txtAmtLeft.Name = "txtAmtLeft"
        Me.txtAmtLeft.Size = New System.Drawing.Size(49, 22)
        Me.txtAmtLeft.TabIndex = 1
        '
        'lblAmountTop
        '
        Me.lblAmountTop.AutoSize = True
        Me.lblAmountTop.BackColor = System.Drawing.Color.Transparent
        Me.lblAmountTop.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountTop.ForeColor = System.Drawing.Color.Black
        Me.lblAmountTop.Location = New System.Drawing.Point(16, 57)
        Me.lblAmountTop.Name = "lblAmountTop"
        Me.lblAmountTop.Size = New System.Drawing.Size(31, 16)
        Me.lblAmountTop.TabIndex = 3
        Me.lblAmountTop.Text = "Top"
        '
        'txtAmtTop
        '
        Me.txtAmtTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmtTop.Location = New System.Drawing.Point(56, 54)
        Me.txtAmtTop.MaxLength = 50
        Me.txtAmtTop.Name = "txtAmtTop"
        Me.txtAmtTop.Size = New System.Drawing.Size(49, 22)
        Me.txtAmtTop.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(263, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 16)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "MM"
        '
        'lblTop
        '
        Me.lblTop.AutoSize = True
        Me.lblTop.BackColor = System.Drawing.Color.Transparent
        Me.lblTop.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTop.ForeColor = System.Drawing.Color.Black
        Me.lblTop.Location = New System.Drawing.Point(170, 57)
        Me.lblTop.Name = "lblTop"
        Me.lblTop.Size = New System.Drawing.Size(31, 16)
        Me.lblTop.TabIndex = 9
        Me.lblTop.Text = "Top"
        '
        'txtTop
        '
        Me.txtTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTop.Location = New System.Drawing.Point(208, 54)
        Me.txtTop.MaxLength = 50
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(49, 22)
        Me.txtTop.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(263, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "MM"
        '
        'lblLeft
        '
        Me.lblLeft.AutoSize = True
        Me.lblLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblLeft.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeft.ForeColor = System.Drawing.Color.Black
        Me.lblLeft.Location = New System.Drawing.Point(170, 27)
        Me.lblLeft.Name = "lblLeft"
        Me.lblLeft.Size = New System.Drawing.Size(32, 16)
        Me.lblLeft.TabIndex = 6
        Me.lblLeft.Text = "Left"
        '
        'txtLeft
        '
        Me.txtLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeft.Location = New System.Drawing.Point(208, 24)
        Me.txtLeft.MaxLength = 50
        Me.txtLeft.Name = "txtLeft"
        Me.txtLeft.Size = New System.Drawing.Size(49, 22)
        Me.txtLeft.TabIndex = 7
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(113, 57)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(30, 16)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "MM"
        '
        'txtInWordTop
        '
        Me.txtInWordTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInWordTop.Location = New System.Drawing.Point(56, 54)
        Me.txtInWordTop.MaxLength = 50
        Me.txtInWordTop.Name = "txtInWordTop"
        Me.txtInWordTop.Size = New System.Drawing.Size(49, 22)
        Me.txtInWordTop.TabIndex = 4
        '
        'GPAmountInWords
        '
        Me.GPAmountInWords.Controls.Add(Me.Label11)
        Me.GPAmountInWords.Controls.Add(Me.Label12)
        Me.GPAmountInWords.Controls.Add(Me.lblInWordsLeft)
        Me.GPAmountInWords.Controls.Add(Me.txtInWordLeft)
        Me.GPAmountInWords.Controls.Add(Me.lblInWordsTop)
        Me.GPAmountInWords.Controls.Add(Me.txtInWordTop)
        Me.GPAmountInWords.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPAmountInWords.Location = New System.Drawing.Point(763, 465)
        Me.GPAmountInWords.Name = "GPAmountInWords"
        Me.GPAmountInWords.Size = New System.Drawing.Size(177, 87)
        Me.GPAmountInWords.TabIndex = 99
        Me.GPAmountInWords.TabStop = False
        Me.GPAmountInWords.Text = "Amt In Words Position"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(105, 57)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 16)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "MM"
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = Global.Customised.My.Resources.Resources.reset
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(55, 55)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Reset Alignment")
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 41)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1574, 612)
        Me.pnlMain.TabIndex = 672
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.pnlForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 0)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(1574, 602)
        Me.pnlContent.TabIndex = 0
        '
        'pnlForm
        '
        Me.pnlForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlForm.Controls.Add(Me.Panel1)
        Me.pnlForm.Controls.Add(Me.btnExit)
        Me.pnlForm.Controls.Add(Me.btnNew)
        Me.pnlForm.Controls.Add(Me.Panel2)
        Me.pnlForm.Controls.Add(Me.btnSave)
        Me.pnlForm.Controls.Add(Me.btnSearch1)
        Me.pnlForm.Controls.Add(Me.btnDelete)
        Me.pnlForm.Controls.Add(Me.btnView)
        Me.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlForm.Location = New System.Drawing.Point(0, 0)
        Me.pnlForm.Name = "pnlForm"
        Me.pnlForm.Size = New System.Drawing.Size(1574, 602)
        Me.pnlForm.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TxtInpAcPayeeTop)
        Me.Panel1.Controls.Add(Me.TxtInpAcPayeeLeft)
        Me.Panel1.Controls.Add(Me.txtInpWordsLeft)
        Me.Panel1.Controls.Add(Me.txtFromTop)
        Me.Panel1.Controls.Add(Me.txtId)
        Me.Panel1.Controls.Add(Me.txtInpNameLeft)
        Me.Panel1.Controls.Add(Me.txtFromLeft)
        Me.Panel1.Controls.Add(Me.txtFilePath)
        Me.Panel1.Controls.Add(Me.txtInpAmountTop)
        Me.Panel1.Controls.Add(Me.txtInpAmountLeft)
        Me.Panel1.Controls.Add(Me.txtInpNameTop)
        Me.Panel1.Controls.Add(Me.txtFileName)
        Me.Panel1.Controls.Add(Me.txtSearchbox)
        Me.Panel1.Controls.Add(Me.txtPnlDateLeft)
        Me.Panel1.Controls.Add(Me.txtPnlDateTop)
        Me.Panel1.Location = New System.Drawing.Point(1254, 55)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(308, 173)
        Me.Panel1.TabIndex = 94
        Me.Panel1.Visible = False
        '
        'txtInpWordsLeft
        '
        Me.txtInpWordsLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInpWordsLeft.Location = New System.Drawing.Point(23, 32)
        Me.txtInpWordsLeft.MaxLength = 50
        Me.txtInpWordsLeft.Name = "txtInpWordsLeft"
        Me.txtInpWordsLeft.Size = New System.Drawing.Size(10, 22)
        Me.txtInpWordsLeft.TabIndex = 92
        Me.txtInpWordsLeft.Visible = False
        '
        'txtFromTop
        '
        Me.txtFromTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromTop.Location = New System.Drawing.Point(209, 91)
        Me.txtFromTop.MaxLength = 50
        Me.txtFromTop.Name = "txtFromTop"
        Me.txtFromTop.Size = New System.Drawing.Size(49, 22)
        Me.txtFromTop.TabIndex = 7
        Me.txtFromTop.Visible = False
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(234, 24)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(123, 20)
        Me.txtId.TabIndex = 81
        Me.txtId.Visible = False
        '
        'txtInpNameLeft
        '
        Me.txtInpNameLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInpNameLeft.Location = New System.Drawing.Point(168, 64)
        Me.txtInpNameLeft.MaxLength = 50
        Me.txtInpNameLeft.Name = "txtInpNameLeft"
        Me.txtInpNameLeft.Size = New System.Drawing.Size(10, 22)
        Me.txtInpNameLeft.TabIndex = 17
        Me.txtInpNameLeft.Visible = False
        '
        'txtFromLeft
        '
        Me.txtFromLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLeft.Location = New System.Drawing.Point(168, 11)
        Me.txtFromLeft.MaxLength = 50
        Me.txtFromLeft.Name = "txtFromLeft"
        Me.txtFromLeft.Size = New System.Drawing.Size(49, 22)
        Me.txtFromLeft.TabIndex = 6
        Me.txtFromLeft.Visible = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(135, 16)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(123, 20)
        Me.txtFilePath.TabIndex = 87
        Me.txtFilePath.Visible = False
        '
        'txtInpAmountTop
        '
        Me.txtInpAmountTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInpAmountTop.Location = New System.Drawing.Point(43, 32)
        Me.txtInpAmountTop.MaxLength = 50
        Me.txtInpAmountTop.Name = "txtInpAmountTop"
        Me.txtInpAmountTop.Size = New System.Drawing.Size(10, 22)
        Me.txtInpAmountTop.TabIndex = 91
        Me.txtInpAmountTop.Visible = False
        '
        'txtInpAmountLeft
        '
        Me.txtInpAmountLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInpAmountLeft.Location = New System.Drawing.Point(248, 68)
        Me.txtInpAmountLeft.MaxLength = 50
        Me.txtInpAmountLeft.Name = "txtInpAmountLeft"
        Me.txtInpAmountLeft.Size = New System.Drawing.Size(10, 22)
        Me.txtInpAmountLeft.TabIndex = 90
        Me.txtInpAmountLeft.Visible = False
        '
        'txtInpNameTop
        '
        Me.txtInpNameTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInpNameTop.Location = New System.Drawing.Point(207, -12)
        Me.txtInpNameTop.MaxLength = 50
        Me.txtInpNameTop.Name = "txtInpNameTop"
        Me.txtInpNameTop.Size = New System.Drawing.Size(10, 22)
        Me.txtInpNameTop.TabIndex = 19
        Me.txtInpNameTop.Visible = False
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(76, 67)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(10, 20)
        Me.txtFileName.TabIndex = 86
        Me.txtFileName.Visible = False
        '
        'txtSearchbox
        '
        Me.txtSearchbox.Location = New System.Drawing.Point(237, -2)
        Me.txtSearchbox.Name = "txtSearchbox"
        Me.txtSearchbox.Size = New System.Drawing.Size(123, 20)
        Me.txtSearchbox.TabIndex = 83
        Me.txtSearchbox.Visible = False
        '
        'txtPnlDateLeft
        '
        Me.txtPnlDateLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPnlDateLeft.Location = New System.Drawing.Point(75, 32)
        Me.txtPnlDateLeft.MaxLength = 50
        Me.txtPnlDateLeft.Name = "txtPnlDateLeft"
        Me.txtPnlDateLeft.Size = New System.Drawing.Size(16, 22)
        Me.txtPnlDateLeft.TabIndex = 13
        Me.txtPnlDateLeft.Visible = False
        '
        'txtPnlDateTop
        '
        Me.txtPnlDateTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPnlDateTop.Location = New System.Drawing.Point(56, 67)
        Me.txtPnlDateTop.MaxLength = 50
        Me.txtPnlDateTop.Name = "txtPnlDateTop"
        Me.txtPnlDateTop.Size = New System.Drawing.Size(10, 22)
        Me.txtPnlDateTop.TabIndex = 15
        Me.txtPnlDateTop.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox4)
        Me.Panel2.Controls.Add(Me.TxtDescription)
        Me.Panel2.Controls.Add(Me.btnBrowse)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.lblBankName)
        Me.Panel2.Controls.Add(Me.GPChequeSize)
        Me.Panel2.Controls.Add(Me.GPAmountInWords)
        Me.Panel2.Controls.Add(Me.GPAmount)
        Me.Panel2.Controls.Add(Me.GPDate)
        Me.Panel2.Controls.Add(Me.GPName)
        Me.Panel2.Controls.Add(Me.txtInpWordsTop)
        Me.Panel2.Controls.Add(Me.txtCode)
        Me.Panel2.Location = New System.Drawing.Point(12, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1110, 549)
        Me.Panel2.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.TxtAcPayeeLeft)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.TxtAcPayeeTop)
        Me.GroupBox4.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(949, 465)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(155, 87)
        Me.GroupBox4.TabIndex = 105
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "A/c Payee Position"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(105, 57)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(30, 16)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "MM"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(105, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(30, 16)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "MM"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(16, 27)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(32, 16)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Left"
        '
        'TxtAcPayeeLeft
        '
        Me.TxtAcPayeeLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcPayeeLeft.Location = New System.Drawing.Point(56, 24)
        Me.TxtAcPayeeLeft.MaxLength = 50
        Me.TxtAcPayeeLeft.Name = "TxtAcPayeeLeft"
        Me.TxtAcPayeeLeft.Size = New System.Drawing.Size(49, 22)
        Me.TxtAcPayeeLeft.TabIndex = 1
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(16, 57)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(31, 16)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Top"
        '
        'TxtAcPayeeTop
        '
        Me.TxtAcPayeeTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcPayeeTop.Location = New System.Drawing.Point(56, 54)
        Me.TxtAcPayeeTop.MaxLength = 50
        Me.TxtAcPayeeTop.Name = "TxtAcPayeeTop"
        Me.TxtAcPayeeTop.Size = New System.Drawing.Size(49, 22)
        Me.TxtAcPayeeTop.TabIndex = 4
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(-1, 60)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1098, 399)
        Me.GroupBox3.TabIndex = 101
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Cheque Preview"
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.Controls.Add(Me.LblDragAcPayee)
        Me.Panel4.Controls.Add(Me.lblDragAmount)
        Me.Panel4.Controls.Add(Me.lblDragAmountWord)
        Me.Panel4.Controls.Add(Me.lblDragName)
        Me.Panel4.Controls.Add(Me.lblDragDate)
        Me.Panel4.Controls.Add(Me.Panel3)
        Me.Panel4.Controls.Add(Me.imgChequePreview)
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 22)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1092, 374)
        Me.Panel4.TabIndex = 89
        '
        'LblDragAcPayee
        '
        Me.LblDragAcPayee.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblDragAcPayee.Location = New System.Drawing.Point(503, 77)
        Me.LblDragAcPayee.Name = "LblDragAcPayee"
        Me.LblDragAcPayee.Size = New System.Drawing.Size(162, 28)
        Me.LblDragAcPayee.TabIndex = 93
        Me.LblDragAcPayee.Text = "A/c Payee [Drag Me]"
        Me.LblDragAcPayee.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDragAmount
        '
        Me.lblDragAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblDragAmount.Location = New System.Drawing.Point(733, 179)
        Me.lblDragAmount.Name = "lblDragAmount"
        Me.lblDragAmount.Size = New System.Drawing.Size(145, 28)
        Me.lblDragAmount.TabIndex = 92
        Me.lblDragAmount.Text = "Amount [Drag Me]"
        Me.lblDragAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDragAmountWord
        '
        Me.lblDragAmountWord.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblDragAmountWord.Location = New System.Drawing.Point(156, 242)
        Me.lblDragAmountWord.Name = "lblDragAmountWord"
        Me.lblDragAmountWord.Size = New System.Drawing.Size(353, 28)
        Me.lblDragAmountWord.TabIndex = 91
        Me.lblDragAmountWord.Text = "Amount in Word [Drag Me]"
        Me.lblDragAmountWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDragName
        '
        Me.lblDragName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblDragName.Location = New System.Drawing.Point(158, 165)
        Me.lblDragName.Name = "lblDragName"
        Me.lblDragName.Size = New System.Drawing.Size(454, 28)
        Me.lblDragName.TabIndex = 90
        Me.lblDragName.Text = "Name [Drag Me]"
        Me.lblDragName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDragDate
        '
        Me.lblDragDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblDragDate.Location = New System.Drawing.Point(747, 58)
        Me.lblDragDate.Name = "lblDragDate"
        Me.lblDragDate.Size = New System.Drawing.Size(162, 28)
        Me.lblDragDate.TabIndex = 89
        Me.lblDragDate.Text = "Date [Drag Me]"
        Me.lblDragDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.panelVertical)
        Me.Panel3.Controls.Add(Me.RulerControl2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 55)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(55, 319)
        Me.Panel3.TabIndex = 2
        '
        'panelVertical
        '
        Me.panelVertical.BackColor = System.Drawing.Color.Teal
        Me.panelVertical.Location = New System.Drawing.Point(1, 1)
        Me.panelVertical.Name = "panelVertical"
        Me.panelVertical.Size = New System.Drawing.Size(55, 5)
        Me.panelVertical.TabIndex = 94
        Me.panelVertical.Visible = False
        '
        'RulerControl2
        '
        Me.RulerControl2.ActualSize = True
        Me.RulerControl2.DivisionMarkFactor = 5
        Me.RulerControl2.Divisions = 10
        Me.RulerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RulerControl2.ForeColor = System.Drawing.Color.Black
        Me.RulerControl2.Location = New System.Drawing.Point(0, 0)
        Me.RulerControl2.MajorInterval = 10
        Me.RulerControl2.MiddleMarkFactor = 3
        Me.RulerControl2.MouseTrackingOn = False
        Me.RulerControl2.Name = "RulerControl2"
        Me.RulerControl2.Orientation = Lyquidity.UtilityLibrary.Controls.enumOrientation.orVertical
        Me.RulerControl2.RulerAlignment = Lyquidity.UtilityLibrary.Controls.enumRulerAlignment.raBottomOrRight
        Me.RulerControl2.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smMillimetres
        Me.RulerControl2.Size = New System.Drawing.Size(55, 319)
        Me.RulerControl2.StartValue = 0R
        Me.RulerControl2.TabIndex = 0
        Me.RulerControl2.Text = "RulerControl2"
        Me.RulerControl2.VerticalNumbers = True
        Me.RulerControl2.ZoomFactor = 1.0R
        '
        'imgChequePreview
        '
        Me.imgChequePreview.BackColor = System.Drawing.Color.Transparent
        Me.imgChequePreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgChequePreview.Location = New System.Drawing.Point(53, 52)
        Me.imgChequePreview.Name = "imgChequePreview"
        Me.imgChequePreview.Size = New System.Drawing.Size(1035, 320)
        Me.imgChequePreview.TabIndex = 88
        Me.imgChequePreview.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.panelHorizantal)
        Me.Panel5.Controls.Add(Me.RulerControl1)
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1092, 55)
        Me.Panel5.TabIndex = 1
        '
        'panelHorizantal
        '
        Me.panelHorizantal.BackColor = System.Drawing.Color.Teal
        Me.panelHorizantal.Location = New System.Drawing.Point(56, 1)
        Me.panelHorizantal.Name = "panelHorizantal"
        Me.panelHorizantal.Size = New System.Drawing.Size(5, 52)
        Me.panelHorizantal.TabIndex = 93
        Me.panelHorizantal.Visible = False
        '
        'RulerControl1
        '
        Me.RulerControl1.ActualSize = True
        Me.RulerControl1.DivisionMarkFactor = 5
        Me.RulerControl1.Divisions = 10
        Me.RulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RulerControl1.ForeColor = System.Drawing.Color.Black
        Me.RulerControl1.Location = New System.Drawing.Point(55, 0)
        Me.RulerControl1.MajorInterval = 10
        Me.RulerControl1.MiddleMarkFactor = 3
        Me.RulerControl1.MouseTrackingOn = False
        Me.RulerControl1.Name = "RulerControl1"
        Me.RulerControl1.Orientation = Lyquidity.UtilityLibrary.Controls.enumOrientation.orHorizontal
        Me.RulerControl1.RulerAlignment = Lyquidity.UtilityLibrary.Controls.enumRulerAlignment.raBottomOrRight
        Me.RulerControl1.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smMillimetres
        Me.RulerControl1.Size = New System.Drawing.Size(1037, 55)
        Me.RulerControl1.StartValue = 0R
        Me.RulerControl1.TabIndex = 1
        Me.RulerControl1.Text = "RulerControl1"
        Me.RulerControl1.VerticalNumbers = True
        Me.RulerControl1.ZoomFactor = 1.0R
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.PictureBox1)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(55, 55)
        Me.Panel6.TabIndex = 0
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.BackColor = System.Drawing.Color.Transparent
        Me.lblBankName.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.ForeColor = System.Drawing.Color.Black
        Me.lblBankName.Location = New System.Drawing.Point(57, 24)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(98, 19)
        Me.lblBankName.TabIndex = 94
        Me.lblBankName.Text = "Bank Name"
        '
        'GPChequeSize
        '
        Me.GPChequeSize.Controls.Add(Me.Label6)
        Me.GPChequeSize.Controls.Add(Me.lblTop)
        Me.GPChequeSize.Controls.Add(Me.txtTop)
        Me.GPChequeSize.Controls.Add(Me.Label5)
        Me.GPChequeSize.Controls.Add(Me.lblLeft)
        Me.GPChequeSize.Controls.Add(Me.txtLeft)
        Me.GPChequeSize.Controls.Add(Me.Label15)
        Me.GPChequeSize.Controls.Add(Me.Label16)
        Me.GPChequeSize.Controls.Add(Me.lblWidth)
        Me.GPChequeSize.Controls.Add(Me.txtWidth)
        Me.GPChequeSize.Controls.Add(Me.lblHeight)
        Me.GPChequeSize.Controls.Add(Me.txtHeight)
        Me.GPChequeSize.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPChequeSize.Location = New System.Drawing.Point(-5, 463)
        Me.GPChequeSize.Name = "GPChequeSize"
        Me.GPChequeSize.Size = New System.Drawing.Size(303, 87)
        Me.GPChequeSize.TabIndex = 100
        Me.GPChequeSize.TabStop = False
        Me.GPChequeSize.Text = "Cheque Size"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(113, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(30, 16)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "MM"
        '
        'lblWidth
        '
        Me.lblWidth.AutoSize = True
        Me.lblWidth.BackColor = System.Drawing.Color.Transparent
        Me.lblWidth.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWidth.ForeColor = System.Drawing.Color.Black
        Me.lblWidth.Location = New System.Drawing.Point(13, 27)
        Me.lblWidth.Name = "lblWidth"
        Me.lblWidth.Size = New System.Drawing.Size(45, 16)
        Me.lblWidth.TabIndex = 0
        Me.lblWidth.Text = "Width"
        '
        'txtWidth
        '
        Me.txtWidth.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWidth.Location = New System.Drawing.Point(64, 24)
        Me.txtWidth.MaxLength = 50
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(49, 22)
        Me.txtWidth.TabIndex = 1
        '
        'lblHeight
        '
        Me.lblHeight.AutoSize = True
        Me.lblHeight.BackColor = System.Drawing.Color.Transparent
        Me.lblHeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeight.ForeColor = System.Drawing.Color.Black
        Me.lblHeight.Location = New System.Drawing.Point(13, 57)
        Me.lblHeight.Name = "lblHeight"
        Me.lblHeight.Size = New System.Drawing.Size(49, 16)
        Me.lblHeight.TabIndex = 3
        Me.lblHeight.Text = "Height"
        '
        'txtHeight
        '
        Me.txtHeight.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeight.Location = New System.Drawing.Point(64, 54)
        Me.txtHeight.MaxLength = 50
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.Size = New System.Drawing.Size(49, 22)
        Me.txtHeight.TabIndex = 4
        '
        'GPDate
        '
        Me.GPDate.Controls.Add(Me.Label3)
        Me.GPDate.Controls.Add(Me.Label4)
        Me.GPDate.Controls.Add(Me.lblDateLeft)
        Me.GPDate.Controls.Add(Me.txtDateLeft)
        Me.GPDate.Controls.Add(Me.lblDateTop)
        Me.GPDate.Controls.Add(Me.txtDateTop)
        Me.GPDate.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPDate.Location = New System.Drawing.Point(457, 465)
        Me.GPDate.Name = "GPDate"
        Me.GPDate.Size = New System.Drawing.Size(146, 87)
        Me.GPDate.TabIndex = 97
        Me.GPDate.TabStop = False
        Me.GPDate.Text = "Date Position"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(105, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "MM"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(105, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 16)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "MM"
        '
        'lblDateLeft
        '
        Me.lblDateLeft.AutoSize = True
        Me.lblDateLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblDateLeft.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateLeft.ForeColor = System.Drawing.Color.Black
        Me.lblDateLeft.Location = New System.Drawing.Point(16, 27)
        Me.lblDateLeft.Name = "lblDateLeft"
        Me.lblDateLeft.Size = New System.Drawing.Size(32, 16)
        Me.lblDateLeft.TabIndex = 0
        Me.lblDateLeft.Text = "Left"
        '
        'txtDateLeft
        '
        Me.txtDateLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateLeft.Location = New System.Drawing.Point(56, 24)
        Me.txtDateLeft.MaxLength = 50
        Me.txtDateLeft.Name = "txtDateLeft"
        Me.txtDateLeft.Size = New System.Drawing.Size(49, 22)
        Me.txtDateLeft.TabIndex = 1
        '
        'lblDateTop
        '
        Me.lblDateTop.AutoSize = True
        Me.lblDateTop.BackColor = System.Drawing.Color.Transparent
        Me.lblDateTop.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateTop.ForeColor = System.Drawing.Color.Black
        Me.lblDateTop.Location = New System.Drawing.Point(16, 57)
        Me.lblDateTop.Name = "lblDateTop"
        Me.lblDateTop.Size = New System.Drawing.Size(31, 16)
        Me.lblDateTop.TabIndex = 3
        Me.lblDateTop.Text = "Top"
        '
        'txtDateTop
        '
        Me.txtDateTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTop.Location = New System.Drawing.Point(56, 54)
        Me.txtDateTop.MaxLength = 50
        Me.txtDateTop.Name = "txtDateTop"
        Me.txtDateTop.Size = New System.Drawing.Size(49, 22)
        Me.txtDateTop.TabIndex = 4
        '
        'GPName
        '
        Me.GPName.Controls.Add(Me.Label2)
        Me.GPName.Controls.Add(Me.Label9)
        Me.GPName.Controls.Add(Me.lblPayeeNameLeft)
        Me.GPName.Controls.Add(Me.txtNameLeft)
        Me.GPName.Controls.Add(Me.lblNameTop)
        Me.GPName.Controls.Add(Me.txtNameTop)
        Me.GPName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPName.Location = New System.Drawing.Point(304, 465)
        Me.GPName.Name = "GPName"
        Me.GPName.Size = New System.Drawing.Size(148, 87)
        Me.GPName.TabIndex = 96
        Me.GPName.TabStop = False
        Me.GPName.Text = "Name Position"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(105, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "MM"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(105, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 16)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "MM"
        '
        'lblPayeeNameLeft
        '
        Me.lblPayeeNameLeft.AutoSize = True
        Me.lblPayeeNameLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblPayeeNameLeft.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayeeNameLeft.ForeColor = System.Drawing.Color.Black
        Me.lblPayeeNameLeft.Location = New System.Drawing.Point(16, 27)
        Me.lblPayeeNameLeft.Name = "lblPayeeNameLeft"
        Me.lblPayeeNameLeft.Size = New System.Drawing.Size(32, 16)
        Me.lblPayeeNameLeft.TabIndex = 0
        Me.lblPayeeNameLeft.Text = "Left"
        '
        'txtNameLeft
        '
        Me.txtNameLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameLeft.Location = New System.Drawing.Point(56, 24)
        Me.txtNameLeft.MaxLength = 50
        Me.txtNameLeft.Name = "txtNameLeft"
        Me.txtNameLeft.Size = New System.Drawing.Size(49, 22)
        Me.txtNameLeft.TabIndex = 1
        '
        'lblNameTop
        '
        Me.lblNameTop.AutoSize = True
        Me.lblNameTop.BackColor = System.Drawing.Color.Transparent
        Me.lblNameTop.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameTop.ForeColor = System.Drawing.Color.Black
        Me.lblNameTop.Location = New System.Drawing.Point(16, 57)
        Me.lblNameTop.Name = "lblNameTop"
        Me.lblNameTop.Size = New System.Drawing.Size(31, 16)
        Me.lblNameTop.TabIndex = 3
        Me.lblNameTop.Text = "Top"
        '
        'txtNameTop
        '
        Me.txtNameTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameTop.Location = New System.Drawing.Point(56, 54)
        Me.txtNameTop.MaxLength = 50
        Me.txtNameTop.Name = "txtNameTop"
        Me.txtNameTop.Size = New System.Drawing.Size(49, 22)
        Me.txtNameTop.TabIndex = 4
        '
        'txtInpWordsTop
        '
        Me.txtInpWordsTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInpWordsTop.Location = New System.Drawing.Point(627, 324)
        Me.txtInpWordsTop.MaxLength = 50
        Me.txtInpWordsTop.Name = "txtInpWordsTop"
        Me.txtInpWordsTop.Size = New System.Drawing.Size(10, 22)
        Me.txtInpWordsTop.TabIndex = 104
        Me.txtInpWordsTop.Visible = False
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(561, 381)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(88, 20)
        Me.txtCode.TabIndex = 103
        Me.txtCode.Visible = False
        '
        'pnlFooter
        '
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 602)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(1574, 10)
        Me.pnlFooter.TabIndex = 1
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'TxtInpAcPayeeLeft
        '
        Me.TxtInpAcPayeeLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInpAcPayeeLeft.Location = New System.Drawing.Point(43, 123)
        Me.TxtInpAcPayeeLeft.MaxLength = 50
        Me.TxtInpAcPayeeLeft.Name = "TxtInpAcPayeeLeft"
        Me.TxtInpAcPayeeLeft.Size = New System.Drawing.Size(10, 22)
        Me.TxtInpAcPayeeLeft.TabIndex = 93
        Me.TxtInpAcPayeeLeft.Visible = False
        '
        'TxtInpAcPayeeTop
        '
        Me.TxtInpAcPayeeTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInpAcPayeeTop.Location = New System.Drawing.Point(75, 123)
        Me.TxtInpAcPayeeTop.MaxLength = 50
        Me.TxtInpAcPayeeTop.Name = "TxtInpAcPayeeTop"
        Me.TxtInpAcPayeeTop.Size = New System.Drawing.Size(10, 22)
        Me.TxtInpAcPayeeTop.TabIndex = 94
        Me.TxtInpAcPayeeTop.Visible = False
        '
        'FrmChequeUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1574, 653)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblDescription)
        Me.MaximizeBox = True
        Me.Name = "FrmChequeUI"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblDescription, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.pnlMain, 0)
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
        Me.GPAmount.ResumeLayout(False)
        Me.GPAmount.PerformLayout()
        Me.GPAmountInWords.ResumeLayout(False)
        Me.GPAmountInWords.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlForm.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.imgChequePreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.GPChequeSize.ResumeLayout(False)
        Me.GPChequeSize.PerformLayout()
        Me.GPDate.ResumeLayout(False)
        Me.GPDate.PerformLayout()
        Me.GPName.ResumeLayout(False)
        Me.GPName.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents btnBrowse As Button
    Friend WithEvents btnSearch1 As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnView As Button
    Friend WithEvents lblInWordsLeft As Label
    Friend WithEvents txtInWordLeft As TextBox
    Friend WithEvents lblInWordsTop As Label
    Friend WithEvents GPAmount As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents lblAmountLeft As Label
    Friend WithEvents txtAmtLeft As TextBox
    Friend WithEvents lblAmountTop As Label
    Friend WithEvents txtAmtTop As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lblTop As Label
    Friend WithEvents txtTop As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lblLeft As Label
    Friend WithEvents txtLeft As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtInWordTop As TextBox
    Friend WithEvents GPAmountInWords As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlContent As Panel
    Friend WithEvents pnlForm As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblDragAmount As Label
    Friend WithEvents lblDragAmountWord As Label
    Friend WithEvents lblDragName As Label
    Friend WithEvents lblDragDate As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents panelVertical As Panel
    Friend WithEvents RulerControl2 As Lyquidity.UtilityLibrary.Controls.RulerControl
    Friend WithEvents imgChequePreview As PictureBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents panelHorizantal As Panel
    Friend WithEvents RulerControl1 As Lyquidity.UtilityLibrary.Controls.RulerControl
    Friend WithEvents Panel6 As Panel
    Friend WithEvents lblBankName As Label
    Friend WithEvents GPChequeSize As GroupBox
    Friend WithEvents Label16 As Label
    Friend WithEvents lblWidth As Label
    Friend WithEvents txtWidth As TextBox
    Friend WithEvents lblHeight As Label
    Friend WithEvents txtHeight As TextBox
    Friend WithEvents GPDate As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblDateLeft As Label
    Friend WithEvents txtDateLeft As TextBox
    Friend WithEvents lblDateTop As Label
    Friend WithEvents txtDateTop As TextBox
    Friend WithEvents GPName As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lblPayeeNameLeft As Label
    Friend WithEvents txtNameLeft As TextBox
    Friend WithEvents lblNameTop As Label
    Friend WithEvents txtNameTop As TextBox
    Friend WithEvents txtInpWordsTop As TextBox
    Friend WithEvents txtCode As TextBox
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtInpWordsLeft As TextBox
    Friend WithEvents txtFromTop As TextBox
    Friend WithEvents txtId As TextBox
    Friend WithEvents txtInpNameLeft As TextBox
    Friend WithEvents txtFromLeft As TextBox
    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents txtInpAmountTop As TextBox
    Friend WithEvents txtInpAmountLeft As TextBox
    Friend WithEvents txtInpNameTop As TextBox
    Friend WithEvents txtFileName As TextBox
    Friend WithEvents txtSearchbox As TextBox
    Friend WithEvents txtPnlDateLeft As TextBox
    Friend WithEvents txtPnlDateTop As TextBox
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation

        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From ChequeUI Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From ChequeUI Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where 1=1  "

        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Description  FROM ChequeUI I "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "ChequeUI"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans


        mQry = "UPDATE ChequeUI 
                 SET 
                 Description = " & AgL.Chk_Text(TxtDescription.Text) & ", 
                 BankImg = @BankImg, 
                 ChqWidth = " & Val(txtWidth.Text) & ", 
                 ChqHeight = " & Val(txtHeight.Text) & ", 
                 LeftMargin = " & Val(txtLeft.Text) & ", 
                 TopMargin = " & Val(txtTop.Text) & ", 
                 PnlDateLeft = " & Val(txtPnlDateLeft.Text) & ", 
                 PnlDateTop = " & Val(txtPnlDateTop.Text) & ", 
                 InpAcPayeeLeft = " & Val(TxtInpAcPayeeLeft.Text) & ", 
                 InpAcPayeeTop = " & Val(TxtInpAcPayeeTop.Text) & ", 
                 InpNameLeft = " & Val(txtInpNameLeft.Text) & ", 
                 InpNameTop = " & Val(txtInpNameTop.Text) & ", 
                 InpAmountLeft = " & Val(txtInpAmountLeft.Text) & ", 
                 InpAmountTop = " & Val(txtInpAmountTop.Text) & ", 
                 InpWordsLeft = " & Val(txtInpWordsLeft.Text) & ", 
                 InpWordsTop = " & Val(txtInpWordsTop.Text) & ", 
                 RPTDateLeft = " & Val(txtDateLeft.Text) & ", 
                 RPTDateTop = " & Val(txtDateTop.Text) & ", 
                 RPTAcPayeeLeft = " & Val(TxtAcPayeeLeft.Text) & ", 
                 RPTAcPayeeTop = " & Val(TxtAcPayeeTop.Text) & ", 
                 RPTNameLeft = " & Val(txtNameLeft.Text) & ", 
                 RPTNameTop = " & Val(txtNameTop.Text) & ", 
                 RPTAmountLeft = " & Val(txtAmtLeft.Text) & ", 
                 RPTAmountTop = " & Val(txtAmtTop.Text) & ", 
                 RPTWordsLeft = " & Val(txtInWordTop.Text) & ", 
                 RPTWordsTop = " & Val(txtInWordTop.Text) & " 
                 Where Code = '" & SearchCode & "' "


        If imgChequePreview.Tag Is Nothing Then
            txtFilePath.Text = Application.StartupPath & "\Default.png"
            txtFileName.Text = System.IO.Path.GetFileNameWithoutExtension("Default.png")
            Me.imgChequePreview.ImageLocation = txtFilePath.Text
            imgChequePreview.Tag = ImageToStream(txtFilePath.Text)
            AgL.ECmd.Parameters.AddWithValue("@BankImg", imgChequePreview.Tag)
        Else
            AgL.ECmd.Parameters.AddWithValue("@BankImg", imgChequePreview.Tag)
        End If

        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        'mQry = "Select Code, Description As Name " &
        '        " From ChequeUI " &
        '        " Order By Description "
        'TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.* " &
                " From ChequeUI H " &
                " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                txtWidth.Text = AgL.VNull(.Rows(0)("ChqWidth"))
                txtHeight.Text = AgL.VNull(.Rows(0)("ChqHeight"))
                txtLeft.Text = AgL.VNull(.Rows(0)("LeftMargin"))
                txtTop.Text = AgL.VNull(.Rows(0)("TopMargin"))
                txtPnlDateLeft.Text = AgL.VNull(.Rows(0)("PnlDateLeft"))
                txtPnlDateTop.Text = AgL.VNull(.Rows(0)("PnlDateTop"))
                TxtInpAcPayeeLeft.Text = AgL.VNull(.Rows(0)("InpAcPayeeLeft"))
                TxtInpAcPayeeTop.Text = AgL.VNull(.Rows(0)("InpAcPayeeTop"))
                txtInpNameLeft.Text = AgL.VNull(.Rows(0)("InpNameLeft"))
                txtInpNameTop.Text = AgL.VNull(.Rows(0)("InpNameTop"))
                txtInpAmountLeft.Text = AgL.VNull(.Rows(0)("InpAmountLeft"))
                txtInpAmountTop.Text = AgL.VNull(.Rows(0)("InpAmountTop"))
                txtInpWordsLeft.Text = AgL.VNull(.Rows(0)("InpWordsLeft"))
                txtInpWordsTop.Text = AgL.VNull(.Rows(0)("InpWordsTop"))
                txtDateLeft.Text = AgL.VNull(.Rows(0)("RptDateLeft"))
                txtDateTop.Text = AgL.VNull(.Rows(0)("RptDateTop"))
                TxtAcPayeeLeft.Text = AgL.VNull(.Rows(0)("RptAcPayeeLeft"))
                TxtAcPayeeTop.Text = AgL.VNull(.Rows(0)("RptAcPayeeTop"))
                txtNameLeft.Text = AgL.VNull(.Rows(0)("RptNameLeft"))
                txtNameTop.Text = AgL.VNull(.Rows(0)("RptNameTop"))
                txtAmtLeft.Text = AgL.VNull(.Rows(0)("RptAmountLeft"))
                txtAmtTop.Text = AgL.VNull(.Rows(0)("RptAmountTop"))
                txtInWordLeft.Text = AgL.VNull(.Rows(0)("RptWordsLeft"))
                txtInWordTop.Text = AgL.VNull(.Rows(0)("RptWordsTop"))


                Dim img_buffer() As Byte
                img_buffer = CType(.Rows(0)("BankImg"), Byte())
                imgChequePreview.Tag = img_buffer
                Dim img_stream As New IO.MemoryStream(img_buffer, True)
                img_stream.Write(img_buffer, 0, img_buffer.Length)
                imgChequePreview.Image = New Bitmap(img_stream)
                img_stream.Close()
                imgChequePreview.SizeMode = PictureBoxSizeMode.StretchImage
                imgChequePreview.Width = ((Val(txtWidth.Text) / oneInch2MM) * 96) * (96 / 72)
                imgChequePreview.Height = ((Val(txtHeight.Text) / oneInch2MM) * 96) * (96 / 72)

            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown
        Select Case sender.Name
            Case TxtDescription.Name
                'If e.KeyCode = Keys.Enter Then
                '    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                '        Topctrl1.FButtonClick(13)
                '    End If
                'End If
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
                " From ChequeUI I " & mConStr &
                " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmChequeUI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 300, 885)
    End Sub

    Private Sub TxtManualCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown
        Select Case sender.NAME
            Case TxtDescription.Name
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
            'mQry = " Select Count(*) From ItemCategory Where ChequeUI = '" & mSearchCode & "'"
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

    Private Sub FrmChequeUI_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
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

        'BlankTextBoxes(Me)
        imgChequePreview.Tag = Nothing
        imgChequePreview.Image = Nothing
    End Sub
    Sub defaultChequeValue()
        If Topctrl1.Mode.ToString.ToUpper = "BROWSE" Then Exit Sub

        txtWidth.Text = "176"
        txtHeight.Text = "89"
        txtPnlDateLeft.Text = "480"
        txtPnlDateTop.Text = "46"
        TxtInpAcPayeeLeft.Text = "200"
        TxtInpAcPayeeTop.Text = "46"
        txtInpNameLeft.Text = "72"
        txtInpNameTop.Text = "97"
        txtInpAmountLeft.Text = "480"
        txtInpAmountTop.Text = "127"
        txtInpWordsLeft.Text = "87"
        txtInpWordsTop.Text = "121"

        txtNameLeft.Text = 20
        txtNameTop.Text = 31

        TxtAcPayeeLeft.Text = 80
        TxtAcPayeeTop.Text = 19


        txtDateLeft.Text = 127
        txtDateTop.Text = 19

        txtAmtLeft.Text = 127
        txtAmtTop.Text = 40

        txtInWordLeft.Text = 25
        txtInWordTop.Text = 41

        txtLeft.Text = 0
        txtTop.Text = 0.75
    End Sub


    Public Shared Sub ImportChequeUITable(ChequeUITable As StructChequeUI)
        Dim mQry As String = ""
        If AgL.Dman_Execute("SELECT Count(*) From ChequeUI With (NoLock) Where Description = '" & ChequeUITable.Description & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = " INSERT INTO ChequeUI(Code, Description, EntryBy, EntryDate, EntryType, EntryStatus, OMSId)
                    Select '" & ChequeUITable.Code & "' As ChequeUICode, " & AgL.Chk_Text(ChequeUITable.Description) & " As ChequeUI, 
                    '" & ChequeUITable.EntryBy & "' As EntryBy, " & AgL.Chk_Date(ChequeUITable.EntryDate) & " As EntryDate, 
                    '" & ChequeUITable.EntryType & "' As EntryType, '" & ChequeUITable.EntryStatus & "' As EntryStatus, 
                    '" & ChequeUITable.OMSId & "' As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE ChequeUI Set OMSId = '" & ChequeUITable.OMSId & "' 
                    Where Description = '" & ChequeUITable.Description & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Public Structure StructChequeUI
        Dim Code As String
        Dim Description As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim OMSId As String
    End Structure

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If Topctrl1.Mode.ToString.ToUpper = "BROWSE" Then Exit Sub
        Dim od As New OpenFileDialog
        Dim mByteArr As Byte()
        od.Title = "Select Cheque Image File"
        od.Filter = "JPG Files(*.jpg)|*.jpg|JPEG Files(*.jpeg)|*.jpeg" &
                                "|Gif Files(*.gif)|*.gif|Bitmap Files(*.bmp)|*.bmp|PNG Files(*.png)|*.png"

        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFilePath.Text = od.FileName
            txtFileName.Text = System.IO.Path.GetFileNameWithoutExtension(od.FileName)

            mByteArr = ClsMain.ImageToStream(txtFilePath.Text)
            If mByteArr.Length > 1000000 Then
                MsgBox("Image size can not exeed 1 MB, Kindly select any other image.")
                Exit Sub
            End If

            imgChequePreview.Tag = mByteArr
            imgChequePreview.Image = New Bitmap(od.FileName)

            'Me.PictureBox1.ImageLocation = txtFilePath.Text

            imgChequePreview.SizeMode = PictureBoxSizeMode.StretchImage
            txtWidth.Text = Math.Round(Val(imgChequePreview.Image.Width / imgChequePreview.Image.HorizontalResolution) * oneInch2MM)
            txtHeight.Text = Math.Round(Val(imgChequePreview.Image.Height / imgChequePreview.Image.VerticalResolution) * oneInch2MM)
            imgChequePreview.Width = ((Val(txtWidth.Text) / oneInch2MM) * 96) * (96 / 72)
            imgChequePreview.Height = ((Val(txtHeight.Text) / oneInch2MM) * 96) * (96 / 72)
        End If

    End Sub

    Private Sub CTRL_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblDragDate.MouseDown, lblDragName.MouseDown, lblDragAmountWord.MouseDown, lblDragAmount.MouseDown, LblDragAcPayee.MouseDown
        If Topctrl1.Mode.ToString.ToUpper = "BROWSE" Then Exit Sub
        IsDrag = True
        DateX = e.X
        DateY = e.Y
        sender.BorderStyle = BorderStyle.FixedSingle
        panelHorizantal.Visible = True
        panelVertical.Visible = True
    End Sub

    Private Sub CTRL_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblDragDate.MouseMove, lblDragName.MouseMove, lblDragAmountWord.MouseMove, lblDragAmount.MouseMove, LblDragAcPayee.MouseMove
        If Topctrl1.Mode.ToString.ToUpper = "BROWSE" Then Exit Sub
        If IsDrag Then
            Dim L, T As Integer
            L = sender.Left + e.X - DateX
            T = sender.Top + e.Y - DateY
            If imgChequePreview.Left < L Then sender.Left = L
            If imgChequePreview.Top < T Then sender.Top = T
            panelHorizantal.Left = L
            panelVertical.Top = T - imgChequePreview.Top
            sender.BringToFront()
            If sender.Name = lblDragDate.Name Then
                txtDateLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                txtDateTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            ElseIf sender.Name = LblDragAcPayee.Name Then
                TxtAcPayeeLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                TxtAcPayeeTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            ElseIf sender.Name = lblDragName.Name Then
                txtNameLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                txtNameTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            ElseIf sender.Name = lblDragAmount.Name Then
                txtAmtLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                txtAmtTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            ElseIf sender.Name = lblDragAmountWord.Name Then
                txtInWordLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                txtInWordTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            End If
        End If

    End Sub

    Private Sub CTRL_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblDragDate.MouseUp, lblDragName.MouseUp, lblDragAmountWord.MouseUp, lblDragAmount.MouseUp, LblDragAcPayee.MouseUp
        If Topctrl1.Mode.ToString.ToUpper = "BROWSE" Then Exit Sub
        IsDrag = False
        sender.BorderStyle = BorderStyle.None
        panelHorizantal.Visible = False
        panelVertical.Visible = False
    End Sub

    Private Sub txtNameLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameLeft.TextChanged
        lblDragName.Left = imgChequePreview.Left + Math.Round(Val(txtNameLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtNameTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameTop.TextChanged
        lblDragName.Top = imgChequePreview.Top + Math.Round(Val(txtNameTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtDateLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDateLeft.TextChanged
        lblDragDate.Left = imgChequePreview.Left + Math.Round(Val(txtDateLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtDateTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDateTop.TextChanged
        lblDragDate.Top = imgChequePreview.Top + Math.Round(Val(txtDateTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtAcPayeeLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtAcPayeeLeft.TextChanged
        LblDragAcPayee.Left = imgChequePreview.Left + Math.Round(Val(TxtAcPayeeLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtAcPayeeTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtAcPayeeTop.TextChanged
        LblDragAcPayee.Top = imgChequePreview.Top + Math.Round(Val(TxtAcPayeeTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub


    Private Sub txtAmtLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtLeft.TextChanged
        lblDragAmount.Left = imgChequePreview.Left + Math.Round(Val(txtAmtLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtAmtTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtTop.TextChanged
        lblDragAmount.Top = imgChequePreview.Top + Math.Round(Val(txtAmtTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtInWordLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInWordLeft.TextChanged
        lblDragAmountWord.Left = imgChequePreview.Left + Math.Round(Val(txtInWordLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtInWordTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInWordTop.TextChanged
        lblDragAmountWord.Top = imgChequePreview.Top + Math.Round(Val(txtInWordTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub FrmChequeUI_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        defaultChequeValue()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        defaultChequeValue()
    End Sub



    Private Sub txtWidth_TextChanged(sender As Object, e As EventArgs) Handles txtWidth.TextChanged, txtHeight.TextChanged
        imgChequePreview.SizeMode = PictureBoxSizeMode.StretchImage
        imgChequePreview.Width = ((Val(txtWidth.Text) / oneInch2MM) * 96) * (96 / 72)
        imgChequePreview.Height = ((Val(txtHeight.Text) / oneInch2MM) * 96) * (96 / 72)
    End Sub
End Class
