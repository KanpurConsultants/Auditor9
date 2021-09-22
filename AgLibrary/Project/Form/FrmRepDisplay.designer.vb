<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRepDisplay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Public Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.MnuMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuVisible = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSort = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuGroupOn = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuExportToExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPreview = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEMail = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSaveSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSaveDisplaySettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSaveSortingSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSaveFilterSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.BtnFilter = New System.Windows.Forms.Button()
        Me.BtnFill = New System.Windows.Forms.Button()
        Me.Pnl3 = New System.Windows.Forms.Panel()
        Me.TxtFind = New System.Windows.Forms.TextBox()
        Me.MnuCustomOption = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BtnCustomMenu = New System.Windows.Forms.Button()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSaveForEveryoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BtnProceed = New System.Windows.Forms.Button()
        Me.PnlFilterDisplay = New System.Windows.Forms.Panel()
        Me.MnuMain.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.AutoSize = True
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(0, 31)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(982, 567)
        Me.Pnl1.TabIndex = 1
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl2.AutoSize = True
        Me.Pnl2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl2.Location = New System.Drawing.Point(0, 599)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(982, 22)
        Me.Pnl2.TabIndex = 3
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuVisible, Me.MnuSort, Me.MnuFilter, Me.MnuGroupOn, Me.MnuExportToExcel, Me.MnuPreview, Me.MnuEMail, Me.MnuSaveSettings})
        Me.MnuMain.Name = "CMSMain"
        Me.MnuMain.ShowImageMargin = False
        Me.MnuMain.Size = New System.Drawing.Size(128, 202)
        '
        'MnuVisible
        '
        Me.MnuVisible.Name = "MnuVisible"
        Me.MnuVisible.Size = New System.Drawing.Size(127, 22)
        Me.MnuVisible.Text = "Visible"
        Me.MnuVisible.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'MnuSort
        '
        Me.MnuSort.Name = "MnuSort"
        Me.MnuSort.Size = New System.Drawing.Size(127, 22)
        Me.MnuSort.Text = "Sort"
        '
        'MnuFilter
        '
        Me.MnuFilter.Name = "MnuFilter"
        Me.MnuFilter.Size = New System.Drawing.Size(127, 22)
        Me.MnuFilter.Text = "Filter"
        '
        'MnuGroupOn
        '
        Me.MnuGroupOn.Name = "MnuGroupOn"
        Me.MnuGroupOn.Size = New System.Drawing.Size(127, 22)
        Me.MnuGroupOn.Text = "Group On"
        Me.MnuGroupOn.Visible = False
        '
        'MnuExportToExcel
        '
        Me.MnuExportToExcel.Name = "MnuExportToExcel"
        Me.MnuExportToExcel.Size = New System.Drawing.Size(127, 22)
        Me.MnuExportToExcel.Text = "Export To Excel"
        '
        'MnuPreview
        '
        Me.MnuPreview.Name = "MnuPreview"
        Me.MnuPreview.Size = New System.Drawing.Size(127, 22)
        Me.MnuPreview.Text = "Preview"
        '
        'MnuEMail
        '
        Me.MnuEMail.Name = "MnuEMail"
        Me.MnuEMail.Size = New System.Drawing.Size(127, 22)
        Me.MnuEMail.Text = "E-Mail"
        '
        'MnuSaveSettings
        '
        Me.MnuSaveSettings.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuSaveDisplaySettings, Me.MnuSaveSortingSettings, Me.MnuSaveFilterSettings})
        Me.MnuSaveSettings.Name = "MnuSaveSettings"
        Me.MnuSaveSettings.Size = New System.Drawing.Size(127, 22)
        Me.MnuSaveSettings.Text = "Save Settings"
        '
        'MnuSaveDisplaySettings
        '
        Me.MnuSaveDisplaySettings.Name = "MnuSaveDisplaySettings"
        Me.MnuSaveDisplaySettings.Size = New System.Drawing.Size(157, 22)
        Me.MnuSaveDisplaySettings.Text = "Display Settings"
        '
        'MnuSaveSortingSettings
        '
        Me.MnuSaveSortingSettings.Name = "MnuSaveSortingSettings"
        Me.MnuSaveSortingSettings.Size = New System.Drawing.Size(157, 22)
        Me.MnuSaveSortingSettings.Text = "Sorting Settings"
        '
        'MnuSaveFilterSettings
        '
        Me.MnuSaveFilterSettings.Name = "MnuSaveFilterSettings"
        Me.MnuSaveFilterSettings.Size = New System.Drawing.Size(157, 22)
        Me.MnuSaveFilterSettings.Text = "Filter Settings"
        Me.MnuSaveFilterSettings.Visible = False
        '
        'BtnFilter
        '
        Me.BtnFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFilter.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFilter.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFilter.ForeColor = System.Drawing.Color.White
        Me.BtnFilter.Location = New System.Drawing.Point(817, 3)
        Me.BtnFilter.Name = "BtnFilter"
        Me.BtnFilter.Size = New System.Drawing.Size(75, 27)
        Me.BtnFilter.TabIndex = 4
        Me.BtnFilter.Text = "Filters"
        Me.BtnFilter.UseVisualStyleBackColor = False
        '
        'BtnFill
        '
        Me.BtnFill.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFill.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.ForeColor = System.Drawing.Color.White
        Me.BtnFill.Location = New System.Drawing.Point(891, 3)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(75, 27)
        Me.BtnFill.TabIndex = 5
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = False
        '
        'Pnl3
        '
        Me.Pnl3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl3.AutoSize = True
        Me.Pnl3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl3.Location = New System.Drawing.Point(0, 32)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(977, 0)
        Me.Pnl3.TabIndex = 6
        '
        'TxtFind
        '
        Me.TxtFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtFind.Enabled = False
        Me.TxtFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFind.Location = New System.Drawing.Point(520, 4)
        Me.TxtFind.Name = "TxtFind"
        Me.TxtFind.Size = New System.Drawing.Size(224, 26)
        Me.TxtFind.TabIndex = 7
        Me.TxtFind.Visible = False
        '
        'MnuCustomOption
        '
        Me.MnuCustomOption.Name = "MnuCustomOption"
        Me.MnuCustomOption.Size = New System.Drawing.Size(61, 4)
        '
        'BtnCustomMenu
        '
        Me.BtnCustomMenu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCustomMenu.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnCustomMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCustomMenu.Font = New System.Drawing.Font("Wingdings 3", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnCustomMenu.ForeColor = System.Drawing.Color.White
        Me.BtnCustomMenu.Location = New System.Drawing.Point(962, 3)
        Me.BtnCustomMenu.Name = "BtnCustomMenu"
        Me.BtnCustomMenu.Size = New System.Drawing.Size(20, 27)
        Me.BtnCustomMenu.TabIndex = 9
        Me.BtnCustomMenu.Text = "¤"
        Me.BtnCustomMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCustomMenu.UseVisualStyleBackColor = False
        Me.BtnCustomMenu.Visible = False
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuSave, Me.MnuSaveForEveryoneToolStripMenuItem})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(170, 48)
        '
        'MnuSave
        '
        Me.MnuSave.Name = "MnuSave"
        Me.MnuSave.Size = New System.Drawing.Size(169, 22)
        Me.MnuSave.Text = "Save"
        '
        'MnuSaveForEveryoneToolStripMenuItem
        '
        Me.MnuSaveForEveryoneToolStripMenuItem.Name = "MnuSaveForEveryoneToolStripMenuItem"
        Me.MnuSaveForEveryoneToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.MnuSaveForEveryoneToolStripMenuItem.Text = "Save For Everyone"
        '
        'BtnProceed
        '
        Me.BtnProceed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnProceed.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnProceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnProceed.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnProceed.ForeColor = System.Drawing.Color.White
        Me.BtnProceed.Location = New System.Drawing.Point(745, 3)
        Me.BtnProceed.Name = "BtnProceed"
        Me.BtnProceed.Size = New System.Drawing.Size(75, 27)
        Me.BtnProceed.TabIndex = 10
        Me.BtnProceed.Text = "Proceed"
        Me.BtnProceed.UseVisualStyleBackColor = False
        Me.BtnProceed.Visible = False
        '
        'PnlFilterDisplay
        '
        Me.PnlFilterDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlFilterDisplay.Location = New System.Drawing.Point(1, 0)
        Me.PnlFilterDisplay.Name = "PnlFilterDisplay"
        Me.PnlFilterDisplay.Size = New System.Drawing.Size(514, 31)
        Me.PnlFilterDisplay.TabIndex = 11
        '
        'FrmRepDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(982, 623)
        Me.Controls.Add(Me.PnlFilterDisplay)
        Me.Controls.Add(Me.BtnProceed)
        Me.Controls.Add(Me.BtnCustomMenu)
        Me.Controls.Add(Me.TxtFind)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.BtnFill)
        Me.Controls.Add(Me.BtnFilter)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.Pnl1)
        Me.KeyPreview = True
        Me.Name = "FrmRepDisplay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report View"
        Me.MnuMain.ResumeLayout(False)
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents Pnl2 As System.Windows.Forms.Panel
    Friend WithEvents MnuSaveDisplaySettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaveSortingSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaveFilterSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtnFilter As Button
    Friend WithEvents BtnFill As Button
    Friend WithEvents TxtFind As TextBox
    Public WithEvents Pnl3 As Panel
    Public WithEvents BtnCustomMenu As Button
    Public WithEvents MnuMain As ContextMenuStrip
    Public WithEvents MnuVisible As ToolStripMenuItem
    Public WithEvents MnuSort As ToolStripMenuItem
    Public WithEvents MnuFilter As ToolStripMenuItem
    Public WithEvents MnuGroupOn As ToolStripMenuItem
    Public WithEvents MnuExportToExcel As ToolStripMenuItem
    Public WithEvents MnuSaveSettings As ToolStripMenuItem
    Public WithEvents MnuPreview As ToolStripMenuItem
    Friend WithEvents MnuEMail As ToolStripMenuItem
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuSave As ToolStripMenuItem
    Friend WithEvents MnuSaveForEveryoneToolStripMenuItem As ToolStripMenuItem
    Public WithEvents BtnProceed As Button
    Friend WithEvents PnlFilterDisplay As Panel
    Public WithEvents MnuCustomOption As ContextMenuStrip
End Class
