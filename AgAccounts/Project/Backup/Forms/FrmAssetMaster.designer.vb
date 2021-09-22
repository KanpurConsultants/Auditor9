<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssetMaster
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
        Me.TxtAssetGroup = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtID = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'LblName
        '
        Me.LblName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(203, 134)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(80, 16)
        Me.LblName.TabIndex = 14
        Me.LblName.Text = "Asset Name"
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(353, 134)
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
        Me.TxtName.Location = New System.Drawing.Point(369, 134)
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
        Me.Topctrl1.Size = New System.Drawing.Size(883, 41)
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
        'TxtAssetGroup
        '
        Me.TxtAssetGroup.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtAssetGroup.BackColor = System.Drawing.Color.White
        Me.TxtAssetGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAssetGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAssetGroup.Location = New System.Drawing.Point(369, 181)
        Me.TxtAssetGroup.MaxLength = 0
        Me.TxtAssetGroup.Name = "TxtAssetGroup"
        Me.TxtAssetGroup.Size = New System.Drawing.Size(312, 18)
        Me.TxtAssetGroup.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(203, 181)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 16)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Asset Group"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(353, 161)
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
        Me.TxtManualCode.Location = New System.Drawing.Point(369, 161)
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
        Me.Label6.Location = New System.Drawing.Point(203, 161)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 16)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Asset ID"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(353, 181)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Ä"
        '
        'TxtID
        '
        Me.TxtID.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TxtID.BackColor = System.Drawing.Color.White
        Me.TxtID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtID.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtID.Location = New System.Drawing.Point(391, 47)
        Me.TxtID.MaxLength = 10
        Me.TxtID.Name = "TxtID"
        Me.TxtID.Size = New System.Drawing.Size(145, 18)
        Me.TxtID.TabIndex = 37
        Me.TxtID.Visible = False
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(169, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 16)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Asset System ID"
        Me.Label3.Visible = False
        '
        'FrmAssetMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(883, 319)
        Me.Controls.Add(Me.TxtID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtAssetGroup)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.Topctrl1)
        Me.KeyPreview = True
        Me.Name = "FrmAssetMaster"
        Me.ShowIcon = False
        Me.Text = "Asset Master"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtName As System.Windows.Forms.ComboBox
    Friend WithEvents TxtAssetGroup As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtManualCode As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtID As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
