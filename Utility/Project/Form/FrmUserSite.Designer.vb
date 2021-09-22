<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserSite
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.TxtUser_Name = New AgControls.AgTextBox
        Me.TxtDescription = New AgControls.AgTextBox
        Me.TxtAdminYn = New AgControls.AgTextBox
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
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(150, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 13)
        Me.Label7.TabIndex = 107
        Me.Label7.Text = "Company/Site Detail:"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(153, 164)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(524, 258)
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
        'FrmUserSite
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 473)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtUser_Name)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtAdminYn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.LblDiv_Name)
        Me.Controls.Add(Me.LblDiv_NameReq)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmUserSite"
        Me.Text = "User Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents LblDiv_Name As System.Windows.Forms.Label
    Friend WithEvents LblDiv_NameReq As System.Windows.Forms.Label
    Friend WithEvents TxtAdminYn As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtDescription As AgControls.AgTextBox
    Friend WithEvents TxtUser_Name As AgControls.AgTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
End Class
