<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBackupDatase
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
        Me.BtnBackupDatabase = New System.Windows.Forms.Button
        Me.LblInfo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'BtnBackupDatabase
        '
        Me.BtnBackupDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBackupDatabase.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBackupDatabase.Location = New System.Drawing.Point(211, 191)
        Me.BtnBackupDatabase.Name = "BtnBackupDatabase"
        Me.BtnBackupDatabase.Size = New System.Drawing.Size(183, 23)
        Me.BtnBackupDatabase.TabIndex = 31
        Me.BtnBackupDatabase.Text = "&Backup Database"
        Me.BtnBackupDatabase.UseVisualStyleBackColor = True
        '
        'LblInfo
        '
        Me.LblInfo.AutoSize = True
        Me.LblInfo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInfo.ForeColor = System.Drawing.Color.Blue
        Me.LblInfo.Location = New System.Drawing.Point(12, 45)
        Me.LblInfo.Name = "LblInfo"
        Me.LblInfo.Size = New System.Drawing.Size(50, 16)
        Me.LblInfo.TabIndex = 33
        Me.LblInfo.Text = "Label1"
        '
        'FrmBackupDatase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 266)
        Me.Controls.Add(Me.LblInfo)
        Me.Controls.Add(Me.BtnBackupDatabase)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmBackupDatase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bakcup Database"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents BtnBackupDatabase As System.Windows.Forms.Button
    Public WithEvents LblInfo As System.Windows.Forms.Label
End Class
