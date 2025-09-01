<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSendWhatsappMessage
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
        Me.BtnSendMessageForTodaySaleInvoice = New System.Windows.Forms.Button()
        Me.LblProgress = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnSendMessageForTodaySaleInvoice
        '
        Me.BtnSendMessageForTodaySaleInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSendMessageForTodaySaleInvoice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSendMessageForTodaySaleInvoice.Location = New System.Drawing.Point(70, 64)
        Me.BtnSendMessageForTodaySaleInvoice.Name = "BtnSendMessageForTodaySaleInvoice"
        Me.BtnSendMessageForTodaySaleInvoice.Size = New System.Drawing.Size(303, 23)
        Me.BtnSendMessageForTodaySaleInvoice.TabIndex = 0
        Me.BtnSendMessageForTodaySaleInvoice.Text = "Send Message For Today Sale Invoice"
        Me.BtnSendMessageForTodaySaleInvoice.UseVisualStyleBackColor = True
        '
        'LblProgress
        '
        Me.LblProgress.AutoSize = True
        Me.LblProgress.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProgress.ForeColor = System.Drawing.Color.Blue
        Me.LblProgress.Location = New System.Drawing.Point(12, 37)
        Me.LblProgress.Name = "LblProgress"
        Me.LblProgress.Size = New System.Drawing.Size(0, 18)
        Me.LblProgress.TabIndex = 1
        '
        'FrmSendWhatsappMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 208)
        Me.Controls.Add(Me.LblProgress)
        Me.Controls.Add(Me.BtnSendMessageForTodaySaleInvoice)
        Me.Name = "FrmSendWhatsappMessage"
        Me.Text = "Send Whatsapp Message"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSendMessageForTodaySaleInvoice As Button
    Friend WithEvents LblProgress As Label
End Class
