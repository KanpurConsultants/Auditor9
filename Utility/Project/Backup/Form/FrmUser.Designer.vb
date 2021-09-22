<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUser
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
        Me.LblDiv_Name = New System.Windows.Forms.Label
        Me.LblDiv_NameReq = New System.Windows.Forms.Label
        Me.LblDataPath = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.TxtUser_Name = New AgControls.AgTextBox
        Me.TxtDescription = New AgControls.AgTextBox
        Me.TxtVarifyPassword = New AgControls.AgTextBox
        Me.TxtAdminYn = New AgControls.AgTextBox
        Me.TxtPassword = New AgControls.AgTextBox
        Me.TxtOldPassword = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtChangePassword = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(150, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "User Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(271, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Ä"
        '
        'LblDiv_Name
        '
        Me.LblDiv_Name.AutoSize = True
        Me.LblDiv_Name.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDiv_Name.Location = New System.Drawing.Point(150, 99)
        Me.LblDiv_Name.Name = "LblDiv_Name"
        Me.LblDiv_Name.Size = New System.Drawing.Size(71, 13)
        Me.LblDiv_Name.TabIndex = 5
        Me.LblDiv_Name.Text = "Description"
        '
        'LblDiv_NameReq
        '
        Me.LblDiv_NameReq.AutoSize = True
        Me.LblDiv_NameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDiv_NameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDiv_NameReq.Location = New System.Drawing.Point(271, 102)
        Me.LblDiv_NameReq.Name = "LblDiv_NameReq"
        Me.LblDiv_NameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDiv_NameReq.TabIndex = 4
        Me.LblDiv_NameReq.Text = "Ä"
        '
        'LblDataPath
        '
        Me.LblDataPath.AutoSize = True
        Me.LblDataPath.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDataPath.Location = New System.Drawing.Point(150, 120)
        Me.LblDataPath.Name = "LblDataPath"
        Me.LblDataPath.Size = New System.Drawing.Size(61, 13)
        Me.LblDataPath.TabIndex = 7
        Me.LblDataPath.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(453, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Admin (Y)es/(N)o"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(598, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Ä"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(453, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Verify Password"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(9, 162)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 13)
        Me.Label7.TabIndex = 107
        Me.Label7.Text = "Company/Site Detail:"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(12, 180)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(506, 258)
        Me.Pnl1.TabIndex = 7
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
        Me.Topctrl1.TabIndex = 8
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
        'TxtUser_Name
        '
        Me.TxtUser_Name.AgMandatory = True
        Me.TxtUser_Name.AgMasterHelp = True
        Me.TxtUser_Name.AgNumberLeftPlaces = 0
        Me.TxtUser_Name.AgNumberNegetiveAllow = False
        Me.TxtUser_Name.AgNumberRightPlaces = 0
        Me.TxtUser_Name.AgPickFromLastValue = False
        Me.TxtUser_Name.AgRowFilter = ""
        Me.TxtUser_Name.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUser_Name.AgSelectedValue = Nothing
        Me.TxtUser_Name.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUser_Name.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUser_Name.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUser_Name.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUser_Name.Location = New System.Drawing.Point(286, 75)
        Me.TxtUser_Name.MaxLength = 10
        Me.TxtUser_Name.Name = "TxtUser_Name"
        Me.TxtUser_Name.Size = New System.Drawing.Size(122, 18)
        Me.TxtUser_Name.TabIndex = 0
        '
        'TxtDescription
        '
        Me.TxtDescription.AgMandatory = True
        Me.TxtDescription.AgMasterHelp = True
        Me.TxtDescription.AgNumberLeftPlaces = 0
        Me.TxtDescription.AgNumberNegetiveAllow = False
        Me.TxtDescription.AgNumberRightPlaces = 0
        Me.TxtDescription.AgPickFromLastValue = False
        Me.TxtDescription.AgRowFilter = ""
        Me.TxtDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDescription.AgSelectedValue = Nothing
        Me.TxtDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(286, 95)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(391, 18)
        Me.TxtDescription.TabIndex = 2
        '
        'TxtVarifyPassword
        '
        Me.TxtVarifyPassword.AgMandatory = False
        Me.TxtVarifyPassword.AgMasterHelp = False
        Me.TxtVarifyPassword.AgNumberLeftPlaces = 0
        Me.TxtVarifyPassword.AgNumberNegetiveAllow = False
        Me.TxtVarifyPassword.AgNumberRightPlaces = 0
        Me.TxtVarifyPassword.AgPickFromLastValue = False
        Me.TxtVarifyPassword.AgRowFilter = ""
        Me.TxtVarifyPassword.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVarifyPassword.AgSelectedValue = Nothing
        Me.TxtVarifyPassword.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVarifyPassword.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVarifyPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVarifyPassword.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVarifyPassword.Location = New System.Drawing.Point(555, 115)
        Me.TxtVarifyPassword.MaxLength = 16
        Me.TxtVarifyPassword.Name = "TxtVarifyPassword"
        Me.TxtVarifyPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtVarifyPassword.Size = New System.Drawing.Size(122, 18)
        Me.TxtVarifyPassword.TabIndex = 6
        '
        'TxtAdminYn
        '
        Me.TxtAdminYn.AgMandatory = True
        Me.TxtAdminYn.AgMasterHelp = False
        Me.TxtAdminYn.AgNumberLeftPlaces = 0
        Me.TxtAdminYn.AgNumberNegetiveAllow = False
        Me.TxtAdminYn.AgNumberRightPlaces = 0
        Me.TxtAdminYn.AgPickFromLastValue = False
        Me.TxtAdminYn.AgRowFilter = ""
        Me.TxtAdminYn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdminYn.AgSelectedValue = Nothing
        Me.TxtAdminYn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdminYn.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtAdminYn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAdminYn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdminYn.Location = New System.Drawing.Point(615, 75)
        Me.TxtAdminYn.Name = "TxtAdminYn"
        Me.TxtAdminYn.Size = New System.Drawing.Size(62, 18)
        Me.TxtAdminYn.TabIndex = 1
        '
        'TxtPassword
        '
        Me.TxtPassword.AgMandatory = False
        Me.TxtPassword.AgMasterHelp = False
        Me.TxtPassword.AgNumberLeftPlaces = 0
        Me.TxtPassword.AgNumberNegetiveAllow = False
        Me.TxtPassword.AgNumberRightPlaces = 0
        Me.TxtPassword.AgPickFromLastValue = False
        Me.TxtPassword.AgRowFilter = ""
        Me.TxtPassword.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPassword.AgSelectedValue = Nothing
        Me.TxtPassword.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPassword.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPassword.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassword.Location = New System.Drawing.Point(286, 115)
        Me.TxtPassword.MaxLength = 16
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassword.Size = New System.Drawing.Size(122, 18)
        Me.TxtPassword.TabIndex = 5
        '
        'TxtOldPassword
        '
        Me.TxtOldPassword.AgMandatory = False
        Me.TxtOldPassword.AgMasterHelp = False
        Me.TxtOldPassword.AgNumberLeftPlaces = 0
        Me.TxtOldPassword.AgNumberNegetiveAllow = False
        Me.TxtOldPassword.AgNumberRightPlaces = 0
        Me.TxtOldPassword.AgPickFromLastValue = False
        Me.TxtOldPassword.AgRowFilter = ""
        Me.TxtOldPassword.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOldPassword.AgSelectedValue = Nothing
        Me.TxtOldPassword.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOldPassword.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOldPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOldPassword.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOldPassword.Location = New System.Drawing.Point(738, 91)
        Me.TxtOldPassword.MaxLength = 16
        Me.TxtOldPassword.Name = "TxtOldPassword"
        Me.TxtOldPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtOldPassword.Size = New System.Drawing.Size(122, 18)
        Me.TxtOldPassword.TabIndex = 4
        Me.TxtOldPassword.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(690, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 13)
        Me.Label6.TabIndex = 109
        Me.Label6.Text = "Old Password"
        Me.Label6.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(693, 75)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(131, 13)
        Me.Label8.TabIndex = 111
        Me.Label8.Text = "Change Password? ..."
        Me.Label8.Visible = False
        '
        'TxtChangePassword
        '
        Me.TxtChangePassword.AgMandatory = True
        Me.TxtChangePassword.AgMasterHelp = False
        Me.TxtChangePassword.AgNumberLeftPlaces = 0
        Me.TxtChangePassword.AgNumberNegetiveAllow = False
        Me.TxtChangePassword.AgNumberRightPlaces = 0
        Me.TxtChangePassword.AgPickFromLastValue = False
        Me.TxtChangePassword.AgRowFilter = ""
        Me.TxtChangePassword.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChangePassword.AgSelectedValue = Nothing
        Me.TxtChangePassword.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChangePassword.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtChangePassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChangePassword.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChangePassword.Location = New System.Drawing.Point(738, 71)
        Me.TxtChangePassword.Name = "TxtChangePassword"
        Me.TxtChangePassword.Size = New System.Drawing.Size(122, 18)
        Me.TxtChangePassword.TabIndex = 3
        Me.TxtChangePassword.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(521, 162)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 13)
        Me.Label9.TabIndex = 113
        Me.Label9.Text = "Module List:"
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(524, 180)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(336, 258)
        Me.Pnl2.TabIndex = 8
        '
        'FrmUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 473)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtChangePassword)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtOldPassword)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtUser_Name)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.TxtVarifyPassword)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtAdminYn)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.LblDiv_Name)
        Me.Controls.Add(Me.LblDiv_NameReq)
        Me.Controls.Add(Me.LblDataPath)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmUser"
        Me.Text = "User Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents LblDiv_Name As System.Windows.Forms.Label
    Friend WithEvents LblDiv_NameReq As System.Windows.Forms.Label
    Friend WithEvents LblDataPath As System.Windows.Forms.Label
    Friend WithEvents TxtPassword As AgControls.AgTextBox
    Friend WithEvents TxtAdminYn As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtVarifyPassword As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtDescription As AgControls.AgTextBox
    Friend WithEvents TxtUser_Name As AgControls.AgTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents TxtOldPassword As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtChangePassword As AgControls.AgTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Pnl2 As System.Windows.Forms.Panel
End Class
