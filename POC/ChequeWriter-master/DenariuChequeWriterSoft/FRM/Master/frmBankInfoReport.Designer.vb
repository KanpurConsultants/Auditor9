<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBankInfoReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBankInfoReport))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlContent = New System.Windows.Forms.Panel
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.pnlFooter = New System.Windows.Forms.Panel
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.pnlToolBar = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.pnlone = New System.Windows.Forms.Panel
        Me.rbnInfixCompany = New System.Windows.Forms.RadioButton
        Me.rbnSuffixCompany = New System.Windows.Forms.RadioButton
        Me.rbnPrefixCompany = New System.Windows.Forms.RadioButton
        Me.txtBankName = New System.Windows.Forms.TextBox
        Me.lblBankName = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lblHeading = New System.Windows.Forms.Label
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlone.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1022, 516)
        Me.pnlMain.TabIndex = 18
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.CrystalReportViewer1)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 123)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(1022, 383)
        Me.pnlContent.TabIndex = 0
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1022, 383)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'pnlFooter
        '
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 506)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(1022, 10)
        Me.pnlFooter.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.AutoScroll = True
        Me.pnlHeader.Controls.Add(Me.pnlToolBar)
        Me.pnlHeader.Controls.Add(Me.lblHeading)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1022, 123)
        Me.pnlHeader.TabIndex = 0
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Controls.Add(Me.btnExit)
        Me.pnlToolBar.Controls.Add(Me.Panel6)
        Me.pnlToolBar.Controls.Add(Me.btnSearch)
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 42)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(1019, 81)
        Me.pnlToolBar.TabIndex = 21
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.exit_32
        Me.btnExit.Location = New System.Drawing.Point(700, 18)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(94, 39)
        Me.btnExit.TabIndex = 81
        Me.btnExit.Text = " E&XIT"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.pnlone)
        Me.Panel6.Controls.Add(Me.txtBankName)
        Me.Panel6.Controls.Add(Me.lblBankName)
        Me.Panel6.Location = New System.Drawing.Point(229, 19)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(348, 43)
        Me.Panel6.TabIndex = 79
        '
        'pnlone
        '
        Me.pnlone.Controls.Add(Me.rbnInfixCompany)
        Me.pnlone.Controls.Add(Me.rbnSuffixCompany)
        Me.pnlone.Controls.Add(Me.rbnPrefixCompany)
        Me.pnlone.Location = New System.Drawing.Point(131, -1)
        Me.pnlone.Name = "pnlone"
        Me.pnlone.Size = New System.Drawing.Size(182, 20)
        Me.pnlone.TabIndex = 25
        '
        'rbnInfixCompany
        '
        Me.rbnInfixCompany.AutoSize = True
        Me.rbnInfixCompany.Checked = True
        Me.rbnInfixCompany.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbnInfixCompany.Location = New System.Drawing.Point(72, 0)
        Me.rbnInfixCompany.Name = "rbnInfixCompany"
        Me.rbnInfixCompany.Size = New System.Drawing.Size(48, 18)
        Me.rbnInfixCompany.TabIndex = 13
        Me.rbnInfixCompany.TabStop = True
        Me.rbnInfixCompany.Text = "Infix"
        Me.rbnInfixCompany.UseVisualStyleBackColor = True
        '
        'rbnSuffixCompany
        '
        Me.rbnSuffixCompany.AutoSize = True
        Me.rbnSuffixCompany.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbnSuffixCompany.Location = New System.Drawing.Point(124, 0)
        Me.rbnSuffixCompany.Name = "rbnSuffixCompany"
        Me.rbnSuffixCompany.Size = New System.Drawing.Size(56, 18)
        Me.rbnSuffixCompany.TabIndex = 14
        Me.rbnSuffixCompany.Text = "Suffix"
        Me.rbnSuffixCompany.UseVisualStyleBackColor = True
        '
        'rbnPrefixCompany
        '
        Me.rbnPrefixCompany.AutoSize = True
        Me.rbnPrefixCompany.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbnPrefixCompany.Location = New System.Drawing.Point(0, 0)
        Me.rbnPrefixCompany.Name = "rbnPrefixCompany"
        Me.rbnPrefixCompany.Size = New System.Drawing.Size(57, 18)
        Me.rbnPrefixCompany.TabIndex = 12
        Me.rbnPrefixCompany.Text = "Prefix"
        Me.rbnPrefixCompany.UseVisualStyleBackColor = True
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(3, 19)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(333, 20)
        Me.txtBankName.TabIndex = 11
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(3, 3)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(81, 16)
        Me.lblBankName.TabIndex = 10
        Me.lblBankName.Text = "Bank Name"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.search_32
        Me.btnSearch.Location = New System.Drawing.Point(598, 18)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(94, 39)
        Me.btnSearch.TabIndex = 69
        Me.btnSearch.Text = "SEARC&H"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'lblHeading
        '
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.Location = New System.Drawing.Point(0, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(1022, 42)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "BANK INFO REPORT"
        Me.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmBankInfoReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1022, 516)
        Me.Controls.Add(Me.pnlMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBankInfoReport"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlToolBar.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.pnlone.ResumeLayout(False)
        Me.pnlone.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents pnlone As System.Windows.Forms.Panel
    Friend WithEvents rbnInfixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSuffixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPrefixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
End Class
