<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPurchaseInvoiceDimension_WithDimension
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblTotalDealQty = New System.Windows.Forms.Label()
        Me.LblTotalDealQtyText = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalPcs = New System.Windows.Forms.Label()
        Me.LblTotalPcsText = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(0, 0)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(633, 245)
        Me.Pnl1.TabIndex = 743
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(567, 3)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(54, 23)
        Me.BtnOk.TabIndex = 744
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LblTotalPcs)
        Me.Panel1.Controls.Add(Me.LblTotalPcsText)
        Me.Panel1.Controls.Add(Me.LblTotalDealQty)
        Me.Panel1.Controls.Add(Me.LblTotalDealQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Controls.Add(Me.BtnOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 249)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(633, 28)
        Me.Panel1.TabIndex = 744
        '
        'LblTotalDealQty
        '
        Me.LblTotalDealQty.AutoSize = True
        Me.LblTotalDealQty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDealQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalDealQty.Location = New System.Drawing.Point(488, 6)
        Me.LblTotalDealQty.Name = "LblTotalDealQty"
        Me.LblTotalDealQty.Size = New System.Drawing.Size(13, 16)
        Me.LblTotalDealQty.TabIndex = 748
        Me.LblTotalDealQty.Text = "."
        Me.LblTotalDealQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalDealQtyText
        '
        Me.LblTotalDealQtyText.AutoSize = True
        Me.LblTotalDealQtyText.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDealQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDealQtyText.Location = New System.Drawing.Point(375, 6)
        Me.LblTotalDealQtyText.Name = "LblTotalDealQtyText"
        Me.LblTotalDealQtyText.Size = New System.Drawing.Size(111, 14)
        Me.LblTotalDealQtyText.TabIndex = 747
        Me.LblTotalDealQtyText.Text = "Total Deal Qty :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(110, 6)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(13, 16)
        Me.LblTotalQty.TabIndex = 746
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(25, 6)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(77, 14)
        Me.LblTotalQtyText.TabIndex = 745
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LblTotalPcs
        '
        Me.LblTotalPcs.AutoSize = True
        Me.LblTotalPcs.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPcs.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalPcs.Location = New System.Drawing.Point(290, 6)
        Me.LblTotalPcs.Name = "LblTotalPcs"
        Me.LblTotalPcs.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalPcs.TabIndex = 750
        Me.LblTotalPcs.Text = "."
        Me.LblTotalPcs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalPcsText
        '
        Me.LblTotalPcsText.AutoSize = True
        Me.LblTotalPcsText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPcsText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalPcsText.Location = New System.Drawing.Point(219, 6)
        Me.LblTotalPcsText.Name = "LblTotalPcsText"
        Me.LblTotalPcsText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalPcsText.TabIndex = 749
        Me.LblTotalPcsText.Text = "Total Pcs :"
        '
        'FrmPurchaseInvoiceDimension_WithDimension
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(633, 277)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.Name = "FrmPurchaseInvoiceDimension_WithDimension"
        Me.Text = "Dimensions"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pnl1 As Panel
    Friend WithEvents BtnOk As Button
    Friend WithEvents Panel1 As Panel
    Protected WithEvents LblTotalQty As Label
    Protected WithEvents LblTotalQtyText As Label
    Protected WithEvents LblTotalDealQty As Label
    Protected WithEvents LblTotalDealQtyText As Label
    Protected WithEvents LblTotalPcs As Label
    Protected WithEvents LblTotalPcsText As Label
End Class
