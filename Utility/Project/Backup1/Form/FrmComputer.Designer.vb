<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmComputer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmComputer))
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.LblTitle = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.BtnOK = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TxtDefaultGodown = New AgControls.AgTextBox
        Me.LblCountry = New System.Windows.Forms.Label
        Me.TxtIP = New AgControls.AgTextBox
        Me.LblState = New System.Windows.Forms.Label
        Me.LblSite_CodeReq = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.Location = New System.Drawing.Point(335, 154)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 695
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
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
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.Location = New System.Drawing.Point(254, 154)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 23)
        Me.BtnOK.TabIndex = 694
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.LblTitle)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(463, 41)
        Me.Panel1.TabIndex = 699
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Location = New System.Drawing.Point(-7, 140)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(479, 10)
        Me.GroupBox3.TabIndex = 700
        Me.GroupBox3.TabStop = False
        '
        'TxtDefaultGodown
        '
        Me.TxtDefaultGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtDefaultGodown.AgLastValueTag = Nothing
        Me.TxtDefaultGodown.AgLastValueText = Nothing
        Me.TxtDefaultGodown.AgMandatory = False
        Me.TxtDefaultGodown.AgMasterHelp = True
        Me.TxtDefaultGodown.AgNumberLeftPlaces = 0
        Me.TxtDefaultGodown.AgNumberNegetiveAllow = False
        Me.TxtDefaultGodown.AgNumberRightPlaces = 0
        Me.TxtDefaultGodown.AgPickFromLastValue = False
        Me.TxtDefaultGodown.AgRowFilter = ""
        Me.TxtDefaultGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDefaultGodown.AgSelectedValue = Nothing
        Me.TxtDefaultGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDefaultGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDefaultGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDefaultGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDefaultGodown.Location = New System.Drawing.Point(189, 94)
        Me.TxtDefaultGodown.MaxLength = 50
        Me.TxtDefaultGodown.Name = "TxtDefaultGodown"
        Me.TxtDefaultGodown.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtDefaultGodown.Size = New System.Drawing.Size(219, 18)
        Me.TxtDefaultGodown.TabIndex = 692
        '
        'LblCountry
        '
        Me.LblCountry.AutoSize = True
        Me.LblCountry.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCountry.Location = New System.Drawing.Point(61, 95)
        Me.LblCountry.Name = "LblCountry"
        Me.LblCountry.Size = New System.Drawing.Size(99, 16)
        Me.LblCountry.TabIndex = 697
        Me.LblCountry.Text = "Default Godown"
        '
        'TxtIP
        '
        Me.TxtIP.AgAllowUserToEnableMasterHelp = False
        Me.TxtIP.AgLastValueTag = Nothing
        Me.TxtIP.AgLastValueText = Nothing
        Me.TxtIP.AgMandatory = False
        Me.TxtIP.AgMasterHelp = True
        Me.TxtIP.AgNumberLeftPlaces = 0
        Me.TxtIP.AgNumberNegetiveAllow = False
        Me.TxtIP.AgNumberRightPlaces = 0
        Me.TxtIP.AgPickFromLastValue = False
        Me.TxtIP.AgRowFilter = ""
        Me.TxtIP.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIP.AgSelectedValue = Nothing
        Me.TxtIP.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIP.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtIP.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIP.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIP.Location = New System.Drawing.Point(189, 74)
        Me.TxtIP.MaxLength = 50
        Me.TxtIP.Name = "TxtIP"
        Me.TxtIP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtIP.Size = New System.Drawing.Size(219, 18)
        Me.TxtIP.TabIndex = 691
        '
        'LblState
        '
        Me.LblState.AutoSize = True
        Me.LblState.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblState.Location = New System.Drawing.Point(61, 75)
        Me.LblState.Name = "LblState"
        Me.LblState.Size = New System.Drawing.Size(71, 16)
        Me.LblState.TabIndex = 696
        Me.LblState.Text = "IP Address"
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.AutoSize = True
        Me.LblSite_CodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblSite_CodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(175, 98)
        Me.LblSite_CodeReq.Name = "LblSite_CodeReq"
        Me.LblSite_CodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblSite_CodeReq.TabIndex = 701
        Me.LblSite_CodeReq.Text = "Ä"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(175, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 702
        Me.Label1.Text = "Ä"
        '
        'FrmComputer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(463, 183)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblSite_CodeReq)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.TxtDefaultGodown)
        Me.Controls.Add(Me.LblCountry)
        Me.Controls.Add(Me.TxtIP)
        Me.Controls.Add(Me.LblState)
        Me.Name = "FrmComputer"
        Me.Text = "FrmComputer"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Public WithEvents LblTitle As System.Windows.Forms.Label
    Public WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtDefaultGodown As AgControls.AgTextBox
    Friend WithEvents LblCountry As System.Windows.Forms.Label
    Friend WithEvents TxtIP As AgControls.AgTextBox
    Friend WithEvents LblState As System.Windows.Forms.Label
    Public WithEvents LblSite_CodeReq As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
End Class
