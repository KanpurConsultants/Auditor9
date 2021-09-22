<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomFields
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
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TxtModified = New System.Windows.Forms.TextBox
        Me.GrpUP = New System.Windows.Forms.GroupBox
        Me.TxtPrepared = New System.Windows.Forms.TextBox
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtDescription = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtCopyFrom = New AgControls.AgTextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BtnCopyFrom = New System.Windows.Forms.Button
        Me.TxtTableName = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtCode = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtPrimaryField = New AgControls.AgTextBox
        Me.GroupBox4.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.TxtModified)
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox4.Location = New System.Drawing.Point(784, 575)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(186, 51)
        Me.GroupBox4.TabIndex = 294
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Tag = "TR"
        Me.GroupBox4.Text = "Modified By "
        Me.GroupBox4.Visible = False
        '
        'TxtModified
        '
        Me.TxtModified.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtModified.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtModified.Enabled = False
        Me.TxtModified.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModified.Location = New System.Drawing.Point(15, 21)
        Me.TxtModified.Name = "TxtModified"
        Me.TxtModified.Size = New System.Drawing.Size(158, 18)
        Me.TxtModified.TabIndex = 0
        Me.TxtModified.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GrpUP
        '
        Me.GrpUP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpUP.Controls.Add(Me.TxtPrepared)
        Me.GrpUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpUP.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GrpUP.ForeColor = System.Drawing.Color.Maroon
        Me.GrpUP.Location = New System.Drawing.Point(5, 575)
        Me.GrpUP.Name = "GrpUP"
        Me.GrpUP.Size = New System.Drawing.Size(186, 51)
        Me.GrpUP.TabIndex = 293
        Me.GrpUP.TabStop = False
        Me.GrpUP.Tag = "TR"
        Me.GrpUP.Text = "Prepared By "
        '
        'TxtPrepared
        '
        Me.TxtPrepared.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtPrepared.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPrepared.Enabled = False
        Me.TxtPrepared.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrepared.Location = New System.Drawing.Point(15, 21)
        Me.TxtPrepared.Name = "TxtPrepared"
        Me.TxtPrepared.Size = New System.Drawing.Size(158, 18)
        Me.TxtPrepared.TabIndex = 0
        Me.TxtPrepared.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Topctrl1
        '
        Me.Topctrl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(982, 41)
        Me.Topctrl1.TabIndex = 5
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
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(-4, 568)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1046, 4)
        Me.GroupBox2.TabIndex = 295
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(12, 141)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(958, 421)
        Me.Pnl1.TabIndex = 4
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
        Me.TxtDescription.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(122, 64)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(325, 21)
        Me.TxtDescription.TabIndex = 1
        Me.TxtDescription.Text = "TxtItem"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 301
        Me.Label1.Text = "Description"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(106, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 302
        Me.Label2.Text = "Ä"
        '
        'TxtCopyFrom
        '
        Me.TxtCopyFrom.AgMandatory = True
        Me.TxtCopyFrom.AgMasterHelp = False
        Me.TxtCopyFrom.AgNumberLeftPlaces = 0
        Me.TxtCopyFrom.AgNumberNegetiveAllow = False
        Me.TxtCopyFrom.AgNumberRightPlaces = 0
        Me.TxtCopyFrom.AgPickFromLastValue = False
        Me.TxtCopyFrom.AgRowFilter = ""
        Me.TxtCopyFrom.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCopyFrom.AgSelectedValue = Nothing
        Me.TxtCopyFrom.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCopyFrom.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCopyFrom.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCopyFrom.Location = New System.Drawing.Point(6, 17)
        Me.TxtCopyFrom.MaxLength = 50
        Me.TxtCopyFrom.Name = "TxtCopyFrom"
        Me.TxtCopyFrom.Size = New System.Drawing.Size(227, 21)
        Me.TxtCopyFrom.TabIndex = 306
        Me.TxtCopyFrom.TabStop = False
        Me.TxtCopyFrom.Text = "TxtItem"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnCopyFrom)
        Me.GroupBox1.Controls.Add(Me.TxtCopyFrom)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(709, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(261, 55)
        Me.GroupBox1.TabIndex = 307
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Copy From"
        '
        'BtnCopyFrom
        '
        Me.BtnCopyFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCopyFrom.Location = New System.Drawing.Point(229, 17)
        Me.BtnCopyFrom.Name = "BtnCopyFrom"
        Me.BtnCopyFrom.Size = New System.Drawing.Size(26, 21)
        Me.BtnCopyFrom.TabIndex = 307
        Me.BtnCopyFrom.TabStop = False
        Me.BtnCopyFrom.Text = "C"
        Me.BtnCopyFrom.UseVisualStyleBackColor = True
        '
        'TxtTableName
        '
        Me.TxtTableName.AgMandatory = False
        Me.TxtTableName.AgMasterHelp = True
        Me.TxtTableName.AgNumberLeftPlaces = 0
        Me.TxtTableName.AgNumberNegetiveAllow = False
        Me.TxtTableName.AgNumberRightPlaces = 0
        Me.TxtTableName.AgPickFromLastValue = False
        Me.TxtTableName.AgRowFilter = ""
        Me.TxtTableName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTableName.AgSelectedValue = Nothing
        Me.TxtTableName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTableName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTableName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTableName.Location = New System.Drawing.Point(122, 86)
        Me.TxtTableName.MaxLength = 50
        Me.TxtTableName.Name = "TxtTableName"
        Me.TxtTableName.Size = New System.Drawing.Size(325, 21)
        Me.TxtTableName.TabIndex = 3
        Me.TxtTableName.Text = "TxtItem"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(14, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 309
        Me.Label5.Text = "Table Name"
        '
        'TxtCode
        '
        Me.TxtCode.AgMandatory = True
        Me.TxtCode.AgMasterHelp = True
        Me.TxtCode.AgNumberLeftPlaces = 0
        Me.TxtCode.AgNumberNegetiveAllow = False
        Me.TxtCode.AgNumberRightPlaces = 0
        Me.TxtCode.AgPickFromLastValue = False
        Me.TxtCode.AgRowFilter = ""
        Me.TxtCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCode.AgSelectedValue = Nothing
        Me.TxtCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.Location = New System.Drawing.Point(122, 42)
        Me.TxtCode.MaxLength = 10
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.Size = New System.Drawing.Size(115, 21)
        Me.TxtCode.TabIndex = 0
        Me.TxtCode.Text = "TxtItem"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(106, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 305
        Me.Label3.Text = "Ä"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 304
        Me.Label4.Text = "Code"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 111)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 311
        Me.Label6.Text = "Primary Field"
        '
        'TxtPrimaryField
        '
        Me.TxtPrimaryField.AgMandatory = False
        Me.TxtPrimaryField.AgMasterHelp = True
        Me.TxtPrimaryField.AgNumberLeftPlaces = 0
        Me.TxtPrimaryField.AgNumberNegetiveAllow = False
        Me.TxtPrimaryField.AgNumberRightPlaces = 0
        Me.TxtPrimaryField.AgPickFromLastValue = False
        Me.TxtPrimaryField.AgRowFilter = ""
        Me.TxtPrimaryField.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPrimaryField.AgSelectedValue = Nothing
        Me.TxtPrimaryField.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPrimaryField.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPrimaryField.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrimaryField.Location = New System.Drawing.Point(122, 108)
        Me.TxtPrimaryField.MaxLength = 50
        Me.TxtPrimaryField.Name = "TxtPrimaryField"
        Me.TxtPrimaryField.Size = New System.Drawing.Size(325, 21)
        Me.TxtPrimaryField.TabIndex = 310
        Me.TxtPrimaryField.Text = "TxtItem"
        '
        'FrmCustomFields
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 622)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtPrimaryField)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtTableName)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GrpUP)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Pnl1)
        Me.KeyPreview = True
        Me.Name = "FrmCustomFields"
        Me.Text = "FrmStoreItemMaster"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtModified As System.Windows.Forms.TextBox
    Friend WithEvents GrpUP As System.Windows.Forms.GroupBox
    Friend WithEvents TxtPrepared As System.Windows.Forms.TextBox
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents TxtDescription As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtCopyFrom As AgControls.AgTextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnCopyFrom As System.Windows.Forms.Button
    Friend WithEvents TxtTableName As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtCode As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtPrimaryField As AgControls.AgTextBox
End Class
