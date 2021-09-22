<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserVoucherTypePermission
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.Label11 = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CmdRevokeAll = New System.Windows.Forms.Button
        Me.CmdPermissionAll = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.CmdSave = New System.Windows.Forms.Button
        Me.CboUserName = New AgControls.AgComboBox
        Me.CmdCopy = New System.Windows.Forms.Button
        Me.TxtUserName = New AgControls.AgTextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "User Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(111, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Ä"
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
        Me.Topctrl1.Size = New System.Drawing.Size(872, 41)
        Me.Topctrl1.TabIndex = 14
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
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(17, 99)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 13)
        Me.Label11.TabIndex = 105
        Me.Label11.Text = "Permission Detail: -"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(15, 115)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(745, 439)
        Me.Pnl1.TabIndex = 104
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmdRevokeAll)
        Me.GroupBox1.Controls.Add(Me.CmdPermissionAll)
        Me.GroupBox1.Location = New System.Drawing.Point(766, 111)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(94, 122)
        Me.GroupBox1.TabIndex = 106
        Me.GroupBox1.TabStop = False
        '
        'CmdRevokeAll
        '
        Me.CmdRevokeAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdRevokeAll.Location = New System.Drawing.Point(12, 69)
        Me.CmdRevokeAll.Name = "CmdRevokeAll"
        Me.CmdRevokeAll.Size = New System.Drawing.Size(72, 36)
        Me.CmdRevokeAll.TabIndex = 4
        Me.CmdRevokeAll.Text = "&Revoke All"
        Me.CmdRevokeAll.UseVisualStyleBackColor = True
        '
        'CmdPermissionAll
        '
        Me.CmdPermissionAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPermissionAll.Location = New System.Drawing.Point(11, 18)
        Me.CmdPermissionAll.Name = "CmdPermissionAll"
        Me.CmdPermissionAll.Size = New System.Drawing.Size(73, 36)
        Me.CmdPermissionAll.TabIndex = 0
        Me.CmdPermissionAll.Tag = "2"
        Me.CmdPermissionAll.Text = "&Permission All"
        Me.CmdPermissionAll.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CmdSave)
        Me.GroupBox2.Controls.Add(Me.CboUserName)
        Me.GroupBox2.Controls.Add(Me.CmdCopy)
        Me.GroupBox2.Location = New System.Drawing.Point(473, 47)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(387, 58)
        Me.GroupBox2.TabIndex = 107
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Copy From"
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
        'TxtUserName
        '
        Me.TxtUserName.AgAllowUserToEnableMasterHelp = False
        Me.TxtUserName.AgLastValueTag = Nothing
        Me.TxtUserName.AgLastValueText = Nothing
        Me.TxtUserName.AgMandatory = False
        Me.TxtUserName.AgMasterHelp = False
        Me.TxtUserName.AgNumberLeftPlaces = 0
        Me.TxtUserName.AgNumberNegetiveAllow = False
        Me.TxtUserName.AgNumberRightPlaces = 0
        Me.TxtUserName.AgPickFromLastValue = False
        Me.TxtUserName.AgRowFilter = ""
        Me.TxtUserName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUserName.AgSelectedValue = Nothing
        Me.TxtUserName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUserName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUserName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUserName.Location = New System.Drawing.Point(128, 66)
        Me.TxtUserName.MaxLength = 10
        Me.TxtUserName.Name = "TxtUserName"
        Me.TxtUserName.Size = New System.Drawing.Size(300, 21)
        Me.TxtUserName.TabIndex = 109
        Me.TxtUserName.Text = "User Name"
        '
        'FrmUserVoucherTypePermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 566)
        Me.Controls.Add(Me.TxtUserName)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Topctrl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmUserVoucherTypePermission"
        Me.Text = "User Permission"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CmdRevokeAll As System.Windows.Forms.Button
    Friend WithEvents CmdPermissionAll As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CmdCopy As System.Windows.Forms.Button
    Friend WithEvents TxtUserName As AgControls.AgTextBox
    Friend WithEvents CboUserName As AgControls.AgComboBox
    Friend WithEvents CmdSave As System.Windows.Forms.Button
End Class
