<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MDIMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIMain))
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SSrpMain = New System.Windows.Forms.StatusStrip()
        Me.TSSL_Company = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSL_Site = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSL_User = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSL_Division = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.TSSL_Btn_UpdateTableStructure = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSSL_Btn_ManageMDI = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSSL_Btn_ManageUserControl = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSSL_Btn_ReconnectDatabase = New System.Windows.Forms.ToolStripMenuItem()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.splitter2 = New System.Windows.Forms.Splitter()
        Me.splitter1 = New System.Windows.Forms.Splitter()
        Me.SSrpMain.SuspendLayout()
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
        Me.SSrpMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSSL_Company, Me.TSSL_Site, Me.TSSL_User, Me.TSSL_Division, Me.ToolStripSplitButton1})
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
        Me.TSSL_Company.Size = New System.Drawing.Size(236, 19)
        Me.TSSL_Company.Spring = True
        Me.TSSL_Company.Text = "Company Name"
        Me.TSSL_Company.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TSSL_Company.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'TSSL_Site
        '
        Me.TSSL_Site.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TSSL_Site.Name = "TSSL_Site"
        Me.TSSL_Site.Size = New System.Drawing.Size(236, 19)
        Me.TSSL_Site.Spring = True
        Me.TSSL_Site.Text = "Site Name"
        Me.TSSL_Site.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TSSL_User
        '
        Me.TSSL_User.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TSSL_User.Name = "TSSL_User"
        Me.TSSL_User.Size = New System.Drawing.Size(236, 19)
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
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSSL_Btn_UpdateTableStructure, Me.TSSL_UpdateTableStructureWebToolStripMenuItem, Me.ToolStripMenuItem1, Me.TSSL_Btn_ManageMDI, Me.TSSL_Btn_ManageUserControl, Me.ToolStripMenuItem2, Me.TSSL_Btn_ReconnectDatabase})
        Me.ToolStripSplitButton1.Image = CType(resources.GetObject("ToolStripSplitButton1.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(90, 22)
        Me.ToolStripSplitButton1.Text = "Master Tools"
        '
        'TSSL_Btn_UpdateTableStructure
        '
        Me.TSSL_Btn_UpdateTableStructure.Name = "TSSL_Btn_UpdateTableStructure"
        Me.TSSL_Btn_UpdateTableStructure.Size = New System.Drawing.Size(221, 22)
        Me.TSSL_Btn_UpdateTableStructure.Text = "Update Table Structure"
        '
        'TSSL_UpdateTableStructureWebToolStripMenuItem
        '
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem.Name = "TSSL_UpdateTableStructureWebToolStripMenuItem"
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem.Text = "Update Table Structure Web"
        Me.TSSL_UpdateTableStructureWebToolStripMenuItem.Visible = False
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(218, 6)
        '
        'TSSL_Btn_ManageMDI
        '
        Me.TSSL_Btn_ManageMDI.Name = "TSSL_Btn_ManageMDI"
        Me.TSSL_Btn_ManageMDI.Size = New System.Drawing.Size(221, 22)
        Me.TSSL_Btn_ManageMDI.Text = "Manage MDI"
        '
        'TSSL_Btn_ManageUserControl
        '
        Me.TSSL_Btn_ManageUserControl.Name = "TSSL_Btn_ManageUserControl"
        Me.TSSL_Btn_ManageUserControl.Size = New System.Drawing.Size(221, 22)
        Me.TSSL_Btn_ManageUserControl.Text = "Manage User Control"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(218, 6)
        '
        'TSSL_Btn_ReconnectDatabase
        '
        Me.TSSL_Btn_ReconnectDatabase.Name = "TSSL_Btn_ReconnectDatabase"
        Me.TSSL_Btn_ReconnectDatabase.Size = New System.Drawing.Size(221, 22)
        Me.TSSL_Btn_ReconnectDatabase.Text = "Reconnect Database"
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
        'splitter1
        '
        Me.splitter1.Location = New System.Drawing.Point(0, 0)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(2, 425)
        Me.splitter1.TabIndex = 14
        Me.splitter1.TabStop = False
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(864, 449)
        Me.Controls.Add(Me.splitter1)
        Me.Controls.Add(Me.SSrpMain)
        Me.DoubleBuffered = True
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Name = "MDIMain"
        Me.Text = "Auditor9"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SSrpMain.ResumeLayout(False)
        Me.SSrpMain.PerformLayout()
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


    Private WithEvents splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents TSSL_Division As System.Windows.Forms.ToolStripStatusLabel
End Class
