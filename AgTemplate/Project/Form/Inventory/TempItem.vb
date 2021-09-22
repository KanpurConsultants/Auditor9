Imports System.Data.SQLite
Public Class TempItem
    Inherits AgTemplate.TempMaster

    Public Const ColSNo As Byte = 0
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Buyer As Byte = 1
    Public Const Col1BuyerSku As Byte = 2
    Public Const Col1BuyerUpcCode As Byte = 3
    Public WithEvents TxtItemCategory As AgControls.AgTextBox
    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.TxtUnit = New AgControls.AgTextBox
        Me.LblUnit = New System.Windows.Forms.Label
        Me.LblManualCodeReq = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.TxtUPCCode = New AgControls.AgTextBox
        Me.LblUPCCode = New System.Windows.Forms.Label
        Me.TxtSalesTaxPostingGroup = New AgControls.AgTextBox
        Me.LblSalesTaxPostingGroup = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtItemGroup = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtExciseGroup = New AgControls.AgTextBox
        Me.LblExciseGroup = New System.Windows.Forms.Label
        Me.TxtEntryTaxGroup = New AgControls.AgTextBox
        Me.LblEntryTaxGroup = New System.Windows.Forms.Label
        Me.TxtItemCategory = New AgControls.AgTextBox
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
        Me.Topctrl1.Size = New System.Drawing.Size(862, 41)
        Me.Topctrl1.TabIndex = 9
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 390)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 394)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 394)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(554, 394)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 23)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(133, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 394)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Tag = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(704, 394)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(278, 394)
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
        Me.TxtDescription.Size = New System.Drawing.Size(385, 18)
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
        Me.TxtUnit.AgMandatory = False
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
        Me.TxtManualCode.Size = New System.Drawing.Size(131, 18)
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
        'TxtUPCCode
        '
        Me.TxtUPCCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtUPCCode.AgMandatory = False
        Me.TxtUPCCode.AgMasterHelp = True
        Me.TxtUPCCode.AgNumberLeftPlaces = 0
        Me.TxtUPCCode.AgNumberNegetiveAllow = False
        Me.TxtUPCCode.AgNumberRightPlaces = 0
        Me.TxtUPCCode.AgPickFromLastValue = False
        Me.TxtUPCCode.AgRowFilter = ""
        Me.TxtUPCCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUPCCode.AgSelectedValue = Nothing
        Me.TxtUPCCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUPCCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUPCCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUPCCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUPCCode.Location = New System.Drawing.Point(590, 114)
        Me.TxtUPCCode.MaxLength = 20
        Me.TxtUPCCode.Name = "TxtUPCCode"
        Me.TxtUPCCode.Size = New System.Drawing.Size(129, 18)
        Me.TxtUPCCode.TabIndex = 3
        '
        'LblUPCCode
        '
        Me.LblUPCCode.AutoSize = True
        Me.LblUPCCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUPCCode.Location = New System.Drawing.Point(477, 117)
        Me.LblUPCCode.Name = "LblUPCCode"
        Me.LblUPCCode.Size = New System.Drawing.Size(69, 16)
        Me.LblUPCCode.TabIndex = 692
        Me.LblUPCCode.Text = "UPC Code"
        '
        'TxtSalesTaxPostingGroup
        '
        Me.TxtSalesTaxPostingGroup.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtSalesTaxPostingGroup.Location = New System.Drawing.Point(590, 134)
        Me.TxtSalesTaxPostingGroup.MaxLength = 20
        Me.TxtSalesTaxPostingGroup.Name = "TxtSalesTaxPostingGroup"
        Me.TxtSalesTaxPostingGroup.Size = New System.Drawing.Size(129, 18)
        Me.TxtSalesTaxPostingGroup.TabIndex = 5
        '
        'LblSalesTaxPostingGroup
        '
        Me.LblSalesTaxPostingGroup.AutoSize = True
        Me.LblSalesTaxPostingGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxPostingGroup.Location = New System.Drawing.Point(477, 135)
        Me.LblSalesTaxPostingGroup.Name = "LblSalesTaxPostingGroup"
        Me.LblSalesTaxPostingGroup.Size = New System.Drawing.Size(105, 16)
        Me.LblSalesTaxPostingGroup.TabIndex = 694
        Me.LblSalesTaxPostingGroup.Text = "Sales Tax Group"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(214, 193)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(505, 163)
        Me.Pnl1.TabIndex = 8
        Me.Pnl1.Visible = False
        '
        'TxtItemGroup
        '
        Me.TxtItemGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemGroup.AgMandatory = False
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
        Me.TxtItemGroup.Location = New System.Drawing.Point(334, 134)
        Me.TxtItemGroup.MaxLength = 20
        Me.TxtItemGroup.Name = "TxtItemGroup"
        Me.TxtItemGroup.Size = New System.Drawing.Size(129, 18)
        Me.TxtItemGroup.TabIndex = 4
        Me.TxtItemGroup.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(211, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 697
        Me.Label2.Text = "Item Group"
        Me.Label2.Visible = False
        '
        'TxtExciseGroup
        '
        Me.TxtExciseGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtExciseGroup.AgMandatory = False
        Me.TxtExciseGroup.AgMasterHelp = False
        Me.TxtExciseGroup.AgNumberLeftPlaces = 0
        Me.TxtExciseGroup.AgNumberNegetiveAllow = False
        Me.TxtExciseGroup.AgNumberRightPlaces = 0
        Me.TxtExciseGroup.AgPickFromLastValue = False
        Me.TxtExciseGroup.AgRowFilter = ""
        Me.TxtExciseGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExciseGroup.AgSelectedValue = Nothing
        Me.TxtExciseGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExciseGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExciseGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExciseGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExciseGroup.Location = New System.Drawing.Point(334, 154)
        Me.TxtExciseGroup.MaxLength = 20
        Me.TxtExciseGroup.Name = "TxtExciseGroup"
        Me.TxtExciseGroup.Size = New System.Drawing.Size(129, 18)
        Me.TxtExciseGroup.TabIndex = 6
        Me.TxtExciseGroup.Visible = False
        '
        'LblExciseGroup
        '
        Me.LblExciseGroup.AutoSize = True
        Me.LblExciseGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblExciseGroup.Location = New System.Drawing.Point(211, 155)
        Me.LblExciseGroup.Name = "LblExciseGroup"
        Me.LblExciseGroup.Size = New System.Drawing.Size(87, 16)
        Me.LblExciseGroup.TabIndex = 699
        Me.LblExciseGroup.Text = "Excise Group"
        Me.LblExciseGroup.Visible = False
        '
        'TxtEntryTaxGroup
        '
        Me.TxtEntryTaxGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtEntryTaxGroup.AgMandatory = False
        Me.TxtEntryTaxGroup.AgMasterHelp = False
        Me.TxtEntryTaxGroup.AgNumberLeftPlaces = 0
        Me.TxtEntryTaxGroup.AgNumberNegetiveAllow = False
        Me.TxtEntryTaxGroup.AgNumberRightPlaces = 0
        Me.TxtEntryTaxGroup.AgPickFromLastValue = False
        Me.TxtEntryTaxGroup.AgRowFilter = ""
        Me.TxtEntryTaxGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtEntryTaxGroup.AgSelectedValue = Nothing
        Me.TxtEntryTaxGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEntryTaxGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEntryTaxGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtEntryTaxGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntryTaxGroup.Location = New System.Drawing.Point(590, 154)
        Me.TxtEntryTaxGroup.MaxLength = 20
        Me.TxtEntryTaxGroup.Name = "TxtEntryTaxGroup"
        Me.TxtEntryTaxGroup.Size = New System.Drawing.Size(129, 18)
        Me.TxtEntryTaxGroup.TabIndex = 7
        Me.TxtEntryTaxGroup.Visible = False
        '
        'LblEntryTaxGroup
        '
        Me.LblEntryTaxGroup.AutoSize = True
        Me.LblEntryTaxGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEntryTaxGroup.Location = New System.Drawing.Point(477, 155)
        Me.LblEntryTaxGroup.Name = "LblEntryTaxGroup"
        Me.LblEntryTaxGroup.Size = New System.Drawing.Size(103, 16)
        Me.LblEntryTaxGroup.TabIndex = 701
        Me.LblEntryTaxGroup.Text = "Entry Tax Group"
        Me.LblEntryTaxGroup.Visible = False
        '
        'TxtItemCategory
        '
        Me.TxtItemCategory.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtItemCategory.Location = New System.Drawing.Point(29, 302)
        Me.TxtItemCategory.MaxLength = 20
        Me.TxtItemCategory.Name = "TxtItemCategory"
        Me.TxtItemCategory.Size = New System.Drawing.Size(129, 18)
        Me.TxtItemCategory.TabIndex = 702
        Me.TxtItemCategory.Visible = False
        '
        'TempItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 438)
        Me.Controls.Add(Me.TxtItemCategory)
        Me.Controls.Add(Me.TxtEntryTaxGroup)
        Me.Controls.Add(Me.LblEntryTaxGroup)
        Me.Controls.Add(Me.TxtExciseGroup)
        Me.Controls.Add(Me.LblExciseGroup)
        Me.Controls.Add(Me.TxtItemGroup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtSalesTaxPostingGroup)
        Me.Controls.Add(Me.LblSalesTaxPostingGroup)
        Me.Controls.Add(Me.TxtUPCCode)
        Me.Controls.Add(Me.LblUPCCode)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtUnit)
        Me.Controls.Add(Me.LblUnit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "TempItem"
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
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.LblUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtUnit, 0)
        Me.Controls.SetChildIndex(Me.LblManualCode, 0)
        Me.Controls.SetChildIndex(Me.TxtManualCode, 0)
        Me.Controls.SetChildIndex(Me.LblManualCodeReq, 0)
        Me.Controls.SetChildIndex(Me.LblUPCCode, 0)
        Me.Controls.SetChildIndex(Me.TxtUPCCode, 0)
        Me.Controls.SetChildIndex(Me.LblSalesTaxPostingGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxPostingGroup, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.TxtItemGroup, 0)
        Me.Controls.SetChildIndex(Me.LblExciseGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtExciseGroup, 0)
        Me.Controls.SetChildIndex(Me.LblEntryTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtEntryTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtItemCategory, 0)
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

    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtUnit As AgControls.AgTextBox
    Public WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Public WithEvents TxtManualCode As AgControls.AgTextBox
    Public WithEvents LblManualCode As System.Windows.Forms.Label
    Public WithEvents TxtUPCCode As AgControls.AgTextBox
    Public WithEvents LblUPCCode As System.Windows.Forms.Label
    Public WithEvents TxtSalesTaxPostingGroup As AgControls.AgTextBox
    Public WithEvents LblSalesTaxPostingGroup As System.Windows.Forms.Label
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents LblUnit As System.Windows.Forms.Label
    Public WithEvents TxtItemGroup As AgControls.AgTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents TxtExciseGroup As AgControls.AgTextBox
    Public WithEvents LblExciseGroup As System.Windows.Forms.Label
    Public WithEvents TxtEntryTaxGroup As AgControls.AgTextBox
    Public WithEvents LblEntryTaxGroup As System.Windows.Forms.Label
