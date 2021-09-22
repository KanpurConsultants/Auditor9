<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmImportDataSingleUI
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
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.Opn = New System.Windows.Forms.OpenFileDialog()
        Me.TPPurchaseInvoiceImport = New System.Windows.Forms.TabPage()
        Me.PnlPurch1 = New System.Windows.Forms.Panel()
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail = New System.Windows.Forms.Button()
        Me.TxtExcelPath_PurchInvoiceDimensionDetail = New AgControls.AgTextBox()
        Me.TxtExcelPath_PurchInvoiceDetail = New AgControls.AgTextBox()
        Me.TxtExcelPath_PurchInvoice = New AgControls.AgTextBox()
        Me.BtnSelectExcelFile_PurchInvoiceDetail = New System.Windows.Forms.Button()
        Me.BtnSelectExcelFile_PurchInvoice = New System.Windows.Forms.Button()
        Me.PnlPurch3 = New System.Windows.Forms.Panel()
        Me.PnlPurch2 = New System.Windows.Forms.Panel()
        Me.TPSaleInvoiceImport = New System.Windows.Forms.TabPage()
        Me.PnlSale1 = New System.Windows.Forms.Panel()
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail = New System.Windows.Forms.Button()
        Me.TxtExcelPath_SaleInvoiceDimensionDetail = New AgControls.AgTextBox()
        Me.TxtExcelPath_SaleInvoiceDetail = New AgControls.AgTextBox()
        Me.TxtExcelPath_SaleInvoice = New AgControls.AgTextBox()
        Me.BtnSelectExcelFile_SaleInvoiceDetail = New System.Windows.Forms.Button()
        Me.BtnSelectExcelFile_SaleInvoice = New System.Windows.Forms.Button()
        Me.PnlSale3 = New System.Windows.Forms.Panel()
        Me.PnlSale2 = New System.Windows.Forms.Panel()
        Me.TPParty = New System.Windows.Forms.TabPage()
        Me.PnlParty = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BtnSelectExcelFile_Party = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TxtExcelPath_Party = New AgControls.AgTextBox()
        Me.TPItemRateList = New System.Windows.Forms.TabPage()
        Me.PnlItemRateList = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnSelectExcelFile_ItemRateList = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TxtExcelPath_ItemRateList = New AgControls.AgTextBox()
        Me.TPItem = New System.Windows.Forms.TabPage()
        Me.PnlItem = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnSelectExcelFile_Item = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtExcelPath_Item = New AgControls.AgTextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TPPurchaseInvoiceImport.SuspendLayout()
        Me.TPSaleInvoiceImport.SuspendLayout()
        Me.TPParty.SuspendLayout()
        Me.TPItemRateList.SuspendLayout()
        Me.TPItem.SuspendLayout()
        Me.TabControl1.SuspendLayout()
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
        Me.GroupBox1.Size = New System.Drawing.Size(1005, 4)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(826, 589)
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
        Me.BtnCancel.Location = New System.Drawing.Point(898, 589)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(64, 23)
        Me.BtnCancel.TabIndex = 669
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'Opn
        '
        Me.Opn.FileName = "OpenFileDialog1"
        Me.Opn.Filter = "*.xlsx|*.Xlsx"
        '
        'TPPurchaseInvoiceImport
        '
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.PnlPurch1)
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail)
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.TxtExcelPath_PurchInvoiceDimensionDetail)
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.TxtExcelPath_PurchInvoiceDetail)
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.TxtExcelPath_PurchInvoice)
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.BtnSelectExcelFile_PurchInvoiceDetail)
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.BtnSelectExcelFile_PurchInvoice)
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.PnlPurch3)
        Me.TPPurchaseInvoiceImport.Controls.Add(Me.PnlPurch2)
        Me.TPPurchaseInvoiceImport.Location = New System.Drawing.Point(4, 25)
        Me.TPPurchaseInvoiceImport.Name = "TPPurchaseInvoiceImport"
        Me.TPPurchaseInvoiceImport.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPurchaseInvoiceImport.Size = New System.Drawing.Size(963, 550)
        Me.TPPurchaseInvoiceImport.TabIndex = 4
        Me.TPPurchaseInvoiceImport.Text = "Purchase"
        Me.TPPurchaseInvoiceImport.UseVisualStyleBackColor = True
        '
        'PnlPurch1
        '
        Me.PnlPurch1.Location = New System.Drawing.Point(2, 2)
        Me.PnlPurch1.Name = "PnlPurch1"
        Me.PnlPurch1.Size = New System.Drawing.Size(952, 184)
        Me.PnlPurch1.TabIndex = 700
        '
        'BtnSelectExcelFile_PurchInvoiceDimensionDetail
        '
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.Location = New System.Drawing.Point(927, 526)
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.Name = "BtnSelectExcelFile_PurchInvoiceDimensionDetail"
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.TabIndex = 708
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.Text = "..."
        Me.BtnSelectExcelFile_PurchInvoiceDimensionDetail.UseVisualStyleBackColor = True
        '
        'TxtExcelPath_PurchInvoiceDimensionDetail
        '
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgLastValueTag = Nothing
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgLastValueText = ""
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgMandatory = True
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgMasterHelp = True
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgNumberRightPlaces = 0
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgPickFromLastValue = False
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgRowFilter = ""
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgSelectedValue = Nothing
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.Location = New System.Drawing.Point(4, 528)
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.MaxLength = 50
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.Multiline = True
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.Name = "TxtExcelPath_PurchInvoiceDimensionDetail"
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.Size = New System.Drawing.Size(917, 20)
        Me.TxtExcelPath_PurchInvoiceDimensionDetail.TabIndex = 707
        '
        'TxtExcelPath_PurchInvoiceDetail
        '
        Me.TxtExcelPath_PurchInvoiceDetail.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_PurchInvoiceDetail.AgLastValueTag = Nothing
        Me.TxtExcelPath_PurchInvoiceDetail.AgLastValueText = ""
        Me.TxtExcelPath_PurchInvoiceDetail.AgMandatory = True
        Me.TxtExcelPath_PurchInvoiceDetail.AgMasterHelp = True
        Me.TxtExcelPath_PurchInvoiceDetail.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_PurchInvoiceDetail.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_PurchInvoiceDetail.AgNumberRightPlaces = 0
        Me.TxtExcelPath_PurchInvoiceDetail.AgPickFromLastValue = False
        Me.TxtExcelPath_PurchInvoiceDetail.AgRowFilter = ""
        Me.TxtExcelPath_PurchInvoiceDetail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_PurchInvoiceDetail.AgSelectedValue = Nothing
        Me.TxtExcelPath_PurchInvoiceDetail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_PurchInvoiceDetail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_PurchInvoiceDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_PurchInvoiceDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_PurchInvoiceDetail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_PurchInvoiceDetail.Location = New System.Drawing.Point(4, 406)
        Me.TxtExcelPath_PurchInvoiceDetail.MaxLength = 50
        Me.TxtExcelPath_PurchInvoiceDetail.Multiline = True
        Me.TxtExcelPath_PurchInvoiceDetail.Name = "TxtExcelPath_PurchInvoiceDetail"
        Me.TxtExcelPath_PurchInvoiceDetail.Size = New System.Drawing.Size(920, 20)
        Me.TxtExcelPath_PurchInvoiceDetail.TabIndex = 705
        '
        'TxtExcelPath_PurchInvoice
        '
        Me.TxtExcelPath_PurchInvoice.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_PurchInvoice.AgLastValueTag = Nothing
        Me.TxtExcelPath_PurchInvoice.AgLastValueText = ""
        Me.TxtExcelPath_PurchInvoice.AgMandatory = True
        Me.TxtExcelPath_PurchInvoice.AgMasterHelp = True
        Me.TxtExcelPath_PurchInvoice.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_PurchInvoice.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_PurchInvoice.AgNumberRightPlaces = 0
        Me.TxtExcelPath_PurchInvoice.AgPickFromLastValue = False
        Me.TxtExcelPath_PurchInvoice.AgRowFilter = ""
        Me.TxtExcelPath_PurchInvoice.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_PurchInvoice.AgSelectedValue = Nothing
        Me.TxtExcelPath_PurchInvoice.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_PurchInvoice.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_PurchInvoice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_PurchInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_PurchInvoice.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_PurchInvoice.Location = New System.Drawing.Point(4, 191)
        Me.TxtExcelPath_PurchInvoice.MaxLength = 50
        Me.TxtExcelPath_PurchInvoice.Multiline = True
        Me.TxtExcelPath_PurchInvoice.Name = "TxtExcelPath_PurchInvoice"
        Me.TxtExcelPath_PurchInvoice.Size = New System.Drawing.Size(920, 20)
        Me.TxtExcelPath_PurchInvoice.TabIndex = 703
        '
        'BtnSelectExcelFile_PurchInvoiceDetail
        '
        Me.BtnSelectExcelFile_PurchInvoiceDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_PurchInvoiceDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_PurchInvoiceDetail.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_PurchInvoiceDetail.Location = New System.Drawing.Point(930, 404)
        Me.BtnSelectExcelFile_PurchInvoiceDetail.Name = "BtnSelectExcelFile_PurchInvoiceDetail"
        Me.BtnSelectExcelFile_PurchInvoiceDetail.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_PurchInvoiceDetail.TabIndex = 706
        Me.BtnSelectExcelFile_PurchInvoiceDetail.Text = "..."
        Me.BtnSelectExcelFile_PurchInvoiceDetail.UseVisualStyleBackColor = True
        '
        'BtnSelectExcelFile_PurchInvoice
        '
        Me.BtnSelectExcelFile_PurchInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_PurchInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_PurchInvoice.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_PurchInvoice.Location = New System.Drawing.Point(929, 189)
        Me.BtnSelectExcelFile_PurchInvoice.Name = "BtnSelectExcelFile_PurchInvoice"
        Me.BtnSelectExcelFile_PurchInvoice.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_PurchInvoice.TabIndex = 704
        Me.BtnSelectExcelFile_PurchInvoice.Text = "..."
        Me.BtnSelectExcelFile_PurchInvoice.UseVisualStyleBackColor = True
        '
        'PnlPurch3
        '
        Me.PnlPurch3.Location = New System.Drawing.Point(2, 429)
        Me.PnlPurch3.Name = "PnlPurch3"
        Me.PnlPurch3.Size = New System.Drawing.Size(952, 96)
        Me.PnlPurch3.TabIndex = 702
        '
        'PnlPurch2
        '
        Me.PnlPurch2.Location = New System.Drawing.Point(3, 216)
        Me.PnlPurch2.Name = "PnlPurch2"
        Me.PnlPurch2.Size = New System.Drawing.Size(952, 186)
        Me.PnlPurch2.TabIndex = 701
        '
        'TPSaleInvoiceImport
        '
        Me.TPSaleInvoiceImport.Controls.Add(Me.PnlSale1)
        Me.TPSaleInvoiceImport.Controls.Add(Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail)
        Me.TPSaleInvoiceImport.Controls.Add(Me.TxtExcelPath_SaleInvoiceDimensionDetail)
        Me.TPSaleInvoiceImport.Controls.Add(Me.TxtExcelPath_SaleInvoiceDetail)
        Me.TPSaleInvoiceImport.Controls.Add(Me.TxtExcelPath_SaleInvoice)
        Me.TPSaleInvoiceImport.Controls.Add(Me.BtnSelectExcelFile_SaleInvoiceDetail)
        Me.TPSaleInvoiceImport.Controls.Add(Me.BtnSelectExcelFile_SaleInvoice)
        Me.TPSaleInvoiceImport.Controls.Add(Me.PnlSale3)
        Me.TPSaleInvoiceImport.Controls.Add(Me.PnlSale2)
        Me.TPSaleInvoiceImport.Location = New System.Drawing.Point(4, 25)
        Me.TPSaleInvoiceImport.Name = "TPSaleInvoiceImport"
        Me.TPSaleInvoiceImport.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSaleInvoiceImport.Size = New System.Drawing.Size(963, 550)
        Me.TPSaleInvoiceImport.TabIndex = 3
        Me.TPSaleInvoiceImport.Text = "Sale"
        Me.TPSaleInvoiceImport.UseVisualStyleBackColor = True
        '
        'PnlSale1
        '
        Me.PnlSale1.Location = New System.Drawing.Point(1, 4)
        Me.PnlSale1.Name = "PnlSale1"
        Me.PnlSale1.Size = New System.Drawing.Size(952, 184)
        Me.PnlSale1.TabIndex = 687
        '
        'BtnSelectExcelFile_SaleInvoiceDimensionDetail
        '
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Location = New System.Drawing.Point(926, 528)
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Name = "BtnSelectExcelFile_SaleInvoiceDimensionDetail"
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.TabIndex = 699
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.Text = "..."
        Me.BtnSelectExcelFile_SaleInvoiceDimensionDetail.UseVisualStyleBackColor = True
        '
        'TxtExcelPath_SaleInvoiceDimensionDetail
        '
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgLastValueTag = Nothing
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.AgLastValueText = ""
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
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Location = New System.Drawing.Point(3, 530)
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.MaxLength = 50
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Multiline = True
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Name = "TxtExcelPath_SaleInvoiceDimensionDetail"
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.Size = New System.Drawing.Size(917, 20)
        Me.TxtExcelPath_SaleInvoiceDimensionDetail.TabIndex = 698
        '
        'TxtExcelPath_SaleInvoiceDetail
        '
        Me.TxtExcelPath_SaleInvoiceDetail.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_SaleInvoiceDetail.AgLastValueTag = Nothing
        Me.TxtExcelPath_SaleInvoiceDetail.AgLastValueText = ""
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
        Me.TxtExcelPath_SaleInvoiceDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_SaleInvoiceDetail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_SaleInvoiceDetail.Location = New System.Drawing.Point(3, 408)
        Me.TxtExcelPath_SaleInvoiceDetail.MaxLength = 50
        Me.TxtExcelPath_SaleInvoiceDetail.Multiline = True
        Me.TxtExcelPath_SaleInvoiceDetail.Name = "TxtExcelPath_SaleInvoiceDetail"
        Me.TxtExcelPath_SaleInvoiceDetail.Size = New System.Drawing.Size(920, 20)
        Me.TxtExcelPath_SaleInvoiceDetail.TabIndex = 695
        '
        'TxtExcelPath_SaleInvoice
        '
        Me.TxtExcelPath_SaleInvoice.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_SaleInvoice.AgLastValueTag = Nothing
        Me.TxtExcelPath_SaleInvoice.AgLastValueText = ""
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
        Me.TxtExcelPath_SaleInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_SaleInvoice.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_SaleInvoice.Location = New System.Drawing.Point(3, 193)
        Me.TxtExcelPath_SaleInvoice.MaxLength = 50
        Me.TxtExcelPath_SaleInvoice.Multiline = True
        Me.TxtExcelPath_SaleInvoice.Name = "TxtExcelPath_SaleInvoice"
        Me.TxtExcelPath_SaleInvoice.Size = New System.Drawing.Size(920, 20)
        Me.TxtExcelPath_SaleInvoice.TabIndex = 691
        '
        'BtnSelectExcelFile_SaleInvoiceDetail
        '
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_SaleInvoiceDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Location = New System.Drawing.Point(929, 406)
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Name = "BtnSelectExcelFile_SaleInvoiceDetail"
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_SaleInvoiceDetail.TabIndex = 696
        Me.BtnSelectExcelFile_SaleInvoiceDetail.Text = "..."
        Me.BtnSelectExcelFile_SaleInvoiceDetail.UseVisualStyleBackColor = True
        '
        'BtnSelectExcelFile_SaleInvoice
        '
        Me.BtnSelectExcelFile_SaleInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_SaleInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_SaleInvoice.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_SaleInvoice.Location = New System.Drawing.Point(928, 191)
        Me.BtnSelectExcelFile_SaleInvoice.Name = "BtnSelectExcelFile_SaleInvoice"
        Me.BtnSelectExcelFile_SaleInvoice.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_SaleInvoice.TabIndex = 692
        Me.BtnSelectExcelFile_SaleInvoice.Text = "..."
        Me.BtnSelectExcelFile_SaleInvoice.UseVisualStyleBackColor = True
        '
        'PnlSale3
        '
        Me.PnlSale3.Location = New System.Drawing.Point(1, 431)
        Me.PnlSale3.Name = "PnlSale3"
        Me.PnlSale3.Size = New System.Drawing.Size(952, 96)
        Me.PnlSale3.TabIndex = 689
        '
        'PnlSale2
        '
        Me.PnlSale2.Location = New System.Drawing.Point(2, 218)
        Me.PnlSale2.Name = "PnlSale2"
        Me.PnlSale2.Size = New System.Drawing.Size(952, 186)
        Me.PnlSale2.TabIndex = 688
        '
        'TPParty
        '
        Me.TPParty.Controls.Add(Me.PnlParty)
        Me.TPParty.Controls.Add(Me.Label5)
        Me.TPParty.Controls.Add(Me.BtnSelectExcelFile_Party)
        Me.TPParty.Controls.Add(Me.Label6)
        Me.TPParty.Controls.Add(Me.GroupBox4)
        Me.TPParty.Controls.Add(Me.TxtExcelPath_Party)
        Me.TPParty.Location = New System.Drawing.Point(4, 25)
        Me.TPParty.Name = "TPParty"
        Me.TPParty.Padding = New System.Windows.Forms.Padding(3)
        Me.TPParty.Size = New System.Drawing.Size(963, 550)
        Me.TPParty.TabIndex = 1
        Me.TPParty.Text = "Party"
        Me.TPParty.UseVisualStyleBackColor = True
        '
        'PnlParty
        '
        Me.PnlParty.Location = New System.Drawing.Point(6, 4)
        Me.PnlParty.Name = "PnlParty"
        Me.PnlParty.Size = New System.Drawing.Size(952, 471)
        Me.PnlParty.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(7, 490)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 29)
        Me.Label5.TabIndex = 675
        Me.Label5.Text = "Select Excel File"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnSelectExcelFile_Party
        '
        Me.BtnSelectExcelFile_Party.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_Party.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_Party.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_Party.Location = New System.Drawing.Point(924, 522)
        Me.BtnSelectExcelFile_Party.Name = "BtnSelectExcelFile_Party"
        Me.BtnSelectExcelFile_Party.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_Party.TabIndex = 674
        Me.BtnSelectExcelFile_Party.Text = "..."
        Me.BtnSelectExcelFile_Party.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Location = New System.Drawing.Point(11, 494)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 29)
        Me.Label6.TabIndex = 676
        Me.Label6.Text = "Label6"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.Location = New System.Drawing.Point(4, 486)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(955, 2)
        Me.GroupBox4.TabIndex = 672
        Me.GroupBox4.TabStop = False
        '
        'TxtExcelPath_Party
        '
        Me.TxtExcelPath_Party.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_Party.AgLastValueTag = Nothing
        Me.TxtExcelPath_Party.AgLastValueText = ""
        Me.TxtExcelPath_Party.AgMandatory = True
        Me.TxtExcelPath_Party.AgMasterHelp = True
        Me.TxtExcelPath_Party.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_Party.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_Party.AgNumberRightPlaces = 0
        Me.TxtExcelPath_Party.AgPickFromLastValue = False
        Me.TxtExcelPath_Party.AgRowFilter = ""
        Me.TxtExcelPath_Party.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_Party.AgSelectedValue = Nothing
        Me.TxtExcelPath_Party.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_Party.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_Party.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_Party.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_Party.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_Party.Location = New System.Drawing.Point(7, 523)
        Me.TxtExcelPath_Party.MaxLength = 50
        Me.TxtExcelPath_Party.Multiline = True
        Me.TxtExcelPath_Party.Name = "TxtExcelPath_Party"
        Me.TxtExcelPath_Party.Size = New System.Drawing.Size(911, 20)
        Me.TxtExcelPath_Party.TabIndex = 673
        '
        'TPItemRateList
        '
        Me.TPItemRateList.Controls.Add(Me.PnlItemRateList)
        Me.TPItemRateList.Controls.Add(Me.Label1)
        Me.TPItemRateList.Controls.Add(Me.BtnSelectExcelFile_ItemRateList)
        Me.TPItemRateList.Controls.Add(Me.Label4)
        Me.TPItemRateList.Controls.Add(Me.GroupBox3)
        Me.TPItemRateList.Controls.Add(Me.TxtExcelPath_ItemRateList)
        Me.TPItemRateList.Location = New System.Drawing.Point(4, 25)
        Me.TPItemRateList.Name = "TPItemRateList"
        Me.TPItemRateList.Padding = New System.Windows.Forms.Padding(3)
        Me.TPItemRateList.Size = New System.Drawing.Size(963, 550)
        Me.TPItemRateList.TabIndex = 2
        Me.TPItemRateList.Text = "Rate List"
        Me.TPItemRateList.UseVisualStyleBackColor = True
        '
        'PnlItemRateList
        '
        Me.PnlItemRateList.Location = New System.Drawing.Point(6, 3)
        Me.PnlItemRateList.Name = "PnlItemRateList"
        Me.PnlItemRateList.Size = New System.Drawing.Size(952, 471)
        Me.PnlItemRateList.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(6, 491)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 29)
        Me.Label1.TabIndex = 675
        Me.Label1.Text = "Select Excel File"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnSelectExcelFile_ItemRateList
        '
        Me.BtnSelectExcelFile_ItemRateList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_ItemRateList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_ItemRateList.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_ItemRateList.Location = New System.Drawing.Point(923, 523)
        Me.BtnSelectExcelFile_ItemRateList.Name = "BtnSelectExcelFile_ItemRateList"
        Me.BtnSelectExcelFile_ItemRateList.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_ItemRateList.TabIndex = 674
        Me.BtnSelectExcelFile_ItemRateList.Text = "..."
        Me.BtnSelectExcelFile_ItemRateList.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(10, 495)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 29)
        Me.Label4.TabIndex = 676
        Me.Label4.Text = "Label4"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Location = New System.Drawing.Point(3, 487)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(955, 2)
        Me.GroupBox3.TabIndex = 672
        Me.GroupBox3.TabStop = False
        '
        'TxtExcelPath_ItemRateList
        '
        Me.TxtExcelPath_ItemRateList.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_ItemRateList.AgLastValueTag = Nothing
        Me.TxtExcelPath_ItemRateList.AgLastValueText = ""
        Me.TxtExcelPath_ItemRateList.AgMandatory = True
        Me.TxtExcelPath_ItemRateList.AgMasterHelp = True
        Me.TxtExcelPath_ItemRateList.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_ItemRateList.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_ItemRateList.AgNumberRightPlaces = 0
        Me.TxtExcelPath_ItemRateList.AgPickFromLastValue = False
        Me.TxtExcelPath_ItemRateList.AgRowFilter = ""
        Me.TxtExcelPath_ItemRateList.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_ItemRateList.AgSelectedValue = Nothing
        Me.TxtExcelPath_ItemRateList.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_ItemRateList.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_ItemRateList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_ItemRateList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_ItemRateList.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_ItemRateList.Location = New System.Drawing.Point(6, 524)
        Me.TxtExcelPath_ItemRateList.MaxLength = 50
        Me.TxtExcelPath_ItemRateList.Multiline = True
        Me.TxtExcelPath_ItemRateList.Name = "TxtExcelPath_ItemRateList"
        Me.TxtExcelPath_ItemRateList.Size = New System.Drawing.Size(911, 20)
        Me.TxtExcelPath_ItemRateList.TabIndex = 673
        '
        'TPItem
        '
        Me.TPItem.Controls.Add(Me.PnlItem)
        Me.TPItem.Controls.Add(Me.Label2)
        Me.TPItem.Controls.Add(Me.GroupBox2)
        Me.TPItem.Controls.Add(Me.BtnSelectExcelFile_Item)
        Me.TPItem.Controls.Add(Me.Label3)
        Me.TPItem.Controls.Add(Me.TxtExcelPath_Item)
        Me.TPItem.Location = New System.Drawing.Point(4, 25)
        Me.TPItem.Name = "TPItem"
        Me.TPItem.Padding = New System.Windows.Forms.Padding(3)
        Me.TPItem.Size = New System.Drawing.Size(963, 550)
        Me.TPItem.TabIndex = 0
        Me.TPItem.Text = "Item"
        Me.TPItem.UseVisualStyleBackColor = True
        '
        'PnlItem
        '
        Me.PnlItem.Location = New System.Drawing.Point(6, 3)
        Me.PnlItem.Name = "PnlItem"
        Me.PnlItem.Size = New System.Drawing.Size(952, 471)
        Me.PnlItem.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(9, 491)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 29)
        Me.Label2.TabIndex = 670
        Me.Label2.Text = "Select Excel File"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(6, 487)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(955, 2)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'BtnSelectExcelFile_Item
        '
        Me.BtnSelectExcelFile_Item.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile_Item.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile_Item.Location = New System.Drawing.Point(926, 523)
        Me.BtnSelectExcelFile_Item.Name = "BtnSelectExcelFile_Item"
        Me.BtnSelectExcelFile_Item.Size = New System.Drawing.Size(31, 22)
        Me.BtnSelectExcelFile_Item.TabIndex = 667
        Me.BtnSelectExcelFile_Item.Text = "..."
        Me.BtnSelectExcelFile_Item.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(13, 495)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 29)
        Me.Label3.TabIndex = 671
        Me.Label3.Text = "Label3"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtExcelPath_Item
        '
        Me.TxtExcelPath_Item.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath_Item.AgLastValueTag = Nothing
        Me.TxtExcelPath_Item.AgLastValueText = ""
        Me.TxtExcelPath_Item.AgMandatory = True
        Me.TxtExcelPath_Item.AgMasterHelp = True
        Me.TxtExcelPath_Item.AgNumberLeftPlaces = 0
        Me.TxtExcelPath_Item.AgNumberNegetiveAllow = False
        Me.TxtExcelPath_Item.AgNumberRightPlaces = 0
        Me.TxtExcelPath_Item.AgPickFromLastValue = False
        Me.TxtExcelPath_Item.AgRowFilter = ""
        Me.TxtExcelPath_Item.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath_Item.AgSelectedValue = Nothing
        Me.TxtExcelPath_Item.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath_Item.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath_Item.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath_Item.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExcelPath_Item.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath_Item.Location = New System.Drawing.Point(9, 524)
        Me.TxtExcelPath_Item.MaxLength = 50
        Me.TxtExcelPath_Item.Multiline = True
        Me.TxtExcelPath_Item.Name = "TxtExcelPath_Item"
        Me.TxtExcelPath_Item.Size = New System.Drawing.Size(911, 20)
        Me.TxtExcelPath_Item.TabIndex = 666
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TPItem)
        Me.TabControl1.Controls.Add(Me.TPItemRateList)
        Me.TabControl1.Controls.Add(Me.TPParty)
        Me.TabControl1.Controls.Add(Me.TPSaleInvoiceImport)
        Me.TabControl1.Controls.Add(Me.TPPurchaseInvoiceImport)
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(1, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(971, 579)
        Me.TabControl1.TabIndex = 672
        '
        'FrmImportDataSingleUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 616)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmImportDataSingleUI"
        Me.Text = "Import From Excel"
        Me.TPPurchaseInvoiceImport.ResumeLayout(False)
        Me.TPPurchaseInvoiceImport.PerformLayout()
        Me.TPSaleInvoiceImport.ResumeLayout(False)
        Me.TPSaleInvoiceImport.PerformLayout()
        Me.TPParty.ResumeLayout(False)
        Me.TPParty.PerformLayout()
        Me.TPItemRateList.ResumeLayout(False)
        Me.TPItemRateList.PerformLayout()
        Me.TPItem.ResumeLayout(False)
        Me.TPItem.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents BtnOK As System.Windows.Forms.Button
    Public WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents Opn As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TPPurchaseInvoiceImport As TabPage
    Public WithEvents BtnSelectExcelFile_PurchInvoiceDimensionDetail As Button
    Public WithEvents TxtExcelPath_PurchInvoiceDimensionDetail As AgControls.AgTextBox
    Public WithEvents TxtExcelPath_PurchInvoiceDetail As AgControls.AgTextBox
    Public WithEvents TxtExcelPath_PurchInvoice As AgControls.AgTextBox
    Public WithEvents BtnSelectExcelFile_PurchInvoiceDetail As Button
    Public WithEvents BtnSelectExcelFile_PurchInvoice As Button
    Public WithEvents PnlPurch3 As Panel
    Public WithEvents PnlPurch2 As Panel
    Public WithEvents PnlPurch1 As Panel
    Friend WithEvents TPSaleInvoiceImport As TabPage
    Public WithEvents BtnSelectExcelFile_SaleInvoiceDimensionDetail As Button
    Public WithEvents TxtExcelPath_SaleInvoiceDimensionDetail As AgControls.AgTextBox
    Public WithEvents TxtExcelPath_SaleInvoiceDetail As AgControls.AgTextBox
    Public WithEvents TxtExcelPath_SaleInvoice As AgControls.AgTextBox
    Public WithEvents BtnSelectExcelFile_SaleInvoiceDetail As Button
    Public WithEvents BtnSelectExcelFile_SaleInvoice As Button
    Public WithEvents PnlSale3 As Panel
    Public WithEvents PnlSale2 As Panel
    Public WithEvents PnlSale1 As Panel
    Friend WithEvents TPParty As TabPage
    Public WithEvents Label5 As Label
    Public WithEvents BtnSelectExcelFile_Party As Button
    Public WithEvents Label6 As Label
    Public WithEvents GroupBox4 As GroupBox
    Public WithEvents TxtExcelPath_Party As AgControls.AgTextBox
    Public WithEvents PnlParty As Panel
    Friend WithEvents TPItemRateList As TabPage
    Public WithEvents Label1 As Label
    Public WithEvents BtnSelectExcelFile_ItemRateList As Button
    Public WithEvents Label4 As Label
    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents TxtExcelPath_ItemRateList As AgControls.AgTextBox
    Public WithEvents PnlItemRateList As Panel
    Friend WithEvents TPItem As TabPage
    Public WithEvents PnlItem As Panel
    Public WithEvents Label2 As Label
    Public WithEvents GroupBox2 As GroupBox
    Public WithEvents BtnSelectExcelFile_Item As Button
    Public WithEvents Label3 As Label
    Public WithEvents TxtExcelPath_Item As AgControls.AgTextBox
    Friend WithEvents TabControl1 As TabControl
End Class
