<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBankReconciliation
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
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TxtBankName = New AgControls.AgTextBox()
        Me.BtnFillGrid = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtType = New AgControls.AgTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtDate = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblBG = New System.Windows.Forms.Label()
        Me.Lblbalance = New System.Windows.Forms.Label()
        Me.LblAmountnotReflected = New System.Windows.Forms.Label()
        Me.Lblbank = New System.Windows.Forms.Label()
        Me.LblCompanyBal = New System.Windows.Forms.Label()
        Me.LblAmtNotClg_Dr = New System.Windows.Forms.Label()
        Me.LblClgAmt = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtshowContra = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LblAmtNotClg_Cr = New System.Windows.Forms.Label()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.TxtFromDate = New AgControls.AgTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtFromAmount = New AgControls.AgTextBox()
        Me.LblFromAmount = New System.Windows.Forms.Label()
        Me.LblToAmount = New System.Windows.Forms.Label()
        Me.TxtToAmount = New AgControls.AgTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtSite = New AgControls.AgTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtDivision = New AgControls.AgTextBox()
        Me.SuspendLayout()
        '
        'PnlMain
        '
        Me.PnlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlMain.Location = New System.Drawing.Point(12, 177)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(958, 351)
        Me.PnlMain.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(307, 66)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(10, 7)
        Me.Label16.TabIndex = 47
        Me.Label16.Text = "Ä"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(199, 61)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(76, 16)
        Me.Label17.TabIndex = 46
        Me.Label17.Text = "Bank Name"
        '
        'TxtBankName
        '
        Me.TxtBankName.AgAllowUserToEnableMasterHelp = False
        Me.TxtBankName.AgLastValueTag = Nothing
        Me.TxtBankName.AgLastValueText = Nothing
        Me.TxtBankName.AgMandatory = False
        Me.TxtBankName.AgMasterHelp = False
        Me.TxtBankName.AgNumberLeftPlaces = 0
        Me.TxtBankName.AgNumberNegetiveAllow = False
        Me.TxtBankName.AgNumberRightPlaces = 0
        Me.TxtBankName.AgPickFromLastValue = False
        Me.TxtBankName.AgRowFilter = ""
        Me.TxtBankName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBankName.AgSelectedValue = Nothing
        Me.TxtBankName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBankName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBankName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankName.Location = New System.Drawing.Point(321, 61)
        Me.TxtBankName.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtBankName.MaxLength = 15
        Me.TxtBankName.Name = "TxtBankName"
        Me.TxtBankName.Size = New System.Drawing.Size(364, 18)
        Me.TxtBankName.TabIndex = 2
        '
        'BtnFillGrid
        '
        Me.BtnFillGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFillGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillGrid.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnFillGrid.Location = New System.Drawing.Point(772, 66)
        Me.BtnFillGrid.Name = "BtnFillGrid"
        Me.BtnFillGrid.Size = New System.Drawing.Size(100, 27)
        Me.BtnFillGrid.TabIndex = 5
        Me.BtnFillGrid.Text = "&Fill Grid"
        Me.BtnFillGrid.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(7, 166)
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
        Me.Label2.Location = New System.Drawing.Point(307, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Ä"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(198, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 16)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Type"
        '
        'TxtType
        '
        Me.TxtType.AgAllowUserToEnableMasterHelp = False
        Me.TxtType.AgLastValueTag = Nothing
        Me.TxtType.AgLastValueText = Nothing
        Me.TxtType.AgMandatory = False
        Me.TxtType.AgMasterHelp = False
        Me.TxtType.AgNumberLeftPlaces = 0
        Me.TxtType.AgNumberNegetiveAllow = False
        Me.TxtType.AgNumberRightPlaces = 0
        Me.TxtType.AgPickFromLastValue = False
        Me.TxtType.AgRowFilter = ""
        Me.TxtType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtType.AgSelectedValue = Nothing
        Me.TxtType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtType.Location = New System.Drawing.Point(321, 81)
        Me.TxtType.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtType.MaxLength = 15
        Me.TxtType.Name = "TxtType"
        Me.TxtType.Size = New System.Drawing.Size(128, 18)
        Me.TxtType.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(307, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "Ä"
        '
        'TxtDate
        '
        Me.TxtDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtDate.AgLastValueTag = Nothing
        Me.TxtDate.AgLastValueText = Nothing
        Me.TxtDate.AgMandatory = False
        Me.TxtDate.AgMasterHelp = False
        Me.TxtDate.AgNumberLeftPlaces = 0
        Me.TxtDate.AgNumberNegetiveAllow = False
        Me.TxtDate.AgNumberRightPlaces = 0
        Me.TxtDate.AgPickFromLastValue = False
        Me.TxtDate.AgRowFilter = ""
        Me.TxtDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDate.AgSelectedValue = Nothing
        Me.TxtDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate.Location = New System.Drawing.Point(321, 41)
        Me.TxtDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtDate.MaxLength = 15
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(128, 18)
        Me.TxtDate.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(199, 38)
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
        Me.Label1.Location = New System.Drawing.Point(453, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 16)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "Show Contra"
        '
        'TxtshowContra
        '
        Me.TxtshowContra.AgAllowUserToEnableMasterHelp = False
        Me.TxtshowContra.AgLastValueTag = Nothing
        Me.TxtshowContra.AgLastValueText = Nothing
        Me.TxtshowContra.AgMandatory = False
        Me.TxtshowContra.AgMasterHelp = False
        Me.TxtshowContra.AgNumberLeftPlaces = 0
        Me.TxtshowContra.AgNumberNegetiveAllow = False
        Me.TxtshowContra.AgNumberRightPlaces = 0
        Me.TxtshowContra.AgPickFromLastValue = False
        Me.TxtshowContra.AgRowFilter = ""
        Me.TxtshowContra.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtshowContra.AgSelectedValue = Nothing
        Me.TxtshowContra.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtshowContra.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtshowContra.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtshowContra.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtshowContra.Location = New System.Drawing.Point(557, 81)
        Me.TxtshowContra.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtshowContra.MaxLength = 15
        Me.TxtshowContra.Name = "TxtshowContra"
        Me.TxtshowContra.Size = New System.Drawing.Size(128, 18)
        Me.TxtshowContra.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(543, 86)
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
        'TxtFromDate
        '
        Me.TxtFromDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromDate.AgLastValueTag = Nothing
        Me.TxtFromDate.AgLastValueText = Nothing
        Me.TxtFromDate.AgMandatory = False
        Me.TxtFromDate.AgMasterHelp = False
        Me.TxtFromDate.AgNumberLeftPlaces = 0
        Me.TxtFromDate.AgNumberNegetiveAllow = False
        Me.TxtFromDate.AgNumberRightPlaces = 0
        Me.TxtFromDate.AgPickFromLastValue = False
        Me.TxtFromDate.AgRowFilter = ""
        Me.TxtFromDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromDate.AgSelectedValue = Nothing
        Me.TxtFromDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromDate.Location = New System.Drawing.Point(557, 41)
        Me.TxtFromDate.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtFromDate.MaxLength = 15
        Me.TxtFromDate.Name = "TxtFromDate"
        Me.TxtFromDate.Size = New System.Drawing.Size(128, 18)
        Me.TxtFromDate.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(456, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 16)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "From Date"
        '
        'TxtFromAmount
        '
        Me.TxtFromAmount.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromAmount.AgLastValueTag = Nothing
        Me.TxtFromAmount.AgLastValueText = Nothing
        Me.TxtFromAmount.AgMandatory = False
        Me.TxtFromAmount.AgMasterHelp = False
        Me.TxtFromAmount.AgNumberLeftPlaces = 0
        Me.TxtFromAmount.AgNumberNegetiveAllow = False
        Me.TxtFromAmount.AgNumberRightPlaces = 0
        Me.TxtFromAmount.AgPickFromLastValue = False
        Me.TxtFromAmount.AgRowFilter = ""
        Me.TxtFromAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromAmount.AgSelectedValue = Nothing
        Me.TxtFromAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFromAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromAmount.Location = New System.Drawing.Point(321, 101)
        Me.TxtFromAmount.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtFromAmount.MaxLength = 15
        Me.TxtFromAmount.Name = "TxtFromAmount"
        Me.TxtFromAmount.Size = New System.Drawing.Size(128, 18)
        Me.TxtFromAmount.TabIndex = 86
        '
        'LblFromAmount
        '
        Me.LblFromAmount.AutoSize = True
        Me.LblFromAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFromAmount.Location = New System.Drawing.Point(199, 102)
        Me.LblFromAmount.Name = "LblFromAmount"
        Me.LblFromAmount.Size = New System.Drawing.Size(86, 16)
        Me.LblFromAmount.TabIndex = 87
        Me.LblFromAmount.Text = "From Amount"
        '
        'LblToAmount
        '
        Me.LblToAmount.AutoSize = True
        Me.LblToAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToAmount.Location = New System.Drawing.Point(453, 101)
        Me.LblToAmount.Name = "LblToAmount"
        Me.LblToAmount.Size = New System.Drawing.Size(69, 16)
        Me.LblToAmount.TabIndex = 89
        Me.LblToAmount.Text = "To Amount"
        '
        'TxtToAmount
        '
        Me.TxtToAmount.AgAllowUserToEnableMasterHelp = False
        Me.TxtToAmount.AgLastValueTag = Nothing
        Me.TxtToAmount.AgLastValueText = Nothing
        Me.TxtToAmount.AgMandatory = False
        Me.TxtToAmount.AgMasterHelp = False
        Me.TxtToAmount.AgNumberLeftPlaces = 0
        Me.TxtToAmount.AgNumberNegetiveAllow = False
        Me.TxtToAmount.AgNumberRightPlaces = 0
        Me.TxtToAmount.AgPickFromLastValue = False
        Me.TxtToAmount.AgRowFilter = ""
        Me.TxtToAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToAmount.AgSelectedValue = Nothing
        Me.TxtToAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToAmount.Location = New System.Drawing.Point(557, 101)
        Me.TxtToAmount.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtToAmount.MaxLength = 15
        Me.TxtToAmount.Name = "TxtToAmount"
        Me.TxtToAmount.Size = New System.Drawing.Size(128, 18)
        Me.TxtToAmount.TabIndex = 88
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(199, 121)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 16)
        Me.Label9.TabIndex = 91
        Me.Label9.Text = "Site Name"
        '
        'TxtSite
        '
        Me.TxtSite.AgAllowUserToEnableMasterHelp = False
        Me.TxtSite.AgLastValueTag = Nothing
        Me.TxtSite.AgLastValueText = Nothing
        Me.TxtSite.AgMandatory = False
        Me.TxtSite.AgMasterHelp = False
        Me.TxtSite.AgNumberLeftPlaces = 0
        Me.TxtSite.AgNumberNegetiveAllow = False
        Me.TxtSite.AgNumberRightPlaces = 0
        Me.TxtSite.AgPickFromLastValue = False
        Me.TxtSite.AgRowFilter = ""
        Me.TxtSite.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSite.AgSelectedValue = Nothing
        Me.TxtSite.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSite.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSite.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSite.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite.Location = New System.Drawing.Point(321, 121)
        Me.TxtSite.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtSite.MaxLength = 15
        Me.TxtSite.Name = "TxtSite"
        Me.TxtSite.Size = New System.Drawing.Size(364, 18)
        Me.TxtSite.TabIndex = 90
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(199, 141)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 16)
        Me.Label11.TabIndex = 94
        Me.Label11.Text = "Division Name"
        '
        'TxtDivision
        '
        Me.TxtDivision.AgAllowUserToEnableMasterHelp = False
        Me.TxtDivision.AgLastValueTag = Nothing
        Me.TxtDivision.AgLastValueText = Nothing
        Me.TxtDivision.AgMandatory = False
        Me.TxtDivision.AgMasterHelp = False
        Me.TxtDivision.AgNumberLeftPlaces = 0
        Me.TxtDivision.AgNumberNegetiveAllow = False
        Me.TxtDivision.AgNumberRightPlaces = 0
        Me.TxtDivision.AgPickFromLastValue = False
        Me.TxtDivision.AgRowFilter = ""
        Me.TxtDivision.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDivision.AgSelectedValue = Nothing
        Me.TxtDivision.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDivision.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDivision.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDivision.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDivision.Location = New System.Drawing.Point(321, 141)
        Me.TxtDivision.Margin = New System.Windows.Forms.Padding(3, 3, 3, 20)
        Me.TxtDivision.MaxLength = 15
        Me.TxtDivision.Name = "TxtDivision"
        Me.TxtDivision.Size = New System.Drawing.Size(364, 18)
        Me.TxtDivision.TabIndex = 93
        '
        'FrmBankReconciliation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(982, 638)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtDivision)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtSite)
        Me.Controls.Add(Me.LblToAmount)
        Me.Controls.Add(Me.TxtToAmount)
        Me.Controls.Add(Me.LblFromAmount)
        Me.Controls.Add(Me.TxtFromAmount)
        Me.Controls.Add(Me.TxtFromDate)
        Me.Controls.Add(Me.Label7)
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
    Friend WithEvents TxtFromDate As AgControls.AgTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtFromAmount As AgControls.AgTextBox
    Friend WithEvents LblFromAmount As Label
    Friend WithEvents LblToAmount As Label
    Friend WithEvents TxtToAmount As AgControls.AgTextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TxtSite As AgControls.AgTextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtDivision As AgControls.AgTextBox
End Class
