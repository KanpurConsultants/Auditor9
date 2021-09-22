Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Xml
Imports Customised.ClsMain
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmItemMaster_Aadhat
    Inherits AgTemplate.TempMaster
    Dim mQry$
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents PnlRateType As Panel
    Dim Photo_Byte As Byte()
    Public Const ColSNo As String = "SNo"
    Public WithEvents DGLRateType As New AgControls.AgDataGrid
    Public Const Col1RateType As String = FrmItemMasterLineRateType.RateType
    Public Const Col1Margin As String = FrmItemMasterLineRateType.MarginPer
    Public Const Col1Rate As String = FrmItemMasterLineRateType.Rate
    Public Const Col1Discount As String = FrmItemMasterLineRateType.DiscountPer
    Public Const Col1Addition As String = FrmItemMasterLineRateType.AdditionPer



    Public WithEvents Label2 As Label
    Public WithEvents TxtSpecification As AgControls.AgTextBox
    Public WithEvents Label4 As Label
    Public WithEvents TxtHsn As AgControls.AgTextBox
    Public WithEvents Label9 As Label
    Public WithEvents TxtPurchaseRate As AgControls.AgTextBox
    Public WithEvents Label7 As Label

    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Dim gItemGroupDefaultMargin As Double
    Dim DtItemTypeSetting



    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"



    Public Const rowDefaultDiscountPerSale As Integer = 0
    Public Const rowDefaultAdditionPerSale As Integer = 1
    Public Const rowDefaultDiscountPerPurchase As Integer = 2
    Public Const rowMaintainStockYn As Integer = 3
    Public Const rowShowItemInOtherDivision As Integer = 4
    Public Const rowBarcode As Integer = 5
    Public Const rowDefaultSupplier As Integer = 6
    Public Const rowMRP As Integer = 7



    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuBulkEdit As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Friend WithEvents MnuImportRateListFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportRateListFromDos As ToolStripMenuItem
    Public WithEvents LblPurchaseRate_Mandatory As Label
    Public WithEvents LblSaleRate_Mandatory As Label
    Friend WithEvents Pnl1 As Panel
    Dim mItemTypeLastValue As String

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.PicPhoto = New System.Windows.Forms.PictureBox()
        Me.BtnBrowse = New System.Windows.Forms.Button()
        Me.BtnPhotoClear = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.LblItemCategory = New System.Windows.Forms.Label()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtItemType = New AgControls.AgTextBox()
        Me.TxtSaleRate = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtItemCategory = New AgControls.AgTextBox()
        Me.TxtItemGroup = New AgControls.AgTextBox()
        Me.LblItemGroup = New System.Windows.Forms.Label()
        Me.TxtSalesTaxPostingGroup = New AgControls.AgTextBox()
        Me.LblSalesTaxPostingGroup = New System.Windows.Forms.Label()
        Me.LblManualCodeReq = New System.Windows.Forms.Label()
        Me.TxtManualCode = New AgControls.AgTextBox()
        Me.LblManualCode = New System.Windows.Forms.Label()
        Me.TxtUnit = New AgControls.AgTextBox()
        Me.LblUnit = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel()
        Me.BtnUnitConversion = New System.Windows.Forms.Button()
        Me.BtnBOMDetail = New System.Windows.Forms.Button()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PnlRateType = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtSpecification = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtHsn = New AgControls.AgTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtPurchaseRate = New AgControls.AgTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportRateListFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportRateListFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBulkEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.LblPurchaseRate_Mandatory = New System.Windows.Forms.Label()
        Me.LblSaleRate_Mandatory = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlCustomGrid.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(944, 41)
        Me.Topctrl1.TabIndex = 3
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 464)
        Me.GroupBox1.Size = New System.Drawing.Size(986, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(23, 468)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(890, 419)
        Me.GBoxEntryType.Visible = False
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(227, 468)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 468)
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
        Me.GroupBox2.Location = New System.Drawing.Point(669, 468)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(443, 468)
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
        'TxtCustomFields
        '
        Me.TxtCustomFields.AgAllowUserToEnableMasterHelp = False
        Me.TxtCustomFields.AgLastValueTag = Nothing
        Me.TxtCustomFields.AgLastValueText = Nothing
        Me.TxtCustomFields.AgMandatory = False
        Me.TxtCustomFields.AgMasterHelp = False
        Me.TxtCustomFields.AgNumberLeftPlaces = 8
        Me.TxtCustomFields.AgNumberNegetiveAllow = False
        Me.TxtCustomFields.AgNumberRightPlaces = 2
        Me.TxtCustomFields.AgPickFromLastValue = False
        Me.TxtCustomFields.AgRowFilter = ""
        Me.TxtCustomFields.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCustomFields.AgSelectedValue = Nothing
        Me.TxtCustomFields.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCustomFields.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCustomFields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCustomFields.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomFields.Location = New System.Drawing.Point(0, 44)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(115, 18)
        Me.TxtCustomFields.TabIndex = 2
        Me.TxtCustomFields.Text = "TxtCustomFields"
        Me.TxtCustomFields.Visible = False
        '
        'PicPhoto
        '
        Me.PicPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicPhoto.Location = New System.Drawing.Point(6, 31)
        Me.PicPhoto.Name = "PicPhoto"
        Me.PicPhoto.Size = New System.Drawing.Size(155, 129)
        Me.PicPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicPhoto.TabIndex = 1015
        Me.PicPhoto.TabStop = False
        '
        'BtnBrowse
        '
        Me.BtnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBrowse.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBrowse.Location = New System.Drawing.Point(6, 164)
        Me.BtnBrowse.Name = "BtnBrowse"
        Me.BtnBrowse.Size = New System.Drawing.Size(69, 23)
        Me.BtnBrowse.TabIndex = 20
        Me.BtnBrowse.Text = "Browse"
        Me.BtnBrowse.UseVisualStyleBackColor = True
        '
        'BtnPhotoClear
        '
        Me.BtnPhotoClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPhotoClear.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPhotoClear.Location = New System.Drawing.Point(91, 164)
        Me.BtnPhotoClear.Name = "BtnPhotoClear"
        Me.BtnPhotoClear.Size = New System.Drawing.Size(69, 23)
        Me.BtnPhotoClear.TabIndex = 21
        Me.BtnPhotoClear.Text = "Clear"
        Me.BtnPhotoClear.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(264, 222)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 14)
        Me.Label15.TabIndex = 1056
        Me.Label15.Text = "Item Type"
        Me.Label15.Visible = False
        '
        'LblItemCategory
        '
        Me.LblItemCategory.AutoSize = True
        Me.LblItemCategory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemCategory.Location = New System.Drawing.Point(18, 119)
        Me.LblItemCategory.Name = "LblItemCategory"
        Me.LblItemCategory.Size = New System.Drawing.Size(102, 14)
        Me.LblItemCategory.TabIndex = 1054
        Me.LblItemCategory.Text = "Item Category"
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCustomGrid.Controls.Add(Me.TxtCustomFields)
        Me.PnlCustomGrid.Location = New System.Drawing.Point(928, 324)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(118, 89)
        Me.PnlCustomGrid.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(125, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 1049
        Me.Label6.Text = "Ä"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(388, 187)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 7)
        Me.Label8.TabIndex = 1048
        Me.Label8.Text = "Ä"
        Me.Label8.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(125, 183)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 1047
        Me.Label5.Text = "Ä"
        Me.Label5.Visible = False
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
        Me.TxtItemType.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemType.Location = New System.Drawing.Point(402, 221)
        Me.TxtItemType.MaxLength = 20
        Me.TxtItemType.Name = "TxtItemType"
        Me.TxtItemType.Size = New System.Drawing.Size(134, 16)
        Me.TxtItemType.TabIndex = 10
        Me.TxtItemType.Visible = False
        '
        'TxtSaleRate
        '
        Me.TxtSaleRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleRate.AgLastValueTag = Nothing
        Me.TxtSaleRate.AgLastValueText = Nothing
        Me.TxtSaleRate.AgMandatory = False
        Me.TxtSaleRate.AgMasterHelp = False
        Me.TxtSaleRate.AgNumberLeftPlaces = 0
        Me.TxtSaleRate.AgNumberNegetiveAllow = False
        Me.TxtSaleRate.AgNumberRightPlaces = 0
        Me.TxtSaleRate.AgPickFromLastValue = False
        Me.TxtSaleRate.AgRowFilter = ""
        Me.TxtSaleRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleRate.AgSelectedValue = Nothing
        Me.TxtSaleRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleRate.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtSaleRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleRate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleRate.Location = New System.Drawing.Point(402, 201)
        Me.TxtSaleRate.MaxLength = 20
        Me.TxtSaleRate.Name = "TxtSaleRate"
        Me.TxtSaleRate.Size = New System.Drawing.Size(133, 16)
        Me.TxtSaleRate.TabIndex = 8
        Me.TxtSaleRate.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 201)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 14)
        Me.Label3.TabIndex = 1043
        Me.Label3.Text = "Purchase Rate"
        Me.Label3.Visible = False
        '
        'TxtItemCategory
        '
        Me.TxtItemCategory.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemCategory.AgLastValueTag = Nothing
        Me.TxtItemCategory.AgLastValueText = Nothing
        Me.TxtItemCategory.AgMandatory = False
        Me.TxtItemCategory.AgMasterHelp = False
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
        Me.TxtItemCategory.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemCategory.Location = New System.Drawing.Point(140, 119)
        Me.TxtItemCategory.MaxLength = 20
        Me.TxtItemCategory.Name = "TxtItemCategory"
        Me.TxtItemCategory.Size = New System.Drawing.Size(394, 16)
        Me.TxtItemCategory.TabIndex = 0
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
        Me.TxtItemGroup.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemGroup.Location = New System.Drawing.Point(141, 100)
        Me.TxtItemGroup.MaxLength = 20
        Me.TxtItemGroup.Name = "TxtItemGroup"
        Me.TxtItemGroup.Size = New System.Drawing.Size(394, 16)
        Me.TxtItemGroup.TabIndex = 4
        '
        'LblItemGroup
        '
        Me.LblItemGroup.AutoSize = True
        Me.LblItemGroup.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemGroup.Location = New System.Drawing.Point(19, 100)
        Me.LblItemGroup.Name = "LblItemGroup"
        Me.LblItemGroup.Size = New System.Drawing.Size(82, 14)
        Me.LblItemGroup.TabIndex = 1042
        Me.LblItemGroup.Text = "Item Group"
        '
        'TxtSalesTaxPostingGroup
        '
        Me.TxtSalesTaxPostingGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxPostingGroup.AgLastValueTag = Nothing
        Me.TxtSalesTaxPostingGroup.AgLastValueText = Nothing
        Me.TxtSalesTaxPostingGroup.AgMandatory = True
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
        Me.TxtSalesTaxPostingGroup.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxPostingGroup.Location = New System.Drawing.Point(402, 181)
        Me.TxtSalesTaxPostingGroup.MaxLength = 20
        Me.TxtSalesTaxPostingGroup.Name = "TxtSalesTaxPostingGroup"
        Me.TxtSalesTaxPostingGroup.Size = New System.Drawing.Size(133, 16)
        Me.TxtSalesTaxPostingGroup.TabIndex = 6
        Me.TxtSalesTaxPostingGroup.Visible = False
        '
        'LblSalesTaxPostingGroup
        '
        Me.LblSalesTaxPostingGroup.AutoSize = True
        Me.LblSalesTaxPostingGroup.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxPostingGroup.Location = New System.Drawing.Point(264, 181)
        Me.LblSalesTaxPostingGroup.Name = "LblSalesTaxPostingGroup"
        Me.LblSalesTaxPostingGroup.Size = New System.Drawing.Size(115, 14)
        Me.LblSalesTaxPostingGroup.TabIndex = 1041
        Me.LblSalesTaxPostingGroup.Text = "Sales Tax Group"
        Me.LblSalesTaxPostingGroup.Visible = False
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(125, 83)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 1040
        Me.LblManualCodeReq.Text = "Ä"
        Me.LblManualCodeReq.Visible = False
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualCode.AgLastValueTag = Nothing
        Me.TxtManualCode.AgLastValueText = ""
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
        Me.TxtManualCode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualCode.Location = New System.Drawing.Point(141, 81)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(394, 16)
        Me.TxtManualCode.TabIndex = 2
        Me.TxtManualCode.Visible = False
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(19, 81)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(75, 14)
        Me.LblManualCode.TabIndex = 1039
        Me.LblManualCode.Text = "Item Code"
        Me.LblManualCode.Visible = False
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
        Me.TxtUnit.Location = New System.Drawing.Point(141, 181)
        Me.TxtUnit.MaxLength = 20
        Me.TxtUnit.Name = "TxtUnit"
        Me.TxtUnit.Size = New System.Drawing.Size(113, 16)
        Me.TxtUnit.TabIndex = 5
        Me.TxtUnit.Visible = False
        '
        'LblUnit
        '
        Me.LblUnit.AutoSize = True
        Me.LblUnit.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUnit.Location = New System.Drawing.Point(19, 181)
        Me.LblUnit.Name = "LblUnit"
        Me.LblUnit.Size = New System.Drawing.Size(34, 14)
        Me.LblUnit.TabIndex = 1038
        Me.LblUnit.Text = "Unit"
        Me.LblUnit.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(125, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 1037
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
        Me.TxtDescription.Location = New System.Drawing.Point(141, 157)
        Me.TxtDescription.MaxLength = 255
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(394, 20)
        Me.TxtDescription.TabIndex = 2
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(19, 157)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(102, 18)
        Me.LblDescription.TabIndex = 1036
        Me.LblDescription.Text = "Item Name"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LblMaterialPlanForFollowingItems)
        Me.Panel1.Controls.Add(Me.PicPhoto)
        Me.Panel1.Controls.Add(Me.BtnBrowse)
        Me.Panel1.Controls.Add(Me.BtnPhotoClear)
        Me.Panel1.Location = New System.Drawing.Point(928, 126)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(169, 192)
        Me.Panel1.TabIndex = 18
        Me.Panel1.Visible = False
        '
        'LblMaterialPlanForFollowingItems
        '
        Me.LblMaterialPlanForFollowingItems.BackColor = System.Drawing.Color.SteelBlue
        Me.LblMaterialPlanForFollowingItems.DisabledLinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaterialPlanForFollowingItems.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblMaterialPlanForFollowingItems.LinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Location = New System.Drawing.Point(-1, 0)
        Me.LblMaterialPlanForFollowingItems.Name = "LblMaterialPlanForFollowingItems"
        Me.LblMaterialPlanForFollowingItems.Size = New System.Drawing.Size(169, 25)
        Me.LblMaterialPlanForFollowingItems.TabIndex = 19
        Me.LblMaterialPlanForFollowingItems.TabStop = True
        Me.LblMaterialPlanForFollowingItems.Text = "Item Image"
        Me.LblMaterialPlanForFollowingItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnUnitConversion
        '
        Me.BtnUnitConversion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnUnitConversion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUnitConversion.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUnitConversion.Location = New System.Drawing.Point(928, 74)
        Me.BtnUnitConversion.Name = "BtnUnitConversion"
        Me.BtnUnitConversion.Size = New System.Drawing.Size(131, 23)
        Me.BtnUnitConversion.TabIndex = 19
        Me.BtnUnitConversion.Text = "Unit Conversion"
        Me.BtnUnitConversion.UseVisualStyleBackColor = True
        Me.BtnUnitConversion.Visible = False
        '
        'BtnBOMDetail
        '
        Me.BtnBOMDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBOMDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBOMDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBOMDetail.Location = New System.Drawing.Point(928, 97)
        Me.BtnBOMDetail.Name = "BtnBOMDetail"
        Me.BtnBOMDetail.Size = New System.Drawing.Size(131, 23)
        Me.BtnBOMDetail.TabIndex = 20
        Me.BtnBOMDetail.Text = "BOM Detail"
        Me.BtnBOMDetail.UseVisualStyleBackColor = True
        Me.BtnBOMDetail.Visible = False
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(18, 274)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1058
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        Me.ChkIsSystemDefine.Visible = False
        '
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(32, 273)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(112, 14)
        Me.LblIsSystemDefine.TabIndex = 1059
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        Me.LblIsSystemDefine.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(124, 122)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 7)
        Me.Label12.TabIndex = 1060
        Me.Label12.Text = "Ä"
        '
        'PnlRateType
        '
        Me.PnlRateType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlRateType.Location = New System.Drawing.Point(28, 301)
        Me.PnlRateType.Name = "PnlRateType"
        Me.PnlRateType.Size = New System.Drawing.Size(465, 139)
        Me.PnlRateType.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(125, 257)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 1063
        Me.Label2.Text = "Ä"
        Me.Label2.Visible = False
        '
        'TxtSpecification
        '
        Me.TxtSpecification.AgAllowUserToEnableMasterHelp = False
        Me.TxtSpecification.AgLastValueTag = Nothing
        Me.TxtSpecification.AgLastValueText = Nothing
        Me.TxtSpecification.AgMandatory = True
        Me.TxtSpecification.AgMasterHelp = True
        Me.TxtSpecification.AgNumberLeftPlaces = 0
        Me.TxtSpecification.AgNumberNegetiveAllow = False
        Me.TxtSpecification.AgNumberRightPlaces = 0
        Me.TxtSpecification.AgPickFromLastValue = False
        Me.TxtSpecification.AgRowFilter = ""
        Me.TxtSpecification.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSpecification.AgSelectedValue = Nothing
        Me.TxtSpecification.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSpecification.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSpecification.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSpecification.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSpecification.Location = New System.Drawing.Point(141, 254)
        Me.TxtSpecification.MaxLength = 255
        Me.TxtSpecification.Name = "TxtSpecification"
        Me.TxtSpecification.Size = New System.Drawing.Size(394, 16)
        Me.TxtSpecification.TabIndex = 3
        Me.TxtSpecification.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(19, 254)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 14)
        Me.Label4.TabIndex = 1062
        Me.Label4.Text = "Specification"
        Me.Label4.Visible = False
        '
        'TxtHsn
        '
        Me.TxtHsn.AgAllowUserToEnableMasterHelp = False
        Me.TxtHsn.AgLastValueTag = Nothing
        Me.TxtHsn.AgLastValueText = Nothing
        Me.TxtHsn.AgMandatory = False
        Me.TxtHsn.AgMasterHelp = False
        Me.TxtHsn.AgNumberLeftPlaces = 8
        Me.TxtHsn.AgNumberNegetiveAllow = False
        Me.TxtHsn.AgNumberRightPlaces = 0
        Me.TxtHsn.AgPickFromLastValue = False
        Me.TxtHsn.AgRowFilter = ""
        Me.TxtHsn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtHsn.AgSelectedValue = Nothing
        Me.TxtHsn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtHsn.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtHsn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtHsn.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHsn.Location = New System.Drawing.Point(141, 138)
        Me.TxtHsn.MaxLength = 8
        Me.TxtHsn.Name = "TxtHsn"
        Me.TxtHsn.Size = New System.Drawing.Size(113, 16)
        Me.TxtHsn.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(19, 139)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 14)
        Me.Label9.TabIndex = 1065
        Me.Label9.Text = "HSN Code"
        '
        'TxtPurchaseRate
        '
        Me.TxtPurchaseRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtPurchaseRate.AgLastValueTag = Nothing
        Me.TxtPurchaseRate.AgLastValueText = Nothing
        Me.TxtPurchaseRate.AgMandatory = False
        Me.TxtPurchaseRate.AgMasterHelp = False
        Me.TxtPurchaseRate.AgNumberLeftPlaces = 8
        Me.TxtPurchaseRate.AgNumberNegetiveAllow = False
        Me.TxtPurchaseRate.AgNumberRightPlaces = 2
        Me.TxtPurchaseRate.AgPickFromLastValue = False
        Me.TxtPurchaseRate.AgRowFilter = ""
        Me.TxtPurchaseRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPurchaseRate.AgSelectedValue = Nothing
        Me.TxtPurchaseRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPurchaseRate.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtPurchaseRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPurchaseRate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPurchaseRate.Location = New System.Drawing.Point(141, 201)
        Me.TxtPurchaseRate.MaxLength = 20
        Me.TxtPurchaseRate.Name = "TxtPurchaseRate"
        Me.TxtPurchaseRate.Size = New System.Drawing.Size(113, 16)
        Me.TxtPurchaseRate.TabIndex = 7
        Me.TxtPurchaseRate.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(264, 201)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 14)
        Me.Label7.TabIndex = 1067
        Me.Label7.Text = "Sale Rate"
        Me.Label7.Visible = False
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuImportRateListFromExcel, Me.MnuImportRateListFromDos, Me.MnuBulkEdit})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(218, 136)
        Me.MnuOptions.Text = "Option"
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(217, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(217, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(217, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuImportRateListFromExcel
        '
        Me.MnuImportRateListFromExcel.Name = "MnuImportRateListFromExcel"
        Me.MnuImportRateListFromExcel.Size = New System.Drawing.Size(217, 22)
        Me.MnuImportRateListFromExcel.Text = "Import Rate List From Excel"
        '
        'MnuImportRateListFromDos
        '
        Me.MnuImportRateListFromDos.Name = "MnuImportRateListFromDos"
        Me.MnuImportRateListFromDos.Size = New System.Drawing.Size(217, 22)
        Me.MnuImportRateListFromDos.Text = "Import Rate List From Dos"
        '
        'MnuBulkEdit
        '
        Me.MnuBulkEdit.Name = "MnuBulkEdit"
        Me.MnuBulkEdit.Size = New System.Drawing.Size(217, 22)
        Me.MnuBulkEdit.Text = "Bulk Edit"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'LblPurchaseRate_Mandatory
        '
        Me.LblPurchaseRate_Mandatory.AutoSize = True
        Me.LblPurchaseRate_Mandatory.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblPurchaseRate_Mandatory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblPurchaseRate_Mandatory.Location = New System.Drawing.Point(126, 205)
        Me.LblPurchaseRate_Mandatory.Name = "LblPurchaseRate_Mandatory"
        Me.LblPurchaseRate_Mandatory.Size = New System.Drawing.Size(10, 7)
        Me.LblPurchaseRate_Mandatory.TabIndex = 1072
        Me.LblPurchaseRate_Mandatory.Text = "Ä"
        Me.LblPurchaseRate_Mandatory.Visible = False
        '
        'LblSaleRate_Mandatory
        '
        Me.LblSaleRate_Mandatory.AutoSize = True
        Me.LblSaleRate_Mandatory.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblSaleRate_Mandatory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblSaleRate_Mandatory.Location = New System.Drawing.Point(388, 207)
        Me.LblSaleRate_Mandatory.Name = "LblSaleRate_Mandatory"
        Me.LblSaleRate_Mandatory.Size = New System.Drawing.Size(10, 7)
        Me.LblSaleRate_Mandatory.TabIndex = 1073
        Me.LblSaleRate_Mandatory.Text = "Ä"
        Me.LblSaleRate_Mandatory.Visible = False
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(542, 60)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(376, 238)
        Me.Pnl1.TabIndex = 15
        '
        'FrmItemMaster_Aadhat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(944, 512)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LblSaleRate_Mandatory)
        Me.Controls.Add(Me.LblPurchaseRate_Mandatory)
        Me.Controls.Add(Me.TxtPurchaseRate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtHsn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtSpecification)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PnlRateType)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Controls.Add(Me.BtnBOMDetail)
        Me.Controls.Add(Me.BtnUnitConversion)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblItemCategory)
        Me.Controls.Add(Me.TxtItemGroup)
        Me.Controls.Add(Me.LblItemGroup)
        Me.Controls.Add(Me.TxtSalesTaxPostingGroup)
        Me.Controls.Add(Me.LblSalesTaxPostingGroup)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.TxtItemType)
        Me.Controls.Add(Me.TxtSaleRate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtUnit)
        Me.Controls.Add(Me.LblUnit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Controls.Add(Me.TxtItemCategory)
        Me.Name = "FrmItemMaster_Aadhat"
        Me.Text = "Item Master"
        Me.Controls.SetChildIndex(Me.TxtItemCategory, 0)
        Me.Controls.SetChildIndex(Me.LblDescription, 0)
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.LblUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtUnit, 0)
        Me.Controls.SetChildIndex(Me.LblManualCode, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtSaleRate, 0)
        Me.Controls.SetChildIndex(Me.TxtItemType, 0)
        Me.Controls.SetChildIndex(Me.TxtManualCode, 0)
        Me.Controls.SetChildIndex(Me.LblManualCodeReq, 0)
        Me.Controls.SetChildIndex(Me.LblSalesTaxPostingGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxPostingGroup, 0)
        Me.Controls.SetChildIndex(Me.LblItemGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtItemGroup, 0)
        Me.Controls.SetChildIndex(Me.LblItemCategory, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.BtnUnitConversion, 0)
        Me.Controls.SetChildIndex(Me.BtnBOMDetail, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.PnlRateType, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.TxtSpecification, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtHsn, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.TxtPurchaseRate, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblPurchaseRate_Mandatory, 0)
        Me.Controls.SetChildIndex(Me.LblSaleRate_Mandatory, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
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
        CType(Me.PicPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlCustomGrid.ResumeLayout(False)
        Me.PnlCustomGrid.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents PicPhoto As System.Windows.Forms.PictureBox
    Public WithEvents BtnBrowse As System.Windows.Forms.Button
    Public WithEvents BtnPhotoClear As System.Windows.Forms.Button
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents LblItemCategory As System.Windows.Forms.Label
    Public WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents TxtItemType As AgControls.AgTextBox
    Public WithEvents TxtSaleRate As AgControls.AgTextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents TxtItemCategory As AgControls.AgTextBox
    Public WithEvents TxtItemGroup As AgControls.AgTextBox
    Public WithEvents LblItemGroup As System.Windows.Forms.Label
    Public WithEvents TxtSalesTaxPostingGroup As AgControls.AgTextBox
    Public WithEvents LblSalesTaxPostingGroup As System.Windows.Forms.Label
    Public WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Public WithEvents TxtManualCode As AgControls.AgTextBox
    Public WithEvents LblManualCode As System.Windows.Forms.Label
    Public WithEvents TxtUnit As AgControls.AgTextBox
    Public WithEvents LblUnit As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid
    Public WithEvents BtnUnitConversion As System.Windows.Forms.Button
    Public WithEvents BtnBOMDetail As System.Windows.Forms.Button
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


        TxtSaleRate.AgMandatory = AgL.VNull(DtItemTypeSetting.rows(0)("IsMandatorySaleRate"))
        LblSaleRate_Mandatory.Visible = AgL.VNull(DtItemTypeSetting.rows(0)("IsMandatorySaleRate"))

        TxtPurchaseRate.AgMandatory = AgL.VNull(DtItemTypeSetting.rows(0)("IsMandatoryPurchaseRate"))
        LblPurchaseRate_Mandatory.Visible = AgL.VNull(DtItemTypeSetting.rows(0)("IsMandatoryPurchaseRate"))

        Dgl1.Rows(rowDefaultDiscountPerSale).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_SaleDiscountInMaster"))
        Dgl1.Rows(rowDefaultAdditionPerSale).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_SaleDiscountInMaster"))
        Dgl1.Rows(rowBarcode).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_Barcode"))

        DGLRateType.Columns(Col1Discount).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_SaleDiscountInMaster"))
        DGLRateType.Columns(Col1Addition).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_SaleDiscountInMaster"))

        ApplyItemTypeSetting(TxtItemType.Tag)
    End Sub


    Private Sub ApplyItemTypeSetting(ItemType As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        Dim mDglRateTypeColumnCount As Integer
        Try

            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName= '" & Me.Name & "'  And NCat = '" & ItemType & "' And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1Head, J).Value Then
                            Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                            Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            'MsgBox(NameOf(rowAdditionalDiscountPatternPurchase))
                        End If
                    Next
                Next
            End If
            If mDgl1RowCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True



            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='" & Me.Name & "' And NCat = '" & ItemType & "' And GridName ='" & DGLRateType.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To DGLRateType.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DGLRateType.Columns(J).Name Then
                            DGLRateType.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglRateTypeColumnCount += 1
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                DGLRateType.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                        End If
                    Next
                Next
            End If
            If mDglRateTypeColumnCount = 0 Then DGLRateType.Visible = False Else DGLRateType.Visible = True




        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub


    Private Sub FrmItemMaster_AadhatNew_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "DELETE FROM RateListDetail WHERE Code = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM RateList WHERE Code = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM UnitConversion WHERE Item = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM BOMDetail WHERE BaseItem = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation

        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Value, i).Value Is Nothing Then Dgl1.Item(Col1Value, i).Value = ""
            If Dgl1.Item(Col1Value, i).Tag Is Nothing Then Dgl1.Item(Col1Value, i).Tag = ""
        Next


        If AgL.RequiredField(TxtManualCode, LblManualCode.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtUnit, LblUnit.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtItemGroup, LblItemGroup.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtItemCategory, LblItemCategory.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtSalesTaxPostingGroup, LblSalesTaxPostingGroup.Text) Then passed = False : Exit Sub



        If Topctrl1.Mode = "Add" Then
            If AgL.PubServerName = "" Then
                TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
            Else
                TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
            End If


            mQry = "Select count(*) From Item Where ManualCode ='" & TxtManualCode.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Item Where ManualCode ='" & TxtManualCode.Text & "' And Code <>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If

    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = " Where H.V_Type='ITEM' AND IfNull(IT.Parent, IT.Code) In ('TP', '" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "') "
        mQry = "Select H.Code As SearchCode " &
                " From Item H  " &
                " Left Join ItemType IT On H.ItemType = IT.Code " & mConStr &
                " Order By H.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Public Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where I.V_Type='ITEM' AND Ifnull(IT.Parent, IT.Code) In ('TP','" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "')  "
        AgL.PubFindQry = "SELECT I.Code, I.ManualCode as [Item Code], I.Description [Item Description],I.Specification, " &
                        " IG.Description AS [Item Group], IC.Description AS [Item Category], IT.Name AS [Item Type], I.Unit, I.PurchaseRate as [Purchase Rate], I.Rate as [Sale Rate]  " &
                        " FROM Item I " &
                        " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup " &
                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                        "  " & mConStr
        AgL.PubFindQryOrdBy = "[Item Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"

        PrimaryField = "Code"

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans

#Region "Barcode Posting"
        Dim mCode As Integer = 0
        If Dgl1.Item(Col1Value, rowBarcode).Tag Is Nothing Then Dgl1.Item(Col1Value, rowBarcode).Tag = ""
        If Dgl1.Item(Col1Value, rowBarcode).Tag = "" Then
            If Dgl1.Item(Col1Value, rowBarcode).Value <> "" And Dgl1.Item(Col1Value, rowBarcode).Value <> Nothing Then
                mCode = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode", AgL.GCn).ExecuteScalar()
                mQry = " INSERT INTO Barcode (Code, Description, Item, Dimension1,
                    Dimension2, Dimension3, Dimension4, GenDocID, GenSr, Qty)
                    VALUES (" & AgL.Chk_Text(mCode) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcode).Value) & ", " & AgL.Chk_Text(SearchCode) & ",
                    Null, Null, Null, Null, " & AgL.Chk_Text(SearchCode) & ", 1, 0) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " INSERT INTO BarcodeSiteDetail (Code,Div_Code, Site_Code, LastTrnDocID,
                        LastTrnSr, LastTrnV_Type, LastTrnManualRefNo,
                        LastTrnSubcode, LastTrnProcess, CurrentGodown, Status)
                        VALUES (" & AgL.Chk_Text(mCode) & ", " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", 
                        " & AgL.Chk_Text(AgL.PubSiteCode) & ",
                        " & AgL.Chk_Text(SearchCode) & ", 1, Null, Null,
                        Null, Null, Null, 'Receive') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                Dgl1.Item(Col1Value, rowBarcode).Tag = mCode
            End If
        Else
            If Dgl1.Item(Col1Value, rowBarcode).Value <> "" And Dgl1.Item(Col1Value, rowBarcode).Value <> Nothing Then
                mQry = " UPDATE Barcode
                    Set Description = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcode).Value) & "
                    Where Code = '" & Dgl1.Item(Col1Value, rowBarcode).Tag & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Delete From BarcodeSiteDetail Where Code = '" & Dgl1.Item(Col1Value, rowBarcode).Tag & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "Delete From Barcode Where Code = '" & Dgl1.Item(Col1Value, rowBarcode).Tag & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                Dgl1.Item(Col1Value, rowBarcode).Tag = Nothing
            End If
        End If
