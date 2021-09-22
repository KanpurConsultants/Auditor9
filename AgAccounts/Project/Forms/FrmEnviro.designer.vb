<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEnviro
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
        Me.TBMain = New System.Windows.Forms.TabControl
        Me.TBPVoucherEntry = New System.Windows.Forms.TabPage
        Me.Label11 = New System.Windows.Forms.Label
        Me.BtnVoucherEnt = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtTo = New AgControls.AgTextBox
        Me.txtFrom = New AgControls.AgTextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TxtArrange = New AgControls.AgTextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtTDSROff = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtNumberingSystem = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtAutoPosting = New AgControls.AgTextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.TxtMaintainTDS = New AgControls.AgTextBox
        Me.BtnClose = New System.Windows.Forms.Button
        Me.BtnSave = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtSrvTaxAdjRefType = New AgControls.AgTextBox
        Me.TBMain.SuspendLayout()
        Me.TBPVoucherEntry.SuspendLayout()
        Me.SuspendLayout()
        '
        'TBMain
        '
        Me.TBMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TBMain.Controls.Add(Me.TBPVoucherEntry)
        Me.TBMain.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBMain.Location = New System.Drawing.Point(-1, 1)
        Me.TBMain.Name = "TBMain"
        Me.TBMain.SelectedIndex = 0
        Me.TBMain.Size = New System.Drawing.Size(784, 460)
        Me.TBMain.TabIndex = 0
        '
        'TBPVoucherEntry
        '
        Me.TBPVoucherEntry.BackColor = System.Drawing.Color.Silver
        Me.TBPVoucherEntry.Controls.Add(Me.Label12)
        Me.TBPVoucherEntry.Controls.Add(Me.Label13)
        Me.TBPVoucherEntry.Controls.Add(Me.TxtSrvTaxAdjRefType)
        Me.TBPVoucherEntry.Controls.Add(Me.Label11)
        Me.TBPVoucherEntry.Controls.Add(Me.BtnVoucherEnt)
        Me.TBPVoucherEntry.Controls.Add(Me.Label8)
        Me.TBPVoucherEntry.Controls.Add(Me.Label9)
        Me.TBPVoucherEntry.Controls.Add(Me.TxtTo)
        Me.TBPVoucherEntry.Controls.Add(Me.txtFrom)
        Me.TBPVoucherEntry.Controls.Add(Me.Label10)
        Me.TBPVoucherEntry.Controls.Add(Me.TxtArrange)
        Me.TBPVoucherEntry.Controls.Add(Me.GroupBox1)
        Me.TBPVoucherEntry.Controls.Add(Me.Label7)
        Me.TBPVoucherEntry.Controls.Add(Me.Label5)
        Me.TBPVoucherEntry.Controls.Add(Me.Label6)
        Me.TBPVoucherEntry.Controls.Add(Me.TxtTDSROff)
        Me.TBPVoucherEntry.Controls.Add(Me.Label3)
        Me.TBPVoucherEntry.Controls.Add(Me.Label4)
        Me.TBPVoucherEntry.Controls.Add(Me.TxtNumberingSystem)
        Me.TBPVoucherEntry.Controls.Add(Me.Label1)
        Me.TBPVoucherEntry.Controls.Add(Me.Label2)
        Me.TBPVoucherEntry.Controls.Add(Me.TxtAutoPosting)
        Me.TBPVoucherEntry.Controls.Add(Me.Label16)
        Me.TBPVoucherEntry.Controls.Add(Me.Label17)
        Me.TBPVoucherEntry.Controls.Add(Me.TxtMaintainTDS)
        Me.TBPVoucherEntry.Location = New System.Drawing.Point(4, 28)
        Me.TBPVoucherEntry.Name = "TBPVoucherEntry"
        Me.TBPVoucherEntry.Padding = New System.Windows.Forms.Padding(3)
        Me.TBPVoucherEntry.Size = New System.Drawing.Size(776, 428)
        Me.TBPVoucherEntry.TabIndex = 1
        Me.TBPVoucherEntry.Text = "Voucher Entry"
        Me.TBPVoucherEntry.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Maroon
        Me.Label11.Location = New System.Drawing.Point(403, 97)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(244, 14)
        Me.Label11.TabIndex = 88
        Me.Label11.Text = "Note : Bills Once Arranged Will Not Be Undo."
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnVoucherEnt
        '
        Me.BtnVoucherEnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnVoucherEnt.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnVoucherEnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnVoucherEnt.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnVoucherEnt.Location = New System.Drawing.Point(632, 19)
        Me.BtnVoucherEnt.Name = "BtnVoucherEnt"
        Me.BtnVoucherEnt.Size = New System.Drawing.Size(119, 58)
        Me.BtnVoucherEnt.TabIndex = 8
        Me.BtnVoucherEnt.Text = "Arrange Voucher No."
        Me.BtnVoucherEnt.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(403, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(22, 16)
        Me.Label8.TabIndex = 85
        Me.Label8.Text = "To"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(403, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 16)
        Me.Label9.TabIndex = 84
        Me.Label9.Text = "From"
        '
        'TxtTo
        '
        Me.TxtTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtTo.BackColor = System.Drawing.Color.White
        Me.TxtTo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTo.Location = New System.Drawing.Point(473, 59)
        Me.TxtTo.Name = "TxtTo"
        Me.TxtTo.Size = New System.Drawing.Size(115, 18)
        Me.TxtTo.TabIndex = 7
        '
        'txtFrom
        '
        Me.txtFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFrom.BackColor = System.Drawing.Color.White
        Me.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFrom.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(473, 39)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(115, 18)
        Me.txtFrom.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(403, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 16)
        Me.Label10.TabIndex = 83
        Me.Label10.Text = "Arrange"
        '
        'TxtArrange
        '
        Me.TxtArrange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtArrange.BackColor = System.Drawing.Color.White
        Me.TxtArrange.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtArrange.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtArrange.Location = New System.Drawing.Point(473, 19)
        Me.TxtArrange.Name = "TxtArrange"
        Me.TxtArrange.Size = New System.Drawing.Size(115, 18)
        Me.TxtArrange.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(357, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(4, 427)
        Me.GroupBox1.TabIndex = 82
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Tag = ""
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(403, 113)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(282, 70)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "NOTE : Before ReArranging Bills, Make Sure That Software Is Not Running Any Where" & _
            " Else, No Body is Working Currently On The System.For Security Reasons."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(171, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Ä"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(28, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 16)
        Me.Label6.TabIndex = 58
        Me.Label6.Text = "TDS Round Off"
        '
        'TxtTDSROff
        '
        Me.TxtTDSROff.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTDSROff.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTDSROff.Location = New System.Drawing.Point(186, 79)
        Me.TxtTDSROff.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtTDSROff.MaxLength = 15
        Me.TxtTDSROff.Name = "TxtTDSROff"
        Me.TxtTDSROff.Size = New System.Drawing.Size(115, 18)
        Me.TxtTDSROff.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(171, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Ä"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(28, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 16)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Numbering System"
        '
        'TxtNumberingSystem
        '
        Me.TxtNumberingSystem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNumberingSystem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNumberingSystem.Location = New System.Drawing.Point(186, 59)
        Me.TxtNumberingSystem.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtNumberingSystem.MaxLength = 15
        Me.TxtNumberingSystem.Name = "TxtNumberingSystem"
        Me.TxtNumberingSystem.Size = New System.Drawing.Size(115, 18)
        Me.TxtNumberingSystem.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(171, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Ä"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 16)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "Auto Posting"
        '
        'TxtAutoPosting
        '
        Me.TxtAutoPosting.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAutoPosting.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAutoPosting.Location = New System.Drawing.Point(186, 39)
        Me.TxtAutoPosting.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtAutoPosting.MaxLength = 15
        Me.TxtAutoPosting.Name = "TxtAutoPosting"
        Me.TxtAutoPosting.Size = New System.Drawing.Size(115, 18)
        Me.TxtAutoPosting.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(171, 19)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(10, 7)
        Me.Label16.TabIndex = 50
        Me.Label16.Text = "Ä"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(28, 19)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(86, 16)
        Me.Label17.TabIndex = 49
        Me.Label17.Text = "Maintain TDS"
        '
        'TxtMaintainTDS
        '
        Me.TxtMaintainTDS.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMaintainTDS.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMaintainTDS.Location = New System.Drawing.Point(186, 19)
        Me.TxtMaintainTDS.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtMaintainTDS.MaxLength = 15
        Me.TxtMaintainTDS.Name = "TxtMaintainTDS"
        Me.TxtMaintainTDS.Size = New System.Drawing.Size(115, 18)
        Me.TxtMaintainTDS.TabIndex = 0
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.AutoEllipsis = True
        Me.BtnClose.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnClose.Location = New System.Drawing.Point(692, 464)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(84, 24)
        Me.BtnClose.TabIndex = 2
        Me.BtnClose.Text = "Clos&e"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.AutoEllipsis = True
        Me.BtnSave.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnSave.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnSave.Location = New System.Drawing.Point(603, 464)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(84, 24)
        Me.BtnSave.TabIndex = 1
        Me.BtnSave.Text = "&Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(171, 99)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 7)
        Me.Label12.TabIndex = 91
        Me.Label12.Text = "Ä"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(28, 99)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(142, 34)
        Me.Label13.TabIndex = 90
        Me.Label13.Text = "Adj .Ref. Voucher Type (For Service Tax)"
        '
        'TxtSrvTaxAdjRefType
        '
        Me.TxtSrvTaxAdjRefType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSrvTaxAdjRefType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSrvTaxAdjRefType.Location = New System.Drawing.Point(186, 99)
        Me.TxtSrvTaxAdjRefType.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtSrvTaxAdjRefType.MaxLength = 15
        Me.TxtSrvTaxAdjRefType.Name = "TxtSrvTaxAdjRefType"
        Me.TxtSrvTaxAdjRefType.Size = New System.Drawing.Size(115, 18)
        Me.TxtSrvTaxAdjRefType.TabIndex = 4
        '
        'FrmEnviro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 491)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.TBMain)
        Me.Name = "FrmEnviro"
        Me.Text = "Environment Setting"
        Me.TBMain.ResumeLayout(False)
        Me.TBPVoucherEntry.ResumeLayout(False)
        Me.TBPVoucherEntry.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TBMain As System.Windows.Forms.TabControl
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents TBPVoucherEntry As System.Windows.Forms.TabPage
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TxtMaintainTDS As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtAutoPosting As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtNumberingSystem As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtTDSROff As AgControls.AgTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtTo As AgControls.AgTextBox
    Friend WithEvents txtFrom As AgControls.AgTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtArrange As AgControls.AgTextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents BtnVoucherEnt As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtSrvTaxAdjRefType As AgControls.AgTextBox
End Class
