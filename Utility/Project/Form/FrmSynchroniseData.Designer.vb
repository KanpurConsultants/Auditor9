<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDataSynchronisation
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
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.OptOnlineData = New System.Windows.Forms.RadioButton
        Me.OptOfflineData = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(148, 191)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(89, 23)
        Me.BtnCancel.TabIndex = 32
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnUpdate
        '
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.Location = New System.Drawing.Point(45, 191)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(89, 23)
        Me.BtnUpdate.TabIndex = 31
        Me.BtnUpdate.Text = "&Synchronise"
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'OptOnlineData
        '
        Me.OptOnlineData.AutoSize = True
        Me.OptOnlineData.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptOnlineData.Location = New System.Drawing.Point(45, 74)
        Me.OptOnlineData.Name = "OptOnlineData"
        Me.OptOnlineData.Size = New System.Drawing.Size(92, 17)
        Me.OptOnlineData.TabIndex = 33
        Me.OptOnlineData.TabStop = True
        Me.OptOnlineData.Text = "Online Data"
        Me.OptOnlineData.UseVisualStyleBackColor = True
        '
        'OptOfflineData
        '
        Me.OptOfflineData.AutoSize = True
        Me.OptOfflineData.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptOfflineData.Location = New System.Drawing.Point(144, 74)
        Me.OptOfflineData.Name = "OptOfflineData"
        Me.OptOfflineData.Size = New System.Drawing.Size(93, 17)
        Me.OptOfflineData.TabIndex = 34
        Me.OptOfflineData.TabStop = True
        Me.OptOfflineData.Text = "Offline Data"
        Me.OptOfflineData.UseVisualStyleBackColor = True
        '
        'FrmDataSynchronisation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.OptOfflineData)
        Me.Controls.Add(Me.OptOnlineData)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnUpdate)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmDataSynchronisation"
        Me.Text = "Data Synchronisation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents OptOnlineData As System.Windows.Forms.RadioButton
    Friend WithEvents OptOfflineData As System.Windows.Forms.RadioButton
End Class
