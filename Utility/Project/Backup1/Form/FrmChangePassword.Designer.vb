<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChangePassword
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmChangePassword))
        Me.TxtNewPassword = New AgControls.AgTextBox
        Me.LblCountry = New System.Windows.Forms.Label
        Me.TxtOldPassword = New AgControls.AgTextBox
        Me.LblState = New System.Windows.Forms.Label
        Me.TxtConfirmPassword = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTitle = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.BtnOK = New System.Windows.Forms.Button
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtNewPassword
        '
        Me.TxtNewPassword.AgAllowUserToEnableMasterHelp = False
        Me.TxtNewPassword.AgMandatory = False
        Me.TxtNewPassword.AgMasterHelp = True
        Me.TxtNewPassword.AgNumberLeftPlaces = 0
        Me.TxtNewPassword.AgNumberNegetiveAllow = False
        Me.TxtNewPassword.AgNumberRightPlaces = 0
        Me.TxtNewPassword.AgPickFromLastValue = False
        Me.TxtNewPassword.AgRowFilter = ""
        Me.TxtNewPassword.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNewPassword.AgSelectedValue = Nothing
        Me.TxtNewPassword.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNewPassword.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNewPassword.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNewPassword.Location = New System.Drawing.Point(192, 111)
        Me.TxtNewPassword.MaxLength = 50
        Me.TxtNewPassword.Name = "TxtNewPassword"
        Me.TxtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtNewPassword.Size = New System.Drawing.Size(151, 18)
        Me.TxtNewPassword.TabIndex = 1
        '
        'LblCountry
        '
        Me.LblCountry.AutoSize = True
        Me.LblCountry.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCountry.Location = New System.Drawing.Point(64, 112)
        Me.LblCountry.Name = "LblCountry"
        Me.LblCountry.Size = New System.Drawing.Size(94, 16)
        Me.LblCountry.TabIndex = 686
        Me.LblCountry.Text = "New Password"
        '
        'TxtOldPassword
        '
        Me.TxtOldPassword.AgAllowUserToEnableMasterHelp = False
        Me.TxtOldPassword.AgMandatory = False
        Me.TxtOldPassword.AgMasterHelp = True
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
        Me.TxtOldPassword.Location = New System.Drawing.Point(192, 90)
        Me.TxtOldPassword.MaxLength = 50
        Me.TxtOldPassword.Name = "TxtOldPassword"
        Me.TxtOldPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtOldPassword.Size = New System.Drawing.Size(151, 18)
        Me.TxtOldPassword.TabIndex = 0
        '
        'LblState
        '
        Me.LblState.AutoSize = True
        Me.LblState.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblState.Location = New System.Drawing.Point(64, 91)
        Me.LblState.Name = "LblState"
        Me.LblState.Size = New System.Drawing.Size(89, 16)
        Me.LblState.TabIndex = 685
        Me.LblState.Text = "Old Password"
        '
        'TxtConfirmPassword
        '
        Me.TxtConfirmPassword.AgAllowUserToEnableMasterHelp = False
        Me.TxtConfirmPassword.AgMandatory = False
        Me.TxtConfirmPassword.AgMasterHelp = True
        Me.TxtConfirmPassword.AgNumberLeftPlaces = 0
        Me.TxtConfirmPassword.AgNumberNegetiveAllow = False
        Me.TxtConfirmPassword.AgNumberRightPlaces = 0
        Me.TxtConfirmPassword.AgPickFromLastValue = False
        Me.TxtConfirmPassword.AgRowFilter = ""
        Me.TxtConfirmPassword.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtConfirmPassword.AgSelectedValue = Nothing
        Me.TxtConfirmPassword.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtConfirmPassword.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtConfirmPassword.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtConfirmPassword.Location = New System.Drawing.Point(192, 132)
        Me.TxtConfirmPassword.MaxLength = 50
        Me.TxtConfirmPassword.Name = "TxtConfirmPassword"
        Me.TxtConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtConfirmPassword.Size = New System.Drawing.Size(151, 18)
        Me.TxtConfirmPassword.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(64, 133)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 16)
        Me.Label1.TabIndex = 688
        Me.Label1.Text = "Confirm Password"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.LblTitle)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(412, 41)
        Me.Panel1.TabIndex = 689
        '
        'LblTitle
        '
        Me.LblTitle.AutoSize = True
        Me.LblTitle.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitle.Location = New System.Drawing.Point(51, 12)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(50, 16)
        Me.LblTitle.TabIndex = 11
        Me.LblTitle.Text = "Label1"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(33, 30)
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Location = New System.Drawing.Point(-14, 204)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(442, 4)
        Me.GroupBox3.TabIndex = 690
        Me.GroupBox3.TabStop = False
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(247, 220)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 23)
        Me.BtnOK.TabIndex = 3
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(328, 220)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 4
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'FrmChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(412, 251)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.TxtConfirmPassword)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtNewPassword)
        Me.Controls.Add(Me.LblCountry)
        Me.Controls.Add(Me.TxtOldPassword)
        Me.Controls.Add(Me.LblState)
        Me.KeyPreview = True
        Me.Name = "FrmChangePassword"
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtNewPassword As AgControls.AgTextBox
    Friend WithEvents LblCountry As System.Windows.Forms.Label
    Friend WithEvents TxtOldPassword As AgControls.AgTextBox
    Friend WithEvents LblState As System.Windows.Forms.Label
    Friend WithEvents TxtConfirmPassword As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblTitle As System.Windows.Forms.Label
    Public WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Public WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
End Class
