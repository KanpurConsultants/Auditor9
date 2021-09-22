<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSyncData
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
        Me.BtnSync = New System.Windows.Forms.Button()
        Me.LblProgress = New System.Windows.Forms.Label()
        Me.BtnSyncImages = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnSync
        '
        Me.BtnSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSync.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSync.Location = New System.Drawing.Point(119, 124)
        Me.BtnSync.Name = "BtnSync"
        Me.BtnSync.Size = New System.Drawing.Size(160, 23)
        Me.BtnSync.TabIndex = 0
        Me.BtnSync.Text = "Sync"
        Me.BtnSync.UseVisualStyleBackColor = True
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
        'BtnSyncImages
        '
        Me.BtnSyncImages.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSyncImages.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSyncImages.Location = New System.Drawing.Point(119, 153)
        Me.BtnSyncImages.Name = "BtnSyncImages"
        Me.BtnSyncImages.Size = New System.Drawing.Size(160, 23)
        Me.BtnSyncImages.TabIndex = 2
        Me.BtnSyncImages.Text = "Sync Images"
        Me.BtnSyncImages.UseVisualStyleBackColor = True
        '
        'FrmSyncData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 208)
        Me.Controls.Add(Me.BtnSyncImages)
        Me.Controls.Add(Me.LblProgress)
        Me.Controls.Add(Me.BtnSync)
        Me.Name = "FrmSyncData"
        Me.Text = "Sync Data"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSync As Button
    Friend WithEvents LblProgress As Label
    Friend WithEvents BtnSyncImages As Button
End Class
