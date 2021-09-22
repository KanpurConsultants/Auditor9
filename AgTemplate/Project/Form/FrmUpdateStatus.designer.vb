<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUpdateStatus
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
        Me.TxtStatus = New AgControls.AgTextBox
        Me.LblStatus = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.TxtEntryDate = New AgControls.AgTextBox
        Me.LblEntryDate = New System.Windows.Forms.Label
        Me.TxtEntryBy = New AgControls.AgTextBox
        Me.LblEntryBy = New System.Windows.Forms.Label
        Me.TxtRemark = New AgControls.AgTextBox
        Me.LblRemark = New System.Windows.Forms.Label
        Me.BtnOK = New System.Windows.Forms.Button
        Me.TxtReminderDate = New AgControls.AgTextBox
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '

        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(454, 41)
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
        Me.Topctrl1.Visible = False
        '
        'TxtStatus
        '
        Me.TxtStatus.AgAllowUserToEnableMasterHelp = False
        Me.TxtStatus.AgMandatory = True
        Me.TxtStatus.AgMasterHelp = False
        Me.TxtStatus.AgNumberLeftPlaces = 0
        Me.TxtStatus.AgNumberNegetiveAllow = False
        Me.TxtStatus.AgNumberRightPlaces = 0
        Me.TxtStatus.AgPickFromLastValue = False
        Me.TxtStatus.AgRowFilter = ""

        Me.TxtStatus.AgSelectedValue = Nothing
        Me.TxtStatus.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStatus.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtStatus.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStatus.Location = New System.Drawing.Point(158, 11)
        Me.TxtStatus.MaxLength = 10
        Me.TxtStatus.Name = "TxtStatus"
        Me.TxtStatus.Size = New System.Drawing.Size(196, 21)
        Me.TxtStatus.TabIndex = 0
        '
        'LblStatus
        '
        Me.LblStatus.AutoSize = True
        Me.LblStatus.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStatus.ForeColor = System.Drawing.Color.Black
        Me.LblStatus.Location = New System.Drawing.Point(77, 11)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(51, 16)
        Me.LblStatus.TabIndex = 7
        Me.LblStatus.Text = "Status"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.TxtEntryDate)
        Me.Panel1.Controls.Add(Me.LblEntryDate)
        Me.Panel1.Controls.Add(Me.TxtEntryBy)
        Me.Panel1.Controls.Add(Me.LblEntryBy)
        Me.Panel1.Controls.Add(Me.TxtRemark)
        Me.Panel1.Controls.Add(Me.LblRemark)
        Me.Panel1.Controls.Add(Me.LblStatus)
        Me.Panel1.Controls.Add(Me.TxtStatus)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(454, 165)
        Me.Panel1.TabIndex = 12
        '
        'TxtEntryDate
        '
        Me.TxtEntryDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtEntryDate.AgMandatory = True
        Me.TxtEntryDate.AgMasterHelp = True
        Me.TxtEntryDate.AgNumberLeftPlaces = 0
        Me.TxtEntryDate.AgNumberNegetiveAllow = False
        Me.TxtEntryDate.AgNumberRightPlaces = 0
        Me.TxtEntryDate.AgPickFromLastValue = False
        Me.TxtEntryDate.AgRowFilter = ""

        Me.TxtEntryDate.AgSelectedValue = Nothing
        Me.TxtEntryDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEntryDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtEntryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEntryDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntryDate.Location = New System.Drawing.Point(158, 127)
        Me.TxtEntryDate.MaxLength = 10
        Me.TxtEntryDate.Name = "TxtEntryDate"
        Me.TxtEntryDate.Size = New System.Drawing.Size(196, 21)
        Me.TxtEntryDate.TabIndex = 13
        '
        'LblEntryDate
        '
        Me.LblEntryDate.AutoSize = True
        Me.LblEntryDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEntryDate.ForeColor = System.Drawing.Color.Black
        Me.LblEntryDate.Location = New System.Drawing.Point(78, 129)
        Me.LblEntryDate.Name = "LblEntryDate"
        Me.LblEntryDate.Size = New System.Drawing.Size(73, 14)
        Me.LblEntryDate.TabIndex = 12
        Me.LblEntryDate.Text = "Entry Date"
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.AgAllowUserToEnableMasterHelp = False
        Me.TxtEntryBy.AgMandatory = True
        Me.TxtEntryBy.AgMasterHelp = True
        Me.TxtEntryBy.AgNumberLeftPlaces = 0
        Me.TxtEntryBy.AgNumberNegetiveAllow = False
        Me.TxtEntryBy.AgNumberRightPlaces = 0
        Me.TxtEntryBy.AgPickFromLastValue = False
        Me.TxtEntryBy.AgRowFilter = ""

        Me.TxtEntryBy.AgSelectedValue = Nothing
        Me.TxtEntryBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEntryBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEntryBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEntryBy.Enabled = False
        Me.TxtEntryBy.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntryBy.Location = New System.Drawing.Point(158, 104)
        Me.TxtEntryBy.MaxLength = 10
        Me.TxtEntryBy.Name = "TxtEntryBy"
        Me.TxtEntryBy.Size = New System.Drawing.Size(196, 21)
        Me.TxtEntryBy.TabIndex = 11
        '
        'LblEntryBy
        '
        Me.LblEntryBy.AutoSize = True
        Me.LblEntryBy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEntryBy.ForeColor = System.Drawing.Color.Black
        Me.LblEntryBy.Location = New System.Drawing.Point(77, 106)
        Me.LblEntryBy.Name = "LblEntryBy"
        Me.LblEntryBy.Size = New System.Drawing.Size(59, 14)
        Me.LblEntryBy.TabIndex = 10
        Me.LblEntryBy.Text = "Entry By"
        '
        'TxtRemark
        '
        Me.TxtRemark.AgAllowUserToEnableMasterHelp = False
        Me.TxtRemark.AgMandatory = False
        Me.TxtRemark.AgMasterHelp = True
        Me.TxtRemark.AgNumberLeftPlaces = 0
        Me.TxtRemark.AgNumberNegetiveAllow = False
        Me.TxtRemark.AgNumberRightPlaces = 0
        Me.TxtRemark.AgPickFromLastValue = False
        Me.TxtRemark.AgRowFilter = ""

        Me.TxtRemark.AgSelectedValue = Nothing
        Me.TxtRemark.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemark.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemark.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemark.Location = New System.Drawing.Point(158, 33)
        Me.TxtRemark.MaxLength = 255
        Me.TxtRemark.Multiline = True
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.Size = New System.Drawing.Size(196, 70)
        Me.TxtRemark.TabIndex = 9
        '
        'LblRemark
        '
        Me.LblRemark.AutoSize = True
        Me.LblRemark.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemark.ForeColor = System.Drawing.Color.Black
        Me.LblRemark.Location = New System.Drawing.Point(77, 35)
        Me.LblRemark.Name = "LblRemark"
        Me.LblRemark.Size = New System.Drawing.Size(53, 14)
        Me.LblRemark.TabIndex = 8
        Me.LblRemark.Text = "Remark"
        '
        'BtnOK
        '
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(158, 174)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 24)
        Me.BtnOK.TabIndex = 13
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'TxtReminderDate
        '
        Me.TxtReminderDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtReminderDate.AgMandatory = False
        Me.TxtReminderDate.AgMasterHelp = False
        Me.TxtReminderDate.AgNumberLeftPlaces = 0
        Me.TxtReminderDate.AgNumberNegetiveAllow = False
        Me.TxtReminderDate.AgNumberRightPlaces = 0
        Me.TxtReminderDate.AgPickFromLastValue = False
        Me.TxtReminderDate.AgRowFilter = ""

        Me.TxtReminderDate.AgSelectedValue = Nothing
        Me.TxtReminderDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReminderDate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReminderDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReminderDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReminderDate.Location = New System.Drawing.Point(80, 320)
        Me.TxtReminderDate.MaxLength = 100
        Me.TxtReminderDate.Name = "TxtReminderDate"
        Me.TxtReminderDate.Size = New System.Drawing.Size(35, 18)
        Me.TxtReminderDate.TabIndex = 17
        Me.TxtReminderDate.Visible = False
        '
        'BtnCancel
        '
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(243, 174)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 24)
        Me.BtnCancel.TabIndex = 18
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'FrmUpdateStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 220)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtReminderDate)
        Me.Controls.Add(Me.Topctrl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmUpdateStatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Update Status"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents TxtStatus As AgControls.AgTextBox
    Friend WithEvents LblStatus As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LblRemark As System.Windows.Forms.Label
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents TxtReminderDate As AgControls.AgTextBox
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents TxtRemark As AgControls.AgTextBox
    Friend WithEvents TxtEntryBy As AgControls.AgTextBox
    Friend WithEvents LblEntryBy As System.Windows.Forms.Label
    Friend WithEvents TxtEntryDate As AgControls.AgTextBox
    Friend WithEvents LblEntryDate As System.Windows.Forms.Label
End Class
