<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmImportPurchaseFromExcel
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.TxtExcelPath_File1 = New AgControls.AgTextBox()
        Me.BtnSelectExcelFile_File1 = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Opn = New System.Windows.Forms.OpenFileDialog()
        Me.BtnSelectExcelFile_File2 = New System.Windows.Forms.Button()
        Me.TxtExcelPath_File2 = New AgControls.AgTextBox()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(0, 579)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(0, 30)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(595, 213)
        Me.Pnl1.TabIndex = 10
        '
        'TxtExcelPath_File1
        '
        Me.TxtExcelPath_File1.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_File1.AgLastValueTag = Nothing
        Me.TxtExcelPath_File1.AgLastValueText = Nothing
        Me.TxtExcelPath_File1.AgMandatory = True
        Me.TxtExcelPath_File1.AgMasterHelp = True
        Me.TxtExcelPath_File1.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_File1.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_File1.AgNumberRightPlaces = 0
        Me.TxtExcelPath_File1.AgPickFromLastValue = False
        Me.TxtExcelPath_File1.AgRowFilter = ""
        Me.TxtExcelPath_File1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_File1.AgSelectedValue = Nothing
        Me.TxtExcelPath_File1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_File1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_File1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_File1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExcelPath_File1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_File1.Location = New System.Drawing.Point(0, 271)
        Me.TxtExcelPath_File1.MaxLength = 50
        Me.TxtExcelPath_File1.Multiline = True
        Me.TxtExcelPath_File1.Name = "TxtExcelPath_File1"
        Me.TxtExcelPath_File1.Size = New System.Drawing.Size(557, 20)
        Me.TxtExcelPath_File1.TabIndex = 666
        '
        'BtnSelectExcelFile_File1
        '
        Me.BtnSelectExcelFile_File1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_File1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_File1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_File1.Location = New System.Drawing.Point(563, 269)
        Me.BtnSelectExcelFile_File1.Name = "BtnSelectExcelFile_File1"
        Me.BtnSelectExcelFile_File1.Size = New System.Drawing.Size(31, 23)
        Me.BtnSelectExcelFile_File1.TabIndex = 667
        Me.BtnSelectExcelFile_File1.Text = "..."
        Me.BtnSelectExcelFile_File1.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(448, 589)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(64, 23)
        Me.BtnOK.TabIndex = 668
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(520, 589)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(64, 23)
        Me.BtnCancel.TabIndex = 669
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(-3, 255)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(0, 245)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 23)
        Me.Label2.TabIndex = 670
        Me.Label2.Text = "Select Excel File"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(4, 249)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 23)
        Me.Label3.TabIndex = 671
        Me.Label3.Text = "Label3"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(-3, -1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 23)
        Me.Label4.TabIndex = 672
        Me.Label4.Text = "Excel File Format"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(4, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 23)
        Me.Label5.TabIndex = 673
        Me.Label5.Text = "Label5"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Location = New System.Drawing.Point(81, 255)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(542, 4)
        Me.GroupBox3.TabIndex = 674
        Me.GroupBox3.TabStop = False
        '
        'Opn
        '
        Me.Opn.FileName = "OpenFileDialog1"
        Me.Opn.Filter = "*.xlsx|*.Xlsx"
        '
        'BtnSelectExcelFile_File2
        '
        Me.BtnSelectExcelFile_File2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_File2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_File2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_File2.Location = New System.Drawing.Point(564, 554)
        Me.BtnSelectExcelFile_File2.Name = "BtnSelectExcelFile_File2"
        Me.BtnSelectExcelFile_File2.Size = New System.Drawing.Size(31, 23)
        Me.BtnSelectExcelFile_File2.TabIndex = 676
        Me.BtnSelectExcelFile_File2.Text = "..."
        Me.BtnSelectExcelFile_File2.UseVisualStyleBackColor = True
        '
        'TxtExcelPath_File2
        '
        Me.TxtExcelPath_File2.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_File2.AgLastValueTag = Nothing
        Me.TxtExcelPath_File2.AgLastValueText = Nothing
        Me.TxtExcelPath_File2.AgMandatory = True
        Me.TxtExcelPath_File2.AgMasterHelp = True
        Me.TxtExcelPath_File2.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_File2.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_File2.AgNumberRightPlaces = 0
        Me.TxtExcelPath_File2.AgPickFromLastValue = False
        Me.TxtExcelPath_File2.AgRowFilter = ""
        Me.TxtExcelPath_File2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_File2.AgSelectedValue = Nothing
        Me.TxtExcelPath_File2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_File2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_File2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_File2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExcelPath_File2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_File2.Location = New System.Drawing.Point(1, 556)
        Me.TxtExcelPath_File2.MaxLength = 50
        Me.TxtExcelPath_File2.Multiline = True
        Me.TxtExcelPath_File2.Name = "TxtExcelPath_File2"
        Me.TxtExcelPath_File2.Size = New System.Drawing.Size(557, 20)
        Me.TxtExcelPath_File2.TabIndex = 675
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(1, 296)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(595, 234)
        Me.Pnl2.TabIndex = 679
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(-12, 531)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 23)
        Me.Label1.TabIndex = 681
        Me.Label1.Text = "Select Excel File"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Location = New System.Drawing.Point(-8, 535)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 23)
        Me.Label6.TabIndex = 682
        Me.Label6.Text = "Label6"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.Location = New System.Drawing.Point(-15, 542)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox4.TabIndex = 680
        Me.GroupBox4.TabStop = False
        '
        'FrmImportPurchaseFromExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(596, 616)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.BtnSelectExcelFile_File2)
        Me.Controls.Add(Me.TxtExcelPath_File2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnSelectExcelFile_File1)
        Me.Controls.Add(Me.TxtExcelPath_File1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmImportPurchaseFromExcel"
        Me.Text = "Import From Excel"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents TxtExcelPath_File1 As AgControls.AgTextBox
    Public WithEvents BtnSelectExcelFile_File1 As System.Windows.Forms.Button
    Public WithEvents BtnOK As System.Windows.Forms.Button
    Public WithEvents BtnCancel As System.Windows.Forms.Button
    Public WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Opn As System.Windows.Forms.OpenFileDialog
    Public WithEvents BtnSelectExcelFile_File2 As Button
    Public WithEvents TxtExcelPath_File2 As AgControls.AgTextBox
    Public WithEvents Pnl2 As Panel
    Public WithEvents Label1 As Label
    Public WithEvents Label6 As Label
    Public WithEvents GroupBox4 As GroupBox
End Class
