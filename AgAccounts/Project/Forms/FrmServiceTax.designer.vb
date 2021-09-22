<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmServiceTax
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
        Me.LblName = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.TxtConsignee = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtPlaceFrom = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtV_No = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtConsignor = New AgControls.AgTextBox
        Me.TxtDate = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtRemark = New AgControls.AgTextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TxtModified = New AgControls.AgTextBox
        Me.GrpUP = New System.Windows.Forms.GroupBox
        Me.TxtPrepared = New AgControls.AgTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TxtRecId = New AgControls.AgTextBox
        Me.TxtAmount = New AgControls.AgTextBox
        Me.TxtConsignorBNo = New AgControls.AgTextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtConsigneeBNo = New AgControls.AgTextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.PnlExp = New System.Windows.Forms.Panel
        Me.TxtDescription = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtPlaceTo = New AgControls.AgTextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtVehicleNo = New AgControls.AgTextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TxtType = New AgControls.AgTextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.TxtVType = New AgControls.AgTextBox
        Me.TxtParty = New AgControls.AgTextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.TxtTDSCategory = New AgControls.AgTextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.PnlTDS = New System.Windows.Forms.Panel
        Me.LblTotalName = New System.Windows.Forms.Label
        Me.LblBG = New System.Windows.Forms.Label
        Me.LblTDSAmt = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtTDSCalOn = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.TxtPtyDate = New AgControls.AgTextBox
        Me.TxtPtyBillNo = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.TxtVRef = New AgControls.AgTextBox
        Me.GrpBox = New System.Windows.Forms.GroupBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.PnlAdj = New System.Windows.Forms.Panel
        Me.GroupBox4.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GrpBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblName
        '
        Me.LblName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(25, 146)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(66, 16)
        Me.LblName.TabIndex = 14
        Me.LblName.Text = "Consignor"
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(133, 146)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(10, 7)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Ä"
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
        Me.Topctrl1.Size = New System.Drawing.Size(977, 41)
        Me.Topctrl1.TabIndex = 21
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
        'TxtConsignee
        '
        Me.TxtConsignee.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtConsignee.BackColor = System.Drawing.Color.White
        Me.TxtConsignee.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtConsignee.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtConsignee.Location = New System.Drawing.Point(149, 166)
        Me.TxtConsignee.MaxLength = 0
        Me.TxtConsignee.Name = "TxtConsignee"
        Me.TxtConsignee.Size = New System.Drawing.Size(302, 18)
        Me.TxtConsignee.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 16)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Consignee"
        '
        'TxtPlaceFrom
        '
        Me.TxtPlaceFrom.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPlaceFrom.BackColor = System.Drawing.Color.White
        Me.TxtPlaceFrom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPlaceFrom.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlaceFrom.Location = New System.Drawing.Point(149, 206)
        Me.TxtPlaceFrom.MaxLength = 0
        Me.TxtPlaceFrom.Name = "TxtPlaceFrom"
        Me.TxtPlaceFrom.Size = New System.Drawing.Size(115, 18)
        Me.TxtPlaceFrom.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(25, 206)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 16)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Place From"
        '
        'TxtV_No
        '
        Me.TxtV_No.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtV_No.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_No.Location = New System.Drawing.Point(781, 43)
        Me.TxtV_No.MaxLength = 0
        Me.TxtV_No.Name = "TxtV_No"
        Me.TxtV_No.Size = New System.Drawing.Size(47, 18)
        Me.TxtV_No.TabIndex = 1
        Me.TxtV_No.Visible = False
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(25, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 16)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Entry No."
        '
        'TxtConsignor
        '
        Me.TxtConsignor.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtConsignor.BackColor = System.Drawing.Color.White
        Me.TxtConsignor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtConsignor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtConsignor.Location = New System.Drawing.Point(149, 146)
        Me.TxtConsignor.MaxLength = 0
        Me.TxtConsignor.Name = "TxtConsignor"
        Me.TxtConsignor.Size = New System.Drawing.Size(302, 18)
        Me.TxtConsignor.TabIndex = 6
        '
        'TxtDate
        '
        Me.TxtDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate.Location = New System.Drawing.Point(336, 86)
        Me.TxtDate.MaxLength = 0
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(115, 18)
        Me.TxtDate.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(267, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 16)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Date"
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(322, 86)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 7)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "Ä"
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(25, 286)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 16)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Remark"
        '
        'TxtRemark
        '
        Me.TxtRemark.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtRemark.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemark.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemark.Location = New System.Drawing.Point(149, 286)
        Me.TxtRemark.MaxLength = 500
        Me.TxtRemark.Multiline = True
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtRemark.Size = New System.Drawing.Size(302, 38)
        Me.TxtRemark.TabIndex = 17
        Me.TxtRemark.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.TxtModified)
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox4.Location = New System.Drawing.Point(782, 581)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(186, 51)
        Me.GroupBox4.TabIndex = 50
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Tag = "TR"
        Me.GroupBox4.Text = "Modified By "
        '
        'TxtModified
        '
        Me.TxtModified.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtModified.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtModified.Enabled = False
        Me.TxtModified.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModified.Location = New System.Drawing.Point(15, 21)
        Me.TxtModified.Name = "TxtModified"
        Me.TxtModified.Size = New System.Drawing.Size(158, 18)
        Me.TxtModified.TabIndex = 6
        Me.TxtModified.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GrpUP
        '
        Me.GrpUP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpUP.Controls.Add(Me.TxtPrepared)
        Me.GrpUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpUP.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GrpUP.ForeColor = System.Drawing.Color.Maroon
        Me.GrpUP.Location = New System.Drawing.Point(8, 581)
        Me.GrpUP.Name = "GrpUP"
        Me.GrpUP.Size = New System.Drawing.Size(186, 51)
        Me.GrpUP.TabIndex = 49
        Me.GrpUP.TabStop = False
        Me.GrpUP.Tag = "TR"
        Me.GrpUP.Text = "Prepared By "
        '
        'TxtPrepared
        '
        Me.TxtPrepared.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtPrepared.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPrepared.Enabled = False
        Me.TxtPrepared.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrepared.Location = New System.Drawing.Point(15, 21)
        Me.TxtPrepared.Name = "TxtPrepared"
        Me.TxtPrepared.Size = New System.Drawing.Size(158, 18)
        Me.TxtPrepared.TabIndex = 6
        Me.TxtPrepared.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(6, 566)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(964, 9)
        Me.GroupBox2.TabIndex = 48
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'TxtRecId
        '
        Me.TxtRecId.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtRecId.BackColor = System.Drawing.Color.White
        Me.TxtRecId.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRecId.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRecId.Location = New System.Drawing.Point(149, 86)
        Me.TxtRecId.MaxLength = 0
        Me.TxtRecId.Name = "TxtRecId"
        Me.TxtRecId.Size = New System.Drawing.Size(115, 18)
        Me.TxtRecId.TabIndex = 1
        '
        'TxtAmount
        '
        Me.TxtAmount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAmount.BackColor = System.Drawing.Color.White
        Me.TxtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAmount.Location = New System.Drawing.Point(149, 246)
        Me.TxtAmount.MaxLength = 0
        Me.TxtAmount.Name = "TxtAmount"
        Me.TxtAmount.Size = New System.Drawing.Size(115, 18)
        Me.TxtAmount.TabIndex = 13
        '
        'TxtConsignorBNo
        '
        Me.TxtConsignorBNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtConsignorBNo.BackColor = System.Drawing.Color.White
        Me.TxtConsignorBNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtConsignorBNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtConsignorBNo.Location = New System.Drawing.Point(149, 186)
        Me.TxtConsignorBNo.MaxLength = 50
        Me.TxtConsignorBNo.Name = "TxtConsignorBNo"
        Me.TxtConsignorBNo.Size = New System.Drawing.Size(115, 18)
        Me.TxtConsignorBNo.TabIndex = 8
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(25, 186)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(99, 16)
        Me.Label12.TabIndex = 54
        Me.Label12.Text = "Consignor B.No"
        '
        'TxtConsigneeBNo
        '
        Me.TxtConsigneeBNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtConsigneeBNo.BackColor = System.Drawing.Color.White
        Me.TxtConsigneeBNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtConsigneeBNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtConsigneeBNo.Location = New System.Drawing.Point(336, 186)
        Me.TxtConsigneeBNo.MaxLength = 50
        Me.TxtConsigneeBNo.Name = "TxtConsigneeBNo"
        Me.TxtConsigneeBNo.Size = New System.Drawing.Size(115, 18)
        Me.TxtConsigneeBNo.TabIndex = 9
        '
        'Label15
        '
        Me.Label15.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(267, 186)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 16)
        Me.Label15.TabIndex = 57
        Me.Label15.Text = "C'ee B.No."
        '
        'PnlExp
        '
        Me.PnlExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlExp.Location = New System.Drawing.Point(28, 349)
        Me.PnlExp.Name = "PnlExp"
        Me.PnlExp.Size = New System.Drawing.Size(377, 206)
        Me.PnlExp.TabIndex = 18
        '
        'TxtDescription
        '
        Me.TxtDescription.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtDescription.BackColor = System.Drawing.Color.White
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(149, 226)
        Me.TxtDescription.MaxLength = 100
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(302, 18)
        Me.TxtDescription.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(25, 226)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 16)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "Description"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(25, 246)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 16)
        Me.Label8.TabIndex = 62
        Me.Label8.Text = "Amount"
        '
        'TxtPlaceTo
        '
        Me.TxtPlaceTo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPlaceTo.BackColor = System.Drawing.Color.White
        Me.TxtPlaceTo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPlaceTo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlaceTo.Location = New System.Drawing.Point(336, 206)
        Me.TxtPlaceTo.MaxLength = 0
        Me.TxtPlaceTo.Name = "TxtPlaceTo"
        Me.TxtPlaceTo.Size = New System.Drawing.Size(115, 18)
        Me.TxtPlaceTo.TabIndex = 11
        '
        'Label16
        '
        Me.Label16.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(267, 206)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(59, 16)
        Me.Label16.TabIndex = 64
        Me.Label16.Text = "Place To"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(267, 246)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 16)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "Vehicle No."
        '
        'TxtVehicleNo
        '
        Me.TxtVehicleNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtVehicleNo.BackColor = System.Drawing.Color.White
        Me.TxtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVehicleNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVehicleNo.Location = New System.Drawing.Point(336, 246)
        Me.TxtVehicleNo.MaxLength = 50
        Me.TxtVehicleNo.Name = "TxtVehicleNo"
        Me.TxtVehicleNo.Size = New System.Drawing.Size(115, 18)
        Me.TxtVehicleNo.TabIndex = 14
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(837, 43)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 16)
        Me.Label10.TabIndex = 70
        Me.Label10.Text = "Type"
        Me.Label10.Visible = False
        '
        'TxtType
        '
        Me.TxtType.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtType.BackColor = System.Drawing.Color.White
        Me.TxtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtType.Location = New System.Drawing.Point(906, 43)
        Me.TxtType.MaxLength = 50
        Me.TxtType.Name = "TxtType"
        Me.TxtType.Size = New System.Drawing.Size(40, 18)
        Me.TxtType.TabIndex = 12
        Me.TxtType.Visible = False
        '
        'Label14
        '
        Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(889, 43)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(10, 7)
        Me.Label14.TabIndex = 71
        Me.Label14.Text = "Ä"
        Me.Label14.Visible = False
        '
        'Label18
        '
        Me.Label18.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(133, 66)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(10, 7)
        Me.Label18.TabIndex = 77
        Me.Label18.Text = "Ä"
        '
        'Label19
        '
        Me.Label19.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(25, 66)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(40, 16)
        Me.Label19.TabIndex = 76
        Me.Label19.Text = "Type."
        '
        'TxtVType
        '
        Me.TxtVType.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtVType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVType.Enabled = False
        Me.TxtVType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVType.Location = New System.Drawing.Point(149, 66)
        Me.TxtVType.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtVType.MaxLength = 8
        Me.TxtVType.Name = "TxtVType"
        Me.TxtVType.Size = New System.Drawing.Size(302, 18)
        Me.TxtVType.TabIndex = 0
        '
        'TxtParty
        '
        Me.TxtParty.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtParty.BackColor = System.Drawing.Color.White
        Me.TxtParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParty.Location = New System.Drawing.Point(149, 106)
        Me.TxtParty.MaxLength = 0
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(302, 18)
        Me.TxtParty.TabIndex = 3
        '
        'Label20
        '
        Me.Label20.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(133, 106)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(10, 7)
        Me.Label20.TabIndex = 80
        Me.Label20.Text = "Ä"
        '
        'Label21
        '
        Me.Label21.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(25, 106)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(39, 16)
        Me.Label21.TabIndex = 79
        Me.Label21.Text = "Party"
        '
        'TxtTDSCategory
        '
        Me.TxtTDSCategory.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtTDSCategory.BackColor = System.Drawing.Color.White
        Me.TxtTDSCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTDSCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTDSCategory.Location = New System.Drawing.Point(336, 266)
        Me.TxtTDSCategory.MaxLength = 0
        Me.TxtTDSCategory.Name = "TxtTDSCategory"
        Me.TxtTDSCategory.Size = New System.Drawing.Size(115, 18)
        Me.TxtTDSCategory.TabIndex = 16
        '
        'Label23
        '
        Me.Label23.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(267, 266)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(57, 16)
        Me.Label23.TabIndex = 82
        Me.Label23.Text = "TDS Cat"
        '
        'PnlTDS
        '
        Me.PnlTDS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTDS.Location = New System.Drawing.Point(414, 349)
        Me.PnlTDS.Name = "PnlTDS"
        Me.PnlTDS.Size = New System.Drawing.Size(542, 179)
        Me.PnlTDS.TabIndex = 19
        '
        'LblTotalName
        '
        Me.LblTotalName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTotalName.AutoSize = True
        Me.LblTotalName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalName.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalName.Location = New System.Drawing.Point(420, 534)
        Me.LblTotalName.Name = "LblTotalName"
        Me.LblTotalName.Size = New System.Drawing.Size(70, 16)
        Me.LblTotalName.TabIndex = 84
        Me.LblTotalName.Text = "Total TDS"
        '
        'LblBG
        '
        Me.LblBG.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblBG.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblBG.Location = New System.Drawing.Point(414, 529)
        Me.LblBG.Name = "LblBG"
        Me.LblBG.Size = New System.Drawing.Size(543, 26)
        Me.LblBG.TabIndex = 83
        '
        'LblTDSAmt
        '
        Me.LblTDSAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTDSAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTDSAmt.ForeColor = System.Drawing.Color.Maroon
        Me.LblTDSAmt.Location = New System.Drawing.Point(813, 534)
        Me.LblTDSAmt.Name = "LblTDSAmt"
        Me.LblTDSAmt.Size = New System.Drawing.Size(124, 16)
        Me.LblTDSAmt.TabIndex = 85
        Me.LblTDSAmt.Text = "0"
        Me.LblTDSAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(25, 266)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(112, 16)
        Me.Label22.TabIndex = 87
        Me.Label22.Text = "TDS Calculate On"
        '
        'TxtTDSCalOn
        '
        Me.TxtTDSCalOn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtTDSCalOn.BackColor = System.Drawing.Color.White
        Me.TxtTDSCalOn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTDSCalOn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTDSCalOn.Location = New System.Drawing.Point(149, 266)
        Me.TxtTDSCalOn.MaxLength = 0
        Me.TxtTDSCalOn.Name = "TxtTDSCalOn"
        Me.TxtTDSCalOn.Size = New System.Drawing.Size(115, 18)
        Me.TxtTDSCalOn.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(25, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 16)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "PB / GR No."
        '
        'Label17
        '
        Me.Label17.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(267, 126)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(59, 16)
        Me.Label17.TabIndex = 41
        Me.Label17.Text = "Pty Date"
        '
        'TxtPtyDate
        '
        Me.TxtPtyDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPtyDate.BackColor = System.Drawing.Color.White
        Me.TxtPtyDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPtyDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPtyDate.Location = New System.Drawing.Point(336, 126)
        Me.TxtPtyDate.MaxLength = 0
        Me.TxtPtyDate.Name = "TxtPtyDate"
        Me.TxtPtyDate.Size = New System.Drawing.Size(115, 18)
        Me.TxtPtyDate.TabIndex = 5
        '
        'TxtPtyBillNo
        '
        Me.TxtPtyBillNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPtyBillNo.BackColor = System.Drawing.Color.White
        Me.TxtPtyBillNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPtyBillNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPtyBillNo.Location = New System.Drawing.Point(149, 126)
        Me.TxtPtyBillNo.MaxLength = 20
        Me.TxtPtyBillNo.Name = "TxtPtyBillNo"
        Me.TxtPtyBillNo.Size = New System.Drawing.Size(115, 18)
        Me.TxtPtyBillNo.TabIndex = 4
        '
        'Label25
        '
        Me.Label25.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(134, 246)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(10, 7)
        Me.Label25.TabIndex = 90
        Me.Label25.Text = "Ä"
        '
        'Label27
        '
        Me.Label27.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Maroon
        Me.Label27.Location = New System.Drawing.Point(245, 12)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(191, 33)
        Me.Label27.TabIndex = 93
        Me.Label27.Text = "By Selecting Payment Vr. No. Will " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Save Data Without A/c Posting."
        '
        'TxtVRef
        '
        Me.TxtVRef.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtVRef.BackColor = System.Drawing.Color.White
        Me.TxtVRef.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVRef.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVRef.Location = New System.Drawing.Point(15, 21)
        Me.TxtVRef.MaxLength = 0
        Me.TxtVRef.Name = "TxtVRef"
        Me.TxtVRef.Size = New System.Drawing.Size(158, 18)
        Me.TxtVRef.TabIndex = 0
        '
        'GrpBox
        '
        Me.GrpBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GrpBox.Controls.Add(Me.Label26)
        Me.GrpBox.Controls.Add(Me.Label27)
        Me.GrpBox.Controls.Add(Me.TxtVRef)
        Me.GrpBox.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GrpBox.ForeColor = System.Drawing.Color.Maroon
        Me.GrpBox.Location = New System.Drawing.Point(271, 581)
        Me.GrpBox.Name = "GrpBox"
        Me.GrpBox.Size = New System.Drawing.Size(450, 51)
        Me.GrpBox.TabIndex = 22
        Me.GrpBox.TabStop = False
        Me.GrpBox.Tag = "TR"
        Me.GrpBox.Text = "Payment Vr. No. "
        '
        'Label26
        '
        Me.Label26.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Maroon
        Me.Label26.Location = New System.Drawing.Point(201, 12)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(39, 15)
        Me.Label26.TabIndex = 94
        Me.Label26.Text = "Note :"
        '
        'PnlAdj
        '
        Me.PnlAdj.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlAdj.Location = New System.Drawing.Point(461, 67)
        Me.PnlAdj.Name = "PnlAdj"
        Me.PnlAdj.Size = New System.Drawing.Size(495, 257)
        Me.PnlAdj.TabIndex = 20
        '
        'FrmServiceTax
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(977, 638)
        Me.Controls.Add(Me.PnlAdj)
        Me.Controls.Add(Me.GrpBox)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.TxtTDSCalOn)
        Me.Controls.Add(Me.LblTDSAmt)
        Me.Controls.Add(Me.LblTotalName)
        Me.Controls.Add(Me.LblBG)
        Me.Controls.Add(Me.PnlTDS)
        Me.Controls.Add(Me.TxtTDSCategory)
        Me.Controls.Add(Me.TxtParty)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.TxtVType)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtType)
        Me.Controls.Add(Me.TxtVehicleNo)
        Me.Controls.Add(Me.TxtPlaceTo)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.PnlExp)
        Me.Controls.Add(Me.TxtConsigneeBNo)
        Me.Controls.Add(Me.TxtConsignorBNo)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TxtAmount)
        Me.Controls.Add(Me.TxtPtyBillNo)
        Me.Controls.Add(Me.TxtRecId)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GrpUP)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtRemark)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtPtyDate)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.TxtDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtConsignor)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtV_No)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtPlaceFrom)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtConsignee)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.KeyPreview = True
        Me.Name = "FrmServiceTax"
        Me.ShowIcon = False
        Me.Text = "Service Tax Entry"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GrpBox.ResumeLayout(False)
        Me.GrpBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtConsignee As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtPlaceFrom As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtV_No As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtConsignor As AgControls.AgTextBox
    Friend WithEvents TxtDate As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtRemark As AgControls.AgTextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtModified As AgControls.AgTextBox
    Friend WithEvents GrpUP As System.Windows.Forms.GroupBox
    Friend WithEvents TxtPrepared As AgControls.AgTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtRecId As AgControls.AgTextBox
    Friend WithEvents TxtAmount As AgControls.AgTextBox
    Friend WithEvents TxtConsignorBNo As AgControls.AgTextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TxtConsigneeBNo As AgControls.AgTextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents PnlExp As System.Windows.Forms.Panel
    Friend WithEvents TxtDescription As AgControls.AgTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtPlaceTo As AgControls.AgTextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtVehicleNo As AgControls.AgTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtType As AgControls.AgTextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TxtVType As AgControls.AgTextBox
    Friend WithEvents TxtParty As AgControls.AgTextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents TxtTDSCategory As AgControls.AgTextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents PnlTDS As System.Windows.Forms.Panel
    Friend WithEvents LblTotalName As System.Windows.Forms.Label
    Friend WithEvents LblBG As System.Windows.Forms.Label
    Friend WithEvents LblTDSAmt As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TxtTDSCalOn As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TxtPtyDate As AgControls.AgTextBox
    Friend WithEvents TxtPtyBillNo As AgControls.AgTextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TxtVRef As AgControls.AgTextBox
    Friend WithEvents GrpBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PnlAdj As System.Windows.Forms.Panel
End Class
