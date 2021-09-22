<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserTarget
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
        Me.CboUserName = New AgControls.AgComboBox
        Me.LblUserName = New System.Windows.Forms.Label
        Me.LblUserNameReq = New System.Windows.Forms.Label
        Me.TxtDate_From = New AgControls.AgTextBox
        Me.LblDate_From = New System.Windows.Forms.Label
        Me.LblDate_FromReq = New System.Windows.Forms.Label
        Me.TxtDate_To = New AgControls.AgTextBox
        Me.LblDate_To = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.Topctrl1.Size = New System.Drawing.Size(872, 41)
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
        'CboUserName
        '
        Me.CboUserName.AgCmboMaster = False
        Me.CboUserName.AgMandatory = False
        Me.CboUserName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboUserName.FormattingEnabled = True
        Me.CboUserName.Location = New System.Drawing.Point(322, 70)
        Me.CboUserName.MaxLength = 10
        Me.CboUserName.Name = "CboUserName"
        Me.CboUserName.Size = New System.Drawing.Size(329, 21)
        Me.CboUserName.TabIndex = 12
        Me.CboUserName.Text = "CboUserName"
        '
        'LblUserName
        '
        Me.LblUserName.AutoSize = True
        Me.LblUserName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUserName.Location = New System.Drawing.Point(222, 74)
        Me.LblUserName.Name = "LblUserName"
        Me.LblUserName.Size = New System.Drawing.Size(70, 13)
        Me.LblUserName.TabIndex = 0
        Me.LblUserName.Text = "User Name"
        '
        'LblUserNameReq
        '
        Me.LblUserNameReq.AutoSize = True
        Me.LblUserNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblUserNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblUserNameReq.Location = New System.Drawing.Point(307, 77)
        Me.LblUserNameReq.Name = "LblUserNameReq"
        Me.LblUserNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblUserNameReq.TabIndex = 0
        Me.LblUserNameReq.Text = "Ä"
        '
        'TxtDate_From
        '
        Me.TxtDate_From.AgMandatory = False
        Me.TxtDate_From.AgMasterHelp = False
        Me.TxtDate_From.AgNumberLeftPlaces = 0
        Me.TxtDate_From.AgNumberNegetiveAllow = False
        Me.TxtDate_From.AgNumberRightPlaces = 0
        Me.TxtDate_From.AgPickFromLastValue = False
        Me.TxtDate_From.AgSelectedValue = Nothing
        Me.TxtDate_From.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDate_From.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDate_From.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate_From.Location = New System.Drawing.Point(322, 93)
        Me.TxtDate_From.Name = "TxtDate_From"
        Me.TxtDate_From.Size = New System.Drawing.Size(100, 21)
        Me.TxtDate_From.TabIndex = 13
        Me.TxtDate_From.Text = "TxtDate_From"
        '
        'LblDate_From
        '
        Me.LblDate_From.AutoSize = True
        Me.LblDate_From.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate_From.Location = New System.Drawing.Point(222, 97)
        Me.LblDate_From.Name = "LblDate_From"
        Me.LblDate_From.Size = New System.Drawing.Size(67, 13)
        Me.LblDate_From.TabIndex = 0
        Me.LblDate_From.Text = "Date From"
        '
        'LblDate_FromReq
        '
        Me.LblDate_FromReq.AutoSize = True
        Me.LblDate_FromReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDate_FromReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDate_FromReq.Location = New System.Drawing.Point(307, 100)
        Me.LblDate_FromReq.Name = "LblDate_FromReq"
        Me.LblDate_FromReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDate_FromReq.TabIndex = 0
        Me.LblDate_FromReq.Text = "Ä"
        '
        'TxtDate_To
        '
        Me.TxtDate_To.AgMandatory = False
        Me.TxtDate_To.AgMasterHelp = False
        Me.TxtDate_To.AgNumberLeftPlaces = 0
        Me.TxtDate_To.AgNumberNegetiveAllow = False
        Me.TxtDate_To.AgNumberRightPlaces = 0
        Me.TxtDate_To.AgPickFromLastValue = False
        Me.TxtDate_To.AgSelectedValue = Nothing
        Me.TxtDate_To.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDate_To.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDate_To.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate_To.Location = New System.Drawing.Point(551, 93)
        Me.TxtDate_To.Name = "TxtDate_To"
        Me.TxtDate_To.Size = New System.Drawing.Size(100, 21)
        Me.TxtDate_To.TabIndex = 14
        Me.TxtDate_To.Text = "TxtDate_To"
        '
        'LblDate_To
        '
        Me.LblDate_To.AutoSize = True
        Me.LblDate_To.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate_To.Location = New System.Drawing.Point(480, 97)
        Me.LblDate_To.Name = "LblDate_To"
        Me.LblDate_To.Size = New System.Drawing.Size(52, 13)
        Me.LblDate_To.TabIndex = 0
        Me.LblDate_To.Text = "Date To"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(145, 160)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(583, 270)
        Me.Pnl1.TabIndex = 102
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(142, 138)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 103
        Me.Label1.Text = "Target Detail: -"
        '
        'FrmUserTarget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 466)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.CboUserName)
        Me.Controls.Add(Me.LblUserName)
        Me.Controls.Add(Me.LblUserNameReq)
        Me.Controls.Add(Me.TxtDate_From)
        Me.Controls.Add(Me.LblDate_From)
        Me.Controls.Add(Me.LblDate_FromReq)
        Me.Controls.Add(Me.TxtDate_To)
        Me.Controls.Add(Me.LblDate_To)
        Me.Controls.Add(Me.Pnl1)
        Me.KeyPreview = True
        Me.Name = "FrmUserTarget"
        Me.Text = "Entry Point Wise User Target"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
Friend WithEvents Topctrl1 As Topctrl.Topctrl
Friend WithEvents CboUserName As AgControls.AgComboBox
Friend WithEvents LblUserName As System.Windows.Forms.Label
Friend WithEvents LblUserNameReq As System.Windows.Forms.Label
Friend WithEvents TxtDate_From As AgControls.AgTextBox
Friend WithEvents LblDate_From As System.Windows.Forms.Label
Friend WithEvents LblDate_FromReq As System.Windows.Forms.Label
Friend WithEvents TxtDate_To As AgControls.AgTextBox
Friend WithEvents LblDate_To As System.Windows.Forms.Label
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
