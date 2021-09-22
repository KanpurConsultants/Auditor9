<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVoucherEntry_Chq_Det
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
        Me.Label23 = New System.Windows.Forms.Label
        Me.TxtChequeDate = New AgControls.AgTextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.TxtChequeNo = New AgControls.AgTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.BtnOK = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label23
        '
        Me.Label23.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(95, 57)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(83, 16)
        Me.Label23.TabIndex = 83
        Me.Label23.Text = "Cheque Date"
        '
        'TxtChequeDate
        '
        Me.TxtChequeDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtChequeDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChequeDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChequeDate.Location = New System.Drawing.Point(241, 57)
        Me.TxtChequeDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtChequeDate.MaxLength = 25
        Me.TxtChequeDate.Name = "TxtChequeDate"
        Me.TxtChequeDate.Size = New System.Drawing.Size(110, 18)
        Me.TxtChequeDate.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(95, 36)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(76, 16)
        Me.Label24.TabIndex = 81
        Me.Label24.Text = "Cheque No."
        '
        'TxtChequeNo
        '
        Me.TxtChequeNo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChequeNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChequeNo.Location = New System.Drawing.Point(241, 36)
        Me.TxtChequeNo.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtChequeNo.MaxLength = 20
        Me.TxtChequeNo.Name = "TxtChequeNo"
        Me.TxtChequeNo.Size = New System.Drawing.Size(159, 18)
        Me.TxtChequeNo.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(8, 111)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(509, 9)
        Me.GroupBox2.TabIndex = 116
        Me.GroupBox2.TabStop = False
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.BackColor = System.Drawing.Color.Transparent
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnCancel.Location = New System.Drawing.Point(433, 126)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(84, 24)
        Me.BtnCancel.TabIndex = 17
        Me.BtnCancel.Text = "Canc&el"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.BackColor = System.Drawing.Color.Transparent
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnOK.Location = New System.Drawing.Point(344, 126)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(84, 24)
        Me.BtnOK.TabIndex = 16
        Me.BtnOK.Text = "O&k"
        Me.BtnOK.UseVisualStyleBackColor = False
        '
        'FrmVoucherEntry_Chq_Det
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(522, 156)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.TxtChequeDate)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.TxtChequeNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmVoucherEntry_Chq_Det"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cheque Detail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TxtChequeDate As AgControls.AgTextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TxtChequeNo As AgControls.AgTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnOK As System.Windows.Forms.Button
End Class
