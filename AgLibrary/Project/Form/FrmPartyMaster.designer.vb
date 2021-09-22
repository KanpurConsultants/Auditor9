<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPartyMaster
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
        Me.LblName = New System.Windows.Forms.Label
        Me.LblDispName = New System.Windows.Forms.Label
        Me.TxtAdd1 = New AgControls.AgTextBox
        Me.LblAdd1 = New System.Windows.Forms.Label
        Me.TxtAdd2 = New AgControls.AgTextBox
        Me.TxtAdd3 = New AgControls.AgTextBox
        Me.LblCityCode = New System.Windows.Forms.Label
        Me.TxtPhone = New AgControls.AgTextBox
        Me.LblPhone = New System.Windows.Forms.Label
        Me.LblMobile = New System.Windows.Forms.Label
        Me.LblDispNameReq = New System.Windows.Forms.Label
        Me.LblAgentCode = New System.Windows.Forms.Label
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.LblManualCodeReq = New System.Windows.Forms.Label
        Me.TxtFatherName = New AgControls.AgTextBox
        Me.TxtDob = New AgControls.AgTextBox
        Me.LblToDate = New System.Windows.Forms.Label
        Me.TxtFatherNamePrefix = New AgControls.AgTextBox
        Me.TxtCityCode = New AgControls.AgTextBox
        Me.TxtName = New AgControls.AgTextBox
        Me.TxtDispName = New AgControls.AgTextBox
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.TxtMobile = New AgControls.AgTextBox
        Me.TxtSite_Code = New AgControls.AgTextBox
        Me.LblSite_Code = New System.Windows.Forms.Label
        Me.TxtEMail = New AgControls.AgTextBox
        Me.LblEMail = New System.Windows.Forms.Label
        Me.TxtCommonAc = New AgControls.AgTextBox
        Me.LblCommonAc = New System.Windows.Forms.Label
        Me.LblCommonAcReq = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(872, 41)
        Me.Topctrl1.TabIndex = 27
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
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(166, 154)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(40, 13)
        Me.LblName.TabIndex = 8
        Me.LblName.Text = "Name"
        '
        'LblDispName
        '
        Me.LblDispName.AutoSize = True
        Me.LblDispName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDispName.Location = New System.Drawing.Point(166, 132)
        Me.LblDispName.Name = "LblDispName"
        Me.LblDispName.Size = New System.Drawing.Size(86, 13)
        Me.LblDispName.TabIndex = 6
        Me.LblDispName.Text = "&Display Name"
        '
        'TxtAdd1
        '
        Me.TxtAdd1.AgMandatory = False
        Me.TxtAdd1.AgMasterHelp = False
        Me.TxtAdd1.AgNumberLeftPlaces = 0
        Me.TxtAdd1.AgNumberNegetiveAllow = False
        Me.TxtAdd1.AgNumberRightPlaces = 0
        Me.TxtAdd1.AgPickFromLastValue = False
        Me.TxtAdd1.AgSelectedValue = Nothing
        Me.TxtAdd1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd1.Location = New System.Drawing.Point(325, 194)
        Me.TxtAdd1.MaxLength = 50
        Me.TxtAdd1.Name = "TxtAdd1"
        Me.TxtAdd1.Size = New System.Drawing.Size(325, 21)
        Me.TxtAdd1.TabIndex = 14
        Me.TxtAdd1.Text = "TxtAdd1"
        '
        'LblAdd1
        '
        Me.LblAdd1.AutoSize = True
        Me.LblAdd1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAdd1.Location = New System.Drawing.Point(166, 198)
        Me.LblAdd1.Name = "LblAdd1"
        Me.LblAdd1.Size = New System.Drawing.Size(53, 13)
        Me.LblAdd1.TabIndex = 13
        Me.LblAdd1.Text = "&Address"
        '
        'TxtAdd2
        '
        Me.TxtAdd2.AgMandatory = False
        Me.TxtAdd2.AgMasterHelp = False
        Me.TxtAdd2.AgNumberLeftPlaces = 0
        Me.TxtAdd2.AgNumberNegetiveAllow = False
        Me.TxtAdd2.AgNumberRightPlaces = 0
        Me.TxtAdd2.AgPickFromLastValue = False
        Me.TxtAdd2.AgSelectedValue = Nothing
        Me.TxtAdd2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd2.Location = New System.Drawing.Point(325, 216)
        Me.TxtAdd2.MaxLength = 50
        Me.TxtAdd2.Name = "TxtAdd2"
        Me.TxtAdd2.Size = New System.Drawing.Size(325, 21)
        Me.TxtAdd2.TabIndex = 15
        Me.TxtAdd2.Text = "TxtAdd2"
        '
        'TxtAdd3
        '
        Me.TxtAdd3.AgMandatory = False
        Me.TxtAdd3.AgMasterHelp = False
        Me.TxtAdd3.AgNumberLeftPlaces = 0
        Me.TxtAdd3.AgNumberNegetiveAllow = False
        Me.TxtAdd3.AgNumberRightPlaces = 0
        Me.TxtAdd3.AgPickFromLastValue = False
        Me.TxtAdd3.AgSelectedValue = Nothing
        Me.TxtAdd3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd3.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd3.Location = New System.Drawing.Point(325, 238)
        Me.TxtAdd3.MaxLength = 50
        Me.TxtAdd3.Name = "TxtAdd3"
        Me.TxtAdd3.Size = New System.Drawing.Size(325, 21)
        Me.TxtAdd3.TabIndex = 16
        Me.TxtAdd3.Text = "TxtAdd3"
        '
        'LblCityCode
        '
        Me.LblCityCode.AutoSize = True
        Me.LblCityCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCityCode.Location = New System.Drawing.Point(166, 264)
        Me.LblCityCode.Name = "LblCityCode"
        Me.LblCityCode.Size = New System.Drawing.Size(67, 13)
        Me.LblCityCode.TabIndex = 17
        Me.LblCityCode.Text = "&City Name"
        '
        'TxtPhone
        '
        Me.TxtPhone.AgMandatory = False
        Me.TxtPhone.AgMasterHelp = False
        Me.TxtPhone.AgNumberLeftPlaces = 0
        Me.TxtPhone.AgNumberNegetiveAllow = False
        Me.TxtPhone.AgNumberRightPlaces = 0
        Me.TxtPhone.AgPickFromLastValue = False
        Me.TxtPhone.AgSelectedValue = Nothing
        Me.TxtPhone.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPhone.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPhone.Location = New System.Drawing.Point(325, 282)
        Me.TxtPhone.MaxLength = 35
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(325, 21)
        Me.TxtPhone.TabIndex = 20
        Me.TxtPhone.Text = "TxtPhone"
        '
        'LblPhone
        '
        Me.LblPhone.AutoSize = True
        Me.LblPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPhone.Location = New System.Drawing.Point(166, 286)
        Me.LblPhone.Name = "LblPhone"
        Me.LblPhone.Size = New System.Drawing.Size(65, 13)
        Me.LblPhone.TabIndex = 19
        Me.LblPhone.Text = "&Phone No."
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(166, 308)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(66, 13)
        Me.LblMobile.TabIndex = 21
        Me.LblMobile.Text = "&Mobile No."
        '
        'LblDispNameReq
        '
        Me.LblDispNameReq.AutoSize = True
        Me.LblDispNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDispNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDispNameReq.Location = New System.Drawing.Point(312, 135)
        Me.LblDispNameReq.Name = "LblDispNameReq"
        Me.LblDispNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDispNameReq.TabIndex = 11
        Me.LblDispNameReq.Text = "Ä"
        '
        'LblAgentCode
        '
        Me.LblAgentCode.AutoSize = True
        Me.LblAgentCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAgentCode.Location = New System.Drawing.Point(166, 176)
        Me.LblAgentCode.Name = "LblAgentCode"
        Me.LblAgentCode.Size = New System.Drawing.Size(89, 13)
        Me.LblAgentCode.TabIndex = 10
        Me.LblAgentCode.Text = "&Father's Name"
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(166, 110)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(55, 13)
        Me.LblManualCode.TabIndex = 2
        Me.LblManualCode.Text = "Party &ID"
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(312, 113)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 12
        Me.LblManualCodeReq.Text = "Ä"
        '
        'TxtFatherName
        '
        Me.TxtFatherName.AgMandatory = True
        Me.TxtFatherName.AgMasterHelp = False
        Me.TxtFatherName.AgNumberLeftPlaces = 0
        Me.TxtFatherName.AgNumberNegetiveAllow = False
        Me.TxtFatherName.AgNumberRightPlaces = 0
        Me.TxtFatherName.AgPickFromLastValue = False
        Me.TxtFatherName.AgSelectedValue = Nothing
        Me.TxtFatherName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFatherName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFatherName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFatherName.Location = New System.Drawing.Point(363, 172)
        Me.TxtFatherName.MaxLength = 100
        Me.TxtFatherName.Name = "TxtFatherName"
        Me.TxtFatherName.Size = New System.Drawing.Size(287, 21)
        Me.TxtFatherName.TabIndex = 12
        '
        'TxtDob
        '
        Me.TxtDob.AgMandatory = True
        Me.TxtDob.AgMasterHelp = False
        Me.TxtDob.AgNumberLeftPlaces = 0
        Me.TxtDob.AgNumberNegetiveAllow = False
        Me.TxtDob.AgNumberRightPlaces = 0
        Me.TxtDob.AgPickFromLastValue = False
        Me.TxtDob.AgSelectedValue = Nothing
        Me.TxtDob.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDob.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDob.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDob.Location = New System.Drawing.Point(325, 348)
        Me.TxtDob.Name = "TxtDob"
        Me.TxtDob.Size = New System.Drawing.Size(100, 21)
        Me.TxtDob.TabIndex = 26
        '
        'LblToDate
        '
        Me.LblToDate.AutoSize = True
        Me.LblToDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToDate.Location = New System.Drawing.Point(166, 352)
        Me.LblToDate.Name = "LblToDate"
        Me.LblToDate.Size = New System.Drawing.Size(80, 13)
        Me.LblToDate.TabIndex = 25
        Me.LblToDate.Text = "Date of &Birth"
        '
        'TxtFatherNamePrefix
        '
        Me.TxtFatherNamePrefix.AgMandatory = True
        Me.TxtFatherNamePrefix.AgMasterHelp = True
        Me.TxtFatherNamePrefix.AgNumberLeftPlaces = 0
        Me.TxtFatherNamePrefix.AgNumberNegetiveAllow = False
        Me.TxtFatherNamePrefix.AgNumberRightPlaces = 0
        Me.TxtFatherNamePrefix.AgPickFromLastValue = False
        Me.TxtFatherNamePrefix.AgSelectedValue = Nothing
        Me.TxtFatherNamePrefix.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFatherNamePrefix.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFatherNamePrefix.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFatherNamePrefix.Location = New System.Drawing.Point(325, 172)
        Me.TxtFatherNamePrefix.MaxLength = 10
        Me.TxtFatherNamePrefix.Name = "TxtFatherNamePrefix"
        Me.TxtFatherNamePrefix.Size = New System.Drawing.Size(36, 21)
        Me.TxtFatherNamePrefix.TabIndex = 11
        '
        'TxtCityCode
        '
        Me.TxtCityCode.AgMandatory = True
        Me.TxtCityCode.AgMasterHelp = False
        Me.TxtCityCode.AgNumberLeftPlaces = 0
        Me.TxtCityCode.AgNumberNegetiveAllow = False
        Me.TxtCityCode.AgNumberRightPlaces = 0
        Me.TxtCityCode.AgPickFromLastValue = False
        Me.TxtCityCode.AgSelectedValue = Nothing
        Me.TxtCityCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCityCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCityCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCityCode.Location = New System.Drawing.Point(325, 260)
        Me.TxtCityCode.Name = "TxtCityCode"
        Me.TxtCityCode.Size = New System.Drawing.Size(325, 21)
        Me.TxtCityCode.TabIndex = 18
        '
        'TxtName
        '
        Me.TxtName.AgMandatory = True
        Me.TxtName.AgMasterHelp = False
        Me.TxtName.AgNumberLeftPlaces = 0
        Me.TxtName.AgNumberNegetiveAllow = False
        Me.TxtName.AgNumberRightPlaces = 0
        Me.TxtName.AgPickFromLastValue = False
        Me.TxtName.AgSelectedValue = Nothing
        Me.TxtName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.Location = New System.Drawing.Point(325, 150)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(325, 21)
        Me.TxtName.TabIndex = 9
        '
        'TxtDispName
        '
        Me.TxtDispName.AgMandatory = True
        Me.TxtDispName.AgMasterHelp = True
        Me.TxtDispName.AgNumberLeftPlaces = 0
        Me.TxtDispName.AgNumberNegetiveAllow = False
        Me.TxtDispName.AgNumberRightPlaces = 0
        Me.TxtDispName.AgPickFromLastValue = False
        Me.TxtDispName.AgSelectedValue = Nothing
        Me.TxtDispName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDispName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDispName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDispName.Location = New System.Drawing.Point(325, 128)
        Me.TxtDispName.Name = "TxtDispName"
        Me.TxtDispName.Size = New System.Drawing.Size(325, 21)
        Me.TxtDispName.TabIndex = 7
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgMandatory = True
        Me.TxtManualCode.AgMasterHelp = True
        Me.TxtManualCode.AgNumberLeftPlaces = 0
        Me.TxtManualCode.AgNumberNegetiveAllow = False
        Me.TxtManualCode.AgNumberRightPlaces = 0
        Me.TxtManualCode.AgPickFromLastValue = False
        Me.TxtManualCode.AgSelectedValue = Nothing
        Me.TxtManualCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualCode.Location = New System.Drawing.Point(325, 106)
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(167, 21)
        Me.TxtManualCode.TabIndex = 3
        Me.TxtManualCode.Text = "XXXXXXXXXXXXXXXXXXXX"
        '
        'TxtMobile
        '
        Me.TxtMobile.AgMandatory = False
        Me.TxtMobile.AgMasterHelp = False
        Me.TxtMobile.AgNumberLeftPlaces = 0
        Me.TxtMobile.AgNumberNegetiveAllow = False
        Me.TxtMobile.AgNumberRightPlaces = 0
        Me.TxtMobile.AgPickFromLastValue = False
        Me.TxtMobile.AgSelectedValue = Nothing
        Me.TxtMobile.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMobile.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMobile.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMobile.Location = New System.Drawing.Point(325, 304)
        Me.TxtMobile.MaxLength = 35
        Me.TxtMobile.Name = "TxtMobile"
        Me.TxtMobile.Size = New System.Drawing.Size(325, 21)
        Me.TxtMobile.TabIndex = 22
        Me.TxtMobile.Text = "TxtMobile"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgMandatory = False
        Me.TxtSite_Code.AgMasterHelp = False
        Me.TxtSite_Code.AgNumberLeftPlaces = 0
        Me.TxtSite_Code.AgNumberNegetiveAllow = False
        Me.TxtSite_Code.AgNumberRightPlaces = 0
        Me.TxtSite_Code.AgPickFromLastValue = False
        Me.TxtSite_Code.AgSelectedValue = Nothing
        Me.TxtSite_Code.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSite_Code.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSite_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(325, 84)
        Me.TxtSite_Code.MaxLength = 35
        Me.TxtSite_Code.Name = "TxtSite_Code"
        Me.TxtSite_Code.ReadOnly = True
        Me.TxtSite_Code.Size = New System.Drawing.Size(325, 21)
        Me.TxtSite_Code.TabIndex = 1
        Me.TxtSite_Code.TabStop = False
        '
        'LblSite_Code
        '
        Me.LblSite_Code.AutoSize = True
        Me.LblSite_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(166, 84)
        Me.LblSite_Code.Name = "LblSite_Code"
        Me.LblSite_Code.Size = New System.Drawing.Size(108, 13)
        Me.LblSite_Code.TabIndex = 0
        Me.LblSite_Code.Text = "Site/Branch Code"
        '
        'TxtEMail
        '
        Me.TxtEMail.AgMandatory = False
        Me.TxtEMail.AgMasterHelp = False
        Me.TxtEMail.AgNumberLeftPlaces = 0
        Me.TxtEMail.AgNumberNegetiveAllow = False
        Me.TxtEMail.AgNumberRightPlaces = 0
        Me.TxtEMail.AgPickFromLastValue = False
        Me.TxtEMail.AgSelectedValue = Nothing
        Me.TxtEMail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEMail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEMail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEMail.Location = New System.Drawing.Point(325, 326)
        Me.TxtEMail.MaxLength = 40
        Me.TxtEMail.Name = "TxtEMail"
        Me.TxtEMail.Size = New System.Drawing.Size(325, 21)
        Me.TxtEMail.TabIndex = 24
        '
        'LblEMail
        '
        Me.LblEMail.AutoSize = True
        Me.LblEMail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEMail.Location = New System.Drawing.Point(166, 330)
        Me.LblEMail.Name = "LblEMail"
        Me.LblEMail.Size = New System.Drawing.Size(58, 13)
        Me.LblEMail.TabIndex = 23
        Me.LblEMail.Text = "&EMail ID."
        '
        'TxtCommonAc
        '
        Me.TxtCommonAc.AgMandatory = True
        Me.TxtCommonAc.AgMasterHelp = True
        Me.TxtCommonAc.AgNumberLeftPlaces = 0
        Me.TxtCommonAc.AgNumberNegetiveAllow = False
        Me.TxtCommonAc.AgNumberRightPlaces = 0
        Me.TxtCommonAc.AgPickFromLastValue = False
        Me.TxtCommonAc.AgSelectedValue = Nothing
        Me.TxtCommonAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCommonAc.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtCommonAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCommonAc.Location = New System.Drawing.Point(603, 106)
        Me.TxtCommonAc.Name = "TxtCommonAc"
        Me.TxtCommonAc.Size = New System.Drawing.Size(47, 21)
        Me.TxtCommonAc.TabIndex = 5
        '
        'LblCommonAc
        '
        Me.LblCommonAc.AutoSize = True
        Me.LblCommonAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCommonAc.Location = New System.Drawing.Point(499, 110)
        Me.LblCommonAc.Name = "LblCommonAc"
        Me.LblCommonAc.Size = New System.Drawing.Size(82, 13)
        Me.LblCommonAc.TabIndex = 4
        Me.LblCommonAc.Text = "C&ommon A/c"
        '
        'LblCommonAcReq
        '
        Me.LblCommonAcReq.AutoSize = True
        Me.LblCommonAcReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblCommonAcReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCommonAcReq.Location = New System.Drawing.Point(587, 113)
        Me.LblCommonAcReq.Name = "LblCommonAcReq"
        Me.LblCommonAcReq.Size = New System.Drawing.Size(10, 7)
        Me.LblCommonAcReq.TabIndex = 50
        Me.LblCommonAcReq.Text = "Ä"
        '
        'FrmPartyMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 416)
        Me.Controls.Add(Me.TxtCommonAc)
        Me.Controls.Add(Me.LblCommonAc)
        Me.Controls.Add(Me.LblCommonAcReq)
        Me.Controls.Add(Me.TxtEMail)
        Me.Controls.Add(Me.LblEMail)
        Me.Controls.Add(Me.TxtSite_Code)
        Me.Controls.Add(Me.LblSite_Code)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.TxtDispName)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.TxtCityCode)
        Me.Controls.Add(Me.TxtFatherNamePrefix)
        Me.Controls.Add(Me.TxtDob)
        Me.Controls.Add(Me.LblToDate)
        Me.Controls.Add(Me.TxtFatherName)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.LblDispNameReq)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.LblDispName)
        Me.Controls.Add(Me.LblAgentCode)
        Me.Controls.Add(Me.TxtAdd1)
        Me.Controls.Add(Me.LblAdd1)
        Me.Controls.Add(Me.TxtAdd2)
        Me.Controls.Add(Me.TxtAdd3)
        Me.Controls.Add(Me.LblCityCode)
        Me.Controls.Add(Me.TxtPhone)
        Me.Controls.Add(Me.LblPhone)
        Me.Controls.Add(Me.TxtMobile)
        Me.Controls.Add(Me.LblMobile)
        Me.KeyPreview = True
        Me.Name = "FrmPartyMaster"
        Me.Text = "Party Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents LblDispName As System.Windows.Forms.Label
    Friend WithEvents TxtAdd1 As AgControls.AgTextBox
    Friend WithEvents LblAdd1 As System.Windows.Forms.Label
    Friend WithEvents TxtAdd2 As AgControls.AgTextBox
    Friend WithEvents TxtAdd3 As AgControls.AgTextBox
    Friend WithEvents LblCityCode As System.Windows.Forms.Label
    Friend WithEvents TxtPhone As AgControls.AgTextBox
    Friend WithEvents LblPhone As System.Windows.Forms.Label
    Friend WithEvents LblMobile As System.Windows.Forms.Label
    Friend WithEvents LblDispNameReq As System.Windows.Forms.Label
    Friend WithEvents LblAgentCode As System.Windows.Forms.Label
    Friend WithEvents LblManualCode As System.Windows.Forms.Label
    Friend WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Friend WithEvents TxtFatherName As AgControls.AgTextBox
    Friend WithEvents TxtDob As AgControls.AgTextBox
    Friend WithEvents LblToDate As System.Windows.Forms.Label
    Friend WithEvents TxtFatherNamePrefix As AgControls.AgTextBox
    Friend WithEvents TxtCityCode As AgControls.AgTextBox
    Friend WithEvents TxtName As AgControls.AgTextBox
    Friend WithEvents TxtDispName As AgControls.AgTextBox
    Friend WithEvents TxtManualCode As AgControls.AgTextBox
    Friend WithEvents TxtMobile As AgControls.AgTextBox
    Friend WithEvents TxtSite_Code As AgControls.AgTextBox
    Friend WithEvents LblSite_Code As System.Windows.Forms.Label
    Friend WithEvents TxtEMail As AgControls.AgTextBox
    Friend WithEvents LblEMail As System.Windows.Forms.Label
    Friend WithEvents TxtCommonAc As AgControls.AgTextBox
    Friend WithEvents LblCommonAc As System.Windows.Forms.Label
    Friend WithEvents LblCommonAcReq As System.Windows.Forms.Label
End Class
