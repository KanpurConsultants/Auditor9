<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Blocked Users")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkDisplayPic = New System.Windows.Forms.CheckBox
        Me.chkPopup = New System.Windows.Forms.CheckBox
        Me.chkFiletran = New System.Windows.Forms.CheckBox
        Me.chkTooltip = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkName = New System.Windows.Forms.CheckBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkSound = New System.Windows.Forms.CheckBox
        Me.btnBrowse = New Glass.GlassButton
        Me.txtSound = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnPicBrowse = New Glass.GlassButton
        Me.txtPic = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.picDisplay = New System.Windows.Forms.PictureBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbSize = New System.Windows.Forms.ComboBox
        Me.GlassButton2 = New Glass.GlassButton
        Me.cmbFont = New System.Windows.Forms.ComboBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.tvwBlockedUser = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnRemove = New Glass.GlassButton
        Me.btnAdd = New Glass.GlassButton
        Me.txtblockUser = New System.Windows.Forms.TextBox
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.GlassButton5 = New Glass.GlassButton
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.btnImBrowse = New Glass.GlassButton
        Me.GlassButton1 = New Glass.GlassButton
        Me.ofdSound = New System.Windows.Forms.OpenFileDialog
        Me.colFont = New System.Windows.Forms.ColorDialog
        Me.LabeledDivider1 = New VistaControls.LabeledDivider
        Me.SearchTextBox1 = New VistaControls.SearchTextBox
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(294, 293)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(286, 267)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkDisplayPic)
        Me.GroupBox3.Controls.Add(Me.chkPopup)
        Me.GroupBox3.Controls.Add(Me.chkFiletran)
        Me.GroupBox3.Controls.Add(Me.chkTooltip)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(3, 147)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(280, 117)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Other Settings"
        '
        'chkDisplayPic
        '
        Me.chkDisplayPic.AutoSize = True
        Me.chkDisplayPic.Checked = True
        Me.chkDisplayPic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDisplayPic.Location = New System.Drawing.Point(7, 91)
        Me.chkDisplayPic.Name = "chkDisplayPic"
        Me.chkDisplayPic.Size = New System.Drawing.Size(140, 17)
        Me.chkDisplayPic.TabIndex = 3
        Me.chkDisplayPic.Text = "Show my display Picture"
        Me.chkDisplayPic.UseVisualStyleBackColor = True
        '
        'chkPopup
        '
        Me.chkPopup.AutoSize = True
        Me.chkPopup.Checked = True
        Me.chkPopup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPopup.Location = New System.Drawing.Point(7, 68)
        Me.chkPopup.Name = "chkPopup"
        Me.chkPopup.Size = New System.Drawing.Size(136, 17)
        Me.chkPopup.TabIndex = 2
        Me.chkPopup.Text = "Show popup messages"
        Me.chkPopup.UseVisualStyleBackColor = True
        '
        'chkFiletran
        '
        Me.chkFiletran.AutoSize = True
        Me.chkFiletran.Checked = True
        Me.chkFiletran.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFiletran.Location = New System.Drawing.Point(7, 44)
        Me.chkFiletran.Name = "chkFiletran"
        Me.chkFiletran.Size = New System.Drawing.Size(109, 17)
        Me.chkFiletran.TabIndex = 1
        Me.chkFiletran.Text = "Allow file Transfer"
        Me.chkFiletran.UseVisualStyleBackColor = True
        '
        'chkTooltip
        '
        Me.chkTooltip.AutoSize = True
        Me.chkTooltip.Checked = True
        Me.chkTooltip.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTooltip.Location = New System.Drawing.Point(7, 20)
        Me.chkTooltip.Name = "chkTooltip"
        Me.chkTooltip.Size = New System.Drawing.Size(149, 17)
        Me.chkTooltip.TabIndex = 0
        Me.chkTooltip.Text = "Show Tooltips on Controls"
        Me.chkTooltip.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkName)
        Me.GroupBox2.Controls.Add(Me.txtName)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(3, 73)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(280, 74)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "My Display Name"
        '
        'chkName
        '
        Me.chkName.AutoSize = True
        Me.chkName.Checked = True
        Me.chkName.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkName.Location = New System.Drawing.Point(7, 47)
        Me.chkName.Name = "chkName"
        Me.chkName.Size = New System.Drawing.Size(169, 17)
        Me.chkName.TabIndex = 1
        Me.chkName.Text = "Show my name in the Taskbar"
        Me.chkName.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(7, 20)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(267, 20)
        Me.txtName.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkSound)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.txtSound)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 70)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sounds"
        '
        'chkSound
        '
        Me.chkSound.AutoSize = True
        Me.chkSound.Checked = True
        Me.chkSound.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSound.Location = New System.Drawing.Point(7, 46)
        Me.chkSound.Name = "chkSound"
        Me.chkSound.Size = New System.Drawing.Size(206, 17)
        Me.chkSound.TabIndex = 8
        Me.chkSound.Text = "Play sound when I receive a Message"
        Me.chkSound.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowse.BackColor = System.Drawing.Color.LightGray
        Me.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowse.ForeColor = System.Drawing.Color.Black
        Me.btnBrowse.GlowColor = System.Drawing.Color.Gold
        Me.btnBrowse.Location = New System.Drawing.Point(214, 17)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(60, 23)
        Me.btnBrowse.TabIndex = 7
        Me.btnBrowse.Text = "Browse..."
        '
        'txtSound
        '
        Me.txtSound.Location = New System.Drawing.Point(6, 19)
        Me.txtSound.Name = "txtSound"
        Me.txtSound.Size = New System.Drawing.Size(202, 20)
        Me.txtSound.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(286, 267)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Appearance"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnPicBrowse)
        Me.GroupBox5.Controls.Add(Me.txtPic)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.picDisplay)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(3, 80)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(280, 184)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "General"
        '
        'btnPicBrowse
        '
        Me.btnPicBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPicBrowse.BackColor = System.Drawing.Color.LightGray
        Me.btnPicBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPicBrowse.ForeColor = System.Drawing.Color.Black
        Me.btnPicBrowse.GlowColor = System.Drawing.Color.Gold
        Me.btnPicBrowse.Location = New System.Drawing.Point(214, 63)
        Me.btnPicBrowse.Name = "btnPicBrowse"
        Me.btnPicBrowse.Size = New System.Drawing.Size(60, 23)
        Me.btnPicBrowse.TabIndex = 9
        Me.btnPicBrowse.Text = "Browse..."
        '
        'txtPic
        '
        Me.txtPic.Location = New System.Drawing.Point(94, 37)
        Me.txtPic.Name = "txtPic"
        Me.txtPic.Size = New System.Drawing.Size(180, 20)
        Me.txtPic.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(94, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "My Display Picture:"
        '
        'picDisplay
        '
        Me.picDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picDisplay.Location = New System.Drawing.Point(7, 19)
        Me.picDisplay.Name = "picDisplay"
        Me.picDisplay.Size = New System.Drawing.Size(80, 80)
        Me.picDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDisplay.TabIndex = 0
        Me.picDisplay.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.cmbSize)
        Me.GroupBox4.Controls.Add(Me.GlassButton2)
        Me.GroupBox4.Controls.Add(Me.cmbFont)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(280, 77)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Font"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(157, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Font Size"
        '
        'cmbSize
        '
        Me.cmbSize.FormattingEnabled = True
        Me.cmbSize.Items.AddRange(New Object() {"8", "9", "10", "11", "12", "14", "16", "18", "24", "28", "32", "36", "48", "72"})
        Me.cmbSize.Location = New System.Drawing.Point(214, 47)
        Me.cmbSize.Name = "cmbSize"
        Me.cmbSize.Size = New System.Drawing.Size(59, 21)
        Me.cmbSize.TabIndex = 9
        Me.cmbSize.Text = "8.25"
        '
        'GlassButton2
        '
        Me.GlassButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlassButton2.BackColor = System.Drawing.Color.LightGray
        Me.GlassButton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GlassButton2.ForeColor = System.Drawing.Color.Black
        Me.GlassButton2.GlowColor = System.Drawing.Color.Gold
        Me.GlassButton2.Location = New System.Drawing.Point(7, 45)
        Me.GlassButton2.Name = "GlassButton2"
        Me.GlassButton2.Size = New System.Drawing.Size(60, 23)
        Me.GlassButton2.TabIndex = 8
        Me.GlassButton2.Text = "Colour"
        '
        'cmbFont
        '
        Me.cmbFont.FormattingEnabled = True
        Me.cmbFont.Location = New System.Drawing.Point(7, 20)
        Me.cmbFont.Name = "cmbFont"
        Me.cmbFont.Size = New System.Drawing.Size(267, 21)
        Me.cmbFont.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox6)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(286, 267)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Blocked Users"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.SearchTextBox1)
        Me.GroupBox6.Controls.Add(Me.tvwBlockedUser)
        Me.GroupBox6.Controls.Add(Me.btnRemove)
        Me.GroupBox6.Controls.Add(Me.btnAdd)
        Me.GroupBox6.Controls.Add(Me.txtblockUser)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(286, 267)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Block files from these Users"
        '
        'tvwBlockedUser
        '
        Me.tvwBlockedUser.ImageIndex = 0
        Me.tvwBlockedUser.ImageList = Me.ImageList1
        Me.tvwBlockedUser.Location = New System.Drawing.Point(8, 101)
        Me.tvwBlockedUser.Name = "tvwBlockedUser"
        TreeNode1.Name = "Node0"
        TreeNode1.Text = "Blocked Users"
        Me.tvwBlockedUser.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.tvwBlockedUser.SelectedImageIndex = 0
        Me.tvwBlockedUser.Size = New System.Drawing.Size(270, 160)
        Me.tvwBlockedUser.TabIndex = 11
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "business_user_delete.png")
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.BackColor = System.Drawing.Color.LightGray
        Me.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRemove.ForeColor = System.Drawing.Color.Black
        Me.btnRemove.GlowColor = System.Drawing.Color.Gold
        Me.btnRemove.Location = New System.Drawing.Point(152, 45)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(60, 23)
        Me.btnRemove.TabIndex = 10
        Me.btnRemove.Text = "Remove"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackColor = System.Drawing.Color.LightGray
        Me.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAdd.ForeColor = System.Drawing.Color.Black
        Me.btnAdd.GlowColor = System.Drawing.Color.Gold
        Me.btnAdd.Location = New System.Drawing.Point(218, 45)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(60, 23)
        Me.btnAdd.TabIndex = 9
        Me.btnAdd.Text = "Add"
        '
        'txtblockUser
        '
        Me.txtblockUser.Location = New System.Drawing.Point(8, 19)
        Me.txtblockUser.Name = "txtblockUser"
        Me.txtblockUser.Size = New System.Drawing.Size(270, 20)
        Me.txtblockUser.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.GroupBox8)
        Me.TabPage4.Controls.Add(Me.GroupBox7)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(286, 267)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "File Transfer"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox8.Location = New System.Drawing.Point(0, 54)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(286, 213)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Options"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.GlassButton5)
        Me.GroupBox7.Controls.Add(Me.ComboBox2)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox7.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(286, 54)
        Me.GroupBox7.TabIndex = 0
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Save Location"
        '
        'GlassButton5
        '
        Me.GlassButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlassButton5.BackColor = System.Drawing.Color.LightGray
        Me.GlassButton5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GlassButton5.ForeColor = System.Drawing.Color.Black
        Me.GlassButton5.GlowColor = System.Drawing.Color.Gold
        Me.GlassButton5.Location = New System.Drawing.Point(220, 18)
        Me.GlassButton5.Name = "GlassButton5"
        Me.GlassButton5.Size = New System.Drawing.Size(60, 23)
        Me.GlassButton5.TabIndex = 8
        Me.GlassButton5.Text = "Browse..."
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(9, 20)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(205, 21)
        Me.ComboBox2.TabIndex = 0
        '
        'btnImBrowse
        '
        Me.btnImBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImBrowse.BackColor = System.Drawing.Color.LightGray
        Me.btnImBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImBrowse.ForeColor = System.Drawing.Color.Black
        Me.btnImBrowse.GlowColor = System.Drawing.Color.DodgerBlue
        Me.btnImBrowse.Location = New System.Drawing.Point(230, 311)
        Me.btnImBrowse.Name = "btnImBrowse"
        Me.btnImBrowse.Size = New System.Drawing.Size(60, 28)
        Me.btnImBrowse.TabIndex = 6
        Me.btnImBrowse.Text = "Apply"
        '
        'GlassButton1
        '
        Me.GlassButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlassButton1.BackColor = System.Drawing.Color.LightGray
        Me.GlassButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GlassButton1.ForeColor = System.Drawing.Color.Black
        Me.GlassButton1.GlowColor = System.Drawing.Color.Red
        Me.GlassButton1.Location = New System.Drawing.Point(164, 311)
        Me.GlassButton1.Name = "GlassButton1"
        Me.GlassButton1.Size = New System.Drawing.Size(60, 28)
        Me.GlassButton1.TabIndex = 7
        Me.GlassButton1.Text = "Close"
        '
        'ofdSound
        '
        Me.ofdSound.FileName = "OpenFileDialog1"
        Me.ofdSound.Filter = "Wav Audio Files (*.wav)|*.wav"
        '
        'LabeledDivider1
        '
        Me.LabeledDivider1.BackColor = System.Drawing.SystemColors.Control
        Me.LabeledDivider1.DividerColor = System.Drawing.Color.LightGray
        Me.LabeledDivider1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabeledDivider1.ForeColor = System.Drawing.Color.Black
        Me.LabeledDivider1.Location = New System.Drawing.Point(4, 295)
        Me.LabeledDivider1.Name = "LabeledDivider1"
        Me.LabeledDivider1.Size = New System.Drawing.Size(286, 16)
        Me.LabeledDivider1.TabIndex = 11
        Me.LabeledDivider1.Text = "Options"
        '
        'SearchTextBox1
        '
        Me.SearchTextBox1.ActiveBackColor = System.Drawing.Color.White
        Me.SearchTextBox1.ActiveFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchTextBox1.BackColor = System.Drawing.Color.White
        Me.SearchTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.SearchTextBox1.ForeColor = System.Drawing.SystemColors.GrayText
        Me.SearchTextBox1.InactiveBackColor = System.Drawing.Color.White
        Me.SearchTextBox1.Location = New System.Drawing.Point(8, 74)
        Me.SearchTextBox1.Name = "SearchTextBox1"
        Me.SearchTextBox1.Size = New System.Drawing.Size(270, 21)
        Me.SearchTextBox1.TabIndex = 12
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(294, 346)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabeledDivider1)
        Me.Controls.Add(Me.GlassButton1)
        Me.Controls.Add(Me.btnImBrowse)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rapid Chat Settings"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnImBrowse As Glass.GlassButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkName As System.Windows.Forms.CheckBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowse As Glass.GlassButton
    Friend WithEvents txtSound As System.Windows.Forms.TextBox
    Friend WithEvents chkPopup As System.Windows.Forms.CheckBox
    Friend WithEvents chkFiletran As System.Windows.Forms.CheckBox
    Friend WithEvents chkTooltip As System.Windows.Forms.CheckBox
    Friend WithEvents chkSound As System.Windows.Forms.CheckBox
    Friend WithEvents GlassButton1 As Glass.GlassButton
    Friend WithEvents chkDisplayPic As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GlassButton2 As Glass.GlassButton
    Friend WithEvents cmbFont As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemove As Glass.GlassButton
    Friend WithEvents btnAdd As Glass.GlassButton
    Friend WithEvents txtblockUser As System.Windows.Forms.TextBox
    Friend WithEvents tvwBlockedUser As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GlassButton5 As Glass.GlassButton
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents ofdSound As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picDisplay As System.Windows.Forms.PictureBox
    Friend WithEvents btnPicBrowse As Glass.GlassButton
    Friend WithEvents txtPic As System.Windows.Forms.TextBox
    Friend WithEvents colFont As System.Windows.Forms.ColorDialog
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbSize As System.Windows.Forms.ComboBox
    Friend WithEvents LabeledDivider1 As VistaControls.LabeledDivider
    Friend WithEvents SearchTextBox1 As VistaControls.SearchTextBox
End Class
