<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFindOld
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GrdFind = New System.Windows.Forms.DataGridView
        Me.FindRightClkMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuFilterSame = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuFilterNotSame = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRemoveFilter = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSortAsc = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSortDesc = New System.Windows.Forms.ToolStripMenuItem
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.btnHelp = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.FindTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.GrdFind, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FindRightClkMenu.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrdFind
        '
        Me.GrdFind.AllowUserToAddRows = False
        Me.GrdFind.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GrdFind.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GrdFind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GrdFind.ContextMenuStrip = Me.FindRightClkMenu
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GrdFind.DefaultCellStyle = DataGridViewCellStyle2
        Me.GrdFind.Location = New System.Drawing.Point(0, 25)
        Me.GrdFind.Name = "GrdFind"
        Me.GrdFind.RowHeadersWidth = 18
        Me.GrdFind.RowTemplate.Height = 18
        Me.GrdFind.Size = New System.Drawing.Size(673, 376)
        Me.GrdFind.TabIndex = 0
        '
        'FindRightClkMenu
        '
        Me.FindRightClkMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFilterSame, Me.mnuFilterNotSame, Me.mnuRemoveFilter, Me.mnuSortAsc, Me.mnuSortDesc})
        Me.FindRightClkMenu.Name = "FindRightClkMenu"
        Me.FindRightClkMenu.Size = New System.Drawing.Size(200, 114)
        '
        'mnuFilterSame
        '
        Me.mnuFilterSame.Name = "mnuFilterSame"
        Me.mnuFilterSame.Size = New System.Drawing.Size(199, 22)
        Me.mnuFilterSame.Text = "Filter Similar Records"
        '
        'mnuFilterNotSame
        '
        Me.mnuFilterNotSame.Name = "mnuFilterNotSame"
        Me.mnuFilterNotSame.Size = New System.Drawing.Size(199, 22)
        Me.mnuFilterNotSame.Text = "Filter Dissimilar Records"
        '
        'mnuRemoveFilter
        '
        Me.mnuRemoveFilter.Name = "mnuRemoveFilter"
        Me.mnuRemoveFilter.Size = New System.Drawing.Size(199, 22)
        Me.mnuRemoveFilter.Text = "Remove All Filter"
        '
        'mnuSortAsc
        '
        Me.mnuSortAsc.Name = "mnuSortAsc"
        Me.mnuSortAsc.Size = New System.Drawing.Size(199, 22)
        Me.mnuSortAsc.Text = "Sort Ascending"
        '
        'mnuSortDesc
        '
        Me.mnuSortDesc.Name = "mnuSortDesc"
        Me.mnuSortDesc.Size = New System.Drawing.Size(199, 22)
        Me.mnuSortDesc.Text = "Sort Descending"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(0, -1)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(454, 23)
        Me.TextBox1.TabIndex = 1
        '
        'btnHelp
        '
        Me.btnHelp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.Location = New System.Drawing.Point(0, 406)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(75, 23)
        Me.btnHelp.TabIndex = 2
        Me.btnHelp.Text = "&?Help"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 278)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(270, 127)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(189, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Delete To Show All Records"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(236, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Ctrl+D To Sort Column Descending"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(226, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Ctrl+S To Sort Column Ascending"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(166, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Enter For <Select & Exit>"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Escape For <Cancel & Exit>"
        '
        'FindTimer
        '
        Me.FindTimer.Interval = 1000
        '
        'frmFind
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(673, 428)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.GrdFind)
        Me.Controls.Add(Me.TextBox1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmFind"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Find "
        CType(Me.GrdFind, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FindRightClkMenu.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FindRightClkMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuFilterSame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterNotSame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoveFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSortAsc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSortDesc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindTimer As System.Windows.Forms.Timer
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents GrdFind As System.Windows.Forms.DataGridView
End Class
