<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCompanyInput
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
        Me.LblCityReq = New System.Windows.Forms.Label()
        Me.TxtCity = New AgControls.AgTextBox()
        Me.LblCity = New System.Windows.Forms.Label()
        Me.LblNameReq = New System.Windows.Forms.Label()
        Me.TxtDispName = New AgControls.AgTextBox()
        Me.LblName = New System.Windows.Forms.Label()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LblCityReq
        '
        Me.LblCityReq.AutoSize = True
        Me.LblCityReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblCityReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCityReq.Location = New System.Drawing.Point(138, 107)
        Me.LblCityReq.Name = "LblCityReq"
        Me.LblCityReq.Size = New System.Drawing.Size(10, 7)
        Me.LblCityReq.TabIndex = 797
        Me.LblCityReq.Text = "Ä"
        '
        'TxtCity
        '
        Me.TxtCity.AgAllowUserToEnableMasterHelp = False
        Me.TxtCity.AgLastValueTag = Nothing
        Me.TxtCity.AgLastValueText = Nothing
        Me.TxtCity.AgMandatory = True
        Me.TxtCity.AgMasterHelp = False
        Me.TxtCity.AgNumberLeftPlaces = 0
        Me.TxtCity.AgNumberNegetiveAllow = False
        Me.TxtCity.AgNumberRightPlaces = 0
        Me.TxtCity.AgPickFromLastValue = False
        Me.TxtCity.AgRowFilter = ""
        Me.TxtCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCity.AgSelectedValue = Nothing
        Me.TxtCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCity.Location = New System.Drawing.Point(155, 100)
        Me.TxtCity.MaxLength = 0
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(292, 18)
        Me.TxtCity.TabIndex = 793
        '
        'LblCity
        '
        Me.LblCity.AutoSize = True
        Me.LblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity.Location = New System.Drawing.Point(30, 100)
        Me.LblCity.Name = "LblCity"
        Me.LblCity.Size = New System.Drawing.Size(31, 16)
        Me.LblCity.TabIndex = 796
        Me.LblCity.Text = "City"
        '
        'LblNameReq
        '
        Me.LblNameReq.AutoSize = True
        Me.LblNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNameReq.Location = New System.Drawing.Point(138, 60)
        Me.LblNameReq.Name = "LblNameReq"
        Me.LblNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblNameReq.TabIndex = 795
        Me.LblNameReq.Text = "Ä"
        '
        'TxtDispName
        '
        Me.TxtDispName.AgAllowUserToEnableMasterHelp = False
        Me.TxtDispName.AgLastValueTag = Nothing
        Me.TxtDispName.AgLastValueText = Nothing
        Me.TxtDispName.AgMandatory = True
        Me.TxtDispName.AgMasterHelp = True
        Me.TxtDispName.AgNumberLeftPlaces = 0
        Me.TxtDispName.AgNumberNegetiveAllow = False
        Me.TxtDispName.AgNumberRightPlaces = 0
        Me.TxtDispName.AgPickFromLastValue = False
        Me.TxtDispName.AgRowFilter = ""
        Me.TxtDispName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDispName.AgSelectedValue = Nothing
        Me.TxtDispName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDispName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDispName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDispName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDispName.Location = New System.Drawing.Point(155, 53)
        Me.TxtDispName.MaxLength = 100
        Me.TxtDispName.Name = "TxtDispName"
        Me.TxtDispName.Size = New System.Drawing.Size(292, 18)
        Me.TxtDispName.TabIndex = 792
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(30, 53)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(101, 16)
        Me.LblName.TabIndex = 794
        Me.LblName.Text = "Company Name"
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.Transparent
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(155, 161)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(83, 23)
        Me.BtnSave.TabIndex = 798
        Me.BtnSave.Text = "&Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.Transparent
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(260, 161)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(83, 23)
        Me.BtnCancel.TabIndex = 799
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(152, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(274, 16)
        Me.Label1.TabIndex = 800
        Me.Label1.Text = "Publicly Known Name e.g. Reliance/Tata Only"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(152, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(184, 16)
        Me.Label2.TabIndex = 801
        Me.Label2.Text = "Jurisdiction City / City of origin"
        '
        'FrmCompany
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 196)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.LblCityReq)
        Me.Controls.Add(Me.TxtCity)
        Me.Controls.Add(Me.LblCity)
        Me.Controls.Add(Me.LblNameReq)
        Me.Controls.Add(Me.TxtDispName)
        Me.Controls.Add(Me.LblName)
        Me.Name = "FrmCompany"
        Me.Text = "FrmCompany"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected WithEvents LblCityReq As Label
    Protected WithEvents TxtCity As AgControls.AgTextBox
    Protected WithEvents LblCity As Label
    Protected WithEvents LblNameReq As Label
    Public WithEvents TxtDispName As AgControls.AgTextBox
    Protected WithEvents LblName As Label
    Friend WithEvents BtnSave As Button
    Friend WithEvents BtnCancel As Button
    Protected WithEvents Label1 As Label
    Protected WithEvents Label2 As Label
End Class
