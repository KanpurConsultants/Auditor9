<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportOptionsDialog
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
        Me.okButton = New System.Windows.Forms.Button
        Me.fieldsListBox = New System.Windows.Forms.CheckedListBox
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'okButton
        '
        Me.okButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.okButton.Location = New System.Drawing.Point(205, 231)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 7
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'fieldsListBox
        '
        Me.fieldsListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fieldsListBox.CheckOnClick = True
        Me.fieldsListBox.FormattingEnabled = True
        Me.fieldsListBox.Location = New System.Drawing.Point(12, 25)
        Me.fieldsListBox.Name = "fieldsListBox"
        Me.fieldsListBox.Size = New System.Drawing.Size(268, 199)
        Me.fieldsListBox.TabIndex = 5
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(73, 13)
        Me.label1.TabIndex = 4
        Me.label1.Text = "Choose fields:"
        '
        'ReportOptionsDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.ControlBox = False
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.fieldsListBox)
        Me.Controls.Add(Me.label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReportOptionsDialog"
        Me.Text = "Report Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents okButton As System.Windows.Forms.Button
    Private WithEvents fieldsListBox As System.Windows.Forms.CheckedListBox
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
