<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmImportSaleFromExcel
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
        Me.TxtExcelPath_SaleInvoice = New AgControls.AgTextBox()
        Me.BtnSelectExcelFile_SaleInvoice = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Opn = New System.Windows.Forms.OpenFileDialog()
        Me.BtnSelectExcelFile_SaleInvoiceDetail = New System.Windows.Forms.Button()
        Me.TxtExcelPath_SaleInvoiceDetail = New AgControls.AgTextBox()
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail = New System.Windows.Forms.Button()
        Me.TxtExcelPath_SaleInvoiceDimensionDetail = New AgControls.AgTextBox()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Pnl3 = New System.Windows.Forms.Panel()
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
        Me.Pnl1.Size = New System.Drawing.Size(596, 159)
        Me.Pnl1.TabIndex = 10
        '
        'TxtExcelPath_SaleInvoice
        '
        Me.TxtExcelPath_SaleInvoice.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_SaleInvoice.AgLastValueTag = Nothing
        Me.TxtExcelPath_SaleInvoice.AgLastValueText = Nothing
        Me.TxtExcelPath_SaleInvoice.AgMandatory = True
        Me.TxtExcelPath_SaleInvoice.AgMasterHelp = True
        Me.TxtExcelPath_SaleInvoice.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_SaleInvoice.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_SaleInvoice.AgNumberRightPlaces = 0
        Me.TxtExcelPath_SaleInvoice.AgPickFromLastValue = False
        Me.TxtExcelPath_SaleInvoice.AgRowFilter = ""
        Me.TxtExcelPath_SaleInvoice.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_SaleInvoice.AgSelectedValue = Nothing
        Me.TxtExcelPath_SaleInvoice.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_SaleInvoice.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_SaleInvoice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_SaleInvoice.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExcelPath_SaleInvoice.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_SaleInvoice.Location = New System.Drawing.Point(0, 218)
        Me.TxtExcelPath_SaleInvoice.MaxLength = 50
        Me.TxtExcelPath_SaleInvoice.Multiline = True
        Me.TxtExcelPath_SaleInvoice.Name = "TxtExcelPath_SaleInvoice"
        Me.TxtExcelPath_SaleInvoice.Size = New System.Drawing.Size(557, 20)
        Me.TxtExcelPath_SaleInvoice.TabIndex = 666
        '
        'BtnSelectExcelFile_SaleInvoice
        '
        Me.BtnSelectExcelFile_SaleInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_SaleInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_SaleInvoice.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_SaleInvoice.Location = New System.Drawing.Point(563, 216)
        Me.BtnSelectExcelFile_SaleInvoice.Name = "BtnSelectExcelFile_SaleInvoice"
        Me.BtnSelectExcelFile_SaleInvoice.Size = New System.Drawing.Size(31, 23)
        Me.BtnSelectExcelFile_SaleInvoice.TabIndex = 667
        Me.BtnSelectExcelFile_SaleInvoice.Text = "..."
        Me.BtnSelectExcelFile_SaleInvoice.UseVisualStyleBackColor = True
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
        Me.GroupBox2.Location = New System.Drawing.Point(-3, 202)
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
        Me.Label2.Location = New System.Drawing.Point(0, 192)
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
        Me.Label3.Location = New System.Drawing.Point(4, 196)
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
        Me.GroupBox3.Location = New System.Drawing.Point(81, 191)
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
        'BtnSelectExcelFile_SaleInvoiceDetail
        '
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_SaleInvoiceDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Location = New System.Drawing.Point(564, 427)
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Name = "BtnSelectExcelFile_SaleInvoiceDetail"
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Size = New System.Drawing.Size(31, 23)
        Me.BtnSelectExcelFile_SaleInvoiceDetail.TabIndex = 676
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Text = "..."
        Me.BtnSelectExcelFile_SaleInvoiceDetail.UseVisualStyleBackColor = True
        '
        'TxtExcelPath_SaleInvoiceDetail
        '
        Me.TxtExcelPath_SaleInvoiceDetail.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_SaleInvoiceDetail.AgLastValueTag = Nothing
        Me.TxtExcelPath_SaleInvoiceDetail.AgLastValueText = Nothing
        Me.TxtExcelPath_SaleInvoiceDetail.AgMandatory = True
        Me.TxtExcelPath_SaleInvoiceDetail.AgMasterHelp = True
        Me.TxtExcelPath_SaleInvoiceDetail.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_SaleInvoiceDetail.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_SaleInvoiceDetail.AgNumberRightPlaces = 0
        Me.TxtExcelPath_SaleInvoiceDetail.AgPickFromLastValue = False
        Me.TxtExcelPath_SaleInvoiceDetail.AgRowFilter = ""
        Me.TxtExcelPath_SaleInvoiceDetail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_SaleInvoiceDetail.AgSelectedValue = Nothing
        Me.TxtExcelPath_SaleInvoiceDetail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_SaleInvoiceDetail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_SaleInvoiceDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_SaleInvoiceDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExcelPath_SaleInvoiceDetail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_SaleInvoiceDetail.Location = New System.Drawing.Point(1, 429)
        Me.TxtExcelPath_SaleInvoiceDetail.MaxLength = 50
        Me.TxtExcelPath_SaleInvoiceDetail.Multiline = True
        Me.TxtExcelPath_SaleInvoiceDetail.Name = "TxtExcelPath_SaleInvoiceDetail"
        Me.TxtExcelPath_SaleInvoiceDetail.Size = New System.Drawing.Size(557, 20)
        Me.TxtExcelPath_SaleInvoiceDetail.TabIndex = 675
        '
        'BtnSelectExcelFile_SaleInvoiceDimensionDetail
        '
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Location = New System.Drawing.Point(563, 555)
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Name = "BtnSelectExcelFile_SaleInvoiceDimensionDetail"
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Size = New System.Drawing.Size(31, 23)
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.TabIndex = 678
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Text = "..."
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.UseVisualStyleBackColor = True
        '
        'TxtExcelPath_SaleInvoiceDimensionDetail
        '
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgLastValueTag = Nothing
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgLastValueText = Nothing
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgMandatory = True
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgMasterHelp = True
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgNumberRightPlaces = 0
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgPickFromLastValue = False
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgRowFilter = ""
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgSelectedValue = Nothing
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Location = New System.Drawing.Point(0, 557)
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.MaxLength = 50
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Multiline = True
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Name = "TxtExcelPath_SaleInvoiceDimensionDetail"
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Size = New System.Drawing.Size(557, 20)
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.TabIndex = 677
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(1, 244)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(596, 159)
        Me.Pnl2.TabIndex = 679
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(-12, 404)
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
        Me.Label6.Location = New System.Drawing.Point(-8, 408)
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
        Me.GroupBox4.Location = New System.Drawing.Point(-15, 415)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox4.TabIndex = 680
        Me.GroupBox4.TabStop = False
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label7.Location = New System.Drawing.Point(-12, 536)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 23)
        Me.Label7.TabIndex = 684
        Me.Label7.Text = "Select Excel File"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(-8, 540)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 23)
        Me.Label8.TabIndex = 685
        Me.Label8.Text = "Label8"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox5.Location = New System.Drawing.Point(-15, 546)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox5.TabIndex = 683
        Me.GroupBox5.TabStop = False
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(0, 457)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(596, 80)
        Me.Pnl3.TabIndex = 686
        '
        'FrmImportSaleFromExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(596, 616)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail)
        Me.Controls.Add(Me.TxtExcelPath_SaleInvoiceDimensionDetail)
        Me.Controls.Add(Me.BtnSelectExcelFile_SaleInvoiceDetail)
        Me.Controls.Add(Me.TxtExcelPath_SaleInvoiceDetail)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnSelectExcelFile_SaleInvoice)
        Me.Controls.Add(Me.TxtExcelPath_SaleInvoice)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmImportSaleFromExcel"
        Me.Text = "Import From Excel"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents TxtExcelPath_SaleInvoice As AgControls.AgTextBox
    Public WithEvents BtnSelectExcelFile_SaleInvoice As System.Windows.Forms.Button
    Public WithEvents BtnOK As System.Windows.Forms.Button
    Public WithEvents BtnCancel As System.Windows.Forms.Button
    Public WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Opn As System.Windows.Forms.OpenFileDialog
    Public WithEvents BtnSelectExcelFile_SaleInvoiceDetail As Button
    Public WithEvents TxtExcelPath_SaleInvoiceDetail As AgControls.AgTextBox
    Public WithEvents BtnSelectExcelFile_SaleInvoiceDimensionDetail As Button
    Public WithEvents TxtExcelPath_SaleInvoiceDimensionDetail As AgControls.AgTextBox
    Public WithEvents Pnl2 As Panel
    Public WithEvents Label1 As Label
    Public WithEvents Label6 As Label
    Public WithEvents GroupBox4 As GroupBox
    Public WithEvents Label7 As Label
    Public WithEvents Label8 As Label
    Public WithEvents GroupBox5 As GroupBox
    Public WithEvents Pnl3 As Panel
End Class
