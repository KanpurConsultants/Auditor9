<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentVoucher
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
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbnChequeDate = New System.Windows.Forms.RadioButton()
        Me.rbnIssueDate = New System.Windows.Forms.RadioButton()
        Me.dtpChequeDateTo = New System.Windows.Forms.DateTimePicker()
        Me.lblBillDateTo = New System.Windows.Forms.Label()
        Me.dtpChequeDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblBillDateFrom = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbnInfixPayee = New System.Windows.Forms.RadioButton()
        Me.rbnSuffixPayee = New System.Windows.Forms.RadioButton()
        Me.rbnPrefixPayee = New System.Windows.Forms.RadioButton()
        Me.txtPayName = New System.Windows.Forms.TextBox()
        Me.lblPayName = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.dgvChequeIssue = New System.Windows.Forms.DataGridView()
        Me.cbxVoucher = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PayeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CHEQUEDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChequeNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BankName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Particulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChequeStatus = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.VoucherNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlToolBar = New System.Windows.Forms.Panel()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvChequeIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolBar.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.exit_32
        Me.btnExit.Location = New System.Drawing.Point(937, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(94, 39)
        Me.btnExit.TabIndex = 88
        Me.btnExit.Text = " E&XIT"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbnChequeDate)
        Me.Panel2.Controls.Add(Me.rbnIssueDate)
        Me.Panel2.Location = New System.Drawing.Point(328, 25)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(180, 29)
        Me.Panel2.TabIndex = 87
        '
        'rbnChequeDate
        '
        Me.rbnChequeDate.AutoSize = True
        Me.rbnChequeDate.Location = New System.Drawing.Point(90, 6)
        Me.rbnChequeDate.Name = "rbnChequeDate"
        Me.rbnChequeDate.Size = New System.Drawing.Size(88, 17)
        Me.rbnChequeDate.TabIndex = 1
        Me.rbnChequeDate.Text = "Cheque Date"
        Me.rbnChequeDate.UseVisualStyleBackColor = True
        '
        'rbnIssueDate
        '
        Me.rbnIssueDate.AutoSize = True
        Me.rbnIssueDate.Checked = True
        Me.rbnIssueDate.Location = New System.Drawing.Point(6, 6)
        Me.rbnIssueDate.Name = "rbnIssueDate"
        Me.rbnIssueDate.Size = New System.Drawing.Size(76, 17)
        Me.rbnIssueDate.TabIndex = 0
        Me.rbnIssueDate.TabStop = True
        Me.rbnIssueDate.Text = "Issue Date"
        Me.rbnIssueDate.UseVisualStyleBackColor = True
        '
        'dtpChequeDateTo
        '
        Me.dtpChequeDateTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpChequeDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDateTo.Location = New System.Drawing.Point(202, 30)
        Me.dtpChequeDateTo.Name = "dtpChequeDateTo"
        Me.dtpChequeDateTo.Size = New System.Drawing.Size(124, 20)
        Me.dtpChequeDateTo.TabIndex = 86
        '
        'lblBillDateTo
        '
        Me.lblBillDateTo.AutoSize = True
        Me.lblBillDateTo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDateTo.Location = New System.Drawing.Point(189, 32)
        Me.lblBillDateTo.Name = "lblBillDateTo"
        Me.lblBillDateTo.Size = New System.Drawing.Size(14, 19)
        Me.lblBillDateTo.TabIndex = 85
        Me.lblBillDateTo.Text = "-"
        '
        'dtpChequeDateFrom
        '
        Me.dtpChequeDateFrom.Checked = False
        Me.dtpChequeDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpChequeDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDateFrom.Location = New System.Drawing.Point(62, 30)
        Me.dtpChequeDateFrom.Name = "dtpChequeDateFrom"
        Me.dtpChequeDateFrom.Size = New System.Drawing.Size(124, 20)
        Me.dtpChequeDateFrom.TabIndex = 84
        '
        'lblBillDateFrom
        '
        Me.lblBillDateFrom.AutoSize = True
        Me.lblBillDateFrom.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDateFrom.Location = New System.Drawing.Point(16, 32)
        Me.lblBillDateFrom.Name = "lblBillDateFrom"
        Me.lblBillDateFrom.Size = New System.Drawing.Size(37, 16)
        Me.lblBillDateFrom.TabIndex = 83
        Me.lblBillDateFrom.Text = "Date"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.search_32
        Me.btnSearch.Location = New System.Drawing.Point(835, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(94, 39)
        Me.btnSearch.TabIndex = 82
        Me.btnSearch.Text = "SEARC&H"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Controls.Add(Me.txtPayName)
        Me.Panel5.Controls.Add(Me.lblPayName)
        Me.Panel5.Location = New System.Drawing.Point(514, 11)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(315, 43)
        Me.Panel5.TabIndex = 89
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbnInfixPayee)
        Me.Panel1.Controls.Add(Me.rbnSuffixPayee)
        Me.Panel1.Controls.Add(Me.rbnPrefixPayee)
        Me.Panel1.Location = New System.Drawing.Point(131, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(182, 20)
        Me.Panel1.TabIndex = 47
        '
        'rbnInfixPayee
        '
        Me.rbnInfixPayee.AutoSize = True
        Me.rbnInfixPayee.Checked = True
        Me.rbnInfixPayee.Location = New System.Drawing.Point(72, 0)
        Me.rbnInfixPayee.Name = "rbnInfixPayee"
        Me.rbnInfixPayee.Size = New System.Drawing.Size(44, 17)
        Me.rbnInfixPayee.TabIndex = 13
        Me.rbnInfixPayee.TabStop = True
        Me.rbnInfixPayee.Text = "Infix"
        Me.rbnInfixPayee.UseVisualStyleBackColor = True
        '
        'rbnSuffixPayee
        '
        Me.rbnSuffixPayee.AutoSize = True
        Me.rbnSuffixPayee.Location = New System.Drawing.Point(124, 0)
        Me.rbnSuffixPayee.Name = "rbnSuffixPayee"
        Me.rbnSuffixPayee.Size = New System.Drawing.Size(51, 17)
        Me.rbnSuffixPayee.TabIndex = 14
        Me.rbnSuffixPayee.Text = "Suffix"
        Me.rbnSuffixPayee.UseVisualStyleBackColor = True
        '
        'rbnPrefixPayee
        '
        Me.rbnPrefixPayee.AutoSize = True
        Me.rbnPrefixPayee.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.rbnPrefixPayee.Location = New System.Drawing.Point(0, 0)
        Me.rbnPrefixPayee.Name = "rbnPrefixPayee"
        Me.rbnPrefixPayee.Size = New System.Drawing.Size(64, 20)
        Me.rbnPrefixPayee.TabIndex = 12
        Me.rbnPrefixPayee.Text = "Prefix"
        Me.rbnPrefixPayee.UseVisualStyleBackColor = True
        '
        'txtPayName
        '
        Me.txtPayName.Location = New System.Drawing.Point(3, 20)
        Me.txtPayName.Name = "txtPayName"
        Me.txtPayName.Size = New System.Drawing.Size(310, 20)
        Me.txtPayName.TabIndex = 46
        '
        'lblPayName
        '
        Me.lblPayName.AutoSize = True
        Me.lblPayName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayName.Location = New System.Drawing.Point(3, 4)
        Me.lblPayName.Name = "lblPayName"
        Me.lblPayName.Size = New System.Drawing.Size(73, 16)
        Me.lblPayName.TabIndex = 45
        Me.lblPayName.Text = "Pay Name"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.White
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrint.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.print_cheque_32
        Me.btnPrint.Location = New System.Drawing.Point(937, 411)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(94, 39)
        Me.btnPrint.TabIndex = 91
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(19, 422)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(51, 16)
        Me.lblTotal.TabIndex = 92
        Me.lblTotal.Text = "Total : "
        '
        'lblHeading
        '
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.Location = New System.Drawing.Point(0, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(1077, 42)
        Me.lblHeading.TabIndex = 93
        Me.lblHeading.Text = "PRINT PAYMENT VOUCHER"
        Me.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvChequeIssue
        '
        Me.dgvChequeIssue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChequeIssue.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cbxVoucher, Me.PayeeName, Me.Amount, Me.IssueDate, Me.CHEQUEDATE, Me.ChequeNo, Me.BankName, Me.Particulars, Me.ChequeStatus, Me.VoucherNo})
        Me.dgvChequeIssue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvChequeIssue.Location = New System.Drawing.Point(19, 71)
        Me.dgvChequeIssue.Name = "dgvChequeIssue"
        Me.dgvChequeIssue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvChequeIssue.Size = New System.Drawing.Size(1012, 334)
        Me.dgvChequeIssue.TabIndex = 94
        '
        'cbxVoucher
        '
        Me.cbxVoucher.HeaderText = ""
        Me.cbxVoucher.Name = "cbxVoucher"
        Me.cbxVoucher.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cbxVoucher.Width = 40
        '
        'PayeeName
        '
        Me.PayeeName.HeaderText = "PAYEE NAME"
        Me.PayeeName.Name = "PayeeName"
        Me.PayeeName.ReadOnly = True
        Me.PayeeName.Width = 200
        '
        'Amount
        '
        Me.Amount.HeaderText = "AMOUNT"
        Me.Amount.Name = "Amount"
        Me.Amount.ReadOnly = True
        '
        'IssueDate
        '
        Me.IssueDate.HeaderText = "ISSUE DATE"
        Me.IssueDate.Name = "IssueDate"
        Me.IssueDate.ReadOnly = True
        Me.IssueDate.Width = 98
        '
        'CHEQUEDATE
        '
        Me.CHEQUEDATE.HeaderText = "CHQ DATE"
        Me.CHEQUEDATE.Name = "CHEQUEDATE"
        Me.CHEQUEDATE.ReadOnly = True
        Me.CHEQUEDATE.Width = 95
        '
        'ChequeNo
        '
        Me.ChequeNo.HeaderText = "CHEQUE NO"
        Me.ChequeNo.Name = "ChequeNo"
        Me.ChequeNo.ReadOnly = True
        '
        'BankName
        '
        Me.BankName.HeaderText = "BANK NAME"
        Me.BankName.Name = "BankName"
        Me.BankName.ReadOnly = True
        Me.BankName.Width = 150
        '
        'Particulars
        '
        Me.Particulars.HeaderText = "PAY FOR"
        Me.Particulars.Name = "Particulars"
        Me.Particulars.ReadOnly = True
        Me.Particulars.Width = 300
        '
        'ChequeStatus
        '
        Me.ChequeStatus.DisplayStyleForCurrentCellOnly = True
        Me.ChequeStatus.HeaderText = "STATUS"
        Me.ChequeStatus.Items.AddRange(New Object() {"Draft Cheque", "Issued Cheque", "Cancel Cheque"})
        Me.ChequeStatus.Name = "ChequeStatus"
        Me.ChequeStatus.ReadOnly = True
        Me.ChequeStatus.Width = 105
        '
        'VoucherNo
        '
        Me.VoucherNo.HeaderText = "VoucherNo"
        Me.VoucherNo.Name = "VoucherNo"
        Me.VoucherNo.ReadOnly = True
        Me.VoucherNo.Visible = False
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Controls.Add(Me.dgvChequeIssue)
        Me.pnlToolBar.Controls.Add(Me.lblTotal)
        Me.pnlToolBar.Controls.Add(Me.btnPrint)
        Me.pnlToolBar.Controls.Add(Me.Panel5)
        Me.pnlToolBar.Controls.Add(Me.btnExit)
        Me.pnlToolBar.Controls.Add(Me.Panel2)
        Me.pnlToolBar.Controls.Add(Me.dtpChequeDateTo)
        Me.pnlToolBar.Controls.Add(Me.lblBillDateTo)
        Me.pnlToolBar.Controls.Add(Me.dtpChequeDateFrom)
        Me.pnlToolBar.Controls.Add(Me.lblBillDateFrom)
        Me.pnlToolBar.Controls.Add(Me.btnSearch)
        Me.pnlToolBar.Location = New System.Drawing.Point(5, 56)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(1049, 465)
        Me.pnlToolBar.TabIndex = 95
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeading)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1077, 50)
        Me.pnlHeader.TabIndex = 96
        '
        'frmPaymentVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1077, 562)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlToolBar)
        Me.Name = "frmPaymentVoucher"
        Me.Text = "PRINT PAYMENT VOUCHER"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvChequeIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents rbnChequeDate As RadioButton
    Friend WithEvents rbnIssueDate As RadioButton
    Friend WithEvents dtpChequeDateTo As DateTimePicker
    Friend WithEvents lblBillDateTo As Label
    Friend WithEvents dtpChequeDateFrom As DateTimePicker
    Friend WithEvents lblBillDateFrom As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents rbnInfixPayee As RadioButton
    Friend WithEvents rbnSuffixPayee As RadioButton
    Friend WithEvents rbnPrefixPayee As RadioButton
    Friend WithEvents txtPayName As TextBox
    Friend WithEvents lblPayName As Label
    Friend WithEvents btnPrint As Button
    Friend WithEvents lblTotal As Label
    Friend WithEvents lblHeading As Label
    Friend WithEvents dgvChequeIssue As DataGridView
    Friend WithEvents cbxVoucher As DataGridViewCheckBoxColumn
    Friend WithEvents PayeeName As DataGridViewTextBoxColumn
    Friend WithEvents Amount As DataGridViewTextBoxColumn
    Friend WithEvents IssueDate As DataGridViewTextBoxColumn
    Friend WithEvents CHEQUEDATE As DataGridViewTextBoxColumn
    Friend WithEvents ChequeNo As DataGridViewTextBoxColumn
    Friend WithEvents BankName As DataGridViewTextBoxColumn
    Friend WithEvents Particulars As DataGridViewTextBoxColumn
    Friend WithEvents ChequeStatus As DataGridViewComboBoxColumn
    Friend WithEvents VoucherNo As DataGridViewTextBoxColumn
    Friend WithEvents pnlToolBar As Panel
    Friend WithEvents pnlHeader As Panel
End Class
