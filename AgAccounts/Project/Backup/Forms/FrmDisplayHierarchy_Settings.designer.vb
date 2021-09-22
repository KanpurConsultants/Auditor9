<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDisplayHierarchy_Settings
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
        Me.Label23 = New System.Windows.Forms.Label
        Me.TxtToDate = New AgControls.AgTextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.TxtFromDate = New AgControls.AgTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.BtnOK = New System.Windows.Forms.Button
        Me.TxtSite = New AgControls.AgTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtZeroBalance = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtShowContra = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtClosingStock = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtAcGroup = New AgControls.AgTextBox
        Me.LblAcGroup = New System.Windows.Forms.Label
        Me.TxtCostCenter = New AgControls.AgTextBox
        Me.LblCostCenter = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label23
        '
        Me.Label23.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(46, 62)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(52, 16)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "To Date"
        '
        'TxtToDate
        '
        Me.TxtToDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtToDate.AgMandatory = False
        Me.TxtToDate.AgMasterHelp = False
        Me.TxtToDate.AgNumberLeftPlaces = 0
        Me.TxtToDate.AgNumberNegetiveAllow = False
        Me.TxtToDate.AgNumberRightPlaces = 0
        Me.TxtToDate.AgPickFromLastValue = False
        Me.TxtToDate.AgRowFilter = ""
        Me.TxtToDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToDate.AgSelectedValue = Nothing
        Me.TxtToDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToDate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtToDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToDate.Location = New System.Drawing.Point(192, 62)
        Me.TxtToDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtToDate.MaxLength = 25
        Me.TxtToDate.Name = "TxtToDate"
        Me.TxtToDate.Size = New System.Drawing.Size(206, 18)
        Me.TxtToDate.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(46, 42)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(69, 16)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "From Date"
        '
        'TxtFromDate
        '
        Me.TxtFromDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromDate.AgMandatory = False
        Me.TxtFromDate.AgMasterHelp = False
        Me.TxtFromDate.AgNumberLeftPlaces = 0
        Me.TxtFromDate.AgNumberNegetiveAllow = False
        Me.TxtFromDate.AgNumberRightPlaces = 0
        Me.TxtFromDate.AgPickFromLastValue = False
        Me.TxtFromDate.AgRowFilter = ""
        Me.TxtFromDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromDate.AgSelectedValue = Nothing
        Me.TxtFromDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromDate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFromDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromDate.Location = New System.Drawing.Point(192, 42)
        Me.TxtFromDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtFromDate.MaxLength = 25
        Me.TxtFromDate.Name = "TxtFromDate"
        Me.TxtFromDate.Size = New System.Drawing.Size(206, 18)
        Me.TxtFromDate.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(8, 247)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(440, 9)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.BackColor = System.Drawing.Color.Transparent
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnCancel.Location = New System.Drawing.Point(364, 262)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(84, 24)
        Me.BtnCancel.TabIndex = 7
        Me.BtnCancel.Text = "Canc&el"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.BackColor = System.Drawing.Color.Transparent
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnOK.Location = New System.Drawing.Point(275, 262)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(84, 24)
        Me.BtnOK.TabIndex = 6
        Me.BtnOK.Text = "O&k"
        Me.BtnOK.UseVisualStyleBackColor = False
        '
        'TxtSite
        '
        Me.TxtSite.AgAllowUserToEnableMasterHelp = False
        Me.TxtSite.AgMandatory = False
        Me.TxtSite.AgMasterHelp = False
        Me.TxtSite.AgNumberLeftPlaces = 0
        Me.TxtSite.AgNumberNegetiveAllow = False
        Me.TxtSite.AgNumberRightPlaces = 0
        Me.TxtSite.AgPickFromLastValue = False
        Me.TxtSite.AgRowFilter = ""
        Me.TxtSite.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSite.AgSelectedValue = Nothing
        Me.TxtSite.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSite.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSite.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtSite.BackColor = System.Drawing.Color.White
        Me.TxtSite.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSite.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite.Location = New System.Drawing.Point(192, 82)
        Me.TxtSite.MaxLength = 0
        Me.TxtSite.Name = "TxtSite"
        Me.TxtSite.Size = New System.Drawing.Size(206, 18)
        Me.TxtSite.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(46, 83)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 16)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Site Name"
        '
        'TxtZeroBalance
        '
        Me.TxtZeroBalance.AgAllowUserToEnableMasterHelp = False
        Me.TxtZeroBalance.AgMandatory = False
        Me.TxtZeroBalance.AgMasterHelp = False
        Me.TxtZeroBalance.AgNumberLeftPlaces = 0
        Me.TxtZeroBalance.AgNumberNegetiveAllow = False
        Me.TxtZeroBalance.AgNumberRightPlaces = 0
        Me.TxtZeroBalance.AgPickFromLastValue = False
        Me.TxtZeroBalance.AgRowFilter = ""
        Me.TxtZeroBalance.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtZeroBalance.AgSelectedValue = Nothing
        Me.TxtZeroBalance.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtZeroBalance.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtZeroBalance.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtZeroBalance.BackColor = System.Drawing.Color.White
        Me.TxtZeroBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtZeroBalance.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtZeroBalance.Location = New System.Drawing.Point(192, 142)
        Me.TxtZeroBalance.MaxLength = 0
        Me.TxtZeroBalance.Name = "TxtZeroBalance"
        Me.TxtZeroBalance.Size = New System.Drawing.Size(206, 18)
        Me.TxtZeroBalance.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(46, 143)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 16)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Show Zero Value A/c  "
        '
        'TxtShowContra
        '
        Me.TxtShowContra.AgAllowUserToEnableMasterHelp = False
        Me.TxtShowContra.AgMandatory = False
        Me.TxtShowContra.AgMasterHelp = False
        Me.TxtShowContra.AgNumberLeftPlaces = 0
        Me.TxtShowContra.AgNumberNegetiveAllow = False
        Me.TxtShowContra.AgNumberRightPlaces = 0
        Me.TxtShowContra.AgPickFromLastValue = False
        Me.TxtShowContra.AgRowFilter = ""
        Me.TxtShowContra.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShowContra.AgSelectedValue = Nothing
        Me.TxtShowContra.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShowContra.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShowContra.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtShowContra.BackColor = System.Drawing.Color.White
        Me.TxtShowContra.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShowContra.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShowContra.Location = New System.Drawing.Point(192, 162)
        Me.TxtShowContra.MaxLength = 0
        Me.TxtShowContra.Name = "TxtShowContra"
        Me.TxtShowContra.Size = New System.Drawing.Size(206, 18)
        Me.TxtShowContra.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(46, 163)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Show Contra A/c"
        '
        'TxtClosingStock
        '
        Me.TxtClosingStock.AgAllowUserToEnableMasterHelp = False
        Me.TxtClosingStock.AgMandatory = False
        Me.TxtClosingStock.AgMasterHelp = False
        Me.TxtClosingStock.AgNumberLeftPlaces = 0
        Me.TxtClosingStock.AgNumberNegetiveAllow = False
        Me.TxtClosingStock.AgNumberRightPlaces = 0
        Me.TxtClosingStock.AgPickFromLastValue = False
        Me.TxtClosingStock.AgRowFilter = ""
        Me.TxtClosingStock.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtClosingStock.AgSelectedValue = Nothing
        Me.TxtClosingStock.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtClosingStock.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtClosingStock.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtClosingStock.BackColor = System.Drawing.Color.White
        Me.TxtClosingStock.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtClosingStock.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtClosingStock.Location = New System.Drawing.Point(192, 182)
        Me.TxtClosingStock.MaxLength = 0
        Me.TxtClosingStock.Name = "TxtClosingStock"
        Me.TxtClosingStock.Size = New System.Drawing.Size(206, 18)
        Me.TxtClosingStock.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(46, 183)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 16)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Closing Stock"
        '
        'TxtAcGroup
        '
        Me.TxtAcGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtAcGroup.AgMandatory = False
        Me.TxtAcGroup.AgMasterHelp = False
        Me.TxtAcGroup.AgNumberLeftPlaces = 0
        Me.TxtAcGroup.AgNumberNegetiveAllow = False
        Me.TxtAcGroup.AgNumberRightPlaces = 0
        Me.TxtAcGroup.AgPickFromLastValue = False
        Me.TxtAcGroup.AgRowFilter = ""
        Me.TxtAcGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcGroup.AgSelectedValue = Nothing
        Me.TxtAcGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcGroup.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtAcGroup.BackColor = System.Drawing.Color.White
        Me.TxtAcGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcGroup.Location = New System.Drawing.Point(192, 102)
        Me.TxtAcGroup.MaxLength = 0
        Me.TxtAcGroup.Name = "TxtAcGroup"
        Me.TxtAcGroup.Size = New System.Drawing.Size(206, 18)
        Me.TxtAcGroup.TabIndex = 15
        '
        'LblAcGroup
        '
        Me.LblAcGroup.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAcGroup.AutoSize = True
        Me.LblAcGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcGroup.Location = New System.Drawing.Point(46, 103)
        Me.LblAcGroup.Name = "LblAcGroup"
        Me.LblAcGroup.Size = New System.Drawing.Size(67, 16)
        Me.LblAcGroup.TabIndex = 16
        Me.LblAcGroup.Text = "A/c Group"
        '
        'TxtCostCenter
        '
        Me.TxtCostCenter.AgAllowUserToEnableMasterHelp = False
        Me.TxtCostCenter.AgMandatory = False
        Me.TxtCostCenter.AgMasterHelp = False
        Me.TxtCostCenter.AgNumberLeftPlaces = 0
        Me.TxtCostCenter.AgNumberNegetiveAllow = False
        Me.TxtCostCenter.AgNumberRightPlaces = 0
        Me.TxtCostCenter.AgPickFromLastValue = False
        Me.TxtCostCenter.AgRowFilter = ""
        Me.TxtCostCenter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCostCenter.AgSelectedValue = Nothing
        Me.TxtCostCenter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCostCenter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCostCenter.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtCostCenter.BackColor = System.Drawing.Color.White
        Me.TxtCostCenter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCostCenter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCostCenter.Location = New System.Drawing.Point(192, 122)
        Me.TxtCostCenter.MaxLength = 0
        Me.TxtCostCenter.Name = "TxtCostCenter"
        Me.TxtCostCenter.Size = New System.Drawing.Size(206, 18)
        Me.TxtCostCenter.TabIndex = 17
        '
        'LblCostCenter
        '
        Me.LblCostCenter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblCostCenter.AutoSize = True
        Me.LblCostCenter.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCostCenter.Location = New System.Drawing.Point(46, 123)
        Me.LblCostCenter.Name = "LblCostCenter"
        Me.LblCostCenter.Size = New System.Drawing.Size(77, 16)
        Me.LblCostCenter.TabIndex = 18
        Me.LblCostCenter.Text = "Cost Center"
        '
        'FrmDisplayHierarchy_Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(453, 292)
        Me.Controls.Add(Me.TxtCostCenter)
        Me.Controls.Add(Me.LblCostCenter)
        Me.Controls.Add(Me.TxtAcGroup)
        Me.Controls.Add(Me.LblAcGroup)
        Me.Controls.Add(Me.TxtClosingStock)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtShowContra)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtZeroBalance)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtSite)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.TxtToDate)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.TxtFromDate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDisplayHierarchy_Settings"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TxtToDate As AgControls.AgTextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TxtFromDate As AgControls.AgTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents TxtSite As AgControls.AgTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtZeroBalance As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtShowContra As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtClosingStock As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtAcGroup As AgControls.AgTextBox
    Friend WithEvents LblAcGroup As System.Windows.Forms.Label
    Friend WithEvents TxtCostCenter As AgControls.AgTextBox
    Friend WithEvents LblCostCenter As System.Windows.Forms.Label
End Class