#End Region

        mQry = "UPDATE Item " &
                " SET " &
                " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                " Specification = " & AgL.Chk_Text(TxtSpecification.Text) & ", " &
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " &
                " Hsn = " & AgL.Chk_Text(TxtHsn.Text) & ", " &
                " Unit = " & AgL.Chk_Text(TxtUnit.Text) & ", " &
                " PurchaseRate = " & Val(TxtPurchaseRate.Text) & ", " &
                " Rate = " & Val(TxtSaleRate.Text) & ", " &
                " ItemGroup = " & AgL.Chk_Text(TxtItemGroup.AgSelectedValue) & ", " &
                " ItemCategory = " & AgL.Chk_Text(TxtItemCategory.Tag) & ", " &
                " ItemType = " & AgL.Chk_Text(TxtItemType.Tag) & ", " &
                " StockYN = 1, " &
                " IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ", " &
                " MaintainStockYn = " & IIf(Dgl1.Item(Col1Value, rowMaintainStockYn).Value.ToUpper = "NO", 0, 1) & ", " &
                " ShowItemInOtherDivisions = " & IIf(Dgl1.Item(Col1Value, rowShowItemInOtherDivision).Value.ToUpper = "YES", 1, 0) & ", " &
                " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxPostingGroup.Text) & ", " &
                " Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcode).Tag) & ", " &
                " Default_DiscountPerSale = " & Val(Dgl1.Item(Col1Value, rowDefaultDiscountPerSale).Value) & "," &
                " Default_AdditionPerSale = " & Val(Dgl1.Item(Col1Value, rowDefaultAdditionPerSale).Value) & "," &
                " Default_DiscountPerPurchase = " & Val(Dgl1.Item(Col1Value, rowDefaultDiscountPerPurchase).Value) & "," &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                " DefaultSupplier = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDefaultSupplier).Tag) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Call FPostRateInRateList(Conn, Cmd)




        'If BtnUnitConversion.Tag IsNot Nothing Then
        '    Call FSaveUnitConversion(Conn, Cmd)
        'End If

        'If BtnBOMDetail.Tag IsNot Nothing Then
        '    Call FSaveBOMDetail(Conn, Cmd)
        'End If

        'mQry = "Delete From Item_Image Where Code = '" & mSearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        'mQry = "Insert Into Item_Image(Code, Photo) Values('" & mSearchCode & "', Null)"
        'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        TxtManualCode.AgLastValueText = TxtManualCode.Text
        'TxtUnit.AgLastValueTag = TxtUnit.Tag
        'TxtUnit.AgLastValueText = TxtUnit.Text
        'TxtSaleRate.AgLastValueText = TxtSaleRate.Text
        'TxtItemGroup.AgLastValueTag = TxtItemGroup.Tag
        'TxtItemGroup.AgLastValueText = TxtItemGroup.Text
        'TxtItemType.AgLastValueTag = TxtItemType.Tag
        'TxtItemType.AgLastValueText = TxtItemType.Text
        'TxtItemCategory.AgLastValueTag = TxtItemCategory.Tag
        'TxtItemCategory.AgLastValueText = TxtItemCategory.Text
        'TxtSalesTaxPostingGroup.AgLastValueTag = TxtSalesTaxPostingGroup.Tag
        'TxtSalesTaxPostingGroup.AgLastValueText = TxtSalesTaxPostingGroup.Text


    End Sub


    Private Sub FPostRateInRateList(ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer


        mQry = "DELETE FROM RateListDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "DELETE FROM RateList WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'bRateListCode = AgL.GetMaxId("RateList", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mQry = " INSERT INTO RateList(Code, WEF, RateType, EntryBy, EntryDate, EntryType, " &
                " EntryStatus, Status, Div_Code) " &
                " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                " NULL,	" & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                " " & AgL.Chk_Text(Topctrl1.Mode) & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                " '" & TxtDivision.AgSelectedValue & "')"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " &
              " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " &
              " 0, " & AgL.Chk_Date(AgL.PubStartDate) & ", " &
              " " & AgL.Chk_Text(mSearchCode) & ", " &
              " NULL, " & Val(TxtSaleRate.Text) & " ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To DGLRateType.RowCount - 1
            If DGLRateType.Item(Col1RateType, I).Value <> "" Then
                mSr += 1

                mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate, DiscountPer, AdditionPer) " &
              " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " &
              " " & mSr & ", " & AgL.Chk_Date(AgL.PubStartDate) & ", " &
              " " & AgL.Chk_Text(mSearchCode) & ", " &
              " " & AgL.Chk_Text(DGLRateType.Item(Col1RateType, I).Tag) & ", " & Val(DGLRateType.Item(Col1Rate, I).Value) & ", " & Val(DGLRateType.Item(Col1Discount, I).Value) & ", " & Val(DGLRateType.Item(Col1Addition, I).Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next



    End Sub

    'Private Sub FSaveUnitConversion(ByVal Conn As Object, ByVal Cmd As Object)
    '    Dim I As Integer
    '    mQry = "DELETE FROM UnitConversion WHERE Item = '" & mSearchCode & "'"
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '    If BtnUnitConversion.Tag IsNot Nothing Then
    '        With BtnUnitConversion.Tag.Dgl1
    '            For I = 0 To .Rows.Count - 1
    '                If .Item(FrmItemMaster_AadhatUnitConversion.Col1FromUnit, I).Value <> "" Then
    '                    mQry = " INSERT INTO UnitConversion ( Item,FromUnit,ToUnit,FromQty,ToQty,Multiplier,EntryBy,EntryDate,EntryType,EntryStatus, " &
    '                            " Status,Div_Code ) " &
    '                            " VALUES ( " & AgL.Chk_Text(mSearchCode) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMaster_AadhatUnitConversion.Col1FromUnit, I).Value) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMaster_AadhatUnitConversion.Col1ToUnit, I).Value) & ", " &
    '                            " " & Val(.Item(FrmItemMaster_AadhatUnitConversion.Col1FromQty, I).Value) & ", " &
    '                            " " & Val(.Item(FrmItemMaster_AadhatUnitConversion.Col1ToQty, I).Value) & ", " &
    '                            " " & Val(.Item(FrmItemMaster_AadhatUnitConversion.Col1Multiplier, I).Value) & ", " &
    '                            " '" & AgL.PubUserName & "'," & AgL.Chk_Text(AgL.PubLoginDate) & ",	'" & Topctrl1.Mode & "', " &
    '                            " 'Open',  '" & AgTemplate.ClsMain.EntryStatus.Active & "' , '" & TxtDivision.AgSelectedValue & "' ) "
    '                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '                End If
    '            Next
    '        End With
    '    End If
    'End Sub

    'Private Sub FSaveBOMDetail(ByVal Conn As Object, ByVal Cmd As Object)
    '    Dim I As Integer
    '    mQry = "DELETE FROM BOMDetail WHERE BaseItem = '" & mSearchCode & "'"
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '    If BtnBOMDetail.Tag IsNot Nothing Then
    '        With BtnBOMDetail.Tag.Dgl1
    '            For I = 0 To .Rows.Count - 1
    '                If .Item(FrmItemMaster_AadhatBOMDetail.Col1Item, I).Value <> "" Then
    '                    mQry = " INSERT INTO BomDetail ( Sr, Item, Qty, Process, Dimension1, Dimension2, " &
    '                            " Unit,WastagePer, BatchQty, BatchUnit, BaseItem ) " &
    '                            " VALUES ( " & I + 1 & "," &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMaster_AadhatBOMDetail.Col1Item, I).tag) & ", " &
    '                            " " & Val(.Item(FrmItemMaster_AadhatBOMDetail.Col1Qty, I).Value) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMaster_AadhatBOMDetail.Col1Process, I).tag) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMaster_AadhatBOMDetail.Col1Dimension1, I).tag) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMaster_AadhatBOMDetail.Col1Dimension2, I).tag) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMaster_AadhatBOMDetail.Col1Unit, I).Value) & ", " &
    '                            " " & Val(.Item(FrmItemMaster_AadhatBOMDetail.Col1WastagePer, I).Value) & ", " &
    '                            " " & Val(BtnBOMDetail.Tag.TxtBatchQty.Text) & ", " &
    '                            " " & AgL.Chk_Text(BtnBOMDetail.Tag.LblUnit.Text) & ", " &
    '                            " " & AgL.Chk_Text(mSearchCode) & "	) "
    '                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '                End If
    '            Next
    '        End With
    '    End If
    'End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        IniGrid()
        mQry = "Select I.*, Ig.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, " &
                " IT.Name AS ItemTypeName, IfNull(V.Cnt,0) AS Cnt, IG.Default_MarginPer, Ds.Name as DefaultSupplierName " &
                " From Item I " &
                " LEFT JOIN ItemGroup Ig ON I.ItemGroup = IG.Code " &
                " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                " Left Join viewHelpSubgroup Ds on I.DefaultSupplier = Ds.Code" &
                " LEFT JOIN ( SELECT L.BaseItem, count(*) AS Cnt  FROM BomDetail L GROUP BY L.BaseItem ) V ON V.BaseItem = I.Code " &
                " Where I.Code ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtSpecification.Text = AgL.XNull(.Rows(0)("Specification"))
                TxtHsn.Text = AgL.XNull(.Rows(0)("Hsn"))
                TxtUnit.Text = AgL.XNull(.Rows(0)("Unit"))
                TxtPurchaseRate.Text = AgL.VNull(.Rows(0)("PurchaseRate"))
                TxtPurchaseRate.Tag = AgL.VNull(.Rows(0)("PurchaseRate"))
                TxtSaleRate.Text = AgL.VNull(.Rows(0)("Rate"))
                TxtSaleRate.Tag = AgL.VNull(.Rows(0)("Rate"))

                TxtItemGroup.Tag = AgL.XNull(.Rows(0)("ItemGroup"))
                TxtItemGroup.Text = AgL.XNull(.Rows(0)("ItemGroupDesc"))
                gItemGroupDefaultMargin = AgL.VNull(.Rows(0)("Default_MarginPer"))
                TxtItemCategory.Text = AgL.XNull(.Rows(0)("ItemCategoryDesc"))
                TxtItemCategory.Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                TxtItemType.Text = AgL.XNull(.Rows(0)("ItemTypeName"))
                TxtItemType.Tag = AgL.XNull(.Rows(0)("ItemType"))

                FGetItemTypeSetting()
                TxtSalesTaxPostingGroup.Text = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False



                Dgl1.Item(Col1Value, rowDefaultDiscountPerSale).Value = Format(AgL.VNull(.Rows(0)("Default_DiscountPerSale")), "0.00")
                Dgl1.Item(Col1Value, rowDefaultAdditionPerSale).Value = Format(AgL.VNull(.Rows(0)("Default_AdditionPerSale")), "0.00")
                Dgl1.Item(Col1Value, rowDefaultDiscountPerPurchase).Value = Format(AgL.VNull(.Rows(0)("Default_DiscountPerPurchase")), "0.00")
                Dgl1.Item(Col1Value, rowShowItemInOtherDivision).Value = IIf((.Rows(0)("ShowItemInOtherDivisions")), "Yes", "No")
                Dgl1.Item(Col1Value, rowMaintainStockYn).Value = IIf((.Rows(0)("MaintainStockYn")), "Yes", "No")
                Dgl1.Item(Col1Value, rowMRP).Value = Format(AgL.VNull(.Rows(0)("MRP")), "0.00")
                Dgl1.Item(Col1Value, rowBarcode).Tag = AgL.XNull(.Rows(0)("Barcode"))
                Dgl1.Item(Col1Value, rowBarcode).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Barcode Where Code = '" & Dgl1.Item(Col1Value, rowBarcode).Tag & "'", AgL.GCn).ExecuteScalar)
                Dgl1.Item(Col1Value, rowDefaultSupplier).Tag = AgL.XNull(.Rows(0)("DefaultSupplier"))
                Dgl1.Item(Col1Value, rowDefaultSupplier).Value = AgL.XNull(.Rows(0)("DefaultSupplierName"))



                TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(ClsMain.Temp_NCat.Item, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.Tag = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.Tag



                If AgL.VNull(.Rows(0)("Cnt")) > 0 Then
                    BtnBOMDetail.ForeColor = Color.Red
                Else
                    BtnBOMDetail.ForeColor = Color.Black
                End If




                Dim I As Integer
                mQry = " Select  H.Code, H.Description, H.Margin, L.Rate, L.DiscountPer, L.AdditionPer 
                        From RateType H 
                        Left join RateListDetail L on L.RateType = H.Code And L.Item='" & SearchCode & "' 
                        Order By H.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGLRateType.RowCount = 1
                    DGLRateType.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGLRateType.Rows.Add()
                            DGLRateType.Item(ColSNo, I).Value = DGLRateType.Rows.Count - 1
                            DGLRateType.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("Code"))
                            DGLRateType.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("Description"))
                            DGLRateType.Item(Col1Margin, I).Value = Format(AgL.VNull(.Rows(I)("Margin")), "0.00")
                            DGLRateType.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                            DGLRateType.Item(Col1Discount, I).Value = Format(AgL.VNull(.Rows(I)("DiscountPer")), "0.00")
                            DGLRateType.Item(Col1Addition, I).Value = Format(AgL.VNull(.Rows(I)("AdditionPer")), "0.00")
                        Next I
                        DGLRateType.Visible = True
                    Else
                        DGLRateType.Visible = False
                    End If
                End With

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))
            End If
        End With






        DsTemp = Nothing


        '-------------------------------------------------------------
        'Image Show
        '-------------------------------------------------------------

        'mQry = "Select Im.* " &
        '        " From Item_Image Im Where Code='" & mSearchCode & "'"
        'DsTemp = AgL.FillData(mQry, AgL.GCn)
        'With DsTemp.Tables(0)
        '    If .Rows.Count > 0 Then
        '        If Not IsDBNull(.Rows(0)("Photo")) Then
        '            Photo_Byte = DirectCast(.Rows(0)("Photo"), Byte())
        '            Show_Picture(PicPhoto, Photo_Byte)
        '        End If
        '    End If
        'End With

        TxtUnit.AgLastValueTag = ""
        TxtUnit.AgLastValueText = ""
        TxtSaleRate.AgLastValueText = 0
        TxtItemGroup.AgLastValueTag = ""
        TxtItemGroup.AgLastValueText = ""
        TxtItemType.AgLastValueTag = ""
        TxtItemType.AgLastValueText = ""
        TxtItemCategory.AgLastValueTag = ""
        TxtItemCategory.AgLastValueText = ""
        TxtSalesTaxPostingGroup.AgLastValueTag = ""
        TxtSalesTaxPostingGroup.AgLastValueText = ""

    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtItemCategory.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtItemCategory.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown, TxtManualCode.KeyDown, TxtUnit.KeyDown, TxtSalesTaxPostingGroup.KeyDown, TxtItemGroup.KeyDown, TxtItemCategory.KeyDown, TxtSpecification.KeyDown
        Try
            Select Case sender.Name
                Case TxtDescription.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtDescription.AgHelpDataSet Is Nothing Then
                            mQry = "Select I.Code, I.Description As Name, I.Div_Code, I.ItemType " &
                                    " From Item I " &
                                    " Left Join ItemType IT On I.ItemType = IT.Code " &
                                    " Where IfNull(IT.Parent,IT.Code) in ('" & ItemTypeCode.TradingProduct & "','" & ItemTypeCode.OtherProduct & "')" &
                                    " Order By I.Description"
                            TxtDescription.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtSpecification.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtSpecification.AgHelpDataSet Is Nothing Then
                            mQry = "Select Specification Code, Specification " &
                                    " From Item Where ItemGroup = '" & TxtItemGroup.Tag & "' Group By Specification" &
                                    " Order By Specification "
                            TxtSpecification.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtManualCode.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtManualCode.AgHelpDataSet Is Nothing Then
                            mQry = "Select I.Code, I.ManualCode As ItemCode, I.Div_Code, I.ItemType " &
                                    " From Item I " &
                                    " Left Join ItemType IT On I.ItemType = IT.Code " &
                                    " Where IfNull(IT.Parent, IT.Code) in ('" & ItemTypeCode.TradingProduct & "','" & ItemTypeCode.OtherProduct & "')" &
                                    " Order By I.ManualCode "
                            TxtManualCode.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtUnit.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtUnit.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Code, Code AS Unit FROM Unit "
                            TxtUnit.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If





                Case TxtSalesTaxPostingGroup.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtSalesTaxPostingGroup.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Description as  Code, Description AS PostingGroupSalesTaxItem FROM PostingGroupSalesTaxItem "
                            TxtSalesTaxPostingGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If




                Case TxtItemGroup.Name
                    If e.KeyCode = Keys.Insert Then
                        FOpenItemGroupMaster()
                    Else
                        If TxtItemGroup.AgHelpDataSet Is Nothing Then
                            If e.KeyCode <> Keys.Enter Then
                                If DtItemTypeSetting.rows(0)("IsItemGroupLinkedWithItemCategory") Then
                                    mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                                        " From ItemGroup I " &
                                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                                        " WHERE I.ItemType = '" & TxtItemType.Tag & "' "

                                    '" WHERE I.ItemCategory='" & TxtItemCategory.Tag & "' 
                                    'And I.ItemType = '" & TxtItemType.Tag & "' "


                                    If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
                                        mQry += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemGroupInOtherDivisions,0) =1) "
                                    End If

                                    If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
                                        mQry += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemGroupInOtherSites,0) =1) "
                                    End If

                                    TxtItemGroup.AgHelpDataSet(4) = AgL.FillData(mQry, AgL.GCn)
                                Else
                                    mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                                        " From ItemGroup I " &
                                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                                        " WHERE  I.ItemType = '" & TxtItemType.Tag & "' "
                                    If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
                                        mQry += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemGroupInOtherDivisions,0) =1) "
                                    End If
                                    If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
                                        mQry += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemGroupInOtherSites,0) =1) "
                                    End If
                                    TxtItemGroup.AgHelpDataSet(4) = AgL.FillData(mQry, AgL.GCn)
                                End If
                            End If
                        End If
                    End If

                Case TxtItemCategory.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtItemCategory.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT IC.Code, IC.Description, IC.ItemType, IT.Name as ItemTypeName, IC.SalesTaxGroup, IC.Unit, IC.Hsn 
                                    FROM ItemCategory IC 
                                    Left Join ItemType IT On IC.ItemType = IT.Code                                     
                                    Where IfNull(IT.Parent, IT.Code) in ('TP','" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "') Order by IC.Description  "
                            TxtItemCategory.AgHelpDataSet(4) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub SetProductName()
        TxtDescription.Text = TxtSpecification.Text + "-" + TxtHsn.Text + Space(10) + "[" + TxtItemGroup.Text + " | " + TxtItemCategory.Text + "]"
    End Sub
    Sub FillRateTypeForItemGroup(ItemGroup As String)
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mQry As String

        mQry = " Select  H.Code, H.Description, IGRT.Margin 
                            from RateType H 
                            Left Join ItemGroupRateType IGRT On H.Code = IGRT.RateType And IGRT.Code = '" & ItemGroup & "'
                            Order By H.Sr "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        With DtTemp
            DGLRateType.RowCount = 1
            DGLRateType.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To .Rows.Count - 1
                    DGLRateType.Rows.Add()
                    DGLRateType.Item(ColSNo, I).Value = DGLRateType.Rows.Count - 1
                    DGLRateType.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("Code"))
                    DGLRateType.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("Description"))
                    DGLRateType.Item(Col1Margin, I).Value = Format(AgL.VNull(.Rows(I)("Margin")), "0.00")
                Next I
                'DGLRateType.Visible = True
            Else
                DGLRateType.Visible = False
            End If
        End With

    End Sub
    Public Sub Validate_ItemCategory()
        Dim mQry As String
        Dim DtTemp As DataTable
        mQry = "SELECT IC.Code, IC.Description, IC.ItemType, IT.Name as ItemTypeName, IC.SalesTaxGroup, IC.Unit, IC.Hsn 
                FROM ItemCategory IC 
                Left Join ItemType IT On IC.ItemType = IT.Code 
                Where IC.Code = '" & TxtItemCategory.Tag & "'  "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            TxtItemType.Text = AgL.XNull(DtTemp.Rows(0)("ItemTypeName"))
            TxtItemType.Tag = AgL.XNull(DtTemp.Rows(0)("ItemType"))
            FGetItemTypeSetting()
            TxtUnit.Tag = AgL.XNull(DtTemp.Rows(0)("Unit"))
            TxtUnit.Text = AgL.XNull(DtTemp.Rows(0)("Unit"))
            TxtSalesTaxPostingGroup.Text = AgL.XNull(DtTemp.Rows(0)("SalesTaxGroup"))
            TxtSalesTaxPostingGroup.Tag = AgL.XNull(DtTemp.Rows(0)("SalesTaxGroup"))
            TxtHsn.Text = AgL.XNull(DtTemp.Rows(0)("Hsn"))

        Else
            TxtItemType.Text = ""
            TxtItemType.Tag = ""
            TxtUnit.AgSelectedValue = ""
            TxtSalesTaxPostingGroup.AgSelectedValue = ""
            TxtHsn.Text = ""
        End If

        TxtSpecification.Text = TxtItemCategory.Text
        TxtItemGroup.AgHelpDataSet = Nothing
        SetProductName()
    End Sub
    Public Sub Validate_ItemGroup()
        TxtSpecification.AgHelpDataSet = Nothing
        SetProductName()
        gItemGroupDefaultMargin = AgL.Dman_Execute("Select IfNull(Default_MarginPer,0) From ItemGroup Where Code ='" & TxtItemGroup.Tag & "'", AgL.GCn).ExecuteScalar
        If TxtItemGroup.Tag <> "" Then
            If Topctrl1.Mode = "Edit" Then
                If TxtItemGroup.AgLastValueTag <> TxtItemGroup.Tag Then
                    FillRateTypeForItemGroup(TxtItemGroup.Tag)
                End If
            Else
                FillRateTypeForItemGroup(TxtItemGroup.Tag)
            End If
        End If
    End Sub
    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtItemGroup.Validating, TxtItemCategory.Validating, TxtSaleRate.Validating, TxtSpecification.Validating, TxtPurchaseRate.Validating, TxtHsn.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Dim i As Integer
        Try
            Select Case sender.NAME
                Case TxtItemCategory.Name
                    'If sender.text.ToString.Trim <> "" Then
                    '    If sender.AgHelpDataSet IsNot Nothing Then
                    '        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                    '        TxtItemType.Text = AgL.XNull(DrTemp(0)("ItemTypeName"))
                    '        TxtItemType.Tag = AgL.XNull(DrTemp(0)("ItemType"))
                    '        FGetItemTypeSetting()
                    '        TxtUnit.Tag = AgL.XNull(DrTemp(0)("Unit"))
                    '        TxtUnit.Text = AgL.XNull(DrTemp(0)("Unit"))
                    '        TxtSalesTaxPostingGroup.Text = AgL.XNull(DrTemp(0)("SalesTaxGroup"))
                    '        TxtSalesTaxPostingGroup.Tag = AgL.XNull(DrTemp(0)("SalesTaxGroup"))
                    '        TxtHsn.Text = AgL.XNull(DrTemp(0)("Hsn"))
                    '    End If
                    'Else
                    '    TxtItemType.Text = ""
                    '    TxtItemType.Tag = ""
                    '    TxtUnit.AgSelectedValue = ""
                    '    TxtSalesTaxPostingGroup.AgSelectedValue = ""
                    '    TxtHsn.Text = ""
                    'End If

                    'TxtItemGroup.AgHelpDataSet = Nothing
                    'SetProductName()
                    Validate_ItemCategory()

                Case TxtItemGroup.Name
                    'SetProductName()
                    'gItemGroupDefaultMargin = AgL.Dman_Execute("Select IfNull(Default_MarginPer,0) From ItemGroup Where Code ='" & TxtItemGroup.Tag & "'", AgL.GCn).ExecuteScalar
                    'If TxtItemGroup.Tag <> "" Then
                    '    If Topctrl1.Mode = "Edit" Then
                    '        If TxtItemGroup.AgLastValueTag <> TxtItemGroup.Tag Then
                    '            FillRateTypeForItemGroup(TxtItemGroup.Tag)
                    '        End If
                    '    Else
                    '        FillRateTypeForItemGroup(TxtItemGroup.Tag)
                    '    End If
                    'End If
                    Validate_ItemGroup()
                Case TxtSpecification.Name
                    SetProductName()

                Case TxtHsn.Name
                    SetProductName()


                Case TxtPurchaseRate.Name, TxtSaleRate.Name
                    If gItemGroupDefaultMargin > 0 Then
                        If Val(TxtSaleRate.Text) = 0 Or Val(TxtSaleRate.Text) = Math.Round(Val(TxtPurchaseRate.AgLastValueText) + (Val(TxtPurchaseRate.AgLastValueText) * gItemGroupDefaultMargin / 100), 0) Then
                            TxtSaleRate.Text = Format(Math.Round(Val(TxtPurchaseRate.Text) + (Val(TxtPurchaseRate.Text) * gItemGroupDefaultMargin / 100), 0), "0.00")
                        End If
                    End If

                    If Val(TxtSaleRate.Tag) = 0 Then
                        For i = 0 To DGLRateType.RowCount - 1
                            If DGLRateType.Item(Col1RateType, i).Value <> "" Then
                                DGLRateType.Item(Col1Rate, i).Value = Format(Math.Round(Val(TxtSaleRate.Text) + (Val(TxtSaleRate.Text) * Val(DGLRateType.Item(Col1Margin, i).Value) / 100), 0), "0.00")
                            End If
                        Next
                    Else
                        If Val(TxtSaleRate.Tag) <> Val(TxtSaleRate.Text) Then
                            If DGLRateType.Visible = True Then
                                If DGLRateType.Rows.Count >= 1 Then
                                    If DGLRateType.Item(Col1RateType, 0).Value <> "" Then
                                        If MsgBox("Do you want to update all rate types", vbYesNo) = vbYes Then
                                            For i = 0 To DGLRateType.RowCount - 1
                                                If DGLRateType.Item(Col1RateType, i).Value <> "" Then
                                                    DGLRateType.Item(Col1Rate, i).Value = Format(Math.Round(Val(TxtSaleRate.Text) + (Val(TxtSaleRate.Text) * Val(DGLRateType.Item(Col1Margin, i).Value) / 100), 0), "0.00")
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            End If
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
        ''AgL.WinSetting(Me, 520, 950, 0, 0)
        AgCustomGrid1.FrmType = Me.FrmType
        FManageSystemDefine()
        Try
            If DGLRateType.Rows.Count <= 1 Then
                DGLRateType.Visible = False
            End If
        Catch ex As Exception
        End Try

        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuImportFromExcel.Visible = False
            MnuImportFromTally.Visible = False
            MnuImportFromDos.Visible = False
            MnuImportRateListFromDos.Visible = False
            MnuImportRateListFromExcel.Visible = False
        End If
        'MnuBulkEdit.Visible = False


    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtManualCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If TxtDescription.Text = "" Then TxtDescription.Text = TxtManualCode.Text
    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Private Sub FrmFinishedItem_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If TxtDescription.AgHelpDataSet IsNot Nothing Then TxtDescription.AgHelpDataSet = Nothing
        If TxtManualCode.AgHelpDataSet IsNot Nothing Then TxtManualCode.AgHelpDataSet = Nothing
        If TxtSalesTaxPostingGroup.AgHelpDataSet IsNot Nothing Then TxtSalesTaxPostingGroup.AgHelpDataSet = Nothing
        If TxtUnit.AgHelpDataSet IsNot Nothing Then TxtUnit.AgHelpDataSet = Nothing
        If TxtItemGroup.AgHelpDataSet IsNot Nothing Then TxtItemGroup.AgHelpDataSet = Nothing
        If TxtItemCategory.AgHelpDataSet IsNot Nothing Then TxtItemCategory.AgHelpDataSet = Nothing
    End Sub

    Private Sub FrmItemMaster_Aadhat_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtItemType.Enabled = False

        TxtDescription.Enabled = False
        ChkIsSystemDefine.Enabled = False
        If DGLRateType.Rows.Count <= 1 Then DGLRateType.Visible = False


    End Sub

    Private Sub FrmItemMaster_Aadhat_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim DsTemp As DataSet
        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(ClsMain.Temp_NCat.Item, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag
        IniGrid()


        If AgL.PubServerName = "" Then
            TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        Else
            TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        End If



        TxtUnit.Tag = TxtUnit.AgLastValueTag
        TxtUnit.Text = TxtUnit.AgLastValueText
        TxtItemCategory.Tag = TxtItemCategory.AgLastValueTag
        TxtItemCategory.Text = TxtItemCategory.AgLastValueText
        TxtItemGroup.Tag = TxtItemGroup.AgLastValueTag
        TxtItemGroup.Text = TxtItemGroup.AgLastValueText
        TxtItemType.Tag = TxtItemType.AgLastValueTag
        TxtItemType.Text = TxtItemType.AgLastValueText
        TxtSalesTaxPostingGroup.Tag = TxtSalesTaxPostingGroup.AgLastValueTag
        TxtSalesTaxPostingGroup.Text = TxtSalesTaxPostingGroup.AgLastValueText


        'Patch
        TxtHsn.Text = TxtHsn.AgLastValueText
        TxtSaleRate.Text = 1
        TxtPurchaseRate.Text = 1
        Dgl1.Visible = False



        Dgl1.Item(Col1Value, rowMaintainStockYn).Value = "YES"


        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()
    End Sub

    Private Sub FrmItemMaster_Aadhat_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        'AgCustomGrid1.Ini_Grid(mSearchCode)
        '    AgCustomGrid1.SplitGrid = False

        DGLRateType.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGLRateType, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DGLRateType, Col1RateType, 120, 0, Col1RateType, True, True, False)
            .AddAgNumberColumn(DGLRateType, Col1Margin, 60, 2, 2, False, Col1Margin, True, True, True)
            .AddAgNumberColumn(DGLRateType, Col1Rate, 90, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(DGLRateType, Col1Discount, 90, 8, 2, False, Col1Discount, True, False, True)
            .AddAgNumberColumn(DGLRateType, Col1Addition, 90, 8, 2, False, Col1Addition, True, False, True)
        End With
        AgL.AddAgDataGrid(DGLRateType, PnlRateType)
        DGLRateType.EnableHeadersVisualStyles = False
        DGLRateType.AgSkipReadOnlyColumns = True
        DGLRateType.RowHeadersVisible = False
        DGLRateType.Visible = False
        AgL.GridDesign(DGLRateType)
        DGLRateType.Name = "DGLRateType"


        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 150, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 170, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersVisible = False


        Dgl1.Rows.Add(8)
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1.Rows(I).Visible = False
        Next

        Dgl1.Name = "Dgl1"
        Dgl1.Tag = "VerticalGrid"
    End Sub

    Private Sub BtnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBrowse.Click, BtnPhotoClear.Click
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Select Case sender.Name
            Case BtnBrowse.Name
                AgL.GetPicture(PicPhoto, Photo_Byte)
                If Photo_Byte.Length > 20480 Then Photo_Byte = Nothing : PicPhoto.Image = Nothing : MsgBox("Image Size Should not be Greater Than 20 KB ")

            Case BtnPhotoClear.Name
                Photo_Byte = Nothing
                PicPhoto.Image = Nothing
        End Select
    End Sub

    Sub Show_Picture(ByVal PicBox As PictureBox, ByVal B As Byte())
        Dim Mem As MemoryStream
        Dim Img As Image

        Mem = New MemoryStream(B)
        Img = Image.FromStream(Mem)
        PicBox.Image = Img
    End Sub

    Sub Update_Picture(ByVal mTable As String, ByVal mColumn As String, ByVal mCondition As String, ByVal ByteArr As Byte())
        If ByteArr Is Nothing Then Exit Sub
        Dim sSQL As String = "Update " & mTable & " Set " & mColumn & "=@pic " & mCondition

        Dim cmd As SQLiteCommand = New SQLiteCommand(sSQL, AgL.GCn)
        Dim Pic As SQLiteParameter = New SQLiteParameter("@pic", SqlDbType.Image)
        Pic.Value = ByteArr
        cmd.Parameters.Add(Pic)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub FrmItemMaster_Aadhat_BaseEvent_Save_PostTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        Call Update_Picture("Item_Image", "Photo", "Where Code = '" & mSearchCode & "'", Photo_Byte)
    End Sub

    Private Sub FCreateHelpItemGroup()
        Dim strCond As String = ""

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemGroupInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemGroupInOtherSites,0) =1) "
        End If

        mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                " From ItemGroup I " &
                " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " & strCond
        TxtItemGroup.AgHelpDataSet(3) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FOpenItemGroupMaster()
        Dim DrTemp As DataRow() = Nothing
        Dim bStrCode$ = ""
        bStrCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Group Master", "")
        FCreateHelpItemGroup()
        DrTemp = TxtItemGroup.AgHelpDataSet.Tables(0).Select("Code = '" & bStrCode & "'")
        TxtItemGroup.Tag = bStrCode
        TxtItemGroup.Text = AgL.XNull(AgL.Dman_Execute("Select Description From ItemGroup Where Code = '" & bStrCode & "'", AgL.GCn).ExecuteScalar)
        TxtSpecification.Focus()
        SendKeys.Send("{Enter}")
    End Sub

    'Private Sub BtnRateConversion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUnitConversion.Click, BtnBOMDetail.Click
    '    Select Case sender.Name
    '        Case BtnUnitConversion.Name
    '            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
    '                FMoveRecItemUnitConversion(mSearchCode)
    '                BtnUnitConversion.Tag.ShowDialog()
    '            Else
    '                FillUnitConversionDetail(True)
    '            End If

    '        Case BtnBOMDetail.Name
    '            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
    '                FMoveRecItemBOMDetail(mSearchCode)
    '                BtnBOMDetail.Tag.Text = TxtDescription.Text
    '                BtnBOMDetail.Tag.StartPosition = FormStartPosition.CenterParent
    '                BtnBOMDetail.Tag.ShowDialog()
    '            Else
    '                FillBOMDetail(True)
    '            End If

    '    End Select
    'End Sub

    'Private Sub FillUnitConversionDetail(ByVal ShowWindow As Boolean)
    '    If BtnUnitConversion.Tag Is Nothing Then
    '        FMoveRecItemUnitConversion(mSearchCode)
    '        If BtnUnitConversion.Tag Is Nothing Then
    '            BtnUnitConversion.Tag = FunRetNewUnitConversionObject()
    '        End If
    '    End If

    '    BtnUnitConversion.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
    '    BtnUnitConversion.Tag.LblItemName.Text = TxtDescription.Text
    '    BtnUnitConversion.Tag.LblItemName.Tag = mSearchCode
    '    BtnUnitConversion.Tag.EntryMode = Topctrl1.Mode
    '    BtnUnitConversion.Tag.Unit = TxtUnit.Text

    '    If ShowWindow = True Then BtnUnitConversion.Tag.ShowDialog()
    'End Sub

    'Private Function FunRetNewUnitConversionObject() As Object
    '    Dim FrmObj As FrmItemMaster_AadhatUnitConversion
    '    Try
    '        FrmObj = New FrmItemMaster_AadhatUnitConversion
    '        FrmObj.IniGrid()
    '        FunRetNewUnitConversionObject = FrmObj
    '    Catch ex As Exception
    '        FunRetNewUnitConversionObject = Nothing
    '        MsgBox(ex.Message)
    '    End Try
    'End Function

    'Public Sub FMoveRecItemUnitConversion(ByVal SearchCode As String)
    '    Dim DtTemp As DataTable = Nothing
    '    Dim I As Integer = 0
    '    Try
    '        BtnUnitConversion.Tag = FunRetNewUnitConversionObject()
    '        BtnUnitConversion.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
    '        mQry = " SELECT U.*, I.Description AS ItemDesc " &
    '                " FROM UnitConversion U " &
    '                " LEFT JOIN Item I ON U.Item = I.Code  " &
    '                " WHERE U.Item = '" & SearchCode & "' "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '        With DtTemp
    '            BtnUnitConversion.Tag.Dgl1.RowCount = 1 : BtnUnitConversion.Tag.Dgl1.Rows.Clear()
    '            If DtTemp.Rows.Count > 0 Then
    '                For I = 0 To DtTemp.Rows.Count - 1
    '                    BtnUnitConversion.Tag.Dgl1.Rows.Add()
    '                    BtnUnitConversion.Tag.LblItemName.Text = AgL.XNull(.Rows(I)("ItemDesc"))
    '                    BtnUnitConversion.Tag.LblItemName.tag = AgL.XNull(.Rows(I)("Item"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMaster_AadhatUnitConversion.ColSNo, I).Value = BtnUnitConversion.Tag.Dgl1.Rows.Count - 1
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMaster_AadhatUnitConversion.Col1FromUnit, I).Value = AgL.XNull(.Rows(I)("FromUnit"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMaster_AadhatUnitConversion.Col1FromQty, I).Value = AgL.VNull(.Rows(I)("FromQty"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMaster_AadhatUnitConversion.Col1ToUnit, I).Value = AgL.XNull(.Rows(I)("ToUnit"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMaster_AadhatUnitConversion.Col1ToQty, I).Value = AgL.VNull(.Rows(I)("ToQty"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMaster_AadhatUnitConversion.Col1Multiplier, I).Value = AgL.VNull(.Rows(I)("Multiplier"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMaster_AadhatUnitConversion.Col1Equal, I).Value = "="

    '                    BtnUnitConversion.Tag.EntryMode = Topctrl1.Mode
    '                Next I
    '            End If
    '        End With

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub FillBOMDetail(ByVal ShowWindow As Boolean)
    '    If BtnBOMDetail.Tag Is Nothing Then
    '        FMoveRecItemBOMDetail(mSearchCode)
    '        If BtnBOMDetail.Tag Is Nothing Then
    '            BtnBOMDetail.Tag = FunRetNewBOMDetailObject()
    '        End If
    '    End If

    '    BtnBOMDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
    '    BtnBOMDetail.Tag.LblItemName.Text = TxtDescription.Text
    '    BtnBOMDetail.Tag.LblItemName.Tag = mSearchCode
    '    BtnBOMDetail.Tag.EntryMode = Topctrl1.Mode
    '    BtnBOMDetail.Tag.LblUnit.Text = TxtUnit.Text

    '    If ShowWindow = True Then BtnBOMDetail.Tag.ShowDialog()
    'End Sub

    'Private Function FunRetNewBOMDetailObject() As Object
    '    Dim FrmObj As FrmItemMaster_AadhatBOMDetail
    '    Try
    '        FrmObj = New FrmItemMaster_AadhatBOMDetail
    '        FrmObj.IniGrid()
    '        FunRetNewBOMDetailObject = FrmObj
    '    Catch ex As Exception
    '        FunRetNewBOMDetailObject = Nothing
    '        MsgBox(ex.Message)
    '    End Try
    'End Function

    'Public Sub FMoveRecItemBOMDetail(ByVal SearchCode As String)
    '    Dim DtTemp As DataTable = Nothing
    '    Dim I As Integer = 0
    '    Try
    '        BtnBOMDetail.Tag = FunRetNewBOMDetailObject()
    '        BtnBOMDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
    '        mQry = " SELECT BD.*, IB.Description AS BaseItemDesc , I.Description AS ItemDesc , P.Description AS ProcessDesc, " &
    '                " D1.Description AS Dimension1Desc, D2.Description AS Dimension2Desc, IfNull(V.Cnt,0) AS Cnt " &
    '                " FROM BomDetail BD " &
    '                " LEFT JOIN Item IB On IB.Code = BD.BaseItem  " &
    '                " LEFT JOIN Process P ON P.NCat = BD.Process  " &
    '                " LEFT JOIN Dimension1 D1 ON D1.Code = BD.Dimension1  " &
    '                " LEFT JOIN Dimension2 D2 ON D2.Code = BD.Dimension2  " &
    '                " LEFT JOIN Item I On I.Code = BD.Item  " &
    '                " LEFT JOIN ( SELECT L.BaseItem, count(*) AS Cnt  FROM BomDetail L GROUP BY L.BaseItem ) V ON V.BaseItem = BD.Item " &
    '                " WHERE BD.BaseItem = '" & SearchCode & "' " &
    '                " ORDER BY BD.Sr "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        With DtTemp
    '            BtnBOMDetail.Tag.Dgl1.RowCount = 1 : BtnBOMDetail.Tag.Dgl1.Rows.Clear()
    '            If DtTemp.Rows.Count > 0 Then
    '                For I = 0 To DtTemp.Rows.Count - 1
    '                    BtnBOMDetail.Tag.Dgl1.Rows.Add()
    '                    BtnBOMDetail.Tag.LblItemName.Text = AgL.XNull(.Rows(I)("BaseItemDesc"))
    '                    BtnBOMDetail.Tag.LblItemName.tag = AgL.XNull(.Rows(I)("BaseItem"))
    '                    BtnBOMDetail.Tag.LblUnit.Text = AgL.XNull(.Rows(I)("BatchUnit"))
    '                    BtnBOMDetail.Tag.TxtBatchQty.Text = AgL.VNull(.Rows(I)("BatchQty"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.ColSNo, I).Value = BtnBOMDetail.Tag.Dgl1.Rows.Count - 1
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Process, I).Value = AgL.XNull(.Rows(I)("ProcessDesc"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1WastagePer, I).Value = AgL.VNull(.Rows(I)("WastagePer"))
    '                    If AgL.VNull(.Rows(I)("Cnt")) > 0 Then
    '                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMaster_AadhatBOMDetail.Col1BtnBOMDetail, I).Style.ForeColor = Color.Red
    '                    End If
    '                    BtnBOMDetail.Tag.EntryMode = Topctrl1.Mode
    '                Next I
    '            End If
    '        End With

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, ItemTypeCode.TradingProduct, "", "", "", "")
        FGetSettings = mValue
    End Function

    Private Sub FrmItemMaster_AadhatNew_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Photo_Byte = Nothing
        PicPhoto.Image = Nothing
        BtnUnitConversion.Tag = Nothing
        BtnBOMDetail.Tag = Nothing
        gItemGroupDefaultMargin = 0

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


    Private Sub DGLRateType_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGLRateType.EditingControlShowing
        If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Lower
        End If
    End Sub
    Private Sub FrmItemMaster_Aadhat_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()

        If Not AgL.StrCmp(TxtDivision.AgSelectedValue, AgL.PubDivCode) And TxtDivision.Text <> "" Then

            If MsgBox("Different Division Record. Do you want to modify it.", MsgBoxStyle.YesNo, "Validation") = vbNo Then
                Topctrl1.FButtonClick(14, True)
                Exit Sub
            Else
                TxtDivision.ReadOnly = False
            End If
        End If

    End Sub

    Private Sub FrmItemMaster_Aadhat_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
        If Passed = False Then Exit Sub
        Passed = Not FGetRelationalData()

        If TxtDivision.Text <> "" Then
            If Not AgL.StrCmp(TxtDivision.AgSelectedValue, AgL.PubDivCode) Then
                MsgBox("Different Division Record. Can't Modify!", MsgBoxStyle.OkOnly, "Validation") : Passed = False : Exit Sub
            End If
        End If


    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From SaleInvoiceDetail Where Item = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Item " & TxtDescription.Text & " In Sale Invoice . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From PurchInvoiceDetail Where Item = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Item " & TxtDescription.Text & " In Purchase Invoice . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From Stock Where Item = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Item " & TxtDescription.Text & " In Stock. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
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

    Private Sub DglRateType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGLRateType.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            If DGLRateType.CurrentCell.ColumnIndex = DGLRateType.Columns(Col1Rate).Index Then
                If DGLRateType.Item(DGLRateType.CurrentCell.ColumnIndex, DGLRateType.CurrentCell.RowIndex).Value Is Nothing Then DGLRateType.Item(DGLRateType.CurrentCell.ColumnIndex, DGLRateType.CurrentCell.RowIndex).Value = ""
                If Val(DGLRateType.Item(DGLRateType.CurrentCell.ColumnIndex, DGLRateType.CurrentCell.RowIndex).Value) = 0 Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FrmItemMaster_Aadhat_Cloth_BaseEvent_ApproveDeletion_PreTrans(SearchCode As String) Handles Me.BaseEvent_ApproveDeletion_PreTrans

    End Sub

    'Public Sub FImport()
    '    Dim mTrans As String = ""
    '    Dim DtTemp As DataTable
    '    Dim DtMain As DataTable = Nothing
    '    Dim I As Integer
    '    'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
    '    Dim StrErrLog As String = ""
    '    mQry = "Select '' as Srl, 'Item Code' as [Field Name], 'Text' as [Data Type], 10 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 50 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'Item Display Name' as [Field Name], 'Text' as [Data Type], 50 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'Item Group' as [Field Name], 'Text' as [Data Type], 50 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'Item Category' as [Field Name], 'Text' as [Data Type], 50 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 50 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 20 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'Purchase Rate' as [Field Name], 'Number' as [Data Type], 0 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'Sale Rate' as [Field Name], 'Number' as [Data Type], 0 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'GST Per' as [Field Name], 'Text' as [Data Type], 20 as [Length] "
    '    mQry = mQry + "Union All Select  '' as Srl,'HSN Code' as [Field Name], 'Text' as [Data Type], 20 as [Length] "
    '    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '    Dim ObjFrmImport As New FrmImportFromExcel
    '    ObjFrmImport.LblTitle.Text = "Item Master Import"
    '    ObjFrmImport.Dgl1.DataSource = DtTemp
    '    ObjFrmImport.ShowDialog()

    '    If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

    '    DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)

    '    Try

    '        AgL.ECmd = AgL.GCn.CreateCommand
    '        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
    '        AgL.ECmd.Transaction = AgL.ETrans
    '        mTrans = "Begin"

    '        mQry = " CREATE TEMP TABLE [TempTable] (
    '                   [RowNumber] Int,
    '                   [ItemCode] nvarchar(10) ,
    '                   [ItemName] nvarchar(50) ,
    '                   [ItemDisplayName] nvarchar(50) ,
    '                   [ItemGroup] nvarchar(10) ,
    '                   [ItemCategory] nvarchar(10) ,
    '                   [Specification] nvarchar(10) ,
    '                   [Unit] nvarchar(10) ,
    '                   [PurchaseRate] float, 
    '                   [SaleRate] float, 
    '                   [GSTPer] float, 
    '                   [HSNCode] nvarchar(10) 
    '                )"
    '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


    '        For I = 0 To DtTemp.Rows.Count - 1
    '            mQry = "INSERT INTO [TempTable] (RowNumber, ItemCode, ItemName , ItemGroup , ItemCategory, Specification , Unit , PurchaseRate, SaleRate, GSTPer, HSNCode)"
    '            mQry = mQry + "Select " & I & " As RowNumber, '" + AgL.XNull(DtTemp.Rows(I)("Item Code")) + "' As ItemCode, 
    '                    '" + AgL.XNull(DtTemp.Rows(I)("Item Name")).Replace("'", "`") + "' As ItemName, 
    '                    '" + AgL.XNull(DtTemp.Rows(I)("Item Group")).ToString().Replace("'", "`") + "' As ItemGroup, 
    '                    '" + AgL.XNull(DtTemp.Rows(I)("Item Category")) + "' As ItemCategory, 
    '                    '" + AgL.XNull(DtTemp.Rows(I)("Specification")) + "' As Specification, 
    '                    '" + AgL.XNull(DtTemp.Rows(I)("Unit")) + "' As Unit, 
    '                    '" + AgL.XNull(DtTemp.Rows(I)("Purchase Rate")) + "' As PurchaseRate,
    '                    '" + AgL.XNull(DtTemp.Rows(I)("Sale Rate")) + "' As SaleRate, 
    '                    '" + AgL.XNull(DtTemp.Rows(I)("GST Per")) + "' As GSTPer, 
    '                    '" + AgL.XNull(DtTemp.Rows(I)("HSN Code")) + "' As HSNCode "
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    '        Next




    '        mQry = " INSERT INTO ItemCategory(Code, Description, ItemType, EntryBy, EntryDate, EntryType, EntryStatus) "
    '        mQry = mQry + " Select (Select Count(*) from TempTable TT where H.RowNumber >= TT.RowNumber) As Code, H.ItemCategory As Description, 
    '            'TP' As ItemType, '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
    '            'Add' As EntryType, 'Open' As EntryStatus 
    '            From TempTable H 
    '            LEFT JOIN ItemCategory Ic On Upper(H.ItemCategory) = Upper(Ic.Description)
    '            Where Ic.Code Is Null 
    '            Group By H.ItemCategory 
    '            Order By H.RowNumber "
    '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


    '        mQry = " INSERT INTO ItemGroup(Code, Description, ItemType, ItemCategory, EntryBy, EntryDate, EntryType, EntryStatus) "
    '        mQry = mQry + " Select (Select Count(*) from TempTable TT where H.RowNumber >= TT.RowNumber) As Code, H.ItemGroup As Description, 
    '            'TP' As ItemType, Max(Ic.Code) As ItemCategory,  
    '            '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
    '            'Add' As EntryType, 'Open' As EntryStatus 
    '            From TempTable H 
    '            LEFT JOIN ItemGroup Ig On Upper(H.ItemGroup) = Upper(Ig.Description)
    '            LEFT JOIN ItemCategory Ic On Upper(H.ItemCategory) = Upper(Ic.Description)
    '            Where Ig.Code Is Null
    '            ANd IfNull(H.ItemGroup,'') <> ''
    '            Group By H.ItemGroup 
    '            Order By H.RowNumber "
    '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    '        mQry = " INSERT INTO Item(Code, ManualCode, Description, DisplayName, ItemGroup, ItemCategory, ItemType, PurchaseRate, Rate, 
    '                SalesTaxPostingGroup, HSN, EntryBy, EntryDate, EntryType, EntryStatus, Status, Div_Code, StockYN, IsSystemDefine) "
    '        mQry = mQry + " Select (Select Count(*) from TempTable TT where H.RowNumber >= TT.RowNumber) As Code, 
    '            H.ItemCode As ManuelCode, H.ItemName As Description, H.ItemDisplayName As DisplayName, 
    '            Max(Ig.Code) As ItemGroup, Max(Ic.Code) As ItemCategory, 'TP' As ItemType, 
    '            Max(IfNull(H.PurchaseRate,0)) As PurchaseRate, Max(H.SaleRate) As SaleRate,
    '            H.GSTPer, H.HSNCode,
    '            '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
    '            'Add' As EntryType, 'Open' As EntryStatus, 'Active' As Status, '" & AgL.PubDivCode & "', 1 As StockYN, 0 As IsSystemDefine
    '            From TempTable H 
    '            LEFT JOIN Item I On Upper(H.ItemName) = Upper(I.Description)
    '            LEFT JOIN ItemGroup Ig On Upper(H.ItemGroup) = Upper(Ig.Description)
    '            LEFT JOIN ItemCategory Ic On Upper(H.ItemCategory) = Upper(Ic.Description)
    '            Where I.Code Is Null
    '            ANd IfNull(H.ItemName,'') <> ''
    '            Group By H.ItemName 
    '            Order By H.RowNumber "
    '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    '        AgL.ETrans.Commit()
    '        mTrans = "Commit"

    '    Catch ex As Exception
    '        AgL.ETrans.Rollback()
    '        MsgBox(ex.Message)
    '    End Try
    '    If StrErrLog <> "" Then MsgBox(StrErrLog)
    'End Sub


    Public Sub FImportFromExcel_Old()
        Dim mTrans As String = ""
        Dim DtTemp As DataTable
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, 'Item Code' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Display Name' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Group' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Category' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Purchase Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'HSN Code' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Item Master Import"
        ObjFrmImport.Dgl1.DataSource = DtTemp
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)

        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("Item Category")) = "" Then
                ErrorLog += "Item Category is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtTemp.Rows(I)("Item Group")) = "" Then
                ErrorLog += "Item Group is blank at row no." + (I + 2).ToString() & vbCrLf
            End If
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            Dim mItemCategoryCode = AgL.GetMaxId("ItemCategory", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemCategory = DtTemp.DefaultView.ToTable(True, "Item Category")
            For I = 0 To DtItemCategory.Rows.Count - 1
                If AgL.XNull(DtItemCategory.Rows(I)("Item Category")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From ItemCategory where Description = '" & AgL.XNull(DtItemCategory.Rows(I)("Item Category")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                        Dim mItemCategoryCode_New = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
                        mQry = " INSERT INTO ItemCategory(Code, Description, ItemType, EntryBy, EntryDate, EntryType, EntryStatus)
                                Select '" & mItemCategoryCode_New & "' As Code, " & AgL.Chk_Text(AgL.XNull(DtItemCategory.Rows(I)("Item Category"))) & " As Description, 
                                'TP' As ItemType, '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                                'Add' As EntryType, 'Open' As EntryStatus "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next

            Dim mItemGroupCode = AgL.GetMaxId("ItemGroup", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemGroup = DtTemp.DefaultView.ToTable(True, "Item Group")
            For I = 0 To DtItemGroup.Rows.Count - 1
                If AgL.XNull(DtItemGroup.Rows(I)("Item Group")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From ItemGroup where Description = '" & AgL.XNull(DtItemGroup.Rows(I)("Item Group")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                        Dim mItemGroupCode_New = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
                        mQry = " INSERT INTO ItemGroup(Code, Description, ItemCategory, ItemType, EntryBy, EntryDate, EntryType, EntryStatus)
                                    Select '" & mItemGroupCode_New & "' As Code, " & AgL.Chk_Text(AgL.XNull(DtItemGroup.Rows(I)("Item Group"))) & " As Description, 
                                    (SELECT Code FROM ItemCategory WHERE Description = '" & AgL.XNull(DtTemp.Rows(I)("Item Category")) & "') As ItemCategory, 
                                    'TP' As ItemType, '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                                    'Add' As EntryType, 'Open' As EntryStatus "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next

            Dim mItemCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            For I = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("Item Name")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From Item where ManualCode = '" & AgL.XNull(DtTemp.Rows(I)("Item Code")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                        Dim mItemCode_New = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
                        mQry = " INSERT INTO Item(Code, ManualCode, Description, DisplayName, Specification, ItemGroup, ItemCategory, ItemType, 
                                PurchaseRate, Rate, 
                                SalesTaxPostingGroup, HSN, EntryBy, EntryDate, EntryType, EntryStatus, Status, Div_Code, StockYN, IsSystemDefine) 
                                Select '" & mItemCode_New & "' As Code, 
                                " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Item Code"))) & " As ManuelCode, 
                                " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Item Name"))) & " As Description, 
                                " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Item Display Name"))) & " As DisplayName, 
                                " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Specification"))) & " As Specification, 
                                (SELECT Code FROM ItemGroup WHERE Description = '" & AgL.XNull(DtTemp.Rows(I)("Item Group")) & "') As ItemGroup, 
                                (SELECT Code FROM ItemCategory WHERE Description = '" & AgL.XNull(DtTemp.Rows(I)("Item Category")) & "') As ItemCategory, 
                                'TP' As ItemType, 
                                " & AgL.VNull(DtTemp.Rows(I)("Purchase Rate")) & " As PurchaseRate, 
                                " & AgL.VNull(DtTemp.Rows(I)("Sale Rate")) & " As SaleRate,
                                (SELECT Description From PostingGroupSalesTaxItem WHERE GrossTaxRate = " & AgL.VNull(DtTemp.Rows(I)("GST Per")) & ") As SalesTaxPostingGroup, 
                                " & AgL.VNull(DtTemp.Rows(I)("HSN Code")) & " As HSNCode,
                                '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                                'Add' As EntryType, 'Open' As EntryStatus, 'Active' As Status, '" & AgL.PubDivCode & "', 1 As StockYN, 0 As IsSystemDefine "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next




            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub



    Public Sub FImportFromExcel(bImportFor As ImportFor)
        Dim mTrans As String = ""
        Dim DtDataFields As DataTable
        Dim DtTemp As DataTable
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "Item Code") & "' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Item Name") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Item Display Name") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Item Group") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Item Category") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Specification") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Unit") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Purchase Rate") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale Rate") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sales Tax Group") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "HSN Code") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        DtDataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Item Master Import"
        ObjFrmImport.Dgl1.DataSource = DtDataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)


        If bImportFor = ImportFor.Dos Then
            For I = 0 To DtTemp.Rows.Count - 1
                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")).ToString().Trim() = "GST @ 5%" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")) = "GST 5%"
                End If

                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString().Trim() = "P" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Pcs"
                ElseIf DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString().Trim() = "M" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Meter"
                End If

                DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification")).ToString.Trim +
                    Space(10) + "[" + DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group")).ToString.Trim + " | " +
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category")).ToString.Trim + "]"

                DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Display Name")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))
            Next
        End If



        For I = 0 To DtDataFields.Rows.Count - 1
            If AgL.XNull(DtDataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtTemp.Columns.Contains(AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
            End If
        Next


        If DtTemp.Columns.Contains(GetFieldAliasName(bImportFor, "Sales Tax Group")) Then
            Dim DtSalesTaxGroup = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Sales Tax Group"))
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxItem where Description = '" & AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                        If ErrorLog.Contains("These Sales Tax Groups Are Not Present In Master") = False Then
                            ErrorLog += vbCrLf & "These Sales Tax Groups Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))) & ", "
                        Else
                            ErrorLog += AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))) & ", "
                        End If
                    End If
                End If
            Next
        End If

        If DtTemp.Columns.Contains(GetFieldAliasName(bImportFor, "Unit")) Then
            Dim DtUnit = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Unit"))
            For I = 0 To DtUnit.Rows.Count - 1
                If AgL.XNull(DtUnit.Rows(I)(GetFieldAliasName(bImportFor, "Unit"))) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From Unit where Code = '" & AgL.XNull(DtUnit.Rows(I)(GetFieldAliasName(bImportFor, "Unit"))) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                        If ErrorLog.Contains("These units are not present in master") = False Then
                            ErrorLog += vbCrLf & "These Unit Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtUnit.Rows(I)(GetFieldAliasName(bImportFor, "Unit"))) & ", "
                        Else
                            ErrorLog += AgL.XNull(DtUnit.Rows(I)(GetFieldAliasName(bImportFor, "Unit"))) & ", "
                        End If
                    End If
                End If
            Next
        End If

        For I = 0 To DtTemp.Rows.Count - 1
            For J As Integer = 0 To DtDataFields.Rows.Count - 1
                If DtTemp.Columns.Contains(DtDataFields.Rows(J)("Field Name")) Then
                    If DtDataFields.Rows(J)("Remark").ToString().Contains("Mandatory") Then
                        If AgL.XNull(DtTemp.Rows(I)(DtDataFields.Rows(J)("Field Name"))) = "" Then
                            ErrorLog += DtDataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
                        End If
                    End If
                End If
            Next
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            Dim bLastItemCategoryCode = AgL.GetMaxId("ItemCategory", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemCategory = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Category"), GetFieldAliasName(bImportFor, "Sales Tax Group"))
            For I = 0 To DtItemCategory.Rows.Count - 1
                If AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))) <> "" Then
                    Dim ItemCategoryTable As New StructItemCategory
                    Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemCategoryTable.Code = bItemCategoryCode
                    ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    ItemCategoryTable.ItemType = "TP"
                    ItemCategoryTable.SalesTaxGroup = AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim
                    ItemCategoryTable.Unit = "Pcs"
                    ItemCategoryTable.EntryBy = AgL.PubUserName
                    ItemCategoryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    ItemCategoryTable.EntryType = "Add"
                    ItemCategoryTable.EntryStatus = LogStatus.LogOpen
                    ItemCategoryTable.Div_Code = AgL.PubDivCode
                    ItemCategoryTable.Status = "Active"

                    ImportItemCategoryTable(ItemCategoryTable)
                End If
            Next

            Dim bLastItemGroupCode = AgL.GetMaxId("ItemGroup", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemGroup = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Group"), GetFieldAliasName(bImportFor, "Item Category"), GetFieldAliasName(bImportFor, "Sales Tax Group"))
            For I = 0 To DtItemGroup.Rows.Count - 1
                If AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))) <> "" Then
                    Dim ItemGroupTable As New StructItemGroup
                    Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemGroupTable.Code = bItemGroupCode
                    ItemGroupTable.Description = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))).ToString.Trim
                    ItemGroupTable.ItemCategory = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    ItemGroupTable.ItemType = "TP"
                    ItemGroupTable.SalesTaxGroup = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim
                    ItemGroupTable.Unit = "Pcs"
                    ItemGroupTable.EntryBy = AgL.PubUserName
                    ItemGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    ItemGroupTable.EntryType = "Add"
                    ItemGroupTable.EntryStatus = LogStatus.LogOpen
                    ItemGroupTable.Div_Code = AgL.PubDivCode
                    ItemGroupTable.Status = "Active"

                    ImportItemGroupTable(ItemGroupTable)
                End If
            Next

            Dim bLastItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            For I = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) <> "" Then

                    Dim ItemTable As New StructItem
                    Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemTable.Code = bItemCode
                    ItemTable.ManualCode = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Code"))).ToString.Trim
                    ItemTable.Description = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))).ToString.Trim
                    ItemTable.DisplayName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Display Name"))).ToString.Trim
                    ItemTable.Specification = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification"))).ToString.Trim
                    ItemTable.ItemGroup = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))).ToString.Trim
                    ItemTable.ItemCategory = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    ItemTable.ItemType = "TP"
                    ItemTable.Unit = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit"))).ToString.Trim
                    ItemTable.PurchaseRate = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Purchase Rate"))).ToString.Trim
                    ItemTable.Rate = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sale Rate"))).ToString.Trim
                    ItemTable.SalesTaxPostingGroup = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim
                    ItemTable.HSN = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "HSN Code"))).ToString.Trim
                    ItemTable.EntryBy = AgL.PubUserName
                    ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    ItemTable.EntryType = "Add"
                    ItemTable.EntryStatus = LogStatus.LogOpen
                    ItemTable.Div_Code = AgL.PubDivCode
                    ItemTable.Status = "Active"
                    ItemTable.StockYN = 1
                    ItemTable.IsSystemDefine = 0



                    ImportItemTable(ItemTable)
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub

    Private Function GetFieldAliasName(bImportFor As ImportFor, bFieldName As String)
        Dim bAliasName As String = bFieldName
        If bImportFor = ImportFor.Dos Then
            Select Case bFieldName
                Case "Item Code"
                    bAliasName = "ITEM_CODE"
                Case "Item Name"
                    bAliasName = "ITEM_NAME"
                Case "Item Display Name"
                    bAliasName = "DISP_NAME"
                Case "Item Group"
                    bAliasName = "ITEM_GROUP"
                Case "Item Category"
                    bAliasName = "ITEM_CAT"
                Case "Specification"
                    bAliasName = "SPECIFIC"
                Case "Unit"
                    bAliasName = "UNIT"
                Case "Purchase Rate"
                    bAliasName = "PUR_RATE"
                Case "Sale Rate"
                    bAliasName = "SALE_RATE"
                Case "Sales Tax Group"
                    bAliasName = "TAX_GROUP"
                Case "HSN Code"
                    bAliasName = "HSN_CODE"
            End Select

            If bAliasName = bFieldName Then
                If bFieldName.Contains("Dhara") Then
                    bAliasName = "DHARA_RATE"
                ElseIf bFieldName.Contains("Net") And Not bFieldName.Contains("Super") Then
                    bAliasName = "NET_RATE"
                ElseIf bFieldName.Contains("Net") And bFieldName.Contains("Super") Then
                    bAliasName = "SPNET_RATE"
                End If
            End If

            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function

    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuImportRateListFromExcel.Click, MnuImportRateListFromDos.Click, MnuBulkEdit.Click
        Select Case sender.name
            Case MnuImportFromExcel.Name
                FImportFromExcel(ImportFor.Excel)

            Case MnuImportFromDos.Name
                FImportFromExcel(ImportFor.Dos)

            Case MnuImportFromTally.Name
                FImportFromTally()

            Case MnuImportRateListFromExcel.Name
                FImportRateListFromExcel(ImportFor.Excel)

            Case MnuImportRateListFromDos.Name
                FImportRateListFromExcel(ImportFor.Dos)

            Case MnuBulkEdit.Name
                Dim FrmObj As New FrmItemMasterBulk()
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.Show()
        End Select
    End Sub
    Public Sub FImportFromTally()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As New DataTable
        Dim I As Integer = 0, J As Integer = 0
        'Dim FileNameWithPath As String = My.Application.Info.DirectoryPath & "\TallyXML\ItemMaster.xml"
        Dim FileNameWithPath As String = ""

        OFDMain.Filter = "*.xml|*.XML"
        If OFDMain.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        FileNameWithPath = OFDMain.FileName

        Dim doc As New XmlDocument()
        doc.Load(FileNameWithPath)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim bLastItemCategoryCode As String = AgL.GetMaxId("ItemCategory", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True)

            Dim ItemCategoryElementList As XmlNodeList = doc.GetElementsByTagName("STOCKCATEGORY")
            For I = 0 To ItemCategoryElementList.Count - 1
                Dim ItemCategoryTable As New StructItemCategory
                Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemCategoryTable.Code = bItemCategoryCode
                ItemCategoryTable.Description = ItemCategoryElementList(I).Attributes("NAME").Value
                ItemCategoryTable.ItemType = "TP"
                ItemCategoryTable.SalesTaxGroup = "GST 5%"
                ItemCategoryTable.Unit = "Pcs"
                ItemCategoryTable.EntryBy = AgL.PubUserName
                ItemCategoryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemCategoryTable.EntryType = "Add"
                ItemCategoryTable.EntryStatus = LogStatus.LogOpen
                ItemCategoryTable.Div_Code = AgL.PubDivCode
                ItemCategoryTable.Status = "Active"

                ImportItemCategoryTable(ItemCategoryTable)
            Next

            Dim bLastItemGroupCode As String = AgL.GetMaxId("ItemGroup", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim ItemGroupElementList As XmlNodeList = doc.GetElementsByTagName("STOCKGROUP")
            For I = 0 To ItemGroupElementList.Count - 1
                Dim ItemGroupTable As New StructItemGroup
                Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemGroupTable.Code = bItemGroupCode
                ItemGroupTable.Description = ItemGroupElementList(I).Attributes("NAME").Value
                ItemGroupTable.ItemType = "TP"
                ItemGroupTable.SalesTaxGroup = "GST 5%"
                ItemGroupTable.Unit = "Pcs"
                ItemGroupTable.EntryBy = AgL.PubUserName
                ItemGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemGroupTable.EntryType = "Add"
                ItemGroupTable.EntryStatus = LogStatus.LogOpen
                ItemGroupTable.Div_Code = AgL.PubDivCode
                ItemGroupTable.Status = "Active"

                ImportItemGroupTable(ItemGroupTable)
            Next

            Dim bLastItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            Dim bLastManualCode As String = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) FROM Item  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)

            Dim ItemElementList As XmlNodeList = doc.GetElementsByTagName("STOCKITEM")
            For I = 0 To ItemElementList.Count - 1
                Dim ItemTable As New StructItem
                Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
                Dim bManualCode = bLastManualCode + I

                ItemTable.Code = bItemCode
                ItemTable.ManualCode = bManualCode
                ItemTable.DisplayName = ItemElementList(I).Attributes("NAME").Value
                ItemTable.Specification = ItemElementList(I).Attributes("NAME").Value

                If ItemElementList(I).Attributes("NAME").Value = "ARISTOCRAT" Then
                    MsgBox(ItemElementList(I).Attributes("NAME").Value)
                End If

                If ItemElementList(I).SelectSingleNode("PARENT") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("PARENT").ChildNodes.Count > 0 Then
                        ItemTable.ItemGroup = ItemElementList(I).SelectSingleNode("PARENT").ChildNodes(0).Value
                    End If
                End If

                If ItemElementList(I).SelectSingleNode("CATEGORY") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("CATEGORY").ChildNodes.Count > 0 Then
                        ItemTable.ItemCategory = ItemElementList(I).SelectSingleNode("CATEGORY").ChildNodes(0).Value
                    End If
                End If

                ItemTable.Description = ItemElementList(I).Attributes("NAME").Value + Space(10) + "[" + ItemTable.ItemGroup + " | " + ItemTable.ItemCategory + "]"


                ItemTable.ItemType = "TP"

                If ItemElementList(I).SelectSingleNode("BASEUNITS") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("BASEUNITS").ChildNodes.Count > 0 Then
                        ItemTable.Unit = ItemElementList(I).SelectSingleNode("BASEUNITS").ChildNodes(0).Value.Replace(".", "")
                    End If
                End If


                If ItemElementList(I).SelectSingleNode("STANDARDCOSTLIST.LIST") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("STANDARDCOSTLIST.LIST").SelectSingleNode("RATE") IsNot Nothing Then
                        ItemTable.PurchaseRate = ItemElementList(I).SelectSingleNode("STANDARDCOSTLIST.LIST").SelectSingleNode("RATE").ChildNodes(0).Value
                    End If
                End If

                If ItemElementList(I).SelectSingleNode("STANDARDPRICELIST.LIST") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("STANDARDPRICELIST.LIST").SelectSingleNode("RATE") IsNot Nothing Then
                        ItemTable.Rate = ItemElementList(I).SelectSingleNode("STANDARDPRICELIST.LIST").SelectSingleNode("RATE").ChildNodes(0).Value
                    End If
                End If


                'ItemTable.PurchaseRate = 0
                'ItemTable.Rate = 0

                If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST") IsNot Nothing Then
                        If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST").SelectNodes("RATEDETAILS.LIST") IsNot Nothing Then
                            If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST").SelectNodes("RATEDETAILS.LIST").Item(2).SelectSingleNode("GSTRATE") IsNot Nothing Then
                                ItemTable.SalesTaxPostingGroup = ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST").SelectNodes("RATEDETAILS.LIST").Item(2).SelectSingleNode("GSTRATE").ChildNodes(0).Value
                            End If
                        End If
                    End If
                End If

                If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("HSNCODE") IsNot Nothing Then
                        If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("HSNCODE").ChildNodes.Count > 0 Then
                            ItemTable.HSN = ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("HSNCODE").ChildNodes(0).Value
                        End If
                    End If
                End If


                ItemTable.EntryBy = AgL.PubUserName
                ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemTable.EntryType = "Add"
                ItemTable.EntryStatus = LogStatus.LogOpen
                ItemTable.Div_Code = AgL.PubDivCode
                ItemTable.Status = "Active"
                ItemTable.StockYN = 1
                ItemTable.IsSystemDefine = 0


                ImportItemTable(ItemTable)

                mQry = "UPDATE ItemGroup Set ItemCategory = (Select code From ItemCategory Where Description = '" & ItemTable.ItemCategory & "'),
                          Where Code = (Select code From ItemGroup Where Description = '" & ItemTable.ItemGroup & "')  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            '''''''''''''For Freight'''''''''''''
            'Dim ItemTable_FRGT As New StructItem
            'Dim bItemCode_FRGT As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
            'Dim bManualCode_FRGT = bLastManualCode + I

            'ItemTable_FRGT.Code = bItemCode_FRGT
            'ItemTable_FRGT.ManualCode = bManualCode_FRGT
            'ItemTable_FRGT.DisplayName = "FRGT INWARD FRM UNRAGISTER"
            'ItemTable_FRGT.Specification = "FRGT INWARD FRM UNRAGISTER"
            'ItemTable_FRGT.ItemGroup = ""
            'ItemTable_FRGT.ItemCategory = ""
            'ItemTable_FRGT.Description = "FRGT INWARD FRM UNRAGISTER"
            'ItemTable_FRGT.ItemType = "TP"
            'ItemTable_FRGT.Unit = "Pcs"
            'ItemTable_FRGT.PurchaseRate = 0
            'ItemTable_FRGT.Rate = 0
            'ItemTable_FRGT.SalesTaxPostingGroup = ""
            'ItemTable_FRGT.HSN = ""
            'ItemTable_FRGT.EntryBy = AgL.PubUserName
            'ItemTable_FRGT.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            'ItemTable_FRGT.EntryType = "Add"
            'ItemTable_FRGT.EntryStatus = LogStatus.LogOpen
            'ItemTable_FRGT.Div_Code = AgL.PubDivCode
            'ItemTable_FRGT.Status = "Active"
            'ItemTable_FRGT.StockYN = 0
            'ItemTable_FRGT.IsSystemDefine = 0
            'ImportItemTable(ItemTable_FRGT)
            'I += 1

            '''''''''''''Bank Charge's'''''''''''''
            'Dim ItemTable_BCHRG As New StructItem
            'Dim bItemCode_BCHRG As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
            'Dim bManualCode_BCHRG = bLastManualCode + I

            'ItemTable_BCHRG.Code = bItemCode_BCHRG
            'ItemTable_BCHRG.ManualCode = bManualCode_BCHRG
            'ItemTable_BCHRG.DisplayName = "BANK CHARGE" + ControlChars.Quote + "S"
            'ItemTable_BCHRG.Specification = "BANK CHARGE" + ControlChars.Quote + "S"
            'ItemTable_BCHRG.ItemGroup = ""
            'ItemTable_BCHRG.ItemCategory = ""
            'ItemTable_BCHRG.Description = "BANK CHARGE" + ControlChars.Quote + "S"
            'ItemTable_BCHRG.ItemType = "TP"
            'ItemTable_BCHRG.Unit = "Pcs"
            'ItemTable_BCHRG.PurchaseRate = 0
            'ItemTable_BCHRG.Rate = 0
            'ItemTable_BCHRG.SalesTaxPostingGroup = ""
            'ItemTable_BCHRG.HSN = ""
            'ItemTable_BCHRG.EntryBy = AgL.PubUserName
            'ItemTable_BCHRG.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            'ItemTable_BCHRG.EntryType = "Add"
            'ItemTable_BCHRG.EntryStatus = LogStatus.LogOpen
            'ItemTable_BCHRG.Div_Code = AgL.PubDivCode
            'ItemTable_BCHRG.Status = "Active"
            'ItemTable_BCHRG.StockYN = 0
            'ItemTable_BCHRG.IsSystemDefine = 0
            'ImportItemTable(ItemTable_BCHRG)
            'I += 1

            '''''''''''''Electricity Charge's'''''''''''''
            'Dim ItemTable_ELECT As New StructItem
            'Dim bItemCode_ELECT As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
            'Dim bManualCode_ELECT = bLastManualCode + I

            'ItemTable_ELECT.Code = bItemCode_ELECT
            'ItemTable_ELECT.ManualCode = bManualCode_ELECT
            'ItemTable_ELECT.DisplayName = "ELECTRICITY EXP."
            'ItemTable_ELECT.Specification = "ELECTRICITY EXP."
            'ItemTable_ELECT.ItemGroup = ""
            'ItemTable_ELECT.ItemCategory = ""
            'ItemTable_ELECT.Description = "ELECTRICITY EXP."
            'ItemTable_ELECT.ItemType = "TP"
            'ItemTable_ELECT.Unit = "Pcs"
            'ItemTable_ELECT.PurchaseRate = 0
            'ItemTable_ELECT.Rate = 0
            'ItemTable_ELECT.SalesTaxPostingGroup = ""
            'ItemTable_ELECT.HSN = ""
            'ItemTable_ELECT.EntryBy = AgL.PubUserName
            'ItemTable_ELECT.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            'ItemTable_ELECT.EntryType = "Add"
            'ItemTable_ELECT.EntryStatus = LogStatus.LogOpen
            'ItemTable_ELECT.Div_Code = AgL.PubDivCode
            'ItemTable_ELECT.Status = "Active"
            'ItemTable_ELECT.StockYN = 0
            'ItemTable_ELECT.IsSystemDefine = 0
            'ImportItemTable(ItemTable_ELECT)
            'I += 1

            '''''''''''''For Freight'''''''''''''
            'Dim ItemTable_FRIEGHT As New StructItem
            'Dim bItemCode_FRIEGHT As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
            'Dim bManualCode_FRIEGHT = bLastManualCode + I

            'ItemTable_FRIEGHT.Code = bItemCode_FRIEGHT
            'ItemTable_FRIEGHT.ManualCode = bManualCode_FRIEGHT
            'ItemTable_FRIEGHT.DisplayName = "FRIEGHT INWARD FROM UNRAGITER"
            'ItemTable_FRIEGHT.Specification = "FRIEGHT INWARD FROM UNRAGITER"
            'ItemTable_FRIEGHT.ItemGroup = ""
            'ItemTable_FRIEGHT.ItemCategory = ""
            'ItemTable_FRIEGHT.Description = "FRIEGHT INWARD FROM UNRAGITER"
            'ItemTable_FRIEGHT.ItemType = "TP"
            'ItemTable_FRIEGHT.Unit = "Pcs"
            'ItemTable_FRIEGHT.PurchaseRate = 0
            'ItemTable_FRIEGHT.Rate = 0
            'ItemTable_FRIEGHT.SalesTaxPostingGroup = ""
            'ItemTable_FRIEGHT.HSN = ""
            'ItemTable_FRIEGHT.EntryBy = AgL.PubUserName
            'ItemTable_FRIEGHT.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            'ItemTable_FRIEGHT.EntryType = "Add"
            'ItemTable_FRIEGHT.EntryStatus = LogStatus.LogOpen
            'ItemTable_FRIEGHT.Div_Code = AgL.PubDivCode
            'ItemTable_FRIEGHT.Status = "Active"
            'ItemTable_FRIEGHT.StockYN = 0
            'ItemTable_FRIEGHT.IsSystemDefine = 0
            'ImportItemTable(ItemTable_FRIEGHT)
            'I += 1

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ImportItemCategoryTable(ItemCategoryTable As StructItemCategory)
        If AgL.Dman_Execute("Select Count(*) From ItemCategory With (NoLock) where Description = '" & ItemCategoryTable.Description & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
            mQry = " INSERT INTO ItemCategory(Code, Description, ItemType, SalesTaxGroup, Unit, EntryBy, EntryDate, EntryType, EntryStatus)
                    Select '" & ItemCategoryTable.Code & "' As Code, " & AgL.Chk_Text(ItemCategoryTable.Description) & " As Description, 
                    " & AgL.Chk_Text(ItemCategoryTable.ItemType) & " As ItemType, 
                    " & AgL.Chk_Text(ItemCategoryTable.SalesTaxGroup) & " As SalesTaxGroup, 
                    " & AgL.Chk_Text(ItemCategoryTable.Unit) & " As Unit, 
                    '" & ItemCategoryTable.EntryBy & "' As EntryBy, 
                    " & AgL.Chk_Date(ItemCategoryTable.EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(ItemCategoryTable.EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(ItemCategoryTable.EntryStatus) & " As EntryStatus "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Private Sub ImportItemGroupTable(ItemGroupTable As StructItemGroup)
        If AgL.Dman_Execute("SELECT Count(*) From ItemGroup With (NoLock) where Description = " & AgL.Chk_Text(ItemGroupTable.Description) & " ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
            ItemGroupTable.ItemCategory = AgL.Dman_Execute("SELECT Code From ItemCategory With (NoLock) Where Replace(Description,' ','') = Replace('" & ItemGroupTable.ItemCategory & "',' ','')", AgL.GcnRead).ExecuteScalar()

            mQry = " INSERT INTO ItemGroup(Code, Description, ItemCategory, ItemType, Default_MarginPer, EntryBy, EntryDate, EntryType, EntryStatus)
                    Select '" & ItemGroupTable.Code & "' As Code, " & AgL.Chk_Text(ItemGroupTable.Description) & " As Description, 
                    " & AgL.Chk_Text(ItemGroupTable.ItemCategory) & " As ItemCategory, 
                    " & AgL.Chk_Text(ItemGroupTable.ItemType) & " As ItemType, 
                    0 As Default_MarginPer,
                    '" & ItemGroupTable.EntryBy & "' As EntryBy, 
                    " & AgL.Chk_Date(ItemGroupTable.EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(ItemGroupTable.EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(ItemGroupTable.EntryStatus) & " As EntryStatus "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Public Sub ImportItemTable(ItemTable As StructItem)
        If AgL.VNull(AgL.Dman_Execute("SELECT Count(*) From Item With (NoLock) where Description = " & AgL.Chk_Text(ItemTable.Description) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar) = 0 Then
            ItemTable.ItemGroup = AgL.Dman_Execute("SELECT Code From ItemGroup With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.ItemGroup) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            ItemTable.ItemCategory = AgL.Dman_Execute("SELECT Code From ItemCategory With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.ItemCategory) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            ItemTable.SalesTaxPostingGroup = AgL.Dman_Execute("SELECT Description From PostingGroupSalesTaxItem With (NoLock) WHERE Description = " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            If ItemTable.SalesTaxPostingGroup = "" Then
                ItemTable.SalesTaxPostingGroup = AgL.Dman_Execute("SELECT Description From PostingGroupSalesTaxItem With (NoLock) WHERE GrossTaxRate = " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If


            mQry = " INSERT INTO Item(Code, ManualCode, Description, DisplayName, Specification, ItemGroup, ItemCategory, ItemType, Unit,
                    PurchaseRate, Rate, 
                    SalesTaxPostingGroup, HSN, EntryBy, EntryDate, EntryType, EntryStatus, Status, Div_Code, StockYN, IsSystemDefine) 
                    Select '" & ItemTable.Code & "' As Code, 
                    " & AgL.Chk_Text(ItemTable.ManualCode) & " As ManuelCode, 
                    " & AgL.Chk_Text(ItemTable.Description) & " As Description, 
                    " & AgL.Chk_Text(ItemTable.DisplayName) & " As DisplayName, 
                    " & AgL.Chk_Text(ItemTable.Specification) & " As Specification, 
                    " & AgL.Chk_Text(ItemTable.ItemGroup) & " As ItemGroup, 
                    " & AgL.Chk_Text(ItemTable.ItemCategory) & " As ItemCategory, 
                    " & AgL.Chk_Text(ItemTable.ItemType) & " As ItemType, 
                    " & AgL.Chk_Text(ItemTable.Unit) & " As Unit, 
                    " & AgL.Chk_Text(ItemTable.PurchaseRate) & " As PurchaseRate, 
                    " & AgL.Chk_Text(ItemTable.Rate) & " As Rate,
                    " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & " As SalesTaxPostingGroup, 
                    " & AgL.Chk_Text(ItemTable.HSN) & " As HSNCode,
                    " & AgL.Chk_Text(ItemTable.EntryBy) & " As EntryBy, 
                    " & AgL.Chk_Text(ItemTable.EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(ItemTable.EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(ItemTable.EntryStatus) & " As EntryStatus, 
                    " & AgL.Chk_Text(ItemTable.Status) & " As Status, 
                    " & AgL.Chk_Text(ItemTable.Div_Code) & " , 
                    " & AgL.Chk_Text(ItemTable.StockYN) & " As StockYN, 
                    " & AgL.Chk_Text(ItemTable.IsSystemDefine) & " As IsSystemDefine "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            Dim RateListTable As New StructRateList

            RateListTable.Code = ItemTable.Code
            RateListTable.WEF = AgL.PubLoginDate
            RateListTable.RateType = ""
            RateListTable.EntryBy = AgL.PubUserName
            RateListTable.EntryDate = AgL.GetDateTime(IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead))
            RateListTable.EntryType = "Add"
            RateListTable.EntryStatus = LogStatus.LogOpen
            RateListTable.Status = "Active"
            RateListTable.Div_Code = AgL.PubDivCode
            RateListTable.Line_Sr = 0
            RateListTable.Line_WEF = AgL.PubStartDate
            RateListTable.Line_Item = ItemTable.Code
            RateListTable.Line_RateType = ""
            RateListTable.Line_Rate = ItemTable.Rate

            ImportRateListTable(RateListTable)
        End If


        'Dim mCnt As Integer = AgL.Dman_Execute("SELECT Count(*) From Item With (NoLock) where Description = " & AgL.Chk_Text(ItemTable.Description) & "", AgL.GcnRead).ExecuteScalar
        'MsgBox(mCnt)

        'Dim DsTemp As DataSet = AgL.FillData("SELECT * From Item With (NoLock) where Description = " & AgL.Chk_Text(ItemTable.Description) & "", AgL.GcnRead)
        'MsgBox(DsTemp.Tables(0).Rows(0)("Code"))
        'MsgBox(DsTemp.Tables(0).Rows(0)("Description"))
        'MsgBox(DsTemp.Tables(0).Rows(0)("Specification"))
    End Sub
    Private Sub ImportRateListTable(RateListTable As StructRateList)
        mQry = "Select Code From RateType With (NoLock) Where Description= '" & RateListTable.Line_RateType & "'"
        RateListTable.Line_RateType = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar())

        If AgL.Dman_Execute("Select Count(*) From RateList With (NoLock) Where Code = '" & RateListTable.Code & "'", AgL.GcnRead).ExecuteScalar() = 0 Then
            mQry = " INSERT INTO RateList(Code, WEF, RateType, EntryBy, EntryDate, EntryType, " &
                        " EntryStatus, Status, Div_Code) " &
                        " VALUES (" & AgL.Chk_Text(RateListTable.Code) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                        " NULL,	" & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                        " " & AgL.Chk_Text("E") & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                        " '" & RateListTable.Div_Code & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        Dim mSr As Integer = AgL.Dman_Execute("Select IfNull(Max(Sr),0) + 1 From RateListDetail With (NoLock) Where Code = '" & RateListTable.Code & "' ", AgL.GcnRead).ExecuteScalar()
        If Val(RateListTable.Line_Rate) > 0 Then
            mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " &
                        " VALUES (" & AgL.Chk_Text(RateListTable.Code) & ", " &
                        " " & mSr & ", " & AgL.Chk_Date(AgL.PubStartDate) & ", " &
                        " " & AgL.Chk_Text(RateListTable.Code) & ", " &
                        " " & AgL.Chk_Text(RateListTable.Line_RateType) & ", 
                        " & Val(RateListTable.Line_Rate) & " ) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        'If AgL.Dman_Execute("Select Count(*) From RateListDetail Where Code = '" & RateListTable.Code & "' 
        '                And IfNull(RateType,'')= " & AgL.Chk_Text(RateListTable.Line_RateType) & "", AgL.GCn).ExecuteScalar() = 0 Then

        '    Dim mSr As Integer = AgL.Dman_Execute("Select IfNull(Max(Sr),0) + 1 From RateListDetail Where Code = '" & RateListTable.Code & "' ", AgL.GCn).ExecuteScalar()
        '    If Val(RateListTable.Line_Rate) > 0 Then
        '        mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " &
        '                " VALUES (" & AgL.Chk_Text(RateListTable.Code) & ", " &
        '                " " & mSr & ", " & AgL.Chk_Date(AgL.PubStartDate) & ", " &
        '                " " & AgL.Chk_Text(RateListTable.Code) & ", " &
        '                " " & AgL.Chk_Text(RateListTable.Line_RateType) & ", 
        '                " & Val(RateListTable.Line_Rate) & " ) "
        '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        '    End If
        'Else
        '    If RateListTable.Line_RateType = "" Or RateListTable.Line_RateType Is Nothing Then
        '        mQry = " UPDATE RateListDetail Set Rate = " & Val(RateListTable.Line_Rate) & "
        '                Where Code = '" & RateListTable.Code & "'
        '                And Item = '" & RateListTable.Code & "'
        '                And IfNull(RateType,'')= " & AgL.Chk_Text(RateListTable.Line_RateType) & " "
        '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        '    End If
        'End If
    End Sub

    Public Structure StructRateList
        Dim Code As String
        Dim WEF As String
        Dim RateType As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Status As String
        Dim Div_Code As String

        Dim Line_Sr As String
        Dim Line_WEF As String
        Dim Line_Item As String
        Dim Line_RateType As String
        Dim Line_Rate As String
    End Structure

    Public Structure StructItemCategory
        Dim Code As String
        Dim Description As String
        Dim ItemType As String
        Dim SalesTaxGroup As String
        Dim Unit As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Div_Code As String
        Dim Status As String
    End Structure
    Public Structure StructItemGroup
        Dim Code As String
        Dim Description As String
        Dim ItemCategory As String
        Dim ItemType As String
        Dim Default_MarginPer As Double
        Dim SalesTaxGroup As String
        Dim Unit As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Div_Code As String
        Dim Status As String
    End Structure
    Public Structure StructItem
        Dim Code As String
        Dim ManualCode As String
        Dim Description As String
        Dim DisplayName As String
        Dim Specification As String
        Dim ItemGroup As String
        Dim ItemCategory As String
        Dim ItemType As String
        Dim PurchaseRate As String
        Dim Rate As String
        Dim SalesTaxPostingGroup As String
        Dim HSN As String
        Dim Unit As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Status As String
        Dim Div_Code As String
        Dim StockYN As String
        Dim IsSystemDefine As String
    End Structure
    Public Sub FImportRateListFromExcel(bImportFor As ImportFor)
        Dim mTrans As String = ""
        Dim DtDataFields As DataTable
        Dim DtTemp As DataTable
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "Item Name") & "' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Mandatory' as Remark "

        Dim DtRateTypes As DataTable = AgL.FillData("Select Description From RateType ", AgL.GCn).Tables(0)

        For I = 0 To DtRateTypes.Rows.Count - 1
            mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, DtRateTypes.Rows(I)("Description")) & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        Next

        DtDataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Item Rate List Import"
        ObjFrmImport.Dgl1.DataSource = DtDataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)

        If bImportFor = ImportFor.Dos Then
            For I = 0 To DtTemp.Rows.Count - 1
                DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification")).ToString.Trim +
                    Space(10) + "[" + DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group")).ToString.Trim + " | " +
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category")).ToString.Trim + "]"
            Next
        End If

        For I = 0 To DtDataFields.Rows.Count - 1
            If AgL.XNull(DtDataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtTemp.Columns.Contains(AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
            End If
        Next

        If DtTemp.Columns.Contains("Item Name") Then
            For I = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From Item where Description = " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")))) & " ", AgL.GCn).ExecuteScalar = 0 Then
                        If ErrorLog.Contains("These Items Are Not Present In Master") = False Then
                            ErrorLog += vbCrLf & "These Items Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) & ", "
                        Else
                            ErrorLog += AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))) & ", "
                        End If
                    End If
                End If
            Next
        End If


        For I = 0 To DtTemp.Rows.Count - 1
            For J As Integer = 0 To DtDataFields.Rows.Count - 1
                If DtTemp.Columns.Contains(DtDataFields.Rows(J)("Field Name")) Then
                    If DtDataFields.Rows(J)("Remark").ToString().Contains("Mandatory") Then
                        If AgL.XNull(DtTemp.Rows(I)(DtDataFields.Rows(J)("Field Name"))) = "" Then
                            ErrorLog += DtDataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
                        End If
                    End If
                End If
            Next
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim bItemCode As String = ""

            For I = 0 To DtTemp.Rows.Count - 1
                For J As Integer = 0 To DtTemp.Columns.Count - 1
                    For K As Integer = 0 To DtRateTypes.Rows.Count - 1
                        If DtTemp.Columns(J).ColumnName.ToUpper() = GetFieldAliasName(bImportFor, DtRateTypes.Rows(K)("Description")).ToUpper() Then
                            If AgL.VNull(DtTemp.Rows(I)(J)) > 0 Then
                                Dim RateListTable As New StructRateList
                                bItemCode = AgL.XNull(AgL.Dman_Execute("Select Code From Item With (NoLock) Where Description = " & AgL.Chk_Text(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")).ToString.Trim) & "", AgL.GcnRead).ExecuteScalar())
                                RateListTable.Code = bItemCode
                                RateListTable.WEF = AgL.PubLoginDate
                                RateListTable.RateType = ""
                                RateListTable.EntryBy = AgL.PubUserName
                                RateListTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                                RateListTable.EntryType = "Add"
                                RateListTable.EntryStatus = LogStatus.LogOpen
                                RateListTable.Status = "Active"
                                RateListTable.Div_Code = AgL.PubDivCode
                                RateListTable.Line_Sr = 0
                                RateListTable.Line_WEF = AgL.PubStartDate
                                RateListTable.Line_Item = bItemCode
                                RateListTable.Line_RateType = DtRateTypes.Rows(K)("Description")
                                RateListTable.Line_Rate = AgL.VNull(DtTemp.Rows(I)(J))

                                ImportRateListTable(RateListTable)
                            End If
                        End If
                    Next
                Next
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex

                Case rowDefaultSupplier
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name FROM viewHelpSubgroup Where SubgroupType = '" & SubgroupType.Supplier & "' "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowShowItemInOtherDivision, rowMaintainStockYn
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl1.CurrentCell.ReadOnly = True
            End If

            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowDefaultDiscountPerSale, rowDefaultAdditionPerSale, rowDefaultDiscountPerPurchase
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


End Class
