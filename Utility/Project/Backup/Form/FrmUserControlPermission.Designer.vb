<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserControlPermission
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
        Me.CmdRevoke = New System.Windows.Forms.Button
        Me.CmdSave = New System.Windows.Forms.Button
        Me.CboUserName = New AgControls.AgComboBox
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.TxtUserName = New AgControls.AgTextBox
        Me.CmdCopy = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CmdAdd = New System.Windows.Forms.Button
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmdRevoke
        '
        Me.CmdRevoke.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdRevoke.Location = New System.Drawing.Point(16, 60)
        Me.CmdRevoke.Name = "CmdRevoke"
        Me.CmdRevoke.Size = New System.Drawing.Size(65, 36)
        Me.CmdRevoke.TabIndex = 4
        Me.CmdRevoke.Text = "&Revoke All"
        Me.CmdRevoke.UseVisualStyleBackColor = True
        '
        'CmdSave
        '
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.Location = New System.Drawing.Point(308, 15)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.Size = New System.Drawing.Size(65, 36)
        Me.CmdSave.TabIndex = 116
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.UseVisualStyleBackColor = True
        '
        'CboUserName
        '
        Me.CboUserName.AgCmboMaster = False
        Me.CboUserName.AgMandatory = False
        Me.CboUserName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboUserName.FormattingEnabled = True
        Me.CboUserName.Location = New System.Drawing.Point(6, 20)
        Me.CboUserName.MaxLength = 10
        Me.CboUserName.Name = "CboUserName"
        Me.CboUserName.Size = New System.Drawing.Size(225, 21)
        Me.CboUserName.TabIndex = 115
        '
        'TreeView1
        '
        Me.TreeView1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView1.Location = New System.Drawing.Point(12, 135)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(425, 425)
        Me.TreeView1.TabIndex = 123
        '
        'TxtUserName
        '
        Me.TxtUserName.AgMandatory = False
        Me.TxtUserName.AgNumberLeftPlaces = 0
        Me.TxtUserName.AgNumberNegetiveAllow = False
        Me.TxtUserName.AgNumberRightPlaces = 0
        Me.TxtUserName.AgSelectedValue = Nothing
        Me.TxtUserName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUserName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUserName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUserName.Location = New System.Drawing.Point(128, 72)
        Me.TxtUserName.MaxLength = 10
        Me.TxtUserName.Name = "TxtUserName"
        Me.TxtUserName.Size = New System.Drawing.Size(300, 21)
        Me.TxtUserName.TabIndex = 122
        Me.TxtUserName.Text = "User Name"
        '
        'CmdCopy
        '
        Me.CmdCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdCopy.Location = New System.Drawing.Point(237, 15)
        Me.CmdCopy.Name = "CmdCopy"
        Me.CmdCopy.Size = New System.Drawing.Size(65, 36)
        Me.CmdCopy.TabIndex = 0
        Me.CmdCopy.Text = "&Copy"
        Me.CmdCopy.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CmdSave)
        Me.GroupBox2.Controls.Add(Me.CboUserName)
        Me.GroupBox2.Controls.Add(Me.CmdCopy)
        Me.GroupBox2.Location = New System.Drawing.Point(473, 53)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(387, 58)
        Me.GroupBox2.TabIndex = 121
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Copy From"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(440, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 13)
        Me.Label11.TabIndex = 119
        Me.Label11.Text = "Permission Detail: -"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 117
        Me.Label1.Text = "User Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(111, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 116
        Me.Label2.Text = "Ä"
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
        Me.Topctrl1.Size = New System.Drawing.Size(872, 41)
        Me.Topctrl1.TabIndex = 115
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmdRevoke)
        Me.GroupBox1.Controls.Add(Me.CmdAdd)
        Me.GroupBox1.Location = New System.Drawing.Point(766, 135)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(94, 109)
        Me.GroupBox1.TabIndex = 120
        Me.GroupBox1.TabStop = False
        '
        'CmdAdd
        '
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.Location = New System.Drawing.Point(15, 18)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.Size = New System.Drawing.Size(65, 36)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Tag = "2"
        Me.CmdAdd.Text = "&Allow All"
        Me.CmdAdd.UseVisualStyleBackColor = True
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(443, 135)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(290, 425)
        Me.Pnl1.TabIndex = 118
        '
        'FrmUserControlPermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 566)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.TxtUserName)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmUserControlPermission"
        Me.Text = "User Control Permission"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CmdRevoke As System.Windows.Forms.Button
    Friend WithEvents CmdSave As System.Windows.Forms.Button
    Friend WithEvents CboUserName As AgControls.AgComboBox
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents TxtUserName As AgControls.AgTextBox
    Friend WithEvents CmdCopy As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CmdAdd As System.Windows.Forms.Button
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
End Class
