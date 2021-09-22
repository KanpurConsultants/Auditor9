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
        Me.MnuCityList = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserReports = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserWiseEntryReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuUserWiseEntryTargetReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPurchaseReports = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMaterialReceiptRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPurchaseRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMaterialReturnRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSaleReports = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSaleOrderRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuAstrologyReports = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuAstrologyRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuArtisionReports = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuArtisionIssueRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuArtisionReceiveRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSaleRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuCityList, Me.MnuUserReports, Me.MnuPurchaseReports, Me.MnuSaleReports, Me.MnuAstrologyReports, Me.MnuArtisionReports})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(967, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuCityList
        '
        Me.MnuCityList.Name = "MnuCityList"
        Me.MnuCityList.Size = New System.Drawing.Size(57, 20)
        Me.MnuCityList.Tag = "REPORTS"
        Me.MnuCityList.Text = "City List"
        '
        'MnuUserReports
        '
        Me.MnuUserReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuUserWiseEntryReport, Me.MnuUserWiseEntryTargetReport})
        Me.MnuUserReports.Name = "MnuUserReports"
        Me.MnuUserReports.Size = New System.Drawing.Size(82, 20)
        Me.MnuUserReports.Text = "User Reports"
        '
        'MnuUserWiseEntryReport
        '
        Me.MnuUserWiseEntryReport.Name = "MnuUserWiseEntryReport"
        Me.MnuUserWiseEntryReport.Size = New System.Drawing.Size(233, 22)
        Me.MnuUserWiseEntryReport.Tag = "REPORTS"
        Me.MnuUserWiseEntryReport.Text = "User Wise Entry Report"
        '
        'MnuUserWiseEntryTargetReport
        '
        Me.MnuUserWiseEntryTargetReport.Name = "MnuUserWiseEntryTargetReport"
        Me.MnuUserWiseEntryTargetReport.Size = New System.Drawing.Size(233, 22)
        Me.MnuUserWiseEntryTargetReport.Tag = "REPORTS"
        Me.MnuUserWiseEntryTargetReport.Text = "User Wise Entry Target Report"
        '
        'MnuPurchaseReports
        '
        Me.MnuPurchaseReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuMaterialReceiptRegister, Me.MnuPurchaseRegister, Me.MnuMaterialReturnRegister})
        Me.MnuPurchaseReports.Name = "MnuPurchaseReports"
        Me.MnuPurchaseReports.Size = New System.Drawing.Size(104, 20)
        Me.MnuPurchaseReports.Text = "Purchase Reports"
        '
        'MnuMaterialReceiptRegister
        '
        Me.MnuMaterialReceiptRegister.Name = "MnuMaterialReceiptRegister"
        Me.MnuMaterialReceiptRegister.Size = New System.Drawing.Size(205, 22)
        Me.MnuMaterialReceiptRegister.Tag = "REPORTS"
        Me.MnuMaterialReceiptRegister.Text = "Material Receipt Register"
        '
        'MnuPurchaseRegister
        '
        Me.MnuPurchaseRegister.Name = "MnuPurchaseRegister"
        Me.MnuPurchaseRegister.Size = New System.Drawing.Size(205, 22)
        Me.MnuPurchaseRegister.Text = "Purchase Register"
        '
        'MnuMaterialReturnRegister
        '
        Me.MnuMaterialReturnRegister.Name = "MnuMaterialReturnRegister"
        Me.MnuMaterialReturnRegister.Size = New System.Drawing.Size(205, 22)
        Me.MnuMaterialReturnRegister.Text = "Material Return Register"
        '
        'MnuSaleReports
        '
        Me.MnuSaleReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuSaleOrderRegister, Me.MnuSaleRegister})
        Me.MnuSaleReports.Name = "MnuSaleReports"
        Me.MnuSaleReports.Size = New System.Drawing.Size(80, 20)
        Me.MnuSaleReports.Text = "Sale Reports"
        '
        'MnuSaleOrderRegister
        '
        Me.MnuSaleOrderRegister.Name = "MnuSaleOrderRegister"
        Me.MnuSaleOrderRegister.Size = New System.Drawing.Size(179, 22)
        Me.MnuSaleOrderRegister.Text = "Sale Order Register"
        '
        'MnuAstrologyReports
        '
        Me.MnuAstrologyReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuAstrologyRegister})
        Me.MnuAstrologyReports.Name = "MnuAstrologyReports"
        Me.MnuAstrologyReports.Size = New System.Drawing.Size(106, 20)
        Me.MnuAstrologyReports.Text = "Astrology Reports"
        '
        'MnuAstrologyRegister
        '
        Me.MnuAstrologyRegister.Name = "MnuAstrologyRegister"
        Me.MnuAstrologyRegister.Size = New System.Drawing.Size(174, 22)
        Me.MnuAstrologyRegister.Text = "Astrology Register"
        '
        'MnuArtisionReports
        '
        Me.MnuArtisionReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuArtisionIssueRegister, Me.MnuArtisionReceiveRegister})
        Me.MnuArtisionReports.Name = "MnuArtisionReports"
        Me.MnuArtisionReports.Size = New System.Drawing.Size(96, 20)
        Me.MnuArtisionReports.Text = "Artision Reports"
        '
        'MnuArtisionIssueRegister
        '
        Me.MnuArtisionIssueRegister.Name = "MnuArtisionIssueRegister"
        Me.MnuArtisionIssueRegister.Size = New System.Drawing.Size(205, 22)
        Me.MnuArtisionIssueRegister.Text = "Artision Issue Register"
        '
        'MnuArtisionReceiveRegister
        '
        Me.MnuArtisionReceiveRegister.Name = "MnuArtisionReceiveRegister"
        Me.MnuArtisionReceiveRegister.Size = New System.Drawing.Size(205, 22)
        Me.MnuArtisionReceiveRegister.Text = "Artision Receive Register"
        '
        'MnuSaleRegister
        '
        Me.MnuSaleRegister.Name = "MnuSaleRegister"
        Me.MnuSaleRegister.Size = New System.Drawing.Size(179, 22)
        Me.MnuSaleRegister.Text = "Sale Register"
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
        Me.Text = "Fleetman Reports"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCityList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUserReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUserWiseEntryReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUserWiseEntryTargetReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaterialReceiptRegister As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseRegister As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaterialReturnRegister As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaleReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaleOrderRegister As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAstrologyReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAstrologyRegister As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuArtisionReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuArtisionIssueRegister As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuArtisionReceiveRegister As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaleRegister As System.Windows.Forms.ToolStripMenuItem

End Class
