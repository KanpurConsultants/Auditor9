<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnConnect = New Glass.GlassButton
        Me.btnImBrowse = New Glass.GlassButton
        Me.cmbAddress = New CustomControls.IconComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.pnlStatus = New System.Windows.Forms.Panel
        Me.lnkOnline = New System.Windows.Forms.LinkLabel
        Me.lblStatus = New System.Windows.Forms.Label
        Me.btnClose = New Glass.GlassButton
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.cmsEditing = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsmCut = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsmCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.btnImage = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlineToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.AwayToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuBusy = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAppearOff = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.SendFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveLocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtSaveLoc = New System.Windows.Forms.ToolStripTextBox
        Me.BrowseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.SaveConversationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCut = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSelectAll = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.SettingsToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.FormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FontToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.FontColourToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutRapidChatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtmessage = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsRapidChat = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlineToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AwayToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.BusyToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OfflineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.SettingsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBrowse = New Andy.UI.ImageButton
        Me.ImageButton2 = New Andy.UI.ImageButton
        Me.ImageButton1 = New Andy.UI.ImageButton
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.cmsSend = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MessageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContactToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.StatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AwayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BusyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AppearOfflineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.fntFont = New System.Windows.Forms.FontDialog
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.btnSend = New Glass.GlassButton
        Me.btnClear = New Glass.GlassButton
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PicClient = New System.Windows.Forms.PictureBox
        Me.clrFont = New System.Windows.Forms.ColorDialog
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.FontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.AddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NetworkStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.HideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.lblStat = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtSound = New System.Windows.Forms.TextBox
        Me.picMypic = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.cmsEditing.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.cmsRapidChat.SuspendLayout()
        Me.cmsSend.SuspendLayout()
        CType(Me.PicClient, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.picMypic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnConnect)
        Me.GroupBox1.Controls.Add(Me.btnImBrowse)
        Me.GroupBox1.Controls.Add(Me.cmbAddress)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Location = New System.Drawing.Point(105, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 86)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Client Data"
        '
        'btnConnect
        '
        Me.btnConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnect.BackColor = System.Drawing.Color.LightGray
        Me.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConnect.ForeColor = System.Drawing.Color.Black
        Me.btnConnect.GlowColor = System.Drawing.Color.Gold
        Me.btnConnect.Location = New System.Drawing.Point(134, 15)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(84, 23)
        Me.btnConnect.TabIndex = 6
        Me.btnConnect.Text = "Connect..."
        Me.ToolTip2.SetToolTip(Me.btnConnect, "Test Connection to Client Address")
        '
        'btnImBrowse
        '
        Me.btnImBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImBrowse.BackColor = System.Drawing.Color.LightGray
        Me.btnImBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImBrowse.ForeColor = System.Drawing.Color.Black
        Me.btnImBrowse.GlowColor = System.Drawing.Color.Gold
        Me.btnImBrowse.Location = New System.Drawing.Point(158, 57)
        Me.btnImBrowse.Name = "btnImBrowse"
        Me.btnImBrowse.Size = New System.Drawing.Size(60, 23)
        Me.btnImBrowse.TabIndex = 5
        Me.btnImBrowse.Text = "Image..."
        Me.ToolTip2.SetToolTip(Me.btnImBrowse, "Browse for a Display Picture")
        '
        'cmbAddress
        '
        Me.cmbAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbAddress.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAddress.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmbAddress.FormattingEnabled = True
        Me.cmbAddress.ItemHeight = 16
        Me.cmbAddress.Location = New System.Drawing.Point(6, 17)
        Me.cmbAddress.Name = "cmbAddress"
        Me.cmbAddress.SelectedItem = Nothing
        Me.cmbAddress.Size = New System.Drawing.Size(122, 22)
        Me.cmbAddress.TabIndex = 5
        Me.cmbAddress.ToolTipText = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "My Display Name:"
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(6, 59)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(146, 20)
        Me.txtName.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.pnlStatus)
        Me.GroupBox2.Controls.Add(Me.RichTextBox1)
        Me.GroupBox2.Controls.Add(Me.btnImage)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.MenuStrip1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 137)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(317, 227)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Messages"
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatus.Controls.Add(Me.lnkOnline)
        Me.pnlStatus.Controls.Add(Me.lblStatus)
        Me.pnlStatus.Controls.Add(Me.btnClose)
        Me.pnlStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlStatus.Location = New System.Drawing.Point(3, 40)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(311, 40)
        Me.pnlStatus.TabIndex = 7
        Me.pnlStatus.Visible = False
        '
        'lnkOnline
        '
        Me.lnkOnline.AutoSize = True
        Me.lnkOnline.Location = New System.Drawing.Point(3, 20)
        Me.lnkOnline.Name = "lnkOnline"
        Me.lnkOnline.Size = New System.Drawing.Size(114, 13)
        Me.lnkOnline.TabIndex = 8
        Me.lnkOnline.TabStop = True
        Me.lnkOnline.Text = "Click here to go Online"
        '
        'lblStatus
        '
        Me.lblStatus.AutoEllipsis = True
        Me.lblStatus.Location = New System.Drawing.Point(3, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(251, 18)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Text = "Label2"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.LightGray
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.GlowColor = System.Drawing.Color.Red
        Me.btnClose.Location = New System.Drawing.Point(264, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.OuterBorderColor = System.Drawing.Color.SkyBlue
        Me.btnClose.Size = New System.Drawing.Size(42, 23)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.ToolTip2.SetToolTip(Me.btnClose, "Close The Notification Bar")
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.ContextMenuStrip = Me.cmsEditing
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(3, 40)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(311, 184)
        Me.RichTextBox1.TabIndex = 6
        Me.RichTextBox1.Text = ""
        '
        'cmsEditing
        '
        Me.cmsEditing.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsmCut, Me.cmsmCopy, Me.ToolStripSeparator12, Me.SelectAllToolStripMenuItem})
        Me.cmsEditing.Name = "cmsEditing"
        Me.cmsEditing.Size = New System.Drawing.Size(153, 76)
        '
        'cmsmCut
        '
        Me.cmsmCut.Enabled = False
        Me.cmsmCut.Name = "cmsmCut"
        Me.cmsmCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.cmsmCut.Size = New System.Drawing.Size(152, 22)
        Me.cmsmCut.Text = "&Cut"
        '
        'cmsmCopy
        '
        Me.cmsmCopy.Enabled = False
        Me.cmsmCopy.Name = "cmsmCopy"
        Me.cmsmCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.cmsmCopy.Size = New System.Drawing.Size(152, 22)
        Me.cmsmCopy.Text = "&Copy"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(149, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SelectAllToolStripMenuItem.Text = "S&elect All"
        '
        'btnImage
        '
        Me.btnImage.Location = New System.Drawing.Point(6, 69)
        Me.btnImage.Name = "btnImage"
        Me.btnImage.Size = New System.Drawing.Size(75, 23)
        Me.btnImage.TabIndex = 4
        Me.btnImage.Text = "Button3"
        Me.btnImage.UseVisualStyleBackColor = True
        Me.btnImage.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(110, 72)
        Me.TextBox1.MaxLength = 88888
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(159, 20)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem1, Me.EditToolStripMenuItem, Me.FormatToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(3, 16)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip1.Size = New System.Drawing.Size(311, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem1
        '
        Me.FileToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusToolStripMenuItem2, Me.ToolStripSeparator9, Me.SendFileToolStripMenuItem, Me.SaveLocationToolStripMenuItem, Me.ToolStripSeparator6, Me.SaveConversationToolStripMenuItem, Me.PrintToolStripMenuItem, Me.ToolStripSeparator7, Me.ExitToolStripMenuItem1})
        Me.FileToolStripMenuItem1.Name = "FileToolStripMenuItem1"
        Me.FileToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem1.Text = "File"
        '
        'StatusToolStripMenuItem2
        '
        Me.StatusToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnlineToolStripMenuItem2, Me.AwayToolStripMenuItem2, Me.mnuBusy, Me.mnuAppearOff})
        Me.StatusToolStripMenuItem2.Image = CType(resources.GetObject("StatusToolStripMenuItem2.Image"), System.Drawing.Image)
        Me.StatusToolStripMenuItem2.Name = "StatusToolStripMenuItem2"
        Me.StatusToolStripMenuItem2.Size = New System.Drawing.Size(155, 22)
        Me.StatusToolStripMenuItem2.Text = "Status"
        '
        'OnlineToolStripMenuItem2
        '
        Me.OnlineToolStripMenuItem2.Name = "OnlineToolStripMenuItem2"
        Me.OnlineToolStripMenuItem2.Size = New System.Drawing.Size(159, 22)
        Me.OnlineToolStripMenuItem2.Text = "Online"
        '
        'AwayToolStripMenuItem2
        '
        Me.AwayToolStripMenuItem2.Name = "AwayToolStripMenuItem2"
        Me.AwayToolStripMenuItem2.Size = New System.Drawing.Size(159, 22)
        Me.AwayToolStripMenuItem2.Tag = "My Status is Currently Set To Away"
        Me.AwayToolStripMenuItem2.Text = "Away"
        '
        'mnuBusy
        '
        Me.mnuBusy.Name = "mnuBusy"
        Me.mnuBusy.Size = New System.Drawing.Size(159, 22)
        Me.mnuBusy.Tag = "My Status is Currently Set To Busy"
        Me.mnuBusy.Text = "Busy"
        '
        'mnuAppearOff
        '
        Me.mnuAppearOff.Name = "mnuAppearOff"
        Me.mnuAppearOff.Size = New System.Drawing.Size(159, 22)
        Me.mnuAppearOff.Text = "Appear Offline"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(152, 6)
        '
        'SendFileToolStripMenuItem
        '
        Me.SendFileToolStripMenuItem.Name = "SendFileToolStripMenuItem"
        Me.SendFileToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SendFileToolStripMenuItem.Text = "S&end Files..."
        '
        'SaveLocationToolStripMenuItem
        '
        Me.SaveLocationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtSaveLoc, Me.BrowseToolStripMenuItem})
        Me.SaveLocationToolStripMenuItem.Name = "SaveLocationToolStripMenuItem"
        Me.SaveLocationToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SaveLocationToolStripMenuItem.Text = "Save Location"
        '
        'txtSaveLoc
        '
        Me.txtSaveLoc.Name = "txtSaveLoc"
        Me.txtSaveLoc.Size = New System.Drawing.Size(100, 23)
        Me.txtSaveLoc.Text = "C:\"
        '
        'BrowseToolStripMenuItem
        '
        Me.BrowseToolStripMenuItem.Name = "BrowseToolStripMenuItem"
        Me.BrowseToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.BrowseToolStripMenuItem.Text = "Browse..."
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(152, 6)
        '
        'SaveConversationToolStripMenuItem
        '
        Me.SaveConversationToolStripMenuItem.Name = "SaveConversationToolStripMenuItem"
        Me.SaveConversationToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SaveConversationToolStripMenuItem.Text = "Save &As..."
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.PrintToolStripMenuItem.Text = "Print"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(152, 6)
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(155, 22)
        Me.ExitToolStripMenuItem1.Text = "Exit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCut, Me.mnuCopy, Me.ToolStripSeparator5, Me.mnuSelectAll, Me.ToolStripSeparator11, Me.SettingsToolStripMenuItem2})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'mnuCut
        '
        Me.mnuCut.Enabled = False
        Me.mnuCut.Name = "mnuCut"
        Me.mnuCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mnuCut.Size = New System.Drawing.Size(152, 22)
        Me.mnuCut.Text = "&Cut"
        '
        'mnuCopy
        '
        Me.mnuCopy.Enabled = False
        Me.mnuCopy.Name = "mnuCopy"
        Me.mnuCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.mnuCopy.Size = New System.Drawing.Size(152, 22)
        Me.mnuCopy.Text = "&Copy"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(149, 6)
        '
        'mnuSelectAll
        '
        Me.mnuSelectAll.Name = "mnuSelectAll"
        Me.mnuSelectAll.Size = New System.Drawing.Size(152, 22)
        Me.mnuSelectAll.Text = "S&elect All..."
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(149, 6)
        '
        'SettingsToolStripMenuItem2
        '
        Me.SettingsToolStripMenuItem2.Name = "SettingsToolStripMenuItem2"
        Me.SettingsToolStripMenuItem2.Size = New System.Drawing.Size(152, 22)
        Me.SettingsToolStripMenuItem2.Text = "Settings"
        '
        'FormatToolStripMenuItem
        '
        Me.FormatToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FontToolStripMenuItem1, Me.FontColourToolStripMenuItem})
        Me.FormatToolStripMenuItem.Name = "FormatToolStripMenuItem"
        Me.FormatToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.FormatToolStripMenuItem.Text = "Format"
        '
        'FontToolStripMenuItem1
        '
        Me.FontToolStripMenuItem1.Name = "FontToolStripMenuItem1"
        Me.FontToolStripMenuItem1.Size = New System.Drawing.Size(145, 22)
        Me.FontToolStripMenuItem1.Text = "Font"
        '
        'FontColourToolStripMenuItem
        '
        Me.FontColourToolStripMenuItem.Name = "FontColourToolStripMenuItem"
        Me.FontColourToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.FontColourToolStripMenuItem.Text = "Font Colour"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1, Me.AboutRapidChatToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = CType(resources.GetObject("HelpToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(185, 22)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'AboutRapidChatToolStripMenuItem
        '
        Me.AboutRapidChatToolStripMenuItem.Name = "AboutRapidChatToolStripMenuItem"
        Me.AboutRapidChatToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AboutRapidChatToolStripMenuItem.Text = "About Rapid Chat..."
        '
        'txtmessage
        '
        Me.txtmessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtmessage.Location = New System.Drawing.Point(12, 371)
        Me.txtmessage.Multiline = True
        Me.txtmessage.Name = "txtmessage"
        Me.txtmessage.Size = New System.Drawing.Size(258, 71)
        Me.txtmessage.TabIndex = 5
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.cmsRapidChat
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Rapid Chat"
        Me.NotifyIcon1.Visible = True
        '
        'cmsRapidChat
        '
        Me.cmsRapidChat.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowToolStripMenuItem1, Me.StatusToolStripMenuItem1, Me.ToolStripSeparator10, Me.SettingsToolStripMenuItem1, Me.ToolStripSeparator8, Me.ExitToolStripMenuItem2})
        Me.cmsRapidChat.Name = "ContextMenuStrip1"
        Me.cmsRapidChat.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.cmsRapidChat.Size = New System.Drawing.Size(125, 104)
        '
        'ShowToolStripMenuItem1
        '
        Me.ShowToolStripMenuItem1.Name = "ShowToolStripMenuItem1"
        Me.ShowToolStripMenuItem1.Size = New System.Drawing.Size(124, 22)
        Me.ShowToolStripMenuItem1.Text = "Show"
        '
        'StatusToolStripMenuItem1
        '
        Me.StatusToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnlineToolStripMenuItem1, Me.AwayToolStripMenuItem1, Me.BusyToolStripMenuItem1, Me.OfflineToolStripMenuItem})
        Me.StatusToolStripMenuItem1.Image = CType(resources.GetObject("StatusToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.StatusToolStripMenuItem1.Name = "StatusToolStripMenuItem1"
        Me.StatusToolStripMenuItem1.Size = New System.Drawing.Size(124, 22)
        Me.StatusToolStripMenuItem1.Text = "Status"
        '
        'OnlineToolStripMenuItem1
        '
        Me.OnlineToolStripMenuItem1.Name = "OnlineToolStripMenuItem1"
        Me.OnlineToolStripMenuItem1.Size = New System.Drawing.Size(159, 22)
        Me.OnlineToolStripMenuItem1.Text = "Online"
        '
        'AwayToolStripMenuItem1
        '
        Me.AwayToolStripMenuItem1.Name = "AwayToolStripMenuItem1"
        Me.AwayToolStripMenuItem1.Size = New System.Drawing.Size(159, 22)
        Me.AwayToolStripMenuItem1.Text = "Away"
        '
        'BusyToolStripMenuItem1
        '
        Me.BusyToolStripMenuItem1.Name = "BusyToolStripMenuItem1"
        Me.BusyToolStripMenuItem1.Size = New System.Drawing.Size(159, 22)
        Me.BusyToolStripMenuItem1.Text = "Busy"
        '
        'OfflineToolStripMenuItem
        '
        Me.OfflineToolStripMenuItem.Name = "OfflineToolStripMenuItem"
        Me.OfflineToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.OfflineToolStripMenuItem.Tag = "Offline"
        Me.OfflineToolStripMenuItem.Text = "Appear Offline"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(121, 6)
        '
        'SettingsToolStripMenuItem1
        '
        Me.SettingsToolStripMenuItem1.Name = "SettingsToolStripMenuItem1"
        Me.SettingsToolStripMenuItem1.Size = New System.Drawing.Size(124, 22)
        Me.SettingsToolStripMenuItem1.Text = "Settings"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(121, 6)
        '
        'ExitToolStripMenuItem2
        '
        Me.ExitToolStripMenuItem2.Name = "ExitToolStripMenuItem2"
        Me.ExitToolStripMenuItem2.Size = New System.Drawing.Size(124, 22)
        Me.ExitToolStripMenuItem2.Text = "Exit"
        '
        'ofdImage
        '
        Me.ofdImage.FileName = "OpenFileDialog1"
        Me.ofdImage.Filter = "Bitmap Images (*.bmp)|*.bmp*"
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Rapid Chat"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowse.DisabledImage = CType(resources.GetObject("btnBrowse.DisabledImage"), System.Drawing.Image)
        Me.btnBrowse.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnBrowse.HoverImage = CType(resources.GetObject("btnBrowse.HoverImage"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(52, 0)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.NormalImage = CType(resources.GetObject("btnBrowse.NormalImage"), System.Drawing.Image)
        Me.btnBrowse.PushedImage = CType(resources.GetObject("btnBrowse.PushedImage"), System.Drawing.Image)
        Me.btnBrowse.Size = New System.Drawing.Size(92, 38)
        Me.btnBrowse.TabIndex = 3
        Me.btnBrowse.Text = "ImageButton3"
        Me.ToolTip1.SetToolTip(Me.btnBrowse, "Check My Network Status")
        '
        'ImageButton2
        '
        Me.ImageButton2.BackColor = System.Drawing.Color.Transparent
        Me.ImageButton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImageButton2.DisabledImage = CType(resources.GetObject("ImageButton2.DisabledImage"), System.Drawing.Image)
        Me.ImageButton2.Dock = System.Windows.Forms.DockStyle.Left
        Me.ImageButton2.HoverImage = CType(resources.GetObject("ImageButton2.HoverImage"), System.Drawing.Image)
        Me.ImageButton2.Location = New System.Drawing.Point(26, 0)
        Me.ImageButton2.Margin = New System.Windows.Forms.Padding(0)
        Me.ImageButton2.Name = "ImageButton2"
        Me.ImageButton2.NormalImage = CType(resources.GetObject("ImageButton2.NormalImage"), System.Drawing.Image)
        Me.ImageButton2.PushedImage = CType(resources.GetObject("ImageButton2.PushedImage"), System.Drawing.Image)
        Me.ImageButton2.Size = New System.Drawing.Size(26, 38)
        Me.ImageButton2.TabIndex = 1
        Me.ImageButton2.Text = "ImageButton2"
        Me.ToolTip1.SetToolTip(Me.ImageButton2, "Connect to Client")
        '
        'ImageButton1
        '
        Me.ImageButton1.BackColor = System.Drawing.Color.Transparent
        Me.ImageButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImageButton1.DisabledImage = CType(resources.GetObject("ImageButton1.DisabledImage"), System.Drawing.Image)
        Me.ImageButton1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ImageButton1.HoverImage = CType(resources.GetObject("ImageButton1.HoverImage"), System.Drawing.Image)
        Me.ImageButton1.Location = New System.Drawing.Point(0, 0)
        Me.ImageButton1.Margin = New System.Windows.Forms.Padding(0)
        Me.ImageButton1.Name = "ImageButton1"
        Me.ImageButton1.NormalImage = CType(resources.GetObject("ImageButton1.NormalImage"), System.Drawing.Image)
        Me.ImageButton1.PushedImage = CType(resources.GetObject("ImageButton1.PushedImage"), System.Drawing.Image)
        Me.ImageButton1.Size = New System.Drawing.Size(26, 38)
        Me.ImageButton1.TabIndex = 0
        Me.ImageButton1.Text = "ImageButton1"
        Me.ToolTip1.SetToolTip(Me.ImageButton1, "Clear all Fields")
        '
        'Timer3
        '
        Me.Timer3.Enabled = True
        Me.Timer3.Interval = 1
        '
        'cmsSend
        '
        Me.cmsSend.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MessageToolStripMenuItem, Me.FileToolStripMenuItem, Me.ContactToolStripMenuItem, Me.ToolStripSeparator3, Me.StatusToolStripMenuItem})
        Me.cmsSend.Name = "cmsSend"
        Me.cmsSend.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.cmsSend.Size = New System.Drawing.Size(129, 98)
        '
        'MessageToolStripMenuItem
        '
        Me.MessageToolStripMenuItem.Name = "MessageToolStripMenuItem"
        Me.MessageToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.MessageToolStripMenuItem.Text = "Message"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ContactToolStripMenuItem
        '
        Me.ContactToolStripMenuItem.Name = "ContactToolStripMenuItem"
        Me.ContactToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.ContactToolStripMenuItem.Text = "Contact"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(125, 6)
        '
        'StatusToolStripMenuItem
        '
        Me.StatusToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnlineToolStripMenuItem, Me.AwayToolStripMenuItem, Me.BusyToolStripMenuItem, Me.AppearOfflineToolStripMenuItem})
        Me.StatusToolStripMenuItem.Image = CType(resources.GetObject("StatusToolStripMenuItem.Image"), System.Drawing.Image)
        Me.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem"
        Me.StatusToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.StatusToolStripMenuItem.Text = "Status"
        '
        'OnlineToolStripMenuItem
        '
        Me.OnlineToolStripMenuItem.Name = "OnlineToolStripMenuItem"
        Me.OnlineToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.OnlineToolStripMenuItem.Text = "Online"
        '
        'AwayToolStripMenuItem
        '
        Me.AwayToolStripMenuItem.Name = "AwayToolStripMenuItem"
        Me.AwayToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.AwayToolStripMenuItem.Text = "Away"
        '
        'BusyToolStripMenuItem
        '
        Me.BusyToolStripMenuItem.Name = "BusyToolStripMenuItem"
        Me.BusyToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.BusyToolStripMenuItem.Text = "Busy"
        '
        'AppearOfflineToolStripMenuItem
        '
        Me.AppearOfflineToolStripMenuItem.Name = "AppearOfflineToolStripMenuItem"
        Me.AppearOfflineToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.AppearOfflineToolStripMenuItem.Text = "Appear Offline"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "All Files (*.*)|*.*"
        Me.OpenFileDialog1.Multiselect = True
        Me.OpenFileDialog1.Title = "Send Files"
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.BackColor = System.Drawing.Color.LightGray
        Me.btnSend.ContextMenuStrip = Me.cmsSend
        Me.btnSend.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSend.Enabled = False
        Me.btnSend.ForeColor = System.Drawing.Color.Black
        Me.btnSend.GlowColor = System.Drawing.Color.DodgerBlue
        Me.btnSend.Location = New System.Drawing.Point(276, 371)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(53, 48)
        Me.btnSend.TabIndex = 8
        Me.btnSend.Text = "Send"
        Me.ToolTip2.SetToolTip(Me.btnSend, "Send Message to Client")
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.BackColor = System.Drawing.Color.LightGray
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Enabled = False
        Me.btnClear.ForeColor = System.Drawing.Color.Black
        Me.btnClear.GlowColor = System.Drawing.Color.Red
        Me.btnClear.Location = New System.Drawing.Point(276, 418)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(53, 24)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "Clear"
        Me.ToolTip2.SetToolTip(Me.btnClear, "Clear Text from Message Box")
        '
        'ToolTip2
        '
        Me.ToolTip2.ToolTipTitle = "Rapid Chat"
        '
        'PicClient
        '
        Me.PicClient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicClient.ErrorImage = CType(resources.GetObject("PicClient.ErrorImage"), System.Drawing.Image)
        Me.PicClient.Image = CType(resources.GetObject("PicClient.Image"), System.Drawing.Image)
        Me.PicClient.InitialImage = Global.Rapid_Chat.My.Resources.Resources.busy
        Me.PicClient.Location = New System.Drawing.Point(12, 44)
        Me.PicClient.Name = "PicClient"
        Me.PicClient.Size = New System.Drawing.Size(87, 87)
        Me.PicClient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicClient.TabIndex = 0
        Me.PicClient.TabStop = False
        Me.ToolTip2.SetToolTip(Me.PicClient, "Double Click to add new Address")
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackgroundImage = Global.Rapid_Chat.My.Resources.Resources.toolbar_background2
        Me.StatusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.lblStat})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 451)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(341, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem, Me.ToolStripSeparator1, Me.FontToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.ToolStripSeparator4, Me.AddToolStripMenuItem, Me.NetworkStatusToolStripMenuItem, Me.ToolStripSeparator2, Me.HideToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(29, 20)
        Me.ToolStripDropDownButton1.Text = "Contat"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(159, 6)
        '
        'FontToolStripMenuItem
        '
        Me.FontToolStripMenuItem.Name = "FontToolStripMenuItem"
        Me.FontToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.FontToolStripMenuItem.Text = "Font"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(159, 6)
        '
        'AddToolStripMenuItem
        '
        Me.AddToolStripMenuItem.Name = "AddToolStripMenuItem"
        Me.AddToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.AddToolStripMenuItem.Text = "Add Contact IP"
        '
        'NetworkStatusToolStripMenuItem
        '
        Me.NetworkStatusToolStripMenuItem.Name = "NetworkStatusToolStripMenuItem"
        Me.NetworkStatusToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.NetworkStatusToolStripMenuItem.Text = "Network Status"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(159, 6)
        '
        'HideToolStripMenuItem
        '
        Me.HideToolStripMenuItem.Name = "HideToolStripMenuItem"
        Me.HideToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.HideToolStripMenuItem.Text = "Hide"
        '
        'lblStat
        '
        Me.lblStat.ForeColor = System.Drawing.Color.White
        Me.lblStat.Image = CType(resources.GetObject("lblStat.Image"), System.Drawing.Image)
        Me.lblStat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblStat.Name = "lblStat"
        Me.lblStat.Size = New System.Drawing.Size(297, 17)
        Me.lblStat.Spring = True
        Me.lblStat.Text = "Last Message Received At:"
        Me.lblStat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.Rapid_Chat.My.Resources.Resources.toolbar_background2
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.txtSound)
        Me.Panel1.Controls.Add(Me.btnBrowse)
        Me.Panel1.Controls.Add(Me.picMypic)
        Me.Panel1.Controls.Add(Me.ImageButton2)
        Me.Panel1.Controls.Add(Me.ImageButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(341, 38)
        Me.Panel1.TabIndex = 1
        '
        'txtSound
        '
        Me.txtSound.Location = New System.Drawing.Point(181, 3)
        Me.txtSound.Name = "txtSound"
        Me.txtSound.Size = New System.Drawing.Size(100, 20)
        Me.txtSound.TabIndex = 4
        Me.txtSound.Visible = False
        '
        'picMypic
        '
        Me.picMypic.BackgroundImage = Global.Rapid_Chat.My.Resources.Resources.toolbar_background2
        Me.picMypic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picMypic.Dock = System.Windows.Forms.DockStyle.Right
        Me.picMypic.InitialImage = Nothing
        Me.picMypic.Location = New System.Drawing.Point(303, 0)
        Me.picMypic.Name = "picMypic"
        Me.picMypic.Size = New System.Drawing.Size(38, 38)
        Me.picMypic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picMypic.TabIndex = 2
        Me.picMypic.TabStop = False
        '
        'Form1
        '
        Me.AcceptButton = Me.btnSend
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(341, 473)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtmessage)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PicClient)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(349, 505)
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rapid Chat"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.cmsEditing.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.cmsRapidChat.ResumeLayout(False)
        Me.cmsSend.ResumeLayout(False)
        CType(Me.PicClient, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.picMypic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PicClient As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ImageButton2 As Andy.UI.ImageButton
    Friend WithEvents ImageButton1 As Andy.UI.ImageButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtmessage As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents picMypic As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents btnImage As System.Windows.Forms.Button
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HideToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblStat As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnBrowse As Andy.UI.ImageButton
    Friend WithEvents NetworkStatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmbAddress As CustomControls.IconComboBox
    Friend WithEvents AddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnImBrowse As Glass.GlassButton
    Friend WithEvents btnConnect As Glass.GlassButton
    Friend WithEvents cmsSend As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MessageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContactToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnlineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AwayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BusyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AppearOfflineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents fntFont As System.Windows.Forms.FontDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveLocationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtSaveLoc As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BrowseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveConversationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutRapidChatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontColourToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSend As Glass.GlassButton
    Friend WithEvents btnClear As Glass.GlassButton
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents clrFont As System.Windows.Forms.ColorDialog
    Friend WithEvents StatusToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnlineToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AwayToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBusy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAppearOff As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsRapidChat As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnlineToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AwayToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BusyToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OfflineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SettingsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SettingsToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtSound As System.Windows.Forms.TextBox
    Friend WithEvents cmsEditing As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsmCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsmCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents lnkOnline As System.Windows.Forms.LinkLabel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnClose As Glass.GlassButton

End Class
