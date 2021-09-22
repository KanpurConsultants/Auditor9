Public Class FrmItem
    Inherits AgTemplate.TempMaster

    Dim mQry$
    Dim mIsReturnValue As Boolean = False

    Public mItemCode$ = "", mItemName$ = ""
    Public WithEvents TxtProfitMarginPer As AgControls.AgTextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Public Property IsReturnValue() As Boolean
        Get
            IsReturnValue = mIsReturnValue
        End Get
        Set(ByVal value As Boolean)
            mIsReturnValue = value
        End Set
    End Property

#Region "Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmItem))
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.TxtUnit = New AgControls.AgTextBox
        Me.LblUnit = New System.Windows.Forms.Label
        Me.LblManualCodeReq = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.TxtSalesTaxPostingGroup = New AgControls.AgTextBox
        Me.LblSalesTaxPostingGroup = New System.Windows.Forms.Label
        Me.TxtItemGroup = New AgControls.AgTextBox
        Me.LblItemGroup = New System.Windows.Forms.Label
        Me.TxtItemCategory = New AgControls.AgTextBox
        Me.TxtRate = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtManufacturer = New AgControls.AgTextBox
        Me.LblManufacturer = New System.Windows.Forms.Label
        Me.TxtVatCommodityCode = New AgControls.AgTextBox
        Me.LblVatCommodityCode = New System.Windows.Forms.Label
        Me.TxtReorderLevel = New AgControls.AgTextBox
        Me.LblReorderLevel = New System.Windows.Forms.Label
        Me.TxtItemType = New AgControls.AgTextBox
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button
        Me.TxtProfitMarginPer = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(862, 41)
        Me.Topctrl1.TabIndex = 9
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 277)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 281)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 281)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(554, 281)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 281)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 281)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(278, 281)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(318, 102)
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
        Me.TxtDescription.AgMasterHelp = True
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
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(334, 94)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(373, 18)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(211, 95)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(71, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Item Name"
        '
        'TxtUnit
        '
        Me.TxtUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtUnit.AgLastValueTag = Nothing
        Me.TxtUnit.AgLastValueText = Nothing
        Me.TxtUnit.AgMandatory = True
        Me.TxtUnit.AgMasterHelp = False
        Me.TxtUnit.AgNumberLeftPlaces = 0
        Me.TxtUnit.AgNumberNegetiveAllow = False
        Me.TxtUnit.AgNumberRightPlaces = 0
        Me.TxtUnit.AgPickFromLastValue = False
        Me.TxtUnit.AgRowFilter = ""
        Me.TxtUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUnit.AgSelectedValue = Nothing
        Me.TxtUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUnit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUnit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUnit.Location = New System.Drawing.Point(334, 114)
        Me.TxtUnit.MaxLength = 20
        Me.TxtUnit.Name = "TxtUnit"
        Me.TxtUnit.Size = New System.Drawing.Size(129, 18)
        Me.TxtUnit.TabIndex = 2
        '
        'LblUnit
        '
        Me.LblUnit.AutoSize = True
        Me.LblUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUnit.Location = New System.Drawing.Point(211, 114)
        Me.LblUnit.Name = "LblUnit"
        Me.LblUnit.Size = New System.Drawing.Size(31, 16)
        Me.LblUnit.TabIndex = 685
        Me.LblUnit.Text = "Unit"
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(318, 82)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 690
        Me.LblManualCodeReq.Text = "Ä"
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualCode.AgLastValueTag = Nothing
        Me.TxtManualCode.AgLastValueText = Nothing
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
        Me.TxtManualCode.Location = New System.Drawing.Point(334, 74)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(373, 18)
        Me.TxtManualCode.TabIndex = 0
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(211, 75)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(67, 16)
        Me.LblManualCode.TabIndex = 689
        Me.LblManualCode.Text = "Item Code"
        '
        'TxtSalesTaxPostingGroup
        '
        Me.TxtSalesTaxPostingGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxPostingGroup.AgLastValueTag = Nothing
        Me.TxtSalesTaxPostingGroup.AgLastValueText = Nothing
        Me.TxtSalesTaxPostingGroup.AgMandatory = False
        Me.TxtSalesTaxPostingGroup.AgMasterHelp = False
        Me.TxtSalesTaxPostingGroup.AgNumberLeftPlaces = 0
        Me.TxtSalesTaxPostingGroup.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxPostingGroup.AgNumberRightPlaces = 0
        Me.TxtSalesTaxPostingGroup.AgPickFromLastValue = False
        Me.TxtSalesTaxPostingGroup.AgRowFilter = ""
        Me.TxtSalesTaxPostingGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxPostingGroup.AgSelectedValue = Nothing
        Me.TxtSalesTaxPostingGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxPostingGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxPostingGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxPostingGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxPostingGroup.Location = New System.Drawing.Point(334, 174)
        Me.TxtSalesTaxPostingGroup.MaxLength = 20
        Me.TxtSalesTaxPostingGroup.Name = "TxtSalesTaxPostingGroup"
        Me.TxtSalesTaxPostingGroup.Size = New System.Drawing.Size(129, 18)
        Me.TxtSalesTaxPostingGroup.TabIndex = 8
        '
        'LblSalesTaxPostingGroup
        '
        Me.LblSalesTaxPostingGroup.AutoSize = True
        Me.LblSalesTaxPostingGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxPostingGroup.Location = New System.Drawing.Point(211, 175)
        Me.LblSalesTaxPostingGroup.Name = "LblSalesTaxPostingGroup"
        Me.LblSalesTaxPostingGroup.Size = New System.Drawing.Size(105, 16)
        Me.LblSalesTaxPostingGroup.TabIndex = 694
        Me.LblSalesTaxPostingGroup.Text = "Sales Tax Group"
        '
        'TxtItemGroup
        '
        Me.TxtItemGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemGroup.AgLastValueTag = Nothing
        Me.TxtItemGroup.AgLastValueText = Nothing
        Me.TxtItemGroup.AgMandatory = True
        Me.TxtItemGroup.AgMasterHelp = False
        Me.TxtItemGroup.AgNumberLeftPlaces = 0
        Me.TxtItemGroup.AgNumberNegetiveAllow = False
        Me.TxtItemGroup.AgNumberRightPlaces = 0
        Me.TxtItemGroup.AgPickFromLastValue = False
        Me.TxtItemGroup.AgRowFilter = ""
        Me.TxtItemGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemGroup.AgSelectedValue = Nothing
        Me.TxtItemGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemGroup.Location = New System.Drawing.Point(578, 114)
        Me.TxtItemGroup.MaxLength = 20
        Me.TxtItemGroup.Name = "TxtItemGroup"
        Me.TxtItemGroup.Size = New System.Drawing.Size(129, 18)
        Me.TxtItemGroup.TabIndex = 3
        '
        'LblItemGroup
        '
        Me.LblItemGroup.AutoSize = True
        Me.LblItemGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemGroup.Location = New System.Drawing.Point(467, 114)
        Me.LblItemGroup.Name = "LblItemGroup"
        Me.LblItemGroup.Size = New System.Drawing.Size(72, 16)
        Me.LblItemGroup.TabIndex = 697
        Me.LblItemGroup.Text = "Item Group"
        '
        'TxtItemCategory
        '
        Me.TxtItemCategory.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemCategory.AgLastValueTag = Nothing
        Me.TxtItemCategory.AgLastValueText = Nothing
        Me.TxtItemCategory.AgMandatory = False
        Me.TxtItemCategory.AgMasterHelp = True
        Me.TxtItemCategory.AgNumberLeftPlaces = 0
        Me.TxtItemCategory.AgNumberNegetiveAllow = False
        Me.TxtItemCategory.AgNumberRightPlaces = 0
        Me.TxtItemCategory.AgPickFromLastValue = False
        Me.TxtItemCategory.AgRowFilter = ""
        Me.TxtItemCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemCategory.AgSelectedValue = Nothing
        Me.TxtItemCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemCategory.Location = New System.Drawing.Point(26, 211)
        Me.TxtItemCategory.MaxLength = 20
        Me.TxtItemCategory.Name = "TxtItemCategory"
        Me.TxtItemCategory.Size = New System.Drawing.Size(129, 18)
        Me.TxtItemCategory.TabIndex = 702
        Me.TxtItemCategory.Visible = False
        '
        'TxtRate
        '
        Me.TxtRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtRate.AgLastValueTag = Nothing
        Me.TxtRate.AgLastValueText = Nothing
        Me.TxtRate.AgMandatory = False
        Me.TxtRate.AgMasterHelp = False
        Me.TxtRate.AgNumberLeftPlaces = 0
        Me.TxtRate.AgNumberNegetiveAllow = False
        Me.TxtRate.AgNumberRightPlaces = 0
        Me.TxtRate.AgPickFromLastValue = False
        Me.TxtRate.AgRowFilter = ""
        Me.TxtRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRate.AgSelectedValue = Nothing
        Me.TxtRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRate.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(334, 134)
        Me.TxtRate.MaxLength = 20
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(129, 18)
        Me.TxtRate.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(211, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 16)
        Me.Label3.TabIndex = 704
        Me.Label3.Text = "Rate"
        '
        'TxtManufacturer
        '
        Me.TxtManufacturer.AgAllowUserToEnableMasterHelp = False
        Me.TxtManufacturer.AgLastValueTag = Nothing
        Me.TxtManufacturer.AgLastValueText = Nothing
        Me.TxtManufacturer.AgMandatory = False
        Me.TxtManufacturer.AgMasterHelp = False
        Me.TxtManufacturer.AgNumberLeftPlaces = 0
        Me.TxtManufacturer.AgNumberNegetiveAllow = False
        Me.TxtManufacturer.AgNumberRightPlaces = 0
        Me.TxtManufacturer.AgPickFromLastValue = False
        Me.TxtManufacturer.AgRowFilter = ""
        Me.TxtManufacturer.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManufacturer.AgSelectedValue = Nothing
        Me.TxtManufacturer.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManufacturer.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManufacturer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManufacturer.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManufacturer.Location = New System.Drawing.Point(578, 134)
        Me.TxtManufacturer.MaxLength = 20
        Me.TxtManufacturer.Name = "TxtManufacturer"
        Me.TxtManufacturer.Size = New System.Drawing.Size(129, 18)
        Me.TxtManufacturer.TabIndex = 5
        '
        'LblManufacturer
        '
        Me.LblManufacturer.AutoSize = True
        Me.LblManufacturer.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManufacturer.Location = New System.Drawing.Point(467, 135)
        Me.LblManufacturer.Name = "LblManufacturer"
        Me.LblManufacturer.Size = New System.Drawing.Size(83, 16)
        Me.LblManufacturer.TabIndex = 706
        Me.LblManufacturer.Text = "Manufacturer"
        '
        'TxtVatCommodityCode
        '
        Me.TxtVatCommodityCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtVatCommodityCode.AgLastValueTag = Nothing
        Me.TxtVatCommodityCode.AgLastValueText = Nothing
        Me.TxtVatCommodityCode.AgMandatory = False
        Me.TxtVatCommodityCode.AgMasterHelp = False
        Me.TxtVatCommodityCode.AgNumberLeftPlaces = 0
        Me.TxtVatCommodityCode.AgNumberNegetiveAllow = False
        Me.TxtVatCommodityCode.AgNumberRightPlaces = 0
        Me.TxtVatCommodityCode.AgPickFromLastValue = False
        Me.TxtVatCommodityCode.AgRowFilter = ""
        Me.TxtVatCommodityCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVatCommodityCode.AgSelectedValue = Nothing
        Me.TxtVatCommodityCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVatCommodityCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVatCommodityCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVatCommodityCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVatCommodityCode.Location = New System.Drawing.Point(334, 154)
        Me.TxtVatCommodityCode.MaxLength = 20
        Me.TxtVatCommodityCode.Name = "TxtVatCommodityCode"
        Me.TxtVatCommodityCode.Size = New System.Drawing.Size(129, 18)
        Me.TxtVatCommodityCode.TabIndex = 6
        '
        'LblVatCommodityCode
        '
        Me.LblVatCommodityCode.AutoSize = True
        Me.LblVatCommodityCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVatCommodityCode.Location = New System.Drawing.Point(211, 155)
        Me.LblVatCommodityCode.Name = "LblVatCommodityCode"
        Me.LblVatCommodityCode.Size = New System.Drawing.Size(108, 16)
        Me.LblVatCommodityCode.TabIndex = 708
        Me.LblVatCommodityCode.Text = "Commodity Code"
        '
        'TxtReorderLevel
        '
        Me.TxtReorderLevel.AgAllowUserToEnableMasterHelp = False
        Me.TxtReorderLevel.AgLastValueTag = Nothing
        Me.TxtReorderLevel.AgLastValueText = Nothing
        Me.TxtReorderLevel.AgMandatory = False
        Me.TxtReorderLevel.AgMasterHelp = False
        Me.TxtReorderLevel.AgNumberLeftPlaces = 0
        Me.TxtReorderLevel.AgNumberNegetiveAllow = False
        Me.TxtReorderLevel.AgNumberRightPlaces = 0
        Me.TxtReorderLevel.AgPickFromLastValue = False
        Me.TxtReorderLevel.AgRowFilter = ""
        Me.TxtReorderLevel.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReorderLevel.AgSelectedValue = Nothing
        Me.TxtReorderLevel.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReorderLevel.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReorderLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReorderLevel.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReorderLevel.Location = New System.Drawing.Point(578, 154)
        Me.TxtReorderLevel.MaxLength = 20
        Me.TxtReorderLevel.Name = "TxtReorderLevel"
        Me.TxtReorderLevel.Size = New System.Drawing.Size(129, 18)
        Me.TxtReorderLevel.TabIndex = 7
        '
        'LblReorderLevel
        '
        Me.LblReorderLevel.AutoSize = True
        Me.LblReorderLevel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReorderLevel.Location = New System.Drawing.Point(467, 155)
        Me.LblReorderLevel.Name = "LblReorderLevel"
        Me.LblReorderLevel.Size = New System.Drawing.Size(86, 16)
        Me.LblReorderLevel.TabIndex = 710
        Me.LblReorderLevel.Text = "Reorder Level"
        '
        'TxtItemType
        '
        Me.TxtItemType.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemType.AgLastValueTag = Nothing
        Me.TxtItemType.AgLastValueText = Nothing
        Me.TxtItemType.AgMandatory = False
        Me.TxtItemType.AgMasterHelp = True
        Me.TxtItemType.AgNumberLeftPlaces = 0
        Me.TxtItemType.AgNumberNegetiveAllow = False
        Me.TxtItemType.AgNumberRightPlaces = 0
        Me.TxtItemType.AgPickFromLastValue = False
        Me.TxtItemType.AgRowFilter = ""
        Me.TxtItemType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemType.AgSelectedValue = Nothing
        Me.TxtItemType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemType.Location = New System.Drawing.Point(26, 232)
        Me.TxtItemType.MaxLength = 20
        Me.TxtItemType.Name = "TxtItemType"
        Me.TxtItemType.Size = New System.Drawing.Size(129, 18)
        Me.TxtItemType.TabIndex = 711
        Me.TxtItemType.Visible = False
        '
        'GBoxImportFromExcel
        '
        Me.GBoxImportFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxImportFromExcel.BackColor = System.Drawing.Color.Transparent
        Me.GBoxImportFromExcel.Controls.Add(Me.BtnImprtFromExcel)
        Me.GBoxImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxImportFromExcel.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxImportFromExcel.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(753, 105)
        Me.GBoxImportFromExcel.Name = "GBoxImportFromExcel"
        Me.GBoxImportFromExcel.Size = New System.Drawing.Size(99, 47)
        Me.GBoxImportFromExcel.TabIndex = 1014
        Me.GBoxImportFromExcel.TabStop = False
        Me.GBoxImportFromExcel.Tag = "UP"
        Me.GBoxImportFromExcel.Text = "Import From Excel"
        '
        'BtnImprtFromExcel
        '
        Me.BtnImprtFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromExcel.Image = CType(resources.GetObject("BtnImprtFromExcel.Image"), System.Drawing.Image)
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(58, 9)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnImprtFromExcel.TabIndex = 669
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'TxtProfitMarginPer
        '
        Me.TxtProfitMarginPer.AgAllowUserToEnableMasterHelp = False
        Me.TxtProfitMarginPer.AgLastValueTag = Nothing
        Me.TxtProfitMarginPer.AgLastValueText = Nothing
        Me.TxtProfitMarginPer.AgMandatory = False
        Me.TxtProfitMarginPer.AgMasterHelp = False
        Me.TxtProfitMarginPer.AgNumberLeftPlaces = 0
        Me.TxtProfitMarginPer.AgNumberNegetiveAllow = False
        Me.TxtProfitMarginPer.AgNumberRightPlaces = 0
        Me.TxtProfitMarginPer.AgPickFromLastValue = False
        Me.TxtProfitMarginPer.AgRowFilter = ""
        Me.TxtProfitMarginPer.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProfitMarginPer.AgSelectedValue = Nothing
        Me.TxtProfitMarginPer.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProfitMarginPer.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtProfitMarginPer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProfitMarginPer.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProfitMarginPer.Location = New System.Drawing.Point(578, 174)
        Me.TxtProfitMarginPer.MaxLength = 20
        Me.TxtProfitMarginPer.Name = "TxtProfitMarginPer"
        Me.TxtProfitMarginPer.Size = New System.Drawing.Size(129, 18)
        Me.TxtProfitMarginPer.TabIndex = 9
        Me.TxtProfitMarginPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(468, 176)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 16)
        Me.Label4.TabIndex = 1016
        Me.Label4.Text = "Profit Margin %"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(318, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 1017
        Me.Label5.Text = "Ä"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(561, 121)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 1018
        Me.Label6.Text = "Ä"
        '
        'FrmItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 325)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtProfitMarginPer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.Controls.Add(Me.TxtItemType)
        Me.Controls.Add(Me.TxtReorderLevel)
        Me.Controls.Add(Me.LblReorderLevel)
        Me.Controls.Add(Me.TxtVatCommodityCode)
        Me.Controls.Add(Me.LblVatCommodityCode)
        Me.Controls.Add(Me.TxtManufacturer)
        Me.Controls.Add(Me.LblManufacturer)
        Me.Controls.Add(Me.TxtRate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtItemCategory)
        Me.Controls.Add(Me.TxtItemGroup)
        Me.Controls.Add(Me.LblItemGroup)
        Me.Controls.Add(Me.TxtSalesTaxPostingGroup)
        Me.Controls.Add(Me.LblSalesTaxPostingGroup)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtUnit)
        Me.Controls.Add(Me.LblUnit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmItem"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.LblDescription, 0)
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.LblUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtUnit, 0)
        Me.Controls.SetChildIndex(Me.LblManualCode, 0)
        Me.Controls.SetChildIndex(Me.TxtManualCode, 0)
        Me.Controls.SetChildIndex(Me.LblManualCodeReq, 0)
        Me.Controls.SetChildIndex(Me.LblSalesTaxPostingGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxPostingGroup, 0)
        Me.Controls.SetChildIndex(Me.LblItemGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtItemGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtItemCategory, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtRate, 0)
        Me.Controls.SetChildIndex(Me.LblManufacturer, 0)
        Me.Controls.SetChildIndex(Me.TxtManufacturer, 0)
        Me.Controls.SetChildIndex(Me.LblVatCommodityCode, 0)
        Me.Controls.SetChildIndex(Me.TxtVatCommodityCode, 0)
        Me.Controls.SetChildIndex(Me.LblReorderLevel, 0)
        Me.Controls.SetChildIndex(Me.TxtReorderLevel, 0)
        Me.Controls.SetChildIndex(Me.TxtItemType, 0)
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.TxtProfitMarginPer, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
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
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtUnit As AgControls.AgTextBox
    Public WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Public WithEvents TxtManualCode As AgControls.AgTextBox
    Public WithEvents LblManualCode As System.Windows.Forms.Label
    Public WithEvents TxtSalesTaxPostingGroup As AgControls.AgTextBox
    Public WithEvents LblSalesTaxPostingGroup As System.Windows.Forms.Label
    Public WithEvents LblUnit As System.Windows.Forms.Label
    Public WithEvents TxtItemGroup As AgControls.AgTextBox
    Public WithEvents LblItemGroup As System.Windows.Forms.Label
    Public WithEvents TxtItemCategory As AgControls.AgTextBox
    Public WithEvents TxtRate As AgControls.AgTextBox
    Public WithEvents TxtManufacturer As AgControls.AgTextBox
    Public WithEvents LblManufacturer As System.Windows.Forms.Label
    Public WithEvents TxtVatCommodityCode As AgControls.AgTextBox
    Public WithEvents LblVatCommodityCode As System.Windows.Forms.Label
    Public WithEvents TxtReorderLevel As AgControls.AgTextBox
    Public WithEvents LblReorderLevel As System.Windows.Forms.Label
    Public WithEvents TxtItemType As AgControls.AgTextBox
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Public WithEvents Label3 As System.Windows.Forms.Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtManualCode, LblManualCode.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtUnit, LblUnit.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtItemGroup, LblItemGroup.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Item Where ManualCode ='" & TxtManualCode.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Item Where ManualCode ='" & TxtManualCode.Text & "' And Code < >'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mQry = "Select H.Code As SearchCode " & _
                " From Item H " & mConStr & _
                " Order By H.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Public Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code, I.ManualCode as [Item Code], I.Description [Item Description], " & _
                        " I.Unit " & _
                        " FROM Item I " & mConStr
        AgL.PubFindQryOrdBy = "[Item Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        LogTableName = "Item_LOG"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Item " & _
                " SET " & _
                " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                " Unit = " & AgL.Chk_Text(TxtUnit.Text) & ", " & _
                " MeasureUnit = " & AgL.Chk_Text(TxtUnit.Text) & ", " & _
                " Rate = " & Val(TxtRate.Text) & ", " & _
                " ItemGroup = " & AgL.Chk_Text(TxtItemGroup.AgSelectedValue) & ", " & _
                " ItemCategory = " & AgL.Chk_Text(TxtItemCategory.Text) & ", " & _
                " ItemType = " & AgL.Chk_Text(ClsMain.ItemType.FinishedMaterial) & ", " & _
                " Manufacturer = " & AgL.Chk_Text(TxtManufacturer.AgSelectedValue) & ", " & _
                " VatCommodityCode = " & AgL.Chk_Text(TxtVatCommodityCode.AgSelectedValue) & ", " & _
                " ReorderLevel = " & Val(TxtReorderLevel.Text) & ", " & _
                " ProfitMarginPer = " & Val(TxtProfitMarginPer.Text) & ", " & _
                " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxPostingGroup.Text) & " " & _
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        Call FPostRateInRateList(Conn, Cmd)
    End Sub

    Private Sub FPostRateInRateList(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        Dim bRateListCode$ = ""
        bRateListCode = AgL.GetMaxId("RateList", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mQry = " INSERT INTO RateList(Code, WEF, RateType, EntryBy, EntryDate, EntryType, " & _
                " EntryStatus, Status, Div_Code) " & _
                " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " & AgL.Chk_Text(AgL.PubStartDate) & ",	" & _
                " NULL,	'" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " & _
                " '" & Topctrl1.Mode & "', 'Open', '" & AgTemplate.ClsMain.EntryStatus.Active & "', " & _
                " '" & TxtDivision.AgSelectedValue & "')"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " & _
              " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " & _
              " 1, " & AgL.Chk_Text(AgL.PubStartDate) & ", " & _
              " " & AgL.Chk_Text(mSearchCode) & ", " & _
              " NULL, " & Val(TxtRate.Text) & " ) "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType From ItemGroup I "
        TxtItemGroup.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code, ManualCode As ItemCode, Div_Code ,ItemType " & _
                " From Item " & _
                " Order By ManualCode "
        TxtManualCode.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code, Description As Name , Div_Code, ItemType " & _
                " From Item " & _
                " Order By Description"
        TxtDescription.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Code AS Unit FROM Unit "
        TxtUnit.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Description as  Code, Description AS PostingGroupSalesTaxItem FROM PostingGroupSalesTaxItem "
        TxtSalesTaxPostingGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code, H.Description FROM Manufacturer H "
        TxtManufacturer.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code, H.Description, H.SalesTaxPostingGroup FROM VatCommodityCode H "
        TxtVatCommodityCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select I.* " & _
            " From Item I " & _
            " Where I.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtUnit.Text = AgL.XNull(.Rows(0)("Unit"))
                TxtRate.Text = AgL.VNull(.Rows(0)("Rate"))
                TxtItemGroup.AgSelectedValue = AgL.XNull(.Rows(0)("ItemGroup"))
                TxtItemCategory.Text = AgL.XNull(.Rows(0)("ItemCategory"))
                TxtItemType.Text = AgL.XNull(.Rows(0)("ItemType"))
                TxtSalesTaxPostingGroup.Text = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                TxtProfitMarginPer.Text = AgL.VNull(.Rows(0)("ProfitMarginPer"))
                TxtProfitMarginPer.AgLastValueText = TxtProfitMarginPer.Text
                TxtManufacturer.AgSelectedValue = AgL.XNull(.Rows(0)("Manufacturer"))
                TxtVatCommodityCode.AgSelectedValue = AgL.XNull(.Rows(0)("VatCommodityCode"))
                TxtReorderLevel.Text = AgL.VNull(.Rows(0)("ReorderLevel"))

                TxtSalesTaxPostingGroup.AgLastValueTag = TxtSalesTaxPostingGroup.Tag
                TxtSalesTaxPostingGroup.AgLastValueText = TxtSalesTaxPostingGroup.Text

                TxtVatCommodityCode.AgLastValueTag = TxtVatCommodityCode.Tag
                TxtVatCommodityCode.AgLastValueText = TxtVatCommodityCode.Text
            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDescription.Enter, TxtManualCode.Enter
        Try
            Select Case sender.name
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtDescription.Validating, TxtItemGroup.Validating, TxtVatCommodityCode.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtItemGroup.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtItemCategory.Text = AgL.XNull(DrTemp(0)("ItemCategory"))
                            TxtItemType.Text = AgL.XNull(DrTemp(0)("ItemType"))
                        End If
                    Else
                        TxtItemCategory.Text = ""
                        TxtItemType.Text = ""
                    End If

                Case TxtVatCommodityCode.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtSalesTaxPostingGroup.Text = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmYarn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsReturnValue = False Then
            AgL.WinSetting(Me, 353, 868, 0, 0)
        Else
            Topctrl1.FButtonClick(0)
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtManualCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtManualCode.Validating
        If TxtDescription.Text = "" Then TxtDescription.Text = TxtManualCode.Text
    End Sub

    Private Sub FrmItem_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtSalesTaxPostingGroup.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        TxtSalesTaxPostingGroup.Text = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        TxtProfitMarginPer.Text = IIf(TxtProfitMarginPer.AgLastValueText Is Nothing, "", TxtProfitMarginPer.AgLastValueText)

        TxtSalesTaxPostingGroup.Tag = TxtSalesTaxPostingGroup.AgLastValueTag
        TxtSalesTaxPostingGroup.Text = TxtSalesTaxPostingGroup.AgLastValueText

        TxtVatCommodityCode.Tag = TxtVatCommodityCode.AgLastValueTag
        TxtVatCommodityCode.Text = TxtVatCommodityCode.AgLastValueText
    End Sub

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtProfitMarginPer.KeyDown
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Private Sub FrmItem_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'TxtSalesTaxPostingGroup.Enabled = False
        GBoxImportFromExcel.Enabled = True
    End Sub

    Private Sub BtnImprtFromExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprtFromExcel.Click
        Dim DtTemp As DataTable
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, 'Item Code' as [Field Name], 'Text' as [Data Type], 50 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 20 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 20 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Number' as [Data Type], '' as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Reorder Level' as [Field Name], 'Number' as [Data Type], '' as [Length] "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.LblTitle.Text = "Item Master Import"
        ObjFrmImport.Dgl1.DataSource = DtTemp
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)
        For I = 0 To DtTemp.Rows.Count - 1
            Topctrl1.FButtonClick(0)
            TxtManualCode.Text = AgL.XNull(DtTemp.Rows(I)("Item Code"))
            TxtDescription.Text = AgL.XNull(DtTemp.Rows(I)("Item Name"))

            TxtItemGroup.AgSelectedValue = AgL.XNull(AgL.Dman_Execute("Select Default_ItemGroup From Enviro", AgL.GcnRead).ExecuteScalar)

            mQry = " Select ItemCategory, ItemType From ItemGroup Where Code = '" & TxtItemGroup.AgSelectedValue & "'"
            DtMain = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            If DtMain.Rows.Count > 0 Then
                TxtItemCategory.Text = AgL.XNull(DtMain.Rows(0)("ItemCategory"))
                TxtItemType.Text = AgL.XNull(DtMain.Rows(0)("ItemType"))
            End If

            TxtUnit.Text = AgL.XNull(DtTemp.Rows(I)("Unit"))

            If AgL.VNull(AgL.Dman_Execute("Select Count(*) From Unit Where Code = '" & TxtUnit.Text & "'", AgL.GcnRead).ExecuteScalar) = 0 Then
                mQry = " INSERT INTO Unit(Code,IsActive,DecimalPlaces) VALUES(" & AgL.Chk_Text(TxtUnit.Text) & ", 1, 0) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            TxtRate.Text = AgL.VNull(DtTemp.Rows(I)("Rate"))
            TxtReorderLevel.Text = AgL.VNull(DtTemp.Rows(I)("Reorder Level"))

            TxtVatCommodityCode.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultVatCommodityCode"))
            TxtSalesTaxPostingGroup.AgSelectedValue = AgL.XNull(AgL.Dman_Execute(" Select SalesTaxPostingGroup From VatCommodityCode Where Code = '" & TxtVatCommodityCode.AgSelectedValue & "'", AgL.GcnRead).ExecuteScalar)

            Topctrl1.FButtonClick(13)
        Next
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub

    Private Sub FrmFinishedItem_BaseEvent_Save_PostTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        If mIsReturnValue = True Then mItemCode = mSearchCode : mItemName = TxtDescription.Text : Me.Close() : Exit Sub
    End Sub
End Class
