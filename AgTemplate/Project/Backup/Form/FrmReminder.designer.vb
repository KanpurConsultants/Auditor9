<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReminder
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
        Me.TxtId = New AgControls.AgTextBox
        Me.LblID = New System.Windows.Forms.Label
        Me.TxtDate = New AgControls.AgTextBox
        Me.LblDate = New System.Windows.Forms.Label
        Me.TxtNarration = New AgControls.AgTextBox
        Me.LblNarration = New System.Windows.Forms.Label
        Me.TxtTime = New AgControls.AgTextBox
        Me.TxtReminderTime = New AgControls.AgTextBox
        Me.TxtReminderDate = New AgControls.AgTextBox
        Me.LblReminderDate = New System.Windows.Forms.Label
        Me.TxtEntryBy = New AgControls.AgTextBox
        Me.LblRemindBy = New System.Windows.Forms.Label
        Me.LblRemindTo = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
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
        Me.Topctrl1.Size = New System.Drawing.Size(892, 41)
        Me.Topctrl1.TabIndex = 7
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
        'TxtId
        '
        Me.TxtId.AgMandatory = True
        Me.TxtId.AgMasterHelp = True
        Me.TxtId.AgNumberLeftPlaces = 0
        Me.TxtId.AgNumberNegetiveAllow = False
        Me.TxtId.AgNumberRightPlaces = 0
        Me.TxtId.AgPickFromLastValue = False
        Me.TxtId.AgRowFilter = ""
        Me.TxtId.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtId.AgSelectedValue = Nothing
        Me.TxtId.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtId.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtId.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtId.Location = New System.Drawing.Point(52, 56)
        Me.TxtId.MaxLength = 10
        Me.TxtId.Name = "TxtId"
        Me.TxtId.Size = New System.Drawing.Size(100, 21)
        Me.TxtId.TabIndex = 0
        '
        'LblID
        '
        Me.LblID.AutoSize = True
        Me.LblID.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblID.Location = New System.Drawing.Point(25, 59)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(21, 13)
        Me.LblID.TabIndex = 0
        Me.LblID.Text = "ID"
        '
        'TxtDate
        '
        Me.TxtDate.AgMandatory = True
        Me.TxtDate.AgMasterHelp = True
        Me.TxtDate.AgNumberLeftPlaces = 0
        Me.TxtDate.AgNumberNegetiveAllow = False
        Me.TxtDate.AgNumberRightPlaces = 0
        Me.TxtDate.AgPickFromLastValue = False
        Me.TxtDate.AgRowFilter = ""
        Me.TxtDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDate.AgSelectedValue = Nothing
        Me.TxtDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate.Location = New System.Drawing.Point(356, 79)
        Me.TxtDate.MaxLength = 15
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(98, 18)
        Me.TxtDate.TabIndex = 1
        '
        'LblDate
        '
        Me.LblDate.AutoSize = True
        Me.LblDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate.Location = New System.Drawing.Point(212, 82)
        Me.LblDate.Name = "LblDate"
        Me.LblDate.Size = New System.Drawing.Size(78, 13)
        Me.LblDate.TabIndex = 0
        Me.LblDate.Text = "Date && Time"
        '
        'TxtNarration
        '
        Me.TxtNarration.AgMandatory = True
        Me.TxtNarration.AgMasterHelp = True
        Me.TxtNarration.AgNumberLeftPlaces = 0
        Me.TxtNarration.AgNumberNegetiveAllow = False
        Me.TxtNarration.AgNumberRightPlaces = 0
        Me.TxtNarration.AgPickFromLastValue = False
        Me.TxtNarration.AgRowFilter = ""
        Me.TxtNarration.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNarration.AgSelectedValue = Nothing
        Me.TxtNarration.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNarration.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNarration.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNarration.Location = New System.Drawing.Point(356, 119)
        Me.TxtNarration.MaxLength = 0
        Me.TxtNarration.Multiline = True
        Me.TxtNarration.Name = "TxtNarration"
        Me.TxtNarration.Size = New System.Drawing.Size(343, 97)
        Me.TxtNarration.TabIndex = 5
        '
        'LblNarration
        '
        Me.LblNarration.AutoSize = True
        Me.LblNarration.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNarration.Location = New System.Drawing.Point(212, 122)
        Me.LblNarration.Name = "LblNarration"
        Me.LblNarration.Size = New System.Drawing.Size(60, 13)
        Me.LblNarration.TabIndex = 5
        Me.LblNarration.Text = "Narration"
        '
        'TxtTime
        '
        Me.TxtTime.AgMandatory = False
        Me.TxtTime.AgMasterHelp = True
        Me.TxtTime.AgNumberLeftPlaces = 4
        Me.TxtTime.AgNumberNegetiveAllow = False
        Me.TxtTime.AgNumberRightPlaces = 3
        Me.TxtTime.AgPickFromLastValue = False
        Me.TxtTime.AgRowFilter = ""
        Me.TxtTime.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTime.AgSelectedValue = Nothing
        Me.TxtTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTime.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTime.Location = New System.Drawing.Point(457, 79)
        Me.TxtTime.MaxLength = 5
        Me.TxtTime.Name = "TxtTime"
        Me.TxtTime.Size = New System.Drawing.Size(63, 18)
        Me.TxtTime.TabIndex = 2
        Me.TxtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtReminderTime
        '
        Me.TxtReminderTime.AgMandatory = False
        Me.TxtReminderTime.AgMasterHelp = True
        Me.TxtReminderTime.AgNumberLeftPlaces = 4
        Me.TxtReminderTime.AgNumberNegetiveAllow = False
        Me.TxtReminderTime.AgNumberRightPlaces = 3
        Me.TxtReminderTime.AgPickFromLastValue = False
        Me.TxtReminderTime.AgRowFilter = ""
        Me.TxtReminderTime.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReminderTime.AgSelectedValue = Nothing
        Me.TxtReminderTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReminderTime.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReminderTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReminderTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReminderTime.Location = New System.Drawing.Point(457, 99)
        Me.TxtReminderTime.MaxLength = 5
        Me.TxtReminderTime.Name = "TxtReminderTime"
        Me.TxtReminderTime.Size = New System.Drawing.Size(63, 18)
        Me.TxtReminderTime.TabIndex = 4
        Me.TxtReminderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtReminderDate
        '
        Me.TxtReminderDate.AgMandatory = True
        Me.TxtReminderDate.AgMasterHelp = True
        Me.TxtReminderDate.AgNumberLeftPlaces = 0
        Me.TxtReminderDate.AgNumberNegetiveAllow = False
        Me.TxtReminderDate.AgNumberRightPlaces = 0
        Me.TxtReminderDate.AgPickFromLastValue = False
        Me.TxtReminderDate.AgRowFilter = ""
        Me.TxtReminderDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReminderDate.AgSelectedValue = Nothing
        Me.TxtReminderDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReminderDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtReminderDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReminderDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReminderDate.Location = New System.Drawing.Point(356, 99)
        Me.TxtReminderDate.MaxLength = 15
        Me.TxtReminderDate.Name = "TxtReminderDate"
        Me.TxtReminderDate.Size = New System.Drawing.Size(98, 18)
        Me.TxtReminderDate.TabIndex = 3
        '
        'LblReminderDate
        '
        Me.LblReminderDate.AutoSize = True
        Me.LblReminderDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReminderDate.Location = New System.Drawing.Point(212, 102)
        Me.LblReminderDate.Name = "LblReminderDate"
        Me.LblReminderDate.Size = New System.Drawing.Size(137, 13)
        Me.LblReminderDate.TabIndex = 7
        Me.LblReminderDate.Text = "Reminder Date && Time"
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.AgMandatory = True
        Me.TxtEntryBy.AgMasterHelp = True
        Me.TxtEntryBy.AgNumberLeftPlaces = 0
        Me.TxtEntryBy.AgNumberNegetiveAllow = False
        Me.TxtEntryBy.AgNumberRightPlaces = 0
        Me.TxtEntryBy.AgPickFromLastValue = False
        Me.TxtEntryBy.AgRowFilter = ""
        Me.TxtEntryBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtEntryBy.AgSelectedValue = Nothing
        Me.TxtEntryBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEntryBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEntryBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtEntryBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntryBy.Location = New System.Drawing.Point(356, 59)
        Me.TxtEntryBy.MaxLength = 100
        Me.TxtEntryBy.Name = "TxtEntryBy"
        Me.TxtEntryBy.Size = New System.Drawing.Size(164, 18)
        Me.TxtEntryBy.TabIndex = 0
        '
        'LblRemindBy
        '
        Me.LblRemindBy.AutoSize = True
        Me.LblRemindBy.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemindBy.Location = New System.Drawing.Point(212, 62)
        Me.LblRemindBy.Name = "LblRemindBy"
        Me.LblRemindBy.Size = New System.Drawing.Size(69, 13)
        Me.LblRemindBy.TabIndex = 10
        Me.LblRemindBy.Text = "Remind By"
        '
        'LblRemindTo
        '
        Me.LblRemindTo.AutoSize = True
        Me.LblRemindTo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemindTo.Location = New System.Drawing.Point(212, 222)
        Me.LblRemindTo.Name = "LblRemindTo"
        Me.LblRemindTo.Size = New System.Drawing.Size(68, 13)
        Me.LblRemindTo.TabIndex = 12
        Me.LblRemindTo.Text = "Remind To"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(356, 222)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(343, 147)
        Me.Pnl1.TabIndex = 6
        '
        'FrmReminder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 416)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LblRemindTo)
        Me.Controls.Add(Me.TxtEntryBy)
        Me.Controls.Add(Me.LblRemindBy)
        Me.Controls.Add(Me.TxtReminderTime)
        Me.Controls.Add(Me.TxtReminderDate)
        Me.Controls.Add(Me.LblReminderDate)
        Me.Controls.Add(Me.TxtNarration)
        Me.Controls.Add(Me.TxtTime)
        Me.Controls.Add(Me.LblNarration)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.TxtId)
        Me.Controls.Add(Me.LblID)
        Me.Controls.Add(Me.TxtDate)
        Me.Controls.Add(Me.LblDate)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmReminder"
        Me.Text = "Reminder"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents TxtId As AgControls.AgTextBox
    Friend WithEvents LblID As System.Windows.Forms.Label
    Friend WithEvents TxtDate As AgControls.AgTextBox
    Friend WithEvents LblDate As System.Windows.Forms.Label
    Friend WithEvents TxtNarration As AgControls.AgTextBox
    Friend WithEvents LblNarration As System.Windows.Forms.Label
    Friend WithEvents TxtTime As AgControls.AgTextBox
    Friend WithEvents TxtReminderTime As AgControls.AgTextBox
    Friend WithEvents TxtReminderDate As AgControls.AgTextBox
    Friend WithEvents LblReminderDate As System.Windows.Forms.Label
    Friend WithEvents TxtEntryBy As AgControls.AgTextBox
    Friend WithEvents LblRemindBy As System.Windows.Forms.Label
    Friend WithEvents LblRemindTo As System.Windows.Forms.Label
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
End Class
