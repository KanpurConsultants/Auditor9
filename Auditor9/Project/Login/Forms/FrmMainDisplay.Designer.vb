<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMainDisplay
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMainDisplay))
        Me.BtnClose = New System.Windows.Forms.Button
        Me.TspMenu = New System.Windows.Forms.ToolStrip
        Me.TsbMasters = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.TsbSales = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.TsbFA = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.TsbReports = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.LblLoginDate = New System.Windows.Forms.Label
        Me.LblUName = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.LblCompName = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.BtnUP = New System.Windows.Forms.Button
        Me.TspMenu.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnClose
        '
        Me.BtnClose.AutoEllipsis = True
        Me.BtnClose.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnClose.Location = New System.Drawing.Point(776, 510)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(84, 24)
        Me.BtnClose.TabIndex = 1
        Me.BtnClose.Text = "Clos&e"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'TspMenu
        '
        Me.TspMenu.AutoSize = False
        Me.TspMenu.BackColor = System.Drawing.Color.Transparent
        Me.TspMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TspMenu.Dock = System.Windows.Forms.DockStyle.None
        Me.TspMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TspMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TsbMasters, Me.toolStripSeparator, Me.TsbSales, Me.toolStripSeparator1, Me.TsbFA, Me.toolStripSeparator2, Me.TsbReports, Me.toolStripSeparator3})
        Me.TspMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.TspMenu.Location = New System.Drawing.Point(0, 71)
        Me.TspMenu.Name = "TspMenu"
        Me.TspMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.TspMenu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TspMenu.Size = New System.Drawing.Size(251, 432)
        Me.TspMenu.Stretch = True
        Me.TspMenu.TabIndex = 0
        '
        'TsbMasters
        '
        Me.TsbMasters.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TsbMasters.Image = CType(resources.GetObject("TsbMasters.Image"), System.Drawing.Image)
        Me.TsbMasters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TsbMasters.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbMasters.Name = "TsbMasters"
        Me.TsbMasters.Size = New System.Drawing.Size(249, 20)
        Me.TsbMasters.Text = "&Masters"
        Me.TsbMasters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(249, 6)
        Me.toolStripSeparator.Visible = False
        '
        'TsbSales
        '
        Me.TsbSales.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.TsbSales.Image = CType(resources.GetObject("TsbSales.Image"), System.Drawing.Image)
        Me.TsbSales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TsbSales.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbSales.Name = "TsbSales"
        Me.TsbSales.Size = New System.Drawing.Size(249, 20)
        Me.TsbSales.Text = "&Sales"
        Me.TsbSales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(249, 6)
        Me.toolStripSeparator1.Visible = False
        '
        'TsbFA
        '
        Me.TsbFA.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.TsbFA.Image = CType(resources.GetObject("TsbFA.Image"), System.Drawing.Image)
        Me.TsbFA.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TsbFA.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbFA.Name = "TsbFA"
        Me.TsbFA.Size = New System.Drawing.Size(249, 20)
        Me.TsbFA.Text = "&Financial Accounts"
        Me.TsbFA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(249, 6)
        Me.toolStripSeparator2.Visible = False
        '
        'TsbReports
        '
        Me.TsbReports.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.TsbReports.Image = CType(resources.GetObject("TsbReports.Image"), System.Drawing.Image)
        Me.TsbReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TsbReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsbReports.Name = "TsbReports"
        Me.TsbReports.Size = New System.Drawing.Size(249, 20)
        Me.TsbReports.Text = "&Reports"
        Me.TsbReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(249, 6)
        Me.toolStripSeparator3.Visible = False
        '
        'LblLoginDate
        '
        Me.LblLoginDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblLoginDate.BackColor = System.Drawing.Color.Transparent
        Me.LblLoginDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLoginDate.ForeColor = System.Drawing.Color.Black
        Me.LblLoginDate.Location = New System.Drawing.Point(615, 92)
        Me.LblLoginDate.Name = "LblLoginDate"
        Me.LblLoginDate.Size = New System.Drawing.Size(251, 20)
        Me.LblLoginDate.TabIndex = 14
        Me.LblLoginDate.Text = "Login Date : "
        Me.LblLoginDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblUName
        '
        Me.LblUName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblUName.BackColor = System.Drawing.Color.Transparent
        Me.LblUName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUName.ForeColor = System.Drawing.Color.Black
        Me.LblUName.Location = New System.Drawing.Point(615, 74)
        Me.LblUName.Name = "LblUName"
        Me.LblUName.Size = New System.Drawing.Size(251, 18)
        Me.LblUName.TabIndex = 10
        Me.LblUName.Text = "User  : "
        Me.LblUName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox1.Image = Global.Login.My.Resources.Resources.My7
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(866, 70)
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'LblCompName
        '
        Me.LblCompName.BackColor = System.Drawing.Color.Transparent
        Me.LblCompName.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LblCompName.Font = New System.Drawing.Font("Elephant", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LblCompName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCompName.Image = CType(resources.GetObject("LblCompName.Image"), System.Drawing.Image)
        Me.LblCompName.Location = New System.Drawing.Point(0, 502)
        Me.LblCompName.Name = "LblCompName"
        Me.LblCompName.Size = New System.Drawing.Size(866, 38)
        Me.LblCompName.TabIndex = 8
        Me.LblCompName.Text = "Company Name"
        Me.LblCompName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(287, 155)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(300, 275)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 15
        Me.PictureBox2.TabStop = False
        '
        'BtnUP
        '
        Me.BtnUP.AutoEllipsis = True
        Me.BtnUP.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnUP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnUP.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnUP.Location = New System.Drawing.Point(721, 308)
        Me.BtnUP.Name = "BtnUP"
        Me.BtnUP.Size = New System.Drawing.Size(84, 24)
        Me.BtnUP.TabIndex = 16
        Me.BtnUP.Text = "UP"
        Me.BtnUP.UseVisualStyleBackColor = False
        '
        'FrmMainDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(866, 540)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnUP)
        Me.Controls.Add(Me.LblLoginDate)
        Me.Controls.Add(Me.LblUName)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LblCompName)
        Me.Controls.Add(Me.TspMenu)
        Me.Controls.Add(Me.PictureBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmMainDisplay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Leatherage"
        Me.TspMenu.ResumeLayout(False)
        Me.TspMenu.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents LblCompName As System.Windows.Forms.Label
    Friend WithEvents LblUName As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TspMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents TsbMasters As System.Windows.Forms.ToolStripButton
    Friend WithEvents TsbSales As System.Windows.Forms.ToolStripButton
    Friend WithEvents TsbFA As System.Windows.Forms.ToolStripButton
    Friend WithEvents TsbReports As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LblLoginDate As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents BtnUP As System.Windows.Forms.Button
End Class
