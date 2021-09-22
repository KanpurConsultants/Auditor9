<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDivision
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
        Me.CboDiv_Name = New AgControls.AgComboBox
        Me.LblDiv_Name = New System.Windows.Forms.Label
        Me.LblDiv_NameReq = New System.Windows.Forms.Label
        Me.CboDataPath = New AgControls.AgComboBox
        Me.LblDataPath = New System.Windows.Forms.Label
        Me.LblDataPathReq = New System.Windows.Forms.Label
        Me.CboDiv_Code = New AgControls.AgComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
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
        Me.Topctrl1.Size = New System.Drawing.Size(871, 41)
        Me.Topctrl1.TabIndex = 0
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
        'CboDiv_Name
        '
        Me.CboDiv_Name.AgCmboMaster = True
        Me.CboDiv_Name.AgMandatory = False
        Me.CboDiv_Name.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDiv_Name.FormattingEnabled = True
        Me.CboDiv_Name.Location = New System.Drawing.Point(326, 129)
        Me.CboDiv_Name.MaxLength = 100
        Me.CboDiv_Name.Name = "CboDiv_Name"
        Me.CboDiv_Name.Size = New System.Drawing.Size(391, 21)
        Me.CboDiv_Name.TabIndex = 1
        Me.CboDiv_Name.Text = "CboDiv_Name"
        '
        'LblDiv_Name
        '
        Me.LblDiv_Name.AutoSize = True
        Me.LblDiv_Name.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDiv_Name.Location = New System.Drawing.Point(222, 132)
        Me.LblDiv_Name.Name = "LblDiv_Name"
        Me.LblDiv_Name.Size = New System.Drawing.Size(89, 13)
        Me.LblDiv_Name.TabIndex = 0
        Me.LblDiv_Name.Text = "Division Name"
        '
        'LblDiv_NameReq
        '
        Me.LblDiv_NameReq.AutoSize = True
        Me.LblDiv_NameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDiv_NameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDiv_NameReq.Location = New System.Drawing.Point(311, 137)
        Me.LblDiv_NameReq.Name = "LblDiv_NameReq"
        Me.LblDiv_NameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDiv_NameReq.TabIndex = 0
        Me.LblDiv_NameReq.Text = "Ä"
        '
        'CboDataPath
        '
        Me.CboDataPath.AgCmboMaster = True
        Me.CboDataPath.AgMandatory = False
        Me.CboDataPath.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDataPath.FormattingEnabled = True
        Me.CboDataPath.Location = New System.Drawing.Point(326, 153)
        Me.CboDataPath.MaxLength = 50
        Me.CboDataPath.Name = "CboDataPath"
        Me.CboDataPath.Size = New System.Drawing.Size(391, 21)
        Me.CboDataPath.TabIndex = 2
        Me.CboDataPath.Text = "CboDataPath"
        '
        'LblDataPath
        '
        Me.LblDataPath.AutoSize = True
        Me.LblDataPath.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDataPath.Location = New System.Drawing.Point(222, 156)
        Me.LblDataPath.Name = "LblDataPath"
        Me.LblDataPath.Size = New System.Drawing.Size(63, 13)
        Me.LblDataPath.TabIndex = 0
        Me.LblDataPath.Text = "Data Path"
        '
        'LblDataPathReq
        '
        Me.LblDataPathReq.AutoSize = True
        Me.LblDataPathReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDataPathReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDataPathReq.Location = New System.Drawing.Point(311, 161)
        Me.LblDataPathReq.Name = "LblDataPathReq"
        Me.LblDataPathReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDataPathReq.TabIndex = 0
        Me.LblDataPathReq.Text = "Ä"
        '
        'CboDiv_Code
        '
        Me.CboDiv_Code.AgCmboMaster = True
        Me.CboDiv_Code.AgMandatory = False
        Me.CboDiv_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDiv_Code.FormattingEnabled = True
        Me.CboDiv_Code.Location = New System.Drawing.Point(326, 105)
        Me.CboDiv_Code.MaxLength = 1
        Me.CboDiv_Code.Name = "CboDiv_Code"
        Me.CboDiv_Code.Size = New System.Drawing.Size(99, 21)
        Me.CboDiv_Code.TabIndex = 0
        Me.CboDiv_Code.Text = "AgComboBox1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(222, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Division Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(311, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Ä"
        '
        'FrmDivision
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(871, 266)
        Me.Controls.Add(Me.CboDiv_Code)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.CboDiv_Name)
        Me.Controls.Add(Me.LblDiv_Name)
        Me.Controls.Add(Me.LblDiv_NameReq)
        Me.Controls.Add(Me.CboDataPath)
        Me.Controls.Add(Me.LblDataPath)
        Me.Controls.Add(Me.LblDataPathReq)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmDivision"
        Me.Text = "Division Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents CboDiv_Name As AgControls.AgComboBox
    Friend WithEvents LblDiv_Name As System.Windows.Forms.Label
    Friend WithEvents LblDiv_NameReq As System.Windows.Forms.Label
    Friend WithEvents CboDataPath As AgControls.AgComboBox
    Friend WithEvents LblDataPath As System.Windows.Forms.Label
    Friend WithEvents LblDataPathReq As System.Windows.Forms.Label
    Friend WithEvents CboDiv_Code As AgControls.AgComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
