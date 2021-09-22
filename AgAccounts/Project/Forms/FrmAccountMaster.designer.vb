<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountMaster
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtManualCode = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAcGroup = New AgControls.AgTextBox()
        Me.cmbPartyName = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNature = New AgControls.AgTextBox()
        Me.BtnAccountDetail = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Topctrl1 = New Topctrl.Topctrl()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtCostCenter = New AgControls.AgTextBox()
        Me.BtnOtherDetails = New System.Windows.Forms.Button()
        Me.TxtSalesTaxPostingGroup = New AgControls.AgTextBox()
        Me.LblSalesTaxGroup = New System.Windows.Forms.Label()
        Me.TxtSalesTaxNo = New AgControls.AgTextBox()
        Me.LblSalesTaxNo = New System.Windows.Forms.Label()
        Me.TxtHsn = New AgControls.AgTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(173, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 16)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Manual Code"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(173, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Account  Name"
        '
        'txtManualCode
        '
        Me.txtManualCode.AgAllowUserToEnableMasterHelp = False
        Me.txtManualCode.AgLastValueTag = Nothing
        Me.txtManualCode.AgLastValueText = Nothing
        Me.txtManualCode.AgMandatory = False
        Me.txtManualCode.AgMasterHelp = False
        Me.txtManualCode.AgNumberLeftPlaces = 0
        Me.txtManualCode.AgNumberNegetiveAllow = False
        Me.txtManualCode.AgNumberRightPlaces = 0
        Me.txtManualCode.AgPickFromLastValue = False
        Me.txtManualCode.AgRowFilter = ""
        Me.txtManualCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtManualCode.AgSelectedValue = Nothing
        Me.txtManualCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtManualCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtManualCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtManualCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualCode.Location = New System.Drawing.Point(332, 120)
        Me.txtManualCode.MaxLength = 8
        Me.txtManualCode.Name = "txtManualCode"
        Me.txtManualCode.Size = New System.Drawing.Size(154, 18)
        Me.txtManualCode.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(173, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 16)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "A/c Group"
        '
        'txtAcGroup
        '
        Me.txtAcGroup.AgAllowUserToEnableMasterHelp = False
        Me.txtAcGroup.AgLastValueTag = Nothing
        Me.txtAcGroup.AgLastValueText = Nothing
        Me.txtAcGroup.AgMandatory = False
        Me.txtAcGroup.AgMasterHelp = False
        Me.txtAcGroup.AgNumberLeftPlaces = 0
        Me.txtAcGroup.AgNumberNegetiveAllow = False
        Me.txtAcGroup.AgNumberRightPlaces = 0
        Me.txtAcGroup.AgPickFromLastValue = False
        Me.txtAcGroup.AgRowFilter = ""
        Me.txtAcGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtAcGroup.AgSelectedValue = Nothing
        Me.txtAcGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtAcGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtAcGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAcGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcGroup.Location = New System.Drawing.Point(332, 140)
        Me.txtAcGroup.MaxLength = 50
        Me.txtAcGroup.Name = "txtAcGroup"
        Me.txtAcGroup.Size = New System.Drawing.Size(296, 18)
        Me.txtAcGroup.TabIndex = 2
        '
        'cmbPartyName
        '
        Me.cmbPartyName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPartyName.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbPartyName.FormattingEnabled = True
        Me.cmbPartyName.Location = New System.Drawing.Point(332, 93)
        Me.cmbPartyName.MaxLength = 50
        Me.cmbPartyName.Name = "cmbPartyName"
        Me.cmbPartyName.Size = New System.Drawing.Size(296, 25)
        Me.cmbPartyName.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(173, 160)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(46, 16)
        Me.Label18.TabIndex = 194
        Me.Label18.Text = "Nature"
        '
        'txtNature
        '
        Me.txtNature.AgAllowUserToEnableMasterHelp = False
        Me.txtNature.AgLastValueTag = Nothing
        Me.txtNature.AgLastValueText = Nothing
        Me.txtNature.AgMandatory = False
        Me.txtNature.AgMasterHelp = False
        Me.txtNature.AgNumberLeftPlaces = 0
        Me.txtNature.AgNumberNegetiveAllow = False
        Me.txtNature.AgNumberRightPlaces = 0
        Me.txtNature.AgPickFromLastValue = False
        Me.txtNature.AgRowFilter = ""
        Me.txtNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtNature.AgSelectedValue = Nothing
        Me.txtNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNature.Location = New System.Drawing.Point(332, 160)
        Me.txtNature.MaxLength = 11
        Me.txtNature.Name = "txtNature"
        Me.txtNature.Size = New System.Drawing.Size(154, 18)
        Me.txtNature.TabIndex = 3
        '
        'BtnAccountDetail
        '
        Me.BtnAccountDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAccountDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAccountDetail.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAccountDetail.Location = New System.Drawing.Point(176, 301)
        Me.BtnAccountDetail.Name = "BtnAccountDetail"
        Me.BtnAccountDetail.Size = New System.Drawing.Size(221, 43)
        Me.BtnAccountDetail.TabIndex = 5
        Me.BtnAccountDetail.Text = "Account &Details"
        Me.BtnAccountDetail.UseVisualStyleBackColor = True
        Me.BtnAccountDetail.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(317, 93)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(10, 7)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Ä"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(317, 120)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(10, 7)
        Me.Label20.TabIndex = 196
        Me.Label20.Text = "Ä"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(317, 140)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(10, 7)
        Me.Label21.TabIndex = 197
        Me.Label21.Text = "Ä"
        '
        'Topctrl1
        '
        Me.Topctrl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(883, 41)
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
        'Label5
        '
        Me.Label5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(173, 180)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 16)
        Me.Label5.TabIndex = 203
        Me.Label5.Text = "Cost Center"
        '
        'TxtCostCenter
        '
        Me.TxtCostCenter.AgAllowUserToEnableMasterHelp = False
        Me.TxtCostCenter.AgLastValueTag = Nothing
        Me.TxtCostCenter.AgLastValueText = Nothing
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
        Me.TxtCostCenter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCostCenter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCostCenter.Location = New System.Drawing.Point(332, 180)
        Me.TxtCostCenter.MaxLength = 11
        Me.TxtCostCenter.Name = "TxtCostCenter"
        Me.TxtCostCenter.Size = New System.Drawing.Size(154, 18)
        Me.TxtCostCenter.TabIndex = 4
        '
        'BtnOtherDetails
        '
        Me.BtnOtherDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOtherDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOtherDetails.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOtherDetails.Location = New System.Drawing.Point(407, 301)
        Me.BtnOtherDetails.Name = "BtnOtherDetails"
        Me.BtnOtherDetails.Size = New System.Drawing.Size(221, 43)
        Me.BtnOtherDetails.TabIndex = 204
        Me.BtnOtherDetails.Text = "&Other Details"
        Me.BtnOtherDetails.UseVisualStyleBackColor = True
        Me.BtnOtherDetails.Visible = False
        '
        'TxtSalesTaxPostingGroup
        '
        Me.TxtSalesTaxPostingGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxPostingGroup.AgLastValueTag = Nothing
        Me.TxtSalesTaxPostingGroup.AgLastValueText = Nothing
        Me.TxtSalesTaxPostingGroup.AgMandatory = False
        Me.TxtSalesTaxPostingGroup.AgMasterHelp = False
        Me.TxtSalesTaxPostingGroup.AgNumberLeftPlaces = 0
        Me.TxtSalesTaxPostingGroup.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxPostingGroup.AgNumberRightPlaces = 0
        Me.TxtSalesTaxPostingGroup.AgPickFromLastValue = False
        Me.TxtSalesTaxPostingGroup.AgRowFilter = ""
        Me.TxtSalesTaxPostingGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxPostingGroup.AgSelectedValue = Nothing
        Me.TxtSalesTaxPostingGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxPostingGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxPostingGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxPostingGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxPostingGroup.Location = New System.Drawing.Point(332, 200)
        Me.TxtSalesTaxPostingGroup.MaxLength = 20
        Me.TxtSalesTaxPostingGroup.Name = "TxtSalesTaxPostingGroup"
        Me.TxtSalesTaxPostingGroup.Size = New System.Drawing.Size(296, 18)
        Me.TxtSalesTaxPostingGroup.TabIndex = 5
        '
        'LblSalesTaxGroup
        '
        Me.LblSalesTaxGroup.AutoSize = True
        Me.LblSalesTaxGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxGroup.Location = New System.Drawing.Point(173, 200)
        Me.LblSalesTaxGroup.Name = "LblSalesTaxGroup"
        Me.LblSalesTaxGroup.Size = New System.Drawing.Size(104, 16)
        Me.LblSalesTaxGroup.TabIndex = 892
        Me.LblSalesTaxGroup.Text = "Sales Tax Group"
        '
        'TxtSalesTaxNo
        '
        Me.TxtSalesTaxNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxNo.AgLastValueTag = Nothing
        Me.TxtSalesTaxNo.AgLastValueText = Nothing
        Me.TxtSalesTaxNo.AgMandatory = False
        Me.TxtSalesTaxNo.AgMasterHelp = False
        Me.TxtSalesTaxNo.AgNumberLeftPlaces = 0
        Me.TxtSalesTaxNo.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxNo.AgNumberRightPlaces = 0
        Me.TxtSalesTaxNo.AgPickFromLastValue = False
        Me.TxtSalesTaxNo.AgRowFilter = ""
        Me.TxtSalesTaxNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxNo.AgSelectedValue = Nothing
        Me.TxtSalesTaxNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxNo.Location = New System.Drawing.Point(332, 238)
        Me.TxtSalesTaxNo.MaxLength = 20
        Me.TxtSalesTaxNo.Name = "TxtSalesTaxNo"
        Me.TxtSalesTaxNo.Size = New System.Drawing.Size(296, 18)
        Me.TxtSalesTaxNo.TabIndex = 7
        '
        'LblSalesTaxNo
        '
        Me.LblSalesTaxNo.AutoSize = True
        Me.LblSalesTaxNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxNo.Location = New System.Drawing.Point(173, 240)
        Me.LblSalesTaxNo.Name = "LblSalesTaxNo"
        Me.LblSalesTaxNo.Size = New System.Drawing.Size(85, 16)
        Me.LblSalesTaxNo.TabIndex = 896
        Me.LblSalesTaxNo.Text = "Sales Tax No"
        '
        'TxtHsn
        '
        Me.TxtHsn.AgAllowUserToEnableMasterHelp = False
        Me.TxtHsn.AgLastValueTag = Nothing
        Me.TxtHsn.AgLastValueText = Nothing
        Me.TxtHsn.AgMandatory = False
        Me.TxtHsn.AgMasterHelp = False
        Me.TxtHsn.AgNumberLeftPlaces = 8
        Me.TxtHsn.AgNumberNegetiveAllow = False
        Me.TxtHsn.AgNumberRightPlaces = 0
        Me.TxtHsn.AgPickFromLastValue = False
        Me.TxtHsn.AgRowFilter = ""
        Me.TxtHsn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtHsn.AgSelectedValue = Nothing
        Me.TxtHsn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtHsn.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtHsn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtHsn.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHsn.Location = New System.Drawing.Point(332, 220)
        Me.TxtHsn.MaxLength = 8
        Me.TxtHsn.Name = "TxtHsn"
        Me.TxtHsn.Size = New System.Drawing.Size(154, 16)
        Me.TxtHsn.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(173, 220)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 16)
        Me.Label9.TabIndex = 1067
        Me.Label9.Text = "HSN Code"
        '
        'FrmAccountMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 352)
        Me.Controls.Add(Me.TxtHsn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtSalesTaxNo)
        Me.Controls.Add(Me.LblSalesTaxNo)
        Me.Controls.Add(Me.TxtSalesTaxPostingGroup)
        Me.Controls.Add(Me.LblSalesTaxGroup)
        Me.Controls.Add(Me.BtnOtherDetails)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtCostCenter)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.BtnAccountDetail)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtNature)
        Me.Controls.Add(Me.cmbPartyName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtAcGroup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtManualCode)
        Me.Controls.Add(Me.Topctrl1)
        Me.KeyPreview = True
        Me.Name = "FrmAccountMaster"
        Me.Text = "Account Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtManualCode As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAcGroup As AgControls.AgTextBox
    Friend WithEvents cmbPartyName As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNature As AgControls.AgTextBox
    Friend WithEvents BtnAccountDetail As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtCostCenter As AgControls.AgTextBox
    Friend WithEvents BtnOtherDetails As System.Windows.Forms.Button
    Protected WithEvents TxtSalesTaxPostingGroup As AgControls.AgTextBox
    Protected WithEvents LblSalesTaxGroup As Label
    Protected WithEvents TxtSalesTaxNo As AgControls.AgTextBox
    Protected WithEvents LblSalesTaxNo As Label
    Public WithEvents TxtHsn As AgControls.AgTextBox
    Public WithEvents Label9 As Label
End Class
