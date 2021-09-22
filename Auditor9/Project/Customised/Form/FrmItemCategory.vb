Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmItemCategory
    Inherits AgTemplate.TempMaster

    Dim mQry$

    Public Const ColSNo As String = "SNo"
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Public Const Col1WEF As String = "WEF"
    Public Const Col1RateGreaterThan As String = "Rate Greater Than"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group"

    Dim DtItemTypeSetting As DataTable

    Public WithEvents TxtParent As AgControls.AgTextBox
    Public WithEvents LblParent As Label
    Public WithEvents TxtIsNewItemAllowedPurch As AgControls.AgTextBox
    Public WithEvents LblIsNewItemAllowedPurch As Label
    Public WithEvents TxtIsNewDimension1AllowedPurch As AgControls.AgTextBox
    Public WithEvents LblIsNewDimension1AllowedPurch As Label
    Public WithEvents TxtIsNewDimension2AllowedPurch As AgControls.AgTextBox
    Public WithEvents LblIsNewDimension2AllowedPurch As Label
    Public WithEvents TxtIsNewDimension3AllowedPurch As AgControls.AgTextBox
    Public WithEvents LblIsNewDimension3AllowedPurch As Label
    Public WithEvents TxtIsNewDimension4AllowedPurch As AgControls.AgTextBox
    Public WithEvents LblIsNewDimension4AllowedPurch As Label
    Dim mItemTypeLastValue As String



