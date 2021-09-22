<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSchemeQualification
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuFreezeColumns = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuExportToExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalInvoiceAmount = New System.Windows.Forms.Label()
        Me.LblTotalSchemeAmount = New System.Windows.Forms.Label()
        Me.LblTotalInvoiceAmountText = New System.Windows.Forms.Label()
        Me.LblTotalSchemeAmountText = New System.Windows.Forms.Label()
        Me.MnuOptions.SuspendLayout()
        Me.PnlTotals.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(0, 578)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1005, 4)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(0, 0)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(979, 548)
        Me.Pnl1.TabIndex = 10
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuFreezeColumns, Me.MnuExportToExcel})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(159, 48)
        Me.MnuOptions.Text = "Option"
        '
        'MnuFreezeColumns
        '
        Me.MnuFreezeColumns.CheckOnClick = True
        Me.MnuFreezeColumns.Name = "MnuFreezeColumns"
        Me.MnuFreezeColumns.Size = New System.Drawing.Size(158, 22)
        Me.MnuFreezeColumns.Text = "Freeze Columns"
        '
        'MnuExportToExcel
        '
        Me.MnuExportToExcel.Name = "MnuExportToExcel"
        Me.MnuExportToExcel.Size = New System.Drawing.Size(158, 22)
        Me.MnuExportToExcel.Text = "Export To Excel"
        '
        'BtnCancel
        '
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(897, 588)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 699
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnOk
        '
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(818, 588)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 23)
        Me.BtnOk.TabIndex = 698
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'PnlTotals
        '
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalInvoiceAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalSchemeAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalInvoiceAmountText)
        Me.PnlTotals.Controls.Add(Me.LblTotalSchemeAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(0, 548)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 700
        '
        'LblTotalInvoiceAmount
        '
        Me.LblTotalInvoiceAmount.AutoSize = True
        Me.LblTotalInvoiceAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalInvoiceAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalInvoiceAmount.Location = New System.Drawing.Point(550, 3)
        Me.LblTotalInvoiceAmount.Name = "LblTotalInvoiceAmount"
        Me.LblTotalInvoiceAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalInvoiceAmount.TabIndex = 660
        Me.LblTotalInvoiceAmount.Text = "."
        Me.LblTotalInvoiceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalSchemeAmount
        '
        Me.LblTotalSchemeAmount.AutoSize = True
        Me.LblTotalSchemeAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalSchemeAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalSchemeAmount.Location = New System.Drawing.Point(900, 4)
        Me.LblTotalSchemeAmount.Name = "LblTotalSchemeAmount"
        Me.LblTotalSchemeAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalSchemeAmount.TabIndex = 662
        Me.LblTotalSchemeAmount.Text = "."
        Me.LblTotalSchemeAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalInvoiceAmountText
        '
        Me.LblTotalInvoiceAmountText.AutoSize = True
        Me.LblTotalInvoiceAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalInvoiceAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalInvoiceAmountText.Location = New System.Drawing.Point(394, 3)
        Me.LblTotalInvoiceAmountText.Name = "LblTotalInvoiceAmountText"
        Me.LblTotalInvoiceAmountText.Size = New System.Drawing.Size(150, 16)
        Me.LblTotalInvoiceAmountText.TabIndex = 659
        Me.LblTotalInvoiceAmountText.Text = "Total Invoice Amount :"
        '
        'LblTotalSchemeAmountText
        '
        Me.LblTotalSchemeAmountText.AutoSize = True
        Me.LblTotalSchemeAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalSchemeAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalSchemeAmountText.Location = New System.Drawing.Point(737, 3)
        Me.LblTotalSchemeAmountText.Name = "LblTotalSchemeAmountText"
        Me.LblTotalSchemeAmountText.Size = New System.Drawing.Size(156, 16)
        Me.LblTotalSchemeAmountText.TabIndex = 661
        Me.LblTotalSchemeAmountText.Text = "Total Scheme Amount :"
        '
        'FrmSchemeQualification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 615)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "FrmSchemeQualification"
        Me.Text = "Edit Items"
        Me.MnuOptions.ResumeLayout(False)
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuFreezeColumns As ToolStripMenuItem
    Friend WithEvents MnuExportToExcel As ToolStripMenuItem
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnOk As Button
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalInvoiceAmount As Label
    Public WithEvents LblTotalSchemeAmount As Label
    Public WithEvents LblTotalInvoiceAmountText As Label
    Public WithEvents LblTotalSchemeAmountText As Label
End Class
