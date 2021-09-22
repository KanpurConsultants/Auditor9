<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVoucherEntry_LedgerAdj
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
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BtnOK = New System.Windows.Forms.Button
        Me.PnlMain = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtAmountToBeAdj = New AgControls.AgTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtPendingAmt = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtTotalAdjustment = New AgControls.AgTextBox
        Me.LblDisplay = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnCancel.Location = New System.Drawing.Point(808, 487)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(84, 24)
        Me.BtnCancel.TabIndex = 4
        Me.BtnCancel.Text = "Canc&el"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(4, 472)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(890, 9)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnOK.Location = New System.Drawing.Point(719, 487)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(84, 24)
        Me.BtnOK.TabIndex = 3
        Me.BtnOK.Text = "O&k"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'PnlMain
        '
        Me.PnlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMain.Location = New System.Drawing.Point(12, 88)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(872, 377)
        Me.PnlMain.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 16)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Amount To Be Adjusted"
        '
        'TxtAmountToBeAdj
        '
        Me.TxtAmountToBeAdj.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAmountToBeAdj.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAmountToBeAdj.Location = New System.Drawing.Point(169, 44)
        Me.TxtAmountToBeAdj.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtAmountToBeAdj.MaxLength = 15
        Me.TxtAmountToBeAdj.Name = "TxtAmountToBeAdj"
        Me.TxtAmountToBeAdj.Size = New System.Drawing.Size(135, 18)
        Me.TxtAmountToBeAdj.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(4, 71)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(890, 9)
        Me.GroupBox2.TabIndex = 53
        Me.GroupBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(634, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 16)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "Pending Amount"
        '
        'TxtPendingAmt
        '
        Me.TxtPendingAmt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtPendingAmt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPendingAmt.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPendingAmt.Location = New System.Drawing.Point(749, 44)
        Me.TxtPendingAmt.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtPendingAmt.MaxLength = 15
        Me.TxtPendingAmt.Name = "TxtPendingAmt"
        Me.TxtPendingAmt.Size = New System.Drawing.Size(135, 18)
        Me.TxtPendingAmt.TabIndex = 54
        Me.TxtPendingAmt.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 487)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 16)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "Total Adjustment"
        '
        'TxtTotalAdjustment
        '
        Me.TxtTotalAdjustment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTotalAdjustment.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalAdjustment.Location = New System.Drawing.Point(169, 487)
        Me.TxtTotalAdjustment.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtTotalAdjustment.MaxLength = 15
        Me.TxtTotalAdjustment.Name = "TxtTotalAdjustment"
        Me.TxtTotalAdjustment.Size = New System.Drawing.Size(135, 18)
        Me.TxtTotalAdjustment.TabIndex = 56
        '
        'LblDisplay
        '
        Me.LblDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDisplay.BackColor = System.Drawing.Color.White
        Me.LblDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LblDisplay.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDisplay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDisplay.Location = New System.Drawing.Point(12, 9)
        Me.LblDisplay.Name = "LblDisplay"
        Me.LblDisplay.Size = New System.Drawing.Size(872, 26)
        Me.LblDisplay.TabIndex = 58
        Me.LblDisplay.Text = "For"
        Me.LblDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmVoucherEntry_LedgerAdj
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(897, 515)
        Me.Controls.Add(Me.LblDisplay)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtTotalAdjustment)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtPendingAmt)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtAmountToBeAdj)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmVoucherEntry_LedgerAdj"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bill Wise Adjustment"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtAmountToBeAdj As AgControls.AgTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtPendingAmt As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalAdjustment As AgControls.AgTextBox
    Friend WithEvents LblDisplay As System.Windows.Forms.Label
End Class
