<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDisplayHierarchy_Settings_Stock
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
        Me.TxtZeroBalance = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtMethod = New AgControls.AgTextBox
        Me.TxtItemCategory = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtItemGroup = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtItemName = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtGodown = New AgControls.AgTextBox
        Me.TxtSite = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label23
        '
        Me.Label23.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(21, 55)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(53, 16)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "To Date"
        '
        'TxtToDate
        '
        Me.TxtToDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtToDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToDate.Location = New System.Drawing.Point(161, 55)
        Me.TxtToDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtToDate.MaxLength = 25
        Me.TxtToDate.Name = "TxtToDate"
        Me.TxtToDate.Size = New System.Drawing.Size(110, 18)
        Me.TxtToDate.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(21, 35)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(69, 16)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "From Date"
        '
        'TxtFromDate
        '
        Me.TxtFromDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromDate.Location = New System.Drawing.Point(161, 35)
        Me.TxtFromDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtFromDate.MaxLength = 25
        Me.TxtFromDate.Name = "TxtFromDate"
        Me.TxtFromDate.Size = New System.Drawing.Size(110, 18)
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
        Me.BtnCancel.TabIndex = 10
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
        Me.BtnOK.TabIndex = 9
        Me.BtnOK.Text = "O&k"
        Me.BtnOK.UseVisualStyleBackColor = False
        '
        'TxtZeroBalance
        '
        Me.TxtZeroBalance.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtZeroBalance.BackColor = System.Drawing.Color.White
        Me.TxtZeroBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtZeroBalance.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtZeroBalance.Location = New System.Drawing.Point(161, 75)
        Me.TxtZeroBalance.MaxLength = 0
        Me.TxtZeroBalance.Name = "TxtZeroBalance"
        Me.TxtZeroBalance.Size = New System.Drawing.Size(110, 18)
        Me.TxtZeroBalance.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 16)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Show A/c  Zero Value"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Method"
        '
        'TxtMethod
        '
        Me.TxtMethod.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtMethod.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMethod.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMethod.Location = New System.Drawing.Point(161, 95)
        Me.TxtMethod.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtMethod.MaxLength = 25
        Me.TxtMethod.Name = "TxtMethod"
        Me.TxtMethod.Size = New System.Drawing.Size(110, 18)
        Me.TxtMethod.TabIndex = 3
        '
        'TxtItemCategory
        '
        Me.TxtItemCategory.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtItemCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemCategory.Location = New System.Drawing.Point(161, 115)
        Me.TxtItemCategory.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtItemCategory.MaxLength = 25
        Me.TxtItemCategory.Name = "TxtItemCategory"
        Me.TxtItemCategory.Size = New System.Drawing.Size(275, 18)
        Me.TxtItemCategory.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Item Category"
        '
        'TxtItemGroup
        '
        Me.TxtItemGroup.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtItemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemGroup.Location = New System.Drawing.Point(161, 135)
        Me.TxtItemGroup.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtItemGroup.MaxLength = 25
        Me.TxtItemGroup.Name = "TxtItemGroup"
        Me.TxtItemGroup.Size = New System.Drawing.Size(275, 18)
        Me.TxtItemGroup.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 135)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Item Group"
        '
        'TxtItemName
        '
        Me.TxtItemName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtItemName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemName.Location = New System.Drawing.Point(161, 155)
        Me.TxtItemName.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtItemName.MaxLength = 25
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(275, 18)
        Me.TxtItemName.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(21, 155)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 16)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Item Name"
        '
        'TxtGodown
        '
        Me.TxtGodown.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGodown.Location = New System.Drawing.Point(161, 175)
        Me.TxtGodown.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtGodown.MaxLength = 25
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(275, 18)
        Me.TxtGodown.TabIndex = 7
        '
        'TxtSite
        '
        Me.TxtSite.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtSite.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSite.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite.Location = New System.Drawing.Point(161, 195)
        Me.TxtSite.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtSite.MaxLength = 25
        Me.TxtSite.Name = "TxtSite"
        Me.TxtSite.Size = New System.Drawing.Size(275, 18)
        Me.TxtSite.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 176)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 16)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Godown"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(21, 197)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 16)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "Site"
        '
        'FrmDisplayHierarchy_Settings_Stock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(453, 292)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtSite)
        Me.Controls.Add(Me.TxtGodown)
        Me.Controls.Add(Me.TxtItemName)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtItemGroup)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtItemCategory)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtMethod)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtZeroBalance)
        Me.Controls.Add(Me.Label1)
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
        Me.Name = "FrmDisplayHierarchy_Settings_Stock"
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
    Friend WithEvents TxtZeroBalance As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtMethod As AgControls.AgTextBox
    Friend WithEvents TxtItemCategory As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtItemGroup As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtItemName As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtGodown As AgControls.AgTextBox
    Friend WithEvents TxtSite As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
