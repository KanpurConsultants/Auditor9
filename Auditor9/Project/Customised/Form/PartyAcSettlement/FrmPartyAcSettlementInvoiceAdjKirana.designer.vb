﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPartyAcSettlementInvoiceAdjKirana
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
        Me.LblTotalAdditions = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblTotalDeductions = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LblNetAmountValue = New System.Windows.Forms.Label()
        Me.TxtIntRate = New AgControls.AgTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LblTaxableAmountValue = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblInvoiceNo = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(0, 55)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(510, 180)
        Me.Pnl1.TabIndex = 743
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(452, 3)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(54, 23)
        Me.BtnOk.TabIndex = 744
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LblTotalAdditions)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.LblTotalDeductions)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Controls.Add(Me.BtnOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 353)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(510, 28)
        Me.Panel1.TabIndex = 744
        '
        'LblTotalAdditions
        '
        Me.LblTotalAdditions.AutoSize = True
        Me.LblTotalAdditions.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAdditions.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAdditions.Location = New System.Drawing.Point(328, 6)
        Me.LblTotalAdditions.Name = "LblTotalAdditions"
        Me.LblTotalAdditions.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAdditions.TabIndex = 748
        Me.LblTotalAdditions.Text = "."
        Me.LblTotalAdditions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(242, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 16)
        Me.Label2.TabIndex = 747
        Me.Label2.Text = "Additions :"
        '
        'LblTotalDeductions
        '
        Me.LblTotalDeductions.AutoSize = True
        Me.LblTotalDeductions.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeductions.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalDeductions.Location = New System.Drawing.Point(98, 6)
        Me.LblTotalDeductions.Name = "LblTotalDeductions"
        Me.LblTotalDeductions.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeductions.TabIndex = 746
        Me.LblTotalDeductions.Text = "."
        Me.LblTotalDeductions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(12, 6)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(86, 16)
        Me.LblTotalQtyText.TabIndex = 745
        Me.LblTotalQtyText.Text = "Deductions :"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(2, 3)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(117, 23)
        Me.LinkLabel1.TabIndex = 809
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Invoice No."
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel2.Controls.Add(Me.LblNetAmountValue)
        Me.Panel2.Controls.Add(Me.TxtIntRate)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.LblTaxableAmountValue)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.LblInvoiceNo)
        Me.Panel2.Location = New System.Drawing.Point(1, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(509, 50)
        Me.Panel2.TabIndex = 808
        '
        'LblNetAmountValue
        '
        Me.LblNetAmountValue.AutoSize = True
        Me.LblNetAmountValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetAmountValue.ForeColor = System.Drawing.Color.Maroon
        Me.LblNetAmountValue.Location = New System.Drawing.Point(371, 29)
        Me.LblNetAmountValue.Name = "LblNetAmountValue"
        Me.LblNetAmountValue.Size = New System.Drawing.Size(12, 16)
        Me.LblNetAmountValue.TabIndex = 666
        Me.LblNetAmountValue.Text = "."
        Me.LblNetAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtIntRate
        '
        Me.TxtIntRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtIntRate.AgLastValueTag = Nothing
        Me.TxtIntRate.AgLastValueText = Nothing
        Me.TxtIntRate.AgMandatory = False
        Me.TxtIntRate.AgMasterHelp = False
        Me.TxtIntRate.AgNumberLeftPlaces = 8
        Me.TxtIntRate.AgNumberNegetiveAllow = False
        Me.TxtIntRate.AgNumberRightPlaces = 2
        Me.TxtIntRate.AgPickFromLastValue = False
        Me.TxtIntRate.AgRowFilter = ""
        Me.TxtIntRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIntRate.AgSelectedValue = Nothing
        Me.TxtIntRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIntRate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtIntRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtIntRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIntRate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIntRate.Location = New System.Drawing.Point(432, 4)
        Me.TxtIntRate.MaxLength = 20
        Me.TxtIntRate.Name = "TxtIntRate"
        Me.TxtIntRate.Size = New System.Drawing.Size(64, 16)
        Me.TxtIntRate.TabIndex = 3015
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(363, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 14)
        Me.Label1.TabIndex = 3016
        Me.Label1.Text = "Int. Days"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(281, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 16)
        Me.Label5.TabIndex = 665
        Me.Label5.Text = "Net Amount :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTaxableAmountValue
        '
        Me.LblTaxableAmountValue.AutoSize = True
        Me.LblTaxableAmountValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTaxableAmountValue.ForeColor = System.Drawing.Color.Maroon
        Me.LblTaxableAmountValue.Location = New System.Drawing.Point(124, 28)
        Me.LblTaxableAmountValue.Name = "LblTaxableAmountValue"
        Me.LblTaxableAmountValue.Size = New System.Drawing.Size(12, 16)
        Me.LblTaxableAmountValue.TabIndex = 664
        Me.LblTaxableAmountValue.Text = "."
        Me.LblTaxableAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(3, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 16)
        Me.Label4.TabIndex = 663
        Me.Label4.Text = "Taxable Amount : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblInvoiceNo
        '
        Me.LblInvoiceNo.AutoSize = True
        Me.LblInvoiceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.LblInvoiceNo.Location = New System.Drawing.Point(124, 4)
        Me.LblInvoiceNo.Name = "LblInvoiceNo"
        Me.LblInvoiceNo.Size = New System.Drawing.Size(12, 16)
        Me.LblInvoiceNo.TabIndex = 660
        Me.LblInvoiceNo.Text = "."
        Me.LblInvoiceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 241)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(510, 110)
        Me.Panel3.TabIndex = 744
        '
        'FrmPartyAcSettlementInvoiceAdjKirana
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 381)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.Name = "FrmPartyAcSettlementInvoiceAdjKirana"
        Me.Text = "PurchaseInvoiceHeader"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pnl1 As Panel
    Friend WithEvents BtnOk As Button
    Friend WithEvents Panel1 As Panel
    Protected WithEvents LblTotalDeductions As Label
    Protected WithEvents LblTotalQtyText As Label
    Protected WithEvents LblTotalAdditions As Label
    Protected WithEvents Label2 As Label
    Protected WithEvents LinkLabel1 As LinkLabel
    Protected WithEvents Panel2 As Panel
    Protected WithEvents LblInvoiceNo As Label
    Protected WithEvents LblNetAmountValue As Label
    Protected WithEvents Label5 As Label
    Protected WithEvents LblTaxableAmountValue As Label
    Protected WithEvents Label4 As Label
    Protected WithEvents TxtIntRate As AgControls.AgTextBox
    Protected WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
End Class
