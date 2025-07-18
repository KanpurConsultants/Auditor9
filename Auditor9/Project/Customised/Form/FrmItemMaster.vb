Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Xml
Imports Customised.ClsMain
Imports Customised.ClsMain.ConfigurableFields
Imports System.ComponentModel
Imports System.Linq

Public Class FrmItemMaster
    Inherits AgTemplate.TempMaster
    Dim mQry$
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Public WithEvents PnlRateType As Panel
    Dim Photo_Byte As Byte()
    Public Const ColSNo As String = "SNo"
    Public WithEvents DGLRateType As New AgControls.AgDataGrid
    Public Const Col1RateType As String = FrmItemMasterLineRateType.RateType
    Public Const Col1CalculateOnRateType As String = "Calculate on Rate Type"
    Public Const Col1Margin As String = FrmItemMasterLineRateType.MarginPer
    Public Const Col1Rate As String = FrmItemMasterLineRateType.Rate
    Public Const Col1Discount As String = FrmItemMasterLineRateType.DiscountPer
    Public Const Col1Addition As String = FrmItemMasterLineRateType.AdditionPer


    Public WithEvents DGLUnitConversion As New AgControls.AgDataGrid
    Public Const Col1FromUnit As String = "From Unit"
    Public Const Col1Multiplier As String = "Multiplier"

    Public WithEvents DGLItemSubgroup As New AgControls.AgDataGrid
    Public Const Col1SubCode As String = "SubCode"
    Public Const Col1Description As String = "Description"

    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem

    Dim DtItemTypeSetting
    Dim DtUnit As DataTable



    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"


    Public Const rowItemType As Integer = 0
    Public Const rowItemCategory As Integer = 1
    Public Const rowItemGroup As Integer = 2
    Public Const rowBaseItem As Integer = 3
    Public Const rowDimension1 As Integer = 4
    Public Const rowDimension2 As Integer = 5
    Public Const rowDimension3 As Integer = 6
    Public Const rowDimension4 As Integer = 7
    Public Const rowSize As Integer = 8
    Public Const rowShape As Integer = 9
    Public Const rowShapeAreaFormula As Integer = 10
    Public Const rowShapePerimeterFormula As Integer = 11
    Public Const rowShapeShortName As Integer = 12
    Public Const rowSizeUnit As Integer = 13
    Public Const rowLength As Integer = 14
    Public Const rowWidth As Integer = 15
    Public Const rowThickness As Integer = 16
    Public Const rowArea As Integer = 17
    Public Const rowPerimeter As Integer = 18
    Public Const rowItemCode As Integer = 19
    Public Const rowSpecification As Integer = 20
    Public Const rowItemName As Integer = 21
    Public Const rowUnit As Integer = 22
    Public Const rowStockUnit As Integer = 23
    Public Const rowSalesTaxGroup As Integer = 24
    Public Const rowPurchaseRate As Integer = 25
    Public Const rowSaleRate As Integer = 26
    Public Const rowMRP As Integer = 27
    Public Const rowMarginPer As Integer = 28
    Public Const rowHSN As Integer = 29
    Public Const rowDealUnit As Integer = 30
    Public Const rowDealQty As Integer = 31
    Public Const rowDefaultDiscountPerSale As Integer = 32
    Public Const rowDefaultAdditionPerSale As Integer = 33
    Public Const rowDefaultDiscountPerPurchase As Integer = 34
    Public Const rowMaintainStockYn As Integer = 35
    Public Const rowShowItemInOtherDivision As Integer = 36
    Public Const rowShowItemInOtherSites As Integer = 37
    Public Const rowBarcode As Integer = 38
    Public Const rowDefaultSupplier As Integer = 39
    Public Const rowSite As Integer = 40
    Public Const rowParent As Integer = 41
    Public Const rowTopParent As Integer = 42
    Public Const rowSalesAc As Integer = 43
    Public Const rowPurchaseAc As Integer = 44
    Public Const rowRemark As Integer = 45
    Public Const rowRemark1 As Integer = 46
    Public Const rowRemark2 As Integer = 47
    Public Const rowRemark3 As Integer = 48


    Public Const hcItemCategory As String = "Item Category"
    Public Const hcItemGroup As String = "Item Group"
    Public Const hcBaseItem As String = "Base Item"
    Public Const hcDimension1 As String = "Dimension1"
    Public Const hcDimension2 As String = "Dimension2"
    Public Const hcDimension3 As String = "Dimension3"
    Public Const hcDimension4 As String = "Dimension4"
    Public Const hcSize As String = "Size"
    Public Const hcShape As String = "Shape"
    Public Const hcShapeAreaFormula As String = "Shape Area Formula"
    Public Const hcShapePerimeterFormula As String = "Shape Perimeter Formula"
    Public Const hcSizeUnit As String = "Size Unit"
    Public Const hcShapeShortName As String = "Shape Short Name"
    Public Const hcLength As String = "Length"
    Public Const hcWidth As String = "Width"
    Public Const hcThickness As String = "Thickness"
    Public Const hcArea As String = "Area"
    Public Const hcPerimeter As String = "Perimeter"
    Public Const hcItemCode As String = "Item Code"
    Public Const hcSpecification As String = "Specification"
    Public Const hcItemName As String = "Item Name"
    Public Const hcUnit As String = "Unit"
    Public Const hcStockUnit As String = "Stock Unit"
    Public Const hcSalesTaxGroup As String = "Sales Tax Group"
    Public Const hcPurchaseRate As String = "Purchase Rate"
    Public Const hcSaleRate As String = "Sale Rate"
    Public Const hcMRP As String = "MRP"
    Public Const hcMarginPer As String = "Margin @"
    Public Const hcHSN As String = "HSN"
    Public Const hcDealUnit As String = "Deal Unit"
    Public Const hcDealQty As String = "Deal Qty"
    Public Const hcItemType As String = "Item Type"
    Public Const hcDefaultDiscountPerSale As String = "Default Disc. @ (Sale)"
    Public Const hcDefaultAdditionPerSale As String = "Default Addition @ (Sale)"
    Public Const hcDefaultDiscountPerPurchase As String = "Default Disc. @ (Purch.)"
    Public Const hcMaintainStockYn As String = "Maintain Stock Y/N"
    Public Const hcShowItemInOtherDivisions As String = "Show Item In Other Divisions"
    Public Const hcShowItemInOtherSites As String = "Show Item In Other Sites"
    Public Const hcBarcode As String = "Barcode"
    Public Const hcDefaultSupplier As String = "Default Supplier"
    Public Const hcSite As String = "Site"
    Public Const hcParent As String = "Parent"
    Public Const hcTopParent As String = "TopParent"
    Public Const hcSalesAc As String = "Sales Ac"
    Public Const hcPurchaseAc As String = "Purchase Ac"
    Public Const hcRemark As String = "Remark"
    Public Const hcRemark1 As String = "Remark1"
    Public Const hcRemark2 As String = "Remark2"
    Public Const hcRemark3 As String = "Remark3"




    Dim mItemTypeLastValue As String
    Public WithEvents PnlUnitConversion As Panel
    Public WithEvents PnlItemSubgroup As Panel
    Dim mItemVTypes As String

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strVType As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
        mItemVTypes = strVType
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.PicPhoto = New System.Windows.Forms.PictureBox()
        Me.BtnBrowse = New System.Windows.Forms.Button()
        Me.BtnPhotoClear = New System.Windows.Forms.Button()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel()
        Me.BtnUnitConversion = New System.Windows.Forms.Button()
        Me.BtnBOMDetail = New System.Windows.Forms.Button()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.PnlRateType = New System.Windows.Forms.Panel()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportRateListFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportRateListFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBulkEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportDesignFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBulkRateEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBarcodePrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.PnlUnitConversion = New System.Windows.Forms.Panel()
        Me.PnlItemSubgroup = New System.Windows.Forms.Panel()
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
        Me.Topctrl1.Size = New System.Drawing.Size(974, 41)
        Me.Topctrl1.TabIndex = 1000
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 574)
        Me.GroupBox1.Size = New System.Drawing.Size(1016, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(23, 578)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(1022, 526)
        Me.GBoxEntryType.Visible = False
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(227, 578)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 578)
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
        Me.GroupBox2.Location = New System.Drawing.Point(669, 578)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(443, 578)
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
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCustomGrid.Controls.Add(Me.TxtCustomFields)
        Me.PnlCustomGrid.Location = New System.Drawing.Point(958, 324)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(118, 89)
        Me.PnlCustomGrid.TabIndex = 17
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LblMaterialPlanForFollowingItems)
        Me.Panel1.Controls.Add(Me.PicPhoto)
        Me.Panel1.Controls.Add(Me.BtnBrowse)
        Me.Panel1.Controls.Add(Me.BtnPhotoClear)
        Me.Panel1.Location = New System.Drawing.Point(928, 236)
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
        Me.BtnUnitConversion.Location = New System.Drawing.Point(958, 74)
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
        Me.BtnBOMDetail.Location = New System.Drawing.Point(958, 97)
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
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(1033, 490)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1058
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(1047, 489)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(112, 14)
        Me.LblIsSystemDefine.TabIndex = 1059
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'PnlRateType
        '
        Me.PnlRateType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlRateType.Location = New System.Drawing.Point(7, 460)
        Me.PnlRateType.Name = "PnlRateType"
        Me.PnlRateType.Size = New System.Drawing.Size(421, 112)
        Me.PnlRateType.TabIndex = 1
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuImportRateListFromExcel, Me.MnuImportRateListFromDos, Me.MnuBulkEdit, Me.MnuImportDesignFromDos, Me.MnuBulkRateEdit, Me.MnuBarcodePrint})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(219, 202)
        Me.MnuOptions.Text = "Option"
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(218, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(218, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(218, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuImportRateListFromExcel
        '
        Me.MnuImportRateListFromExcel.Name = "MnuImportRateListFromExcel"
        Me.MnuImportRateListFromExcel.Size = New System.Drawing.Size(218, 22)
        Me.MnuImportRateListFromExcel.Text = "Import Rate List From Excel"
        '
        'MnuImportRateListFromDos
        '
        Me.MnuImportRateListFromDos.Name = "MnuImportRateListFromDos"
        Me.MnuImportRateListFromDos.Size = New System.Drawing.Size(218, 22)
        Me.MnuImportRateListFromDos.Text = "Import Rate List From Dos"
        '
        'MnuBulkEdit
        '
        Me.MnuBulkEdit.Name = "MnuBulkEdit"
        Me.MnuBulkEdit.Size = New System.Drawing.Size(218, 22)
        Me.MnuBulkEdit.Text = "Bulk Edit"
        '
        'MnuImportDesignFromDos
        '
        Me.MnuImportDesignFromDos.Name = "MnuImportDesignFromDos"
        Me.MnuImportDesignFromDos.Size = New System.Drawing.Size(218, 22)
        Me.MnuImportDesignFromDos.Text = "Import Design From Dos"
        '
        'MnuBulkRateEdit
        '
        Me.MnuBulkRateEdit.Name = "MnuBulkRateEdit"
        Me.MnuBulkRateEdit.Size = New System.Drawing.Size(218, 22)
        Me.MnuBulkRateEdit.Text = "Bulk Rate Edit"
        '
        'MnuBarcodePrint
        '
        Me.MnuBarcodePrint.Name = "MnuBarcodePrint"
        Me.MnuBarcodePrint.Size = New System.Drawing.Size(218, 22)
        Me.MnuBarcodePrint.Text = "Barcode Print"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(7, 49)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(962, 405)
        Me.Pnl1.TabIndex = 0
        '
        'BtnAttachments
        '
        Me.BtnAttachments.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAttachments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAttachments.Location = New System.Drawing.Point(838, 599)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(136, 23)
        Me.BtnAttachments.TabIndex = 1060
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Add Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'PnlUnitConversion
        '
        Me.PnlUnitConversion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlUnitConversion.Location = New System.Drawing.Point(635, 460)
        Me.PnlUnitConversion.Name = "PnlUnitConversion"
        Me.PnlUnitConversion.Size = New System.Drawing.Size(334, 112)
        Me.PnlUnitConversion.TabIndex = 1061
        '
        'PnlItemSubgroup
        '
        Me.PnlItemSubgroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlItemSubgroup.Location = New System.Drawing.Point(446, 460)
        Me.PnlItemSubgroup.Name = "PnlItemSubgroup"
        Me.PnlItemSubgroup.Size = New System.Drawing.Size(200, 100)
        Me.PnlItemSubgroup.TabIndex = 1062
        Me.PnlItemSubgroup.Visible = False
        '
        'FrmItemMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 622)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.PnlItemSubgroup)
        Me.Controls.Add(Me.PnlUnitConversion)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.PnlRateType)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Controls.Add(Me.BtnBOMDetail)
        Me.Controls.Add(Me.BtnUnitConversion)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.MaximizeBox = True
        Me.Name = "FrmItemMaster"
        Me.Text = "Item Master"
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.BtnUnitConversion, 0)
        Me.Controls.SetChildIndex(Me.BtnBOMDetail, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.PnlRateType, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
        Me.Controls.SetChildIndex(Me.PnlUnitConversion, 0)
        Me.Controls.SetChildIndex(Me.PnlItemSubgroup, 0)
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
    Public WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid
    Public WithEvents BtnUnitConversion As System.Windows.Forms.Button
    Public WithEvents BtnBOMDetail As System.Windows.Forms.Button
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuBulkEdit As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Friend WithEvents MnuImportRateListFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportRateListFromDos As ToolStripMenuItem
    Friend WithEvents Pnl1 As Panel
    Friend WithEvents MnuImportDesignFromDos As ToolStripMenuItem
    Protected WithEvents BtnAttachments As Button
    Friend WithEvents MnuBulkRateEdit As ToolStripMenuItem
    Friend WithEvents MnuBarcodePrint As ToolStripMenuItem
#End Region

    Private Sub FGetItemTypeSetting()
        If mItemTypeLastValue <> Dgl1(Col1Value, rowItemType).Tag And Dgl1(Col1Value, rowItemType).Tag <> "" Then
            mItemTypeLastValue = Dgl1(Col1Value, rowItemType).Tag
            mQry = "Select * From ItemTypeSetting Where ItemType = '" & Dgl1(Col1Value, rowItemType).Tag & "' And Div_Code = '" & TxtDivision.Tag & "' "
            DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
            If DtItemTypeSetting.Rows.Count = 0 Then
                mQry = "Select * From ItemTypeSetting Where ItemType = '" & Dgl1(Col1Value, rowItemType).Tag & "' And Div_Code Is Null "
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

        If DtItemTypeSetting Is Nothing Then Exit Sub
        Dgl1.Rows(rowDefaultDiscountPerSale).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_SaleDiscountInMaster"))
        Dgl1.Rows(rowDefaultAdditionPerSale).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_SaleDiscountInMaster"))
        Dgl1.Rows(rowBarcode).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_Barcode"))

        DGLRateType.Columns(Col1Discount).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_SaleDiscountInMaster"))
        DGLRateType.Columns(Col1Addition).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_SaleDiscountInMaster"))
    End Sub
    'Private Sub ApplyItemTypeSetting(ItemType As String)
    '    Dim mQry As String
    '    Dim DtTemp As DataTable
    '    Dim I As Integer, J As Integer
    '    Dim mDglRateTypeColumnCount As Integer
    '    Dim mDgl1RowCount As Integer
    '    Try
    '        For I = 0 To Dgl1.Rows.Count - 1
    '            Dgl1.Rows(I).Visible = False
    '        Next


    '        mQry = "Select H.*
    '                from EntryHeaderUISetting H                   
    '                Where EntryName= '" & Me.Name & "'  And V_Type = '" & mItemVTypes & "' And GridName ='" & Dgl1.Name & "' "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '        If DtTemp.Rows.Count = 0 Then
    '            mQry = "Select H.*
    '                from EntryHeaderUISetting H                   
    '                Where EntryName= '" & Me.Name & "'  And NCat = '" & ItemType & "' And GridName ='" & Dgl1.Name & "' "
    '            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '        End If

    '        If DtTemp.Rows.Count > 0 Then
    '            For I = 0 To DtTemp.Rows.Count - 1
    '                For J = 0 To Dgl1.Rows.Count - 1
    '                    If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1Head, J).Value Then
    '                        Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
    '                        If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
    '                        Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
    '                        If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
    '                            Dgl1.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
    '                        End If
    '                        'MsgBox(NameOf(rowAdditionalDiscountPatternPurchase))
    '                    End If
    '                Next
    '            Next
    '        End If



    '        If mDgl1RowCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True


    '        If mItemVTypes = ItemV_Type.Item Then

    '            mQry = "Select H.*
    '                from EntryLineUISetting H                    
    '                Where EntryName='" & Me.Name & "' And NCat = '" & ItemType & "' And GridName ='" & DGLRateType.Name & "' "
    '            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


    '            If DtTemp.Rows.Count > 0 Then
    '                For I = 0 To DtTemp.Rows.Count - 1
    '                    For J = 0 To DGLRateType.Columns.Count - 1
    '                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DGLRateType.Columns(J).Name Then
    '                            DGLRateType.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
    '                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglRateTypeColumnCount += 1
    '                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
    '                                DGLRateType.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
    '                            End If
    '                            'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
    '                        End If
    '                    Next
    '                Next
    '            End If
    '        End If
    '        If mDglRateTypeColumnCount = 0 Then
    '            DGLRateType.Visible = False
    '        Else
    '            If DGLRateType.RowCount > 0 Then
    '                DGLRateType.Visible = True
    '            End If
    '        End If


    '        For I = 0 To Dgl1.Rows.Count - 1
    '            If Dgl1.Rows(I).Visible = True Then
    '                mDgl1LastRowIndex = I
    '            End If
    '        Next

    'Dgl1.Rows(rowRemark).Visible = True
    'Dgl1.Item(Col1Head, rowRemark).Value = "Pack Size"
    '        'If DGLRateType.Visible = False Then
    '        '    Dgl1.Height = DGLRateType.Bottom - Dgl1.Top
    '        'End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
    '    End Try
    'End Sub

    Private Sub ApplyUISetting()
        GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, Dgl1.Item(Col1Value, rowItemType).Tag, mItemVTypes, "", "", ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(DGLRateType, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, Dgl1.Item(Col1Value, rowItemType).Tag, mItemVTypes, "", "", ClsMain.GridTypeConstants.HorizontalGrid)
        GetUISetting_WithDataTables(DGLUnitConversion, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, Dgl1.Item(Col1Value, rowItemType).Tag, mItemVTypes, "", "", ClsMain.GridTypeConstants.HorizontalGrid)
        'GetUISetting_WithDataTables(DGLItemSubgroup, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, Dgl1.Item(Col1Value, rowItemType).Tag, mItemVTypes, "", "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub FrmItemMasterNew_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim dtTemp As DataTable

        mQry = "Select Code From RateList With (NoLock) Where GenDocId = '" & mSearchCode & "' And GenV_Type='" & ItemV_Type.Item & "'"
        dtTemp = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            mQry = "DELETE FROM RateListDetail WHERE Code='" & AgL.XNull(dtTemp.Rows(0)("Code")) & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "DELETE FROM RateList WHERE Code = '" & AgL.XNull(dtTemp.Rows(0)("Code")) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        mQry = "DELETE FROM UnitConversion WHERE Item = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM BOMDetail WHERE Code = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM ItemSize WHERE Code = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim i As Integer

        Dgl1.EndEdit()
        DGLRateType.EndEdit()
        DGLUnitConversion.EndEdit()

        For i = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Value, i).Value Is Nothing Then Dgl1.Item(Col1Value, i).Value = ""
            If Dgl1.Item(Col1Value, i).Tag Is Nothing Then Dgl1.Item(Col1Value, i).Tag = ""
        Next


        For i = 0 To Dgl1.RowCount - 1
            If Dgl1(Col1Mandatory, i).Value <> "" And Dgl1.Rows(i).Visible Then
                If Dgl1(Col1Value, i).Value.ToString = "" Then
                    MsgBox(Dgl1(Col1Head, i).Value.ToString & " can not be blank.")
                    passed = False
                    Dgl1.CurrentCell = Dgl1(Col1Value, i)
                    Dgl1.Focus()
                    Exit Sub
                End If
            End If
        Next



        If Topctrl1.Mode = "Add" Then
            If AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Product", "") = "Spare" Then
            Else
                If AgL.PubServerName = "" Then
                    Dgl1(Col1Value, rowItemCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
                Else
                    Dgl1(Col1Value, rowItemCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
                End If
            End If

            If AgL.XNull(AgL.Dman_Execute("Select BarcodeType From Item IG With (NoLock) Where IG.Code = '" & Dgl1.Item(Col1Value, rowItemGroup).Tag & "' And BarcodePattern = '" & BarcodePattern.Auto & "' ", AgL.GCn).executescalar()) = BarcodeType.Fixed Then
                If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" And (AgL.StrCmp(AgL.PubDBName, "Sadhvi") Or AgL.StrCmp(AgL.PubDBName, "Sadhvi2")) Then
                    Dgl1.Item(Col1Value, rowBarcode).Value = AgL.Dman_Execute("Select IfNull(Max(CAST(Description as BIGINT)),0) + 1 From BarCode  With (NoLock) WHERE Item NOT IN ('Lr','LrBale') ", AgL.GCn).ExecuteScalar()
                Else
                    Dgl1.Item(Col1Value, rowBarcode).Value = AgL.XNull(AgL.Dman_Execute("Select IfNull(Max(CAST(Description as BIGINT)),0) + 1 From BarCode  With (NoLock)", AgL.GCn).ExecuteScalar())
                End If
            End If


            mQry = "Select count(*) From Item Where ManualCode ='" & Dgl1(Col1Value, rowItemCode).Value & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Replace(Replace(Description,' ',''),'-','') ='" & Dgl1(Col1Value, rowItemName).Value.ToString.Replace(" ", "").Replace("-", "") & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")

            mQry = "Select count(*) From Item WHERE ItemGroup = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemGroup).Tag) & " AND ItemCategory = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemCategory).Tag) & "  AND Specification  ='" & Dgl1(Col1Value, rowSpecification).Value & "'  AND HSN  ='" & Dgl1(Col1Value, rowHSN).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Item Already Exist!")

            If Dgl1.Item(Col1Value, rowBarcode).Value.ToString <> "" Then
                mQry = "Select count(*) From Barcode Where Description ='" & Dgl1(Col1Value, rowBarcode).Value & "' "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Barcode Already Exist!")
            End If
        Else
            mQry = "Select count(*) From Item Where ManualCode ='" & Dgl1(Col1Value, rowItemCode).Value & "' And Code <>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Replace(Replace(Description,' ',''),'-','') ='" & Dgl1(Col1Value, rowItemName).Value.ToString.Replace(" ", "").Replace("-", "") & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")

            mQry = "Select count(*) From Item WHERE ItemGroup = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemGroup).Tag) & " AND ItemCategory = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemCategory).Tag) & "  AND Specification  ='" & Dgl1(Col1Value, rowSpecification).Value & "' AND HSN  ='" & Dgl1(Col1Value, rowHSN).Value & "'  AND Status ='Active' AND V_Type ='Item' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Item Already Exist!")

            If Dgl1.Item(Col1Value, rowBarcode).Value <> "" Then
                mQry = "Select count(*) From Barcode Where Description='" & Dgl1(Col1Value, rowBarcode).Value & "' And  Item <> '" & mInternalCode & "' And Item=GenDocID "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Barcode Already Exist!")
            End If

        End If


        If Dgl1.Item(Col1Value, rowMaintainStockYn).Value = Nothing Then Dgl1.Item(Col1Value, rowMaintainStockYn).Value = ""
        If Dgl1.Item(Col1Value, rowMaintainStockYn).Value = "" Then Dgl1.Item(Col1Value, rowMaintainStockYn).Value = "YES"

        If AgL.XNull(Dgl1.Item(Col1Value, rowSite).Tag) = "" Then Dgl1.Item(Col1Value, rowSite).Tag = AgL.PubSiteCode

        SetLastValues()
    End Sub
    Private Sub SetLastValues()
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1LastValue, I).Value = Dgl1(Col1Value, I).Value
            Dgl1(Col1LastValue, I).Tag = Dgl1(Col1Value, I).Tag
        Next
    End Sub
    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = " Where H.V_Type='" & mItemVTypes & "' AND (IT.Parent <> '" & ItemTypeCode.InternalProduct & "' OR H.ItemType Is Null )"
        mQry = "Select H.Code As SearchCode " &
                " From Item H Left Join ItemType IT On H.ItemType = IT.Code  " & mConStr &
                " Order By H.Description "
        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Public Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where I.V_Type='" & mItemVTypes & "' AND (IT.Parent <> '" & ItemTypeCode.InternalProduct & "' OR I.ITEMTYPE IS NULL) "

        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Description, I.ManualCode as [Item Code], I.Specification, " &
            " IG.Description AS [Item Group], IC.Description AS [Item Category], IT.Name AS [Item Type], I.Unit, I.PurchaseRate as [Purchase Rate], I.Rate as [Sale Rate], " &
            " S.Description As Shape, ISize.Length, ISize.Width, ISize.Thickness, ISize.Area, ISize.Perimeter " &
            " FROM Item I " &
            " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup " &
            " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
            " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
            " LEFT JOIN ItemSize ISize ON I.Code = ISize.Code " &
            " LEFT JOIN Shape S ON ISize.Shape = S.Code " &
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
            'If Dgl1.Item(Col1Value, rowBarcode).Value <> "" And Dgl1.Item(Col1Value, rowBarcode).Value <> Nothing Then
            If AgL.XNull(Dgl1.Item(Col1Value, rowBarcode).Value) <> "" Then
                mCode = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode With (NoLock)", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()
                mQry = " INSERT INTO Barcode (Code, Description, Item, Div_Code, GenDocID, GenSr, Qty)
                    VALUES (" & AgL.Chk_Text(mCode) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcode).Value) & ", " & AgL.Chk_Text(SearchCode) & ",
                    " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(SearchCode) & ", 1, 0) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " INSERT INTO BarcodeSiteDetail (Code,Div_Code, Site_Code, LastTrnDocID,
                        LastTrnSr, LastTrnV_Type, LastTrnManualRefNo,
                        LastTrnSubcode, LastTrnProcess, CurrentGodown, Status)
                        VALUES (" & AgL.Chk_Text(mCode) & ", " & AgL.Chk_Text(TxtDivision.Tag) & ", 
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
                " V_Type = " & AgL.Chk_Text(mItemVTypes) & ", " &
                " ManualCode = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemCode).Value) & ", " &
                " Specification = " & AgL.Chk_Text(Dgl1(Col1Value, rowSpecification).Value) & ", " &
                " Description = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemName).Value) & ", " &
                " BaseItem = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBaseItem).Tag) & ", " &
                " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension1).Tag) & ", " &
                " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension2).Tag) & ", " &
                " Dimension3 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension3).Tag) & ", " &
                " Dimension4 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension4).Tag) & ", " &
                " Size = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSize).Tag) & ", " &
                " Hsn = " & AgL.Chk_Text(Dgl1(Col1Value, rowHSN).Value) & ", " &
                " Unit = " & AgL.Chk_Text(Dgl1(Col1Value, rowUnit).Value) & ", " &
                " StockUnit = " & AgL.Chk_Text(Dgl1(Col1Value, rowStockUnit).Value) & ", " &
                " DealUnit = " & AgL.Chk_Text(Dgl1(Col1Value, rowDealUnit).Value) & ", " &
                " DealQty = " & Val(Dgl1(Col1Value, rowDealQty).Value) & ", " &
                " PurchaseRate = " & Val(Dgl1(Col1Value, rowPurchaseRate).Value) & ", " &
                " Rate = " & Val(Dgl1(Col1Value, rowSaleRate).Value) & ", " &
                " Default_MarginPer = " & Val(Dgl1(Col1Value, rowMarginPer).Value) & ", " &
                " ItemGroup = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemGroup).Tag) & ", " &
                " ItemCategory = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemCategory).Tag) & ", " &
                " ItemType = " & AgL.Chk_Text(Dgl1(Col1Value, rowItemType).Tag) & ", " &
                " StockYN = 1, " &
                " IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ", " &
                " MaintainStockYn = " & IIf(Dgl1.Item(Col1Value, rowMaintainStockYn).Value.ToUpper = "NO", 0, 1) & ", " &
                " ShowItemInOtherDivisions = " & IIf(Dgl1.Item(Col1Value, rowShowItemInOtherDivision).Value.ToUpper = "YES", 1, 0) & ", " &
                " ShowItemInOtherSites = " & IIf(Dgl1.Item(Col1Value, rowShowItemInOtherSites).Value.ToUpper = "YES", 1, 0) & ", " &
                " SalesTaxPostingGroup = " & AgL.Chk_Text(Dgl1(Col1Value, rowSalesTaxGroup).Value) & ", " &
                " Barcode = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcode).Tag) & ", " &
                " Default_DiscountPerSale = " & Val(Dgl1.Item(Col1Value, rowDefaultDiscountPerSale).Value) & "," &
                " Default_AdditionPerSale = " & Val(Dgl1.Item(Col1Value, rowDefaultAdditionPerSale).Value) & "," &
                " Default_DiscountPerPurchase = " & Val(Dgl1.Item(Col1Value, rowDefaultDiscountPerPurchase).Value) & "," &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                " DefaultSupplier = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDefaultSupplier).Tag) & ", " &
                " PurchaseAc = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPurchaseAc).Tag) & ", " &
                " SalesAc = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesAc).Tag) & ", " &
                " Parent = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowParent).Tag) & ", " &
                " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRemark).Value) & ", " &
                " Remark1 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRemark1).Value) & ", " &
                " Remark2 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRemark2).Value) & ", " &
                " Remark3 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRemark3).Value) & ", " &
                " UploadDate = Null, " &
                " TopParent = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTopParent).Tag) & ", " &
                " Site_Code = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSite).Tag) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Call FPostRateInRateList(Conn, Cmd)

        mQry = "Delete From ItemSize Where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If AgL.XNull(Dgl1.Item(Col1Value, rowShape).Value) <> "" Or
                AgL.XNull(Dgl1.Item(Col1Value, rowSizeUnit).Value) <> "" Or
                AgL.VNull(Dgl1.Item(Col1Value, rowLength).Value) > 0 Or
                AgL.VNull(Dgl1.Item(Col1Value, rowWidth).Value) > 0 Or
                AgL.VNull(Dgl1.Item(Col1Value, rowThickness).Value) > 0 Or
                AgL.VNull(Dgl1.Item(Col1Value, rowArea).Value) > 0 Or
                AgL.VNull(Dgl1.Item(Col1Value, rowPerimeter).Value) > 0 Then
            mQry = "Insert Into ItemSize (Code, Shape, SizeUnit, Length, Width, Thickness, Area, Perimeter)
                    Values ('" & SearchCode & "', " & AgL.Chk_Text(AgL.XNull(Dgl1.Item(Col1Value, rowShape).Tag)) & "
                            , " & AgL.Chk_Text(AgL.XNull(Dgl1.Item(Col1Value, rowSizeUnit).Tag)) & "
                            , " & AgL.Chk_Text(AgL.XNull(Dgl1.Item(Col1Value, rowLength).Value)) & "
                            , " & AgL.Chk_Text(AgL.XNull(Dgl1.Item(Col1Value, rowWidth).Value)) & "
                            , " & AgL.Chk_Text(AgL.XNull(Dgl1.Item(Col1Value, rowThickness).Value)) & "
                            , " & AgL.Chk_Text(AgL.XNull(Dgl1.Item(Col1Value, rowArea).Value)) & "
                            , " & AgL.Chk_Text(AgL.XNull(Dgl1.Item(Col1Value, rowPerimeter).Value)) & "
                           )"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "Delete From UnitConversion Where Item = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I As Integer = 0 To DGLUnitConversion.RowCount - 1
            If DGLUnitConversion.Item(Col1FromUnit, I).Value <> "" Then
                Dim bUnitConversionCode As String = AgL.GetMaxId("UnitConversion", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                mQry = "INSERT INTO UnitConversion(Code, Item, FromUnit, ToUnit, Multiplier) " &
                      " VALUES (" & AgL.Chk_Text(bUnitConversionCode) & ", " &
                      " " & AgL.Chk_Text(SearchCode) & ", " &
                      " " & AgL.Chk_Text(DGLUnitConversion.Item(Col1FromUnit, I).Value) & ", " &
                      " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowStockUnit).Value) & ", 
                      " & Val(DGLUnitConversion.Item(Col1Multiplier, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next

        mQry = "Delete From ItemSubgroup Where Item = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I As Integer = 0 To DGLItemSubgroup.RowCount - 1
            If DGLItemSubgroup.Item(Col1SubCode, I).Value <> "" Then
                Dim bItemSubgroupCode As String = AgL.GetMaxId("ItemSubgroup", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                mQry = "INSERT INTO ItemSubgroup(Code, Item, SubCode, Description) " &
                      " VALUES (" & AgL.Chk_Text(bItemSubgroupCode) & ", " &
                      " " & AgL.Chk_Text(SearchCode) & ", " &
                      " " & AgL.Chk_Text(DGLItemSubgroup.Item(Col1SubCode, I).tag) & ", " &
                      " " & AgL.Chk_Text(DGLItemSubgroup.Item(Col1Description, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next

        'If FDivisionNameForCustomization(15) = "MANISH TEXTILES" Or FDivisionNameForCustomization(13) = "JEET TEXTILES" Then
        '    mQry = " UPDATE Item SET Rate = " & Val(Dgl1.Item(Col1Value, rowSaleRate).Value) & " WHERE Code IN (SELECT I.Code FROM Item I WHERE I.BaseItem = '" & mSearchCode & "') "
        '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        '    mQry = "UPDATE RateListDetail SET Rate = " & Val(Dgl1.Item(Col1Value, rowSaleRate).Value) & " WHERE Code IN (SELECT I.Code FROM Item I WHERE I.BaseItem = '" & mSearchCode & "') "
        '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        'End If

        FUpdateDimensionRates(Conn, Cmd)

        If PubDtSaleInvoiceItemHelp IsNot Nothing Then PubDtSaleInvoiceItemHelp = Nothing
    End Sub
    Private Sub FPostRateInRateList(ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim dtTemp As DataTable
        Dim I As Integer, mSr As Integer

        'mQry = "Select Code From RateList With (NoLock) Where GenDocId = '" & mSearchCode & "' And GenV_Type='" & mItemVTypes & "'"
        mQry = "Select Code From RateList With (NoLock) Where GenDocId = '" & mSearchCode & "' And GenV_Type='" & mItemVTypes & "'"
        dtTemp = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            bRateListCode = AgL.XNull(dtTemp.Rows(0)("Code"))

            mQry = "DELETE FROM RateListDetail WHERE Code='" & bRateListCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "DELETE FROM RateList WHERE Code = '" & bRateListCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Else
            bRateListCode = AgL.GetMaxId("RateList", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        End If

        If mItemVTypes = ItemV_Type.Item Then
            mQry = " INSERT INTO RateList(Code, WEF, EntryBy, EntryDate, EntryType, " &
                " EntryStatus, Status, Div_Code, GenDocId, GenV_Type) " &
                " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                " " & AgL.Chk_Text(Topctrl1.Mode) & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                " '" & TxtDivision.AgSelectedValue & "', " & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mItemVTypes) & ")"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO RateListDetail(Code, Sr, Item, RateType, Rate) " &
                  " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " &
                  " 0, " &
                  " " & AgL.Chk_Text(mSearchCode) & ", " &
                  " NULL, " & Val(Dgl1(Col1Value, rowSaleRate).Value) & " ) "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            For I = 0 To DGLRateType.RowCount - 1
                If DGLRateType.Item(Col1RateType, I).Value <> "" Then
                    mSr += 1

                    mQry = "INSERT INTO RateListDetail(Code, Sr, Item, RateType, Rate, DiscountPer, AdditionPer) " &
                  " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " &
                  " " & mSr & ",  " &
                  " " & AgL.Chk_Text(mSearchCode) & ", " &
                  " " & AgL.Chk_Text(DGLRateType.Item(Col1RateType, I).Tag) & ", " & Val(DGLRateType.Item(Col1Rate, I).Value) & ", " & Val(DGLRateType.Item(Col1Discount, I).Value) & ", " & Val(DGLRateType.Item(Col1Addition, I).Value) & " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        Else
            If Val(Dgl1(Col1Value, rowSaleRate).Value) > 0 Then

                If FDivisionNameForCustomization(15) = "MANISH TEXTILES" Then
                    mQry = "DELETE FROM RateListDetail WHERE Item ='" & mSearchCode & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If

                mQry = " INSERT INTO RateList(Code, WEF, EntryBy, EntryDate, EntryType, " &
                        " EntryStatus, Status, Div_Code, GenDocId, GenV_Type) " &
                        " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                        " " & AgL.Chk_Text(Topctrl1.Mode) & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                        " '" & TxtDivision.AgSelectedValue & "', " & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mItemVTypes) & ")"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = "INSERT INTO RateListDetail(Code, Sr, Item, RateType, Rate) " &
                      " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " &
                      " 0, " &
                      " " & AgL.Chk_Text(mSearchCode) & ", " &
                      " NULL, " & Val(Dgl1(Col1Value, rowSaleRate).Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub

    'Private Sub FSaveUnitConversion(ByVal Conn As Object, ByVal Cmd As Object)
    '    Dim I As Integer
    '    mQry = "DELETE FROM UnitConversion WHERE Item = '" & mSearchCode & "'"
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '    If BtnUnitConversion.Tag IsNot Nothing Then
    '        With BtnUnitConversion.Tag.Dgl1
    '            For I = 0 To .Rows.Count - 1
    '                If .Item(FrmItemMasterUnitConversion.Col1FromUnit, I).Value <> "" Then
    '                    mQry = " INSERT INTO UnitConversion ( Item,FromUnit,ToUnit,FromQty,ToQty,Multiplier,EntryBy,EntryDate,EntryType,EntryStatus, " &
    '                            " Status,Div_Code ) " &
    '                            " VALUES ( " & AgL.Chk_Text(mSearchCode) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMasterUnitConversion.Col1FromUnit, I).Value) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMasterUnitConversion.Col1ToUnit, I).Value) & ", " &
    '                            " " & Val(.Item(FrmItemMasterUnitConversion.Col1FromQty, I).Value) & ", " &
    '                            " " & Val(.Item(FrmItemMasterUnitConversion.Col1ToQty, I).Value) & ", " &
    '                            " " & Val(.Item(FrmItemMasterUnitConversion.Col1Multiplier, I).Value) & ", " &
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
    '                If .Item(FrmItemMasterBOMDetail.Col1Item, I).Value <> "" Then
    '                    mQry = " INSERT INTO BomDetail ( Sr, Item, Qty, Process, Dimension1, Dimension2, " &
    '                            " Unit,WastagePer, BatchQty, BatchUnit, BaseItem ) " &
    '                            " VALUES ( " & I + 1 & "," &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Item, I).tag) & ", " &
    '                            " " & Val(.Item(FrmItemMasterBOMDetail.Col1Qty, I).Value) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Process, I).tag) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).tag) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).tag) & ", " &
    '                            " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Unit, I).Value) & ", " &
    '                            " " & Val(.Item(FrmItemMasterBOMDetail.Col1WastagePer, I).Value) & ", " &
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
        Dim DtTemp As DataTable
        IniGrid()
        mQry = "Select I.*, Ig.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, " &
                " IT.Name AS ItemTypeName, BI.Description As BaseItemName, IfNull(V.Cnt,0) AS Cnt, IG.Default_MarginPer as Default_MarginPerGroup, Ds.Name as DefaultSupplierName, Sm.Name As SiteName " &
                " , D1.Description as Dimension1Name , D2.Description as Dimension2Name " &
                " , D3.Description as Dimension3Name , D4.Description as Dimension4Name " &
                " , Size.Description as SizeName , Parent.Description as ParentName " &
                " , TopParent.Description as TopParentName, PAC.Name as PurchaseAcName, SAC.Name as SalesAcName " &
                " From Item I With (NoLock) " &
                " LEFT JOIN Item Ig With (NoLock) ON I.ItemGroup = IG.Code " &
                " LEFT JOIN Item IC With (NoLock) ON IC.Code = I.ItemCategory " &
                " LEFT JOIN ItemType IT With (NoLock)  ON IT.Code = I.ItemType " &
                " LEFT JOIN Item Bi With (NoLock) ON Bi.Code = I.BaseItem " &
                " LEFT JOIN Item D1 With (NoLock) ON D1.Code = I.Dimension1 " &
                " LEFT JOIN Item D2 With (NoLock) ON D2.Code = I.Dimension2 " &
                " LEFT JOIN Item D3 With (NoLock) ON D3.Code = I.Dimension3 " &
                " LEFT JOIN Item D4 With (NoLock) ON D4.Code = I.Dimension4 " &
                " LEFT JOIN Item Size With (NoLock) ON Size.Code = I.Size " &
                " LEFT JOIN Item Parent With (NoLock) ON Parent.Code = I.Parent " &
                " LEFT JOIN Item TopParent With (NoLock) ON TopParent.Code = I.TopParent " &
                " Left Join viewHelpSubgroup Ds With (NoLock) on I.DefaultSupplier = Ds.Code" &
                " Left Join SiteMast Sm With (NoLock) on I.Site_Code = Sm.Code " &
                " LEFT JOIN ( SELECT L.Code, count(*) AS Cnt  FROM BomDetail L  With (NoLock) GROUP BY L.Code ) V ON V.Code = I.Code " &
                " Left Join viewHelpSubgroup PAC On I.PurchaseAc = PAC.Code " &
                " Left Join viewHelpSubgroup SAC On I.SalesAc = SAC.Code " &
                " Where I.Code ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                Dgl1(Col1Value, rowItemCode).Value = AgL.XNull(.Rows(0)("ManualCode"))
                Dgl1(Col1Value, rowItemName).Value = AgL.XNull(.Rows(0)("Description"))
                Dgl1(Col1Value, rowSpecification).Value = AgL.XNull(.Rows(0)("Specification"))


                Dgl1(Col1Value, rowBaseItem).Tag = AgL.XNull(.Rows(0)("BaseItem"))
                Dgl1(Col1Value, rowBaseItem).Value = AgL.XNull(.Rows(0)("BaseItemName"))
                Dgl1(Col1Value, rowDimension1).Tag = AgL.XNull(.Rows(0)("Dimension1"))
                Dgl1(Col1Value, rowDimension1).Value = AgL.XNull(.Rows(0)("Dimension1Name"))
                Dgl1(Col1Value, rowDimension2).Tag = AgL.XNull(.Rows(0)("Dimension2"))
                Dgl1(Col1Value, rowDimension2).Value = AgL.XNull(.Rows(0)("Dimension2Name"))
                Dgl1(Col1Value, rowDimension3).Tag = AgL.XNull(.Rows(0)("Dimension3"))
                Dgl1(Col1Value, rowDimension3).Value = AgL.XNull(.Rows(0)("Dimension3Name"))
                Dgl1(Col1Value, rowDimension4).Tag = AgL.XNull(.Rows(0)("Dimension4"))
                Dgl1(Col1Value, rowDimension4).Value = AgL.XNull(.Rows(0)("Dimension4Name"))
                Dgl1(Col1Value, rowSize).Tag = AgL.XNull(.Rows(0)("Size"))
                Dgl1(Col1Value, rowSize).Value = AgL.XNull(.Rows(0)("SizeName"))
                Dgl1(Col1Value, rowParent).Tag = AgL.XNull(.Rows(0)("Parent"))
                Dgl1(Col1Value, rowParent).Value = AgL.XNull(.Rows(0)("ParentName"))
                Dgl1(Col1Value, rowTopParent).Tag = AgL.XNull(.Rows(0)("TopParent"))
                Dgl1(Col1Value, rowTopParent).Value = AgL.XNull(.Rows(0)("TopParentName"))
                Dgl1(Col1Value, rowDealQty).Value = AgL.XNull(.Rows(0)("DealQty"))
                Dgl1(Col1Value, rowDealUnit).Value = AgL.XNull(.Rows(0)("DealUnit"))
                Dgl1(Col1Value, rowHSN).Value = AgL.XNull(.Rows(0)("Hsn"))
                Dgl1(Col1Value, rowUnit).Value = AgL.XNull(.Rows(0)("Unit"))
                Dgl1(Col1Value, rowStockUnit).Value = AgL.XNull(.Rows(0)("StockUnit"))
                Dgl1(Col1Value, rowPurchaseRate).Value = AgL.VNull(.Rows(0)("PurchaseRate"))
                Dgl1(Col1Value, rowPurchaseRate).Tag = AgL.VNull(.Rows(0)("PurchaseRate"))
                Dgl1(Col1Value, rowSaleRate).Value = AgL.VNull(.Rows(0)("Rate"))
                Dgl1(Col1Value, rowSaleRate).Tag = AgL.VNull(.Rows(0)("Rate"))

                Dgl1(Col1Value, rowItemGroup).Tag = AgL.XNull(.Rows(0)("ItemGroup"))
                Dgl1(Col1Value, rowItemGroup).Value = AgL.XNull(.Rows(0)("ItemGroupDesc"))

                Dgl1(Col1Value, rowPurchaseAc).Tag = AgL.XNull(.Rows(0)("PurchaseAc"))
                Dgl1(Col1Value, rowPurchaseAc).Value = AgL.XNull(.Rows(0)("PurchaseAcName"))

                Dgl1(Col1Value, rowSalesAc).Tag = AgL.XNull(.Rows(0)("SalesAc"))
                Dgl1(Col1Value, rowSalesAc).Value = AgL.XNull(.Rows(0)("SalesAcName"))


                If FGetSettings(SettingFields.Default_MarginBaseField, SettingType.General) = DefaultMarginBaseField.Item Then
                    Dgl1(Col1Value, rowMarginPer).Value = AgL.VNull(.Rows(0)("Default_MarginPer"))
                Else
                    Dgl1(Col1Value, rowMarginPer).Value = AgL.VNull(.Rows(0)("Default_MarginPerGroup"))
                End If


                Dgl1(Col1Value, rowItemCategory).Value = AgL.XNull(.Rows(0)("ItemCategoryDesc"))
                Dgl1(Col1Value, rowItemCategory).Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                Dgl1(Col1Value, rowItemType).Value = AgL.XNull(.Rows(0)("ItemTypeName"))
                Dgl1(Col1Value, rowItemType).Tag = AgL.XNull(.Rows(0)("ItemType"))
                'ApplyItemTypeSetting(Dgl1.Item(Col1Value, rowItemType).Tag)
                ApplyUISetting()
                FGetItemTypeSetting()
                Dgl1(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False



                Dgl1.Item(Col1Value, rowDefaultDiscountPerSale).Value = Format(AgL.VNull(.Rows(0)("Default_DiscountPerSale")), "0.00")
                Dgl1.Item(Col1Value, rowDefaultAdditionPerSale).Value = Format(AgL.VNull(.Rows(0)("Default_AdditionPerSale")), "0.00")
                Dgl1.Item(Col1Value, rowDefaultDiscountPerPurchase).Value = Format(AgL.VNull(.Rows(0)("Default_DiscountPerPurchase")), "0.00")
                Dgl1.Item(Col1Value, rowShowItemInOtherDivision).Value = IIf((.Rows(0)("ShowItemInOtherDivisions")), "Yes", "No")
                Dgl1.Item(Col1Value, rowShowItemInOtherSites).Value = IIf((.Rows(0)("ShowItemInOtherSites")), "Yes", "No")
                Dgl1.Item(Col1Value, rowMaintainStockYn).Value = IIf((.Rows(0)("MaintainStockYn")), "Yes", "No")
                Dgl1.Item(Col1Value, rowMRP).Value = Format(AgL.VNull(.Rows(0)("MRP")), "0.00")
                Dgl1.Item(Col1Value, rowBarcode).Tag = AgL.XNull(.Rows(0)("Barcode"))
                Dgl1.Item(Col1Value, rowBarcode).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Barcode Where Code = '" & Dgl1.Item(Col1Value, rowBarcode).Tag & "'", AgL.GCn).ExecuteScalar)
                Dgl1.Item(Col1Value, rowDefaultSupplier).Tag = AgL.XNull(.Rows(0)("DefaultSupplier"))
                Dgl1.Item(Col1Value, rowDefaultSupplier).Value = AgL.XNull(.Rows(0)("DefaultSupplierName"))
                Dgl1.Item(Col1Value, rowSite).Tag = AgL.XNull(.Rows(0)("Site_Code"))
                Dgl1.Item(Col1Value, rowSite).Value = AgL.XNull(.Rows(0)("SiteName"))
                Dgl1.Item(Col1Value, rowRemark).Value = AgL.XNull(.Rows(0)("Remark"))
                Dgl1.Item(Col1Value, rowRemark1).Value = AgL.XNull(.Rows(0)("Remark1"))
                Dgl1.Item(Col1Value, rowRemark2).Value = AgL.XNull(.Rows(0)("Remark2"))
                Dgl1.Item(Col1Value, rowRemark3).Value = AgL.XNull(.Rows(0)("Remark3"))



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



                If mItemVTypes = ItemV_Type.Item Or mItemVTypes = ItemV_Type.ItemGroup Then
                    Dim I As Integer
                    mQry = " Select  H.Code, H.Description, IGRT.Margin, H.CalculateOnRateType, Rt.Description as CalculateOnRateTypeName, L.Rate, L.DiscountPer, L.AdditionPer 
                        From RateType H 
                        Left Join RateType Rt On H.CalculateOnRateType = Rt.Code
                        Left Join ItemGroupRateType IGRT On H.Code = IGRT.RateType And IGRT.Code = '" & Dgl1.Item(Col1Value, rowItemGroup).Tag & "'
                        Left join (Select Item, RateType, Max(Rate) Rate, Max(DiscountPer) DiscountPer, Max(AdditionPer) as AdditionPer From RateListDetail Group By Item, RateType) L on L.RateType = H.Code And L.Item='" & SearchCode & "' 
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
                                DGLRateType.Item(Col1CalculateOnRateType, I).Tag = AgL.XNull(.Rows(I)("CalculateOnRateType"))
                                DGLRateType.Item(Col1CalculateOnRateType, I).Value = AgL.XNull(.Rows(I)("CalculateOnRateTypeName"))

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
                End If
                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))
            End If
        End With


        mQry = "Select S.*, Shape.Description as ShapeName, Shape.AreaFormula, Shape.PerimeterFormula, Shape.ShortName as ShapeShortName 
                from ItemSize S 
                Left Join Shape On S.Shape = Shape.Code
                Where S.Code ='" & SearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowShape).Tag = AgL.XNull(DtTemp.Rows(0)("Shape"))
            Dgl1.Item(Col1Value, rowShape).Value = AgL.XNull(DtTemp.Rows(0)("ShapeName"))
            Dgl1.Item(Col1Value, rowShapeAreaFormula).Value = AgL.XNull(DtTemp.Rows(0)("AreaFormula"))
            Dgl1.Item(Col1Value, rowShapePerimeterFormula).Value = AgL.XNull(DtTemp.Rows(0)("PerimeterFormula"))
            Dgl1.Item(Col1Value, rowShapeShortName).Value = AgL.XNull(DtTemp.Rows(0)("ShapeShortName"))
            Dgl1.Item(Col1Value, rowSizeUnit).Tag = AgL.XNull(DtTemp.Rows(0)("SizeUnit"))
            Dgl1.Item(Col1Value, rowSizeUnit).Value = AgL.XNull(DtTemp.Rows(0)("SizeUnit"))
            Dgl1.Item(Col1Value, rowLength).Value = AgL.XNull(DtTemp.Rows(0)("Length"))
            Dgl1.Item(Col1Value, rowWidth).Value = AgL.XNull(DtTemp.Rows(0)("Width"))
            Dgl1.Item(Col1Value, rowThickness).Value = AgL.XNull(DtTemp.Rows(0)("Thickness"))
            Dgl1.Item(Col1Value, rowArea).Value = AgL.XNull(DtTemp.Rows(0)("Area"))
            Dgl1.Item(Col1Value, rowPerimeter).Value = AgL.XNull(DtTemp.Rows(0)("Perimeter"))
        End If

        mQry = " SELECT * FROM UnitConversion Where Item = '" & mSearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGLUnitConversion.RowCount = 1 : DGLUnitConversion.Rows.Clear()
            For I As Integer = 0 To DsTemp.Tables(0).Rows.Count - 1
                DGLUnitConversion.Rows.Add()
                DGLUnitConversion.Item(ColSNo, I).Value = DGLUnitConversion.Rows.Count - 1
                DGLUnitConversion.Item(Col1FromUnit, I).Value = AgL.XNull(.Rows(I)("FromUnit"))
                DGLUnitConversion.Item(Col1Multiplier, I).Value = Format(AgL.VNull(.Rows(I)("Multiplier")), "0.00")
            Next I
        End With

        DsTemp = Nothing

        mQry = " SELECT SG.Name, ISG.* 
                FROM ItemSubGroup ISG
                LEFT JOIN SubGroup SG on SG.SubCode = ISG.SubCode
                Where ISG.Item = '" & mSearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGLItemSubgroup.RowCount = 1 : DGLItemSubgroup.Rows.Clear()
            For I As Integer = 0 To DsTemp.Tables(0).Rows.Count - 1
                DGLItemSubgroup.Rows.Add()
                DGLItemSubgroup.Item(ColSNo, I).Value = DGLItemSubgroup.Rows.Count - 1
                DGLItemSubgroup.Item(Col1SubCode, I).Value = AgL.XNull(.Rows(I)("Name"))
                DGLItemSubgroup.Item(Col1Description, I).Value = AgL.XNull(.Rows(I)("Description"))
            Next I
        End With

        DsTemp = Nothing

        SetLastValues()

        SetAttachmentCaption()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub


    Sub SetProductName()
        Dim mName As String

        If mItemVTypes = ItemV_Type.Item Then
            If Dgl1.Item(Col1Value, rowSpecification).Value = "" Then Exit Sub
            mName = FGetSettings(SettingFields.ItemNamePattern, SettingType.General)
            If mName = "" Then mName = "<SPECIFICATION>"
            mName = mName.ToString.ToUpper.Replace("+", "||").Replace("'%*S'", "'%*s'").
            Replace("<SPECIFICATION>", Dgl1.Item(Col1Value, rowSpecification).Value).
                          Replace("<MANUALCODE>", Dgl1.Item(Col1Value, rowItemCode).Value).
                          Replace("<ITEMGROUP>", Dgl1.Item(Col1Value, rowItemGroup).Value).
                          Replace("<ITEMCATEGORY>", Dgl1.Item(Col1Value, rowItemCategory).Value).
                          Replace("<ITEMTYPE>", Dgl1.Item(Col1Value, rowItemType).Value).
                          Replace("<BASEITEM>", Dgl1.Item(Col1Value, rowBaseItem).Value).
                          Replace("<DIMENSION1>", Dgl1.Item(Col1Value, rowDimension1).Value).
                          Replace("<DIMENSION2>", Dgl1.Item(Col1Value, rowDimension2).Value).
                          Replace("<DIMENSION3>", Dgl1.Item(Col1Value, rowDimension3).Value).
                          Replace("<DIMENSION4>", Dgl1.Item(Col1Value, rowDimension4).Value).
                          Replace("<SIZE>", Dgl1.Item(Col1Value, rowSize).Value).
                          Replace("<HSN>", Dgl1.Item(Col1Value, rowHSN).Value)
            mName = "SELECT " & "'" & mName & "'"
            mName = AgL.GetBackendBasedQuery(mName)
            mName = AgL.Dman_Execute(mName, AgL.GCn).ExecuteScalar
            'Dgl1(Col1Value, rowItemName).Value = Dgl1(Col1Value, rowSpecification).Value + Space(10) + "[" + Dgl1(Col1Value, rowItemGroup).Value + " | " + Dgl1(Col1Value, rowItemCategory).Value + "]"
            Dgl1(Col1Value, rowItemName).Value = mName
        ElseIf mItemVTypes = ItemV_Type.SIZE Then
            mName = ""
            If Dgl1.Rows(rowLength).Visible Then
                If Dgl1.Item(Col1Value, rowWidth).Value <> "" Then
                    If mName <> "" Then mName += " X "
                    If AgL.XNull(Dgl1.Item(Col1Value, rowSizeUnit).Value).ToString.Contains("Meter") Then
                        mName += (Val(Dgl1.Item(Col1Value, rowWidth).Value) * 100).ToString
                    Else
                        mName += Dgl1.Item(Col1Value, rowWidth).Value
                    End If
                End If
                If Dgl1.Item(Col1Value, rowLength).Value <> "" Then
                    If mName <> "" Then mName += " X "
                    If AgL.XNull(Dgl1.Item(Col1Value, rowSizeUnit).Value).ToString.Contains("Meter") Then
                        mName += (Val(Dgl1.Item(Col1Value, rowLength).Value) * 100).ToString
                    Else
                        mName += Dgl1.Item(Col1Value, rowLength).Value
                    End If
                End If
                If Dgl1.Item(Col1Value, rowThickness).Value <> "" Then
                    If mName <> "" Then mName += " X "
                    If AgL.XNull(Dgl1.Item(Col1Value, rowSizeUnit).Value).ToString.Contains("Meter") Then
                        mName += (Val(Dgl1.Item(Col1Value, rowThickness).Value) * 100).ToString
                    Else
                        mName += Dgl1.Item(Col1Value, rowThickness).Value
                    End If
                End If
                If Dgl1.Item(Col1Value, rowShapeShortName).Value <> "" Then
                    If mName <> "" Then mName += " "
                    mName += Dgl1.Item(Col1Value, rowShapeShortName).Value
                End If
                Dgl1(Col1Value, rowItemName).Value = mName
                Dgl1(Col1Value, rowSpecification).Value = mName
                If Dgl1(Col1Value, rowSizeUnit).Value <> "" Then
                    Dgl1(Col1Value, rowUnit).Value = "Sq." + Dgl1(Col1Value, rowSizeUnit).Value
                End If
            Else
                If Dgl1.Item(Col1Value, rowSpecification).Value <> "" Then
                    If mName <> "" Then mName += " - "
                    mName += Dgl1.Item(Col1Value, rowSpecification).Value
                End If
                If Dgl1.Item(Col1Value, rowItemCategory).Value <> "" Then
                    If mName <> "" Then mName += " - "
                    mName += Dgl1.Item(Col1Value, rowItemCategory).Value
                End If
                Dgl1(Col1Value, rowItemName).Value = mName
            End If
        Else
            Dgl1(Col1Value, rowItemName).Value = Dgl1(Col1Value, rowSpecification).Value

            'Patch 28/March/2019
            If Dgl1(Col1Value, rowBaseItem).Value <> "" Then
                Dgl1(Col1Value, rowItemName).Value = Dgl1(Col1Value, rowSpecification).Value + " - " +
                                    Dgl1(Col1Value, rowBaseItem).Value
            End If
        End If
    End Sub

    Public Sub FillRateTypeForItemGroup(ItemGroup As String)
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mQry As String

        If mItemVTypes <> ItemV_Type.Item And mItemVTypes <> ItemV_Type.ItemGroup Then Exit Sub

        mQry = " Select  H.Code, H.Description, IGRT.Margin, H.CalculateOnRateType, RT.Description as CalculateOnRateTypeName
                            from RateType H 
                            Left Join RateType Rt On H.CalculateOnRateType = Rt.Code
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
                    DGLRateType.Item(Col1CalculateOnRateType, I).Tag = AgL.XNull(.Rows(I)("CalculateOnRateType"))
                    DGLRateType.Item(Col1CalculateOnRateType, I).Value = AgL.XNull(.Rows(I)("CalculateOnRateTypeName"))
                    DGLRateType.Item(Col1Margin, I).Value = Format(AgL.VNull(.Rows(I)("Margin")), "0.00")
                Next I
                DGLRateType.Visible = True
            Else
                DGLRateType.Visible = False
            End If
        End With

    End Sub
    Public Sub Validate_ItemCategory()
        Dim mQry As String
        Dim DtTemp As DataTable
        mQry = "SELECT IC.Code, IC.Description, IC.ItemType, IT.Name as ItemTypeName, IC.SalesTaxGroup, 
                IC.Unit, IC.StockUnit, IC.DealUnit, IC.Hsn 
                FROM ItemCategory IC 
                Left Join ItemType IT On IC.ItemType = IT.Code 
                Where IC.Code = '" & Dgl1(Col1Value, rowItemCategory).Tag & "'  "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            Dgl1(Col1Value, rowItemType).Value = AgL.XNull(DtTemp.Rows(0)("ItemTypeName"))
            Dgl1(Col1Value, rowItemType).Tag = AgL.XNull(DtTemp.Rows(0)("ItemType"))
            Dgl1(Col1Value, rowUnit).Tag = AgL.XNull(DtTemp.Rows(0)("Unit"))
            Dgl1(Col1Value, rowUnit).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
            Dgl1(Col1Value, rowStockUnit).Tag = AgL.XNull(DtTemp.Rows(0)("StockUnit"))
            Dgl1(Col1Value, rowStockUnit).Value = AgL.XNull(DtTemp.Rows(0)("StockUnit"))
            Dgl1(Col1Value, rowDealUnit).Tag = AgL.XNull(DtTemp.Rows(0)("DealUnit"))
            Dgl1(Col1Value, rowDealUnit).Value = AgL.XNull(DtTemp.Rows(0)("DealUnit"))
            Dgl1(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(DtTemp.Rows(0)("SalesTaxGroup"))
            Dgl1(Col1Value, rowSalesTaxGroup).Tag = AgL.XNull(DtTemp.Rows(0)("SalesTaxGroup"))
            Dgl1(Col1Value, rowHSN).Value = AgL.XNull(DtTemp.Rows(0)("Hsn"))
        Else
            Dgl1(Col1Value, rowItemType).Value = ""
            Dgl1(Col1Value, rowItemType).Tag = ""
            Dgl1(Col1Value, rowUnit).Tag = ""
            Dgl1(Col1Value, rowStockUnit).Tag = ""
            Dgl1(Col1Value, rowDealUnit).Tag = ""
            Dgl1(Col1Value, rowSalesTaxGroup).Tag = ""
            Dgl1(Col1Value, rowHSN).Value = ""
        End If

        FGetItemTypeSetting()
        Dgl1(Col1Head, rowItemGroup).Tag = Nothing
        SetProductName()

        If Dgl1.Rows(rowItemGroup).Visible Then
            Dgl1.CurrentCell = Dgl1(Col1Value, rowItemGroup)
            Dgl1.Focus()
        End If

    End Sub
    Public Sub Validate_ItemGroup()
        Dgl1(Col1Head, rowSpecification).Tag = Nothing

        SetProductName()
        Dgl1(Col1Value, rowMarginPer).Value = AgL.Dman_Execute("Select IfNull(Default_MarginPer,0) From ItemGroup Where Code ='" & Dgl1(Col1Value, rowItemGroup).Tag & "'", AgL.GCn).ExecuteScalar
        If Dgl1(Col1Value, rowItemGroup).Tag <> "" Then
            If Topctrl1.Mode = "Edit" Then
                If Dgl1(Col1LastValue, rowItemGroup).Tag <> Dgl1(Col1Value, rowItemGroup).Tag Then
                    FillRateTypeForItemGroup(Dgl1(Col1Value, rowItemGroup).Tag)
                End If
            Else
                FillRateTypeForItemGroup(Dgl1(Col1Value, rowItemGroup).Tag)
            End If
        End If
        Dgl1(Col1Value, rowSaleRate).Tag = 0
        Calculation()

        If Topctrl1.Mode = "Add" Then
            If AgL.XNull(AgL.Dman_Execute("Select BarcodeType From Item IG With (NoLock) Where IG.Code = '" & Dgl1.Item(Col1Value, rowItemGroup).Tag & "' And BarcodePattern = '" & BarcodePattern.Auto & "' ", AgL.GCn).executescalar()) = BarcodeType.Fixed Then
                If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" And (AgL.StrCmp(AgL.PubDBName, "Sadhvi") Or AgL.StrCmp(AgL.PubDBName, "Sadhvi2")) Then
                    Dgl1.Item(Col1Value, rowBarcode).Value = AgL.Dman_Execute("Select IfNull(Max(CAST(Description as BIGINT)),0) + 1 From BarCode  With (NoLock) WHERE Item NOT IN ('Lr','LrBale') ", AgL.GCn).ExecuteScalar()
                Else
                    Dgl1.Item(Col1Value, rowBarcode).Value = AgL.Dman_Execute("Select IfNull(Max(CAST(Description as BIGINT)),0) + 1 From BarCode  With (NoLock)", AgL.GCn).ExecuteScalar()
                End If
            End If
        End If

    End Sub

    Public Sub Validate_Shape()
        Dim dtTemp As DataTable
        mQry = "Select * From Shape Where Code = '" & Dgl1.Item(Col1Value, rowShape).Tag & "'"
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowShapeAreaFormula).Value = AgL.XNull(dtTemp.Rows(0)("AreaFormula"))
            Dgl1.Item(Col1Value, rowShapePerimeterFormula).Value = AgL.XNull(dtTemp.Rows(0)("PerimeterFormula"))
            Dgl1.Item(Col1Value, rowShapeShortName).Value = AgL.XNull(dtTemp.Rows(0)("ShortName"))
        End If
        SetProductName()
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
            MnuImportDesignFromDos.Visible = False
            MnuImportRateListFromDos.Visible = False
            MnuImportRateListFromExcel.Visible = False
        End If
        'MnuBulkEdit.Visible = False

        If AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Product", "") = "Spare" Then
            MnuImportFromExcel.Visible = True
            MnuBulkEdit.Visible = False
            MnuBulkRateEdit.Visible = False
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtManualCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If Dgl1(Col1Value, rowItemName).Value = "" Then Dgl1(Col1Value, rowItemName).Value = Dgl1(Col1Value, rowItemCode).Value
    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Private Sub FrmFinishedItem_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Head, I).Tag = Nothing
        Next
    End Sub

    Private Sub FrmItemMaster_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        Dgl1(Col1Value, rowItemName).ReadOnly = True

        ChkIsSystemDefine.Enabled = False
        If DGLRateType.Rows.Count <= 1 Then DGLRateType.Visible = False
        PnlItemSubgroup.Visible = False
        DGLItemSubgroup.Visible = False
        If (mItemVTypes = "D1" And AgL.StrCmp(AgL.PubDBName, "Aeroclub")) Then
            DGLItemSubgroup.Visible = True
            PnlItemSubgroup.Visible = True
        End If
        DGLItemSubgroup.Width = 500
        PnlItemSubgroup.Width = 500
        PnlItemSubgroup.Height = 100

    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(ClsMain.Temp_NCat.Item, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag
        If AgL.StrCmp(AgL.PubDBName, "Aeroclub") Then
            IniGrid()
        End If

        If AgL.PubServerName = "" Then
            Dgl1(Col1Value, rowItemCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        Else
            Dgl1(Col1Value, rowItemCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        End If




        If Dgl1(Col1LastValue, rowItemType).Value = "" Then
            If mItemVTypes = ItemV_Type.Item Then
                Dgl1(Col1Value, rowItemType).Tag = ItemTypeCode.TradingProduct
                Dgl1(Col1Value, rowItemType).Value = ItemTypeCode.TradingProduct
            End If
        Else
            Dgl1(Col1Value, rowItemType).Value = Dgl1(Col1LastValue, rowItemType).Value
            Dgl1(Col1Value, rowItemType).Tag = Dgl1(Col1LastValue, rowItemType).Tag
        End If

        'ApplyItemTypeSetting(Dgl1(Col1Value, rowItemType).Tag)
        ApplyUISetting()


        If Dgl1.Rows(rowUnit).Visible Then
            Dgl1(Col1Value, rowUnit).Tag = Dgl1(Col1LastValue, rowUnit).Tag
            Dgl1(Col1Value, rowUnit).Value = Dgl1(Col1LastValue, rowUnit).Value
        End If
        If Dgl1.Rows(rowItemCategory).Visible Then
            Dgl1(Col1Value, rowItemCategory).Value = Dgl1(Col1LastValue, rowItemCategory).Value
            Dgl1(Col1Value, rowItemCategory).Tag = Dgl1(Col1LastValue, rowItemCategory).Tag
        End If
        If Dgl1.Rows(rowItemGroup).Visible Then
            Dgl1(Col1Value, rowItemGroup).Tag = Dgl1(Col1LastValue, rowItemGroup).Tag
            Dgl1(Col1Value, rowItemGroup).Value = Dgl1(Col1LastValue, rowItemGroup).Value
        End If

        Dgl1(Col1Value, rowSalesTaxGroup).Tag = Dgl1(Col1LastValue, rowSalesTaxGroup).Tag
        Dgl1(Col1Value, rowSalesTaxGroup).Value = Dgl1(Col1LastValue, rowSalesTaxGroup).Value

        If Dgl1(Col1LastValue, rowItemType).Tag = "" Then
            Dgl1(Col1LastValue, rowItemType).Tag = ItemTypeCode.TradingProduct
        End If




        Validate_ItemCategory()
        Validate_ItemGroup()


        Dgl1.Item(Col1Value, rowMaintainStockYn).Value = "YES"
        'DGLRateType.Visible = False



        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()
        SetAttachmentCaption()

        'If mItemVTypes = ItemV_Type.SIZE Then
        '    If Dgl1.Rows(rowShape).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowShape)
        'Else
        '    If Dgl1.Rows(rowSpecification).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowSpecification)
        '    If Dgl1.Rows(rowItemGroup).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowItemGroup)
        '    If Dgl1.Rows(rowItemCategory).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowItemCategory)
        '    If Dgl1.Rows(rowItemType).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowItemType)
        'End If
        Dgl1.CurrentCell = Dgl1.FirstDisplayedCell
        Dgl1.Focus()
    End Sub

    Private Sub FrmItemMaster_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        'AgCustomGrid1.Ini_Grid(mSearchCode)
        '    AgCustomGrid1.SplitGrid = False

        DGLRateType.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGLRateType, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DGLRateType, Col1RateType, 320, 0, Col1RateType, True, True, False)
            .AddAgTextColumn(DGLRateType, Col1CalculateOnRateType, 320, 0, Col1CalculateOnRateType, False, True, False)
            .AddAgNumberColumn(DGLRateType, Col1Margin, 100, 2, 2, False, Col1Margin, True, True, True)
            .AddAgNumberColumn(DGLRateType, Col1Rate, 150, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(DGLRateType, Col1Discount, 120, 8, 2, False, Col1Discount, True, False, True)
            .AddAgNumberColumn(DGLRateType, Col1Addition, 120, 8, 2, False, Col1Addition, True, False, True)
        End With
        AgL.AddAgDataGrid(DGLRateType, PnlRateType)
        DGLRateType.EnableHeadersVisualStyles = False
        DGLRateType.AgSkipReadOnlyColumns = True
        DGLRateType.RowHeadersVisible = False
        Dgl1.AllowUserToAddRows = False
        DGLRateType.Visible = False
        DGLRateType.BackgroundColor = Me.BackColor
        AgL.GridDesign(DGLRateType)
        DGLRateType.Name = "DGLRateType"
        DGLRateType.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom



        DGLUnitConversion.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGLUnitConversion, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DGLUnitConversion, Col1FromUnit, 180, 0, Col1FromUnit, True, False, False)
            .AddAgNumberColumn(DGLUnitConversion, Col1Multiplier, 80, 3, 2, False, Col1Multiplier, True, False, True)
        End With
        AgL.AddAgDataGrid(DGLUnitConversion, PnlUnitConversion)
        DGLUnitConversion.EnableHeadersVisualStyles = False
        DGLUnitConversion.AgSkipReadOnlyColumns = True
        DGLUnitConversion.RowHeadersVisible = False
        Dgl1.AllowUserToAddRows = False
        DGLUnitConversion.Visible = False
        DGLUnitConversion.BackgroundColor = Me.BackColor
        AgL.GridDesign(DGLUnitConversion)
        DGLUnitConversion.Name = "DglUnitConversion"
        DGLUnitConversion.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom



        DGLItemSubgroup.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGLItemSubgroup, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DGLItemSubgroup, Col1SubCode, 280, 0, Col1SubCode, True, False, False)
            .AddAgTextColumn(DGLItemSubgroup, Col1Description, 180, 0, Col1Description, True, False, False)
        End With
        AgL.AddAgDataGrid(DGLItemSubgroup, PnlItemSubgroup)
        DGLItemSubgroup.EnableHeadersVisualStyles = False
        DGLItemSubgroup.AgSkipReadOnlyColumns = True
        DGLItemSubgroup.RowHeadersVisible = False
        Dgl1.AllowUserToAddRows = False
        PnlItemSubgroup.Visible = False
        DGLItemSubgroup.Visible = False
        If (mItemVTypes = "D1" And AgL.StrCmp(AgL.PubDBName, "Aeroclub")) Then
            DGLItemSubgroup.Visible = True
            PnlItemSubgroup.Visible = True
        End If

        DGLItemSubgroup.BackgroundColor = Me.BackColor
        AgL.GridDesign(DGLItemSubgroup)
        DGLItemSubgroup.Name = "DGLItemSubGroup"
        DGLItemSubgroup.Width = 500
        PnlItemSubgroup.Width = 500
        PnlItemSubgroup.Height = 100



        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 280, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 650, 255, Col1Value, True, False)
            .AddAgTextColumn(Dgl1, Col1LastValue, 170, 255, Col1Value, False, False)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 170, 255, Col1HeadOriginal, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        AgL.GridDesign(Dgl1)
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Pnl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom
        Dgl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        Dgl1.Rows.Add(49)

        Dgl1.Item(Col1Head, rowItemCategory).Value = hcItemCategory
        Dgl1.Item(Col1Head, rowItemGroup).Value = hcItemGroup
        Dgl1.Item(Col1Head, rowBaseItem).Value = hcBaseItem
        Dgl1.Item(Col1Head, rowDimension1).Value = hcDimension1
        Dgl1.Item(Col1Head, rowDimension2).Value = hcDimension2
        Dgl1.Item(Col1Head, rowDimension3).Value = hcDimension3
        Dgl1.Item(Col1Head, rowDimension4).Value = hcDimension4
        Dgl1.Item(Col1Head, rowSize).Value = hcSize
        Dgl1.Item(Col1Head, rowShape).Value = hcShape
        Dgl1.Item(Col1Head, rowShapeAreaFormula).Value = hcShapeAreaFormula
        Dgl1.Item(Col1Head, rowShapePerimeterFormula).Value = hcShapePerimeterFormula
        Dgl1.Item(Col1Head, rowShapeShortName).Value = hcShapeShortName
        Dgl1.Item(Col1Head, rowSizeUnit).Value = hcSizeUnit
        Dgl1.Item(Col1Head, rowLength).Value = hcLength
        Dgl1.Item(Col1Head, rowWidth).Value = hcWidth
        Dgl1.Item(Col1Head, rowThickness).Value = hcThickness
        Dgl1.Item(Col1Head, rowArea).Value = hcArea
        Dgl1.Item(Col1Head, rowPerimeter).Value = hcPerimeter
        Dgl1.Item(Col1Head, rowItemCode).Value = hcItemCode
        Dgl1.Item(Col1Head, rowSpecification).Value = hcSpecification
        Dgl1.Item(Col1Head, rowItemName).Value = hcItemName
        Dgl1.Item(Col1Head, rowUnit).Value = hcUnit
        Dgl1.Item(Col1Head, rowStockUnit).Value = hcStockUnit
        Dgl1.Item(Col1Head, rowDealQty).Value = hcDealQty
        Dgl1.Item(Col1Head, rowDealUnit).Value = hcDealUnit
        Dgl1.Item(Col1Head, rowSalesTaxGroup).Value = hcSalesTaxGroup
        Dgl1.Item(Col1Head, rowPurchaseRate).Value = hcPurchaseRate
        Dgl1.Item(Col1Head, rowMarginPer).Value = hcMarginPer
        Dgl1.Item(Col1Head, rowSaleRate).Value = hcSaleRate
        Dgl1.Item(Col1Head, rowHSN).Value = hcHSN
        Dgl1.Item(Col1Head, rowItemType).Value = hcItemType
        Dgl1.Item(Col1Head, rowDefaultDiscountPerSale).Value = hcDefaultDiscountPerSale
        Dgl1.Item(Col1Head, rowDefaultAdditionPerSale).Value = hcDefaultAdditionPerSale
        Dgl1.Item(Col1Head, rowDefaultDiscountPerPurchase).Value = hcDefaultDiscountPerPurchase
        Dgl1.Item(Col1Head, rowDefaultSupplier).Value = hcDefaultSupplier
        Dgl1.Item(Col1Head, rowBarcode).Value = hcBarcode
        Dgl1.Item(Col1Head, rowMRP).Value = hcMRP
        Dgl1.Item(Col1Head, rowShowItemInOtherDivision).Value = hcShowItemInOtherDivisions
        Dgl1.Item(Col1Head, rowShowItemInOtherSites).Value = hcShowItemInOtherSites
        Dgl1.Item(Col1Head, rowMaintainStockYn).Value = hcMaintainStockYn
        Dgl1.Item(Col1Head, rowSite).Value = hcSite
        Dgl1.Item(Col1Head, rowParent).Value = hcParent
        Dgl1.Item(Col1Head, rowTopParent).Value = hcTopParent
        Dgl1.Item(Col1Head, rowPurchaseAc).Value = hcPurchaseAc
        Dgl1.Item(Col1Head, rowSalesAc).Value = hcSalesAc
        Dgl1.Item(Col1Head, rowRemark).Value = hcRemark
        Dgl1.Item(Col1Head, rowRemark1).Value = hcRemark1
        Dgl1.Item(Col1Head, rowRemark2).Value = hcRemark2
        Dgl1.Item(Col1Head, rowRemark3).Value = hcRemark3
        Dgl1.Name = "Dgl1"
        Dgl1.Tag = "VerticalGrid"

        For I = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1(Col1HeadOriginal, I).Value) = "" Then
                Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
            End If
        Next

        AgL.FSetDimensionCaptionForVerticalGrid(Dgl1, AgL)

        'If Dgl1.Item(Col1Value, rowItemType).Value <> "" Then
        '    ApplyItemTypeSetting(Dgl1.Item(Col1Value, rowItemType).Tag)
        'Else
        '    ApplyItemTypeSetting(ItemTypeCode.TradingProduct)
        'End If
        ApplyUISetting()
    End Sub

    Private Sub BtnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBrowse.Click, BtnPhotoClear.Click
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Select Case sender.Name
            Case BtnBrowse.Name
                AgL.GetPicture(PicPhoto, Photo_Byte)
                If Photo_Byte.Length > 20480 Then Photo_Byte = Nothing : PicPhoto.Image = Nothing : MsgBox("Image Size Should Not be Greater Than 20 KB ")

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

    Private Sub FrmItemMaster_BaseEvent_Save_PostTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
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

        Dgl1(Col1Head, rowItemGroup).Tag = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FOpenItemGroupMaster()
        Dim DrTemp As DataRow() = Nothing
        Dim bStrCode$ = ""
        bStrCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Group Master", "")
        FCreateHelpItemGroup()
        DrTemp = CType(Dgl1(Col1Head, rowItemGroup).Tag, DataSet).Tables(0).Select("Code = '" & bStrCode & "'")
        Dgl1(Col1Value, rowItemGroup).Tag = bStrCode
        Dgl1(Col1Value, rowItemGroup).Value = AgL.XNull(AgL.Dman_Execute("Select Description From ItemGroup Where Code = '" & bStrCode & "'", AgL.GCn).ExecuteScalar)
        Dgl1.CurrentCell = Dgl1(Col1Value, rowItemGroup)
        Dgl1.Focus()
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
    '    Dim FrmObj As FrmItemMasterUnitConversion
    '    Try
    '        FrmObj = New FrmItemMasterUnitConversion
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
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.ColSNo, I).Value = BtnUnitConversion.Tag.Dgl1.Rows.Count - 1
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1FromUnit, I).Value = AgL.XNull(.Rows(I)("FromUnit"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1FromQty, I).Value = AgL.VNull(.Rows(I)("FromQty"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1ToUnit, I).Value = AgL.XNull(.Rows(I)("ToUnit"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1ToQty, I).Value = AgL.VNull(.Rows(I)("ToQty"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1Multiplier, I).Value = AgL.VNull(.Rows(I)("Multiplier"))
    '                    BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1Equal, I).Value = "="

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
    '    Dim FrmObj As FrmItemMasterBOMDetail
    '    Try
    '        FrmObj = New FrmItemMasterBOMDetail
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
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.ColSNo, I).Value = BtnBOMDetail.Tag.Dgl1.Rows.Count - 1
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Process, I).Value = AgL.XNull(.Rows(I)("ProcessDesc"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
    '                    BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1WastagePer, I).Value = AgL.VNull(.Rows(I)("WastagePer"))
    '                    If AgL.VNull(.Rows(I)("Cnt")) > 0 Then
    '                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1BtnBOMDetail, I).Style.ForeColor = Color.Red
    '                    End If
    '                    BtnBOMDetail.Tag.EntryMode = Topctrl1.Mode
    '                Next I
    '            End If
    '        End With

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub FrmItemMasterNew_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim I As Integer
        Photo_Byte = Nothing
        PicPhoto.Image = Nothing
        BtnUnitConversion.Tag = Nothing
        BtnBOMDetail.Tag = Nothing

        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Value, I).Value = ""
            Dgl1(Col1Value, I).Tag = ""
        Next

    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()

        If Not AgL.StrCmp(TxtDivision.AgSelectedValue, AgL.PubDivCode) And TxtDivision.Text <> "" Then

            If MsgBox("Different Division Record. Do you want to modify it.", MsgBoxStyle.YesNo, "Validation") = vbNo Then
                Topctrl1.FButtonClick(14, True)
                Exit Sub
            Else
                TxtDivision.ReadOnly = False
            End If
        End If

        If ClsMain.IsEntryLockedWithLockText("Item", "Code", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        If Dgl1.Rows(rowSpecification).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowSpecification)
        If Dgl1.Rows(rowItemGroup).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowItemGroup)
        If Dgl1.Rows(rowItemCategory).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowItemCategory)
        If Dgl1.Rows(rowItemType).Visible = True Then Dgl1.CurrentCell = Dgl1(Col1Value, rowItemType)
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
        If Passed = False Then Exit Sub
        Passed = Not FGetRelationalData()

        If TxtDivision.Text <> "" Then
            If Not AgL.StrCmp(TxtDivision.AgSelectedValue, AgL.PubDivCode) Then
                MsgBox("Different Division Record. Can't Modify!", MsgBoxStyle.OkOnly, "Validation") : Passed = False : Exit Sub
            End If
        End If

        If ClsMain.IsEntryLockedWithLockText("Item", "Code", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            If mItemVTypes = ItemV_Type.Item Then
                If IsRelationalDataExist("SaleInvoice", "SaleInvoiceDetail", "Item") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("PurchInvoice", "PurchInvoiceDetail", "Item") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("StockHead", "StockHeadDetail", "Item") = True Then FGetRelationalData = True : Exit Function

                If IsRelationalDataExist("SaleInvoice", "SaleInvoiceDetailSku", "Item") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("PurchInvoice", "PurchInvoiceDetailSku", "Item") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("StockHead", "StockHeadDetailSku", "Item") = True Then FGetRelationalData = True : Exit Function

                If IsRelationalDataExistWithItem("BaseItem") = True Then FGetRelationalData = True : Exit Function
            End If

            If mItemVTypes = ItemV_Type.Dimension1 Then
                If IsRelationalDataExist("SaleInvoice", "SaleInvoiceDetailSku", "Dimension1") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("PurchInvoice", "PurchInvoiceDetailSku", "Dimension1") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("StockHead", "StockHeadDetailSku", "Dimension1") = True Then FGetRelationalData = True : Exit Function

                If IsRelationalDataExistWithItem("Dimension1") = True Then FGetRelationalData = True : Exit Function
            End If

            If mItemVTypes = ItemV_Type.Dimension2 Then
                If IsRelationalDataExist("SaleInvoice", "SaleInvoiceDetailSku", "Dimension2") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("PurchInvoice", "PurchInvoiceDetailSku", "Dimension2") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("StockHead", "StockHeadDetailSku", "Dimension2") = True Then FGetRelationalData = True : Exit Function

                If IsRelationalDataExistWithItem("Dimension2") = True Then FGetRelationalData = True : Exit Function
            End If

            If mItemVTypes = ItemV_Type.Dimension3 Then
                If IsRelationalDataExist("SaleInvoice", "SaleInvoiceDetailSku", "Dimension3") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("PurchInvoice", "PurchInvoiceDetailSku", "Dimension3") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("StockHead", "StockHeadDetailSku", "Dimension3") = True Then FGetRelationalData = True : Exit Function

                If IsRelationalDataExistWithItem("Dimension3") = True Then FGetRelationalData = True : Exit Function
            End If

            If mItemVTypes = ItemV_Type.Dimension4 Then
                If IsRelationalDataExist("SaleInvoice", "SaleInvoiceDetailSku", "Dimension4") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("PurchInvoice", "PurchInvoiceDetailSku", "Dimension4") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("StockHead", "StockHeadDetailSku", "Dimension4") = True Then FGetRelationalData = True : Exit Function

                If IsRelationalDataExistWithItem("Dimension4") = True Then FGetRelationalData = True : Exit Function
            End If

            If mItemVTypes = ItemV_Type.SIZE Then
                If IsRelationalDataExist("SaleInvoice", "SaleInvoiceDetailSku", "Size") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("PurchInvoice", "PurchInvoiceDetailSku", "Size") = True Then FGetRelationalData = True : Exit Function
                If IsRelationalDataExist("StockHead", "StockHeadDetailSku", "Size") = True Then FGetRelationalData = True : Exit Function

                If IsRelationalDataExistWithItem("Size") = True Then FGetRelationalData = True : Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
    Private Function IsRelationalDataExist(HeaderTableName As String, LineTableName As String,
                                      FieldName As String) As Boolean
        Dim DtRelationalData As DataTable
        mQry = " Select Vt.Description || '-' || H.ManualRefNo As DocNo
                        From " & LineTableName & " L
                        LEFT JOIN " & HeaderTableName & " H On L.DocId = H.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where L." & FieldName & " = '" & mSearchCode & "'"
        DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtRelationalData.Rows.Count > 0 Then
            MsgBox("Data Exists For " & Dgl1(Col1Value, rowSpecification).Value & " In " + DtRelationalData.Rows(0)("DocNo") + ".Can't Delete Entry", MsgBoxStyle.Information)
            IsRelationalDataExist = True
            Exit Function
        Else
            IsRelationalDataExist = False
        End If
    End Function
    Private Function IsRelationalDataExistWithItem(FieldName As String) As Boolean
        Dim DtRelationalData As DataTable

        mQry = " Select I.Specification As LinkedItem, I.V_Type
                        From Item I
                        LEFT JOIN Item I1 ON I.BaseItem = I1.Code
                        Where I." & FieldName & " = '" & mSearchCode & "'"
        DtRelationalData = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtRelationalData.Rows.Count > 0 Then
            MsgBox("Data Exists For " & Dgl1(Col1Value, rowSpecification).Value & " In " + DtRelationalData.Rows(0)("LinkedItem") + ".Can't Delete Entry", MsgBoxStyle.Information)
            IsRelationalDataExistWithItem = True
            Exit Function
        Else
            IsRelationalDataExistWithItem = False
        End If
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

    Private Sub FrmItemMaster_Cloth_BaseEvent_ApproveDeletion_PreTrans(SearchCode As String) Handles Me.BaseEvent_ApproveDeletion_PreTrans

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



    Public Sub FImportFromExcel(bImportFor As ImportFor, Optional UpdateIfExists As Boolean = False)
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
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Item Group") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Item Category") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Specification") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Unit") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Purchase Rate") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sale Rate") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sales Tax Group") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "HSN Code") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Remark1") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
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
                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")).ToString().Trim() = "GST @12%" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")) = "GST 12%"
                End If

                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString().Trim() = "P" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Pcs"
                ElseIf DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString().Trim() = "M" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Meter"
                ElseIf DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString().Trim() = "K" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Kg"
                End If


                If Me.Text = "Design Master" Then
                    If Not DtTemp.Columns.Contains("BaseItem") Then
                        DtTemp.Columns.Add("BaseItem", GetType(String))
                    End If
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Base Item")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification")).ToString.Trim +
                    " - " + DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group")).ToString.Trim + " - " +
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category")).ToString.Trim

                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Design")).ToString.Trim + "-" + DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification")).ToString.Trim +
                    " - " + DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group")).ToString.Trim + " - " +
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category")).ToString.Trim

                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Display Name")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))

                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Design")).ToString.Trim
                Else
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification")).ToString.Trim +
                    " - " + DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group")).ToString.Trim + " - " +
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category")).ToString.Trim

                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Display Name")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))
                End If
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
                    If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxItem where Description = '" & AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim & "' ", AgL.GCn).ExecuteScalar = 0 Then
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
            If AgL.PubServerName = "" Then AgL.Dman_ExecuteNonQry("PRAGMA SYNCHRONOUS=OFF", AgL.GCn)

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            Dim bLastItemCategoryCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemCategory = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Category"), GetFieldAliasName(bImportFor, "Sales Tax Group"))
            For I = 0 To DtItemCategory.Rows.Count - 1
                If AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))) <> "" Then
                    Dim ItemCategoryTable As New StructItemCategory
                    Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemCategoryTable.Code = bItemCategoryCode
                    ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    ItemCategoryTable.ItemType = "TP"
                    ItemCategoryTable.SalesTaxPostingGroup = AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim
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

            Dim bLastItemGroupCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemGroup = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Group"), GetFieldAliasName(bImportFor, "Item Category"), GetFieldAliasName(bImportFor, "Sales Tax Group"))
            For I = 0 To DtItemGroup.Rows.Count - 1
                If AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))) <> "" Then
                    Dim ItemGroupTable As New StructItemGroup
                    Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemGroupTable.Code = bItemGroupCode
                    ItemGroupTable.Description = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))).ToString.Trim
                    ItemGroupTable.ItemCategory = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    ItemGroupTable.ItemType = "TP"
                    ItemGroupTable.SalesTaxPostingGroup = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim
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
                    If I Mod 1000 = 0 Then
                        MsgBox(I.ToString)
                    End If

                    Dim ItemTable As New StructItem
                    Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemTable.Code = bItemCode
                    ItemTable.ManualCode = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Code"))).ToString.Trim
                    ItemTable.Description = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))).ToString.Trim
                    ItemTable.DisplayName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Display Name"))).ToString.Trim
                    ItemTable.Specification = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification"))).ToString.Trim
                    ItemTable.ItemGroupDesc = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))).ToString.Trim
                    ItemTable.ItemCategoryDesc = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    ItemTable.ItemType = "TP"
                    ItemTable.V_Type = mItemVTypes ' "ITEM"
                    ItemTable.Unit = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit"))).ToString.Trim
                    ItemTable.PurchaseRate = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Purchase Rate"))).ToString.Trim
                    ItemTable.Rate = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sale Rate"))).ToString.Trim
                    ItemTable.SalesTaxPostingGroup = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim
                    ItemTable.HSN = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "HSN Code"))).ToString.Trim
                    ItemTable.Remark = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Remark"))).ToString.Trim
                    ItemTable.Remark1 = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Remark1"))).ToString.Trim
                    ItemTable.EntryBy = AgL.PubUserName
                    ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    ItemTable.EntryType = "Add"
                    ItemTable.EntryStatus = LogStatus.LogOpen
                    ItemTable.Div_Code = AgL.PubDivCode
                    ItemTable.Status = "Active"
                    ItemTable.StockYN = 1
                    ItemTable.IsSystemDefine = 0


                    If DtTemp.Columns.Contains("Base Item") Or DtTemp.Columns.Contains("BaseItem") Then
                        ItemTable.BaseItemDesc = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Base Item"))).ToString.Trim
                    End If


                    ImportItemTable(ItemTable, UpdateIfExists)

                    mQry = "Update Item Set HSN=(SELECT Max(I.HSN) FROM Item I WHERE I.ItemCategory=Item.Code) Where Item.V_Type='IC'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
            If AgL.PubServerName = "" Then AgL.Dman_ExecuteNonQry("PRAGMA SYNCHRONOUS=ON", AgL.GCn)
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
                Case "Base Item"
                    bAliasName = "BaseItem"
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
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuImportRateListFromExcel.Click, MnuImportRateListFromDos.Click, MnuBulkEdit.Click, MnuImportDesignFromDos.Click, MnuBulkRateEdit.Click, MnuBarcodePrint.Click
        Select Case sender.name
            Case MnuImportFromExcel.Name
                FImportFromExcel(ImportFor.Excel)
            'FImportItem_SparePart()

            Case MnuImportFromDos.Name
                FImportFromExcel(ImportFor.Dos, True)

            Case MnuImportDesignFromDos.Name
                FImportDesignFromExcel(ImportFor.Dos)

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

            Case MnuBulkRateEdit.Name
                FOpenBulkRateEdit()

            Case MnuBarcodePrint.Name
                Dim FrmObj As FrmPrintBarcode
                FrmObj = New FrmPrintBarcode()
                FrmObj.DocId = mSearchCode
                FrmObj.PrintBarcodeFrom = Me.Name
                FrmObj.LblTitle.Text = Dgl1.Item(Col1Value, rowItemName).Value + " - " + Dgl1.Item(Col1Value, rowBarcode).Value
                FrmObj.StartPosition = FormStartPosition.CenterParent
                FrmObj.ShowDialog()
        End Select
    End Sub
    Private Sub FOpenBulkRateEdit()
        Dim StrSenderText As String = Me.Text
        GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
        GridReportFrm.Filter_IniGrid()

        Dim CRep As ClsItemMasterBulk = New ClsItemMasterBulk(GridReportFrm)
        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
        CRep.Ini_Grid()
        ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
        GridReportFrm.MdiParent = Me.MdiParent
        GridReportFrm.Show()
        'CRep.ProcItemMasterBulk()
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

            Dim bLastItemCategoryCode As String = AgL.GetMaxId("Item", "Code", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode, 4, True, True)

            Dim ItemCategoryElementList As XmlNodeList = doc.GetElementsByTagName("STOCKCATEGORY")
            For I = 0 To ItemCategoryElementList.Count - 1
                Dim ItemCategoryTable As New StructItemCategory
                Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemCategoryTable.Code = bItemCategoryCode
                ItemCategoryTable.Description = ItemCategoryElementList(I).Attributes("NAME").Value
                ItemCategoryTable.ItemType = "TP"
                ItemCategoryTable.SalesTaxPostingGroup = "GST 5%"
                ItemCategoryTable.Unit = "Pcs"
                ItemCategoryTable.EntryBy = AgL.PubUserName
                ItemCategoryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemCategoryTable.EntryType = "Add"
                ItemCategoryTable.EntryStatus = LogStatus.LogOpen
                ItemCategoryTable.Div_Code = AgL.PubDivCode
                ItemCategoryTable.Status = "Active"

                ImportItemCategoryTable(ItemCategoryTable)
            Next

            Dim bLastItemGroupCode As String = AgL.GetMaxId("Item", "Code", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim ItemGroupElementList As XmlNodeList = doc.GetElementsByTagName("STOCKGROUP")
            For I = 0 To ItemGroupElementList.Count - 1
                Dim ItemGroupTable As New StructItemGroup
                Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemGroupTable.Code = bItemGroupCode
                ItemGroupTable.Description = ItemGroupElementList(I).Attributes("NAME").Value
                ItemGroupTable.ItemType = "TP"
                ItemGroupTable.SalesTaxPostingGroup = "GST 5%"
                ItemGroupTable.Unit = "Pcs"
                ItemGroupTable.EntryBy = AgL.PubUserName
                ItemGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemGroupTable.EntryType = "Add"
                ItemGroupTable.EntryStatus = LogStatus.LogOpen
                ItemGroupTable.Div_Code = AgL.PubDivCode
                ItemGroupTable.Status = "Active"

                ImportItemGroupTable(ItemGroupTable)
            Next

            Dim bLastItemCode As String = AgL.GetMaxId("Item", "Code", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            'Dim bLastManualCode As String = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) FROM Item With (NoLock)  WHERE ABS(ManualCode)>0", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)
            Dim bLastManualCode As String = "0"

            Dim ItemElementList As XmlNodeList = doc.GetElementsByTagName("STOCKITEM")
            For I = 0 To ItemElementList.Count - 1
                If I = 470 Then MsgBox(I)
                Dim ItemTable As New StructItem
                Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
                Dim bManualCode = bLastManualCode + I

                ItemTable.Code = bItemCode
                ItemTable.ManualCode = bManualCode
                ItemTable.V_Type = ItemV_Type.Item
                ItemTable.DisplayName = ItemElementList(I).Attributes("NAME").Value
                ItemTable.Specification = ItemElementList(I).Attributes("NAME").Value

                If ItemElementList(I).Attributes("NAME").Value = "ARISTOCRAT" Then
                    MsgBox(ItemElementList(I).Attributes("NAME").Value)
                End If

                If ItemElementList(I).SelectSingleNode("PARENT") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("PARENT").ChildNodes.Count > 0 Then
                        ItemTable.ItemGroupDesc = ItemElementList(I).SelectSingleNode("PARENT").ChildNodes(0).Value
                    End If
                End If

                If ItemElementList(I).SelectSingleNode("CATEGORY") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("CATEGORY").ChildNodes.Count > 0 Then
                        ItemTable.ItemCategoryDesc = ItemElementList(I).SelectSingleNode("CATEGORY").ChildNodes(0).Value
                    End If
                End If

                ItemTable.Description = ItemElementList(I).Attributes("NAME").Value + Space(10) + "[" + ItemTable.ItemGroupDesc + " | " + ItemTable.ItemCategoryDesc + "]"


                ItemTable.ItemType = "TP"

                If ItemElementList(I).SelectSingleNode("BASEUNITS") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("BASEUNITS").ChildNodes.Count > 0 Then
                        ItemTable.Unit = ItemElementList(I).SelectSingleNode("BASEUNITS").ChildNodes(0).Value.Replace(".", "")
                        If ItemTable.Unit = "MTR" Then
                            ItemTable.Unit = "Meter"
                        End If
                    End If
                End If


                If ItemElementList(I).SelectSingleNode("STANDARDCOSTLIST.LIST") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("STANDARDCOSTLIST.LIST").SelectSingleNode("RATE") IsNot Nothing Then
                        ItemTable.PurchaseRate = ItemElementList(I).SelectSingleNode("STANDARDCOSTLIST.LIST").SelectSingleNode("RATE").ChildNodes(0).Value.ToString.Replace("/pcs", "")
                    End If
                End If

                If ItemElementList(I).SelectSingleNode("STANDARDPRICELIST.LIST") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("STANDARDPRICELIST.LIST").SelectSingleNode("RATE") IsNot Nothing Then
                        ItemTable.Rate = ItemElementList(I).SelectSingleNode("STANDARDPRICELIST.LIST").SelectSingleNode("RATE").ChildNodes(0).Value.ToString.Replace("/pcs", "")
                    End If
                End If


                'ItemTable.PurchaseRate = 0
                'ItemTable.Rate = 0

                If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST") IsNot Nothing Then
                    If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST") IsNot Nothing Then
                        If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST").SelectNodes("RATEDETAILS.LIST") IsNot Nothing Then
                            If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST").SelectNodes("RATEDETAILS.LIST").Count > 0 Then
                                If ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST").SelectNodes("RATEDETAILS.LIST").Item(2).SelectSingleNode("GSTRATE") IsNot Nothing Then
                                    ItemTable.SalesTaxPostingGroup = ItemElementList(I).SelectSingleNode("GSTDETAILS.LIST").SelectSingleNode("STATEWISEDETAILS.LIST").SelectNodes("RATEDETAILS.LIST").Item(2).SelectSingleNode("GSTRATE").ChildNodes(0).Value
                                End If
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

                If AgL.XNull(ItemTable.SalesTaxPostingGroup) = "" Then
                    ItemTable.SalesTaxPostingGroup = "GST 5%"
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

                mQry = "UPDATE ItemGroup Set ItemCategory = (Select code From ItemCategory Where Description = '" & ItemTable.ItemCategoryDesc & "')
                          Where Code = (Select code From ItemGroup Where Description = '" & ItemTable.ItemGroupDesc & "')  "
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
            MsgBox(ex.Message + " at row number " + I.ToString)
        End Try
    End Sub
    Public Shared Sub ImportItemCategoryTable(ItemCategoryTable As StructItemCategory)
        Dim mQry As String = ""
        If AgL.Dman_Execute("Select Count(*) From ItemCategory With (NoLock) where Description = '" & ItemCategoryTable.Description & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
            mQry = " INSERT INTO Item(Code, Description, ItemType, V_Type, SalesTaxPostingGroup, Unit, EntryBy, EntryDate, EntryType, EntryStatus, LockText, OMSId)
                    Select '" & ItemCategoryTable.Code & "' As Code, " & AgL.Chk_Text(ItemCategoryTable.Description) & " As Description, 
                    " & AgL.Chk_Text(ItemCategoryTable.ItemType) & " As ItemType, 
                    " & AgL.Chk_Text(ItemV_Type.ItemCategory) & " As V_Type, 
                    " & AgL.Chk_Text(ItemCategoryTable.SalesTaxPostingGroup) & " As SalesTaxPostingGroup, 
                    " & AgL.Chk_Text(ItemCategoryTable.Unit) & " As Unit, 
                    '" & ItemCategoryTable.EntryBy & "' As EntryBy, 
                    " & AgL.Chk_Date(ItemCategoryTable.EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(ItemCategoryTable.EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(ItemCategoryTable.EntryStatus) & " As EntryStatus,
                    " & AgL.Chk_Text(ItemCategoryTable.LockText) & " As LockText,
                    " & AgL.Chk_Text(ItemCategoryTable.OMSId) & " As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Public Shared Sub ImportItemGroupTable(ItemGroupTable As StructItemGroup)
        Dim mQry As String = ""
        If AgL.Dman_Execute("SELECT Count(*) From ItemGroup With (NoLock) where Description = " & AgL.Chk_Text(ItemGroupTable.Description) & " ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
            ItemGroupTable.ItemCategory = AgL.Dman_Execute("SELECT Code From ItemCategory With (NoLock) Where Replace(Description,' ','') = Replace('" & ItemGroupTable.ItemCategory & "',' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            mQry = " INSERT INTO Item(Code, Description, ItemCategory, ItemType, V_Type, Unit, Default_MarginPer, DefaultSupplier, EntryBy, EntryDate, EntryType, EntryStatus, LockText, OMSId)
                    Select '" & ItemGroupTable.Code & "' As Code, " & AgL.Chk_Text(ItemGroupTable.Description) & " As Description, 
                    " & AgL.Chk_Text(ItemGroupTable.ItemCategory) & " As ItemCategory, 
                    " & AgL.Chk_Text(ItemGroupTable.ItemType) & " As ItemType, 
                    " & AgL.Chk_Text(ItemV_Type.ItemGroup) & " As ItemType, 
                    " & AgL.Chk_Text(ItemGroupTable.Unit) & " As Unit, 
                    0 As Default_MarginPer,
                    " & AgL.Chk_Text(ItemGroupTable.DefaultSupplier) & " As DefaultSupplier, 
                    '" & ItemGroupTable.EntryBy & "' As EntryBy, 
                    " & AgL.Chk_Date(ItemGroupTable.EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(ItemGroupTable.EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(ItemGroupTable.EntryStatus) & " As EntryStatus, 
                    " & AgL.Chk_Text(ItemGroupTable.LockText) & " As LockText, 
                    " & AgL.Chk_Text(ItemGroupTable.OMSId) & " As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Public Shared Sub ImportItemGroupPersonTable(ItemGroupPersonTable As StructItemGroupPerson)
        Dim mQry As String = ""
        If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From ItemGroupPerson Igp 
                Where IfNull(Igp.ItemCategory,'') = '" & ItemGroupPersonTable.ItemCategory & "'
                And IfNull(Igp.ItemGroup,'') = '" & ItemGroupPersonTable.ItemGroup & "'
                And IfNull(Igp.Person,'') = '" & ItemGroupPersonTable.Person & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO ItemGroupPerson (ItemCategory, ItemGroup, Person, 
                    DiscountCalculationPattern, DiscountPer, AdditionalDiscountPer, 
                    AdditionalDiscountCalculationPattern, AdditionCalculationPattern, 
                    AdditionPer, InterestSlab)
                    Select " & AgL.Chk_Text(ItemGroupPersonTable.ItemCategory) & " As ItemCategory, 
                    " & AgL.Chk_Text(ItemGroupPersonTable.ItemGroup) & " As ItemGroup, 
                    " & AgL.Chk_Text(ItemGroupPersonTable.Person) & " As Person, 
                    " & AgL.Chk_Text(ItemGroupPersonTable.DiscountCalculationPattern) & " As DiscountCalculationPattern, 
                    " & Val(ItemGroupPersonTable.DiscountPer) & " As DiscountPer, 
                    " & Val(ItemGroupPersonTable.AdditionalDiscountPer) & " As AdditionalDiscountPer, 
                    " & AgL.Chk_Text(ItemGroupPersonTable.AdditionalDiscountCalculationPattern) & " As AdditionalDiscountCalculationPattern, 
                    " & AgL.Chk_Text(ItemGroupPersonTable.AdditionCalculationPattern) & " As AdditionCalculationPattern, 
                    " & Val(ItemGroupPersonTable.AdditionPer) & " As AdditionPer, 
                    " & AgL.Chk_Text(ItemGroupPersonTable.InterestSlab) & " As InterestSlab "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE ItemGroupPerson
                    Set DiscountCalculationPattern = " & AgL.Chk_Text(ItemGroupPersonTable.DiscountCalculationPattern) & ", 
                    DiscountPer = " & Val(ItemGroupPersonTable.DiscountPer) & ", 
                    AdditionalDiscountPer = " & Val(ItemGroupPersonTable.AdditionalDiscountPer) & ", 
                    AdditionalDiscountCalculationPattern = " & AgL.Chk_Text(ItemGroupPersonTable.AdditionalDiscountCalculationPattern) & ", 
                    AdditionCalculationPattern = " & AgL.Chk_Text(ItemGroupPersonTable.AdditionCalculationPattern) & ", 
                    AdditionPer = " & Val(ItemGroupPersonTable.AdditionPer) & ", 
                    InterestSlab  = " & AgL.Chk_Text(ItemGroupPersonTable.InterestSlab) & "
                    Where IfNull(ItemCategory,'') = '" & ItemGroupPersonTable.ItemCategory & "'
                    And IfNull(ItemGroup,'') = '" & ItemGroupPersonTable.ItemGroup & "'
                    And IfNull(Person,'') = '" & ItemGroupPersonTable.Person & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub

    Public Shared Sub ImportItemGroupRateTypeTable(ItemGroupRateTypeTable As StructItemGroupRateType)
        Dim mQry As String = ""
        If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From ItemGroupRateType Igp                 
                Where IfNull(Igp.Code,'') = '" & ItemGroupRateTypeTable.ItemGroup & "'
                And IfNull(Igp.RateType,'') = '" & ItemGroupRateTypeTable.RateType & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO ItemGroupRateType (Code, RateType, Margin, 
                    DiscountCalculationPattern, DiscountPer, AdditionalDiscountPer, 
                    AdditionalDiscountCalculationPattern, AdditionCalculationPattern, 
                    AdditionPer)
                    Select 
                    " & AgL.Chk_Text(ItemGroupRateTypeTable.ItemGroup) & " As ItemGroup, 
                    " & AgL.Chk_Text(ItemGroupRateTypeTable.RateType) & " As RateType, 
                    " & Val(ItemGroupRateTypeTable.Margin) & " As Margin,
                    " & AgL.Chk_Text(ItemGroupRateTypeTable.DiscountCalculationPattern) & " As DiscountCalculationPattern, 
                    " & Val(ItemGroupRateTypeTable.DiscountPer) & " As DiscountPer, 
                    " & Val(ItemGroupRateTypeTable.AdditionalDiscountPer) & " As AdditionalDiscountPer, 
                    " & AgL.Chk_Text(ItemGroupRateTypeTable.AdditionalDiscountCalculationPattern) & " As AdditionalDiscountCalculationPattern, 
                    " & AgL.Chk_Text(ItemGroupRateTypeTable.AdditionCalculationPattern) & " As AdditionCalculationPattern, 
                    " & Val(ItemGroupRateTypeTable.AdditionPer) & " As AdditionPer "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE ItemGroupRateType
                    Set DiscountCalculationPattern = " & AgL.Chk_Text(ItemGroupRateTypeTable.DiscountCalculationPattern) & ", 
                    DiscountPer = " & Val(ItemGroupRateTypeTable.DiscountPer) & ", 
                    AdditionalDiscountPer = " & Val(ItemGroupRateTypeTable.AdditionalDiscountPer) & ", 
                    AdditionalDiscountCalculationPattern = " & AgL.Chk_Text(ItemGroupRateTypeTable.AdditionalDiscountCalculationPattern) & ", 
                    AdditionCalculationPattern = " & AgL.Chk_Text(ItemGroupRateTypeTable.AdditionCalculationPattern) & ", 
                    AdditionPer = " & Val(ItemGroupRateTypeTable.AdditionPer) & ", 
                    Margin  = " & AgL.Chk_Text(ItemGroupRateTypeTable.Margin) & "                    
                    Where IfNull(Code,'') = '" & ItemGroupRateTypeTable.ItemGroup & "'
                    And IfNull(RateType,'') = '" & ItemGroupRateTypeTable.RateType & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub

    Public Shared Sub ImportItemTable(ItemTable As StructItem, Optional UpdateIfExists As Boolean = False)
        Dim mQry As String = ""
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From Item With (NoLock) where Description = " & AgL.Chk_Text(ItemTable.Description) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar) = 0 Then
            If AgL.XNull(ItemTable.ItemCategoryCode) = "" Then
                ItemTable.ItemCategoryCode = AgL.XNull(AgL.Dman_Execute("SELECT Code From ItemCategory With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.ItemCategoryDesc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            End If
            If AgL.XNull(ItemTable.ItemGroupCode) = "" And AgL.XNull(ItemTable.ItemGroupDesc) <> "" Then
                ItemTable.ItemGroupCode = AgL.XNull(AgL.Dman_Execute("Select Code From ItemGroup With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.ItemGroupDesc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            End If
            If AgL.XNull(ItemTable.BaseItemCode) = "" And AgL.XNull(ItemTable.BaseItemDesc) <> "" Then
                ItemTable.BaseItemCode = AgL.XNull(AgL.Dman_Execute("SELECT Code From Item With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.BaseItemDesc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            End If
            If AgL.XNull(ItemTable.Dimension1Code) = "" And AgL.XNull(ItemTable.Dimension1Desc) <> "" Then
                ItemTable.Dimension1Code = AgL.XNull(AgL.Dman_Execute("SELECT Code From Item With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.Dimension1Desc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            End If
            If AgL.XNull(ItemTable.Dimension2Code) = "" And AgL.XNull(ItemTable.Dimension2Desc) <> "" Then
                ItemTable.Dimension2Code = AgL.XNull(AgL.Dman_Execute("SELECT Code From Item With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.Dimension2Desc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            End If
            If AgL.XNull(ItemTable.Dimension3Code) = "" And AgL.XNull(ItemTable.Dimension3Desc) <> "" Then
                ItemTable.Dimension3Code = AgL.XNull(AgL.Dman_Execute("SELECT Code From Item With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.Dimension3Desc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            End If
            If AgL.XNull(ItemTable.Dimension4Code) = "" And AgL.XNull(ItemTable.Dimension4Desc) <> "" Then
                ItemTable.Dimension4Code = AgL.XNull(AgL.Dman_Execute("SELECT Code From Item With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.Dimension4Desc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            End If
            If AgL.XNull(ItemTable.SizeCode) = "" And AgL.XNull(ItemTable.Dimension4Desc) <> "" Then
                ItemTable.SizeCode = AgL.XNull(AgL.Dman_Execute("SELECT Code From Item With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.SizeDesc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
            End If


            ItemTable.SalesTaxPostingGroup = AgL.Dman_Execute("SELECT Description From PostingGroupSalesTaxItem With (NoLock) WHERE Description = " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            If ItemTable.SalesTaxPostingGroup = "" Then
                ItemTable.SalesTaxPostingGroup = AgL.Dman_Execute("SELECT Description From PostingGroupSalesTaxItem With (NoLock) WHERE GrossTaxRate = " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If

            mQry = " INSERT INTO Item(Code, ManualCode, Description, DisplayName, Specification, ItemGroup, ItemCategory, ItemType, BaseItem, Dimension1, Dimension2, Dimension3, Dimension4, Size, V_Type, Unit,
                    PurchaseRate, Rate, 
                    SalesTaxPostingGroup, HSN, Remark, Remark1, BarcodeType, BarcodePattern, EntryBy, EntryDate, EntryType, EntryStatus, Status, Div_Code, LockText, OMSId, StockYN, IsSystemDefine) 
                    Select '" & ItemTable.Code & "' As Code, 
                    " & AgL.Chk_Text(ItemTable.ManualCode) & " As ManuelCode, 
                    " & AgL.Chk_Text(ItemTable.Description) & " As Description, 
                    " & AgL.Chk_Text(ItemTable.DisplayName) & " As DisplayName, 
                    " & AgL.Chk_Text(ItemTable.Specification) & " As Specification, 
                    " & AgL.Chk_Text(ItemTable.ItemGroupCode) & " As ItemGroup, 
                    " & AgL.Chk_Text(ItemTable.ItemCategoryCode) & " As ItemCategory, 
                    " & AgL.Chk_Text(ItemTable.ItemType) & " As ItemType, 
                    " & AgL.Chk_Text(ItemTable.BaseItemCode) & " As BaseItem, 
                    " & AgL.Chk_Text(ItemTable.Dimension1Code) & " As Dimension1, 
                    " & AgL.Chk_Text(ItemTable.Dimension2Code) & " As Dimension2, 
                    " & AgL.Chk_Text(ItemTable.Dimension3Code) & " As Dimension3, 
                    " & AgL.Chk_Text(ItemTable.Dimension4Code) & " As Dimension4, 
                    " & AgL.Chk_Text(ItemTable.SizeCode) & " As Size, 
                    " & AgL.Chk_Text(ItemTable.V_Type) & " As V_Type, 
                    " & AgL.Chk_Text(ItemTable.Unit) & " As Unit, 
                    " & AgL.Chk_Text(ItemTable.PurchaseRate) & " As PurchaseRate, 
                    " & AgL.Chk_Text(ItemTable.Rate) & " As Rate,
                    " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & " As SalesTaxPostingGroup, 
                    " & AgL.Chk_Text(ItemTable.HSN) & " As HSNCode,
                    " & AgL.Chk_Text(ItemTable.Remark) & " As Remark,
                    " & AgL.Chk_Text(ItemTable.Remark1) & " As Remark1,
                    " & AgL.Chk_Text(ItemTable.BarcodeType) & " As BarcodeType, 
                    " & AgL.Chk_Text(ItemTable.BarcodePattern) & " As BarcodePattern, 
                    " & AgL.Chk_Text(ItemTable.EntryBy) & " As EntryBy, 
                    " & AgL.Chk_Text(ItemTable.EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(ItemTable.EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(ItemTable.EntryStatus) & " As EntryStatus, 
                    " & AgL.Chk_Text(ItemTable.Status) & " As Status, 
                    " & AgL.Chk_Text(ItemTable.Div_Code) & " , 
                    " & AgL.Chk_Text(ItemTable.LockText) & " As LockText , 
                    " & AgL.Chk_Text(ItemTable.OMSId) & " As OMSId , 
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
        Else
            ItemTable.Code = AgL.Dman_Execute("SELECT Code From Item With (NoLock) where Description = " & AgL.Chk_Text(ItemTable.Description) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            If AgL.XNull(ItemTable.ItemGroupCode) = "" And AgL.XNull(ItemTable.ItemGroupDesc) <> "" Then
                ItemTable.ItemGroupCode = AgL.Dman_Execute("SELECT Code From ItemGroup With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.ItemGroupDesc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If
            If AgL.XNull(ItemTable.ItemCategoryCode) = "" And AgL.XNull(ItemTable.ItemCategoryDesc) <> "" Then
                ItemTable.ItemCategoryCode = AgL.Dman_Execute("SELECT Code From ItemCategory With (NoLock) Where Replace(Description,' ','') = Replace(" & AgL.Chk_Text(ItemTable.ItemCategoryDesc) & ",' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If
            ItemTable.SalesTaxPostingGroup = AgL.Dman_Execute("SELECT Description From PostingGroupSalesTaxItem With (NoLock) WHERE Description = " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            If ItemTable.SalesTaxPostingGroup = "" Then
                ItemTable.SalesTaxPostingGroup = AgL.Dman_Execute("SELECT Description From PostingGroupSalesTaxItem With (NoLock) WHERE GrossTaxRate = " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            End If


            mQry = " 
                    Update Item Set             
                    ManualCode = " & AgL.Chk_Text(ItemTable.ManualCode) & ", 
                    Description = " & AgL.Chk_Text(ItemTable.Description) & ", 
                    DisplayName = " & AgL.Chk_Text(ItemTable.DisplayName) & ", 
                    Specification = " & AgL.Chk_Text(ItemTable.Specification) & ", 
                    ItemGroup = " & AgL.Chk_Text(ItemTable.ItemGroupCode) & ", 
                    ItemCategory = " & AgL.Chk_Text(ItemTable.ItemCategoryCode) & ", 
                    ItemType = " & AgL.Chk_Text(ItemTable.ItemType) & ", 
                    V_Type = " & AgL.Chk_Text(ItemTable.V_Type) & ", 
                    Unit = " & AgL.Chk_Text(ItemTable.Unit) & ",
                    PurchaseRate = " & AgL.Chk_Text(ItemTable.PurchaseRate) & ", 
                    Rate = " & AgL.Chk_Text(ItemTable.Rate) & ", 
                    SalesTaxPostingGroup = " & AgL.Chk_Text(ItemTable.SalesTaxPostingGroup) & ", 
                    HSN = " & AgL.Chk_Text(ItemTable.HSN) & ", 
                    Remark = " & AgL.Chk_Text(ItemTable.Remark) & ", 
                    Remark1 = " & AgL.Chk_Text(ItemTable.Remark1) & ", 
                    EntryBy = " & AgL.Chk_Text(ItemTable.EntryBy) & ", 
                    EntryDate = " & AgL.Chk_Date(ItemTable.EntryDate) & ", 
                    EntryType = " & AgL.Chk_Text(ItemTable.EntryType) & ", 
                    EntryStatus = " & AgL.Chk_Text(ItemTable.EntryStatus) & ", 
                    Status = " & AgL.Chk_Text(ItemTable.Status) & ", 
                    Div_Code = " & AgL.Chk_Text(ItemTable.Div_Code) & ", 
                    LockText = " & AgL.Chk_Text(ItemTable.LockText) & ", 
                    OMSId = " & AgL.Chk_Text(ItemTable.OMSId) & ", 
                    StockYN = " & AgL.Chk_Text(ItemTable.StockYN) & ", 
                    IsSystemDefine = " & AgL.Chk_Text(ItemTable.IsSystemDefine) & " 
                    Where Code = '" & ItemTable.Code & "' 
                "
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

            ImportRateListTable(RateListTable, UpdateIfExists)

        End If


        'Dim mCnt As Integer = AgL.Dman_Execute("SELECT Count(*) From Item With (NoLock) where Description = " & AgL.Chk_Text(ItemTable.Description) & "", AgL.GcnRead).ExecuteScalar
        'MsgBox(mCnt)

        'Dim DsTemp As DataSet = AgL.FillData("SELECT * From Item With (NoLock) where Description = " & AgL.Chk_Text(ItemTable.Description) & "", AgL.GcnRead)
        'MsgBox(DsTemp.Tables(0).Rows(0)("Code"))
        'MsgBox(DsTemp.Tables(0).Rows(0)("Description"))
        'MsgBox(DsTemp.Tables(0).Rows(0)("Specification"))
    End Sub
    Public Shared Sub ImportRateListTable(RateListTable As StructRateList, Optional UpdateIfExists As Boolean = False)
        Dim mQry As String = ""
        mQry = "Select Code From RateType With (NoLock) Where Description= '" & RateListTable.Line_RateType & "'"
        RateListTable.Line_RateType = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

        If UpdateIfExists Then
            mQry = "Delete from RateList Where Code = " & AgL.Chk_Text(RateListTable.Code) & " "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Delete from RateListDetail Where Code = " & AgL.Chk_Text(RateListTable.Code) & " "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        If AgL.Dman_Execute("Select Count(*) From RateList With (NoLock) Where Code = '" & RateListTable.Code & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then
            mQry = " INSERT INTO RateList(Code, WEF, EntryBy, EntryDate, EntryType, " &
                        " EntryStatus, Status, Div_Code, GenDocID, GenV_Type) " &
                        " VALUES (" & AgL.Chk_Text(RateListTable.Code) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                        " " & AgL.Chk_Text("E") & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                        " '" & RateListTable.Div_Code & "', " & AgL.Chk_Text(RateListTable.Code) & ", '" & ItemV_Type.Item & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        Dim mSr As Integer = AgL.Dman_Execute("Select IfNull(Max(Sr),0) + 1 From RateListDetail With (NoLock) Where Code = '" & RateListTable.Code & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
        If Val(RateListTable.Line_Rate) > 0 Then
            mQry = "INSERT INTO RateListDetail(Code, Sr, Item, RateType, Rate) " &
                        " VALUES (" & AgL.Chk_Text(RateListTable.Code) & ", " &
                        " " & mSr & ", " &
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
        Dim SalesTaxPostingGroup As String
        Dim Unit As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim LockText As String
        Dim Div_Code As String
        Dim Status As String
        Dim OMSId As String
    End Structure
    Public Structure StructItemGroup
        Dim Code As String
        Dim Description As String
        Dim ItemCategory As String
        Dim ItemType As String
        Dim Default_MarginPer As Double
        Dim SalesTaxPostingGroup As String
        Dim DefaultSupplier As String
        Dim Unit As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim LockText As String
        Dim Div_Code As String
        Dim Status As String
        Dim OMSId As String
    End Structure
    Public Structure StructItemGroupPerson
        Dim ItemCategory As String
        Dim ItemGroup As String
        Dim Person As String
        Dim DiscountCalculationPattern As String
        Dim DiscountPer As String
        Dim AdditionalDiscountPer As String
        Dim AdditionalDiscountCalculationPattern As String
        Dim AdditionCalculationPattern As String
        Dim AdditionPer As String
        Dim InterestSlab As String
        Dim OMSId As String
    End Structure

    Public Structure StructItemGroupRateType
        Dim ItemGroup As String
        Dim RateType As String
        Dim Margin As String
        Dim DiscountCalculationPattern As String
        Dim DiscountPer As String
        Dim AdditionalDiscountPer As String
        Dim AdditionalDiscountCalculationPattern As String
        Dim AdditionCalculationPattern As String
        Dim AdditionPer As String
        Dim OMSId As String
    End Structure

    Public Structure StructItem
        Dim Code As String
        Dim ManualCode As String
        Dim Description As String
        Dim DisplayName As String
        Dim Specification As String
        Dim ItemGroupCode As String
        Dim ItemGroupDesc As String
        Dim ItemCategoryCode As String
        Dim ItemCategoryDesc As String
        Dim BaseItemCode As String
        Dim BaseItemDesc As String
        Dim Dimension1Code As String
        Dim Dimension1Desc As String
        Dim Dimension2Code As String
        Dim Dimension2Desc As String
        Dim Dimension3Code As String
        Dim Dimension3Desc As String
        Dim Dimension4Code As String
        Dim Dimension4Desc As String
        Dim SizeCode As String
        Dim SizeDesc As String
        Dim ItemType As String
        Dim V_Type As String
        Dim PurchaseRate As String
        Dim Rate As String
        Dim SalesTaxPostingGroup As String
        Dim HSN As String
        Dim Remark As String
        Dim Remark1 As String
        Dim Unit As String
        Dim BarcodeType As String
        Dim BarcodePattern As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Status As String
        Dim LockText As String
        Dim OMSId As String
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
        Dim mCondStr As String
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
                Case rowPurchaseAc, rowSalesAc
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name FROM viewHelpSubgroup Where Nature Not In ('Customer','Supplier') "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowBaseItem
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.Code, H.Description From Item H Where H.V_Type = 'Item' Order By H.Description "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
                Case rowDimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension1 H Order By H.Description "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
                Case rowDimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension2 H Order By H.Description "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
                Case rowDimension3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension3 H Order By H.Description "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
                Case rowDimension4
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension4 H Order By H.Description "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowSite
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name FROM SiteMast "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowItemName
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select I.Code, I.Description As Name , I.Div_Code, I.ItemType " &
                                    " From Item I " &
                                    " Left Join ItemType IT On I.ItemType = IT.Code " &
                                    " Where IfNull(IT.Parent,IT.Code) in ('" & ItemTypeCode.TradingProduct & "','" & ItemTypeCode.OtherProduct & "')" &
                                    " Order By I.Description"
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value, 2) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If


                Case rowUnit, rowDealUnit, rowStockUnit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Code AS Unit FROM Unit "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowShape
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Description FROM Shape Order By Description "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowSizeUnit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Code AS Unit FROM Unit "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowSalesTaxGroup
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Description as  Code, Description AS PostingGroupSalesTaxItem FROM PostingGroupSalesTaxItem "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If





                Case rowItemCode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select I.Code, I.ManualCode As ItemCode, I.Div_Code , I.ItemType " &
                                    " From Item I " &
                                    " Left Join ItemType IT On I.ItemType = IT.Code " &
                                    " Where IfNull(IT.Parent, IT.Code) in ('" & ItemTypeCode.TradingProduct & "','" & ItemTypeCode.OtherProduct & "')" &
                                    " Order By I.ManualCode "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value, 2) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If


                Case rowSpecification
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select Specification Code, Specification " &
                                    " From Item Where ItemGroup = '" & Dgl1(Col1Value, rowItemGroup).Tag & "' Group By Specification" &
                                    " Order By Specification "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If



                Case rowItemType
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT IT.Code, IT.Name 
                                    FROM ItemType IT  Where Code <> '" & ItemTypeCode.InternalProduct & "'"
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowItemCategory
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mCondStr = ""
                            If Dgl1.Item(Col1Value, rowItemType).Value <> "" Then mCondStr += " And  IC.ItemType =  " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowItemType).Tag) & " "
                            mQry = "Select IC.Code, IC.Description, IC.ItemType, IT.Name As ItemTypeName, IC.SalesTaxGroup, IC.Unit, IC.Hsn 
                                    FROM ItemCategory IC 
                                    Left Join ItemType IT On IC.ItemType = IT.Code 
                                    Where 1=1  " & mCondStr & " Order by IC.Description  "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value, 4) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If



                Case rowItemGroup
                    If e.KeyCode = Keys.Insert Then
                        FOpenItemGroupMaster()
                    Else
                        If e.KeyCode <> Keys.Enter Then
                            If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                                If DtItemTypeSetting.rows(0)("IsItemGroupLinkedWithItemCategory") Then
                                    mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                                        " From ItemGroup I " &
                                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                                        " WHERE I.ItemCategory='" & Dgl1(Col1Value, rowItemCategory).Tag & "' And I.ItemType = '" & Dgl1(Col1Value, rowItemType).Tag & "' "
                                    If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
                                        mQry += " And (IfNull(I.Div_Code,'" & AgL.PubDivCode & "') = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemGroupInOtherDivisions,0) =1) "
                                    End If
                                    If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
                                        mQry += " And (IfNull(I.Site_Code,'" & AgL.PubSiteCode & "') = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemGroupInOtherSites,0) =1) "
                                    End If


                                    Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                                Else
                                    mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                                        " From ItemGroup I " &
                                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                                        " WHERE  I.ItemType = '" & Dgl1(Col1Value, rowItemType).Tag & "' "
                                    If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
                                        mQry += " And (IfNull(I.Div_Code,'" & AgL.PubDivCode & "') = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemGroupInOtherDivisions,0) =1) "
                                    End If
                                    If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
                                        mQry += " And (IfNull(I.Site_Code,'" & AgL.PubSiteCode & "') = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemGroupInOtherSites,0) =1) "
                                    End If
                                    Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                                End If
                            End If

                            If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                                Dgl1.AgHelpDataSet(Col1Value, 4) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                            End If
                        End If
                    End If





                Case rowShowItemInOtherDivision, rowShowItemInOtherSites, rowMaintainStockYn
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



            If Me.Visible And sender.ReadOnly = False Then
                If sender.CurrentCell.ColumnIndex = sender.Columns(Col1Head).Index Or
                    sender.CurrentCell.ColumnIndex = sender.Columns(Col1Mandatory).Index Then
                    SendKeys.Send("{Tab}")
                End If
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
                Case rowDealQty
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 8
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
                Case rowLength, rowWidth, rowThickness
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 8
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
                Case rowItemCategory
                    Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = Nothing
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim I As Integer

        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then
            If Dgl1.Item(Col1Mandatory, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                If Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = "" Then
                    MsgBox(Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowItemType
                    'ApplyItemTypeSetting(Dgl1.Item(Col1Value, rowItemType).Tag)
                    ApplyUISetting()

                    If Dgl1.Item(Col1Value, rowItemType).Value <> Dgl1.Item(Col1LastValue, rowItemType).Value Then
                        Dgl1.Item(Col1Value, rowItemCategory).Value = ""
                        Dgl1.Item(Col1Value, rowItemCategory).Tag = ""
                        Dgl1.Item(Col1Value, rowItemGroup).Value = ""
                        Dgl1.Item(Col1Value, rowItemGroup).Tag = ""
                    End If

                    If Dgl1.Rows(rowSpecification).Visible Then Dgl1.CurrentCell = Dgl1(Col1Value, rowSpecification)
                    If Dgl1.Rows(rowItemGroup).Visible Then Dgl1.CurrentCell = Dgl1(Col1Value, rowItemGroup)
                    If Dgl1.Rows(rowItemCategory).Visible Then Dgl1.CurrentCell = Dgl1(Col1Value, rowItemCategory)
                    Dgl1.Focus()


                Case rowItemCategory
                    Validate_ItemCategory()
                Case rowItemGroup
                    Validate_ItemGroup()
                Case rowSpecification
                    SetProductName()
                Case rowShape
                    Validate_Shape()
                Case rowPurchaseRate, rowMarginPer, rowLength, rowWidth, rowThickness, rowShape, rowSizeUnit, rowArea, rowPerimeter
                    Calculation()
                Case rowSaleRate
                    Calculation()
                    'Dgl1(Col1Value, rowMarginPer).Value = Math.Round(((Val(Dgl1(Col1Value, rowSaleRate).Value) - Val(Dgl1(Col1Value, rowPurchaseRate).Value)) * 100) / Val(Dgl1(Col1Value, rowPurchaseRate).Value), 2)
            End Select
        End If
        SetProductName()
    End Sub



    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub

        If Topctrl1.Mode.ToUpper <> "BROWSE" Then
            If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then
                If e.KeyCode = Keys.Delete Then
                    Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                    Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = ""
                End If

                If e.KeyCode = Keys.Enter Then
                    Dim LastCell As DataGridViewCell = LastDisplayedCell(Dgl1)
                    If Dgl1.CurrentCell.RowIndex = LastCell.RowIndex And Dgl1.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                        DGLRateType.CurrentCell = DGLRateType.FirstDisplayedCell
                        DGLRateType.Focus()
                    End If
                End If
            End If
        End If


    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, Dgl1(Col1Value, rowItemType).Tag, Dgl1(Col1Value, rowItemCategory).Tag, mItemVTypes, "", "")
        FGetSettings = mValue
    End Function

    Private Sub FrmItemMaster_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        Dim J As Integer
        Dim mBaseRate As Double
        Dim mMarginAmt As Double = 0



        Dim mMarginCalculation As String = FGetSettings(SettingFields.Default_MarginCalculation, SettingType.General)

        If mMarginCalculation <> "" Then
            If Dgl1(Col1Value, rowMarginPer).Value > 0 Then
                mMarginCalculation = Replace(mMarginCalculation, "{MARGINRATE}", Dgl1(Col1Value, rowMarginPer).Value)
                mMarginCalculation = Replace(mMarginCalculation, "{PURCHASERATE}", Dgl1(Col1Value, rowPurchaseRate).Value)
                mMarginAmt = New DataTable().Compute(mMarginCalculation, "")

                'If Val(Dgl1(Col1Value, rowSaleRate).Value) = 0 Or Val(Dgl1(Col1Value, rowSaleRate).Value) = Val(Dgl1(Col1Value, rowPurchaseRate).Value) + Math.Round(mMarginAmt, 0) Then
                Dgl1(Col1Value, rowSaleRate).Value = Format(Math.Round(Val(Dgl1(Col1Value, rowPurchaseRate).Value) + mMarginAmt, 0), "0.00")
                'End If
            End If
        Else

            If Val(Dgl1(Col1Value, rowMarginPer).Value) > 0 Then
                If Val(Dgl1(Col1Value, rowSaleRate).Value) = 0 Or Val(Dgl1(Col1Value, rowSaleRate).Value) = Math.Round(Val(Dgl1(Col1LastValue, rowPurchaseRate).Value) + (Val(Dgl1(Col1LastValue, rowPurchaseRate).Value) * Val(Dgl1(Col1Value, rowMarginPer).Value / 100)), 0) Or Val(Dgl1(Col1Value, rowSaleRate).Value) = Math.Round(Val(Dgl1(Col1Value, rowPurchaseRate).Value) + (Val(Dgl1(Col1Value, rowPurchaseRate).Value) * Val(Dgl1(Col1LastValue, rowMarginPer).Value / 100)), 0) Then
                    Dgl1(Col1Value, rowSaleRate).Value = Format(Math.Round(Val(Dgl1(Col1Value, rowPurchaseRate).Value) + (Val(Dgl1(Col1Value, rowPurchaseRate).Value) * Val(Dgl1(Col1Value, rowMarginPer).Value) / 100), 0), "0.00")
                End If
            End If
        End If

        If Val(Dgl1(Col1Value, rowSaleRate).Tag) = 0 Then
            If AgL.StrCmp(AgL.PubDBName, "SITARAMHC") Then
                For I = 0 To DGLRateType.RowCount - 1
                    If DGLRateType.Item(Col1RateType, I).Value <> "" Then
                        If DGLRateType.Item(Col1CalculateOnRateType, I).Value <> "" Then
                            For J = 0 To DGLRateType.Rows.Count - 1
                                If DGLRateType.Item(Col1CalculateOnRateType, I).Value = DGLRateType.Item(Col1RateType, J).Value Then
                                    mBaseRate = Val(DGLRateType.Item(Col1Rate, J).Value)
                                End If
                            Next
                        Else
                            mBaseRate = Val(Dgl1(Col1Value, rowPurchaseRate).Value)
                        End If

                        'DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(Val(Dgl1(Col1Value, rowPurchaseRate).Value) + (Val(Dgl1(Col1Value, rowPurchaseRate).Value) * Val(DGLRateType.Item(Col1Margin, I).Value) / 100), 0), "0.00")
                        If Val(DGLRateType.Item(Col1Margin, I).Value) <> 0 Then
                            DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(mBaseRate + (mBaseRate * Val(DGLRateType.Item(Col1Margin, I).Value) / 100), 0), "0.00")
                        Else
                            If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
                                DGLRateType.Item(Col1Rate, I).Value = Math.Round(mBaseRate, 0)
                            End If
                        End If
                    End If
                Next
            Else
                For I = 0 To DGLRateType.RowCount - 1
                    If DGLRateType.Item(Col1RateType, I).Value <> "" Then
                        If DGLRateType.Item(Col1CalculateOnRateType, I).Value <> "" Then
                            For J = 0 To DGLRateType.Rows.Count - 1
                                If DGLRateType.Item(Col1CalculateOnRateType, I).Value = DGLRateType.Item(Col1RateType, J).Value Then
                                    mBaseRate = Val(DGLRateType.Item(Col1Rate, J).Value)
                                End If
                            Next
                        Else
                            mBaseRate = Val(Dgl1(Col1Value, rowSaleRate).Value)
                        End If

                        'DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(Val(Dgl1(Col1Value, rowSaleRate).Value) + (Val(Dgl1(Col1Value, rowSaleRate).Value) * Val(DGLRateType.Item(Col1Margin, I).Value) / 100), 0), "0.00")
                        If Val(DGLRateType.Item(Col1Margin, I).Value) <> 0 Then
                            DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(mBaseRate + (mBaseRate * Val(DGLRateType.Item(Col1Margin, I).Value) / 100), 0), "0.00")
                        Else
                            If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
                                DGLRateType.Item(Col1Rate, I).Value = Math.Round(mBaseRate, 0)
                            End If
                        End If
                    End If
                Next
            End If
        Else
                If AgL.StrCmp(AgL.PubDBName, "SITARAMHC") Then
                If Val(Dgl1(Col1Value, rowPurchaseRate).Tag) <> Val(Dgl1(Col1Value, rowPurchaseRate).Value) Then
                    If DGLRateType.Visible = True Then
                        If DGLRateType.Rows.Count >= 1 Then
                            If DGLRateType.Item(Col1RateType, 0).Value <> "" Then
                                'If MsgBox("Do you want to update all rate types", vbYesNo) = vbYes Then
                                For I = 0 To DGLRateType.RowCount - 1
                                    If DGLRateType.Item(Col1RateType, I).Value <> "" Then
                                        If DGLRateType.Item(Col1CalculateOnRateType, I).Value <> "" Then
                                            For J = 0 To DGLRateType.Rows.Count - 1
                                                If DGLRateType.Item(Col1CalculateOnRateType, I).Value = DGLRateType.Item(Col1RateType, J).Value Then
                                                    mBaseRate = Val(DGLRateType.Item(Col1Rate, J).Value)
                                                End If
                                            Next
                                        Else
                                            mBaseRate = Val(Dgl1(Col1Value, rowPurchaseRate).Value)
                                        End If

                                        'DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(Val(Dgl1(Col1Value, rowPurchaseRate).Value) + (Val(Dgl1(Col1Value, rowPurchaseRate).Value) * Val(DGLRateType.Item(Col1Margin, I).Value) / 100), 0), "0.00")
                                        If Val(DGLRateType.Item(Col1Margin, I).Value) <> 0 Then
                                            DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(mBaseRate + (mBaseRate * Val(DGLRateType.Item(Col1Margin, I).Value) / 100), 0), "0.00")
                                        Else
                                            DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(mBaseRate, 0), "0.00")
                                        End If
                                    End If
                                Next
                                'End If
                            End If
                        End If
                    End If
                End If
            Else
                If Val(Dgl1(Col1Value, rowSaleRate).Tag) <> Val(Dgl1(Col1Value, rowSaleRate).Value) Then
                    If DGLRateType.Visible = True Then
                        If DGLRateType.Rows.Count >= 1 Then
                            If DGLRateType.Item(Col1RateType, 0).Value <> "" Then
                                'If MsgBox("Do you want to update all rate types", vbYesNo) = vbYes Then
                                For I = 0 To DGLRateType.RowCount - 1
                                    If DGLRateType.Item(Col1RateType, I).Value <> "" Then
                                        If DGLRateType.Item(Col1CalculateOnRateType, I).Value <> "" Then
                                            For J = 0 To DGLRateType.Rows.Count - 1
                                                If DGLRateType.Item(Col1CalculateOnRateType, I).Value = DGLRateType.Item(Col1RateType, J).Value Then
                                                    mBaseRate = Val(DGLRateType.Item(Col1Rate, J).Value)
                                                End If
                                            Next
                                        Else
                                            mBaseRate = Val(Dgl1(Col1Value, rowSaleRate).Value)
                                        End If

                                        'DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(Val(Dgl1(Col1Value, rowSaleRate).Value) + (Val(Dgl1(Col1Value, rowSaleRate).Value) * Val(DGLRateType.Item(Col1Margin, I).Value) / 100), 0), "0.00")
                                        If Val(DGLRateType.Item(Col1Margin, I).Value) <> 0 Then
                                            DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(mBaseRate + (mBaseRate * Val(DGLRateType.Item(Col1Margin, I).Value) / 100), 0), "0.00")
                                        Else
                                            DGLRateType.Item(Col1Rate, I).Value = Format(Math.Round(mBaseRate, 0), "0.00")
                                        End If
                                    End If
                                Next
                                'End If
                            End If
                        End If
                    End If
                End If
            End If


        End If

        If Dgl1.Rows(rowArea).Visible Then
            If AgL.XNull(Dgl1.Item(Col1Value, rowShapeAreaFormula).Value) = "" Then
                'Dgl1.Item(Col1Value, rowShapeAreaFormula).Value = "<LENGTH>*<WIDTH>"
            End If
            If AgL.XNull(Dgl1.Item(Col1Value, rowShapePerimeterFormula).Value) = "" Then
                'Dgl1.Item(Col1Value, rowShapePerimeterFormula).Value = "2*(<LENGTH>+<WIDTH>)"
            End If

            Dim mFormulaStr As String
            Dim mLength As Double = 0
            Dim mLengthWholePart As Double = 0
            Dim mLengthFractionPart As Double = 0
            Dim mWidth As Double = 0
            Dim mWidthWholePart As Double = 0
            Dim mWidthFractionPart As Double = 0
            Dim mThickness As Double = 0
            Dim mThicknessWholePart As Double = 0
            Dim mThicknessFractionPart As Double = 0

            If Dgl1.Item(Col1Value, rowShapeAreaFormula).Value <> "" Then
                If AgL.XNull(Dgl1.Item(Col1Value, rowSizeUnit).Value).ToString.Contains("Feet") Then
                    mLengthWholePart = Math.Truncate(Val(Dgl1.Item(Col1Value, rowLength).Value))
                    mWidthWholePart = Math.Truncate(Val(Dgl1.Item(Col1Value, rowWidth).Value))
                    mThicknessWholePart = Math.Truncate(Val(Dgl1.Item(Col1Value, rowThickness).Value))

                    If Math.Truncate((Val(Dgl1.Item(Col1Value, rowLength).Value) - mLengthWholePart) * 100) > 11 Then
                        Dgl1.Item(Col1Value, rowLength).Value = mLengthWholePart.ToString
                    End If
                    If Math.Truncate((Val(Dgl1.Item(Col1Value, rowWidth).Value) - mWidthWholePart) * 100) > 11 Then
                        Dgl1.Item(Col1Value, rowWidth).Value = mWidthWholePart.ToString
                    End If
                    If Math.Truncate((Val(Dgl1.Item(Col1Value, rowThickness).Value) - mThicknessWholePart) * 100) > 11 Then
                        Dgl1.Item(Col1Value, rowThickness).Value = mThicknessWholePart.ToString
                    End If

                    mLengthFractionPart = Math.Round((Val(Dgl1.Item(Col1Value, rowLength).Value) - mLengthWholePart) * 100 / 12, 3)
                    mWidthFractionPart = Math.Round((Val(Dgl1.Item(Col1Value, rowWidth).Value) - mWidthWholePart) * 100 / 12, 3)
                    mThicknessFractionPart = Math.Round((Val(Dgl1.Item(Col1Value, rowThickness).Value) - mThicknessWholePart) * 100 / 12, 3)

                    mLength = mLengthWholePart + mLengthFractionPart
                    mWidth = mWidthWholePart + mWidthFractionPart
                    mThickness = mThicknessWholePart + mThicknessFractionPart
                Else
                    mLength = Val(Dgl1.Item(Col1Value, rowLength).Value)
                    mWidth = Val(Dgl1.Item(Col1Value, rowWidth).Value)
                    mThickness = Val(Dgl1.Item(Col1Value, rowThickness).Value)
                End If

                mFormulaStr = Dgl1.Item(Col1Value, rowShapeAreaFormula).Value.ToString.ToUpper().
                 Replace("<LENGTH>", Val(mLength)).
                 Replace("<WIDTH>", Val(mWidth)).
                 Replace("<THICKNESS>", Val(mThickness)).
                 Replace("<HEIGHT>", Val(mThickness))

                Dgl1.Item(Col1Value, rowArea).Value = New DataTable().Compute(mFormulaStr, "")
                Dgl1.Item(Col1Value, rowArea).Value = Math.Round(Dgl1.Item(Col1Value, rowArea).Value, 3)
            End If

            If Dgl1.Item(Col1Value, rowShapePerimeterFormula).Value <> "" Then
                mFormulaStr = Dgl1.Item(Col1Value, rowShapePerimeterFormula).Value.ToString.ToUpper().
                 Replace("<LENGTH>", Val(mLength)).
                 Replace("<WIDTH>", Val(mWidth)).
                 Replace("<THICKNESS>", Val(mThickness)).
                 Replace("<HEIGHT>", Val(mThickness))
                Dgl1.Item(Col1Value, rowPerimeter).Value = New DataTable().Compute(mFormulaStr, "")
                Dgl1.Item(Col1Value, rowPerimeter).Value = Math.Round(Dgl1.Item(Col1Value, rowPerimeter).Value, 2)
            End If
        End If
    End Sub

    Private Sub DGLRateType_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DGLRateType.EditingControl_Validating
        If DGLRateType.Columns(DGLRateType.CurrentCell.ColumnIndex).Name = Col1Rate Then
            Calculation()
        End If
    End Sub

    Private Sub Dgl1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl1.CellBeginEdit
        If Dgl1.CurrentCell.RowIndex = rowMarginPer Then
            If Dgl1.Columns(Col1Value).Index = Dgl1.CurrentCell.ColumnIndex Then
                If FGetSettings(SettingFields.Default_MarginBaseField, SettingType.General) <> DefaultMarginBaseField.Item Then
                    MsgBox("Margin Calculation Base Field Is Set to 'Item Group', In this setting margin rate is controlled from Item Group Master")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If

        Dgl1(Col1LastValue, Dgl1.CurrentCell.RowIndex).Value = Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value
    End Sub

    Private Sub FrmItemMaster_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select * From Unit"
        DtUnit = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub


    Private Sub Dgl1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Dgl1.EditingControlShowing, DGLRateType.EditingControlShowing
        If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Lower
        End If
    End Sub
    'Patch 30/Mar/2019
    Public Sub FImportDesignFromExcel(bImportFor As ImportFor)
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
                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")).ToString().Trim() = "GST @3%" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")) = "GST 3%"
                End If
                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")).ToString().Trim() = "GST @ 5%" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")) = "GST 5%"
                End If
                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")).ToString().Trim() = "GST @12%" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")) = "GST 12%"
                End If
                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")).ToString().Trim() = "GST @18%" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")) = "GST 18%"
                End If
                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")).ToString().Trim() = "GST @28%" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")) = "GST 28%"
                End If
                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")).ToString().Trim() = "" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group")) = "GST 5%"
                End If


                If DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString().Trim() = "P" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Pcs"
                ElseIf DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString().Trim() = "M" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Meter"
                ElseIf DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")).ToString().Trim() = "K" Then
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Kg"
                Else
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Unit")) = "Pcs"
                End If

                DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name")) = DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification")).ToString.Trim +
                    " - " + DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group")).ToString.Trim + " - " +
                    DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category")).ToString.Trim

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
                    If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxItem where Description = '" & AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim & "' ", AgL.GCn).ExecuteScalar = 0 Then
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


            Dim bLastItemCategoryCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemCategory = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Category"), GetFieldAliasName(bImportFor, "Sales Tax Group"))
            For I = 0 To DtItemCategory.Rows.Count - 1
                If AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))) <> "" Then
                    Dim ItemCategoryTable As New StructItemCategory
                    Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemCategoryTable.Code = bItemCategoryCode
                    ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    ItemCategoryTable.ItemType = "TP"
                    ItemCategoryTable.SalesTaxPostingGroup = AgL.XNull(DtItemCategory.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim
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

            Dim bLastItemGroupCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemGroup = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Item Group"), GetFieldAliasName(bImportFor, "Item Category"), GetFieldAliasName(bImportFor, "Sales Tax Group"))
            For I = 0 To DtItemGroup.Rows.Count - 1
                If AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))) <> "" Then
                    Dim ItemGroupTable As New StructItemGroup
                    Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemGroupTable.Code = bItemGroupCode
                    ItemGroupTable.Description = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))).ToString.Trim
                    ItemGroupTable.ItemCategory = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    ItemGroupTable.ItemType = "TP"
                    ItemGroupTable.SalesTaxPostingGroup = AgL.XNull(DtItemGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString.Trim
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
                    'ItemTable.Description = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Name"))).ToString.Trim
                    ItemTable.Description = AgL.XNull(DtTemp.Rows(I)("Design")).ToString.Trim +
                        " - " + AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification"))).ToString.Trim +
                        " - " + AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))).ToString.Trim +
                        " - " + AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim
                    'ItemTable.DisplayName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Display Name"))).ToString.Trim
                    ItemTable.DisplayName = ItemTable.Description
                    'ItemTable.Specification = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification"))).ToString.Trim
                    ItemTable.Specification = AgL.XNull(DtTemp.Rows(I)("Design")).ToString.Trim
                    ItemTable.ItemGroupDesc = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))).ToString.Trim
                    ItemTable.ItemCategoryDesc = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim

                    'If DtTemp.Columns.Contains("Base Item") Then
                    '    ItemTable.BaseItem = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Base Item"))).ToString.Trim
                    'End If
                    ItemTable.BaseItemDesc = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Specification"))).ToString.Trim +
                        " - " + AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Group"))).ToString.Trim +
                        " - " + AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Item Category"))).ToString.Trim

                    ItemTable.ItemType = "TP"
                    ItemTable.V_Type = "D1"
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

                    mQry = "Update Item Set HSN=(SELECT Max(I.HSN) FROM Item I WHERE I.ItemCategory=Item.Code) Where Item.V_Type='IC'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
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
    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.LblDocNo.Text = "Item Name : " + Dgl1(Col1Value, rowSpecification).Value
        FrmObj.SearchCode = "Item-" + mSearchCode
        FrmObj.TableName = "SubGroupAttachments"
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()
        FrmObj.Dispose()
        FrmObj = Nothing
        SetAttachmentCaption()
    End Sub
    Private Sub SetAttachmentCaption()
        Dim AttachmentPath As String = PubAttachmentPath + "Item-" + mSearchCode + "\"
        If Directory.Exists(AttachmentPath) Then
            Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
            If FileCount > 0 Then BtnAttachments.Text = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else BtnAttachments.Text = "Attachments"
        Else
            BtnAttachments.Text = "Attachments"
        End If
    End Sub
    Private Sub DGLUnitConversion_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGLUnitConversion.EditingControl_KeyDown
        Try
            If DGLUnitConversion.CurrentCell Is Nothing Then Exit Sub
            Select Case DGLUnitConversion.Columns(DGLUnitConversion.CurrentCell.ColumnIndex).Name
                Case Col1FromUnit
                    If e.KeyCode <> Keys.Enter Then
                        If DGLUnitConversion.AgHelpDataSet(DGLUnitConversion.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = " SELECT Code, Code as Name  FROM Unit where IsActive=1 Order By Code "
                            DGLUnitConversion.AgHelpDataSet(DGLUnitConversion.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGLItemSubGroup_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGLItemSubgroup.EditingControl_KeyDown
        Try
            If DGLItemSubgroup.CurrentCell Is Nothing Then Exit Sub
            Select Case DGLItemSubgroup.Columns(DGLItemSubgroup.CurrentCell.ColumnIndex).Name
                Case Col1SubCode
                    If e.KeyCode <> Keys.Enter Then
                        If DGLItemSubgroup.AgHelpDataSet(DGLItemSubgroup.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = " SELECT Subcode As Code, Name as Name  FROM Subgroup Order By Name "
                            DGLItemSubgroup.AgHelpDataSet(DGLItemSubgroup.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FImportItem_SparePart()
        Dim mTrans As String = ""
        Dim DtDataFields As DataTable
        Dim DtTemp As DataTable
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, 'Item Code' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl, 'Item Name' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl, 'Item Category' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl, 'Unit' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl, 'Sale Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtDataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Item Master Import"
        ObjFrmImport.Dgl1.DataSource = DtDataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)


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


        If DtTemp.Columns.Contains("Unit") Then
            Dim DtUnit = DtTemp.DefaultView.ToTable(True, "Unit")
            For I = 0 To DtUnit.Rows.Count - 1
                If AgL.XNull(DtUnit.Rows(I)("Unit")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From Unit where Code = '" & AgL.XNull(DtUnit.Rows(I)("Unit")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                        If ErrorLog.Contains("These units are not present in master") = False Then
                            ErrorLog += vbCrLf & "These Unit Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtUnit.Rows(I)("Unit")) & ", "
                        Else
                            ErrorLog += AgL.XNull(DtUnit.Rows(I)("Unit")) & ", "
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
            If AgL.PubServerName = "" Then AgL.Dman_ExecuteNonQry("PRAGMA SYNCHRONOUS=OFF", AgL.GCn)

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            Dim bLastItemCategoryCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtItemCategory = DtTemp.DefaultView.ToTable(True, "Item Category")
            For I = 0 To DtItemCategory.Rows.Count - 1
                If AgL.XNull(DtItemCategory.Rows(I)("Item Category")) <> "" Then
                    Dim ItemCategoryTable As New StructItemCategory
                    Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemCategoryTable.Code = bItemCategoryCode
                    ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)("Item Category")).ToString.Trim
                    ItemCategoryTable.ItemType = "TP"
                    ItemCategoryTable.SalesTaxPostingGroup = ""
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


            Dim bLastItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            For I = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("Item Name")) <> "" Then
                    Dim ItemTable As New StructItem
                    Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemTable.Code = bItemCode
                    ItemTable.ManualCode = AgL.XNull(DtTemp.Rows(I)("Item Code")).ToString.Trim
                    ItemTable.Specification = AgL.XNull(DtTemp.Rows(I)("Item Name")).ToString.Trim
                    ItemTable.ItemGroupDesc = ""
                    ItemTable.ItemCategoryDesc = AgL.XNull(DtTemp.Rows(I)("Item Category")).ToString.Trim
                    ItemTable.Description = ItemTable.Specification + "-" + ItemTable.ItemCategoryDesc + "-" + ItemTable.ManualCode
                    ItemTable.DisplayName = ItemTable.Description
                    ItemTable.ItemType = "TP"
                    ItemTable.V_Type = mItemVTypes ' "ITEM"
                    ItemTable.Unit = AgL.XNull(DtTemp.Rows(I)("Unit")).ToString.Trim
                    ItemTable.PurchaseRate = 0
                    ItemTable.Rate = AgL.XNull(DtTemp.Rows(I)("Sale Rate")).ToString.Trim
                    ItemTable.SalesTaxPostingGroup = ""
                    ItemTable.HSN = ""
                    ItemTable.Remark = ""
                    ItemTable.Remark1 = ""
                    ItemTable.EntryBy = AgL.PubUserName
                    ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    ItemTable.EntryType = "Add"
                    ItemTable.EntryStatus = LogStatus.LogOpen
                    ItemTable.Div_Code = AgL.PubDivCode
                    ItemTable.Status = "Active"
                    ItemTable.StockYN = 1
                    ItemTable.IsSystemDefine = 0


                    If DtTemp.Columns.Contains("Base Item") Or DtTemp.Columns.Contains("BaseItem") Then
                        ItemTable.BaseItemDesc = AgL.XNull(DtTemp.Rows(I)("Base Item")).ToString.Trim
                    End If


                    ImportItemTable(ItemTable, True)

                    'mQry = "Update Item Set HSN=(SELECT Max(I.HSN) FROM Item I WHERE I.ItemCategory=Item.Code) Where Item.V_Type='IC'"
                    'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
            If AgL.PubServerName = "" Then AgL.Dman_ExecuteNonQry("PRAGMA SYNCHRONOUS=ON", AgL.GCn)
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub
    Private Sub FUpdateDimensionRates(Conn As Object, Cmd As Object)
        'If FDivisionNameForCustomization(15) = "MANISH TEXTILES" Then
        '    mQry = " Select Count(*) From Item Where BaseItem = '" & mSearchCode & "' And V_Type = '" & ItemV_Type.Dimension1 & "' And IfNull(Rate,0) <> " & Val(Dgl1.Item(Col1Value, rowSaleRate).Value) & "  "
        '    If AgL.VNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()) > 0 Then
        '        If MsgBox("Do you want to update design rates also ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '            mQry = " UPDATE Item SET Rate = " & Val(Dgl1.Item(Col1Value, rowSaleRate).Value) & " WHERE Code IN (SELECT I.Code FROM Item I WHERE I.BaseItem = '" & mSearchCode & "') "
        '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        '            mQry = "UPDATE RateListDetail SET Rate = " & Val(Dgl1.Item(Col1Value, rowSaleRate).Value) & " WHERE Code IN (SELECT I.Code FROM Item I WHERE I.BaseItem = '" & mSearchCode & "') "
        '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '        End If
        '    End If
        'End If
    End Sub
End Class
