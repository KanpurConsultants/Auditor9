<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankReconciliation
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
        Me.PnlMain = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.TxtBankName = New AgControls.AgTextBox
        Me.BtnFillGrid = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BtnSave = New System.Windows.Forms.Button
        Me.BtnExit = New System.Windows.Forms.Button
        Me.LblTitle = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtType = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtDate = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.LblBG = New System.Windows.Forms.Label
        Me.Lblbalance = New System.Windows.Forms.Label
        Me.LblAmountnotReflected = New System.Windows.Forms.Label
        Me.Lblbank = New System.Windows.Forms.Label
        Me.LblCompanyBal = New System.Windows.Forms.Label
        Me.LblAmtNotClg_Dr = New System.Windows.Forms.Label
        Me.LblClgAmt = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtshowContra = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.LblAmtNotClg_Cr = New System.Windows.Forms.Label
        Me.BtnPrint = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'PnlMain
        '
        Me.PnlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlMain.Location = New System.Drawing.Point(12, 144)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(958, 384)
        Me.PnlMain.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(307, 70)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(10, 7)
        Me.Label16.TabIndex = 47
        Me.Label16.Text = "Ä"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(198, 70)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(76, 16)
        Me.Label17.TabIndex = 46
        Me.Label17.Text = "Bank Name"
        '
        'TxtBankName
        '
        Me.TxtBankName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBankName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankName.Location = New System.Drawing.Point(321, 70)
        Me.TxtBankName.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtBankName.MaxLength = 15
        Me.TxtBankName.Name = "TxtBankName"
        Me.TxtBankName.Size = New System.Drawing.Size(364, 18)
        Me.TxtBankName.TabIndex = 1
        '
        'BtnFillGrid
        '
        Me.BtnFillGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFillGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillGrid.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnFillGrid.Location = New System.Drawing.Point(772, 66)
        Me.BtnFillGrid.Name = "BtnFillGrid"
        Me.BtnFillGrid.Size = New System.Drawing.Size(100, 27)
        Me.BtnFillGrid.TabIndex = 4
        Me.BtnFillGrid.Text = "&Fill Grid"
        Me.BtnFillGrid.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(7, 125)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(971, 9)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Tag = ""
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnSave.Location = New System.Drawing.Point(666, 607)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(100, 27)
        Me.BtnSave.TabIndex = 6
        Me.BtnSave.Text = "&Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnExit.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnExit.Location = New System.Drawing.Point(878, 607)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(100, 27)
        Me.BtnExit.TabIndex = 8
        Me.BtnExit.Text = "&Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'LblTitle
        '
        Me.LblTitle.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitle.ForeColor = System.Drawing.Color.Maroon
        Me.LblTitle.Location = New System.Drawing.Point(0, 0)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(982, 31)
        Me.LblTitle.TabIndex = 54
        Me.LblTitle.Text = "Bank Reconciliation Entry "
        Me.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(7, 591)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(971, 10)
        Me.GroupBox2.TabIndex = 55
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(307, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Ä"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(198, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 16)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Type"
        '
        'TxtType
        '
        Me.TxtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtType.Location = New System.Drawing.Point(321, 90)
        Me.TxtType.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtType.MaxLength = 15
        Me.TxtType.Name = "TxtType"
        Me.TxtType.Size = New System.Drawing.Size(128, 18)
        Me.TxtType.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(307, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "Ä"
        '
        'TxtDate
        '
        Me.TxtDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate.Location = New System.Drawing.Point(321, 50)
        Me.TxtDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtDate.MaxLength = 15
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(364, 18)
        Me.TxtDate.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(198, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 16)
        Me.Label4.TabIndex = 70
        Me.Label4.Text = "As On Date"
        '
        'LblBG
        '
        Me.LblBG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblBG.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblBG.Location = New System.Drawing.Point(12, 529)
        Me.LblBG.Name = "LblBG"
        Me.LblBG.Size = New System.Drawing.Size(958, 61)
        Me.LblBG.TabIndex = 73
        '
        'Lblbalance
        '
        Me.Lblbalance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lblbalance.AutoSize = True
        Me.Lblbalance.BackColor = System.Drawing.Color.LemonChiffon
        Me.Lblbalance.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblbalance.ForeColor = System.Drawing.Color.Maroon
        Me.Lblbalance.Location = New System.Drawing.Point(446, 532)
        Me.Lblbalance.Name = "Lblbalance"
        Me.Lblbalance.Size = New System.Drawing.Size(189, 15)
        Me.Lblbalance.TabIndex = 74
        Me.Lblbalance.Text = "Balance As Per Company Books"
        '
        'LblAmountnotReflected
        '
        Me.LblAmountnotReflected.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblAmountnotReflected.AutoSize = True
        Me.LblAmountnotReflected.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblAmountnotReflected.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAmountnotReflected.ForeColor = System.Drawing.Color.Maroon
        Me.LblAmountnotReflected.Location = New System.Drawing.Point(446, 551)
        Me.LblAmountnotReflected.Name = "LblAmountnotReflected"
        Me.LblAmountnotReflected.Size = New System.Drawing.Size(126, 15)
        Me.LblAmountnotReflected.TabIndex = 75
        Me.LblAmountnotReflected.Text = "Amount not reflected"
        '
        'Lblbank
        '
        Me.Lblbank.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lblbank.AutoSize = True
        Me.Lblbank.BackColor = System.Drawing.Color.LemonChiffon
        Me.Lblbank.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblbank.ForeColor = System.Drawing.Color.Maroon
        Me.Lblbank.Location = New System.Drawing.Point(446, 570)
        Me.Lblbank.Name = "Lblbank"
        Me.Lblbank.Size = New System.Drawing.Size(126, 15)
        Me.Lblbank.TabIndex = 76
        Me.Lblbank.Text = "Balance As Per Bank"
        '
        'LblCompanyBal
        '
        Me.LblCompanyBal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCompanyBal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCompanyBal.ForeColor = System.Drawing.Color.Black
        Me.LblCompanyBal.Location = New System.Drawing.Point(852, 532)
        Me.LblCompanyBal.Name = "LblCompanyBal"
        Me.LblCompanyBal.Size = New System.Drawing.Size(118, 15)
        Me.LblCompanyBal.TabIndex = 77
        Me.LblCompanyBal.Text = "0"
        Me.LblCompanyBal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblAmtNotClg_Dr
        '
        Me.LblAmtNotClg_Dr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblAmtNotClg_Dr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAmtNotClg_Dr.ForeColor = System.Drawing.Color.Black
        Me.LblAmtNotClg_Dr.Location = New System.Drawing.Point(731, 551)
        Me.LblAmtNotClg_Dr.Name = "LblAmtNotClg_Dr"
        Me.LblAmtNotClg_Dr.Size = New System.Drawing.Size(118, 15)
        Me.LblAmtNotClg_Dr.TabIndex = 78
        Me.LblAmtNotClg_Dr.Text = "0"
        Me.LblAmtNotClg_Dr.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblClgAmt
        '
        Me.LblClgAmt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblClgAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblClgAmt.ForeColor = System.Drawing.Color.Black
        Me.LblClgAmt.Location = New System.Drawing.Point(852, 570)
        Me.LblClgAmt.Name = "LblClgAmt"
        Me.LblClgAmt.Size = New System.Drawing.Size(118, 15)
        Me.LblClgAmt.TabIndex = 79
        Me.LblClgAmt.Text = "0"
        Me.LblClgAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(453, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 16)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "Show Contra"
        '
        'TxtshowContra
        '
        Me.TxtshowContra.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtshowContra.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtshowContra.Location = New System.Drawing.Point(557, 90)
        Me.TxtshowContra.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtshowContra.MaxLength = 15
        Me.TxtshowContra.Name = "TxtshowContra"
        Me.TxtshowContra.Size = New System.Drawing.Size(128, 18)
        Me.TxtshowContra.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(541, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 82
        Me.Label5.Text = "Ä"
        '
        'LblAmtNotClg_Cr
        '
        Me.LblAmtNotClg_Cr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblAmtNotClg_Cr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAmtNotClg_Cr.ForeColor = System.Drawing.Color.Black
        Me.LblAmtNotClg_Cr.Location = New System.Drawing.Point(852, 551)
        Me.LblAmtNotClg_Cr.Name = "LblAmtNotClg_Cr"
        Me.LblAmtNotClg_Cr.Size = New System.Drawing.Size(118, 15)
        Me.LblAmtNotClg_Cr.TabIndex = 83
        Me.LblAmtNotClg_Cr.Text = "0"
        Me.LblAmtNotClg_Cr.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnPrint.Location = New System.Drawing.Point(772, 607)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(100, 27)
        Me.BtnPrint.TabIndex = 7
        Me.BtnPrint.Text = "&Print"
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'FrmBankReconciliation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(982, 638)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.LblAmtNotClg_Cr)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtshowContra)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblClgAmt)
        Me.Controls.Add(Me.LblAmtNotClg_Dr)
        Me.Controls.Add(Me.LblCompanyBal)
        Me.Controls.Add(Me.Lblbank)
        Me.Controls.Add(Me.LblAmountnotReflected)
        Me.Controls.Add(Me.Lblbalance)
        Me.Controls.Add(Me.LblBG)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtType)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.LblTitle)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnFillGrid)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.TxtBankName)
        Me.Controls.Add(Me.PnlMain)
        Me.KeyPreview = True
        Me.Name = "FrmBankReconciliation"
        Me.ShowIcon = False
        Me.Tag = "BG"
        Me.Text = "Bank Reconciliation Entry"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TxtBankName As AgControls.AgTextBox
    Friend WithEvents BtnFillGrid As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents LblTitle As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtType As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtDate As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LblBG As System.Windows.Forms.Label
    Friend WithEvents Lblbalance As System.Windows.Forms.Label
    Friend WithEvents LblAmountnotReflected As System.Windows.Forms.Label
    Friend WithEvents Lblbank As System.Windows.Forms.Label
    Friend WithEvents LblCompanyBal As System.Windows.Forms.Label
    Friend WithEvents LblAmtNotClg_Dr As System.Windows.Forms.Label
    Friend WithEvents LblClgAmt As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtshowContra As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents LblAmtNotClg_Cr As System.Windows.Forms.Label
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
End Class
