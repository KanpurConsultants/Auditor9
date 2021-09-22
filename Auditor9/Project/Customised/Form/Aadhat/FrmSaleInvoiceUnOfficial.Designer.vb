<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSaleInvoiceUnOfficial
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
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.TxtRemark = New AgControls.AgTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.Pnl3 = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
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
        Me.LblSaleToParty.Location = New System.Drawing.Point(455, 12)
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
        Me.TxtPartyName.Location = New System.Drawing.Point(572, 12)
        Me.TxtPartyName.MaxLength = 20
        Me.TxtPartyName.Name = "TxtPartyName"
        Me.TxtPartyName.Size = New System.Drawing.Size(400, 16)
        Me.TxtPartyName.TabIndex = 1
        '
        'BtnOk
        '
        Me.BtnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(898, 596)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 23)
        Me.BtnOk.TabIndex = 14
        Me.BtnOk.Text = "Save"
        Me.BtnOk.UseVisualStyleBackColor = True
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
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
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
        Me.LinkLabel2.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel2.TabIndex = 741
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Sale Invoice Detail"
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
        Me.LinkLabel3.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel3.TabIndex = 745
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Debit/Credit Note Detail"
        Me.LinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pnl3
        '
        Me.Pnl3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl3.Location = New System.Drawing.Point(9, 388)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(962, 118)
        Me.Pnl3.TabIndex = 744
        '
        'FrmSaleInvoiceUnOfficial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 621)
        Me.Controls.Add(Me.LinkLabel3)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.TxtRemark)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.TxtPartyName)
        Me.Controls.Add(Me.LblSaleToParty)
        Me.Controls.Add(Me.TxtOrderNo)
        Me.Controls.Add(Me.LblOrderNo)
        Me.Controls.Add(Me.Pnl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "FrmSaleInvoiceUnOfficial"
        Me.Text = "Sale Invoice Order Summary"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents LblOrderNo As Label
    Public WithEvents LblSaleToParty As Label
    Public WithEvents TxtOrderNo As AgControls.AgTextBox
    Public WithEvents TxtPartyName As AgControls.AgTextBox
    Friend WithEvents BtnOk As Button
    Public WithEvents TxtRemark As AgControls.AgTextBox
    Public WithEvents Label1 As Label
    Public WithEvents Pnl2 As Panel
    Public WithEvents LinkLabel1 As LinkLabel
    Public WithEvents LinkLabel2 As LinkLabel
    Public WithEvents GroupBox2 As GroupBox
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents LinkLabel3 As LinkLabel
    Public WithEvents Pnl3 As Panel
End Class
