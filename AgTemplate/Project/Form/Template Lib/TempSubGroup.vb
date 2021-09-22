Imports System.Data.SQLite
Public Class TempSubGroup
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Protected mGroupNature As String = "", mNature As String = "", mMainTable$ = "", mLogTable$ = ""

    Protected WithEvents LblAcGroupReq As System.Windows.Forms.Label

    Dim mSgMainTable As String
    Dim mSgLogTable As String

    Dim mSgMainLineTableCsv As String
    Dim mSgLogLineTableCsv As String

    Dim mSubGroupNature As ESubgroupNature


    Public Event BaseEvent_Save_SubGroupPreTrans(ByVal SearchCode As String)
    Public Event BaseEvent_Save_SubGroupInTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
    Public Event BaseEvent_Save_SubGroupPostTrans(ByVal SearchCode As String)
    Public Shadows Event BaseEvent_Data_Validation(ByRef passed As Boolean)


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Enum ESubgroupNature
        Customer = 0
        Supplier = 1
    End Enum

    Public Class SubGroupConst
        Public Const GroupNature_Debtors As String = "A"
        Public Const Nature_Debtors As String = "Customer"
        Public Const GroupCode_Debtors As String = "0020"
        Public Const GroupNature_Creditors As String = "L"
        Public Const Nature_Creditors As String = "Supplier"
        Public Const GroupCode_Creditors As String = "0016"
    End Class

    Public Property SubGroupNature() As ESubgroupNature
        Get

        End Get
        Set(ByVal value As ESubgroupNature)

        End Set
    End Property

    Public Property MainTable() As String
        Get
            Return mMainTable
        End Get
        Set(ByVal value As String)
            mMainTable = value
        End Set
    End Property

    Public Property LogTable() As String
        Get
            Return mLogTable
        End Get
        Set(ByVal value As String)
            mLogTable = value
        End Set
    End Property

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.TxtCountry = New AgControls.AgTextBox
        Me.LblCountry = New System.Windows.Forms.Label
        Me.TxtEMail = New AgControls.AgTextBox
        Me.LblEMail = New System.Windows.Forms.Label
        Me.TxtFax = New AgControls.AgTextBox
        Me.LblFax = New System.Windows.Forms.Label
        Me.TxtPhone = New AgControls.AgTextBox
        Me.LblPhone = New System.Windows.Forms.Label
        Me.LblCityReq = New System.Windows.Forms.Label
        Me.TxtCity = New AgControls.AgTextBox
        Me.LblCity = New System.Windows.Forms.Label
        Me.LblAddressReq = New System.Windows.Forms.Label
        Me.TxtAdd2 = New AgControls.AgTextBox
        Me.TxtAdd1 = New AgControls.AgTextBox
        Me.LblAddress = New System.Windows.Forms.Label
        Me.TxtAdd3 = New AgControls.AgTextBox
        Me.LblNameReq = New System.Windows.Forms.Label
        Me.LblManualCodeReq = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.TxtDispName = New AgControls.AgTextBox
        Me.LblName = New System.Windows.Forms.Label
        Me.TxtAcGroup = New AgControls.AgTextBox
        Me.LblAcGroup = New System.Windows.Forms.Label
        Me.LblAcGroupReq = New System.Windows.Forms.Label
        Me.TxtTDSDescription = New AgControls.AgTextBox
        Me.LblTDSDescription = New System.Windows.Forms.Label
        Me.TxtTDSCategory = New AgControls.AgTextBox
        Me.LblTDSCategory = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(907, 41)
        Me.Topctrl1.TabIndex = 32
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 438)
        Me.GroupBox1.Size = New System.Drawing.Size(949, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 442)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 442)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(556, 442)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 23)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(133, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 442)
        Me.GBoxApprove.Size = New System.Drawing.Size(147, 44)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Size = New System.Drawing.Size(89, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(118, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(702, 442)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(271, 442)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'TxtCountry
        '
        Me.TxtCountry.AgMandatory = False
        Me.TxtCountry.AgMasterHelp = False
        Me.TxtCountry.AgNumberLeftPlaces = 0
        Me.TxtCountry.AgNumberNegetiveAllow = False
        Me.TxtCountry.AgNumberRightPlaces = 0
        Me.TxtCountry.AgPickFromLastValue = False
        Me.TxtCountry.AgRowFilter = ""
        Me.TxtCountry.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCountry.AgSelectedValue = Nothing
        Me.TxtCountry.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCountry.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCountry.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCountry.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCountry.Location = New System.Drawing.Point(561, 191)
        Me.TxtCountry.MaxLength = 0
        Me.TxtCountry.Name = "TxtCountry"
        Me.TxtCountry.Size = New System.Drawing.Size(101, 18)
        Me.TxtCountry.TabIndex = 7
        '
        'LblCountry
        '
        Me.LblCountry.AutoSize = True
        Me.LblCountry.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCountry.Location = New System.Drawing.Point(508, 192)
        Me.LblCountry.Name = "LblCountry"
        Me.LblCountry.Size = New System.Drawing.Size(53, 16)
        Me.LblCountry.TabIndex = 804
        Me.LblCountry.Text = "Country"
        '
        'TxtEMail
        '
        Me.TxtEMail.AgMandatory = False
        Me.TxtEMail.AgMasterHelp = False
        Me.TxtEMail.AgNumberLeftPlaces = 0
        Me.TxtEMail.AgNumberNegetiveAllow = False
        Me.TxtEMail.AgNumberRightPlaces = 0
        Me.TxtEMail.AgPickFromLastValue = False
        Me.TxtEMail.AgRowFilter = ""
        Me.TxtEMail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtEMail.AgSelectedValue = Nothing
        Me.TxtEMail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEMail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEMail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtEMail.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEMail.Location = New System.Drawing.Point(370, 251)
        Me.TxtEMail.MaxLength = 100
        Me.TxtEMail.Name = "TxtEMail"
        Me.TxtEMail.Size = New System.Drawing.Size(292, 18)
        Me.TxtEMail.TabIndex = 10
        '
        'LblEMail
        '
        Me.LblEMail.AutoSize = True
        Me.LblEMail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEMail.Location = New System.Drawing.Point(245, 251)
        Me.LblEMail.Name = "LblEMail"
        Me.LblEMail.Size = New System.Drawing.Size(41, 16)
        Me.LblEMail.TabIndex = 799
        Me.LblEMail.Text = "EMail"
        '
        'TxtFax
        '
        Me.TxtFax.AgMandatory = False
        Me.TxtFax.AgMasterHelp = False
        Me.TxtFax.AgNumberLeftPlaces = 0
        Me.TxtFax.AgNumberNegetiveAllow = False
        Me.TxtFax.AgNumberRightPlaces = 0
        Me.TxtFax.AgPickFromLastValue = False
        Me.TxtFax.AgRowFilter = ""
        Me.TxtFax.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFax.AgSelectedValue = Nothing
        Me.TxtFax.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFax.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFax.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(370, 231)
        Me.TxtFax.MaxLength = 35
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(292, 18)
        Me.TxtFax.TabIndex = 9
        '
        'LblFax
        '
        Me.LblFax.AutoSize = True
        Me.LblFax.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFax.Location = New System.Drawing.Point(245, 231)
        Me.LblFax.Name = "LblFax"
        Me.LblFax.Size = New System.Drawing.Size(54, 16)
        Me.LblFax.TabIndex = 796
        Me.LblFax.Text = "Fax No,"
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
        Me.TxtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPhone.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPhone.Location = New System.Drawing.Point(370, 211)
        Me.TxtPhone.MaxLength = 35
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(292, 18)
        Me.TxtPhone.TabIndex = 8
        '
        'LblPhone
        '
        Me.LblPhone.AutoSize = True
        Me.LblPhone.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPhone.Location = New System.Drawing.Point(245, 211)
        Me.LblPhone.Name = "LblPhone"
        Me.LblPhone.Size = New System.Drawing.Size(77, 16)
        Me.LblPhone.TabIndex = 793
        Me.LblPhone.Text = "Contact No."
        '
        'LblCityReq
        '
        Me.LblCityReq.AutoSize = True
        Me.LblCityReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblCityReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCityReq.Location = New System.Drawing.Point(351, 198)
        Me.LblCityReq.Name = "LblCityReq"
        Me.LblCityReq.Size = New System.Drawing.Size(10, 7)
        Me.LblCityReq.TabIndex = 791
        Me.LblCityReq.Text = "Ä"
        '
        'TxtCity
        '
        Me.TxtCity.AgMandatory = True
        Me.TxtCity.AgMasterHelp = False
        Me.TxtCity.AgNumberLeftPlaces = 0
        Me.TxtCity.AgNumberNegetiveAllow = False
        Me.TxtCity.AgNumberRightPlaces = 0
        Me.TxtCity.AgPickFromLastValue = False
        Me.TxtCity.AgRowFilter = ""
        Me.TxtCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCity.AgSelectedValue = Nothing
        Me.TxtCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCity.Location = New System.Drawing.Point(370, 191)
        Me.TxtCity.MaxLength = 0
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(132, 18)
        Me.TxtCity.TabIndex = 6
        '
        'LblCity
        '
        Me.LblCity.AutoSize = True
        Me.LblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity.Location = New System.Drawing.Point(245, 191)
        Me.LblCity.Name = "LblCity"
        Me.LblCity.Size = New System.Drawing.Size(31, 16)
        Me.LblCity.TabIndex = 790
        Me.LblCity.Text = "City"
        '
        'LblAddressReq
        '
        Me.LblAddressReq.AutoSize = True
        Me.LblAddressReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblAddressReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblAddressReq.Location = New System.Drawing.Point(351, 139)
        Me.LblAddressReq.Name = "LblAddressReq"
        Me.LblAddressReq.Size = New System.Drawing.Size(10, 7)
        Me.LblAddressReq.TabIndex = 785
        Me.LblAddressReq.Text = "Ä"
        '
        'TxtAdd2
        '
        Me.TxtAdd2.AgMandatory = False
        Me.TxtAdd2.AgMasterHelp = True
        Me.TxtAdd2.AgNumberLeftPlaces = 8
        Me.TxtAdd2.AgNumberNegetiveAllow = False
        Me.TxtAdd2.AgNumberRightPlaces = 2
        Me.TxtAdd2.AgPickFromLastValue = False
        Me.TxtAdd2.AgRowFilter = ""
        Me.TxtAdd2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdd2.AgSelectedValue = Nothing
        Me.TxtAdd2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAdd2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd2.Location = New System.Drawing.Point(370, 151)
        Me.TxtAdd2.MaxLength = 50
        Me.TxtAdd2.Name = "TxtAdd2"
        Me.TxtAdd2.Size = New System.Drawing.Size(292, 18)
        Me.TxtAdd2.TabIndex = 4
        '
        'TxtAdd1
        '
        Me.TxtAdd1.AgMandatory = True
        Me.TxtAdd1.AgMasterHelp = True
        Me.TxtAdd1.AgNumberLeftPlaces = 8
        Me.TxtAdd1.AgNumberNegetiveAllow = False
        Me.TxtAdd1.AgNumberRightPlaces = 2
        Me.TxtAdd1.AgPickFromLastValue = False
        Me.TxtAdd1.AgRowFilter = ""
        Me.TxtAdd1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdd1.AgSelectedValue = Nothing
        Me.TxtAdd1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAdd1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd1.Location = New System.Drawing.Point(370, 131)
        Me.TxtAdd1.MaxLength = 50
        Me.TxtAdd1.Name = "TxtAdd1"
        Me.TxtAdd1.Size = New System.Drawing.Size(292, 18)
        Me.TxtAdd1.TabIndex = 3
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(245, 131)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(56, 16)
        Me.LblAddress.TabIndex = 784
        Me.LblAddress.Text = "Address"
        '
        'TxtAdd3
        '
        Me.TxtAdd3.AgMandatory = False
        Me.TxtAdd3.AgMasterHelp = True
        Me.TxtAdd3.AgNumberLeftPlaces = 8
        Me.TxtAdd3.AgNumberNegetiveAllow = False
        Me.TxtAdd3.AgNumberRightPlaces = 3
        Me.TxtAdd3.AgPickFromLastValue = False
        Me.TxtAdd3.AgRowFilter = ""
        Me.TxtAdd3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdd3.AgSelectedValue = Nothing
        Me.TxtAdd3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd3.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAdd3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd3.Location = New System.Drawing.Point(370, 171)
        Me.TxtAdd3.MaxLength = 50
        Me.TxtAdd3.Name = "TxtAdd3"
        Me.TxtAdd3.Size = New System.Drawing.Size(292, 18)
        Me.TxtAdd3.TabIndex = 5
        '
        'LblNameReq
        '
        Me.LblNameReq.AutoSize = True
        Me.LblNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNameReq.Location = New System.Drawing.Point(351, 98)
        Me.LblNameReq.Name = "LblNameReq"
        Me.LblNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblNameReq.TabIndex = 781
        Me.LblNameReq.Text = "Ä"
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(351, 78)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 778
        Me.LblManualCodeReq.Text = "Ä"
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
        Me.TxtManualCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualCode.Location = New System.Drawing.Point(370, 71)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(171, 18)
        Me.TxtManualCode.TabIndex = 0
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(245, 71)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(38, 16)
        Me.LblManualCode.TabIndex = 775
        Me.LblManualCode.Text = "Code"
        '
        'TxtDispName
        '
        Me.TxtDispName.AgMandatory = True
        Me.TxtDispName.AgMasterHelp = True
        Me.TxtDispName.AgNumberLeftPlaces = 0
        Me.TxtDispName.AgNumberNegetiveAllow = False
        Me.TxtDispName.AgNumberRightPlaces = 0
        Me.TxtDispName.AgPickFromLastValue = False
        Me.TxtDispName.AgRowFilter = ""
        Me.TxtDispName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDispName.AgSelectedValue = Nothing
        Me.TxtDispName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDispName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDispName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDispName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDispName.Location = New System.Drawing.Point(370, 91)
        Me.TxtDispName.MaxLength = 100
        Me.TxtDispName.Name = "TxtDispName"
        Me.TxtDispName.Size = New System.Drawing.Size(292, 18)
        Me.TxtDispName.TabIndex = 1
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(245, 91)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(42, 16)
        Me.LblName.TabIndex = 777
        Me.LblName.Text = "Name"
        '
        'TxtAcGroup
        '
        Me.TxtAcGroup.AgMandatory = False
        Me.TxtAcGroup.AgMasterHelp = False
        Me.TxtAcGroup.AgNumberLeftPlaces = 0
        Me.TxtAcGroup.AgNumberNegetiveAllow = False
        Me.TxtAcGroup.AgNumberRightPlaces = 0
        Me.TxtAcGroup.AgPickFromLastValue = False
        Me.TxtAcGroup.AgRowFilter = ""
        Me.TxtAcGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcGroup.AgSelectedValue = Nothing
        Me.TxtAcGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcGroup.Location = New System.Drawing.Point(370, 111)
        Me.TxtAcGroup.MaxLength = 100
        Me.TxtAcGroup.Name = "TxtAcGroup"
        Me.TxtAcGroup.Size = New System.Drawing.Size(292, 18)
        Me.TxtAcGroup.TabIndex = 2
        '
        'LblAcGroup
        '
        Me.LblAcGroup.AutoSize = True
        Me.LblAcGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcGroup.Location = New System.Drawing.Point(245, 111)
        Me.LblAcGroup.Name = "LblAcGroup"
        Me.LblAcGroup.Size = New System.Drawing.Size(67, 16)
        Me.LblAcGroup.TabIndex = 860
        Me.LblAcGroup.Text = "A/c Group"
        '
        'LblAcGroupReq
        '
        Me.LblAcGroupReq.AutoSize = True
        Me.LblAcGroupReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblAcGroupReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblAcGroupReq.Location = New System.Drawing.Point(351, 117)
        Me.LblAcGroupReq.Name = "LblAcGroupReq"
        Me.LblAcGroupReq.Size = New System.Drawing.Size(10, 7)
        Me.LblAcGroupReq.TabIndex = 861
        Me.LblAcGroupReq.Text = "Ä"
        '
        'TxtTDSDescription
        '
        Me.TxtTDSDescription.AgMandatory = False
        Me.TxtTDSDescription.AgMasterHelp = False
        Me.TxtTDSDescription.AgNumberLeftPlaces = 8
        Me.TxtTDSDescription.AgNumberNegetiveAllow = False
        Me.TxtTDSDescription.AgNumberRightPlaces = 2
        Me.TxtTDSDescription.AgPickFromLastValue = False
        Me.TxtTDSDescription.AgRowFilter = ""
        Me.TxtTDSDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTDSDescription.AgSelectedValue = Nothing
        Me.TxtTDSDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTDSDescription.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtTDSDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTDSDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTDSDescription.Location = New System.Drawing.Point(370, 399)
        Me.TxtTDSDescription.MaxLength = 100
        Me.TxtTDSDescription.Name = "TxtTDSDescription"
        Me.TxtTDSDescription.Size = New System.Drawing.Size(292, 18)
        Me.TxtTDSDescription.TabIndex = 877
        Me.TxtTDSDescription.Visible = False
        '
        'LblTDSDescription
        '
        Me.LblTDSDescription.AutoSize = True
        Me.LblTDSDescription.BackColor = System.Drawing.Color.Transparent
        Me.LblTDSDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTDSDescription.Location = New System.Drawing.Point(228, 401)
        Me.LblTDSDescription.Name = "LblTDSDescription"
        Me.LblTDSDescription.Size = New System.Drawing.Size(102, 16)
        Me.LblTDSDescription.TabIndex = 879
        Me.LblTDSDescription.Text = "TDS Description"
        Me.LblTDSDescription.Visible = False
        '
        'TxtTDSCategory
        '
        Me.TxtTDSCategory.AgMandatory = False
        Me.TxtTDSCategory.AgMasterHelp = False
        Me.TxtTDSCategory.AgNumberLeftPlaces = 8
        Me.TxtTDSCategory.AgNumberNegetiveAllow = False
        Me.TxtTDSCategory.AgNumberRightPlaces = 2
        Me.TxtTDSCategory.AgPickFromLastValue = False
        Me.TxtTDSCategory.AgRowFilter = ""
        Me.TxtTDSCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTDSCategory.AgSelectedValue = Nothing
        Me.TxtTDSCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTDSCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTDSCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTDSCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTDSCategory.Location = New System.Drawing.Point(370, 379)
        Me.TxtTDSCategory.MaxLength = 100
        Me.TxtTDSCategory.Name = "TxtTDSCategory"
        Me.TxtTDSCategory.Size = New System.Drawing.Size(292, 18)
        Me.TxtTDSCategory.TabIndex = 876
        Me.TxtTDSCategory.Visible = False
        '
        'LblTDSCategory
        '
        Me.LblTDSCategory.AutoSize = True
        Me.LblTDSCategory.BackColor = System.Drawing.Color.Transparent
        Me.LblTDSCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTDSCategory.Location = New System.Drawing.Point(228, 379)
        Me.LblTDSCategory.Name = "LblTDSCategory"
        Me.LblTDSCategory.Size = New System.Drawing.Size(89, 16)
        Me.LblTDSCategory.TabIndex = 878
        Me.LblTDSCategory.Text = "TDS Category"
        Me.LblTDSCategory.Visible = False
        '
        'TempSubGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(907, 486)
        Me.Controls.Add(Me.TxtTDSDescription)
        Me.Controls.Add(Me.LblTDSDescription)
        Me.Controls.Add(Me.TxtTDSCategory)
        Me.Controls.Add(Me.LblTDSCategory)
        Me.Controls.Add(Me.LblAcGroupReq)
        Me.Controls.Add(Me.TxtAcGroup)
        Me.Controls.Add(Me.LblAcGroup)
        Me.Controls.Add(Me.TxtCountry)
        Me.Controls.Add(Me.LblCountry)
        Me.Controls.Add(Me.TxtEMail)
        Me.Controls.Add(Me.LblEMail)
        Me.Controls.Add(Me.TxtFax)
        Me.Controls.Add(Me.LblFax)
        Me.Controls.Add(Me.TxtPhone)
        Me.Controls.Add(Me.LblPhone)
        Me.Controls.Add(Me.LblCityReq)
        Me.Controls.Add(Me.TxtCity)
        Me.Controls.Add(Me.LblCity)
        Me.Controls.Add(Me.LblAddressReq)
        Me.Controls.Add(Me.TxtAdd2)
        Me.Controls.Add(Me.TxtAdd1)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtAdd3)
        Me.Controls.Add(Me.LblNameReq)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtDispName)
        Me.Controls.Add(Me.LblName)
        Me.Name = "TempSubGroup"
        Me.Text = "Buyer Master"
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.LblName, 0)
        Me.Controls.SetChildIndex(Me.TxtDispName, 0)
        Me.Controls.SetChildIndex(Me.LblManualCode, 0)
        Me.Controls.SetChildIndex(Me.TxtManualCode, 0)
        Me.Controls.SetChildIndex(Me.LblManualCodeReq, 0)
        Me.Controls.SetChildIndex(Me.LblNameReq, 0)
        Me.Controls.SetChildIndex(Me.TxtAdd3, 0)
        Me.Controls.SetChildIndex(Me.LblAddress, 0)
        Me.Controls.SetChildIndex(Me.TxtAdd1, 0)
        Me.Controls.SetChildIndex(Me.TxtAdd2, 0)
        Me.Controls.SetChildIndex(Me.LblAddressReq, 0)
        Me.Controls.SetChildIndex(Me.LblCity, 0)
        Me.Controls.SetChildIndex(Me.TxtCity, 0)
        Me.Controls.SetChildIndex(Me.LblCityReq, 0)
        Me.Controls.SetChildIndex(Me.LblPhone, 0)
        Me.Controls.SetChildIndex(Me.TxtPhone, 0)
        Me.Controls.SetChildIndex(Me.LblFax, 0)
        Me.Controls.SetChildIndex(Me.TxtFax, 0)
        Me.Controls.SetChildIndex(Me.LblEMail, 0)
        Me.Controls.SetChildIndex(Me.TxtEMail, 0)
        Me.Controls.SetChildIndex(Me.LblCountry, 0)
        Me.Controls.SetChildIndex(Me.TxtCountry, 0)
        Me.Controls.SetChildIndex(Me.LblAcGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtAcGroup, 0)
        Me.Controls.SetChildIndex(Me.LblAcGroupReq, 0)
        Me.Controls.SetChildIndex(Me.LblTDSCategory, 0)
        Me.Controls.SetChildIndex(Me.TxtTDSCategory, 0)
        Me.Controls.SetChildIndex(Me.LblTDSDescription, 0)
        Me.Controls.SetChildIndex(Me.TxtTDSDescription, 0)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected WithEvents LblName As System.Windows.Forms.Label
    Protected WithEvents TxtDispName As AgControls.AgTextBox
    Protected WithEvents LblManualCode As System.Windows.Forms.Label
    Protected WithEvents TxtManualCode As AgControls.AgTextBox
    Protected WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Protected WithEvents LblNameReq As System.Windows.Forms.Label
    Protected WithEvents TxtAdd3 As AgControls.AgTextBox
    Protected WithEvents LblAddress As System.Windows.Forms.Label
    Protected WithEvents TxtAdd1 As AgControls.AgTextBox
    Protected WithEvents TxtAdd2 As AgControls.AgTextBox
    Protected WithEvents LblAddressReq As System.Windows.Forms.Label
    Protected WithEvents LblCity As System.Windows.Forms.Label
    Protected WithEvents TxtCity As AgControls.AgTextBox
    Protected WithEvents LblCityReq As System.Windows.Forms.Label
    Protected WithEvents LblPhone As System.Windows.Forms.Label
    Protected WithEvents TxtPhone As AgControls.AgTextBox
    Protected WithEvents LblFax As System.Windows.Forms.Label
    Protected WithEvents TxtFax As AgControls.AgTextBox
    Protected WithEvents LblEMail As System.Windows.Forms.Label
    Protected WithEvents TxtEMail As AgControls.AgTextBox
    Protected WithEvents LblCountry As System.Windows.Forms.Label
    Protected WithEvents TxtAcGroup As AgControls.AgTextBox
    Protected WithEvents LblAcGroup As System.Windows.Forms.Label
    Protected WithEvents TxtCountry As AgControls.AgTextBox
    Protected WithEvents TxtTDSDescription As AgControls.AgTextBox
    Protected WithEvents LblTDSDescription As System.Windows.Forms.Label
    Protected WithEvents TxtTDSCategory As AgControls.AgTextBox
    Protected WithEvents LblTDSCategory As System.Windows.Forms.Label
