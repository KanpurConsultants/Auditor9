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
        Me.MnuStructureManagement = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuStructureMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuStructureAcPosting = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuChargesMasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuTaxManagement = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuNCatMapping = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuUtility})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(804, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuUtility
        '
        Me.MnuUtility.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuStructureManagement})
        Me.MnuUtility.Name = "MnuUtility"
        Me.MnuUtility.Size = New System.Drawing.Size(47, 20)
        Me.MnuUtility.Text = "Setup"
        '
        'MnuStructureManagement
        '
        Me.MnuStructureManagement.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuStructureMaster, Me.MnuStructureAcPosting, Me.MnuChargesMasterToolStripMenuItem, Me.MnuTaxManagement, Me.MnuNCatMapping})
        Me.MnuStructureManagement.Name = "MnuStructureManagement"
        Me.MnuStructureManagement.Size = New System.Drawing.Size(152, 22)
        Me.MnuStructureManagement.Text = "Structure Setup"
        '
        'MnuStructureMaster
        '
        Me.MnuStructureMaster.Name = "MnuStructureMaster"
        Me.MnuStructureMaster.Size = New System.Drawing.Size(169, 22)
        Me.MnuStructureMaster.Text = "Structure Master"
        '
        'MnuStructureAcPosting
        '
        Me.MnuStructureAcPosting.Name = "MnuStructureAcPosting"
        Me.MnuStructureAcPosting.Size = New System.Drawing.Size(169, 22)
        Me.MnuStructureAcPosting.Text = "Structure AcPosting"
        '
        'MnuChargesMasterToolStripMenuItem
        '
        Me.MnuChargesMasterToolStripMenuItem.Name = "MnuChargesMasterToolStripMenuItem"
        Me.MnuChargesMasterToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.MnuChargesMasterToolStripMenuItem.Text = "Charges Master"
        '
        'MnuTaxManagement
        '
        Me.MnuTaxManagement.Name = "MnuTaxManagement"
        Me.MnuTaxManagement.Size = New System.Drawing.Size(169, 22)
        Me.MnuTaxManagement.Text = "Tax Management"
        '
        'MnuNCatMapping
        '
        Me.MnuNCatMapping.Name = "MnuNCatMapping"
        Me.MnuNCatMapping.Size = New System.Drawing.Size(169, 22)
        Me.MnuNCatMapping.Text = "NCat Mapping"
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(804, 578)
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
    Friend WithEvents MnuStructureManagement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStructureMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuChargesMasterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStructureAcPosting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTaxManagement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNCatMapping As System.Windows.Forms.ToolStripMenuItem

End Class
