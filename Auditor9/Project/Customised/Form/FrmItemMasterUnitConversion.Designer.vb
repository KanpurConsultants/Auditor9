<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemMasterUnitConversion
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
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtItem = New AgControls.AgTextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblItemNameText = New System.Windows.Forms.Label
        Me.LblItemName = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BtnOk = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(0, 35)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(469, 187)
        Me.Pnl1.TabIndex = 0
        '
        'TxtItem
        '
        Me.TxtItem.AgAllowUserToEnableMasterHelp = False
        Me.TxtItem.AgLastValueTag = Nothing
        Me.TxtItem.AgLastValueText = Nothing
        Me.TxtItem.AgMandatory = False
        Me.TxtItem.AgMasterHelp = False
        Me.TxtItem.AgNumberLeftPlaces = 8
        Me.TxtItem.AgNumberNegetiveAllow = False
        Me.TxtItem.AgNumberRightPlaces = 2
        Me.TxtItem.AgPickFromLastValue = False
        Me.TxtItem.AgRowFilter = ""
        Me.TxtItem.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItem.AgSelectedValue = Nothing
        Me.TxtItem.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItem.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItem.Location = New System.Drawing.Point(714, 18)
        Me.TxtItem.MaxLength = 50
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(135, 18)
        Me.TxtItem.TabIndex = 720
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LblItemNameText)
        Me.Panel1.Controls.Add(Me.LblItemName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(469, 35)
        Me.Panel1.TabIndex = 741
        '
        'LblItemNameText
        '
        Me.LblItemNameText.AutoSize = True
        Me.LblItemNameText.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblItemNameText.Location = New System.Drawing.Point(6, 11)
        Me.LblItemNameText.Name = "LblItemNameText"
        Me.LblItemNameText.Size = New System.Drawing.Size(46, 13)
        Me.LblItemNameText.TabIndex = 737
        Me.LblItemNameText.Text = "Item :"
        '
        'LblItemName
        '
        Me.LblItemName.AutoSize = True
        Me.LblItemName.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblItemName.Location = New System.Drawing.Point(59, 11)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(79, 13)
        Me.LblItemName.TabIndex = 736
        Me.LblItemName.Text = "Item Name"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(0, 221)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(471, 4)
        Me.GroupBox2.TabIndex = 742
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(410, 228)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(54, 23)
        Me.BtnOk.TabIndex = 1
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'FrmItemMasterUnitConversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(469, 254)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtItem)
        Me.Controls.Add(Me.Pnl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmItemMasterUnitConversion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Unit Conversion"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtItem As AgControls.AgTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblItemNameText As System.Windows.Forms.Label
    Public WithEvents LblItemName As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnOk As System.Windows.Forms.Button
End Class
