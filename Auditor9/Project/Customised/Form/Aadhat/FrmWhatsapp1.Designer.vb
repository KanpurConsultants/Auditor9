<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmWhatsapp1
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
        Me.btnSendText = New System.Windows.Forms.Button()
        Me.btnSendTextFile = New System.Windows.Forms.Button()
        Me.btnSendMulti = New System.Windows.Forms.Button()
        Me.btByURL = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSendText
        '
        Me.btnSendText.Location = New System.Drawing.Point(58, 45)
        Me.btnSendText.Name = "btnSendText"
        Me.btnSendText.Size = New System.Drawing.Size(145, 23)
        Me.btnSendText.TabIndex = 0
        Me.btnSendText.Text = "btnSendText"
        Me.btnSendText.UseVisualStyleBackColor = True
        '
        'btnSendTextFile
        '
        Me.btnSendTextFile.Location = New System.Drawing.Point(58, 74)
        Me.btnSendTextFile.Name = "btnSendTextFile"
        Me.btnSendTextFile.Size = New System.Drawing.Size(145, 23)
        Me.btnSendTextFile.TabIndex = 1
        Me.btnSendTextFile.Text = "btnSendTextFile"
        Me.btnSendTextFile.UseVisualStyleBackColor = True
        '
        'btnSendMulti
        '
        Me.btnSendMulti.Location = New System.Drawing.Point(58, 114)
        Me.btnSendMulti.Name = "btnSendMulti"
        Me.btnSendMulti.Size = New System.Drawing.Size(145, 23)
        Me.btnSendMulti.TabIndex = 2
        Me.btnSendMulti.Text = "btnSendMulti"
        Me.btnSendMulti.UseVisualStyleBackColor = True
        '
        'btByURL
        '
        Me.btByURL.Location = New System.Drawing.Point(58, 158)
        Me.btByURL.Name = "btByURL"
        Me.btByURL.Size = New System.Drawing.Size(145, 23)
        Me.btByURL.TabIndex = 3
        Me.btByURL.Text = "ByURL"
        Me.btByURL.UseVisualStyleBackColor = True
        '
        'FrmWhatsapp1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.btByURL)
        Me.Controls.Add(Me.btnSendMulti)
        Me.Controls.Add(Me.btnSendTextFile)
        Me.Controls.Add(Me.btnSendText)
        Me.Name = "FrmWhatsapp1"
        Me.Text = "FrmWhatsapp"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnSendText As Button
    Friend WithEvents btnSendTextFile As Button
    Friend WithEvents btnSendMulti As Button
    Friend WithEvents btByURL As Button
End Class
