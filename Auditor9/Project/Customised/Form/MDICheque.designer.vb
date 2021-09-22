<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MDICheque
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
        Me.MnuMain = New System.Windows.Forms.MenuStrip()
        Me.MnuChequeCompanyMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChequeBankMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChequePrintCheque = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChequeReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChequeBackupData = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuChequeCompanyMaster, Me.MnuChequeBankMaster, Me.MnuChequePrintCheque, Me.MnuChequeReport, Me.MnuChequeBackupData})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(1370, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuChequeCompanyMaster
        '
        Me.MnuChequeCompanyMaster.Name = "MnuChequeCompanyMaster"
        Me.MnuChequeCompanyMaster.Size = New System.Drawing.Size(110, 20)
        Me.MnuChequeCompanyMaster.Text = "Company Master"
        '
        'MnuChequeBankMaster
        '
        Me.MnuChequeBankMaster.Name = "MnuChequeBankMaster"
        Me.MnuChequeBankMaster.Size = New System.Drawing.Size(84, 20)
        Me.MnuChequeBankMaster.Text = "Bank Master"
        '
        'MnuChequePrintCheque
        '
        Me.MnuChequePrintCheque.Name = "MnuChequePrintCheque"
        Me.MnuChequePrintCheque.Size = New System.Drawing.Size(88, 20)
        Me.MnuChequePrintCheque.Text = "Print Cheque"
        '
        'MnuChequeReport
        '
        Me.MnuChequeReport.Name = "MnuChequeReport"
        Me.MnuChequeReport.Size = New System.Drawing.Size(98, 20)
        Me.MnuChequeReport.Tag = "GRID REPORT"
        Me.MnuChequeReport.Text = "Cheque Report"
        '
        'MnuChequeBackupData
        '
        Me.MnuChequeBackupData.Name = "MnuChequeBackupData"
        Me.MnuChequeBackupData.Size = New System.Drawing.Size(85, 20)
        Me.MnuChequeBackupData.Text = "Backup Data"
        '
        'MDICheque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1370, 749)
        Me.Controls.Add(Me.MnuMain)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MnuMain
        Me.Name = "MDICheque"
        Me.Text = "Customise"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuSaleInvoiceK As ToolStripMenuItem
    Friend WithEvents MnuChequeCompanyMaster As ToolStripMenuItem
    Friend WithEvents MnuChequeBankMaster As ToolStripMenuItem
    Friend WithEvents MnuChequePrintCheque As ToolStripMenuItem
    Friend WithEvents MnuChequeBackupData As ToolStripMenuItem
    Friend WithEvents MnuChequeReport As ToolStripMenuItem
End Class
