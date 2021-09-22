<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserPermission
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
        Me.CmdApprove = New System.Windows.Forms.Button
        Me.CmdRevoke = New System.Windows.Forms.Button
        Me.CmdPrint = New System.Windows.Forms.Button
        Me.CmdDel = New System.Windows.Forms.Button
        Me.CmdEdit = New System.Windows.Forms.Button
        Me.CmdAdd = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.CmdSave = New System.Windows.Forms.Button
        Me.CboUserName = New AgControls.AgComboBox
        Me.CmdCopy = New System.Windows.Forms.Button
        Me.TxtUserName = New AgControls.AgTextBox
        Me.TreeView1 = New System.Windows.Forms.TreeView
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
        Me.Label11.Location = New System.Drawing.Point(302, 99)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 13)
        Me.Label11.TabIndex = 105
        Me.Label11.Text = "Permission Detail: -"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(225, 115)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(535, 439)
        Me.Pnl1.TabIndex = 104
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmdRevoke)
        Me.GroupBox1.Controls.Add(Me.CmdPrint)
        Me.GroupBox1.Controls.Add(Me.CmdDel)
        Me.GroupBox1.Controls.Add(Me.CmdEdit)
        Me.GroupBox1.Controls.Add(Me.CmdAdd)
        Me.GroupBox1.Location = New System.Drawing.Point(766, 111)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(94, 237)
        Me.GroupBox1.TabIndex = 106
        Me.GroupBox1.TabStop = False
        '
        'CmdApprove
        '
        Me.CmdApprove.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdApprove.Location = New System.Drawing.Point(782, 366)
        Me.CmdApprove.Name = "CmdApprove"
        Me.CmdApprove.Size = New System.Drawing.Size(65, 36)
        Me.CmdApprove.TabIndex = 5
        Me.CmdApprove.Tag = "5"
        Me.CmdApprove.Text = "Appr&ove All"
        Me.CmdApprove.UseVisualStyleBackColor = True
        Me.CmdApprove.Visible = False
        '
        'CmdRevoke
        '
        Me.CmdRevoke.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdRevoke.Location = New System.Drawing.Point(16, 186)
        Me.CmdRevoke.Name = "CmdRevoke"
        Me.CmdRevoke.Size = New System.Drawing.Size(65, 36)
        Me.CmdRevoke.TabIndex = 4
        Me.CmdRevoke.Text = "&Revoke All"
        Me.CmdRevoke.UseVisualStyleBackColor = True
        '
        'CmdPrint
        '
        Me.CmdPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPrint.Location = New System.Drawing.Point(15, 144)
        Me.CmdPrint.Name = "CmdPrint"
        Me.CmdPrint.Size = New System.Drawing.Size(65, 36)
        Me.CmdPrint.TabIndex = 3
        Me.CmdPrint.Tag = "5"
        Me.CmdPrint.Text = "&Print all"
        Me.CmdPrint.UseVisualStyleBackColor = True
        '
        'CmdDel
        '
        Me.CmdDel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDel.Location = New System.Drawing.Point(16, 102)
        Me.CmdDel.Name = "CmdDel"
        Me.CmdDel.Size = New System.Drawing.Size(65, 36)
        Me.CmdDel.TabIndex = 2
        Me.CmdDel.Tag = "4"
        Me.CmdDel.Text = "&Delete All"
        Me.CmdDel.UseVisualStyleBackColor = True
        '
        'CmdEdit
        '
        Me.CmdEdit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdEdit.Location = New System.Drawing.Point(16, 60)
        Me.CmdEdit.Name = "CmdEdit"
        Me.CmdEdit.Size = New System.Drawing.Size(65, 36)
        Me.CmdEdit.TabIndex = 1
        Me.CmdEdit.Tag = "3"
        Me.CmdEdit.Text = "&Edit All"
        Me.CmdEdit.UseVisualStyleBackColor = True
        '
        'CmdAdd
        '
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.Location = New System.Drawing.Point(15, 18)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.Size = New System.Drawing.Size(65, 36)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Tag = "2"
        Me.CmdAdd.Text = "&Add All"
        Me.CmdAdd.UseVisualStyleBackColor = True
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
        'TreeView1
        '
        Me.TreeView1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView1.Location = New System.Drawing.Point(12, 115)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(207, 439)
        Me.TreeView1.TabIndex = 114
        '
        'FrmUserPermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 566)
        Me.Controls.Add(Me.CmdApprove)
        Me.Controls.Add(Me.TreeView1)
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
        Me.Name = "FrmUserPermission"
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
    Friend WithEvents CmdRevoke As System.Windows.Forms.Button
    Friend WithEvents CmdPrint As System.Windows.Forms.Button
    Friend WithEvents CmdDel As System.Windows.Forms.Button
    Friend WithEvents CmdEdit As System.Windows.Forms.Button
    Friend WithEvents CmdAdd As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CmdCopy As System.Windows.Forms.Button
    Friend WithEvents TxtUserName As AgControls.AgTextBox
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents CboUserName As AgControls.AgComboBox
    Friend WithEvents CmdSave As System.Windows.Forms.Button
    Friend WithEvents CmdApprove As System.Windows.Forms.Button
End Class
