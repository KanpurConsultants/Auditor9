<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBarcodeHistory
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
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtBarcode = New AgControls.AgTextBox()
        Me.LblBarcode = New System.Windows.Forms.Label()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalPcs = New System.Windows.Forms.Label()
        Me.LblTotalPcsText = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalAmount = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalAmountText = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.PnlTotals.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl2.Location = New System.Drawing.Point(0, 257)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(974, 168)
        Me.Pnl2.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TxtBarcode)
        Me.Panel1.Controls.Add(Me.LblBarcode)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(974, 36)
        Me.Panel1.TabIndex = 671
        '
        'TxtBarcode
        '
        Me.TxtBarcode.AgAllowUserToEnableMasterHelp = False
        Me.TxtBarcode.AgLastValueTag = Nothing
        Me.TxtBarcode.AgLastValueText = Nothing
        Me.TxtBarcode.AgMandatory = False
        Me.TxtBarcode.AgMasterHelp = False
        Me.TxtBarcode.AgNumberLeftPlaces = 8
        Me.TxtBarcode.AgNumberNegetiveAllow = False
        Me.TxtBarcode.AgNumberRightPlaces = 2
        Me.TxtBarcode.AgPickFromLastValue = False
        Me.TxtBarcode.AgRowFilter = ""
        Me.TxtBarcode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBarcode.AgSelectedValue = Nothing
        Me.TxtBarcode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBarcode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBarcode.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBarcode.Location = New System.Drawing.Point(103, 6)
        Me.TxtBarcode.MaxLength = 20
        Me.TxtBarcode.Name = "TxtBarcode"
        Me.TxtBarcode.Size = New System.Drawing.Size(141, 26)
        Me.TxtBarcode.TabIndex = 12
        '
        'LblBarcode
        '
        Me.LblBarcode.AutoSize = True
        Me.LblBarcode.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBarcode.Location = New System.Drawing.Point(16, 11)
        Me.LblBarcode.Name = "LblBarcode"
        Me.LblBarcode.Size = New System.Drawing.Size(81, 18)
        Me.LblBarcode.TabIndex = 11
        Me.LblBarcode.Text = "Barcode"
        '
        'PnlTotals
        '
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalPcs)
        Me.PnlTotals.Controls.Add(Me.LblTotalPcsText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmount)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountText)
        Me.PnlTotals.Location = New System.Drawing.Point(0, 425)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 695
        '
        'LblTotalPcs
        '
        Me.LblTotalPcs.AutoSize = True
        Me.LblTotalPcs.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPcs.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalPcs.Location = New System.Drawing.Point(99, 3)
        Me.LblTotalPcs.Name = "LblTotalPcs"
        Me.LblTotalPcs.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalPcs.TabIndex = 664
        Me.LblTotalPcs.Text = "."
        Me.LblTotalPcs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalPcsText
        '
        Me.LblTotalPcsText.AutoSize = True
        Me.LblTotalPcsText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPcsText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalPcsText.Location = New System.Drawing.Point(14, 3)
        Me.LblTotalPcsText.Name = "LblTotalPcsText"
        Me.LblTotalPcsText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalPcsText.TabIndex = 663
        Me.LblTotalPcsText.Text = "Total Pcs :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(502, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(900, 4)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(417, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(796, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(0, 37)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(974, 214)
        Me.Pnl1.TabIndex = 696
        '
        'FrmBarcodeHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 448)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "FrmBarcodeHistory"
        Me.Text = "Sale Invoice Reconcillation"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Pnl2 As System.Windows.Forms.Panel
    Public WithEvents Panel1 As Panel
    Public WithEvents LblBarcode As Label
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalQty As Label
    Public WithEvents LblTotalAmount As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LblTotalAmountText As Label
    Public WithEvents LblTotalPcs As Label
    Public WithEvents LblTotalPcsText As Label
    Public WithEvents TxtBarcode As AgControls.AgTextBox
    Friend WithEvents Pnl1 As Panel
End Class
