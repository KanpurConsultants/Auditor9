<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProgressBar
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
        Me.components = New System.ComponentModel.Container
        Me.PBar1 = New Framework.Controls.XpProgressBar
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'PBar1
        '
        Me.PBar1.ColorBackGround = System.Drawing.Color.White
        Me.PBar1.ColorBarBorder = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.PBar1.ColorBarCenter = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.PBar1.ColorText = System.Drawing.Color.Black
        Me.PBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PBar1.Location = New System.Drawing.Point(0, 0)
        Me.PBar1.Name = "PBar1"
        Me.PBar1.Position = 50
        Me.PBar1.PositionMax = 100
        Me.PBar1.PositionMin = 0
        Me.PBar1.Size = New System.Drawing.Size(395, 30)
        Me.PBar1.TabIndex = 0
        '
        'Timer1
        '
        '
        'FrmProgressBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 30)
        Me.Controls.Add(Me.PBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmProgressBar"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PBar1 As Framework.Controls.XpProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
