<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVoucherAdjBulk
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.LblStatus = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtAcNature = New AgControls.AgTextBox()
        Me.LblVendor = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(2, 34)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(972, 238)
        Me.Panel1.TabIndex = 11
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(2, 275)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(610, 230)
        Me.Panel2.TabIndex = 12
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(618, 301)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(353, 179)
        Me.Panel3.TabIndex = 12
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(618, 275)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(353, 24)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Adjust on FIFO basis"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(618, 482)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(177, 23)
        Me.BtnSave.TabIndex = 14
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(801, 482)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(170, 23)
        Me.BtnCancel.TabIndex = 15
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'LblStatus
        '
        Me.LblStatus.AutoSize = True
        Me.LblStatus.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStatus.Location = New System.Drawing.Point(12, 492)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(16, 16)
        Me.LblStatus.TabIndex = 16
        Me.LblStatus.Text = "."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(123, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 697
        Me.Label4.Text = "Ä"
        '
        'TxtAcNature
        '
        Me.TxtAcNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtAcNature.AgLastValueTag = Nothing
        Me.TxtAcNature.AgLastValueText = Nothing
        Me.TxtAcNature.AgMandatory = True
        Me.TxtAcNature.AgMasterHelp = False
        Me.TxtAcNature.AgNumberLeftPlaces = 8
        Me.TxtAcNature.AgNumberNegetiveAllow = False
        Me.TxtAcNature.AgNumberRightPlaces = 2
        Me.TxtAcNature.AgPickFromLastValue = False
        Me.TxtAcNature.AgRowFilter = ""
        Me.TxtAcNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcNature.AgSelectedValue = Nothing
        Me.TxtAcNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcNature.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcNature.Location = New System.Drawing.Point(139, 12)
        Me.TxtAcNature.MaxLength = 0
        Me.TxtAcNature.Name = "TxtAcNature"
        Me.TxtAcNature.Size = New System.Drawing.Size(243, 16)
        Me.TxtAcNature.TabIndex = 695
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(21, 12)
        Me.LblVendor.Name = "LblVendor"
        Me.LblVendor.Size = New System.Drawing.Size(80, 14)
        Me.LblVendor.TabIndex = 696
        Me.LblVendor.Text = "A/c Nature"
        '
        'FrmVoucherAdjBulk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 513)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtAcNature)
        Me.Controls.Add(Me.LblVendor)
        Me.Controls.Add(Me.LblStatus)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "FrmVoucherAdjBulk"
        Me.Text = "FrmSaleInvoiceAdj"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents Panel2 As System.Windows.Forms.Panel
    Public WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents LblStatus As System.Windows.Forms.Label
    Public WithEvents Label4 As Label
    Public WithEvents TxtAcNature As AgControls.AgTextBox
    Public WithEvents LblVendor As Label
End Class
