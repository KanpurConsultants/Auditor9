<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSaleInvoicePurchaseSummary
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
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pnl1.Location = New System.Drawing.Point(0, 0)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(780, 349)
        Me.Pnl1.TabIndex = 10
        '
        'FrmSaleInvoicePurchaseSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 349)
        Me.Controls.Add(Me.Pnl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmSaleInvoicePurchaseSummary"
        Me.Text = "Sale Invoice Purchase Summary"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
End Class