#End Region

    Private Sub TempItem_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Approve_InTrans

    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If TxtDescription.Text.Trim = "" Then Err.Raise(1, , "Item Description Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' And " & ClsMain.RetDivFilterStr & " And IfNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Item Description Already Exist!")

            mQry = "Select count(*) From Item_Log Where Description='" & TxtDescription.Text & "' And " & ClsMain.RetDivFilterStr & "  And IfNull(IsDeleted,0) = 0  And EntryStatus='" & ClsMain.LogStatus.LogOpen & "' and IfNull(MoveToLog,'')=''   "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Item Description Already Exists in Log File")
        Else
            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' And " & ClsMain.RetDivFilterStr & "  And IfNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Item Description Already Exist!")

            mQry = "Select count(*) From Item_Log Where Description = '" & TxtDescription.Text & "' And UID <>'" & mSearchCode & "' And " & ClsMain.RetDivFilterStr & "  And IfNull(IsDeleted,0) = 0  And EntryStatus='" & ClsMain.LogStatus.LogOpen & "' and IfNull(MoveToLog,'')=''  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Item Description Already Exists in Log File")
        End If

        If AgCL.AgIsDuplicate(Dgl1, "" & Col1Buyer & "") Then passed = False : Exit Sub

        Dim I As Integer = 0
        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Buyer, I).Value <> "" Then
                    If .Item(Col1BuyerSku, I).Value = "" And .Item(Col1BuyerUpcCode, I).Value = "" Then
                        Err.Raise(1, , "Buyer Sku And Upc Code Are Blank At Row No.  " & .Item(ColSNo, I).Value & "")
                    End If
                End If
            Next
        End With
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "I.Div_Code") & " "
        AgL.PubFindQry = "SELECT I.UID, I.ManualCode as [Item Code], I.Description [Item Description], " &
                        " I.Unit " &
                        " FROM Item_Log I " & mConStr &
                        " And I.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  "
        AgL.PubFindQryOrdBy = "[Item Description]"
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "I.Div_Code") & " "
        AgL.PubFindQry = "SELECT I.Code, I.ManualCode as [Item Code], I.Description [Item Description], " &
                        " I.Unit " &
                        " FROM Item I " & mConStr &
                        " And IfNull(I.IsDeleted,0)=0 "
        AgL.PubFindQryOrdBy = "[Item Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        LogTableName = "Item_LOG"
        MainLineTableCsv = "ItemBuyer"
        LogLineTableCsv = "ItemBuyer_Log"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Item_Log " &
                " SET " &
                " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " &
                " Unit = " & AgL.Chk_Text(TxtUnit.Text) & ", " &
                " ItemGroup = " & AgL.Chk_Text(TxtItemGroup.AgSelectedValue) & ", " &
                " ItemCategory = " & AgL.Chk_Text(TxtItemCategory.Text) & ", " &
                " UPCCode = " & AgL.Chk_Text(TxtUPCCode.Text) & ", " &
                " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxPostingGroup.Text) & ", " &
                " ExcisePostingGroup = " & AgL.Chk_Text(TxtExciseGroup.Text) & ", " &
                " EntryTaxPostingGroup = " & AgL.Chk_Text(TxtEntryTaxGroup.Text) & " " &
                " Where UID = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete From ItemBuyer_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        Dim I As Integer
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Buyer, I).Value <> "" Then
                    mQry = "INSERT INTO ItemBuyer_Log (UID, Code, Sr, Buyer, BuyerSku, BuyerUpcCode) " &
                           "VALUES (" & AgL.Chk_Text(SearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", " &
                           " " & Val(I) & ", " & AgL.Chk_Text(.AgSelectedValue(Col1Buyer, I)) & ", " &
                           " " & AgL.Chk_Text(.Item(Col1BuyerSku, I).Value) & ",  " &
                           " " & AgL.Chk_Text(.Item(Col1BuyerUpcCode, I).Value) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next
        End With
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory From ItemGroup I "
        TxtItemGroup.AgHelpDataSet(1) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code, ManualCode As ItemCode, Div_Code ,ItemType " &
                " From Item " &
                " Order By ManualCode "
        TxtManualCode.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code, Description As Name , Div_Code, ItemType " &
                " From Item " &
                " Order By Description"
        TxtDescription.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Code AS Unit FROM Unit "
        TxtUnit.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Description as  Code, Description AS PostingGroupSalesTaxItem FROM PostingGroupSalesTaxItem "
        TxtSalesTaxPostingGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Description as  Code, Description AS PostingGroupExciseItem FROM PostingGroupExciseItem "
        TxtExciseGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Description as  Code, Description AS PostingGroupEntryTaxItem FROM PostingGroupEntryTaxItem "
        TxtEntryTaxGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT B.SubCode AS Code, S.DispName AS Name FROM Buyer B  LEFT JOIN SubGroup  S ON B.SubCode = S.SubCode "
        Dgl1.AgHelpDataSet(Col1Buyer) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmQuality1_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        With AgCL
            .AddAgTextColumn(Dgl1, "S.No.", 40, 5, "S.No.", True, True, False)
            .AddAgTextColumn(Dgl1, "Buyer", 270, 0, "Buyer*", True, False, False)
            .AddAgTextColumn(Dgl1, "BuyerSku", 200, 50, "Buyer Sku", True, False, False)
            .AddAgTextColumn(Dgl1, "BuyerUpcCode", 80, 12, "UPC Code", True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 25
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select I.* " &
                " From Item I " &
                " Where I.Code='" & SearchCode & "'"
        Else
            mQry = "Select I.* " &
                " From Item_Log I " &
                " Where I.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtUnit.Text = AgL.XNull(.Rows(0)("Unit"))
                TxtItemGroup.AgSelectedValue = AgL.XNull(.Rows(0)("ItemGroup"))
                TxtItemCategory.AgSelectedValue = AgL.XNull(.Rows(0)("ItemCategory"))
                TxtUPCCode.Text = AgL.XNull(.Rows(0)("UPCCode"))
                TxtSalesTaxPostingGroup.Text = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                TxtExciseGroup.Text = AgL.XNull(.Rows(0)("ExcisePostingGroup"))
                TxtEntryTaxGroup.Text = AgL.XNull(.Rows(0)("EntryTaxPostingGroup"))

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                Dim I As Integer
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from ItemBuyer where Code = '" & SearchCode & "'"
                Else
                    mQry = "Select * from ItemBuyer_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1Buyer, I) = AgL.XNull(.Rows(I)("Buyer"))
                            Dgl1.Item(Col1BuyerSku, I).Value = AgL.XNull(.Rows(I)("BuyerSku"))
                            Dgl1.Item(Col1BuyerUpcCode, I).Value = AgL.XNull(.Rows(I)("BuyerUpcCode"))
                        Next I
                    End If
                End With
                Calculation()
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

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtDescription.Validating, TxtItemGroup.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtItemGroup.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtItemCategory.Text = AgL.XNull(DrTemp(0)("ItemCategory"))
                        End If
                    Else
                        TxtItemCategory.Text = ""
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmQuality1_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                sender.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub


End Class
