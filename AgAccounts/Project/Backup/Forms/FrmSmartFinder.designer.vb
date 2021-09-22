<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSmartFinder
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
        Me.TxtFindIn = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.PnlMain = New System.Windows.Forms.Panel
        Me.BtnClose = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtAccountName = New AgControls.AgTextBox
        Me.BtnShow = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtFromDate = New AgControls.AgTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtVoucherType = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtToDate = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtToAmount = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtFromAmount = New AgControls.AgTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtItemName = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtType = New AgControls.AgTextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TxtChequeNo = New AgControls.AgTextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtNarration = New AgControls.AgTextBox
        Me.SuspendLayout()
        '
        'TxtFindIn
        '
        Me.TxtFindIn.BackColor = System.Drawing.Color.White
        Me.TxtFindIn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFindIn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFindIn.Location = New System.Drawing.Point(120, 21)
        Me.TxtFindIn.Name = "TxtFindIn"
        Me.TxtFindIn.Size = New System.Drawing.Size(113, 18)
        Me.TxtFindIn.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(16, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Find In"
        '
        'PnlMain
        '
        Me.PnlMain.BackColor = System.Drawing.Color.Transparent
        Me.PnlMain.Location = New System.Drawing.Point(5, 130)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(963, 458)
        Me.PnlMain.TabIndex = 12
        '
        'BtnClose
        '
        Me.BtnClose.AutoEllipsis = True
        Me.BtnClose.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnClose.Location = New System.Drawing.Point(884, 606)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(84, 24)
        Me.BtnClose.TabIndex = 13
        Me.BtnClose.Text = "Clos&e"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(450, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 16)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Account Name"
        '
        'TxtAccountName
        '
        Me.TxtAccountName.BackColor = System.Drawing.Color.White
        Me.TxtAccountName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAccountName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccountName.Location = New System.Drawing.Point(555, 21)
        Me.TxtAccountName.Name = "TxtAccountName"
        Me.TxtAccountName.Size = New System.Drawing.Size(290, 18)
        Me.TxtAccountName.TabIndex = 7
        '
        'BtnShow
        '
        Me.BtnShow.AutoEllipsis = True
        Me.BtnShow.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnShow.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnShow.Location = New System.Drawing.Point(851, 15)
        Me.BtnShow.Name = "BtnShow"
        Me.BtnShow.Size = New System.Drawing.Size(117, 88)
        Me.BtnShow.TabIndex = 11
        Me.BtnShow.Text = "&Show"
        Me.BtnShow.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(5, 118)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(963, 9)
        Me.GroupBox1.TabIndex = 71
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Tag = ""
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(16, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "From Date"
        '
        'TxtFromDate
        '
        Me.TxtFromDate.BackColor = System.Drawing.Color.White
        Me.TxtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromDate.Location = New System.Drawing.Point(120, 41)
        Me.TxtFromDate.Name = "TxtFromDate"
        Me.TxtFromDate.Size = New System.Drawing.Size(113, 18)
        Me.TxtFromDate.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(5, 592)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(963, 9)
        Me.GroupBox2.TabIndex = 76
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(15, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 78
        Me.Label4.Text = "Voucher Type"
        '
        'TxtVoucherType
        '
        Me.TxtVoucherType.BackColor = System.Drawing.Color.White
        Me.TxtVoucherType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVoucherType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVoucherType.Location = New System.Drawing.Point(120, 81)
        Me.TxtVoucherType.Name = "TxtVoucherType"
        Me.TxtVoucherType.Size = New System.Drawing.Size(298, 18)
        Me.TxtVoucherType.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(242, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 16)
        Me.Label5.TabIndex = 80
        Me.Label5.Text = "To Date"
        '
        'TxtToDate
        '
        Me.TxtToDate.BackColor = System.Drawing.Color.White
        Me.TxtToDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToDate.Location = New System.Drawing.Point(305, 41)
        Me.TxtToDate.Name = "TxtToDate"
        Me.TxtToDate.Size = New System.Drawing.Size(113, 18)
        Me.TxtToDate.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(242, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 16)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Between"
        '
        'TxtToAmount
        '
        Me.TxtToAmount.BackColor = System.Drawing.Color.White
        Me.TxtToAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToAmount.Location = New System.Drawing.Point(305, 61)
        Me.TxtToAmount.Name = "TxtToAmount"
        Me.TxtToAmount.Size = New System.Drawing.Size(113, 18)
        Me.TxtToAmount.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(16, 61)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 16)
        Me.Label7.TabIndex = 82
        Me.Label7.Text = "Amt / Qty"
        '
        'TxtFromAmount
        '
        Me.TxtFromAmount.BackColor = System.Drawing.Color.White
        Me.TxtFromAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromAmount.Location = New System.Drawing.Point(120, 61)
        Me.TxtFromAmount.Name = "TxtFromAmount"
        Me.TxtFromAmount.Size = New System.Drawing.Size(113, 18)
        Me.TxtFromAmount.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(450, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 16)
        Me.Label8.TabIndex = 86
        Me.Label8.Text = "Item Name"
        '
        'TxtItemName
        '
        Me.TxtItemName.BackColor = System.Drawing.Color.White
        Me.TxtItemName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemName.Location = New System.Drawing.Point(555, 41)
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(290, 18)
        Me.TxtItemName.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(242, 21)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 16)
        Me.Label9.TabIndex = 88
        Me.Label9.Text = "Type"
        '
        'TxtType
        '
        Me.TxtType.BackColor = System.Drawing.Color.White
        Me.TxtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtType.Location = New System.Drawing.Point(305, 21)
        Me.TxtType.Name = "TxtType"
        Me.TxtType.Size = New System.Drawing.Size(113, 18)
        Me.TxtType.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(450, 61)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 16)
        Me.Label10.TabIndex = 90
        Me.Label10.Text = "Cheque No."
        '
        'TxtChequeNo
        '
        Me.TxtChequeNo.BackColor = System.Drawing.Color.White
        Me.TxtChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChequeNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChequeNo.Location = New System.Drawing.Point(555, 61)
        Me.TxtChequeNo.Name = "TxtChequeNo"
        Me.TxtChequeNo.Size = New System.Drawing.Size(290, 18)
        Me.TxtChequeNo.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(450, 81)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 16)
        Me.Label11.TabIndex = 92
        Me.Label11.Text = "Narration"
        '
        'TxtNarration
        '
        Me.TxtNarration.BackColor = System.Drawing.Color.White
        Me.TxtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNarration.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNarration.Location = New System.Drawing.Point(555, 81)
        Me.TxtNarration.Name = "TxtNarration"
        Me.TxtNarration.Size = New System.Drawing.Size(290, 18)
        Me.TxtNarration.TabIndex = 10
        '
        'FrmSmartFinder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(972, 633)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtNarration)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtChequeNo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtType)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtItemName)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtToAmount)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtFromAmount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtToDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtVoucherType)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtFromDate)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnShow)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtAccountName)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtFindIn)
        Me.Name = "FrmSmartFinder"
        Me.ShowIcon = False
        Me.Text = "Smart Finder"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtFindIn As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtAccountName As AgControls.AgTextBox
    Friend WithEvents BtnShow As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtFromDate As AgControls.AgTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtVoucherType As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtToDate As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtToAmount As AgControls.AgTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtFromAmount As AgControls.AgTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtItemName As AgControls.AgTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtType As AgControls.AgTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtChequeNo As AgControls.AgTextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtNarration As AgControls.AgTextBox
End Class
