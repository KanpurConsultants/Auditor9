<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVoucher
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
        Me.txtVoucherNo = New System.Windows.Forms.TextBox
        Me.txtCompanyName = New System.Windows.Forms.TextBox
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblHead = New System.Windows.Forms.Label
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pboxClose = New System.Windows.Forms.PictureBox
        Me.pboxRestore = New System.Windows.Forms.PictureBox
        Me.pboxMinimize = New System.Windows.Forms.PictureBox
        Me.pboxMaximize = New System.Windows.Forms.PictureBox
        Me.pnlTop.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.pboxClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxRestore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxMinimize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxMaximize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.Location = New System.Drawing.Point(400, 140)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.Size = New System.Drawing.Size(129, 20)
        Me.txtVoucherNo.TabIndex = 10
        Me.txtVoucherNo.Visible = False
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(557, 140)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(129, 20)
        Me.txtCompanyName.TabIndex = 11
        Me.txtCompanyName.Visible = False
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.Panel3)
        Me.pnlTop.Controls.Add(Me.btnExit)
        Me.pnlTop.Controls.Add(Me.lblHead)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(929, 54)
        Me.pnlTop.TabIndex = 12
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.exit_32
        Me.btnExit.Location = New System.Drawing.Point(0, 1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(74, 45)
        Me.btnExit.TabIndex = 50
        Me.btnExit.Text = " E&XIT"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ToolTip1.SetToolTip(Me.btnExit, "Exit Current Window")
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblHead
        '
        Me.lblHead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHead.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHead.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblHead.Location = New System.Drawing.Point(0, 0)
        Me.lblHead.Name = "lblHead"
        Me.lblHead.Size = New System.Drawing.Size(929, 54)
        Me.lblHead.TabIndex = 48
        Me.lblHead.Text = "Voucher Report"
        Me.lblHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblHead, "ChequeWritterPro")
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Top
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 54)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(929, 700)
        Me.CrystalReportViewer1.TabIndex = 13
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pboxClose)
        Me.Panel3.Controls.Add(Me.pboxRestore)
        Me.Panel3.Controls.Add(Me.pboxMinimize)
        Me.Panel3.Controls.Add(Me.pboxMaximize)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(836, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(93, 54)
        Me.Panel3.TabIndex = 51
        '
        'pboxClose
        '
        Me.pboxClose.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.CLOSE1
        Me.pboxClose.Location = New System.Drawing.Point(60, 10)
        Me.pboxClose.Name = "pboxClose"
        Me.pboxClose.Size = New System.Drawing.Size(24, 24)
        Me.pboxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxClose.TabIndex = 0
        Me.pboxClose.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxClose, "Close")
        '
        'pboxRestore
        '
        Me.pboxRestore.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.restore1
        Me.pboxRestore.Location = New System.Drawing.Point(34, 10)
        Me.pboxRestore.Name = "pboxRestore"
        Me.pboxRestore.Size = New System.Drawing.Size(24, 24)
        Me.pboxRestore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxRestore.TabIndex = 0
        Me.pboxRestore.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxRestore, "Restore")
        '
        'pboxMinimize
        '
        Me.pboxMinimize.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.minimize1
        Me.pboxMinimize.Location = New System.Drawing.Point(8, 10)
        Me.pboxMinimize.Name = "pboxMinimize"
        Me.pboxMinimize.Size = New System.Drawing.Size(24, 24)
        Me.pboxMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxMinimize.TabIndex = 0
        Me.pboxMinimize.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxMinimize, "Minimize")
        '
        'pboxMaximize
        '
        Me.pboxMaximize.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.maximize1
        Me.pboxMaximize.Location = New System.Drawing.Point(34, 10)
        Me.pboxMaximize.Name = "pboxMaximize"
        Me.pboxMaximize.Size = New System.Drawing.Size(24, 24)
        Me.pboxMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxMaximize.TabIndex = 0
        Me.pboxMaximize.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxMaximize, "Maximize")
        '
        'frmVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(929, 407)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.txtCompanyName)
        Me.Controls.Add(Me.txtVoucherNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmVoucher"
        Me.Text = "Voucher Print"
        Me.pnlTop.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.pboxClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxRestore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxMinimize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxMaximize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblHead As System.Windows.Forms.Label
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pboxClose As System.Windows.Forms.PictureBox
    Friend WithEvents pboxRestore As System.Windows.Forms.PictureBox
    Friend WithEvents pboxMinimize As System.Windows.Forms.PictureBox
    Friend WithEvents pboxMaximize As System.Windows.Forms.PictureBox
End Class
