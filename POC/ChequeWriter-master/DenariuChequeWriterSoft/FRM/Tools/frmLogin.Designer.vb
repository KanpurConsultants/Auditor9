<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cbCompanyName = New System.Windows.Forms.ComboBox()
        Me.btnCompany = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.lblInvalidUserName = New System.Windows.Forms.Label()
        Me.pnlOption = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pboxClose = New System.Windows.Forms.PictureBox()
        Me.pboxRestore = New System.Windows.Forms.PictureBox()
        Me.pboxMinimize = New System.Windows.Forms.PictureBox()
        Me.pboxMaximize = New System.Windows.Forms.PictureBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PnlMaster = New System.Windows.Forms.Panel()
        Me.pnlMain.SuspendLayout()
        Me.pnlOption.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.pboxClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxRestore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxMinimize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxMaximize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlMaster.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImage = Global.DenariuChequeWriterSoft.My.Resources.Resources.login_screen
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.cbCompanyName)
        Me.pnlMain.Controls.Add(Me.btnCompany)
        Me.pnlMain.Controls.Add(Me.txtPassword)
        Me.pnlMain.Controls.Add(Me.lblPassword)
        Me.pnlMain.Controls.Add(Me.txtUserName)
        Me.pnlMain.Controls.Add(Me.lblUserName)
        Me.pnlMain.Controls.Add(Me.lblInvalidUserName)
        Me.pnlMain.Controls.Add(Me.pnlOption)
        Me.pnlMain.Controls.Add(Me.btnLogin)
        Me.pnlMain.Controls.Add(Me.lblCompany)
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(800, 521)
        Me.pnlMain.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.pnlMain, "DenariuChequeWritePro")
        '
        'cbCompanyName
        '
        Me.cbCompanyName.BackColor = System.Drawing.Color.SaddleBrown
        Me.cbCompanyName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cbCompanyName.FormattingEnabled = True
        Me.cbCompanyName.Location = New System.Drawing.Point(34, 464)
        Me.cbCompanyName.Name = "cbCompanyName"
        Me.cbCompanyName.Size = New System.Drawing.Size(185, 27)
        Me.cbCompanyName.TabIndex = 0
        '
        'btnCompany
        '
        Me.btnCompany.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCompany.FlatAppearance.BorderColor = System.Drawing.Color.SaddleBrown
        Me.btnCompany.FlatAppearance.BorderSize = 3
        Me.btnCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCompany.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompany.ForeColor = System.Drawing.Color.SaddleBrown
        Me.btnCompany.Location = New System.Drawing.Point(225, 460)
        Me.btnCompany.Name = "btnCompany"
        Me.btnCompany.Size = New System.Drawing.Size(32, 35)
        Me.btnCompany.TabIndex = 4
        Me.btnCompany.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnCompany, "Add Company")
        Me.btnCompany.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.SaddleBrown
        Me.txtPassword.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.White
        Me.txtPassword.Location = New System.Drawing.Point(474, 464)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(186, 26)
        Me.txtPassword.TabIndex = 2
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblPassword.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.ForeColor = System.Drawing.Color.Linen
        Me.lblPassword.Location = New System.Drawing.Point(474, 439)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(90, 22)
        Me.lblPassword.TabIndex = 5
        Me.lblPassword.Text = "Password"
        '
        'txtUserName
        '
        Me.txtUserName.BackColor = System.Drawing.Color.SaddleBrown
        Me.txtUserName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.ForeColor = System.Drawing.Color.White
        Me.txtUserName.Location = New System.Drawing.Point(282, 464)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(186, 26)
        Me.txtUserName.TabIndex = 1
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblUserName.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserName.ForeColor = System.Drawing.Color.Linen
        Me.lblUserName.Location = New System.Drawing.Point(282, 439)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(101, 22)
        Me.lblUserName.TabIndex = 3
        Me.lblUserName.Text = "User Name"
        '
        'lblInvalidUserName
        '
        Me.lblInvalidUserName.AutoSize = True
        Me.lblInvalidUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblInvalidUserName.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvalidUserName.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblInvalidUserName.Location = New System.Drawing.Point(34, 494)
        Me.lblInvalidUserName.Name = "lblInvalidUserName"
        Me.lblInvalidUserName.Size = New System.Drawing.Size(66, 22)
        Me.lblInvalidUserName.TabIndex = 4
        Me.lblInvalidUserName.Text = "Invalid"
        Me.lblInvalidUserName.Visible = False
        '
        'pnlOption
        '
        Me.pnlOption.BackColor = System.Drawing.Color.Transparent
        Me.pnlOption.Controls.Add(Me.Panel3)
        Me.pnlOption.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlOption.Location = New System.Drawing.Point(0, 0)
        Me.pnlOption.Name = "pnlOption"
        Me.pnlOption.Size = New System.Drawing.Size(800, 61)
        Me.pnlOption.TabIndex = 9
        Me.pnlOption.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pboxClose)
        Me.Panel3.Controls.Add(Me.pboxRestore)
        Me.Panel3.Controls.Add(Me.pboxMinimize)
        Me.Panel3.Controls.Add(Me.pboxMaximize)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(707, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(93, 61)
        Me.Panel3.TabIndex = 0
        '
        'pboxClose
        '
        Me.pboxClose.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.CLOSE1
        Me.pboxClose.Location = New System.Drawing.Point(60, 10)
        Me.pboxClose.Name = "pboxClose"
        Me.pboxClose.Size = New System.Drawing.Size(24, 24)
        Me.pboxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxClose.TabIndex = 0
        Me.pboxClose.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxClose, "Close")
        '
        'pboxRestore
        '
        Me.pboxRestore.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.restore1
        Me.pboxRestore.Location = New System.Drawing.Point(34, 10)
        Me.pboxRestore.Name = "pboxRestore"
        Me.pboxRestore.Size = New System.Drawing.Size(24, 24)
        Me.pboxRestore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxRestore.TabIndex = 0
        Me.pboxRestore.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxRestore, "Restore")
        '
        'pboxMinimize
        '
        Me.pboxMinimize.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.minimize1
        Me.pboxMinimize.Location = New System.Drawing.Point(8, 10)
        Me.pboxMinimize.Name = "pboxMinimize"
        Me.pboxMinimize.Size = New System.Drawing.Size(24, 24)
        Me.pboxMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxMinimize.TabIndex = 0
        Me.pboxMinimize.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxMinimize, "Minimize")
        '
        'pboxMaximize
        '
        Me.pboxMaximize.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.maximize1
        Me.pboxMaximize.Location = New System.Drawing.Point(34, 10)
        Me.pboxMaximize.Name = "pboxMaximize"
        Me.pboxMaximize.Size = New System.Drawing.Size(24, 24)
        Me.pboxMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxMaximize.TabIndex = 0
        Me.pboxMaximize.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxMaximize, "Maximize")
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.SaddleBrown
        Me.btnLogin.FlatAppearance.BorderSize = 3
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.ForeColor = System.Drawing.Color.SaddleBrown
        Me.btnLogin.Location = New System.Drawing.Point(668, 455)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(99, 35)
        Me.btnLogin.TabIndex = 3
        Me.btnLogin.Text = "Login"
        Me.ToolTip1.SetToolTip(Me.btnLogin, "Login")
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.BackColor = System.Drawing.Color.Transparent
        Me.lblCompany.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.ForeColor = System.Drawing.Color.Linen
        Me.lblCompany.Location = New System.Drawing.Point(34, 439)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(88, 22)
        Me.lblCompany.TabIndex = 0
        Me.lblCompany.Text = "Company"
        '
        'PnlMaster
        '
        Me.PnlMaster.Controls.Add(Me.pnlMain)
        Me.PnlMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMaster.Location = New System.Drawing.Point(0, 0)
        Me.PnlMaster.Name = "PnlMaster"
        Me.PnlMaster.Size = New System.Drawing.Size(802, 522)
        Me.PnlMaster.TabIndex = 10
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gray
        Me.ClientSize = New System.Drawing.Size(802, 522)
        Me.Controls.Add(Me.PnlMaster)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlOption.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.pboxClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxRestore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxMinimize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxMaximize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlMaster.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents lblInvalidUserName As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlOption As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pboxMinimize As System.Windows.Forms.PictureBox
    Friend WithEvents pboxClose As System.Windows.Forms.PictureBox
    Friend WithEvents pboxMaximize As System.Windows.Forms.PictureBox
    Friend WithEvents pboxRestore As System.Windows.Forms.PictureBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents PnlMaster As System.Windows.Forms.Panel
    Friend WithEvents btnCompany As System.Windows.Forms.Button
    Friend WithEvents cbCompanyName As System.Windows.Forms.ComboBox
End Class
