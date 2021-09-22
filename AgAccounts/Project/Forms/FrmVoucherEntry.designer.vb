<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVoucherEntry
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVoucherEntry))
        Me.LblAcName = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.BtnPayments = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.GrpUP = New System.Windows.Forms.GroupBox()
        Me.TxtPrepared = New AgControls.AgTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TxtModified = New AgControls.AgTextBox()
        Me.LblBG = New System.Windows.Forms.Label()
        Me.LblDrName = New System.Windows.Forms.Label()
        Me.LblTotalName = New System.Windows.Forms.Label()
        Me.LblDrAmt = New System.Windows.Forms.Label()
        Me.Topctrl1 = New Topctrl.Topctrl()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.BtnJournal = New System.Windows.Forms.Button()
        Me.BtnReceipt = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LblCrAmt = New System.Windows.Forms.Label()
        Me.LblCrName = New System.Windows.Forms.Label()
        Me.LblCurrentType = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BtnPostedBy = New System.Windows.Forms.Button()
        Me.TxtPostedBy = New AgControls.AgTextBox()
        Me.LblBalance = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TxtNarration = New AgControls.AgTextBox()
        Me.LblDifferenceAmt = New System.Windows.Forms.Label()
        Me.LblDifferenceName = New System.Windows.Forms.Label()
        Me.LblFormBackColor = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblPtyBalance = New System.Windows.Forms.Label()
        Me.BtnImport = New System.Windows.Forms.Button()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.TTPMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnRefreshVNo = New System.Windows.Forms.Button()
        Me.BtnCopy = New System.Windows.Forms.Button()
        Me.BtnPaste = New System.Windows.Forms.Button()
        Me.TxtRecId = New AgControls.AgTextBox()
        Me.TxtType = New AgControls.AgTextBox()
        Me.TxtVDate = New AgControls.AgTextBox()
        Me.TxtVNo = New AgControls.AgTextBox()
        Me.TxtAcName = New AgControls.AgTextBox()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTallyLedgerOpening = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDosLedgerOpening = New System.Windows.Forms.ToolStripMenuItem()
        Me.GrpUP.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblAcName
        '
        Me.LblAcName.AutoSize = True
        Me.LblAcName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcName.Location = New System.Drawing.Point(120, 111)
        Me.LblAcName.Name = "LblAcName"
        Me.LblAcName.Size = New System.Drawing.Size(66, 16)
        Me.LblAcName.TabIndex = 16
        Me.LblAcName.Text = "A/c Name"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 16)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Narration"
        '
        'BtnPayments
        '
        Me.BtnPayments.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPayments.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnPayments.Location = New System.Drawing.Point(837, 60)
        Me.BtnPayments.Name = "BtnPayments"
        Me.BtnPayments.Size = New System.Drawing.Size(134, 27)
        Me.BtnPayments.TabIndex = 8
        Me.BtnPayments.TabStop = False
        Me.BtnPayments.Text = "&Payments"
        Me.BtnPayments.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(234, 111)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(10, 7)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "Ä"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(7, 567)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(970, 9)
        Me.GroupBox2.TabIndex = 28
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'PnlMain
        '
        Me.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlMain.Location = New System.Drawing.Point(12, 176)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(958, 355)
        Me.PnlMain.TabIndex = 6
        '
        'GrpUP
        '
        Me.GrpUP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpUP.Controls.Add(Me.TxtPrepared)
        Me.GrpUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpUP.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GrpUP.ForeColor = System.Drawing.Color.Maroon
        Me.GrpUP.Location = New System.Drawing.Point(12, 582)
        Me.GrpUP.Name = "GrpUP"
        Me.GrpUP.Size = New System.Drawing.Size(135, 51)
        Me.GrpUP.TabIndex = 30
        Me.GrpUP.TabStop = False
        Me.GrpUP.Tag = "TR"
        Me.GrpUP.Text = "Prepared By "
        '
        'TxtPrepared
        '
        Me.TxtPrepared.AgAllowUserToEnableMasterHelp = False
        Me.TxtPrepared.AgLastValueTag = Nothing
        Me.TxtPrepared.AgLastValueText = Nothing
        Me.TxtPrepared.AgMandatory = False
        Me.TxtPrepared.AgMasterHelp = False
        Me.TxtPrepared.AgNumberLeftPlaces = 0
        Me.TxtPrepared.AgNumberNegetiveAllow = False
        Me.TxtPrepared.AgNumberRightPlaces = 0
        Me.TxtPrepared.AgPickFromLastValue = False
        Me.TxtPrepared.AgRowFilter = ""
        Me.TxtPrepared.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPrepared.AgSelectedValue = Nothing
        Me.TxtPrepared.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPrepared.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPrepared.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtPrepared.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPrepared.Enabled = False
        Me.TxtPrepared.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrepared.Location = New System.Drawing.Point(7, 21)
        Me.TxtPrepared.Name = "TxtPrepared"
        Me.TxtPrepared.Size = New System.Drawing.Size(120, 18)
        Me.TxtPrepared.TabIndex = 6
        Me.TxtPrepared.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(225, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 16)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "V No."
        Me.Label2.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.TxtModified)
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox4.Location = New System.Drawing.Point(282, 582)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(135, 51)
        Me.GroupBox4.TabIndex = 35
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Tag = "TR"
        Me.GroupBox4.Text = "Modified By "
        '
        'TxtModified
        '
        Me.TxtModified.AgAllowUserToEnableMasterHelp = False
        Me.TxtModified.AgLastValueTag = Nothing
        Me.TxtModified.AgLastValueText = Nothing
        Me.TxtModified.AgMandatory = False
        Me.TxtModified.AgMasterHelp = False
        Me.TxtModified.AgNumberLeftPlaces = 0
        Me.TxtModified.AgNumberNegetiveAllow = False
        Me.TxtModified.AgNumberRightPlaces = 0
        Me.TxtModified.AgPickFromLastValue = False
        Me.TxtModified.AgRowFilter = ""
        Me.TxtModified.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtModified.AgSelectedValue = Nothing
        Me.TxtModified.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtModified.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtModified.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtModified.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtModified.Enabled = False
        Me.TxtModified.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModified.Location = New System.Drawing.Point(7, 21)
        Me.TxtModified.Name = "TxtModified"
        Me.TxtModified.Size = New System.Drawing.Size(120, 18)
        Me.TxtModified.TabIndex = 6
        Me.TxtModified.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblBG
        '
        Me.LblBG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblBG.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblBG.Location = New System.Drawing.Point(12, 532)
        Me.LblBG.Name = "LblBG"
        Me.LblBG.Size = New System.Drawing.Size(959, 26)
        Me.LblBG.TabIndex = 36
        '
        'LblDrName
        '
        Me.LblDrName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDrName.AutoSize = True
        Me.LblDrName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDrName.ForeColor = System.Drawing.Color.Maroon
        Me.LblDrName.Location = New System.Drawing.Point(564, 537)
        Me.LblDrName.Name = "LblDrName"
        Me.LblDrName.Size = New System.Drawing.Size(30, 16)
        Me.LblDrName.TabIndex = 38
        Me.LblDrName.Text = "Dr :"
        '
        'LblTotalName
        '
        Me.LblTotalName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTotalName.AutoSize = True
        Me.LblTotalName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalName.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalName.Location = New System.Drawing.Point(20, 537)
        Me.LblTotalName.Name = "LblTotalName"
        Me.LblTotalName.Size = New System.Drawing.Size(39, 16)
        Me.LblTotalName.TabIndex = 39
        Me.LblTotalName.Text = "Total"
        '
        'LblDrAmt
        '
        Me.LblDrAmt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDrAmt.AutoSize = True
        Me.LblDrAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDrAmt.ForeColor = System.Drawing.Color.Black
        Me.LblDrAmt.Location = New System.Drawing.Point(595, 537)
        Me.LblDrAmt.Name = "LblDrAmt"
        Me.LblDrAmt.Size = New System.Drawing.Size(15, 16)
        Me.LblDrAmt.TabIndex = 40
        Me.LblDrAmt.Text = "0"
        '
        'Topctrl1
        '
        Me.Topctrl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(982, 41)
        Me.Topctrl1.TabIndex = 7
        Me.Topctrl1.tAdd = True
        Me.Topctrl1.tCancel = True
        Me.Topctrl1.tDel = True
        Me.Topctrl1.tDiscard = False
        Me.Topctrl1.tEdit = True
        Me.Topctrl1.tExit = True
        Me.Topctrl1.tFind = True
        Me.Topctrl1.tFirst = True
        Me.Topctrl1.tLast = True
        Me.Topctrl1.tNext = True
        Me.Topctrl1.tPrev = True
        Me.Topctrl1.tPrn = True
        Me.Topctrl1.tRef = True
        Me.Topctrl1.tSave = False
        Me.Topctrl1.tSite = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(457, 91)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 7)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Ä"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(399, 91)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(35, 16)
        Me.Label13.TabIndex = 43
        Me.Label13.Text = "Date"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(234, 71)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(10, 7)
        Me.Label16.TabIndex = 47
        Me.Label16.Text = "Ä"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(120, 71)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(35, 16)
        Me.Label17.TabIndex = 46
        Me.Label17.Text = "Type"
        '
        'BtnJournal
        '
        Me.BtnJournal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnJournal.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnJournal.Location = New System.Drawing.Point(837, 116)
        Me.BtnJournal.Name = "BtnJournal"
        Me.BtnJournal.Size = New System.Drawing.Size(134, 27)
        Me.BtnJournal.TabIndex = 10
        Me.BtnJournal.TabStop = False
        Me.BtnJournal.Text = "&Journal"
        Me.BtnJournal.UseVisualStyleBackColor = True
        '
        'BtnReceipt
        '
        Me.BtnReceipt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnReceipt.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnReceipt.Location = New System.Drawing.Point(837, 88)
        Me.BtnReceipt.Name = "BtnReceipt"
        Me.BtnReceipt.Size = New System.Drawing.Size(134, 27)
        Me.BtnReceipt.TabIndex = 9
        Me.BtnReceipt.TabStop = False
        Me.BtnReceipt.Text = "&Receipts"
        Me.BtnReceipt.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(7, 157)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(970, 9)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Tag = ""
        '
        'LblCrAmt
        '
        Me.LblCrAmt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCrAmt.AutoSize = True
        Me.LblCrAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCrAmt.ForeColor = System.Drawing.Color.Black
        Me.LblCrAmt.Location = New System.Drawing.Point(777, 537)
        Me.LblCrAmt.Name = "LblCrAmt"
        Me.LblCrAmt.Size = New System.Drawing.Size(15, 16)
        Me.LblCrAmt.TabIndex = 52
        Me.LblCrAmt.Text = "0"
        '
        'LblCrName
        '
        Me.LblCrName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCrName.AutoSize = True
        Me.LblCrName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCrName.ForeColor = System.Drawing.Color.Maroon
        Me.LblCrName.Location = New System.Drawing.Point(746, 537)
        Me.LblCrName.Name = "LblCrName"
        Me.LblCrName.Size = New System.Drawing.Size(30, 16)
        Me.LblCrName.TabIndex = 51
        Me.LblCrName.Text = "Cr :"
        '
        'LblCurrentType
        '
        Me.LblCurrentType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrentType.AutoSize = True
        Me.LblCurrentType.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblCurrentType.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.LblCurrentType.Location = New System.Drawing.Point(625, 81)
        Me.LblCurrentType.Name = "LblCurrentType"
        Me.LblCurrentType.Size = New System.Drawing.Size(174, 39)
        Me.LblCurrentType.TabIndex = 53
        Me.LblCurrentType.Text = "Payments"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.BtnPostedBy)
        Me.GroupBox3.Controls.Add(Me.TxtPostedBy)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox3.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox3.Location = New System.Drawing.Point(560, 582)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(135, 51)
        Me.GroupBox3.TabIndex = 54
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Tag = "UP"
        Me.GroupBox3.Text = "Posted By "
        '
        'BtnPostedBy
        '
        Me.BtnPostedBy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPostedBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPostedBy.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnPostedBy.Image = CType(resources.GetObject("BtnPostedBy.Image"), System.Drawing.Image)
        Me.BtnPostedBy.Location = New System.Drawing.Point(7, 19)
        Me.BtnPostedBy.Name = "BtnPostedBy"
        Me.BtnPostedBy.Size = New System.Drawing.Size(23, 23)
        Me.BtnPostedBy.TabIndex = 0
        Me.BtnPostedBy.UseVisualStyleBackColor = True
        '
        'TxtPostedBy
        '
        Me.TxtPostedBy.AgAllowUserToEnableMasterHelp = False
        Me.TxtPostedBy.AgLastValueTag = Nothing
        Me.TxtPostedBy.AgLastValueText = Nothing
        Me.TxtPostedBy.AgMandatory = False
        Me.TxtPostedBy.AgMasterHelp = False
        Me.TxtPostedBy.AgNumberLeftPlaces = 0
        Me.TxtPostedBy.AgNumberNegetiveAllow = False
        Me.TxtPostedBy.AgNumberRightPlaces = 0
        Me.TxtPostedBy.AgPickFromLastValue = False
        Me.TxtPostedBy.AgRowFilter = ""
        Me.TxtPostedBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPostedBy.AgSelectedValue = Nothing
        Me.TxtPostedBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPostedBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPostedBy.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtPostedBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPostedBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPostedBy.Location = New System.Drawing.Point(36, 21)
        Me.TxtPostedBy.Name = "TxtPostedBy"
        Me.TxtPostedBy.Size = New System.Drawing.Size(92, 18)
        Me.TxtPostedBy.TabIndex = 1
        Me.TxtPostedBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblBalance
        '
        Me.LblBalance.AutoSize = True
        Me.LblBalance.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBalance.ForeColor = System.Drawing.Color.Maroon
        Me.LblBalance.Location = New System.Drawing.Point(249, 129)
        Me.LblBalance.Name = "LblBalance"
        Me.LblBalance.Size = New System.Drawing.Size(51, 16)
        Me.LblBalance.TabIndex = 55
        Me.LblBalance.Text = "Dr 0.00"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.TxtNarration)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Location = New System.Drawing.Point(77, 71)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(70, 81)
        Me.GroupBox5.TabIndex = 56
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Garbage"
        Me.GroupBox5.Visible = False
        '
        'TxtNarration
        '
        Me.TxtNarration.AgAllowUserToEnableMasterHelp = False
        Me.TxtNarration.AgLastValueTag = Nothing
        Me.TxtNarration.AgLastValueText = Nothing
        Me.TxtNarration.AgMandatory = False
        Me.TxtNarration.AgMasterHelp = False
        Me.TxtNarration.AgNumberLeftPlaces = 0
        Me.TxtNarration.AgNumberNegetiveAllow = False
        Me.TxtNarration.AgNumberRightPlaces = 0
        Me.TxtNarration.AgPickFromLastValue = False
        Me.TxtNarration.AgRowFilter = ""
        Me.TxtNarration.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNarration.AgSelectedValue = Nothing
        Me.TxtNarration.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNarration.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNarration.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNarration.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNarration.Location = New System.Drawing.Point(9, 33)
        Me.TxtNarration.MaxLength = 255
        Me.TxtNarration.Multiline = True
        Me.TxtNarration.Name = "TxtNarration"
        Me.TxtNarration.Size = New System.Drawing.Size(55, 42)
        Me.TxtNarration.TabIndex = 5
        '
        'LblDifferenceAmt
        '
        Me.LblDifferenceAmt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDifferenceAmt.AutoSize = True
        Me.LblDifferenceAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDifferenceAmt.ForeColor = System.Drawing.Color.Black
        Me.LblDifferenceAmt.Location = New System.Drawing.Point(410, 537)
        Me.LblDifferenceAmt.Name = "LblDifferenceAmt"
        Me.LblDifferenceAmt.Size = New System.Drawing.Size(15, 16)
        Me.LblDifferenceAmt.TabIndex = 58
        Me.LblDifferenceAmt.Text = "0"
        '
        'LblDifferenceName
        '
        Me.LblDifferenceName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDifferenceName.AutoSize = True
        Me.LblDifferenceName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDifferenceName.ForeColor = System.Drawing.Color.Maroon
        Me.LblDifferenceName.Location = New System.Drawing.Point(328, 537)
        Me.LblDifferenceName.Name = "LblDifferenceName"
        Me.LblDifferenceName.Size = New System.Drawing.Size(81, 16)
        Me.LblDifferenceName.TabIndex = 57
        Me.LblDifferenceName.Text = "Difference :"
        '
        'LblFormBackColor
        '
        Me.LblFormBackColor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblFormBackColor.AutoSize = True
        Me.LblFormBackColor.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblFormBackColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFormBackColor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.LblFormBackColor.Location = New System.Drawing.Point(580, 43)
        Me.LblFormBackColor.Name = "LblFormBackColor"
        Me.LblFormBackColor.Size = New System.Drawing.Size(217, 31)
        Me.LblFormBackColor.TabIndex = 59
        Me.LblFormBackColor.Text = "Form Back Color"
        Me.LblFormBackColor.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(120, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 16)
        Me.Label1.TabIndex = 61
        Me.Label1.Text = "Voucher No."
        '
        'LblPtyBalance
        '
        Me.LblPtyBalance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblPtyBalance.AutoSize = True
        Me.LblPtyBalance.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPtyBalance.ForeColor = System.Drawing.Color.Black
        Me.LblPtyBalance.Location = New System.Drawing.Point(147, 537)
        Me.LblPtyBalance.Name = "LblPtyBalance"
        Me.LblPtyBalance.Size = New System.Drawing.Size(0, 16)
        Me.LblPtyBalance.TabIndex = 63
        '
        'BtnImport
        '
        Me.BtnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImport.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnImport.Image = CType(resources.GetObject("BtnImport.Image"), System.Drawing.Image)
        Me.BtnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnImport.Location = New System.Drawing.Point(831, 588)
        Me.BtnImport.Name = "BtnImport"
        Me.BtnImport.Size = New System.Drawing.Size(146, 44)
        Me.BtnImport.TabIndex = 11
        Me.BtnImport.TabStop = False
        Me.BtnImport.Text = "&Import Voucher"
        Me.BtnImport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TTPMain.SetToolTip(Me.BtnImport, "ACCODE | DR | CR | NARRATION | CHQ_NO | CHQ_DT")
        Me.BtnImport.UseVisualStyleBackColor = True
        Me.BtnImport.Visible = False
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'TTPMain
        '
        Me.TTPMain.BackColor = System.Drawing.Color.Transparent
        Me.TTPMain.IsBalloon = True
        Me.TTPMain.ShowAlways = True
        Me.TTPMain.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.TTPMain.ToolTipTitle = "Format Of Excel Sheet Should Be : -"
        '
        'BtnRefreshVNo
        '
        Me.BtnRefreshVNo.BackgroundImage = CType(resources.GetObject("BtnRefreshVNo.BackgroundImage"), System.Drawing.Image)
        Me.BtnRefreshVNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnRefreshVNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRefreshVNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRefreshVNo.Location = New System.Drawing.Point(368, 90)
        Me.BtnRefreshVNo.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnRefreshVNo.Name = "BtnRefreshVNo"
        Me.BtnRefreshVNo.Size = New System.Drawing.Size(17, 21)
        Me.BtnRefreshVNo.TabIndex = 64
        Me.BtnRefreshVNo.TabStop = False
        Me.BtnRefreshVNo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnRefreshVNo.UseVisualStyleBackColor = True
        '
        'BtnCopy
        '
        Me.BtnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCopy.Location = New System.Drawing.Point(1, 43)
        Me.BtnCopy.Name = "BtnCopy"
        Me.BtnCopy.Size = New System.Drawing.Size(44, 25)
        Me.BtnCopy.TabIndex = 65
        Me.BtnCopy.TabStop = False
        Me.BtnCopy.Text = "&Copy"
        Me.BtnCopy.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCopy.UseVisualStyleBackColor = True
        '
        'BtnPaste
        '
        Me.BtnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPaste.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPaste.Location = New System.Drawing.Point(1, 43)
        Me.BtnPaste.Name = "BtnPaste"
        Me.BtnPaste.Size = New System.Drawing.Size(44, 25)
        Me.BtnPaste.TabIndex = 66
        Me.BtnPaste.TabStop = False
        Me.BtnPaste.Text = "Pas&te"
        Me.BtnPaste.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPaste.UseVisualStyleBackColor = True
        Me.BtnPaste.Visible = False
        '
        'TxtRecId
        '
        Me.TxtRecId.AgAllowUserToEnableMasterHelp = False
        Me.TxtRecId.AgLastValueTag = Nothing
        Me.TxtRecId.AgLastValueText = Nothing
        Me.TxtRecId.AgMandatory = False
        Me.TxtRecId.AgMasterHelp = False
        Me.TxtRecId.AgNumberLeftPlaces = 0
        Me.TxtRecId.AgNumberNegetiveAllow = False
        Me.TxtRecId.AgNumberRightPlaces = 0
        Me.TxtRecId.AgPickFromLastValue = False
        Me.TxtRecId.AgRowFilter = ""
        Me.TxtRecId.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRecId.AgSelectedValue = Nothing
        Me.TxtRecId.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRecId.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRecId.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRecId.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRecId.Location = New System.Drawing.Point(249, 91)
        Me.TxtRecId.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtRecId.MaxLength = 10
        Me.TxtRecId.Name = "TxtRecId"
        Me.TxtRecId.Size = New System.Drawing.Size(118, 18)
        Me.TxtRecId.TabIndex = 1
        '
        'TxtType
        '
        Me.TxtType.AgAllowUserToEnableMasterHelp = False
        Me.TxtType.AgLastValueTag = Nothing
        Me.TxtType.AgLastValueText = Nothing
        Me.TxtType.AgMandatory = False
        Me.TxtType.AgMasterHelp = False
        Me.TxtType.AgNumberLeftPlaces = 0
        Me.TxtType.AgNumberNegetiveAllow = False
        Me.TxtType.AgNumberRightPlaces = 0
        Me.TxtType.AgPickFromLastValue = False
        Me.TxtType.AgRowFilter = ""
        Me.TxtType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtType.AgSelectedValue = Nothing
        Me.TxtType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtType.Location = New System.Drawing.Point(249, 71)
        Me.TxtType.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtType.MaxLength = 15
        Me.TxtType.Name = "TxtType"
        Me.TxtType.Size = New System.Drawing.Size(341, 18)
        Me.TxtType.TabIndex = 0
        '
        'TxtVDate
        '
        Me.TxtVDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtVDate.AgLastValueTag = Nothing
        Me.TxtVDate.AgLastValueText = Nothing
        Me.TxtVDate.AgMandatory = False
        Me.TxtVDate.AgMasterHelp = False
        Me.TxtVDate.AgNumberLeftPlaces = 0
        Me.TxtVDate.AgNumberNegetiveAllow = False
        Me.TxtVDate.AgNumberRightPlaces = 0
        Me.TxtVDate.AgPickFromLastValue = False
        Me.TxtVDate.AgRowFilter = ""
        Me.TxtVDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVDate.AgSelectedValue = Nothing
        Me.TxtVDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVDate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVDate.Location = New System.Drawing.Point(472, 91)
        Me.TxtVDate.Name = "TxtVDate"
        Me.TxtVDate.Size = New System.Drawing.Size(118, 18)
        Me.TxtVDate.TabIndex = 2
        '
        'TxtVNo
        '
        Me.TxtVNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtVNo.AgLastValueTag = Nothing
        Me.TxtVNo.AgLastValueText = Nothing
        Me.TxtVNo.AgMandatory = False
        Me.TxtVNo.AgMasterHelp = False
        Me.TxtVNo.AgNumberLeftPlaces = 0
        Me.TxtVNo.AgNumberNegetiveAllow = False
        Me.TxtVNo.AgNumberRightPlaces = 0
        Me.TxtVNo.AgPickFromLastValue = False
        Me.TxtVNo.AgRowFilter = ""
        Me.TxtVNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVNo.AgSelectedValue = Nothing
        Me.TxtVNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVNo.Location = New System.Drawing.Point(272, 43)
        Me.TxtVNo.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtVNo.MaxLength = 10
        Me.TxtVNo.Name = "TxtVNo"
        Me.TxtVNo.Size = New System.Drawing.Size(118, 18)
        Me.TxtVNo.TabIndex = 1
        Me.TxtVNo.Visible = False
        '
        'TxtAcName
        '
        Me.TxtAcName.AgAllowUserToEnableMasterHelp = False
        Me.TxtAcName.AgLastValueTag = Nothing
        Me.TxtAcName.AgLastValueText = Nothing
        Me.TxtAcName.AgMandatory = False
        Me.TxtAcName.AgMasterHelp = False
        Me.TxtAcName.AgNumberLeftPlaces = 0
        Me.TxtAcName.AgNumberNegetiveAllow = False
        Me.TxtAcName.AgNumberRightPlaces = 0
        Me.TxtAcName.AgPickFromLastValue = False
        Me.TxtAcName.AgRowFilter = ""
        Me.TxtAcName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcName.AgSelectedValue = Nothing
        Me.TxtAcName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcName.Location = New System.Drawing.Point(249, 111)
        Me.TxtAcName.Name = "TxtAcName"
        Me.TxtAcName.Size = New System.Drawing.Size(341, 18)
        Me.TxtAcName.TabIndex = 5
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuImportFromTallyLedgerOpening, Me.MnuImportFromDosLedgerOpening})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(265, 136)
        Me.MnuOptions.Text = "Option"
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(264, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(264, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(264, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuImportFromTallyLedgerOpening
        '
        Me.MnuImportFromTallyLedgerOpening.Name = "MnuImportFromTallyLedgerOpening"
        Me.MnuImportFromTallyLedgerOpening.Size = New System.Drawing.Size(264, 22)
        Me.MnuImportFromTallyLedgerOpening.Text = "Import From Tally (Ledger Opening)"
        '
        'MnuImportFromDosLedgerOpening
        '
        Me.MnuImportFromDosLedgerOpening.Name = "MnuImportFromDosLedgerOpening"
        Me.MnuImportFromDosLedgerOpening.Size = New System.Drawing.Size(264, 22)
        Me.MnuImportFromDosLedgerOpening.Text = "Import From Dos (Ledger Opening)"
        '
        'FrmVoucherEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(982, 638)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.BtnPaste)
        Me.Controls.Add(Me.BtnCopy)
        Me.Controls.Add(Me.BtnRefreshVNo)
        Me.Controls.Add(Me.BtnImport)
        Me.Controls.Add(Me.LblPtyBalance)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtRecId)
        Me.Controls.Add(Me.LblFormBackColor)
        Me.Controls.Add(Me.LblDifferenceAmt)
        Me.Controls.Add(Me.LblDifferenceName)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.LblBalance)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.LblCurrentType)
        Me.Controls.Add(Me.LblCrAmt)
        Me.Controls.Add(Me.LblCrName)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnReceipt)
        Me.Controls.Add(Me.BtnJournal)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.TxtType)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TxtVDate)
        Me.Controls.Add(Me.LblDrAmt)
        Me.Controls.Add(Me.LblTotalName)
        Me.Controls.Add(Me.LblDrName)
        Me.Controls.Add(Me.LblBG)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtVNo)
        Me.Controls.Add(Me.GrpUP)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.BtnPayments)
        Me.Controls.Add(Me.LblAcName)
        Me.Controls.Add(Me.TxtAcName)
        Me.Controls.Add(Me.Topctrl1)
        Me.KeyPreview = True
        Me.Name = "FrmVoucherEntry"
        Me.ShowIcon = False
        Me.Tag = "BG"
        Me.Text = "Voucher Entry"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Topctrl1 As Topctrl.Topctrl
    Public WithEvents TxtAcName As AgControls.AgTextBox
    Public WithEvents LblAcName As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents TxtNarration As AgControls.AgTextBox
    Public WithEvents BtnPayments As System.Windows.Forms.Button
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents PnlMain As System.Windows.Forms.Panel
    Public WithEvents GrpUP As System.Windows.Forms.GroupBox
    Public WithEvents TxtPrepared As AgControls.AgTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents TxtVNo As AgControls.AgTextBox
    Public WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents TxtModified As AgControls.AgTextBox
    Public WithEvents LblBG As System.Windows.Forms.Label
    Public WithEvents LblDrName As System.Windows.Forms.Label
    Public WithEvents LblTotalName As System.Windows.Forms.Label
    Public WithEvents LblDrAmt As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents TxtVDate As AgControls.AgTextBox
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents TxtType As AgControls.AgTextBox
    Public WithEvents BtnJournal As System.Windows.Forms.Button
    Public WithEvents BtnReceipt As System.Windows.Forms.Button
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents LblCrAmt As System.Windows.Forms.Label
    Public WithEvents LblCrName As System.Windows.Forms.Label
    Public WithEvents LblCurrentType As System.Windows.Forms.Label
    Public WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents BtnPostedBy As System.Windows.Forms.Button
    Public WithEvents TxtPostedBy As AgControls.AgTextBox
    Public WithEvents LblBalance As System.Windows.Forms.Label
    Public WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Public WithEvents LblDifferenceAmt As System.Windows.Forms.Label
    Public WithEvents LblDifferenceName As System.Windows.Forms.Label
    Public WithEvents LblFormBackColor As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtRecId As AgControls.AgTextBox
    Public WithEvents LblPtyBalance As System.Windows.Forms.Label
    Public WithEvents BtnImport As System.Windows.Forms.Button
    Public WithEvents OFDMain As System.Windows.Forms.OpenFileDialog
    Public WithEvents TTPMain As System.Windows.Forms.ToolTip
    Public WithEvents BtnRefreshVNo As System.Windows.Forms.Button
    Public WithEvents BtnCopy As System.Windows.Forms.Button
    Public WithEvents BtnPaste As System.Windows.Forms.Button
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Friend WithEvents MnuImportFromTallyLedgerOpening As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Friend WithEvents MnuImportFromDosLedgerOpening As ToolStripMenuItem
End Class
