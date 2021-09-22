<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSaleInvoiceW_OnlyW
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
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.LblSaleToParty = New System.Windows.Forms.Label()
        Me.LblOrderNo = New System.Windows.Forms.Label()
        Me.TxtOrderNo = New AgControls.AgTextBox()
        Me.TxtPartyName = New AgControls.AgTextBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.TxtRemark = New AgControls.AgTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.Pnl3 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnFind = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.TxtTag = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BtnPrintW = New System.Windows.Forms.Button()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.BtnTransportDetail = New System.Windows.Forms.Button()
        Me.TxtSaleOrderDocId_W = New AgControls.AgTextBox()
        Me.BtnApprove = New System.Windows.Forms.Button()
        Me.LblApproveBy = New System.Windows.Forms.Label()
        Me.BtnAddItem = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnFetchTransporterDetail = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(10, 75)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(962, 133)
        Me.Pnl1.TabIndex = 2
        '
        'LblSaleToParty
        '
        Me.LblSaleToParty.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblSaleToParty.AutoSize = True
        Me.LblSaleToParty.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSaleToParty.Location = New System.Drawing.Point(375, 12)
        Me.LblSaleToParty.Name = "LblSaleToParty"
        Me.LblSaleToParty.Size = New System.Drawing.Size(83, 16)
        Me.LblSaleToParty.TabIndex = 13
        Me.LblSaleToParty.Text = "Party Name"
        '
        'LblOrderNo
        '
        Me.LblOrderNo.AutoSize = True
        Me.LblOrderNo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOrderNo.Location = New System.Drawing.Point(4, 12)
        Me.LblOrderNo.Name = "LblOrderNo"
        Me.LblOrderNo.Size = New System.Drawing.Size(65, 16)
        Me.LblOrderNo.TabIndex = 11
        Me.LblOrderNo.Text = "Order No"
        '
        'TxtOrderNo
        '
        Me.TxtOrderNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtOrderNo.AgLastValueTag = Nothing
        Me.TxtOrderNo.AgLastValueText = Nothing
        Me.TxtOrderNo.AgMandatory = False
        Me.TxtOrderNo.AgMasterHelp = False
        Me.TxtOrderNo.AgNumberLeftPlaces = 8
        Me.TxtOrderNo.AgNumberNegetiveAllow = False
        Me.TxtOrderNo.AgNumberRightPlaces = 2
        Me.TxtOrderNo.AgPickFromLastValue = False
        Me.TxtOrderNo.AgRowFilter = ""
        Me.TxtOrderNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOrderNo.AgSelectedValue = Nothing
        Me.TxtOrderNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOrderNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOrderNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOrderNo.Location = New System.Drawing.Point(75, 12)
        Me.TxtOrderNo.MaxLength = 20
        Me.TxtOrderNo.Name = "TxtOrderNo"
        Me.TxtOrderNo.Size = New System.Drawing.Size(141, 16)
        Me.TxtOrderNo.TabIndex = 0
        '
        'TxtPartyName
        '
        Me.TxtPartyName.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyName.AgLastValueTag = Nothing
        Me.TxtPartyName.AgLastValueText = Nothing
        Me.TxtPartyName.AgMandatory = False
        Me.TxtPartyName.AgMasterHelp = False
        Me.TxtPartyName.AgNumberLeftPlaces = 8
        Me.TxtPartyName.AgNumberNegetiveAllow = False
        Me.TxtPartyName.AgNumberRightPlaces = 2
        Me.TxtPartyName.AgPickFromLastValue = False
        Me.TxtPartyName.AgRowFilter = ""
        Me.TxtPartyName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyName.AgSelectedValue = Nothing
        Me.TxtPartyName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyName.Location = New System.Drawing.Point(492, 12)
        Me.TxtPartyName.MaxLength = 20
        Me.TxtPartyName.Name = "TxtPartyName"
        Me.TxtPartyName.Size = New System.Drawing.Size(400, 16)
        Me.TxtPartyName.TabIndex = 1
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(898, 589)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(75, 23)
        Me.BtnSave.TabIndex = 14
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'TxtRemark
        '
        Me.TxtRemark.AgAllowUserToEnableMasterHelp = False
        Me.TxtRemark.AgLastValueTag = Nothing
        Me.TxtRemark.AgLastValueText = Nothing
        Me.TxtRemark.AgMandatory = False
        Me.TxtRemark.AgMasterHelp = False
        Me.TxtRemark.AgNumberLeftPlaces = 8
        Me.TxtRemark.AgNumberNegetiveAllow = False
        Me.TxtRemark.AgNumberRightPlaces = 2
        Me.TxtRemark.AgPickFromLastValue = False
        Me.TxtRemark.AgRowFilter = ""
        Me.TxtRemark.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRemark.AgSelectedValue = Nothing
        Me.TxtRemark.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemark.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtRemark.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemark.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemark.Location = New System.Drawing.Point(70, 513)
        Me.TxtRemark.MaxLength = 20
        Me.TxtRemark.Multiline = True
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.Size = New System.Drawing.Size(400, 56)
        Me.TxtRemark.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 513)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 16)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Remark"
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl2.Location = New System.Drawing.Point(11, 232)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(962, 133)
        Me.Pnl2.TabIndex = 17
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(10, 54)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(171, 20)
        Me.LinkLabel1.TabIndex = 740
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Invoice Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel2.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Location = New System.Drawing.Point(10, 211)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(90, 20)
        Me.LinkLabel2.TabIndex = 741
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Sale Detail"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(7, 39)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(966, 3)
        Me.GroupBox2.TabIndex = 742
        Me.GroupBox2.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(9, 583)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(966, 3)
        Me.GroupBox1.TabIndex = 743
        Me.GroupBox1.TabStop = False
        '
        'LinkLabel3
        '
        Me.LinkLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel3.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel3.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel3.LinkColor = System.Drawing.Color.White
        Me.LinkLabel3.Location = New System.Drawing.Point(8, 367)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(171, 20)
        Me.LinkLabel3.TabIndex = 745
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Debit/Credit Note Detail"
        Me.LinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LinkLabel3.Visible = False
        '
        'Pnl3
        '
        Me.Pnl3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl3.Location = New System.Drawing.Point(9, 388)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(962, 118)
        Me.Pnl3.TabIndex = 744
        Me.Pnl3.Visible = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(104, 215)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(789, 13)
        Me.Label3.TabIndex = 747
        Me.Label3.Text = "W Sale Invoice Amount = W Amount + (W Amount * Sale Addition / 100) - (W Amount *" &
    " Sale Add Disc Per / 100) -(Sale Qty * Sale Disc Per)"
        '
        'BtnFind
        '
        Me.BtnFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFind.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFind.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFind.Location = New System.Drawing.Point(817, 589)
        Me.BtnFind.Name = "BtnFind"
        Me.BtnFind.Size = New System.Drawing.Size(75, 23)
        Me.BtnFind.TabIndex = 748
        Me.BtnFind.Text = "Find"
        Me.BtnFind.UseVisualStyleBackColor = True
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDelete.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.Location = New System.Drawing.Point(736, 589)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(75, 23)
        Me.BtnDelete.TabIndex = 749
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnAdd
        '
        Me.BtnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAdd.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(655, 589)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(75, 23)
        Me.BtnAdd.TabIndex = 750
        Me.BtnAdd.Text = "Add"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'TxtTag
        '
        Me.TxtTag.AgAllowUserToEnableMasterHelp = False
        Me.TxtTag.AgLastValueTag = Nothing
        Me.TxtTag.AgLastValueText = Nothing
        Me.TxtTag.AgMandatory = False
        Me.TxtTag.AgMasterHelp = False
        Me.TxtTag.AgNumberLeftPlaces = 8
        Me.TxtTag.AgNumberNegetiveAllow = False
        Me.TxtTag.AgNumberRightPlaces = 2
        Me.TxtTag.AgPickFromLastValue = False
        Me.TxtTag.AgRowFilter = ""
        Me.TxtTag.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTag.AgSelectedValue = Nothing
        Me.TxtTag.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTag.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtTag.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTag.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTag.Location = New System.Drawing.Point(741, 513)
        Me.TxtTag.MaxLength = 20
        Me.TxtTag.Name = "TxtTag"
        Me.TxtTag.Size = New System.Drawing.Size(230, 16)
        Me.TxtTag.TabIndex = 754
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(693, 513)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 16)
        Me.Label4.TabIndex = 755
        Me.Label4.Text = "Tag"
        '
        'BtnPrintW
        '
        Me.BtnPrintW.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrintW.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnPrintW.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrintW.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrintW.Location = New System.Drawing.Point(574, 589)
        Me.BtnPrintW.Name = "BtnPrintW"
        Me.BtnPrintW.Size = New System.Drawing.Size(75, 23)
        Me.BtnPrintW.TabIndex = 758
        Me.BtnPrintW.Text = "Print &W"
        Me.BtnPrintW.UseVisualStyleBackColor = True
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrint.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(413, 589)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(75, 23)
        Me.BtnPrint.TabIndex = 757
        Me.BtnPrint.Text = "&Print"
        Me.BtnPrint.UseVisualStyleBackColor = True
        Me.BtnPrint.Visible = False
        '
        'BtnTransportDetail
        '
        Me.BtnTransportDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnTransportDetail.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnTransportDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnTransportDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnTransportDetail.Location = New System.Drawing.Point(494, 547)
        Me.BtnTransportDetail.Name = "BtnTransportDetail"
        Me.BtnTransportDetail.Size = New System.Drawing.Size(164, 23)
        Me.BtnTransportDetail.TabIndex = 759
        Me.BtnTransportDetail.Text = "&Transport Detail"
        Me.BtnTransportDetail.UseVisualStyleBackColor = True
        '
        'TxtSaleOrderDocId_W
        '
        Me.TxtSaleOrderDocId_W.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleOrderDocId_W.AgLastValueTag = Nothing
        Me.TxtSaleOrderDocId_W.AgLastValueText = Nothing
        Me.TxtSaleOrderDocId_W.AgMandatory = False
        Me.TxtSaleOrderDocId_W.AgMasterHelp = False
        Me.TxtSaleOrderDocId_W.AgNumberLeftPlaces = 8
        Me.TxtSaleOrderDocId_W.AgNumberNegetiveAllow = False
        Me.TxtSaleOrderDocId_W.AgNumberRightPlaces = 2
        Me.TxtSaleOrderDocId_W.AgPickFromLastValue = False
        Me.TxtSaleOrderDocId_W.AgRowFilter = ""
        Me.TxtSaleOrderDocId_W.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleOrderDocId_W.AgSelectedValue = Nothing
        Me.TxtSaleOrderDocId_W.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleOrderDocId_W.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleOrderDocId_W.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleOrderDocId_W.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleOrderDocId_W.Location = New System.Drawing.Point(228, 12)
        Me.TxtSaleOrderDocId_W.MaxLength = 20
        Me.TxtSaleOrderDocId_W.Name = "TxtSaleOrderDocId_W"
        Me.TxtSaleOrderDocId_W.Size = New System.Drawing.Size(141, 16)
        Me.TxtSaleOrderDocId_W.TabIndex = 760
        Me.TxtSaleOrderDocId_W.Visible = False
        '
        'BtnApprove
        '
        Me.BtnApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnApprove.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnApprove.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnApprove.Location = New System.Drawing.Point(7, 592)
        Me.BtnApprove.Name = "BtnApprove"
        Me.BtnApprove.Size = New System.Drawing.Size(75, 23)
        Me.BtnApprove.TabIndex = 761
        Me.BtnApprove.Text = "&Approve"
        Me.BtnApprove.UseVisualStyleBackColor = True
        '
        'LblApproveBy
        '
        Me.LblApproveBy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblApproveBy.AutoSize = True
        Me.LblApproveBy.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblApproveBy.Location = New System.Drawing.Point(88, 596)
        Me.LblApproveBy.Name = "LblApproveBy"
        Me.LblApproveBy.Size = New System.Drawing.Size(12, 16)
        Me.LblApproveBy.TabIndex = 762
        Me.LblApproveBy.Text = "."
        '
        'BtnAddItem
        '
        Me.BtnAddItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAddItem.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddItem.Location = New System.Drawing.Point(900, 9)
        Me.BtnAddItem.Name = "BtnAddItem"
        Me.BtnAddItem.Size = New System.Drawing.Size(78, 23)
        Me.BtnAddItem.TabIndex = 763
        Me.BtnAddItem.Text = "Add Item"
        Me.BtnAddItem.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(185, 370)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(595, 14)
        Me.Label2.TabIndex = 764
        Me.Label2.Text = "Debit Note Amount = (W Amount * Purchase Add Disc Per / 100) -(Purch Qty * Purch " &
    "Disc Per)"
        Me.Label2.Visible = False
        '
        'BtnEdit
        '
        Me.BtnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEdit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(494, 589)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(75, 23)
        Me.BtnEdit.TabIndex = 765
        Me.BtnEdit.Text = "&Edit"
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'BtnFetchTransporterDetail
        '
        Me.BtnFetchTransporterDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFetchTransporterDetail.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnFetchTransporterDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFetchTransporterDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFetchTransporterDetail.Location = New System.Drawing.Point(494, 519)
        Me.BtnFetchTransporterDetail.Name = "BtnFetchTransporterDetail"
        Me.BtnFetchTransporterDetail.Size = New System.Drawing.Size(164, 23)
        Me.BtnFetchTransporterDetail.TabIndex = 766
        Me.BtnFetchTransporterDetail.Text = "&Fetch Transport Detail"
        Me.BtnFetchTransporterDetail.UseVisualStyleBackColor = True
        Me.BtnFetchTransporterDetail.Visible = False
        '
        'FrmSaleInvoiceW_OnlyW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 621)
        Me.Controls.Add(Me.BtnFetchTransporterDetail)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnAddItem)
        Me.Controls.Add(Me.LblApproveBy)
        Me.Controls.Add(Me.BtnApprove)
        Me.Controls.Add(Me.TxtSaleOrderDocId_W)
        Me.Controls.Add(Me.BtnTransportDetail)
        Me.Controls.Add(Me.BtnPrintW)
        Me.Controls.Add(Me.TxtTag)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnFind)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LinkLabel3)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.TxtRemark)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.TxtPartyName)
        Me.Controls.Add(Me.LblSaleToParty)
        Me.Controls.Add(Me.TxtOrderNo)
        Me.Controls.Add(Me.LblOrderNo)
        Me.Controls.Add(Me.Pnl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "FrmSaleInvoiceW_OnlyW"
        Me.Text = "Sale Invoice W"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents LblOrderNo As Label
    Public WithEvents LblSaleToParty As Label
    Public WithEvents TxtOrderNo As AgControls.AgTextBox
    Public WithEvents TxtPartyName As AgControls.AgTextBox
    Friend WithEvents BtnSave As Button
    Public WithEvents TxtRemark As AgControls.AgTextBox
    Public WithEvents Label1 As Label
    Public WithEvents Pnl2 As Panel
    Public WithEvents LinkLabel1 As LinkLabel
    Public WithEvents LinkLabel2 As LinkLabel
    Public WithEvents GroupBox2 As GroupBox
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents LinkLabel3 As LinkLabel
    Public WithEvents Pnl3 As Panel
    Public WithEvents Label3 As Label
    Friend WithEvents BtnFind As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents BtnAdd As Button
    Public WithEvents TxtTag As AgControls.AgTextBox
    Public WithEvents Label4 As Label
    Friend WithEvents BtnPrintW As Button
    Friend WithEvents BtnPrint As Button
    Friend WithEvents BtnTransportDetail As Button
    Public WithEvents TxtSaleOrderDocId_W As AgControls.AgTextBox
    Friend WithEvents BtnApprove As Button
    Public WithEvents LblApproveBy As Label
    Friend WithEvents BtnAddItem As Button
    Public WithEvents Label2 As Label
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnFetchTransporterDetail As Button
End Class
