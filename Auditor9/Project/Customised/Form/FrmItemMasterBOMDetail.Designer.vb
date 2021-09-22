<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemMasterBOMDetail
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.TxtBatchQty = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblUnit = New System.Windows.Forms.Label
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
        Me.Pnl1.Size = New System.Drawing.Size(826, 250)
        Me.Pnl1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TxtBatchQty)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.LblUnit)
        Me.Panel1.Controls.Add(Me.LblItemNameText)
        Me.Panel1.Controls.Add(Me.LblItemName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(824, 35)
        Me.Panel1.TabIndex = 741
        '
        'TxtBatchQty
        '
        Me.TxtBatchQty.AgAllowUserToEnableMasterHelp = False
        Me.TxtBatchQty.AgLastValueTag = Nothing
        Me.TxtBatchQty.AgLastValueText = Nothing
        Me.TxtBatchQty.AgMandatory = False
        Me.TxtBatchQty.AgMasterHelp = False
        Me.TxtBatchQty.AgNumberLeftPlaces = 0
        Me.TxtBatchQty.AgNumberNegetiveAllow = False
        Me.TxtBatchQty.AgNumberRightPlaces = 0
        Me.TxtBatchQty.AgPickFromLastValue = False
        Me.TxtBatchQty.AgRowFilter = ""
        Me.TxtBatchQty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBatchQty.AgSelectedValue = Nothing
        Me.TxtBatchQty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBatchQty.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtBatchQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBatchQty.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBatchQty.Location = New System.Drawing.Point(634, 6)
        Me.TxtBatchQty.MaxLength = 20
        Me.TxtBatchQty.Name = "TxtBatchQty"
        Me.TxtBatchQty.Size = New System.Drawing.Size(94, 21)
        Me.TxtBatchQty.TabIndex = 741
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(551, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 740
        Me.Label1.Text = "Batch Qty :"
        '
        'LblUnit
        '
        Me.LblUnit.AutoSize = True
        Me.LblUnit.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblUnit.Location = New System.Drawing.Point(732, 10)
        Me.LblUnit.Name = "LblUnit"
        Me.LblUnit.Size = New System.Drawing.Size(74, 13)
        Me.LblUnit.TabIndex = 738
        Me.LblUnit.Text = "Unit Name"
        '
        'LblItemNameText
        '
        Me.LblItemNameText.AutoSize = True
        Me.LblItemNameText.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblItemNameText.Location = New System.Drawing.Point(6, 10)
        Me.LblItemNameText.Name = "LblItemNameText"
        Me.LblItemNameText.Size = New System.Drawing.Size(46, 13)
        Me.LblItemNameText.TabIndex = 737
        Me.LblItemNameText.Text = "Item :"
        '
        'LblItemName
        '
        Me.LblItemName.AutoSize = True
        Me.LblItemName.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblItemName.Location = New System.Drawing.Point(59, 10)
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
        Me.GroupBox2.Location = New System.Drawing.Point(0, 291)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(826, 4)
        Me.GroupBox2.TabIndex = 742
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(757, 301)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(54, 23)
        Me.BtnOk.TabIndex = 1
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'FrmItemMasterBOMDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 324)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmItemMasterBOMDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "BOM Detail"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblItemNameText As System.Windows.Forms.Label
    Public WithEvents LblItemName As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Public WithEvents LblUnit As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtBatchQty As AgControls.AgTextBox
End Class
