<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmItemMerging
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmItemMerging))
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalPcsText = New System.Windows.Forms.Label()
        Me.BtnMerge = New System.Windows.Forms.Button()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuRollBackMerge = New System.Windows.Forms.ToolStripMenuItem()
        Me.PnlTotals.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(0, 70)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(855, 346)
        Me.Pnl1.TabIndex = 10
        '
        'PnlTotals
        '
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalPcsText)
        Me.PnlTotals.Location = New System.Drawing.Point(1, 0)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(856, 69)
        Me.PnlTotals.TabIndex = 696
        '
        'LblTotalPcsText
        '
        Me.LblTotalPcsText.AutoSize = True
        Me.LblTotalPcsText.Font = New System.Drawing.Font("Verdana", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPcsText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalPcsText.Location = New System.Drawing.Point(1, 13)
        Me.LblTotalPcsText.Name = "LblTotalPcsText"
        Me.LblTotalPcsText.Size = New System.Drawing.Size(852, 32)
        Me.LblTotalPcsText.TabIndex = 663
        Me.LblTotalPcsText.Text = resources.GetString("LblTotalPcsText.Text")
        '
        'BtnMerge
        '
        Me.BtnMerge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMerge.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMerge.Location = New System.Drawing.Point(1, 417)
        Me.BtnMerge.Name = "BtnMerge"
        Me.BtnMerge.Size = New System.Drawing.Size(856, 23)
        Me.BtnMerge.TabIndex = 697
        Me.BtnMerge.Text = "Merge"
        Me.BtnMerge.UseVisualStyleBackColor = True
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuRollBackMerge})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(160, 26)
        Me.MnuOptions.Text = "Option"
        '
        'MnuRollBackMerge
        '
        Me.MnuRollBackMerge.Name = "MnuRollBackMerge"
        Me.MnuRollBackMerge.Size = New System.Drawing.Size(159, 22)
        Me.MnuRollBackMerge.Text = "Roll Back Merge"
        '
        'FrmItemMerging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 439)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.BtnMerge)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "FrmItemMerging"
        Me.Text = "Master Merging"
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalPcsText As Label
    Friend WithEvents BtnMerge As Button
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuRollBackMerge As ToolStripMenuItem
End Class
