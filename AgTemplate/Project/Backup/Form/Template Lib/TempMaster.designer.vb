<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TempMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TempMaster))
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GrpUP = New System.Windows.Forms.GroupBox
        Me.TxtEntryBy = New System.Windows.Forms.TextBox
        Me.GBoxEntryType = New System.Windows.Forms.GroupBox
        Me.TxtEntryType = New System.Windows.Forms.TextBox
        Me.GBoxMoveToLog = New System.Windows.Forms.GroupBox
        Me.TxtMoveToLog = New System.Windows.Forms.TextBox
        Me.CmdMoveToLog = New System.Windows.Forms.Button
        Me.GBoxApprove = New System.Windows.Forms.GroupBox
        Me.TxtApproveBy = New System.Windows.Forms.TextBox
        Me.CmdDiscard = New System.Windows.Forms.Button
        Me.CmdApprove = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TxtStatus = New AgControls.AgTextBox
        Me.CmdStatus = New System.Windows.Forms.Button
        Me.GBoxDivision = New System.Windows.Forms.GroupBox
        Me.TxtDivision = New AgControls.AgTextBox
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
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
        Me.Topctrl1.Size = New System.Drawing.Size(857, 41)
        Me.Topctrl1.TabIndex = 0
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
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(0, 421)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(884, 4)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'GrpUP
        '
        Me.GrpUP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpUP.BackColor = System.Drawing.Color.Transparent
        Me.GrpUP.Controls.Add(Me.TxtEntryBy)
        Me.GrpUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpUP.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpUP.ForeColor = System.Drawing.Color.Maroon
        Me.GrpUP.Location = New System.Drawing.Point(9, 425)
        Me.GrpUP.Name = "GrpUP"
        Me.GrpUP.Size = New System.Drawing.Size(121, 44)
        Me.GrpUP.TabIndex = 639
        Me.GrpUP.TabStop = False
        Me.GrpUP.Tag = "TR"
        Me.GrpUP.Text = "Entry By "
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.BackColor = System.Drawing.Color.White
        Me.TxtEntryBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtEntryBy.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtEntryBy.Enabled = False
        Me.TxtEntryBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntryBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtEntryBy.Name = "TxtEntryBy"
        Me.TxtEntryBy.ReadOnly = True
        Me.TxtEntryBy.Size = New System.Drawing.Size(115, 18)
        Me.TxtEntryBy.TabIndex = 0
        Me.TxtEntryBy.TabStop = False
        Me.TxtEntryBy.Text = "AAAAAAAAAA"
        Me.TxtEntryBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxEntryType.BackColor = System.Drawing.Color.Transparent
        Me.GBoxEntryType.Controls.Add(Me.TxtEntryType)
        Me.GBoxEntryType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxEntryType.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxEntryType.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxEntryType.Location = New System.Drawing.Point(143, 425)
        Me.GBoxEntryType.Name = "GBoxEntryType"
        Me.GBoxEntryType.Size = New System.Drawing.Size(114, 44)
        Me.GBoxEntryType.TabIndex = 659
        Me.GBoxEntryType.TabStop = False
        Me.GBoxEntryType.Tag = "TR"
        Me.GBoxEntryType.Text = "User Action"
        '
        'TxtEntryType
        '
        Me.TxtEntryType.BackColor = System.Drawing.Color.White
        Me.TxtEntryType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtEntryType.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtEntryType.Enabled = False
        Me.TxtEntryType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 23)
        Me.TxtEntryType.Name = "TxtEntryType"
        Me.TxtEntryType.ReadOnly = True
        Me.TxtEntryType.Size = New System.Drawing.Size(108, 18)
        Me.TxtEntryType.TabIndex = 0
        Me.TxtEntryType.TabStop = False
        Me.TxtEntryType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxMoveToLog.BackColor = System.Drawing.Color.Transparent
        Me.GBoxMoveToLog.Controls.Add(Me.TxtMoveToLog)
        Me.GBoxMoveToLog.Controls.Add(Me.CmdMoveToLog)
        Me.GBoxMoveToLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxMoveToLog.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxMoveToLog.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(551, 425)
        Me.GBoxMoveToLog.Name = "GBoxMoveToLog"
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(139, 44)
        Me.GBoxMoveToLog.TabIndex = 647
        Me.GBoxMoveToLog.TabStop = False
        Me.GBoxMoveToLog.Tag = "UP"
        Me.GBoxMoveToLog.Text = "Move to Log"
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.BackColor = System.Drawing.Color.White
        Me.TxtMoveToLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMoveToLog.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtMoveToLog.Enabled = False
        Me.TxtMoveToLog.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMoveToLog.Location = New System.Drawing.Point(29, 23)
        Me.TxtMoveToLog.Name = "TxtMoveToLog"
        Me.TxtMoveToLog.ReadOnly = True
        Me.TxtMoveToLog.Size = New System.Drawing.Size(107, 18)
        Me.TxtMoveToLog.TabIndex = 670
        Me.TxtMoveToLog.TabStop = False
        Me.TxtMoveToLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CmdMoveToLog
        '
        Me.CmdMoveToLog.Dock = System.Windows.Forms.DockStyle.Left
        Me.CmdMoveToLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdMoveToLog.Image = Global.AgTemplate.My.Resources.Resources.disc1
        Me.CmdMoveToLog.Location = New System.Drawing.Point(3, 18)
        Me.CmdMoveToLog.Name = "CmdMoveToLog"
        Me.CmdMoveToLog.Size = New System.Drawing.Size(26, 23)
        Me.CmdMoveToLog.TabIndex = 669
        Me.CmdMoveToLog.TabStop = False
        Me.CmdMoveToLog.UseVisualStyleBackColor = True
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxApprove.BackColor = System.Drawing.Color.Transparent
        Me.GBoxApprove.Controls.Add(Me.TxtApproveBy)
        Me.GBoxApprove.Controls.Add(Me.CmdDiscard)
        Me.GBoxApprove.Controls.Add(Me.CmdApprove)
        Me.GBoxApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxApprove.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxApprove.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxApprove.Location = New System.Drawing.Point(397, 425)
        Me.GBoxApprove.Name = "GBoxApprove"
        Me.GBoxApprove.Size = New System.Drawing.Size(142, 44)
        Me.GBoxApprove.TabIndex = 646
        Me.GBoxApprove.TabStop = False
        Me.GBoxApprove.Tag = "UP"
        Me.GBoxApprove.Text = "Approve/Discard"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.BackColor = System.Drawing.Color.White
        Me.TxtApproveBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtApproveBy.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtApproveBy.Enabled = False
        Me.TxtApproveBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 23)
        Me.TxtApproveBy.Name = "TxtApproveBy"
        Me.TxtApproveBy.ReadOnly = True
        Me.TxtApproveBy.Size = New System.Drawing.Size(84, 18)
        Me.TxtApproveBy.TabIndex = 644
        Me.TxtApproveBy.TabStop = False
        Me.TxtApproveBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Dock = System.Windows.Forms.DockStyle.Right
        Me.CmdDiscard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdDiscard.Image = Global.AgTemplate.My.Resources.Resources.Copy_of_stock_vector_vector_ok_and_cancel_button_set_54696784
        Me.CmdDiscard.Location = New System.Drawing.Point(113, 18)
        Me.CmdDiscard.Name = "CmdDiscard"
        Me.CmdDiscard.Size = New System.Drawing.Size(26, 23)
        Me.CmdDiscard.TabIndex = 669
        Me.CmdDiscard.TabStop = False
        Me.CmdDiscard.UseVisualStyleBackColor = True
        '
        'CmdApprove
        '
        Me.CmdApprove.Dock = System.Windows.Forms.DockStyle.Left
        Me.CmdApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdApprove.Image = Global.AgTemplate.My.Resources.Resources.stock_vector_vector_ok_and_cancel_button_set_54696784
        Me.CmdApprove.Location = New System.Drawing.Point(3, 18)
        Me.CmdApprove.Name = "CmdApprove"
        Me.CmdApprove.Size = New System.Drawing.Size(26, 23)
        Me.CmdApprove.TabIndex = 668
        Me.CmdApprove.TabStop = False
        Me.CmdApprove.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.TxtStatus)
        Me.GroupBox2.Controls.Add(Me.CmdStatus)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox2.Location = New System.Drawing.Point(697, 425)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(148, 44)
        Me.GroupBox2.TabIndex = 671
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = "UP"
        Me.GroupBox2.Text = "Status"
        '
        'TxtStatus
        '
        Me.TxtStatus.AgMandatory = False
        Me.TxtStatus.AgMasterHelp = False
        Me.TxtStatus.AgNumberLeftPlaces = 0
        Me.TxtStatus.AgNumberNegetiveAllow = False
        Me.TxtStatus.AgNumberRightPlaces = 0
        Me.TxtStatus.AgPickFromLastValue = False
        Me.TxtStatus.AgRowFilter = ""
        Me.TxtStatus.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtStatus.AgSelectedValue = Nothing
        Me.TxtStatus.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStatus.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStatus.BackColor = System.Drawing.Color.White
        Me.TxtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtStatus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStatus.Location = New System.Drawing.Point(29, 23)
        Me.TxtStatus.Name = "TxtStatus"
        Me.TxtStatus.Size = New System.Drawing.Size(116, 18)
        Me.TxtStatus.TabIndex = 670
        Me.TxtStatus.TabStop = False
        Me.TxtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CmdStatus
        '
        Me.CmdStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.CmdStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdStatus.Image = CType(resources.GetObject("CmdStatus.Image"), System.Drawing.Image)
        Me.CmdStatus.Location = New System.Drawing.Point(3, 18)
        Me.CmdStatus.Name = "CmdStatus"
        Me.CmdStatus.Size = New System.Drawing.Size(26, 23)
        Me.CmdStatus.TabIndex = 669
        Me.CmdStatus.TabStop = False
        Me.CmdStatus.UseVisualStyleBackColor = True
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxDivision.BackColor = System.Drawing.Color.Transparent
        Me.GBoxDivision.Controls.Add(Me.TxtDivision)
        Me.GBoxDivision.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxDivision.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxDivision.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxDivision.Location = New System.Drawing.Point(271, 425)
        Me.GBoxDivision.Name = "GBoxDivision"
        Me.GBoxDivision.Size = New System.Drawing.Size(114, 44)
        Me.GBoxDivision.TabIndex = 660
        Me.GBoxDivision.TabStop = False
        Me.GBoxDivision.Tag = "TR"
        Me.GBoxDivision.Text = "Division"
        '
        'TxtDivision
        '
        Me.TxtDivision.AgMandatory = False
        Me.TxtDivision.AgMasterHelp = False
        Me.TxtDivision.AgNumberLeftPlaces = 0
        Me.TxtDivision.AgNumberNegetiveAllow = False
        Me.TxtDivision.AgNumberRightPlaces = 0
        Me.TxtDivision.AgPickFromLastValue = False
        Me.TxtDivision.AgRowFilter = ""
        Me.TxtDivision.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDivision.AgSelectedValue = Nothing
        Me.TxtDivision.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDivision.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDivision.BackColor = System.Drawing.Color.White
        Me.TxtDivision.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDivision.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtDivision.Enabled = False
        Me.TxtDivision.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDivision.Location = New System.Drawing.Point(3, 23)
        Me.TxtDivision.Name = "TxtDivision"
        Me.TxtDivision.ReadOnly = True
        Me.TxtDivision.Size = New System.Drawing.Size(108, 18)
        Me.TxtDivision.TabIndex = 0
        Me.TxtDivision.TabStop = False
        Me.TxtDivision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TempMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 469)
        Me.Controls.Add(Me.GBoxDivision)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GBoxMoveToLog)
        Me.Controls.Add(Me.GBoxApprove)
        Me.Controls.Add(Me.GBoxEntryType)
        Me.Controls.Add(Me.GrpUP)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Topctrl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "TempMaster"
        Me.Text = "Fee Group Master"
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Topctrl1 As Topctrl.Topctrl
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents GrpUP As System.Windows.Forms.GroupBox
    Public WithEvents TxtEntryBy As System.Windows.Forms.TextBox
    Public WithEvents GBoxEntryType As System.Windows.Forms.GroupBox
    Public WithEvents TxtEntryType As System.Windows.Forms.TextBox
    Public WithEvents GBoxMoveToLog As System.Windows.Forms.GroupBox
    Public WithEvents TxtMoveToLog As System.Windows.Forms.TextBox
    Public WithEvents CmdMoveToLog As System.Windows.Forms.Button
    Public WithEvents GBoxApprove As System.Windows.Forms.GroupBox
    Public WithEvents TxtApproveBy As System.Windows.Forms.TextBox
    Public WithEvents CmdDiscard As System.Windows.Forms.Button
    Public WithEvents CmdApprove As System.Windows.Forms.Button
    Public WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents CmdStatus As System.Windows.Forms.Button
    Public WithEvents GBoxDivision As System.Windows.Forms.GroupBox
    Public WithEvents TxtDivision As AgControls.AgTextBox
    Public WithEvents TxtStatus As AgControls.AgTextBox
End Class