#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtItemType = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtSalesTaxGroup = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtUnit = New AgControls.AgTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtHsn = New AgControls.AgTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtDepartment = New AgControls.AgTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.TxtParent = New AgControls.AgTextBox()
        Me.LblParent = New System.Windows.Forms.Label()
        Me.TxtIsNewItemAllowedPurch = New AgControls.AgTextBox()
        Me.LblIsNewItemAllowedPurch = New System.Windows.Forms.Label()
        Me.TxtIsNewDimension1AllowedPurch = New AgControls.AgTextBox()
        Me.LblIsNewDimension1AllowedPurch = New System.Windows.Forms.Label()
        Me.TxtIsNewDimension2AllowedPurch = New AgControls.AgTextBox()
        Me.LblIsNewDimension2AllowedPurch = New System.Windows.Forms.Label()
        Me.TxtIsNewDimension3AllowedPurch = New AgControls.AgTextBox()
        Me.LblIsNewDimension3AllowedPurch = New System.Windows.Forms.Label()
        Me.TxtIsNewDimension4AllowedPurch = New AgControls.AgTextBox()
        Me.LblIsNewDimension4AllowedPurch = New System.Windows.Forms.Label()
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
        Me.Topctrl1.Size = New System.Drawing.Size(897, 41)
        Me.Topctrl1.TabIndex = 12
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 395)
        Me.GroupBox1.Size = New System.Drawing.Size(939, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 399)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 464)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(231, 399)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 399)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 399)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(470, 399)
        Me.GBoxDivision.Size = New System.Drawing.Size(132, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(126, 18)
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
        Me.Label1.Location = New System.Drawing.Point(185, 124)
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
        Me.TxtDescription.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(201, 116)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(364, 20)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(6, 117)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(129, 18)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Item Category"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(185, 105)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 674
        Me.Label2.Text = "Ä"
        '
        'TxtItemType
        '
        Me.TxtItemType.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemType.AgLastValueTag = Nothing
        Me.TxtItemType.AgLastValueText = Nothing
        Me.TxtItemType.AgMandatory = True
        Me.TxtItemType.AgMasterHelp = False
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
        Me.TxtItemType.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemType.Location = New System.Drawing.Point(201, 97)
        Me.TxtItemType.MaxLength = 50
        Me.TxtItemType.Name = "TxtItemType"
        Me.TxtItemType.Size = New System.Drawing.Size(364, 16)
        Me.TxtItemType.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 14)
        Me.Label3.TabIndex = 673
        Me.Label3.Text = "Item Type"
        '
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(39, 377)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1063
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(25, 378)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1062
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(185, 166)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 1066
        Me.Label4.Text = "Ä"
        '
        'TxtSalesTaxGroup
        '
        Me.TxtSalesTaxGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxGroup.AgLastValueTag = Nothing
        Me.TxtSalesTaxGroup.AgLastValueText = Nothing
        Me.TxtSalesTaxGroup.AgMandatory = True
        Me.TxtSalesTaxGroup.AgMasterHelp = False
        Me.TxtSalesTaxGroup.AgNumberLeftPlaces = 0
        Me.TxtSalesTaxGroup.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxGroup.AgNumberRightPlaces = 0
        Me.TxtSalesTaxGroup.AgPickFromLastValue = False
        Me.TxtSalesTaxGroup.AgRowFilter = ""
        Me.TxtSalesTaxGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxGroup.AgSelectedValue = Nothing
        Me.TxtSalesTaxGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxGroup.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxGroup.Location = New System.Drawing.Point(201, 158)
        Me.TxtSalesTaxGroup.MaxLength = 50
        Me.TxtSalesTaxGroup.Name = "TxtSalesTaxGroup"
        Me.TxtSalesTaxGroup.Size = New System.Drawing.Size(364, 16)
        Me.TxtSalesTaxGroup.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 159)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 14)
        Me.Label5.TabIndex = 1065
        Me.Label5.Text = "Sales Tax Group"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(185, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 1069
        Me.Label6.Text = "Ä"
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
        Me.TxtUnit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUnit.Location = New System.Drawing.Point(201, 139)
        Me.TxtUnit.MaxLength = 50
        Me.TxtUnit.Name = "TxtUnit"
        Me.TxtUnit.Size = New System.Drawing.Size(364, 16)
        Me.TxtUnit.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(11, 140)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 14)
        Me.Label7.TabIndex = 1068
        Me.Label7.Text = "Unit"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(185, 185)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 7)
        Me.Label8.TabIndex = 1072
        Me.Label8.Text = "Ä"
        '
        'TxtHsn
        '
        Me.TxtHsn.AgAllowUserToEnableMasterHelp = False
        Me.TxtHsn.AgLastValueTag = Nothing
        Me.TxtHsn.AgLastValueText = Nothing
        Me.TxtHsn.AgMandatory = True
        Me.TxtHsn.AgMasterHelp = False
        Me.TxtHsn.AgNumberLeftPlaces = 8
        Me.TxtHsn.AgNumberNegetiveAllow = False
        Me.TxtHsn.AgNumberRightPlaces = 0
        Me.TxtHsn.AgPickFromLastValue = False
        Me.TxtHsn.AgRowFilter = ""
        Me.TxtHsn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtHsn.AgSelectedValue = Nothing
        Me.TxtHsn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtHsn.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtHsn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtHsn.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHsn.Location = New System.Drawing.Point(201, 177)
        Me.TxtHsn.MaxLength = 8
        Me.TxtHsn.Name = "TxtHsn"
        Me.TxtHsn.Size = New System.Drawing.Size(364, 16)
        Me.TxtHsn.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 178)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 14)
        Me.Label9.TabIndex = 1071
        Me.Label9.Text = "HSN Code"
        '
        'TxtDepartment
        '
        Me.TxtDepartment.AgAllowUserToEnableMasterHelp = False
        Me.TxtDepartment.AgLastValueTag = Nothing
        Me.TxtDepartment.AgLastValueText = Nothing
        Me.TxtDepartment.AgMandatory = False
        Me.TxtDepartment.AgMasterHelp = False
        Me.TxtDepartment.AgNumberLeftPlaces = 0
        Me.TxtDepartment.AgNumberNegetiveAllow = False
        Me.TxtDepartment.AgNumberRightPlaces = 0
        Me.TxtDepartment.AgPickFromLastValue = False
        Me.TxtDepartment.AgRowFilter = ""
        Me.TxtDepartment.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDepartment.AgSelectedValue = Nothing
        Me.TxtDepartment.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDepartment.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDepartment.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDepartment.Location = New System.Drawing.Point(201, 196)
        Me.TxtDepartment.MaxLength = 50
        Me.TxtDepartment.Name = "TxtDepartment"
        Me.TxtDepartment.Size = New System.Drawing.Size(364, 16)
        Me.TxtDepartment.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(6, 197)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 14)
        Me.Label11.TabIndex = 1074
        Me.Label11.Text = "Department"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(591, 94)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(294, 139)
        Me.Pnl1.TabIndex = 7
        '
        'TxtParent
        '
        Me.TxtParent.AgAllowUserToEnableMasterHelp = False
        Me.TxtParent.AgLastValueTag = Nothing
        Me.TxtParent.AgLastValueText = Nothing
        Me.TxtParent.AgMandatory = False
        Me.TxtParent.AgMasterHelp = False
        Me.TxtParent.AgNumberLeftPlaces = 0
        Me.TxtParent.AgNumberNegetiveAllow = False
        Me.TxtParent.AgNumberRightPlaces = 0
        Me.TxtParent.AgPickFromLastValue = False
        Me.TxtParent.AgRowFilter = ""
        Me.TxtParent.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtParent.AgSelectedValue = Nothing
        Me.TxtParent.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtParent.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtParent.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtParent.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParent.Location = New System.Drawing.Point(201, 215)
        Me.TxtParent.MaxLength = 50
        Me.TxtParent.Name = "TxtParent"
        Me.TxtParent.Size = New System.Drawing.Size(364, 16)
        Me.TxtParent.TabIndex = 6
        '
        'LblParent
        '
        Me.LblParent.AutoSize = True
        Me.LblParent.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblParent.Location = New System.Drawing.Point(6, 216)
        Me.LblParent.Name = "LblParent"
        Me.LblParent.Size = New System.Drawing.Size(51, 14)
        Me.LblParent.TabIndex = 1076
        Me.LblParent.Text = "Parent"
        '
        'TxtIsNewItemAllowedPurch
        '
        Me.TxtIsNewItemAllowedPurch.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsNewItemAllowedPurch.AgLastValueTag = Nothing
        Me.TxtIsNewItemAllowedPurch.AgLastValueText = Nothing
        Me.TxtIsNewItemAllowedPurch.AgMandatory = False
        Me.TxtIsNewItemAllowedPurch.AgMasterHelp = False
        Me.TxtIsNewItemAllowedPurch.AgNumberLeftPlaces = 0
        Me.TxtIsNewItemAllowedPurch.AgNumberNegetiveAllow = False
        Me.TxtIsNewItemAllowedPurch.AgNumberRightPlaces = 0
        Me.TxtIsNewItemAllowedPurch.AgPickFromLastValue = False
        Me.TxtIsNewItemAllowedPurch.AgRowFilter = ""
        Me.TxtIsNewItemAllowedPurch.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsNewItemAllowedPurch.AgSelectedValue = Nothing
        Me.TxtIsNewItemAllowedPurch.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsNewItemAllowedPurch.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsNewItemAllowedPurch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsNewItemAllowedPurch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsNewItemAllowedPurch.Location = New System.Drawing.Point(246, 262)
        Me.TxtIsNewItemAllowedPurch.MaxLength = 50
        Me.TxtIsNewItemAllowedPurch.Name = "TxtIsNewItemAllowedPurch"
        Me.TxtIsNewItemAllowedPurch.Size = New System.Drawing.Size(73, 16)
        Me.TxtIsNewItemAllowedPurch.TabIndex = 7
        Me.TxtIsNewItemAllowedPurch.Visible = False
        '
        'LblIsNewItemAllowedPurch
        '
        Me.LblIsNewItemAllowedPurch.AutoSize = True
        Me.LblIsNewItemAllowedPurch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsNewItemAllowedPurch.Location = New System.Drawing.Point(7, 263)
        Me.LblIsNewItemAllowedPurch.Name = "LblIsNewItemAllowedPurch"
        Me.LblIsNewItemAllowedPurch.Size = New System.Drawing.Size(188, 14)
        Me.LblIsNewItemAllowedPurch.TabIndex = 1078
        Me.LblIsNewItemAllowedPurch.Text = "Is New Item Allowed Purch"
        Me.LblIsNewItemAllowedPurch.Visible = False
        '
        'TxtIsNewDimension1AllowedPurch
        '
        Me.TxtIsNewDimension1AllowedPurch.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsNewDimension1AllowedPurch.AgLastValueTag = Nothing
        Me.TxtIsNewDimension1AllowedPurch.AgLastValueText = Nothing
        Me.TxtIsNewDimension1AllowedPurch.AgMandatory = False
        Me.TxtIsNewDimension1AllowedPurch.AgMasterHelp = False
        Me.TxtIsNewDimension1AllowedPurch.AgNumberLeftPlaces = 0
        Me.TxtIsNewDimension1AllowedPurch.AgNumberNegetiveAllow = False
        Me.TxtIsNewDimension1AllowedPurch.AgNumberRightPlaces = 0
        Me.TxtIsNewDimension1AllowedPurch.AgPickFromLastValue = False
        Me.TxtIsNewDimension1AllowedPurch.AgRowFilter = ""
        Me.TxtIsNewDimension1AllowedPurch.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsNewDimension1AllowedPurch.AgSelectedValue = Nothing
        Me.TxtIsNewDimension1AllowedPurch.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsNewDimension1AllowedPurch.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsNewDimension1AllowedPurch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsNewDimension1AllowedPurch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsNewDimension1AllowedPurch.Location = New System.Drawing.Point(246, 281)
        Me.TxtIsNewDimension1AllowedPurch.MaxLength = 50
        Me.TxtIsNewDimension1AllowedPurch.Name = "TxtIsNewDimension1AllowedPurch"
        Me.TxtIsNewDimension1AllowedPurch.Size = New System.Drawing.Size(73, 16)
        Me.TxtIsNewDimension1AllowedPurch.TabIndex = 8
        Me.TxtIsNewDimension1AllowedPurch.Visible = False
        '
        'LblIsNewDimension1AllowedPurch
        '
        Me.LblIsNewDimension1AllowedPurch.AutoSize = True
        Me.LblIsNewDimension1AllowedPurch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsNewDimension1AllowedPurch.Location = New System.Drawing.Point(6, 282)
        Me.LblIsNewDimension1AllowedPurch.Name = "LblIsNewDimension1AllowedPurch"
        Me.LblIsNewDimension1AllowedPurch.Size = New System.Drawing.Size(235, 14)
        Me.LblIsNewDimension1AllowedPurch.TabIndex = 1080
        Me.LblIsNewDimension1AllowedPurch.Text = "Is New Dimension1 Allowed Purch"
        Me.LblIsNewDimension1AllowedPurch.Visible = False
        '
        'TxtIsNewDimension2AllowedPurch
        '
        Me.TxtIsNewDimension2AllowedPurch.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsNewDimension2AllowedPurch.AgLastValueTag = Nothing
        Me.TxtIsNewDimension2AllowedPurch.AgLastValueText = Nothing
        Me.TxtIsNewDimension2AllowedPurch.AgMandatory = False
        Me.TxtIsNewDimension2AllowedPurch.AgMasterHelp = False
        Me.TxtIsNewDimension2AllowedPurch.AgNumberLeftPlaces = 0
        Me.TxtIsNewDimension2AllowedPurch.AgNumberNegetiveAllow = False
        Me.TxtIsNewDimension2AllowedPurch.AgNumberRightPlaces = 0
        Me.TxtIsNewDimension2AllowedPurch.AgPickFromLastValue = False
        Me.TxtIsNewDimension2AllowedPurch.AgRowFilter = ""
        Me.TxtIsNewDimension2AllowedPurch.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsNewDimension2AllowedPurch.AgSelectedValue = Nothing
        Me.TxtIsNewDimension2AllowedPurch.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsNewDimension2AllowedPurch.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsNewDimension2AllowedPurch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsNewDimension2AllowedPurch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsNewDimension2AllowedPurch.Location = New System.Drawing.Point(246, 301)
        Me.TxtIsNewDimension2AllowedPurch.MaxLength = 50
        Me.TxtIsNewDimension2AllowedPurch.Name = "TxtIsNewDimension2AllowedPurch"
        Me.TxtIsNewDimension2AllowedPurch.Size = New System.Drawing.Size(73, 16)
        Me.TxtIsNewDimension2AllowedPurch.TabIndex = 9
        Me.TxtIsNewDimension2AllowedPurch.Visible = False
        '
        'LblIsNewDimension2AllowedPurch
        '
        Me.LblIsNewDimension2AllowedPurch.AutoSize = True
        Me.LblIsNewDimension2AllowedPurch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsNewDimension2AllowedPurch.Location = New System.Drawing.Point(6, 302)
        Me.LblIsNewDimension2AllowedPurch.Name = "LblIsNewDimension2AllowedPurch"
        Me.LblIsNewDimension2AllowedPurch.Size = New System.Drawing.Size(235, 14)
        Me.LblIsNewDimension2AllowedPurch.TabIndex = 1082
        Me.LblIsNewDimension2AllowedPurch.Text = "Is New Dimension2 Allowed Purch"
        Me.LblIsNewDimension2AllowedPurch.Visible = False
        '
        'TxtIsNewDimension3AllowedPurch
        '
        Me.TxtIsNewDimension3AllowedPurch.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsNewDimension3AllowedPurch.AgLastValueTag = Nothing
        Me.TxtIsNewDimension3AllowedPurch.AgLastValueText = Nothing
        Me.TxtIsNewDimension3AllowedPurch.AgMandatory = False
        Me.TxtIsNewDimension3AllowedPurch.AgMasterHelp = False
        Me.TxtIsNewDimension3AllowedPurch.AgNumberLeftPlaces = 0
        Me.TxtIsNewDimension3AllowedPurch.AgNumberNegetiveAllow = False
        Me.TxtIsNewDimension3AllowedPurch.AgNumberRightPlaces = 0
        Me.TxtIsNewDimension3AllowedPurch.AgPickFromLastValue = False
        Me.TxtIsNewDimension3AllowedPurch.AgRowFilter = ""
        Me.TxtIsNewDimension3AllowedPurch.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsNewDimension3AllowedPurch.AgSelectedValue = Nothing
        Me.TxtIsNewDimension3AllowedPurch.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsNewDimension3AllowedPurch.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsNewDimension3AllowedPurch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsNewDimension3AllowedPurch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsNewDimension3AllowedPurch.Location = New System.Drawing.Point(246, 320)
        Me.TxtIsNewDimension3AllowedPurch.MaxLength = 50
        Me.TxtIsNewDimension3AllowedPurch.Name = "TxtIsNewDimension3AllowedPurch"
        Me.TxtIsNewDimension3AllowedPurch.Size = New System.Drawing.Size(73, 16)
        Me.TxtIsNewDimension3AllowedPurch.TabIndex = 10
        Me.TxtIsNewDimension3AllowedPurch.Visible = False
        '
        'LblIsNewDimension3AllowedPurch
        '
        Me.LblIsNewDimension3AllowedPurch.AutoSize = True
        Me.LblIsNewDimension3AllowedPurch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsNewDimension3AllowedPurch.Location = New System.Drawing.Point(7, 323)
        Me.LblIsNewDimension3AllowedPurch.Name = "LblIsNewDimension3AllowedPurch"
        Me.LblIsNewDimension3AllowedPurch.Size = New System.Drawing.Size(235, 14)
        Me.LblIsNewDimension3AllowedPurch.TabIndex = 1084
        Me.LblIsNewDimension3AllowedPurch.Text = "Is New Dimension3 Allowed Purch"
        Me.LblIsNewDimension3AllowedPurch.Visible = False
        '
        'TxtIsNewDimension4AllowedPurch
        '
        Me.TxtIsNewDimension4AllowedPurch.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsNewDimension4AllowedPurch.AgLastValueTag = Nothing
        Me.TxtIsNewDimension4AllowedPurch.AgLastValueText = Nothing
        Me.TxtIsNewDimension4AllowedPurch.AgMandatory = False
        Me.TxtIsNewDimension4AllowedPurch.AgMasterHelp = False
        Me.TxtIsNewDimension4AllowedPurch.AgNumberLeftPlaces = 0
        Me.TxtIsNewDimension4AllowedPurch.AgNumberNegetiveAllow = False
        Me.TxtIsNewDimension4AllowedPurch.AgNumberRightPlaces = 0
        Me.TxtIsNewDimension4AllowedPurch.AgPickFromLastValue = False
        Me.TxtIsNewDimension4AllowedPurch.AgRowFilter = ""
        Me.TxtIsNewDimension4AllowedPurch.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsNewDimension4AllowedPurch.AgSelectedValue = Nothing
        Me.TxtIsNewDimension4AllowedPurch.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsNewDimension4AllowedPurch.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsNewDimension4AllowedPurch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsNewDimension4AllowedPurch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsNewDimension4AllowedPurch.Location = New System.Drawing.Point(246, 340)
        Me.TxtIsNewDimension4AllowedPurch.MaxLength = 50
        Me.TxtIsNewDimension4AllowedPurch.Name = "TxtIsNewDimension4AllowedPurch"
        Me.TxtIsNewDimension4AllowedPurch.Size = New System.Drawing.Size(73, 16)
        Me.TxtIsNewDimension4AllowedPurch.TabIndex = 11
        Me.TxtIsNewDimension4AllowedPurch.Visible = False
        '
        'LblIsNewDimension4AllowedPurch
        '
        Me.LblIsNewDimension4AllowedPurch.AutoSize = True
        Me.LblIsNewDimension4AllowedPurch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsNewDimension4AllowedPurch.Location = New System.Drawing.Point(7, 343)
        Me.LblIsNewDimension4AllowedPurch.Name = "LblIsNewDimension4AllowedPurch"
        Me.LblIsNewDimension4AllowedPurch.Size = New System.Drawing.Size(235, 14)
        Me.LblIsNewDimension4AllowedPurch.TabIndex = 1086
        Me.LblIsNewDimension4AllowedPurch.Text = "Is New Dimension4 Allowed Purch"
        Me.LblIsNewDimension4AllowedPurch.Visible = False
        '
        'FrmItemCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(897, 443)
        Me.Controls.Add(Me.TxtIsNewDimension4AllowedPurch)
        Me.Controls.Add(Me.LblIsNewDimension4AllowedPurch)
        Me.Controls.Add(Me.TxtIsNewDimension3AllowedPurch)
        Me.Controls.Add(Me.LblIsNewDimension3AllowedPurch)
        Me.Controls.Add(Me.TxtIsNewDimension2AllowedPurch)
        Me.Controls.Add(Me.LblIsNewDimension2AllowedPurch)
        Me.Controls.Add(Me.TxtIsNewDimension1AllowedPurch)
        Me.Controls.Add(Me.LblIsNewDimension1AllowedPurch)
        Me.Controls.Add(Me.TxtIsNewItemAllowedPurch)
        Me.Controls.Add(Me.LblIsNewItemAllowedPurch)
        Me.Controls.Add(Me.TxtParent)
        Me.Controls.Add(Me.LblParent)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtDepartment)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtHsn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtUnit)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtSalesTaxGroup)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtItemType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmItemCategory"
        Me.Text = "Item Category Master"
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
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtItemType, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.TxtUnit, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtHsn, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.TxtDepartment, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LblParent, 0)
        Me.Controls.SetChildIndex(Me.TxtParent, 0)
        Me.Controls.SetChildIndex(Me.LblIsNewItemAllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.TxtIsNewItemAllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.LblIsNewDimension1AllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.TxtIsNewDimension1AllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.LblIsNewDimension2AllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.TxtIsNewDimension2AllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.LblIsNewDimension3AllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.TxtIsNewDimension3AllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.LblIsNewDimension4AllowedPurch, 0)
        Me.Controls.SetChildIndex(Me.TxtIsNewDimension4AllowedPurch, 0)
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
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents TxtItemType As AgControls.AgTextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents Label4 As Label
    Public WithEvents TxtSalesTaxGroup As AgControls.AgTextBox
    Public WithEvents Label5 As Label
    Public WithEvents Label6 As Label
    Public WithEvents TxtUnit As AgControls.AgTextBox
    Public WithEvents Label7 As Label
    Public WithEvents Label8 As Label
    Public WithEvents TxtHsn As AgControls.AgTextBox
    Public WithEvents Label9 As Label
    Public WithEvents TxtDepartment As AgControls.AgTextBox
    Public WithEvents Label11 As Label
    Public WithEvents Pnl1 As Panel
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region


    Private Sub FGetItemTypeSetting()
        If mItemTypeLastValue <> TxtItemType.Tag And TxtItemType.Tag <> "" Then
            mItemTypeLastValue = TxtItemType.Tag
            mQry = "Select * From ItemTypeSetting Where ItemType = '" & TxtItemType.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' "
            DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
            If DtItemTypeSetting.Rows.Count = 0 Then
                mQry = "Select * From ItemTypeSetting Where ItemType = '" & TxtItemType.Tag & "' And Div_Code Is Null "
                DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                If DtItemTypeSetting.Rows.Count = 0 Then
                    mQry = "Select * From ItemTypeSetting Where ItemType Is Null And Div_Code Is Null "
                    DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                    If DtItemTypeSetting.Rows.Count = 0 Then
                        MsgBox("Item Type Setting Not Found")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If TxtDescription.Text.Trim = "" Then Err.Raise(1, , "Description Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From ItemCategory Where Description='" & TxtDescription.Text & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From ItemCategory Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If

        If Len(TxtHsn.Text) < 2 Then
            MsgBox("HSN Code and not be less than 2 characters.")
            passed = False
            TxtHsn.Focus()
        End If

    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code, I.Description, T.Name AS ItemType  " &
                        " FROM ItemCategory I " &
                        " Left Join ItemType T On I.ItemType = T.Code "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        'LogTableName = "ItemCategory_Log"
        'MainLineTableCsv = "ItemBuyer"
        'LogLineTableCsv = "ItemBuyer_Log"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer

        mQry = "UPDATE Item
                Set 
                Description = " & AgL.Chk_Text(TxtDescription.Text) & ", 
                V_Type = " & AgL.Chk_Text("IC") & ", 
                IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ", 
                ItemType = " & AgL.Chk_Text(TxtItemType.AgSelectedValue) & ", 
                SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxGroup.AgSelectedValue) & ", 
                HSN = " & AgL.Chk_Text(TxtHsn.Text) & ", 
                Department = " & AgL.Chk_Text(TxtDepartment.AgSelectedValue) & ", 
                Parent = " & AgL.Chk_Text(TxtParent.AgSelectedValue) & ", 
                IsNewItemAllowedPurch = " & IIf(TxtIsNewItemAllowedPurch.Text = "Yes", 1, 0) & ", 
                IsNewDimension1AllowedPurch = " & IIf(TxtIsNewDimension1AllowedPurch.Text = "Yes", 1, 0) & ", 
                IsNewDimension2AllowedPurch = " & IIf(TxtIsNewDimension2AllowedPurch.Text = "Yes", 1, 0) & ", 
                IsNewDimension3AllowedPurch = " & IIf(TxtIsNewDimension3AllowedPurch.Text = "Yes", 1, 0) & ", 
                IsNewDimension4AllowedPurch = " & IIf(TxtIsNewDimension4AllowedPurch.Text = "Yes", 1, 0) & ", 
                OMSId = Null, 
                Unit = " & AgL.Chk_Text(TxtUnit.AgSelectedValue) & " 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


        mQry = " UPDATE Item Set ItemType = '" & TxtItemType.AgSelectedValue & "' Where ItemCategory = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete from ItemCategorySalesTax where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To DGL1.Rows.Count - 1
            If DGL1.Item(Col1SalesTaxGroup, I).Value <> "" And Val(DGL1.Item(Col1RateGreaterThan, I).Value) > 0 Then
                mQry = " Insert Into ItemCategorySalesTax (Code,WEF, RateGreaterThan, SalesTaxGroupItem) " &
                       " Values ('" & SearchCode & "', " & AgL.Chk_Date(DGL1.Item(Col1WEF, I).Value) & ", " & Val(DGL1.Item(Col1RateGreaterThan, I).Value) & ", " & AgL.Chk_Text(DGL1.Item(Col1SalesTaxGroup, I).Value) & " )"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " &
                " From ItemCategory " &
                " Order By Description "
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Code, Name  FROM ItemType "
        TxtItemType.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Description as  Code, Description as Name  FROM PostingGroupSalesTaxItem where Active=1 Order By Description"
        TxtSalesTaxGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Code, Code as Name  FROM Unit where IsActive=1 Order By Code "
        TxtUnit.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Description as Name  FROM Department where Status='Active' Order By Code"
        TxtDepartment.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Description as Name  FROM ItemCategory where Status='Active' Order By Code"
        TxtParent.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, D.Description as DepartmentName, P.Description As ParentName
                 From ItemCategory H 
                 Left Join Department D On H.Department = D.Code
                 LEFT JOIN ItemCategory P On H.Parent = P.Code
                 Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(DsTemp.Tables(0).Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("Description"))
                TxtItemType.AgSelectedValue = AgL.XNull(DsTemp.Tables(0).Rows(0)("ItemType"))
                FGetItemTypeSetting()
                TxtSalesTaxGroup.AgSelectedValue = AgL.XNull(DsTemp.Tables(0).Rows(0)("SalesTaxGroup"))
                TxtUnit.AgSelectedValue = AgL.XNull(DsTemp.Tables(0).Rows(0)("Unit"))
                TxtDepartment.Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Department"))
                TxtDepartment.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("DepartmentName"))
                TxtParent.Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Parent"))
                TxtParent.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("ParentName"))
                TxtHsn.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("HSN"))

                TxtIsNewItemAllowedPurch.Text = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewItemAllowedPurch")) <> 0, "Yes", "No")
                TxtIsNewDimension1AllowedPurch.Text = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewDimension1AllowedPurch")) <> 0, "Yes", "No")
                TxtIsNewDimension2AllowedPurch.Text = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewDimension2AllowedPurch")) <> 0, "Yes", "No")
                TxtIsNewDimension3AllowedPurch.Text = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewDimension3AllowedPurch")) <> 0, "Yes", "No")
                TxtIsNewDimension4AllowedPurch.Text = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewDimension4AllowedPurch")) <> 0, "Yes", "No")

                ChkIsSystemDefine.Checked = AgL.VNull(DsTemp.Tables(0).Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False
            End If
        End With


        Dim I As Integer
        mQry = " Select  H.Code, H.WEF, H.RateGreaterThan, H.SalesTaxGroupItem 
                        From ItemCategorySalesTax H 
                        Where H.Code='" & SearchCode & "' 
                        Order By H.WEF, H.RateGreaterThan "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGL1.RowCount = 1
            DGL1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGL1.Rows.Add()
                    DGL1.Item(ColSNo, I).Value = DGL1.Rows.Count - 1
                    DGL1.Item(Col1WEF, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("WEF")))
                    DGL1.Item(Col1RateGreaterThan, I).Value = Format(AgL.VNull(.Rows(I)("RateGreaterThan")), "0.00")
                    DGL1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                    DGL1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                Next I
                DGL1.Visible = True
            Else
                DGL1.Visible = False
            End If
        End With

        FrmItemCategory_BaseFunction_DispText()
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From Item Where ItemCategory = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For ItemCategory " & TxtDescription.Text & " In Item Master . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtParent.KeyDown
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
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
            " From ItemCategory I " &
            " Order By I.Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 360, 885)
        FManageSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()

        If ClsMain.IsEntryLockedWithLockText("Item", "Code", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        FGetItemTypeSetting()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
        If Passed = False Then Exit Sub
        Passed = Not FGetRelationalData()

        If ClsMain.IsEntryLockedWithLockText("Item", "Code", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub
    Private Sub ChkIsSystemDefine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkIsSystemDefine.Click
        FManageSystemDefine()
    End Sub

    Private Sub FManageSystemDefine()
        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            ChkIsSystemDefine.Visible = True
            ChkIsSystemDefine.Enabled = True
        Else
            ChkIsSystemDefine.Visible = False
            ChkIsSystemDefine.Enabled = False
        End If

        If ChkIsSystemDefine.Checked Then
            LblIsSystemDefine.Text = "System Define"
        Else
            LblIsSystemDefine.Text = "User Define"
        End If
    End Sub

    Private Function FRestrictSystemDefine() As Boolean
        If ChkIsSystemDefine.Checked = True Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                If MsgBox("This is a System Define Item.Do You Want To Proceed...?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Topctrl1.FButtonClick(14, True)
                    FRestrictSystemDefine = False
                    Exit Function
                End If
            Else
                MsgBox("Can't Edit System Define Items...!", MsgBoxStyle.Information) : Topctrl1.FButtonClick(14, True)
                FRestrictSystemDefine = False
                Exit Function
            End If
        End If
        FManageSystemDefine()
        FRestrictSystemDefine = True
    End Function

    Private Sub FrmItemCategory_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim DtTemp As DataTable
        Try

            ChkIsSystemDefine.Checked = False
            FManageSystemDefine()

            TxtItemType.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_ItemType"))
            If TxtItemType.Tag <> "" Then
                DtTemp = AgL.FillData("Select Name From ItemType Where Code = '" & TxtItemType.Tag & "'", AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    TxtItemType.Text = AgL.XNull(DtTemp.Rows(0)("Name"))
                    FGetItemTypeSetting()
                Else
                    MsgBox("Invalid data in Default_ItemType of Enviromentment Settings")
                End If
            End If


            TxtUnit.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_Unit"))
            If TxtUnit.Tag <> "" Then
                DtTemp = AgL.FillData("Select Code From Unit Where Code = '" & TxtUnit.Tag & "'", AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    TxtUnit.Text = AgL.XNull(DtTemp.Rows(0)("Code"))
                Else
                    MsgBox("Invalid data in Default_Unit of Enviromentment Settings")
                End If
            End If

            TxtSalesTaxGroup.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
            If TxtSalesTaxGroup.Tag <> "" Then
                DtTemp = AgL.FillData("Select Description From PostingGroupSalesTaxItem Where Description = '" & TxtSalesTaxGroup.Tag & "'", AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    TxtSalesTaxGroup.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                Else
                    MsgBox("Invalid data in Default_SalesTaxGroupItem of Enviromentment Settings")
                End If
            End If


            TxtHsn.Text = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_HSN"))
            TxtItemType.Focus()
        Catch ex As Exception
            MsgBox(ex.Message & " [FrmItemCategory_BaseEvent_Topctrl_tbAdd]")
        End Try

    End Sub

    Private Sub FrmItemCategory_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        ChkIsSystemDefine.Enabled = False

        If DtItemTypeSetting IsNot Nothing Then
            If AgL.VNull(DtItemTypeSetting.Rows(0)("IsSalesTaxBasedOnRate")) Then
                DGL1.Visible = True
            Else
                DGL1.Visible = False
            End If
        End If

        If ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.RetailModule) Then
            LblIsNewItemAllowedPurch.Visible = True : TxtIsNewItemAllowedPurch.Visible = True
            LblIsNewDimension1AllowedPurch.Visible = True : TxtIsNewDimension1AllowedPurch.Visible = True
            LblIsNewDimension1AllowedPurch.Text = LblIsNewDimension1AllowedPurch.Text.Replace("Dimension1", AgL.PubCaptionDimension1)
        End If
    End Sub

    Private Sub Txt_Validating(sender As Object, e As CancelEventArgs) Handles TxtHsn.Validating, TxtItemType.Validating
        Select Case sender.name
            Case TxtHsn.Name
                If Len(sender.text) < 2 Then
                    MsgBox("HSN Code can not be less than 2 characters.")
                    e.Cancel = True
                End If
            Case TxtItemType.Name
                FGetItemTypeSetting()
                FrmItemCategory_BaseFunction_DispText()
        End Select
    End Sub

    Private Sub FrmItemCategory_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        DGL1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGL1, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgDateColumn(DGL1, Col1WEF, 90, Col1WEF, True, False)
            .AddAgNumberColumn(DGL1, Col1RateGreaterThan, 80, 8, 2, False, Col1RateGreaterThan, True, False, True)
            .AddAgTextColumn(DGL1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, True, False, False)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.EnableHeadersVisualStyles = False
        DGL1.AgSkipReadOnlyColumns = True
        DGL1.RowHeadersVisible = False
        DGL1.ColumnHeadersHeight = 48
        AgL.GridDesign(DGL1)
    End Sub

    Private Sub DGL1_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles DGL1.EditingControl_KeyDown
        Dim mQry As String
        Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
            Case Col1SalesTaxGroup
                If e.KeyCode <> Keys.Enter Then
                    If DGL1.AgHelpDataSet(Col1SalesTaxGroup) Is Nothing Then
                        mQry = "select Description as Code, Description  from postinggroupsalesTaxitem Where IfNull(Active,1)=1 Order By Description"
                        DGL1.AgHelpDataSet(Col1SalesTaxGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, TxtItemType.Tag, "", "", "", "")
        FGetSettings = mValue
    End Function

    Private Sub FrmItemCategory_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
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
    End Sub

    Private Sub LblDescription_Click(sender As Object, e As EventArgs) Handles LblDescription.Click

    End Sub
End Class
