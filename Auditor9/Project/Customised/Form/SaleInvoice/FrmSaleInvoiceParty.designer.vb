<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSaleInvoiceParty
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
        Me.Pnl4 = New System.Windows.Forms.Panel()
        Me.LblBalanceToReceipt = New System.Windows.Forms.Label()
        Me.LblTotalReceipt = New System.Windows.Forms.Label()
        Me.LblBalanceToReceiptText = New System.Windows.Forms.Label()
        Me.LblTotalReceiptText = New System.Windows.Forms.Label()
        Me.LblCashToRefund = New System.Windows.Forms.Label()
        Me.LblCashToRefundText = New System.Windows.Forms.Label()
        Me.TxtCashReceived = New AgControls.AgTextBox()
        Me.LblCashReceivedText = New System.Windows.Forms.Label()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.LblInvoiceAmount = New System.Windows.Forms.Label()
        Me.LblInvoiceAmountText = New System.Windows.Forms.Label()
        Me.Pnl3 = New System.Windows.Forms.Panel()
        Me.Pnl4.SuspendLayout()
        Me.Pnl3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(0, 0)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(549, 213)
        Me.Pnl1.TabIndex = 1
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(482, 9)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(54, 23)
        Me.BtnOk.TabIndex = 744
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'Pnl4
        '
        Me.Pnl4.BackColor = System.Drawing.Color.Transparent
        Me.Pnl4.Controls.Add(Me.LblBalanceToReceipt)
        Me.Pnl4.Controls.Add(Me.LblTotalReceipt)
        Me.Pnl4.Controls.Add(Me.LblBalanceToReceiptText)
        Me.Pnl4.Controls.Add(Me.LblTotalReceiptText)
        Me.Pnl4.Controls.Add(Me.BtnOk)
        Me.Pnl4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl4.Location = New System.Drawing.Point(0, 386)
        Me.Pnl4.Name = "Pnl4"
        Me.Pnl4.Size = New System.Drawing.Size(549, 38)
        Me.Pnl4.TabIndex = 744
        '
        'LblBalanceToReceipt
        '
        Me.LblBalanceToReceipt.AutoSize = True
        Me.LblBalanceToReceipt.BackColor = System.Drawing.Color.Transparent
        Me.LblBalanceToReceipt.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBalanceToReceipt.ForeColor = System.Drawing.Color.Maroon
        Me.LblBalanceToReceipt.Location = New System.Drawing.Point(293, 16)
        Me.LblBalanceToReceipt.Name = "LblBalanceToReceipt"
        Me.LblBalanceToReceipt.Size = New System.Drawing.Size(13, 16)
        Me.LblBalanceToReceipt.TabIndex = 3016
        Me.LblBalanceToReceipt.Text = "."
        '
        'LblTotalReceipt
        '
        Me.LblTotalReceipt.AutoSize = True
        Me.LblTotalReceipt.BackColor = System.Drawing.Color.Transparent
        Me.LblTotalReceipt.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalReceipt.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalReceipt.Location = New System.Drawing.Point(82, 16)
        Me.LblTotalReceipt.Name = "LblTotalReceipt"
        Me.LblTotalReceipt.Size = New System.Drawing.Size(13, 16)
        Me.LblTotalReceipt.TabIndex = 3014
        Me.LblTotalReceipt.Text = "."
        '
        'LblBalanceToReceiptText
        '
        Me.LblBalanceToReceiptText.AutoSize = True
        Me.LblBalanceToReceiptText.BackColor = System.Drawing.Color.Transparent
        Me.LblBalanceToReceiptText.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Bold)
        Me.LblBalanceToReceiptText.Location = New System.Drawing.Point(210, 5)
        Me.LblBalanceToReceiptText.Name = "LblBalanceToReceiptText"
        Me.LblBalanceToReceiptText.Size = New System.Drawing.Size(83, 28)
        Me.LblBalanceToReceiptText.TabIndex = 3015
        Me.LblBalanceToReceiptText.Text = "Balance To " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Received"
        '
        'LblTotalReceiptText
        '
        Me.LblTotalReceiptText.AutoSize = True
        Me.LblTotalReceiptText.BackColor = System.Drawing.Color.Transparent
        Me.LblTotalReceiptText.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Bold)
        Me.LblTotalReceiptText.Location = New System.Drawing.Point(9, 5)
        Me.LblTotalReceiptText.Name = "LblTotalReceiptText"
        Me.LblTotalReceiptText.Size = New System.Drawing.Size(67, 28)
        Me.LblTotalReceiptText.TabIndex = 3014
        Me.LblTotalReceiptText.Text = "Total " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Received"
        '
        'LblCashToRefund
        '
        Me.LblCashToRefund.AutoSize = True
        Me.LblCashToRefund.BackColor = System.Drawing.Color.Transparent
        Me.LblCashToRefund.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCashToRefund.ForeColor = System.Drawing.Color.Maroon
        Me.LblCashToRefund.Location = New System.Drawing.Point(443, 13)
        Me.LblCashToRefund.Name = "LblCashToRefund"
        Me.LblCashToRefund.Size = New System.Drawing.Size(13, 16)
        Me.LblCashToRefund.TabIndex = 3011
        Me.LblCashToRefund.Text = "."
        '
        'LblCashToRefundText
        '
        Me.LblCashToRefundText.AutoSize = True
        Me.LblCashToRefundText.BackColor = System.Drawing.Color.Transparent
        Me.LblCashToRefundText.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Bold)
        Me.LblCashToRefundText.Location = New System.Drawing.Point(368, 4)
        Me.LblCashToRefundText.Name = "LblCashToRefundText"
        Me.LblCashToRefundText.Size = New System.Drawing.Size(73, 28)
        Me.LblCashToRefundText.TabIndex = 3009
        Me.LblCashToRefundText.Text = "Cash " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "To Refund"
        '
        'TxtCashReceived
        '
        Me.TxtCashReceived.AgAllowUserToEnableMasterHelp = False
        Me.TxtCashReceived.AgLastValueTag = Nothing
        Me.TxtCashReceived.AgLastValueText = Nothing
        Me.TxtCashReceived.AgMandatory = False
        Me.TxtCashReceived.AgMasterHelp = False
        Me.TxtCashReceived.AgNumberLeftPlaces = 8
        Me.TxtCashReceived.AgNumberNegetiveAllow = False
        Me.TxtCashReceived.AgNumberRightPlaces = 2
        Me.TxtCashReceived.AgPickFromLastValue = False
        Me.TxtCashReceived.AgRowFilter = ""
        Me.TxtCashReceived.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCashReceived.AgSelectedValue = Nothing
        Me.TxtCashReceived.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCashReceived.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCashReceived.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCashReceived.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCashReceived.Location = New System.Drawing.Point(242, 8)
        Me.TxtCashReceived.MaxLength = 20
        Me.TxtCashReceived.Name = "TxtCashReceived"
        Me.TxtCashReceived.Size = New System.Drawing.Size(98, 19)
        Me.TxtCashReceived.TabIndex = 3
        '
        'LblCashReceivedText
        '
        Me.LblCashReceivedText.AutoSize = True
        Me.LblCashReceivedText.BackColor = System.Drawing.Color.Transparent
        Me.LblCashReceivedText.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Bold)
        Me.LblCashReceivedText.Location = New System.Drawing.Point(168, 4)
        Me.LblCashReceivedText.Name = "LblCashReceivedText"
        Me.LblCashReceivedText.Size = New System.Drawing.Size(67, 28)
        Me.LblCashReceivedText.TabIndex = 3007
        Me.LblCashReceivedText.Text = "Cash " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Received"
        '
        'Pnl2
        '
        Me.Pnl2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl2.Location = New System.Drawing.Point(0, 261)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(549, 120)
        Me.Pnl2.TabIndex = 4
        '
        'LblInvoiceAmount
        '
        Me.LblInvoiceAmount.AutoSize = True
        Me.LblInvoiceAmount.BackColor = System.Drawing.Color.Transparent
        Me.LblInvoiceAmount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInvoiceAmount.ForeColor = System.Drawing.Color.Maroon
        Me.LblInvoiceAmount.Location = New System.Drawing.Point(71, 12)
        Me.LblInvoiceAmount.Name = "LblInvoiceAmount"
        Me.LblInvoiceAmount.Size = New System.Drawing.Size(13, 16)
        Me.LblInvoiceAmount.TabIndex = 3013
        Me.LblInvoiceAmount.Text = "."
        '
        'LblInvoiceAmountText
        '
        Me.LblInvoiceAmountText.AutoSize = True
        Me.LblInvoiceAmountText.BackColor = System.Drawing.Color.Transparent
        Me.LblInvoiceAmountText.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Bold)
        Me.LblInvoiceAmountText.Location = New System.Drawing.Point(5, 4)
        Me.LblInvoiceAmountText.Name = "LblInvoiceAmountText"
        Me.LblInvoiceAmountText.Size = New System.Drawing.Size(60, 28)
        Me.LblInvoiceAmountText.TabIndex = 3012
        Me.LblInvoiceAmountText.Text = "Invoice " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Amount"
        '
        'Pnl3
        '
        Me.Pnl3.Controls.Add(Me.LblCashToRefund)
        Me.Pnl3.Controls.Add(Me.LblInvoiceAmountText)
        Me.Pnl3.Controls.Add(Me.LblCashToRefundText)
        Me.Pnl3.Controls.Add(Me.LblInvoiceAmount)
        Me.Pnl3.Controls.Add(Me.TxtCashReceived)
        Me.Pnl3.Controls.Add(Me.LblCashReceivedText)
        Me.Pnl3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl3.Location = New System.Drawing.Point(0, 219)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(549, 38)
        Me.Pnl3.TabIndex = 2
        '
        'FrmSaleInvoiceParty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 427)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.Pnl4)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.Name = "FrmSaleInvoiceParty"
        Me.Text = "Party Detail"
        Me.Pnl4.ResumeLayout(False)
        Me.Pnl4.PerformLayout()
        Me.Pnl3.ResumeLayout(False)
        Me.Pnl3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pnl1 As Panel
    Friend WithEvents BtnOk As Button
    Friend WithEvents Pnl4 As Panel
    Friend WithEvents Pnl2 As Panel
    Public WithEvents TxtCashReceived As AgControls.AgTextBox
    Public WithEvents LblCashReceivedText As Label
    Public WithEvents LblCashToRefund As Label
    Public WithEvents LblCashToRefundText As Label
    Public WithEvents LblInvoiceAmount As Label
    Public WithEvents LblInvoiceAmountText As Label
    Friend WithEvents Pnl3 As Panel
    Public WithEvents LblBalanceToReceipt As Label
    Public WithEvents LblTotalReceipt As Label
    Public WithEvents LblBalanceToReceiptText As Label
    Public WithEvents LblTotalReceiptText As Label
End Class
