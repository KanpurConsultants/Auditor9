<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVoucherEntry_ReferenceFilter
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
        Me.TxtParty = New AgControls.AgTextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.TxtBillNo = New AgControls.AgTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.BtnOK = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtFromDate = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtToDate = New AgControls.AgTextBox
        Me.SuspendLayout()
        '
        'Label23
        '
        Me.Label23.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(85, 80)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(39, 16)
        Me.Label23.TabIndex = 83
        Me.Label23.Text = "Party"
        '
        'TxtParty
        '
        Me.TxtParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtParty.AgMandatory = False
        Me.TxtParty.AgMasterHelp = False
        Me.TxtParty.AgNumberLeftPlaces = 0
        Me.TxtParty.AgNumberNegetiveAllow = False
        Me.TxtParty.AgNumberRightPlaces = 0
        Me.TxtParty.AgPickFromLastValue = False
        Me.TxtParty.AgRowFilter = ""
        Me.TxtParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtParty.AgSelectedValue = Nothing
        Me.TxtParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtParty.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParty.Location = New System.Drawing.Point(160, 80)
        Me.TxtParty.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtParty.MaxLength = 25
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(278, 18)
        Me.TxtParty.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(85, 59)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(46, 16)
        Me.Label24.TabIndex = 81
        Me.Label24.Text = "Bill No"
        '
        'TxtBillNo
        '
        Me.TxtBillNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtBillNo.AgMandatory = False
        Me.TxtBillNo.AgMasterHelp = False
        Me.TxtBillNo.AgNumberLeftPlaces = 0
        Me.TxtBillNo.AgNumberNegetiveAllow = False
        Me.TxtBillNo.AgNumberRightPlaces = 0
        Me.TxtBillNo.AgPickFromLastValue = False
        Me.TxtBillNo.AgRowFilter = ""
        Me.TxtBillNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillNo.AgSelectedValue = Nothing
        Me.TxtBillNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillNo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillNo.Location = New System.Drawing.Point(160, 59)
        Me.TxtBillNo.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtBillNo.MaxLength = 20
        Me.TxtBillNo.Name = "TxtBillNo"
        Me.TxtBillNo.Size = New System.Drawing.Size(278, 18)
        Me.TxtBillNo.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(8, 111)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(509, 9)
        Me.GroupBox2.TabIndex = 116
        Me.GroupBox2.TabStop = False
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.BackColor = System.Drawing.Color.Transparent
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnCancel.Location = New System.Drawing.Point(433, 126)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(84, 24)
        Me.BtnCancel.TabIndex = 5
        Me.BtnCancel.Text = "Canc&el"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.BackColor = System.Drawing.Color.Transparent
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnOK.Location = New System.Drawing.Point(344, 126)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(84, 24)
        Me.BtnOK.TabIndex = 4
        Me.BtnOK.Text = "O&k"
        Me.BtnOK.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(85, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 118
        Me.Label1.Text = "From Date"
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
        Me.TxtFromDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtFromDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromDate.Location = New System.Drawing.Point(160, 38)
        Me.TxtFromDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtFromDate.MaxLength = 20
        Me.TxtFromDate.Name = "TxtFromDate"
        Me.TxtFromDate.Size = New System.Drawing.Size(114, 18)
        Me.TxtFromDate.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(280, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 16)
        Me.Label2.TabIndex = 120
        Me.Label2.Text = "To Date"
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
        Me.TxtToDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtToDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtToDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToDate.Location = New System.Drawing.Point(340, 38)
        Me.TxtToDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtToDate.MaxLength = 20
        Me.TxtToDate.Name = "TxtToDate"
        Me.TxtToDate.Size = New System.Drawing.Size(98, 18)
        Me.TxtToDate.TabIndex = 1
        '
        'FrmVoucherEntry_ReferenceFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(522, 156)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtToDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtFromDate)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.TxtParty)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.TxtBillNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmVoucherEntry_ReferenceFilter"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cheque Detail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TxtParty As AgControls.AgTextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TxtBillNo As AgControls.AgTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtFromDate As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtToDate As AgControls.AgTextBox
End Class
