<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchPartyDetail
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
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.TxtVendorName = New AgControls.AgTextBox()
        Me.LblBuyerName = New System.Windows.Forms.Label()
        Me.TxtVendorAdd1 = New AgControls.AgTextBox()
        Me.LblAddress = New System.Windows.Forms.Label()
        Me.TxtVendorCity = New AgControls.AgTextBox()
        Me.LblCity = New System.Windows.Forms.Label()
        Me.TxtVendorMobile = New AgControls.AgTextBox()
        Me.LblMobile = New System.Windows.Forms.Label()
        Me.TxtState = New AgControls.AgTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(277, 161)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(60, 23)
        Me.BtnOk.TabIndex = 5
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(5, 141)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(416, 5)
        Me.GroupBox2.TabIndex = 737
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.Transparent
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(342, 161)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(60, 23)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "Close"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'TxtVendorName
        '
        Me.TxtVendorName.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendorName.AgLastValueTag = Nothing
        Me.TxtVendorName.AgLastValueText = Nothing
        Me.TxtVendorName.AgMandatory = True
        Me.TxtVendorName.AgMasterHelp = False
        Me.TxtVendorName.AgNumberLeftPlaces = 8
        Me.TxtVendorName.AgNumberNegetiveAllow = False
        Me.TxtVendorName.AgNumberRightPlaces = 2
        Me.TxtVendorName.AgPickFromLastValue = False
        Me.TxtVendorName.AgRowFilter = ""
        Me.TxtVendorName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorName.AgSelectedValue = Nothing
        Me.TxtVendorName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorName.Location = New System.Drawing.Point(102, 42)
        Me.TxtVendorName.MaxLength = 0
        Me.TxtVendorName.Name = "TxtVendorName"
        Me.TxtVendorName.Size = New System.Drawing.Size(300, 18)
        Me.TxtVendorName.TabIndex = 1
        '
        'LblBuyerName
        '
        Me.LblBuyerName.AutoSize = True
        Me.LblBuyerName.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyerName.Location = New System.Drawing.Point(19, 43)
        Me.LblBuyerName.Name = "LblBuyerName"
        Me.LblBuyerName.Size = New System.Drawing.Size(77, 16)
        Me.LblBuyerName.TabIndex = 742
        Me.LblBuyerName.Text = "Party Name"
        '
        'TxtVendorAdd1
        '
        Me.TxtVendorAdd1.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendorAdd1.AgLastValueTag = Nothing
        Me.TxtVendorAdd1.AgLastValueText = Nothing
        Me.TxtVendorAdd1.AgMandatory = False
        Me.TxtVendorAdd1.AgMasterHelp = False
        Me.TxtVendorAdd1.AgNumberLeftPlaces = 8
        Me.TxtVendorAdd1.AgNumberNegetiveAllow = False
        Me.TxtVendorAdd1.AgNumberRightPlaces = 2
        Me.TxtVendorAdd1.AgPickFromLastValue = False
        Me.TxtVendorAdd1.AgRowFilter = ""
        Me.TxtVendorAdd1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorAdd1.AgSelectedValue = Nothing
        Me.TxtVendorAdd1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorAdd1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorAdd1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorAdd1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorAdd1.Location = New System.Drawing.Point(102, 62)
        Me.TxtVendorAdd1.MaxLength = 0
        Me.TxtVendorAdd1.Name = "TxtVendorAdd1"
        Me.TxtVendorAdd1.Size = New System.Drawing.Size(300, 18)
        Me.TxtVendorAdd1.TabIndex = 2
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.BackColor = System.Drawing.Color.Transparent
        Me.LblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(19, 63)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(56, 16)
        Me.LblAddress.TabIndex = 744
        Me.LblAddress.Text = "Address"
        '
        'TxtVendorCity
        '
        Me.TxtVendorCity.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendorCity.AgLastValueTag = Nothing
        Me.TxtVendorCity.AgLastValueText = Nothing
        Me.TxtVendorCity.AgMandatory = False
        Me.TxtVendorCity.AgMasterHelp = False
        Me.TxtVendorCity.AgNumberLeftPlaces = 8
        Me.TxtVendorCity.AgNumberNegetiveAllow = False
        Me.TxtVendorCity.AgNumberRightPlaces = 2
        Me.TxtVendorCity.AgPickFromLastValue = False
        Me.TxtVendorCity.AgRowFilter = ""
        Me.TxtVendorCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorCity.AgSelectedValue = Nothing
        Me.TxtVendorCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorCity.Location = New System.Drawing.Point(102, 82)
        Me.TxtVendorCity.MaxLength = 0
        Me.TxtVendorCity.Name = "TxtVendorCity"
        Me.TxtVendorCity.Size = New System.Drawing.Size(300, 18)
        Me.TxtVendorCity.TabIndex = 4
        '
        'LblCity
        '
        Me.LblCity.AutoSize = True
        Me.LblCity.BackColor = System.Drawing.Color.Transparent
        Me.LblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity.Location = New System.Drawing.Point(19, 83)
        Me.LblCity.Name = "LblCity"
        Me.LblCity.Size = New System.Drawing.Size(31, 16)
        Me.LblCity.TabIndex = 746
        Me.LblCity.Text = "City"
        '
        'TxtVendorMobile
        '
        Me.TxtVendorMobile.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendorMobile.AgLastValueTag = Nothing
        Me.TxtVendorMobile.AgLastValueText = Nothing
        Me.TxtVendorMobile.AgMandatory = False
        Me.TxtVendorMobile.AgMasterHelp = False
        Me.TxtVendorMobile.AgNumberLeftPlaces = 8
        Me.TxtVendorMobile.AgNumberNegetiveAllow = False
        Me.TxtVendorMobile.AgNumberRightPlaces = 2
        Me.TxtVendorMobile.AgPickFromLastValue = False
        Me.TxtVendorMobile.AgRowFilter = ""
        Me.TxtVendorMobile.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorMobile.AgSelectedValue = Nothing
        Me.TxtVendorMobile.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorMobile.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorMobile.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorMobile.Location = New System.Drawing.Point(102, 22)
        Me.TxtVendorMobile.MaxLength = 0
        Me.TxtVendorMobile.Name = "TxtVendorMobile"
        Me.TxtVendorMobile.Size = New System.Drawing.Size(300, 18)
        Me.TxtVendorMobile.TabIndex = 0
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.BackColor = System.Drawing.Color.Transparent
        Me.LblMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(19, 23)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(46, 16)
        Me.LblMobile.TabIndex = 748
        Me.LblMobile.Text = "Mobile"
        '
        'TxtState
        '
        Me.TxtState.AgAllowUserToEnableMasterHelp = False
        Me.TxtState.AgLastValueTag = Nothing
        Me.TxtState.AgLastValueText = Nothing
        Me.TxtState.AgMandatory = False
        Me.TxtState.AgMasterHelp = False
        Me.TxtState.AgNumberLeftPlaces = 8
        Me.TxtState.AgNumberNegetiveAllow = False
        Me.TxtState.AgNumberRightPlaces = 2
        Me.TxtState.AgPickFromLastValue = False
        Me.TxtState.AgRowFilter = ""
        Me.TxtState.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtState.AgSelectedValue = Nothing
        Me.TxtState.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtState.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtState.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtState.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtState.Location = New System.Drawing.Point(102, 102)
        Me.TxtState.MaxLength = 0
        Me.TxtState.Name = "TxtState"
        Me.TxtState.Size = New System.Drawing.Size(300, 18)
        Me.TxtState.TabIndex = 749
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 16)
        Me.Label1.TabIndex = 750
        Me.Label1.Text = "State"
        '
        'FrmPurchPartyDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 193)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtState)
        Me.Controls.Add(Me.TxtVendorMobile)
        Me.Controls.Add(Me.LblMobile)
        Me.Controls.Add(Me.TxtVendorCity)
        Me.Controls.Add(Me.LblCity)
        Me.Controls.Add(Me.TxtVendorAdd1)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtVendorName)
        Me.Controls.Add(Me.LblBuyerName)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(300, 300)
        Me.MaximizeBox = False
        Me.Name = "FrmPurchPartyDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Party Detail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents BtnOk As System.Windows.Forms.Button
    Public WithEvents BtnCancel As System.Windows.Forms.Button
    Protected WithEvents LblBuyerName As System.Windows.Forms.Label
    Protected WithEvents LblAddress As System.Windows.Forms.Label
    Protected WithEvents LblCity As System.Windows.Forms.Label
    Protected WithEvents LblMobile As System.Windows.Forms.Label
    Public WithEvents TxtVendorName As AgControls.AgTextBox
    Public WithEvents TxtVendorAdd1 As AgControls.AgTextBox
    Public WithEvents TxtVendorCity As AgControls.AgTextBox
    Public WithEvents TxtVendorMobile As AgControls.AgTextBox
    Public WithEvents TxtState As AgControls.AgTextBox
    Protected WithEvents Label1 As Label
End Class
