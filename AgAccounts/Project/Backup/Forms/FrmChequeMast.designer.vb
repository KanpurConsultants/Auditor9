<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChequeMast
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
        Me.TxtChequeNofrom = New AgControls.AgTextBox
        Me.LblAcName = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtChequeSrNo = New AgControls.AgTextBox
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtV_Date = New AgControls.AgTextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.TxtBankName = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtChequeNoTo = New AgControls.AgTextBox
        Me.LblTotalForm = New System.Windows.Forms.Label
        Me.PnlMain = New System.Windows.Forms.Panel
        Me.BtnChequeToEdit = New System.Windows.Forms.Button
        Me.TxtChequeStatus = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TxtChequeNofrom
        '
        Me.TxtChequeNofrom.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtChequeNofrom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChequeNofrom.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChequeNofrom.Location = New System.Drawing.Point(372, 118)
        Me.TxtChequeNofrom.MaxLength = 7
        Me.TxtChequeNofrom.Name = "TxtChequeNofrom"
        Me.TxtChequeNofrom.Size = New System.Drawing.Size(118, 18)
        Me.TxtChequeNofrom.TabIndex = 3
        '
        'LblAcName
        '
        Me.LblAcName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LblAcName.AutoSize = True
        Me.LblAcName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcName.Location = New System.Drawing.Point(165, 118)
        Me.LblAcName.Name = "LblAcName"
        Me.LblAcName.Size = New System.Drawing.Size(106, 16)
        Me.LblAcName.TabIndex = 16
        Me.LblAcName.Text = "Cheque No From"
        '
        'Label14
        '
        Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(357, 118)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(10, 7)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Ä"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(491, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 16)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Cheque Serial."
        '
        'TxtChequeSrNo
        '
        Me.TxtChequeSrNo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtChequeSrNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChequeSrNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChequeSrNo.Location = New System.Drawing.Point(595, 98)
        Me.TxtChequeSrNo.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtChequeSrNo.MaxLength = 5
        Me.TxtChequeSrNo.Name = "TxtChequeSrNo"
        Me.TxtChequeSrNo.Size = New System.Drawing.Size(118, 18)
        Me.TxtChequeSrNo.TabIndex = 2
        '
        'Topctrl1
        '
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(883, 41)
        Me.Topctrl1.TabIndex = 8
        Me.Topctrl1.tAdd = True
        Me.Topctrl1.tCancel = True
        Me.Topctrl1.tDel = True
        Me.Topctrl1.tDiscard = False
        Me.Topctrl1.tEdit = True
        Me.Topctrl1.tExit = True
        Me.Topctrl1.tFind = True
        Me.Topctrl1.tFirst = True
        Me.Topctrl1.tLast = True
        Me.Topctrl1.tNext = True
        Me.Topctrl1.tPrev = True
        Me.Topctrl1.tPrn = True
        Me.Topctrl1.tRef = True
        Me.Topctrl1.tSave = False
        Me.Topctrl1.tSite = True
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(357, 98)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 7)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Ä"
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(165, 98)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(35, 16)
        Me.Label13.TabIndex = 43
        Me.Label13.Text = "Date"
        '
        'TxtV_Date
        '
        Me.TxtV_Date.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtV_Date.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtV_Date.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(372, 98)
        Me.TxtV_Date.Name = "TxtV_Date"
        Me.TxtV_Date.Size = New System.Drawing.Size(118, 18)
        Me.TxtV_Date.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(357, 78)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(10, 7)
        Me.Label16.TabIndex = 47
        Me.Label16.Text = "Ä"
        '
        'Label17
        '
        Me.Label17.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(165, 78)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(76, 16)
        Me.Label17.TabIndex = 46
        Me.Label17.Text = "Bank Name"
        '
        'TxtBankName
        '
        Me.TxtBankName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtBankName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBankName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankName.Location = New System.Drawing.Point(372, 78)
        Me.TxtBankName.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtBankName.MaxLength = 15
        Me.TxtBankName.Name = "TxtBankName"
        Me.TxtBankName.Size = New System.Drawing.Size(341, 18)
        Me.TxtBankName.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(581, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Ä"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(491, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 16)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Cheque No To"
        '
        'TxtChequeNoTo
        '
        Me.TxtChequeNoTo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtChequeNoTo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChequeNoTo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChequeNoTo.Location = New System.Drawing.Point(595, 118)
        Me.TxtChequeNoTo.MaxLength = 7
        Me.TxtChequeNoTo.Name = "TxtChequeNoTo"
        Me.TxtChequeNoTo.Size = New System.Drawing.Size(118, 18)
        Me.TxtChequeNoTo.TabIndex = 4
        '
        'LblTotalForm
        '
        Me.LblTotalForm.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LblTotalForm.AutoSize = True
        Me.LblTotalForm.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalForm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblTotalForm.Location = New System.Drawing.Point(719, 118)
        Me.LblTotalForm.Name = "LblTotalForm"
        Me.LblTotalForm.Size = New System.Drawing.Size(14, 15)
        Me.LblTotalForm.TabIndex = 60
        Me.LblTotalForm.Text = "0"
        '
        'PnlMain
        '
        Me.PnlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlMain.Location = New System.Drawing.Point(168, 208)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(545, 220)
        Me.PnlMain.TabIndex = 7
        '
        'BtnChequeToEdit
        '
        Me.BtnChequeToEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnChequeToEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnChequeToEdit.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnChequeToEdit.Location = New System.Drawing.Point(553, 172)
        Me.BtnChequeToEdit.Name = "BtnChequeToEdit"
        Me.BtnChequeToEdit.Size = New System.Drawing.Size(161, 27)
        Me.BtnChequeToEdit.TabIndex = 6
        Me.BtnChequeToEdit.Text = "&Cheques To Edit"
        Me.BtnChequeToEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnChequeToEdit.UseVisualStyleBackColor = True
        '
        'TxtChequeStatus
        '
        Me.TxtChequeStatus.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TxtChequeStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChequeStatus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChequeStatus.Location = New System.Drawing.Point(372, 138)
        Me.TxtChequeStatus.MaxLength = 7
        Me.TxtChequeStatus.Name = "TxtChequeStatus"
        Me.TxtChequeStatus.Size = New System.Drawing.Size(118, 18)
        Me.TxtChequeStatus.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(165, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 16)
        Me.Label4.TabIndex = 64
        Me.Label4.Text = "Cheque Status"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(357, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Ä"
        '
        'FrmChequeMast
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(883, 558)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtChequeStatus)
        Me.Controls.Add(Me.BtnChequeToEdit)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.LblTotalForm)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtChequeNoTo)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.TxtBankName)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TxtV_Date)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtChequeSrNo)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.LblAcName)
        Me.Controls.Add(Me.TxtChequeNofrom)
        Me.Controls.Add(Me.Topctrl1)
        Me.KeyPreview = True
        Me.Name = "FrmChequeMast"
        Me.ShowIcon = False
        Me.Tag = "BG"
        Me.Text = "Cheque Master"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents TxtChequeNofrom As AgControls.AgTextBox
    Friend WithEvents LblAcName As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtChequeSrNo As AgControls.AgTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtV_Date As AgControls.AgTextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TxtBankName As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtChequeNoTo As AgControls.AgTextBox
    Friend WithEvents LblTotalForm As System.Windows.Forms.Label
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents BtnChequeToEdit As System.Windows.Forms.Button
    Friend WithEvents TxtChequeStatus As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
