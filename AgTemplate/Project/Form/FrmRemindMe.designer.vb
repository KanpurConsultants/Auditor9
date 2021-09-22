<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRemindMe
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If Disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.          [Ag]
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.TxtId = New AgControls.AgTextBox
        Me.LblID = New System.Windows.Forms.Label
        Me.LblRemindeBy = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.LblRemindOnDateTime = New System.Windows.Forms.Label
        Me.BtnOK = New System.Windows.Forms.Button
        Me.DODPeriodValue = New System.Windows.Forms.DomainUpDown
        Me.TxtReminderTime = New AgControls.AgTextBox
        Me.TxtReminderDate = New AgControls.AgTextBox
        Me.TxtNarration = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DUDPeriodText = New System.Windows.Forms.DomainUpDown
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '

        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(454, 41)
        Me.Topctrl1.TabIndex = 3
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
        Me.Topctrl1.Visible = False
        '
        'TxtId
        '
        Me.TxtId.AgMandatory = True
        Me.TxtId.AgMasterHelp = True
        Me.TxtId.AgNumberLeftPlaces = 0
        Me.TxtId.AgNumberNegetiveAllow = False
        Me.TxtId.AgNumberRightPlaces = 0
        Me.TxtId.AgPickFromLastValue = False
        Me.TxtId.AgRowFilter = ""

        Me.TxtId.AgSelectedValue = Nothing
        Me.TxtId.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtId.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtId.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtId.Location = New System.Drawing.Point(52, 140)
        Me.TxtId.MaxLength = 10
        Me.TxtId.Name = "TxtId"
        Me.TxtId.Size = New System.Drawing.Size(100, 21)
        Me.TxtId.TabIndex = 0
        '
        'LblID
        '
        Me.LblID.AutoSize = True
        Me.LblID.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblID.Location = New System.Drawing.Point(25, 143)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(21, 13)
        Me.LblID.TabIndex = 0
        Me.LblID.Text = "ID"
        '
        'LblRemindeBy
        '
        Me.LblRemindeBy.AutoSize = True
        Me.LblRemindeBy.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemindeBy.ForeColor = System.Drawing.Color.Black
        Me.LblRemindeBy.Location = New System.Drawing.Point(77, 17)
        Me.LblRemindeBy.Name = "LblRemindeBy"
        Me.LblRemindeBy.Size = New System.Drawing.Size(75, 16)
        Me.LblRemindeBy.TabIndex = 7
        Me.LblRemindeBy.Text = "Remind By"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.LblRemindOnDateTime)
        Me.Panel1.Controls.Add(Me.LblRemindeBy)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(454, 56)
        Me.Panel1.TabIndex = 12
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = My.Resources.Resources.clock
        Me.PictureBox1.Location = New System.Drawing.Point(16, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(44, 44)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'LblRemindOnDateTime
        '
        Me.LblRemindOnDateTime.AutoSize = True
        Me.LblRemindOnDateTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemindOnDateTime.ForeColor = System.Drawing.Color.Black
        Me.LblRemindOnDateTime.Location = New System.Drawing.Point(288, 18)
        Me.LblRemindOnDateTime.Name = "LblRemindOnDateTime"
        Me.LblRemindOnDateTime.Size = New System.Drawing.Size(110, 14)
        Me.LblRemindOnDateTime.TabIndex = 8
        Me.LblRemindOnDateTime.Text = "RemindDateTime"
        '
        'BtnOK
        '
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(376, 263)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 24)
        Me.BtnOK.TabIndex = 13
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'DODPeriodValue
        '
        Me.DODPeriodValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DODPeriodValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DODPeriodValue.Items.Add("0")
        Me.DODPeriodValue.Items.Add("1")
        Me.DODPeriodValue.Items.Add("2")
        Me.DODPeriodValue.Items.Add("3")
        Me.DODPeriodValue.Items.Add("4")
        Me.DODPeriodValue.Items.Add("5")
        Me.DODPeriodValue.Items.Add("6")
        Me.DODPeriodValue.Items.Add("7")
        Me.DODPeriodValue.Items.Add("8")
        Me.DODPeriodValue.Items.Add("9")
        Me.DODPeriodValue.Items.Add("10")
        Me.DODPeriodValue.Items.Add("11")
        Me.DODPeriodValue.Items.Add("12")
        Me.DODPeriodValue.Items.Add("13")
        Me.DODPeriodValue.Items.Add("14")
        Me.DODPeriodValue.Items.Add("15")
        Me.DODPeriodValue.Items.Add("16")
        Me.DODPeriodValue.Items.Add("17")
        Me.DODPeriodValue.Items.Add("18")
        Me.DODPeriodValue.Items.Add("19")
        Me.DODPeriodValue.Items.Add("20")
        Me.DODPeriodValue.Items.Add("21")
        Me.DODPeriodValue.Items.Add("22")
        Me.DODPeriodValue.Items.Add("23")
        Me.DODPeriodValue.Items.Add("24")
        Me.DODPeriodValue.Items.Add("25")
        Me.DODPeriodValue.Items.Add("26")
        Me.DODPeriodValue.Items.Add("27")
        Me.DODPeriodValue.Items.Add("28")
        Me.DODPeriodValue.Items.Add("29")
        Me.DODPeriodValue.Items.Add("30")
        Me.DODPeriodValue.Items.Add("31")
        Me.DODPeriodValue.Location = New System.Drawing.Point(161, 262)
        Me.DODPeriodValue.Name = "DODPeriodValue"
        Me.DODPeriodValue.Size = New System.Drawing.Size(51, 22)
        Me.DODPeriodValue.TabIndex = 21
        Me.DODPeriodValue.Text = "DomainUpDown1"
        '
        'TxtReminderTime
        '
        Me.TxtReminderTime.AgMandatory = False
        Me.TxtReminderTime.AgMasterHelp = False
        Me.TxtReminderTime.AgNumberLeftPlaces = 0
        Me.TxtReminderTime.AgNumberNegetiveAllow = False
        Me.TxtReminderTime.AgNumberRightPlaces = 0
        Me.TxtReminderTime.AgPickFromLastValue = False
        Me.TxtReminderTime.AgRowFilter = ""

        Me.TxtReminderTime.AgSelectedValue = Nothing
        Me.TxtReminderTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReminderTime.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReminderTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReminderTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReminderTime.Location = New System.Drawing.Point(106, 320)
        Me.TxtReminderTime.MaxLength = 100
        Me.TxtReminderTime.Name = "TxtReminderTime"
        Me.TxtReminderTime.Size = New System.Drawing.Size(35, 18)
        Me.TxtReminderTime.TabIndex = 18
        Me.TxtReminderTime.Visible = False
        '
        'TxtReminderDate
        '
        Me.TxtReminderDate.AgMandatory = False
        Me.TxtReminderDate.AgMasterHelp = False
        Me.TxtReminderDate.AgNumberLeftPlaces = 0
        Me.TxtReminderDate.AgNumberNegetiveAllow = False
        Me.TxtReminderDate.AgNumberRightPlaces = 0
        Me.TxtReminderDate.AgPickFromLastValue = False
        Me.TxtReminderDate.AgRowFilter = ""

        Me.TxtReminderDate.AgSelectedValue = Nothing
        Me.TxtReminderDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReminderDate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReminderDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReminderDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReminderDate.Location = New System.Drawing.Point(80, 320)
        Me.TxtReminderDate.MaxLength = 100
        Me.TxtReminderDate.Name = "TxtReminderDate"
        Me.TxtReminderDate.Size = New System.Drawing.Size(35, 18)
        Me.TxtReminderDate.TabIndex = 17
        Me.TxtReminderDate.Visible = False
        '
        'TxtNarration
        '
        Me.TxtNarration.AgMandatory = True
        Me.TxtNarration.AgMasterHelp = True
        Me.TxtNarration.AgNumberLeftPlaces = 0
        Me.TxtNarration.AgNumberNegetiveAllow = False
        Me.TxtNarration.AgNumberRightPlaces = 0
        Me.TxtNarration.AgPickFromLastValue = False
        Me.TxtNarration.AgRowFilter = ""

        Me.TxtNarration.AgSelectedValue = Nothing
        Me.TxtNarration.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNarration.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNarration.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNarration.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNarration.Location = New System.Drawing.Point(1, 84)
        Me.TxtNarration.MaxLength = 0
        Me.TxtNarration.Multiline = True
        Me.TxtNarration.Name = "TxtNarration"
        Me.TxtNarration.Size = New System.Drawing.Size(453, 161)
        Me.TxtNarration.TabIndex = 6
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(1, 60)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(138, 20)
        Me.LinkLabel1.TabIndex = 738
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Reminder Description"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Location = New System.Drawing.Point(88, 68)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(366, 4)
        Me.GroupBox1.TabIndex = 739
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 251)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(453, 4)
        Me.GroupBox2.TabIndex = 740
        Me.GroupBox2.TabStop = False
        '
        'DUDPeriodText
        '
        Me.DUDPeriodText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DUDPeriodText.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DUDPeriodText.Items.Add("Hour")
        Me.DUDPeriodText.Items.Add("Day")
        Me.DUDPeriodText.Items.Add("Week")
        Me.DUDPeriodText.Items.Add("Month")
        Me.DUDPeriodText.Items.Add("Year")
        Me.DUDPeriodText.Location = New System.Drawing.Point(218, 261)
        Me.DUDPeriodText.Name = "DUDPeriodText"
        Me.DUDPeriodText.Size = New System.Drawing.Size(62, 22)
        Me.DUDPeriodText.TabIndex = 741
        Me.DUDPeriodText.Text = "DomainUpDown1"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LinkLabel2.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Location = New System.Drawing.Point(1, 263)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(138, 20)
        Me.LinkLabel2.TabIndex = 742
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Remind me later"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmRemindMe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 295)
        Me.ControlBox = False
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.DUDPeriodText)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DODPeriodValue)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.TxtReminderTime)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtReminderDate)
        Me.Controls.Add(Me.TxtNarration)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.TxtId)
        Me.Controls.Add(Me.LblID)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmRemindMe"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reminder"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents TxtId As AgControls.AgTextBox
    Friend WithEvents LblID As System.Windows.Forms.Label
    Friend WithEvents LblRemindeBy As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LblRemindOnDateTime As System.Windows.Forms.Label
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents TxtNarration As AgControls.AgTextBox
    Friend WithEvents TxtReminderDate As AgControls.AgTextBox
    Friend WithEvents TxtReminderTime As AgControls.AgTextBox
    Friend WithEvents DODPeriodValue As System.Windows.Forms.DomainUpDown
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DUDPeriodText As System.Windows.Forms.DomainUpDown
    Protected WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
End Class
