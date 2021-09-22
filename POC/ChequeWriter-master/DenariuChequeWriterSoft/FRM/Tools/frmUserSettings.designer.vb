<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserSettings
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
        Me.components = New System.ComponentModel.Container
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pboxClose = New System.Windows.Forms.PictureBox
        Me.pboxRestore = New System.Windows.Forms.PictureBox
        Me.pboxMinimize = New System.Windows.Forms.PictureBox
        Me.pboxMaximize = New System.Windows.Forms.PictureBox
        Me.lblHead = New System.Windows.Forms.Label
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.lblUsername = New System.Windows.Forms.Label
        Me.pnlToolBar = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lblOldPassword = New System.Windows.Forms.Label
        Me.txtOldPassword = New System.Windows.Forms.TextBox
        Me.lblNewPassword = New System.Windows.Forms.Label
        Me.txtNewPassword = New System.Windows.Forms.TextBox
        Me.lblConfirmPassword = New System.Windows.Forms.Label
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox
        Me.txtUsername = New System.Windows.Forms.TextBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.pboxClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxRestore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxMinimize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxMaximize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.lblHead)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1168, 72)
        Me.Panel2.TabIndex = 46
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pboxClose)
        Me.Panel3.Controls.Add(Me.pboxRestore)
        Me.Panel3.Controls.Add(Me.pboxMinimize)
        Me.Panel3.Controls.Add(Me.pboxMaximize)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(1075, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(93, 72)
        Me.Panel3.TabIndex = 1
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
        'lblHead
        '
        Me.lblHead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHead.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHead.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblHead.Location = New System.Drawing.Point(0, 0)
        Me.lblHead.Name = "lblHead"
        Me.lblHead.Size = New System.Drawing.Size(1168, 72)
        Me.lblHead.TabIndex = 0
        Me.lblHead.Text = "Modify Login Detail"
        Me.lblHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblHead, "Modify Login Details")
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImage = Global.DenariuChequeWriterSoft.My.Resources.Resources.white_bck_20_9
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.lblUsername)
        Me.pnlMain.Controls.Add(Me.pnlToolBar)
        Me.pnlMain.Controls.Add(Me.lblOldPassword)
        Me.pnlMain.Controls.Add(Me.txtOldPassword)
        Me.pnlMain.Controls.Add(Me.lblNewPassword)
        Me.pnlMain.Controls.Add(Me.txtNewPassword)
        Me.pnlMain.Controls.Add(Me.lblConfirmPassword)
        Me.pnlMain.Controls.Add(Me.txtConfirmPassword)
        Me.pnlMain.Controls.Add(Me.txtUsername)
        Me.pnlMain.Location = New System.Drawing.Point(369, 130)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(430, 371)
        Me.pnlMain.TabIndex = 47
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.BackColor = System.Drawing.Color.Transparent
        Me.lblUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.Location = New System.Drawing.Point(84, 99)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(97, 24)
        Me.lblUsername.TabIndex = 0
        Me.lblUsername.Text = "Username"
        '
        'pnlToolBar
        '
        Me.pnlToolBar.BackColor = System.Drawing.Color.Transparent
        Me.pnlToolBar.Controls.Add(Me.btnExit)
        Me.pnlToolBar.Controls.Add(Me.btnSave)
        Me.pnlToolBar.Location = New System.Drawing.Point(112, 241)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(236, 64)
        Me.pnlToolBar.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.exit_32
        Me.btnExit.Location = New System.Drawing.Point(125, 9)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(99, 49)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = " E&XIT"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.save_32
        Me.btnSave.Location = New System.Drawing.Point(16, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 49)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&SAVE"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lblOldPassword
        '
        Me.lblOldPassword.AutoSize = True
        Me.lblOldPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblOldPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOldPassword.Location = New System.Drawing.Point(54, 132)
        Me.lblOldPassword.Name = "lblOldPassword"
        Me.lblOldPassword.Size = New System.Drawing.Size(127, 24)
        Me.lblOldPassword.TabIndex = 2
        Me.lblOldPassword.Text = "Old Password"
        '
        'txtOldPassword
        '
        Me.txtOldPassword.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldPassword.Location = New System.Drawing.Point(197, 132)
        Me.txtOldPassword.Name = "txtOldPassword"
        Me.txtOldPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtOldPassword.Size = New System.Drawing.Size(215, 25)
        Me.txtOldPassword.TabIndex = 3
        '
        'lblNewPassword
        '
        Me.lblNewPassword.AutoSize = True
        Me.lblNewPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblNewPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewPassword.Location = New System.Drawing.Point(45, 165)
        Me.lblNewPassword.Name = "lblNewPassword"
        Me.lblNewPassword.Size = New System.Drawing.Size(136, 24)
        Me.lblNewPassword.TabIndex = 4
        Me.lblNewPassword.Text = "New Password"
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPassword.Location = New System.Drawing.Point(197, 165)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPassword.Size = New System.Drawing.Size(215, 25)
        Me.txtNewPassword.TabIndex = 5
        '
        'lblConfirmPassword
        '
        Me.lblConfirmPassword.AutoSize = True
        Me.lblConfirmPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblConfirmPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfirmPassword.Location = New System.Drawing.Point(19, 198)
        Me.lblConfirmPassword.Name = "lblConfirmPassword"
        Me.lblConfirmPassword.Size = New System.Drawing.Size(162, 24)
        Me.lblConfirmPassword.TabIndex = 6
        Me.lblConfirmPassword.Text = "Confirm Password"
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPassword.Location = New System.Drawing.Point(197, 198)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmPassword.Size = New System.Drawing.Size(215, 25)
        Me.txtConfirmPassword.TabIndex = 7
        '
        'txtUsername
        '
        Me.txtUsername.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(197, 99)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(215, 25)
        Me.txtUsername.TabIndex = 1
        '
        'frmUserSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.DenariuChequeWriterSoft.My.Resources.Resources.plsn_bck
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1168, 572)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmUserSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Settings"
        Me.ToolTip1.SetToolTip(Me, "DenariuChequeWriterProV1.0")
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.pboxClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxRestore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxMinimize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxMaximize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlToolBar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblHead As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblOldPassword As System.Windows.Forms.Label
    Friend WithEvents txtOldPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblNewPassword As System.Windows.Forms.Label
    Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirmPassword As System.Windows.Forms.Label
    Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pboxClose As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pboxRestore As System.Windows.Forms.PictureBox
    Friend WithEvents pboxMinimize As System.Windows.Forms.PictureBox
    Friend WithEvents pboxMaximize As System.Windows.Forms.PictureBox
End Class
