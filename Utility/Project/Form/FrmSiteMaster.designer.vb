<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSiteMaster
Inherits System.Windows.Forms.Form
'Form overrides dispose to clean up the component list.
<System.Diagnostics.DebuggerNonUserCode()> _
Protected Overrides Sub Dispose(ByVal disposing As Boolean)
If Disposing AndAlso components IsNot Nothing Then
components.Dispose()
End If
MyBase.Dispose(Disposing)
End Sub
'Required by the Windows Form Designer
Private components As System.ComponentModel.IContainer
'NOTE: The following procedure is required by the Windows Form Designer
'It can be modified using the Windows Form Designer.  
'Do not modify it using the code editor.          [Ag]
<System.Diagnostics.DebuggerStepThrough()> _
Private Sub InitializeComponent()
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.CboName = New AgControls.AgComboBox
        Me.LblName = New System.Windows.Forms.Label
        Me.LblNameReq = New System.Windows.Forms.Label
        Me.TxtHO_YN = New AgControls.AgTextBox
        Me.LblHO_YN = New System.Windows.Forms.Label
        Me.LblHO_YNReq = New System.Windows.Forms.Label
        Me.TxtAdd1 = New AgControls.AgTextBox
        Me.LblAdd1 = New System.Windows.Forms.Label
        Me.TxtAdd2 = New AgControls.AgTextBox
        Me.TxtAdd3 = New AgControls.AgTextBox
        Me.CboCity_Code = New AgControls.AgComboBox
        Me.LblCity_Code = New System.Windows.Forms.Label
        Me.TxtPinNo = New AgControls.AgTextBox
        Me.LblPinNo = New System.Windows.Forms.Label
        Me.TxtPhone = New AgControls.AgTextBox
        Me.LblPhone = New System.Windows.Forms.Label
        Me.TxtMobile = New AgControls.AgTextBox
        Me.LblMobile = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TxtModified = New System.Windows.Forms.TextBox
        Me.GrpUP = New System.Windows.Forms.GroupBox
        Me.TxtPrepared = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.CboSiteCode = New AgControls.AgComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.LblManualCodeReq = New System.Windows.Forms.Label
        Me.TxtAcCode = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtCreditLimit = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtActive = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.PicPhoto = New System.Windows.Forms.PictureBox
        Me.GroupBox4.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        CType(Me.PicPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(872, 41)
        Me.Topctrl1.TabIndex = 14
        Me.Topctrl1.tAdd = True
        Me.Topctrl1.tCancel = True
        Me.Topctrl1.tDel = True
        Me.Topctrl1.tDiscard = False
        Me.Topctrl1.tEdit = True
        Me.Topctrl1.tExit = True
        Me.Topctrl1.tFind = True
        Me.Topctrl1.tFirst = True
        Me.Topctrl1.tLast = True
        Me.Topctrl1.tNext = True
        Me.Topctrl1.tPrev = True
        Me.Topctrl1.tPrn = True
        Me.Topctrl1.tRef = True
        Me.Topctrl1.tSave = False
        Me.Topctrl1.tSite = True
        '
        'CboName
        '
        Me.CboName.AgCmboMaster = True
        Me.CboName.AgMandatory = True
        Me.CboName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboName.FormattingEnabled = True
        Me.CboName.Location = New System.Drawing.Point(226, 128)
        Me.CboName.MaxLength = 50
        Me.CboName.Name = "CboName"
        Me.CboName.Size = New System.Drawing.Size(379, 21)
        Me.CboName.TabIndex = 3
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(125, 132)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(66, 13)
        Me.LblName.TabIndex = 0
        Me.LblName.Text = "Site Name"
        '
        'LblNameReq
        '
        Me.LblNameReq.AutoSize = True
        Me.LblNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNameReq.Location = New System.Drawing.Point(210, 135)
        Me.LblNameReq.Name = "LblNameReq"
        Me.LblNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblNameReq.TabIndex = 0
        Me.LblNameReq.Text = "Ä"
        '
        'TxtHO_YN
        '
        Me.TxtHO_YN.AgMandatory = True
        Me.TxtHO_YN.AgMasterHelp = False
        Me.TxtHO_YN.AgNumberLeftPlaces = 0
        Me.TxtHO_YN.AgNumberNegetiveAllow = False
        Me.TxtHO_YN.AgNumberRightPlaces = 0
        Me.TxtHO_YN.AgPickFromLastValue = False
        Me.TxtHO_YN.AgRowFilter = ""
        Me.TxtHO_YN.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtHO_YN.AgSelectedValue = Nothing
        Me.TxtHO_YN.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtHO_YN.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtHO_YN.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHO_YN.Location = New System.Drawing.Point(505, 84)
        Me.TxtHO_YN.MaxLength = 1
        Me.TxtHO_YN.Name = "TxtHO_YN"
        Me.TxtHO_YN.Size = New System.Drawing.Size(100, 21)
        Me.TxtHO_YN.TabIndex = 1
        '
        'LblHO_YN
        '
        Me.LblHO_YN.AutoSize = True
        Me.LblHO_YN.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHO_YN.Location = New System.Drawing.Point(347, 88)
        Me.LblHO_YN.Name = "LblHO_YN"
        Me.LblHO_YN.Size = New System.Drawing.Size(147, 13)
        Me.LblHO_YN.TabIndex = 0
        Me.LblHO_YN.Text = "Head Office [(Y)es/(N)o]"
        '
        'LblHO_YNReq
        '
        Me.LblHO_YNReq.AutoSize = True
        Me.LblHO_YNReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblHO_YNReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblHO_YNReq.Location = New System.Drawing.Point(493, 91)
        Me.LblHO_YNReq.Name = "LblHO_YNReq"
        Me.LblHO_YNReq.Size = New System.Drawing.Size(10, 7)
        Me.LblHO_YNReq.TabIndex = 0
        Me.LblHO_YNReq.Text = "Ä"
        '
        'TxtAdd1
        '
        Me.TxtAdd1.AgMandatory = False
        Me.TxtAdd1.AgMasterHelp = False
        Me.TxtAdd1.AgNumberLeftPlaces = 0
        Me.TxtAdd1.AgNumberNegetiveAllow = False
        Me.TxtAdd1.AgNumberRightPlaces = 0
        Me.TxtAdd1.AgPickFromLastValue = False
        Me.TxtAdd1.AgRowFilter = ""
        Me.TxtAdd1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdd1.AgSelectedValue = Nothing
        Me.TxtAdd1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd1.Location = New System.Drawing.Point(226, 150)
        Me.TxtAdd1.MaxLength = 50
        Me.TxtAdd1.Name = "TxtAdd1"
        Me.TxtAdd1.Size = New System.Drawing.Size(379, 21)
        Me.TxtAdd1.TabIndex = 4
        '
        'LblAdd1
        '
        Me.LblAdd1.AutoSize = True
        Me.LblAdd1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAdd1.Location = New System.Drawing.Point(125, 154)
        Me.LblAdd1.Name = "LblAdd1"
        Me.LblAdd1.Size = New System.Drawing.Size(53, 13)
        Me.LblAdd1.TabIndex = 0
        Me.LblAdd1.Text = "Address"
        '
        'TxtAdd2
        '
        Me.TxtAdd2.AgMandatory = False
        Me.TxtAdd2.AgMasterHelp = False
        Me.TxtAdd2.AgNumberLeftPlaces = 0
        Me.TxtAdd2.AgNumberNegetiveAllow = False
        Me.TxtAdd2.AgNumberRightPlaces = 0
        Me.TxtAdd2.AgPickFromLastValue = False
        Me.TxtAdd2.AgRowFilter = ""
        Me.TxtAdd2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdd2.AgSelectedValue = Nothing
        Me.TxtAdd2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd2.Location = New System.Drawing.Point(226, 172)
        Me.TxtAdd2.MaxLength = 50
        Me.TxtAdd2.Name = "TxtAdd2"
        Me.TxtAdd2.Size = New System.Drawing.Size(379, 21)
        Me.TxtAdd2.TabIndex = 5
        '
        'TxtAdd3
        '
        Me.TxtAdd3.AgMandatory = False
        Me.TxtAdd3.AgMasterHelp = False
        Me.TxtAdd3.AgNumberLeftPlaces = 0
        Me.TxtAdd3.AgNumberNegetiveAllow = False
        Me.TxtAdd3.AgNumberRightPlaces = 0
        Me.TxtAdd3.AgPickFromLastValue = False
        Me.TxtAdd3.AgRowFilter = ""
        Me.TxtAdd3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdd3.AgSelectedValue = Nothing
        Me.TxtAdd3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd3.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd3.Location = New System.Drawing.Point(226, 194)
        Me.TxtAdd3.MaxLength = 50
        Me.TxtAdd3.Name = "TxtAdd3"
        Me.TxtAdd3.Size = New System.Drawing.Size(379, 21)
        Me.TxtAdd3.TabIndex = 6
        '
        'CboCity_Code
        '
        Me.CboCity_Code.AgCmboMaster = False
        Me.CboCity_Code.AgMandatory = False
        Me.CboCity_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboCity_Code.FormattingEnabled = True
        Me.CboCity_Code.Location = New System.Drawing.Point(226, 216)
        Me.CboCity_Code.Name = "CboCity_Code"
        Me.CboCity_Code.Size = New System.Drawing.Size(246, 21)
        Me.CboCity_Code.TabIndex = 7
        '
        'LblCity_Code
        '
        Me.LblCity_Code.AutoSize = True
        Me.LblCity_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity_Code.Location = New System.Drawing.Point(125, 220)
        Me.LblCity_Code.Name = "LblCity_Code"
        Me.LblCity_Code.Size = New System.Drawing.Size(30, 13)
        Me.LblCity_Code.TabIndex = 0
        Me.LblCity_Code.Text = "City"
        '
        'TxtPinNo
        '
        Me.TxtPinNo.AgMandatory = False
        Me.TxtPinNo.AgMasterHelp = False
        Me.TxtPinNo.AgNumberLeftPlaces = 0
        Me.TxtPinNo.AgNumberNegetiveAllow = False
        Me.TxtPinNo.AgNumberRightPlaces = 0
        Me.TxtPinNo.AgPickFromLastValue = False
        Me.TxtPinNo.AgRowFilter = ""
        Me.TxtPinNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPinNo.AgSelectedValue = Nothing
        Me.TxtPinNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPinNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPinNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPinNo.Location = New System.Drawing.Point(505, 216)
        Me.TxtPinNo.MaxLength = 15
        Me.TxtPinNo.Name = "TxtPinNo"
        Me.TxtPinNo.Size = New System.Drawing.Size(100, 21)
        Me.TxtPinNo.TabIndex = 8
        Me.TxtPinNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblPinNo
        '
        Me.LblPinNo.AutoSize = True
        Me.LblPinNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPinNo.Location = New System.Drawing.Point(478, 220)
        Me.LblPinNo.Name = "LblPinNo"
        Me.LblPinNo.Size = New System.Drawing.Size(24, 13)
        Me.LblPinNo.TabIndex = 0
        Me.LblPinNo.Text = "Pin"
        '
        'TxtPhone
        '
        Me.TxtPhone.AgMandatory = False
        Me.TxtPhone.AgMasterHelp = False
        Me.TxtPhone.AgNumberLeftPlaces = 0
        Me.TxtPhone.AgNumberNegetiveAllow = False
        Me.TxtPhone.AgNumberRightPlaces = 0
        Me.TxtPhone.AgPickFromLastValue = False
        Me.TxtPhone.AgRowFilter = ""
        Me.TxtPhone.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPhone.AgSelectedValue = Nothing
        Me.TxtPhone.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPhone.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPhone.Location = New System.Drawing.Point(226, 238)
        Me.TxtPhone.MaxLength = 50
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(379, 21)
        Me.TxtPhone.TabIndex = 9
        '
        'LblPhone
        '
        Me.LblPhone.AutoSize = True
        Me.LblPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPhone.Location = New System.Drawing.Point(125, 242)
        Me.LblPhone.Name = "LblPhone"
        Me.LblPhone.Size = New System.Drawing.Size(65, 13)
        Me.LblPhone.TabIndex = 0
        Me.LblPhone.Text = "Phone No."
        '
        'TxtMobile
        '
        Me.TxtMobile.AgMandatory = False
        Me.TxtMobile.AgMasterHelp = False
        Me.TxtMobile.AgNumberLeftPlaces = 0
        Me.TxtMobile.AgNumberNegetiveAllow = False
        Me.TxtMobile.AgNumberRightPlaces = 0
        Me.TxtMobile.AgPickFromLastValue = False
        Me.TxtMobile.AgRowFilter = ""
        Me.TxtMobile.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMobile.AgSelectedValue = Nothing
        Me.TxtMobile.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMobile.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMobile.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMobile.Location = New System.Drawing.Point(226, 260)
        Me.TxtMobile.MaxLength = 50
        Me.TxtMobile.Name = "TxtMobile"
        Me.TxtMobile.Size = New System.Drawing.Size(379, 21)
        Me.TxtMobile.TabIndex = 10
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(125, 264)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(66, 13)
        Me.LblMobile.TabIndex = 0
        Me.LblMobile.Text = "Mobile No."
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.TxtModified)
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox4.Location = New System.Drawing.Point(676, 408)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(186, 51)
        Me.GroupBox4.TabIndex = 204
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Tag = "TR"
        Me.GroupBox4.Text = "Modified By "
        Me.GroupBox4.Visible = False
        '
        'TxtModified
        '
        Me.TxtModified.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtModified.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtModified.Enabled = False
        Me.TxtModified.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModified.Location = New System.Drawing.Point(15, 21)
        Me.TxtModified.Name = "TxtModified"
        Me.TxtModified.Size = New System.Drawing.Size(158, 18)
        Me.TxtModified.TabIndex = 0
        Me.TxtModified.TabStop = False
        Me.TxtModified.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GrpUP
        '
        Me.GrpUP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpUP.Controls.Add(Me.TxtPrepared)
        Me.GrpUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpUP.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GrpUP.ForeColor = System.Drawing.Color.Maroon
        Me.GrpUP.Location = New System.Drawing.Point(9, 408)
        Me.GrpUP.Name = "GrpUP"
        Me.GrpUP.Size = New System.Drawing.Size(186, 51)
        Me.GrpUP.TabIndex = 203
        Me.GrpUP.TabStop = False
        Me.GrpUP.Tag = "TR"
        Me.GrpUP.Text = "Prepared By "
        '
        'TxtPrepared
        '
        Me.TxtPrepared.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtPrepared.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPrepared.Enabled = False
        Me.TxtPrepared.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrepared.Location = New System.Drawing.Point(15, 21)
        Me.TxtPrepared.Name = "TxtPrepared"
        Me.TxtPrepared.Size = New System.Drawing.Size(158, 18)
        Me.TxtPrepared.TabIndex = 0
        Me.TxtPrepared.TabStop = False
        Me.TxtPrepared.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(3, 394)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(871, 4)
        Me.GroupBox2.TabIndex = 205
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'CboSiteCode
        '
        Me.CboSiteCode.AgCmboMaster = True
        Me.CboSiteCode.AgMandatory = False
        Me.CboSiteCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboSiteCode.FormattingEnabled = True
        Me.CboSiteCode.Location = New System.Drawing.Point(226, 84)
        Me.CboSiteCode.MaxLength = 1
        Me.CboSiteCode.Name = "CboSiteCode"
        Me.CboSiteCode.Size = New System.Drawing.Size(100, 21)
        Me.CboSiteCode.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(125, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 207
        Me.Label1.Text = "Site Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(210, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 206
        Me.Label2.Text = "Ä"
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgMandatory = True
        Me.TxtManualCode.AgMasterHelp = True
        Me.TxtManualCode.AgNumberLeftPlaces = 0
        Me.TxtManualCode.AgNumberNegetiveAllow = False
        Me.TxtManualCode.AgNumberRightPlaces = 0
        Me.TxtManualCode.AgPickFromLastValue = False
        Me.TxtManualCode.AgRowFilter = ""
        Me.TxtManualCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualCode.AgSelectedValue = Nothing
        Me.TxtManualCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualCode.Location = New System.Drawing.Point(226, 106)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(379, 21)
        Me.TxtManualCode.TabIndex = 2
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(125, 110)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(81, 13)
        Me.LblManualCode.TabIndex = 209
        Me.LblManualCode.Text = "Manual Code"
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(210, 113)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 208
        Me.LblManualCodeReq.Text = "Ä"
        '
        'TxtAcCode
        '
        Me.TxtAcCode.AgMandatory = False
        Me.TxtAcCode.AgMasterHelp = False
        Me.TxtAcCode.AgNumberLeftPlaces = 0
        Me.TxtAcCode.AgNumberNegetiveAllow = False
        Me.TxtAcCode.AgNumberRightPlaces = 0
        Me.TxtAcCode.AgPickFromLastValue = False
        Me.TxtAcCode.AgRowFilter = ""
        Me.TxtAcCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcCode.AgSelectedValue = Nothing
        Me.TxtAcCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcCode.Location = New System.Drawing.Point(226, 282)
        Me.TxtAcCode.MaxLength = 50
        Me.TxtAcCode.Name = "TxtAcCode"
        Me.TxtAcCode.Size = New System.Drawing.Size(379, 21)
        Me.TxtAcCode.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(125, 286)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 210
        Me.Label3.Text = "Ledger A/c"
        '
        'TxtCreditLimit
        '
        Me.TxtCreditLimit.AgMandatory = False
        Me.TxtCreditLimit.AgMasterHelp = False
        Me.TxtCreditLimit.AgNumberLeftPlaces = 8
        Me.TxtCreditLimit.AgNumberNegetiveAllow = False
        Me.TxtCreditLimit.AgNumberRightPlaces = 0
        Me.TxtCreditLimit.AgPickFromLastValue = False
        Me.TxtCreditLimit.AgRowFilter = ""
        Me.TxtCreditLimit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCreditLimit.AgSelectedValue = Nothing
        Me.TxtCreditLimit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCreditLimit.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCreditLimit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditLimit.Location = New System.Drawing.Point(226, 304)
        Me.TxtCreditLimit.MaxLength = 50
        Me.TxtCreditLimit.Name = "TxtCreditLimit"
        Me.TxtCreditLimit.Size = New System.Drawing.Size(100, 21)
        Me.TxtCreditLimit.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(125, 308)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.TabIndex = 213
        Me.Label5.Text = "Credit Limit"
        '
        'TxtActive
        '
        Me.TxtActive.AgMandatory = True
        Me.TxtActive.AgMasterHelp = False
        Me.TxtActive.AgNumberLeftPlaces = 0
        Me.TxtActive.AgNumberNegetiveAllow = False
        Me.TxtActive.AgNumberRightPlaces = 0
        Me.TxtActive.AgPickFromLastValue = False
        Me.TxtActive.AgRowFilter = ""
        Me.TxtActive.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtActive.AgSelectedValue = Nothing
        Me.TxtActive.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtActive.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtActive.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtActive.Location = New System.Drawing.Point(505, 304)
        Me.TxtActive.MaxLength = 1
        Me.TxtActive.Name = "TxtActive"
        Me.TxtActive.Size = New System.Drawing.Size(100, 21)
        Me.TxtActive.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(347, 308)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(116, 13)
        Me.Label6.TabIndex = 215
        Me.Label6.Text = "Active [(Y)es/(N)o]"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(493, 311)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 7)
        Me.Label7.TabIndex = 214
        Me.Label7.Text = "Ä"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(704, 170)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(70, 13)
        Me.Label44.TabIndex = 520
        Me.Label44.Text = "Site Image"
        '
        'PicPhoto
        '
        Me.PicPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicPhoto.Location = New System.Drawing.Point(707, 186)
        Me.PicPhoto.Name = "PicPhoto"
        Me.PicPhoto.Size = New System.Drawing.Size(130, 139)
        Me.PicPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicPhoto.TabIndex = 521
        Me.PicPhoto.TabStop = False
        '
        'FrmSiteMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 466)
        Me.Controls.Add(Me.Label44)
        Me.Controls.Add(Me.PicPhoto)
        Me.Controls.Add(Me.TxtActive)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtCreditLimit)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtAcCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.CboSiteCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GrpUP)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.CboName)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.LblNameReq)
        Me.Controls.Add(Me.TxtHO_YN)
        Me.Controls.Add(Me.LblHO_YN)
        Me.Controls.Add(Me.LblHO_YNReq)
        Me.Controls.Add(Me.TxtAdd1)
        Me.Controls.Add(Me.LblAdd1)
        Me.Controls.Add(Me.TxtAdd2)
        Me.Controls.Add(Me.TxtAdd3)
        Me.Controls.Add(Me.CboCity_Code)
        Me.Controls.Add(Me.LblCity_Code)
        Me.Controls.Add(Me.TxtPinNo)
        Me.Controls.Add(Me.LblPinNo)
        Me.Controls.Add(Me.TxtPhone)
        Me.Controls.Add(Me.LblPhone)
        Me.Controls.Add(Me.TxtMobile)
        Me.Controls.Add(Me.LblMobile)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmSiteMaster"
        Me.Text = "Site Master"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        CType(Me.PicPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents CboName As AgControls.AgComboBox
    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents LblNameReq As System.Windows.Forms.Label
    Friend WithEvents TxtHO_YN As AgControls.AgTextBox
    Friend WithEvents LblHO_YN As System.Windows.Forms.Label
    Friend WithEvents LblHO_YNReq As System.Windows.Forms.Label
    Friend WithEvents TxtAdd1 As AgControls.AgTextBox
    Friend WithEvents LblAdd1 As System.Windows.Forms.Label
    Friend WithEvents TxtAdd2 As AgControls.AgTextBox
    Friend WithEvents TxtAdd3 As AgControls.AgTextBox
    Friend WithEvents CboCity_Code As AgControls.AgComboBox
    Friend WithEvents LblCity_Code As System.Windows.Forms.Label
    Friend WithEvents TxtPinNo As AgControls.AgTextBox
    Friend WithEvents LblPinNo As System.Windows.Forms.Label
    Friend WithEvents TxtPhone As AgControls.AgTextBox
    Friend WithEvents LblPhone As System.Windows.Forms.Label
    Friend WithEvents TxtMobile As AgControls.AgTextBox
    Friend WithEvents LblMobile As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtModified As System.Windows.Forms.TextBox
    Friend WithEvents GrpUP As System.Windows.Forms.GroupBox
    Friend WithEvents TxtPrepared As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CboSiteCode As AgControls.AgComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtManualCode As AgControls.AgTextBox
    Friend WithEvents LblManualCode As System.Windows.Forms.Label
    Friend WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Friend WithEvents TxtAcCode As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtCreditLimit As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtActive As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents PicPhoto As System.Windows.Forms.PictureBox
End Class
