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
        Me.MnuMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserLoginPasswardChange = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuTransaction = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserPermission = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserControlPermission = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserTarget = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDataSynchronisation = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuReports = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserWiseEntryReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserWiseEntryTargetReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSystemControl = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuTableKeys = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuReportMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDatabaseManagement = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDataNotSynchronised = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuBackupDatabase = New System.Windows.Forms.ToolStripMenuItem
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
        Me.MnuUtility.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuMaster, Me.MnuTransaction, Me.MnuReports, Me.MnuSystemControl, Me.MnuDatabaseManagement, Me.MnuBackupDatabase})
        Me.MnuUtility.Name = "MnuUtility"
        Me.MnuUtility.Size = New System.Drawing.Size(46, 20)
        Me.MnuUtility.Text = "Utility"
        '
        'MnuMaster
        '
        Me.MnuMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuUserMaster, Me.MnuUserLoginPasswardChange})
        Me.MnuMaster.Name = "MnuMaster"
        Me.MnuMaster.Size = New System.Drawing.Size(196, 22)
        Me.MnuMaster.Text = "Master"
        '
        'MnuUserMaster
        '
        Me.MnuUserMaster.Name = "MnuUserMaster"
        Me.MnuUserMaster.Size = New System.Drawing.Size(224, 22)
        Me.MnuUserMaster.Text = "User Master"
        '
        'MnuUserLoginPasswardChange
        '
        Me.MnuUserLoginPasswardChange.Name = "MnuUserLoginPasswardChange"
        Me.MnuUserLoginPasswardChange.Size = New System.Drawing.Size(224, 22)
        Me.MnuUserLoginPasswardChange.Text = "User Login Password Change"
        '
        'MnuTransaction
        '
        Me.MnuTransaction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuUserPermission, Me.MnuUserControlPermission, Me.MnuUserTarget, Me.MnuDataSynchronisation})
        Me.MnuTransaction.Name = "MnuTransaction"
        Me.MnuTransaction.Size = New System.Drawing.Size(196, 22)
        Me.MnuTransaction.Text = "Transaction"
        '
        'MnuUserPermission
        '
        Me.MnuUserPermission.Name = "MnuUserPermission"
        Me.MnuUserPermission.Size = New System.Drawing.Size(198, 22)
        Me.MnuUserPermission.Text = "User Permission"
        '
        'MnuUserControlPermission
        '
        Me.MnuUserControlPermission.Name = "MnuUserControlPermission"
        Me.MnuUserControlPermission.Size = New System.Drawing.Size(198, 22)
        Me.MnuUserControlPermission.Text = "User Control Permission"
        '
        'MnuUserTarget
        '
        Me.MnuUserTarget.Name = "MnuUserTarget"
        Me.MnuUserTarget.Size = New System.Drawing.Size(198, 22)
        Me.MnuUserTarget.Text = "User Target"
        '
        'MnuDataSynchronisation
        '
        Me.MnuDataSynchronisation.Name = "MnuDataSynchronisation"
        Me.MnuDataSynchronisation.Size = New System.Drawing.Size(198, 22)
        Me.MnuDataSynchronisation.Text = "Data Synchronisation"
        '
        'MnuReports
        '
        Me.MnuReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuUserWiseEntryReport, Me.MnuUserWiseEntryTargetReport})
        Me.MnuReports.Name = "MnuReports"
        Me.MnuReports.Size = New System.Drawing.Size(196, 22)
        Me.MnuReports.Text = "Reports"
        '
        'MnuUserWiseEntryReport
        '
        Me.MnuUserWiseEntryReport.Name = "MnuUserWiseEntryReport"
        Me.MnuUserWiseEntryReport.Size = New System.Drawing.Size(233, 22)
        Me.MnuUserWiseEntryReport.Tag = "UTILITY"
        Me.MnuUserWiseEntryReport.Text = "User Wise Entry Report"
        '
        'MnuUserWiseEntryTargetReport
        '
        Me.MnuUserWiseEntryTargetReport.Name = "MnuUserWiseEntryTargetReport"
        Me.MnuUserWiseEntryTargetReport.Size = New System.Drawing.Size(233, 22)
        Me.MnuUserWiseEntryTargetReport.Tag = "UTILITY"
        Me.MnuUserWiseEntryTargetReport.Text = "User Wise Entry Target Report"
        '
        'MnuSystemControl
        '
        Me.MnuSystemControl.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuTableKeys, Me.MnuReportMaster})
        Me.MnuSystemControl.Name = "MnuSystemControl"
        Me.MnuSystemControl.Size = New System.Drawing.Size(196, 22)
        Me.MnuSystemControl.Text = "System Control"
        '
        'MnuTableKeys
        '
        Me.MnuTableKeys.Name = "MnuTableKeys"
        Me.MnuTableKeys.Size = New System.Drawing.Size(154, 22)
        Me.MnuTableKeys.Text = "Table Keys"
        '
        'MnuReportMaster
        '
        Me.MnuReportMaster.Name = "MnuReportMaster"
        Me.MnuReportMaster.Size = New System.Drawing.Size(154, 22)
        Me.MnuReportMaster.Text = "Report Master"
        '
        'MnuDatabaseManagement
        '
        Me.MnuDatabaseManagement.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuDataNotSynchronised})
        Me.MnuDatabaseManagement.Name = "MnuDatabaseManagement"
        Me.MnuDatabaseManagement.Size = New System.Drawing.Size(196, 22)
        Me.MnuDatabaseManagement.Text = "Database Management"
        '
        'MnuDataNotSynchronised
        '
        Me.MnuDataNotSynchronised.Name = "MnuDataNotSynchronised"
        Me.MnuDataNotSynchronised.Size = New System.Drawing.Size(195, 22)
        Me.MnuDataNotSynchronised.Text = "Data Not Synchronised"
        '
        'MnuBackupDatabase
        '
        Me.MnuBackupDatabase.Name = "MnuBackupDatabase"
        Me.MnuBackupDatabase.Size = New System.Drawing.Size(196, 22)
        Me.MnuBackupDatabase.Text = "Backup Database"
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
    Friend WithEvents MnuUserWiseEntryReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUserWiseEntryTargetReport As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuSystemControl As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuTableKeys As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuDataSynchronisation As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuDataNotSynchronised As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuReportMaster As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuBackupDatabase As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMaster As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuTransaction As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuUserMaster As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuUserPermission As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuUserControlPermission As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuUserTarget As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuReports As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuDatabaseManagement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUserLoginPasswardChange As System.Windows.Forms.ToolStripMenuItem

End Class