#End Region


    Private Sub FrmShade_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        'AgL.PubFindQry = "SELECT B.UID, S.ManualCode As Code, S.DispName As Name, " & _
        '                " C.CityName as City, C.State, C.Country, S.Phone, S.Fax " & _
        '                " FROM " & mLogTable & " B " & _
        '                " LEFT JOIN SubGroup_Log S On B.UID = S.UID  " & _
        '                " LEFT JOIN City C On S.CityCode = C.CityCode " & _
        '                " WHERE S.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  "

        AgL.PubFindQry = " SELECT H.UID AS SearchCode,  H.SiteList AS [Site List], H.NamePrefix AS [Name Prefix], H.Name, H.DispName AS [Display Name], " &
                " H.GroupNature AS [GROUP Nature], H.ManualCode AS [Manual Code], H.Nature, H.Add1, H.Add2, H.Add3, H.PIN, H.Phone, H.Mobile, H.FAX,  " &
                " H.EMail, H.CSTNo, H.LSTNo, H.TINNo, H.PAN, H.TDS_Catg AS [TDS Category], H.ActiveYN AS [Active Y/N], H.Party_Type AS [Party Type], " &
                " H.FatherName AS [Father Name], H.HusbandName AS [Husband Name], H.DOB, H.Remark, H.Location,  " &
                " H.ModifiedBy AS [Modified By], H.ApprovedBy AS [Approved By], H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], " &
                " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, " &
                " D.Div_Name AS Division,SM.Name AS [Site Name],AG.GroupName AS [GROUP No], C.CityName AS [City Name] " &
                " FROM  SubGroup_Log H " &
                " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " &
                " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code " &
                " LEFT JOIN AcGroup AG ON AG.GroupCode = H.GroupCode " &
                " LEFT JOIN City C ON C.CityCode = H.CityCode  " &
                " WHERE H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  "

        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        'AgL.PubFindQry = "SELECT B.SubCode, S.ManualCode As Code, S.DispName As Name, " & _
        '                " C.CityName as City, C.State, C.Country, S.Phone, S.Fax  " & _
        '                " FROM " & mMainTable & " B " & _
        '                " LEFT JOIN SubGroup  S On B.SubCode = S.SubCode " & _
        '                " LEFT JOIN CIty C On S.CityCode = C.CityCode " & _
        '                " WHERE IfNull(S.IsDeleted,0)=0 "

        AgL.PubFindQry = " SELECT H.SubCode AS SearchCode,  H.SiteList AS [Site List], H.NamePrefix AS [Name Prefix], H.Name, H.DispName AS [Display Name], " &
                        " H.GroupNature AS [GROUP Nature], H.ManualCode AS [Manual Code], H.Nature, H.Add1, H.Add2, H.Add3, H.PIN, H.Phone, H.Mobile, H.FAX,  " &
                        " H.EMail, H.CSTNo, H.LSTNo, H.TINNo, H.PAN, H.TDS_Catg AS [TDS Category], H.ActiveYN AS [Active Y/N], H.Party_Type AS [Party Type], " &
                        " H.FatherName AS [Father Name], H.HusbandName AS [Husband Name], H.DOB, H.Remark, H.Location,  " &
                        " H.ModifiedBy AS [Modified By], H.ApprovedBy AS [Approved By], H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], " &
                        " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, " &
                        " D.Div_Name AS Division,SM.Name AS [Site Name],AG.GroupName AS [GROUP No], C.CityName AS [City Name] " &
                        " FROM  SubGroup H " &
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " &
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code " &
                        " LEFT JOIN AcGroup AG ON AG.GroupCode = H.GroupCode " &
                        " LEFT JOIN City C ON C.CityCode = H.CityCode  " &
                        " WHERE IfNull(H.IsDeleted,0)=0 "

        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SubGroup"
        LogTableName = "SubGroup_LOG"
        'mSgMainTable = IIf(mSgMainTable <> "", mMainTable, "")
        'mSgLogTable = IIf(mSgLogTable <> "", mLogTable, "")
        mSgMainLineTableCsv = IIf(mSgMainLineTableCsv = "", MainLineTableCsv, mSgMainLineTableCsv)
        mSgLogLineTableCsv = IIf(mSgLogLineTableCsv = "", LogLineTableCsv, mSgLogLineTableCsv)

        MainLineTableCsv = mMainTable & "," & mSgMainLineTableCsv
        LogLineTableCsv = mLogTable & "," & mSgLogLineTableCsv

        PrimaryField = "SubCode"
    End Sub

    Private Sub TempSubGroup_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans

    End Sub

    Private Sub TempSubGroup_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualCode.Focus()
    End Sub

    Private Sub TempSubGroup_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        TxtManualCode.Focus()
    End Sub

    Private Sub FrmQuality1_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtCountry.Enabled = False
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select S.SubCode as Code, S.ManualCode, S.DispName as Name " &
                " From " & mMainTable & " B " &
                " LEFT JOIN SubGroup S On B.SubCode = S.SubCode " &
                " Order By S.ManualCode "
        TxtManualCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select S.SubCode as Code, S.DispName As Name " &
                " From  " & mMainTable & " B " &
                " LEFT JOIN SubGroup S On B.SubCode = S.SubCode " &
                " Order By S.DispName "
        TxtDispName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT C.CityCode AS Code, C.CityName, C.State, C.Country, IfNull(C.Status,'" & ClsMain.EntryStatus.Active & "' ) as Status, IfNull(C.IsDeleted,0) as IsDeleted  " &
                " FROM City C  "
        TxtCity.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT A.GroupCode AS Code, A.GroupName AS Name, A.GroupNature , A.Nature  " &
                  " FROM AcGroup A " &
                  " WHERE LEFT(MainGrCode," & AgLibrary.ClsConstant.MainGRLenSundryCreditors & ") in ('" & AgLibrary.ClsConstant.MainGRCodeSundryCreditors & "' , '" & AgLibrary.ClsConstant.MainGRCodeSundryDebtors & "') " &
                  " AND MainGrLen >= " & AgLibrary.ClsConstant.MainGRLenSundryCreditors & " " &
                  " AND AliasYn = 'N'"
        TxtAcGroup.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code , Name From TdsCat_Description " &
            "  Order By Name"
        TxtTDSDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code , Name From TdsCat " &
            "  Order By Name"
        TxtTDSCategory.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select B.SubCode As SearchCode " &
            " From " & mMainTable & " B " &
            " LEFT JOIN SubGroup S ON B.SubCode = S.SubCode " &
            " WHERE S.Site_Code = '" & AgL.PubSiteCode & "' and  IfNull(S.IsDeleted,0)=0 "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmShade_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        mQry = "Select B.UID As SearchCode " &
               " From " & mLogTable & " B " &
               " LEFT JOIN SubGroup_Log S On B.UID = S.UID " &
               " WHERE  S.Site_Code = '" & AgL.PubSiteCode & "' and S.EntryStatus = '" & LogStatus.LogOpen & "' "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim DrTemp As DataRow() = Nothing

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select S.* " &
                    " From " & mMainTable & " B " &
                    " LEFT JOIN SubGroup S On B.SubCode = S.SubCode " &
                    " Where B.SubCode='" & SearchCode & "'"
        Else
            mQry = "Select S.* " &
                    " From " & mLogTable & " B " &
                    " LEFT JOIN SubGroup_Log  S On B.UID = S.UID " &
                    " Where B.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("SubCode"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDispName.Text = AgL.XNull(.Rows(0)("DispName"))
                TxtAcGroup.AgSelectedValue = AgL.XNull(.Rows(0)("GroupCode"))
                TxtAdd1.Text = AgL.XNull(.Rows(0)("Add1"))
                TxtAdd2.Text = AgL.XNull(.Rows(0)("Add2"))
                TxtAdd3.Text = AgL.XNull(.Rows(0)("Add3"))
                TxtCity.AgSelectedValue = AgL.XNull(.Rows(0)("CityCode"))
                TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                TxtFax.Text = AgL.XNull(.Rows(0)("Fax"))
                TxtEMail.Text = AgL.XNull(.Rows(0)("EMail"))
                TxtTDSCategory.AgSelectedValue = AgL.XNull(.Rows(0)("TDS_Catg"))
                TxtTDSDescription.AgSelectedValue = AgL.XNull(.Rows(0)("TDSCat_Description"))
                mNature = AgL.XNull(.Rows(0)("Nature"))
                mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))

                If TxtCity.Text.ToString.Trim = "" Or TxtCity.AgSelectedValue.Trim = "" Then
                    TxtCountry.Text = ""
                Else
                    If TxtCity.AgHelpDataSet IsNot Nothing Then
                        DrTemp = TxtCity.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtCity.AgSelectedValue) & "")
                        TxtCountry.Text = AgL.XNull(DrTemp(0)("Country"))
                    End If
                End If
            End If
        End With
        Topctrl1.tPrn = False
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub ProcSave()
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Dim ChildDataPassed As Boolean = True
        Dim bName$ = ""
        Try
            If AgL.PubMoveRecApplicable Then MastPos = BMBMaster.Position

            'For Data Validation
            If AgCL.AgCheckMandatory(Me) = False Then Exit Sub
            If AgL.RequiredField(TxtDispName, LblName.Text) Then Exit Sub


            RaiseEvent BaseEvent_Data_Validation(ChildDataPassed)
            If Not ChildDataPassed Then
                Exit Sub
            End If

            If Topctrl1.Mode = "Add" Then
                mSearchCode = AgL.GetGUID(AgL.GCn).ToString
                mInternalCode = AgL.GetMaxId("SubGroup_Log", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            End If

            If TxtAcGroup.Visible = False Then
                If mSubGroupNature = ESubgroupNature.Customer Then
                    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Debtors
                    mGroupNature = SubGroupConst.GroupNature_Debtors
                    mNature = SubGroupConst.Nature_Debtors
                Else
                    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Creditors
                    mGroupNature = SubGroupConst.GroupNature_Creditors
                    mNature = SubGroupConst.Nature_Creditors
                End If
            End If

            RaiseEvent BaseEvent_Save_SubGroupPreTrans(mSearchCode)


            If AgL.RequiredField(TxtManualCode, LblManualCode.Text) Then Exit Sub

            If Topctrl1.Mode = "Add" Then
                mQry = "Select count(*) From SubGroup Where ManualCode='" & TxtManualCode.Text & "' And IfNull(IsDeleted,0)=0 "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")

                mQry = "Select count(*) From SubGroup_Log Where ManualCode='" & TxtManualCode.Text & "' And EntryStatus='" & ClsMain.LogStatus.LogOpen & "' and IfNull(MoveToLog,'')=''  "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists in Log File")

                'mQry = "Select count(*) From SubGroup_Log Where DispName='" & TxtDispName.Text & "' And EntryStatus='" & ClsMain.LogStatus.LogOpen & "' and IfNull(MoveToLog,'')=''  "
                'If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Name Already Exists in Log File")

            Else
                mQry = "Select count(*) From SubGroup Where ManualCode ='" & TxtManualCode.Text & "' And SubCode<>'" & mInternalCode & "'  And IfNull(IsDeleted,0)=0 "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")

                'mQry = "Select count(*) From SubGroup Where DispName='" & TxtDispName.Text & "' And SubCode<>'" & mInternalCode & "' "
                'If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Name Already Exists")

                mQry = "Select count(*) From SubGroup_Log Where ManualCode='" & TxtManualCode.Text & "' And UID <>'" & mSearchCode & "' And EntryStatus='" & ClsMain.LogStatus.LogOpen & "' and IfNull(MoveToLog,'')=''  "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists in Log File")

                'mQry = "Select count(*) From SubGroup_Log Where DispName='" & TxtDispName.Text & "' And UID <>'" & mSearchCode & "' And EntryStatus='" & ClsMain.LogStatus.LogOpen & "' and IfNull(MoveToLog,'')=''  "
                'If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Name Already Exists in Log File")
            End If
            'End Data Validation








            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            bName = TxtDispName.Text + " {" + TxtManualCode.Text + "}"
            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO SubGroup_Log(UID, SubCode, Site_Code, Name, DispName, " &
                        " GroupCode, GroupNature,	ManualCode,	Nature,	Add1,	Add2,	Add3,	CityCode, " &
                        " Phone, FAX,	EMail, TDS_Catg, TDSCat_Description, " &
                        " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status, " &
                        " U_Name, U_EntDt, U_AE) " &
                        " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " &
                        " '" & AgL.PubSiteCode & "', " & AgL.Chk_Text(bName) & ",	" &
                        " " & AgL.Chk_Text(TxtDispName.Text) & ", " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " &
                        " " & AgL.Chk_Text(mGroupNature) & ", " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                        " " & AgL.Chk_Text(mNature) & ", " & AgL.Chk_Text(TxtAdd1.Text) & ", " &
                        " " & AgL.Chk_Text(TxtAdd2.Text) & ", " & AgL.Chk_Text(TxtAdd3.Text) & ", " &
                        " " & AgL.Chk_Text(TxtCity.AgSelectedValue) & ", " &
                        " " & AgL.Chk_Text(TxtPhone.Text) & ", " &
                        " " & AgL.Chk_Text(TxtFax.Text) & ", " & AgL.Chk_Text(TxtEMail.Text) & ", " &
                        " " & AgL.Chk_Text(TxtTDSCategory.AgSelectedValue) & ", " & AgL.Chk_Text(TxtTDSDescription.AgSelectedValue) & ", " &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                        " " & AgL.Chk_Text(Topctrl1.Mode) & ", " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtStatus.Text) & ", " &
                        " '" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "', 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "INSERT INTO " & mLogTable & "(UID, SubCode) " &
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", '" & mInternalCode & "') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE SubGroup_Log " &
                        " SET " &
                        " Name = " & AgL.Chk_Text(bName) & ", " &
                        " DispName = " & AgL.Chk_Text(TxtDispName.Text) & ", " &
                        " GroupCode = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " &
                        " GroupNature = " & AgL.Chk_Text(mGroupNature) & ", " &
                        " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                        " Nature = " & AgL.Chk_Text(mNature) & ", " &
                        " Add1 = " & AgL.Chk_Text(TxtAdd1.Text) & ", " &
                        " Add2 = " & AgL.Chk_Text(TxtAdd2.Text) & ", " &
                        " Add3 = " & AgL.Chk_Text(TxtAdd3.Text) & ", " &
                        " CityCode = " & AgL.Chk_Text(TxtCity.AgSelectedValue) & ", " &
                        " Phone = " & AgL.Chk_Text(TxtPhone.Text) & ", " &
                        " FAX = " & AgL.Chk_Text(TxtFax.Text) & ", " &
                        " EMail = " & AgL.Chk_Text(TxtEMail.Text) & ", " &
                        " TDS_Catg = " & AgL.Chk_Text(TxtTDSCategory.AgSelectedValue) & ", " &
                        " TDSCat_Description = " & AgL.Chk_Text(TxtTDSDescription.AgSelectedValue) & ", " &
                        " EntryBy = " & AgL.Chk_Text(AgL.PubUserName) & ", " &
                        " EntryDate = " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                        " EntryType = " & AgL.Chk_Text(Topctrl1.Mode) & ", " &
                        " EntryStatus = " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " Div_Code = " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " &
                        " U_AE = 'E', " &
                        " Edit_Date = '" & Format(AgL.PubLoginDate, "Short Date") & "', " &
                        " ModifiedBy = '" & AgL.PubUserName & "' " &
                        " Where UID = " & AgL.Chk_Text(mSearchCode) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            RaiseEvent BaseEvent_Save_SubGroupInTrans(mSearchCode, AgL.GCn, AgL.ECmd)


            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)



            '--------------------------------------------------------------
            'If not using Log System then approve record automatic on each save
            '--------------------------------------------------------------
            If Not LogSystem Then
                Call ProcApporve(AgL.GCn, AgL.ECmd)
            End If
            '--------------------------------------------------------------

            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
            AgL.ETrans.Commit()
            mTrans = False

            RaiseEvent BaseEvent_Save_SubGroupPostTrans(mSearchCode)

            If AgL.PubMoveRecApplicable Then
                FIniMaster(0, 1)
                Topctrl1_tbRef()
            End If

            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                If AgL.PubMoveRecApplicable Then MoveRec()
            End If

        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCity.Enter
        Select Case sender.name
            Case TxtCity.Name
                CType(sender, AgControls.AgTextBox).AgRowFilter = " Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' And IsDeleted=0 "
        End Select
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtCity.Validating, TxtAcGroup.Validating, TxtCity.Validating, TxtCountry.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtCity.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        TxtCountry.Text = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtCountry.Text = AgL.XNull(DrTemp(0)("Country"))
                        End If
                    End If


                Case TxtAcGroup.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        mGroupNature = ""
                        mNature = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = TxtAcGroup.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & "")
                            mGroupNature = AgL.XNull(DrTemp(0)("GroupNature"))
                            mNature = AgL.XNull(DrTemp(0)("Nature"))
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtManualCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtManualCode.TextChanged

    End Sub
    Private Sub LblManualCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblManualCode.Click

    End Sub


End Class
