<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAcGroupMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAcGroupMaster))
        Me.LblName = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtName = New System.Windows.Forms.ComboBox
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.txtGroupUnder = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtNature = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblSysGroup = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtContraGroupName = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtMainGroup = New AgControls.AgTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.BtnPositioning = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LblName
        '
        Me.LblName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(169, 130)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(105, 16)
        Me.LblName.TabIndex = 14
        Me.LblName.Text = "A/c Group Name"
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(375, 130)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(10, 7)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Ä"
        '
        'TxtName
        '
        Me.TxtName.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TxtName.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtName.FormattingEnabled = True
        Me.TxtName.Location = New System.Drawing.Point(391, 130)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(312, 25)
        Me.TxtName.TabIndex = 0
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
        Me.Topctrl1.Size = New System.Drawing.Size(883, 41)
        Me.Topctrl1.TabIndex = 5
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
        'txtGroupUnder
        '
        Me.txtGroupUnder.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtGroupUnder.BackColor = System.Drawing.Color.White
        Me.txtGroupUnder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGroupUnder.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGroupUnder.Location = New System.Drawing.Point(391, 177)
        Me.txtGroupUnder.MaxLength = 0
        Me.txtGroupUnder.Name = "txtGroupUnder"
        Me.txtGroupUnder.Size = New System.Drawing.Size(312, 18)
        Me.txtGroupUnder.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(169, 177)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 16)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Group Under"
        '
        'txtNature
        '
        Me.txtNature.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtNature.BackColor = System.Drawing.Color.White
        Me.txtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNature.Location = New System.Drawing.Point(391, 217)
        Me.txtNature.MaxLength = 0
        Me.txtNature.Name = "txtNature"
        Me.txtNature.Size = New System.Drawing.Size(172, 18)
        Me.txtNature.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(169, 217)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 16)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Nature"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(375, 217)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Ä"
        '
        'LblSysGroup
        '
        Me.LblSysGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblSysGroup.AutoSize = True
        Me.LblSysGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSysGroup.ForeColor = System.Drawing.Color.Blue
        Me.LblSysGroup.Location = New System.Drawing.Point(342, 79)
        Me.LblSysGroup.Name = "LblSysGroup"
        Me.LblSysGroup.Size = New System.Drawing.Size(99, 16)
        Me.LblSysGroup.TabIndex = 32
        Me.LblSysGroup.Text = "System Define"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(375, 157)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Ä"
        '
        'TxtContraGroupName
        '
        Me.TxtContraGroupName.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtContraGroupName.BackColor = System.Drawing.Color.White
        Me.TxtContraGroupName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtContraGroupName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtContraGroupName.Location = New System.Drawing.Point(391, 157)
        Me.TxtContraGroupName.MaxLength = 50
        Me.TxtContraGroupName.Name = "TxtContraGroupName"
        Me.TxtContraGroupName.Size = New System.Drawing.Size(312, 18)
        Me.TxtContraGroupName.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(169, 157)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 16)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Contra Group Name"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(375, 197)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 7)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Ä"
        '
        'TxtMainGroup
        '
        Me.TxtMainGroup.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtMainGroup.BackColor = System.Drawing.Color.White
        Me.TxtMainGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMainGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMainGroup.Location = New System.Drawing.Point(391, 197)
        Me.TxtMainGroup.MaxLength = 0
        Me.TxtMainGroup.Name = "TxtMainGroup"
        Me.TxtMainGroup.Size = New System.Drawing.Size(172, 18)
        Me.TxtMainGroup.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(169, 197)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 16)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "Main Group"
        '
        'BtnPositioning
        '
        Me.BtnPositioning.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPositioning.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPositioning.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnPositioning.Image = CType(resources.GetObject("BtnPositioning.Image"), System.Drawing.Image)
        Me.BtnPositioning.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnPositioning.Location = New System.Drawing.Point(759, 261)
        Me.BtnPositioning.Name = "BtnPositioning"
        Me.BtnPositioning.Size = New System.Drawing.Size(112, 38)
        Me.BtnPositioning.TabIndex = 39
        Me.BtnPositioning.Text = "&Positioning"
        Me.BtnPositioning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnPositioning.UseVisualStyleBackColor = True
        '
        'FrmAcGroupMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(883, 311)
        Me.Controls.Add(Me.BtnPositioning)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtMainGroup)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtContraGroupName)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.LblSysGroup)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtNature)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtGroupUnder)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.Topctrl1)
        Me.KeyPreview = True
        Me.Name = "FrmAcGroupMaster"
        Me.ShowIcon = False
        Me.Text = "A/c Group Master"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtName As System.Windows.Forms.ComboBox
    Friend WithEvents txtGroupUnder As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNature As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblSysGroup As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtContraGroupName As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtMainGroup As AgControls.AgTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents BtnPositioning As System.Windows.Forms.Button
End Class
