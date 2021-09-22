<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNCat
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
        Me.TxtCategory = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.TxtNCatDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.LblBank_NameReq = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtHeaderTable = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtLineTable = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
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
        Me.Topctrl1.Size = New System.Drawing.Size(872, 41)
        Me.Topctrl1.TabIndex = 3
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
        'TxtCategory
        '
        Me.TxtCategory.AgAllowUserToEnableMasterHelp = False
        Me.TxtCategory.AgMandatory = True
        Me.TxtCategory.AgMasterHelp = True
        Me.TxtCategory.AgNumberLeftPlaces = 0
        Me.TxtCategory.AgNumberNegetiveAllow = False
        Me.TxtCategory.AgNumberRightPlaces = 0
        Me.TxtCategory.AgPickFromLastValue = False
        Me.TxtCategory.AgRowFilter = ""
        Me.TxtCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCategory.AgSelectedValue = Nothing
        Me.TxtCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCategory.Location = New System.Drawing.Point(326, 101)
        Me.TxtCategory.MaxLength = 20
        Me.TxtCategory.Name = "TxtCategory"
        Me.TxtCategory.Size = New System.Drawing.Size(150, 18)
        Me.TxtCategory.TabIndex = 0
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(225, 101)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(84, 16)
        Me.LblManualCode.TabIndex = 0
        Me.LblManualCode.Text = "Manual Code"
        '
        'TxtNCatDescription
        '
        Me.TxtNCatDescription.AgAllowUserToEnableMasterHelp = False
        Me.TxtNCatDescription.AgMandatory = True
        Me.TxtNCatDescription.AgMasterHelp = True
        Me.TxtNCatDescription.AgNumberLeftPlaces = 0
        Me.TxtNCatDescription.AgNumberNegetiveAllow = False
        Me.TxtNCatDescription.AgNumberRightPlaces = 0
        Me.TxtNCatDescription.AgPickFromLastValue = False
        Me.TxtNCatDescription.AgRowFilter = ""
        Me.TxtNCatDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNCatDescription.AgSelectedValue = Nothing
        Me.TxtNCatDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNCatDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNCatDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNCatDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNCatDescription.Location = New System.Drawing.Point(326, 122)
        Me.TxtNCatDescription.MaxLength = 50
        Me.TxtNCatDescription.Name = "TxtNCatDescription"
        Me.TxtNCatDescription.Size = New System.Drawing.Size(325, 18)
        Me.TxtNCatDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(225, 122)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblDescription.TabIndex = 0
        Me.LblDescription.Text = "Description"
        '
        'LblBank_NameReq
        '
        Me.LblBank_NameReq.AutoSize = True
        Me.LblBank_NameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblBank_NameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblBank_NameReq.Location = New System.Drawing.Point(310, 106)
        Me.LblBank_NameReq.Name = "LblBank_NameReq"
        Me.LblBank_NameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblBank_NameReq.TabIndex = 2
        Me.LblBank_NameReq.Text = "Ä"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(310, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Ä"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(310, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Ä"
        '
        'TxtHeaderTable
        '
        Me.TxtHeaderTable.AgAllowUserToEnableMasterHelp = False
        Me.TxtHeaderTable.AgMandatory = True
        Me.TxtHeaderTable.AgMasterHelp = False
        Me.TxtHeaderTable.AgNumberLeftPlaces = 0
        Me.TxtHeaderTable.AgNumberNegetiveAllow = False
        Me.TxtHeaderTable.AgNumberRightPlaces = 0
        Me.TxtHeaderTable.AgPickFromLastValue = False
        Me.TxtHeaderTable.AgRowFilter = ""
        Me.TxtHeaderTable.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtHeaderTable.AgSelectedValue = Nothing
        Me.TxtHeaderTable.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtHeaderTable.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtHeaderTable.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtHeaderTable.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHeaderTable.Location = New System.Drawing.Point(326, 143)
        Me.TxtHeaderTable.MaxLength = 50
        Me.TxtHeaderTable.Name = "TxtHeaderTable"
        Me.TxtHeaderTable.Size = New System.Drawing.Size(325, 18)
        Me.TxtHeaderTable.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(225, 143)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Header Table"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(310, 171)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Ä"
        '
        'TxtLineTable
        '
        Me.TxtLineTable.AgAllowUserToEnableMasterHelp = False
        Me.TxtLineTable.AgMandatory = True
        Me.TxtLineTable.AgMasterHelp = False
        Me.TxtLineTable.AgNumberLeftPlaces = 0
        Me.TxtLineTable.AgNumberNegetiveAllow = False
        Me.TxtLineTable.AgNumberRightPlaces = 0
        Me.TxtLineTable.AgPickFromLastValue = False
        Me.TxtLineTable.AgRowFilter = ""
        Me.TxtLineTable.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLineTable.AgSelectedValue = Nothing
        Me.TxtLineTable.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLineTable.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtLineTable.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLineTable.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLineTable.Location = New System.Drawing.Point(326, 164)
        Me.TxtLineTable.MaxLength = 50
        Me.TxtLineTable.Name = "TxtLineTable"
        Me.TxtLineTable.Size = New System.Drawing.Size(325, 18)
        Me.TxtLineTable.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(225, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Line Table"
        '
        'TxtStructure
        '
        Me.TxtStructure.AgAllowUserToEnableMasterHelp = False
        Me.TxtStructure.AgMandatory = True
        Me.TxtStructure.AgMasterHelp = False
        Me.TxtStructure.AgNumberLeftPlaces = 0
        Me.TxtStructure.AgNumberNegetiveAllow = False
        Me.TxtStructure.AgNumberRightPlaces = 0
        Me.TxtStructure.AgPickFromLastValue = False
        Me.TxtStructure.AgRowFilter = ""
        Me.TxtStructure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtStructure.AgSelectedValue = Nothing
        Me.TxtStructure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStructure.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStructure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStructure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStructure.Location = New System.Drawing.Point(326, 185)
        Me.TxtStructure.MaxLength = 50
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(150, 18)
        Me.TxtStructure.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(225, 185)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 16)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Structure"
        '
        'FrmNCat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 266)
        Me.Controls.Add(Me.TxtStructure)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtLineTable)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtHeaderTable)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblBank_NameReq)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.TxtCategory)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtNCatDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmNCat"
        Me.Text = "Charges Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents TxtCategory As AgControls.AgTextBox
    Friend WithEvents LblManualCode As System.Windows.Forms.Label
    Friend WithEvents TxtNCatDescription As AgControls.AgTextBox
    Friend WithEvents LblDescription As System.Windows.Forms.Label
    Friend WithEvents LblBank_NameReq As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtHeaderTable As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtLineTable As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtStructure As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
