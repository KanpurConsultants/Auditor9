<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEnvelopePrinting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEnvelopePrinting))
        Me.txtEnvelopeId = New System.Windows.Forms.TextBox
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.lblHead = New System.Windows.Forms.Label
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtESId = New System.Windows.Forms.TextBox
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtEnvelopeId
        '
        Me.txtEnvelopeId.Location = New System.Drawing.Point(400, 140)
        Me.txtEnvelopeId.Name = "txtEnvelopeId"
        Me.txtEnvelopeId.Size = New System.Drawing.Size(129, 20)
        Me.txtEnvelopeId.TabIndex = 12
        Me.txtEnvelopeId.Visible = False
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.lblHead)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(929, 54)
        Me.pnlTop.TabIndex = 13
        '
        'lblHead
        '
        Me.lblHead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHead.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHead.ForeColor = System.Drawing.Color.Black
        Me.lblHead.Location = New System.Drawing.Point(0, 0)
        Me.lblHead.Name = "lblHead"
        Me.lblHead.Size = New System.Drawing.Size(929, 54)
        Me.lblHead.TabIndex = 48
        Me.lblHead.Text = "ENVELOPE REPORT"
        Me.lblHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblHead, "ChequeWritterpro")
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
        Me.CrystalReportViewer1.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.CrystalReportViewer1, "EnvelopePrint")
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'txtESId
        '
        Me.txtESId.Location = New System.Drawing.Point(318, 166)
        Me.txtESId.Name = "txtESId"
        Me.txtESId.Size = New System.Drawing.Size(100, 20)
        Me.txtESId.TabIndex = 15
        Me.txtESId.Visible = False
        '
        'frmEnvelopePrinting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(929, 541)
        Me.Controls.Add(Me.txtESId)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.txtEnvelopeId)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEnvelopePrinting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.pnlTop.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtEnvelopeId As System.Windows.Forms.TextBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblHead As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents txtESId As System.Windows.Forms.TextBox
End Class
