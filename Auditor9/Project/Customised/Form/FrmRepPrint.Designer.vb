<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRepPrint
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.reportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.SuspendLayout()
        '
        'reportViewer1
        '
        Me.reportViewer1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.reportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.reportViewer1.Name = "reportViewer1"
        Me.reportViewer1.Size = New System.Drawing.Size(998, 662)
        Me.reportViewer1.TabIndex = 3
        Me.reportViewer1.Dock = DockStyle.Fill
        Me.reportViewer1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        '
        'FrmReportPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 662)
        Me.Controls.Add(Me.reportViewer1)
        Me.Name = "FrmReportPrint"
        Me.Text = "FrmReportPrint"
        Me.ResumeLayout(False)
        Me.KeyPreview = True
    End Sub
    Public WithEvents reportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
