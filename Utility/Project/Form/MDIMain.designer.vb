<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MnuMain = New System.Windows.Forms.MenuStrip
        Me.MnuUtility = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuStructure = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuStructureHeadMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuStructureMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuTaxRateMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuNCatMapping = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUser = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserPermission = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserControlPermission = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserVoucherTypeRestriction = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDatabase = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuBackup = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCustomFields = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCustomFieldHeadMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCustomFieldMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuVoucherType = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuVoucherTypeMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuVoucherTypePrintSetting = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuVoucherTypeSetting = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCompanyHierarchy = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCompanyMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSiteBranchMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDivisionMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuUtility})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(967, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuUtility
        '
        Me.MnuUtility.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuStructure, Me.MnuUser, Me.MnuDatabase, Me.MnuCustomFields, Me.MnuVoucherType, Me.MnuCompanyHierarchy})
        Me.MnuUtility.Name = "MnuUtility"
        Me.MnuUtility.Size = New System.Drawing.Size(50, 20)
        Me.MnuUtility.Text = "Utility"
        '
        'MnuStructure
        '
        Me.MnuStructure.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuStructureHeadMaster, Me.MnuStructureMaster, Me.MnuTaxRateMaster, Me.MnuNCatMapping})
        Me.MnuStructure.Name = "MnuStructure"
        Me.MnuStructure.Size = New System.Drawing.Size(180, 22)
        Me.MnuStructure.Text = "Structure"
        '
        'MnuStructureHeadMaster
        '
        Me.MnuStructureHeadMaster.Name = "MnuStructureHeadMaster"
        Me.MnuStructureHeadMaster.Size = New System.Drawing.Size(161, 22)
        Me.MnuStructureHeadMaster.Text = "Head Master"
        '
        'MnuStructureMaster
        '
        Me.MnuStructureMaster.Name = "MnuStructureMaster"
        Me.MnuStructureMaster.Size = New System.Drawing.Size(161, 22)
        Me.MnuStructureMaster.Text = "Structure Master"
        '
        'MnuTaxRateMaster
        '
        Me.MnuTaxRateMaster.Name = "MnuTaxRateMaster"
        Me.MnuTaxRateMaster.Size = New System.Drawing.Size(161, 22)
        Me.MnuTaxRateMaster.Text = "Tax Rate Master"
        '
        'MnuNCatMapping
        '
        Me.MnuNCatMapping.Name = "MnuNCatMapping"
        Me.MnuNCatMapping.Size = New System.Drawing.Size(161, 22)
        Me.MnuNCatMapping.Text = "NCat Mapping"
        '
        'MnuUser
        '
        Me.MnuUser.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuUserMaster, Me.MnuUserPermission, Me.MnuUserControlPermission, Me.MnuUserVoucherTypeRestriction})
        Me.MnuUser.Name = "MnuUser"
        Me.MnuUser.Size = New System.Drawing.Size(180, 22)
        Me.MnuUser.Text = "User"
        '
        'MnuUserMaster
        '
        Me.MnuUserMaster.Name = "MnuUserMaster"
        Me.MnuUserMaster.Size = New System.Drawing.Size(232, 22)
        Me.MnuUserMaster.Text = "User Master"
        '
        'MnuUserPermission
        '
        Me.MnuUserPermission.Name = "MnuUserPermission"
        Me.MnuUserPermission.Size = New System.Drawing.Size(232, 22)
        Me.MnuUserPermission.Text = "User Permission"
        '
        'MnuUserControlPermission
        '
        Me.MnuUserControlPermission.Name = "MnuUserControlPermission"
        Me.MnuUserControlPermission.Size = New System.Drawing.Size(232, 22)
        Me.MnuUserControlPermission.Text = "User Control Permission"
        '
        'MnuUserVoucherTypeRestriction
        '
        Me.MnuUserVoucherTypeRestriction.Name = "MnuUserVoucherTypeRestriction"
        Me.MnuUserVoucherTypeRestriction.Size = New System.Drawing.Size(232, 22)
        Me.MnuUserVoucherTypeRestriction.Text = "User Voucher Type Restriction"
        '
        'MnuDatabase
        '
        Me.MnuDatabase.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuBackup})
        Me.MnuDatabase.Name = "MnuDatabase"
        Me.MnuDatabase.Size = New System.Drawing.Size(180, 22)
        Me.MnuDatabase.Text = "Database"
        '
        'MnuBackup
        '
        Me.MnuBackup.Name = "MnuBackup"
        Me.MnuBackup.Size = New System.Drawing.Size(113, 22)
        Me.MnuBackup.Text = "Backup"
        '
        'MnuCustomFields
        '
        Me.MnuCustomFields.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuCustomFieldHeadMaster, Me.MnuCustomFieldMaster})
        Me.MnuCustomFields.Name = "MnuCustomFields"
        Me.MnuCustomFields.Size = New System.Drawing.Size(180, 22)
        Me.MnuCustomFields.Text = "Custom Fields"
        '
        'MnuCustomFieldHeadMaster
        '
        Me.MnuCustomFieldHeadMaster.Name = "MnuCustomFieldHeadMaster"
        Me.MnuCustomFieldHeadMaster.Size = New System.Drawing.Size(188, 22)
        Me.MnuCustomFieldHeadMaster.Text = "Head Master"
        '
        'MnuCustomFieldMaster
        '
        Me.MnuCustomFieldMaster.Name = "MnuCustomFieldMaster"
        Me.MnuCustomFieldMaster.Size = New System.Drawing.Size(188, 22)
        Me.MnuCustomFieldMaster.Text = "Custom Fields Master"
        '
        'MnuVoucherType
        '
        Me.MnuVoucherType.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuVoucherTypeMaster, Me.MnuVoucherTypePrintSetting, Me.MnuVoucherTypeSetting})
        Me.MnuVoucherType.Name = "MnuVoucherType"
        Me.MnuVoucherType.Size = New System.Drawing.Size(180, 22)
        Me.MnuVoucherType.Text = "Voucher Type"
        '
        'MnuVoucherTypeMaster
        '
        Me.MnuVoucherTypeMaster.Name = "MnuVoucherTypeMaster"
        Me.MnuVoucherTypeMaster.Size = New System.Drawing.Size(215, 22)
        Me.MnuVoucherTypeMaster.Text = "Voucher Type Master"
        '
        'MnuVoucherTypePrintSetting
        '
        Me.MnuVoucherTypePrintSetting.Name = "MnuVoucherTypePrintSetting"
        Me.MnuVoucherTypePrintSetting.Size = New System.Drawing.Size(215, 22)
        Me.MnuVoucherTypePrintSetting.Text = "Voucher Type Print Setting"
        '
        'MnuVoucherTypeSetting
        '
        Me.MnuVoucherTypeSetting.Name = "MnuVoucherTypeSetting"
        Me.MnuVoucherTypeSetting.Size = New System.Drawing.Size(215, 22)
        Me.MnuVoucherTypeSetting.Text = "Voucher Type Setting"
        '
        'MnuCompanyHierarchy
        '
        Me.MnuCompanyHierarchy.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuCompanyMaster, Me.MnuSiteBranchMaster, Me.MnuDivisionMaster})
        Me.MnuCompanyHierarchy.Name = "MnuCompanyHierarchy"
        Me.MnuCompanyHierarchy.Size = New System.Drawing.Size(180, 22)
        Me.MnuCompanyHierarchy.Text = "Company Hierarchy"
        '
        'MnuCompanyMaster
        '
        Me.MnuCompanyMaster.Name = "MnuCompanyMaster"
        Me.MnuCompanyMaster.Size = New System.Drawing.Size(180, 22)
        Me.MnuCompanyMaster.Text = "Company Master"
        '
        'MnuSiteBranchMaster
        '
        Me.MnuSiteBranchMaster.Name = "MnuSiteBranchMaster"
        Me.MnuSiteBranchMaster.Size = New System.Drawing.Size(180, 22)
        Me.MnuSiteBranchMaster.Text = "Site / Branch Master"
        '
        'MnuDivisionMaster
        '
        Me.MnuDivisionMaster.Name = "MnuDivisionMaster"
        Me.MnuDivisionMaster.Size = New System.Drawing.Size(180, 22)
        Me.MnuDivisionMaster.Text = "Division Master"
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(967, 586)
        Me.Controls.Add(Me.MnuMain)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MnuMain
        Me.Name = "MDIMain"
        Me.Text = "Utility"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuUtility As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuDatabase As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuStructure As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuUser As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuStructureHeadMaster As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuUserPermission As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuUserControlPermission As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStructureMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTaxRateMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUserMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuBackup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNCatMapping As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCustomFields As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCustomFieldHeadMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCustomFieldMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVoucherType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVoucherTypeMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVoucherTypePrintSetting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCompanyHierarchy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCompanyMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSiteBranchMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDivisionMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVoucherTypeSetting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUserVoucherTypeRestriction As System.Windows.Forms.ToolStripMenuItem

End Class
