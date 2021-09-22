<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFind
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
    Private Sub InitializeComponent()
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
        Me.MnuSaveSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSaveDisplaySettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSaveSortingSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSaveFilterSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.TxtFind = New System.Windows.Forms.TextBox()
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(0, 27)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(982, 359)
        Me.Pnl1.TabIndex = 1
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl2.Location = New System.Drawing.Point(0, 387)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(982, 24)
        Me.Pnl2.TabIndex = 3
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuVisible, Me.MnuSort, Me.MnuFilter, Me.MnuGroupOn, Me.MnuExportToExcel, Me.MnuPreview, Me.MnuSaveSettings})
        Me.MnuMain.Name = "CMSMain"
        Me.MnuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MnuMain.ShowImageMargin = False
        Me.MnuMain.Size = New System.Drawing.Size(128, 158)
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
        '
        'TxtFind
        '
        Me.TxtFind.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtFind.Enabled = False
        Me.TxtFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFind.Location = New System.Drawing.Point(0, 1)
        Me.TxtFind.Name = "TxtFind"
        Me.TxtFind.Size = New System.Drawing.Size(981, 26)
        Me.TxtFind.TabIndex = 8
        Me.TxtFind.Visible = False
        '
        'FrmFind
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 411)
        Me.Controls.Add(Me.TxtFind)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.Pnl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmFind"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Find"
        Me.MnuMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents Pnl2 As System.Windows.Forms.Panel
    Friend WithEvents MnuMain As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MnuVisible As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSort As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuGroupOn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuExportToExcel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaveSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaveDisplaySettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaveSortingSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaveFilterSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPreview As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TxtFind As TextBox
End Class
