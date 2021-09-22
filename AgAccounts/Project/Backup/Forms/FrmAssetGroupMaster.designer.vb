<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssetGroupMaster
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
        Me.LblName = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtName = New System.Windows.Forms.ComboBox
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.TxtAcName = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtDepreciation = New AgControls.AgTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.PnlMain = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'LblName
        '
        Me.LblName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(251, 74)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(119, 16)
        Me.LblName.TabIndex = 14
        Me.LblName.Text = "Asset Group Name"
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(406, 74)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(10, 7)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Ä"
        '
        'TxtName
        '
        Me.TxtName.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TxtName.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtName.FormattingEnabled = True
        Me.TxtName.Location = New System.Drawing.Point(422, 74)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(312, 25)
        Me.TxtName.TabIndex = 0
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
        Me.Topctrl1.Size = New System.Drawing.Size(922, 41)
        Me.Topctrl1.TabIndex = 4
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
        'TxtAcName
        '
        Me.TxtAcName.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtAcName.BackColor = System.Drawing.Color.White
        Me.TxtAcName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcName.Location = New System.Drawing.Point(422, 121)
        Me.TxtAcName.MaxLength = 0
        Me.TxtAcName.Name = "TxtAcName"
        Me.TxtAcName.Size = New System.Drawing.Size(312, 18)
        Me.TxtAcName.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(251, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 16)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "A/c Name"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(406, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Ä"
        '
        'TxtManualCode
        '
        Me.TxtManualCode.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtManualCode.BackColor = System.Drawing.Color.White
        Me.TxtManualCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualCode.Location = New System.Drawing.Point(422, 101)
        Me.TxtManualCode.MaxLength = 10
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(145, 18)
        Me.TxtManualCode.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(251, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 16)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Asset Group Code"
        '
        'TxtDepreciation
        '
        Me.TxtDepreciation.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtDepreciation.BackColor = System.Drawing.Color.White
        Me.TxtDepreciation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDepreciation.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDepreciation.Location = New System.Drawing.Point(422, 141)
        Me.TxtDepreciation.MaxLength = 0
        Me.TxtDepreciation.Name = "TxtDepreciation"
        Me.TxtDepreciation.Size = New System.Drawing.Size(90, 18)
        Me.TxtDepreciation.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(251, 141)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 16)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "Depriciation [%]"
        '
        'PnlMain
        '
        Me.PnlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMain.Location = New System.Drawing.Point(35, 193)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(854, 324)
        Me.PnlMain.TabIndex = 38
        '
        'FrmAssetGroupMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(922, 545)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.TxtDepreciation)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtAcName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.Topctrl1)
        Me.KeyPreview = True
        Me.Name = "FrmAssetGroupMaster"
        Me.ShowIcon = False
        Me.Text = "Asset Group Master"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtName As System.Windows.Forms.ComboBox
    Friend WithEvents TxtAcName As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtManualCode As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtDepreciation As AgControls.AgTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
End Class
