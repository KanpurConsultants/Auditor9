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
        Me.PGBMain = New System.Windows.Forms.ProgressBar
        Me.PGBMain1 = New System.Windows.Forms.ProgressBar
        Me.SuspendLayout()
        '
        'PGBMain
        '
        Me.PGBMain.BackColor = System.Drawing.Color.OldLace
        Me.PGBMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.PGBMain.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.PGBMain.Location = New System.Drawing.Point(0, 0)
        Me.PGBMain.MarqueeAnimationSpeed = 0
        Me.PGBMain.Name = "PGBMain"
        Me.PGBMain.Size = New System.Drawing.Size(150, 7)
        Me.PGBMain.Step = 0
        Me.PGBMain.TabIndex = 0
        '
        'PGBMain1
        '
        Me.PGBMain1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.PGBMain1.BackColor = System.Drawing.Color.OldLace
        Me.PGBMain1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PGBMain1.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.PGBMain1.Location = New System.Drawing.Point(147, 0)
        Me.PGBMain1.MarqueeAnimationSpeed = 0
        Me.PGBMain1.Name = "PGBMain1"
        Me.PGBMain1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PGBMain1.RightToLeftLayout = True
        Me.PGBMain1.Size = New System.Drawing.Size(150, 7)
        Me.PGBMain1.Step = 0
        Me.PGBMain1.TabIndex = 1
        '
        'FrmProgressBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(297, 7)
        Me.ControlBox = False
        Me.Controls.Add(Me.PGBMain1)
        Me.Controls.Add(Me.PGBMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProgressBar"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.White
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PGBMain As System.Windows.Forms.ProgressBar
    Friend WithEvents PGBMain1 As System.Windows.Forms.ProgressBar
End Class
