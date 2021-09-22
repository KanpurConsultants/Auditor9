<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHelpGrid
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmHelpGrid))
        Me.FGMain = New System.Windows.Forms.DataGridView
        Me.TxtSearch = New System.Windows.Forms.Label
        Me.BtnClose = New System.Windows.Forms.Button
        Me.PnlMain = New System.Windows.Forms.Panel
        Me.BtnOk = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.TSMIMain = New System.Windows.Forms.ToolStripMenuItem
        Me.TSMHold = New System.Windows.Forms.ToolStripMenuItem
        Me.TSMRelease = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.FGMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlMain.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FGMain
        '
        Me.FGMain.AllowUserToAddRows = False
        Me.FGMain.AllowUserToDeleteRows = False
        Me.FGMain.AllowUserToResizeRows = False
        Me.FGMain.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FGMain.BackgroundColor = System.Drawing.Color.White
        Me.FGMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FGMain.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.FGMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FGMain.GridColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.FGMain.Location = New System.Drawing.Point(1, 26)
        Me.FGMain.MultiSelect = False
        Me.FGMain.Name = "FGMain"
        Me.FGMain.ReadOnly = True
        Me.FGMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.FGMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.FGMain.Size = New System.Drawing.Size(552, 385)
        Me.FGMain.TabIndex = 0
        '
        'TxtSearch
        '
        Me.TxtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSearch.BackColor = System.Drawing.Color.White
        Me.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TxtSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.ForeColor = System.Drawing.Color.Black
        Me.TxtSearch.Location = New System.Drawing.Point(2, 2)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(551, 23)
        Me.TxtSearch.TabIndex = 5
        Me.TxtSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(499, 414)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(52, 24)
        Me.BtnClose.TabIndex = 10
        Me.BtnClose.Text = "Clos&e"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'PnlMain
        '
        Me.PnlMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMain.BackColor = System.Drawing.Color.Gainsboro
        Me.PnlMain.Controls.Add(Me.BtnOk)
        Me.PnlMain.Controls.Add(Me.MenuStrip1)
        Me.PnlMain.Location = New System.Drawing.Point(2, 412)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(551, 28)
        Me.PnlMain.TabIndex = 11
        '
        'BtnOk
        '
        Me.BtnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOk.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(443, 2)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(52, 24)
        Me.BtnOk.TabIndex = 12
        Me.BtnOk.Text = "O&k"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMIMain})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.MdiWindowListItem = Me.TSMIMain
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(551, 24)
        Me.MenuStrip1.TabIndex = 16
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TSMIMain
        '
        Me.TSMIMain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSMIMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMHold, Me.TSMRelease})
        Me.TSMIMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TSMIMain.Image = CType(resources.GetObject("TSMIMain.Image"), System.Drawing.Image)
        Me.TSMIMain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TSMIMain.Name = "TSMIMain"
        Me.TSMIMain.Size = New System.Drawing.Size(28, 20)
        Me.TSMIMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TSMHold
        '
        Me.TSMHold.Name = "TSMHold"
        Me.TSMHold.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.TSMHold.Size = New System.Drawing.Size(190, 22)
        Me.TSMHold.Text = "Hold Filter"
        '
        'TSMRelease
        '
        Me.TSMRelease.Name = "TSMRelease"
        Me.TSMRelease.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.TSMRelease.Size = New System.Drawing.Size(190, 22)
        Me.TSMRelease.Text = "Release Filter"
        '
        'FrmHelpGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(554, 442)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.FGMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmHelpGrid"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Help Grid"
        CType(Me.FGMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlMain.ResumeLayout(False)
        Me.PnlMain.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FGMain As System.Windows.Forms.DataGridView
    Friend WithEvents TxtSearch As System.Windows.Forms.Label
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents TSMIMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMHold As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMRelease As System.Windows.Forms.ToolStripMenuItem

End Class
