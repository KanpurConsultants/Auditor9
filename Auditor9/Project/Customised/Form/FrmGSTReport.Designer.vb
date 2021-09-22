<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGSTReport
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
        Me.BtnGST3B = New System.Windows.Forms.Button()
        Me.BtnGSTR1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnGST3B
        '
        Me.BtnGST3B.Location = New System.Drawing.Point(86, 82)
        Me.BtnGST3B.Name = "BtnGST3B"
        Me.BtnGST3B.Size = New System.Drawing.Size(75, 23)
        Me.BtnGST3B.TabIndex = 0
        Me.BtnGST3B.Text = "3B"
        Me.BtnGST3B.UseVisualStyleBackColor = True
        '
        'BtnGSTR1
        '
        Me.BtnGSTR1.Location = New System.Drawing.Point(84, 131)
        Me.BtnGSTR1.Name = "BtnGSTR1"
        Me.BtnGSTR1.Size = New System.Drawing.Size(75, 23)
        Me.BtnGSTR1.TabIndex = 1
        Me.BtnGSTR1.Text = "GSTR"
        Me.BtnGSTR1.UseVisualStyleBackColor = True
        '
        'FrmGSTReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.BtnGSTR1)
        Me.Controls.Add(Me.BtnGST3B)
        Me.Name = "FrmGSTReport"
        Me.Text = "FrmGSTReport"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnGST3B As Button
    Friend WithEvents BtnGSTR1 As Button
End Class
