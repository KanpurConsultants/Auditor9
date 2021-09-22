<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIMain))
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.SSrpMain = New System.Windows.Forms.StatusStrip
        Me.TSSL_Company = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSL_Site = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSL_User = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSL_Division = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSL_OnlineOffLine = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton
        Me.TSSL_Btn_UpdateTableStructure = New System.Windows.Forms.ToolStripMenuItem
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.TSSL_Btn_ManageMDI = New System.Windows.Forms.ToolStripMenuItem
        Me.TSSL_Btn_ManageUserControl = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.TSSL_Btn_ReconnectDatabase = New System.Windows.Forms.ToolStripMenuItem
        Me.AgSideBar1 = New AgControls.OutlookStyleControls.AgSideBar
        Me.PnlSideBar = New System.Windows.Forms.Panel
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.splitter2 = New System.Windows.Forms.Splitter
        Me.TbcMain = New System.Windows.Forms.TabControl
        Me.Tbp1 = New System.Windows.Forms.TabPage
        Me.splitter1 = New System.Windows.Forms.Splitter
        Me.SSrpMain.SuspendLayout()
        Me.PnlSideBar.SuspendLayout()
        Me.TbcMain.SuspendLayout()
        Me.Tbp1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(169, 276)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SSrpMain
        '
        Me.SSrpMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSSL_Company, Me.TSSL_Site, Me.TSSL_User, Me.TSSL_Division, Me.TSSL_OnlineOffLine, Me.ToolStripSplitButton1})
        Me.SSrpMain.Location = New System.Drawing.Point(0, 425)
        Me.SSrpMain.Name = "SSrpMain"
        Me.SSrpMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.SSrpMain.Size = New System.Drawing.Size(864, 24)
        Me.SSrpMain.TabIndex = 4
        Me.SSrpMain.Text = "StatusStrip1"
        '
        'TSSL_Company
        '
        Me.TSSL_Company.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TSSL_Company.Name = "TSSL_Company"
        Me.TSSL_Company.Size = New System.Drawing.Size(177, 19)
        Me.TSSL_Company.Spring = True
        Me.TSSL_Company.Text = "Company Name"
        Me.TSSL_Company.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TSSL_Site
        '
        Me.TSSL_Site.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TSSL_Site.Name = "TSSL_Site"
        Me.TSSL_Site.Size = New System.Drawing.Size(177, 19)
        Me.TSSL_Site.Spring = True
        Me.TSSL_Site.Text = "Site Name"
        Me.TSSL_Site.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TSSL_User
        '
        Me.TSSL_User.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TSSL_User.Name = "TSSL_User"
        Me.TSSL_User.Size = New System.Drawing.Size(177, 19)
        Me.TSSL_User.Spring = True
        Me.TSSL_User.Text = "User Name"
        Me.TSSL_User.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TSSL_Division
        '
        Me.TSSL_Division.Name = "TSSL_Division"
        Me.TSSL_Division.Size = New System.Drawing.Size(49, 19)
        Me.TSSL_Division.Text = "Division"
        '
        'TSSL_OnlineOffLine
        '
        Me.TSSL_OnlineOffLine.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TSSL_OnlineOffLine.Name = "TSSL_OnlineOffLine"
        Me.TSSL_OnlineOffLine.Size = New System.Drawing.Size(177, 19)
        Me.TSSL_OnlineOffLine.Spring = True
        Me.TSSL_OnlineOffLine.Text = "[Online]"
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSSL_Btn_UpdateTableStructure, Me.TSSL_UpdateTableStructureWebToolStripMenuItem, Me.ToolStripMenuItem1, Me.TSSL_Btn_ManageMDI, Me.TSSL_Btn_ManageUserControl, Me.ToolStripMenuItem2, Me.TSSL_Btn_ReconnectDatabase})
        Me.ToolStripSplitButton1.Image = CType(resources.GetObject("ToolStripSplitButton1.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(91, 22)
        Me.ToolStripSplitButton1.Text = "Master Tools"
        '
        'TSSL_Btn_UpdateTableStructure
        '
        Me.TSSL_Btn_UpdateTableStructure.Name = "TSSL_Btn_UpdateTableStructure"
        Me.TSSL_Btn_UpdateTableStructure.Size = New System.Drawing.Size(222, 22)
        Me.TSSL_Btn_UpdateTableStructure.Text = "Update Table Structure"
        '
        'TSSL_UpdateTableStructureWebToolStripMenuItem
        '
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem.Name = "TSSL_UpdateTableStructureWebToolStripMenuItem"
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem.Text = "Update Table Structure Web"
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem.Visible = False
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(219, 6)
        '
        'TSSL_Btn_ManageMDI
        '
        Me.TSSL_Btn_ManageMDI.Name = "TSSL_Btn_ManageMDI"
        Me.TSSL_Btn_ManageMDI.Size = New System.Drawing.Size(222, 22)
        Me.TSSL_Btn_ManageMDI.Text = "Manage MDI"
        '
        'TSSL_Btn_ManageUserControl
        '
        Me.TSSL_Btn_ManageUserControl.Name = "TSSL_Btn_ManageUserControl"
        Me.TSSL_Btn_ManageUserControl.Size = New System.Drawing.Size(222, 22)
        Me.TSSL_Btn_ManageUserControl.Text = "Manage User Control"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(219, 6)
        '
        'TSSL_Btn_ReconnectDatabase
        '
        Me.TSSL_Btn_ReconnectDatabase.Name = "TSSL_Btn_ReconnectDatabase"
        Me.TSSL_Btn_ReconnectDatabase.Size = New System.Drawing.Size(222, 22)
        Me.TSSL_Btn_ReconnectDatabase.Text = "Reconnect Database"
        '
        'AgSideBar1
        '
        Me.AgSideBar1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.AgSideBar1.ButtonHeight = 30
        Me.AgSideBar1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AgSideBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.AgSideBar1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AgSideBar1.GradientButtonHoverDark = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.AgSideBar1.GradientButtonHoverLight = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.AgSideBar1.GradientButtonNormalDark = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.AgSideBar1.GradientButtonNormalLight = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.AgSideBar1.GradientButtonSelectedDark = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.AgSideBar1.GradientButtonSelectedLight = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.AgSideBar1.Location = New System.Drawing.Point(0, 318)
        Me.AgSideBar1.Margin = New System.Windows.Forms.Padding(4)
        Me.AgSideBar1.Name = "AgSideBar1"
        Me.AgSideBar1.SelectedButton = Nothing
        Me.AgSideBar1.Size = New System.Drawing.Size(192, 91)
        Me.AgSideBar1.TabIndex = 6
        '
        'PnlSideBar
        '
        Me.PnlSideBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlSideBar.Controls.Add(Me.TreeView1)
        Me.PnlSideBar.Controls.Add(Me.splitter2)
        Me.PnlSideBar.Controls.Add(Me.AgSideBar1)
        Me.PnlSideBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlSideBar.Location = New System.Drawing.Point(3, 3)
        Me.PnlSideBar.Name = "PnlSideBar"
        Me.PnlSideBar.Size = New System.Drawing.Size(194, 411)
        Me.PnlSideBar.TabIndex = 7
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView1.ImageIndex = 1
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.Size = New System.Drawing.Size(192, 313)
        Me.TreeView1.TabIndex = 117
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Bigfolder.jpg")
        Me.ImageList1.Images.SetKeyName(1, "FolderYellow.jpg")
        '
        'splitter2
        '
        Me.splitter2.BackColor = System.Drawing.SystemColors.Window
        Me.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.splitter2.Location = New System.Drawing.Point(0, 313)
        Me.splitter2.MinExtra = 20
        Me.splitter2.MinSize = 32
        Me.splitter2.Name = "splitter2"
        Me.splitter2.Size = New System.Drawing.Size(192, 5)
        Me.splitter2.TabIndex = 116
        Me.splitter2.TabStop = False
        '
        'TbcMain
        '
        Me.TbcMain.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TbcMain.Controls.Add(Me.Tbp1)
        Me.TbcMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.TbcMain.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbcMain.ItemSize = New System.Drawing.Size(100, 20)
        Me.TbcMain.Location = New System.Drawing.Point(0, 0)
        Me.TbcMain.Multiline = True
        Me.TbcMain.Name = "TbcMain"
        Me.TbcMain.SelectedIndex = 0
        Me.TbcMain.Size = New System.Drawing.Size(228, 425)
        Me.TbcMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TbcMain.TabIndex = 13
        '
        'Tbp1
        '
        Me.Tbp1.AutoScroll = True
        Me.Tbp1.Controls.Add(Me.PnlSideBar)
        Me.Tbp1.Font = New System.Drawing.Font("Arial", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tbp1.ForeColor = System.Drawing.Color.Black
        Me.Tbp1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Tbp1.Location = New System.Drawing.Point(24, 4)
        Me.Tbp1.Name = "Tbp1"
        Me.Tbp1.Padding = New System.Windows.Forms.Padding(3)
        Me.Tbp1.Size = New System.Drawing.Size(200, 417)
        Me.Tbp1.TabIndex = 1
        Me.Tbp1.Text = "Menu"
        Me.Tbp1.UseVisualStyleBackColor = True
        '
        'splitter1
        '
        Me.splitter1.Location = New System.Drawing.Point(228, 0)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(2, 425)
        Me.splitter1.TabIndex = 14
        Me.splitter1.TabStop = False
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(864, 449)
        Me.Controls.Add(Me.splitter1)
        Me.Controls.Add(Me.TbcMain)
        Me.Controls.Add(Me.SSrpMain)
        Me.DoubleBuffered = True
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Name = "MDIMain"
        Me.Text = "KC"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SSrpMain.ResumeLayout(False)
        Me.SSrpMain.PerformLayout()
        Me.PnlSideBar.ResumeLayout(False)
        Me.TbcMain.ResumeLayout(False)
        Me.Tbp1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents SSrpMain As System.Windows.Forms.StatusStrip
    Friend WithEvents TSSL_Company As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSSL_User As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents TSSL_Btn_ManageMDI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSSL_Btn_ManageUserControl As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSSL_Btn_UpdateTableStructure As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSSL_UpdateTableStructureWebToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSSL_Site As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSSL_Btn_ReconnectDatabase As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSSL_OnlineOffLine As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents AgSideBar1 As AgControls.OutlookStyleControls.AgSideBar
    Friend WithEvents PnlSideBar As System.Windows.Forms.Panel
    Private WithEvents splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents TbcMain As System.Windows.Forms.TabControl
    Friend WithEvents Tbp1 As System.Windows.Forms.TabPage
    Private WithEvents splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents TSSL_Division As System.Windows.Forms.ToolStripStatusLabel

End Class
